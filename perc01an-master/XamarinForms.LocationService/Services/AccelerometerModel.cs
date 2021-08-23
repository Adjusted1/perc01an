using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace XamarinForms.LocationService.Services
{
    class AccelerometerModel : Location
    {
        SensorSpeed speed = SensorSpeed.UI;
        public static List<Tuple<float,float,float>> AccelData { get; set; }
        public static List<float> JerkHistory { get; set; }
        public static int i { get; set; } = 0;

        public AccelerometerModel()
        {
            // Register for reading changes, be sure to unsubscribe when finished
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            AccelData = new List<Tuple<float,float,float>>();
        }

        void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            i++;
            var data = e.Reading;
            AccelData.Add(Tuple.Create(data.Acceleration.X, data.Acceleration.Y,data.Acceleration.Z));
            if (i > 0) 
            {
                var compx = (double)AccelData[i-1].Item1;
                var compy = (double)AccelData[i-1].Item2;
                var compz = (double)AccelData[i-1].Item3;
                JerkHistory.Add((float)Math.Sqrt(compx * compx + compy * compy + compz * compz));
            }
            var firstXvalue = AccelData[0].Item1;
            var firstYvalue = AccelData[0].Item2;
            var firstZvalue = AccelData[0].Item3;
        }
        public float ReturnJerk(float accel)
        {
            return 0.0f;
        }
        public void CollectData()
        {
            ToggleAccelerometer();
        }
        public void ToggleAccelerometer()
        {
            try
            {
                if (Accelerometer.IsMonitoring)
                    Accelerometer.Stop();
                else
                    Accelerometer.Start(speed);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }
    }
}
