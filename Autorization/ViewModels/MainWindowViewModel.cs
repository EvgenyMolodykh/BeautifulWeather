using System.Windows.Input;
using System.Windows;
using WeatherAppWPF.Interfaces;
using WeatherAppWPF.ViewModels.Auth;
using WeatherAppWPF.Services.GeoCoder;
using WeatherAppWPF.Repository;

namespace WeatherAppWPF.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public ICommand? HomeCommand { get; set; }
        public ICommand? LocationCommand { get; set; }
        public ICommand? SettingsCommand { get; set; }
        public ICommand? CloseCommand { get; set; }
        public ICommand? SingInCommand { get; set; }
        public ICommand? RegisterCommand { get; set; }
        public ICommand? SingOutCommand { get; set; }
        public ICommand? CollapceAppCommand { get; set; }
        public ICommand? FullScreenCommand { get; set; }


        private ViewModelBase currentViewModel;
        private readonly HomeViewViewModel homeViewViewModel;
        private readonly IUserProvider _userProvider;
        private readonly SingInWindowViewModel singInWindowViewModel;
        private readonly RegistrationWindowViewModel registrationWindowViewModel;
        private readonly ILocalizationService localizationService;
        private readonly ISettingService settingService;
        private readonly GeoCoderService geoCoderService;
        private readonly IFavoriteProvider favoriteProvider;
        private readonly DatabaseContext databaseContext;

        public ViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set 
            { 
                currentViewModel = value;
                OnPropertyChanged();
            }
        }
        private bool singInButtonIsVisible;
        public bool SingInButtonIsVisible
        {
            get { return singInButtonIsVisible; }
            set 
            { 
                singInButtonIsVisible = value;
                OnPropertyChanged();
            }
        }
        private bool loginNameLabel;
        public bool LoginNameLabel
        {
            get { return loginNameLabel; }
            set 
            {
                loginNameLabel = value;
                OnPropertyChanged();
            }
        }
        private bool personalDeskLabel;
        public bool PersonalDeskLabel
        {
            get { return personalDeskLabel; }
            set {  personalDeskLabel = value;
            OnPropertyChanged();}
        }

        private bool singOutButton;
        public bool SingOutButton
        {
            get { return singOutButton; }
            set 
            {  
               singOutButton = value;
               OnPropertyChanged();
            }
        }

        private bool registerButton;
        public bool RegisterButton
        {
            get { return registerButton; }
            set 
            { 
                registerButton = value;
                OnPropertyChanged();
            }
        }
        public MainWindowViewModel(HomeViewViewModel homeViewViewModel, 
            IUserProvider userProvider, 
            IFileProvider fileProvider, 
            SingInWindowViewModel singInWindowViewModel, 
            RegistrationWindowViewModel registrationWindowViewModel,
            ILocalizationService localizationService,
            ISettingService settingService,
            GeoCoderService geoCoderService,
            IFavoriteProvider favoriteProvider, DatabaseContext databaseContext)
        {
            this.geoCoderService = geoCoderService;
            this.favoriteProvider = favoriteProvider;
            this.databaseContext = databaseContext;
            _userProvider = userProvider;
            this.singInWindowViewModel = singInWindowViewModel;
            this.registrationWindowViewModel = registrationWindowViewModel;
           
            this.settingService = settingService;
            var user = _userProvider.GetSingInUser();
            if (user != null)
            {
                Autorized(); 
            }
            else
            {
                UnAuthorized();
            }
            HomeCommand = new RelayCommand(OpenHomeView, CanOpenHomeView);
            LocationCommand = new RelayCommand(OpenLocationView, CanOpenLocationView);
            SettingsCommand = new RelayCommand(OpenSettingsView, CanOpenSettingsView);
            CloseCommand = new RelayCommand(CloseView, CanCloseView);
            SingInCommand = new RelayCommand(SingIn, CanSingIn);
            RegisterCommand = new RelayCommand(Register, CanRegister);
            SingOutCommand = new RelayCommand(SingOut, CanSingOut);
            CollapceAppCommand = new RelayCommand(CollapceApp, CanCollapceApp);
            FullScreenCommand = new RelayCommand(FullScreen, CanFullScreenCommand);
            this.homeViewViewModel = homeViewViewModel;
            var settings = settingService.Settings;
            this.localizationService = localizationService;
            localizationService.SetCulture(settings.Cultures);
        }

        private bool _isFullScreen;
        private bool CanFullScreenCommand(object arg)
        {
            return true;
        }

        private void FullScreen(object obj)
        {
            var window = Window.GetWindow(obj as DependencyObject);

            if (window == null) return;

            if (!_isFullScreen)
            {
                window.WindowState = WindowState.Maximized;
                _isFullScreen = true;
            }
            else
            {
                window.WindowState = WindowState.Normal;
                _isFullScreen = false;
            }
            OnPropertyChanged();
        }

        private bool CanCollapceApp(object arg)
        {
            return true;
        }

        private void CollapceApp(object obj)
        {
            var window = obj as Window ?? Application.Current.MainWindow;
            if (window != null)
            {
                window.WindowState = WindowState.Minimized;
            }
        }

        private bool CanSingOut(object arg)
        {
            return true;
        }

        private void SingOut(object obj)
        {
            _userProvider.SingOut();
            UnAuthorized();
        }

        private bool CanRegister(object arg)
        {
            return true;
        }

        private void Register(object obj)
        {
            var registrationWindow = new RegistrationWindow(registrationWindowViewModel);
            registrationWindow.ShowDialog();
            var singInUser = _userProvider.GetSingInUser();
            if (singInUser != null)
            {
                Autorized();
            }
            else
            {
                UnAuthorized();
            }
        }

        private void UnAuthorized()
        {
            LoginNameLabel = false;
            PersonalDeskLabel = false;
            SingOutButton = false;
            RegisterButton = true;
            SingInButtonIsVisible = true;
            LoginNameLabelContent = string.Empty;
        }

        private void Autorized()
        {
            LoginNameLabel = true;
            PersonalDeskLabel = true;
            SingOutButton = true;
            RegisterButton = false;
            SingInButtonIsVisible = false;
            ShowLoggedUser();
        }

        private bool CanSingIn(object obj)
        {
          return true;
        }

        private void SingIn(object obj)
        {
            var singInWindow = new SingInWindow(singInWindowViewModel);
            singInWindow.ShowDialog(); 
            Autorized();
        }

        private void CloseView(object obj)
        {
            Application.Current.MainWindow.Close();
        }

        private bool CanCloseView(object arg)
        {
            return true;
        }

        private bool CanOpenSettingsView(object arg)
        {
            return true;
        }

        private void OpenSettingsView(object obj)
        {
            CurrentViewModel = new SettingVievViewModel(localizationService, settingService);
        }

        private bool CanOpenLocationView(object arg)
        {
            return true;
        }

        private void OpenLocationView(object obj)
        {
            CurrentViewModel = new LocationViewViewModel(geoCoderService, settingService, favoriteProvider, databaseContext);
        }

        private bool CanOpenHomeView(object arg)
        {
            return true;
        }

        private void OpenHomeView(object obj)
        {
            homeViewViewModel.TryUpdateWeather();
            CurrentViewModel = homeViewViewModel;
        }

        private string loginNameLabelContent;
        public string LoginNameLabelContent
        {
            get => loginNameLabelContent;
            set
            {
                loginNameLabelContent = value;
                OnPropertyChanged();
            }
        }

        private void ShowLoggedUser()
        {
            var user = _userProvider.GetSingInUser();
            if (user != null)
            {
                LoginNameLabelContent = user.Login;
            }
            else
            {
                LoginNameLabelContent = string.Empty;
            }
        }
    }
}
