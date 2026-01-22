using System;

namespace LVS.ASN.ASNFormatFunctions
{
    public class Format_DateTimeFromEDI
    {
        /// <summary>
        ///             
        /// </summary>
        /// <param name="myValue"></param>
        /// <returns></returns>
        public static string Execute(string myValue)
        {
            string strReturn = string.Empty;
            DateTime dtTmp = new DateTime();
            string yy = string.Empty;
            string mm = string.Empty;
            string dd = string.Empty;

            switch (myValue.Length)
            {
                //für Datum xx.xx.xxxx
                case 10:
                    if (DateTime.TryParse(myValue, out dtTmp))
                    {
                        strReturn = myValue;
                    }
                    break;

                // Datum xxxxxxxx 
                // DAtum xx.xx.xx
                case 8:
                    if (myValue.Contains("."))
                    {
                        if (DateTime.TryParse(myValue, out dtTmp))
                        {
                            strReturn = myValue;
                        }
                    }
                    else
                    {
                        int iTmp = 0;
                        int.TryParse(myValue.Substring(0, 4), out iTmp);
                        if (iTmp > 2020)
                        {
                            yy = myValue.Substring(0, 4);
                            mm = myValue.Substring(4, 2);
                            dd = myValue.Substring(6, 2);
                            strReturn = dd + "." + mm + "." + yy;
                        }
                        else
                        {
                            yy = myValue.Substring(0, 2);
                            mm = myValue.Substring(2, 2);
                            dd = myValue.Substring(4, 4);
                            strReturn = dd + "." + mm + "." + yy;
                        }
                    }
                    break;

                // DAtum xxxxxx
                case 6:
                    yy = myValue.Substring(0, 2);
                    mm = myValue.Substring(2, 2);
                    dd = myValue.Substring(4, 2);
                    strReturn = dd + "." + mm + "." + "20" + yy;
                    break;

                default:
                    strReturn = Globals.DefaultDateTimeMinValue.ToString("dd.MM.yyyy");
                    break;
            }

            return strReturn;
        }

        public static string Execute_ddMMyyyy(string myValue)
        {
            string strReturn = string.Empty;
            string yyyy = string.Empty;
            string mm = string.Empty;
            string dd = string.Empty;

            // -- Korrekur mr 20.06.2024
            if (myValue.Length > 8)
            {
                myValue = myValue.Remove(0, myValue.Length - 8);
            }

            if (myValue.Length == 8)
            {
                dd = myValue.Substring(0, 2);
                mm = myValue.Substring(2, 2);
                yyyy = myValue.Substring(4, 4);
                strReturn = dd + "." + mm + "." + yyyy;

            }
            else
            {
                strReturn = Globals.DefaultDateTimeMinValue.ToString("dd.MM.yyyy");
            }
            return strReturn;
        }

        public static string Execute_yyyyMMdd(string myValue)
        {
            string strReturn = string.Empty;
            string yyyy = string.Empty;
            string mm = string.Empty;
            string dd = string.Empty;

            if (myValue.Length == 8)
            {
                yyyy = myValue.Substring(0, 4);
                mm = myValue.Substring(4, 2);
                dd = myValue.Substring(6, 2);
                strReturn = dd + "." + mm + "." + yyyy;
            }
            else
            {
                strReturn = Globals.DefaultDateTimeMinValue.ToString("dd.MM.yyyy");
            }
            return strReturn;
        }
        public static DateTime Execute_yyMMddToDateTime(string myValue)
        {
            DateTime retDateTime = new DateTime(1900, 1, 1);
            string strReturn = string.Empty;
            string yy = string.Empty;
            string mm = string.Empty;
            string dd = string.Empty;

            if (myValue.Length == 6)
            {
                yy = myValue.Substring(0, 2);
                int iYeahr = 00;
                int.TryParse(yy, out iYeahr);
                iYeahr = 2000 + iYeahr;

                mm = myValue.Substring(2, 2);
                int iMonth = 1;
                int.TryParse(mm, out iMonth);

                dd = myValue.Substring(4, 2);
                int iDay = 1;
                int.TryParse(dd, out iDay);

                strReturn = dd + "." + mm + "." + yy;
                retDateTime = new DateTime(iYeahr, iMonth, iDay);
            }
            else
            {
                strReturn = Globals.DefaultDateTimeMinValue.ToString("dd.MM.yyyy");
                retDateTime = Convert.ToDateTime(strReturn);
            }
            return retDateTime;
        }
        public static DateTime Execute_yyyyMMddToDateTime(string myValue)
        {
            DateTime retDateTime = new DateTime(1900, 1, 1);
            string strReturn = string.Empty;
            string yyyy = string.Empty;
            string mm = string.Empty;
            string dd = string.Empty;

            if (myValue.Length == 8)
            {
                yyyy = myValue.Substring(0, 4);
                int iYeahr = 1900;
                int.TryParse(yyyy, out iYeahr);

                mm = myValue.Substring(4, 2);
                int iMonth = 1;
                int.TryParse(mm, out iMonth);

                dd = myValue.Substring(6, 2);
                int iDay = 1;
                int.TryParse(dd, out iDay);

                strReturn = dd + "." + mm + "." + yyyy;
                retDateTime = new DateTime(iYeahr, iMonth, iDay);
            }
            else
            {
                strReturn = Globals.DefaultDateTimeMinValue.ToString("dd.MM.yyyy");
                retDateTime = Convert.ToDateTime(strReturn);
            }
            return retDateTime;
        }

        public static DateTime Execute_yyyyMMddHHmmToDateTime(string myValue)
        {
            DateTime retDateTime = new DateTime(1900, 1, 1);
            string strReturn = string.Empty;
            string yyyy = string.Empty;
            string mm = string.Empty;
            string dd = string.Empty;
            string HH = string.Empty;
            string mmHH = string.Empty;

            if (myValue.Length == 12)
            {
                yyyy = myValue.Substring(0, 4);
                int iYeahr = 1900;
                int.TryParse(yyyy, out iYeahr);

                mm = myValue.Substring(4, 2);
                int iMonth = 1;
                int.TryParse(mm, out iMonth);

                dd = myValue.Substring(6, 2);
                int iDay = 1;
                int.TryParse(dd, out iDay);

                HH = myValue.Substring(8, 2);
                int iHour = 0;
                int.TryParse(HH, out iHour);

                mmHH = myValue.Substring(10, 2);
                int iMM = 0;
                int.TryParse(mmHH, out iMM);

                strReturn = dd + "." + mm + "." + yyyy;

                retDateTime = new DateTime(iYeahr, iMonth, iDay, iHour, iMM, 0);
            }
            else
            {
                strReturn = Globals.DefaultDateTimeMinValue.ToString("dd.MM.yyyy");
                retDateTime = Convert.ToDateTime(strReturn);
            }
            return retDateTime;
        }



        public static DateTime Execute_HHmmToTime(string myValue)
        {
            DateTime retDateTime = new DateTime(1900, 1, 1);
            string strReturn = string.Empty;
            string HH = string.Empty;
            string mm = string.Empty;

            if (myValue.Length == 4)
            {
                HH = myValue.Substring(0, 2);
                int iHour = 0;
                int.TryParse(HH, out iHour);

                mm = myValue.Substring(2, 2);
                int iMinute = 1;
                int.TryParse(mm, out iMinute);
                retDateTime = retDateTime.AddHours(iHour);
                retDateTime = retDateTime.AddMinutes(iMinute);

            }
            else
            {
                strReturn = Globals.DefaultDateTimeMinValue.ToString("dd.MM.yyyy");
                retDateTime = Convert.ToDateTime(strReturn);
            }
            return retDateTime;
        }
    }
}
