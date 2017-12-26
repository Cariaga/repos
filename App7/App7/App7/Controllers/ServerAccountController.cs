using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App7.Controllers
{
    class ServerAccountController
    {
        public void Login(string UserName,string Password)
        {

        }
        public void Update()
        {

        }
        public void AsFacebookRegister(string UserName, string Password, string Name, string Surname, string Email, string Address, string City, string PhoneNumber, string Role, string Location, string SecurityQuestion, string Answer, string FacebookLink="")
        {

        }

        public void isVerified(string UserName)
        {

        }

        public async Task<bool> IsUserNameExistAsync(string tocheck)
        {

            var ExistRequest = new Browser().JsonWebAsync<List<ViewModels.UserNameModel>>("http://bvusolutions.com/Geo/Account/isUserExist.php?UserName=" + tocheck);
           await Task.WhenAll(ExistRequest);
            //UserInfo.Instance.UserInfoModel.id is the username
            if (ExistRequest.Result.FirstOrDefault().UserName == tocheck)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> IsNumberExistAsync(string tocheck)
        {

            var ExistRequest = new Browser().JsonWebAsync<List<ViewModels.PhoneNumberModel>>("http://bvusolutions.com/Geo/Account/isNumberExist.php?PhoneNumber=" + tocheck);
            await Task.WhenAll(ExistRequest);

            if (ExistRequest.Result.FirstOrDefault().PhoneNumber == tocheck)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> IsEmailExistAsync(string tocheck)
        {
            var ExistRequest = new Browser().JsonWebAsync<List<ViewModels.EmailViewModel>>("http://bvusolutions.com/Geo/Account/isExistEmail.php?Email=" + tocheck);
            await Task.WhenAll(ExistRequest);
            if (ExistRequest.Result.FirstOrDefault().Email == tocheck)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
