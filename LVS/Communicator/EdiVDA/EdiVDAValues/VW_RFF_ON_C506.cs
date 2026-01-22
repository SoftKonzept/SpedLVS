namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class VW_RFF_ON_C506
    {
        public const string const_VW_RFF_ON_C506 = "#VW_RFF_ON_C506#";

        ///<remarks>
        ///             RF+ON+ Bestellnummer
        ///             
        ///             Bestellnummer
        ///             12 Zeichen max. Rückgabewert
        /// </remarks>

        public static string Execute(clsArtikel myArt)
        {
            string strTmp = string.Empty;
            if (myArt.Bestellnummer.Length > 12)
            {
                strTmp = myArt.Bestellnummer.Substring(0, 12);
            }
            else
            {
                strTmp = myArt.Bestellnummer;
            }
            string strReturn = strTmp;
            return strReturn;
        }
    }
}
