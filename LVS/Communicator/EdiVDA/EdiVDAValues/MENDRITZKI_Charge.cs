using System.Linq;

namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class MENDRITZKI_Charge
    {
        public const string const_MENDRITZKI_Charge = "#MENDRITZKI_Charge#";

        public static string Execute(clsArtikel myArtikel)
        {
            string strTmp = string.Empty;
            if (myArtikel is clsArtikel)
            {
                int count = myArtikel.exMaterialnummer.ToString().Count(c => c == '-');
                if (count > 1)
                {
                    int lastDashIndex = myArtikel.exMaterialnummer.ToString().LastIndexOf('-');
                    if (lastDashIndex > 0)
                    {
                        strTmp = myArtikel.exMaterialnummer.ToString().Substring(0, lastDashIndex); // Schneidet den String bis zum letzten Bindestrich                       
                    }
                }
                else
                {
                    strTmp = myArtikel.exMaterialnummer;
                }
            }
            myArtikel.Charge = strTmp;
            return strTmp;
        }
    }
}
