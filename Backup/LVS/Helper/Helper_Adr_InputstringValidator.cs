using System.Collections.Generic;
using System.Linq;

namespace LVS.Helper
{
    public class Helper_Adr_InputstringValidator
    {
        private static readonly char[] ForbiddenChars = { '\'', '"', '<', '>', ';', '\\', '/', };
        public bool ValidationOK { get; set; }

        public static string Validation(string myValidationString)
        {
            if (string.IsNullOrEmpty(myValidationString))
            {
                myValidationString = string.Empty;
                return myValidationString;
            }

            string cleaned = Helper_Adr_InputstringValidator.RemoveForbiddenChars(myValidationString);
            return cleaned;
        }
        public static string RemoveForbiddenChars(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return new string(input.Where(c => !ForbiddenChars.Contains(c)).ToArray());
        }

    }
}
