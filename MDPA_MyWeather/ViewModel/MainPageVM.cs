
using MDPA_MyWeather.Model;
using MDPA_MyWeather.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Windows.UI.Xaml.Media.Imaging;

namespace MDPA_MyWeather.ViewModel
{
    class MainPageVM : ViewModelBase
    {
        private string currentDate;
        public string CurrentDate
        {
            get { return currentDate; }
            set { currentDate = value; }

        }

        private string currentLocation;
        public string CurrentLocation
        {
            get { return currentLocation; }
            set { currentLocation = value; }
        }

        private Weather currentWeather;
        public Weather CurrentWeather
        {
            get { return currentWeather; }
            set
            {
                currentWeather = value;
                RaisePropertyChanged("CurrentWeather");
            }
        }

        private List<Weather> forecastWeather;
        public List<Weather> ForecastWeather
        {
            get { return forecastWeather; }
            set
            {
                forecastWeather = value;
                RaisePropertyChanged("ForecastWeather");
            }
        }

        private DelegateCommand getWeather;
        public ICommand GetWeather
        {
            get { return this.getWeather; }
        }

        private WeatherService weatherService;

        public MainPageVM()
        {
            this.weatherService = new WeatherService();

            setWeather();

            this.CurrentDate = DateTime.Now.ToString("dddd dd MMMM yyyy");
            this.CurrentLocation = "Barcelona";
            this.getWeather = new DelegateCommand(
                async () =>
                {
                    await weatherService.getCurrentWeather(41.390205, 2.154007);
                });

        }

        private async void setWeather()
        {
            this.CurrentWeather = await weatherService.getCurrentWeather(41.390205, 2.154007);
            this.ForecastWeather = await weatherService.getForecastWeather(41.390205, 2.154007);
        }

    }
}
