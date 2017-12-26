using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using Xamarin.Forms.GoogleMaps;
using System.Collections.ObjectModel;
using App7.ViewModels;
using static App7.MainPage;
using System.Net;
using System.IO;
using Xamarin.Forms.Internals;
using Plugin.SecureStorage;

namespace App7
{
    public enum Category
    {
        CarRide,BikeRide,Tow,CarRepair,BikeRepair,HouseClean,HousePlumb,Grocery,MedicineSend,ItemSend,FoodSend
    }
    public static class GlobalSettings{
        private static MapType mapType = MapType.Street;//default
        private static string selectedCategorySearch = "Car";

        

        public static MapType MapType { get => mapType; set => mapType = value; }
    }


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPage1Detail : ContentPage
    {

        public ObservableCollection<TaskModel> TaskList { get; set; }
   
        readonly Pin _pinTokyo = new Pin()
        {
            Type = PinType.Place,
            Label = "Tokyo SKYTREE",
            Address = "Sumida-ku, Tokyo, Japan",
            Position = new Position(35.71d, 139.81d)
        };
        protected override void OnDisappearing()
        {
            Debug.WriteLine("Disapeared");
            //we need to unsubscribe //on master detail page switch// Warning its un tested on mutli pages
            MasterDetailPage1._show -= MasterDetailPage1__show;
            MasterDetailPage1.OnGPSUpdate -= MasterDetailPage1Detail_OnGPSUpdate;

            base.OnDisappearing();
        }
        public MasterDetailPage1Detail()
        {
            InitializeComponent();

           

            MasterDetailPage1._show += MasterDetailPage1__show;

           var MapType= CrossSecureStorage.Current.GetValue("MapType");
            if (MapType=="Hybrid")
            {
                Hybrid.IsToggled = true;
                GlobalSettings.MapType = Xamarin.Forms.GoogleMaps.MapType.Hybrid;
            }
            else if(MapType == "Street")
            {
                Hybrid.IsToggled = false;
                GlobalSettings.MapType = Xamarin.Forms.GoogleMaps.MapType.Street;
            }



            Page0.IsVisible = true;
            Page1.IsVisible = false;
            Page2.IsVisible = false;
            Page3.IsVisible = false;
            Page4.IsVisible = false;
            Page5.IsVisible = false;

            map.MapType = GlobalSettings.MapType;

            _pinTokyo.Icon = BitmapDescriptorFactory.FromBundle("icon.png");
            //circle example
            var circle1 = new Circle();
            circle1.StrokeWidth = 10f;
            circle1.StrokeColor = Color.Red;
            circle1.FillColor = new Color(1, 0, 0, 0.5F);
            circle1.Center = new Position(35.71d, 139.81d);
            circle1.Radius = Distance.FromKilometers(8);
            map.Circles.Add(circle1);
            map.Pins.Add(_pinTokyo);
            
           
            Start();

            Debug.WriteLine("Started");
        }
     
      //  private static bool AlreadyAdded;
        void Start()//this get call multiple times each time a user selects a new item from master detail
        {

           
             MasterDetailPage1.OnGPSUpdate += MasterDetailPage1Detail_OnGPSUpdate;
            
            


            //--user set up
                Device.BeginInvokeOnMainThread(async () => {
                    try
                    {
                        if (UserInfo.Instance!=null)
                        {
                            var userInfo = UserInfo.Instance.UserInfoModel;
                            if (userInfo != null && userInfo.picture != null && userInfo.cover != null)
                            {
                                FullName.Text = userInfo.first_name + " " + userInfo.last_name;
                                //Email.Text = "";
                                Gender.Text = userInfo.gender;
                                Age.Text = userInfo.age_range.min.ToString();
                                Link.GestureRecognizers.Add(new TapGestureRecognizer()
                                {
                                    Command = new Command(() =>
                                    {
                                        Link.TextColor = Color.DarkBlue;
                                        Device.OpenUri(new Uri(userInfo.link));
                                    })
                                });
                                CoverImage.Source = userInfo.cover.source;                             
                                ProfilePicture.Source = userInfo.picture.data.url;
                                ProfilePicture.GestureRecognizers.Add(new TapGestureRecognizer()
                                {
                                    Command = new Command(() =>
                                    {

                                        Device.OpenUri(new Uri(userInfo.link));
                                    })
                                });
                            }
                        }
                    }
                    catch(Exception e)
                    {

                    }
                });
           //user set up

            //--set map camera position
            Device.BeginInvokeOnMainThread(() => {
                Task.Run(async () => {
                    var t = await GPSLocator.UpdateLocation();
                    var pos = GPSLocator.ResultPosition;
                }).ConfigureAwait(false);
            });
            //--set map camera position
                 Task.Run(async () =>
                 {
                     var t = await GPSLocator.UpdateLocation();
                     await Task.Delay(5000);//delay so map has time to load
                     Device.BeginInvokeOnMainThread(async () =>
                     {
                      await map.AnimateCamera(CameraUpdateFactory.NewCameraPosition(new CameraPosition(
                      new Position(GPSLocator.ResultPosition.Latitude, GPSLocator.ResultPosition.Longitude),
                      17d, // zoom
                      0d, // bearing(rotation)
                      45d)), // tilt
                  TimeSpan.FromMilliseconds(100));
                     });
                 }).ConfigureAwait(false);


            //--for the dashboard
            TaskList = new ObservableCollection<TaskModel>();

            ListViewTask.ItemTemplate = new DataTemplate(typeof(CustomVeggieCell));

           
        

          /*  for(var i = 0; i < 500; i++)
            {
                TaskList.Add(new TaskModel { Image = "icon.png", Name = "I need delivery", Type = "somthing somthing" });
                TaskList.Add(new TaskModel { Image = "icon.png", Name = "I need someone to pick up", Type = "somthing somthing ", });
                TaskList.Add(new TaskModel { Image = "icon.png", Name = "I require house keeping", Type = "somthing somthing" });
            }*/
          


            ListViewTask.ItemsSource = TaskList;
            ListViewTask.ItemSelected += ListViewTask_ItemSelected;
            //--
        }

        private void ListViewTask_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Debug.WriteLine((e.SelectedItem as TaskModel).Name);
            var page = new NavigationPage(new TaskHire((e.SelectedItem as TaskModel)));
            Device.BeginInvokeOnMainThread(async () => {
                await Navigation.PushAsync(page);
            });
        }
        private void MasterDetailPage1Detail_OnGPSUpdate(Plugin.Geolocator.Abstractions.Position position)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {

                var str = "Longitude " + position.Longitude + " " + position.Latitude;
                Debug.WriteLine(str);
                Debugposition.Text = str;


                //works use this for constantly updating position
             /*   await map.AnimateCamera(CameraUpdateFactory.NewCameraPosition(
                new CameraPosition(
                    new Position(position.Latitude, position.Longitude),
                    17d, // zoom
                    0d, // bearing(rotation)
                    0d)), // tilt
                TimeSpan.FromSeconds(2));*/

                // LocationAvilableText.Text ="Avilable: "+ GPSLocator.IsGeoLocationAvilable.ToString();
                //LocationEnableText.Text = "Enable: " + GPSLocator.IsGeoLocationEnabled.ToString();
            });
        }

        void MapUpdate()
        {

        }
        void DisableAllPage()
        {
            Page0.IsVisible = false;
            Page1.IsVisible = false;
            Page2.IsVisible = false;
            Page3.IsVisible = false;
            Page4.IsVisible = false;
            Page5.IsVisible = false;
        }
        void AnimatePage1()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {

                FullName.Opacity = 0;
                //Email.Opacity = 0;
                Age.Opacity = 0;
                Link.Opacity = 0;
                Gender.Opacity = 0;

                await FullName.FadeTo(1, 300);
               // await Email.FadeTo(1, 300);
                await Age.FadeTo(1, 300);
                await Gender.FadeTo(1, 300);
                await Link.FadeTo(1, 300);

                //  await CrossTextToSpeech.Current.Speak("Text to speak");
            });
        }

        void AnimatePage2()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                SnackBar.TranslationY = 50;
                await Task.Delay(500);
                await SnackBar.TranslateTo(0, 0,1000,Easing.CubicInOut);
            });
        }

        private void MasterDetailPage1__show(int PageID)
        {
            DisableAllPage();
            if (PageID==0)
            {
                Page0.IsVisible = true;
            }
            if (PageID == 1)
            {
                Page1.IsVisible = true;
                AnimatePage1();
            }
            if (PageID == 2)
            {
                Page2.IsVisible = true;
                AnimatePage2();
            }
            if (PageID == 3)
            {
                Page3.IsVisible = true;
            }
            if (PageID == 4)
            {
                Page4.IsVisible = true;
            }
            if (PageID == 5)
            {
                Page5.IsVisible = true;
            }

        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private void Logout_Clicked(object sender, EventArgs e)
        {
            //custom dependancy so i can access the native from pcl
            var closer = DependencyService.Get<IForm2Native>();
            if (closer != null)
                closer.Form2native();
        }

        private void Hybrid_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
        }

        private void Hybrid_Toggled(object sender, ToggledEventArgs e)
        {
            if (Hybrid.IsToggled)
            {
                GlobalSettings.MapType = MapType.Hybrid;
                CrossSecureStorage.Current.SetValue("MapType", "Hybrid");
            }
            else
            {
                GlobalSettings.MapType = MapType.Street;
                CrossSecureStorage.Current.SetValue("MapType", "Street");
            }
        }

        private void CarCategory_Clicked(object sender, EventArgs e)
        {
            Debug.WriteLine("CarRide");
        }
        private void BikeCategory_Clicked(object sender, EventArgs e)
        {
            Debug.WriteLine("BikeRide");
        }

        private void HouseCategory_Clicked(object sender, EventArgs e)
        {
            Debug.WriteLine("HouseCleaning");
        }

        private void DeliveryCategory_Clicked(object sender, EventArgs e)
        {
            Debug.WriteLine("Delivery");
        }

        private void CartCategory_Clicked(object sender, EventArgs e)
        {
            Debug.WriteLine("Grocery");
        }

       
    }


}