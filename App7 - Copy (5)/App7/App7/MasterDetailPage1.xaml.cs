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
     
          
        }
        protected override bool OnBackButtonPressed()
        {
           // Debug.WriteLine("Pressed Back");
            return true;//prevents the user going back to the login un intentionally when pressed back
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