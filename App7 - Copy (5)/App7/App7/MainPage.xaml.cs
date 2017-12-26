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
using System.Collections.ObjectModel;
using App7.ViewModels;
using System.Net.Http;

using System.Text.RegularExpressions;
using Plugin.Geolocator;
using Plugin.Permissions.Abstractions;
using Plugin.Permissions;
using Plugin.Geolocator.Abstractions;
using Xamarin.Auth;
using Xamarin.Utilities;
using Xamarin.Auth.Presenters;

/*
* 
*/

namespace App7
{
    public class Browser
    {
        public async Task<string> Request(string url = "")
        {
            using (var httpClient = new HttpClient())
            using (var httpResonse = await httpClient.GetAsync(url))
            {
                Debug.WriteLine(httpResonse.Content.ReadAsStringAsync().Result);
                if (httpResonse.IsSuccessStatusCode)
                {
                    return await httpResonse.Content.ReadAsStringAsync();
                }
                else
                {
                    return "Error from : " + url;
                }
            }
        }

    }
    //MUST BE Single Instance
    public static class GPSLocator{
        public static bool IsGeoLocationAvilable { get; private set; }
        public static bool IsGeoLocationEnabled { get; private set; }
        public static Plugin.Geolocator.Abstractions.Position ResultPosition { get; private set; }
        public static async Task<bool> UpdateLocation()
        {
          
           var x= Task.Run(async () => {

                try
                {
                    var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                    if (status != PermissionStatus.Granted)
                    {
                        if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                        {
                            await Application.Current.MainPage.DisplayAlert("Need location", "Will not work ", "OK");
                        }

                        var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                        //Best practice to always check that the key exists
                        if (results.ContainsKey(Permission.Location))
                            status = results[Permission.Location];
                    }

                    if (status == PermissionStatus.Granted)
                    {
                        Debug.WriteLine("GPS IS GRANTED");
                       ResultPosition = await CrossGeolocator.Current.GetPositionAsync(TimeSpan.FromSeconds(1));//must be lower than the repeat rate that is calling it
                       IsGeoLocationAvilable =  CrossGeolocator.Current.IsGeolocationAvailable;
                        IsGeoLocationEnabled =  CrossGeolocator.Current.IsGeolocationEnabled;
                     
                     
                    }
                    else if (status != PermissionStatus.Unknown)
                    {
                        await Application.Current.MainPage.DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                    }
                }
                catch (Exception ex)
                {
                }
            });
           var y = await x.ContinueWith(f =>
            {
                if (ResultPosition.Longitude == 0 && ResultPosition.Latitude == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }).ConfigureAwait(false);

            return y;
           
        }
    }
    public static class ValidatorConnection
    {
        public static bool IsValidateNetwork()
        {
            bool isValid = true;
            Device.BeginInvokeOnMainThread(async () =>
            {

                if (!CrossConnectivity.Current.IsConnected)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "WIFI NOT CONNECTED!", "OK");
                    isValid = false;
                }
                if (!CrossGeolocator.Current.IsGeolocationEnabled || !CrossGeolocator.Current.IsGeolocationAvailable)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "GPS NOT CONNECTED!", "OK");
                    isValid = false;
                }
            });
            return isValid;
        }
        
    }
    public static class Validator
    {
        public static bool IsEmailUsed(string Email="")
        {
            return true;
        }
        public static bool IsEmailValid(string Email="")
        {
          return Regex.IsMatch(Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
           
        }
        public static bool IsUserNameUsed(string UserName = "")
        {
            return true;
        }

        /// <summary>
        /// Minimum eight characters, Has number and uppercase
        /// </summary>
        /// <param name="Pass"></param>
        /// <returns></returns>
        public static bool IsValidPassword(string Pass = "")
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");
            return hasNumber.IsMatch(Pass) && hasUpperChar.IsMatch(Pass) && hasMinimum8Chars.IsMatch(Pass);
        }
        public static bool IsAccountActivated(string UserName = "")
        {
            return true;
        }
        public static bool IsNumberUsed(string Number = "")
        {
            return true;
        }
        public static bool IsValidName(string Name = "")
        {
            return true;
        }
        private static string[] Phone_Patterns = new string[] {
   @"^[0-9]{11}$",//with the zero
   @"^[0-9]{10}$",//without the zero
 //  @"^\+[0-9]{2}\s+[0-9]{2}[0-9]{8}$",//maybe useful
 //  @"^[0-9]{3}-[0-9]{4}-[0-9]{4}$",
 };
        private static string PhonePattern()
        {
            return string.Join("|", Phone_Patterns
              .Select(item => "(" + item + ")"));
        }
        public static bool IsValidPhoneNumber(string PhoneNumber = "")
        {
            /*
     "09088534816"//yes
     "9088534816",//yes
     "+91 33 40653155",//yes
     "033-2647-0969",//yes
     "123", //no
     "12115351689385",//no*/
            var result = string.Join(Environment.NewLine,$"{PhoneNumber,18} {(Regex.IsMatch(PhoneNumber, PhonePattern()) ? "yes" : "no"),3}");
            Debug.WriteLine(result);
            return true;
        }
        public static bool IsAccountExist(string UserName="",string Password="")
        {
            return true;
        }
    }
    public static class Alerts
    {

        //--Input Alerts
        public static string IsInvalidAccountExistAlert()
        {
            return "Account Does Not Exist";
        }
        public static string IsInvalidAccountActivatedAlert()
        {
            return "Account Not Activated";
        }
        public static string IsInvalidEmailAlreadyUsed()
        {
            return "Email Already Used";
        }
        public static string IsInvalidEmailFormat()
        {
            return "Invalid Email Format Not Allowed";
        }
        public static string IsInvalidName()
        {
            return "Invalid Input No Special Characters";
        }
        public static string IsInvalidPassword()
        {
            return "Invalid Password Must Longer Than 5 Characters And Must Contain Numbers and Letters";
        }
        public static string IsInvalidUserName()
        {
            return "Invalid UserName Must Longer Than 5 Characters And Must Contain Numbers and Letters";
        }
 
        //--Updates Alerts
        public static string IsUpdatedAccount()
        {
            return "Account Updated";
        }
        public static string IsSettingsUpdated()
        {
            return "Account Settings Updated";
        }

    }
    public static class DebugEx
    {
        public static void DebugLine() => Debug.WriteLine("-----------");
    }
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class MainPage : ContentPage
    {
    

        public MainPage()
        {
            InitializeComponent();

           

            var result=ValidatorConnection.IsValidateNetwork();
            
          




            DebugEx.DebugLine();

            Validator.IsValidPhoneNumber("09026487141");
            Debug.WriteLine("Valid Password :" + Validator.IsValidPassword("eastcoast123"));

            /* Task.Run( () => {
               var x= new Browser().Request("http://jsonplaceholder.typicode.com/photos");

                 Debug.WriteLine(x.Result);

             });*/
            DebugEx.DebugLine();



            //    CrossLocalNotifications.Current.Show("title", "body", 101, DateTime.Now.AddSeconds(5));
            Debug.WriteLine(CrossDeviceInfo.Current.Model);
            Debug.WriteLine(CrossDeviceInfo.Current.Platform);
            Debug.WriteLine(CrossDeviceInfo.Current.Version);

            var v = CrossVibrate.Current;
            v.Vibration(TimeSpan.FromMilliseconds(10)); // very tiny vibration
            Device.BeginInvokeOnMainThread(async () =>
            {

                Username.Opacity = 0;
                Password.Opacity = 0;
                SignInButton.Opacity = 0;
                SignUpButton.Opacity = 0;
                await Username.FadeTo(1, 500);
                await Password.FadeTo(1, 500);
                await SignInButton.FadeTo(1, 500);
                await SignUpButton.FadeTo(1, 500);

                //  await CrossTextToSpeech.Current.Speak("Text to speak");
            });
           


            /*  Task.Run(async () =>
               {
                   var locator = CrossGeolocator.Current;
                   locator.DesiredAccuracy = 20;



                   /*   if (locator.IsListening)
                      return;*/


            ///This logic will run on the background automatically on iOS, however for Android and UWP you must put logic in background services. Else if your app is killed the location updates will be killed.
           /* await locator.StartListeningAsync(TimeSpan.FromSeconds(2), 2, true, new Plugin.Geolocator.Abstractions.ListenerSettings
                    {
                        ActivityType = Plugin.Geolocator.Abstractions.ActivityType.AutomotiveNavigation,
                        AllowBackgroundUpdates = true,
                        DeferLocationUpdates = true,
                        DeferralDistanceMeters = 1,

                        DeferralTime = TimeSpan.FromSeconds(1),
                        ListenForSignificantChanges = true,
                        PauseLocationUpdatesAutomatically = false

                    });

                var x = await locator.GetPositionAsync(TimeSpan.FromSeconds(1), null, true);
                Debug.WriteLine(x.Latitude + " " + x.Longitude);

                locator.PositionChanged += Locator_PositionChanged;
            }).ConfigureAwait(false);
            */
        }
     

        private void SignUpButton_Clicked(object sender, EventArgs e)
        {
            
           Device.BeginInvokeOnMainThread(async () =>
            {
                SignUpButton.Opacity = 0;
                await SignUpButton.FadeTo(1, 2000,Easing.BounceIn);
                //  await CrossTextToSpeech.Current.Speak("Text to speak");
            });

            Application.Current.MainPage = new NavigationPage(new SignUp());
        }

        private void SignInButton_Clicked(object sender, EventArgs e)
        {
          

            if (Validator.IsAccountExist("",""))
            {
                if (Validator.IsAccountActivated(""))
                {
                    Application.Current.MainPage = new NavigationPage(new MasterDetailPage1());
                }
                else
                {
                   Alerts.IsInvalidAccountActivatedAlert();

                }
            }
            else
            {
               Alerts.IsInvalidAccountExistAlert();
            }
          
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

        public class CustomVeggieCell : ViewCell
        {
            public CustomVeggieCell()
            {
                //instantiate each of our views
                var image = new Image();
                var nameLabel = new Label();
                var typeLabel = new Label();
                var verticaLayout = new StackLayout();
                var horizontalLayout = new StackLayout() {};
                horizontalLayout.Padding = new Thickness(10,0,0,0);
                
                //set bindings
                nameLabel.SetBinding(Label.TextProperty, new Binding("Name"));
                typeLabel.SetBinding(Label.TextProperty, new Binding("Type"));
                image.SetBinding(Image.SourceProperty, new Binding("Image"));

                //Set properties for desired design
                horizontalLayout.Orientation = StackOrientation.Horizontal;
                horizontalLayout.HorizontalOptions = LayoutOptions.Fill;
                image.HorizontalOptions = LayoutOptions.End;
               
                nameLabel.FontSize = 24;

                //add views to the view hierarchy
                horizontalLayout.Children.Add(image);
                verticaLayout.Children.Add(nameLabel);
                verticaLayout.Children.Add(typeLabel);
                horizontalLayout.Children.Add(verticaLayout);
               

                // add to parent view
                View = horizontalLayout;
            }
        }

        private void FacebookLogin_Clicked(object sender, EventArgs e)
        {
            Debug.WriteLine("LoginFacebook");
            Device.BeginInvokeOnMainThread(() =>{
                Task.Run(async () => {

                    await Navigation.PopAsync();
                }).ConfigureAwait(false);
            });
            

            Application.Current.MainPage = App.FacebookLoginPage();
     
            //custom dependancy so i can access the native from pcl
            // DependencyService.Get<IForm2Native>().Form2native();//not used might be useful for form to native comunication

        }
    }
}
