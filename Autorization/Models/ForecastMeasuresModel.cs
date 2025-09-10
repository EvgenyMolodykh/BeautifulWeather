using Microsoft.EntityFrameworkCore;
using WeatherAppWPF.Enums;

namespace WeatherAppWPF.Services.OpenMeteo
{
    [PrimaryKey("Id")]
    public class ForecastMeasuresModel //прогноз погоды температура/напраление ветра/чистота неба
    {
        public Guid Id { get; set; } 
        public TemperatureMeasure Temperature { get; set; } = 0.0f;
        public WeatherCodes weatherCodes { get; set; } = WeatherCodes.ClearSkyDay;
        public WindDirection windDirection { get; set; } = WindDirection.North;

    }
}