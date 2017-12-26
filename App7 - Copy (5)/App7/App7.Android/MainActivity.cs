using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.Permissions;
using Android.Net.Wifi;
using Android.Locations;
using Xamarin.Auth;
using Android.Util;
using Xamarin.Forms;
using Android.Content;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.Android;
using System.IO;
using System.Reflection;







//[assembly: Dependency(typeof(IForm2Native))]//my custom dependacy//not used
namespace App7.Droid
{
    [Activity(Label = "App7", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
  

    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity , IForm2Native
    {
  
    

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            //--android specific 
            WifiManager wifiManager = (WifiManager)GetSystemService(WifiService);
            if (!wifiManager.IsWifiEnabled)
            {
                //attept to turn on wifi // dependant on wifi state change and internet in manifest file
                wifiManager.SetWifiEnabled(true);
            }
            this.Window.SetFlags(WindowManagerFlags.KeepScreenOn, WindowManagerFlags.KeepScreenOn);// dependant on wake lock in manifest file

            //--

          //  Xamarin.Forms.DependencyService.Register<MainActivity>();

            //--
            global::Xamarin.Forms.Forms.Init(this, bundle);
        
            Xamarin.FormsGoogleMaps.Init(this, bundle); // initialize for Xamarin.Forms.GoogleMaps
            LoadApplication(new App());

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    
       void IForm2Native.Form2native()
        {

            //  Forms.Context.StartActivity();pass a paramter to invoke a UI for native
            Console.WriteLine("Test");

        }
    }
}

