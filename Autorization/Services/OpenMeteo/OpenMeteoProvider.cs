using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using WeatherAppWPF.Enums;
using WeatherAppWPF.Interfaces;
using WeatherAppWPF.Models;

namespace WeatherAppWPF.Services.OpenMeteo
{
    public class OpenMeteoProvider
    {
        private readonly ISettingService settings;
        public OpenMeteoProvider(ISettingService settingService)
        {
            
            this.settings = settingService;
        }

        private readonly HttpClient httpClient = new HttpClient()
        {
           BaseAddress = new Uri("https://api.open-meteo.com/v1/forecast/") 
        };
        

        public WeatherForecast GetWeather(float latitude, float longitude, ForecastMeasuresModel measures)
        {
            var url = new StringBuilder();
            url.Append("?latitude=" + latitude.ToString(CultureInfo.InvariantCulture));
            url.Append("&longitude=" + longitude.ToString(CultureInfo.InvariantCulture));
            url.Append("&temperature_unit=" + measures.Temperature.ToString().ToLower());
            url.Append("&timezone=auto");
            url.Append("&past_days=2");
            url.Append("&daily=temperature_2m_max,temperature_2m_min,precipitation_sum,rain_sum,showers_sum,snowfall_sum,precipitation_hours,weathercode,sunrise,sunset,windspeed_10m_max,windgusts_10m_max,winddirection_10m_dominant");
            url.Append("&hourly=temperature_2m,relativehumidity_2m,apparent_temperature,surface_pressure,windspeed_10m,winddirection_10m,weathercode");

            DailyApiResponse response;
            try
            {
                response = httpClient.GetFromJsonAsync<DailyApiResponse>(url.ToString()).Result;
            }
            catch
            {
                return null!;
            }

            return ToWeatherForecastModel(response!, measures, latitude, longitude);
        }

        private WeatherForecast ToWeatherForecastModel(DailyApiResponse apiModel, ForecastMeasuresModel measures, float latitude, float longitude)
        {
            WeatherForecast weatherForecast = new();
            weatherForecast.Measures = new ForecastMeasuresModel()
            {
                Temperature = measures.Temperature,
            };

            int hoursCounter = 0;
            for (int i = 0; i < apiModel?.Daily?.Time?.Count; i++)
            {
                DayForecastModel day = new()
                {
                    Date = apiModel.Daily.Time[i],
                    MaxTemperature = apiModel.Daily.Temperature_2m_max[i],
                    MinTemperature = apiModel.Daily.Temperature_2m_min[i],
                    Location = settings.Settings.SelectedLocation.Name,
                    Wheater = (WeatherCodes)apiModel.Daily.Weathercode[i],

                    WindDirection = apiModel.Daily.Winddirection_10m_dominant[i],
                    WindSpeed = apiModel.Daily.Windspeed_10m_max[i],
                    WeekDay = apiModel.Daily.Time[i].DayOfWeek
                };
                float pressure = 0;
                for (int j = hoursCounter; j < hoursCounter + 24; j++)
                {
                    HourlyForecastModel hour = new()
                    {
                        ApparentTemperature = apiModel.Hourly.Apparent_temperature[j],
                        RelativeHumidity = apiModel.Hourly.Relativehumidity_2m[j],
                        Temperature = apiModel.Hourly.Temperature_2m[j],
                        Time = apiModel.Hourly.Time[j],
                        Weather = (WeatherCodes)apiModel.Hourly.Weathercode[j],
                        WindDirection = apiModel.Hourly.Winddirection_10m[j],
                        WindSpeed = apiModel.Hourly.Windspeed_10m[j]
                    };
                    day.HourlyForecasts.Add(hour);
                }

                day.Pressure = pressure / 24;
                hoursCounter += 24;
                weatherForecast.DayForecasts.Add(day);

            }

            weatherForecast.StartDate = apiModel.Daily.Time.First();
            weatherForecast.EndDate = apiModel.Daily.Time.Last();

            weatherForecast.Location = new LocationModel
            {
                Latitude = (decimal)latitude,
                Longitude = (decimal)longitude
            };

            return weatherForecast;
        }
    }
}
