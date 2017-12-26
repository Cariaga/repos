using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
public interface IForm2Native
{
   void Form2native();
}
namespace App7
{
    public partial class App : Application
    {
     
        public App()
        {
            InitializeComponent();

            MainPage = new App7.MainPage();
          
        }
      
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        //--master detail start
        static NavigationPage MasterDetailPage1;
        public static void MasterDetailPage()
        {
            //must point to it indrectly
            MasterDetailPage1 = new NavigationPage(new MasterDetailPage1());
            Application.Current.MainPage = MasterDetailPage1;
        }
        public static void CloseMasterDetailPage()
        {
            MasterDetailPage1.Navigation.PopModalAsync();
        }
        //--master detail end



        //--facebook start
        static NavigationPage _NavPageFacebook=null;

        public static Page FacebookLoginPage()
        {
              var  profilePage = new ProfilePage();
               _NavPageFacebook = new NavigationPage(profilePage);
            return _NavPageFacebook;
        }

        public static bool IsLoggedIn
        {
            get { return !string.IsNullOrWhiteSpace(_Token); }
        }

        static string _Token;
        public static string Token
        {
            get { return _Token; }
        }

        public static void SaveToken(string token)
        {
            _Token = token;
        }

        public static Action SuccessfulLoginActionFacebook
        {
            get
            {
                return new Action(() => {
                    _NavPageFacebook.Navigation.PopModalAsync();
                 
                });
            }
        }
        //--facebook end
    }
}

