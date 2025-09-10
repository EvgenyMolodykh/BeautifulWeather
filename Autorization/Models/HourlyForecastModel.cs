using Microsoft.EntityFrameworkCore;
using WeatherAppWPF.Enums;

namespace WeatherAppWPF.Models
{
    [PrimaryKey("Id")]
    public class HourlyForecastModel
    {
        public Guid Id { get; set; } 
        public DateTime Time { get; set; }
        public float Temperature { get; set; }
        public float ApparentTemperature { get; set; }
        public float RelativeHumidity { get; set; }
        public float SurfasePressure { get; set; }
        public float WindSpeed { get; set; }
        public int WindDirection { get; set; }//снести enum
        public WeatherCodes Weather { get; set; }
    }
}
