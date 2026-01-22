namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class BMW_VGS
    {
        /// <summary>
        ///             VGS - 713F09 - Unterscheidung zu Standard
        ///             Unterschied AVE / AVL = 36 , da diese Meldung bei dem Prozess erzeugt wird
        /// </summary>
        public const string const_BMW_VGS = "#BMW_VGS#";

        public static string Execute(clsASNTyp myAsnTyp, clsLagerdaten myLager)
        {
            string strTmp = string.Empty;
            switch (myAsnTyp.Typ)
            {
                case clsASNTyp.const_string_ASNTyp_EML:
                case clsASNTyp.const_string_ASNTyp_EME:
                    strTmp = "30";
                    break;

                case clsASNTyp.const_string_ASNTyp_TSL:
                case clsASNTyp.const_string_ASNTyp_TSE:
                    strTmp = "32";
                    break;

                case clsASNTyp.const_string_ASNTyp_BML:
                case clsASNTyp.const_string_ASNTyp_BME:
                    strTmp = "35";
                    break;

                case clsASNTyp.const_string_ASNTyp_AVL:    //Unterschied
                case clsASNTyp.const_string_ASNTyp_AVE:    //Unterschied
                    strTmp = string.Empty;
                    break;


                case clsASNTyp.const_string_ASNTyp_AML:
                case clsASNTyp.const_string_ASNTyp_AME:
                    strTmp = string.Empty;
                    break;

                case clsASNTyp.const_string_ASNTyp_UBE:
                case clsASNTyp.const_string_ASNTyp_UBL:
                    strTmp = string.Empty;
                    break;

                case clsASNTyp.const_string_ASNTyp_STE:
                case clsASNTyp.const_string_ASNTyp_STL:
                    strTmp = "40";
                    break;

                case clsASNTyp.const_string_ASNTyp_RLL:
                case clsASNTyp.const_string_ASNTyp_RLE:
                    strTmp = "33";
                    break;

            }
            return strTmp;
        }
    }
}
