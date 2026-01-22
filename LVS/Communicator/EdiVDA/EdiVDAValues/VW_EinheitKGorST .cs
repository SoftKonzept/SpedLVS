using System;

namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class VW_EinheitKGorST
    {
        public const string const_VW_EinheitKGorST = "#EinheitKGorSt#";

        ///<remarks>
        ///            Unterscheidung der Einheit STk / KG durch die Güterart                 
        ///            Güterart Coils => KG wird übermittelt
        ///            sonst(Platinen) => ST wird übermittelt
        /// </remarks>

        public static string Execute(clsArtikel myArt)
        {
            string strReturn = string.Empty;
            myArt.GArt.ID = myArt.GArtID;
            myArt.GArt.Fill();
            if (myArt.GArt.ArtikelArt.IndexOf("Coil", StringComparison.CurrentCultureIgnoreCase) > -1)
            {
                strReturn = "KG";
            }
            else
            {
                strReturn = "ST";
            }
            return strReturn;
        }
    }
}
