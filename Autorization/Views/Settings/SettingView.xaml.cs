using System.Windows.Controls;
using WeatherAppWPF.Enums;

namespace WeatherAppWPF.Views.Settings
{

    public partial class SettingView : UserControl
    {
        public SettingView()
        {
            InitializeComponent();
            //Pressure_Combobox.ItemsSource = Enum.GetValues(typeof(PressureMeasure)).Cast<PressureMeasure>();
            Precipitation_Combobox.ItemsSource = Enum.GetValues(typeof(PrecipitationMeasure)).Cast<PrecipitationMeasure>();
            WerterSpeed_Combobox.ItemsSource = Enum.GetValues(typeof(MeasurementWindSpeed)).Cast<MeasurementWindSpeed>();
        }
    }
}
