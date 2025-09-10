using WeatherAppWPF.Interfaces;
using System.IO;
using System.Text.Json;

namespace WeatherAppWPF
{
    public class FileProvider : IFileProvider
    {
        public void Save(object data, string fileName)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string serializedData = JsonSerializer.Serialize(data, options);
            if(File.Exists(fileName))
            {
                File.WriteAllText(fileName, serializedData);
            }
            else
            {
                File.Create(fileName).Close();
                File.WriteAllText(fileName, serializedData);
            }
        }

        public T Load<T>(string fileName)
        {
            if (!File.Exists(fileName)) 
            {
                return default;
            }

            var serializedData = File.ReadAllText(fileName);

            if (string.IsNullOrEmpty(serializedData))
            {
                return default;
            }
            return JsonSerializer.Deserialize<T>(serializedData);
        }
    }
}
