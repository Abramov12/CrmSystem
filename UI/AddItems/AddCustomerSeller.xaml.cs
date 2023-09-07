using CrmComputerModel;
using CrmComputerModel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// Логика взаимодействия для AddItem.xaml
    /// </summary>
    public partial class AddSellerCustomer : Window
    {
        Crmcontext dataBase;
        int typeFact;
        public AddSellerCustomer(Crmcontext db,int type)
        {
            InitializeComponent();
            dataBase=db;
            typeFact = type;
        }

        public void ClickConfirm(object sender, RoutedEventArgs e)
        {
            if (typeFact == 1)
            {
                dataBase.Customers.Add(new Customer(Input.Text));
                dataBase.SaveChanges();
            }
            else 
            {
                dataBase.Sellers.Add(new Seller(Input.Text));
                dataBase.SaveChanges();
            }
            this.Close();
        }
    }
}
