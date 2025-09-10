using WeatherAppWPF.Services.OpenMeteo;

namespace WeatherAppWPF.Interfaces
{
    public interface IWeatherStorage
    {
        public WeatherForecast Get(float latitude, float longitude, ForecastMeasuresModel measures, string name);
        WeatherForecast GetWeatherForecast(decimal latitude, decimal longitude);
    }
}
