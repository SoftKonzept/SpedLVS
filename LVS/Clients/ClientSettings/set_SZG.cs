using System.Collections.Generic;

namespace LVS
{
    public class set_SZG
    {

        /***********************************************************************************************
         *                              SZG
         * ********************************************************************************************/

        public void InitSettings(clsClient ClientToSet)
        {
            SetModulToClient(ref ClientToSet.Modul);
            SetViewsToClient(ref ClientToSet);
            SetDocViewNames(ref ClientToSet);

            //weitere nötige Einstellungen
            ClientToSet.Eingang_Artikel_DefaulEinheit = "kg";
            ClientToSet.Eingang_Artikel_DefaulAnzahl = "1";
            //ClientToSet.Abrufe_DefaulEmpfaengerAdrID = 18; //VW
            ClientToSet.UserAnzahl = 10;
            ClientToSet.AdrID = 1;  //manuell hinzugefügt
            ClientToSet.DefaultASNParnter_Emp = 18;

            ClientToSet.ListVDA4905Sender.Clear();
            ClientToSet.ListVDA4905Sender.Add(18);  //AdrID von VW

            //Abrufe AbrufDefEmpfaengerId
            ClientToSet.DictArbeitsbereich_Abrufe_DefaulEmpfaengerAdrID = new Dictionary<decimal, decimal>();
            ClientToSet.DictArbeitsbereich_Abrufe_DefaulEmpfaengerAdrID.Add(1, 18);  //VW - VW Mosel
            ClientToSet.DictArbeitsbereich_Abrufe_DefaulEmpfaengerAdrID.Add(3, 59);   //Stahlo - Stahlo DIllenburg

            ClientToSet.DictArbeitsbereich_Abrufe_DefaultCompanyAdrID = new Dictionary<decimal, decimal>();
            ClientToSet.DictArbeitsbereich_Abrufe_DefaultCompanyAdrID.Add(1, 2);  //VW - VW Mosel  kommt aus SZG_Call

            //Eingänge EingangDefEmpfaengerId
            ClientToSet.DictArbeitsbereich_Eingang_DefaultEmpfaengerAdrID = new Dictionary<decimal, decimal>();
            ClientToSet.DictArbeitsbereich_Eingang_DefaultEmpfaengerAdrID.Add(1, 18); //VW - VW Mosel
            ClientToSet.DictArbeitsbereich_Eingang_DefaultEmpfaengerAdrID.Add(3, 59); //Stahlo - Stahlo DIllenburg
            ClientToSet.DictArbeitsbereich_Eingang_DefaultEmpfaengerAdrID.Add(5, 195); //BMW - BMW Leipzig
            // EingangDefEntladeId und EingangDefBeladeId
            ClientToSet.DictArbeitsbereich_Eingang_DefaultEntladeAdrID = new Dictionary<decimal, decimal>();
            ClientToSet.DictArbeitsbereich_Eingang_DefaultEntladeAdrID.Add(1, 1);  //VW - SZG
            ClientToSet.DictArbeitsbereich_Eingang_DefaultEntladeAdrID.Add(3, 59); //Stahlo - Stahlo DIllenburg
            ClientToSet.DictArbeitsbereich_Eingang_DefaultEntladeAdrID.Add(5, 195); //BMW - BMW Leipzig

            //Ausgänge  AusgangDefEmpfaengerId
            ClientToSet.DictArbeitsbereich_Ausgang_DefaultEmpfaengerAdrID = new Dictionary<decimal, decimal>();
            ClientToSet.DictArbeitsbereich_Ausgang_DefaultEmpfaengerAdrID.Add(1, 18);//VW - VW Mosel
            ClientToSet.DictArbeitsbereich_Ausgang_DefaultEmpfaengerAdrID.Add(3, 59); //Stahlo - Stahlo DIllenburg
            ClientToSet.DictArbeitsbereich_Ausgang_DefaultEmpfaengerAdrID.Add(5, 195); //BMW - BMW Leipzig

            // AusgangDefEntladeId
            ClientToSet.DictArbeitsbereich_Ausgang_DefaultEntladeAdrID = new Dictionary<decimal, decimal>();
            ClientToSet.DictArbeitsbereich_Ausgang_DefaultEntladeAdrID.Add(1, 18);
            ClientToSet.DictArbeitsbereich_Ausgang_DefaultEntladeAdrID.Add(3, 59); //Stahlo - Stahlo DIllenburg
            ClientToSet.DictArbeitsbereich_Ausgang_DefaultEntladeAdrID.Add(5, 195); //BMW - BMW Leipzigg

            // AusgangDefVersenderId
            ClientToSet.DictArbeitsbereich_Ausgang_DefaultVersenderAdrID = new Dictionary<decimal, decimal>();

            // AusgangDefBeladeId
            ClientToSet.DictArbeitsbereich_Ausgang_DefaultBeladeAdrID = new Dictionary<decimal, decimal>();
            ClientToSet.DictArbeitsbereich_Ausgang_DefaultBeladeAdrID.Add(1, 1);
            ClientToSet.DictArbeitsbereich_Ausgang_DefaultBeladeAdrID.Add(2, 59); //Arcelor - SLE Glauchau
            ClientToSet.DictArbeitsbereich_Ausgang_DefaultBeladeAdrID.Add(3, 59); //STahlo - SLE Glauchau
            ClientToSet.DictArbeitsbereich_Ausgang_DefaultBeladeAdrID.Add(4, 59); //Diverse - SLE Glauchau
            ClientToSet.DictArbeitsbereich_Ausgang_DefaultBeladeAdrID.Add(5, 59); //BMW - SLE Glauchau

            //Umbuchung UBDefEmpfaengerId
            ClientToSet.DictArbeitsbereich_Umbuchung_DefaultEmpfaengerAdrID = new Dictionary<decimal, decimal>();
            ClientToSet.DictArbeitsbereich_Umbuchung_DefaultEmpfaengerAdrID.Add(1, 18);
            ClientToSet.DictArbeitsbereich_Umbuchung_DefaultEmpfaengerAdrID.Add(5, 195);

            // UBDefAuftraggeberNeuId
            ClientToSet.DictArbeitsbereich_Umbuchung_DefaultAuftraggeberNeuAdrID = new Dictionary<decimal, decimal>();
            ClientToSet.DictArbeitsbereich_Umbuchung_DefaultAuftraggeberNeuAdrID.Add(1, 18);
            ClientToSet.DictArbeitsbereich_Umbuchung_DefaultAuftraggeberNeuAdrID.Add(5, 195);
        }
        ///<summary>set_SZG / SetModulToClientSZG_</summary>
        ///<remarks>SZG
        ///         Datum:
        ///         Änderungen:</remarks>
        //public static void SetModulToClient(ref clsModule myModul)
        private void SetModulToClient(ref clsModule myModul)
        {
            //Hauptmenu
            //myClient.Modul.MainMenu_Stammdaten = true;
            myModul.MainMenu_Statistik = true;
            myModul.MainMenu_Disposition = false;
            //this.Modul.MainMenu_Fakurierung = true;
            //this.Modul.MainMenu_Lager = true;
            myModul.MainMenu_AuftragserfassungDispo = false;

            //Disposition
            myModul.Spedition = false;
            myModul.Spedition_Auftragserfassung = false;
            myModul.Spedition_Dispo = false;

            //STammdaten
            myModul.Stammdaten_Adressen = true;
            myModul.Stammdaten_Personal = false;
            myModul.Stammdaten_Fahrzeuge = true;
            //this.Modul.Stammdaten_Gut = true;                
            //myModul.Stammdaten_Lagerreihenverwaltung = false; // bis zur Freigabe normal Lagerortverwaltung oder Lagerreihenverwaltung
            myModul.Stammdaten_Gut_UseGutDefinition = true;
            myModul.Stammdaten_Gut_UseGutDefinitionByASNTransfer = true;
            //this.Modul.Stammdaten_GutShowAllwaysAll = true;
            myModul.Stammdaten_UseGutAdrAssignment = true;   //zuweisung von Güterarten zur Adresse
            myModul.Stammdaten_Relation = false;
            myModul.Stammdaten_Lagerortverwaltung = false;

            //this.Modul.Stammdaten_Schaeden = true;              
            //this.Modul.Stammdaten_Einheiten = true;             
            myModul.Stammdaten_ExtraCharge = true;
            myModul.Stammdaten_KontenPlan = false;
            myModul.Stammdaten_StorelocationLable = true;

            //Archiv
            myModul.Archiv = true;

            //Lagerverwaltung
            //this.Modul.Lager_SearchGridInLifeTime = true;
            //this.Modul.Lager_Einlagerung = true;
            myModul.Lager_Einlagerung_RetourBooking = true;
            myModul.Lager_Einlagerung_Print_DirectEingangDoc = false;
            myModul.Lager_Einlagerung_Print_DirectList = false;
            myModul.Lager_Einlagerung_Print_DirectLabel = false;                              //Prüft directer Labeldruck wenn alle Artikel geprüft wurden
            myModul.Lager_Einlagerung_Print_DirectLabelAfterCheckEingang = false;              //Prüft directer Labeldruck nach Abschluss Eingang
            myModul.Lager_Einlagerung_LagerOrt_manuell_Changeable = true;
            myModul.Lager_Einlagerung_ClearLagerOrteByArtikelCopy = false;
            myModul.Lager_Einlagerung_LagerOrt_Enabled_Werk = false;
            myModul.Lager_Einlagerung_LagerOrt_Enabled_Halle = true;
            myModul.Lager_Einlagerung_LagerOrt_Enabled_Reihe = true;
            myModul.Lager_Einlagerung_LagerOrt_Enabled_Ebene = true;
            myModul.Lager_Einlagerung_LagerOrt_Enabled_Platz = true;
            //this.Modul.Lager_Einlagerung_EditAfterClose = true;
            myModul.Lager_Einlagerung_EditADRAfterClose = true;
            myModul.Lager_Einlagerung_EditAfterClose = true;
            myModul.Lager_Einlagerung_EditExTransportRef = false;
            myModul.Lager_Einlagerung_ArtikelIDRef_Create = true;
            myModul.Lager_Einglagerung_ArtikelIDRef_CreateProzedure = clsModule.const_Lager_Einlagerung_ArtikelIDRef_SZG;
            myModul.Lager_Einlagerung_CheckAllArtikel = false;
            myModul.Lager_Einlagerung_CheckComplete = false;
            myModul.Lager_Einlagerung_BruttoEqualsNetto = false;
            myModul.Lager_Einlagerung_Enabeled_Einheit = false;
            myModul.Lager_Eingang_FreeForChange = true;

            myModul.Lager_UB_ArikelProduktionsnummerChange = false;   //Standard
            myModul.Lager_SPL_SchadenRequire = true;

            //Eingang Pfichtfelder
            myModul.Lager_Einlagerung_RequiredValue_Auftraggeber = true;
            myModul.Lager_Einlagerung_RequiredValue_LieferscheinNr = true;
            myModul.Lager_Einlagerung_RequiredValue_Vehicle = true;
            myModul.Lager_Einlagerung_RequiredValue_Halle = false;
            myModul.Lager_Einlagerung_RequiredValue_Reihe = false;
            myModul.Lager_Einlagerung_Artikel_RequiredValue_Produktionsnummer = false;

            myModul.Lager_Einlagerung_SetCheckDate = false;
            myModul.Lager_UB_ArikelProduktionsnummerChange = false;   //Standard
            myModul.Lager_Artikel_UseKorreturStornierVerfahren = true;
            myModul.Lager_Artikel_FreeForChange = true;
            myModul.Lager_USEBKZ = false;

            //SPL - Sperrlager
            myModul.Lager_SPL_OutFromEingang = true;
            myModul.Lager_SPL_SchadenRequire = true;
            myModul.Lager_SPL_AutoSPL = true;
            myModul.Lager_SPL_PrintSPLDocument = true;
            myModul.Lager_SPL_AutoPrintSPLDocument = false;
            myModul.Lager_SPL_RebookInAltEingang = true;

            //Lager / Module
            myModul.Lager_Umbuchung = false;      //Standard
            //this.Modul.Lager_Journal = true;        //Standard
            //this.Modul.Lager_Bestandsliste = true;  //Standard
            myModul.Lager_Bestandsliste_TagesbestandOhneSPL = false;       // dafür wurd eine zusätzliche Liste im Bestand angelegt
            myModul.Lager_Bestandsliste_PrintButtonReport_Bestand = true;  //Print Bestand über Report
            myModul.Lager_Bestandsliste_PrintButtonReport_Inventur = true;  // Print Inventur über Report
            myModul.Lager_Bestandsliste_PrintButtonGrid = true;
            myModul.Lager_Bestandsliste_BestandOverAllWorkspaces = true;
            //this.Modul.Lager_Sperrlager = true;     //Standard
            myModul.Lager_FreeForCall = false;
            myModul.Lager_PostCenter = true;
            myModul.Lager_Arbeitsliste = true;
            myModul.Lager_DirectDelivery = false;
            myModul.Lager_DirectDeliveryTransformation = true;

            //...|LvsScan
            myModul.LvsScan = true;
            myModul.LvsScan_Inventory_List = true;
            myModul.LvsScan_Inventory_Scan = true;

            //...|Schaeden
            myModul.Lager_Schaeden_ShowPrintCenterAfterSelection = true;

            //--- Artikel menü
            myModul.Menu_Einlagerung_Artikel_tsbtnLagerort = false;

            //ASN Verkehr
            myModul.ASNTransfer = true;
            myModul.ASN_Create_Man = true;
            myModul.ASN_Create_Auto = false;
            myModul.ASN_VDA4905LiefereinteilungenAktiv = true;
            myModul.ASN_VDA4905LiefereinteilungenAktiv_SupplierDetails = true;
            myModul.ASN_VDA4913_LVS_ReadASN_TakeOverGArtValues = true;
            myModul.ASN_UseNewASNCreateFunction = true;
            myModul.ASN_UserOldASNFileCreation = true;

            //ASNCall 
            myModul.ASNCall_UserCallStatus = true;

            //Auslagerung
            //this.Modul.Lager_Auslagerung = true;
            myModul.Lager_Auslagerung_Print_DirectAusgangDoc = false;
            myModul.Lager_Auslagerung_Print_DirectAusgangListe = false;
            //this.Modul.Lager_Auslagerung_DGVBestand_SortID_1 = false;
            //this.Modul.Lager_Auslagerung_EditAfterClose = true;
            myModul.Lager_Auslagerung_EditAfterClose = false;
            myModul.Lager_Auslagerung_CheckComplete = false;
            myModul.Lager_Auslagerung_StoreOutDirect = false;

            //Auslagerung Pflichtfelder
            myModul.Lager_Auslagerung_RequiredReceiver = true;

            //Print
            myModul.Print_OldVersion = false;  // Standard = true
            myModul.Print_GridPrint_ViewByGridPrint_Bestandsliste = true;

            //Fakturierung
            myModul.Fakt_Lager = true;
            myModul.Fakt_Spedition = false;
            myModul.Fakt_Manuell = true;
            myModul.Fakt_Sonderkosten = true;   //Sondermodul
            //this.Modul.Fakt_UB_DifferentCalcAssignment = false;
            myModul.Fakt_Rechnungsbuch = true;
            myModul.Fakt_LagerManuellSelection = true;
            myModul.Fakt_GetRGGSNrFromTable_Primekey = false;
            myModul.Fakt_GetRGGSNrFromTable_Mandant = true;
            myModul.Fakt_UseOneRGNrKreisForRGandGS = true;
            myModul.Fakt_DeactivateMenueCtrRGList = true;
            myModul.Fakt_eInvoiceIsAvailable = true;

            myModul.PrimeyKey_LVSNRUseOneIDRange = true;

            //Statistik
            myModul.Statistik_Lager = true;
            myModul.Statistik_FaktLager = false;
            myModul.Statistik_FaktDispo = false;
            myModul.Statistik_Waggonbuch = false;
            myModul.Statistik_Gesamtbestand = false;
            myModul.Statistik_Bestandsbewegungen = false;
            myModul.Statistik_durchschn_Lagerbestand = false;
            myModul.Statistik_druchschn_Lagerdauer = false;
            myModul.Statistik_Monatsuebersicht = true;

            //Search
            //this.Modul.EnableAdvancedSearch = true;
            //this.Modul.EnableDirectSearch = true;
            //this.Modul.EnableEditExAuftrag = false;		

            //Mail
            //myModul.Mail_UsingMainMailForMailing = false;
            //myModul.Mail_UsingNoReplyDefault = false;
            //myModul.Mail_SMTPServer = "smtp.1und1.de";
            //myModul.Mail_SMTPUser = "lvsreport@comtec-noeker.de";
            //myModul.Mail_SMTPPasswort = "CTlvs2014";
            //myModul.Mail_MailAdress = "lvsreport@comtec-noeker.de";
            //myModul.Mail_SMTPPort = 587;
            //myModul.Mail_SMTPSSL = true;

            myModul.Mail_UsingMainMailForMailing = false;
            myModul.Mail_UsingNoReplyDefault = false;

            myModul.Mail_SMTPServer = "w012d21e.kasserver.com";
            myModul.Mail_SMTPUser = "m076aaf0";
            myModul.Mail_SMTPPasswort = "n3pneEkc-T6P";
            myModul.Mail_MailAdress = "noreply@szg-glauchau.de";
            myModul.Mail_SMTPPort = 587;
            myModul.Mail_SMTPSSL = true;

            myModul.Mail_Noreply_SMTPServer = myModul.Mail_SMTPServer;
            myModul.Mail_Noreply_SMTPUser = myModul.Mail_SMTPUser;
            myModul.Mail_Noreply_SMTPPasswort = myModul.Mail_SMTPPasswort;
            myModul.Mail_Noreply_MailAdress = myModul.Mail_MailAdress;
            myModul.Mail_Noreply_SMTPPort = myModul.Mail_SMTPPort;
            myModul.Mail_Noreply_SMTPSSL = myModul.Mail_SMTPSSL;

            //Communicator
            //Direkte Verarbeitung der XML Dateien
            //myClient.Modul.Xml_Uniport_CreateDirect_LEingang = false;
            //myClient.Modul.Xml_Uniport_CreateDirect_LAusgang = false;
            myModul.VDA_Use_KFZ = true;
        }
        ///<summary>set_SZG / SetViewsToClientSLE_</summary>
        ///<remarks>Test für Schulung</remarks>
        //public static void SetViewsToClient(clsClient myClient)
        private void SetViewsToClient(ref clsClient myClient)
        {
            List<string> Artikel;
            /*************************************  BESTAND *******************************/
            Artikel = new List<string>();
            Artikel.Add("LVSNr");
            Artikel.Add("Auftraggeber");
            Artikel.Add("Werksnummer");
            Artikel.Add("Gut");
            //Artikel.Add("GT - Art");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("exMaterialnummer");
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
            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, clsClient.const_ViewName_Bestand, Artikel, false);
            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, clsClient.const_ViewName_Bestand, Artikel, true);


            /*************************************  BESTAND *******************************/
            Artikel = new List<string>();
            Artikel.Add("LVSNr");
            Artikel.Add("Auftraggeber");
            Artikel.Add("Werksnummer");
            Artikel.Add("Gut");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("exMaterialnummer");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            //Artikel.Add("Laenge");
            Artikel.Add("Anzahl");
            Artikel.Add("Netto");
            Artikel.Add("Brutto");
            //Artikel.Add("Halle");
            //Artikel.Add("Reihe");
            //Artikel.Add("Ebene");
            //Artikel.Add("Platz");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Ausgangsdatum");
            Artikel.Add("Lagerdauer");
            Artikel.Add("Glühdatum");
            //Artikel.Add("Charge");
            //Artikel.Add("Schaden");
            //Artikel.Add("Lieferschein");
            //Artikel.Add("Bemerkung");
            //Artikel.Add("intInfo");
            //Artikel.Add("Brutto");
            //Artikel.Add("Bestellnummer");
            //Artikel.Add("exMaterialnummer");
            //Artikel.Add("exBezeichnung");
            //Artikel.Add("ArtikelID");
            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, "Bestand_1", Artikel, false);
            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, "Bestand_1", Artikel, true);


            ///*************************************  BESTAND *******************************/
            //Artikel = new List<string>();
            //Artikel.Add("Arbeitsbereich");
            //Artikel.Add("SPL");
            //Artikel.Add("LVSNr");
            //Artikel.Add("Auftraggeber");
            //Artikel.Add("Werksnummer");
            //Artikel.Add("Gut");
            ////Artikel.Add("GT - Art");
            //Artikel.Add("Produktionsnummer");
            //Artikel.Add("Dicke");
            //Artikel.Add("Breite");
            ////Artikel.Add("Laenge");
            //Artikel.Add("Anzahl");
            //Artikel.Add("Netto");
            //Artikel.Add("Brutto");
            //Artikel.Add("Halle");
            //Artikel.Add("Reihe");
            //Artikel.Add("Ebene");
            //Artikel.Add("Platz");
            //Artikel.Add("Eingangsdatum");
            ////Artikel.Add("Ausgangsdatum");
            //Artikel.Add("Lagerdauer");
            //Artikel.Add("Charge");
            //Artikel.Add("Schaden");
            //Artikel.Add("Lieferschein");
            //Artikel.Add("Bemerkung");
            //Artikel.Add("intInfo");
            //Artikel.Add("Glühdatum");
            ////Artikel.Add("Brutto");
            ////Artikel.Add("Bestellnummer");
            ////Artikel.Add("exMaterialnummer");
            ////Artikel.Add("exBezeichnung");
            ////Artikel.Add("ArtikelID");
            //myClient.AddToView(clsClient.const_ViewKategorie_Bestand, "Bestand_Novelis", Artikel, false);
            //myClient.AddToView(clsClient.const_ViewKategorie_Bestand, "Bestand_Novelis", Artikel, true);

            /*************************************  BESTAND *******************************/
            Artikel = new List<string>();
            Artikel.Add("LVSNr");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Werksnummer");
            Artikel.Add("Gut");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Ausgangsdatum");
            Artikel.Add("Lagerdauer");
            Artikel.Add("AbrDauer");
            Artikel.Add("Brutto");
            Artikel.Add("exMaterialnummer");
            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, "Bestand_Abr", Artikel, false);
            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, "Bestand_Abr", Artikel, true);



            Artikel = new List<string>();
            Artikel.Add("Prod.-Nr.");
            Artikel.Add("Abmessung");
            Artikel.Add("Netto");
            Artikel.Add("Brutto");
            Artikel.Add("Halle");
            Artikel.Add("Reihe");
            Artikel.Add("Bemerkung");
            Artikel.Add("exMaterialnummer");
            myClient.AddToView("Arbeitsliste", "Arbeitsliste", Artikel, true);
            myClient.AddToView("Arbeitsliste", "Arbeitsliste", Artikel, false);

            //--------------------------------------------- InventurLager
            Artikel = new List<string>();
            //Artikel.Add("ArtikelID");
            Artikel.Add("LVSNr");
            Artikel.Add("Reihe");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Werksnummer");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Brutto");
            Artikel.Add("Netto");
            Artikel.Add("iO");
            Artikel.Add("neueReihe");

            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, "InventurLager", Artikel, false);
            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, "InventurLager", Artikel, true);

            /*************************************  Journal *******************************/
            Artikel = new List<string>();
            Artikel.Add("LVSNr");
            Artikel.Add("Auftraggeber");
            Artikel.Add("Werksnummer");
            Artikel.Add("Gut");
            Artikel.Add("Art");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("exMaterialnummer");
            Artikel.Add("Charge");
            Artikel.Add("Anzahl");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Netto");
            Artikel.Add("Brutto");
            Artikel.Add("Schaden");
            Artikel.Add("Eingang");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Lagerdauer");
            Artikel.Add("Ausgang");
            Artikel.Add("Ausgangsdatum");
            Artikel.Add("Lieferschein");
            Artikel.Add("Halle");
            Artikel.Add("Reihe");
            Artikel.Add("Ebene");
            Artikel.Add("Platz");
            Artikel.Add("Bemerkung");
            //Artikel.Add("intInfo");
            //Artikel.Add("Ausgang");
            //Artikel.Add("Netto");
            //Artikel.Add("Brutto");
            //Artikel.Add("Charge");
            //Artikel.Add("Platz");
            //Artikel.Add("ArtikelID");

            myClient.AddToView(clsClient.const_ViewKategorie_Journal, clsClient.const_ViewName_Journal, Artikel, false);
            myClient.AddToView(clsClient.const_ViewKategorie_Journal, clsClient.const_ViewName_Journal, Artikel, true);


            /*************************************  Lager Eingang   *******************************/
            Artikel = new List<string>();
            Artikel.Add("Werksnummer");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("LVSNr");
            Artikel.Add("ArtikelID");
            myClient.AddToView(clsClient.const_ViewKategorie_LEingang, "VW", Artikel, false);

            /*************************************  Lager Eingang   *******************************/
            Artikel = new List<string>();
            Artikel.Add("Werksnummer");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("exMaterialnummer");
            Artikel.Add("LVSNr");
            Artikel.Add("ArtikelID");
            myClient.AddToView(clsClient.const_ViewKategorie_LEingang, "Mendritzki", Artikel, false);

            /*************************************  Lager Ausgang   *******************************/
            Artikel = new List<string>();
            Artikel.Add("Selected");
            Artikel.Add("LVSNr");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Werksnummer");
            Artikel.Add("Brutto");
            Artikel.Add("Netto");
            Artikel.Add("Anzahl");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Eingang");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Reihe");
            Artikel.Add("Platz");
            Artikel.Add("Einheit");
            Artikel.Add("Gut");
            Artikel.Add("Auftraggeber");
            Artikel.Add("Ausgang");
            Artikel.Add("ArtikelID");
            //Artikel.Add("ArtIDRef");
            myClient.AddToView(clsClient.const_ViewKategorie_LAusgang, "VW", Artikel, false);

            /*************************************  Lager Ausgang   *******************************/
            Artikel = new List<string>();
            Artikel.Add("Selected");
            Artikel.Add("LVSNr");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("exMaterialnummer");
            Artikel.Add("Werksnummer");
            Artikel.Add("Brutto");
            Artikel.Add("Netto");
            Artikel.Add("Anzahl");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Eingang");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Reihe");
            Artikel.Add("Platz");
            Artikel.Add("Einheit");
            Artikel.Add("Gut");
            Artikel.Add("Auftraggeber");
            Artikel.Add("Ausgang");
            Artikel.Add("ArtikelID");
            //Artikel.Add("ArtIDRef");
            myClient.AddToView(clsClient.const_ViewKategorie_LAusgang, "Mendritzki", Artikel, false);


            /*************************************  Lager Ausgang   *******************************/
            Artikel = new List<string>();
            Artikel.Add("ENTL");
            Artikel.Add("Check");
            Artikel.Add("LVSNr");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Werksnummer");
            Artikel.Add("Brutto");
            Artikel.Add("Netto");
            Artikel.Add("Anzahl");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Eingang");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Reihe");
            Artikel.Add("Platz");
            Artikel.Add("Einheit");
            Artikel.Add("Gut");
            Artikel.Add("Auftraggeber");
            Artikel.Add("Ausgang");
            Artikel.Add("ArtikelID");
            Artikel.Add("ArtIDRef");
            myClient.AddToView(clsClient.const_ViewKategorie_LAusgangArtikel, "VW", Artikel, false);

            /*************************************  Lager Ausgang   *******************************/
            Artikel = new List<string>();
            Artikel.Add("ENTL");
            Artikel.Add("Check");
            Artikel.Add("LVSNr");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Werksnummer");
            Artikel.Add("exMaterialnummer");
            Artikel.Add("Brutto");
            Artikel.Add("Netto");
            Artikel.Add("Anzahl");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Eingang");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Reihe");
            Artikel.Add("Platz");
            Artikel.Add("Einheit");
            Artikel.Add("Gut");
            Artikel.Add("Auftraggeber");
            Artikel.Add("Ausgang");
            Artikel.Add("ArtikelID");
            Artikel.Add("ArtIDRef");
            myClient.AddToView(clsClient.const_ViewKategorie_LAusgangArtikel, "Mendritzki", Artikel, false);

            Artikel = new List<string>();
            /*************************************  Search *******************************/
            Artikel = new List<string>();
            Artikel.Add("LVSNr");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Werksnummer");
            Artikel.Add("Auftraggeber");
            Artikel.Add("Eingang");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Ausgang");
            Artikel.Add("Ausgangdatum");
            Artikel.Add("Brutto");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Netto");
            Artikel.Add("Brutto");
            Artikel.Add("ArtikelID");
            Artikel.Add("Charge");
            Artikel.Add("Bestellnummer");
            Artikel.Add("exMaterialnummer");
            Artikel.Add("exBezeichnung");
            myClient.AddToView(clsClient.const_ViewKategorie_Search, "SZG", Artikel, false);

            Artikel = new List<string>();
            /*************************************  Call/Rebooking *******************************/
            Artikel = new List<string>();
            Artikel.Add("Select");
            Artikel.Add("Eintreffdatum");
            Artikel.Add("Eintreffzeit");
            Artikel.Add("LVSNr");
            Artikel.Add("Werksnummer");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Brutto");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Menge");
            Artikel.Add("Abladestelle");
            Artikel.Add("Empfänger");
            Artikel.Add("Spediteur");
            Artikel.Add("Reihe/Platz");
            Artikel.Add("Lieferant");
            Artikel.Add("Schaden");
            Artikel.Add("Bearbeiter");
            Artikel.Add("Schicht");
            Artikel.Add("erstellt");
            Artikel.Add("Aktion");
            Artikel.Add("ArtikelID");
            Artikel.Add("AbrufID");
            Artikel.Add("Referenz");
            Artikel.Add("Entladestelle");


            myClient.AddToView(clsClient.const_ViewKategorie_Abruf, clsClient.const_ViewName_Abruf, Artikel, false);


            Artikel = new List<string>();
            /*************************************  SPL *******************************/
            Artikel = new List<string>();
            Artikel.Add("ausbuchen");
            Artikel.Add("Zert");
            Artikel.Add("Datum SPL");
            Artikel.Add("Buchung");
            Artikel.Add("ArtikelID");
            Artikel.Add("LVSNr");
            Artikel.Add("Werksnummer");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Charge");
            Artikel.Add("Gut");
            Artikel.Add("Auftraggeber");
            Artikel.Add("Empfänger");
            Artikel.Add("Sperrgrund");
            Artikel.Add("Schaden");
            myClient.AddToView(clsClient.const_ViewKategorie_SPL, clsClient.const_ViewName_SPL, Artikel, false);

        }
        ///<summary>set_SZG / SetDocViewNames</summary>
        ///<remarks>Hier können die Anzeigenamen für die Dokumenten nach kundenwunsch angepasst werden</remarks>
        private void SetDocViewNames(ref clsClient myClient)
        {
            //if (myClient.INIDocuments is clsINIDocuments)
            //{
            //    foreach (KeyValuePair<string, string> pair in myClient.INIDocuments.DictINIDokuments)
            //    {
            //        switch (pair.Key)
            //        {
            //            ////Ausgangsdokumente
            //            //case clsINIDocuments.const_Key_LabelAll:
            //            //case clsINIDocuments.const_Key_LabelOne:
            //            //case clsINIDocuments.const_Key_SPLLabel:
            //            //case clsINIDocuments.const_Key_SchadenLable:
            //            //case clsINIDocuments.const_Key_SPLDoc:
            //            //case clsINIDocuments.const_Key_SchadenDoc:
            //            //case clsINIDocuments.const_Key_EingangAnzeige:
            //            //case clsINIDocuments.const_Key_EingangLfs:
            //            //case clsINIDocuments.const_Key_Eingangsliste:
            //            //case clsINIDocuments.const_Key_EingangAnzeigeMail:
            //            //case clsINIDocuments.const_Key_EingangAnzeigePerDayMail:
            //            //case clsINIDocuments.const_Key_EingangLfsMail:
            //            //case clsINIDocuments.const_Key_EingangDocMatKunden:
            //            //    pair.Value = "1234";
            //            //    break;
            //            default:
            //                break;
            //        }
            //    }
            //    myClient.INIDocuments.FillDictByClsMainDict(myClient.INIDocuments.DictINIDokuments);
            //}
        }

        ///<summary>set_SZG / CustomizeDGVGueterArtenView</summary>
        ///<remarks></remarks>
        //public void CustomizeDGVGueterArtenView(ref Telerik.WinControls.UI.RadGridView myGrd)
        //{
        //    for (Int32 i = 0; i <= myGrd.Columns.Count - 1; i++)
        //    {
        //        string colName = myGrd.Columns[i].Name.ToString();
        //        //Güterarten
        //        switch (colName)
        //        {
        //            case "ID":
        //                myGrd.Columns[i].IsVisible = true;
        //                break;

        //            case "ViewID":
        //                myGrd.Columns[i].HeaderText = "Matchcode";
        //                if (i != 1)
        //                {
        //                    myGrd.Columns.Move(i, 1);
        //                    i = 0;
        //                }
        //                myGrd.Columns[i].SortOrder = RadSortOrder.Ascending;
        //                break;

        //            case "Bezeichnung":
        //                if (i != 2)
        //                {
        //                    myGrd.Columns.Move(i, 2);
        //                    i = 0;
        //                }
        //                break;

        //            case "Auftraggeber":
        //                myGrd.Columns[i].HeaderText = "Auftraggeber";
        //                if (i != 3)
        //                {
        //                    myGrd.Columns.Move(i, 3);
        //                    i = 0;
        //                }
        //                //i = 0;
        //                break;

        //            case "ArtikelArt":
        //                myGrd.Columns[i].HeaderText = "Art";
        //                if (i != 4)
        //                {
        //                    myGrd.Columns.Move(i, 4);
        //                    i = 0;
        //                }
        //                break;

        //            case "Werksnummer":
        //                //myGrd.Columns[i].HeaderText = "Werksnummer";
        //                //myGrd.Columns.Move(i, 5);
        //                if (i != 5)
        //                {
        //                    myGrd.Columns.Move(i, 5);
        //                    i = 0;
        //                }
        //                break;

        //            case "Brutto":
        //                //this.dgvGArtList.Columns[i].IsVisible = false;
        //                break;

        //            case "NichtStapelbar":
        //                myGrd.Columns[i].HeaderText = "Nicht stapelbar";
        //                break;

        //            case "aktiv":
        //            case "activ":
        //            case "Verweis":
        //            case "Arbeitsbereich":
        //                myGrd.Columns[i].IsVisible = false;
        //                break;

        //            default:
        //                //this.dgvGArtList.Columns[i].IsVisible = false;
        //                break;
        //        }
        //        myGrd.Columns[i].AutoSizeMode = Telerik.WinControls.UI.BestFitColumnMode.DisplayedCells;

        //    }
        //}
        ///<summary>set_SZG / CustomizeDGVSchaedenView</summary>
        ///<remarks></remarks>
        public bool CustomizeDGVSchaedenView(string myHeaderText)
        {
            bool bReturn = false;
            switch (myHeaderText)
            {
                case "Bezeichnung":
                case "Beschreibung":
                case "aktiv":
                //case "Art":
                case "Code":
                case "AutoSPL":
                case "Schadensart":
                    bReturn = true;
                    break;
                default:
                    bReturn = false;
                    break;
            }
            return bReturn;
        }


        //*************************************************************************************************************  ctrASNCall
        ///<summary>clsClient / ctrASNCall_CustomizeTsbtnDeleteASN</summary>
        ///<remarks>Die Löschfunktion soll generell deaktiviert sein</remarks>
        public void ctrASNCall_CustomizeTsbtnDeleteASN(ref System.Windows.Forms.ToolStripButton myTB)
        {
            myTB.Visible = false;
        }
        //*************************************************************************************************************  ctrBSInfo4905
        ///<summary>clsClient / ctrASNCall_CustomizeTsbtnDeleteASN</summary>
        ///<remarks>Die Löschfunktion soll generell deaktiviert sein</remarks>
        public void ctrBSInfo4905_CustomizeCheckBoxRueckstand(ref System.Windows.Forms.CheckBox myCB)
        {
            myCB.Checked = true;
        }
        ///<summary>clsClient / ctrASNCall_CustomizeTsbtnDeleteASN</summary>
        ///<remarks>Die Löschfunktion soll generell deaktiviert sein</remarks>
        public void ctrBSInfo4905_CustomizeCheckBoxChecked(ref System.Windows.Forms.CheckBox myCB)
        {
            myCB.Checked = true;
        }
        //*************************************************************************************************************  ctrEinlagerung
        ///<summary>set_SZG / ctrEinlagerung_SetInputFieldEnabled</summary>
        ///<remarks>Deaktiviert die gewünschten Textfelder</remarks>
        //public void ctrEinlagerung_SetInputFieldReadOnly(ref System.Windows.Forms.TextBox textbox)
        //{
        //    switch (textbox.Name)
        //    {
        //        case "tbGArtZusatz":
        //        case "tbPos":
        //        //case "tbexMaterialnummer":
        //        case "tbexBezeichnung":
        //        case "tbHoehe":
        //        case "tbPackmittelGewicht":
        //        case "tbExLagerOrt":
        //        case "tbexAuftrag":
        //            textbox.ReadOnly = true;
        //            //textbox.BackColor = System.Drawing.SystemColors.ControlLight;
        //            textbox.BackColor = System.Drawing.SystemColors.Control;
        //            break;
        //        default:
        //            textbox.ReadOnly = false;
        //            textbox.BackColor = System.Windows.Forms.TextBox.DefaultBackColor;
        //            break;
        //    }
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myCombo"></param>
        public void ctrEinlagerung_CustomizedComboFahrzeugeSetStartValue_ComboFahrzeuge(ref System.Windows.Forms.ComboBox myCombo)
        {
            //public const string const_cbFahrzeugText_Fremdfahrzeug = "--Fremdfahrzeug--";
            Functions.SetComboToSelecetedItem(ref myCombo, "--Fremdfahrzeug--");
        }
        ///<summary>set_SZG / ctrEinlagerung_CustomizedSettbInfoInternEnabled</summary>
        ///<remarks>InfoIntern soll immer änderbar sein</remarks>
        public void ctrEinlagerung_CustomizedSettbInfoInternEnabled(ref System.Windows.Forms.TextBox myTB)
        {
            myTB.Enabled = true;
        }
        //*************************************************************************************************************  ctrPrintLager
        ///<summary>set_SZG / ctrPrintLager_SetPrintDocs</summary>
        ///<remarks></remarks>
        public bool ctrPrintLager_SetPrintDocs(string strCtrDocView)
        {
            bool bReturn = false;
            switch (strCtrDocView)
            {
                case "Alle Label":
                case "alle Label":
                case "Artikellabel":
                    bReturn = true;
                    break;
                default:
                    bReturn = false;
                    break;
            }

            return bReturn;
        }

        //************************************************************************************************************** ctrUmbuchung
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myBtn"></param>
        public void ctrUmbuchung_CustomizeBtnAuftraggeberEnabled(ref System.Windows.Forms.Button myBtn)
        {
            myBtn.Enabled = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myBtn"></param>
        public void ctrUmbuchung_CustomizeBtnEmpfängerEnabled(ref System.Windows.Forms.Button myBtn)
        {
            myBtn.Enabled = true;
        }

    }
}
