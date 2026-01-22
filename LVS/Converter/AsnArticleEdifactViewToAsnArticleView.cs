using Common.Models;
using Common.Views;
using LVS.ViewData;
using LVS.Views;

namespace LVS.Converter
{
    public class AsnArticleEdifactViewToAsnArticleView
    {
        public AsnArticleView asnArticleView { get; set; } = new AsnArticleView();

        public AsnArticleEdifactViewToAsnArticleView(ctrASNRead_AsnArticleEdifactView myAsnArticleEdifactView)
        {
            if (myAsnArticleEdifactView != null)
            {
                asnArticleView = new AsnArticleView();

                asnArticleView.LfdNr = 1;
                asnArticleView.AsnId = myAsnArticleEdifactView.ASN;
                asnArticleView.Netto = myAsnArticleEdifactView.Netto;
                asnArticleView.Brutto = myAsnArticleEdifactView.Brutto;
                asnArticleView.Dicke = myAsnArticleEdifactView.Dicke;
                asnArticleView.Breite = myAsnArticleEdifactView.Breite;
                asnArticleView.Laenge = myAsnArticleEdifactView.Laenge;
                asnArticleView.Werksnummer = myAsnArticleEdifactView.Werksnummer;
                asnArticleView.Produktionsnummer = myAsnArticleEdifactView.Produktionsnummer;
                asnArticleView.Charge = myAsnArticleEdifactView.Charge;
                asnArticleView.Bestellnummer = myAsnArticleEdifactView.Bestellnummer;
                asnArticleView.exMaterialnummer = myAsnArticleEdifactView.exMaterialnummer;
                asnArticleView.Position = myAsnArticleEdifactView.Position;
                asnArticleView.Gut = string.Empty;
                asnArticleView.LfsNr = myAsnArticleEdifactView.LfsNr;
                asnArticleView.VehicleNo = myAsnArticleEdifactView.eingang.KFZ;
                asnArticleView.Workspace = null;
                asnArticleView.WorkspaceId = myAsnArticleEdifactView.eingang.ArbeitsbereichId;
                asnArticleView.IsSearchResult = true;

                if (myAsnArticleEdifactView.article.GArtID > 0)
                {
                    GoodstypeViewData gtVD = new GoodstypeViewData(myAsnArticleEdifactView.article.GArtID, 1, false);
                    asnArticleView.Gut = gtVD.Gut.Bezeichnung;
                }

                if ((myAsnArticleEdifactView.eingang is Eingaenge) && (myAsnArticleEdifactView.eingang.ArbeitsbereichId > 0))
                {
                    WorkspaceViewData wsVD = new WorkspaceViewData(myAsnArticleEdifactView.eingang.ArbeitsbereichId);
                    asnArticleView.Workspace = wsVD.Workspace;
                    asnArticleView.WorkspaceId = wsVD.Workspace.Id;
                }
            }
        }
    }
}
