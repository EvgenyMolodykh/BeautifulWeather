using System.Globalization;
using System.Windows;
using WeatherAppWPF.Enums;
using WeatherAppWPF.Interfaces;

namespace WeatherAppWPF.Services.Localization
{
    public class LocalizationService: ILocalizationService
    {
        private Dictionary<Cultures, ResourceDictionary> cultureDictionary = new Dictionary<Cultures, ResourceDictionary>()
        {
            { Cultures.RU, new ResourceDictionary() { Source = new Uri("pack://application:,,,/Resources/Localization/Language.ru-RU.xaml", UriKind.RelativeOrAbsolute) } },
            { Cultures.EN, new ResourceDictionary() { Source = new Uri("pack://application:,,,/Resources/Localization/Language.en-US.xaml", UriKind.RelativeOrAbsolute) }}
        };
        public void SetCulture(Cultures culture)
        {
           App.Current.Resources.MergedDictionaries.Add(cultureDictionary[culture]);
           CultureInfo.CurrentCulture = new CultureInfo(App.Current.Resources["lang"].ToString());
        }
    }
}
