using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public enum NotificationStatus
{
    Cancelled, Completed, Ongoing
}
namespace App7.Controllers
{
    class ServerNotificationController
    {
        public void ListAll(string RequestByUserName, NotificationStatus Status)
        {
            var NotificationStatus = Status.ToString();
        }
        public async Task<List<ViewModels.StatusByUserRequestModel>> StatusRequestByUser(string RequestByUserName, NotificationStatus Status)
        {
            var NotificationStatus = Status.ToString();
            //StatusByUserRequestModel
            //http://bvusolutions.com/Geo/Notification/StatusByUserRequest.php?RequestByUserName=Username2&TaskStatus=Ongoing
            var url = $"http://bvusolutions.com/Geo/Notification/StatusByUserRequest.php?RequestByUserName={RequestByUserName}&TaskStatus={NotificationStatus}";
            var ExistRequest = new Browser().JsonWebAsync<List<ViewModels.StatusByUserRequestModel>>(url);
            await Task.WhenAll(ExistRequest);
            return ExistRequest.Result;
        }
    }
}
