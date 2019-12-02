using LiveCharts;
using LiveCharts.Wpf;
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

namespace BitmexbotWPF
{
    /// <summary>
    /// Lógica de interacción para hart.xaml
    /// </summary>
    public partial class chart : Window
    {
        public chart()
        {
            InitializeComponent();
        }

        private void LoadChart_Click(object sender, RoutedEventArgs e)
        {
            SeriesCollection series = new SeriesCollection {
        new LineSeries
        {
        Values = new ChartValues<double> { 3, 5, 7, 4 }
        },
        new ColumnSeries
        {
        Values = new ChartValues<decimal> { 5, 6, 2, 7 }
        
    }
};
        }

    }
}
