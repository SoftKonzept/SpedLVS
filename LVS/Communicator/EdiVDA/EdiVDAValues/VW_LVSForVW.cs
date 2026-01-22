namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class VW_LVSForVW
    {
        public const string const_VW_LVSForVW = "#LVSForVW#";

        ///<remarks>
        ///
        ///             Bei VW muss bei Umgebuchten Artikel die original LVS des "alten" Artikels an VW übermittelt werden
        ///             Also muss bei dem Artikel auf UB geprüft werden
        /// </remarks>

        public static string Execute(clsLagerdaten myLager)
        {
            string strReturn = myLager.Artikel.LVS_ID.ToString();
            if ((!myLager.Artikel.Umbuchung) && (myLager.Artikel.ArtIDAlt > 0))
            {
                strReturn = myLager.Artikel.LVSNrBeforeUB.ToString();
            }
            return strReturn;
        }
    }
}
