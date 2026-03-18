namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class EdifactINVRPT_INV_4499_4499_MovementReason
    {
        public const string const_EdifactINVRPT_INV_4499_4499_MovementReason = "#EdifactINVRPT_INV_4499_4499_MovementReason#";

        /// <summary>
        ///             EDIFACT INVRPT 
        ///             INV|4499|4499
        ///             
        ///             1 = Reception
        ///             2 = Delivery  
        ///             3 = Scrapped parts
        ///             4 = Difference
        ///             5 = Property transfer within warehouse
        ///             6 = Inventory recycling
        ///             7 = Reversal of previous movement
        ///             8 = Defects (technical)
        ///             9 = Commercial
        ///             10 = Conversion
        ///             11 = Consumption
        ///             
        ///             Standard values for Movement into consignment stock:
        ///             4501 -> “1”
        ///             7491 -> “1”
        ///             4499 -> “1”
        ///             4503 -> “”
        ///             INV+1+1+1++2:::10’
        /// 
        /// 
        ///             Standard values for Movement out of consignment stock:
        ///             4501 -> “2”
        ///             7491 -> “1”
        ///             4499 -> “11”
        ///             4503 -> “”
        ///             INV+2+1+11++2:::10’
        /// </summary>
        /// <param name="myAsnTyp"></param>
        /// <param name="myLager"></param>
        /// <returns></returns>

        public static string Execute(clsASNTyp myAsnTyp, clsLagerdaten myLager)
        {
            string strTmp = string.Empty;

            switch (myAsnTyp.Typ)
            {
                case clsASNTyp.const_string_ASNTyp_EML:
                case clsASNTyp.const_string_ASNTyp_EME:
                    //case clsASNTyp.const_string_ASNTyp_BML:
                    //case clsASNTyp.const_string_ASNTyp_BME:
                    //case clsASNTyp.const_string_ASNTyp_STE:
                    //case clsASNTyp.const_string_ASNTyp_STL:
                    //case clsASNTyp.const_string_ASNTyp_UBE:
                    //case clsASNTyp.const_string_ASNTyp_UBL:
                    //case clsASNTyp.const_string_ASNTyp_TSE:
                    //case clsASNTyp.const_string_ASNTyp_TSL:

                    strTmp = "1";
                    break;


                case clsASNTyp.const_string_ASNTyp_AML:
                case clsASNTyp.const_string_ASNTyp_AME:
                    //case clsASNTyp.const_string_ASNTyp_AVL:
                    //case clsASNTyp.const_string_ASNTyp_AVE:
                    //case clsASNTyp.const_string_ASNTyp_RLL:
                    //case clsASNTyp.const_string_ASNTyp_RLE:

                    strTmp = "11";
                    break;

            }
            return strTmp;
        }
    }
}
