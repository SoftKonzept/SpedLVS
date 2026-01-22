using System;

namespace LVS
{
    public class helper_FilePrefixAfterComProzess
    {
        public static string GetPrefixEDI(string myAsnTyp, string myId)
        {
            string strReturn = string.Empty;
            strReturn += myAsnTyp + "_" + myId + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + "_";
            return strReturn;
        }
        public static string GetPrefixVDA(string myAsnTyp, string myId)
        {
            string strReturn = string.Empty;
            strReturn += DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + "_" + myAsnTyp + "_" + myId + "_";
            //strReturn +=  myId + "_";
            return strReturn;
        }
    }
}
