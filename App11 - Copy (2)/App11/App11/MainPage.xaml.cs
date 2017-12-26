using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;
using Plugin.Geolocator.Abstractions;
using Plugin.Battery.Abstractions;
using Plugin.Battery;
using Plugin.Connectivity;
using Plugin.Vibrate;
using Plugin.TextToSpeech;
using Plugin.DeviceInfo;
using Plugin.LocalNotifications;


namespace App11
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Update();//works
      
            //works
            var battery = CrossBattery.Current;
            battery.BatteryChanged += Battery_BatteryChanged;
            Debug.WriteLine(battery.Status.ToString());

            Debug.WriteLine(DoIHaveInternet());//works

            var v = CrossVibrate.Current;
            v.Vibration(TimeSpan.FromSeconds(1)); // 1 second vibration
            Device.BeginInvokeOnMainThread(async () =>
            {
                await CrossTextToSpeech.Current.Speak("Text to speak");
            });

            Debug.WriteLine(CrossDeviceInfo.Current.Model);
            Debug.WriteLine(CrossDeviceInfo.Current.Platform);
            Debug.WriteLine(CrossDeviceInfo.Current.Version);
            CrossLocalNotifications.Current.Show("title", "body", 101, DateTime.Now.AddSeconds(5));//works


            toasted();
        }

        void toasted()
        {
            Device.BeginInvokeOnMainThread(async () => {

                var action = await DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, "Email", "Twitter", "Facebook");
            });
          
        }

        public bool DoIHaveInternet()
        {
            if (!CrossConnectivity.IsSupported)
                return true;

            //Do this only if you need to and aren't listening to any other events as they will not fire.
            var connectivity = CrossConnectivity.Current;

            try
            {
                return connectivity.IsConnected;
            }
            finally
            {
                CrossConnectivity.Dispose();
            }

        }


        private void Battery_BatteryChanged(object sender, BatteryChangedEventArgs e)
        {
            Debug.WriteLine(e.Status.ToString());
        }

        void Update()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Task.Run(async () =>
                {
                    await Task.Delay(1000);
                    CheckLocation();
                    // do something with time...
                });
                return true;
            });
        }

        public Position position { get; private set; }

        public void CheckLocation()
        {
            position = new Position();
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            Task.Run(async () => {
                 position = await locator.GetPositionAsync(TimeSpan.FromMilliseconds(1000));
                Debug.WriteLine(position.Latitude);
            });
        }

        private void login_Clicked(object sender, EventArgs e)
        {
            //   await Navigation.PushModalAsync(new MasterDetailPage1());

            PopUp();
            

        }
        void PopUp()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                    var answer = await DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");
                    LocationText.Text = position.Latitude.ToString() + " : " + position.Longitude.ToString();//works
            });
        }
        void OpenPage()
        {
            Application.Current.MainPage = new NavigationPage(new MasterDetailPage1());
        }
    }
}
