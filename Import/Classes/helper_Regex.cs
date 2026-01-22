using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Import
{
    public class helper_Regex
    {
        public static string OnlyNumbers(string myValue)
        {
            string strReturn = Regex.Replace(myValue, @"[^\d]", "");
            return strReturn;
        }
    }
}
