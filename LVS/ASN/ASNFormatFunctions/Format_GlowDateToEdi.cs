using System;

namespace LVS.ASN.ASNFormatFunctions
{
    public class Format_GlowDateToEdi
    {
        public const string const_Function_GlowDateToEdi = "#func_GlowDateToEdi#";
        public const string const_Function_GlowDateToEdi_yyyyMMdd = "#func_GlowDateToEdi_yyyyMMdd#";
        public const string const_Function_GlowDateToEdi_yyyyMMddOrBlank = "#func_GlowDateToEdi_yyyyMMddOrBlank#";
        public const string const_Function_GlowDateToEdi_yyMMdd = "#func_GlowDateToEdi_yyMMdd#";
        public const string const_Function_GlowDateToEdi_ddMMyyyy = "#func_GlowDateToEdi_ddMMyyyy#";
        public const string const_Function_GlowDateToEdi_ddMMyy = "#func_GlowDateToEdi_ddMMyy#";

        public const string const_Format_yyyyMMdd = "yyyyMMdd";
        public const string const_Format_yyyyMMddOrBlank = "yyyyMMddOrBlank";
        public const string const_Format_yyMMdd = "yyMMdd";
        public const string const_Format_ddMMyyyy = "ddMMyyyy";
        public const string const_Format_ddMMyy = "ddMMyy";

        /// <summary>
        ///             
        /// </summary>
        /// <param name="myValue"></param>
        /// <returns></returns>
        public static string Execute(clsArtikel myArticle, string myFormat)
        {
            string strReturn = string.Empty;
            switch (myFormat)
            {
                case Format_GlowDateToEdi.const_Format_yyMMdd:
                    strReturn = myArticle.GlowDate.ToString("yyMMdd");
                    break;
                case Format_GlowDateToEdi.const_Function_GlowDateToEdi_yyyyMMddOrBlank:
                    strReturn = string.Empty;
                    TimeSpan timeSpan = Globals.DefaultDateTimeMinValue - myArticle.GlowDate;
                    if (timeSpan.TotalMilliseconds != 0)
                    {
                        strReturn = myArticle.GlowDate.ToString("yyMMdd");
                    }
                    break;
                case Format_GlowDateToEdi.const_Format_yyyyMMdd:
                    strReturn = myArticle.GlowDate.ToString("yyyyMMdd");
                    break;
                case Format_GlowDateToEdi.const_Format_ddMMyy:
                    strReturn = myArticle.GlowDate.ToString("ddMMyy");
                    break;
                case Format_GlowDateToEdi.const_Format_ddMMyyyy:
                    strReturn = myArticle.GlowDate.ToString("ddMMyyyy");
                    break;

                default:
                    strReturn = Globals.DefaultDateTimeMinValue.ToString("ddMMyyyy");
                    break;
            }
            return strReturn;
        }


    }
}
