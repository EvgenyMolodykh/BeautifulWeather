using Microsoft.EntityFrameworkCore;

namespace WeatherAppWPF.Services.GeoCoder
{
    [PrimaryKey("Latitude","Longitude")]
    public class GeoLocation
    {
        private decimal _latitude;
        private decimal _longitude;

        public decimal Latitude
        {
            get => _latitude;
            set => _latitude = decimal.Round(value, 2);
        }

        public decimal Longitude
        {
            get => _longitude;
            set => _longitude = decimal.Round(value, 2);
        }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}