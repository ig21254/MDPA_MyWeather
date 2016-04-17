

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
            set
            {
                currentDate = value;
                RaisePropertyChanged("CurrentDate");
            }

        }

        private string currentLocation;
        public string CurrentLocation
        {
            get { return currentLocation; }
            set
            {
                currentLocation = value;
                RaisePropertyChanged("CurrentLocation");
            }
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

        private string search;
        public string Search
        {
            get { return search; }
            set
            {
                search = value;
                RaisePropertyChanged("Search");
            }
                
        }

        private DelegateCommand searchWeatherByCityName;
        public ICommand SearchWeatherByCityName
        {
            get { return this.searchWeatherByCityName; }
        }

        private IWeatherService WeatherService;
        private IGeolocationService GeolocationService;

        public MainPageVM()
        {
            this.WeatherService = new WeatherServiceImpl();
            this.GeolocationService = new GeolocationServiceImpl();

            InitAttributes();
            SetWeather();
            
            this.searchWeatherByCityName = new DelegateCommand(
                async () =>
                {
                    Weather current = await WeatherService.GetCurrentWeather(Search);
                    List<Weather> forecast = await WeatherService.GetForecastWeather(Search);
                    UpdateWeather(current, forecast);
                });

        }

        private async void SetWeather()
        {
            Geoposition location = await GeolocationService.GetCurrentGeoposition();
            double latitude = location.Coordinate.Latitude;
            double longitude = location.Coordinate.Longitude;
            Weather current = await WeatherService.GetCurrentWeather(latitude, longitude);
            List<Weather> forecast = await WeatherService.GetForecastWeather(latitude, longitude);
            UpdateWeather(current, forecast);
        }

        private void InitAttributes()
        {
            this.CurrentDate = Utils.GetTodayFullDate();
            this.CurrentLocation = "---";
            this.CurrentWeather = new Weather();
            this.CurrentWeather.CityName = "---";
        }

        private void UpdateWeather(Weather current, List<Weather> forecast)
        {
            if (current != null && forecast != null)
            {
                CurrentWeather = current;
                ForecastWeather = forecast;
                Search = "";
            }
            else
            {
                SimpleTextToastManager.Instance.ShowToast("Error", "Couldn't find the location you were looking for.");
            }
        }

    }

}
