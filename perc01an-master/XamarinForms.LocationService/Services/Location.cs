using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinForms.LocationService.Messages;
using Android.Bluetooth;
using Google.Type;
using Plugin.BLE;
using Plugin.BLE.Abstractions.EventArgs;
using Android.OS;
using Android.Runtime;
using Plugin.BLE.Abstractions.Exceptions;
using Plugin.BLE.Abstractions.Contracts;
using System.Collections.ObjectModel;

namespace XamarinForms.LocationService.Services
{
    public class Location : ContentPage
    {

        public bool stopping = false;
        private p2p p2p = null;

       
        private static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }
        public static double CalculateDistance(Xamarin.Essentials.Location location1, Xamarin.Essentials.Location location2)
        {
            double circumference = 40000.0; // Earth's circumference at the equator in km
            double distance = 0.0;

            //Calculate radians
            double latitude1Rad = DegreesToRadians(location1.Latitude);
            double longitude1Rad = DegreesToRadians(location1.Longitude);
            double latititude2Rad = DegreesToRadians(location2.Latitude);
            double longitude2Rad = DegreesToRadians(location2.Longitude);

            double logitudeDiff = Math.Abs(longitude1Rad - longitude2Rad);

            if (logitudeDiff > Math.PI)
            {
                logitudeDiff = 2.0 * Math.PI - logitudeDiff;
            }

            double angleCalculation =
                Math.Acos(
                    Math.Sin(latititude2Rad) * Math.Sin(latitude1Rad) +
                    Math.Cos(latititude2Rad) * Math.Cos(latitude1Rad) * Math.Cos(logitudeDiff));

            distance = circumference * angleCalculation / (2.0 * Math.PI);

            return distance;
        }

        private static Xamarin.Essentials.Location LocFomNeighStr(string neighStr)
        {
            if (!neighStr.Contains(',')) { return new Xamarin.Essentials.Location(0, 0); }
            string[] lat_long = neighStr.Split(',');
            double lat = double.Parse(lat_long[0], System.Globalization.CultureInfo.InvariantCulture);
            double lon = double.Parse(lat_long[1], System.Globalization.CultureInfo.InvariantCulture);
            Xamarin.Essentials.Location L = new Xamarin.Essentials.Location(lat,lon);
            return L;            
        }
        
        public Location()
        {
        }
        //public Location(bool suppress) { }       
        //  I use the Connect method at a higher level of abstraction in a loop with a cancellation token until
        //  I get ConnectSucceed and am pretty quickly successful, although not always the first time
        public async Task Run(CancellationToken token)
        {

            XamarinForms.LocationService.Services.AccelerometerModel am = new LocationService.Services.AccelerometerModel();

            //am.active = true;
            //
            // why I'm using await Task.Run(async ()
            // sometimes I/O-bound operations are blocking and they don't provide a fully asynchronous API; 
            // in that case, it would be proper use await Task.Run to block a background thread 
            // instead of the UI thread even though the operation is technically I/O-bound and not CPU-bound.
            //
            var request = new GeolocationRequest(GeolocationAccuracy.Best);
            var location = await Geolocation.GetLocationAsync(request);
            var lat = location.Latitude;
            var lng = location.Longitude;
            p2p = new p2p();
            {
                while (!stopping)
                {
                    token.ThrowIfCancellationRequested();

                    double dist0 = CalculateDistance(LocFomNeighStr(p2p.Ssidneighzero), location);
                    double dist1 = CalculateDistance(LocFomNeighStr(p2p.Ssidneighone), location);
                    double dist2 = CalculateDistance(LocFomNeighStr(p2p.Ssidneightwo), location);
                    double dist3 = CalculateDistance(LocFomNeighStr(p2p.Ssidneighthree), location);
                    double dist4 = CalculateDistance(LocFomNeighStr(p2p.Ssidneighfour), location);
                    double dist5 = CalculateDistance(LocFomNeighStr(p2p.Ssidneighfive), location);
                    double dist6 = CalculateDistance(LocFomNeighStr(p2p.Ssidneighsix), location);
                    double dist7 = CalculateDistance(LocFomNeighStr(p2p.Ssidneighseven), location);
                    //p2p.AndroidBluetoothSetLocalName(location.Latitude.ToString() + "," + location.Longitude.ToString());

                    var message = new LocationMessage
                    {
                        Latitude = location.Latitude,
                        Longitude = location.Longitude,
                        Scanning = p2p.Scanning + location.Latitude.ToString() + "," + location.Longitude.ToString(),
                        Ssid = location.Latitude.ToString() + "," + location.Longitude.ToString(),
                        SsidNeighZero = (Math.Truncate(dist0 * 100) / 100).ToString() + "km",
                        SsidNeighOne = (Math.Truncate(dist1 * 100) / 100).ToString() + "km",
                        SsidNeighTwo = (Math.Truncate(dist2 * 100) / 100).ToString() + "km",
                        SsidNeighThree = (Math.Truncate(dist3 * 100) / 100).ToString() + "km",
                        SsidNeighFour = (Math.Truncate(dist4 * 100) / 100).ToString() + "km",
                        SsidNeighFive = (Math.Truncate(dist5 * 100) / 100).ToString() + "km",
                        SsidNeighSix = (Math.Truncate(dist6 * 100) / 100).ToString() + "km",
                        SsidNeighSeven = (Math.Truncate(dist7 * 100) / 100).ToString() + "km"
                    };
                    await Task.Run(async () =>
                    {
                        try
                        {
                            await p2p.GetNeighs();

                        }
                        catch (Exception exc)
                        {

                        }
                    }, token);
                    //Device.BeginInvokeOnMainThread(async () =>
                    //{
                    //    MessagingCenter.Send<LocationMessage>(message, "Location");
                    //    await Task.Run(async () =>
                    //    {
                    //        try
                    //        {
                    //            await p2p.GetNeighs();

                    //        }
                    //        catch (Exception exc)
                    //        {

                    //        }
                    //    }, token);
                    //});
                    //await Task.Run(async () =>
                    //{
                    //    try
                    //    {
                    //        p2p.GetNeighs();

                    //    }
                    //    catch (Exception exc)
                    //    {

                    //    }
                    //}, token);




                }
            }
        }

    }
}
