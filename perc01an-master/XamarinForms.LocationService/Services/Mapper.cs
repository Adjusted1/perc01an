using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinForms.LocationService.Messages;

namespace XamarinForms.LocationService.Services
{
    static class Mapper
    {
        //  NeighNumber_GPS_Mapper[0] = this devices gps coord, lat + "," + lng
        public static ConcurrentDictionary<int, string> NeighNumber_GPS_Mapper { get; set; }

        public static void Update(int neighNum, string GPScommaSep)
        {
            NeighNumber_GPS_Mapper[neighNum] = GPScommaSep;
            Show(GPScommaSep);
        }
        public static void Show(string gps)
        {
            string[] gpsSplit = gps.Split(",");
            double lat = double.Parse(gpsSplit[0], System.Globalization.CultureInfo.InvariantCulture);
            double lng = double.Parse(gpsSplit[1], System.Globalization.CultureInfo.InvariantCulture);
            CartesianVector v = new CartesianVector(lat, lng);

            var message = new LocationMessage
            {
                SsidNeighZero = p2p.Ssidneighzero,
                SsidNeighOne = p2p.Ssidneighone,
                SsidNeighTwo = p2p.Ssidneightwo,
                SsidNeighThree = p2p.Ssidneighthree,
                SsidNeighFour = p2p.Ssidneighfour,
                SsidNeighFive = p2p.Ssidneighfive,
                SsidNeighSix = p2p.Ssidneighsix,
                SsidNeighSeven = p2p.Ssidneighseven,
            };
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    MessagingCenter.Send<LocationMessage>(message, "Location");
                });
            }
            catch (Exception e)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    MessagingCenter.Send<LocationMessage>(message, "Location");
                });
            }
        }
        public class CartesianVector
        {
            public double x { get; set; } = 0.0;
            public double y { get; set; } = 0.0;
            public double z { get; set; } = 0.0;

            const double R = 6371000; // Radius earth in meters
            public CartesianVector(double lat, double lng)
            {
                x = R * Math.Cos(lat) * Math.Cos(lng);
                y = R * Math.Cos(lat) * Math.Sin(lng);
                z = R * Math.Sin(lat);
            }
        }
    }
}
