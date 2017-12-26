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
using Newtonsoft.Json;

namespace App7
{
    public delegate void EventHandler(int PageID);
    public delegate void DelegateOnGPSUpdate(Plugin.Geolocator.Abstractions.Position position);

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPage1 : MasterDetailPage
    {
        public static event EventHandler _show;
    
        public static event DelegateOnGPSUpdate OnGPSUpdate;
        public static MasterDetailPage1 instance;
        public MasterDetailPage1()
        {
            InitializeComponent();
            instance = this;

            NavigationPage.SetHasNavigationBar(this,false);
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
          

            //-- gps constant update
            Device.StartTimer(TimeSpan.FromSeconds(5), () => {
                Task.Run(async () => {
                    var t = await GPSLocator.UpdateLocation();
                    OnGPSUpdate.Invoke(GPSLocator.ResultPosition);
                }).ConfigureAwait(false);
                return true;
            });
            //-- gps constant update

            
        }
       


        protected override bool OnBackButtonPressed()
        {
           // Debug.WriteLine("Pressed Back");
            return true;//prevents the user going back to the login un intentionally when pressed back
        }
        private int currentPage;
        

        public Page page { get; private set; }

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


            page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;
            
            Detail = new NavigationPage(page);
            
            _show.Invoke(item.Id);
            IsPresented = false;
            MasterPage.ListView.SelectedItem = null;
            //--new page selected end
        }
    }
}