namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class SLBWithPrefix
    {
        /// <summary>
        /// </summary>
        public const string const_SLBWithPrefix = "#SLBWithPrefix#";
        internal const string const_Ausgang = "A";
        public static string Execute(clsASNTyp myAsnTyp, clsLagerdaten myLager)
        {
            string strTmp = string.Empty;

            switch (myAsnTyp.Typ)
            {
                case clsASNTyp.const_string_ASNTyp_EML:
                case clsASNTyp.const_string_ASNTyp_EME:
                case clsASNTyp.const_string_ASNTyp_BML:
                case clsASNTyp.const_string_ASNTyp_BME:
                case clsASNTyp.const_string_ASNTyp_RLL:
                case clsASNTyp.const_string_ASNTyp_RLE:
                case clsASNTyp.const_string_ASNTyp_UBE:
                case clsASNTyp.const_string_ASNTyp_UBL:
                    break;

                case clsASNTyp.const_string_ASNTyp_AML:
                case clsASNTyp.const_string_ASNTyp_AME:
                case clsASNTyp.const_string_ASNTyp_AVL:
                case clsASNTyp.const_string_ASNTyp_AVE:
                    if (myLager.Ausgang is clsLAusgang)
                    {
                        int iTmp = 0;
                        int.TryParse(myLager.Ausgang.SLB.ToString(), out iTmp);
                        if (iTmp > 0)
                        {
                            strTmp = const_Ausgang + iTmp.ToString("000000");
                        }
                    }
                    break;

            }
            return strTmp;
        }
    }
}
