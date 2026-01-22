using System;

namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class BMW_MATDate
    {
        /// <summary>
        ///             spezielle Kundenfunktion
        ///             im alten LVS/Communicator wird, sobald keine Wert hinterlegt ist "004" ausgegeben
        /// </summary>
        public const string const_BMW_MATDate = "#BMW_MATDate#";

        public static string Execute(clsASNTyp myAsnTyp, clsLagerdaten myLager)
        {
            string strTmp = string.Empty;

            switch (myAsnTyp.Typ)
            {
                case clsASNTyp.const_string_ASNTyp_EML:
                case clsASNTyp.const_string_ASNTyp_EME:
                case clsASNTyp.const_string_ASNTyp_BML:
                case clsASNTyp.const_string_ASNTyp_BME:
                case clsASNTyp.const_string_ASNTyp_STE:
                case clsASNTyp.const_string_ASNTyp_STL:
                case clsASNTyp.const_string_ASNTyp_UBE:
                case clsASNTyp.const_string_ASNTyp_UBL:
                case clsASNTyp.const_string_ASNTyp_RLL:
                case clsASNTyp.const_string_ASNTyp_RLE:
                    strTmp = "004";
                    break;

                case clsASNTyp.const_string_ASNTyp_AML:
                case clsASNTyp.const_string_ASNTyp_AME:
                case clsASNTyp.const_string_ASNTyp_AVL:
                case clsASNTyp.const_string_ASNTyp_AVE:

                    if (myLager.Ausgang is clsLAusgang)
                    {
                        //strTmp = myLager.Ausgang.LAusgangsDate.ToString("yyMMdd");
                        DateTime dtTmp = clsSystem.const_DefaultDateTimeValue_Min;
                        if (DateTime.TryParse(myLager.Ausgang.MAT.ToString(), out dtTmp))
                        {
                            strTmp = string.Format("{0:yyMMdd}", dtTmp);
                        }
                    }
                    break;
            }
            return strTmp;
        }
    }
}
