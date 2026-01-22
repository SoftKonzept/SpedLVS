using LVS.ASN.ASNFormatFunctions;

namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class VW_GIN_ML_C208
    {
        public const string const_VW_GIN_ML_C208 = "#VW_GIN_ML_C208#";

        ///<remarks>
        ///             GIN+ML+ DUNSNR : 
        ///             lt. Doku  VW
        ///             1JUN + DUNSNR (Lieferant SF) + Produktionsnummer
        ///             
        ///             lt. Absprache mit VW bei Installation der Anbindung
        ///             >>  1JUN + DUNSNR (Lieferant SF) + neue Sendungsnummer
        ///             
        ///             22 Zeichen Rückgabewert
        /// </remarks>

        public static string Execute(clsADR myAdr, string mySId)
        {
            string strTmp = FillTo9With0Left.Execute(mySId);
            //string strProd = FillTo9With0Left.Execute(myLager.Artikel.Produktionsnummer);
            //string strReturn = strTmp +  //an9
            //                   UNA.const_Trennzeichen_Gruppe_Doppelpunkt +
            //                   "1JUN" +
            //                   myAdr.DUNSNr.ToString() +
            //                   strTmp;
            // --- Länge 9 
            string C208_7402_an9 = strTmp;
            //--- Länge 22
            string strJun = "1JUN";
            string C208_7402_an22 = strJun + (myAdr.DUNSNr.ToString() + strTmp).PadLeft((22 - strJun.Length), '0');
            string strReturn = C208_7402_an9 + UNA.const_Trennzeichen_Gruppe_Doppelpunkt + C208_7402_an22;

            return strReturn;
        }
    }
}
