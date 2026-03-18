namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class TMN
    {
        /// <summary>
        ///
        /// </summary>
        public const string const_TMN = "#TMN#";

        public static string Execute(clsASNTyp myAsnTyp, clsLagerdaten myLager)
        {
            string strTmp = string.Empty;
            switch (myAsnTyp.Typ)
            {
                case clsASNTyp.const_string_ASNTyp_BML:
                case clsASNTyp.const_string_ASNTyp_BME:
                    strTmp = string.Empty;
                    break;

                case clsASNTyp.const_string_ASNTyp_EML:
                case clsASNTyp.const_string_ASNTyp_EME:
                case clsASNTyp.const_string_ASNTyp_TSL:
                case clsASNTyp.const_string_ASNTyp_TSE:
                case clsASNTyp.const_string_ASNTyp_STE:
                case clsASNTyp.const_string_ASNTyp_STL:
                    if (myLager.Eingang is clsLEingang)
                    {
                        if (myLager.Eingang.IsWaggon)
                        {
                            strTmp = myLager.Eingang.WaggonNr;
                        }
                        else
                        {
                            strTmp = myLager.Eingang.KFZ;
                        }
                    }
                    break;
                case clsASNTyp.const_string_ASNTyp_AML:
                case clsASNTyp.const_string_ASNTyp_AME:
                case clsASNTyp.const_string_ASNTyp_AVL:    //Unterschied
                case clsASNTyp.const_string_ASNTyp_AVE:    //Unterschied
                case clsASNTyp.const_string_ASNTyp_RLL:
                case clsASNTyp.const_string_ASNTyp_RLE:
                    if (myLager.Ausgang is clsLAusgang)
                    {
                        if (myLager.Ausgang.IsWaggon)
                        {
                            strTmp = myLager.Ausgang.WaggonNr;
                        }
                        else
                        {
                            strTmp = myLager.Ausgang.KFZ;
                        }
                    }
                    break;
            }
            return strTmp;
        }
    }
}
