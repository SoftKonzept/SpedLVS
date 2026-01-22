using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Import
{
    public class helper_LogStringCreater
    {
        public static string CreateString(string myString)
        {
            string strReturn = string.Empty;

            if (!myString.Equals(string.Empty))
            {
                strReturn = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + " " + myString;
            }
            return strReturn;
        }
    }
}
