using Common.Models;
using LVS.Clients;
using System;
using System.Collections.Generic;

namespace LVS
{
    public class clsClient
    {
        internal SIL_spFunc silFunc = new SIL_spFunc();
        internal SZG_spFunc szgFunc = new SZG_spFunc();

        internal set_ALTHAUS setALTHAUS;
        internal set_COMTEC setCOMTC;
        internal set_HEISIEP setHeisiep;
        internal set_SIL setSIL;
        internal set_SLE setSLE;
        internal set_SZG setSZG;
        //internal set_VALERIUS setVALERIUS;


        internal clsINIDocuments INIDocuments;

        //---------------------------- Matchcode immer GROSS !!!! -------------------------------
        public const string const_ClientMatchcode_Althaus = "ALTHAUS";
        public const string const_ClientMatchcode_Comtec = "CTN";
        public const string const_ClientMatchcode_Honeselmann = "HONSELMANN";
        public const string const_ClientMatchcode_Heisiep = "HEISIEP";
        public const string const_ClientMatchcode_SIL = "SIL";
        public const string const_ClientMatchcode_SLE = "SLE";
        public const string const_ClientMatchcode_SZG = "SZG";
        public const string const_ClientMatchcode_Valerius = "VALERIUS";

        public const string const_ViewName_Abruf = "Abruf";
        public const string const_ViewName_Arbeitsliste = "Arbeitsliste";
        public const string const_ViewName_Journal = "Journal";
        public const string const_ViewName_JournalSPL = "JournalSPL";
        public const string const_ViewName_Bestand = "Bestand";
        public const string const_ViewName_InventurLager = "InventurLager";
        public const string const_ViewName_SPL = "SPL";

        public const string cont_ViewName_Bestand_Customized_AllWorkspaces = "Bestand_AlleArbeitsbereiche";

        public const string const_ViewKategorie_Abruf = "Abruf";
        public const string const_ViewKategorie_Arbeitsliste = "Arbeitsliste";
        public const string const_ViewKategorie_Journal = "Journal";
        public const string const_ViewKategorie_JournalSPL = "JournalSPL";
        public const string const_ViewKategorie_Bestand = "Bestand";
        public const string const_ViewKategorie_Search = "Search";
        public const string const_ViewKategorie_LAusgangArtikel = "LAusgangA";
        public const string const_ViewKategorie_LAusgang = "LAusgang";
        public const string const_ViewKategorie_LEingang = "LEingang";
        public const string const_ViewKategorie_SPL = "SPL";

        public Globals._GL_USER _GL_User;
        public Globals._GL_SYSTEM _GL_System;

        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = _GL_User.User_ID;
                return _BenutzerID;
            }
            set { _BenutzerID = value; }
        }

        public clsADR ADR;
        public clsModule Modul;

        public decimal AdrID = 0;
        public const Int32 DefaultUserAnzahl = 50;


        public string MatchCode { get; set; }
        public Int32 UserAnzahl { get; set; }

        //Vorgaben des Client
        public string Eingang_Artikel_DefaulEinheit { get; set; }
        public string Eingang_Artikel_DefaulAnzahl { get; set; }
        public bool Ausgang_DefaulTerminAktiv { get; set; }
        public Int32 DefaultASNParnter_Emp { get; set; }


        //Abrufe
        public Dictionary<decimal, decimal> DictArbeitsbereich_Abrufe_DefaulEmpfaengerAdrID { get; set; }
        public decimal Abrufe_DefaulEmpfaengerAdrID { get; set; }
        public Dictionary<decimal, decimal> DictArbeitsbereich_Abrufe_DefaultCompanyAdrID { get; set; }
        public decimal Abrufe_DefaulCompanyAdrID { get; set; }

        //EIngang
        public Dictionary<decimal, decimal> DictArbeitsbereich_Eingang_DefaultEmpfaengerAdrID { get; set; }
        public decimal Eingang_DefaultEmpfaengerAdrID { get; set; }
        public Dictionary<decimal, decimal> DictArbeitsbereich_Eingang_DefaultEntladeAdrID { get; set; }
        public decimal Eingang_DefaultEntladeAdrID { get; set; }

        //Ausgang
        public Dictionary<decimal, decimal> DictArbeitsbereich_Ausgang_DefaultEmpfaengerAdrID { get; set; }
        public decimal Ausgang_DefaultEmpfaengerAdrID { get; set; }
        public Dictionary<decimal, decimal> DictArbeitsbereich_Ausgang_DefaultEntladeAdrID { get; set; }
        public decimal Ausgang_DefaultEntladeAdrID { get; set; }
        public Dictionary<decimal, decimal> DictArbeitsbereich_Ausgang_DefaultVersenderAdrID { get; set; }
        public decimal Ausgang_DefaultVersenderAdrID { get; set; }
        public Dictionary<decimal, decimal> DictArbeitsbereich_Ausgang_DefaultBeladeAdrID { get; set; }
        public decimal Ausgang_DefaultBeladeAdrID { get; set; }

        //Umbbuchung
        public Dictionary<decimal, decimal> DictArbeitsbereich_Umbuchung_DefaultEmpfaengerAdrID { get; set; }
        public decimal Umbuchung_DefaultEmpfaengerAdrID { get; set; }
        public Dictionary<decimal, decimal> DictArbeitsbereich_Umbuchung_DefaultAuftraggeberNeuAdrID { get; set; }
        public decimal Umbuchung_DefaultAuftraggeberNeuAdrID { get; set; }

        public Dictionary<decimal, string> DictArtikelIDRefCreationAuftraggeber = new Dictionary<decimal, string>();
        public Dictionary<decimal, string> DictArtikelIDRefCreationEmpfaenger = new Dictionary<decimal, string>();

        public List<clsReportDocSetting> ListPrintCenterReportEinlagerung = new List<clsReportDocSetting>();
        public List<clsReportDocSetting> ListPrintCenterReportAuslagerung = new List<clsReportDocSetting>();



        public List<decimal> ListVDA4905Sender = new List<decimal>();
        public Dictionary<string, Dictionary<string, List<string>>> DictViews = new Dictionary<string, Dictionary<string, List<string>>>();
        public Dictionary<string, string> DictOrders = new Dictionary<string, string>();
        public Dictionary<string, Dictionary<string, List<string>>> DictPrintViews = new Dictionary<string, Dictionary<string, List<string>>>();

        /*************************************************************************************
         *                  Methoden / Procedures
         * **********************************************************************************/
        ///<summary>clsClient / InitClass</summary>
        ///<remarks>strClientMatchCode kommt hier aus der Ini</remarks>
        public void InitClass(string strClientMatchCode)
        {
            this.MatchCode = strClientMatchCode.ToUpper();
            this.DefaultASNParnter_Emp = 0;
            //Module
            Modul = new clsModule();
            SetModulToClient();
            INIDocuments = new clsINIDocuments();
            INIDocuments.InitClass();
        }
        ///<summary>clsClient / InitSubClass</summary>
        ///<remarks></remarks>
        public void InitSubClass()
        {
            ADR = new clsADR();
            ADR.InitClass(this._GL_User, this._GL_System, AdrID, false);
        }
        ///<summary>clsClient / SetModulToClient</summary>
        ///<remarks>Auflistung der Client Matchcodes bitte nach dem Alphabet. Hier werden dem einzelenen Client die 
        ///         speziellen Systemwerte zugewiesen.
        ///         Manuell hinzugefügt werden muss:we
        ///             - UserAnzahl
        ///             - AdrID aus der ADR Datenbank</remarks>
        private void SetModulToClient()
        {
            //Standardeinstellungen
            this.Eingang_Artikel_DefaulEinheit = "Stk";
            this.Eingang_Artikel_DefaulAnzahl = "1";
            this.Ausgang_DefaulTerminAktiv = true;

            AdrID = 0;
            if (MatchCode != string.Empty)
            {
                switch (MatchCode)
                {
                    case const_ClientMatchcode_Althaus + "_":
                        SetModulDefault();
                        //s/*et_ALTHAUS alh = new set_ALTHAUS()*/;
                        setALTHAUS = new set_ALTHAUS();
                        setALTHAUS.InitSettings(this);
                        SetViewsDefault();
                        break;

                    case "COMTEC_":
                    case "CTN_":
                        SetModulDefault();
                        setCOMTC = new set_COMTEC();
                        setCOMTC.InitSettings(this);
                        SetViewsDefault();
                        break;

                    case const_ClientMatchcode_Heisiep + "_":
                        //01.09.2013 - Lager eingeführt
                        //SetModulDefault();
                        //SetViewsDefault();
                        //SetModulToClientHeisiep_();
                        //SetViewsToClientHeisiep_();
                        //this.Eingang_Artikel_DefaulEinheit = "kg";
                        //this.UserAnzahl = 10;
                        //AdrID = 1189;  //manuell hinzugefügt aus der ensprechenden Datenbank

                        //ab 14.07.2016 
                        SetModulDefault();
                        setHeisiep = new set_HEISIEP();
                        setHeisiep.InitSettings(this);
                        SetViewsDefault();
                        break;

                    case const_ClientMatchcode_Honeselmann + "_":
                        SetModulDefault();
                        SetModulToClientHonselmann_();
                        SetViewsToClientHonselmann_();
                        SetViewsDefault();
                        //Eingang
                        this.Eingang_Artikel_DefaulEinheit = "kg";
                        this.Eingang_Artikel_DefaulAnzahl = "1";
                        this.UserAnzahl = DefaultUserAnzahl;
                        this.Ausgang_DefaulTerminAktiv = false;
                        AdrID = 1;  //manuell hinzugefügt
                        break;

                    case const_ClientMatchcode_SLE + "_":
                        SetModulDefault();
                        setSLE = new set_SLE();
                        setSLE.InitSettings(this);
                        SetViewsDefault();
                        break;

                    case const_ClientMatchcode_SIL + "_":
                        SetModulDefault();
                        setSIL = new set_SIL();
                        setSIL.InitSettings(this);
                        SetViewsDefault();
                        break;

                    //SZG in Glauchau
                    //case "SK_":
                    case const_ClientMatchcode_SZG + "_":
                        SetModulDefault();
                        setSZG = new set_SZG();
                        setSZG.InitSettings(this);
                        SetViewsDefault();
                        break;

                    ////Valerius
                    //case const_ClientMatchcode_Valerius + "_":
                    //    SetModulDefault();
                    //    setVALERIUS = new set_VALERIUS();
                    //    setVALERIUS.InitSettings(this);
                    //    SetViewsDefault();
                    //    break;

                    default:
                        //hier könnte das Programm dann beendet werden
                        //Baustelle ????
                        break;
                }
            }
        }
        ///<summary>clsClient / SetModulDefault</summary>
        ///<remarks>Alle Module werden freiggegeben</remarks>
        private void SetModulDefault()
        {
            //Hauptmenu
            this.Modul.MainMenu_Stammdaten = true;
            this.Modul.MainMenu_Statistik = true;
            this.Modul.MainMenu_Disposition = true;
            this.Modul.MainMenu_Fakurierung = true;
            this.Modul.MainMenu_Lager = true;
            this.Modul.MainMenu_AuftragserfassungDispo = true;

            //STammdaten
            this.Modul.Stammdaten_Adressen = true;              //Standard
            this.Modul.Stammdaten_Personal = true;              //Standard
            this.Modul.Stammdaten_Fahrzeuge = true;             //Standard
            this.Modul.Stammdaten_Gut = true;                   //Standard
            this.Modul.Stammdaten_Gut_UseGutDefinition = true;    //Standard
            this.Modul.Stammdaten_Gut_UseGutDefinitionByASNTransfer = true;
            this.Modul.Stammdaten_GutShowAllwaysAll = true;
            this.Modul.Stammdaten_UseGutAdrAssignment = false;                 //Standard
            this.Modul.Stammdaten_Relation = true;              //Standard
            this.Modul.Stammdaten_Lagerortverwaltung = true;    //Standard
            this.Modul.Stammdaten_Lagerreihenverwaltung = (!this.Modul.Stammdaten_Lagerortverwaltung);
            this.Modul.Stammdaten_Schaeden = true;              //Standard
            this.Modul.Stammdaten_Einheiten = true;             //Standard
            this.Modul.Stammdaten_ExtraCharge = false;
            this.Modul.Stammdaten_KontenPlan = false;
            this.Modul.Stammdaten_StorelocationLable = false;

            //Archiv
            this.Modul.Archiv = false;

            //Spedition
            this.Modul.Spedition = true;
            this.Modul.Spedition_Auftragserfassung = true;
            this.Modul.Spedition_Dispo = true;


            //Lagerverwaltung
            this.Modul.Lager_SearchGridInLifeTime = true;
            this.Modul.Lager_Einlagerung = true;
            this.Modul.Lager_Einlagerung_RetourBooking = false;
            this.Modul.Lager_Einlagerung_Print_DirectEingangDoc = false;
            this.Modul.Lager_Einlagerung_Print_DirectList = false;
            this.Modul.Lager_Einlagerung_Print_DirectLabel = false;
            this.Modul.Lager_Einlagerung_Print_DirectLabelAfterCheckEingang = false;
            this.Modul.Lager_Einlagerung_ShowDirectPrintCenter = true;
            this.Modul.Lager_Einlagerung_LagerOrt_manuell_Changeable = false;
            this.Modul.Lager_Einlagerung_ClearLagerOrteByArtikelCopy = false;
            this.Modul.Lager_Einlagerung_LagerOrt_Enabled_Werk = true;
            this.Modul.Lager_Einlagerung_LagerOrt_Enabled_Halle = true;
            this.Modul.Lager_Einlagerung_LagerOrt_Enabled_Reihe = true;
            this.Modul.Lager_Einlagerung_LagerOrt_Enabled_Ebene = true;
            this.Modul.Lager_Einlagerung_LagerOrt_Enabled_Platz = true;
            this.Modul.Lager_Einlagerung_EditAfterClose = true;
            this.Modul.Lager_Einlagerung_EditADRAfterClose = true;
            this.Modul.Lager_Einlagerung_EditExTransportRef = false;
            this.Modul.Lager_Einlagerung_InkrementArtikelPos = true;
            this.Modul.Lager_Einlagerung_ArtikelIDRef_Create = false;
            this.Modul.Lager_Einlagerung_CheckAllArtikel = false;
            this.Modul.Lager_Einlagerung_GArt_InfoMessageAllData = true;
            //this.Modul.Lager_Einlagerung_Reihenvorschlag = false;
            this.Modul.Lager_Einlagerung_CheckComplete = true;
            this.Modul.Lager_Einlagerung_SetCheckDate = false;
            this.Modul.Lager_Einlagerung_BruttoEqualsNetto = false;
            this.Modul.Lager_Einlagerung_Enabeled_Einheit = true;

            this.Modul.Lager_Eingang_FreeForChange = false;

            //Eingang Pfichtfelder
            this.Modul.Lager_Einlagerung_RequiredValue_Auftraggeber = true;
            this.Modul.Lager_Einlagerung_RequiredValue_LieferscheinNr = true;
            this.Modul.Lager_Einlagerung_RequiredValue_Vehicle = false;
            this.Modul.Lager_Einlagerung_RequiredValue_Halle = false;
            this.Modul.Lager_Einlagerung_RequiredValue_Reihe = true;

            this.Modul.Lager_Einlagerung_Artikel_RequiredValue_Produktionsnummer = false;
            this.Modul.Lager_Einlagerung_Artikel_RequiredValue_Netto = false;
            this.Modul.Lager_Einlagerung_Artikel_RequiredValue_Brutto = false;
            this.Modul.Lager_Einlagerung_Artikel_RequiredValue_Laenge = false;


            this.Modul.Lager_WaggonNo_Mask = string.Empty;
            this.Modul.Lager_DisplaySPLArtikelinAusgang = false;

            this.Modul.Lager_AskForDeleteEA = true;

            this.Modul.Lager_USEBKZ = false;

            this.Modul.Lager_Artikel_UseKorreturStornierVerfahren = false;

            this.Modul.Lager_UB_ArikelProduktionsnummerChange = false;   //Standard

            // SPL - Sperrlager
            this.Modul.Lager_SPL_SchadenRequire = false;
            this.Modul.Lager_SPL_OutFromEingang = false;
            this.Modul.Lager_SPL_AutoSPL = false;
            this.Modul.Lager_SPL_PrintSPLDocument = false;
            this.Modul.Lager_SPL_AutoPrintSPLDocument = false;
            this.Modul.Lager_SPL_RebookInAltEingang = false;

            //Lager / Module
            this.Modul.Lager_Umbuchung = true;      //Standard
            this.Modul.Lager_Journal = true;        //Standard
            this.Modul.Lager_Bestandsliste = true;  //Standard
            this.Modul.Lager_Bestandsliste_TagesbestandOhneSPL = false;
            this.Modul.Lager_Bestandsliste_PrintButtonReport_Bestand = true;  //Print Bestand über Report
            this.Modul.Lager_Bestandsliste_PrintButtonReport_Inventur = true;  // Print Inventur über Report
            this.Modul.Lager_Bestandsliste_PrintButtonGrid = true;
            this.Modul.Lager_Bestandsliste_BestandOverAllWorkspaces = false;
            this.Modul.Lager_Sperrlager = true;     //Standard
            this.Modul.Lager_FreeForCall = false;
            this.Modul.Lager_PostCenter = true;
            this.Modul.Lager_Arbeitsliste = false;

            //...|LvsScan
            this.Modul.LvsScan = false;
            this.Modul.LvsScan_Inventory_List = false;
            this.Modul.LvsScan_Inventory_Scan = false;


            //Lagerverwaltung | DirectDelivery
            this.Modul.Lager_DirectDelivery = true;
            this.Modul.Lager_DirectDeliveryTransformation = true;

            //...|Schaeden
            this.Modul.Lager_Schaeden_ShowPrintCenterAfterSelection = false;

            //ASN Verkehr
            this.Modul.ASNTransfer = false;
            this.Modul.ASN_Create_Man = this.Modul.ASNTransfer;
            this.Modul.ASN_Create_Auto = this.Modul.ASNTransfer;
            this.Modul.ASN_VDA4905LiefereinteilungenAktiv = this.Modul.ASNTransfer;
            this.Modul.ASN_VDA4905LiefereinteilungenAktiv_SupplierDetails = false;
            this.Modul.ASN_VDA4913_LVS_ReadASN_TakeOverGArtValues = false;
            this.Modul.ASN_AutoCreateNewGArtByASN = false;
            this.Modul.ASN_UserOldASNFileCreation = false;
            this.Modul.ASN_UseNewASNCreateFunction = false;

            //ASNCall 
            this.Modul.ASNCall_UserCallStatus = false;

            //Auslagerung
            this.Modul.Lager_Auslagerung = true;
            this.Modul.Lager_Auslagerung_Print_DirectAusgangDoc = false;
            this.Modul.Lager_Auslagerung_Print_DirectAusgangListe = false;
            this.Modul.Lager_Auslagerung_ShowDirectPrintCenter = true;
            this.Modul.Lager_Auslagerung_DGVBestand_SortID_1 = false;
            this.Modul.Lager_Auslagerung_EditAfterClose = true;
            this.Modul.Lager_Auslagerung_Print_AdditionalTransportDoc = false;
            this.Modul.Lager_Auslagerung_CheckComplete = true;
            this.Modul.Lager_Auslagerung_StoreOutDirect = true;

            //Auslagerung Pflichtfelder
            this.Modul.Lager_Auslagerung_RequiredReceiver = false;

            //Fakturierung
            this.Modul.Fakt_Lager = true;
            this.Modul.Fakt_Spedition = true;
            this.Modul.Fakt_Manuell = true;
            this.Modul.Fakt_Sonderkosten = false;   //Sondermodul
            this.Modul.Fakt_UB_DifferentCalcAssignment = false;
            this.Modul.Fakt_Rechnungsbuch = false;
            this.Modul.Fakt_LagerManuellSelection = false;
            this.Modul.Fakt_SendRGAnhangMailExcel = false;
            this.Modul.Fakt_GetRGGSNrFromTable_Primekey = true;
            this.Modul.Fakt_GetRGGSNrFromTable_Mandant = false;
            this.Modul.Fakt_DeactivateMenueCtrRGList = false;
            this.Modul.Fakt_UseOneRGNrKreisForRGandGS = false;
            this.Modul.Fakt_CalcSLSVMaterialkosten = false;
            this.Modul.Fakt_eInvoiceIsAvailable = true;

            this.Modul.PrimeyKey_LVSNRUseOneIDRange = false;

            this.Modul.EnableAdvancedSearch = true;
            this.Modul.EnableDirectSearch = true;
            this.Modul.EnableEditExAuftrag = false;

            //--- ReadOnly
            this.Modul.ReadOnly_Artikel_IsNOTStackable = true;

            // STatistik
            this.Modul.Statistik_Lager = true;
            this.Modul.Statistik_FaktLager = false;
            this.Modul.Statistik_FaktDispo = false;
            this.Modul.Statistik_Waggonbuch = false;
            this.Modul.Statistik_Gesamtbestand = false;
            this.Modul.Statistik_Bestandsbewegungen = false;
            this.Modul.Statistik_durchschn_Lagerbestand = false;
            this.Modul.Statistik_druchschn_Lagerdauer = false;
            this.Modul.Statistik_Monatsuebersicht = false;


            this.Modul.Excel_UseOldExport = true;

            /*******************************************************
             *          Menüs
             * ****************************************************/

            this.Modul.Menu_Einlagerung_Artikel_tsbtnLagerort = true;

            //Mailkontodaten leeren
            this.Modul.Mail_UsingMainMailForMailing = true;
            this.Modul.Mail_UsingNoReplyDefault = true;

            this.Modul.Mail_SMTPServer = "smtp.1und1.de";
            this.Modul.Mail_SMTPUser = "support@softkonzept.com";
            this.Modul.Mail_SMTPPasswort = "!29suPP%1Ay33e&fcdW";
            this.Modul.Mail_MailAdress = "support@softkonzept.com";
            this.Modul.Mail_SMTPPort = 587;
            this.Modul.Mail_SMTPSSL = true;

            //this.Modul.Mail_SMTPServer = string.Empty;
            //this.Modul.Mail_SMTPUser = string.Empty;
            //this.Modul.Mail_SMTPPasswort = string.Empty;
            //this.Modul.Mail_MailAdress = string.Empty;
            //this.Modul.Mail_SMTPPort = 587;
            //this.Modul.Mail_SMTPSSL = true;


            this.Modul.Mail_Noreply_SMTPServer = this.Modul.Mail_SMTPServer;
            this.Modul.Mail_Noreply_SMTPUser = this.Modul.Mail_SMTPUser;
            this.Modul.Mail_Noreply_SMTPPasswort = this.Modul.Mail_SMTPPasswort;
            this.Modul.Mail_Noreply_MailAdress = this.Modul.Mail_MailAdress;
            this.Modul.Mail_Noreply_SMTPPort = this.Modul.Mail_SMTPPort;
            this.Modul.Mail_Noreply_SMTPSSL = this.Modul.Mail_SMTPSSL;

            /****************************************************
             *                  Print
             * *************************************************/
            this.Modul.Print_OldVersion = true;
            this.Modul.Print_LieferscheinOhneAbschluss = true;
            this.Modul.Print_GridPrint_ViewByGridPrint_Bestandsliste = false;
            this.Modul.Print_GridPrint_ViewByGridPrint_Journal = false;
            this.Modul.Print_Documents_UseRGAnhang = true;

            /****************************************************
            *                   TELERIK 
            ****************************************************/
            //..|Grid
            this.Modul.Telerik_GridPrint_SummaryRow = true;


            /**************************************************
             *                 Communicator 
             * ***********************************************/

            //Direkte Verarbeitung der XML Dateien
            this.Modul.Xml_Uniport_CreateDirect_LEingang = false;
            this.Modul.Xml_Uniport_CreateDirect_LAusgang = false;
            this.Modul.VDA_Use_KFZ = true;

        }

        /***********************************************************************************
         * Ab hier kommen die Funktionen für jeden einzelnen Clienten - auch ab hier bitte
         * Sortierung alphabetisch des ClientMatchcodes. In der einzelnen Funktion wird 
         * dann je nach Client das entsprechende Modul freigegeben.
         * Funktionsname  "SetModulToClient"+ClientMatchcode
         * *********************************************************************************/

        /********************************************************************************************************
         *                                  Honselmann -> Sped. Honselmann Hagen
         * ******************************************************************************************************/
        ///<summary>clsClient / SetModulToClientSZG_</summary>
        ///<remarks>Spedition Honselmann
        ///         Datum: 25.03.2014 
        ///         Änderungen: </remarks>
        private void SetModulToClientHonselmann_()
        {
            //22.04.2014 lt. Frau Löher ausblenden
            this.Modul.Stammdaten_Personal = false;
            this.Modul.Stammdaten_Fahrzeuge = false;
            this.Modul.Stammdaten_Lagerortverwaltung = false;
            this.Modul.Stammdaten_Gut_UseGutDefinition = false;
            this.Modul.Stammdaten_ExtraCharge = true;
            this.Modul.Stammdaten_KontenPlan = true;
            this.Modul.Stammdaten_StorelocationLable = false;

            // Print
            this.Modul.Print_OldVersion = true;
            this.Modul.Print_LieferscheinOhneAbschluss = false;

            //Disposition
            this.Modul.Spedition = false;
            this.Modul.Spedition_Auftragserfassung = false;
            this.Modul.Spedition_Dispo = false;

            //Lagerverwaltung 
            this.Modul.Lager_SearchGridInLifeTime = false;
            this.Modul.Lager_DirectDeliveryTransformation = false;
            this.Modul.Lager_DirectDelivery = false;    //28.04.2014 Frau Löher
            this.Modul.Lager_FreeForCall = true;
            this.Modul.Lager_PostCenter = true;
            this.Modul.Lager_Arbeitsliste = true;

            //ASN Verkehr
            this.Modul.ASNTransfer = true;
            this.Modul.ASN_Create_Man = false;
            this.Modul.ASN_Create_Auto = true;
            this.Modul.ASN_VDA4905LiefereinteilungenAktiv = false;

            //..|Einlagerung
            this.Modul.Lager_Einlagerung_Print_DirectEingangDoc = true;
            this.Modul.Lager_Einlagerung_LagerOrt_manuell_Changeable = true;
            this.Modul.Lager_Einlagerung_ClearLagerOrteByArtikelCopy = true;
            this.Modul.Lager_Einlagerung_LagerOrt_Enabled_Werk = false;
            this.Modul.Lager_Einlagerung_LagerOrt_Enabled_Halle = true;
            this.Modul.Lager_Einlagerung_LagerOrt_Enabled_Reihe = true;
            this.Modul.Lager_Einlagerung_LagerOrt_Enabled_Ebene = false;
            this.Modul.Lager_Einlagerung_LagerOrt_Enabled_Platz = false;
            //EIngang Pflichtfelder
            this.Modul.Lager_Einlagerung_RequiredValue_Halle = true;
            this.Modul.Lager_Einlagerung_RequiredValue_Reihe = true;
            //Eingang Artikel Pflichtfelder
            this.Modul.Lager_Einlagerung_Artikel_RequiredValue_Produktionsnummer = true;
            this.Modul.Lager_Einlagerung_ArtikelIDRef_Create = true;
            this.Modul.Lager_Einglagerung_ArtikelIDRef_CreateProzedure = clsModule.const_Lager_Einlagerung_ArtikelIDRef_Hons;
            this.Modul.Lager_Einlagerung_EditAfterClose = true;
            this.Modul.Lager_Einlagerung_EditADRAfterClose = false;
            this.Modul.Lager_Einlagerung_EditExTransportRef = true;
            this.Modul.Lager_Einlagerung_CheckAllArtikel = true;

            //...|Umlagerung
            this.Modul.Lager_UB_ArikelProduktionsnummerChange = true;

            //..|Auslagerung
            this.Modul.Lager_Auslagerung_EditAfterClose = true;
            this.Modul.Lager_Auslagerung_DGVBestand_SortID_1 = true; //spezielle für Honselmann
            this.Modul.Lager_Auslagerung_Print_DirectAusgangDoc = true;
            this.Modul.Lager_Auslagerung_Print_AdditionalTransportDoc = true;

            this.Modul.Lager_WaggonNo_Mask = "0000 0000 000-0";
            this.Modul.Lager_DisplaySPLArtikelinAusgang = true;

            this.Modul.Lager_AskForDeleteEA = true;
            //Erweiterete Suche
            this.Modul.EnableAdvancedSearch = true;
            this.Modul.EnableDirectSearch = false;

            // Statistik 
            this.Modul.Statistik_Waggonbuch = true;
            this.Modul.Statistik_Gesamtbestand = true;
            this.Modul.Statistik_Bestandsbewegungen = true;
            this.Modul.Statistik_durchschn_Lagerbestand = true;
            this.Modul.Statistik_druchschn_Lagerdauer = true;
            this.Modul.Statistik_Monatsuebersicht = true;

            this.Modul.Excel_UseOldExport = false;

            //raus??? -> prüfen
            DictArtikelIDRefCreationAuftraggeber = new Dictionary<decimal, string>()
            {
                { 19, "SAG" }
            };

            this.Modul.Lager_Arbeitsliste = true;

            //Lagerverwaltung - Einlagerung
            this.Modul.Menu_Einlagerung_Artikel_tsbtnLagerort = false;

            //Fakturierung
            this.Modul.Fakt_Sonderkosten = true;
            this.Modul.Fakt_UB_DifferentCalcAssignment = true;
            this.Modul.Fakt_Rechnungsbuch = true;
            this.Modul.Fakt_SendRGAnhangMailExcel = true;


            //Mail
            this.Modul.Mail_UsingMainMailForMailing = true;
            this.Modul.Mail_UsingNoReplyDefault = true;

            this.Modul.Mail_SMTPServer = "smtp.1und1.de";
            this.Modul.Mail_SMTPUser = "auto-mail-hlu@honselmann.de";
            this.Modul.Mail_SMTPPasswort = "auto-mail-hlu-58089";
            this.Modul.Mail_MailAdress = "auto-mail-hlu@honselmann.de";
            this.Modul.Mail_SMTPPort = 25;
            this.Modul.Mail_SMTPSSL = false;

            this.Modul.Mail_Noreply_SMTPServer = this.Modul.Mail_SMTPServer;
            this.Modul.Mail_Noreply_SMTPUser = this.Modul.Mail_SMTPUser;
            this.Modul.Mail_Noreply_SMTPPasswort = this.Modul.Mail_SMTPPasswort;
            this.Modul.Mail_Noreply_MailAdress = this.Modul.Mail_MailAdress;
            this.Modul.Mail_Noreply_SMTPPort = this.Modul.Mail_SMTPPort;
            this.Modul.Mail_Noreply_SMTPSSL = this.Modul.Mail_SMTPSSL;

            // Entsperren des Eingabefeldes für exAuftragnummer für Honselmann
            this.Modul.EnableEditExAuftrag = true;

            this.Modul.Lager_SPL_OutFromEingang = true;
        }
        ///<summary>clsClient / SetViewsToClientHonselmann_</summary>
        ///<remarks></remarks>
        private void SetViewsToClientHonselmann_()
        {
            //Dictionary<string, List<string>> BestandViews = new Dictionary<string, List<string>>();
            //Dictionary<string, List<string>> BestandPrund intViews = new Dictionary<string, List<string>>();
            List<string> Artikel;

            // ABMESSUNG
            Artikel = new List<string>();
            Artikel.Add("Status");            // BKZ?
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Brutto");
            Artikel.Add("Netto");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("ArtIDRef"); //Produkt.Nr.
            Artikel.Add("LVSNr");
            Artikel.Add("Auftraggeber");      //KD.Nr.(Empf./Abs.)
            Artikel.Add("Halle");
            Artikel.Add("Reihe");    // Reihe
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Eingang");             //E-ScheinNr.
            Artikel.Add("WaggonNo");                      // fehlt in Abfrage
            //Artikel.Add("Lfs-Datum");
            Artikel.Add("Lieferschein");  // LS.Nr.

            AddToView("Bestand", "Generell", Artikel, false, "LVSNr");
            AddToView("Freigabe für Abruf", "Generell", Artikel, false);
            //AddToView("LAusgang", "Generell", Artikel, false);

            // KUNDE / ABMESSUNG
            Artikel = new List<string>();
            Artikel.Add("Status");            // Status?
            Artikel.Add("Auftraggeber");      //KD.Nr.(Empf./Abs.)
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Brutto");
            Artikel.Add("Netto");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("ArtIDRef"); //Produkt.Nr.
            Artikel.Add("LVSNr");
            Artikel.Add("Halle");
            Artikel.Add("Reihe");    // Reihe
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Eingang");             //E-ScheinNr.
            Artikel.Add("WaggonNo");
            // Artikel.Add("Lfs-Datum");
            Artikel.Add("Lieferschein");  // LS.Nr.

            AddToView("Bestand", "Kunde/Abmessung", Artikel, false);
            AddToView("Freigabe für Abruf", "Kunde/Abmessung", Artikel, false);
            //AddToView("LAusgang", "Kunde/Abmessung", Artikel, false);
            // Gewicht
            Artikel = new List<string>();
            Artikel.Add("Status");            // Status?
            Artikel.Add("Brutto");
            Artikel.Add("Netto");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("ArtIDRef"); //Produkt.Nr.
            Artikel.Add("LVSNr");
            Artikel.Add("Auftraggeber");      //KD.Nr.(Empf./Abs.)
            Artikel.Add("Halle");
            Artikel.Add("Reihe");    // Reihe
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Eingang");             //E-ScheinNr.
            Artikel.Add("Bemerkung"); // ??
            Artikel.Add("WaggonNo");
            // Artikel.Add("Lfs-Datum");
            Artikel.Add("Lieferschein");  // LS.Nr.

            AddToView("Bestand", "Gewicht", Artikel, false);
            AddToView("Freigabe für Abruf", "Gewicht", Artikel, false);
            //AddToView("LAusgang", "Gewicht", Artikel, false);

            // LVS_NR
            Artikel = new List<string>();
            Artikel.Add("Status");            // Status?
            Artikel.Add("LVSNr");
            Artikel.Add("Auftraggeber");      //KD.Nr.(Empf./Abs.)
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Brutto");
            Artikel.Add("Netto");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("ArtIDRef"); //Produkt.Nr.
            Artikel.Add("Halle");
            Artikel.Add("Reihe");    // Reihe
            Artikel.Add("Eingangsdatum");
            // Artikel.Add("Lfs-Datum");       // A-ScheinNr
            Artikel.Add("Lieferschein");  // LS.Nr.

            AddToView("Bestand", "LVS Nr.", Artikel, false);
            AddToView("Freigabe für Abruf", "LVS Nr.", Artikel, false);
            //AddToView("LAusgang", "LVS Nr.", Artikel, false);
            // ProdNummer
            Artikel = new List<string>();
            Artikel.Add("Status");            // Status?
            Artikel.Add("Freigabe");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("ArtIDRef"); //Produkt.Nr.
            Artikel.Add("Auftraggeber");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Brutto");
            Artikel.Add("Netto");
            Artikel.Add("Halle");
            Artikel.Add("Reihe");    // Reihe
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Ausgang");             //E-ScheinNr.
            //Artikel.Add("Lfs-Datum"); // ??
            Artikel.Add("Lieferschein");

            AddToView("Bestand", "Prod.Nr.", Artikel, false);
            AddToView("Freigabe für Abruf", "Prod.Nr.", Artikel, false);
            //AddToView("LAusgang", "Prod.Nr.", Artikel, false);

            // Materialnummer
            Artikel = new List<string>();
            Artikel.Add("Status");            // Status?
            Artikel.Add("exMaterialnummer");  //Mat.Nr.
            Artikel.Add("MaterialNr");
            Artikel.Add("Freigabe");
            Artikel.Add("LZZ");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Brutto");
            Artikel.Add("Netto");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("ArtIDRef"); //Produkt.Nr.
            Artikel.Add("LVSNr");
            Artikel.Add("Auftraggeber");      //KD.Nr.(Empf./Abs.)
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Halle");
            Artikel.Add("Reihe");    // Reihe
            //Artikel.Add("Lfs-Datum");
            Artikel.Add("Ausgang");       // A-ScheinNr

            AddToView("Bestand", "Materialnr.", Artikel, false);
            AddToView("Freigabe für Abruf", "Materialnr.", Artikel, false);
            // AddToView("LAusgang", "Materialnr.", Artikel, false);

            Artikel = new List<string>();
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Brutto");
            Artikel.Add("Netto");
            //Artikel.Add("Lagerort");
            Artikel.Add("Halle");
            Artikel.Add("Reihe");    // Reihe
            Artikel.Add("Bemerkung"); // ??

            AddToView("Bestand", "Druck", Artikel, true);
            AddToView("Journal", "Druck", Artikel, true);

            // JOURNAL -------------------------------------


            // ABMESSUNG
            Artikel = new List<string>();
            Artikel.Add("Status");            // BKZ?
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Brutto");
            Artikel.Add("Netto");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("ArtIDRef"); //Produkt.Nr.
            Artikel.Add("LVSNr");
            Artikel.Add("Auftraggeber");      //KD.Nr.(Empf./Abs.)
            Artikel.Add("Halle");
            Artikel.Add("Reihe");    // Reihe
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Eingang");             //E-ScheinNr.
            Artikel.Add("Ausgangsdatum");
            Artikel.Add("Ausgang");
            Artikel.Add("WaggonNo");                      // fehlt in Abfrage 
            //Artikel.Add("Lfs-Datum");
            Artikel.Add("Lieferschein");  // LS.Nr.

            AddToView("Journal", "Generell", Artikel, false);
            AddToView("Search", "Generell", Artikel, false);

            // KUNDE / ABMESSUNG
            Artikel = new List<string>();
            Artikel.Add("Status");            // Status?
            Artikel.Add("Auftraggeber");      //KD.Nr.(Empf./Abs.)
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Brutto");
            Artikel.Add("Netto");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("ArtIDRef"); //Produkt.Nr.
            Artikel.Add("LVSNr");
            Artikel.Add("Halle");
            Artikel.Add("Reihe");    // Reihe
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Eingang");             //E-ScheinNr.
            Artikel.Add("Ausgangsdatum");
            Artikel.Add("Ausgang");
            Artikel.Add("WaggonNo");
            // Artikel.Add("Lfs-Datum");
            Artikel.Add("Lieferschein");  // LS.Nr.

            AddToView("Journal", "Kunde/Abmessung", Artikel, false);
            // Gewicht
            Artikel = new List<string>();
            Artikel.Add("Status");            // Status?
            Artikel.Add("Brutto");
            Artikel.Add("Netto");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("ArtIDRef"); //Produkt.Nr.
            Artikel.Add("LVSNr");
            Artikel.Add("Auftraggeber");      //KD.Nr.(Empf./Abs.)
            Artikel.Add("Halle");
            Artikel.Add("Reihe");    // Reihe
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Eingang");             //E-ScheinNr.
            Artikel.Add("Ausgangsdatum");
            Artikel.Add("Ausgang");
            Artikel.Add("Bemerkung"); // ??
            Artikel.Add("WaggonNo");
            // Artikel.Add("Lfs-Datum");
            Artikel.Add("Lieferschein");  // LS.Nr.

            AddToView("Journal", "Gewicht", Artikel, false);

            // LVS_NR
            Artikel = new List<string>();
            Artikel.Add("Status");            // Status?
            Artikel.Add("LVSNr");
            Artikel.Add("Auftraggeber");      //KD.Nr.(Empf./Abs.)
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Brutto");
            Artikel.Add("Netto");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("ArtIDRef"); //Produkt.Nr.
            Artikel.Add("Halle");
            Artikel.Add("Reihe");    // Reihe
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Eingang");
            Artikel.Add("Ausgangsdatum");
            Artikel.Add("Ausgang");
            // Artikel.Add("Lfs-Datum");       // A-ScheinNr
            Artikel.Add("Lieferschein");  // LS.Nr.

            AddToView("Journal", "LVS Nr.", Artikel, false);
            // ProdNummer
            Artikel = new List<string>();
            Artikel.Add("Status");            // Status?
            Artikel.Add("Freigabe");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("ArtIDRef"); //Produkt.Nr.
            Artikel.Add("Auftraggeber");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Brutto");
            Artikel.Add("Netto");
            Artikel.Add("Halle");
            Artikel.Add("Reihe");    // Reihe
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Eingang");             //E-ScheinNr.
            Artikel.Add("Ausgangsdatum");
            Artikel.Add("Ausgang");
            //Artikel.Add("Lfs-Datum"); // ??
            Artikel.Add("Lieferschein");

            AddToView("Journal", "Prod.Nr.", Artikel, false);

            // Materialnummer
            Artikel = new List<string>();
            Artikel.Add("Status");            // Status?
            Artikel.Add("exMaterialnummer");  //Mat.Nr.
            Artikel.Add("MaterialNr");
            Artikel.Add("Freigabe");
            Artikel.Add("LZZ");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Brutto");
            Artikel.Add("Netto");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("ArtIDRef"); //Produkt.Nr.
            Artikel.Add("LVSNr");
            Artikel.Add("Auftraggeber");      //KD.Nr.(Empf./Abs.)
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Eingang");
            Artikel.Add("Ausgangsdatum");
            Artikel.Add("Ausgang");
            Artikel.Add("Halle");
            Artikel.Add("Reihe");    // Reihe
            //Artikel.Add("Lfs-Datum");


            AddToView("Journal", "Materialnr.", Artikel, false);

            //AddToView("Freigabe für Abruf", "Druck", Artikel, true);
            //AddToView("Arbeitsliste", "Druck", Artikel, true);
            //*********************** AUSLAGERUNG *****************//

            // ABMESSUNG
            Artikel = new List<string>();
            Artikel.Add("Selected");
            Artikel.Add("Status");            // Status?
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Brutto");
            Artikel.Add("Netto");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("ArtIDRef"); //Produkt.Nr.
            Artikel.Add("LVSNr");
            Artikel.Add("Auftraggeber");      //KD.Nr.(Empf./Abs.)
            Artikel.Add("Halle");
            Artikel.Add("Reihe");    // Reihe
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Eingang");             //E-ScheinNr.
            Artikel.Add("WaggonNo");                      // fehlt in Abfrage
            Artikel.Add("Lieferschein");  // LS.Nr.

            AddToView("LAusgang", "Generell", Artikel, false);

            // KUNDE / ABMESSUNG
            Artikel = new List<string>();
            Artikel.Add("Selected");
            Artikel.Add("Status");            // Status?
            Artikel.Add("Auftraggeber");      //KD.Nr.(Empf./Abs.)
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Brutto");
            Artikel.Add("Netto");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("ArtIDRef"); //Produkt.Nr.
            Artikel.Add("LVSNr");
            Artikel.Add("Halle");
            Artikel.Add("Reihe");    // Reihe
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Eingang");             //E-ScheinNr.
            Artikel.Add("WaggonNo");
            Artikel.Add("Lieferschein");  // LS.Nr.

            AddToView("LAusgang", "Kunde/Abmessung", Artikel, false);

            // Gewicht
            Artikel = new List<string>();
            Artikel.Add("Selected");
            Artikel.Add("Status");            // Status?
            Artikel.Add("Brutto");
            Artikel.Add("Netto");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("ArtIDRef"); //Produkt.Nr.
            Artikel.Add("LVSNr");
            Artikel.Add("Auftraggeber");      //KD.Nr.(Empf./Abs.)
            Artikel.Add("Halle");
            Artikel.Add("Reihe");    // Reihe
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Eingang");             //E-ScheinNr.
            Artikel.Add("Bemerkung"); // ??
            Artikel.Add("WaggonNo");
            Artikel.Add("Lieferschein");  // LS.Nr.

            AddToView("LAusgang", "Gewicht", Artikel, false);

            // LVS_NR
            Artikel = new List<string>();
            Artikel.Add("Selected");
            Artikel.Add("Status");            // Status?
            Artikel.Add("LVSNr");
            Artikel.Add("Auftraggeber");      //KD.Nr.(Empf./Abs.)
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Brutto");
            Artikel.Add("Netto");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("ArtIDRef"); //Produkt.Nr.
            Artikel.Add("Halle");
            Artikel.Add("Reihe");    // Reihe
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Lieferschein");  // LS.Nr.

            AddToView("LAusgang", "LVS Nr.", Artikel, false);

            // ProdNummer
            Artikel = new List<string>();
            Artikel.Add("Selected");
            Artikel.Add("Status");            // Status?
            Artikel.Add("Freigabe");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("ArtIDRef"); //Produkt.Nr.
            Artikel.Add("Auftraggeber");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Brutto");
            Artikel.Add("Netto");
            Artikel.Add("Halle");
            Artikel.Add("Reihe");    // Reihe
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Ausgang");             //E-ScheinNr.
            //Artikel.Add("Lfs-Datum"); // ??
            Artikel.Add("Lieferschein");

            AddToView("LAusgang", "Prod.Nr.", Artikel, false);

            // Materialnummer
            Artikel = new List<string>();
            Artikel.Add("Selected");
            Artikel.Add("Status");            // Status?
            Artikel.Add("exMaterialnummer");  //Mat.Nr.
            Artikel.Add("MaterialNr");
            Artikel.Add("Freigabe");
            Artikel.Add("LZZ");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Brutto");
            Artikel.Add("Netto");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("ArtIDRef"); //Produkt.Nr.
            Artikel.Add("LVSNr");
            Artikel.Add("Auftraggeber");      //KD.Nr.(Empf./Abs.)
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Halle");
            Artikel.Add("Reihe");    // Reihe

            AddToView("LAusgang", "Materialnr.", Artikel, false);


            //*********************** Einlagerung ****************//

            // ABMESSUNG
            Artikel = new List<string>();
            Artikel.Add("Status");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Länge");
            Artikel.Add("Brutto");
            Artikel.Add("Netto");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("ArtIDRef"); //Produkt.Nr.
            Artikel.Add("LVSNr");

            AddToView("LEingang", "Generell", Artikel, false);

            // Gewicht
            Artikel = new List<string>();
            Artikel.Add("Status");
            Artikel.Add("Brutto");
            Artikel.Add("Netto");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Länge");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("ArtIDRef"); //Produkt.Nr.
            Artikel.Add("LVSNr");

            AddToView("LEingang", "Gewicht", Artikel, false);

            // LVS_NR
            Artikel = new List<string>();
            Artikel.Add("Status");
            Artikel.Add("LVSNr");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Länge");
            Artikel.Add("Brutto");
            Artikel.Add("Netto");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("ArtIDRef"); //Produkt.Nr.

            AddToView("LEingang", "LVS Nr.", Artikel, false);

            // ProdNummer
            Artikel = new List<string>();
            Artikel.Add("Status");
            Artikel.Add("Freigabe");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("ArtIDRef"); //Produkt.Nr.
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Länge");
            Artikel.Add("Brutto");
            Artikel.Add("Netto");

            AddToView("LEingang", "Prod.Nr.", Artikel, false);

            // Materialnummer
            Artikel = new List<string>();
            Artikel.Add("Status");
            Artikel.Add("exMaterialnummer");  //Mat.Nr.
            Artikel.Add("Freigabe");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Länge");
            Artikel.Add("Brutto");
            Artikel.Add("Netto");
            Artikel.Add("LZZ");              //LZZ Ang.   // fehlt noch in Abfrage
            Artikel.Add("Produktionsnummer");
            Artikel.Add("ArtIDRef"); //Produkt.Nr.
            Artikel.Add("LVSNr");

            AddToView("LEingang", "Materialnr.", Artikel, false);

            Artikel = new List<string>();
            Artikel.Add("LVSNr");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Abmessung"); //Produkt.Nr.
            Artikel.Add("Netto");
            Artikel.Add("Brutto");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("exMaterialnummer");
            Artikel.Add("Lieferschein");
            Artikel.Add("Anmerkung");

            AddToView("Kunden", "Bestand", Artikel, true);

            Artikel = new List<string>();
            Artikel.Add("LVSNr");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Brutto");
            Artikel.Add("Halle");
            Artikel.Add("Reihe");
            Artikel.Add("Bemerkung");

            AddToView("Kunden", "Inventur", Artikel, true);

            Artikel = new List<string>();
            Artikel.Add("LVSNr");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Abmessung");
            Artikel.Add("Netto");
            Artikel.Add("Brutto");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("exMaterialnummer");
            Artikel.Add("LZZ");
            Artikel.Add("Lieferschein");
            AddToView("Kunden", "Freigabe", Artikel, true);

            Artikel = new List<string>();
            //Artikel.Add("LVSNr");
            Artikel.Add("Prod.-Nr.");
            Artikel.Add("Abmessung"); //Produkt.Nr.
            Artikel.Add("Netto");
            Artikel.Add("Brutto");
            Artikel.Add("Trans.-Ref.");
            Artikel.Add("Bemerkung");
            Artikel.Add("LZZ");
            Artikel.Add("MaterialNr");
            AddToView("Freigabe für Abruf", "Freigabe für Abruf", Artikel, true);

            Artikel = new List<string>();
            Artikel.Add("Prod.-Nr.");
            Artikel.Add("Abmessung");
            Artikel.Add("Netto");
            Artikel.Add("Brutto");
            Artikel.Add("Halle");
            Artikel.Add("Reihe");
            Artikel.Add("Bemerkung");
            AddToView("Arbeitsliste", "Arbeitsliste", Artikel, true);

        }

        /**************************************************************************************************************
         * 
         *                          Procedure / Methoden intern
         *                
         **************************************************************************************************************/
        ///<summary>clsClient / SetViewsDefault</summary>
        ///<remarks></remarks>
        private void SetViewsDefault()
        {
            List<string> Artikel;
            /*************************************  BESTAND *******************************/

            Artikel = new List<string>();
            AddToView("Bestand", "Default", Artikel, false);
            AddToView("Journal", "Default", Artikel, false);


            Artikel = new List<string>();
            Artikel.Add("LVSNr");
            Artikel.Add("Eingang");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Auftraggeber");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Brutto");
            Artikel.Add("Laenge");
            Artikel.Add("TransportId");
            //BestandPrintViews.Add("Default", tmp1);
            //DictPrintViews.Add("Bestand", BestandPrintViews);
            AddToView("Bestand", "Default", Artikel, true);
            AddToView("Journal", "Default", Artikel, true);
            //AddToView("Freigabe für Abruf", "Default",Artikel,true);
            //AddToView("Arbeitsliste", "Default", Artikel, true);

            if (this.Modul.Lager_Bestandsliste_BestandOverAllWorkspaces)
            {
                /*************************************  BESTAND *******************************/
                Artikel = new List<string>();
                Artikel.Add("Arbeitsbereich");
                Artikel.Add("SPL");
                Artikel.Add("LVSNr");
                Artikel.Add("Auftraggeber");
                Artikel.Add("Werksnummer");
                Artikel.Add("Gut");
                //Artikel.Add("GT - Art");
                Artikel.Add("Produktionsnummer");
                Artikel.Add("Dicke");
                Artikel.Add("Breite");
                //Artikel.Add("Laenge");
                Artikel.Add("Anzahl");
                Artikel.Add("Netto");
                Artikel.Add("Brutto");
                Artikel.Add("Halle");
                Artikel.Add("Reihe");
                Artikel.Add("Ebene");
                Artikel.Add("Platz");
                Artikel.Add("Eingangsdatum");
                //Artikel.Add("Ausgangsdatum");
                Artikel.Add("Lagerdauer");
                Artikel.Add("Charge");
                Artikel.Add("Schaden");
                Artikel.Add("Lieferschein");
                Artikel.Add("Bemerkung");
                Artikel.Add("intInfo");
                Artikel.Add("Glühdatum");
                //Artikel.Add("Brutto");
                //Artikel.Add("Bestellnummer");
                //Artikel.Add("exMaterialnummer");
                //Artikel.Add("exBezeichnung");
                //Artikel.Add("ArtikelID");
                AddToView(clsClient.const_ViewKategorie_Bestand, clsClient.cont_ViewName_Bestand_Customized_AllWorkspaces, Artikel, false);
                AddToView(clsClient.const_ViewKategorie_Bestand, clsClient.cont_ViewName_Bestand_Customized_AllWorkspaces, Artikel, true);
            }

            /*************************************  Lager Eingang   *******************************/
            Artikel = new List<string>();
            Artikel.Add("ArtikelID");
            Artikel.Add("LVSNr");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Werksnummer");
            Artikel.Add("Netto");
            Artikel.Add("Brutto");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Länge");
            Artikel.Add("Höhe");
            Artikel.Add("Güterart");
            Artikel.Add("Zusatz");
            Artikel.Add("Charge");
            Artikel.Add("Bestellnummer");
            Artikel.Add("exMaterialnummer");
            Artikel.Add("exBezeichnung");
            Artikel.Add("Position");
            Artikel.Add("ArtIDRef");
            Artikel.Add("Anzahl");
            Artikel.Add("Einheit");
            Artikel.Add("Packmittel");

            AddToView("LEingang", "Default", Artikel, false);
            AddToView("LEingang", "Default", Artikel, true);

            /*************************************  Lager Ausgang   *******************************/

            Artikel = new List<string>();
            Artikel.Add("Check");
            Artikel.Add("LVSNr");
            Artikel.Add("Charge");
            Artikel.Add("Laenge");
            Artikel.Add("Werksnummer");
            Artikel.Add("Gut");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Brutto");
            Artikel.Add("Netto");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Eingang");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Freigabe");
            AddToView("LAusgang", "Default", Artikel, false);
            AddToView("LAusgang", "Default", Artikel, true);

            /*************************************  Lager Ausgang dtAArtikel   *******************************/

            Artikel = new List<string>();
            Artikel.Add("ENTL");
            Artikel.Add("Check");
            Artikel.Add("LVSNr");
            Artikel.Add("Charge");
            Artikel.Add("Laenge");
            Artikel.Add("Werksnummer");
            Artikel.Add("Gut");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Brutto");
            Artikel.Add("Netto");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Eingang");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Freigabe");
            AddToView("LAusgangA", "Default", Artikel, false);


            /*************************************  Inventur   *******************************/

            Artikel = new List<string>();
            Artikel.Add("Status");
            Artikel.Add("Id");
            Artikel.Add("Name");
            Artikel.Add("Description");
            Artikel.Add("Text");
            Artikel.Add("Art");
            Artikel.Add("UserId");
            Artikel.Add("ArbeitsbereichID");
            Artikel.Add("Created");
            Artikel.Add("CloseDate");
            Artikel.Add("CloseUserId");
            AddToView("Inventur", "Default", Artikel, false);

            Artikel = new List<string>();
            Artikel.Add("Status");
            Artikel.Add("Id");
            Artikel.Add("InventoryId");
            Artikel.Add("Description");
            Artikel.Add("Text");
            Artikel.Add("LVSNr");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Werksnummer");
            Artikel.Add("Werk");
            Artikel.Add("Halle");
            Artikel.Add("Reihe");
            Artikel.Add("Ebene");
            Artikel.Add("Platz");
            Artikel.Add("ArtikelId");

            AddToView("InventurArtikel", "Default", Artikel, false);


            Artikel = new List<string>();
            Artikel.Add("ArtikelID");
            Artikel.Add("LVSNr");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Werksnummer");
            Artikel.Add("Werk");
            Artikel.Add("Halle");
            Artikel.Add("Reihe");
            Artikel.Add("Ebene");
            Artikel.Add("Platz");

            AddToView("InventurAdd", "Default", Artikel, false);

        }
        ///<summary>clsClient / AddToView</summary>
        ///<remarks></remarks>
        public void AddToView(string katName, string viewname, List<string> Artikel, bool bPrint, string strOrder = "")
        {
            if (bPrint == false)
            {
                //Dictionary<string, List<string>> Views = DictViews.GetValueOrNull(katName);

                Dictionary<string, List<string>> Views;
                DictViews.TryGetValue(katName, out Views);
                if (Views != null)
                    Views.Add(viewname, Artikel);
                else
                {
                    Views = new Dictionary<string, List<string>>();
                    Views.Add(viewname, Artikel);
                    DictViews.Add(katName, Views);
                    DictOrders.Add(katName, strOrder);
                }
            }
            else
            {
                //Dictionary<string, List<string>> Views = DictPrintViews.GetValueOrNull(katName);

                Dictionary<string, List<string>> Views;
                DictPrintViews.TryGetValue(katName, out Views);
                if (Views != null)
                    Views.Add(viewname, Artikel);
                else
                {
                    Views = new Dictionary<string, List<string>>();
                    Views.Add(viewname, Artikel);
                    DictPrintViews.Add(katName, Views);
                }
            }
        }


        /***********************************************************************************************
         *                           public static client functions
         * *********************************************************************************************/
        /// <summary>
        ///             Steuerung der manipulation von Artikel.Bestellnummer
        /// </summary>
        /// <param name="myArt"></param>
        public void clsLagerdaten_Customized_ASNArtikel_Bestellnummer(ref clsArtikel myArt, string myReplaceVal)
        {
            switch (this.MatchCode)
            {
                case (const_ClientMatchcode_SZG + "_"):
                    myArt.Bestellnummer = myReplaceVal;
                    break;
                case (const_ClientMatchcode_SIL + "_"):
                    myArt.Bestellnummer = myReplaceVal;
                    break;
                default:
                    if (myArt.Bestellnummer.Equals(string.Empty))
                    {
                        myArt.Bestellnummer = myReplaceVal;
                    }
                    break;
            }
        }
        public void clsLagerdaten_Customized_ASNArtikel_Bestellnummer(ref Articles myArt, string myReplaceVal)
        {
            switch (this.MatchCode)
            {
                case (const_ClientMatchcode_SZG + "_"):
                    myArt.Bestellnummer = myReplaceVal;
                    break;
                case (const_ClientMatchcode_SIL + "_"):
                    myArt.Bestellnummer = myReplaceVal;
                    break;
                default:
                    if (myArt.Bestellnummer.Equals(string.Empty))
                    {
                        myArt.Bestellnummer = myReplaceVal;
                    }
                    break;
            }
        }
        //*************************************************************************************************************  ctrASNCall
        ///<summary>clsClient / ctrASNCall_CustomizeTsbtnDeleteASN</summary>
        ///<remarks></remarks>
        public void ctrASNCall_CustomizeTsbtnDeleteASN(ref System.Windows.Forms.ToolStripButton myTB)
        {
            switch (this.MatchCode)
            {
                case (const_ClientMatchcode_SZG + "_"):
                    this.setSZG.ctrASNCall_CustomizeTsbtnDeleteASN(ref myTB);
                    break;
                default:
                    break;
            }
        }
        //*************************************************************************************************************  ctrAuslagerung
        ///<summary>clsClient / ctrAuslagerung_CustomizeDefaulAusgangsdaten</summary>
        ///<remarks> </remarks>
        public static void ctrAuslagerung_CustomizeDefaulAusgangsdaten(ref clsSystem mySystem, ref clsLager myLager)
        {
            decimal decTmp = 0;
            //Empfänger
            myLager.Ausgang.Empfaenger = decTmp; //AdrId VW
            if (mySystem.Client.DictArbeitsbereich_Ausgang_DefaultEmpfaengerAdrID.Count > 0)
            {
                decTmp = 0;
                mySystem.Client.DictArbeitsbereich_Ausgang_DefaultEmpfaengerAdrID.TryGetValue(mySystem.AbBereich.ID, out decTmp);
            }
            myLager.Ausgang.Empfaenger = decTmp;
            decTmp = 0;

            //Entladestelle
            decTmp = 0;
            if (mySystem.Client.DictArbeitsbereich_Ausgang_DefaultEntladeAdrID.Count > 0)
            {
                decTmp = 0;
                mySystem.Client.DictArbeitsbereich_Ausgang_DefaultEntladeAdrID.TryGetValue(mySystem.AbBereich.ID, out decTmp);
            }
            myLager.Ausgang.Entladestelle = decTmp;

            //Beladeadresse
            decTmp = 0;
            if (mySystem.Client.DictArbeitsbereich_Ausgang_DefaultBeladeAdrID.Count > 0)
            {
                decTmp = 0;
                mySystem.Client.DictArbeitsbereich_Ausgang_DefaultBeladeAdrID.TryGetValue(mySystem.AbBereich.ID, out decTmp);
            }
            myLager.Ausgang.BeladeID = decTmp;

            //Versender
            decTmp = 0;
            if (mySystem.Client.DictArbeitsbereich_Ausgang_DefaultVersenderAdrID.Count > 0)
            {
                decTmp = 0;
                mySystem.Client.DictArbeitsbereich_Ausgang_DefaultVersenderAdrID.TryGetValue(mySystem.AbBereich.ID, out decTmp);
            }
            myLager.Ausgang.Versender = decTmp;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mySystem"></param>
        /// <param name="myLager"></param>
        public void ctrAuslagerung_CustomizeTrailerDataInput(ref System.Windows.Forms.ComboBox myCombo,
                                                             ref System.Windows.Forms.MaskedTextBox myTextbox,
                                                             ref System.Windows.Forms.Label myLCombo,
                                                             ref System.Windows.Forms.Label myLTbBox
                                                             )
        {
            switch (this.MatchCode)
            {
                //SIL
                case (const_ClientMatchcode_SLE + "_"):
                    this.setSLE.ctrAuslagerung_CustomizeTrailerDataInput(ref myCombo, ref myTextbox, ref myLCombo, ref myLTbBox);
                    break;
                default:
                    break;
            }
        }
        ///<summary>clsClient / ctrEinlagerung_CustomizeEingangAdrDatenInputFieldsEnabled</summary>
        ///<remarks></remarks>
        public static void ctrAuslagerung_CustomizeAusgangAdrDatenInputFieldsEnabled(string myClientMC, ref System.Windows.Forms.Button btnAdrSearch, ref System.Windows.Forms.Button btnManAdr, ref System.Windows.Forms.TextBox tbMCSearchTxt)
        {
            switch (myClientMC)
            {
                //SIL
                case (const_ClientMatchcode_SIL + "_"):
                    btnManAdr.Enabled = false;
                    btnAdrSearch.Enabled = false;
                    tbMCSearchTxt.Enabled = false;
                    break;
                default:
                    break;
            }
        }
        //*************************************************************************************************************  ctrBSInfo4905
        ///<summary>clsClient / ctrBSInfo4905_CustomizeCheckBoxRueckstand</summary>
        ///<remarks></remarks>
        public void ctrBSInfo4905_CustomizeCheckBoxRueckstand(ref System.Windows.Forms.CheckBox myCB)
        {
            switch (this.MatchCode)
            {
                case (const_ClientMatchcode_SZG + "_"):
                    this.setSZG.ctrBSInfo4905_CustomizeCheckBoxRueckstand(ref myCB);
                    break;
                default:
                    break;
            }
        }
        ///<summary>clsClient / ctrBSInfo4905_CustomizeCheckBoxChecked</summary>
        ///<remarks></remarks>
        public void ctrBSInfo4905_CustomizeCheckBoxChecked(ref System.Windows.Forms.CheckBox myCB)
        {
            switch (this.MatchCode)
            {
                case (const_ClientMatchcode_SZG + "_"):
                    this.setSZG.ctrBSInfo4905_CustomizeCheckBoxRueckstand(ref myCB);
                    break;
                default:
                    break;
            }
        }
        //*************************************************************************************************************  ctrBestand
        ///<summary>clsClient / ctrBSInfo4905_CustomizeCheckBoxRueckstand</summary>
        ///<remarks></remarks>
        public void ctrBestand_CustomizeComboBestandArt(ref System.Windows.Forms.ComboBox myCB)
        {
            switch (this.MatchCode)
            {
                case (const_ClientMatchcode_Althaus + "_"):
                    this.setALTHAUS.ctrBestand_CustomizeComboBestandArt(ref myCB);
                    break;
                default:
                    break;
            }
        }
        ///<summary>clsClient / ctrBSInfo4905_CustomizeCheckBoxRueckstand</summary>
        ///<remarks></remarks>
        public bool ctrBestand_CustomizeGroupListCheckedUncheckedStoreINAndOUT()
        {
            bool bReturn = false;
            switch (this.MatchCode)
            {
                case (const_ClientMatchcode_SIL + "_"):
                    bReturn = this.setSIL.ctrBestand_CustomizeGroupListCheckedUncheckedStoreINAndOUT();
                    break;
                default:
                    bReturn = true;
                    break;
            }
            return bReturn;
        }
        //******************************************************************************************************************* ctrEinlagerung    
        /// <summary>
        ///             bei Suche nach einem umgebuchten Aritkel mit der alten LVSNr soll direkt in den neuen Artikeldatensatz 
        ///             gesprungen werden
        /// </summary>
        public bool ctrEinlagerung_tstbJumpArtID_DirectJumpToUBArtikel()
        {
            bool retVal = false;
            switch (this.MatchCode)
            {
                //SIL
                case (const_ClientMatchcode_SIL + "_"):
                    break;
                case (const_ClientMatchcode_SZG + "_"):
                    retVal = true;
                    break;
                default:
                    break;
            }
            return retVal;
        }
        ///<summary>clsClient / SetViewsDefault</summary>
        ///<remarks></remarks>
        public static void ctrEinlagerung_tbProdNr_ProdNrToCharge(string myClientMC, ref System.Windows.Forms.TextBox tbProdNr, ref System.Windows.Forms.TextBox tbCharge)
        {
            switch (myClientMC)
            {
                case (const_ClientMatchcode_SIL + "_"):
                    if (tbCharge.Text.Equals(string.Empty))
                    {
                        tbCharge.Text = tbProdNr.Text;
                    }
                    break;
            }
        }
        ///<summary>clsClient / ctrEinlagerung_tbInfoAusgang_SetText</summary>
        ///<remarks>Die übergebene Textbox enthält bereits den Standardtext. Hier kann nun entschieden werden ober der Text noch bearbeitet oder
        ///         so ausgegeben wird</remarks>
        public static void ctrEinlagerung_tbInfoAusgang_SetText(string myClientMC, ref clsLager myLager, ref System.Windows.Forms.TextBox myTBInfoAusgang)
        {
            switch (myClientMC)
            {
                case (const_ClientMatchcode_SIL + "_"):
                    set_SIL tmpSIL = new set_SIL();
                    tmpSIL.ctrEinlagerung_tbInfoAusgang_SetText(ref myLager, ref myTBInfoAusgang);
                    break;

                default:
                    break;
            }
        }
        ///<summary>clsClient / ctrEinlagerung_tbInfoAusgang_SetText</summary>
        ///<remarks>Die übergebene Textbox enthält bereits den Standardtext. Hier kann nun entschieden werden ober der Text noch bearbeitet oder
        ///         so ausgegeben wird</remarks>
        public static void ctrEinlagerung_tbInfoAusgang_SetBackColor(string myClientMC, ref clsLager myLager, ref System.Windows.Forms.TextBox myTBInfoAusgang)
        {
            switch (myClientMC)
            {
                //case (const_ClientMatchcode_SIL + "_"):
                //    set_SIL tmpSIL = new set_SIL();
                //    tmpSIL.ctrEinlagerung_tbInfoAusgang_SetText(ref myLager, ref myTBInfoAusgang);
                //    break;
                default:
                    //Schriftfarbe ändern wenn der Ausgang noch nicht abgeschlossen ist
                    if (myLager.Artikel.LAusgangTableID > 0)
                    {
                        if (myLager.Artikel.Ausgang.Checked)
                        {
                            myTBInfoAusgang.BackColor = System.Windows.Forms.TextBox.DefaultBackColor;
                        }
                        else
                        {
                            myTBInfoAusgang.BackColor = System.Drawing.Color.Tomato;
                        }
                    }
                    else
                    {
                        myTBInfoAusgang.BackColor = System.Windows.Forms.TextBox.DefaultBackColor;
                    }
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myClientMC"></param>
        /// <param name="myLager"></param>
        /// <param name="myTBInfoAusgang"></param>

        public void ctrEinlagerung_tbUBInfo_SetBackColor(ref clsLager myLager, ref System.Windows.Forms.TextBox myTBInfo)
        {
            myTBInfo.BackColor = System.Windows.Forms.TextBox.DefaultBackColor;
            switch (this.MatchCode)
            {
                //case (const_ClientMatchcode_ + "_"):
                //    set_SIL tmpSIL = new set_SIL();
                //    tmpSIL.ctrEinlagerung_tbInfoAusgang_SetText(ref myLager, ref myTBInfoAusgang);
                //    break;
                default:
                    if (myLager.Artikel.LVSNrBeforeUB > 0)
                    {
                        myTBInfo.BackColor = System.Drawing.Color.Yellow;
                    }
                    if (myLager.Artikel.LVSNrAfterUB > 0)
                    {
                        if (myLager.Artikel.Umbuchung)
                        {
                            myTBInfo.BackColor = System.Drawing.Color.GreenYellow;
                        }
                        else
                        {
                            myTBInfo.BackColor = System.Drawing.Color.LimeGreen;
                        }
                    }
                    if (myLager.Artikel.Eingang.Retoure)
                    {
                        myTBInfo.BackColor = System.Drawing.Color.LimeGreen;
                    }
                    break;
            }
        }
        ///<summary>clsClient / ctrEinlagerung_lLaenge_SetText</summary>
        ///<remarks></remarks>
        public static void ctrEinlagerung_TextBox_SetEnabledBackcolor(string myClientMC, ref System.Windows.Forms.TextBox myTextBox)
        {
            switch (myClientMC)
            {
                case (const_ClientMatchcode_SIL + "_"):
                    if (myTextBox.Enabled)
                    {
                        myTextBox.BackColor = System.Drawing.SystemColors.Window;
                    }
                    else
                    {
                        myTextBox.BackColor = System.Windows.Forms.TextBox.DefaultBackColor;
                    }
                    break;
                default:

                    break;
            }
        }
        ///<summary>clsClient / ctrEinlagerung_CustomizeDefaulEingangsdaten</summary>
        ///<remarks>Empfänger und Entladestelle werden voreingestellt</remarks>
        //public static void ctrEinlagerung_CustomizeDefaulEingangsdaten(string myClientMC, ref  clsLager myLager)
        public static void ctrEinlagerung_CustomizeDefaulEingangsdaten(ref clsSystem mySytem, ref clsLager myLager)
        {
            decimal decTmp = 0;
            switch (mySytem.Client.MatchCode)
            {
                //Althaus 
                case (const_ClientMatchcode_Althaus + "_"):
                    mySytem.Client.setALTHAUS.ctrEinlagerung_CustomizeDefault_Adressdaten(ref myLager);
                    break;

                default:
                    // mr 2020_09_29 raus
                    //myLager.Eingang.Empfaenger = 0; //AdrId VW
                    //myLager.Eingang.EntladeID = 0;
                    //if (mySytem.Client.DictArbeitsbereich_Eingang_DefaultEmpfaengerAdrID.Count > 0)
                    //{
                    //    decTmp = 0;
                    //    mySytem.Client.DictArbeitsbereich_Eingang_DefaultEmpfaengerAdrID.TryGetValue(mySytem.AbBereich.ID, out decTmp);
                    //}
                    //myLager.Eingang.Empfaenger = decTmp;
                    //if (mySytem.Client.DictArbeitsbereich_Eingang_DefaultEntladeAdrID.Count > 0)
                    //{
                    //    decTmp = 0;
                    //    mySytem.Client.DictArbeitsbereich_Eingang_DefaultEntladeAdrID.TryGetValue(mySytem.AbBereich.ID, out decTmp);
                    //}
                    //myLager.Eingang.EntladeID = decTmp;

                    // mr 2020_09_29 geändert
                    //Empfänger
                    if (myLager.Eingang.Empfaenger == 0)
                    {
                        if (mySytem.Client.DictArbeitsbereich_Eingang_DefaultEmpfaengerAdrID.Count > 0)
                        {
                            decTmp = 0;
                            mySytem.Client.DictArbeitsbereich_Eingang_DefaultEmpfaengerAdrID.TryGetValue(mySytem.AbBereich.ID, out decTmp);
                        }
                        myLager.Eingang.Empfaenger = decTmp;
                    }
                    //Entlade
                    if (myLager.Eingang.EntladeID == 0)
                    {
                        if (mySytem.Client.DictArbeitsbereich_Eingang_DefaultEntladeAdrID.Count > 0)
                        {
                            decTmp = 0;
                            mySytem.Client.DictArbeitsbereich_Eingang_DefaultEntladeAdrID.TryGetValue(mySytem.AbBereich.ID, out decTmp);
                        }
                        myLager.Eingang.EntladeID = decTmp;
                    }
                    break;
            }
        }
        ///<summary>clsClient / ctrEinlagerung_CustomizeEingangDefaultAbrufReceiver</summary>
        ///<remarks></remarks>
        public static void ctrEinlagerung_CustomizeEingangDefaultAbrufReceiver(ref clsSystem mySystem, ref clsLager myLager)
        {
            myLager.Artikel.Call.EmpAdrID = 0; //AdrId VW
            decimal decTmp = 0;
            //Empfänger
            if (mySystem.Client.DictArbeitsbereich_Abrufe_DefaulEmpfaengerAdrID.Count > 0)
            {
                decTmp = 0;
                mySystem.Client.DictArbeitsbereich_Abrufe_DefaulEmpfaengerAdrID.TryGetValue(mySystem.AbBereich.ID, out decTmp);
            }
            myLager.Artikel.Call.EmpAdrID = (Int32)decTmp;
        }
        ///<summary>clsClient / ctrEinlagerung_CustomizeDefaultCallCompany</summary>
        ///<remarks></remarks>
        public static void ctrEinlagerung_CustomizeDefaultCallCompany(ref clsSystem mySystem, ref clsLager myLager)
        {
            if (myLager.Artikel.Call.Company == null)
            {
                myLager.Artikel.Call.Company = new clsCompany();
                myLager.Artikel.Call.Company._GL_User = mySystem._GL_User;
            }
            myLager.Artikel.Call.Company.ID = 0;
            decimal decTmp = 0;
            //Empfänger
            if (mySystem.Client.DictArbeitsbereich_Abrufe_DefaultCompanyAdrID.Count > 0)
            {
                decTmp = 0;
                mySystem.Client.DictArbeitsbereich_Abrufe_DefaultCompanyAdrID.TryGetValue(mySystem.AbBereich.ID, out decTmp);
            }
            myLager.Artikel.Call.Company.ID = (Int32)decTmp;
            myLager.Artikel.Call.Company.Fill();
        }
        ///<summary>clsClient / ctrEinlagerung_CustomizeEingangAdrDatenInputFieldsEnabled</summary>
        ///<remarks></remarks>
        public static void ctrEinlagerung_CustomizeEingangAdrDatenInputFieldsEnabled(string myClientMC, ref System.Windows.Forms.Button btnAdrSearch, ref System.Windows.Forms.Button btnManAdr, System.Windows.Forms.TextBox tbMCSearchTxt)
        {
            switch (myClientMC)
            {
                //SIL
                case (const_ClientMatchcode_SIL + "_"):
                    btnManAdr.Enabled = false;
                    btnAdrSearch.Enabled = false;
                    tbMCSearchTxt.Enabled = false;
                    break;
                default:
                    break;
            }
        }
        ///<summary>clsClient / ctrEinlagerung_CustomizeSetGArtMCEnabled</summary>
        ///<remarks>MC für Güterart ausblenden</remarks>
        public static void ctrEinlagerung_CustomizeSetGArtMCEnabled(string myClientMC, System.Windows.Forms.TextBox tbMCSearchTxt)
        {
            switch (myClientMC)
            {
                //SIL
                case (const_ClientMatchcode_SIL + "_"):
                    tbMCSearchTxt.Enabled = false;
                    break;
                default:
                    break;
            }
        }
        ///<summary>clsClient / ctrEinlagerung_CustomizeEingangAdrDatenInputFieldsEnabled</summary>
        ///<remarks></remarks>
        //public void ctrEinlagerung_CustomizeSetInputFieldsReadOnly(ref System.Windows.Forms.TextBox myTB)
        //{
        //    //if (myTB != null)
        //    //{
        //    //    switch (this.MatchCode)
        //    //    {
        //    //        //SIL
        //    //        case (const_ClientMatchcode_SIL + "_"):
        //    //            sil_ctrEinlagerung_SetInputFieldReadOnly.Execute(ref myTB);
        //    //            break;
        //    //        case (const_ClientMatchcode_SZG + "_"):
        //    //            //myTB.ReadOnly = true;
        //    //            //myTB.BackColor = System.Windows.Forms.TextBox.DefaultBackColor;

        //    //            //myTB.ReadOnly = false;
        //    //            //myTB.BackColor = System.Drawing.SystemColors.ControlLightLight;

        //    //            szg_ctrEinlagerung_SetInputFieldReadOnly.Execute(ref myTB);
        //    //            //this.setSZG.ctrEinlagerung_SetInputFieldReadOnly(ref myTB);
        //    //            break;
        //    //        case (const_ClientMatchcode_SLE + "_"):
        //    //            //sil_ctrEinlagerung_SetInputFieldReadOnly.Execute(ref myTB);
        //    //            break;

        //    //        default:
        //    //            break;
        //    //    }
        //    //}
        //}
        public bool ctrEinlagerung_CustomizeSetInputFieldsReadOnly(string myTbName)
        {
            bool bReturn = false;
            if (!myTbName.Equals(string.Empty))
            {
                switch (this.MatchCode)
                {
                    //SIL
                    case (const_ClientMatchcode_SIL + "_"):
                        sil_ctrEinlagerung_SetInputFieldReadOnly.Execute(myTbName);
                        break;
                    case (const_ClientMatchcode_SZG + "_"):
                        bReturn = szg_ctrEinlagerung_SetInputFieldReadOnly.Execute(myTbName);
                        //this.setSZG.ctrEinlagerung_SetInputFieldReadOnly(ref myTB);
                        break;
                    case (const_ClientMatchcode_SLE + "_"):
                        sil_ctrEinlagerung_SetInputFieldReadOnly.Execute(myTbName);
                        break;

                    default:
                        break;
                }
            }
            return bReturn;
        }


        ///<summary>clsClient / ctrEinlagerung_CustomizeEingangAdrDatenInputFieldsEnabled</summary>
        ///<remarks>Textbox InfoIntern </remarks>
        public void ctrEinlagerung_CustomizedSettbInfoInternEnabled(ref System.Windows.Forms.TextBox myTB)
        {
            switch (this.MatchCode)
            {
                case (const_ClientMatchcode_SZG + "_"):
                    this.setSZG.ctrEinlagerung_CustomizedSettbInfoInternEnabled(ref myTB);
                    break;
                default:
                    break;
            }
        }
        ///<summary>clsClient / ctrEinlagerung_lLaenge_SetText</summary>
        ///<remarks></remarks>
        public void ctrEinlagerung_CustomizedSetLabel_lLaenge_Text(ref System.Windows.Forms.Label lLae)
        {
            switch (this.MatchCode)
            {
                case (const_ClientMatchcode_SLE + "_"):
                    if (this.setSLE != null)
                    {
                        this.setSLE.ctrEinlagerung_CustomizedSetLabel_lLaenge_Text(ref lLae);
                        //strText = "Länge [m]:";
                    }
                    break;
                default:
                    lLae.Text = "Länge [mm]:";
                    break;
            }
        }
        ///<summary>clsClient / ctrEinlagerung_CustomizedSetLabelWerksnummerText</summary>
        ///<remarks>Label Werksnummer ändern</remarks>
        public void ctrEinlagerung_CustomizedSetLabel_lWerksnummer_Text(ref System.Windows.Forms.Label myWNrLabel)
        {
            switch (this.MatchCode)
            {
                //SIL
                case (const_ClientMatchcode_Althaus + "_"):
                    this.setALTHAUS.ctrEinlagerung_CustomizedSetLabelWerksnummerText(ref myWNrLabel);
                    break;

                default:
                    break;
            }
        }
        ///<summary>clsClient / ctrEinlagerung_CustomizedSetLabelWerksnummerText</summary>
        ///<remarks>Label Werksnummer ändern</remarks>
        public void ctrEinlagerung_CustomizedSetLabel_tsbtnChangeEinlagerung(ref System.Windows.Forms.ToolStripButton myTsbtn)
        {
            switch (this.MatchCode)
            {
                //SLE
                case (const_ClientMatchcode_SLE + "_"):
                    this.setSLE.ctrEinlagerung_CustomizedSetLabel_tsbtnChangeEinlagerung(ref myTsbtn);
                    break;
                case (const_ClientMatchcode_Comtec + "_"):
                    this.setCOMTC.ctrEinlagerung_CustomizedSetLabel_tsbtnChangeEinlagerung(ref myTsbtn);
                    break;
                //case (const_ClientMatchcode_Valerius + "_"):
                //    this.setVALERIUS.ctrEinlagerung_CustomizedSetLabel_tsbtnChangeEinlagerung(ref myTsbtn);
                //    break;

                default:
                    myTsbtn.Visible = this.Modul.Lager_Einlagerung_EditAfterClose && !(this.Modul.ASNTransfer);
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myTsbtn"></param>
        public void ctrEinlagerung_CustomizedComboFahrzeugeSetStartValue_ComboFahrzeuge(ref System.Windows.Forms.ComboBox myCombo)
        {
            switch (this.MatchCode)
            {
                //case (const_ClientMatchcode_SLE + "_"):

                //    break;
                //case (const_ClientMatchcode_Comtec + "_"):

                //    break;
                //case (const_ClientMatchcode_Valerius + "_"):

                //    break;

                case (const_ClientMatchcode_SZG + "_"):
                    this.setSZG.ctrEinlagerung_CustomizedComboFahrzeugeSetStartValue_ComboFahrzeuge(ref myCombo);
                    break;

                default:
                    if (myCombo.Items.Count > 0)
                    {
                        myCombo.SelectedIndex = 0;
                    }
                    break;
            }
        }
        //*************************************************************************************************************  ctrJournal
        ///<summary>clsClient / ctrJournal_CustomizeExcelExtportHeaderText</summary>
        ///<remarks></remarks>
        public static string ctrJournal_CustomizeExcelExtportHeaderText(string myClientMC, string myHeaderText, int myRowIndex)
        {
            string strReturnText = string.Empty;
            switch (myClientMC)
            {
                //SIL
                case (const_ClientMatchcode_SIL + "_"):
                    //SIL_spFunc silTmp = new SIL_spFunc();
                    //strReturnText = silTmp.GetJournalHeaderText(myHeaderText);
                    strReturnText = sil_ctrJournal_ExcelHeaderText.Execute(myHeaderText, myRowIndex);
                    break;

                default:
                    strReturnText = myHeaderText;
                    break;
            }
            return strReturnText;
        }
        //*************************************************************************************************************  ctrGueterArtListe
        ///<summary>clsClient / ctrGueterArtListe_CustomizeArtikelArten</summary>
        ///<remarks>Auflistung der Artikelarten wird nach Kundenwunsch angepasst</remarks>
        public static void ctrGueterArtListe_CustomizeArtikelArten(string myClientMC, ref List<string> myList)
        {
            switch (myClientMC)
            {
                //SIL
                case (const_ClientMatchcode_SIL + "_"):
                    set_SIL tmpSIL = new set_SIL();
                    tmpSIL.CustomizeCtrGueterArtenList(ref myList);
                    break;
            }
        }

        ///<summary>clsClient / ctrGueterArtListe_DGVGueterArtenView</summary>
        ///<remarks></remarks>
        //public static bool ctrGueterArtListe_DGVGueterArtenView(string myClientMC, ref Telerik.WinControls.UI.RadGridView myDGV)
        //{
        //    bool bReturn = false;
        //    switch (myClientMC)
        //    {
        //        //SZG
        //        case (const_ClientMatchcode_SZG + "_"):
        //            set_SZG tmpSZG = new set_SZG();
        //            //bReturn = tmpSZG.CustomizeDGVGueterArtenView(ref myDGV);
        //            //tmpSZG.CustomizeDGVGueterArtenView(ref myDGV);
        //            GridViewCustomizedView.CustomizeDGVGueterArtenView(ref myDGV);
        //            break;

        //        default:
        //            for (Int32 i = 0; i <= myDGV.Columns.Count - 1; i++)
        //            {
        //                string colName = myDGV.Columns[i].Name.ToString();
        //                //Warengruppen
        //                switch (colName)
        //                {
        //                    case "ViewID":
        //                        myDGV.Columns[i].HeaderText = "Matchcode";
        //                        myDGV.Columns[i].SortOrder = RadSortOrder.Ascending;
        //                        myDGV.Columns.Move(i, 1);
        //                        break;
        //                    case "Bezeichnung":
        //                        myDGV.Columns.Move(i, 2);
        //                        break;
        //                    default:
        //                        myDGV.Columns[i].IsVisible = false;
        //                        break;
        //                }
        //            }
        //            break;
        //    }
        //    return bReturn;
        //}
        //************************************************************************************************************** ctrMailCockpit
        ///<summary>clsClient / ctrGueterArtListe_CustomizeArtikelArten</summary>
        ///<remarks>Auflistung der Artikelarten wird nach Kundenwunsch angepasst</remarks>
        public static void ctrMailCockpi_CustomizeAddMailBCC(string myClientMC, ref clsMail myMail)
        {
            switch (myClientMC)
            {
                //SIL
                case (const_ClientMatchcode_SIL + "_"):
                    myMail.MailBCC = "Sekretariat@sil-steffens.de";
                    break;
            }
        }

        //**************************************************************************************************************** ctrPrintDoc
        ///<summary>clsClient / ctrPrintLager_CustomizeEingangAdrDatenInputFieldsEnabled</summary>
        ///<remarks></remarks>
        public void ctrPrintLager_CustomizeSetPrintDoc(ref System.Windows.Forms.CheckedListBox myLB, clsLager myLager)
        {
            for (Int32 i = 0; i <= myLB.Items.Count - 1; i++)
            {
                string strCtrName = myLB.Items[i].ToString();
                switch (this.MatchCode)
                {
                    //SZG
                    case (const_ClientMatchcode_SZG + "_"):
                        myLB.SetItemChecked(i, true);
                        if ((!myLager.Eingang.Retoure) && (myLager.Eingang.IsUB))
                        {
                            if (this.setSZG.ctrPrintLager_SetPrintDocs(strCtrName))
                            {
                                myLB.Items.RemoveAt(i);
                                i = -1;
                            }
                        }
                        break;
                    case (const_ClientMatchcode_SLE + "_"):
                        myLB.SetItemChecked(i, true);
                        if ((!myLager.Eingang.Retoure) && (myLager.Eingang.IsUB))
                        {
                            if (this.setSZG.ctrPrintLager_SetPrintDocs(strCtrName))
                            {
                                myLB.Items.RemoveAt(i);
                                i = -1;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        //*************************************************************************************************************  ctrRGManuell
        ///<summary>clsClient / ctrBSInfo4905_CustomizeCheckBoxRueckstand</summary>
        ///<remarks></remarks>
        public void ctrRGManuell_CustomizeNudMengeMaxValue(ref System.Windows.Forms.NumericUpDown myNud)
        {
            switch (this.MatchCode)
            {
                case (const_ClientMatchcode_SLE + "_"):
                    this.setSLE.ctrRGManuell_CustomizeNudMengeMaxValue(ref myNud);
                    break;
                default:
                    myNud.Maximum = 32798;
                    break;
            }
        }

        //**************************************************************************************************************** ctrSearch
        ///<summary>clsClient / ctrSearch_CustomizeComboSearchField</summary>
        ///<remarks>Auflistung der Suchfelder wird nach Kundenwunsch angepasst</remarks>
        public static bool ctrSearch_CustomizeComboSearchField(string myClientMC, string mySearchField)
        {
            bool bReturn = false;
            try
            {
                switch (myClientMC)
                {
                    //SIL
                    case (const_ClientMatchcode_SIL + "_"):
                        switch (mySearchField)
                        {
                            case "LVSNr":
                            case "Produktionsnummer":
                                bReturn = true;
                                break;
                            default:
                                bReturn = false;
                                break;
                        }
                        break;
                    default:
                        bReturn = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                string strError = ex.ToString();
            }
            return bReturn;
        }

        //************************************************************************************************************** ctrSperrlager
        ///<summary>clsClient / ctrGueterArtListe_CustomizeArtikelArten</summary>
        ///<remarks>Auflistung der Artikelarten wird nach Kundenwunsch angepasst</remarks>
        public void ctrSperrlager_CustomizeChangeReebookInOldEingangSetEnabled(ref System.Windows.Forms.CheckBox myCheckBox)
        {
            switch (this.MatchCode)
            {
                //SIL
                case (const_ClientMatchcode_SIL + "_"):
                    myCheckBox.Enabled = false;
                    break;
                default:
                    myCheckBox.Enabled = true;
                    break;
            }
        }
        /// <summary>
        ///             Customized Menü / Button
        /// </summary>
        /// <param name="myTSButton"></param>
        public void ctrSperrlager_CustomizeSETCheckOutButtonVisible(ref System.Windows.Forms.ToolStripButton myTSButton)
        {
            switch (this.MatchCode)
            {
                //Althause
                case (const_ClientMatchcode_Althaus + "_"):
                    myTSButton.Visible = true;
                    break;
                //Heisiep
                case (const_ClientMatchcode_Heisiep + "_"):
                    myTSButton.Visible = true;
                    break;
                //SLE
                case (const_ClientMatchcode_SLE + "_"):
                    myTSButton.Visible = true;  // 10.01.2016 hinzugefügt Rsp. Frau Langheld
                    break;
                //SZG
                case (const_ClientMatchcode_SZG + "_"):
                    myTSButton.Visible = true;
                    break;

                ////SIL Test 22.09.2020
                //case (const_ClientMatchcode_SIL + "_"):
                //    myTSButton.Visible = true;
                //    break;


                default:
                    myTSButton.Visible = false;
                    break;
            }
        }
        //************************************************************************************************************** ctrSchaeden
        ///<summary>clsClient / ctrSchaeden_CustomizeDGVView</summary>
        ///<remarks></remarks>
        public static bool ctrSchaeden_CustomizeDGVView(string myClientMC, string myColName)
        {
            bool bReturn = false;
            switch (myClientMC)
            {
                //SZG
                case (const_ClientMatchcode_SZG + "_"):
                    set_SZG tmpSZG = new set_SZG();
                    bReturn = tmpSZG.CustomizeDGVSchaedenView(myColName);
                    break;

                default:
                    switch (myColName)
                    {
                        case "Bezeichnung":
                        case "Beschreibung":
                        case "Code":
                            bReturn = true;
                            break;
                        //Spalten ausblenden
                        default:
                            bReturn = false;
                            break;
                    }
                    break;
            }
            return bReturn;
        }

        //**************************************************************************************************************** ctrStatistik
        ///<summary>clsClient / ctrStatistik_CustomizeCtrCheckBoxRLExcl</summary>
        ///<remarks></remarks>
        public void ctrStatistik_CustomizeCtrCheckBoxRLExcl(ref System.Windows.Forms.CheckBox myCheckBox)
        {
            switch (this.MatchCode)
            {
                case const_ClientMatchcode_SIL + "_":
                    this.setSIL.ctrStatistik_CustomizeCtrCheckBoxRLExcl(ref myCheckBox);
                    break;

                default:
                    break;
            }
        }
        ///<summary>clsClient / ctrStatistik_CustomizeCtrCheckBoxSchaedenExcl</summary>
        ///<remarks></remarks>
        public void ctrStatistik_CustomizeCtrCheckBoxSchaedenExcl(ref System.Windows.Forms.CheckBox myCheckBox)
        {
            switch (this.MatchCode)
            {
                case const_ClientMatchcode_SIL + "_":
                    this.setSIL.ctrStatistik_CustomizeCtrCheckBoxSchaedenExcl(ref myCheckBox);
                    break;

                default:
                    break;
            }
        }
        ///<summary>clsClient / ctrStatistik_CustomizeCtrCheckBoxSchaedenExcl</summary>
        ///<remarks></remarks>
        public void ctrStatistik_CustomizeCtrCheckBoxSPLExcl(ref System.Windows.Forms.CheckBox myCheckBox)
        {
            switch (this.MatchCode)
            {
                case const_ClientMatchcode_SIL + "_":
                    this.setSIL.ctrStatistik_CustomizeCtrCheckBoxSPLExcl(ref myCheckBox);
                    break;

                default:
                    break;
            }
        }
        //************************************************************************************************************** ctrUmbuchung
        ///<summary>clsClient / ctrEinlagerung_CustomizeDefaulEingangsdaten</summary>
        ///<remarks>Empfänger und Entladestelle werden voreingestellt</remarks>
        public static void ctrUmbuchung_CustomizeDefaulUBDaten(ref clsSystem mySytem, ref clsUmbuchung myUB)
        {
            myUB.AuftraggeberNeuID = 0;
            myUB.EmpfaengerID = 0;
            decimal decTmp = 0;

            //AuftraggeberNEU
            if (mySytem.Client.DictArbeitsbereich_Umbuchung_DefaultAuftraggeberNeuAdrID != null)
            {
                if (mySytem.Client.DictArbeitsbereich_Umbuchung_DefaultAuftraggeberNeuAdrID.Count > 0)
                {
                    decTmp = 0;
                    mySytem.Client.DictArbeitsbereich_Umbuchung_DefaultAuftraggeberNeuAdrID.TryGetValue(mySytem.AbBereich.ID, out decTmp);
                }
            }
            myUB.AuftraggeberNeuID = decTmp;
            //Empfänger
            if (mySytem.Client.DictArbeitsbereich_Umbuchung_DefaultEmpfaengerAdrID != null)
            {
                if (mySytem.Client.DictArbeitsbereich_Umbuchung_DefaultEmpfaengerAdrID.Count > 0)
                {
                    decTmp = 0;
                    mySytem.Client.DictArbeitsbereich_Umbuchung_DefaultEmpfaengerAdrID.TryGetValue(mySytem.AbBereich.ID, out decTmp);
                }
            }
            myUB.EmpfaengerID = decTmp;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myBtn"></param>
        public void ctrUmbuchung_CustomizeBtnAuftraggeberEnabled(ref System.Windows.Forms.Button myBtn)
        {
            switch (this.MatchCode)
            {
                case const_ClientMatchcode_SZG + "_":
                    this.setSZG.ctrUmbuchung_CustomizeBtnAuftraggeberEnabled(ref myBtn);
                    break;

                default:
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myBtn"></param>
        public void ctrUmbuchung_CustomizeBtnEmpfängerEnabled(ref System.Windows.Forms.Button myBtn)
        {
            switch (this.MatchCode)
            {
                case const_ClientMatchcode_SZG + "_":
                    this.setSZG.ctrUmbuchung_CustomizeBtnEmpfängerEnabled(ref myBtn);
                    break;

                default:
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myBtn"></param>
        public string ctrUmbuchung_Customize_SetLieferantenNr(ref clsSystem mySytem, clsUmbuchung myUB)
        {
            string strRet = string.Empty;
            switch (this.MatchCode)
            {
                case const_ClientMatchcode_SZG + "_":
                    strRet = szg_ctrUmbuchung_Customize_SetLieferantenNr.Execute(ref mySytem, myUB);
                    break;

                case const_ClientMatchcode_SIL + "_":
                    strRet = clsADRVerweis.GetSupplierNoBySenderAndReceiverAdr(myUB.EmpfaengerID,
                                                           myUB.EmpfaengerID,
                                                           mySytem.BenutzerID,
                                                           enumASNFileTyp.VDA4913.ToString(),
                                                           mySytem.AbBereich.ID);
                    break;

                default:
                    strRet = string.Empty;
                    break;
            }
            return strRet;
        }

        public string CreateArtikelIDRef(clsArtikel myArtikel)
        {
            string strReturn = string.Empty;
            //_ArtIDRef = string.Empty;
            if (this.Modul.Lager_Einlagerung_ArtikelIDRef_Create)
            {
                switch (this.Modul.Lager_Einglagerung_ArtikelIDRef_CreateProzedure)
                {
                    case clsModule.const_Lager_Einlagerung_ArtikelIDRef_SLE:
                        strReturn = sle_ArtikelIDReferenz.Execute(myArtikel);
                        break;

                    case clsModule.const_Lager_Einlagerung_ArtikelIDRef_SZG:
                        strReturn = vw_ArtikelIDReferenz.Execute(myArtikel);
                        break;

                    case clsModule.const_Lager_Einlagerung_ArtikelIDRef_Hons:
                        ////Ermitteln des Auftraggeber
                        //switch ((Int32)this.Lager.Eingang.Auftraggeber)
                        //{
                        //    case 19:
                        //        //SAZ Salzgitter
                        //        //Die ARtikel ID Ref wird aus folgenden Elementen gebildet:
                        //        //Produktionsnummer+Auftragnummer + Auftragposition
                        //        strReturn = tbProduktionsNr.Text +
                        //                    clsSystem.const_Default_ArtikelIDRefSeparator +
                        //                    tbExAuftrag.Text +
                        //                    clsSystem.const_Default_ArtikelIDRefSeparator +
                        //                    tbExAuftragPos.Text;
                        //        break;
                        //    default:
                        //        clsKunde tmpKD = new clsKunde();
                        //        tmpKD._GL_User = this.GL_User;
                        //        tmpKD.ADR_ID = this.Lager.Eingang.Auftraggeber;
                        //        tmpKD.FillbyAdrID();
                        //        strReturn = tbProduktionsNr.Text +
                        //                    clsSystem.const_Default_ArtikelIDRefSeparator +
                        //                    tbLfsNr.Text +
                        //                    clsSystem.const_Default_ArtikelIDRefSeparator +
                        //                    tmpKD.KD_ID.ToString();
                        //        break;
                        //}
                        break;
                    //alle anderen
                    default:
                        strReturn = string.Empty;
                        break;
                }
            }
            return strReturn;
        }

    }
}
