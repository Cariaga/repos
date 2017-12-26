using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;
using Plugin.Battery.Abstractions;
using Plugin.Battery;
using Plugin.Connectivity;
using Plugin.Vibrate;
using Plugin.TextToSpeech;
using Plugin.DeviceInfo;
using Plugin.LocalNotifications;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace App11
{
     public class CustomMap : Map
     {
         public List<CustomPin> CustomPins { get; set; }
     }
     public class CustomPin : Pin
     {
         public string Id { get; set; }
         public string Url { get; set; }
     }

    [XamlCompilation(XamlCompilationOptions.Compile)]
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
            // CrossLocalNotifications.Current.Show("title", "body", 101, DateTime.Now.AddSeconds(5));//works


            //   toasted();//works


            /*  var map = new Map(
              MapSpan.FromCenterAndRadius(
                      new Xamarin.Forms.Maps.Position(37, -122), Distance.FromMiles(0.3)))
               {
                   IsShowingUser = true,
                   HeightRequest = 100,
                   WidthRequest = 960,
                   VerticalOptions = LayoutOptions.FillAndExpand
               };
               var stack = new StackLayout { Spacing = 0 };
               stack.Children.Add(map);
               Content = stack;*/

            //assign the point
            /* var position = new Xamarin.Forms.Maps.Position(37, -122); // Latitude, Longitude
             var pin = new Pin
             {
                 Type = PinType.Place,
                 Position = position,
                 Label = "custom pin",
                 Address = "custom detail info"

             };*/
            //  MyMap.Pins.Add(pin);

            //works moving to region
            //MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(37, -122), Distance.FromMiles(1)));


            /*
            var pin2 = new CustomPin
            {
                Type = PinType.Place,
                Position = new Xamarin.Forms.Maps.Position(37.79752, -122.40183),
                Label = "Xamarin San Francisco Office",
                Address = "394 Pacific Ave, San Francisco CA",
                Id = "Xamarin",
                Url = "http://xamarin.com/about/"
            };*/
            /*var mapView = new TKCustomMap();
             List<TKCustomMapPin> pins = new List<TKCustomMapPin>();

             var pinx = new TKCustomMapPin();
             pinx.Position = new Xamarin.Forms.Maps.Position(37.79752, -122.40183);
             pinx.Title = "Somthing";


             mapView.CustomPins = pins;*/


            //custom map
            /* List<Xamarin.Forms.Maps.Position> pos = new List<Xamarin.Forms.Maps.Position> { new Xamarin.Forms.Maps.Position(39.939889, 116.423493), new Xamarin.Forms.Maps.Position(39.930622, 116.423924), new Xamarin.Forms.Maps.Position(39.930733, 116.441135), new Xamarin.Forms.Maps.Position(39.939944, 116.44056) };
             List<Xamarin.Forms.Maps.Position> posi = new List<Xamarin.Forms.Maps.Position> { new Xamarin.Forms.Maps.Position(39.934633, 116.399921), new Xamarin.Forms.Maps.Position(39.929709, 116.400208), new Xamarin.Forms.Maps.Position(39.929792, 116.405994), new Xamarin.Forms.Maps.Position(39.934689, 116.405526) };
             customMap.ShapeCoordinates.Add(pos);
             customMap.ShapeCoordinates.Add(posi);
             customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(39.934689, 116.405526), Distance.FromMiles(1.5)));
             */


            /*
            Xamarin.Forms.GoogleMaps.Pin _pinTokyo2 = new Xamarin.Forms.GoogleMaps.Pin()
            {
                Icon = BitmapDescriptorFactory.DefaultMarker(Color.Gray),
                Type = Xamarin.Forms.GoogleMaps.PinType.Place,
                Label = "Second Pin",
                Position = new Xamarin.Forms.GoogleMaps.Position(35.71d, 139.815d),
                ZIndex = 5
            };
            _pinTokyo2.Icon = BitmapDescriptorFactory.FromBundle("icon.png");*/

            //    MyMap.MyLocationEnabled = true;
            //  MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.GoogleMaps.Position(37, -122), Distance.FromMiles(1)));
           /* var customMap = new CustomMap
            {
                MapType = MapType.Street,
                WidthRequest =200,
                HeightRequest = 200
            };
           

            var pin = new CustomPin
            {
                Type = PinType.Place,
                Position = new Position(37.79752, -122.40183),
                Label = "Xamarin San Francisco Office",
                Address = "394 Pacific Ave, San Francisco CA",
                Id = "Xamarin",
                Url = "http://xamarin.com/about/"
            };
            customMap.CustomPins = new List<CustomPin> { pin };
            customMap.Pins.Add(pin);
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(
              new Position(37.79752, -122.40183), Distance.FromMiles(1.0)));
            Content = customMap;*/
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

        public Plugin.Geolocator.Abstractions.Position position { get; private set; }

        public void CheckLocation()
        {
            position = new Plugin.Geolocator.Abstractions.Position();
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
