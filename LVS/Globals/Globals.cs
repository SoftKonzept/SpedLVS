using System;
using System.Collections.Generic;
using System.Reflection;
//using System.Windows.Forms;


namespace LVS
{
    public static class Globals
    {
        ///<summary>Globals / clsSQL</summary>
        ///<remarks></remarks>
        public static clsSQLcon SQLcon = new clsSQLcon();
        public static clsSQLCOM SQLconCom = new clsSQLCOM();
        public static clsSQLCall SQLconCall = new clsSQLCall();
        public static clsSQLARCHIVE SQLconArchive = new clsSQLARCHIVE();
        public static clsSQLconImport SQLconImp = new clsSQLconImport();


        public static DateTime DefaultDateTimeMinValue = Convert.ToDateTime("01.01.1900 00:00:00");
        public static DateTime DefaultDateTimeMaxValue = Convert.ToDateTime("31.12.5000 23:59:59");
        public static String DefaultDecimalFormat = "N2";


        // TEMP 

        public static DateTime t1;
        public static DateTime t2;

        public static void time()
        {
            Console.WriteLine((Globals.t2 - Globals.t1).ToString());
        }


        public struct _GL_SYSTEM
        {
            public string sys_VersionSystemBuilt;
            public string sys_VersionApp;
            public decimal sys_VersionAppDecimal;

            public string sys_VersionARCHIV;
            public decimal sys_VersionARCHIVDecimal;

            public Int32 sys_UserQuantity;
            public decimal sys_ArbeitsbereichID;
            public string sys_Arbeitsbereichsname;
            public bool sys_Arbeitsbereich_ASNTransfer;
            public string sys_Sprache;
            public string sys_WorkingPathExport;
            public decimal sys_MandantenID;

            //Client
            public string client_MatchCode;
            public string client_CompanyName;
            public string client_Strasse;
            public string client_PLZOrt;
            public decimal client_AdrID;

            //expl. System - Einstellungen
            public string Client;
            public string con_Server;
            public string con_Database;
            public string con_UserDB;
            public string con_PassDB;

            //expl. System - Einstellungen COM
            public string con_Server_COM;
            public string con_Database_COM;
            public string con_UserDB_COM;
            public string con_PassDB_COM;

            //expl. System - Einstellungen CALL
            public string con_Server_CALL;
            public string con_Database_CALL;
            public string con_UserDB_CALL;
            public string con_PassDB_CALL;

            //expl. System - Einstellungen ARCHIVE
            public string con_Server_ARCHIVE;
            public string con_Database_ARCHIVE;
            public string con_UserDB_ARCHIVE;
            public string con_PassDB_ARCHIVE;

            //expl. System - Einstellungen Import
            public string con_Server_Imp;
            public string con_Database_Imp;
            public string con_UserDB_Imp;
            public string con_PassDB_Imp;

            //Dokumentenpfade
            public string docPath_LabelAll;
            public string docPath_LabelOne;
            public string docPath_LOLabelLinks;
            public string docPath_LOLabelRechts;
            public string docPath_LOLabelBeide;
            public string docPath_SchadenLabel;
            public string docPath_SchadenDoc;
            public string docPath_SPLLabel;
            public string docPath_SPLDoc;
            public string docPath_LabelOneNeutral;
            public string docPath_EingangLfs;
            public string docPath_EingangAnzeige;
            public string docPath_EingangAnzeigePerDay;
            public string docPath_EingangAnzeigePerDayFull;
            public string docPath_EingangDoc;
            public string docPath_EingangList;
            public string docPath_AusgangLfs;
            public string docPath_AusgangAnzeige;
            public string docPath_AusgangAnzeigePerDay;
            public string docPath_AusgangAnzeigePerDayFull;
            public string docPath_AusgangDoc;
            public string docPath_AusgangList;
            public string docPath_AusgangStornoDoc;
            public string docPath_AusgangNeutralDoc;
            public string docPath_Lagerrechnung;
            public string docPath_Manuellerechnung;
            public string docPath_RGAnhang;
            public string docPath_RGBuch;
            public string docPath_EingangAnzeigeMail;
            public string docPath_EingangLfsMail;
            public string docPath_EingangAnzeigePerDayMail;
            public string docPath_AusgangLfsMail;
            public string docPath_AusgangAnzeigeMail;
            public string docPath_AusgangAnzeigePerDayMail;
            public string docPath_LagerrechnungMail;
            public string docPath_ManuellerechnungMail;
            public string docPath_Bestand;
            public string docPath_Journal;
            public string docPath_Inventur;
            public string docPath_Adressliste;
            public string docPath_ManuelleGutschrift;
            public string docPath_ArtikelListe;
            public string docPath_KundenInformationen;
            public string docPath_TarifeKunden;
            public string docPath_ViewPrintBestand;
            public string docPath_ViewPrintJournal;

            public List<string> docPath;


            //PapierQuellenDruck
            public string docPath_LabelAll_PaperSource;
            public string docPath_LabelOne_PaperSource;
            public string docPath_SPLLabel_PaperSource;
            public string docPath_SPLDoc_PaperSource;
            public string docPath_EingangLfs_PaperSource;
            public string docPath_EingangAnzeige_PaperSource;
            public string docPath_EingangList_PaperSource;
            public string docPath_EingangAnzeigePerDay_PaperSource;
            public string docPath_EingangDoc_PaperSource;
            public string docPath_AusgangList_PaperSource;
            public string docPath_AusgangLfs_PaperSource;
            public string docPath_AusgangAnzeige_PaperSource;
            public string docPath_AusgangAnzeigePerDay_PaperSource;
            public string docPath_AusgangDoc_PaperSource;
            public string docPath_AusgangNeutralDoc_PaperSource;
            public string docPath_Lagerrechnung_PaperSource;
            public string docPath_Manuellerechnung_PaperSource;
            public string docPath_RGAnhang_PaperSource;
            public string docPath_RGBuch_PaperSource;
            public string docPath_ManuelleGutschrift_PaperSource;
            public string docPath_EingangDocMat;
            public string docPath_RGAnhangMat;
            public string docPath_AusgangLfsMat;


            public string docPath_LabelAll_Printer;
            public string docPath_LabelOne_Printer;
            public string docPath_SPLLabel_Printer;
            public string docPath_SPLDoc_Printer;
            public string docPath_EingangLfs_Printer;
            public string docPath_EingangAnzeige_Printer;
            public string docPath_EingangAnzeigePerDay_Printer;
            public string docPath_EingangDoc_Printer;
            public string docPath_EingangList_Printer;
            public string docPath_AusgangLfs_Printer;
            public string docPath_AusgangAnzeige_Printer;
            public string docPath_AusgangAnzeigePerDay_Printer;
            public string docPath_AusgangDoc_Printer;
            public string docPath_AusgangList_Printer;
            public string docPath_AusgangStornoDoc_Printer;
            public string docPath_AusgangNeutralDoc_Printer;
            public string docPath_Lagerrechnung_Printer;
            public string docPath_Manuellerechnung_Printer;
            public string docPath_RGAnhang_Printer;
            public string docPath_RGBuch_Printer;
            public string docPath_EingangAnzeigeMail_Printer;
            public string docPath_EingangLfsMail_Printer;
            public string docPath_EingangAnzeigePerDayMail_Printer;
            public string docPath_AusgangLfsMail_Printer;
            public string docPath_AusgangAnzeigeMail_Printer;
            public string docPath_AusgangAnzeigePerDayMail_Printer;
            public string docPath_LagerrechnungMail_Printer;
            public string docPath_ManuellerechnungMail_Printer;
            public string docPath_Bestand_Printer;
            public string docPath_Journal_Printer;
            public string docPath_Inventur_Printer;
            public string docPath_Adressliste_Printer;
            public string docPath_ManuelleGutschrift_Printer;

            //Dokumentenpfade
            public Int32 docPath_LabelAll_Count;
            public Int32 docPath_LabelOne_Count;
            public Int32 docPath_SPLLabel_Count;
            public Int32 docPath_SPLDoc_Count;
            public Int32 docPath_EingangLfs_Count;
            public Int32 docPath_EingangAnzeige_Count;
            public Int32 docPath_EingangAnzeigePerDay_Count;
            public Int32 docPath_EingangDoc_Count;
            public Int32 docPath_EingangList_Count;
            public Int32 docPath_AusgangLfs_Count;
            public Int32 docPath_AusgangAnzeige_Count;
            public Int32 docPath_AusgangAnzeigePerDay_Count;
            public Int32 docPath_AusgangDoc_Count;
            public Int32 docPath_AusgangList_Count;
            public Int32 docPath_AusgangNeutralDoc_Count;
            public Int32 docPath_Lagerrechnung_Count;
            public Int32 docPath_Manuellerechnung_Count;
            public Int32 docPath_RGAnhang_Count;
            public Int32 docPath_RGBuch_Count;
            public Int32 docPath_EingangAnzeigeMail_Count;
            public Int32 docPath_EingangLfsMail_Count;
            public Int32 docPath_Bestand_Count;
            public Int32 docPath_Journal_Count;
            public Int32 docPath_Inventur_Count;
            public Int32 docPath_Adressliste_Count;
            public Int32 docPath_ManuelleGutschrift_Count;

            // DOkumente Communicator
            public string Doc_DocJournalAutoMail;
            public string Doc_DocBestandAutoMail;

            //Voreinstellungen
            public decimal VE_Fakturierung_MwStSatz;
            public string VE_LagerOrt_AuswahlLagerOrtPlatz;
            public bool VE_Lager_ASNVerkehr;
            public bool VE_Fakturierung_InvoiceDateInPast;
            public string VE_DocScanPath;


            //Module
            //Stammdaten
            public bool Modul_Stammdaten_Adressen;
            public bool Modul_Stammdaten_Personal;
            public bool Modul_Stammdaten_Fahrzeuge;
            public bool Modul_Stammdaten_Gut;
            public bool Modul_Stammdaten_Relation;
            public bool Modul_Stammdaten_Lagerortverwaltung;
            public bool Modul_Stammdaten_Schaeden;
            public bool Modul_Stammdaten_Einheiten;

            //Spedition
            public bool Modul_Spedition;
            public bool Modul_Spedition_Dispo;
            public bool Modul_Spedition_Auftragserfassung;


            //Lagerverwaltung
            public bool Modul_Lager_Einlagerung;
            public bool Modul_Lager_Auslagerung;
            public bool Modul_Lager_Umbuchung;
            public bool Modul_Lager_Journal;
            public bool Modul_Lager_Bestandsliste;
            public bool Modul_Lager_Sperrlager;

            public bool Modul_LvsScan;
            public bool Modul_LvsScan_Inventory_List;
            public bool Modul_LvsScan_Inventory_Scan;

            //Fakturierung
            public bool Modul_Fakt_Lager;
            public bool Modul_Fakt_Spedition;
            public bool Modul_Fakt_Manuell;
            public bool Modul_Fakt_Sonderkosten;
            public bool Modul_Fakt_UB_DifferentCalcAssignment;
            public bool Modul_Fakt_Rechnungsbuch;
            public bool Modul_Fakt_LagerManuellSelection;


            //Reports
            public Dictionary<string, string> DictReportEinlagerung;
            public Dictionary<string, string> DictReportAuslagerung;

            // Views
            public Dictionary<string, Dictionary<string, List<string>>> DictPrintViews;
            public Dictionary<string, Dictionary<string, List<string>>> DictViews;
            public Dictionary<string, string> DictOrders;
            // Kundenliste für alternatives Dokumentlayout
            public string AuftraggeberMat;
            public string AuftraggeberAnhangMat;
            public string LieferscheinMat;

            public string con_FtpUser;// = this.con_FtpUser;
            public string con_FtpPass;// = this.con_FtpPass;
            public string con_FtpServer;// = this.con_FtpServer;

            //Voreinstellungen 
            public string VE_OdettePath;// = this.VE_OdettePath;
            public int VE_MainThreadDuration;// = this.VE_MainThreadDuration;
            //Module
            public bool Modul_Xml_Uniport_CreateDirect_LAusgang;// = this.Client.Modul.Xml_Uniport_CreateDirect_LAusgang;
            public bool Modul_Xml_Uniport_CreateDirect_LEingang;// = this.Client.Modul.Xml_Uniport_CreateDirect_LEingang;
            public bool Modul_VDA_Use_KFZ;// = this.Client.Modul.VDA_Use_KFZ;



        }
        ///<summary>Globals / struct _GL_USER</summary>
        ///<remarks></remarks>
        public struct _GL_USER
        {
            public decimal User_ID;
            public string Name;
            public string initialen;
            public string LoginName;
            public string Vorname;
            public string Telefon;
            public string Fax;
            public string Mail;
            public string SMTPUser;
            public string SMTPPass;
            public string SMTPServer;
            public Int32 SMTPPort;
            public bool IsAdmin;

            //expl. User - Einstellungen
            public DateTime us_dtDispoVon;
            public DateTime us_dtDispoBis;
            public decimal us_decFontSize;

            //Berechtigung Menü
            public bool Menue_Fakturierung;
            public bool Menu_Stammdaten;
            public bool Menu_Auftragserfassung;
            public bool Menu_Lager;
            public bool Menu_Statistik;
            public bool Menu_Disposition;
            public bool Menu_System;

            public bool read_ADR;
            public bool write_ADR;
            public bool read_Kunde;
            public bool write_Kunde;
            public bool read_Personal;
            public bool write_Personal;
            public bool read_KFZ;
            public bool write_KFZ;
            public bool read_Gut;
            public bool write_Gut;
            public bool read_Relation;
            public bool write_Relation;
            public bool read_Order;
            public bool write_Order;
            public bool write_TransportOrder;
            public bool read_TransportOrder;
            public bool write_Disposition;
            public bool read_Disposition;
            public bool read_FaktLager;
            public bool write_FaktLager;
            public bool read_FaktSpedition;
            public bool write_FaktSpedition;
            public bool read_FaktExtraCharge;
            public bool write_FaktExtraCharge;
            public bool read_Bestand;
            public bool read_Inventory;
            public bool write_Inventory;
            public bool read_LagerEingang;
            public bool write_LagerEingang;
            public bool read_LagerAusgang;
            public bool write_LagerAusgang;
            public bool read_User;
            public bool write_User;
            public bool read_Arbeitsbereich;
            public bool write_Arbeitsbereich;
            public bool read_Mandant;
            public bool write_Mandant;
            public bool read_Statistik;
            public bool read_Einheit;
            public bool write_Einheit;
            public bool read_Schaden;
            public bool write_Schaden;
            public bool read_LagerOrt;
            public bool write_LagerOrt;
            public bool read_ASNTransfer;
            public bool write_ASNTransfer;
            public bool write_FibuExport;
            public bool access_StKV;

            public bool access_App;
            public bool access_AppStoreIn;
            public bool access_AppStoreOut;
            public bool access_AppInventory;

        }
        public static void GLUserSetDefault(ref Globals._GL_USER myGLUser)
        {
            decimal decDefault = 0M;
            int iDefault = 0;
            string strDefault = string.Empty;

            FieldInfo[] fi = myGLUser.GetType().GetFields(); // typeof(Globals._GL_USER).GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo info in fi)
            {
                switch (info.FieldType.FullName)
                {
                    case "System.Decimal":
                        info.SetValue(myGLUser, decDefault);
                        break;
                    case "System.Integer":
                        info.SetValue(myGLUser, iDefault);
                        break;
                    case "System.String":
                        info.SetValue(myGLUser, strDefault);
                        break;
                }
            }
        }

        public static void GLSystemSetDefault(ref Globals._GL_SYSTEM myGLSystem)
        {
            decimal decDefault = 0M;
            int iDefault = 0;
            string strDefault = string.Empty;

            FieldInfo[] fi = myGLSystem.GetType().GetFields(); // typeof(Globals._GL_USER).GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo info in fi)
            {
                if (info.Name.Equals("docPath_EingangAnzeige"))
                {
                    string str = string.Empty;
                }

                switch (info.FieldType.FullName)
                {
                    case "System.Decimal":
                        info.SetValue(myGLSystem, decDefault);
                        break;
                    case "System.Integer":
                        info.SetValue(myGLSystem, iDefault);
                        break;
                    case "System.String":
                        info.SetValue(myGLSystem, strDefault);
                        break;
                }
            }
        }


    }

}




