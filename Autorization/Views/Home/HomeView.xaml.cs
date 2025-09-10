using System.Windows;
using System.Windows.Controls;

namespace WeatherAppWPF.Views.Home
{
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void LeftArrowButton_Click(object sender, RoutedEventArgs e)
        {
            WeatherScrollViewer.LineLeft();
        }

        private void RightArrowButton_Click(object sender, RoutedEventArgs e)
        {
            WeatherScrollViewer.LineRight();
        }
    }
}
