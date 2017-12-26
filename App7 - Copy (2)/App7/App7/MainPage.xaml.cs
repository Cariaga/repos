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
    public class Validator
    {
        public bool IsEmailUsed()
        {
            return true;
        }
        public bool IsUserNameUsed()
        {
            return true;
        }
        public bool IsValidPassword()
        {
            return true;
        }
        public bool IsAccountActivated()
        {
            return true;
        }
        public bool IsNumberUsed()
        {
            return true;
        }
        public bool IsValidName()
        {
            return true;
        }
       
    }
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        

        private void SignUpButton_Clicked(object sender, EventArgs e)
        {

        }

        private void SignInButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new MasterDetailPage1());
        }
    }
}
