﻿
using MDPA_MyWeather.Model;
using MDPA_MyWeather.ViewModel.Base;
using System;
using System.Windows.Input;

namespace MDPA_MyWeather.ViewModel
{
    class MainPageVM : ViewModelBase
    {
        private string currentDate;
        public string CurrentDate
        {
            get { return currentDate; }

        }

        private string currentLocation;
        public string CurrentLocation
        {
            get { return currentLocation; }
            set { currentLocation = value; }
        }

        private DelegateCommand getWeather;
        public ICommand GetWeather
        {
            get { return this.getWeather; }
        }

        private WeatherService weatherService;

        public MainPageVM()
        {
            this.currentDate = DateTime.Now.ToString("dddd dd MMMM yyyy");
            this.currentLocation = "Barcelona";
            this.weatherService = new WeatherService();
            this.getWeather = new DelegateCommand(
                async () =>
                {
                    await weatherService.getCurrentWeather(41.390205, 2.154007);
                });
        }
    }
}