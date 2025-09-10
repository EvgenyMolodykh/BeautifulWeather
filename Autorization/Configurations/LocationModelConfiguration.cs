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
    internal class LocationModelConfiguration : IEntityTypeConfiguration<LocationModel>
    {
        public void Configure(EntityTypeBuilder<LocationModel> builder)
        {
            builder.Property(x => x.Latitude)
                   .HasPrecision(5, 2);

            builder.Property(x => x.Longitude)
                   .HasPrecision(5, 2);
        }
    }
}
