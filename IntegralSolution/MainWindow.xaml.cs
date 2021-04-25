using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using CalculateLibrary;
using OxyPlot;

namespace IntegralSolution
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btCalculate_Click(object sender, RoutedEventArgs e)
        {
            List<(double, int)> time = Calculate();
            DrawGraph(time);
        }

        private void DrawGraph(List<(double, int)> times)
        {
            ClearGraph();
            MainViewModel model = DataContext as MainViewModel;
            model.Points.Clear();

            foreach (var (time, count) in times)
            {
                model.Points.Add(new DataPoint(count, time));
            }

            graph.InvalidatePlot(true);
            this.Cursor = Cursors.Arrow;
        }

        private void ClearGraph()
        {
            var model = DataContext as MainViewModel;
            model.Points.Clear();
            graph.InvalidatePlot(true);
        }

        private List<(double, int)> Calculate()
        {
            this.Cursor = Cursors.Wait;

            double a = Convert.ToDouble(tbLowerBound.Text);
            double b = Convert.ToDouble(tbUpperBound.Text);
            long n = Convert.ToInt64(tbN.Text);

            ICalculator calculator = GetCalculator();
            List<(double, int)> time = new List<(double, int)>();

            double result = calculator.Calculate(a, b, n, x => 11 * x - Math.Log(11 * x) - 2);

            for (int i = 0; i < n; i += 1000)
            {
                DateTime timeStart = DateTime.Now;

                calculator.Calculate(a, b, i, x => 11 * x - Math.Log(11 * x) - 2);

                DateTime timeStop = DateTime.Now;
                time.Add(((timeStop - timeStart).TotalMilliseconds, i));
            }

            tbResult.Text = Convert.ToString(result);

            return time;
        }

        private ICalculator GetCalculator()
        {
            switch (cbmMethod.SelectedIndex)
            {
                case 0:
                    return new RectangleCalculator();break;
                case 1:
                    return new TrapCalculator();break;
                case 2:
                    return new ParabolaCalculator();break;
                default:
                    return new RectangleCalculator();break;
            }
        }

        private void btClear_Click(object sender, RoutedEventArgs e)
        {
            ClearGraph();
        }
    }
}
