using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App7.ViewModels
{
   public class UserTable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string UserID { get; set; }//from facebook
    }
}
