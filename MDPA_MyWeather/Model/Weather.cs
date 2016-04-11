
namespace MDPA_MyWeather.Model
{
    class Weather
    {
        private int weatherId;
        private string weatherDescription;
        private double temp;
        private double tempMin;
        private double tempMax;
        private int pressure;
        private int humidity;
        private double windSpeed;
        private double cloudiness;
        private string icon;

        public int WeatherId
        {
            get { return weatherId; }
            set { weatherId = value; }
        }
        public string WeatherDescription
        {
            get { return weatherDescription; }
            set { weatherDescription = value; }
        }
        public double Temp
        {
            get { return temp; }
            set { temp = value; }
        }
        public double TempMin
        {
            get { return tempMin; }
            set { tempMin = value; }
        }
        public double TempMax
        {
            get { return tempMax; }
            set { tempMax = value; }
        }
        public int Pressure
        {
            get { return pressure; }
            set { pressure = value; }
        }
        public int Humidity
        {
            get { return humidity; }
            set { humidity = value; }
        }
        public double WindSpeed
        {
            get { return windSpeed; }
            set { windSpeed = value; }
        }
        public double Cloudiness
        {
            get { return cloudiness; }
            set { cloudiness = value; }
        }
        public string Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        public Weather()
        {

        }
    }
}
