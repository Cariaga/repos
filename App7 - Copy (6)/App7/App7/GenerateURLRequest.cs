using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App7
{
    class GenerateURLRequest
    {
        public static string GenerateGet(Dictionary<string, string> dictionary)
        {
            var result = "?";
            foreach (var dic in dictionary)
            {
                result += dic.Key + "=" + dic.Value;
            }
            return result;
        }
    }
}
