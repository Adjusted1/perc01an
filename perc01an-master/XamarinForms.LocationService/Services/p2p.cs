using Android.Bluetooth;
using Android.Bluetooth.LE;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinForms.LocationService.Messages;
using XamarinForms.LocationService;
using System.IO;
using Plugin.BLE;
using Plugin.BLE.Abstractions.EventArgs;
using Android.OS;
using Android.Runtime;
using Plugin.BLE.Abstractions.Exceptions;
using Plugin.BLE.Abstractions.Contracts;
//lat -90 to 90
//long -180 to 180
namespace XamarinForms.LocationService.Services
{
    class p2p : Location
    {
        readonly NeighborMatrices NM = new NeighborMatrices();



        //public static Dictionary<string, CartesianVector> CartesianLocation= new Dictionary<string, CartesianVector>(); // string is BL hardware Addr

        public static List<Plugin.BLE.Abstractions.Contracts.IDevice> deviceList = new List<Plugin.BLE.Abstractions.Contracts.IDevice>();

        private int numNeighs = 0;
        public static string lastSsid = "0,0";
        public static string MyNewName = "";
        public static List<string> CombinedSsids { get; set; }
        public static string filepath = "";
        public static bool doneScanning = false;
        public static int recvdFrom = 0;
        public static int nextNeigh = 0;
        public static string Ssidneighzero = "0,0";
        public static string Ssidneighone = "0,0";
        public static string Ssidneightwo = "0,0";
        public static string Ssidneighthree = "0,0";
        public static string Ssidneighfour = "0,0";
        public static string Ssidneighfive = "0,0";
        public static string Ssidneighsix = "0,0";
        public static string Ssidneighseven = "0,0";
        public static string Scanning = "";
        public static bool ThisNameWasChanged = false;
        public static bool ReleaseHold = false;
        public object Lock;
        public static Random _random = new Random();

        public static bool debugging = false;

        Plugin.BLE.Abstractions.Contracts.IBluetoothLE current;
        Plugin.BLE.Abstractions.Contracts.IAdapter adapter;

        public p2p(double lat, double lng)
        {
            Xamarin.Essentials.Location location = new Xamarin.Essentials.Location(lat, lng);
            Scanning = location.Latitude.ToString() + "," + location.Longitude.ToString();
            AndroidBluetoothSetLocalName(Scanning);
            

            CombinedSsids = new List<string>();
            Lock = new object();            
            current = CrossBluetoothLE.Current;
            adapter = CrossBluetoothLE.Current.Adapter;
            adapter.ScanTimeout = 1000;
            adapter.DeviceDiscovered += (s, a) =>
            {
                //adapter.StopScanningForDevicesAsync();
                if (!deviceList.Contains(a.Device))
                {
                    deviceList.Add(a.Device);
                    numNeighs = GetLikelyToBeHumanNeighCount(deviceList);
                }
            };
            adapter.DeviceConnected += (s, a) =>
            {
                lastSsid = deviceList[recvdFrom].Name;
            };
            StartGATT();
            
        }



        public void StartTimer(int ms)
        {
            Device.StartTimer(TimeSpan.FromSeconds(ms), () =>
            {
                // called every ms: milliseconds
                ReleaseHold = !ReleaseHold;
                return true; // return true to repeat counting, false to stop timer
            });
        }
        public async Task GetNeighs()
        {
            adapter.StartScanningForDevicesAsync();
            if (doneScanning)
            {
                await adapter.StopScanningForDevicesAsync();
            }

            if (numNeighs > 0)
            {
                ConnectToNeighbors(deviceList, numNeighs, adapter);
                UpdateNames(recvdFrom, lastSsid);
                StopGATT();
                AndroidBluetoothSetLocalName(lastSsid);
                StartGATT();
            }
        }
        private int GetLikelyToBeHumanNeighCount(List<IDevice> deviceList)
        {
            int i = 0;
            foreach (var device in deviceList)
            {
                if (device.NativeDevice != null)
                {
                    i++;
                }
            }
            return i;
        }
        private static async Task ConnectToNeighbors(List<Plugin.BLE.Abstractions.Contracts.IDevice> devices, int i,
                                               Plugin.BLE.Abstractions.Contracts.IAdapter adapter)
        {
            if (!debugging)
            {
                // run if not in debug mode
                recvdFrom = _random.Next(0, 7);
                try
                {
                    if (devices.Count > 0)
                    {
                        try
                        {                            
                            adapter.ConnectToDeviceAsync(devices[recvdFrom]);
                        }
                        catch (DeviceConnectionException e)
                        {
                            // ... could not connect to device
                            //lastSsid = "0,0";
                        }
                    }
                    else
                    {
                        
                    }
                }
                catch (DeviceConnectionException e)
                {

                }
            }



        }
        private static void GetPeerNames()
        {

        }
        public static int GetRandomNumber(int min, int max)
        {
             lock(_random) // synchronize
             {
                 return _random.Next(min, max);
             }
        }
        public void UpdateNames(int recvFrom, string lastSSID)
        {
            CombinedSsids.Add(lastSSID + "|");
            if (debugging)
            {
                var lastSSIDLat = GetRandomNumber(-90, 90);
                var lastSSIDLong = GetRandomNumber(-180, 180);
                lastSSID = lastSSIDLat + "," + lastSSIDLong;
                recvdFrom = GetRandomNumber(0, 7);
            }
            try
            {
                if (recvFrom == 0)
                {
                    Ssidneighzero = lastSSID;
                }
                if (recvFrom == 1)
                {
                    Ssidneighone = lastSSID;
                }
                if (recvFrom == 2)
                {
                    Ssidneightwo = lastSSID;
                }
                if (recvFrom == 3)
                {
                    Ssidneighthree = lastSSID;
                }
                if (recvFrom == 4)
                {
                    Ssidneighfour = lastSSID;
                }
                if (recvFrom == 5)
                {
                    Ssidneighfive = lastSSID;
                }
                if (recvFrom == 6)
                {
                    Ssidneighsix = lastSSID;
                }
                if (recvFrom == 7)
                {
                    Ssidneighseven = lastSSID;
                }
            }
            catch (Exception ex)
            {
            }

        }
        
        public void RemoveSafeNodes()
        {

        }
        public void ShowData()
        {
            var s = File.ReadAllText(filepath);
            var message = new LocationMessage
            {
                Ssid = s
            };
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                MessagingCenter.Send<LocationMessage>(message, "Location");
            });
        }
        public void AndroidBluetoothSetLocalName(string thisNewBLE_Name)
        {
            try
            {
                if (thisNewBLE_Name.Length > 0)
                {
                    BluetoothAdapter.DefaultAdapter.SetName(thisNewBLE_Name);
                }
            }
            catch (Exception e)
            {
                lastSsid = ">fault ON this BL rename<";
            }
        }
        BluetoothLeAdvertiser advertiser;
        private void StartGATT()
        {
            try
            {


                advertiser = BluetoothAdapter.DefaultAdapter.BluetoothLeAdvertiser;

                var advertiseBuilder = new AdvertiseSettings.Builder();
                var parameters = advertiseBuilder.SetConnectable(true)
                                                 .SetAdvertiseMode(AdvertiseMode.Balanced)
                                                 .SetTxPowerLevel(AdvertiseTx.PowerHigh)
                                                 .Build();

                AdvertiseData data = (new AdvertiseData.Builder()).SetIncludeDeviceName(true).Build();
                MyAdvertiseCallback callback = new MyAdvertiseCallback();
                advertiser.StartAdvertising(parameters, data, callback);
            }
            catch (Exception e)
            {
                CombinedSsids.Add("GATT Advertisement error");
            }
        }
        public class MyAdvertiseCallback : AdvertiseCallback
        {
            public override void OnStartFailure([GeneratedEnum] AdvertiseFailure errorCode)
            {
                base.OnStartFailure(errorCode);
            }

            public override void OnStartSuccess(AdvertiseSettings settingsInEffect)
            {
                base.OnStartSuccess(settingsInEffect);
            }
        }
        public void StopGATT()
        {
            try
            {
                advertiser.StopAdvertising(null);
            }
            catch (Exception exc) { }

        }
    }

 }
    
    

