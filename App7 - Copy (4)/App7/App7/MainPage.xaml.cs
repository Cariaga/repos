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

namespace App7
{
    public class Browser
    {
        public async Task<string> Request(string url ="")
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
    public static class Validator
    {
        public static bool IsEmailUsed(string Email="")
        {
            return true;
        }
        public static bool IsUserNameUsed(string UserName = "")
        {
            return true;
        }
        public static bool IsValidPassword(string Pass = "")
        {
            return true;
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
        public static bool IsValidPhoneNumber(string PhoneNumber = "")
        {
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

        //--Connection Alerts
        public static string IsInvalidInternet()
        {
            return "No Network Connection";
        }
        public static string IsInvalidGPS()
        {
            return "No GPS Connection";
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
        public ObservableCollection<VeggieViewModel> veggies { get; set; }
        public MainPage()
        {
            InitializeComponent();
            DebugEx.DebugLine();
            Task.Run( () => {
              var x= new Browser().Request("http://jsonplaceholder.typicode.com/photos");
              
                Debug.WriteLine(x.Result);

            });
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
            veggies = new ObservableCollection<VeggieViewModel>();
           
            lstView.ItemTemplate = new DataTemplate(typeof(CustomVeggieCell));
            veggies.Add(new VeggieViewModel { Name = "Tomato", Type = "Fruit", Image = "icon.png" });
            veggies.Add(new VeggieViewModel { Name = "Romaine Lettuce", Type = "Vegetable", Image = "icon.png" });
            veggies.Add(new VeggieViewModel { Name = "Zucchini", Type = "Vegetable", Image = "icon.png" });
            lstView.ItemsSource = veggies;
           
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
                var horizontalLayout = new StackLayout() { BackgroundColor = Color.Olive };

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
                verticaLayout.Children.Add(nameLabel);
                verticaLayout.Children.Add(typeLabel);
                horizontalLayout.Children.Add(verticaLayout);
                horizontalLayout.Children.Add(image);

                // add to parent view
                View = horizontalLayout;
            }
        }
    }
}
