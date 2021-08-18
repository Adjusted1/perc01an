using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinForms.LocationService.Messages;
using Android.Bluetooth;
using Google.Type;

namespace XamarinForms.LocationService.Services
{
    public class Location
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
            string[] lat_long = neighStr.Split(',');
            double lat = double.Parse(lat_long[0], System.Globalization.CultureInfo.InvariantCulture);
            double lon = double.Parse(lat_long[1], System.Globalization.CultureInfo.InvariantCulture);
            Xamarin.Essentials.Location L = new Xamarin.Essentials.Location(lat,lon);// = new Location(lat_long[0] + lat_long[1]);
            return L;            
        }
        
        public Location()
        {
        }
        //public Location(bool suppress) { }       
        public async Task Run(CancellationToken token)
        {
            //
            // why I'm using await Task.Run(async ()
            // sometimes I/O-bound operations are blocking and they don't provide a fully asynchronous API; 
            // in that case, it would be proper use await Task.Run to block a background thread 
            // instead of the UI thread even though the operation is technically I/O-bound and not CPU-bound.
            //
            p2p = new p2p();
            {
                //p2p.lastSsid = "waiting for data";
                while (!stopping)
                {
                    if(stopping)
                    {
                         p2p.doneScanning = true;   
                    }
                    var request = new GeolocationRequest(GeolocationAccuracy.Best);
                    var location = await Geolocation.GetLocationAsync(request);

                    //p2p.AndroidBluetoothSetLocalName(location.Latitude.ToString() + "," + location.Longitude.ToString());

                    token.ThrowIfCancellationRequested();
                    await Task.Run(async() =>
                    {
                        try
                        {
                            await p2p.GetNeighs(location.Latitude, location.Longitude);

                        }
                        catch (Exception exc)
                        {

                        }
                    }, token);
                    
                    await Task.Run(async () =>
                    {
                        try
                        {
                            
                            if (location != null)
                            {
                                // always same value w/diff gps wtf
                                double dist0 = CalculateDistance(LocFomNeighStr(p2p.Ssidneighzero), location);
                                double dist1 = CalculateDistance(LocFomNeighStr(p2p.Ssidneighone), location);
                                double dist2 = CalculateDistance(LocFomNeighStr(p2p.Ssidneightwo), location);
                                double dist3 = CalculateDistance(LocFomNeighStr(p2p.Ssidneighthree), location);
                                double dist4 = CalculateDistance(LocFomNeighStr(p2p.Ssidneighfour), location);
                                double dist5 = CalculateDistance(LocFomNeighStr(p2p.Ssidneighfive), location);
                                double dist6 = CalculateDistance(LocFomNeighStr(p2p.Ssidneighsix), location);
                                double dist7 = CalculateDistance(LocFomNeighStr(p2p.Ssidneighseven), location);
                                var message = new LocationMessage
                                {
                                    Latitude = location.Latitude,
                                    Longitude = location.Longitude,
                                    Ssid = location.Latitude.ToString() + "," + location.Longitude.ToString(),
                                    SsidNeighZero = dist0.ToString(),
                                    SsidNeighOne = dist1.ToString(),
                                    SsidNeighTwo = dist2.ToString(),
                                    SsidNeighThree = dist3.ToString(),
                                    SsidNeighFour = dist4.ToString(),
                                    SsidNeighFive = dist5.ToString(),
                                    SsidNeighSix = dist6.ToString(),
                                    SsidNeighSeven = dist7.ToString(),
                                    Scanning = p2p.Scanning
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
