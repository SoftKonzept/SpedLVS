using Common.Enumerations;
using LVS;
using Sped4.Classes;
using Sped4.Controls;
using Sped4.Controls.AdminCockpit;
using Sped4.Controls.ASNCenter;
using Sped4.Controls.Edifact;
using Sped4.Controls.Processes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Sped4
{
    public partial class ctrMenu : UserControl, INotifyPropertyChanged
    {



        //---------------------------------------------- 
        private const string const_TMP = "Temp";

        //public clsPreloader Preloader;

        public Globals._GL_USER GL_User;

        public ctrAufträge _ctrAuftrag;
        public ctrSUList _ctrSUList;
        public ctrTourDetails _ctrTourDetails;
        public ctrDistance _ctrDistance;
        public ctrArtDetails _ctrArtDetails;
        public ctrLagerOrt _ctrLagerOrt;
        public ctrPrintLagerOrtLabel _ctrPrintLagerOrtLabel;
        public ctrAuslagerung _ctrAuslagerung;
        public ctrEinlagerung _ctrEinlagerung;
        public ctrUmbuchung _ctrUmbuchung;
        public ctrSearch _ctrSearch;
        public ctrJournal _ctrJournal;
        public ctrBestand _ctrBestand;
        public ctrInventory _ctrInventory;
        public ctrInventoryAdd _ctrInventoryAdd;
        public ctrSchaeden _ctrSchaeden;
        public ctrSperrlager _ctrSperrlager;
        public ctrEinheitenListe _ctrEinheitenListe;
        public ctrUserList _ctrUserList;
        public ctrPrintLager _ctrPrintLager;
        public ctrPrinter _ctrPrinter;
        public ctrFaktLager _ctrFaktLager;
        public ctrFaktArtikelList _ctrFaktArtikelList;
        public ctrTarifErfassung _ctrTarifErfassung;
        public ctrStatistik _ctrStatistik;
        public ctrADR_List _ctrAdrList;
        public ctrMailCockpit _ctrMailCockpit;
        public ctrExtraCharge _ctrExtraCharge;
        public ctrExtraChargeAssignment _ctrExtraChargeAssignment;
        public ctrTableOfAccount _ctrTableOfAccount;
        public ctrFreeForCall _ctrFreeForCall;
        public ctrADRManAdd _ctrADRManAdd;
        public ctrASNRead _ctrASNRead;
        public ctrRGList _ctrRGList;
        public ctrPostCenter _ctrPostCenter;
        public ctrArbeitsliste _ctrArbeitsliste;
        public ctrWorklist _ctrWorklist;
        public ctrRGManuell _ctrRGManuell;
        public ctrInfoPanel _ctrInfoPanel;
        public ctrSendToSPL _ctrSendToSPL;
        public ctrLieferEinteilung _ctrLiefereinteilungen;
        public ctrBSInfo4905 _ctrBSInfo4905;
        public ctrWaggonbuch _ctrWaggonbuch;
        public ctrComLog _ctrComLog;
        public ctrSPLAdd _ctrSPLAdd;
        public ctrADRSearch _ctrADRSearch;
        public ctrASNCall _ctrASNCall;
        public ctrReihen _ctrLagerReihen;
        public ctrRetoure _ctrRetoure;
        public ctrVDA4984Details _ctrVDA4984Details;
        public ctrASNMain _ctrASNMain;
        public ctrASNArtFieldAssignment _ctrASNArtFieldAssignment;
        public ctrASNArtFieldAssignSelectToCopy _ctrASNArtFieldAssignSelectToCopy;
        public ctrASNActionSelectToCopy _ctrASNActionSelectToCopy;
        public ctrJobSelectToCopy _ctrJobSelectToCopy;
        public ctrVDAClientWorkspaceValue _ctrVDAClientWorkspaceValue;
        public ctrVDAClientWorkspaceValueSelectToCopy _ctrVDAClientWorkspaceValueSelectToCopy;
        public ctrDelforDeliveryForecast _ctrDelforDeliveryForecast;
        public ctrCreateEdiStruckture _ctrCreateEdiStruckture;
        public ctrCustomProcess _ctrCustomProcess;

        public frmTmp _FrmTmp;
        public frmMAIN _frmMain;
        public frmDispoTourChange _frmDispoTourChange;
        public frmDispoKalender _Kalender;
        internal frmArbeitsbereiche _frmArbeitsbereich;
        public frmAuftragView _frmAuftragView;
        public frmAuftrag_Splitting _frmAuftragSplitting;
        public frmPrintRepViewer _frmPrintRepView;
        public frmWait _frmWait;
        public frmSettingsView _frmSettingsView;
        public frmFahrzeuge _frmFahrzeuge;
        public frmArchiveView _frmArchivView;

        //public frmEinlagerung _frmEinlagerung;


        internal frmLogin _frmLogin;

        public Color UntermenuBackcolor;
        public Color UntermenuForecolor;

        public event PropertyChangedEventHandler PropertyChanged;
        public List<string> LogMessages { get; set; } = new List<string>();

        public ctrMenu()
        {
            InitializeComponent();
        }
        public bool DateChange = false;

        public delegate void Sped4ChangeWorkspaceEventHandler();
        public event Sped4ChangeWorkspaceEventHandler ChangeWorkspace;

        public delegate void Sped4CloseEventHandler();
        public event Sped4CloseEventHandler Sped4Close;

        public delegate void StatusProzessChangeMenuEventHandler();
        public event StatusProzessChangeMenuEventHandler ProzessBarStatusChangeMenu;

        public delegate void DispoDataChangedEventHandler(ctrMenu sender);
        public event DispoDataChangedEventHandler DispoDataChanged;

        public delegate void OpenKalenerFrmEventHandler(DateTime _From, DateTime _To);
        public event OpenKalenerFrmEventHandler OpenKalender;

        public delegate void ThreadCtrInvokeEventHandler();

        public delegate void AuthUserRefreshEventHandler(Globals._GL_USER _GL_User);
        public event AuthUserRefreshEventHandler SetGLUser;

        private DateTime dtVonZP = DateTime.MinValue;
        private DateTime dtBisZP = DateTime.MaxValue;

        // Userberechtigungen
        private bool bLagerEingang;
        private bool bLagerAusgang;
        private bool bLagerBestand;
        private bool bLagerInventur;
        private bool bLagerArchiv;
        private bool bDispositionAuftragsliste;
        private bool bDispositionDispoplan;
        private bool bLagerUserlist;
        private bool bLagerArtSearch;
        private bool bFaktLager;
        private bool bFaktSped;
        private bool bFaktRGBuch;
        private bool bStammADR;
        private bool bStammFahrzeuge;
        private bool bStammGArt;
        private bool bStammPersonal;
        private bool bStammRelation;
        private bool bStammEinheiten;
        private bool bStammSchaden;
        private bool bStammLagerOrt;
        private bool bStammExtraCharge;
        private bool bStammKP; //Kontenplan
        private bool bStatLager;
        private bool bStatFaktLager;
        private bool bStatFaktDisposition;
        private bool bLagerPost;
        private bool bLagerArbeitsliste;

        ///<summary>ctrMenu / enum EStatus</summary>
        ///<remarks></remarks>
        public enum EStatus
        {
            Default = 0,
            Statistik = 1,
            Spedition = 2,
            Fakturierung = 3,
            //Auftrag = 4,
            Stammdaten = 4,
            Lager = 5,
        }
        ///<summary>ctrMenu / Status</summary>
        ///<remarks></remarks>
        private EStatus _Status;
        public EStatus Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
                //SetControls();
                setMenu();
            }
        }
        ///<summary>ctrMenu / SetControls</summary>
        ///<remarks></remarks>
        public void SetDefaultEStatus()
        {
            this.Status = EStatus.Default;
        }
        ///<summary>ctrMenu / SetUserAuthForCtr</summary>
        ///<remarks></remarks>        
        public void SetUserAuthForCtr()
        {
            //Hauptmenü
            btnSpedition.Visible = this._frmMain.system.Client.Modul.MainMenu_AuftragserfassungDispo;
            btnDispo.Visible = this._frmMain.system.Client.Modul.MainMenu_Disposition;

            btnFakturierung.Visible = this._frmMain.system.Client.Modul.MainMenu_Fakurierung;
            btnLager.Visible = this._frmMain.system.Client.Modul.MainMenu_Lager;
            btnStammdaten.Visible = this._frmMain.system.Client.Modul.MainMenu_Stammdaten;
            btnStatistik.Visible = this._frmMain.system.Client.Modul.MainMenu_Statistik;
            btnFakturierung.Visible = this._frmMain.system.Client.Modul.MainMenu_Fakurierung;

            //Menü KOpfzeile in frmMain
            this._frmMain.SetUserAuthForCtr();

            // lager
            bLagerEingang = ((this.GL_User.read_LagerEingang) & (this.GL_User.write_LagerEingang)) & (this._frmMain.system.AbBereich.IsLager);
            bLagerAusgang = ((this.GL_User.read_LagerAusgang) & (this.GL_User.write_LagerAusgang)) & (this._frmMain.system.AbBereich.IsLager);
            bLagerBestand = this.GL_User.read_Bestand;
            //bLagerInventur = this._frmMain.system.Client.Modul.Lager_Inventory_List & (this.GL_User.read_Inventory) & (this.GL_User.write_Inventory);
            bLagerInventur = this._frmMain.system.Client.Modul.LvsScan_Inventory_List & (this.GL_User.read_Inventory) & (this.GL_User.write_Inventory);
            //bLagerArchiv = true;
            bLagerArchiv = this._frmMain.system.Client.Modul.Archiv;

            bLagerArbeitsliste = (bLagerEingang & (this._frmMain.system.AbBereich.IsLager));

            bLagerUserlist = false; // BAUSTELLE
            bLagerArtSearch = bLagerEingang & this._frmMain.system.AbBereich.IsLager; // BAUSTELLE
            bLagerPost = bLagerEingang & this._frmMain.system.AbBereich.IsLager;

            //Disposition
            btnSpedition.Visible = this._frmMain.GL_System.Modul_Spedition;
            //btnDispo.Visible = this._frmMain.GL_System.Modul_Disposition_Dispo;

            bDispositionAuftragsliste = ((this.GL_User.read_Order) & (this.GL_User.write_Order)) & (this._frmMain.system.AbBereich.IsSpedition);
            bDispositionDispoplan = ((this.GL_User.read_Disposition) & (this.GL_User.write_Disposition)) & (this._frmMain.system.AbBereich.IsSpedition);

            // fakturierung
            bFaktLager = ((this.GL_User.read_FaktLager) & (this.GL_User.write_FaktLager) & this._frmMain.system.AbBereich.IsLager); //((this.GL_User.read_FaktLager) & (this.GL_User.write_FaktLager));
            bFaktSped = false; // ((this.GL_User.read_FaktSpedition) & (this.GL_User.write_FaktSpedition));
            bFaktRGBuch = ((this.GL_User.read_FaktLager) & this._frmMain.system.Client.Modul.Fakt_Rechnungsbuch);

            // stammdaten
            bStammADR = ((this.GL_User.read_ADR) & (this.GL_User.write_ADR));
            bStammFahrzeuge = ((this.GL_User.read_KFZ) & (this.GL_User.write_KFZ));
            bStammGArt = ((this.GL_User.read_Gut) & (this.GL_User.write_Gut));
            bStammPersonal = ((this.GL_User.read_Personal) & (this.GL_User.write_Personal));
            bStammRelation = ((this.GL_User.read_Relation) & (this.GL_User.write_Relation));
            bStammEinheiten = ((this.GL_User.read_Einheit) & (this.GL_User.write_Einheit));
            bStammSchaden = ((this.GL_User.read_Schaden) & (this.GL_User.write_Schaden));
            bStammLagerOrt = ((this.GL_User.read_LagerOrt) & (this.GL_User.write_LagerOrt));
            bStammExtraCharge = ((this.GL_User.write_FaktExtraCharge) && (this.GL_User.read_FaktExtraCharge));
            bStammKP = ((this.GL_User.read_FaktLager) & (this.GL_User.write_FaktLager) & this._frmMain.system.AbBereich.IsLager);

            // USER BERECHTIGUNGEN AUF ALTE ANSICHT ÄNDERN :
            //bStatLager = true;
            //bStatFaktLager = false;
            //bStatFaktDisposition = false;

            bStatLager = (this.GL_User.read_Statistik & (this._frmMain.system.AbBereich.IsLager));
            bStatFaktLager = (this.GL_User.read_Statistik & (this._frmMain.system.AbBereich.IsLager));
            bStatFaktDisposition = (this.GL_User.read_Statistik & (this._frmMain.system.AbBereich.IsSpedition));
        }
        ///<summary>ctrMenü / setMenu</summary>
        ///<remarks>Erstellt das jeweilige Menü anhand der Freigabe der entsprechenden Module</remarks>
        private void setMenu()
        {
            // BAUSTELLE AUF BASIS VON DB ???
            ListViewDataItem menuItemLager;
            lvMenu.Items.Clear();

            switch (Status)
            {
                case EStatus.Lager:
                    menuItemLager = new ListViewDataItem();

                    menuItemLager.Image = global::Sped4.Properties.Resources.box_into_24x24;
                    menuItemLager.Text = "Einlagerung";
                    menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                    menuItemLager.Visible = bLagerEingang;
                    lvMenu.Items.Add(menuItemLager);

                    menuItemLager = new ListViewDataItem();
                    menuItemLager.Image = global::Sped4.Properties.Resources.box_out_24x24;
                    menuItemLager.Text = "Auslagerung";
                    menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                    menuItemLager.Visible = bLagerAusgang;
                    lvMenu.Items.Add(menuItemLager);

                    menuItemLager = new ListViewDataItem();
                    menuItemLager.Image = global::Sped4.Properties.Resources.document_out_24x24;
                    menuItemLager.Text = "Umbuchung";
                    menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                    menuItemLager.Visible = (bLagerEingang & bLagerAusgang);// BAUSTELLE
                    lvMenu.Items.Add(menuItemLager);

                    menuItemLager = new ListViewDataItem();
                    menuItemLager.Image = global::Sped4.Properties.Resources.notebook3_24x24;
                    menuItemLager.Text = "Journal Lager";
                    menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                    menuItemLager.Visible = bLagerBestand;
                    lvMenu.Items.Add(menuItemLager);

                    menuItemLager = new ListViewDataItem();
                    menuItemLager.Image = global::Sped4.Properties.Resources.book_blue_open_24x24;
                    menuItemLager.Text = "Bestandsliste";
                    menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                    menuItemLager.Visible = bLagerBestand;
                    lvMenu.Items.Add(menuItemLager);

                    menuItemLager = new ListViewDataItem();
                    menuItemLager.Image = global::Sped4.Properties.Resources.Inventory_24x24;
                    menuItemLager.Text = "Inventurlisten";
                    menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                    menuItemLager.Visible = bLagerInventur;
                    lvMenu.Items.Add(menuItemLager);

                    menuItemLager = new ListViewDataItem();
                    menuItemLager.Image = global::Sped4.Properties.Resources.cabinet_warning_24x24;
                    menuItemLager.Text = "Sperrlager";
                    menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                    menuItemLager.Visible = (bLagerEingang & bLagerAusgang);  // BAUSTELLE
                    lvMenu.Items.Add(menuItemLager);

                    if (this._frmMain.system.Client.Modul.Lager_FreeForCall)
                    {
                        menuItemLager = new ListViewDataItem();
                        menuItemLager.Image = global::Sped4.Properties.Resources.chart_bar_24x24;
                        menuItemLager.Text = "Bestand freigeben";
                        menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        menuItemLager.Visible = bLagerBestand;
                        lvMenu.Items.Add(menuItemLager);
                    }

                    if (this._frmMain.system.Client.Modul.Lager_PostCenter)
                    {
                        menuItemLager = new ListViewDataItem();
                        menuItemLager.Image = global::Sped4.Properties.Resources.mail2_32x32;
                        menuItemLager.Text = "Post Center";
                        menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        menuItemLager.Visible = bLagerPost;
                        lvMenu.Items.Add(menuItemLager);
                    }

                    menuItemLager = new ListViewDataItem();
                    menuItemLager.Image = global::Sped4.Properties.Resources.magnifying_glass;
                    menuItemLager.Text = "Artikelsuche";
                    menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                    menuItemLager.Visible = bLagerArtSearch;
                    lvMenu.Items.Add(menuItemLager);

                    menuItemLager = new ListViewDataItem();
                    menuItemLager.Image = global::Sped4.Properties.Resources.chart_bar_24x24;
                    menuItemLager.Text = "Userlisten";
                    menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                    menuItemLager.Visible = bLagerUserlist;
                    lvMenu.Items.Add(menuItemLager);

                    if (this._frmMain.system.Client.Modul.Lager_Arbeitsliste)
                    {
                        menuItemLager = new ListViewDataItem();
                        menuItemLager.Image = global::Sped4.Properties.Resources.printer2_24x24;
                        menuItemLager.Text = "Arbeitsliste";
                        menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        menuItemLager.Visible = bLagerArbeitsliste;
                        lvMenu.Items.Add(menuItemLager);
                    }

                    menuItemLager = new ListViewDataItem();
                    menuItemLager.Image = global::Sped4.Properties.Resources.folders2_24x24;
                    menuItemLager.Text = "Archiv";
                    menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                    menuItemLager.Visible = bLagerArchiv;
                    lvMenu.Items.Add(menuItemLager);

                    lblCaption.myText = "Lagerverwaltung";

                    btnDispo.Active = false;
                    btnStatistik.Active = false;
                    btnStammdaten.Active = false;
                    btnLager.Active = true;
                    btnSpedition.Active = false;
                    btnFakturierung.Active = false;
                    //panDefault.Visible = false;
                    break;

                case EStatus.Spedition:

                    menuItemLager = new ListViewDataItem();
                    menuItemLager.Image = global::Sped4.Properties.Resources.chart_gantt_24x24;
                    menuItemLager.Text = "Dispoplan";
                    menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                    menuItemLager.Enabled = bDispositionDispoplan;
                    lvMenu.Items.Add(menuItemLager);

                    menuItemLager = new ListViewDataItem();
                    menuItemLager.Image = global::Sped4.Properties.Resources.code_delete_24x24;
                    menuItemLager.Text = "Dispoplan schließen";
                    menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                    menuItemLager.Enabled = bDispositionDispoplan;
                    lvMenu.Items.Add(menuItemLager);

                    menuItemLager = new ListViewDataItem();
                    menuItemLager.Image = global::Sped4.Properties.Resources.book_blue_open_24x24;
                    menuItemLager.Text = "Auftragsliste";
                    menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                    menuItemLager.Enabled = bDispositionAuftragsliste;
                    lvMenu.Items.Add(menuItemLager);


                    lblCaption.myText = "Spedition";

                    btnDispo.Active = true;
                    btnStatistik.Active = false;
                    btnStammdaten.Active = false;
                    btnLager.Active = false;
                    btnSpedition.Active = false;
                    btnFakturierung.Active = false;
                    break;

                case EStatus.Statistik:

                    if (this._frmMain.system.Client.Modul.Statistik_Lager)
                    {
                        menuItemLager = new ListViewDataItem();
                        menuItemLager.Image = global::Sped4.Properties.Resources.box_white_24x24;
                        menuItemLager.Text = "Lager";
                        menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        menuItemLager.Visible = bStatLager;
                        lvMenu.Items.Add(menuItemLager);
                    }

                    if (this._frmMain.system.Client.Modul.Statistik_FaktLager)
                    {
                        menuItemLager = new ListViewDataItem();
                        menuItemLager.Image = global::Sped4.Properties.Resources.money_24x24;
                        menuItemLager.Text = "Fakturierung Lager";
                        menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        menuItemLager.Visible = bStatFaktLager;
                        lvMenu.Items.Add(menuItemLager);
                    }

                    if (this._frmMain.system.Client.Modul.Statistik_FaktDispo)
                    {
                        menuItemLager = new ListViewDataItem();
                        menuItemLager.Image = global::Sped4.Properties.Resources.money2_24x24;
                        menuItemLager.Text = "Fakturierung Disposition";
                        menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        menuItemLager.Visible = bStatFaktDisposition;
                        lvMenu.Items.Add(menuItemLager);
                        lblCaption.myText = "Statistik";
                    }

                    if (this._frmMain.system.Client.Modul.Statistik_Waggonbuch)
                    {
                        menuItemLager = new ListViewDataItem();
                        menuItemLager.Image = global::Sped4.Properties.Resources.book_blue;
                        menuItemLager.Text = "Waggonbuch";
                        menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        menuItemLager.Visible = bStatLager;
                        lvMenu.Items.Add(menuItemLager);
                    }
                    btnDispo.Active = false;
                    btnStatistik.Active = true;
                    btnStammdaten.Active = false;
                    btnLager.Active = false;
                    btnSpedition.Active = false;
                    btnFakturierung.Active = false;
                    break;

                case EStatus.Stammdaten:

                    menuItemLager = new ListViewDataItem();
                    menuItemLager.Image = global::Sped4.Properties.Resources.address_book2;
                    menuItemLager.Text = "Adressen";
                    menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                    menuItemLager.Visible = bStammADR;
                    lvMenu.Items.Add(menuItemLager);

                    if (this._frmMain.system.Client.Modul.Stammdaten_Personal)
                    {
                        menuItemLager = new ListViewDataItem();
                        menuItemLager.Image = global::Sped4.Properties.Resources.users_24x24;
                        menuItemLager.Text = "Personal";
                        menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        menuItemLager.Visible = bStammPersonal;
                        lvMenu.Items.Add(menuItemLager);
                    }

                    if (this._frmMain.system.Client.Modul.Stammdaten_Fahrzeuge)
                    {
                        menuItemLager = new ListViewDataItem();
                        menuItemLager.Image = global::Sped4.Properties.Resources.truck_green_24x24;
                        menuItemLager.Text = "Fahrzeuge";
                        menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        menuItemLager.Visible = bStammFahrzeuge;
                        lvMenu.Items.Add(menuItemLager);
                    }
                    if (this._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinition)
                    {
                        menuItemLager = new ListViewDataItem();
                        menuItemLager.Image = global::Sped4.Properties.Resources.gears;
                        menuItemLager.Text = "Güterart";
                        menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        menuItemLager.Visible = bStammGArt;
                        lvMenu.Items.Add(menuItemLager);
                    }
                    else
                    {
                        menuItemLager = new ListViewDataItem();
                        menuItemLager.Image = global::Sped4.Properties.Resources.gears;
                        menuItemLager.Text = "Warengruppen";
                        menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        menuItemLager.Visible = bStammGArt;
                        lvMenu.Items.Add(menuItemLager);
                    }

                    if (this._frmMain.system.Client.Modul.Stammdaten_Relation)
                    {
                        menuItemLager = new ListViewDataItem();
                        menuItemLager.Image = global::Sped4.Properties.Resources.signal_flag_blue_24x24;
                        menuItemLager.Text = "Relation";
                        menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        menuItemLager.Visible = bStammRelation;
                        lvMenu.Items.Add(menuItemLager);
                    }

                    if (this._frmMain.system.Client.Modul.Stammdaten_Lagerortverwaltung)
                    {
                        menuItemLager = new ListViewDataItem();
                        menuItemLager.Image = global::Sped4.Properties.Resources.shelf_empty_24x24;
                        menuItemLager.Text = "Lagerortverwaltung";
                        menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        menuItemLager.Visible = bStammLagerOrt;
                        lvMenu.Items.Add(menuItemLager);
                    }
                    if (this._frmMain.system.Client.Modul.Stammdaten_Lagerreihenverwaltung)
                    {
                        menuItemLager = new ListViewDataItem();
                        menuItemLager.Image = global::Sped4.Properties.Resources.shelf_empty_24x24;
                        menuItemLager.Text = "Lagerreihen";
                        menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        menuItemLager.Visible = bStammLagerOrt;
                        lvMenu.Items.Add(menuItemLager);
                    }


                    if (this._frmMain.system.Client.Modul.Stammdaten_Schaeden)
                    {
                        menuItemLager = new ListViewDataItem();
                        menuItemLager.Image = global::Sped4.Properties.Resources.gear_warning_24x24;
                        menuItemLager.Text = "Schäden";
                        menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        menuItemLager.Visible = bStammSchaden;
                        lvMenu.Items.Add(menuItemLager);
                    }

                    if (this._frmMain.system.Client.Modul.Stammdaten_Einheiten)
                    {
                        menuItemLager = new ListViewDataItem();
                        menuItemLager.Image = global::Sped4.Properties.Resources.cube_molecule_24x24;
                        menuItemLager.Text = "Einheiten";
                        menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        menuItemLager.Visible = bStammEinheiten;
                        lvMenu.Items.Add(menuItemLager);
                    }

                    if (this._frmMain.system.Client.Modul.Stammdaten_ExtraCharge)
                    {
                        menuItemLager = new ListViewDataItem();
                        menuItemLager.Image = global::Sped4.Properties.Resources.money_24x24;
                        menuItemLager.Text = "Nebenkosten";
                        menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        menuItemLager.Visible = bStammExtraCharge;
                        lvMenu.Items.Add(menuItemLager);
                    }

                    if (this._frmMain.system.Client.Modul.Stammdaten_KontenPlan)
                    {
                        menuItemLager = new ListViewDataItem();
                        menuItemLager.Image = global::Sped4.Properties.Resources.chart_gantt_24x24;
                        menuItemLager.Text = "Kontenplan";
                        menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        menuItemLager.Visible = bStammKP;
                        lvMenu.Items.Add(menuItemLager);
                    }

                    if (this._frmMain.system.Client.Modul.Stammdaten_StorelocationLable)
                    {
                        menuItemLager = new ListViewDataItem();
                        menuItemLager.Image = global::Sped4.Properties.Resources.shelf_empty_24x24;
                        menuItemLager.Text = "Lagerort Label Drucken";
                        menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        menuItemLager.Visible = true;
                        lvMenu.Items.Add(menuItemLager);
                    }

                    lblCaption.myText = "Stammdaten";

                    btnDispo.Active = false;
                    btnStatistik.Active = false;
                    btnStammdaten.Active = true;
                    btnLager.Active = false;
                    btnSpedition.Active = false;
                    btnFakturierung.Active = false;
                    break;

                case EStatus.Fakturierung:

                    menuItemLager = new ListViewDataItem();
                    menuItemLager.Image = global::Sped4.Properties.Resources.notebook3_24x24;
                    menuItemLager.Text = "Einzeltarifabrechnung";
                    menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                    menuItemLager.Visible = bFaktLager;
                    lvMenu.Items.Add(menuItemLager);

                    menuItemLager = new ListViewDataItem();
                    menuItemLager.Image = global::Sped4.Properties.Resources.folder2;
                    menuItemLager.Text = "Rechnungen";
                    menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                    menuItemLager.Visible = bFaktRGBuch;
                    lvMenu.Items.Add(menuItemLager);

                    menuItemLager = new ListViewDataItem();
                    menuItemLager.Image = global::Sped4.Properties.Resources.chart_gantt_24x24;
                    menuItemLager.Text = "Manuelle Rechnung";
                    menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                    menuItemLager.Visible = bFaktRGBuch;
                    lvMenu.Items.Add(menuItemLager);

                    menuItemLager = new ListViewDataItem();
                    menuItemLager.Image = global::Sped4.Properties.Resources.notebook3_24x24;
                    menuItemLager.Text = "Spedition";
                    menuItemLager.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                    menuItemLager.Visible = bFaktSped;
                    lvMenu.Items.Add(menuItemLager);

                    lblCaption.myText = "Fakturierung";

                    btnDispo.Active = false;
                    btnStatistik.Active = false;
                    btnStammdaten.Active = false;
                    btnLager.Active = false;
                    btnSpedition.Active = false;
                    btnFakturierung.Active = true;
                    break;

                case EStatus.Default:

                    lblCaption.myText = "Sped - LVS";

                    btnDispo.Active = false;
                    btnStatistik.Active = false;
                    btnStammdaten.Active = false;
                    btnLager.Active = false;
                    btnSpedition.Active = false;
                    btnFakturierung.Active = false;
                    break;
            }

            btnFakturierung.Refresh();
            btnDispo.Refresh();
            btnSpedition.Refresh();
            btnStatistik.Refresh();
            btnStammdaten.Refresh();
            btnLager.Refresh();
            lblCaption.Refresh();
        }
        ///<summary>ctrMenu / radListView1_ItemMouseClick</summary>
        ///<remarks></remarks> 
        private void radListView1_ItemMouseClick(object sender, ListViewItemEventArgs e)
        {
            object obj = null;
            if (((RadListViewElement)sender).CurrentItem != null)
            {
                switch (Status)
                {
                    case EStatus.Lager:
                        switch (((RadListViewElement)sender).CurrentItem.Text)
                        {
                            case "Einlagerung":
                                obj = new object();
                                OpenCtrEinlagerung(obj);
                                break;
                            case "Auslagerung":
                                obj = new object();
                                OpenCtrAuslagerung(obj);
                                break;
                            case "Arbeitsliste":
                                OpenCtrWorklist();
                                //OpenCtrArbeitsliste(new ctrArbeitsliste());
                                break;
                            case "Post Center":
                                this._ctrPostCenter = new ctrPostCenter();
                                obj = this._ctrPostCenter;
                                OpenFrmTMP(obj);
                                break;
                            case "Userlisten":
                                OpenCtrUserList();
                                break;
                            case "Artikelsuche":
                                //obj = new ctrSearch();
                                //OpenCtrEinlagerung(obj);
                                //OpenCtrSearch(this._ctrSearch);
                                this._ctrSearch = new ctrSearch(true);
                                OpenCtrSearch(this._ctrSearch);
                                break;
                            case "Bestand freigeben":
                                OpenCtrFreeForCall();
                                break;
                            case "Sperrlager":
                                OpenCtrSperrlager();
                                break;
                            case "Sperrlagertransfer":
                                OpenCtrSendToSPL();
                                break;
                            case "Journal Lager":
                                OpenCtrJournal();
                                break;
                            case "Bestandsliste":
                                OpenCtrBestand();
                                break;
                            case "Inventurlisten":
                                OpenCtrInventory();
                                break;
                            case "Umbuchung":
                                obj = this._ctrUmbuchung;
                                this._ctrEinlagerung = new ctrEinlagerung();
                                OpenCtrUmbuchungInFrm(obj, this._ctrEinlagerung);
                                break;

                            case "Archiv":
                                OpenFrmArchivView();
                                break;
                        }
                        break;
                    case EStatus.Stammdaten:
                        switch (((RadListViewElement)sender).SelectedItem.Text)
                        {
                            case "Adressen":
                                OpenADRFrmAndList();
                                break;
                            case "Personal":
                                OpenCtrPersonal();
                                break;
                            case "Fahrzeuge":
                                OpenCtrFahrzeuge();
                                break;
                            case "Güterart":
                            case "Warengruppen":
                                OpenCtrGArt();
                                break;
                            case "Relation":
                                OpenCtrRelation();
                                break;
                            case "Lagerortverwaltung":
                                OpenCtrLagerOrt();
                                break;
                            case "Schäden":
                                OpenCtrSchaeden(false);
                                break;
                            case "Einheiten":
                                OpenCtrEinheitenListe();
                                break;
                            case "Kontenplan":
                                OpenCtrTableOfAccount();
                                break;
                            case "Lagerort Label Drucken":
                                OpenCtrPrintLagerOrtLabel();
                                break;
                            case "Nebenkosten":
                                OpenCtrExtraCharge();
                                break;
                            case "Lagerreihen":
                                OpenCtrLagerreihen();
                                break;
                        }
                        break;
                    case EStatus.Spedition:
                        switch (((RadListViewElement)sender).SelectedItem.Text)
                        {
                            case "Dispoplan":
                                OpenCtrAuftraege();
                                //Test Dispo
                                OpenFrmDispo();
                                //DispoKalenderOpen();
                                break;
                            case "Dispoplan schließen":
                                CloseDispoKaledner();
                                break;
                            case "Auftragsliste":
                                OpenCtrAuftraege();
                                break;
                        }
                        break;
                    case EStatus.Fakturierung:
                        switch (((RadListViewElement)sender).SelectedItem.Text)
                        {
                            case "Einzeltarifabrechnung":
                            case "Lager":
                                OpenCtrFaktLager();
                                break;
                            case "Rechnungen":
                                OpenCtrRGList();
                                break;
                            case "Manuelle Rechnung":
                                OpenCtrRGManuell();
                                break;

                        }
                        break;
                    case EStatus.Statistik:
                        switch (((RadListViewElement)sender).SelectedItem.Text)
                        {
                            case "Lager":
                                OpenCtrStatistik(enumStatistikArt.Lager.ToString());
                                break;
                            case "Fakturierung Lager":
                                OpenCtrStatistik(enumStatistikArt.RGLager.ToString());
                                break;
                            case "Fakturierung Disposition":
                                OpenCtrStatistik(enumStatistikArt.RGDispo.ToString());
                                break;
                            case "Waggonbuch":
                                OpenCtrWaggonbuch();
                                break;
                        }
                        break;
                }
            }
        }
        ///<summary>ctrMenu / InitCtr</summary>
        ///<remarks>Ctr.Widt wird auf die Standardbreite gebracht und über die Submenüs wird ein Dockstyle (Fill) gelegt.</remarks>
        public void InitCtr()
        {
            this.Width = 210;
            this.panDefault.Dock = DockStyle.Fill;
            Status = EStatus.Default;
            this.Refresh();
        }
        /*******************************************************************************
         *                          Disposition  
         * ****************************************************************************/
        ///<summary>ctrMenu / btnDispo_Click</summary>
        ///<remarks></remarks>
        private void btnDispo_Click()
        {
            Status = EStatus.Spedition;
            this.OpenCtrAuftrag();
        }
        ///<summary>ctrMenu / btnAuftrag_Click</summary>
        ///<remarks></remarks>
        private void btnAuftrag_Click()
        {
            //if (bDispositionAuftragsliste)
            //{
            //    if (Status != EStatus.Auftrag)
            //    {
            //        for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            //        {
            //            try
            //            {
            //                if (this.ParentForm.Controls[i].Name == const_TMP + "Splitter")
            //                {
            //                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
            //                }
            //                if (this.ParentForm.Controls[i].GetType() == typeof(ctrAufträge))
            //                {
            //                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
            //                }
            //            }
            //            catch
            //            {
            //            }
            //        }
            //    }
            //    Status = EStatus.Auftrag;

            //    //--------------- AUftrag direkt öffnen ,da kein Untermenü
            //    bool open = false;
            //    Int32 j = -1;

            //    for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            //    {
            //        if (this.ParentForm.Controls[i].GetType() == typeof(ctrAufträge))
            //        {
            //            open = true;
            //            j = i;
            //        }
            //    }

            //    if (open)
            //    {
            //        this.ParentForm.Controls[j].Show();
            //    }
            //    else
            //    {

            //        Thread OpenThread = new Thread(this.OpenCtrAuftrag);
            //        OpenThread.Start();

            //        CreateSplitter(const_TMP + "SplitterAuftrag");
            //    }
            //}
        }
        ///<summary>ctrMenu / OpenCtrAuftrag</summary>
        ///<remarks></remarks>
        private void OpenCtrAuftrag()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ThreadCtrInvokeEventHandler(OpenCtrAuftrag));
                return;
            }
            ctrAufträge ctrAuftrag = new ctrAufträge();
            ctrAuftrag.GL_User = this.GL_User;
            ctrAuftrag._ctrMenu = this;
            ctrAuftrag.Dock = System.Windows.Forms.DockStyle.Left;
            ctrAuftrag.Name = const_TMP + "Auftrag";
            ctrAuftrag.OpenKalender += new ctrAufträge.OpenKalenerFrmEventHandler(ctrAuftrag_OpenKalender);
            this.ParentForm.Controls.Add(ctrAuftrag);
            this.ParentForm.Controls.SetChildIndex(ctrAuftrag, 0);
            ctrAuftrag.Show();

            this._ctrAuftrag = ctrAuftrag;
        }

        //************************************************************************************************
        //*************************************  Stammdaten  *********************************************
        ///<summary>ctrMenu / btnStammdaten_Click</summary>
        ///<remarks></remarks>
        private void btnStammdaten_Click()
        {
            Status = EStatus.Stammdaten;
        }
        ///<summary>ctrMenu / OpenStammdatenFromMDI</summary>
        ///<remarks></remarks>
        public void OpenStammdatenFromMDI(string StammdatenToOpen)
        {
            Status = EStatus.Stammdaten;
            EventArgs e = new EventArgs();
            switch (StammdatenToOpen)
            {
                case "ADR":
                    lADR_Click(this, e);
                    break;
                case "Fahrzeuge":
                    lFahrzeuge_Click(this, e);
                    break;
                case "GArt":
                    lGArt_Click(this, e);
                    break;
                case "Personal":
                    lPersonal_Click(this, e);
                    break;
                case "Relation":
                    lRelation_Click(this, e);
                    break;
            }
        }
        //
        //**************************************************************************** ADR  
        ///<summary>ctrMenü / lADR_Click</summary>
        ///<remarks></remarks>
        private void lADR_Click(object sender, EventArgs e)
        {
            if (Status == EStatus.Stammdaten)
            {
                OpenADRFrmAndList();
            }
        }
        ///<summary>ctrMenü / OpenADRFrmAndList</summary>
        ///<remarks></remarks>
        public void OpenADRFrmAndList()
        {
            Int32 j = -1;
            for (int i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrADR_List))
                {
                    j = i;
                }
            }
            if (j > -1)
            {
                this.ParentForm.Controls[j].Show();
            }
            else
            {
                //--- generieren ADR Liste und anzeigenL
                Globals.t1 = DateTime.Now;
                //ctrADR_List ctrADRList = new ctrADR_List();
                //ctrADRList.SetGlobalValue(this);
                //ctrADRList.Dock = DockStyle.Left;
                //ctrADRList.Name = const_TMP + "ADR";

                ctrADR_List ctrADRList = InitADRList();
                this.ParentForm.Controls.Add(ctrADRList);
                this.ParentForm.Controls.SetChildIndex(ctrADRList, 0);
                ctrADRList.Show();
                //this.ResumeLayout();
                //this.ParentForm.ResumeLayout(true);
                Globals.t2 = DateTime.Now;
                Globals.time();
            }
        }

        public ctrADR_List InitADRList()
        {
            ctrADR_List ctrADRListTmp = new ctrADR_List();
            ctrADRListTmp.SetGlobalValue(this);
            ctrADRListTmp.Dock = DockStyle.Left;
            ctrADRListTmp.Name = const_TMP + "ADR";
            ctrADRListTmp._ctrMenu = this;
            return ctrADRListTmp;
        }
        public ctrADRSearch InitAdrSearch()
        {
            ctrADRSearch ctrADRSearchTmp = new ctrADRSearch();
            ctrADRSearchTmp.SetGlobalValue(this);
            ctrADRSearchTmp.Dock = DockStyle.Left;
            ctrADRSearchTmp.Name = const_TMP + "ADRSearch";
            ctrADRSearchTmp._ctrMenu = this;
            return ctrADRSearchTmp;
        }
        //************************************************************************************ Fahrzeuge  
        ///<summary>ctrMenü / lFahrzeuge_Click</summary>
        ///<remarks>.</remarks>
        private void lFahrzeuge_Click(object sender, EventArgs e)
        {
            OpenCtrFahrzeuge();
        }
        ///<summary>ctrMenü / OpenCtrFahrzeuge</summary>
        ///<remarks>.</remarks>
        private void OpenCtrFahrzeuge()
        {
            if (Status == EStatus.Stammdaten)
            {
                Int32 j = -1;
                for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
                {
                    if (this.ParentForm.Controls[i].GetType() == typeof(ctrFahrzeug_List))
                    {
                        j = i;
                    }
                }
                if (j > -1)
                {
                    this.ParentForm.Controls[j].Show();
                }
                else
                {
                    OpenFahrzeugFrmAndList();
                }
            }
        }
        ///<summary>ctrMenü / OpenFahrzeugFrmAndList</summary>
        ///<remarks>generieren Fahrzeug Liste und anzeigen</remarks>
        public void OpenFahrzeugFrmAndList()
        {
            ctrFahrzeug_List ctrFahrzeugListe = new ctrFahrzeug_List();
            ctrFahrzeugListe.GL_User = GL_User;
            ctrFahrzeugListe._ctrMenu = this;
            ctrFahrzeugListe.initList();
            ctrFahrzeugListe.Dock = DockStyle.Left;
            ctrFahrzeugListe.Name = const_TMP + "Fahrzeuge";
            this.ParentForm.Controls.Add(ctrFahrzeugListe);
            this.ParentForm.Controls.SetChildIndex(ctrFahrzeugListe, 0);
            ctrFahrzeugListe.Show();

            CreateSplitter(const_TMP + "SplitterFahrzeuge");
        }
        ///<summary>ctrMenu / CloseCtrFahrzeugList</summary>
        ///<remarks>SChließt das Ctr Fahrzeugliste</remarks>
        public void CloseCtrFahrzeugList()
        {
            Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].Name == const_TMP + "SplitterFahrzeuge")
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    //i = Count - 1;      // ist nur ein Controll vorhanden
                }
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrFahrzeug_List))
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    i = Count - 1;      // ist nur ein Controll vorhanden
                }
            }
        }
        ///<summary>ctrMenü / lRelation_Click</summary>
        ///<remarks></remarks>
        private void lRelation_Click(object sender, EventArgs e)
        {
            OpenCtrRelation();
        }
        ///<summary>ctrMenü / OpenCtrRelation</summary>
        ///<remarks></remarks>
        private void OpenCtrRelation()
        {
            if (Status == EStatus.Stammdaten)
            {
                Int32 j = -1;
                for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
                {
                    if (this.ParentForm.Controls[i].GetType() == typeof(ctrRelationen))
                    {
                        j = i;
                    }
                }
                if (j > -1)
                {
                    this.ParentForm.Controls[j].Show();
                }
                else
                {
                    OpenRelationFrmAndList();
                }
            }
        }
        public void OpenRelationFrmAndList()
        {
            //--- generieren ADR Liste und anzeigen
            ctrRelationen ctrRelation = new ctrRelationen();
            ctrRelation.GL_User = GL_User;
            ctrRelation.Dock = DockStyle.Left;
            ctrRelation.Name = const_TMP + "Relation";
            this.ParentForm.Controls.Add(ctrRelation);
            this.ParentForm.Controls.SetChildIndex(ctrRelation, 0);
            ctrRelation.Show();

            CreateSplitter(const_TMP + "SplitterRelation");
        }
        //---------------- Personal  ---------------------------------
        //
        //
        private void lPersonal_Click(object sender, EventArgs e)
        {
            OpenCtrPersonal();
        }

        public void OpenCtrPersonal()
        {
            if (Status == EStatus.Stammdaten)
            {
                Int32 j = -1;
                for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
                {
                    if (this.ParentForm.Controls[i].GetType() == typeof(ctrPersonal_List))
                    {
                        j = i;
                    }
                }
                if (j > -1)
                {
                    this.ParentForm.Controls[j].Show();
                }
                else
                {
                    OpenPersonalFrmAndList();
                }
            }
        }

        public void OpenPersonalFrmAndList()
        {
            //--- generieren Liste und anzeigen
            ctrPersonal_List ctrPersonalList = new ctrPersonal_List();
            ctrPersonalList.GL_User = GL_User;
            ctrPersonalList.initList();
            ctrPersonalList.Width = 300;
            ctrPersonalList.Dock = DockStyle.Left;
            ctrPersonalList.Name = const_TMP + "Personal";
            this.ParentForm.Controls.Add(ctrPersonalList);
            this.ParentForm.Controls.SetChildIndex(ctrPersonalList, 0);
            ctrPersonalList.Show();

            CreateSplitter(const_TMP + "SplitterPersonal");
        }
        //************************************************************************* Güterarten
        ///<summary>ctrMenu / lGArt_Click</summary>
        ///<remarks></remarks>
        private void lGArt_Click(object sender, EventArgs e)
        {
            OpenCtrGArt();
        }
        private void OpenCtrGArt()
        {
            if (Status == EStatus.Stammdaten)
            {
                Int32 j = -1;
                for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
                {
                    if (this.ParentForm.Controls[i].GetType() == typeof(ctrGueterArtListe))
                    {
                        j = i;
                    }
                }
                if (j > -1)
                {
                    this.ParentForm.Controls[j].Show();
                }
                else
                {
                    OpenCtrGArtenList(this);
                }
            }
        }
        ///<summary>ctrMenü / OpenCtrGArtenList</summary>
        ///<remarks>.</remarks>
        public void OpenCtrGArtenList(object obj)
        {
            //-- generieren GAListe und anzeigen
            ctrGueterArtListe ctrGut = new ctrGueterArtListe();
            ctrGut._ctrMenu = this;
            ctrGut.Dock = DockStyle.Left;
            ctrGut.Name = const_TMP + "Gut";
            ctrGut.cbAdrGArtAssignmentFilter.Visible = false;
            ctrGut.cbAdrGArtAssignmentFilter.Checked = false;
            if (obj.GetType() == typeof(frmGArtenAuftragserfassung))
            {
                ctrGut._frmGArtenAuftragserfassung = (frmGArtenAuftragserfassung)obj;
                //if (ctrGut._frmGArtenAuftragserfassung.AdrIDForGArtAssignment > 0)
                //{
                //    ctrGut.cbAdrGArtAssignmentFilter.Visible = this._frmMain.system.Client.Modul.Stammdaten_UseGutAdrAssignment;
                //    ctrGut.cbAdrGArtAssignmentFilter.Checked = this._frmMain.system.Client.Modul.Stammdaten_UseGutAdrAssignment;
                //    ctrGut.AdrIDForGArtAssignment = ctrGut._frmGArtenAuftragserfassung.AdrIDForGArtAssignment;
                //}
                ctrGut.SearchGArt = true;  // aktivieren für das Suchformular

                ctrGut.getIDTakeOver += new ctrGueterArtListe.IDTakeOverEventHandler(ctrGut._frmGArtenAuftragserfassung.SetSearch_ID);
                ctrGut.ClosefrmGArtenAuftragserfassung += new ctrGueterArtListe.frmGArtenAuftragsErfassungEventHandler(ctrGut._frmGArtenAuftragserfassung.CloseFrmGArtenAutfragsErfassung);
                ctrGut.Dock = DockStyle.Fill;
                ctrGut.Parent = (frmGArtenAuftragserfassung)obj;
            }
            else
            {
                this.ParentForm.Controls.Add(ctrGut);
                this.ParentForm.Controls.SetChildIndex(ctrGut, 0);
            }

            ctrGut.Show();
            ctrGut.BringToFront();
            CreateSplitter(const_TMP + "SplitterGut");
        }
        //************************************************************************ Schaden
        ///<summary>ctrMenu / OpenCtrSchaeden</summary>
        ///<remarks></remarks>
        public void lSchaden_Click(object sender, EventArgs e)
        {
            OpenCtrSchaeden(false);
        }
        ///<summary>ctrMenu / OpenCtrSchaeden</summary>
        ///<remarks></remarks>
        public void OpenCtrSchaeden(bool bColaps)
        {
            if (Status == EStatus.Stammdaten)
            {
                Int32 j = -1;
                for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
                {
                    if (this.ParentForm.Controls[i].GetType() == typeof(ctrSchaeden))
                    {
                        j = i;
                    }
                }
                if (j > -1)
                {
                    this.ParentForm.Controls[j].Show();
                }
                else
                {
                    _ctrSchaeden = new ctrSchaeden();
                    _ctrSchaeden.GL_User = GL_User;
                    _ctrSchaeden.Dock = DockStyle.Left;
                    _ctrSchaeden._ctrMenu = this;
                    _ctrSchaeden.scSchaden.Panel1Collapsed = bColaps;
                    _ctrSchaeden.Name = const_TMP + "Schaden";
                    this.ParentForm.Controls.Add(_ctrSchaeden);
                    this.ParentForm.Controls.SetChildIndex(_ctrSchaeden, 0);
                    _ctrSchaeden.Show();

                    CreateSplitter(const_TMP + "SplitterSchaden");
                }
            }
        }
        ///<summary>ctrMenu / OpenCtrSchaedenInFrm</summary>
        ///<remarks></remarks>
        public void OpenCtrSchaedenInFrm(object myObj)
        {
            this._ctrSchaeden = new ctrSchaeden();
            this._ctrSchaeden.scSchaden.Panel1Collapsed = true;
            if (typeof(ctrEinlagerung) == myObj.GetType())
            {
                this._ctrSchaeden.iListToShow = clsSchaeden.const_Art_Active;
                this._ctrSchaeden._ctrEinlagerung = (ctrEinlagerung)myObj;
            }
            object obj = this._ctrSchaeden;
            OpenFrmTMP(obj);
        }
        ///<summary>ctrMenu / CloseCtrSchaden</summary>
        ///<remarks></remarks>
        public void CloseCtrSchaden()
        {
            Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].Name == const_TMP + "SplitterSchaden")
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    //i = Count - 1;      // ist nur ein Controll vorhanden
                }
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrSchaeden))
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    i = Count - 1;      // ist nur ein Controll vorhanden
                }
            }
        }
        //******************************************************************************* Einheiten
        ///<summary>ctrMenu / lEinheiten_Click</summary>
        ///<remarks></remarks>
        private void lEinheiten_Click(object sender, EventArgs e)
        {
            OpenCtrEinheitenListe();
        }
        ///<summary>ctrMenu / OpenCtrEinheitenListe</summary>
        ///<remarks></remarks>
        public void OpenCtrEinheitenListe()
        {
            if (Status == EStatus.Stammdaten)
            {
                Int32 j = -1;
                for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
                {
                    if (this.ParentForm.Controls[i].GetType() == typeof(ctrEinheitenListe))
                    {
                        j = i;
                    }
                }
                if (j > -1)
                {
                    this.ParentForm.Controls[j].Show();
                }
                else
                {
                    _ctrEinheitenListe = new ctrEinheitenListe();
                    _ctrEinheitenListe.GL_User = GL_User;
                    _ctrEinheitenListe.Dock = DockStyle.Left;
                    _ctrEinheitenListe._ctrMenu = this;
                    _ctrEinheitenListe.Name = const_TMP + "Einheit";
                    this.ParentForm.Controls.Add(_ctrEinheitenListe);
                    this.ParentForm.Controls.SetChildIndex(_ctrEinheitenListe, 0);
                    _ctrEinheitenListe.Show();

                    CreateSplitter(const_TMP + "SplitterEinheit");
                }
            }
        }
        ///<summary>ctrMenu / CloseCtrEinheitenListe</summary>
        ///<remarks></remarks>
        public void CloseCtrEinheitenListe()
        {
            Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].Name == const_TMP + "SplitterEinheit")
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    //i = Count - 1;      // ist nur ein Controll vorhanden
                }
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrEinheitenListe))
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    i = Count - 1;      // ist nur ein Controll vorhanden
                }
            }
        }
        //******************************************************************************* Extrakosten
        ///<summary>ctrMenu / lExtraCharge_Click</summary>
        ///<remarks></remarks>
        private void lExtraCharge_Click(object sender, EventArgs e)
        {
            OpenCtrExtraCharge();
        }
        ///<summary>ctrMenu / OpenCtrEinheitenListe</summary>
        ///<remarks>Öffnen Ctr ExtraCharge</remarks>
        public void OpenCtrExtraCharge()
        {
            if (Status == EStatus.Stammdaten)
            {
                Int32 j = -1;
                for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
                {
                    if (this.ParentForm.Controls[i].GetType() == typeof(ctrExtraCharge))
                    {
                        j = i;
                    }
                }
                if (j > -1)
                {
                    this.ParentForm.Controls[j].Show();
                }
                else
                {
                    _ctrExtraCharge = new ctrExtraCharge();
                    _ctrExtraCharge.InitGlobals(this);
                    _ctrExtraCharge.Dock = DockStyle.Left;
                    _ctrExtraCharge.Name = const_TMP + "ExtraCharge";
                    this.ParentForm.Controls.Add(_ctrExtraCharge);
                    this.ParentForm.Controls.SetChildIndex(_ctrExtraCharge, 0);
                    _ctrExtraCharge.Show();

                    CreateSplitter(const_TMP + "SplitterExtraCharge");
                }
            }
        }
        ///<summary>ctrMenu / CloseCtrExtraCharge</summary>
        ///<remarks>schließt Ctr ExtraCharge</remarks>
        public void CloseCtrExtraCharge()
        {
            Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].Name == const_TMP + "SplitterExtraCharge")
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    //i = Count - 1;      // ist nur ein Controll vorhanden
                }
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrExtraCharge))
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    i = Count - 1;      // ist nur ein Controll vorhanden
                }
            }
        }

        //******************************************************************************* Konteplan / TableOfAccount
        ///<summary>ctrMenu / lExtraCharge_Click</summary>
        ///<remarks></remarks>
        private void lStammTableOfAccount_Click(object sender, EventArgs e)
        {
            OpenCtrTableOfAccount();
        }
        ///<summary>ctrMenu / OpenCtrEinheitenListe</summary>
        ///<remarks>Öffnen Ctr ExtraCharge</remarks>
        public void OpenCtrTableOfAccount()
        {
            if (Status == EStatus.Stammdaten)
            {
                Int32 j = -1;
                for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
                {
                    if (this.ParentForm.Controls[i].GetType() == typeof(ctrTableOfAccount))
                    {
                        j = i;
                    }
                }
                if (j > -1)
                {
                    this.ParentForm.Controls[j].Show();
                }
                else
                {
                    _ctrTableOfAccount = new ctrTableOfAccount();
                    _ctrTableOfAccount.InitGlobals(this);
                    _ctrTableOfAccount.Dock = DockStyle.Left;
                    _ctrTableOfAccount.Name = const_TMP + "ExtraCharge";
                    this.ParentForm.Controls.Add(_ctrTableOfAccount);
                    this.ParentForm.Controls.SetChildIndex(_ctrTableOfAccount, 0);
                    _ctrTableOfAccount.Show();

                    CreateSplitter(const_TMP + "SplitterTableOfAccount");
                }
            }
        }
        ///<summary>ctrMenu / CloseCtrTableOfAccount</summary>
        ///<remarks>schließt Ctr CtrTableOfAccount</remarks>
        public void CloseCtrTableOfAccount()
        {
            Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].Name == const_TMP + "SplitterTableOfAccount")
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    //i = Count - 1;      // ist nur ein Controll vorhanden
                }
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrTableOfAccount))
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    i = Count - 1;      // ist nur ein Controll vorhanden
                }
            }
        }
        //************************************************ Menü Disposition ****************************
        ///<summary>ctrMenu / label1_Click</summary>
        ///<remarks></remarks>
        private void label1_Click(object sender, EventArgs e)
        {
            if (Status == EStatus.Spedition)
            {
                OpenCtrAuftraege();
                DispoKalenderOpen();
            }
        }
        ///<summary>ctrMenu / CloseCtrTableOfAccount</summary>
        ///<remarks></remarks>
        public void DispoKalenderOpen()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ThreadCtrInvokeEventHandler(DispoKalenderOpen));
                return;
            }

            foreach (ctrAufträge ctr in this.ParentForm.Controls.Find(const_TMP + "Auftrag", true))
            {
                if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmDispoKalender)) == null)
                {

                    frmDispoKalender Kalender = new frmDispoKalender(ctr);
                    Kalender.menue = this;
                    Kalender.GL_User = this.GL_User;
                    Kalender.WindowState = FormWindowState.Maximized;
                    Kalender.Show();
                    Kalender.BringToFront();
                }
                else
                {
                    //this.Kalender.Show();
                }
                if (DispoDataChanged != null)
                {
                    DispoDataChanged(this);
                }
            }
        }
        //Test Dispo
        public void OpenFrmDispo()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ThreadCtrInvokeEventHandler(OpenFrmDispo));
                return;
            }

            foreach (ctrAufträge ctr in this.ParentForm.Controls.Find(const_TMP + "Auftrag", true))
            {
                if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmDispo)) == null)
                {

                    frmDispo Kalender = new frmDispo(ctr);
                    Kalender.menue = this;
                    Kalender.GL_User = this.GL_User;
                    Kalender.WindowState = FormWindowState.Maximized;
                    Kalender.Show();
                    Kalender.BringToFront();
                }
                else
                {
                    //this.Kalender.Show();
                }
                if (DispoDataChanged != null)
                {
                    DispoDataChanged(this);
                }
            }
        }
        //
        //
        public void DispoKalenderOpenSplitter()
        {
            if (this._ctrAuftrag.GetType() == typeof(ctrAufträge))
            {
                if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmDispoKalender)) == null)
                {
                    frmDispoKalender Kalender = new frmDispoKalender(this._ctrAuftrag);
                    Kalender.menue = this;
                    Kalender.GL_User = this.GL_User;
                    Kalender.MdiParent = this.ParentForm;
                    Kalender.Dock = System.Windows.Forms.DockStyle.Left;
                    Kalender.Name = const_TMP + "Dispo";
                    Kalender.WindowState = FormWindowState.Maximized;
                    Kalender.Show();
                    Kalender.BringToFront();

                    CreateSplitter(const_TMP + "SplitterDispo");
                }
                else
                {
                    //this.Kalender.Show();
                }
                if (DispoDataChanged != null)
                {
                    DispoDataChanged(this);
                }
            }
        }
        //testc Dispo
        public void DispoOpenSplitter()
        {
            if (this._ctrAuftrag.GetType() == typeof(ctrAufträge))
            {
                if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmDispo)) == null)
                {
                    frmDispo Kalender = new frmDispo(this._ctrAuftrag);
                    Kalender.menue = this;
                    Kalender.GL_User = this.GL_User;
                    Kalender.MdiParent = this.ParentForm;
                    Kalender.Dock = System.Windows.Forms.DockStyle.Left;
                    Kalender.Name = const_TMP + "Dispo";
                    Kalender.WindowState = FormWindowState.Maximized;
                    Kalender.Show();
                    Kalender.BringToFront();

                    CreateSplitter(const_TMP + "SplitterDispo");
                }
                else
                {
                    //this.Kalender.Show();
                }
                if (DispoDataChanged != null)
                {
                    DispoDataChanged(this);
                }
            }
        }
        //Dispoplan close
        private void label2_Click(object sender, EventArgs e)
        {
            if (Status == EStatus.Spedition)
            {
                CloseDispoKaledner();
            }
        }
        ///<summary>ctrMenü / CloseDispoKaledner</summary>
        ///<remarks></remarks>
        public void CloseDispoKaledner()
        {
            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmDispoKalender)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmDispoKalender));
            }
        }
        ///<summary>ctrMenü / CloseCtrAutrag</summary>
        ///<remarks></remarks>
        public void CloseCtrAutrag()
        {
            Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].Name == const_TMP + "SplitterAuftrag")
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    //i = Count - 1;      // ist nur ein Controll vorhanden
                }
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrAufträge))
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    i = Count - 1;      // ist nur ein Controll vorhanden
                }
            }
            //und Dispokaldender schliessen
            this.CloseDispoKaledner();
        }
        ///<summary>ctrMenü / label3_Click</summary>
        ///<remarks></remarks>
        private void label3_Click(object sender, EventArgs e)
        {
            OpenCtrAuftraege();
        }
        ///<summary>ctrMenü / OpenCtrAuftraege</summary>
        ///<remarks></remarks> 
        private void OpenCtrAuftraege()
        {
            if (Status == EStatus.Spedition)
            {
                bool open = false;
                Int32 j = -1;

                for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
                {
                    if (this.ParentForm.Controls[i].GetType() == typeof(ctrAufträge))
                    {
                        open = true;
                        j = i;
                    }
                }
                if (open)
                {
                    this.ParentForm.Controls[j].Show();
                }
                else
                {
                    // Auftragsliste generieren und anzeigen
                    Thread OpenThread = new Thread(this.OpenCtrAuftrag);
                    OpenThread.Start();

                    CreateSplitter(const_TMP + "SplitterAuftrag");
                }
            }
        }
        ///<summary>ctrMenü / ctrAuftrag_OpenKalender</summary>
        ///<remarks></remarks> 
        void ctrAuftrag_OpenKalender(frmDispoKalender _Kalender, DateTime _From, DateTime _To)
        {
            DispoKalenderOpen();
        }
        //******************************************* FAKTURIEUNG *********************************
        ///<summary>ctrMenü / btnFakturierung_Click</summary>
        ///<remarks></remarks> 
        private void btnFakturierung_Click()
        {
            Status = EStatus.Fakturierung;

            if (Status != EStatus.Fakturierung)
            {
                for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
                {
                    try
                    {
                        if (this.ParentForm.Controls[i].Name == const_TMP + "Splitter")
                        {
                            this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                        }
                        if (this.ParentForm.Controls[i].GetType() == typeof(ctrFaktSpedition))
                        {
                            this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                        }
                    }
                    catch
                    {
                    }
                }
            }
            Status = EStatus.Fakturierung;
        }
        ///<summary>ctrMenü / SetMouseHoverColorUntermenu</summary>
        ///<remarks></remarks> 
        public void SetMouseHoverColorUntermenu()
        {
            UntermenuBackcolor = Color.Orange;
            UntermenuForecolor = Color.White;
        }
        ///<summary>ctrMenü / ResetMouseHoverColorUnternmu</summary>
        ///<remarks></remarks> 
        public void ResetMouseHoverColorUnternmu()
        {
            UntermenuBackcolor = Color.White;
            UntermenuForecolor = Color.Black;
        }
        ///<summary>ctrMenü / SetColorToUntermenu</summary>
        ///<remarks></remarks> 
        private void SetColorToUntermenu(Label label, bool boReset)
        {
            if (boReset)
            {
                ResetMouseHoverColorUnternmu();
            }
            else
            {
                SetMouseHoverColorUntermenu();
            }

            label.BackColor = UntermenuBackcolor;
            label.ForeColor = UntermenuForecolor;
        }
        /*****************************************************************************************+
         *                                           System 
         * ***************************************************************************************/
        ///<summary>ctrMenü / lUserverwaltung_Click</summary>
        ///<remarks></remarks>    
        private void lUserverwaltung_Click(object sender, EventArgs e)
        {
            OpenUserverwaltung();
        }
        ///<summary>ctrMenü / OpenUserverwaltung</summary>
        ///<remarks></remarks>
        private void OpenUserverwaltung()
        {
            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmUserverwaltung)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmUserverwaltung));
            }

            frmUserverwaltung user = new frmUserverwaltung();
            user.GL_User = GL_User;
            user.SetGLUser += new frmUserverwaltung.frmUserverwaltungEventHandler(user_SetGLUser);
            user.StartPosition = FormStartPosition.CenterScreen;
            user.Show();
            user.BringToFront();

        }
        ///<summary>ctrMenü / user_SetGLUser</summary>
        ///<remarks></remarks>
        public void user_SetGLUser(Globals._GL_USER _GL_User)
        {
            GL_User = _GL_User;
            SetGLUser(GL_User);
        }
        ///<summary>ctrMenü / OpenLogbuch</summary>
        ///<remarks></remarks>
        public void OpenLogbuch(decimal Mode)
        {
            bool open = false;
            Int32 j = -1;

            for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrLogbuch))
                {
                    open = true;
                    j = i;
                }
            }
            if (open)
            {
                this.ParentForm.Controls[j].Show();
            }
            else
            {
                // Logbuch generieren
                ctrLogbuch ctrlog = new ctrLogbuch(Mode);
                ctrlog.GL_User = GL_User;
                ctrlog.Dock = System.Windows.Forms.DockStyle.Left;
                ctrlog.Name = const_TMP + "Logbuch";
                this.ParentForm.Controls.Add(ctrlog);
                this.ParentForm.Controls.SetChildIndex(ctrlog, 0);
                ctrlog.Show();

                CreateSplitter(const_TMP + "SplitterLogbuch");
            }
        }
        ///<summary>ctrMenü / OpenLogCOM</summary>
        ///<remarks></remarks>
        public void OpenLogCOM()
        {
            bool open = false;
            Int32 j = -1;

            for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrComLog))
                {
                    open = true;
                    j = i;
                }
            }
            if (open)
            {
                this.ParentForm.Controls[j].Show();
            }
            else
            {
                // Logbuch generieren
                _ctrComLog = new ctrComLog();
                _ctrComLog.GL_User = GL_User;
                _ctrComLog.Dock = System.Windows.Forms.DockStyle.Left;
                _ctrComLog.Name = const_TMP + "COMLog";
                _ctrComLog._ctrMenu = this;
                this.ParentForm.Controls.Add(_ctrComLog);
                this.ParentForm.Controls.SetChildIndex(_ctrComLog, 0);
                _ctrComLog.Show();

                CreateSplitter(const_TMP + "SplitterCOMLog");
            }
        }
        ///<summary>ctrMenü / CloseCtrBestand</summary>
        ///<remarks>.</remarks>
        public void CloseCtrCOMLog()
        {
            for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].Name == const_TMP + "SplitterCOMLog")
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    //Count = this.ParentForm.Controls.Count;
                    //i = 0;      // ist nur ein Controll vorhanden
                }
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrComLog))
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    //i = Count - 1;      // ist nur ein Controll vorhanden
                }
            }
        }
        ///<summary>ctrMenü / lLogout_Click</summary>
        ///<remarks></remarks>
        private void lLogout_Click(object sender, EventArgs e)
        {
            Sped4Close();
        }

        /**************************************************************************************************
         *                                            Statistik
         * ************************************************************************************************/
        ///<summary>ctrMenu / btnStatistik_Click</summary>
        ///<remarks></remarks>
        private void btnStatistik_Click()
        {
            Status = EStatus.Statistik;
        }
        ///<summary>ctrMenü / lStatLager_Click</summary>
        ///<remarks>Statistik Lager öffnen</remarks>
        private void lStatLager_Click(object sender, EventArgs e)
        {
            OpenCtrStatistik(enumStatistikArt.Lager.ToString());
        }
        ///<summary>ctrMenü / lStatFaktLager_Click</summary>
        ///<remarks>Statistik Fakturierung Lager öffnen</remarks>
        private void lStatFaktLager_Click(object sender, EventArgs e)
        {
            OpenCtrStatistik(enumStatistikArt.RGLager.ToString());
        }
        ///<summary>ctrMenü / lStatFaktDispo_Click</summary>
        ///<remarks>Statistik Fakturierung Disposition öffnen</remarks>
        private void lStatFaktDispo_Click(object sender, EventArgs e)
        {
            OpenCtrStatistik(enumStatistikArt.RGDispo.ToString());
        }
        ///<summary>ctrMenü / OpenCtrStatistik</summary>
        ///<remarks>.</remarks>
        public void OpenCtrStatistik(string strStatArt)
        {
            bool open = false;
            Int32 j = -1;
            for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrStatistik))
                {
                    open = true;
                    j = i;
                }
            }
            if (open)
            {
                this.ParentForm.Controls[j].Show();
            }
            else
            {
                _ctrStatistik = new ctrStatistik();
                _ctrStatistik._ctrMenu = this;
                _ctrStatistik.Dock = DockStyle.Left;
                _ctrStatistik.Name = const_TMP + "Statistik";
                _ctrStatistik.strStatistikArt = strStatArt;
                _ctrStatistik.InitCtr();
                this.ParentForm.Controls.Add(_ctrStatistik);
                this.ParentForm.Controls.SetChildIndex(_ctrStatistik, 0);
                _ctrStatistik.Show();
                CreateSplitter(const_TMP + "SplitterStatistik");
            }
        }
        ///<summary>ctrMenü / CloseCtrStatistik</summary>
        ///<remarks>.</remarks>
        public void CloseCtrStatistik()
        {
            Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].Name == const_TMP + "SplitterStatistik")
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    Count = this.ParentForm.Controls.Count;
                    i = 0;      // ist nur ein Controll vorhanden
                }
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrStatistik))
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    i = Count - 1;      // ist nur ein Controll vorhanden
                }
            }
        }
        /****************************************************************************************************
         *                                          Lager
         * **************************************************************************************************/
        ///<summary>ctrMenu / btnLager_Click</summary>
        ///<remarks></remarks>
        private void btnLager_Click()
        {
            Status = EStatus.Lager;
        }
        ///<summary>ctrMenü / lAuslagerung_Click</summary>
        ///<remarks></remarks>
        private void lAuslagerung_Click(object sender, EventArgs e)
        {
            object obj = new object();
            OpenCtrAuslagerung(obj);
        }
        ///<summary>ctrMenü / lLagerliste_Click</summary>
        ///<remarks></remarks>
        private void lLagerliste_Click(object sender, EventArgs e)
        {
            //Baustelle
        }
        ///<summary>ctrMenü / lLagerOrt_Click</summary>
        ///<remarks>Update eines Datensatzes in der DB über die ID.</remarks>
        private void lLagerOrt_Click(object sender, EventArgs e)
        {
            OpenCtrLagerOrt();
        }
        ///<summary>ctrMenü / lEinlagerung_Click</summary>
        ///<remarks></remarks>
        private void lEinlagerung_Click(object sender, EventArgs e)
        {
            object obj = new object();
            OpenCtrEinlagerung(obj);
        }
        ///<summary>ctrMenü / label1_Click_1</summary>
        ///<remarks>Umbuchung</remarks>
        private void label1_Click_1(object sender, EventArgs e)
        {
            object obj = this._ctrUmbuchung;
            this._ctrEinlagerung = new ctrEinlagerung();
            OpenCtrUmbuchungInFrm(obj, this._ctrEinlagerung);
        }
        ///<summary>ctrMenü / lJournal_Click</summary>
        ///<remarks>Journal</remarks>
        private void lJournal_Click(object sender, EventArgs e)
        {
            OpenCtrJournal();
        }
        ///<summary>ctrMenü / lBestandsliste_Click</summary>
        ///<remarks>Bestandsliste</remarks>
        private void lBestandsliste_Click(object sender, EventArgs e)
        {
            OpenCtrBestand();
        }
        //************************************************************************ Sperrlager
        ///<summary>ctrMenü / lSPL_Click</summary>
        ///<remarks>Sperrlager</remarks>
        private void lSPL_Click(object sender, EventArgs e)
        {
            OpenCtrSperrlager();
        }
        ///<summary>ctrMenü / OpenCtrSperrlager</summary>
        ///<remarks>.</remarks>
        public void OpenCtrSperrlager()
        {
            bool open = false;
            Int32 j = -1;
            for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrSperrlager))
                {
                    open = true;
                    j = i;
                }
            }
            if (open)
            {
                this.ParentForm.Controls[j].Show();
            }
            else
            {
                _ctrSperrlager = new ctrSperrlager();
                _ctrSperrlager._ctrMenu = this;
                _ctrSperrlager.GL_User = this.GL_User;
                _ctrSperrlager.Dock = DockStyle.Left;
                _ctrSperrlager.Name = const_TMP + "Sperrlager";
                this.ParentForm.Controls.Add(_ctrSperrlager);
                this.ParentForm.Controls.SetChildIndex(_ctrSperrlager, 0);
                _ctrSperrlager.Show();
                CreateSplitter(const_TMP + "SplitterSperrlager");
            }
        }
        ///<summary>ctrMenü / CloseCtrSperrlager</summary>
        ///<remarks>.</remarks>
        public void CloseCtrSperrlager()
        {
            Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].Name == const_TMP + "SplitterSperrlager")
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    Count = this.ParentForm.Controls.Count;
                    i = 0;      // ist nur ein Controll vorhanden
                }
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrSperrlager))
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    i = Count - 1;      // ist nur ein Controll vorhanden
                }
            }
        }
        //************************************************************************ Artikelsuche
        ///<summary>ctrMenü / lArtSearch_Click</summary>
        ///<remarks>Artikelsuche</remarks>
        private void lArtSearch_Click(object sender, EventArgs e)
        {
            object _ctrTmpSearch = new ctrSearch();
            OpenCtrEinlagerung(_ctrTmpSearch);
            OpenCtrSearch(this._ctrSearch);
        }
        //***************************************************************************** Userlisten
        ///<summary>ctrMenü / lUserList_Click</summary>
        ///<remarks>User - Listen</remarks>
        private void lUserList_Click(object sender, EventArgs e)
        {
            OpenCtrUserList();
        }
        ///<summary>ctrMenü / OpenCtrUserList</summary>
        ///<remarks>.</remarks>
        public void OpenCtrUserList()
        {
            bool open = false;
            Int32 j = -1;
            for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrUserList))
                {
                    open = true;
                    j = i;
                }
            }
            if (open)
            {
                this.ParentForm.Controls[j].Show();
            }
            else
            {
                _ctrUserList = new ctrUserList();
                _ctrUserList._ctrMenu = this;
                _ctrUserList.GL_User = this.GL_User;
                _ctrUserList.Dock = DockStyle.Left;
                _ctrUserList.Name = const_TMP + "UserList";
                this.ParentForm.Controls.Add(_ctrUserList);
                this.ParentForm.Controls.SetChildIndex(_ctrUserList, 0);
                _ctrUserList.Show();
                CreateSplitter(const_TMP + "SplitterUserList");
            }
        }
        ///<summary>ctrMenü / CloseCtrUserList</summary>
        ///<remarks>.</remarks>
        public void CloseCtrUserList()
        {
            Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].Name == const_TMP + "SplitterUserList")
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    Count = this.ParentForm.Controls.Count;
                    i = 0;      // ist nur ein Controll vorhanden
                }
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrUserList))
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    i = Count - 1;      // ist nur ein Controll vorhanden
                }
            }
        }
        ///<summary>ctrMenü / CloseCtrUserList</summary>
        ///<remarks></remarks>
        public void OpenCtrPrintLagerInFrm(object myObj)
        {
            this._ctrPrintLager = new ctrPrintLager();

            if (typeof(ctrEinlagerung) == myObj.GetType())
            {
                this._ctrPrintLager._ctrEinlagerung = (ctrEinlagerung)myObj;
            }
            if (typeof(ctrAuslagerung) == myObj.GetType())
            {
                this._ctrPrintLager._ctrAuslagerung = (ctrAuslagerung)myObj;
            }
            object obj = this._ctrPrintLager;
            OpenFrmTMP(obj);
        }
        ///<summary>ctrMenü / CloseCtrUserList</summary>
        ///<remarks></remarks>
        public void OpenCtrMailCockpitInFrm(object myObj)
        {
            //LogMessages.Add(" > Paremter:");
            //LogMessages.Add(string.Format("{0,0} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}"," >", "mySourcePath", mySourcePath));
            this._ctrMailCockpit = new ctrMailCockpit();
            //null
            this._ctrJournal = null;
            this._ctrBestand = null;
            this._ctrFreeForCall = null;
            this._ctrSPLAdd = null;
            this._ctrFaktLager = null;
            this._ctrRGList = null;

            //check My Object
            if (typeof(ctrJournal) == myObj.GetType())
            {
                this._ctrJournal = (ctrJournal)myObj;
            }
            if (typeof(ctrBestand) == myObj.GetType())
            {
                this._ctrBestand = (ctrBestand)myObj;
            }
            if (typeof(ctrFreeForCall) == myObj.GetType())
            {
                this._ctrFreeForCall = (ctrFreeForCall)myObj;
            }
            if (typeof(ctrSPLAdd) == myObj.GetType())
            {
                this._ctrSPLAdd = (ctrSPLAdd)myObj;
                enumDokumentenArt tmpDocArt = enumDokumentenArt.DEFAULT;
                Enum.TryParse(this._ctrSPLAdd.DokumentenArt, out tmpDocArt);
                this._ctrMailCockpit.eDocumentArt = tmpDocArt;
            }
            if (typeof(ctrFaktLager) == myObj.GetType())
            {
                this._ctrFaktLager = (ctrFaktLager)myObj;
            }
            if (typeof(ctrRGList) == myObj.GetType())
            {
                this._ctrRGList = (ctrRGList)myObj;
            }
            object obj = this._ctrMailCockpit;
            OpenFrmTMP(obj);
        }
        ///<summary>ctrMenü / OpenFrmReporView</summary>
        ///<remarks></remarks>
        public void OpenFrmReporView(object obj, bool bPrintDirect, bool bCountFromGui = false)
        {
            if (obj != null)
            {
                frmPrintRepViewer prView = new frmPrintRepViewer();
                prView.GL_User = this.GL_User;
                prView.GL_System = this._frmMain.GL_System;
                //Lager
                if (obj.GetType() == typeof(ctrPrintLager))
                {
                    prView._ctrPrintLager = (ctrPrintLager)obj;
                }
                if (obj.GetType() == typeof(ctrReihen))
                {
                    prView._ctrReihen = (ctrReihen)obj;
                }
                if (obj.GetType() == typeof(ctrPrintLagerOrtLabel))
                {
                    prView._ctrPrintLagerOrtLabel = (ctrPrintLagerOrtLabel)obj;
                }
                //Bestandsliste
                if (obj.GetType() == typeof(ctrBestand))
                {
                    prView._ctrBestand = (ctrBestand)obj;
                }
                //Journal
                if (obj.GetType() == typeof(ctrJournal))
                {
                    prView._ctrJournal = (ctrJournal)obj;
                }
                //Fakturierung LAGER
                if (obj.GetType() == typeof(ctrFaktLager))
                {
                    prView._ctrFaktLager = (ctrFaktLager)obj;
                }
                if (obj.GetType() == typeof(ctrRGList))
                {
                    prView._ctrRGList = (ctrRGList)obj;
                }
                if (obj.GetType() == typeof(ctrRGManuell))
                {
                    prView._ctrRGManuell = (ctrRGManuell)obj;
                }
                prView.InitFrm(bCountFromGui);

                if (bPrintDirect)
                {
                    prView.Hide();
                    prView.PrintDirect();
                    if (_frmMain.system.Client.Modul.Archiv)
                    {
                        prView.AddToArchiv();
                    }
                    prView.Close();
                }
                else
                {
                    prView.StartPosition = FormStartPosition.CenterParent;
                    prView.Refresh();
                    prView.Show();
                    prView.BringToFront();
                }
            }
        }
        /****************************************************************
         *              Untermenü fabrlich hinterlegen 
         * *************************************************************/
        ///<summary>ctrMenü /LabelEvent_MouseHover</summary>
        ///<remarks></remarks>
        private void LabelEvent_MouseHover(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            SetColorToUntermenu(label, false);
        }
        ///<summary>ctrMenü /LabelEvent_MouseLeave</summary>
        ///<remarks></remarks>
        private void LabelEvent_MouseLeave(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            SetColorToUntermenu(label, true);
        }
        ///<summary>ctrMenü /CtrResizeALL</summary>
        ///<remarks>splitter Resize</remarks>
        public void SplitterResize()
        {
            Int32 iSplitterWidth = 1;

            for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].GetType() == typeof(Splitter))
                {
                    this.ParentForm.Controls[i].Width = iSplitterWidth;
                    this.ParentForm.Controls[i].Refresh();
                }
            }
        }
        ///<summary>ctrMenü /CtrResizeALL</summary>
        ///<remarks></remarks>
        public void CtrResizeALL()
        {
            for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrMenu))
                {
                    ctrMenu tmpCtr = (ctrMenu)this.ParentForm.Controls[i];
                    tmpCtr.SplitterResize();
                }
                //ADR
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrADR_List))
                {
                    ctrADR_List tmpCtr = (ctrADR_List)this.ParentForm.Controls[i];
                    //tmpCtr.ctrADRListe_Resize();
                }
                //Auftrag
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrAufträge))
                {
                    ctrAufträge tmpCtr = (ctrAufträge)this.ParentForm.Controls[i];
                    //tmpCtr.ctrAuftrag_Resize();
                }
                //Fahrzeuge
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrFahrzeug_List))
                {
                    ctrFahrzeug_List tmpCtr = (ctrFahrzeug_List)this.ParentForm.Controls[i];
                    tmpCtr.ctrFahrzeugListe_Resize();
                }
                //Güterarten
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrGueterArtListe))
                {
                    //ctrGueterArtListe tmpCtr = (ctrGueterArtListe)this.ParentForm.Controls[i];
                    //tmpCtr.ctrGArtenListe_Resize();
                }
                //Personal
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrPersonal_List))
                {
                    ctrPersonal_List tmpCtr = (ctrPersonal_List)this.ParentForm.Controls[i];
                    tmpCtr.ctrPersonalListe_Resize();
                }
                //        

            }
        }
        ///<summary>ctrMenü / CreateSplitter</summary>
        ///<remarks>             * Beispiel Splittername
        ///     * - const_TMP + "SplitterLager";
        ///     * - const_TMP + "SplitterLogbuch";
        ///     * - const_TMP + "SplitterFakturierung";
        ///     * - Temp" + "SplitterAuftrag
        ///     * - const_TMP + "SplitterDispo";
        ///     * - const_TMP + "SplitterADRDetails"
        ///     * - const_TMP + "SplitterLagerOrt"</remarks>
        public void CreateSplitter(string strSplitterName)
        {

            // Splitter zur Auftragsliste generieren und anzeigen
            Splitter splitter = new Splitter();
            splitter.BackColor = Sped4.Properties.Settings.Default.EffectColor;
            splitter.BorderStyle = BorderStyle.None;
            splitter.Dock = DockStyle.Left;
            splitter.Name = strSplitterName;
            this.ParentForm.Controls.Add(splitter);
            this.ParentForm.Controls.SetChildIndex(splitter, 0);
            splitter.Show();

            SplitterResize();
        }
        /**********************************************************************************************
         *                          Global Open Forms 
         * ********************************************************************************************/
        //************************************************************************* Lagerort 
        ///<summary>ctrMenü / OpenCtrLagerOrt</summary>
        ///<remarks>.</remarks>
        public void OpenCtrLagerOrt()
        {
            bool open = false;
            Int32 j = -1;
            for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrLagerOrt))
                {
                    open = true;
                    j = i;
                }
            }
            if (open)
            {
                this.ParentForm.Controls[j].Show();
            }
            else
            {
                _ctrLagerOrt = new ctrLagerOrt();
                _ctrLagerOrt._ctrMenu = this;
                _ctrLagerOrt.GL_User = this.GL_User;
                _ctrLagerOrt.Dock = DockStyle.Left;
                _ctrLagerOrt.Name = "TmpLagerOrt";
                _ctrLagerOrt.bTakeOverLagerort = true;
                this.ParentForm.Controls.Add(_ctrLagerOrt);
                this.ParentForm.Controls.SetChildIndex(_ctrLagerOrt, 0);
                _ctrLagerOrt.Show();

                CreateSplitter(const_TMP + "SplitterLogbuch");
            }
        }
        ///<summary>ctrMenü / OpenCtrPrintLagerOrtLabel</summary>
        ///<remarks>.</remarks>
        public void OpenCtrPrintLagerOrtLabel()
        {
            bool open = false;
            Int32 j = -1;
            for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrPrintLagerOrtLabel))
                {
                    open = true;
                    j = i;
                }
            }
            if (open)
            {
                this.ParentForm.Controls[j].Show();
            }
            else
            {
                _ctrPrintLagerOrtLabel = new ctrPrintLagerOrtLabel();
                _ctrPrintLagerOrtLabel._ctrMenu = this;
                _ctrPrintLagerOrtLabel.GL_User = this.GL_User;
                _ctrPrintLagerOrtLabel.Dock = DockStyle.Left;
                _ctrPrintLagerOrtLabel.Name = "TmpLagerOrtLabel";
                this.ParentForm.Controls.Add(_ctrPrintLagerOrtLabel);
                this.ParentForm.Controls.SetChildIndex(_ctrPrintLagerOrtLabel, 0);
                _ctrPrintLagerOrtLabel.Show();

                CreateSplitter(const_TMP + "SplitterLogbuch");
            }
        }

        ///<summary>ctrMenü / OpenCtrLagerOrtInFrm</summary>
        ///<remarks>.</remarks>
        public void OpenCtrLagerOrtInFrm(bool myBIsLagerOrtSelection, object myTakeOverObj)
        {
            this._ctrLagerOrt = new ctrLagerOrt();
            this._ctrLagerOrt.bTakeOverLagerort = myBIsLagerOrtSelection;
            if (myTakeOverObj.GetType() == typeof(ctrEinlagerung))
            {
                this._ctrEinlagerung = (ctrEinlagerung)myTakeOverObj;
            }
            object obj = this._ctrLagerOrt;
            OpenFrmTMP(obj);
        }

        ///<summary>ctrMenü / OpenCtrLagerOrt</summary>
        ///<remarks>.</remarks>
        public void OpenCtrLagerreihen()
        {
            bool open = false;
            Int32 j = -1;
            for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrReihen))
                {
                    open = true;
                    j = i;
                }
            }
            if (open)
            {
                this.ParentForm.Controls[j].Show();
            }
            else
            {
                _ctrLagerReihen = new ctrReihen();
                _ctrLagerReihen._ctrMenu = this;
                _ctrLagerReihen.GL_User = this.GL_User;
                _ctrLagerReihen.Dock = DockStyle.Left;
                _ctrLagerReihen.Name = "TmpLagerreihe";
                // _ctrLagerReihen.bTakeOverLagerort = true;
                this.ParentForm.Controls.Add(_ctrLagerReihen);
                this.ParentForm.Controls.SetChildIndex(_ctrLagerReihen, 0);
                _ctrLagerReihen.Show();

                CreateSplitter(const_TMP + "SplitterLagerreihe");
            }
        }


        //****************************************************************** Einlagerung
        ///<summary>ctrMenü / OpenCtrLagerOrt</summary>
        ///<remarks>.</remarks>
        public void OpenCtrEinlagerung(object obj, bool bFromBestand = false)
        {
            bool open = false;
            Int32 j = -1;
            for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrEinlagerung))
                {
                    open = true;
                    j = i;
                }
            }
            if (open)
            {
                if (obj.GetType() == typeof(ctrSearch))
                {
                    this._ctrEinlagerung = (ctrEinlagerung)this.ParentForm.Controls[j];
                }
                else
                {
                    this.ParentForm.Controls[j].Show();
                }
            }
            else
            {
                _ctrEinlagerung = new ctrEinlagerung();
                _ctrEinlagerung._ctrMenu = this;
                _ctrEinlagerung._ctrMenu._frmMain.ResetStatusBar();
                _ctrEinlagerung._ctrMenu._frmMain.InitStatusBar(4);
                _ctrEinlagerung._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
                _ctrEinlagerung._ctrMenu._frmMain.StatusBarWork(false, "Lagereingang wird geöffnet...");

                _ctrEinlagerung.Dock = DockStyle.Left;
                _ctrEinlagerung.Name = const_TMP + "Einlagerung";
                _ctrEinlagerung._frmTmp = null;
                _ctrEinlagerung._ctrMenu._frmMain.StatusBarWork(false, "Lagereingang wird geöffnet...");
                //_ctrEinlagerung.InitCtrEinlagerung();
                _ctrEinlagerung.bFromBestand = bFromBestand;
                this.ParentForm.Controls.Add(_ctrEinlagerung);
                this.ParentForm.Controls.SetChildIndex(_ctrEinlagerung, 0);
                this._frmMain.ctrFormList.AddCtrToList(ref this._frmMain);

                _ctrEinlagerung.InitCtrEinlagerung();

                if (obj.GetType() == typeof(ctrSearch))
                {
                    this._ctrSearch = (ctrSearch)obj;
                    _ctrEinlagerung.Visible = false;
                    CreateSplitter(const_TMP + "SplitterEinlagerung");
                    _ctrEinlagerung._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
                    _ctrEinlagerung._ctrMenu._frmMain.ResetStatusBar();
                    this._ctrSearch = (ctrSearch)obj;
                    this._ctrSearch._ctrEinlagerung = this._ctrEinlagerung;
                }
                else
                {
                    _ctrEinlagerung.Visible = true;
                    _ctrEinlagerung.Show();
                    CreateSplitter(const_TMP + "SplitterEinlagerung");
                    _ctrEinlagerung._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
                    _ctrEinlagerung._ctrMenu._frmMain.ResetStatusBar();
                }
            }
        }
        ///<summary>ctrMenü / OpenCtrEinlagerungInFrm</summary>
        ///<remarks>.</remarks>
        public void OpenCtrEinlagerungInFrm()
        {
            this._ctrEinlagerung = new ctrEinlagerung();
            object obj = this._ctrEinlagerung;
            OpenFrmTMP(obj);
        }
        ///<summary>ctrMenü / CloseCtrEinlagerung</summary>
        ///<remarks>.</remarks>
        public void CloseCtrEinlagerung()
        {
            Int32 Count = this.ParentForm.Controls.Count;
            if (_ctrEinlagerung != null && _ctrEinlagerung.bFromBestand == true)
            {
                this._ctrEinlagerung.Dispose();
                //OpenCtrBestand();
            }
        }
        ///<summary>ctrMenü / CloseCtrAuslagerungFrmTmp</summary>
        ///<remarks>.</remarks>
        public void CloseCtrEinlagerungFrmTmp()
        {
            foreach (Form OpenForm in Application.OpenForms)
            {
                if (OpenForm.GetType() == typeof(frmTmp))
                {
                    if (((frmTmp)OpenForm)._ctrEinlagerung != null)
                    {
                        ((frmTmp)OpenForm).CloseFrmTmp();
                    }
                }
            }
        }
        //****************************************************************** Auslagerung
        ///<summary>ctrMenü / OpenCtrLagerOrt</summary>
        ///<remarks>.</remarks>
        public void OpenCtrAuslagerung(object obj, bool bFromBestand = false)
        {
            bool open = false;
            Int32 j = -1;
            for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrAuslagerung))
                {
                    open = true;
                    j = i;
                }
            }
            if (open)
            {
                this.ParentForm.Controls[j].Show();
            }
            else
            {
                _ctrAuslagerung = new ctrAuslagerung();
                _ctrAuslagerung._ctrMenu = this;

                _ctrAuslagerung._ctrMenu._frmMain.ResetStatusBar();
                _ctrAuslagerung._ctrMenu._frmMain.InitStatusBar(4);
                _ctrAuslagerung._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
                _ctrAuslagerung._ctrMenu._frmMain.StatusBarWork(false, "Lagerausgang wird geöffnet...");
                _ctrAuslagerung.Dock = DockStyle.Left;
                _ctrAuslagerung.Name = const_TMP + "Auslagerung";
                _ctrAuslagerung._frmTmp = null;
                _ctrAuslagerung._ctrMenu._frmMain.StatusBarWork(false, "Lagerausgang wird geöffnet...");
                _ctrAuslagerung.InitCtrAuslagerung();
                this.ParentForm.Controls.Add(_ctrAuslagerung);
                this.ParentForm.Controls.SetChildIndex(_ctrAuslagerung, 0);
                _ctrAuslagerung.bFromBestand = bFromBestand;
                _ctrAuslagerung.Show();
                CreateSplitter(const_TMP + "SplitterAuslagerung");
                _ctrAuslagerung._ctrMenu._frmMain.StatusBarWork(false, string.Empty);

                _ctrAuslagerung._ctrMenu._frmMain.ResetStatusBar();
            }
        }
        ///<summary>ctrMenü / OpenCtrLagerOrtInFrm</summary>
        ///<remarks>.</remarks>
        public void OpenCtrAuslagerungInFrm(object myObj)
        {
            if (myObj.GetType() == typeof(ctrAuslagerung))
            {
                this._ctrAuslagerung = (ctrAuslagerung)myObj;
            }
            else
            {
                this._ctrAuslagerung = new ctrAuslagerung();
            }
            object obj = this._ctrAuslagerung;
            OpenFrmTMP(obj);
        }
        ///<summary>ctrMenü / CloseCtrAuslagerung</summary>
        ///<remarks>.</remarks>
        public void CloseCtrAuslagerung()
        {
            Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i] != null)
                {
                    if (this.ParentForm.Controls[i].Name == const_TMP + "SplitterAuslagerung")
                    {
                        this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                        Count = this.ParentForm.Controls.Count;
                        i = 0;      // ist nur ein Controll vorhanden
                    }
                    if (this.ParentForm.Controls[i].GetType() == typeof(ctrAuslagerung))
                    {
                        this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                        i = Count - 1;      // ist nur ein Controll vorhanden
                    }
                }
            }
        }
        ///<summary>ctrMenü / CloseCtrADR_List</summary>
        ///<remarks>.</remarks>
        public void CloseCtrADR_List(bool bClose = true)
        {
            Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i] != null)
                {
                    if (this.ParentForm.Controls[i].Name == const_TMP + "SplitterAdressen")
                    {
                        this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                        Count = this.ParentForm.Controls.Count;
                        i = 0;      // ist nur ein Controll vorhanden
                    }
                    if (this.ParentForm.Controls[i].GetType() == typeof(ctrADR_List))
                    {
                        if (bClose)
                            this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                        else
                            this.ParentForm.Controls[i].Hide();
                        i = Count - 1;      // ist nur ein Controll vorhanden
                    }
                }
            }
        }
        ///<summary>ctrMenü / CloseCtrAuslagerungFrmTmp</summary>
        ///<remarks>.</remarks>
        public void CloseCtrAuslagerungFrmTmp()
        {
            foreach (Form OpenForm in Application.OpenForms)
            {
                if (OpenForm.GetType() == typeof(frmTmp))
                {
                    if (((frmTmp)OpenForm)._ctrAuslagerung != null)
                    {
                        ((frmTmp)OpenForm).CloseFrmTmp();
                    }
                }
            }
        }
        //******************************************************************  Freigabe Bestand
        ///<summary>ctrMenü / lLagerFreeForCall_Click</summary>
        ///<remarks>.</remarks>
        private void lLagerFreeForCall_Click(object sender, EventArgs e)
        {
            OpenCtrFreeForCall();
        }
        ///<summary>ctrMenü / OpenCtrFreeForCall</summary>
        ///<remarks>.</remarks>
        public void OpenCtrFreeForCall()
        {
            bool open = false;
            Int32 j = -1;
            for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrFreeForCall))
                {
                    open = true;
                    j = i;
                }
            }
            if (open)
            {
                this.ParentForm.Controls[j].Show();
            }
            else
            {
                _ctrFreeForCall = new ctrFreeForCall();
                _ctrFreeForCall.InitGlobals(this);
                _ctrFreeForCall.Dock = DockStyle.Left;
                _ctrFreeForCall.Name = const_TMP + "FreeForCall";
                _ctrFreeForCall._frmTmp = null;
                this.ParentForm.Controls.Add(_ctrFreeForCall);
                this.ParentForm.Controls.SetChildIndex(_ctrFreeForCall, 0);
                _ctrFreeForCall.Show();
                CreateSplitter(const_TMP + "SplitterFreeForCall");
            }
        }
        ///<summary>ctrMenü / OpenCtrSendToSPL</summary>
        ///<remarks>.</remarks>
        public void OpenCtrSendToSPL()
        {
            bool open = false;
            Int32 j = -1;
            for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrSendToSPL))
                {
                    open = true;
                    j = i;
                }
            }
            if (open)
            {
                this.ParentForm.Controls[j].Show();
            }
            else
            {
                _ctrSendToSPL = new ctrSendToSPL();
                //_ctrSendToSPL.InitGlobals(this);
                _ctrSendToSPL.Dock = DockStyle.Left;
                _ctrSendToSPL.Name = const_TMP + "FreeForCall";
                //_ctrSendToSPL._frmTmp = null;
                this.ParentForm.Controls.Add(_ctrSendToSPL);
                this.ParentForm.Controls.SetChildIndex(_ctrSendToSPL, 0);
                _ctrSendToSPL.Show();
                CreateSplitter(const_TMP + "SplitterSendToSPL");
            }
        }
        //********************************************************************************************* Post Center
        ///<summary>ctrMenü / lPostCenter_Click</summary>
        ///<remarks>.</remarks>
        private void lPostCenter_Click(object sender, EventArgs e)
        {
            this._ctrPostCenter = new ctrPostCenter();
            object obj = (object)this._ctrPostCenter;
            OpenFrmTMP(obj);
        }
        //*********************************************************************************************** Umbuchung
        ///<summary>ctrMenü / OpenCtrUmbuchungInFrm</summary>
        ///<remarks>.</remarks>
        public void OpenCtrFreeForCallInFrm(object myObj, ctrFreeForCall myCtrFreeForCall)
        {
            this._ctrFreeForCall = new ctrFreeForCall();
            this._ctrFreeForCall.InitGlobals(this);

            object obj = this._ctrUmbuchung;
            OpenFrmTMP(obj);
        }
        ///<summary>ctrMenü / CloseCtrUmbuchung</summary>
        ///<remarks>.</remarks>
        public void CloseCtrFreeForCall()
        {
            Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i] != null)
                {
                    if (this.ParentForm.Controls[i].Name == const_TMP + "SplitterFreeForCall")
                    {
                        this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                        Count = this.ParentForm.Controls.Count;
                        i = 0;      // ist nur ein Controll vorhanden
                    }
                    if (this.ParentForm.Controls[i].GetType() == typeof(ctrFreeForCall))
                    {
                        this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                        i = Count - 1;      // ist nur ein Controll vorhanden
                    }
                }
            }
        }
        //****************************************************************** Umbuchung
        ///<summary>ctrMenü / OpenCtrLagerOrt</summary>
        ///<remarks>.</remarks>
        public void OpenCtrUmbuchung(ctrEinlagerung myCtrEinlagerung)
        {
            bool open = false;
            Int32 j = -1;
            for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrUmbuchung))
                {
                    open = true;
                    j = i;
                }
            }
            if (open)
            {
                this.ParentForm.Controls[j].Show();
            }
            else
            {
                _ctrUmbuchung = new ctrUmbuchung();
                if (myCtrEinlagerung != null)
                {
                    _ctrUmbuchung._ctrEinlagerung = myCtrEinlagerung;
                }
                _ctrUmbuchung._ctrMenu = this;
                _ctrUmbuchung.GL_User = this.GL_User;
                _ctrUmbuchung.Dock = DockStyle.Left;
                _ctrUmbuchung.Name = const_TMP + "Umbuchung";
                _ctrUmbuchung._frmTmp = null;
                _ctrUmbuchung.InitCtr();
                this.ParentForm.Controls.Add(_ctrUmbuchung);
                this.ParentForm.Controls.SetChildIndex(_ctrUmbuchung, 0);
                _ctrUmbuchung.Show();
                CreateSplitter(const_TMP + "SplitterUmbuchung");
            }
        }
        ///<summary>ctrMenü / OpenCtrUmbuchungInFrm</summary>
        ///<remarks>.</remarks>
        public void OpenCtrUmbuchungInFrm(object myObj, ctrEinlagerung myCtrEinlagerung)
        {
            this._ctrUmbuchung = new ctrUmbuchung();
            this._ctrUmbuchung._ctrEinlagerung = myCtrEinlagerung;
            this._ctrUmbuchung._ctrMenu = this;
            object obj = this._ctrUmbuchung;
            OpenFrmTMP(obj);
        }
        ///<summary>ctrMenü / CloseCtrUmbuchung</summary>
        ///<remarks>.</remarks>
        public void CloseCtrUmbuchung()
        {
            Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i] != null)
                {
                    if (this.ParentForm.Controls[i].Name == const_TMP + "SplitterUmbuchung")
                    {
                        this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                        Count = this.ParentForm.Controls.Count;
                        i = 0;      // ist nur ein Controll vorhanden
                    }
                    if (this.ParentForm.Controls[i].GetType() == typeof(ctrUmbuchung))
                    {
                        this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                        i = Count - 1;      // ist nur ein Controll vorhanden
                    }
                }
            }
        }
        ///<summary>ctrMenü / CloseCtrUmbuchungFrmTmp</summary>
        ///<remarks>.</remarks>
        public void CloseCtrUmbuchungFrmTmp()
        {
            foreach (Form OpenForm in Application.OpenForms)
            {
                if (OpenForm.GetType() == typeof(frmTmp))
                {
                    if (((frmTmp)OpenForm)._ctrUmbuchung != null)
                    {
                        ((frmTmp)OpenForm).CloseFrmTmp();
                        break;
                    }
                }
            }
        }
        ///<summary>ctrMenü / CloseCtrPrintLagerFrmTmp</summary>
        ///<remarks></remarks>
        public void CloseCtrPrintLagerFrmTmp()
        {
            foreach (Form OpenForm in Application.OpenForms)
            {
                if (OpenForm.GetType() == typeof(frmTmp))
                {
                    if (((frmTmp)OpenForm)._ctrPrintLager != null)
                    {
                        ((frmTmp)OpenForm).CloseFrmTmp();
                        break;
                    }
                }
            }
        }

        //****************************************************************** GüterArtenliste
        ///<summary>ctrMenü / OpenFrmGArtenList</summary>
        ///<remarks>.</remarks>
        public void OpenFrmGArtenList(object obj)
        {
            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmGArtenAuftragserfassung)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmGArtenAuftragserfassung));
            }
            frmGArtenAuftragserfassung gaAE = new frmGArtenAuftragserfassung();
            gaAE._ctrMenu = this;


            if (obj.GetType() == typeof(ctrReihen))
            {
                gaAE._ctrReihen = (ctrReihen)obj;
            }
            if (obj.GetType() == typeof(frmAuftrag_Fast))
            {
                gaAE._frmAuftragErfassung = (frmAuftrag_Fast)obj;
            }
            if (obj.GetType() == typeof(ctrEinlagerung))
            {
                gaAE._ctrEinlagerung = (ctrEinlagerung)obj;
            }
            if (obj.GetType() == typeof(ctrArtDetails))
            {
                gaAE._ctrArtDetails = (ctrArtDetails)obj;
            }
            //TarifErfassung
            if (obj.GetType() == typeof(ctrTarifErfassung))
            {
                gaAE._ctrTarifErfassung = (ctrTarifErfassung)obj;
            }
            //Search
            if (obj.GetType() == typeof(ctrSearch))
            {
                gaAE._ctrSearch = (ctrSearch)obj;
            }
            //Journal
            if (obj.GetType() == typeof(ctrJournal))
            {
                gaAE._ctrJournal = (ctrJournal)obj;
            }
            if (obj.GetType() == typeof(ctrADR_List))
            {
                gaAE._ctrAdrListGArtDefault = (ctrADR_List)obj;
            }
            if (obj.GetType() == typeof(ctrCustomProcessExcesption))
            {
                gaAE._ctrCustomProcessException = (ctrCustomProcessExcesption)obj;
            }

            gaAE.Show();
            gaAE.BringToFront();
        }
        ///<summary>ctrMenü / OpenCtrSearch</summary>
        ///<remarks>.</remarks>
        public void OpenCtrSearch(object myObj)
        {
            if (typeof(ctrSearch) != myObj.GetType())
            {
                this._ctrSearch = new ctrSearch();
            }
            //Umbuchung
            if (typeof(ctrUmbuchung) == myObj.GetType())
            {
                this._ctrSearch._ctrUmbuchung = (ctrUmbuchung)myObj;
            }
            //Einlagerung
            if (typeof(ctrEinlagerung) == myObj.GetType())
            {
                this._ctrSearch._ctrEinlagerung = (ctrEinlagerung)myObj;
            }
            //Auslagerung
            if (typeof(ctrAuslagerung) == myObj.GetType())
            {
                this._ctrSearch._ctrAuslagerung = (ctrAuslagerung)myObj;
            }
            object obj = this._ctrSearch;
            OpenFrmTMP(obj);
        }
        ///<summary>ctrMenü / OpenCtrSearch</summary>
        ///<remarks>.</remarks>
        public void OpenCtrFaktArtikelList(object myObj)
        {
            this._ctrFaktArtikelList = new ctrFaktArtikelList();
            //Auslagerung
            if (typeof(ctrFaktLager) == myObj.GetType())
            {
                this._ctrFaktArtikelList._ctrMenu = this;
                this._ctrFaktArtikelList._ctrFaktLager = (ctrFaktLager)myObj;
            }
            object obj = this._ctrFaktArtikelList;
            OpenFrmTMP(obj);
        }
        ///<summary>ctrMenü / OpenCtrExtraChargeAssignment</summary>
        ///<remarks></remarks>
        public void OpenCtrExtraChargeAssignment(object myObj)//ctrEinlagerung myCtrEinlagerung)
        {
            if (myObj.GetType() == typeof(ctrEinlagerung))
            {
                this._ctrExtraChargeAssignment = new ctrExtraChargeAssignment();
                this._ctrExtraChargeAssignment.Lager = ((ctrEinlagerung)myObj).Lager;
                this._ctrExtraChargeAssignment.ctrEinlagerung = (ctrEinlagerung)myObj;
                this._ctrExtraChargeAssignment.bExtraChargeAssignmentForArt = ((ctrEinlagerung)myObj).bSetExtraChargeAssignmentToArt;
                object obj = (object)this._ctrExtraChargeAssignment;
                OpenFrmTMP(obj);
            }
            if (myObj.GetType() == typeof(ctrArbeitsliste))
            {
                this._ctrExtraChargeAssignment = new ctrExtraChargeAssignment();
                //this._ctrExtraChargeAssignment.Lager = ((ctrArbeitsliste)myObj).Lager;
                this._ctrExtraChargeAssignment.ctrArbeitsliste = (ctrArbeitsliste)myObj;
                this._ctrExtraChargeAssignment.bExtraChargeAssignmentForArt = true;
                clsADR tmp = new clsADR();
                string mc = ((ctrArbeitsliste)myObj).dgvArbeitsliste.SelectedRows[0].Cells["Auftraggeber"].Value.ToString();
                this._ctrExtraChargeAssignment.AdrID = clsADR.GetIDByMatchcode(mc);
                object obj = (object)this._ctrExtraChargeAssignment;
                OpenFrmTMP(obj);
            }

        }
        //************************************************************************** Journal
        ///<summary>ctrMenü / OpenCtrJournal</summary>
        ///<remarks>.</remarks>
        public void OpenCtrJournal()
        {
            bool open = false;
            Int32 j = -1;
            for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrJournal))
                {
                    open = true;
                    j = i;
                }
            }
            if (open)
            {
                this.ParentForm.Controls[j].Show();
            }
            else
            {
                _ctrJournal = new ctrJournal();
                _ctrJournal._ctrMenu = this;
                _ctrJournal.GL_User = this.GL_User;
                _ctrJournal._ctrMenu._frmMain.ResetStatusBar();
                _ctrJournal._ctrMenu._frmMain.InitStatusBar(3);
                _ctrJournal._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
                _ctrJournal._ctrMenu._frmMain.StatusBarWork(false, "Die Journal wird geöffnet...");
                _ctrJournal.Dock = DockStyle.Left;
                _ctrJournal.Name = const_TMP + "Journal";
                _ctrJournal._frmTmp = null;
                this.ParentForm.Controls.Add(_ctrJournal);
                this.ParentForm.Controls.SetChildIndex(_ctrJournal, 0);
                this._frmMain.ctrFormList.AddCtrToList(ref this._frmMain);

                _ctrJournal.Show();
                CreateSplitter(const_TMP + "SplitterJournal");
                _ctrJournal._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
                _ctrJournal._ctrMenu._frmMain.ResetStatusBar();

            }
        }
        ///<summary>ctrMenü / CloseCtrJournal</summary>
        ///<remarks>.</remarks>
        public void CloseCtrJournal()
        {
            Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].Name == const_TMP + "SplitterJournal")
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    Count = this.ParentForm.Controls.Count;
                    i = 0;      // ist nur ein Controll vorhanden
                }
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrJournal))
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    i = Count - 1;      // ist nur ein Controll vorhanden
                }
            }
        }
        //************************************************************************** Bestandsliste
        ///<summary>ctrMenü / OpenCtrBestand</summary>
        ///<remarks>.</remarks>
        public void OpenCtrBestand()
        {
            bool open = false;
            Int32 j = -1;
            for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrBestand))
                {
                    open = true;
                    j = i;
                }
            }
            if (open)
            {
                this.ParentForm.Controls[j].Show();
            }
            else
            {
                _ctrBestand = new ctrBestand();
                _ctrBestand._ctrMenu = this;
                _ctrBestand.GL_User = this.GL_User;
                _ctrBestand._ctrMenu._frmMain.ResetStatusBar();
                _ctrBestand._ctrMenu._frmMain.InitStatusBar(3);

                _ctrBestand._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
                _ctrBestand._ctrMenu._frmMain.StatusBarWork(false, "Bestandsliste wird geladen...");

                _ctrBestand.Dock = DockStyle.Left;
                _ctrBestand.Name = const_TMP + "Bestand";
                this.ParentForm.Controls.Add(_ctrBestand);
                this.ParentForm.Controls.SetChildIndex(_ctrBestand, 0);
                this._frmMain.ctrFormList.AddCtrToList(ref this._frmMain);
                _ctrBestand.Show();
                CreateSplitter(const_TMP + "SplitterBestand");
                _ctrBestand._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
                _ctrBestand._ctrMenu._frmMain.ResetStatusBar();
            }
        }

        ///<summary>ctrMenü / CloseCtrBestand</summary>
        ///<remarks>.</remarks>
        public void CloseCtrBestand()
        {
            this._frmMain.ResetStatusBar();
            Int32 Count = this.ParentForm.Controls.Count;

            if (Count > 0)
            {
                if (_ctrEinlagerung != null && _ctrEinlagerung.bFromBestand == true)
                {
                    CloseCtrEinlagerung();
                }
                if (_ctrAuslagerung != null && _ctrAuslagerung.bFromBestand == true)
                {
                    CloseCtrAuslagerung();
                }
            }


            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                try
                {

                    if (this.ParentForm.Controls[i].Name == const_TMP + "SplitterBestand")
                    {
                        this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                        Count = this.ParentForm.Controls.Count;
                        i = 0;      // ist nur ein Controll vorhanden
                    }
                    if (this.ParentForm.Controls[i].GetType() == typeof(ctrBestand))
                    {
                        this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                        i = Count - 1;      // ist nur ein Controll vorhanden
                    }
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }
            }
        }


        //************************************************************************** Invneturlisten
        ///<summary>ctrMenü / OpenCtrInventory</summary>
        ///<remarks>.</remarks>
        public void OpenCtrInventory()
        {
            bool open = false;
            Int32 j = -1;
            for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrInventory))
                {
                    open = true;
                    j = i;
                }
            }
            if (open)
            {
                this.ParentForm.Controls[j].Show();
            }
            else
            {
                _ctrInventory = new ctrInventory();
                _ctrInventory._ctrMenu = this;
                _ctrInventory.GL_User = this.GL_User;
                _ctrInventory._ctrMenu._frmMain.ResetStatusBar();
                _ctrInventory._ctrMenu._frmMain.InitStatusBar(3);

                _ctrInventory._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
                _ctrInventory._ctrMenu._frmMain.StatusBarWork(false, "Inventurlisten werden geladen...");

                _ctrInventory.Dock = DockStyle.Left;
                _ctrInventory.Name = const_TMP + "Inventurlisten";
                this.ParentForm.Controls.Add(_ctrInventory);
                this.ParentForm.Controls.SetChildIndex(_ctrInventory, 0);
                this._frmMain.ctrFormList.AddCtrToList(ref this._frmMain);
                _ctrInventory.Show();
                CreateSplitter(const_TMP + "SplitterInventur");
                _ctrInventory._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
                _ctrInventory._ctrMenu._frmMain.ResetStatusBar();
            }
        }

        ///<summary>ctrMenü / CloseCtrInventory</summary>
        ///<remarks>.</remarks>
        public void CloseCtrInventory()
        {
            this._frmMain.ResetStatusBar();
            Int32 Count = this.ParentForm.Controls.Count;

            if (Count > 0)
            {
                if (_ctrEinlagerung != null && _ctrEinlagerung.bFromBestand == true)
                {
                    CloseCtrEinlagerung();
                }
                if (_ctrAuslagerung != null && _ctrAuslagerung.bFromBestand == true)
                {
                    CloseCtrAuslagerung();
                }
            }


            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                try
                {

                    if (this.ParentForm.Controls[i].Name == const_TMP + "SplitterInventur")
                    {
                        this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                        Count = this.ParentForm.Controls.Count;
                        i = 0;      // ist nur ein Controll vorhanden
                    }
                    if (this.ParentForm.Controls[i].GetType() == typeof(ctrInventory))
                    {
                        this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                        i = Count - 1;      // ist nur ein Controll vorhanden
                    }
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }
            }
        }


        ///<summary>ctrMenü / OpenCtrTariferfassung</summary>
        ///<remarks>.</remarks>
        public void OpenCtrTariferfassung(decimal myAdrID)
        {
            bool open = false;
            Int32 j = -1;
            for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrBestand))
                {
                    open = true;
                    j = i;
                }
            }
            if (open)
            {
                this.ParentForm.Controls[j].Show();
            }
            else
            {
                _ctrTarifErfassung = new ctrTarifErfassung();
                _ctrTarifErfassung._ctrMenu = this;
                _ctrTarifErfassung.GL_User = this.GL_User;
                _ctrTarifErfassung._ctrMenu._frmMain.ResetStatusBar();
                _ctrTarifErfassung._ctrMenu._frmMain.InitStatusBar(3);

                _ctrTarifErfassung._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
                _ctrTarifErfassung._ctrMenu._frmMain.StatusBarWork(false, "Tariferfassung wird geladen...");

                _ctrTarifErfassung.InitCtr(myAdrID);
                _ctrTarifErfassung.Dock = DockStyle.Left;
                _ctrTarifErfassung.Name = const_TMP + "TarifErfassung";

                //ADR-Liste schließen 
                CloseCtrADR_List(false);

                this.ParentForm.Controls.Add(_ctrTarifErfassung);
                this.ParentForm.Controls.SetChildIndex(_ctrTarifErfassung, 0);
                _ctrTarifErfassung.Show();
                CreateSplitter(const_TMP + "SplitterTarifErfassung");
                _ctrTarifErfassung._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
                _ctrTarifErfassung._ctrMenu._frmMain.ResetStatusBar();
            }
        }
        ///<summary>ctrMenü / CloseCtrTarifErfassung</summary>
        ///<remarks>.</remarks>
        public void CloseCtrTarifErfassung()
        {
            this._frmMain.ResetStatusBar();
            Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].Name == const_TMP + "SplitterTarifErfassung")
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    Count = this.ParentForm.Controls.Count;
                    i = 0;      // ist nur ein Controll vorhanden
                }
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrTarifErfassung))
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    i = Count - 1;      // ist nur ein Controll vorhanden
                }
            }
        }
        ///<summary>ctrMenü / OpenFrmAppInfo</summary>
        ///<remarks>.</remarks>
        public void OpenFrmAppInfo()
        {
            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(ctrAppInfo)) != null)
            {
                Functions.frm_FormTypeClose(typeof(ctrAppInfo));
            }
            //-- Update Forma einfügen ---
            ctrAppInfo _ctrAppInfo = new ctrAppInfo();
            _ctrAppInfo.GLUser = this.GL_User;
            _ctrAppInfo.GLSystem = this._frmMain.GL_System;
            _ctrAppInfo.StartPosition = FormStartPosition.CenterScreen;
            _ctrAppInfo.Show();
            _ctrAppInfo.BringToFront();
        }
        ///<summary>ctrMenü / OpenFrmADR</summary>
        ///<remarks>.</remarks>
        public void OpenFrmADR(ctrADR_List myADRList)
        {
            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmADR)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmADR));
            }
            //-- Update Forma einfügen ---
            frmADR ADR = new frmADR();
            ADR.GL_User = this.GL_User;
            ADR._ctrADRList = myADRList;
            ADR.StartPosition = FormStartPosition.CenterScreen;
            ADR.Show();
            ADR.BringToFront();
        }
        ///<summary>ctrMenü / OpenFrmArbeitsbereiche</summary>
        ///<remarks>.</remarks>
        public void OpenFrmArbeitsbereiche()
        {
            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmArbeitsbereiche)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmArbeitsbereiche));
            }
            frmArbeitsbereiche myfrmArbeitsbereich = new frmArbeitsbereiche();
            myfrmArbeitsbereich.GL_User = GL_User;
            myfrmArbeitsbereich._ctrMenu = this;
            myfrmArbeitsbereich.StartPosition = FormStartPosition.CenterScreen;
            myfrmArbeitsbereich.Show();
            myfrmArbeitsbereich.BringToFront();

        }
        ///<summary>ctrMenü / OpenFrmKommiDetailsPanel</summary>
        ///<remarks>.</remarks>
        public void OpenFrmKommiDetailsPanel(frmDispoKalender Kalender, AFKalenderItemKommi Kommi)
        {
            if ((Kalender != null) && (Kommi != null))
            {
                if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmKommiDetailsPanel)) != null)
                {
                    Functions.frm_FormTypeClose(typeof(frmKommiDetailsPanel));
                }
                frmKommiDetailsPanel Details = new frmKommiDetailsPanel(Kalender, Kommi);
                Details.StartPosition = FormStartPosition.CenterScreen;
                //GL_User über DispoKalender
                Details.Show();
                Details.BringToFront();
            }
        }
        ///<summary>ctrMenü / OpenFrmAuftragView</summary>
        ///<remarks>.</remarks>
        public ctrMenu OpenFrmAuftragView(object sender, clsTour myTour)
        {
            if (clsAuftragPos.IsAuftragPosInByID(this.GL_User, myTour.Auftrag.AuftragPos.ID))
            {
                Functions.frm_FormTypeClose(typeof(frmAuftragView));

                if (sender.GetType() == typeof(ctrAufträge))
                {
                    this._ctrAuftrag = (ctrAufträge)sender;
                }
                if (sender.GetType() == typeof(frmAuftrag_Splitting))
                {
                    this._frmAuftragSplitting = (frmAuftrag_Splitting)sender;
                }
                if (sender.GetType() == typeof(ctrSUList))
                {
                    this._ctrSUList = (ctrSUList)sender;
                }
                frmAuftragView av = new frmAuftragView();
                av.GL_User = this.GL_User;
                av._ctrMenu = this;
                av.Tour = myTour;
                //av._AuftragPosTableID = myAuftragPosTableID;
                av._ctrAuftrag = this._ctrAuftrag;
                av._AuftragSplit = this._frmAuftragSplitting;
                av._ctrSUListe = this._ctrSUList;
                av.Show();
                av.BringToFront();
                this._frmAuftragView = av;
            }
            return this;
        }
        ///<summary>ctrMenü / OpenFrmDispoTourChange</summary>
        ///<remarks>.</remarks>
        public ctrMenu OpenFrmDispoTourChange(object sender)
        {

            if (sender.GetType() == typeof(frmDispoKalender))
            {
                this._Kalender = (frmDispoKalender)sender;
            }

            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmDispoTourChange)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmDispoTourChange));
            }
            frmDispoTourChange kc = new frmDispoTourChange(this._Kalender);
            kc.GL_User = this.GL_User;
            kc.StartPosition = FormStartPosition.CenterScreen;
            kc.Show();
            kc.BringToFront();
            this._frmDispoTourChange = kc;
            return this;
        }
        ///<summary>ctrMenü / OpenFrmTMP</summary>
        ///<remarks>.</remarks>
        public ctrMenu OpenFrmTMP(object sender)
        {
            //Check ob die TmpFrm bereits geöffnet ist
            string strCheckFrmName = string.Empty;
            if (sender.GetType() == typeof(frmDispoKalender))
            {
                strCheckFrmName = const_TMP + "FrmDispoKalender";
            }
            //Entfernungscockpit
            if (sender.GetType() == typeof(ctrDistance))
            {
                strCheckFrmName = const_TMP + "CtrDistance";
            }
            //Lagerortverwaltung
            if (sender.GetType() == typeof(ctrLagerOrt))
            {
                strCheckFrmName = const_TMP + "CtrEinlagerung";
            }
            //Einlagerung
            if (sender.GetType() == typeof(ctrEinlagerung))
            {
                strCheckFrmName = const_TMP + "CtrEinlagerung";
            }
            //Umbuchung
            if (sender.GetType() == typeof(ctrUmbuchung))
            {
                strCheckFrmName = const_TMP + "CtrUmbuchung";
            }
            //Search
            if (sender.GetType() == typeof(ctrSearch))
            {
                strCheckFrmName = const_TMP + "CtrSearch";
            }
            //Schaden
            if (sender.GetType() == typeof(ctrSchaeden))
            {
                strCheckFrmName = const_TMP + "CtrSchaden";
            }
            //PrintLager
            if (sender.GetType() == typeof(ctrPrintLager))
            {
                strCheckFrmName = const_TMP + "CtrPrintLager";
            }
            //FaktLagerArtikelList
            if (sender.GetType() == typeof(ctrFaktArtikelList))
            {
                strCheckFrmName = const_TMP + "CtrFaktArtikelList";
            }
            //MAilCockpit
            if (sender.GetType() == typeof(ctrMailCockpit))
            {
                strCheckFrmName = const_TMP + "CtrMailCockpit";
            }
            //ExtraChargeAssignment
            if (sender.GetType() == typeof(ctrExtraChargeAssignment))
            {
                strCheckFrmName = const_TMP + "CtrExtraChargeAssignment";
            }
            //ExtraChargeAssignment
            if (sender.GetType() == typeof(ctrFreeForCall))
            {
                strCheckFrmName = const_TMP + "CtrFreeForCall";
            }
            if (sender.GetType() == typeof(ctrADRManAdd))
            {
                strCheckFrmName = const_TMP + "CtrADRManAdd";
            }
            //Post Center
            if (sender.GetType() == typeof(ctrPostCenter))
            {
                strCheckFrmName = const_TMP + "CtrPostCenter";
            }
            //Arbeitsliste
            if (sender.GetType() == typeof(ctrArbeitsliste))
            {
                strCheckFrmName = const_TMP + "CtrArbeitsliste";
            }
            //Infopanel
            if (sender.GetType() == typeof(ctrInfoPanel))
            {
                strCheckFrmName = const_TMP + "CtrInfoPanel";
            }
            //ctrSPLAdd
            if (sender.GetType() == typeof(ctrSPLAdd))
            {
                strCheckFrmName = const_TMP + "CtrSPLADD";
            }
            //ctrRetoure
            if (sender.GetType() == typeof(ctrRetoure))
            {
                strCheckFrmName = const_TMP + "CtrRetoure";
            }
            //ctrBInfo4905
            if (sender.GetType() == typeof(ctrBSInfo4905))
            {
                strCheckFrmName = const_TMP + "CtrBSInfo4905";
            }
            //ctrVDA4984Details
            if (sender.GetType() == typeof(ctrVDA4984Details))
            {
                strCheckFrmName = const_TMP + "CtrVDA4984Details";
            }
            //ctrASNArtFieldAssignSelectToCopy
            if (sender.GetType() == typeof(ctrASNArtFieldAssignSelectToCopy))
            {
                strCheckFrmName = const_TMP + "CtrASNArtFieldAssignSelectToCopy";
            }
            //ctrVDAClientWorkspaceValueSelectToCopy
            if (sender.GetType() == typeof(ctrVDAClientWorkspaceValueSelectToCopy))
            {
                strCheckFrmName = const_TMP + "CtrVDAClientWorkspaceValueSelectToCopy";
            }
            //ctrVDAClientWorkspaceValueSelectToCopy
            if (sender.GetType() == typeof(ctrInventoryAdd))
            {
                strCheckFrmName = const_TMP + "CtrInventoryAdd";
            }

            //
            Functions.frm_FormClose(typeof(frmTmp), strCheckFrmName);

            frmTmp tmp = new frmTmp();
            tmp.GL_User = this.GL_User;
            tmp.ctrMenu = this;

            if (sender.GetType() == typeof(frmDispoKalender))
            {
                this._Kalender = (frmDispoKalender)sender;
                tmp.Kalender = this._Kalender;
                tmp.Name = const_TMP + "FrmDispoKalender";
            }
            //Entfernungscockpit
            if (sender.GetType() == typeof(ctrDistance))
            {
                this._ctrDistance = (ctrDistance)sender;
                this._ctrDistance.GL_User = this.GL_User;
                tmp._ctrDistance = this._ctrDistance;
                tmp.Name = const_TMP + "CtrDistance";
            }
            //Lagerortverwaltung
            if (sender.GetType() == typeof(ctrLagerOrt))
            {
                this._ctrLagerOrt.GL_User = this.GL_User;
                tmp._ctrLagerort = this._ctrLagerOrt;
                tmp._ctrLagerort._ctrEinlagerung = this._ctrEinlagerung;
                tmp.Name = const_TMP + "CtrEinlagerung";
            }
            //Einlagerung
            if (sender.GetType() == typeof(ctrEinlagerung))
            {
                this._ctrEinlagerung.GL_User = this.GL_User;
                tmp._ctrEinlagerung = this._ctrEinlagerung;
                tmp.Name = const_TMP + "CtrEinlagerung";
            }
            //Auslagerung
            if (sender.GetType() == typeof(ctrAuslagerung))
            {
                this._ctrAuslagerung.GL_User = this.GL_User;
                tmp._ctrAuslagerung = this._ctrAuslagerung;
                tmp.Name = const_TMP + "CtrAuslagerung";
            }
            //Umbuchung
            if (sender.GetType() == typeof(ctrUmbuchung))
            {
                this._ctrUmbuchung.GL_User = this.GL_User;
                tmp._ctrUmbuchung = this._ctrUmbuchung;
                tmp.Name = const_TMP + "CtrUmbuchung";
            }
            //InfoPanel
            if (sender.GetType() == typeof(ctrInfoPanel))
            {
                //this._ctrInfoPanel.GL_User = this.GL_User;
                this._ctrInfoPanel = (ctrInfoPanel)sender;
                tmp._ctrInfoPanel = this._ctrInfoPanel;
                tmp.Name = const_TMP + "CtrInfoPanel";
            }
            //Schaden
            if (sender.GetType() == typeof(ctrSchaeden))
            {
                this._ctrSchaeden.GL_User = this.GL_User;
                tmp._ctrSchaeden = this._ctrSchaeden;
                tmp._ctrSchaeden._IsSchadensAuswahlAktion = true;
                tmp.Name = const_TMP + "CtrSchaden";
            }
            //PrintLager
            if (sender.GetType() == typeof(ctrPrintLager))
            {
                this._ctrPrintLager.GL_User = this.GL_User;
                tmp._ctrPrintLager = this._ctrPrintLager;
                tmp.Name = const_TMP + "CtrPrintLager";
            }
            //FaktLAgerArtikelList
            if (sender.GetType() == typeof(ctrFaktArtikelList))
            {
                this._ctrFaktArtikelList.GL_User = this.GL_User;
                tmp._ctrFaktArtikelList = this._ctrFaktArtikelList;
                tmp.Name = const_TMP + "CtrFaktArtikelList";
            }
            //Mail Cockpit
            if (sender.GetType() == typeof(ctrMailCockpit))
            {
                this._ctrMailCockpit.GL_User = this.GL_User;
                tmp._ctrMailCockpit = this._ctrMailCockpit;
                tmp._ctrMailCockpit.ctrMenu = this;

                tmp.Name = const_TMP + "CtrMailCockpit";
            }
            //ctrExtraChargeAssignemt
            if (sender.GetType() == typeof(ctrExtraChargeAssignment))
            {
                this._ctrExtraChargeAssignment.GLUser = this.GL_User;
                tmp._ctrExtraChargeAssignment = (ctrExtraChargeAssignment)sender;
                tmp._ctrExtraChargeAssignment.ctrMenu = this;
                tmp.Name = const_TMP + "CtrExtraChargeAssignment";
            }
            //ctrFreeForCall
            if (sender.GetType() == typeof(ctrFreeForCall))
            {
                this._ctrFreeForCall.GLUser = this.GL_User;
                tmp._ctrFreeForCall = (ctrFreeForCall)sender;
                tmp._ctrFreeForCall._ctrMenu = this;
                tmp.Name = const_TMP + "CtrFreeForCall";
            }
            //
            if (sender.GetType() == typeof(ctrADRManAdd))
            {
                this._ctrADRManAdd = (ctrADRManAdd)sender;
                this._ctrADRManAdd.GLUser = this.GL_User;
                tmp._ctrAdrManAdd = (ctrADRManAdd)sender;
                tmp._ctrAdrManAdd.GLUser = this.GL_User;
                tmp._ctrAdrManAdd._ctrMenu = this;
                tmp.Name = const_TMP + "CtrAdrManAdd";
                strCheckFrmName = const_TMP + "CtrADRManAdd";
            }
            //Post Center
            if (sender.GetType() == typeof(ctrPostCenter))
            {
                this._ctrPostCenter = (ctrPostCenter)sender;
                this._ctrPostCenter.GL_User = this.GL_User;
                tmp._ctrPostCenter = (ctrPostCenter)sender;
                tmp._ctrPostCenter.GL_User = this.GL_User;
                tmp._ctrPostCenter._ctrMenu = this;
                tmp.Name = const_TMP + "CtrPostCenter";
            }
            //SPL
            if (sender.GetType() == typeof(ctrSPLAdd))
            {
                this._ctrSPLAdd = (ctrSPLAdd)sender;
                this._ctrSPLAdd.GLUser = this.GL_User;
                this._ctrSPLAdd.ctrMenu = this;
                tmp._ctrSPLAdd = this._ctrSPLAdd;
                tmp.Name = const_TMP + "CtrSPLADD";
            }

            //Search
            if (sender.GetType() == typeof(ctrSearch))
            {
                this._ctrSearch.GL_User = this.GL_User;
                //this._ctrSearch._ctrEinlagerung = this._ctrEinlagerung;
                tmp._ctrSearch = this._ctrSearch;
                tmp.Name = const_TMP + "CtrSearch";
            }
            //Arbeitsliste
            if (sender.GetType() == typeof(ctrArbeitsliste))
            {
                this._ctrArbeitsliste = (ctrArbeitsliste)sender;
                this._ctrArbeitsliste.GL_User = this.GL_User;
                tmp._ctrArbeitsliste = (ctrArbeitsliste)sender;
                tmp._ctrArbeitsliste.GL_User = this.GL_User;
                tmp._ctrArbeitsliste._ctrMenu = this;
                tmp.Name = const_TMP + "CtrArbeitsliste";
            }
            //ctrRetoure
            if (sender.GetType() == typeof(ctrRetoure))
            {
                this._ctrRetoure = (ctrRetoure)sender;
                this._ctrRetoure.GLUser = this.GL_User;
                tmp._ctrRetoureEingang = (ctrRetoure)sender;
                tmp._ctrRetoureEingang.GLUser = this.GL_User;
                tmp._ctrRetoureEingang.ctrMenu = this;
                tmp.Name = const_TMP + "CtrRetoure";
            }
            //ctrBSInfo4905
            if (sender.GetType() == typeof(ctrBSInfo4905))
            {
                this._ctrBSInfo4905 = (ctrBSInfo4905)sender;
                tmp._ctrBSInfo4905 = this._ctrBSInfo4905;
                tmp._ctrBSInfo4905.GL_User = this.GL_User;
                tmp._ctrBSInfo4905._ctrMenu = this;
                tmp.Name = const_TMP + "CtrBSInfo4905";
            }
            //ctrVDA4905Details
            if (sender.GetType() == typeof(ctrVDA4984Details))
            {
                this._ctrVDA4984Details = (ctrVDA4984Details)sender;
                tmp._ctrVDA4984Details = this._ctrVDA4984Details;
                tmp._ctrVDA4984Details.GLUser = this.GL_User;
                tmp._ctrVDA4984Details._ctrMenu = this;
                tmp.Name = const_TMP + "CtrVDA4984Details";
            }
            //ctrASNArtFieldAssignSelectToCopy            
            if (sender.GetType() == typeof(ctrASNArtFieldAssignSelectToCopy))
            {
                this._ctrASNArtFieldAssignSelectToCopy = (ctrASNArtFieldAssignSelectToCopy)sender;
                tmp._ctrASNArtFieldAssignSelectToCopy = this._ctrASNArtFieldAssignSelectToCopy;
                tmp.Name = const_TMP + "CtrASNArtFieldAssignSelectToCopy";
            }
            //ctrASNActionSelectToCopy            
            if (sender.GetType() == typeof(ctrASNActionSelectToCopy))
            {
                this._ctrASNActionSelectToCopy = (ctrASNActionSelectToCopy)sender;
                tmp._ctrASNActionSelectToCopy = this._ctrASNActionSelectToCopy;
                tmp.Name = const_TMP + "CtrASNActionSelectToCopy";
            }
            //ctrJobSelectToCopy            
            if (sender.GetType() == typeof(ctrJobSelectToCopy))
            {
                this._ctrJobSelectToCopy = (ctrJobSelectToCopy)sender;
                tmp._ctrJobSelectToCopy = this._ctrJobSelectToCopy;
                tmp.Name = const_TMP + "CtrASNActionSelectToCopy";
            }
            //ctrVDAClientWorkspaceValueSelectToCopy            
            if (sender.GetType() == typeof(ctrVDAClientWorkspaceValueSelectToCopy))
            {
                this._ctrVDAClientWorkspaceValueSelectToCopy = (ctrVDAClientWorkspaceValueSelectToCopy)sender;
                tmp._ctrVDAClientWorkspaceValueSelectToCopy = this._ctrVDAClientWorkspaceValueSelectToCopy;
                tmp.Name = const_TMP + "CtrVDAClientWorkspaceValueSelectToCopy";
            }
            //ctrVDAClientWorkspaceValueSelectToCopy            
            if (sender.GetType() == typeof(ctrInventoryAdd))
            {
                this._ctrInventoryAdd = (ctrInventoryAdd)sender;
                tmp._ctrInventoryAdd = this._ctrInventoryAdd;
                tmp.Name = const_TMP + "CtrInventoryAdd";
            }
            //tmp.StartPosition = FormStartPosition.CenterScreen;
            //tmp.StartPosition = FormStartPosition.CenterParent;

            tmp.StartPosition = FormStartPosition.WindowsDefaultBounds;
            tmp.Show();
            this._FrmTmp = tmp;
            return this;
        }
        ///<summary>ctrMenü / OpenFrmAuftrag_Fast</summary>
        ///<remarks>.</remarks>
        public void OpenFrmAuftrag_Fast(ctrAufträge myCtrAuftrag)
        {
            if (myCtrAuftrag.GetType() == typeof(ctrAufträge))
            {
                if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmAuftrag_Fast)) != null)
                {
                    Functions.frm_FormTypeClose(typeof(frmAuftrag_Fast));
                }
                //-- Update Form einfügen ---
                frmAuftrag_Fast Auftrag = new frmAuftrag_Fast(myCtrAuftrag, true);
                Auftrag.StartPosition = FormStartPosition.CenterScreen;
                Auftrag._ctrMenu = this;
                Auftrag.GL_User = this.GL_User;
                Auftrag.Show();
                Auftrag.BringToFront();
            }
        }
        //
        public ctrMenu OpenCtrTourDetails(object sender)
        {
            if (sender.GetType() == typeof(frmDispoKalender))
            {
                bool open = false;
                Int32 j = -1;

                for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
                {
                    if (this.ParentForm.Controls[i].GetType() == typeof(ctrTourDetails))
                    {
                        open = true;
                        j = i;
                    }
                }
                if (open)
                {
                    this.ParentForm.Controls[j].Show();
                }
                else
                {
                    this._Kalender = (frmDispoKalender)sender;
                    ctrTourDetails tour = new ctrTourDetails();
                    tour.GL_User = this.GL_User;
                    tour.Kalender = this._Kalender;
                    tour.Dock = System.Windows.Forms.DockStyle.Left;
                    tour.Name = const_TMP + "TourDetails";
                    this.ParentForm.Controls.Add(tour);
                    this.ParentForm.Controls.SetChildIndex(tour, 0);
                    tour.Show();

                    CreateSplitter(const_TMP + "SplitterTourDetails");
                    this._ctrTourDetails = tour;
                }
            }
            return this;
        }
        //
        public void CloseCtrTourDetails()
        {
            Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].Name == const_TMP + "SplitterTourDetails")
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    //i = Count - 1;      // ist nur ein Controll vorhanden
                }
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrTourDetails))
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    i = Count - 1;      // ist nur ein Controll vorhanden
                }
            }
        }
        ///<summary>ctrMenü / OpenFrmMandanten</summary>
        ///<remarks>.</remarks>
        public void OpenFrmMandanten()
        {
            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmMandanten)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmMandanten));
            }

            frmMandanten test = new frmMandanten();
            test.GL_User = GL_User;
            test._ctrMenu = this;
            test.StartPosition = FormStartPosition.CenterScreen;
            test.Show();
            test.BringToFront();
        }
        ///<summary>ctrMenü / OpenFrmFahrzeuge</summary>
        ///<remarks>.</remarks>
        public void OpenFrmFahrzeuge(object obj, bool bAddNewItem)
        {
            if (typeof(ctrFahrzeug_List) == obj.GetType())
            {
                if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmFahrzeuge)) != null)
                {
                    Functions.frm_FormTypeClose(typeof(frmFahrzeuge));
                }
                _frmFahrzeuge = new frmFahrzeuge((ctrFahrzeug_List)obj);
                _frmFahrzeuge.GL_User = GL_User;
                _frmFahrzeuge.StartPosition = FormStartPosition.CenterScreen;
                _frmFahrzeuge.BringToFront();
                _frmFahrzeuge.Show();
                if (!bAddNewItem)
                {
                    _frmFahrzeuge.bo_update = (!bAddNewItem);
                    _frmFahrzeuge.ReadDataByID(_frmFahrzeuge._ctrFahrzeugList.Fahrzeug.ID);
                }
            }
        }

        ///<summary>ctrMenü / OpenFrmFahrzeuge</summary>
        ///<remarks>.</remarks>
        public void OpenFrmArchivView()
        {
            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmArchiveView)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmArchiveView));
            }
            frmArchiveView _frmArchivView = new frmArchiveView();
            _frmArchivView.InitFrm(this);
            _frmArchivView.StartPosition = FormStartPosition.CenterScreen;
            _frmArchivView.Show();
            _frmArchivView.BringToFront();
        }
        ///<summary>ctrMenü / CloseFrmFahrzeuge</summary>
        ///<remarks>.</remarks>
        public void CloseFrmFahrzeuge()
        {
            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmFahrzeuge)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmFahrzeuge));
            }
        }
        ///<summary>ctrMenü / OpenScanFrm</summary>
        ///<remarks>.</remarks>
        public void OpenScanFrm(decimal myDecAuftragID, decimal myDecAuftragPosTableID, decimal myDecLEingangID, decimal myDecLAusgangID, object objFrm)
        {
            frmTwain twain = new frmTwain();
            if (objFrm.GetType() == typeof(frmAuftragView))
            {
                this._frmAuftragView = (frmAuftragView)objFrm;
                twain._frmAV = this._frmAuftragView;
            }
            else
            {
                twain._frmAV = null;
            }
            if (objFrm.GetType() == typeof(ctrAufträge))
            {
                this._ctrAuftrag = (ctrAufträge)objFrm;
                twain._ctrAuftrag = this._ctrAuftrag;
            }
            else
            {
                twain._ctrAuftrag = null;
            }
            twain.GLSystem = this._frmMain.GL_System;
            twain.GL_User = this._frmMain.GL_User;
            twain.decAuftragID = myDecAuftragID;
            twain.decLEingangID = myDecLEingangID;
            twain.decLAusgangID = myDecLAusgangID;
            twain.decAuftragPosTableID = myDecAuftragPosTableID;
            twain.StartPosition = FormStartPosition.CenterScreen;
            twain.Show();
        }
        ///<summary>ctrMenü / OpenADRSearch</summary>
        ///<remarks>.</remarks>
        public void OpenADRSearch(object obj)
        {
            bool bObjectOK = false;
            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmADRSearch)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmADRSearch));
            }
            frmADRSearch ADR = new frmADRSearch();
            ADR.GL_User = this.GL_User;


            //Abfrage nach den Möglichen Frm / Ctr
            if (obj.GetType() == typeof(frmMandanten))
            {
                ADR.mandanten = (frmMandanten)obj;
                ADR.ctrMenu = ((frmMandanten)obj)._ctrMenu;
                bObjectOK = true;
            }
            //Auftragserfassung
            if (obj.GetType() == typeof(frmAuftrag_Fast))
            {
                ADR.auftragErfassung = (frmAuftrag_Fast)obj;
                ADR.ctrMenu = ((frmAuftrag_Fast)obj)._ctrAuftrag._ctrMenu;
                bObjectOK = true;
            }
            //Einlagerung
            if (obj.GetType() == typeof(ctrEinlagerung))
            {
                // weiterleiten der SearchbuttonNummer als befehl?
                ADR.einlagerung = (ctrEinlagerung)obj;
                ADR.ctrMenu = ((ctrEinlagerung)obj)._ctrMenu;
                bObjectOK = true;
            }
            //ctrArtDetail
            if (obj.GetType() == typeof(ctrArtDetails))
            {
                // weiterleiten der SearchbuttonNummer als befehl?
                ADR.ctrArtDetail = (ctrArtDetails)obj;
                ADR.ctrMenu = ((ctrArtDetails)obj)._ctrMenu;
                bObjectOK = true;
            }
            //Manuelle Rechnung
            if (obj.GetType() == typeof(ctrRGManuell))
            {

                ADR.ctrRGManuell = (ctrRGManuell)obj;
                ADR.ctrMenu = ((ctrRGManuell)obj)._ctrMenu;
                bObjectOK = true;
            }
            //Auslagerung
            if (obj.GetType() == typeof(ctrAuslagerung))
            {
                // weiterleiten der SearchbuttonNummer als befehl?
                ADR.auslagerung = (ctrAuslagerung)obj;
                ADR.ctrMenu = ((ctrAuslagerung)obj)._ctrMenu;
                bObjectOK = true;
            }
            //Umbuchung
            if (obj.GetType() == typeof(ctrUmbuchung))
            {
                ADR.ctrUmbuchung = (ctrUmbuchung)obj;
                ADR.ctrMenu = ((ctrUmbuchung)obj)._ctrMenu;
                bObjectOK = true;
            }
            //Bestand
            if (obj.GetType() == typeof(ctrBestand))
            {
                ADR.ctrBestand = (ctrBestand)obj;
                // ADR.ctrMenu = ((ctrBestand)obj)._ctrMenu;
                ADR.ctrMenu = this;
                bObjectOK = true;
            }
            //Journal - für die Filtersuche
            if (obj.GetType() == typeof(ctrJournal))
            {
                ADR.ctrJournal = (ctrJournal)obj;
                // ADR.ctrMenu = ((ctrBestand)obj)._ctrMenu;
                ADR.ctrMenu = this;
                bObjectOK = true;
            }
            //Print Lager
            if (obj.GetType() == typeof(ctrPrintLager))
            {
                ADR.ctrPrintLager = (ctrPrintLager)obj;
                ADR.ctrMenu = ((ctrPrintLager)obj)._ctrMenu;
                bObjectOK = true;
            }
            //Fakturierung Lager
            if (obj.GetType() == typeof(ctrFaktLager))
            {
                ADR.ctrFaktLager = (ctrFaktLager)obj;
                ADR.ctrMenu = ((ctrFaktLager)obj)._ctrMenu;
                bObjectOK = true;
            }
            //Adress Liste
            if (obj.GetType() == typeof(ctrADR_List))
            {
                ADR.ctrADRSearch = InitAdrSearch();
                ADR.ctrMenu = this;
                ADR.ctrMenu._ctrAdrList = ((ctrADR_List)obj).Copy();
                bObjectOK = true;
            }
            //Adress Liste
            if (obj.GetType() == typeof(ctrFreeForCall))
            {
                ctrFreeForCall tmpFfC = (ctrFreeForCall)obj;
                ADR.ctrFreeForCall = tmpFfC;
                ADR.ctrMenu = tmpFfC._ctrMenu;
                bObjectOK = true;
            }
            //Artikel Search
            if (obj.GetType() == typeof(ctrSearch))
            {
                ctrSearch Search = (ctrSearch)obj;
                ADR.ctrSearch = Search;
                ADR.ctrMenu = Search._ctrMenu;
                bObjectOK = true;
            }
            //RGList
            if (obj.GetType() == typeof(ctrRGList))
            {
                ctrRGList tmpCtrRGList = (ctrRGList)obj;
                tmpCtrRGList._ctrMenu = this;
                ADR.ctrRGList = tmpCtrRGList;
                ADR.ctrMenu = tmpCtrRGList._ctrMenu;
                bObjectOK = true;
            }
            //Statistik
            if (obj.GetType() == typeof(ctrStatistik))
            {
                ctrStatistik tmpCtrStat = (ctrStatistik)obj;
                tmpCtrStat._ctrMenu = this;
                ADR.ctrStatistik = tmpCtrStat;
                ADR.ctrMenu = tmpCtrStat._ctrMenu;
                bObjectOK = true;
            }
            //Güterartenliste
            if (obj.GetType() == typeof(ctrGueterArtListe))
            {
                ctrGueterArtListe tmpCtrGArtList = (ctrGueterArtListe)obj;
                tmpCtrGArtList._ctrMenu = this;
                ADR.ctrGArtenListe = tmpCtrGArtList;
                ADR.ctrMenu = this;
                bObjectOK = true;
            }
            //Worklist
            if (obj.GetType() == typeof(ctrWorklist))
            {
                ctrWorklist tmpCtrWorklist = (ctrWorklist)obj;
                tmpCtrWorklist._ctrMenu = this;
                ADR.ctrWorklist = tmpCtrWorklist;
                ADR.ctrMenu = this;
                bObjectOK = true;
            }
            //ctrReportSetting
            if (obj.GetType() == typeof(ctrReportSetting))
            {
                ctrReportSetting tmpCtrRepSetting = (ctrReportSetting)obj;
                tmpCtrRepSetting._ctrMenu = this;
                ADR.ctrRepSetting = tmpCtrRepSetting;
                ADR.ctrMenu = this;
                bObjectOK = true;
            }
            //ctrReportSetting
            if (obj.GetType() == typeof(ctrASNAction))
            {
                ctrASNAction tmpctrASNAction = (ctrASNAction)obj;
                tmpctrASNAction._ctrMenu = this;
                ADR.ctrASNAction = tmpctrASNAction;
                ADR.ctrMenu = this;
                bObjectOK = true;
            }
            //ctrASNMAin
            if (obj.GetType() == typeof(ctrASNMain))
            {
                ctrASNMain tmpctrASNMain = (ctrASNMain)obj;
                tmpctrASNMain._ctrMenu = this;
                ADR.ctrASNMain = tmpctrASNMain;
                ADR.ctrMenu = this;
                bObjectOK = true;
            }
            //ctrADRVerweis
            if (obj.GetType() == typeof(ctrAdrVerweis))
            {
                ctrAdrVerweis tmpctrAdrVerweis = (ctrAdrVerweis)obj;
                tmpctrAdrVerweis._ctrMenu = this;
                ADR.ctrAdrVerweis = tmpctrAdrVerweis;
                ADR.ctrMenu = this;
                bObjectOK = true;
            }
            //ctrVDAClientOut
            if (obj.GetType() == typeof(ctrVDAClientOut))
            {
                ctrVDAClientOut tmpctrVDAClientOut = (ctrVDAClientOut)obj;
                tmpctrVDAClientOut._ctrMenu = this;
                ADR.ctrVDAClientOut = tmpctrVDAClientOut;
                ADR.ctrMenu = this;
                bObjectOK = true;
            }
            //ctrASNArtFieldAssignment
            if (obj.GetType() == typeof(ctrASNArtFieldAssignment))
            {
                ctrASNArtFieldAssignment tmpctrASNArtFieldAssignment = (ctrASNArtFieldAssignment)obj;
                tmpctrASNArtFieldAssignment._ctrMenu = this;
                ADR.ctrASNArtFieldAssign = tmpctrASNArtFieldAssignment;
                ADR.ctrMenu = this;
                bObjectOK = true;
            }
            //ctrASNArtFieldAssignment
            if (obj.GetType() == typeof(ctrASNActionSelectToCopy))
            {
                ctrASNActionSelectToCopy tmpctrASNActionSelectToCopy = (ctrASNActionSelectToCopy)obj;
                tmpctrASNActionSelectToCopy._ctrMenue = this;
                ADR.ctrASNActionSelectToCopy = tmpctrASNActionSelectToCopy;
                ADR.ctrMenu = this;
                bObjectOK = true;
            }
            //ctrJob
            if (obj.GetType() == typeof(ctrJob))
            {
                ctrJob tmpctrJob = (ctrJob)obj;
                tmpctrJob._ctrMenu = this;
                ADR.ctrJob = tmpctrJob;
                ADR.ctrMenu = this;
                bObjectOK = true;
            }
            //ctrVDAClientWorkspaceValue
            if (obj.GetType() == typeof(ctrVDAClientWorkspaceValue))
            {
                ctrVDAClientWorkspaceValue tmpCtr = (ctrVDAClientWorkspaceValue)obj;
                tmpCtr._ctrMenu = this;
                ADR.ctrVDAClientWorkspaceValue = tmpCtr;
                ADR.ctrMenu = this;
                bObjectOK = true;
            }
            //ctrEdiAdrWorkspaceAssignment
            if (obj.GetType() == typeof(ctrEdiClientWorkspaceValue))
            {
                ctrEdiClientWorkspaceValue tmpCtr = (ctrEdiClientWorkspaceValue)obj;
                tmpCtr._ctrMenu = this;
                ADR.ctrEdiAdrWorkspaceAssignment = tmpCtr;
                ADR.ctrMenu = this;
                bObjectOK = true;
            }
            //ctrEDIFACTClientOut
            if (obj.GetType() == typeof(ctrEDIFACTClientOut))
            {
                ctrEDIFACTClientOut tmpCtr = (ctrEDIFACTClientOut)obj;
                tmpCtr._ctrMenu = this;
                ADR.ctrEdifactClientOUT = tmpCtr;
                ADR.ctrMenu = this;
                bObjectOK = true;
            }
            //ctrCreateEdiStruckture
            if (obj.GetType() == typeof(ctrCreateEdiStruckture))
            {
                ctrCreateEdiStruckture tmpCtr = (ctrCreateEdiStruckture)obj;
                tmpCtr._ctrMenu = this;
                ADR.ctrCreateEdiStruckture = tmpCtr;
                ADR.ctrMenu = this;
                bObjectOK = true;
            }
            //ctrCustomProcess
            if (obj.GetType() == typeof(ctrCustomProcess))
            {
                ctrCustomProcess tmpCtr = (ctrCustomProcess)obj;
                tmpCtr._ctrMenu = this;
                ADR.ctrCustomProcess = tmpCtr;
                ADR.ctrMenu = this;
                bObjectOK = true;
            }
            //ctrCronJobs
            if (obj.GetType() == typeof(ctrCronJobs))
            {
                ctrCronJobs tmpCtr = (ctrCronJobs)obj;
                tmpCtr._ctrMenu = this;
                ADR.ctrCronJob = tmpCtr;
                ADR.ctrMenu = this;
                bObjectOK = true;
            }

            //Öffenen der Form
            if (bObjectOK)
            {
                ADR.InitCtrADRSearch();
                ADR.StartPosition = FormStartPosition.CenterScreen;
                ADR.Dock = DockStyle.Fill;
                ADR.Show();
                ADR.BringToFront();
            }
        }
        //
        //
        //********************************************************************************************** Fakturierung
        //Spedition
        private void lCalcSped_Click(object sender, EventArgs e)
        {
            //OpenCtrFaktSpedition(); 
        }
        private void OpenCtrFaktSpedition()
        {
            ctrFaktSpedition ctrFaktSped = new ctrFaktSpedition();
            ctrFaktSped.GL_User = GL_User;
            ctrFaktSped._ctrMenu = this;
            ctrFaktSped.Dock = DockStyle.Left;
            ctrFaktSped.Name = const_TMP + "Fakturierung";
            this.ParentForm.Controls.Add(ctrFaktSped);
            this.ParentForm.Controls.SetChildIndex(ctrFaktSped, 0);
            ctrFaktSped.Show();

            CreateSplitter(const_TMP + "SplitterFakturierung");
        }
        public void CloseCtrFakturierung()
        {
            Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].Name == const_TMP + "SplitterFakturierung")
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    //i = Count - 1;      // ist nur ein Controll vorhanden
                }
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrFaktSpedition))
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    i = Count - 1;      // ist nur ein Controll vorhanden
                }
            }
        }
        //LAger
        private void lCalcLager_Click(object sender, EventArgs e)
        {
            OpenCtrFaktLager();
        }
        private void OpenCtrFaktLager()
        {
            //OpenFrmWait();
            ctrFaktLager ctrFaktLager = new ctrFaktLager();
            ctrFaktLager.GL_User = GL_User;
            ctrFaktLager._ctrMenu = this;
            ctrFaktLager.Dock = DockStyle.Left;
            ctrFaktLager.Name = const_TMP + "Fakturierung";
            this.ParentForm.Controls.Add(ctrFaktLager);
            this.ParentForm.Controls.SetChildIndex(ctrFaktLager, 0);
            ctrFaktLager.Show(); // Show in dem Ctr, dabei wird direkt frmWait geschlossen

            CreateSplitter(const_TMP + "SplitterFakturierungLager");
        }
        public void CloseCtrFaktLager()
        {
            Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].Name == const_TMP + "SplitterFakturierungLager")
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    //i = Count - 1;      // ist nur ein Controll vorhanden
                }
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrFaktLager))
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    i = Count - 1;      // ist nur ein Controll vorhanden
                }
            }
        }
        //************************************************************************* Rechnungsbuch
        ///<summary>ctrMenu / label1_Click_2</summary>
        ///<remarks></remarks>
        private void label1_Click_2(object sender, EventArgs e)
        {
            OpenCtrRGList();
        }
        ///<summary>ctrMenu / OpenCtrRGList</summary>
        ///<remarks></remarks>
        private void OpenCtrRGList()
        {
            //OpenFrmWait();
            ctrRGList _ctrRGList = new ctrRGList();
            _ctrRGList.GL_User = GL_User;
            _ctrRGList._ctrMenu = this;
            _ctrRGList.Dock = DockStyle.Left;
            _ctrRGList.Name = const_TMP + "Fakturierung";
            this.ParentForm.Controls.Add(_ctrRGList);
            this.ParentForm.Controls.SetChildIndex(_ctrRGList, 0);
            _ctrRGList.Show(); // Show in dem Ctr, dabei wird direkt frmWait geschlossen

            CreateSplitter(const_TMP + "SplitterRGList");
        }
        ///<summary>ctrMenu / CloseCtrRGList</summary>
        ///<remarks></remarks>
        public void CloseCtrRGList()
        {
            Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].Name == const_TMP + "SplitterRGList")
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    //i = Count - 1;      // ist nur ein Controll vorhanden
                }
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrRGList))
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    i = Count - 1;      // ist nur ein Controll vorhanden
                }
            }
        }
        //******************************************************************************************* CtrASNRead
        ///<summary>ctrMenu / OpenCtrASNRead</summary>
        ///<remarks></remarks>
        public void OpenCtrASNRead()
        {
            ctrASNRead ctrASNRead = new ctrASNRead();
            ctrASNRead.GL_User = this.GL_User;
            ctrASNRead._ctrMenu = this;
            ctrASNRead.Dock = System.Windows.Forms.DockStyle.Left;
            ctrASNRead.Name = const_TMP + "ASNRead";
            this.ParentForm.Controls.Add(ctrASNRead);
            this.ParentForm.Controls.SetChildIndex(ctrASNRead, 0);
            ctrASNRead.Show();
            CreateSplitter(const_TMP + "SplitterCtrASNRead");
            this._ctrASNRead = ctrASNRead;
        }
        ///<summary>ctrMenu / CloseCtrASNRead</summary>
        ///<remarks></remarks>
        public void CloseCtrASNRead()
        {
            Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].Name == const_TMP + "SplitterCtrASNRead")
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    //i = Count - 1;      // ist nur ein Controll vorhanden
                }
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrASNRead))
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    i = Count - 1;      // ist nur ein Controll vorhanden
                }
            }
        }
        //******************************************************************************************* CtrASNCall
        ///<summary>ctrMenu / OpenCtrASNRead</summary>
        ///<remarks></remarks>
        public void OpenCtrASNCall(string strModus)
        {
            _ctrASNCall = new ctrASNCall();
            _ctrASNCall.GL_User = this.GL_User;
            _ctrASNCall._ctrMenu = this;
            _ctrASNCall.AktionModus = strModus;
            _ctrASNCall.Dock = System.Windows.Forms.DockStyle.Left;
            _ctrASNCall.Name = const_TMP + "ASNCall";
            this.ParentForm.Controls.Add(_ctrASNCall);
            this.ParentForm.Controls.SetChildIndex(_ctrASNCall, 0);
            _ctrASNCall.Show();
            CreateSplitter(const_TMP + "SplitterCtrASNCall");
            //this._ctrASNRead = ctrASNRead;
        }
        ///<summary>ctrMenu / OpenCtrASNRead</summary>
        ///<remarks></remarks>
        public void OpenCtrDelforDeliveryForecast()
        {
            _ctrDelforDeliveryForecast = new ctrDelforDeliveryForecast();
            _ctrDelforDeliveryForecast.Dock = System.Windows.Forms.DockStyle.Left;
            _ctrDelforDeliveryForecast.Name = const_TMP + "DelforDeliveryForecast";
            this.ParentForm.Controls.Add(_ctrDelforDeliveryForecast);
            this.ParentForm.Controls.SetChildIndex(_ctrDelforDeliveryForecast, 0);
            _ctrDelforDeliveryForecast.InitCtr(this);
            _ctrDelforDeliveryForecast.Show();
            CreateSplitter(const_TMP + "SplitterCtrDelforDeliveryForecast");
        }

        //public void OpenCtrCreateEdiStruckture()
        //{
        //    _ctrCreateEdiStruckture = new ctrCreateEdiStruckture();
        //    _ctrCreateEdiStruckture.Dock = System.Windows.Forms.DockStyle.Left;
        //    _ctrCreateEdiStruckture.Name = const_TMP + "CreateEdiStruckture";
        //    this.ParentForm.Controls.Add(_ctrCreateEdiStruckture);
        //    this.ParentForm.Controls.SetChildIndex(_ctrCreateEdiStruckture, 0);
        //    //_ctrCreateEdiStruckture.InitCtr(this);
        //    _ctrCreateEdiStruckture.Show();
        //    CreateSplitter(const_TMP + "SplitterCtrCreateEdiStruckture");
        //}
        ///<summary>ctrMenu / CloseCtrASNRead</summary>
        ///<remarks></remarks>
        public void CloseCtrASNCall()
        {
            Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].Name == const_TMP + "SplitterCtrASNCall")
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    //i = Count - 1;      // ist nur ein Controll vorhanden
                }
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrASNCall))
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    i = Count - 1;      // ist nur ein Controll vorhanden
                }
            }
        }
        //******************************************************************************************* ctrLieferEinteilung
        ///<summary>ctrMenu / OpenCtrASNRead</summary>
        ///<remarks></remarks>
        public void OpenCtrPrinter()
        {
            ctrPrinter ctrPrinter = new ctrPrinter(this);

            ctrPrinter.Dock = System.Windows.Forms.DockStyle.Left;
            ctrPrinter.Name = const_TMP + "Printer";
            this.ParentForm.Controls.Add(ctrPrinter);
            this.ParentForm.Controls.SetChildIndex(ctrPrinter, 0);
            ctrPrinter.Show();
            CreateSplitter(const_TMP + "SplitterCtrPrinter");
            this._ctrPrinter = ctrPrinter;
        }



        ///<summary>ctrMenu / OpenCtrASNRead</summary>
        ///<remarks></remarks>
        public void OpenCtrLiefereinteilungen()
        {
            ctrLieferEinteilung _ctrLET = new ctrLieferEinteilung();
            _ctrLET.GL_User = this.GL_User;
            _ctrLET.Dock = System.Windows.Forms.DockStyle.Left;
            _ctrLET.Name = const_TMP + "LETVDA4905";
            _ctrLET._ctrMenu = this;
            this.ParentForm.Controls.Add(_ctrLET);
            this.ParentForm.Controls.SetChildIndex(_ctrLET, 0);
            _ctrLET.Show();
            CreateSplitter(const_TMP + "SplitterCtrLETVDA4905");
            this._ctrLiefereinteilungen = _ctrLET;
        }
        ///<summary>ctrMenu / CloseCtrASNRead</summary>
        ///<remarks></remarks>
        public void CloseCtrLieferEinteilung()
        {
            Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].Name == const_TMP + "SplitterCtrLETVDA4905")
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    //i = Count - 1;      // ist nur ein Controll vorhanden
                }
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrLieferEinteilung))
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    i = Count - 1;      // ist nur ein Controll vorhanden
                }
            }
        }
        //******************************************************************************************* ctrBSInfo4905
        ///<summary>ctrMenu / OpenCtrBSInfo4905</summary>
        ///<remarks></remarks>
        public void OpenCtrBSInfo4905(ctrBSInfo4905 ctr)
        {
            //ctrBSInfo4905 _ctrBSInfo;
            //if (ctr is ctrBSInfo4905)
            //{
            //    _ctrBSInfo = ctr;
            //}
            //else
            //{
            //    _ctrBSInfo = new ctrBSInfo4905();
            //    _ctrBSInfo.GL_User = this.GL_User;
            //    _ctrBSInfo._ctrMenu = this;
            //}
            //_ctrBSInfo.Dock = System.Windows.Forms.DockStyle.Left;
            //_ctrBSInfo.Name = const_TMP + "BSInfoVDA4905";
            //this.ParentForm.Controls.Add(_ctrBSInfo);
            //this.ParentForm.Controls.SetChildIndex(_ctrBSInfo, 0);
            //_ctrBSInfo.Show();
            //CreateSplitter(const_TMP + "SplitterCtrBSInfoVDA4905");
            //this._ctrBSInfo4905 = _ctrBSInfo;

            bool open = false;
            Int32 j = -1;
            for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrBSInfo4905))
                {
                    open = true;
                    j = i;
                }
            }
            if (open)
            {
                this.ParentForm.Controls[j].Show();
            }
            else
            {
                ctrBSInfo4905 _ctrBSInfo = new ctrBSInfo4905();
                _ctrBSInfo.GL_User = this.GL_User;
                _ctrBSInfo._ctrMenu = this;
                _ctrBSInfo.Dock = System.Windows.Forms.DockStyle.Left;
                _ctrBSInfo.Name = const_TMP + "BSInfoVDA4905";
                this.ParentForm.Controls.Add(_ctrBSInfo);
                this.ParentForm.Controls.SetChildIndex(_ctrBSInfo, 0);
                _ctrBSInfo.Show();
                CreateSplitter(const_TMP + "SplitterCtrBSInfoVDA4905");
                this._ctrBSInfo4905 = _ctrBSInfo;
            }


        }
        ///<summary>ctrMenu / CloseCtrBSInfo4905</summary>
        ///<remarks></remarks>
        public void CloseCtrBSInfo4905()
        {
            //Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].Name == const_TMP + "SplitterCtrBSInfoVDA4905")
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    //i = Count - 1;      // ist nur ein Controll vorhanden
                    i = 0;
                }
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrBSInfo4905))
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    i = this.ParentForm.Controls.Count - 1;      // ist nur ein Controll vorhanden
                }
            }
        }
        ///<summary>ctrMenü / OpenCtrBSInfo4905InFrm</summary>
        ///<remarks>.</remarks>
        public void OpenCtrBSInfo4905InFrm(ctrBSInfo4905 ctr)
        {
            if (ctr is ctrBSInfo4905)
            {
                this._ctrBSInfo4905 = ctr;
                object obj = this._ctrBSInfo4905;
                OpenFrmTMP(obj);
            }
        }
        ///<summary>ctrMenü / CloseCtrBSInfo4905FrmTmp</summary>
        ///<remarks>.</remarks>
        public void CloseCtrBSInfo4905FrmTmp()
        {
            foreach (Form OpenForm in Application.OpenForms)
            {
                if (OpenForm.GetType() == typeof(frmTmp))
                {
                    if (((frmTmp)OpenForm)._ctrBSInfo4905 != null)
                    {
                        ((frmTmp)OpenForm).CloseFrmTmp();
                    }
                }
            }
        }
        //******************************************************************************************* CtrVDA4984Details
        public void OpenCtrVDA4984Details(DataRow myRow)
        {
            this._ctrVDA4984Details = new ctrVDA4984Details(myRow);
            this._ctrVDA4984Details.GLUser = this.GL_User;
            this.OpenFrmTMP(this._ctrVDA4984Details);
        }
        //******************************************************************************************* CtrRGManuell
        ///<summary>ctrMenu / OpenCtrRGManuell</summary>
        ///<remarks></remarks>
        private void OpenCtrRGManuell()
        {
            Int32 j = -1;
            for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrRGManuell))
                {
                    j = i;
                }
            }
            if (j > -1)
            {
                this.ParentForm.Controls[j].Show();
            }
            else
            {
                ctrRGManuell _ctrRGManuell = new ctrRGManuell();
                _ctrRGManuell.GL_User = this.GL_User;
                _ctrRGManuell._ctrMenu = this;
                _ctrRGManuell.Dock = DockStyle.Left;
                _ctrRGManuell.Name = const_TMP + "Fakturierung";
                this.ParentForm.Controls.Add(_ctrRGManuell);
                this.ParentForm.Controls.SetChildIndex(_ctrRGManuell, 0);
                _ctrRGManuell.Show(); // Show in dem Ctr, dabei wird direkt frmWait geschlossen

                CreateSplitter(const_TMP + "SplitterRGManuell");
            }
        }
        ///<summary>ctrMenu / CloseCtrRGManuell</summary>
        ///<remarks></remarks>
        public void CloseCtrRGManuell()
        {
            Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].Name == const_TMP + "SplitterRGManuell")
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    i = Count - 1;      // ist nur ein Controll vorhanden
                }
            }
        }
        //***************************************************************************************************************

        public void ShowOrHideSpecificCtr(Object myObj, bool myBShow)
        {
            //Bestand
            if (myObj.GetType() == typeof(ctrBestand))
            {
                myObj = (ctrBestand)myObj;
                if (myBShow)
                {

                }
                else
                {

                }

            }
            //Journal
            if (myObj.GetType() == typeof(ctrJournal))
            {
                myObj = (ctrJournal)myObj;
                if (myBShow)
                {

                }
                else
                {

                }

            }


            Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {


                if (this.ParentForm.Controls[i] == myObj)
                {
                    if (myBShow)
                    {
                        this.ParentForm.Controls[i].Show();
                    }
                    else
                    {
                        this.ParentForm.Controls[i].Hide();
                    }
                }
            }


        }

        public void ShowOrHideCtr(Type myObjType, bool myBShow)
        {

        }
        ///<summary>ctrMenü / OpenFrmWait</summary>
        ///<remarks>.</remarks>
        public void OpenFrmWait()
        {
            this._frmWait = new frmWait();
            this._frmWait.StartPosition = FormStartPosition.CenterScreen;
            this._frmWait.Show();
            this._frmWait.BringToFront();
        }
        ///<summary>ctrMenü / OpenFrmSettings</summary>
        ///<remarks>.</remarks>
        public void OpenFrmSettings()
        {
            this._frmSettingsView = new frmSettingsView();
            this._frmSettingsView._ctrMenu = this;
            this._frmSettingsView.MdiParent = this._frmMain.ParentForm;
            this._frmSettingsView.StartPosition = FormStartPosition.CenterScreen;
            this._frmSettingsView.Show();
            this._frmSettingsView.BringToFront();
        }


        ///<summary>ctrMenü / lArbeitsliste_Click</summary>
        ///<remarks>.</remarks>
        private void lArbeitsliste_Click(object sender, EventArgs e)
        {
            OpenCtrArbeitsliste(new ctrArbeitsliste());
        }
        ///<summary>ctrMenü / OpenCtrArbeitsliste</summary>
        ///<remarks>.</remarks>
        public void OpenCtrArbeitsliste(object myObj)
        {
            if (typeof(ctrArbeitsliste) == myObj.GetType())
            {
                OpenFrmTMP(myObj);
            }
        }
        ///<summary>ctrMenü / CloseCtrWorklist</summary>
        ///<remarks>.</remarks>
        public void CloseCtrWorklist()
        {
            this._frmMain.ResetStatusBar();
            Int32 Count = this.ParentForm.Controls.Count;

            if (this.ParentForm.Controls.Contains(_ctrWorklist))
                for (Int32 i = 0; (i <= (Count - 1)); i++)
                {
                    if (this.ParentForm.Controls[i].Name == const_TMP + "SplitterWorklist")
                    {
                        this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                        Count = this.ParentForm.Controls.Count;
                        i = 0;      // ist nur ein Controll vorhanden
                    }
                    if (this.ParentForm.Controls[i].GetType() == typeof(ctrBestand))
                    {
                        this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                        i = Count - 1;      // ist nur ein Controll vorhanden
                    }
                }
        }
        ///<summary>ctrMenü / OpenCtrWorklist</summary>
        ///<remarks>.</remarks>
        private void OpenCtrWorklist()
        {
            ctrWorklist ctrWorklist = new ctrWorklist();
            ctrWorklist.GL_User = GL_User;
            ctrWorklist._ctrMenu = this;
            ctrWorklist.Dock = DockStyle.Left;
            //ctrWorklist.Dock = DockStyle.Fill;
            ctrWorklist.Name = const_TMP + "SplitterWorklist";
            this.ParentForm.Controls.Add(ctrWorklist);
            this.ParentForm.Controls.SetChildIndex(ctrWorklist, 0);
            ctrWorklist.Show();

            // CreateSplitter(const_TMP + "SplitterWorklist");
        }

        ///<summary>ctrMenu / closeAll</summary>
        ///<remarks>.</remarks>
        public void closeAll()
        {
            Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                Console.WriteLine(this.ParentForm.Controls[i].Name);
            }
            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {

                //this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                //i = Count - 1;      // ist nur ein Controll vorhanden

                if (this.ParentForm.Controls[i].Name.Length >= 4 && this.ParentForm.Controls[i].Name.Substring(0, 4).Equals(const_TMP))
                {
                    //Console.WriteLine(this.ParentForm.Controls[i].Name);
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    i--;
                    Count = Count - 1;
                }
                //if (this.ParentForm.Controls[i].GetType() == typeof(ctrRGList))
                //{
                //    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                //    i = Count - 1;      // ist nur ein Controll vorhanden
                //}
            }

            OnPropertyChanged("Close");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName">Der Name der Eigenschaft</param>
        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
        ///<summary>ctrMenü / OpenCtrJournal</summary>
        ///<remarks>.</remarks>
        public void OpenCtrWaggonbuch()
        {
            bool open = false;
            Int32 j = -1;
            for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrWaggonbuch))
                {
                    open = true;
                    j = i;
                }
            }
            if (open)
            {

                //if (_ctrEinlagerung != null && _ctrEinlagerung.bFromBestand == true)
                //{
                //    CloseCtrEinlagerung();
                //}
                //if (_ctrAuslagerung != null && _ctrAuslagerung.bFromBestand == true)
                //{
                //    CloseCtrAuslagerung();
                //}

                this.ParentForm.Controls[j].Show();
            }
            else
            {
                _ctrWaggonbuch = new ctrWaggonbuch();
                _ctrWaggonbuch._ctrMenu = this;
                _ctrWaggonbuch.GL_User = this.GL_User;
                _ctrWaggonbuch._ctrMenu._frmMain.ResetStatusBar();
                _ctrWaggonbuch._ctrMenu._frmMain.InitStatusBar(3);

                _ctrWaggonbuch._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
                _ctrWaggonbuch._ctrMenu._frmMain.StatusBarWork(false, "Waggonbuch wird geladen...");

                _ctrWaggonbuch.Dock = DockStyle.Left;
                _ctrWaggonbuch.Name = const_TMP + "Waggonbuch";
                this.ParentForm.Controls.Add(_ctrWaggonbuch);
                this.ParentForm.Controls.SetChildIndex(_ctrWaggonbuch, 0);
                this._frmMain.ctrFormList.AddCtrToList(ref this._frmMain);
                _ctrWaggonbuch.Show();
                CreateSplitter(const_TMP + "SplitterWaggonbuch");
                _ctrWaggonbuch._ctrMenu._frmMain.StatusBarWork(false, string.Empty);
                _ctrWaggonbuch._ctrMenu._frmMain.ResetStatusBar();
            }
        }

        ///<summary>ctrMenü / CloseCtrWaggonbuch</summary>
        ///<remarks>.</remarks>
        public void CloseCtrWaggonbuch()
        {
            this._frmMain.ResetStatusBar();
            Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].Name == const_TMP + "SplitterWaggonbuch")
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    Count = this.ParentForm.Controls.Count;
                    i = 0;      // ist nur ein Controll vorhanden
                }
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrWaggonbuch))
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    i = Count - 1;      // ist nur ein Controll vorhanden
                }
            }
        }


        internal void CloseCtrReihe()
        {
            this._frmMain.ResetStatusBar();
            Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].Name == const_TMP + "SplitterLagerreihe")
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    Count = this.ParentForm.Controls.Count;
                    i = 0;      // ist nur ein Controll vorhanden
                }
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrReihen))
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    i = Count - 1;      // ist nur ein Controll vorhanden
                }
            }
        }
    }
}
