using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LVS
{
    class SZG_spFunc
    {
        ///<summary>clsVDACreate / GetTMN</summary>
        ///<remarks>TMN = Transportmittelnummer</remarks>
        public string GetTMN(string myASNTyp, ref clsLagerdaten myLager )
        {
            string strReturn = string.Empty;
            switch (myASNTyp)
            {
                case "EML":
                case "EME":
                case "BML":
                case "BME":
                    strReturn = myLager.Eingang.KFZ;
                    break;

                case "AML":
                case "AME":
                case "AVL":
                case "AVE":
                case "RLL":
                case "RLE":
                    strReturn = myLager.Ausgang.KFZ;
                    DateTime dtTmp = clsSystem.const_DefaultDateTimeValue_Min;
                    if (DateTime.TryParse(myLager.Ausgang.MAT.ToString(), out dtTmp))
                    {
                        strReturn = string.Format("{0:HHMM}", dtTmp);
                    }
                    break;
            }
            if (strReturn.Equals(string.Empty))
            {
                strReturn = "GC-SL 121";
            }
            return strReturn;
        }
    }
}
