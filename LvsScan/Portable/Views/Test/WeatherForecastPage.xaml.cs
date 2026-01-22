using LvsScan.Portable.Services;
using LvsScan.Portable.ViewModels.Test;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.Test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherForecastPage : ContentPage
    {
        ServiceAPI serviceAPI;
        public WeatherForecastViewModel ViewModel { get; set; }
        public WeatherForecastPage()
        {
            InitializeComponent();
            this.BindingContext = this.ViewModel = new WeatherForecastViewModel();
            serviceAPI = new ServiceAPI();
            ViewModel.GetForecast();
        }
    }
}