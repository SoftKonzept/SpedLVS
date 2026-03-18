using System;
using System.Collections.Generic;
using System.Data;



namespace LVS
{
    public class clsGut
    {
        public const string const_GArtArt_Bleche = "Bleche";
        public const string const_GArtArt_Coils = "Coils";
        public const string const_GArtArt_EuroPaletten = "EURO-Paletten";
        public const string const_GArtArt_Paletten = "Paletten";
        public const string const_GArtArt_Platinen = "Platinen";
        public const string const_GArtArt_Rohre = "Rohre";
        public const string const_GArtArt_Stabstahl = "Stabstahl";


        public clsStyleSheetColumn Style = new clsStyleSheetColumn();
        public clsArbeitsbereichGArten AbBereichGut;
        public clsGueterartADR GutADR;
        public Globals._GL_SYSTEM GLSystem;
        public Globals._GL_USER GLUser;

        public clsSystem sys;
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
        //************************************

        public decimal ID { get; set; }
        public string ViewID { get; set; }
        public decimal Gut_ID { get; set; }
        public Int32 ME { get; set; }
        public string Bezeichnung { get; set; }
        public decimal Gewicht { get; set; }
        public decimal Dicke { get; set; }
        public decimal Hoehe { get; set; }
        public decimal Laenge { get; set; }
        public decimal Breite { get; set; }
        public Int32 MassAnzahl { get; set; }
        public decimal Netto { get; set; }
        public decimal Brutto { get; set; }
        public string ArtikelArt { get; set; }
        public string Besonderheit { get; set; }
        public string Verpackung { get; set; }
        public string AbsteckBolzenNr { get; set; }
        public Int32 MEAbsteckBolzen { get; set; }
        public decimal ArbeitsbereichID { get; set; }
        public decimal LieferantenID { get; set; }
        public bool Aktiv { get; set; }
        private bool _GArtIsUsed;
        public bool GArtIsUsed
        {
            get
            {
                _GArtIsUsed = CheckGArtIsUsed();
                return _GArtIsUsed;
            }
            set { _GArtIsUsed = value; }
        }
        public decimal MindestBestand { get; set; }
        public string BestellNr { get; set; }
        public string Einheit { get; set; }
        public string Zusatz { get; set; }
        public string Verweis { get; set; }
        public string Werksnummer { get; set; }
        public Dictionary<decimal, clsGut> DictGutVda4905 { get; set; }
        public List<decimal> ListGutIDVDA4905 { get; set; }

        private List<string> _ListArtikelArt;
        public List<string> ListArtikelArt
        {
            get
            {
                _ListArtikelArt = new List<string>();
                //_ListArtikelArt.Add("Bleche");
                //_ListArtikelArt.Add("Coils");
                //_ListArtikelArt.Add("EURO-Paletten");
                //_ListArtikelArt.Add("Paletten");
                //_ListArtikelArt.Add("Platinen");
                //_ListArtikelArt.Add("Rohre");
                //_ListArtikelArt.Add("Stabstahl");
                _ListArtikelArt = Common.Helper.ArtikelArt.ListArticleArt();

                clsClient.ctrGueterArtListe_CustomizeArtikelArten(this.sys.Client.MatchCode, ref _ListArtikelArt);
                return _ListArtikelArt;
            }
            set { _ListArtikelArt = value; }
        }
        public string VDA4905LieferantenInfo { get; set; }
        public bool IsStackable { get; set; }
        public bool UseProdNrCheck { get; set; }
        public string tmpLiefVerweis { get; set; }
        public string DelforVerweis { get; set; } = string.Empty;
        public bool IgnoreEdi { get; set; } = false;


        /*************************************************************************
         *                  Methoden / Procedure
         * **********************************************************************/
        ///<summary>clsGut/InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSystem)
        {
            this.GLUser = myGLUser;
            this.GLSystem = myGLSystem;

            //if (!bClassOnly)
            //{
            GutADR = new clsGueterartADR();
            GutADR.InitClass(this.GLUser, this.GLSystem);

            AbBereichGut = new clsArbeitsbereichGArten();
            //}
        }
        ///<summary>clsGut/Copy</summary>
        ///<remarks></remarks>
        public clsGut Copy()
        {
            return (clsGut)this.MemberwiseClone();
        }
        ///<summary>clsGut/UpdateGueterArt</summary>
        ///<remarks>Der Datensatz wird upgedatet.</remarks>
        public void UpdateGueterArt()
        {
            if (ID == 1)
            {
                ViewID = "0";
                Bezeichnung = "Alle Güter";
                Dicke = 0;
                Breite = 0;
                Laenge = 0;
                Hoehe = 0;
                Aktiv = true;
                MassAnzahl = 4;
            }
            string strSql = "Update Gueterart SET ViewID ='" + ViewID + "' " +
                                          ", Bezeichnung ='" + Bezeichnung + "' " +
                                          ", Dicke ='" + Dicke.ToString().Replace(",", ".") + "' " +
                                          ", Breite ='" + Breite.ToString().Replace(",", ".") + "' " +
                                          ", Laenge ='" + Laenge.ToString().Replace(",", ".") + "' " +
                                          ", Hoehe ='" + Hoehe.ToString().Replace(",", ".") + "' " +
                                          ", MassAnzahl =" + MassAnzahl +
                                          ", Netto='" + Netto.ToString().Replace(",", ".") + "' " +
                                          ", Brutto='" + Brutto.ToString().Replace(",", ".") + "' " +
                                          ", ArtikelArt='" + ArtikelArt + "' " +
                                          ", Besonderheit='" + Besonderheit + "' " +
                                          ", Verpackung ='" + Verpackung + "' " +
                                          ", AbsteckBolzenNr='" + AbsteckBolzenNr + "' " +
                                          ", MEAbsteckBolzen =" + MEAbsteckBolzen +
                                          ", Arbeitsbereich =" + ArbeitsbereichID +
                                          ", LieferantenID =" + LieferantenID +
                                          ", aktiv=" + Convert.ToInt32(Aktiv) +
                                          ", Mindestbestand='" + MindestBestand.ToString().Replace(",", ".") + "'" +
                                          ", BestellNr='" + BestellNr + "' " +
                                          ", Zusatz ='" + Zusatz + "'" +
                                          ", Einheit ='" + Einheit + "'" +
                                          ", Verweis ='" + Verweis + "'" +
                                          ", Werksnummer ='" + Werksnummer + "'" +
                                          ", IsStackable = " + Convert.ToInt32(IsStackable) +
                                          ", UseProdNrCheck= " + Convert.ToInt32(UseProdNrCheck) +
                                          ", tmpLiefVerweis= '" + this.tmpLiefVerweis + "'" +
                                          ", DelforVerweis = '" + this.DelforVerweis + "'" +
                                          ", IgnoreEdi =" + Convert.ToInt32(IgnoreEdi) +

                                                            " WHERE ID=" + ID + ";";
            if (clsSQLcon.ExecuteSQL(strSql, BenutzerID))
            {
                //Add Logbucheintrag
                string logBeschreibung = "Güterart: " + ViewID + " " + Bezeichnung + " geändert";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), logBeschreibung);
            }
        }
        ///<summary>clsGut/GetMatchCodeByBezeichnung</summary>
        ///<remarks>Initialisiert den Formbereich / Tarif</remarks>
        public static string GetMatchCodeByBezeichnung(string _Bezeichnung, decimal decBenutzerID)
        {
            string strSql = string.Empty;
            string strTmp = string.Empty;
            strSql = "Select ViewID FROM Gueterart WHERE Bezeichnung ='" + _Bezeichnung + "'";
            strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, decBenutzerID);
            return strTmp;
        }
        ///<summary>clsGut/afMinMaxTarifInit</summary>
        ///<remarks>Initialisiert den Formbereich / Tarif</remarks>
        public static DataTable GetGArtenForCombo(decimal decBenutzerID)
        {
            string strSql = string.Empty;
            strSql = "Select a.ID, a.ViewID, a.Bezeichnung, c.AbBereichID " +
                                                  " FROM ArbeitsbereichGArten c " +
                                                  "LEFT JOIN Arbeitsbereich b ON b.ID=c.AbBereichID " +
                                                  "LEFT JOIN Gueterart a on a.ID=c.GArtID " +
                                                  " Order BY a.Bezeichnung";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, decBenutzerID, "Güterarten");
            DataRow row = dt.NewRow();
            row["ID"] = 0;
            row["Bezeichnung"] = "--keine Güterart--";
            dt.Rows.Add(row);
            dt.DefaultView.Sort = "ID";
            DataTable reDT = dt.DefaultView.ToTable();
            return reDT;
        }
        ///<summary>clsGut/ExistGArtByBezeichnung</summary>
        ///<remarks>Check Güterart by Bezeichnung</remarks>
        public static bool ExistGArtByBezeichnung(Globals._GL_USER GLUser, string strBezeichnung)
        {
            bool bExist = false;
            string strSql = string.Empty;
            strSql = "Select ID FROM Gueterart WHERE Bezeichnung ='" + strBezeichnung + "'";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, GLUser.User_ID);
            decimal decTmp = 0;
            Decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                bExist = true;
            }
            return bExist;
        }
        ///<summary>clsGut/ExistGArtByID</summary>
        ///<remarks>Check Güterart by ID</remarks>
        public static bool ExistGArtByID(Globals._GL_USER GLUser, decimal myID)
        {
            bool bExist = false;
            string strSql = string.Empty;
            strSql = "Select ID FROM Gueterart WHERE ID =" + myID + ";";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, GLUser.User_ID);
            decimal decTmp = 0;
            Decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                bExist = true;
            }
            return bExist;
        }
        ///<summary>clsGut/Fill</summary>
        ///<remarks>Füllt die Klasse</remarks>ks>
        public void Fill()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM Gueterart WHERE ID=" + ID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Gut");

            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                this.ID = decTmp;
                this.ViewID = dt.Rows[i]["ViewID"].ToString();
                this.Bezeichnung = dt.Rows[i]["Bezeichnung"].ToString().Trim();
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["Dicke"].ToString(), out decTmp);
                this.Dicke = decTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["Breite"].ToString(), out decTmp);
                this.Breite = decTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["Laenge"].ToString(), out decTmp);
                this.Laenge = decTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["Hoehe"].ToString(), out decTmp);
                this.Hoehe = decTmp;
                Int32 iTmp = 0;
                Int32.TryParse(dt.Rows[i]["MassAnzahl"].ToString(), out iTmp);
                this.MassAnzahl = iTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["Netto"].ToString(), out decTmp);
                this.Netto = decTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["Brutto"].ToString(), out decTmp);
                this.Brutto = decTmp;
                this.ArtikelArt = dt.Rows[i]["ArtikelArt"].ToString().Trim(); ;
                this.Besonderheit = dt.Rows[i]["Besonderheit"].ToString().Trim();
                this.Verpackung = dt.Rows[i]["Verpackung"].ToString().Trim(); ;
                this.AbsteckBolzenNr = dt.Rows[i]["AbsteckBolzenNr"].ToString().Trim();
                iTmp = 0;
                Int32.TryParse(dt.Rows[i]["MEAbsteckBolzen"].ToString(), out iTmp);
                this.MEAbsteckBolzen = iTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["Arbeitsbereich"].ToString(), out decTmp);
                this.ArbeitsbereichID = decTmp;
                this.LieferantenID = (decimal)dt.Rows[i]["LieferantenID"];
                this.Aktiv = (bool)dt.Rows[i]["aktiv"];
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["Mindestbestand"].ToString(), out decTmp);
                this.MindestBestand = decTmp;
                this.BestellNr = dt.Rows[i]["BestellNr"].ToString().Trim();
                this.Zusatz = dt.Rows[i]["Zusatz"].ToString();
                this.Einheit = dt.Rows[i]["Einheit"].ToString();
                this.Verweis = dt.Rows[i]["Verweis"].ToString();
                this.Werksnummer = dt.Rows[i]["Werksnummer"].ToString().Trim();
                this.IsStackable = (bool)dt.Rows[i]["IsStackable"];
                this.UseProdNrCheck = (bool)dt.Rows[i]["UseProdNrCheck"];
                this.tmpLiefVerweis = string.Empty;
                if (dt.Rows[i]["tmpLiefVerweis"] != null)
                {
                    this.tmpLiefVerweis = dt.Rows[i]["tmpLiefVerweis"].ToString();
                }
                this.DelforVerweis = dt.Rows[i]["DelforVerweis"].ToString();
                this.IgnoreEdi = false;
                if (dt.Rows[i]["IgnoreEdi"] != null)
                {
                    this.IgnoreEdi = (bool)dt.Rows[i]["IgnoreEdi"];
                }


                //StyleSheet
                //Style = new clsStyleSheetColumn();
                //Style._GL_User = this.GLUser;
                //Style.FTable = "Gueterarten";
                //Style.FTableID = this.ID;
                //Style.GetDataTableStyleSheet();

                if (this.ViewID.Equals("418"))
                {
                    string st = string.Empty;
                }

                GutADR = new clsGueterartADR();
                GutADR.InitClass(this.GLUser, this.GLSystem);
                GutADR.GArtID = this.ID;
                GutADR.AbBereichID = this.GLSystem.sys_ArbeitsbereichID;
                this.VDA4905LieferantenInfo = GutADR.AssignAdrsAsString;

                AbBereichGut = new clsArbeitsbereichGArten();
                AbBereichGut.AbBereichID = this.GLSystem.sys_ArbeitsbereichID;
                AbBereichGut.GArtID = this.ID;
            }
        }
        ///<summary>clsGut/CheckGArtIsUsed</summary>
        ///<remarks></remarks>ks>
        private bool CheckGArtIsUsed()
        {
            string strSql = string.Empty;
            strSql = "SELECT DISTINCT a.ViewID FROM Gueterart a " +
                                                "INNER JOIN Artikel b ON b.GArtID=a.ID " +
                                                "WHERE a.ID=" + ID + ";";
            bool bCheck = clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            return bCheck;
        }
        ///<summary>clsGut/ GueterArtTable</summary>
        ///<remarks></remarks>
        public static DataTable GetGueterartenForList(Globals._GL_USER myGLUser, Int32 mySelection)//,bool bShowAll)
        {
            string strSql = "SELECT DISTINCT " +
                                    "a.ID " +
                                    ", a.ViewID" +
                                    ", a.Bezeichnung" +
                                    ", a.Dicke" +
                                    ", a.Breite" +
                                    ", a.Laenge as Länge" +
                                    ", a.Hoehe as Höhe" +
                                    ", a.MassAnzahl" +
                                    ", a.Netto" +
                                    ", a.Brutto" +
                                    ", a.ArtikelArt" +
                                    ", a.Besonderheit" +
                                    ", a.Verpackung" +
                                    ", a.AbsteckBolzenNr" +
                                    ", a.MEAbsteckBolzen" +
                                    ", a.aktiv" +
                                    ", a.Mindestbestand" +
                                    ", a.BestellNr" +
                                    ", a.Zusatz" +
                                    ", a.Einheit" +
                                    ", a.Verweis" +
                                    ", a.Werksnummer" +
                                    ", a.IgnoreEdi" +
                                    ", CASE " +
                                        "WHEN a.IsStackable =1 THEN CAST(0 as bit) " +
                                        "ELSE CAST(1 as bit) " +
                                      "END as NichtStapelbar " +
                                    ", b.Name as Arbeitsbereich " +
                                    ", (Select TOP(1) v.LieferantenVerweis from ADRVerweis v " +
                                                                            "INNER JOIN GueterartADR ga on ga.AdrID=v.SenderAdrID " +
                                                                            "WHERE ga.GArtID=c.GArtID AND ArbeitsbereichID=c.AbBereichID) as LieferantNr " +
                                    ", (Select Top(1) adr.Name1 FROM ADR adr " +
                                                              "INNER JOIN GueterartADR gAdr on gAdr.AdrID=adr.ID " +
                                                              "WHERE gAdr.GArtID =a.ID) as Auftraggeber " +
                                    " FROM ArbeitsbereichGArten c " +
                                      "LEFT JOIN Arbeitsbereich b ON b.ID=c.AbBereichID " +
                                      "LEFT JOIN Gueterart a on a.ID=c.GArtID ";

            switch (mySelection)
            {
                //passiv
                case -2:
                    strSql = strSql + "WHERE a.aktiv=0 ";
                    break;
                //aktiv
                case -1:
                    strSql = strSql + "WHERE a.aktiv=1 ";
                    break;
                //alle
                case 0:
                    //strSql = string.Empty;
                    //strSql = "SELECT " +
                    //                    "DISTINCT a.* , b.Name " +
                    //                    "FROM  Gueterart a " +
                    //                    "LEFT JOIN  ArbeitsbereichGArten c on a.ID=c.GArtID " +
                    //                    "LEFT JOIN Arbeitsbereich b ON b.ID=c.AbBereichID ";
                    break;
                default:
                    strSql = strSql + //"INNER JOIN ArbeitsbereichGArten c ON c.AbBereichID=b.ID " +
                                      "WHERE b.ID IN (" + mySelection + ") ";
                    break;
            }
            strSql = strSql + " ORDER BY a.ViewID";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGLUser.User_ID, "Gut");
            return dt;
        }
        ///<summary>clsGut/ SQLAdd</summary>
        ///<remarks></remarks>
        private string SQLAdd()
        {
            UseProdNrCheck = true;
            string strSql = "INSERT INTO Gueterart (ViewID, Bezeichnung, Dicke, Breite, Laenge, Hoehe, MassAnzahl, Netto, Brutto, " +
                                             "ArtikelArt, Besonderheit, Verpackung, AbsteckBolzenNr, MEAbsteckBolzen, " +
                                             "Arbeitsbereich, LieferantenID, aktiv, Mindestbestand, BestellNr, Zusatz, Einheit, Verweis, " +
                                             "Werksnummer, IsStackable, UseProdNrCheck, tmpLiefVerweis, DelforVerweis, IgnoreEdi" +
                                              ") " +
                                              "VALUES (" +
                                                          "'" + ViewID + "'" +
                                                          ", '" + Bezeichnung + "'" +
                                                          ", '" + Dicke.ToString().Replace(",", ".") + "'" +
                                                          ", '" + Breite.ToString().Replace(",", ".") + "'" +
                                                          ", '" + Laenge.ToString().Replace(",", ".") + "'" +
                                                          ", '" + Hoehe.ToString().Replace(",", ".") + "'" +
                                                          ", " + MassAnzahl +
                                                          ", '" + Netto.ToString().Replace(",", ".") + "'" +
                                                          ", '" + Brutto.ToString().Replace(",", ".") + "'" +
                                                          ", '" + ArtikelArt + "' " +
                                                          ", '" + Besonderheit + "' " +
                                                          ", '" + Verpackung + "' " +
                                                          ", '" + AbsteckBolzenNr + "' " +
                                                          ", " + MEAbsteckBolzen +
                                                          ", " + ArbeitsbereichID +
                                                          ", " + LieferantenID +
                                                          ", " + Convert.ToInt32(Aktiv) +
                                                          ", '" + MindestBestand.ToString().Replace(",", ".") + "'" +
                                                          ", '" + BestellNr + "' " +
                                                          ", '" + Zusatz + "'" +
                                                          ", '" + Einheit + "'" +
                                                          ", '" + Verweis + "'" +
                                                          ", '" + Werksnummer + "'" +
                                                          ", " + Convert.ToInt32(IsStackable) +
                                                          ", " + Convert.ToInt32(UseProdNrCheck) +
                                                          ", '" + this.tmpLiefVerweis + "'" +
                                                          ", '" + this.DelforVerweis + "'" +
                                                          ", " + Convert.ToInt32(IgnoreEdi) +
                                              " ); ";
            return strSql;
        }
        ///<summary>clsGut/ Add</summary>
        ///<remarks>Fügt einen neue Güterart hinzu</remarks>
        public void Add()
        {
            string strSql = SQLAdd();
            strSql = strSql + "Select @@IDENTITY as 'ID' ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                ID = decTmp;
                clsGut.AddGutArbeitsbereich(this, this.GLSystem.sys_ArbeitsbereichID, this.GLUser.User_ID);
                //this.AbBereichGut = new clsArbeitsbereichGArten();
                //this.AbBereichGut.AbBereichID = this.GLSystem.sys_ArbeitsbereichID;
                //this.AbBereichGut.GArtID = this.ID;
                //this.AbBereichGut.Add();
                ////Add Logbucheintrag
                //string logBeschreibung = "Güterart ID: [" + ID + "] - " + ViewID + " " + Bezeichnung + " hinzugefügt";
                //Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Eintrag.ToString(), logBeschreibung);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myGArtId"></param>
        public static void AddGutArbeitsbereich(clsGut myGut, decimal myAbBereichID, decimal myUserId)
        {
            clsArbeitsbereichGArten AbBereichGut = new clsArbeitsbereichGArten();
            AbBereichGut.AbBereichID = myAbBereichID;
            AbBereichGut.GArtID = myGut.ID;
            AbBereichGut.Add();
            //Add Logbucheintrag
            string logBeschreibung = "Güterart ID: [" + myGut.ID + "] - " + myGut.ViewID + " " + myGut.Bezeichnung + " hinzugefügt";
            Functions.AddLogbuch(myUserId, enumLogbuchAktion.Eintrag.ToString(), logBeschreibung);
        }

        ///<summary>clsGut/ Delete</summary>
        ///<remarks>Datensatz löschen</remarks>
        public void Delete()
        {
            clsGut tmpGut = this;
            string strSql = "DELETE FROM Gueterart WHERE ID=" + ID + ";";
            if (clsSQLcon.ExecuteSQL(strSql, BenutzerID))
            {
                //Add Logbucheintrag
                //Bezeichnung = GetBezeichnungByID(ID);
                //ViewID = GetMatchCodeByBezeichnung(Bezeichnung, BenutzerID);
                string Beschreibung = "Güterart ID: [" + tmpGut.ID + "] - " + tmpGut.ViewID + " " + tmpGut.Bezeichnung + " gelöscht";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), Beschreibung);
            }
        }
        ///<summary>clsGut/ FillDictGutVda4095</summary>
        ///<remarks></remarks>
        public void FillDictGutVda4095(bool bActiveOnly)
        {
            this.DictGutVda4905 = new Dictionary<decimal, clsGut>();
            this.ListGutIDVDA4905 = new List<decimal>();
            string strSQL = string.Empty;

            //strSQL = "SELECT Top (15) g.* " +
            strSQL = "SELECT g.* " +
                                    "FROM Gueterart g " +
                                    "INNER JOIN ArbeitsbereichGArten aGA on aGa.GArtID=g.ID " +
                                    "WHERE " +
                                        "(g.Verweis<>'' OR g.Verweis IS NOT NULL) ";

            //strSQL += "AND g.ViewID IN ('480', '506', '518', '651', '525','527','588', '625', '627' ) ";
            //strSQL += "AND g.ViewID IN ('518' ) ";
            if (bActiveOnly)
            {
                strSQL += "AND g.aktiv = 1 ";
            }
            strSQL += " AND g.ID<>1 AND aGA.AbBereichID=" + (int)this.GLSystem.sys_ArbeitsbereichID +
                      " ORDER BY g.ViewID ;";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, GLUser.User_ID, "Gut");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    clsGut tmpGut = new clsGut();
                    tmpGut.InitClass(this.GLUser, this.GLSystem);
                    tmpGut.ID = decTmp;
                    tmpGut.Fill();
                    if (!this.DictGutVda4905.ContainsKey(tmpGut.ID))
                    {
                        this.DictGutVda4905.Add(tmpGut.ID, tmpGut);
                        this.ListGutIDVDA4905.Add(tmpGut.ID);
                    }
                    else
                    {
                        string strTeest = string.Empty;
                    }
                }
            }
        }

        public bool CreateNewGArtByASN(decimal myAdrID, string strVerweis)
        {
            bool bReturn = false;
            clsADR tmpADR = new clsADR();
            tmpADR.InitClass(this.GLUser, this.GLSystem, myAdrID, true);

            clsGut tmpGut = new clsGut();
            tmpGut.InitClass(this.GLUser, this.GLSystem);
            tmpGut.ViewID = strVerweis;
            tmpGut.Bezeichnung = strVerweis;
            tmpGut.Dicke = 0;
            tmpGut.Breite = 0;
            tmpGut.Laenge = 0;
            tmpGut.Hoehe = 0;
            tmpGut.MassAnzahl = 0;
            tmpGut.Netto = 0;
            tmpGut.Brutto = 0;
            tmpGut.ArtikelArt = String.Empty;
            tmpGut.Besonderheit = string.Empty;
            tmpGut.Verpackung = string.Empty;
            tmpGut.AbsteckBolzenNr = string.Empty;
            tmpGut.MEAbsteckBolzen = 0;
            tmpGut.ArbeitsbereichID = 0; //diese Feld ist inaktiv-> table ArbeitsbereichGArten
            tmpGut.LieferantenID = 0;   // diese Feld ist inaktiv
            tmpGut.Aktiv = true;
            tmpGut.MindestBestand = 0;
            tmpGut.BestellNr = string.Empty;
            tmpGut.Zusatz = string.Empty;
            tmpGut.Einheit = "KG";
            tmpGut.Verweis = strVerweis;
            tmpGut.Werksnummer = strVerweis;
            tmpGut.tmpLiefVerweis = string.Empty;
            tmpGut.IgnoreEdi = false;

            string strSQL = string.Empty;
            //neu GArt anlegen
            strSQL = "DECLARE @GArtID as decimal(28,0); ";
            strSQL = strSQL + tmpGut.SQLAdd();
            strSQL = strSQL + "SET @GArtID=(Select @@IDENTITY as 'ID' ); ";
            //Verweis Arbeitsberech/Güterart
            strSQL = strSQL + "INSERT INTO ArbeitsbereichGArten (AbBereichID, GArtID) " +
                                               "VALUES (" + (Int32)this.GLSystem.sys_ArbeitsbereichID +
                                                        ", @GArtID" +
                                                        "); ";
            //Verweis Güterart Adresse
            strSQL = strSQL + "INSERT INTO GueterartADR (GArtID, AdrID, AbBereichID) " +
                                       "VALUES (@GArtID " +
                                                 ", " + (Int32)myAdrID +
                                                 ", " + (Int32)this.GLSystem.sys_ArbeitsbereichID +
                                                "); ";
            strSQL = strSQL + "Select @GArtID;";
            string strTmp = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSQL, "NeueGArt", BenutzerID);
            decimal decTmp = 0;
            Decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                this.ID = decTmp;
                this.Fill();
                bReturn = true;
            }
            return bReturn;
        }
        /********************************************************************************************
         *                                  public static 
         * *****************************************************************************************/
        ///<summary>clsGut/ ViewIDExistsByID</summary>
        ///<remarks></remarks>
        public static bool ViewIDExistsByID(Globals._GL_USER myGLUser, string ViewIDG, decimal decID)
        {
            string strSql = "SELECT Bezeichnung FROM Gueterart WHERE ViewID='" + ViewIDG + "' AND ID=" + decID + ";";
            bool bExist = clsSQLcon.ExecuteSQL_GetValueBool(strSql, myGLUser.User_ID);
            return bExist;
        }
        ///<summary>clsGut/ GueterArtTable</summary>
        ///<remarks></remarks>
        public static DataTable GetGueterarten(Globals._GL_USER GLUser, decimal myAbBereichID)
        {
            string strSQL = "SELECT ID" +
                                    ", ViewID as 'Suchbegriff'" +
                                    ", Bezeichnung " +
                                    ", Dicke" +
                                    ", Breite" +
                                    ", Laenge" +
                                    ", Hoehe" +
                                    ", MassAnzahl" +
                                    ", Netto" +
                                    ", Brutto" +
                                    ", ArtikelArt" +
                                    ", Besonderheit" +
                                    ", Verpackung" +
                                    ", AbsteckBolzenNr" +
                                    ", MEAbsteckBolzen" +
                                    ", Arbeitsbereich" +
                                    ", LieferantenID" +
                                    ", aktiv" +
                                    ", Mindestbestand" +
                                    ", BestellNr" +
                                    ", IgnoreEdi" +

                                        " FROM Gueterart where Arbeitsbereich=" + (int)myAbBereichID + "  ORDER BY Suchbegriff";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, GLUser.User_ID, "Gut");
            return dt;
        }
        ///<summary>clsGut/ ViewIDExists</summary>
        ///<remarks></remarks>
        public static bool ViewIDExists(Globals._GL_USER myGLUser, string ViewIDG)
        {
            string strSQL = "SELECT ID FROM Gueterart WHERE ViewID='" + ViewIDG + "'";
            bool bExist = clsSQLcon.ExecuteSQL_GetValueBool(strSQL, myGLUser.User_ID);
            return bExist;
        }
        ///<summary>clsGut/ GetGutByADRAndVerweis</summary>
        ///<remarks></remarks>
        public static decimal GetGutByADRAndVerweis(Globals._GL_USER myGLUser, clsADR myAdrClass, string myVerweis, decimal myArBereich)
        {
            string strSQL = "Select Top(1) g.ID FROM Gueterart g " +
                                        "INNER JOIN GueterartADR gADR on gADR.GArtID=g.ID " +
                                        "WHERE " +
                                            " REPLACE(g.Verweis, ' ', '') = REPLACE('" + myVerweis + "', ' ', '') " +
                                            "AND gADR.AbBereichID= " + myArBereich +
                                             " AND gADR.AdrID=" + myAdrClass.ID +
                                             " Order by g.ID ;";

            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, myGLUser.User_ID);
            decimal decTmp = 0;
            Decimal.TryParse(strTmp, out decTmp);
            if (decTmp == 0)
            {
                //es liegt keine Verweis vor jetzt prüfen, ob wir für diese Adresse eine Default Güterart haben
                clsKundGArtDefault tmDefault = new clsKundGArtDefault();
                decTmp = 1;
                if (myAdrClass.KdGartDefault.DictKundeGartDefault != null)
                {
                    if (myAdrClass.KdGartDefault.DictKundeGartDefault.TryGetValue(myArBereich, out tmDefault))
                    {
                        decTmp = tmDefault.GArtID;
                    }
                    //else
                    //{
                    //    decTmp = 1;
                    //}
                }
            }
            return decTmp;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetViewIDOffer(Globals._GL_USER myGLUser)
        {
            string strReturn = string.Empty;
            string strSQL = string.Empty;
            strSQL = "Select MAX(t.nr) from (" +
                                                "SELECT CAST(ViewID as int) as nr FROM Gueterart " +
                                                                                    " where (ViewID not like N'%[^0-9]%')" +
                                            ") t;";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, myGLUser.User_ID);
            int iTmp = 0;
            int.TryParse(strTmp, out iTmp);
            if (iTmp > 0)
            {
                strReturn = (iTmp + 1).ToString();
            }
            return strReturn;
        }


    }
}
