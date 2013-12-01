using System;
using System.Collections.Generic;
using System.Linq;
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
    /// BrowseCustomerDialog.xaml 的互動邏輯
    /// </summary>
    /// 
    public delegate void selectedCustomerHandler(String a, int b);

    public partial class BrowseCustomerDialog : Window
    {
        public event selectedCustomerHandler selectedCustomer;    

        public BrowseCustomerDialog()
        {
            InitializeComponent();
            BrowseCustomerDataGrid.IsReadOnly = true;
            //set it read only.
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

            ERP_System.ERP_DBDataSet eRP_DBDataSet = ((ERP_System.ERP_DBDataSet)(this.FindResource("eRP_DBDataSet")));
            // 将数据加载到表 customers 中。可以根据需要修改此代码。
            ERP_System.ERP_DBDataSetTableAdapters.customersTableAdapter eRP_DBDataSetcustomersTableAdapter = new ERP_System.ERP_DBDataSetTableAdapters.customersTableAdapter();
            eRP_DBDataSetcustomersTableAdapter.Fill(eRP_DBDataSet.customers);
            System.Windows.Data.CollectionViewSource customersViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("customersViewSource")));
            customersViewSource.View.MoveCurrentToFirst();
            //binding data to datagrid.
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            ConfirmSelect();
        }

        private void BrowseCustomerDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ConfirmSelect();
        }

        private void ConfirmSelect()
        {
            var selectRow = (System.Data.DataRowView)BrowseCustomerDataGrid.SelectedItem;
            selectedCustomer((String)selectRow["name"], (int)selectRow["companyCode"]);
            //use this func to call datagrid in father window to re binding the different data.
            this.Close();
        }

    }
}
