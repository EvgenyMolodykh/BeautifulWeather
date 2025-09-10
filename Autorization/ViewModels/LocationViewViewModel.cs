using System.Windows.Input;
using WeatherAppWPF.Interfaces;
using WeatherAppWPF.Models;
using WeatherAppWPF.Repository;
using WeatherAppWPF.Services.GeoCoder;
using static WeatherAppWPF.ViewModels.MainWindowViewModel;

namespace WeatherAppWPF.ViewModels
{
    public class LocationViewViewModel : ViewModelBase
    {
        private readonly GeoCoderService geoCoderService;
        private readonly ISettingService settingService;
        private readonly IFavoriteProvider favoriteProvider;
        private readonly DatabaseContext databaseContext;
        private List<FavoriteLocations> _favoriteLocations;

        public ICommand? FavoriteCommand { get; set; }
        public ICommand? DeleteFavoriteLocationCommand { get; set; }
        public ICommand? SelectedFavoriteLocationCommand { get; set; }
        public LocationViewViewModel(GeoCoderService geoCoderService, ISettingService settingService, IFavoriteProvider favoriteProvider, DatabaseContext databaseContext)
        {
            this.geoCoderService = geoCoderService;
            this.settingService = settingService;
            this.favoriteProvider = favoriteProvider;
            this.databaseContext = databaseContext;
            _favoriteLocations = favoriteProvider.GetLAllFavoriteLocations();
            FavoriteCommand = new RelayCommand(AddFavorite, CanAddFavorite);
            DeleteFavoriteLocationCommand = new RelayCommand(DeleteFavoriteLocation, CanDeleteFavoriteLocation);
            SelectedFavoriteLocationCommand = new RelayCommand(SelectedFavoriteLocation, CanSelectFavoriteLocation);
        }

        private bool CanSelectFavoriteLocation(object arg)
        {
            return true;
        }

        private void SelectedFavoriteLocation(object obj)
        {
           if (obj != null ) 
            {
                
                if (obj is FavoriteLocations favoriteLocation)
                {
                    var location = settingService.Settings.SelectedLocation;
                    location = new GeoLocation
                    {
                        Name = favoriteLocation.Name,
                        Description = favoriteLocation.Description,
                        Latitude = (decimal)favoriteLocation.Latitude,
                        Longitude = (decimal)favoriteLocation.Longitude
                    };
                    SelectedLocation = location;
                    OnPropertyChanged();
                }

            }
            else
            {
                return;
            }
        }

        private bool CanDeleteFavoriteLocation(object arg)
        {
            return true;
        }

        private void DeleteFavoriteLocation(object obj)
        {
            if (obj is FavoriteLocations favoriteLocation)
            {
                favoriteProvider.RemoteLocations(favoriteLocation);
                _favoriteLocations = favoriteProvider.GetLAllFavoriteLocations();
                OnPropertyChanged();
            }
            else { return; }

        }

        public List<FavoriteLocations> FavoriteLocations
        {
            get => _favoriteLocations;
            set
            {
                _favoriteLocations = value;
                OnPropertyChanged();
            }
        }
        private bool CanAddFavorite(object arg)
        {
            return true;
        }

        private void AddFavorite(object obj)
        {
            if (obj is GeoLocation location)
            {
                var favoriteLocation = new FavoriteLocations
                {
                    Name = location.Name,
                    Description = location.Description,
                    Latitude = (float)location.Latitude,
                    Longitude = (float)location.Longitude
                };

                if (_favoriteLocations.Any(f => f.Name == favoriteLocation.Name && f.Latitude == favoriteLocation.Latitude && f.Longitude == favoriteLocation.Longitude))
                {
                    
                    return;
                }

                favoriteProvider.AddFavoriteLocations(favoriteLocation);
                _favoriteLocations = favoriteProvider.GetLAllFavoriteLocations();
                OnPropertyChanged();
            }
        }

        private string locationSearch;
        public string LocationSearch
        {
            get
            {
                return locationSearch;
            }
            set
            {
                locationSearch = value;
                if (string.IsNullOrEmpty(value))
                {
                    SearchResults = null;
                    return;
                }
                Task.Run(async () =>
                {
                    var search = value;
                    await Task.Delay(700);

                    if (search != locationSearch)
                    {
                        return;
                    }
                    SearchResults = geoCoderService.GetLocation(value,databaseContext.Settings.FirstOrDefault().CurrentUser.Login);
                    OnPropertyChanged();
                });

            }

        }

        private List<GeoLocation> searchResults;
        public List<GeoLocation> SearchResults
        {
            get { return searchResults; }
            set
            {
             
                searchResults = value;
                OnPropertyChanged();
            }
        }

        public GeoLocation SelectedLocation
        {
            
            get 
            { 
                return settingService.Settings.SelectedLocation; 
            }
            set
            {
                
                if (value is not null)
                {
                    
                    settingService.Settings.SelectedLocation = value;
                    SearchResults = null!;
                    OnPropertyChanged();
                }
                
            } 

        }

    }
}
