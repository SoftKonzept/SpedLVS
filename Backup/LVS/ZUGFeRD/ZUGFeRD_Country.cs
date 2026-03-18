using Common.Models;
using s2industries.ZUGFeRD;

namespace LVS.ZUGFeRD
{
    public class ZUGFeRD_Country
    {
        public static CountryCodes ZUGFeRD_CountryCode(Addresses myAdr)
        {
            //CountryCodes cc = new CountryCodes();
            switch (myAdr.LKZ)
            {
                case "AD": return CountryCodes.AD;
                case "AT":
                case "A":
                    return CountryCodes.AT;
                case "BA": return CountryCodes.BA;
                case "BE": return CountryCodes.BE;
                case "BG": return CountryCodes.BG;
                case "CH": return CountryCodes.CH;
                case "CZ": return CountryCodes.CZ;
                case "DE": return CountryCodes.DE;
                case "DK": return CountryCodes.DK;
                case "ES": return CountryCodes.ES;
                case "FI": return CountryCodes.FI;
                case "FR": return CountryCodes.FR;
                case "GB": return CountryCodes.GB;
                case "HR": return CountryCodes.HR;
                case "HU": return CountryCodes.HU;
                case "IE": return CountryCodes.IE;
                case "IT": return CountryCodes.IT;
                case "LU": return CountryCodes.LU;
                case "ME": return CountryCodes.ME;
                case "NL": return CountryCodes.NL;
                case "NO": return CountryCodes.NO;
                case "PL": return CountryCodes.PL;
                case "PT": return CountryCodes.PT;
                case "RO": return CountryCodes.RO;
                case "RS": return CountryCodes.RS;
                case "RU": return CountryCodes.RU;
                case "SE": return CountryCodes.SE;
                case "SI": return CountryCodes.SI;
                case "SK": return CountryCodes.SK;
                default: return CountryCodes.DE;

            }
        }

    }
}
