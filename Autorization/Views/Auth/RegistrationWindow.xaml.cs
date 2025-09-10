using WeatherAppWPF.Models;
using System.Windows;
using WeatherAppWPF.Interfaces;
using WeatherAppWPF.ViewModels.Auth;


namespace WeatherAppWPF
{
    public partial class RegistrationWindow : Window
    {
        private readonly IUserProvider _userProvider;
        public RegistrationWindow(RegistrationWindowViewModel registrationWindowViewModel)
        {
            DataContext = registrationWindowViewModel;
            InitializeComponent();
        }
    }
}
