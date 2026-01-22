using LvsScan.Portable.Models;
using LvsScan.Portable.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LvsScan.Portable.ViewModels.Test
{
    public class WeatherForecastViewModel : BaseViewModel
    {
        public ServiceAPI serviceAPI;
        public WeatherForecastViewModel()
        {
            Title = "WeatherForecast";
            serviceAPI = new ServiceAPI();
        }

        public ObservableCollection<WeatherForecast> forecasts;
        public ObservableCollection<WeatherForecast> Forecasts
        {
            get
            {
                if (forecasts == null)
                {
                    Forecasts = new ObservableCollection<WeatherForecast>();
                }
                return forecasts;
            }
            set
            {
                forecasts = value;
                OnPropertyChanged();
            }
        }

        public async void GetForecast()
        {
            List<WeatherForecast> locs = await serviceAPI.GetWeatherForecastList();
            Forecasts = new ObservableCollection<WeatherForecast>(locs);
        }
    }
}
