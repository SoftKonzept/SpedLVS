namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class AluDuffel_EA_712F05_FF
    {
        /// <summary>
        /// </summary>
        public const string const_AluDuffel_EA_712F05_FF = "#AluDuffel_EA_712F05_FF#";

        public static string Execute(clsASNTyp myAsnTyp, clsLagerdaten myLager)
        {
            string strTmp = string.Empty;

            switch (myAsnTyp.Typ)
            {
                case clsASNTyp.const_string_ASNTyp_EML:
                case clsASNTyp.const_string_ASNTyp_EME:
                case clsASNTyp.const_string_ASNTyp_BML:
                case clsASNTyp.const_string_ASNTyp_BME:
                case clsASNTyp.const_string_ASNTyp_UBE:
                case clsASNTyp.const_string_ASNTyp_UBL:
                case clsASNTyp.const_string_ASNTyp_TSE:
                case clsASNTyp.const_string_ASNTyp_TSL:
                    if (myLager.Eingang is clsLEingang)
                    {
                        strTmp = myLager.Eingang.ExAuftragRef.ToString();
                    }
                    break;

                case clsASNTyp.const_string_ASNTyp_AML:
                case clsASNTyp.const_string_ASNTyp_AME:
                case clsASNTyp.const_string_ASNTyp_AVL:
                case clsASNTyp.const_string_ASNTyp_AVE:
                case clsASNTyp.const_string_ASNTyp_RLL:
                case clsASNTyp.const_string_ASNTyp_RLE:
                    if (myLager.Ausgang is clsLAusgang)
                    {
                        strTmp = string.Empty;
                    }
                    break;
            }
            return strTmp;
        }
    }
}
