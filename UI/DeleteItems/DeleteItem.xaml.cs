using CrmComputerModel;
using CrmComputerModel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Printing;
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

namespace UI.DeleteItems
{
    /// <summary>
    /// Логика взаимодействия для DeleteItem.xaml
    /// </summary>
    public partial class DeleteItem : Window
    {
        Crmcontext database;
        int Type;
        public DeleteItem(Crmcontext db,int type)
        {
            InitializeComponent();
            database = db;
            Type = type;
        }

        private void FindId_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Type == 1)
                {
                    var id = Int32.Parse(Id.Text);
                    var query = from Customers in database.Customers
                                where Customers.CustomerId == id
                                select new { Customers.CustomerName };
                    DeleteObjectName.Content = "Вы собираетесь удалить объект с названием " + query.First().CustomerName;
                    ConfirmDelete.Visibility = Visibility.Visible;
                    ConfirmDelete.Content = "Подтвердить";
                    ConfirmDelete.BorderBrush = new SolidColorBrush(Colors.Black);
                }
                else if (Type == 0)
                {
                    var id = Int32.Parse(Id.Text);
                    var query = from Sellers in database.Sellers
                                where Sellers.SellerId == id
                                select new { Sellers.SellerName };
                    
                    DeleteObjectName.Content = "Вы собираетесь удалить объект с названием " + query.First().SellerName;
                    ConfirmDelete.Content = "Подтвердить";
                    ConfirmDelete.Visibility = Visibility.Visible;
                    ConfirmDelete.BorderBrush = new SolidColorBrush(Colors.Black);
                }
                else
                {
                    var id = Int32.Parse(Id.Text);
                    var query = from Products in database.Products
                                where Products.ProductId == id
                                select new { Products };
                    DeleteObjectName.Content = "Вы собираетесь удалить объект с названием " + query.First().Products.ProductName;
                    ConfirmDelete.Content = "Подтвердить";
                    ConfirmDelete.Visibility = Visibility.Visible;
                    ConfirmDelete.BorderBrush = new SolidColorBrush(Colors.Black);
                }
            }
            catch
            {
                Id.Text = "Такого id не существует.";
            }
            
        }
        public void Delete(object sender, RoutedEventArgs e)
        {
            if (Type == 1)
            {
                var id = Int32.Parse(Id.Text);
                Customer query = database.Customers.Where(c => c.CustomerId == id).FirstOrDefault();
                if(query!=null)
                    database.Customers.Remove(query);
                database.SaveChanges();
                Close();
            }
            else if (Type == 0)
            {
                var id = Int32.Parse(Id.Text);
                Seller query = database.Sellers.Where(c => c.SellerId == id).FirstOrDefault();
                if (query != null)
                    database.Sellers.Remove(query);
                database.SaveChanges();
                Close();
            }
            else
            {
                var id = Int32.Parse(Id.Text);
                Product query = database.Products.Where(c => c.ProductId == id).FirstOrDefault();
                if (query != null)
                    database.Products.Remove(query);
                database.SaveChanges();
                Close();
            }
        }
    }
}
