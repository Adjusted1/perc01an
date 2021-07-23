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
            Show(GPScommaSep, neighNum);
        }
        public static void Show(string gps, int neighNum)
        {
            string[] gpsSplit = gps.Split(",");
            double lat = double.Parse(gpsSplit[0], System.Globalization.CultureInfo.InvariantCulture);
            double lng = double.Parse(gpsSplit[1], System.Globalization.CultureInfo.InvariantCulture);
            CartesianVector v = new CartesianVector(lat, lng);
#if DEBUG
            neighNum = 0;
#endif
            if (neighNum == 0)
            {
                var message = new LocationMessage
                {
                    X0 = v.x.ToString(),
                    Y0 = v.y.ToString(),
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
        }
        public class CartesianVector
        {
            public double x { get; set; } = 0.0;
            public double y { get; set; } = 0.0;
            public double z { get; set; } = 0.0;
            
            /*
             * normalize formula:
             * newvalue= (max'-min')/(max-min)*(value-min)+min'.
             * 
             */
            const double R = 6371000; // Radius earth in meters
            public CartesianVector(double lat, double lng)
            {
                x = R * Math.Cos(lat) * Math.Cos(lng);
                y = R * Math.Cos(lat) * Math.Sin(lng);
                z = R * Math.Sin(lat);
                Normalizer();
            }
            private void Normalizer()
            {
                // x = x-minx/(maxx - minx)

                x = x / 100;
                y = y / 100;
                z = z / 100;
            }
        }
    }
}
