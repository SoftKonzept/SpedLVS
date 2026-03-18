using System;
using System.Collections.Generic;

namespace LVS
{
    public class set_COMTEC
    {

        /***********************************************************************************************
         *                             COMTEC 
         * ********************************************************************************************/
        ///<summary>set_SLE / InitSettings</summary>
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
            ClientToSet.UserAnzahl = 10;
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
        ///<summary>set_SLE / SetModulToClient</summary>
        ///<remarks>SZG
        ///         Datum:
        ///         Änderungen:</remarks>
        private void SetModulToClient(ref clsModule myModul)
        {
            //Hauptmenu
            myModul.MainMenu_Stammdaten = true;
            myModul.MainMenu_Statistik = true;
            myModul.MainMenu_Disposition = false;
            myModul.MainMenu_Fakurierung = true;
            myModul.MainMenu_Lager = true;
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
            myModul.Stammdaten_ExtraCharge = true;
            myModul.Stammdaten_KontenPlan = true;
            myModul.Stammdaten_StorelocationLable = false;

            //Archiv
            myModul.Archiv = true;

            //Lagerverwaltung
            //myModul.Lager_SearchGridInLifeTime = true;
            //myModul.Lager_Einlagerung = true;
            //myModul.Lager_Einlagerung_RetourBooking = false;
            myModul.Lager_Einlagerung_Print_DirectEingangDoc = false;
            myModul.Lager_Einlagerung_LagerOrt_manuell_Changeable = true;
            myModul.Lager_Einlagerung_ClearLagerOrteByArtikelCopy = true;  //geänder 2015_11_06_ nach Rücksprache mit HR. Sackser
            myModul.Lager_Einlagerung_LagerOrt_Enabled_Werk = false;
            myModul.Lager_Einlagerung_LagerOrt_Enabled_Halle = false;
            myModul.Lager_Einlagerung_LagerOrt_Enabled_Ebene = false;
            myModul.Lager_Einlagerung_LagerOrt_Enabled_Platz = false;
            myModul.Lager_Einlagerung_EditAfterClose = true;
            myModul.Lager_Einlagerung_EditADRAfterClose = true;
            myModul.Lager_Einlagerung_EditExTransportRef = false;
            myModul.Lager_Einlagerung_ArtikelIDRef_Create = false;
            myModul.Lager_Einlagerung_CheckAllArtikel = true;
            myModul.Lager_Einlagerung_BruttoEqualsNetto = true;
            myModul.Lager_Einglagerung_ArtikelIDRef_CreateProzedure = clsModule.const_Lager_Einlagerung_ArtikelIDRef_SZG;
            myModul.Lager_Einlagerung_CheckComplete = true;

            myModul.Lager_UB_ArikelProduktionsnummerChange = false;   //Standard
            myModul.Lager_USEBKZ = false;

            myModul.Lager_SPL_OutFromEingang = false;
            myModul.Lager_SPL_SchadenRequire = true;
            myModul.Lager_SPL_AutoSPL = true;


            //Eingang Pfichtfelder
            myModul.Lager_Einlagerung_RequiredValue_Auftraggeber = false;
            myModul.Lager_Einlagerung_RequiredValue_LieferscheinNr = false;
            myModul.Lager_Einlagerung_RequiredValue_Halle = false;
            myModul.Lager_Einlagerung_RequiredValue_Reihe = false;
            myModul.Lager_Einlagerung_Artikel_RequiredValue_Produktionsnummer = true;  // 2015_11_26 lt Herr Sackser rein
            myModul.Lager_Einlagerung_GArt_InfoMessageAllData = true;

            //myModul.Lager_Einlagerung_Reihenvorschlag = true;

            //Lager / Module
            myModul.Lager_Umbuchung = true;      //Standard
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
            //SPL - Sperrlager
            myModul.Lager_SPL_OutFromEingang = true;  //können direkt aus Eingang wieder ausgebucht werden
            myModul.Lager_SPL_SchadenRequire = false;
            myModul.Lager_SPL_AutoSPL = false;
            myModul.Lager_SPL_PrintSPLDocument = false;
            myModul.Lager_SPL_AutoPrintSPLDocument = false;
            myModul.Lager_SPL_RebookInAltEingang = true;


            myModul.Lager_PostCenter = true;
            myModul.Lager_Arbeitsliste = true;
            myModul.Lager_DirectDelivery = true;
            myModul.Lager_DirectDeliveryTransformation = true;

            //ASN Verkehr
            myModul.ASNTransfer = true;
            myModul.ASN_Create_Man = true;
            myModul.ASN_Create_Auto = false;
            myModul.ASN_VDA4905LiefereinteilungenAktiv = false;
            myModul.ASN_VDA4913_LVS_ReadASN_TakeOverGArtValues = true;
            myModul.ASN_UserOldASNFileCreation = true;
            myModul.ASN_UseNewASNCreateFunction = true;

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
            myModul.Mail_UsingMainMailForMailing = false;
            myModul.Mail_UsingNoReplyDefault = false;
            //myModul.Mail_SMTPServer = "smtp.1und1.de";
            //myModul.Mail_SMTPUser = "lvsreport@comtec-noeker.de";
            //myModul.Mail_SMTPPasswort = "CTlvs2014";
            //myModul.Mail_MailAdress = "lvsreport@comtec-noeker.de";
            //myModul.Mail_SMTPPort = 587;
            //myModul.Mail_SMTPSSL = true;

            //Communicator
            //Direkte Verarbeitung der XML Dateien
            //myModul.Xml_Uniport_CreateDirect_LEingang = false;
            //myModul.Xml_Uniport_CreateDirect_LAusgang = false;
            myModul.VDA_Use_KFZ = true;

        }
        ///<summary>set_SLE / SetViewsToClient</summary>
        ///<remarks>Test für Schulung</remarks>
        private void SetViewsToClient(ref clsClient myClient)
        {
            List<string> Artikel;

            /*************************************  BESTAND *******************************/
            Artikel = new List<string>();
            //Artikel.Add("ArtikelID");
            Artikel.Add("LVSNr");
            Artikel.Add("Eingang");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Werksnummer");
            Artikel.Add("Charge");
            Artikel.Add("Brutto");
            Artikel.Add("Bestellnummer");
            Artikel.Add("exMaterialnummer");
            Artikel.Add("exBezeichnung");
            Artikel.Add("Auftraggeber");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Laenge");
            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, "BestandExcel", Artikel, false);
            //AddToView(clsClient.const_ViewKategorie_Bestand, "BestandExcel", Artikel, true);

            //--------------------------------------------- BestandLager
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
            Artikel.Add("Eingangsdatum");
            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, "BestandLager/Büro", Artikel, false);
            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, "BestandLager/Büro", Artikel, true);

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
            Artikel.Add("Ausgang");
            Artikel.Add("Schaden");

            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, "InventurLager", Artikel, false);
            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, "InventurLager", Artikel, true);

            //--------------------------------------------- ARbeitsliste
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
            myClient.AddToView(clsClient.const_ViewKategorie_Arbeitsliste, "Arbeitsliste", Artikel, false);
            myClient.AddToView(clsClient.const_ViewKategorie_Arbeitsliste, "Arbeitsliste", Artikel, true);


            //--------------------------------------------- Liste Comtec
            Artikel = new List<string>();
            Artikel.Add("Auftraggeber");
            Artikel.Add("LVSNr");
            //Artikel.Add("Reihe");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Werksnummer");
            Artikel.Add("Brutto");
            Artikel.Add("Eingang");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Ausgang");
            Artikel.Add("Ausgangsdatum");
            Artikel.Add("Lagerdauer");
            Artikel.Add("Gut");
            Artikel.Add("GArtId");
            //Artikel.Add("Breite");
            //Artikel.Add("Laenge");

            //Artikel.Add("Netto");
            //Artikel.Add("iO");
            //Artikel.Add("neueReihe");
            //Artikel.Add("Schaden");

            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, "COMTEC", Artikel, false);
            myClient.AddToView(clsClient.const_ViewKategorie_Bestand, "COMTEC", Artikel, true);



            /*************************************  Journal *******************************/
            Artikel = new List<string>();
            //Artikel.Add("ArtikelID");
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
            myClient.AddToView(clsClient.const_ViewKategorie_Journal, "JournalBüro", Artikel, false);
            myClient.AddToView(clsClient.const_ViewKategorie_Journal, "JournalBüro", Artikel, true);

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
            Artikel.Add("Eingang");
            Artikel.Add("Eingangsdatum");
            Artikel.Add("Produktionsnummer");
            Artikel.Add("Werksnummer");
            Artikel.Add("Charge");
            Artikel.Add("Auftraggeber");
            Artikel.Add("Dicke");
            Artikel.Add("Breite");
            Artikel.Add("Brutto");
            Artikel.Add("Ausgang");
            Artikel.Add("Ausgangsdatum");
            Artikel.Add("Lagerort");

            myClient.AddToView(clsClient.const_ViewKategorie_LEingang, "TestAnsicht", Artikel, false);

            /*************************************  Lager Ausgang   *******************************/
            Artikel = new List<string>();
            Artikel.Add("Selected");
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
            myClient.AddToView(clsClient.const_ViewKategorie_LAusgang, "GMH-View", Artikel, false);

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
            myClient.AddToView(clsClient.const_ViewKategorie_LAusgangArtikel, "GMH-View", Artikel, false);

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

        //********************************************************************************************************  ctrEinlagerung
        ///<summary>set_SLE / ctrEinlagerung_CustomizedSettbInfoInternEnabled</summary>
        ///<remarks>InfoIntern soll immer änderbar sein</remarks>
        public void ctrEinlagerung_CustomizedSetLabel_lLaenge_Text(ref System.Windows.Forms.Label myLabel)
        {
            myLabel.Text = "Länge[m]:";
        }
        ///<summary>clsClient / ctrEinlagerung_CustomizedSetLabel_tsbtnChangeEinlagerung</summary>
        ///<remarks>Label Werksnummer ändern</remarks>
        public void ctrEinlagerung_CustomizedSetLabel_tsbtnChangeEinlagerung(ref System.Windows.Forms.ToolStripButton myTsbtn)
        {
            myTsbtn.Visible = true;
        }

        //*************************************************************************************************************  ctrRGManuell
        ///<summary>set_SLE / ctrRGManuell_CustomizeNudMengeMaxValue</summary>
        ///<remarks></remarks>
        public void ctrRGManuell_CustomizeNudMengeMaxValue(ref System.Windows.Forms.NumericUpDown myNud)
        {
            myNud.Maximum = 1000000;
        }
        //**************************************************************************************************************** ctrStatistik

        ///<summary>set_SLE / ctrStatistik_CustomizeDGVMonatsuebersicht_Auslagerung</summary>
        ///<remarks></remarks>
        public string ctrStatistik_CustomizeDGVMonatsuebersicht_Auslagerung(string strCol, decimal myAbBereichID, DateTime mydtpVon, DateTime mydtpBis, decimal myAdrID, bool bLagerKomplett)
        {
            string strReturn = string.Empty;
            strReturn += ", (select SUM(a." + strCol + ")/1000 From Artikel a " +
                                  "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                                  "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                                  "INNER JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                                  "WHERE " +
                                  "c.Checked=1 AND c.DirectDelivery=0 ";
            if (!bLagerKomplett)
            {
                strReturn += " AND c.Auftraggeber=" + myAdrID + " ";
            }
            strReturn += " AND c.AbBereich=" + (Int32)myAbBereichID + " " +
            //" AND (c.Datum between '" + sqlDateVon.Date.ToShortDateString() + "' AND '" + sqlDateBis.Date.AddDays(1).ToShortDateString() + "') "+
            " AND (CAST(c.Datum as Date) between '" + mydtpVon.Date.ToShortDateString() + "' AND '" + mydtpBis.Date.ToShortDateString() + "') " +
           //    //nur ohne schaden
           //" AND a.ID NOT IN (" +
           //                    "SELECT sch.ArtikelID FROM SchadenZuweisung sch " +
           //                                            "INNER JOIN Artikel art on sch.ArtikelID=art.ID " +
           //                    ") " +
           ") as [" + strCol + " Ausgang] ";
            return strReturn;
        }
    }
}
