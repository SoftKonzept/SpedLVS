namespace LVS
{
    class SLE_spFunc
    {
        /// <summary>
        ///             GetVGS -> spezielle für GMH, da GMH den VGS 35 bei ein Eingangsmeldung erhält
        /// </summary>
        /// <param name="myASNTyp"></param>
        /// <param name="myLager"></param>
        /// <returns></returns>
        public string GetVGS(string myASNTyp)
        {
            string strReturn = string.Empty;

            switch (myASNTyp)
            {
                case "TSL":
                case "TSE":
                    strReturn = "32";
                    break;

                case "RLL":
                case "RLE":
                    strReturn = "33";
                    break;

                case "EML":
                case "EME":
                case "BML":
                case "BME":
                    strReturn = "35";
                    break;

                case "AML":
                case "AME":
                    strReturn = "36";
                    break;

                case "AVL":
                case "AVE":
                case "STE":
                case "STL":
                    strReturn = "40";
                    break;
            }
            return strReturn;
        }
    }
}
