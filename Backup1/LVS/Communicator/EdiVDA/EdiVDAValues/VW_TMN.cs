namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class VW_TMN
    {
        public const string const_SZG_TMN = "#VW_TMN#";

        ///<remarks>
        ///             TMN = Transportmittelnummer
        ///             extra für SZG / VW Sachsen
        /// </remarks>

        public static string Execute(string myASNTyp, ref clsLagerdaten myLager)
        {
            string strReturn = string.Empty;
            switch (myASNTyp)
            {
                case clsASNTyp.const_string_ASNTyp_EML:
                case clsASNTyp.const_string_ASNTyp_EME:
                case clsASNTyp.const_string_ASNTyp_BML:
                case clsASNTyp.const_string_ASNTyp_BME:
                    if (myLager.Eingang.KFZ != null)
                    {
                        strReturn = myLager.Eingang.KFZ;
                    }
                    break;

                case clsASNTyp.const_string_ASNTyp_AML:
                case clsASNTyp.const_string_ASNTyp_AME:
                case clsASNTyp.const_string_ASNTyp_AVL:
                case clsASNTyp.const_string_ASNTyp_AVE:
                case clsASNTyp.const_string_ASNTyp_RLL:
                case clsASNTyp.const_string_ASNTyp_RLE:
                    if (myLager.Ausgang.KFZ != null)
                    {
                        strReturn = myLager.Ausgang.KFZ;
                    }
                    break;
            }
            if (strReturn.Equals(string.Empty))
            {
                strReturn = "GC-SL 121";
            }
            return strReturn;
        }
    }
}
