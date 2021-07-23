using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinForms.LocationService.Messages;
using XamarinForms.LocationService.Utils;
using XamarinForms.LocationService.Services;

namespace XamarinForms.LocationService.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        #region vars
        private double latitude;
        private double longitude;
        private string ssid;
        private string ssidneighzero;
        private string ssidneighone;
        private string ssidneightwo;
        private string ssidneighthree;
        private string ssidneighfour;
        private string ssidneighfive;
        private string ssidneighsix;
        private string ssidneighseven;
        private string scanning;

        private string x0;
        private string x1;
        private string x2;
        private string x3;
        private string x4;
        private string x5;
        private string x6;
        private string x7;
        private string y0;
        private string y1;
        private string y2;
        private string y3;
        private string y4;
        private string y5;
        private string y6;
        private string y7;
        private string _x0;
        private string _x1;
        private string _x2;
        private string _x3;
        private string _x4;
        private string _x5;
        private string _x6;
        private string _x7;
        private string _y0;
        private string _y1;
        private string _y2;
        private string _y3;
        private string _y4;
        private string _y5;
        private string _y6;
        private string _y7;
       

        private string userMessage;
        private bool startEnabled;
        private bool stopEnabled;
        #endregion vars

        #region properties
        public double Latitude
        {
            get => latitude;
            set => SetProperty(ref latitude, value);
        }
        public double Longitude
        {
            get => longitude;
            set => SetProperty(ref longitude, value);
        }
        public string UserMessage
        {
            get => userMessage;
            set => SetProperty(ref userMessage, value);
        }
        public bool StartEnabled
        {
            get => startEnabled;
            set => SetProperty(ref startEnabled, value);
        }
        public bool StopEnabled
        {
            get => stopEnabled;
            set => SetProperty(ref stopEnabled, value);
        }
        public string Ssid
        {
            get => ssid;
            set => SetProperty(ref ssid, value);
        }
        public string SsidNeighZero
        {
            get => ssidneighzero;
            set => SetProperty(ref ssidneighzero, value);
        }
        public string SsidNeighOne
        {
            get => ssidneighone;
            set => SetProperty(ref ssidneighone, value);
        }
        public string SsidNeighTwo
        {
            get => ssidneightwo;
            set => SetProperty(ref ssidneightwo, value);
        }
        public string SsidNeighThree
        {
            get => ssidneighthree;
            set => SetProperty(ref ssidneighthree, value);
        }
        public string SsidNeighFour
        {
            get => ssidneighfour;
            set => SetProperty(ref ssidneighfour, value);
        }
        public string SsidNeighFive
        {
            get => ssidneighfive;
            set => SetProperty(ref ssidneighfive, value);
        }
        public string SsidNeighSix
        {
            get => ssidneighsix;
            set => SetProperty(ref ssidneighsix, value);
        }
        public string SsidNeighSeven
        {
            get => ssidneighseven;
            set => SetProperty(ref ssidneighseven, value);
        }

        public string Scanning
        {
            get => scanning;
            set => SetProperty(ref scanning, value);
        }
        public string X0
        {
            get => x0;
            set => SetProperty(ref x0, value);
        }
        public string X1
        {
            get => x1;
            set => SetProperty(ref x1, value);
        }
        public string X2
        {
            get => x2;
            set => SetProperty(ref x2, value);
        }
        public string X3
        {
            get => x3;
            set => SetProperty(ref x3, value);
        }
        public string X4
        {
            get => x4;
            set => SetProperty(ref x4, value);
        }
        public string X5
        {
            get => x5;
            set => SetProperty(ref x5, value);
        }
        public string X6
        {
            get => x6;
            set => SetProperty(ref x6, value);
        }
        public string X7
        {
            get => x7;
            set => SetProperty(ref x7, value);
        }
        public string Y0
        {
            get => y0;
            set => SetProperty(ref y0, value);
        }
        public string Y1
        {
            get => y1;
            set => SetProperty(ref y1, value);
        }
        public string Y2
        {
            get => y2;
            set => SetProperty(ref y2, value);
        }
        public string Y3
        {
            get => y3;
            set => SetProperty(ref y3, value);
        }
        public string Y4
        {
            get => y4;
            set => SetProperty(ref y4, value);
        }
        public string Y5
        {
            get => y5;
            set => SetProperty(ref y5, value);
        }
        public string Y6
        {
            get => y6;
            set => SetProperty(ref y6, value);
        }
        public string Y7
        {
            get => y7;
            set => SetProperty(ref y7, value);
        }

        public string _X0
        {
            get => _x0;
            set => SetProperty(ref _x0, value);
        }
        public string _X1
        {
            get => _x1;
            set => SetProperty(ref _x1, value);
        }
        public string _X2
        {
            get => _x2;
            set => SetProperty(ref _x2, value);
        }
        public string _X3
        {
            get => _x3;
            set => SetProperty(ref _x3, value);
        }
        public string _X4
        {
            get => _x4;
            set => SetProperty(ref _x4, value);
        }
        public string _X5
        {
            get => _x5;
            set => SetProperty(ref _x5, value);
        }
        public string _X6
        {
            get => _x6;
            set => SetProperty(ref _x6, value);
        }
        public string _X7
        {
            get => _x7;
            set => SetProperty(ref _x7, value);
        }
        public string _Y0
        {
            get => _y0;
            set => SetProperty(ref _y0, value);
        }
        public string _Y1
        {
            get => _y1;
            set => SetProperty(ref _y1, value);
        }
        public string _Y2
        {
            get => _y2;
            set => SetProperty(ref _y2, value);
        }
        public string _Y3
        {
            get => _y3;
            set => SetProperty(ref _y3, value);
        }
        public string _Y4
        {
            get => _y4;
            set => SetProperty(ref _y4, value);
        }
        public string _Y5
        {
            get => _y5;
            set => SetProperty(ref _y5, value);
        }
        public string _Y6
        {
            get => _y6;
            set => SetProperty(ref _y6, value);
        }
        public string _Y7
        {
            get => _y7;
            set => SetProperty(ref _y7, value);
        }
        #endregion properties

        #region commands
        public Command StartCommand { get; }
        public Command EndCommand { get; }
        public Command LoadLog { get; }
        #endregion commands

        readonly ILocationConsent locationConsent;

        public MainPageViewModel()
        {
            locationConsent = DependencyService.Get<ILocationConsent>();
            StartCommand = new Command(() => OnStartClick());
            EndCommand = new Command(() => OnStopClick());
            LoadLog = new Command(() => OnLoadLog());
            HandleReceivedMessages();
            locationConsent.GetLocationConsent();
            StartEnabled = true;
            StopEnabled = false;
            ValidateStatus();
        }

        public void OnLoadLog()
        {        
        }

        public void OnStartClick()
        {
            Start();
        }

        public void OnStopClick()
        {
            var message = new StopServiceMessage();
            MessagingCenter.Send(message, "ServiceStopped");
            UserMessage = "Location Service has been stopped!";
            SecureStorage.SetAsync(Constants.SERVICE_STATUS_KEY, "0");
            StartEnabled = true;
            StopEnabled = false;
        }

        void ValidateStatus() 
        {
            var status = SecureStorage.GetAsync(Constants.SERVICE_STATUS_KEY).Result;
            if (status != null && status.Equals("1")) 
            {
                Start();
            }
        }

        void Start() 
        {
            var message = new StartServiceMessage();
            MessagingCenter.Send(message, "ServiceStarted");
            UserMessage = "GPS p2p service (perc01an) has started";
            SecureStorage.SetAsync(Constants.SERVICE_STATUS_KEY, "1");
            StartEnabled = false;
            StopEnabled = true;
        }

        void HandleReceivedMessages()
        {
            MessagingCenter.Subscribe<LocationMessage>(this, "Location", message => {
                Device.BeginInvokeOnMainThread(() => {
                    Latitude = message.Latitude;
                    Longitude = message.Longitude;
                    Ssid = message.Ssid;
                    SsidNeighZero = message.SsidNeighZero;
                    SsidNeighOne = message.SsidNeighOne;
                    SsidNeighTwo = message.SsidNeighTwo;
                    SsidNeighThree = message.SsidNeighThree;
                    SsidNeighFour = message.SsidNeighFour;
                    SsidNeighFive = message.SsidNeighFive;
                    SsidNeighSix = message.SsidNeighSix;
                    SsidNeighSeven = message.SsidNeighSeven;
                    UserMessage = "Location Updated";
                    Scanning = message.Scanning;
                    X0 = message.X0;
                    X1 = message.X1;
                    X2 = message.X2;
                    X3 = message.X3;
                    X4 = message.X4;
                    X5 = message.X5;
                    X6 = message.X6;
                    X7 = message.X7;
                    Y0 = message.Y0;
                    Y1 = message.Y1;
                    Y2 = message.Y2;
                    Y3 = message.Y3;
                    Y4 = message.Y4;
                    Y5 = message.Y5;
                    Y6 = message.Y6;
                    Y7 = message.Y7;
                    _X0 = message._X0;
                    _X1 = message._X1;
                    _X2 = message._X2;
                    _X3 = message._X3;
                    _X4 = message._X4;
                    _X5 = message._X5;
                    _X6 = message._X6;
                    _X7 = message._X7;
                    _Y0 = message._Y0;
                    _Y1 = message._Y1;
                    _Y2 = message._Y2;
                    _Y3 = message._Y3;
                    _Y4 = message._Y4;
                    _Y5 = message._Y5;
                    _Y6 = message._Y6;
                    _Y7 = message._Y7;
                });
            });
            MessagingCenter.Subscribe<StopServiceMessage>(this, "ServiceStopped", message => {
                Device.BeginInvokeOnMainThread(() => {
                    UserMessage = "Location Service has been stopped!";
                });
            });
            MessagingCenter.Subscribe<LocationErrorMessage>(this, "LocationError", message => {
                Device.BeginInvokeOnMainThread(() => {
                    UserMessage = "There was an error updating location!";
                });
            });
        }
    }
}
