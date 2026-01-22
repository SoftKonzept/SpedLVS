using System;

namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class Datetime_NOW
    {
        /// <summary>
        /// </summary>
        public const string const_Datetime_NOW = "#NOW#";

        public static string Execute()
        {
            string strTmp = string.Empty;
            //string strYear = String.Format("{0:yy}", DateTime.Now);
            //string strMonth = String.Format("{0:MM}", DateTime.Now);
            //string strDay = String.Format("{0:dd}", DateTime.Now);
            strTmp = DateTime.Now.ToString();
            return strTmp;
        }
    }
}
