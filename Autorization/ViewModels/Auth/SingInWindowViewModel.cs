using System.Windows;
using System.Windows.Input;
using WeatherAppWPF.Models;
using WeatherAppWPF.Repository;
using WeatherAppWPF.Services;
using static WeatherAppWPF.ViewModels.MainWindowViewModel;


namespace WeatherAppWPF.ViewModels.Auth
{
    public class SingInWindowViewModel : ViewModelBase
    {
        private readonly UserStorage userStorage;
        private readonly SettingKey settingKey;

        public ICommand? SignInCommand { get;}
        private string login;
        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged();
            }
        }
        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }
        private bool? rememberMe;
        public bool? RememberMe
        {
            get => rememberMe;
            set
            {
                rememberMe = value;
                OnPropertyChanged();
            }
        }
        public SingInWindowViewModel(UserStorage userStorage, SettingKey settingKey)
        {
            this.userStorage = userStorage;
            this.settingKey = settingKey;
            SignInCommand = new RelayCommand(OnSingIn, CanSingIn);
        }
        private bool CanSingIn(object arg)
        {
            return true;
        }
        private void OnSingIn(object obj)
        {
            //var userKey = settingKey.LoadKey(user);
            var userRegister = new User(Login, Password);
            if (ValidationInput(Login, Password))
            {
                var existingUser = userStorage.GetUser(userRegister);
                if (existingUser == null)
                {
                    MessageBox.Show("Не верные данные для входа");
                    return;
                }
                
                existingUser.IsSingIn = true;
                
                userStorage.Update(existingUser);
                MessageBox.Show("Вы успешно авторизовались");

                var singInWindowUser = obj as Window;
                singInWindowUser.Close();
            }
            return;
        }

        private bool ValidationInput(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Заполните все поля");
                return false;
            }
            return true;
        }
    }
}



