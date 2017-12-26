using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App7
{
    /// <summary>
    /// used for authentication only
    /// </summary>
    public class ProfilePage : BaseContentPage
    {
        public ProfilePage()
        {
            Content = new Label()
            {
                Text = "Profile Page",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
        }
    }
    public class BaseContentPage : ContentPage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!App.IsLoggedIn)
            {
                Debug.WriteLine("Loged in");
                Navigation.PushModalAsync(new LoginPage());
            }
        }
    }
}
