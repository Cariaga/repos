using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App7.ViewModels
{
    public class Data
    {
        public int height { get; set; }
        public bool is_silhouette { get; set; }
        public string url { get; set; }
        public int width { get; set; }
    }

    public class Picture
    {
        public Data data { get; set; }
    }

    public class AgeRange
    {
        public int min { get; set; }
    }

    public class Cover
    {
        public string id { get; set; }
        public int offset_x { get; set; }
        public int offset_y { get; set; }
        public string source { get; set; }
    }

    public class UserInfoModel
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string gender { get; set; }
        public Picture picture { get; set; }
        public AgeRange age_range { get; set; }
        public Cover cover { get; set; }
        public string id { get; set; }
        public int timezone { get; set; }
        public string locale { get; set; }
        public string link { get; set; }
        public bool verified { get; set; }
    }
}
