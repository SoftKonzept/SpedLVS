namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class BMW_EANoWithPrefix
    {
        public const string const_BMW_EANoWithPrefix = "#BMW_EANoWithPrefix#";

        internal const string Prefix_E = "E";
        internal const string Prefix_A = "A";

        /// <summary>
        ///             Bei einer UB - wird aus dem AVIS quasi die EM für das Material in den BMW Bestand
        /// </summary>
        /// <param name="myAsnTyp"></param>
        /// <param name="myLager"></param>
        /// <returns></returns>

        public static string Execute(clsASNTyp myAsnTyp, clsLagerdaten myLager)
        {
            string strTmp = string.Empty;

            switch (myAsnTyp.Typ)
            {

                case clsASNTyp.const_string_ASNTyp_BML:
                case clsASNTyp.const_string_ASNTyp_BME:
                case clsASNTyp.const_string_ASNTyp_STE:
                case clsASNTyp.const_string_ASNTyp_STL:
                    if (myLager.Eingang is clsLEingang)
                    {
                        strTmp = Prefix_E + myLager.Eingang.LEingangID.ToString("000000");
                    }
                    break;

                case clsASNTyp.const_string_ASNTyp_EML:
                case clsASNTyp.const_string_ASNTyp_EME:
                case clsASNTyp.const_string_ASNTyp_UBE:
                case clsASNTyp.const_string_ASNTyp_UBL:
                case clsASNTyp.const_string_ASNTyp_RLL:
                case clsASNTyp.const_string_ASNTyp_RLE:
                    if ((myLager.Eingang is clsLEingang) && (myLager.Eingang.LEingangLfsNr != null))
                    {
                        strTmp = myLager.Eingang.LEingangLfsNr.TrimStart('0');
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
