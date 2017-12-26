using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App7
{
 
    //--Stored Singleton Instance
   public class UserInfo
    {
        public ViewModels.UserInfoModel UserInfoModel { get; set; }
        private static UserInfo _UserInfo;

        public UserInfo()
        {
            _UserInfo = this;
        }

        public static UserInfo Instance
        {
            get
            {
                return _UserInfo;
            }
        }


    }
}
