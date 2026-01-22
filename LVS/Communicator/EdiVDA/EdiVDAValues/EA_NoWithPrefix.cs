namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class EA_NoWithPrefix
    {
        public const string const_EA_NoWithPrefix = "#EANrWithPrefix#";

        internal const string Prefix_E = "E";
        internal const string Prefix_A = "A";

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
                case clsASNTyp.const_string_ASNTyp_RLL:
                case clsASNTyp.const_string_ASNTyp_RLE:
                case clsASNTyp.const_string_ASNTyp_UBE:
                case clsASNTyp.const_string_ASNTyp_UBL:
                case clsASNTyp.const_string_ASNTyp_TSE:
                case clsASNTyp.const_string_ASNTyp_TSL:
                    if (myLager.Eingang is clsLEingang)
                    {
                        strTmp = Prefix_E + myLager.Eingang.LEingangID.ToString("000000");
                    }
                    break;

                case clsASNTyp.const_string_ASNTyp_AML:
                case clsASNTyp.const_string_ASNTyp_AME:
                case clsASNTyp.const_string_ASNTyp_AVL:
                case clsASNTyp.const_string_ASNTyp_AVE:

                    if (myLager.Ausgang is clsLAusgang)
                    {
                        strTmp = Prefix_A + myLager.Ausgang.LAusgangID.ToString("000000");
                    }
                    break;

            }
            return strTmp;
        }
    }
}
