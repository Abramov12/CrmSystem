using CrmComputerModel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using CrmComputerModel;
using Wpf.Ui.Controls;

namespace UI
{
    /// <summary>
    /// Логика взаимодействия для AddItem2.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        Crmcontext database;
        public AddProduct(Crmcontext db)
        {
            InitializeComponent();
            database = db;
        }
        
        private void ClickConfirm(object sender, RoutedEventArgs e)
        {
            database.Products.Add(new Product(Name.Text, decimal.Parse(Price.Text),Int32.Parse(Count.Text)));
            database.SaveChanges();
            this.Close();
        }
    }
}
