using WeatherAppWPF.Models;

namespace WeatherAppWPF.Interfaces
{
    public interface IUserProvider
    {
        public void Add(User user);

        public  User? GetSingInUser();

        public  void SingOut();
       
        public List<User> GetAllUsers();

        public User GetUser(User user);

        public void Update(User user);
    }
}
