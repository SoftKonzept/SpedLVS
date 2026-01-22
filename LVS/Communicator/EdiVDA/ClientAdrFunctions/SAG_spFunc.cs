using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LVS
{
    class SAG_spFunc
    {
        ///<summary>clsVDACreate / SAG713F11WerkByWorkspace</summary>
        ///<remarks>Unterscheidung der Werke je Arbeitsbereich</remarks>
        public string SAG713F11WerkByWorkspace(Int32 myAbBereichID)
        {
            string strReturn = string.Empty;
            switch (myAbBereichID)
            {
                //VW
                case 1:
                    strReturn = "GK";
                    break;
                //BMW
                //case 2:
                //    strReturn = "CY";
                //    break;
            }
            return strReturn;
        }
    }
}
