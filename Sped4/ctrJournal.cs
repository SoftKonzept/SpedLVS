using LVS;
using Sped4.Classes;
using Sped4.Classes.TelerikCls;
using Sped4.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Localization;


namespace Sped4
{
    public partial class ctrJournal : UserControl
    {
        const string const_FileName = "_Journal";
        const string const_Headline = "Journal";
        public const string const_ControlName = "Journal";
        const string viewName = "Journal";
        public DateTime FileDateForMail;
        public string strJournalTyp = string.Empty;
        public string strJournalZeitraum = string.Empty;
        public Globals._GL_USER GL_User;
        internal ctrMenu _ctrMenu;
        internal ctrArtSearchFilter _ctrArtSearchFilter;
        internal clsLager Lager = new clsLager();
        internal DataTable dtJournal = new DataTable();
        //internal bool bFirstGrdLoad = true;
        public frmTmp _frmTmp;
        internal DataTable dtCustomWithRate;

        internal Int32 SearchButton = 0;
        internal List<string> ListAttachmentPath;
        internal string strAttachmentPath = string.Empty;
        internal string strFileName = string.Empty;

        internal string SelectedValue = string.Empty;
        DataColumn[] dts;


        public ctrJournal()
        {
            InitializeComponent();

            //comboBox Auftraggeber laden
            dtCustomWithRate = new DataTable("KundenTarif");
            dtCustomWithRate = clsKundenTarife.GetGetKundenWithTarif(this.GL_User);
            this.afColorLabel1.myText = const_Headline;
            this.tscbSort.SelectedIndex = 0;
            this.tscbGroup.SelectedIndex = 0;
        }
        ///<summary>ctrJournal / ctrJournal_Load</summary>
        ///<remarks>Beim Laden des CTR werden folgende Aktionen durchgeführt:
        ///         - Datum von / bis muss gesetzt werden
        ///         - entsprechende Datensätze werden ermittelt
        ///         - Berechnung Anzahl / Gewicht der Datensätze</remarks>
        private void ctrJournal_Load(object sender, EventArgs e)
        {
            RadGridLocalizationProvider.CurrentProvider = new clsGermanRadGridLocalizationProvider();
            SetAuswahlJournalDaten();
            InitFilterSearchCtr();
            Functions.InitComboViews(_ctrMenu._frmMain.GL_System, ref tsbcViews, const_Headline);
            CustomerSettings();
            SetComTECSettings();
        }
        ///<summary>ctrFaktLager/ SetComTECSettings</summary>
        ///<remarks></remarks>
        private void SetComTECSettings()
        {
            this.rDgv.EnableCustomFiltering = false;
            string strAdmin = "Administrator";
            if (
                (this.GL_User.LoginName.ToString().ToUpper() == "ADMINISTRATOR")
                ||
                (this.GL_User.LoginName.ToString().ToUpper() == "ADMIN")
                )
            {
                this.rDgv.ShowFilteringRow = true;
            }
            else
            {
                this.rDgv.ShowFilteringRow = false;
            }
        }
        ///<summary>ctrBestand / InitFilterSearchCtr</summary>
        ///<remarks></remarks>
        private void InitFilterSearchCtr()
        {
            _ctrArtSearchFilter = new ctrArtSearchFilter();
            _ctrArtSearchFilter.InitCtr(this);
            _ctrArtSearchFilter.Dock = DockStyle.Fill;
            _ctrArtSearchFilter.Parent = this.splitPanel1;
            _ctrArtSearchFilter.Show();
            _ctrArtSearchFilter.BringToFront();
            this.splitPanel1.Width = _ctrArtSearchFilter.Width + 5;
        }
        ///<summary>ctrBestand / CustomerSettings</summary>
        ///<remarks></remarks>
        private void CustomerSettings()
        {
            //Erweiterete Suche 
            this.tsbtnSearchShow.Visible = this._ctrMenu._frmMain.system.Client.Modul.EnableAdvancedSearch;
            //DirectSearch
            this.tscbSearch.Visible = this._ctrMenu._frmMain.system.Client.Modul.EnableDirectSearch;
            this.tstbSearchArtikel.Visible = this._ctrMenu._frmMain.system.Client.Modul.EnableDirectSearch;
            this.tslSearchText.Visible = this._ctrMenu._frmMain.system.Client.Modul.EnableDirectSearch;

            //Submenu 
            //aktuell keine andere Lösung            
            switch (this._ctrMenu._frmMain.system.Client.MatchCode)
            {
                //SLE
                case (clsClient.const_ClientMatchcode_SLE + "_"):
                    this.menuSubFilter.Visible = true;
                    break;
                default:
                    this.menuSubFilter.Visible = false;
                    break;
            }

        }
        ///<summary>ctrJournal / SetAuswahlJournalDaten</summary>
        ///<remarks>Setzt die Standardwerte</remarks>
        private void SetAuswahlJournalDaten()
        {
            //Datum
            string strTmp = "01." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Year.ToString();
            dtpVon.Value = Convert.ToDateTime(strTmp);
            dtpBis.Value = DateTime.Now.Date.AddDays(1);
            //Bestandsarten Combo
            cbJournalart.DataSource = ctrJournalSettings.InitTableJournalarten();
            cbJournalart.DisplayMember = "Journalart";
            cbJournalart.ValueMember = "ID";
        }
        ///<summary>ctrJournal / InitDGV</summary>
        ///<remarks></remarks>
        private void InitDGV()
        {
            this._ctrMenu._frmMain.InitStatusBar(3);
            this._ctrMenu._frmMain.StatusBarWork(false, string.Empty);

            dtJournal.Clear();
            Lager = new clsLager();
            Lager._GL_User = this.GL_User;
            Lager.BestandVon = dtpVon.Value;
            Lager.BestandBis = dtpBis.Value;
            Lager.AbBereichID = this._ctrMenu._frmMain.system.AbBereich.ID;
            Lager.RLJournalExcl = cbRLExcl.Checked;
            Lager.SchadenJournalExcl = cbSchadenExcl.Checked;

            //wenn der Filter ausgewählt ist...
            if (cbFilter.Checked)
            {
                Lager.bFilterJournal = cbFilter.Checked;
                Lager.InitSubClasses();
                decimal decTmp = 0;
                Decimal.TryParse(comboAuftraggeber.SelectedValue.ToString(), out decTmp);
                Lager.ADR.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, decTmp, false);

                //Tarif
                if (comboTarif.SelectedValue != null)
                {
                    decTmp = 0;
                    Decimal.TryParse(comboTarif.SelectedValue.ToString(), out decTmp);
                    Lager.ADR.Kunde.Tarif.ID = decTmp;
                    Lager.ADR.Kunde.Tarif.Fill();
                }
            }

            dtJournal = Lager.GetJournalDaten((Int32)cbJournalart.SelectedValue, tscbGroup.Text);
            dts = new DataColumn[dtJournal.Columns.Count];
            dtJournal.Columns.CopyTo(dts, 0);

            //wenn die Spalte ausbuchen (Sperrlager) vorhanden ist, so soll diese
            //erst gelöscht werden
            if (dtJournal.Columns["ausbuchen"] != null)
            {
                dtJournal.Columns.Remove("ausbuchen");
            }
            this._ctrMenu._frmMain.InitStatusBar(dtJournal.Rows.Count);

            this._ctrMenu._frmMain.StatusBarWork(false, "Daten werden geladen und initialisiert...");

            switch ((Int32)cbJournalart.SelectedValue)
            {
                case 5:
                    Functions.setView(ref dtJournal, ref rDgv, clsClient.const_ViewKategorie_Journal, clsClient.const_ViewName_JournalSPL, this._ctrMenu._frmMain.GL_System, false, dts);
                    break;

                default:
                    Functions.setView(ref dtJournal, ref rDgv, clsClient.const_ViewKategorie_Journal, tsbcViews.SelectedItem.ToString(), this._ctrMenu._frmMain.GL_System, false, dts);
                    break;
            }


            //if (bFirstGrdLoad)
            //{
            if (this.rDgv.Columns.Count > 0)
            {
                this.rDgv.SummaryRowsTop.Clear();
                string ColName = this.rDgv.Columns[0].Name.ToString();
                GridViewSummaryItem sumDaten = new GridViewSummaryItem(ColName, "Datensätze: {0}", GridAggregateFunction.Count);

                //GridViewSummaryItem sumDaten = new GridViewSummaryItem("ArtikelID", "Datensätze: {0}", GridAggregateFunction.Count);
                GridViewSummaryItem sumAnzahl = new GridViewSummaryItem("Anzahl", "Gesamt [Stk]: {0}", GridAggregateFunction.Sum);
                GridViewSummaryItem sumNetto = new GridViewSummaryItem("Netto", "Gesamt [KG]: {0:N2}", GridAggregateFunction.Sum);
                GridViewSummaryItem sumBrutto = new GridViewSummaryItem("Brutto", "Gesamt [KG]: {0:N2}", GridAggregateFunction.Sum);
                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem(
                    new GridViewSummaryItem[] { sumDaten, sumAnzahl, sumNetto, sumBrutto });
                this.rDgv.SummaryRowsTop.Add(summaryRowItem);
                //bFirstGrdLoad = false;
            }
            //}
            //this.rDgv.BestFitColumns();
            if (this._ctrArtSearchFilter != null)
            {
                this._ctrArtSearchFilter.ClearFilterInput();
                this._ctrArtSearchFilter.SetFilterSearchElementAllEnabled(false);
                this._ctrArtSearchFilter.SetFilterElementEnabledByColumns(ref this.rDgv);
            }
            this._ctrMenu._frmMain.StatusBarWork(false, string.Empty);

            //SEARCH
            Functions.InitComboSearch(ref tscbSearch, dtJournal, this._ctrMenu._frmMain.system);

            //SORT
            if (this.tscbSort.SelectedIndex > 0)
            {
                SortDGV();
            }

            //Group
            if (this.tscbGroup.SelectedIndex > 0)
            {
                GroupDGV();
                GridCollapseAndExpand(false);
            }

            this.rDgv.BestFitColumns();
        }
        /// <summary>
        /// 
        /// </summary>
        private void GroupDGV()
        {
            this.rDgv.MasterTemplate.GroupDescriptors.Clear();
            GroupDescriptor gd = new GroupDescriptor();
            if (this.rDgv.Columns.Contains("Eingang"))
            {
                gd = new GroupDescriptor();
                gd.GroupNames.Add("Eingang", System.ComponentModel.ListSortDirection.Ascending);

                this.rDgv.MasterTemplate.GroupDescriptors.Add(gd);
            }
            //if (this.rDgv.Columns.Contains("TransportId"))
            //{
            //    gd = new GroupDescriptor();
            //    gd.GroupNames.Add("TransportId", System.ComponentModel.ListSortDirection.Ascending);

            //    this.rDgv.MasterTemplate.GroupDescriptors.Add(gd);
            //}
        }
        ///<summary>ctrJournal / SortDGV</summary>
        ///<remarks>DGV Sortierung</remarks>
        private void SortDGV()
        {
            this.rDgv.MasterTemplate.SortDescriptors.Clear();
            SortDescriptor Sort = new SortDescriptor();
            if (this.rDgv.Columns.Contains("Dicke"))
            {
                Sort = new SortDescriptor();
                Sort.PropertyName = "Dicke";
                Sort.Direction = System.ComponentModel.ListSortDirection.Ascending;
                this.rDgv.MasterTemplate.SortDescriptors.Add(Sort);
            }
            if (this.rDgv.Columns.Contains("Breite"))
            {
                Sort = new SortDescriptor();
                Sort.PropertyName = "Breite";
                Sort.Direction = System.ComponentModel.ListSortDirection.Ascending;
                this.rDgv.MasterTemplate.SortDescriptors.Add(Sort);
            }
            if (this.rDgv.Columns.Contains("Länge"))
            {
                Sort = new SortDescriptor();
                Sort.PropertyName = "Länge";
                Sort.Direction = System.ComponentModel.ListSortDirection.Ascending;
                this.rDgv.MasterTemplate.SortDescriptors.Add(Sort);
            }
            if (this.rDgv.Columns.Contains("Laenge"))
            {
                Sort = new SortDescriptor();
                Sort.PropertyName = "Laenge";
                Sort.Direction = System.ComponentModel.ListSortDirection.Ascending;
                this.rDgv.MasterTemplate.SortDescriptors.Add(Sort);
            }
        }
        ///<summary>ctrJournal / tsbtnClose_Click</summary>
        ///<remarks>Ctr schliessen</remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            if (this._frmTmp != null)
            {
                this._frmTmp.CloseFrmTmp();
            }
            else
            {
                this._ctrMenu.CloseCtrJournal();
            }
        }
        ///<summary>ctrJournal / tsbtnShowAll_Click</summary>
        ///<remarks></remarks>
        private void tsbtnShowAll_Click(object sender, EventArgs e)
        {
            //Standardwerte setzten
            SetAuswahlJournalDaten();
            //Journaldaten laden
            InitDGV();
        }
        ///<summary>ctrJournal / tsbtnSearch_Click</summary>
        ///<remarks></remarks>
        private void tsbtnSearch_Click(object sender, EventArgs e)
        {
            SelectedValue = cbJournalart.SelectedValue.ToString();
            InitDGV();
        }
        ///<summary>ctrJournal / tsbtnPrint_Click</summary>
        ///<remarks></remarks>
        private void tsbtnPrint_Click(object sender, EventArgs e)
        {
            //eventuell eine switch anweisung
            if (
                (this._ctrMenu._frmMain.system.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_SIL + "_"))
               )
            {
                clsPrint print = new clsPrint(((DataTable)this.rDgv.DataSource).Select(this.rDgv.FilterDescriptors.Expression).CopyToDataTable(), this._ctrMenu._frmMain.GL_System, "Journal", 0); // viewName // CF
                string txt = string.Empty;
                //Journalart
                if (cbJournalart.SelectedIndex > -1)
                {
                    txt = "Journal: " + clsClient.ctrJournal_CustomizeExcelExtportHeaderText(this._ctrMenu._frmMain.system.Client.MatchCode, cbJournalart.Text.ToString(), 1);
                }

                //Zeitraum
                if ((dtpVon.Enabled) && (dtpBis.Enabled))
                {
                    txt = txt + " / Zeitraum: " + dtpVon.Value.ToShortDateString() + " bis " + dtpBis.Value.ToShortDateString();
                }
                if ((dtpVon.Enabled) && (!dtpBis.Enabled))
                {
                    txt = txt + " / Stichtag: " + dtpVon.Value.ToShortDateString();
                }
                print.printTitel = txt;
                print.SelectedView = this.tsbcViews.Text;
                FilterDescriptorCollection dgvFilter = this.rDgv.MasterTemplate.FilterDescriptors;
                SortDescriptorCollection dgvSortCol = this.rDgv.MasterTemplate.SortDescriptors;

                print.Print(true, false, _ctrMenu, dgvFilter);
            }
            else
            {
                if (this.rDgv != null && this.rDgv.RowCount > 0)
                {
                    //clsPrint print = new clsPrint(this.dtJournal, _ctrMenu._frmMain.GL_System, viewName, 0);
                    clsPrint print = new clsPrint(((DataTable)this.rDgv.DataSource).Select(this.rDgv.FilterDescriptors.Expression).CopyToDataTable(), this._ctrMenu._frmMain.GL_System, viewName, 0);

                    //Headertext erstellt
                    string txt = string.Empty;
                    //Journalart
                    if (cbJournalart.SelectedIndex > -1)
                    {
                        txt = "Journal: " + cbJournalart.Text.ToString() + "/ Zeitraum: " + dtpVon.Value.ToShortDateString() + " bis " + dtpBis.Value.ToShortDateString();
                    }
                    //Kunde wenn gewählt
                    if (comboAuftraggeber.SelectedIndex > -1)
                    {
                        if (this.Lager.ADR.ID > 0)
                        {
                            txt = txt + Environment.NewLine +
                                  "Auftraggeber: " + this.Lager.ADR.ViewID + " - " + this.Lager.ADR.Name1;
                        }
                    }
                    print.printTitel = txt;
                    print.SelectedView = tsbcViews.Text;
                    print.SelectedView = this.tsbcViews.Text;

                    FilterDescriptorCollection dgvFilter = this.rDgv.MasterTemplate.FilterDescriptors;
                    SortDescriptorCollection dgvSortCol = this.rDgv.MasterTemplate.SortDescriptors;

                    print.Print(true, true, _ctrMenu, this.rDgv.MasterTemplate.FilterDescriptors);
                }
            }
        }
        ///<summary>ctrJournal / tsbtnExcel_Click</summary>
        ///<remarks>Export zu Excel über SaveFileDialog</remarks>
        private void tsbtnExcel_Click(object sender, EventArgs e)
        {
            strJournalTyp = string.Empty;
            strJournalZeitraum = string.Empty;

            FileDateForMail = DateTime.Now;
            string strFileName = FileDateForMail.ToString("yyyy_MM_dd_HHmmss") + "_Journal_" + cbJournalart.Text + ".xls";
            saveFileDialog.FileName = strFileName;
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName.Equals(String.Empty))
            {
                return;
            }
            strFileName = this.saveFileDialog.FileName;
            bool openExportFile = false;

            Sped4.Classes.TelerikCls.clsExcelML exPort = new clsExcelML();
            exPort.InitClass(ref this._ctrMenu._frmMain, this.GL_User);

            //Headertext erstellt
            string txt = string.Empty;
            //Journalart
            if (cbJournalart.SelectedIndex > -1)
            {
                txt = "Journal: " + clsClient.ctrJournal_CustomizeExcelExtportHeaderText(this._ctrMenu._frmMain.system.Client.MatchCode, cbJournalart.Text.ToString(), 1);
                exPort.ListHeaderText.Add(txt);
                string strTmp = "Eingang/Artikel: ";
                if (cbRLExcl.Checked)
                {
                    strTmp += "excl. Rücklieferungen";
                }
                else
                {
                    strTmp += "incl. Rücklieferungen";
                }
                if (cbSchadenExcl.Checked)
                {
                    strTmp += "| excl. Schäden";
                }
                else
                {
                    strTmp += "| incl. Schäden";
                }
                txt = string.Empty;
                txt = clsClient.ctrJournal_CustomizeExcelExtportHeaderText(this._ctrMenu._frmMain.system.Client.MatchCode, strTmp, 2);
                if (!txt.Equals(string.Empty))
                {
                    exPort.ListHeaderText.Add(txt);
                }
            }
            //Kunde wenn gewählt
            if (comboAuftraggeber.SelectedIndex > -1)
            {
                if (this.Lager.ADR.ID > 0)
                {
                    txt = "Auftraggeber: " + this.Lager.ADR.ViewID + " - " + this.Lager.ADR.Name1;
                    exPort.ListHeaderText.Add(txt);
                }
            }
            //Zeitraum
            txt = "Zeitraum: " + dtpVon.Value.ToShortDateString() + " bis " + dtpBis.Value.ToShortDateString();
            exPort.ListHeaderText.Add(txt);
            strJournalZeitraum = txt;
            strJournalTyp = "Typ: " + cbJournalart.Text.ToString();
            exPort.Telerik_RunExportToExcelML(ref this.rDgv, strFileName, ref openExportFile, true, "Journal");
            //exPort.Telerik_RunExportToExcelML(this.rDgv, strFileName, ref openExportFile, true, "Journal");

            if (openExportFile)
            {
                try
                {
                    System.Diagnostics.Process.Start(strFileName);
                }
                catch (Exception ex)
                {
                    clsError error = new clsError();
                    error._GL_User = this.GL_User;
                    error.Code = clsError.code1_501;
                    error.Aktion = "Journal - Excelexport öffnen";
                    error.exceptText = ex.ToString();
                }
            }
        }
        ///<summary>ctrJournal / tsbtnMail_Click</summary>
        ///<remarks>Journal per MAil an E-Mail adresse oder an Mailverteiler versenden.</remarks>
        private void tsbtnMail_Click(object sender, EventArgs e)
        {
            if (this.rDgv.Rows.Count > 0)
            {

                strJournalTyp = string.Empty;
                strJournalZeitraum = string.Empty;
                FileDateForMail = DateTime.Now;
                CreateAttachmentFilePath();
                if (strAttachmentPath.Equals(String.Empty))
                {
                    return;
                }
                bool openExportFile = false;
                //Functions.Telerik_RunExportToExcelML(ref this._ctrMenu._frmMain, ref this.rDgv, strAttachmentPath, ref openExportFile, this.GL_User, false);

                Sped4.Classes.TelerikCls.clsExcelML exPort = new clsExcelML();
                exPort.InitClass(ref this._ctrMenu._frmMain, this.GL_User);

                //Headertext erstellt
                string txt = string.Empty;
                //Journalart
                if (cbJournalart.SelectedIndex > -1)
                {
                    txt = "Journal: " + cbJournalart.Text.ToString();
                    exPort.ListHeaderText.Add(txt);
                }
                //Kunde wenn gewählt
                if (comboAuftraggeber.SelectedIndex > -1)
                {
                    if (this.Lager.ADR.ID > 0)
                    {
                        txt = "Auftraggeber: " + this.Lager.ADR.ViewID + " - " + this.Lager.ADR.Name1;
                        exPort.ListHeaderText.Add(txt);
                    }
                }
                //Zeitraum
                txt = "Zeitraum: " + dtpVon.Value.ToShortDateString() + " bis " + dtpBis.Value.ToShortDateString();
                exPort.ListHeaderText.Add(txt);
                strJournalZeitraum = txt;
                strJournalTyp = "Typ: " + cbJournalart.Text.ToString();
                exPort.Telerik_RunExportToExcelML(ref this.rDgv, strAttachmentPath, ref openExportFile, true, "Journal");
                //exPort.Telerik_RunExportToExcelML( this.rDgv, strAttachmentPath, ref openExportFile, true, "Journal");
                if (!strAttachmentPath.Equals(string.Empty))
                {
                    this._ctrMenu.OpenCtrMailCockpitInFrm(this);
                }
            }
            else
            {
                string strText = "Das Journal ist aktuell mit keinen Daten gefüllt!";
                clsMessages.Allgemein_ERRORTextShow(strText);
            }
        }
        ///<summary>ctrJournal / rDgv_ToolTipTextNeeded</summary>
        ///<remarks>Zeigt den Cellinhalt als ToolTip an.</remarks>
        private void rDgv_ToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
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
                    if (cell.Value != null)
                    {
                        e.ToolTipText = cell.Value.ToString();
                    }
                }
            }
        }
        ///<summary>ctrJournal / dtpVon_ValueChanged</summary>
        ///<remarks>Setz nach Auswahl des Datum automatisch das Enddatum auf den letzten des Monats.</remarks>
        private void dtpVon_ValueChanged(object sender, EventArgs e)
        {
            DateTime ZeitRaumMonat = ((DateTimePicker)sender).Value.Date;
            dtpVon.Value = ((DateTimePicker)sender).Value;
            dtpBis.Value = Functions.GetLastDayOfMonth(ZeitRaumMonat);
        }
        ///<summary>ctrJournal / cbFilter_CheckedChanged</summary>
        ///<remarks>FIlter wird aktiviert / deaktiviert.</remarks>
        private void cbFilter_CheckedChanged(object sender, EventArgs e)
        {
            gbFilter.Enabled = cbFilter.Checked;
            if (gbFilter.Enabled)
            {
                InitComboAuftraggeber();
            }
            else
            {
                this.comboAuftraggeber.DataSource = null;
                this.comboTarif.DataSource = null;
            }
        }
        ///<summary>ctrJournal / InitFilterGroupBox</summary>
        ///<remarks>Ermittelt alle Auftraggeber für den gewählten Zeitraum </remarks>
        private void InitComboAuftraggeber()
        {
            this.comboAuftraggeber.DataSource = dtCustomWithRate;
            this.comboAuftraggeber.SelectedItem = -1;
            this.comboAuftraggeber.DisplayMember = "Auftraggeber";
            this.comboAuftraggeber.ValueMember = "ID";
            this.comboTarif.SelectedIndex = -1;
        }
        ///<summary>ctrJournal / InitComboTarif</summary>
        ///<remarks></remarks>
        private void InitComboTarif()
        {
            this.comboTarif.DataSource = clsKundenTarife.GetCustomRateForGArtRelation(this.GL_User.User_ID, this.Lager.ADR.ID);
            this.comboTarif.DisplayMember = "Tarifname";
            this.comboTarif.ValueMember = "TarifID";
        }
        ///<summary>ctrJournal / comboAuftraggeber_SelectedIndexChanged</summary>
        ///<remarks>Ermitteln der Tarife des jeweiligen Auftraggebers</remarks>
        private void comboAuftraggeber_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            if (this.comboAuftraggeber.SelectedValue != null)
            {
                Decimal.TryParse(this.comboAuftraggeber.SelectedValue.ToString(), out decTmp);
                if (decTmp > 0)
                {
                    this.Lager.ADR = new clsADR();
                    this.Lager.ADR.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, decTmp, false);
                    InitComboTarif();
                }
            }
        }

        ///<summary>ctrJournal / CreateExportFileName</summary>
        ///<remarks></remarks>
        private void CreateAttachmentFilePath()
        {
            strAttachmentPath = string.Empty;
            strAttachmentPath = Application.StartupPath + "\\TMP\\";
            strFileName = string.Empty;
            strFileName = FileDateForMail.ToString("yyyy_MM_dd_HHmmss") + "_Journal.xls";

            //prüfen, ob der Ordner Tmp existiert
            if (!Directory.Exists(strAttachmentPath))
            {
                Directory.CreateDirectory(strAttachmentPath);
            }
            strAttachmentPath = strAttachmentPath + strFileName;
            ListAttachmentPath = new List<string>();
            ListAttachmentPath.Add(strAttachmentPath);
        }
        ///<summary>ctrJournal / CreateExportFileName</summary>
        ///<remarks></remarks>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            splitPanel1.Collapsed = (!splitPanel1.Collapsed);
        }
        ///<summary>ctrBestand / tsbcViews_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void tsbcViews_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedValue != "")
            {
                if (tsbcViews.SelectedIndex > -1)
                {
                    tscbGroup.SelectedIndex = 0;
                    Functions.setView(ref dtJournal, ref rDgv, "Journal", tsbcViews.SelectedItem.ToString(), this._ctrMenu._frmMain.GL_System, false, dts);
                    this.rDgv.BestFitColumns();
                    this._ctrArtSearchFilter.SetFilterforDGV(ref this.rDgv, false);
                }
            }
        }
        ///<summary>ctrJournal / setViewOrder</summary>
        ///<remarks></remarks>
        private void setView(ref DataTable dt, ref RadGridView dgv, string viewname)
        {
            string MissingOnes = "";
            //Dictionary<string, List<string>> dicViews = _ctrMenu._frmMain.GL_System.DictViews.GetValueOrNull(const_Headline);
            Dictionary<string, List<string>> dicViews;
            _ctrMenu._frmMain.GL_System.DictViews.TryGetValue(const_Headline, out dicViews);
            List<string> tmpList;
            dicViews.TryGetValue(viewname, out tmpList);
            Int32 j = 0;
            for (Int32 i = 0; i < tmpList.Count; i++)
            {
                string temp = tmpList.ElementAt(i);
                try
                {
                    dt.Columns[temp].SetOrdinal(j++);
                }
                catch (Exception ex)
                {
                    if (MissingOnes != "")
                    {
                        MissingOnes += ", ";
                    }
                    MissingOnes += temp;
                    // Spalte des Views in DB Query nicht enthalten ...
                    j--;
                }

            }
            dgv.DataSource = dt;
            if (viewname != "Default")
            {
                for (Int32 i = j; i < dgv.Columns.Count; i++)
                {
                    dgv.Columns[i].IsVisible = false;
                }
            }
            Console.WriteLine(MissingOnes);
        }
        ///<summary>ctrJournal / dtpBis_ValueChanged</summary>
        ///<remarks></remarks>
        private void dtpBis_ValueChanged(object sender, EventArgs e)
        {
            if (dtpBis.Value < dtpVon.Value)
            {
                dtpBis.Value = dtpVon.Value;
            }
        }
        ///<summary>ctrJournal / tstbSearchArtikel_TextChanged</summary>
        ///<remarks></remarks>
        private void tstbSearchArtikel_TextChanged(object sender, EventArgs e)
        {
            tstbSearchArtikel.Text = tstbSearchArtikel.Text.Trim();
            string strFilter = "";
            strFilter = Functions.GetSearchFilterString(ref tscbSearch, tscbSearch.Text, tstbSearchArtikel.Text);
            dtJournal.DefaultView.Sort = tscbSearch.Text.ToString();
            dtJournal.DefaultView.RowFilter = strFilter;
        }
        ///<summary>ctrBestand / cbJournalart_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void cbJournalart_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbJournalart.Text.Contains("alle"))
            {
                cbFilter.Checked = false;
                comboAuftraggeber.SelectedIndex = -1;
                comboTarif.SelectedIndex = -1;
            }
        }
        ///<summary>ctrJournal / rDgv_ContextMenuOpening</summary>
        ///<remarks></remarks>
        private void rDgv_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            RadMenuSeparatorItem separator;
            RadMenuItem customMenuItem;

            if (this.rDgv.SelectedRows.Count > 0)
            {
                decimal decTmp = 0;
                if (rDgv.Columns.Contains("ArtikelID"))
                {
                    Decimal.TryParse(rDgv.SelectedRows[0].Cells["ArtikelID"].Value.ToString(), out decTmp);
                    this.Lager.Artikel.ID = decTmp;
                    this.Lager.Artikel.GetArtikeldatenByTableID();

                    switch (cbJournalart.Text)
                    {
                        default:
                            if (this.Lager.Artikel.LEingangTableID > 0)
                            {
                                separator = new RadMenuSeparatorItem();
                                e.ContextMenu.Items.Add(separator);
                                customMenuItem = new RadMenuItem();
                                customMenuItem.Text = "Gehe zu Eingang";
                                customMenuItem.Click += new EventHandler(this.geheZuEingang);
                                e.ContextMenu.Items.Add(customMenuItem);
                            }
                            if (this.Lager.Artikel.LAusgangTableID > 0)
                            {
                                separator = new RadMenuSeparatorItem();
                                e.ContextMenu.Items.Add(separator);
                                customMenuItem = new RadMenuItem();
                                customMenuItem.Text = "Gehe zu Ausgang";
                                customMenuItem.Click += new EventHandler(this.geheZuAusgang);
                                e.ContextMenu.Items.Add(customMenuItem);
                            }
                            break;
                    }
                }
            }
        }
        ///<summary>ctrJournal / geheZuEingang</summary>
        ///<remarks></remarks>
        private void geheZuEingang(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            if (rDgv.Columns.Contains("ArtikelID"))
            {
                Decimal.TryParse(rDgv.SelectedRows[0].Cells["ArtikelID"].Value.ToString(), out decTmp);
                if (decTmp > 0)
                {
                    object obj = new object();
                    _ctrMenu.CloseCtrEinlagerung();
                    _ctrMenu.OpenCtrEinlagerung(obj, true);
                    _ctrMenu._ctrEinlagerung.SetSearchLEingangskopfdatenToFrm(decTmp);
                    _ctrMenu._ctrEinlagerung.BackToCtr = ctrJournal.const_ControlName;
                    this._ctrMenu.ShowOrHideSpecificCtr(this, false);
                }
            }
        }
        ///<summary>ctrJournal / geheZuAusgang</summary>
        ///<remarks></remarks>
        private void geheZuAusgang(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            if (rDgv.Columns.Contains("ArtikelID"))
            {
                Decimal.TryParse(rDgv.SelectedRows[0].Cells["ArtikelID"].Value.ToString(), out decTmp);
                if (decTmp > 0)
                {
                    object obj = new object();
                    _ctrMenu.CloseCtrEinlagerung();
                    _ctrMenu.OpenCtrAuslagerung(obj, true);
                    _ctrMenu._ctrAuslagerung.SetSearchLAusgangToFrm(decTmp);
                    _ctrMenu._ctrAuslagerung.BackToCtr = ctrJournal.const_ControlName;
                    this._ctrMenu.ShowOrHideSpecificCtr(this, false);
                }
            }
        }
        ///<summary>ctrJournal / tscbSort_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void tscbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rDgv.Rows.Count > 0)
            {
                if (this.tscbSort.SelectedIndex == 0)
                {
                    this.rDgv.MasterTemplate.SortDescriptors.Clear();
                }
                else
                {
                    SortDGV();
                }
            }
        }
        ///<summary>ctrJournal / btnCalDateDiff_Click</summary>
        ///<remarks></remarks>
        private void btnCalDateDiff_Click(object sender, EventArgs e)
        {
            DateTime ZeitraumBis = dtpBis.Value;
            Int32 iDiffDays = 0;
            if (nudDays.Value > 0)
            {
                iDiffDays = -((Int32)nudDays.Value - 1);
            }
            DateTime ZeitraumVon = (dtpBis.Value).AddDays(iDiffDays);
            dtpVon.Value = ZeitraumVon;
            dtpBis.Value = ZeitraumBis;

            nudDays.Value = 1;
        }
        ///<summary>ctrJournal / rDgv_CellFormatting</summary>
        ///<remarks></remarks>
        private void rDgv_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement.ColumnInfo.GetType() == typeof(Telerik.WinControls.UI.GridViewDateTimeColumn))
            {
                DateTime tmpDate = Globals.DefaultDateTimeMinValue;
                string strDate = rDgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if (DateTime.TryParse(strDate, out tmpDate))
                {
                    if (tmpDate <= Globals.DefaultDateTimeMinValue)
                    {
                        rDgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Globals.DefaultDateTimeMinValue;
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbtnGroupExpand_Click(object sender, EventArgs e)
        {
            GridCollapseAndExpand(false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbtnGroupCollapse_Click(object sender, EventArgs e)
        {
            GridCollapseAndExpand(true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myCollapseGrid"></param>
        private void GridCollapseAndExpand(bool myCollapseGrid)
        {
            if (this.rDgv.MasterTemplate.GroupDescriptors.Count > 0)
            {
                if (myCollapseGrid)
                {
                    this.rDgv.MasterTemplate.CollapseAll();
                }
                else
                {
                    this.rDgv.MasterTemplate.ExpandAll();
                }
            }
        }
    }
}
