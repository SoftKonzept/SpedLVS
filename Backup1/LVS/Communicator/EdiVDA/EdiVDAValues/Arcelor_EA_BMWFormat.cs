namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class Arcelor_EA_BMWFormat
    {
        /// <summary>
        ///             spezieller string - BMW rechnet anhand dieser Combi bei Arcelor ab
        ///             
        ///             Eingang -> 52 + Eingangsnummer
        ///             Ausgang -> 51 + Ausgangsnummer
        /// </summary>
        public const string const_Arcelor_EA_BMWFormat = "#Arcelor_EA_BMWFormat#";

        public static string Execute(clsASNTyp myAsnTyp, clsLagerdaten myLager)
        {
            string strTmp = string.Empty;
            if (myLager.Artikel.ArtIDAlt > 0)
            {
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
                        strTmp = "52";
                        if (art.Eingang is clsLEingang)
                        {
                            strTmp += art.Eingang.LEingangID.ToString("000000");
                        }
                        break;

                    case clsASNTyp.const_string_ASNTyp_AML:
                    case clsASNTyp.const_string_ASNTyp_AME:
                    case clsASNTyp.const_string_ASNTyp_AVL:
                    case clsASNTyp.const_string_ASNTyp_AVE:
                        strTmp = "51";
                        if (art.Ausgang is clsLAusgang)
                        {
                            strTmp += art.Ausgang.LAusgangID.ToString("000000");
                        }
                        break;
                }
            }
            else
            {
                switch (myAsnTyp.Typ)
                {
                    case clsASNTyp.const_string_ASNTyp_EML:
                    case clsASNTyp.const_string_ASNTyp_EME:
                    case clsASNTyp.const_string_ASNTyp_BML:
                    case clsASNTyp.const_string_ASNTyp_BME:
                        strTmp = "52";
                        if (myLager.Artikel.Eingang is clsLEingang)
                        {
                            strTmp += myLager.Artikel.Eingang.LEingangID.ToString("000000");
                        }
                        break;

                    case clsASNTyp.const_string_ASNTyp_AML:
                    case clsASNTyp.const_string_ASNTyp_AME:
                    case clsASNTyp.const_string_ASNTyp_AVL:
                    case clsASNTyp.const_string_ASNTyp_AVE:
                    case clsASNTyp.const_string_ASNTyp_RLL:
                    case clsASNTyp.const_string_ASNTyp_RLE:
                        strTmp = "51";
                        if (myLager.Artikel.Ausgang is clsLAusgang)
                        {
                            strTmp += myLager.Artikel.Ausgang.LAusgangID.ToString("000000");
                        }
                        break;
                }
            }
            return strTmp;
        }
    }
}
