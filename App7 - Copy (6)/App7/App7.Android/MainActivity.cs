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
using System.Net;
using Android.Graphics;
using Plugin.SecureStorage;
[assembly: Dependency(typeof(IForm2Native))]//my custom dependacy
namespace App7.Droid
{
    [Activity(Label = "Asisto", Icon = "@drawable/assisto", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
  

    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity , IForm2Native
    {

    

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            ////custom password 2048 password
            SecureStorageImplementation.StoragePassword = "-S6pVJuDRhwCB*H6WqmNT*?MjT6FRW-U@t%Jtnr%mY&%WKkjv8cAFXvphV79*zTvPZXV*QeQu%yDG3StD%x&hS+?*aH&NEjwzZyNh@xPEFMrgj#r!xfK$sJs46D$kdsehc53$^Yxae&V8DymZ*9CjE!xAZETvN?Yb2Fdy6SWFzS@JWsry2eCt@&wReJ9qD*7aRP-@z+c5CJYV*=B-ZDKQ&9+h#58?TNvBAPbyZ$hX^N@P@tu&G6m2NG6DdJk!FYxNPKj6fRfRXVU5Z!d!wqCjFnU&wncDZB!@X*SbB@s-6!q9yvd_PTX6%y!Qd9PDa=d-#4NBwMDgwK&B^h7Sj+3pyQX^Ht*ABVGvj%E*xVphk+sqbk_L+GXM#VxRWUH*t%DMu&Y2F9qj5=-6&v6RHrcd2Y%Nvdsc#@jk5knYJw$!vCdGNv7_BAKE=q672jQ@AS&FpRutdZ&+QV5p+*-vnkzUSWVUQp*haRc-eEBHx^_9CpAsZgm@K53zJz$EN&3g_L?S9reh_uWXrwuV8%fUta*tkCECtt78nzDT?dzfq=uRSf_-=JNwt2xu&E@XfDK-yamGeU%77c9$^@=BnSUbUb3T!g7ePuT!HZ%AcDMz7*L6bx2YhGF2jW5PXpG2B!x8yxhY$_65F-g8CT#WuUQDc4FvJ@tLs$rnrdn&YB&e?67qU6-eH?-MgSz$uwtGUuCPN6Q^zsLvsZvGpXbn97gpV_+eqy=6W-JBSWG_cz@q3bxDbz7?x8KndUsD4-nFvBNy7mw3CaACjK9VKP5B?Uyf_9LAbDRujFypy5x@FYFX$Uva65my9+X7*aGCEL*65u!7J-?SxhHnfGQtCmaRuA8!Cmu6pwa$ZrZ?gaZ=4=GJYFrB#LGwH6T#fFhXdBkzx+#q@DY-khNbgXAk$?2rTR!BUUaQ^^#+hnTxVc$F+Mz?Fna&w$4_EsjGTXMg*p^sNJX*JDSNLzU5+Y$-f%5PSks!kb_7jzr*uj_e54DWaekJ=EkxpZgQP9uQ=Fe^MwJ%Zxk-G^%7M8mFNtHpybQjCH69j6aPLm!@#wHM*6XsB4%PcWv4X2G7ZdvS@Bn#tDC&TYdSJfzyQ3#&VN_BFwBk2CHm+svew@@e+txAxdk5DXhZXWS*p_Xu2KrXgR#tbA8v7&mL435#$raY6W^S#Qxfjk_AnWULDk*jW$d*u7UhQ&rVMD6zMe4sQTLBsH+p23ed^eGPx?APx@fv!aK368HxP66B$EaCj6vdcK@we3k=c#H=F&DP8YXZVnLqK=!_wAXNb&?w9Mg2qDDDy-UE^8T+!WAY*2M%xt#He#DCS!z368LHSugL8@89#YJWwFP&bGMHR=EUjpRAuAL3E&*Cy6Q4zU*2fE4*2AQhr&@Auw6sNmV%**qTDL+K!hfHd6sJ6-yHue^#H7VSfKN!*!j34FUctTxd*L+^=$5K^#+WUtH#j7LTVjFUY+5Fnp6*6D!MHMpePe&CRrt6!MAm$x+HJtqKdhLuE77v#tJH9hMBN%yX2H+9ZCvg4bQA^QUAH+yw6jmdS5jr7k9pxyxH9@t9fZ9hPptDRM5SySuK!^%xsH#MLRkbQ%&s$p@Nj%Ku#mGwprLH_cBQ^FSczdcTZ!PBPx!n^U@&j3yCEx3N9wW9mqHw46qt8DkcJ6%Sbz2=_k&7QPCRr!KhtX%?UM+byjB@P#bVX6twwN_CjnC7-tpp7K8^bbNqyRfDksJ?9FL$G46E7&LVu$XeQ_ef3ZGHJMy4Ee?5ZygySt8b&@mZFEGc9eX7gAXjTF3K22Mw_sqTLugSC5!py^-2r6v_6X%AjvMpMfcUnE3e4=jy8uFQ5bZEN$?k!ArMcScM*_!WBF=w*Yh@JuqwyMMN+gsV#BBG!b#pmaRa6EnanqC-&kV*dP%ZznVC&$2kn%R##f=MASdnGU+rf3-5@BYh!9xFSN&gG=_XvLh=kZm2mXTGyS+*KM@QeJLXP88bsgc%4*^jEGsf?Ar7y3nPj+m6C$g+FqkdjJLzUDx$7R+hNPuNrTRnna2SY-Gt?m$t!hpUU-G944Q4%98had^b+b=VR?G";


            Xamarin.Forms.DependencyService.Register<SaveAndLoad>();
            //--create folder

            //--create folder 




            //--android specific 
            WifiManager wifiManager = (WifiManager)GetSystemService(WifiService);
            if (!wifiManager.IsWifiEnabled)
            {
                //attept to turn on wifi // dependant on wifi state change and internet in manifest file
                wifiManager.SetWifiEnabled(true);
            }
            this.Window.SetFlags(WindowManagerFlags.KeepScreenOn, WindowManagerFlags.KeepScreenOn);// dependant on wake lock in manifest file

            //--

            Xamarin.Forms.DependencyService.Register<MainActivity>();

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

            //not used  Forms.Context.StartActivity();pass a paramter to invoke a UI for native
            Console.WriteLine("AppClosed");
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}

