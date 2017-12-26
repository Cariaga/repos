using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TK.CustomMap;
using TK.CustomMap.Overlays;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace App6
{

    public class RoutePin : TKCustomMapPin
    {
        /// <summary>
        /// Gets/Sets if the pin is the source of a route. If <value>false</value> pin is destination
        /// </summary>
        public bool IsSource { get; set; }
        /// <summary>
        /// Gets/Sets reference to the route
        /// </summary>
        public TKRoute Route { get; set; }
    }
    public class MyMap : TKCustomMap
    {
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
         
        }

        private void clicked_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() => {


                var x = new ObservableCollection<TKCustomMapPin>();
            x.Clear();

            for (var i = 0; i<50;i++)
            {
                var f = new TKCustomMapPin();
                f.Position = new Position(37+i, -122);
                f.Title = "asfgasg";
                f.IsVisible = true;
                f.DefaultPinColor = Color.AliceBlue;
               
            //    f.Image = ImageSource.FromResource(ImageNameFromResource("icon.png"));
                f.Anchor = Point.Zero;
            }


            custom.Pins = x;


            custom.IsVisible = true;
            custom.IsShowingUser = true;
            custom.IsRegionChangeAnimated = true;
            custom.HasZoomEnabled = true;
            custom.HasScrollEnabled = true;
            custom.ShowTraffic = true;
            custom.MapType = MapType.Street;
         

            //works only when inisialized
            custom.MoveToMapRegion(MapSpan.FromCenterAndRadius(new Position(37, -122), Distance.FromMiles(1)));
            });
        }
        private string ImageNameFromResource(string u)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            foreach (var res in assembly.GetManifestResourceNames())
            {
                //  System.Diagnostics.Debug.WriteLine("found resource: " + res);
                if (res.Contains(u))
                {
                    return res;
                }
            }
            return null;
        }
    }

}
