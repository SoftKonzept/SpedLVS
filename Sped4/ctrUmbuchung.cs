using LVS;
using LVS.Constants;
using Sped4.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.UI;
namespace Sped4
{
    public partial class ctrUmbuchung : UserControl
    {
        internal clsUmbuchung Umbuchung = new clsUmbuchung();
        public frmTmp _frmTmp;
        public ctrEinlagerung _ctrEinlagerung;
        internal Globals._GL_USER GL_User;
        internal Globals._GL_SYSTEM GL_System;
        public ctrMenu _ctrMenu;
        public decimal _ArtikelIDTakeOver = 0;
        internal string _ADRSearch;
        public Int32 SearchButton = 0;      //für Suche in FrmAdrSearch
        internal DataTable dtArtikel;
        internal DataTable dtArtikelUmbuchung;
        internal DataTable dtMandanten;
        internal decimal _MandantenID = 0;
        internal string _MandantenName = string.Empty;
        internal bool bFirstSelect = true;
        internal ctrArtSearchFilter _ctrArtSearchFilter;
        internal string viewName = "Bestand";
        DataColumn[] dts;

        internal clsASNTransfer AsnTransfer;

        public ctrUmbuchung()
        {
            InitializeComponent();
        }
        ///<summary>ctrUmbuchung / InitCtr</summary>
        ///<remarks>Folgende Funktionen werden ausgeführt:
        ///         </remarks>
        public void InitCtr()
        {
            Umbuchung = new clsUmbuchung();
            Umbuchung.System = this._ctrMenu._frmMain.system;
            dtArtikel = new DataTable();
            dtArtikelUmbuchung = new DataTable();

            _MandantenID = this._ctrMenu._frmMain.GL_System.sys_MandantenID;
            this.GL_System = this._ctrMenu._frmMain.GL_System;
            this.GL_User = this._ctrMenu._frmMain.GL_User;

            if (this._ctrEinlagerung != null)
            {
                if ((this.Umbuchung.LEingangUB == 0) && (this.Umbuchung.LAusgangID == 0))
                {
                    _ArtikelIDTakeOver = this._ctrEinlagerung._ArtikelIDTakeOverUB;
                    InitUmbuchung();
                }
            }
            //Button deaktivieren
            this.btnEmpfänger.Enabled = false;
            this.btnAuftraggeber.Enabled = false;

            InitFilterSearchCtr();
            //tsmBestandSource.Enabled = false;
            Functions.InitComboViews(_ctrMenu._frmMain.GL_System, ref tscbView, viewName);
            CustomerSettings();
        }
        ///<summary>ctrUmbuchung / CustomerSettings</summary>
        ///<remarks>Hier kann den Kundenwünschen entsprechend ein / mehere Elemente 
        ///         ein-/ausgeblendet werden</remarks>
        private void CustomerSettings()
        {
            //ein-/ausklappen UB-Kostenzuweisung
            this.splitPanel2.Collapsed = (!this._ctrMenu._frmMain.GL_System.Modul_Fakt_UB_DifferentCalcAssignment);
            //Erweiterete Suche -> Panel ausbelnden
            this.splitPanel5.Collapsed = (!this._ctrMenu._frmMain.system.Client.Modul.EnableAdvancedSearch);
            //DirectSearch
            this.tscbSearch.Visible = this._ctrMenu._frmMain.system.Client.Modul.EnableDirectSearch;
            this.tstbSearchArtikel.Visible = this._ctrMenu._frmMain.system.Client.Modul.EnableDirectSearch;
            this.tslSearchText.Visible = this._ctrMenu._frmMain.system.Client.Modul.EnableDirectSearch;

            //-- tsbtnAuftraggeber
            this._ctrMenu._frmMain.system.Client.ctrUmbuchung_CustomizeBtnAuftraggeberEnabled(ref this.btnAuftraggeber);
            this._ctrMenu._frmMain.system.Client.ctrUmbuchung_CustomizeBtnEmpfängerEnabled(ref this.btnEmpfänger);
        }
        ///<summary>ctrUmbuchung / InitFilterSearchCtr</summary>
        ///<remarks></remarks>
        private void InitFilterSearchCtr()
        {
            _ctrArtSearchFilter = new ctrArtSearchFilter();
            _ctrArtSearchFilter.InitCtr(this);
            _ctrArtSearchFilter.Dock = DockStyle.Fill;
            _ctrArtSearchFilter.Parent = this.splitPanel5;
            _ctrArtSearchFilter.Show();
            _ctrArtSearchFilter.BringToFront();
            this.splitPanel5.Width = _ctrArtSearchFilter.Width;
        }
        ///<summary>ctrUmbuchung / InitUmbuchung</summary>
        ///<remarks>Umbuchung wird separat gestartet. Nach dem der Abbuchungsbestand gewählt wurde 
        ///         kann die Umbuchung initialisiert werden:
        ///         - Laden des Bestand
        ///         - freigeben der Felder</remarks>
        public void InitUmbuchung()
        {
            Umbuchung.ArtikelID = _ArtikelIDTakeOver;
            Umbuchung._GL_User = this.GL_User;
            Umbuchung._GL_System = this.GL_System;
            Umbuchung.MandantenID = _MandantenID;
            //Artikedaten und alte Lagereingangsdaten werden geladen
            Umbuchung.InitUmbuchung();
            SetUmbuchungsdaten();
            SetUmbuchungsdatenFelderEnabled(true);
        }
        ///<summary>ctrUmbuchung / InitDGVBestand</summary>
        ///<remarks></remarks>
        private void InitDGVBestand()
        {
            if (Umbuchung.AuftraggeberAltID > 0)
            {
                dtArtikel = new DataTable();
                Umbuchung.LEingang.Auftraggeber = Umbuchung.AuftraggeberAltID;
                Umbuchung.LEingang.MandantenID = _MandantenID;
                Umbuchung.LEingang.AbBereichID = this.GL_System.sys_ArbeitsbereichID;

                dtArtikel = Umbuchung.LEingang.GetLagerArtikelDatenByAuftraggeber(0);

                //Die Spalte Check wird nicht benötigt und kann gelöscht werden
                if (dtArtikel.Columns["Check"] != null)
                    dtArtikel.Columns.Remove("Check");
                //Spalte   umbenennen in umbuchen
                //dtArtikel.Columns["Selected"].ColumnName = "umbuchen";
                dtArtikel.AcceptChanges();
                //Functions.InitComboSearch(ref tscbSearch, dtArtikel);
                dts = new DataColumn[dtArtikel.Columns.Count];
                dtArtikel.Columns.CopyTo(dts, 0);

                if (this._ArtikelIDTakeOver > 0)
                {
                    foreach (DataRow row in dtArtikel.Rows)
                    {
                        decimal decTmp = 0;
                        decimal.TryParse(row["ArtikelID"].ToString(), out decTmp);
                        if (decTmp > 0)
                        {
                            if (decTmp == this._ArtikelIDTakeOver)
                            {
                                row["Selected"] = true;
                            }
                        }
                    }
                }
                DGVConnect();
                InitDGVAArtikel();
            }
            else
            {
                if (dtArtikel != null)
                {
                    dtArtikel.Rows.Clear();
                    this.dgv.DataSource = dtArtikel;
                    InitDGVAArtikel();
                }
            }
            //TestSearch
            Functions.InitComboSearch(ref tscbSearch, dtArtikel, this._ctrMenu._frmMain.system);
        }
        ///<summary>ctrUmbuchung / InitDGVBestand</summary>
        ///<remarks></remarks>
        private void DGVConnect()
        {
            try
            {
                if (tscbView.SelectedItem != null)
                {
                    //this.dgv.DataSource = dtArtikel;
                    Functions.setView(ref dtArtikel, ref dgv, viewName, tscbView.SelectedItem.ToString(), this._ctrMenu._frmMain.GL_System, true, dts);
                    SetColumns(ref dgv);
                    //Spalte Selected an erste Stelle verschieben
                    //for (Int32 i = 0; i <= this.dgv.Columns.Count - 1; i++)
                    //{
                    //    string strColName = this.dgv.Columns[i].Name;
                    //    switch (strColName)
                    //    {
                    //        case "Selected":
                    //            this.dgv.Columns.Move(this.dgv.Columns["Selected"].Index, 0);
                    //            this.dgv.Columns["Selected"].ReadOnly = false;
                    //            break;

                    //        case "Eingangsdatum":
                    //            this.dgv.Columns[i].FormatString = "{0:d}";
                    //            break;

                    //        case "Ausgang":
                    //        case "UB_AltCalcEinlagerung":
                    //        case "UB_AltCalcAuslagerung":
                    //        case "UB_AltCalcLagergeld":
                    //        case "UB_NeuCalcEinlagerung":
                    //        case "UB_NeuCalcAuslagerung":
                    //        case "UB_NeuCalcLagergeld":
                    //            this.dgv.Columns[i].IsVisible = false;
                    //            break;

                    //        default:
                    //            this.dgv.Columns[i].IsVisible = true;
                    //            break;
                    //    }
                    //}
                    this.dgv.BestFitColumns();
                    if (this._ctrArtSearchFilter != null)
                    {
                        this._ctrArtSearchFilter.ClearFilterInput();
                        this._ctrArtSearchFilter.SetFilterSearchElementAllEnabled(false);
                        this._ctrArtSearchFilter.SetFilterElementEnabledByColumns(ref this.dgv);
                    }
                }
            }
            catch (Exception ex)
            {
                string Error = ex.ToString();
            }
        }
        ///<summary>ctrUmbuchung / tbSearchA_TextChanged</summary>
        ///<remarks>Adresssuche</remarks>
        private void InitDGVAArtikel()
        {
            dtArtikelUmbuchung = dtArtikel.Copy();
            dtArtikelUmbuchung.Clear();
            DGVAArtikelConnect();
        }
        ///<summary>ctrUmbuchung / InitDGVBestand</summary>
        ///<remarks></remarks>
        private void DGVAArtikelConnect()
        {
            dtArtikelUmbuchung.DefaultView.RowFilter = string.Empty;
            this.dgvAArtikel.DataSource = null;
            if (dtArtikelUmbuchung.Columns.Contains("Selected"))
            {
                dtArtikelUmbuchung.Columns["Selected"].SetOrdinal(0);
            }
            this.dgvAArtikel.DataSource = dtArtikelUmbuchung;
            SetColumns(ref dgvAArtikel);
            this.dgvAArtikel.BestFitColumns();
        }
        ///<summary>ctrUmbuchung / ctrUmbuchung_Load</summary>
        ///<remarks>Die entsprechenden Daten der Umbuchung werden ins Ctr eingetragen</remarks>
        private void SetUmbuchungsdaten()
        {
            dtpEinlagerungDate.Value = DateTime.Now;
            tbLAusgangID.Text = Umbuchung.LAusgangID.ToString();
            tbLEingangID.Text = Umbuchung.LEingangUB.ToString();

            //Adressen
            //Auftraggeber alt
            SearchButton = 8;
            SetADRByID(Umbuchung.AuftraggeberAltID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myDgv"></param>
        private void SetColumns(ref RadGridView myDgv)
        {
            //Spalte Selected an erste Stelle verschieben

            for (Int32 i = 0; i <= myDgv.Columns.Count - 1; i++)
            {
                string strColName = myDgv.Columns[i].Name;
                switch (strColName)
                {
                    case "Selected":
                        if (this.dgv.ColumnCount > 0)
                        {
                            myDgv.Columns.Move(this.dgv.Columns["Selected"].Index, 0);
                        }
                        myDgv.Columns["Selected"].ReadOnly = false;
                        myDgv.Columns["Selected"].IsVisible = true;
                        break;

                    case "Eingangsdatum":
                        myDgv.Columns[i].FormatString = "{0:d}";
                        break;

                    case "Ausgang":
                    case "UB_AltCalcEinlagerung":
                    case "UB_AltCalcAuslagerung":
                    case "UB_AltCalcLagergeld":
                    case "UB_NeuCalcEinlagerung":
                    case "UB_NeuCalcAuslagerung":
                    case "UB_NeuCalcLagergeld":
                        myDgv.Columns[i].IsVisible = false;
                        break;

                    default:
                        myDgv.Columns[i].IsVisible = myDgv.Columns[i].IsVisible = true;
                        break;
                }
            }


        }

        ///<summary>ctrUmbuchung / ctrUmbuchung_Load</summary>
        ///<remarks>Die entsprechenden Daten der Umbuchung werden ins Ctr eingetragen</remarks>
        private void SetUmbuchungsdatenFelderEnabled(bool bEnabled)
        {
            gboxLEDaten.Enabled = bEnabled;
            //UB aus Einlagerung, dann Originalbestandauswahl aus
            tbSearchA.Enabled = bEnabled;
            btnSearchA.Enabled = bEnabled;

            btnAuftraggeber.Enabled = bEnabled;
            btnEmpfänger.Enabled = bEnabled;

            tbMCAuftraggeber.Enabled = bEnabled;
            tbMCEmpfänger.Enabled = bEnabled;

            tbAuftraggeberAlt.Enabled = bEnabled;
            tbADRAuftraggeber.Enabled = bEnabled;
            tbADREmpfänger.Enabled = bEnabled;

            tbLieferantenID.Enabled = bEnabled;
            tbEArtAnzahl.Enabled = bEnabled;
            tbBruttoGesamt.Enabled = bEnabled;
            tbNettoGesamt.Enabled = bEnabled;

            dtpEinlagerungDate.Enabled = bEnabled;
            tbLAusgangID.Enabled = bEnabled;
            tbLEingangID.Enabled = bEnabled;
        }
        ///<summary>ctrUmbuchung / ClearFrm</summary>
        ///<remarks>Leeren aller Eingabefelder</remarks>
        private void ClearFrm()
        {
            tbSearchA.Text = string.Empty;
            tbMCAuftraggeber.Text = string.Empty;
            tbMCEmpfänger.Text = string.Empty;

            tbAuftraggeberAlt.Text = string.Empty;
            tbADRAuftraggeber.Text = string.Empty;
            tbADREmpfänger.Text = string.Empty;

            tbLieferantenID.Text = string.Empty;
            tbEArtAnzahl.Text = "0";
            tbBruttoGesamt.Text = Functions.FormatDecimal(0);
            tbNettoGesamt.Text = Functions.FormatDecimal(0);

            dtpEinlagerungDate.Value = DateTime.Now;
            tbLAusgangID.Text = string.Empty;
            tbLEingangID.Text = string.Empty;

            tsmBestandSource.Enabled = false;

            dtArtikel.Clear();
            dtArtikelUmbuchung.Clear();
        }
        ///<summary>ctrUmbuchung / ctrUmbuchung_Load</summary>
        ///<remarks>Die entsprechenden Daten der Umbuchung werden ins Ctr eingetragen</remarks>
        private void dtpEinlagerungDate_ValueChanged(object sender, EventArgs e)
        {
            //Wenn OK dann in Ein- auslagerung einfügen
            string strHour = DateTime.Now.Hour.ToString();
            string strMin = DateTime.Now.Minute.ToString();
            string strDate = dtpEinlagerungDate.Value.Date.ToShortDateString() + " " + strHour + ":" + strMin;
            DateTime dtTmp;
            DateTime.TryParse(strDate, out dtTmp);
            dtpEinlagerungDate.Value = dtTmp;
        }
        ///<summary>ctrUmbuchung / btnSearchA_Click</summary>
        ///<remarks></remarks>
        private void btnSearchA_Click(object sender, EventArgs e)
        {
            _ADRSearch = "Auftraggeber";
            SearchButton = 8;
            this._ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrUmbuchung / btnAuftraggeber_Click</summary>
        ///<remarks></remarks>
        private void btnAuftraggeber_Click(object sender, EventArgs e)
        {
            _ADRSearch = "Auftraggeber";
            SearchButton = 1;
            this._ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrUmbuchung / ctrUmbuchung_Load</summary>
        ///<remarks></remarks>
        private void btnEmpfänger_Click(object sender, EventArgs e)
        {
            _ADRSearch = "Empfänger";
            SearchButton = 3;
            this._ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrUmbuchung/SetADRByID</summary>
        ///<remarks>Adressdaten werden anhand der ID ermittelt und in die entsprechenden
        ///         Eingabefelder übergeben. Der Matchcode aus den Adressen wird vorerst
        ///         hier mitübernommen wird später noch einmal überschrieben.</remarks>
        public void SetADRByID(decimal ADR_ID)
        {
            if (ADR_ID > 0)
            {
                string strE = string.Empty;
                string strMC = string.Empty;
                DataSet ds = clsADR.ReadADRbyID(ADR_ID);

                for (Int32 i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    strMC = ds.Tables[0].Rows[i]["ViewID"].ToString();
                    strMC = strMC.ToString().Trim();
                    strE = ds.Tables[0].Rows[i]["ViewID"].ToString() + " - ";
                    strE = strE.Trim();



                    string strName = string.Empty;
                    string strPLZ = string.Empty;
                    string strOrt = string.Empty;

                    strName = ds.Tables[0].Rows[i]["Name1"].ToString().Trim();
                    strPLZ = ds.Tables[0].Rows[i]["PLZ"].ToString().Trim();
                    strOrt = ds.Tables[0].Rows[i]["Ort"].ToString().Trim();

                    strE = strName + " - " + strPLZ + " - " + strOrt;
                    switch (SearchButton)
                    {
                        //KD / Auftraggeber
                        case 1:
                            Umbuchung.AuftraggeberNeuID = ADR_ID;
                            tbMCAuftraggeber.Text = strMC;
                            tbADRAuftraggeber.Text = strE;
                            //Laden des Originalbestandes, wenn der Empfänger ausgewählt ist
                            InitDGVBestand();
                            SumArtikelUmbuchung();
                            tsmBestandSource.Enabled = true;
                            //Lieferantenverweis
                            this.tbLieferantenID.Text = clsADRVerweis.GetLieferantenVerweisBySenderAndReceiverAdr(Umbuchung.AuftraggeberAltID, Umbuchung.AuftraggeberNeuID, this.GL_User.User_ID, constValue_AsnArt.const_Art_VDA4913, this.GL_System.sys_ArbeitsbereichID);
                            break;

                        // Versender
                        case 2:
                            break;

                        // Empfänger
                        case 3:
                            //Empfänger
                            Umbuchung.EmpfaengerID = ADR_ID;
                            tbMCEmpfänger.Text = strMC;
                            tbADREmpfänger.Text = strE;
                            break;

                        //neutrale Versandadresse
                        case 4:
                            break;

                        // neutrale Empfangsadresse
                        case 5:
                            break;

                        // Mandanten
                        case 6:
                            break;

                        // Spedition
                        case 7:
                            break;

                        // KD /Auftraggeber alt bei Umbuchung
                        case 8:
                            //ist hier besonderheit, da 2 der Auftraggeber benötigt wird                        
                            Umbuchung.AuftraggeberAltID = ADR_ID;
                            tbSearchA.Text = strMC;
                            tbAuftraggeberAlt.Text = strE;

                            clsClient.ctrUmbuchung_CustomizeDefaulUBDaten(ref this._ctrMenu._frmMain.system, ref this.Umbuchung);
                            this.btnEmpfänger.Enabled = !(this.Umbuchung.EmpfaengerID > 0);
                            this.btnAuftraggeber.Enabled = !(Umbuchung.AuftraggeberNeuID > 0);
                            if (Umbuchung.AuftraggeberNeuID > 0)
                            {
                                _ADRSearch = "Auftraggeber";
                                SearchButton = 1;
                                SetADRByID(Umbuchung.AuftraggeberNeuID);
                            }
                            if (this.Umbuchung.EmpfaengerID > 0)
                            {
                                _ADRSearch = "Empfänger";
                                SearchButton = 3;
                                SetADRByID(this.Umbuchung.EmpfaengerID);
                            }

                            tsmBestandSource.Enabled = ((Umbuchung.AuftraggeberAltID > 0) && (Umbuchung.AuftraggeberNeuID > 0));

                            //Alles wieder zurücksetzen
                            _ADRSearch = "Auftraggeber";
                            SearchButton = 8;
                            break;
                    }
                }
            }
            SearchButton = 0;
        }
        ///<summary>ctrUmbuchung / tbSearchA_TextChanged</summary>
        ///<remarks>Adresssuche</remarks>
        private void tbSearchA_TextChanged(object sender, EventArgs e)
        {
            DataTable dtTmp = Functions.GetADRTableSearchResultTable(tbSearchA.Text, this.GL_User);
            if (dtTmp.Rows.Count > 0)
            {
                //ClearFrm();
                tbAuftraggeberAlt.Text = Functions.GetADRStringFromTable(dtTmp);
                Umbuchung.AuftraggeberAltID = Functions.GetADR_IDFromTable(dtTmp);
                //tsmBestandSource.Enabled = false;
                SearchButton = 8;
                SetADRByID(Umbuchung.AuftraggeberAltID);
            }
            else
            {
                tbAuftraggeberAlt.Text = string.Empty;
                Umbuchung.AuftraggeberAltID = 0;
            }
        }
        ///<summary>ctrUmbuchung / tbSearchA_TextChanged</summary>
        ///<remarks>Adresssuche, der neue Bestand wurde gewählt</remarks>
        private void tbMCAuftraggeber_TextChanged(object sender, EventArgs e)
        {
            DataTable dtTmp = Functions.GetADRTableSearchResultTable(tbMCAuftraggeber.Text, this.GL_User);
            if (dtTmp.Rows.Count > 0)
            {
                tbADRAuftraggeber.Text = Functions.GetADRStringFromTable(dtTmp);
                Umbuchung.AuftraggeberNeuID = Functions.GetADR_IDFromTable(dtTmp);
                //Laden des Originalbestandes, wenn der Empfänger ausgewählt ist
                InitDGVBestand();
                SumArtikelUmbuchung();
                tsmBestandSource.Enabled = true;
            }
            else
            {
                tbADRAuftraggeber.Text = string.Empty;
                Umbuchung.AuftraggeberNeuID = 0;
            }

        }
        ///<summary>ctrUmbuchung / tbMCEmpfänger_TextChanged</summary>
        ///<remarks></remarks>
        private void tbMCEmpfänger_TextChanged(object sender, EventArgs e)
        {
            DataTable dtTmp = Functions.GetADRTableSearchResultTable(tbMCEmpfänger.Text, this.GL_User);
            if (dtTmp.Rows.Count > 0)
            {
                tbADREmpfänger.Text = Functions.GetADRStringFromTable(dtTmp);
                Umbuchung.EmpfaengerID = Functions.GetADR_IDFromTable(dtTmp);
            }
            else
            {
                tbADREmpfänger.Text = string.Empty;
                Umbuchung.EmpfaengerID = 0;
            }
        }
        ///<summary>ctrUmbuchung / tbSearchA_Validating</summary>
        ///<remarks>entsprechender Bestand wird geladen</remarks>
        private void tbSearchA_Validating(object sender, CancelEventArgs e)
        {
            InitDGVBestand();
        }
        ///<summary>ctrUmbuchung / SumArtikelUmbuchung</summary>
        ///<remarks>Ermittel bei jeder Veränderung der Umbuchung Gewicht und Anzahl neu.</remarks>
        private void SumArtikelUmbuchung()
        {
            //Ermittlung und Anzeige im Eingangskopf von Anzahl und Gewicht
            object objNetto = 0;
            object objBrutto = 0;
            Int32 iCount = 0;
            if ((dtArtikelUmbuchung != null) && (dtArtikelUmbuchung.Rows.Count > 0))
            {
                objNetto = dtArtikelUmbuchung.Compute("SUM(Netto)", "LVSNr>0");
                objBrutto = dtArtikelUmbuchung.Compute("SUM(Brutto)", "LVSNr>0");
                iCount = dtArtikelUmbuchung.Rows.Count;
            }
            tbNettoGesamt.Text = Functions.FormatDecimal(Convert.ToDecimal(objNetto.ToString()));
            tbBruttoGesamt.Text = Functions.FormatDecimal(Convert.ToDecimal(objBrutto.ToString()));
            tbEArtAnzahl.Text = iCount.ToString();
        }
        ///<summary>ctrUmbuchung/RemoveRowFromArtUmbuchung</summary>
        ///<remarks>Copiert den gewählten Datensatz in die Ausgangstable und löscht den entsprechenden 
        ///         Datensatz aus der Aritkeltabelle.</remarks>
        private void RemoveRowFromArtUmbuchung()
        {
            //prüfen, ob die UB Tabelle Datensätze enthält
            if (dtArtikelUmbuchung.Rows.Count > 0)
            {
                Int32 i = 0;
                while ((i < dtArtikelUmbuchung.Rows.Count) && (dtArtikelUmbuchung.Rows.Count > 0))
                {
                    if (dtArtikelUmbuchung.Rows[i]["Selected"] != null)
                    {
                        if ((bool)dtArtikelUmbuchung.Rows[i]["Selected"])
                        {
                            //Selected auf False, damit es in der dtArtikel angezeigt wird
                            dtArtikelUmbuchung.Rows[i]["Selected"] = false;
                            //Copieren des Datensatzes
                            dtArtikel.ImportRow(dtArtikelUmbuchung.Rows[i]);
                            //Datensatz aus der Talbe Umbuchung löschen
                            dtArtikelUmbuchung.Rows.RemoveAt(i);
                            //da sich nun die Anzahl der Datensätze verändert hat, 
                            //wird die Tabelle nun von vorne wieder durchlaufen,
                            //solange keine gewählten Datensätze mehr vorliegen
                            i = -1;
                        }
                    }
                    i++;
                }
                DGVAArtikelConnect();
            }
        }
        ///<summary>ctrUmbuchung/ResetArtikelinDGV</summary>
        ///<remarks>Setzt die Spalte Selected wieder auf False und dadurch wir der Artikel wieder angezeigt.</remarks>
        private void ResetArtikelinDGV(decimal myArtikelID)
        {
            for (Int32 i = 0; i <= dtArtikel.Rows.Count - 1; i++)
            {
                //nur ein Datensatz wird zurück gesetzt
                if ((decimal)dtArtikel.Rows[i]["ArtikelID"] == myArtikelID)
                {
                    dtArtikel.Rows[i]["Selected"] = false;
                }
            }
        }
        ///<summary>ctrUmbuchung/CopyDataRowdtAArtikel</summary>
        ///<remarks>Copiert den gewählten Datensatz in die Ausgangstable und löscht den entsprechenden 
        ///         Datensatz aus der Aritkeltabelle.</remarks>
        private void CopyDataRowdtoAArtikel()
        {
            //DataSource trennen, da sonst der Filter nicht richtig funktioniert
            dgv.DataSource = null;
            dgvAArtikel.DataSource = null;
            //Selektierten Artikel herausfiltern
            dtArtikel.DefaultView.RowFilter = String.Empty;
            dtArtikel.DefaultView.RowFilter = "Selected=True";

            DataTable dtTmpArtikel = dtArtikel.DefaultView.ToTable();

            for (Int32 i = 0; i <= dtTmpArtikel.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                string strTmp = dtTmpArtikel.Rows[i]["ArtikelID"].ToString();
                decimal.TryParse(strTmp, out decTmp);
                dtArtikelUmbuchung.DefaultView.RowFilter = "ArtikelID=" + decTmp.ToString();
                DataTable dtCheck = dtArtikelUmbuchung.DefaultView.ToTable();
                if (dtCheck.Rows.Count < 1)
                {
                    dtTmpArtikel.Rows[i]["Selected"] = false;
                    dtArtikelUmbuchung.ImportRow(dtTmpArtikel.Rows[i]);
                }
            }
            //Nicht selektierten Artikel herausfiltern
            dtArtikel.DefaultView.RowFilter = String.Empty;
            dtArtikel.DefaultView.RowFilter = "Selected=False";
            //Leeren der TmpTable
            dtTmpArtikel.Clear();
            //Nicht selektierten Artikel in den TmpTable
            dtTmpArtikel = dtArtikel.DefaultView.ToTable();
            //ArtikelTable leeren, für die nicht selektierten Artikel
            dtArtikel.Clear();
            //nicht selektierten Aritkel in den ArtikelTable
            dtArtikel = dtTmpArtikel.DefaultView.ToTable();
            DGVConnect();

            //UB-Grid wieder verbinden
            DGVAArtikelConnect();
        }
        ///<summary>ctrUmbuchung / CopySelectedARtikelAusLEingangInUBList</summary>
        ///<remarks>Copiert den im Eingang gewählten Artikel direkt in die UB-Liste</remarks>
        private void CopySelectedARtikelAusLEingangInUBList()
        {
            if (_ArtikelIDTakeOver > 0)
            {
                for (Int32 i = 0; i <= dtArtikel.Rows.Count - 1; i++)
                {
                    if (_ArtikelIDTakeOver == (decimal)dtArtikel.Rows[i]["ArtikelID"])
                    {
                        dtArtikelUmbuchung.ImportRow(dtArtikel.Rows[i]);
                        dtArtikel.Rows[i]["Selected"] = true;
                        //ClearSearch();
                        break;
                    }
                }
            }
        }
        /////<summary>ctrUmbuchung / tstbSearchArtikel_TextChanged</summary>
        /////<remarks></remarks>
        //private void tstbSearchArtikel_TextChanged(object sender, EventArgs e)
        //{
        //    string strFilter = string.Empty;
        //    //strFilter = Functions.GetSearchFilterString(ref tscbSearch, tscbSearch.Text, tstbSearchArtikel.Text);
        //    dtArtikel.DefaultView.RowFilter = strFilter;
        //}
        ///<summary>ctrUmbuchung / tsbtnShowAll_Click</summary>
        ///<remarks></remarks>
        private void tsbtnShowAll_Click(object sender, EventArgs e)
        {
            InitDGVBestand();
            dtArtikelUmbuchung.Clear();
        }
        ///<summary>ctrUmbuchung / tsbtnShowAll_Click</summary>
        ///<remarks>Start der Umbuchung</remarks>
        private void tsbtnEinlagerungSpeichern_Click(object sender, EventArgs e)
        {
            if (dtArtikelUmbuchung != null)
            {
                if (dtArtikelUmbuchung.Rows.Count > 0)
                {
                    //Quell und Zielbestand müssen unterschiedlich sein
                    if (Umbuchung.AuftraggeberAltID != Umbuchung.AuftraggeberNeuID)
                    {
                        Umbuchung.System = this._ctrMenu._frmMain.system;
                        decimal decTmp = 0;
                        decimal.TryParse(tbNettoGesamt.Text, out decTmp);
                        Umbuchung.NettoGesamt = decTmp;
                        decTmp = 0;
                        decimal.TryParse(tbBruttoGesamt.Text, out decTmp);
                        Umbuchung.BruttoGesamt = decTmp;
                        Umbuchung.UBDate = dtpEinlagerungDate.Value;
                        Umbuchung.dtUmbuchung = dtArtikelUmbuchung;

                        //Kostenzuweisung in Tabelle speichern
                        foreach (DataRow row in Umbuchung.dtUmbuchung.Rows)
                        {
                            row["UB_AltCalcEinlagerung"] = cbAltBestandEinlagerung.Checked;
                            row["UB_AltCalcAuslagerung"] = cbAltBestandAuslagerung.Checked;
                            row["UB_AltCalcLagergeld"] = cbAltBestandLagergeld.Checked;
                            row["UB_NeuCalcEinlagerung"] = cbNeuBestandEinlagerung.Checked;
                            row["UB_NeuCalcAuslagerung"] = cbNeuBestandAuslagerung.Checked;
                            row["UB_NeuCalcLagergeld"] = cbNeuBestandLagergeld.Checked;
                        }
                        //Start der Umbuchung
                        if (Umbuchung.DoUmbuchung())
                        {
                            AsnTransfer = new clsASNTransfer();
                            if (AsnTransfer.DoASNTransfer(this.GL_System, this.Umbuchung.LAusgang.AbBereichID, this.Umbuchung.LAusgang.MandantenID))
                            {
                                clsLager Lager = new LVS.clsLager();
                                Lager.InitClass(this.GL_User, this.GL_System, this._ctrMenu._frmMain.system);
                                Lager.Artikel.listArt.Clear();
                                clsArtikel tmpArt = new clsArtikel();

                                for (int x = 0; x <= this.Umbuchung.dtUmbuchung.Rows.Count - 1; x++)
                                {
                                    int iTmp = 0;
                                    string strVal = this.Umbuchung.dtUmbuchung.Rows[x]["ArtikelID"].ToString();
                                    int.TryParse(strVal, out iTmp);
                                    if (iTmp > 0)
                                    {
                                        tmpArt = new clsArtikel();
                                        tmpArt.InitClass(this.GL_User, this.GL_System);
                                        tmpArt.ID = iTmp;
                                        tmpArt.GetArtikeldatenByTableID();

                                        if (Lager.Artikel.listArt.Count == 0)
                                        {
                                            Lager.Artikel = tmpArt.Copy();
                                            Lager.Artikel.listArt.Clear();
                                        }
                                        if (!Lager.Artikel.listArt.Contains(tmpArt))
                                        {
                                            Lager.Artikel.listArt.Add(tmpArt);
                                        }
                                    }
                                }
                                Lager.ASNAction.ASNActionProcessNr = clsASNAction.const_ASNAction_Umbuchung;

                                //Umbuchung es werden nur Ausgangmeldungen ertellt.
                                if (this._ctrMenu._frmMain.system.Client.Modul.ASN_UserOldASNFileCreation)
                                {
                                    AsnTransfer.CreateLM(ref Lager);
                                }
                                else
                                {
                                    AsnTransfer.CreateLM_Ausgang(ref Lager);
                                }
                            }
                        }
                        ClearFrm();
                        InitCtr();
                    }
                    else
                    {
                        clsMessages.Lager_IdentischeBestandsbesitzer();
                    }
                }
                else
                {
                    clsMessages.Lager_KeineArtikelFuerUB();
                }
            }
        }
        ///<summary>ctrUmbuchung / tsbtnClose_Click</summary>
        ///<remarks></remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            CloseCtr();
        }
        ///<summary>ctrUmbuchung / CloseCtr</summary>
        ///<remarks>Schliesst das CTR</remarks>
        private void CloseCtr()
        {
            if (this._frmTmp != null)
            {
                this._frmTmp.CloseFrmTmp();
            }
            else
            {
                this._ctrMenu.CloseCtrUmbuchung();
            }
        }
        ///<summary>ctrUmbuchung / tbBruttoGesamt_Validated</summary>
        ///<remarks></remarks>
        private void tbBruttoGesamt_Validated(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            if (!Decimal.TryParse(tbBruttoGesamt.Text, out decTmp))
            {
                clsMessages.Allgemein_EingabeFormatFehlerhaft();
            }
            if (decTmp < 0)
            {
                decTmp = decTmp * (-1);
            }
            tbBruttoGesamt.Text = Functions.FormatDecimal(decTmp);
        }
        ///<summary>ctrUmbuchung / tbEArtAnzahl_Validated</summary>
        ///<remarks></remarks>
        private void tbEArtAnzahl_Validated(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            if (!Decimal.TryParse(tbEArtAnzahl.Text, out decTmp))
            {
                clsMessages.Allgemein_EingabeFormatFehlerhaft();
            }
            if (decTmp < 0)
            {
                decTmp = decTmp * (-1);
            }
            tbEArtAnzahl.Text = Functions.FormatDecimal(decTmp);
        }
        ///<summary>ctrUmbuchung / tbNettoGesamt_Validated</summary>
        ///<remarks></remarks>
        private void tbNettoGesamt_Validated(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            if (!Decimal.TryParse(tbNettoGesamt.Text, out decTmp))
            {
                clsMessages.Allgemein_EingabeFormatFehlerhaft();
            }
            if (decTmp < 0)
            {
                decTmp = decTmp * (-1);
            }
            tbNettoGesamt.Text = Functions.FormatDecimal(decTmp);
        }
        ///<summary>ctrUmbuchung / tbNettoGesamt_Validated</summary>
        ///<remarks></remarks>
        private void tsbtnSearch_Click(object sender, EventArgs e)
        {
            OpenSearch();
        }
        ///<summary>ctrUmbuchung / OpenSearch</summary>
        ///<remarks></remarks>
        private void OpenSearch()
        {
            this._ctrMenu.OpenCtrSearch(this);
        }
        ///<summary>ctrUmbuchung / dgv_CellClick</summary>
        ///<remarks></remarks>
        private void dgv_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.Value != null)
            {
                if (e.RowIndex > -1)
                {


                    if (this.dgv.Columns[e.ColumnIndex].Name.Equals("Selected"))
                    {
                        if ((bool)this.dgv.Rows[e.RowIndex].Cells["Selected"].Value == true)
                        {
                            this.dgv.Rows[e.RowIndex].Cells["Selected"].Value = false;
                        }
                        else
                        {
                            int iBRows = (int)dtArtikel.Compute("COUNT(Selected)", "Selected=true");
                            int iUBRows = dtArtikelUmbuchung.Rows.Count;
                            int iSelRows = iBRows + iUBRows;

                            if (
                                (this._ctrMenu._frmMain.system.AbBereich.ArtMaxCountInAusgang > 0) &&
                                (iSelRows >= this._ctrMenu._frmMain.system.AbBereich.ArtMaxCountInAusgang)
                               )
                            {
                                string strMes = "Das Limit der zulässigen Artikelanzahl in Ausgängen ist erreicht!" + Environment.NewLine;
                                strMes += "[" + iSelRows + " von " + this._ctrMenu._frmMain.system.AbBereich.ArtMaxCountInAusgang.ToString() + "] Artikeln ist selektiert!";
                                clsMessages.Allgemein_ERRORTextShow(strMes);
                            }
                            else
                            {
                                this.dgv.Rows[e.RowIndex].Cells["Selected"].Value = true;
                            }
                        }
                    }
                }
            }
        }
        ///<summary>ctrUmbuchung / tsbtnUBAll_Click</summary>
        ///<remarks>Der komplette Bestand soll umgebucht werden</remarks>
        private void tsbtnUBAll_Click(object sender, EventArgs e)
        {
            SetAllSelectedOrUnselected(ref dtArtikel, true);
            CopyDataRowdtoAArtikel();
            SumArtikelUmbuchung();
        }
        ///<summary>ctrUmbuchung / tsbtnSelectedUB_Click</summary>
        ///<remarks>Die markierten Artikel sollen umgebucht werden</remarks>
        private void tsbtnSelectedUB_Click(object sender, EventArgs e)
        {
            CopyDataRowdtoAArtikel();
            SumArtikelUmbuchung();
        }
        ///<summary>ctrUmbuchung / toolStripButton1_Click</summary>
        ///<remarks></remarks>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            RemoveRowFromArtUmbuchung();
            SumArtikelUmbuchung();
        }
        ///<summary>ctrUmbuchung / tsbtnAllBack_Click</summary>
        ///<remarks></remarks>
        private void tsbtnAllBack_Click(object sender, EventArgs e)
        {
            SetAllSelectedOrUnselected(ref dtArtikelUmbuchung, true);
            RemoveRowFromArtUmbuchung();
            SumArtikelUmbuchung();
        }
        ///<summary>ctrUmbuchung / SetAllSelectedOrUnselected</summary>
        ///<remarks>Setzt die Spalte Selected der Tabelle auf True oder False</remarks>
        private void SetAllSelectedOrUnselected(ref DataTable dt, bool bSelected)
        {
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                dt.Rows[i]["Selected"] = bSelected;
            }
        }
        ///<summary>ctrUmbuchung / dgvAArtikel_CellClick</summary>
        ///<remarks></remarks>
        private void dgvAArtikel_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.ColumnIndex > -1)
            {
                if (this.dgvAArtikel.Columns[e.ColumnIndex].Name.Equals("Selected"))
                {
                    if (e.RowIndex > -1)
                    {
                        if ((bool)this.dgvAArtikel.Rows[e.RowIndex].Cells["Selected"].Value == true)
                        {
                            this.dgvAArtikel.Rows[e.RowIndex].Cells["Selected"].Value = false;
                        }
                        else
                        {
                            this.dgvAArtikel.Rows[e.RowIndex].Cells["Selected"].Value = true;
                        }
                    }
                }
            }
        }
        ///<summary>ctrUmbuchung / tscbView_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void tscbView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tscbView.SelectedIndex > -1)
            {
                Functions.setView(ref dtArtikel, ref dgv, viewName, tscbView.SelectedItem.ToString(), this._ctrMenu._frmMain.GL_System, true, dts);
                SetColumns(ref this.dgv);
                this.dgv.BestFitColumns();
                this._ctrArtSearchFilter.SetFilterforDGV(ref this.dgv, false);
                Functions.FillSearchColumnFromDGV(ref this.dgv, ref this.tscbSearch, this._ctrMenu._frmMain.system);
            }
        }
        ///<summary>ctrUmbuchung / dgv_ToolTipTextNeeded</summary>
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
        ///<summary>ctrUmbuchung / tstbSearchArtikel_TextChanged</summary>
        ///<remarks></remarks>
        private void tstbSearchArtikel_TextChanged(object sender, EventArgs e)
        {
            string strFilter = string.Empty;
            strFilter = Functions.GetSearchFilterString(ref tscbSearch, tscbSearch.Text, tstbSearchArtikel.Text);
            dtArtikel.DefaultView.RowFilter = strFilter;
        }
    }
}
