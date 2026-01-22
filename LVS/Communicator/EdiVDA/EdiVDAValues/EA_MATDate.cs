using System;

namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class EA_MATDate
    {
        public const string const_EA_MATDate = "EA.MATDate";

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
                case clsASNTyp.const_string_ASNTyp_TSE:
                case clsASNTyp.const_string_ASNTyp_TSL:
                    break;

                case clsASNTyp.const_string_ASNTyp_AML:
                case clsASNTyp.const_string_ASNTyp_AME:
                case clsASNTyp.const_string_ASNTyp_AVL:
                case clsASNTyp.const_string_ASNTyp_AVE:
                case clsASNTyp.const_string_ASNTyp_RLL:
                case clsASNTyp.const_string_ASNTyp_RLE:

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
