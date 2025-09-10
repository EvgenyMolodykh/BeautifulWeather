using Microsoft.EntityFrameworkCore;

namespace WeatherAppWPF.Services.OpenMeteo
{
    [PrimaryKey("Latitude", "Longitude")]
    public class LocationModel
    {
        public decimal Latitude { get; set; } = 0.0m;
        public decimal Longitude { get; set; } = 0.0m;
        public string Name { get; set; } = string.Empty;
    } 
}