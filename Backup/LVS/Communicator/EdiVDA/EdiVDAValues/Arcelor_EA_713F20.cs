namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class Arcelor_EA_713F20
    {
        /// <summary>
        ///             24 + Lieferscheinnummer
        ///             
        /// </summary>
        public const string const_Arcelor_EA_713F20 = "#Arcelor_EA_713F20#";

        public static string Execute(clsASNTyp myAsnTyp, clsLagerdaten myLager)
        {
            string strTmp = string.Empty;
            strTmp = "24";
            //es ist eine UB
            clsArtikel art = myLager.Artikel.Copy();
            art.ID = myLager.Artikel.ArtIDAlt;
            art.GetArtikeldatenByTableID();
            switch (myAsnTyp.Typ)
            {
                case clsASNTyp.const_string_ASNTyp_EML:
                case clsASNTyp.const_string_ASNTyp_EME:
                case clsASNTyp.const_string_ASNTyp_BML:
                case clsASNTyp.const_string_ASNTyp_BME:
                case clsASNTyp.const_string_ASNTyp_RLL:
                case clsASNTyp.const_string_ASNTyp_RLE:
                    if (art.Eingang is clsLEingang)
                    {
                        strTmp += art.Eingang.LEingangLfsNr.ToString();
                    }
                    break;

                case clsASNTyp.const_string_ASNTyp_AML:
                case clsASNTyp.const_string_ASNTyp_AME:
                case clsASNTyp.const_string_ASNTyp_AVL:
                case clsASNTyp.const_string_ASNTyp_AVE:
                    if (art.Eingang is clsLEingang)
                    {
                        strTmp += art.Eingang.LEingangLfsNr.ToString();
                    }
                    break;
            }
            return strTmp;
        }
    }
}
