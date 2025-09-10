using WeatherAppWPF.Interfaces;
using WeatherAppWPF.Models;

namespace WeatherAppWPF.Repository
{
    public class FaforiteLocationStorage:IFavoriteProvider
    {
        private readonly DatabaseContext databaseContext;
        public FaforiteLocationStorage(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public void AddFavoriteLocations(FavoriteLocations favoriteLocations) 
        {
            var exists = databaseContext.FavoriteLocations.Any(f => f.Name.ToLower() == favoriteLocations.Name.ToLower());
            if (exists)
            {
                return;
            }
            databaseContext.FavoriteLocations.Add(favoriteLocations);
            databaseContext.SaveChanges();
        }
        public List<FavoriteLocations> GetLAllFavoriteLocations() 
        { 
            return databaseContext.FavoriteLocations.ToList();
        }
        public void RemoteLocations(FavoriteLocations favoriteLocations)
        {
            databaseContext.FavoriteLocations.Remove(favoriteLocations);
            databaseContext.SaveChanges();
        }

    }
}
