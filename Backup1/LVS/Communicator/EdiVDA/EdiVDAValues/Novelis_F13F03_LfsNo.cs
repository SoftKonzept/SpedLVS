namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class Novelis_F13F03_LfsNo
    {
        public const string const_Novelis_F13F03_LfsNo = "#Novelis_F13F03_LfsNo#";

        /// <summary>
        ///                 Novelils möchte zur besseren Zuweisung der RG von BMW haben
        ///                 BMW verknüpgt die Rechnungspositionen durch  51 + LAusgangID.ToString("000000")
        ///                 
        ///                 Bei den anderen Arbeitsbereichen soll alles so bleiben
        /// </summary>
        /// <param name="myAsnTyp"></param>
        /// <param name="myLager"></param>
        /// <returns></returns>

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
                        strTmp = myLager.Eingang.LEingangLfsNr;
                    }
                    break;

                case clsASNTyp.const_string_ASNTyp_AML:
                case clsASNTyp.const_string_ASNTyp_AME:
                case clsASNTyp.const_string_ASNTyp_AVL:
                case clsASNTyp.const_string_ASNTyp_AVE:
                    if (myLager.Ausgang.AbBereichID == 5)
                    {
                        if (myLager.Ausgang is clsLAusgang)
                        {
                            strTmp = "51" + myLager.Ausgang.LAusgangID.ToString("000000");
                        }
                    }
                    else
                    {
                        if (myLager.Ausgang is clsLAusgang)
                        {
                            strTmp = myLager.Ausgang.LfsNr.Replace(" ", "");
                        }
                    }
                    break;

            }
            return strTmp;
        }
    }
}
