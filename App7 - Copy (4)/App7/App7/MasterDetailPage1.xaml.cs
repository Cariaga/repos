using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using Xamarin.Forms.GoogleMaps;
using Plugin.Geolocator.Abstractions;
using Plugin.Geolocator;
using Plugin.Connectivity;

namespace App7
{
    public delegate void EventHandler(int PageID);

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPage1 : MasterDetailPage
    {
        public static event EventHandler _show;

        public MasterDetailPage1()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this,false);
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;

            Update();

        }

        void Update()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Task.Run(async () =>
                {
                    await Task.Delay(1000);
                    //CheckLocation();
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

        private int currentPage;

       

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterDetailPage1MenuItem;
            //--Page refresh start when page is the same don't do anything
            if (item!=null&&currentPage == item.Id)
            {
                IsPresented = false;
                MasterPage.ListView.SelectedItem = null;
                return;
            }
            if (item!=null)
            {
                currentPage = item.Id;
            }
            //--Page refresh end

            //--new page selected start
            if (item == null)
                return;
            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;
            Detail = new NavigationPage(page);
            _show.Invoke(item.Id);
            IsPresented = false;
            MasterPage.ListView.SelectedItem = null;
            //--new page selected end
        }
    }
}