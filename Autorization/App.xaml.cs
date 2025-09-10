using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using WeatherAppWPF.Interfaces;
using WeatherAppWPF.Repository;
using WeatherAppWPF.Services;
using WeatherAppWPF.Services.GeoCoder;
using WeatherAppWPF.Services.Localization;
using WeatherAppWPF.Services.OpenMeteo;
using WeatherAppWPF.Services.ServiceLocator;
using WeatherAppWPF.Services.SettingsService;
using WeatherAppWPF.ViewModels;
using WeatherAppWPF.ViewModels.Auth;

namespace WeatherAppWPF
{
    public partial class App : Application
    {
        private readonly IHost _host;
        public App()
        {
            _host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
            {
                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainWindowViewModel>();
                services.AddSingleton<HomeViewViewModel>();
                services.AddSingleton<IWeatherStorage, WeatherDataStorage>();
                services.AddTransient<IUserProvider, UserStorage>();
                services.AddSingleton<IFileProvider,FileProvider>();//всегда 1 на протяжении работы приложения
                services.AddTransient<SingInWindow>();
                services.AddTransient<SingInWindowViewModel>();
                services.AddTransient<RegistrationWindowViewModel>();
                services.AddTransient<RegistrationWindow>();//каждый раз новый экземпляр
                services.AddSingleton<ILocalizationService, LocalizationService>();
                services.AddSingleton<ISettingService, SettingService>();
                services.AddSingleton<GeoCoderService>();
                services.AddSingleton<OpenMeteoProvider>();
                services.AddSingleton<UserStorage>();
                services.AddSingleton<IFavoriteProvider, FaforiteLocationStorage>();
                services.AddSingleton<SettingKey>();
                services.AddSingleton<SecretProvider>();


                services.AddSingleton<PBKDF2PasswordHasher>();
                var connectionString = "Data Source=WeatherApp.db"; 
                services.AddDbContext<DatabaseContext>(options =>
                {
                    options.UseSqlite(connectionString);
                });
            }).Build();
        }

        protected async void OnStartup(object sender, StartupEventArgs e)
        {
            await _host.StartAsync();
            ServiceLocator.ServiceProvider = _host.Services; 
            var mainWindow = _host.Services.GetService<MainWindow>();
            mainWindow.Show();
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            var settingServicse = _host.Services.GetService<ISettingService>();
            settingServicse.Save();
            using (_host) 
            {
                await _host.StopAsync(TimeSpan.FromSeconds(5));
            }
            base.OnExit(e);
        }
    }
}
