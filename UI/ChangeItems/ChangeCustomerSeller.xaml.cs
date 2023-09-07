using CrmComputerModel;
using CrmComputerModel.Models;
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

namespace UI.ChangeItems
{
    /// <summary>
    /// Логика взаимодействия для ChangeCustomerSeller.xaml
    /// </summary>
    public partial class ChangeCustomerSeller : Window
    {
        public Crmcontext database;
        public int Type;
        public ChangeCustomerSeller(Crmcontext db, int type)
        {
            InitializeComponent();
            Type = type;
            database = db;
        }

        private void FindId(object sender, RoutedEventArgs e)
        {
            if (Type == 1)
            {
                var id = Int32.Parse(SearchId.Text);
                var query = from Customers in database.Customers
                            where Customers.CustomerId == id
                            select new { Customers.CustomerName };
                try
                {
                    PreviousName.Content = "Изменяемое имя: " + query.First().CustomerName;
                    EnterNewNameLabel.Content = "Введите новое имя";
                    EnterNewNameTextBox.Visibility = Visibility.Visible;
                    Enter.Visibility = Visibility.Visible;
                    EnterNewNameTextBox.BorderBrush = new SolidColorBrush(Colors.Black);
                    Enter.BorderBrush = new SolidColorBrush(Colors.Black);
                    Enter.Content = "Подтвердить";
                }
                catch
                {
                    PreviousName.Content = "Такого id нет в списке объектов";
                }
            }
            else
            {
                var id = Int32.Parse(SearchId.Text);
                var query = from Sellers in database.Sellers
                            where Sellers.SellerId == id
                            select new { Sellers.SellerName };
                try
                {
                    PreviousName.Content = "Изменяемое имя: " + query.First().SellerName;
                    EnterNewNameLabel.Content = "Введите новое имя";
                    EnterNewNameTextBox.Visibility = Visibility.Visible;
                    Enter.Visibility = Visibility.Visible;
                    Enter.Content = "Подтвердить";
                    Enter.BorderBrush = new SolidColorBrush(Colors.Black);
                }
                catch
                {
                    PreviousName.Content = "Такого id нет в списке объектов";
                }
            }
        }
        private void UpdateItem(object sender, RoutedEventArgs e)
        {
            int id = Int32.Parse(SearchId.Text);
            if (Type == 1)
            {
                var q = database.Customers.Where(s => s.CustomerId == id);
                q.First().CustomerName = EnterNewNameTextBox.Text; 
            }
            else
            {
                var q = database.Sellers.Where(s => s.SellerId == id);
                q.First().SellerName = EnterNewNameTextBox.Text; 
            }
            
            database.SaveChanges();
            this.Close();
        }
    }
}
