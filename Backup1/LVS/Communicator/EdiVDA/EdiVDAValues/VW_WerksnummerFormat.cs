using System;
using System.Text.RegularExpressions;

namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class VW_WerksnummerFormat
    {
        public const string const_VW_WerksnummerFormat = "#WerksnummerVWFormat#";

        ///<remarks>Werksnummer soll folgendes Format aufweisen:
        ///  Die "supertolle-Syntax" von VW (Achtung, Leerzeichen vorn):
        ///  " 123 123 123 12"
        ///  " 123 123 123 123"
        ///  " 123 123 123 PLA" -> wird NICHT berücksichtigt!
        ///  " 123 123 123 A  PLA"
        ///  " 123 123 123    PLA" 
        /// </remarks>

        public static string Execute(clsArtikel myArt)
        {
            string strTmp = Regex.Replace(myArt.Werksnummer, " ", "");
            Int32 iLen = strTmp.Length;
            string str1To9 = string.Empty;
            string strRest = string.Empty;
            string strReturn = string.Empty;

            if (strTmp.Length > 8)
            {
                str1To9 = strTmp.Substring(0, 9);
                strRest = strTmp.Substring(9, (strTmp.Length - 9));

                str1To9 = str1To9.Insert(6, " ");
                str1To9 = str1To9.Insert(3, " ");
                strRest.Trim();

                if (strRest.Equals("PLA"))
                {
                    strRest = "    " + strRest;
                }
                else if (strRest.Equals("APLA"))
                {
                    strRest = " A  PLA";
                }
                strReturn = " " + str1To9 + strRest;
            }
            else
            {
                strReturn = " " + myArt.Werksnummer;
            }

            return strReturn;
        }
    }
}
