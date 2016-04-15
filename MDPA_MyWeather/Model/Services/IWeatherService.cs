
namespace MDPA_MyWeather.Model.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IWeatherService
    {
        Task<Weather> GetCurrentWeather(double latitude, double longitude);
        Task<Weather> GetCurrentWeather(string cityName);
        Task<List<Weather>> GetForecastWeather(double latitude, double longitude);
        Task<List<Weather>> GetForecastWeather(string cityName);
    }
}

