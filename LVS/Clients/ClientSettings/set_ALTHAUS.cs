using System;
using System.Collections.Generic;

namespace LVS
{
    public class set_ALTHAUS
    {

        /***********************************************************************************************
         *                             spedition Alhaus 
         * ********************************************************************************************/
        ///<summary>set_ALTHAUS / InitSettings</summary>
        ///<remarks>SZG
        ///         Datum:
        ///         Änderungen:</remarks
        public void InitSettings(clsClient ClientToSet)
        {
            SetModulToClient(ref ClientToSet.Modul);
            SetViewsToClient(ref ClientToSet);

            ////weitere nötige Einstellungen
            ClientToSet.Eingang_Artikel_DefaulEinheit = "Stück";
            ClientToSet.Eingang_Artikel_DefaulAnzahl = "1";
            ClientToSet.UserAnzahl = 2;
            ClientToSet.AdrID = 1;  //manuell hinzugefügt
            ClientToSet.DefaultASNParnter_Emp = 0;

            //Abrufe
            ClientToSet.DictArbeitsbereich_Abrufe_DefaulEmpfaengerAdrID = new Dictionary<decimal, decimal>();
            ClientToSet.DictArbeitsbereich_Abrufe_DefaultCompanyAdrID = new Dictionary<decimal, decimal>();

            //Eingänge
            ClientToSet.DictArbeitsbereich_Eingang_DefaultEmpfaengerAdrID = new Dictionary<decimal, decimal>();
            ClientToSet.DictArbeitsbereich_Eingang_DefaultEntladeAdrID = new Dictionary<decimal, decimal>();

            //Ausgänge
            ClientToSet.DictArbeitsbereich_Ausgang_DefaultEmpfaengerAdrID = new Dictionary<decimal, decimal>();
            ClientToSet.DictArbeitsbereich_Ausgang_DefaultEntladeAdrID = new Dictionary<decimal, decimal>();
            ClientToSet.DictArbeitsbereich_Ausgang_DefaultVersenderAdrID = new Dictionary<decimal, decimal>();
            ClientToSet.DictArbeitsbereich_Ausgang_DefaultBeladeAdrID = new Dictionary<decimal, decimal>();

            //Umbuchung
            ClientToSet.DictArbeitsbereich_Umbuchung_DefaultEmpfaengerAdrID = new Dictionary<decimal, decimal>();
            ClientToSet.DictArbeitsbereich_Umbuchung_DefaultAuftraggeberNeuAdrID = new Dictionary<decimal, decimal>();
        }
        ///<summary>set_ALTHAUS / SetModulToClient</summary>
        ///<remarks>
        ///         Datum:
        ///         Änderungen:</remarks>
        private void SetModulToClient(ref clsModule myModul)
        {
            //Hauptmenu
            //myModul.MainMenu_Stammdaten = true;
            myModul.MainMenu_Statistik = true;
            myModul.MainMenu_Disposition = false;
            //myModul.MainMenu_Fakurierung = true;
            //myModul.MainMenu_Lager = true;
            myModul.MainMenu_AuftragserfassungDispo = false;

            //Spedition
            myModul.Spedition = false;
            myModul.Spedition_Auftragserfassung = false;
            myModul.Spedition_Dispo = false;

            //STammdaten
            //myModul.Stammdaten_Adressen = true;              
            myModul.Stammdaten_Personal = false;
            myModul.Stammdaten_Fahrzeuge = false;
            //myModul.Stammdaten_Gut = true;                   
            myModul.Stammdaten_Gut_UseGutDefinition = true;     //Warengruppe / Güterarten
            //myModul.Stammdaten_GutShowAllwaysAll = true;
            myModul.Stammdaten_Relation = false;
            myModul.Stammdaten_Lagerortverwaltung = false;
            //myModul.Stammdaten_Schaeden = true;              
            //myModul.Stammdaten_Einheiten = true;             
            myModul.Stammdaten_ExtraCharge = false;
            myModul.Stammdaten_KontenPlan = false;

            //Archiv
            myModul.Archiv = false;

            //Lagerverwaltung
            //myModul.Lager_SearchGridInLifeTime = true;
            //myModul.Lager_Einlagerung = true;
            //myModul.Lager_Einlagerung_RetourBooking = false;
            myModul.Lager_Einlagerung_Print_DirectEingangDoc = false;
            myModul.Lager_Einlagerung_LagerOrt_manuell_Changeable = true;
            myModul.Lager_Einlagerung_ClearLagerOrteByArtikelCopy = false;  //geänder 08.11.2022 nach Rücksprache mit Herr Lorsbach
            myModul.Lager_Einlagerung_LagerOrt_Enabled_Werk = true;
            myModul.Lager_Einlagerung_LagerOrt_Enabled_Halle = true;
            myModul.Lager_Einlagerung_LagerOrt_Enabled_Ebene = true;
            myModul.Lager_Einlagerung_LagerOrt_Enabled_Platz = true;
            myModul.Lager_Einlagerung_EditAfterClose = true;
            myModul.Lager_Einlagerung_EditADRAfterClose = true;
            myModul.Lager_Einlagerung_EditExTransportRef = false;
            myModul.Lager_Einlagerung_ArtikelIDRef_Create = false;
            myModul.Lager_Einlagerung_BruttoEqualsNetto = true;
            myModul.Lager_Einlagerung_Enabeled_Einheit = true;
            myModul.Lager_Einglagerung_ArtikelIDRef_CreateProzedure = clsModule.const_Lager_Einlagerung_ArtikelIDRef_SZG;
            myModul.Lager_Einlagerung_CheckAllArtikel = true;
            myModul.Lager_UB_ArikelProduktionsnummerChange = false;   //Standard
            myModul.Lager_USEBKZ = false;

            myModul.Lager_SPL_OutFromEingang = false;
            myModul.Lager_SPL_SchadenRequire = true;
            myModul.Lager_SPL_AutoSPL = false;


            //Eingang Pfichtfelder
            myModul.Lager_Einlagerung_RequiredValue_Auftraggeber = false;
            myModul.Lager_Einlagerung_RequiredValue_LieferscheinNr = false;
            myModul.Lager_Einlagerung_RequiredValue_Halle = false;
            myModul.Lager_Einlagerung_RequiredValue_Reihe = false;
            myModul.Lager_Einlagerung_Artikel_RequiredValue_Produktionsnummer = true;
            myModul.Lager_Einlagerung_GArt_InfoMessageAllData = false;

            //myModul.Lager_Einlagerung_Reihenvorschlag = true;

            //Lager / Module
            //myModul.Lager_Umbuchung = true;      //Standard
            //myModul.Lager_Journal = true;        //Standard
            //myModul.Lager_Bestandsliste = true;  //Standard

            //myModul.Lager_Bestandsliste_TagesbestandOhneSPL = true;
            myModul.Lager_Bestandsliste_TagesbestandOhneSPL = false;

            myModul.Lager_Bestandsliste_PrintButtonReport_Bestand = true;  //Print Bestand über Report
            myModul.Lager_Bestandsliste_PrintButtonReport_Inventur = false;  // Print Inventur über Report
            myModul.Lager_Bestandsliste_PrintButtonGrid = true;
            myModul.Lager_Bestandsliste_BestandOverAllWorkspaces = false;
            //myModul.Lager_Sperrlager = true;     //Standard
            //myModul.Lager_FreeForCall = false;
            myModul.Lager_PostCenter = false;
            myModul.Lager_Arbeitsliste = true;
            myModul.Lager_DirectDelivery = false;
            myModul.Lager_DirectDeliveryTransformation = false;

            //...|LvsScan
            myModul.LvsScan = false;
            myModul.LvsScan_Inventory_List = false;
            myModul.LvsScan_Inventory_Scan = false;

            //ASN Verkehr
            myModul.ASNTransfer = false;
            myModul.ASN_Create_Man = false;
            myModul.ASN_Create_Auto = false;
            myModul.ASN_VDA4905LiefereinteilungenAktiv = false;
            myModul.ASN_VDA4913_LVS_ReadASN_TakeOverGArtValues = false;
            myModul.ASN_UserOldASNFileCreation = false;
            myModul.ASN_UseNewASNCreateFunction = false;

            //ASNCall 
            myModul.ASNCall_UserCallStatus = false;

            //Auslagerung
            //myModul.Lager_Auslagerung = true;
            //myModul.Lager_Auslagerung_Print_DirectAusgangDoc = false;
            //myModul.Lager_Auslagerung_DGVBestand_SortID_1 = false;
            //myModul.Lager_Auslagerung_EditAfterClose = true;

            //Print
            myModul.Print_OldVersion = false;  // Standard = true
            myModul.Print_GridPrint_ViewByGridPrint_Bestandsliste = true;

            //Fakturierung
            myModul.Fakt_LagerManuellSelection = true;
            myModul.Fakt_Lager = true;
            myModul.Fakt_Spedition = false;
            myModul.Fakt_Manuell = true;
            myModul.Fakt_Sonderkosten = true;   //Sondermodul
            myModul.Fakt_Rechnungsbuch = true;
            //myModul.Fakt_UB_DifferentCalcAssignment = false;
            myModul.Fakt_GetRGGSNrFromTable_Primekey = false;
            myModul.Fakt_GetRGGSNrFromTable_Mandant = true;
            myModul.Fakt_UseOneRGNrKreisForRGandGS = true;
            myModul.Fakt_DeactivateMenueCtrRGList = true;
            myModul.Fakt_eInvoiceIsAvailable = false;

            //Statistik
            myModul.Statistik_Lager = true;
            myModul.Statistik_FaktLager = false;
            myModul.Statistik_FaktDispo = false;
            myModul.Statistik_Waggonbuch = false;
            myModul.Statistik_Gesamtbestand = false;
            myModul.Statistik_Bestandsbewegungen = false;
            myModul.Statistik_durchschn_Lagerbestand = true;
            myModul.Statistik_druchschn_Lagerdauer = true;
            myModul.Statistik_Monatsuebersicht = true;

            //Search
            //myModul.EnableAdvancedSearch = true;
            //myModul.EnableDirectSearch = true;
            //myModul.EnableEditExAuftrag = false;		

            //Mail
            //myModul.Mail_UsingMainMailForMailing = true;
            //myModul.Mail_UsingNoReplyDefault = false;
            //myModul.Mail_SMTPServer = "smtp.1und1.de";
            //myModul.Mail_SMTPUser = "lager@althaus-sped.com";
            //myModul.Mail_SMTPPasswort = "AlthausLager2016!";
            //myModul.Mail_MailAdress = "Lager@Althaus-Sped.com";
            //myModul.Mail_SMTPPort = 587;
            //myModul.Mail_SMTPSSL = true;

            //Mail 14.01.2025
            myModul.Mail_UsingMainMailForMailing = true;
            myModul.Mail_UsingNoReplyDefault = false;
            myModul.Mail_SMTPServer = "sslout.df.eu";
            myModul.Mail_SMTPUser = "lager@althaus-sped.com";
            myModul.Mail_SMTPPasswort = "g.mH4n!q";
            myModul.Mail_MailAdress = "Lager@Althaus-Sped.com";
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
            //myModul.Xml_Uniport_CreateDirect_LEingang = false;
            //myModul.Xml_Uniport_CreateDirect_LAusgang = false;
            myModul.VDA_Use_KFZ = true;

            //*********** Telerik
            myModul.Telerik_GridPrint_SummaryRow = true;


        }
        ///<summary>set_ALTHAUS / SetViewsToClient</summary>
        ///<remarks>
        ///         Datum:
        ///         Änderungen:</remarks>
        private void SetViewsToClient(ref clsClient myClient)
        {
            List<string> Artikel;
            /*************************************  BESTAND *******************************/
            Artikel = new List<string>();
            //Artikel.Add("ArtikelID");
            Artikel.Add("LVSNr");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Werksnummer");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Brutto");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Gut");
            Artikel.Add("Empfänger");

            //Artikel.Add("Bestellnummer");
            //Artikel.Add("exMaterialnummer");
            //Artikel.Add("exBezeichnung");
            //Artikel.Add("Auftraggeber");

            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, "Bestand 1", Artikel, false);
            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, "Bestand 1", Artikel, true);


            Artikel = new List<string>();
            //Artikel.Add("ArtikelID");
            Artikel.Add("LVSNr");
            //Artikel.Add("Produktionsnummer");
            Artikel.Add("Werksnummer");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Brutto");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Gut");
            Artikel.Add("Empfänger");

            //Artikel.Add("Bestellnummer");
            //Artikel.Add("exMaterialnummer");
            //Artikel.Add("exBezeichnung");
            //Artikel.Add("Auftraggeber");

            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, "Bestand oProd", Artikel, false);
            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, "Bestand oProd", Artikel, true);

            Artikel = new List<string>();
            //Artikel.Add("ArtikelID");
            Artikel.Add("LVSNr");
            //Artikel.Add("Produktionsnummer");
            //Artikel.Add("Werksnummer");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Brutto");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Gut");
            Artikel.Add("Empfänger");

            //Artikel.Add("Bestellnummer");
            //Artikel.Add("exMaterialnummer");
            //Artikel.Add("exBezeichnung");
            //Artikel.Add("Auftraggeber");

            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, "Bestand oW", Artikel, false);
            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, "Bestand oW", Artikel, true);


            Artikel = new List<string>();
            Artikel.Add("ArtikelID");
            Artikel.Add("LVSNr");
            //Artikel.Add("Produktionsnummer");
            Artikel.Add("Werksnummer");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Brutto");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Gut");
            Artikel.Add("Empfänger");

            //Artikel.Add("Bestellnummer");
            //Artikel.Add("exMaterialnummer");
            //Artikel.Add("exBezeichnung");
            //Artikel.Add("Auftraggeber");

            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, "Bestand mArtID", Artikel, false);
            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, "Bestand mArtID", Artikel, true);



            Artikel = new List<string>();
            Artikel.Add("ArtikelID");
            Artikel.Add("LVSNr");
            Artikel.Add("Gut");
            Artikel.Add("Auftraggeber");
            Artikel.Add("Empfaenger");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Netto");
            Artikel.Add("Brutto");
            Artikel.Add("Eingang");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Ausgang");
            Artikel.Add("Ausgangsdatum");
            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, "comtec", Artikel, false);
            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, "comtec", Artikel, true);


            //--------------------------------------------- BestandLager
            Artikel = new List<string>();
            //Artikel.Add("ArtikelID");
            Artikel.Add("LVSNr");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Werksnummer");
            Artikel.Add("Gut");
            Artikel.Add("Charge");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Netto");
            Artikel.Add("Brutto");
            Artikel.Add("Eingang");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Empfänger");

            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, "Bestand 2", Artikel, false);
            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, "Bestand 2", Artikel, true);

            //--------------------------------------------- BestandLager
            Artikel = new List<string>();
            //Artikel.Add("ArtikelID");
            Artikel.Add("LVSNr");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Werksnummer");
            Artikel.Add("Gut");
            Artikel.Add("Charge");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Netto");
            Artikel.Add("Brutto");
            Artikel.Add("Eingang");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Empfänger");
            Artikel.Add("Werk");
            Artikel.Add("Halle");
            Artikel.Add("Reihe");
            Artikel.Add("Ebene");
            Artikel.Add("Platz");


            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, "Bestand 3", Artikel, false);
            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, "Bestand 3", Artikel, true);

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
            //Artikel.Add("ArtikelID");
            Artikel.Add("LVSNr");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Brutto");
            //Artikel.Add("Eingang");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Gut");
            Artikel.Add("Empfänger");
            //Artikel.Add("LVSNr");
            //Artikel.Add("Produktionsnummer");
            //Artikel.Add("Werksnummer");
            //Artikel.Add("Gut");
            //Artikel.Add("Laenge");
            //Artikel.Add("Breite");
            //Artikel.Add("Dicke");
            //Artikel.Add("Netto");
            //Artikel.Add("Brutto");
            //Artikel.Add("Eingangsdatum");
            //Artikel.Add("Ausgangsdatum");
            //Artikel.Add("Lagerdauer");
            myClient.AddToView(clsClient.const_ViewKategorie_Journal, "Journal 1", Artikel, false);
            myClient.AddToView(clsClient.const_ViewKategorie_Journal, "Journal 1", Artikel, true);


            /*************************************  Journal *******************************/
            Artikel = new List<string>();
            Artikel.Add("ArtikelID");
            Artikel.Add("LVSNr");
            Artikel.Add("Gut");
            Artikel.Add("Auftraggeber");
            Artikel.Add("Empfaenger");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Netto");
            Artikel.Add("Brutto");
            Artikel.Add("Eingang");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Werksnummer");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Ausgangsdatum");
            Artikel.Add("Lagerdauer");
            Artikel.Add("Empfänger");
            myClient.AddToView(clsClient.const_ViewKategorie_Journal, "comtec", Artikel, false);
            myClient.AddToView(clsClient.const_ViewKategorie_Journal, "comtec", Artikel, true);

            /*************************************  Journal SPL*******************************/
            Artikel = new List<string>();
            Artikel.Add("SPL_IN");
            Artikel.Add("SPL_OUT");
            Artikel.Add("LVSNr");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Werksnummer");
            Artikel.Add("Gut");
            Artikel.Add("Laenge");
            Artikel.Add("Breite");
            Artikel.Add("Dicke");
            Artikel.Add("Netto");
            Artikel.Add("Brutto");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Ausgangsdatum");
            Artikel.Add("Lagerdauer");
            Artikel.Add("ArtikelID");
            myClient.AddToView(clsClient.const_ViewKategorie_Journal, clsClient.const_ViewName_JournalSPL, Artikel, false);
            myClient.AddToView(clsClient.const_ViewKategorie_Journal, clsClient.const_ViewName_JournalSPL, Artikel, true);


            /*************************************  Lager Eingang   *******************************/
            Artikel = new List<string>();
            //Artikel.Add("ArtikelID");
            Artikel.Add("LVSNr");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Dicke");
            Artikel.Add("Brutto");
            Artikel.Add("Charge");
            Artikel.Add("Werksnummer");
            Artikel.Add("Auftraggeber");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Eingang");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Ausgang");
            Artikel.Add("Ausgangsdatum");
            Artikel.Add("Lagerort");

            myClient.AddToView(clsClient.const_ViewKategorie_LEingang, "Ansicht2", Artikel, false);

            /*************************************  Lager Ausgang   *******************************/
            Artikel = new List<string>();
            Artikel.Add("Selected");
            Artikel.Add("LVSNr");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Dicke");
            Artikel.Add("Brutto");
            Artikel.Add("Charge");
            Artikel.Add("Werksnummer");
            Artikel.Add("Bestellnummer");
            Artikel.Add("exMaterialnummer");
            Artikel.Add("exBezeichnung");
            Artikel.Add("Charge");
            Artikel.Add("Netto");
            Artikel.Add("Eingang");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("ArtikelID");
            myClient.AddToView(clsClient.const_ViewKategorie_LAusgang, "Ansicht3", Artikel, false);

            ///*************************************  Lager Ausgang dtAArtikel   *******************************/
            //Artikel = new List<string>();
            //Artikel.Add("Check");
            //Artikel.Add("LVSNr");
            //Artikel.Add("Produktionsnummer");
            //Artikel.Add("Dicke");
            //Artikel.Add("Brutto");
            //Artikel.Add("Charge");
            //Artikel.Add("Werksnummer");
            //Artikel.Add("Bestellnummer");
            //Artikel.Add("exMaterialnummer");
            //Artikel.Add("exBezeichnung");
            //Artikel.Add("Charge");
            //Artikel.Add("Netto");
            //Artikel.Add("Eingang");
            //Artikel.Add("Eingangsdatum");
            //Artikel.Add("ArtikelID");
            //myClient.AddToView(clsClient.const_ViewKategorie_LAusgangArtikel, "GMH-View", Artikel, false);

            Artikel = new List<string>();
            /*************************************  Search *******************************/
            Artikel = new List<string>();
            Artikel.Add("ArtikelID");
            Artikel.Add("LVSNr");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Werksnummer");
            Artikel.Add("Charge");
            Artikel.Add("Bestellnummer");
            Artikel.Add("exMaterialnummer");
            Artikel.Add("exBezeichnung");
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

            myClient.AddToView(clsClient.const_ViewKategorie_Search, "Bestandsuche", Artikel, false);

            //********************************** Arbeitsliste **************************** /
            Artikel = new List<string>();
            Artikel.Add("Prod.-Nr.");
            Artikel.Add("Abmessung");
            Artikel.Add("Netto");
            Artikel.Add("Brutto");
            Artikel.Add("Halle");
            Artikel.Add("Reihe");
            Artikel.Add("Bemerkung");
            myClient.AddToView(clsClient.const_ViewName_Arbeitsliste, clsClient.const_ViewKategorie_Arbeitsliste, Artikel, true);

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

            myClient.AddToView(clsClient.const_ViewKategorie_Abruf, clsClient.const_ViewName_Abruf, Artikel, false);

            Artikel = new List<string>();
            /*************************************  SPL *******************************/
            Artikel = new List<string>();
            Artikel.Add("ausbuchen");
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
            Artikel.Add("Schaden");
            myClient.AddToView(clsClient.const_ViewKategorie_SPL, clsClient.const_ViewName_SPL, Artikel, false);
        }
        //********************************************************************************************************  ctrBestand
        ///<summary>set_ALTHAUS / ctrEinlagerung_CustomizedSettbInfoInternEnabled</summary>
        ///<remarks></remarks>
        public void ctrBestand_CustomizeComboBestandArt(ref System.Windows.Forms.ComboBox myCB)
        {
            Int32 iTmp = myCB.FindString(clsLager.const_Bestandsart_Tagesbestand);
            if (iTmp != myCB.SelectedIndex)
            {
                myCB.SelectedIndex = iTmp;
            }
        }
        //********************************************************************************************************  ctrEinlagerung
        ///<summary>set_ALTHAUS / ctrEinlagerung_CustomizedSettbInfoInternEnabled</summary>
        ///<remarks>InfoIntern soll immer änderbar sein</remarks>
        public void ctrEinlagerung_CustomizedSetLabelWerksnummerText(ref System.Windows.Forms.Label myLabel)
        {
            myLabel.Text = "Werksnummer:";
        }
        ///<summary>set_ALTHAUS / ctrEinlagerung_CustomizedSettbInfoInternEnabled</summary>
        ///<remarks>Beim Anlegen des Lagereingangs soll immer:
        ///         Versender = Auftraggeber
        ///         EntladeID = Empfänger 
        ///         sein.</remarks>
        public void ctrEinlagerung_CustomizeDefault_Adressdaten(ref clsLager myLager)
        {
            myLager.Eingang.Versender = myLager.Eingang.Auftraggeber;
            myLager.Eingang.EntladeID = myLager.Eingang.Empfaenger;
        }
    }
}
