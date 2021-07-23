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

       
        public Location()
        {
        }
        //public Location(bool suppress) { }       
        public async Task Run(CancellationToken token)
        {
            p2p = new p2p();
            {
                while (!stopping)
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Best);
                    var location = await Geolocation.GetLocationAsync(request);

                    p2p.AndroidBluetoothSetLocalName(location.Latitude.ToString() + "," + location.Longitude.ToString());
                    Mapper.Update(0, location.Latitude.ToString() + "," + location.Longitude.ToString());

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
                                double testDistance = CalculateDistance(location, location);
                                var message = new LocationMessage
                                {
                                    Latitude = location.Latitude,
                                    Longitude = location.Longitude,
                                    Ssid = p2p.CombinedSsids + System.Environment.NewLine + "|my lat:" + location.Latitude.ToString() + "|my long:" + location.Longitude.ToString(),
                                    SsidNeighZero = p2p.Ssidneighzero,
                                    SsidNeighOne = p2p.Ssidneighone,
                                    SsidNeighTwo = p2p.Ssidneightwo,
                                    SsidNeighThree = p2p.Ssidneighthree,
                                    SsidNeighFour = p2p.Ssidneighfour,
                                    SsidNeighFive = p2p.Ssidneighfive,
                                    SsidNeighSix = p2p.Ssidneighsix,
                                    SsidNeighSeven = p2p.Ssidneighseven,
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
