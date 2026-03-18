namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class TMS
    {
        /// <summary>
        ///             Satz712F14 Transportmittelschlüssel
        ///             01 = KFZ
        ///             02 = Bordero#
        ///             06 = Stückgut#
        ///             07 = Expressgut#
        ///             08 = Waggon
        ///             09 = Postpaket
        ///             10 = Flugnummer
        ///             11 = Schiffsname
        /// </summary>
        public const string const_TMS = "#TMS#";

        public static string Execute(clsASNTyp myAsnTyp, clsLagerdaten myLager)
        {
            string strTmp = string.Empty;
            switch (myAsnTyp.Typ)
            {
                case clsASNTyp.const_string_ASNTyp_EML:
                case clsASNTyp.const_string_ASNTyp_EME:
                    if (myLager.Eingang is clsLEingang)
                    {
                        if (myLager.Eingang.IsWaggon)
                        {
                            strTmp = "08";
                        }
                        else
                        {
                            strTmp = "01";
                        }
                    }
                    break;

                case clsASNTyp.const_string_ASNTyp_BML:
                case clsASNTyp.const_string_ASNTyp_BME:
                case clsASNTyp.const_string_ASNTyp_TSL:
                case clsASNTyp.const_string_ASNTyp_TSE:
                case clsASNTyp.const_string_ASNTyp_STE:
                case clsASNTyp.const_string_ASNTyp_STL:
                case clsASNTyp.const_string_ASNTyp_UBE:
                case clsASNTyp.const_string_ASNTyp_UBL:
                    if (myLager.Artikel.Eingang is clsLEingang)
                    {
                        if (myLager.Artikel.Eingang.IsWaggon)
                        {
                            strTmp = "08";
                        }
                        else
                        {
                            strTmp = "01";
                        }
                    }
                    break;


                case clsASNTyp.const_string_ASNTyp_AML:
                case clsASNTyp.const_string_ASNTyp_AME:
                    if (myLager.Ausgang is clsLAusgang)
                    {
                        if (myLager.Ausgang.IsWaggon)
                        {
                            strTmp = "08";
                        }
                        else
                        {
                            strTmp = "01";
                        }
                    }
                    break;

                case clsASNTyp.const_string_ASNTyp_AVL:    //Unterschied
                case clsASNTyp.const_string_ASNTyp_AVE:    //Unterschied
                case clsASNTyp.const_string_ASNTyp_RLL:
                case clsASNTyp.const_string_ASNTyp_RLE:
                    if (myLager.Artikel.Ausgang is clsLAusgang)
                    {
                        if (myLager.Artikel.Ausgang.IsWaggon)
                        {
                            strTmp = "08";
                        }
                        else
                        {
                            strTmp = "01";
                        }
                    }
                    break;
            }
            return strTmp;
        }
    }
}
