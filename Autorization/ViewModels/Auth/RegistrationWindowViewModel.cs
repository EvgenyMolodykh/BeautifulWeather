using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using WeatherAppWPF.Interfaces;
using WeatherAppWPF.Models;
using WeatherAppWPF.Repository;
using WeatherAppWPF.Services.GeoCoder;
using WeatherAppWPF.Services.SettingsService;
using static WeatherAppWPF.ViewModels.MainWindowViewModel;

namespace WeatherAppWPF.ViewModels.Auth
{
    public class RegistrationWindowViewModel : ViewModelBase
    {
        public ICommand? RegistrationCommand { get; }

        private string loginRegistration;
        public string LoginRegistration
        {
            get { return loginRegistration; }
            set
            {
                loginRegistration = value;
                OnPropertyChanged();
            }
        }
        private string regPassword;
        public string RegPassword
        {
            get { return regPassword; }
            set
            {
                regPassword = value;
                OnPropertyChanged();
            }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                OnPropertyChanged();
            }
        }

        private string yandexApiKey;
        public string YandexApiKey
        {
            get { return yandexApiKey; }
            set
            {
                yandexApiKey = value;
                OnPropertyChanged();
            }
        }

        private readonly IUserProvider userProvider;
        private readonly GeoCoderService geoCoderService;
        private readonly DatabaseContext databaseContext;

        public RegistrationWindowViewModel(IUserProvider userProvider, GeoCoderService geoCoderService, DatabaseContext databaseContext)
        {
            RegistrationCommand = new RelayCommand(newRegister, canRegister);
            this.userProvider = userProvider;
            this.geoCoderService = geoCoderService;
            this.databaseContext = databaseContext;
        }

        private bool canRegister(object arg)
        {
            return true;
        }

        private void newRegister(object obj)//user
        {
            if (!validationInputRegister(LoginRegistration, RegPassword, ConfirmPassword))
            {
                return;
            }


            if (!validationApiKey(YandexApiKey))
            {
                return;
            }

            var allUsers = userProvider.GetAllUsers();
            foreach (var user in allUsers)
            {
                if (user.IsSingIn)
                {
                    user.IsSingIn = false;
                    userProvider.Update(user);
                }
            }

            var userRegister = new User(LoginRegistration, RegPassword, YandexApiKey)
            {
                IsSingIn = true
            };

            if (userProvider.GetUser(userRegister) != null)
            {
                MessageBox.Show("Пользователь с таким логином уже существует");
                return;
            }

            userProvider.Add(userRegister);
            MessageBox.Show("Пользователь успешно зарегистрирован");

            var registrationWindow = obj as Window;
            registrationWindow?.Close();
        }


        private bool validationInputRegister(string inputLogin, string inputPassword, string inputConfirmPassword)
        {
            if (string.IsNullOrWhiteSpace(inputPassword) || string.IsNullOrWhiteSpace(inputConfirmPassword) || string.IsNullOrWhiteSpace(inputLogin))
            {
                MessageBox.Show("Поля не могут быть пустыми, заполните все поля");
                return false;
            }

            if (inputPassword != inputConfirmPassword)
            {
                MessageBox.Show("Пароли не совпадают");
                return false;
            }

            return true;
        }

        private bool validationApiKey(string inputApiKey)
        {
            if (string.IsNullOrEmpty(inputApiKey))
            {
                return false;
                MessageBox.Show("Ключ не может быть пустым");
                
            }

            else
            {
                bool isValid = geoCoderService.TestApiKey(inputApiKey);
                if (!isValid)
                {
                    MessageBox.Show("Ключ API не верен или сервис не доступен");
                    return false;
                }
                return isValid;
            }
        }
    }
}
