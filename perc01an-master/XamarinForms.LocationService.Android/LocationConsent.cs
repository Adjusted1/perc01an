using System.Threading.Tasks;
using Xamarin.Essentials;
using XamarinForms.LocationService.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(LocationConsent))]
namespace XamarinForms.LocationService.Droid
{
    public class LocationConsent : ILocationConsent
    {
        public async Task GetLocationConsent()
        {
            // sdk v29+ bug on Android 10,11 requires location in use request BEFORE loc always request
            await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            // did NOT fix crash on my android 11.x s10
            await Permissions.RequestAsync<Permissions.LocationAlways>();
        }
    }
}