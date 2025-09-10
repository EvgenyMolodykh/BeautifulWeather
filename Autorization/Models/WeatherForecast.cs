using Microsoft.EntityFrameworkCore;
using WeatherAppWPF.Models;

namespace WeatherAppWPF.Services.OpenMeteo
{
    [PrimaryKey("Id")]
    public class WeatherForecast
    {
        public Guid Id { get; set; } 
        public LocationModel Location { get; set; } = new();
        public ForecastMeasuresModel Measures { get; set; } = new();
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<DayForecastModel> DayForecasts { get; set; } = new();
    }
}
