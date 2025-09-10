using System.Windows;
using WeatherAppWPF.ViewModels.Auth;

namespace WeatherAppWPF
{
    public partial class SingInWindow : Window
    {
        public SingInWindow(SingInWindowViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }    
    }
}
