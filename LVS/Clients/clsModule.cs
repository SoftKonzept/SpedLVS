using System;

namespace LVS
{
    public class clsModule
    {
        public const string const_Lager_Einlagerung_ArtikelIDRef_Heisiep = "Heisiep";
        public const string const_Lager_Einlagerung_ArtikelIDRef_Hons = "Hons";
        public const string const_Lager_Einlagerung_ArtikelIDRef_SZG = "SZG";
        public const string const_Lager_Einlagerung_ArtikelIDRef_SLE = "SLE";

        /****************************************************************************************
         * Hier werden alle Module (Menü/Untermenu) aufgeführt, damit hinterher variable die 
         * einzelnen Module für den Client freigegeben werden können. 
         * 
         * Diese Module müssen auch in Globals.System nachgetragen werden und deren Zuweisung 
         * in clsSystem - InitSystem()
         * **************************************************************************************/
        public bool ASNTransfer { get; set; }
        public bool ASN_Create_Man { get; set; }
        public bool ASN_Create_Auto { get; set; }
        public bool ASN_VDA4905LiefereinteilungenAktiv { get; set; }
        public bool ASN_VDA4905LiefereinteilungenAktiv_SupplierDetails { get; set; }
        public bool ASN_VDA4913_LVS_ReadASN_TakeOverGArtValues { get; set; }   //Übernimmt die Werte aus der Güterart für Dicke, Breite usw.
        public bool ASN_UserOldASNFileCreation { get; set; }   // aktuell zur Unterscheidung des neuen Verfahrens zur Erstellung der Lagermeldungen
        public bool ASN_AutoCreateNewGArtByASN { get; set; }   // erstellt eine neue Güterart, wenn keine passende Güterart gefunden wird 
        public bool ASN_UseNewASNCreateFunction { get; set; }  // erste einmal für SIL

        public bool ASNCall_UserCallStatus { get; set; }  // Status der Artikel für LVSCall



        // Main Menü 
        public bool MainMenu_Stammdaten { get; set; }
        public bool MainMenu_Statistik { get; set; }
        public bool MainMenu_Disposition { get; set; }
        public bool MainMenu_Fakurierung { get; set; }
        public bool MainMenu_Lager { get; set; }
        public bool MainMenu_AuftragserfassungDispo { get; set; }

        //...|Stammdaten
        public bool Stammdaten_Adressen { get; set; }
        public bool Stammdaten_Personal { get; set; }
        public bool Stammdaten_Fahrzeuge { get; set; }
        public bool Stammdaten_Gut { get; set; }
        public bool Stammdaten_Gut_UseGutDefinition { get; set; }   // Güterarten=true -> Warengruppen=false
        public bool Stammdaten_Gut_UseGutDefinitionByASNTransfer { get; set; }
        public bool Stammdaten_GutShowAllwaysAll { get; set; }      // true= es werden immer alle Güterarten angezeigt / false=es werden nur Güterarten aus dem Arbeitsbereich angezeigt
        public bool Stammdaten_UseGutAdrAssignment { get; set; }    // standard false -> Zuweisung Adressen Güterart
        public bool Stammdaten_Relation { get; set; }
        public bool Stammdaten_Lagerortverwaltung { get; set; }
        public bool Stammdaten_Lagerreihenverwaltung { get; set; }
        public bool Stammdaten_Schaeden { get; set; }
        public bool Stammdaten_Einheiten { get; set; }
        public bool Stammdaten_ExtraCharge { get; set; }   //Nebenkosten
        public bool Stammdaten_KontenPlan { get; set; }
        public bool Stammdaten_StorelocationLable { get; set; }


        //....|Archiv
        public bool Archiv { get; set; }

        //Spedition
        public bool Spedition { get; set; }
        public bool Spedition_Auftragserfassung { get; set; }
        public bool Spedition_Dispo { get; set; }

        //...|Lagerverwaltung

        //Die Echtzeitsuche in den verschiedenen Grid über eine Spalte wird mithilfe diese Moduls aktiviert
        public bool Lager_SearchGridInLifeTime { get; set; }

        public bool Lager_USEBKZ { get; set; }

        // ...|Einlagerung
        public bool Lager_Einlagerung { get; set; }
        public bool Lager_Einlagerung_RetourBooking { get; set; }                   //Retouren Einbuchung
        public bool Lager_Einlagerung_Print_DirectEingangDoc { get; set; }          //direktes Drucken der Eingangsdokumente
        public bool Lager_Einlagerung_Print_DirectList { get; set; }                //direktes Drucken der EIngangsliste
        public bool Lager_Einlagerung_Print_DirectLabel { get; set; }               //direktes Drucken wenn alle Artikel im Eingang geprüft
        public bool Lager_Einlagerung_Print_DirectLabelAfterCheckEingang { get; set; } //direktes drucken wenn der Eingang abgeschlossen wird
        public bool Lager_Einlagerung_ShowDirectPrintCenter { get; set; }           // 16.09.2016 neu
        public bool Lager_Einlagerung_LagerOrt_manuell_Changeable { get; set; }
        public bool Lager_Einlagerung_ClearLagerOrteByArtikelCopy { get; set; }     //Lagerort wird nicht mit kopiert
        public bool Lager_Einlagerung_LagerOrt_Enabled_Werk { get; set; }           //Werk als Lagerort deaktivieren
        public bool Lager_Einlagerung_LagerOrt_Enabled_Halle { get; set; }          //Halle als Lagerort deaktivieren
        public bool Lager_Einlagerung_LagerOrt_Enabled_Reihe { get; set; }          //Reihe als Lagerort deaktivieren
        public bool Lager_Einlagerung_LagerOrt_Enabled_Ebene { get; set; }          //Ebene als Lagerort deaktivieren
        public bool Lager_Einlagerung_LagerOrt_Enabled_Platz { get; set; }          //Platz als Lagerot deaktivieren
        public bool Lager_Einlagerung_EditAfterClose { get; set; }
        public bool Lager_Einlagerung_EditADRAfterClose { get; set; }
        public bool Lager_Einlagerung_EditExTransportRef { get; set; }
        public bool Lager_Einlagerung_InkrementArtikelPos { get; set; }
        public bool Lager_Einlagerung_CheckComplete { get; set; }
        public bool Lager_Einlagerung_SetCheckDate { get; set; } // Datum wird auf das Aktuelle Datum gesetzt
        public bool Lager_Einlagerung_BruttoEqualsNetto { get; set; }  // Brutto wird immer gleich netto gesetzt
        public bool Lager_Eingang_FreeForChange { get; set; }
        public bool Lager_Einlagerung_Enabeled_Einheit { get; set; }


        //...|Pflichtfelder Eingang
        public bool Lager_Einlagerung_RequiredValue_Auftraggeber { get; set; }
        public bool Lager_Einlagerung_RequiredValue_LieferscheinNr { get; set; }
        public bool Lager_Einlagerung_RequiredValue_Vehicle { get; set; }
        public bool Lager_Einlagerung_RequiredValue_Halle { get; set; }
        public bool Lager_Einlagerung_RequiredValue_Reihe { get; set; }


        //...|Pflichtfelder Eingang Artikel
        public bool Lager_Einlagerung_Artikel_RequiredValue_Produktionsnummer { get; set; }
        public bool Lager_Einlagerung_Artikel_RequiredValue_Netto { get; set; }
        public bool Lager_Einlagerung_Artikel_RequiredValue_Brutto { get; set; }
        public bool Lager_Einlagerung_ArtikelIDRef_Create { get; set; }                       //ArtikelID Ref
        public string Lager_Einglagerung_ArtikelIDRef_CreateProzedure = string.Empty;
        public bool Lager_Einlagerung_GArt_InfoMessageAllData { get; set; }   //Abfrage auf übernahme aller Daten wird deaktiviert
        public bool Lager_Einlagerung_Artikel_RequiredValue_Laenge { get; set; }


        //...|Einlagerung | Artikel Button
        public bool Menu_Einlagerung_Artikel_tsbtnLagerort { get; set; }

        //...|Einlagerung | Artikelfunktion
        public bool Lager_Artikel_UseKorreturStornierVerfahren { get; set; }
        public bool Lager_Artikel_FreeForChange { get; set; }

        //...|Umbuchung
        public bool Lager_UB_ArikelProduktionsnummerChange { get; set; }

        //...|SPL Sperrlager
        public bool Lager_SPL_SchadenRequire { get; set; }     // Schaden wird benötigt, damit ein Artikel ins SPL gebucht werden kann
        public bool Lager_SPL_OutFromEingang { get; set; }     // auslagerung aus SPL über EINgangsctr (Button SPL)
        public bool Lager_SPL_AutoSPL { get; set; }            //ein Schaden, der mit entsprechend markiert ist bewirkt eine autom. UB in SPL
        public bool Lager_SPL_PrintSPLDocument { get; set; }    // Druck eines eigenen SPL Dokuments
        public bool Lager_SPL_AutoPrintSPLDocument { get; set; } // automatischer Druck des SPL Dokuments 
        public bool Lager_SPL_RebookInAltEingang { get; set; } // Artikel wird mit der selben LVSNR in den alten Eingang gebucht

        // - Auslagerung
        public bool Lager_Auslagerung { get; set; }
        public bool Lager_Auslagerung_Print_DirectAusgangDoc { get; set; }
        public bool Lager_Auslagerung_Print_DirectAusgangListe { get; set; }
        public bool Lager_Auslagerung_DGVBestand_SortID_1 { get; set; }      // Honselmann für besondere Sortierung des Grids für Kunden       
        public bool Lager_Auslagerung_EditAfterClose { get; set; }
        public bool Lager_Auslagerung_Print_AdditionalTransportDoc { get; set; }
        public bool Lager_Auslagerung_CheckComplete { get; set; }
        public bool Lager_Auslagerung_ShowDirectPrintCenter { get; set; }   // 16.09.2016 neu
        public bool Lager_Auslagerung_StoreOutDirect { get; set; }   // 14.11.2017 als Modul neu

        // - Pflichtfelder Auslagerung
        public bool Lager_Auslagerung_RequiredReceiver { get; set; }

        //...|Schaeden
        public bool Lager_Schaeden_ShowPrintCenterAfterSelection { get; set; }

        public bool Lager_Umbuchung { get; set; }
        public bool Lager_Journal { get; set; }
        public bool Lager_Bestandsliste { get; set; }
        public bool Lager_Bestandsliste_TagesbestandOhneSPL { get; set; }    // aktuelle Tagesbestand wird ohne SPL angezeigt
        public bool Lager_Bestandsliste_PrintButtonReport_Bestand { get; set; }  //Print Bestand über Report
        public bool Lager_Bestandsliste_PrintButtonReport_Inventur { get; set; } // Print Inventur über Report
        public bool Lager_Bestandsliste_PrintButtonGrid { get; set; }            // Print über GridPrintFunktion
        public bool Lager_Bestandsliste_BestandOverAllWorkspaces { get; set; }  // für SZG - Novelis erstellt


        public bool Lager_Sperrlager { get; set; }
        public bool Lager_DirectDelivery { get; set; }
        public bool Lager_DirectDeliveryTransformation { get; set; }
        public bool Lager_FreeForCall { get; set; }  //Bestandsfreigabe
        public bool Lager_PostCenter { get; set; }
        public bool Lager_Arbeitsliste { get; set; }                      // Honselmann

        //...| LvsScan Modul
        public bool LvsScan { get; set; }
        public bool LvsScan_Inventory_List { get; set; }                           // Inventurlisten für Scanner
        public bool LvsScan_Inventory_Scan { get; set; }                           // Inventurlisten für Scanner


        //...|Fakturierung
        public bool Fakt_Lager { get; set; }
        public bool Fakt_Spedition { get; set; }
        public bool Fakt_Manuell { get; set; }
        public bool Fakt_Sonderkosten { get; set; }      // 2014_03_25 ExtraModul für Honselmann entwickelt
        public bool Fakt_UB_DifferentCalcAssignment { get; set; }  // bei der UB kann zugewiesen werden, welche Kosten wie zu tragen sind
        public bool Fakt_Rechnungsbuch { get; set; }
        public bool Fakt_LagerManuellSelection { get; set; } //Standard Fakturierung bsp. Heisiep
        public bool Fakt_eInvoiceIsAvailable { get; set; } //Standard Fakturierung bsp. Heisiep



        //...|Primekeys
        public bool Fakt_GetRGGSNrFromTable_Primekey { get; set; }
        public bool Fakt_GetRGGSNrFromTable_Mandant { get; set; }
        public bool Fakt_UseOneRGNrKreisForRGandGS { get; set; }
        public bool Fakt_DeactivateMenueCtrRGList { get; set; }
        public bool PrimeyKey_LVSNRUseOneIDRange { get; set; }   // LVSNR über alle Arbeitsbereiche



        //Main Client MAil
        public bool Mail_UsingMainMailForMailing = false;
        public bool Mail_UsingNoReplyDefault = false;

        public string Mail_SMTPServer = string.Empty;
        public Int32 Mail_SMTPPort = 0;
        public string Mail_SMTPUser = string.Empty;
        public string Mail_SMTPPasswort = string.Empty;
        public string Mail_MailAdress = string.Empty;
        public bool Mail_SMTPSSL = true;

        public string Mail_Noreply_SMTPServer = string.Empty;
        public Int32 Mail_Noreply_SMTPPort = 0;
        public string Mail_Noreply_SMTPUser = string.Empty;
        public string Mail_Noreply_SMTPPasswort = string.Empty;
        public string Mail_Noreply_MailAdress = string.Empty;
        public bool Mail_Noreply_SMTPSSL = true;



        public bool EnableAdvancedSearch { get; set; }
        public bool EnableDirectSearch { get; set; }
        public bool EnableEditExAuftrag { get; set; }


        //---- readOnly Datenfelder
        public bool ReadOnly_Artikel_IsNOTStackable { get; set; }


        //Print
        public bool Print_OldVersion { get; set; }    // false =  ab 2017-01-25 Reportdaten aus DB ReportDocSetting
        public bool Print_PrintByReportDocSetting { get; set; }    // ab 2017-01-25 Reportdaten aus DB
        public bool Print_LieferscheinOhneAbschluss { get; set; }
        public bool Print_GridPrint_ViewByGridPrint_Bestandsliste { get; set; } // true = Grid False=ReportPrint
        public bool Print_GridPrint_ViewByGridPrint_Journal { get; set; }       // true = Grid False=ReportPrint
        public bool Print_Documents_UseRGAnhang { get; set; }   // Verwendung von RGAnhang 

        //Communicator 
        public bool Xml_Uniport_CreateDirect_LEingang = false;
        public bool Xml_Uniport_CreateDirect_LAusgang = false;
        public bool VDA_Use_KFZ = true;

        // Statistik 
        public bool Statistik_Lager { get; set; }
        public bool Statistik_FaktLager { get; set; }
        public bool Statistik_FaktDispo { get; set; }
        public bool Statistik_Waggonbuch { get; set; }


        public bool Excel_UseOldExport { get; set; }

        public bool Lager_Einlagerung_CheckAllArtikel { get; set; }

        public bool Fakt_SendRGAnhangMailExcel { get; set; }
        public bool Fakt_CalcSLSVMaterialkosten { get; set; }

        //public bool Lager_Einlagerung_Reihenvorschlag { get; set; }
        public string Lager_WaggonNo_Mask { get; set; }
        public bool Lager_DisplaySPLArtikelinAusgang { get; set; }

        public bool Lager_AskForDeleteEA { get; set; }

        public bool Statistik_Gesamtbestand { get; set; }
        public bool Statistik_Bestandsbewegungen { get; set; }
        public bool Statistik_durchschn_Lagerbestand { get; set; }
        public bool Statistik_druchschn_Lagerdauer { get; set; }
        public bool Statistik_Monatsuebersicht { get; set; }





        //-------------- Telerik Datagrid
        public bool Telerik_GridPrint_SummaryRow { get; set; }
    }
}
