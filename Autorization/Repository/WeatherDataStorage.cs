using Microsoft.EntityFrameworkCore;
using WeatherAppWPF.Interfaces;
using WeatherAppWPF.Models;
using WeatherAppWPF.Services.OpenMeteo;

namespace WeatherAppWPF.Repository
{
    public class WeatherDataStorage : IWeatherStorage
    {
        private readonly OpenMeteoProvider openMeteoProvider;
        private readonly DatabaseContext databaseContext;
        public WeatherDataStorage(OpenMeteoProvider openMeteoProvider, DatabaseContext databaseContext)
        {
            this.openMeteoProvider = openMeteoProvider;
            this.databaseContext = databaseContext;
        }

        public WeatherForecast Get(float latitude, float longitude, ForecastMeasuresModel measures, string name)
        {

            decimal lat = Math.Round((decimal)latitude, 2);
            decimal lon = Math.Round((decimal)longitude, 2);

            var existingLocation = databaseContext.WeatherForecasts
                .Include(wf => wf.DayForecasts)
                .ThenInclude(wf => wf.HourlyForecasts)
                .FirstOrDefault(g => g.Location.Latitude == lat && g.Location.Longitude == lon);

            if (existingLocation != null)
            {
                return existingLocation;
            }

            var weatherResponce = openMeteoProvider.GetWeather(latitude, longitude, measures);
            SetLocationName(name, weatherResponce);
            databaseContext.WeatherForecasts.Add(weatherResponce);
            databaseContext.SaveChanges();
            return weatherResponce;
        }

        private void SetLocationName(string name, WeatherForecast weather)
        {
            if (string.IsNullOrEmpty(weather.Location.Name))
            {
                var selectedLocation = databaseContext.Settings
                    .Select(s => s.SelectedLocation)
                    .FirstOrDefault();

                weather.Location.Name = selectedLocation?.Name ?? "Неизвестная локация";
            }
            else
            {
                weather.Location.Name = name;
            }
        }

        public WeatherForecast GetWeatherForecast(decimal latitude, decimal longitude)
        {
            decimal lat = Math.Round((decimal)latitude, 2);
            decimal lon = Math.Round((decimal)longitude, 2);
            return databaseContext.WeatherForecasts
        .Include(w => w.DayForecasts)
        .ThenInclude(d => d.HourlyForecasts)
        .FirstOrDefault(w => w.Location.Latitude == lat && w.Location.Longitude == lon);
        }

    }
}
