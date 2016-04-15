
namespace MDPA_MyWeather.Model
{
    using Services;
    using System;
    using System.Threading.Tasks;
    using Windows.Devices.Geolocation;

    class GeolocationServiceImpl : IGeolocationService
    {
        private static Geoposition currentPosition = null;

        public async Task<Geoposition> GetCurrentGeoposition()
        {
            
            var result = await Geolocator.RequestAccessAsync();

            switch (result)
            {
                case GeolocationAccessStatus.Allowed:
                    var locator = new Geolocator();
                    currentPosition = await locator.GetGeopositionAsync();
                    break;
                case GeolocationAccessStatus.Denied:
                    break;
                case GeolocationAccessStatus.Unspecified:
                    break;
            }

            return currentPosition;
        }

    }

}
