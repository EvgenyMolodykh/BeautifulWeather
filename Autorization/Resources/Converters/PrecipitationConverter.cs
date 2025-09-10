using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Windows.Data;
using WeatherAppWPF.Enums;
using WeatherAppWPF.Interfaces;
using WeatherAppWPF.Services.ServiceLocator;

namespace WeatherAppWPF.Resources.Converters
{
    public class PrecipitationConverter : IValueConverter 
    {
        private readonly ISettingService settingsService = ServiceLocator.ServiceProvider.GetService<ISettingService>();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var settings = settingsService.Settings;
            var precipitationMeasure = settings.Precipitation;
            var sediment = (double)value;

            if (precipitationMeasure == Enums.PrecipitationMeasure.Cm)
            {
                sediment = sediment / 10;
            }

            if (precipitationMeasure == Enums.PrecipitationMeasure.Inch)
            {
                sediment =  Math.Round(sediment / 25.4, 1);
            }

            var dimension = App.Current.Resources[precipitationMeasure.ToString()].ToString();

            return sediment + " " + dimension;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
