
namespace MDPA_MyWeather.Model.Services
{
    using System.Threading.Tasks;
    using Windows.Devices.Geolocation;

    public interface IGeolocationService
    {
        Task<Geoposition> GetCurrentGeoposition();
    }
}
