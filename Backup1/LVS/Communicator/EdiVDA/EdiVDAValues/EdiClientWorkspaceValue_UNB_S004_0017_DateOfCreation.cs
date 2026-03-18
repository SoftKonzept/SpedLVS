using LVS.ViewData;
using System;

namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class EdiClientWorkspaceValue_UNB_S004_0017_DateOfCreation
    {
        //-- alt muss aber erhlaten bleiben
        public const string const_EdiAdrWorkspaceAssign_UNB_S004_0017_DateOfCreation = "#UNB_S004_0017_DateOfCreation#";
        public const string const_EdiClientWorkspaceValue_Property = "UNB#S004#0017";
        public static string Execute(clsASN myAsn, clsASNTyp myAsnTyp, clsLagerdaten myLager)
        {
            string strTmp = DateTime.Now.ToString("yyyyMMdd");
            return strTmp;
        }
    }
}
