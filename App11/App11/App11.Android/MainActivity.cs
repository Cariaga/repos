using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using System.Collections.Generic;
using Android.Gms.Maps;
using Xamarin.Forms.Maps.Android;
using Android.Gms.Maps.Model;
using Xamarin.Forms.Maps;
using App11;
using App11.Droid;


namespace App11.Droid
{
    [Activity(Label = "App11", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Xamarin.FormsMaps.Init(this, bundle);
     
            global::Xamarin.Forms.Forms.Init(this, bundle);
         
            //     Xamarin.FormsGoogleMaps.Init(this, bundle); // initialize for Xamarin.Forms.GoogleMaps
            // TKGoogleMaps.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

