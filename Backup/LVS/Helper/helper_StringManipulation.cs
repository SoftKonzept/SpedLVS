using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LVS
{
    public class helper_StringManipulation
    {

        public static string DeleteDatePrefix(string myString)
        {
            StringBuilder sb = new StringBuilder(string.Empty);
            List<char> StringAsChar = myString.ToCharArray().ToList();
            bool bSwitch = false;
            foreach (char strChar in StringAsChar)
            {
                if (
                        (bSwitch == false) &&
                        (
                            (strChar.Equals('_')) ||
                            (strChar.Equals('0')) ||
                            (strChar.Equals('1')) ||
                            (strChar.Equals('2')) ||
                            (strChar.Equals('3')) ||
                            (strChar.Equals('4')) ||
                            (strChar.Equals('5')) ||
                            (strChar.Equals('6')) ||
                            (strChar.Equals('7')) ||
                            (strChar.Equals('8')) ||
                            (strChar.Equals('9'))
                        )
                   )
                {
                }
                else
                {
                    bSwitch = true;
                    sb.Append(strChar);
                }
            }
            string strTmp = sb.ToString();
            return strTmp;
        }

    }
}
