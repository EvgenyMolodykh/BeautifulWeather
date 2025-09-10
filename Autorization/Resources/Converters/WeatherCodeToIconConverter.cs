using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using WeatherAppWPF.Enums;

namespace WeatherAppWPF.Resources.Converters
{
    public class WeatherCodeToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string? recourceName = null;
   
            switch ((WeatherCodes)value)
            {
                case WeatherCodes.ClearSkyDay:
                    recourceName = "clear-day"; 
                    break;
                case WeatherCodes.ClearSkyNight:
                    recourceName = "clear-night";
                    break;
                case WeatherCodes.PartlyCloudy:
                    recourceName = "party-cloudly-day"; 
                    break;
                case WeatherCodes.Overcast:
                    recourceName = "overcast";
                    break;
                case WeatherCodes.Thunderstorm:
                    recourceName = "thunderstorm"; 
                    break;
                case WeatherCodes.SnowGrains:
                    recourceName = "slight-snowfall";
                    break;
                case WeatherCodes.HeavyRain:
                    recourceName = "rain";
                    break;
                case WeatherCodes.Windy:
                    recourceName = "wind";
                    break;
                case WeatherCodes.LightDrizzle:
                    recourceName = "drizzle";
                    break;
                case WeatherCodes.Showern:
                    recourceName = "shower";
                    break;
                case WeatherCodes.ModerateRain:
                    recourceName = "cloudly";
                    break;
                case WeatherCodes.Fog:
                    recourceName = "fog";
                    break;  
                case WeatherCodes.LightFreezingDrizzle:
                    recourceName = "freezing-drizzle";
                    break;
                case WeatherCodes.HeavySnowfall:
                    recourceName = "hard-snowfall";
                    break;
                case WeatherCodes.ModerateSnowfall:
                    recourceName = "moderate-snowfall";
                    break;
                case WeatherCodes.SlightSnowfall:
                    recourceName = "slight-snowfall";
                    break;
                case WeatherCodes.LightFreezingRain:
                    recourceName = "party-cloudly-night";
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
