namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class WerksnummerMitFBlank
    {
        /// <summary>
        ///             Werksnummer mit führendem Blank
        /// </summary>
        public const string const_WerksnummerMitFBlank = "#WerksnummerMitFBlank#";



        public static string Execute(clsLagerdaten myLager)
        {
            string strReturn = " " + myLager.Artikel.Werksnummer;
            return strReturn;
        }
    }
}
