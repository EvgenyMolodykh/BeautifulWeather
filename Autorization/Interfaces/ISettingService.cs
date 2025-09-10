using WeatherAppWPF.Models;

namespace WeatherAppWPF.Interfaces
{
    public interface ISettingService
    {
        Settings Settings { get; }
        public void Save();
    }
}
