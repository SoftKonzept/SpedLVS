using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
//using System.Windows.Forms;

namespace LVS
{
    public class clsADR
    {
        public Globals._GL_USER _GL_User;
        public Globals._GL_SYSTEM _GL_System;
        public clsKunde Kunde = new clsKunde();
        public clsKontakte Kontakt = new clsKontakte();
        public clsADRCat AdrCategory = new clsADRCat();
        public clsADRText ADRTexte = new clsADRText();
        public clsADRVerweis AdrVerweis = new clsADRVerweis();
        public clsLieferantenGruppe LiefGroup = new clsLieferantenGruppe();
        internal clsKundGArtDefault KdGartDefault = new clsKundGArtDefault();

        // KOnstanten aus Control 
        public const Int32 const_AdrRange_Kundenliste = 0;
        public const Int32 const_AdrRange_AdrListeKomplett = 1;
        public const Int32 const_AdrRange_AdrListAktiv = 2;
        public const Int32 const_AdrRange_AdrListePassiv = 3;
        public const Int32 const_AdrRange_AdrListeKunde = 4;
        public const Int32 const_AdrRange_AdrListeVersender = 5;
        public const Int32 const_AdrRange_AdrListeEmpfaenger = 6;
        public const Int32 const_AdrRange_AdrListeEntlade = 7;
        public const Int32 const_AdrRange_AdrListeSpedition = 8;
        public const Int32 const_AdrRange_AdrListeBelade = 9;
        public const Int32 const_AdrRange_AdrListePost = 10;
        public const Int32 const_AdrRange_AdrListeRechnung = 11;
        public const Int32 const_AdrRange_AdrListeDiverse = -1;

        public const string const_AdrRange_KundenlisteString = "";
        public const string const_AdrRange_AdrListAktivString = "Adressliste [aktiv]";
        public const string const_AdrRange_AdrListePassivString = "Adressliste [passiv]";
        public const string const_AdrRange_AdrListeKundeString = "Kunden-/Auftraggeberadressliste";
        public const string const_AdrRange_AdrListeVersenderString = "Versandadressliste";
        public const string const_AdrRange_AdrListeEmpfaengerString = "Empfangsadressliste";
        public const string const_AdrRange_AdrListeEntladeString = "Entladestellenadressen";
        public const string const_AdrRange_AdrListeSpeditionString = "Spedition / Transportunternehmer";
        public const string const_AdrRange_AdrListeBeladeString = "Beladestellenadressen";
        public const string const_AdrRange_AdrListePostString = "Postadresse";
        public const string const_AdrRange_AdrListeRechnungString = "Rechnungsadresse";
        public const string const_AdrRange_AdrListeDiverseString = "Sonstige Adressen";


        public Dictionary<string, clsADRVerweis> DictAdrVerweis = new Dictionary<string, clsADRVerweis>();
        public Dictionary<int, string> DictAdrCatagorySelect = new Dictionary<int, string>();
        public Dictionary<int, string> DictAdrCatagoryUnSelect = new Dictionary<int, string>();
        private Dictionary<int, string> DictAdrCatagory = new Dictionary<int, string>()
        {
            { 0, "Auftraggeber / Kunde" },
            { 1, "Versender / Lieferant" },
            { 2, "Beladeadresse" },
            { 3, "Empfänger" },
            { 4, "Entladeadresse" },
            { 5, "Postadresse" },
            { 6, "Rechnungsadresse" },
            { 7, "Diverse" },
            { 8, "Spedition" }
        };

        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = _GL_User.User_ID;
                return _BenutzerID;
            }
            set
            {
                _BenutzerID = value;
            }
        }
        private clsSystem _sys;
        public clsSystem sys
        {
            get { return _sys; }
            set
            {
                _sys = value;
                //this.AbBereichID = _sys.AbBereich.ID;
                //this.MandantenID = _sys.AbBereich.MandantenID;
            }
        }
        public const decimal const_PostEinstellunge_Default = 0;

        //************************************
        public decimal ID { get; set; }
        public string ViewID { get; set; }
        public decimal KD_ID { get; set; }
        public string FBez { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string PF { get; set; }
        public string Str { get; set; }
        public string HausNr { get; set; }
        public string PLZ { get; set; }
        public string PLZPF { get; set; }
        public string Ort { get; set; }
        public string OrtPF { get; set; }
        public string Land { get; set; }
        public string LKZ { get; set; }
        private DateTime _WAvon = default(DateTime);
        public DateTime WAvon
        {
            get
            {
                return _WAvon;
            }
            set
            {
                _WAvon = value;
            }
        }
        private DateTime _WAbis = default(DateTime);
        public DateTime WAbis
        {
            get
            {
                return _WAbis;
            }
            set
            {
                _WAbis = value;
            }
        }
        private DateTime _Date_Add = default(DateTime);
        public DateTime Date_Add
        {
            get
            {
                return _Date_Add;
            }
            set
            {
                _Date_Add = value;
            }
        }
        public bool Dummy { get; set; }
        private bool _IsUsed;       //=true, dann kann der Datensatz nicht gelöscht werden
        public bool IsUsed
        {
            get
            {
                if (ExistAdrID(ID, BenutzerID))
                {
                    _IsUsed = IsADRUsed(ID, BenutzerID);
                }
                else
                {
                    _IsUsed = false;
                }
                return _IsUsed;
            }
            set
            {
                IsUsed = value;
            }
        }
        private string _ADRString;  //komplette Adresse in einem String
        public string ADRString
        {
            get
            {
                string strTmp = string.Empty;
                if (FBez != string.Empty)
                {
                    strTmp = FBez;
                }
                if (Name1 != string.Empty)
                {
                    strTmp = strTmp + Environment.NewLine + Name1;
                }
                if (Name2 != string.Empty)
                {
                    strTmp = strTmp + Environment.NewLine + Name2;
                }
                if (Name3 != string.Empty)
                {
                    strTmp = strTmp + Environment.NewLine + Name3;
                }
                if (Str != string.Empty)
                {
                    strTmp = strTmp + Environment.NewLine + Str + " " + HausNr;
                }
                if (Ort != string.Empty)
                {
                    strTmp = strTmp + Environment.NewLine +
                             Environment.NewLine + PLZ + " " + Ort;
                }
                if (Land != string.Empty)
                {
                    strTmp = strTmp + Environment.NewLine + Land;
                }
                _ADRString = strTmp;
                return _ADRString;
            }
            set
            {
                _ADRString = value;
            }
        }
        public string ADRStringShort
        {
            get
            {
                string strTmp = string.Empty;
                if (ViewID != string.Empty)
                {
                    strTmp = ViewID;
                }
                if (Name1 != string.Empty)
                {
                    strTmp = strTmp + " - " + Name1;
                }
                if (PLZ != string.Empty)
                {
                    strTmp = strTmp + " - " + PLZ;
                }
                if (Ort != string.Empty)
                {
                    strTmp = strTmp + " " + Ort;
                }

                //_ADRString = strTmp;
                return strTmp;
            }
            //set
            //{
            //    _ADRString = value;
            //}
        }

        public string ADRStringShortWithID
        {
            get
            {
                string strTmp = string.Empty;
                strTmp = "[" + this.ID + "]";
                if (ViewID != string.Empty)
                {
                    strTmp += " - " + ViewID;
                }
                if (Name1 != string.Empty)
                {
                    strTmp = strTmp + " - " + Name1;
                }
                if (PLZ != string.Empty)
                {
                    strTmp = strTmp + " - " + PLZ;
                }
                if (Ort != string.Empty)
                {
                    strTmp = strTmp + " " + Ort;
                }
                return strTmp;
            }
            //set
            //{
            //    _ADRString = value;
            //}
        }
        public string UserInfoTxt { get; set; }
        public bool activ { get; set; }
        public decimal Lagernummer { get; set; }
        private decimal _LagernummerMax;
        public decimal LagernummerMax
        {
            get
            {
                string strSQL = "Select MAX(a.ADRLagerNr) FROM Artikel a " +
                                "INNER JOIN LEingang c ON c.ID=a.LEingangTableID " +
                                "INNER JOIN ADR b ON b.ID=c.Auftraggeber " +
                                "WHERE " +
                                "c.Auftraggeber=" + ID + ";";
                decimal decTmp = 0;
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                Decimal.TryParse(strTmp, out decTmp);
                _LagernummerMax = decTmp;
                return _LagernummerMax;
            }
            set
            {
                _LagernummerMax = value;
            }
        }
        public bool ASNCommunication { get; set; }
        public decimal AdrID_Post { get; set; }// Postanschrift
        public decimal AdrID_RG { get; set; }// Rechnungsanschrift
        public decimal AdrID_Be { get; set; }// feste Beladeanschrift
        public decimal AdrID_Ent { get; set; }// feste Entladeanschrift
        public bool IsAuftraggeber { get; set; }// Adress kann Auftraggeber sein
        public bool IsVersender { get; set; }// Adress kann Versender sein
        public bool IsBelade { get; set; }// Adress kann Versender sein
        public bool IsEmpfaenger { get; set; }// Adress kann Empfänger sein
        public bool IsEntlade { get; set; }// Adress kann Empfänger sein
        public bool IsPost { get; set; }// Adress kann Postadresse sein
        public bool IsRG { get; set; }// Adress kann Rechnungsadresse sein
        public bool CalcLagerVers { get; set; }// Adress kann Auftraggeber sein
        public string DocEinlagerAnzeige { get; set; }
        public string DocAuslagerAnzeige { get; set; }
        public string Verweis { get; set; }
        public decimal PostRGBy { get; set; }
        public decimal PostAnlageBy { get; set; }
        public decimal PostLfsBy { get; set; }
        public decimal PostListBy { get; set; }
        public decimal PostAnzeigeBy { get; set; }
        public bool IsDiv { get; set; }// Adress kann Diverse sein 
        public bool IsSpedition { get; set; }// Adress kann Spedition sein
        public DataTable dtADRPost { get; set; }
        public DataTable dtADRRechnungen { get; set; }
        public DataTable dtADRVersender { get; set; }
        public DataTable dtADRBeladeadresse { get; set; }
        public DataTable dtADREntladeadresse { get; set; }
        public DataTable dtADREmpfaenger { get; set; }
        public DataTable dtADRAuftraggeber { get; set; }
        public int DUNSNr { get; set; }

        /********************************************************************************************************
         *                                  Methoden
         * *****************************************************************************************************/
        ///<summary>clsADR / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSystem, decimal myID, bool InitClassOnly)
        {
            this._GL_User = myGLUser;
            this._GL_System = myGLSystem;
            if (myID > 0)
            {
                this.ID = myID;
                Fill();
            }
            if (!InitClassOnly)
            {
                FillClassADRTable();
                //InitSubClasses();
            }
        }
        ///<summary>clsADR / Copy</summary>
        ///<remarks></remarks>
        public clsADR Copy()
        {
            return (clsADR)this.MemberwiseClone();
        }
        ///<summary>clsADR / GetNewDataRowFromTable</summary>
        ///<remarks></remarks>
        private DataRow GetNewDataRowFromTable(DataTable myDT)
        {
            DataRow retRow = myDT.NewRow();
            retRow["ID"] = 0;
            retRow["Matchcode"] = " Standard ";
            retRow["Adresse"] = string.Empty;
            return retRow;
        }
        ///<summary>clsADR / FillClassADRTable</summary>
        ///<remarks>Code für iTable:
        ///         1 = Postadresse
        ///         2 = Rechnungsadressen
        ///         3 = Versender
        ///         4 = </remarks>
        public void FillClassADRTable()
        {
            //Standard Row erstellen für alle Tabelle
            string strSQL = GetSQLForClassADRTable(1);
            dtADRPost = new DataTable();
            dtADRPost = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "PostADR");
            dtADRPost.Rows.Add(GetNewDataRowFromTable(dtADRPost));
            dtADRPost.DefaultView.Sort = "Matchcode";

            //Rechnungen
            strSQL = string.Empty;
            strSQL = GetSQLForClassADRTable(2);
            dtADRRechnungen = new DataTable();
            dtADRRechnungen = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "RGADR");
            dtADRRechnungen.Rows.Add(GetNewDataRowFromTable(dtADRRechnungen));
            dtADRRechnungen.DefaultView.Sort = "Matchcode";

            //Versender
            strSQL = string.Empty;
            strSQL = GetSQLForClassADRTable(3);
            dtADRVersender = new DataTable();
            dtADRVersender = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "VersandADR");
            dtADRVersender.Rows.Add(GetNewDataRowFromTable(dtADRVersender));
            dtADRVersender.DefaultView.Sort = "Matchcode";

            //Beladeadressen
            strSQL = string.Empty;
            strSQL = GetSQLForClassADRTable(4);
            dtADRBeladeadresse = new DataTable();
            dtADRBeladeadresse = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "BeladeADR");
            dtADRBeladeadresse.Rows.Add(GetNewDataRowFromTable(dtADRBeladeadresse));
            dtADRBeladeadresse.DefaultView.Sort = "Matchcode";

            //Entladeadressen
            strSQL = string.Empty;
            strSQL = GetSQLForClassADRTable(5);
            dtADREntladeadresse = new DataTable();
            dtADREntladeadresse = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EntladeADR");
            dtADREntladeadresse.Rows.Add(GetNewDataRowFromTable(dtADREntladeadresse));
            dtADREntladeadresse.DefaultView.Sort = "Matchcode";

            //Empfänger
            strSQL = string.Empty;
            strSQL = GetSQLForClassADRTable(6);
            dtADREmpfaenger = new DataTable();
            dtADREmpfaenger = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EmpfangsADR");
            dtADREmpfaenger.Rows.Add(GetNewDataRowFromTable(dtADREmpfaenger));
            dtADREmpfaenger.DefaultView.Sort = "Matchcode";

            //Auftraggeber
            strSQL = string.Empty;
            strSQL = GetSQLForClassADRTable(7);
            dtADRAuftraggeber = new DataTable();
            dtADRAuftraggeber = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "AuftraggeberADR");
            dtADRAuftraggeber.Rows.Add(GetNewDataRowFromTable(dtADRAuftraggeber));
            dtADRAuftraggeber.DefaultView.Sort = "Matchcode";
        }

        ///<summary>clsADR / GetSQLForClassADRTable</summary>
        ///<remarks></remarks>
        private string GetSQLForClassADRTable(Int32 iTable)
        {
            string strSQL = string.Empty;
            strSQL = "Select a.ID" +
                     ", a.ViewID as Matchcode" +
                     ", a.Name1" +
                     ", a.Str +' '+a.HausNr as Anschrift" +
                     ", a.PLZ" +
                     ", a.Ort" +
                     ", a.ViewID+' | '+ a.Name1 +' - '+ a.PLZ+' '+a.Ort as Adresse " +
                     " FROM ADR a " +
                     "WHERE a.Dummy=0 ";
            switch (iTable)
            {
                case 0:
                    strSQL = strSQL + " AND ID=0;";
                    break;
                case 1:
                    strSQL = strSQL + " AND IsPost=1;";
                    break;
                case 2:
                    strSQL = strSQL + " AND IsRG=1;";
                    break;
                case 3:
                    strSQL = strSQL + " AND IsVersender=1;";
                    break;
                case 4:
                    strSQL = strSQL + " AND IsBelade=1;";
                    break;
                case 5:
                    strSQL = strSQL + " AND IsEntlade=1;";
                    break;
                case 6:
                    strSQL = strSQL + " AND IsEmpfaenger=1;";
                    break;
                case 7:
                    strSQL = strSQL + " AND IsAuftraggeber=1;";
                    break;
                case 8:
                    strSQL = strSQL + " AND IsDiv=1;";
                    break;
                case 9:
                    strSQL = strSQL + " AND IsSpedition=1;";
                    break;
            }
            return strSQL;
        }

        ///<summary>clsADR / InitSubClasses</summary>
        ///<remarks></remarks>
        private void InitSubClasses()
        {
            Kunde = new clsKunde();
            Kunde._GL_User = this._GL_User;

            Kontakt = new clsKontakte();
            Kontakt._GL_User = this._GL_User;
            Kontakt._GL_System = this._GL_System;

            ADRTexte = new clsADRText();
            ADRTexte.InitClass(this._GL_User, this._GL_System, this.sys, this.ID);

            KdGartDefault = new clsKundGArtDefault();
            KdGartDefault.InitClass(this._GL_User, this._GL_System, this.ID);

            LiefGroup = new clsLieferantenGruppe();
            LiefGroup.InitClass(this._GL_User, this._GL_System, this.sys);

            AdrVerweis = new clsADRVerweis();
            AdrVerweis.InitClass(this._GL_User, this.ID, this.sys);

            if (this.ID > 0)
            {
                Kunde.ADR_ID = this.ID;
                Kunde.FillbyAdrID();

                Kontakt.ADR_ID = this.ID;
                Kontakt.FillbyAdrID();
            }
        }
        ///<summary>clsADR / FillClassOnly</summary>
        ///<remarks></remarks>>
        public static DataTable GetADRListByIDList(List<decimal> myList, decimal decBenutzerID)
        {
            string strSql = string.Empty;
            strSql = "Select " +
                            "ID" +
                            ", ViewID" +
                            ", Name1 as Name" +
                            ", PLZ" +
                            ", Ort" +
                            " FROM ADR " +
                                "WHERE ID IN (" + String.Join(",", myList.ToArray()) + ");";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, decBenutzerID, "ADR");
            return dt;
        }
        ///<summary>clsADR / GetMatchCodeByID</summary>
        ///<remarks>Ermittelt den Matchcode eines Datensatzes</remarks>
        public static string GetMatchCodeByID(decimal _ADR_ID, decimal decBenutzerID)
        {
            string strSql = string.Empty;
            string strTmp = string.Empty;
            if (_ADR_ID > 0)
            {
                strSql = "Select ViewID FROM ADR WHERE ID='" + _ADR_ID + "'";
                strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, decBenutzerID);
            }
            return strTmp;
        }
        ///<summary>clsADR / GetADRbyID</summary>
        ///<remarks>Ermittelt den Datensatz einer Adresse</remarks>
        public static DataTable GetADRbyID(decimal decID, decimal decBenutzerID)
        {
            string strSql = string.Empty;
            DataTable dt = new DataTable();
            if (decID > 0)
            {
                strSql = "SELECT * FROM ADR WHERE ID='" + decID + "'";
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, decBenutzerID, "ADR");
            }
            return dt;
        }
        ///<summary>clsADR / GetADRbyID</summary>
        ///<remarks>Ermittelt den Datensatz einer Adresse</remarks>
        public bool ExistAdrByAnschrift()
        {
            bool reVal = false;
            string strSql = string.Empty;
            strSql = "SELECT ID FROM ADR WHERE " +
                                            "Name1='" + Name1 + "' " +
                                            "AND Str='" + Str + "' " +
                                            "AND HausNr='" + HausNr + "' " +
                                            "AND PLZ = '" + PLZ + "' " +
                                            "AND Ort ='" + Ort + "' ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0;
            Decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                ID = decTmp;
                Fill();
                reVal = true;
            }
            return reVal;
        }
        ///<summary>clsADR / GetADRbyID</summary>
        ///<remarks>Ermittelt den Datensatz einer Adresse</remarks>
        public static bool ExistAdrID(decimal myAdrID, decimal decBenutzerID)
        {
            string strSql = string.Empty;
            strSql = "SELECT ID FROM ADR WHERE ID='" + myAdrID + "'";
            bool reVal = clsSQLcon.ExecuteSQL_GetValueBool(strSql, decBenutzerID);
            return reVal;
        }

        ///<summary>clsADR / GetADRbyID</summary>
        ///<remarks>Ob eine Adresse bereits verwendet wird muss in folgenden Table geprüft werden:
        ///         - Auftrag
        ///         - LEingang
        ///         - LAusgang
        ///         - Rechnungen</remarks>
        public static bool IsADRUsed(decimal myAdrID, decimal decBenutzerID)
        {
            bool bIsUsed = false;
            string strSql = string.Empty;
            strSql = "SELECT ID FROM Auftrag WHERE B_ID=" + myAdrID + " OR " +
                     "E_ID=" + myAdrID + " OR " +
                     "nB_ID=" + myAdrID + " OR " +
                     "nE_ID=" + myAdrID + ";";
            bIsUsed = clsSQLcon.ExecuteSQL_GetValueBool(strSql, decBenutzerID);

            //wenn bIsUsed = false , dann muss der Check nicht weiter durchgeführt werden
            if (!bIsUsed)
            {
                //LEingang
                //-Auftraggeber
                //-Empfaenger
                //-Lieferant
                //-Versender
                //-SpedID
                strSql = string.Empty;
                strSql = "SELECT ID FROM LEingang WHERE Auftraggeber=" + myAdrID + " OR " +
                         "Empfaenger=" + myAdrID + " OR " +
                         //"Lieferant=" + myAdrID + " OR " +
                         "Versender=" + myAdrID + " OR " +
                         "SpedID=" + myAdrID + " ; ";

                bIsUsed = clsSQLcon.ExecuteSQL_GetValueBool(strSql, decBenutzerID);
            }

            //wenn bIsUsed = true , dann muss der Check nicht weiter durchgeführt werden
            if (!bIsUsed)
            {
                //LAusgang
                //-Auftraggeber
                //-Versender
                //-Empfaenger
                //-Entladestelle
                //-Lieferant
                //-SpedID
                strSql = string.Empty;
                strSql = "SELECT ID FROM LAusgang WHERE Auftraggeber=" + myAdrID + " OR " +
                         "Versender=" + myAdrID + " OR " +
                         "Empfaenger=" + myAdrID + " OR " +
                         "Entladestelle=" + myAdrID + " OR " +
                         //"Lieferant=" + myAdrID + " OR " +
                         "SpedID=" + myAdrID + " ;";

                bIsUsed = clsSQLcon.ExecuteSQL_GetValueBool(strSql, decBenutzerID);
            }
            return bIsUsed;
        }
        ///<summary>clsADR / GetADRByVerweis</summary>
        ///<remarks></remarks>>
        public void GetADRByVerweis()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT TOP(1) ID FROM ADR WHERE Verweis='" + Verweis + "';";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0;
            Decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                this.ID = decTmp;
                Fill();
            }
        }
        ///<summary>clsADR / Fill</summary>
        ///<remarks></remarks>>
        public void Fill()
        {
            FillClassOnly();

            FillAdrDictonary();
            FillClassADRTable();

            InitSubClasses();
        }
        ///<summary>clsADR / FillClassOnly</summary>
        ///<remarks></remarks>>
        public void FillClassOnly()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ADR WHERE ID=" + ID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Adressen");
            decimal decTmp = 0;
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                this.ID = (decimal)dt.Rows[i]["ID"];
                this.ViewID = dt.Rows[i]["ViewID"].ToString();
                this.KD_ID = (decimal)dt.Rows[i]["KD_ID"];
                this.FBez = dt.Rows[i]["FBez"].ToString();
                this.Name1 = dt.Rows[i]["Name1"].ToString();
                this.Name2 = dt.Rows[i]["Name2"].ToString();
                this.Name3 = dt.Rows[i]["Name3"].ToString();
                this.Str = dt.Rows[i]["Str"].ToString();
                this.HausNr = dt.Rows[i]["HausNr"].ToString();
                this.PF = dt.Rows[i]["PF"].ToString();
                this.PLZ = dt.Rows[i]["PLZ"].ToString();
                this.PLZPF = dt.Rows[i]["PLZPF"].ToString();
                this.Ort = dt.Rows[i]["Ort"].ToString();
                this.OrtPF = dt.Rows[i]["OrtPF"].ToString();
                this.Land = dt.Rows[i]["Land"].ToString();
                this.LKZ = dt.Rows[i]["LKZ"].ToString();
                this.WAvon = (DateTime)dt.Rows[i]["WAvon"];
                this.WAbis = (DateTime)dt.Rows[i]["WAbis"];
                this.Dummy = (bool)dt.Rows[i]["Dummy"];
                this.UserInfoTxt = dt.Rows[i]["UserInfoTxt"].ToString();
                this.activ = (bool)dt.Rows[i]["activ"];
                this.ASNCommunication = (bool)dt.Rows[i]["ASNCom"];
                this.Lagernummer = (decimal)dt.Rows[i]["Lagernummer"];
                this.AdrID_Be = (decimal)dt.Rows[i]["AdrID_Be"];
                this.AdrID_Ent = (decimal)dt.Rows[i]["AdrID_Ent"];
                this.AdrID_Post = (decimal)dt.Rows[i]["AdrID_Post"];
                this.AdrID_RG = (decimal)dt.Rows[i]["AdrID_RG"];
                this.IsAuftraggeber = (bool)dt.Rows[i]["IsAuftraggeber"];
                this.IsVersender = (bool)dt.Rows[i]["IsVersender"];
                this.IsBelade = (bool)dt.Rows[i]["IsBelade"];
                this.IsEmpfaenger = (bool)dt.Rows[i]["IsEmpfaenger"];
                this.IsEntlade = (bool)dt.Rows[i]["IsEntlade"];
                this.IsPost = (bool)dt.Rows[i]["IsPost"];
                this.IsRG = (bool)dt.Rows[i]["IsRG"];
                this.CalcLagerVers = (bool)dt.Rows[i]["CalcLagerVers"];
                this.DocEinlagerAnzeige = dt.Rows[i]["DocEinlagerAnzeige"].ToString();
                this.DocAuslagerAnzeige = dt.Rows[i]["DocAuslagerAnzeige"].ToString();
                this.Verweis = dt.Rows[i]["Verweis"].ToString();
                decTmp = 0;
                decimal.TryParse(dt.Rows[i]["PostRGBy"].ToString(), out decTmp);
                this.PostRGBy = decTmp;
                decTmp = 0;
                decimal.TryParse(dt.Rows[i]["PostAnlageBy"].ToString(), out decTmp);
                this.PostAnlageBy = decTmp;
                decTmp = 0;
                decimal.TryParse(dt.Rows[i]["PostLfsBy"].ToString(), out decTmp);
                this.PostLfsBy = decTmp;
                decTmp = 0;
                decimal.TryParse(dt.Rows[i]["PostListBy"].ToString(), out decTmp);
                this.PostListBy = decTmp;
                this.IsDiv = (bool)dt.Rows[i]["IsDiv"];
                this.IsSpedition = (bool)dt.Rows[i]["IsSpedition"];
                decTmp = 0;
                decimal.TryParse(dt.Rows[i]["PostAnzeigeBy"].ToString(), out decTmp);
                this.PostAnzeigeBy = decTmp;
                int iTmp = 0;
                if (dt.Columns.IndexOf("DUNSNr") != -1)
                {
                    int.TryParse(dt.Rows[i]["DUNSNr"].ToString(), out iTmp);
                }
                this.DUNSNr = iTmp;
            }
        }
        ///<summary>clsADR / Add</summary>
        ///<remarks></remarks>>
        public void Add()
        {
            //Bausstelle prüfen so oK????
            this.IsAuftraggeber = true;
            this.IsVersender = true;
            this.IsBelade = true;
            this.IsEmpfaenger = true;
            this.IsEntlade = true;
            this.IsPost = true;
            this.IsRG = true;
            this.IsDiv = true;
            this.IsSpedition = true;

            Date_Add = DateTime.Now;
            string strSQL = string.Empty;
            strSQL = "INSERT INTO ADR (ViewID, KD_ID, FBez, Name1, Name2, Name3, Str, HausNr,PF, PLZ, PLZPF, Ort, " +
                     "OrtPF, Land, WAvon, WAbis, Dummy, LKZ, UserInfoTxt, activ, Lagernummer, ASNCom, " +
                     "AdrID_Be, AdrID_Ent, AdrID_Post, AdrID_RG, IsAuftraggeber, IsVersender, IsBelade, " +
                     "IsEmpfaenger, IsEntlade, IsPost, IsRG, CalcLagerVers, DocEinlagerAnzeige, " +
                     "DocAuslagerAnzeige, Verweis, " + // PostRGBy, PostAnlageBy, PostLfsBy, PostListBy, 
                     "IsDiv, IsSpedition, DUNSNr " +
                     ") " +
                     "VALUES ('" + ViewID + "'" +
                     ", " + KD_ID +
                     ", '" + FBez + "'" +
                     ", '" + Name1 + "'" +
                     ", '" + Name2 + "'" +
                     ", '" + Name3 + "'" +
                     ", '" + Str + "'" +
                     ", '" + HausNr + "'" +
                     ", '" + PF + "'" +
                     ", '" + PLZ + "'" +
                     ", '" + PLZPF + "'" +
                     ", '" + Ort + "'" +
                     ", '" + OrtPF + "'" +
                     ", '" + Land + "'" +
                     ", '" + WAvon + "'" +
                     ", '" + WAbis + "'" +
                     ", " + Convert.ToInt32(Dummy) +
                     ", '" + LKZ + "'" +
                     ", '" + UserInfoTxt + "'" +
                     ", " + Convert.ToInt32(activ) +
                     ", " + Lagernummer +
                     ", " + Convert.ToInt32(ASNCommunication) +
                     ", " + AdrID_Be +
                     ", " + AdrID_Ent +
                     ", " + AdrID_RG +
                     ", " + AdrID_RG +
                     ", " + Convert.ToInt32(IsAuftraggeber) +
                     ", " + Convert.ToInt32(IsVersender) +
                     ", " + Convert.ToInt32(IsBelade) +
                     ", " + Convert.ToInt32(IsEmpfaenger) +
                     ", " + Convert.ToInt32(IsEntlade) +
                     ", " + Convert.ToInt32(IsPost) +
                     ", " + Convert.ToInt32(IsRG) +
                     ", " + Convert.ToInt32(CalcLagerVers) +
                     ", '" + DocEinlagerAnzeige + "'" +
                     ", '" + DocAuslagerAnzeige + "'" +
                     ", '" + Verweis + "'" +
                     //", '" + PostRGBy + "'" +  //Default 
                     //", '" + PostAnlageBy + "'" + //Default
                     //", '" + PostLfsBy + "'" +  //Default
                     //", '" + PostListBy + "'" +  //Default
                     ", " + Convert.ToInt32(IsDiv) +
                     ", " + Convert.ToInt32(IsSpedition) +
                     ", " + this.DUNSNr +
                     ");";
            strSQL = strSQL + " Select @@IDENTITY as 'ID'; ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                ID = decTmp;
                //Beim anlegen der Adresse muss die Rechnungs- und Postadresse auf den neuen
                //Datensatz gesetzt werden und dann der Datensatz upgedated werden
                this.AdrID_RG = this.ID;
                this.AdrID_Post = this.ID;
                this.Update();

                // überflüssig                Fill();
                if (Dummy)
                {
                    Name1 = "Adressdummy" + PLZ + " " + Ort;
                }
                //Add Logbucheintrag Eintrag
                string Beschreibung = "Adresse: " + ViewID + " - " + Name1 + " hinzugefügt";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), Beschreibung);
            }
        }
        ///<summary>clsADR / Update</summary>
        ///<remarks>Update Adressdaten.</remarks>
        public void Update()
        {
            string strSQL = string.Empty;
            Date_Add = DateTime.Now;
            strSQL = "Update ADR SET ViewID='" + ViewID + "'" +
                     ", FBez='" + FBez + "'" +
                     ", Name1='" + Name1 + "'" +
                     ", Name2='" + Name2 + "'" +
                     ", Name3='" + Name3 + "'" +
                     ", Str='" + Str + "'" +
                     ", HausNr='" + HausNr + "'" +
                     ", PF='" + PF + "'" +
                     ", PLZ='" + PLZ + "'" +
                     ", PLZPF='" + PLZPF + "'" +
                     ", Ort='" + Ort + "'" +
                     ", OrtPF='" + OrtPF + "'" +
                     ", Land='" + Land + "'" +
                     ", WAvon='" + WAvon + "'" +
                     ", WAbis='" + WAbis + "'" +
                     ", Dummy=" + Convert.ToInt32(Dummy) +
                     ", LKZ='" + LKZ + "'" +
                     ", UserInfoTxt='" + UserInfoTxt + "'" +
                     ", activ=" + Convert.ToInt32(activ) +
                     ", Lagernummer=" + Lagernummer +
                     ", ASNCom=" + Convert.ToInt32(ASNCommunication) +
                     ", AdrID_Be=" + AdrID_Be +
                     ", AdrID_Ent=" + AdrID_Ent +
                     ", AdrID_Post=" + AdrID_Post +
                     ", AdrID_RG =" + AdrID_RG +
                     ", IsAuftraggeber =" + Convert.ToInt32(IsAuftraggeber) +
                     ", IsVersender =" + Convert.ToInt32(IsVersender) +
                     ", IsBelade =" + Convert.ToInt32(IsBelade) +
                     ", IsEmpfaenger =" + Convert.ToInt32(IsEmpfaenger) +
                     ", IsEntlade =" + Convert.ToInt32(IsEntlade) +
                     ", IsPost =" + Convert.ToInt32(IsPost) +
                     ", IsRG =" + Convert.ToInt32(IsRG) +
                     ", CalcLagerVers= " + Convert.ToInt32(CalcLagerVers) +
                     ", DocEinlagerAnzeige='" + DocEinlagerAnzeige + "'" +
                     ", DocAuslagerAnzeige='" + DocAuslagerAnzeige + "'" +
                     ", Verweis='" + Verweis + "'" +
                     ", PostRGBy=" + PostRGBy +
                     ", PostAnlageBy=" + PostAnlageBy +
                     ", PostLfsBy=" + PostLfsBy +
                     ", PostListBy=" + PostListBy +
                     ", IsDiv=" + Convert.ToInt32(IsDiv) +
                     ", IsSpedition=" + Convert.ToInt32(IsSpedition) +
                     ", PostAnzeigeBy=" + PostAnzeigeBy +
                     ", DUNSNr =" + this.DUNSNr +
                     " WHERE ID=" + ID + ";";

            clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
            //Add Logbucheintrag update
            ViewID = GetMatchCodeByID(ID, BenutzerID);
            string Beschreibung = "Adresse: " + ViewID + "  ID:" + ID + " geändert";
            Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
            this.Fill();
        }
        ///<summary>clsADR / UpdateAdrCategory</summary>
        ///<remarks></remarks>>
        public void UpdateAdrCategory(Dictionary<int, bool> myDict)
        {
            //erst alle Flags auf false setzen
            this.IsAuftraggeber = false;
            this.IsVersender = false;
            this.IsBelade = false;
            this.IsEmpfaenger = false;
            this.IsEntlade = false;
            this.IsPost = false;
            this.IsRG = false;
            this.IsDiv = false;
            this.IsSpedition = false;

            foreach (KeyValuePair<int, bool> tmpAdrCat in myDict)
            {
                Int32 iTmp = tmpAdrCat.Key;
                bool bTmp = tmpAdrCat.Value;
                switch (iTmp)
                {
                    case 0:
                        //Auftraggeber
                        this.IsAuftraggeber = bTmp;
                        break;
                    case 1:
                        //Versender
                        this.IsVersender = bTmp;
                        break;
                    case 2:
                        //Belade
                        this.IsBelade = bTmp;
                        break;
                    case 3:
                        //Empfaenger
                        this.IsEmpfaenger = bTmp;
                        break;
                    case 4:
                        //Entladesadresse
                        this.IsEntlade = bTmp;
                        break;
                    case 5:
                        //Post
                        this.IsPost = bTmp;
                        break;
                    case 6:
                        //Rechnung
                        this.IsRG = bTmp;
                        break;
                    case 7:
                        // Diverse
                        this.IsDiv = bTmp;
                        break;
                    case 8:
                        // Spedition
                        this.IsSpedition = bTmp;
                        break;
                }
            }

            string strSql = string.Empty;
            strSql = "Update ADR SET " +
                     " IsAuftraggeber =" + Convert.ToInt32(IsAuftraggeber) +
                     ", IsVersender =" + Convert.ToInt32(IsVersender) +
                     ", IsBelade =" + Convert.ToInt32(IsBelade) +
                     ", IsEmpfaenger =" + Convert.ToInt32(IsEmpfaenger) +
                     ", IsEntlade =" + Convert.ToInt32(IsEntlade) +
                     ", IsPost =" + Convert.ToInt32(IsPost) +
                     ", IsRG =" + Convert.ToInt32(IsRG) +
                     ", IsDiv =" + Convert.ToInt32(IsDiv) +
                     ", IsSpedition=" + Convert.ToInt32(IsSpedition) +
                     " WHERE ID=" + ID + ";";

            bool bOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            if (bOK)
            {
                this.Fill();
            }
        }
        ///<summary>clsADR / FillAdrDictonary</summary>
        ///<remarks></remarks>>
        private void FillAdrDictonary()
        {
            DictAdrCatagorySelect = new Dictionary<int, string>();
            DictAdrCatagoryUnSelect = new Dictionary<int, string>();

            foreach (KeyValuePair<int, string> AdrCat in DictAdrCatagory)
            {
                Int32 iTmp = AdrCat.Key;
                string strTmp = AdrCat.Value.ToString();
                switch (iTmp)
                {
                    case 0:
                        //Auftraggeber
                        if (this.IsAuftraggeber)
                        {
                            DictAdrCatagorySelect.Add(iTmp, strTmp);
                        }
                        else
                        {
                            DictAdrCatagoryUnSelect.Add(iTmp, strTmp);
                        }
                        break;
                    case 1:
                        //Versender
                        if (this.IsVersender)
                        {
                            DictAdrCatagorySelect.Add(iTmp, strTmp);
                        }
                        else
                        {
                            DictAdrCatagoryUnSelect.Add(iTmp, strTmp);
                        }
                        break;
                    case 2:
                        //Belade
                        if (this.IsBelade)
                        {
                            DictAdrCatagorySelect.Add(iTmp, strTmp);
                        }
                        else
                        {
                            DictAdrCatagoryUnSelect.Add(iTmp, strTmp);
                        }
                        break;
                    case 3:
                        //Empfaenger
                        if (this.IsEmpfaenger)
                        {
                            DictAdrCatagorySelect.Add(iTmp, strTmp);
                        }
                        else
                        {
                            DictAdrCatagoryUnSelect.Add(iTmp, strTmp);
                        }
                        break;
                    case 4:
                        //Entladesadresse
                        if (this.IsEntlade)
                        {
                            DictAdrCatagorySelect.Add(iTmp, strTmp);
                        }
                        else
                        {
                            DictAdrCatagoryUnSelect.Add(iTmp, strTmp);
                        }
                        break;
                    case 5:
                        //Post
                        if (this.IsPost)
                        {
                            DictAdrCatagorySelect.Add(iTmp, strTmp);
                        }
                        else
                        {
                            DictAdrCatagoryUnSelect.Add(iTmp, strTmp);
                        }
                        break;
                    case 6:
                        //Rechnung
                        if (this.IsRG)
                        {
                            DictAdrCatagorySelect.Add(iTmp, strTmp);
                        }
                        else
                        {
                            DictAdrCatagoryUnSelect.Add(iTmp, strTmp);
                        }
                        break;
                    case 7:
                        //Diverse
                        if (this.IsDiv)
                        {
                            DictAdrCatagorySelect.Add(iTmp, strTmp);
                        }
                        else
                        {
                            DictAdrCatagoryUnSelect.Add(iTmp, strTmp);
                        }
                        break;
                    case 8:
                        //Spedition
                        if (this.IsSpedition)
                        {
                            DictAdrCatagorySelect.Add(iTmp, strTmp);
                        }
                        else
                        {
                            DictAdrCatagoryUnSelect.Add(iTmp, strTmp);
                        }
                        break;
                }
            }
        }
        ///<summary>clsADR / GetADR_ID</summary>
        ///<remarks></remarks>>
        public decimal GetADR_ID()
        {
            decimal adrID = 0;
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = "Select ID FROM ADR WHERE ViewID='" + ViewID + "' AND KD_ID='" + KD_ID + "'";
            Globals.SQLcon.Open();
            object obj = Command.ExecuteScalar();
            if ((obj == null) | (obj is DBNull))
            {
                adrID = 0.0m;
            }
            else
            {
                adrID = (decimal)obj;
            }
            Command.Dispose();
            Globals.SQLcon.Close();

            return adrID;
        }
        ///<summary>clsADR / GetADRList</summary>
        ///<remarks></remarks>>
        public static DataTable GetADRList(decimal myBenutzer)
        {
            string strSQL = string.Empty;
            DataTable dataTable = new DataTable();
            strSQL = "SELECT " +
                     "ID, " +
                     "ViewID as 'Suchbegriff', " +
                     "KD_ID as 'KD-Nr', " +
                     "FBez as 'Bezeichnung', " +
                     "Name1, " +
                     "Name2, " +
                     "Name3, " +
                     "Str as 'Strasse', " +
                     "HausNr, " +
                     "PF as 'Postfach', " +
                     "PLZ, " +
                     "PLZPF, " +
                     "Ort, " +
                     "OrtPF, " +
                     "Land " +
                     "FROM ADR ORDER BY ViewID";

            dataTable = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myBenutzer, "Adressen");
            return dataTable;
        }
        ///<summary>clsADR / GetADRListForAdrListCtr</summary>
        ///<remarks> Adressbereich:
        ///          1 = Adressliste komplett
        ///          2 = Adressliste aktiv
        ///          3 = Adressliste passiv 
        ///          4 = Adressliste aktive Auftraggeber
        ///          5 = Adressliste aktive Lieferanten / Versender 
        ///          6 = Adressliste aktive Empfänger
        ///          7 = Adressliste aktive Entladestellen
        ///          8 = Adressliste aktive Speditionen</remarks>>
        public static DataTable GetADRListForAdrListCtr(decimal myBenutzer, Int32 iMyAdrRange)
        {
            string strSQL = string.Empty;
            DataTable dataTable = new DataTable();
            strSQL = "SELECT " +
                     "ID, " +
                     "ViewID as 'Suchbegriff', " +
                     //"FBez as 'Bezeichnung', " +
                     "Name1, " +
                     "Name2, " +
                     "Name3, " +
                     "Str as 'Strasse', " +
                     "HausNr, " +
                     "PF as 'Postfach', " +
                     "PLZ, " +
                     "PLZPF, " +
                     "Ort, " +
                     "OrtPF, " +
                     "Land, " +
                     "KD_ID as 'KD-Nr', " +
                     "DUNSNr " +
                     "FROM ADR ";

            switch (iMyAdrRange)
            {
                case const_AdrRange_AdrListAktiv:
                    strSQL = strSQL + " WHERE activ=1 ";
                    break;
                case const_AdrRange_AdrListePassiv:
                    strSQL = strSQL + " WHERE activ=0 ";
                    break;
                case const_AdrRange_AdrListeKunde:
                    strSQL = strSQL + " WHERE IsAuftraggeber=1 AND activ=1 ";
                    break;
                case const_AdrRange_AdrListeVersender:
                    strSQL = strSQL + " WHERE IsVersender=1 AND activ=1 ";
                    break;
                case const_AdrRange_AdrListeEmpfaenger:
                    strSQL = strSQL + " WHERE IsEmpfaenger=1 AND activ=1 ";
                    break;
                case const_AdrRange_AdrListeEntlade:
                    strSQL = strSQL + " WHERE IsEntlade=1 AND activ=1 ";
                    break;
                case const_AdrRange_AdrListeSpedition:
                    strSQL = strSQL + " WHERE IsSpedition=1 AND activ=1 ";
                    break;
                case const_AdrRange_AdrListePost:
                    strSQL = strSQL + " WHERE IsPost=1 AND activ=1 ";
                    break;
                case const_AdrRange_AdrListeRechnung:
                    strSQL = strSQL + " WHERE IsRG=1 AND activ=1 ";
                    break;
                case const_AdrRange_AdrListeBelade:
                    strSQL = strSQL + " WHERE IsBelade=1 AND activ=1 ";
                    break;
                case const_AdrRange_AdrListeDiverse:
                    strSQL = strSQL + " WHERE IsDiv=1 AND activ=1 ";
                    break;
                default:
                    //alle Adressdatensätze
                    //strSQL = strSQL + " WHERE activ=1 ";
                    break;
            }

            strSQL = strSQL + " ORDER BY ViewID";
            dataTable = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myBenutzer, "Adressen");
            return dataTable;
        }
        ///<summary>clsADR / UpdateColumn</summary>
        ///<remarks></remarks>>
        public void UpdateColumn(string ID, string Column, string Update)
        {
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand UpCommand = new SqlCommand();
                UpCommand.Connection = Globals.SQLcon.Connection;

                switch (Column)
                {
                    case "Suchbegriff":
                        UpCommand.CommandText = "Update ADR SET ViewID='" + Update + "' WHERE ID='" + ID + "'";
                        break;
                    case "Strasse":
                        UpCommand.CommandText = "Update ADR SET Str='" + Update + "' WHERE ID='" + ID + "'";
                        break;
                    case "Postfach":
                        UpCommand.CommandText = "Update ADR SET PF='" + Update + "' WHERE ID='" + ID + "'";
                        break;
                    default:
                        UpCommand.CommandText = "Update ADR SET " + Column + "='" + Update + "' WHERE ID='" + ID + "'";
                        break;
                }

                Globals.SQLcon.Open();
                UpCommand.ExecuteNonQuery();
                UpCommand.Dispose();
                Globals.SQLcon.Close();
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
                //Add Logbucheintrag Exception
                string Beschreibung = "Exception: " + ex;
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Exception.ToString(), Beschreibung);
            }
            finally
            {
                //Add Logbucheintrag update
                ViewID = GetMatchCodeByID(Convert.ToDecimal(ID), BenutzerID);
                string Beschreibung = "Adresse: " + ViewID + "  ID:" + ID + " geändert";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
            }
        }

        //
        //---------------------- Update ADR mit KDNr ------------------------
        //
        public static void updateADRforKD(decimal KDNr, decimal ADR_ID, decimal BenutzerID)
        {
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand UpCommand = new SqlCommand();
                UpCommand.Connection = Globals.SQLcon.Connection;

                UpCommand.CommandText = "Update ADR SET KD_ID='" + KDNr + "' WHERE ID='" + ADR_ID + "'";

                Globals.SQLcon.Open();
                UpCommand.ExecuteNonQuery();
                UpCommand.Dispose();
                Globals.SQLcon.Close();
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
                //Add Logbucheintrag Exception
                string Beschreibung = "Exception: " + ex;
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Exception.ToString(), Beschreibung);
            }
            finally
            {
                //Add Logbucheintrag update
                string ViewID = GetMatchCodeByID(ADR_ID, BenutzerID);
                string Beschreibung = "Adresse: " + ViewID + "  ID:" + ADR_ID + " Kundennummer:" + KDNr + " geändert";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
            }
        }

        //
        //---------- DataTabel Versender/Beladestelle und Empfänger/Entladestelle  ------
        //
        public static DataTable ADRTable()
        {
            DataTable adrTable = new DataTable();
            adrTable.Clear();
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT " +
                                  "ID, ViewID as 'Suchbegriff', KD_ID, Name1, PLZ, Ort " +
                                  "FROM ADR";

            ada.Fill(adrTable);
            Command.Dispose();
            Globals.SQLcon.Close();
            return adrTable;
        }

        //
        //
        //
        public static DataSet ReadADRbyID(decimal ID)
        {
            DataSet ds = new DataSet();
            ds.Clear();

            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();

            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT * FROM ADR WHERE ID=" + (Int32)ID;

            ada.Fill(ds);
            ada.Dispose();
            Command.Dispose();
            Globals.SQLcon.Close();

            return ds;
        }

        //
        //------------------ Get Suchname by ADR ID  ------------------
        //
        public static string ReadViewIDbyID(decimal adrID)
        {
            string ReturnValue = "";
            if (adrID > 1)
            {
                SqlDataAdapter ada = new SqlDataAdapter();
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                ada.SelectCommand = Command;
                Command.CommandText = "SELECT ViewID FROM ADR WHERE ID=" + (Int32)adrID;
                Globals.SQLcon.Open();
                ReturnValue = Command.ExecuteScalar().ToString();
                Command.Dispose();
                Globals.SQLcon.Close();
                return ReturnValue;
            }
            else
            {
                return ReturnValue;
            }
        }

        //
        //--------- String einer Adresse ---------------
        //
        public static String GetADRString(decimal ID)
        {
            DataSet ds = ReadADRbyID(ID);
            string strADR = string.Empty;

            for (Int32 i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                //strADR = strADR + ds.Tables[0].Rows[i]["KD_ID"].ToString() + " - ";
                strADR = strADR + ds.Tables[0].Rows[i]["Name1"].ToString().Trim() + " - ";
                strADR = strADR + ds.Tables[0].Rows[i]["PLZ"].ToString().Trim() + " - ";
                strADR = strADR + ds.Tables[0].Rows[i]["Ort"].ToString().Trim();
            }
            return strADR;
        }

        //
        //--------- String einer Adresse mehrzeilig ---------------
        //
        public static String GetADRStringMZ(decimal ID)
        {
            DataSet ds = ReadADRbyID(ID);
            string strADR = string.Empty;

            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                //strADR = strADR + ds.Tables[0].Rows[i]["KD_ID"].ToString() + " - ";
                strADR = strADR + ds.Tables[0].Rows[i]["Name1"].ToString().Trim() + Environment.NewLine;
                strADR = strADR + ds.Tables[0].Rows[i]["PLZ"].ToString().Trim() + " ";
                strADR = strADR + ds.Tables[0].Rows[i]["Ort"].ToString().Trim();
            }
            return strADR;
        }
        /// <summary>
        /// clsADR / GetADRStringKDNrName
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static String GetADRStringKDNrName(decimal ID)
        {
            DataSet ds = ReadADRbyID(ID);
            string strADR = string.Empty;

            for (Int32 i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                strADR = strADR + ds.Tables[0].Rows[i]["KD_ID"].ToString() + " - ";
                strADR = strADR + ds.Tables[0].Rows[i]["Name1"].ToString().Trim() + " - ";
            }
            return strADR;
        }

        //
        //----------- Check Verwendung ------------------- 
        //

        ///<summary>clsArbeitsbereiche / DeleteADR</summary>
        ///<remarks>Löscht eine Adresse aus der Datenbank.
        ///         Folgende Tabellen müssen direkt mit bereinigt werden:
        ///         -Tarife
        ///         - Kunde
        ///         - KOntakte</remarks>
        public void DeleteADR()
        {
            //Löschen aller Tarife
            clsTarif delTarif = new clsTarif();
            delTarif._GL_User = _GL_User;
            delTarif.AdrID = ID;
            delTarif.DelteTarifbyAdrID();

            string strSql = string.Empty;
            strSql = "DELETE FROM ADR WHERE ID='" + ID + "'";
            bool bDeleteOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);

            if (bDeleteOK)
            {
                //Löschen der Kundendaten wenn vorhanden
                clsKunde kd = new clsKunde();
                kd.BenutzerID = BenutzerID;
                kd.ADR_ID = ID;
                kd.DeleteKunde();

                //Löschen der Kontaktdaten wenn vorhanden
                clsKontakte kt = new clsKontakte();
                kt.BenutzerID = BenutzerID;
                kt.ADR_ID = ID;
                kt.DeleteKontakte();

                //Add Logbucheintrag Löschen
                string ViewID = GetMatchCodeByID(ID, BenutzerID);
                string Beschreibung = "Adressen: " + ViewID + " ID: " + ID + " gelöscht";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), Beschreibung);
            }
        }

        //
        //----------- prüft Matchcode - darf nicht doppelt sein -------------
        //
        public static bool ViewIDExists(Globals._GL_USER myGLUser, string ViewIDADR)
        {
            string strSQL = string.Empty;
            strSQL = "SELECT ID FROM ADR WHERE ViewID='" + ViewIDADR + "'";
            return clsSQLcon.ExecuteSQL_GetValueBool(strSQL, myGLUser.User_ID);
        }

        //
        //--------------------- Kundenummer lt. ADR-ID ------------------------
        //
        public static decimal GetKD_IDByID(decimal _ADR_ID)
        {
            decimal KD_ID = 0;
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = "Select KD_ID FROM ADR WHERE ID='" + _ADR_ID + "'";
            Globals.SQLcon.Open();
            object obj = Command.ExecuteScalar();
            if ((obj == null) | (obj is DBNull))
            {
                KD_ID = 0.0m;
            }
            else
            {
                KD_ID = (decimal)obj;
            }
            Command.Dispose();
            Globals.SQLcon.Close();

            return KD_ID;
        }

        //
        //--------------------- Kundenummer lt. ADR-ID ------------------------
        //
        public static decimal GetKD_IDByMatchcode(string mc)
        {
            decimal KD_ID = 0;
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = "Select KD_ID FROM ADR WHERE ViewID='" + mc + "'";
            Globals.SQLcon.Open();
            object obj = Command.ExecuteScalar();
            if ((obj == null) | (obj is DBNull))
            {
                KD_ID = 0.0m;
            }
            else
            {
                KD_ID = (decimal)obj;
            }
            Command.Dispose();
            Globals.SQLcon.Close();

            return KD_ID;
        }

        //
        //--------------------- ADR ID BY MC  ------------------------
        //
        public static decimal GetIDByMatchcode(string mc)
        {
            decimal ADR_ID = 0;
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = "Select ID FROM ADR WHERE ViewID='" + mc + "'";
            Globals.SQLcon.Open();
            object obj = Command.ExecuteScalar();
            if ((obj == null) | (obj is DBNull))
            {
                ADR_ID = 0.0m;
            }
            else
            {
                ADR_ID = (decimal)obj;
            }
            Command.Dispose();
            Globals.SQLcon.Close();

            return ADR_ID;
        }

        //
        //--------------------- Kundenummer lt. ADR-ID ------------------------
        //
        public static bool IsADRKunde(decimal _ADR_ID)
        {
            bool IsKd = false;

            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = "Select ViewID FROM ADR WHERE ID='" + _ADR_ID + "'";
            Globals.SQLcon.Open();
            object obj = Command.ExecuteScalar();
            if ((obj == null) | (obj is DBNull))
            {
                IsKd = false;
            }
            else
            {
                IsKd = true;
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            return IsKd;
        }

        //
        //--------------------- Ort einer Adresse ------------------------
        //--- bsp. FVGS von nach ------------
        public static string GetOrtByID(Globals._GL_USER myGLUser, decimal myADRId)
        {
            string strSQL = string.Empty;
            strSQL = "Select Ort FROM ADR WHERE ID=" + myADRId + ";";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, myGLUser.User_ID);
            return strTmp;
        }

        //
        public static string GetLandByID(Globals._GL_USER myGLUser, decimal myADRId)
        {
            string strSQL = string.Empty;
            strSQL = "Select Land FROM ADR WHERE ID=" + myADRId + ";";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, myGLUser.User_ID);
            return strTmp;
        }

        //
        //
        //
        public static string GetPLZByID(Globals._GL_USER myGLUser, decimal myADRId)
        {
            string strSQL = string.Empty;
            strSQL = "Select PLZ FROM ADR WHERE ID=" + myADRId + ";";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, myGLUser.User_ID);
            return strTmp;
        }

        //
        //--------------------- Ort einer Adresse ------------------------
        //--- bsp. FVGS von nach ------------
        public static bool CheckMatchcodeIsUsed(string Matchcode)
        {
            bool mc = false;
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = "Select ID FROM ADR WHERE ViewID='" + Matchcode + "'";
            Globals.SQLcon.Open();
            object obj = Command.ExecuteScalar();
            if ((obj == null) | (obj is DBNull))
            {
                mc = false;
            }
            else
            {
                mc = true;
            }
            Command.Dispose();
            Globals.SQLcon.Close();

            return mc;
        }

        //
        //
        public static DataSet GetSUForCBSUAuswahl(DateTime Date_von, DateTime Date_bis, Int32 Status)
        {
            DataSet ds = new DataSet();
            string strSQL = string.Empty;
            try
            {
                SqlDataAdapter ada = new SqlDataAdapter();
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                ada.SelectCommand = Command;

                strSQL = "SELECT DISTINCT " +
                         "(Select Frachtvergabe.SU FROM Frachtvergabe WHERE Frachtvergabe.ID_AP=AuftragPos.ID) as 'SU_ID', " +
                         "(Select Name1 FROM ADR WHERE ID=(Select Frachtvergabe.SU FROM Frachtvergabe WHERE Frachtvergabe.ID_AP=AuftragPos.ID)) as 'SU' " +
                         "FROM AuftragPos " +
                         "INNER JOIN Auftrag ON Auftrag.ANr=AuftragPos.Auftrag_ID " +
                         "WHERE (AuftragPos.Status>'3' AND AuftragPos.Status<'7') AND " +
                         "((AuftragPos.T_Date>'" + Date_von.AddDays(-1).ToShortDateString() + "' AND AuftragPos.T_Date<'" + Date_bis.AddDays(1).ToShortDateString() + "') " +
                         "OR (AuftragPos.T_Date='" + DateTime.MaxValue + "')) AND " +
                         "AuftragPos.ID IN (SELECT ID_AP FROM Frachtvergabe) ";

                Command.CommandText = strSQL;
                ada.Fill(ds);

                ada.Dispose();
                Command.Dispose();
                Globals.SQLcon.Close();

                if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                {
                    Command.Connection.Close();
                }
                //return ds;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
            return ds;
        }

        ///<summary>clsADR / SetKundenDaten</summary>
        ///<remarks></remarks>
        public void SetKundenDaten()
        {
            Kunde = new clsKunde();
            Kunde._GL_User = this._GL_User;
            Kunde.ADR_ID = this.ID;
            Kunde.KD_ID = this.KD_ID;
            Kunde.SetDefValueToKDDaten();
            // Version CF
            //Kunde.Debitor = clsDebitorDefaultNo.GetDebitorDefaultNoByName(Name1, this._GL_User.User_ID);
            // alternative auf Basis von MR
            Int32 defDebitor = 0;
            if (Kunde.dictDebitorDefaultNo.TryGetValue(Name1.Substring(0, 1), out defDebitor))
            {
                Kunde.Debitor = defDebitor;
            }
            Kunde.SalesTaxKeyDebitor = 89;
            Kunde.SalesTaxKeyKreditor = 49;

            Kunde.Add();
        }
    }
}