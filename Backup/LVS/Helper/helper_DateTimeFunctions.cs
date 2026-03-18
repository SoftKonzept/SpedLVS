using System;

namespace LVS.Helper
{
    public class helper_DateTimeFunctions
    {
        public static DateTime AddTimeToDate(DateTime myDate, TimeSpan myTime)
        {
            DateTime dtTmp = myDate;
            DateTime dtReturn = dtTmp.Add(myTime);
            return dtReturn;
        }

    }
}
