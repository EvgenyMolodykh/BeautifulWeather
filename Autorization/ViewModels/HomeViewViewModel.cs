using WeatherAppWPF.Interfaces;
using WeatherAppWPF.Models;
using WeatherAppWPF.Services.OpenMeteo;

namespace WeatherAppWPF.ViewModels
{
    public class HomeViewViewModel : ViewModelBase
    {
        private readonly IWeatherStorage weatherStorage;
        private readonly ISettingService settingService;
        private List<DayForecastModel> forecastsDays;
        private WeatherForecast currentWeather;
        private DayForecastModel selectedDay;

        public HomeViewViewModel(IWeatherStorage wetherStorage, ISettingService settingService)
        {
            this.weatherStorage = wetherStorage;
            this.settingService = settingService;
            LoadWeatherForSelectedLocation();
        }

        public List<DayForecastModel> ForecastsDays
        {
            get => forecastsDays; 
            set
            {
                forecastsDays = value;
                OnPropertyChanged();
            }
        }
       
        public DayForecastModel SelectedDay
        {
            get => selectedDay;
            set
            {
                selectedDay = value;
                OnPropertyChanged();
            }
        }
      

        public void TryUpdateWeather()
        {
            var settings = settingService.Settings;
            var selectedLocation = settingService.Settings.SelectedLocation;
            

            if (currentWeather == null || Math.Round(selectedLocation.Latitude,1) != Math.Round(currentWeather.Location.Latitude,1) || Math.Round(selectedLocation.Longitude,1) != currentWeather.Location.Longitude)
            {
                var weather = weatherStorage.Get((float)selectedLocation.Latitude, (float)selectedLocation.Longitude, new ForecastMeasuresModel { Temperature = settings.Temperature }, selectedLocation.Name);
                currentWeather = weather;
                ForecastsDays = weather.DayForecasts;
            }
        }


        private void LoadWeatherForSelectedLocation()
        {
            var selectedLocation = settingService.Settings.SelectedLocation;

            if (selectedLocation == null) return;

            var lat = (decimal)selectedLocation.Latitude;
            var lon = (decimal)selectedLocation.Longitude;

            currentWeather = weatherStorage.GetWeatherForecast(lat, lon);

            if (currentWeather == null)
            {
                
                var measures = new ForecastMeasuresModel { Temperature = settingService.Settings.Temperature };
                currentWeather = weatherStorage.Get(
                    (float)selectedLocation.Latitude,
                    (float)selectedLocation.Longitude,
                    measures,
                    selectedLocation.Name
                );
            }
        
            ForecastsDays = currentWeather.DayForecasts;
            SelectedDay = ForecastsDays.FirstOrDefault(); 
        }

    }
}
