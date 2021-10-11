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
//using Plugin.BLE.Abstractions.Exceptions;
//using Plugin.BLE.Abstractions.Contracts;
using Android.Widget;
using Android.Content;

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

        //Plugin.BLE.Abstractions.Contracts.IBluetoothLE current;
        //Plugin.BLE.Abstractions.Contracts.IAdapter adapter;

       
        public p2p()
        {
            Lock = new object();

            //StartTimer(3000);
            //current = CrossBluetoothLE.Current;
            //adapter = CrossBluetoothLE.Current.Adapter;
            //adapter.ScanTimeout = 1000;
            //adapter.DeviceDiscovered += (s, a) => deviceList.Add(a.Device);
            Scanning = "|Scan:ON!";
            StartGATT("percNode");
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
            numNeighs = 0;
            deviceList.Clear();
            //await adapter.StartScanningForDevicesAsync();
            //ConnectToNeighbors(deviceList, numNeighs, adapter);
            //numNeighs = GetLikelyToBeHumanNeighCount(deviceList);
            AndroidBluetoothSetLocalName(lastSsid);
            UpdateNames(recvdFrom, lastSsid);
           
        }
        //private int GetLikelyToBeHumanNeighCount(List<IDevice> deviceList)
        //{
        //    int i = 0;
        //    foreach (var device in deviceList)
        //    {
        //        if (device.NativeDevice != null)
        //        {
        //            i++;
        //        }
        //    }
        //    return i;
        //}
        //private static void ConnectToNeighbors(List<Plugin.BLE.Abstractions.Contracts.IDevice> devices, int i, Plugin.BLE.Abstractions.Contracts.IAdapter adapter)
        //{
        //    recvdFrom = _random.Next(0, 7);
        //    try
        //    {
        //        adapter.StopScanningForDevicesAsync();
        //        adapter.ConnectToDeviceAsync(devices[recvdFrom]);
        //        if (devices[recvdFrom].Name == null)
        //        {
        //        }
        //        else
        //        {
        //            lastSsid = devices[recvdFrom].Name;
        //        }
        //    }
        //    catch (DeviceConnectionException e)
        //    {

        //    }

        //}
        private static void GetPeerNames()
        {

        }
        public void UpdateNames(int recvFrom, string lastSSID)
        {

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
        //public void LogData()
        //{
        //    byte[] data = Encoding.ASCII.GetBytes(CombinedSsids);
        //    string DownloadsPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads);
        //    filepath = CombinedSsids + "_" + Path.Combine(DownloadsPath, "perc01anData.csv");
        //    File.WriteAllBytes(filepath, data);
        //}
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
                    //ThisNameWasChanged = true;

                }
            }
            catch (Exception e)
            {
                CombinedSsids.Add(">fault ON this BL rename<");
            }
        }
        BluetoothLeAdvertiser advertiser;
        private void StartGATT(string newName)
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
        private void StopGATT()
        {
            try
            {
                advertiser.StopAdvertising(null);
            }
            catch (Exception exc) { }

        }
    }

}


