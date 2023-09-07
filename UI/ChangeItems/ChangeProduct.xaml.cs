using CrmComputerModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Metrics;
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
using Wpf.Ui.Controls;

namespace UI.ChangeItems
{
    /// <summary>
    /// Логика взаимодействия для ChangeProduct.xaml
    /// </summary>
    public partial class ChangeProduct : Window
    {
        public Crmcontext database;
        public ChangeProduct(Crmcontext db)
        {
            InitializeComponent();
            database = db;
        }

        private void FindId(object sender, RoutedEventArgs e)
        {
            var id = Int32.Parse(SearchId.Text);
            var query = from Products in database.Products
                        where Products.ProductId == id
                        select new { Products };
            try
            {
                PrevProductName.Content = "Название изменяемого товара: " + query.First().Products.ProductName;
                PrevProductPrice.Content = "Цена изменяемого товара: " + query.First().Products.ProductPrice;
                EnterNewNameLabel.Content = "Введите новое имя";
                NewName.BorderBrush = new SolidColorBrush(Colors.Black);
                EnterNewPriceLabel.Content = "Введите новую цену";
                NewPrice.BorderBrush = new SolidColorBrush(Colors.Black);
                Confirm.BorderBrush = new SolidColorBrush(Colors.Black);
                Confirm.Content = "Подтвердить";
                PrevCount.Content="Количество изменяемого товара: "+ query.First().Products.ProductCount;
                NewCountLabel.Content = "Введите новое количество товара";
                NewCountText.BorderBrush = new SolidColorBrush(Colors.Black);
                ChangingProductForm.Height = 460;
                ChangingProductForm.Width = 750;
            }
            catch
            {
                SearchId.Text = "Такого id не существует";
            }
        }
        private void Update(object sender, RoutedEventArgs e)
        {
            int id = Int32.Parse(SearchId.Text);
            var q = database.Products.Where(s => s.ProductId == id);
            q.First().ProductName = NewName.Text;
            q.First().ProductPrice = Decimal.Parse(NewPrice.Text);
            q.First().ProductCount = int.Parse(NewCountText.Text);
            database.SaveChanges();
            this.Close();
        }
    }
}
