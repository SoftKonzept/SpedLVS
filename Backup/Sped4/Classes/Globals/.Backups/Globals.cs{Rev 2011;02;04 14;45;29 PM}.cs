using System;
using System.Data.SqlClient;
using Sped4.Classes;
using System.Windows.Forms;
using System.Drawing;
using Sped4;


public static class Globals
{


    // Globale Connection
    public static clsSQLcon SQLcon = new clsSQLcon();

    // Config INI 
    public static clsINI.clsINI INI = new clsINI.clsINI(Application.StartupPath + "\\Config.ini");

    public static Form MDIParent;

    public static Color DefaultBackColor = Color.FromArgb(255, 255, 255);
    public static Color DefaultBaseColor = Color.FromArgb(4, 72, 117);
    public static Color DefaultBaseColor2 = Color.FromArgb(255, 128, 0);
    public static Color DefaultEffectColor = Color.FromArgb(191, 219, 255);
    public static Color DefaultEffectColor2 = Color.FromArgb(255, 177, 99);

    // für DragDrop, speichert dir ID der Auftragsposition, mit dem Rowindex kann über ein Event der Sender die 
    // Row rausschmeißen wenn okay bzw. Menge verringern
    public struct strAuftPosRow
    {
        //internal int PosID;     // Auftragnummer
        internal Int32 AuftragID;
        internal Int32 AuftragPos; // Pos des Auftrages
        internal Int32 B_ID;
        internal Int32 E_ID;
        internal decimal Menge;
        internal int RowIndex;
        internal ctrAufträge Receiverctr;
        internal Int32 KommissionsID;
        internal bool disponierbar;
        
    }

    //Recources für Fahrzeuge (z.B. Fahrer, Auflieger etc. kommt aus Tabelle VehicleRecources
    public struct _Recources
    {
        internal int RecourceID;
        internal String RecourceTyp;    
        internal DateTime TimeFrom;
        internal DateTime TimeTo;
        internal Int32 VehicleID;
        internal Int32 PersonalID;
        internal Int32 Status;
        internal string Name;
        internal string KFZ;
        internal Point oldPosition;
        internal Int32 oldVehicleIDZM;
        internal DateTime oldTimeFrom;
        internal DateTime oldTimeTo;
    }
   // -------------- Global User ----------------------------
    public struct _GL_USER
    {
      internal Int32 User_ID;
      internal string Name;
      internal string initialen;
      internal string LoginName;
      internal string Vorname;
      internal string Telefon;
      internal string Fax;
      internal string Mail;

      //Berechtigung
      internal bool ADR_anlegen;
      internal bool ADR_loeschen;
      internal bool ADR_aendern;
      internal bool KD_anlegen;
      internal bool KD_loeschen;
      internal bool KD_aendern;
      internal bool Personal_anlegen;
      internal bool Personal_aendern;
      internal bool Personal_loeschen;
      internal bool FZ_anlegen;
      internal bool FZ_loeschen;
      internal bool FZ_aendern;
      internal bool GA_anlegen;
      internal bool GA_loeschen;
      internal bool GA_aendern;
      internal bool Relation_anlegen;
      internal bool Relation_loeschen;
      internal bool Relation_aendern;
      internal bool Auftrag_anlegen;
      internal bool Auftrag_loeschen;
      internal bool Auftrag_aendern;
      internal bool Auftrag_teilen;
      internal bool FrachtvergabeSU;
      internal bool Auftrag_StornoSU;
      internal bool Disposition;
      internal bool Fakt_Liste;
      internal bool Fakt_StatusAendern;
      internal bool Fakt_Frachten;
      internal bool Fakt_drucken;
      internal bool Fakt_RGloeschen;
      internal bool Lager_BestandAnlegen;
      internal bool Lager_BestandLoeschen;
      internal bool Lager_BestandAendern;
      internal bool User_anlegen;
      internal bool User_loeschen;
      internal bool User_aendern;
    }

  /****
    public struct DispoCheck
    {
      internal Int32 ZM_ID;   //Zugmaschine
      internal Int32 A_ID;    //Auflieger
      internal Int32 P_ID;    //Personal
      internal decimal maxGewicht;//maxLadungsgewicht=maxGesamtgewicht-Leergewicht ZM - Leergewicht Auflieger
      internal decimal TourGewicht;
      internal decimal LGZM;      //Leergewicht ZM
      internal decimal LGA;       // Leergewicht Auftlieger
      internal decimal zlGG;      // max Gesamtgewicht
      internal bool disponieren;
      internal bool GewichtZuHoch;
      internal bool GewichtFreigabe;
      internal decimal AuftragPosGewicht;
      internal DateTime oldBeladezeit;
      internal DateTime oldEntladezeit;
      internal Int32 oldZM;
      internal Int32 KommiAnzahlTour;
      internal bool init;
    }
***/
    //Dokumente
    public struct _Documents
    {
        internal Int32 AuftragID;
        internal Int32 AuftragPos;
        internal string DocuArt;
        internal Int32 PicNumber;                //???
        internal Int32 AuftragScanID;
        internal Int32 OrderPosRecID;
        internal Int32 x_Pos;
        internal Int32 y_Pos;
        internal bool init;
    }
    //
    //
    //
    public enum enumAnrede
    {
      Herr = 1,
      Frau = 2,
      Fräulein = 3,
    }
    //
    //
    //
    public enum enumLagerSuchFilter
    { 
      LVS_ID=1,
      Eingang=2,
      Ausgang=3,
      Dicke=4,
      Breite=5,
      Laenge=6,
      Hoehe=7,
      Nettogewicht=8,
      Bruttogewicht=9,
      Gueterart =10,
      Einheit=11,
      Charge=12,
      Werksnummer=13,
      Produktionsnummer=14,
      exBezeichnung=15,
      exMaterialnummer=16,
      Bestellnummer=17,
      Werk=18,
      Halle=19,
      Reihe=20,
      Platz=21,
    }

    //
    //---------- Abrechnungsvariante ------
    //
    public enum enumAbrechnungsart
    {
        AuftragsPositionPauschal = 1,
        AuftragsPositionTarif = 2,
        ArtikelPauschal = 3,
        ArtikelTarif=4,
        GutschriftAuftragPosPauschal=5,
        GutschriftanSU=6,
        GutschriftFrachtvorlage=7,
    }
    //
    //---- Dokumentenart ----------
    //
    public enum enumDokumentenart
    {
      Briefkopf = 1,
      FrachtauftragAnSU = 2,
      Lieferschein = 3,
      AuftragScan=4,  //Imagedruck
      Abholschein=5,
      Rechnung=6,
      manuelleRGGS=7,
      Anmeldeschein=8,
    }
    public enum enumAbteilung
    {
      Fahrpersonal = 1,
      Lager = 2,
      Büro = 3,
      Buchhaltung = 4,
    }
  //
  //
  //
    public enum enumEinheit
    {
      Stück = 1,
      EWP = 2,
      Europaletten = 3,
      ldm = 4,
      Bund=5,
      Partie=6,
      Pakete=7,
      Ladung=8,
    }
  //
  //
  //
    public enum enumBeruf
    {
      Fahrer = 1,
      Disponent = 2,
      Sachbearbeiter = 3,
    }
    //
    // ----- Land für ADR-----
    //
    public enum enumLand
    {
        Belgien=2,
        Bulgarien=3,
        Dänemark=4,
        Deutschland = 1,
        Estland=5,
        Finnland=6,
        Frankreich=7,
        Griechenland=8,
        Großbritanien=9,
        Irland=10,
        Italien=11,
        Lettland=12,
        Litauen=13,
        Luxemburg=14,
        Niederlande=15,
        Österreich=16,
        Polen=17,
        Portugal=18,
        Rumänien=19,
        Schweden=20,
        Slowakei=21,
        Slowenien=22,
        Spanien=23,
        Tschechische_Republik=24,
        Türkei=25,
        Ungarn=26,
    }
    //
    //------ Firmenbezeichnung / Anrede  -----
    //
    public enum enumFBez
    {
        Firma,
        Spedition,
        Transportunternehmen,
    }
    //
    //
    //
    public enum enumFibuExportListe
    { 
        RG=1,
        GS=2,
        GSSU=3,
        FVGS=4,
        All=5,
    }

    //-- kann wohl weg, da Aufträge mit unbekannten Ladestellen nicht disponiert werden
    public enum enumLadestelle
    {
        UnKonwn = 0,
        Beladestelle = 1,
        Entladestelle = 2,
    }

    public enum enumDatabaseTable
    {
        Auftrag = 1,
        AuftragPos = 2,
        Fahrzeuge = 3,
        Gut = 4,
        Kommission = 5,
        Kontakt = 6,
    }
    //
    //
    //
    public enum enumImageArt
    { 
      Auftrag =1,
      Versandlieferschein=2,
      Empfangslieferschein=3,      
      Frachtbrief=4,
      Lademittelschein=5,
      Subunternehmerauftrag=6,
    }
    //
    //
    public enum enumMissedAngaben
    {
      Liefertermin,
      VSB,
      Zeitfenster,
      Ladenummer,
      Adressberechtigung
    }
    //Abgasnorm Fahrzeuge
    public enum enumAbgasnorm
    {
      Euro3,
      Euro4,
      Euro5
    }
    //
    //
    public enum enumLogbuchAktion
    { 
      Eintrag,
      Loeschung,
      Aenderung,
      Login,
      LoginFehlversuch,
      Logout,
      Exception,
      Auftragsplitting,
      VergabeSU,
      Dispo,
      Druck
    }

}

