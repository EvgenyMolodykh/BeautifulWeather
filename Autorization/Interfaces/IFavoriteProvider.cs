using WeatherAppWPF.Models;

namespace WeatherAppWPF.Repository
{
    public interface IFavoriteProvider
    {
        public void AddFavoriteLocations(FavoriteLocations favoriteLocations);
        public List<FavoriteLocations> GetLAllFavoriteLocations();
        public void RemoteLocations(FavoriteLocations favoriteLocations);
    }
}