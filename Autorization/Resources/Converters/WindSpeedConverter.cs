using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Windows.Data;
using WeatherAppWPF.Enums;
using WeatherAppWPF.Interfaces;
using WeatherAppWPF.Services.ServiceLocator;

namespace WeatherAppWPF.Resources.Converters
{
    internal class WindSpeedConverter : IValueConverter
    {
        private readonly ISettingService settingsService = ServiceLocator.ServiceProvider.GetService<ISettingService>();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var settings = settingsService.Settings;
            var windSpeedMeasure = settings.MeasurementWindSpeed;
            var windSpeed = (double)value;

            if (windSpeedMeasure == Enums.MeasurementWindSpeed.Kmh)
            {
                windSpeed = windSpeed * 3.6;
            }
            if (windSpeedMeasure == Enums.MeasurementWindSpeed.Mph)
            {
                windSpeed = windSpeed * 2.23694;
            }
            if (windSpeedMeasure == Enums.MeasurementWindSpeed.Kn)
            {
                windSpeed = windSpeed * 1.94384;
            }
            var dimension = App.Current.Resources[windSpeedMeasure.ToString()].ToString();


            return Math.Round(windSpeed,1) + " " + dimension;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
