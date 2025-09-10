using WeatherAppWPF.Interfaces;
using WeatherAppWPF.Repository;
using WeatherAppWPF.Models;
using Microsoft.EntityFrameworkCore;

namespace WeatherAppWPF.Services.SettingsService
{
    public class SettingService : ISettingService
    {
        public Settings Settings { get; }
        private readonly DatabaseContext databaseContext;
    

        public SettingService(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
            Settings = Load();
        }
        public void Save()
        {
            var existingSettings = databaseContext.Settings.Include(s => s.SelectedLocation).FirstOrDefault();

            if (existingSettings != null)
            {
               
                existingSettings.Cultures = Settings.Cultures;
                existingSettings.Temperature = Settings.Temperature;
                existingSettings.Pressure = Settings.Pressure;
                existingSettings.MeasurementWindSpeed = Settings.MeasurementWindSpeed;

                if (Settings.SelectedLocation != null)
                { 
                    var existingLocation = databaseContext.Locations.FirstOrDefault(l => l.Latitude == Settings.SelectedLocation.Latitude && l.Longitude == Settings.SelectedLocation.Longitude);

                    if (existingLocation != null)
                    {
                        existingSettings.SelectedLocation = existingLocation;
                    }
                    else
                    {
                      
                        databaseContext.Locations.Add(Settings.SelectedLocation);
                        databaseContext.SaveChanges();
                        existingSettings.SelectedLocation = Settings.SelectedLocation;
                    }
                }
                databaseContext.SaveChanges(); 
            }
        }

        private Settings Load()
        {
            var settings = databaseContext.Settings
                .Include(s=>s.FavoriteLocations)
                .Include(s=>s.SelectedLocation)
                .FirstOrDefault();
            if (settings == null)
            {
                settings = new Settings();
                databaseContext.Settings.Add(settings);
                databaseContext.SaveChanges();
            }
            return settings;
        }
    }
}
