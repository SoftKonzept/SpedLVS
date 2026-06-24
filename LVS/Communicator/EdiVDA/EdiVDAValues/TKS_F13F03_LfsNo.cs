namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class TKS_F13F03_LfsNo
    {
        public const string const_TKS_F13F03_LfsNo = "#TKS_F13F03_LfsNo#";

        /// <summary>
        ///                 TKS benötigt in Satz713F03 die LfsNr, damit die Rechnungspositionen zugeordnet werden können. 
        ///                 Es sollen die AM folgendermapen angepasst werden:
        ///                 
        ///                 VW -> Datenfeld 713F03 (Lieferscheinnummer) -> LVSNR
        ///                 BMW -> Datenfeld 713F03 (Lieferscheinnummer) -> Wert aus BMW Edifact BGM+351::10+51037883+9' = 51037883 = 51 + AusgangId
        /// 
        ///                 Bei BMW soll die LfsNr mit 51 + LAusgangID.ToString("000000") 
        /// </summary>
        /// <param name="myAsnTyp"></param>
        /// <param name="myLager"></param>
        /// <returns></returns>

        public static string Execute(clsASNTyp myAsnTyp, clsArtikel myArtikel)
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
                    if (myArtikel.Eingang is clsLEingang)
                    {
                        strTmp = myArtikel.Eingang.LEingangLfsNr;
                    }
                    break;

                case clsASNTyp.const_string_ASNTyp_AML:
                case clsASNTyp.const_string_ASNTyp_AME:
                case clsASNTyp.const_string_ASNTyp_AVL:
                case clsASNTyp.const_string_ASNTyp_AVE:
                    switch (myArtikel.Ausgang.AbBereichID)
                    {
                        case 1:
                            strTmp = myArtikel.LVS_ID.ToString();
                            break;
                        case 5:
                            if (myArtikel.Ausgang is clsLAusgang)
                            {
                                strTmp = "51" + myArtikel.Ausgang.LAusgangID.ToString("000000");
                            }
                            break;
                        default:

                            break;
                    }
                    break;

            }
            return strTmp;
        }
    }
}
