using Microsoft.EntityFrameworkCore;
using WeatherAppWPF.Enums;

namespace WeatherAppWPF.Models
{
    [PrimaryKey("Date","Location")]
    public class DayForecastModel
    {
        public DateTime Date { get; set; }
        public float MaxTemperature { get; set; }
        public float MinTemperature { get; set; }
        public string Location { get; set; }
        public WeatherCodes Wheater { get; set; }
        public DayOfWeek WeekDay { get; set; }
        public double Pressure { get; set; }
        public double WindSpeed { get; set; }
        public int WindDirection { get; set; }
        public List<HourlyForecastModel> HourlyForecasts { get; set; } = new List<HourlyForecastModel>();

    }
}
