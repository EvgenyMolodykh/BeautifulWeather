using Microsoft.Extensions.DependencyInjection;
using System.Windows.Data;
using WeatherAppWPF.Interfaces;
using WeatherAppWPF.Services.ServiceLocator;

namespace WeatherAppWPF.Resources.Converters
{
    public class TemperatureConverter : IValueConverter
    {
        private readonly ISettingService settingsService = ServiceLocator.ServiceProvider.GetService<ISettingService>();
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var settings = settingsService.Settings;
            var temperatureMeasure = settings.Temperature;
            var temperature = (float)value;
            if (temperatureMeasure == Enums.TemperatureMeasure.Fahrenheit)
            {
                temperature = temperature * 9 / 5 + 32;
            }
            var dimension = App.Current.Resources[temperatureMeasure.ToString()].ToString();


            return temperature + dimension; 
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException("Обратное преобразование не реализовано для преобразователя температуры");
        }
    }
}
