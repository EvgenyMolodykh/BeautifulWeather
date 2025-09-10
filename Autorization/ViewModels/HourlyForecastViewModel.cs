using System.ComponentModel;

namespace WeatherAppWPF.ViewModels
{
    class HourlyForecastViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private HourlyForecastViewModel hourlyForecastViewModel;

        private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
