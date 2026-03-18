namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class PACNumber
    {
        public const string const_PACNumber = "#PACNumber#";

        /// <summary>
        ///             Verpackungsdaten für BMW
        ///             SON000 -> 
        ///             
        /// </summary>
        /// <param name="myArtikel"></param>
        /// <returns></returns>

        public static string Execute(clsArtikel myArtikel)
        {
            string strTmp = string.Empty;
            if ((myArtikel is clsArtikel) && (myArtikel.GArt is clsGut))
            {
                strTmp = myArtikel.GArt.Verpackung.Trim();
            }
            return strTmp;
        }
    }
}
