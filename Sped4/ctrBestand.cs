using Common.Models;
using LVS;
using LVS.ViewData;
using Sped4.Classes;
using Sped4.Classes.TelerikCls;
using Sped4.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Localization;


namespace Sped4
{
    public partial class ctrBestand : UserControl
    {
        public Globals._GL_USER GL_User;
        internal clsADR ADR;
        internal ctrMenu _ctrMenu;
        internal clsReportDocSetting ReportDocSetting;
        internal ctrInventoryAdd _ctrInventoryAdd;
        internal ctrArtSearchFilter _ctrArtSearchFilter;
        internal clsLager Lager = new clsLager();
        internal DataTable dtBestand = new DataTable("Bestand");
        internal DataTable dtMandanten = new DataTable();
        internal Int32 SearchButton = 0;
        internal decimal MandantenID = 0;
        internal bool bFirstGrdLoad = true;
        public DateTime FileDateForMail;
        public string strBestandZeitraum = string.Empty;
        internal List<string> ListAttachmentPath;
        internal string AttachmentPath;
        internal string FileName;
        public string PrintTitel = string.Empty;
        const string const_FileName = "_Bestandsliste";
        const string const_Headline = "Bestandsliste";
        public const string const_ControlName = "Bestandsliste";

        internal BackgroundWorker bw;
        internal delegate void ThreadCtrInvokeEventHandler();

        internal string viewName = "Bestand";
        internal string SelectedBestandsart = "";
        DataColumn[] dts;
        internal frmPrintRepViewer _frmPrintRepViewer;
        public string DokumentArt;
        public decimal sortID { get; private set; }
        public DateTime Stichtag = Globals.DefaultDateTimeMaxValue;

        public List<Int32> lstIDs;

        public string Halle;

        internal DataTable dtSourceCbBestandsart = new DataTable();
        ///<summary>ctrBestand / ctrBestand</summary>
        ///<remarks></remarks>
        public ctrBestand()
        {
            InitializeComponent();
            this.afColorLabel1.myText = const_Headline;
        }
        ///<summary>ctrBestand / cbSchaden_CheckedChanged</summary>
        ///<remarks></remarks>
        private void ctrBestand_Load(object sender, EventArgs e)
        {
            AttachmentPath = this._ctrMenu._frmMain.GL_System.sys_WorkingPathExport;
            MandantenID = this._ctrMenu._frmMain.GL_System.sys_MandantenID;
            ADR = new clsADR();
            ADR._GL_User = this.GL_User;

            this._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
            RadGridLocalizationProvider.CurrentProvider = new clsGermanRadGridLocalizationProvider();
            SetAdrEingabeEnabeld(true);
            SetAuswahlBestandDaten();

            InitFilterSearchCtr();
            Functions.InitComboViews(_ctrMenu._frmMain.GL_System, ref tsbcViews, viewName);
            CustomerSettings();
            SetComTECSettings();
            this.tscbSort.SelectedIndex = 0;
            InitComboGArt();
        }
        ///<summary>ctrFaktLager/ SetComTECSettings</summary>
        ///<remarks></remarks>
        private void SetComTECSettings()
        {
            //Baustelle Änderung
            string strAdmin = "Administrator";
            this.dgv.EnableCustomFiltering = false;
            this.dgv.ShowFilteringRow = Functions.CheckUserForAdminComtec(this.GL_User);
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
        ///<summary>ctrEinlagerung / CustomerSettings</summary>
        ///<remarks>Hier kann den Kundenwünschen entsprechend ein / mehere Elemente 
        ///         ein-/ausgeblendet werden</remarks>
        private void CustomerSettings()
        {
            //Erweiterete Suche 
            this.tsbtnSearchShow.Visible = this._ctrMenu._frmMain.system.Client.Modul.EnableAdvancedSearch;
            //DirectSearch
            this.tscbSearch.Visible = this._ctrMenu._frmMain.system.Client.Modul.EnableDirectSearch;
            this.tstbSearchArtikel.Visible = this._ctrMenu._frmMain.system.Client.Modul.EnableDirectSearch;
            this.tslSearchText.Visible = this._ctrMenu._frmMain.system.Client.Modul.EnableDirectSearch;

            this.tsbtnPrint.Visible = false; // this._ctrMenu._frmMain.system.Client.Modul.Lager_Bestandsliste_PrintButtonReport_Bestand;
            this.tsbtnPrintTelerik.Visible = false; // this._ctrMenu._frmMain.system.Client.Modul.Lager_Bestandsliste_PrintButtonReport_Inventur;

            this.miReportTagesbestand.Enabled = this._ctrMenu._frmMain.system.Client.Modul.Lager_Bestandsliste_PrintButtonReport_Bestand;
            this.miReportTagesbestand.Enabled = this._ctrMenu._frmMain.system.Client.Modul.Lager_Bestandsliste_PrintButtonReport_Inventur;

            this.tsbtnPrintByGrid.Visible = this._ctrMenu._frmMain.system.Client.Modul.Lager_Bestandsliste_PrintButtonGrid;

            //Vorauswahl Bestandsart
            this._ctrMenu._frmMain.system.Client.ctrBestand_CustomizeComboBestandArt(ref this.cbBestandsart);
        }
        ///<summary>ctrBestand / cbSchaden_CheckedChanged</summary>
        ///<remarks></remarks>
        private void tsbtnSearch_Click(object sender, EventArgs e)
        {
            SelectedBestandsart = cbBestandsart.Text;
            dtBestand.Clear();
            InitDGV();
        }
        ///<summary>ctrBestand / InitDGV</summary>
        ///<remarks></remarks>        
        public void InitDGV()
        {
            this.dgv.DataSource = null;
            this._ctrMenu._frmMain.ResetStatusBar();
            this._ctrMenu._frmMain.InitStatusBar(4);
            this._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
            this._ctrMenu._frmMain.StatusBarWork(false, "Daten werden geladen und initialisiert...");

            this.tsComboGroup.SelectedIndex = 0;
            //this.tscbSearch.SelectedIndex = 0;

            Lager = new clsLager();
            Lager.sys = this._ctrMenu._frmMain.system;
            Lager.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system);
            Lager.BestandVon = dtpVon.Value;
            Lager.BestandBis = dtpBis.Value;
            Lager.BestandAdrID = ADR.ID;
            Lager.ADR = new clsADR();
            Lager.ADR.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, Lager.BestandAdrID, false);
            Lager.AbBereichID = this._ctrMenu._frmMain.GL_System.sys_ArbeitsbereichID;
            Lager.MandantenID = MandantenID;
            Lager.bFilterJournal = cbFilter.Checked;
            Lager.Gut.ID = (decimal)comboGArt.SelectedValue;
            Lager.FreieLagertage = 0;

            //Stichtag setzen
            switch (cbBestandsart.Text)
            {
                case clsLager.const_Bestandsart_TagesbestandexSPL:
                case clsLager.const_Bestandsart_Tagesbestand:
                case clsLager.const_Bestandsart_TagesbestandAll:
                case clsLager.const_Bestandsart_TagesbestandAllExclDam:
                case clsLager.const_Bestandsart_TagesbestandAllExclSPL:
                case clsLager.const_Bestandsart_TagesbestandAllExclDamSPL:
                case clsLager.const_Bestandsart_TagesbestandEmp:
                case clsLager.const_Bestandsart_TagesbestandAccrossAllWorkspaces:
                    Lager.Stichtag = dtpVon.Value;
                    break;

                case clsLager.const_Bestandsart_RL:
                case clsLager.const_Bestandsart_DirectDelivery:
                    Lager.Stichtag = dtpBis.Value;
                    break;

                case clsLager.const_Bestandsart_LagergeldTag:
                    Lager.FreieLagertage = (Int32)nudDays.Value;
                    Lager.Stichtag = dtpBis.Value;
                    break;

                case clsLager.const_Bestandsart_SPL:
                case clsLager.const_Bestandsart_ArtikelUnchecked_StoreIN:
                case clsLager.const_Bestandsart_ArtikelUnchecked_StoreOUT:
                case clsLager.const_Bestandsart_Artikel_UncheckedStoreIN:
                case clsLager.const_Bestandsart_Artikel_UncheckedStoreOUT:
                case clsLager.const_Bestandsart_StoreIN_Unchecked:
                case clsLager.const_Bestandsart_StoreOUT_Unchecked:
                    Lager.Stichtag = DateTime.Now;
                    break;

                case clsLager.const_Bestandsart_Inventur:
                    Lager.Stichtag = DateTime.Now;
                    break;

                default:
                    Lager.Stichtag = DateTime.Now;
                    break;
            }

            if (cbFilter.Checked)
            {
                //Tarif
                decimal decTmp = 0;
                Decimal.TryParse(comboTarif.SelectedValue.ToString(), out decTmp);
                Lager.ADR.Kunde.Tarif.ID = decTmp;
                Lager.ADR.Kunde.Tarif.Fill();
            }

            this.dgv.DataSource = null;
            this.dgv.Rows.Clear();
            this._ctrMenu._frmMain.StatusBarWork(false, "Daten werden geladen und initialisiert...");
            dtBestand = Lager.GetBestandsdaten(cbBestandsart.Text, Lager.Gut.ID, this._ctrMenu._frmMain.system.Client.Modul.Lager_USEBKZ);
            //TestSearch
            Functions.InitComboSearch(ref tscbSearch, dtBestand, this._ctrMenu._frmMain.system);
            dtBestand.DefaultView.RowFilter = string.Empty;
            dts = new DataColumn[dtBestand.Columns.Count];
            dtBestand.Columns.CopyTo(dts, 0);

            if (
                   (cbBestandsart.Text != clsLager.const_Bestandsart_StoreIN_Unchecked) &&
                   (cbBestandsart.Text != clsLager.const_Bestandsart_StoreOUT_Unchecked)
               )
            {
                Functions.setView(ref dtBestand, ref dgv, "Bestand", tsbcViews.SelectedItem.ToString(), this._ctrMenu._frmMain.GL_System, false);
            }
            else
            {
                this.dgv.DataSource = dtBestand;
            }

            if (
                    (cbBestandsart.Text == clsLager.const_Bestandsart_ArtikelUnchecked_StoreIN) ||
                    (cbBestandsart.Text == clsLager.const_Bestandsart_ArtikelUnchecked_StoreOUT)
                )
            {
                if (this._ctrMenu._frmMain.system.Client.ctrBestand_CustomizeGroupListCheckedUncheckedStoreINAndOUT())
                {
                    GroupDescriptor gdiscriptor = new GroupDescriptor();
                    gdiscriptor.GroupNames.Add("Auftraggeber", System.ComponentModel.ListSortDirection.Ascending);
                    dgv.GroupDescriptors.Add(gdiscriptor);
                }
            }
            if (bFirstGrdLoad)
            {
                if (this.dgv.Columns.Count > 0)
                {
                    GridViewSummaryItem sumDaten = new GridViewSummaryItem(this.dgv.Columns[0].Name.ToString(), "Datensätze: {0}", GridAggregateFunction.Count);
                    GridViewSummaryItem sumAnzahl = new GridViewSummaryItem("Anzahl", "Gesamt [Stk]: {0}", GridAggregateFunction.Sum);
                    GridViewSummaryItem sumNetto = new GridViewSummaryItem("Netto", "Gesamt [KG]: {0:N2}", GridAggregateFunction.Sum);
                    GridViewSummaryItem sumBrutto = new GridViewSummaryItem("Brutto", "Gesamt [KG]: {0:N2}", GridAggregateFunction.Sum);
                    GridViewSummaryItem sumLagertage = new GridViewSummaryItem("Lagerdauer", "Gesamt [Anzahl]: {0:N0}", GridAggregateFunction.Sum);
                    GridViewSummaryItem sumAbrDauer = new GridViewSummaryItem("AbrDauer", "Gesamt [Anzahl]: {0:N0}", GridAggregateFunction.Sum);

                    GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem(
                        new GridViewSummaryItem[] { sumDaten, sumAnzahl, sumNetto, sumBrutto, sumLagertage, sumAbrDauer });
                    this.dgv.SummaryRowsTop.Add(summaryRowItem);
                    bFirstGrdLoad = false;
                }
            }
            if (this._ctrArtSearchFilter != null)
            {
                this._ctrArtSearchFilter.ClearFilterInput();
                this._ctrArtSearchFilter.SetFilterSearchElementAllEnabled(false);
                this._ctrArtSearchFilter.SetFilterElementEnabledByColumns(ref this.dgv);
            }

            if (
                    (cbBestandsart.Text != clsLager.const_Bestandsart_StoreIN_Unchecked) &&
                    (cbBestandsart.Text != clsLager.const_Bestandsart_StoreOUT_Unchecked)
                )
            {
                CalcSumGridDaten();
                this.dgv.BestFitColumns();

            }
            else
            {
                CalcSumGridDaten(false);
                decimal workingwidth = dgv.Width - 35;
                if (dgv.ColumnCount > 0)
                {
                    decimal colwidth = workingwidth / dgv.ColumnCount;
                    for (int i = 0; i < dgv.ColumnCount; i++)
                    {
                        dgv.Columns[i].Width = (int)colwidth;
                    }
                }
            }
            this._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
            if (this.tscbSort.SelectedIndex > 0)
            {
                SortDGV();
            }
            this.dgv.AutoSizeRows = true;
            for (Int32 i = 0; i <= this.dgv.Columns.Count - 1; i++)
            {
                this.dgv.Columns[i].WrapText = true;
            }
        }
        ///<summary>ctrBestand / CalcSumGridDaten</summary>
        ///<remarks></remarks>
        private void CalcSumGridDaten(bool bCalc = true)
        {
            if (bCalc)
            {
                Int32 iTmp = 0;
                decimal decTmpNetto = 0;
                decimal decTmpBrutto = 0;
                if (dtBestand.Rows.Count > 0)
                {
                    DataTable dtTmp = dtBestand.DefaultView.ToTable(true, "ArtikelID", "Netto", "Brutto");
                    if (dtTmp.Rows.Count > 0)
                    {
                        object objAnzahl = dtTmp.Rows.Count;
                        object objNetto = dtTmp.Compute("SUM(Netto)", "");
                        object objBrutto = dtTmp.Compute("SUM(Brutto)", "");
                        Int32.TryParse(objAnzahl.ToString(), out iTmp);
                        decimal.TryParse(objNetto.ToString(), out decTmpNetto);
                        decimal.TryParse(objBrutto.ToString(), out decTmpBrutto);
                    }
                }
                tbAnzahl.Text = iTmp.ToString();
                tbNetto.Text = Functions.FormatDecimal(decTmpNetto);
                tbBrutto.Text = Functions.FormatDecimal(decTmpBrutto);
            }
            else
            {
                tbAnzahl.Text = string.Empty;
                tbNetto.Text = string.Empty;
                tbBrutto.Text = string.Empty;
            }
        }
        ///<summary>ctrBestand / SetAuswahlJournalDaten</summary>
        ///<remarks>Setzt die Standardwerte</remarks>
        private void SetAuswahlBestandDaten()
        {
            //Datum
            //string strTmp = "01." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Year.ToString();
            //dtpVon.Value = Convert.ToDateTime(strTmp);

            dtpVon.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpBis.Value = DateTime.Now.Date.AddDays(1);
            ClearAdressEingabeFelder();

            //Bestandsarten Combo
            //dtSourceCbBestandsart =ctrBestandSettings.BestandsArt_Customized(ctrBestandSettings.InitTableBestandslistenarten(), this._ctrMenu._frmMain.system.Client);
            dtSourceCbBestandsart = ctrBestandSettings.InitTableBestandslistenarten(this._ctrMenu._frmMain.system.Client);

            cbBestandsart.DataSource = dtSourceCbBestandsart;
            //cbBestandsart.DataSource = ctrBestandSettings.InitTableBestandslistenarten();
            cbBestandsart.DisplayMember = "Bestandsart";
            cbBestandsart.ValueMember = "ID";
        }
        ///<summary>ctrBestand / SetAdrEingabeEnabeld</summary>
        ///<remarks>Bei den entsprechenden Bestandslisten müssten die ADR-Eingabefelder entweder
        ///         aktiviert oder deaktiviert werden.</remarks>
        private void cbBestandsart_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAdressEingabeFelder();
            Int32 myBestArtID = 0;
            Int32.TryParse(cbBestandsart.SelectedValue.ToString(), out myBestArtID);

            DataTable dt = ctrBestandSettings.InitTableBestandslistenarten(this._ctrMenu._frmMain.system.Client);
            dt.DefaultView.RowFilter = "ID=" + myBestArtID;
            DataTable dtTmp = dt.DefaultView.ToTable();
            bool bCheck = (bool)dtTmp.Rows[0]["ADRRequire"];
            SetAdrEingabeEnabeld(bCheck);
            bool bDate = (bool)dtTmp.Rows[0]["DateRequire"];
            SetDateTimePickerEnabeld(bDate);
            InitComboGArt();
            nudDays.Enabled = false;
            tsbtnInventoryCreate.Enabled = false;

            switch (cbBestandsart.Text)
            {
                case clsLager.const_Bestandsart_Tagesbestand:
                    dtpVon.Enabled = true;
                    dtpVon.Value = DateTime.Now.Date;
                    lZeitraumVon.Text = "Stichtag:";
                    //wenn Tagebestand, dann soll der Filter aktiviert werden
                    cbFilter.Enabled = true;
                    comboGArt.Enabled = true;
                    break;

                case clsLager.const_Bestandsart_TagesbestandAll:
                case clsLager.const_Bestandsart_TagesbestandAllExclDam:
                case clsLager.const_Bestandsart_TagesbestandAllExclSPL:
                case clsLager.const_Bestandsart_TagesbestandAllExclDamSPL:
                    dtpVon.Enabled = true;
                    dtpVon.Value = DateTime.Now.Date;
                    lZeitraumVon.Text = "Stichtag:";
                    //wenn Tagebestand, dann soll der Filter aktiviert werden
                    //cbFilter.Enabled = true;
                    break;

                case clsLager.const_Bestandsart_TagesbestandAccrossAllWorkspaces:
                    dtpVon.Enabled = true;
                    dtpVon.Value = DateTime.Now.Date;
                    lZeitraumVon.Text = "Stichtag:";
                    //-- View setzen =  clsClient.cont_ViewName_Bestand_Customized_AllWorkspaces
                    Functions.SetToolStripComboToSelecetedItem(ref tsbcViews, clsClient.cont_ViewName_Bestand_Customized_AllWorkspaces);
                    break;

                case clsLager.const_Bestandsart_TagesbestandexSPL:
                    dtpVon.Enabled = true;
                    dtpVon.Value = DateTime.Now.Date;
                    lZeitraumVon.Text = "Stichtag:";

                    //wenn Tagebestand, dann soll der Filter aktiviert werden
                    cbFilter.Enabled = true;
                    comboGArt.Enabled = true;
                    break;

                case clsLager.const_Bestandsart_TagesbestandEmp:
                    dtpVon.Enabled = true;
                    dtpVon.Value = DateTime.Now.Date;
                    lZeitraumVon.Text = "Stichtag:";
                    //wenn Tagebestand, dann soll der Filter aktiviert werden
                    cbFilter.Enabled = true;
                    comboGArt.Enabled = true;
                    break;

                case clsLager.const_Bestandsart_Inventur:
                    dtpVon.Enabled = true;
                    dtpVon.Value = DateTime.Now.Date;
                    lZeitraumVon.Text = "Stichtag:";

                    //wenn Tagebestand, dann soll der Filter aktiviert werden
                    comboGArt.Enabled = true;
                    tsbtnInventoryCreate.Enabled = true;
                    break;

                case clsLager.const_Bestandsart_SPL:
                    dtpVon.Enabled = false;
                    dtpVon.Value = DateTime.Now.Date;
                    lZeitraumVon.Text = "Stichtag:";
                    break;

                case clsLager.const_Bestandsart_LagergeldTag:
                    dtpVon.Enabled = true;
                    dtpBis.Enabled = true;
                    nudDays.Enabled = true;
                    break;

                default:
                    lZeitraumVon.Text = "Zeitraum von:";
                    cbFilter.Enabled = false;
                    cbFilter.Checked = false;
                    comboTarif.Enabled = false;
                    comboGArt.Enabled = false;
                    break;
            }
        }
        ///<summary>ctrBestand / ClearAdressEingabeFelder</summary>
        ///<remarks></remarks>
        private void ClearAdressEingabeFelder()
        {
            tbSearchA.Text = string.Empty;
            tbAuftraggeber.Text = string.Empty;
            SearchButton = 0;
        }
        ///<summary>ctrBestand / SetAdrEingabeEnabeld</summary>
        ///<remarks></remarks>
        private void SetAdrEingabeEnabeld(bool bEnabled)
        {
            btnSearchA.Enabled = bEnabled;
            tbSearchA.Enabled = bEnabled;
            tbAuftraggeber.Enabled = bEnabled;
        }
        ///<summary>ctrBestand / SetAdrEingabeEnabeld</summary>
        ///<remarks></remarks>
        private void SetDateTimePickerEnabeld(bool bEnabled)
        {
            dtpBis.Enabled = bEnabled;
            dtpVon.Enabled = bEnabled;
        }
        ///<summary>ctrBestand / btnSearchA_Click</summary>
        ///<remarks>Adressdaten werden anhand der ID ermittelt und in die entsprechenden
        ///         Eingabefelder übergeben. Der Matchcode aus den Adressen wird vorerst
        ///         hier mitübernommen wird später noch einmal überschrieben.</remarks>
        public void SetADRByID(decimal ADR_ID)
        {
            switch (SearchButton)
            {
                case 1:
                    ADR.ID = ADR_ID;
                    ADR.Fill();
                    tbSearchA.Text = ADR.ViewID;
                    tbAuftraggeber.Text = ADR.Name1 + " - " + ADR.PLZ + " - " + ADR.Ort;
                    break;
            }
            SearchButton = 0;
        }
        ///<summary>ctrBestand / tbSearchA_TextChanged</summary>
        ///<remarks>ADR-Suche per manueller Eingabe</remarks>
        private void tbSearchA_TextChanged(object sender, EventArgs e)
        {
            DataTable dtTmp = Functions.GetADRTableSearchResultTable(tbSearchA.Text, this.GL_User);
            if (dtTmp.Rows.Count > 0)
            {
                tbAuftraggeber.Text = Functions.GetADRStringFromTable(dtTmp);
                ADR.ID = Functions.GetADR_IDFromTable(dtTmp);
                InitComboGArt();
            }
            else
            {
                tbAuftraggeber.Text = string.Empty;
                ADR.ID = 0;
            }
            ADR.Fill();
        }
        ///<summary>ctrBestand / InitComboGArt</summary>
        ///<remarks></remarks>
        private void InitComboGArt()
        {
            DataTable dtGArtSource = new DataTable();
            //this.comboGArt.Enabled = (ADR.ID > 0);
            //Datasource für combo ermitteln
            clsGueterartADR gADR = new clsGueterartADR();
            gADR.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System);
            dtGArtSource = gADR.GetAssignGArteToADR(this.ADR.ID);

            DataRow row = dtGArtSource.NewRow();
            row["GArtID"] = 0;
            row["Bezeichnung"] = "-- bitte Güterart wählen -- ";
            dtGArtSource.Rows.InsertAt(row, 0);

            comboGArt.DisplayMember = "Bezeichnung";
            comboGArt.ValueMember = "GArtID";
            comboGArt.DataSource = dtGArtSource;
        }
        ///<summary>ctrBestand / tstbSearchArtikel_TextChanged</summary>
        ///<remarks>Direkte Suche im Grid</remarks>
        private void tstbSearchArtikel_TextChanged(object sender, EventArgs e)
        {
            tstbSearchArtikel.Text = tstbSearchArtikel.Text.Trim();
            if (!tstbSearchArtikel.Text.Equals(string.Empty))
            {
                this.dgv.FilterDescriptors.Clear();
                CompositeFilterDescriptor Filter = TelerikFunktions.SetGridViewFilter(tscbSearch.Text, tstbSearchArtikel.Text);
                this.dgv.FilterDescriptors.Add(Filter);
            }
            else
            {
                this.dgv.FilterDescriptors.Clear();
            }
        }
        ///<summary>ctrBestand / tsbtnClose_Click</summary>
        ///<remarks>Ctr schliessen</remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this._ctrMenu.CloseCtrBestand();
        }
        ///<summary>ctrBestand / tsbtnPrint_Click</summary>
        ///<remarks>.</remarks>
        private void tsbtnPrint_Click(object sender, EventArgs e)
        {
            if (this.cbBestandsart.SelectedIndex == 1)
            {
                if (!tbAuftraggeber.Text.Equals(string.Empty))
                {
                    try
                    {
                        this.PrintTitel = "Bestandsart: " + cbBestandsart.Text;
                        this.Stichtag = dtpVon.Value;
                        this.sortID = tscbPrintOrder.SelectedIndex;
                        this._frmPrintRepViewer = new frmPrintRepViewer();
                        this._frmPrintRepViewer._ctrBestand = this;
                        this._frmPrintRepViewer.GL_System = this._ctrMenu._frmMain.GL_System;

                        if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
                        {
                            this._frmPrintRepViewer.iPrintCount = 1;
                            this._frmPrintRepViewer.DokumentenArt = enumIniDocKey.Bestandsliste.ToString();
                        }
                        else
                        {
                            this._ctrMenu._frmMain.system.ReportDocSetting.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, this.Lager.Eingang.Auftraggeber, this._ctrMenu._frmMain.system.AbBereich.ID);
                            this.ReportDocSetting = null; ;
                            this.ReportDocSetting = this._ctrMenu._frmMain.system.ReportDocSetting.ListReportDocListPrint.Find(x => x.DocKey.Equals(enumIniDocKey.Bestandsliste.ToString()));
                            this._frmPrintRepViewer.iPrintCount = this.ReportDocSetting.PrintCount;
                        }
                        this._frmPrintRepViewer.InitFrm();
                        this._frmPrintRepViewer.PrintDirect();
                        this._frmPrintRepViewer.Dispose();
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            else
            {
                clsPrint print = new clsPrint(this.dtBestand, _ctrMenu._frmMain.GL_System, "Kunden", 0); // viewName // CF
                //Headertext erstellt
                string txt = string.Empty;
                txt = "Bestand " + this.cbBestandsart.Text;
                //Kunde wenn gewählt
                if (tbAuftraggeber.Text != string.Empty)
                {
                    txt = txt + " / Auftraggeber: " + ADR.ViewID + " - " + ADR.Name1;
                }
                //Zeitraum
                if ((dtpVon.Enabled) && (dtpBis.Enabled))
                {
                    txt = "Zeitraum: " + dtpVon.Value.ToShortDateString() + " bis " + dtpBis.Value.ToShortDateString();
                }
                if ((dtpVon.Enabled) && (!dtpBis.Enabled))
                {
                    txt = "Stichtag: " + dtpVon.Value.ToShortDateString();
                }
                print.printTitel = txt;
                print.Print(true, this._ctrMenu._frmMain.system.Client.Modul.Lager_Bestandsliste_PrintButtonGrid, _ctrMenu);
            }
        }
        ///<summary>ctrBestand / btnSearchA_Click</summary>
        ///<remarks>Öffnet ADR - Form Suche</remarks>
        private void btnSearchA_Click(object sender, EventArgs e)
        {
            SearchButton = 1;
            this._ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrBestand / tsbtnClear_Click</summary>
        ///<remarks>löscht alle Vorgaben und setzt alle Ctr auf den ursprungszustand zurück</remarks>
        private void tsbtnClear_Click(object sender, EventArgs e)
        {
            SetAuswahlBestandDaten();
            SetAdrEingabeEnabeld(false);
            InitDGV();
            dtBestand.Rows.Clear();
        }
        ///<summary>ctrBestand / tscbSearch_SelectedIndexChanged</summary>
        ///<remarks>Wenn eine neue suchspalte gewählt wird, so soll der suchbegriff geleert werden</remarks>
        private void tscbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tstbSearchArtikel.Text = string.Empty;
        }
        ///<summary>ctrBestand / tsbtnPrintTelerik_Click</summary>
        ///<remarks></remarks>
        private void tsbtnPrintTelerik_Click(object sender, EventArgs e)
        {
            // Inventurliste drucken
            // if (this.dgv != null && this.dgv.RowCount > 0) Druck soll unabhängig des Grids ablaufen
            if (!tbAuftraggeber.Text.Equals(string.Empty))
            {
                if (
                       (this.cbBestandsart.SelectedIndex == 1) ||
                       (this.cbBestandsart.SelectedIndex == 2)
                    )                // Tagesbestand und Inventur
                {
                    try
                    {
                        this._frmPrintRepViewer = new frmPrintRepViewer();
                        this._frmPrintRepViewer._ctrBestand = this;

                        this._frmPrintRepViewer.iPrintCount = 1;
                        this._frmPrintRepViewer.DokumentenArt = "Inventur";
                        this._frmPrintRepViewer.GL_System = this._ctrMenu._frmMain.GL_System;
                        this._frmPrintRepViewer.InitFrm();
                        //this._frmPrintRepViewer.InitReportView();
                        this._frmPrintRepViewer.PrintDirect();
                        this._frmPrintRepViewer.Dispose();
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
        ///<summary>ctrBestand / tsbtnExcel_Click</summary>
        ///<remarks>Wenn eine neue suchspalte gewählt wird, so soll der suchbegriff geleert werden</remarks>      
        private void tsbtnExcel_Click(object sender, EventArgs e)
        {
            strBestandZeitraum = string.Empty;
            bool openExportFile = false;
            FileDateForMail = DateTime.Now;
            if (this._ctrMenu._frmMain.system.Client.Modul.Excel_UseOldExport)
            {
                FileName = FileDateForMail.ToString("yyyy_MM_dd_HHmmss") + const_FileName + ".xls";
                saveFileDialog.InitialDirectory = AttachmentPath;
                saveFileDialog.FileName = AttachmentPath + "\\" + FileName;
                saveFileDialog.ShowDialog();
                FileName = saveFileDialog.FileName;

                if (saveFileDialog.FileName.Equals(String.Empty))
                {
                    return;
                }
                string strFileName = this.saveFileDialog.FileName;

                Sped4.Classes.TelerikCls.clsExcelML exPort = new clsExcelML();
                exPort.InitClass(ref this._ctrMenu._frmMain, this.GL_User);

                //Headertext erstellt
                string txt = string.Empty;

                txt = "Bestand  ";
                exPort.ListHeaderText.Add(txt + " " + this.cbBestandsart.Text + " vom " + Lager.Stichtag.ToShortDateString());
                //Kunde wenn gewählt
                if (tbAuftraggeber.Text != string.Empty)
                {
                    txt = "Auftraggeber: " + ADR.ViewID + " - " + ADR.Name1;
                    exPort.ListHeaderText.Add(txt);
                }
                //Zeitraum
                if ((dtpVon.Enabled) && (dtpBis.Enabled))
                {
                    txt = "Zeitraum: " + dtpVon.Value.ToShortDateString() + " bis " + dtpBis.Value.ToShortDateString();
                }
                if ((dtpVon.Enabled) && (!dtpBis.Enabled))
                {
                    txt = "Stichtag: " + dtpVon.Value.ToShortDateString();
                }
                //exPort.ListHeaderText.Add(txt);
                //exPort.ListHeaderText.Add(this.cbBestandsart.Text);
                strBestandZeitraum = txt;
                exPort.Telerik_RunExportToExcelML(ref this.dgv, strFileName, ref openExportFile, true, "Bestand");
                //exPort.Telerik_RunExportToExcelML(this.dgv, strFileName, ref openExportFile, true, "Bestand");
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
                        error.Aktion = "Bestand - Excelexport öffnen";
                        error.exceptText = ex.ToString();
                    }
                }
            }
            else
            {
                bool bUseBKZ = false;
                string strSql = "Select  " +
                         "LVS_ID as LVS,exMaterialNummer as MATNR,Produktionsnummer as PRODNR,a.Dicke as DICKE,Cast(a.Breite as integer) as BREITE,CAST(a.Laenge as integer) as LAENGE,CAST(a.Brutto as integer)  as BRUTTO, " +
                         "Cast(b.[Date] as Date) as EDATUM, exInfo as BEMERK, " +
                         "Cast(a.Netto as integer) as NETTO, " +
                         //"FreigabeAbruf as Freigabe," +
                         "(Select ViewID from ADR where ID =b.Auftraggeber) as KUNDE ";
                strSql += " From Artikel a " +
                              "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                              "LEFT JOIN Gueterart e ON e.ID=a.GArtID " +
                              "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                              "WHERE " +
                              "( " +
                                    "b.Auftraggeber=" + this.ADR.ID + " ";
                if (bUseBKZ)
                {
                    strSql += " AND a.BKZ=1 AND a.CheckArt=1 AND b.[Check]=1 ";
                }
                else
                {
                    strSql += " AND a.CheckArt=1 AND b.[Check]=1 and (c.Checked is Null or c.Checked=0) ";
                }
                //"AND b.Mandant=" + MandantenID + " " +
                strSql += " AND b.DirectDelivery=0  AND b.AbBereich=" + this._ctrMenu._frmMain.GL_System.sys_ArbeitsbereichID + " " +
                          "AND b.Date <'" + dtpVon.Value.Date.AddDays(1).ToShortDateString() + "' ";
                //"AND b.Date <'" + BestandVon.Date.ToShortDateString() + "' " ;
                strSql = strSql +
                ") " +
                "OR " +
                "(" +
                      "b.Auftraggeber=" + this.ADR.ID + " ";
                if (bUseBKZ)
                {
                    strSql += " AND a.BKZ=1 AND a.CheckArt=1 AND b.[Check]=1 ";
                }
                else
                {
                    strSql += " AND a.CheckArt=1 AND b.[Check]=1";
                }
                //"AND b.Mandant=" + MandantenID + " " +
                strSql += " AND b.DirectDelivery=0 AND b.AbBereich=" + this._ctrMenu._frmMain.GL_System.sys_ArbeitsbereichID + " " +
      //"AND (c.Datum between '" + BestandVon.Date.ToShortDateString() + "' AND '" + DateTime.Now.Date.AddDays(1).ToShortDateString() + "') " +
      //"AND (c.Datum between '" + BestandVon.Date.AddDays(1).ToShortDateString() + "' AND '" + DateTime.Now.Date.AddDays(1).ToShortDateString() + "') " +         29.10.2014
      " AND c.Datum>='" + dtpVon.Value.Date.AddDays(1).ToShortDateString() + "' " +
      //"AND b.Date <'" + BestandVon.Date.ToShortDateString() + "' " ;
      "AND b.Date <'" + dtpVon.Value.Date.AddDays(1).ToShortDateString() + "' ";

                strSql = strSql + ")";
                DataTable dtGewBestand = new DataTable("Bestand");
                dtGewBestand = clsSQLcon.ExecuteSQL_GetDataTable(strSql, this.GL_User.User_ID, "Bestand");
                LVS.clsExcel Excel = new clsExcel();
                string FileName = "BestandKW" + (Functions.GetCalendarWeek(DateTime.Now)) + "_" + ADR.ViewID;
                string FilePath = Excel.ExportDataTableToWorksheet(dtGewBestand, AttachmentPath + "\\" + FileName);
                openExportFile = true;
            }

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
                    error.Aktion = "Bestandsliste - Excelexport öffnen";
                    error.exceptText = ex.ToString();
                    error.WriteError();
                }
            }
        }
        ///<summary>ctrBestand / tsbtnMail_Click</summary>
        ///<remarks>Bestandsliste per Mail versenden</remarks>
        private void tsbtnMail_Click(object sender, EventArgs e)
        {
            if (this.dgv.Rows.Count > 0)
            {
                strBestandZeitraum = string.Empty;
                bool openExportFile = false;
                string strPDFPath = "";
                FileDateForMail = DateTime.Now;
                if (this._ctrMenu._frmMain.system.Client.Modul.Excel_UseOldExport)
                {
                    FileName = FileDateForMail.ToString("yyyy_MM_dd_HHmmss") + const_FileName + ".xls";
                    saveFileDialog.InitialDirectory = AttachmentPath;
                    saveFileDialog.FileName = AttachmentPath + "\\" + FileName;
                    saveFileDialog.ShowDialog();
                    FileName = saveFileDialog.FileName;

                    if (saveFileDialog.FileName.Equals(String.Empty))
                    {
                        return;
                    }
                    //Functions.Telerik_RunExportToExcelML(ref this._ctrMenu._frmMain, ref this.dgv, FileName, ref openExportFile, this.GL_User, true);
                    Sped4.Classes.TelerikCls.clsExcelML exPort = new clsExcelML();
                    exPort.InitClass(ref this._ctrMenu._frmMain, this.GL_User);

                    //Headertext erstellt
                    string txt = string.Empty;

                    txt = "Bestand  ";
                    exPort.ListHeaderText.Add(txt);
                    //Kunde wenn gewählt
                    if (tbAuftraggeber.Text != string.Empty)
                    {
                        txt = "Auftraggeber: " + ADR.ViewID + " - " + ADR.Name1;
                        exPort.ListHeaderText.Add(txt);
                    }
                    //Zeitraum
                    if ((dtpVon.Enabled) && (dtpBis.Enabled))
                    {
                        txt = "Zeitraum: " + dtpVon.Value.ToShortDateString() + " bis " + dtpBis.Value.ToShortDateString();
                    }
                    if ((dtpVon.Enabled) && (!dtpBis.Enabled))
                    {
                        txt = "Stichtag: " + dtpVon.Value.ToShortDateString();
                    }
                    exPort.ListHeaderText.Add(txt);
                    strBestandZeitraum = txt;
                    exPort.Telerik_RunExportToExcelML(ref this.dgv, FileName, ref openExportFile, true, "Bestand");
                    //exPort.Telerik_RunExportToExcelML(this.dgv, FileName, ref openExportFile, true, "Bestand");
                    strPDFPath = FileName;
                }
                else
                {
                    bool bUseBKZ = false;

                    string strSql = "Select  " +
                             "LVS_ID as LVS,exMaterialNummer as MATNR,Produktionsnummer as PRODNR,a.Dicke as DICKE,Cast(a.Breite as integer) as BREITE,CAST(a.Laenge as integer) as LAENGE,CAST(a.Brutto as integer)  as BRUTTO, " +
                             "Cast(b.[Date] as Date) as EDATUM, exInfo as BEMERK, " +
                             "Cast(a.Netto as integer) as NETTO, " +
                             //"FreigabeAbruf as Freigabe," +
                             "(Select ViewID from ADR where ID =b.Auftraggeber) as KUNDE ";
                    strSql += " From Artikel a " +
                                  "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                                  "LEFT JOIN Gueterart e ON e.ID=a.GArtID " +
                                  "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                                  "WHERE " +
                                  "( " +
                                        "b.Auftraggeber=" + this.ADR.ID + " ";
                    if (bUseBKZ)
                    {
                        strSql += " AND a.BKZ=1 AND a.CheckArt=1 AND b.[Check]=1 ";
                    }
                    else
                    {
                        strSql += " AND a.CheckArt=1 AND b.[Check]=1 and (c.Checked is Null or c.Checked=0) ";
                    }
                    //"AND b.Mandant=" + MandantenID + " " +
                    strSql += " AND b.DirectDelivery=0  AND b.AbBereich=" + this._ctrMenu._frmMain.GL_System.sys_ArbeitsbereichID + " " +
                              "AND b.Date <'" + dtpVon.Value.Date.AddDays(1).ToShortDateString() + "' ";
                    //"AND b.Date <'" + BestandVon.Date.ToShortDateString() + "' " ;
                    strSql = strSql +
                    ") " +
                    "OR " +
                    "(" +
                          "b.Auftraggeber=" + this.ADR.ID + " ";
                    if (bUseBKZ)
                    {
                        strSql += " AND a.BKZ=1 AND a.CheckArt=1 AND b.[Check]=1 ";
                    }
                    else
                    {
                        strSql += " AND a.CheckArt=1 AND b.[Check]=1";
                    }
                    //"AND b.Mandant=" + MandantenID + " " +
                    strSql += " AND b.DirectDelivery=0 AND b.AbBereich=" + this._ctrMenu._frmMain.GL_System.sys_ArbeitsbereichID + " " +
          //"AND (c.Datum between '" + BestandVon.Date.ToShortDateString() + "' AND '" + DateTime.Now.Date.AddDays(1).ToShortDateString() + "') " +
          //"AND (c.Datum between '" + BestandVon.Date.AddDays(1).ToShortDateString() + "' AND '" + DateTime.Now.Date.AddDays(1).ToShortDateString() + "') " +         29.10.2014
          " AND c.Datum>='" + dtpVon.Value.Date.AddDays(1).ToShortDateString() + "' " +
          //"AND b.Date <'" + BestandVon.Date.ToShortDateString() + "' " ;
          "AND b.Date <'" + dtpVon.Value.Date.AddDays(1).ToShortDateString() + "' ";

                    strSql = strSql + ")";
                    DataTable dtGewBestand = new DataTable("Bestand");
                    dtGewBestand = clsSQLcon.ExecuteSQL_GetDataTable(strSql, this.GL_User.User_ID, "Bestand");
                    LVS.clsExcel Excel = new clsExcel();
                    string FileName = "BestandKW" + (Functions.GetCalendarWeek(DateTime.Now)) + "_" + ADR.ViewID;
                    strPDFPath = Excel.ExportDataTableToWorksheet(dtGewBestand, AttachmentPath + "\\" + FileName);
                    openExportFile = true;
                }
                // Functions.Telerik_RunExportToExcelML(ref this._ctrMenu._frmMain, ref this.dgv, FileName, ref openExportFile, this.GL_User, false); // CF PDF statt EXCEL
                //if (!FileName.Equals(string.Empty))
                if (!strPDFPath.Equals(string.Empty))
                {
                    System.Threading.Thread.Sleep(500);
                    ListAttachmentPath = new List<string>();
                    ListAttachmentPath.Add(strPDFPath);//FileName);
                    this._ctrMenu.OpenCtrMailCockpitInFrm(this);
                }
            }
            else
            {
                string strText = "Die Bestandsliste ist aktuell nicht gefüllt!";
                clsMessages.Allgemein_ERRORTextShow(strText);
            }
        }
        ///<summary>ctrBestand / dtpVon_ValueChanged</summary>
        ///<remarks></remarks>   
        private void dtpVon_ValueChanged(object sender, EventArgs e)
        {
            DateTime ZeitRaumMonat = ((DateTimePicker)sender).Value.Date;
            dtpVon.Value = ((DateTimePicker)sender).Value;
            dtpBis.Value = Functions.GetLastDayOfMonth(ZeitRaumMonat);
        }
        ///<summary>ctrBestand / dgv_ToolTipTextNeeded</summary>
        ///<remarks>Zeigt den Cellinhalt als ToolTip an.</remarks>
        private void dgv_ToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
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
        ///<summary>ctrBestand / tscbMandanten_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void cbFilter_CheckedChanged(object sender, EventArgs e)
        {
            //Combo aktivieren
            comboTarif.Enabled = cbFilter.Checked;
            //combo füllen
            InitComboTarife();
            //Filter aktiv dann ComboGart aus
            comboGArt.Enabled = (!cbFilter.Checked);
        }
        ///<summary>ctrBestand / InitComboTarife</summary>
        ///<remarks></remarks>
        private void InitComboTarife()
        {
            if (cbFilter.Checked)
            {
                if (this.ADR.ID > 0)
                {
                    this.Lager.ADR = new clsADR();
                    this.Lager.ADR.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, ADR.ID, false);
                    this.comboTarif.DataSource = clsKundenTarife.GetCustomRateForGArtRelation(this.GL_User.User_ID, this.ADR.ID);
                    this.comboTarif.DisplayMember = "Tarifname";
                    this.comboTarif.ValueMember = "TarifID";
                }
                else
                {
                    cbFilter.Checked = false;
                }
            }
            else
            {
                this.comboTarif.DataSource = null;
            }
        }
        ///<summary>ctrBestand / tsbtnSearchShow_Click</summary>
        ///<remarks></remarks>
        private void tsbtnSearchShow_Click(object sender, EventArgs e)
        {
            splitPanel1.Collapsed = (!splitPanel1.Collapsed);
        }
        ///<summary>ctrBestand / tsbcViews_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void tsbcViews_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedBestandsart != "")
            {
                if (tsbcViews.SelectedIndex > -1)
                {
                    //dgv.DataSource = null;
                    //InitDGV();
                    Functions.setView(ref dtBestand, ref dgv, "Bestand", tsbcViews.SelectedItem.ToString(), this._ctrMenu._frmMain.GL_System, false, dts);
                    this.dgv.BestFitColumns();
                    this._ctrArtSearchFilter.SetFilterforDGV(ref this.dgv, false);
                }
            }
        }
        ///<summary>ctrBestand / dgv_ContextMenuOpening</summary>
        ///<remarks></remarks>
        private void dgv_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            RadMenuSeparatorItem separator;
            RadMenuItem customMenuItem;
            if (this.dgv.SelectedRows.Count > 0)
            {
                switch (SelectedBestandsart)
                {
                    case clsLager.const_Bestandsart_Tagesbestand:
                    case clsLager.const_Bestandsart_TagesbestandAll:
                    case clsLager.const_Bestandsart_TagesbestandAllExclDam:
                    case clsLager.const_Bestandsart_TagesbestandAllExclSPL:
                    case clsLager.const_Bestandsart_TagesbestandAllExclDamSPL:
                    case clsLager.const_Bestandsart_Inventur:
                    case clsLager.const_Bestandsart_TagesbestandEmp:
                    case clsLager.const_Bestandsart_DirectDelivery:
                    case clsLager.const_Bestandsart_ArtikelUnchecked_StoreIN:
                    case clsLager.const_Bestandsart_Artikel_UncheckedStoreIN:

                        //case "Tagesbestand":
                        //case "Tagesbestand [Lager komplett]":
                        //case "Inventur":
                        //case "Direktanlieferungen":
                        //case "Ungeprüfte Artikel im Eingang":
                        //case "Artikel in offenen Eingängen":
                        separator = new RadMenuSeparatorItem();
                        e.ContextMenu.Items.Add(separator);
                        customMenuItem = new RadMenuItem();
                        customMenuItem.Text = "Gehe zu Eingang";
                        customMenuItem.Click += new EventHandler(this.geheZuEingang);
                        e.ContextMenu.Items.Add(customMenuItem);
                        break;

                    case clsLager.const_Bestandsart_Artikel_UncheckedStoreOUT:
                    case clsLager.const_Bestandsart_ArtikelUnchecked_StoreOUT:
                        //case "Ungeprüfte Artikel im Ausgang":
                        //case "Artikel in offenen Ausgängen":
                        separator = new RadMenuSeparatorItem();
                        e.ContextMenu.Items.Add(separator);
                        customMenuItem = new RadMenuItem();
                        customMenuItem.Text = "Gehe zu Ausgang";
                        customMenuItem.Click += new EventHandler(this.geheZuAusgang);
                        e.ContextMenu.Items.Add(customMenuItem);
                        break;

                    //case "Nicht abgeschlossene Eingänge":
                    case clsLager.const_Bestandsart_StoreIN_Unchecked:
                        separator = new RadMenuSeparatorItem();
                        e.ContextMenu.Items.Add(separator);
                        customMenuItem = new RadMenuItem();
                        customMenuItem.Text = "Gehe zu Eingang";
                        customMenuItem.Click += new EventHandler(this.geheZuEingangEA);
                        e.ContextMenu.Items.Add(customMenuItem);
                        break;

                    //case "Nicht abgeschlossene Ausgänge":
                    case clsLager.const_Bestandsart_StoreOUT_Unchecked:
                        separator = new RadMenuSeparatorItem();
                        e.ContextMenu.Items.Add(separator);
                        customMenuItem = new RadMenuItem();
                        customMenuItem.Text = "Gehe zu Ausgang";
                        customMenuItem.Click += new EventHandler(this.geheZuAusgangEA);
                        e.ContextMenu.Items.Add(customMenuItem);
                        break;

                    //case "Sperrlager[SPL]":
                    case clsLager.const_Bestandsart_SPL:
                        if (this._ctrMenu._frmMain.system.Client.Modul.Lager_SPL_OutFromEingang)
                        {
                            separator = new RadMenuSeparatorItem();
                            e.ContextMenu.Items.Add(separator);
                            customMenuItem = new RadMenuItem();
                            customMenuItem.Text = "Artikel aus dem Sperrlager entfernen";
                            customMenuItem.Click += new EventHandler(this.SPL_Unlock);
                            e.ContextMenu.Items.Add(customMenuItem);
                        }
                        break;
                    case clsLager.const_Bestandsart_TagesbestandAccrossAllWorkspaces:
                        //kein Menü
                        break;

                    default:
                        //auch wenn nichts ausgewählt wurde und eine Ergebnisliste angezeigt wird, 
                        //soll der Menüpunkt erstellt werden
                        if (this.dgv.Rows.Count > 0)
                        {
                            separator = new RadMenuSeparatorItem();
                            e.ContextMenu.Items.Add(separator);
                            customMenuItem = new RadMenuItem();
                            customMenuItem.Text = "Gehe zu Eingang";
                            customMenuItem.Click += new EventHandler(this.geheZuEingang);
                            e.ContextMenu.Items.Add(customMenuItem);
                        }
                        break;
                }
            }
        }
        ///<summary>ctrBestand / SPL_Unlock</summary>
        ///<remarks></remarks>
        private void SPL_Unlock(object sender, EventArgs e)
        {
            bool bCanBookingOutSpl = false;
            decimal decTmp = 0;

            string strMes = "";
            Decimal.TryParse(dgv.SelectedRows[0].Cells["ArtikelID"].Value.ToString(), out decTmp);
            if (decTmp > 0)
            {
                //--Custom Processes
                CustomProcessesViewData cpVD = new CustomProcessesViewData((int)decTmp, this.GL_User);
                if (cpVD.ExistCustomProcess)
                {
                    Articles art = new Articles();
                    art.Id = (int)decTmp;
                    SperrlagerViewData splVD = new SperrlagerViewData(art, (int)GL_User.User_ID);
                    if (splVD.Spl.IsCustomCertificateMissing)
                    {
                        strMes = "Die Buchung kann nicht vorgenommen werden, da der Artikel einem kundenspezifischen Prozess unterliegt!";
                        clsMessages.Allgemein_InfoTextShow(strMes);
                    }
                    else
                    {
                        bCanBookingOutSpl = true;
                    }
                }
                else
                {
                    bCanBookingOutSpl = true;
                }
            }

            if (bCanBookingOutSpl)
            {
                if (clsMessages.Lager_SPL_Out())
                {
                    bool IsBookedOut = SperrlagerViewData.BookingArticleOutSpl((int)decTmp, (int)this.GL_User.User_ID);
                    if (IsBookedOut)
                    {
                        this.InitDGV();
                    }
                }
            }
            //clsSPL spl = new clsSPL();
            //spl.ArtikelID = decTmp;
            //spl.FillLastINByArtikelID();
            //if (spl.CheckArtikelInSPL())
            //{
            //    spl.lstCheckOut = new List<Int32>();
            //    spl.lstCheckOut.Add((Int32)spl.ArtikelID);
            //    if (clsMessages.Lager_SPL_Out() && spl.DoSPLCheckOutInOldEingang())
            //    {
            //        this.InitDGV();
            //    }
            //}
        }
        ///<summary>ctrBestand / geheZuEingang</summary>
        ///<remarks></remarks>
        private void geheZuEingang(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            Decimal.TryParse(dgv.SelectedRows[0].Cells["ArtikelID"].Value.ToString(), out decTmp);
            if (decTmp > 0)
            {
                object obj = new object();
                _ctrMenu.CloseCtrEinlagerung();
                _ctrMenu.OpenCtrEinlagerung(obj, true);
                _ctrMenu._ctrEinlagerung.SetSearchLEingangskopfdatenToFrm(decTmp);
                _ctrMenu._ctrEinlagerung.BackToCtr = ctrBestand.const_ControlName;
                this._ctrMenu.ShowOrHideSpecificCtr(this, false);
            }
        }
        ///<summary>ctrBestand / geheZuEingangEA</summary>
        ///<remarks></remarks>
        private void geheZuEingangEA(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            //Decimal.TryParse(dgv.SelectedRows[0].Cells["Eingang"].Value.ToString(), out decTmp);
            if (dgv.SelectedRows[0].Cells["LEingangTableID"] != null)
            {
                Decimal.TryParse(dgv.SelectedRows[0].Cells["LEingangTableID"].Value.ToString(), out decTmp);
                if (decTmp > 0)
                {
                    //decimal decLEingangTableID = clsLEingang.GetLEingangTableIDByLEingangID(this._ctrMenu._frmMain.system.BenutzerID, decTmp, this._ctrMenu._frmMain.system);

                    object obj = new object();
                    _ctrMenu.CloseCtrEinlagerung();
                    _ctrMenu.OpenCtrEinlagerung(obj, true);
                    _ctrMenu._ctrEinlagerung.SetSearchLEingangskopfdatenToFrmEA(decTmp);
                    _ctrMenu._ctrEinlagerung.BackToCtr = ctrBestand.const_ControlName;
                    this._ctrMenu.ShowOrHideSpecificCtr(this, false);
                }
            }
        }
        ///<summary>ctrBestand / geheZuAusgang</summary>
        ///<remarks></remarks>
        private void geheZuAusgang(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            Decimal.TryParse(dgv.SelectedRows[0].Cells["ArtikelID"].Value.ToString(), out decTmp);
            if (decTmp > 0)
            {
                object obj = new object();
                _ctrMenu.CloseCtrEinlagerung();
                _ctrMenu.OpenCtrAuslagerung(obj, true);
                _ctrMenu._ctrAuslagerung.SetSearchLAusgangToFrm(decTmp);
                _ctrMenu._ctrAuslagerung.BackToCtr = ctrBestand.const_ControlName;
                this._ctrMenu.ShowOrHideSpecificCtr(this, false);
            }
        }
        ///<summary>ctrBestand / geheZuAusgangEA</summary>
        ///<remarks></remarks>
        private void geheZuAusgangEA(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            Decimal.TryParse(dgv.SelectedRows[0].Cells["Ausgang"].Value.ToString(), out decTmp);
            if (decTmp > 0)
            {
                object obj = new object();
                _ctrMenu.CloseCtrEinlagerung();
                _ctrMenu.OpenCtrAuslagerung(obj, true);
                _ctrMenu._ctrAuslagerung.SetSearchLAusgangToFrmEA(decTmp);
                _ctrMenu._ctrAuslagerung.BackToCtr = ctrBestand.const_ControlName;
                this._ctrMenu.ShowOrHideSpecificCtr(this, false);
            }
        }
        ///<summary>ctrBestand / tsbtnStartSearch_Click</summary>
        ///<remarks></remarks>
        private void tsbtnStartSearch_Click(object sender, EventArgs e)
        {
            if (tscbSearch.SelectedIndex > -1)
            {
                tstbSearchArtikel.Text = tstbSearchArtikel.Text.Trim();
                string strFilter = "";
                strFilter = Functions.GetSearchFilterString(ref tscbSearch, tscbSearch.Text, tstbSearchArtikel.Text);
                dtBestand.DefaultView.Sort = tscbSearch.Text.ToString();
                dtBestand.DefaultView.RowFilter = strFilter;
            }
        }
        ///<summary>ctrBestand / toolStripButton1_Click</summary>
        ///<remarks></remarks>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            PrintGridView();
        }
        ///<summary>ctrBestand / PrintGridView</summary>
        ///<remarks></remarks>
        private void PrintGridView()
        {
            if (
                    (this.dgv.DataSource != null) &&
                    (this.dgv.Rows.Count > 0)
               )

            {
                ////clsPrint print = new clsPrint(((DataTable)this.dgv.DataSource).Select(this.dgv.FilterDescriptors.Expression).CopyToDataTable(), _ctrMenu._frmMain.GL_System, "Bestand", 0); // viewName // CF

                //clsPrint print = new clsPrint(this.dgv, this._ctrMenu, "Bestand", 0);

                ////print._GL_User = this.GL_User;
                //string txt = string.Empty;
                //txt = "Bestandsart: " + cbBestandsart.Text;
                //if (tbAuftraggeber.Text != string.Empty)
                //{
                //    txt += " / Auftraggeber: " + ADR.ViewID + " - " + ADR.Name1;

                //}
                ////Zeitraum / Stichtag
                //switch (lZeitraumVon.Text)
                //{
                //    case "Stichtag:":
                //        txt += Environment.NewLine + "Stichtag: " + this.Lager.Stichtag.ToShortDateString();
                //        break;

                //    case "Zeitraum von:":
                //        txt += Environment.NewLine + "Zeitraum von - bis : " + this.Lager.BestandVon.ToShortDateString()+ " - "  + this.Lager.BestandBis.ToShortDateString();
                //        //txt += Environment.NewLine + "Zeitraum bis: " + this.Lager.BestandBis.ToShortDateString();
                //        break;
                //}
                //print.printTitel = txt;
                //print.Stichtag = this.Lager.Stichtag;
                //print.SelectedView = this.tsbcViews.Text;
                //FilterDescriptorCollection dgvFilter = this.dgv.MasterTemplate.FilterDescriptors;
                ////print.Print(true, this._ctrMenu._frmMain.system.Client.Modul.Lager_Bestandsliste_PrintButtonGrid, _ctrMenu, dgvFilter);
                ////print.Print(true, this._ctrMenu._frmMain.system.Client.Modul.Print_GridPrint_ViewByGridPrint_Bestandsliste, _ctrMenu, dgvFilter);

                //print.PrintGrid(true, this._ctrMenu._frmMain.system.Client.Modul.Print_GridPrint_ViewByGridPrint_Bestandsliste);

                string txt = string.Empty;
                txt = "Bestandsart: " + cbBestandsart.Text;
                if (tbAuftraggeber.Text != string.Empty)
                {
                    txt += " / Auftraggeber: " + ADR.ViewID + " - " + ADR.Name1;

                }
                //Zeitraum / Stichtag
                switch (lZeitraumVon.Text)
                {
                    case "Stichtag:":
                        txt += Environment.NewLine + "Stichtag: " + this.Lager.Stichtag.ToShortDateString();
                        break;

                    case "Zeitraum von:":
                        txt += Environment.NewLine + "Zeitraum von - bis : " + this.Lager.BestandVon.ToShortDateString() + " - " + this.Lager.BestandBis.ToShortDateString();
                        //txt += Environment.NewLine + "Zeitraum bis: " + this.Lager.BestandBis.ToShortDateString();
                        break;
                }

                clsPrint print;
                switch (this._ctrMenu._frmMain.system.Client.MatchCode)
                {
                    case clsClient.const_ClientMatchcode_Althaus + "_":
                        print = new clsPrint(this.dgv, this._ctrMenu, "Bestand", 0);
                        print.printTitel = txt;
                        print.Stichtag = this.Lager.Stichtag;
                        print.SelectedView = this.tsbcViews.Text;
                        print.PrintGrid(true, this._ctrMenu._frmMain.system.Client.Modul.Print_GridPrint_ViewByGridPrint_Bestandsliste);
                        break;

                    default:
                        //print = new clsPrint(((DataTable)this.dgv.DataSource).Select(this.dgv.FilterDescriptors.Expression).CopyToDataTable(), _ctrMenu._frmMain.GL_System, "Bestand", 0);
                        //print._GL_User = this.GL_User;
                        print = new clsPrint(this.dgv, this._ctrMenu, "Bestand", 0);
                        print.printTitel = txt;
                        print.Stichtag = this.Lager.Stichtag;
                        print.SelectedView = this.tsbcViews.Text;
                        FilterDescriptorCollection dgvFilter = this.dgv.MasterTemplate.FilterDescriptors;
                        print.Print(true, this._ctrMenu._frmMain.system.Client.Modul.Print_GridPrint_ViewByGridPrint_Bestandsliste, _ctrMenu, dgvFilter);

                        // print.PrintGrid(true, this._ctrMenu._frmMain.system.Client.Modul.Print_GridPrint_ViewByGridPrint_Bestandsliste);
                        break;
                }
            }
        }
        ///<summary>ctrBestand / tsbExcel2_Click</summary>
        ///<remarks></remarks>
        private void tsbExcel2_Click(object sender, EventArgs e)
        {
            bool bUseBKZ = false;
            string strSql = "Select  " +
                     "LVS_ID as LVS,exMaterialNummer as MATNR,Produktionsnummer as PRODNR,a.Dicke as DICKE,Cast(a.Breite as integer) as BREITE,CAST(a.Laenge as integer) as LAENGE,CAST(a.Brutto as integer)  as BRUTTO, " +
                     "Cast(b.[Date] as Date) as EDATUM, exInfo as BEMERK, " +
                     "Cast(a.Netto as integer) as NETTO " +
                     //",FreigabeAbruf as Freigabe," +
                     "(Select ViewID from ADR where ID =b.Auftraggeber) as KUNDE ";
            strSql += " From Artikel a " +
                          "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                          "LEFT JOIN Gueterart e ON e.ID=a.GArtID " +
                          "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                          "WHERE " +
                          "( " +
                                "b.Auftraggeber=" + this.ADR.ID + " ";
            if (bUseBKZ)
            {
                strSql += " AND a.BKZ=1 AND a.CheckArt=1 AND b.[Check]=1 ";
            }
            else
            {
                strSql += " AND a.CheckArt=1 AND b.[Check]=1 and (c.Checked is Null or c.Checked=0) ";
            }
            //"AND b.Mandant=" + MandantenID + " " +
            strSql += " AND b.DirectDelivery=0  AND b.AbBereich=" + this._ctrMenu._frmMain.GL_System.sys_ArbeitsbereichID + " " +
                      "AND b.Date <'" + dtpVon.Value.Date.AddDays(1).ToShortDateString() + "' ";
            //"AND b.Date <'" + BestandVon.Date.ToShortDateString() + "' " ;
            strSql = strSql +
            ") " +
            "OR " +
            "(" +
                  "b.Auftraggeber=" + this.ADR.ID + " ";
            if (bUseBKZ)
            {
                strSql += " AND a.BKZ=1 AND a.CheckArt=1 AND b.[Check]=1 ";
            }
            else
            {
                strSql += " AND a.CheckArt=1 AND b.[Check]=1";
            }
            //"AND b.Mandant=" + MandantenID + " " +
            strSql += " AND b.DirectDelivery=0 AND b.AbBereich=" + this._ctrMenu._frmMain.GL_System.sys_ArbeitsbereichID + " " +
   //"AND (c.Datum between '" + BestandVon.Date.ToShortDateString() + "' AND '" + DateTime.Now.Date.AddDays(1).ToShortDateString() + "') " +
   //"AND (c.Datum between '" + BestandVon.Date.AddDays(1).ToShortDateString() + "' AND '" + DateTime.Now.Date.AddDays(1).ToShortDateString() + "') " +         29.10.2014
   " AND c.Datum>='" + dtpVon.Value.Date.AddDays(1).ToShortDateString() + "' " +
   //"AND b.Date <'" + BestandVon.Date.ToShortDateString() + "' " ;
   "AND b.Date <'" + dtpVon.Value.Date.AddDays(1).ToShortDateString() + "' ";

            strSql = strSql + ")";
            DataTable dtGewBestand = new DataTable("Bestand");
            dtGewBestand = clsSQLcon.ExecuteSQL_GetDataTable(strSql, this.GL_User.User_ID, "Bestand");
            LVS.clsExcel Excel = new clsExcel();
            string FileName = "BestandKW" + (Functions.GetCalendarWeek(DateTime.Now)) + "_" + ADR.ViewID;
            string FilePath = Excel.ExportDataTableToWorksheet(dtGewBestand, AttachmentPath + "\\" + FileName);
            //string FilePath = AttachmentPath + "\\" + FileName;
            List<string> listAttach = new List<string>();
            listAttach.Add(FilePath);
        }
        ///<summary>ctrBestand / dgv_RowFormatting</summary>
        ///<remarks></remarks>
        private void dgv_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            Int32 iTmp = 0;
            if (e.RowElement.RowInfo.Cells["ArtikelID"] != null)
            {
                if (!cbBestandsart.Text.Equals("Sperrlager[SPL]"))
                {
                    if (Int32.TryParse(e.RowElement.RowInfo.Cells["ArtikelID"].Value.ToString(), out iTmp))
                    {
                        clsSPL spl = new clsSPL();
                        spl.Fill();
                        spl.ArtikelID = iTmp;
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
                }
            }
        }
        ///<summary>ctrBestand / SortDGV</summary>
        ///<remarks>DGV Sortierung</remarks>
        private void SortDGV()
        {
            this.dgv.MasterTemplate.SortDescriptors.Clear();
            SortDescriptor Sort = new SortDescriptor();
            if (this.dgv.Columns.Contains("Dicke"))
            {
                Sort = new SortDescriptor();
                Sort.PropertyName = "Dicke";
                Sort.Direction = System.ComponentModel.ListSortDirection.Ascending;
                this.dgv.MasterTemplate.SortDescriptors.Add(Sort);
            }
            if (this.dgv.Columns.Contains("Breite"))
            {
                Sort = new SortDescriptor();
                Sort.PropertyName = "Breite";
                Sort.Direction = System.ComponentModel.ListSortDirection.Ascending;
                this.dgv.MasterTemplate.SortDescriptors.Add(Sort);
            }
            if (this.dgv.Columns.Contains("Länge"))
            {
                Sort = new SortDescriptor();
                Sort.PropertyName = "Länge";
                Sort.Direction = System.ComponentModel.ListSortDirection.Ascending;
                this.dgv.MasterTemplate.SortDescriptors.Add(Sort);
            }
            if (this.dgv.Columns.Contains("Laenge"))
            {
                Sort = new SortDescriptor();
                Sort.PropertyName = "Laenge";
                Sort.Direction = System.ComponentModel.ListSortDirection.Ascending;
                this.dgv.MasterTemplate.SortDescriptors.Add(Sort);
            }
        }
        ///<summary>ctrBestand / tscbSort_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void tscbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.dgv.Rows.Count > 0)
            {
                if (this.tscbSort.SelectedIndex == 0)
                {
                    this.dgv.MasterTemplate.SortDescriptors.Clear();
                }
                else
                {
                    SortDGV();
                }
            }
        }
        ///<summary>ctrBestand / reportTagesbestandToolStripMenuItem_Click</summary>
        ///<remarks></remarks>
        private void reportTagesbestandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strError = string.Empty;
            //CHeck auf Error
            if (this.cbBestandsart.SelectedIndex != 1)
            {
                strError = strError + "- Es wurde die falsche Bestandsart: " + cbBestandsart.Text + " ausgewählt." + Environment.NewLine;
            }
            if (tbAuftraggeber.Text.Equals(string.Empty))
            {
                strError = strError + "- Es wurde kein Lieferant / Auftraggeber ausgewählt." + Environment.NewLine;
            }
            if (!this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
            {
                this._ctrMenu._frmMain.system.ReportDocSetting.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, this.ADR.ID, this._ctrMenu._frmMain.system.AbBereich.ID);
                this.ReportDocSetting = null; ;
                this.ReportDocSetting = this._ctrMenu._frmMain.system.ReportDocSetting.ListReportDocListPrint.Find(x => x.DocKey.Equals(enumIniDocKey.Bestandsliste.ToString()));
                if (this.ReportDocSetting == null)
                {
                    strError = strError + "- Es konnte keine Report für die Betandsliste gefunden werden " + Environment.NewLine;
                }
            }
            //Print wenn kein Error vorliegt
            if (strError.Equals(string.Empty))
            {
                DirectPrintReport(enumIniDocKey.Bestandsliste);
            }
            else
            {
                strError = "Folgende Fehler sind aufgetreten: " + Environment.NewLine +
                           strError +
                           "Der Druckvorgang konnte nicht durchgeführt werden!";
                clsMessages.Allgemein_ERRORTextShow(strError);
            }
        }
        ///<summary>ctrBestand / reportInventurToolStripMenuItem_Click</summary>
        ///<remarks></remarks>
        private void reportInventurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strError = string.Empty;
            //CHeck auf Error
            if (
                    (this.cbBestandsart.SelectedIndex < 0) ||
                    (this.cbBestandsart.SelectedIndex < 2)
               )
            {
                strError = strError + "- Es wurde die falsche Bestandsart: " + cbBestandsart.Text + " ausgewählt." + Environment.NewLine;
            }
            if (tbAuftraggeber.Text.Equals(string.Empty))
            {
                strError = strError + "- Es wurde kein Lieferant / Auftraggeber ausgewählt." + Environment.NewLine;
            }
            if (!this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
            {
                this._ctrMenu._frmMain.system.ReportDocSetting.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, this.ADR.ID, this._ctrMenu._frmMain.system.AbBereich.ID);
                this.ReportDocSetting = null; ;
                this.ReportDocSetting = this._ctrMenu._frmMain.system.ReportDocSetting.ListReportDocListPrint.Find(x => x.DocKey.Equals(enumIniDocKey.Inventur.ToString()));
                if (this.ReportDocSetting == null)
                {
                    strError = strError + "- Es konnte keine Report für die Betandsliste gefunden werden " + Environment.NewLine;
                }
            }
            //Print wenn kein Error vorliegt
            if (strError.Equals(string.Empty))
            {
                DirectPrintReport(enumIniDocKey.Inventur);
            }
            else
            {
                strError = "Folgende Fehler sind aufgetreten: " + Environment.NewLine +
                           strError +
                           "Der Druckvorgang konnte nicht durchgeführt werden!";
                clsMessages.Allgemein_ERRORTextShow(strError);
            }
        }
        ///<summary>ctrBestand / reportInventurToolStripMenuItem_Click</summary>
        ///<remarks>Bestandsliste und Inventur</remarks>
        private void DirectPrintReport(enumIniDocKey myDocKey)
        {
            try
            {
                this.PrintTitel = "Bestandsart: " + cbBestandsart.Text;
                this.Stichtag = dtpVon.Value;
                this.sortID = tscbPrintOrder.SelectedIndex;
                this._frmPrintRepViewer = new frmPrintRepViewer();
                this._frmPrintRepViewer._ctrBestand = this;
                this._frmPrintRepViewer.GL_System = this._ctrMenu._frmMain.GL_System;

                if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
                {
                    this._frmPrintRepViewer.iPrintCount = 1;
                    this._frmPrintRepViewer.DokumentenArt = myDocKey.ToString();
                }
                else
                {
                    //this._ctrMenu._frmMain.system.ReportDocSetting.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this.Lager.Eingang.Auftraggeber, this._ctrMenu._frmMain.system.AbBereich.ID);
                    //this.ReportDocSetting = null; ;
                    //this.ReportDocSetting = this._ctrMenu._frmMain.system.ReportDocSetting.ListReportDocListPrint.Find(x => x.DocKey.Equals(myDocKey.ToString()));
                    this._frmPrintRepViewer.iPrintCount = this.ReportDocSetting.PrintCount;
                }
                this._frmPrintRepViewer.InitFrm();
                this._frmPrintRepViewer.PrintDirect();
                this._frmPrintRepViewer.Dispose();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        ///<summary>ctrBestand / tsComboGroup_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void tsComboGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.dgv.Rows.Count > 0)
            {
                this.dgv.MasterTemplate.GroupDescriptors.Clear();
                if (this.tsComboGroup.SelectedIndex > 0)
                {
                    GroupDGV();
                }
            }
        }
        ///<summary>ctrBestand / GroupDGV</summary>
        ///<remarks></remarks>
        private void GroupDGV()
        {
            this.dgv.MasterTemplate.GroupDescriptors.Clear();
            GroupDescriptor desc = new GroupDescriptor();
            //desc.Expression = tsComboGroup.Text;
            if (this.dgv.Columns.Contains("Dicke"))
            {
                desc.GroupNames.Add("Dicke", ListSortDirection.Ascending);
            }
            if (this.dgv.Columns.Contains("Breite"))
            {
                desc.GroupNames.Add("Breite", ListSortDirection.Ascending);
            }
            this.dgv.MasterTemplate.GroupDescriptors.Add(desc);
            //this.dgv.MasterTemplate.BestFitColumns();
            for (Int32 i = 0; i <= this.dgv.MasterTemplate.Columns.Count - 1; i++)
            {
                this.dgv.MasterTemplate.Columns[i].AutoSizeMode = BestFitColumnMode.AllCells;
            }
        }
        ///<summary>ctrBestand / tsbtnGroupExpand_Click</summary>
        ///<remarks></remarks>
        private void tsbtnGroupExpand_Click(object sender, EventArgs e)
        {
            if (this.dgv.MasterTemplate.GroupDescriptors.Count > 0)
            {
                this.dgv.MasterTemplate.ExpandAll();
                //for (Int32 i = 0; i <= this.dgv.MasterTemplate.Groups.Count - 1; i++)
                //{                    
                //    this.dgv.MasterTemplate.Groups[i].Expand();
                //}
            }
        }
        ///<summary>ctrBestand / tsbtnGroupCollapse_Click</summary>
        ///<remarks></remarks>
        private void tsbtnGroupCollapse_Click(object sender, EventArgs e)
        {
            if (this.dgv.MasterTemplate.GroupDescriptors.Count > 0)
            {
                this.dgv.MasterTemplate.CollapseAll();
                //for (Int32 i = 0; i <= this.dgv.MasterTemplate.Groups.Count - 1; i++)
                //{
                //    this.dgv.MasterTemplate.Groups[i].Collapse();
                //}
            }
        }

        private void tsmSelection_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tsbtnInventoryCreate_Click(object sender, EventArgs e)
        {
            if (this.dtBestand.Rows.Count > 0)
            {
                _ctrInventoryAdd = new ctrInventoryAdd();
                _ctrInventoryAdd._ctrMenu = this._ctrMenu;
                _ctrInventoryAdd.dtSource = this.dtBestand;
                //_ctrInventoryAdd.ListArtikelId = Classes.TelerikCls.TelerikFunktions.GetColumnArtikelIdFrom(ref this.dgv);            
                this._ctrMenu.OpenFrmTMP(this._ctrInventoryAdd);
            }
            else
            {
                string strText = "Keine Artikel vorhanden!";
                clsMessages.Allgemein_ERRORTextShow(strText);
            }
        }
    }
}
