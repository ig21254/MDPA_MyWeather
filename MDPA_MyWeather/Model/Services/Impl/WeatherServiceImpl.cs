
namespace MDPA_MyWeather.Model
{
    using System;
    using Windows.Web.Http;
    using Windows.Data.Json;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Services;

    class WeatherServiceImpl : IWeatherService
    {
        private const string API_KEY = "appid=30784d457d092ececdc59b00384ca2df";
        private const string URL_BASE = "http://api.openweathermap.org/data/2.5/";
        /* api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon} */
        private const string CURRENT_WEATHER = "weather?";
        /* api.openweathermap.org/data/2.5/forecast/daily?lat={lat}&lon={lon}&cnt={cnt} */
        private const string FORECAST = "forecast/daily?";

        private HttpClient client;

        public WeatherServiceImpl()
        {
            client = new HttpClient();
        }

        private async Task<string> SendHttpRequest(String uri)
        {
            try
            {
                Uri myUri = new Uri(uri);
                HttpResponseMessage message;
                message = await client.GetAsync(myUri);
                if (message.StatusCode != HttpStatusCode.Ok)
                {
                    return null;
                }

                return await message.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private Weather ParseCurrentWeatherResponse(string content)
        {
            Weather current = new Weather();
            var root = JsonValue.Parse(content).GetObject();

            if (!root.GetNamedValue("cod").Stringify().Equals("200"))
            {
                return null;
            }

            current.CityName = root.GetNamedValue("name").GetString();

            current.Date = (long)root.GetNamedValue("dt").GetNumber();

            var weather = root.GetNamedValue("weather").GetArray().GetObjectAt(0);
            current.WeatherId = (int)weather.GetNamedNumber("id");
            current.WeatherDescription = weather.GetNamedString("description");

            var main = root.GetNamedValue("main").GetObject();
            current.Temp = Convert.ToInt32(main.GetNamedNumber("temp"));
            current.Pressure = (int)main.GetNamedNumber("pressure");
            current.Humidity = (int)main.GetNamedNumber("humidity");
            current.TempMin = Convert.ToInt32(main.GetNamedNumber("temp_min"));
            current.TempMax = Convert.ToInt32(main.GetNamedNumber("temp_max"));

            var wind = root.GetNamedValue("wind").GetObject();
            current.WindSpeed = wind.GetNamedNumber("speed");

            var clouds = root.GetNamedObject("clouds");
            current.Cloudiness = clouds.GetNamedNumber("all");

            string imgName = convertWeatherId(current.WeatherId);
            current.Icon = "ms-appx://MDPA_MyWeather/Assets/WeatherIcons/" + imgName + ".png";

            return current;
        }

        public async Task<Weather> GetCurrentWeather(double latitude, double longitude)
        {
            String uri = URL_BASE + CURRENT_WEATHER + "lat=" + latitude + "&lon=" + longitude + "&" + API_KEY + "&units=metric";
            string content = await SendHttpRequest(uri);
            if (content == null) return null;
            return ParseCurrentWeatherResponse(content);
        }

        public async Task<Weather> GetCurrentWeather(string cityName)
        {
            String uri = URL_BASE + CURRENT_WEATHER + "q=" + cityName + "&" + API_KEY + "&units=metric";
            string content = await SendHttpRequest(uri);
            if (content == null) return null;
            return ParseCurrentWeatherResponse(content);
        }

        private List<Weather> parseForecastWeatherResponse(string content)
        {
            List<Weather> forecast = new List<Weather>();

            var root = JsonValue.Parse(content).GetObject();

            if (!root.GetNamedValue("cod").GetString().Equals("200"))
            {
                return null;
            }

            var cityName = root.GetNamedObject("city").GetNamedString("name");

            var list = root.GetNamedArray("list");
            foreach (var day in list)
            {
                Weather weather = new Weather();

                weather.CityName = cityName;
                weather.Date = (long)day.GetObject().GetNamedNumber("dt");

                var temp = day.GetObject().GetNamedObject("temp");
                weather.Temp = Convert.ToInt32(temp.GetNamedNumber("day"));
                weather.TempMax = Convert.ToInt32(temp.GetNamedNumber("max"));
                weather.TempMin = Convert.ToInt32(temp.GetNamedNumber("min"));

                weather.Pressure = (int)day.GetObject().GetNamedNumber("pressure");
                weather.Humidity = (int)day.GetObject().GetNamedNumber("humidity");

                var weatherTag = day.GetObject().GetNamedValue("weather").GetArray().GetObjectAt(0);

                weather.WeatherId = (int)weatherTag.GetNamedNumber("id");
                string imgName = convertWeatherId(weather.WeatherId);
                weather.Icon = "ms-appx://MDPA_MyWeather/Assets/WeatherIcons/" + imgName + ".png";

                weather.WeatherDescription = weatherTag.GetNamedString("description");

                weather.WindSpeed = (int)day.GetObject().GetNamedNumber("speed");

                weather.Cloudiness = (int)day.GetObject().GetNamedNumber("clouds");

                weather.DateText = Utils.FormatFullDateFromTicks(weather.Date);

                forecast.Add(weather);
            }
            return forecast;
        }

        public async Task<List<Weather>> GetForecastWeather(double latitude, double longitude)
        {
            String uri = URL_BASE + FORECAST + "lat=" + latitude + "&lon=" + longitude + "&cnt=7" + "&" + API_KEY + "&units=metric";
            string content = await SendHttpRequest(uri);
            if (content == null) return null;
            return parseForecastWeatherResponse(content);
        }

        public async Task<List<Weather>> GetForecastWeather(string cityNameQuery)
        {
            String uri = URL_BASE + FORECAST + "q=" + cityNameQuery + "&cnt=7" + "&" + API_KEY + "&units=metric";
            string content = await SendHttpRequest(uri);
            if (content == null) return null;
            return parseForecastWeatherResponse(content);
        }

        public string convertWeatherId(int id)
        {
            switch (id / 100)
            {
                case 2:
                    return "Thunderstorm";
                case 3:
                    return "Drizzle";
                case 5:
                    return "Rain";
                case 6:
                    return "Snow";
                case 7:
                    return "Atmosphere";
                case 8:
                    if (id == 800) return "Clear";
                    else return "Clouds";
                case 9:
                    if (id / 10 == 90) return "Extreme";
                    else return "Additional";
                default:
                    return "error";
                    
            }
        }
    }
}
