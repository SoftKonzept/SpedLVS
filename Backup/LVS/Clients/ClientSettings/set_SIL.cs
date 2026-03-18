using System.Collections.Generic;

namespace LVS
{
    public class set_SIL
    {

        /***********************************************************************************************
         *                             
         * ********************************************************************************************/

        public void InitSettings(clsClient ClientToSet)
        {
            SetModulToClient(ref ClientToSet.Modul);
            SetViewsToClient(ref ClientToSet);

            ////weitere nötige Einstellungen
            ClientToSet.UserAnzahl = clsClient.DefaultUserAnzahl;
            ClientToSet.Eingang_Artikel_DefaulEinheit = "Stück";
            ClientToSet.Eingang_Artikel_DefaulAnzahl = "1";
            ClientToSet.AdrID = 1;  //manuell hinzugefügt
            ClientToSet.DefaultASNParnter_Emp = 4;

            //Prüfen ob dies LIST noch gebraucht wird
            ClientToSet.ListVDA4905Sender.Clear();
            ClientToSet.ListVDA4905Sender.Add(5);  //AdrID von VW

            //Abrufe
            ClientToSet.DictArbeitsbereich_Abrufe_DefaulEmpfaengerAdrID = new Dictionary<decimal, decimal>();
            ClientToSet.DictArbeitsbereich_Abrufe_DefaultCompanyAdrID = new Dictionary<decimal, decimal>();

            //Eingänge
            ClientToSet.DictArbeitsbereich_Eingang_DefaultEmpfaengerAdrID = new Dictionary<decimal, decimal>();
            ClientToSet.DictArbeitsbereich_Eingang_DefaultEmpfaengerAdrID.Add(1, 4); //SIL - VW Emden

            ClientToSet.DictArbeitsbereich_Eingang_DefaultEntladeAdrID = new Dictionary<decimal, decimal>();
            ClientToSet.DictArbeitsbereich_Eingang_DefaultEntladeAdrID.Add(1, 1);  //SIL - SIL

            //Ausgänge
            ClientToSet.DictArbeitsbereich_Ausgang_DefaultEmpfaengerAdrID = new Dictionary<decimal, decimal>();
            ClientToSet.DictArbeitsbereich_Ausgang_DefaultEmpfaengerAdrID.Add(1, 4);

            ClientToSet.DictArbeitsbereich_Ausgang_DefaultEntladeAdrID = new Dictionary<decimal, decimal>();
            ClientToSet.DictArbeitsbereich_Ausgang_DefaultEntladeAdrID.Add(1, 4);

            ClientToSet.DictArbeitsbereich_Ausgang_DefaultVersenderAdrID = new Dictionary<decimal, decimal>();

            ClientToSet.DictArbeitsbereich_Ausgang_DefaultBeladeAdrID = new Dictionary<decimal, decimal>();
            ClientToSet.DictArbeitsbereich_Ausgang_DefaultBeladeAdrID.Add(1, 1);

            //Umbuchung
            ClientToSet.DictArbeitsbereich_Umbuchung_DefaultEmpfaengerAdrID = new Dictionary<decimal, decimal>();
            ClientToSet.DictArbeitsbereich_Umbuchung_DefaultAuftraggeberNeuAdrID = new Dictionary<decimal, decimal>();
        }
        ///<summary>clsClient / SetModulToClientSZG_</summary>
        ///<remarks>SZG
        ///         Datum:
        ///         Änderungen:</remarks>
        //public static void SetModulToClient(ref clsModule myModul)
        private void SetModulToClient(ref clsModule myModul)
        {
            //Hauptmenu
            //this.Modul.MainMenu_Stammdaten = true;
            myModul.MainMenu_Statistik = true;
            myModul.MainMenu_Disposition = false;
            myModul.MainMenu_Fakurierung = true;
            //myModul.MainMenu_Lager = true;
            myModul.MainMenu_AuftragserfassungDispo = false;

            //Disposition
            myModul.Spedition = false;
            myModul.Spedition_Auftragserfassung = false;
            myModul.Spedition_Dispo = false;

            //STammdaten
            //myModul.Stammdaten_Adressen = true;              
            myModul.Stammdaten_Personal = false;
            myModul.Stammdaten_Fahrzeuge = false;
            //this.Modul.Stammdaten_Gut = true;                   
            myModul.Stammdaten_Gut_UseGutDefinition = true;     //Warengruppe / Güterarten
            myModul.Stammdaten_Gut_UseGutDefinitionByASNTransfer = true;
            //this.Modul.Stammdaten_GutShowAllwaysAll = true;
            myModul.Stammdaten_UseGutAdrAssignment = true;   //zuweisung von Güterarten zur Adresse
            myModul.Stammdaten_Relation = false;
            myModul.Stammdaten_Lagerortverwaltung = false;
            //this.Modul.Stammdaten_Schaeden = true;              
            //this.Modul.Stammdaten_Einheiten = true;             
            myModul.Stammdaten_ExtraCharge = true;
            myModul.Stammdaten_KontenPlan = true;
            myModul.Stammdaten_StorelocationLable = false;

            //Archiv
            myModul.Archiv = true;

            //Lagerverwaltung
            myModul.Lager_SearchGridInLifeTime = false;
            //this.Modul.Lager_Einlagerung = true;
            //myModul.Lager_Einlagerung_RetourBooking = false;
            myModul.Lager_Einlagerung_Print_DirectEingangDoc = false;
            myModul.Lager_Einlagerung_Print_DirectLabel = false;
            myModul.Lager_Einlagerung_Print_DirectLabelAfterCheckEingang = true;
            myModul.Lager_Einlagerung_ShowDirectPrintCenter = false;
            myModul.Lager_Einlagerung_LagerOrt_manuell_Changeable = true;
            myModul.Lager_Einlagerung_ClearLagerOrteByArtikelCopy = true;
            myModul.Lager_Einlagerung_BruttoEqualsNetto = false;
            myModul.Lager_Einlagerung_LagerOrt_Enabled_Werk = false;
            myModul.Lager_Einlagerung_LagerOrt_Enabled_Halle = false;
            myModul.Lager_Einlagerung_LagerOrt_Enabled_Reihe = true;
            myModul.Lager_Einlagerung_LagerOrt_Enabled_Ebene = false;
            myModul.Lager_Einlagerung_LagerOrt_Enabled_Platz = true;
            myModul.Lager_Einlagerung_EditADRAfterClose = false;
            myModul.Lager_Einlagerung_EditAfterClose = false;
            myModul.Lager_Einlagerung_EditExTransportRef = false;
            myModul.Lager_Einlagerung_ArtikelIDRef_Create = true;
            myModul.Lager_Einglagerung_ArtikelIDRef_CreateProzedure = clsModule.const_Lager_Einlagerung_ArtikelIDRef_SZG;
            myModul.Lager_Einlagerung_CheckAllArtikel = false;
            myModul.Lager_Einlagerung_CheckComplete = false;
            myModul.Lager_Einlagerung_GArt_InfoMessageAllData = false;

            myModul.Lager_Eingang_FreeForChange = false;

            //Eingang Pfichtfelder
            myModul.Lager_Einlagerung_RequiredValue_Auftraggeber = true;
            myModul.Lager_Einlagerung_RequiredValue_LieferscheinNr = true;
            myModul.Lager_Einlagerung_RequiredValue_Vehicle = true;
            myModul.Lager_Einlagerung_RequiredValue_Halle = false;
            myModul.Lager_Einlagerung_RequiredValue_Reihe = false;
            //myModul.Lager_Einlagerung_Reihenvorschlag = true;

            myModul.Lager_Einlagerung_Artikel_RequiredValue_Produktionsnummer = true;
            myModul.Lager_Einlagerung_Artikel_RequiredValue_Netto = true;
            myModul.Lager_Einlagerung_Artikel_RequiredValue_Brutto = true;
            myModul.Lager_Einlagerung_Artikel_RequiredValue_Laenge = true;

            myModul.Lager_Einlagerung_SetCheckDate = true;
            myModul.Lager_UB_ArikelProduktionsnummerChange = false;   //Standard
            myModul.Lager_Artikel_UseKorreturStornierVerfahren = true;
            myModul.Lager_Artikel_FreeForChange = false;
            myModul.Lager_USEBKZ = false;

            //SPL - Sperrlager
            myModul.Lager_SPL_OutFromEingang = true;
            myModul.Lager_SPL_SchadenRequire = true;
            myModul.Lager_SPL_AutoSPL = true;
            myModul.Lager_SPL_PrintSPLDocument = true;
            myModul.Lager_SPL_AutoPrintSPLDocument = false;
            myModul.Lager_SPL_RebookInAltEingang = true;

            //Lager / Module
            //this.Modul.Lager_Umbuchung = true;      //Standard
            //this.Modul.Lager_Journal = true;        //Standard
            //this.Modul.Lager_Bestandsliste = true;  //Standard
            myModul.Lager_Bestandsliste_TagesbestandOhneSPL = true;
            myModul.Lager_Bestandsliste_PrintButtonReport_Bestand = false;  //Print Bestand über Report
            myModul.Lager_Bestandsliste_PrintButtonReport_Inventur = false;  // Print Inventur über Report
            myModul.Lager_Bestandsliste_PrintButtonGrid = true;
            myModul.Lager_Bestandsliste_BestandOverAllWorkspaces = false;
            //this.Modul.Lager_Sperrlager = true;     //Standard
            //this.Modul.Lager_FreeForCall = false;
            myModul.Lager_PostCenter = true;
            myModul.Lager_Arbeitsliste = true;

            //Lagerverwaltung | Einlagerung | DirectDelivery
            myModul.Lager_DirectDelivery = false;
            myModul.Lager_DirectDeliveryTransformation = false;

            //ASN Verkehr
            myModul.ASNTransfer = true;
            myModul.ASN_Create_Man = true;
            myModul.ASN_Create_Auto = false;
            myModul.ASN_VDA4905LiefereinteilungenAktiv = true;
            myModul.ASN_VDA4913_LVS_ReadASN_TakeOverGArtValues = true;
            myModul.ASN_UserOldASNFileCreation = true;
            myModul.ASN_UseNewASNCreateFunction = true;
            myModul.ASN_AutoCreateNewGArtByASN = true;

            //ASNCall 
            myModul.ASNCall_UserCallStatus = false;

            //Auslagerung
            //this.Modul.Lager_Auslagerung = true;
            //this.Modul.Lager_Auslagerung_Print_DirectAusgangDoc = false;
            //this.Modul.Lager_Auslagerung_DGVBestand_SortID_1 = false;
            myModul.Lager_Auslagerung_ShowDirectPrintCenter = false;
            myModul.Lager_Auslagerung_EditAfterClose = false;
            myModul.Lager_Auslagerung_CheckComplete = false;

            //Print
            myModul.Print_OldVersion = true;  // Standard = true
            myModul.Print_GridPrint_ViewByGridPrint_Bestandsliste = false;
            //myModul.Print_GridPrint_ViewByReport_Journal = true;

            //Fakturierung
            myModul.Fakt_LagerManuellSelection = true;
            myModul.Fakt_Lager = true;
            myModul.Fakt_Spedition = false;
            myModul.Fakt_Manuell = true;
            myModul.Fakt_Sonderkosten = true;   //Sondermodul
            myModul.Fakt_Rechnungsbuch = true;
            //this.Modul.Fakt_UB_DifferentCalcAssignment = false;
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
            myModul.Statistik_durchschn_Lagerbestand = false;
            myModul.Statistik_druchschn_Lagerdauer = false;
            myModul.Statistik_Monatsuebersicht = true;

            //Search
            //this.Modul.EnableAdvancedSearch = true;
            //this.Modul.EnableDirectSearch = true;
            //this.Modul.EnableEditExAuftrag = false;		

            //Mail
            myModul.Mail_UsingMainMailForMailing = true;
            myModul.Mail_UsingNoReplyDefault = true;

            myModul.Mail_SMTPServer = "smtpout.md-systemhaus.de";
            myModul.Mail_SMTPUser = "sil";
            myModul.Mail_SMTPPasswort = "MPm3wX2L";
            myModul.Mail_MailAdress = "NoReply_LVS@sil-steffens.de";
            myModul.Mail_SMTPPort = 25;
            myModul.Mail_SMTPSSL = false;

            myModul.Mail_Noreply_SMTPServer = myModul.Mail_SMTPServer;
            myModul.Mail_Noreply_SMTPUser = myModul.Mail_SMTPUser;
            myModul.Mail_Noreply_SMTPPasswort = myModul.Mail_SMTPPasswort;
            myModul.Mail_Noreply_MailAdress = myModul.Mail_MailAdress;
            myModul.Mail_Noreply_SMTPPort = myModul.Mail_SMTPPort;
            myModul.Mail_Noreply_SMTPSSL = myModul.Mail_SMTPSSL;

            //Communicator
            //Direkte Verarbeitung der XML Dateien
            //this.Modul.Xml_Uniport_CreateDirect_LEingang = false;
            //this.Modul.Xml_Uniport_CreateDirect_LAusgang = false;
            myModul.VDA_Use_KFZ = true;

        }
        ///<summary>clsClient / SetViewsToClientSLE_</summary>
        ///<remarks>Test für Schulung</remarks>
        //public static void SetViewsToClient(clsClient myClient)
        private void SetViewsToClient(ref clsClient myClient)
        {
            List<string> Artikel;
            /*************************************  BESTAND *******************************/
            Artikel = new List<string>();
            Artikel.Add("LVSNr");
            Artikel.Add("Auftraggeber");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Werksnummer");
            Artikel.Add("Gut");
            Artikel.Add("Anzahl");
            Artikel.Add("Netto");
            Artikel.Add("Brutto");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("Reihe");
            Artikel.Add("Platz");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Ausgangsdatum");
            Artikel.Add("Lagerdauer");
            Artikel.Add("LagerdauerST");
            //Artikel.Add("Charge");
            //Artikel.Add("Brutto");
            //Artikel.Add("Bestellnummer");
            //Artikel.Add("exMaterialnummer");
            //Artikel.Add("exBezeichnung");
            //myClient.AddToView("Bestand", "Bestand", Artikel, false);
            //myClient.AddToView("Bestand", "Bestand", Artikel, true);

            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, clsClient.const_ViewName_Bestand, Artikel, false);
            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, clsClient.const_ViewName_Bestand, Artikel, true);


            //--------------------------------------------- InventurLager
            //Artikel = new List<string>();
            ////Artikel.Add("ArtikelID");
            //Artikel.Add("LVSNr");
            //Artikel.Add("Reihe");
            //Artikel.Add("Produktionsnummer");
            //Artikel.Add("Werksnummer");
            //Artikel.Add("Dicke");
            //Artikel.Add("Breite");
            //Artikel.Add("Laenge");
            //Artikel.Add("Brutto");
            //Artikel.Add("Netto");
            //Artikel.Add("iO");
            //Artikel.Add("neueReihe");

            //myClient.AddToView("Bestand", "InventurLager", Artikel, false);
            //myClient.AddToView("Bestand", "InventurLager", Artikel, true);

            /*************************************  Journal *******************************/
            Artikel = new List<string>();
            Artikel.Add("LVSNr");
            Artikel.Add("Auftraggeber");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Werksnummer");
            Artikel.Add("Gut");
            Artikel.Add("Charge");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Ausgangsdatum");
            Artikel.Add("Lagerdauer");
            Artikel.Add("Netto");
            Artikel.Add("Brutto");
            Artikel.Add("Anzahl");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("ArtikelID");
            //myClient.AddToView("Journal", "Journal", Artikel, false);
            //myClient.AddToView("Journal", "Journal", Artikel, true);

            myClient.AddToView(clsClient.const_ViewKategorie_Journal, clsClient.const_ViewName_Journal, Artikel, false);
            myClient.AddToView(clsClient.const_ViewKategorie_Journal, clsClient.const_ViewName_Journal, Artikel, true);

            /*************************************  Journal SPL*******************************/
            Artikel = new List<string>();
            Artikel.Add("LVSNr");
            Artikel.Add("Auftraggeber");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Werksnummer");
            Artikel.Add("Gut");
            Artikel.Add("Charge");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Ausgangsdatum");
            Artikel.Add("SPL_IN");
            Artikel.Add("SPL_OUT");
            Artikel.Add("Lagerdauer");
            Artikel.Add("Netto");
            Artikel.Add("Brutto");
            Artikel.Add("Anzahl");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            Artikel.Add("ArtikelID");
            myClient.AddToView(clsClient.const_ViewKategorie_Journal, clsClient.const_ViewName_JournalSPL, Artikel, false);
            myClient.AddToView(clsClient.const_ViewKategorie_Journal, clsClient.const_ViewName_JournalSPL, Artikel, true);


            /*************************************  Lager Eingang   *******************************/
            Artikel = new List<string>();
            Artikel.Add("Werksnummer");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("LVSNr");
            Artikel.Add("ArtikelID");
            myClient.AddToView(clsClient.const_ViewKategorie_LEingang, "SIL", Artikel, false);

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
            Artikel.Add("ArtIDRef");
            //myClient.AddToView("LAusgang", "SIL-VW", Artikel, false);
            myClient.AddToView(clsClient.const_ViewKategorie_LAusgang, "SIL-VW", Artikel, false);


            /*************************************  Lager Ausgang   *******************************/
            Artikel = new List<string>();
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
            //myClient.AddToView("LAusgangA", "SIL-VW", Artikel, false);
            myClient.AddToView(clsClient.const_ViewKategorie_LAusgangArtikel, "SIL-VW", Artikel, false);


            Artikel = new List<string>();
            /*************************************  Search *******************************/
            Artikel = new List<string>();
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
            Artikel.Add("ArtikelID");

            myClient.AddToView(clsClient.const_ViewKategorie_Search, "SIL", Artikel, false);


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

        ///<summary>SIL_spFunc / CustomizeCtrGueterArtenList</summary>
        ///<remarks></remarks>
        public void CustomizeCtrGueterArtenList(ref List<string> myList)
        {
            myList.Clear();
            myList.Add("Coils");
            myList.Add("Paletten");
            myList.Add("Platinen");
        }

        //**************************************************************************************************************** ctrBestand
        ///<summary>clsClient / ctrBestand_CustomizeGroupListCheckedUncheckedStoreINAndOUT</summary>
        ///<remarks></remarks>
        public bool ctrBestand_CustomizeGroupListCheckedUncheckedStoreINAndOUT()
        {
            bool bReturn = false;
            return bReturn;
        }
        ///<summary>set_SIL / ctrEinlagerung_tbInfoAusgang_SetText</summary>
        ///<remarks>Ausgabe der Ausgangsinfo erst wenn der Ausgang abgeschlossen ist</remarks>
        public void ctrEinlagerung_tbInfoAusgang_SetText(ref clsLager myLager, ref System.Windows.Forms.TextBox myTBInfoAusgang)
        {
            if ((myLager.Artikel.Ausgang is clsLAusgang) && (!myLager.Artikel.Ausgang.Checked))
            {
                myTBInfoAusgang.Text = string.Empty;
            }
        }
        //**************************************************************************************************************** ctrStatistik
        ///<summary>set_SLE / ctrStatistik_CustomizeCtrCheckBoxRLExcl</summary>
        ///<remarks></remarks>
        public void ctrStatistik_CustomizeCtrCheckBoxRLExcl(ref System.Windows.Forms.CheckBox myCheckBox)
        {
            myCheckBox.Checked = true;
        }
        ///<summary>set_SLE / ctrStatistik_CustomizeCtrCheckBoxRLExcl</summary>
        ///<remarks></remarks>
        public void ctrStatistik_CustomizeCtrCheckBoxSchaedenExcl(ref System.Windows.Forms.CheckBox myCheckBox)
        {
            myCheckBox.Checked = false;
        }
        ///<summary>set_SLE / ctrStatistik_CustomizeCtrCheckBoxSPLExcl</summary>
        ///<remarks></remarks>
        public void ctrStatistik_CustomizeCtrCheckBoxSPLExcl(ref System.Windows.Forms.CheckBox myCheckBox)
        {
            myCheckBox.Checked = true;
        }
        ///<summary>set_SIL / ctrStatistik_CustomizeDGVMonatsuebersicht_Einlagerung</summary>
        ///<remarks></remarks>
        //public string ctrStatistik_CustomizeDGVMonatsuebersicht_Einlagerung(string strCol, decimal myAbBereichID, DateTime mydtpVon, DateTime mydtpBis, decimal myAdrID, bool bLagerKomplett)
        //{
        //    string strReturn = string.Empty;
        //    strReturn += ", (select SUM(a."+ strCol + ")/1000 From Artikel a "+
        //                                                "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
        //                                                "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
        //                                                "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
        //                                                "WHERE " +
        //                                                "b.[Check]=1 AND b.DirectDelivery=0 " +
        //                                                "AND b.AbBereich=" + (Int32)myAbBereichID + " ";

        //                                                if (!bLagerKomplett)
        //                                                {
        //                                                    strReturn += "AND b.Auftraggeber=" + myAdrID + " ";
        //                                                }
        //                                                strReturn += "AND (b.Date between '" + mydtpVon.Date.ToShortDateString() + "' AND '" + mydtpBis.Date.AddDays(1).ToShortDateString() + "')" +
        //                                                //nur ohne schaden
        //                                                //" AND a.ID NOT IN (" +
        //                                                //                    "SELECT sch.ArtikelID FROM SchadenZuweisung sch " +
        //                                                //                                            "INNER JOIN Artikel art on sch.ArtikelID=art.ID " +
        //                                                //                    ") " +
        //                                                                    ") as [" + strCol + " Eingang] ";
        //    return strReturn;
        //}
        /////<summary>set_SIL / ctrStatistik_CustomizeDGVMonatsuebersicht_Auslagerung</summary>
        /////<remarks></remarks>
        //public string ctrStatistik_CustomizeDGVMonatsuebersicht_Auslagerung(string strCol, decimal myAbBereichID, DateTime mydtpVon, DateTime mydtpBis, decimal myAdrID, bool bLagerKomplett)
        //{
        //    string strReturn = string.Empty;
        //    strReturn += ", (select SUM(a." + strCol + ")/1000 From Artikel a " +
        //                          "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
        //                          "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
        //                          "INNER JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
        //                          "WHERE " +
        //                          "c.Checked=1 AND c.DirectDelivery=0 ";
        //                            if (!bLagerKomplett)
        //                            {
        //                                strReturn += " AND c.Auftraggeber=" + myAdrID + " ";
        //                            }
        //                            strReturn += " AND c.AbBereich=" + (Int32)myAbBereichID + " " +
        //                            //" AND (c.Datum between '" + sqlDateVon.Date.ToShortDateString() + "' AND '" + sqlDateBis.Date.AddDays(1).ToShortDateString() + "') "+
        //                            " AND (CAST(c.Datum as Date) between '" + mydtpVon.Date.ToShortDateString() + "' AND '" + mydtpBis.Date.ToShortDateString() + "') " +
        //                            //    //nur ohne schaden
        //                            //" AND a.ID NOT IN (" +
        //                            //                    "SELECT sch.ArtikelID FROM SchadenZuweisung sch " +
        //                            //                                            "INNER JOIN Artikel art on sch.ArtikelID=art.ID " +
        //                            //                    ") " +
        //                           ") as [" + strCol + " Ausgang] ";
        //    return strReturn;
        //}
    }
}
