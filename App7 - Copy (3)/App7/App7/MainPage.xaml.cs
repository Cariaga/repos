using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.GoogleMaps;
using System.Net;
using Plugin.Connectivity;
using System.Diagnostics;
using Plugin.DeviceInfo;
using Plugin.Vibrate;

namespace App7
{
    public class Validator
    {
        public bool IsEmailUsed()
        {
            return true;
        }
        public bool IsUserNameUsed()
        {
            return true;
        }
        public bool IsValidPassword()
        {
            return true;
        }
        public bool IsAccountActivated()
        {
            return true;
        }
        public bool IsNumberUsed()
        {
            return true;
        }
        public bool IsValidName()
        {
            return true;
        }
       
    }
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        //    CrossLocalNotifications.Current.Show("title", "body", 101, DateTime.Now.AddSeconds(5));
            Debug.WriteLine(CrossDeviceInfo.Current.Model);
            Debug.WriteLine(CrossDeviceInfo.Current.Platform);
            Debug.WriteLine(CrossDeviceInfo.Current.Version);

            var v = CrossVibrate.Current;
            v.Vibration(TimeSpan.FromMilliseconds(10)); // very tiny vibration
         /*   Device.BeginInvokeOnMainThread(async () =>
            {
                await CrossTextToSpeech.Current.Speak("Text to speak");
            });*/
        }

        

        private void SignUpButton_Clicked(object sender, EventArgs e)
        {

        }

        private void SignInButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new MasterDetailPage1());
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
    }
}
