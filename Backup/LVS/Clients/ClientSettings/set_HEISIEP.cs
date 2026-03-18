using System.Collections.Generic;

namespace LVS
{
    public class set_HEISIEP
    {

        /***********************************************************************************************
         *                             spedition Alhaus 
         * ********************************************************************************************/
        ///<summary>set_HEISIEP / InitSettings</summary>
        ///<remarks>
        ///         Datum:
        ///         Änderungen:</remarks
        public void InitSettings(clsClient ClientToSet)
        {
            SetModulToClient(ref ClientToSet.Modul);
            SetViewsToClient(ref ClientToSet);

            ////weitere nötige Einstellungen
            ClientToSet.Eingang_Artikel_DefaulEinheit = "kg";
            ClientToSet.Eingang_Artikel_DefaulAnzahl = "1";
            ClientToSet.UserAnzahl = 10;
            ClientToSet.AdrID = 1189;  //manuell hinzugefügt
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
            myModul.MainMenu_Stammdaten = true;
            myModul.MainMenu_Statistik = false;
            myModul.MainMenu_Disposition = true;
            myModul.MainMenu_Fakurierung = true;
            myModul.MainMenu_Lager = true;
            myModul.MainMenu_AuftragserfassungDispo = true;

            //Spedition
            myModul.Spedition = false;
            myModul.Spedition_Auftragserfassung = false;
            myModul.Spedition_Dispo = false;

            //STammdaten
            myModul.Stammdaten_Adressen = true;
            myModul.Stammdaten_Personal = true;
            myModul.Stammdaten_Fahrzeuge = true;
            myModul.Stammdaten_Gut = true;
            myModul.Stammdaten_Gut_UseGutDefinition = false;     //Warengruppe / Güterarten
            myModul.Stammdaten_GutShowAllwaysAll = true;
            myModul.Stammdaten_Relation = true;
            myModul.Stammdaten_Lagerortverwaltung = true;
            myModul.Stammdaten_Schaeden = true;
            myModul.Stammdaten_Einheiten = true;
            myModul.Stammdaten_ExtraCharge = false;
            myModul.Stammdaten_KontenPlan = false;
            myModul.Stammdaten_StorelocationLable = false;

            //Archiv
            myModul.Archiv = true;

            //Lagerverwaltung
            myModul.Lager_SearchGridInLifeTime = true;
            myModul.Lager_Einlagerung = true;
            myModul.Lager_Einlagerung_RetourBooking = false;
            myModul.Lager_Einlagerung_Print_DirectEingangDoc = false;
            myModul.Lager_Einlagerung_Print_DirectList = false;
            myModul.Lager_Einlagerung_Print_DirectLabel = false;
            myModul.Lager_Einlagerung_Print_DirectLabelAfterCheckEingang = false;
            myModul.Lager_Einlagerung_LagerOrt_manuell_Changeable = false;
            myModul.Lager_Einlagerung_ClearLagerOrteByArtikelCopy = false;
            myModul.Lager_Einlagerung_LagerOrt_Enabled_Werk = false;
            myModul.Lager_Einlagerung_LagerOrt_Enabled_Halle = false;
            myModul.Lager_Einlagerung_LagerOrt_Enabled_Reihe = false;
            myModul.Lager_Einlagerung_LagerOrt_Enabled_Ebene = false;
            myModul.Lager_Einlagerung_LagerOrt_Enabled_Platz = false;
            myModul.Lager_Einlagerung_RequiredValue_Reihe = false;
            myModul.Lager_Einlagerung_EditAfterClose = true;
            myModul.Lager_Einlagerung_EditADRAfterClose = true;
            myModul.Lager_Einlagerung_EditExTransportRef = false;
            myModul.Lager_Einlagerung_ArtikelIDRef_Create = false;
            myModul.Lager_Einlagerung_CheckAllArtikel = false;
            myModul.Lager_Einlagerung_GArt_InfoMessageAllData = true;
            //myModul.Lager_Einlagerung_Reihenvorschlag = false;
            myModul.Lager_Einlagerung_CheckComplete = true;
            myModul.Lager_Einlagerung_SetCheckDate = false;
            myModul.Lager_Einlagerung_BruttoEqualsNetto = false;

            //ArtikelIDRef = Standard
            myModul.Lager_Einglagerung_ArtikelIDRef_CreateProzedure = clsModule.const_Lager_Einlagerung_ArtikelIDRef_Heisiep;
            //kein Prüfen der Produktionsnummer
            myModul.Lager_Einlagerung_Artikel_RequiredValue_Produktionsnummer = false;
            myModul.Lager_USEBKZ = true;


            myModul.Lager_SPL_OutFromEingang = false;
            myModul.Lager_SPL_SchadenRequire = true;
            myModul.Lager_SPL_AutoSPL = false;

            myModul.Lager_Bestandsliste_TagesbestandOhneSPL = false;

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
            //myModul.Lager_Auslagerung_Print_DirectAusgangListe = false;
            //myModul.Lager_Auslagerung_DGVBestand_SortID_1 = false;
            myModul.Lager_Auslagerung_EditAfterClose = true;
            //myModul.Lager_Auslagerung_Print_AdditionalTransportDoc = false;
            myModul.Lager_Auslagerung_CheckComplete = true;

            //Print
            myModul.Print_OldVersion = true;  // Standard = true
            //myModul.Print_GridPrint_ViewByGridPrint_Bestandsliste = true;
            myModul.Print_Documents_UseRGAnhang = false;

            //Fakturierung
            myModul.Fakt_LagerManuellSelection = true;
            myModul.Fakt_Lager = true;
            myModul.Fakt_Spedition = false;
            myModul.Fakt_Manuell = true;
            myModul.Fakt_Sonderkosten = false;   //Sondermodul
            myModul.Fakt_Rechnungsbuch = true;
            //myModul.Fakt_UB_DifferentCalcAssignment = false;
            myModul.Fakt_GetRGGSNrFromTable_Primekey = false;
            myModul.Fakt_GetRGGSNrFromTable_Mandant = true;
            myModul.Fakt_UseOneRGNrKreisForRGandGS = false;
            myModul.Fakt_DeactivateMenueCtrRGList = true;
            myModul.Fakt_eInvoiceIsAvailable = false;


            //Statistik
            //myModul.Statistik_Lager = true;
            //myModul.Statistik_FaktLager = false;
            //myModul.Statistik_FaktDispo = false;
            //myModul.Statistik_Waggonbuch = false;
            //myModul.Statistik_Gesamtbestand = false;
            //myModul.Statistik_Bestandsbewegungen = false;
            //myModul.Statistik_durchschn_Lagerbestand = true;
            //myModul.Statistik_druchschn_Lagerdauer = true;
            //myModul.Statistik_Monatsuebersicht = true;

            //-- Menü
            myModul.Menu_Einlagerung_Artikel_tsbtnLagerort = true;

            //Search
            myModul.EnableAdvancedSearch = false;
            myModul.EnableDirectSearch = true;
            //myModul.EnableEditExAuftrag = false;		

            //Excelexport
            myModul.Excel_UseOldExport = true;

            //Mail
            myModul.Mail_UsingMainMailForMailing = false;
            myModul.Mail_UsingNoReplyDefault = false;
            //myModul.Mail_SMTPServer = "smtp.1und1.de";
            //myModul.Mail_SMTPUser = "lager@althaus-sped.com";
            //myModul.Mail_SMTPPasswort = "AlthausLager2016!";
            //myModul.Mail_MailAdress = "Lager@Althaus-Sped.com";
            myModul.Mail_SMTPPort = 587;
            myModul.Mail_SMTPSSL = true;

            //Communicator
            //Direkte Verarbeitung der XML Dateien
            //myModul.Xml_Uniport_CreateDirect_LEingang = false;
            //myModul.Xml_Uniport_CreateDirect_LAusgang = false;
            myModul.VDA_Use_KFZ = true;

            //*********** Telerik
            myModul.Telerik_GridPrint_SummaryRow = true;


        }
        ///<summary>set_HEISIEP / SetViewsToClient</summary>
        ///<remarks>
        ///         Datum:
        ///         Änderungen:</remarks>
        private void SetViewsToClient(ref clsClient myClient)
        {
            List<string> Artikel;
            /*************************************  BESTAND *******************************/
            Artikel = new List<string>();
            Artikel.Add("LVSNr");
            Artikel.Add("Eingang");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Ausgang");
            Artikel.Add("Ausgangsdatum");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Werksnummer");
            Artikel.Add("Charge");
            Artikel.Add("Bestellnummer");
            Artikel.Add("exMaterialnummer");
            Artikel.Add("exBezeichnung");
            Artikel.Add("Auftraggeber");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Brutto");
            Artikel.Add("Laenge");
            Artikel.Add("Lagerort");

            myClient.AddToView("Bestand", "TestAnsicht", Artikel, false);
            myClient.AddToView("Journal", "TestAnsicht", Artikel, false);
            myClient.AddToView("Bestand", "TestAnsicht", Artikel, true);
            myClient.AddToView("Journal", "TestAnsicht", Artikel, true);
            myClient.AddToView("Search", "TestAnsicht", Artikel, false);

            /*************************************  Lager Eingang   *******************************/
            Artikel = new List<string>();
            Artikel.Add("LVSNr");
            Artikel.Add("Eingang");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Ausgang");
            Artikel.Add("Ausgangsdatum");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Werksnummer");
            Artikel.Add("Charge");
            Artikel.Add("Auftraggeber");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Brutto");
            Artikel.Add("Lagerort");

            myClient.AddToView("LEingang", "TestAnsicht", Artikel, false);

            /*************************************  Lager Ausgang   *******************************/

            Artikel = new List<string>();
            Artikel.Add("Selected");
            Artikel.Add("LVSNr");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Werksnummer");
            Artikel.Add("Charge");
            Artikel.Add("Bestellnummer");
            Artikel.Add("exMaterialnummer");
            Artikel.Add("exBezeichnung");
            Artikel.Add("Netto");
            Artikel.Add("Brutto");
            Artikel.Add("Eingang");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("ArtikelID");
            myClient.AddToView("LAusgang", "TestAnsicht", Artikel, false);

            /*************************************  Lager Ausgang dtAArtikel   *******************************/
            Artikel = new List<string>();
            Artikel.Add("Check");
            Artikel.Add("LVSNr");
            Artikel.Add("Werksnummer");
            Artikel.Add("Bestellnummer");
            Artikel.Add("Dicke");
            Artikel.Add("Brutto");
            Artikel.Add("exMaterialnummer");
            Artikel.Add("exBezeichnung");
            Artikel.Add("Eingang");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Charge");
            Artikel.Add("Netto");
            Artikel.Add("ArtikelID");
            myClient.AddToView("LAusgangA", "TestAnsicht", Artikel, false);


            /*************************************  Spedition AuftragDetailsArtikel dtAArtikel   *******************************/
            Artikel = new List<string>();
            Artikel.Add("Brutto");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Netto");
            Artikel.Add("Werksnummer");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("exBezeichnung");
            Artikel.Add("Charge");
            Artikel.Add("Bestellnummer");
            Artikel.Add("exMaterialnummer");
            Artikel.Add("Auftrag");
            Artikel.Add("AuftragPos");
            Artikel.Add("ArtikelID");
            myClient.AddToView("AuftragDetailsArtikel", "TestAnsicht", Artikel, false);

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

            //Artikel = new List<string>();
            ///*************************************  Search *******************************/
            //Artikel = new List<string>();
            //Artikel.Add("ArtikelID");
            //Artikel.Add("LVSNr");
            //Artikel.Add("Produktionsnummer");
            //Artikel.Add("Werksnummer");
            //Artikel.Add("Charge");
            //Artikel.Add("Bestellnummer");
            //Artikel.Add("exMaterialnummer");
            //Artikel.Add("exBezeichnung");
            //Artikel.Add("Auftraggeber");
            //Artikel.Add("Eingang");
            //Artikel.Add("Eingangsdatum");
            //Artikel.Add("Ausgang");
            //Artikel.Add("Ausgangdatum");
            //Artikel.Add("Brutto");
            //Artikel.Add("Dicke");
            //Artikel.Add("Breite");
            //Artikel.Add("Laenge");
            //Artikel.Add("Netto");
            //Artikel.Add("Brutto");

            //myClient.AddToView("Search", "TestAnsicht", Artikel, false);
        }

    }
}
