using System;

namespace LVS
{
    public class clsViewLog
    {
        public string Datum { get; set; }
        public string Eintrag { get; set; }
        public long ID { get; set; }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="myStringLine"></param>
        /// <returns></returns>
        public static clsViewLog GetViewLogItem(string myStringLine)
        {
            clsViewLog tmpView = new clsViewLog();
            string strDatum = string.Empty;
            if (myStringLine.Length >= 19)
            {
                strDatum = myStringLine.Substring(0, 19);
            }
            DateTime tmpDate;
            if (DateTime.TryParse(strDatum, out tmpDate))
            {
                tmpView.Datum = strDatum;
                tmpView.Eintrag = myStringLine.Replace(strDatum, "");
                //tmpView.ID = tmpDate.Ticks;
            }
            else
            {
                //Auswahl der korrekten Dateiendung
                tmpView.Datum = clsViewLog.GetLogViewDateTimeString();
                tmpView.Eintrag = myStringLine;
                tmpDate = DateTime.Now;
            }
            tmpView.ID = tmpDate.Ticks;
            return tmpView;
        }

        public static string GetLogViewDateTimeString()
        {
            string strLogViewItemDateTimeString = DateTime.Now.ToString();
            return strLogViewItemDateTimeString;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetLogViewFileDateTimeMask()
        {
            string strLogFileDateTimeMask = DateTime.Now.ToString("yyyy") + "_" +
                                    DateTime.Now.ToString("MM") + "_" +
                                    DateTime.Now.ToString("dd") + "_";
            return strLogFileDateTimeMask;
        }
    }


}
