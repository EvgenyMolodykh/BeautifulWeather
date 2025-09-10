using WeatherAppWPF.Models;
using System.Windows;
using System.Windows.Controls;
using System.Timers;
using System.Windows.Media;
using WeatherAppWPF.ViewModels;
using System.Windows.Input;

namespace WeatherAppWPF
{

    public partial class MainWindow : Window
    {
        private System.Timers.Timer timer = new System.Timers.Timer();
        public MainWindow(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            timer.Interval = 3000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            DataContext = mainWindowViewModel;
        }
        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            var hour = DateTime.Now.Hour;
            LinearGradientBrush gradient;
            if (hour >= 7 && hour <= 17)
            {
                var gradientDay = new LinearGradientBrush
                {
                    GradientStops = new GradientStopCollection
                    {
                        new GradientStop((Color)ColorConverter.ConvertFromString("#FFC371"),0),
                        new GradientStop((Color)ColorConverter.ConvertFromString("#FF5F6D"),1),
                    },
                };
            }
            else
            {
                gradient = new LinearGradientBrush
                {
                    GradientStops = new GradientStopCollection
                    {
                        new GradientStop(Colors.Blue, 0.0),
                        new GradientStop(Colors.WhiteSmoke, 1.0)
                    },
                };
                Application.Current.Resources["DayMainWindowGradient"] = gradient;
            }
        }

        private void WeaterDay_Button(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                var day = button.DataContext as DayForecastModel;
                if (day != null)
                {
                    MonitoringPeiod_Label.Content = $"Weather for cast {day.Date.AddDays(-3).ToString("dd MMMM")} - {day.Date.AddDays(+3).ToString("dd MMMM")}";
                }
                return;
            }
        }
    }
}