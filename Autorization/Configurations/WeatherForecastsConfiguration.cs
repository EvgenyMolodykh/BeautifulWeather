using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAppWPF.Services.OpenMeteo;

namespace WeatherAppWPF.Configurations
{
    public class WeatherForecastsConfiguration : IEntityTypeConfiguration<WeatherForecast>
    {
        public void Configure(EntityTypeBuilder<WeatherForecast> builder)
        {
            builder.OwnsOne(w => w.Location, location =>
            {
                location.Property(l => l.Latitude)
                    .HasPrecision(5, 2); 

                location.Property(l => l.Longitude)
                    .HasPrecision(5, 2); 
            });
        }
    }
}
