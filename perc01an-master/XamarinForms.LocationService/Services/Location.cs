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
        
        private interface Ip2p
        {
            
        }
        public Location()
        {
           
        }
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

            double dist0 = 0.0;
            double dist1 = 0.0;
            double dist2 = 0.0;
            double dist3 = 0.0;
            double dist4 = 0.0;
            double dist5 = 0.0;
            double dist6 = 0.0;
            double dist7 = 0.0;

            bool perc0 = false;
            bool perc1 = false;
            bool perc2 = false;
            bool perc3 = false;
            bool perc4 = false;
            bool perc5 = false;
            bool perc6 = false;
            bool perc7 = false;

            p2p = new p2p();
            {
                while (!stopping)
                {
                    //await Task.Run(async () =>
                    //{
                    //    try
                    //    {
                    //        await p2p.ProcessDiscovered();

                    //    }
                    //    catch (Exception exc)
                    //    {

                    //    }
                    //}, token);
                    await Task.Run(async () =>
                    {
                        try
                        {
                            if (location != null)
                            {
                                double testDistance = CalculateDistance(location, location);
                                if (p2p.Ssidneighzero.Contains(','))
                                {
                                    perc0 = true;
                                    dist0 = CalculateDistance(LocFomNeighStr(p2p.Ssidneighzero), location);
                                } 
                                else { perc0 = false; }
                                if (p2p.Ssidneighone.Contains(','))
                                {
                                    perc1 = true;
                                    dist1 = CalculateDistance(LocFomNeighStr(p2p.Ssidneighone), location);
                                }
                                else { perc1 = false; }
                                if (p2p.Ssidneightwo.Contains(','))
                                {
                                    perc2 = true;
                                    dist2 = CalculateDistance(LocFomNeighStr(p2p.Ssidneightwo), location);
                                }
                                else { perc2 = false; }
                                if (p2p.Ssidneighthree.Contains(','))
                                {
                                    perc3 = true;
                                    dist3 = CalculateDistance(LocFomNeighStr(p2p.Ssidneighthree), location);
                                }
                                else { perc3 = false; }
                                if (p2p.Ssidneighfour.Contains(','))
                                {
                                    perc4 = true;
                                    dist4 = CalculateDistance(LocFomNeighStr(p2p.Ssidneighfour), location);
                                }
                                else { perc4 = false; }
                                if (p2p.Ssidneighfive.Contains(','))
                                {
                                    perc5 = true;
                                    dist5 = CalculateDistance(LocFomNeighStr(p2p.Ssidneighfive), location);
                                }
                                else { perc5 = false; }
                                if (p2p.Ssidneighsix.Contains(','))
                                {
                                    perc6 = true;
                                    dist6 = CalculateDistance(LocFomNeighStr(p2p.Ssidneighsix), location);
                                }
                                else { perc6 = false; }
                                if (p2p.Ssidneighseven.Contains(','))
                                {
                                    perc7 = true;
                                    dist7 = CalculateDistance(LocFomNeighStr(p2p.Ssidneighseven), location);
                                }
                                else { perc7 = false; }

                                /*
                                 *  
                                 *      Latitude = location.Latitude,
                                        Longitude = location.Longitude,
                                        Ssid = location.Latitude.ToString() + "," + location.Longitude.ToString() + " " + "No Excessive Accelerations Detected",
                                        SsidNeighZero = (Math.Truncate(dist0 * 100) / 100).ToString() + "km",
                                        SsidNeighOne = (Math.Truncate(dist1 * 100) / 100).ToString() + "km",
                                        SsidNeighTwo = (Math.Truncate(dist2 * 100) / 100).ToString() + "km",
                                        SsidNeighThree = (Math.Truncate(dist3 * 100) / 100).ToString() + "km",
                                        SsidNeighFour = (Math.Truncate(dist4 * 100) / 100).ToString() + "km",
                                        SsidNeighFive = (Math.Truncate(dist5 * 100) / 100).ToString() + "km",
                                        SsidNeighSix = (Math.Truncate(dist6 * 100) / 100).ToString() + "km",
                                        SsidNeighSeven = (Math.Truncate(dist7 * 100) / 100).ToString() + "km",
                                        Scanning = p2p.Scanning
                                */
                                var message = new LocationMessage
                                {
                                    Latitude = location.Latitude,
                                    Longitude = location.Longitude,
                                    Ssid = location.Latitude.ToString() + "," + location.Longitude.ToString() + " " + "No Excessive Accelerations Detected",
                                   
                                    SsidNeighZero = (Math.Truncate(dist0 * 100) / 100).ToString() + "km",
                                    SsidNeighOne = (Math.Truncate(dist1 * 100) / 100).ToString() + "km",
                                    SsidNeighTwo = (Math.Truncate(dist2 * 100) / 100).ToString() + "km",
                                    SsidNeighThree = (Math.Truncate(dist3 * 100) / 100).ToString() + "km",
                                    SsidNeighFour = (Math.Truncate(dist4 * 100) / 100).ToString() + "km",
                                    SsidNeighFive = (Math.Truncate(dist5 * 100) / 100).ToString() + "km",
                                    SsidNeighSix = (Math.Truncate(dist6 * 100) / 100).ToString() + "km",
                                    SsidNeighSeven = (Math.Truncate(dist7 * 100) / 100).ToString() + "km",
                                    Scanning = p2p.Scanning
                                    //Latitude = location.Latitude,
                                    //Longitude = location.Longitude,
                                    //Ssid = location.Latitude.ToString() + "," + location.Longitude.ToString(),
                                    //SsidNeighZero = p2p.Ssidneighzero,
                                    //SsidNeighOne = p2p.Ssidneighone,
                                    //SsidNeighTwo = p2p.Ssidneightwo,
                                    //SsidNeighThree = p2p.Ssidneighthree,
                                    //SsidNeighFour = p2p.Ssidneighfour,
                                    //SsidNeighFive = p2p.Ssidneighfive,
                                    //SsidNeighSix = p2p.Ssidneighsix,
                                    //SsidNeighSeven = p2p.Ssidneighseven,
                                    //Scanning = p2p.Scanning
                                };
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    MessagingCenter.Send<LocationMessage>(message, "Location");
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                var errormessage = new LocationErrorMessage();
                                MessagingCenter.Send<LocationErrorMessage>(errormessage, "LocationError" + " " + ex.ToString());
                            });
                        }
                        return;
                    }, token);
                }
            }
        }
    }

    
}
