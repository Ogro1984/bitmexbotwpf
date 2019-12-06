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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BitmexbotWPF.Helpers;
using BitmexbotWPF.Objects;

namespace BitmexbotWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OrderHandler orderhandler = new OrderHandler();
        string order;
  
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void GetCandelstickData_Click(object sender, RoutedEventArgs e)
        {
            List<Objects.Candle> candleslist = new List<Objects.Candle>();

            candleslist = orderhandler.GetCandelstickData();
            foreach (var item in candleslist) {
                texbox1.Text= item.close.ToString();
                texbox1.Text = item.Timestamp.ToString();
                listView.Items.Add(item.close.ToString());


            }
            

        }

        private void Gotochartbutton_Click(object sender, RoutedEventArgs e)
        {
           chart win2 = new chart();
            win2.Show();
           // this.Close();
        }
    }
}
