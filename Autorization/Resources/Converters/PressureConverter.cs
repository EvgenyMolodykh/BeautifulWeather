using Microsoft.Extensions.DependencyInjection;
using System.Windows.Data;
using WeatherAppWPF.Interfaces;
using WeatherAppWPF.Services.ServiceLocator;

namespace WeatherAppWPF.Resources.Converters
{
    public class PressureConverter: IValueConverter
    {
        private readonly ISettingService settingsService = ServiceLocator.ServiceProvider.GetService<ISettingService>();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var settings = settingsService.Settings;
            var pressureMeasure = settings.Pressure;
            var pressure = (double)value;
          
            if (pressureMeasure == Enums.PressureMeasure.HPa)
            {
                pressure = pressure / 1.33322;
            }
            var dimension = App.Current.Resources[pressureMeasure.ToString()].ToString();


            return pressure + " " + dimension;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    
}
