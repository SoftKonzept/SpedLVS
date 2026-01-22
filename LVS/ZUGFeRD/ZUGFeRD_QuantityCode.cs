using LVS.Models;
using s2industries.ZUGFeRD;

namespace LVS.ZUGFeRD
{
    public class ZUGFeRD_QuantityCode
    {
        /// <summary>
        ///                 namespace s2industries.ZUGFeRD;
        ///                 public enum QuantityCodes
        /// </summary>
        /// <param name="myItem"></param>
        /// <returns></returns>
        public static QuantityCodes ConvertToZUGFeRD_QuantityCode(InvoiceItems myItem)
        {
            QuantityCodes qc = new QuantityCodes();


            switch (myItem.BillingUnit.Replace(".", ""))
            {
                case "Anzahl":
                case "Anz":
                case "ANZAHL":
                case "ANZ":
                    return QuantityCodes.NAR;

                case "KMT":
                case "kmt":
                case "KM":
                case "km":
                    return QuantityCodes.KMT;

                case "Liter":
                case "liter":
                case "Ltr":
                case "LTR":
                    return QuantityCodes.LTR;

                case "Pauschal":
                case "pauschal":
                    return QuantityCodes.LS;

                case "Stunde":
                case "Std":
                case "stunde":
                case "std":
                    return QuantityCodes.HUR;

                case "Stk":
                case "stk":
                    return QuantityCodes.C62;

                case "Tag":
                case "Tage":
                case "tag":
                case "tage":
                    return QuantityCodes.DAY;

                case "TO":
                case "To":
                case "to":
                    return QuantityCodes.TNE;

                default: return QuantityCodes.TNE;
            }
        }

    }
}
