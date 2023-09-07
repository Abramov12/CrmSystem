using CrmComputerModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Label = System.Windows.Controls.Label;
using System.Windows.Controls.Primitives;
using System.Drawing;
using System.Windows.Media;

namespace UI
{
    public class CashDeskView
    {
        CashDesk cashDesk;
        public Label CashDeskName { get; set; }
        public TextBox Price { get; set; }
        static public Label QueueLeave { get; set; }

        public static ShopComputerModel model;
        public ProgressBar bar { get; set; }
        public static TextBox CashDeskSpeedDisplay { get; set; }
        public static TextBox ClientSpeed { get; set; }

        public int SumChtole = 0;

        public CashDeskView(StackPanel myPanel, CashDesk cashDesk, int number)
        {
            this.cashDesk = cashDesk;
            CashDeskName = new Label();
            CashDeskName.Name = "a" + cashDesk.Number.ToString();
            CashDeskName.Content = cashDesk.ToString();
            CashDeskName.Width = 90;
            CashDeskName.Height = 40;
            CashDeskName.VerticalAlignment = VerticalAlignment.Top;
            CashDeskName.HorizontalAlignment = HorizontalAlignment.Left;
            CashDeskName.Margin = new Thickness(15, 30, 0, 0);

            Price = new TextBox();
            Price.Name = "Sum" + number;
            Price.Width = 95;
            Price.Height = 40;
            Price.VerticalAlignment = VerticalAlignment.Top;
            Price.HorizontalAlignment = HorizontalAlignment.Left;
            Price.Margin = new Thickness(114, -48, 0, 0);


            bar = new ProgressBar();
            bar.Name = "Bar";
            bar.Width = 300;
            bar.Height = 40;
            bar.VerticalAlignment = VerticalAlignment.Top;
            bar.HorizontalAlignment = HorizontalAlignment.Left;
            bar.Margin = new Thickness(220, -50, 0, 0);
            bar.Maximum = cashDesk.MaxQueueLength;
            bar.Value = 0;

            myPanel.Children.Add(CashDeskName);
            myPanel.Children.Add(Price);
            myPanel.Children.Add(bar);
            cashDesk.CheckClosed += CashDesk_CheckClosed;
        }

        public static void OtherInterface(StackPanel myPanel, ShopComputerModel models)
        {
            model = models;

            var Grid = new Grid();
            Grid.VerticalAlignment = VerticalAlignment.Top;
            Grid.Margin = new Thickness(0, -70*model.CashDeskCount-100, 0, 0);


            //Names mismatch.
            //speed of objects works using sleep, so increasing the speed of the customer, the queue at the cashdesks decreases.
            //Therefore, the decision was made to change the titles of the labels

            var ClientSpeedLabel = new Label();
            ClientSpeedLabel.Content = "EnterCashDeskSpeed";
            ClientSpeedLabel.Height = 35;
            ClientSpeedLabel.Margin = new Thickness(580, -55, 0, 0);


            ClientSpeed = new TextBox();
            ClientSpeed.Text = models.CustomerSpeed.ToString();
            ClientSpeed.Margin = new Thickness(685, -61, 0, 0);
            ClientSpeed.Height = 35;
            ClientSpeed.Width = 100;
            ClientSpeed.TextChanged += ClientSpeed_TextChanged;


            var CashDeskSpeed = new Label();
            CashDeskSpeed.Height = 35;
            CashDeskSpeed.Content = "Enter ClientSpeed ";
            CashDeskSpeed.Margin = new Thickness(580, 62, 0, 0);


            CashDeskSpeedDisplay = new TextBox();
            CashDeskSpeedDisplay.Text = models.CashDeskSpeed.ToString();
            CashDeskSpeedDisplay.Height = 35;
            CashDeskSpeedDisplay.Width = 100;
            CashDeskSpeedDisplay.Margin = new Thickness(685, 50, 0, 0);
            CashDeskSpeedDisplay.TextChanged += FrequencyDisplay_TextChanged;


            var ClientsLeave = new Label();
            ClientsLeave.Margin = new Thickness(580, 150, 0, 0);
            ClientsLeave.Content = "ClientsLeave:";
            ClientsLeave.Height = 30;


            QueueLeave = new Label();
            QueueLeave.Name = "LeaveCustomers";
            QueueLeave.Width = 140;
            QueueLeave.Height = 30;
            QueueLeave.Margin = new Thickness(689, 144, 0, 0);
            QueueLeave.Content = "0";


            var RandomPick = new Label();
            RandomPick.Content = "RandomPick";
            RandomPick.Height = 30;
            RandomPick.Margin = new Thickness(580, 225, 0, 0);


            var Switch = new CheckBox();
            Switch.Content = "";
            Switch.Height = 30;
            Switch.Margin = new Thickness(739, 215, 0, 0);
            Switch.IsChecked= true;
            model.RandomPick = true;
            Switch.Checked += Switch_Checked;
            Switch.Unchecked += Switch_Unchecked;


            Button button = new Button();
            button.Content = "Stop Simulating";
            button.Name = "StopSimultaion";
            button.Click += Stop_Click;
            button.Height = 34;
            button.HorizontalAlignment = HorizontalAlignment.Right;
            button.VerticalAlignment = VerticalAlignment.Bottom;
            button.Margin = new Thickness(0, 320,120, 0);


            Grid.Children.Add(ClientSpeedLabel);
            Grid.Children.Add(ClientSpeed);
            Grid.Children.Add(CashDeskSpeed);
            Grid.Children.Add(CashDeskSpeedDisplay);
            Grid.Children.Add(ClientsLeave);
            Grid.Children.Add(QueueLeave);
            Grid.Children.Add(RandomPick);
            Grid.Children.Add(Switch);
            Grid.Children.Add(button);
            myPanel.Children.Add(Grid);
        }

        public static void Stop_Click(object sender, RoutedEventArgs e)
        {
            model.Stop();
        }
        private void CashDesk_CheckClosed(object? sender, Check e)
        {
            Price.Dispatcher.Invoke((Action)delegate
            {
                SumChtole += (int)Math.Ceiling(e.Price);
                Price.Text = SumChtole.ToString();
                bar.Value = cashDesk.count;
                if (Int32.Parse((string)QueueLeave.Content) < cashDesk.ExitCustomer)
                    QueueLeave.Content = cashDesk.ExitCustomer.ToString();
            });
        }

        private static void ClientSpeed_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ClientSpeed.Text.Equals("")) 
            {
                model.CustomerSpeed = 0;
            }
            else
            {
                model.CustomerSpeed = Int32.Parse(ClientSpeed.Text);
            }
        }

        private static void FrequencyDisplay_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CashDeskSpeedDisplay.Text.Equals("")) 
            {
                model.CashDeskSpeed = 0;
            }
            else
            {
                model.CashDeskSpeed = Int32.Parse(CashDeskSpeedDisplay.Text);
            }
        }
        private static void Switch_Checked(object sender, RoutedEventArgs e)
        {
            model.RandomPick = true;
        }

        private static void Switch_Unchecked(object sender, RoutedEventArgs e)
        {
            model.RandomPick = false;
        }
    }
}
