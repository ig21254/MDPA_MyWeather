
namespace MDPA_MyWeather.Model
{
    public class Weather
    {
        private int weatherId;
        private string weatherDescription;
        private int temp;
        private int tempMin;
        private int tempMax;
        private int pressure;
        private int humidity;
        private double windSpeed;
        private double cloudiness;
        private string icon;
        private string cityName;
        private long date;
        private string dateText;

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
        public int Temp
        {
            get { return temp; }
            set { temp = value; }
        }
        public int TempMin
        {
            get { return tempMin; }
            set { tempMin = value; }
        }
        public int TempMax
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
        
        public string CityName
        {
            get { return cityName; }
            set { cityName = value; }
        }
        
        public long Date
        {
            get { return date; }
            set { date = value; }
        }

        public string DateText
        {
            get { return dateText; }
            set { dateText = value; }
        }

        public Weather()
        {

        }
    }
}
