using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App7.ViewModels
{
    public class StatusByUserRequestModel
    {
        public string RequestToUsername { get; set; }
        public string RequestByUsername { get; set; }
        public string Task { get; set; }
        public string RequestDate { get; set; }
        public string RequestTime { get; set; }
        public string CompletedDate { get; set; }
        public string CompletedTime { get; set; }
        public string Status { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string Category { get; set; }
        public string UUID { get; set; }
    }
}
