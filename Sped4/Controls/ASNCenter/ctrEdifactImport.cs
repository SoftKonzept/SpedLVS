using LVS;
using LVS.Helper;
using LVS.Models;
using LVS.ViewData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Sped4.Controls.ASNCenter
{
    public partial class ctrEdifactImport : UserControl
    {
        public Globals._GL_USER GLUser;
        internal int VdaClientOutAdrId { get; set; } = 0;
        internal LVS.ViewData.AsnArtViewData AsnArtVD { get; set; }
        //internal clsEdiSegmentElementField clsEdiSegmentElementField { get; set; }
        internal AsnArt SelectedAsnArtToImport { get; set; }
        internal List<string> SqlServerList { get; set; } = new List<string>();
        internal DataTable dtAsnArt { get; set; } = new DataTable();
        internal DataTable dtEdiSegmentElementFields { get; set; } = new DataTable();

        public ctrEdifactImport()
        {
            InitializeComponent();

            //--- akutell
            VdaClientOutAdrId = 195;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (this.SelectedAsnArtToImport.Id > 0)
            {
                int NewAsnArtId = 0;
                try
                {
                    int iEsSortId = 1;
                    int iEseSortId = 1;
                    int iEsefSortId = 1;

                    AsnArtViewData asnVDImported = new AsnArtViewData(this.SelectedAsnArtToImport);
                    //-- AsnArt anlegen
                    asnVDImported.Add();

                    if (asnVDImported.AsnArt.Id > 0)
                    {
                        NewAsnArtId = (int)asnVDImported.AsnArt.Id;

                        foreach (EdiSegments es in asnVDImported.AsnArt.ListEdiSegments)
                        {
                            //-- EdiSegment anlegen
                            if (es.Id > 0)
                            {
                                es.AsnArtId = NewAsnArtId;
                                es.SortId = iEsSortId;
                                EdiSegmentViewData esVDImported = new EdiSegmentViewData(es);
                                //if()
                                esVDImported.Add();
                                iEsSortId++;

                                if (esVDImported.EdiSegment.Id > 0)
                                {
                                    int NewEdiSegmentId = (int)esVDImported.EdiSegment.Id;

                                    foreach (EdiSegmentElements ese in es.ListEdiSegmentElements)
                                    {
                                        ese.EdiSegmentId = NewEdiSegmentId;
                                        ese.SortId = iEseSortId;

                                        EdiSegmentElementViewData eseVDImported = new EdiSegmentElementViewData(ese);
                                        eseVDImported.Add();
                                        iEseSortId++;

                                        if (eseVDImported.EdiSegmentElement.Id > 0)
                                        {
                                            int NewEdiSegmentElementId = (int)eseVDImported.EdiSegmentElement.Id;

                                            foreach (EdiSegmentElementFields esef in ese.ListEdiSegmentElementFields)
                                            {
                                                esef.EdiSemgentElementId = NewEdiSegmentElementId;
                                                esef.EdiSegmentId = NewEdiSegmentId;
                                                esef.AsnArtId = NewAsnArtId;
                                                esef.SortId = iEsefSortId;

                                                int EsefIdOld = (int)esef.Id;

                                                EdiSegmentElementFieldViewData esefVDImported = new EdiSegmentElementFieldViewData(esef);
                                                esefVDImported.Add();

                                                iEsefSortId++;

                                                //--VDAClientOut Datensatz für dieses EdiSegmentElementField anlegen
                                                VDAClientValues cv = new VDAClientValues();
                                                cv = VDAClientValueViewData.GetVdaClientValueToImport(GetSqlDiv(), VdaClientOutAdrId, EsefIdOld);
                                                if (cv.Id > 0)
                                                {
                                                    cv.AsnFieldId = (int)esefVDImported.EdiSegmentElementField.Id;
                                                    cv.AdrId = VdaClientOutAdrId;
                                                    cv.ASNArtId = NewAsnArtId;
                                                    cv.Kennung = esefVDImported.EdiSegmentElementField.Kennung;
                                                    cv.Description = "Import " + DateTime.Now.ToString("dd.MM.yyyy");

                                                    VDAClientValueViewData cvVDImport = new VDAClientValueViewData(cv);
                                                    cvVDImport.Add();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string str = string.Empty;
                    //--- rollback über AsnArt by AsnArtId
                    //--- rollback über EdiSegment by AsnArtId
                    //-- rollback über EdiSegmentElementField by AsnArtId
                    string strSql = "DELETE FROM EdiSegmentElementField WHERE ASNArtId=" + NewAsnArtId + " ; ";
                    strSql += "DELETE FROM EdiSegmentElement WHERE EdiSegmentId in (SELECT ID FROM EdiSegment WHERE ASNArtId=" + NewAsnArtId + "); ";
                    strSql += "DELETE FROM EdiSegment WHERE ASNArtId=" + NewAsnArtId + "; ";
                    bool bRollback = clsSQLCOM.ExecuteSQLWithTRANSACTION(strSql, "Rollback", 1);

                    string mes = "Es sind zu folgenden Problemen gekommen: " + Environment.NewLine;
                    mes += ex.Message + Environment.NewLine + Environment.NewLine;

                    if (bRollback)
                    {
                        mes += "Ein Rollback wurde durchgeführt!" + Environment.NewLine;
                    }
                    else
                    {
                        mes += "Es konnte KEIN Rollback durchgeführt werden!" + Environment.NewLine;
                    }
                    clsMessages.Allgemein_InfoTextShow(mes);
                }
            }
        }
        public void InitCtr()
        {
            //-- get SQL Server list
            SqlServerList = helper_sqlServers.GetListForSqlServers();
            comboSqlServerLists.DataSource = SqlServerList;

            //btnImport.Enabled = false;
        }

        private void dgvAsnArt_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.dgvAsnArt.RowCount > 0)
            {
                this.dgvAsnArt.CurrentRow.IsSelected = true;
            }
        }

        private void dgvAsnArt_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvAsnArt.RowCount > 0)
            {
                if (dgvAsnArt.SelectedRows.Count > 0)
                {
                    int iTmp = 0;
                    int.TryParse(dgvAsnArt.SelectedRows[0].Cells["ID"].Value.ToString(), out iTmp);
                    if (iTmp > 0)
                    {
                        SelectedAsnArtToImport = AsnArtViewData.GetAsnArtValueToImport(GetSqlDiv(), iTmp);
                        if (SelectedAsnArtToImport.Id > 0)
                        {
                            InitEdiSegmentDgv(SelectedAsnArtToImport.ListEdiSegments);
                        }
                    }
                }
            }
        }

        private void InitAsnArtDgv(List<AsnArt> myList)
        {
            this.dgvAsnArt.DataSource = myList;
            this.dgvAsnArt.BestFitColumns();
        }
        private void InitEdiSegmentDgv(List<EdiSegments> myList)
        {

            this.dgvEdiSegmentElementField.DataSource = myList;
            this.dgvAsnArt.BestFitColumns();
        }

        private void comboDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void comboSqlServerLists_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private clsSQLconComDiverse GetSqlDiv()
        {
            clsSQLconComDiverse sqlDiv = new clsSQLconComDiverse(string.Empty, string.Empty, string.Empty, string.Empty);
            string strServer = comboSqlServerLists.SelectedItem.ToString();
            string strDB = comboDatabase.SelectedItem.ToString();
            string strUser = comboUser.SelectedItem.ToString();
            string strPass = comboPass.SelectedItem.ToString();

            if (
                    (!strServer.Equals(string.Empty)) &&
                    (!strDB.Equals(string.Empty)) &&
                    (!strUser.Equals(string.Empty)) &&
                    (!strPass.Equals(string.Empty))
                )
            {
                sqlDiv = new clsSQLconComDiverse(strServer, strDB, strUser, strPass);
            }
            else
            {
                sqlDiv.ConnectionOK = false;
            }
            return sqlDiv;
        }
        private void btnCheckConnection_Click(object sender, EventArgs e)
        {
            clsSQLconComDiverse sqlDiv = GetSqlDiv();
            if (sqlDiv.ConnectionOK)
            {
                List<AsnArt> list = AsnArtViewData.GetAsnArtListToImport(sqlDiv);
                InitAsnArtDgv(list);
            }
        }



        private void tbPasswort_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
