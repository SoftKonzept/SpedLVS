using Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Data;
//using System.Windows.Forms;

namespace LVS
{
    public class clsLAusgang
    {
        internal clsADR AdrAuftraggeber;
        internal clsADR AdrEmpfaenger;
        internal clsADR AdrVersender;

        public const string const_DBTableName = "LAusgang";
        public clsADRMan AdrManuell = new clsADRMan();

        private clsSystem _Sys;
        public clsSystem Sys
        {
            get { return _Sys; }
            set
            {
                _Sys = value;
                if ((this._Sys != null) && (this._Sys.AbBereich is clsArbeitsbereiche))
                {
                    this.AbBereichID = _Sys.AbBereich.ID;
                    this.MandantenID = _Sys.AbBereich.MandantenID;
                }
            }
        }
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

        public DataTable dtArtInLAusgang = new DataTable("ArtikelLAusgang");
        //public bool bIsCreatedByCall { get; set; }
        public decimal LockedBy { get; set; }
        public string exTransportRef { get; set; }
        public ClsStatus Stat { get; set; }

        private decimal _LAusgangTableID;
        public decimal LAusgangTableID
        {
            get
            {
                return _LAusgangTableID;
            }
            set
            {
                _LAusgangTableID = value;
                this.Stat = ClsStatus.initialized;
            }
        }
        public decimal LAusgangID { get; set; }
        public DateTime LAusgangsDate { get; set; }

        public DateTime MinLAusgangsDate
        {
            get
            {
                DateTime dtTmp = Convert.ToDateTime("01.01.1753");
                string strSQL = string.Empty;
                strSQL = "Select MAX(e.Date) FROM Artikel a " +
                                                    "INNER JOIN LEingang e on e.ID = a.LEingangTableID " +
                                                    "INNER JOIN LAusgang aus on aus.ID = a.LAusgangTableID " +
                                                    "WHERE a.LAusgangTableID=" + this.LAusgangTableID + ";";
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, this.BenutzerID);
                DateTime.TryParse(strTmp, out dtTmp);
                if (DateTime.TryParse(strTmp, out dtTmp))
                {
                    return dtTmp;
                }
                else
                {
                    return Convert.ToDateTime("01.01.1753");
                }
            }
        }

        public decimal Entladestelle { get; set; }
        public decimal ArtikelID { get; set; }
        public Int32 iSearchTxt { get; set; }
        public decimal Empfaenger { get; set; }
        public decimal Auftraggeber { get; set; }
        public decimal Versender { get; set; }
        public string strSearchTxt { get; set; }
        public decimal LVSNr { get; set; }
        //public DateTime LfsDate { get; set; }
        public string LfsNr
        {
            get
            {
                string strTmp = string.Empty;
                if (this.AbBereichID != null)
                {
                    strTmp += this.AbBereichID.ToString();
                }
                if (this.LAusgangID != null)
                {
                    strTmp += "/A " + this.LAusgangID.ToString();
                }
                return strTmp;
            }
        }
        public decimal SLB { get; set; }
        public string MAT { get; set; }
        public bool Checked { get; set; }
        public decimal SpedID { get; set; }
        public string KFZ { get; set; }
        public decimal Sachbearbeiter { get; set; }
        public string Info { get; set; }
        public decimal ASN { get; set; }
        public decimal MandantenID { get; set; }
        public string Lieferant { get; set; }
        public decimal GewichtNetto { get; set; }
        public decimal GewichtBrutto { get; set; }
        public decimal AbBereichID { get; set; }
        public DateTime Termin { get; set; }
        public bool DirectDelivery { get; set; }
        public bool bAllArtikelChecked { get; set; }
        public bool bAllArtikelArePlacedInStore { get; set; }
        public decimal NeutralerAuftraggeber { get; set; }
        public decimal NeutralerEmpfaenger { get; set; }
        public bool LagerTransport { get; set; }
        public string WaggonNr { get; set; }
        public string EAIDalteLVS { get; set; }
        public decimal BeladeID { get; set; }
        public bool IsPrintDoc { get; set; }
        public bool IsPrintAnzeige { get; set; }
        public bool IsPrintLfs { get; set; }
        public bool IsWaggon { get; set; }
        public bool IsRL { get; set; }
        public string Fahrer { get; set; }
        public bool IsPrintList { get; set; }
        public string Trailer { get; set; }

        public List<decimal> listPostAuftraggeber { get; set; }
        public List<decimal> listPostDUpdateForAnzeigePrint { get; set; }

        public string Brutto
        {
            get
            {
                string sql = "Select SUM(Cast(Brutto as int)) from Artikel where LAusgangTableID=" + LAusgangTableID;
                return LVS.clsSQLcon.ExecuteSQL_GetValue(sql, _GL_User.User_ID);
            }
        }
        public string Netto
        {
            get
            {
                string sql = "Select SUM(Cast(Netto as int)) from Artikel where LAusgangTableID=" + LAusgangTableID;
                return LVS.clsSQLcon.ExecuteSQL_GetValue(sql, _GL_User.User_ID);
            }
        }
        public string ArtikelCount
        {
            get
            {
                string sql = "Select Count(*) from Artikel where LAusgangTableID=" + LAusgangTableID;
                return LVS.clsSQLcon.ExecuteSQL_GetValue(sql, _GL_User.User_ID);
            }
        }

        //nur Zwischenspeicher für ASN XML einlesen
        public string ZP_AuftragNo { get; set; }
        public string ZP_AuftragPosNo { get; set; }
        public string ZP_LsPos { get; set; }
        public string ZP_LsNo { get; set; }


        /*********************************************************************************************************
         *                                          Methoden
         * ******************************************************************************************************/
        ///<summary>clsLAusgang / InitDefaultClsAusgang</summary>
        ///<remarks>Erstellt einen Standardausgang mit Standardwerten</remarks>
        public void InitDefaultClsAusgang(Globals._GL_USER myGLUser, clsSystem mySys)
        {
            try
            {
                this._GL_User = myGLUser;
                this.Sys = mySys;
                this.AbBereichID = 0;
                this.Stat = ClsStatus.initialized;
                if ((this.Sys is clsSystem) && (this.Sys.AbBereich is clsArbeitsbereiche))
                {
                    this.AbBereichID = this.Sys.AbBereich.ID;
                }
                this.MandantenID = 0;
                if ((this.Sys is clsSystem) && (this.Sys.AbBereich is clsArbeitsbereiche))
                {
                    this.MandantenID = this.Sys.AbBereich.MandantenID;
                }

                this.LAusgangsDate = DateTime.Now;
                this.GewichtNetto = 0;
                this.GewichtBrutto = 0;
                this.Auftraggeber = 0;
                this.Empfaenger = 0;
                this.Versender = 0;
                this.Entladestelle = 0;
                //this.LfsNr = string.Empty;
                //this.LfsDate = clsSystem.const_DefaultDateTimeValue_Min;
                this.Lieferant = string.Empty;
                this.SLB = 0;
                this.MAT = string.Empty;
                this.Checked = false;
                this.SpedID = 0;
                this.KFZ = string.Empty;
                this.Info = string.Empty;
                this.Termin = clsSystem.const_DefaultDateTimeValue_Min;
                this.IsPrintDoc = false;
                this.IsPrintAnzeige = false;
                this.IsPrintLfs = false;
                this.IsRL = false;
                this.IsWaggon = false;
                this.LagerTransport = false;
                this.Fahrer = string.Empty;
                this.KFZ = string.Empty;
                this.Trailer = string.Empty;
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }
        }
        ///<summary>clsLAusgang / Copy</summary>
        ///<remarks></remarks>
        public clsLAusgang Copy()
        {
            return (clsLAusgang)this.MemberwiseClone();
        }
        ///<summary>clsLAusgang / GetLagerDaten</summary>
        ///<remarks>Ermittel anhand der LagerausgangstableID die entsprechenden Artikel.</remarks>
        public DataTable GetLagerLAusgangArtikelDaten()
        {
            string strSQL = string.Empty;
            DataTable dt = new DataTable("Artikel");
            dt.Clear();
            if (LAusgangTableID > 0)
            {
                strSQL = "SELECT " +
                          "a.LA_Checked as [Check]" +
                          ", a.ID as ArtikelID" +
                          ", a.LVS_ID as LVSNr" +
                          ", e.Bezeichnung as Gut" +
                          ", a.Werksnummer" +
                          ", a.Produktionsnummer" +
                          ", a.Charge" +
                          ", a.exBezeichnung" +
                          ", a.Netto" +
                          ", a.Brutto" +
                          ", a.Breite" +
                          ", a.Hoehe" +
                          ", a.Laenge" +
                          ", a.Dicke" +
                          ", a.Bestellnummer" +
                          ", a.FreigabeAbruf as Freigabe " +
                          ", c.Date as Eingangsdatum " +
                          ", c.LEingangID as Eingang " +
                          ", a.BKZ " +
                          ", a.Halle " +
                          ", a.Reihe " +
                          ", c.WaggonNo " +
                          ", c.LfsNr as Lieferschein" +
                          ", CAST(0 as bit) as Ausgang " +
                          ", d.KD_ID as Auftraggeber " +
                          ", a.exInfo as Bemerkung" +
                          ", CAST(DATEPART(YYYY, a.LZZ) as varchar)+CAST(DATEPART(ISOWK, a.LZZ)as varchar) as LZZ" +
                          ", a.exMaterialnummer " +
                          ", a.ArtIDRef " +
                          ", '" + exTransportRef + "'" +
                          ", " + clsArtikel.GetStatusColumnSQL("b", "c") +
                          ",(Select Top(1) [Status] From Abrufe LEFT JOIN Artikel on Artikel.ID=Abrufe.ArtikelID WHERE Artikel.ID=a.ID AND Status<>'" + clsASNCall.const_Status_deactivated + "') as CallStatus " +
                          ",CASE " +
                                "WHEN b.Checked=0 THEN b.Checked " +
                                "ELSE " +
                                    "CASE " +
                                        "WHEN ISNULL((Select Top(1) [Status] From Abrufe WHERE Abrufe.ArtikelID=a.ID),'0')= '0' THEN  CAST(1 as bit) " +
                                        "WHEN (Select Top(1) [Status] From Abrufe LEFT JOIN Artikel on Artikel.ID=Abrufe.ArtikelID WHERE Artikel.ID=a.ID)='ENTL' THEN CAST(1 as bit) " +
                                        "ELSE CAST(0 as bit) " +
                                        "END " +
                                "END as ENTL " +

                          " FROM Artikel a " +
                          "INNER JOIN LAusgang b ON b.ID = a.LAusgangTableID " +
                          "INNER JOIN Gueterart e ON e.ID = a.GArtID " +
                          "INNER JOIN LEingang c ON c.ID = a.LEingangTableID " + // anpassung für die Views
                          "INNER JOIN ADR d ON d.ID = c.Auftraggeber " +
                          "WHERE b.ID='" + LAusgangTableID + "' ";

                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, dt.TableName.ToString());
            }
            return dt;
        }
        ///<summary>clsLAusgang / UpdateLAusgang</summary>
        ///<remarks>Update eines Datensatzes in der DB über die ID.</remarks>
        public bool UpdateLagerAusgang()
        {
            if (LAusgangTableID > 0)
            {
                if (ExistLAusgangTableID())
                {
                    //if (LfsDate == DateTime.MinValue) { LfsDate = DateTime.MaxValue; }

                    string strSql = string.Empty;

                    strSql = "Update LAusgang SET " +
                                                "LAusgangID =" + (Int32)LAusgangID +
                                                ", Datum = '" + LAusgangsDate + "'" +
                                                ", Netto = '" + GewichtNetto.ToString().Replace(",", ".") + "'" +
                                                ", Brutto = '" + GewichtBrutto.ToString().Replace(",", ".") + "'" +
                                                ", Auftraggeber = " + (Int32)Auftraggeber +
                                                ", Versender =" + (Int32)Versender +
                                                ", Empfaenger = " + (Int32)Empfaenger +
                                                ", Entladestelle =" + (Int32)Entladestelle +
                                                ", Lieferant = '" + Lieferant + "'" +
                                                ", LfsNr ='" + LfsNr + "'" +
                                                //", LfsDate = '" + LfsDate + "'" +
                                                ", SLB ='" + SLB + "'" +
                                                ", MAT = '" + MAT + "'" +
                                                ", Checked =" + Convert.ToInt32(Checked) +
                                                // ", Checked ='" + MandantenID + "'" + extra raus da diese Feld nur über abschluss veränderbar sein soll
                                                ", SpedID =" + (Int32)SpedID +
                                                ", KFZ = '" + KFZ + "'" +
                                                ", [USER] =" + (Int32)_GL_User.User_ID +
                                                ", ASN = '" + (Int32)ASN + "'" +
                                                ", Info = '" + Info + "'" +
                                                ", AbBereich = " + (Int32)AbBereichID +
                                                ", MandantenID =" + (Int32)MandantenID +
                                                ", Termin ='" + Termin + "'" +
                                                ", DirectDelivery ='" + DirectDelivery + "'" +
                                                ", neutrAuftraggeber =" + NeutralerAuftraggeber +
                                                ", neutrEmpfaenger =" + NeutralerEmpfaenger +
                                                ", LagerTransport =" + Convert.ToInt32(LagerTransport) +
                                                ", WaggonNo='" + WaggonNr + "' " +
                                                ", IsPrintDoc=" + Convert.ToInt32(IsPrintDoc) +
                                                ", IsPrintAnzeige=" + Convert.ToInt32(IsPrintAnzeige) +
                                                ", IsPrintLfs=" + Convert.ToInt32(IsPrintLfs) +
                                                ", IsWaggon=" + Convert.ToInt32(IsWaggon) +
                                                ", exTransportRef='" + exTransportRef + "'" +
                                                ", Fahrer='" + Fahrer + "'" +
                                                ", Trailer='" + this.Trailer + "'" +

                                                        " WHERE ID=" + LAusgangTableID + ";";
                    bool retVal = clsSQLcon.ExecuteSQL(strSql, BenutzerID);

                    //ArtikelVita
                    clsArtikelVita.LagerAusgangChange(BenutzerID, LAusgangTableID, LAusgangID);

                    // Logbucheintrag Eintrag
                    string Beschreibung = "Lager Ausgang geändert: Nr [" + LAusgangID.ToString() + "] / Mandant [" + MandantenID.ToString() + "] / Arbeitsbereich [" + AbBereichID.ToString() + "]";
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
                    return retVal;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsLAusgang / GetLAusgangsdatenByLAusgangTableID</summary>
        ///<remarks>Ermittel die Daten des Lagerausgangs anhand der TableID.</remarks>
        public bool FillAusgang()
        {
            if (ExistLAusgangTableID())
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = "Select * FROM LAusgang WHERE ID=" + LAusgangTableID + ";";

                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Lagerausgang");
                if (dt.Rows.Count > 0)
                {
                    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        this.LAusgangTableID = (decimal)dt.Rows[i]["ID"];
                        this.LAusgangID = (decimal)dt.Rows[i]["LAusgangID"];
                        this.LAusgangsDate = (DateTime)dt.Rows[i]["Datum"];
                        this.GewichtNetto = (decimal)dt.Rows[i]["Netto"];
                        this.GewichtBrutto = (decimal)dt.Rows[i]["Brutto"];
                        this.Auftraggeber = (decimal)dt.Rows[i]["Auftraggeber"];
                        this.Versender = (decimal)dt.Rows[i]["Versender"];
                        this.Empfaenger = (decimal)dt.Rows[i]["Empfaenger"];
                        this.Entladestelle = (decimal)dt.Rows[i]["Entladestelle"];
                        this.Lieferant = dt.Rows[i]["Lieferant"].ToString();
                        decimal decTmp = 0;
                        //Decimal.TryParse(dt.Rows[i]["LfsNr"].ToString(), out decTmp);
                        //this.LfsNr = decTmp;
                        //DateTime dtTmp = clsSystem.const_DefaultDateTimeValue_Min;
                        //DateTime.TryParse(dt.Rows[i]["LfsDate"].ToString(), out dtTmp);
                        //this.LfsDate = dtTmp;
                        decTmp = 0;
                        Decimal.TryParse(dt.Rows[i]["SLB"].ToString(), out decTmp);
                        this.SLB = decTmp;
                        this.MAT = dt.Rows[i]["MAT"].ToString().Trim();
                        this.Checked = (bool)dt.Rows[i]["Checked"];
                        decTmp = 0;
                        Decimal.TryParse(dt.Rows[i]["SpedID"].ToString(), out decTmp);
                        this.SpedID = decTmp;
                        this.KFZ = dt.Rows[i]["KFZ"].ToString().Trim();
                        this.Sachbearbeiter = (decimal)dt.Rows[i]["USER"];
                        decTmp = 0;
                        Decimal.TryParse(dt.Rows[i]["ASN"].ToString(), out decTmp);
                        this.ASN = decTmp;
                        this.Info = dt.Rows[i]["Info"].ToString().Trim();
                        decTmp = 0;
                        Decimal.TryParse(dt.Rows[i]["AbBereich"].ToString(), out decTmp);
                        this.AbBereichID = decTmp;
                        decTmp = 0;
                        Decimal.TryParse(dt.Rows[i]["MandantenID"].ToString(), out decTmp);
                        this.MandantenID = decTmp;
                        DateTime dtTmp = clsSystem.const_DefaultDateTimeValue_Min;
                        DateTime.TryParse(dt.Rows[i]["Termin"].ToString(), out dtTmp);
                        this.Termin = dtTmp;
                        this.DirectDelivery = (bool)dt.Rows[i]["DirectDelivery"];
                        decTmp = 0;
                        Decimal.TryParse(dt.Rows[i]["neutrAuftraggeber"].ToString(), out decTmp);
                        this.NeutralerAuftraggeber = decTmp;
                        decTmp = 0;
                        Decimal.TryParse(dt.Rows[i]["neutrEmpfaenger"].ToString(), out decTmp);
                        this.NeutralerEmpfaenger = decTmp;
                        this.LagerTransport = (bool)dt.Rows[i]["LagerTransport"];
                        this.WaggonNr = dt.Rows[i]["WaggonNo"].ToString().Trim();
                        decTmp = 0;
                        Decimal.TryParse(dt.Rows[i]["BeladeID"].ToString(), out decTmp);
                        this.BeladeID = decTmp;
                        this.IsPrintDoc = (bool)dt.Rows[i]["IsPrintDoc"];
                        this.IsPrintAnzeige = (bool)dt.Rows[i]["IsPrintAnzeige"];
                        this.IsPrintLfs = (bool)dt.Rows[i]["IsPrintLfs"];
                        decTmp = 0;
                        Decimal.TryParse(dt.Rows[i]["LockedBy"].ToString(), out decTmp);
                        this.LockedBy = decTmp;
                        this.IsWaggon = (bool)dt.Rows[i]["IsWaggon"];
                        this.exTransportRef = dt.Rows[i]["exTransportRef"].ToString();
                        this.Fahrer = dt.Rows[i]["Fahrer"].ToString();
                        this.IsRL = (bool)dt.Rows[i]["IsRL"];
                        this.IsPrintList = (bool)dt.Rows[i]["IsPrintList"];
                        this.Trailer = dt.Rows[i]["Trailer"].ToString().Trim();

                        this.Stat = ClsStatus.loaded;
                    }
                    bAllArtikelChecked = clsArtikel.CheckAllArtikelChecked_Ausgang(BenutzerID, this.LAusgangTableID);
                    bAllArtikelArePlacedInStore = clsArtikel.CheckAllArtikelArePlacedInStore(BenutzerID, this.LAusgangTableID, false);

                    dtArtInLAusgang.Clear();
                    dtArtInLAusgang = clsArtikel.GetArtikelInAusgang(this._GL_User, this.LAusgangTableID);

                    //manuelle ADR
                    AdrManuell = new clsADRMan();
                    AdrManuell.InitClass(this._GL_User, this.LAusgangTableID, "LAusgang");

                    if (this.Auftraggeber > 0)
                    {
                        AdrAuftraggeber = new clsADR();
                        AdrAuftraggeber.ID = this.Auftraggeber;
                        AdrAuftraggeber.FillClassOnly();
                    }

                    if (this.Empfaenger > 0)
                    {
                        AdrEmpfaenger = new clsADR();
                        AdrEmpfaenger.ID = this.Empfaenger;
                        AdrEmpfaenger.FillClassOnly();
                    }

                    if (this.Versender > 0)
                    {
                        AdrVersender = new clsADR();
                        AdrVersender.ID = this.Auftraggeber;
                        AdrVersender.FillClassOnly();
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsLAusgang / ExistLAusgangTableID</summary>
        ///<remarks>Prüft ob der Datensatz mit der ID existiert.</remarks>
        ///<returns>Returns BOOL</returns>
        public bool ExistLAusgangTableID()
        {
            if (LAusgangTableID > 0)
            {
                string strSql = string.Empty;
                strSql = "Select * FROM LAusgang WHERE ID='" + LAusgangTableID + "'";
                return clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsLAusgang / ExistLEingangTableID</summary>
        ///<remarks>Prüft, ob die angegebene LagereingangstableID vorhanden ist.</remarks>
        public static bool ExistLAusgangTableID(decimal decBenuzter, decimal myTableID)
        {
            if (myTableID > 0)
            {
                string strSQL = "SELECT ID FROM LAusgang WHERE ID=" + myTableID + ";";
                return clsSQLcon.ExecuteSQL_GetValueBool(strSQL, decBenuzter);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// clsLEingang / lockEingang        /// </summary>
        public void lockAusgang()
        {
            if (LAusgangTableID > 0)
            {
                string strSql = string.Empty;
                strSql = "Update LAusgang set lockedBy = 0 where lockedby = " + this._GL_User.User_ID + ";" +
                         "Update LAusgang set lockedBy = " + this._GL_User.User_ID + " where ID=" + LAusgangTableID +
                         " AND lockedBy=0;";
                clsSQLcon.ExecuteSQL(strSql, this._GL_User.User_ID);
            }
        }
        /// <summary>
        /// clsLEingang / lockEingang        /// </summary>
        public static void unlockAusgang(decimal userID = 0)
        {
            string strSql = string.Empty;
            if (userID > 0)
                strSql = "Update LAusgang set lockedBy = 0 where lockedby = " + userID + ";";
            else
                strSql = "Update LAusgang set lockedBy = 0;";
            clsSQLcon.ExecuteSQL(strSql, userID);
        }
        ///<summary>clsLAusgang / ExistLAusgangTableID</summary>
        ///<remarks>Prüft ob der Datensatz mit der ID existiert.</remarks>
        ///<returns>Returns BOOL</returns>
        public bool CheckLAusgangByLAusgangDaten()
        {
            if ((MandantenID > 0) && (AbBereichID > 0))
            {
                string strSql = string.Empty;
                strSql = "Select ID FROM LAusgang WHERE LAusgangID='" + LAusgangID + "' AND " +
                                                        "MandantenID='" + MandantenID + "' AND " +
                                                        "AbBereich='" + AbBereichID + "' ";
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
                decimal decTmp = 0;
                if (Decimal.TryParse(strTmp, out decTmp))
                {
                    LAusgangTableID = decTmp;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsLAusgang / AddArtikelToLAusgang</summary>
        ///<remarks>Setzt ein Update in der Artikeldatenbank. Folgende Spalten werden upgedatet:
        ///         - BKZ von 1 auf 0</remarks>
        ///         - LAusgangTableID
        ///<returns>Returns BOOL</returns>
        public bool AddArtikelToLAusgang(ref DataTable dtArtAusgang)
        {
            if (dtArtAusgang.Rows.Count > 0)
            {
                string strSql = string.Empty;
                string strArtIDList = string.Empty;
                for (Int32 i = 0; i <= dtArtAusgang.Rows.Count - 1; i++)
                {
                    strSql = strSql + "Update Artikel Set BKZ=0 AND LAusgangTableID='" + LAusgangTableID + "' WHERE ID='" + dtArtAusgang.Rows[i]["ID"].ToString() + "' ;";
                    strArtIDList = strArtIDList + dtArtAusgang.Rows[i]["ID"].ToString() + ",";
                }
                bool bVal = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "Lagerausgang Artikel", BenutzerID);

                if (!bVal)
                {
                    //Add Logbucheintrag 
                    string myBeschreibung = "Lager - Ausgang Artikel hinzugefügt: NR [" + LAusgangID + "] / Mandant [" + MandantenID + "] / Arbeitsbereich  [" + AbBereichID + "] - ArtikelID (" + strArtIDList + ")";
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), myBeschreibung);
                    //ArtikelVita
                    if (LAusgangID < 1)
                    {
                        LAusgangID = clsLAusgang.GetLAusgangIDByLAusgangTableID(BenutzerID, LAusgangTableID);
                    }
                    clsArtikelVita.AddArtikelManualLAusgang(_GL_User, LAusgangTableID, LAusgangID);
                }
                return bVal;
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsLAusgang / GetLAusgangIDByLAusgangTableID</summary>
        ///<remarks>.</remarks>
        public static decimal GetLAusgangIDByLAusgangTableID(decimal myBenutzerID, decimal myLAusgangTableID)
        {
            decimal myLAusgangID = 0;
            if (myLAusgangTableID > 0)
            {
                string strSql = string.Empty;
                strSql = "SELECT LAusgangID FROM LAusgang WHERE ID=" + myLAusgangTableID + ";";
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, myBenutzerID);
                Decimal.TryParse(strTmp, out myLAusgangID);
            }
            return myLAusgangID;
        }
        ///<summary>clsLAusgang / AddLAusgang</summary>
        ///<remarks>.</remarks>
        public string AddLAusgang_SQL()
        {
            this.MAT = this.LAusgangsDate.ToString();
            string strSql = string.Empty;
            strSql = clsPrimeKeys.GetNEWLAusgangIDSQL(MandantenID, AbBereichID, 0) +
                     " INSERT INTO LAusgang (LAusgangID, Datum, Netto, Brutto, Auftraggeber, Versender, Empfaenger, " +
                                           "Entladestelle, Lieferant, LfsNr, SLB, MAT, Checked, " +
                                           "SpedID, KFZ, [USER], ASN, Info, AbBereich, MandantenID, Termin, DirectDelivery, IsPrintDoc,ExTransportRef, " +
                                           "LagerTransport, WaggonNo, BeladeID, Fahrer, IsRL) " +
                                            "VALUES (( " + clsPrimeKeys.GetNEWLAusgangIDSQL(MandantenID, AbBereichID, 1) + ")" +
                                                    ",'" + LAusgangsDate + "'" +
                                                    ",'" + GewichtNetto.ToString().Replace(",", ".") + "'" +
                                                    ",'" + GewichtBrutto.ToString().Replace(",", ".") + "'" +
                                                    "," + (Int32)Auftraggeber +
                                                    "," + (Int32)Versender +
                                                    "," + (Int32)Empfaenger +
                                                    "," + (Int32)Entladestelle +
                                                    ",'" + Lieferant + "'" +
                                                    ",'" + LfsNr + "'" +
                                                    ",'" + SLB + "'" +
                                                    ",'" + MAT + "'" +
                                                    ",'" + Convert.ToBoolean(Checked) + "'" +
                                                    "," + (Int32)SpedID +
                                                    ",'" + KFZ + "'" +
                                                    ", " + (Int32)this.BenutzerID +
                                                    "," + (Int32)ASN +
                                                    ",'" + Info + "'" +
                                                    "," + (Int32)AbBereichID +
                                                    "," + (Int32)MandantenID +
                                                    ",'" + Termin + "'" +
                                                    ",'" + Convert.ToBoolean(DirectDelivery) + "'" +
                                                    ", " + Convert.ToInt32(IsPrintDoc) +
                                                    ", '" + exTransportRef + "'" +
                                                    ",'" + LagerTransport + "'" +
                                                    ",'" + WaggonNr + "' " +
                                                    ", " + (Int32)BeladeID +
                                                    ", '" + Fahrer + "'" +
                                                     ", " + Convert.ToInt32(IsRL) +
                                                    "); ";
            return strSql;
        }
        ///<summary>clsLAusgang / AddLAusgang</summary>
        ///<remarks>.</remarks>
        public bool AddLAusgang()
        {
            bool bAddOK = false;
            //if ((MandantenID > 0) && (LAusgangID > 0))
            if (MandantenID > 0)
            {
                //Beim Anlegen des Ausgangs ist Checked immer false
                Checked = false;

                //DateTimePicker dtp = new DateTimePicker();
                //LfsDate = DateTime.MaxValue;
                string strSQL = AddLAusgang_SQL();

                strSQL = strSQL + "Select @@IDENTITY as 'ID' ";
                string strTmp = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSQL, "Insert", BenutzerID);
                decimal decTmp = 0;
                Decimal.TryParse(strTmp, out decTmp);
                LAusgangTableID = decTmp;
                if (LAusgangTableID > 0)
                {
                    //Add Logbucheintrag 
                    string myBeschreibung = "Lager - Ausgang erstellt: NR [" + LAusgangID + "] / Mandant [" + MandantenID + "] / Arbeitsbereich  [" + AbBereichID + "] ";
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), myBeschreibung);

                    clsArtikelVita.AddAuslagerungManual(BenutzerID, LAusgangTableID, LAusgangID);
                    bAddOK = true;
                }
            }
            return bAddOK;
        }
        ///<summary>clsLAusgang / GetLAusgangsTableID</summary>
        ///<remarks>Ermittelt anhand der Lagerausgangsnummer und der Mandanten ID die Table ID.</remarks>
        public void GetLAusgangsTableID()
        {
            if (LAusgangID > 0)
            {
                string strSql = string.Empty;
                strSql = "SELECT ID FROM LAusgang WHERE LAusgangID='" + LAusgangID + "' AND MandantenID='" + MandantenID + "' AND AbBereich='" + AbBereichID + "'  ";
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
                if (strTmp != string.Empty)
                {
                    this.LAusgangTableID = Convert.ToDecimal(strTmp);
                }
            }
        }
        ///<summary>clsLAusgang/ UpdatePrintLEingang</summary>
        ///<remarks></remarks>
        public void UpdatePrintLAusgang(string myDocArt, decimal AdrID = -1, DateTime? date = null)
        {
            string strSql = string.Empty;
            switch (myDocArt)
            {
                case "LagerAusgangDoc":
                    //if (IsPrintDoc == false)
                    clsArtikelVita.LagerAusgangPrintDoc(BenutzerID, this.LAusgangTableID, LAusgangID);
                    strSql = "Update LAusgang SET IsPrintDoc=1 WHERE ID=" + this.LAusgangTableID + "; ";
                    break;

                case "Ausgangsliste":
                    clsArtikelVita.LagerAusgangPrintDoc(BenutzerID, this.LAusgangTableID, LAusgangID);
                    strSql = "Update LAusgang SET IsPrintList=1 WHERE ID=" + (Int32)this.LAusgangTableID + "; ";
                    break;

                case "LagerAusgangAnzeige":
                    //if (IsPrintAnzeige == false)
                    clsArtikelVita.LagerAusgangPrintAnzeige(BenutzerID, this.LAusgangTableID, LAusgangID);
                    strSql = "Update LAusgang SET IsPrintAnzeige=1 WHERE ID=" + this.LAusgangTableID + "; ";
                    break;
                case "LagerAusgangAnzeigePerDay":
                    //if (IsPrintAnzeige == false)
                    clsArtikelVita.LagerAusgangPrintPerDay(BenutzerID, this.LAusgangTableID, LAusgangID, AdrID, date);
                    strSql = "Update LAusgang SET IsPrintAnzeige=1 WHERE Auftraggeber=" + AdrID + " AND Cast(Datum as Date)=Cast('" + date + "' as Date) and Checked=1; ";
                    break;

                case "LagerAusgangLfs":
                case "AusgangLfs":
                    //if (IsPrintLfs == false)
                    clsArtikelVita.LagerAusgangPrintLfs(BenutzerID, this.LAusgangTableID, LAusgangID);
                    strSql = "Update LAusgang SET IsPrintLfs=1 WHERE ID=" + this.LAusgangTableID + "; ";
                    break;
            }
            if (strSql != string.Empty)
            {
                clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            }
        }
        ///<summary>clsLager / GetLAusgangsTableID</summary>
        ///<remarks>Ermittelt anhand der Lagerausgangsnummer und der Mandanten ID die Table ID.</remarks>
        public static string UpdateLArtikelLAusgang_SQL(Globals._GL_USER myGL_User, decimal myLAusgangTableID, decimal myArtikelID, bool myUB)
        {
            //BKZ
            Int32 iBKZ = 0;
            if (myLAusgangTableID > 0)
            {
                iBKZ = 0;  //wird ausgebucht
            }
            else
            {
                iBKZ = 1; //im Bestand
            }
            string strSql = string.Empty;
            strSql = "Update Artikel SET LAusgangTableID=" + myLAusgangTableID +
                                         ", BKZ=" + iBKZ +
                                         ", UB=" + Convert.ToInt32(myUB) +

                                         " WHERE ID=" + (int)myArtikelID + " ;";
            return strSql;
        }
        ///<summary>clsLager / GetLAusgangsTableID</summary>
        ///<remarks>Ermittelt anhand der Lagerausgangsnummer und der Mandanten ID die Table ID.</remarks>
        public static bool UpdateLArtikelLAusgang(Globals._GL_USER myGL_User, decimal myLAusgangTableID, decimal myArtikelID, bool myUB)
        {
            bool bUpOK = false;
            if (myArtikelID > 0)
            {
                string strSQL = UpdateLArtikelLAusgang_SQL(myGL_User, myLAusgangTableID, myArtikelID, myUB);
                bUpOK = clsSQLcon.ExecuteSQL(strSQL, myGL_User.User_ID);

                if (bUpOK)
                {
                    decimal decTmpLAusgangID = clsLAusgang.GetLAusgangIDByLAusgangTableID(myGL_User.User_ID, myLAusgangTableID);
                    if (myLAusgangTableID > 0)
                    {
                        clsArtikelVita.AddArtikelManualLAusgang(myGL_User, myArtikelID, decTmpLAusgangID);
                    }
                    else
                    {
                        clsArtikelVita.DeleteArtikelManualFROMLAusgang(myGL_User, myArtikelID, decTmpLAusgangID);
                    }
                }
            }
            return bUpOK;
        }
        ///<summary>clsLAusgang / GetLAusgangsTableID</summary>
        ///<remarks>Ermittelt anhand der Lagerausgangsnummer und der Mandanten ID die Table ID.</remarks>
        public static void UpdateLAusgangGewichtNetto(Globals._GL_USER myGL_User, decimal myLAusgangTableID, decimal myNetto)
        {
            if (myLAusgangTableID > 0)
            {
                string strSql = string.Empty;
                strSql = "Update LAusgang SET Netto='" + myNetto.ToString().Replace(",", ".") + "' " +
                                        "WHERE ID=" + (int)myLAusgangTableID + " ; ";
                bool bUpOK = clsSQLcon.ExecuteSQL(strSql, myGL_User.User_ID);
            }
        }
        ///<summary>clsLAusgang / GetLAusgangsTableID</summary>
        ///<remarks>Ermittelt anhand der Lagerausgangsnummer und der Mandanten ID die Table ID.</remarks>
        public static void UpdateLAusgangGewichtBrutto(Globals._GL_USER myGL_User, decimal myLAusgangTableID, decimal myBrutto)
        {
            if (myLAusgangTableID > 0)
            {
                string strSql = string.Empty;
                strSql = "Update LAusgang SET Brutto='" + myBrutto.ToString().Replace(",", ".") + "' " +
                                        "WHERE ID=" + (int)myLAusgangTableID + " ; ";
                bool bUpOK = clsSQLcon.ExecuteSQL(strSql, myGL_User.User_ID);
            }
        }
        ///<summary>clsLAusgang / GetLAusgangsTableID</summary>
        ///<remarks>Markiert den Artikel mit geprüft / nicht geprüft im Ausgang.</remarks>
        public static void UpdateLAusgangArtikelChecked(Globals._GL_USER myGL_User, decimal myArtikelID, bool bArtChecked)
        {
            if (myArtikelID > 0)
            {
                string strSql = string.Empty;
                strSql = "Update Artikel SET LA_Checked=" + Convert.ToInt32(bArtChecked) + " " +
                                             " WHERE ID=" + (int)myArtikelID + " ; ";
                bool bUpOK = clsSQLcon.ExecuteSQL(strSql, myGL_User.User_ID);
                if (bUpOK)
                {

                }
            }
        }
        public static string UpdateLAusgangArtikelCheckedSQL(Globals._GL_USER myGL_User, decimal myArtikelID, bool bArtChecked)
        {
            string strSql = string.Empty;
            if (myArtikelID > 0)
            {
                strSql = "Update Artikel SET LA_Checked=" + Convert.ToInt32(bArtChecked) + " " +
                                             " WHERE ID=" + (int)myArtikelID + " ; ";
            }
            return strSql;
        }
        ///<summary>clsLAusgang / GetLAusgangsTableID</summary>
        ///<remarks>Ermittelt anhand der Lagerausgangsnummer und der Mandanten ID die Table ID.</remarks>
        public static void UpdateLAusgangArtikelDeleteLAusgang(Globals._GL_USER myGL_User, decimal myArtikelID)
        {
            if (myArtikelID > 0)
            {
                string strSql = string.Empty;
                strSql = "Update Artikel SET LAusgangTableID='0' " +
                                        "WHERE ID=" + (int)myArtikelID + " ;";
                clsSQLcon.ExecuteSQL(strSql, myGL_User.User_ID);
            }
        }
        ///<summary>clsLAusgang / GetLAusgangsTableID</summary>
        ///<remarks>Ermittelt anhand der Lagerausgangsnummer und der Mandanten ID die Table ID.</remarks>
        public void UpdateLAusgangSetAusgangAbgeschlossen(bool bAusgangAbgeschlossen)
        {
            if (this.LAusgangTableID > 0)
            {
                Int32 iCheck = 0;
                if (bAusgangAbgeschlossen)
                {
                    iCheck = 1;
                }
                else
                {
                    iCheck = 0;
                }
                string strSql = string.Empty;

                strSql = "Update LAusgang SET Checked=" + Convert.ToInt32(bAusgangAbgeschlossen) +
                                              " , MAT='" + DateTime.Now.ToString() + "'" +
                                                " WHERE ID=" + this.LAusgangTableID + " ; ";
                bool bUpOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
                if (bUpOK)
                {
                    //ArtikelVita
                    //decimal decTmpLAusgangID = clsLAusgang.GetLAusgangIDByLAusgangTableID(BenutzerID, this.LAusgangTableID);
                    //if (this.LAusgangTableID > 0)
                    //{
                    clsArtikelVita.LagerAusgangChecked(BenutzerID, this.LAusgangTableID, this.LAusgangID);
                    //LVsCallStatus
                    foreach (DataRow row in this.dtArtInLAusgang.Rows)
                    {
                        decimal decArtID = 0;
                        Decimal.TryParse(row["ID"].ToString(), out decArtID);
                        if (decArtID > 0)
                        {
                            clsASNCall.UpdateAbrufeSetAbrufStatus(decArtID, this._GL_User, clsASNCall.const_Status_MAT);
                        }
                    }

                    //}
                }
            }
        }

        ///<summary>clsLAusgang / GetLAusgangsTableID</summary>
        ///<remarks>Ermittelt anhand der Lagerausgangsnummer und der Mandanten ID die Table ID.</remarks>
        public static void Delete(Globals._GL_USER myGL_User, decimal myArtikelID)
        {
            if (myArtikelID > 0)
            {
                string strSql = string.Empty;
                strSql = "Update Artikel SET LAusgangTableID='0' " +
                                        "WHERE ID='" + myArtikelID + "' ; ";
                clsSQLcon.ExecuteSQL(strSql, myGL_User.User_ID);
            }
        }

        ///<summary>clsLAusgang / DeleteLAusgangByLAusgangTableID</summary>
        ///<remarks>Löscht den Eingang anhand der Table ID. Hierfür werden unbeding folgende Angaben benötigt:
        ///         - MandantenID
        ///         - Arbeitsbereich
        ///         - LAusgangTableID</remarks>
        public void DeleteLAusgangByLAusgangTableID()
        {
            if (LAusgangTableID > 0)
            {
                FillAusgang();
                string strSql = string.Empty;
                //Delete Lagerausgang Datensatz
                strSql = "Delete FROM LAusgang WHERE ID='" + LAusgangTableID + "'; ";

                //ArtikelVita 
                clsArtikelVita artV = new clsArtikelVita();
                artV._GL_User = this._GL_User;
                strSql = strSql + artV.GetSQLDeleteLagerAusgang(LAusgangTableID);

                //Artikel LAusgangTableID=0;
                strSql = strSql + "Update Artikel Set LAusgangTableID=0, BKZ=1 WHERE LAusgangTableID='" + LAusgangTableID + "';";

                if (clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "LagerAusgangDelete", this._GL_User.User_ID))
                {
                    //Add Logbucheintrag 
                    string myBeschreibung = "Lager - Ausgang gelöscht: NR [" + this.LAusgangID + "] / Mandant [" + MandantenID + "] / Arbeitsbereich  [" + AbBereichID + "] ";
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);
                    //manuelle Adressen für diesen Ausgang löschen
                    this.AdrManuell.TableName = "LAusgang";
                    this.AdrManuell.TableID = this.LAusgangTableID;
                    this.AdrManuell.DeleteAllByTableID();
                }
            }
        }
        ///<summary>clsLAusgang / GetNewLAusgangID</summary>
        ///<remarks>Die nächste frei LvsNr wird ermittelt.</remarks>
        public static decimal GetNewLAusgangID(Globals._GL_USER myGL_User, clsSystem mySystem)
        {
            Decimal decTmpID = 0;
            if (mySystem != null)
            {
                clsPrimeKeys Lager = new clsPrimeKeys();
                Lager.sys = mySystem;
                Lager.AbBereichID = mySystem.AbBereich.ID;
                Lager._GL_User = myGL_User;
                Lager.Mandanten_ID = mySystem.AbBereich.MandantenID;
                Lager.GetNEWLAusgangID();
                decTmpID = Lager.LAusgangID;
            }
            return decTmpID;
        }
        ///<summary>clsLAusgang / GetNextLAusgangsID</summary>
        ///<remarks></remarks>
        public void GetNextLAusgangsID(bool SearchDirection)
        {
            if (LAusgangID > 0)
            {
                //Buastelle Rücklieferung nicht mit anzeigen
                string strSql = string.Empty;
                strSql = "Select Top(1) LAusgangID FROM LAusgang WHERE IsRL=0 AND MandantenID='" + MandantenID + "' AND AbBereich='" + AbBereichID + "' ";

                if (SearchDirection)
                {
                    //forward
                    strSql = strSql + "AND LAusgangID<'" + LAusgangID + "'  ORDER BY LAusgangID DESC ";
                }
                else
                {
                    //back
                    strSql = strSql + "AND LAusgangID>'" + LAusgangID + "' ORDER BY LAusgangID ";
                }

                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
                if (strTmp != string.Empty)
                {
                    decimal decTmp = 0;
                    if (Decimal.TryParse(strTmp, out decTmp))
                    {
                        LAusgangID = decTmp;
                    }
                }
                this.GetLAusgangsTableID();
            }
            else
            {
                string strSql = string.Empty;
                if (SearchDirection)
                {
                    //forward
                    strSql = "Select Top(1) LAusgangID FROM LAusgang WHERE MandantenID='" + MandantenID + "' AND AbBereich='" + AbBereichID + "' ORDER BY LAusgangID ";
                }
                else
                {
                    //back
                    strSql = "Select Top(1) LAusgangID FROM LAusgang WHERE MandantenID='" + MandantenID + "' AND AbBereich='" + AbBereichID + "' ORDER BY LAusgangID DESC ";
                }

                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
                if (strTmp != string.Empty)
                {
                    decimal decTmp = 0;
                    if (Decimal.TryParse(strTmp, out decTmp))
                    {
                        this.LAusgangID = decTmp;
                    }
                }
                this.GetLAusgangsTableID();
            }
        }
        ///<summary>clsLAusgang / GetLEingangIDByLEingangTableID</summary>
        ///<remarks>.</remarks>
        public static decimal GetLAusgangTableIDByLAusgangID(decimal myBenutzerID, decimal myLAusgangID, clsSystem mySystem)
        {
            decimal myID = 0;
            if (myLAusgangID > 0)
            {
                string strSql = string.Empty;
                strSql = "SELECT ID FROM LAusgang WHERE LAusgangID=" + myLAusgangID +
                                                        " AND MandantenID=" + mySystem.AbBereich.MandantenID +
                                                        " AND AbBereich=" + mySystem.AbBereich.ID +
                                                        ";";
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, myBenutzerID);
                if (strTmp != string.Empty)
                {
                    myID = Convert.ToDecimal(strTmp);
                }
            }
            return myID;
        }
        ///<summary>clsLAusgang / GetEingangAuftraggeberForEingangAnzeigeAtDate</summary>
        ///<remarks>Die Funktion ermittelt die Auftraggeber der Eingänge für das gewählte 
        ///         Datum, die noch keine Lagereingangsanzeige erhalten haben</remarks>
        public static DataTable GetAusgangAuftraggeberForAusgangAnzeigeAtDate(decimal myUserID, DateTime myDate, decimal Auftraggeber = 0)
        {
            List<decimal> retList = new List<decimal>();
            string strSql = string.Empty;
            if (Auftraggeber == 0)
            {
                strSql = "SELECT DISTINCT  CAST(Datum as Date) as Datum " +
                                    ", Auftraggeber " +
                                     ", ADR.ViewID as ViewID" +
                                    ", '" + enumDokumentenArt.LagerAusgangAnzeigePerDay.ToString() + "' as DokumentArt " +
                                    "FROM LAusgang " +
                                    "Left join ADR on Auftraggeber=ADR.ID " +
                                        "WHERE (DATEDIFF(dd, Datum, CAST('" + myDate + "' as Date))>=0) " +
                                               "AND IsPrintAnzeige=0  AND Checked=1 " +
                                               "ORDER BY ViewID ;";
            }
            else
            {
                strSql = "SELECT DISTINCT  CAST(Datum as Date) as Datum " +
                                      ", Auftraggeber " +
                                       ", ADR.ViewID as ViewID" +
                                      ", '" + enumDokumentenArt.LagerAusgangAnzeigePerDay.ToString() + "' as DokumentArt " +
                                      "FROM LAusgang " +
                                      "Left join ADR on Auftraggeber=ADR.ID " +
                                          "WHERE (DATEDIFF(dd, Datum, CAST('" + myDate + "' as Date))=0) " +
                                                 "AND Checked=1 AND Auftraggeber=" + Auftraggeber;
            }
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myUserID, "Auftraggeber");
            return dt;
        }
        /// <summary>
        /// UpdatePrintStatus
        /// </summary>
        public void UpdatePrintStatus()
        {
            if (ExistLAusgangTableID())
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = "Select IsPrintDoc,IsPRintAnzeige,IsPrintLfs FROM LAusgang WHERE ID=" + LAusgangTableID + ";";

                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Lagerausgang");
                if (dt.Rows.Count > 0)
                {
                    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        this.IsPrintDoc = (bool)dt.Rows[i]["IsPrintDoc"];
                        this.IsPrintAnzeige = (bool)dt.Rows[i]["IsPrintAnzeige"];
                        this.IsPrintLfs = (bool)dt.Rows[i]["IsPrintLfs"];
                    }
                }
            }
        }


    }
}
