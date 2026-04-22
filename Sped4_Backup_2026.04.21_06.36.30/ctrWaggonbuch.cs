using LVS;
using Sped4.Classes;
using Sped4.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Localization;


namespace Sped4
{
    public partial class ctrWaggonbuch : UserControl
    {
        public Globals._GL_USER GL_User;
        internal clsADR ADR;
        internal ctrMenu _ctrMenu;
        internal ctrArtSearchFilter _ctrArtSearchFilter;
        internal clsLager Lager = new clsLager();
        internal DataTable dtBestand = new DataTable("Bestand");
        internal DataTable dtMandanten = new DataTable();
        internal Int32 SearchButton = 0;
        internal decimal MandantenID = 0;
        internal bool bFirstGrdLoad = true;

        internal List<string> ListAttachmentPath;
        internal string AttachmentPath;
        internal string FileName;
        const string const_FileName = "_Bestandsliste";
        const string const_Headline = "Waggonbuch";

        internal BackgroundWorker bw;
        internal delegate void ThreadCtrInvokeEventHandler();

        internal string viewName = "Bestand";
        internal string SelectedBestandsart = "";
        DataColumn[] dts;
        internal frmPrintRepViewer _frmPrintRepViewer;
        public string DokumentArt;
        public decimal sortID { get; private set; }

        public string Halle;
        ///<summary>ctrBestand / ctrBestand</summary>
        ///<remarks></remarks>
        public ctrWaggonbuch()
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
            SetAuswahlWaggonDaten();

            InitFilterSearchCtr();
            //Functions.InitComboViews(_ctrMenu._frmMain.GL_System, ref tsbcViews, viewName);
            CustomerSettings();
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
        }
        ///<summary>ctrBestand / cbSchaden_CheckedChanged</summary>
        ///<remarks></remarks>
        private void tsbtnSearch_Click(object sender, EventArgs e)
        {
            SelectedBestandsart = cbBestandsart.Text;
            InitDGV();
        }
        ///<summary>ctrBestand / InitDGV</summary>
        ///<remarks></remarks>        
        public void InitDGV()
        {

            this._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
            string strSql = string.Empty;

            string strSQLE = "Select  distinct a.WaggonNo , 'E' as Richtung , Cast(a.date as date) as Datum, (Select ViewID from ADR where ID=a.Auftraggeber) As Kunde, (Select Cast(SUM(Brutto) as integer) from Artikel where LEingangTableID in(select b.ID from LEingang b where b.WaggonNo=a.WaggonNo and DATEDIFF(dd,a.date,b.date)=0)) as Gewicht   from LEingang a  where a.[check]=1 and Date<='" + dtpBis.Value.ToString() + "' and Date>='" + dtpVon.Value.ToString() + "'";
            string strSQLA = "Select  distinct a.WaggonNo , 'A' as Richtung , Cast(a.datum as date) as Datum, (Select ViewID from ADR where ID=a.Auftraggeber) As Kunde, (Select Cast(SUM(Brutto) as integer) from Artikel where LAusgangTableID in(select b.ID from LAusgang b where b.WaggonNo=a.WaggonNo and DATEDIFF(dd,a.datum,b.datum)=0)) as Gewicht   from LAusgang a  where a.[checked]=1 and Datum<='" + dtpBis.Value.ToString() + "' and Datum>='" + dtpVon.Value.ToString() + "'";

            switch (SelectedBestandsart)
            {
                case "-bitte wählen Sie-":
                    break;
                case "Alle":
                    strSql = strSQLE + " and a.WaggonNo<>''";
                    if (!tbAuftraggeber.Text.Equals(string.Empty))
                    {
                        strSql += " AND Auftraggeber = " + this.ADR.ID;
                    }
                    strSql += " Union " + strSQLA + " and a.WaggonNo <>''";
                    if (!tbAuftraggeber.Text.Equals(string.Empty))
                    {
                        strSql += " AND Auftraggeber = " + this.ADR.ID;
                    }
                    break;
                case "Waggons Eingänge":
                    strSql = strSQLE + " and a.WaggonNo<>''";
                    if (!tbAuftraggeber.Text.Equals(string.Empty))
                    {
                        strSql += " AND Auftraggeber = " + this.ADR.ID;
                    }
                    break;
                case "Waggons Ausgänge":
                    strSql = strSQLA + " and a.WaggonNo<>''";
                    //strSql = "Select distinct a.WaggonNo , 'A' as Richtung , Cast(a.Datum as date) as Datum, (Select ViewID from ADR where ID=a.Auftraggeber) As Kunde, (Select Cast(SUM(Brutto) as integer) from Artikel where LAusgangTableID in(select b.ID from LAusgang b where b.WaggonNo=a.WaggonNo and DATEDIFF(dd,a.date,b.date)=0)) as Gewicht   from LAusgang a  where a.WaggonNo<>'' and Datum<='" + dtpBis.Value.ToString() + "' and Datum>='" + dtpVon.Value.ToString() + "'";
                    if (!tbAuftraggeber.Text.Equals(string.Empty))
                    {
                        strSql += " AND Auftraggeber = " + this.ADR.ID;
                    }
                    break;
                case "Private Waggons":
                    strSql = strSQLE + " and a.WaggonNo<>''";
                    strSql = "Select distinct a.WaggonNo , 'E' as Richtung , Cast(a.date as date) as Datum, (Select ViewID from ADR where ID=a.Auftraggeber) As Kunde, (Select Cast(SUM(Brutto) as integer) from Artikel where LEingangTableID in(select b.ID from LEingang b where b.WaggonNo=a.WaggonNo and DATEDIFF(dd,a.date,b.date)=0)) as Gewicht   from LEingang a  where a.WaggonNo<>'' and (a.WaggonNo like '_3%' or a.WaggonNo like '_4%' or a.WaggonNo like '_5%' or a.WaggonNo like '_6%') and Date<='" + dtpBis.Value.ToString() + "' and Date>='" + dtpVon.Value.ToString() + "' and a.[check]=1";
                    if (!tbAuftraggeber.Text.Equals(string.Empty))
                    {
                        strSql += " AND Auftraggeber = " + this.ADR.ID;
                    }
                    strSql += " UNION Select distinct a.WaggonNo , 'A' as Richtung , Cast(a.Datum as date) as Datum, (Select ViewID from ADR where ID=a.Auftraggeber) As Kunde, (Select Cast(SUM(Brutto) as integer) from Artikel where LAusgangTableID in(select b.ID from LAusgang b where b.WaggonNo=a.WaggonNo and DATEDIFF(dd,a.date,b.date)=0)) as Gewicht   from LAusgang a g where a.WaggonNo<>'' and (a.WaggonNo like '_3%' or a.WaggonNo like '_4%' or a.WaggonNo like '_5%' or a.WaggonNo like '_6%') and Datum<='" + dtpBis.Value.ToString() + "' and Datum>='" + dtpVon.Value.ToString() + "'";
                    if (!tbAuftraggeber.Text.Equals(string.Empty))
                    {
                        strSql += " AND Auftraggeber = " + this.ADR.ID;
                    }

                    break;
                case "Private Waggons Eingänge":
                    strSql = strSQLE + " and a.WaggonNo<>''";
                    strSql = "Select distinct a.WaggonNo , 'E' as Richtung , Cast(a.date as date) as Datum, (Select ViewID from ADR where ID=a.Auftraggeber) As Kunde, (Select Cast(SUM(Brutto) as integer) from Artikel where LEingangTableID in(select b.ID from LEingang b where b.WaggonNo=a.WaggonNo and DATEDIFF(dd,a.date,b.date)=0)) as Gewicht   from LEingang a  where a.WaggonNo<>'' and (a.WaggonNo like '_3%' or a.WaggonNo like '_4%' or a.WaggonNo like '_5%' or a.WaggonNo like '_6%') and Date<='" + dtpBis.Value.ToString() + "' and Date>='" + dtpVon.Value.ToString() + "' and a.[check]=1";
                    if (!tbAuftraggeber.Text.Equals(string.Empty))
                    {
                        strSql += " AND Auftraggeber = " + this.ADR.ID;
                    }
                    break;
                case "Private Waggons Ausgänge":
                    strSql = "Select distinct a.WaggonNo , 'A' as Richtung , Cast(a.Datum as date) as Datum, (Select ViewID from ADR where ID=a.Auftraggeber) As Kunde, (Select Cast(SUM(Brutto) as integer) from Artikel where LAusgangTableID in(select b.ID from LAusgang b where b.WaggonNo=a.WaggonNo and DATEDIFF(dd,a.date,b.date)=0)) as Gewicht   from LAusgang a where a.WaggonNo<>'' and (a.WaggonNo like '_3%' or a.WaggonNo like '_4%' or a.WaggonNo like '_5%' or a.WaggonNo like '_6%') and Datum<='" + dtpBis.Value.ToString() + "' and Datum>='" + dtpVon.Value.ToString() + "'";
                    if (!tbAuftraggeber.Text.Equals(string.Empty))
                    {
                        strSql += " AND Auftraggeber = " + this.ADR.ID;
                    }
                    break;
                case "V. an Private Waggons":
                    strSql = strSQLE + " and a.WaggonNo<>''";
                    strSql = "Select distinct a.WaggonNo , 'A' as Richtung, Cast(Datum as date) as Datum   from Lausgang where a.WaggonNo<>'' and (a.WaggonNo like '_5%' or a.WaggonNo like '_6%')  and Date<='" + dtpBis.Value.ToString() + "' and Date>='" + dtpVon.Value.ToString() + "' UNION " +
                             "Select distinct a.WaggonNo , 'E' as Richtung , Cast(date as date) as Datum  from LEingang where a.WaggonNo<>'' and (a.WaggonNo like '_5%' or a.WaggonNo like '_6%') and Datum<='" + dtpBis.Value.ToString() + "' and Datum>='" + dtpVon.Value.ToString() + "'";

                    break;
                case "V. an Private  distinct Waggons Eingänge":
                    strSql = strSQLE + " and a.WaggonNo<>''";
                    strSql = "Select distinct a.WaggonNo ,'E' as Richtung , Cast(date as date) as Datum  from LEingang where a.WaggonNo<>'' and (a.WaggonNo like '_5%' or a.WaggonNo like '_6%') and Date<='" + dtpBis.Value.ToString() + "' and Date>='" + dtpVon.Value.ToString() + "'";
                    break;
                case "V. an Private  distinct Waggons Ausgänge":
                    strSql = "Select distinct a.WaggonNo, 'A' as Richtung, Cast(Datum as date) as Datum   from Lausgang where a.WaggonNo<>'' and (a.WaggonNo like '_5%' or a.WaggonNo like '_6%') and Datum<='" + dtpBis.Value.ToString() + "' and Datum>='" + dtpVon.Value.ToString() + "'";
                    break;
                case "DB Waggons":
                    strSql = strSQLE + " and (a.WaggonNo like '_1%' or a.WaggonNo like '_2%')";
                    if (!tbAuftraggeber.Text.Equals(string.Empty))
                    {
                        strSql += " AND Auftraggeber = " + this.ADR.ID;
                    }
                    strSql += "  UNION  Select distinct a.WaggonNo , 'A' as Richtung , Cast(a.Datum as date) as Datum, (Select ViewID from ADR where ID=a.Auftraggeber) As Kunde, (Select Cast(SUM(Brutto) as integer) from Artikel where LAusgangTableID in(select b.ID from LAusgang b where b.WaggonNo=a.WaggonNo and DATEDIFF(dd,a.date,b.date)=0)) as Gewicht   from LAusgang a  where a.WaggonNo<>'' and (a.WaggonNo like '_1%' or a.WaggonNo like '_2%') and Datum<='" + dtpBis.Value.ToString() + "' and Datum>='" + dtpVon.Value.ToString() + "'";
                    if (!tbAuftraggeber.Text.Equals(string.Empty))
                    {
                        strSql += " AND Auftraggeber = " + this.ADR.ID;
                    }
                    break;
                case "DB Waggons Eingänge":
                    strSql = strSQLE + " and a.WaggonNo<>''";
                    strSql = "Select distinct a.WaggonNo , 'E' as Richtung , Cast(a.date as date) as Datum, (Select ViewID from ADR where ID=a.Auftraggeber) As Kunde, (Select Cast(SUM(Brutto) as integer) from Artikel where LEingangTableID in(select b.ID from LEingang b where b.WaggonNo=a.WaggonNo and DATEDIFF(dd,a.date,b.date)=0)) as Gewicht   from LEingang a  where a.WaggonNo<>'' and (a.WaggonNo like '_1%' or a.WaggonNo like '_2%') and Date<='" + dtpBis.Value.ToString() + "' and Date>='" + dtpVon.Value.ToString() + "' and a.[check]=1";
                    break;
                case "DB Waggons Ausgänge":

                    strSql = "Select distinct a.WaggonNo , 'A' as Richtung , Cast(a.Datum as date) as Datum, (Select ViewID from ADR where ID=a.Auftraggeber) As Kunde, (Select Cast(SUM(Brutto) as integer) from Artikel where LAusgangTableID in(select b.ID from LAusgang b where b.WaggonNo=a.WaggonNo and DATEDIFF(dd,a.date,b.date)=0)) as Gewicht   from LAusgang a  where a.WaggonNo<>'' and (a.WaggonNo like '_1%' or a.WaggonNo like '_2%') and Datum<='" + dtpBis.Value.ToString() + "' and Datum>='" + dtpVon.Value.ToString() + "'";
                    break;
            }

            DataTable dtWaggons = clsSQLcon.ExecuteSQL_GetDataTable(strSql, this.GL_User.User_ID, "Waggons");
            this.dgv.DataSource = dtWaggons;
            dgv.SortDescriptors.Add("Datum", ListSortDirection.Ascending);
            tbAnzahl.Text = dtWaggons.Rows.Count.ToString();
            //tbAnzahl.Text = dtWaggons.Rows.Count.ToString();
            //tbAnzahl.Text = dtWaggons.Rows.Count.ToString();

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
        private void SetAuswahlWaggonDaten()
        {
            //Datum
            string strTmp = "01." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Year.ToString();
            dtpVon.Value = Convert.ToDateTime(strTmp);
            dtpBis.Value = DateTime.Now.Date.AddDays(1);
            ClearAdressEingabeFelder();

            //Bestandsarten Combo
            cbBestandsart.DataSource = ctrWaggonbuchSettings.InitTableWaggonBuchAuswahl();
            cbBestandsart.DisplayMember = "Waggonansicht";
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

            DataTable dt = ctrWaggonbuchSettings.InitTableWaggonBuchAuswahl();
            dt.DefaultView.RowFilter = "ID=" + myBestArtID;
            DataTable dtTmp = dt.DefaultView.ToTable();
            bool bCheck = (bool)dtTmp.Rows[0]["ADRRequire"];
            SetAdrEingabeEnabeld(bCheck);
            bool bDate = (bool)dtTmp.Rows[0]["DateRequire"];
            SetDateTimePickerEnabeld(bDate);

            //wenn Tagesbestand Datum für Stichtag freigeben
            //if (cbBestandsart.Text == "Tagesbestand")
            //{
            //   dtpVon.Enabled = true;
            //   dtpVon.Value = DateTime.Now.Date;
            //   lZeitraumVon.Text = "Stichtag:";

            //}
            //else if (cbBestandsart.Text == "Tagesbestand [Lager komplett]")
            //{
            //   dtpVon.Enabled = true;
            //   dtpVon.Value = DateTime.Now.Date;
            //   lZeitraumVon.Text = "Stichtag:";

            //   //wenn Tagebestand, dann soll der Filter aktiviert werden
            //   //cbFilter.Enabled = true;
            //}
            //else
            //{
            //   lZeitraumVon.Text = "Zeitraum von:";
            //}
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
            }
            else
            {
                tbAuftraggeber.Text = string.Empty;
                ADR.ID = 0;
            }
            ADR.Fill();
        }
        ///<summary>ctrBestand / tstbSearchArtikel_TextChanged</summary>
        ///<remarks>Direkte Suche im Grid</remarks>
        private void tstbSearchArtikel_TextChanged(object sender, EventArgs e)
        {
            tstbSearchArtikel.Text = tstbSearchArtikel.Text.Trim();
            string strFilter = "";
            strFilter = Functions.GetSearchFilterString(ref tscbSearch, tscbSearch.Text, tstbSearchArtikel.Text);
            dtBestand.DefaultView.Sort = tscbSearch.Text.ToString();
            dtBestand.DefaultView.RowFilter = strFilter;
        }
        ///<summary>ctrBestand / tsbtnClose_Click</summary>
        ///<remarks>Ctr schliessen</remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this._ctrMenu.CloseCtrWaggonbuch();
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
            SetAuswahlWaggonDaten();
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
        ///<summary>ctrBestand / tsbtnExcel_Click</summary>
        ///<remarks>Wenn eine neue suchspalte gewählt wird, so soll der suchbegriff geleert werden</remarks>      
        private void tsbtnExcel_Click(object sender, EventArgs e)
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
            Functions.Telerik_RunExportToExcelML(ref this._ctrMenu._frmMain, ref this.dgv, FileName, ref openExportFile, this.GL_User, true);

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
                    e.ToolTipText = cell.Value.ToString();
                }
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
        //private void tsbcViews_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //   if (SelectedBestandsart != "")
        //   {
        //      if (tsbcViews.SelectedIndex > -1)
        //      {
        //         //dgv.DataSource = null;
        //         //InitDGV();
        //         Functions.setView(ref dtBestand, ref dgv, "Bestand", tsbcViews.SelectedItem.ToString(), this._ctrMenu._frmMain.GL_System, false, dts);
        //         this.dgv.BestFitColumns();
        //         this._ctrArtSearchFilter.SetFilterforDGV(ref this.dgv, false);
        //      }
        //   }
        //}
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

                    case "Ungeprüfte Artikel im Eingang":
                        separator = new RadMenuSeparatorItem();
                        e.ContextMenu.Items.Add(separator);
                        customMenuItem = new RadMenuItem();
                        customMenuItem.Text = "Gehe zu Eingang";
                        customMenuItem.Click += new EventHandler(this.geheZuEingang);
                        e.ContextMenu.Items.Add(customMenuItem);
                        break;


                    case "Ungeprüfte Artikel im Ausgang":
                        separator = new RadMenuSeparatorItem();
                        e.ContextMenu.Items.Add(separator);
                        customMenuItem = new RadMenuItem();
                        customMenuItem.Text = "Gehe zu Ausgang";
                        customMenuItem.Click += new EventHandler(this.geheZuAusgang);
                        e.ContextMenu.Items.Add(customMenuItem);
                        break;
                    case "Artikel in offenen Eingängen":
                        separator = new RadMenuSeparatorItem();
                        e.ContextMenu.Items.Add(separator);
                        customMenuItem = new RadMenuItem();
                        customMenuItem.Text = "Gehe zu Eingang";
                        customMenuItem.Click += new EventHandler(this.geheZuEingang);
                        e.ContextMenu.Items.Add(customMenuItem);
                        break;


                    case "Artikel in offenen Ausgängen":
                        separator = new RadMenuSeparatorItem();
                        e.ContextMenu.Items.Add(separator);
                        customMenuItem = new RadMenuItem();
                        customMenuItem.Text = "Gehe zu Ausgang";
                        customMenuItem.Click += new EventHandler(this.geheZuAusgang);
                        e.ContextMenu.Items.Add(customMenuItem);
                        break;
                    case "Nicht abgeschlossene Eingänge":
                        separator = new RadMenuSeparatorItem();
                        e.ContextMenu.Items.Add(separator);
                        customMenuItem = new RadMenuItem();
                        customMenuItem.Text = "Gehe zu Eingang";
                        customMenuItem.Click += new EventHandler(this.geheZuEingangEA);
                        e.ContextMenu.Items.Add(customMenuItem);
                        break;

                    case "Nicht abgeschlossene Ausgänge":
                        separator = new RadMenuSeparatorItem();
                        e.ContextMenu.Items.Add(separator);
                        customMenuItem = new RadMenuItem();
                        customMenuItem.Text = "Gehe zu Ausgang";
                        customMenuItem.Click += new EventHandler(this.geheZuAusgangEA);
                        e.ContextMenu.Items.Add(customMenuItem);
                        break;
                }
            }
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
                this._ctrMenu.ShowOrHideSpecificCtr(this, false);
            }
        }
        ///<summary>ctrBestand / geheZuEingangEA</summary>
        ///<remarks></remarks>
        private void geheZuEingangEA(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            Decimal.TryParse(dgv.SelectedRows[0].Cells["Eingang"].Value.ToString(), out decTmp);
            if (decTmp > 0)
            {
                object obj = new object();
                _ctrMenu.CloseCtrEinlagerung();
                _ctrMenu.OpenCtrEinlagerung(obj, true);
                _ctrMenu._ctrEinlagerung.SetSearchLEingangskopfdatenToFrmEA(decTmp);
                this._ctrMenu.ShowOrHideSpecificCtr(this, false);
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            clsPrint print = new clsPrint(this.dtBestand, _ctrMenu._frmMain.GL_System, "Kunden", 0); // viewName // CF
            print.printTitel = cbBestandsart.Text + " / " + tbAuftraggeber.Text;
            print.Print(true, true, _ctrMenu);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
