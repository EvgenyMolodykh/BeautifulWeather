using WeatherAppWPF.Enums;

namespace WeatherAppWPF.Interfaces
{
    public interface ILocalizationService
    {
        public void SetCulture(Cultures culture);
    }
}
