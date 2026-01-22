using Common.Enumerations;
using LVS;
using LVS.Constants;
using Sped4.Classes;
using Sped4.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Localization;

namespace Sped4
{
    public partial class ctrAuslagerung : UserControl
    {
        public const string const_AusgangArt_Direktanlieferung = "DIREKTANLIEFERUNG";
        public const string const_AusgangArt_Ruecklieferung = "RÜCKLIEFERUNG";
        public const string const_Fremdfahrzeug = "--Fremdfahrzeug--";
        public const string const_Waggon = "--Bahn/Waggon--";
        public const Int32 const_StatusSearch_ArtikelSuche = 0;
        public const Int32 const_StatusSearch_ArtikelAuslagerung = 1;


        internal clsLager Lager = new clsLager();
        public frmTmp _frmTmp;
        internal Globals._GL_USER GL_User;
        internal Globals._GL_SYSTEM GL_System;
        internal ctrADRManAdd _ctrADRManAdd;
        internal ctrArtSearchFilter _ctrArtSearchFilter;
        internal clsASNTransfer AsnTransfer;
        public ctrMenu _ctrMenu;
        public decimal _KD_ADR_ID = 0;
        public bool _Update = false;
        public Int32 _ListenArt = 0;
        public decimal _LVS_ID = 0;
        public decimal _LEingangID = 0;
        public decimal _LAusgangID = 0;
        public decimal _MandantenID = 0;
        public string _MandantenName = string.Empty;
        public decimal _LAusgangTableID = 0;
        internal bool _bLAusgangIsCHecked;
        public Int32 _iSearchButton = 0;
        public DateTime ADatum = DateTime.Today.Date;
        Int32 iSelectedMenge = 0;
        public Int32 iStatusSearch = 0;
        internal bool bIsNew = false;
        public string BackToCtr = string.Empty;
        internal delegate void ThreadCtrInvokeEventHandler();
        BackgroundWorker workerConfirm;
        BackgroundWorker workerDGVBestand;
        BackgroundWorker workerPrint;

        public DataTable dtAuftrag = new DataTable("Auftrag");  //eingelesene daten Einlagerung
        public DataTable dtArtikel = new DataTable("Artikel");  //Artikel Einlagerung
        public DataTable dtAuslagerung = new DataTable("Auslagerung"); //AUftragsdaten Auslagerung
        public DataTable dtArtikelAuslagerung = new DataTable("Artikel"); //Artikel Auslagerung DataSource dgvAuslagerung
        public DataTable dtSearch = new DataTable();

        public DataTable dtMandanten;

        internal decimal _ADRAuftraggeber = 0;
        internal decimal _ADRVersender = 0;
        internal decimal _ADREmpfänger = 0;
        internal decimal _ADREntladestelle = 0;
        internal decimal _ADRSpedition = 0;
        internal string _SearchTXT = string.Empty;
        internal string _SearchCriteron = string.Empty;
        internal string manAdrArt = string.Empty;
        internal string _ADRSearch = string.Empty;

        internal bool isPRinting = false;
        internal bool _bAusgangAktive = false;
        /**********************************
         * 1: Kunde / Auftraggeber
         * 2: Versand
         * 3: Empfänger oder Entladestelle
         * ********************************/

        internal clsLager _lager = new clsLager();
        private bool _bFromBestand;
        public bool bFromBestand
        {
            get
            {
                return _bFromBestand;
            }
            set
            {
                _bFromBestand = value;
                this.tsbtnBackToBestand.Visible = value;
            }
        }
        /************************************************************
         *              Procedure / Methoden
         * **********************************************************/
        ///<summary>ctrAuslagerung</summary>
        ///<remarks></remarks>
        public ctrAuslagerung()
        {
            InitializeComponent();

            workerConfirm = new BackgroundWorker();
            workerConfirm.WorkerReportsProgress = true;
            workerConfirm.WorkerSupportsCancellation = true;
            workerConfirm.DoWork += new DoWorkEventHandler(workerConfirm_DoWork);

            workerPrint = new BackgroundWorker();
            workerPrint.WorkerReportsProgress = true;
            workerPrint.WorkerSupportsCancellation = true;
            workerPrint.DoWork += new DoWorkEventHandler(workerPrint_DoWork);

            workerDGVBestand = new BackgroundWorker();
            workerDGVBestand.WorkerReportsProgress = true;
            workerDGVBestand.WorkerSupportsCancellation = true;
            workerDGVBestand.DoWork += new DoWorkEventHandler(workerDGVBestand_DoWork);
            workerDGVBestand.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerDGVBestand_Finsished);

            //Tag für die Button setzen
            this.btnManEmpfaenger.Tag = 3;
            this.btnManEntladestelle.Tag = 4;
            this.btnManSped.Tag = 5;
        }
        ///<summary>ctrAuslagerung / ctrAuslagerung_Load</summary>
        ///<remarks></remarks>
        private void ctrAuslagerung_Load(object sender, EventArgs e)
        {
            SetComTECSettings();
            CustomerSettings();
        }
        ///<summary>ctrAuslagerung / workerDGVBestand_Finsished</summary>
        ///<remarks>Anzeigen der Artikel im Grid nachdem der worker durchgelaufen ist</remarks>
        private void workerDGVBestand_Finsished(object sender, RunWorkerCompletedEventArgs e)
        {
            Functions.setView(ref dtArtikel, ref this.dgv, "LAusgang", tscbArtikel.SelectedItem.ToString(), GL_System, false);
            this.dgv.Enabled = !this.Lager.Ausgang.Checked;
        }
        ///<summary>ctrAuslagerung/ SetComTECSettings</summary>
        ///<remarks></remarks>
        private void SetComTECSettings()
        {
        }
        ///<summary>ctrAuslagerung / CustomerSettings</summary>
        ///<remarks>Hier kann den Kundenwünschen entsprechend ein / mehere Elemente 
        ///         ein-/ausgeblendet werden</remarks>
        private void CustomerSettings()
        {
            //Adressdaten Eingabefelder
            clsClient.ctrAuslagerung_CustomizeAusgangAdrDatenInputFieldsEnabled(this._ctrMenu._frmMain.system.Client.MatchCode, ref this.btnEmpfänger, ref this.btnManEmpfaenger, ref this.tbMCEmpfänger);
            clsClient.ctrAuslagerung_CustomizeAusgangAdrDatenInputFieldsEnabled(this._ctrMenu._frmMain.system.Client.MatchCode, ref this.btnEntladestelle, ref this.btnManEntladestelle, ref this.tbMCEntladestelle);

            //btnChangeAusgang ein-/ausbelden
            //this.tsbtnChangeAusgang.Visible = this._ctrMenu._frmMain.system.Client.Modul.Lager_Auslagerung_EditAfterClose && this._ctrMenu._frmMain.system.AbBereich.ASNTransfer;
            //- mr 2024_11_12
            if (this._ctrMenu._frmMain.system.AbBereich.ASNTransfer)
            {
                this.tsbtnChangeAusgang.Visible = false;
            }
            else
            {
                this.tsbtnChangeAusgang.Visible = this._ctrMenu._frmMain.system.Client.Modul.Lager_Auslagerung_EditAfterClose;
            }

            //tsbtnBestandFreeForCall => Blendet die Liste/Ordern ein, wenn FreeForCall aktiviert ist
            tsbtnBestandFreeForCall.Visible = this._ctrMenu._frmMain.system.Client.Modul.Lager_FreeForCall;

            //----CHeck Complete
            this.tsbtnCheckComplete.Visible = this._ctrMenu._frmMain.system.Client.Modul.Lager_Auslagerung_CheckComplete;

            //Erweiterete Suche 
            this.tsbtnSearchShow.Visible = this._ctrMenu._frmMain.system.Client.Modul.EnableAdvancedSearch;

            //DirectSearch
            this.tscbSearch.Visible = this._ctrMenu._frmMain.system.Client.Modul.EnableDirectSearch;
            this.tstbSearchArtikel.Visible = this._ctrMenu._frmMain.system.Client.Modul.EnableDirectSearch;
            this.tslSearchtext.Visible = this._ctrMenu._frmMain.system.Client.Modul.EnableDirectSearch;

            //-- Suche mit direkter Auslagerung und Abschluss
            this.tsbtnStoreOutDirect.Visible = this._ctrMenu._frmMain.system.Client.Modul.Lager_Auslagerung_StoreOutDirect;

            //--- Call Status
            this.tsbtnAbrufSetStatusAll.Visible = this._ctrMenu._frmMain.system.Client.Modul.ASNCall_UserCallStatus;

            //---SLE Eingabefelder Trailer ausblenden            
            this._ctrMenu._frmMain.system.Client.ctrAuslagerung_CustomizeTrailerDataInput(ref this.cbTrailer, ref this.mtbKFZTrailer, ref this.lTrailerCombo, ref lTrailerTxtBox);
        }
        ///<summary>ctrAuslagerung / InitCtrAuslagerung</summary>
        ///<remarks>Folgende Funktionen werden ausgeführt:
        ///         - ComboMandanten füllen
        ///         - Ermittel des letzten Lagerausgangs
        ///         - Daten des Lagerausgangs auf die Form
        ///         - Laden der entsprechenden Artikeldaten</remarks>
        public void InitCtrAuslagerung()
        {
            this.GL_User = this._ctrMenu._frmMain.GL_User;
            this.GL_System = this._ctrMenu._frmMain.GL_System;
            _MandantenID = this.GL_System.sys_MandantenID;
            InitFilterSearchCtr();
            RadGridLocalizationProvider.CurrentProvider = new clsGermanRadGridLocalizationProvider();

            _bLAusgangIsCHecked = false;
            SetLabelDirektanlieferung();

            //combo Mandanten füllen
            //dtMandanten = new DataTable("Mandanten");
            //Functions.InitComboMandanten(GL_User, ref tscbMandanten, ref dtMandanten, true);
            //Combobox für Fahrzeug füllen
            Functions.InitComboFahrzeuge(this.GL_User, ref cbFahrzeug);
            Functions.InitComboTrailer(this.GL_User, ref cbTrailer);

            //Klasse füllen
            Lager.LEingangTableID = 0;
            Lager.LAusgangTableID = 0;
            Lager._GL_User = this.GL_User;
            Lager.AbBereichID = this.GL_System.sys_ArbeitsbereichID;
            Lager.MandantenID = _MandantenID;
            Lager.FillLagerDaten(true);
            Functions.InitComboViews(_ctrMenu._frmMain.GL_System, ref tscbArtikel, "LAusgang");
            Functions.InitComboViews(_ctrMenu._frmMain.GL_System, ref tscbAArtikel, "LAusgang");
            InitLoad();
            //hier einmal laden, damit die Spalten des Grids angezeigt werden
            InitDGV();
        }
        ///<summary>ctrAuslagerung / InitLoad</summary>
        ///<remarks>Schliessen der Form.</remarks>
        private void InitLoad()
        {
            //Leere der Felder
            ClearLAusgangEingabefelder();
            //letzten LAusgang laden
            GetNextLAusgang(false);
            SetLAusgangsdatenToFrm();
            //Menü freigeben
            SetMenuLAusgangEnabled((tbAusgangsnummer.Text != string.Empty));
            // Fremdfahrzeug Eingabe deaktiveren 
            bool bEnable = false;
            if (cbFahrzeug.SelectedIndex == 1)
                bEnable = true;
            SetFelderFremdfahrzeugeEnabled(bEnable);
        }
        ///<summary>ctrAuslagerung / completeAusgang</summary>
        ///<remarks>Anbschließen der Auslagerung</remarks>
        private void completeAusgang()
        {
            //Code aus dem Background worker ....          
            //Durch die Prüfung wird eine nochmaliger Abschluss und somit 
            //der Eintrag in die ArtikelVita verhindert
            //if (!Lager.Ausgang.Checked)
            //{
            //    //LEingang entsprechend updaten
            //    clsLager.UpdateLAusgangSetAusgangAbgeschlossen(ref this.Lager, true);
            //    //Ausgangsmeldung erstellen
            //    AsnTransfer = new clsASNTransfer();
            //    if (AsnTransfer.DoASNTransfer(this.GL_System, this.Lager.Ausgang.AbBereichID, this.Lager.Ausgang.MandantenID))
            //    {
            //        AsnTransfer.CreateLM_Ausgang(ref this.Lager);
            //    }
            //}


            //Artikel als geprüft markieren
            bool bAllCheckedFromBeginning = true;
            for (Int32 i = 0; i <= dtArtikelAuslagerung.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                string strTmp = dtArtikelAuslagerung.Rows[i]["ArtikelID"].ToString();
                decimal.TryParse(strTmp, out decTmp);
                if (decTmp > 0)
                {
                    if (bool.Parse(dtArtikelAuslagerung.Rows[i]["Check"].ToString()) == false)
                    {
                        bAllCheckedFromBeginning = false;
                    }
                    clsLager.UpdateLAusgangArtikelChecked(this.GL_User, decTmp, true);
                }
            }
            //--- Update Ausgang Checked=true wird gesetzt
            //--- Image für Ausgang abgeschlossen wird gesetzt
            //--- ASN Auftrag wird in Queue gespeichert
            SetCheckAusgang();


            // Druck innerhalb des workers anstoßen

            //workerConfirm.RunWorkerAsync();
            // CF Druck aus Worker genommen
            //if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Auslagerung_Print_DirectAusgangDoc)
            //{
            //    if (!this.Lager.Ausgang.IsPrintDoc)
            //    {
            //        DirectPrintLAusgangDoc();
            //    }
            //    if (!this.Lager.Ausgang.IsPrintLfs)
            //    {
            //        DirectPrintLAusgangLfs();
            //    }
            //}
            //if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Auslagerung_Print_DirectAusgangListe)
            //{
            //    DirectPrintAusgangsliste();
            //}
            //Grid Artikel leeren
            //dtArtikel.Rows.Clear();
            ////Leere der Felder
            //ClearLAusgangEingabefelder();
            //Lager.Ausgang.FillAusgang();
            //SetLAusgangsdatenToFrm();
            //Menü freigeben

            //Baustelle 17.12.2015
            //this.dgvAArtikel.Enabled = false;
            //this.dgv.Enabled = false;
            SetMenuLAusgangEnabled((tbAusgangsnummer.Text != string.Empty));
            InitDGVAArtikel(true);
        }
        ///<summary>ctrAuslagerung / worker_DoWork</summary>
        ///<remarks></remarks>
        void workerConfirm_DoWork(object sender, DoWorkEventArgs e)
        {
            //Print Doc directly
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Auslagerung_Print_DirectAusgangDoc)
            {
                if (!this.Lager.Ausgang.IsPrintDoc)
                {
                    DirectPrintLAusgangDoc();
                }
                if (!this.Lager.Ausgang.IsPrintList)
                {
                    DirectPrintAusgangsliste();
                }
                DirectPrintLAusgangLfs();
            }
        }
        ///<summary>ctrAuslagerung / worker_DoWork</summary>
        ///<remarks></remarks>
        void workerPrint_DoWork(object sender, DoWorkEventArgs e)
        {
            //Print Doc directly

            if (!this.Lager.Ausgang.IsPrintDoc)
            {
                //this.Lager.Ausgang.IsPrintDoc = true; // Doc muss schon vorher auf gedruckt gesetzt werden
                DirectPrintLAusgangDoc();
            }
            if (!this.Lager.Ausgang.IsPrintList)
            {
                DirectPrintAusgangsliste();
            }

        }
        ///<summary>ctrAuslagerung / workerDGV_DoWork</summary>
        ///<remarks></remarks>
        void workerDGVBestand_DoWork(object sender, DoWorkEventArgs e)
        {
            InitDGV();
        }
        ///<summary>ctrAuslagerung / InitFilterSearchCtr</summary>
        ///<remarks></remarks>
        private void InitFilterSearchCtr()
        {
            _ctrArtSearchFilter = new ctrArtSearchFilter();
            _ctrArtSearchFilter.InitCtr(this);
            _ctrArtSearchFilter.Dock = DockStyle.Fill;
            _ctrArtSearchFilter.Parent = this.splitPanel3;
            _ctrArtSearchFilter.Show();
            _ctrArtSearchFilter.BringToFront();
            this.splitPanel3.Width = _ctrArtSearchFilter.Width + 10;
        }
        ///<summary>ctrAuslagerung / SetSearchLAusgangToFrm</summary>
        ///<remarks></remarks>
        public void SetSearchLAusgangToFrm(decimal myDecArtID)
        {
            ClearLAusgangEingabefelder();
            Lager.Artikel.ID = myDecArtID;
            Lager.Artikel.GetArtikeldatenByTableID();
            Lager.LAusgangTableID = Lager.Artikel.LAusgangTableID;
            Lager.FillLagerDaten(true);
            Lager.Artikel.ID = myDecArtID;
            SetLAusgangsdatenToFrm();
            SetSelectedRowInDGV();
        }
        ///<summary>ctrAuslagerung / SetSearchLAusgangToFrm</summary>
        ///<remarks></remarks>
        public void SetSearchLAusgangToFrmEA(decimal myDecArtID)
        {
            //Lager.Ausgang.LAusgangTableID = clsLAusgang.GetLAusgangTableIDByLAusgangID(this.GL_User.User_ID, myDecArtID, this.GL_System.sys_MandantenID);
            this._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
            Lager.Ausgang.LAusgangTableID = clsLAusgang.GetLAusgangTableIDByLAusgangID(this.GL_User.User_ID, myDecArtID, this._ctrMenu._frmMain.system);
            Lager.Ausgang.FillAusgang();
            ClearLAusgangEingabefelder();
            SetLAusgangsdatenToFrm();
        }
        ///<summary>ctrAuslagerung / SetSelectedRowInDGV</summary>
        ///<remarks>Die Datenrow mit der im CTR angezeigten Artikelid wird im Grid markiert.</remarks> 
        private void SetSelectedRowInDGV()
        {
            if (this.dgvAArtikel.Rows.Count > 0)
            {
                bool bRowSelected = false;
                for (Int32 i = 0; i <= this.dgvAArtikel.Rows.Count - 1; i++)
                {
                    decimal decTmp = 0;
                    string strTmp = string.Empty;
                    strTmp = this.dgvAArtikel.Rows[i].Cells["ArtikelID"].Value.ToString();
                    Decimal.TryParse(strTmp, out decTmp);
                    if (decTmp == this.Lager.Artikel.ID)
                    {
                        this.dgvAArtikel.Rows[i].IsSelected = true;
                        this.dgvAArtikel.Rows[i].IsCurrent = true;
                        bRowSelected = true;
                    }
                    else
                    {
                        //wenn das gesamte Grid durchlaufen wurde und noch keine 
                        //Übereinstimmung gefunden wurde, dann beim letzten Durchlauf
                        //die Selected Row auf Row[0] setzen
                        if ((i == this.dgvAArtikel.Rows.Count - 1) && (!bRowSelected))
                        {
                            this.dgvAArtikel.Rows[0].IsSelected = true;
                            this.dgvAArtikel.Rows[0].IsCurrent = true;
                        }
                    }
                }
            }
        }

        ///<summary>ctrAuslagerung / tscbMandanten_SelectedIndexChanged</summary>
        ///<remarks>Auswahl des Mandanten.</remarks>
        private void tscbMandanten_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Functions.SetMandantenDaten(ref this.tscbMandanten, ref this._MandantenID, ref this._MandantenName);
            Lager.MandantenID = _MandantenID;
            //Mandant wurde geändert, so muss LEingang- und LAusgangsID =0 gesetzt werden
            Lager.LEingangTableID = 0;
            Lager.LAusgangTableID = 0;
            Lager.FillLagerDaten(true);
            InitLoad();
        }
        ///<summary>ctrAuslagerung / tsbtnClose_Click</summary>
        ///<remarks>Schliessen der Form.</remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            clsLAusgang.unlockAusgang(this.GL_User.User_ID);
            if (this._frmTmp != null)
            {
                this._frmTmp.CloseFrmTmp();
            }
            else
            {
                if (_bFromBestand == true)
                {
                    this._ctrMenu.CloseCtrBestand();
                }
                this._ctrMenu.CloseCtrAuslagerung();
            }
        }
        ///<summary>ctrAuslagerung / GetNextLAusgang</summary>
        ///<remarks>Blättern 
        ///         - true = zurück
        ///         - false = vorwärts</remarks>
        private void GetNextLAusgang(bool myDirection)
        {
            if (_MandantenID > 0)
            {
                Lager.Ausgang._GL_User = this.GL_User;
                //Lager.Ausgang.AbBereichID = this.GL_User.sys_ArbeitsbereichID;
                Lager.Ausgang.AbBereichID = this.GL_System.sys_ArbeitsbereichID;
                Lager.Ausgang.GetNextLAusgangsID(myDirection);
                Lager.LAusgangTableID = Lager.Ausgang.LAusgangTableID;
                Lager.FillLagerDaten(true);
            }
        }
        ///<summary>ctrAuslagerung / tscbMandanten_SelectedIndexChanged</summary>
        ///<remarks>Auswahl des Mandanten.</remarks>
        public void SetLAusgangsdatenToFrm()
        {
            if (_MandantenID > 0)
            {
                this.lbLocked.Visible = false;
                this.lbLockedBy.Visible = false;
                _bAusgangAktive = false;
                dtArtikelAuslagerung.Rows.Clear();
                isPRinting = false;

                if (Lager.Ausgang.ExistLAusgangTableID())
                {
                    bIsNew = false;
                    this.Lager.Ausgang.AdrManuell = new clsADRMan();
                    this.Lager.Ausgang.AdrManuell.InitClass(this.GL_User, this.Lager.Ausgang.LAusgangTableID, "LAusgang");

                    _bLAusgangIsCHecked = Lager.Ausgang.Checked;
                    //LAgerausgang abgeschlossen
                    if (Lager.Ausgang.Checked)
                    {
                        pbCheckAusgang.Image = (Image)Sped4.Properties.Resources.check;
                    }
                    else
                    {
                        pbCheckAusgang.Image = (Image)Sped4.Properties.Resources.warning.ToBitmap();
                    }
                    //Lagerausgangkopfdaten
                    dtpAusgangDate.Value = Lager.Ausgang.LAusgangsDate;
                    SetDTPAusgangDateMinDate();

                    tbLAusgangID.Text = Lager.Ausgang.LAusgangTableID.ToString();
                    tbAusgangsnummer.Text = Lager.Ausgang.LAusgangID.ToString();
                    tbExTransportRef.Text = Lager.Ausgang.exTransportRef.ToString();
                    tbLieferantenNr.Text = Lager.Ausgang.Lieferant;
                    tbSLB.Text = Lager.Ausgang.SLB.ToString();
                    tbFahrer.Text = Lager.Ausgang.Fahrer;
                    //Ausgangskopfdaten aktivieren
                    SetLAusgangsKopfDatenEnabled(!Lager.Ausgang.Checked);

                    tstbCatLabel.Text = "Bestand komplett";
                    //ADRESSEN setzen
                    //Auftraggeber
                    if (Lager.Ausgang.Auftraggeber > 0)
                    {
                        _iSearchButton = 1;
                        SetADRByID(Lager.Ausgang.Auftraggeber);
                    }
                    else
                    {
                        tbMCAuftraggeber.Text = string.Empty;
                        tbADRAuftraggeber.Text = string.Empty;
                        try
                        {
                            clsADRMan tmpADRMan = new clsADRMan();
                            tmpADRMan.InitClass(this.GL_User, this.Lager.Ausgang.LAusgangTableID, "LAusgang");
                            tmpADRMan.AdrArtID = clsADRMan.cont_AdrArtID_Auftraggeber;
                            if (tmpADRMan.CheckManADRForAdrArt())
                            {
                                tmpADRMan.DictManuellADRAuslagerung.TryGetValue(clsADRMan.cont_AdrArtID_Auftraggeber, out tmpADRMan);
                                if (tmpADRMan != null)
                                {
                                    this.Lager.Ausgang.AdrManuell = tmpADRMan;
                                    tbADRAuftraggeber.Text = this.Lager.Ausgang.AdrManuell.AdrString;
                                }
                            }
                        }
                        catch (Exception ex)
                        { string mes = ex.ToString(); }
                    }
                    //Empfänger
                    if (Lager.Ausgang.Empfaenger > 0)
                    {
                        _iSearchButton = 3;
                        SetADRByID(Lager.Ausgang.Empfaenger);
                    }
                    else
                    {
                        tbMCEmpfänger.Text = string.Empty;
                        tbADREmpfänger.Text = string.Empty;
                        try
                        {
                            clsADRMan tmpADRMan = new clsADRMan();
                            tmpADRMan.InitClass(this.GL_User, this.Lager.Ausgang.LAusgangTableID, "LAusgang");
                            tmpADRMan.AdrArtID = clsADRMan.cont_AdrArtID_Empfaenger;
                            if (tmpADRMan.CheckManADRForAdrArt())
                            {
                                tmpADRMan.DictManuellADRAuslagerung.TryGetValue(clsADRMan.cont_AdrArtID_Empfaenger, out tmpADRMan);
                                if (tmpADRMan != null)
                                {
                                    this.Lager.Ausgang.AdrManuell = tmpADRMan;
                                    tbADREmpfänger.Text = this.Lager.Ausgang.AdrManuell.AdrString;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            string mes = ex.ToString();
                        }
                    }
                    //Entladestelle
                    if (Lager.Ausgang.Entladestelle > 0)
                    {
                        //_ADR_ID_E = Lager.Eingang.EntladeID;
                        //_iSearchButton = 12;
                        _iSearchButton = 4;
                        SetADRByID(Lager.Ausgang.Entladestelle);
                    }
                    else
                    {
                        tbMCEntladestelle.Text = string.Empty;
                        tbADREntladestelle.Text = string.Empty;
                        try
                        {
                            clsADRMan tmpADRMan = new clsADRMan();
                            tmpADRMan.InitClass(this.GL_User, this.Lager.Ausgang.LAusgangTableID, "LAusgang");
                            tmpADRMan.AdrArtID = clsADRMan.cont_AdrArtID_Entladeadresse;
                            if (tmpADRMan.CheckManADRForAdrArt())
                            {
                                tmpADRMan.DictManuellADRAuslagerung.TryGetValue(clsADRMan.cont_AdrArtID_Entladeadresse, out tmpADRMan);
                                if (tmpADRMan != null)
                                {
                                    this.Lager.Ausgang.AdrManuell = tmpADRMan;
                                    tbADREntladestelle.Text = this.Lager.Ausgang.AdrManuell.AdrString;
                                }
                            }
                        }
                        catch (Exception ex)
                        { }
                    }

                    //Spedition
                    if (Lager.Ausgang.SpedID > 0)
                    {
                        //Spedition
                        //Functions.SetComboToSelecetedItem(ref cbFahrzeug, Lager.Eingang.KFZ);
                        _iSearchButton = 5;
                        SetADRByID(Lager.Ausgang.SpedID);
                        //cbFahrzeug.SelectedIndex = -1;
                        try
                        {
                            cbFahrzeug.SelectedIndex = 1;
                            SetFelderFremdfahrzeugeEnabled(true);

                        }
                        catch (Exception ex)
                        {
                            string mes = ex.ToString();
                        }
                        mtbKFZ.Text = Lager.Ausgang.KFZ;
                        mtbKFZTrailer.Text = Lager.Ausgang.Trailer;
                        SetLabelKennzeichen(true);
                        tbMCSpedition.Enabled = true;
                        tbADRSpedition.Enabled = true;
                    }
                    else
                    {
                        tbMCSpedition.Text = string.Empty;
                        tbADRSpedition.Text = string.Empty;
                        tbMCSpedition.Enabled = false;
                        tbADRSpedition.Enabled = false;

                        //Bahn/Waggon
                        //Waggon ist leer
                        if (Lager.Ausgang.IsWaggon || Lager.Ausgang.WaggonNr != string.Empty)
                        {
                            if (cbFahrzeug.Items.Count > 0)
                            {
                                cbFahrzeug.SelectedIndex = 0;
                            }
                            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_WaggonNo_Mask != string.Empty)
                            {
                                string tmpWaggon = Lager.Ausgang.WaggonNr;
                                tmpWaggon = tmpWaggon.Replace(" ", "");
                                mtbKFZ.Text = tmpWaggon;

                            }
                            else
                            {
                                mtbKFZ.Text = Lager.Ausgang.WaggonNr;
                            }
                            SetLabelKennzeichen(false);
                            mtbKFZTrailer.Text = string.Empty;
                        }
                        else
                        {
                            Functions.SetComboToSelecetedItem(ref cbFahrzeug, Lager.Ausgang.KFZ);
                            //wenn selected Index>1 ==> eigene Fahrzeug
                            if (cbFahrzeug.SelectedIndex > 1)
                            {
                                //Eigenfahrzeug
                                cbFahrzeug.Text = Lager.Ausgang.KFZ;
                                mtbKFZ.Text = Lager.Ausgang.KFZ;
                                SetLabelKennzeichen(true);
                            }
                            else
                            {
                                //Fremdfahrzeug 
                                //Waggon ist leer
                                //Kfz kann leer sein
                                if (cbFahrzeug.Items.Count > 0)
                                {
                                    cbFahrzeug.SelectedIndex = 1;
                                }
                                mtbKFZ.Text = Lager.Ausgang.KFZ;
                                SetLabelKennzeichen(true);
                            }

                            Functions.SetComboToSelecetedItem(ref cbTrailer, Lager.Ausgang.Trailer);
                            //wenn selected Index>1 ==> eigene Fahrzeug
                            if (cbTrailer.SelectedIndex > 1)
                            {
                                //Eigenfahrzeug
                                cbTrailer.Text = Lager.Ausgang.Trailer;
                                mtbKFZTrailer.Text = Lager.Ausgang.Trailer;
                                //SetLabelKennzeichen(true);
                            }
                            else
                            {
                                //Fremdfahrzeug 
                                //Kfz kann leer sein
                                if (cbTrailer.Items.Count > 0)
                                {
                                    cbTrailer.SelectedIndex = 0;
                                }
                                mtbKFZTrailer.Text = Lager.Ausgang.Trailer;
                                //SetLabelKennzeichen(true);
                            }
                        }
                        tbADRSpedition.Text = string.Empty;
                        tbMCSpedition.Text = string.Empty;
                        try
                        {
                            clsADRMan tmpADRMan = new clsADRMan();
                            tmpADRMan.InitClass(this.GL_User, this.Lager.Ausgang.LAusgangTableID, "LAusgang");
                            tmpADRMan.AdrArtID = clsADRMan.cont_AdrArtID_Spedition;
                            if (tmpADRMan.CheckManADRForAdrArt())
                            {
                                tmpADRMan.DictManuellADRAuslagerung.TryGetValue(clsADRMan.cont_AdrArtID_Spedition, out tmpADRMan);
                                if (tmpADRMan != null)
                                {
                                    this.Lager.Ausgang.AdrManuell = tmpADRMan;
                                    tbADRSpedition.Text = this.Lager.Ausgang.AdrManuell.AdrString;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            string mes = ex.ToString();
                        }
                    }

                    //Info
                    tbInfo.Text = Lager.Ausgang.Info;
                    //Termin
                    if (Lager.Ausgang.Termin < clsSystem.const_DefaultDateTimeValue_Min)
                    {
                        Lager.Ausgang.Termin = clsSystem.const_DefaultDateTimeValue_Min;
                    }
                    dtpT_date.Value = dtpT_date.MinDate;
                    dtpT_date.Value = Lager.Ausgang.Termin;
                    if (Lager.Ausgang.Termin == Globals.DefaultDateTimeMinValue)
                    {
                        cbTermin.Checked = false;
                    }
                    else
                    {
                        cbTermin.Checked = true;
                    }
                    tpTerminZeit.Value = Lager.Ausgang.Termin;

                    //Direktanlieferung
                    SetLabelDirektanlieferung();
                    //LAgerTransport
                    cbLagerTransport.Checked = Lager.Ausgang.LagerTransport;
                    //Lagerausgangartikel laden

                    //InitDGVAArtikel(false);
                    //SumArtikelAusgang();
                    //Menü aktivieren
                    SetMenuLAusgangEnabled(true); // von true

                    //REsendButton
                    this.tsbtnASNReSENDTest.Enabled = this.Lager.Ausgang.Checked;
                    //ArtikelMove Button 
                    SetArtikelMoveButtonEnabled(!Lager.Ausgang.Checked);
                    SetArtikelMenuEnabled(!Lager.Ausgang.Checked);

                    //Auftraggeber soll nicht mehr geändert werden können nachdem der Ausgang gespeichert worden ist
                    SetLAusgangEingabeFelderEnabled();
                    //AusgangCheck 
                    //SetCheckAusgang();

                    InitDGVAArtikel(true);
                    SumArtikelAusgang();
                }
            }
            else
            {
                SetLAusgangsKopfDatenEnabled(false);
            }
            SetAusgangLocked();
        }
        /// <summary>
        /// ctrAuslagerung / SetLAusgangEingabeFelderEnabled
        /// </summary>
        private void SetLAusgangEingabeFelderEnabled()
        {
            if (Lager.Ausgang.ExistLAusgangTableID())
            {
                this.btnAuftraggeber.Enabled = false;
                //this.tbADRAuftraggeber.Enabled = false;
                this.tbMCAuftraggeber.Enabled = false;
            }
            else
            {
                this.btnAuftraggeber.Enabled = true;
                //this.tbADRAuftraggeber.Enabled = false;
                this.tbMCAuftraggeber.Enabled = true;
            }
        }
        ///<summary>ctrAuslagerung / tsbtnNeueAuslagerung_Click</summary>
        ///<remarks>Ein neuer Lagerausgang wird angelegt.</remarks>
        private void tsbtnNeueAuslagerung_Click(object sender, EventArgs e)
        {
            bIsNew = true;
            Lager.LAusgangTableID = 0;
            Lager.LEingangTableID = 0;
            Lager.FillLagerDaten(true);

            SetAusgangLocked(false);
            //alle Ausgangkopfdaten leeren
            ClearLAusgangEingabefelder();
            //Neue Auslagerungsnummer
            SetAuslagerungsIDToForm();
            //Ausgangskopfdaten aktivieren
            SetLAusgangsKopfDatenEnabled(true);
            //Artikel/Bestandsliste leeren
            dtArtikel.Rows.Clear();
            //Table AusgangArtikel leeren
            dtArtikelAuslagerung.Rows.Clear();
            //Gewichte und Menge aktualisieren
            SumArtikelAusgang();
            //Speicherbutton aktivieren
            tsbtnAuslagerungSpeichern.Enabled = true;
            //ArtikelMoveButton erst nach Speicher der Kopfdaten aktivieren
            SetArtikelMoveButtonEnabled(false);
            SetArtikelMenuEnabled(false);
            //Label Direktanlieferung 
            SetLabelDirektanlieferung();
            SetFelderFremdfahrzeugeEnabled(false);
            SetAusgangLocked();
        }
        ///<summary>ctrAuslagerung / SetAuslagerungsIDToForm</summary>
        ///<remarks>Ermittelt eine neue Auslagerungs-ID.</remarks>
        private void SetAuslagerungsIDToForm()
        {
            decimal decTemp = 0;
            if (_MandantenID > 0)
            {
                decTemp = clsLager.GetNewLAusgangID(GL_User, this._ctrMenu._frmMain.system);
            }
            tbAusgangsnummer.Text = decTemp.ToString();
            tbSLB.Text = tbAusgangsnummer.Text;
            dtpAusgangDate.Value = DateTime.Now;
        }
        ///<summary>ctrAuslagerung / ClearLAusgangEingabefelder</summary>
        ///<remarks>Eingabefelder im Lagerausgangskopf deaktivieren.</remarks>
        private void ClearLAusgangEingabefelder()
        {
            tbMCAuftraggeber.Text = string.Empty;
            tbMCEmpfänger.Text = string.Empty;
            tbMCEntladestelle.Text = string.Empty;
            tbFahrer.Text = string.Empty;

            tbADRAuftraggeber.Text = string.Empty;
            tbADREmpfänger.Text = string.Empty;
            tbADREntladestelle.Text = string.Empty;
            tbLAusgangID.Text = string.Empty;
            tbAusgangsnummer.Text = string.Empty;
            tbAAnzahl.Text = string.Empty;
            tbANetto.Text = string.Empty;
            tbABrutto.Text = string.Empty;
            tbInfo.Text = string.Empty;
            tbExTransportRef.Text = string.Empty;
            tbLieferantenNr.Text = string.Empty;

            _ADRAuftraggeber = 0;
            _ADREmpfänger = 0;
            _ADREntladestelle = 0;
            _ADRVersender = 0;
            _ADRSpedition = 0;
            _iSearchButton = 0;

            tbMCSpedition.Text = string.Empty;
            tbADRSpedition.Text = string.Empty;
            mtbKFZ.Text = string.Empty;
            cbFahrzeug.SelectedIndex = -1;
            //cbFahrzeug.SelectedIndex = 0;

            cbTermin.Checked = this._ctrMenu._frmMain.system.Client.Ausgang_DefaulTerminAktiv;
            dtpAusgangDate.Value = DateTime.Now;
            dtpT_date.Value = DateTime.Now;
            tpTerminZeit.Value = DateTime.Now;
            cbLagerTransport.Checked = false;

            SetFelderFremdfahrzeugeEnabled(false);

            this.dtArtikel.Rows.Clear();
            //this.dgv.Rows.Clear();
            this.dgv.DataSource = null;
            pbCheckAusgang.Image = (Image)Sped4.Properties.Resources.warning.ToBitmap();

        }
        ///<summary>ctrAuslagerung / SetEnableLAusgangsKopfDaten</summary>
        ///<remarks>Eingabefelder im Lagerausgangskopf deaktivieren.</remarks>
        private void SetLAusgangsKopfDatenEnabled(bool boEnable)
        {
            boEnable = boEnable & (Lager.Ausgang.LockedBy == 0 || Lager.Ausgang.LockedBy == this.GL_User.User_ID);
            tbLAusgangID.Enabled = boEnable;
            tbAusgangsnummer.Enabled = boEnable;
            tbAAnzahl.Enabled = boEnable;
            tbANetto.Enabled = boEnable;
            tbABrutto.Enabled = boEnable;
            tbFahrer.Enabled = boEnable;

            //ADR-Button
            btnAuftraggeber.Enabled = boEnable;
            btnEmpfänger.Enabled = boEnable;
            btnManEmpfaenger.Enabled = boEnable;
            btnEntladestelle.Enabled = boEnable;
            btnManEntladestelle.Enabled = boEnable;
            btnSpedition.Enabled = boEnable;
            btnManSped.Enabled = boEnable;

            tbMCAuftraggeber.Enabled = boEnable;
            tbMCAuftraggeber.ReadOnly = !(tbADRAuftraggeber.Text == string.Empty);
            tbMCEmpfänger.Enabled = boEnable;
            tbMCEntladestelle.Enabled = boEnable;
            tbMCSpedition.Enabled = boEnable;

            tbADRAuftraggeber.Enabled = boEnable;
            tbADREmpfänger.Enabled = boEnable;
            tbADREntladestelle.Enabled = boEnable;
            tbADRSpedition.Enabled = boEnable;

            dtpAusgangDate.Enabled = boEnable;
            dtpT_date.Enabled = boEnable;
            tpTerminZeit.Enabled = boEnable;

            cbFahrzeug.Enabled = boEnable;
            mtbKFZ.Enabled = boEnable;
            tbInfo.Enabled = boEnable;
            cbLagerTransport.Enabled = boEnable;

            //Artikel Ausgang Menu Enabled setzen
            SetArtikelAusgangMenuEnabled(boEnable);
            minMaxADRSearch.Enabled = boEnable;

            //Customize 
            clsClient.ctrAuslagerung_CustomizeAusgangAdrDatenInputFieldsEnabled(this._ctrMenu._frmMain.system.Client.MatchCode, ref btnEmpfänger, ref btnManEmpfaenger, ref tbMCEmpfänger);
            clsClient.ctrAuslagerung_CustomizeAusgangAdrDatenInputFieldsEnabled(this._ctrMenu._frmMain.system.Client.MatchCode, ref this.btnEntladestelle, ref this.btnManEntladestelle, ref this.tbMCEntladestelle);
        }
        ///<summary>ctrAuslagerung / SetEnableMenuLAusgang</summary>
        ///<remarks>Menü LAusgang aktivieren / deaktivieren.</remarks>
        private void SetMenuLAusgangEnabled(bool bEnable)
        {
            tsbtnAuslagerungSpeichern.Enabled = bEnable & ((this.Lager.Ausgang.LockedBy == 0 || this.Lager.Ausgang.LockedBy == this.GL_User.User_ID) && !this.Lager.Ausgang.Checked);
            tsbtnBack.Enabled = bEnable;
            tsbtnForward.Enabled = bEnable;
            tsbtnDeleteLAusgang.Enabled = (!Lager.Ausgang.Checked) & (this.Lager.Ausgang.LockedBy == 0 || this.Lager.Ausgang.LockedBy == this.GL_User.User_ID);
            tsbtnCheckComplete.Enabled = (!Lager.Ausgang.Checked) & (this.Lager.Ausgang.LockedBy == 0 || this.Lager.Ausgang.LockedBy == this.GL_User.User_ID);
        }
        ///<summary>ctrAuslagerung / btnEntladestelle_Click</summary>
        ///<remarks>Adresssuche.</remarks>
        private void btnEntladestelle_Click(object sender, EventArgs e)
        {
            _ADRSearch = "Entladestelle";
            //_iSearchButton = 4;
            _iSearchButton = 12;
            this._ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrAuslagerung / btnEmpfänger_Click</summary>
        ///<remarks>Adresssuche.</remarks>
        private void btnEmpfänger_Click(object sender, EventArgs e)
        {
            _ADRSearch = "Empfänger";
            _iSearchButton = 3;
            this._ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrAuslagerung / btnAuftraggeber_Click</summary>
        ///<remarks>Adresssuche.</remarks>
        private void btnAuftraggeber_Click(object sender, EventArgs e)
        {
            _ADRSearch = "Auftraggeber";
            _iSearchButton = 1;
            this._ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrAuslagerung / btnVersender_Click</summary>
        ///<remarks>Adresssuche.</remarks>
        private void btnVersender_Click(object sender, EventArgs e)
        {
            _ADRSearch = "Versender";
            _iSearchButton = 2;
            this._ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrAuslagerung / btnSpedition_Click</summary>
        ///<remarks>Adresssuche.</remarks>
        private void btnSpedition_Click(object sender, EventArgs e)
        {
            _ADRSearch = "Spedition";
            _iSearchButton = 5;
            this._ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrAuslagerung/TakeOverAdressID</summary>
        ///<remarks>Delegatenfunktion aus der ADRSuche zur Übergabe der Adress ID. Anhand der ADR-ID 
        ///         werden die entsprechenden Adressdaten ausgelesen und die Daten entsprechend der 
        ///         Eingabefelder auf der Form gesetzt. Entsprechend müssen nun die Button entsprechend
        ///         aktiviert werden.</remarks>
        ///<param name="decTmp">Adress ID</param>
        ///<returns>decTmp ist der Rückgabewert, den wir aus der Adresssuche erhalten.</returns>
        public void TakeOverAdressID(decimal decTmp)
        {
            if (decTmp > 0)
            {
                SetADRByID(decTmp);
            }
        }
        ///<summary>ctrAuslagerung/SetADRByID</summary>
        ///<remarks>Adressdaten werden anhand der ID ermittelt und in die entsprechenden
        ///         Eingabefelder übergeben. Der Matchcode aus den Adressen wird vorerst
        ///         hier mitübernommen wird später noch einmal überschrieben.</remarks>
        public void SetADRByID(decimal ADR_ID)
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
                switch (_iSearchButton)
                {
                    //Auftraggeber
                    case 1:
                        _ADRAuftraggeber = ADR_ID;
                        tbMCAuftraggeber.Text = strMC;
                        tbADRAuftraggeber.Text = strE;

                        if (this.Lager.Ausgang.LAusgangTableID < 1)
                        {
                            clsClient.ctrAuslagerung_CustomizeDefaulAusgangsdaten(ref this._ctrMenu._frmMain.system, ref Lager);
                            //Entladestelle
                            if (Lager.Ausgang.Entladestelle > 0)
                            {
                                //default Empfänger wurde gesetzt
                                this._iSearchButton = 4;
                                SetADRByID(Lager.Ausgang.Entladestelle);
                            }
                            //Empfänger
                            if (Lager.Ausgang.Empfaenger > 0)
                            {
                                //default Empfänger wurde gesetzt
                                this._iSearchButton = 3;
                                SetADRByID(Lager.Ausgang.Empfaenger);
                            }
                            //Beladeadresse     
                            if (Lager.Ausgang.BeladeID > 0)
                            {
                                //default Beladeadresse wurde gesetzt
                                //this._iSearchButton = 2;
                                //SetADRByID(Lager.Ausgang.BeladeID);
                            }
                            //Versender     
                            if (Lager.Ausgang.Versender > 0)
                            {
                                //default Empfänger wurde gesetzt
                                this._iSearchButton = 2;
                                SetADRByID(Lager.Ausgang.Versender);
                            }
                        }

                        if (this.Lager.Ausgang.LAusgangTableID < 1)
                        {
                            if (workerDGVBestand.IsBusy != true)
                            {
                                workerDGVBestand.RunWorkerAsync();
                            }
                        }
                        break;

                    //Versender
                    case 2:
                        _ADRVersender = ADR_ID;
                        //tbMCV.Text = strMC;
                        //tbVersender.Text = strE;
                        clsADRMan.DeleteAllByTableIDAndAdrArtID(this.GL_User, "LAusgang", this.Lager.Ausgang.LAusgangTableID, clsADRMan.cont_AdrArtID_Versender);
                        break;
                    //Empfänger
                    case 3:
                        _ADREmpfänger = ADR_ID;
                        tbMCEmpfänger.Text = strMC;
                        tbADREmpfänger.Text = strE;
                        clsADRMan.DeleteAllByTableIDAndAdrArtID(this.GL_User, "LAusgang", this.Lager.Ausgang.LAusgangTableID, clsADRMan.cont_AdrArtID_Empfaenger);
                        break;
                    //Entladestelle
                    case 4:
                        _ADREntladestelle = ADR_ID;
                        tbMCEntladestelle.Text = strMC;
                        tbADREntladestelle.Text = strE;
                        clsADRMan.DeleteAllByTableIDAndAdrArtID(this.GL_User, "LAusgang", this.Lager.Ausgang.LAusgangTableID, clsADRMan.cont_AdrArtID_Entladeadresse);
                        break;
                    //Spedition
                    case 5:
                        _ADRSpedition = ADR_ID;
                        tbMCSpedition.Text = strMC;
                        tbADRSpedition.Text = strE;
                        //Alle entsprechenden Daten aus ADRMan löschen
                        clsADRMan.DeleteAllByTableIDAndAdrArtID(this.GL_User, "LAusgang", this.Lager.Ausgang.LAusgangTableID, clsADRMan.cont_AdrArtID_Spedition);
                        break;
                }
            }
            _iSearchButton = 0;
        }
        ///<summary>ctrAuslagerung/tbMCAuftraggeber_Validated</summary>
        ///<remarks>Sobald die Auftraggeberdaten gesetzt wurden werden alle eingelagerten Artikel im Grid angezeigt.</remarks>
        private void tbMCAuftraggeber_Validated(object sender, EventArgs e)
        {
            if (_ADRAuftraggeber > 0)
            {
                //Initialisieren der ArtikelGriddaten
                InitDGV();
            }
        }
        ///<summary>ctrAuslagerung/tsbtnAuslagerungSpeichern_Click</summary>
        ///<remarks>Sobald die Auftraggeberdaten gesetzt wurden werden alle eingelagerten Artikel im Grid angezeigt.</remarks>
        private void tsbtnAuslagerungSpeichern_Click(object sender, EventArgs e)
        {
            SaveAusgangDaten();
        }
        ///<summary>ctrAuslagerung/tsbtnAuslagerungSpeichern_Click</summary>
        ///<remarks>Sobald die Auftraggeberdaten gesetzt wurden werden alle eingelagerten Artikel im Grid angezeigt.</remarks>
        private void SaveAusgangDaten()
        {
            if (GL_User.write_LagerAusgang)
            {
                if (_MandantenID > 0)
                {
                    decimal decTmp = 0;
                    Lager.Ausgang._GL_User = this.GL_User;
                    //Lager.Ausgang.AbBereichID = this.GL_User.sys_ArbeitsbereichID;
                    Lager.Ausgang.AbBereichID = this.GL_System.sys_ArbeitsbereichID;
                    Lager.Ausgang.MandantenID = this.GL_System.sys_MandantenID;

                    //Ausgangsdaten zuweisen
                    decTmp = 0;
                    decimal.TryParse(tbAusgangsnummer.Text, out decTmp);
                    Lager.Ausgang.LAusgangID = decTmp;
                    Lager.Ausgang.LAusgangsDate = dtpAusgangDate.Value;
                    decTmp = 0;
                    Decimal.TryParse(tbANetto.Text, out decTmp);
                    Lager.Ausgang.GewichtNetto = decTmp;
                    decTmp = 0;
                    Decimal.TryParse(tbABrutto.Text, out decTmp);
                    Lager.Ausgang.GewichtBrutto = decTmp;
                    Lager.Ausgang.Auftraggeber = _ADRAuftraggeber;
                    Lager.Ausgang.Empfaenger = _ADREmpfänger;
                    Lager.Ausgang.Entladestelle = _ADREntladestelle;
                    Lager.Ausgang.Versender = _ADRVersender;
                    decTmp = 0;
                    decimal.TryParse(tbSLB.Text, out decTmp);
                    Lager.Ausgang.SLB = decTmp;
                    Lager.Ausgang.MAT = string.Empty;
                    Lager.Ausgang.Checked = false;
                    Lager.Ausgang.Info = tbInfo.Text.Trim();
                    if (tbLieferantenNr.Text.Trim().Equals(string.Empty))
                    {
                        tbLieferantenNr.Text = clsADRVerweis.GetLieferantenVerweisBySenderAndReceiverAdr(Lager.Ausgang.Auftraggeber, Lager.Ausgang.Empfaenger, this.GL_User.User_ID, constValue_AsnArt.const_Art_VDA4913, this._ctrMenu._frmMain.system.AbBereich.ID);
                    }
                    Lager.Ausgang.Lieferant = tbLieferantenNr.Text.Trim();
                    Lager.Ausgang.exTransportRef = tbExTransportRef.Text.Trim();
                    DateTime dtTmp;
                    if (cbTermin.Checked)
                    {
                        dtTmp = Convert.ToDateTime(dtpT_date.Value.ToShortDateString() + " " + ((DateTime)tpTerminZeit.Value).ToShortTimeString());
                    }
                    else
                    {
                        dtTmp = Globals.DefaultDateTimeMinValue;
                    }
                    Lager.Ausgang.Termin = dtTmp;
                    Lager.Ausgang.LagerTransport = cbLagerTransport.Checked;

                    //Transportmittel
                    string strKFZ = cbFahrzeug.Text.ToString();
                    switch (strKFZ)
                    {
                        case "--Bahn/Waggon--":
                            Lager.Ausgang.SpedID = 0;
                            Lager.Ausgang.KFZ = string.Empty;
                            Lager.Ausgang.WaggonNr = mtbKFZ.Text;
                            Lager.Ausgang.IsWaggon = true;
                            break;

                        case "--Fremdfahrzeug--":
                            Lager.Ausgang.SpedID = this._ADRSpedition;
                            Lager.Ausgang.KFZ = mtbKFZ.Text;
                            Lager.Ausgang.WaggonNr = string.Empty;
                            Lager.Ausgang.IsWaggon = false;
                            break;

                        default:
                            //eigene Fahrzeuge
                            Lager.Ausgang.SpedID = 0;
                            Lager.Ausgang.KFZ = mtbKFZ.Text;
                            Lager.Ausgang.WaggonNr = string.Empty;
                            Lager.Ausgang.IsWaggon = false;
                            break;
                    }
                    //--- Trailer
                    string strTrailer = cbTrailer.Text.ToString();
                    Lager.Ausgang.Trailer = string.Empty;
                    switch (strTrailer)
                    {
                        case "--Fremdfahrzeug--":
                            Lager.Ausgang.Trailer = mtbKFZTrailer.Text;
                            break;

                        default:
                            //eigene Fahrzeuge
                            Lager.Ausgang.SpedID = 0;
                            Lager.Ausgang.Trailer = mtbKFZTrailer.Text;
                            break;
                    }

                    Lager.Ausgang.Fahrer = tbFahrer.Text;

                    if (!bIsNew && Lager.Ausgang.CheckLAusgangByLAusgangDaten())
                    {
                        Lager.Ausgang.UpdateLagerAusgang();
                    }
                    else
                    {
                        Lager.Ausgang.Checked = false;
                        Lager.Ausgang.AddLAusgang();
                        Lager.Ausgang.FillAusgang();
                        bIsNew = false;
                        //InitLoad();                        
                    }
                    //Button ArtikelMOve freigebben
                    SetArtikelMoveButtonEnabled(true);
                    SetArtikelMenuEnabled(true);
                    //Am Ende alle Button im Menü freigeben
                    SetMenuLAusgangEnabled(true);
                    SetLAusgangsdatenToFrm();
                }
                else
                {
                    clsMessages.Allgemein_MandantFehlt();
                }
            }
            else
            {
                clsMessages.User_NoAuthen();
            }
        }
        ///<summary>ctrAuslagerung/InitDGV</summary>
        ///<remarks>Ermittelt alle eingelagerten Artikel des Auftraggebers und zeigt diese im Grid an.</remarks>
        private void InitDGV()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            InitDGV();
                                                                        }
                                                                    )
                                 );
                return;
            }
            if ((_ADRAuftraggeber > 0) && (_MandantenID > 0))
            {
                Lager.Eingang.Auftraggeber = _ADRAuftraggeber;
                Lager.Eingang.AbBereichID = this.GL_System.sys_ArbeitsbereichID;
                dtArtikel = Lager.Eingang.GetLagerArtikelDatenByAuftraggeber(iSelectedMenge, this._ctrMenu._frmMain.system.Client.Modul.Lager_DisplaySPLArtikelinAusgang);

                if ((!Lager.Ausgang.Checked) || (Lager.Ausgang.ExistLAusgangTableID()))
                {
                    SetDtArtikelAsDataSource();
                    //Auslagerungsartikel
                    if (!_bAusgangAktive)
                    {
                        dtArtikelAuslagerung = dtArtikel.Copy();
                        InitDGVAArtikel(true);
                    }
                    //sorgt dafür, dass bei abgeschlossenen Ausgängen das Artikelgrd geleert wird
                    if (Lager.Ausgang.Checked)
                    {
                        dtArtikel.Rows.Clear();
                        SetDtArtikelAsDataSource();
                        //this.dgv.DataSource = dtArtikel;
                        //this.dgvAArtikel.Enabled = false;
                        this.dgv.Enabled = false;
                    }
                    else
                    {
                        //this.dgvAArtikel.Enabled = true;
                    }
                    //Search Combo füllen
                    dtSearch.Clear();
                    dtSearch = dtArtikel;
                    //Functions.InitComboSearch(ref tscbSearch, dtSearch);
                    Functions.FillSearchColumnFromDGV(ref this.dgv, ref tscbSearch, this._ctrMenu._frmMain.system);
                }
                else
                {
                    dtArtikel.Rows.Clear();
                    //this.dgv.DataSource = dtArtikel;
                    SetDtArtikelAsDataSource();
                }
            }
            else
            {
                dtArtikel.Rows.Clear();
                //this.dgv.DataSource = dtArtikel;
                SetDtArtikelAsDataSource();
            }

        }
        ///<summary>ctrAuslagerung/SetDtArtikelAsDataSource</summary>
        ///<remarks></remarks>
        private void SetDtArtikelAsDataSource()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            SetDtArtikelAsDataSource();
                                                                        }
                                                                    )
                                 );
                return;
            }

            //Functions.setView(ref dtArtikel, ref this.dgv, "LAusgang", tscbArtikel.SelectedItem.ToString(), GL_System, false);
            Functions.setView(ref dtArtikel, ref this.dgv, "LAusgang", tscbArtikel.SelectedItem.ToString(), GL_System, false, null, false, "Selected");
            if (this.dgv.Rows.Count > 0)
            {
                //this.dgv.Columns["Ausgang"].IsPinned = true;
                //Baustelle 
                this.dgv.Columns["Selected"].IsPinned = true;
                this.dgv.Columns["Selected"].IsVisible = true;
            }
            this.dgv.BestFitColumns();
            if (this._ctrArtSearchFilter != null)
            {
                this._ctrArtSearchFilter.SetFilterforDGV(ref this.dgv, true);
                this._ctrArtSearchFilter.ClearFilterInput();
                this._ctrArtSearchFilter.SetFilterSearchElementAllEnabled(false);
                this._ctrArtSearchFilter.SetFilterElementEnabledByColumns(ref this.dgv);
            }

        }
        ///<summary>ctrAuslagerung/InitDGVAArtikel</summary>
        ///<remarks></remarks>
        public void refreshDGVAArtikel()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            refreshDGVAArtikel();
                                                                        }
                                                                    )
                                 );
                return;
            }
            clsArtikel art = new clsArtikel();
            art._GL_User = GL_User;
            dtArtikelAuslagerung = Lager.Ausgang.GetLagerLAusgangArtikelDaten();
            if (dtArtikel.Rows.Count > 0)
            {
                Functions.setView(ref dtArtikel, ref dgv, "LAusgang", tscbAArtikel.SelectedItem.ToString(), GL_System, false, null, false, "Check");
            }
            dgv.BestFitColumns();

        }
        ///<summary>ctrAuslagerung/InitDGVAArtikel</summary>
        ///<remarks></remarks>
        private void InitDGVAArtikel(bool bClearDT)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            InitDGVAArtikel(bClearDT);
                                                                        }
                                                                    )
                                 );
                return;
            }
            if (bClearDT)
            {
                dtArtikelAuslagerung.Rows.Clear();
            }
            dtArtikelAuslagerung = Lager.Ausgang.GetLagerLAusgangArtikelDaten();
            dgvAArtikel.DataSource = null;

            // Functions.setView(ref dtArtikel, ref this.dgv, "LAusgang", tscbArtikel.SelectedItem.ToString(), GL_System, false, null, false, "Selected");
            //wenn Ausgangs abgeschlossen ist, dann neue Spalte für Status select einfügen
            string strFirstCol = "Check";
            if (this._ctrMenu._frmMain.system.Client.Modul.ASNCall_UserCallStatus)
            {
                if (this.Lager.Ausgang.Checked)
                {
                    strFirstCol = "ENTL";
                    dtArtikelAuslagerung.Columns["ENTL"].SetOrdinal(0);
                }
                else
                {
                    strFirstCol = "Check";
                    if (dtArtikelAuslagerung.Columns.Contains("ENTL"))
                    {
                        dtArtikelAuslagerung.Columns.Remove("ENTL");
                    }
                }
            }
            else
            {
                strFirstCol = "Check";
                if (dtArtikelAuslagerung.Columns.Contains("ENTL"))
                {
                    dtArtikelAuslagerung.Columns.Remove("ENTL");
                }
            }
            if (dtArtikelAuslagerung.Columns["Check"] != null)
            {
                dtArtikelAuslagerung.Columns["Check"].SetOrdinal(1);
                dgvAArtikel.DataSource = dtArtikelAuslagerung;
            }
            Functions.setView(ref dtArtikelAuslagerung, ref this.dgvAArtikel, "LAusgangA", tscbAArtikel.SelectedItem.ToString(), GL_System, false, null, false, strFirstCol);



            if (this.dgvAArtikel.Columns["Ausgang"] != null)
            {
                this.dgvAArtikel.Columns["Ausgang"].IsVisible = false;
            }
            this.dgvAArtikel.BestFitColumns();
        }
        ///<summary>ctrAuslagerung/dgv_CellClick</summary>
        ///<remarks></remarks>
        //private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (this.dgv.Rows[e.RowIndex] != null)
        //    {
        //        string strTmpArtikelID = this.dgv.Rows[e.RowIndex].Cells["ArtikelID"].Value.ToString();
        //        decimal decTmp = 0;
        //        decimal.TryParse(strTmpArtikelID, out decTmp);
        //        if (decTmp > 0)
        //        {
        //            Int32 i = 0;
        //            while (i <= dtArtikel.Rows.Count - 1)
        //            {
        //                decimal decTmp2 = 0;
        //                string strTmpArtikelID2 = dtArtikel.Rows[i]["ArtikelID"].ToString();
        //                Decimal.TryParse(strTmpArtikelID2, out decTmp2);
        //                if (decTmp2 > 0)
        //                {
        //                    if (decTmp == decTmp2)
        //                    {
        //                        bool bAusgangRow = (bool)dtArtikel.Rows[i]["Selected"];
        //                        this.dtArtikel.Rows[i]["Selected"] = !bAusgangRow;
        //                        break;
        //                    }
        //                    i++;
        //                }
        //            }
        //        }
        //    }
        //}
        ///<summary>ctrAuslagerung/CopyDataRowdtAArtikel</summary>
        ///<remarks>Kopiert den gewählten Datensatz in die Ausgangstable und löscht den entsprechenden 
        ///         Datensatz aus der Aritkeltabelle.</remarks>
        private void CopyDataRowdtoAArtikel(bool bAll)
        {
            //for (Int32 i = 0; i <= this.dtArtikel.Rows.Count - 1; i++)
            Int32 i = 0;

            bool bSPL = false;
            while (i <= this.dtArtikel.Rows.Count - 1)
            {
                bool ToAusgang = false;
                string strTmp = string.Empty;
                //strTmp = dtArtikel.Rows[i]["Ausgang"].ToString();
                strTmp = dtArtikel.Rows[i]["Selected"].ToString();
                Boolean.TryParse(strTmp, out ToAusgang);

                if (ToAusgang)
                {
                    decimal decTmpArtID = 0;
                    string strTmpID = dtArtikel.Rows[i]["ArtikelID"].ToString();
                    Decimal.TryParse(strTmpID, out decTmpArtID);
                    if (decTmpArtID > 0)
                    {
                        if (this.dgv.Columns.Contains("spl") && dtArtikel.Rows[i]["spl"].GetType() != typeof(System.DBNull))
                        {
                            bSPL = true;
                            break;
                        }
                    }
                }
                i++;
            }
            bool bret = true;
            if (bSPL)
            {
                //bret = 
                clsMessages.Lager_Ausgang_SPL();
            }
            if (!bSPL)
            {
                i = 0;
                while (i <= this.dtArtikel.Rows.Count - 1)
                {
                    bool ToAusgang = false;
                    string strTmp = string.Empty;
                    //strTmp = dtArtikel.Rows[i]["Ausgang"].ToString();
                    strTmp = dtArtikel.Rows[i]["Selected"].ToString();
                    Boolean.TryParse(strTmp, out ToAusgang);
                    if (ToAusgang)
                    {
                        decimal decTmpArtID = 0;
                        string strTmpID = dtArtikel.Rows[i]["ArtikelID"].ToString();
                        Decimal.TryParse(strTmpID, out decTmpArtID);
                        if (decTmpArtID > 0)
                        {
                            if (dtArtikel.Columns.Contains("spl"))
                            {
                                if (dtArtikel.Rows[i]["spl"].GetType() != typeof(System.DBNull))
                                {
                                    bSPL = true;
                                    break;
                                }
                            }
                            clsLager.UpdateLArtikelLAusgang(GL_User, Lager.Ausgang.LAusgangTableID, decTmpArtID, false);
                            dtArtikelAuslagerung.ImportRow(dtArtikel.Rows[i]);
                            dtArtikel.Rows.RemoveAt(i);
                            i = -1;
                        }
                    }
                    i++;
                }
            }
        }
        ///<summary>ctrAuslagerung/tsbtnSelectedArtToAusgang_Click</summary>
        ///<remarks>Der markierte Artikeldatensatz wird in das Auslagerungsgrid übernommen.</remarks>
        private void tsbtnSelectedArtToAusgang_Click(object sender, EventArgs e)
        {
            _bAusgangAktive = true;
            CopyDataRowdtoAArtikel(false);
            SumArtikelAusgang();
            InitDGV();
            ClearSearch();
        }
        ///<summary>ctrAuslagerung/tsbtnAllToAusgang_Click</summary>
        ///<remarks>Übernimmt den kompletten Artikelbestand in die Ausgangstabelle.</remarks>
        private void tsbtnAllToAusgang_Click(object sender, EventArgs e)
        {
            _bAusgangAktive = true;
            CopyDataRowdtoAArtikel(true);
            SumArtikelAusgang();
            SetDTPAusgangDateMinDate();
            //ClearSearch();
        }
        ///<summary>ctrAuslagerung/tsbtnDelAllFromAAusgang_Click</summary>
        ///<remarks>Löscht alle Artikel aus der Auslagerungstable und läd das Artikelgrid neu.</remarks>
        private void tsbtnDelAllFromAAusgang_Click(object sender, EventArgs e)
        {
            _bAusgangAktive = true;
            RemoveRowFromArtAusgang(true);
            SumArtikelAusgang();
            SetDTPAusgangDateMinDate();
            ClearSearch();
        }
        ///<summary>ctrAuslagerung/tsbtnDelArtFromAAusgang_Click</summary>
        ///<remarks>Löscht den .</remarks>
        private void tsbtnDelArtFromAAusgang_Click(object sender, EventArgs e)
        {
            _bAusgangAktive = true;
            RemoveRowFromArtAusgang(false);
            SumArtikelAusgang();
            SetDTPAusgangDateMinDate();
            ClearSearch();
        }
        ///<summary>ctrAuslagerung/CopyDataRowdtAArtikel</summary>
        ///<remarks>Copiert den gewählten Datensatz in die Ausgangstable und löscht den entsprechenden 
        ///         Datensatz aus der Aritkeltabelle.</remarks>
        private void RemoveRowFromArtAusgang(bool bAll)
        {
            if (this.dgvAArtikel.Rows.Count > 0)
            {
                if (this.dgvAArtikel.CurrentCell != null)
                {
                    decimal decTmpArtID = (decimal)this.dgvAArtikel.Rows[this.dgvAArtikel.CurrentCell.RowIndex].Cells["ArtikelID"].Value;
                    if (decTmpArtID > 0)
                    {
                        string strSQL = string.Empty;
                        if (bAll)
                        {
                            Int32 i = 0;
                            while (i <= dtArtikelAuslagerung.Rows.Count - 1)
                            {
                                if (dtArtikelAuslagerung.Rows.Count > 0)
                                {
                                    if (!(bool)dtArtikelAuslagerung.Rows[i]["Check"])
                                    {
                                        decTmpArtID = 0;
                                        string strTmp = this.dgvAArtikel.Rows[i].Cells["ArtikelID"].Value.ToString();
                                        Decimal.TryParse(strTmp, out decTmpArtID);
                                        if (decTmpArtID > 0)
                                        {
                                            clsLager.UpdateLArtikelLAusgang(GL_User, 0, decTmpArtID, false);
                                            clsLager.UpdateLAusgangArtikelChecked(this.GL_User, decTmpArtID, false);

                                            //Testonly
                                            //strSQL += clsLager.SQLUpdateLArtikelLAusgang(GL_User, 0, decTmpArtID, false);
                                            //strSQL += clsLager.SQLUpdateLAusgangArtikelChecked(this.GL_User, decTmpArtID, false);
                                            dtArtikelAuslagerung.Rows.RemoveAt(i);
                                            i = 0;
                                        }
                                        else
                                        {
                                            i++;
                                        }
                                    }
                                    else
                                    {
                                        i++;
                                    }
                                }
                            }

                        }
                        else
                        {
                            for (Int32 i = 0; i <= dtArtikelAuslagerung.Rows.Count - 1; i++)
                            {
                                if (!(bool)this.dgvAArtikel.Rows[i].Cells["Check"].Value)
                                {
                                    if (decTmpArtID == (decimal)dtArtikelAuslagerung.Rows[i]["ArtikelID"])
                                    {
                                        clsLager.UpdateLArtikelLAusgang(GL_User, 0, decTmpArtID, false);

                                        //testonly
                                        //strSQL += clsLager.SQLUpdateLArtikelLAusgang(GL_User, 0, decTmpArtID, false);
                                        //strSQL += clsLager.SQLUpdateLAusgangArtikelChecked(this.GL_User, decTmpArtID, false);
                                        dtArtikelAuslagerung.Rows.RemoveAt(i);
                                        //dtArtikelAuslagerung.Rows.Remove(this.dtArtikelAuslagerung.Rows[i]);
                                        break;
                                    }
                                }
                            }
                        }
                        //clsLager.SQLDoTransaction(strSQL, this.GL_User);
                    }
                    InitDGV();
                }
            }
        }
        ///<summary>ctrAuslagerung/SetArtikelMoveButtonEnabled</summary>
        ///<remarks>Akiviert / Deaktiviert die Artikel Move Button.</remarks>
        private void SetArtikelMoveButtonEnabled(bool bEnabled)
        {
            bEnabled = bEnabled & (Lager.Ausgang.LockedBy == 0 || Lager.Ausgang.LockedBy == this.GL_User.User_ID);
            tsbtnDelArtFromAAusgang.Enabled = bEnabled;
            tsbtnAllToAusgang.Enabled = bEnabled;
            tsbtnDelAllFromAAusgang.Enabled = bEnabled;
            //tsbtnSelectedArtToAusgang.Enabled = bEnabled;
            this.dgv.Enabled = bEnabled;
            //Baustelle 17.12.2015
            //this.dgvAArtikel.Enabled = bEnabled;
        }
        ///<summary>ctrAuslagerung / tbMCAuftraggeber_TextChanged</summary>
        ///<remarks>Auftraggeber Adresssuche</remarks>
        private void tbMCAuftraggeber_TextChanged(object sender, EventArgs e)
        {
            //Adressdaten laden
            DataTable dt = new DataTable();
            dt = clsADR.GetADRList(this.GL_User.User_ID);
            DataTable dtTmp = new DataTable();

            if (dt.Rows.Count > 0)
            {
                string SearchText = tbMCAuftraggeber.Text.ToString();
                string Ausgabe = string.Empty;

                DataRow[] rows = dt.Select("Suchbegriff LIKE '" + SearchText + "'", "Suchbegriff");
                dtTmp = dt.Clone();
                foreach (DataRow row in rows)
                {
                    Ausgabe = Ausgabe + row["Suchbegriff"].ToString() + "\n";
                    dtTmp.ImportRow(row);
                }
                tbADRAuftraggeber.Text = Functions.GetADRStringFromTable(dtTmp);
                _ADRAuftraggeber = Functions.GetADR_IDFromTable(dtTmp);
                this._iSearchButton = 1;
                SetADRByID(_ADRAuftraggeber);
            }
        }
        ///<summary>ctrAuslagerung / tbMCEmpfänger_TextChanged</summary>
        ///<remarks>Empfänger Adresssuche</remarks>
        private void tbMCEmpfänger_TextChanged(object sender, EventArgs e)
        {
            //Adressdaten laden
            DataTable dt = new DataTable();
            dt = clsADR.GetADRList(this.GL_User.User_ID);
            DataTable dtTmp = new DataTable();

            //Suchtext
            string SearchText = tbMCEmpfänger.Text.ToString();
            string Ausgabe = "";

            DataRow[] rows = dt.Select("Suchbegriff LIKE '" + SearchText + "'", "Suchbegriff");
            dtTmp = dt.Clone();

            foreach (DataRow row in rows)
            {
                Ausgabe = Ausgabe + row["Suchbegriff"].ToString() + "\n";
                dtTmp.ImportRow(row);
            }
            tbADREmpfänger.Text = Functions.GetADRStringFromTable(dtTmp);
            _ADREmpfänger = Functions.GetADR_IDFromTable(dtTmp);
        }
        ///<summary>ctrAuslagerung / tbMCEntladestelle_TextChanged</summary>
        ///<remarks>Empfänger Adresssuche</remarks>
        private void tbMCEntladestelle_TextChanged(object sender, EventArgs e)
        {
            //Adressdaten laden
            DataTable dt = new DataTable();
            dt = clsADR.GetADRList(this.GL_User.User_ID);
            DataTable dtTmp = new DataTable();

            //Suchtext
            string SearchText = tbMCEntladestelle.Text.ToString();
            string Ausgabe = "";

            DataRow[] rows = dt.Select("Suchbegriff LIKE '" + SearchText + "'", "Suchbegriff");
            dtTmp = dt.Clone();

            foreach (DataRow row in rows)
            {
                Ausgabe = Ausgabe + row["Suchbegriff"].ToString() + "\n";
                dtTmp.ImportRow(row);
            }
            tbADREntladestelle.Text = Functions.GetADRStringFromTable(dtTmp);
            _ADREntladestelle = Functions.GetADR_IDFromTable(dtTmp);
        }
        ///<summary>ctrAuslagerung / SumArtikelAusgang</summary>
        ///<remarks>Ermittel bei jeder Veränderung des Ausgangs Gewicht und Anzahl neu.</remarks>
        private void SumArtikelAusgang()
        {
            //Ermittlung und Anzeige im Eingangskopf von Anzahl und Gewicht
            object objNetto = 0;
            object objBrutto = 0;
            if (dtArtikelAuslagerung.Rows.Count > 0)
            {
                if (dtArtikelAuslagerung.Columns.Contains("Netto"))
                {
                    objNetto = dtArtikelAuslagerung.Compute("SUM(Netto)", "LVSNr>0");
                }
                if (dtArtikelAuslagerung.Columns.Contains("Brutto"))
                {
                    objBrutto = dtArtikelAuslagerung.Compute("SUM(Brutto)", "LVSNr>0");
                }
            }
            if (_LAusgangTableID > 0)
            {
                clsLager.UpdateLAusgangGewichtNetto(GL_User, _LAusgangTableID, Convert.ToDecimal(objNetto.ToString()));
                clsLager.UpdateLAusgangGewichtBrutto(GL_User, _LAusgangTableID, Convert.ToDecimal(objBrutto.ToString()));
            }
            tbANetto.Text = Functions.FormatDecimal(Convert.ToDecimal(objNetto.ToString()));
            tbABrutto.Text = Functions.FormatDecimal(Convert.ToDecimal(objBrutto.ToString()));
            tbAAnzahl.Text = dtArtikelAuslagerung.Rows.Count.ToString();
        }
        ///<summary>ctrAuslagerung / tsbtnSaveArtikelAusgang_Click</summary>
        ///<remarks>Artikel werden gespeichert für den Lagerausgang.</remarks>
        private void tsbtnSaveArtikelAusgang_Click(object sender, EventArgs e)
        {
            if (GL_User.write_LagerAusgang)
            {
                //Checken ob LAusgangsID gespeichert ist
                Lager.Ausgang.LAusgangTableID = _LAusgangTableID;
                if (Lager.Ausgang.ExistLAusgangTableID())
                {
                    Lager.Ausgang.MandantenID = _MandantenID;
                    //Lager.Ausgang.AbBereichID = GL_User.sys_ArbeitsbereichID;
                    Lager.Ausgang.AbBereichID = this.GL_System.sys_ArbeitsbereichID;
                    Lager.Ausgang.AddArtikelToLAusgang(ref dtArtikelAuslagerung);
                }
            }
            else
            {
                clsMessages.User_NoAuthen();
            }
        }
        ///<summary>ctrAuslagerung / pbCheckEingang_Click</summary>
        ///<remarks></remarks>
        private void pbCheckAusgang_Click(object sender, EventArgs e)
        {
            this.tsbtnAuslagerungSpeichern.Enabled = false;

            if (this.Lager.Ausgang.LAusgangTableID > 0)
            {
                SetCheckAusgang();
            }
            //this.tsbtnAuslagerungSpeichern.Enabled = true;
        }
        ///<summary>ctrAuslagerung / SetCheckAusgang</summary>
        ///<remarks>Anpassen des Hintergrunds im Grid bei setzen des Flag Aritkel geprüft.</remarks>
        private void SetCheckAusgang()
        {
            Lager.Ausgang.FillAusgang();
            //check ob alle geprüft sind
            if (!Lager.Ausgang.Checked)
            {
                //Check Pflichtangaben Ausgang
                string strInfo = string.Empty;

                //alle Artikel gecheckt
                if (!Lager.Ausgang.bAllArtikelChecked)
                {
                    strInfo = strInfo + "- nicht alle Artikel sind gecheckt " + Environment.NewLine;
                }
                //Empfänger
                if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Auslagerung_RequiredReceiver)
                {
                    if (this.Lager.Ausgang.Empfaenger < 1)
                    {
                        strInfo = strInfo + "- Feld [Empfänger]  " + Environment.NewLine;
                    }
                }

                if (strInfo.Equals(string.Empty))
                {
                    //Lagerausgang kann abgeschlossen werden
                    pbCheckAusgang.Image = (Image)Sped4.Properties.Resources.check;
                    //LEingang entsprechend updaten
                    //clsLager.UpdateLAusgangSetAusgangAbgeschlossen(this.GL_User, Lager.Ausgang.LAusgangTableID, true);
                    clsLager.UpdateLAusgangSetAusgangAbgeschlossen(ref this.Lager, true);
                    //Lager.Ausgang.Checked = true;
                    Lager.Ausgang.FillAusgang();
                    if (this.Lager.Ausgang.Checked)
                    {
                        if (
                                (this._ctrMenu._frmMain.system.Client.Modul.Lager_Auslagerung_Print_DirectAusgangDoc) ||
                                (this._ctrMenu._frmMain.system.Client.Modul.Lager_Auslagerung_Print_DirectAusgangListe)
                            )
                        {
                            DirectPrintLAusgangDoc();
                            DirectPrintAusgangsliste();
                        }
                        else
                        {
                            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Auslagerung_ShowDirectPrintCenter)
                            {
                                this._ctrMenu.OpenCtrPrintLagerInFrm(this);
                            }
                        }
                        InitASNTransfer(clsASNAction.const_ASNAction_Ausgang);
                        //LvsCall Status setzen

                        //Lagerausgangkopf enable
                        SetLAusgangsKopfDatenEnabled(false);
                        // enable MoveButtons
                        SetArtikelMoveButtonEnabled(false);
                        SetArtikelMenuEnabled(false);
                        _bAusgangAktive = false;
                        _bLAusgangIsCHecked = true;
                        //Grid Artikel leeren
                        dtArtikel.Rows.Clear();
                    }
                }
                else
                {
                    string strKopf = "Der Ausgang kann nicht abgeschlossen werden, da folgende Pflichtfelder nicht ausgefüllt sind: " + Environment.NewLine;
                    strInfo = strKopf + Environment.NewLine + strInfo;
                    clsMessages.Allgemein_ERRORTextShow(strInfo);
                    pbCheckAusgang.Image = (Image)Sped4.Properties.Resources.warning.ToBitmap();
                }
            }
            else
            {
                pbCheckAusgang.Image = (Image)Sped4.Properties.Resources.check;
            }
            //Lager.Ausgang.FillAusgang();
        }
        ///<summary>ctrEinlagerung / CheckDirectPrintLabel</summary>
        ///<remarks>prüft die Freigabe auf den Labeldruck</remarks> 
        public void InitASNTransfer(Int32 myASNAction)
        {
            AsnTransfer = new clsASNTransfer();
            if (AsnTransfer.DoASNTransfer(this.GL_System, this.Lager.Ausgang.AbBereichID, this.Lager.Ausgang.MandantenID))
            {
                if (this._ctrMenu._frmMain.system.Client.Modul.ASN_UserOldASNFileCreation)
                {
                    if (myASNAction > 0)
                    {
                        this.Lager.ASNAction = new clsASNAction();
                        this.Lager.ASNAction.ASNActionProcessNr = myASNAction;
                        AsnTransfer.CreateLM(ref this.Lager);
                    }
                }
                else
                {
                    AsnTransfer.CreateLM_Ausgang(ref this.Lager);
                }
            }
        }
        ///<summary>ctrEinlagerung / DirectPrintLEingangDoc</summary>
        ///<remarks>Nach Abschluss des Eingangs soll direct das Dokument gedruckt werden</remarks> 
        private void DirectPrintLAusgangDoc()
        {
            //direct Print
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Auslagerung_Print_DirectAusgangDoc)
            {
                if (!this.Lager.Ausgang.IsPrintDoc)
                {
                    this.Lager.Ausgang.UpdatePrintLAusgang(enumDokumentenArt.LagerAusgangDoc.ToString());
                    ctrPrintLager TmpPrint = new ctrPrintLager();
                    TmpPrint.Hide();
                    TmpPrint._ctrMenu = this._ctrMenu;
                    TmpPrint._ctrAuslagerung = this;
                    TmpPrint._DokumentenArt = enumDokumentenArt.LagerAusgangDoc.ToString();
                    TmpPrint.SetLagerDatenToFrm();

                    clsSystem sys = new clsSystem();
                    //TmpPrint.nudPrintCount.Value = sys.GetCountPrintReport(ref this.GL_System, this.GL_System.sys_MandantenID,Globals.enumDokumentenart.LagerAusgangDoc.ToString());
                    TmpPrint.StartPrint(true);
                    TmpPrint.Dispose();
                    Lager.Ausgang.UpdatePrintLAusgang(enumDokumentenArt.LagerAusgangDoc.ToString());
                }
            }
        }
        ///<summary>ctrEinlagerung / DirectPrintLEingangDoc</summary>
        ///<remarks>Nach Abschluss des Eingangs soll direct das Dokument gedruckt werden</remarks> 
        private void DirectPrintLAusgangLfs()
        {
            //direct Print
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Auslagerung_Print_DirectAusgangDoc)
            {
                if (!this.Lager.Ausgang.IsPrintLfs)
                {
                    ctrPrintLager TmpPrint = new ctrPrintLager();
                    TmpPrint.Hide();
                    TmpPrint._ctrMenu = this._ctrMenu;
                    TmpPrint._ctrAuslagerung = this;
                    if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
                    {
                        TmpPrint._DokumentenArt = enumDokumentenArt.LagerAusgangLfs.ToString();
                        TmpPrint._DocPath = this.GL_System.docPath_AusgangList;
                        TmpPrint.nudPrintCount.Value = 1;
                    }
                    else
                    {
                        this._ctrMenu._frmMain.system.ReportDocSetting.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, this.Lager.Ausgang.Auftraggeber, this._ctrMenu._frmMain.system.AbBereich.ID);
                        TmpPrint.RepDocSettings = this._ctrMenu._frmMain.system.ReportDocSetting.GetClassByDocKey(enumIniDocKey.AusgangLfs.ToString());
                        TmpPrint._DokumentenArt = TmpPrint.RepDocSettings.DocKey;
                        TmpPrint._DocPath = TmpPrint.RepDocSettings.DocFileNameAndPath;
                        TmpPrint.nudPrintCount.Value = TmpPrint.RepDocSettings.PrintCount;
                    }
                    TmpPrint.SetLagerDatenToFrm();
                    TmpPrint.StartPrint(true);
                    TmpPrint.Dispose();
                    Lager.Ausgang.UpdatePrintLAusgang(enumDokumentenArt.LagerAusgangLfs.ToString());
                }
            }
        }
        ///<summary>ctrAuslagerung / DirectPrintAusgangsliste</summary>
        ///<remarks>Zurückblättern.</remarks>
        private void DirectPrintAusgangsliste()
        {
            bool bPrint = true;
            try
            {
                //direct Print
                if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Auslagerung_Print_DirectAusgangListe)
                {
                    ctrPrintLager TmpPrint = new ctrPrintLager();
                    TmpPrint.Hide();
                    TmpPrint._ctrMenu = this._ctrMenu;
                    TmpPrint._ctrAuslagerung = this;
                    //TmpPrint.InitCtr();
                    if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
                    {
                        TmpPrint._DokumentenArt = enumDokumentenArt.Ausgangsliste.ToString();
                        TmpPrint._DocPath = this.GL_System.docPath_AusgangList;
                        TmpPrint.nudPrintCount.Value = 1;
                    }
                    else
                    {
                        this._ctrMenu._frmMain.system.ReportDocSetting.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, this.Lager.Ausgang.Auftraggeber, this._ctrMenu._frmMain.system.AbBereich.ID);
                        TmpPrint.RepDocSettings = this._ctrMenu._frmMain.system.ReportDocSetting.GetClassByDocKey(enumIniDocKey.Ausgangsliste.ToString());
                        TmpPrint._DokumentenArt = TmpPrint.RepDocSettings.DocKey;
                        TmpPrint._DocPath = TmpPrint.RepDocSettings.DocFileNameAndPath;
                        TmpPrint.nudPrintCount.Value = TmpPrint.RepDocSettings.PrintCount;
                    }
                    TmpPrint.SetLagerDatenToFrm();
                    TmpPrint.StartPrint(true);
                    TmpPrint.Dispose();
                }
            }
            catch (Exception ex)
            {
                bPrint = false;
                clsMessages.Allgemein_ERRORTextShow(ex.ToString());
            }
            finally
            {
                if (bPrint)
                {
                    Lager.Ausgang.UpdatePrintLAusgang(enumDokumentenArt.Ausgangsliste.ToString());
                }
            }
        }
        ///<summary>ctrAuslagerung / tsbtnLastItem_Click</summary>
        ///<remarks>Zurückblättern.</remarks>
        private void tsbtnLastItem_Click(object sender, EventArgs e)
        {
            this.dtArtikel.Rows.Clear();
            //this.dgv.Rows.Clear();
            this.dgv.DataSource = null;
            Lager.Ausgang.LAusgangID = 0;
            GetNextLAusgang(false);
            SetLAusgangsdatenToFrm();

        }
        ///<summary>ctrAuslagerung / tsbtnFirstItem_Click</summary>
        ///<remarks>Zum ersten Ausgang wechseln.</remarks>
        private void tsbtnFirstItem_Click(object sender, EventArgs e)
        {
            this.dtArtikel.Rows.Clear();
            //this.dgv.Rows.Clear();
            this.dgv.DataSource = null;
            Lager.Ausgang.LAusgangID = 0;
            GetNextLAusgang(true);
            SetLAusgangsdatenToFrm();

        }
        ///<summary>ctrAuslagerung / tsbtnBack_Click</summary>
        ///<remarks>Zurückblättern.</remarks>
        private void tsbtnBack_Click(object sender, EventArgs e)
        {
            //this.dgv.Rows.Clear();
            this.dtArtikel.Rows.Clear();
            //this.dgv.Rows.Clear();
            this.dgv.DataSource = null;
            GetNextLAusgang(true);
            SetLAusgangsdatenToFrm();
        }
        ///<summary>ctrAuslagerung / tsbtnForward_Click</summary>
        ///<remarks>vorwärts.</remarks>
        private void tsbtnForward_Click(object sender, EventArgs e)
        {
            this.dtArtikel.Rows.Clear();
            //this.dgv.Rows.Clear();
            this.dgv.DataSource = null;
            GetNextLAusgang(false);
            SetLAusgangsdatenToFrm();
        }
        ///<summary>ctrAuslagerung / tsbtnDeleteLAusgang_Click</summary>
        ///<remarks>Ausgang löschen nur wenn noch nicht abgeschlossen.</remarks>
        private void tsbtnDeleteLAusgang_Click(object sender, EventArgs e)
        {
            if (GL_User.write_LagerAusgang)
            {
                if (!_bLAusgangIsCHecked)
                {
                    if (this._ctrMenu._frmMain.system.Client.Modul.Lager_AskForDeleteEA && clsMessages.Lager_Delete())
                    {
                        if (Lager.Ausgang.ExistLAusgangTableID())
                        {
                            //Artikel
                            Lager.Ausgang.DeleteLAusgangByLAusgangTableID();
                            //Leere der Felder
                            ClearLAusgangEingabefelder();
                            this.Lager.Ausgang.LAusgangID = 0;
                            GetNextLAusgang(false);
                            SetLAusgangsdatenToFrm();
                        }
                    }
                }
            }
            else
            {
                clsMessages.User_NoAuthen();
            }
        }
        ///<summary>ctrAuslagerung / SetLabelKennzeichen</summary>
        ///<remarks></remarks> 
        private void SetLabelKennzeichen(bool bIsKFZ)
        {
            if (bIsKFZ)
            {
                lKennzeichen.Text = "KFZ - Kennzeichen:";
            }
            else
            {
                lKennzeichen.Text = "Waggon-Nr.:";
            }
        }
        ///<summary>ctrAuslagerung / cbFahrzeug_SelectedIndexChanged</summary>
        ///<remarks>Fahrzeug wurde ausgewählt.</remarks>
        private void cbFahrzeug_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFahrzeug.SelectedValue != null)
            {
                Int32 iValue = -1;
                Int32.TryParse(cbFahrzeug.SelectedValue.ToString(), out iValue);
                switch (iValue)
                {
                    //Bahn/Waggon
                    case -2:
                        SetLabelKennzeichen(false);
                        mtbKFZ.Text = string.Empty;
                        _ADRSpedition = 0;
                        SetFelderFremdfahrzeugeEnabled(false, true);
                        mtbKFZ.Mask = this._ctrMenu._frmMain.system.Client.Modul.Lager_WaggonNo_Mask;
                        mtbKFZ.Focus();
                        break;
                    //Fremdfahrzeuge
                    case -1:
                        SetLabelKennzeichen(true);
                        //Felder aktivieren
                        SetFelderFremdfahrzeugeEnabled(true);
                        mtbKFZ.Mask = "";
                        mtbKFZ.Text = cbFahrzeug.Text;
                        mtbKFZ.Focus();
                        break;
                    //eigene KFZ
                    default:
                        SetLabelKennzeichen(true);
                        //Auswahl eigene Fahrzeuge
                        SetFelderFremdfahrzeugeEnabled(false);
                        _ADRSpedition = 0;
                        mtbKFZ.Mask = "";
                        mtbKFZ.Text = cbFahrzeug.Text;
                        break;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbTrailer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTrailer.SelectedValue != null)
            {
                Int32 iValue = -1;
                Int32.TryParse(cbFahrzeug.SelectedValue.ToString(), out iValue);
                switch (iValue)
                {
                    //Fremdfahrzeuge
                    case -1:
                        SetLabelKennzeichen(true);
                        //Felder aktivieren
                        SetFelderFremdfahrzeugeEnabled(true);
                        mtbKFZTrailer.Mask = "";
                        mtbKFZTrailer.Text = cbTrailer.Text;
                        mtbKFZTrailer.Focus();
                        break;
                    //eigene KFZ
                    default:
                        SetLabelKennzeichen(true);
                        //Auswahl eigene Fahrzeuge
                        SetFelderFremdfahrzeugeEnabled(false);
                        _ADRSpedition = 0;
                        mtbKFZTrailer.Mask = "";
                        mtbKFZTrailer.Text = cbTrailer.Text;
                        break;
                }
            }
        }
        ///<summary>ctrAuslagerung / SetFelderFremdfahrzeugeEnabled</summary>
        ///<remarks>.</remarks>
        private void SetFelderFremdfahrzeugeEnabled(bool bEndabled, bool bWaggon = false)
        {
            tbMCSpedition.Enabled = bEndabled;
            tbADRSpedition.Enabled = bEndabled;
            btnSpedition.Enabled = bEndabled;
            btnManSped.Enabled = bEndabled;
            if (bWaggon == false)
            {
                mtbKFZ.Enabled = bEndabled;
                mtbKFZTrailer.Enabled = bEndabled;
            }
            else
            {
                mtbKFZ.Enabled = !bEndabled;
                mtbKFZTrailer.Enabled = !bEndabled;
            }
            if (!bEndabled)
            {
                tbMCSpedition.Text = string.Empty;
                tbADRSpedition.Text = string.Empty;
            }
        }
        ///<summary>ctrAuslagerung / GetTerminDateTime</summary>
        ///<remarks>Setzt den Termin anhand von dem Datum und den gewählten Stunden / Min zusammen.</remarks>
        private DateTime GetTerminDateTime()
        {
            string strDateTime = string.Empty;
            strDateTime = dtpT_date.Value.ToShortDateString() + " " + ((DateTime)tpTerminZeit.Value).ToShortTimeString();
            DateTime dtTermin = Convert.ToDateTime(strDateTime);
            return dtTermin;
        }
        ///<summary>ctrAuslagerung / dtpT_date_ValueChanged</summary>
        ///<remarks>Termin muss gleich oder jünger als das Lagerausgangsdatum sein</remarks>
        private void dtpT_date_ValueChanged(object sender, EventArgs e)
        {
            if (dtpT_date.Value < dtpAusgangDate.Value)
            {
                dtpT_date.Value = dtpAusgangDate.Value;
            }

        }
        ///<summary>ctrAuslagerung / tsbtnIntegrate_Click</summary>
        ///<remarks>Ctr in Hauptform integrieren.</remarks>
        private void tsbtnIntegrate_Click(object sender, EventArgs e)
        {
            object obj = this;
            if (this._frmTmp != null)
            {
                this._frmTmp.CloseFrmTmp();
            }
            else
            {
                this._ctrMenu.CloseCtrAuslagerungFrmTmp();
            }
            this._ctrMenu.OpenCtrAuslagerung(this);
        }
        ///<summary>ctrAuslagerung / toolStripButton1_Click</summary>
        ///<remarks>Ctr in eingenem Fenster anzeigen.</remarks>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            object obj = this;
            this._ctrMenu.OpenCtrAuslagerungInFrm(obj);
            this._ctrMenu.CloseCtrAuslagerung();
        }
        ///<summary>ctrAuslagerung / tstbSearchArtikel_TextChanged</summary>
        ///<remarks></remarks>
        private void tstbSearchArtikel_TextChanged(object sender, EventArgs e)
        {
            string strFilter = string.Empty;// "Selected = False ";
            string strSubFilter = string.Empty;
            strSubFilter = Functions.GetSearchFilterString(ref tscbSearch, tscbSearch.Text, tstbSearchArtikel.Text);
            if (strSubFilter != string.Empty)
            {
                //strFilter = strFilter + " AND " + strSubFilter;
                strFilter = strSubFilter;
            }
            dtArtikel.DefaultView.RowFilter = strFilter;
            dtArtikel.DefaultView.Sort = tscbSearch.Text;
        }
        ///<summary>ctrAuslagerung / tsbtnShowAll_Click</summary>
        ///<remarks></remarks>
        private void tsbtnShowAll_Click(object sender, EventArgs e)
        {
            ClearSearch();
        }
        ///<summary>ctrAuslagerung / ClearSearch</summary>
        ///<remarks></remarks>
        private void ClearSearch()
        {
            //dtArtikel.DefaultView.RowFilter = "Selected = False ";
            tstbSearchArtikel.Text = string.Empty;
            tscbSearch.SelectedIndex = -1;
            tscbSearch.Text = string.Empty;
            //Sortbefehl löschen
            if (dtArtikel.Rows.Count > 0)
            {
                dtArtikel.DefaultView.Sort = string.Empty;
            }
            InitDGVAArtikel(false);
        }
        ///<summary>ctrAuslagerung / tsbtnSearch_Click</summary>
        ///<remarks></remarks>
        private void tsbtnSearch_Click(object sender, EventArgs e)
        {
            this.iStatusSearch = const_StatusSearch_ArtikelSuche;
            this._ctrMenu.OpenCtrSearch(this);
        }
        ///<summary>ctrAuslagerung / tsbtnSearch_Click</summary>
        ///<remarks></remarks>
        private void tsbtnStoreOutDirect_Click(object sender, EventArgs e)
        {
            this.iStatusSearch = const_StatusSearch_ArtikelAuslagerung;
            this._ctrMenu.OpenCtrSearch(this);
        }
        ///<summary>ctrAuslagerung / tsbtnSearch_Click</summary>
        ///<remarks></remarks>
        private void tsbtnCheckComplete_Click(object sender, EventArgs e)
        {
            this.tsbtnAuslagerungSpeichern.Enabled = false;
            if (dgvAArtikel.Rows.Count > 0)
            {
                SaveAusgangDaten();// auf wunsch herr honselmann wird erst gespeichert dann abgeschlossen
                this.Lager.Ausgang.UpdatePrintStatus();
                //Lager.Ausgang.FillAusgang();
                // Code aus dem Worker heraus genommen
                completeAusgang();
            }
            //this.tsbtnAuslagerungSpeichern.Enabled = true;
        }
        ///<summary>ctrAuslagerung / SetLabelDirektanlieferung</summary>
        ///<remarks></remarks> 
        private void SetLabelDirektanlieferung()
        {
            bool bVisible = false;
            lDirektanlieferung.Text = string.Empty;
            if (this.Lager.Ausgang != null)
            {
                if (this.Lager.Ausgang.IsRL)
                {
                    lDirektanlieferung.Text = const_AusgangArt_Ruecklieferung;
                    bVisible = this.Lager.Ausgang.IsRL;
                }
                if (this.Lager.Ausgang.DirectDelivery)
                {
                    lDirektanlieferung.Text = const_AusgangArt_Direktanlieferung;
                    bVisible = this.Lager.Ausgang.DirectDelivery;
                }
            }
            lDirektanlieferung.Visible = bVisible;
        }
        ///<summary>ctrAuslagerung / tsbtnPrintAusagng_Click</summary>
        ///<remarks>Lagerausgangdokumente erstellen</remarks> 
        private void tsbtnPrintAusagng_Click(object sender, EventArgs e)
        {
            this._ctrMenu.OpenCtrPrintLagerInFrm(this);
        }
        ///<summary>ctrAuslagerung / tsbtnChangeAusgang_Click</summary>
        ///<remarks>Ein abegschlossener Ausgang wird auf nicht gecheckt gesetzt und Ausgangsdaten neu
        ///         geladen und angezeitgt.</remarks> 
        private void tsbtnChangeAusgang_Click(object sender, EventArgs e)
        {
            //Update Ausgang zurücksetzen
            if (this.Lager.Ausgang.ASN == 0)
            {
                Lager.Ausgang.Checked = false;
                Lager.Ausgang.UpdateLagerAusgang();

                //Ausgang neu laden laden
                SetLAusgangsdatenToFrm();
            }
        }
        ///<summary>ctrAuslagerung / tscbSearch_SelectedIndexChanged</summary>
        ///<remarks></remarks> 
        private void tscbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tstbSearchArtikel.Text = string.Empty;
            this.dtArtikel.DefaultView.Sort = tscbSearch.Text;
        }
        ///<summary>ctrAuslagerung / dgv_CellClick</summary>
        ///<remarks>Setzt die Markierung für die Auslagerung auf True or False</remarks> 
        private void dgv_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (this.dgv.Rows[e.RowIndex] != null)
                {
                    int iARows = (int)dtArtikel.Compute("COUNT(Selected)", "Selected=true");
                    int iAARows = dtArtikelAuslagerung.Rows.Count;
                    int iSelRows = iARows + iAARows;

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
                        string strTmpArtikelID = this.dgv.Rows[e.RowIndex].Cells["ArtikelID"].Value.ToString();
                        decimal decTmp = 0;
                        decimal.TryParse(strTmpArtikelID, out decTmp);
                        if (decTmp > 0)
                        {
                            Int32 i = 0;
                            while (i <= dtArtikel.Rows.Count - 1)
                            {
                                decimal decTmp2 = 0;
                                string strTmpArtikelID2 = dtArtikel.Rows[i]["ArtikelID"].ToString();
                                Decimal.TryParse(strTmpArtikelID2, out decTmp2);
                                if (decTmp2 > 0)
                                {
                                    if (decTmp == decTmp2)
                                    {
                                        //bool bAusgangRow = (bool)dtArtikel.Rows[i]["Ausgang"];
                                        //this.dtArtikel.Rows[i]["Ausgang"] = !bAusgangRow;

                                        bool bAusgangRow = (bool)dtArtikel.Rows[i]["Selected"];
                                        this.dtArtikel.Rows[i]["Selected"] = !bAusgangRow;
                                        break;
                                    }
                                    i++;
                                }
                            }
                        }
                    }
                }
            }
        }
        ///<summary>ctrAuslagerung / tsbtnAllUncheck_Click</summary>
        ///<remarks>Alle Artikel im Ausgang als nicht geprüft markieren</remarks> 
        private void tsbtnAllUncheck_Click(object sender, EventArgs e)
        {
            SetArtikelImAusgangCheckedValue(false);
        }
        ///<summary>ctrAuslagerung / tsbtnAllCheck_Click</summary>
        ///<remarks>Alle Artikel im Ausgang als geprüft markieren</remarks> 
        private void tsbtnAllCheck_Click(object sender, EventArgs e)
        {
            SaveAusgangDaten();
            SetArtikelImAusgangCheckedValue(true);
        }
        ///<summary>ctrAuslagerung / tsbtnAllCheck_Click</summary>
        ///<remarks>Markiert alle Artikeldatensätze in der Table als markiert / unmarkiert</remarks> 
        private void SetArtikelImAusgangCheckedValue(bool bChecked)
        {
            for (Int32 i = 0; i <= dtArtikelAuslagerung.Rows.Count - 1; i++)
            {
                decimal decTmpArtID = 0;
                string strTmp = dtArtikelAuslagerung.Rows[i]["ArtikelID"].ToString();
                Decimal.TryParse(strTmp, out decTmpArtID);
                if (decTmpArtID > 0)
                {
                    clsLager.UpdateLAusgangArtikelChecked(this.GL_User, decTmpArtID, bChecked);
                    this.dtArtikelAuslagerung.Rows[i]["Check"] = bChecked;
                }
            }
            //SaveAusgangDaten();
            bool bAllChecked = true;
            for (Int32 i = 0; i < dtArtikelAuslagerung.Rows.Count; i++)
            {
                if (!(bool)dtArtikelAuslagerung.Rows[i]["Check"])
                {
                    bAllChecked = false;
                }
            }

            if (bAllChecked)
            {
                if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Auslagerung_Print_DirectAusgangDoc)
                {

                    //DirectPrintLAusgangDoc();
                    if (!this.workerPrint.IsBusy)
                        this.workerPrint.RunWorkerAsync();
                }
            }
        }
        ///<summary>ctrAuslagerung / SetArtikelAusgangMenuEnabled</summary>
        ///<remarks></remarks> 
        private void SetArtikelAusgangMenuEnabled(bool bEnabled)
        {
            bEnabled = bEnabled & (this.Lager.Ausgang.LockedBy == 0 || this.Lager.Ausgang.LockedBy == this.GL_User.User_ID);
            tsbtnAllCheck.Enabled = bEnabled;
            tsbtnAllUncheck.Enabled = bEnabled;

            //STatus Button
            this.tsbtnAbrufSetStatusAll.Visible = this.tsbtnAbrufSetStatusAll.Visible && (this.Lager.Ausgang.Checked);
        }
        ///<summary>ctrAuslagerung / SetArtikelAusgangMenuEnabled</summary>
        ///<remarks></remarks> 
        private void SetArtikelMenuEnabled(bool bEnabled)
        {
            bEnabled = bEnabled & (this.Lager.Ausgang.LockedBy == 0 || this.Lager.Ausgang.LockedBy == this.GL_User.User_ID);
            tsbtnShowAll.Enabled = bEnabled;
            tsbtnRefreshBestand.Enabled = bEnabled;
        }
        ///<summary>ctrAuslagerung / dgvAArtikel_CellClick</summary>
        ///<remarks></remarks> 
        private void dgvAArtikel_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column.Name.Equals("Check"))// == 0)
            {
                if (!Lager.Ausgang.Checked)
                {
                    if (e.RowIndex > -1)
                    {
                        string strTmp = string.Empty;
                        decimal decTmpArtID = (decimal)this.dgvAArtikel.Rows[e.RowIndex].Cells["ArtikelID"].Value;
                        strTmp = this.dgvAArtikel.Rows[e.RowIndex].Cells["Check"].Value.ToString();
                        if ((bool)this.dgvAArtikel.Rows[e.RowIndex].Cells["Check"].Value == true)
                        {
                            this.dgvAArtikel.Rows[e.RowIndex].Cells["Check"].Value = false;
                            clsLager.UpdateLAusgangArtikelChecked(this.GL_User, decTmpArtID, false);
                        }
                        else
                        {
                            this.dgvAArtikel.Rows[e.RowIndex].Cells["Check"].Value = true;
                            clsLager.UpdateLAusgangArtikelChecked(this.GL_User, decTmpArtID, true);
                        }

                        bool bAllChecked = true;
                        for (Int32 i = 0; i < dtArtikelAuslagerung.Rows.Count; i++)
                        {
                            if (!(bool)dtArtikelAuslagerung.Rows[i]["Check"])
                            {
                                bAllChecked = false;
                            }
                        }

                        if (bAllChecked)
                        {

                            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Auslagerung_Print_DirectAusgangDoc)
                            {
                                SaveAusgangDaten();
                                while (this.workerPrint.IsBusy)
                                {
                                }
                                this.workerPrint.RunWorkerAsync();
                                //DirectPrintLAusgangDoc();
                            }
                        }
                    }
                }
                else
                {
                    //Status setzen
                    this.dgvAArtikel.Rows[e.RowIndex].Cells["ENTL"].Value = true;
                    decimal decTmpArtID = 0;
                    if (decimal.TryParse(this.dgvAArtikel.Rows[e.RowIndex].Cells["ArtikelID"].Value.ToString(), out decTmpArtID))
                    {
                        clsASNCall.UpdateAbrufeSetAbrufStatus(decTmpArtID, this.GL_User, clsASNCall.const_Status_ENTL);
                    }
                }
            }
            if (e.Column.Name.Equals("ENTL"))// == 0)
            {
                if (this.Lager.Ausgang.Checked)
                {
                    if (!(bool)this.dgvAArtikel.Rows[e.RowIndex].Cells["ENTL"].Value)
                    {
                        //Status setzen
                        this.dgvAArtikel.Rows[e.RowIndex].Cells["ENTL"].Value = true;
                        decimal decTmpArtID = 0;
                        if (decimal.TryParse(this.dgvAArtikel.Rows[e.RowIndex].Cells["ArtikelID"].Value.ToString(), out decTmpArtID))
                        {
                            if (clsASNCall.UpdateAbrufeSetAbrufStatus(decTmpArtID, this.GL_User, clsASNCall.const_Status_ENTL))
                            {
                                InitDGVAArtikel(true);
                            }
                        }
                    }
                }
            }
        }
        ///<summary>ctrAuslagerung / tsbtnJumpToAusgang_Click</summary>
        ///<remarks></remarks> 
        private void tsbtnJumpToAusgang_Click(object sender, EventArgs e)
        {
            DoJumpToAusgang();
        }
        ///<summary>ctrAuslagerung / tstbJumpAusgangID_KeyPress</summary>
        ///<remarks></remarks> 
        private void tstbJumpAusgangID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (this.tstbJumpAusgangID.Text != string.Empty)
                {
                    DoJumpToAusgang();
                }
            }
        }
        ///<summary>ctrAuslagerung / DoJumpToAusgang</summary>
        ///<remarks></remarks> 
        public void DoJumpToAusgang()
        {
            this._ctrMenu._frmMain.ResetStatusBar();
            this._ctrMenu._frmMain.InitStatusBar(4);
            this._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
            this._ctrMenu._frmMain.StatusBarWork(false, "Daten werden geladen und initialisiert...");

            decimal decTmp = 0;
            Decimal.TryParse(tstbJumpAusgangID.Text, out decTmp);
            //decimal decEIDToJump = clsLEingang.GetLEingangTableIDByLEingangID(this.GL_User.User_ID, decTmp, _MandantenID);
            JumpToAusgang(decTmp);
            this._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
        }
        ///<summary>ctrAuslagerung / JumpToAusgang</summary>
        ///<remarks></remarks> 
        public void JumpToAusgang(decimal myLAusgangID)
        {
            decimal decEIDToJump = clsLAusgang.GetLAusgangTableIDByLAusgangID(this.GL_User.User_ID, myLAusgangID, this._ctrMenu._frmMain.system);

            //if (clsLAusgang.ExistLAusgangTableID(this.GL_User.User_ID, decEIDToJump))
            if (decEIDToJump > 0)
            {
                this._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
                Lager.Ausgang.LAusgangTableID = decEIDToJump;
                Lager.Ausgang.FillAusgang();
                ClearLAusgangEingabefelder();
                SetLAusgangsdatenToFrm();
            }
            else
            {
                this._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
                clsMessages.Lager_AusgangasIDExistiertNicht();
            }
        }
        ///<summary>ctrAuslagerung / tsbtnJumpToArtikel_Click</summary>
        ///<remarks></remarks> 
        private void tsbtnJumpToArtikel_Click(object sender, EventArgs e)
        {

            decimal decTmp = 0;
            Decimal.TryParse(tstbJumpToArtID.Text, out decTmp);
            decimal decArtikelID = clsArtikel.GetArtikelIDByLVSNr(this.GL_User, this._ctrMenu._frmMain.system, decTmp);
            DoJumpToArtikel(decArtikelID);
        }
        ///<summary>ctrAuslagerung / tstbJumpToArtID_KeyPress</summary>
        ///<remarks></remarks>
        private void tstbJumpToArtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (this.tstbJumpToArtID.Text != string.Empty)
                {

                    decimal decTmp = 0;
                    Decimal.TryParse(tstbJumpToArtID.Text, out decTmp);
                    decimal decArtikelID = clsArtikel.GetArtikelIDByLVSNr(this.GL_User, this._ctrMenu._frmMain.system, decTmp);
                    DoJumpToArtikel(decArtikelID);
                }
            }
        }
        ///<summary>ctrAuslagerung / DoJumpToArtikel</summary>
        ///<remarks></remarks> 
        private void DoJumpToArtikel(decimal myArtId)
        {
            this._ctrMenu._frmMain.ResetStatusBar();
            this._ctrMenu._frmMain.InitStatusBar(4);
            this._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
            this._ctrMenu._frmMain.StatusBarWork(false, "Daten werden geladen und initialisiert...");

            if (myArtId > 0)
            {
                LVS.Helper.helper_ArtikelArbeitsbereichCheck check = new LVS.Helper.helper_ArtikelArbeitsbereichCheck(myArtId, this._ctrMenu._frmMain.system, this.GL_System, this.GL_User);
                if (check.FindInSameWorkspace)
                {
                    Lager.Artikel.ID = check.ArtCheck.ID;
                    Lager.Artikel.GetArtikeldatenByTableID();
                    Lager.Ausgang.LAusgangTableID = Lager.Artikel.LAusgangTableID;
                    Lager.Ausgang.FillAusgang();
                    ClearLAusgangEingabefelder();
                    SetLAusgangsdatenToFrm();
                    this._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
                }
                else
                {
                    this._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
                    //clsMessages.Lager_Artikel_LVSNRNotExist(string.Empty);

                    if (clsMessages.Lager_Artikel_LVSNRNotExist(check.InfoText) == DialogResult.OK)
                    {
                        //direkt zum Artikel
                        //this._ctrMenu._frmMain.ChangeWorkspace(check.Arbeitsbereich.ID);
                        this._ctrMenu._frmMain.AusgangJumpDierctToArtInWorkspace(check.ArtCheck);

                    }
                }
            }
        }
        ///<summary>ctrAuslagerung / OpenManAdrInput</summary>
        ///<remarks></remarks> 
        private void OpenManAdrInput(Button myBtn)
        {
            Int32 iAdrArtID = -1;
            Int32.TryParse(myBtn.Tag.ToString(), out iAdrArtID);
            if (iAdrArtID > -1)
            {
                //Falls der Eingang noch nicht gespeichert ist,so muss dies hier 
                //geschehen, da sonst keien EingangTableID vorhanden ist
                if (this.Lager.Ausgang.LAusgangTableID == 0)
                {
                    SaveAusgangDaten();
                }
                SaveAusgangDaten();
                if (this.Lager.Ausgang.LAusgangTableID > 0)
                {
                    manAdrArt = string.Empty;
                    switch (iAdrArtID)
                    {
                        case 1:
                            manAdrArt = "Versender";
                            break;
                        case 2:
                            manAdrArt = "Auftraggeber";
                            break;
                        case 3:
                            manAdrArt = "Empfänger";
                            break;
                        case 4:
                            manAdrArt = "Entladestelle";
                            break;
                        case 5:
                            manAdrArt = "Spedition/Transportunternehmer";
                            break;
                    }
                    this.Lager.Ausgang.AdrManuell.TableName = "LAusgang";
                    this.Lager.Ausgang.AdrManuell.TableID = this.Lager.Ausgang.LAusgangTableID;
                    this.Lager.Ausgang.AdrManuell.AdrArtID = iAdrArtID;
                    this.Lager.Ausgang.AdrManuell.FillbyTableAndAdrArtID();

                    this._ctrADRManAdd = new ctrADRManAdd();
                    this._ctrADRManAdd._ctrAuslagerung = this;
                    this._ctrMenu.OpenFrmTMP(this._ctrADRManAdd);
                }
            }
        }
        ///<summary>ctrAuslagerung / btnManEmpfaenger_Click</summary>
        ///<remarks></remarks>
        private void btnManEmpfaenger_Click(object sender, EventArgs e)
        {
            OpenManAdrInput((Button)sender);
        }
        ///<summary>ctrAuslagerung / btnManEntladestelle_Click</summary>
        ///<remarks></remarks>
        private void btnManEntladestelle_Click(object sender, EventArgs e)
        {
            OpenManAdrInput((Button)sender);
        }
        ///<summary>ctrAuslagerung / btnManSped_Click</summary>
        ///<remarks></remarks>
        private void btnManSped_Click(object sender, EventArgs e)
        {
            OpenManAdrInput((Button)sender);
        }
        ///<summary>ctrAuslagerung / bestandMitFreigabeToolStripMenuItem_Click</summary>
        ///<remarks></remarks>
        private void bestandMitFreigabeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iSelectedMenge = 1;
            InitDGV();
            tstbCatLabel.Text = "Bestand mit Freigabe";
        }
        ///<summary>ctrAuslagerung / bestandOhneFreigabeToolStripMenuItem_Click</summary>
        ///<remarks></remarks>
        private void bestandOhneFreigabeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iSelectedMenge = 2;
            InitDGV();
            tstbCatLabel.Text = "Bestand ohne Freigabe";
        }
        ///<summary>ctrAuslagerung / bestandKomplettToolStripMenuItem_Click</summary>
        ///<remarks></remarks>
        private void bestandKomplettToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iSelectedMenge = 0;
            InitDGV();
            tstbCatLabel.Text = "Bestand komplett";
        }
        ///<summary>ctrAuslagerung / tsbtnSearchShow_Click</summary>
        ///<remarks></remarks>
        private void tsbtnSearchShow_Click(object sender, EventArgs e)
        {
            this.splitPanel3.Collapsed = (!this.splitPanel3.Collapsed);
        }
        ///<summary>ctrAuslagerung / tbMCAuftraggeber_TextChanged</summary>
        ///<remarks>Auftraggeber Adresssuche</remarks>
        private void tbMCSpedition_TextChanged(object sender, EventArgs e)
        {
            //Adressdaten laden
            DataTable dt = new DataTable();
            dt = clsADR.GetADRList(this.GL_User.User_ID);
            DataTable dtTmp = new DataTable();

            string SearchText = tbMCSpedition.Text.ToString();
            string Ausgabe = string.Empty;
            DataRow[] rows = dt.Select("Suchbegriff LIKE '" + SearchText + "'", "Suchbegriff");
            dtTmp = dt.Clone();
            foreach (DataRow row in rows)
            {
                Ausgabe = Ausgabe + row["Suchbegriff"].ToString() + "\n";
                dtTmp.ImportRow(row);
            }
            tbADRSpedition.Text = Functions.GetADRStringFromTable(dtTmp);
            _ADRSpedition = Functions.GetADR_IDFromTable(dtTmp);
        }
        ///<summary>ctrAuslagerung / LoadAusgang</summary>
        ///<remarks></remarks>
        //internal void LoadAusgang(int ausgang)
        internal void LoadAusgang()
        {
            if (_MandantenID > 0)
            {
                Lager.Ausgang._GL_User = this.GL_User;
                Lager.Ausgang.AbBereichID = this.GL_System.sys_ArbeitsbereichID;
                Lager.Ausgang.LAusgangTableID = clsLAusgang.GetLAusgangTableIDByLAusgangID(this.GL_User.User_ID, Lager.Ausgang.LAusgangID, this._ctrMenu._frmMain.system);
                Lager.FillLagerDaten(true);
                SetLAusgangsdatenToFrm();
            }
        }
        ///<summary>ctrAuslagerung / tscbArtikel_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void tscbArtikel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tscbArtikel.SelectedIndex > -1)
            {
                if (dtArtikel.Rows.Count > 0)
                {
                    SetDtArtikelAsDataSource();
                    Functions.FillSearchColumnFromDGV(ref this.dgv, ref this.tscbSearch, this._ctrMenu._frmMain.system);
                }
            }

        }
        ///<summary>ctrAuslagerung / tscbAArtikel_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void tscbAArtikel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tscbAArtikel.SelectedIndex > -1)
            {
                //if (dtArtikelAuslagerung.Rows.Count > 0)
                //{
                dgvAArtikel.DataSource = null;
                InitDGVAArtikel(false);
                //}
            }
        }
        ///<summary>ctrAuslagerung / tsbtnBackToBestand_Click</summary>
        ///<remarks></remarks>
        private void tsbtnBackToBestand_Click(object sender, EventArgs e)
        {
            switch (BackToCtr)
            {
                case ctrBestand.const_ControlName:
                    this._ctrMenu.OpenCtrBestand();
                    break;

                case ctrJournal.const_ControlName:
                    this._ctrMenu.OpenCtrJournal();
                    break;
            }
            if (this._frmTmp != null)
            {
                this._frmTmp.CloseFrmTmp();
            }
            this._ctrMenu.CloseCtrAuslagerung();
        }
        ///<summary>ctrAuslagerung / tsbtnRefreshBestand_Click</summary>
        ///<remarks></remarks>
        private void tsbtnRefreshBestand_Click(object sender, EventArgs e)
        {
            //if (this.workerDGVBestand.IsBusy != true)
            //{
            //    this.dgv.Enabled = false;
            //    this.workerDGVBestand.RunWorkerAsync();
            //}
            // Probleme mit bgWorker??
            this.dgv.Enabled = false;
            InitDGV();
            Functions.setView(ref dtArtikel, ref this.dgv, "LAusgang", tscbArtikel.SelectedItem.ToString(), GL_System, false);
            this.dgv.Enabled = !this.Lager.Ausgang.Checked;

        }
        ///<summary>ctrAuslagerung / dgvAArtikel_ToolTipTextNeeded</summary>
        ///<remarks></remark
        private void dgvAArtikel_ToolTipTextNeeded(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
        {
            GridDataCellElement cell = sender as GridDataCellElement;
            if (cell != null)
            {
                if (cell.ColumnInfo.Name == "Status")
                {
                    e.ToolTipText = string.Empty;
                    Dictionary<string, string> dict = DictSettings.DicStatus();
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
        ///<summary>ctrAuslagerung / dgv_ToolTipTextNeeded</summary>
        ///<remarks></remark
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
        ///<summary>ctrAuslagerung / cbTermin_CheckedChanged</summary>
        ///<remarks></remark
        private void cbTermin_CheckedChanged(object sender, EventArgs e)
        {
            dtpT_date.Enabled = cbTermin.Checked;
            tpTerminZeit.Enabled = cbTermin.Checked;
        }
        ///<summary>ctrAuslagerung / SetAusgangLocked</summary>
        ///<remarks></remark
        private void SetAusgangLocked(bool bSetLocked = true)
        {
            bool locked = false;
            if (Lager.Ausgang.LockedBy > 0 && Lager.Ausgang.LockedBy != this.GL_User.User_ID && bSetLocked)
            {
                locked = true;
            }
            //dgv.Enabled = !locked;
            //SetLAusgangsKopfDatenEnabled(!locked);
            //SetArtikelMenuBtnEnabled(locked);
            //SetArtikelEingabefelderDatenEnable(!locked);
            //tbHalle.Enabled = !locked;
            //tbReihe.Enabled = !locked;
            //tbInfoAusgang.ReadOnly = true;
            //SetLagerEingangsFelderEnabled(!locked);
            lbLocked.Visible = locked;
            lbLockedBy.Visible = locked;
            lbLockedBy.Text = clsUser.GetBenutzerFullNameByID(Lager.Ausgang.LockedBy);
            // tsbtnAuslagerungSpeichern
            //tsbtnDeleteLAusgang.Enabled = !locked;
            //tsbtnCheckComplete = !locked;
            tsbtnChangeAusgang.Enabled = !locked;

            //tsbtnDeleteLEingang.Enabled = tsbtnDeleteLEingang.Enabled & !locked;
            //tsbtnChangeEingang.Enabled = tsbtnChangeEingang.Enabled & !locked;
            //tsbtnCheckComplete.Enabled = tsbtnCheckComplete.Enabled & !locked;
            //pbCheckEingang.Enabled = !locked;
            //pbCheckArtikel.Enabled = !locked;
            //gbArtCheck.Enabled = !locked;
        }
        ///<summary>ctrAuslagerung / dgv_RowFormatting</summary>
        ///<remarks></remark
        private void dgv_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            if (this.dgv.Columns.Contains("spl") && e.RowElement.RowInfo.Cells["spl"].Value.GetType() != typeof(System.DBNull))
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

            if (this.dgv.Columns.Contains("spl") && e.RowElement.RowInfo.Cells["spl"].Value.GetType() != typeof(System.DBNull))
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
        ///<summary>ctrAuslagerung / dgvAArtikel_CellFormatting</summary>
        ///<remarks></remark
        private void dgvAArtikel_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            //if (e.Column.UniqueName.Equals("ENTL"))
            if (e.Column.Name.Equals("ENTL"))
            {
                if (this.Lager.Ausgang.Checked)
                {
                    if (e.CellElement.Value.GetType() != typeof(System.DBNull))
                    {
                        switch (e.Row.Cells["CallStatus"].Value.ToString())
                        {
                            case "":
                            case clsASNCall.const_Status_ENTL:
                                e.CellElement.Enabled = false;
                                break;
                            default:
                                e.CellElement.Enabled = true;
                                break;
                        }
                    }
                    else
                    {
                        e.CellElement.Enabled = false;
                    }
                }
                else
                {
                    e.CellElement.Enabled = false;
                }
            }
            else
            {
                e.CellElement.Enabled = (!this.Lager.Ausgang.Checked);
            }
        }
        ///<summary>ctrAuslagerung / dgvAArtikel_CellFormatting</summary>
        ///<remarks>Anpassen des Hintergrunds im Grid bei setzen des Flag Aritkel geprüft.</remarks>
        private void dgvAArtikel_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dgvAArtikel.Rows[e.RowIndex].Cells["Check"] != null)
            {
                if ((bool)this.dgvAArtikel.Rows[e.RowIndex].Cells["Check"].Value == true)
                {
                    e.CellStyle.BackColor = Color.Green;
                }
                else
                {
                    e.CellStyle.BackColor = Color.Red;
                }
            }
        }
        ///<summary>ctrAuslagerung / mtbKFZ_Enter</summary>
        ///<remarks></remark
        private void mtbKFZ_Enter(object sender, EventArgs e)
        {
            if (mtbKFZ.Text.Equals(const_Fremdfahrzeug))
            {
                mtbKFZ.Text = string.Empty;
            }
        }
        ///<summary>ctrAuslagerung / mtbKFZ_Leave</summary>
        ///<remarks></remark
        private void mtbKFZ_Leave(object sender, EventArgs e)
        {
            if (mtbKFZ.Text.Equals(string.Empty))
            {
                mtbKFZ.Text = const_Fremdfahrzeug;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mtbKFZTrailer_Enter(object sender, EventArgs e)
        {
            if (mtbKFZTrailer.Text.Equals(const_Fremdfahrzeug))
            {
                mtbKFZTrailer.Text = string.Empty;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mtbKFZTrailer_Leave(object sender, EventArgs e)
        {
            if (mtbKFZTrailer.Text.Equals(string.Empty))
            {
                mtbKFZTrailer.Text = const_Fremdfahrzeug;
            }
        }
        ///<summary>ctrAuslagerung / tsbtnASNReSENDTest_Click</summary>
        ///<remarks></remark
        private void tsbtnASNReSENDTest_Click(object sender, EventArgs e)
        {
            //if (this.Lager.Ausgang.LAusgangTableID > 0)
            //{
            //    Lager.Ausgang.FillAusgang();
            //    if (this.Lager.Ausgang.Checked)
            //    {
            //        if (clsMessages.ASN_ResendASN())
            //        {
            //            InitASNTransfer(clsASNAction.const_ASNAction_Ausgang);
            //            ////Lagerausgangkopf enable
            //            //SetLAusgangsKopfDatenEnabled(false);
            //            //// enable MoveButtons
            //            //SetArtikelMoveButtonEnabled(false);
            //            //SetArtikelMenuEnabled(false);
            //            //_bAusgangAktive = false;
            //            //_bLAusgangIsCHecked = true;
            //            ////Grid Artikel leeren
            //            //dtArtikel.Rows.Clear();
            //        }
            //    }
            //}
        }
        ///<summary>ctrAuslagerung / tsbtnAbrufSetStatusAll_Click</summary>
        ///<remarks></remark
        private void tsbtnAbrufSetStatusAll_Click(object sender, EventArgs e)
        {
            if (this.Lager.Ausgang.Checked)
            {
                foreach (DataRow row in this.dtArtikelAuslagerung.Rows)
                {
                    if (!(bool)row["ENTL"])
                    {
                        decimal decTmpArtID = 0;
                        if (decimal.TryParse(row["ArtikelID"].ToString(), out decTmpArtID))
                        {
                            clsASNCall.UpdateAbrufeSetAbrufStatus(decTmpArtID, this.GL_User, clsASNCall.const_Status_ENTL);
                        }
                    }
                }
                this.InitDGVAArtikel(true);
            }
        }
        ///<summary>ctrAuslagerung / dtpAusgangDate_ValueChanged</summary>
        ///<remarks></remark
        private void dtpAusgangDate_ValueChanged(object sender, EventArgs e)
        {
            SetDTPAusgangDateMinDate();
        }
        ///<summary>ctrAuslagerung / dtpAusgangDate_ValueChanged</summary>
        ///<remarks>Logikprüfung Datum 
        ///         - ermitteln vom MAX-Eingangsdatum aller enthaltenen Artikel
        ///         - MaxEingangsdatum = dtp_MinimalDatum
        ///         mr am 07.10.2016 hinzugefügt</remarks>
        private void SetDTPAusgangDateMinDate()
        {
            this.dtpAusgangDate.MinDate = Convert.ToDateTime("01.01.1753");
            if (dgvAArtikel.Rows.Count > 0)
            {
                this.dtpAusgangDate.MinDate = this.Lager.Ausgang.MinLAusgangsDate;
            }
            //else
            //{
            //    this.dtpAusgangDate.MinDate = Convert.ToDateTime("01.01.1753");
            //}
        }

        private void tsbtnRefresh_Click(object sender, EventArgs e)
        {
            InitLoad();
        }
    }
}
