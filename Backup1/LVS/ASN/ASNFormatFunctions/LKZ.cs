using LVS.Helper;
using System.Collections.Generic;

namespace LVS
{
    public class LKZ
    {
        /// <summary>
        ///             Länderkennzeichen nach 
        ///             ISO-3166-1-Kodierliste
        /// </summary>


        public static string Execute(clsADR myAdr)
        {
            Dictionary<string, string> dicCounty = helper_Laenderkennzeichen.DicCountry();
            string resultCountry = string.Empty;
            string retValue = string.Empty;
            if (dicCounty.ContainsKey(myAdr.LKZ))
            {
                retValue = myAdr.LKZ;
            }
            else
            {
                foreach (KeyValuePair<string, string> item in dicCounty)
                {
                    if (item.Value.ToUpper().Equals(myAdr.Land.ToUpper()))
                    {
                        retValue = item.Key;
                    }
                }
            }

            return retValue;
        }
    }
}
