using System.ComponentModel;

namespace WeatherAppWPF.Enums
{
    public enum WeatherCodes
    {
        [Description("Ясный день")]
        ClearSkyDay = 0,
        [Description("Туман")]
        Fog = 4,
        [Description("Ливень")]
        Showern = 5,
        [Description("Сильный дождь")]
        HeavyRain = 6,
        [Description("Сильный снегопад")]
        HeavySnowfall = 7,
        [Description("Легкий дождь")]
        LightDrizzle = 9,
        [Description("Легкий моросящий дождь")]
        LightFreezingDrizzle = 10,
        [Description("Небольшой ледяной дождь")]
        LightFreezingRain = 11,
        [Description("Умеренный дождь")]
        ModerateRain = 14,
        [Description("Умеренный снегопад")]
        ModerateSnowfall = 16,
        [Description("Пасмурно")]
        Overcast = 18,
        [Description("Переменная облачность")]
        PartlyCloudy = 19,
        [Description("Небольшой снегопад")]
        SlightSnowfall = 22,
        [Description("Снегопад")]
        SnowGrains = 24,
        [Description("Гроза")]
        Thunderstorm = 25,
        [Description("Ветренно")]
        Windy = 29,
        [Description("Ясная ночь")]
        ClearSkyNight = 30
    }
}