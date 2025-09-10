using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using WeatherAppWPF.Interfaces;
using WeatherAppWPF.Models;

namespace WeatherAppWPF.Services
{
    public class SettingKey
    {
        private const string SettingsFileName = "settingsUserKey.json";
        
        public void SaveKey(User user)

        {

            Dictionary<string, string> users = new Dictionary<string, string>();
            if (File.Exists(SettingsFileName))
            {
                var json = File.ReadAllText(SettingsFileName);
                users = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
            //пользователь --> ключ связка
            users[user.Login] = user.YandexApiKey;
            File.WriteAllText(SettingsFileName, JsonConvert.SerializeObject(users, Formatting.Indented));
        }

        public string LoadKey(User user)
        {
            if (File.Exists(SettingsFileName))
            {
                var users = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                    File.ReadAllText(SettingsFileName)
                );
                if (users.ContainsKey(user.Login))
                    return users[user.Login];
            }
            return null;
        }
    }
}
