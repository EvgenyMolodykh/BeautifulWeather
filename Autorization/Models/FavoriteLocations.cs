using Microsoft.EntityFrameworkCore;

namespace WeatherAppWPF.Models
{
    [PrimaryKey("Id")]
    public class FavoriteLocations
    {
        public Guid Id { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
