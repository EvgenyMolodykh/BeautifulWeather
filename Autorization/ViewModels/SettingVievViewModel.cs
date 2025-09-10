using System.Text.RegularExpressions;
using WeatherAppWPF.Enums;
using WeatherAppWPF.Interfaces;

namespace WeatherAppWPF.ViewModels
{
    class SettingVievViewModel : ViewModelBase

    {
        
        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private TemperatureMeasure temperature;
        private Cultures cultures;
        private PressureMeasure pressureMeasure;

        public SettingVievViewModel(ILocalizationService localizationService, ISettingService settingService)
        {
            _localizationService = localizationService;
            _settingService = settingService;

            InitParameters(settingService);
        }

        private void InitParameters(ISettingService settingService)
        {
            var settings = settingService.Settings;
            cultures = settings.Cultures;
            pressureMeasure = settings.Pressure;
            temperature = settings.Temperature;
            Precipitation = settings.Precipitation;
            TemperatureController = (temperature == TemperatureMeasure.Celsius);
            LanguageController = (cultures == Cultures.RU);
            PressureController = (pressureMeasure == PressureMeasure.mmHg);
        }
        private bool languageController;
        public bool LanguageController
        {
            get { return languageController; }
            set
            {
                var settings = _settingService.Settings;
                if (value)
                {
                    settings.Cultures = Cultures.RU;
                }
                else
                {
                   settings.Cultures = Cultures.EN;
                }
                _localizationService.SetCulture(settings.Cultures);
                Set(ref languageController, value, nameof(LanguageController));
            }

        }


        private bool temperatureController;
        public bool TemperatureController
        {
            get { return temperatureController; }
            set
            {
                var settings = _settingService.Settings;
                if (value) 
                {
                    settings.Temperature = TemperatureMeasure.Celsius;
                }
                else
                {
                    settings.Temperature = TemperatureMeasure.Fahrenheit;
                }
                
                Set(ref temperatureController, value, nameof(TemperatureController));
            }

        }

        private bool Set(ref bool field, bool value, string propertyName)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        //private PressureMeasure pressure;
        //public PressureMeasure Pressure
        //{
        //    get { return pressure; }
        //    set
        //    {
        //        pressure = value;
        //        OnPropertyChanged();
        //        var settings = _settingService.Settings;
        //        if (settings.Pressure != value)
        //        {
        //            settings.Pressure = value;
        //        }
        //    }

        //}

        private bool pressureController;
        public bool PressureController
        {
            get { return pressureController; }
            set
            {
                if (Set(ref pressureController, value, nameof(PressureController)))
                {
                    var settings = _settingService.Settings;
                    settings.Pressure = value ? PressureMeasure.mmHg : PressureMeasure.HPa;
                }
            }
        }

        private PrecipitationMeasure precipitation;
        public PrecipitationMeasure Precipitation
        {
            get { return precipitation; }
            set
            {
                precipitation = value;
                OnPropertyChanged();
                var settings = _settingService.Settings;
                if (settings.Precipitation != value)
                {
                    settings.Precipitation = value;
                }
            }

        }

        private MeasurementWindSpeed windSpeed;
        public MeasurementWindSpeed WindSpeed
        {
            get { return windSpeed; }
            set
            {
                windSpeed = value;
                OnPropertyChanged();
                var settings = _settingService.Settings;
                if (settings.MeasurementWindSpeed != value)
                {
                    settings.MeasurementWindSpeed = value;
                }
            }

        }
    }
}
