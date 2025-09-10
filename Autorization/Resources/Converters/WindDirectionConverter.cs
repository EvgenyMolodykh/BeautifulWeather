using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using WeatherAppWPF.Enums;

namespace WeatherAppWPF.Resources.Converters
{
    public class WindDirectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string recourceName = null;

            switch ((WindDirection)value)
            {
                case WindDirection.North:
                    recourceName = "wind_arrow_north";
                    break;
                case WindDirection.South:
                    recourceName = "wind_arrow_south";
                    break;
                case WindDirection.East:
                    recourceName = "wind_arrow_east";
                    break;
                case WindDirection.West:
                    recourceName = "wind_arrow_west";
                    break;
            }
            if (recourceName == null)
                return null;

            return App.Current.Resources[recourceName] as ControlTemplate;
        
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}