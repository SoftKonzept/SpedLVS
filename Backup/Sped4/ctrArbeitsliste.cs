using LVS;
using Sped4.Classes;
using Sped4.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.UI;


namespace Sped4
{
    public partial class ctrArbeitsliste : UserControl
    {
        public Globals._GL_USER GL_User;
        internal ctrMenu _ctrMenu;
        internal ctrArtSearchFilter _ctrArtSearchFilter;
        internal frmTmp _frmTmp;
        internal BackgroundWorker bw;
        internal delegate void ThreadCtrInvokeEventHandler();

        internal List<string> ListAttachmentPath;
        internal string AttachmentPath = string.Empty;
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

        internal List<string> extraFelder = new List<string>();

        public Dictionary<decimal, clsAufgabe> aufgaben = new Dictionary<decimal, clsAufgabe>();

        /*******************************************************************************
         *                          Procedure / Methode
         * *****************************************************************************/
        ///<summary>ctrSearch2 </summary>
        ///<remarks></remarks>
        public ctrArbeitsliste()
        {
            InitializeComponent();
            this.splitPanel1.Collapsed = true;
        }
        ///<summary>ctrSearch2 / InitCtr</summary>
        ///<remarks></remarks>
        public void InitCtr()
        {
            InitFilterSearchCtr();
            Functions.InitComboViews(_ctrMenu._frmMain.GL_System, ref tscbView, "Bestand");
            Functions.InitComboViews(_ctrMenu._frmMain.GL_System, ref tscbPView, "Bestand", true);
            this.afColorLabel1.myText = "Suche... [Daten werden geladen]";
            bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            //bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.RunWorkerAsync();

            extraFelder.Add("Aufgaben");

            WidthSearchFilter = this._ctrArtSearchFilter.Width + 50;
            WidthSearchResult = this.dgvArtikel.Width + 20;
            this.splitPanel1.Width = WidthSearchFilter;
            ResizeCtr();

        }

        private void InitDGVArbeitsliste()
        {
            dtArbeitsliste = dtBestand.Copy();
            dtArbeitsliste.Clear();
            Functions.setPrintView(ref dtArbeitsliste, ref this.dgvArbeitsliste, "Bestand", tscbPView.SelectedItem.ToString(), _ctrMenu._frmMain.GL_System, "Selected", extraFelder);

            //Functions.setView();
        }

        public void test()
        {
            for (int i = 0; i < dtArbeitsliste.Rows.Count; i++)
            {
                if (aufgaben.ContainsKey((decimal)(dtArbeitsliste.Rows[i]["ArtikelID"])))
                {
                    dtArbeitsliste.Rows[i]["Aufgaben"] = aufgaben[(decimal)(dtArbeitsliste.Rows[i]["ArtikelID"])].ToString();
                }
            }

        }
        ///<summary>ctrSearch2 / bw_DoWork</summary>
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
        ///<summary>ctrSearch2 / bw_DoWork</summary>
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
                InitDGV(false);
            }
        }
        ///<summary>ctrSearch2 / InitDGV</summary>
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
                //switch (this._ctrArtSearchFilter.Lager.FilterSearchSpace)
                //{
                //    case 0:

                //        break;
                //    case 1:

                //        break;
                //    default:
                //        dtBestand = this._ctrArtSearchFilter.Lager.GetLagerDatenByFilter(this._ctrMenu._frmMain.system, true);
                //        break;
                //}


            }
            else
            {
                dtBestand = clsLager.GetAllLagerdaten(this._ctrMenu._frmMain.system, this.GL_User, true);
                dtBestand.DefaultView.RowFilter = string.Empty;

            }
            //this.dgvArtikel.DataSource = dtBestand;

            dts = new DataColumn[dtBestand.Columns.Count];
            dtBestand.Columns.CopyTo(dts, 0);

            Functions.setView(ref dtBestand, ref this.dgvArtikel, "Bestand", tscbView.SelectedItem.ToString(), _ctrMenu._frmMain.GL_System, true, null, false, "Selected");
            //AddColumnSelected(ref this.dgvArtikel, ref dtBestand);
            //this.dgvArtikel.BestFitColumns();
            if (this.dgvArtikel.RowCount > 0 && this.dgvArtikel.ColumnCount > 0)
            {
                this.dgvArtikel.Rows[0].Cells[0].IsSelected = true;
            }

            //dgvArtikel.
            InitDGVArbeitsliste();
        }


        public void clearFilter()
        {
            this.dgvArtikel.DataSource = null;
            dtBestand.DefaultView.RowFilter = string.Empty;
            dtBestand.DefaultView.RowFilter = "Selected=False";
            Functions.setView(ref dtBestand, ref this.dgvArtikel, "Bestand", tscbView.SelectedItem.ToString(), _ctrMenu._frmMain.GL_System, true, null, false, "Selected");
            if (this.dgvArtikel.RowCount > 0 && this.dgvArtikel.ColumnCount > 0)
            {
                this.dgvArtikel.Rows[0].Cells[0].IsSelected = true;
            }


        }
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
        ///<summary>ctrSearch2 / bw_RunWorkerCompleted</summary>
        ///<remarks></remarks>
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
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
                //TestSearch
                Functions.InitComboSearch(ref tscbSearch, dtBestand, this._ctrMenu._frmMain.system);
            }
            this.afColorLabel1.myText = strText;
        }
        ///<summary>ctrSearch2 / tstbSearchArtikel_TextChanged</summary>
        ///<remarks></remarks>
        private void tstbSearchArtikel_TextChanged(object sender, EventArgs e)
        {
            tstbSearchArtikel.Text = tstbSearchArtikel.Text.Trim();
            string strFilter = "";

            if (tstbSearchArtikel.Text != string.Empty)
            {
                strFilter = Functions.GetSearchFilterString(ref tscbSearch, tscbSearch.Text, tstbSearchArtikel.Text);

                dtBestand.DefaultView.RowFilter = strFilter;
                dtBestand.DefaultView.Sort = tscbSearch.Text + " , BKZ DESC";
            }
            else
            {
                dtBestand.DefaultView.Sort = string.Empty;
            }
        }
        ///<summary>ctrSearch2 / tsbtnShowAll_Click</summary>
        ///<remarks></remarks>
        private void tsbtnShowAll_Click(object sender, EventArgs e)
        {
            tscbSearch.SelectedIndex = -1;
            tstbSearchArtikel.Text = string.Empty;
            InitCtr();
        }
        ///<summary>ctrSearch2 / tsbtnTabkeOver_Click</summary>
        ///<remarks>Übergabe des Suchergebnisses und schliessen der Form</remarks>
        private void tsbtnTabkeOver_Click(object sender, EventArgs e)
        {
            this._frmTmp.CloseFrmTmp();
        }
        ///<summary>ctrSearch2 / tsbtnClose_Click</summary>
        ///<remarks></remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this._frmTmp.CloseFrmTmp();
        }
        ///<summary>ctrSearch2 / dgv_CellClick</summary>
        ///<remarks></remarks>
        private void dgv_CellClick(object sender, GridViewCellEventArgs e)
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
        ///<summary>ctrSearch2 / tsbtnShowSearchCtr_Click</summary>
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
        ///<summary>ctrSearch2 / ResizeCtr</summary>
        ///<remarks></remarks>
        private void ResizeCtr()
        {
            if (bShowSearchCTR)
            {
                this.Width = this.WidthSearchFilter + this.WidthSearchResult;
            }
            else
            {
                this.Width = this.Width - this.WidthSearchFilter;
            }
            this.Refresh();
        }

        private void tscbView_SelectedIndexChanged(object sender, EventArgs e)
        {
            object cbSearch = null;
            string search = String.Empty;
            if (tscbSearch.SelectedIndex > -1)
            {
                cbSearch = tscbSearch.SelectedItem;
                search = tstbSearchArtikel.Text;
            }
            bCombo = true;//Functions.setView(ref dtBestand, ref this.dgvArtikel, "Bestand", tscbView.SelectedItem.ToString(), _ctrMenu._frmMain.GL_System);
            //if (bw != null)
            //{
            //    this.afColorLabel1.myText = "Suche... [Daten werden geladen]";
            //    bw.RunWorkerAsync();
            //}
            Functions.setView(ref dtBestand, ref this.dgvArtikel, "Bestand", tscbView.SelectedItem.ToString(), _ctrMenu._frmMain.GL_System, true, null, false, "Selected");
            if (cbSearch != null)
            {
                tscbSearch.SelectedItem = cbSearch;
                tstbSearchArtikel.Text = search;
            }
        }

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

        private void tsbtnSelectedToArbeitsliste(object sender, EventArgs e)
        {

        }

        private void CopyDataRowstoListe(bool bAllVis = false)
        {
            dgvArtikel.DataSource = null;
            dgvArbeitsliste.DataSource = null;
            //Selektierten Artikel herausfiltern


            dtBestand.DefaultView.RowFilter = String.Empty;
            dtBestand.DefaultView.RowFilter = "Selected=True";

            DataTable dtTmpArtikel = dtBestand.DefaultView.ToTable();
            DataTable dtTmpAuftraggeber = dtBestand.DefaultView.ToTable();
            dtBestand.DefaultView.RowFilter = String.Empty;
            Functions.setView(ref dtBestand, ref this.dgvArtikel, "Bestand", tscbView.SelectedItem.ToString(), _ctrMenu._frmMain.GL_System, true, null, false, "Selected");
            Dictionary<string, List<DataRow>> toList = new Dictionary<string, List<DataRow>>();
            for (Int32 i = 0; i <= dtTmpArtikel.Rows.Count - 1; i++)
            {

                string auftraggeber = dtTmpArtikel.Rows[i]["Auftraggeber"].ToString();
                if (!toList.ContainsKey(auftraggeber))
                    toList.Add(auftraggeber, new List<DataRow>());
                toList[auftraggeber].Add(dtTmpArtikel.Rows[i]);

                dtTmpArtikel.Rows[i]["Selected"] = false;


            }
            foreach (KeyValuePair<string, List<DataRow>> kvp in toList)
            {
                dtTmpAuftraggeber.Clear();
                foreach (DataRow dr in kvp.Value)
                {
                    dtTmpAuftraggeber.ImportRow(dr);
                }
                DialogResult res = new frmDialog(new ctrArbeistlisteAufgaben(dtTmpAuftraggeber, kvp.Key), _ctrMenu, this).ShowDialog();
                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    for (int i = 0; i < dtTmpAuftraggeber.Rows.Count; i++)
                    {
                        dtArbeitsliste.ImportRow(dtTmpAuftraggeber.Rows[i]);
                    }


                }
            }

            //Nicht selektierten Artikel herausfiltern
            dtBestand.DefaultView.RowFilter = String.Empty;
            SetAllSelectedOrUnselected(ref dtBestand, false);



            //dtBestand.DefaultView.RowFilter = "Selected='False'";
            //Leeren der TmpTable
            //dtTmpArtikel.Clear();
            //Nicht selektierten Artikel in den TmpTable
            //dtTmpArtikel = dtBestand.DefaultView.ToTable();
            //ArtikelTable leeren, für die nicht selektierten Artikel
            //dtBestand.Clear();
            //nicht selektierten Aritkel in den ArtikelTable
            // dtBestand = dtTmpArtikel.DefaultView.ToTable();
            //dtArbeitsliste.DefaultView.RowFilter = String.Empty;
            //Functions.setView(ref dtBestand, ref this.dgvArtikel, "Bestand", tscbView.SelectedItem.ToString(), _ctrMenu._frmMain.GL_System, true, null, false, "Selected");
            //Functions.setView(ref dtArbeitsliste, ref this.dgvArbeitsliste, "Bestand", tscbView.SelectedItem.ToString(), this._ctrMenu._frmMain.GL_System, true, dts);
            Functions.setPrintView(ref dtArbeitsliste, ref this.dgvArbeitsliste, "Bestand", tscbPView.SelectedItem.ToString(), _ctrMenu._frmMain.GL_System, "Selected", extraFelder);
        }
        private void RemoveRowFromListe()
        {
            if (dtArbeitsliste.Rows.Count > 0)
            {
                Int32 i = 0;
                while ((i < dtArbeitsliste.Rows.Count) && (dtArbeitsliste.Rows.Count > 0))
                {
                    if (dtArbeitsliste.Rows[i]["Selected"] != null)
                    {
                        if ((bool)dtArbeitsliste.Rows[i]["Selected"])
                        {
                            decimal tmp = 0;
                            tmp = decimal.Parse(dtArbeitsliste.Rows[i]["ArtikelID"].ToString());
                            clsExtraCharge aufgabe = (clsExtraCharge)dtArbeitsliste.Rows[i]["Aufgaben"];


                            if (aufgaben[tmp].Count == 1)
                            {
                                aufgaben.Remove(tmp);
                            }
                            else if (aufgaben[tmp].Count > 1)
                            {
                                aufgaben[tmp].Remove(aufgabe);
                            }
                            dtArbeitsliste.Rows.RemoveAt(i);
                            i = -1;
                        }
                    }
                    i++;
                }
                //DGVAArtikelConnect();
            }
        }


        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (this.dgvArtikel != null && this.dgvArtikel.RowCount > 0)
            {
                clsPrint print = new clsPrint(this.dgvArbeitsliste, _ctrMenu._frmMain.GL_System, "Arbeitsliste", 1);
                print.printTitel = "";
                print.Print(true, false, _ctrMenu);
            }
        }

        private void tsbtnExcelExport_Click(object sender, EventArgs e)
        {
            FileName = DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + const_FileName + ".xls";
            saveFileDialog.InitialDirectory = AttachmentPath;
            saveFileDialog.FileName = AttachmentPath + "\\" + FileName;
            saveFileDialog.ShowDialog();
            FileName = saveFileDialog.FileName;

            if (saveFileDialog.FileName.Equals(String.Empty))
            {
                return;
            }

            bool openExportFile = false;
            Functions.Telerik_RunExportToExcelML(ref this._ctrMenu._frmMain, ref this.dgvArbeitsliste, FileName, ref openExportFile, this.GL_User, true);

            if (openExportFile)
            {
                try
                {
                    System.Diagnostics.Process.Start(FileName);
                }
                catch (Exception ex)
                {
                    clsError error = new clsError();
                    error._GL_User = this.GL_User;
                    error.Code = clsError.code1_501;
                    error.Aktion = const_Headline + " - Excelexport öffnen";
                    error.exceptText = ex.ToString();
                    error.WriteError();
                }
            }
        }

        private void SetAllSelectedOrUnselected(ref DataTable dt, bool bSelected)
        {
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {

                dt.Rows[i]["Selected"] = bSelected;
            }
        }
        private void dgvArbeitsliste_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            // Anlegen von Sonderkosten für den jeweiligen Kunden 

        }

        private void tsbtnSelectedToArbeitsliste_Click(object sender, EventArgs e)
        {

            CopyDataRowstoListe();
            //Functions.setPrintView(ref dtArbeitsliste, ref this.dgvArbeitsliste, "Bestand", tscbPView.SelectedItem.ToString(), _ctrMenu._frmMain.GL_System, "Selected", extraFelder);

        }

        private void tsbtnRemoveAllFromWorklist_Click(object sender, EventArgs e)
        {
            dgvArbeitsliste.Rows.Clear();
            aufgaben.Clear();
            // SetAllSelectedOrUnselected(ref dtArbeitsliste, true);
            // RemoveRowFromListe();
        }

        private void tsbtnRemoveSelectedFromWorklist_Click(object sender, EventArgs e)
        {
            RemoveRowFromListe();
        }

        private void tsbtnAllToWorklist_Click(object sender, EventArgs e)
        {
            //dtBestand.DefaultView.RowFilter = "Selected = true";
            //SetAllSelectedOrUnselected(ref dtBestand, true);
            //dtBestand.DefaultView.RowFilter = string.Empty;
            CopyDataRowstoListe(true);
            Functions.setPrintView(ref dtArbeitsliste, ref this.dgvArbeitsliste, "Bestand", tscbPView.SelectedItem.ToString(), _ctrMenu._frmMain.GL_System, "Selected", extraFelder);

        }

        private void tsbtnExtraChargeAssignment_Click(object sender, EventArgs e)
        {
            //bSetExtraChargeAssignmentToArt = true;
            this._ctrMenu.OpenCtrExtraChargeAssignment(this);
        }

        private void dgvArbeitsliste_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Value != null)
            {
                if (e.RowIndex > -1)
                {

                    if (this.dgvArbeitsliste.Columns[e.ColumnIndex].Name.Equals("Selected"))
                    {

                        if (this.dgvArbeitsliste.Rows[e.RowIndex].Cells["Selected"].Value.GetType() == System.DBNull.Value.GetType() || (bool)this.dgvArbeitsliste.Rows[e.RowIndex].Cells["Selected"].Value == false)
                        {
                            this.dgvArbeitsliste.Rows[e.RowIndex].Cells["Selected"].Value = true;
                        }
                        else
                        {
                            this.dgvArbeitsliste.Rows[e.RowIndex].Cells["Selected"].Value = false;
                        }
                    }
                }
            }
        }

        private void tscbPView_SelectedIndexChanged(object sender, EventArgs e)
        {
            Functions.setPrintView(ref dtArbeitsliste, ref this.dgvArbeitsliste, "Arbeitsliste", tscbPView.SelectedItem.ToString(), _ctrMenu._frmMain.GL_System, "Selected", extraFelder);
        }

        private void tsbtnSave_Click(object sender, EventArgs e)
        {
            lockGrids(true);
            foreach (KeyValuePair<decimal, clsAufgabe> kvp in aufgaben)
            {
                foreach (clsExtraCharge aufgabe in kvp.Value.aufgaben)
                {
                    clsExtraChargeAssignment ExtraChargeAssignment = new clsExtraChargeAssignment();
                    ExtraChargeAssignment.InitClass(this._ctrMenu._frmMain.GL_User);
                    //DataTable artikel = clsArtikel.GetArtikelInEingangByArtID(this._ctrMenu._frmMain.GL_User,kvp.Key);
                    clsArtikel art = new clsArtikel();
                    art.ID = kvp.Key;
                    art.GetArtikeldatenByTableID();
                    ExtraChargeAssignment.Add(aufgabe, true, art.ID, art.LEingangTableID, 1, DateTime.Now);
                }

            }

        }

        private void lockGrids(bool bLock)
        {
            tsbtbSelectedToWorklist.Enabled = !bLock;
            tsbtnAllToWorklist.Enabled = !bLock;
            tsbtnRemoveAllFromWorklist.Enabled = !bLock;
            tsbtnRemoveSelectedFromWorklist.Enabled = !bLock;
            dgvArbeitsliste.Enabled = !bLock;
            dgvArtikel.Enabled = !bLock;
            tsbtnPrint.Enabled = bLock;
            tsbtnExcelExport.Enabled = bLock;
        }
    }
}
