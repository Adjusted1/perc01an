using Android.Content;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace XamarinForms.LocationService.Services
{
    public class AccelerometerModel
    {
        SensorSpeed speed = SensorSpeed.UI;
        public static List<Tuple<float,float,float>> AccelData { get; set; }
        public static List<float> JerkHistory { get; set; }
        public static int i { get; set; } = 0;
        private static float jerkThreshold = 0.5f;
        public bool active { get; set; } = false;

        public AccelerometerModel()
        {

            // Register for reading changes, be sure to unsubscribe when finished
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
            //ToggleAccelerometer();
            Accelerometer.Start(speed);
            AccelData = new List<Tuple<float,float,float>>();
            JerkHistory = new List<float>();
            AccelData.Add(Tuple.Create(0.0f, 0.0f, 0.0f));
        }

        private void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            // start perc01an
            //AccelData.Add(null);
            //if(JerkHistory[i] > 0.5f)
            //{
            //    // start perc01ating
            //    active = true; 
            //}

        }

        void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            /**
             * 
             * jerk values all 1 !!!
             * 
             * 
             */
            i++;
            var data = e.Reading;
            AccelData.Add(Tuple.Create(data.Acceleration.X, data.Acceleration.Y,data.Acceleration.Z));
            if (i > 0) 
            {
                var compx = (double)AccelData[i-1].Item1;
                var compy = (double)AccelData[i-1].Item2;
                var compz = (double)AccelData[i-1].Item3;
                JerkHistory.Add((float)Math.Sqrt(compx * compx + compy * compy + compz * compz));
                if (JerkHistory[i-1] > 1.5f)
                {
                    active = true; 
                }
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
