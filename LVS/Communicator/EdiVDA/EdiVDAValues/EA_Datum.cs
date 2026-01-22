namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class EA_Datum
    {
        public const string const_EA_Datum = "EA.Datum";


        public static string Execute(clsASNTyp myAsnTyp, clsLagerdaten myLager, clsASNArt myAsnArt = null)
        {
            string strTmp = string.Empty;

            switch (myAsnTyp.Typ)
            {
                case clsASNTyp.const_string_ASNTyp_BML:
                case clsASNTyp.const_string_ASNTyp_BME:
                    if ((myLager.Artikel is clsArtikel) && (myLager.Artikel.Eingang is clsLEingang))
                    {
                        if (myAsnArt is clsASNArt)
                        {
                            strTmp = myLager.Artikel.Eingang.LEingangDate.ToString();
                        }
                        else
                        {
                            strTmp = myLager.Artikel.Eingang.LEingangDate.ToString("yyMMdd");
                        }

                    }
                    break;

                case clsASNTyp.const_string_ASNTyp_EML:
                case clsASNTyp.const_string_ASNTyp_EME:
                case clsASNTyp.const_string_ASNTyp_STE:
                case clsASNTyp.const_string_ASNTyp_STL:
                case clsASNTyp.const_string_ASNTyp_TSE:
                case clsASNTyp.const_string_ASNTyp_TSL:
                case clsASNTyp.const_string_ASNTyp_UBE:
                case clsASNTyp.const_string_ASNTyp_UBL:
                    if (myLager.Artikel.Eingang is clsLEingang)
                    {
                        //strTmp = myLager.Eingang.LEingangDate.ToString("yyMMdd");
                        if (myAsnArt is clsASNArt)
                        {
                            strTmp = myLager.Artikel.Eingang.LEingangDate.ToString();
                        }
                        else
                        {
                            strTmp = myLager.Artikel.Eingang.LEingangDate.ToString("yyMMdd");
                        }
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
                        if (myAsnArt is clsASNArt)
                        {
                            strTmp = myLager.Artikel.Ausgang.LAusgangsDate.ToString();
                        }
                        else
                        {
                            strTmp = myLager.Artikel.Ausgang.LAusgangsDate.ToString("yyMMdd");
                        }
                    }
                    break;
            }
            return strTmp;
        }

        public static string GetValue(clsASNTyp myAsnTyp, clsLagerdaten myLager)
        {
            string strTmp = string.Empty;

            switch (myAsnTyp.Typ)
            {
                case clsASNTyp.const_string_ASNTyp_BML:
                case clsASNTyp.const_string_ASNTyp_BME:
                    if ((myLager.Artikel is clsArtikel) && (myLager.Artikel.Eingang is clsLEingang))
                    {
                        strTmp = myLager.Artikel.Eingang.LEingangDate.ToString();
                    }
                    break;

                case clsASNTyp.const_string_ASNTyp_EML:
                case clsASNTyp.const_string_ASNTyp_EME:
                case clsASNTyp.const_string_ASNTyp_STE:
                case clsASNTyp.const_string_ASNTyp_STL:
                case clsASNTyp.const_string_ASNTyp_TSE:
                case clsASNTyp.const_string_ASNTyp_TSL:
                case clsASNTyp.const_string_ASNTyp_UBE:
                case clsASNTyp.const_string_ASNTyp_UBL:
                    if (myLager.Eingang is clsLEingang)
                    {
                        strTmp = myLager.Eingang.LEingangDate.ToString("");
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
                        strTmp = myLager.Ausgang.LAusgangsDate.ToString("");
                    }
                    break;
            }
            return strTmp;
        }
    }
}
