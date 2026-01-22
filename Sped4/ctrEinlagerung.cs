using Common.Enumerations;
using LVS;
using LVS.CustomProcesses;
using LVS.Helper;
using LVS.ViewData;
using Sped4.Classes;
using Sped4.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls.UI;


namespace Sped4
{
    public partial class ctrEinlagerung : UserControl
    {
        //internal decimal _ArtikelTableID = 0;

        const decimal const_ArtikelValue_MaxLaenge = 14000M;
        const decimal const_ArtikelValue_WeightToAller = 40000M;

        public const Int32 const_cbFahrzeugValue_Fremdfahrzeug = -1;
        public const Int32 const_cbFahrzeugValue_Waggon = -2;
        public const Int32 const_cbFahrzeugValue_Schiff = -3;
        public const string const_LabelCallId_Text = "[Abruf-Id]: #";

        public const string const_cbFahrzeugText_Fremdfahrzeug = "--Fremdfahrzeug--";
        public const string const_cbFahrzeugText_Waggon = "--Bahn/Waggon--";
        public const string const_cbFahrzeugText_Schiff = "--Schiff--";

        const string const_TabArtikel_PageCall = "tabPageManCall";
        const string const_TabArtikel_PageArtikelExtraCharge = "tabPageArtikelExtraCharge";
        const string const_TabArtikel_PageArtikelPreise = "tabPageArtikelPreis";
        const string const_TabArtikel_PageExtraCharge = "tabPageExtraCharge";
        const string const_TabArtikel_PageSchaden = "tabPageSchaden";
        const string const_TabArtikel_PageArtikelVita = "tabPageVita";
        const string const_TabArtikel_PageArtikelZusatz = "tabPageZusatz";
        const string const_TabArtikel_PageImages = "tabPageImages";

        internal Common.Enumerations.enumDokumentenArt eDokumentenArt = enumDokumentenArt.DEFAULT;
        internal enumIniDocKey eDocKey = enumIniDocKey.Default;

        internal Globals._GL_USER GL_User;
        internal Globals._GL_SYSTEM GL_System;
        public frmTmp _frmTmp;
        public ctrMenu _ctrMenu = null;
        internal ctrADRManAdd _ctrADRManAdd;
        internal clsASNTransfer AsnTransfer;
        public Int32 SearchButton = 0;
        internal clsLager Lager = new clsLager();
        //internal DataTable dtMandanten;
        internal DataTable dtArtikel;
        internal DataTable dtSchaden;
        internal bool bUpdate = false;
        internal bool bUpdateArtikel = false;
        internal bool bUpdateCall = false;
        internal bool bBack = false;
        internal DateTime EDatum = DateTime.Today.Date;
        internal delegate void ThreadCtrInvokeEventHandler();
        internal decimal _LVSNr = 0;

        internal decimal _SelectedArtikelID = 0;
        internal decimal _MandantenID = 0;
        internal decimal _decGArtID = 0;
        internal string _MandantenName = string.Empty;
        internal string _lTextGArtID = "Güterart ID: #";
        internal bool _bLEingangIsChecked;
        internal bool _bArtikelIsChecked;
        public decimal _ArtikelIDTakeOverUB;
        internal bool _ISChangeAfterCheck = false;
        internal bool _bDirectDelivery = false;
        public bool _bArtPrint = false;
        internal string _ArtIDRef = string.Empty;
        internal string manAdrArt = string.Empty;
        public bool bSetExtraChargeAssignmentToArt = true;   //Unterscheidungsflag für Eingang bzw. nur Artikel
        internal decimal lastprintID;
        public string BackToCtr = string.Empty;

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

        BackgroundWorker worker;
        BackgroundWorker printWorker;

        ///<summary>ctrEinlagerung / ctrEinlagerung</summary>
        ///<remarks></remarks>
        public ctrEinlagerung()
        {
            InitializeComponent();

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted +=
                    new RunWorkerCompletedEventHandler(worker_CompleteWork);
            printWorker = new BackgroundWorker();
            printWorker.WorkerReportsProgress = true;
            printWorker.WorkerSupportsCancellation = true;
            printWorker.DoWork += new DoWorkEventHandler(printWorker_DoWork);
            printWorker.RunWorkerCompleted +=
                    new RunWorkerCompletedEventHandler(printWorker_CompleteWork);

            //Images 
            pbRL.Image = null;
            pbSchaden.Image = null;
            pbSPL.Image = null;

            //Tag für die Button setzen
            this.btnManVersender.Tag = 1;
            this.btnManEmpfaenger.Tag = 3;
            this.btnManEntladestelle.Tag = 4;
            this.btnManSped.Tag = 5;

            pbSPL.ContextMenuStrip = new ContextMenuStrip();
            pbSchaden.ContextMenuStrip = new ContextMenuStrip();
        }
        ///<summary>ctrEinlagerung / ctrEinlagerung_Load</summary>
        ///<remarks></remarks>
        private void ctrEinlagerung_Load(object sender, EventArgs e)
        {
            this.GL_User = this._ctrMenu._frmMain.GL_User;
            this.GL_System = this._ctrMenu._frmMain.GL_System;
            CustomerSettings();
        }
        ///<summary>ctrEinlagerung / CustomerSettings</summary>
        ///<remarks>Hier kann den Kundenwünschen entsprechend ein / mehere Elemente 
        ///         ein-/ausgeblendet werden</remarks>
        private void CustomerSettings()
        {
            //Anpassung Güterarten/Wargengruppen
            if (!this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinition)
            {
                this.btnGArt.Text = "[Warengruppe]";
                _lTextGArtID = "WarengruppeID #";
                SetLabelGArdIDInfo();
            }
            //Module / Buttons
            // --- Adress Eingabefelder
            clsClient.ctrEinlagerung_CustomizeEingangAdrDatenInputFieldsEnabled(this._ctrMenu._frmMain.system.Client.MatchCode, ref this.btnVersender, ref this.btnManVersender, this.tbSearchV);
            clsClient.ctrEinlagerung_CustomizeEingangAdrDatenInputFieldsEnabled(this._ctrMenu._frmMain.system.Client.MatchCode, ref this.btnSearchE, ref this.btnManEmpfaenger, this.tbSearchE);
            clsClient.ctrEinlagerung_CustomizeEingangAdrDatenInputFieldsEnabled(this._ctrMenu._frmMain.system.Client.MatchCode, ref this.btnSearchES, ref this.btnManEntladestelle, this.tbSearchES);

            //...|Retoure-Rebooking
            this.tsbtnCreateRetoureEingang.Visible = this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_RetourBooking;

            //...|DirectDelivery
            this.tsbtnDirect.Visible = this._ctrMenu._frmMain.system.Client.Modul.Lager_DirectDelivery;
            this.tsbtnDirectAbschluss.Visible = this._ctrMenu._frmMain.system.Client.Modul.Lager_DirectDelivery;

            //...|DirectDeliveryTransformation
            this.tsbtnDirectTransformation.Visible = this._ctrMenu._frmMain.system.Client.Modul.Lager_DirectDeliveryTransformation;

            //...|Sonderkosten
            tsbtnEingangExtraChargeAssignmentOpen.Visible = this._ctrMenu._frmMain.GL_System.Modul_Fakt_Sonderkosten; ;
            if (!this._ctrMenu._frmMain.GL_System.Modul_Fakt_Sonderkosten)
            {
                Functions.HideTabPage(ref tabArtikel, "tabPageExtraCharge");
            }
            //...|CheckComplete
            this.tsbtnCheckComplete.Visible = this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_CheckComplete;
            //...|EIngangChange
            //this.tsbtnChangeEingang.Visible = this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_EditAfterClose && !(this._ctrMenu._frmMain.system.Client.Modul.ASNTransfer);
            this._ctrMenu._frmMain.system.Client.ctrEinlagerung_CustomizedSetLabel_tsbtnChangeEinlagerung(ref this.tsbtnChangeEingang);

            //...|ExtragCharge 
            //Extragkosten Buttton für kompletten Eingang und der TabExtragCharge wird ausgeblendet
            bool bExCharge = (this.GL_User.read_FaktExtraCharge) && (this.GL_User.write_FaktExtraCharge);
            this.tsbtnEingangExtraChargeAssignmentOpen.Visible = bExCharge;
            if (!bExCharge)
            {
                Functions.HideTabPage(ref tabArtikel, tabPageExtraCharge.Name);
            }
            // da noch Baustelle ausblenden
            Functions.HideTabPage(ref tabArtikel, tabPageArtikelPreis.Name);

            //Artikelmenü
            this.tsbtnLagerort.Visible = this._ctrMenu._frmMain.system.Client.Modul.Menu_Einlagerung_Artikel_tsbtnLagerort;
            this.tbWerk.Enabled = this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_LagerOrt_Enabled_Werk;
            this.tbHalle.Enabled = this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_LagerOrt_Enabled_Halle;
            this.tbReihe.Enabled = this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_LagerOrt_Enabled_Reihe;
            this.tbEbene.Enabled = this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_LagerOrt_Enabled_Ebene;
            this.tbPlatz.Enabled = this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_LagerOrt_Enabled_Platz;
            this.tbExTransportRef.ReadOnly = !this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_EditExTransportRef;
            this.tsbtnCheckAll.Visible = this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_CheckAllArtikel;
            this.tsbtnArtKorrStorVerfahren.Visible = ((this._ctrMenu._frmMain.system.Client.Modul.Lager_Artikel_UseKorreturStornierVerfahren) && (this.GL_User.access_StKV));
            this.tsbtnArtikelRL.Visible = this._ctrMenu._frmMain.system.Client.Modul.Lager_Artikel_UseKorreturStornierVerfahren;
            this.cbEinheit.Enabled = this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_Enabeled_Einheit;

            //Eingabefelder deaktivieren
            //this._ctrMenu._frmMain.system.Client.ctrEinlagerung_CustomizeSetInputFieldsReadOnly(ref tbGArtZusatz);
            //this._ctrMenu._frmMain.system.Client.ctrEinlagerung_CustomizeSetInputFieldsReadOnly(ref tbPos);
            //this._ctrMenu._frmMain.system.Client.ctrEinlagerung_CustomizeSetInputFieldsReadOnly(ref tbexMaterialnummer);
            //this._ctrMenu._frmMain.system.Client.ctrEinlagerung_CustomizeSetInputFieldsReadOnly(ref tbexBezeichnung);
            //this._ctrMenu._frmMain.system.Client.ctrEinlagerung_CustomizeSetInputFieldsReadOnly(ref tbTransportId);
            //this._ctrMenu._frmMain.system.Client.ctrEinlagerung_CustomizeSetInputFieldsReadOnly(ref tbHoehe);
            //this._ctrMenu._frmMain.system.Client.ctrEinlagerung_CustomizeSetInputFieldsReadOnly(ref tbPackmittelGewicht);
            //this._ctrMenu._frmMain.system.Client.ctrEinlagerung_CustomizeSetInputFieldsReadOnly(ref tbExLagerOrt);
            //this._ctrMenu._frmMain.system.Client.ctrEinlagerung_CustomizeSetInputFieldsReadOnly(ref tbExAuftrag);


            tbGArtZusatz.ReadOnly = this._ctrMenu._frmMain.system.Client.ctrEinlagerung_CustomizeSetInputFieldsReadOnly(tbGArtZusatz.Name);
            tbPos.ReadOnly = this._ctrMenu._frmMain.system.Client.ctrEinlagerung_CustomizeSetInputFieldsReadOnly(tbPos.Name);
            tbexMaterialnummer.ReadOnly = this._ctrMenu._frmMain.system.Client.ctrEinlagerung_CustomizeSetInputFieldsReadOnly(tbexMaterialnummer.Name);
            tbexBezeichnung.ReadOnly = this._ctrMenu._frmMain.system.Client.ctrEinlagerung_CustomizeSetInputFieldsReadOnly(tbexBezeichnung.Name);
            tbTransportId.ReadOnly = this._ctrMenu._frmMain.system.Client.ctrEinlagerung_CustomizeSetInputFieldsReadOnly(tbTransportId.Name);
            tbHoehe.ReadOnly = this._ctrMenu._frmMain.system.Client.ctrEinlagerung_CustomizeSetInputFieldsReadOnly(tbHoehe.Name);
            tbHoehe.ReadOnly = this._ctrMenu._frmMain.system.Client.ctrEinlagerung_CustomizeSetInputFieldsReadOnly(tbHoehe.Name);
            tbPackmittelGewicht.ReadOnly = this._ctrMenu._frmMain.system.Client.ctrEinlagerung_CustomizeSetInputFieldsReadOnly(tbPackmittelGewicht.Name);
            tbExLagerOrt.ReadOnly = this._ctrMenu._frmMain.system.Client.ctrEinlagerung_CustomizeSetInputFieldsReadOnly(tbExLagerOrt.Name);
            tbExAuftrag.ReadOnly = this._ctrMenu._frmMain.system.Client.ctrEinlagerung_CustomizeSetInputFieldsReadOnly(tbExAuftrag.Name);

            //this._ctrMenu._frmMain.system.Client.ctrEinlagerung_CustomizeSetInputFieldsReadOnly(ref tbProduktionsNr);

            //tbexMaterialnummer.ReadOnly = true;
            //tbexBezeichnung.BackColor = System.Drawing.SystemColors.Control;

            //Label Länge           
            this._ctrMenu._frmMain.system.Client.ctrEinlagerung_CustomizedSetLabel_lLaenge_Text(ref lLaenge);
            this._ctrMenu._frmMain.system.Client.ctrEinlagerung_CustomizedSetLabel_lWerksnummer_Text(ref lWerksnummer);

            //--- ComboFahrzeuge 
            this._ctrMenu._frmMain.system.Client.ctrEinlagerung_CustomizedComboFahrzeugeSetStartValue_ComboFahrzeuge(ref this.cbFahrzeug);

        }
        ///<summary>ctrEinlagerung / frmLager_Load</summary>
        ///<remarks></remarks>
        public void InitCtrEinlagerung()
        {
            this.GL_System = this._ctrMenu._frmMain.GL_System;
            this.GL_User = this._ctrMenu._frmMain.GL_User;
            this._MandantenID = this.GL_System.sys_MandantenID;
            dtArtikel = new DataTable();

            //Combo Fahrzeuge
            Functions.InitComboFahrzeuge(this.GL_User, ref cbFahrzeug);

            //Einheit
            cbEinheit.DataSource = clsEinheiten.GetEinheiten(this.GL_User);
            cbEinheit.DisplayMember = "Bezeichnung";
            cbEinheit.ValueMember = "Bezeichnung";
            Functions.SetComboToSelecetedValue(ref cbEinheit, this._ctrMenu._frmMain.system.Client.Eingang_Artikel_DefaulEinheit);

            Functions.InitComboViews(_ctrMenu._frmMain.GL_System, ref tscbView, "LEingang");

            //decimal decTmpLastEingangId = (decimal)clsLEingang.GetLastEingangIdArbeitsbereich(this._ctrMenu._frmMain.system);
            Lager.InitClass(this.GL_User, this.GL_System, this._ctrMenu._frmMain.system);
            //Lager.sys = this._ctrMenu._frmMain.system;
            //Lager.AbBereichID = Lager.sys.AbBereich.ID;

            EingangBrowse(0, 0, enumBrowseAcivity.LastItem);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myEingangId"></param>
        /// <param name="myArtikelId"></param>
        public void EingangBrowse(decimal myEingangId, decimal myArtikelId, enumBrowseAcivity myBrowseAcitvity)
        {
            try
            {
                decimal decTmpId = 0;
                switch (myBrowseAcitvity)
                {
                    case enumBrowseAcivity.FirstItem:
                        Lager.FillLagerDatenByEA(this.Lager.FirstLEingangTableIDByArbeitsbereich, 0);
                        EingangBrowse(this.Lager.LEingangTableID, 0, enumBrowseAcivity.NoSelect);
                        break;

                    case enumBrowseAcivity.LastItem:
                        Lager.FillLagerDatenByEA(this.Lager.LastLEingangTableIDByArbeitsbereich, 0);
                        EingangBrowse(this.Lager.LEingangTableID, 0, enumBrowseAcivity.NoSelect);
                        break;

                    case enumBrowseAcivity.BackItem:
                        decTmpId = 0;
                        decTmpId = this.Lager.ForwardLEingangTableIDByArbeitsbereich;
                        if (decTmpId < 1)
                        {
                            EingangBrowse(0, 0, enumBrowseAcivity.NoSelect);
                        }
                        else
                        {
                            Lager.FillLagerDatenByEA(this.Lager.ForwardLEingangTableIDByArbeitsbereich, 0);
                            EingangBrowse(this.Lager.LEingangTableID, 0, enumBrowseAcivity.NoSelect);
                        }
                        break;

                    case enumBrowseAcivity.NextItem:
                        decTmpId = 0;
                        decTmpId = this.Lager.NextLEingangTableIDByArbeitsbereich;
                        if (decTmpId < 1)
                        {
                            EingangBrowse(0, 0, enumBrowseAcivity.NoSelect);
                        }
                        else
                        {
                            Lager.FillLagerDatenByEA(decTmpId, 0);
                            EingangBrowse(this.Lager.LEingangTableID, 0, enumBrowseAcivity.NoSelect);
                        }
                        break;

                    case enumBrowseAcivity.ArtInItem:
                        if (myArtikelId > 0)
                        {
                            this.Lager.Artikel.ID = myArtikelId;
                            this.Lager.Artikel.GetArtikeldatenByTableID();
                            EingangBrowse(0, this.Lager.Artikel.ID, enumBrowseAcivity.NoSelect);
                        }
                        else
                        {
                            EingangBrowse(myEingangId, 0, enumBrowseAcivity.Item);
                        }
                        break;

                    case enumBrowseAcivity.ArtItem:
                        this.Lager.FillLagerDatenByArtikelId(myArtikelId);
                        EingangBrowse(this.Lager.LEingangTableID, this.Lager.Artikel.ID, enumBrowseAcivity.NoSelect);
                        break;

                    case enumBrowseAcivity.Item:
                        Lager.FillLagerDatenByEA(myEingangId, 0);
                        EingangBrowse(this.Lager.LEingangTableID, 0, enumBrowseAcivity.NoSelect);
                        break;

                    case enumBrowseAcivity.NoSelect:
                        //---Eingangsdaten werden gesetzt
                        if (myEingangId > 0)
                        {
                            //Button LEingang Speichern
                            tsbtnEinlagerungSpeichern.Enabled = false;
                            tsbtnDeleteLEingang.Enabled = false;
                            ClearEingangsuebersicht();
                            SetLagerDatenToFrm();
                            decimal decTmp = -1;
                            if (!decimal.TryParse(tbLEingangID.Text, out decTmp))
                            {
                                gboxLEDaten.Enabled = false & (this.Lager.Eingang.LockedBy == 0 || this.Lager.Eingang.LockedBy == this.GL_User.User_ID);
                            }
                            else
                            {
                                gboxLEDaten.Enabled = true & (this.Lager.Eingang.LockedBy == 0 || this.Lager.Eingang.LockedBy == this.GL_User.User_ID);
                            }
                            SetLEingangCheck();
                            SetEingangLocked();

                            if (this.Lager.Eingang.dtArtInLEingang.Rows.Count > 0)
                            {
                                if (myArtikelId < 1)
                                {
                                    this.Lager.Artikel = this.Lager.Eingang.GetTopArtikelIDByLEingang();
                                    myArtikelId = this.Lager.Artikel.ID;
                                }
                            }
                        }

                        ClearArtikelEingabeFelder(true);
                        bool bEingabeFelderEnabled = false;
                        InitDGV();

                        //---Artikeldaten werden gesetzt
                        if (myArtikelId > 0)
                        {
                            SetArtikelToForm(myArtikelId, false);
                            bEingabeFelderEnabled = (!this.Lager.Artikel.EingangChecked);
                        }
                        //Artikeleingabefelder deaktivieren
                        SetArtikelEingabefelderDatenEnable(bEingabeFelderEnabled);
                        SetArtikelMenuBtnEnabled();

                        this.tabArtikel.SelectedTab = this.tabPageZusatz;
                        InitSelectedTabPage(this.tabPageZusatz.Name.ToString());
                        break;
                }
            }
            catch (Exception ex)
            {
                string strError = ex.ToString();
            }
        }
        ///<summary>ctrEinlagerung / worker_DoWork</summary>
        ///<remarks></remarks>
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            AsnTransfer = new clsASNTransfer();
            if (AsnTransfer.DoASNTransfer(this.GL_System, this.Lager.Eingang.AbBereichID, this.Lager.Eingang.MandantenID))
            {
                if (this._ctrMenu._frmMain.system.Client.Modul.ASN_UserOldASNFileCreation)
                {
                    AsnTransfer.CreateLM(ref this.Lager);
                }
                else
                {
                    AsnTransfer.CreateLM_Eingang(ref this.Lager);
                }
            }
        }
        ///<summary>ctrEinlagerung / worker_CompleteWork</summary>
        ///<remarks></remarks>
        void printWorker_CompleteWork(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine(e.ToString());
            InitDGV(true);
        }
        ///<summary>ctrEinlagerung / worker_DoWork</summary>
        ///<remarks></remarks>
        void printWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
            {
                lastprintID = this.Lager.Eingang.LEingangTableID;
                DirectPrintLEingangDoc();
                DirectPrintList();
            }
            else
            {
                //--- .Modul.Lager_Einlagerung_Print_DirectEingangDoc
                if (DoDirectPrintReport(enumIniDocKey.EingangDoc))
                {
                    this.Lager.Eingang.UpdatePrintLEingang(enumDokumentenArt.LagerEingangDoc.ToString());
                }

                //--- Print Modul.Lager_Einlagerung_Print_DirectList
                if (DoDirectPrintReport(enumIniDocKey.Eingangsliste))
                {
                    this.Lager.Eingang.UpdatePrintLEingang(enumIniDocKey.Eingangsliste.ToString());
                }
            }
            CheckDirectPrintLabel();
        }
        ///<summary>ctrEinlagerung / worker_CompleteWork</summary>
        ///<remarks></remarks>
        void worker_CompleteWork(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine(e.ToString());
            InitDGV(true);
        }
        ///<summary>ctrEinlagerung / tabArtikel_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void tabArtikel_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitSelectedTabPage(tabArtikel.SelectedTab.Name.ToString());
        }
        ///<summary>ctrEinlagerung / InitSelectedTabPage</summary>
        ///<remarks></remarks>
        private void InitSelectedTabPage(string myPageName)
        {
            //pbox leeren
            this.pbImageThumb.Image = null;
            switch (myPageName)
            {
                case const_TabArtikel_PageArtikelExtraCharge:

                    break;

                case const_TabArtikel_PageArtikelPreise:

                    break;

                case const_TabArtikel_PageExtraCharge:
                    InitDGVExtraChargeAssignment();
                    break;

                case const_TabArtikel_PageSchaden:
                    //Set Menü Buttons
                    SetMenuArtikelSchadenEnabled();
                    InitDGVSchaden();
                    break;

                case const_TabArtikel_PageArtikelVita:
                    InitGrdArtVita();
                    break;

                case const_TabArtikel_PageArtikelZusatz:
                    break;

                case const_TabArtikel_PageImages:
                    tstbMaxSPLMes.Text = this._ctrMenu._frmMain.system.VE_MaxImageCountSPLMessage.ToString();
                    SetMenuArtikelImagesEnabled();
                    InitLVArtImages();
                    break;

                case const_TabArtikel_PageCall:
                    InitTabPageCall();
                    break;
            }
        }
        /*********************************************************************************************
         *                              Methoden für Eingangsdaten / Eingangskopf
         * ******************************************************************************************/
        ///<summary>ctrEinlagerung / tsbtnDeleteLEingang_Click</summary>
        ///<remarks>Der angezeigte, nicht abgeschlossene Lagereingang wird gelöscht.</remarks>  
        private void tsbtnDeleteLEingang_Click(object sender, EventArgs e)
        {
            if (!_bLEingangIsChecked)
            {
                if (GL_User.write_LagerEingang)
                {
                    if (this._ctrMenu._frmMain.system.Client.Modul.Lager_AskForDeleteEA && clsMessages.Lager_Delete())
                    {
                        if (Lager.Eingang.ExistLEingangTableID())
                        {
                            Lager.Eingang.DeleteLEingangByLEingangTableID();
                            EingangBrowse(0, 0, enumBrowseAcivity.LastItem);
                        }
                    }
                }
                else
                {
                    clsMessages.User_NoAuthen();
                }
            }
        }
        ///<summary>ctrEinlagerung / tsbtnClose_Click</summary>
        ///<remarks>Schließt die Einlagerungsform.</remarks>  
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            clsLEingang.unlockEingang(this.GL_User.User_ID);
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
                //this._ctrMenu.CloseCtrEinlagerung();
                this.Dispose();
            }
        }
        ///<summary>ctrEinlagerung / InitEingabe</summary>
        ///<remarks>Form wird für die Eingabe von Daten vorbereite.</remarks>
        private void InitEingabe()
        {
            //aktivieren der Eingabefelder
            SetLagerEingangsFelderEnabled(true, false);
            ClearEingangsuebersicht();
            //Defaultwert in die Eingabefelder setzen
            SetLEingangsFelderToDefault();
            if (!bUpdate)
            {
                //Ermitteln der neuen EingangsID
                SetLEingangsID();
            }
            //Eingang abgeschlossen?
            SetLEingangCheck();
        }
        ///<summary>ctrEinlagerung / SetEingabefelderDatenEnable</summary>
        ///<remarks>Gibt die Eingabefelder für einen neuen Eingang frei.</remarks>
        private void SetLEingangsFelderToDefault()
        {
            dtpEinlagerungDate.Value = DateTime.Now;
            tbLEingangID.Text = "0";
            tbDFUE.Text = "0";
            tbEArtAnzahl.Text = "0";
            tbBruttoGesamt.Text = "0";
            tbNettoGesamt.Text = "0";
            tbLieferantenID.Text = "0";
            tbLfsNr.Text = string.Empty;
            tbFahrer.Text = string.Empty;
            tbMCSpedition.Text = string.Empty;
            tbADRSpedition.Text = string.Empty;
            mtbKFZ.Text = string.Empty;
            cbFahrzeug.SelectedIndex = -1;
            tbLocked.Visible = false;
            tbLockedBy.Text = string.Empty;

            //combo
            this._ctrMenu._frmMain.system.Client.ctrEinlagerung_CustomizedComboFahrzeugeSetStartValue_ComboFahrzeuge(ref this.cbFahrzeug);
        }
        ///<summary>ctrEinlagerung / btnSearchA_Click</summary>
        ///<remarks>Pffnet die ADR-From zur Adresssuche.</remarks>
        private void btnSearchA_Click(object sender, EventArgs e)
        {
            SearchButton = 1;
            _ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrEinlagerung / btnSearchV_Click</summary>
        ///<remarks>Pffnet die ADR-From zur Adresssuche.</remarks>
        private void btnSearchV_Click(object sender, EventArgs e)
        {
            SearchButton = 2;
            _ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrEinlagerung / btnSearchE_Click</summary>
        ///<remarks>Pffnet die ADR-From zur Adresssuche.</remarks>
        private void btnSearchE_Click(object sender, EventArgs e)
        {
            SearchButton = 3;
            _ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrEinlagerung / btnSearchES_Click</summary>
        ///<remarks>.</remarks>
        private void btnSearchES_Click(object sender, EventArgs e)
        {
            SearchButton = 12;
            _ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrEinlagerung / btnSpedition_Click</summary>
        ///<remarks>.</remarks>
        private void btnSpedition_Click(object sender, EventArgs e)
        {
            //_ADRSearch = "Spedition";
            SearchButton = 7;
            this._ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrEinlagerung / tbSearchA_TextChanged</summary>
        ///<remarks>Auftraggeber Adresssuche</remarks>
        private void tbSearchA_TextChanged(object sender, EventArgs e)
        {
            //Adressdaten laden
            DataTable dt = new DataTable();
            dt = clsADR.GetADRList(this.GL_User.User_ID);
            DataTable dtTmp = new DataTable();

            string SearchText = tbSearchA.Text.ToString();
            string Ausgabe = string.Empty;

            DataRow[] rows = dt.Select("Suchbegriff LIKE '" + SearchText + "'", "Suchbegriff");
            dtTmp = dt.Clone();
            foreach (DataRow row in rows)
            {
                Ausgabe = Ausgabe + row["Suchbegriff"].ToString() + "\n";
                dtTmp.ImportRow(row);
            }
            //tbAuftraggeber.Text = Functions.GetADRStringFromTable(dtTmp);
            if (dtTmp.Rows.Count > 0)
            {
                //Lager.Eingang.Auftraggeber
                decimal decTmp = Functions.GetADR_IDFromTable(dtTmp);
                this.SearchButton = 1;
                SetKDRecAfterADRSearch(decTmp, false);
            }
            else
            {

                if (this._ctrMenu._frmMain.system.AbBereich.ASNTransfer)
                {
                    if (!this.Lager.Eingang.Checked)
                    {
                        tbLieferantenID.Text = string.Empty;
                        //Lager.Eingang.Lieferant = string.Empty;
                    }
                }
                else
                { }
                tbAuftraggeber.Text = string.Empty;
            }
        }
        ///<summary>ctrEinlagerung / tbSearchV_TextChanged</summary>
        ///<remarks>Versender Adresssuche</remarks>
        private void tbSearchV_TextChanged(object sender, EventArgs e)
        {
            //Adressdaten laden
            DataTable dt = new DataTable();
            dt = clsADR.GetADRList(this.GL_User.User_ID);
            DataTable dtTmp = new DataTable();

            string SearchText = tbSearchV.Text.ToString();
            string Ausgabe = "";

            DataRow[] rows = dt.Select("Suchbegriff LIKE '" + SearchText + "'", "Suchbegriff");
            dtTmp = dt.Clone();

            foreach (DataRow row in rows)
            {
                Ausgabe = Ausgabe + row["Suchbegriff"].ToString() + "\n";
                dtTmp.ImportRow(row);
            }
            //tbVersender.Text = Functions.GetADRStringFromTable(dtTmp);
            if (dtTmp.Rows.Count > 0)
            {
                //Lager.Eingang.Versender = Functions.GetADR_IDFromTable(dtTmp);
                decimal decTmp = Functions.GetADR_IDFromTable(dtTmp);

                //SearchButton
                // 1 = Auftraggeber
                // 2 = Versender
                // 3 = Empfänger
                // 4 = neutrale Versandadresse
                // 5 = neutrale Empfangsadresse
                // 6 = Mandanten
                // 7 = Spedition
                // 8 = Kunden
                // 9 = neutraler Auftraggeber
                // 10 = Rechnungsadresse
                // 11 = Postadresse
                // 12 = Entladestelle
                // 13 = Beladestelle
                // 14 = AbrufEmpfänger
                // 15 = AbrufSpedition
                // 16 = AbrufEntladestelle
                SearchButton = 2;
                SetKDRecAfterADRSearch(decTmp, false);
            }
            else
            {
                tbVersender.Text = string.Empty;
            }
        }
        ///<summary>ctrEinlagerung / tbMCSpedition_TextChanged</summary>
        ///<remarks>Spedition Adresssuche</remarks>
        private void tbMCSpedition_TextChanged(object sender, EventArgs e)
        {
            //Adressdaten laden
            DataTable dt = new DataTable();
            dt = clsADR.GetADRList(this.GL_User.User_ID);
            DataTable dtTmp = new DataTable();

            string SearchText = tbMCSpedition.Text.ToString();
            string Ausgabe = "";

            DataRow[] rows = dt.Select("Suchbegriff LIKE '" + SearchText + "'", "Suchbegriff");
            dtTmp = dt.Clone();

            foreach (DataRow row in rows)
            {
                Ausgabe = Ausgabe + row["Suchbegriff"].ToString() + "\n";
                dtTmp.ImportRow(row);
            }
            if (dtTmp.Rows.Count > 0)
            {
                //Lager.Eingang.Versender = Functions.GetADR_IDFromTable(dtTmp);
                decimal decTmp = Functions.GetADR_IDFromTable(dtTmp);

                //SearchButton
                // 1 = Auftraggeber
                // 2 = Versender
                // 3 = Empfänger
                // 4 = neutrale Versandadresse
                // 5 = neutrale Empfangsadresse
                // 6 = Mandanten
                // 7 = Spedition
                // 8 = Kunden
                // 9 = neutraler Auftraggeber
                // 10 = Rechnungsadresse
                // 11 = Postadresse
                // 12 = Entladestelle
                // 13 = Beladestelle
                // 14 = AbrufEmpfänger
                // 15 = AbrufSpedition
                // 16 = AbrufEntladestelle

                SearchButton = 7;
                SetKDRecAfterADRSearch(decTmp, false);
            }
            else
            {
                tbMCSpedition.Text = string.Empty;
            }
        }
        ///<summary>ctrEinlagerung / tbSearchE_TextChanged</summary>
        ///<remarks>Empfänger Adresssuche</remarks>
        private void tbSearchE_TextChanged(object sender, EventArgs e)
        {
            //Adressdaten laden
            DataTable dt = new DataTable();
            dt = clsADR.GetADRList(this.GL_User.User_ID);
            DataTable dtTmp = new DataTable();

            //Suchtext
            string SearchText = tbSearchE.Text.ToString();
            string Ausgabe = "";

            DataRow[] rows = dt.Select("Suchbegriff LIKE '" + SearchText + "'", "Suchbegriff");
            dtTmp = dt.Clone();

            foreach (DataRow row in rows)
            {
                Ausgabe = Ausgabe + row["Suchbegriff"].ToString() + "\n";
                dtTmp.ImportRow(row);
            }
            //tbEmpfaenger.Text = Functions.GetADRStringFromTable(dtTmp);
            if (dtTmp.Rows.Count > 0)
            {
                decimal decTmp = Functions.GetADR_IDFromTable(dtTmp);
                SearchButton = 3;
                SetKDRecAfterADRSearch(decTmp, false);
            }
            else
            {
                //Check 
                if (this._ctrMenu._frmMain.system.AbBereich.ASNTransfer)
                {
                    if (!this.Lager.Eingang.Checked)
                    {
                        tbLieferantenID.Text = string.Empty;
                        //Lager.Eingang.Lieferant = string.Empty;
                    }
                }
                else
                { }
                tbEmpfaenger.Text = string.Empty;
            }
        }
        ///<summary>ctrEinlagerung / SetKDRecAfterADRSearch</summary>
        ///<remarks>Ermittelt anhander der übergebenen Adresse ID die Adresse und setzt diese in die From.</remarks>
        ///<param name="ADR_ID">ADR_ID</param>
        public void SetKDRecAfterADRSearch(decimal myDecADR_ID, bool myIsCallBack)
        {
            string strE = string.Empty;
            string strMC = string.Empty;
            clsADR tmpAdr = new clsADR();
            tmpAdr.InitClass(this.GL_User, this.GL_System, myDecADR_ID, true);
            strMC = tmpAdr.ViewID;
            strE = tmpAdr.ADRStringShortWithID;

            //SearchButton
            // 1 = Auftraggeber
            // 2 = Versender
            // 3 = Empfänger
            // 4 = neutrale Versandadresse
            // 5 = neutrale Empfangsadresse
            // 6 = Mandanten
            // 7 = Spedition
            // 8 = Kunden
            // 9 = neutraler Auftraggeber
            // 10 = Rechnungsadresse
            // 11 = Postadresse
            // 12 = Entladestelle
            // 13 = Beladestelle
            // 14 = AbrufEmpfänger
            // 15 = AbrufSpedition
            // 16 = AbrufEntladestelle

            switch (SearchButton)
            {
                case 1:
                    if (this.Lager.Eingang.LEingangTableID < 1)
                    {
                        Lager.Eingang.Auftraggeber = myDecADR_ID;
                        tbSearchA.Text = strMC;
                        tbAuftraggeber.Text = strE;

                        clsClient.ctrEinlagerung_CustomizeDefaulEingangsdaten(ref this._ctrMenu._frmMain.system, ref Lager);

                        //-- default Entladestelle gesetzt
                        if (Lager.Eingang.EntladeID > 0)
                        {
                            this.SearchButton = 12;
                            SetKDRecAfterADRSearch(Lager.Eingang.EntladeID, true);
                        }
                        //-- default Empfänger gesetzt
                        if (Lager.Eingang.Empfaenger > 0)
                        {
                            //default Empfänger wurde gesetzt
                            this.SearchButton = 3;
                            SetKDRecAfterADRSearch(Lager.Eingang.Empfaenger, true);
                        }
                        //-- default Versender gesetzt
                        if (Lager.Eingang.Versender > 0)
                        {
                            this.SearchButton = 2;
                            SetKDRecAfterADRSearch(Lager.Eingang.Versender, true);
                        }
                        if (!myIsCallBack)
                        {
                            if ((Lager.Eingang.Auftraggeber > 0) && (Lager.Eingang.Empfaenger > 0))
                            {
                                Lager.Eingang.Lieferant = clsADRVerweis.GetSupplierNoBySenderAndReceiverAdr(Lager.Eingang.Auftraggeber,
                                                                                                            Lager.Eingang.Empfaenger,
                                                                                                            this.GL_User.User_ID,
                                                                                                            enumASNFileTyp.VDA4913.ToString(),
                                                                                                            this.GL_System.sys_ArbeitsbereichID);
                            }
                            else
                            {
                                Lager.Eingang.Lieferant = string.Empty;
                            }
                            this.tbLieferantenID.Text = this.Lager.Eingang.Lieferant;
                        }
                    }
                    else
                    {
                        if (!Lager.Eingang.Auftraggeber.Equals(myDecADR_ID))
                        {
                            Lager.Eingang.Auftraggeber = myDecADR_ID;
                            Lager.Eingang.Lieferant = clsADRVerweis.GetSupplierNoBySenderAndReceiverAdr(Lager.Eingang.Auftraggeber,
                                                                                Lager.Eingang.Empfaenger,
                                                                                this.GL_User.User_ID,
                                                                                enumASNFileTyp.VDA4913.ToString(),
                                                                                this.GL_System.sys_ArbeitsbereichID);

                        }
                        this.tbLieferantenID.Text = this.Lager.Eingang.Lieferant;
                        Lager.Eingang.Auftraggeber = myDecADR_ID;
                        tbSearchA.Text = strMC;
                        tbAuftraggeber.Text = strE;
                    }
                    break;

                case 2:
                    Lager.Eingang.Versender = myDecADR_ID;
                    tbSearchV.Text = strMC;
                    tbVersender.Text = strE;
                    clsADRMan.DeleteAllByTableIDAndAdrArtID(this.GL_User, "LEingang", this.Lager.Eingang.LEingangTableID, clsADRMan.cont_AdrArtID_Versender);
                    break;

                case 3:
                    if (this.Lager.Eingang.LEingangTableID < 1)
                    {
                        Lager.Eingang.Empfaenger = myDecADR_ID;
                        tbSearchE.Text = strMC;
                        tbEmpfaenger.Text = strE;
                        clsClient.ctrEinlagerung_CustomizeDefaulEingangsdaten(ref this._ctrMenu._frmMain.system, ref Lager);

                        //-- default Entladestelle gesetzt
                        if (Lager.Eingang.EntladeID > 0)
                        {
                            this.SearchButton = 12;
                            SetKDRecAfterADRSearch(Lager.Eingang.EntladeID, true);
                        }
                        if (!myIsCallBack)
                        {
                            if ((Lager.Eingang.Auftraggeber > 0) && (Lager.Eingang.Empfaenger > 0))
                            {
                                //Lager.Eingang.Lieferant = clsADRVerweis.GetSupplierNoBySenderAndReceiverAdr(Lager.Eingang.Auftraggeber,
                                //                                                                            Lager.Eingang.Empfaenger,
                                //                                                                            this.GL_User.User_ID,
                                //                                                                            enumASNFileTyp.VDA4913.ToString(),
                                //                                                                            this.GL_System.sys_ArbeitsbereichID);
                                Lager.Eingang.Lieferant = clsADRVerweis.GetSupplierNoBySenderAndReceiverAdr(Lager.Eingang);
                            }
                            else
                            {
                                Lager.Eingang.Lieferant = string.Empty;
                            }
                            this.tbLieferantenID.Text = this.Lager.Eingang.Lieferant;
                        }
                    }
                    else
                    {
                        // mr 2020_09_29
                        Lager.Eingang.Empfaenger = myDecADR_ID;
                        //Lager.Eingang.Lieferant = clsADRVerweis.GetSupplierNoBySenderAndReceiverAdr(Lager.Eingang.Auftraggeber,
                        //                                                    Lager.Eingang.Empfaenger,
                        //                                                    this.GL_User.User_ID,
                        //                                                    enumASNFileTyp.VDA4913.ToString(),
                        //                                                    this.GL_System.sys_ArbeitsbereichID);
                        if (Lager.Eingang.Lieferant.Equals(string.Empty))
                        {
                            Lager.Eingang.Lieferant = clsADRVerweis.GetSupplierNoBySenderAndReceiverAdr(Lager.Eingang);
                        }
                        this.tbLieferantenID.Text = this.Lager.Eingang.Lieferant;
                        if (!Lager.Eingang.Empfaenger.Equals(myDecADR_ID))
                        {
                            // mr 2020_09_29 verschoben nach oben
                            //Lager.Eingang.Empfaenger = myDecADR_ID;
                            //Lager.Eingang.Lieferant = clsADRVerweis.GetSupplierNoBySenderAndReceiverAdr(Lager.Eingang.Auftraggeber,
                            //                                                    Lager.Eingang.Empfaenger,
                            //                                                    this.GL_User.User_ID,
                            //                                                    enumASNFileTyp.VDA4913.ToString(),
                            //                                                    this.GL_System.sys_ArbeitsbereichID);
                            //this.tbLieferantenID.Text = this.Lager.Eingang.Lieferant;


                            clsADRMan.DeleteAllByTableIDAndAdrArtID(this.GL_User, "LEingang", this.Lager.Eingang.LEingangTableID, clsADRMan.cont_AdrArtID_Empfaenger);

                        }
                        Lager.Eingang.Empfaenger = myDecADR_ID;
                        if (!tbSearchE.Text.Equals(strMC))
                        {
                            tbSearchE.Text = strMC;
                        }
                        tbEmpfaenger.Text = strE;
                    }
                    break;

                case 7:
                    Lager.Eingang.SpedID = myDecADR_ID;
                    tbMCSpedition.Text = strMC;
                    tbADRSpedition.Text = strE;
                    //Alle entsprechenden Daten aus ADRMan löschen
                    clsADRMan.DeleteAllByTableIDAndAdrArtID(this.GL_User, "LEingang", this.Lager.Eingang.LEingangTableID, clsADRMan.cont_AdrArtID_Spedition);
                    break;

                case 12:
                    this.Lager.Eingang.EntladeID = myDecADR_ID;
                    tbSearchES.Text = strMC;
                    tbEntladestelle.Text = strE;
                    clsADRMan.DeleteAllByTableIDAndAdrArtID(this.GL_User, "LEingang", this.Lager.Eingang.LEingangTableID, clsADRMan.cont_AdrArtID_Entladeadresse);
                    break;

                case 14:
                    this.Lager.Artikel.Call.EmpAdrID = (Int32)myDecADR_ID;
                    this.tbCallSearchAdr.Text = strMC;
                    this.tbCallEmpfaenger.Text = strE;
                    break;
                case 15:
                    this.Lager.Artikel.Call.SpedAdrID = (Int32)myDecADR_ID;
                    this.tbCallSearchAdrSped.Text = strMC;
                    this.tbCallSpedition.Text = strE;
                    break;
                case 16:
                    this.Lager.Artikel.Call.LiefAdrID = (Int32)myDecADR_ID;
                    this.tbCallSearchESAdr.Text = strMC;
                    this.tbCallEntladestelle.Text = strE;
                    break;
            }


            //}
        }
        ///<summary>ctrEinlagerung / SetKDRecAfterADRSearch</summary>
        ///<remarks>Ermittelt anhander der übergebenen Adresse ID die Adresse und setzt diese in die From.</remarks>
        private void tsbtnEinlagerungSpeichern_Click(object sender, EventArgs e)
        {
            SaveEingang();
        }
        ///<summary>ctrEinlagerung / SaveEingang</summary>
        ///<remarks></remarks>
        private bool SaveEingang()
        {
            bool bSave = true;
            //Check der Pflichtfelder
            string strInfo = string.Empty;
            /************************************************************************************
             *               CHeck Eingabefelder EIngangskopf
             * ********************************************************************************/

            //Auftraggeber
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_RequiredValue_Auftraggeber)
            {
                if (this.Lager.Eingang.Auftraggeber < 1)
                {
                    strInfo = strInfo + "- Feld [Auftraggeber] " + Environment.NewLine;
                }
            }
            //VehicleDaten
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_RequiredValue_Vehicle)
            {
                Int32 iTmp = 0;
                if (cbFahrzeug.SelectedValue != null)
                {
                    Int32.TryParse(cbFahrzeug.SelectedValue.ToString(), out iTmp);
                }

                switch (iTmp)
                {
                    case ctrEinlagerung.const_cbFahrzeugValue_Schiff:
                        if (
                                (mtbKFZ.Text.Equals(string.Empty)) ||
                                (mtbKFZ.Text.Equals(ctrEinlagerung.const_cbFahrzeugText_Schiff))
                           )
                        {
                            strInfo = strInfo + "- Feld [Schiff] " + Environment.NewLine;
                        }
                        break;

                    case ctrEinlagerung.const_cbFahrzeugValue_Fremdfahrzeug:
                        if (
                                (mtbKFZ.Text.Equals(string.Empty)) ||
                                (mtbKFZ.Text.Equals(ctrEinlagerung.const_cbFahrzeugText_Fremdfahrzeug))
                           )
                        {
                            strInfo = strInfo + "- Feld [KFZ] " + Environment.NewLine;
                        }
                        break;
                    case ctrEinlagerung.const_cbFahrzeugValue_Waggon:
                        if (
                                (mtbKFZ.Text.Equals(string.Empty)) ||
                                (mtbKFZ.Text.Equals(ctrEinlagerung.const_cbFahrzeugText_Waggon))
                           )
                        {
                            strInfo = strInfo + "- Feld [Waggon] " + Environment.NewLine;
                        }
                        break;

                    default:
                        if (mtbKFZ.Text.Equals(string.Empty))
                        {
                            strInfo = strInfo + "- Feld [Waggon/KFZ/Schiff] " + Environment.NewLine;
                        }
                        break;
                }
            }
            //Lieferscheinnummer
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_RequiredValue_LieferscheinNr)
            {
                if (this.tbLfsNr.Text == string.Empty)
                {
                    strInfo = strInfo + "- Feld [Lieferscheinnummer] " + Environment.NewLine;
                }
            }

            if (strInfo == string.Empty)
            {
                SaveEingangDaten();
            }
            else
            {
                string strKopf = "Der Eingang kann nicht gespeichert werden, da folgende Pflichtfelder nicht ausgefüllt sind: " + Environment.NewLine;
                strInfo = strKopf + strInfo;
                clsMessages.Allgemein_ERRORTextShow(strInfo);
                bSave = false;
            }
            tbLEingangID.Text = Lager.Eingang.LEingangID.ToString();
            tbLEingangTableID.Text = "[" + Lager.Eingang.LEingangTableID.ToString() + "]";
            SetTsbtnEinlagerungSpeichernEnabled();
            return bSave;
        }
        ///<summary>ctrEinlagerung / SaveEingangDaten</summary>
        ///<remarks></remarks>
        private void SaveEingangDaten()
        {
            if (!_bLEingangIsChecked)
            {
                //Userberechtigung prüfen
                if (GL_User.write_LagerEingang)
                {
                    if (CheckLEingangkopfdaten())
                    {
                        Lager.Eingang.AbBereichID = this.GL_System.sys_ArbeitsbereichID;
                        Lager.Eingang.MandantenID = this._MandantenID;
                        decimal decTmp = 0;
                        Decimal.TryParse(tbLEingangID.Text, out decTmp);
                        Lager.Eingang.LEingangID = decTmp;
                        Lager.Eingang.LEingangDate = dtpEinlagerungDate.Value;
                        Lager.Eingang.LEingangLfsNr = tbLfsNr.Text.ToString().Trim();
                        Lager.Eingang.ExTransportRef = tbExTransportRef.Text.Trim();
                        Lager.Eingang.Fahrer = tbFahrer.Text.Trim();
                        Lager.Eingang.Lieferant = tbLieferantenID.Text.ToString().Trim();
                        Lager.Eingang.Checked = _bLEingangIsChecked;
                        Lager.Eingang.DirektDelivery = _bDirectDelivery;
                        Lager.Eingang.Retoure = cbRetoure.Checked;
                        Lager.Eingang.Vorfracht = cbVorfracht.Checked;
                        Lager.Eingang.Verlagerung = cbVerlagerung.Checked;
                        Lager.Eingang.LagerTransport = cbLagerTransport.Checked;
                        Lager.Eingang.Umbuchung = cbUmbuchung.Checked;
                        //Transporteinheit
                        string strKFZ = cbFahrzeug.Text.ToString();
                        //Lager.Eingang.SpedID = 0;
                        Lager.Eingang.KFZ = string.Empty;
                        Lager.Eingang.WaggonNr = string.Empty;
                        Lager.Eingang.IsWaggon = false;
                        Lager.Eingang.Ship = string.Empty;
                        Lager.Eingang.IsShip = false;

                        switch (strKFZ)
                        {
                            //Schiff
                            case ctrEinlagerung.const_cbFahrzeugText_Schiff:
                                Lager.Eingang.Ship = mtbKFZ.Text;
                                Lager.Eingang.IsShip = true;
                                break;

                            //case "--Bahn/Waggon--":
                            case ctrEinlagerung.const_cbFahrzeugText_Waggon:
                                Lager.Eingang.WaggonNr = mtbKFZ.Text;
                                Lager.Eingang.IsWaggon = true;
                                break;

                            //case "--Fremdfahrzeug--":
                            case ctrEinlagerung.const_cbFahrzeugText_Fremdfahrzeug:
                                Lager.Eingang.KFZ = mtbKFZ.Text;
                                break;

                            default:
                                //eigene Fahrzeuge
                                Lager.Eingang.KFZ = mtbKFZ.Text;
                                break;
                        }

                        if (Lager.Eingang.LEingangTableID > 0)
                        {
                            //Update Daten
                            if (!Lager.Eingang.ExistLEingangTableID())
                            {
                                Lager.Eingang.GetLEingangTableID();
                            }
                            Lager.Eingang.UpdateLagerEingang();
                        }
                        else
                        {
                            //Insert Daten
                            Lager.Eingang.AddLagerEingang();
                            Lager.LEingangTableID = Lager.Eingang.LEingangTableID;
                        }

                        // in FillLAgerDaten wird der Artikel auf 0 gesetzt, deshalb wir der letzte angezeigte Artikel
                        // in tmpSelectedArt zwischengespeichert und anschließend wieder LAger.Artikel zugewiesen
                        clsArtikel tmpSelectedArt = this.Lager.Artikel.Copy();
                        Lager.FillLagerDaten(true);
                        Lager.Artikel = tmpSelectedArt.Copy();
                        //EingangBrowse(0, this.Lager.Artikel.ID, enumBrowseAcivity.ArtInItem);
                        //Da die Daten jetzt sichtbar sind und geblättert werden kann muss nun Update = true gesetzt werden
                        bUpdate = true;
                        //Button ArtikelNeu aktivieren
                        tsbtnArtikelNeu.Enabled = true;
                    }
                    else
                    {
                        //TEMP CODE 
                        string a = "b";
                    }
                }
                else
                {
                    clsMessages.User_NoAuthen();
                }
            }
            InitSelectedTabPage(tabArtikel.SelectedTab.Name.ToString());

            //Wenn Direkanlieferung, dann nach dem Speichern der 
            //den Button Direktanlieferung abschließen aktivieren
            if (_bDirectDelivery)
            {
                tsbtnDirectAbschluss.Enabled = true;
            }
            //Arikelmenü neu aktivieren
            tsbtnNeueEinlagerung.Enabled = true;
        }
        ///<summary>ctrEinlagerung / SetKDRecAfterADRSearch</summary>
        ///<remarks>Ermittelt anhander der übergebenen Adresse ID die Adresse und setzt diese in die From.</remarks>
        ///<param name="ADR_ID">ADR_ID</param>
        private bool CheckLEingangkopfdaten()
        {
            bool EingabeOK = true;
            string strHelp = string.Empty;
            Int32 result = 0;
            decimal decResult = 0.0m;
            //Auftraggeber
            if (tbLEingangID.Text == string.Empty)
            {
                EingabeOK = false;
                strHelp = strHelp + "Eingangsnummer fehlt \n\r";
            }
            //Auftraggeber
            if (tbAuftraggeber.Text == string.Empty)
            {
                EingabeOK = false;
                strHelp = strHelp + "Auftraggeber fehlt \n\r";
            }
            //Lieferscheinnummer
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_RequiredValue_LieferscheinNr)
            {
                if (this.tbLfsNr.Text == string.Empty)
                {
                    EingabeOK = false;
                    strHelp = strHelp + "Lieferscheinnummer fehlt \n\r";
                }
            }
            if (!EingabeOK)
            {
                MessageBox.Show(strHelp, "Achtung");
            }
            return EingabeOK;
        }
        ///<summary>ctrEinlagerung / tsbtnNeueEinlagerung_Click</summary>
        ///<remarks>Init neue Einlagerung.</remarks>
        private void tsbtnNeueEinlagerung_Click(object sender, EventArgs e)
        {
            if (_MandantenID > 0)
            {
                //Direktanlieferung ausschalten
                SetLabelDirektanlieferung(false);
                _bDirectDelivery = false;

                Lager.LEingangTableID = 0;
                Lager.FillLagerDaten(true);
                _bLEingangIsChecked = false;
                _bArtikelIsChecked = false;
                _LVSNr = 0;
                bUpdate = false;

                //Eingabefelder leeren und neue LEingangsID wird ermittelt
                InitEingabe();
                //Artikelbereich Reset
                SetArtikelMenuBtnEnabled();
                //ArtikelEingabefelder und Table leeren
                ClearArtikelEingabeFelder(true);
                dtArtikel.Clear();
                SetArtikelEingabefelderDatenEnable(false);
                //ArtikelVita leeren
                dgvVita.DataSource = null;
                dgvSchaden.DataSource = null;
                // Speditionseinstellungen deaktivieren
                SetFelderFremdfahrzeugeEnabled(false);

                //Leingang udn ArtikelCheck zurücksetzen
                pbCheckArtikel.Image = null;
                pbCheckEingang.Image = null;
                //SetLabelKennzeichen(true);
                SetLEingangCheck();
            }
            else
            {
                clsMessages.Allgemein_MandantFehlt();
            }
        }
        ///<summary>ctrEinlagerung / SetLagerEingangsFelderEnabled</summary>
        ///<remarks>Aktiviert die entsprechenden Button und Eingabefelder für die neue Einlagerung.</remarks>
        private void SetLagerEingangsFelderEnabled(bool Enable, bool bWasChecked = false)
        {
            Enable = Enable & (this.Lager.Eingang.LockedBy == 0 || this.Lager.Eingang.LockedBy == this.GL_User.User_ID);
            //Angaben in Menüzeile
            tbLEingangTableID.Enabled = Enable;
            tbLEingangID.Enabled = Enable;
            dtpEinlagerungDate.Enabled = Enable;
            tbDFUE.Enabled = Enable;
            gbZusatzInfo.Enabled = Enable;
            tsbtnCheckComplete.Enabled = Enable;
            gboxLEDaten.Enabled = Enable;

            if (!this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_EditADRAfterClose)
            {
                if (bWasChecked)
                {
                    btnSearchA.Enabled = false;
                    btnSearchE.Enabled = false;
                    btnSearchES.Enabled = false;
                    btnVersender.Enabled = false;
                    btnSpedition.Enabled = false;

                    tbSearchA.Enabled = false;
                    tbSearchE.Enabled = false;
                    tbSearchES.Enabled = false;
                    tbSearchV.Enabled = false;
                    tbMCSpedition.Enabled = false;

                    btnManSped.Enabled = false;
                    btnManVersender.Enabled = false;
                    btnManEmpfaenger.Enabled = false;
                    btnManEntladestelle.Enabled = false;
                }
                else
                {
                    btnSearchA.Enabled = Enabled;
                    btnSearchE.Enabled = Enabled;
                    btnSearchES.Enabled = Enabled;
                    btnVersender.Enabled = Enabled;
                    btnSpedition.Enabled = Enabled;

                    tbSearchA.Enabled = Enabled;
                    tbSearchE.Enabled = Enabled;
                    tbSearchES.Enabled = Enabled;
                    tbSearchV.Enabled = Enabled;
                    tbMCSpedition.Enabled = Enabled;

                    btnManSped.Enabled = Enabled;
                    btnManVersender.Enabled = Enabled;
                    btnManEmpfaenger.Enabled = Enabled;
                    btnManEntladestelle.Enabled = Enabled;
                }

            }
        }
        ///<summary>ctrEinlagerung / toolStripButton1_Click</summary>
        ///<remarks>Zum ersten Eingang springen </remarks>
        private void tsbtnFirstItem_Click(object sender, EventArgs e)
        {
            try
            {
                bUpdate = true;
                bBack = true;
                EingangBrowse(0, 0, enumBrowseAcivity.FirstItem);
            }
            catch (Exception ex)
            {
                string mes = ex.ToString();
            }
        }
        ///<summary>ctrEinlagerung / tsbtnLastItem_Click</summary>
        ///<remarks>Zum letzten EIngang springen</remarks>
        private void tsbtnLastItem_Click(object sender, EventArgs e)
        {
            bUpdate = true;
            bBack = false;
            EingangBrowse(0, 0, enumBrowseAcivity.LastItem);
        }
        /// <summary>
        /// ctrEinlagerung / SetEingangLocked
        /// </summary>
        private void SetEingangLocked()
        {
            bool locked = false;
            if (Lager.Eingang.LockedBy > 0 && Lager.Eingang.LockedBy != this.GL_User.User_ID)
            {
                locked = true;
            }

            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_LagerOrt_Enabled_Halle)
            {
                tbHalle.Enabled = !locked;
            }
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_LagerOrt_Enabled_Reihe)
            {
                tbReihe.Enabled = !locked;
            }
            tbInfoAusgang.ReadOnly = true;
            tbUBInfo.ReadOnly = true;

            //SetLagerEingangsFelderEnabled(!locked);
            tbLocked.Visible = locked;
            tbLockedBy.Visible = locked;
            tbLockedBy.Text = clsUser.GetBenutzerFullNameByID(Lager.Eingang.LockedBy);
            // tsbtnDeleteLEingang.Enabled = tsbtnDeleteLEingang.Enabled & !locked;
            tsbtnChangeEingang.Enabled = !locked;
            tsbtnCheckComplete.Enabled = !locked & tsbtnCheckComplete.Enabled;
            //pbCheckEingang.Enabled = !locked;
            pbCheckArtikel.Enabled = !locked;
            gbArtCheck.Enabled = !locked;
        }
        ///<summary>ctrEinlagerung / tsbtnBack_Click</summary>
        ///<remarks>Eingänge blättern</remarks>
        private void tsbtnBack_Click(object sender, EventArgs e)
        {
            try
            {
                bUpdate = true;
                bBack = false;
                EingangBrowse(0, 0, enumBrowseAcivity.BackItem);
            }
            catch (Exception ex)
            {
                string mes = ex.ToString();
            }
        }
        ///<summary>ctrEinlagerung / tsbtnForward_Click</summary>
        ///<remarks>Eingänge blättern</remarks>
        private void tsbtnForward_Click(object sender, EventArgs e)
        {
            try
            {
                bUpdate = true;
                bBack = true;
                EingangBrowse(0, 0, enumBrowseAcivity.NextItem);
            }
            catch (Exception ex)
            {
                string mes = ex.ToString();
            }
        }
        ///<summary>ctrEinlagerung / tsbtnForward_Click</summary>
        ///<remarks>Eingänge blättern</remarks>
        public void SetLEingangskopfdatenToFrm(bool bReload)
        {
            if (_MandantenID > 0)
            {
                if (!bReload)
                {
                    Lager.Eingang.AbBereichID = this.GL_System.sys_ArbeitsbereichID;
                    Lager.Eingang.GetNextLEingangsID(bBack);
                }
                Lager.LEingangTableID = Lager.Eingang.LEingangTableID;
                SetLagerDatenToFrm();
            }
        }
        ///<summary>ctrEinlagerung / SetSearchLEingangskopfdatenToFrm</summary>
        ///<remarks>setzt gesuchten Artikel</remarks>
        public void SetSearchLEingangskopfdatenToFrm(decimal myDecArtID)
        {
            EingangBrowse(0, myDecArtID, enumBrowseAcivity.ArtItem);
            bUpdate = true;
        }
        ///<summary>ctrEinlagerung / SetSearchLEingangskopfdatenToFrm</summary>
        ///<remarks>setzt gesuchten Artikel</remarks>
        public void SetSearchLEingangskopfdatenToFrmEA(decimal myDecEID)
        {
            EingangBrowse(myDecEID, 0, enumBrowseAcivity.Item);
            bUpdate = true;
        }
        /// <summary>
        ///             ctr die unter bestimmten Bedingungen (Lagerdaten) aktiviert bzw. deaktiviert werden
        /// </summary>
        private void SetMenuItemEingangEnabled()
        {
            //...|tsbtnDirectTransformation
            if (this.tsbtnDirectTransformation.Visible)
            {
                this.tsbtnDirectTransformation.Enabled = (
                                                           (!this.Lager.Eingang.DirektDelivery) &&
                                                           (this.Lager.Eingang.Checked) &&
                                                           (this.Lager.Eingang.ArtikelCountInStore > 0)
                                                         );
            }
        }
        ///<summary>ctrEinlagerung / SetLagerDatenToFrm</summary>
        ///<remarks>Lagerkopfdaten werden auf die Form gesetzt</remarks>
        private void SetLagerDatenToFrm()
        {
            SetMenuItemEingangEnabled();

            if ((this.Lager.Eingang is clsLEingang) && (this.Lager.Eingang.ExistLEingangTableID()))
            {
                this.tbLocked.Visible = false;
                this.tbLockedBy.Visible = false;

                tbLEingangTableID.Text = "[" + Lager.Eingang.LEingangTableID.ToString() + "]";
                dtpEinlagerungDate.Value = Lager.Eingang.LEingangDate;
                tbLEingangID.Text = Lager.Eingang.LEingangID.ToString();
                tbDFUE.Text = Lager.Eingang.ASN.ToString();
                if (this.Lager.Eingang.AdrManuell == null)
                {
                    this.Lager.Eingang.AdrManuell = new clsADRMan();
                    this.Lager.Eingang.AdrManuell.InitClass(this.GL_User, this.Lager.Eingang.LEingangTableID, "LEingang");
                }

                //Auftraggeber
                if (Lager.Eingang.Auftraggeber > 0)
                {
                    //_ADR_ID_A = Lager.Eingang.Auftraggeber;
                    SearchButton = 1;
                    SetKDRecAfterADRSearch(Lager.Eingang.Auftraggeber, false);
                }
                else
                {
                    tbSearchA.Text = string.Empty;
                    tbAuftraggeber.Text = string.Empty;
                    try
                    {
                        clsADRMan tmpADRMan = new clsADRMan();
                        tmpADRMan.InitClass(this.GL_User, this.Lager.Eingang.LEingangTableID, "LEingang");
                        tmpADRMan.DictManuellADREinlagerung.TryGetValue(clsADRMan.cont_AdrArtID_Auftraggeber, out tmpADRMan);
                        if (tmpADRMan != null)
                        {
                            this.Lager.Eingang.AdrManuell = tmpADRMan;
                            tbAuftraggeber.Text = this.Lager.Eingang.AdrManuell.AdrString;
                        }
                    }
                    catch (Exception ex)
                    {
                        string mes = ex.ToString();
                    }
                }

                //Versender
                if (Lager.Eingang.Versender > 0)
                {
                    //_ADR_ID_V = Lager.Eingang.Versender;
                    SearchButton = 2;
                    SetKDRecAfterADRSearch(Lager.Eingang.Versender, false);
                }
                else
                {
                    tbSearchV.Text = string.Empty;
                    tbVersender.Text = string.Empty;
                    try
                    {
                        clsADRMan tmpADRMan = new clsADRMan();
                        tmpADRMan.InitClass(this.GL_User, this.Lager.Eingang.LEingangTableID, "LEingang");
                        tmpADRMan.DictManuellADREinlagerung.TryGetValue(clsADRMan.cont_AdrArtID_Versender, out tmpADRMan);
                        if (tmpADRMan != null)
                        {
                            this.Lager.Eingang.AdrManuell = tmpADRMan;
                            tbVersender.Text = this.Lager.Eingang.AdrManuell.AdrString;
                        }
                    }
                    catch (Exception ex)
                    {
                        string mes = ex.ToString();
                    }
                }

                //Empfänger
                if (Lager.Eingang.Empfaenger > 0)
                {
                    //_ADR_ID_E = Lager.Eingang.Empfaenger;
                    SearchButton = 3;
                    SetKDRecAfterADRSearch(Lager.Eingang.Empfaenger, false);
                }
                else
                {
                    tbSearchE.Text = string.Empty;
                    tbEmpfaenger.Text = string.Empty;
                    try
                    {
                        clsADRMan tmpADRMan = new clsADRMan();
                        tmpADRMan.InitClass(this.GL_User, this.Lager.Eingang.LEingangTableID, "LEingang");
                        tmpADRMan.DictManuellADREinlagerung.TryGetValue(clsADRMan.cont_AdrArtID_Empfaenger, out tmpADRMan);
                        if (tmpADRMan != null)
                        {
                            this.Lager.Eingang.AdrManuell = tmpADRMan;
                            tbEmpfaenger.Text = this.Lager.Eingang.AdrManuell.AdrString;
                        }
                    }
                    catch (Exception ex)
                    {
                        string mes = ex.ToString();
                    }
                }

                //Entladestelle
                if (Lager.Eingang.EntladeID > 0)
                {
                    //_ADR_ID_E = Lager.Eingang.EntladeID;
                    SearchButton = 12;
                    SetKDRecAfterADRSearch(Lager.Eingang.EntladeID, false);
                }
                else
                {
                    tbSearchES.Text = string.Empty;
                    tbEntladestelle.Text = string.Empty;
                    try
                    {
                        clsADRMan tmpADRMan = new clsADRMan();
                        tmpADRMan.InitClass(this.GL_User, this.Lager.Eingang.LEingangTableID, "LEingang");
                        tmpADRMan.DictManuellADREinlagerung.TryGetValue(clsADRMan.cont_AdrArtID_Entladeadresse, out tmpADRMan);
                        if (tmpADRMan != null)
                        {
                            this.Lager.Eingang.AdrManuell = tmpADRMan;
                            tbEntladestelle.Text = this.Lager.Eingang.AdrManuell.AdrString;
                        }
                    }
                    catch (Exception ex)
                    {
                        string mes = ex.ToString();
                    }
                }

                //--- Vehicle / FAhrzeug
                if (this.Lager.Eingang.IsShip)
                {
                    Functions.SetComboToSelecetedValue(ref cbFahrzeug, ctrEinlagerung.const_cbFahrzeugValue_Schiff.ToString());
                    mtbKFZ.Text = this.Lager.Eingang.Ship;
                    SetLabelKennzeichen(ctrEinlagerung.const_cbFahrzeugValue_Schiff);
                }
                else if (this.Lager.Eingang.IsWaggon)
                {
                    Functions.SetComboToSelecetedValue(ref cbFahrzeug, ctrEinlagerung.const_cbFahrzeugValue_Waggon.ToString());
                    mtbKFZ.Text = this.Lager.Eingang.WaggonNr;
                    SetLabelKennzeichen(ctrEinlagerung.const_cbFahrzeugValue_Waggon);
                }
                else
                {
                    Functions.SetComboToSelecetedValue(ref cbFahrzeug, ctrEinlagerung.const_cbFahrzeugValue_Fremdfahrzeug.ToString());
                    mtbKFZ.Text = Lager.Eingang.KFZ;
                    SetLabelKennzeichen(ctrEinlagerung.const_cbFahrzeugValue_Fremdfahrzeug);
                }


                if (Lager.Eingang.SpedID > 0)
                {
                    //Functions.SetComboToSelecetedItem(ref cbFahrzeug, Lager.Eingang.KFZ);
                    //mtbKFZ.Text = Lager.Eingang.KFZ;
                    SearchButton = 7;
                    SetKDRecAfterADRSearch(Lager.Eingang.SpedID, false);
                }
                else
                {
                    tbADRSpedition.Text = string.Empty;
                    tbMCSpedition.Text = string.Empty;
                    try
                    {
                        clsADRMan tmpADRMan = new clsADRMan();
                        tmpADRMan.InitClass(this.GL_User, this.Lager.Eingang.LEingangTableID, "LEingang");
                        tmpADRMan.AdrArtID = clsADRMan.cont_AdrArtID_Spedition;
                        if (tmpADRMan.CheckManADRForAdrArt())
                        {
                            tmpADRMan.DictManuellADREinlagerung.TryGetValue(clsADRMan.cont_AdrArtID_Spedition, out tmpADRMan);
                            if (tmpADRMan != null)
                            {
                                this.Lager.Eingang.AdrManuell = tmpADRMan;
                                tbADRSpedition.Text = this.Lager.Eingang.AdrManuell.AdrString;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string mes = ex.ToString();
                    }
                }
                SetLEingangCheck();
                SetLabelDirektanlieferung(Lager.Eingang.DirektDelivery);

                tbLieferantenID.Text = Lager.Eingang.Lieferant;
                tbLfsNr.Text = Lager.Eingang.LEingangLfsNr;
                tbExTransportRef.Text = Lager.Eingang.ExTransportRef;
                tbFahrer.Text = Lager.Eingang.Fahrer;

                cbUmbuchung.Checked = Lager.Eingang.Umbuchung;
                cbVerlagerung.Checked = Lager.Eingang.Verlagerung;
                cbRetoure.Checked = Lager.Eingang.Retoure;
                cbVorfracht.Checked = Lager.Eingang.Vorfracht;
                cbLagerTransport.Checked = Lager.Eingang.LagerTransport;
            }
        }
        ///<summary>ctrEinlagerung / SetLabelDirektanlieferung</summary>
        ///<remarks>Infotext Direktanlieferung wird ein-/ausgeblendet.</remarks> 
        private void SetLabelDirektanlieferung(bool bVisible)
        {
            string strInfoText = string.Empty;
            if (this.Lager.Eingang.IsKorrStVerfahrenInUse)
            {
                //strInfoText = "Korrektur-/Stornoverfahren aktiv";
                strInfoText = "KSV aktiv";
                bVisible = true;
            }
            if (this.Lager.Eingang.DirektDelivery)
            {
                //strInfoText = "Direktanlieferung ";
                strInfoText = "Direktanlieferung";
            }
            lInfoEingang.Text = strInfoText;
            lInfoEingang.Visible = bVisible;
            tsbtnDirectAbschluss.Enabled = bVisible;
        }
        ///<summary>ctrEinlagerung / SetLEingangCheck</summary>
        ///<remarks>.</remarks>    
        private void SetLEingangArtikelCheck()
        {
            if (_bArtikelIsChecked)
            {
                //Eingang bereits geprüft
                pbCheckArtikel.Image = (Image)Sped4.Properties.Resources.check;
                SetArtikelMenuBtnEnabled();
                //tsbtnArtikelNeu.Enabled = true;
                SetArtikelEingabefelderDatenEnable(false);
            }
            else
            {
                pbCheckArtikel.Image = (Image)Sped4.Properties.Resources.warning.ToBitmap();
                SetArtikelMenuBtnEnabled();
                SetArtikelEingabefelderDatenEnable(true);
            }
        }
        ///<summary>ctrEinlagerung / ClearEingangsuebersicht</summary>
        ///<remarks>Leert alle Eingabefelder der Eingangsdaten.</remarks>     
        private void ClearEingangsuebersicht()
        {
            tbLEingangTableID.Text = string.Empty;
            tbLEingangID.Text = string.Empty;
            tbEArtAnzahl.Text = string.Empty;
            tbNettoGesamt.Text = string.Empty;
            tbBruttoGesamt.Text = string.Empty;

            tbLfsNr.Text = string.Empty;
            tbFahrer.Text = string.Empty;

            tstbJumpEingangID.Text = string.Empty;
            tstbJumpArtID.Text = string.Empty;

            mtbKFZ.Text = string.Empty;
            cbFahrzeug.SelectedIndex = 0;
            cbRetoure.Checked = false;
            cbVerlagerung.Checked = false;
            cbUmbuchung.Checked = false;
            cbVorfracht.Checked = false;
            cbLagerTransport.Checked = false;
            tbExTransportRef.Text = string.Empty;

            //Adressen müssen zum Schluss kommen
            tbSearchA.Text = string.Empty;
            tbSearchV.Text = string.Empty;
            tbSearchE.Text = string.Empty;
            tbSearchES.Text = string.Empty;
            tbMCSpedition.Text = string.Empty;
            tbADRSpedition.Text = string.Empty;
            tbEntladestelle.Text = string.Empty;
            tbAuftraggeber.Text = string.Empty;
            tbVersender.Text = string.Empty;
            tbEmpfaenger.Text = string.Empty;
        }
        ///<summary>ctrEinlagerung / pbCheckEingang_Click</summary>
        ///<remarks>Eingang kann abgeschlossen werden, wernn alle Artikel im Eingang geprüft wurden.</remarks> 
        private void pbCheckEingang_Click(object sender, EventArgs e)
        {
            if (!_bLEingangIsChecked)
            {
                //Prüfung Eingang
                if (this.dgv.Rows.Count > 0)
                {
                    if (CheckEingangElementsToFinish(true))
                    {
                        SaveAndCheckEingang();
                    }
                }
            }
            SetLEingangCheck();
            InitGrdArtVita();
        }
        ///<summary>ctrEinlagerung / tsbtnCheckComplete_Click</summary>
        ///<remarks></remarks> 
        private void tsbtnCheckComplete_Click(object sender, EventArgs e)
        {
            if (!_bLEingangIsChecked)
            {
                //Prüfung Eingang
                if (this.dgv.Rows.Count > 0)
                {
                    if (Lager.Eingang.ExistLEingangTableID())
                    {
                        bUpdate = true;
                    }
                    //Prüfung Eingang
                    if (CheckEingangElementsToFinish(false))
                    {
                        if (SaveAndCheckEingang())
                        {
                            // Verursachte Exceptions in Backgroundworker
                            DoCheckEingangArtikelComplete();
                            SetArtikelMenuBtnEnabled();
                            SetArtikelEingabefelderDatenEnable(false);
                        }
                    }
                }
            }
        }
        ///<summary>ctrEinlagerung / SaveAndCheckEingang</summary>
        ///<remarks></remarks> 
        private bool SaveAndCheckEingang()
        {
            bool bReturn = false;
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_SetCheckDate)
            {
                if (this.Lager.Eingang.FirstCheckDateTime == clsSystem.const_DefaultDateTimeValue_Min)
                {
                    dtpEinlagerungDate.Value = DateTime.Now;
                }
            }
            if (SaveEingang())
            {
                _bLEingangIsChecked = true;
                //---Reihenfolgde muss hier eingehalten werden erst die Abfrage auf Korrekturverfahren   
                if (this.Lager.Eingang.IsKorrStVerfahrenInUse)
                {
                    clsLager.UpdateLEingangCheck(GL_User.User_ID, _bLEingangIsChecked, Lager.LEingangTableID);
                    this.Lager.Eingang.FillEingang();
                    InitASNTransfer(clsASNAction.const_ASNAction_StornoKorrektur);
                }
                else
                {
                    clsLager.UpdateLEingangCheck(GL_User.User_ID, _bLEingangIsChecked, Lager.LEingangTableID);
                    this.Lager.Eingang.FillEingang();
                    InitASNTransfer(clsASNAction.const_ASNAction_Eingang);
                }
                this.Lager.Eingang.FillEingang();
                SetLabelDirektanlieferung(false);
                CheckDirectPrintLabel();
                bReturn = true;
            }
            else
            {
                bReturn = false;
            }
            return bReturn;
        }
        ///<summary>ctrEinlagerung / CheckDirectPrintLabel</summary>
        ///<remarks>prüft die Freigabe auf den Labeldruck</remarks> 
        private void InitASNTransfer(Int32 myASNAction)
        {
            AsnTransfer = new clsASNTransfer();
            if (AsnTransfer.DoASNTransfer(this.GL_System, this.Lager.Eingang.AbBereichID, this.Lager.Eingang.MandantenID))
            {
                if (this._ctrMenu._frmMain.system.Client.Modul.ASN_UserOldASNFileCreation)
                {
                    //dieses Verfahren wird hier verwendet
                    this.Lager.Artikel.listArt.Clear();
                    this.Lager.Artikel.listArt.Add(this.Lager.Artikel.Copy());

                    this.Lager.ASNAction.ASNActionProcessNr = myASNAction;
                    AsnTransfer.CreateLM(ref this.Lager);
                }
                else
                {
                    //-- wird nicht verwendet
                    AsnTransfer.CreateLM_Eingang(ref this.Lager);
                }
            }
        }
        ///<summary>ctrEinlagerung / CheckDirectPrintLabel</summary>
        ///<remarks>prüft die Freigabe auf den Labeldruck</remarks> 
        private void CheckDirectPrintLabel()
        {
            this.Lager.Eingang.FillEingang();
            //Prüft directer Labeldruck nach Abschluss Eingang
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_Print_DirectLabelAfterCheckEingang)
            {
                if (this.Lager.Eingang.Checked)
                {
                    this.Lager.Artikel.GetArtikeldatenByTableID();
                    if (!this.Lager.Eingang.AllArtikelLabelPrinted)
                    {
                        if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
                        {
                            DirectPrintArtikelLabel();
                            this.Lager.Artikel.UpdateLabelPrintByEingang(true, this.Lager.Eingang.LEingangTableID);
                            this.Lager.Artikel.GetArtikeldatenByTableID();
                        }
                        else
                        {
                            if (DoDirectPrintReport(enumIniDocKey.LabelAll))
                            {
                                this.Lager.Artikel.UpdateLabelPrintByEingang(true, this.Lager.Eingang.LEingangTableID);
                                this.Lager.Artikel.GetArtikeldatenByTableID();
                            }
                        }
                    }
                }
                else
                {
                }
            }
            else
            {
                //Prüft directer Labeldruck wenn alle Artikel geprüft wurden
                if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_Print_DirectLabel)
                {
                    if (this.Lager.Eingang.AllArtikelChecked)
                    {
                        if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
                        {
                            DirectPrintArtikelLabel();
                            this.Lager.Artikel.UpdateLabelPrintByEingang(true, this.Lager.Eingang.LEingangTableID);
                            this.Lager.Artikel.GetArtikeldatenByTableID();
                        }
                        else
                        {
                            if (DoDirectPrintReport(enumIniDocKey.LabelAll))
                            {
                                this.Lager.Artikel.UpdateLabelPrintByEingang(true, this.Lager.Eingang.LEingangTableID);
                                this.Lager.Artikel.GetArtikeldatenByTableID();
                            }
                        }
                    }
                }
                else
                {
                    if (this.Lager.Eingang.Checked)
                    {
                        _bArtPrint = false;
                        this._ctrMenu.OpenCtrPrintLagerInFrm(this);
                    }
                }
            }
        }
        ///<summary>ctrEinlagerung / CheckEingangElementsToFinish</summary>
        ///<remarks>Hier werden die zu prüfenden Eingangselemente laut Clientdefinition 
        ///         ermittelt und geprüft</remarks> 
        private bool CheckEingangElementsToFinish(bool bCheckArtCheck)
        {
            bool bReturn = true;
            //Check Eingang
            string strInfo = string.Empty;

            //Lieferantennummer bei ASN-Communikation
            //if (this._ctrMenu._frmMain.system.AbBereich.ASNTransfer)
            //{
            //    if (this.Lager.Eingang.Lieferant.Equals(string.Empty))
            //    {
            //        strInfo = strInfo + "- Feld [Lieferantennummer] (ist notwendig bei der DFÜ Kommunikation) " + Environment.NewLine;
            //    }
            //}
            //Halle
            this.Lager.Artikel.LEingangTableID = this.Lager.LEingangTableID;
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_RequiredValue_Halle)
            {
                if (!this.Lager.Artikel.CheckAllArtikelInEingangPacedinHalle())
                {
                    strInfo = strInfo + "- Feld [Halle] " + Environment.NewLine;
                }
            }
            //Reihe
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_RequiredValue_Reihe)
            {
                if (!this.Lager.Artikel.CheckAllArtikelInEingangPacedinReihe())
                {
                    strInfo = strInfo + "- Feld [Reihe] " + Environment.NewLine;
                }
            }
            //kein ArtikelCheck bei CheckALL
            if (bCheckArtCheck)
            {
                //Check Alle Artikel geprüft
                if (!clsArtikel.CheckAllArtikelChecked_Eingang(GL_User.User_ID, Lager.LEingangTableID))
                {
                    strInfo = strInfo + "- Artikleprüfung / Artikelcheck " + Environment.NewLine;
                }
            }
            //--Custom Processes
            CustomProcessesViewData cpVD = new CustomProcessesViewData((int)this.Lager.Artikel.Eingang.Auftraggeber, (int)this.Lager.Artikel.AbBereichID, this.GL_User);
            if (cpVD.ExistCustomProcess)
            {
                bool IsAction = cpVD.CheckAndExecuteCustomProcess(0, (int)this.Lager.Eingang.LEingangTableID, 0, CustomProcess_Novelis_AccessByArticleCert.const_ProcessLocation_ctrEinlagerung_CheckEingangElementsToFinish);
                if ((IsAction) && (!cpVD.Process_Novelis_AccessByArticleCert.IsArticleZertifacteProcessComplete))
                {
                    strInfo = strInfo + "- Artikleprüfung / Custom Process / SPL - Artikel Zertifikat fehlt " + Environment.NewLine;
                }
            }

            //Check
            if (strInfo != string.Empty)
            {
                if ((_ctrMenu.GL_User.IsAdmin) && (_ctrMenu.GL_User.LoginName.ToUpper().Equals("ADMIN")))
                {
                    string strKopf = "Der Eingang kann nicht abgeschlossen werden, da die Prüfung folgender Felder negativ verlaufen ist:" + Environment.NewLine;
                    strInfo = strKopf + strInfo;
                    strInfo += Environment.NewLine;
                    strInfo += "Soll der Eingang dennoch abgeschlossen werden, dann bestätigen Sie mit OK!" + Environment.NewLine;

                    bReturn = clsMessages.Allgemein_SelectionInfoTextShow(strInfo);
                }
                else
                {
                    bReturn = false;
                    string strKopf = "Der Eingang kann nicht abgeschlossen werden, da die Prüfung folgender Felder negativ verlaufen ist:" + Environment.NewLine;
                    strInfo = strKopf + strInfo;
                    clsMessages.Allgemein_ERRORTextShow(strInfo);
                }
            }
            return bReturn;
        }
        ///<summary>ctrEinlagerung / DirectPrintLEingangDoc</summary>
        ///<remarks>Nach Abschluss des Eingangs soll direct das Dokument gedruckt werden</remarks> 
        private void DirectPrintLEingangDoc()
        {
            //direct Print
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_Print_DirectEingangDoc)
            {
                if (!this.Lager.Eingang.IsPrintDoc)
                {
                    //Aktuell im Quellcode brauche noch ne bessere idee
                    List<decimal> AuftraggeberMat = new List<decimal>();
                    bool bUseMatDoc = false;
                    clsSystem sys = new clsSystem(Application.StartupPath);
                    sys.GetAuftraggeberMat(ref this.GL_System, this.GL_System.sys_MandantenID);
                    if (this.GL_System.AuftraggeberMat != string.Empty)
                    {

                        string[] AuftraggeberSplit = this.GL_System.AuftraggeberMat.Split(',');
                        foreach (string auftraggeber in AuftraggeberSplit)
                        {
                            if (this.Lager.Eingang.Auftraggeber == clsADR.GetIDByMatchcode(auftraggeber))
                            {
                                bUseMatDoc = true;
                            }
                        }
                    }
                    if (true) { }
                    ctrPrintLager TmpPrint = new ctrPrintLager();
                    TmpPrint.Hide();
                    TmpPrint._ctrMenu = this._ctrMenu;
                    TmpPrint._ctrEinlagerung = this;

                    if (!bUseMatDoc)
                    {
                        TmpPrint._DokumentenArt = enumDokumentenArt.LagerEingangDoc.ToString();
                    }
                    else
                    {
                        TmpPrint._DokumentenArt = enumDokumentenArt.LagerEingangDocMat.ToString();
                    }
                    TmpPrint.SetLagerDatenToFrm();

                    //TmpPrint.nudPrintCount.Value = 2;
                    if (cbNachbearbeitung.Checked)
                    {
                        TmpPrint.nudPrintCount.Value = 1;
                        TmpPrint.bCountfromGUI = true;
                    }
                    else
                        TmpPrint.nudPrintCount.Value = sys.GetCountPrintDoc(ref this.GL_System, this.GL_System.sys_MandantenID);
                    TmpPrint.StartPrint(true);
                    TmpPrint.Dispose();
                    this.Lager.Eingang.UpdatePrintLEingang(enumDokumentenArt.LagerEingangDoc.ToString());
                }
            }
        }
        ///<summary>ctrEinlagerung / DirectPrintList</summary>
        ///<remarks></remarks> 
        private void DirectPrintList()
        {
            //direct Print
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_Print_DirectList)
            {
                if (!this.Lager.Eingang.IsPrintList)
                {
                    ctrPrintLager TmpPrint = new ctrPrintLager();
                    TmpPrint.Hide();
                    TmpPrint._ctrMenu = this._ctrMenu;
                    TmpPrint._ctrEinlagerung = this;
                    TmpPrint._DokumentenArt = enumDokumentenArt.Eingangsliste.ToString();
                    TmpPrint.SetLagerDatenToFrm();
                    TmpPrint.StartDirectPrint();
                    TmpPrint.Dispose();
                    this.Lager.Eingang.UpdatePrintLEingang(enumDokumentenArt.Eingangsliste.ToString());
                }
            }
        }
        ///<summary>ctrEinlagerung / DirectPrintArtikelLabel</summary>
        ///<remarks></remarks> 
        private void DirectPrintArtikelLabel()
        {
            if (this.Lager.Eingang.LEingangTableID > 0)
            {
                ctrPrintLager TmpPrint = new ctrPrintLager();
                TmpPrint.Hide();
                TmpPrint._ctrMenu = this._ctrMenu;
                TmpPrint._ctrEinlagerung = this;
                TmpPrint._DokumentenArt = enumDokumentenArt.LabelAll.ToString();
                TmpPrint.SetLagerDatenToFrm();
                TmpPrint.nudPrintCount.Value = this._ctrMenu._frmMain.system.GetCountPrintDoc(ref this.GL_System, this.GL_System.sys_MandantenID);
                //TmpPrint.StartPrintArtikelLabel(true);
                TmpPrint.StartDirectPrint();
                TmpPrint.Dispose();
            }
        }
        ///<summary>ctrEinlagerung / DirectPrintLEingangDoc</summary>
        ///<remarks>Nach Abschluss des Eingangs soll direct das Dokument gedruckt werden</remarks> 
        private void DirectPrintSPLLable(object sender, EventArgs e)
        {
            ctrSPLAdd tmpSPL = new ctrSPLAdd(this);
            this._ctrMenu.OpenFrmTMP(tmpSPL);

            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_SPL_AutoPrintSPLDocument)
            {

            }
            else
            {
                //Abfrage
            }
            tmpSPL.Dispose();
        }
        ///<summary>ctrEinlagerung / SetLEingangCheck</summary>
        ///<remarks>.</remarks>    
        private void SetLEingangCheck()
        {
            //Lager.FillLagerDaten(true);
            if (Lager.Eingang.Checked)
            {
                //Eingang bereits geprüft
                pbCheckEingang.Image = (Image)Sped4.Properties.Resources.check;
                //Artikelmenu enabled setzen
                SetArtikelMenuBtnEnabled();
                tsbtnDeleteLEingang.Enabled = false;
            }
            else
            {
                pbCheckEingang.Image = (Image)Sped4.Properties.Resources.warning.ToBitmap();
                tsbtnDeleteLEingang.Enabled = true & (this.Lager.Eingang.LockedBy == 0 || this.Lager.Eingang.LockedBy == this.GL_User.User_ID);
            }
            //Deaktiviert die Eingabefelder im Lager Eingangsbereich
            DateTime datetimeChecked = clsArtikelVita.getCheckDate(this.GL_User, this.Lager.LEingangTableID);
            bool bWasChecked = false;
            if (datetimeChecked.Year > 1)
                bWasChecked = true;
            SetLagerEingangsFelderEnabled(!Lager.Eingang.Checked, bWasChecked);// && !bWasChecked); CF  Nach entsperren des Einganges ließen sich die Felder nciht bearbeiten
            _bLEingangIsChecked = Lager.Eingang.Checked;
        }

        /***************************************************************************************************
        *                                Methoden für Artikel
        * ************************************************************************************************/
        ///<summary>ctrEinlagerung / tbBreite_Validated</summary>
        ///<remarks>Die Eingabe wird direkt auf das Format geprüft.</remarks>  
        private void tbProduktionsNr_Validated(object sender, EventArgs e)
        {
        }
        ///<summary>frmEinlagerung / tbBreite_Validated</summary>
        ///<remarks>Die Eingabe wird direkt auf das Format geprüft.</remarks>  
        private void tbBreite_Validated(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            if (!Decimal.TryParse(tbBreite.Text, out decTmp))
            {
                clsMessages.Allgemein_EingabeFormatFehlerhaft();
            }
            if (decTmp < 0)
            {
                decTmp = decTmp * (-1);
            }
            tbBreite.Text = Functions.FormatDecimal(decTmp);
        }
        ///<summary>ctrEinlagerung / tbLaenge_Validated</summary>
        ///<remarks>Die Eingabe wird direkt auf das Format geprüft.</remarks>  
        private void tbLaenge_Validated(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            if (!Decimal.TryParse(tbLaenge.Text, out decTmp))
            {
                clsMessages.Allgemein_EingabeFormatFehlerhaft();
            }
            if (decTmp < 0)
            {
                decTmp = decTmp * (-1);
            }
            tbLaenge.Text = Functions.FormatDecimal(decTmp);
        }
        ///<summary>ctrEinlagerung / tbHoehe_Validated</summary>
        ///<remarks>Die Eingabe wird direkt auf das Format geprüft.</remarks>  
        private void tbHoehe_Validated(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            if (!Decimal.TryParse(tbHoehe.Text, out decTmp))
            {
                clsMessages.Allgemein_EingabeFormatFehlerhaft();
            }
            if (decTmp < 0)
            {
                decTmp = decTmp * (-1);
            }
            tbHoehe.Text = Functions.FormatDecimal(decTmp);
        }
        ///<summary>ctrEinlagerung / tbNetto_Validated</summary>
        ///<remarks>Die Eingabe wird direkt auf das Format geprüft.</remarks>  
        private void tbNetto_Validated(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            if (!Decimal.TryParse(tbNetto.Text, out decTmp))
            {
                clsMessages.Allgemein_EingabeFormatFehlerhaft();
            }
            if (decTmp < 0)
            {
                decTmp = decTmp * (-1);
            }
            tbNetto.Text = Functions.FormatDecimal(decTmp);
            tbBrutto.Text = tbNetto.Text;
        }
        ///<summary>ctrEinlagerung / tbBrutto_Validated</summary>
        ///<remarks>Die Eingabe wird direkt auf das Format geprüft.</remarks>  
        private void tbBrutto_Validated(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            if (!Decimal.TryParse(tbBrutto.Text, out decTmp))
            {
                clsMessages.Allgemein_EingabeFormatFehlerhaft();
            }
            if (decTmp < 0)
            {
                decTmp = decTmp * (-1);
            }
            tbBrutto.Text = Functions.FormatDecimal(decTmp);

            //Bruttowert >0 dann Berechnung des Packmittels
            if (decTmp > 0)
            {
                decimal decNet = Convert.ToDecimal(tbNetto.Text);
                decimal decBru = Convert.ToDecimal(tbBrutto.Text);
                decimal decPack = 0;
                if (decNet > decBru)
                {
                    clsMessages.Allgemein_BruttoKleinerNetto();
                }
                else
                {
                    decPack = decBru - decNet;
                }
                tbPackmittelGewicht.Text = Functions.FormatDecimal(decPack);
            }
        }
        ///<summary>ctrEinlagerung / tbPackmittelGewicht_Validated</summary>
        ///<remarks>Die Eingabe wird direkt auf das Format geprüft.</remarks>  
        private void tbPackmittelGewicht_Validated(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            if (!Decimal.TryParse(tbPackmittelGewicht.Text, out decTmp))
            {
                clsMessages.Allgemein_EingabeFormatFehlerhaft();
            }
            if (decTmp < 0)
            {
                decTmp = decTmp * (-1);
            }
            tbPackmittelGewicht.Text = Functions.FormatDecimal(decTmp);
        }
        ///<summary>ctrEinlagerung / tbAnzahl_Validated</summary>
        ///<remarks>Die Eingabe wird direkt auf das Format geprüft.</remarks>     
        private void tbAnzahl_Validated(object sender, EventArgs e)
        {
            Int32 iTmp = 0;
            if (!Int32.TryParse(tbAnzahl.Text, out iTmp))
            {
                clsMessages.Allgemein_EingabeFormatFehlerhaft();
            }
            if (iTmp < 0)
            {
                iTmp = iTmp * (-1);
            }
            tbAnzahl.Text = iTmp.ToString();
        }
        ///<summary>ctrEinlagerung / tbDicke_Validated</summary>
        ///<remarks>Die Eingabe wird direkt auf das Format geprüft.</remarks>    
        private void tbDicke_Validated(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            if (!Decimal.TryParse(tbDicke.Text, out decTmp))
            {
                clsMessages.Allgemein_EingabeFormatFehlerhaft();
            }
            if (decTmp < 0)
            {
                decTmp = decTmp * (-1);
            }
            tbDicke.Text = Functions.FormatDecimal(decTmp);
        }
        ///<summary>ctrEinlagerung / TrimmEingabe</summary>
        ///<remarks>Trimm über alle Artikeleingabefelder.</remarks> 
        private void TrimmEingabe()
        {
            //ID-Ref
            tbAnzahl.Text = tbAnzahl.Text.ToString().Trim();
            tbGArt.Text = tbGArt.Text.ToString().Trim();
            tbGArtZusatz.Text = tbGArtZusatz.Text.ToString().Trim();
            tbWerksnummer.Text = tbWerksnummer.Text.ToString().Trim();
            tbProduktionsNr.Text = tbProduktionsNr.Text.ToString().Trim();
            tbexBezeichnung.Text = tbexBezeichnung.Text.ToString().Trim();
            tbTransportId.Text = tbTransportId.Text.ToString().Trim();
            tbBestellnummer.Text = tbBestellnummer.Text.ToString().Trim();
            tbCharge.Text = tbCharge.Text.ToString().Trim();
            tbPos.Text = tbPos.Text.ToString().Trim();
            tbexMaterialnummer.Text = tbexMaterialnummer.Text.ToString().Trim();
            tbArtIDRef.Text = tbArtIDRef.Text.ToString().Trim();

            //Abmessungen - Gewichte
            tbDicke.Text = tbDicke.Text.ToString().Trim();
            tbBreite.Text = tbBreite.Text.ToString().Trim();
            tbLaenge.Text = tbLaenge.Text.ToString().Trim();
            tbHoehe.Text = tbHoehe.Text.ToString().Trim();
            tbNetto.Text = tbNetto.Text.ToString().Trim();
            tbBrutto.Text = tbBrutto.Text.ToString().Trim();
        }
        ///<summary>ctrEinlagerung / ClearArtikelEingabeFelder</summary>
        ///<remarks>Leert alle Eingabefelder der Artikeldaten.</remarks>       
        private void ClearArtikelEingabeFelder(bool bIsNewItem)
        {
            //ID - RefNr
            tbLVSNr.Text = string.Empty;
            tbArtikelID.Text = string.Empty;
            tbGArtZusatz.Text = string.Empty;
            tbGArtSearch.Text = string.Empty;
            tbGArt.Text = string.Empty;
            tbWerksnummer.Text = string.Empty;
            tbProduktionsNr.Text = string.Empty;
            tbCharge.Text = string.Empty;
            tbBestellnummer.Text = string.Empty;
            tbexMaterialnummer.Text = string.Empty;
            tbexBezeichnung.Text = string.Empty;
            tbTransportId.Text = string.Empty;

            //automatisches hochzählen der Position im Eingang
            Int32 iRows = 1;
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_InkrementArtikelPos)
            {
                iRows = this.dgv.Rows.Count + 1;
            }
            tbPos.Text = iRows.ToString();

            tbArtIDRef.Text = string.Empty;

            decimal decTmp = 0;
            //Maße/Gewichte
            tbAnzahl.Text = this._ctrMenu._frmMain.system.Client.Eingang_Artikel_DefaulAnzahl;

            tbDicke.Text = Functions.FormatDecimal(decTmp);
            tbBreite.Text = Functions.FormatDecimal(decTmp);
            tbLaenge.Text = Functions.FormatDecimal(decTmp);
            tbHoehe.Text = Functions.FormatDecimal(decTmp);
            tbNetto.Text = Functions.FormatDecimal(decTmp);
            tbBrutto.Text = Functions.FormatDecimal(decTmp);
            tbPackmittelGewicht.Text = Functions.FormatDecimal(decTmp);
            Functions.SetComboToSelecetedItem(ref cbEinheit, this._ctrMenu._frmMain.system.Client.Eingang_Artikel_DefaulEinheit);
            //Lagerort 
            if (bIsNewItem)
            {
                tbWerk.Text = string.Empty;
                tbHalle.Text = string.Empty;
                tbReihe.Text = string.Empty;
                tbEbene.Text = string.Empty;
                tbPlatz.Text = string.Empty;
            }
            else
            {
                if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_ClearLagerOrteByArtikelCopy)
                {
                    tbWerk.Text = string.Empty;
                    tbHalle.Text = string.Empty;
                    tbReihe.Text = string.Empty;
                    tbEbene.Text = string.Empty;
                    tbPlatz.Text = string.Empty;
                }
            }
            tbExLagerOrt.Text = string.Empty;
            tbInfoAusgang.Text = string.Empty;
            tbUBInfo.Text = string.Empty;

            //tabpage Zusatz Info
            tbArtikelArt.Text = string.Empty;
            tbExAuftrag.Text = string.Empty;
            tbExAuftragPos.Text = string.Empty;
            tbGuete.Text = string.Empty;
            dtpGlowDate.Value = Globals.DefaultDateTimeMinValue;
            nudAdrKommisssion.Value = 0;
            //nudLzzKW.Value = (decimal)Functions.GetCalendarWeek(DateTime.Now);
            //nudLzzJahr.Value =(decimal) DateTime.Now.Year;
            nudLzzKW.Value = 1;
            nudLzzJahr.Value = 1900;
            pbAbrufFreigabe.Image = null;
            tbASNVerbraucher.Text = string.Empty;
            cbArtikelIsNOTStackable.Checked = false;
            cbArtikelVerpackt.Checked = false;
            cbIsMulde.Checked = false;
            cbArtIsProblem.Checked = false;

            //tabpage Info
            tbSysInfo.Text = string.Empty;
            tbInfoIntern.Text = string.Empty;
            tbInfoExtern.Text = string.Empty;

            //Label Güterart ID
            _decGArtID = 0;
            SetLabelGArdIDInfo();

            //Info Images
            pbCheckArtikel.Image = null;
            pbAbrufFreigabe.Image = null;
            pbSchaden.Image = null;
            pbRL.Image = null;
            pbSPL.Image = null;
        }
        ///<summary>ctrEinlagerung / SetArtikelEingabefelderDatenEnable</summary>
        ///<remarks>Gibt die Eingabefelder für einen neuen Eingang frei.</remarks>
        private void SetArtikelEingabefelderDatenEnable(bool bEnabled)
        {
            bEnabled = bEnabled & (this.Lager.Eingang.LockedBy == 0 || this.Lager.Eingang.LockedBy == this.GL_User.User_ID);

            //ID - Ref
            tbLVSNr.Enabled = false;
            tbArtikelID.Enabled = false;
            tbGArt.Enabled = bEnabled;
            tbGArtSearch.Enabled = bEnabled;
            tbGArtZusatz.Enabled = bEnabled;
            tbWerksnummer.Enabled = bEnabled;
            tbProduktionsNr.Enabled = bEnabled;
            tbCharge.Enabled = bEnabled;
            tbBestellnummer.Enabled = bEnabled;
            tbexMaterialnummer.Enabled = bEnabled;
            tbexBezeichnung.Enabled = bEnabled;
            tbTransportId.Enabled = bEnabled;
            tbPos.Enabled = bEnabled;
            tbArtIDRef.Enabled = bEnabled;

            //Gewichte Abmessungen
            tbAnzahl.Enabled = bEnabled;
            tbDicke.Enabled = bEnabled;
            tbBreite.Enabled = bEnabled;
            tbLaenge.Enabled = bEnabled;
            tbHoehe.Enabled = bEnabled;
            tbNetto.Enabled = bEnabled;
            tbBrutto.Enabled = bEnabled;
            tbPackmittelGewicht.Enabled = bEnabled;

            //Lagerort
            //tbLagerort.Enabled = bEnabled;
            tbExLagerOrt.Enabled = bEnabled;
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_LagerOrt_manuell_Changeable)
            {
                tbWerk.Enabled = (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_LagerOrt_Enabled_Werk);
                tbHalle.Enabled = (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_LagerOrt_Enabled_Halle);
                tbReihe.Enabled = (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_LagerOrt_Enabled_Reihe);
                tbEbene.Enabled = (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_LagerOrt_Enabled_Ebene);
                tbPlatz.Enabled = (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_LagerOrt_Enabled_Platz);
            }
            else
            {
                tbWerk.Enabled = false;
                tbHalle.Enabled = false;
                tbReihe.Enabled = false;
                tbEbene.Enabled = false;
                tbPlatz.Enabled = false;
            }

            //tab Zusatz Infos
            if (this._ctrMenu._frmMain.system.Client.Modul.EnableEditExAuftrag == true)
            {
                tbExAuftrag.Enabled = true;
            }
            else
            {
                tbExAuftrag.Enabled = bEnabled;
            }
            tbExAuftragPos.Enabled = bEnabled;
            tbGuete.Enabled = bEnabled;
            nudAdrKommisssion.Enabled = bEnabled;
            nudLzzJahr.Enabled = bEnabled;
            nudLzzKW.Enabled = bEnabled;
            tbASNVerbraucher.Enabled = bEnabled;
            dtpGlowDate.Enabled = bEnabled;

            //READ only
            this.cbArtikelIsNOTStackable.Enabled = !(this._ctrMenu._frmMain.system.Client.Modul.ReadOnly_Artikel_IsNOTStackable);


            cbArtikelVerpackt.Enabled = bEnabled;
            cbIsMulde.Enabled = bEnabled;
            cbArtIsProblem.Enabled = bEnabled;
            //Button auch
            btnFreeForCallReset.Enabled = bEnabled;
            btnKWNow.Enabled = bEnabled;

            //Tab Info
            tbSysInfo.Enabled = bEnabled;
            tbInfoExtern.Enabled = bEnabled;
            tbInfoIntern.Enabled = bEnabled;

            //combo Einheit            
            //cbEinheit.Enabled = bEnabled;
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_Enabeled_Einheit != bEnabled)
            {
                cbEinheit.Enabled = this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_Enabeled_Einheit;
            }
            else
            {
                cbEinheit.Enabled = bEnabled;
            }

            //Button Güterarten 
            btnGArt.Enabled = bEnabled;

            //Customize
            // --- MCSearch Güterarten
            clsClient.ctrEinlagerung_CustomizeSetGArtMCEnabled(this._ctrMenu._frmMain.system.Client.MatchCode, this.tbGArtSearch);
        }
        ///<summary>ctrEinlagerung / SetEingangsID</summary>
        ///<remarks>Ermittelt und setzt die neue Lager EIngangsID.</remarks>
        private void SetLEingangsID()
        {
            decimal decTemp = 0;
            if (_MandantenID > 0)
            {
                decTemp = clsLager.GetNewLEingangID(GL_User, this._ctrMenu._frmMain.system);
            }
            tbLEingangID.Text = decTemp.ToString();
            this.Lager.Eingang.LEingangID = decTemp;
        }
        ///<summary>frmEinlagerung / SetLVSNummer</summary>
        ///<remarks>Ermittelt für den neuen Eingang die nächste LVSNr.</remarks>
        private void SetLVSNummer()
        {
            if (_MandantenID > 0)
            {
                tbLVSNr.Text = clsLager.GetNextLVSNr(GL_User, this._ctrMenu._frmMain.system).ToString();
            }
        }
        ///<summary>ctrEinlagerung / CheckArtikelEingabe</summary>
        ///<remarks>Prüflogik für die Eingabefelder der Artikeldaten.</remarks>
        private bool CheckArtikelEingabe()
        {
            bool EingabeOK = true;
            string strHelp = string.Empty;
            Int32 result = 0;
            decimal decResult = 0.0m;

            //Güterart
            if (tbGArt.Text == "")
            {
                EingabeOK = false;
                strHelp = strHelp + "Güterart fehlt \n\r";
            }
            //ME
            if (!Int32.TryParse(tbAnzahl.Text, out result))
            {
                EingabeOK = false;
                strHelp = strHelp + "Anzahl hat das falsche Eingabeformat \n\r";
            }
            //Dicke
            if (!decimal.TryParse(tbDicke.Text, out decResult))
            {
                EingabeOK = false;
                strHelp = strHelp + "Dicke hat das falsche Eingabeformat \n\r";
            }
            else
            {
                tbDicke.Text = Functions.FormatDecimal(decResult);
                decResult = 0.0m;
            }
            //Breite
            if (!decimal.TryParse(tbBreite.Text, out decResult))
            {
                EingabeOK = false;
                strHelp = strHelp + "Breite hat das falsche Eingabeformat \n\r";
            }
            else
            {
                tbBreite.Text = Functions.FormatDecimal(decResult);
                decResult = 0.0m;
            }
            //Länge
            if (!decimal.TryParse(tbLaenge.Text, out decResult))
            {
                EingabeOK = false;
                strHelp = strHelp + "Länge hat das falsche Eingabeformat \n\r";
            }
            else
            {
                tbLaenge.Text = Functions.FormatDecimal(decResult);
                decResult = 0.0m;
            }
            //Höhe
            if (!decimal.TryParse(tbHoehe.Text, out decResult))
            {
                EingabeOK = false;
                strHelp = strHelp + "Höhe hat das falsche Eingabeformat \n\r";
            }
            else
            {
                tbHoehe.Text = Functions.FormatDecimal(decResult);
                decResult = 0.0m;
            }
            //Netto
            if (!decimal.TryParse(tbNetto.Text, out decResult))
            {
                EingabeOK = false;
                strHelp = strHelp + "Netto hat das falsche Eingabeformat \n\r";
            }
            else
            {
                tbNetto.Text = Functions.FormatDecimal(decResult);
                decResult = 0.0m;
            }
            //Brutto
            if (!decimal.TryParse(tbBrutto.Text, out decResult))
            {
                EingabeOK = false;
                strHelp = strHelp + "Brutto hat das falsche Eingabeformat \n\r";
            }
            else
            {
                tbBrutto.Text = Functions.FormatDecimal(decResult);
                decResult = 0.0m;
            }

            if (!EingabeOK)
            {
                MessageBox.Show(strHelp, "Achtung");
            }
            return EingabeOK;
        }
        ///<summary>ctrEinlagerung / button1_Click</summary>
        ///<remarks>Öffnen der Güterartenliste zur Suche / Übernahme der Güterart. Nur wenn ein Artikel (LVSNR) vorhanden ist.</remarks>
        private void button1_Click(object sender, EventArgs e)
        {
            decimal decTmp = -1;
            if (Decimal.TryParse(tbLVSNr.Text, out decTmp))
            {
                this._ctrMenu.OpenFrmGArtenList(this);
            }
        }
        ///<summary>ctrEinlagerung / TakeOverGueterArt</summary>
        ///<remarks>Übernahme der gewählten Güterart</remarks>
        public void TakeOverGueterArt(decimal gaID)
        {
            Lager.Artikel.GArtID = gaID;
            _decGArtID = gaID;
            SetGArtDatenFromSelectedGut();
            SetFocusAfterSetGut();
        }
        /// <summary>
        ///            Es werden die einzelnen Textboxen geprüft, da wo die Prüfung positiv ist, wird der Focus gesetzt
        ///            Reihenfolge Textboxen
        ///            tbGArtZusatz
        ///            tbPos
        ///            tbWerksnummer
        ///            tbProduktionsNr
        ///            tbCharge
        ///            tbexMaterialnummer
        /// </summary>
        private void SetFocusAfterSetGut()
        {
            //--tbGArtZusatz
            if (
                 (!tbGArtZusatz.ReadOnly) && (tbGArtZusatz.Text.Equals(string.Empty))
               )
            {
                tbGArtZusatz.Focus();
            }
            //-- tbPos
            else if (
                        (!tbPos.ReadOnly) && (tbPos.Text.Equals(string.Empty))
                    )
            {
                tbPos.Focus();
            }
            //-- tbWerksnummer
            else if (
                        (!tbWerksnummer.ReadOnly) && (tbWerksnummer.Text.Equals(string.Empty))
                    )
            {
                tbWerksnummer.Focus();
            }
            //-- tbProduktionsNr
            else if (
                        (!tbProduktionsNr.ReadOnly) && (tbProduktionsNr.Text.Equals(string.Empty))
                    )
            {
                tbProduktionsNr.Focus();
            }
            //-- tbCharge
            else if (
                        (!tbCharge.ReadOnly) && (tbCharge.Text.Equals(string.Empty))
                    )
            {
                tbCharge.Focus();
            }
            //-- tbexMaterialnummer
            else if (
                        (!tbexMaterialnummer.ReadOnly) && (tbexMaterialnummer.Text.Equals(string.Empty))
                    )
            {
                tbexMaterialnummer.Focus();
            }

            else
            {
                //--- Alle Textboxen sind gefüllt, dann nun die Textbox, die !ReadOnly ist
                //--tbGArtZusatz
                if (!tbGArtZusatz.ReadOnly)
                {
                    tbGArtZusatz.Focus();
                }
                //-- tbPos
                else if (!tbPos.ReadOnly)
                {
                    tbPos.Focus();
                }
                //-- tbWerksnummer
                else if (!tbWerksnummer.ReadOnly)
                {
                    tbWerksnummer.Focus();
                }
                //-- tbProduktionsNr
                else if (!tbProduktionsNr.ReadOnly)
                {
                    tbProduktionsNr.Focus();
                }
                //-- tbCharge
                else if (!tbCharge.ReadOnly)
                {
                    tbCharge.Focus();
                }
                //-- tbexMaterialnummer
                else if (!tbexMaterialnummer.ReadOnly)
                {
                    tbexMaterialnummer.Focus();
                }
            }
        }
        ///<summary>ctrEinlagerung / SetGArtDatenFromSelectedGut</summary>
        ///<remarks>Aritkel schliessen.</remarks>  
        private void SetGArtDatenFromSelectedGut()
        {
            if (Lager.Artikel.GArt.ID > 0)
            {
                tbGArtSearch.Text = Lager.Artikel.GArt.ViewID;
                tbGArt.Text = Lager.Artikel.GArt.Bezeichnung;
                tbGArtZusatz.Text = Lager.Artikel.GArt.Zusatz;
                tbArtikelArt.Text = Lager.Artikel.GArt.ArtikelArt;
                Functions.SetComboToSelecetedValue(ref cbEinheit, this._ctrMenu._frmMain.system.Client.Eingang_Artikel_DefaulEinheit);

                if (this._ctrMenu._frmMain.system.AbBereich.ASNTransfer)
                {
                    if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinitionByASNTransfer)
                    {
                        if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_GArt_InfoMessageAllData)
                        {
                            //Der KUnde möchte vorher noch ein abfrage haben ob die Daten wirklich übernommen werden sollen
                            if (clsMessages.Artikel_GetAllGArtenData())
                            {
                                SetGArtValueToCtr();
                            }
                        }
                        else
                        {
                            SetGArtValueToCtr();
                        }
                    }
                }
                else
                {
                    if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinition)
                    {
                        if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_GArt_InfoMessageAllData)
                        {
                            //Der KUnde möchte vorher noch ein abfrage haben ob die Daten wirklich übernommen werden sollen
                            if (clsMessages.Artikel_GetAllGArtenData())
                            {
                                SetGArtValueToCtr();
                            }
                        }
                        else
                        {
                            SetGArtValueToCtr();
                        }
                    }
                }
            }
        }
        ///<summary>ctrEinlagerung / SetGArtValueToCtr</summary>
        ///<remarks>Aritkel schliessen.</remarks> 
        private void SetGArtValueToCtr()
        {
            tbDicke.Text = Functions.FormatDecimal(Lager.Artikel.GArt.Dicke);
            tbBreite.Text = Functions.FormatDecimal(Lager.Artikel.GArt.Breite);
            tbLaenge.Text = Functions.FormatDecimal(Lager.Artikel.GArt.Laenge);
            tbWerksnummer.Text = Lager.Artikel.GArt.Werksnummer;
            tbBestellnummer.Text = Lager.Artikel.GArt.BestellNr;
            tbNetto.Text = Functions.FormatDecimal(Lager.Artikel.GArt.Netto);
            tbBrutto.Text = Functions.FormatDecimal(Lager.Artikel.GArt.Brutto);

            tbPackmittelGewicht.Text = Functions.FormatDecimal(Lager.Artikel.GArt.Brutto - Lager.Artikel.GArt.Netto);
            Functions.SetComboToSelecetedValue(ref cbEinheit, Lager.Artikel.GArt.Einheit);
            if (cbEinheit.SelectedIndex < 0)
            {
                Functions.SetComboToSelecetedValue(ref cbEinheit, this._ctrMenu._frmMain.system.Client.Eingang_Artikel_DefaulEinheit);
            }
        }
        ///<summary>ctrEinlagerung / tsbtnArtikelSave_Click</summary>
        ///<remarks>Aritkel schliessen.</remarks>  
        private void tsbtnArtikelSave_Click(object sender, EventArgs e)
        {
            SaveArtikel();
        }
        ///<summary>ctrEinlagerung / SaveArtikel</summary>
        ///<remarks></remarks> 
        private bool SaveArtikel()
        {
            bool bSave = true;
            string strInfo = string.Empty;
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_Artikel_RequiredValue_Produktionsnummer)
            {
                if (this.tbProduktionsNr.Text == string.Empty)
                {
                    strInfo = strInfo + "- Feld [Produktionnummer]" + Environment.NewLine;
                }
            }
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_Artikel_RequiredValue_Netto)
            {
                decimal decTmp = 0;
                Decimal.TryParse(tbNetto.Text, out decTmp);
                if (decTmp == 0)
                {
                    strInfo = strInfo + "- Feld [Netto]" + Environment.NewLine;
                }
            }
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_Artikel_RequiredValue_Brutto)
            {
                decimal decTmp = 0;
                Decimal.TryParse(tbBrutto.Text, out decTmp);
                if (decTmp == 0)
                {
                    strInfo = strInfo + "- Feld [Brutto]" + Environment.NewLine;
                }
            }
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_Artikel_RequiredValue_Laenge)
            {
                this.Lager.Artikel.GArt.ID = this.Lager.Artikel.GArtID;
                this.Lager.Artikel.GArt.Fill();
                string strCheck = "_" + this.Lager.Artikel.GArt.ArtikelArt;
                Int32 iCheck = strCheck.IndexOf("Coil", StringComparison.CurrentCultureIgnoreCase);
                if (iCheck > 0)
                {
                    decimal decTmp = 0;
                    Decimal.TryParse(tbLaenge.Text, out decTmp);
                    if (decTmp == 0)
                    {
                        strInfo = strInfo + "- Feld [Länge]" + Environment.NewLine;
                    }
                }
            }
            //Ausgabe Error Text
            if (strInfo != string.Empty)
            {
                string strKopf = "Der Artikel kann nicht gespeichert werden, da folgende Pflichtfelder nicht gefüllt sind: " + Environment.NewLine;
                strInfo = strKopf + strInfo + Environment.NewLine;
                clsMessages.Allgemein_ERRORTextShow(strInfo);
                bSave = false;
            }
            else
            {
                SaveArtikelDaten();

            }
            return bSave;
        }
        ///<summary>ctrEinlagerung / SaveArtikelDaten</summary>
        ///<remarks>Artikel wird gespeichert</remarks>  
        private void SaveArtikelDaten()
        {
            //Userberechigung prüfen
            //if ((GL_User.write_LagerEingang) && (this.Lager.Artikel.ID>0))
            if (GL_User.write_LagerEingang)
            {
                TrimmEingabe();
                if (CheckArtikelEingabe())
                {
                    //Check ob ArtIDRef erzeugt ist                   
                    string strText = this.Lager.Artikel.ID.ToString();
                    clsArtikel TmpArt = new clsArtikel();
                    TmpArt.sys = this._ctrMenu._frmMain.system;
                    TmpArt = Lager.Artikel;
                    Lager.Artikel.MandantenID = this._ctrMenu._frmMain.system.AbBereich.MandantenID;
                    Lager.Artikel.AbBereichID = this._ctrMenu._frmMain.system.AbBereich.ID;

                    Lager.Artikel.LEingangTableID = Lager.LEingangTableID;
                    Lager.Artikel.LVS_ID = Convert.ToDecimal(tbLVSNr.Text);
                    Lager.Artikel.GArtID = _decGArtID;
                    Lager.Artikel.GutZusatz = tbGArtZusatz.Text;
                    Lager.Artikel.Werksnummer = tbWerksnummer.Text;
                    Lager.Artikel.Produktionsnummer = tbProduktionsNr.Text;
                    Lager.Artikel.Charge = tbCharge.Text;
                    Lager.Artikel.Bestellnummer = tbBestellnummer.Text;
                    Lager.Artikel.exMaterialnummer = tbexMaterialnummer.Text;
                    Lager.Artikel.exBezeichnung = tbexBezeichnung.Text;
                    Lager.Artikel.TARef = tbTransportId.Text;
                    Lager.Artikel.Position = tbPos.Text;
                    Lager.Artikel.Anzahl = Convert.ToInt32(tbAnzahl.Text.ToString().Trim());
                    Lager.Artikel.ArtIDRef = this._ctrMenu._frmMain.system.Client.CreateArtikelIDRef(this.Lager.Artikel);

                    Lager.Artikel.Dicke = Convert.ToDecimal(tbDicke.Text);
                    Lager.Artikel.Breite = Convert.ToDecimal(tbBreite.Text);
                    Lager.Artikel.Laenge = Convert.ToDecimal(tbLaenge.Text);
                    Lager.Artikel.Hoehe = Convert.ToDecimal(tbHoehe.Text);
                    Lager.Artikel.Netto = Convert.ToDecimal(tbNetto.Text);
                    Lager.Artikel.Brutto = Convert.ToDecimal(tbBrutto.Text);

                    //Lagerort
                    Lager.Artikel.LagerOrt = Lager.LagerOrt.LagerPlatzID;
                    Lager.Artikel.LagerOrtTable = Lager.LagerOrt.LOTable;
                    Lager.Artikel.Werk = tbWerk.Text.Trim();
                    Lager.Artikel.Halle = tbHalle.Text.Trim();
                    Lager.Artikel.Reihe = tbReihe.Text.Trim();
                    Lager.Artikel.Ebene = tbEbene.Text.Trim();
                    Lager.Artikel.Platz = tbPlatz.Text.Trim();

                    //ex LAgerort
                    Lager.Artikel.exLagerOrt = tbExLagerOrt.Text.ToString().Trim();
                    //Einheiten
                    if (cbEinheit.SelectedValue != null)
                    {
                        Lager.Artikel.Einheit = cbEinheit.SelectedValue.ToString();
                    }

                    //tabpage Zusatz Infos

                    Lager.Artikel.exAuftrag = tbExAuftrag.Text.Trim();
                    Lager.Artikel.exAuftragPos = tbExAuftragPos.Text.Trim();
                    Lager.Artikel.Guete = tbGuete.Text.Trim();
                    Lager.Artikel.GlowDate = dtpGlowDate.Value;
                    Lager.Artikel.ADRLagerNr = nudAdrKommisssion.Value;
                    Lager.Artikel.LZZ = Functions.GetDateFromLastDayOfCalWeek((Int32)nudLzzKW.Value, (Int32)nudLzzJahr.Value);
                    //FreigabeAbruf kann nur über die Freigabe geändert werden nicht hier
                    Lager.Artikel.ASNVerbraucher = tbASNVerbraucher.Text.Trim();
                    Lager.Artikel.IsStackable = (!cbArtikelIsNOTStackable.Checked);

                    Lager.Artikel.IsVerpackt = cbArtikelVerpackt.Checked;
                    Lager.Artikel.IsMulde = cbIsMulde.Checked;
                    Lager.Artikel.IsProblem = cbArtIsProblem.Checked;

                    //tabInfo
                    Lager.Artikel.Info = tbSysInfo.Text.Trim();
                    Lager.Artikel.interneInfo = tbInfoIntern.Text.Trim();
                    Lager.Artikel.externeInfo = tbInfoExtern.Text.Trim();

                    //CHeck, ob die LVS schon vergeben und UpdateArtikel = false
                    //dann muss UpdateArtikel = true, damit sichergestellt ist, dass 
                    //die LVSNR nicht zweimal existiert ==> da kann man hier auch über
                    //die ArtikelTableID gehen 

                    if (Lager.Artikel.ID > 0)
                    {
                        Lager.Artikel.ID = TmpArt.ID;
                        Lager.Artikel.EingangChecked = TmpArt.EingangChecked;
                        Lager.Artikel.LAusgangTableID = TmpArt.LAusgangTableID;
                        Lager.Artikel.FreigabeAbruf = TmpArt.FreigabeAbruf;
                        Lager.Artikel.IsLabelPrint = TmpArt.IsLabelPrint;
                        Lager.Artikel.IsKorStVerUse = TmpArt.IsKorStVerUse;
                        Lager.Artikel.ArtIDAlt = TmpArt.ArtIDAlt;

                        if (TmpArt.LAusgangTableID == 0)
                        {
                            Lager.Artikel.BKZ = 1;
                        }
                        else
                        {
                            Lager.Artikel.BKZ = 0;
                        }
                        Lager.Artikel.UpdateArtikelLager();
                    }
                    else
                    {
                        //Insert Daten
                        Lager.Artikel.BKZ = 1;
                        Lager.Artikel.AddArtikelLager(false);
                    }
                    //this.tbArtIDRef.Text = Lager.Artikel.ArtIDRef;

                    //Da die Daten jetzt sichtbar sind und geblättert werden kann muss nun Update = true gesetzt werden
                    bUpdate = true;
                    //Artikel neu auf die From setzen, damit die entsprechenden Button und
                    //Infos geladen und gesetzt werden können
                    EingangBrowse(0, this.Lager.Artikel.ID, enumBrowseAcivity.ArtInItem);
                }
            }
        }

        private void tsbtnFreeEingang_Click(object sender, EventArgs e)
        {

        }
        ///<summary>ctrEinlagerung / SetArtikelMenuBtnEnabled</summary>
        ///<remarks>Das Artikelmenü wird aktiviert / deaktiviert.</remarks>   
        private void SetArtikelMenuBtnEnabled(bool bLocked = false)
        {
            bool bInLager;

            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_USEBKZ)
            {
                bInLager = Lager.Artikel.BKZ == 1;
            }
            else
            {
                bInLager = !this.Lager.Artikel.AusgangChecked;
            }
            if (bInLager && (this.Lager.Eingang.LockedBy == 0 || this.Lager.Eingang.LockedBy == this.GL_User.User_ID))
            {
                //Eingang offen
                //Wenn der Eingang abgeschlossen ist dürfen keine Aritkel mehr 
                //hinzugefügt werden können
                tsbtnArtikelNeu.Enabled = (!Lager.Eingang.Checked);
                if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_LagerOrt_manuell_Changeable)
                {
                    tsbtnArtikelSave.Enabled = true;
                }
                else
                {
                    tsbtnArtikelSave.Enabled = (!Lager.Eingang.Checked);
                }
                tsbtnArtikelCopy.Enabled = (!Lager.Eingang.Checked);
                //Immer verfügbar
                //Artikel können mometan noch so gelöscht werden
                //sobald SZG hinzu kommt musss das löschen über das Korrektur / Stornierungsverfahren laufen
                tsbtnArtikelDelete.Enabled = true;

                //Umbuchung 
                //-nur wenn der Artikel bzw.der Eingang abgeschlossen ist
                bool bUBEnabled = Lager.Eingang.Checked;

                //Sperrlager
                //-Artikel im Sperrlager können nicht umgebucht werden und 
                //-nicht noch einmal ins Sperrlager gesetzt werden
                //-Eingang muss abgeschlossen sein, damit der Artikel ins SPL gebucht werden kann (wichtig für EDI)
                bool bSPLEnabled = Lager.Eingang.Checked;

                if (Lager.Eingang.Checked)
                {
                    bUBEnabled = (!this.Lager.Artikel.bSPL);
                    bSPLEnabled = (!this.Lager.Artikel.bSPL);
                }
                tsbtnSPL.Enabled = bSPLEnabled;
                tsbtnUB.Enabled = bUBEnabled;

                if (tsbtnArtKorrStorVerfahren.Visible)
                {
                    //Checkliste für SKV
                    //- Check besteht DFÜ Kommunikation im Arbeitsbereich
                    //- Check Lieferant und Empfänger gleich(vW oder BMW)
                    //- Check besteht DFÜ-Verbindung zwischen Auftraggeber und Lieferant
                    //	-> nein->SKV aktiv
                    //  ->ja(wenn eine DFÜ Verbindung besteht, darf SKV nur aktiv sein, wenn EM raus sind)
                    //      ->Check EM, AM
                    //         ->EM vorhanden->SKV aktiv
                    //      ->EM nicht vorhanden -> SKV deaktiviert

                    if (this.Lager.Eingang.Auftraggeber.Equals(this.Lager.Eingang.Empfaenger))
                    //if (this.Lager.Artikel.Eingang.Auftraggeber.Equals(this.Lager.Artikel.Eingang.Empfaenger))
                    {
                        if (
                                (this.Lager.Artikel.Ausgang is clsLAusgang) &&
                                (!this.Lager.Artikel.Ausgang.Checked)
                           )
                        {
                            tsbtnArtKorrStorVerfahren.Enabled = true;
                        }
                    }
                    else if (
                            (this.Lager.Eingang.LEingangTableID > 0) &&
                            (this.Lager.Eingang.AdrAuftraggeber.AdrVerweis.CheckEdiCommForAdr(this.Lager.Eingang.Empfaenger))
                        )
                    {
                        if ((this._ctrMenu._frmMain.system.AbBereich.ASNTransfer) && (this.Lager.Artikel.Lagermeldungen != null))
                        {
                            tsbtnArtKorrStorVerfahren.Enabled = ((this.Lager.Artikel.Lagermeldungen.LM_EME) && (!this.Lager.Artikel.IsKorStVerUse));
                        }
                        else
                        {
                            tsbtnArtKorrStorVerfahren.Enabled = false;
                        }
                    }
                    else
                    {
                        tsbtnArtKorrStorVerfahren.Enabled = true;
                    }
                    tsbtnArtikelRL.Enabled = true;
                }
                else
                {
                    if (this.Lager.Eingang.Auftraggeber.Equals(this.Lager.Eingang.Empfaenger))
                    {
                        if (
                                (this.Lager.Artikel.Ausgang is clsLAusgang) &&
                                (!this.Lager.Artikel.Ausgang.Checked)
                           )
                        {
                            tsbtnArtKorrStorVerfahren.Enabled = true;
                        }
                    }
                }

                //man. Abruf nur wenn der Eingang abgeschlossen ist
                this.tsbtnManCall.Enabled = Lager.Eingang.Checked;

                //Lagerort ist immer buchbar
                tsbtnLagerort.Enabled = true;
                tsbtnExLagerOrt.Enabled = true;
                tsbtnSchaden.Enabled = true;
                tsbtnLagerort.Enabled = true;
            }
            else
            {
                //Artikel ist ausgebucht - Menü deaktivieren
                tsbtnArtikelNeu.Enabled = false;
                tsbtnArtikelSave.Enabled = false;
                tsbtnArtikelCopy.Enabled = false;
                tsbtnLagerort.Enabled = false;
                tsbtnExLagerOrt.Enabled = false;
                tsbtnLagerort.Enabled = false;
                tsbtnUB.Enabled = false;
                tsbtnArtikelDelete.Enabled = false;
                tsbtnSchaden.Enabled = false;
                tsbtnSPL.Enabled = false;
                //man. Abruf nur wenn der Eingang abgeschlossen ist
                this.tsbtnManCall.Enabled = false;
                if (tsbtnArtKorrStorVerfahren.Visible)
                {
                    tsbtnArtKorrStorVerfahren.Enabled = false;
                    tsbtnArtikelRL.Enabled = false;
                }
            }
        }
        ///<summary>ctrEinlagerung / pbCheckArtikel_Click</summary>
        ///<remarks>Artikel prüfen.</remarks>   
        private void pbCheckArtikel_Click(object sender, EventArgs e)
        {
            //-- 2016-12-20
            //-bereits ausgelagerte Artikel dürfen nicht mehr geändert werden können
            if (
                (this.Lager.Artikel.ID > 0) &&
                (this.Lager.Artikel.LAusgangTableID == 0)
                )
            {
                if (SaveArtikel())
                {
                    if (this.Lager.Artikel.ID > 0)
                    {
                        SetArtikelToForm(this.Lager.Artikel.ID, false);
                        //Halle
                        string strInfo = string.Empty;
                        this.Lager.Artikel.LEingangTableID = this.Lager.Eingang.LEingangTableID;
                        if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_RequiredValue_Halle)
                        {
                            if (this.Lager.Artikel.Halle.Equals(string.Empty))
                            {
                                strInfo = strInfo + "- Feld [Halle] " + Environment.NewLine;
                            }
                        }
                        if (this.Lager.Artikel.Gut.Equals(string.Empty))
                        {
                            strInfo = strInfo + "- Feld [Warengruppe/Güterart] " + Environment.NewLine;
                        }

                        if (strInfo.Equals(string.Empty))
                        {
                            if (!Lager.Eingang.Checked)
                            {
                                SetArtikelCheckStatus();
                                clsArtikel.UpdateArtikelCheck(this.GL_User, _bArtikelIsChecked, this.Lager.Artikel.ID);
                                if (this.Lager.Eingang.AllArtikelChecked)
                                {
                                    if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
                                    {
                                        lastprintID = this.Lager.Eingang.LEingangTableID;
                                        DirectPrintLEingangDoc();
                                        DirectPrintList();
                                    }
                                    else
                                    {
                                        if (DoDirectPrintReport(enumIniDocKey.Eingangsliste))
                                        {
                                            this.Lager.Eingang.UpdatePrintLEingang(enumIniDocKey.Eingangsliste.ToString());
                                        }
                                    }
                                    CheckDirectPrintLabel();
                                }
                            }
                        }
                        else
                        {
                            string strKopf = "Der Artikel kann nicht abgeschlossen werden, da die Prüfung folgender Felder negativ verlaufen ist:" + Environment.NewLine;
                            strInfo = strKopf + strInfo;
                            clsMessages.Allgemein_ERRORTextShow(strInfo);
                        }
                    }
                    InitSelectedTabPage(tabArtikel.SelectedTab.Name.ToString());
                    SetArtikelMenuBtnEnabled(); //CF 
                }
            }
            else
            {
                string strMe = string.Empty;
                if (this.Lager.Artikel.LAusgangTableID > 0)
                {
                    strMe = "Der Artikelstatus kann nicht geändert werden, da der Artikel bereits ausgelagert wurde!";
                }
                else
                {
                    strMe = "Der Artikelstatus kann nicht geändert werden!";
                }
                clsMessages.Allgemein_ERRORTextShow(strMe);
            }
        }
        ///<summary>ctrEinlagerung / SetArtikelCheckStatus</summary>
        ///<remarks></remarks>  
        private void SetArtikelCheckStatus()
        {
            if (!_bArtikelIsChecked)
            {
                //Artikel checked
                pbCheckArtikel.Image = (Image)Sped4.Properties.Resources.check;
                _bArtikelIsChecked = true;
                SetArtikelMenuBtnEnabled();
                tsbtnArtikelNeu.Enabled = true;
            }
            else
            {
                pbCheckArtikel.Image = (Image)Sped4.Properties.Resources.warning.ToBitmap();
                _bArtikelIsChecked = false;
                SetArtikelMenuBtnEnabled();
            }

            SetArtikelEingabefelderDatenEnable(!_bArtikelIsChecked);
            this.pbCheckArtikel.Refresh();

        }
        ///<summary>ctrEinlagerung / SetLabelGArdIDInfo</summary>
        ///<remarks>Set den GüterartID Info.</remarks>  
        private void SetLabelGArdIDInfo()
        {
            lGArtID.Text = _lTextGArtID + Lager.Artikel.GArt.ID;
        }
        /*********************************************************************************************** 
         *                                      D G V
         ***********************************************************************************************/
        ///<summary>ctrEinlagerung / InitDGV</summary>
        ///<remarks>Init - füllt das Datagrid mit den Artikel des Eingangs.</remarks>
        public void InitDGV(bool refresh = false)
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

            decimal decOldArtikelTableID = this.Lager.Artikel.ID;
            dtArtikel.Rows.Clear();

            clsArtikel art = new clsArtikel();
            art._GL_User = GL_User;
            dgv.DataSource = null;
            dtArtikel = art.GetArtikelForLEingangGrd(Lager.LEingangTableID);

            //Ermittlung und Anzeige im Eingangskopf von Anzahl und Gewicht
            tbEArtAnzahl.Text = dtArtikel.Rows.Count.ToString();
            if (dtArtikel.Rows.Count > 0)
            {
                Functions.setView(ref dtArtikel, ref dgv, "LEingang", tscbView.SelectedItem.ToString(), GL_System, false);
                //gewählter Artikel wird auf die Form gesetzt
                if (!refresh)
                {
                    SetSelectedRowInDGV();
                }
                dgv.BestFitColumns();
            }
            else
            {
                dtArtikel.Rows.Clear();
            }
            SetLEingangGesamtGewichte();
            //SetArtikelMenuBtnEnabled();
        }
        ///<summary>ctrEinlagerung / SetLEingangGesamtGewichte</summary>
        ///<remarks>Ermittelt das Netto- und Bruttogesamtgewicht.</remarks>
        private void SetLEingangGesamtGewichte()
        {
            object objNetto = 0;
            object objBrutto = 0;

            if (dtArtikel.Rows.Count > 0)
            {
                objNetto = dtArtikel.Compute("SUM(Netto)", "LVSNr>0");
                objBrutto = dtArtikel.Compute("SUM(Brutto)", "LVSNr>0");
            }
            decimal decTmp = 0;
            Decimal.TryParse(objNetto.ToString(), out decTmp);
            tbNettoGesamt.Text = Functions.FormatDecimal(decTmp);
            decTmp = 0;
            Decimal.TryParse(objBrutto.ToString(), out decTmp);
            tbBruttoGesamt.Text = Functions.FormatDecimal(decTmp);
        }
        ///<summary>ctrEinlagerung / SetArtikelToForm</summary>
        ///<remarks>Init - füllt das Datagrid mit den Artikel des Eingangs.</remarks>
        public void SetArtikelToForm(decimal myDecArtikelTableID, bool IsArtCopy)
        {
            this.Lager.Artikel.ID = myDecArtikelTableID;
            //_ArtikelTableID = this.Lager.Artikel.ID;

            //if (this.Lager.Artikel.GetArtikeldatenByTableID())
            //{
            if (IsArtCopy)
            {
                //Bei einer Artikelkopie muss eine neue LVSNr ermittelt werden und die wichtigsten 
                //Refdaten bleiben leer
                GetNewLVSNr();
                tbProduktionsNr.Text = string.Empty;
                tbWerksnummer.Text = string.Empty;
                tbArtIDRef.Text = string.Empty;
                //Flag für AritkelUpdate setzen
                bUpdateArtikel = false;
                //Lagerort
                //tbLagerort.Text = string.Empty;
                Lager.LagerOrt.LagerPlatzID = 0;
                tbWerk.Text = string.Empty;
                tbHalle.Text = string.Empty;
                tbReihe.Text = string.Empty;
                tbEbene.Text = string.Empty;
                tbPlatz.Text = string.Empty;
            }
            else
            {
                _LVSNr = this.Lager.Artikel.LVS_ID;
                tbArtIDRef.Text = this.Lager.Artikel.ArtIDRef;
                tbProduktionsNr.Text = this.Lager.Artikel.Produktionsnummer;

                //Flag für AritkelUpdate setzen
                bUpdateArtikel = true;
                //Lagerort
                Lager.LagerOrt.LagerPlatzID = Lager.Artikel.LagerOrt;
                Lager.LagerOrt.LOTable = Lager.Artikel.LagerOrtTable;
                Lager.LagerOrt.InitLagerPlatz();
                tbWerk.Text = Lager.Artikel.Werk;
                tbHalle.Text = Lager.Artikel.Halle;
                tbReihe.Text = Lager.Artikel.Reihe;
                tbEbene.Text = Lager.Artikel.Ebene;
                tbPlatz.Text = Lager.Artikel.Platz;
            }

            //exLagerort
            tbExLagerOrt.Text = this.Lager.Artikel.exLagerOrt;

            //lGArtID
            SetLabelGArdIDInfo();
            tbGArtSearch.Text = this.Lager.Artikel.GArt.ViewID;
            tbGArt.Text = this.Lager.Artikel.Gut;
            tbGArtZusatz.Text = this.Lager.Artikel.GutZusatz;
            tbLVSNr.Text = _LVSNr.ToString();
            tbArtikelID.Text = this.Lager.Artikel.ID.ToString();
            tbWerksnummer.Text = this.Lager.Artikel.Werksnummer;

            tbCharge.Text = this.Lager.Artikel.Charge;
            tbBestellnummer.Text = this.Lager.Artikel.Bestellnummer;
            tbexMaterialnummer.Text = this.Lager.Artikel.exMaterialnummer;
            tbexBezeichnung.Text = this.Lager.Artikel.exBezeichnung;
            tbTransportId.Text = this.Lager.Artikel.TARef;
            tbPos.Text = this.Lager.Artikel.Position;

            //Einheit
            Functions.SetComboToSelecetedItem(ref cbEinheit, this.Lager.Artikel.Einheit);
            //ArtikelCheck
            _bArtikelIsChecked = this.Lager.Artikel.EingangChecked;
            SetLEingangArtikelCheck();
            //Maße und Gewicht
            tbAnzahl.Text = this.Lager.Artikel.Anzahl.ToString();
            cbEinheit.SelectedText = this.Lager.Artikel.Einheit;

            tbDicke.Text = Functions.FormatDecimal(this.Lager.Artikel.Dicke);
            tbBreite.Text = Functions.FormatDecimal(this.Lager.Artikel.Breite);
            tbLaenge.Text = Functions.FormatDecimal(this.Lager.Artikel.Laenge);
            tbHoehe.Text = Functions.FormatDecimal(this.Lager.Artikel.Hoehe);
            tbNetto.Text = Functions.FormatDecimal(this.Lager.Artikel.Netto);
            tbBrutto.Text = Functions.FormatDecimal(this.Lager.Artikel.Brutto);
            decimal decPackMittel = (this.Lager.Artikel.Brutto - this.Lager.Artikel.Netto);
            tbPackmittelGewicht.Text = Functions.FormatDecimal(decPackMittel);
            //Info Ausgang
            tbInfoAusgang.Text = string.Empty;
            if (this.Lager.Artikel.BKZ == 0)
            {
                //Info, damit hier bei dem Artikel sichtbar wird, ob der Artikel schon ausgelagert ist
                //da der Artikel auch schon im alten System ausgelagert sein konnte hier dann die Info
                if (this.Lager.Artikel.EAAusgangAltLVS != "0")
                {
                    tbInfoAusgang.Text = "altes LVS: " + this.Lager.Artikel.EAAusgangAltLVS;
                }
                if (this.Lager.Artikel.EAAusgangAltLVS == null)
                {
                    // tbInfoAusgang.Text = "altes LVS: " + this.Lager.Artikel.EAAusgangAltLVS;
                }

            }

            string strInfoAusgangText = string.Empty;
            if (this.Lager.Artikel.LAusgangTableID > 0)
            {
                if (this.Lager.Artikel.IsRL)
                {
                    strInfoAusgangText = strInfoAusgangText + "RL - ";
                }
                strInfoAusgangText = strInfoAusgangText + "Ausgang: " + this.Lager.Artikel.Ausgang.LAusgangID.ToString() + " - [" + this.Lager.Artikel.Ausgang.LAusgangTableID.ToString() + "]"
                                     + Environment.NewLine +
                                     "Datum: \t" + this.Lager.Artikel.Ausgang.LAusgangsDate.ToShortDateString() +
                                            " " +
                                            this.Lager.Artikel.Ausgang.LAusgangsDate.ToShortTimeString();

                if (this.Lager.Artikel.Call is clsASNCall)
                {
                    switch (this.Lager.Artikel.Call.Status)
                    {
                        case clsASNCall.const_Status_NotExist:
                        case clsASNCall.const_Status_erstellt:
                            break;
                        case clsASNCall.const_Status_bearbeitet:
                        case clsASNCall.const_Status_MAT:
                        case clsASNCall.const_Status_ENTL:
                            strInfoAusgangText += Environment.NewLine;
                            strInfoAusgangText += "Status: " + this.Lager.Artikel.Call.Status;
                            break;
                    }
                }
            }
            tbInfoAusgang.Text = strInfoAusgangText;
            clsClient.ctrEinlagerung_tbInfoAusgang_SetText(this._ctrMenu._frmMain.system.Client.MatchCode, ref this.Lager, ref this.tbInfoAusgang);
            clsClient.ctrEinlagerung_tbInfoAusgang_SetBackColor(this._ctrMenu._frmMain.system.Client.MatchCode, ref this.Lager, ref this.tbInfoAusgang);

            //--- UB INFO
            tbUBInfo.Text = string.Empty;
            if (this.Lager.Artikel.Eingang.Retoure)
            {
                tbUBInfo.Text = "[R] LVSNr alt: " + this.Lager.Artikel.LVSNrBeforeUB.ToString();
            }
            else
            {
                if (this.Lager.Artikel.ArtIDAlt > 0)
                {
                    tbUBInfo.Text = "[UB] LVSNr alt: " + this.Lager.Artikel.LVSNrBeforeUB.ToString();
                }
                if (this.Lager.Artikel.LVSNrAfterUB > 0)
                {
                    if (this.Lager.Artikel.Umbuchung)
                    {
                        tbUBInfo.Text = "[UB] LVSNr neu: " + this.Lager.Artikel.LVSNrAfterUB.ToString();
                    }
                    else
                    {
                        tbUBInfo.Text = "[R] LVSNr neu: " + this.Lager.Artikel.LVSNrAfterUB.ToString();
                    }
                }
                //this._ctrMenu._frmMain.system.Client.ctrEinlagerung_tbUBInfo_SetBackColor(ref this.Lager, ref this.tbUBInfo);
            }
            this._ctrMenu._frmMain.system.Client.ctrEinlagerung_tbUBInfo_SetBackColor(ref this.Lager, ref this.tbUBInfo);
            SetPictureBoxCallImage();

            //tabPage Zusatz Info
            tbArtikelArt.Text = this.Lager.Artikel.GArt.ArtikelArt;
            tbExAuftrag.Text = this.Lager.Artikel.exAuftrag;
            tbExAuftragPos.Text = this.Lager.Artikel.exAuftragPos;
            tbGuete.Text = this.Lager.Artikel.Guete;
            dtpGlowDate.Value = this.Lager.Artikel.GlowDate;
            nudAdrKommisssion.Value = this.Lager.Artikel.ADRLagerNr;
            //LZZ
            DateTime dtTmp = clsSystem.const_DefaultDateTimeValue_Min;
            DateTime.TryParse(this.Lager.Artikel.LZZ.ToString(), out dtTmp);
            Functions.SetKWValue(ref nudLzzKW, dtTmp);
            Functions.SetYearValue(ref nudLzzJahr, dtTmp);

            //Freigabe
            SetFreeForCallImage();
            tbASNVerbraucher.Text = this.Lager.Artikel.ASNVerbraucher;
            //cbArtikelIsNOTStackable.Checked = (!this.Lager.Artikel.GArt.IsStackable);
            cbArtikelIsNOTStackable.Checked = (!this.Lager.Artikel.IsStackable);
            cbArtikelVerpackt.Checked = this.Lager.Artikel.IsVerpackt;
            cbIsMulde.Checked = this.Lager.Artikel.IsMulde;
            cbArtIsProblem.Checked = this.Lager.Artikel.IsProblem;

            //tabInfo
            tbSysInfo.Text = this.Lager.Artikel.Info;
            tbInfoExtern.Text = this.Lager.Artikel.externeInfo;
            tbInfoIntern.Text = this.Lager.Artikel.interneInfo;

            //Schaden
            SetSchadenToFrm(this.Lager.Artikel.bSchaden);
            //RL
            SetRLToFrm(this.Lager.Artikel.IsRL);
            //SPL
            SetSPLtoFrm(this.Lager.Artikel.bSPL);
            //}
            //else
            //{
            //    _bArtikelIsChecked = false;
            //    SetLEingangArtikelCheck();
            //}
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetPictureBoxCallImage()
        {
            this.pbCallSet.Image = null;
            //-- Info Abruf / Call
            if (
                (this.Lager.Artikel.Call is clsASNCall) &&
                (this.Lager.Artikel.Call.ID > 0)
               )
            {
                if (this.Lager.Artikel.LAusgangTableID > 0)
                {
                    this.pbCallSet.Image = Sped4.Properties.Resources.check;
                }
                else
                {
                    this.pbCallSet.Image = Sped4.Properties.Resources.check_red.ToBitmap();
                }
            }
        }
        ///<summary>ctrEinlagerung / SetFreeForCallImage</summary>
        ///<remarks></remarks>
        private void SetFreeForCallImage()
        {
            //Freigabe Abruf
            if (this.Lager.Artikel.FreigabeAbruf)
            {
                pbAbrufFreigabe.Image = Sped4.Properties.Resources.check;
            }
            else
            {
                pbAbrufFreigabe.Image = Sped4.Properties.Resources.delete_16;
            }
        }
        ///<summary>ctrEinlagerung / tsbtnArtikelCopy_Click_1</summary>
        ///<remarks>Ein ausgewähter Artikle soll kopriert werden.</remarks>
        private void tsbtnArtikelCopy_Click_1(object sender, EventArgs e)
        {
            CopyArtikel();
            this.tbProduktionsNr.Focus();
        }
        ///<summary>ctrEinlagerung / CopyArtikel</summary>
        ///<remarks>Kopiervorgang Aritkel.</remarks>
        private void CopyArtikel()
        {
            if (this.Lager.Artikel.ID > 0)
            {
                clsArtikel ArtCopy = this.Lager.Artikel;
                GetNewLVSNr();
                //nicht übernommen werden
                ArtCopy.ID = 0;
                ArtCopy.LVS_ID = _LVSNr;
                ArtCopy.Produktionsnummer = string.Empty;
                ArtCopy.Charge = string.Empty;
                ArtCopy.ArtIDRef = string.Empty;
                ArtCopy.Info = string.Empty;
                ArtCopy.EingangChecked = false;

                if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_ClearLagerOrteByArtikelCopy)
                {
                    ArtCopy.Werk = string.Empty;
                    ArtCopy.Halle = string.Empty;
                    ArtCopy.Reihe = string.Empty;
                    ArtCopy.Ebene = string.Empty;
                    ArtCopy.Platz = string.Empty;
                }
                if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_InkrementArtikelPos)
                {
                    ArtCopy.Position = (this.dgv.Rows.Count + 1).ToString();
                }
                ArtCopy.AddArtikelLager();

                this.Lager.Artikel = new clsArtikel();
                this.Lager.Artikel = ArtCopy.Copy();
                EingangBrowse(0, this.Lager.Artikel.ID, enumBrowseAcivity.ArtInItem);
            }
        }
        /*************************************************************************************
         *                                    DGV / Artikel  - Menü
         * ***********************************************************************************/
        ///<summary>ctrEinlagerung / tsbtnArtikelDelete_Click</summary>
        ///<remarks>Löschen des gewählten Artikels aus dem Eingang.</remarks> 
        private void tsbtnArtikelDelete_Click(object sender, EventArgs e)
        {
            if (!_bLEingangIsChecked)
            {
                if (GL_User.write_LagerEingang)
                {
                    if (this.Lager.Artikel.ID > 0)
                    {
                        if (clsMessages.Artikel_DeleteDatenSatz())
                        {
                            clsArtikel art = new clsArtikel();
                            art._GL_User = this.GL_User;
                            art.DeleteArtikelByID(this.Lager.Artikel.ID);
                            this.Lager.Artikel.ID = 0;
                        }
                    }
                    EingangBrowse(this.Lager.LEingangTableID, 0, enumBrowseAcivity.NoSelect);
                }
            }
            else
            {
                clsMessages.Lager_ArtDeleteFailed();
            }
        }
        ///<summary>ctrEinlagerung / tsbtnArtGrid_Click</summary>
        ///<remarks>Öffnet / Schließt das Panel zur Ansicht des Artikel-Grids.</remarks>       
        private void tsbtnArtGrid_Click(object sender, EventArgs e)
        {
            if (splittConLager.Panel1Collapsed)
            {
                splittConLager.Panel1Collapsed = false;
                tsbtnArtGrid.Image = Sped4.Properties.Resources.layout_left;
                tsbtnArtGrid.Text = "Artikelliste ausblenden!";
            }
            else
            {
                splittConLager.Panel1Collapsed = true;
                tsbtnArtGrid.Image = Sped4.Properties.Resources.layout;
                tsbtnArtGrid.Text = "Artikelliste einblenden!";
            }
        }
        ///<summary>ctrEinlagerung / tsbtnArtVita_Click</summary>
        ///<remarks>Öffnet / Schließt das Panel zur Ansicht der Artikel-Vita.</remarks>       
        private void tsbtnArtVita_Click(object sender, EventArgs e)
        {
            if (splitConArtikelDaten.Panel2Collapsed)
            {
                splitConArtikelDaten.Panel2Collapsed = false;
            }
            else
            {
                splitConArtikelDaten.Panel2Collapsed = true;
            }
        }
        ///<summary>ctrEinlagerung / tsbtnArtikelNeu_Click</summary>
        ///<remarks>Ein neuer Artikel soll angelegt werden. Vorgänge:
        ///         - Eingabefelder leeren
        ///         - neue LVS Nr holen</remarks>       
        private void tsbtnArtikelNeu_Click(object sender, EventArgs e)
        {
            //Flag für update setzen
            _LVSNr = 0;
            //_ArtikelTableID = 0;
            bUpdateArtikel = false;
            _bArtikelIsChecked = false;
            Lager.LagerOrt.LagerPlatzID = 0;
            //Eingabefelder leeren
            ClearArtikelEingabeFelder(true);
            //Freigeben der Eingabefelder
            SetArtikelEingabefelderDatenEnable(true);
            //neue LVS NR
            if (GetNewLVSNr())
            {
                //neue LVSNR
                tbLVSNr.Text = _LVSNr.ToString();
                //ArtikelMenübutton aktivieren
                SetArtikelMenuBtnEnabled();
                tsbtnArtikelSave.Enabled = true;
                tsbtnArtikelNeu.Enabled = true;
            }

            //this.Lager.Artikel.GArt = new clsGut();
            //this.Lager.Artikel.GArt.InitClass(this.GL_User, this.GL_System);

            tbGArtSearch.Focus();
        }
        ///<summary>ctrEinlagerung / GetNewLVSNr</summary>
        ///<remarks>Eine neue LVSNr wird ermittelt.</remarks>    
        private bool GetNewLVSNr()
        {
            if (this._ctrMenu._frmMain.system != null)
            {
                clsArtikel artikel = new clsArtikel();
                artikel._GL_User = this.GL_User;
                artikel.sys = this._ctrMenu._frmMain.system;
                artikel.MandantenID = this._MandantenID;
                _LVSNr = artikel.GetNewLVSNr();
                this.Lager.Artikel = artikel.Copy();
                return true;
            }
            else
            {
                clsMessages.Allgemein_MandantFehlt();
                return false;
            }
        }
        /****************************************************************************************
         *                  Datagridview grdVita - ArtikelVita
         * *************************************************************************************/
        ///<summary>ctrEinlagerung / GetNewLVSNr</summary>
        ///<remarks>ArtikleVita wird gefüllt.</remarks>    
        public void InitGrdArtVita()
        {
            if (Lager.LEingangTableID > 0)
            {
                DataTable dt = clsArtikelVita.GetArtikelVitaByLEingangTableID(this.GL_User, Lager.Eingang.LEingangTableID, this.Lager.Artikel.ID, Lager.LAusgangTableID);
                this.dgvVita.DataSource = dt;

                if (this.dgvVita.Rows.Count > 0)
                {
                    this.dgvVita.Columns["ID"].Visible = false;
                    this.dgvVita.Columns["TableID"].Visible = false;
                    this.dgvVita.Columns["TableName"].Visible = false;
                    this.dgvVita.Columns["Aktion"].Visible = false;
                    this.dgvVita.Columns["UserID"].Visible = false;

                    Int32 i = 0;
                    i++;
                    this.dgvVita.Columns["Datum"].DisplayIndex = i;
                    i++;
                    this.dgvVita.Columns["Beschreibung"].DisplayIndex = i;
                    i++;
                    this.dgvVita.Columns["User"].DisplayIndex = i;
                }
            }
            else
            {
                this.dgvVita.DataSource = null;
            }
        }
        ///<summary>ctrEinlagerung / dgvVita_CellFormatting</summary>
        ///<remarks>Formatierung der Griddarstellung.</remarks>  
        private void dgvVita_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string tmpAction = string.Empty;

            if (this.dgvVita.Columns["Aktion"] != null)
            {
                if ((!object.ReferenceEquals(dgvVita.Rows[e.RowIndex].Cells["Aktion"].Value, DBNull.Value)))
                {
                    if (dgvVita.Rows[e.RowIndex].Cells["Aktion"].Value != null)
                    {
                        tmpAction = dgvVita.Rows[e.RowIndex].Cells["Aktion"].Value.ToString().Trim();
                    }
                }
            }

            if (e.ColumnIndex == 0)
            {
                if (tmpAction != String.Empty)
                {
                    Functions.InitActionImageToArtikelVitaGrid(ref e, tmpAction);
                }
            }
        }
        ///<summary>ctrEinlagerung / tbLEingangID_TextChanged</summary>
        ///<remarks>Sobald ein Eingang im Kopf angezeigt wird, so müssen die beiden Button Speichern / Delete 
        ///         ein- bzw. ausgeblendet werden.</remarks> 
        private void tbLEingangID_TextChanged(object sender, EventArgs e)
        {
            SetTsbtnEinlagerungSpeichernEnabled();
        }
        ///<summary>ctrEinlagerung / SetTsbtnEinlagerungSpeichernEnabled</summary>
        ///<remarks></remarks> 
        private void SetTsbtnEinlagerungSpeichernEnabled()
        {
            bool bEnabled = false;
            if (!this.Lager.Eingang.Checked && (this.Lager.Eingang.LockedBy == 0 || this.Lager.Eingang.LockedBy == this.GL_User.User_ID))
            {
                bEnabled = true;
            }
            tsbtnEinlagerungSpeichern.Enabled = bEnabled;
            tsbtnDeleteLEingang.Enabled = bEnabled;
        }
        ///<summary>ctrEinlagerung / SetLabelKennzeichen</summary>
        ///<remarks></remarks> 
        private void SetLabelKennzeichen(Int32 icbSelVal)
        {
            string strLabelText = string.Empty;
            switch (icbSelVal)
            {
                //SChiff
                case -3:
                    strLabelText = "Schiff:";
                    break;

                //Bahn/Waggon
                case -2:
                    strLabelText = "Waggon-Nr.:";
                    break;
                //Fremdfahrzeuge
                case -1:
                    strLabelText = "KFZ - Kennzeichen:";
                    break;
                //eigene KFZ
                default:
                    strLabelText = "KFZ - Kennzeichen:";
                    break;
            }
            this.lKennzeichen.Text = strLabelText;
        }
        ///<summary>ctrEinlagerung / cbFahrzeug_SelectedIndexChanged</summary>
        ///<remarks></remarks> 
        private void cbFahrzeug_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFahrzeug.SelectedValue != null)
            {
                Int32 iValue = -1;
                Int32.TryParse(cbFahrzeug.SelectedValue.ToString(), out iValue);
                switch (iValue)
                {
                    //SChiff
                    case -3:
                        mtbKFZ.Text = string.Empty;
                        mtbKFZ.Focus();
                        mtbKFZ.Mask = string.Empty;
                        //Lager.Eingang.SpedID = 0;
                        mtbKFZ.Focus();
                        break;

                    //Bahn/Waggon
                    case -2:
                        mtbKFZ.Text = string.Empty;
                        mtbKFZ.Focus();
                        mtbKFZ.Mask = this._ctrMenu._frmMain.system.Client.Modul.Lager_WaggonNo_Mask;
                        //Lager.Eingang.SpedID = 0;
                        SetFelderFremdfahrzeugeEnabled(false, true);
                        break;
                    //Fremdfahrzeuge
                    case -1:
                        //Felder aktivieren
                        SetFelderFremdfahrzeugeEnabled(true);
                        mtbKFZ.Mask = string.Empty;
                        mtbKFZ.Text = cbFahrzeug.Text;
                        mtbKFZ.Focus();
                        break;
                    //eigene KFZ
                    default:
                        //Auswahl eigene Fahrzeuge
                        SetFelderFremdfahrzeugeEnabled(false);
                        if (Lager.Eingang != null)
                        {
                            Lager.Eingang.SpedID = 0;
                        }
                        mtbKFZ.Mask = string.Empty;
                        mtbKFZ.Text = cbFahrzeug.Text;
                        break;
                }
                SetLabelKennzeichen(iValue);
            }
        }
        ///<summary>ctrEinlagerung / SetFelderFremdfahrzeugeEnabled</summary>
        ///<remarks>.</remarks>
        private void SetFelderFremdfahrzeugeEnabled(bool bEndabled, bool bwaggon = false)
        {
            tbMCSpedition.Enabled = bEndabled;
            //tbADRSpedition.Enabled = bEndabled;
            btnSpedition.Enabled = bEndabled;
            btnManSped.Enabled = bEndabled;
            if (bwaggon == true) { mtbKFZ.Enabled = !bEndabled; }
            else { mtbKFZ.Enabled = bEndabled; }

            if (!bEndabled)
            {
                tbMCSpedition.Text = string.Empty;
                tbADRSpedition.Text = string.Empty;
            }
        }
        ///<summary>ctrEinlagerung / SetFelderFremdfahrzeugeEnabled</summary>
        ///<remarks>Internen Lagerplatz zuweisen</remarks>
        private void tsbtnLagerort_Click(object sender, EventArgs e)
        {
            //this.bIsExternerLagerOrt = false;
            this._ctrMenu.OpenCtrLagerOrtInFrm(true, this);
        }
        ///<summary>ctrEinlagerung / SetFelderFremdfahrzeugeEnabled</summary>
        ///<remarks>Externen Lagerplatz zuweisen.</remarks>
        private void tsbtnExLagerOrt_Click(object sender, EventArgs e)
        {
            //this.bIsExternerLagerOrt = true;
            this._ctrMenu.OpenCtrLagerOrtInFrm(true, this);
        }
        ///<summary>ctrEinlagerung / label14_Click</summary>
        ///<remarks>Zur Vereinfachung</remarks>
        private void label14_Click(object sender, EventArgs e)
        {
            this._ctrMenu.OpenCtrLagerOrtInFrm(true, this);
        }
        ///<summary>ctrEinlagerung / TakeOverLagerOrt</summary>
        ///<remarks>Übernimmt den Lagerort, speichert den Lagerot im Artikeldatensatz und zeigt den Lagerort im 
        ///         Ctr an.</remarks>
        public void TakeOverLagerOrt(decimal myDecLagerOrtID, string strLagerPlatzOrt)
        {
            //bIsExLagerOrt = false;
            Lager.LagerOrt.ArtikelID = Lager.Artikel.ID;
            Lager.LagerOrt.LagerPlatzID = myDecLagerOrtID;
            Lager.LagerOrt.LOTable = strLagerPlatzOrt;

            //eigener LagerOrt
            if (!Lager.LagerOrt.UpdateArtikelLagerOrt())
            {
                //Update war nicht erfolgreich, Lagerplatz besetzt(Check in Updatefunktion)
                //Der alte Lagerort muss wieder zugewiesen werden, damit die Daten in der 
                //LagerPlatzBezeichnungListe richtig angezeigt werden
                Lager.LagerOrt.LagerPlatzID = Lager.Artikel.LagerOrt;
                tbWerk.Text = string.Empty;
                tbHalle.Text = string.Empty;
                tbReihe.Text = string.Empty;
                tbEbene.Text = string.Empty;
                tbPlatz.Text = string.Empty;
            }
            else
            {
                //Update erfolgreich der neue Lagerort muss nun noch dem Arikel
                //zugewiesen werden, da die Daten nicht noch einmal aus der DB geholt werden
                //Lager.Artikel.LagerOrt = Lager.LagerOrt.LagerPlatzID;
                Lager.Artikel.GetArtikeldatenByTableID();
                tbWerk.Text = Lager.LagerOrt.WerkBezeichnung;
                tbHalle.Text = Lager.LagerOrt.HalleBezeichnung;
                tbReihe.Text = Lager.LagerOrt.ReiheBezeichnung;
                tbEbene.Text = Lager.LagerOrt.EbeneBezeichnung;
                tbPlatz.Text = Lager.LagerOrt.PlatzBezeichnung;
            }
        }
        ///<summary>ctrEinlagerung / tsbtnChangeEingang_Click</summary>
        ///<remarks>abgeschlossener Eingang wird zurückgesetz, damit er wieder editiert werden kann</remarks>
        private void tsbtnChangeEingang_Click(object sender, EventArgs e)
        {
            if (Lager.Eingang.Checked)
            {
                bUpdate = true;
                //Eingang Checked zurücksetzen
                Lager.Eingang.Checked = false;
                Lager.Eingang.UpdateLagerEingang();
                _bLEingangIsChecked = false;
                clsLager.UpdateLEingangCheck(GL_User.User_ID, _bLEingangIsChecked, Lager.LEingangTableID);

                //neuladen
                //SetLEingangskopfdatenToFrm(true);
                //tsbtnEinlagerungSpeichern.Enabled = true;
                //InitArtikelLoad();
                //SetArtikelMenuBtnEnabled(); //CF
                //InitSelectedTabPage(tabArtikel.SelectedTab.Name.ToString());

                //--Damit der EIngang neu geladen wird,
                this.Lager.Eingang.Stat = ClsStatus.initialized;
                EingangBrowse(this.Lager.LEingangTableID, 0, enumBrowseAcivity.NoSelect);
            }
        }
        ///<summary>ctrEinlagerung / tsbtnUB_Click</summary>
        ///<remarks>Artikel umbuchen - dazu muss der Aritkel abgeschlossen sein.</remarks>
        private void tsbtnUB_Click(object sender, EventArgs e)
        {
            if (GL_User.write_LagerEingang)
            {
                if (Lager.Eingang.Checked)
                {
                    //_ArtikelIDTakeOverUB = _ArtikelTableID;
                    _ArtikelIDTakeOverUB = this.Lager.Artikel.ID;
                    //this._ctrMenu.OpenCtrUmbuchung(this);
                    this._ctrMenu.OpenCtrUmbuchungInFrm(null, this);
                }
                else
                {
                    clsMessages.Lager_EingangOffenKeineUmbuchung();
                }
            }
            else
            {
                clsMessages.User_NoAuthen();
            }
        }
        ///<summary>ctrEinlagerung / tsbtnUB_Click</summary>
        ///<remarks>Artikel umbuchen - dazu muss der Aritkel abgeschlossen sein.</remarks>
        private void tsbtnIntegrate_Click(object sender, EventArgs e)
        {
            object obj = this;
            if (this._frmTmp != null)
            {
                this._frmTmp.CloseFrmTmp();
            }
            else
            {
                this._ctrMenu.CloseCtrEinlagerungFrmTmp();
            }
            object obj1 = new object();
            this._ctrMenu.OpenCtrEinlagerung(obj1);
        }
        ///<summary>ctrEinlagerung / tsbtnUB_Click</summary>
        ///<remarks>Artikel umbuchen - dazu muss der Aritkel abgeschlossen sein.</remarks>
        private void tsbtnSeparate_Click(object sender, EventArgs e)
        {
            object obj = this;
            this._ctrMenu.OpenCtrEinlagerungInFrm();
            this._ctrMenu.CloseCtrEinlagerung();
        }
        ///<summary>ctrEinlagerung / tbDicke_Validating</summary>
        ///<remarks></remarks>
        private void tbDicke_Validating(object sender, CancelEventArgs e)
        {
            decimal decTmp = 0;
            if (!Decimal.TryParse(tbDicke.Text, out decTmp))
            {
                clsMessages.Allgemein_EingabeFormatFehlerhaft();
            }
            else
            {
                string strTxt = string.Empty;
                if (decTmp < 0)
                {
                    decTmp = decTmp * (-1);
                }

                //if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinition)
                //{
                //    if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_GArt_InfoMessageAllData)
                //    {
                //        if ((decTmp != this.Lager.Artikel.GArt.Dicke) && (this.Lager.Artikel.GArt.Dicke > 0))
                //        {
                //            strTxt = strTxt + "- Die eingegebene Dicke stimmt nicht mit der angegebenen Wert der Güterartdaten überein." + Environment.NewLine;
                //        }
                //    }
                //}

                if (this._ctrMenu._frmMain.system.AbBereich.ASNTransfer)
                {
                    if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinitionByASNTransfer)
                    {
                        if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_GArt_InfoMessageAllData)
                        {
                            if ((decTmp != this.Lager.Artikel.GArt.Dicke) && (this.Lager.Artikel.GArt.Dicke > 0))
                            {
                                strTxt = strTxt + "- Die eingegebene Dicke stimmt nicht mit der angegebenen Wert der Güterartdaten überein." + Environment.NewLine;
                            }
                        }
                    }
                }
                else
                {
                    if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinition)
                    {
                        if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_GArt_InfoMessageAllData)
                        {
                            if ((decTmp != this.Lager.Artikel.GArt.Dicke) && (this.Lager.Artikel.GArt.Dicke > 0))
                            {
                                strTxt = strTxt + "- Die eingegebene Dicke stimmt nicht mit der angegebenen Wert der Güterartdaten überein." + Environment.NewLine;
                            }
                        }
                    }
                }


                if (strTxt != string.Empty)
                {
                    strTxt = "Folgende Fehler sind aufgetreten. Bitte prüfen Sie die Eingabe." + Environment.NewLine + strTxt;
                    clsMessages.Allgemein_ERRORTextShow(strTxt);
                }
            }
            tbDicke.Text = Functions.FormatDecimal(decTmp);
        }
        ///<summary>ctrEinlagerung / tbDicke_Validated</summary>
        ///<remarks>Die Eingabe wird direkt auf das Format geprüft.</remarks>    
        private void tbBreite_Validating(object sender, CancelEventArgs e)
        {
            decimal decTmp = 0;
            if (!Decimal.TryParse(tbBreite.Text, out decTmp))
            {
                clsMessages.Allgemein_EingabeFormatFehlerhaft();
            }
            if (decTmp < 0)
            {
                decTmp = decTmp * (-1);
            }
            tbBreite.Text = Functions.FormatDecimal(decTmp);
        }
        ///<summary>ctrEinlagerung / tbAnzahl_Validated</summary>
        ///<remarks>Die Eingabe wird direkt auf das Format geprüft.</remarks>  
        private void tbAnzahl_Validating(object sender, CancelEventArgs e)
        {
            Int32 iTmp = 0;
            if (!Int32.TryParse(tbAnzahl.Text, out iTmp))
            {
                clsMessages.Allgemein_EingabeFormatFehlerhaft();
            }
            else
            {
                string strTxt = string.Empty;
                if (iTmp < 0)
                {
                    iTmp = iTmp * (-1);
                }
                if (strTxt != string.Empty)
                {
                    strTxt = "Folgende Fehler sind aufgetreten. Bitte prüfen Sie die Eingabe." + Environment.NewLine + strTxt;
                    clsMessages.Allgemein_ERRORTextShow(strTxt);
                }
            }
            tbAnzahl.Text = iTmp.ToString();
        }
        ///<summary>ctrEinlagerung / tbPackmittelGewicht_Validated</summary>
        ///<remarks>Die Eingabe wird direkt auf das Format geprüft.</remarks>  
        private void tbPackmittelGewicht_Validating(object sender, CancelEventArgs e)
        {
            decimal decTmp = 0;
            if (!Decimal.TryParse(tbPackmittelGewicht.Text, out decTmp))
            {
                clsMessages.Allgemein_EingabeFormatFehlerhaft();
            }
            if (decTmp < 0)
            {
                decTmp = decTmp * (-1);
            }
            tbPackmittelGewicht.Text = Functions.FormatDecimal(decTmp);
        }
        ///<summary>ctrEinlagerung / tbBrutto_Validating</summary>
        ///<remarks>Die Eingabe wird direkt auf das Format geprüft.</remarks>  
        private void tbBrutto_Validating(object sender, CancelEventArgs e)
        {
            decimal decTmp = 0;
            if (!Decimal.TryParse(tbBrutto.Text, out decTmp))
            {
                clsMessages.Allgemein_EingabeFormatFehlerhaft();
            }
            else
            {
                string strTxt = string.Empty;
                if (decTmp < 0)
                {
                    decTmp = decTmp * (-1);
                }
                //if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinition)
                //{
                //    if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_GArt_InfoMessageAllData)
                //    {
                //        if ((decTmp != this.Lager.Artikel.GArt.Brutto) && (this.Lager.Artikel.GArt.Brutto > 0))
                //        {
                //            strTxt = strTxt + "- Das eingegebene Bruttogewicht stimmt nicht mit dem angegebenen Wert der Güterartdaten überein." + Environment.NewLine;
                //        }
                //    }
                //}

                if (this._ctrMenu._frmMain.system.AbBereich.ASNTransfer)
                {
                    if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinitionByASNTransfer)
                    {
                        if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_GArt_InfoMessageAllData)
                        {
                            if ((decTmp != this.Lager.Artikel.GArt.Brutto) && (this.Lager.Artikel.GArt.Brutto > 0))
                            {
                                strTxt = strTxt + "- Das eingegebene Bruttogewicht stimmt nicht mit dem angegebenen Wert der Güterartdaten überein." + Environment.NewLine;
                            }
                        }
                    }
                }
                else
                {
                    if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinition)
                    {
                        if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_GArt_InfoMessageAllData)
                        {
                            if ((decTmp != this.Lager.Artikel.GArt.Brutto) && (this.Lager.Artikel.GArt.Brutto > 0))
                            {
                                strTxt = strTxt + "- Das eingegebene Bruttogewicht stimmt nicht mit dem angegebenen Wert der Güterartdaten überein." + Environment.NewLine;
                            }
                        }
                    }
                }




                if (decTmp > const_ArtikelValue_WeightToAller)
                {
                    strTxt = strTxt + "- Das Bruttogewicht ist zu hoch." + Environment.NewLine;
                }
                if (strTxt != string.Empty)
                {
                    strTxt = "Folgende Fehler sind aufgetreten. Bitte prüfen Sie die Eingabe." + Environment.NewLine + Environment.NewLine + strTxt;
                    clsMessages.Allgemein_ERRORTextShow(strTxt);
                }
            }
            tbBrutto.Text = Functions.FormatDecimal(decTmp);

            //Bruttowert >0 dann Berechnung des Packmittels
            if (decTmp > 0)
            {
                decimal decNet = Convert.ToDecimal(tbNetto.Text);
                decimal decBru = Convert.ToDecimal(tbBrutto.Text);
                decimal decPack = 0;
                if (decNet > decBru)
                {
                    clsMessages.Allgemein_BruttoKleinerNetto();
                }
                else
                {
                    decPack = decBru - decNet;
                }
                tbPackmittelGewicht.Text = Functions.FormatDecimal(decPack);
            }
        }
        ///<summary>ctrEinlagerung / tbNetto_Validated</summary>
        ///<remarks>Die Eingabe wird direkt auf das Format geprüft.</remarks> 
        private void tbNetto_Validating(object sender, CancelEventArgs e)
        {
            decimal decTmp = 0;
            if (!Decimal.TryParse(tbNetto.Text, out decTmp))
            {
                clsMessages.Allgemein_EingabeFormatFehlerhaft();
            }
            if (decTmp < 0)
            {
                decTmp = decTmp * (-1);
            }
            tbNetto.Text = Functions.FormatDecimal(decTmp);

            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_BruttoEqualsNetto)
            {
                tbBrutto.Text = tbNetto.Text;
            }
            else
            {
                decTmp = 0;
                Decimal.TryParse(tbBrutto.Text, out decTmp);
                if (decTmp < 1)
                {
                    tbBrutto.Text = tbNetto.Text;
                }
            }
        }
        ///<summary>ctrEinlagerung / tbHoehe_Validated</summary>
        ///<remarks>Die Eingabe wird direkt auf das Format geprüft.</remarks>  
        private void tbHoehe_Validated_1(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            if (!Decimal.TryParse(tbHoehe.Text, out decTmp))
            {
                clsMessages.Allgemein_EingabeFormatFehlerhaft();
            }
            else
            {
                string strTxt = string.Empty;
                if (decTmp < 0)
                {
                    decTmp = decTmp * (-1);
                }

                //if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinition)
                //{
                //    if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_GArt_InfoMessageAllData)
                //    {
                //        if ((decTmp != this.Lager.Artikel.GArt.Hoehe) && (this.Lager.Artikel.GArt.Hoehe > 0))
                //        {
                //            strTxt = strTxt + "- Die eingegebene Höhe stimmt nicht mit der angegebenen Wert der Güterartdaten überein." + Environment.NewLine;
                //        }
                //    }
                //}

                if (this._ctrMenu._frmMain.system.AbBereich.ASNTransfer)
                {
                    if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinitionByASNTransfer)
                    {
                        if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_GArt_InfoMessageAllData)
                        {
                            if ((decTmp != this.Lager.Artikel.GArt.Hoehe) && (this.Lager.Artikel.GArt.Hoehe > 0))
                            {
                                strTxt = strTxt + "- Die eingegebene Höhe stimmt nicht mit der angegebenen Wert der Güterartdaten überein." + Environment.NewLine;
                            }
                        }
                    }
                }
                else
                {
                    if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinition)
                    {
                        if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_GArt_InfoMessageAllData)
                        {
                            if ((decTmp != this.Lager.Artikel.GArt.Hoehe) && (this.Lager.Artikel.GArt.Hoehe > 0))
                            {
                                strTxt = strTxt + "- Die eingegebene Höhe stimmt nicht mit der angegebenen Wert der Güterartdaten überein." + Environment.NewLine;
                            }
                        }
                    }
                }


                if (strTxt != string.Empty)
                {
                    strTxt = "Folgende Fehler sind aufgetreten. Bitte prüfen Sie die Eingabe." + Environment.NewLine + strTxt;
                    clsMessages.Allgemein_ERRORTextShow(strTxt);
                }
            }
            tbHoehe.Text = Functions.FormatDecimal(decTmp);
        }
        ///<summary>ctrEinlagerung / tbLaenge_Validated</summary>
        ///<remarks>Die Eingabe wird direkt auf das Format geprüft.</remarks>  
        private void tbLaenge_Validating(object sender, CancelEventArgs e)
        {
            decimal decTmp = 0;
            if (!Decimal.TryParse(tbLaenge.Text, out decTmp))
            {
                clsMessages.Allgemein_EingabeFormatFehlerhaft();
            }
            else
            {
                string strTxt = string.Empty;
                if (decTmp < 0)
                {
                    decTmp = decTmp * (-1);
                }
                //if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinition)
                //{
                //    if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_GArt_InfoMessageAllData)
                //    {
                //        //Länge > 
                //        if ((decTmp != this.Lager.Artikel.GArt.Laenge) && (this.Lager.Artikel.GArt.Laenge > 0))
                //        {
                //            strTxt = strTxt + "- Die eingegebene Länge stimmt nicht mit der angegebenen Wert der Güterartdaten überein." + Environment.NewLine;
                //        }
                //    }
                //}
                if (this._ctrMenu._frmMain.system.AbBereich.ASNTransfer)
                {
                    if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinitionByASNTransfer)
                    {
                        if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_GArt_InfoMessageAllData)
                        {
                            //Länge > 
                            if ((decTmp != this.Lager.Artikel.GArt.Laenge) && (this.Lager.Artikel.GArt.Laenge > 0))
                            {
                                strTxt = strTxt + "- Die eingegebene Länge stimmt nicht mit der angegebenen Wert der Güterartdaten überein." + Environment.NewLine;
                            }
                        }
                    }
                }
                else
                {
                    if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinition)
                    {
                        if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_GArt_InfoMessageAllData)
                        {
                            //Länge > 
                            if ((decTmp != this.Lager.Artikel.GArt.Laenge) && (this.Lager.Artikel.GArt.Laenge > 0))
                            {
                                strTxt = strTxt + "- Die eingegebene Länge stimmt nicht mit der angegebenen Wert der Güterartdaten überein." + Environment.NewLine;
                            }
                        }
                    }
                }



                if (strTxt != string.Empty)
                {
                    strTxt = "Folgende Fehler sind aufgetreten. Bitte prüfen Sie die Eingabe" + Environment.NewLine + strTxt;
                    clsMessages.Allgemein_ERRORTextShow(strTxt);
                }
            }
            tbLaenge.Text = Functions.FormatDecimal(decTmp);
        }
        ///<summary>ctrEinlagerung / tbBreite_Validated</summary>
        ///<remarks>Die Eingabe wird direkt auf das Format geprüft.</remarks>  
        private void tbBreite_TextChanged(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            if (!Decimal.TryParse(tbBreite.Text, out decTmp))
            {
                clsMessages.Allgemein_EingabeFormatFehlerhaft();
            }
            else
            {
                string strTxt = string.Empty;
                if (decTmp < 0)
                {
                    decTmp = decTmp * (-1);
                }
                //if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinition)
                //{
                //    if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_GArt_InfoMessageAllData)
                //    {
                //        if ((decTmp != this.Lager.Artikel.GArt.Breite) && (this.Lager.Artikel.GArt.Breite > 0))
                //        {
                //            strTxt = strTxt + "- Die eingegebene Breite stimmt nicht mit der angegebenen Wert der Güterartdaten überein." + Environment.NewLine;
                //        }
                //    }
                //}

                if (this._ctrMenu._frmMain.system.AbBereich.ASNTransfer)
                {
                    if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinitionByASNTransfer)
                    {
                        if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_GArt_InfoMessageAllData)
                        {
                            if ((decTmp != this.Lager.Artikel.GArt.Breite) && (this.Lager.Artikel.GArt.Breite > 0))
                            {
                                strTxt = strTxt + "- Die eingegebene Breite stimmt nicht mit der angegebenen Wert der Güterartdaten überein." + Environment.NewLine;
                            }
                        }
                    }
                }
                else
                {
                    if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinition)
                    {
                        if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_GArt_InfoMessageAllData)
                        {
                            if ((decTmp != this.Lager.Artikel.GArt.Breite) && (this.Lager.Artikel.GArt.Breite > 0))
                            {
                                strTxt = strTxt + "- Die eingegebene Breite stimmt nicht mit der angegebenen Wert der Güterartdaten überein." + Environment.NewLine;
                            }
                        }
                    }
                }

                if (strTxt != string.Empty)
                {
                    strTxt = "Folgende Fehler sind aufgetreten. Bitte prüfen Sie die Eingabe." + Environment.NewLine + strTxt;
                    clsMessages.Allgemein_ERRORTextShow(strTxt);
                }
            }
            tbBreite.Text = Functions.FormatDecimal(decTmp);
        }
        ///<summary>ctrEinlagerung / tbProduktionsNr_Validated_1</summary>
        ///<remarks></remarks>  
        private void tbProduktionsNr_Validated_1(object sender, EventArgs e)
        {
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_Artikel_RequiredValue_Produktionsnummer)
            {
                if (this.tbProduktionsNr.Text == string.Empty)
                {
                    clsMessages.Allgemein_PflichtfeldNichtLeer("Produktionsnummer");
                }
                if (this.Lager.Artikel.GArt.UseProdNrCheck)
                {
                    //prüfe, ob die Produktionsnummer schon einmal vergeben ist
                    if (clsArtikel.ExistProdNr(this.GL_User.User_ID, tbProduktionsNr.Text.Trim(), this.GL_System.sys_ArbeitsbereichID))
                    {
                        clsMessages.Artikel_ArtikelProduktionsnummerExist();
                    }
                }
            }
            clsClient.ctrEinlagerung_tbProdNr_ProdNrToCharge(this._ctrMenu._frmMain.system.Client.MatchCode, ref this.tbProduktionsNr, ref this.tbCharge);
        }
        ///<summary>ctrEinlagerung / tsbtnSearch_Click</summary>
        ///<remarks></remarks>  
        private void tsbtnSearch_Click(object sender, EventArgs e)
        {
            this._ctrMenu.OpenCtrSearch(this);
        }
        ///<summary>ctrEinlagerung / tsbtnSearch_Click</summary>
        ///<remarks></remarks>  
        private void tsbtnSchaden_Click(object sender, EventArgs e)
        {
            if (this.Lager.Artikel.ID > 0)
            {
                this._ctrMenu.OpenCtrSchaedenInFrm(this);
            }
        }
        ///<summary>ctrEinlagerung / InitArtikelSchaden</summary>
        ///<remarks></remarks>  
        private void SetSchadenToFrm(bool bSchaden)
        {
            if (bSchaden)
            {
                pbSchaden.Image = Sped4.Properties.Resources.error_32x32;
            }
            else
            {
                pbSchaden.Image = null;
            }
            pbSchaden.Refresh();
        }
        ///<summary>ctrEinlagerung / SetRLToFrm</summary>
        ///<remarks></remarks> 
        private void SetRLToFrm(bool myRL)
        {
            if (myRL)
            {
                pbRL.Image = Sped4.Properties.Resources.preferences1;
            }
            else
            {
                pbRL.Image = null;
            }
            pbRL.Refresh();
        }
        ///<summary>ctrEinlagerung / SetSPLtoFrm</summary>
        ///<remarks></remarks> 
        private void SetSPLtoFrm(bool mySPL)
        {
            if (mySPL)
            {
                pbSPL.Image = Sped4.Properties.Resources.error_32x32;
            }
            else
            {
                pbSPL.Image = null;
            }
            pbSPL.Refresh();
        }
        ///<summary>ctrEinlagerung / InitDGVSchaden</summary>
        ///<remarks></remarks> 
        public void InitDGVSchaden()
        {
            dtSchaden = new DataTable();
            Lager.Schaeden.ArtikelID = Lager.Artikel.ID;
            dtSchaden.Clear();
            dtSchaden = Lager.Schaeden.GetArtikelSchäden();
            this.dgvSchaden.DataSource = dtSchaden.DefaultView;
            if (this.dgvSchaden.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= this.dgvSchaden.Columns.Count - 1; i++)
                {
                    string strColName = this.dgvSchaden.Columns[i].Name;
                    switch (strColName)
                    {
                        case "ID":
                            this.dgvSchaden.Columns[i].IsVisible = false;
                            break;
                        case "Datum":
                            this.dgvSchaden.Columns[i].FormatString = "{0:d}";
                            break;
                        case "Bezeichnung":
                            this.dgvSchaden.Columns[i].BestFit();
                            this.dgvSchaden.Columns[i].WrapText = true;
                            break;
                    }
                }
            }
            SetSchadenToFrm((this.dgvSchaden.Rows.Count > 0));
        }
        ///<summary>ctrEinlagerung / SetMenuArtikelSchadenEnabled</summary>
        ///<remarks>Wenn der Artikel ausgelager ist, soll der Delete Button deaktiviert sein</remarks> 
        private void SetMenuArtikelSchadenEnabled()
        {
            bool bEnabled = true;
            if (this.Lager.Artikel.LAusgangTableID > 0)
            {
                if (this.Lager.Artikel.Ausgang.Checked)
                {
                    bEnabled = false;
                }
            }
            this.tsbtnDeleteSchaden.Enabled = bEnabled;
        }
        ///<summary>ctrEinlagerung / tsbtnSPL_Click</summary>
        ///<remarks>Artikel wird ins Sperrlager gebucht</remarks> 
        private void tsbtnSPL_Click(object sender, EventArgs e)
        {
            string strError = string.Empty;
            if (Lager.Artikel.LAusgangTableID == 0)
            {
                bool bBreak = false;
                if (this._ctrMenu._frmMain.system.Client.Modul.Lager_SPL_SchadenRequire)
                {
                    //prüfen, ob der Artikel einen Schaden besitz
                    bBreak = (this.Lager.Artikel.bSchaden);
                    if (!bBreak)
                    {
                        strError = "Diesem Artikel ist kein Schaden zugewiesen und somit kann der Artikel nicht ins Sperrlager gebucht werden!";
                    }
                }
                if ((bBreak) || (!this._ctrMenu._frmMain.system.Client.Modul.Lager_SPL_SchadenRequire))
                {
                    if (!Lager.Artikel.bSPL)
                    {
                        if (clsMessages.Sperrlager_add())
                        {
                            Lager.SPL._GL_User = this.GL_User;
                            //Lager.SPL.ArtikelID = _ArtikelTableID;
                            Lager.SPL.ArtikelID = this.Lager.Artikel.ID;
                            if (Lager.SPL.Add(true))
                            {
                                SaveArtikel();
                                //SetArtikelToForm(_ArtikelTableID, false);
                                SetArtikelToForm(this.Lager.Artikel.ID, false);
                                SetArtikelMenuBtnEnabled();
                                OpenCtrSPL(sender, e);
                                //Meldungsverkehr einfügen Storno bzw. SPL-Meldung
                                InitASNTransfer(clsASNAction.const_ASNAction_SPLIn);
                            }
                        }
                    }
                }
            }

            if (strError != string.Empty)
            {
                clsMessages.Allgemein_ERRORTextShow(strError);
            }
        }
        ///<summary>ctrEinlagerung / SetSelectedRowInDGV</summary>
        ///<remarks>Die Datenrow mit der im CTR angezeigten Artikelid wird im Grid markiert.</remarks> 
        private void SetSelectedRowInDGV()
        {
            if (this.dgv.Rows.Count > 0)
            {
                bool bRowSelected = false;
                for (Int32 i = 0; i <= this.dgv.Rows.Count - 1; i++)
                {
                    decimal decTmp = 0;
                    string strTmp = string.Empty;
                    strTmp = this.dgv.Rows[i].Cells["ArtikelID"].Value.ToString();
                    Decimal.TryParse(strTmp, out decTmp);
                    if (decTmp == this.Lager.Artikel.ID)
                    {
                        this.dgv.Rows[i].IsSelected = true;
                        this.dgv.Rows[i].IsCurrent = true;
                        bRowSelected = true;
                        i = this.dgv.Rows.Count;
                    }
                    else
                    {
                        //wenn das gesamte Grid durchlaufen wurde und noch keine 
                        //Übereinstimmung gefunden wurde, dann beim letzten Durchlauf
                        //die Selected Row auf Row[0] setzen
                        if ((i == this.dgv.Rows.Count - 1) && (!bRowSelected))
                        {
                            this.dgv.Rows[0].IsSelected = true;
                            this.dgv.Rows[0].IsCurrent = true;
                        }
                    }
                }
            }
        }
        ///<summary>ctrEinlagerung / DoCheckEingangArtikelComplete</summary>
        ///<remarks>Dies Funktion checked und schliesst einen Eingang und alle Artikel ab.</remarks>
        private void DoCheckEingangArtikelComplete()
        {
            //Check Eingang
            Lager.Eingang.FillEingang();
            //Durch die Prüfung wird eine nochmaliger Abschluss und somit 
            //der Eintrag in die ArtikelVita verhindert
            if (!Lager.Eingang.Checked)
            {
                // Prüfe ob bereits ein Datum in der Vita eingetragen ist 
                DateTime tmpDate = clsArtikelVita.getCheckDate(GL_User, Lager.Eingang.LEingangTableID);
                // falls dies nicht der Fall ist 
                if (tmpDate.Equals(new DateTime()))
                {
                    // aktualisiere den Eingang auf das aktuelle Datum
                    // Lager.Eingang.LEingangDate = DateTime.Now; // CF DEaktiviert wegen Datumsgenauer berechnung falls artikel am wochenende eingehen
                    Lager.Eingang.UpdateLagerEingang();
                }
                clsLager.UpdateLEingangCheck(GL_User.User_ID, true, Lager.LEingangTableID);
                //Eingangmeldungen erstellen
                AsnTransfer = new clsASNTransfer();
                if (AsnTransfer.DoASNTransfer(this.GL_System, this.Lager.Eingang.AbBereichID, this.Lager.Eingang.MandantenID))
                {
                    //AsnTransfer.CreateLM_Eingang(ref this.Lager);
                    if (this._ctrMenu._frmMain.system.Client.Modul.ASN_UserOldASNFileCreation)
                    {
                        //this.Lager.ASNAction.

                        AsnTransfer.CreateLM(ref this.Lager);
                    }
                    else
                    {
                        AsnTransfer.CreateLM_Eingang(ref this.Lager);
                    }
                }
            }
            bool bAllCheckedFromBeginning = true;
            for (Int32 i = 0; i <= dtArtikel.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                string strTmp = dtArtikel.Rows[i]["ArtikelID"].ToString();
                decimal.TryParse(strTmp, out decTmp);
                if (decTmp > 0)
                {
                    Lager.Artikel.ID = decTmp;
                    Lager.Artikel._GL_User = this.GL_User;
                    Lager.Artikel.GetArtikeldatenByTableID();

                    if (!Lager.Artikel.EingangChecked)
                    {
                        clsArtikel.UpdateArtikelCheck(this.GL_User, true, decTmp);
                        bAllCheckedFromBeginning = false;
                    }
                }
            }

            if (!bAllCheckedFromBeginning)
            {
                Int32 iCount = 0;
                if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
                {
                    //this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_Print_DirectEingangDoc in 
                    lastprintID = this.Lager.Eingang.LEingangTableID;
                    DirectPrintLEingangDoc();

                    DirectPrintList();
                }
                else
                {
                    if (DoDirectPrintReport(enumIniDocKey.Eingangsliste))
                    {
                        this.Lager.Eingang.UpdatePrintLEingang(enumIniDocKey.Eingangsliste.ToString());
                    }
                }
                CheckDirectPrintLabel();
            }
            //LEingangdaten neu auf das CTR setzen
            SetLagerDatenToFrm();
            //alten Artikel neu auf das CTR setzen
            SetArtikelToForm(this.Lager.Artikel.ID, false);
            //Summe neu berechnen
            SetLEingangGesamtGewichte();
            //Button Enable
            SetTsbtnEinlagerungSpeichernEnabled();
        }
        ///<summary>ctrEinlagerung / tsbtn_Click</summary>
        ///<remarks>Eingang für die Directanlieferung wird angelegt.</remarks> 
        private void tsbtnDirect_Click(object sender, EventArgs e)
        {
            if (_MandantenID > 0)
            {
                _bDirectDelivery = true;
                SetLabelDirektanlieferung(true);

                Lager.LEingangTableID = 0;
                Lager.FillLagerDaten(true);
                _bLEingangIsChecked = false;
                _bArtikelIsChecked = false;
                _LVSNr = 0;
                bUpdate = false;

                //Eingabefelder leeren und neue LEingangsID wird ermittelt
                InitEingabe();
                //Artikelbereich Reset
                SetArtikelMenuBtnEnabled();

                //ArtikelEingabefelder und Table leeren
                ClearArtikelEingabeFelder(true);
                dtArtikel.Clear();
                //ArtikelVita leeren
                dgvVita.DataSource = null;
                dgvSchaden.DataSource = null;

                //Leingang udn ArtikelCheck zurücksetzen
                pbCheckArtikel.Image = null;
                pbCheckEingang.Image = null;

                //Button Abschluss Direktanlieferung deaktivieren bis Eingangskopf gespeichert ist
                tsbtnDirectAbschluss.Enabled = false;
            }
            else
            {
                clsMessages.Allgemein_MandantFehlt();
            }
        }
        ///<summary>ctrEinlagerung / tsbtnDirectAbschluss_Click</summary>
        ///<remarks></remarks> 
        private void tsbtnDirectAbschluss_Click(object sender, EventArgs e)
        {
            if (
                (dtArtikel.Rows.Count > 0) && (Lager.Eingang.DirektDelivery)
               )
            {
                //Wenn der angezeigte Lagereingang bereits abgeschlossen ist,
                //so ist der komplette Vorgang der Direktanlieferung bereits abschlossen
                if (!Lager.Eingang.Checked)
                {
                    //Eingang abschließen
                    DoCheckEingangArtikelComplete();
                    Lager.Eingang.FillEingang();

                    DirectDeliveryConfirmation();

                }
                _bDirectDelivery = false;

            }
            else
            {
                if (dtArtikel.Rows.Count == 0)
                {
                    clsMessages.DirectDelivery_KeineArtikeldatenVorhanden();
                }
            }
        }
        /// <summary>
        ///             Abschluss der Direktanlieferung mit Erstellung des Ausgangs
        /// </summary>
        private void DirectDeliveryConfirmation()
        {
            //Ausgang anlegen
            decimal decTmp = 0;
            Lager.Ausgang._GL_User = this.GL_User;
            //Lager.Ausgang.AbBereichID = //this.GL_User.sys_ArbeitsbereichID;
            Lager.Ausgang.AbBereichID = this.GL_System.sys_ArbeitsbereichID;
            Lager.Ausgang.MandantenID = Lager.Eingang.MandantenID;

            //Ausgangsdaten zuweisen
            Lager.Ausgang.LAusgangID = clsLager.GetNewLAusgangID(this.GL_User, this._ctrMenu._frmMain.system);
            Lager.Ausgang.LAusgangsDate = DateTime.Now;

            decTmp = 0;
            Decimal.TryParse(tbNetto.Text, out decTmp);
            Lager.Ausgang.GewichtNetto = decTmp;
            decTmp = 0;
            Decimal.TryParse(tbBrutto.Text, out decTmp);
            Lager.Ausgang.GewichtBrutto = decTmp;
            Lager.Ausgang.Auftraggeber = Lager.Eingang.Auftraggeber;
            Lager.Ausgang.Empfaenger = Lager.Eingang.Empfaenger;
            Lager.Ausgang.Entladestelle = Lager.Eingang.Empfaenger;
            Lager.Ausgang.Lieferant = Lager.Eingang.Lieferant;
            Lager.Ausgang.SLB = 0;
            Lager.Ausgang.MAT = string.Empty;
            Lager.Ausgang.Checked = true;
            Lager.Ausgang.SpedID = Lager.Eingang.SpedID;
            Lager.Ausgang.KFZ = mtbKFZ.Text;
            Lager.Ausgang.Info = "Direktanlieferung";
            Lager.Ausgang.Termin = DateTime.Now;
            Lager.Ausgang.DirectDelivery = true;
            Lager.Ausgang.AddLAusgang();

            //Artikel Ausgang abschließen
            for (Int32 i = 0; i <= dtArtikel.Rows.Count - 1; i++)
            {
                decTmp = 0;
                string strTmp = dtArtikel.Rows[i]["ArtikelID"].ToString();
                decimal.TryParse(strTmp, out decTmp);
                //CHeck, ob für den Artikel bereits ein Ausgang vorliegt
                Lager.Artikel = new clsArtikel();
                Lager.Artikel._GL_User = this.GL_User;
                Lager.Artikel.ID = decTmp;
                Lager.Artikel.GetArtikeldatenByTableID();
                if (Lager.Artikel.LAusgangTableID == 0)
                {
                    Lager.Artikel.LAusgangTableID = Lager.Ausgang.LAusgangTableID;
                    Lager.Artikel.AusgangChecked = true;
                    Lager.Artikel.Info = "Direktanlieferung";
                    Lager.Artikel.BKZ = 0;  //ausgebucht
                    Lager.Artikel.UpdateArtikelLager();
                }
            }
            SetLEingangskopfdatenToFrm(true);
            SetMenuItemEingangEnabled();
            SetArtikelMenuBtnEnabled();
        }
        ///<summary>ctrEinlagerung / dgvVita_CellErrorTextNeeded</summary>
        ///<remarks>Zeit den Inhalt der Cellen als Tool Tipp an.</remarks> 
        private void dgvVita_CellErrorTextNeeded(object sender, DataGridViewCellErrorTextNeededEventArgs e)
        {
            string strToolTip = this.dgvVita.Rows[e.RowIndex].Cells["User"].Value.ToString()
                                + " - " +
                                this.dgvVita.Rows[e.RowIndex].Cells["Beschreibung"].Value.ToString();
            this.dgvVita.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = strToolTip;
        }
        ///<summary>ctrEinlagerung / tabArtikel_DrawItem</summary>
        ///<remarks>Hinertlegt die TAB Headers farbig.</remarks> 
        private void tabArtikel_DrawItem(object sender, DrawItemEventArgs e)
        {

            if (tbSysInfo.Text != string.Empty)
            {
                //e.DrawBackground();
                using (Brush br = new SolidBrush(Color.Yellow))
                {
                    e.Graphics.FillRectangle(br, e.Bounds);
                    SizeF sz = e.Graphics.MeasureString(tabArtikel.TabPages["tabInfo"].Text, e.Font);
                    e.Graphics.DrawString(tabArtikel.TabPages["tabInfo"].Text, e.Font, Brushes.Black, e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2, e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + 1);

                    Rectangle rect = e.Bounds;
                    rect.Offset(0, 1);
                    rect.Inflate(0, -1);
                    e.Graphics.DrawRectangle(Pens.DarkGray, rect);
                    e.DrawFocusRectangle();
                }
            }
        }
        ///<summary>ctrEinlagerung / btnPrint_Click</summary>
        ///<remarks>Lagereingangsdokumente drucken.</remarks> 
        private void btnPrint_Click(object sender, EventArgs e)
        {
            InitPrint(false);
        }
        ///<summary>ctrEinlagerung / tsbtnPrintArt_Click</summary>
        ///<remarks></remarks> 
        private void tsbtnPrintArt_Click(object sender, EventArgs e)
        {
            InitPrint(true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myIsArtikelPrint"></param>
        private void InitPrint(bool myIsArtikelPrint)
        {
            _bArtPrint = myIsArtikelPrint;
            this._ctrMenu.OpenCtrPrintLagerInFrm(this);
        }
        ///<summary>ctrEinlagerung / tbGArtSearch_TextChanged</summary>
        ///<remarks>Suche der Güterart über den Matchcode.</remarks> 
        private void tbGArtSearch_TextChanged(object sender, EventArgs e)
        {
            //Güterarten laden
            DataTable dt = new DataTable();
            dt = clsGut.GetGArtenForCombo(this.GL_User.User_ID);
            string Filter = tbGArtSearch.Text.Trim();
            DataTable dtTmp = new DataTable();

            if (Filter != string.Empty)
            {
                dt.DefaultView.RowFilter = "ViewID ='" + Filter + "' AND AbBereichID=" + (int)this._ctrMenu._frmMain.system.AbBereich.ID;
                dtTmp = dt.DefaultView.ToTable();

                if (dtTmp.Rows.Count > 0)
                {
                    tbGArtSearch.Text = dtTmp.Rows[0]["ViewID"].ToString();
                    tbGArt.Text = dtTmp.Rows[0]["Bezeichnung"].ToString();
                    _decGArtID = (decimal)dtTmp.Rows[0]["ID"];
                    Lager.Artikel.GArt.ID = _decGArtID;
                    Lager.Artikel.GArt.InitClass(this.GL_User, this.GL_System);
                }
                else
                {
                    tbGArt.Text = string.Empty;
                    _decGArtID = 0;
                }
            }
            else
            {
                tbGArt.Text = string.Empty;
                _decGArtID = 0;
            }
            SetLabelGArdIDInfo();
        }
        ///<summary>ctrEinlagerung / miDelSchaden_Click</summary>
        ///<remarks>gewählten Schaden löschen.</remarks> 
        private void miDelSchaden_Click(object sender, EventArgs e)
        {
            DeleteSchaden();
        }
        ///<summary>ctrEinlagerung / DeleteSchaden</summary>
        ///<remarks>gewählten Schaden löschen.</remarks> 
        private void DeleteSchaden()
        {
            if (clsMessages.Schaeden_Delete())
            {
                if (this.dgvSchaden.Rows.Count > 0)
                {
                    clsSchaeden SchadenDelete = new clsSchaeden();
                    SchadenDelete._GL_User = this.GL_User;
                    SchadenDelete.SchadenZuweisungID = decimal.Parse(dgvSchaden.SelectedRows[0].Cells["ID"].Value.ToString());
                    SchadenDelete.FillSchadensZuweisungByID();
                    SchadenDelete.ArtikelID = Lager.Artikel.ID;
                    SchadenDelete.DeleteSchadenFromArtikel();

                    SetArtikelToForm(this.Lager.Artikel.ID, false);
                }
            }
        }
        ///<summary>ctrEinlagerung / miDelSchaden_Click</summary>
        ///<remarks>Menü im Schadensgrid wird aufgerufen.</remarks> 
        private void dgvSchaden_DoubleClick(object sender, EventArgs e)
        {


        }
        ///<summary>ctrEinlagerung / dgvSchaden_CellMouseClick</summary>
        ///<remarks>Ermittelt die gewählte SchadenID im Schadensgrid.</remarks> 
        private void dgvSchaden_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this.dgvSchaden.Rows.Count > 0)
            {
                string strSchadenID = this.dgvSchaden.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                decimal decTmp = 0;
                Decimal.TryParse(strSchadenID, out decTmp);
                Lager.Artikel.SelectedSchadenID = decTmp;
            }
        }
        ///<summary>ctrEinlagerung / dgvSchaden_MouseDoubleClick</summary>
        ///<remarks>Menü im Schadensgrid wird aufgerufen.</remarks> 
        private void dgvSchaden_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                cmsSchaeden.Show(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }
        ///<summary>ctrEinlagerung / ctrEinlagerung_KeyDown</summary>
        ///<remarks>Tastenkürzel.</remarks> 
        private void ctrEinlagerung_KeyDown(object sender, KeyEventArgs e)
        {
        }
        ///<summary>ctrEinlagerung / ctrEinlagerung_KeyDown</summary>
        ///<remarks>Tastenkürzel.</remarks> 
        private void KeyDownProzess(KeyEventArgs e)
        {
            //Es müssen Artikel vorhanden sein 
            //es muss ein Artikl ausgewählt sein
            if (dtArtikel.Rows.Count > 0)
            {
                if (Lager.Artikel.ID > 0)
                {
                    //ARtikel copiereln
                    if (e.KeyCode == Keys.F1)
                    {
                        CopyArtikel();
                    }
                    //Artikel speichern
                    if (e.KeyCode == Keys.F2)
                    {
                        SaveArtikelDaten();
                    }
                }
            }
            else
            {
                //erste Artikel - Check ob LVSNr vergeben
                decimal decTmp = 0;
                if (Decimal.TryParse(tbLVSNr.Text, out decTmp))
                {
                    //LVSNr ist vergeben 
                    //Artikel speichern
                    if (e.KeyCode == Keys.F2)
                    {
                        SaveArtikelDaten();
                    }
                }
            }
        }
        ///<summary>ctrEinlagerung / tbLVSNr_KeyDown</summary>
        ///<remarks>.</remarks> 
        private void tbLVSNr_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownProzess(e);
        }
        ///<summary>ctrEinlagerung / tbGArtSearch_KeyDown</summary>
        ///<remarks>.</remarks> 
        private void tbGArtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownProzess(e);
        }
        ///<summary>ctrEinlagerung / tbGArt_KeyDown</summary>
        ///<remarks>.</remarks> 
        private void tbGArt_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownProzess(e);
        }
        ///<summary>ctrEinlagerung / tbGArtZusatz_KeyDown</summary>
        ///<remarks>.</remarks> 
        private void tbGArtZusatz_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownProzess(e);
        }
        ///<summary>ctrEinlagerung / tbWerksnummer_KeyDown</summary>
        ///<remarks>.</remarks> 
        private void tbWerksnummer_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownProzess(e);
        }
        ///<summary>ctrEinlagerung / tbCharge_KeyDown</summary>
        ///<remarks>.</remarks> 
        private void tbCharge_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownProzess(e);
        }
        ///<summary>ctrEinlagerung / tbBestellnummer_KeyDown</summary>
        ///<remarks>.</remarks> 
        private void tbBestellnummer_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownProzess(e);
        }
        ///<summary>ctrEinlagerung / tbexMaterialnummer_KeyDown</summary>
        ///<remarks>.</remarks> 
        private void tbexMaterialnummer_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownProzess(e);
        }
        ///<summary>ctrEinlagerung / tbexBezeichnung_KeyDown</summary>
        ///<remarks>.</remarks> 
        private void tbexBezeichnung_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownProzess(e);
        }
        ///<summary>ctrEinlagerung / tbPos_KeyDown</summary>
        ///<remarks>.</remarks> 
        private void tbPos_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownProzess(e);
        }
        ///<summary>ctrEinlagerung / tbArtIDRef_KeyDown</summary>
        ///<remarks>.</remarks> 
        private void tbArtIDRef_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownProzess(e);
        }
        ///<summary>ctrEinlagerung / tbAnzahl_KeyDown</summary>
        ///<remarks>.</remarks> 
        private void tbAnzahl_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownProzess(e);
        }
        ///<summary>ctrEinlagerung / tbDicke_KeyDown</summary>
        ///<remarks>.</remarks> 
        private void tbDicke_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownProzess(e);
        }
        ///<summary>ctrEinlagerung / tbBreite_KeyDown</summary>
        ///<remarks>.</remarks> 
        private void tbBreite_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownProzess(e);
        }
        ///<summary>ctrEinlagerung / tbLaenge_KeyDown</summary>
        ///<remarks>.</remarks> 
        private void tbLaenge_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownProzess(e);
        }
        ///<summary>ctrEinlagerung / tbHoehe_KeyDown</summary>
        ///<remarks>.</remarks> 
        private void tbHoehe_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownProzess(e);
        }
        ///<summary>ctrEinlagerung / tbNetto_KeyDown</summary>
        ///<remarks>.</remarks> 
        private void tbNetto_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownProzess(e);
        }
        ///<summary>ctrEinlagerung / tbBrutto_KeyDown</summary>
        ///<remarks>.</remarks> 
        private void tbBrutto_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownProzess(e);
        }
        ///<summary>ctrEinlagerung / tbPackmittelGewicht_KeyDown</summary>
        ///<remarks>.</remarks> 
        private void tbPackmittelGewicht_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownProzess(e);
        }
        ///<summary>ctrEinlagerung / tsbtnJumpToEingang_Click</summary>
        ///<remarks>Läd direkt den eingegebene Eingang</remarks> 
        private void tsbtnJumpToEingang_Click(object sender, EventArgs e)
        {
            DoJumpToEingang();
        }
        ///<summary>ctrEinlagerung / tstbJumpEingangID_KeyPress</summary>
        ///<remarks></remarks>
        private void tstbJumpEingangID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (this.tstbJumpEingangID.Text != string.Empty)
                {
                    DoJumpToEingang();
                }
            }
        }
        ///<summary>ctrEinlagerung / DoJumpToEingang</summary>
        ///<remarks></remarks>
        private void DoJumpToEingang()
        {
            this._ctrMenu._frmMain.ResetStatusBar();
            this._ctrMenu._frmMain.InitStatusBar(4);
            this._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
            this._ctrMenu._frmMain.StatusBarWork(false, "Daten werden geladen und initialisiert...");

            decimal decTmp = 0;
            Decimal.TryParse(tstbJumpEingangID.Text, out decTmp);
            decimal decEIDToJump = clsLEingang.GetLEingangTableIDByLEingangID(this.GL_User.User_ID, decTmp, this._ctrMenu._frmMain.system);
            this.Lager.LEingangTableID = decEIDToJump;
            //JumpToLEingang(decEIDToJump);
            EingangBrowse(this.Lager.LEingangTableID, 0, enumBrowseAcivity.Item);

            this._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
        }
        ///<summary>ctrEinlagerung / tsbtnJumpToArtikel_Click</summary>
        ///<remarks>Läd direkt den eingegebene Artikel</remarks> 
        private void tsbtnJumpToArtikel_Click(object sender, EventArgs e)
        {
            //DoJumpToArtikel();
            if (this.tstbJumpArtID.Text != string.Empty)
            {
                decimal decTmp = 0;
                Decimal.TryParse(tstbJumpArtID.Text, out decTmp);
                decimal decArtToJump = clsArtikel.GetArtikelIDByLVSNr(this.GL_User, this._ctrMenu._frmMain.system, decTmp);
                DoJumpToArtikel(decArtToJump);
            }
        }
        ///<summary>ctrEinlagerung / tstbJumpArtID_KeyPress</summary>
        ///<remarks></remarks> 
        private void tstbJumpArtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (this.tstbJumpArtID.Text != string.Empty)
                {
                    decimal decTmp = 0;
                    Decimal.TryParse(tstbJumpArtID.Text, out decTmp);
                    decimal decArtToJump = clsArtikel.GetArtikelIDByLVSNr(this.GL_User, this._ctrMenu._frmMain.system, decTmp);
                    DoJumpToArtikel(decArtToJump);
                }
            }
        }
        ///<summary>ctrEinlagerung / DoJumpToArtikel</summary>
        ///<remarks></remarks> 
        public void DoJumpToArtikel(decimal myArtId)
        {
            this._ctrMenu._frmMain.ResetStatusBar();
            this._ctrMenu._frmMain.InitStatusBar(4);
            this._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
            this._ctrMenu._frmMain.StatusBarWork(false, "Daten werden geladen und initialisiert...");

            if (myArtId > 0)
            {
                LVS.Helper.helper_ArtikelArbeitsbereichCheck check = new LVS.Helper.helper_ArtikelArbeitsbereichCheck(myArtId, this._ctrMenu._frmMain.system, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.GL_User);

                if (check.FindInSameWorkspace)
                {
                    this._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
                    this.Lager.FillLagerDatenByArtikelId(check.ArtCheck.ID);
                    if (
                        (Lager.Artikel.Umbuchung) &&
                        (this._ctrMenu._frmMain.system.Client.ctrEinlagerung_tstbJumpArtID_DirectJumpToUBArtikel())
                      )
                    {
                        if (clsMessages.Lager_DirectTransferToUBArtikel())
                        {
                            this.Lager.FillLagerDatenByArtikelId(Lager.Artikel.ArtIDAfterUB);
                        }
                    }
                    else
                    {
                        if (!check.InfoText.Equals(string.Empty))
                        {
                            clsMessages.Allgemein_ERRORTextShow(check.InfoText);
                        }
                    }
                    EingangBrowse(0, this.Lager.Artikel.ID, enumBrowseAcivity.ArtItem);
                    bUpdate = true;
                    this._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
                }
                else
                {
                    this._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
                    if (clsMessages.Lager_Artikel_LVSNRNotExist(check.InfoText) == DialogResult.OK)
                    {
                        //direkt zum Artikel
                        this._ctrMenu._frmMain.EingangJumpDierctToArtInWorkspace(check.ArtCheck);
                    }
                }
            }
            else
            {
                string strText = "Die LVSNr. ist in keinem Arbeitsbereich vorhanden!";
                clsMessages.Allgemein_ERRORTextShow(strText);
            }
        }
        ///<summary>ctrEinlagerung / tsbtnExtraChargeAssignmentOpen_Click</summary>
        ///<remarks></remarks> 
        private void tsbtnExtraChargeAssignmentOpen_Click(object sender, EventArgs e)
        {
            bSetExtraChargeAssignmentToArt = false;
            this._ctrMenu.OpenCtrExtraChargeAssignment(this);
        }
        ///<summary>ctrEinlagerung / tsbtnExtraChargeAssignmentArt_Click</summary>
        ///<remarks></remarks> 
        private void tsbtnExtraChargeAssignmentArt_Click(object sender, EventArgs e)
        {
            bSetExtraChargeAssignmentToArt = true;
            this._ctrMenu.OpenCtrExtraChargeAssignment(this);
        }
        /******************************************************************************************
         *                      DGV Sonderkosten / ExtraCharge
         * ***************************************************************************************/
        ///<summary>ctrEinlagerung / InitDGVExtraChargeAssignment</summary>
        ///<remarks></remarks> 
        public void InitDGVExtraChargeAssignment()
        {
            clsExtraChargeAssignment ecAssTmp = new clsExtraChargeAssignment();
            ecAssTmp.InitClass(this.GL_User);
            DataTable dt = ecAssTmp.GetExtraChargeAssignmentList(true, Lager.Artikel.ID, this.Lager.Eingang.LEingangTableID);
            this.dgvExtraChargeAssignment.DataSource = dt;

            for (Int32 i = 0; i <= this.dgvExtraChargeAssignment.Columns.Count - 1; i++)
            {
                string colName = this.dgvExtraChargeAssignment.Columns[i].Name.ToString();
                switch (colName)
                {
                    case "ID":
                    case "ExtraChargeID":
                    case "ArtikelID":
                        this.dgvExtraChargeAssignment.Columns[i].IsVisible = false;
                        break;

                    case "LEingangID":
                        this.dgvExtraChargeAssignment.Columns[i].IsVisible = false;
                        this.dgvExtraChargeAssignment.Columns[i].HeaderText = "Eingang";
                        //this.dgvExtraChargeAssignment.Columns.Move(i,0);
                        break;

                    case "Datum":
                        this.dgvExtraChargeAssignment.Columns[i].FormatString = "{0:d}";
                        break;
                }
                this.dgvExtraChargeAssignment.Columns[i].AutoSizeMode = Telerik.WinControls.UI.BestFitColumnMode.AllCells;
            }
            this.dgvExtraChargeAssignment.BestFitColumns();
        }
        ///<summary>ctrEinlagerung / tsbtnArtECAdd_Click</summary>
        ///<remarks></remarks> 
        private void tsbtnArtECAdd_Click(object sender, EventArgs e)
        {
            bSetExtraChargeAssignmentToArt = true;
            this._ctrMenu.OpenCtrExtraChargeAssignment(this);
        }
        ///<summary>ctrEinlagerung / tsbtnArtECDelete_Click</summary>
        ///<remarks>Der ausgewählte Datensatz soll gelöscht werden</remarks> 
        private void tsbtnArtECDelete_Click(object sender, EventArgs e)
        {
            if (this.Lager.Artikel.ExtraChargeAssignment.ID > 0)
            {
                if (clsMessages.DeleteAllgemein())
                {
                    this.Lager.Artikel.ExtraChargeAssignment.Delete();
                    InitSelectedTabPage(tabArtikel.SelectedTab.Name.ToString());
                    //Baustelle
                    //InitDGVExtraChargeAssignment();
                    //InitGrdArtVita();
                }
            }
        }
        ///<summary>ctrEinlagerung / tsbtnArtECRefresh_Click</summary>
        ///<remarks>Refresh der Sonderkostenliste</remarks> 
        private void tsbtnArtECRefresh_Click(object sender, EventArgs e)
        {
            InitDGVExtraChargeAssignment();
        }
        ///<summary>ctrEinlagerung / dgvExtraCharge_MouseClick</summary>
        ///<remarks>Der ausgewählte Datensatz wird anhand der ID geladen</remarks> 
        private void dgvExtraCharge_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.dgvExtraChargeAssignment.Rows.Count > 0)
            {
                decimal decTmp = 0;
                string strTmp = this.dgvExtraChargeAssignment.Rows[this.dgvExtraChargeAssignment.CurrentRow.Index].Cells["ID"].Value.ToString();
                Decimal.TryParse(strTmp, out decTmp);
                if (decTmp > 0)
                {
                    this.Lager.Artikel.ExtraChargeAssignment.ID = decTmp;
                    this.Lager.Artikel.ExtraChargeAssignment.Fill();
                    this.dgvExtraChargeAssignment.CurrentRow.IsSelected = true;
                }
            }
        }
        ///<summary>ctrEinlagerung / OpenManAdrInput</summary>
        ///<remarks></remarks> 
        private void OpenManAdrInput(Button myBtn)
        {
            Int32 iAdrArtID = -1;
            Int32.TryParse(myBtn.Tag.ToString(), out iAdrArtID);
            if (iAdrArtID > -1)
            {
                //Falls der Eingang noch nicht gespeichert ist,so muss dies hier 
                //geschehen, da sonst keien EingangTableID vorhanden ist

                SaveEingangDaten(); // Speichert den Eingang immer ab falls eine manuelle adresse eingetragen wird 
                if (this.Lager.Eingang.LEingangTableID == 0)
                {

                    tbLEingangID.Text = Lager.Eingang.LEingangID.ToString();
                    tbLEingangTableID.Text = "[" + Lager.Eingang.LEingangTableID.ToString() + "]";
                    SetTsbtnEinlagerungSpeichernEnabled();
                    //SetLEingangsID();
                }

                if (this.Lager.Eingang.LEingangTableID > 0)
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

                    this.Lager.Eingang.AdrManuell.TableName = "LEingang";
                    this.Lager.Eingang.AdrManuell.TableID = this.Lager.Eingang.LEingangTableID;
                    this.Lager.Eingang.AdrManuell.AdrArtID = iAdrArtID;
                    this.Lager.Eingang.AdrManuell.FillbyTableAndAdrArtID();

                    this._ctrADRManAdd = new ctrADRManAdd();
                    this._ctrADRManAdd._ctrEinlagerung = this;
                    this._ctrMenu.OpenFrmTMP(this._ctrADRManAdd);
                }
            }
        }
        ///<summary>ctrEinlagerung / btnManSped_Click</summary>
        ///<remarks></remarks>
        private void btnManSped_Click(object sender, EventArgs e)
        {
            OpenManAdrInput((Button)sender);
        }
        ///<summary>ctrEinlagerung / btnManEntladestelle_Click</summary>
        ///<remarks></remarks>
        private void btnManEntladestelle_Click(object sender, EventArgs e)
        {
            OpenManAdrInput((Button)sender);
        }
        ///<summary>ctrEinlagerung / btnManEmpfaenger_Click</summary>
        ///<remarks></remarks>
        private void btnManEmpfaenger_Click(object sender, EventArgs e)
        {
            OpenManAdrInput((Button)sender);
        }
        ///<summary>ctrEinlagerung / btnManVersender_Click</summary>
        ///<remarks></remarks>
        private void btnManVersender_Click(object sender, EventArgs e)
        {
            OpenManAdrInput((Button)sender);
        }
        ///<summary>ctrEinlagerung / btnManAuftraggeber_Click</summary>
        ///<remarks>Setzt die LZZ auf datum heute</remarks>
        private void btnKWNow_Click(object sender, EventArgs e)
        {
            this.Lager.Artikel.LZZ = DateTime.Now;
            Functions.SetKWValue(ref nudLzzKW, this.Lager.Artikel.LZZ);
            Functions.SetYearValue(ref nudLzzJahr, this.Lager.Artikel.LZZ);
        }
        ///<summary>ctrEinlagerung / tbGArtSearch_Validated</summary>
        ///<remarks></remarks>
        private void tbGArtSearch_Validated(object sender, EventArgs e)
        {
            TakeOverGueterArt(_decGArtID);
        }
        ///<summary>ctrEinlagerung / btnFreeForCallReset_Click</summary>
        ///<remarks>Freigabe zum Abruf wird zurück auf False gesetzt</remarks>
        private void btnFreeForCallReset_Click(object sender, EventArgs e)
        {
            Lager.Artikel.FreigabeAbruf = false;
            Lager.Artikel.ResetFreeForCallByArtikel();
            //Lager.Artikel.GetArtikeldatenByTableID();
            //SetArtikelToForm(this.Lager.Artikel.ID, false);
            EingangBrowse(0, this.Lager.Artikel.ID, enumBrowseAcivity.ArtInItem);
        }
        ///<summary>ctrEinlagerung / tbSearchES_TextChanged</summary>
        ///<remarks>Entladestelle Adresssuche</remarks>
        private void tbSearchES_TextChanged(object sender, EventArgs e)
        {
            //Adressdaten laden
            DataTable dt = new DataTable();
            dt = clsADR.GetADRList(this.GL_User.User_ID);
            DataTable dtTmp = new DataTable();

            string SearchText = tbSearchES.Text.ToString();
            string Ausgabe = string.Empty;

            DataRow[] rows = dt.Select("Suchbegriff LIKE '" + SearchText + "'", "Suchbegriff");
            dtTmp = dt.Clone();
            foreach (DataRow row in rows)
            {
                Ausgabe = Ausgabe + row["Suchbegriff"].ToString() + "\n";
                dtTmp.ImportRow(row);
            }
            //tbEntladestelle.Text = Functions.GetADRStringFromTable(dtTmp);
            if (dtTmp.Rows.Count > 0)
            {
                decimal decTmp = Functions.GetADR_IDFromTable(dtTmp);
                if (this._ctrMenu._frmMain.system.AbBereich.ASNTransfer)
                {
                    //SearchButton
                    // 1 = Auftraggeber
                    // 2 = Versender
                    // 3 = Empfänger
                    // 4 = neutrale Versandadresse
                    // 5 = neutrale Empfangsadresse
                    // 6 = Mandanten
                    // 7 = Spedition
                    // 8 = Kunden
                    // 9 = neutraler Auftraggeber
                    // 10 = Rechnungsadresse
                    // 11 = Postadresse
                    // 12 = Entladestelle
                    // 13 = Beladestelle
                    // 14 = AbrufEmpfänger
                    // 15 = AbrufSpedition
                    // 16 = AbrufEntladestelle

                    SearchButton = 12;
                    SetKDRecAfterADRSearch(decTmp, false);
                }
            }
        }
        ///<summary>ctrEinlagerung / LoadEingang</summary>
        ///<remarks></remarks>
        //public void LoadEingang(Int32 eingang)
        //{
        //    bUpdate = true;
        //    bBack = false;
        //    //Eingabefelder leeren und neue LEingangsID wird ermittelt            
        //    InitEingabe();
        //    decimal LEingangTableID = clsLEingang.GetLEingangTableIDByLEingangID(this.GL_User.User_ID, eingang, this._ctrMenu._frmMain.system);
        //    Lager.LEingangTableID = LEingangTableID;
        //    Lager.FillLagerDaten(true);
        //    SetLEingangskopfdatenToFrm(true);
        //    InitArtikelLoad();
        //    SetArtikelMenuBtnEnabled();
        //}
        ///<summary>ctrEinlagerung / setView</summary>
        ///<remarks></remarks>
        private void setView(ref DataTable dt, ref RadGridView dgv, string viewname)
        {
            string MissingOnes = "";
            if (viewname != "")
            {
                //Dictionary<string, List<string>> dicViews = _ctrMenu._frmMain.GL_System.DictViews.GetValueOrNull("LEingang");
                Dictionary<string, List<string>> dicViews;
                _ctrMenu._frmMain.GL_System.DictViews.TryGetValue("LEingang", out dicViews);
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
                        // Spalte des Views in DB Query nicht enthalten ...
                        if (MissingOnes != "")
                            MissingOnes += ", ";
                        MissingOnes += temp;
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
        }
        ///<summary>ctrEinlagerung / tscbView_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void tscbView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tscbView.SelectedIndex > -1)
            {
                if (dtArtikel.Rows.Count > 0)
                {
                    dgv.DataSource = null;
                    InitDGV();
                }
            }
        }
        ///<summary>ctrEinlagerung / dgv_CellClick</summary>
        ///<remarks></remarks>
        private void dgv_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dgv.Rows.Count > 0)
                {
                    if (!this.Lager.Artikel.IsKorStVerUse)
                    {
                        if (this.dgv.CurrentRow != null)
                        {
                            decimal decTmp = 0;
                            decimal.TryParse(this.dgv.Rows[e.RowIndex].Cells["ArtikelID"].Value.ToString(), out decTmp);
                            if (decTmp > 0)
                            {
                                //alle Eingabefelder leeren
                                ClearArtikelEingabeFelder(true);
                                EingangBrowse(0, decTmp, enumBrowseAcivity.ArtInItem);
                            }
                        }
                    }
                    else
                    {
                        string strMes = "Das Storno-/Korrekturverfahren ist noch aktiv. Bitte beenden Sie den Vorgang, indem Sie den Eingang komplett abschließen.";
                        clsMessages.Allgemein_ERRORTextShow(strMes);
                    }
                }
            }
            SetEingangLocked();
        }
        ///<summary>ctrEinlagerung / dgv_SelectionChanged</summary>
        ///<remarks></remarks>
        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (this.Lager.Artikel.IsKorStVerUse)
            {
                SetSelectedRowInDGV();
            }
        }
        ///<summary>ctrEinlagerung / tsbtnArtKorrStorVerfahren_Click</summary>
        ///<remarks></remarks>
        private void tsbtnArtKorrStorVerfahren_Click(object sender, EventArgs e)
        {
            if (clsMessages.Artikel_UseKorrStorVerfahren())
            {
                //if ((this.Lager.Artikel.ID > 1) && (this.Lager.Artikel.IsKorStVerUse))
                if ((this.Lager.Artikel.ID > 1))
                {
                    if (this.Lager.Artikel.UpdateArtikelForKorrekturStorVerfahren(true))
                    {
                        SetLabelDirektanlieferung(false);
                        //true wird der Artikel direkt mit freigegben
                        //Eintrag in ArtikelVita
                        clsArtikelVita.AddArtikelKorrekturStVerfahren(this.GL_User, this.Lager.Artikel.ID, enumLagerAktionen.StornoKorrekturVerfahren.ToString());
                        _bLEingangIsChecked = false;
                        clsLager.UpdateLEingangCheck(this.GL_User.User_ID, _bLEingangIsChecked, this.Lager.LEingangTableID);
                        EingangBrowse(this.Lager.LEingangTableID, this.Lager.Artikel.ID, enumBrowseAcivity.ArtItem);
                    }
                }
            }
        }
        ///<summary>ctrEinlagerung / tsbtnBackToBestand_Click</summary>
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
            this._ctrMenu.CloseCtrEinlagerung();
        }
        ///<summary>ctrEinlagerung / tsbtnDeleteSchaden_Click</summary>
        ///<remarks></remarks>
        private void tsbtnDeleteSchaden_Click(object sender, EventArgs e)
        {
            DeleteSchaden();
            InitDGVSchaden();
        }
        ///<summary>ctrEinlagerung / tbLfsNr_Validated</summary>
        ///<remarks></remarks>
        private void tbLfsNr_Validated(object sender, EventArgs e)
        {
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_RequiredValue_LieferscheinNr)
            {
                if (this.tbLfsNr.Text == string.Empty)
                {
                    clsMessages.Allgemein_PflichtfeldNichtLeer("Lieferscheinnummer");
                }
            }
        }
        ///<summary>ctrEinlagerung / dgv_ToolTipTextNeeded</summary>
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
        ///<summary>ctrEinlagerung / tsbtnRefreshVita_Click</summary>
        ///<remarks></remarks>
        private void tsbtnRefreshVita_Click(object sender, EventArgs e)
        {
            InitGrdArtVita();
        }
        ///<summary>ctrEinlagerung / tsbtnCheckALLArtikel_Click</summary>
        ///<remarks></remarks>
        private void tsbtnCheckALLArtikel_Click(object sender, EventArgs e)
        {
            string strInfo = string.Empty;
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_RequiredValue_Halle)
            {
                if (!this.Lager.Artikel.CheckAllArtikelInEingangPacedinHalle())
                {
                    strInfo = strInfo + "- Feld [Halle] " + Environment.NewLine;
                }
                else
                {
                    bool bAllCheckedFromBeginning = true;
                    for (Int32 i = 0; i <= dtArtikel.Rows.Count - 1; i++)
                    {
                        decimal decTmp = 0;
                        string strTmp = dtArtikel.Rows[i]["ArtikelID"].ToString();
                        decimal.TryParse(strTmp, out decTmp);
                        if (decTmp > 0)
                        {
                            Lager.Artikel.ID = decTmp;
                            Lager.Artikel._GL_User = this.GL_User;
                            Lager.Artikel.GetArtikeldatenByTableID();

                            if (!Lager.Artikel.EingangChecked)
                            {
                                clsArtikel.UpdateArtikelCheck(this.GL_User, true, decTmp);
                                bAllCheckedFromBeginning = false;
                            }
                        }
                    }
                    SetArtikelToForm(this.Lager.Artikel.ID, false);
                    if (!bAllCheckedFromBeginning)
                    {
                        Int32 iCount = 0;
                        //if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_Print_DirectEingangDoc)
                        //{
                        //    lastprintID = this.Lager.Eingang.LEingangTableID;
                        //    DirectPrintLEingangDoc();
                        //}

                        //Baustelle Print
                        //if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_Print_DirectList)
                        //{
                        //    DirectPrintList();
                        //}
                        if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
                        {
                            //this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_Print_DirectEingangDoc in 
                            lastprintID = this.Lager.Eingang.LEingangTableID;
                            DirectPrintLEingangDoc();

                            DirectPrintList();
                        }
                        else
                        {
                            if (DoDirectPrintReport(enumIniDocKey.Eingangsliste))
                            {
                                this.Lager.Eingang.UpdatePrintLEingang(enumIniDocKey.Eingangsliste.ToString());
                            }
                        }
                        CheckDirectPrintLabel();
                    }
                }
            }

            if (strInfo != string.Empty)
            {
                string strKopf = "Der Die Artikel können nicht Abgeschlossen werden, da die Prüfung folgender Felder negativ verlaufen ist:" + Environment.NewLine;
                strInfo = strKopf + strInfo;
                clsMessages.Allgemein_ERRORTextShow(strInfo);
            }
        }
        ///<summary>ctrEinlagerung / tsbtnCheckAll_Click</summary>
        ///<remarks></remarks>
        private void tsbtnCheckAll_Click(object sender, EventArgs e)
        {
            string strInfo = string.Empty;
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_RequiredValue_Halle)
            {
                if (!this.Lager.Artikel.CheckAllArtikelInEingangPacedinHalle())
                {
                    strInfo = strInfo + "- Feld [Halle] " + Environment.NewLine;
                }
                else
                {
                    bool bAllCheckedFromBeginning = true;
                    for (Int32 i = 0; i <= dtArtikel.Rows.Count - 1; i++)
                    {
                        decimal decTmp = 0;
                        string strTmp = dtArtikel.Rows[i]["ArtikelID"].ToString();
                        decimal.TryParse(strTmp, out decTmp);
                        if (decTmp > 0)
                        {
                            Lager.Artikel.ID = decTmp;
                            Lager.Artikel._GL_User = this.GL_User;
                            Lager.Artikel.GetArtikeldatenByTableID();

                            if (!Lager.Artikel.EingangChecked)
                            {
                                clsArtikel.UpdateArtikelCheck(this.GL_User, true, decTmp);
                                bAllCheckedFromBeginning = false;
                            }
                        }
                    }
                    SetArtikelToForm(Lager.Artikel.ID, false);


                    if (!bAllCheckedFromBeginning)
                    {
                        //Int32 iCount = 0;
                        //if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_Print_DirectEingangDoc)
                        //{
                        //    lastprintID = this.Lager.Eingang.LEingangTableID;
                        //    DirectPrintLEingangDoc();
                        //}
                        //Baustelle Print
                        //if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_Print_DirectList)
                        //{
                        //    DirectPrintList();
                        //}
                        if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
                        {
                            //this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_Print_DirectEingangDoc in 
                            lastprintID = this.Lager.Eingang.LEingangTableID;
                            DirectPrintLEingangDoc();

                            DirectPrintList();
                        }
                        else
                        {
                            if (DoDirectPrintReport(enumIniDocKey.Eingangsliste))
                            {
                                this.Lager.Eingang.UpdatePrintLEingang(enumIniDocKey.Eingangsliste.ToString());
                            }
                        }
                        CheckDirectPrintLabel();
                    }
                }
            }
            if (strInfo != string.Empty)
            {
                string strKopf = "Der Die Artikel können nicht Abgeschlossen werden, da die Prüfung folgender Felder negativ verlaufen ist:" + Environment.NewLine;
                strInfo = strKopf + strInfo;
                clsMessages.Allgemein_ERRORTextShow(strInfo);
            }
        }
        ///<summary>ctrEinlagerung / tbReihe_Enter</summary>
        ///<remarks></remarks>
        private void tbReihe_Enter(object sender, EventArgs e)
        {
            if (this._ctrMenu._frmMain.system.AbBereich.UseAutoRowAssignment)
            {
                // Reihe vorschlagen .
                if (tbReihe.Text == string.Empty)
                {
                    clsReihe Reihe = new clsReihe();
                    Reihe._GL_User = this.GL_User;
                    tbReihe.Text = Reihe.GetVorschlag(tbDicke.Text, tbBreite.Text, tbLaenge.Text, tbHoehe.Text, tbBrutto.Text, _decGArtID.ToString());
                }
            }
        }
        ///<summary>ctrEinlagerung / pbSPL_Click</summary>
        ///<remarks></remarks>
        private void pbSPL_Click(object sender, EventArgs e)
        {
            //if (this._ctrMenu._frmMain.system.Client.Modul.Lager_SPL_OutFromEingang)
            //{
            //    clsSPL spl = new clsSPL();
            //    spl.ArtikelID = Lager.Artikel.ID;
            //    spl.FillLastINByArtikelID();
            //    if (spl.CheckArtikelInSPL())
            //    {
            //        spl.lstCheckOut = new List<Int32>();
            //        spl.lstCheckOut.Add((Int32)spl.ArtikelID);
            //        if (clsMessages.Lager_SPL_Out() && spl.DoSPLCheckOutInOldEingang())
            //        {
            //            SaveArtikel();
            //            SetArtikelToForm(spl.ArtikelID, false);
            //        }
            //    }
            //}
        }
        ///<summary>ctrEinlagerung / tsbtnPreisRefresh_Click</summary>
        ///<remarks></remarks>
        private void tsbtnPreisRefresh_Click(object sender, EventArgs e)
        {
            clsFaktLager fakt = new clsFaktLager();
            fakt.BenutzerID = this.GL_User.User_ID;
            Dictionary<string, decimal> dictPreis = fakt.CalcArtikel(this.Lager.Artikel);
            if (dictPreis.Keys.Contains("Einlagerungskosten"))
            {
                tbEinlagerungCalc.Text = dictPreis["Einlagerungskosten"].ToString();
            }
            if (dictPreis.Keys.Contains("Auslagerungskosten"))
            {
                tbAuslagerungCalc.Text = dictPreis["Auslagerungskosten"].ToString();
            }
            if (dictPreis.Keys.Contains("Lagerkosten"))
            {
                tbLagergeldCalc.Text = dictPreis["Lagerkosten"].ToString();
            }
        }
        ///<summary>ctrEinlagerung / mtbKFZ_Enter</summary>
        ///<remarks></remarks>
        private void mtbKFZ_Enter(object sender, EventArgs e)
        {
            if (cbFahrzeug.Text.Equals(ctrEinlagerung.const_cbFahrzeugText_Fremdfahrzeug))
            {
                if (mtbKFZ.Text.Equals(ctrEinlagerung.const_cbFahrzeugText_Fremdfahrzeug))
                {
                    mtbKFZ.Text = string.Empty;
                }
            }
        }
        ///<summary>ctrEinlagerung / mtbKFZ_Leave</summary>
        ///<remarks></remarks>
        private void mtbKFZ_Leave(object sender, EventArgs e)
        {
            if (cbFahrzeug.Text.Equals(ctrEinlagerung.const_cbFahrzeugText_Fremdfahrzeug))
            {
                if (mtbKFZ.Text.Equals(string.Empty))
                {
                    mtbKFZ.Text = ctrEinlagerung.const_cbFahrzeugText_Fremdfahrzeug;
                }
            }
        }
        ///<summary>ctrEinlagerung / pbSchaden_MouseClick</summary>
        ///<remarks></remarks>
        private void pbSchaden_MouseClick(object sender, MouseEventArgs e)
        {
            pbSchaden.ContextMenuStrip.Items.Clear();
            this.eDokumentenArt = enumDokumentenArt.DEFAULT;
            if (e.Button == MouseButtons.Right)
            {
                if (!this.Lager.Artikel.SchadenTopOne.Equals(string.Empty))
                {
                    if (!this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
                    {
                        ToolStripMenuItem customMenuItem2;
                        customMenuItem2 = new ToolStripMenuItem();
                        customMenuItem2.Text = "Schaden-Label drucken";
                        customMenuItem2.Tag = enumDokumentenArt.SchadenLabel;
                        //customMenuItem2.Click += new EventHandler(this.DoPrintLabelOrDoc);
                        customMenuItem2.Click += new EventHandler(this.PrintAktion);
                        pbSchaden.ContextMenuStrip.Items.Add(customMenuItem2);

                        ToolStripMenuItem customMenuItem4;
                        customMenuItem4 = new ToolStripMenuItem();
                        customMenuItem4.Text = "Schadenmeldung drucken";
                        customMenuItem4.Tag = enumDokumentenArt.SchadenDoc;
                        //customMenuItem4.Click += new EventHandler(this.DoPrintLabelOrDoc);
                        customMenuItem4.Click += new EventHandler(this.PrintAktion);
                        pbSchaden.ContextMenuStrip.Items.Add(customMenuItem4);
                    }
                }
            }
        }
        ///<summary>ctrEinlagerung / pbSPL_MouseClick</summary>
        ///<remarks></remarks>
        private void pbSPL_MouseClick(object sender, MouseEventArgs e)
        {
            pbSPL.ContextMenuStrip.Items.Clear();
            this.eDokumentenArt = enumDokumentenArt.DEFAULT;
            if (e.Button == MouseButtons.Right)
            {
                if (this.Lager.Artikel.bSPL)
                {
                    if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
                    {
                        ToolStripMenuItem customMenuItem1;
                        customMenuItem1 = new ToolStripMenuItem();
                        customMenuItem1.Text = "Sperrlager-Label drucken";
                        customMenuItem1.Click += new EventHandler(this.OpenCtrSPL);
                        pbSPL.ContextMenuStrip.Items.Add(customMenuItem1);

                        ToolStripMenuItem customMenuItem3;
                        customMenuItem3 = new ToolStripMenuItem();
                        customMenuItem3.Text = "Sperrlagermeldung drucken";
                        customMenuItem3.Click += new EventHandler(this.PrintSPLDoc);
                        pbSPL.ContextMenuStrip.Items.Add(customMenuItem3);
                    }
                    else
                    {
                        ToolStripMenuItem customMenuItem1;
                        customMenuItem1 = new ToolStripMenuItem();
                        customMenuItem1.Text = "Sperrlager-Label drucken";
                        customMenuItem1.Tag = enumDokumentenArt.SPLLabel;
                        //this.eDokumentenArt = Globals.enumDokumentenart.SPLLabel;
                        //customMenuItem1.Click += new EventHandler(this.DoPrintLabelOrDoc);
                        customMenuItem1.Click += new EventHandler(this.PrintAktion);
                        pbSPL.ContextMenuStrip.Items.Add(customMenuItem1);

                        ToolStripMenuItem customMenuItem3;
                        customMenuItem3 = new ToolStripMenuItem();
                        customMenuItem3.Text = "Sperrlagermeldung drucken";
                        customMenuItem3.Tag = enumDokumentenArt.SPLDoc;
                        //this.eDokumentenArt = Globals.enumDokumentenart.SPLDoc;
                        //customMenuItem3.Click += new EventHandler(this.DoPrintLabelOrDoc);
                        customMenuItem3.Click += new EventHandler(this.PrintAktion);
                        pbSPL.ContextMenuStrip.Items.Add(customMenuItem3);
                    }
                }
            }
        }
        ///<summary>ctrEinlagerung / OpenCtrSPL</summary>
        ///<remarks>.</remarks> 
        private void OpenCtrSPL(object sender, EventArgs e)
        {
            //Druck SPL Dokumente einfügen
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_SPL_PrintSPLDocument)
            {
                if (this.Lager.Artikel.ID > 0)
                {
                    this.Lager.SPL.ArtikelID = this.Lager.Artikel.ID;
                    this.Lager.SPL.FillLastINByArtikelID(false);

                    ctrSPLAdd tmpSPL = new ctrSPLAdd(this);
                    this._ctrMenu.OpenFrmTMP(tmpSPL);

                    if (this._ctrMenu._frmMain.system.Client.Modul.Lager_SPL_AutoPrintSPLDocument)
                    {

                    }
                    else
                    {
                        //Abfrage
                    }
                }
            }
        }

        ///<summary>ctrEinlagerung / DoDirectPrintReport</summary>
        ///<remarks>.</remarks>
        private bool DoDirectPrintReport(enumIniDocKey myDocKey)
        {
            bool bPrintOK = false;
            ctrPrintLager TmpPrint = new ctrPrintLager();
            TmpPrint.Hide();
            TmpPrint._ctrMenu = this._ctrMenu;
            TmpPrint._ctrEinlagerung = this;
            try
            {
                this._ctrMenu._frmMain.system.ReportDocSetting.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, this.Lager.Eingang.Auftraggeber, this._ctrMenu._frmMain.system.AbBereich.ID);
                TmpPrint.RepDocSettings = null;
                TmpPrint.RepDocSettings = this._ctrMenu._frmMain.system.ReportDocSetting.ListReportDocEingang.Find(x => x.DocKey.Equals(myDocKey.ToString()));
                if (TmpPrint.RepDocSettings is clsReportDocSetting)
                {
                    TmpPrint.nudPrintCount.Value = TmpPrint.RepDocSettings.PrintCount;
                    TmpPrint._DokumentenArt = TmpPrint.RepDocSettings.DocKey;
                    TmpPrint._DocPath = TmpPrint.RepDocSettings.DocFileNameAndPath;
                    TmpPrint.SetLagerDatenToFrm();

                    switch (myDocKey)
                    {
                        case enumIniDocKey.EingangDoc:
                            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_Print_DirectEingangDoc)
                            {
                                if (!this.Lager.Eingang.IsPrintDoc)
                                {
                                    TmpPrint.StartDirectPrint();
                                    bPrintOK = true;
                                }
                            }
                            break;

                        case enumIniDocKey.Eingangsliste:
                            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_Print_DirectList)
                            {
                                if (!this.Lager.Eingang.IsPrintList)
                                {
                                    TmpPrint.StartDirectPrint();
                                    bPrintOK = true;
                                }
                            }
                            break;

                        case enumIniDocKey.LabelAll:
                            TmpPrint.StartDirectPrint();
                            bPrintOK = true;
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                bPrintOK = false;
                string strEx = ex.ToString();
            }
            finally
            {
                TmpPrint.Dispose();
            }
            return bPrintOK;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintAktion(object sender, EventArgs e)
        {
            try
            {
                enumDokumentenArt eDocArt = enumDokumentenArt.DEFAULT;

                if (typeof(ToolStripMenuItem) == sender.GetType())
                {
                    ToolStripMenuItem tmpMI = (ToolStripMenuItem)sender;
                    eDocArt = (enumDokumentenArt)tmpMI.Tag;
                }
                else if (typeof(ToolStripButton) == sender.GetType())
                {
                    ToolStripButton tmpMI = (ToolStripButton)sender;
                    eDocArt = (enumDokumentenArt)tmpMI.Tag;
                }


                //Globals.enumDokumentenart eDocArt = (Globals.enumDokumentenart)tmpMI.Tag;
                this.eDokumentenArt = eDocArt;
                //Druck SPL Dokumente einfügen
                if (this._ctrMenu._frmMain.system.Client.Modul.Lager_SPL_PrintSPLDocument)
                {
                    if (this.Lager.Artikel.ID > 0)
                    {
                        this.Lager.SPL.ArtikelID = this.Lager.Artikel.ID;
                        this.Lager.SPL.FillLastINByArtikelID(false);
                        if (this._ctrMenu._frmMain.system.Client.Modul.Lager_SPL_AutoPrintSPLDocument)
                        {
                            ctrPrintLager TmpPrint = new ctrPrintLager();
                            TmpPrint.Hide();
                            TmpPrint._ctrMenu = this._ctrMenu;
                            TmpPrint._ctrEinlagerung = this;
                            if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
                            {
                                TmpPrint._DokumentenArt = this.eDokumentenArt.ToString();
                                TmpPrint.nudPrintCount.Value = this._ctrMenu._frmMain.GL_System.docPath_SPLDoc_Count;
                            }
                            else
                            {
                                this._ctrMenu._frmMain.system.ReportDocSetting.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, this.Lager.Eingang.Auftraggeber, this._ctrMenu._frmMain.system.AbBereich.ID);
                                TmpPrint.RepDocSettings = null; ;
                                TmpPrint.RepDocSettings = this._ctrMenu._frmMain.system.ReportDocSetting.ListReportDocEingang.Find(x => x.DocKey.Equals(this.eDokumentenArt.ToString()));
                                TmpPrint.nudPrintCount.Value = TmpPrint.RepDocSettings.PrintCount;
                                TmpPrint._DokumentenArt = TmpPrint.RepDocSettings.DocKey;
                                TmpPrint._DocPath = TmpPrint.RepDocSettings.DocFileNameAndPath;
                            }
                            TmpPrint.SetLagerDatenToFrm();
                            TmpPrint.StartPrintSPLDoc(true);
                            TmpPrint.Dispose();
                        }
                        else
                        {
                            ctrSPLAdd tmpSPL = new ctrSPLAdd(this);
                            switch (eDokumentenArt)
                            {
                                case enumDokumentenArt.SPLDoc:
                                    tmpSPL.tsbtnPrintSPLLabel.Visible = false;
                                    tmpSPL.tsbtnPrintSPLDoc.Visible = true;
                                    this._ctrMenu.OpenFrmTMP(tmpSPL);
                                    break;

                                case enumDokumentenArt.SPLLabel:
                                    tmpSPL.tsbtnPrintSPLLabel.Visible = true;
                                    tmpSPL.tsbtnPrintSPLDoc.Visible = false;
                                    this._ctrMenu.OpenFrmTMP(tmpSPL);
                                    break;

                                case enumDokumentenArt.SchadenDoc:
                                case enumDokumentenArt.SchadenLabel:
                                    ctrPrintLager TmpPrint = new ctrPrintLager();
                                    TmpPrint.Hide();
                                    TmpPrint._ctrMenu = this._ctrMenu;
                                    TmpPrint._ctrEinlagerung = this;
                                    TmpPrint._DokumentenArt = eDokumentenArt.ToString();
                                    if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
                                    {
                                        TmpPrint._DokumentenArt = this.eDokumentenArt.ToString();
                                        TmpPrint.nudPrintCount.Value = this._ctrMenu._frmMain.GL_System.docPath_SPLDoc_Count;
                                        TmpPrint.SetLagerDatenToFrm();
                                        TmpPrint.StartPrintDoc(true);
                                    }
                                    else
                                    {
                                        this._ctrMenu._frmMain.system.ReportDocSetting.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, this.Lager.Eingang.Auftraggeber, this._ctrMenu._frmMain.system.AbBereich.ID);
                                        TmpPrint.RepDocSettings = null;
                                        TmpPrint.RepDocSettings = this._ctrMenu._frmMain.system.ReportDocSetting.ListReportDocEingang.Find(x => x.DocKey.Equals(this.eDokumentenArt.ToString()));
                                        if (TmpPrint.RepDocSettings is clsReportDocSetting)
                                        {
                                            TmpPrint.nudPrintCount.Value = TmpPrint.RepDocSettings.PrintCount;
                                            TmpPrint._DokumentenArt = TmpPrint.RepDocSettings.DocKey;
                                            TmpPrint._DocPath = TmpPrint.RepDocSettings.DocFileNameAndPath;
                                            TmpPrint.SetLagerDatenToFrm();
                                            TmpPrint.StartPrintDoc(true);
                                        }
                                        else
                                        {
                                            clsMessages.Print_Fail_ReportAssignment();

                                        }
                                    }

                                    TmpPrint.Dispose();
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsMessages.Allgemein_ERRORTextShow(ex.ToString());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myPrintCtr"></param>
        private void PrintByReportDocSetting(ctrPrintLager myPrintCtr)
        {
            myPrintCtr.Hide();
            myPrintCtr._ctrMenu = this._ctrMenu;
            myPrintCtr._ctrEinlagerung = this;
            myPrintCtr._DokumentenArt = eDokumentenArt.ToString();

            if (myPrintCtr.RepDocSettings is clsReportDocSetting)
            {
                myPrintCtr.nudPrintCount.Value = myPrintCtr.RepDocSettings.PrintCount;
                myPrintCtr._DokumentenArt = myPrintCtr.RepDocSettings.DocKey;
                myPrintCtr._DocPath = myPrintCtr.RepDocSettings.DocFileNameAndPath;
                myPrintCtr.SetLagerDatenToFrm();
                myPrintCtr.StartPrintDoc(true);
            }
            else
            {
                clsMessages.Print_Fail_ReportAssignment();

            }
            myPrintCtr.Dispose();
        }
        ///<summary>ctrEinlagerung / TransferArtOutOfSPL</summary>
        ///<remarks></remarks>
        //private void TransferArtOutOfSPL(object sender, EventArgs e)
        //{
        //    if (this._ctrMenu._frmMain.system.Client.Modul.Lager_SPL_OutFromEingang)
        //    {
        //        bool IsBookedOut = SperrlagerViewData.BookingArticleOutSpl((int)Lager.Artikel.ID, (int)this.GL_User.User_ID);
        //        if (clsMessages.Lager_SPL_Out() && (IsBookedOut))
        //        {
        //            SaveArtikel();
        //            SetArtikelToForm(Lager.Artikel.ID, false);
        //            this.Lager.Artikel.ID = Lager.Artikel.ID;
        //            this.Lager.Artikel.GetArtikeldatenByTableID();
        //            InitASNTransfer(clsASNAction.const_ASNAction_SPLOut);
        //        }

        //        //clsSPL spl = new clsSPL();
        //        //spl.ArtikelID = Lager.Artikel.ID;
        //        //spl.FillLastINByArtikelID();
        //        //if (spl.CheckArtikelInSPL())
        //        //{
        //        //    spl.lstCheckOut = new List<Int32>();
        //        //    spl.lstCheckOut.Add((Int32)spl.ArtikelID);
        //        //    if (clsMessages.Lager_SPL_Out() && spl.DoSPLCheckOutInOldEingang())
        //        //    {
        //        //        SaveArtikel();
        //        //        SetArtikelToForm(spl.ArtikelID, false);
        //        //        this.Lager.Artikel.ID = spl.ArtikelID;
        //        //        this.Lager.Artikel.GetArtikeldatenByTableID();
        //        //        InitASNTransfer(clsASNAction.const_ASNAction_SPLOut);
        //        //    }
        //        //}
        //    }
        //}
        /******************************************************************************************
         *                              tabPageImages
         * ***************************************************************************************/
        ///<summary>ctrEinlagerung / InitLVArtImages</summary>
        ///<remarks></remarks>
        private void InitLVArtImages()
        {
            this.pbImageThumb.Image = null;
            DataTable dtListView = new DataTable();
            //dtListView = clsDocScan.GetImages(this.GL_User, enumDatabaseTableNames.Artikel.ToString(), this.Lager.Artikel.ID);
            //lvArtImages.DataSource = null;
            //lvArtImages.DataSource = dtListView;
            //lvArtImages.DisplayMember = "ScanFileName";
            //lvArtImages.ValueMember = "ID";

            dtListView = ImageViewData.GetImages(this.GL_User, enumDatabaseSped4_TableNames.Artikel.ToString(), this.Lager.Artikel.ID, _ctrMenu._frmMain.system.AbBereich.ID);
            lvArtImages.DataSource = null;
            lvArtImages.DataSource = dtListView;
            lvArtImages.DisplayMember = "ScanFileName";
            lvArtImages.ValueMember = "Id";
        }
        ///<summary>ctrEinlagerung / SetMenuArtikelSchadenEnabled</summary>
        ///<remarks>Wenn der Artikel ausgelager ist, soll der Delete Button deaktiviert sein</remarks> 
        private void SetMenuArtikelImagesEnabled()
        {
            bool bEnabled = true;
            // Test mr
            //if ((this.Lager.Artikel.Ausgang!=null) && (this.Lager.Artikel.Ausgang.Checked))
            //{
            //        bEnabled = false;             
            //}
            this.tsbtnImageAdd.Enabled = bEnabled;
            this.tsbtnImageDelete.Enabled = bEnabled;
        }
        ///<summary>ctrEinlagerung / lvArtImages_SelectedItemChanged</summary>
        ///<remarks></remarks>
        private void lvArtImages_SelectedItemChanged(object sender, EventArgs e)
        {
            Image imgLV = null;
            this.pbImageThumb.Image = imgLV;
            RadListViewElement element = (RadListViewElement)sender;
            ListViewDataItem item = (ListViewDataItem)element.SelectedItem;
            if (item != null)
            {
                try
                {
                    string strIdTmp = item["ID"].ToString();
                    int iIdTmp = 0;
                    if (int.TryParse(strIdTmp, out iIdTmp))
                    {
                        this.Lager.ImageVD = new ImageViewData(GL_User, iIdTmp);
                        imgLV = helper_Image.ByteArrayToImage(this.Lager.ImageVD.Image.DocImage);
                    }


                    //decimal decTmp = 0;
                    //if (Decimal.TryParse(item["ID"].ToString(), out decTmp))
                    //{
                    //    this.Lager.DocScan.ID = decTmp;
                    //    this.Lager.DocScan.Fill();

                    //    clsImages img = new clsImages();
                    //    img.byteArrayIn = (byte[])item["DocImage"];
                    //    img.ImageIn = img.ConvertByteArrayToImage();
                    //    img.ResizeImage((decimal)pbImageThumb.Width - 5, (decimal)pbImageThumb.Height - 5, true);
                    //    //imgLV = img.ThumbImage;

                    //    this.Lager.DocScan.img = img.Copy();
                    //}
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                }
            }
            this.pbImageThumb.Image = imgLV;
        }
        ///<summary>ctrEinlagerung / lvArtImages_ItemDataBound</summary>
        ///<remarks></remarks>
        private void lvArtImages_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)e.Item.Value;
                decimal decTmpDocScanID = 0;
                Decimal.TryParse(row["ID"].ToString(), out decTmpDocScanID);

                string extension = row["ScanFilename"].ToString();
                if (extension.Contains(string.Empty))
                {
                    Icon FileIcon = clsFileIconLoader.GetFileIcon(extension, true);
                    Bitmap bmp = FileIcon.ToBitmap();
                    MemoryStream ms = new MemoryStream();
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    ms.Seek(0, SeekOrigin.Begin);

                    Image image = Image.FromStream(ms);
                    e.Item.Image = image;
                    e.Item.TextImageRelation = TextImageRelation.ImageBeforeText;
                    e.ListViewElement.AllowArbitraryItemHeight = true;
                }

                //Set Checkbox
                bool bTmp = (bool)row["IsForSPLMessage"];
                if (bTmp)
                {
                    e.Item.CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
                }
                else
                {
                    e.Item.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
                }
                // Test MR
                //e.Item.Enabled = (!this.Lager.Artikel.Ausgang.Checked);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }
        ///<summary>ctrEinlagerung / lvArtImages_ItemCheckedChanged </summary>
        ///<remarks></remarks>
        private void lvArtImages_ItemCheckedChanged(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (this.lvArtImages.CheckedItems.Count - 1 >= this._ctrMenu._frmMain.system.VE_MaxImageCountSPLMessage)
                {
                    e.Item.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
                    string strERROR = "Die max. Anzahl der Bilder für die Sperrlagermeldung ist erreicht!. Das Bild kann nicht für die Sperrlagermeldung aktiviert werden.";
                    clsMessages.Allgemein_ERRORTextShow(strERROR);
                }
                else
                {
                    bool bSelect = false;
                    if (e.Item.CheckState == Telerik.WinControls.Enumerations.ToggleState.On)
                    {
                        bSelect = true;
                    }

                    //Eintrag in DB
                    string strTmp = e.Item.Value.ToString();
                    int iImageId = 0;
                    if (int.TryParse(strTmp, out iImageId))
                    {
                        this.Lager.ImageVD = new ImageViewData(GL_User, iImageId);
                        this.Lager.ImageVD.UpdateIsForSPLMessage(bSelect);
                    }

                    //decimal decTmp = 0;
                    //Decimal.TryParse(strTmp, out decTmp);
                    //if (decTmp > 0)
                    //{                     
                    //    this.Lager.DocScan.ID = decTmp;
                    //    //bool bSelect = false;
                    //    //if (e.Item.CheckState == Telerik.WinControls.Enumerations.ToggleState.On)
                    //    //{
                    //    //    bSelect = true;
                    //    //}
                    //    this.Lager.DocScan.UpdateIsForSPLMessage(bSelect);
                    //}
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }
        ///<summary>ctrEinlagerung / tsbtnImageAdd_Click</summary>
        ///<remarks></remarks>
        private void tsbtnImageAdd_Click(object sender, EventArgs e)
        {
            // Set filter options and filter index.
            openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Wählen Sie die Bilddateien.";

            openFileDialog.Filter = "Alle Bilddateien | *.jpg;*.png;*.jpeg;*.bmp;*.gif";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                List<string> listFiles = openFileDialog.FileNames.ToList();
                if (listFiles.Count > 0)
                {
                    for (Int32 i = 0; i <= listFiles.Count - 1; i++)
                    {
                        //Bilder werden eingelesen und als image und thumbnail erstellt
                        Image tmpImg = helper_Image.ReadImageByStringPath(listFiles[i], false);
                        if (tmpImg != null)
                        {
                            //ImageViewData viewData = new ImageViewData(this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.GL_User, this._ctrMenu._frmMain.system);
                            ImageViewData viewData = new ImageViewData(this._ctrMenu._frmMain.GL_User);

                            viewData.Image.DocImage = helper_Image.ImageToByteArray(tmpImg);
                            viewData.Image.TableId = (int)this.Lager.Artikel.ID;
                            viewData.Image.TableName = enumDatabaseSped4_TableNames.Artikel.ToString();
                            viewData.Image.ScanFilename = Path.GetFileName(listFiles[i]);
                            viewData.Image.ImageArt = enumDokumentenArt.Bilder.ToString();
                            viewData.Image.WorkspaceId = (int)this.Lager.Artikel.AbBereichID;

                            Image thumbTmp = helper_Image.Generate75x75Pixel(helper_Image.ByteArrayToBitmap(viewData.Image.DocImage));
                            viewData.Image.Thumbnail = helper_Image.ImageToByteArray(thumbTmp);
                            viewData.Image.IsForSPLMessage = false;

                            viewData.Add();
                        }

                    }
                }
            }
            InitLVArtImages();
        }
        ///<summary>ctrEinlagerung / tsbtnImageRefresh_Click</summary>
        ///<remarks></remarks>
        private void tsbtnImageRefresh_Click(object sender, EventArgs e)
        {
            InitLVArtImages();
        }
        ///<summary>ctrEinlagerung / toolStripButton2_Click</summary>
        ///<remarks></remarks>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ListViewDataItem item = (ListViewDataItem)this.lvArtImages.SelectedItem;
            if (item != null)
            {
                try
                {
                    clsImages img = new clsImages();
                    img.byteArrayIn = (byte[])item["DocImage"];
                    img.ImageIn = img.ConvertByteArrayToImage();
                    //img.ImageIn.Save(this.GL_System.sys_WorkingPathExport + "\\TestOrg.jpg");
                    img.ResizeImage((decimal)img.ImageIn.Width, (decimal)img.ImageIn.Height, true);
                    img.ResizeImage((decimal)img.ImageIn.Width, (decimal)img.ImageIn.Height, false);
                    //img.WriteImageToHDD(this.GL_System.sys_WorkingPathExport + "\\Test.jpg", this.GL_System.sys_WorkingPathExport + "\\Test_th.jpg");

                    img.returnImage.Save(this.GL_System.sys_WorkingPathExport + "\\Test.jpg");
                    img.ThumbImage.Save(this.GL_System.sys_WorkingPathExport + "\\Test_th.jpg");

                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                }
            }
        }
        ///<summary>ctrEinlagerung / tsbtnImageDelete_Click</summary>
        ///<remarks></remarks>
        private void tsbtnImageDelete_Click(object sender, EventArgs e)
        {
            if (clsMessages.DeleteAllgemein())
            {
                if (this.Lager.ImageVD.Image.Id > 0)
                {
                    this.Lager.ImageVD.Delete();
                    this.InitLVArtImages();
                }

                //if (this.Lager.DocScan.ID > 0)
                //{
                //    this.Lager.DocScan.Delete();
                //    this.InitLVArtImages();
                //}
            }
        }
        ///<summary>ctrEinlagerung / tbArtikelID_EnabledChanged</summary>
        ///<remarks></remarks>
        private void tbArtikelID_EnabledChanged(object sender, EventArgs e)
        {
            clsClient.ctrEinlagerung_TextBox_SetEnabledBackcolor(this._ctrMenu._frmMain.system.Client.MatchCode, ref tbArtikelID);
        }
        ///<summary>ctrEinlagerung / tbLVSNr_EnabledChanged</summary>
        ///<remarks></remarks>
        private void tbLVSNr_EnabledChanged(object sender, EventArgs e)
        {
            clsClient.ctrEinlagerung_TextBox_SetEnabledBackcolor(this._ctrMenu._frmMain.system.Client.MatchCode, ref tbLVSNr);
        }
        ///<summary>ctrEinlagerung / tbArtIDRef_EnabledChanged</summary>
        ///<remarks></remarks>
        private void tbArtIDRef_EnabledChanged(object sender, EventArgs e)
        {
            clsClient.ctrEinlagerung_TextBox_SetEnabledBackcolor(this._ctrMenu._frmMain.system.Client.MatchCode, ref tbArtIDRef);
        }
        ///<summary>ctrEinlagerung / tbWerk_EnabledChanged</summary>
        ///<remarks></remarks>
        private void tbWerk_EnabledChanged(object sender, EventArgs e)
        {
            clsClient.ctrEinlagerung_TextBox_SetEnabledBackcolor(this._ctrMenu._frmMain.system.Client.MatchCode, ref tbWerk);
        }
        ///<summary>ctrEinlagerung / tbHalle_EnabledChanged</summary>
        ///<remarks></remarks>
        private void tbHalle_EnabledChanged(object sender, EventArgs e)
        {
            clsClient.ctrEinlagerung_TextBox_SetEnabledBackcolor(this._ctrMenu._frmMain.system.Client.MatchCode, ref tbHalle);
        }
        ///<summary>ctrEinlagerung / tbReihe_EnabledChanged</summary>
        ///<remarks></remarks>
        private void tbReihe_EnabledChanged(object sender, EventArgs e)
        {
            clsClient.ctrEinlagerung_TextBox_SetEnabledBackcolor(this._ctrMenu._frmMain.system.Client.MatchCode, ref tbReihe);
        }
        ///<summary>ctrEinlagerung / tbEbene_EnabledChanged</summary>
        ///<remarks></remarks>
        private void tbEbene_EnabledChanged(object sender, EventArgs e)
        {
            clsClient.ctrEinlagerung_TextBox_SetEnabledBackcolor(this._ctrMenu._frmMain.system.Client.MatchCode, ref tbEbene);
        }
        ///<summary>ctrEinlagerung / tbPlatz_EnabledChanged</summary>
        ///<remarks></remarks>
        private void tbPlatz_EnabledChanged(object sender, EventArgs e)
        {
            clsClient.ctrEinlagerung_TextBox_SetEnabledBackcolor(this._ctrMenu._frmMain.system.Client.MatchCode, ref tbPlatz);
        }
        ///<summary>ctrEinlagerung / tbPlatz_EnabledChanged</summary>
        ///<remarks></remarks>
        private void tbInfoAusgang_EnabledChanged(object sender, EventArgs e)
        {
            clsClient.ctrEinlagerung_TextBox_SetEnabledBackcolor(this._ctrMenu._frmMain.system.Client.MatchCode, ref tbInfoAusgang);
        }

        ///<summary>ctrEinlagerung / tsbtnArtikelRL_Click</summary>
        ///<remarks></remarks>
        private void tsbtnArtikelRL_Click(object sender, EventArgs e)
        {
            if (this.Lager.Artikel.ID > 0)
            {
                if (clsMessages.RL_DoRL())
                {
                    //SPL ausbuchen
                    this.Lager.sys = this._ctrMenu._frmMain.system;
                    this.Lager.SPL = new clsSPL();
                    this.Lager.SPL._GL_User = this.GL_User;

                    if (this.Lager.DoRL())
                    {
                        //Rücklieferung durchgeführt
                        // --- Lagermeldungen erstellen
                        InitASNTransfer(clsASNAction.const_ASNAction_RücklieferungSL);
                        EingangBrowse(0, this.Lager.Artikel.ID, enumBrowseAcivity.ArtInItem);
                    }
                }
            }
        }
        /*******************************************************************************************************
         *                                      tabPageCall
         * ****************************************************************************************************/
        ///<summary>ctrEinlagerung / tsbtnManCall_Click</summary>
        ///<remarks></remarks>
        private void tsbtnManCall_Click(object sender, EventArgs e)
        {
            this.tabArtikel.SelectedTab = this.tabPageManCall;
        }
        ///<summary>ctrEinlagerung / InitTabPageCall</summary>
        ///<remarks></remarks>
        private void InitTabPageCall()
        {
            if (this.Lager.Artikel.Call is clsASNCall)
            {
                //Button 
                SetArtikelCallMenuButtonEnabled();
                //Eingabefedler leeren
                ClearCallInputFields();

                switch (this.Lager.Artikel.Call.Status)
                {
                    case clsASNCall.const_Status_NotExist:
                        SetEingabeFelderEnabled((this.Lager.Artikel.LAusgangTableID == 0));
                        this.tsbtnCallSave.Enabled = (this.Lager.Eingang.Checked) && (this.Lager.Artikel.LAusgangTableID == 0);
                        this.tsbtnCallActivate.Enabled = false;
                        this.tsbtnCallDelete.Enabled = false;

                        SearchButton = 14;
                        SetKDRecAfterADRSearch((decimal)this.Lager.Eingang.Empfaenger, false);
                        SearchButton = 15;
                        SetKDRecAfterADRSearch((decimal)this.Lager.Eingang.SpedID, false);
                        SearchButton = 16;
                        SetKDRecAfterADRSearch((decimal)this.Lager.Eingang.EntladeID, false);

                        bUpdateCall = false;
                        break;
                    case clsASNCall.const_Status_erstellt:
                        SetEingabeFelderEnabled(true);
                        //SaveButton freigeben
                        //this.tsbtnCallSave.Enabled = true;
                        this.tsbtnCallSave.Enabled = (this.Lager.Eingang.Checked) && (this.Lager.Artikel.LAusgangTableID == 0);
                        this.tsbtnCallActivate.Enabled = (!this.tsbtnCallSave.Enabled);
                        this.tsbtnCallDelete.Enabled = true;
                        SetCallValueToInputFields();
                        bUpdateCall = (this.Lager.Artikel.Call.ID > 0);
                        break;
                    case clsASNCall.const_Status_bearbeitet:
                        SetEingabeFelderEnabled(false);
                        SetCallValueToInputFields();
                        this.tsbtnCallSave.Enabled = false;
                        this.tsbtnCallActivate.Enabled = (!this.tsbtnCallSave.Enabled);
                        this.tsbtnCallDelete.Enabled = false;
                        bUpdateCall = true;
                        break;
                    case clsASNCall.const_Status_MAT:
                    case clsASNCall.const_Status_ENTL:
                        SetEingabeFelderEnabled(false);
                        SetCallValueToInputFields();
                        this.tsbtnCallSave.Enabled = false;
                        this.tsbtnCallActivate.Enabled = (!this.tsbtnCallSave.Enabled);
                        this.tsbtnCallDelete.Enabled = false;
                        bUpdateCall = true;
                        break;

                    case clsASNCall.const_Status_deactivated:
                        SetEingabeFelderEnabled(false);
                        SetCallValueToInputFields();
                        this.tsbtnCallSave.Enabled = false; // (this.Lager.Eingang.Checked) && (this.Lager.Artikel.LAusgangTableID == 0);
                        this.tsbtnCallActivate.Enabled = (!this.tsbtnCallSave.Enabled);
                        this.tsbtnCallDelete.Enabled = true;
                        bUpdateCall = true;
                        break;
                    default:
                        this.tsbtnCallSave.Enabled = (this.Lager.Eingang.Checked) && (this.Lager.Artikel.LAusgangTableID == 0);
                        this.tsbtnCallActivate.Enabled = (!this.tsbtnCallSave.Enabled);
                        this.tsbtnCallDelete.Enabled = false;
                        break;
                }
            }
            SetPictureBoxCallImage();
        }
        /// <summary>
        ///             Setzt die Daten vom Abruf in die Eingabefelder man.Abruf
        /// </summary>
        private void SetCallValueToInputFields()
        {
            lAbrufId.Text = const_LabelCallId_Text + " " + this.Lager.Artikel.Call.ID.ToString();
            tbCallAbladestelle.Text = this.Lager.Artikel.Call.Abladestelle;
            tbCallReferenz.Text = this.Lager.Artikel.Call.Referenz;
            tbCallStatus.Text = this.Lager.Artikel.Call.Status;

            //Set DefaultEmpfänger AdrID
            SearchButton = 14;
            SetKDRecAfterADRSearch((decimal)this.Lager.Artikel.Call.EmpAdrID, false);
            SearchButton = 15;
            SetKDRecAfterADRSearch((decimal)this.Lager.Artikel.Call.SpedAdrID, false);
            SearchButton = 16;
            SetKDRecAfterADRSearch((decimal)this.Lager.Artikel.Call.LiefAdrID, false);

            dtpCallEintreffDatum.Value = this.Lager.Artikel.Call.EintreffDatum;
            tpCallEintreffZeit.Value = this.Lager.Artikel.Call.EintreffZeit;
        }
        ///<summary>ctrEinlagerung / SetMenuButtonEnabled</summary>
        ///<remarks></remarks>
        private void SetArtikelCallMenuButtonEnabled()
        {
            this.tsbtnCallSave.Enabled = (this.Lager.Eingang.Checked); // && (!(this.Lager.Artikel.Call.ID>0));
            this.tsbtnCallDelete.Enabled = ((this.Lager.Artikel.Call.ID > 0) && (!this.Lager.Artikel.Call.IsRead));
            this.tsbtnCallRefresh.Enabled = true;
        }
        ///<summary>ctrEinlagerung / SetEingabeFelderEnabledAndValue</summary>
        ///<remarks></remarks>
        private void SetEingabeFelderEnabled(bool bEnabled)
        {
            tbCallAbladestelle.Enabled = bEnabled;
            tbCallReferenz.Enabled = bEnabled;
            tbCallStatus.Enabled = true;
            dtpCallEintreffDatum.Enabled = bEnabled;
            tpCallEintreffZeit.Enabled = bEnabled;
            tsbtnCallDelete.Enabled = bEnabled;
            tbnCallEmpfaenger.Enabled = bEnabled;
            tbCallSearchAdr.Enabled = bEnabled;
            tbCallEmpfaenger.Enabled = false;
        }
        ///<summary>ctrEinlagerung / ClearCallInputFields</summary>
        ///<remarks></remarks>
        private void ClearCallInputFields()
        {
            //Value
            lAbrufId.Text = const_LabelCallId_Text + "0";
            string strAbladeStelle = string.Empty;
            string strReferenz = string.Empty;
            //Eintreffzeit / Eintreffdatum
            DateTime MinDate = DateTime.Now.Date;
            DateTime dtEintreffDatum = DateTime.Now.Date;
            //DateTime TimeEintreffZeit = Convert.ToDateTime(clsSystem.const_DefaultDateTimeValue_Min.Date.ToString() + " " + DateTime.Now.AddHours(1).Hour.ToString() + ":" + DateTime.Now.Minute.ToString());
            Int32 iHour = (Int32)DateTime.Now.Hour + 2;
            DateTime TimeEintreffZeit = clsSystem.const_DefaultDateTimeValue_Min.Date.AddHours(iHour); //.Date.AddMinutes(dbMinutes);
            bool bEnabled = false;
            switch (this.Lager.Artikel.Call.Status)
            {
                case clsASNCall.const_Status_NotExist:
                    bEnabled = true;
                    break;
                case clsASNCall.const_Status_MAT:
                case clsASNCall.const_Status_ENTL:
                    MinDate = this.Lager.Artikel.Call.EintreffDatum;
                    dtEintreffDatum = this.Lager.Artikel.Call.EintreffDatum;
                    TimeEintreffZeit = this.Lager.Artikel.Call.EintreffZeit;
                    bEnabled = false;
                    strAbladeStelle = this.Lager.Artikel.Call.Abladestelle;
                    strReferenz = this.Lager.Artikel.Call.Referenz;
                    break;

                case clsASNCall.const_Status_bearbeitet:
                case clsASNCall.const_Status_erstellt:
                case clsASNCall.const_Status_deactivated:
                    if (this.Lager.Artikel.Call.EintreffDatum.Date < DateTime.Now.Date)
                    {
                        MinDate = this.Lager.Artikel.Call.EintreffDatum;
                    }
                    else
                    {
                        MinDate = DateTime.Now.Date;
                    }
                    dtEintreffDatum = this.Lager.Artikel.Call.EintreffDatum;
                    TimeEintreffZeit = this.Lager.Artikel.Call.EintreffZeit;
                    bEnabled = true;
                    strAbladeStelle = this.Lager.Artikel.Call.Abladestelle;
                    strReferenz = this.Lager.Artikel.Call.Referenz;
                    break;
            }

            dtpCallEintreffDatum.MinDate = MinDate;
            dtpCallEintreffDatum.Value = dtEintreffDatum.Date;
            tpCallEintreffZeit.Value = TimeEintreffZeit;

            tbCallAbladestelle.Text = strAbladeStelle;
            tbCallReferenz.Text = strReferenz;
            tbCallStatus.Text = Lager.Artikel.Call.Status;

            if (this.Lager.Artikel.Call.ID < 1)
            {
                clsClient.ctrEinlagerung_CustomizeEingangDefaultAbrufReceiver(ref this._ctrMenu._frmMain.system, ref this.Lager);

                this.Lager.Artikel.Call.SpedAdrID = (int)this.Lager.Eingang.SpedID;
                this.Lager.Artikel.Call.LiefAdrID = (int)this.Lager.Eingang.EntladeID;
            }
            if (this.Lager.Artikel.Call.EmpAdrID > 0)
            {
                //Set DefaultEmpfänger AdrID
                SearchButton = 14;
                SetKDRecAfterADRSearch((Int32)this.Lager.Artikel.Call.EmpAdrID, false);
            }
            if (this.Lager.Artikel.Call.SpedAdrID > 0)
            {
                //Set DefaultSpediteur AdrID
                SearchButton = 15;
                SetKDRecAfterADRSearch((Int32)this.Lager.Artikel.Call.SpedAdrID, false);
            }
            if (this.Lager.Artikel.Call.LiefAdrID > 0)
            {
                //Set DefaultEntladesteller AdrID
                SearchButton = 16;
                SetKDRecAfterADRSearch((Int32)this.Lager.Artikel.Call.LiefAdrID, false);
            }
        }
        ///<summary>ctrEinlagerung / AddCall</summary>
        ///<remarks></remarks>
        private void AddCall()
        {
            this.Lager.Artikel.Call.UserID = (Int32)this.GL_User.User_ID;
            this.Lager.Artikel.Call.ArtikelID = (Int32)this.Lager.Artikel.ID;
            this.Lager.Artikel.Call.Abladestelle = this.tbCallAbladestelle.Text.Trim();
            this.Lager.Artikel.Call.Referenz = this.tbCallReferenz.Text.Trim();
            this.Lager.Artikel.Call.LVSNr = (Int32)this.Lager.Artikel.LVS_ID;

            this.Lager.Artikel.Call.Datum = DateTime.Now;
            this.Lager.Artikel.Call.EintreffDatum = (DateTime)this.dtpCallEintreffDatum.Value.Date;
            this.Lager.Artikel.Call.EintreffZeit = Convert.ToDateTime(((DateTime)Globals.DefaultDateTimeMinValue).ToShortDateString() + " " + ((DateTime)this.tpCallEintreffZeit.Value).ToShortTimeString());

            string strCallTxt = "- Datum / Zeit: [" + this.Lager.Artikel.Call.EintreffDatum.ToShortDateString() + " / " + Functions.FormatShortTime(this.Lager.Artikel.Call.EintreffZeit) + "]";

            this.Lager.Artikel.Call.Aktion = clsASNCall.const_AbrufAktion_Abruf;

            //this.Lager.Artikel.Call.LiefAdrID =(int) this.Lager.Artikel.Eingang.EntladeID;
            //this.Lager.Artikel.Call.EmpAdrID = (int)this.Lager.Artikel.Eingang.Empfaenger;
            //this.Lager.Artikel.Call.SpedAdrID = (int)this.Lager.Artikel.Eingang.SpedID;

            this.Lager.Artikel.Call.Status = clsASNCall.const_Status_erstellt;
            this.Lager.Artikel.Call.BenutzerID = this._ctrMenu._frmMain.GL_User.User_ID;
            this.Lager.Artikel.Call.Benutzername = this._ctrMenu._frmMain.GL_User.Name;
            this.Lager.Artikel.Call.IsRead = false;
            this.Lager.Artikel.Call.IsCreated = true;
            this.Lager.Artikel.Call.AbBereichID = (Int32)this._ctrMenu._frmMain.system.AbBereich.ID;
            clsClient.ctrEinlagerung_CustomizeDefaultCallCompany(ref this._ctrMenu._frmMain.system, ref this.Lager);
            this.Lager.Artikel.Call.CompanyID = this.Lager.Artikel.Call.Company.ID;
            this.Lager.Artikel.Call.CompanyName = this.Lager.Artikel.Call.Company.Fullname;
            //ARtikeldaten
            this.Lager.Artikel.Call.Werksnummer = this.Lager.Artikel.Werksnummer;
            this.Lager.Artikel.Call.Produktionsnummer = this.Lager.Artikel.Produktionsnummer;
            this.Lager.Artikel.Call.Charge = this.Lager.Artikel.Charge;
            this.Lager.Artikel.Call.Brutto = this.Lager.Artikel.Brutto;


            if ((this.Lager.Artikel.Call.ID > 0) && (bUpdateCall))
            {
                if (this.Lager.Artikel.Call.Update())
                {
                    clsArtikelVita.Call_ChangeCall(this.GL_User, this.Lager.Artikel.ID, this.Lager.Artikel.Call.ID, strCallTxt);
                }
                this.Lager.Artikel.Call.Fill();
            }
            else
            {
                //this.Lager.Artikel.Call.Status = clsASNCall.const_Status_erstellt;
                //this.Lager.Artikel.Call.Benutzername = this._ctrMenu._frmMain.GL_User.Name;
                ////this.Lager.Artikel.Call.Schicht = string.Empty; // nicht setzen nur VW
                //this.Lager.Artikel.Call.IsRead = false;
                //this.Lager.Artikel.Call.IsCreated = true;
                //this.Lager.Artikel.Call.AbBereichID =(Int32) this._ctrMenu._frmMain.system.AbBereich.ID;
                //clsClient.ctrEinlagerung_CustomizeDefaultCallCompany(ref this._ctrMenu._frmMain.system, ref this.Lager);
                //this.Lager.Artikel.Call.CompanyID=this.Lager.Artikel.Call.Company.ID;
                //this.Lager.Artikel.Call.CompanyName = this.Lager.Artikel.Call.Company.Fullname;
                ////ARtikeldaten
                //this.Lager.Artikel.Call.Werksnummer=this.Lager.Artikel.Werksnummer;
                //this.Lager.Artikel.Call.Produktionsnummer=this.Lager.Artikel.Produktionsnummer;
                //this.Lager.Artikel.Call.Charge=this.Lager.Artikel.Charge;
                //this.Lager.Artikel.Call.Brutto=this.Lager.Artikel.Brutto;


                bool bInsertOK = this.Lager.Artikel.Call.Add();
                this.Lager.Artikel.Call.FillbyArtikelID();
                if (bInsertOK)
                {
                    clsArtikelVita.Call_AddCall(this.GL_User, this.Lager.Artikel.ID, this.Lager.Artikel.Call.ID, strCallTxt);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbnCallEmpfaenger_Click(object sender, EventArgs e)
        {
            SearchButton = 14;
            _ctrMenu.OpenADRSearch(this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCallLiefAdr_Click(object sender, EventArgs e)
        {
            SearchButton = 16;
            _ctrMenu.OpenADRSearch(this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCallSped_Click(object sender, EventArgs e)
        {
            SearchButton = 15;
            _ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrEinlagerung / tsbtnCreateCall_Click</summary>
        ///<remarks></remarks>
        private void tsbtnCreateCall_Click(object sender, EventArgs e)
        {
            if (this.Lager.Artikel.Call.ID == 0)
            {
                InitCall();
            }
        }
        ///<summary>ctrEinlagerung / InitCall</summary>
        ///<remarks></remarks>
        private void InitCall()
        {
            ClearCallInputFields();
            SetEingabeFelderEnabled(true);
        }
        ///<summary>ctrEinlagerung / tsbtnCallSave_Click</summary>
        ///<remarks></remarks>
        private void tsbtnCallSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Lager.Artikel.Call is clsASNCall) // && (this.Lager.Artikel.Call.ID < 1))
                {
                    bUpdateCall = (this.Lager.Artikel.Call.ID > 0);
                    if (clsMessages.Call_CreateCall())
                    {
                        AddCall();
                    }
                }
            }
            catch (Exception ex)
            {
                clsMessages.Allgemein_ERRORTextShow(ex.ToString());
            }
            InitTabPageCall();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbtnCallActivate_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.Lager.Artikel.Call is clsASNCall) && (this.Lager.Artikel.Call.ID > 0))
                {
                    if (this.Lager.Artikel.Call.Status.Equals(clsASNCall.const_Status_deactivated))
                    {
                        if (clsMessages.Call_Activate())
                        {
                            bUpdateCall = true;
                            AddCall();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsMessages.Allgemein_ERRORTextShow(ex.ToString());
            }
            InitTabPageCall();
        }
        ///<summary>ctrEinlagerung / tsbtnCallRefresh_Click</summary>
        ///<remarks></remarks>
        private void tsbtnCallRefresh_Click(object sender, EventArgs e)
        {
            EingangBrowse(0, this.Lager.Artikel.ID, enumBrowseAcivity.ArtInItem);
            this.tabArtikel.SelectedTab = this.tabPageManCall;
            InitSelectedTabPage(this.tabArtikel.SelectedTab.ToString());
            //InitTabPageCall();
        }
        ///<summary>ctrEinlagerung / tsbtnCallDelete_Click</summary>
        ///<remarks></remarks>
        private void tsbtnCallDelete_Click(object sender, EventArgs e)
        {
            if (this.Lager.Artikel.Call is clsASNCall)
            {
                if (!this.Lager.Artikel.Call.IsRead)
                {
                    if (clsMessages.Call_DeleteCall())
                    {
                        if (this.Lager.Artikel.Call.Delete())
                        {
                            string strCallTxt = "- Datum / Zeit: [" + this.Lager.Artikel.Call.EintreffDatum.ToShortDateString() + " / " + Functions.FormatShortTime(this.Lager.Artikel.Call.EintreffZeit) + "]";

                            clsArtikelVita.Call_DeleteCall(this.GL_User, this.Lager.Artikel.ID, this.Lager.Artikel.Call.ID, strCallTxt);
                        }
                        InitTabPageCall();
                    }
                }
            }
        }
        ///<summary>ctrEinlagerung / tsbtnCreateRetoureEingang_Click</summary>
        ///<remarks></remarks>
        private void tsbtnCreateRetoureEingang_Click(object sender, EventArgs e)
        {
            ctrRetoure retEA = new ctrRetoure();
            retEA.ctrEinlagerung = this;
            this._ctrMenu.OpenFrmTMP(retEA);
        }
        ///<summary>ctrEinlagerung / tbInfoIntern_EnabledChanged</summary>
        ///<remarks></remarks>
        private void tbInfoIntern_EnabledChanged(object sender, EventArgs e)
        {
            this._ctrMenu._frmMain.system.Client.ctrEinlagerung_CustomizedSettbInfoInternEnabled(ref this.tbInfoIntern);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnDirectTransformation_Click(object sender, EventArgs e)
        {
            if (_MandantenID > 0)
            {
                if (this.Lager.Eingang.DirectDeliveryTransformation())
                {
                    DirectDeliveryConfirmation();
                    if (this.dgv.Rows.Count > 0)
                    {
                        this.dgv.Rows[0].IsSelected = true;
                        SetSelectedRowInDGV();
                    }
                    _bDirectDelivery = true;
                    SetLabelDirektanlieferung(true);
                }
            }
            else
            {
                clsMessages.Allgemein_MandantFehlt();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnSchadenDoc_Click(object sender, EventArgs e)
        {
            if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
            {
                if (this._ctrMenu._frmMain.system.Client.Modul.Lager_SPL_PrintSPLDocument)
                {
                    if (this.Lager.Artikel.ID > 0)
                    {
                        PrintSPLDoc(sender, e);
                    }
                }
                else
                {
                    clsMessages.Allgemein_ModulNotInstalled();
                }
            }
            else
            {
                //DoPrintLabelOrDoc(sender, e);
                if (this._ctrMenu._frmMain.system.Client.Modul.Lager_SPL_PrintSPLDocument)
                {
                    if (this._ctrMenu._frmMain.system.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_SZG + "_"))
                    {
                        this.tsbtnSchadenDoc.Tag = enumDokumentenArt.SchadenDoc;
                        this.PrintAktion(sender, e);
                    }
                    else
                    {
                        if (this.Lager.Artikel.ID > 0)
                        {
                            PrintSPLDoc(sender, e);
                        }
                    }
                }
                else
                {
                    clsMessages.Allgemein_ModulNotInstalled();
                }
            }
        }
        ///<summary>ctrEinlagerung / PrintSPLDoc</summary>
        ///<remarks></remarks>
        private void PrintSPLDoc(object sender, EventArgs e)
        {
            eDokumentenArt = enumDokumentenArt.SPLDoc;
            DoPrintLabelOrDoc(sender, e);
        }
        /// <summary>
        ///                noch im Test bzw. noch nicht eingestellt 
        ///                es soll eine Druckfunktion für alles erstelle werden wegen der übersicht
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoPrintLabelOrDoc(object sender, EventArgs e)
        {
            switch (eDokumentenArt)
            {
                case enumDokumentenArt.SPLDoc:
                case enumDokumentenArt.SPLLabel:
                case enumDokumentenArt.SchadenDoc:
                case enumDokumentenArt.SchadenLabel:
                    if (this.Lager.Artikel.ID > 0)
                    {
                        ctrSPLAdd tmpSPL = new ctrSPLAdd(this);
                        this.Lager.SPL.ArtikelID = this.Lager.Artikel.ID;
                        this.Lager.SPL.FillLastINByArtikelID(false);
                        //Druck SPL Dokumente einfügen
                        if (this._ctrMenu._frmMain.system.Client.Modul.Lager_SPL_PrintSPLDocument)
                        {
                            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_SPL_AutoPrintSPLDocument)
                            {
                                ctrPrintLager TmpPrint = new ctrPrintLager();
                                TmpPrint.Hide();
                                TmpPrint._ctrMenu = this._ctrMenu;
                                TmpPrint._ctrEinlagerung = this;
                                if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
                                {
                                    TmpPrint._DokumentenArt = this.eDokumentenArt.ToString();
                                    TmpPrint.nudPrintCount.Value = this._ctrMenu._frmMain.GL_System.docPath_SPLDoc_Count;
                                    TmpPrint.SetLagerDatenToFrm();
                                    TmpPrint.StartPrintSPLDoc(true);
                                    TmpPrint.Dispose();
                                }
                                else
                                {
                                    this._ctrMenu._frmMain.system.ReportDocSetting.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, this.Lager.Eingang.Auftraggeber, this._ctrMenu._frmMain.system.AbBereich.ID);
                                    TmpPrint.RepDocSettings = null; ;
                                    TmpPrint.RepDocSettings = this._ctrMenu._frmMain.system.ReportDocSetting.ListReportDocEingang.Find(x => x.DocKey.Equals(this.eDokumentenArt.ToString()));
                                    PrintByReportDocSetting(TmpPrint);
                                }
                            }
                            else
                            {
                                switch (eDokumentenArt)
                                {
                                    case enumDokumentenArt.SPLDoc:
                                        tmpSPL.tsbtnPrintSPLLabel.Visible = false;
                                        tmpSPL.tsbtnPrintSPLDoc.Visible = true;
                                        this._ctrMenu.OpenFrmTMP(tmpSPL);
                                        break;

                                    case enumDokumentenArt.SPLLabel:
                                        tmpSPL.tsbtnPrintSPLLabel.Visible = true;
                                        tmpSPL.tsbtnPrintSPLDoc.Visible = false;
                                        this._ctrMenu.OpenFrmTMP(tmpSPL);
                                        break;

                                    case enumDokumentenArt.SchadenDoc:
                                    case enumDokumentenArt.SchadenLabel:
                                        ctrPrintLager TmpPrint = new ctrPrintLager();
                                        if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
                                        {
                                            TmpPrint.Hide();
                                            TmpPrint._ctrMenu = this._ctrMenu;
                                            TmpPrint._ctrEinlagerung = this;
                                            TmpPrint._DokumentenArt = this.eDokumentenArt.ToString();
                                            TmpPrint.nudPrintCount.Value = this._ctrMenu._frmMain.GL_System.docPath_SPLDoc_Count;
                                            TmpPrint.SetLagerDatenToFrm();
                                            TmpPrint.StartPrintDoc(true);
                                            TmpPrint.Dispose();
                                        }
                                        else
                                        {
                                            this._ctrMenu._frmMain.system.ReportDocSetting.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, this.Lager.Eingang.Auftraggeber, this._ctrMenu._frmMain.system.AbBereich.ID);
                                            TmpPrint.RepDocSettings = null;
                                            TmpPrint.RepDocSettings = this._ctrMenu._frmMain.system.ReportDocSetting.ListReportDocEingang.Find(x => x.DocKey.Equals(this.eDokumentenArt.ToString()));
                                            PrintByReportDocSetting(TmpPrint);
                                        }
                                        break;
                                }
                            }
                        }
                    }
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnSchadensDocMail_Click(object sender, EventArgs e)
        {
            string strPDFName = string.Empty;
            this._ctrMenu._ctrSPLAdd = new ctrSPLAdd(this);
            this._ctrMenu._ctrSPLAdd.ctrMenu = this._ctrMenu;

            bool openExportFile = false;
            string strPDFPath = "";
            DateTime FileDateForMail = DateTime.Now;


            //Zusweisung der Dokumentenart und Path
            //this._ctrMenu._ctrSPLAdd.DokumentenArt = Globals.enumDokumentenart.SPLDoc.ToString();
            //this._ctrMenu._ctrSPLAdd.DocPath = this._ctrMenu._frmMain.GL_System.docPath_SPLDoc;
            this._ctrMenu._ctrSPLAdd.DokumentenArt = enumDokumentenArt.SchadenDoc.ToString();
            strPDFName = ctrSPLAdd.const_SPLAdd_DocNameSchaden;

            if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
            {
                //this._ctrMenu._ctrSPLAdd.DocPath = this._ctrMenu._frmMain.GL_System.docPath_SPLDoc;
                //strPDFName = ctrSPLAdd.const_SPLAdd_DocNameSchaden;
            }
            else
            {
                //strPDFName = ctrSPLAdd.const_SPLAdd_DocNameSPL;
                //---Zwischenlösung mr 20171117
                //if (this._ctrMenu._frmMain.system.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_SZG + "_"))
                //{
                //    this._ctrMenu._ctrSPLAdd.DokumentenArt = Globals.enumDokumentenart.SchadenDoc.ToString();
                //    strPDFName = ctrSPLAdd.const_SPLAdd_DocNameSchaden;
                //}

                this._ctrMenu._frmMain.system.ReportDocSetting.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, this.Lager.Eingang.Auftraggeber, this._ctrMenu._frmMain.system.AbBereich.ID);
                this._ctrMenu._ctrSPLAdd.RepDocSettings = null;
                this._ctrMenu._ctrSPLAdd.RepDocSettings = this._ctrMenu._frmMain.system.ReportDocSetting.ListReportDocEingang.Find(x => x.DocKey.Equals(this._ctrMenu._ctrSPLAdd.DokumentenArt.ToString()));
                if (this._ctrMenu._ctrSPLAdd.RepDocSettings is clsReportDocSetting)
                {
                    this._ctrMenu._ctrSPLAdd.DocPath = this._ctrMenu._ctrSPLAdd.RepDocSettings.DocFileNameAndPath;
                }
                else
                {
                    clsMessages.Print_Fail_ReportAssignment();

                }
            }

            this._ctrMenu._ctrSPLAdd._frmPrintRepViewer = new frmPrintRepViewer();
            this._ctrMenu._ctrSPLAdd._frmPrintRepViewer.GL_System = this._ctrMenu._frmMain.GL_System;
            this._ctrMenu._ctrSPLAdd._frmPrintRepViewer._SPLAdd = this._ctrMenu._ctrSPLAdd;
            this._ctrMenu._ctrSPLAdd._frmPrintRepViewer.iPrintCount = 1;
            this._ctrMenu._ctrSPLAdd._frmPrintRepViewer.DokumentenArt = this._ctrMenu._ctrSPLAdd.DokumentenArt;
            this._ctrMenu._ctrSPLAdd._frmPrintRepViewer.InitFrm();
            string FileName = FileDateForMail.ToString("yyyy_MM_dd_HHmmss") + "_" + strPDFName + ".pdf";
            this._ctrMenu._ctrSPLAdd._frmPrintRepViewer.PrintDirectToPDF(strPDFName, FileName);

            this._ctrMenu._ctrSPLAdd.ListAttachmentPath = new List<string>();
            //Check File exist
            if (File.Exists(FileName))
            {
                this._ctrMenu._ctrSPLAdd.ListAttachmentPath.Add(FileName);
            }
            if (this._ctrMenu._ctrSPLAdd.ListAttachmentPath.Count > 0)
            {
                if (!FileName.Equals(string.Empty))
                {
                    //System.Threading.Thread.Sleep(100);
                    this._ctrMenu.OpenCtrMailCockpitInFrm(this._ctrMenu._ctrSPLAdd);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbtnRefreshEingang_Click(object sender, EventArgs e)
        {
            this.EingangBrowse(this.Lager.LEingangTableID, this.Lager.Artikel.ID, enumBrowseAcivity.ArtItem);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveChangeLO_Click(object sender, EventArgs e)
        {
            SaveChangeLagerOrt();
        }
        /// <summary>
        /// 
        /// </summary>
        private void SaveChangeLagerOrt()
        {
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_LagerOrt_manuell_Changeable)
            {
                string strLOValue = string.Empty;
                if (tbWerk.Enabled)
                {
                    if (!tbWerk.Text.Equals(this.Lager.Artikel.Werk))
                    {
                        this.Lager.Artikel.SetArtValueLagerOrt(clsArtikel.ArtikelField_Werk, tbWerk.Text.Trim(), true);
                    }
                }
                if (tbHalle.Enabled)
                {
                    if (!tbHalle.Text.Equals(this.Lager.Artikel.Halle))
                    {
                        this.Lager.Artikel.SetArtValueLagerOrt(clsArtikel.ArtikelField_Halle, tbHalle.Text.Trim(), true);
                    }
                }
                if (tbReihe.Enabled)
                {
                    if (!tbReihe.Text.Equals(this.Lager.Artikel.Reihe))
                    {
                        this.Lager.Artikel.SetArtValueLagerOrt(clsArtikel.ArtikelField_Reihe, tbReihe.Text.Trim(), true);
                    }
                }
                if (tbEbene.Enabled)
                {
                    if (!tbEbene.Text.Equals(this.Lager.Artikel.Ebene))
                    {
                        this.Lager.Artikel.SetArtValueLagerOrt(clsArtikel.ArtikelField_Ebene, tbEbene.Text.Trim(), true);
                    }
                }
                if (tbPlatz.Enabled)
                {
                    if (!tbPlatz.Text.Equals(this.Lager.Artikel.Platz))
                    {
                        this.Lager.Artikel.SetArtValueLagerOrt(clsArtikel.ArtikelField_Platz, tbPlatz.Text.Trim(), true);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnFreeArtikel_Click(object sender, EventArgs e)
        {
            //SetArtikelMenuBtnEnabled(true);
            //SetArtikelEingabefelderDatenEnable(true);
        }
    }
}
