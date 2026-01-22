namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class WerksnummerOhneBlank
    {
        public const string const_WerksnummerOhneBlank = "#WerksnummerOhneBlank#";
        public static string Execute(clsLagerdaten myLager)
        {
            string strReturn = myLager.Artikel.Werksnummer.Replace(" ", "");
            return strReturn;
        }
    }
}
