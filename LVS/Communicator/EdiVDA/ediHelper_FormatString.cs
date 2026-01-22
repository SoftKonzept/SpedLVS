using System;

namespace LVS
{
    public class ediHelper_FormatString
    {
        /// <summary>
        ///             kürzt den Substring auf die angegeben Länge
        /// </summary>
        /// <param name="myVal"></param>
        /// <param name="myFillVal"></param>
        /// <param name="myFillLenght"></param>
        /// <param name="myFillLeft"></param>
        /// <returns></returns>
        public static string CutValToLenth(string myVal, int myMaxLength)
        {
            if (myVal.Length > myMaxLength)
            {
                myVal = myVal.Substring(0, myMaxLength);
            }
            return myVal;
        }

        /// <summary>
        ///             füllt den String mit dem angegebenen FillVal rechts oder links auf
        /// </summary>
        /// <param name="myVal"></param>
        /// <param name="myFillVal"></param>
        /// <param name="myFillLenght"></param>
        /// <param name="myFillLeft"></param>
        /// <returns></returns>
        public static string FillValueToLength(bool bFillValue, string StrToFill, string myValue, Int32 myLength, bool myLeft)
        {
            string retVal = myValue;
            if (bFillValue)
            {
                string strFillValue = string.Empty;
                if (
                    (StrToFill.Equals(clsEdiVDAValueAlias.const_VDA_Value_Blanks)) || (StrToFill.Equals(""))
                   )
                {
                    strFillValue = " ";
                }
                else
                {
                    strFillValue = StrToFill;
                }
                while (retVal.Length < myLength)
                {
                    if (myLeft)
                    {
                        //0 voranstelle
                        retVal = strFillValue + retVal;
                    }
                    else
                    {
                        retVal = retVal + strFillValue;
                    }
                }
                if (retVal.Length > myLength)
                {
                    retVal = retVal.Substring(0, myLength);
                }
            }
            return retVal;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strVal"></param>
        /// <param name="strFormatString"></param>
        /// <returns></returns>

        public static string FormatValue_DateTime(string strVal, string strFormatString)
        {
            DateTime tmpDate = DateTime.MinValue;

            if (
                (!strFormatString.Equals(string.Empty)) ||
                (strFormatString != null)
              )
            {
                try
                {
                    enumEdifactFormatString tmpFString = (enumEdifactFormatString)Enum.Parse(typeof(enumEdifactFormatString), strFormatString);
                    switch (tmpFString)
                    {
                        case enumEdifactFormatString.YYYYMMDD:
                            if (
                                (DateTime.TryParse(strVal, out tmpDate)) &&
                                (tmpDate > DateTime.MinValue)
                               )
                            {
                                strVal = tmpDate.ToString("yyyyMMdd");
                            }
                            break;
                        case enumEdifactFormatString.YYYYMMDDHHMM:
                            if (
                                (DateTime.TryParse(strVal, out tmpDate)) &&
                                (tmpDate > DateTime.MinValue)
                               )
                            {
                                strVal = tmpDate.ToString("yyyyMMddHHmm");
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    string strE = ex.ToString();
                }
                finally
                {

                }
            }
            return strVal;
        }
        public static string FormatValue_Decimal(decimal decTmp, string strFormatString, int myDigitCount = 0)
        {

            string strReturn = string.Empty;

            string strDecFormat = string.Empty;
            while (myDigitCount > 0)
            {
                strDecFormat += "0";
                myDigitCount--;
            }
            if (strDecFormat.Equals(string.Empty))
            {
                strReturn = string.Format("{0:0}", decTmp);
            }
            else
            {
                strReturn = string.Format("{0:0." + strDecFormat + "}", decTmp);
                strReturn = strReturn.Replace(",", ".");
            }

            return strReturn;
        }
    }
}
