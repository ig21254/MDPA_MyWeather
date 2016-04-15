

namespace MDPA_MyWeather.ViewModel
{

    using Model;
    using Model.Services;
    using Base;
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using Windows.Devices.Geolocation;

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

        private IWeatherService WeatherService;
        private IGeolocationService GeolocationService;

        public MainPageVM()
        {
            this.WeatherService = new WeatherServiceImpl();
            this.GeolocationService = new GeolocationServiceImpl();

            initAttributes();
            setWeather();
            
            
            /*this.getWeather = new DelegateCommand(
                async () =>
                {
                    await weatherService.getCurrentWeather(41.390205, 2.154007);
                });*/

        }

        private async void setWeather()
        {
            Geoposition location = await GeolocationService.GetCurrentGeoposition();
            double latitude = location.Coordinate.Latitude;
            double longitude = location.Coordinate.Longitude;
            this.CurrentWeather = await WeatherService.GetCurrentWeather(latitude, longitude);
            this.ForecastWeather = await WeatherService.GetForecastWeather(latitude, longitude);
        }

        private void initAttributes()
        {
            this.CurrentDate = Utils.GetTodayFullDate();
            this.CurrentLocation = "---";
            this.CurrentWeather = new Weather();
            this.CurrentWeather.CityName = "---";
        }
    }
}
