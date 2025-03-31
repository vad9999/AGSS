using System;
using System.Collections.Generic;
using System.Data;
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
using Microsoft.Data.SqlClient;
using OxyPlot;
using OxyPlot.Series;

namespace AGSS
{
    /// <summary>
    /// Логика взаимодействия для AnalystWindow.xaml
    /// </summary>
    public partial class AnalystWindow : Window
    {
        public AnalystWindow()
        {
            InitializeComponent();
            LoadGraph();
        }

    

        private void CreateReportBTN_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Отчёт создан");
        }

        private void SortBTN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadGraph()
        {
            // Пример данных (координаты)
            var coordinates = new List<DataPoint>
            {
                new DataPoint(0, 3),
                new DataPoint(1, 5),
                new DataPoint(2, 2),
                new DataPoint(3, 7),
                new DataPoint(4, 4)
            };

            // Создание модели графика
            var plotModel = new PlotModel { Title = "График координат" };
            var series = new LineSeries { Title = "Линия", MarkerType = MarkerType.Circle };

            // Добавление точек на график
            foreach (var point in coordinates)
            {
                series.Points.Add(point);
            }

            plotModel.Series.Add(series);
            plotView.Model = plotModel; // Установка модели для отображения
        }
    }
}
