namespace Common.Helper
{
    public class StringValueEdit
    {
        public static string RemoveFirtsCharFromValue(string value)
        {
            string strReturn = string.Empty;
            if (value != null)
            {
                if (value.Length > 2)
                {
                    strReturn = value.Substring(1, value.Length - 1);
                }
                else
                {
                    strReturn = value;
                }
            }
            return strReturn;
        }
        /// <summary>
        ///             Letzte Zeichen soll gelöscht werden
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveLastCharFromValue(string value)
        {
            string strReturn = string.Empty;
            if (value != null)
            {
                if (value.Length > 2)
                {
                    strReturn = value.Substring(0, value.Length - 1);
                }
                else
                {
                    strReturn = value;
                }
            }
            return strReturn;
        }
    }
}
