namespace LVS.Converter
{
    public class EdifactToTextToSaveInDBConverter
    {
        public static char[] TrimStartChars = { '#' };

        public const string const_EdiValueIn_ReplaceValue = "#";

        public const string const_EdiValueOut_ReplaceValue = "'";
        public static string ConvertEdifactToTextToDb(string myEdi)
        {
            string strReturn = string.Empty;
            if ((myEdi != null) && (myEdi.Length > 0))
            {
                strReturn = myEdi.Replace(const_EdiValueOut_ReplaceValue, const_EdiValueIn_ReplaceValue);
                strReturn = const_EdiValueIn_ReplaceValue + strReturn + const_EdiValueIn_ReplaceValue;
            }
            return strReturn;
        }
        public static string ConvertTextFromDBToEdifact(string myText)
        {
            string strReturn = string.Empty;
            if ((myText != null) && (myText.Length > 0))
            {
                myText = myText.TrimStart(TrimStartChars);
                myText = myText.TrimEnd(TrimStartChars);
                strReturn = myText.Replace(const_EdiValueIn_ReplaceValue, const_EdiValueOut_ReplaceValue);
            }
            return strReturn;
        }
    }
}
