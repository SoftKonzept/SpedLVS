namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class VGS
    {
        public const string const_VGS = "#VGS#";

        public static string Execute(clsASNTyp myAsnTyp)
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

                case clsASNTyp.const_string_ASNTyp_AML:
                case clsASNTyp.const_string_ASNTyp_AME:
                    strTmp = "36";
                    break;

                case clsASNTyp.const_string_ASNTyp_AVL:
                case clsASNTyp.const_string_ASNTyp_AVE:
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
