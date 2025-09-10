using Microsoft.EntityFrameworkCore;

namespace WeatherAppWPF.Models
{
    [PrimaryKey("Id")]
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsSingIn { get; set; }
        public User(string login, string password, string yandexApiKey)
        {
            Id = Guid.NewGuid();
            Login = login;
            Password = password;
            YandexApiKey = yandexApiKey;
        }

        public User(string login, string password)
        {
            Id = Guid.NewGuid();
            Login = login;
            Password = password;
          
        }
        public string YandexApiKey { get; set; }
    }
}
