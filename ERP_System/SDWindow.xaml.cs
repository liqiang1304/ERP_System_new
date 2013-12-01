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
        private int selectedCustomerCode = -1;
        //public static String selectedCustomerName;
        //public static int selectedCustomerCode;

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

                db.customers.Add(customer_Insert);
                db.SaveChanges();
                MessageBox.Show("已新增客户：" + CompanyNameTextBox.Text.ToString(), "新增客户成功！",MessageBoxButton.OK, MessageBoxImage.Information);
                CompanyCodeTextBox.Text = "";
                CompanyNameTextBox.Text = "";
                AddressTextBox.Text = "";
                TexRateTextBox.Text = "";
                CountryTextBox.Text = "";
            }
            catch
            {
                MessageBox.Show("数据库写入异常！", "新增客户失败！", MessageBoxButton.OK, MessageBoxImage.Information);
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
            }
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            reload_EditDataGrid();
        }

        private void reload_EditDataGrid()
        {
            ERP_System.ERP_DBDataSet eRP_DBDataSet = ((ERP_System.ERP_DBDataSet)(this.FindResource("eRP_DBDataSet")));
            // 将数据加载到表 contactPerson 中。可以根据需要修改此代码。
            ERP_System.ERP_DBDataSetTableAdapters.contactPersonTableAdapter eRP_DBDataSetcontactPersonTableAdapter = new ERP_System.ERP_DBDataSetTableAdapters.contactPersonTableAdapter();
            //eRP_DBDataSetcontactPersonTableAdapter.Fill(eRP_DBDataSet.contactPerson);
            if (selectedCustomerCode != -1)
                eRP_DBDataSetcontactPersonTableAdapter.FillByCode(eRP_DBDataSet.contactPerson, selectedCustomerCode);
            System.Windows.Data.CollectionViewSource contactPersonViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("contactPersonViewSource")));
            contactPersonViewSource.View.MoveCurrentToFirst();
        }

        private void contactPersonInCustomer_Filter(object sender, FilterEventArgs e)
        {
            contactPerson t = e.Item as contactPerson;
            if (t != null)
            // If filter is turned on, filter completed items.
            {
                if (t.companyCode != selectedCustomerCode)
                    e.Accepted = false;
                else
                    e.Accepted = true;
            }
        }

        private void ChoseCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            BrowseCustomerDialog browseCustomerDialog = new BrowseCustomerDialog();
            browseCustomerDialog.selectedCustomer += new selectedCustomerHandler(ChoseCustomerChanged);
            browseCustomerDialog.Show();
        }

        void ChoseCustomerChanged(String selectedCustomerName, int selectedCustomerCode)
        {
            ChoseCustomerCodeTextBox.Text = selectedCustomerName;
            ChoseCustomerNameTextBox.Text = selectedCustomerCode.ToString();
            this.selectedCustomerCode = selectedCustomerCode;

            reload_EditDataGrid();

            contactPersonDataGrid.UpdateLayout();
        }

        private void NewContactPerson_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
