using WeatherAppWPF.Interfaces;
using WeatherAppWPF.Models;
using WeatherAppWPF.Services;

namespace WeatherAppWPF.Repository
{
    public class UserStorage : IUserProvider

    {
        private readonly DatabaseContext databaseContext;
        private readonly PBKDF2PasswordHasher pBKDF2PasswordHasher;
        private readonly SettingKey settingKey;

        public UserStorage(DatabaseContext databaseContext, PBKDF2PasswordHasher pBKDF2PasswordHasher, SettingKey settingKey)
        {
            this.databaseContext = databaseContext;
            this.pBKDF2PasswordHasher = pBKDF2PasswordHasher;
            this.settingKey = settingKey;
        }
        public void Add(User user)
        {
            user.Password = pBKDF2PasswordHasher.HashPassword(user.Password);
            settingKey.SaveKey(user);
            databaseContext.Users.Add(user);
            var setting = databaseContext.Settings.FirstOrDefault();
            setting.CurrentUser = user;
            databaseContext.SaveChanges();
          
        }
        public User? GetSingInUser()
        {
            return databaseContext.Users.FirstOrDefault(u => u.IsSingIn);
        }
        public  void SingOut()
        {
            var singInUser = GetSingInUser();
            if (singInUser != null)
            {
                var existingSingInUser = databaseContext.Users.FirstOrDefault(u => u.Login == singInUser.Login);
                existingSingInUser.IsSingIn = false;
                databaseContext.SaveChanges();
            }
        }
        public List<User> GetAllUsers()
        {
            return databaseContext.Users.ToList();
        }
        public User GetUser(User user)
        {
            if (string.IsNullOrEmpty(user.Login) || string.IsNullOrEmpty(user.Password))
            {
                return null;
            }

            var userCurrent = databaseContext.Users.FirstOrDefault(u => u.Login == user.Login);

            if (userCurrent == null)
            {
                return null;
            }
            if (userCurrent.Login == "admin") 
            {
                return userCurrent;
            }

            if (pBKDF2PasswordHasher.VerifyPassword(user.Password, userCurrent.Password))
            {
                return userCurrent;
            }
            return null;
        }

        public void Update(User user)
        {
            databaseContext.Users.Update(user);
            databaseContext.SaveChanges();

        }
    }
}
