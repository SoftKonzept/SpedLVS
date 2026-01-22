using LVS.Communicator.ZQM;
using LVS.Models;
using LVS.ViewData;
using System;
using System.Xml;

namespace Communicator.Controls.AdminCockpit
{
    public partial class frmAdminCockpit : Telerik.WinControls.UI.RadForm
    {
        public frmAdminCockpit()
        {
            InitializeComponent();
        }




        //---------------------------------------------------------------------------
        /// <summary>
        ///                         EdiZQMQalityXm
        /// </summary>

        private void btnReReadZQMQalityXml_Click(object sender, EventArgs e)
        {
            EdiZQMQalityXmlViewData zqmVD = new EdiZQMQalityXmlViewData();
            zqmVD.GetListActivItemsFrom(dtpEdiZQMQalityXmlFrom.Value);

            foreach (EdiZQMQalityXml item in zqmVD.ListZQMQalityActive)
            {
                if (!item.iDocXml.Equals(string.Empty))
                {
                    //-- CHeck Kontrolle, korrekte Daten in DB geschrieben
                    //-- XML erneut auslesen
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(item.iDocXml);
                    ZQM_Quality02 zqmCheck = new ZQM_Quality02(doc);

                    bool bUpdate = false;
                    ///--------------Vergleich
                    //-- LfsNr
                    if (!item.LfsNr.Equals(zqmCheck.LfsNo))
                    {
                        bUpdate = true;
                    }
                    if (!item.Produktionsnummer.Equals(zqmCheck.Produktionsnummer))
                    {
                        bUpdate = true;
                    }


                    if (bUpdate)
                    {
                        ArticleViewData artVD = new ArticleViewData();
                        artVD.SearchArtikelByProductionNoAndLfsNo(zqmCheck.Produktionsnummer, zqmCheck.LfsNo);

                        if (artVD.Artikel.Id > 0)
                        {
                            if (!item.ArticleId.Equals(artVD.Artikel.Id))
                            {
                                item.ArticleId = artVD.Artikel.Id;
                                item.WorkspaceId = artVD.Artikel.AbBereichID;
                                //ArticleViewData artVDTmp = new ArticleViewData(item.ArticleId, 1, false);
                                //if (
                                //      (artVDTmp.Artikel.Produktionsnummer.Equals())

                                //  )
                                //{ 

                                //}
                            }
                        }
                        item.Produktionsnummer = zqmCheck.Produktionsnummer;
                        item.LfsNr = zqmCheck.LfsNo;

                        item.Description = "ReRead - " + DateTime.Now.ToString("dd.MM.yyyy HH:MM:ss")
                                           + "ProdNr: " + zqmCheck.Produktionsnummer + Environment.NewLine
                                           + "LfsNr: " + zqmCheck.LfsNo + Environment.NewLine
                                           + item.Description;
                    }
                }
            }
        }
    }
}
