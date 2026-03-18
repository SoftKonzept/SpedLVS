using LVS;
using Sped4.Classes;
using Sped4.Properties;
using Sped4.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Sped4
{
    public partial class ctrWorklist : UserControl
    {
        public Globals._GL_USER GL_User;
        internal ctrMenu _ctrMenu;
        internal ctrArtSearchFilter _ctrArtSearchFilter;
        internal frmTmp _frmTmp;
        internal BackgroundWorker bw;
        internal delegate void ThreadCtrInvokeEventHandler();

        internal List<string> ListAttachmentPath;
        internal string AttachmentPath;
        internal string FileName;
        const string const_FileName = "_Arbeitsliste";
        const string const_Headline = "Arbeitsliste";
        const string viewName = "Arbeitsliste";

        internal string strFilter = string.Empty;
        internal DataTable dtBestand = new DataTable();
        internal DataTable dtArbeitsliste = new DataTable();
        internal decimal decSelectedArtikelID = 0;
        internal bool bShowSearchCTR = true;
        internal Int32 WidthSearchFilter;
        internal Int32 WidthSearchResult;

        internal bool bCombo = false;
        internal bool bFilterAktiv = false;
        DataColumn[] dts;
        public bool bSetExtraChargeAssignmentToArt;
        public Int32 iSearchADRButton = 1;

        internal bool bUseFilter = false;

        internal List<string> extraFelder = new List<string>();

        internal Dictionary<decimal, clsAufgabe> aufgaben = new Dictionary<decimal, clsAufgabe>();


        internal string strAuftraggeber = string.Empty;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        /*******************************************************************************
         *                          Procedure / Methode
         * *****************************************************************************/
        ///<summary>ctrWorklist </summary>
        ///<remarks></remarks>
        public ctrWorklist()
        {
            InitializeComponent();
            this.splitPanel1.Collapsed = true;

        }
        ///<summary>ctrWorklist / InitCtr</summary>
        ///<remarks></remarks>
        public void InitCtr()
        {
            InitFilterSearchCtr();
            Functions.InitComboViews(_ctrMenu._frmMain.GL_System, ref tscbView, "Bestand", false);
            //Functions.InitComboEC(_ctrMenu._frmMain.GL_System, ref tscbExtraCharge);
            tscbExtraCharge.ComboBox.DisplayMember = "Bezeichnung";
            tscbExtraCharge.ComboBox.ValueMember = "Bezeichnung";
            tscbExtraCharge.ComboBox.DataSource = clsExtraCharge.GetExtraCharge(GL_User);



            this.afColorLabel1.myText = "Suche... [Daten werden geladen]";
            bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            //bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            //bw.RunWorkerAsync();

            extraFelder.Add("Aufgaben");

            WidthSearchFilter = this._ctrArtSearchFilter.Width + 50;
            WidthSearchResult = this.dgvArtikel.Width;// +200; // this.lvExtraCharge.Width;//; + 20;
            this.splitPanel1.Width = WidthSearchFilter;
            ResizeCtr();

        }
        ///<summary>ctrWorklist / bw_DoWork</summary>
        ///<remarks></remarks>
        private void InitDGVArbeitsliste()
        {
            dtArbeitsliste = dtBestand.Copy();
            dtArbeitsliste.Clear();
        }
        ///<summary>ctrWorklist / bw_DoWork</summary>
        ///<remarks></remarks>
        private void InitFilterSearchCtr()
        {
            this.Text = "Anzeige Tourdetails";
            _ctrArtSearchFilter = new ctrArtSearchFilter();
            _ctrArtSearchFilter.InitCtr(this);
            _ctrArtSearchFilter.Dock = DockStyle.Fill;
            _ctrArtSearchFilter.Parent = this.splitPanel1;
            _ctrArtSearchFilter.Show();
            _ctrArtSearchFilter.BringToFront();
            this.splitPanel1.Width = _ctrArtSearchFilter.Width;
        }
        /// <summary>ctrWorklist / runworker</summary>
        /// <param name="bFilter"></param>
        public void runWorker(bool bFilter = false)
        {
            this.bFilterAktiv = bFilter;
            if (!bw.IsBusy)
            {
                bw.RunWorkerAsync();
            }
            else
            {
                this.bFilterAktiv = false;
            }
        }
        ///<summary>ctrWorklist / bw_DoWork</summary>
        ///<remarks></remarks>
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            if ((worker.CancellationPending == true))
            {
                e.Cancel = true;
            }
            else
            {
                LoadArtikel(false);
            }
        }
        ///<summary>ctrWorklist / LoadArtikel</summary>
        ///<remarks></remarks>
        private void LoadArtikel(bool p)
        {
            DataTable dtBestandWorklist = new DataTable();
            if (bFilterAktiv)
            {
                dtBestandWorklist = this._ctrArtSearchFilter.Lager.GetLagerDatenByFilter(this._ctrMenu._frmMain.system, true);

            }
            else
            {
                dtBestandWorklist = clsLager.GetAllLagerdaten(this._ctrMenu._frmMain.system, this.GL_User, true);
                dtBestandWorklist.DefaultView.RowFilter = string.Empty;
            }
            dtBestand = dtBestandWorklist;
        }
        ///<summary>ctrWorklist / InitDGV</summary>
        ///<remarks></remarks>
        public void InitDGV(bool bSearchByFilter)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            InitDGV(bSearchByFilter);
                                                                        }
                                                                    )
                                 );
                return;
            }
            if (bSearchByFilter || (bCombo == true && bFilterAktiv == true))
            {
                bFilterAktiv = true;
            }
            else
            {
                bFilterAktiv = false;
            }
            bCombo = false;
            if (bFilterAktiv)
            {
                dtBestand = this._ctrArtSearchFilter.Lager.GetLagerDatenByFilter(this._ctrMenu._frmMain.system, true);

            }
            else
            {
                dtBestand = clsLager.GetAllLagerdaten(this._ctrMenu._frmMain.system, this.GL_User, true);
                dtBestand.DefaultView.RowFilter = string.Empty;

            }
            //this.dgvArtikel.DataSource = dtBestand;

            dts = new DataColumn[dtBestand.Columns.Count];
            dtBestand.Columns.CopyTo(dts, 0);

            Functions.setView(ref dtBestand, ref this.dgvArtikel, "Bestand", tscbView.SelectedItem.ToString(), _ctrMenu._frmMain.GL_System, true, dts, true, "Selected");
            //AddColumnSelected(ref this.dgvArtikel, ref dtBestand);
            //this.dgvArtikel.BestFitColumns();
            if (this.dgvArtikel.RowCount > 0 && this.dgvArtikel.ColumnCount > 0)
            {
                this.dgvArtikel.Rows[0].Cells[0].IsSelected = true;
            }

            //dgvArtikel.
            InitDGVArbeitsliste();

        }
        ///<summary>ctrWorklist / clearFilter</summary>
        ///<remarks></remarks>
        public void clearFilter()
        {
            this.dgvArtikel.DataSource = null;
            dtBestand.DefaultView.RowFilter = string.Empty;
            dtBestand.DefaultView.RowFilter = "Selected=False";
            Functions.setView(ref dtBestand, ref this.dgvArtikel, "Bestand", tscbView.SelectedItem.ToString(), _ctrMenu._frmMain.GL_System, true, dts, false, "Selected");
            if (this.dgvArtikel.RowCount > 0 && this.dgvArtikel.ColumnCount > 0)
            {
                this.dgvArtikel.Rows[0].Cells[0].IsSelected = true;
            }


        }
        ///<summary>ctrWorklist / AddColumnSelected</summary>
        ///<remarks></remarks>
        private void AddColumnSelected(ref RadGridView myDgv, ref DataTable myDT)
        {
            myDgv.DataSource = null;
            if (myDT.Columns["Selected"] != null)
            {
                myDT.Columns["Selected"].ReadOnly = false;
                //            myDT.Columns["Selected"].DefaultValue = false;

                myDT.Columns["Selected"].SetOrdinal(0);
            }
            myDgv.DataSource = myDT;
        }
        ///<summary>ctrWorklist / bw_RunWorkerCompleted</summary>
        ///<remarks></remarks>
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // TMP BEGIN
            dts = new DataColumn[dtBestand.Columns.Count];
            dtBestand.Columns.CopyTo(dts, 0);

            Functions.setView(ref dtBestand, ref this.dgvArtikel, "Bestand", tscbView.SelectedItem.ToString(), _ctrMenu._frmMain.GL_System, true, dts, false, "Selected");
            //AddColumnSelected(ref this.dgvArtikel, ref dtBestand);
            //this.dgvArtikel.BestFitColumns();
            if (this.dgvArtikel.RowCount > 0 && this.dgvArtikel.ColumnCount > 0)
            {
                this.dgvArtikel.Rows[0].Cells[0].IsSelected = true;
            }

            //dgvArtikel.
            InitDGVArbeitsliste();

            // TMP ENDE

            string strText = string.Empty;
            if ((e.Cancelled == true))
            {
                strText = "Suche... [wurde unterbrochen]";
            }
            else if (!(e.Error == null))
            {
                strText = "Suche... [es sind Fehler aufgetreten]";
            }
            else
            {
                strText = "Suche... [Daten geladen - zur Suche bereit]";
            }
            this.afColorLabel1.myText = strText;
        }
        ///<summary>ctrWorklist / tsbtnTabkeOver_Click</summary>
        ///<remarks>Übergabe des Suchergebnisses und schliessen der Form</remarks>
        private void tsbtnTabkeOver_Click(object sender, EventArgs e)
        {
            this._frmTmp.CloseFrmTmp();
        }
        ///<summary>ctrWorklist / tsbtnClose_Click</summary>
        ///<remarks></remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        ///<summary>ctrWorklist / dgv_CellClick</summary>
        ///<remarks></remarks>
        private void dgvArtikel_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Value != null)
            {
                if (e.RowIndex > -1)
                {

                    if (this.dgvArtikel.Columns[e.ColumnIndex].Name.Equals("Selected"))
                    {

                        if (this.dgvArtikel.Rows[e.RowIndex].Cells["Selected"].Value.GetType() == System.DBNull.Value.GetType() || (bool)this.dgvArtikel.Rows[e.RowIndex].Cells["Selected"].Value == false)
                        {
                            this.dgvArtikel.Rows[e.RowIndex].Cells["Selected"].Value = true;
                        }
                        else
                        {
                            this.dgvArtikel.Rows[e.RowIndex].Cells["Selected"].Value = false;
                        }
                    }
                }
            }
        }
        ///<summary>ctrWorklist / tsbtnShowSearchCtr_Click</summary>
        ///<remarks></remarks>
        private void tsbtnShowSearchCtr_Click(object sender, EventArgs e)
        {
            if (bShowSearchCTR)
            {
                tsbtnShowSearchCtr.Image = Resources.layout_left;
                bShowSearchCTR = false;
            }
            else
            {
                tsbtnShowSearchCtr.Image = Resources.layout;
                bShowSearchCTR = true;
            }
            this.splitPanel1.Collapsed = bShowSearchCTR;
            ResizeCtr();
        }
        ///<summary>ctrWorklist / ResizeCtr</summary>
        ///<remarks></remarks>
        private void ResizeCtr()
        {
            if (bShowSearchCTR)
            {
                this.Width = this.WidthSearchFilter + this.WidthSearchResult;
            }
            else
            {
                this.Width = this.WidthSearchFilter + this.WidthSearchResult;
            }
            this.Refresh();
        }
        ///<summary>ctrWorklist / tscbView_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void tscbView_SelectedIndexChanged(object sender, EventArgs e)
        {
            object cbSearch = null;
            string search = String.Empty;

            bCombo = true;
            //Functions.setView(ref dtBestand, ref this.dgvArtikel, "Bestand", tscbView.SelectedItem.ToString(), _ctrMenu._frmMain.GL_System);
            Functions.setView(ref dtBestand, ref this.dgvArtikel, "Bestand", tscbView.SelectedItem.ToString(), _ctrMenu._frmMain.GL_System, true, dts, false, "Selected");

        }
        ///<summary>ctrWorklist / dgv_SelectionChanged</summary>
        ///<remarks></remarks>
        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            if (this.dgvArtikel.Rows.Count > 0)
            {
                if (this.dgvArtikel.SelectedRows[0].Index > -1)
                {
                    Decimal.TryParse(this.dgvArtikel.Rows[dgvArtikel.SelectedRows[0].Index].Cells["ArtikelID"].Value.ToString(), out decTmp);
                }

            }
            decSelectedArtikelID = decTmp;
        }
        ///<summary>ctrWorklist / toolStripButton5_Click</summary>
        ///<remarks></remarks>
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (this.dgvArtikel != null && this.dgvArtikel.RowCount > 0)
            {
                clsPrint print = new clsPrint(this.dgvArtikel, _ctrMenu._frmMain.GL_System, "Arbeitsliste", 1);
                print.printTitel = "[AUFGABE]";
                print.Print(true, true, _ctrMenu);
            }
        }
        ///<summary>ctrWorklist / tsbtnExcelExport_Click</summary>
        ///<remarks></remarks>
        private void tsbtnExcelExport_Click(object sender, EventArgs e)
        {
            //FileName = DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + const_FileName + ".xls";
            //saveFileDialog.InitialDirectory = AttachmentPath;
            //saveFileDialog.FileName = AttachmentPath + "\\" + FileName;
            //saveFileDialog.ShowDialog();
            //FileName = saveFileDialog.FileName;

            //if (saveFileDialog.FileName.Equals(String.Empty))
            //{
            //    return;
            //}

            //bool openExportFile = false;
            //Functions.Telerik_RunExportToExcelML(ref this._ctrMenu._frmMain, ref this.dgvArbeitsliste, FileName, ref openExportFile, this.GL_User, true);

            //if (openExportFile)
            //{
            //    try
            //    {
            //        System.Diagnostics.Process.Start(FileName);
            //    }
            //    catch (Exception ex)
            //    {
            //        clsError error = new clsError();
            //        error._GL_User = this.GL_User;
            //        error.Code = clsError.code1_501;
            //        error.Aktion = const_Headline + " - Excelexport öffnen";
            //        error.exceptText = ex.ToString();
            //        error.WriteError();
            //    }
            //}
        }
        ///<summary>ctrWorklist / tscbPView_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void tscbPView_SelectedIndexChanged(object sender, EventArgs e)
        {
            Functions.setView(ref dtBestand, ref this.dgvArtikel, "Arbeitsliste", tscbView.SelectedItem.ToString(), _ctrMenu._frmMain.GL_System, false, dts, false, "Selected", extraFelder);
        }
        ///<summary>ctrWorklist / ctrWorklist_Load</summary>
        ///<remarks></remarks>
        private void ctrWorklist_Load(object sender, EventArgs e)
        {
            InitCtr();
        }
        /// <summary>ctrWorklist  / tsbtnPrint_Click </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnPrint_Click(object sender, EventArgs e)
        {
            if (dgvArtikel.DataSource != null && dgvArtikel.Rows.Count > 0)
            {
                RadGridView tmp = new RadGridView();
                DataTable tmpDT = CopyDataRowstoListe(dtBestand);

                tmpDT.DefaultView.RowFilter = string.Empty;
                if (tmpDT != null && tmpDT.Rows.Count > 0)
                {
                    tmpDT.Columns["Produktionsnummer"].ColumnName = "Prod.-Nr.";
                    //clsPrint print = new clsPrint(tmp, _ctrMenu._frmMain.GL_System, viewName, 1);
                    clsPrint print = new clsPrint(tmpDT, _ctrMenu._frmMain.GL_System, viewName, 1);
                    string Titel = tscbExtraCharge.Text;
                    clsExtraCharge ExtraCharge = new clsExtraCharge();
                    try
                    {
                        if (this.tscbExtraCharge.Items.Count > 0)
                        {
                            ExtraCharge.ID = Decimal.Parse(((DataRowView)tscbExtraCharge.SelectedItem)["ID"].ToString());
                            ExtraCharge.Fill();
                            print.printTitel = clsADR.GetADRStringKDNrName(clsADR.GetIDByMatchcode(strAuftraggeber)).ToString() + " | " + Titel + " | ";
                            if (print.Print(true, true, _ctrMenu) == true)
                            {
                                for (int i = 0; i < tmpDT.Rows.Count; i++)
                                {
                                    clsExtraChargeAssignment ExtraChargeAssignment = new clsExtraChargeAssignment();
                                    ExtraChargeAssignment.InitClass(this._ctrMenu._frmMain.GL_User);
                                    clsArtikel art = new clsArtikel();
                                    art.ID = decimal.Parse(tmpDT.Rows[i]["ArtikelID"].ToString());
                                    art.GetArtikeldatenByTableID();
                                    ExtraChargeAssignment.Add(ExtraCharge, true, art.ID, art.LEingangTableID, 1, DateTime.Now);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }
        /// <summary>ctrWorklist  / CopyDataRowstoListe </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private DataTable CopyDataRowstoListe(DataTable dtArtikel)
        {
            dgvArtikel.DataSource = null;
            //Selektierten Artikel herausfiltern
            DataTable x = new DataTable();

            dtArtikel.DefaultView.RowFilter = String.Empty;
            dtArtikel.DefaultView.RowFilter = "Selected=True";
            DataTable dtTmp = dtBestand.DefaultView.ToTable();
            dtBestand.DefaultView.RowFilter = String.Empty;
            for (Int32 i = 0; i <= dtTmp.Rows.Count - 1; i++)
            {
                dtTmp.Rows[i]["Selected"] = false;
            }
            Functions.setView(ref dtBestand, ref this.dgvArtikel, "Bestand", tscbView.SelectedItem.ToString(), _ctrMenu._frmMain.GL_System, false, dts, false, "Selected", extraFelder);
            return dtTmp;

        }
        ///<summary>ctrWorklist / toolStripButton1_Click</summary>
        ///<remarks></remarks>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dtBestand.Rows.Count; i++)
            {

                dtBestand.Rows[i]["Selected"] = false;

            }
        }
        ///<summary>ctrWorklist / dgvArtikel_ToolTipTextNeeded</summary>
        ///<remarks></remarks
        private void dgvArtikel_ToolTipTextNeeded(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
        {
            GridDataCellElement cell = sender as GridDataCellElement;
            if (cell != null)
            {
                if (cell.ColumnInfo.Name == "Status")
                {
                    e.ToolTipText = string.Empty;
                    foreach (KeyValuePair<string, string> kvp in DictSettings.DicStatus())
                    {
                        e.ToolTipText += kvp.Key + " : " + kvp.Value + " \n";
                    }
                }
                else
                {
                    e.ToolTipText = cell.Value.ToString();
                }
            }
        }
        ///<summary>ctrWorklist / tscbExtraCharge_Click</summary>
        ///<remarks></remarks
        private void tscbExtraCharge_Click(object sender, EventArgs e)
        {
            if (dgvArtikel.DataSource != null && dgvArtikel.Rows.Count > 0)
            {
                RadGridView tmp = new RadGridView();
                DataTable tmpDT = CopyDataRowstoListe(dtBestand);
                tmpDT.DefaultView.RowFilter = string.Empty;
                if (tmpDT != null && tmpDT.Rows.Count > 0)
                {
                    tmpDT.Columns["Produktionsnummer"].ColumnName = "Prod.-Nr.";
                    clsPrint print = new clsPrint(tmpDT, _ctrMenu._frmMain.GL_System, viewName, 1);
                    string Titel = tscbExtraCharge.Text;
                    try
                    {
                        print.printTitel = clsADR.GetADRStringKDNrName(clsADR.GetIDByMatchcode(strAuftraggeber)).ToString() + " | " + Titel + " | ";
                        if (print.Print(false, true, _ctrMenu) == true)
                        {
                            //for (int i = 0; i < tmpDT.Rows.Count; i++)
                            //{
                            //   clsExtraChargeAssignment ExtraChargeAssignment = new clsExtraChargeAssignment();
                            //   ExtraChargeAssignment.InitClass(this._ctrMenu._frmMain.GL_User);
                            //   clsArtikel art = new clsArtikel();
                            //   art.ID = decimal.Parse(tmpDT.Rows[i]["ArtikelID"].ToString());
                            //   art.GetArtikeldatenByTableID();
                            //   ExtraChargeAssignment.Add(ExtraCharge, true, art.ID, art.LEingangTableID, 1);
                            //}
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }
        ///<summary>ctrWorklist/TakeOverAdrID</summary>
        ///<remarks></remarks>
        public void TakeOverAdrID(decimal myDecADR_ID)
        {
            if (myDecADR_ID > 0)
            {
                this._ctrArtSearchFilter.Lager.ADR.ID = myDecADR_ID;
                this._ctrArtSearchFilter.Lager.ADR.Fill();
                this._ctrArtSearchFilter.tbSearchA.Text = this._ctrArtSearchFilter.Lager.ADR.ViewID;
            }
        }

        private void tsbtnPrintOhneNK_Click(object sender, EventArgs e)
        {

        }
    }
}
