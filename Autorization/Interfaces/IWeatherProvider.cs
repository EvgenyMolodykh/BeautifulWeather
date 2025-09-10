using WeatherAppWPF.Models;

namespace WeatherAppWPF.Interfaces
{
    public interface IWeatherProvider
    {
        List<DayForecastModel> GetAll();
        List<HourlyForecastModel> GetHourlyForecastModels();
    }
}
