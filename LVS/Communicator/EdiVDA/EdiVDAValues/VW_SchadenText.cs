namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class VW_SchadenText
    {
        public const string const_VW_SchadenText = "#SchadenText#";

        ///<remarks>
        ///             Ermittel den ersten hinterlegten Schaden aller zugewiesenen Schäden
        /// </remarks>

        public static string Execute(clsArtikel myArt)
        {
            clsStringCheck StrCheck = new clsStringCheck();
            string strReturn = myArt.SchadenTopOne.Trim();
            StrCheck.CheckString(ref strReturn);
            return strReturn;
        }
    }
}
