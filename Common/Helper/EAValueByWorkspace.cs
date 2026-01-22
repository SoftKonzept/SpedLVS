using Common.Models;

namespace Common.Helper
{
    public class EAValueByWorkspace
    {

        public static void SetValueInitEingang(ref Eingaenge myEA, Workspaces myWorkspace)
        {
            myEA.Empfaenger = myWorkspace.WorkspaceOwner;
            myEA.EmpfaengerString = myWorkspace.WorkspaceOwnerAddress.AddressStringShort;

            myEA.EntladeID = myWorkspace.EingangDefEntladeId;
            myEA.BeladeID = myWorkspace.EingangDefBeladeId;
        }
        public static void SetValueInitAusgang(ref Ausgaenge myEA, Workspaces myWorkspace)
        {
            myEA.Empfaenger = myWorkspace.AusgangDefEmpfaengerId;
            myEA.Versender = myWorkspace.AusgangDefVersenderId;
            myEA.Entladestelle = myWorkspace.AusgangDefEntladeId;
            myEA.BeladeId = myWorkspace.AusgangDefBeladeId;
        }

        public static void SetAndCheckValueEingang(ref Eingaenge myEA, Workspaces myWorkspace)
        {
            if (myWorkspace.ASNTransfer)
            {
                myEA.Empfaenger = myWorkspace.WorkspaceOwner;
                myEA.EmpfaengerString = myWorkspace.WorkspaceOwnerAddress.AddressStringShort;
                myEA.EntladeID = myWorkspace.EingangDefEntladeId;
                myEA.BeladeID = myWorkspace.EingangDefBeladeId;
            }
            else
            {

            }
        }





    }
}
