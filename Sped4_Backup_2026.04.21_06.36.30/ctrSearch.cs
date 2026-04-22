using LVS;
using Sped4.Properties;
using Sped4.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;


namespace Sped4
{
    public partial class ctrSearch : UserControl
    {
        public Globals._GL_USER GL_User;
        internal ctrMenu _ctrMenu;
        internal ctrUmbuchung _ctrUmbuchung;
        internal ctrEinlagerung _ctrEinlagerung;
        internal ctrAuslagerung _ctrAuslagerung;
        internal ctrArtSearchFilter _ctrArtSearchFilter;
        internal frmTmp _frmTmp;
        internal BackgroundWorker bw;
        internal delegate void ThreadCtrInvokeEventHandler();
        internal clsArtikel Artikel;

        internal string strFilter = string.Empty;
        internal DataTable dtBestand = new DataTable();
        //internal decimal decSelectedArtikelID = 0;
        internal bool bShowSearchCTR = true;
        internal Int32 WidthSearchFilter;
        internal Int32 WidthSearchResult;

        internal bool bCombo = false;
        internal bool bFilterAktiv = false;
        private bool bUseFilter = false;
        private bool bStandAlone = false;
        DataColumn[] dts;
        /*******************************************************************************
         *                          Procedure / Methode
         * *****************************************************************************/
        ///<summary>ctrSearch </summary>
        ///<remarks></remarks>
        public ctrSearch(bool bStandalone = false)
        {
            bStandAlone = bStandalone;
            InitializeComponent();
            this.splitPanel1.Collapsed = true;
        }
        ///<summary>ctrSearch / InitCtr</summary>
        ///<remarks></remarks>
        public void InitCtr()
        {
            tsbtnCreateAusgang.Visible = false;
            if (this._ctrAuslagerung != null)
            {
                tsbtnTabkeOver.Image = Sped4.Properties.Resources.box_out_24x24;
                tsbtnTabkeOver.Text = "ausgewählten Artikel im Ausgang anzeigen";

                switch (this._ctrAuslagerung.iStatusSearch)
                {
                    //0 = normale Suche
                    case ctrAuslagerung.const_StatusSearch_ArtikelSuche:
                        tsbtnTabkeOver.Visible = true;
                        tsbtnTabkeOverAusgang.Visible = true;
                        tsbtnCreateAusgang.Visible = false;
                        break;

                    case ctrAuslagerung.const_StatusSearch_ArtikelAuslagerung:
                        tsbtnTabkeOver.Visible = false;
                        tsbtnTabkeOverAusgang.Visible = false;
                        tsbtnCreateAusgang.Visible = true;
                        break;
                }
            }
            this.tsbtnTabkeOverAusgang.Visible = bStandAlone;

            this.Artikel = new clsArtikel();
            this.Artikel.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System);

            InitFilterSearchCtr();
            Functions.InitComboViews(_ctrMenu._frmMain.GL_System, ref tscbView, "Bestand");
            this.afColorLabel1.myText = "Suche... [Daten werden geladen]";
            bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            if (!bw.IsBusy)
            {
                bw.RunWorkerAsync();
            }
            WidthSearchFilter = this._ctrArtSearchFilter.Width + 50;
            WidthSearchResult = this.dgv.Width + 20;
            this.splitPanel1.Width = WidthSearchFilter;
            ResizeCtr();
        }
        ///<summary>ctrSearch / bw_DoWork</summary>
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
        ///<summary>ctrSearch / bw_DoWork</summary>
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
                InitDGV(this.bUseFilter);
            }
        }
        ///<summary>ctrSearch / runWorker</summary>
        ///<remarks></remarks>
        public void runWorker(bool bFilter = false)
        {
            this.bUseFilter = bFilter;
            if (!bw.IsBusy)
            {
                bw.RunWorkerAsync();
            }
            else
            {
                this.bUseFilter = false;
            }
        }
        ///<summary>ctrSearch / InitDGV</summary>
        ///<remarks></remarks>
        public void InitDGV(bool bSearchByFilter)
        {
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
                dtBestand = this._ctrArtSearchFilter.Lager.GetLagerDatenByFilter(this._ctrMenu._frmMain.system);
            }
            else
            {
                dtBestand = clsLager.GetAllLagerdaten(this._ctrMenu._frmMain.system, this.GL_User);
            }
            dtBestand.DefaultView.RowFilter = SetDGVFilter();
        }
        ///<summary>ctrSearch / SetDGVFilter</summary>
        ///<remarks></remarks>
        private string SetDGVFilter()
        {
            string strFilter = string.Empty;
            //Filter setzen
            if (this._ctrAuslagerung != null)
            {
                switch (this._ctrAuslagerung.iStatusSearch)
                {
                    //0 = normale Suche
                    case ctrAuslagerung.const_StatusSearch_ArtikelSuche:
                        strFilter = "Ausgang > 0";
                        break;

                    case ctrAuslagerung.const_StatusSearch_ArtikelAuslagerung:
                        strFilter = "Ausgang = 0 AND IsArtikelECheck=true AND IsEingangCheck=true";
                        break;
                }
            }
            if (this._ctrUmbuchung != null)
            {
                strFilter = "Ausgang = 0";
            }
            return strFilter;
        }
        ///<summary>ctrSearch / bw_RunWorkerCompleted</summary>
        ///<remarks></remarks>
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // AUSGELAGERT AUS INIT DGV
            try
            {
                this.dgv.DataSource = null;
                this.dgv.Rows.Clear();
                dts = new DataColumn[dtBestand.Columns.Count];
                dtBestand.Columns.CopyTo(dts, 0);

                Functions.setView(ref dtBestand, ref this.dgv, "Search", tscbView.SelectedItem.ToString(), _ctrMenu._frmMain.GL_System);
                this.dgv.BestFitColumns();
                if (this.dgv.RowCount > 0 && this.dgv.ColumnCount > 0)
                {
                    this.dgv.Rows[0].Cells[0].IsSelected = true;
                }
                // ENDE AUSGELAGERTER CODE 
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
                    if (this._ctrAuslagerung != null)
                    {
                        switch (this._ctrAuslagerung.iStatusSearch)
                        {
                            //0 = normale Suche
                            case ctrAuslagerung.const_StatusSearch_ArtikelSuche:
                                strText = "Suche... [Ausgangsdaten geladen - zur Suche bereit]";
                                break;

                            case ctrAuslagerung.const_StatusSearch_ArtikelAuslagerung:
                                strText = "Artikel geladen für Auslagerung... [Bestandsdaten geladen - zur Auslagerung bereit]";
                                break;
                        }
                    }
                    Functions.InitComboSearch(ref tscbSearch, dtBestand, this._ctrMenu._frmMain.system);
                }
                this.afColorLabel1.myText = strText;
                this.bUseFilter = false;
            }
            catch (Exception ex)
            {

            }
        }
        ///<summary>ctrSearch / tstbSearchArtikel_TextChanged</summary>
        ///<remarks></remarks>
        private void tstbSearchArtikel_TextChanged(object sender, EventArgs e)
        {
            //prüfen ob der direkte LifeTimeFilter angewendet werden soll
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_SearchGridInLifeTime)
            {
                tstbSearchArtikel.Text = tstbSearchArtikel.Text.Trim();
                string strTmpFilter = "";

                if (tstbSearchArtikel.Text != string.Empty)
                {
                    strTmpFilter = Functions.GetSearchFilterString(ref tscbSearch, tscbSearch.Text, tstbSearchArtikel.Text);
                    string Filter = SetDGVFilter();
                    if (!Filter.Equals(string.Empty))
                    {
                        Filter = Filter + " AND " + strTmpFilter;
                    }
                    else
                    {
                        Filter = strTmpFilter;
                    }
                    dtBestand.DefaultView.RowFilter = Filter;
                    if (dtBestand.Columns.Contains("BKZ"))
                    {
                        dtBestand.DefaultView.Sort = tscbSearch.Text + " , BKZ DESC";
                    }
                    else
                    {
                        dtBestand.DefaultView.Sort = tscbSearch.Text;
                    }
                }
                else
                {
                    dtBestand.DefaultView.Sort = string.Empty;
                }
            }
        }
        ///<summary>ctrSearch / tsbtnShowAll_Click</summary>
        ///<remarks></remarks>
        private void tsbtnShowAll_Click(object sender, EventArgs e)
        {
            tscbSearch.SelectedIndex = -1;
            tstbSearchArtikel.Text = string.Empty;
            InitCtr();
        }
        ///<summary>ctrSearch / tsbtnTabkeOver_Click</summary>
        ///<remarks>Übergabe des Suchergebnisses und schliessen der Form</remarks>
        private void tsbtnTabkeOver_Click(object sender, EventArgs e)
        {
            //Umbuchung
            if (this._ctrUmbuchung != null)
            {
                this._ctrUmbuchung._ArtikelIDTakeOver = this.Artikel.ID;
                //this._ctrUmbuchung._ArtikelIDTakeOver = decSelectedArtikelID;
                this._ctrUmbuchung.InitUmbuchung();
            }
            //Auslagerung
            else if (this._ctrAuslagerung != null)
            {
                this._ctrAuslagerung.SetSearchLAusgangToFrm(this.Artikel.ID);
                //this._ctrAuslagerung.SetSearchLAusgangToFrm(decSelectedArtikelID);
            }
            //Einlagerung
            else if (this._ctrEinlagerung != null)
            {
                if (this._ctrEinlagerung.Visible == false)
                {
                    this._ctrEinlagerung.Visible = true;
                }
                this._ctrEinlagerung.SetSearchLEingangskopfdatenToFrm(this.Artikel.ID);
                //this._ctrEinlagerung.SetSearchLEingangskopfdatenToFrm(decSelectedArtikelID);
            }
            else if (bStandAlone == true)
            {
                object obj = new object();
                this._ctrMenu.OpenCtrEinlagerung(obj);
                //this._ctrMenu._ctrEinlagerung.SetSearchLEingangskopfdatenToFrm(decSelectedArtikelID);
                this._ctrMenu._ctrEinlagerung.SetSearchLEingangskopfdatenToFrm(this.Artikel.ID);
            }
            //
            this._frmTmp.CloseFrmTmp();
        }
        ///<summary>ctrSearch / tsbtnClose_Click</summary>
        ///<remarks></remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this.afColorLabel1.Text = "Suche....[wird geschlossen]";
            this._ctrMenu._frmMain.InitStatusBar(0);
            this._frmTmp.CloseFrmTmp();
        }
        ///<summary>ctrSearch / dgv_CellClick</summary>
        ///<remarks></remarks>
        private void dgv_CellClick(object sender, GridViewCellEventArgs e)
        {
            decimal decTmp = 0;
            if (this.dgv.Rows.Count > 0)
            {
                if (e.RowIndex > -1)
                {
                    Decimal.TryParse(dgv.SelectedRows[0].Cells["ArtikelID"].Value.ToString(), out decTmp);
                    //Decimal.TryParse(this.dgv.Rows[e.RowIndex].Cells["ArtikelID"].Value.ToString(), out decTmp);
                    this.Artikel.ID = decTmp;
                    this.Artikel.GetArtikeldatenByTableID();
                }
            }
            //decSelectedArtikelID = decTmp;
        }
        ///<summary>ctrSearch / tsbtnShowSearchCtr_Click</summary>
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
        ///<summary>ctrSearch / ResizeCtr</summary>
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
        ///<summary>ctrSearch / tscbView_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void tscbView_SelectedIndexChanged(object sender, EventArgs e)
        {
            object cbSearch = null;
            string search = String.Empty;
            if (tscbSearch.SelectedIndex > -1)
            {
                cbSearch = tscbSearch.SelectedItem;
                search = tstbSearchArtikel.Text;
            }
            bCombo = true;
            Functions.setView(ref dtBestand, ref this.dgv, "Search", tscbView.SelectedItem.ToString(), _ctrMenu._frmMain.GL_System, true, dts);
            this.dgv.BestFitColumns();
            if (cbSearch != null)
            {
                tscbSearch.SelectedItem = cbSearch;
                tstbSearchArtikel.Text = search;
            }
            //Functions.InitComboSearch(ref tscbSearch, dtBestand);
            Functions.FillSearchColumnFromDGV(ref this.dgv, ref this.tscbSearch, this._ctrMenu._frmMain.system);
        }
        ///<summary>ctrSearch / dgv_SelectionChanged</summary>
        ///<remarks></remarks>
        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            if (this.dgv.Rows.Count > 0)
            {
                if (this.dgv.SelectedRows[0].Index > -1)
                {
                    Decimal.TryParse(this.dgv.Rows[dgv.SelectedRows[0].Index].Cells["ArtikelID"].Value.ToString(), out decTmp);
                }
            }
            //
            //decSelectedArtikelID = decTmp;
            this.Artikel.ID = decTmp;
            this.Artikel.GetArtikeldatenByTableID();
        }
        ///<summary>ctrSearch / dgv_ToolTipTextNeeded</summary>
        ///<remarks></remarks>
        private void dgv_ToolTipTextNeeded(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
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
        ///<summary>ctrSearch / toolStripButton1_Click</summary>
        ///<remarks></remarks>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DoSearchFilter();
        }
        ///<summary>ctrSearch / tstbSearchArtikel_KeyPress</summary>
        ///<remarks></remarks>
        private void tstbSearchArtikel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (this.tstbSearchArtikel.Text != string.Empty)
                {
                    DoSearchFilter();
                }
            }
        }
        ///<summary>ctrSearch / DoSearchFilter</summary>
        ///<remarks></remarks>
        private void DoSearchFilter()
        {
            if (tscbSearch.SelectedIndex > -1)
            {
                tstbSearchArtikel.Text = tstbSearchArtikel.Text.Trim();
                string strTmpFilter = "";

                if (tstbSearchArtikel.Text != string.Empty)
                {
                    strTmpFilter = Functions.GetSearchFilterString(ref tscbSearch, tscbSearch.Text, tstbSearchArtikel.Text);
                    string Filter = SetDGVFilter();
                    if (!Filter.Equals(string.Empty))
                    {
                        Filter = Filter + " AND " + strTmpFilter;
                    }
                    else
                    {
                        Filter = strTmpFilter;
                    }
                    ((DataTable)dgv.DataSource).DefaultView.RowFilter = Filter;
                    ((DataTable)dgv.DataSource).DefaultView.Sort = tscbSearch.Text + " , BKZ DESC";
                }
                else
                {
                    dtBestand.DefaultView.Sort = string.Empty;
                }
                //erste Zeile des Grids als Selected markieren
                if (this.dgv.Rows.Count > 0)
                {
                    this.dgv.Rows[0].IsCurrent = true;
                    this.dgv.Rows[0].IsSelected = true;
                }
            }
        }
        ///<summary>ctrSearch / tsbtnTabkeOverAusgang_Click</summary>
        ///<remarks></remarks>
        private void tsbtnTabkeOverAusgang_Click(object sender, EventArgs e)
        {
            if (this._ctrUmbuchung != null)
            {
                this._ctrUmbuchung._ArtikelIDTakeOver = this.Artikel.ID;
                //this._ctrUmbuchung._ArtikelIDTakeOver = decSelectedArtikelID;
                this._ctrUmbuchung.InitUmbuchung();
            }
            //Auslagerung
            else if (this._ctrAuslagerung != null)
            {
                this._ctrAuslagerung.SetSearchLAusgangToFrm(this.Artikel.ID);
                //this._ctrAuslagerung.SetSearchLAusgangToFrm(decSelectedArtikelID);
            }
            //Einlagerung
            else if (this._ctrEinlagerung != null)
            {
                if (this._ctrEinlagerung.Visible == false)
                {
                    this._ctrEinlagerung.Visible = true;
                }
                this._ctrEinlagerung.SetSearchLEingangskopfdatenToFrm(this.Artikel.ID);
                //this._ctrEinlagerung.SetSearchLEingangskopfdatenToFrm(decSelectedArtikelID);
            }
            else if (bStandAlone == true)
            {

                if (this.Artikel.LAusgangTableID > 0)
                {
                    object obj = new object();
                    this._ctrMenu.OpenCtrAuslagerung(obj);
                    this._ctrMenu._ctrAuslagerung.SetSearchLAusgangToFrm(this.Artikel.ID);
                }
                else
                {
                    clsMessages.Lager_AusgangasIDExistiertNicht();
                }


                //clsArtikel art = new clsArtikel();
                //art.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System);
                //art.ID = decSelectedArtikelID;
                //art.GetArtikeldatenByTableID();
                //if (art.LAusgangTableID > 0)
                //{
                //    object obj = new object();
                //    this._ctrMenu.OpenCtrAuslagerung(obj);
                //    this._ctrMenu._ctrAuslagerung.SetSearchLAusgangToFrm(decSelectedArtikelID);
                //}
                //else
                //{
                //    clsMessages.Lager_AusgangasIDExistiertNicht();
                //}
            }
            this._frmTmp.CloseFrmTmp();
        }
        ///<summary>ctrSearch / dgv_RowFormatting</summary>
        ///<remarks></remarks>
        private void dgv_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            Int32 ID = Convert.ToInt32(e.RowElement.RowInfo.Cells["ArtikelID"].Value);
            clsSPL spl = new clsSPL();
            spl.Fill();
            spl.ArtikelID = ID;
            spl.FillLastINByArtikelID();
            if (spl.IsInSPL)
            {
                e.RowElement.DrawFill = true;
                e.RowElement.GradientStyle = GradientStyles.Solid;
                e.RowElement.BackColor = Color.Red;
            }
            else
            {
                e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local);
                e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
                e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
            }
        }
        ///<summary>ctrSearch / tsbtnCreateAusgang_Click</summary>
        ///<remarks></remarks>
        private void tsbtnCreateAusgang_Click(object sender, EventArgs e)
        {
            if (this.dgv.Rows.Count > 0)
            {
                if (clsMessages.Lager_AuslagerungKomplett())
                {
                    clsLager tmpLager = new clsLager();
                    tmpLager.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system);
                    tmpLager.Artikel = this.Artikel;
                    tmpLager.Artikel.GetArtikeldatenByTableID();
                    tmpLager.Eingang.LEingangTableID = this.Artikel.LEingangTableID;
                    tmpLager.Eingang.FillEingang();

                    if (
                            (tmpLager.Artikel.EingangChecked) &&
                            (tmpLager.Eingang.Checked) &&
                            (tmpLager.Artikel.LAusgangTableID == 0) &&
                            (tmpLager.Artikel.ID > 0)
                        )
                    {
                        bool bProzessOK = false;
                        if (tmpLager.Artikel.bSPL)
                        {
                            List<decimal> listArtikelStoreOut = new List<decimal>();
                            listArtikelStoreOut.Add(tmpLager.Artikel.ID);
                            bProzessOK = tmpLager.ProzessStoreOutWithSPLOut(listArtikelStoreOut);
                        }
                        else
                        {
                            bProzessOK = tmpLager.ProzessStoreOut();
                        }
                        if (bProzessOK)
                        {
                            //LAgermeldung erstellen
                            this._ctrAuslagerung.Lager = tmpLager;
                            this._ctrAuslagerung.Lager.Ausgang.FillAusgang();
                            this._ctrAuslagerung.Lager.Artikel.GetArtikeldatenByTableID();
                            this._ctrAuslagerung.InitASNTransfer(clsASNAction.const_ASNAction_Ausgang);
                            this._ctrMenu._frmMain.InitStatusBar(0);
                            //zum Ausgang
                            this._ctrAuslagerung.JumpToAusgang(tmpLager.Ausgang.LAusgangID);
                            this._frmTmp.CloseFrmTmp();
                        }
                    }
                    else
                    {
                        string strError = "Es ist ein Fehler aufgetreten. Es konnte kein Ausgang erzeugt werden:" + Environment.NewLine +
                                           "Artikel ID: " + this.Artikel.ID.ToString() + Environment.NewLine +
                                           "LVS-Nr.: " + this.Artikel.LVS_ID.ToString() + Environment.NewLine +
                                           "Artikel LagerausgangstableID: " + this.Artikel.ID.ToString() + Environment.NewLine;
                        clsMessages.Allgemein_ERRORTextShow(strError);
                    }
                }
            }
            else
            {
                //Message
            }
        }




    }
}
