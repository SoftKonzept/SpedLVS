namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class WerksnummerWithHyphen
    {
        public const string const_WerksnummerWithHyphen = "#WerksNrWithHyphen#";// Werksnummer mit BIndestich vor Index 00
        public static string Execute(string strWerksnummer)
        {
            string strTmp = string.Empty;
            strTmp = strWerksnummer;
            if ((!strTmp.Contains("-")) && (strTmp.Length > 2))
            {
                int fLength = strTmp.Length - 2;
                strTmp = strTmp.Insert(fLength, "-");
            }
            return strTmp;
        }
    }
}
