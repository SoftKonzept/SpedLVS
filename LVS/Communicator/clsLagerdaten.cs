using LVS.ASN.GlobalValues;
using LVS.Constants;
using LVS.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
//using System.Windows.Forms;
using Telerik.Reporting;


namespace LVS
{
    public class clsLagerdaten
    {

        public LVS.clsLEingang Eingang;
        public LVS.clsLAusgang Ausgang;
        public LVS.clsArtikel Artikel;
        public bool bIsStorno = false;
        public bool bStornoDelete = false;
        internal Dictionary<string, string> Dict713F10OrderID; //= new Dictionary<string, string>();
        internal Dictionary<string, ediHelper_712_TM> Dict712_Transportmittel; //= new Dictionary<string, string>();

        internal UriReportSource uRepSource;

        public List<clsLogbuchCon> ListError;
        public Globals._GL_USER GLUser;
        public Globals._GL_SYSTEM GLSystem;
        public clsSystem Sys;
        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = GLUser.User_ID;
                return _BenutzerID;
            }
            set { _BenutzerID = value; }
        }

        public decimal ASNSender { get; set; }
        public decimal ASNReceiver { get; set; }


        //public clsADR ADRAuftraggeber { get; set; }
        //public clsADR ADREmpfaenger { get; set; }

        public List<string> ListInfoAsnLfsInserted;
        public List<string> ListCreatedNewGArten;


        //internal Satz716 s716;

        ///<summary>clsLagerdaten / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSystem)
        {
            this.GLSystem = myGLSystem;
            this.GLUser = myGLUser;
        }
        ///<summary>clsLagerdaten / GetArtikelDatenFromLVSByLEingang</summary>
        ///<remarks></remarks>
        public DataTable GetArtikelDatenFromLVS(string myTableName, decimal myTableID)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            switch (myTableName)
            {
                case "LEingang":
                    strSql = "SELECT a.* " +
                                        "FROM Artikel a " +
                                        "INNER JOIN LEingang b ON b.ID=a.LEingangTableID " +
                                        " WHERE a.LEingangTableID =" + myTableID + ";";
                    break;
                case "Artikel":
                    strSql = "SELECT a.* " +
                                        "FROM Artikel a " +
                                        "INNER JOIN LEingang b ON b.ID=a.LEingangTableID " +
                                        " WHERE a.ID =" + myTableID + ";";
                    break;
                case "LAusgang":
                    strSql = "SELECT a.* " +
                                        "FROM Artikel a " +
                                        "INNER JOIN LAusgang b ON b.ID=a.LAusgangTableID " +
                                        " WHERE a.LAusgangTableID =" + myTableID + ";";
                    break;
            }
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, this.GLUser.User_ID, "Artikel");
            return dt;
        }
        ///<summary>clsLagerdaten / GetLEingangDatenFromLVS</summary>
        ///<remarks></remarks>
        public DataTable GetLEingangDatenFromLVS(string myTableName, decimal myTablID)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            switch (myTableName)
            {
                case "LEingang":
                    strSql = "SELECT a.* " +
                                        "FROM LEingang a " +
                                        " WHERE a.ID =" + myTablID + ";";
                    break;
                case "Artikel":
                    strSql = "SELECT a.* " +
                                        "FROM LEingang a " +
                                        "INNER JOIN Artikel b ON b.LEingangTableID=a.ID " +
                                        " WHERE b.ID =" + myTablID + ";";
                    break;
            }
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, this.GLUser.User_ID, "LEingang");
            return dt;
        }
        ///<summary>clsLagerdaten / GetLEingangDatenFromLVS</summary>
        ///<remarks></remarks>
        public DataTable GetLAusgangDatenFromLVS(string myTableName, decimal myTableID)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            switch (myTableName)
            {
                case "LAusgang":
                    strSql = "SELECT a.*, c.LfsNr  as LfsLieferant " +
                                        "FROM LAusgang a " +
                                        "INNER JOIN Artikel b ON b.LAusgangTableID=a.ID " +
                                        "LEFT JOIN LEingang c ON c.ID=b.LEingangTableID " +
                                        " WHERE a.ID =" + myTableID + ";";
                    break;
                case "Artikel":
                    strSql = "SELECT a.*, c.LfsNr as LfsLieferant " +
                                        "FROM LAusgang a " +
                                        "INNER JOIN Artikel b ON b.LAusgangTableID=a.ID " +
                                        "LEFT JOIN LEingang c ON c.ID=b.LEingangTableID " +
                                        " WHERE b.ID =" + myTableID + ";";
                    break;

            }
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, this.GLUser.User_ID, "LAusgang");
            return dt;
        }
        ///<summary>clsLagerdaten / AddNewEingang</summary>
        ///<remarks></remarks>
        public void AddNewEingangXMLUniport(DataTable myDT)
        {
            ListInfoAsnLfsInserted = new List<string>();
            ListError = new List<clsLogbuchCon>();
            decimal decTmp = 0;
            decimal decSenderAdrID = 0;
            Int32 iCountLfs = 0;
            clsLEingang tmpEingang = new clsLEingang();
            tmpEingang.MandantenID = this.Eingang.MandantenID;
            tmpEingang.AbBereichID = this.Eingang.AbBereichID;
            tmpEingang.LEingangID = 0;
            tmpEingang.LEingangDate = DateTime.Now;
            tmpEingang.Auftraggeber = 0;
            tmpEingang.Empfaenger = 0;
            tmpEingang.Empfaenger = 0;
            tmpEingang.Lieferant = string.Empty;

            List<clsArtikel> listArtikel = new List<clsArtikel>();
            clsArtikel Artikel = new clsArtikel();

            Dictionary<string, clsADRVerweis> dictVerweis = new Dictionary<string, clsADRVerweis>();
            dictVerweis = clsADRVerweis.FillDictAdrVerweis(this.Eingang.MandantenID, this.Eingang.AbBereichID, this.GLUser.User_ID, constValue_AsnArt.const_Art_XML_Uniport);

            clsADRVerweis tmpADRVerweis = new clsADRVerweis();

            string strVerbraucher = string.Empty;
            for (Int32 x = 0; x <= myDT.Rows.Count - 1; x++)
            {
                string strFieldName = myDT.Rows[x]["FieldName"].ToString();
                string strValue = myDT.Rows[x]["Value"].ToString();
                string strASNNr = myDT.Rows[x]["ASNID"].ToString();

                switch (strFieldName)
                {
                    case "MANDANT":
                        break;
                    case "SENDER":
                        tmpADRVerweis = new clsADRVerweis();
                        dictVerweis.TryGetValue(strValue, out tmpADRVerweis);
                        if (tmpADRVerweis != null)
                        {
                            tmpEingang.Auftraggeber = tmpADRVerweis.VerweisAdrID;
                            decSenderAdrID = tmpADRVerweis.VerweisAdrID;
                        }
                        else
                        {
                            //Fehler kein Verweis vom sENDER, somit kann die Datei auch nicht
                            //verarbeitete werden und die Verarbeiter der Datei wird abgebrochen
                            clsLogbuchCon tmpLog = new clsLogbuchCon();
                            tmpLog.ID = decTmp;
                            tmpLog.Datum = DateTime.Now;
                            tmpLog.Typ = enumLogArtItem.ERROR.ToString();
                            tmpLog.LogText = "-> SENDER - Verweis fehlt -> keine Zuordnung möglich...";
                            ListError.Add(tmpLog);
                            x = myDT.Rows.Count;
                        }
                        break;
                    case "ACTION":

                        switch (strValue)
                        {
                            case "N":

                                bIsStorno = false;
                                break;
                            case "S":
                                // STORNOMELDUNG;
                                bIsStorno = true;
                                break;
                        }
                        break;
                    case "TRAN_EXT":
                        tmpEingang.ExTransportRef = strValue;
                        break;
                    case "ATG":
                        tmpEingang.ASNRef = strValue;
                        break;
                    case "TRAN_PART":
                        //Hier werden die entsprechenden Adressen ermittelt
                        string strAdrVerweis = string.Empty;
                        LVS.clsADR AdrTU = new LVS.clsADR();
                        AdrTU._GL_User = this.GLUser;
                        LVS.clsADRVerweis tmpAdrVerweis = new LVS.clsADRVerweis();
                        tmpAdrVerweis._GL_User = this.GLUser;

                        AdrTU.WAvon = clsSystem.const_DefaultDateTimeValue_Min;
                        AdrTU.WAbis = clsSystem.const_DefaultDateTimeValue_Min;

                        bool bGetADR = false;
                        for (Int32 j = x + 1; j <= myDT.Rows.Count - 1; j++)
                        {
                            string strFieldAdr = myDT.Rows[j]["FieldName"].ToString();
                            string strValueAdr = myDT.Rows[j]["Value"].ToString();

                            switch (strFieldAdr)
                            {
                                case "PART_TYP":
                                    //hier nur TU / Sped sonst raus
                                    switch (strValueAdr)
                                    {
                                        case "TU":
                                            AdrTU.KD_ID = 0;
                                            AdrTU.IsSpedition = true;
                                            AdrTU.IsAuftraggeber = false;
                                            AdrTU.IsBelade = false;
                                            AdrTU.IsDiv = true;
                                            AdrTU.IsEmpfaenger = false;
                                            AdrTU.IsEntlade = false;
                                            AdrTU.IsPost = false;
                                            AdrTU.IsRG = false;
                                            AdrTU.IsVersender = false;
                                            bGetADR = true;
                                            break;
                                    }
                                    break;
                                case "PART_EXT":
                                    strAdrVerweis = strValueAdr;
                                    break;
                                case "PART_NAME1":
                                    AdrTU.Name1 = strValueAdr;
                                    AdrTU.ViewID = strValueAdr;
                                    break;
                                case "PART_NAME2":
                                    AdrTU.Name2 = strValueAdr;
                                    break;
                                case "PART_LKZ":
                                    AdrTU.LKZ = strValueAdr;
                                    string strLand = string.Empty;
                                    helper_Laenderkennzeichen.DicCountry().TryGetValue(strValueAdr, out strLand);
                                    //AdrTU.DictCountry.TryGetValue(strValueAdr, out strLand);
                                    AdrTU.Land = strLand;
                                    break;
                                case "PART_ZIP":
                                    AdrTU.PLZ = strValueAdr;
                                    break;
                                case "PART_ORT":
                                    AdrTU.Ort = strValueAdr;
                                    if (bGetADR)
                                    {
                                        //Adressdaten vollständig
                                        //Verweis prüfen, ob Adresse bereits vorhanden
                                        //wenn nicht vorhanden, dann Eintrag in ADR und AdrVerweis  

                                        if (dictVerweis.TryGetValue(strAdrVerweis, out tmpADRVerweis))
                                        {
                                            this.Eingang.SpedID = tmpADRVerweis.VerweisAdrID;
                                        }
                                        else
                                        {
                                            AdrTU._GL_User = this.GLUser;
                                            if (!AdrTU.ExistAdrByAnschrift())
                                            {
                                                AdrTU.Add();
                                            }
                                            //Verweis Eintragen
                                            tmpADRVerweis = new clsADRVerweis();
                                            tmpADRVerweis._GL_User = this.GLUser;
                                            tmpADRVerweis.VerweisAdrID = AdrTU.ID;
                                            tmpADRVerweis.SenderAdrID = decSenderAdrID;
                                            tmpADRVerweis.aktiv = true;
                                            tmpADRVerweis.ArbeitsbereichID = this.Eingang.AbBereichID;
                                            tmpADRVerweis.MandantenID = this.Eingang.MandantenID;
                                            //decTmp = 0;
                                            //Decimal.TryParse(strAdrVerweis, out decTmp);
                                            tmpADRVerweis.SupplierNo = string.Empty;
                                            tmpADRVerweis.Verweis = strAdrVerweis;
                                            tmpAdrVerweis.LieferantenVerweis = strAdrVerweis;
                                            tmpADRVerweis.Add();

                                            this.Eingang.SpedID = AdrTU.ID;
                                            dictVerweis = clsADRVerweis.FillDictAdrVerweis(this.Eingang.MandantenID, this.Eingang.AbBereichID, this.GLUser.User_ID, constValue_AsnArt.const_Art_XML_Uniport);
                                        }
                                    }
                                    AdrTU = new LVS.clsADR();
                                    tmpAdrVerweis = new LVS.clsADRVerweis();
                                    //diese Schleife verlassen
                                    x = j;
                                    j = myDT.Rows.Count;
                                    break;

                            }
                        }
                        break;

                    case "LS_PART":
                        string strAdrTyp = string.Empty;
                        //Hier werden die entsprechenden Adressen ermittelt
                        LVS.clsADR AdrLS = new LVS.clsADR();
                        AdrLS._GL_User = this.GLUser;
                        AdrLS.WAvon = clsSystem.const_DefaultDateTimeValue_Min;
                        AdrLS.WAbis = clsSystem.const_DefaultDateTimeValue_Min;

                        clsADRVerweis AdrLsVerweis = new clsADRVerweis();
                        string strAdrVerweisLS = string.Empty;
                        AdrLsVerweis._GL_User = this.GLUser;

                        string strAdrTypLS = string.Empty;
                        bool bGetAdrLfs = false;
                        for (Int32 j = x + 1; j <= myDT.Rows.Count - 1; j++)
                        {
                            string strFieldAdr = myDT.Rows[j]["FieldName"].ToString();
                            string strValueAdr = myDT.Rows[j]["Value"].ToString();

                            switch (strFieldAdr)
                            {
                                case "PART_TYP":
                                    strAdrTypLS = strValueAdr;
                                    switch (strValueAdr)
                                    {
                                        case "ABS":
                                            AdrLS.IsVersender = true;
                                            AdrLS.KD_ID = 0;
                                            AdrLS.IsSpedition = false;
                                            AdrLS.IsAuftraggeber = false;
                                            AdrLS.IsBelade = true;
                                            AdrLS.IsDiv = true;
                                            AdrLS.IsEmpfaenger = false;
                                            AdrLS.IsEntlade = false;
                                            AdrLS.IsPost = false;
                                            AdrLS.IsRG = false;
                                            bGetAdrLfs = true;
                                            break;

                                        case "EMP":
                                            //case "VBR":
                                            AdrLS.IsVersender = false;
                                            AdrLS.KD_ID = 0;
                                            AdrLS.IsSpedition = false;
                                            AdrLS.IsAuftraggeber = false;
                                            AdrLS.IsBelade = false;
                                            AdrLS.IsDiv = true;
                                            AdrLS.IsEmpfaenger = true;
                                            AdrLS.IsEntlade = true;
                                            AdrLS.IsPost = false;
                                            AdrLS.IsRG = false;
                                            bGetAdrLfs = true;
                                            break;

                                        case "VBR":
                                            //case "VBR":
                                            AdrLS.IsVersender = false;
                                            AdrLS.KD_ID = 0;
                                            AdrLS.IsSpedition = false;
                                            AdrLS.IsAuftraggeber = false;
                                            AdrLS.IsBelade = false;
                                            AdrLS.IsDiv = true;
                                            AdrLS.IsEmpfaenger = true;
                                            AdrLS.IsEntlade = true;
                                            AdrLS.IsPost = false;
                                            AdrLS.IsRG = false;
                                            bGetAdrLfs = true;
                                            break;
                                    }
                                    break;
                                case "PART_EXT":
                                    strAdrVerweisLS = strValueAdr;
                                    break;
                                case "PART_NAME1":
                                    AdrLS.Name1 = strValueAdr;
                                    AdrLS.ViewID = strValueAdr;
                                    break;
                                case "PART_NAME2":
                                    AdrLS.Name2 = strValueAdr;
                                    break;
                                case "PART_STRASSE":
                                    Match tMatch = Regex.Match(strValueAdr, @"(?<strasse>.*?)\s+(?<hausnr>\d+\s*.*)");
                                    if (tMatch != null)
                                    {
                                        AdrLS.Str = tMatch.Groups["strasse"].Value;
                                        AdrLS.HausNr = tMatch.Groups["hausnr"].Value;
                                    }
                                    else
                                    {
                                        AdrLS.Str = strValueAdr;
                                    }
                                    break;
                                case "PART_LKZ":
                                    AdrLS.LKZ = strValueAdr;
                                    string strLand = string.Empty;
                                    //AdrLS.DictCountry.TryGetValue(strValueAdr, out strLand);
                                    helper_Laenderkennzeichen.DicCountry().TryGetValue(strValueAdr, out strLand);
                                    AdrLS.Land = strLand;
                                    break;
                                case "PART_ZIP":
                                    AdrLS.PLZ = strValueAdr;
                                    break;
                                case "PART_ORT":
                                    AdrLS.Ort = strValueAdr;
                                    if (bGetAdrLfs)
                                    {
                                        //Adressdaten vollständig
                                        //Verweis prüfen, ob Adresse bereits vorhanden
                                        //wenn nicht vorhanden, dann Eintrag in ADR und AdrVerweis                                            
                                        if (dictVerweis.TryGetValue(strAdrVerweisLS, out AdrLsVerweis))
                                        {
                                            switch (strAdrTypLS)
                                            {
                                                case "ABS":
                                                    this.Eingang.Versender = AdrLsVerweis.VerweisAdrID;
                                                    break;
                                                case "EMP":
                                                    this.Eingang.Empfaenger = AdrLsVerweis.VerweisAdrID;
                                                    break;
                                                case "VBR":
                                                    this.Eingang.EntladeID = AdrLsVerweis.VerweisAdrID;
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            AdrLS._GL_User = this.GLUser;
                                            if (!AdrLS.ExistAdrByAnschrift())
                                            {
                                                AdrLS.Add();
                                                switch (strAdrTypLS)
                                                {
                                                    case "ABS":
                                                        this.Eingang.Versender = AdrLS.ID;
                                                        break;
                                                    case "EMP":
                                                        this.Eingang.Empfaenger = AdrLS.ID;
                                                        break;
                                                    case "VBR":
                                                        this.Eingang.EntladeID = AdrLS.ID;
                                                        break;
                                                }
                                            }
                                            //Verweis Eintragen
                                            AdrLsVerweis = new clsADRVerweis();
                                            AdrLsVerweis._GL_User = this.GLUser;
                                            AdrLsVerweis.VerweisAdrID = AdrLS.ID;
                                            AdrLsVerweis.SenderAdrID = decSenderAdrID;
                                            AdrLsVerweis.aktiv = true;
                                            AdrLsVerweis.ArbeitsbereichID = this.Eingang.AbBereichID;
                                            AdrLsVerweis.MandantenID = this.Eingang.MandantenID;

                                            AdrLsVerweis.Verweis = strAdrVerweisLS;
                                            AdrLsVerweis.LieferantenVerweis = strAdrVerweisLS;
                                            AdrLsVerweis.ASNFileTyp = constValue_AsnArt.const_Art_XML_Uniport;
                                            if (!clsADRVerweis.Exist(ref AdrLsVerweis, this.GLUser.User_ID))
                                            {
                                                AdrLsVerweis.Add();
                                            }

                                            dictVerweis = clsADRVerweis.FillDictAdrVerweis(this.Eingang.MandantenID, this.Eingang.AbBereichID, this.GLUser.User_ID, constValue_AsnArt.const_Art_XML_Uniport);

                                        }
                                        AdrLS = new clsADR();
                                        AdrLsVerweis = new clsADRVerweis();
                                    }
                                    //schleife verlassen
                                    x = j;
                                    j = myDT.Rows.Count;
                                    break;
                            }
                        }
                        break;

                    case "LS_NO":
                        if (iCountLfs > 0)
                        {
                            this.InsertEingangANDArtikelDatenToDB(listArtikel); //  keine Lfs Gru8ppierung bei Salzgitter ausgängen sondern Dateibasiert
                            iCountLfs = 0;
                        }
                        //neuer Lieferschein -> neuer Eingang
                        iCountLfs++;
                        listArtikel = new List<clsArtikel>();
                        this.Eingang = new clsLEingang();
                        this.Eingang = tmpEingang;
                        this.Eingang.LEingangLfsNr = strValue;
                        decTmp = 0;
                        Decimal.TryParse(strASNNr, out decTmp);
                        this.Eingang.ASN = decTmp;
                        this.Eingang.Checked = false;
                        break;
                    case "AUFTRAG_NOO":
                        this.Eingang.ExAuftragRef = strValue;
                        break;
                    case "WAGGONNR":

                        string tmpWaggonNo = strValue.Replace(" ", "");
                        if (tmpWaggonNo.Length == 12)
                        {
                            this.Eingang.WaggonNr = tmpWaggonNo.Substring(0, 4) + " " + tmpWaggonNo.Substring(4, 4) + " " + tmpWaggonNo.Substring(8, 3) + "-" + tmpWaggonNo.Substring(11);
                        }
                        else
                        {
                            this.Eingang.WaggonNr = strValue;
                        }
                        break;
                    case "VKMT_KZ":
                        this.Eingang.KFZ = strValue;
                        break;
                    case "AUFTRAG_POS":
                        Int32 iTmp = 0;
                        Int32.TryParse(strValue, out iTmp);
                        this.Eingang.ZP_AuftragPosNo = iTmp.ToString();
                        break;
                    case "CHRG":
                        //Ab hier muss die Tabelle separat für die Artikel weiter durchlaufen werden
                        Artikel = new clsArtikel();
                        Int32 iPos = 0;
                        for (Int32 i = x; i <= myDT.Rows.Count - 1; i++)
                        {
                            string strFieldArt = myDT.Rows[i]["FieldName"].ToString();
                            string strValueArt = myDT.Rows[i]["Value"].ToString();

                            switch (strFieldArt)
                            {
                                case "CHRG":
                                    iPos++;   //Posisition hochzählen
                                    Artikel = new clsArtikel();
                                    Artikel.Einheit = "KG";
                                    Artikel.exAuftragPos = this.Eingang.ZP_AuftragPosNo;
                                    Artikel.LZZ = clsSystem.const_DefaultDateTimeValue_Min;
                                    Artikel.Position = iPos.ToString();
                                    Artikel.exAuftrag = this.Eingang.ExAuftragRef;
                                    Artikel.BKZ = 1;
                                    break;
                                case "TPOS_NO":
                                    //Ermitteln der Warrengruppe nach Aussage von Hr. Honselmann immer nur Dicke udn Breite 
                                    //berücksichtigen                                
                                    Artikel.GArtID = clsLagerdaten.GetWarengruppenID(this.GLUser, Artikel.Dicke, Artikel.Breite, Artikel.Laenge);
                                    //lt. Hr Honselmann, wenn Coils dann keine Länge
                                    if (clsLagerdaten.IsWarengruppeCoils(this.GLUser, Artikel.Dicke, Artikel.Breite, Artikel.Laenge))
                                    {
                                        Artikel.Laenge = 0;
                                    }
                                    listArtikel.Add(Artikel);
                                    if (i < myDT.Rows.Count - 1)
                                    {
                                        if (myDT.Rows[i + 1]["FieldName"].ToString() != "CHRG")
                                        {
                                            //neuer Lieferschein
                                            Artikel = null;
                                            x = i;
                                            i = myDT.Rows.Count;
                                        }
                                    }
                                    break;
                                case "CHARGE":
                                    Artikel.Charge = strValueArt;
                                    Artikel.Produktionsnummer = strValueArt;
                                    //ArtikelID für Salzgitter
                                    //CHarge+Auftrag+AuftragPos
                                    Artikel.ArtIDRef = strValueArt +
                                                       clsSystem.const_Default_ArtikelIDRefSeparator +
                                                       this.Eingang.ExAuftragRef +
                                                       clsSystem.const_Default_ArtikelIDRefSeparator +
                                                       this.Eingang.ZP_AuftragPosNo;
                                    // Prüfen ob die ID Ref vorhanden ist?
                                    break;
                                case "GEWICHT_BRUTTO":
                                    decTmp = 0;
                                    Decimal.TryParse(strValueArt, out decTmp);
                                    Artikel.Brutto = decTmp;
                                    break;
                                case "GEWICHT_NETTO":
                                    decTmp = 0;
                                    Decimal.TryParse(strValueArt, out decTmp);
                                    Artikel.Netto = decTmp;
                                    break;
                                case "DICKE":
                                    decTmp = 0;
                                    Decimal.TryParse(strValueArt, out decTmp);
                                    Artikel.Dicke = decTmp;
                                    break;
                                case "BREITE":
                                    decTmp = 0;
                                    Decimal.TryParse(strValueArt, out decTmp);
                                    Artikel.Breite = decTmp;
                                    break;
                                case "LAENGE":
                                    decTmp = 0;
                                    Decimal.TryParse(strValueArt, out decTmp);
                                    //da in der XMLDatei die Länge in m und nicht in mm 
                                    //angegeben wird hier * 1000
                                    Artikel.Laenge = decTmp * 1000;
                                    break;
                                case "ANZAHL":
                                    iTmp = 0;
                                    Int32.TryParse(strValueArt, out iTmp);
                                    Artikel.Anzahl = iTmp;
                                    break;
                            }
                        }
                        break;
                }
            }
            //wenn hier der Lieferschein Count = 1 ist, dann dann muss der Eintrag vorgenommen werden, da 
            //nach nur einem Durchlauf sonst die STelle zum Eintrag erreicht wird

            if ((iCountLfs >= 1) && (ListError.Count < 1))
            {
                this.InsertEingangANDArtikelDatenToDB(listArtikel);
            }

            if (bIsStorno)
            {
                this.deleteEingangANDArtikelDaten(tmpEingang.ExTransportRef);
            }
        }
        /// <summary>
        /// clsLagerdaten / InsertEingangANDArtikelDatenToDB
        /// </summary>
        private void deleteEingangANDArtikelDaten(string Plannummer)
        {
            string strSql = string.Empty;
            strSql = "Delete from Artikel where LeingangTableID in(Select ID from Leingang where exTransportRef='" + Plannummer + "');";
            strSql += "Delete from Leingang where exTransportRef='" + Plannummer + "';";
            clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "StornoEingang", this.BenutzerID);
        }
        ///<summary>clsLagerdaten / InsertEingangANDArtikelDatenToDB</summary>
        ///<remarks></remarks>
        private void InsertEingangANDArtikelDatenToDB(List<clsArtikel> myListArtikel)
        {
            if (myListArtikel.Count > 0)
            {
                List<string> listArtToUpdate = new List<string>();
                string strSql = string.Empty;
                strSql = "DECLARE @LEingangTableID as decimal(28,0); " +
                         "DECLARE @LvsID as decimal(28,0); " +
                         "DECLARE @ArtID as decimal(28,0); ";

                strSql = strSql +
                        this.Eingang.AddLagerEingangSQL() +
                        " Select @LEingangTableID= @@IDENTITY; ";

                for (Int32 i = 0; i <= myListArtikel.Count - 1; i++)
                {
                    clsArtikel tmp = (clsArtikel)myListArtikel[i];
                    if (!clsArtikel.ExistIDRef(tmp.ArtIDRef, this.BenutzerID))   // Prüfung auf doppelte IDREF 
                    {
                        tmp.MandantenID = this.Eingang.MandantenID;
                        tmp.AbBereichID = this.Eingang.AbBereichID;

                        tmp.BKZ = 1;

                        strSql = strSql + tmp.AddArtikelLager_SQL(false, true);
                        strSql = strSql + "SET @ArtID=(Select @@IDENTITY); ";
                        strSql = strSql + "SET  @LvsID=(SELECT MAX(Artikel.LVS_ID) FROM Artikel INNER JOIN LEingang ON LEingang.ID=Artikel.LEingangTableID WHERE LEingang.Mandant=" + this.Eingang.MandantenID + ")+1; ";
                        strSql = strSql + "Update Artikel SET LVS_ID=@LvsID " +
                                                             "WHERE ID = @ArtID; ";
                        strSql = strSql + "UPDATE PrimeKeys SET LvsNr = @LvsID WHERE Mandanten_ID=" + this.Eingang.MandantenID + ";";
                    }
                    else
                    {
                        myListArtikel.RemoveAt(i);
                        i--;
                    }
                }
                if (myListArtikel.Count > 0)
                {
                    strSql = strSql + " Select @LEingangTableID;";
                    string strTmp = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSql, "AddEingang", BenutzerID);

                    decimal decTmp = 0;
                    Decimal.TryParse(strTmp, out decTmp);
                    if (decTmp > 0)
                    {
                        this.Eingang.LEingangTableID = decTmp;
                        this.Eingang.FillEingang();

                        //Direct Print 
                        //2014_07_15 Eingangsschein soll lt. Herrn Honselmann nicht gedruckt werden
                        //UriReportSource uRepSource = new UriReportSource();
                        //uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LEingangTableID", this.Eingang.LEingangTableID));
                        //uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("BenutzerID", GLUser.User_ID));
                        //uRepSource.Uri = Application.StartupPath + this.GLSystem.Doc_EingangDoc;
                        //clsPrint.PrintDirect(2, uRepSource);

                        //als Rückgabe erhalten wir die LEingangTableID
                        //ArtikelVIta

                        string myBeschreibung = "Lager - Eingang autom. erstellt: NR [" + this.Eingang.LEingangID.ToString() + "] / Mandant [" + this.Eingang.MandantenID.ToString() + "] / Arbeitsbereich  [" + this.Eingang.AbBereichID.ToString() + "] ";
                        Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), myBeschreibung);

                        //Infoausgabe
                        strTmp = string.Empty;
                        strTmp = ".[ADD] - ASN ID / Lfs-Nr / EingangID : [" + this.Eingang.ASN.ToString() + " / " + this.Eingang.LEingangLfsNr + " / " + this.Eingang.LEingangID.ToString() + "]";
                        ListInfoAsnLfsInserted.Add(strTmp);

                        //Artikel eintragen
                        //Eintrag ArtikelVita
                        strSql = string.Empty;
                        List<string> listArtikelID = clsArtikel.GetArtikelByLEingangTableID(this.GLUser.User_ID, this.Eingang.LEingangTableID);
                        for (Int32 i = 0; i <= listArtikelID.Count - 1; i++)
                        {
                            decTmp = 0;
                            Decimal.TryParse(listArtikelID[i].ToString(), out decTmp);
                            if (decTmp > 0)
                            {
                                clsArtikel tmpArt = new clsArtikel();
                                tmpArt._GL_User = this.GLUser;
                                tmpArt.ID = decTmp;
                                tmpArt.GetArtikeldatenByTableID();

                                strSql = strSql + clsArtikelVita.AddArtikelLEingangAuto(this.GLUser, tmpArt.ID, this.Eingang.LEingangID);
                                strSql = strSql + clsArtikelVita.AddArtikelLagermeldungen(this.GLUser, tmpArt.ID, enumLagerMeldungen.LSL.ToString());

                                myBeschreibung = "Artikel autom. hinzugefügt: LVS-NR [" + tmpArt.LVS_ID.ToString() + "] / Eingang [" + this.Eingang.LEingangID.ToString() + "]";
                                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), myBeschreibung);

                            }
                        }
                        strSql = strSql + clsArtikelVita.AddEinlagerungAuto(this.GLUser.User_ID, this.Eingang.LEingangTableID, this.Eingang.LEingangID);
                        bool bOK = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "ArtikelVita", this.BenutzerID);
                    }
                }
            }
        }
        ///<summary>clsLagerdaten / AddLAusgang</summary>
        ///<remarks></remarks>
        public void AddLAusgang(DataTable myDT, int arbeitsbereich, int mandant)
        {
            ListInfoAsnLfsInserted = new List<string>();
            ListError = new List<clsLogbuchCon>();
            decimal decTmp = 0;
            decimal decSenderAdrID = 0;
            Int32 iCountLfs = 0;

            clsLAusgang tmpAusgang = new clsLAusgang();

            List<clsArtikel> listArtikel = new List<clsArtikel>();
            clsArtikel Artikel = new clsArtikel();

            Dictionary<string, clsADRVerweis> dictVerweis = new Dictionary<string, clsADRVerweis>();
            dictVerweis = clsADRVerweis.FillDictAdrVerweis(this.Eingang.MandantenID, this.Eingang.AbBereichID, this.GLUser.User_ID, constValue_AsnArt.const_Art_XML_Uniport);

            clsADRVerweis tmpADRVerweis = new clsADRVerweis();

            string strVerbraucher = string.Empty;
            int asnId = Int32.Parse(myDT.Rows[0]["ASNID"].ToString());
            for (Int32 x = 0; x <= myDT.Rows.Count - 1; x++)
            {
                string strFieldName = myDT.Rows[x]["FieldName"].ToString();
                string strValue = myDT.Rows[x]["Value"].ToString();
                string strASNNr = myDT.Rows[x]["ASNID"].ToString();

                switch (strFieldName)
                {
                    case "MANDANT":
                        break;

                    case "SENDER":
                        tmpADRVerweis = new clsADRVerweis();
                        dictVerweis.TryGetValue(strValue, out tmpADRVerweis);
                        tmpAusgang.Auftraggeber = tmpADRVerweis.VerweisAdrID;

                        tmpAusgang.MandantenID = tmpADRVerweis.MandantenID;
                        tmpAusgang.AbBereichID = tmpADRVerweis.ArbeitsbereichID;

                        decSenderAdrID = tmpADRVerweis.VerweisAdrID;
                        break;
                    case "ACTION":

                        switch (strValue)
                        {
                            case "N":

                                bIsStorno = false;
                                break;
                            case "S":
                                // STORNOMELDUNG;
                                bIsStorno = true;
                                break;
                        }
                        break;

                    case "TRAN_EXT":
                        tmpAusgang.exTransportRef = strValue;
                        if (!bIsStorno)
                        {
                            this.deleteAusgang(tmpAusgang.exTransportRef, asnId, bIsStorno);
                        }
                        break;

                    case "VSDT":
                        DateTime dtResult;
                        if (DateTime.TryParseExact(strValue, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dtResult))
                        {
                            tmpAusgang.Termin = dtResult;
                        }
                        else
                        {
                            tmpAusgang.Termin = clsSystem.const_DefaultDateTimeValue_Min;
                        }
                        break;
                    case "TRAN_PART":
                        //Hier werden die entsprechenden Adressen ermittelt
                        string strAdrVerweis = string.Empty;
                        clsADR AdrTU = new clsADR();
                        AdrTU._GL_User = this.GLUser;
                        clsADRVerweis tmpAdrVerweis = new clsADRVerweis();
                        tmpAdrVerweis._GL_User = this.GLUser;

                        AdrTU.WAvon = clsSystem.const_DefaultDateTimeValue_Min;
                        AdrTU.WAbis = clsSystem.const_DefaultDateTimeValue_Min;

                        bool bGetADR = false;
                        for (Int32 j = x + 1; j <= myDT.Rows.Count - 1; j++)
                        {
                            string strFieldAdr = myDT.Rows[j]["FieldName"].ToString();
                            string strValueAdr = myDT.Rows[j]["Value"].ToString();

                            switch (strFieldAdr)
                            {
                                case "PART_TYP":
                                    //hier nur TU / Sped sonst raus
                                    switch (strValueAdr)
                                    {
                                        case "TU":
                                            AdrTU.KD_ID = 0;
                                            AdrTU.IsSpedition = true;
                                            AdrTU.IsAuftraggeber = false;
                                            AdrTU.IsBelade = false;
                                            AdrTU.IsDiv = true;
                                            AdrTU.IsEmpfaenger = false;
                                            AdrTU.IsEntlade = false;
                                            AdrTU.IsPost = false;
                                            AdrTU.IsRG = false;
                                            AdrTU.IsVersender = false;
                                            bGetADR = true;
                                            break;
                                    }
                                    break;
                                case "PART_EXT":
                                    strAdrVerweis = strValueAdr;
                                    break;
                                case "PART_NAME1":
                                    AdrTU.Name1 = strValueAdr;
                                    AdrTU.ViewID = strValueAdr;
                                    break;
                                case "PART_NAME2":
                                    AdrTU.Name2 = strValueAdr;
                                    break;
                                case "PART_LKZ":
                                    AdrTU.LKZ = strValueAdr;
                                    string strLand = string.Empty;
                                    helper_Laenderkennzeichen.DicCountry().TryGetValue(strValueAdr, out strLand);
                                    //AdrTU.DictCountry.TryGetValue(strValueAdr, out strLand);
                                    AdrTU.Land = strLand;
                                    break;
                                case "PART_ZIP":
                                    AdrTU.PLZ = strValueAdr;
                                    break;
                                case "PART_ORT":
                                    AdrTU.Ort = strValueAdr;
                                    if (bGetADR)
                                    {
                                        //Adressdaten vollständig
                                        //Verweis prüfen, ob Adresse bereits vorhanden
                                        //wenn nicht vorhanden, dann Eintrag in ADR und AdrVerweis  
                                        if (dictVerweis.TryGetValue(strAdrVerweis, out tmpADRVerweis))
                                        {
                                            tmpAusgang.SpedID = tmpADRVerweis.VerweisAdrID;
                                        }
                                        else
                                        {
                                            AdrTU._GL_User = this.GLUser;
                                            if (!AdrTU.ExistAdrByAnschrift())
                                            {
                                                AdrTU.Add();
                                            }
                                            //Verweis Eintragen
                                            tmpADRVerweis = new clsADRVerweis();
                                            tmpADRVerweis._GL_User = this.GLUser;
                                            tmpADRVerweis.VerweisAdrID = AdrTU.ID;
                                            tmpADRVerweis.SenderAdrID = decSenderAdrID;
                                            tmpADRVerweis.aktiv = true;
                                            tmpADRVerweis.ArbeitsbereichID = this.Eingang.AbBereichID;
                                            tmpADRVerweis.MandantenID = this.Eingang.MandantenID;
                                            //decTmp = 0;
                                            //Decimal.TryParse(strAdrVerweis, out decTmp);
                                            tmpADRVerweis.SupplierNo = string.Empty;
                                            tmpADRVerweis.Verweis = strAdrVerweis;
                                            tmpADRVerweis.LieferantenVerweis = strAdrVerweis;
                                            tmpADRVerweis.Add();

                                            this.Eingang.SpedID = AdrTU.ID;
                                            dictVerweis = clsADRVerweis.FillDictAdrVerweis(this.Eingang.MandantenID, this.Eingang.AbBereichID, this.GLUser.User_ID, constValue_AsnArt.const_Art_XML_Uniport);
                                        }
                                    }
                                    AdrTU = new clsADR();
                                    tmpAdrVerweis = new clsADRVerweis();
                                    //diese Schleife verlassen
                                    x = j;
                                    j = myDT.Rows.Count;
                                    break;
                            }
                        }
                        break;

                    case "LS_PART":
                        string strAdrTyp = string.Empty;
                        //Hier werden die entsprechenden Adressen ermittelt
                        clsADR AdrLS = new clsADR();
                        AdrLS._GL_User = this.GLUser;
                        AdrLS.WAvon = clsSystem.const_DefaultDateTimeValue_Min;
                        AdrLS.WAbis = clsSystem.const_DefaultDateTimeValue_Min;

                        clsADRVerweis AdrLsVerweis = new clsADRVerweis();
                        string strAdrVerweisLS = string.Empty;
                        AdrLsVerweis._GL_User = this.GLUser;

                        string strAdrTypLS = string.Empty;
                        bool bGetAdrLfs = false;
                        for (Int32 j = x + 1; j <= myDT.Rows.Count - 1; j++)
                        {
                            string strFieldAdr = myDT.Rows[j]["FieldName"].ToString();
                            string strValueAdr = myDT.Rows[j]["Value"].ToString();

                            switch (strFieldAdr)
                            {
                                case "PART_TYP":
                                    strAdrTypLS = strValueAdr;
                                    switch (strValueAdr)
                                    {
                                        case "ABS":
                                            AdrLS.IsVersender = true;
                                            AdrLS.KD_ID = 0;
                                            AdrLS.IsSpedition = false;
                                            AdrLS.IsAuftraggeber = false;
                                            AdrLS.IsBelade = true;
                                            AdrLS.IsDiv = true;
                                            AdrLS.IsEmpfaenger = false;
                                            AdrLS.IsEntlade = false;
                                            AdrLS.IsPost = false;
                                            AdrLS.IsRG = false;
                                            bGetAdrLfs = true;
                                            break;

                                        case "EMP":
                                            //case "VBR":
                                            AdrLS.IsVersender = false;
                                            AdrLS.KD_ID = 0;
                                            AdrLS.IsSpedition = false;
                                            AdrLS.IsAuftraggeber = false;
                                            AdrLS.IsBelade = false;
                                            AdrLS.IsDiv = true;
                                            AdrLS.IsEmpfaenger = true;
                                            AdrLS.IsEntlade = true;
                                            AdrLS.IsPost = false;
                                            AdrLS.IsRG = false;
                                            bGetAdrLfs = true;
                                            break;

                                        case "VBR":
                                            //case "VBR":
                                            AdrLS.IsVersender = false;
                                            AdrLS.KD_ID = 0;
                                            AdrLS.IsSpedition = false;
                                            AdrLS.IsAuftraggeber = false;
                                            AdrLS.IsBelade = false;
                                            AdrLS.IsDiv = true;
                                            AdrLS.IsEmpfaenger = true;
                                            AdrLS.IsEntlade = true;
                                            AdrLS.IsPost = false;
                                            AdrLS.IsRG = false;
                                            bGetAdrLfs = true;
                                            break;
                                    }
                                    break;
                                case "PART_EXT":
                                    strAdrVerweisLS = strValueAdr;
                                    break;
                                case "PART_NAME1":
                                    AdrLS.Name1 = strValueAdr;
                                    AdrLS.ViewID = strValueAdr;
                                    break;
                                case "PART_NAME2":
                                    AdrLS.Name2 = strValueAdr;
                                    break;
                                case "PART_STRASSE":
                                    Match tMatch = Regex.Match(strValueAdr, @"(?<strasse>.*?)\s+(?<hausnr>\d+\s*.*)");
                                    if (tMatch != null)
                                    {
                                        AdrLS.Str = tMatch.Groups["strasse"].Value;
                                        AdrLS.HausNr = tMatch.Groups["hausnr"].Value;
                                    }
                                    else
                                    {
                                        AdrLS.Str = strValueAdr;
                                    }
                                    break;
                                case "PART_LKZ":
                                    AdrLS.LKZ = strValueAdr;
                                    string strLand = string.Empty;
                                    helper_Laenderkennzeichen.DicCountry().TryGetValue(strValueAdr, out strLand);
                                    AdrLS.Land = strLand;
                                    break;
                                case "PART_ZIP":
                                    AdrLS.PLZ = strValueAdr;
                                    break;
                                case "PART_ORT":
                                    AdrLS.Ort = strValueAdr;
                                    if (bGetAdrLfs)
                                    {
                                        //Adressdaten vollständig
                                        //Verweis prüfen, ob Adresse bereits vorhanden
                                        //wenn nicht vorhanden, dann Eintrag in ADR und AdrVerweis                                            
                                        if (dictVerweis.TryGetValue(strAdrVerweisLS, out AdrLsVerweis))
                                        {
                                            switch (strAdrTypLS)
                                            {
                                                case "ABS":
                                                    this.Ausgang.Versender = AdrLsVerweis.VerweisAdrID;
                                                    break;
                                                case "EMP":
                                                    this.Ausgang.Empfaenger = AdrLsVerweis.VerweisAdrID;
                                                    break;
                                                case "VBR":
                                                    this.Ausgang.Entladestelle = AdrLsVerweis.VerweisAdrID;
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            AdrLS._GL_User = this.GLUser;
                                            if (!AdrLS.ExistAdrByAnschrift())
                                            {
                                                AdrLS.Add();
                                            }
                                            //Verweis Eintragen
                                            AdrLsVerweis = new clsADRVerweis();
                                            AdrLsVerweis._GL_User = this.GLUser;
                                            AdrLsVerweis.VerweisAdrID = AdrLS.ID;
                                            AdrLsVerweis.SenderAdrID = decSenderAdrID;
                                            AdrLsVerweis.aktiv = true;
                                            AdrLsVerweis.ArbeitsbereichID = this.Ausgang.AbBereichID;
                                            AdrLsVerweis.MandantenID = this.Ausgang.MandantenID;
                                            AdrLsVerweis.Verweis = strAdrVerweisLS;
                                            AdrLsVerweis.LieferantenVerweis = strAdrVerweisLS;
                                            AdrLsVerweis.ASNFileTyp = constValue_AsnArt.const_Art_XML_Uniport;

                                            if (!clsADRVerweis.Exist(ref AdrLsVerweis, this.GLUser.User_ID))
                                            {
                                                AdrLsVerweis.Add();
                                            }
                                            dictVerweis = clsADRVerweis.FillDictAdrVerweis(this.Eingang.MandantenID, this.Eingang.AbBereichID, this.GLUser.User_ID, constValue_AsnArt.const_Art_XML_Uniport);

                                            switch (strAdrTypLS)
                                            {
                                                case "ABS":
                                                    this.Ausgang.Versender = AdrLsVerweis.VerweisAdrID;
                                                    break;
                                                case "EMP":
                                                    this.Ausgang.Empfaenger = AdrLsVerweis.VerweisAdrID;
                                                    break;
                                                case "VBR":
                                                    this.Ausgang.Entladestelle = AdrLsVerweis.VerweisAdrID;
                                                    break;
                                            }
                                        }
                                        AdrLS = new clsADR();
                                        AdrLsVerweis = new clsADRVerweis();
                                    }
                                    //schleife verlassen
                                    x = j;
                                    j = myDT.Rows.Count;
                                    break;
                            }
                        }
                        break;

                    case "LS_NO":
                        if (iCountLfs > 0)
                        {
                            this.InsertAusgangToDB(listArtikel);
                        }
                        //neuer Lieferschein -> neuer Ausgang
                        iCountLfs++;
                        listArtikel = new List<clsArtikel>();
                        this.Ausgang = new clsLAusgang();
                        this.Ausgang = tmpAusgang;
                        //this.Ausgang.LfsNr = strValue;
                        decTmp = 0;
                        Decimal.TryParse(strASNNr, out decTmp);
                        this.Ausgang.ASN = decTmp;
                        this.Ausgang.Checked = false;
                        this.Ausgang.ZP_LsNo = strValue;
                        break;
                    case "AUFTRAG_NOO":
                        this.Ausgang.ZP_AuftragNo = strValue;  //wird hier als zwischenspeicher für Verwendet
                        break;
                    case "LS_POS":
                        this.Ausgang.ZP_LsPos = strValue;
                        break;
                    case "AUFTRAG_POS":
                        Int32 iTmp = 0;
                        Int32.TryParse(strValue, out iTmp);
                        this.Ausgang.ZP_AuftragPosNo = iTmp.ToString();  //wird hier als zwischenspeicher für Verwendet
                        break;
                    case "CHRG":
                        //Ab hier muss die Tabelle separat für die Artikel weiter durchlaufen werden
                        Artikel = new clsArtikel();
                        for (Int32 i = x; i <= myDT.Rows.Count - 1; i++)
                        {
                            string strFieldArt = myDT.Rows[i]["FieldName"].ToString();
                            string strValueArt = myDT.Rows[i]["Value"].ToString();

                            switch (strFieldArt)
                            {
                                case "CHRG":
                                    Artikel = new clsArtikel();
                                    break;
                                case "TPOS_NO":
                                    Artikel.UpdateArtikelLager();
                                    listArtikel.Add(Artikel);
                                    if (i < myDT.Rows.Count - 1)
                                    {
                                        if (myDT.Rows[i + 1]["FieldName"].ToString() != "CHRG")
                                        {
                                            //neuer Lieferschein
                                            Artikel = null;
                                            x = i;
                                            i = myDT.Rows.Count;
                                        }
                                    }
                                    break;
                                case "CHARGE":
                                    Artikel.Charge = strValueArt;
                                    //ArtikelID für Salzgitter
                                    //Charge+Auftrag+AuftragPos
                                    Artikel.ArtIDRef = strValueArt +
                                                       clsSystem.const_Default_ArtikelIDRefSeparator +
                                                       this.Ausgang.ZP_AuftragNo +
                                                       clsSystem.const_Default_ArtikelIDRefSeparator +
                                                       this.Ausgang.ZP_AuftragPosNo;  //ID zur identifizierung des Artikels
                                    Artikel.exAuftrag = this.Ausgang.ZP_AuftragNo;
                                    Artikel.exAuftragPos = this.Ausgang.ZP_AuftragPosNo;
                                    Artikel.exLsNoA = this.Ausgang.ZP_LsNo;
                                    Artikel.exLsPosA = this.Ausgang.ZP_LsPos;

                                    break;
                                case "GEWICHT_BRUTTO":
                                    decTmp = 0;
                                    Decimal.TryParse(strValueArt, out decTmp);
                                    Artikel.Brutto = decTmp;
                                    break;
                                case "GEWICHT_NETTO":
                                    decTmp = 0;
                                    Decimal.TryParse(strValueArt, out decTmp);
                                    Artikel.Netto = decTmp;
                                    break;
                                case "LAENGE":
                                    decTmp = 0;
                                    Decimal.TryParse(strValueArt, out decTmp);
                                    Artikel.Laenge = decTmp;
                                    break;
                                case "DICKE":
                                    decTmp = 0;
                                    Decimal.TryParse(strValueArt, out decTmp);
                                    Artikel.Dicke = decTmp;
                                    break;
                                case "BREITE":
                                    decTmp = 0;
                                    Decimal.TryParse(strValueArt, out decTmp);
                                    Artikel.Breite = decTmp;
                                    break;
                                case "ANZAHL":
                                    break;
                            }
                        }
                        break;
                }
            }
            //wenn hier der Lieferschein Count >= 1 ist, dann dann muss der Eintrag vorgenommen werden, da 
            //nach nur einem Durchlauf sonst die STelle zum Eintrag erreicht wird
            if (iCountLfs >= 1)
            {
                this.InsertAusgangToDB(listArtikel);
            }
            if (bIsStorno)
            {
                this.deleteAusgang(tmpAusgang.exTransportRef, asnId);
            }
        }
        /// <summary>
        /// clsLagerdaten / deleteAusgang
        /// </summary>
        /// <param name="p"></param>
        private void deleteAusgang(string Plannummer, Int32 asn = 0, bool bIsStorno = true)
        {
            bStornoDelete = false;
            string strSql = string.Empty;
            string strSqlSelect = "select ID from Lausgang where exTransportRef='" + Plannummer + "' and ASN < " + asn + ";";
            DataTable dtStornos = clsSQLcon.ExecuteSQL_GetDataTable(strSqlSelect, this._BenutzerID, "Storno");
            clsSystem sys = new clsSystem();
            sys.GetDocPathByMandant(ref this.GLSystem, 1); // mandant hinterlegen
            for (int i = 0; i < dtStornos.Rows.Count; i++)
            {
                UriReportSource uRepSource = new UriReportSource();
                uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LAusgangTableID", dtStornos.Rows[0]["ID"]));
                uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("BenutzerID", GLUser.User_ID));
                uRepSource.Uri = helper_WindowsFromsdll.ApplicationStartupPath() + this.GLSystem.docPath_AusgangStornoDoc;
                //uRepSource.Uri = Application.StartupPath + this.GLSystem.docPath_AusgangStornoDoc;// Doc_AusgangDoc;
                clsPrint.PrintDirect(1, uRepSource);
            }
            if (dtStornos.Rows.Count > 0)
            {

                strSql = "Update Artikel " +
                            "set " +
                                "LausgangTableId=0" +
                                ", LA_Checked=0" +
                                ", BKZ=1 " +
                                " where LausgangTableID in(Select ID from Lausgang where exTransportRef='" + Plannummer + "' and  ASN < " + asn + "  and [Checked]=0);";

                strSql += "Delete from LAusgang where exTransportRef='" + Plannummer + "'  and  ASN < " + asn + " and [checked]=0;";

                clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "StornoEingang", this.BenutzerID);
                bStornoDelete = true;
                clsLogbuchCon Log = new clsLogbuchCon();
                Log.GL_User = this.GLUser;
                Log.Typ = "STORNO"; // KONSTANTE EINBAUEN
                Log.LogText = "Storno für " + Plannummer + " durchgeführt!";
                Log.Add(Log.GetAddLogbuchSQLString());

            }
            else
            {
                clsLogbuchCon ErrorLog = new clsLogbuchCon();
                ErrorLog.GL_User = this.GLUser;
                ErrorLog.Typ = enumLogArtItem.ERROR.ToString();
                ErrorLog.LogText = "Storno für " + Plannummer + " ohne entsprechenden Ein/Ausgang!";
                ListError.Add(ErrorLog);
            }

        }
        ///<summary>clsLagerdaten / InsertEingangANDArtikelDatenToDB</summary>
        ///<remarks></remarks>
        private void InsertAusgangToDB(List<clsArtikel> myListArtikel)
        {
            decimal decTmp = 0;
            bool bArtikelFehlt = false;
            List<string> listArtIDRef = new List<string>();
            for (Int32 i = 0; i <= myListArtikel.Count - 1; i++)
            {
                clsArtikel tmpArt = (clsArtikel)myListArtikel[i];
                listArtIDRef.Add(tmpArt.ArtIDRef);
            }
            listArtIDRef = (new HashSet<string>(listArtIDRef)).ToList();
            //Artikel ermittlen und abgeleichen und Artlist neu füllen
            DataTable dt = clsArtikel.GetArtikelForAbrufByAuftraggeberAndArtIDRef(this.GLUser.User_ID, this.Ausgang.Auftraggeber, listArtIDRef, false);
            DataTable dtAusgang = clsArtikel.GetArtikelForAbrufByAuftraggeberAndArtIDRef(this.GLUser.User_ID, this.Ausgang.Auftraggeber, listArtIDRef, true);
            if (dt.Rows.Count == 0 && dtAusgang.Rows.Count == 0)
            {
                for (Int32 i = 0; i <= myListArtikel.Count - 1; i++)
                {
                    clsArtikel tmpArt = (clsArtikel)myListArtikel[i];
                    listArtIDRef.Add(tmpArt.ArtIDRef);
                    clsLogbuchCon ErrorLog = new clsLogbuchCon();
                    ErrorLog.GL_User = this.GLUser;
                    ErrorLog.Typ = enumLogArtItem.ERROR.ToString();
                    ErrorLog.LogText = "Auftrag:[" + tmpArt.exAuftrag + "]/ AuftragPos [" + tmpArt.exAuftragPos + "]/ Charge: [" + tmpArt.Charge + "]" + //Environment.NewLine +
                                        " => Artikel nicht vorhanden!";
                    ListError.Add(ErrorLog);
                    bArtikelFehlt = true;
                }
                myListArtikel.Clear();
            }
            else
            {
                if (dt.Rows.Count <= listArtIDRef.Count)
                {

                    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        //clsArtikel tmpArt = (clsArtikel)myListArtikel[i];

                        listArtIDRef.Remove(dt.Rows[i]["ArtIDRef"].ToString());
                    }
                    for (Int32 i = 0; i <= dtAusgang.Rows.Count - 1; i++)
                    {
                        listArtIDRef.Remove(dtAusgang.Rows[i]["ArtIDRef"].ToString());
                    }
                    for (Int32 i = 0; i <= myListArtikel.Count - 1; i++)
                    {
                        clsArtikel tmpArt = (clsArtikel)myListArtikel[i];
                        //listArtIDRef.Add(tmpArt.ArtIDRef);
                        if (listArtIDRef.Contains(tmpArt.ArtIDRef))
                        {
                            clsLogbuchCon ErrorLog = new clsLogbuchCon();
                            ErrorLog.GL_User = this.GLUser;
                            ErrorLog.Typ = enumLogArtItem.ERROR.ToString();
                            ErrorLog.LogText = "Auftrag:[" + tmpArt.exAuftrag + "]/ AuftragPos [" + tmpArt.exAuftragPos + "]/ Charge: [" + tmpArt.Charge + "]" + //Environment.NewLine +
                                                " => Artikel nicht vorhanden!";
                            ListError.Add(ErrorLog);
                            bArtikelFehlt = true;
                        }

                    }

                    if (dt.Rows.Count > 0 && !bArtikelFehlt)
                    {
                        this.Ausgang.LAusgangsDate = DateTime.Now;
                        //this.Ausgang.LfsDate = clsSystem.const_DefaultDateTimeValue_Min;
                        //this.Ausgang.Termin = clsSystem.const_DefaultDateTimeValue_Min;
                        this.Ausgang.IsPrintDoc = true;

                        string strSql = string.Empty;
                        strSql = "DECLARE @LAusgangsTableID as decimal(28,0); " +
                                 //"DECLARE @LvsID as decimal(28,0); " +
                                 "DECLARE @ArtID as decimal(28,0); ";

                        strSql = strSql +
                                this.Ausgang.AddLAusgang_SQL() +
                                " Select @LAusgangsTableID= @@IDENTITY; ";

                        List<decimal> listArtikelUpdate = new List<decimal>();
                        //erhalt der gesuchten Artikel 
                        if (dt.Rows.Count > 0)
                        {
                            String strSQL2 = string.Empty;
                            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                decTmp = 0;
                                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                                if (decTmp > 0)
                                {
                                    clsArtikel tmpArt = (clsArtikel)myListArtikel[i];
                                    listArtikelUpdate.Add(decTmp);
                                    strSQL2 = "Update Artikel SET " +
                                                       // " exAuftrag ='" + tmpArt.exAuftrag + "'" +
                                                       //", exAuftragPos ='" + tmpArt.exAuftragPos + "'" +
                                                       " exLsNoA='" + tmpArt.exLsNoA + "'" +
                                                      ", exLsPosA='" + tmpArt.exLsPosA + "'" +
                                                       " WHERE ID=" + decTmp + ";";
                                    string strX = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSQL2, "UpdateArtikel", BenutzerID);

                                }
                            }
                        }
                        //in der LIST listArtikelUpdate sind nun alle Artikel drin, die dem Ausgang zugewiesen werden 
                        //sollen -> die Table dt enthält jetzt noch 
                        string strArtID = string.Join(",", listArtikelUpdate.ToArray());
                        strSql = strSql + "Update Artikel SET " +
                                                    "LAusgangTableID=@LAusgangsTableID " +
                                                    ", LA_Checked =1 " +
                                                    "WHERE ID IN (" + strArtID + ");";
                        strSql = strSql + " Select @LAusgangsTableID;";

                        string strTmp = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSql, "AddAusgang", BenutzerID);
                        decTmp = 0;
                        Decimal.TryParse(strTmp, out decTmp);
                        if (decTmp > 0)
                        {
                            //als Rückgabe erhalten wir die LEingangTableID
                            //ArtikelVIta
                            this.Ausgang = new clsLAusgang();
                            this.Ausgang._GL_User = this.GLUser;
                            this.Ausgang.LAusgangTableID = decTmp;
                            this.Ausgang.FillAusgang();
                            //if (this.Ausgang.Auftraggeber != 19) // TEMP keine Drucke für Salzgitter
                            //{
                            UriReportSource uRepSource = new UriReportSource();
                            uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LAusgangTableID", this.Ausgang.LAusgangTableID));
                            uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("BenutzerID", GLUser.User_ID));

                            //MessageBox.Show(Application.StartupPath + this.GLSystem.docPath_AusgangDoc);
                            clsSystem sys = new clsSystem();
                            sys.InitSystem(ref this.GLSystem);
                            sys.GetDocPathByMandant(ref this.GLSystem, 1); // mandant hinterlegen
                            uRepSource.Uri = helper_WindowsFromsdll.ApplicationStartupPath() + this.GLSystem.docPath_AusgangDoc;// Doc_AusgangDoc;
                            //uRepSource.Uri = Application.StartupPath + this.GLSystem.docPath_AusgangDoc;// Doc_AusgangDoc;
                            clsPrint.PrintDirect(2, uRepSource);
                            //}
                            string myBeschreibung = "Lager - Ausgang autom. erstellt: NR [" + Ausgang.LAusgangID.ToString() + "] / Mandant [" + this.Ausgang.MandantenID.ToString() + "] / Arbeitsbereich  [" + this.Ausgang.AbBereichID.ToString() + "] ";
                            Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), myBeschreibung);

                            //Infoausgabe
                            strTmp = string.Empty;
                            strTmp = ".[ADD] - ASN ID / AusgangID : [" + Ausgang.ASN.ToString() + " / " + Ausgang.LAusgangID.ToString() + "]";
                            ListInfoAsnLfsInserted.Add(strTmp);
                            //Artikel eintragen
                            strSql = string.Empty;
                            for (Int32 i = 0; i <= listArtikelUpdate.Count - 1; i++)
                            {
                                decTmp = 0;
                                Decimal.TryParse(listArtikelUpdate[i].ToString(), out decTmp);
                                if (decTmp > 0)
                                {
                                    clsArtikel tmpArt = new clsArtikel();
                                    tmpArt._GL_User = this.GLUser;
                                    tmpArt.ID = decTmp;
                                    tmpArt.GetArtikeldatenByTableID();
                                    strSql = strSql + clsArtikelVita.AddArtikelLAusgangAutoSQL(this.GLUser, tmpArt.ID, Ausgang.LAusgangID);
                                    strSql = strSql + clsArtikelVita.AddArtikelLagermeldungen(this.GLUser, tmpArt.ID, enumLagerMeldungen.AML.ToString());
                                    myBeschreibung = "Artikel autom. zum Ausgang hinzugefügt: LVS-NR [" + tmpArt.LVS_ID.ToString() + "] / Ausgang [" + Ausgang.LAusgangID.ToString() + "]";
                                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), myBeschreibung);
                                }
                            }
                            strSql = strSql + clsArtikelVita.AddAuslagerungAutoSQL(this.GLUser.User_ID, Ausgang.LAusgangTableID, Ausgang.LAusgangID);
                            bool bOK = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "ArtikelVita", BenutzerID);
                        }
                    }
                }
            }
        }
        ///<summary>clsLagerdaten / AddEingangVDA4913</summary>
        ///<remarks></remarks>
        public void AddNEWEingangVDA4913(clsASN myASN)
        {
            //VDA - 
            //ASNField ID = 3  >>> Vereweis Datenempfänger
            //ASNField ID= 4 >>> Verweis Datensender (satz711F04)
            //ASNField ID =16 >>> satz712F04 (WERK)

            //Als Verweis verwenden wir satz711F04+'#'+satz712F04

            ListInfoAsnLfsInserted = new List<string>();
            ListError = new List<clsLogbuchCon>();

            clsASNValue asnValue = new clsASNValue();
            asnValue.GL_User = this.GLUser;
            asnValue.ASNID = myASN.ID;

            clsLEingang TmpEin = new clsLEingang();
            TmpEin._GL_User = this.GLUser;
            TmpEin.ASN = myASN.ID;
            //nicht abgeschlossen
            TmpEin.Checked = false;
            TmpEin.LEingangDate = DateTime.Now;
            TmpEin.SpedID = 0;
            TmpEin.DirektDelivery = false;
            TmpEin.Retoure = false;
            TmpEin.LagerTransport = false;
            TmpEin.WaggonNr = string.Empty;
            TmpEin.BeladeID = 0;
            TmpEin.EntladeID = 0;
            TmpEin.IsPrintDoc = false;
            TmpEin.IsPrintAnzeige = false;
            TmpEin.IsPrintLfs = false;
            DataTable dtASNValue = clsASNValue.GetASNValueDataTableByASNId(this.BenutzerID, myASN.ID);
            if (dtASNValue.Rows.Count > 0)
            {
                string VDAVerweisAuftraggeber = string.Empty; // asnValue.GetVerweisFromVDA4913(clsASNValue.const_VerweisAuftraggeber);
                string VDAVerweisVersender = string.Empty; //  asnValue.GetVerweisFromVDA4913(clsASNValue.const_VerweisVersender);
                string VDAVerweisEmpfaenger = string.Empty; //  asnValue.GetVerweisFromVDA4913(clsASNValue.const_VerweisEmpfaenger);
                Dictionary<string, clsADRVerweis> DictVerweis = clsADRVerweis.FillDictAdrVerweisAll(this.GLUser.User_ID, constValue_AsnArt.const_Art_VDA4913);

                List<clsArtikel> listArtikel = new List<clsArtikel>();
                clsLEingang AddEingang = new clsLEingang();
                clsArtikel AddArtikel = new clsArtikel();
                Int32 iCountEin = 0;
                Int32 iCountArt = 0;
                decimal decTmp = 0;

                string Receiver = string.Empty;
                string Sender = string.Empty;
                string Lieferant = string.Empty;
                string s711F05 = string.Empty;
                string s711F06 = string.Empty;
                string s712F14_TMS = string.Empty; //Transportmittelschlüssel

                //die Tabelle ASNValue als Schleife durchlaufen und die Eingänge erstellen
                for (Int32 i = 0; i <= dtASNValue.Rows.Count - 1; i++)
                {
                    Int32 iASNField = 0;
                    Int32.TryParse(dtASNValue.Rows[i]["ASNFieldID"].ToString(), out iASNField);

                    string strValue = dtASNValue.Rows[i]["Value"].ToString();
                    switch (iASNField)
                    {
                        //DAten Empfännger
                        case 3:
                            Receiver = strValue;
                            break;
                        case 4:
                            Sender = strValue;
                            break;
                        //DFÜ Alt
                        case 5:
                            s711F05 = strValue;
                            break;
                        //DFÜ Neu
                        case 6:
                            s711F06 = strValue;
                            break;
                        //Satz 712
                        case 13:
                            //Check ob auch 712
                            if (strValue == "712")
                            {
                                if (iCountArt > 0)
                                {
                                    listArtikel.Add(AddArtikel);
                                    iCountArt = 0;
                                }
                                if (iCountEin > 0)
                                {
                                    if (listArtikel.Count > 0)
                                    {
                                        InsertEingangANDArtikelDatenToDB(listArtikel);
                                        listArtikel = new List<clsArtikel>();
                                    }
                                    iCountEin = 0;
                                }
                                DictVerweis = clsADRVerweis.FillDictAdrVerweisAll(this.GLUser.User_ID, constValue_AsnArt.const_Art_VDA4913);
                                //neuer Lieferschein
                                this.Eingang = new clsLEingang();
                                this.Eingang = TmpEin;
                                this.Eingang.ASNRef = s711F05 + "/" + s711F06;
                                this.Eingang.AbBereichID = myASN.ArbeitsbereichID;
                                this.Eingang.MandantenID = myASN.MandantenID;
                                this.Eingang.IsPrintDoc = true;
                                iCountEin = iCountEin + 1;
                            }
                            break;
                        //SLB
                        case 15:
                            //TmpEin.ExTransportRef = strValue;
                            this.Eingang.ExTransportRef = strValue;
                            break;
                        //Lieferant
                        case 16:
                            Lieferant = strValue;

                            //Verweis Auftraggeber
                            //String für Auftraggeber: Empfänger#Sender#Lieferwerk
                            VDAVerweisAuftraggeber = Receiver + "#" + Sender + "#" + Lieferant;
                            foreach (KeyValuePair<string, clsADRVerweis> item in DictVerweis)
                            {
                                clsADRVerweis TmpVerw = item.Value;
                                if (string.Compare(TmpVerw.Verweis, VDAVerweisAuftraggeber, true) == 0)
                                {
                                    //TmpEin.Auftraggeber = TmpVerw.VerweisAdrID;
                                    this.Eingang.Auftraggeber = TmpVerw.VerweisAdrID;
                                    break;
                                }
                            }
                            if (TmpEin.Auftraggeber <= 0)
                            {
                                //Ein Datensatz in der Table ASN existiert aber es gibt keine entsprechenden 
                                //Daten in der Table ASNValue
                                clsLogbuchCon tmpLog = new clsLogbuchCon();
                                tmpLog.ID = 0;
                                tmpLog.Datum = DateTime.Now;
                                tmpLog.Typ = enumLogArtItem.ERROR.ToString();
                                tmpLog.LogText = "-> ASN ID: [" + myASN.ID.ToString() + "] -> SENDER - Verweis fehlt -> keine Zuordnung möglich...";
                                ListError.Add(tmpLog);
                                //Schleife beenden
                                i = dtASNValue.Rows.Count;
                            }
                            else
                            {
                                //Verweise für diesen SENDER laden
                                DictVerweis.Clear();
                                DictVerweis = clsADRVerweis.FillDictAdrVerweisBySender(TmpEin.Auftraggeber, this.GLUser.User_ID);
                                if (DictVerweis.Count == 0)
                                {
                                    //Ein Datensatz in der Table ASN existiert aber es gibt keine entsprechenden 
                                    //Daten in der Table ASNValue
                                    clsLogbuchCon tmpLog = new clsLogbuchCon();
                                    tmpLog.ID = 0;
                                    tmpLog.Datum = DateTime.Now;
                                    tmpLog.Typ = enumLogArtItem.ERROR.ToString();
                                    tmpLog.LogText = "-> ASN ID: [" + myASN.ID.ToString() + "] / Auftraggeber[" + TmpEin.Auftraggeber.ToString() + "] -> Es liegen keine Verweise vor...";
                                    ListError.Add(tmpLog);
                                }
                                else
                                {
                                    //Verweis Versender / Lieferant => Sender#Lieferant
                                    VDAVerweisVersender = Sender + "#" + Lieferant;
                                    foreach (KeyValuePair<string, clsADRVerweis> item in DictVerweis)
                                    {
                                        clsADRVerweis TmpVerw = item.Value;
                                        //Adressverweis
                                        if (TmpVerw.Verweis == VDAVerweisVersender)
                                        {
                                            //TmpEin.Versender = TmpVerw.VerweisAdrID;
                                            this.Eingang.Versender = TmpVerw.VerweisAdrID;
                                        }
                                    }
                                }
                            }
                            break;
                        //-s712F14 -> Transportmittelschlüssel
                        case 26:
                            s712F14_TMS = strValue;
                            break;

                        //KFZ
                        case 27:
                            //TmpEin.KFZ = strValue;
                            switch (s712F14_TMS)
                            {
                                case "01":
                                    if (this.GLSystem.Modul_VDA_Use_KFZ)
                                    { this.Eingang.KFZ = strValue; } // TEST EVENTUELL WERDEN MELDUNGEN AUF BASIS DESSEN NICHT KORREKT ERZEUGT ;
                                    break;
                                case "08":
                                    if (strValue.Length == 12)
                                    {
                                        this.Eingang.WaggonNr = strValue.Substring(0, 4) + " " + strValue.Substring(4, 4) + " " + strValue.Substring(8, 3) + "-" + strValue.Substring(11);
                                    }
                                    else
                                    {
                                        string tmpWaggonNo = strValue.Replace(" ", "");
                                        if (tmpWaggonNo.Length == 12)
                                        {
                                            this.Eingang.WaggonNr = tmpWaggonNo.Substring(0, 4) + " " + tmpWaggonNo.Substring(4, 4) + " " + tmpWaggonNo.Substring(8, 3) + "-" + tmpWaggonNo.Substring(11);
                                        }
                                        else
                                        {
                                            this.Eingang.WaggonNr = strValue;
                                        }
                                    }
                                    break;
                            }
                            s712F14_TMS = string.Empty;
                            break;
                        //Lieferscheinnummer
                        case 36:
                            this.Eingang.LEingangLfsNr = strValue;
                            break;
                        //Auftrag/Abschluss/BEstellnummer
                        case 41:
                            this.Eingang.ExAuftragRef = strValue;
                            break;
                        case 46:
                            VDAVerweisEmpfaenger = Sender + "#" + Lieferant + "#" + strValue;
                            foreach (KeyValuePair<string, clsADRVerweis> item in DictVerweis)
                            {
                                clsADRVerweis TmpVerw = item.Value;
                                if (TmpVerw.Verweis == VDAVerweisEmpfaenger)
                                {
                                    //TmpEin.Empfaenger = TmpVerw.VerweisAdrID;
                                    this.Eingang.Empfaenger = TmpVerw.VerweisAdrID;
                                }
                            }
                            break;
                        //Artikel
                        case 55:
                            if (strValue == "714")
                            {
                                if (iCountArt > 0)
                                {
                                    listArtikel.Add(AddArtikel);
                                    iCountArt = 0;
                                }
                                iCountArt = iCountArt + 1;
                                AddArtikel = new clsArtikel();
                                AddArtikel._GL_User = this.GLUser;
                                AddArtikel.AuftragID = 0;
                                AddArtikel.AuftragPos = 0;
                                AddArtikel.AuftragPosTableID = 0;
                                AddArtikel.LVS_ID = 0;   //wird beim Add ermittelt
                                AddArtikel.EingangChecked = false;
                                AddArtikel.AusgangChecked = false;
                                AddArtikel.UB_AltCalcAuslagerung = false;
                                AddArtikel.IsLagerArtikel = true;
                                AddArtikel.AbBereichID = this.Eingang.AbBereichID;
                                AddArtikel.MandantenID = this.Eingang.MandantenID;
                                AddArtikel.LEingangTableID = 0;
                                AddArtikel.LAusgangTableID = 0;
                                AddArtikel.FreigabeAbruf = false;
                                AddArtikel.exAuftrag = this.Eingang.ExAuftragRef;
                            }
                            break;
                        //materialnummer
                        case 58:
                            AddArtikel.exMaterialnummer = strValue;
                            //Ermitteln der Güte und Abmessungen
                            Tmp.clsTmpVerwTWB tmp = new Tmp.clsTmpVerwTWB();
                            tmp.GL_User = this.GLUser;
                            Int32 iTmp = 0;
                            Int32.TryParse(strValue, out iTmp);
                            tmp.SAP = iTmp;
                            tmp.FillBySAP();

                            AddArtikel.Dicke = tmp.Dicke;
                            AddArtikel.Breite = tmp.Breite;
                            AddArtikel.Laenge = tmp.Laenge;
                            AddArtikel.Guete = tmp.Guete;
                            AddArtikel.GArtID = tmp.GArtID;
                            break;
                        //Brutto
                        case 60:
                            decTmp = 0;
                            decimal.TryParse(strValue, out decTmp);
                            AddArtikel.Brutto = decTmp / 1000;
                            AddArtikel.Einheit = "KG";
                            break;
                        //netto
                        case 62:
                            decTmp = 0;
                            decimal.TryParse(strValue, out decTmp);
                            AddArtikel.Netto = decTmp / 1000;
                            AddArtikel.Einheit = "KG";
                            break;
                        //Pos
                        case 66:
                            AddArtikel.Position = strValue;
                            break;
                        //charge
                        case 68:
                            AddArtikel.Charge = "";
                            AddArtikel.Produktionsnummer = strValue;

                            //ArtikelIDRef
                            clsADR adr = new clsADR();
                            adr._GL_User = this.GLUser;
                            adr.ID = this.Eingang.Auftraggeber;
                            adr.Fill();
                            AddArtikel.ArtIDRef = AddArtikel.Produktionsnummer + "#" + adr.KD_ID.ToString();
                            break;
                        //gef. Stoffe -> für LZZ
                        case 70:
                            Int32 iYear = 1900;
                            Int32 iKW = 1;
                            if (strValue.Length >= 6)
                            {
                                string strYear = string.Empty;
                                string strKW = string.Empty;
                                strYear = strValue.Substring(0, 4);
                                Int32.TryParse(strYear, out iYear);
                                strKW = strValue.Substring(4, strValue.Length - 4);
                                Int32.TryParse(strKW, out iKW);

                            }
                            AddArtikel.LZZ = Functions.GetDateFromLastDayOfCalWeek(iKW, iYear);
                            break;
                        //Satz 719
                        case 123:
                            //Hier für den letzten druchlauf, iCountEin ist = 1 und damit muss der 
                            //Eingang eingetragen werden
                            if (iCountArt > 0)
                            {
                                listArtikel.Add(AddArtikel);
                                iCountArt = 0;
                            }

                            if (iCountEin > 0)
                            {
                                if (listArtikel.Count > 0)
                                {
                                    InsertEingangANDArtikelDatenToDB(listArtikel);
                                    listArtikel = new List<clsArtikel>();
                                }
                                iCountEin = 0;
                            }
                            break;

                    }

                }
            }

        }
        ///<summary>clsLagerdaten / UpdateASNTableSenderAndReceiver</summary>
        ///<remarks></remarks>
        private void UpdateASNTableSenderAndReceiver(ref DataTable dt, decimal mySender, decimal myReceiver, decimal myASNNr)
        {
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decTmpASN = 0;
                Decimal.TryParse(dt.Rows[i]["ASNID"].ToString(), out decTmpASN);
                if (decTmpASN > 0)
                {
                    //Check gleiche ASN
                    if (myASNNr == decTmpASN)
                    {
                        dt.Rows[i]["ASNSender"] = mySender;
                        dt.Rows[i]["ASNReceiver"] = myReceiver;
                    }
                }
            }
        }
        ///<summary>clsLagerdaten / AddEingangVDA4913</summary>
        ///<remarks></remarks>
        public DataTable GetLfsKopfdaten(ref DataTable dtASN)
        {
            ASNSender = 0;
            ASNReceiver = 0;

            DataTable dtEingang = new DataTable("Eingang");
            clsLEingang Eingang = new clsLEingang();
            dtEingang = clsLEingang.GetLEingangTableColumnSchema(this.GLUser);

            //zusaätziche Felder für die Übersicht 
            dtEingang.Columns.Add("Select", typeof(bool));
            dtEingang.Columns.Add("ASN-Datum", typeof(DateTime));
            dtEingang.Columns.Add("Ref.Auftraggeber", typeof(string));
            dtEingang.Columns.Add("Ref.Empfaenger", typeof(string));
            dtEingang.Columns.Add("TransportNr", typeof(string));
            dtEingang.Columns.Add("VS-Datum", typeof(DateTime));
            dtEingang.Columns.Add("AuftraggeberView", typeof(string));
            dtEingang.Columns.Add("EmpfaengerView", typeof(string));
            dtEingang.Columns.Add("Transportmittel", typeof(string));
            dtEingang.Columns.Add("Lieferantennummer", typeof(string));
            dtEingang.Columns.Add("Log", typeof(string));
            //dtEingang.Columns.Add("ParentID", typeof(string));

            dtEingang.Columns["Select"].SetOrdinal(0);
            dtEingang.Columns["ASN"].SetOrdinal(1);

            clsADRMan adrManTmp = new clsADRMan();
            if (dtASN.Rows.Count > 0)
            {
                //Liste der verschiedenen Eingägne erstellen
                DataTable dtASNID = dtASN.DefaultView.ToTable(true, "ASNID");
                for (Int32 i = 0; i <= dtASNID.Rows.Count - 1; i++)
                {
                    DataRow row = dtEingang.NewRow();

                    string asnIDTmp = dtASNID.Rows[i]["ASNID"].ToString();
                    decimal decASNID = 0;
                    Decimal.TryParse(asnIDTmp, out decASNID);
                    row["Select"] = false;
                    row["ASN"] = decASNID;


                    dtASN.DefaultView.RowFilter = string.Empty;
                    dtASN.DefaultView.RowFilter = "ASNID=" + asnIDTmp;
                    DataTable dtASNValue = dtASN.DefaultView.ToTable();
                    //Table mit den XMLDaten aus der Message

                    //bool bTRAN_PART = false;
                    //bool bLS_PART = false;
                    bool bIsRead16 = false;
                    bool bIsRead46 = false;
                    string strLastLfsNr = string.Empty;

                    for (Int32 x = 0; x <= dtASNValue.Rows.Count - 1; x++)
                    {
                        string knot = dtASNValue.Rows[x]["FieldName"].ToString();
                        string Value = dtASNValue.Rows[x]["Value"].ToString();


                        Int32 iASNField = 0;
                        Int32.TryParse(dtASNValue.Rows[x]["ASNFieldID"].ToString(), out iASNField);
                        string strKennung = dtASNValue.Rows[x]["Kennung"].ToString();
                        //string strValue = dtASNValue.Rows[i]["Value"].ToString();
                        switch (strKennung)
                        {
                            case clsASN.const_VDA4913SatzField_SATZ711F01:
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ711F02:
                                break;
                            //case 3:
                            case clsASN.const_VDA4913SatzField_SATZ711F03:
                                row["Ref.Empfaenger"] = Value;
                                break;

                            //DAten Empfännger
                            //case 4:
                            case clsASN.const_VDA4913SatzField_SATZ711F04:
                                row["Ref.Auftraggeber"] = Value + "#" + row["Ref.Empfaenger"];
                                row["Ref.Empfaenger"] += "#" + Value;
                                break;
                            //Übertragungsnummer alt
                            case clsASN.const_VDA4913SatzField_SATZ711F05:
                                row["ASNRef"] = Value;
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ711F06:
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ711F07:
                                //Functions.GetDateFromStringVDA(Value);
                                DateTime dtASNDate = DateTime.ParseExact(Value, "yyMMdd", System.Globalization.CultureInfo.InvariantCulture); // CF hh -> HH
                                row["ASN-Datum"] = dtASNDate;
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ711F08:
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ711F09:
                                break;

                            //SLB NR
                            case clsASN.const_VDA4913SatzField_SATZ712F03:
                                row["ExTransportRef"] = Value;
                                break;
                            //case 16:
                            case clsASN.const_VDA4913SatzField_SATZ712F04:
                                if (!bIsRead16)
                                {
                                    // TEST
                                    string tmp = row["Ref.Auftraggeber"].ToString();
                                    clsADRVerweis adrverweis = new clsADRVerweis();
                                    adrverweis.FillClassByVerweis(row["Ref.Auftraggeber"].ToString(), constValue_AsnArt.const_Art_VDA4913);
                                    if ((adrverweis.ID == 0) || (adrverweis.UseS712F04))
                                    {
                                        row["Ref.Auftraggeber"] += "#" + Value;
                                        tmp = row["Ref.Auftraggeber"].ToString();
                                        adrverweis.FillClassByVerweis(row["Ref.Auftraggeber"].ToString(), constValue_AsnArt.const_Art_VDA4913);
                                    }
                                    row["Auftraggeber"] = adrverweis.VerweisAdrID;
                                    clsADR ADR = new clsADR();
                                    ADR._GL_User = this.GLUser;
                                    ADR.ID = adrverweis.VerweisAdrID;
                                    ASNSender = adrverweis.VerweisAdrID;
                                    ADR.Fill();
                                    row["Lieferantennummer"] = adrverweis.LieferantenVerweis;
                                    row["AuftraggeberView"] = ADR.ViewID;
                                    bIsRead16 = true;
                                }
                                break;
                            //Transportmittel Schlüssel
                            case clsASN.const_VDA4913SatzField_SATZ712F14:
                                row["Transportmittel"] = Value.ToString();
                                break;

                            //Transportmittel - Nummer
                            case clsASN.const_VDA4913SatzField_SATZ712F15:
                                string strF712F15 = row["Transportmittel"].ToString();
                                string strValue = Value.ToString();
                                ediHelper_712_TM tmp712TM = new ediHelper_712_TM(strF712F15, strValue);

                                row["KFZ"] = string.Empty;
                                row["WaggonNo"] = string.Empty;
                                row["IsWaggon"] = false;
                                row["Ship"] = string.Empty;
                                row["IsShip"] = false;

                                switch (tmp712TM.TMS)
                                {
                                    case "01":
                                        if (this.GLSystem.Modul_VDA_Use_KFZ)
                                        {
                                            row["KFZ"] = tmp712TM.VehicleNo;
                                        }
                                        break;
                                    case "08":
                                        row["WaggonNo"] = tmp712TM.VehicleNo;
                                        row["IsWaggon"] = true;
                                        break;
                                    case "11":
                                        row["Ship"] = row["WaggonNo"] = tmp712TM.VehicleNo; ;
                                        row["IsShip"] = true;
                                        break;
                                }
                                break;

                            //Lieferscheinnummer
                            case clsASN.const_VDA4913SatzField_SATZ713F03:
                                //Check auf Lieferscheinnummer, wenn neue Lieferscheinnummer, dann neuen Eingang
                                row["LfsNr"] = Value;
                                strLastLfsNr = Value;
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ713F04:
                                DateTime dtVSDate = DateTime.ParseExact(Value, "yyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                                row["VS-Datum"] = dtVSDate;
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ713F05:
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ713F06:
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ713F07:
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ713F08:
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ713F09:
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ713F10:
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ713F11:
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ713F12:
                                break;

                            case clsASN.const_VDA4913SatzField_SATZ713F13:
                                if (!bIsRead46)
                                {
                                    string tmp = row["Ref.Empfaenger"].ToString();
                                    clsADRVerweis adrverweisE = new clsADRVerweis();
                                    adrverweisE.FillClassByVerweis(row["Ref.Empfaenger"].ToString(), constValue_AsnArt.const_Art_VDA4913);
                                    if ((adrverweisE.ID == 0) || (adrverweisE.UseS713F13))
                                    {
                                        row["Ref.Empfaenger"] += "#" + Value;
                                        tmp = row["Ref.Empfaenger"].ToString();
                                        adrverweisE.FillClassByVerweis(row["Ref.Empfaenger"].ToString(), constValue_AsnArt.const_Art_VDA4913);
                                    }
                                    //clsADRVerweis adrverweisE = new clsADRVerweis();
                                    //adrverweisE.FillClassByVerweis(row["Ref.Empfaenger"].ToString(), clsASN.const_ASNFiledTyp_VDA4913);
                                    row["Empfaenger"] = adrverweisE.VerweisAdrID;
                                    clsADR ADRE = new clsADR();
                                    ADRE._GL_User = this.GLUser;
                                    ADRE.ID = adrverweisE.VerweisAdrID;
                                    ASNReceiver = adrverweisE.VerweisAdrID;
                                    ADRE.Fill();
                                    row["EmpfaengerView"] = ADRE.ViewID;
                                    bIsRead46 = true;
                                }
                                break;
                        }

                        //im letzten Durchlauf die Row der Talbe EIngang hinzufügen
                        if (x == dtASNValue.Rows.Count - 1)
                        {
                            dtEingang.Rows.Add(row);
                            UpdateASNTableSenderAndReceiver(ref dtASN, ASNSender, ASNReceiver, decASNID);
                        }
                    }

                }
            }
            return dtEingang;
        }
        ///<summary>clsLagerdaten / GetArtikelDaten1</summary>
        ///<remarks></remarks>
        public DataTable GetArtikelDaten1(ref DataTable dtASN)
        {
            //int iCount714 = 0;
            //int iCount715 = 0;
            //int iCount716 = 0;
            //int iCount717 = 0;
            //int iCount718 = 0;

            if (this.Sys.AbBereich.DefaultValue is clsArbeitsbereichDefaultValue)
            { }
            else
            {
                this.Sys.AbBereich.DefaultValue = new clsArbeitsbereichDefaultValue();
                this.Sys.AbBereich.DefaultValue.InitCls(this.Sys.AbBereich.ID);
            }

            Int32 iCountRow4 = dtASN.Rows.Count;

            ListCreatedNewGArten = new List<string>();
            clsADRMan adrManTmp = new clsADRMan();

            DataTable dtArtikel = new DataTable();
            dtArtikel = clsArtikel.GetDataTableArtikelSchema(this.GLUser);
            dtArtikel.Columns.Add("Gut", typeof(string));
            dtArtikel.Columns.Add("ASN", typeof(decimal));
            dtArtikel.Columns.Add("LfsNr", typeof(string));
            dtArtikel.Columns.Add("ChildID", typeof(string));
            dtArtikel.Columns.Add("TMS", typeof(string));
            dtArtikel.Columns.Add("VehicleNo", typeof(string));
            //dtArtikel.Columns.Add("Guete", typeof(string));
            Int32 j = 0;
            dtArtikel.Columns["ASN"].SetOrdinal(j);

            if (dtASN.Rows.Count > 0)
            {
                //DataTable dtASNID = dtASN.DefaultView.ToTable(true, "ASNID");
                dtASN.DefaultView.RowFilter = string.Empty;
                //dtASN.DefaultView.Sort = "ID";
                DataTable dtASNID = dtASN.DefaultView.ToTable(true, "ASNID", "ASNSender", "ASNReceiver");
                for (Int32 i = 0; i <= dtASNID.Rows.Count - 1; i++)
                {
                    Int32 iCountArt = 0;
                    DataRow row = dtArtikel.NewRow();
                    string asnIDTmp = dtASNID.Rows[i]["ASNID"].ToString();
                    decimal decTmp = 0;
                    decimal decTmpASN = 0;
                    Decimal.TryParse(asnIDTmp, out decTmpASN);
                    row["ASN"] = decTmpASN;
                    row["ChildID"] = ((Int32)decTmpASN).ToString();

                    Int32 iCountRow3 = dtASN.Rows.Count;
                    //dtASN.DefaultView.Sort = "ID";
                    dtASN.DefaultView.Sort = "LfdNr";
                    dtASN.DefaultView.RowFilter = string.Empty;
                    dtASN.DefaultView.RowFilter = "ASNID=" + asnIDTmp;

                    decimal decASNSender = 0;
                    decimal.TryParse(dtASNID.Rows[i]["ASNSender"].ToString(), out decASNSender);
                    decimal decASNReceiver = 0;
                    decimal.TryParse(dtASNID.Rows[i]["ASNReceiver"].ToString(), out decASNReceiver);

                    DataTable dtASNValue = dtASN.DefaultView.ToTable();

                    Int32 iCountRow1 = dtASN.Rows.Count;
                    Int32 iCountRow = dtASNValue.Rows.Count;

                    //Table mit den XMLDaten aus der Message
                    Int32 iTmp = 0;
                    bool bTRAN_PART = false;
                    bool bLS_PART = false;

                    string ZS_Bestellnummer = string.Empty;
                    string strVerweisE = string.Empty;
                    string strVerweisA = string.Empty;
                    clsADR tmpAdrA = new clsADR();
                    tmpAdrA._GL_User = this.GLUser;
                    tmpAdrA.ID = decASNSender;
                    tmpAdrA.FillClassOnly();

                    clsADR tmpAdrE = new clsADR();
                    tmpAdrE._GL_User = this.GLUser;
                    tmpAdrE.ID = decASNReceiver;
                    tmpAdrE.FillClassOnly();

                    bool bIsRead16 = false;
                    bool bIsRead46 = false;
                    string strLastLfsNr = string.Empty;
                    string strTMS = string.Empty;
                    string strVehicleNo = string.Empty;

                    Int32 iArtikelPos = 0;
                    clsASNArtFieldAssignment ASNArtFieldAssign = new clsASNArtFieldAssignment();
                    ASNArtFieldAssign._GL_User = this.GLUser;
                    Dictionary<string, clsASNArtFieldAssignment> DictASNArtFieldAssignment = ASNArtFieldAssign.GetArtikelFieldAssignment(decASNSender, decASNReceiver, (int)this.Sys.AbBereich.ID);
                    Dictionary<string, clsASNArtFieldAssignment> DictASNArtFieldAssCopyFieldValue = ASNArtFieldAssign.GetArtikelFieldAssignmentCopyFields(decASNSender, decASNReceiver, (int)this.Sys.AbBereich.ID);

                    clsASNTableCombiValue ASNTableCombiVal = new clsASNTableCombiValue();
                    ASNTableCombiVal.InitClass(this.GLUser, this.GLSystem);
                    Dictionary<string, clsASNTableCombiValue> DictASNTableCombiValue = ASNTableCombiVal.GetArtikelFieldAssignment(decASNSender, decASNReceiver);

                    //Zur Zwischenspeicherung der Bestell / Auftragsnummer, um diese im Artikel zu hinterlegen
                    Dict713F10OrderID = new Dictionary<string, string>();

                    //Zur Zwischenspeicherung der Lfs / VehicleNlo , um diese im Artikel zu hinterlegen
                    Dict712_Transportmittel = new Dictionary<string, ediHelper_712_TM>();


                    clsArtikel tmpArtZS = new clsArtikel();
                    clsArtikel AddArtikel = new clsArtikel();
                    AddArtikel.InitClass(this.GLUser, this.GLSystem);
                    AddArtikel.sys = this.Sys;
                    AddArtikel.GlowDate = new DateTime(1900, 1, 1);

                    int iGlobalArtCount = GlobalFieldVal_ArticleCountInEdi.Check(ref DictASNArtFieldAssignment, dtASNValue);

                    //Dictionary<string, string> dict713ZS = new Dictionary<string, string>();
                    for (Int32 x = 0; x <= dtASNValue.Rows.Count - 1; x++)
                    {
                        //prüfen, ob Datenfelder speziell zugewiesen wurden 
                        Int32 iASNField = 0;
                        Int32.TryParse(dtASNValue.Rows[x]["ASNFieldID"].ToString(), out iASNField);
                        string strKennung = dtASNValue.Rows[x]["Kennung"].ToString();
                        string Value = dtASNValue.Rows[x]["Value"].ToString();

                        //Bestellnummer
                        if (strKennung.Equals("SATZ718F04"))
                        {
                            string str = string.Empty;
                        }

                        if (DictASNArtFieldAssignment.TryGetValue(strKennung, out ASNArtFieldAssign))
                        {
                            SetASNArtFieldAssignment(ASNArtFieldAssign.ArtField, ref AddArtikel, ref ASNArtFieldAssign, Value, iArtikelPos, false);

                            //Bestellnummer
                            if (strKennung.Equals("SATZ713F08"))
                            {
                                ZS_Bestellnummer = Value;
                                AddToDictOrderID(ref AddArtikel, ZS_Bestellnummer);
                            }
                            if ((ASNArtFieldAssign != null) && (ASNArtFieldAssign.ArtField.Equals("Artikel.Produktionsnummer")))
                            {
                                AddToDictOrderID(ref AddArtikel, ZS_Bestellnummer);
                            }

                            if (CheckForGArtVerweis(strKennung))
                            {
                                if (this.Sys.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_Honeselmann + "_"))
                                {
                                    Tmp.clsTmpVerwTWB tmp = new Tmp.clsTmpVerwTWB();
                                    tmp.GL_User = this.GLUser;
                                    Int32 iTmp2 = 0;
                                    Int32.TryParse(Value, out iTmp2);
                                    tmp.SAP = iTmp2;
                                    tmp.FillBySAP();

                                    AddArtikel.Dicke = tmp.Dicke;
                                    AddArtikel.Breite = tmp.Breite;
                                    AddArtikel.Laenge = tmp.Laenge;
                                    AddArtikel.Guete = tmp.Guete;
                                    AddArtikel.GArtID = tmp.GArtID;
                                }
                                else
                                {
                                    AddArtikel.GArtID = clsGut.GetGutByADRAndVerweis(this.GLUser, tmpAdrA, AddArtikel.Werksnummer, this.Sys.AbBereich.ID);
                                    AddArtikel.Dicke = 0;
                                    AddArtikel.Breite = 0;
                                    AddArtikel.Laenge = 0;
                                    AddArtikel.Hoehe = 0;
                                    if (this.Sys.Client.Modul.ASN_AutoCreateNewGArtByASN)
                                    {
                                        if (AddArtikel.GArtID == 1)
                                        {
                                            string strTxt = string.Empty;
                                            if (AddArtikel.GArt.CreateNewGArtByASN(decASNSender, Value))
                                            {
                                                AddArtikel.GArtID = AddArtikel.GArt.ID;
                                                //Güterart wurde erfolgreich angelegt
                                                strTxt = "NEU -> Güterart ID[" + AddArtikel.GArt.ID.ToString() + "] - Matchcode [" + AddArtikel.GArt.ViewID + "]";
                                            }
                                            else
                                            {
                                                //Güterart konnte nicht angelegt werden
                                                strTxt = "Achtung - Güterart konnte nicht erstellt werden!";
                                            }
                                            ListCreatedNewGArten.Add(strTxt);
                                        }
                                    }
                                    if (
                                        (AddArtikel.GArtID > 1) &&
                                        (this.Sys.Client.Modul.ASN_VDA4913_LVS_ReadASN_TakeOverGArtValues)
                                       )
                                    {
                                        try
                                        {
                                            SetASNGArtValue(ref AddArtikel);
                                        }
                                        catch (Exception ex)
                                        {
                                            string str = ex.ToString();
                                        }
                                    }

                                }
                            }
                        }
                        else
                        {
                            switch (strKennung)
                            {
                                //case 3:
                                case clsASN.const_VDA4913SatzField_SATZ711F03:
                                    strVerweisE = Value;
                                    break;
                                //DAten Empfännger
                                //case 4:
                                case clsASN.const_VDA4913SatzField_SATZ711F04:
                                    strVerweisA = Value + "#" + strVerweisE;
                                    strVerweisE += "#" + Value;
                                    break;

                                //case 16:
                                case clsASN.const_VDA4913SatzField_SATZ712F04:
                                    if (!bIsRead16)
                                    {
                                        clsADRVerweis adrverweis = new clsADRVerweis();
                                        adrverweis.FillClassByVerweis(strVerweisA, constValue_AsnArt.const_Art_VDA4913);
                                        if ((adrverweis.ID == 0) || (adrverweis.UseS713F13))
                                        {
                                            strVerweisA += "#" + Value;
                                            adrverweis.FillClassByVerweis(strVerweisA, constValue_AsnArt.const_Art_VDA4913);
                                        }

                                        tmpAdrA = new clsADR();
                                        tmpAdrA._GL_User = this.GLUser;
                                        tmpAdrA.ID = adrverweis.VerweisAdrID;
                                        tmpAdrA.Fill();
                                        bIsRead16 = true;
                                    }
                                    break;

                                // TMS
                                case clsASN.const_VDA4913SatzField_SATZ712F14:
                                    strTMS = Value;
                                    //AddArtikel.ASN_TMS = strTMS;
                                    break;
                                // VehicleNo
                                case clsASN.const_VDA4913SatzField_SATZ712F15:
                                    strVehicleNo = Value;
                                    //AddArtikel.ASN_VehicleNo = strVehicleNo;
                                    break;

                                //Lieferscheinnummer
                                case clsASN.const_VDA4913SatzField_SATZ713F03:
                                    ////Check auf Lieferscheinnummer, wenn neue Lieferscheinnummer, dann neuen Eingang
                                    if (!strLastLfsNr.Equals(string.Empty))
                                    {
                                        if (!strLastLfsNr.Equals(Value))
                                        {
                                            //Check Zuweisung Bestellnummer in Customized Artikelfeld
                                            CheckFor713F10OrderIDValue(ref AddArtikel, ref DictASNArtFieldAssignment);
                                            //Prüfen, ob Defaultvalue für Datenfelder vorliegt und entsprechend ändern
                                            if (this.Sys.AbBereich.DefaultValue.DictArbeitsbereichDefaultValue.Count > 0)
                                            {
                                                this.Sys.AbBereich.DefaultValue.SetDefaultValue(ref AddArtikel);
                                            }
                                            //Prüfen ob Datenfelder zusammengelegt / combiniert werden müssen
                                            if (DictASNTableCombiValue.Count > 0)
                                            {
                                                SetASNColCombiValue(ref AddArtikel, ref DictASNTableCombiValue);
                                            }
                                            SetRowValue(ref row, ref dtArtikel, AddArtikel.Copy(), decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);
                                            iCountArt = 0;
                                            tmpArtZS = new clsArtikel();
                                            tmpArtZS = AddArtikel.Copy();
                                            AddArtikel = new clsArtikel();
                                            AddArtikel.AbBereichID = this.Sys.AbBereich.ID;
                                            AddArtikel._GL_User = this.GLUser;
                                            AddArtikel.AuftragID = 0;
                                            AddArtikel.AuftragPos = 0;
                                            AddArtikel.AuftragPosTableID = 0;
                                            AddArtikel.LVS_ID = 0;
                                            AddArtikel.EingangChecked = false;
                                            AddArtikel.AusgangChecked = false;
                                            AddArtikel.UB_AltCalcAuslagerung = false;
                                            AddArtikel.IsLagerArtikel = true;
                                            AddArtikel.LEingangTableID = 0;
                                            AddArtikel.LAusgangTableID = 0;
                                            AddArtikel.FreigabeAbruf = false;
                                            //AddArtikel.Bestellnummer = ZS_Bestellnummer;
                                            AddArtikel.exAuftrag = string.Empty;
                                            AddArtikel.exAuftragPos = string.Empty;
                                            AddArtikel.GlowDate = new DateTime(1900, 1, 1);
                                            CheckForCopyToNewArtikelValue(ref tmpArtZS, ref AddArtikel, ref DictASNArtFieldAssignment);

                                        }
                                    }

                                    iArtikelPos = 1;
                                    AddArtikel.Position = iArtikelPos.ToString();

                                    row["LfsNr"] = Value;
                                    strLastLfsNr = Value;

                                    AddToDict712_TM(strTMS, strVehicleNo, strLastLfsNr);
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ713F08:
                                    ZS_Bestellnummer = string.Empty;
                                    ZS_Bestellnummer = Value;
                                    AddArtikel.Bestellnummer = Value;
                                    break;

                                case clsASN.const_VDA4913SatzField_SATZ713F13:
                                    if (!bIsRead46)
                                    {
                                        clsADRVerweis adrverweisE = new clsADRVerweis();
                                        adrverweisE.FillClassByVerweis(strVerweisE, constValue_AsnArt.const_Art_VDA4913);
                                        if ((adrverweisE.ID == 0) || (adrverweisE.UseS713F13))
                                        {
                                            strVerweisE += "#" + Value;
                                            adrverweisE.FillClassByVerweis(strVerweisE, constValue_AsnArt.const_Art_VDA4913);
                                        }
                                        tmpAdrE = new clsADR();
                                        tmpAdrE._GL_User = this.GLUser;
                                        tmpAdrE.ID = adrverweisE.VerweisAdrID;
                                        tmpAdrE.Fill();
                                        bIsRead46 = true;
                                    }
                                    break;

                                //case 55:
                                case clsASN.const_VDA4913SatzField_SATZ714F01:
                                    if (Value == "714")
                                    {
                                        if (iCountArt > 0)
                                        {
                                            //Check Zuweisung Bestellnummer in Customized Artikelfeld
                                            CheckFor713F10OrderIDValue(ref AddArtikel, ref DictASNArtFieldAssignment);

                                            //Prüfen, ob Defaultvalue für Datenfelder vorliegt und entsprechend ändern
                                            if (this.Sys.AbBereich.DefaultValue.DictArbeitsbereichDefaultValue.Count > 0)
                                            {
                                                this.Sys.AbBereich.DefaultValue.SetDefaultValue(ref AddArtikel);
                                            }
                                            //Prüfen ob Datenfelder zusammengelegt / combiniert werden müssen
                                            if (DictASNTableCombiValue.Count > 0)
                                            {
                                                SetASNColCombiValue(ref AddArtikel, ref DictASNTableCombiValue);
                                            }

                                            //--- mr 2024_06_04
                                            //SetRowValue(ref row, ref dtArtikel, AddArtikel.Copy(), decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);
                                            if (iGlobalArtCount > 0)
                                            {
                                                if (iCountArt <= iGlobalArtCount)
                                                {
                                                    SetRowValue(ref row, ref dtArtikel, AddArtikel.Copy(), decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);
                                                }
                                            }
                                            else
                                            {
                                                SetRowValue(ref row, ref dtArtikel, AddArtikel.Copy(), decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);
                                            }

                                            iArtikelPos++;
                                            tmpArtZS = new clsArtikel();
                                            tmpArtZS = AddArtikel.Copy();

                                            AddArtikel = new clsArtikel();
                                            AddArtikel.AbBereichID = this.Sys.AbBereich.ID;
                                            AddArtikel.Position = iArtikelPos.ToString();
                                            AddArtikel._GL_User = this.GLUser;
                                            AddArtikel.AuftragID = 0;
                                            AddArtikel.AuftragPos = 0;
                                            AddArtikel.AuftragPosTableID = 0;
                                            AddArtikel.LVS_ID = 0;
                                            AddArtikel.EingangChecked = false;
                                            AddArtikel.AusgangChecked = false;
                                            AddArtikel.UB_AltCalcAuslagerung = false;
                                            AddArtikel.IsLagerArtikel = true;
                                            AddArtikel.LEingangTableID = 0;
                                            AddArtikel.LAusgangTableID = 0;
                                            AddArtikel.FreigabeAbruf = false;
                                            AddArtikel.GlowDate = new DateTime(1900, 1, 1);
                                            AddArtikel.Guete = string.Empty;
                                            CheckForCopyToNewArtikelValue(ref tmpArtZS, ref AddArtikel, ref DictASNArtFieldAssignment);
                                        }
                                        iCountArt++;
                                    }
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ714F02:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ714F03:
                                    AddArtikel.Produktionsnummer = Value;
                                    if (CheckForGArtVerweis(strKennung))
                                    {
                                        if (this.Sys.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_Honeselmann + "_"))
                                        {
                                            Tmp.clsTmpVerwTWB tmp = new Tmp.clsTmpVerwTWB();
                                            tmp.GL_User = this.GLUser;
                                            Int32 iTmp2 = 0;
                                            Int32.TryParse(Value, out iTmp2);
                                            tmp.SAP = iTmp2;
                                            tmp.FillBySAP();

                                            AddArtikel.Dicke = tmp.Dicke;
                                            AddArtikel.Breite = tmp.Breite;
                                            AddArtikel.Laenge = tmp.Laenge;
                                            AddArtikel.Guete = tmp.Guete;
                                            AddArtikel.GArtID = tmp.GArtID;
                                        }
                                        else
                                        {
                                            AddArtikel.GArtID = clsGut.GetGutByADRAndVerweis(this.GLUser, tmpAdrA, AddArtikel.Produktionsnummer, this.Sys.AbBereich.ID);
                                            //--testMR
                                            //AddArtikel.GArt.ID = AddArtikel.GArtID;
                                            //AddArtikel.GArt.Fill();
                                            AddArtikel.Dicke = 0;
                                            AddArtikel.Breite = 0;
                                            AddArtikel.Laenge = 0;
                                            AddArtikel.Hoehe = 0;
                                            AddArtikel.Bestellnummer = string.Empty;
                                            if (this.Sys.Client.Modul.ASN_AutoCreateNewGArtByASN)
                                            {
                                                if (AddArtikel.GArtID == 1)
                                                {
                                                    string strTxt = string.Empty;
                                                    if (AddArtikel.GArt.CreateNewGArtByASN(decASNSender, Value))
                                                    {
                                                        AddArtikel.GArtID = AddArtikel.GArt.ID;
                                                        //Güterart wurde erfolgreich angelegt
                                                        strTxt = "NEU -> Güterart ID[" + AddArtikel.GArt.ID.ToString() + "] - Matchcode [" + AddArtikel.GArt.ViewID + "]";
                                                    }
                                                    else
                                                    {
                                                        //Güterart konnte nicht angelegt werden
                                                        strTxt = "Achtung - Güterart konnte nicht erstellt werden!";
                                                    }
                                                    ListCreatedNewGArten.Add(strTxt);
                                                }
                                            }
                                            if (
                                                (AddArtikel.GArtID > 1) &&
                                                (this.Sys.Client.Modul.ASN_VDA4913_LVS_ReadASN_TakeOverGArtValues)
                                               )
                                            {
                                                SetASNGArtValue(ref AddArtikel);
                                            }
                                        }
                                    }
                                    AddToDictOrderID(ref AddArtikel, ZS_Bestellnummer);
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ714F04:
                                    AddArtikel.Werksnummer = Value;
                                    //Ermitteln der Güte und Abmessungen
                                    if (CheckForGArtVerweis(strKennung))
                                    {
                                        if (this.Sys.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_Honeselmann + "_"))
                                        {
                                            Tmp.clsTmpVerwTWB tmp = new Tmp.clsTmpVerwTWB();
                                            tmp.GL_User = this.GLUser;
                                            Int32 iTmp2 = 0;
                                            Int32.TryParse(Value, out iTmp2);
                                            tmp.SAP = iTmp2;
                                            tmp.FillBySAP();

                                            AddArtikel.Dicke = tmp.Dicke;
                                            AddArtikel.Breite = tmp.Breite;
                                            AddArtikel.Laenge = tmp.Laenge;
                                            AddArtikel.Guete = tmp.Guete;
                                            AddArtikel.GArtID = tmp.GArtID;
                                        }
                                        else
                                        {
                                            AddArtikel.GArtID = clsGut.GetGutByADRAndVerweis(this.GLUser, tmpAdrA, AddArtikel.Werksnummer, this.Sys.AbBereich.ID);
                                            //---testMR
                                            //AddArtikel.GArt.ID = AddArtikel.GArtID;
                                            //AddArtikel.GArt.Fill();
                                            AddArtikel.Dicke = 0;
                                            AddArtikel.Breite = 0;
                                            AddArtikel.Laenge = 0;
                                            AddArtikel.Hoehe = 0;
                                            AddArtikel.Bestellnummer = string.Empty;
                                            if (this.Sys.Client.Modul.ASN_AutoCreateNewGArtByASN)
                                            {
                                                if (AddArtikel.GArtID == 1)
                                                {
                                                    string strTxt = string.Empty;
                                                    if (AddArtikel.GArt.CreateNewGArtByASN(decASNSender, Value))
                                                    {
                                                        AddArtikel.GArtID = AddArtikel.GArt.ID;
                                                        //Güterart wurde erfolgreich angelegt
                                                        strTxt = "NEU -> Güterart ID[" + AddArtikel.GArt.ID.ToString() + "] - Matchcode [" + AddArtikel.GArt.ViewID + "]";
                                                    }
                                                    else
                                                    {
                                                        //Güterart konnte nicht angelegt werden
                                                        strTxt = "Achtung - Güterart konnte nicht erstellt werden!";
                                                    }
                                                    ListCreatedNewGArten.Add(strTxt);
                                                }
                                            }
                                            if (
                                                (AddArtikel.GArtID > 1) &&
                                                (this.Sys.Client.Modul.ASN_VDA4913_LVS_ReadASN_TakeOverGArtValues)
                                               )
                                            {
                                                SetASNGArtValue(ref AddArtikel);
                                            }
                                        }


                                    }
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ714F05:
                                    break;
                                //Brutto
                                case clsASN.const_VDA4913SatzField_SATZ714F06:
                                    decTmp = 0;
                                    decimal.TryParse(Value, out decTmp);
                                    AddArtikel.Brutto = decTmp / 1000;
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ714F07:
                                    AddArtikel.Einheit = Value;
                                    break;
                                //netto
                                case clsASN.const_VDA4913SatzField_SATZ714F08:
                                    decTmp = 0;
                                    decimal.TryParse(Value, out decTmp);
                                    AddArtikel.Netto = decTmp / 1000;
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ714F09:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ714F10:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ714F11:
                                    break;
                                //Pos
                                //case 66:
                                case clsASN.const_VDA4913SatzField_SATZ714F12:
                                    //AddArtikel.Position = Value;
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ714F13:
                                    break;
                                //charge
                                //case 68:
                                case clsASN.const_VDA4913SatzField_SATZ714F14:
                                    AddArtikel.Charge = Value;
                                    break;
                                //gef. Stoffe -> für LZZ
                                //case 70:
                                case clsASN.const_VDA4913SatzField_SATZ714F16:
                                    Int32 iYear = 1900;
                                    Int32 iKW = 1;
                                    if (Value.Length >= 6)
                                    {
                                        string strYear = string.Empty;
                                        string strKW = string.Empty;
                                        strYear = Value.Substring(0, 4);
                                        Int32.TryParse(strYear, out iYear);
                                        strKW = Value.Substring(4, Value.Length - 4);
                                        Int32.TryParse(strKW, out iKW);

                                    }
                                    AddArtikel.LZZ = Functions.GetDateFromLastDayOfCalWeek(iKW, iYear);
                                    break;

                                case clsASN.const_VDA4913SatzField_SATZ715F01:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ715F02:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ715F03:
                                    switch (Value)
                                    {
                                        case clsASN.const_VDA715F03_VerpackungsCodierung_Bund:
                                            AddArtikel.Einheit = "Bund";
                                            break;
                                        case clsASN.const_VDA715F03_VerpackungsCodierung_Bleche:
                                            AddArtikel.Einheit = "Paket";
                                            break;
                                        case clsASN.const_VDA715F03_VerpackungsCodierung_Pal:
                                            AddArtikel.Einheit = "Palette";
                                            break;
                                    }
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ715F04:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ715F05:
                                    iTmp = 0;
                                    Int32.TryParse(Value.ToString(), out iTmp);
                                    AddArtikel.Anzahl = iTmp;
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ715F06:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ715F07:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ715F08:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ715F09:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ715F10:
                                    break;

                                case clsASN.const_VDA4913SatzField_SATZ716F01:
                                    //s716 = new ASN.vda4913.Satz716();
                                    //s716.InitClass(dtASNValue.Copy());
                                    break;

                                case clsASN.const_VDA4913SatzField_SATZ717F01:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ717F02:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ717F03:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ717F04:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ717F05:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ717F06:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ717F07:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ717F08:
                                    break;

                                case clsASN.const_VDA4913SatzField_SATZ718F01:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ718F02:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ718F03:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ718F04:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ718F05:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ718F06:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ718F07:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ718F08:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ718F09:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ718F10:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ718F11:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ718F12:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ718F13:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ718F14:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ718F15:
                                    break;
                            }

                            if (
                                (AddArtikel.GArtID > 1) &&
                                (this.Sys.Client.Modul.ASN_VDA4913_LVS_ReadASN_TakeOverGArtValues)
                               )
                            {
                                SetASNGArtValue(ref AddArtikel);
                            }
                        }
                    }
                    if (iCountArt > 0)
                    {
                        //Check Zuweisung Bestellnummer in Customized Artikelfeld
                        CheckFor713F10OrderIDValue(ref AddArtikel, ref DictASNArtFieldAssignment);

                        if (this.Sys.AbBereich.DefaultValue.DictArbeitsbereichDefaultValue.Count > 0)
                        {
                            this.Sys.AbBereich.DefaultValue.SetDefaultValue(ref AddArtikel);
                        }
                        //Prüfen ob Datenfelder zusammengelegt / combiniert werden müssen
                        if (DictASNTableCombiValue.Count > 0)
                        {
                            SetASNColCombiValue(ref AddArtikel, ref DictASNTableCombiValue);
                        }
                        //SetRowValue(ref row, ref dtArtikel, AddArtikel.Copy(), decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);

                        //--- mr 2024_06_04
                        if (iGlobalArtCount > 0)
                        {
                            if (iCountArt <= iGlobalArtCount)
                            {
                                SetRowValue(ref row, ref dtArtikel, AddArtikel.Copy(), decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);
                            }
                        }
                        else
                        {
                            SetRowValue(ref row, ref dtArtikel, AddArtikel.Copy(), decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);
                        }
                    }
                }
            }
            return dtArtikel;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myAddArt"></param>
        /// <param name="myZS_Bestellnummer"></param>
        private void AddToDictOrderID(ref clsArtikel myAddArt, string myZS_Bestellnummer)
        {
            if (!myZS_Bestellnummer.Equals(string.Empty))
            {
                if ((myAddArt.Produktionsnummer != null) && (!myAddArt.Produktionsnummer.Equals(string.Empty)))
                {
                    if (!Dict713F10OrderID.ContainsValue(myZS_Bestellnummer))
                    {
                        if (!Dict713F10OrderID.ContainsKey(myAddArt.Produktionsnummer))
                        {
                            Dict713F10OrderID.Add(myAddArt.Produktionsnummer, myZS_Bestellnummer);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddToDict712_TM(string myTM, string myVehicleNo, string myLfs)
        {
            if (!myLfs.Equals(string.Empty))
            {
                if (!Dict712_Transportmittel.ContainsKey(myLfs))
                {
                    ediHelper_712_TM tmpTM = new ediHelper_712_TM(myTM, myVehicleNo);
                    Dict712_Transportmittel.Add(myLfs, tmpTM);
                }
            }
        }
        ///<summary>clsLagerdaten / CheckForCopyToNewArtikelValue</summary>
        ///<remarks>
        ///         Es wird geprüft ob im DictASNArtFieldAssignment sich Verweise auf 
        ///         Felder im Satz713 befinden, diese werden dann in der Artikelklasse entsprechend kopiert
        ///</remarks>
        private void CheckFor713F10OrderIDValue(ref clsArtikel myArtDest, ref Dictionary<string, clsASNArtFieldAssignment> myDictASNArtFieldAssignment)
        {
            foreach (KeyValuePair<string, clsASNArtFieldAssignment> itm in myDictASNArtFieldAssignment)
            {
                // do something with entry.Value or entry.Key
                switch (itm.Key)
                {
                    ////711
                    //case clsASN.const_VDA4913SatzField_SATZ711F01:
                    //case clsASN.const_VDA4913SatzField_SATZ711F02:
                    //case clsASN.const_VDA4913SatzField_SATZ711F03:
                    //case clsASN.const_VDA4913SatzField_SATZ711F04:
                    //case clsASN.const_VDA4913SatzField_SATZ711F05:
                    //case clsASN.const_VDA4913SatzField_SATZ711F06:
                    //case clsASN.const_VDA4913SatzField_SATZ711F07:
                    //case clsASN.const_VDA4913SatzField_SATZ711F08:
                    //case clsASN.const_VDA4913SatzField_SATZ711F09:
                    //case clsASN.const_VDA4913SatzField_SATZ711F10:
                    //case clsASN.const_VDA4913SatzField_SATZ711F11:
                    //case clsASN.const_VDA4913SatzField_SATZ711F12:
                    ////712
                    //case clsASN.const_VDA4913SatzField_SATZ712F01:
                    //case clsASN.const_VDA4913SatzField_SATZ712F02:
                    //case clsASN.const_VDA4913SatzField_SATZ712F03:
                    //case clsASN.const_VDA4913SatzField_SATZ712F04:
                    //case clsASN.const_VDA4913SatzField_SATZ712F05:
                    //case clsASN.const_VDA4913SatzField_SATZ712F06:
                    //case clsASN.const_VDA4913SatzField_SATZ712F07:
                    //case clsASN.const_VDA4913SatzField_SATZ712F08:
                    //case clsASN.const_VDA4913SatzField_SATZ712F09:
                    //case clsASN.const_VDA4913SatzField_SATZ712F10:
                    //case clsASN.const_VDA4913SatzField_SATZ712F11:
                    //case clsASN.const_VDA4913SatzField_SATZ712F12:
                    //case clsASN.const_VDA4913SatzField_SATZ712F13:
                    //case clsASN.const_VDA4913SatzField_SATZ712F14:
                    //case clsASN.const_VDA4913SatzField_SATZ712F15:
                    //case clsASN.const_VDA4913SatzField_SATZ712F16:
                    //case clsASN.const_VDA4913SatzField_SATZ712F17:
                    //case clsASN.const_VDA4913SatzField_SATZ712F18:
                    //case clsASN.const_VDA4913SatzField_SATZ712F19:
                    //case clsASN.const_VDA4913SatzField_SATZ712F20:
                    //case clsASN.const_VDA4913SatzField_SATZ712F21:
                    ////713
                    //case clsASN.const_VDA4913SatzField_SATZ713F01:
                    //case clsASN.const_VDA4913SatzField_SATZ713F02:
                    //case clsASN.const_VDA4913SatzField_SATZ713F03:
                    //case clsASN.const_VDA4913SatzField_SATZ713F04:
                    //case clsASN.const_VDA4913SatzField_SATZ713F05:
                    //case clsASN.const_VDA4913SatzField_SATZ713F06:
                    //case clsASN.const_VDA4913SatzField_SATZ713F07:
                    case clsASN.const_VDA4913SatzField_SATZ713F08:
                        //case clsASN.const_VDA4913SatzField_SATZ713F09:
                        //case clsASN.const_VDA4913SatzField_SATZ713F10:
                        //case clsASN.const_VDA4913SatzField_SATZ713F11:
                        //case clsASN.const_VDA4913SatzField_SATZ713F12:
                        //case clsASN.const_VDA4913SatzField_SATZ713F13:
                        //case clsASN.const_VDA4913SatzField_SATZ713F14:
                        //case clsASN.const_VDA4913SatzField_SATZ713F15:
                        //case clsASN.const_VDA4913SatzField_SATZ713F16:
                        //case clsASN.const_VDA4913SatzField_SATZ713F17:
                        //case clsASN.const_VDA4913SatzField_SATZ713F18:
                        //case clsASN.const_VDA4913SatzField_SATZ713F19:
                        //case clsASN.const_VDA4913SatzField_SATZ713F20:
                        //case clsASN.const_VDA4913SatzField_SATZ713F21:
                        clsASNArtFieldAssignment tmpASFA = (clsASNArtFieldAssignment)itm.Value;
                        if (tmpASFA is clsASNArtFieldAssignment)
                        {
                            foreach (KeyValuePair<string, string> xItm in this.Dict713F10OrderID)
                            {
                                if ((myArtDest.Produktionsnummer != null) && (!myArtDest.Produktionsnummer.Equals(string.Empty)))
                                {
                                    if (myArtDest.Produktionsnummer.Equals(xItm.Key.ToString()))
                                    {
                                        myArtDest.SetArtValue(tmpASFA.ArtField, xItm.Value.ToString());
                                    }
                                }
                            }
                        }
                        break;
                }
            }

        }
        ///<summary>clsLagerdaten / CheckForCopyToNewArtikelValue</summary>
        ///<remarks>Es wird geprüft ob im DictASNArtFieldAssignment sich Verweise auf Felder im Satz713 befinden, diese werden dann in der Artikelklasse entsprechend kopiert</remarks>
        private void CheckForCopyToNewArtikelValue(ref clsArtikel ArtSource, ref clsArtikel ArdDest, ref Dictionary<string, clsASNArtFieldAssignment> myDictASNArtFieldAssignment)
        {
            //AddArtikel.Bestellnummer = ZS_Bestellnummer;
            //Datenvalue
            foreach (KeyValuePair<string, clsASNArtFieldAssignment> itm in myDictASNArtFieldAssignment)
            {
                // do something with entry.Value or entry.Key
                switch (itm.Key)
                {
                    //711
                    case clsASN.const_VDA4913SatzField_SATZ711F01:
                    case clsASN.const_VDA4913SatzField_SATZ711F02:
                    case clsASN.const_VDA4913SatzField_SATZ711F03:
                    case clsASN.const_VDA4913SatzField_SATZ711F04:
                    case clsASN.const_VDA4913SatzField_SATZ711F05:
                    case clsASN.const_VDA4913SatzField_SATZ711F06:
                    case clsASN.const_VDA4913SatzField_SATZ711F07:
                    case clsASN.const_VDA4913SatzField_SATZ711F08:
                    case clsASN.const_VDA4913SatzField_SATZ711F09:
                    case clsASN.const_VDA4913SatzField_SATZ711F10:
                    case clsASN.const_VDA4913SatzField_SATZ711F11:
                    case clsASN.const_VDA4913SatzField_SATZ711F12:
                    //712
                    case clsASN.const_VDA4913SatzField_SATZ712F01:
                    case clsASN.const_VDA4913SatzField_SATZ712F02:
                    case clsASN.const_VDA4913SatzField_SATZ712F03:
                    case clsASN.const_VDA4913SatzField_SATZ712F04:
                    case clsASN.const_VDA4913SatzField_SATZ712F05:
                    case clsASN.const_VDA4913SatzField_SATZ712F06:
                    case clsASN.const_VDA4913SatzField_SATZ712F07:
                    case clsASN.const_VDA4913SatzField_SATZ712F08:
                    case clsASN.const_VDA4913SatzField_SATZ712F09:
                    case clsASN.const_VDA4913SatzField_SATZ712F10:
                    case clsASN.const_VDA4913SatzField_SATZ712F11:
                    case clsASN.const_VDA4913SatzField_SATZ712F12:
                    case clsASN.const_VDA4913SatzField_SATZ712F13:
                    case clsASN.const_VDA4913SatzField_SATZ712F14:
                    case clsASN.const_VDA4913SatzField_SATZ712F15:
                    case clsASN.const_VDA4913SatzField_SATZ712F16:
                    case clsASN.const_VDA4913SatzField_SATZ712F17:
                    case clsASN.const_VDA4913SatzField_SATZ712F18:
                    case clsASN.const_VDA4913SatzField_SATZ712F19:
                    case clsASN.const_VDA4913SatzField_SATZ712F20:
                    case clsASN.const_VDA4913SatzField_SATZ712F21:
                    //713
                    case clsASN.const_VDA4913SatzField_SATZ713F01:
                    case clsASN.const_VDA4913SatzField_SATZ713F02:
                    case clsASN.const_VDA4913SatzField_SATZ713F03:
                    case clsASN.const_VDA4913SatzField_SATZ713F04:
                    case clsASN.const_VDA4913SatzField_SATZ713F05:
                    case clsASN.const_VDA4913SatzField_SATZ713F06:
                    case clsASN.const_VDA4913SatzField_SATZ713F07:
                    case clsASN.const_VDA4913SatzField_SATZ713F08:
                    case clsASN.const_VDA4913SatzField_SATZ713F09:
                    case clsASN.const_VDA4913SatzField_SATZ713F10:
                    case clsASN.const_VDA4913SatzField_SATZ713F11:
                    case clsASN.const_VDA4913SatzField_SATZ713F12:
                    case clsASN.const_VDA4913SatzField_SATZ713F13:
                    case clsASN.const_VDA4913SatzField_SATZ713F14:
                    case clsASN.const_VDA4913SatzField_SATZ713F15:
                    case clsASN.const_VDA4913SatzField_SATZ713F16:
                    case clsASN.const_VDA4913SatzField_SATZ713F17:
                    case clsASN.const_VDA4913SatzField_SATZ713F18:
                    case clsASN.const_VDA4913SatzField_SATZ713F19:
                    case clsASN.const_VDA4913SatzField_SATZ713F20:
                    case clsASN.const_VDA4913SatzField_SATZ713F21:
                        clsASNArtFieldAssignment tmpASFA = (clsASNArtFieldAssignment)itm.Value;
                        if (tmpASFA is clsASNArtFieldAssignment)
                        {
                            ArdDest.CopyArtValue(tmpASFA.ArtField, ref ArtSource);
                        }
                        break;
                }
            }

        }
        ///<summary>clsLagerdaten / SetRowValue</summary>
        ///<remarks></remarks>
        private void SetRowValue(ref DataRow myRow, ref DataTable mydtArtikel, clsArtikel myArt, decimal myASN, string myLastLfsNR, ref Dictionary<string, clsASNArtFieldAssignment> myDictASNArtFieldAssCopyFieldValue)
        {
            //CHeck CopyValue to 
            if (myDictASNArtFieldAssCopyFieldValue.Count > 0)
            {
                foreach (KeyValuePair<string, clsASNArtFieldAssignment> item in myDictASNArtFieldAssCopyFieldValue)
                {
                    clsASNArtFieldAssignment tmpAss = (clsASNArtFieldAssignment)item.Value;
                    string strFieldSource = tmpAss.CopyToField;
                    //tmpAss.ArtField = tmpAss.CopyToField;
                    Int32 iPos = 0;
                    if (myArt.Position == null)
                    {
                        myArt.Position = "0";
                    }

                    Int32.TryParse(myArt.Position.ToString(), out iPos);
                    string strFieldValueToCopy = myArt.GetArtValueByField(tmpAss.ArtField);
                    SetASNArtFieldAssignment(strFieldSource, ref myArt, ref tmpAss, strFieldValueToCopy, iPos, true);
                }
            }




            DataRow myTmpRow = mydtArtikel.NewRow();
            myTmpRow["ASN"] = myASN;
            myTmpRow["Position"] = myArt.Position;
            myTmpRow["Werksnummer"] = myArt.Werksnummer;
            myTmpRow["Produktionsnummer"] = myArt.Produktionsnummer;
            myTmpRow["Charge"] = myArt.Charge;
            myTmpRow["Dicke"] = myArt.Dicke;
            myTmpRow["Breite"] = myArt.Breite;
            myTmpRow["Laenge"] = myArt.Laenge;
            myTmpRow["Hoehe"] = myArt.Hoehe;
            myTmpRow["Netto"] = myArt.Netto;
            myTmpRow["Brutto"] = myArt.Brutto;
            myTmpRow["Anzahl"] = myArt.Anzahl;
            myTmpRow["Einheit"] = myArt.Einheit;
            myTmpRow["Bestellnummer"] = myArt.Bestellnummer;
            myTmpRow["exBezeichnung"] = myArt.exBezeichnung;
            myTmpRow["exAuftrag"] = myArt.exAuftrag;
            myTmpRow["exAuftragPos"] = myArt.exAuftragPos;
            myTmpRow["exMaterialnummer"] = myArt.exMaterialnummer;
            myTmpRow["TARef"] = myArt.TARef;
            myTmpRow["GArtID"] = myArt.GArtID;
            myTmpRow["Gut"] = myArt.GArt.Bezeichnung;
            myTmpRow["ArtIDRef"] = myArt.ArtIDRef;
            myTmpRow["LfsNr"] = myLastLfsNR;
            myTmpRow["ChildID"] = ((Int32)myASN).ToString() + myLastLfsNR;

            if (this.Dict712_Transportmittel.Count > 0)
            {
                ediHelper_712_TM tmpTM = new ediHelper_712_TM(string.Empty, string.Empty);
                this.Dict712_Transportmittel.TryGetValue(myLastLfsNR, out tmpTM);
                myArt.ASN_TMS = tmpTM.TMS;
                myArt.ASN_VehicleNo = tmpTM.VehicleNo;
            }
            myTmpRow["TMS"] = myArt.ASN_TMS;
            myTmpRow["VehicleNo"] = myArt.ASN_VehicleNo;
            myTmpRow["GlowDate"] = myArt.GlowDate;
            myTmpRow["Guete"] = myArt.Guete;

            mydtArtikel.Rows.Add(myTmpRow);
        }
        ///<summary>clsLagerdaten / SetASNGArtValue</summary>
        ///<remarks></remarks>
        private bool CheckForGArtVerweis(string myKennung)
        {
            bool bReturn = false;
            switch (myKennung)
            {
                case clsASN.const_VDA4913SatzField_SATZ714F03:
                    if (
                        (this.Sys.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_SIL.ToString() + "_")) ||
                        (this.Sys.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_SZG.ToString() + "_"))
                        )
                    {
                        bReturn = true;
                    }
                    break;
                case clsASN.const_VDA4913SatzField_SATZ714F04:
                    if (
                            (this.Sys.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_SLE.ToString() + "_")) ||
                            (this.Sys.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_Honeselmann.ToString() + "_"))
                       )
                    {
                        bReturn = true;
                    }
                    break;
            }
            return bReturn;
        }
        ///<summary>clsLagerdaten / SetASNGArtValue</summary>
        ///<remarks></remarks>
        private void SetASNGArtValue(ref clsArtikel myArt)
        {
            if (myArt.GArtID > 0)
            {
                //---testMR
                //myArt.GArt.ID = myArt.GArtID;
                //myArt.GArt.Fill();

                if (!myArt.GArt.Werksnummer.Equals(string.Empty))
                {
                    myArt.Werksnummer = myArt.GArt.Werksnummer;
                }
                myArt.Einheit = myArt.GArt.Einheit;
                if (myArt.Dicke == 0M)
                {
                    myArt.Dicke = myArt.GArt.Dicke;
                }
                if (myArt.Breite == 0M)
                {
                    myArt.Breite = myArt.GArt.Breite;
                }
                if (myArt.Laenge == 0M)
                {
                    myArt.Laenge = myArt.GArt.Laenge;
                }
                if (myArt.Hoehe == 0M)
                {
                    myArt.Hoehe = myArt.GArt.Hoehe;
                }
                //if (myArt.Bestellnummer.Equals(string.Empty))
                //{
                //    myArt.Bestellnummer = myArt.GArt.BestellNr;
                //}
                //--- Bestellnummer
                if ((myArt.Netto == 0) && (myArt.GArt.Netto > 0))
                {
                    myArt.Netto = myArt.GArt.Netto;
                }
                if ((myArt.Brutto == 0) && (myArt.GArt.Brutto > 0))
                {
                    myArt.Brutto = myArt.GArt.Brutto;
                }

                this.Sys.Client.clsLagerdaten_Customized_ASNArtikel_Bestellnummer(ref myArt, myArt.GArt.BestellNr);

                //-- IsMulde
                if (
                        (myArt.GArt.ArtikelArt.IndexOf("COIL") > -1) ||
                        (myArt.GArt.ArtikelArt.IndexOf("Coil") > -1)
                    )
                {
                    myArt.IsMulde = true;
                }
                //-- IsStackable
                myArt.IsStackable = myArt.GArt.IsStackable;
            }
        }
        ///<summary>clsLagerdaten / SetASNArtFieldAssignment</summary>
        ///<remarks></remarks>
        private bool SetASNArtFieldAssignment(string strArtField, ref clsArtikel myArt, ref clsASNArtFieldAssignment myArtAssign, string strValue, Int32 myArtPos, bool IsFieldCopy)
        {
            clsASNFormatFunctions ASNfunc = new clsASNFormatFunctions();

            bool bReturn = false;
            if (myArtAssign != null)
            {
                decimal decTmp = 0;
                switch (strArtField)
                {
                    case clsArtikel.ArtikelField_Anzahl:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                        }
                        Int32 iTmp = 0;
                        Int32.TryParse(strValue, out iTmp);
                        myArt.Anzahl = iTmp;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_LVSID:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                        }
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_Dicke:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                        }
                        decTmp = 0;
                        Decimal.TryParse(strValue, out decTmp);
                        myArt.Dicke = decTmp / 1000;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_Breite:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                        }
                        decTmp = 0;
                        Decimal.TryParse(strValue, out decTmp);
                        myArt.Breite = decTmp;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_Länge:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                        }
                        decTmp = 0;
                        Decimal.TryParse(strValue, out decTmp);
                        string strHelp = Functions.FormatDecimal(decTmp);
                        decTmp = 0;
                        Decimal.TryParse(strValue, out decTmp);
                        myArt.Laenge = decTmp;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_Höhe:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                        }
                        decTmp = 0;
                        Decimal.TryParse(strValue, out decTmp);
                        myArt.Hoehe = decTmp;
                        bReturn = true;
                        break;

                    case clsArtikel.ArtikelField_Abmessungen:
                        strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                        //hier string.Empty, da die Werte direkt in die clsArtikel myArt geschrieben werden
                        strValue = string.Empty;
                        break;

                    case clsArtikel.ArtikelField_Netto:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                        }
                        decTmp = 0;
                        Decimal.TryParse(strValue, out decTmp);
                        Decimal.TryParse(Functions.FormatDecimal(decTmp), out decTmp);
                        myArt.Netto = decTmp;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_Brutto:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                        }
                        decTmp = 0;
                        Decimal.TryParse(strValue, out decTmp);
                        Decimal.TryParse(Functions.FormatDecimal(decTmp), out decTmp);
                        myArt.Brutto = decTmp;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_Einheit:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                        }
                        myArt.Einheit = strValue;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_Produktionsnummer:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                        }
                        myArt.Produktionsnummer = strValue;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_Werksnummer:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                        }
                        myArt.Werksnummer = strValue;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_Charge:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                        }
                        myArt.Charge = strValue;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_Bestellnummer:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                        }
                        myArt.Bestellnummer = strValue;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_exBezeichnung:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        //Formatierung
                        if (
                            (!IsFieldCopy) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                        }
                        myArt.exBezeichnung = strValue;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_exMaterialnummer:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                        }
                        myArt.exMaterialnummer = strValue;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_Gut:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                        }
                        myArt.Gut = strValue;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_Güte:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                        }
                        myArt.Guete = strValue;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_Position:
                        if (myArtAssign.IsDefValue)
                        {
                            switch (myArtAssign.DefValue)
                            {
                                case clsArtikel.ArtikelFunction_ArtikelPosition:
                                    strValue = myArtPos.ToString(); ;
                                    break;
                                default:
                                    strValue = myArtAssign.DefValue;
                                    break;
                            }
                        }
                        if (
                            (!IsFieldCopy) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                        }
                        myArt.Position = strValue;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_exAuftrag:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                        }
                        myArt.exAuftrag = strValue;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_exAuftragPos:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                        }
                        myArt.exAuftragPos = strValue;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_ArtikelIDRef:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                        }
                        myArt.ArtIDRef = strValue;
                        bReturn = true;
                        break;

                    case clsArtikel.ArtikelField_TARef:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                        }
                        myArt.TARef = strValue;
                        bReturn = true;
                        break;

                    case clsArtikel.ArtikelField_GlowDate:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                        }
                        DateTime tmpDT = Globals.DefaultDateTimeMinValue;
                        DateTime.TryParse(strValue, out tmpDT);
                        myArt.GlowDate = tmpDT;
                        bReturn = true;
                        break;

                    default:
                        break;
                }
            }
            return bReturn;
        }
        ///<summary>clsLagerdaten / SetASNArtFieldAssignment</summary>
        ///<remarks></remarks>
        private void SetASNColCombiValue(ref clsArtikel myArt, ref Dictionary<string, clsASNTableCombiValue> myDict)
        {
            foreach (KeyValuePair<string, clsASNTableCombiValue> pair in myDict)
            {
                string colTarget = pair.Key.ToString();
                clsASNTableCombiValue TmpVal = (clsASNTableCombiValue)pair.Value;
                myArt.CombinateValue(colTarget, TmpVal.ListColsForCombination);
            }
        }
        ///<summary>clsLagerdaten / GetWarengruppenID</summary>
        ///<remarks>Ermittlung der Warengruppe über die Abmessung:
        ///         - alle drei Abmessungen = Pakete
        ///         - 2 Abmessungen = Coils
        ///         </remarks>
        public static decimal GetWarengruppenID(Globals._GL_USER myUser, decimal myDicke, decimal myBreite, decimal myLaenge)
        {
            string strSql = string.Empty;
            if (
                   (myDicke > 0) &
                   (myBreite > 0) &
                  ((myLaenge > 0) & (myLaenge < clsSystem.const_Default_MaxLenghtBleche))
              )
            {
                strSql = "Select Top(1) ID FROM Gueterart WHERE Bezeichnung='Pakete' OR Bezeichnung='PAKETE';";
            }
            else
            {
                strSql = "Select Top(1) ID FROM Gueterart WHERE Bezeichnung='COILS' OR Bezeichnung='Coils';";
            }
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, myUser.User_ID);
            decimal decTmp = 0;
            Decimal.TryParse(strTmp, out decTmp);
            return decTmp;
        }
        ///<summary>clsLagerdaten / IsWarengruppeCoils</summary>
        ///<remarks></remarks>
        public static bool IsWarengruppeCoils(Globals._GL_USER myUser, decimal myDicke, decimal myBreite, decimal myLaenge)
        {
            bool bReturn = false;
            if (
                   (myDicke > 0) &
                   (myBreite > 0) &
                  ((myLaenge > 0) & (myLaenge < clsSystem.const_Default_MaxLenghtBleche))
              )
            {
                bReturn = false;
            }
            else
            {
                bReturn = true;
            }
            return bReturn;
        }
        /// <summary>
        ///             Ermittelt den aktuellen Tagesbestand für die VDA BM
        /// </summary>
        /// <param name="myGLSystem"></param>
        /// <param name="myGLUser"></param>
        /// <param name="mySystem"></param>
        /// <returns></returns>
        public DataTable GetArtikelForBM(clsJobs myJob)
        {
            DataTable dtReturn = new DataTable("Artikel");
            clsLager tmpLager = new clsLager();
            tmpLager.InitClass(this.GLUser, this.GLSystem, this.Sys);
            tmpLager.BestandVon = DateTime.Now;
            tmpLager.AbBereichID = myJob.ArbeitsbereichID;
            tmpLager.BestandAdrID = myJob.AdrVerweisID;

            string strSql = string.Empty;
            if (this.Sys.DebugModeCOM)
            {
                strSql = "SELECT Top(5) a.* ";
            }
            else
            {
                strSql = "SELECT a.* ";
            }
            string strSql2 = tmpLager.GetSQLBestandsdaten2(clsLager.const_Bestandsart_Tagesbestand, 0, strSql, false);
            strSql2 = strSql2.Replace(";", "");
            if (this.Sys.DebugModeCOM)
            {
                strSql2 += " ORDER BY LVS_ID desc";
            }

            dtReturn = clsSQLcon.ExecuteSQL_GetDataTable(strSql2, this.GLUser.User_ID, "Artikel");
            return dtReturn;
        }


    }
}
