using LVS;
using System;
using System.Collections.Generic;
using System.Data;

namespace Sped4.Classes
{
    class clsSQLStatement
    {
        public Globals._GL_USER _GL_User;
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
        //----------------------------------------------------------------------------------------------------  List Table 
        List<string> listDBTable = new List<string>()
        {
            "ADR",
            "Artikel",
            "LAusgang",
            "LEingang",
        };

        //----------------------------------------------------------------------------------------------------  ADR Adressen 
        public Dictionary<string, clsSQLTableColumn> dicADR = new Dictionary<string, clsSQLTableColumn>();
        Dictionary<string, string> dicADRColInfo = new Dictionary<string, string>();
        private void INIT_dicADRColInfo()
        {
            dicADRColInfo.Add("ID", "Datenbank ID Adressen");
            dicADRColInfo.Add("ViewID", "Matchcode");
            dicADRColInfo.Add("KD_ID", "interne Kunden-ID");
            dicADRColInfo.Add("Fbez", "Adress-/Firmenbezeichnung");
            dicADRColInfo.Add("Name1", "Adresszeile 1");
            dicADRColInfo.Add("Name2", "Adresszeile 2");
            dicADRColInfo.Add("Name3", "Adresszeile 3");
            dicADRColInfo.Add("Str", "Straße");
            dicADRColInfo.Add("HausNr", "Hausnummer");
            dicADRColInfo.Add("PLZ", "Postleitzahl");
            dicADRColInfo.Add("ORT", "Ort");
            dicADRColInfo.Add("Land", "Land / Staat");
            dicADRColInfo.Add("Wavon", "Warenannahme / Öffnungszeit von");
            dicADRColInfo.Add("Wabis", "Warenannahme / Öffnungszeit bis");
        }

        //----------------------------------------------------------------------------------------------------  Artikel   
        public Dictionary<string, clsSQLTableColumn> dicArtikel = new Dictionary<string, clsSQLTableColumn>();
        Dictionary<string, string> dicArtikelColInfo = new Dictionary<string, string>();
        private void INIT_dicArtikelColInfo()
        {
            dicArtikelColInfo.Add("ID", "Datenbank ID Artikel");
            dicArtikelColInfo.Add("AuftragID", "Auftrags-ID/Auftrags-Nr in der  Disposition");
            dicArtikelColInfo.Add("AuftragPos", "Auftragsposition in der Disposition");
            dicArtikelColInfo.Add("LVS_ID", "LVS-Nr im Lager");
            dicArtikelColInfo.Add("BKZ", "Buchungskennzeichen");
            dicArtikelColInfo.Add("gemGewicht", "gmeledetes Gewicht in der Disposition");
            dicArtikelColInfo.Add("Netto", "Nettogewicht des Artikels");
            dicArtikelColInfo.Add("Brutto", "Bruttogewicht des Artikels");
            dicArtikelColInfo.Add("GArtID", "Datenbank ID Güterart");
            dicArtikelColInfo.Add("Dicke", "Abmessung Dicke");
            dicArtikelColInfo.Add("Breite", "Abmessung Breite");
            dicArtikelColInfo.Add("Laenge", "Abmessung Länge");
            dicArtikelColInfo.Add("Anzahl", "Artikelanzahl");
            dicArtikelColInfo.Add("Einheit", "zugewiesene Einheit");
            dicArtikelColInfo.Add("Werksnummer", "Werksnummer des Artikels");
            dicArtikelColInfo.Add("Produktionsnummer", "Produktionsnummer des Artikels");
            dicArtikelColInfo.Add("exBezeichnung", "externe Bezeichung");
            dicArtikelColInfo.Add("Charge", "Charge des Artikels");
            dicArtikelColInfo.Add("Bestellnummer", "zugewiesene Bestellnummer");
            dicArtikelColInfo.Add("exMaterialnummer", "externe Materialnummer ");
            dicArtikelColInfo.Add("Position", "Position auf Lieferschein/Eingang");
            dicArtikelColInfo.Add("GutZusatz", "Zusatzinfo zur Güterart");
            dicArtikelColInfo.Add("CheckArt", "Markierung Artikel geprüft");
            dicArtikelColInfo.Add("Storno", "Markierung Aritkel storniert");
            dicArtikelColInfo.Add("StornoDate", "Datum Storno");
            dicArtikelColInfo.Add("UB", "Markierung Artikel umgebucht");
            dicArtikelColInfo.Add("AbrufRef", "Abrufreferenz des Artikels");
            //dicArtikelColInfo.Add("TARef", ""),
            dicArtikelColInfo.Add("AB_ID", "Datenbank ID Arbeitsbereich");
            dicArtikelColInfo.Add("Mandanten_ID", "Datenbank ID Mandant");
            dicArtikelColInfo.Add("LEingangTableID", "Datenbank ID Eingang");
            dicArtikelColInfo.Add("LAusgangTableID", "Datenbank ID Augang");
            dicArtikelColInfo.Add("LA_Checked", "Markierung Artikel im Ausgang geprüft");
            //dicArtikelColInfo.Add("ArtIDAlt", "")
            dicArtikelColInfo.Add("Info", "zusätzler Infotext zum Artikel");
            dicArtikelColInfo.Add("LagerOrt", "Datenbank ID Lagerort");
            dicArtikelColInfo.Add("LOTable", "Datenbankname der Lagerort ID");
            dicArtikelColInfo.Add("exLagerOrt", "manuelle Lagerortangabe/ externer Lagerort");
            //dicArtikelColInfo.Add("EAEingangAltLVS", ""),
            //dicArtikelColInfo.Add("EAAusgangAltLVS", ""),
            //dicArtikelColInfo.Add("IsLagerArtikel", ""),
            //dicArtikelColInfo.Add("LVSNr_ALTLvs", ""),
        }

        //---------------------------------------------------------------------------------------------------- LEingang    
        public Dictionary<string, clsSQLTableColumn> dicLEingang = new Dictionary<string, clsSQLTableColumn>();
        Dictionary<string, string> dicLEingangColInfo = new Dictionary<string, string>();
        private void INIT_dicLEingangColInfo()
        {
            dicLEingangColInfo.Add("ID", "Datenbank ID Eingang");
            dicLEingangColInfo.Add("LEingangID", "Eingangs - ID / Eingangs-Nr.");
            dicLEingangColInfo.Add("Date", "Lagereingangsdatum");
            dicLEingangColInfo.Add("Auftraggeber", "Adresse ID Auftraggeber im Eingang");
            dicLEingangColInfo.Add("Empfaenger", "Adress ID Empfänger im Eingang");
            dicLEingangColInfo.Add("Lieferant", "Adress ID Lieferant im Eingang");
            dicLEingangColInfo.Add("AbBereich", "Datenbank ID Arbeitsbereich");
            dicLEingangColInfo.Add("Mandant", "Datenbank ID Mandant");
            dicLEingangColInfo.Add("LfsNr", "Lieferscheinnummer Lagereingang");
            dicLEingangColInfo.Add("ASN", "ASN/DFÜ - Nummer");
            dicLEingangColInfo.Add("Check", "Markierung Eingang abgeschlossen");
            dicLEingangColInfo.Add("Versender", "Adress ID Versender im Eingang");
            dicLEingangColInfo.Add("SpedID", "Adress ID Spedition Anlieferung");
            dicLEingangColInfo.Add("KFZ", "KFZ-Kennzeichen Anlieferung");
            //dicLEingangColInfo.Add("Mandant", "Datenbank ID Mandant");
            //dicLEingangColInfo.Add("LfsNr", "Lieferscheinnummer Lagereingang");
            //dicLEingangColInfo.Add("ASN", "ASN/DFÜ - Nummer Lagereingang");
            //dicLEingangColInfo.Add("Check", "Markierung Eingang abgeschlossen");
            //dicLEingangColInfo.Add("Versender", "Ádress ID Versender");
            //dicLEingangColInfo.Add("SpedID", "Adress ID Spedition Anlieferung");
            //dicLEingangColInfo.Add("KFZ", "KFZ-Kennzeichen Anlieferung");
            dicLEingangColInfo.Add("DirectDelivery", "Markierung Direkte Anlieferung");
            dicLEingangColInfo.Add("Retoure", "Markierung Direkte Anlieferung");
            dicLEingangColInfo.Add("Vorfracht", "Markierung Vorfracht");
            //dicLEingangColInfo.Add("EAIDaltLVS", "Markierung Direkte Anlieferung"),
            dicLEingangColInfo.Add("LagerTransport", "Markierung Lagertransporte");
        }

        //---------------------------------------------------------------------------------------------------- LAusgang    
        public Dictionary<string, clsSQLTableColumn> dicLAusgang = new Dictionary<string, clsSQLTableColumn>();
        Dictionary<string, string> dicLAusgangColInfo = new Dictionary<string, string>();
        private void INIT_dicLAusgangColInfo()
        {
            dicLAusgangColInfo.Add("ID", "Datenbank ID Ausgang");
            dicLAusgangColInfo.Add("LAusgangID", "Augangs-ID / Ausgangs-Nr.");
            dicLAusgangColInfo.Add("Datum", "Lagerausgangsdatum");
            dicLAusgangColInfo.Add("Netto", "Nettogesamtgewicht Ausgang");
            dicLAusgangColInfo.Add("Brutto", "Bruttogewicht Ausgang");
            dicLAusgangColInfo.Add("Auftraggeber", "Adress ID Auftraggeber im Ausgang");
            dicLAusgangColInfo.Add("Versender", "Adress ID Versender im Ausgang");
            dicLAusgangColInfo.Add("Empfaenger", "Aderss ID Empfänger im Ausgang");
            dicLAusgangColInfo.Add("Entladestelle", "Adress ID Entladestelle im Ausgang");
            dicLAusgangColInfo.Add("Lieferant", "Adrss ID Lieferant im Ausgang");
            dicLAusgangColInfo.Add("LfsNr", "Lieferscheinnummer Lagerausgang");
            dicLAusgangColInfo.Add("LfsDate", "Lieferscheindatum Lagerausgang");
            //dicLAusgangColInfo.Add("SLB", "Sendungs-Ladungs-Bezugsnummer"),
            //dicLAusgangColInfo.Add("MAT", "Material auf Transport"),
            dicLAusgangColInfo.Add("Checked", "Markierung Lagerausgang abgeschlossen");
            dicLAusgangColInfo.Add("SpedID", "Adress ID Spedition Abholung");
            dicLAusgangColInfo.Add("KFZ", "KFZ-Kennzeichen Abholung");
            dicLAusgangColInfo.Add("ASN", "ASN-/DFÜ-NR Lagerausgangsmeldung");
            dicLAusgangColInfo.Add("Info", "zusätzliche Informationen Ausgang");
            dicLAusgangColInfo.Add("AbBereich", "Datenbank ID Arbeitsbereich");
            dicLAusgangColInfo.Add("MandantenID", "Datenbank ID Mandant");
            dicLAusgangColInfo.Add("Termin", "Anlieferungstermin");
            dicLAusgangColInfo.Add("DirectDelivery", "Markierung Direkte Anlieferung");
            dicLAusgangColInfo.Add("neutrAuftraggeber", "Adress ID neutraler Auftraggeber");
            dicLAusgangColInfo.Add("neutrEmpfaenger", "Adress ID neutraler Empfänger");
            dicLAusgangColInfo.Add("LagerTransport", "Markierung Lagertransport Ausgang");
            //dicLAusgangColInfo.Add("EAIDaltLVS", ""),            
        }


        ///<summary>clsSQLStatement / InitClass</summary>
        ///<remarks>Klasse wird initialisiert. Dabei müsse folgende Punkte durchgeführt werden:
        ///         - Zuweisung GLUser
        ///         - Initialisierung der DictionaryInfo
        ///         - Zuweisung DictionaryInfo und Dictionary der einzelnen Table</remarks>
        public void InitClass(Globals._GL_USER myGLUser)
        {
            this._GL_User = myGLUser;
            //Initialisierung der DictionaryInfo
            INIT_dicADRColInfo();
            INIT_dicArtikelColInfo();
            INIT_dicLAusgangColInfo();
            INIT_dicLEingangColInfo();

            //Zuweisung/erstellen der Dictionary für die einezelnen Tabellen
            //druchlaufen der listTable und dann für die einzelnen TAblelen die Dictionary erstellen
            foreach (string strTable in listDBTable)
            {

            }


        }
        ///<summary>clsSQLStatement / GetColumns</summary>
        ///<remarks></remarks>
        private void CreateDictionaryTable(string myTable)
        {
            switch (myTable)
            {
                case "ADR":
                    //dicADR.Clear();
                    //AddColumnsToList(ref dt, ref listColADR, "ADR");
                    break;

                case "Artikel":
                    //listColArtikel = new List<string>();
                    //AddColumnsToList(ref dt, ref listColArtikel, "Artikel");
                    break;

                case "Auftrag":
                    //listColAuftrag = new List<string>();
                    //AddColumnsToList(ref dt, ref listColAuftrag, "Auftrag");
                    break;

                case "AuftragPos":

                    break;

                case "AuftragRead":

                    break;

                case "Ebene":

                    break;

                case "Einheiten":

                    break;

                case "Fahrzeuge":

                    break;

                case "Gueterart":
                    //listColGueterarten = new List<string>();
                    //AddColumnsToList(ref dt, ref listColGueterarten, "Auftrag");
                    break;

                case "Halle":

                    break;

                case "Kontakte":

                    break;

                case "Kunde":

                    break;

                case "LAusgang":
                    //listColLAusgang = new List<string>();
                    //AddColumnsToList(ref dt, ref listColLAusgang, "LAusgang");
                    break;

                case "LEingang":
                    //listColLEingang = new List<string>();
                    //AddColumnsToList(ref dt, ref listColLEingang, "LEingang");
                    break;

                case "Lieferanten":

                    break;
            }
        }
        ///<summary>clsSQLStatement / AddColumnsToList</summary>
        ///<remarks></remarks>
        private void AddColumnsToList(ref DataTable dt, ref List<string> myList, string myTable)
        {
            if (dt.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    myList.Add(myTable + "." + dt.Rows[i]["column_name"].ToString());
                }
            }
        }








    }
}
