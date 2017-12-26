using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using Xamarin.Forms.GoogleMaps;
namespace App7
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPage1Detail : ContentPage
    {
        readonly Pin _pinTokyo = new Pin()
        {
            Type = PinType.Place,
            Label = "Tokyo SKYTREE",
            Address = "Sumida-ku, Tokyo, Japan",
            Position = new Position(35.71d, 139.81d)
        };
        public MasterDetailPage1Detail()
        {
            InitializeComponent();
            MasterDetailPage1._show += MasterDetailPage1__show;
            Page0.IsVisible = true;
            Page1.IsVisible = false;
            Page2.IsVisible = false;
            Page3.IsVisible = false;
            Page4.IsVisible = false;
            Page5.IsVisible = false;
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
                Email.Opacity = 0;
                Number.Opacity = 0;
                Address.Opacity = 0;
                City.Opacity = 0;

                await FullName.FadeTo(1, 300);
                await Email.FadeTo(1, 300);
                await Number.FadeTo(1, 300);
                await Address.FadeTo(1, 300);
                await City.FadeTo(1, 300);


                //  await CrossTextToSpeech.Current.Speak("Text to speak");
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
     
    }


}