using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace App7.Controllers
{
    class ServerLocationController
    {
        public void ListAll()
        {

        }
        //both user need a two way handshake in order to find each other
        public async Task<List<ViewModels.LocationByUserModel>> GetLocationByUser(string UserName,string UUID)
        {
            //http://bvusolutions.com/Geo/Location/GetLocationByUser.php?UserName=UserName2&Key=fcb6cec6-e7d4-11e7-87ce-00163e4a7907
            var url = "http://bvusolutions.com/Geo/Location/GetLocationByUser.php?UserName=UserName2&Key=fcb6cec6-e7d4-11e7-87ce-00163e4a7907";
            var ExistRequest = new Browser().JsonWebAsync<List<ViewModels.LocationByUserModel>>(url);
            await Task.WhenAll(ExistRequest);
            return ExistRequest.Result;

        }

        public async Task<RequestState> SetLocationByUser(string Username,string Latitude,string Longitude)
        {
            var url = "http://bvusolutions.com/Geo/Location/SetLocationByUser.php?UserName=UserName&Longitude={Longitude}&Latitude={Latitude}";
            var ExistResult = new Browser().Request(url);
            await Task.WhenAll(ExistResult);
            if (ExistResult.Result == "1")
            {
                return RequestState.Success;
            }
            else
            {
                return RequestState.Failed;
            }
        }

    }
}
