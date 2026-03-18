namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class Hydro_712F03SLB
    {
        /// <summary>
        ///             S712F03 - SLB             
        ///             Eingang -> SLB aus ASN -> Eingang.exTransportRef
        ///             Ausgang -> AusgangID
        /// </summary>
        public const string const_Hydro_712F03SLB = "#Hydro_712F03SLB#";

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
                        if (art.Eingang is clsLEingang)
                        {
                            strTmp += art.Eingang.ExTransportRef;
                        }
                        break;

                    case clsASNTyp.const_string_ASNTyp_AML:
                    case clsASNTyp.const_string_ASNTyp_AME:
                    case clsASNTyp.const_string_ASNTyp_AVL:
                    case clsASNTyp.const_string_ASNTyp_AVE:
                        if (myLager.Ausgang is clsLAusgang)
                        {
                            strTmp = myLager.Ausgang.SLB.ToString();
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
                        if (myLager.Artikel.Eingang is clsLEingang)
                        {
                            strTmp += myLager.Artikel.Eingang.ExTransportRef;
                        }
                        break;

                    case clsASNTyp.const_string_ASNTyp_AML:
                    case clsASNTyp.const_string_ASNTyp_AME:
                    case clsASNTyp.const_string_ASNTyp_AVL:
                    case clsASNTyp.const_string_ASNTyp_AVE:
                    case clsASNTyp.const_string_ASNTyp_RLL:
                    case clsASNTyp.const_string_ASNTyp_RLE:
                        if (myLager.Artikel.Ausgang is clsLAusgang)
                        {
                            strTmp += myLager.Artikel.Ausgang.LAusgangID.ToString();
                        }
                        break;
                }
            }
            return strTmp;
        }
    }
}
