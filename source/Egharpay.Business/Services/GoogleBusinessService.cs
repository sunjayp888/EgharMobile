using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Models;
using System.Device.Location;

namespace Egharpay.Business.Services
{
    public class GoogleBusinessService : IGoogleBusinessService
    {
        public double RetrieveDistanceInKilometer(GeoPosition startGeoPosition, GeoPosition endGeoPosition)
        {
            //HaversineFormulae to calculate distance
            var latitude1 = startGeoPosition.Latitude;
            var longitude1 = startGeoPosition.Longitude;
            var latitude2 = endGeoPosition.Latitude;
            var longitude2 = endGeoPosition.Longitude;
            const int radiusOfEarth = 6371; // Radius of the earth in km
            var dLat = DegreeToRadian(latitude2 - latitude1);  // deg2rad below
            var dLon = DegreeToRadian(longitude2 - longitude1);
            var a =
              Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
              Math.Cos(DegreeToRadian(latitude1)) * Math.Cos(DegreeToRadian(latitude2)) *
              Math.Sin(dLon / 2) * Math.Sin(dLon / 2)
              ;
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return Math.Round((radiusOfEarth * c), 2); // Distance in km
        }

        public Task<GeoPosition> RetrieveCurrentGeoCoordinates()
        {
            var watcher = new GeoCoordinateWatcher();

            // Do not suppress prompt, and wait 1000 milliseconds to start.
            watcher.TryStart(false, TimeSpan.FromMilliseconds(100));

            var coordinates = watcher.Position.Location;
            return Task.FromResult(new GeoPosition()
            {
                Latitude = coordinates.IsUnknown != true ? coordinates.Latitude : 0.0,
                Longitude = coordinates.IsUnknown != true ? coordinates.Longitude : 0.0
            });
        }

        private double DegreeToRadian(double deg)
        {
            return deg * (Math.PI / 180);
        }
    }
}
