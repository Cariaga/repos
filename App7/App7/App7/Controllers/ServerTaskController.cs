using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public enum RequestState
{
    Success,Failed,AlreadySet
}
namespace App7.Controllers
{
    class ServerTaskController
    {
       
        public void ListAllTaskBy(string Category,string Status)
        {
            var url = $"http://bvusolutions.com/Geo/Task/ListAllTaskBy.php?Category={Category}";//meeds to be re evaluated
        }
        public void ListAll()
        {

        }
        public async Task<List<ViewModels.TaskSearchModel>> SearchAsync(string Search,string Limit)
        {
            var url = $"http://bvusolutions.com/Geo/Task/Search.php?Search={Search}&Limit={Limit}";

            var ExistRequest = new Browser().JsonWebAsync<List<ViewModels.TaskSearchModel>>(url);
            await Task.WhenAll(ExistRequest);
            return ExistRequest.Result;
        }
        public async Task<List<ViewModels.TaskHistoryToUserModel>> TaskHistoryToUser(string RequestToUserName)
        {
            //http://bvusolutions.com/Geo/Task/TaskHistoryToUser.php?RequestToUserName=UserName
            var url = $"http://bvusolutions.com/Geo/Task/TaskHistoryToUser.php?RequestToUserName={RequestToUserName}";

            var ExistRequest = new Browser().JsonWebAsync<List<ViewModels.TaskHistoryToUserModel>>(url);
            await Task.WhenAll(ExistRequest);
            return ExistRequest.Result;
        }
        public async Task <List<ViewModels.TaskRequestByUserModel>> TaskRequestByUser(string TaskRequestByUser)
        {
            //"http://bvusolutions.com/Geo/Task/TaskRequestByUser.php?RequestByUserName=UserName2"
            var url = $"http://bvusolutions.com/Geo/Task/TaskRequestByUser.php?RequestByUserName={TaskRequestByUser}";

            var ExistRequest = new Browser().JsonWebAsync<List<ViewModels.TaskRequestByUserModel>>(url);
            await Task.WhenAll(ExistRequest);
            return ExistRequest.Result;
        }

        public async Task<RequestState> SetCancelled(string Key, string UserName)
        {
            //http://bvusolutions.com/Geo/Task/SetCancelled.php?RequestByUserName=UserName2&Key=fcb6cec6-e7d4-11e7-87ce-00163e4a7907
            var url = $"http://bvusolutions.com/Geo/Task/SetCancelled.php?RequestByUserName={UserName}&Key={Key}";
            var ExistResult = new Browser().Request(url);
            await Task.WhenAll(ExistResult);
            if (ExistResult.Result=="1")
            {
                return RequestState.Success;
            }
            else
            {
                return RequestState.Failed;
            }
          
        }
        public async Task<RequestState> SetCompleted(string Key, string UserName)
        {
            //"http://bvusolutions.com/Geo/Task/SetCompleted.php?RequestByUserName=UserName2&Key=fcb6cec6-e7d4-11e7-87ce-00163e4a7907"
            var url = $"http://bvusolutions.com/Geo/Task/SetCompleted.php?RequestByUserName={UserName}&Key={Key}";
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
        public async Task<RequestState> SetOngoing(string Key, string UserName)
        {
            //http://bvusolutions.com/Geo/Task/SetOngoing.php?RequestByUserName=UserName2&Key=fcb6cec6-e7d4-11e7-87ce-00163e4a7907"
            var url = $"http://bvusolutions.com/Geo/Task/SetOngoing.php?RequestByUserName={UserName}&Key={Key}";
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

        public async Task<RequestState> SetDestination(string latitude, string longitude, string Key,string UserName)
        {
            //"http://bvusolutions.com/Geo/Task/SetDestination.php?RequestByUserName=UserName2&Key=fcb6cec6-e7d4-11e7-87ce-00163e4a7907&Longitude=1&Latitude=1"
            var url = $"http://bvusolutions.com/Geo/Task/SetDestination.php?RequestByUserName=UserName2&Key=fcb6cec6-e7d4-11e7-87ce-00163e4a7907&Longitude={longitude}&Latitude={latitude}";
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
        public async Task<RequestState> SetOrigin(string latitude, string longitude, string Key,string UserName)
        {
            //"http://bvusolutions.com/Geo/Task/SetOrigin.php?RequestByUserName=UserName2&Key=fcb6cec6-e7d4-11e7-87ce-00163e4a7907&Longitude=1&Latitude=1"
            var url = $"http://bvusolutions.com/Geo/Task/SetOrigin.php?RequestByUserName=UserName2&Key=fcb6cec6-e7d4-11e7-87ce-00163e4a7907&Longitude={longitude}&Latitude={latitude}";
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
