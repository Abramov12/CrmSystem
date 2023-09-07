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
using CrmComputerModel;

namespace UI
{
    /// <summary>
    /// Логика взаимодействия для Model.xaml
    /// </summary>
    public partial class Model : Window
    {
        ShopComputerModel Shopmodel ;
        public Model()
        {
            InitializeComponent();
        }
        public void ShowCashDesks(object sender, EventArgs e)
        {
            ModelForm.Height = 300 + 45 * (int)CashDesksCount.Value;
            ModelForm.Width = 950;
            ShopComputerModel model = new ShopComputerModel((int)CashDesksCount.Value);
            Shopmodel = model;
            var cashDesks = new List<CashDeskView>();
            StackPanel myPanel = new StackPanel();
            for (int i = 0; i <model.CashDeskCount; i++)
            {
                cashDesks.Add(new CashDeskView(myPanel,model.CashDesks[i], i+1));
            }
            CashDeskView.OtherInterface(myPanel,model);
            this.Content = myPanel;
            model.Start();
        }
        public void Model_FormClosing(object sender, EventArgs e)
        {
            Shopmodel.Stop();
        }
    }
}
