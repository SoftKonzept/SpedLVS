using LVS.sqlStatementCreater;
using LVS.ViewData;
using LVS.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace LVS
{
    public class clsLager
    {
        /**************************************** Bestandsarten  ***************************/
        public const string const_Bestandsart_Select = "-bitte wählen Sie-";
        public const string const_Bestandsart_Tagesbestand = "Tagesbestand";
        public const string const_Bestandsart_TagesbestandexSPL = "Tagesbestand [ohne SPL]";
        public const string const_Bestandsart_TagesbestandEmp = "Tagesendbestand[Empfänger]";
        public const string const_Bestandsart_TagesbestandAll = "Tagesbestand [Lager komplett]";
        public const string const_Bestandsart_TagesbestandAllExclDam = "Tagesbestand [Lager komplett (ohne Schaden)]";
        public const string const_Bestandsart_TagesbestandAllExclSPL = "Tagesbestand [Lager komplett (ohne SPL)]";
        public const string const_Bestandsart_TagesbestandAllExclDamSPL = "Tagesbestand [Lager komplett (ohne Schaden, SPL)]";
        public const string const_Bestandsart_TagesbestandAccrossAllWorkspaces = "Tagesbestand [über alle Arbeitsbereiche]";
        public const string const_Bestandsart_Inventur = "Inventur";
        public const string const_Bestandsart_SPL = "Sperrlager[SPL]";
        public const string const_Bestandsart_RL = "Rücklieferungen[RL]";
        public const string const_Bestandsart_DirectDelivery = "Direktanlieferungen";
        public const string const_Bestandsart_LagergeldTag = "LagergeldTag";


        public const string const_Bestandsart_ArtikelUnchecked_StoreIN = "Ungeprüfte Artikel im Eingang";
        public const string const_Bestandsart_ArtikelUnchecked_StoreOUT = "Ungeprüfte Artikel im Ausgang";
        public const string const_Bestandsart_Artikel_UncheckedStoreIN = "Artikel in offenen Eingängen";
        public const string const_Bestandsart_Artikel_UncheckedStoreOUT = "Artikel in offenen Ausgängen";
        public const string const_Bestandsart_StoreIN_Unchecked = "Nicht abgeschlossene Eingänge";
        public const string const_Bestandsart_StoreOUT_Unchecked = "Nicht abgeschlossene Ausgänge";


        public clsLEingang Eingang; // = new clsLEingang();
        public clsLAusgang Ausgang; // = new clsAuftrag();
        public clsLagerOrt LagerOrt;

        private clsArtikel artikel;
        public clsArtikel Artikel
        {
            get
            {
                artikel.InitClass(this._GL_User, this._GL_System);
                artikel.sys = this.sys;
                return artikel;
            }
            set
            {
                artikel = value;
            }
        }
        public clsSchaeden Schaeden;
        public clsGut Gut;
        public clsSPL SPL;
        public clsADR ADR;
        public clsDocScan DocScan;

        public ImageViewData ImageVD;

        public clsASNAction ASNAction;

        public clsSystem sys;

        public Globals._GL_SYSTEM _GL_System;
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


        private decimal _FirstLEingangTableIDByArbeitsbereich;
        public decimal FirstLEingangTableIDByArbeitsbereich
        {
            get
            {
                string strSql = string.Empty;
                strSql = "SELECT TOP(1)[ID], MAX([LEingangID]) as LEingangID " +
                                                    "FROM LEingang " +
                                                        "where " +
                                                            "AbBereich = " + (int)this.AbBereichID +
                                                            " GROUP by ID, LEingangID ";
                //" Order by ID desc ";
                DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, this.sys._GL_User.User_ID, "FirstEingang");
                decimal decTmp = 0;
                foreach (DataRow r in dt.Rows)
                {

                    decimal.TryParse(r["ID"].ToString(), out decTmp);
                }

                _FirstLEingangTableIDByArbeitsbereich = decTmp;
                return _FirstLEingangTableIDByArbeitsbereich;
            }
        }


        private decimal _LastLEingangTableIDByArbeitsbereich;
        public decimal LastLEingangTableIDByArbeitsbereich
        {
            get
            {
                string strSql = string.Empty;
                strSql = "SELECT TOP(1)[ID], MAX([LEingangID]) as LEingangID " +
                                                    "FROM LEingang " +
                                                        "where " +
                                                            "AbBereich = " + (int)this.AbBereichID +
                                                            " GROUP by ID, LEingangID " +
                                                            " Order by ID desc ";
                DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, this.sys._GL_User.User_ID, "LastEingang");
                decimal decTmp = 0;
                foreach (DataRow r in dt.Rows)
                {

                    decimal.TryParse(r["ID"].ToString(), out decTmp);
                }

                _LastLEingangTableIDByArbeitsbereich = decTmp;
                return _LastLEingangTableIDByArbeitsbereich;
            }
        }

        private decimal _NextLEingangTableIDByArbeitsbereich;
        public decimal NextLEingangTableIDByArbeitsbereich
        {
            get
            {
                string strSql = string.Empty;
                strSql = "SELECT TOP(1)[ID], MAX([LEingangID]) as LEingangID " +
                                                    "FROM LEingang " +
                                                        "where " +
                                                            "AbBereich = " + (int)this.AbBereichID +
                                                            " AND LEingangID>" + (int)this.Eingang.LEingangID +
                                                            " GROUP by ID, LEingangID ";
                //" Order by ID desc ";
                DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, this.sys._GL_User.User_ID, "NextEingang");
                decimal decTmp = 0;
                foreach (DataRow r in dt.Rows)
                {

                    decimal.TryParse(r["ID"].ToString(), out decTmp);
                }

                _NextLEingangTableIDByArbeitsbereich = decTmp;
                return _NextLEingangTableIDByArbeitsbereich;
            }
        }
        private decimal _ForwardLEingangTableIDByArbeitsbereich;
        public decimal ForwardLEingangTableIDByArbeitsbereich
        {
            get
            {
                string strSql = string.Empty;
                strSql = "SELECT TOP(1)[ID], MAX([LEingangID]) as LEingangID " +
                                                    "FROM LEingang " +
                                                        "where " +
                                                            "AbBereich = " + (int)this.AbBereichID +
                                                            " AND LEingangID<" + (int)this.Eingang.LEingangID +
                                                            " GROUP by ID, LEingangID " +
                                                            " Order by ID desc ";
                DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, this.sys._GL_User.User_ID, "ForwardEingang");
                decimal decTmp = 0;
                foreach (DataRow r in dt.Rows)
                {

                    decimal.TryParse(r["ID"].ToString(), out decTmp);
                }
                _ForwardLEingangTableIDByArbeitsbereich = decTmp;
                return _ForwardLEingangTableIDByArbeitsbereich;
            }
        }

        //Journal
        public bool bFilterJournal = false;
        List<string> listFilterGArten = new List<string>();

        private decimal _LEingangTableID;

        public decimal LEingangTableID
        {
            get
            {
                return _LEingangTableID;
            }
            set
            {
                _LEingangTableID = value;
                if (
                        (_LEingangTableID > 0) &&
                        (this.Eingang is clsLEingang) &&
                        ((!_LEingangTableID.Equals(this.Eingang.LEingangTableID)) || (this.Eingang.Stat == ClsStatus.initialized)) &&
                        (clsLEingang.ExistLEingangTableID(this.BenutzerID, _LEingangTableID))
                   )
                {
                    this.Eingang = new clsLEingang();
                    this.Eingang.InitDefaultClsEingang(this._GL_User, this.sys);
                    this.Eingang.LEingangTableID = _LEingangTableID;
                    this.Eingang.FillEingang();
                }
            }
        }
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
                if (
                        (_LAusgangTableID > 0) &&
                        (clsLAusgang.ExistLAusgangTableID(this.BenutzerID, _LAusgangTableID))
                   )
                {
                    this.Ausgang = new clsLAusgang();
                    this.Ausgang.LAusgangTableID = _LAusgangTableID;
                    this.Ausgang.FillAusgang();
                }
            }
        }

        private decimal _AbBereichID;
        public decimal AbBereichID
        {
            get { return _AbBereichID; }
            set
            {
                decimal decTmp = value;
                if (this.Eingang == null)
                {
                    this.InitSubClasses();
                }
                this.Eingang.AbBereichID = decTmp;
                this.Ausgang.AbBereichID = decTmp;
                _AbBereichID = decTmp;
            }
        }
        public decimal MandantenID { get; set; }
        public DateTime BestandVon { get; set; }
        public DateTime BestandBis { get; set; }
        public decimal BestandAdrID { get; set; }
        public decimal FilterTarifID { get; set; }
        public bool RLJournalExcl { get; set; }
        public bool SchadenJournalExcl { get; set; }
        public bool SPLEndbestandExcl { get; set; }

        //SearchFilter
        public Int32 FilterSearchSpace { get; set; }
        public decimal FilterAuftraggeber { get; set; }
        public decimal FilterArtikelID { get; set; }
        public decimal FilterLVSNr { get; set; }
        public decimal FilterLEingangID { get; set; }
        public decimal FilterLAusgangID { get; set; }
        public decimal FilterGutID { get; set; }
        public string FilterWerksnummer { get; set; }
        public string FilterProduktionsnummer { get; set; }
        public string FilterCharge { get; set; }
        public string FilterBestellnummer { get; set; }
        public decimal FilterDicke { get; set; }
        public decimal FilterBreite { get; set; }
        public decimal FilterHoehe { get; set; }
        public decimal FilterLaenge { get; set; }
        public decimal FilterBrutto { get; set; }
        public Int32 FilterArtikelFreigabe { get; set; }
        public DateTime Stichtag { get; set; }
        public Int32 FreieLagertage { get; set; }
        public string ErrorText { get; set; }
        /******************************************************************************************
         *                      const
         * ****************************************************************************************/
        //Default Filter
        public const decimal const_FilterAuftraggeber = 0;
        public const decimal const_FilterArtikelID = 0;
        public const decimal const_FilterLVSNr = 0;
        public const decimal const_FilterLEingangID = 0;
        public const decimal const_FilterLAusgangID = 0;
        public const decimal const_FilterGutID = 0;
        //public const string  const_FilterWerksnummer= string.Empty;
        //public const string  const_FilterProduktionsnummer = string.Empty;
        //public const string  const_FilterCharge = string.Empty;
        //public const string  const_FilterBestellnummer = string.Empty;
        public const decimal const_FilterDicke = 0;
        public const decimal const_FilterBreite = 0;
        public const decimal const_FilterHoehe = 0;
        public const decimal const_FilterLaenge = 0;
        public const decimal const_FilterBrutto = 0;
        public const Int32 const_FilterArtikelFreigabe = 0;


        public bool AcrossWorkArea { get; set; } = false;

        /*********************************************************************************************
         *                              Methoden / Procedure
         * ******************************************************************************************/
        ///<summary>clsLager / InitClass</summary>
        ///<remarks></remarks>
        ///
        public clsLager()
        { }

        public clsLager(WarehouseViewData myWarehouseVD) : this()
        {
            this._GL_System = new Globals._GL_SYSTEM();
            this.sys = new clsSystem();
            this._GL_User = myWarehouseVD.GLUser;

            decimal decTmp = 0;
            decimal.TryParse(myWarehouseVD.Workspace.Id.ToString(), out decTmp);
            this.AbBereichID = decTmp;

            decTmp = 0;
            decimal.TryParse(myWarehouseVD.Workspace.MandantId.ToString(), out decTmp);
            this.MandantenID = decTmp;

            this.FreieLagertage = 0;
            InitSubClasses();
            if (myWarehouseVD.EingangTableId > 0)
            {
                Eingang.LEingangTableID = myWarehouseVD.EingangTableId;
                Eingang.FillEingang();
                FillLagerDatenByEA(Eingang.LEingangTableID, 0);
            }
            if (myWarehouseVD.AusgangTableId > 0)
            {
                Ausgang.LAusgangTableID = myWarehouseVD.AusgangTableId;
                Ausgang.FillAusgang();
                FillLagerDatenByEA(0, Ausgang.LAusgangTableID);
            }
            if (myWarehouseVD.ArticleId > 0)
            {
                Artikel.ID = myWarehouseVD.ArticleId;
                Artikel.GetArtikeldatenByTableID();
                FillLagerDatenByArtikelId(Artikel.ID);
            }
            ASNAction = new clsASNAction();
            ASNAction.ASNActionProcessNr = myWarehouseVD.asnActionProcessId;
        }

        public void InitClass(Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSystem, clsSystem mySys)
        {
            this._GL_User = myGLUser;
            this._GL_System = myGLSystem;
            this.AbBereichID = mySys.AbBereich.ID;
            this.MandantenID = mySys.AbBereich.MandantenID;
            this.sys = mySys;
            this.FreieLagertage = 0;
            InitSubClasses();
        }
        ///<summary>clsLager / InitSubClasses</summary>
        ///<remarks></remarks>
        public void InitSubClasses()
        {
            Eingang = new clsLEingang();
            Eingang.sys = this.sys;
            Eingang._GL_User = this._GL_User;

            Ausgang = new clsLAusgang();
            Ausgang.Sys = this.sys;
            Ausgang._GL_User = this._GL_User;

            LagerOrt = new clsLagerOrt();
            LagerOrt._GL_User = this._GL_User;
            //exLagerOrt = new clsLagerOrt();

            Artikel = new clsArtikel(); // Artikel muss in Eingang und Ausgang Prüfen !!!!!
            Artikel.InitClass(this._GL_User, this._GL_System);
            Artikel.sys = this.sys;

            Gut = new clsGut();
            Gut.InitClass(this._GL_User, this._GL_System);

            Schaeden = new clsSchaeden(); //Schäden muss in Artikel
            Schaeden._GL_User = this._GL_User;

            SPL = new clsSPL();  //Schäden muss in SPL
            SPL.InitClass(this._GL_User);

            ADR = new clsADR();
            ADR.sys = this.sys;
            ADR.InitClass(this._GL_User, this._GL_System, 0, false);

            DocScan = new clsDocScan();
            DocScan.InitClass(this._GL_User, this._GL_System, this.sys);

            ImageVD = new ImageViewData(this._GL_User);

            ASNAction = new clsASNAction();
            ASNAction.InitClass(ref this._GL_User);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myArtikelId"></param>
        public void FillLagerDatenByArtikelId(decimal myArtikelId)
        {
            InitSubClasses();
            if (this.Artikel is clsArtikel)
            {
                this.Artikel._GL_System = this._GL_System;
                this.Artikel._GL_User = this._GL_User;
                this.Artikel.ID = myArtikelId;
                this.Artikel.GetArtikeldatenByTableID();
                this.LEingangTableID = this.Artikel.LEingangTableID;
                this.LAusgangTableID = this.Artikel.LAusgangTableID;
                this.AbBereichID = this.Artikel.AbBereichID;
                this.MandantenID = this.Artikel.MandantenID;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myArtikelId"></param>
        public void FillLagerDatenByEA(decimal myEingangTableId, decimal myAusgangTableId)
        {
            InitSubClasses();
            this.LEingangTableID = myEingangTableId;
            if (this.LEingangTableID > 0)
            {
                this.AbBereichID = this.Eingang.AbBereichID;
                this.MandantenID = this.Eingang.MandantenID;
                Eingang.lockEingang();
            }
            this.LAusgangTableID = myAusgangTableId;
            if (this.LAusgangTableID > 0)
            {
                this.AbBereichID = this.Ausgang.AbBereichID;
                this.MandantenID = this.Ausgang.MandantenID;
                Ausgang.lockAusgang();
            }
        }
        ///<summary>clsLager / UpdateArtikelLager</summary>
        ///< marks>Update eines Datensatzes in der DB über die ID.</remarks>
        public void FillLagerDaten(bool bInitSubCls)
        {
            if (bInitSubCls)
            {
                InitSubClasses();
            }
            this.AbBereichID = this._GL_System.sys_ArbeitsbereichID;
            Eingang.AbBereichID = this.AbBereichID;
            Eingang.MandantenID = this.MandantenID;
            Ausgang.AbBereichID = this.AbBereichID;
            Ausgang.MandantenID = this.MandantenID;

            if (clsLAusgang.ExistLAusgangTableID(this._GL_User.User_ID, LAusgangTableID))
            {
                Ausgang.LAusgangTableID = LAusgangTableID;
                Ausgang.lockAusgang();
                Ausgang.FillAusgang();
            }
            else
            {
                LAusgangTableID = 0;
                Ausgang.LAusgangTableID = LAusgangTableID;
            }
            if (clsLEingang.ExistLEingangTableID(this._GL_User.User_ID, LEingangTableID))
            {
                Eingang.LEingangTableID = LEingangTableID;
                Eingang.lockEingang();
                Eingang.FillEingang();
            }
            else
            {
                LEingangTableID = 0;
                Eingang.LEingangTableID = LEingangTableID;
            }
        }
        ///<summary>clsLager / GetSQLSelectForArtikelSearch</summary>
        ///<remarks></remarks>
        public static string GetSQLSelectForArtikelSearch(clsSystem mySystem, bool bAddSelected, bool bList = false)
        {
            string strSql = string.Empty;
            strSql = "Select " +
                                   "Artikel.ID as ArtikelID " +
                                   ", Artikel.LVS_ID as LVSNr " +
                                   ", Artikel.BKZ" +
                                   ", Artikel.Werksnummer" +
                                   ", Artikel.Produktionsnummer" +
                                   ", Artikel.Charge" +
                                   ", Artikel.Anzahl" +
                                   ", Artikel.Einheit" +
                                   ", Gueterart.Bezeichnung as Gut" +
                                   ", Artikel.Dicke" +
                                   ", Artikel.Breite" +
                                   ", Artikel.Laenge" +
                                   ", Artikel.Hoehe" +
                                   ", Cast(Artikel.Netto as int) as Netto" +
                                   ", Cast(Artikel.Brutto as int) as Brutto" +
                                   ", Artikel.exBezeichnung" +
                                   ", Artikel.Bestellnummer" +
                                   ", Artikel.exMaterialnummer" +
                                   ", Artikel.ArtIDRef " +
                                   ", LEingang.LEingangID as Eingang" +
                                   ", LEingang.LfsNr as Lieferschein" +
                                   ", CASE WHEN (LAusgang.LAusgangID >0) THEN LAusgang.LAusgangID ELSE 0 END as Ausgang" +
                                   ", CASE WHEN (LAusgang.LAusgangID >0) THEN LAusgang.Datum ELSE '' END as Ausgangsdatum" +
                                   ", ADR.ViewID as Auftraggeber" +
                                   //", Artikel.Werk"+
                                   ", Artikel.Halle" +
                                   ", Artikel.Reihe" +
                                //", Aritkel.Ebene"+
                                //", Artikel.Plazt"+                                 

                                //",CASE Artikel.LOTable " +
                                //    "WHEN '' THEN '' " +
                                //    "WHEN 'Werk' THEN (Select (Werk.Bezeichnung) " +
                                //                           "FROM Werk " +
                                //                           "WHERE Werk.ID= Artikel.LagerOrt) " +
                                //    "WHEN 'Halle' THEN (Select (Werk.Bezeichnung+' | ' + Halle.Bezeichnung) " +
                                //                           "FROM Halle " +
                                //                           "INNER JOIN Werk ON Werk.ID = Halle.WerkID " +
                                //                           "WHERE Halle.ID= Artikel.LagerOrt) " +
                                //    "WHEN 'Reihe' THEN (Select (Werk.Bezeichnung+' | ' + Halle.Bezeichnung+' | ' + Reihe.Bezeichnung) " +
                                //                           "FROM Reihe " +
                                //                           "INNER JOIN Halle ON Halle.ID = Reihe.HalleID " +
                                //                           "INNER JOIN Werk ON Werk.ID = Halle.WerkID " +
                                //                           "WHERE Reihe.ID= Artikel.LagerOrt) " +
                                //    "WHEN 'Ebene' THEN (Select (Werk.Bezeichnung+' | ' + Halle.Bezeichnung+' | ' + Reihe.Bezeichnung+' | ' " +
                                //                           "+ Ebene.Bezeichnung) " +
                                //                           "FROM  Ebene " +
                                //                           "INNER JOIN Reihe ON Reihe.ID = Ebene.ReiheID " +
                                //                           "INNER JOIN Halle ON Halle.ID = Reihe.HalleID " +
                                //                           "INNER JOIN Werk ON Werk.ID = Halle.WerkID " +
                                //                           "WHERE Ebene.ID= Artikel.LagerOrt) " +

                                //    "WHEN 'Platz' THEN (Select (Werk.Bezeichnung+' | ' + Halle.Bezeichnung+' | ' + Reihe.Bezeichnung+' | ' " +
                                //                           "+ Ebene.Bezeichnung+' | ' + Platz.Bezeichnung) " +
                                //                           "FROM Platz " +
                                //                           "INNER JOIN Ebene ON Ebene.ID = Platz.EbeneID " +
                                //                           "INNER JOIN Reihe ON Reihe.ID = Ebene.ReiheID " +
                                //                           "INNER JOIN Halle ON Halle.ID = Reihe.HalleID " +
                                //                           "INNER JOIN Werk ON Werk.ID = Halle.WerkID " +
                                //                           "WHERE Platz.ID= Artikel.LagerOrt) " +
                                //    "END as Lagerort" +
                                // 
                                ", Artikel.exInfo as Bemerkung" +
                                ", LEingang.WaggonNo" +
                                ", LEingang.Date as Eingangsdatum" +
                                ", CAST(DATEPART(YYYY, Artikel.LZZ) as varchar)+CAST(DATEPART(ISOWK, Artikel.LZZ)as varchar) as LZZ" +
                                ", Artikel.FreigabeAbruf as Freigabe" +
                                ", CASE WHEN (Artikel.Laenge>0) THEN CAST(Artikel.Dicke as varchar (20))+' x '+ CAST(CAST(Artikel.Breite as int) as varchar(20))+' x '+CAST(CAST(Artikel.Laenge as int) as varchar(20)) " +
                               " ELSE CAST(Artikel.Dicke as varchar (20))+' x '+ CAST(Cast(Artikel.Breite as int) as varchar(20)) " +
                               " END as Abmessung";
            if (bList == true)
                strSql += ", Artikel.Produktionsnummer as [Prod.-Nr.]";
            strSql += ", " + clsArtikel.GetStatusColumnSQL();
            if (bAddSelected == true)
                strSql += ", CAST(0 as Bit) as Selected ";
            //  
            strSql += ", Artikel.Info as Info" +
                      ", Artikel.CheckArt as IsArtikelECheck" +
                      ", LEingang.[Check] as IsEingangCheck " +

                                   " From Artikel " +
                                   "INNER JOIN LEingang ON LEingang.ID = Artikel.LEingangTableID " +
                                   "INNER JOIN ADR  ON ADR.ID=LEingang.Auftraggeber " +
                                   "LEFT JOIN Gueterart ON Gueterart.ID=Artikel.GArtID  " +
                                   "LEFT JOIN LAusgang ON LAusgang.ID = Artikel.LAusgangTableID " +
                                    " WHERE " +
                                         "LEingang.AbBereich=" + mySystem.AbBereich.ID +
                                         " AND LEingang.Mandant=" + mySystem.AbBereich.MandantenID + " ";
            if (bAddSelected == true)
                strSql += " AND Artikel.LAusgangTableID = 0";
            //else
            //    strSql += " WHERE ";
            return strSql;
        }
        ///<summary>clsLager / GetAllArtikeldatenLager</summary>
        ///<remarks>Ermittelt die Artikel für die entsprechende Artikel ID für die DISPO.</remarks>
        public static DataTable GetAllLagerdaten(clsSystem mySystem, Globals._GL_USER myGLUser, bool bAddSelected = false)
        {
            string strSql = string.Empty;
            DataTable dt = new DataTable();
            strSql = clsLager.GetSQLSelectForArtikelSearch(mySystem, bAddSelected);
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGLUser.User_ID, "Bestand");
            return dt;
        }
        ///<summary>clsLager / GetLagerDatenByFilter</summary>
        ///<remarks></remarks>
        public DataTable GetLagerDatenByFilter(clsSystem mySystem, bool bAddSelected = false)
        {
            string strSql = string.Empty;
            DataTable dt = new DataTable();
            strSql = clsLager.GetSQLSelectForArtikelSearch(mySystem, bAddSelected);
            //strSql = strSql + GetSqlFilterForSearch(!bAddSelected, bAddSelected);
            strSql = strSql + GetSqlFilterForSearch();
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Bestand");
            return dt;
        }
        ///<summary>clsLager / GetSqlFilterForSearch</summary>
        ///<remarks>Hier wird nun der Filter aus den Vorgaben aus zusammengestellt</remarks>
        // private string GetSqlFilterForSearch(bool addWhere = true, bool addAnd = false)
        private string GetSqlFilterForSearch()
        {
            string strSql = string.Empty;

            //if (addAnd)
            //{
            strSql = strSql + " ";

            //}
            //BKZ
            if (FilterSearchSpace > -1)
            {
                //if (strSql != string.Empty) { strSql = strSql + " AND "; }
                //strSql = strSql + " Artikel.BKZ=" + FilterSearchSpace + " ";
                switch (FilterSearchSpace)
                {
                    case 0:
                        //strSql = strSql + " Artikel.LAusgangTableID=0  ";
                        break;
                    case 1:
                        //strSql = strSql + " Artikel.LAusgangTableID>0 ";
                        break;
                }
            }
            //Auftraggeber
            if (FilterAuftraggeber != const_FilterAuftraggeber)
            {
                if (strSql != string.Empty) { strSql = strSql + " AND "; }
                strSql = strSql + " LEingang.Auftraggeber=" + FilterAuftraggeber + " ";
            }
            //ArtikelID
            if (FilterArtikelID != const_FilterArtikelID)
            {
                if (strSql != string.Empty) { strSql = strSql + " AND "; }
                strSql = strSql + " Artikel.ID=" + FilterArtikelID + " ";
            }
            //VLSNR
            if (FilterLVSNr != const_FilterLVSNr)
            {
                if (strSql != string.Empty) { strSql = strSql + " AND "; }
                strSql = strSql + " Artikel.LVS_ID=" + FilterLVSNr + " ";
            }
            //EIngangID
            if (FilterLEingangID != const_FilterLEingangID)
            {
                if (strSql != string.Empty) { strSql = strSql + " AND "; }
                strSql = strSql + " LEingang.LEingangID=" + FilterLEingangID + " ";
            }
            //AusgangID
            if (FilterLAusgangID != const_FilterLAusgangID)
            {
                if (strSql != string.Empty) { strSql = strSql + " AND "; }
                strSql = strSql + " LAusgang.LAusgangID=" + FilterLAusgangID + " ";
            }
            //Gueterart
            if (FilterGutID != const_FilterGutID)
            {
                if (strSql != string.Empty) { strSql = strSql + " AND "; }
                strSql = strSql + " Artikel.GArtID=" + FilterGutID + " ";
            }
            //Werksnummer
            if (FilterWerksnummer != string.Empty)
            {
                if (strSql != string.Empty) { strSql = strSql + " AND "; }
                strSql = strSql + " Artikel.Werksnummer LIKE '" + FilterWerksnummer + "%' ";
            }
            //Produktionsnummer
            if (FilterProduktionsnummer != string.Empty)
            {
                if (strSql != string.Empty) { strSql = strSql + " AND "; }
                strSql = strSql + " Artikel.Produktionsnummer LIKE '" + FilterProduktionsnummer + "%' ";
            }
            //Charge
            if (FilterCharge != string.Empty)
            {
                if (strSql != string.Empty) { strSql = strSql + " AND "; }
                strSql = strSql + " Artikel.Charge LIKE '" + FilterCharge + "%' ";
            }
            //Bestellnummer
            if (FilterBestellnummer != string.Empty)
            {
                if (strSql != string.Empty) { strSql = strSql + " AND "; }
                strSql = strSql + " Artikel.Bestellnummer LIKE '" + FilterBestellnummer + "%' ";
            }
            //Dicke
            if (FilterDicke != const_FilterDicke)
            {
                if (strSql != string.Empty) { strSql = strSql + " AND "; }
                strSql = strSql + " Artikel.Dicke ='" + FilterDicke.ToString().Replace(",", ".") + "' ";
            }
            //Breite
            if (FilterBreite != const_FilterBreite)
            {
                if (strSql != string.Empty) { strSql = strSql + " AND "; }
                strSql = strSql + " Artikel.Breite ='" + FilterBreite.ToString().Replace(",", ".") + "' ";
            }
            //Hoehe
            if (FilterHoehe != const_FilterHoehe)
            {
                if (strSql != string.Empty) { strSql = strSql + " AND "; }
                strSql = strSql + " Artikel.Hoehe ='" + FilterHoehe.ToString().Replace(",", ".") + "' ";
            }
            //Lanege
            if (FilterLaenge != const_FilterLaenge)
            {
                if (strSql != string.Empty) { strSql = strSql + " AND "; }
                strSql = strSql + " Artikel.Laenge ='" + FilterLaenge.ToString().Replace(",", ".") + "' ";
            }
            //Brutto
            if (FilterBrutto != const_FilterBrutto)
            {
                if (strSql != string.Empty) { strSql = strSql + " AND "; }
                strSql = strSql + " Artikel.Brutto ='" + FilterBrutto.ToString().Replace(",", ".") + "' ";
            }
            //Freigabe
            if (FilterArtikelFreigabe != const_FilterBrutto)
            {
                if (strSql != string.Empty) { strSql = strSql + " AND "; }
                switch (FilterArtikelFreigabe)
                {
                    case 1:
                        strSql = strSql + " Artikel.FreigabeAbruf =1";
                        break;
                    case 2:
                        strSql = strSql + " Artikel.FreigabeAbruf =0";
                        break;
                }
            }

            //
            //if (strSql != string.Empty && addWhere)
            //{
            //   strSql = " WHERE " + strSql;
            //}
            return strSql;
        }

        ///<summary>clsLager / GetJournalDaten</summary>
        ///<remarks>.</remarks>
        public DataTable GetJournalDaten(Int32 myJournalArtID, string myCustomGroupingSelection)
        {
            string strSql = string.Empty;
            string strSQL2 = string.Empty;
            DataTable dt = new DataTable();

            clsLager TmpLager = this;
            switch (myCustomGroupingSelection)
            {
                case viewSql_Journal.const_Customized_EingangCharge:
                    strSql = viewSql_Journal.GetCustomizedTransportRefEingang(myJournalArtID, TmpLager);
                    break;
                default:
                    strSql = viewSql_Journal.GetDefaultMainSQL(myJournalArtID, TmpLager);
                    strSQL2 = viewSql_Journal.GetSQLFilterSelection(myJournalArtID, TmpLager);
                    break;
            }
            strSql = strSql + strSQL2;
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Journal");
            if (dt.Rows.Count > 0)
            {
                if (dt.Columns["Auftraggeber"] != null)
                {
                    dt.Columns["Auftraggeber"].SetOrdinal(2);
                }
            }

            return dt;
        }
        ///<summary>clsLager / GetSQLMainBestandsdaten</summary>
        ///<remarks>.</remarks>
        private string GetSQLMainBestandsdaten()
        {
            string strSql = string.Empty;
            strSql = "Select " +
                            "CAST(a.ID as INT) as ArtikelID " +
                            ", CAST(a.LVS_ID as INT) as LVSNr " +
                            ", a.Werksnummer" +
                            ", a.Produktionsnummer" +
                            ", a.Charge" +
                            ", a.GArtId" +
                            ", e.Bezeichnung as Gut" +
                            ", (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Auftraggeber) as Auftraggeber" +
                            ", CASE " +
                                "WHEN a.LAusgangTableID>0 THEN (Select ADR.ViewID FROM ADR WHERE ADR.ID=c.Empfaenger) " +
                                "ELSE (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Empfaenger) " +
                                "END as Empfaenger" +
                            ", a.FreigabeAbruf as Freigabe" +
                            ", a.Anzahl" +
                            ", a.Einheit" +
                            ", a.Dicke" +
                            ", a.Breite" +
                            ", a.Laenge" +
                            ", a.Hoehe" +
                            ", a.Netto" +
                            ", a.Brutto" +
                            ", a.exBezeichnung" +
                            ", a.Bestellnummer" +
                            ", a.exMaterialnummer" +
                            ", CAST(b.LEingangID as INT) as Eingang" +
                            ", b.Date as 'Eingangsdatum'" +
                            ", CAST(MONTH(b.Date) as varchar)+'/'+CAST(YEAR(b.Date) as varchar)  as Eingangsmonat" +
                            ", b.LfsNr as Lieferschein" +
                            ", CAST(c.LAusgangID as INT) as Ausgang" +
                            ", c.Datum as 'Ausgangsdatum'" +
                            ", CAST(MONTH(c.Datum) as varchar)+'/'+CAST(YEAR(c.Datum) as varchar)  as Ausgangsmonat" +
                            ", CASE " +
                                "WHEN (c.Datum IS NULL) " +
                                "THEN CAST( DATEDIFF(day, CAST(b.Date as Date),CAST('" + this.Stichtag.Date.ToString() + "' as Date)) as INT)+1 " +
                                "ELSE CAST( DATEDIFF(day, CAST(b.Date as Date),CAST(c.Datum as Date)) as INT)+1 " +
                                "END as Lagerdauer " +
                            ", CASE " +
                                "WHEN(a.LAusgangTableID > 0) " +
                                "THEN " +
                                    "CASE " +
                                        "WHEN DATEDIFF(day, CAST(b.Date as Date),CAST('" + this.Stichtag.Date.ToString() + "' as Date))> 0 " +
                                        "THEN DATEDIFF(day, CAST(b.Date as Date),CAST('" + this.Stichtag.Date.ToString() + "' as Date))+1 " +
                                        "ELSE DATEDIFF(day, CAST(b.Date as Date),CAST(c.Datum as date))+1 " +
                                    "end " +
                                "ELSE " +
                                    "DATEDIFF(day, CAST(b.Date as Date), CAST('" + this.Stichtag.Date.ToString() + "' as Date)) + 1 " +
                                "END as LagerdauerST " +
                         ", Case " +
                            "WHEN (Werk<>'') AND (Halle<>'') AND (Reihe<>'') AND (Ebene<>'') AND (Platz<>'') THEN Werk+' | ' +Halle+' | '+Reihe+' | '+Ebene+' | '+Platz " +
                            "WHEN (Werk<>'') AND (Halle<>'') AND (Reihe<>'') AND (Ebene<>'') AND (Platz='')THEN Werk+' | ' +Halle+' | '+Reihe+' | '+Ebene " +
                            "WHEN (Werk<>'') AND (Halle<>'') AND (Reihe<>'') AND (Ebene='') AND (Platz='') THEN Werk+' | ' +Halle+' | '+Reihe " +
                            "WHEN (Werk<>'') AND (Halle<>'') AND (Reihe='') AND (Ebene='') AND (Platz='') THEN Werk+' | ' +Halle " +
                            "WHEN (Werk<>'') AND (Halle='') AND (Reihe='') AND (Ebene='') AND (Platz='')THEN Werk " +
                            "END as Lagerort " +
                        ",CASE " +
                             "WHEN EAAusgangAltLVS='0' " +
                             "THEN a.Info " +
                             "ELSE SUBSTRING(a.Info,1, PATINDEX('%- LVS-Ausgang:%', a.Info)) " +
                             "END as LargerortAltLvs " +
                        ", a.BKZ " +
                        ", b.DirectDelivery as DA" +
                        ", b.Retoure as RL" +
                        ", b.Vorfracht as VF" +
                        ", CASE " +
                            "WHEN b.LagerTransport IS NULL " +
                            "THEN CAST(0 as BIT) " +
                            "ELSE b.LagerTransport " +
                            "END as LT_Eingang" +
                        ", CASE " +
                            "WHEN c.LagerTransport IS NULL " +
                            "THEN CAST(0 as BIT) " +
                            "ELSE c.LagerTransport " +
                            "END as LT_Ausgang";
            strSql +=                // 
                                ", a.Werk" +
                                ", a.Halle" +
                                ", a.Reihe" +
                                ", a.Ebene" +
                                ", a.Platz" +
                                ", a.exInfo as Bemerkung" +
                                ", a.intInfo " +
                                ", b.WaggonNo" +
                                ", CAST(DATEPART(YYYY, a.LZZ) as varchar)+CAST(DATEPART(ISOWK, a.LZZ)as varchar) as LZZ" +
                                ", a.ArtIDRef" +
                                //",CASE WHEN (SELECT COUNT (*) " +
                                //    " FROM Artikel a1 " +
                                //    " INNER JOIN LEingang c1 ON c1.ID=a1.LEingangTableID " +
                                //    " INNER JOIN SchadenZuweisung d1 ON d1.ArtikelID=a1.ID " +
                                //    " INNER JOIN Schaeden e1 ON e1.ID=d1.SchadenID " +
                                //    " WHERE a1.ID=a.ID) > 0 " +
                                //  " THEN (SELECT e2.Bezeichnung + char(10) " +
                                //      " FROM Artikel a2 " +
                                //      " INNER JOIN LEingang c2 ON c2.ID=a2.LEingangTableID " +
                                //      " LEFT OUTER JOIN SchadenZuweisung d2 ON d2.ArtikelID=a2.ID " +
                                //      " LEFT OUTER JOIN Schaeden e2 ON e2.ID=d2.SchadenID " +
                                //      " WHERE a2.ID=a.ID " +
                                //      " FOR XML PATH ('')) " +
                                //  " ELSE '' " +
                                //  " END as Schaden " +
                                //",(Select CAST(s.Datum as datetime) FROM Sperrlager s WHERE s.BKZ='IN' AND s.ArtikelID=a.ID) as SPL_IN " +
                                //",(Select CAST(s.Datum as datetime) FROM Sperrlager s WHERE s.BKZ='OUT' AND s.ArtikelID=a.ID) as SPL_OUT " +
                                " , CASE " +
                                " 	WHEN (a.Laenge>0) " +
                                        " THEN CAST(a.Dicke as varchar (20))+'x'+ CAST(a.Breite as varchar(20))+'x'+CAST(a.Laenge as varchar(20)) " +
                                    " 	ELSE CAST(a.Dicke as varchar (20))+'x'+ CAST(a.Breite as varchar(20)) " +
                                    " END as Abmessung " +
                                 " ,(CASE WHEN IsVerpackt = 1 THEN 'verpackt' + char(10) ELSE '' END) + " +
                                 " (CASE WHEN ((exInfo IS NOT NULL) AND (exInfo <> '')) THEN exInfo + char(10) ELSE '' END ) as Bemerkungen " +
                                ", " + clsArtikel.GetStatusColumnSQL("c", "b") + " " +
                                ", ' ' as iO " +
                                ", ' ' as neueReihe " +
                                ", a.EAEingangAltLVS " +
                                ", a.EAAusgangAltLVS " +
                                ", a.GlowDate as Glühdatum " +
                                ", b.ID as LEingangTableID " +
                                ", c.ID as LAusgnangTableID ";
            return strSql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strMyBestandArt"></param>
        /// <param name="myGArtID"></param>
        /// <param name="strSql"></param>
        /// <param name="bUseBKZ"></param>
        /// <returns></returns>
        public string GetSQLBestandsdaten2(string strMyBestandArt, decimal myGArtID, string strSql, bool bUseBKZ = true)
        {
            string strSql2 = string.Empty;
            switch (strMyBestandArt)
            {
                //Default
                case clsLager.const_Bestandsart_Select:
                    //case "-bitte wählen Sie-":
                    strSql2 = " From Artikel a " +
                              "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                              "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                              "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                              "WHERE ";

                    if (!AcrossWorkArea)
                    {
                        strSql2 += " b.AbBereich=" + AbBereichID + " AND ";
                    }

                    if (bUseBKZ)
                    {
                        strSql2 += " a.BKZ=1 AND a.CheckArt=1 AND b.[Check]=1 ";
                    }
                    else
                    {
                        strSql2 += "  a.CheckArt=1 AND b.[Check]=1 and (c.Checked is Null or c.Checked=0) ";
                    }

                    if (bFilterJournal)
                    {
                        strSql2 = strSql2 + " AND b.Auftraggeber=" + BestandAdrID + " " +
                                           " AND a.GArtID IN (" + this.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString + ") ";
                    }
                    else
                    {
                        if (myGArtID > 0)
                        {
                            strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                        }
                    }
                    break;

                //Inventur ist gleich Tagesbestand incl. SPL
                case clsLager.const_Bestandsart_Inventur:
                    //case "Inventur":
                    strSql2 = " From Artikel a " +
                              "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                              "LEFT JOIN Gueterart e ON e.ID=a.GArtID " +
                              "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                              "WHERE ";
                    if (this.BestandAdrID > 0)
                    {
                        strSql2 = strSql2 + "b.Auftraggeber=" + BestandAdrID + " AND ";
                    }
                    if (bUseBKZ)
                    {
                        strSql2 += " a.BKZ=1 AND a.CheckArt=1 AND b.[Check]=1 ";
                    }
                    else
                    {
                        strSql2 += " a.CheckArt=1 AND b.[Check]=1 and (c.Checked is Null or c.Checked=0) ";
                    }
                    if (myGArtID > 0)
                    {
                        strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                    }
                    strSql2 += " AND b.DirectDelivery=0  AND b.AbBereich=" + AbBereichID + " ";
                    break;

                //Tagesbestand
                /**********************************************************************************************************
                 * Die Abfrage ist in zwei Filter aufgebaut:
                 * 1. Alle Eingänge vor dem Starpunkt des Beobachtungszeitraums. Diese können folgende Merkmale aufweisen:
                 *  - Artikel befindet sich auch zum Zeitpunkt der Abfrage noch im Lager
                 *  - Auslagerung des Artikels hat im Zeitraum zwischen Stichtag und Zeitpunkt Abfrage stattgefunden
                 *  *******************************************************************************************************/
                case clsLager.const_Bestandsart_Tagesbestand:
                    //case "Tagesbestand":
                    strSql2 = " From Artikel a " +
                              "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                              "LEFT JOIN Gueterart e ON e.ID=a.GArtID " +
                              "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                              "WHERE ";

                    //" b.AbBereich=" + AbBereichID + " AND " +

                    if (!AcrossWorkArea)
                    {
                        strSql2 += " b.AbBereich=" + AbBereichID + " AND ";
                    }

                    strSql2 += "(( " +
                                    "b.Auftraggeber=" + BestandAdrID + " ";
                    if (bUseBKZ)
                    {
                        strSql2 += " AND a.BKZ=1 AND a.CheckArt=1 AND b.[Check]=1 ";
                    }
                    else
                    {
                        strSql2 += " AND a.CheckArt=1 AND b.[Check]=1 and (c.Checked is Null or c.Checked=0) ";
                    }
                    //"AND b.Mandant=" + MandantenID + " " +
                    strSql2 += " AND b.DirectDelivery=0  AND b.AbBereich=" + AbBereichID + " " +
                              "AND b.Date <'" + BestandVon.Date.AddDays(1).ToShortDateString() + "' ";
                    //"AND b.Date <'" + BestandVon.Date.ToShortDateString() + "' " ;
                    if (bFilterJournal)
                    {
                        if (this.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != string.Empty)
                        {
                            strSql2 = strSql2 + " AND a.GArtID IN (" + this.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString + ") ";
                        }
                    }
                    else
                    {
                        if (myGArtID > 0)
                        {
                            strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                        }
                    }
                    strSql2 = strSql2 +
                    ") " +
                    "OR " +
                    "(" +
                          "b.Auftraggeber=" + BestandAdrID + " ";
                    if (bUseBKZ)
                    {
                        strSql2 += " AND a.BKZ=0 AND a.CheckArt=1 AND b.[Check]=1 ";
                    }
                    else
                    {
                        if (myGArtID > 0)
                        {
                            strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                        }
                    }
                    //"AND b.Mandant=" + MandantenID + " " +
                    strSql2 += " AND b.DirectDelivery=0 AND b.AbBereich=" + AbBereichID + " " +
                               " AND c.Datum>='" + BestandVon.Date.AddDays(1).ToShortDateString() + "' " +
                               " AND b.Date <'" + BestandVon.Date.AddDays(1).ToShortDateString() + "' ";
                    if (bFilterJournal)
                    {
                        if (this.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != string.Empty)
                        {
                            strSql2 = strSql2 + " AND a.GArtID IN (" + this.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString + ") ";
                        }
                    }
                    else
                    {
                        if (myGArtID > 0)
                        {
                            strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                        }
                    }
                    strSql2 = strSql2 +
                    "))";
                    //ohne sPL
                    if (this.sys != null)
                    {
                        if (this.sys.Client.Modul.Lager_Bestandsliste_TagesbestandOhneSPL)
                        {
                            strSql2 = strSql2 + " AND a.ID NOT IN (" +
                                                                   "SELECT a.ArtikelID FROM Sperrlager a WHERE a.BKZ = 'IN' AND a.ID NOT IN " +
                                                                         "(SELECT DISTINCT c.SPLIDIn FROM Sperrlager c WHERE c.SPLIDIn>0)" +
                                                                 ");";
                        }
                    }
                    break;

                //---nach Empfänger
                case clsLager.const_Bestandsart_TagesbestandEmp:
                    strSql2 = " From Artikel a " +
                              "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                              "LEFT JOIN Gueterart e ON e.ID=a.GArtID " +
                              "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                              "WHERE ";

                    //" b.AbBereich=" + AbBereichID + " AND " +
                    if (!AcrossWorkArea)
                    {
                        strSql2 += " b.AbBereich=" + AbBereichID + " AND ";
                    }

                    strSql2 += "(( " +
                                    "b.Empfaenger=" + BestandAdrID + " ";
                    if (bUseBKZ)
                    {
                        strSql2 += " AND a.BKZ=1 AND a.CheckArt=1 AND b.[Check]=1 ";
                    }
                    else
                    {
                        strSql2 += " AND a.CheckArt=1 AND b.[Check]=1 and (c.Checked is Null or c.Checked=0) ";
                    }
                    //"AND b.Mandant=" + MandantenID + " " +
                    strSql2 += " AND b.DirectDelivery=0  AND b.AbBereich=" + AbBereichID + " " +
                              "AND b.Date <'" + BestandVon.Date.AddDays(1).ToShortDateString() + "' ";
                    //"AND b.Date <'" + BestandVon.Date.ToShortDateString() + "' " ;
                    if (bFilterJournal)
                    {
                        if (this.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != string.Empty)
                        {
                            strSql2 = strSql2 + " AND a.GArtID IN (" + this.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString + ") ";
                        }
                    }
                    else
                    {
                        if (myGArtID > 0)
                        {
                            strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                        }
                    }
                    strSql2 = strSql2 +
                    ") " +
                    "OR " +
                    "(" +
                          "b.Empfaenger=" + BestandAdrID + " ";
                    if (bUseBKZ)
                    {
                        strSql2 += " AND a.BKZ=0 AND a.CheckArt=1 AND b.[Check]=1 ";
                    }
                    else
                    {
                        if (myGArtID > 0)
                        {
                            strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                        }
                    }
                    //"AND b.Mandant=" + MandantenID + " " +
                    strSql2 += " AND b.DirectDelivery=0 AND b.AbBereich=" + AbBereichID + " " +
                               " AND c.Datum>='" + BestandVon.Date.AddDays(1).ToShortDateString() + "' " +
                               " AND b.Date <'" + BestandVon.Date.AddDays(1).ToShortDateString() + "' ";
                    if (bFilterJournal)
                    {
                        if (this.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != string.Empty)
                        {
                            strSql2 = strSql2 + " AND a.GArtID IN (" + this.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString + ") ";
                        }
                    }
                    else
                    {
                        if (myGArtID > 0)
                        {
                            strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                        }
                    }
                    strSql2 = strSql2 +
                    "))";
                    //ohne sPL
                    if (this.sys != null)
                    {
                        if (this.sys.Client.Modul.Lager_Bestandsliste_TagesbestandOhneSPL)
                        {
                            strSql2 = strSql2 + " AND a.ID NOT IN (" +
                                                                   "SELECT a.ArtikelID FROM Sperrlager a WHERE a.BKZ = 'IN' AND a.ID NOT IN " +
                                                                         "(SELECT DISTINCT c.SPLIDIn FROM Sperrlager c WHERE c.SPLIDIn>0)" +
                                                                 ");";
                        }
                    }
                    break;

                case clsLager.const_Bestandsart_TagesbestandexSPL:
                    //case "Tagesbestand [ohne SPL]":
                    strSql2 = " From Artikel a " +
                              "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                              "LEFT JOIN Gueterart e ON e.ID=a.GArtID " +
                              "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                              "WHERE ";

                    // " b.AbBereich=" + AbBereichID + " AND " +
                    if (!AcrossWorkArea)
                    {
                        strSql2 += " b.AbBereich=" + AbBereichID + " AND ";
                    }


                    strSql2 += "(( " +
                                    "b.Auftraggeber=" + BestandAdrID + " ";
                    if (bUseBKZ)
                    {
                        strSql2 += " AND a.BKZ=1 AND a.CheckArt=1 AND b.[Check]=1 ";
                    }
                    else
                    {
                        strSql2 += " AND a.CheckArt=1 AND b.[Check]=1 and (c.Checked is Null or c.Checked=0) ";
                    }
                    //"AND b.Mandant=" + MandantenID + " " +
                    strSql2 += " AND b.DirectDelivery=0  AND b.AbBereich=" + AbBereichID + " " +
                              "AND b.Date <'" + BestandVon.Date.AddDays(1).ToShortDateString() + "' ";
                    //"AND b.Date <'" + BestandVon.Date.ToShortDateString() + "' " ;
                    if (bFilterJournal)
                    {
                        if (this.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != string.Empty)
                        {
                            strSql2 = strSql2 + " AND a.GArtID IN (" + this.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString + ") ";
                        }
                    }
                    else
                    {
                        if (myGArtID > 0)
                        {
                            strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                        }
                    }
                    strSql2 = strSql2 +
                    ") " +
                    "OR " +
                    "(" +
                          "b.Auftraggeber=" + BestandAdrID + " ";
                    if (bUseBKZ)
                    {
                        strSql2 += " AND a.BKZ=0 AND a.CheckArt=1 AND b.[Check]=1 ";
                    }
                    else
                    {
                        if (myGArtID > 0)
                        {
                            strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                        }
                    }
                    //"AND b.Mandant=" + MandantenID + " " +
                    strSql2 += " AND b.DirectDelivery=0 AND b.AbBereich=" + AbBereichID + " " +
                               " AND c.Datum>='" + BestandVon.Date.AddDays(1).ToShortDateString() + "' " +
                               " AND b.Date <'" + BestandVon.Date.AddDays(1).ToShortDateString() + "' ";
                    if (bFilterJournal)
                    {
                        if (this.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != string.Empty)
                        {
                            strSql2 = strSql2 + " AND a.GArtID IN (" + this.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString + ") ";
                        }
                    }
                    else
                    {
                        if (myGArtID > 0)
                        {
                            strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                        }
                    }
                    strSql2 = strSql2 +
                    ")) " +
                   " AND a.ID NOT IN (" +
                                        "SELECT a.ArtikelID FROM Sperrlager a WHERE a.BKZ = 'IN' AND a.ID NOT IN " +
                                                "(SELECT DISTINCT c.SPLIDIn FROM Sperrlager c WHERE c.SPLIDIn>0)" +
                                        ");";

                    break;

                //Tagesbestand LAger komplett
                /**********************************************************************************************************
                 * Die Abfrage ist in zwei Filter aufgebaut:
                 * 1. Alle Eingänge vor dem Starpunkt des Beobachtungszeitraums. Diese können folgende Merkmale aufweisen:
                 *  - Artikel befindet sich auch zum Zeitpunkt der Abfrage noch im Lager
                 *  - Auslagerung des Artikels hat im Zeitraum zwischen Stichtag und Zeitpunkt Abfrage stattgefunden
                 *  *******************************************************************************************************/
                case clsLager.const_Bestandsart_TagesbestandAll:
                    strSql2 = SqlTagesbestandKomplett(myGArtID, bUseBKZ);
                    break;
                case clsLager.const_Bestandsart_TagesbestandAllExclDam:
                    strSql2 = SqlTagesbestandKomplett(myGArtID, bUseBKZ);
                    strSql2 += " AND a.ID NOT IN (SELECT DISTINCT ArtikelID FROM SchadenZuweisung) ";
                    break;

                case clsLager.const_Bestandsart_TagesbestandAllExclSPL:
                    strSql2 = SqlTagesbestandKomplett(myGArtID, bUseBKZ);
                    strSql2 += " AND a.ID NOT IN (" +
                                                    "SELECT a.ArtikelID FROM Sperrlager a WHERE a.BKZ = 'IN' AND a.ID NOT IN " +
                                                            "(SELECT DISTINCT c.SPLIDIn FROM Sperrlager c WHERE c.SPLIDIn>0)" +
                                                    ") ";
                    break;

                case clsLager.const_Bestandsart_TagesbestandAllExclDamSPL:
                    strSql2 = SqlTagesbestandKomplett(myGArtID, bUseBKZ);
                    strSql2 += " AND a.ID NOT IN (SELECT DISTINCT ArtikelID FROM SchadenZuweisung) ";
                    strSql2 += " AND a.ID NOT IN (" +
                                                    "SELECT a.ArtikelID FROM Sperrlager a WHERE a.BKZ = 'IN' AND a.ID NOT IN " +
                                                            "(SELECT DISTINCT c.SPLIDIn FROM Sperrlager c WHERE c.SPLIDIn>0)" +
                                                    ") ";
                    break;

                case clsLager.const_Bestandsart_TagesbestandAccrossAllWorkspaces:

                    break;


                //Sperrlager
                case clsLager.const_Bestandsart_SPL:
                    //case "Sperrlager[SPL]":
                    strSql = string.Empty;
                    strSql2 = GetSperrlagerSQL();
                    break;

                //Rücklieferungen
                case clsLager.const_Bestandsart_RL:
                    //case "Rücklieferungen[RL]":
                    strSql2 = " From Artikel a " +
                             "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                             "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                             "INNER JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                             "WHERE " +
                                    //" b.Auftraggeber=" + BestandAdrID +
                                    //"AND "+
                                    " b.AbBereich=" + AbBereichID + " " +
                                    "AND (c.Datum between '" + BestandVon.Date.ToShortDateString() + "' AND '" + BestandBis.Date.AddDays(1).ToShortDateString() + "') " +
                                    " AND a.CheckArt=1 " +
                                    " AND c.IsRL=1 ";
                    if (myGArtID > 0)
                    {
                        strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                    }
                    break;

                //Direktanlieferungen
                case clsLager.const_Bestandsart_DirectDelivery:
                    //case "Direktanlieferungen":
                    strSql2 = " From Artikel a " +
                             "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                             "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                             "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                             "WHERE " +
                             // b.Date >= '" + BestandVon + "' AND b.Date <'" + BestandBis + "' " +
                             " (b.Date between '" + BestandVon.Date.ToShortDateString() + "' AND '" + BestandBis.Date.AddDays(1).ToShortDateString() + "') " +
                             " AND b.Auftraggeber=" + BestandAdrID +
                             "  AND b.DirectDelivery=1 " +//"AND b.Mandant=" + MandantenID + " " +
                                    " AND b.AbBereich=" + AbBereichID + " ";
                    if (bFilterJournal)
                    {
                        if (this.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != string.Empty)
                        {
                            strSql2 = strSql2 + // " AND b.Auftraggeber=" + BestandAdrID + " " +
                                               " AND a.GArtID IN (" + this.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString + ") ";
                        }
                    }
                    else
                    {
                        if (myGArtID > 0)
                        {
                            strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                        }
                    }
                    break;

                //Ungeprüfte Artikel im Eingang
                case clsLager.const_Bestandsart_ArtikelUnchecked_StoreIN:
                    //case "Ungeprüfte Artikel im Eingang":
                    strSql2 = " From Artikel a " +
                             "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                             "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                             "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                             "WHERE " +
                                "a.CheckArt=0 AND a.LEingangTableID>0 " +
                                    //"AND b.Mandant=" + MandantenID + " " +
                                    " AND b.AbBereich=" + AbBereichID + " ";
                    if (BestandAdrID > 0)
                    {
                        strSql2 += " AND b.Auftraggeber=" + BestandAdrID;
                    }
                    if (myGArtID > 0)
                    {
                        strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                    }
                    break;

                //Ungeprüfte Artikel in Ausgang
                case clsLager.const_Bestandsart_ArtikelUnchecked_StoreOUT:
                    //case "Ungeprüfte Artikel im Ausgang":
                    strSql2 = " From Artikel a " +
                             "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                             "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                             "INNER JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                             "WHERE " +
                                "a.LA_Checked=0 AND a.LAusgangTableID>0 " +
                                    " AND b.AbBereich=" + AbBereichID + " ";
                    if (BestandAdrID > 0)
                    {
                        strSql2 += " AND b.Auftraggeber=" + BestandAdrID;
                    }
                    if (myGArtID > 0)
                    {
                        strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                    }
                    break;

                //Artikel in offenen Eingngen
                //case "Artikel in offenen Eingängen":
                case clsLager.const_Bestandsart_Artikel_UncheckedStoreIN:
                    strSql2 = " From Artikel a " +
                             "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                             "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                             "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                             "WHERE " +
                                " b.[check]=0 AND a.LEingangTableID>0 " +
                                " AND b.AbBereich=" + AbBereichID + " ";
                    if (BestandAdrID > 0)
                    {
                        strSql2 += " AND b.Auftraggeber=" + BestandAdrID;
                    }
                    if (myGArtID > 0)
                    {
                        strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                    }
                    break;

                //Ungeprüfte Artikel in Ausgang
                case clsLager.const_Bestandsart_Artikel_UncheckedStoreOUT:
                    //case "Artikel in offenen Ausgängen":
                    strSql2 = " From Artikel a " +
                             "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                             "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                             "INNER JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                             "WHERE " +
                                "c.checked=0 AND a.LAusgangTableID>0 " +
                                " AND b.AbBereich=" + AbBereichID + " ";
                    if (BestandAdrID > 0)
                    {
                        strSql2 += "AND b.Auftraggeber=" + BestandAdrID;
                    }
                    if (myGArtID > 0)
                    {
                        strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                    }
                    break;

                //Nicht abgeschlossene Ein-/Ausgänge
                case clsLager.const_Bestandsart_StoreIN_Unchecked:
                    //case "Nicht abgeschlossene Eingänge":
                    strSql = string.Empty;
                    strSql2 = GetOffeneEingänge();
                    break;

                //Nicht abgeschlossene Ein-/Ausgänge
                //case "Nicht abgeschlossene Ausgänge":
                case clsLager.const_Bestandsart_StoreOUT_Unchecked:
                    strSql = string.Empty;
                    strSql2 = GetOffeneAusgänge();
                    break;
                //Nicht platzierte Artikel
                case "Nicht platzierte Artikel":
                    strSql2 = " From Artikel a " +
                                 "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                                 "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                                 "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                                 "WHERE " +
                                    "a.BKZ=1 AND (a.LagerOrt=0 OR a.LagerOrt is Null) " +
                                    "AND (a.LOTable='') " +
                                    "AND a.LVSNr_ALTLvs=0 " +
                                    "AND b.DirectDelivery=0 " +
                                    "AND b.AbBereich=" + AbBereichID + " ";
                    break;

                case clsLager.const_Bestandsart_LagergeldTag:
                    strSql2 += ", CASE " +
                                "WHEN (c.Datum IS NULL) " +
                                "THEN CAST( DATEDIFF(day, CAST(b.Date as Date),CAST('" + this.Stichtag.Date.ToString() + "' as Date)) as INT)+1 - " + this.FreieLagertage + " " +
                                "ELSE CAST( DATEDIFF(day, CAST(b.Date as Date),CAST(c.Datum as Date)) as INT)+1 - " + this.FreieLagertage + " " +
                                "END as AbrDauer ";
                    strSql2 += " From Artikel a " +
                                 "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                                 "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                                 "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                                " WHERE " +
                                    "(" +
                                      "(" +
                                        " a.CheckArt = 1 AND b.[Check] = 1 and(c.Checked is Null or c.Checked = 0) " +
                                        " AND b.DirectDelivery = 0 AND b.AbBereich = " + this.AbBereichID + " ";
                    if (BestandAdrID > 0)
                    {
                        strSql2 += " AND b.Auftraggeber=" + BestandAdrID + " ";
                    }
                    if (myGArtID > 0)
                    {
                        strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                    }
                    strSql2 += /*" AND DATEDIFF(dd, b.Date, '" + this.BestandBis.ToShortDateString() + "') >= " + this.FreieLagertage + " " +*/
                               " AND " +
                                    "(CASE " +
                                        "WHEN(c.Datum IS NULL) THEN(CAST(DATEDIFF(dd, b.Date, '" + this.BestandBis.ToShortDateString() + "') as INT) + 1) " +
                                        "ELSE (CAST(DATEDIFF(dd, b.Date, c.Datum) as INT) + 1) " +
                                        " END) >= " + this.FreieLagertage + " " +
                               " AND b.Date < '" + this.BestandVon.ToShortDateString() + "' " +
                            ") OR ( " +
                                 " a.CheckArt = 1 AND b.[Check] = 1 and c.Checked = 1 " +
                                 " AND b.DirectDelivery = 0  AND b.AbBereich =" + this.AbBereichID + " ";
                    if (BestandAdrID > 0)
                    {
                        strSql2 += " AND b.Auftraggeber=" + BestandAdrID + " ";
                    }
                    if (myGArtID > 0)
                    {
                        strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                    }

                    strSql2 += " AND c.Datum >= '" + this.BestandVon.ToShortDateString() + "' " +
                            //" AND DATEDIFF(dd, b.Date, '" + this.BestandBis.ToShortDateString() + "') >= " + this.FreieLagertage + " " +
                            " AND " +
                                    "(CASE " +
                                        "WHEN(c.Datum IS NULL) THEN(CAST(DATEDIFF(dd, b.Date, '" + this.BestandBis.ToShortDateString() + "') as INT) + 1) " +
                                        "ELSE (CAST(DATEDIFF(dd, b.Date, c.Datum) as INT) + 1) " +
                                        " END) >= " + this.FreieLagertage + " " +
                            " AND b.Date < '" + this.BestandVon.ToShortDateString() + "' " +
                    ")" +
                    ")" +
                    " AND a.ID NOT IN(SELECT a.ArtikelID FROM Sperrlager a WHERE a.BKZ = 'IN' AND a.ID NOT IN(SELECT DISTINCT c.SPLIDIn FROM Sperrlager c WHERE c.SPLIDIn > 0))";
                    break;
            }

            return strSql + strSql2; ;
        }
        /// <summary>
        ///             sql-String Tagesbestand Lager komplett
        /// </summary>
        /// <param name="myGArtID"></param>
        /// <param name="bUseBKZ"></param>
        /// <returns></returns>
        private string SqlTagesbestandKomplett(decimal myGArtID, bool bUseBKZ = true)
        {
            string strSql2 = string.Empty;
            strSql2 = " From Artikel a " +
                          "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                          "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                          "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                          "WHERE " +
                             " b.AbBereich=" + AbBereichID + " AND " +
                             "(( " +
                                "";
            if (bUseBKZ)
            {
                strSql2 += " a.BKZ=1 AND a.CheckArt=1 AND b.[Check]=1 ";
            }
            else
            {
                strSql2 += " a.CheckArt=1 AND b.[Check]=1 and (c.Checked is Null or c.Checked=0) ";
            }
            if (myGArtID > 0)
            {
                strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
            }
            strSql2 +=
            //"AND b.Mandant=" + MandantenID + " " +
            " AND b.DirectDelivery=0  AND b.Date <'" + BestandVon.Date.AddDays(1).ToShortDateString() + "' " +
        ") " +
        "OR " +
        "(" +
              //"b.Auftraggeber=" + BestandAdrID + " " +
              //"AND 
              "";
            if (bUseBKZ)
            {
                strSql2 += "a.BKZ=0 AND a.CheckArt=1 AND b.[Check]=1 ";
            }
            else
            {
                strSql2 += "a.CheckArt=1 AND b.[Check]=1 and (c.Checked=1) ";
            }
            if (myGArtID > 0)
            {
                strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
            }
            strSql2 +=
              //"AND b.Mandant=" + MandantenID + " " +
              //"AND (c.Datum between '" + BestandVon.Date.ToShortDateString() + "' AND '" + DateTime.Now.Date.AddDays(1).ToShortDateString() + "') " +
              //" AND b.DirectDelivery=0 AND (c.Datum between '" + BestandVon.Date.AddDays(1).ToShortDateString() + "' AND '" + DateTime.Now.Date.AddDays(1).ToShortDateString() + "') " +
              " AND c.Datum>='" + BestandVon.Date.AddDays(1).ToShortDateString() + "' " +
              //"AND b.Date <'" + BestandVon.Date.ToShortDateString() + "' " ;
              "AND b.Date <'" + BestandVon.Date.AddDays(1).ToShortDateString() + "' " +
        "))";
            return strSql2;
        }
        ///<summary>clsLager / GetBestandsdaten</summary>
        ///<remarks>.</remarks>
        public DataTable GetBestandsdaten(string strMyBestandArt, decimal myGArtID, bool bUseBKZ = true)
        {
            string strSql = string.Empty;
            string strSql2 = string.Empty;

            DataTable dt = new DataTable();

            ////--- mr erst mal nur 25.02.2025
            //if (strMyBestandArt.Equals(clsLager.const_Bestandsart_TagesbestandAccrossAllWorkspaces))
            //{
            //    //sqlCreater_Stocks sqlStock = new sqlCreater_Stocks(strMyBestandArt,
            //    //                                                    (int)this.BestandAdrID,
            //    //                                                    myGArtID,
            //    //                                                    (int)AbBereichID,
            //    //                                                    Stichtag,
            //    //                                                    BestandVon,
            //    //                                                    BestandBis,
            //    //                                                    bFilterJournal,
            //    //                                                    bUseBKZ);


            //    //strSql2 = sqlStock.sql_Statement;
            //}
            //else
            //{
            //strSql = GetSQLMainBestandsdaten();
            //strSql2 = string.Empty;
            //strSql2 = GetSQLBestandsdaten2(strMyBestandArt, myGArtID, strSql, bUseBKZ);
            //}


            sqlCreater_Stocks sqlStock = new sqlCreater_Stocks(strMyBestandArt,
                                                                (int)AbBereichID,
                                                                (int)BestandAdrID,
                                                                myGArtID,
                                                                Stichtag,
                                                                BestandVon,
                                                                BestandBis,
                                                                this.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString,
                                                                FreieLagertage,
                                                                bFilterJournal,
                                                                bUseBKZ,
                                                                this.sys.Client.Modul.Lager_Bestandsliste_TagesbestandOhneSPL
                                                                );
            strSql2 = string.Empty;
            strSql2 = sqlStock.sql_Statement;
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql2, BenutzerID, "Bestand");
            return dt;
        }
        ///<summary>clsLager / GetSperrlager</summary>
        ///<remarks>.</remarks>
        public string GetSperrlagerSQL(bool bForJournal = false)
        {
            string strSql = string.Empty;
            DataTable dt = new DataTable();
            if (!bForJournal)
            {
                strSql = GetSQLMainBestandsdaten();
            }
            //strSql += ", (Select TOP(1) Datum FROM Sperrlager WHERE ArtikelID=a.ID ORDER BY ID DESC) as 'Datum SPL' " +
            //            ", (Select TOP(1) BKZ FROM Sperrlager WHERE ArtikelID=a.ID ORDER BY ID DESC) as 'Buchung'" +
            //            ", (Select TOP(1) IsCustomCertificateMissing FROM Sperrlager WHERE ArtikelID=a.ID ORDER BY ID DESC) as 'Zert'" +
            //            ", (Select TOP(1) Sperrgrund FROM Sperrlager WHERE ArtikelID=a.ID ORDER BY ID DESC) as 'Sperrgrund'" +
            //            ", CAST(0 as bit) as 'ausbuchen'";

            strSql += " , spl.id as 'SplId' " +
                      " , spl.Datum as 'Datum SPL' " +
                      " , spl.IsCustomCertificateMissing as 'Zert' " +
                      " , spl.BKZ as 'Buchung' " +
                      " , spl.Sperrgrund as 'Sperrgrund' " +
                        ", CAST(0 as bit) as 'ausbuchen'";

            if (bForJournal)
            {
                strSql += ", (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Auftraggeber) as Auftraggeber";
            }

            strSql += " From Artikel a " +
                            "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                            "INNER JOIN Gueterart e ON e.ID = a.GArtID " +
                            "INNER JOIN Sperrlager spl on spl.ArtikelID = a.ID " +
                            "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                            "WHERE " +
                            //"(Select TOP(1) Datum FROM Sperrlager WHERE ArtikelID=a.ID ORDER BY ID DESC) > '" + BestandVon.Date.AddDays(-1) + "' AND " +
                            //"(Select TOP(1) Datum FROM Sperrlager WHERE ArtikelID=a.ID ORDER BY ID DESC) <'" + BestandBis.Date.AddDays(1) + "' AND" +
                            " b.AbBereich=" + AbBereichID + " ";
            if (this.BestandAdrID > 0)
            {
                strSql += " AND b.Auftraggeber =" + (Int32)this.BestandAdrID + " ";
            }


            if (bForJournal)
            {
                //strSql += " AND a.ID IN (SELECT a.ArtikelID FROM Sperrlager a WHERE a.BKZ = 'IN' AND a.ID IN " +
                strSql += " AND spl.ID IN (SELECT a.ID FROM Sperrlager a WHERE a.BKZ = 'IN' AND a.ID NOT IN " +
                            "(SELECT DISTINCT c.SPLIDIn FROM Sperrlager c WHERE " +
                                                                            "c.SPLIDIn>0 " +
                                                                            " AND c.Datum between '" + BestandVon.Date + "' AND '" + BestandBis.Date.AddDays(1) + "' ))";
            }
            else
            {
                //strSql += " AND a.ID IN (SELECT a.ArtikelID FROM Sperrlager a WHERE a.BKZ = 'IN' AND a.ID NOT IN " +
                strSql += " AND spl.ID IN (SELECT a.ID FROM Sperrlager a WHERE a.BKZ = 'IN' AND a.ID NOT IN " +
                           "(SELECT DISTINCT c.SPLIDIn FROM Sperrlager c WHERE c.SPLIDIn>0)) ";
            }
            return strSql;
        }
        ///<summary>clsLager / GetJournalSPL</summary>
        ///<remarks>.</remarks>
        //public string SQLGetJournalSPL()
        //{
        //    string strSql = string.Empty;
        //    strSql += ", (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Auftraggeber) as Auftraggeber " +
        //              ", spl.Datum as SPL_IN " +
        //              ",(Select CAST(s.Datum as datetime) FROM Sperrlager s WHERE s.BKZ='OUT' AND s.SPLIDIn=spl.ID) as SPL_OUT ";


        //    strSql += " From Artikel a " +
        //              "INNER JOIN Sperrlager spl on spl.ArtikelID = a.ID " +
        //              "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
        //              "INNER JOIN Gueterart e ON e.ID = a.GArtID " +
        //              "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
        //              "WHERE " +
        //                   " ( " +
        //        //--- Bsp: 1 -> IN vor dem Zeitraum und Out im Zeitraum 
        //                       "(Datediff(dd,'"+this.BestandVon.Date.ToShortDateString()+"',spl.Datum)<0) AND spl.BKZ='IN' " +
        //                       "AND " +
        //                       "(" +
        //                            "(Datediff(dd,(Select CAST(s.Datum as DATE) FROM Sperrlager s WHERE s.BKZ='OUT' AND s.SPLIDIn=spl.ID),'" + this.BestandBis.Date.ToShortDateString() + "')<0) " +
        //                        ")" +
        //                    ")" +
        //                  " OR " +
        //                    "(" +
        //        // --    Bsp: 2 -> IN und OUT im Zeitraum
        //                       "(Datediff(dd,'" + this.BestandVon.Date.ToShortDateString() + "',spl.Datum)>=0) AND spl.BKZ='IN' " +
        //                       " AND " +
        //                       "(Datediff(dd,(Select CAST(s.Datum as DATE) FROM Sperrlager s WHERE s.BKZ='OUT' AND s.SPLIDIn=spl.ID),'" + this.BestandBis.Date.ToShortDateString() + "')>=0) " +
        //                    ")" +
        //                    " OR " +
        //                   "(" +
        //        //-- Bsp: 3 -> IN im Zeitraum und Out nach dem Zeitraum
        //                       "(Datediff(dd,'" + this.BestandVon.Date.ToShortDateString() + "',spl.Datum)>=0) AND spl.BKZ='IN' " +
        //                       " AND " +
        //                       "(" +
        //                           "(Datediff(dd,(Select CAST(s.Datum as DATE) FROM Sperrlager s WHERE s.BKZ='OUT' AND s.SPLIDIn=spl.ID),'" + this.BestandBis.Date.ToShortDateString() + "')>0) " +
        //                           " OR " +
        //                           " ISNULL((Select s.ID FROM Sperrlager s WHERE s.BKZ='OUT' AND s.SPLIDIn=spl.ID),0)=0 " +
        //                        ")" +
        //                   ")" +
        //                   " OR " +
        //                   "( " +
        //        //--- Bsp: 4 -> IN und OUT liegen nicht im Zeitraum
        //                       "(Datediff(dd,'" + this.BestandVon.Date.ToShortDateString() + "',spl.Datum)<0) AND spl.BKZ='IN' " +
        //                       " AND " +
        //                       "(" +
        //                           " (Datediff(dd,'" + this.BestandBis.Date.ToShortDateString() + "',(Select CAST(s.Datum as DATE) FROM Sperrlager s WHERE s.BKZ='OUT' AND s.SPLIDIn=spl.ID))>0) " +
        //                           " OR " +
        //                           "ISNULL((Select s.ID FROM Sperrlager s WHERE s.BKZ='OUT' AND s.SPLIDIn=spl.ID),0)=0 " +
        //                       ")" +
        //                   ")"; 
        //    return strSql;
        //}
        ///<summary>clsLager / GetSperrlager</summary>
        ///<remarks>.</remarks>
        public DataTable GetSperrlager()
        {
            string strSql = string.Empty;
            DataTable dt = new DataTable();
            strSql = GetSperrlagerSQL();
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "SPL");
            return dt;
        }
        ///<summary>clsLager / GetBestandFreeForCall</summary>
        ///<remarks>Ermittelt den aktuellen Bestand der der Adresse ohne Freigabe für den Abruf</remarks>
        public DataTable GetBestandToSelectForFreeForCall(bool bUseBKZ = true)
        {
            string strSql = string.Empty;
            DataTable dt = new DataTable();
            if ((this.ADR != null) & (this.ADR.ID > 0))
            {
                strSql = "Select " +
                                "Cast(0 as bit) as 'Select'" +
                                ", a.ID as ArtikelID " +
                                ", a.LVS_ID as LVSNr " +
                                ", a.Produktionsnummer" +
                                ", CASE " +
                                    "WHEN a.LZZ IS NULL THEN '' " +
                                    "WHEN DATEDIFF(YYYY, a.LZZ,'01.01.1900')=0 THEN '' " +
                                    "ELSE " +
                                    "CAST(DATEPART(YYYY, a.LZZ) as varchar)+CAST(DATEPART(ISOWK, a.LZZ)as varchar) " +
                                   "END as LZZ " +
                                ", a.Dicke" +
                                ", a.Breite" +
                                ", a.Laenge" +
                                ", a.FreigabeAbruf as Freigabe" +
                                ", cast(a.Netto as int) as Netto" +
                                ", cast(a.Brutto as int) as Brutto" +
                                ", b.LEingangID as Eingang" +
                                ", b.LfsNr as Lieferschein" +
                                ", d.ViewID as Auftraggeber" +
                                ", a.BKZ" +
                                ", a.Charge" +
                                ", e.Bezeichnung as Gut" +
                                ", a.exMaterialnummer as MaterialNr" +
                                ", a.Halle " +
                                ", a.Reihe" +
                                ", b.Date as Eingangsdatum " +
                                ", CAST('' as nvarchar(1)) as Ausgang " +
                                ", b.WaggonNo " +
                                ", a.exInfo as Bemerkung " +
                                ", CASE " +
                                " WHEN (a.Laenge>0) " +
                                "THEN CAST(a.Dicke as varchar (20))+' x '+ CAST(CAST(a.Breite as int) as varchar(20))+' x '+CAST(CAST(a.Laenge as int) as varchar(20)) " +
                                "ELSE CAST(a.Dicke as varchar (20))+' x '+ CAST(Cast(a.Breite as int) as varchar(20)) " +
                                "END as Abmessung " +
                                ", b.ExTransportRef " +
                                ", " + clsArtikel.GetStatusColumnSQL("c", "b") +
                                ", b.ExTransportRef as[Trans.-Ref.] " +
                                " From Artikel a " +
                                "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                                "INNER JOIN ADR d ON d.ID=b.Auftraggeber " +
                                "INNER JOIN Gueterart e ON e.ID=a.GArtID  " +
                                "LEFT JOIN LAusgang c ON  a.LausgangTableID = c.ID " +
                                "WHERE ";

                if (bUseBKZ)
                {
                    strSql += "BKZ=1";
                }
                else
                {
                    strSql += "b.[check]=1 and (c.checked=0 or c.checked is null)";
                }
                strSql += " AND a.FreigabeAbruf=0 AND " +
                                           "d.ID=" + this.ADR.ID;

                strSql = strSql + " Order By MaterialNr,a.LZZ, a.Dicke, a.Breite;  ";

                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Bestand");
            }
            return dt;
        }
        ///<summary>clsLager / DoRL</summary>
        ///<remarks>Rücklieferung zum Stahllieferanten</remarks>
        public bool DoRL()
        {
            bool bReturn = false;
            this.Eingang.LEingangTableID = this.Artikel.LEingangTableID;
            this.Eingang.FillEingang();
            string strSQL = string.Empty;

            strSQL = "DECLARE @LAusgangTableID as INT; ";

            //Ausbuchung SPL
            strSQL = strSQL + this.SPL.SQLArtikelBookOutSPL(this.Artikel, true);


            //Ausgang RL erstellen
            this.Ausgang = new clsLAusgang();
            this.Ausgang._GL_User = this._GL_User;
            this.Ausgang.Sys = this.sys;
            this.Ausgang.LAusgangsDate = DateTime.Now;
            this.Ausgang.GewichtNetto = this.Artikel.Netto;
            this.Ausgang.GewichtBrutto = this.Artikel.Brutto;
            this.Ausgang.Auftraggeber = this.Eingang.Auftraggeber;
            this.Ausgang.Empfaenger = this.Eingang.Auftraggeber;
            this.Ausgang.Versender = 0;
            this.Ausgang.Entladestelle = 0;
            this.Ausgang.Lieferant = this.Eingang.Lieferant;
            //this.Ausgang.LfsNr = 0;
            //this.Ausgang.LfsDate = Globals.DefaultDateTimeMinValue;
            this.Ausgang.SLB = 0;
            this.Ausgang.MAT = string.Empty;
            this.Ausgang.Checked = true;
            this.Ausgang.SpedID = 0;
            this.Ausgang.KFZ = string.Empty;
            this.Ausgang.ASN = 0;
            this.Ausgang.Info = "Rücklieferung an SL";
            this.Ausgang.AbBereichID = this.sys.AbBereich.ID;
            this.Ausgang.MandantenID = this.sys.AbBereich.MandantenID;
            this.Ausgang.Termin = Globals.DefaultDateTimeMinValue;
            this.Ausgang.DirectDelivery = false;
            this.Ausgang.NeutralerAuftraggeber = 0;
            this.Ausgang.NeutralerEmpfaenger = 0;
            this.Ausgang.LagerTransport = false;
            this.Ausgang.WaggonNr = string.Empty;
            this.Ausgang.BeladeID = 0;
            this.Ausgang.IsPrintDoc = false;
            this.Ausgang.IsPrintLfs = false;
            this.Ausgang.exTransportRef = string.Empty;
            this.Ausgang.Fahrer = string.Empty;
            this.Ausgang.IsWaggon = false;
            this.Ausgang.IsRL = true;
            strSQL = strSQL + this.Ausgang.AddLAusgang_SQL();
            strSQL = strSQL + "SET @LAusgangTableID =(Select @@IDENTITY); ";
            //Update Artikeldaten
            strSQL = strSQL + " " + this.Artikel.SQLUpdateForRL();
            strSQL = strSQL + " SELECT @LAusgangTableID;";
            string strTmpAusgang = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSQL, "RL", BenutzerID);
            Decimal decTmp = 0;
            decimal.TryParse(strTmpAusgang, out decTmp);
            if (decTmp > 0)
            {
                bReturn = true;
                this.Ausgang.LAusgangTableID = decTmp;
                this.Ausgang.FillAusgang();
                this.Artikel.GetArtikeldatenByTableID();
                //Vita SPL in SQLArtikelBookOutSPL()
                //Ausgang
                clsArtikelVita.AddAuslagerungAuto(BenutzerID, this.Ausgang.LAusgangTableID, this.Ausgang.LAusgangID);
                //Artikel
                clsArtikelVita.AddArtikelLAusgangAuto(this._GL_User, this.Artikel.ID, this.Ausgang.LAusgangID);
                //AddArtikelLRL
                clsArtikelVita.AddArtikelLRL(this._GL_User, this.Artikel.ID, this.Ausgang.LAusgangID);
            }
            return bReturn;
        }
        /******************************************************************************************************************
         *                                      public static procedure
         * ***************************************************************************************************************/
        //********************************************************************************************************************* EINGANG
        ///<summary>clsLager / UpdateLEingangCheck</summary>
        ///<remarks></remarks>
        public static void UpdateLEingangCheck(decimal myDecBenutzer, bool myArtCheck, decimal myDecTableID)
        {
            clsLEingang.UpdateLEingangCheck(myDecBenutzer, myArtCheck, myDecTableID);
        }
        ///<summary>clsLager / GetNextLVSNr</summary>
        ///<remarks></remarks>
        public static decimal GetNextLVSNr(Globals._GL_USER myGL_User, clsSystem mySystem)
        {
            return clsLEingang.GetNextLVSNr(myGL_User, mySystem);
        }
        ///<summary>clsLager / GetNextLEingangID</summary>
        ///<remarks></remarks>
        public static decimal GetNewLEingangID(Globals._GL_USER myGL_User, clsSystem mySystem)
        {
            return clsLEingang.GetNewLEingangID(myGL_User, mySystem);
        }
        ///<summary>clsLager / GetNewLAusgangID</summary>
        ///<remarks></remarks>
        public static decimal GetNewLAusgangID(Globals._GL_USER myGL_User, clsSystem mySystem)
        {
            return clsLAusgang.GetNewLAusgangID(myGL_User, mySystem);
        }
        ///<summary>clsLager / GetLEingangIDByLEingangTableID</summary>
        ///<remarks>.</remarks>
        public static decimal GetLEingangIDByLEingangTableID(decimal myBenutzerID, decimal myLEingangTableID)
        {
            return clsLEingang.GetLEingangIDByLEingangTableID(myBenutzerID, myLEingangTableID);
        }
        ///<summary>clsLager / GetLEingangCheck</summary>
        ///<remarks></remarks>
        public static bool GetLEingangCheck(Globals._GL_USER myGL_User, decimal myLEingangTableID)
        {
            return clsLEingang.GetLEingangCheck(myGL_User, myLEingangTableID);
        }
        //****************************************************************************************************************  Ausgang
        ///<summary>clsLager / UpdateLAusgangSetAusgangAbgeschlossen</summary>
        ///<remarks></remarks>
        //public static void UpdateLAusgangSetAusgangAbgeschlossen(Globals._GL_USER myGL_User, decimal myLAusgangTableID, bool bAusgangAbgeschlossen)
        //{
        //    clsLAusgang.UpdateLAusgangSetAusgangAbgeschlossen(myGL_User, myLAusgangTableID, bAusgangAbgeschlossen);
        //}

        public static void UpdateLAusgangSetAusgangAbgeschlossen(ref clsLager myLager, bool bAusgangAbgeschlossen)
        {
            myLager.Ausgang.UpdateLAusgangSetAusgangAbgeschlossen(bAusgangAbgeschlossen);
            //clsLAusgang.UpdateLAusgangSetAusgangAbgeschlossen(myGL_User, myLAusgangTableID, bAusgangAbgeschlossen);
        }
        ///<summary>clsLager / UpdateLAusgangGewichtNetto</summary>
        ///<remarks></remarks>
        public static void UpdateLAusgangGewichtNetto(Globals._GL_USER myGL_User, decimal myLAusgangTableID, decimal myNetto)
        {
            clsLAusgang.UpdateLAusgangGewichtNetto(myGL_User, myLAusgangTableID, myNetto);
        }
        ///<summary>clsLager / UpdateLAusgangGewichtBrutto</summary>
        ///<remarks>.</remarks>
        public static void UpdateLAusgangGewichtBrutto(Globals._GL_USER myGL_User, decimal myLAusgangTableID, decimal myBrutto)
        {
            clsLAusgang.UpdateLAusgangGewichtBrutto(myGL_User, myLAusgangTableID, myBrutto);
        }
        ///<summary>clsLager / UpdateLAusgangArtikelChecked</summary>
        ///<remarks></remarks>
        public static void UpdateLAusgangArtikelChecked(Globals._GL_USER myGL_User, decimal myArtikelID, bool bArtChecked)
        {
            clsLAusgang.UpdateLAusgangArtikelChecked(myGL_User, myArtikelID, bArtChecked);
        }
        public static string SQLUpdateLAusgangArtikelChecked(Globals._GL_USER myGL_User, decimal myArtikelID, bool bArtChecked)
        {
            return clsLAusgang.UpdateLAusgangArtikelCheckedSQL(myGL_User, myArtikelID, bArtChecked);
        }

        public static bool SQLDoTransaction(string mySQL, Globals._GL_USER myGLUser)
        {
            bool bReturn = clsSQLcon.ExecuteSQLWithTRANSACTION(mySQL, "UpdateAusgang", myGLUser.User_ID);
            return bReturn;
        }
        ///<summary>clsLager / UpdateLAusgangArtikelDeleteLAusgang</summary>
        ///<remarks></remarks>
        public static void UpdateLAusgangArtikelDeleteLAusgang(Globals._GL_USER myGL_User, decimal myArtikelID)
        {
            clsLAusgang.UpdateLAusgangArtikelDeleteLAusgang(myGL_User, myArtikelID);
        }
        ///<summary>clsLager / UpdateLArtikelLAusgang</summary>
        ///<remarks></remarks>
        public static void UpdateLArtikelLAusgang(Globals._GL_USER myGL_User, decimal myLAusgangTableID, decimal myArtikelID, bool myUB)
        {
            clsLAusgang.UpdateLArtikelLAusgang(myGL_User, myLAusgangTableID, myArtikelID, myUB);
        }
        public static string SQLUpdateLArtikelLAusgang(Globals._GL_USER myGL_User, decimal myLAusgangTableID, decimal myArtikelID, bool myUB)
        {
            return clsLAusgang.UpdateLArtikelLAusgang_SQL(myGL_User, myLAusgangTableID, myArtikelID, myUB);
        }
        /******************************************************************************************************
         *                              ASN / DFÜ einlesen
         * 
         * ***************************************************************************************************/
        ///<summary>clsLager / InsertASN</summary>
        ///<remarks></remarks>
        public DataTable InsertASN(DataTable dtEingang, DataTable dtArtikel, bool myUseAutoRowAssign)
        {
            ErrorText = string.Empty;
            List<string> ListASNRead = new List<string>();
            DataTable dtCreated = new DataTable("Eingaenge");
            //Eingang
            for (Int32 i = 0; i <= dtEingang.Rows.Count - 1; i++)
            {
                string strASN = string.Empty;
                strASN = dtEingang.Rows[i]["ASN"].ToString();
                decimal decASNNr = 0;
                if (Decimal.TryParse(strASN, out decASNNr))
                {
                    //Check ASN noch Aktiv
                    clsASN tmpASN = new clsASN();
                    tmpASN.InitClass(this._GL_System, this._GL_User);
                    tmpASN.ID = decASNNr;
                    tmpASN.Fill();
                    if (tmpASN.IsRead)
                    {
                        //Message ASN wurde schon eingelesen
                        ErrorText = "Die DFÜ/ASN [" + strASN + "] ist nicht mehr verfügbar!";
                    }
                    else
                    {
                        dtArtikel.DefaultView.RowFilter = string.Empty;
                        dtArtikel.DefaultView.RowFilter = "ASN=" + strASN;
                        DataTable dtLEGroup = dtArtikel.DefaultView.ToTable();
                        string strLfs = string.Empty;
                        List<string> listLfs = new List<string>();

                        foreach (DataRow row in dtLEGroup.Rows)
                        {
                            strLfs = row["LfsNr"].ToString();
                            if (!strLfs.Equals(string.Empty))
                            {
                                if (!listLfs.Contains(strLfs))
                                {
                                    listLfs.Add(strLfs);
                                }
                            }
                        }

                        for (Int32 n = 0; n <= listLfs.Count - 1; n++)
                        {
                            string strLfsNr = string.Empty;
                            strLfsNr = listLfs[n].ToString();
                            decimal decTmp = 0;
                            decimal decTmpASN = 0;
                            Decimal.TryParse(strASN, out decTmpASN);
                            if (decTmpASN > 0)
                            {
                                try
                                {
                                    dtArtikel.DefaultView.RowFilter = string.Empty;
                                    //dtArtikel.DefaultView.RowFilter = "ASN=" + decTmpASN.ToString();
                                    dtArtikel.DefaultView.RowFilter = "ChildID='" + strASN + strLfsNr + "'";
                                    //dtArtikel.DefaultView.RowFilter = "LfsNr=" + strLfsNr;
                                    DataTable dtTmpArt = dtArtikel.DefaultView.ToTable();

                                    clsLEingang ein = new clsLEingang();
                                    ein.sys = this.sys;
                                    ein.LEingangDate = DateTime.Now;
                                    decTmp = 0;
                                    Decimal.TryParse(dtEingang.Rows[i]["Auftraggeber"].ToString(), out decTmp);
                                    ein.Auftraggeber = decTmp;
                                    decTmp = 0;
                                    Decimal.TryParse(dtEingang.Rows[i]["Empfaenger"].ToString(), out decTmp);
                                    ein.Empfaenger = decTmp;
                                    ein.AbBereichID = this.sys.AbBereich.ID;
                                    ein.MandantenID = this.sys.AbBereich.MandantenID;
                                    ein.LEingangLfsNr = strLfsNr;

                                    ein.KFZ = string.Empty;
                                    ein.WaggonNr = string.Empty;
                                    ein.Ship = string.Empty;

                                    //ein.WaggonNr = dtEingang.Rows[i]["WaggonNo"].ToString();
                                    //ein.IsWaggon = (dtEingang.Rows[i]["Transportmittel"].ToString() == "08");
                                    if (dtTmpArt.Rows.Count > 0)
                                    {
                                        ediHelper_712_TM e712TM = new ediHelper_712_TM(dtTmpArt.Rows[0]["TMS"].ToString(), dtTmpArt.Rows[0]["VehicleNo"].ToString());
                                        //ein.WaggonNr = dtTmpArt.Rows[0]["VehicleNo"].ToString();
                                        //ein.IsWaggon = (dtTmpArt.Rows[0]["TMS"].ToString() == "08");

                                        switch (e712TM.enumTMS)
                                        {
                                            case enumVDA4913_712F14_TMS.KFZ:
                                                ein.KFZ = e712TM.VehicleNo;
                                                ein.IsWaggon = false;
                                                ein.IsShip = false;
                                                break;
                                            case enumVDA4913_712F14_TMS.Waggonnummer:
                                                ein.WaggonNr = e712TM.VehicleNo;
                                                ein.IsWaggon = true;
                                                ein.IsShip = false;
                                                break;
                                            case enumVDA4913_712F14_TMS.Schiffsname:
                                                ein.Ship = e712TM.VehicleNo;
                                                ein.IsShip = true;
                                                ein.IsWaggon = false;
                                                //this.IsShip = ein.IsShip;
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        ein.WaggonNr = string.Empty;
                                        ein.IsWaggon = false;
                                    }
                                    ein.ExTransportRef = dtEingang.Rows[i]["ExTransportRef"].ToString();
                                    ein.ASNRef = dtEingang.Rows[i]["ASNRef"].ToString();
                                    ein.Lieferant = dtEingang.Rows[i]["Lieferantennummer"].ToString();
                                    //if (ein.Lieferant.Equals(string.Empty))
                                    //{ 

                                    //}
                                    ein.Checked = false;
                                    ein.DirektDelivery = false;
                                    ein.Retoure = false;
                                    ein.Vorfracht = false;
                                    ein.LagerTransport = false;

                                    ein.ASN = decTmpASN;

                                    clsPrimeKeys pk = new clsPrimeKeys();
                                    pk.sys = this.sys;
                                    pk.AbBereichID = this.sys.AbBereich.ID;
                                    pk.Mandanten_ID = this.sys.AbBereich.MandantenID;
                                    pk._GL_User = this._GL_User;
                                    pk.GetNEWLEingnagID();
                                    ein.LEingangID = pk.LEingangID;
                                    string strSql = string.Empty;

                                    strSql = "DECLARE @LEingangTableID as decimal(28,0); " +
                                        "DECLARE @LvsID as decimal(28,0); " +
                                        "DECLARE @ArtID as decimal(28,0); ";

                                    strSql = strSql +
                                            ein.AddLagerEingangSQL() +
                                            " Select @LEingangTableID= @@IDENTITY; ";

                                    //Vita
                                    string tmpAktion = enumLagerAktionen.EingangErstellt.ToString();
                                    strSql = strSql + " INSERT INTO ArtikelVita (TableID, TableName, Aktion, Datum, UserID, Beschreibung" +
                                                        ") " +
                                                        "VALUES (@LEingangTableID" +
                                                                ",'LEingang'" +
                                                                ",'" + tmpAktion + "'" +
                                                                ",'" + DateTime.Now + "'" +
                                                                ", " + BenutzerID +
                                                                ",'Lagereingang ['+CAST(@LEingangTableID as nvarchar)+'] autom. erstellt'" +
                                                                "); ";

                                    for (Int32 x = 0; x <= dtTmpArt.Rows.Count - 1; x++)
                                    {
                                        //Achtung die LVSNR wird innerhalber der SQL Anweisung ermittelt 
                                        //und muss nicht über die Klasse Primekeys ermittelt werden
                                        clsArtikel art = new clsArtikel();
                                        art.sys = this.sys;
                                        art.LVS_ID = 0;
                                        art.MandantenID = this.sys.AbBereich.MandantenID;
                                        art.AbBereichID = this.sys.AbBereich.ID;
                                        art.Eingang = ein.Copy();
                                        decTmp = 0;
                                        Decimal.TryParse(dtTmpArt.Rows[x]["GArtID"].ToString(), out decTmp);

                                        art.GArtID = decTmp;
                                        decTmp = 0;
                                        Decimal.TryParse(dtTmpArt.Rows[x]["Dicke"].ToString(), out decTmp);
                                        art.Dicke = decTmp;
                                        decTmp = 0;
                                        Decimal.TryParse(dtTmpArt.Rows[x]["Breite"].ToString(), out decTmp);
                                        art.Breite = decTmp;
                                        decTmp = 0;
                                        Decimal.TryParse(dtTmpArt.Rows[x]["Laenge"].ToString(), out decTmp);
                                        art.Laenge = decTmp;
                                        decTmp = 0;
                                        Decimal.TryParse(dtTmpArt.Rows[x]["Hoehe"].ToString(), out decTmp);
                                        art.Hoehe = decTmp;
                                        Int32 iTmp = 0;
                                        Int32.TryParse(dtTmpArt.Rows[x]["Anzahl"].ToString(), out iTmp);
                                        art.Anzahl = iTmp;
                                        art.Einheit = dtTmpArt.Rows[x]["Einheit"].ToString();

                                        if (art.GArtID > 0)
                                        {
                                            art.Einheit = art.GArt.Einheit;
                                            art.IsStackable = art.GArt.IsStackable;
                                        }
                                        art.gemGewicht = 0;
                                        decTmp = 0;
                                        Decimal.TryParse(dtTmpArt.Rows[x]["Netto"].ToString(), out decTmp);
                                        art.Netto = decTmp;
                                        decTmp = 0;
                                        Decimal.TryParse(dtTmpArt.Rows[x]["Brutto"].ToString(), out decTmp);
                                        art.Brutto = decTmp;
                                        art.Werksnummer = dtTmpArt.Rows[x]["Werksnummer"].ToString();
                                        art.Produktionsnummer = dtTmpArt.Rows[x]["Produktionsnummer"].ToString();
                                        art.exBezeichnung = dtTmpArt.Rows[x]["exBezeichnung"].ToString();
                                        art.Charge = dtTmpArt.Rows[x]["Charge"].ToString();
                                        art.Bestellnummer = dtTmpArt.Rows[x]["Bestellnummer"].ToString();
                                        art.exMaterialnummer = dtTmpArt.Rows[x]["exMaterialnummer"].ToString();
                                        art.TARef = dtTmpArt.Rows[x]["TARef"].ToString();
                                        iTmp = 0;
                                        Int32.TryParse(dtTmpArt.Rows[x]["Position"].ToString(), out iTmp);
                                        string strGlowDate = dtTmpArt.Rows[x]["GlowDate"].ToString();
                                        DateTime tmpGlowDate = Globals.DefaultDateTimeMinValue;
                                        DateTime.TryParse(strGlowDate, out tmpGlowDate);
                                        art.GlowDate = tmpGlowDate;


                                        art.Position = iTmp.ToString();
                                        art.GutZusatz = string.Empty;
                                        art.ArtIDRef = this.sys.Client.CreateArtikelIDRef(art);
                                        art.AuftragPosTableID = 0;
                                        art.ArtIDAlt = 0;
                                        art.Info = string.Empty;
                                        art.LagerOrt = 0;
                                        art.LagerOrtTable = string.Empty;
                                        art.exLagerOrt = string.Empty;
                                        art.ADRLagerNr = 0;
                                        art.FreigabeAbruf = false;
                                        art.LZZ = DateTime.ParseExact("01.01.2001", "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                        art.Werk = string.Empty;
                                        art.Halle = string.Empty;
                                        art.Ebene = string.Empty;
                                        art.Reihe = string.Empty;
                                        //if (myUseAutoRowAssign)
                                        //{
                                        //    if (art.GArt is clsGut)
                                        //    {
                                        //        clsReihe Reihe = new clsReihe();
                                        //        Reihe._GL_User = this._GL_User;
                                        //        art.Reihe = Reihe.GetVorschlag(art.Dicke.ToString()
                                        //                                        , art.Breite.ToString()
                                        //                                        , art.Laenge.ToString()
                                        //                                        , art.Hoehe.ToString()
                                        //                                        , art.Brutto.ToString()
                                        //                                        , art.GArtID.ToString());
                                        //    }
                                        //}
                                        art.Platz = string.Empty;
                                        art.exAuftrag = dtTmpArt.Rows[x]["exAuftrag"].ToString();
                                        art.exAuftragPos = dtTmpArt.Rows[x]["exAuftragPos"].ToString();
                                        art.ASNVerbraucher = dtTmpArt.Rows[x]["ASNVerbraucher"].ToString();
                                        art.Guete = dtTmpArt.Rows[x]["Guete"].ToString();

                                        strSql += art.AddArtikelLager_SQL(true, true);
                                        strSql = strSql + "SET @ArtID=(Select @@IDENTITY); ";
                                        //strSql = strSql + "SET  @LvsID=(SELECT MAX(Artikel.LVS_ID) FROM Artikel INNER JOIN LEingang ON LEingang.ID=Artikel.LEingangTableID WHERE LEingang.Mandant=" + ein.MandantenID + ") +1; ";
                                        strSql = strSql + "SET  @LvsID=" + clsPrimeKeys.SQLStringNewLVSNr(this.sys);

                                        strSql = strSql + "Update Artikel SET LVS_ID=@LvsID " +
                                                                             "WHERE ID = @ArtID; ";

                                        if (this.sys.Client.Modul.PrimeyKey_LVSNRUseOneIDRange)
                                        {
                                            strSql = strSql + "UPDATE PrimeKeys SET LvsNr = @LvsID;";
                                        }
                                        else
                                        {
                                            strSql = strSql + "UPDATE PrimeKeys SET LvsNr = @LvsID WHERE Mandanten_ID=" + ein.MandantenID + ";";
                                        }
                                        //strSql = strSql + " Select  @ArtikelTableID  = @@IDENTITY; ";
                                        //Vita
                                        tmpAktion = enumLagerAktionen.ArtikelAdd_Eingang.ToString();
                                        strSql = strSql + " INSERT INTO ArtikelVita (TableID, TableName, Aktion, Datum, UserID, Beschreibung" +
                                                                    ") " +
                                                                    "VALUES (@ArtID" +
                                                                            ",'Artikel'" +
                                                                            ",'" + tmpAktion + "'" +
                                                                            ",'" + DateTime.Now + "'" +
                                                                            ", " + BenutzerID +
                                                                            ", 'autom. Artikel hinzugefügt: LVS-NR [ ' + CAST((Select LVS_ID from Artikel where ID=@ArtID) as nvarchar) + ' ] / Eingang ID ['+CAST(@LEingangTableID as nvarchar)+']'" +
                                                                            "); ";
                                    } // schleife Artikel
                                    //Insert

                                    strSql = strSql + " Select @LEingangTableID";
                                    string strEID = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSql, "ASNInsert", BenutzerID);
                                    decTmp = 0;
                                    decimal.TryParse(strEID, out decTmp);
                                    if (decTmp > 0)
                                    {
                                        ein.LEingangTableID = decTmp;
                                        ein.FillEingang();

                                        string strSql1 = string.Empty;
                                        strSql1 = "Update ASN SET IsRead=1 WHERE ID=" + (Int32)tmpASN.ID + ";";
                                        bool bUpdate = clsSQLCOM.ExecuteSQL(strSql1, BenutzerID);
                                        //List ASN read füllen
                                        ListASNRead.Add(strASN);

                                        if (myUseAutoRowAssign)
                                        {
                                            if (ein.dtArtInLEingang.Rows.Count > 0)
                                            {
                                                foreach (DataRow r in ein.dtArtInLEingang.Rows)
                                                {
                                                    decTmp = 0;
                                                    decimal.TryParse(r["ID"].ToString(), out decTmp);
                                                    if (decTmp > 0)
                                                    {
                                                        clsArtikel tmpArt = new clsArtikel();
                                                        tmpArt.InitClass(this._GL_User, this._GL_System);
                                                        tmpArt.ID = decTmp;
                                                        tmpArt.GetArtikeldatenByTableID();
                                                        if (tmpArt.GArt is clsGut)
                                                        {
                                                            clsReihe Reihe = new clsReihe();
                                                            Reihe._GL_User = this._GL_User;
                                                            tmpArt.Reihe = Reihe.GetVorschlag(tmpArt.Dicke.ToString()
                                                                                            , tmpArt.Breite.ToString()
                                                                                            , tmpArt.Laenge.ToString()
                                                                                            , tmpArt.Hoehe.ToString()
                                                                                            , tmpArt.Brutto.ToString()
                                                                                            , tmpArt.GArtID.ToString());
                                                            tmpArt.SetArtValueLagerOrt(clsArtikel.ArtikelField_Reihe, tmpArt.Reihe, false);
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }
                                catch (Exception ex)
                                {
                                    clsMessages.Allgemein_ERRORTextShow(ex.ToString());
                                }
                            }
                            //iCountLoop++;
                        }//Ende Group By
                    }//end check IsRead
                }//end If
            }

            if (ListASNRead.Count > 0)
            {
                string strSQL = "Select * from LEingang where ASN in (";
                for (int i = 0; i < ListASNRead.Count; i++)
                {
                    strSQL += ListASNRead.ElementAt(i);
                    if ((i + 1) < ListASNRead.Count)
                    {
                        strSQL += ",";
                    }

                }
                strSQL += ") order by LEingangID";
                dtCreated = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Eingänge");
            }
            //wenn erfolgreich die ASN updaten und IsRead=true setzen
            //string strSql1 = string.Empty;
            //strSql1 = "Update ASN SET IsRead=1 WHERE ID IN (" + string.Join(",", ListASNRead.ToArray()) + ");";
            //bool bUpdate = clsSQLCOM.ExecuteSQL(strSql1, BenutzerID);
            return dtCreated;
        }


        ///<summary>clsLager / InsertASN</summary>
        ///<remarks></remarks>
        public bool DisableASN(DataTable dtASN)
        {
            bool bUpdate = false;
            List<string> ListASNDisable = new List<string>();
            //Eingang
            for (Int32 i = 0; i <= dtASN.Rows.Count - 1; i++)
            {
                string strASN = dtASN.Rows[i]["ASN"].ToString();
                ListASNDisable.Add(strASN);
            }
            if (ListASNDisable.Count > 0)
            {
                string strSql1 = string.Empty;
                strSql1 = "Update ASN SET IsRead=1 WHERE ID IN (" + string.Join(",", ListASNDisable.ToArray()) + "); ";
                bUpdate = clsSQLCOM.ExecuteSQLWithTRANSACTION(strSql1, "ASNDisable", BenutzerID);
            }
            return bUpdate;
        }
        ///<summary>clsLager / GetSQLStatistikColumns</summary>
        ///<remarks></remarks>
        private string GetSQLStatistikColumns(string strArt)
        {
            string strSQL = "Select " +
                                "'" + strArt + "' as Bestand" +
                                ", CAST(a.ID as INT) as ArtikelID" +
                                ", CAST(a.LVS_ID as INT) as LVSNr" +
                                ", a.Werksnummer" +
                                ", a.Produktionsnummer" +
                                ", a.Charge" +
                                ", e.Bezeichnung as Gut" +
                                ", (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Auftraggeber) as Auftraggeber " +
                                ", (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Empfaenger) as Empfaenger" +
                                ", a.Anzahl " +
                                ", a.Einheit " +
                                ", a.Dicke" +
                                ", a.Breite, a.Laenge, a.Hoehe" +
                                ", a.Netto, a.Brutto, a.exBezeichnung, a.Bestellnummer, a.exMaterialnummer" +
                                ", CAST(b.LEingangID as INT) as Eingang, b.Date as 'Eingangsdatum'" +
                                ", CAST(MONTH(b.Date) as varchar)+'/'+CAST(YEAR(b.Date) as varchar)  as Eingangsmonat " +
                                ", b.LfsNr as Lieferschein, CAST(c.LAusgangID as INT) as Ausgang, c.Datum as 'Ausgangsdatum'" +
                                ", CAST(MONTH(c.Datum) as varchar)+'/'+CAST(YEAR(c.Datum) as varchar)  as Ausgangsmonat " +
                                ", CASE WHEN (c.Datum IS NULL) THEN CAST( DATEDIFF(day, CAST(b.Date as Date)" +
                                ",CAST(GETDATE()as Date)) as INT)+1 ELSE CAST( DATEDIFF(day, CAST(b.Date as Date),CAST(c.Datum as Date)) as INT)+1 END as Lagerdauer " +
                                ",CASE WHEN EAAusgangAltLVS='0' THEN a.Info ELSE SUBSTRING(a.Info,1, PATINDEX('%- LVS-Ausgang:%', a.Info)) END as LargerortAltLvs" +
                                ", a.BKZ , b.DirectDelivery as DA, b.Retoure as RL, b.Vorfracht as VF" +
                                ", CASE WHEN b.LagerTransport IS NULL THEN CAST(0 as BIT) ELSE b.LagerTransport END as LT_Eingang" +
                                ", CASE WHEN c.LagerTransport IS NULL THEN CAST(0 as BIT) ELSE c.LagerTransport END as LT_Ausgang ";
            return strSQL;
        }
        ///<summary>clsLager / GetTagesbestand</summary>
        ///<remarks></remarks>
        private decimal GetTagesbestand(DateTime dtTag)
        {
            string strSQL = string.Empty;
            //Anfangsbestand
            strSQL = "Select " +
                        "Sum(a.Brutto) as Bestand ";
            strSQL = strSQL +
                            "From Artikel a " +
                                "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                                "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                                "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                                "WHERE " +
                                "((" +
                                        "a.CheckArt=1 AND b.[Check]=1 and (c.Checked is Null or c.Checked=0)  AND b.DirectDelivery=0 " +
                                        " AND b.Mandant=1 AND b.Date <'" + dtTag.AddDays(1).ToShortDateString() + "' " +
                                ") OR (" +

                                    " a.CheckArt=1 AND b.[Check]=1 and (c.checked=1)  " +
                                    "AND b.DirectDelivery=0 AND b.Mandant=1 " +
                                    "AND (c.Datum between '" + dtTag.AddDays(1).ToShortDateString() + "' " +
                                    "AND '" + DateTime.Now.AddDays(1).ToShortDateString() + "') " +
                                    "AND b.Date <'" + dtTag.AddDays(1).ToShortDateString() + "' " +
                                ")) AND b.AbBereich=1 ";

            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            decimal decTmp = 0;
            Decimal.TryParse(strTmp, out decTmp);
            return decTmp;
        }
        ///<summary>clsLager / GetStatitikBestandVerlauf</summary>
        ///<remarks></remarks>
        public DataTable GetStatitikBestandVerlauf(Int32 myAdrID)
        {
            //Zeitspanne in Tagen
            TimeSpan ts = this.BestandBis - this.BestandVon;
            Int32 iTage = ts.Days + 1;

            DataTable dtReturn = new DataTable();
            dtReturn.Columns.Add("Datum", typeof(string));
            dtReturn.Columns.Add("Bestand [kg]", typeof(decimal));

            DateTime dateTmp = this.BestandVon;
            for (Int32 i = 0; i <= iTage - 1; i++)
            {
                dateTmp = this.BestandVon.AddDays(i);
                DataRow row = dtReturn.NewRow();
                row["Datum"] = dateTmp.Date.ToShortDateString();
                decimal decTmp = 0;
                if (myAdrID > 0)
                {
                    decTmp = GetTagesbestandKunde(dateTmp, myAdrID);
                }
                else
                {
                    decTmp = GetTagesbestand(dateTmp);
                }
                row["Bestand [kg]"] = decTmp;
                dtReturn.Rows.Add(row);
            }
            return dtReturn;
        }
        ///<summary>clsLager / GetTagesbestandKunde</summary>
        ///<remarks></remarks>
        private decimal GetTagesbestandKunde(DateTime dtTag, Int32 myAdrID)
        {
            string strSQL = string.Empty;
            //Anfangsbestand
            strSQL = "Select " +
                        "Sum(a.Brutto) as Bestand ";
            strSQL = strSQL +
                            "From Artikel a " +
                                "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                                "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                                "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                                "WHERE " +
                                "((" +
                                       "b.Auftraggeber=" + myAdrID + " AND " +
                                        "a.BKZ=1 AND a.CheckArt=1 AND b.[Check]=1 AND b.DirectDelivery=0 " +
                                        " AND b.Mandant=1 AND b.Date <'" + dtTag.ToShortDateString() + "' " +
                                ") OR (" +

                                     "  b.Auftraggeber=" + myAdrID + " AND " +
                                    "a.BKZ=0 AND a.CheckArt=1 AND b.[Check]=1 " +
                                    "AND b.DirectDelivery=0 AND b.Mandant=1 " +
                                    "AND (c.Datum between '" + dtTag.ToShortDateString() + "' " +
                                    "AND '" + DateTime.Now.ToShortDateString() + "') " +
                                    "AND b.Date <'" + dtTag.ToShortDateString() + "' " +
                                "))";

            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            decimal decTmp = 0;
            Decimal.TryParse(strTmp, out decTmp);
            return decTmp;
        }
        ///<summary>clsLager / GetStatitikDSLagerbestand</summary>
        ///<remarks></remarks>
        public DataTable GetStatitikDSLagerbestand()
        {
            DataTable dt = GetStatitikBestandVerlauf(0);
            //Zeitspanne in Tagen
            TimeSpan ts = this.BestandBis.Date - this.BestandVon.Date;
            Int32 iTage = ts.Days + 1;

            decimal decSumBestand = 0;
            foreach (DataRow row in dt.Rows)
            {
                decimal decTmp = 0;
                Decimal.TryParse(row["Bestand [kg]"].ToString(), out decTmp);
                decSumBestand = decSumBestand + decTmp;
            }

            DataTable dtReturn = new DataTable("druchschnittLagerbestand");
            dtReturn.Columns.Add("Beschreibung", typeof(string));
            dtReturn.Columns.Add("Werte", typeof(string));

            DataRow dtRow = dtReturn.NewRow();
            dtRow["Beschreibung"] = "Zeitraum: ";
            dtRow["Werte"] = this.BestandVon.ToShortDateString() + " bis " + this.BestandBis.ToShortDateString();
            dtReturn.Rows.Add(dtRow);

            dtRow = dtReturn.NewRow();
            dtRow["Beschreibung"] = "Dauer: ";
            dtRow["Werte"] = iTage.ToString();
            dtReturn.Rows.Add(dtRow);

            dtRow = dtReturn.NewRow();
            dtRow["Beschreibung"] = "kum. Bestand [to]: ";
            dtRow["Werte"] = String.Format("{0:n}", (decSumBestand / 1000));
            dtReturn.Rows.Add(dtRow);

            decimal decDruchBestand = 0;
            if (iTage > 0)
            {
                decDruchBestand = decSumBestand / iTage;
            }
            else
            {
                decDruchBestand = decSumBestand;
            }
            Math.Round(decDruchBestand, 2);
            dtRow = dtReturn.NewRow();
            dtRow["Beschreibung"] = "durchschn. Bestand [to]: ";
            dtRow["Werte"] = String.Format("{0:n}", (decDruchBestand / 1000));
            dtReturn.Rows.Add(dtRow);

            return dtReturn;
        }
        ///<summary>clsLager / GetStatistikTagesbestandkomplett</summary>
        ///<remarks></remarks>
        public DataTable GetStatistikTagesbestandkomplett()
        {
            DataTable dt = new DataTable("Tagesbestand");
            decimal decTmp = GetTagesbestand(this.BestandVon);

            dt.Columns.Add("Datum", typeof(DateTime));
            dt.Columns.Add("Bestand", typeof(decimal));

            DataRow row = dt.NewRow();
            row["Datum"] = this.BestandVon;
            row["Bestand"] = decTmp;

            dt.Rows.Add(row);
            return dt;
        }
        ///<summary>clsLager / GetStatistikMonatsuebersicht</summary>
        ///<remarks></remarks>
        public DataTable GetStatistikMonatsuebersicht(decimal myAdrID, bool bLagerComplet)
        {
            DataTable dt = new DataTable("Monatsübersicht");
            //if (this.sys.Client.Modul.Lager_Bestandsliste_TagesbestandOhneSPL)
            //{
            //    //SIL
            //    dt = GetMonatswerteCustomizeSteffens(this.BestandVon, this.BestandBis, myAdrID, bLagerComplet);
            //}
            //else
            //{
            //    //SLE
            //    dt = GetMonatswerte(this.BestandVon, this.BestandBis, myAdrID, bLagerComplet);
            //}

            dt = GetMonatswerteCustomize(this.BestandVon, this.BestandBis, myAdrID, bLagerComplet);
            return dt;
        }
        ///<summary>clsLager / GetStatistikDSLagerdauer</summary>
        ///<remarks></remarks>
        public DataTable GetStatistikDSLagerdauer()
        {
            string strSQL = string.Empty;
            //Anfangsbestand
            strSQL = "Select " +
                            "(" +
                                "(Select SUM(Datediff(dd, b.Date, c.Datum)) From Artikel a " +
                                                                               "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                                                                               "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                                                                               "WHERE c.[Checked]=1 and a.LAusgangTableID>0) " +

                                "+ (Select SUM(Datediff(dd, b.Date, Getdate())) From Artikel a " +
                                                                                "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                                                                                "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                                                                                "WHERE a.LAusgangTableID=0)) as Lagertage " +
                             ", COUNT(ID) as Artikelanzahl " +
                             "From Artikel ";

            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Lagertage");

            Int32 iTage = 0;
            Int32 iArtAnzahl = 0;
            foreach (DataRow row in dt.Rows)
            {
                Int32.TryParse(row["Lagertage"].ToString(), out iTage);
                Int32.TryParse(row["Artikelanzahl"].ToString(), out iArtAnzahl);
            }

            DataTable dtReturn = new DataTable("druchschnittLagertage");
            dtReturn.Columns.Add("Beschreibung", typeof(string));
            dtReturn.Columns.Add("Werte", typeof(string));

            DataRow dtRow = dtReturn.NewRow();
            dtRow["Beschreibung"] = "Lagertage alle Artikel: ";
            dtRow["Werte"] = iTage.ToString();
            dtReturn.Rows.Add(dtRow);

            dtRow = dtReturn.NewRow();
            dtRow["Beschreibung"] = "Aritkelanzahl komplett: ";
            dtRow["Werte"] = iArtAnzahl.ToString();
            dtReturn.Rows.Add(dtRow);

            decimal decDS = (iTage / iArtAnzahl);
            dtRow = dtReturn.NewRow();
            dtRow["Beschreibung"] = "durchschn. Lagerdauer: ";
            dtRow["Werte"] = Math.Round(decDS, 0);
            dtReturn.Rows.Add(dtRow);
            return dtReturn;
        }
        ///<summary>clsLager / GetStatistikWaggonEA</summary>
        ///<remarks></remarks>
        public DataTable GetStatistikWaggonEA()
        {
            string strSQL = string.Empty;
            //Anfangsbestand
            strSQL = "(Select " +
                            "'IN' as Richtung " +
                            ",a.WaggonNo " +
                            ",COUNT(a.WaggonNo) as Anzahl " +
                            //", CONVERT(DateTime, a.[Date], 104) as Datum " +
                            "from LEingang a " +
                            "Group by  a.[Date],a.WaggonNo " +
                            "having a.WaggonNo is Not Null AND a.WaggonNo <> '' " +
                      ")" +
                      " UNION " +
                      "( " +
                        "Select " +
                            "'OUT' as Richtung " +
                            ",b.WaggonNo " +
                            ",COUNT(b.WaggonNo) as Anzahl " +
                            //", CONVERT(DateTime, b.Datum, 104) as Datum " +
                            "from LAusgang b " +
                            "Group by b.Datum,b.WaggonNo " +
                            "having b.WaggonNo is Not Null AND b.WaggonNo <> '' " +
                            ")" +
                        "Order By Anzahl desc ";

            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "LKW");
            return dt;
        }
        ///<summary>clsLager / GetStatistikLKWEA</summary>
        ///<remarks></remarks>
        public DataTable GetStatistikLKWEA()
        {
            string strSQL = string.Empty;
            //Anfangsbestand
            strSQL = "Select " +
                            "'IN' as Richtung " +
                            //",ROW_NUMBER() OVER(order by GetDate()) as pos " +
                            ",a.KFZ " +
                            ",COUNT(a.KFZ) as Anzahl " +
                            "from LEingang a " +
                            "Group by a.KFZ " +
                            "having a.KFZ is Not Null AND a.KFZ <> '' AND a.KFZ <> '--Fremdfahrzeug--' " +

                     "UNION " +

                     "Select " +
                                "'OUT' as Richtung " +
                                //", ROW_NUMBER() OVER(order by GetDate()) as pos " +
                                ",a.KFZ " +
                                ",COUNT(a.KFZ) as Anzahl " +
                                "from LAusgang a " +
                                "Group by a.KFZ " +
                                "having a.KFZ is Not Null AND a.KFZ <> '' AND a.KFZ <> '--Fremdfahrzeug--' " +
                                "Order by Anzahl desc ";

            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "LKW");
            return dt;
        }
        ///<summary>clsLager / getOffeneEingänge</summary>
        ///<remarks></remarks>
        private string GetOffeneEingänge()
        {
            string strSQL = string.Empty;

            strSQL = "Select " +
                           " LEingangID as Eingang " +
                           ",Convert(date, [Date],104) as Eingangsdatum" +
                           ",ViewID as Kunde" +
                           ",ExTransportRef" +
                           ",ExAuftragRef " +
                           ",LEingang.ID as LEingangTableID " +

                           " from LEingang " +
                               "left join ADR on Leingang.Auftraggeber=ADR.ID " +
                               "where " +
                                   "LEingang.AbBereich=" + this.AbBereichID + " " +
                                   "AND LEingang.[check] = 0 ";

            if (BestandAdrID > 0)
            {
                strSQL += " AND Leingang.Auftraggeber=" + BestandAdrID;
            }

            strSQL += " order by viewID";
            return strSQL;
        }
        ///<summary>clsLager / getOffeneAusgänge</summary>
        ///<remarks></remarks>
        private string GetOffeneAusgänge()
        {
            string strSQL = string.Empty;
            strSQL = "Select " +
                           " LAusgangID as Ausgang " +
                           ",convert(date,Datum,104) as Ausgangsdatum" +
                           ",ViewID as Kunde" +
                           ",ExTransportRef " +
                           ",LAusgang.ID as LAusgangTableID " +
                               " from LAusgang " +
                                       "left join ADR on LAusgang.Auftraggeber=ADR.ID " +
                                        "where " +
                                           "LAusgang.AbBereich=" + this.AbBereichID + " " +
                                           "AND LAusgang.checked = 0";


            if (BestandAdrID > 0)
            {
                strSQL += " AND LAusgang.Auftraggeber=" + BestandAdrID;
            }
            strSQL += " order by viewID";
            return strSQL;
        }
        ///<summary>clsLager / GetMonatswerteCustomizeSteffens</summary>
        ///<remarks></remarks>
        private DataTable GetMonatswerteCustomize(DateTime mydtpVon, DateTime mydtpBis, decimal myAdrID, bool bLagerKomplett)
        {
            //create result Table
            DataTable dtMonatwerte = new DataTable();
            dtMonatwerte.Columns.Add("Monat", typeof(string));
            dtMonatwerte.Columns.Add("Jahr", typeof(string));
            dtMonatwerte.Columns.Add("Netto Eingang", typeof(decimal));
            dtMonatwerte.Columns.Add("Netto Ausgang", typeof(decimal));
            dtMonatwerte.Columns.Add("Netto Saldo", typeof(decimal));
            dtMonatwerte.Columns.Add("Brutto Eingang", typeof(decimal));
            dtMonatwerte.Columns.Add("Brutto Ausgang", typeof(decimal));
            dtMonatwerte.Columns.Add("Brutto Saldo", typeof(decimal));
            dtMonatwerte.Columns.Add("Tage", typeof(Int32));
            dtMonatwerte.Columns.Add("Endbestand", typeof(decimal));


            //Ermittlung Monatsanzahl
            DateTime dateVon = Functions.GetFirstDayOfMonth(mydtpVon);
            DateTime dateBis = Functions.GetLastDayOfMonth(mydtpBis);
            Int32 iMonate = 12 * (dateBis.Year - dateVon.Year) + dateBis.Month - dateVon.Month + 1;

            decimal decNettoE = 0;
            decimal decNettoA = 0;
            decimal decBruttoE = 0;
            decimal decBruttoA = 0;
            decimal decEndbestand = 0;
            //Table Monatswerte füllen
            for (Int32 i = 0; i <= iMonate - 1; i++)
            {
                DateTime sqlDateVon = dateVon.AddMonths(i);
                DateTime sqlDateBis = Functions.GetLastDayOfMonth(dateVon.AddMonths(i));

                //i=0 => dann Übertragszeile hinzufügen
                if (i == 0)
                {
                    //Zeile für Übertrag einfügen
                    DataRow rowUbertrag = dtMonatwerte.NewRow();
                    string strSQL = "Select sum(i.Brutto)/1000   " +
                                    "From Artikel i  " +
                                    "INNER JOIN LEingang j ON j.ID = i.LEingangTableID  " +
                                    "LEFT JOIN LAusgang k ON k.ID = i.LAusgangTableID  " +
                                    "WHERE " +
                                         "(";
                    if (!bLagerKomplett)
                    {
                        strSQL = strSQL + "j.Auftraggeber=" + myAdrID + " AND ";
                    }

                    strSQL = strSQL + " i.CheckArt=1 " +
                                             "AND j.[Check]=1 and (k.Checked is Null or k.Checked=0) " +
                                             "AND j.DirectDelivery=0 AND k.IsRL=0 " +
                                             "AND j.AbBereich=" + this.sys.AbBereich.ID + " " +
                                             //"AND datediff(month,j.date,'01." + (j + 1) + "." + myDateTime.Year.ToString() + "')>=0" +
                                             //"AND datediff(dd,j.date,'01." + (j + 1) + "." + myDateTime.Year.ToString() + "')>=0" +
                                             "AND j.Date<'" + sqlDateVon.AddDays(-1).ToString() + "' " +
                                        ") " +
                                        "OR  " +
                                        "(";
                    if (!bLagerKomplett)
                    {
                        strSQL = strSQL + "j.Auftraggeber=" + myAdrID + " AND ";
                    }

                    strSQL = strSQL + "i.CheckArt=1 " +
                                             "AND j.[Check]=1 " +
                                             "AND j.DirectDelivery=0 AND k.IsRL=0 " +
                                             "AND j.AbBereich=" + this.sys.AbBereich.ID + " " +
                                             //"AND datediff(month,k.Datum,'01." + (j + 1) + "." + myDateTime.Year.ToString() + "')<0 " +
                                             //"AND datediff(month,j.date,'01." + (j + 1) + "." + myDateTime.Year.ToString() + "')>=0 " +
                                             "AND j.Date<'" + sqlDateVon.AddDays(-1).ToString() + "' " +
                                             "AND (CAST(k.Datum as Date) between '" + sqlDateVon.Date.ToString() + "' AND '" + DateTime.Now.ToString() + "') " +

                                    ") ";

                    decimal decTmp = 0;
                    string val = clsSQLcon.ExecuteSQL_GetValue(strSQL, this.BenutzerID);
                    decimal.TryParse(val, out decTmp);
                    rowUbertrag["Monat"] = "Übertrag";
                    rowUbertrag["Endbestand"] = Math.Round(decTmp, 3).ToString();
                    dtMonatwerte.Rows.Add(rowUbertrag);
                }


                //Ermitteln der Bestandswerte
                string strSql = "select  " +
                                      "Case  " +
                                      "when month('" + sqlDateVon + "')=1 then 'Januar' " +
                                      "when month('" + sqlDateVon + "')=2 then 'Februar' " +
                                      "when month('" + sqlDateVon + "')=3 then 'März' " +
                                      "when month('" + sqlDateVon + "')=4 then 'April' " +
                                      "when month('" + sqlDateVon + "')=5 then 'Mai' " +
                                      "when month('" + sqlDateVon + "')=6 then 'Juni' " +
                                      "when month('" + sqlDateVon + "')=7 then 'Juli' " +
                                      "when month('" + sqlDateVon + "')=8 then 'August' " +
                                      "when month('" + sqlDateVon + "')=9 then 'September' " +
                                      "when month('" + sqlDateVon + "')=10 then 'Oktober' " +
                                      "when month('" + sqlDateVon + "')=11 then 'November' " +
                                      "when month('" + sqlDateVon + "')=12 then 'Dezember' " +
                                      " " +
                                      "End as Monat ";


                //strSql += this.sys.Client.ctrStatistik_CustomizeDGVMonatsuebersicht("Eingang", "Netto", this.sys.AbBereich.ID, sqlDateVon, sqlDateBis, myAdrID, bLagerKomplett);
                //strSql += this.sys.Client.ctrStatistik_CustomizeDGVMonatsuebersicht("Eingang", "Brutto", this.sys.AbBereich.ID, sqlDateVon, sqlDateBis, myAdrID, bLagerKomplett);
                //strSql += this.sys.Client.ctrStatistik_CustomizeDGVMonatsuebersicht("Ausgang", "Netto", this.sys.AbBereich.ID, sqlDateVon, sqlDateBis, myAdrID, bLagerKomplett);
                //strSql += this.sys.Client.ctrStatistik_CustomizeDGVMonatsuebersicht("Ausgang", "Brutto", this.sys.AbBereich.ID, sqlDateVon, sqlDateBis, myAdrID, bLagerKomplett);

                //--- Netto Eingang
                strSql += ", (select SUM(a.Netto)/1000 From Artikel a " +
                                                                  "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                                                                  "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                                                                  "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                                                                  "WHERE " +
                                                                  "b.[Check]=1 AND b.DirectDelivery=0 " +
                                                                  "AND b.AbBereich=" + this.sys.AbBereich.ID + " ";
                if (this.RLJournalExcl)
                {
                    strSql += " AND (a.LAusgangTableID=0 OR c.IsRL=0) ";
                }

                if (!bLagerKomplett)
                {
                    strSql += "AND b.Auftraggeber=" + myAdrID + " ";
                }
                strSql += "AND (CAST(b.Date as Date) between '" + sqlDateVon.Date.ToShortDateString() + "' AND '" + sqlDateBis.Date.ToShortDateString() + "')";

                //nur ohne schaden
                if (this.SchadenJournalExcl)
                {
                    strSql += " AND a.ID NOT IN (SELECT sch.ArtikelID FROM SchadenZuweisung sch " +
                                                                "INNER JOIN Artikel art on sch.ArtikelID=art.ID) ";
                }
                strSql += ") as [Netto Eingang] ";

                //--- Netto Ausgang
                strSql += ", (select SUM(a.Netto)/1000 From Artikel a " +
                                                  "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                                                  "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                                                  "INNER JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                                                  "WHERE " +
                                                  "c.Checked=1 AND c.DirectDelivery=0 AND c.IsRL=0 ";
                if (!bLagerKomplett)
                {
                    strSql += " AND c.Auftraggeber=" + myAdrID + " ";
                }
                strSql += " AND c.AbBereich=" + this.sys.AbBereich.ID + " " +

                " AND (CAST(c.Datum as Date) between '" + sqlDateVon.Date.ToShortDateString() + "' AND '" + sqlDateBis.Date.ToShortDateString() + "') " +
               //    //nur ohne schaden
               //" AND a.ID NOT IN (" +
               //                    "SELECT sch.ArtikelID FROM SchadenZuweisung sch " +
               //                                            "INNER JOIN Artikel art on sch.ArtikelID=art.ID " +
               //                    ") " +
               ") as [Netto Ausgang] ";


                //--- Brutto Eingang
                strSql += ", (select SUM(a.Brutto)/1000 From Artikel a " +
                                                      "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                                                      "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                                                      "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                                                      "WHERE " +
                                                      "b.[Check]=1 AND b.DirectDelivery=0 AND b.AbBereich=" + this.sys.AbBereich.ID + " ";
                if (this.RLJournalExcl)
                {
                    strSql += " AND (a.LAusgangTableID=0 OR c.IsRL=0) ";
                }

                if (!bLagerKomplett)
                {
                    strSql += "AND b.Auftraggeber=" + myAdrID + " ";
                }
                strSql += "AND (CAST(b.Date as Date) between '" + sqlDateVon.Date.ToShortDateString() + "' AND '" + sqlDateBis.Date.ToShortDateString() + "')";

                //nur ohne schaden
                if (this.SchadenJournalExcl)
                {
                    strSql += " AND a.ID NOT IN (SELECT sch.ArtikelID FROM SchadenZuweisung sch " +
                                                                "INNER JOIN Artikel art on sch.ArtikelID=art.ID) ";
                }
                strSql += ")  as [Brutto Eingang]  ";

                //--- Brutto Ausgang
                strSql += ", (select SUM(a.Brutto)/1000 From Artikel a " +
                                                          "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                                                          "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                                                          "INNER JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                                                          "WHERE " +
                                                          "c.Checked=1 AND c.DirectDelivery=0 AND c.IsRL=0  ";

                if (!bLagerKomplett)
                {
                    strSql += "AND b.Auftraggeber=" + myAdrID + " ";
                }
                strSql += " AND c.AbBereich=" + this.sys.AbBereich.ID + " " +
                "AND (CAST(c.Datum as Date) between '" + sqlDateVon.Date.ToShortDateString() + "' AND '" + sqlDateBis.Date.ToShortDateString() + "') " +
               //nur ohne schaden -> Ausgangsmenge egal ob mit oder ohne schaden
               //" AND a.ID NOT IN (" +
               //                    "SELECT sch.ArtikelID FROM SchadenZuweisung sch " +
               //                                            "INNER JOIN Artikel art on sch.ArtikelID=art.ID " +
               //                    ") " +
               ") as [Brutto Ausgang] ";

                //---- Endbestand
                strSql += ", (Select sum(i.Brutto)/1000  From Artikel i  " +
                                                "INNER JOIN LEingang j ON j.ID = i.LEingangTableID  " +
                                                "LEFT JOIN LAusgang k ON k.ID = i.LAusgangTableID  " +
                                                "WHERE " +
                                                "j.AbBereich=" + this.sys.AbBereich.ID + " AND " +
                                                "(" +
                                                  "(";
                if (!bLagerKomplett)
                {
                    strSql += "j.Auftraggeber=" + myAdrID + " AND ";
                }
                strSql = strSql + "i.CheckArt=1 " +
                                  "AND j.[Check]=1 and (k.Checked is Null or k.Checked=0) " +
                                  "AND j.DirectDelivery=0  " +
                                  "AND j.date <'" + sqlDateBis.AddDays(1).Date.ToShortDateString() + "' " +
              ") " +
              "OR  " +
              "(";
                if (!bLagerKomplett)
                {
                    strSql += "j.Auftraggeber=" + myAdrID + " AND ";
                }
                strSql += "i.CheckArt=1 " +
                        "AND j.[Check]=1 " +
                        "AND j.DirectDelivery=0  " +
                        "AND k.Datum >= '" + sqlDateBis.AddDays(1).Date.ToShortDateString() + "' " +
                        "AND j.date < '" + sqlDateBis.AddDays(1).Date.ToShortDateString() + "' " +
                            ") " +
                        ")";

                if (this.SchadenJournalExcl)
                {
                    strSql += " AND i.ID NOT IN (SELECT DISTINCT ArtikelID FROM SchadenZuweisung) ";

                }
                if (this.SPLEndbestandExcl)
                {
                    strSql += " AND i.ID NOT IN (SELECT spl.ArtikelID FROM Sperrlager spl WHERE spl.BKZ = 'IN' AND spl.ID NOT IN (SELECT DISTINCT c.SPLIDIn FROM Sperrlager c WHERE c.SPLIDIn > 0)) ";
                }
                strSql += ") as [Endbestand] ";


                DataTable dtTmp = clsSQLcon.ExecuteSQL_GetDataTable(strSql, this.BenutzerID, "Monatsübersicht");
                if (dtTmp.Rows.Count > 0)
                {
                    DataRow row = dtMonatwerte.NewRow();
                    decimal decTmp = 0;

                    row["Monat"] = dtTmp.Rows[0]["Monat"].ToString();
                    row["Jahr"] = sqlDateVon.Year.ToString();

                    Decimal.TryParse(dtTmp.Rows[0]["Netto Eingang"].ToString(), out decTmp);
                    decNettoE = decNettoE + decTmp;
                    row["Netto Eingang"] = Math.Round(decTmp, 3);

                    decTmp = 0;
                    Decimal.TryParse(dtTmp.Rows[0]["Netto Ausgang"].ToString(), out decTmp);
                    decNettoA = decNettoA + decTmp;
                    row["Netto Ausgang"] = Math.Round(decTmp, 3);

                    decTmp = 0;
                    decTmp = (decimal)row["Netto Eingang"] - (decimal)row["Netto Ausgang"];
                    row["Netto Saldo"] = Math.Round(decTmp, 3);

                    decTmp = 0;
                    Decimal.TryParse(dtTmp.Rows[0]["Brutto Eingang"].ToString(), out decTmp);
                    decBruttoE = decBruttoE + decTmp;
                    row["Brutto Eingang"] = Math.Round(decTmp, 3);

                    decTmp = 0;
                    Decimal.TryParse(dtTmp.Rows[0]["Brutto Ausgang"].ToString(), out decTmp);
                    decBruttoA = decBruttoA + decTmp;
                    row["Brutto Ausgang"] = Math.Round(decTmp, 3);

                    TimeSpan tsTage = (sqlDateBis - sqlDateVon);
                    row["Tage"] = tsTage.Days + 1;

                    decTmp = 0;
                    decTmp = (decimal)row["Brutto Eingang"] - (decimal)row["Brutto Ausgang"];
                    row["Brutto Saldo"] = Math.Round(decTmp, 3);

                    decTmp = 0;
                    Decimal.TryParse(dtTmp.Rows[0]["Endbestand"].ToString(), out decTmp);
                    decEndbestand = decEndbestand + decTmp;
                    row["Endbestand"] = Math.Round(decTmp, 3);

                    dtMonatwerte.Rows.Add(row);
                }
                //Summenzeile hinzufügen
                if (i == (iMonate - 1))
                {
                    DataRow rowSumme = dtMonatwerte.NewRow();
                    rowSumme[0] = "Summe";
                    rowSumme["Netto Eingang"] = Math.Round(decNettoE, 3);
                    rowSumme["Netto Ausgang"] = Math.Round(decNettoA, 3);
                    rowSumme["Netto Saldo"] = Math.Round(decNettoE - decNettoA, 3);
                    rowSumme["Brutto Eingang"] = Math.Round(decBruttoE, 3);
                    rowSumme["Brutto Ausgang"] = Math.Round(decBruttoA, 3);
                    rowSumme["Brutto Saldo"] = Math.Round(decBruttoE - decBruttoA, 3);
                    rowSumme["Endbestand"] = Math.Round(decEndbestand, 3);
                    dtMonatwerte.Rows.Add(rowSumme);
                }
            }
            return dtMonatwerte;
        }
        ///<summary>clsLager / GetMonatswerte</summary>
        ///<remarks></remarks>
        private DataTable GetMonatswerte(DateTime mydtpVon, DateTime mydtpBis, decimal myAdrID, bool bLagerKomplett)
        {
            //create result Table
            DataTable dtMonatwerte = new DataTable();
            dtMonatwerte.Columns.Add("Monat", typeof(string));
            dtMonatwerte.Columns.Add("Jahr", typeof(string));
            dtMonatwerte.Columns.Add("Netto Eingang", typeof(decimal));
            dtMonatwerte.Columns.Add("Netto Ausgang", typeof(decimal));
            dtMonatwerte.Columns.Add("Netto Saldo", typeof(decimal));
            dtMonatwerte.Columns.Add("Brutto Eingang", typeof(decimal));
            dtMonatwerte.Columns.Add("Brutto Ausgang", typeof(decimal));
            dtMonatwerte.Columns.Add("Brutto Saldo", typeof(decimal));
            dtMonatwerte.Columns.Add("Tage", typeof(Int32));
            dtMonatwerte.Columns.Add("Endbestand", typeof(decimal));


            //Ermittlung Monatsanzahl
            DateTime dateVon = Functions.GetFirstDayOfMonth(mydtpVon);
            DateTime dateBis = Functions.GetLastDayOfMonth(mydtpBis);
            Int32 iMonate = 12 * (dateBis.Year - dateVon.Year) + dateBis.Month - dateVon.Month + 1;

            decimal decNettoE = 0;
            decimal decNettoA = 0;
            decimal decBruttoE = 0;
            decimal decBruttoA = 0;
            decimal decEndbestand = 0;
            //Table Monatswerte füllen
            for (Int32 i = 0; i <= iMonate - 1; i++)
            {
                DateTime sqlDateVon = dateVon.AddMonths(i);
                DateTime sqlDateBis = Functions.GetLastDayOfMonth(dateVon.AddMonths(i));

                //i=0 => dann Übertragszeile hinzufügen
                if (i == 0)
                {
                    //Zeile für Übertrag einfügen
                    DataRow rowUbertrag = dtMonatwerte.NewRow();
                    string strSQL = "Select sum(i.Brutto)/1000   " +
                                    "From Artikel i  " +
                                    "INNER JOIN LEingang j ON j.ID = i.LEingangTableID  " +
                                    "LEFT JOIN LAusgang k ON k.ID = i.LAusgangTableID  " +
                                    "WHERE " +
                                         "(";
                    if (!bLagerKomplett)
                    {
                        strSQL = strSQL + "j.Auftraggeber=" + myAdrID + " AND ";
                    }
                    strSQL = strSQL +         //"j.Auftraggeber=" + myAdrID + " " +
                                             " i.CheckArt=1 " +
                                             "AND j.[Check]=1 and (k.Checked is Null or k.Checked=0) " +
                                             "AND j.DirectDelivery=0 " +
                                             "AND j.AbBereich=" + this.sys.AbBereich.ID + " " +
                                             //"AND datediff(month,j.date,'01." + (j + 1) + "." + myDateTime.Year.ToString() + "')>=0" +
                                             //"AND datediff(dd,j.date,'01." + (j + 1) + "." + myDateTime.Year.ToString() + "')>=0" +
                                             "AND j.Date<'" + sqlDateVon.AddDays(-1).ToString() + "' " +
                                        ") " +
                                        "OR  " +
                                        "(";

                    if (!bLagerKomplett)
                    {
                        strSQL = strSQL + "j.Auftraggeber=" + myAdrID + " AND ";
                    }
                    strSQL = strSQL +    //"j.Auftraggeber=" + myAdrID + " " +
                                             " i.CheckArt=1 " +
                                             "AND j.[Check]=1 " +
                                             "AND j.DirectDelivery=0 " +
                                             "AND j.AbBereich=" + this.sys.AbBereich.ID + " " +
                                             //"AND datediff(month,k.Datum,'01." + (j + 1) + "." + myDateTime.Year.ToString() + "')<0 " +
                                             //"AND datediff(month,j.date,'01." + (j + 1) + "." + myDateTime.Year.ToString() + "')>=0 " +
                                             "AND j.Date<'" + sqlDateVon.AddDays(-1).ToString() + "' " +
                                             "AND (k.Datum between '" + sqlDateVon.AddDays(-1).ToString() + "' AND '" + DateTime.Now.ToString() + "') " +

                                    ") ";

                    decimal decTmp = 0;
                    string val = clsSQLcon.ExecuteSQL_GetValue(strSQL, this.BenutzerID);
                    decimal.TryParse(val, out decTmp);
                    rowUbertrag["Monat"] = "Übertrag";
                    rowUbertrag["Endbestand"] = Math.Round(decTmp, 3).ToString();
                    dtMonatwerte.Rows.Add(rowUbertrag);
                }


                //Ermitteln der Bestandswerte
                string strSql = "select  " +
                                      "Case  " +
                                      "when month('" + sqlDateVon + "')=1 then 'Januar' " +
                                      "when month('" + sqlDateVon + "')=2 then 'Februar' " +
                                      "when month('" + sqlDateVon + "')=3 then 'März' " +
                                      "when month('" + sqlDateVon + "')=4 then 'April' " +
                                      "when month('" + sqlDateVon + "')=5 then 'Mai' " +
                                      "when month('" + sqlDateVon + "')=6 then 'Juni' " +
                                      "when month('" + sqlDateVon + "')=7 then 'Juli' " +
                                      "when month('" + sqlDateVon + "')=8 then 'August' " +
                                      "when month('" + sqlDateVon + "')=9 then 'September' " +
                                      "when month('" + sqlDateVon + "')=10 then 'Oktober' " +
                                      "when month('" + sqlDateVon + "')=11 then 'November' " +
                                      "when month('" + sqlDateVon + "')=12 then 'Dezember' " +
                                      " " +
                                      "End as Monat " +
                                  ", (select SUM(a.Netto)/1000 From Artikel a " +
                                                                  "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                                                                  "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                                                                  "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                                                                  "WHERE " +
                                                                  "b.[Check]=1 AND b.DirectDelivery=0 AND b.AbBereich=" + this.sys.AbBereich.ID + " AND c.IsRL=0 ";
                if (!bLagerKomplett)
                {
                    strSql = strSql + "AND b.Auftraggeber=" + myAdrID + " ";
                }
                strSql = strSql + "AND (b.Date between '" + sqlDateVon.Date.ToShortDateString() + "' AND '" + sqlDateBis.Date.AddDays(1).ToShortDateString() + "')) as [Netto Eingang] " +
", (select SUM(a.Netto)/1000 From Artikel a " +
                  "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                  "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                  "INNER JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                  "WHERE " +
                  "c.Checked=1 AND c.DirectDelivery=0 AND c.IsRL=0 ";
                if (!bLagerKomplett)
                {
                    strSql = strSql + " AND c.Auftraggeber=" + myAdrID + " ";
                }
                strSql = strSql + " AND c.AbBereich=" + this.sys.AbBereich.ID + " " +
                //" AND (c.Datum between '" + sqlDateVon.Date.ToShortDateString() + "' AND '" + sqlDateBis.Date.AddDays(1).ToShortDateString() + "')) as [Netto Ausgang] " +
                " AND (CAST(c.Datum as Date) between '" + sqlDateVon.Date.ToShortDateString() + "' AND '" + sqlDateBis.Date.ToShortDateString() + "')  " +
                ") as [Netto Ausgang] " +
", (select SUM(a.Brutto)/1000 From Artikel a " +
                  "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                  "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                  "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                  "WHERE " +
                  "b.[Check]=1 AND b.DirectDelivery=0 AND b.AbBereich=" + this.sys.AbBereich.ID + " AND c.IsRL=0 ";
                if (!bLagerKomplett)
                {
                    strSql = strSql + "AND b.Auftraggeber=" + myAdrID + " ";
                }
                //"AND (b.Date between '" + sqlDateVon.Date.ToShortDateString() + "' AND '" + sqlDateBis.Date.AddDays(1).ToShortDateString() + "')) as [Brutto Eingang] " +
                strSql = strSql + "AND (CAST(b.Date as Date) between '" + sqlDateVon.Date.ToShortDateString() + "' AND '" + sqlDateBis.Date.ToShortDateString() + "')" +
                 //nur ohne schaden
                 ")  as [Brutto Eingang]  " +
", (select SUM(a.Brutto)/1000 From Artikel a " +
                "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                "INNER JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                "WHERE " +
                "c.Checked=1 AND c.DirectDelivery=0 AND c.IsRL=0 ";
                if (!bLagerKomplett)
                {
                    strSql = strSql + "AND b.Auftraggeber=" + myAdrID + " ";
                }
                strSql = strSql + " AND c.AbBereich=" + this.sys.AbBereich.ID + " " +
                //"AND (c.Datum between '" + sqlDateVon.Date.ToShortDateString() + "' AND '" + sqlDateBis.Date.AddDays(1).ToShortDateString() + "')) as [Brutto Ausgang] " +
                "AND (CAST(c.Datum as Date) between '" + sqlDateVon.Date.ToShortDateString() + "' AND '" + sqlDateBis.Date.ToShortDateString() + "') " +
                  ") as [Brutto Ausgang] " +


", (Select sum(i.Brutto)/1000  From Artikel i  " +
                  "INNER JOIN LEingang j ON j.ID = i.LEingangTableID  " +
                  "LEFT JOIN LAusgang k ON k.ID = i.LAusgangTableID  " +
                  "WHERE " +
                  "(";
                if (!bLagerKomplett)
                {
                    strSql = strSql + "j.Auftraggeber=" + myAdrID + " AND ";
                }
                strSql = strSql + " i.CheckArt=1 " +
                     "AND j.[Check]=1 and (k.Checked is Null or k.Checked=0) " +
                     "AND j.DirectDelivery=0 " +
                     "AND j.AbBereich=" + this.sys.AbBereich.ID + " " +
                     "AND datediff(dd,j.date,'" + sqlDateBis.ToString() + "')>=0" +
             ") " +
             "OR  " +
             "(";
                if (!bLagerKomplett)
                {
                    strSql = strSql + "j.Auftraggeber=" + myAdrID + " AND ";
                }
                strSql = strSql + " i.CheckArt=1 " +
                     "AND j.[Check]=1 " +
                     "AND j.DirectDelivery=0 " +
                     "AND j.AbBereich=" + this.sys.AbBereich.ID + " " +
                     "AND datediff(dd,k.Datum,'" + sqlDateBis.ToString() + "')<0 " +
                     "AND datediff(dd,j.date,'" + sqlDateBis.ToString() + "')>=0 " +
         ") ) as [Endbestand] " +


" ";

                DataTable dtTmp = clsSQLcon.ExecuteSQL_GetDataTable(strSql, this.BenutzerID, "Monatsübersicht");
                if (dtTmp.Rows.Count > 0)
                {
                    DataRow row = dtMonatwerte.NewRow();
                    decimal decTmp = 0;

                    row["Monat"] = dtTmp.Rows[0]["Monat"].ToString();
                    row["Jahr"] = sqlDateVon.Year.ToString();

                    Decimal.TryParse(dtTmp.Rows[0]["Netto Eingang"].ToString(), out decTmp);
                    decNettoE = decNettoE + decTmp;
                    row["Netto Eingang"] = Math.Round(decTmp, 3);

                    decTmp = 0;
                    Decimal.TryParse(dtTmp.Rows[0]["Netto Ausgang"].ToString(), out decTmp);
                    decNettoA = decNettoA + decTmp;
                    row["Netto Ausgang"] = Math.Round(decTmp, 3);

                    decTmp = 0;
                    decTmp = (decimal)row["Netto Eingang"] - (decimal)row["Netto Ausgang"];
                    row["Netto Saldo"] = Math.Round(decTmp, 3);

                    decTmp = 0;
                    Decimal.TryParse(dtTmp.Rows[0]["Brutto Eingang"].ToString(), out decTmp);
                    decBruttoE = decBruttoE + decTmp;
                    row["Brutto Eingang"] = Math.Round(decTmp, 3);

                    decTmp = 0;
                    Decimal.TryParse(dtTmp.Rows[0]["Brutto Ausgang"].ToString(), out decTmp);
                    decBruttoA = decBruttoA + decTmp;
                    row["Brutto Ausgang"] = Math.Round(decTmp, 3);

                    TimeSpan tsTage = (sqlDateBis - sqlDateVon);
                    row["Tage"] = tsTage.Days + 1;

                    decTmp = 0;
                    decTmp = (decimal)row["Brutto Eingang"] - (decimal)row["Brutto Ausgang"];
                    row["Brutto Saldo"] = Math.Round(decTmp, 3);

                    decTmp = 0;
                    Decimal.TryParse(dtTmp.Rows[0]["Endbestand"].ToString(), out decTmp);
                    decEndbestand = decEndbestand + decTmp;
                    row["Endbestand"] = Math.Round(decTmp, 3);

                    dtMonatwerte.Rows.Add(row);
                }
                //Summenzeile hinzufügen
                if (i == (iMonate - 1))
                {
                    DataRow rowSumme = dtMonatwerte.NewRow();
                    rowSumme[0] = "Summe";
                    rowSumme["Netto Eingang"] = Math.Round(decNettoE, 3);
                    rowSumme["Netto Ausgang"] = Math.Round(decNettoA, 3);
                    rowSumme["Netto Saldo"] = Math.Round(decNettoE - decNettoA, 3);
                    rowSumme["Brutto Eingang"] = Math.Round(decBruttoE, 3);
                    rowSumme["Brutto Ausgang"] = Math.Round(decBruttoA, 3);
                    rowSumme["Brutto Saldo"] = Math.Round(decBruttoE - decBruttoA, 3);
                    rowSumme["Endbestand"] = Math.Round(decEndbestand, 3);
                    dtMonatwerte.Rows.Add(rowSumme);
                }
            }
            return dtMonatwerte;
        }
        ///<summary>clsLager / ProzessStoreOutWithSPLOut</summary>
        ///<remarks></remarks>
        public bool ProzessStoreOutWithSPLOut(List<decimal> myArtikelList)
        {
            bool bReturn = false;
            this.SPL = new clsSPL();
            this.SPL.InitClass(this._GL_User);

            //Auslagerung
            this.Ausgang = new clsLAusgang();
            this.Ausgang.InitDefaultClsAusgang(this._GL_User, this.sys);
            this.Ausgang.LAusgangsDate = DateTime.Now;
            //Gewichtsupdate per SQL
            this.Ausgang.GewichtNetto = 0;
            this.Ausgang.GewichtBrutto = 0;
            //siehe im Artikeldurchlauf
            //this.Ausgang.Auftraggeber = this.Artikel.Eingang.Auftraggeber;
            //this.Ausgang.Empfaenger = this.Artikel.Eingang.Empfaenger;
            //this.Ausgang.Lieferant = this.Artikel.Eingang.Lieferant;
            this.Ausgang.Entladestelle = 0;
            this.Ausgang.Checked = true;

            if (myArtikelList.Count > 0)
            {
                //Zusammenbau der SQL-Anweisung
                string strSQLSPL = string.Empty;
                string strSQLArtUpdate = string.Empty;
                for (Int32 i = 0; i <= myArtikelList.Count - 1; i++)
                {
                    this.Artikel.ID = (decimal)myArtikelList[i];
                    this.Artikel.GetArtikeldatenByTableID();
                    strSQLSPL += this.SPL.SQLArtikelBookOutSPL(this.Artikel, false);
                    if (i == 0)
                    {
                        if (this.Artikel.Eingang is clsLEingang)
                        {
                            this.Ausgang.Auftraggeber = this.Artikel.Eingang.Auftraggeber;
                            this.Ausgang.Empfaenger = this.Artikel.Eingang.Empfaenger;
                            this.Ausgang.Lieferant = this.Artikel.Eingang.Lieferant;
                        }
                    }
                    strSQLArtUpdate += " Update Artikel SET LAusgangTableID=@LAusgangTableID " +
                                           ", BKZ=0 " +
                                           ", LA_Checked=1 " +
                                           " WHERE ID =" + (Int32)this.Artikel.ID + " ;";
                }

                string strSQLAusgang = "DECLARE @LAusgangTableID decimal; ";
                strSQLAusgang += this.Ausgang.AddLAusgang_SQL();
                strSQLAusgang += "SET @LAusgangTableID=(Select @@IDENTITY); ";

                string strSQLAusgangUpdate = string.Empty;
                strSQLAusgangUpdate += " Update LAusgang SET " +
                                            "Netto = (Select SUM(Netto) FROM Artikel WHERE LAusgangTableID=@LAusgangTableID) " +
                                            ", Brutto =  (Select SUM(Brutto) FROM Artikel WHERE LAusgangTableID=@LAusgangTableID) " +
                                            ", LfsNr= 'A'+CAST(LAusgangID as nvarchar) " +
                                            ", SLB = LAusgangID" +
                                            " WHERE ID =@LAusgangTableID ; ";

                string strSQLFinal = strSQLAusgang +
                                     strSQLSPL +
                                     strSQLArtUpdate +
                                     strSQLAusgangUpdate +
                                     " Select @LAusgangTableID;";
                string strTmp = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSQLFinal, "StoreOutSPL", this.BenutzerID);
                decimal decTmp = 0;
                Decimal.TryParse(strTmp, out decTmp);
                if (decTmp > 0)
                {
                    bReturn = true;
                    this.Ausgang.LAusgangTableID = decTmp;
                    //Vita Ausgang
                    clsArtikelVita.AddAuslagerungAutoSPL(this.BenutzerID, this.Ausgang.LAusgangTableID, this.Ausgang.LAusgangID);
                    //Vita Artikel
                    clsArtikelVita.AddArtikelLAusgangAuto(this._GL_User, this.Artikel.ID, this.Ausgang.LAusgangID);
                    //Vita Ausgang abgeschlossen
                    clsArtikelVita.LagerAusgangAutoChecked(this.BenutzerID, this.Ausgang.LAusgangTableID, this.Ausgang.LAusgangID);
                }
            }
            return bReturn;
        }
        ///<summary>clsLager / CreateAusgangByArtikel</summary>
        ///<remarks></remarks>
        public bool ProzessStoreOut()
        {
            bool bReturn = false;
            if (this.Artikel.ID > 0)
            {
                this.Ausgang.LAusgangsDate = DateTime.Now.Date;
                this.Ausgang.GewichtNetto = this.Artikel.Netto;
                this.Ausgang.GewichtBrutto = this.Artikel.Brutto;
                this.Ausgang.Auftraggeber = this.Eingang.Auftraggeber;
                this.Ausgang.Versender = 0;
                this.Ausgang.Empfaenger = 0;
                this.Ausgang.Entladestelle = 0;
                this.Ausgang.Lieferant = this.Eingang.Lieferant;
                //this.Ausgang.LfsDate = DateTime.Now.Date;
                this.Ausgang.Checked = true;
                this.Ausgang.AbBereichID = this.sys.AbBereich.ID;
                this.Ausgang.MandantenID = this.sys.AbBereich.MandantenID;
                this.Ausgang.Termin = Globals.DefaultDateTimeMinValue;
                this.Ausgang.DirectDelivery = false;
                this.Ausgang.NeutralerAuftraggeber = 0;
                this.Ausgang.NeutralerEmpfaenger = 0;
                this.Ausgang.LagerTransport = false;
                this.Ausgang.WaggonNr = string.Empty;
                this.Ausgang.BeladeID = 0;
                this.Ausgang.IsWaggon = false;
                this.Ausgang.exTransportRef = string.Empty;
                this.Ausgang.Fahrer = string.Empty;

                string strSQL = this.Ausgang.AddLAusgang_SQL();
                strSQL = strSQL + "Select @@IDENTITY as 'ID' ";
                strSQL = strSQL + " Update Artikel SET LAusgangTableID=@@IDENTITY " +
                                                       ", BKZ=0 " +
                                                       ", LA_Checked=1 " +
                                                       " WHERE ID =" + (Int32)this.Artikel.ID + " ;";
                string strLAusgangTableID = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSQL, "Ausgang", this.BenutzerID);
                decimal decTmp = 0;
                decimal.TryParse(strLAusgangTableID, out decTmp);
                if (decTmp > 0)
                {
                    bReturn = true;
                    //Update Lieferschein
                    this.Ausgang.LAusgangTableID = decTmp;
                    this.Ausgang.FillAusgang();

                    //Add Logbucheintrag 
                    string myBeschreibung = "Lager - Ausgang autom erstellt: NR [" + this.Ausgang.LAusgangID + "] / Mandant [" + this.Ausgang.MandantenID + "] / Arbeitsbereich  [" + this.Ausgang.AbBereichID + "] ";
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), myBeschreibung);
                    //Ausgang
                    clsArtikelVita.AddAuslagerungAuto(BenutzerID, this.Ausgang.LAusgangTableID, this.Ausgang.LAusgangID);
                    //Artitel hinzugefügt
                    clsArtikelVita.AddArtikelLAusgangAuto(this._GL_User, this.Artikel.ID, this.Ausgang.LAusgangID);
                    //Ausgang abschließen
                    clsArtikelVita.LagerAusgangAutoChecked(this.BenutzerID, this.Ausgang.LAusgangTableID, this.Ausgang.LAusgangID);
                }
            }
            return bReturn;
        }
    }
}
