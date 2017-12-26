using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.GoogleMaps;
using System.Net;

namespace App7
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

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
        readonly Pin _pinTokyo = new Pin()
        {
            Type = PinType.Place,
            Label = "Tokyo SKYTREE",
            Address = "Sumida-ku, Tokyo, Japan",
            Position = new Position(35.71d, 139.81d)
        };
    }
}
