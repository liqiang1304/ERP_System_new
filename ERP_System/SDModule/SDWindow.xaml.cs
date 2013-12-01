using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ERP_System.SDModule
{
    /// <summary>
    /// SDWindow.xaml 的互動邏輯
    /// </summary>
    public partial class SDWindow : Window
    {
        private ERP_DBEntities db;
        private int selectedCustomerCode = -1;//in New contact person page, recode which customer need to add contact person, if not set it to -1

        public SDWindow()
        {
            InitializeComponent();
            db = new ERP_DBEntities();
        }

        private void CompanyCodeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void NewCustomerSaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                customers customer_Insert = new customers
                {
                    companyCode = int.Parse(CompanyCodeTextBox.Text.ToString()),
                    name = CompanyNameTextBox.Text.ToString(),
                    address = AddressTextBox.Text.ToString(),
                    texRate = decimal.Parse(TexRateTextBox.Text.ToString()),
                    country = CountryTextBox.Text.ToString()
                };
                //to new a new customer.
                db.customers.Add(customer_Insert);
                db.SaveChanges();
                MessageBox.Show("已新增客户：" + CompanyNameTextBox.Text.ToString(), "新增客户成功！",MessageBoxButton.OK, MessageBoxImage.Information);
               
                CompanyCodeTextBox.Text = "";
                CompanyNameTextBox.Text = "";
                AddressTextBox.Text = "";
                TexRateTextBox.Text = "";
                CountryTextBox.Text = "";
                // set all the textbox to null;
            }
            catch
            {
                MessageBox.Show("数据库写入异常！", "新增客户失败！", MessageBoxButton.OK, MessageBoxImage.Information);
                //if insert db error then info.
            }
        }

        private void NewCustomerClearButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("是否清空所填内容？", "警告！", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                CompanyCodeTextBox.Text = "";
                CompanyNameTextBox.Text = "";
                AddressTextBox.Text = "";
                TexRateTextBox.Text = "";
                CountryTextBox.Text = "";
                //click clear button to clear all the textbox;
            }
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            reload_EditDataGrid();
            reload_InquiryDataGrid();
        }

        private void reload_InquiryDataGrid()
        {
            ERP_System.ERP_DBDataSet eRP_DBDataSet = ((ERP_System.ERP_DBDataSet)(this.FindResource("eRP_DBDataSet")));
            // 将数据加载到表 inquiry 中。可以根据需要修改此代码。
            ERP_System.ERP_DBDataSetTableAdapters.inquiryTableAdapter eRP_DBDataSetinquiryTableAdapter = new ERP_System.ERP_DBDataSetTableAdapters.inquiryTableAdapter();
            eRP_DBDataSetinquiryTableAdapter.Fill(eRP_DBDataSet.inquiry);
            System.Windows.Data.CollectionViewSource inquiryViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("inquiryViewSource")));
            inquiryViewSource.View.MoveCurrentToFirst();
        }

        private void reload_EditDataGrid()
        {
            ERP_System.ERP_DBDataSet eRP_DBDataSet = ((ERP_System.ERP_DBDataSet)(this.FindResource("eRP_DBDataSet")));
            // 将数据加载到表 contactPerson 中。可以根据需要修改此代码。
            ERP_System.ERP_DBDataSetTableAdapters.contactPersonTableAdapter eRP_DBDataSetcontactPersonTableAdapter = new ERP_System.ERP_DBDataSetTableAdapters.contactPersonTableAdapter();

            if (selectedCustomerCode != -1)
                eRP_DBDataSetcontactPersonTableAdapter.FillByCode(eRP_DBDataSet.contactPerson, selectedCustomerCode);
            //fill the contactPersonDataGrid use binding data which is speical customercode;

            System.Windows.Data.CollectionViewSource contactPersonViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("contactPersonViewSource")));
            contactPersonViewSource.View.MoveCurrentToFirst();
        }

        private void ChoseCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            BrowseCustomerDialog browseCustomerDialog = new BrowseCustomerDialog();
            browseCustomerDialog.selectedCustomer += new selectedCustomerHandler(ChoseCustomerChanged);
            //this is a handler in BrowseCustomerDialog and pass a func ChoseCustomerChanged to it.
            browseCustomerDialog.Show();
        }

        void ChoseCustomerChanged(String selectedCustomerName, int selectedCustomerCode)
        {
            ChoseCustomerCodeTextBox.Text = selectedCustomerCode.ToString();
            ChoseCustomerNameTextBox.Text = selectedCustomerName;
            InquiryCustomerCodeTextBox.Text = selectedCustomerCode.ToString();
            InquiryCustomerNameTextbox.Text = selectedCustomerName;
            this.selectedCustomerCode = selectedCustomerCode;
            //when user select another customer to display it's contact person, it need to chang costomer id and name in add contact person page.
            reload_EditDataGrid();
            //reload datagrid to refresh data selected.
            contactPersonDataGrid.UpdateLayout();
        }

        private void NewContactPerson_Click(object sender, RoutedEventArgs e)
        {
            NewContactPersonDialog newContactPersonDialog = new NewContactPersonDialog(ChoseCustomerNameTextBox.Text, selectedCustomerCode);
            try
            {
                newContactPersonDialog.Show();
            }
            catch
            {
                Console.WriteLine("can't open the dialog because there is not matching conditions");
            }
            newContactPersonDialog.closeDialogEvent += new CloseDialogEvent(reload_EditDataGrid);
            //a handler to pass a func reload_EditDataGrid, when add a new contact person, the page refresh the datagrid of contact person.
        }

        private void RefreshContactPersonButton_Click(object sender, RoutedEventArgs e)
        {
            reload_EditDataGrid();
        }

        private void BrowseCustomerDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void DeleteContactPerson_Click(object sender, RoutedEventArgs e)
        {

            var selectRow = (System.Data.DataRowView)contactPersonDataGrid.SelectedItem;
            int icontactPersonID = (int)selectRow["contacnPersonID"];
            //to identify slected row data in contact datagrid, and find it's person id.
            contactPerson deleteContactPerson = db.contactPerson.First(c => c.contacnPersonID == icontactPersonID);
            //delete contact person linq
            db.contactPerson.Remove(deleteContactPerson);
            db.SaveChanges();
            reload_EditDataGrid();
            //reload datagrid.
        }

        private void SelectInquiryCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            BrowseCustomerDialog browseCustomerDialog = new BrowseCustomerDialog();
            browseCustomerDialog.selectedCustomer += new selectedCustomerHandler(ChoseCustomerChanged);
            //this is a handler in BrowseCustomerDialog and pass a func ChoseCustomerChanged to it.
            browseCustomerDialog.Show();
        }

        private void AddInquiryButton_Click(object sender, RoutedEventArgs e)
        {
            NewInquiryDialog newInquiryDialog = new NewInquiryDialog(selectedCustomerCode);
            newInquiryDialog.Show();

        }
    }
}
