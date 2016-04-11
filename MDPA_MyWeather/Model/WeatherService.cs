
namespace MDPA_MyWeather.Model
{
    using System;
    using Windows.Web.Http;
    using Windows.Data.Json;


    class WeatherService
    {
        private const string API_KEY = "appid=30784d457d092ececdc59b00384ca2df";
        private const string URL_BASE = "http://api.openweathermap.org/data/2.5/";
        /* api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon} */
        private const string CURRENT_WEATHER = "weather?";
        /* api.openweathermap.org/data/2.5/forecast/daily?lat={lat}&lon={lon}&cnt={cnt} */
        private const string FORECAST = "forecast/daily?q=";

        private HttpClient client;

        public WeatherService()
        {
            client = new HttpClient();
        }

        public async System.Threading.Tasks.Task<Weather> getCurrentWeather(double latitude, double longitude)
        {
            Weather current = new Weather();

            String uri = URL_BASE + CURRENT_WEATHER + "lat=" + latitude + "&lon=" + longitude + "&" + API_KEY + "&units=metric";
            Uri myUri = new Uri(uri);
            HttpResponseMessage message = await client.GetAsync(myUri);

            //System.Diagnostics.Debug.WriteLine(message);

            if (message.StatusCode != HttpStatusCode.Ok)
            {
                return null;
            }

            string content = await message.Content.ReadAsStringAsync();

            var root = JsonValue.Parse(content).GetObject();

            var weather = root.GetNamedValue("weather").GetArray().GetObjectAt(0);
            current.WeatherId = (int)weather.GetNamedNumber("id");
            current.WeatherDescription = weather.GetNamedString("description");

            var main = root.GetNamedValue("main").GetObject();
            current.Temp = main.GetNamedNumber("temp");
            current.Pressure = (int)main.GetNamedNumber("pressure");
            current.Humidity = (int)main.GetNamedNumber("humidity");
            current.TempMin = main.GetNamedNumber("temp_min");
            current.TempMax = main.GetNamedNumber("temp_max");

            var wind = root.GetNamedValue("wind").GetObject();
            current.WindSpeed = wind.GetNamedNumber("speed");

            var clouds = root.GetNamedObject("clouds");
            current.Cloudiness = clouds.GetNamedNumber("all");

            string imgName = convertWeatherId(current.WeatherId);
            current.Icon = "ms-appx://MDPA_MyWeather/Assets/WeatherIcons/" + imgName + ".png";

            return current;
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
