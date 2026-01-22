using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace LVS
{
    public class clsStringCheck
    {
        internal Dictionary<Char, string> DictCriticalCharExChange = new Dictionary<char, string>()
        {
            {'ä',"ae"},
            {'Ä',"Ae"},
            {'ö',"oe"},
            {'Ö',"Oe"},
            {'ü',"ue"},
            {'Ü',"Ue"},
            {'ß',"ss"}
        };
        internal string strCriticalChar = "äÄöÖüÜß";
        internal Regex regEx;
        internal MatchEvaluator regEvaluator;


        /*****************************************************************************
         *                          Methoden
         * **************************************************************************/
        ///<summary>clsStringCheck / CheckString</summary>
        ///<remarks></remarks>
        public void CheckString(ref string myString)
        {
            regEx = new Regex("[" + strCriticalChar + "]");
            regEvaluator = new MatchEvaluator(StringReplace);
            myString = regEx.Replace(myString, regEvaluator);
        }
        ///<summary>clsStringCheck / StringReplace</summary>
        ///<remarks></remarks>
        public string StringReplace(Match m)
        {
            string reString = string.Empty;
            if (DictCriticalCharExChange.Keys.Contains(Convert.ToChar(m.Value)))
            {
                DictCriticalCharExChange.TryGetValue(Convert.ToChar(m.Value), out reString);
            }
            return reString;
        }

    }
}


