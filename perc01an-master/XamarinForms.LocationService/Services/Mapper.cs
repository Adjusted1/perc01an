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
            Show();
        }
        public static void Show() 
        {
            var message = new LocationMessage
            {
                
            };
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    MessagingCenter.Send<LocationMessage>(message, "Location");
                });
            }
            catch(Exception e)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    MessagingCenter.Send<LocationMessage>(message, "Location");
                });
            }
        }
    }
}
