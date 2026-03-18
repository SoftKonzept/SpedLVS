using System;
using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class ediHelper_SegmentSplitt
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mySegment"></param>
        /// <param name="myReturnPosition"></param>
        /// <returns></returns>
        public static string GetSegmentFieldValue(string mySegment, int myReturnPosition)
        {
            string strReturn = string.Empty;
            try
            {
                if ((!mySegment.Equals(string.Empty) && (myReturnPosition > -1)))
                {
                    List<string> strValue = mySegment.Split(new char[] { ':', '+' }).ToList();
                    int i = myReturnPosition - 1;
                    strReturn = strValue[i];
                }
            }
            catch (Exception ex)
            {
                string strError = ex.ToString();
            }
            return strReturn.Trim();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mySegment"></param>
        /// <param name="myReturnPosition"></param>
        /// <returns></returns>
        public static string GetSegmentDTMFieldValue(string mySegment, enumEdi4984SegmQualifier_DTM myEnumDate)
        {
            string strReturn = string.Empty;
            try
            {
                if (!mySegment.Equals(string.Empty))
                {
                    List<string> strValue = mySegment.Split(new char[] { ':', '+' }).ToList();
                    if (strValue.Count > 2)
                    {
                        string strQualifier = strValue[1];
                        if (strQualifier.Equals(((int)myEnumDate).ToString()))
                        {
                            string strDate = strValue[2];
                            string strFormat = strValue[3];
                            DateTime tmpDate = DateTime.MinValue;
                            switch (strFormat)
                            {
                                case "102":
                                    if (strDate.Length == 8)
                                    {
                                        string strWork = strDate.Substring(6, 2) + "." + strDate.Substring(4, 2) + "." + strDate.Substring(0, 4);
                                        DateTime.TryParse(strWork, out tmpDate);
                                        strReturn = tmpDate.ToString();
                                    }
                                    break;
                                case "203":

                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string strError = ex.ToString();
            }
            return strReturn;
        }
    }
}
