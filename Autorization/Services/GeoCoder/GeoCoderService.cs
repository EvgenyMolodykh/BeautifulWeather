using Microsoft.EntityFrameworkCore.Metadata;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using WeatherAppWPF.Repository;

namespace WeatherAppWPF.Services.GeoCoder
{
    public class GeoCoderService
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly DatabaseContext databaseContext;
        private readonly SettingKey settingKey;

        public GeoCoderService(DatabaseContext databaseContext, SettingKey settingKey)
        {
            this.databaseContext = databaseContext;
            this.settingKey = settingKey;
        }
        public List<GeoLocation> GetLocation(string place, string login)
        {

            var user = databaseContext.Users.FirstOrDefault(u => u.Login == login);
            var extistingLocations = databaseContext.Locations.Where(x => x.Name.ToLower().Contains(place.ToLower())).ToList();

            if (extistingLocations.Count > 0)
            {
                return extistingLocations;
            }
            string url = $"https://geocode-maps.yandex.ru/v1?apikey={settingKey.LoadKey(user)}&geocode={place}&lang=ru_RU&format=json";
            var response = httpClient.GetFromJsonAsync<ApiResponce>(url).Result;

            var geoLocations = ToGeoLocation(response);

            foreach (var geoLocation in geoLocations)
            {
                databaseContext.Locations.Add(geoLocation);
            }
            databaseContext.SaveChanges();
            return geoLocations;
        }

        private List<GeoLocation> ToGeoLocation(ApiResponce? response)
        {
            var locations = new List<GeoLocation>();
            foreach (var item in response.response.GeoObjectCollection.featureMember)
            {
                var location = new GeoLocation
                {
                    Name = item.GeoObject.name.ToLower(),
                    Description = item.GeoObject.description
                };

                var points = item.GeoObject.Point.pos.Split(' ');
                location.Latitude = (decimal)Math.Round(float.Parse(points[1], CultureInfo.InvariantCulture), 2);
                location.Longitude = (decimal)Math.Round(float.Parse(points[0], CultureInfo.InvariantCulture.NumberFormat), 2);

                locations.Add(location);
            }

            return locations;
        }

        public bool TestApiKey(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                return false;

            string url = $"https://geocode-maps.yandex.ru/1.x/?apikey={apiKey}&geocode=Москва&format=json&lang=ru_RU";

            try
            {
                using var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(10);

                var response = client.GetAsync(url).GetAwaiter().GetResult();

                if (!response.IsSuccessStatusCode)
                    return false;

                var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                if (!root.TryGetProperty("response", out var responseElement))
                    return false;

                if (!responseElement.TryGetProperty("GeoObjectCollection", out var collectionElement))
                    return false;

                if (!collectionElement.TryGetProperty("featureMember", out var featureMemberElement))
                    return false;

                return featureMemberElement.GetArrayLength() > 0;
            }
            catch (TaskCanceledException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
