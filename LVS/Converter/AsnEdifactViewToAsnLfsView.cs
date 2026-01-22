using Common.Views;
using LVS.ViewData;
using LVS.Views;

namespace LVS.Converter
{
    public class AsnEdifactViewToAsnLfsView
    {
        public AsnLfsView AsnLfsView { get; set; } = new AsnLfsView();
        public AsnEdifactViewToAsnLfsView(ctrASNRead_AsnEdifactView myAsnEdifactView)
        {
            if (myAsnEdifactView != null)
            {
                AddressViewData adrVD = new AddressViewData();

                AsnLfsView = new AsnLfsView();

                AsnLfsView.LfdNr = 1;
                AsnLfsView.AsnId = myAsnEdifactView.ASN;
                AsnLfsView.AsnDatum = myAsnEdifactView.ASN_Datum;
                AsnLfsView.VsDatum = myAsnEdifactView.ASN_Datum;
                AsnLfsView.LfsNr = myAsnEdifactView.LfsNr;
                AsnLfsView.TransportNr = myAsnEdifactView.ExTransportRef;
                AsnLfsView.Auftraggeber = myAsnEdifactView.eingang.Auftraggeber;
                AsnLfsView.AuftraggeberAdr = null;
                AsnLfsView.Empfaenger = myAsnEdifactView.eingang.Empfaenger;
                AsnLfsView.Transportmittel = myAsnEdifactView.eingang.KFZ;
                AsnLfsView.ASNRef = myAsnEdifactView.eingang.ASNRef;
                //AsnLfsView.Workspace = myAsnEdifactView.eingang.Workspace;
                //AsnLfsView.WorkspaceId = myAsnEdifactView.eingang.Workspace.Id;

                if (myAsnEdifactView.AsnMessage.WorkspaceId > 0)
                {
                    WorkspaceViewData wsVD = new WorkspaceViewData(myAsnEdifactView.AsnMessage.WorkspaceId);
                    AsnLfsView.Workspace = wsVD.Workspace;
                    AsnLfsView.WorkspaceId = wsVD.Workspace.Id;
                }

                if ((AsnLfsView.Auftraggeber > 0))
                {
                    adrVD = new AddressViewData(AsnLfsView.Auftraggeber, 1);
                    if (adrVD.Address != null)
                    {
                        AsnLfsView.AuftraggeberAdr = adrVD.Address.Copy();
                    }
                }
                if ((AsnLfsView.Empfaenger > 0))
                {
                    adrVD = new AddressViewData(AsnLfsView.Empfaenger, 1);
                    if (adrVD.Address != null)
                    {
                        AsnLfsView.EmpfaengerAdr = adrVD.Address.Copy();
                    }
                }


            }
        }
    }
}
