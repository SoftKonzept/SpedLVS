using System;

namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class VW_QTY_EinheitPCIorKGM
    {
        public const string const_VW_QTY_EinheitPCIorKGM = "#VW_QTY_EinheitPCIorKGM#";

        ///<remarks>
        ///             Unterscheidung der Menge / KG durch die Güterart                 
        ///             Güterart Coils  => Bruttogewicht wird übermittelt
        ///             sonst (Platinen)=> Anzahl wird übermittelt 
        /// </remarks>

        public static string Execute(clsArtikel myArt)
        {
            string strReturn = string.Empty;
            myArt.GArt.ID = myArt.GArtID;
            myArt.GArt.Fill();
            if (myArt.GArt.ArtikelArt.IndexOf("Coil", StringComparison.CurrentCultureIgnoreCase) > -1)
            {
                strReturn = "KGM";
            }
            else
            {
                strReturn = "PCE";
            }
            return strReturn;
        }
    }
}
