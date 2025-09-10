using Microsoft.EntityFrameworkCore;
using WeatherAppWPF.Configurations;
using WeatherAppWPF.Models;
using WeatherAppWPF.Services.GeoCoder;
using WeatherAppWPF.Services.OpenMeteo;

namespace WeatherAppWPF.Repository
{
    public class DatabaseContext : DbContext
    {
        public DbSet<GeoLocation> Locations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<FavoriteLocations> FavoriteLocations { get; set; }
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new LocationModelConfiguration());
        
        }
    }
}
