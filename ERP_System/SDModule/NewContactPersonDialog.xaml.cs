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
    /// NewContactPersonDialog.xaml 的互動邏輯
    /// </summary>
    /// 
    public delegate void CloseDialogEvent();

    public partial class NewContactPersonDialog : Window
    {
        private int getCompanyCode;
        private String getCompanyName;
        private ERP_DBEntities db;
        public event CloseDialogEvent closeDialogEvent;  

        public NewContactPersonDialog(String companyName, int companyCode)
        {
            InitializeComponent();
            db = new ERP_DBEntities();
            this.getCompanyCode = companyCode;
            this.getCompanyName = companyName;

            if (companyCode != -1)
            {
                CompanyCodeTextBox.Text = companyCode.ToString();
                //get contact person's company code. if not, then return.
            }
            else
            {
                MessageBox.Show("请选择需要添加联系人的客户！", "错误！");
                this.Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            contactPerson addContactPerson = new contactPerson()
            {
                companyCode = getCompanyCode,
                department = DepartmentTextBox.Text,
                gender = GenderTextBox.Text,
                name = NameTextBox.Text,
                contacnPersonID = int.Parse(ContactPersonIDTextBox.Text)
            };
            //add a new contact person to db.
            db.contactPerson.Add(addContactPerson);
            db.SaveChanges();
            closeDialogEvent();
            //after save in db,use this func to call reload_datagrid func to refresh the data.
            this.Close();
        }
    }
}
