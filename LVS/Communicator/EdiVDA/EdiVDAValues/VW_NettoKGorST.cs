using System;

namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class VW_NettoKGorST
    {
        public const string const_VW_NettoKGorST = "#NettoKGorST#";

        ///<remarks>
        /// </remarks>

        public static string Execute(clsArtikel myArt)
        {
            object valObj = new object();
            string strReturn = string.Empty;
            myArt.GArt.ID = myArt.GArtID;
            myArt.GArt.Fill();
            if (myArt.GArt.ArtikelArt.IndexOf("Coil", StringComparison.CurrentCultureIgnoreCase) > -1)
            {
                valObj = Artikel_Netto.Execute(myArt);
                decimal decTmp = 0;
                if (decimal.TryParse(valObj.ToString(), out decTmp))
                {
                    decTmp = decTmp * 1000;
                }
                strReturn = Functions.FormatDecimalNoDiggits(decTmp);
            }
            else
            {
                valObj = Artikel_Anzahl.Execute(myArt);
                decimal decTmp = 0;
                if (decimal.TryParse(valObj.ToString(), out decTmp))
                {
                    decTmp = decTmp * 1000;
                }
                strReturn = Functions.FormatDecimalNoDiggits(decTmp);
            }
            return strReturn;
        }
    }
}
