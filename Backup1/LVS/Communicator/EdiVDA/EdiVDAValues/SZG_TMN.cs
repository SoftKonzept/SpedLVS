using System;

namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class SZG_TMN
    {
        public const string const_SZG_TMN = "#SZG_TMN#";

        ///<remarks>
        ///             TMN = Transportmittelnummer, bei alle Eingangsbuchungen
        ///             MAT Uhrzeit der Auslagerung bei allen Ausgangsbuchungen
        ///             
        /// </remarks>

        public static string Execute(string myASNTyp, ref clsLagerdaten myLager)
        {
            string strReturn = string.Empty;
            switch (myASNTyp)
            {
                case clsASNTyp.const_string_ASNTyp_EML:
                case clsASNTyp.const_string_ASNTyp_EME:
                case clsASNTyp.const_string_ASNTyp_BML:
                case clsASNTyp.const_string_ASNTyp_BME:
                    strReturn = myLager.Eingang.KFZ;
                    break;

                case clsASNTyp.const_string_ASNTyp_AML:
                case clsASNTyp.const_string_ASNTyp_AME:
                case clsASNTyp.const_string_ASNTyp_AVL:
                case clsASNTyp.const_string_ASNTyp_AVE:
                case clsASNTyp.const_string_ASNTyp_RLL:
                case clsASNTyp.const_string_ASNTyp_RLE:
                    strReturn = myLager.Ausgang.KFZ;
                    DateTime dtTmp = clsSystem.const_DefaultDateTimeValue_Min;
                    if (DateTime.TryParse(myLager.Ausgang.MAT.ToString(), out dtTmp))
                    {
                        strReturn = string.Format("{0:HHMM}", dtTmp);
                    }
                    break;
            }
            if (strReturn.Equals(string.Empty))
            {
                strReturn = "GC-SL 121";
            }
            return strReturn;
        }
    }
}
