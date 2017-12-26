using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App7
{
    //deprected
   public class ServerValidator
    {
        public async Task<bool> IsUserNameExistAsync(string tocheck)
        {
          
            var ExistRequest = new Browser().Request("http://bvusolutions.com/Geo/Account/isUserExist.php?UserName=" + tocheck);
            var ExistResult = JsonConvert.DeserializeObject<List<ViewModels.UserNameModel>>(ExistRequest.Result);

           await Task.WhenAll(ExistRequest);
            //UserInfo.Instance.UserInfoModel.id is the username
            if (ExistResult.FirstOrDefault().UserName == tocheck)
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
        
            var ExistRequest = new Browser().Request("http://bvusolutions.com/Geo/Account/isNumberExist.php?PhoneNumber=" + tocheck);
            var ExistResult = JsonConvert.DeserializeObject<List<ViewModels.PhoneNumberModel>>(ExistRequest.Result);
           await Task.WhenAll(ExistRequest);
            //Debug.WriteLine(ExistResult.FirstOrDefault().PhoneNumber);
            if (ExistResult.FirstOrDefault().PhoneNumber == tocheck)
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
            var ExistRequest = new Browser().Request("http://bvusolutions.com/Geo/Account/isExistEmail.php?Email=" + tocheck);
            var ExistResult = JsonConvert.DeserializeObject<List<ViewModels.EmailViewModel>>(ExistRequest.Result);
           await Task.WhenAll(ExistRequest);
            // Debug.WriteLine(ExistResult.FirstOrDefault().Email);
            if (ExistResult.FirstOrDefault().Email == tocheck)
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
