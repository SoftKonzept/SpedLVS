namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class WerksnummerWithOutHyphen
    {
        public const string const_WerksnummerWithOutHyphen = "#WerksNrWithOutHyphen#";// Werksnummer mit BIndestich vor Index 00
        public static string Execute(string myWerksnummer)
        {
            string strReturn = myWerksnummer.Replace(" ", "");
            strReturn = strReturn.Replace("-", "");
            return strReturn;
        }
    }
}
