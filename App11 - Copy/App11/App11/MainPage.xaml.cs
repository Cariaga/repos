using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App11
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
           
        }

        private void login_Clicked(object sender, EventArgs e)
        {
         //   await Navigation.PushModalAsync(new MasterDetailPage1());
            Application.Current.MainPage = new NavigationPage(new MasterDetailPage1());
        }
    }
}
