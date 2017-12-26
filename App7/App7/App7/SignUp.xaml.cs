using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App7
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUp : ContentPage
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void Username_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Validator.IsUserNameUsed())
            {

            }
        }

        private void Password_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (Validator.IsValidPassword())
            {

            }
        }

        private void Email_TextChanged_2(object sender, TextChangedEventArgs e)
        {
            if (Validator.IsEmailUsed())
            {

            }
        }

        private void PhoneNumber_TextChanged_3(object sender, TextChangedEventArgs e)
        {
            if (Validator.IsValidPhoneNumber())
            {

            }
        }

        private void Name_TextChanged_4(object sender, TextChangedEventArgs e)
        {
            if (Validator.IsValidName())
            {

            }
        }

        private void Surname_TextChanged_5(object sender, TextChangedEventArgs e)
        {
            if (Validator.IsValidName())
            {

            }
        }

        private void Address_TextChanged_6(object sender, TextChangedEventArgs e)
        {
            //no need to check
        }

        private void City_TextChanged_7(object sender, TextChangedEventArgs e)
        {
            //no need to check
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}