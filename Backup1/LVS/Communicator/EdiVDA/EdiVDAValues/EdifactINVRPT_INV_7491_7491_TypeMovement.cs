namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class EdifactINVRPT_INV_7491_7491_TypeMovement
    {
        public const string const_EdifactINVRPT_INV_7491_7491_TypeMovement = "#EdifactINVRPT_INV_7491_7491_TypeMovement#";

        /// <summary>
        ///             EDIFACT INVRPT 
        ///             INV|7491|7491
        ///             
        ///             Type of Inventory Movement affected Coded
        ///             
        ///             1 = Accepted product inventory
        ///             2 = Damaged product inventory
        ///             3 = Bonded inventory
        ///             4 = Reserved inventory
        ///             5 = Products for Inspection 
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
            strTmp = "1";

            //switch (myAsnTyp.Typ)
            //{
            //    case clsASNTyp.const_string_ASNTyp_EML:
            //    case clsASNTyp.const_string_ASNTyp_EME:
            //        //case clsASNTyp.const_string_ASNTyp_BML:
            //        //case clsASNTyp.const_string_ASNTyp_BME:
            //        //case clsASNTyp.const_string_ASNTyp_STE:
            //        //case clsASNTyp.const_string_ASNTyp_STL:
            //        //case clsASNTyp.const_string_ASNTyp_UBE:
            //        //case clsASNTyp.const_string_ASNTyp_UBL:
            //        //case clsASNTyp.const_string_ASNTyp_TSE:
            //        //case clsASNTyp.const_string_ASNTyp_TSL:

            //        strTmp = "2";
            //        break;


            //    case clsASNTyp.const_string_ASNTyp_AML:
            //    case clsASNTyp.const_string_ASNTyp_AME:
            //        //case clsASNTyp.const_string_ASNTyp_AVL:
            //        //case clsASNTyp.const_string_ASNTyp_AVE:
            //        //case clsASNTyp.const_string_ASNTyp_RLL:
            //        //case clsASNTyp.const_string_ASNTyp_RLE:

            //        strTmp = "1";
            //        break;

            //}
            return strTmp;
        }
    }
}
