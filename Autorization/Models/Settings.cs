using Microsoft.EntityFrameworkCore;
using WeatherAppWPF.Enums;
using WeatherAppWPF.Services.GeoCoder;

namespace WeatherAppWPF.Models
{
    [PrimaryKey("Id")]
    public class Settings
    {
        public Guid Id { get; set; }
        public Cultures Cultures { get; set; } = Cultures.RU;
        public TemperatureMeasure Temperature { get; set; } = TemperatureMeasure.Celsius;
        public PressureMeasure Pressure { get; set; } = PressureMeasure.HPa;
        public PrecipitationMeasure Precipitation { get; set; } = PrecipitationMeasure.Mm;
        public MeasurementWindSpeed MeasurementWindSpeed { get; set; } = MeasurementWindSpeed.Ms;
        public GeoLocation SelectedLocation { get; set; } = new GeoLocation
        {
            Latitude = 55.7558m,
            Longitude = 37.6173m,
            Name = "москва",
            Description = "Россия, Москва"
        };

        public List<FavoriteLocations> FavoriteLocations { get; set; } = new List<FavoriteLocations> { };

        public User CurrentUser { get; set; }
        public Settings()
        {
            
            var secretProvider = new SecretProvider();
            CurrentUser = new User("admin", "admin")
            {
                YandexApiKey = secretProvider.YandexApiKey
            };
        }

    }
}
