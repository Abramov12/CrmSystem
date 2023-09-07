using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CrmComputerModel;
using CrmComputerModel.Models;
using Wpf.Ui.Controls;
using Brushes = System.Drawing.Brushes;
using Color = System.Windows.Media.Color;
using MenuItem = System.Windows.Controls.MenuItem;


namespace UI
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        Crmcontext db;
        Cart cart;
        Customer customer;
        CashDesk CashDesk;
        
        public MainWindow()
        {
            InitializeComponent();
            db = new Crmcontext(); 
            cart = new Cart(customer);
            CashDesk = new CashDesk(1, db.Sellers.FirstOrDefault(),db);
            CashDesk.IsModel = false;
        }

        private void Load(object sender,EventArgs e)
        {
            Task.Run(() =>
            {
                AllProducts.Dispatcher.Invoke((Action)delegate  
                {
                    
                    UpdateLists();
                });
                
            });
        }
        private void UpdateLists()
        {
            db.Products.RemoveRange(db.Products.Where(x => x.ProductCount == 0));
            db.SaveChanges();
            AllProducts.ItemsSource = db.Products.ToArray();
            Cart.ItemsSource = cart.GetAll().ToArray();
            Price.Content = "Total amount: " + cart.Price;
        }
        private void Update(object sender, RoutedEventArgs e)
        {
            UpdateLists();
        }

        private void SelectItems(object sender, MouseButtonEventArgs e)
        {
            if(AllProducts.SelectedItem is Product product)
            {
                cart.AddProduct(product);
                UpdateLists();
            }
        }
        
        private void EnterName(object sender,EventArgs e)
        {
            var login = new Login();
            login.ShowDialog();
            if (login.DialogResult==true)
            {
                var TempCustomer = db.Customers.FirstOrDefault(c => c.CustomerName.Equals(login.Customer.CustomerName));
                if (TempCustomer != null)
                {
                    customer = TempCustomer;
                }
                else
                {
                    db.Customers.Add(login.Customer);
                    db.SaveChanges();
                    customer = login.Customer;
                }
                cart.Customer= customer; 
                CustomerName.Content = "Welcome, " + customer.CustomerName;
            }
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)((MenuItem)sender).Parent;
            if (menuItem.Name == "Покупатель")
            {
                AddSellerCustomer addCustomer = new AddSellerCustomer(db,1);
                addCustomer.Show();  
            }
            else if (menuItem.Name == "Продавец")
            {
                AddSellerCustomer addSeller = new AddSellerCustomer(db,0);
                addSeller.Show();    
            }
            else
            {
                AddProduct addProduct = new AddProduct(db);
                addProduct.Show();
                UpdateLists();
            }
        }
        private void ChangeItem(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)((MenuItem)sender).Parent;
            if(menuItem.Name == "Покупатель")
            {
                var ChangeCustomer = new ChangeItems.ChangeCustomerSeller(db, 1);
                ChangeCustomer.Show();
            }
            else if (menuItem.Name == "Продавец")
            {
                var ChangeSeller = new ChangeItems.ChangeCustomerSeller(db, 0);
                ChangeSeller.Show();
            }
            else
            {
                var ChangeProduct = new ChangeItems.ChangeProduct(db);
                ChangeProduct.Show();
            }
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)((MenuItem)sender).Parent;
            if (menuItem.Name == "Покупатель")
            {
                var DeleteCustomer = new DeleteItems.DeleteItem(db, 1);
                DeleteCustomer.Show();
            }
            else if (menuItem.Name == "Продавец")
            {
                var DeleteSeller = new DeleteItems.DeleteItem(db, 0);
                DeleteSeller.Show();
            }
            else
            {
                var DeleteProduct = new DeleteItems.DeleteItem(db,2);
                DeleteProduct.Show();
            }
        }

        private void ShowCustomer(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            Catalog catalog = new Catalog();
            var query =
             from Customer in db.Customers
             select new { Customer.CustomerId, Customer.CustomerName };
            catalog.Grid.ItemsSource = query.ToList();
            catalog.Show();
        }
        
        private void ShowProduct(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            Catalog catalog = new Catalog();
            var query =
             from Product in db.Products
             select new { Product.ProductId, Product.ProductName, Product.ProductPrice,Product.ProductCount};
            catalog.Grid.ItemsSource = query.ToList();
            catalog.Show();
        }
        private void ShowCheck(object sender,RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            Catalog catalog = new Catalog();
            var query =
             db.Checks;
            catalog.Grid.ItemsSource = query.ToList();
            catalog.Show();
        }
        
        private void ShowSeller(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            Catalog catalog = new Catalog();
            var query =
             from Seller in db.Sellers
             select new { Seller.SellerId, Seller.SellerName };
            catalog.Grid.ItemsSource = query.ToList();
            catalog.Show();
        }
        private void ShowModelWindow(object sender,EventArgs e)
        {
            Model model=new Model();
            model.Show();
        }

        private void Pay(object sender, RoutedEventArgs e)
        {
            if (customer!=null)
            {
                CashDesk.Enqueue(cart);
                var price=CashDesk.Dequeue();
                Cart.ItemsSource = null;
                cart = new Cart(customer);
                UpdateLists();
                var box = new Wpf.Ui.Controls.MessageBox();
                box.ButtonLeftName = "Continue shopping";
                box.ButtonRightName = "Leave";
                box.ButtonLeftClick += MessageBox_Close;
                box.ButtonRightClick += MessageBox_Close;
                box.ButtonRightClick += MainWindow_Close;
                box.Show("Purchase completed", "The purchase was successful. Total amount: " + price);
            }
            else
            {
                var box = new Wpf.Ui.Controls.MessageBox();
                box.ButtonLeftName = "Enter a name";
                box.ButtonRightName = "Back";
                box.ButtonLeftClick += MessageBox_Close;
                box.ButtonLeftClick += EnterName;
                box.ButtonRightClick += MessageBox_Close;
                box.Show("Name not entered", "To make a purchase, you must enter a name.");
            }
        }
        private void MessageBox_Close(object sender, System.Windows.RoutedEventArgs e)
        {
            (sender as Wpf.Ui.Controls.MessageBox)?.Close();
        }

        private void MainWindow_Close(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindowForm.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            foreach(Window w in App.Current.Windows)
            {
                w.Close();
            }
        }
    }
}
