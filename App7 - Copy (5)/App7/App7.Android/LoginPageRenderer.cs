using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Xamarin.Auth;
using App7.Droid;
using App7;
using System.Diagnostics;
using System.Threading.Tasks;
using Android.Content.PM;

[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]//reqired for custom renderer or else nothing will show
namespace App7.Droid
{
    
    //prevent looping of activity aka reseting when closing
    [Activity(NoHistory  = true, Label = "App7", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize,LaunchMode = LaunchMode.SingleInstance)]
    public class LoginPageRenderer : PageRenderer
    {
        public static bool instance;
        //this is from https://stackoverflow.com/questions/24105390/how-to-login-to-facebook-in-xamarin-forms/24423833#24423833
        //https://forums.xamarin.com/discussion/26374/onmodelchanged-event-not-found-for-android-page-renderer
        //notice the OnModel is outdated you should use OnElementChanged but OnElementChanged is called twice 
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Page> e)
        {
            base.OnElementChanged(e);


            if (e.OldElement != null || Element == null)
            {
                return;
            }
            if (!instance)
            {
                Console.WriteLine("Called");
                instance = true;
                return;
            }
           


            //try to retrive account
            IEnumerable<Account> accounts = AccountStore.Create().FindAccountsForService("Facebook");

            // this is a ViewGroup - so should be able to load an AXML file and FindView<>
            var activity = this.Context as Activity;
            
            
            //for facebook
             var auth = new OAuth2Authenticator(
                 clientId: "203538636868958", // your OAuth2 client id
                 scope: "", // the scopes for the particular API you're accessing, delimited by "+" symbols
                 authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"), // the auth URL for the service
                 redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html")); // the redirect URL for the service

            //for google // google is non working
            /*   var auth = new OAuth2Authenticator(
                 clientId: "833412182850-lvl6gh2pprh35dnutrnb61thgcq4efic.apps.googleusercontent.com", // your OAuth2 client id
                 scope: "", // the scopes for the particular API you're accessing, delimited by "+" symbols
                 authorizeUrl: new Uri("https://accounts.google.com/o/oauth2/auth/"), // the auth URL for the service
                 redirectUrl: new Uri("https://www.googleapis.com/oauth2/v2/userinfo"),
                 accessTokenUrl:new Uri("https://www.googleapis.com/oauth2/v4/token"),
                 getUsernameAsync: null,
                 clientSecret: "oL9klRrrltzSmmSF8Fd-Jbli"); // the redirect URL for the service
                */

            auth.Completed += (sender, eventArgs) => {
                if (eventArgs.IsAuthenticated)
                {
                    App.SuccessfulLoginActionFacebook.Invoke();
                    // Use eventArgs.Account to do wonderful things
                    var account = eventArgs.Account;
                    var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me?fields=email,first_name,last_name,gender,picture"), null, account);
                    AccountStore.Create().Save(eventArgs.Account, "Facebook");


                    var x= Task.Run(async () => {
                        return await request.GetResponseAsync();

                    });
                    x.ContinueWith(
                        t =>
                        {
                            if (t.IsFaulted)
                                Console.WriteLine("Error: " + t.Exception.InnerException.Message);
                            else
                            {
                                string json = t.Result.GetResponseText();
                                Console.WriteLine(json);
                            }
                        }
                    ).ConfigureAwait(false);

                    App.SaveToken(eventArgs.Account.Properties["access_token"]);
                 
                    App.MasterDetailPage();
                }
                else
                {
                    Console.WriteLine("User Cancelled");
                    activity.StartActivity(typeof(MainActivity));//we restart it
                    activity.Finish();//we lose the previews activities entirly 
                }
            };

           activity.StartActivity(auth.GetUI(activity));
        }
       
    }
}