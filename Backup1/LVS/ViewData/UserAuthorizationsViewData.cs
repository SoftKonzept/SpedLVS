using Common.Models;
using System;
using System.Data;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class UserAuthorizationsViewData
    {
        private int UserId { get; set; }
        public UserAuthorizations UserAuthorization { get; set; }

        public DataTable dtBerechtigungen = new DataTable("Berechtigungslist");

        //--- zu ändern
        //public clsUserBerechtigungen Berechtigung = new clsUserBerechtigungen();
        public clsArbeitsbereichUser ArbeitsbereichAccess = new clsArbeitsbereichUser();


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

        public string[,] UserAuthArray = {
                            {"Stammdaten - Adressendaten lesen", "read_ADR"},
                            {"Stammdaten - Adressendaten anlegen/ändern", "write_ADR"},

                            {"Stammdaten - Kundendaten lesen", "read_Kunde" },
                            {"Stammdaten - Kundendaten anlegen ändern", "write_Kunde" },

                            {"Stammdaten - Personaldaten lesen", "read_Personal" },
                            {"Stammdaten - Personaldaten anlegen/ändernn", "write_Personal" },

                            {"Stammdaten - Fahrzeugdaten lesen", "read_KFZ" },
                            {"Stammdaten - Fahrzeugdaten anlegen/ändern", "write_KFZ" },

                            {"Stammdaten - Güterartdaten lesen", "read_Gut" },
                            {"Stammdaten - Güterartdaten anlegen/ändern", "write_Gut" },

                            {"Stammdaten - Relationdaten lesen", "read_Relation" },
                            {"Stammdaten - Relationdaten anlegen/ändern", "write_Relation" },

                            {"Stammdaten - Einheiten lesen", "read_Einheit" },
                            {"Stammdaten - Einheiten anlegen/ändern", "write_Einheit" },

                            {"Stammdaten - Schäden lesen", "read_Schaden" },
                            {"Stammdaten - Schäden anlegen/ändern", "write_Schaden" },

                            {"Stammdaten - Lagerort lesen", "read_LagerOrt" },
                            {"Stammdaten - Lagerort anlegen/ändern", "write_LagerOrt" },

                            {"Stammdaten - Sonderkosten lesen", "read_FaktExtraCharge" },
                            {"Stammdaten - Sonderkosten anlegen/ändern", "write_FaktExtraCharge" },

                            {"Auftragserfassung - Auftragsdaten lesen", "read_Order" },
                            {"Auftragserfassung - Auftragdaten anlegen/ändern", "write_Order" },

                            {"Disposition - Frachtvergabe an SU", "write_TransportOrder" },
                            {"Disposition - Frachtvergabe lesen", "read_TransportOrder" },

                            {"Disposition - disponieren", "write_Disposition" },
                            {"Disposition - lesen", "read_Disposition" },

                            {"Fakturierung Lager - lesen", "read_FaktLager" },
                            {"Fakturierung Lager - erstellen/ändern", "write_FaktLager" },

                            {"Fakturierung Spedition - lesen", "read_FaktSpedition" },
                            {"Fakturierung Spedition - erstellen/ändern", "write_FaktSpedition" },

                            {"Lager - Bestand lesen", "read_Bestand" },

                            {"Lager - Einlagerung lesen", "read_LagerEingang" },
                            {"Lager - Einlagerung erstellen/ändern", "write_LagerEingang" },

                            {"Lager - Auslagerung lesen", "read_LagerAusgang" },
                            {"Lager - Auslagerung erstellen/ändern", "write_LagerAusgang" },

                            {"Lager - Storno-/Korrekturverfahren freigeben", "access_StKV" },

                            {"Lager - ASN/DFÜ - Meldungen lesen", "read_ASNTransfer" },
                            {"Lager - ASN/DFÜ - Meldungen anlegen/ändern", "write_ASNTransfer" },

                            {"Lager - Inventur - Liste für Scanner anlegen", "write_Inventory" },
                            {"Lager - Inventur - Liste für Scanner lesen", "read_Inventory" },

                            {"System - Userdaten lesen", "read_User" },
                            {"System - Userdaten anlegen/ändern", "write_User" },

                            {"System - Arbeitsbereichsdaten lesen", "read_Arbeitsbereich" },
                            {"System - Arbeitsbereichsdaten anlegen/ändern", "write_Arbeitsbereich" },

                            {"System - Mandantendaten lesen", "read_Mandant" },
                            {"System - Mandantendaten anlegen/ändern", "write_Mandant" },

                            {"Statistik - Statistikdaten lesen", "read_Statistik" },

                            {"App - Zugriff ", "access_App" },
                            {"App - Zugriff Einlagerung ", "access_AppStoreIn" },
                            {"App - Zugriff Auslagerung ", "access_AppStoreOut" },
                            {"App - Zugriff Inventur ", "access_AppInventory" },
                         };



        /// ----------------------------------------------------------------------------------------------
        ///                             Functions / Procedures
        ///  ----------------------------------------------------------------------------------------------

        public UserAuthorizationsViewData(Globals._GL_USER myGLUser)
        {
            this._GL_User = myGLUser;
            InitCls();
        }

        public UserAuthorizationsViewData(Globals._GL_USER myGLUser, int myUserId)
        {
            this._GL_User = myGLUser;
            InitCls();
            UserId = myUserId;
            if (UserId > 0)
            {
                //Fill();
                FillByUserID();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitCls()
        {
            UserAuthorization = new UserAuthorizations();

            this.UserAuthorization.Id = 0;

            this.UserAuthorization.read_ADR = false;
            this.UserAuthorization.write_ADR = false;
            this.UserAuthorization.read_Kunde = false;
            this.UserAuthorization.write_Kunde = false;
            this.UserAuthorization.read_Personal = false;
            this.UserAuthorization.write_Personal = false;
            this.UserAuthorization.read_KFZ = false;
            this.UserAuthorization.write_KFZ = false;
            this.UserAuthorization.read_Gut = false;
            this.UserAuthorization.write_Gut = false;
            this.UserAuthorization.read_Relation = false;
            this.UserAuthorization.write_Relation = false;
            this.UserAuthorization.read_Order = false;
            this.UserAuthorization.write_Order = false;
            this.UserAuthorization.write_TransportOrder = false;
            this.UserAuthorization.read_TransportOrder = false;
            this.UserAuthorization.write_Disposition = false;
            this.UserAuthorization.read_Disposition = false;
            this.UserAuthorization.read_FaktLager = false;
            this.UserAuthorization.write_FaktLager = false;
            this.UserAuthorization.read_FaktSpedition = false;
            this.UserAuthorization.write_FaktSpedition = false;
            this.UserAuthorization.read_Bestand = false;
            this.UserAuthorization.read_LagerEingang = false;
            this.UserAuthorization.write_LagerEingang = false;
            this.UserAuthorization.read_LagerAusgang = false;
            this.UserAuthorization.write_LagerAusgang = false;
            this.UserAuthorization.read_User = false;
            this.UserAuthorization.write_User = false;
            this.UserAuthorization.read_Arbeitsbereich = false;
            this.UserAuthorization.write_Arbeitsbereich = false;
            this.UserAuthorization.read_Mandant = false;
            this.UserAuthorization.write_Mandant = false;
            this.UserAuthorization.read_Statistik = false;
            this.UserAuthorization.read_Einheit = false;
            this.UserAuthorization.write_Einheit = false;
            this.UserAuthorization.read_Schaden = false;
            this.UserAuthorization.write_Schaden = false;
            this.UserAuthorization.read_LagerOrt = false;
            this.UserAuthorization.write_LagerOrt = false;
            this.UserAuthorization.read_ASNTransfer = false;
            this.UserAuthorization.write_ASNTransfer = false;
            this.UserAuthorization.read_FaktExtraCharge = false;
            this.UserAuthorization.write_FaktExtraCharge = false;
            this.UserAuthorization.access_StKV = false;
            this.UserAuthorization.access_App = false;
            this.UserAuthorization.access_AppStoreIn = false;
            this.UserAuthorization.access_AppStoreOut = false;
            this.UserAuthorization.access_AppInventory = false;
            this.UserAuthorization.read_Inventory = false;
            this.UserAuthorization.write_Inventory = false;

            InitDataTableUserAuth();

            ////this.Inventory.InventoryArticle = new InventoryArticles();
            //this.ListArticleAddToInventory = new List<int>();

            //InventoryArticleVM = new InvnetoryArticleViewModel();
            //GetInventoryList();
        }

        ///<summary>clsUserBerechtigungen / tbWerkBezeichnung_Validated</summary>
        ///<remarks>Datenbank Spalte nvarchar(50), Pflichtfeld</remarks>
        public void InitDataTableUserAuth()
        {
            dtBerechtigungen.Clear();

            if (dtBerechtigungen.Columns["Berechtigung"] == null)
            {
                DataColumn column1 = new DataColumn();
                column1.DataType = System.Type.GetType("System.String");
                column1.AllowDBNull = false;
                column1.Caption = "Berechtigung";
                column1.ColumnName = "Berechtigung";
                column1.DefaultValue = string.Empty;

                dtBerechtigungen.Columns.Add(column1);
            }

            //DataColumn column2 = new DataColumn();
            if (dtBerechtigungen.Columns["Freigabe"] == null)
            {
                DataColumn column2 = new DataColumn();
                column2.DataType = System.Type.GetType("System.Boolean");
                column2.AllowDBNull = false;
                column2.Caption = "Freigabe";
                column2.ColumnName = "Freigabe";
                column2.DefaultValue = false;

                dtBerechtigungen.Columns.Add(column2);
            }
            if (dtBerechtigungen.Columns["dbCol"] == null)
            {
                DataColumn column3 = new DataColumn();
                column3.DataType = System.Type.GetType("System.String");
                column3.Caption = "dbCol";
                column3.ColumnName = "dbCol";

                dtBerechtigungen.Columns.Add(column3);
            }
            InitRows();
        }
        ///<summary>clsUserBerechtigungen / InitRows</summary>
        ///<remarks></remarks>
        private void InitRows()
        {
            Int32 Count = UserAuthArray.Length / 2;
            for (Int32 i = 0; i <= Count - 1; i++)
            {
                string strCol = UserAuthArray[i, 1].ToString();
                if (this.UserAuthorization.Id > 0)
                {
                    strCol = UserAuthArray[i, 1].ToString();
                }
                DataRow row = dtBerechtigungen.NewRow();
                row["Berechtigung"] = UserAuthArray[i, 0].ToString();
                if (this.UserAuthorization.Id > 0)
                {
                    row["Freigabe"] = GetUserAuthByDBCol(strCol, false);
                }
                else
                {
                    row["Freigabe"] = GetUserAuthByDBCol(string.Empty, false);
                }
                row["dbCol"] = strCol;
                dtBerechtigungen.Rows.Add(row);
            }
        }
        ///<summary>clsUserBerechtigungen / SetUserAuthToTable</summary>
        ///<remarks>Diese Funktion wird für das Füllen und auslesen der Eigenschaften verewendet. 
        ///         - bSetVal (true = Klasse füllen / false = auslesen</remarks>
        private bool GetUserAuthByDBCol(string myDBCol, bool bSetVal)
        {
            bool reVal = false;
            bool bAuth = false;
            Int32 iRowCount = 1;

            if (bSetVal)
            {
                iRowCount = dtBerechtigungen.Rows.Count - 1;
            }

            for (Int32 i = 0; i <= iRowCount; i++)
            {
                string strCol = string.Empty;
                if (bSetVal)
                {
                    strCol = dtBerechtigungen.Rows[i]["dbCol"].ToString();
                    bAuth = (bool)dtBerechtigungen.Rows[i]["Freigabe"];
                }
                else
                {
                    strCol = myDBCol;
                }

                switch (strCol)
                {
                    case "read_ADR":
                        if (!bSetVal) { reVal = this.UserAuthorization.read_ADR; }
                        else { this.UserAuthorization.read_ADR = bAuth; }
                        break;

                    case "write_ADR":
                        if (!bSetVal) { reVal = this.UserAuthorization.write_ADR; }
                        else { this.UserAuthorization.write_ADR = bAuth; }
                        break;

                    case "read_Kunde":
                        if (!bSetVal) { reVal = this.UserAuthorization.read_Kunde; }
                        else { this.UserAuthorization.read_Kunde = bAuth; }
                        break;

                    case "write_Kunde":
                        if (!bSetVal) { reVal = this.UserAuthorization.write_Kunde; }
                        else { this.UserAuthorization.write_Kunde = bAuth; }
                        break;

                    case "read_Personal":
                        if (!bSetVal) { reVal = this.UserAuthorization.read_Personal; }
                        else { this.UserAuthorization.read_Personal = bAuth; }
                        break;

                    case "write_Personal":
                        if (!bSetVal) { reVal = this.UserAuthorization.write_Personal; }
                        else { this.UserAuthorization.write_Personal = bAuth; }
                        break;

                    case "read_KFZ":
                        if (!bSetVal) { reVal = this.UserAuthorization.read_KFZ; }
                        else { this.UserAuthorization.read_KFZ = bAuth; }
                        break;
                    case "write_KFZ":
                        if (!bSetVal) { reVal = this.UserAuthorization.write_KFZ; }
                        else { this.UserAuthorization.write_KFZ = bAuth; }
                        break;
                    case "read_Gut":
                        if (!bSetVal) { reVal = this.UserAuthorization.read_Gut; }
                        else { this.UserAuthorization.read_Gut = bAuth; }
                        break;
                    case "write_Gut":
                        if (!bSetVal) { reVal = this.UserAuthorization.write_Gut; }
                        else { this.UserAuthorization.write_Gut = bAuth; }
                        break;
                    case "read_Relation":
                        if (!bSetVal) { reVal = this.UserAuthorization.read_Relation; }
                        else { this.UserAuthorization.read_Relation = bAuth; }
                        break;
                    case "write_Relation":
                        if (!bSetVal) { reVal = this.UserAuthorization.write_Relation; }
                        else { this.UserAuthorization.write_Relation = bAuth; }
                        break;
                    case "read_Order":
                        if (!bSetVal) { reVal = this.UserAuthorization.read_Order; }
                        else { this.UserAuthorization.read_Order = bAuth; }
                        break;
                    case "write_Order":
                        if (!bSetVal) { reVal = this.UserAuthorization.write_Order; }
                        else { this.UserAuthorization.write_Order = bAuth; }
                        break;
                    case "write_TransportOrder":
                        if (!bSetVal) { reVal = this.UserAuthorization.write_TransportOrder; }
                        else { this.UserAuthorization.write_TransportOrder = bAuth; }
                        break;
                    case "read_TransportOrder":
                        if (!bSetVal) { reVal = this.UserAuthorization.read_TransportOrder; }
                        else { this.UserAuthorization.read_TransportOrder = bAuth; }
                        break;
                    case "write_Disposition":
                        if (!bSetVal) { reVal = this.UserAuthorization.write_Disposition; }
                        else { this.UserAuthorization.write_Disposition = bAuth; }
                        break;
                    case "read_Disposition":
                        if (!bSetVal) { reVal = this.UserAuthorization.read_Disposition; }
                        else { this.UserAuthorization.read_Disposition = bAuth; }
                        break;
                    case "read_FaktLager":
                        if (!bSetVal) { reVal = this.UserAuthorization.read_FaktLager; }
                        else { this.UserAuthorization.read_FaktLager = bAuth; }
                        break;
                    case "write_FaktLager":
                        if (!bSetVal) { reVal = this.UserAuthorization.write_FaktLager; }
                        else { this.UserAuthorization.write_FaktLager = bAuth; }
                        break;
                    case "read_FaktSpedition":
                        if (!bSetVal) { reVal = this.UserAuthorization.read_FaktSpedition; }
                        else { this.UserAuthorization.read_FaktSpedition = bAuth; }
                        break;
                    case "write_FaktSpedition":
                        if (!bSetVal) { reVal = this.UserAuthorization.write_FaktSpedition; }
                        else { this.UserAuthorization.write_FaktSpedition = bAuth; }
                        break;
                    case "read_Bestand":
                        if (!bSetVal) { reVal = this.UserAuthorization.read_Bestand; }
                        else { this.UserAuthorization.read_Bestand = bAuth; }
                        break;
                    case "read_LagerEingang":
                        if (!bSetVal) { reVal = this.UserAuthorization.read_LagerEingang; }
                        else { this.UserAuthorization.read_LagerEingang = bAuth; }
                        break;
                    case "write_LagerEingang":
                        if (!bSetVal) { reVal = this.UserAuthorization.write_LagerEingang; }
                        else { this.UserAuthorization.write_LagerEingang = bAuth; }
                        break;
                    case "read_LagerAusgang":
                        if (!bSetVal) { reVal = this.UserAuthorization.read_LagerAusgang; }
                        else { this.UserAuthorization.read_LagerAusgang = bAuth; }
                        break;
                    case "write_LagerAusgang":
                        if (!bSetVal) { reVal = this.UserAuthorization.write_LagerAusgang; }
                        else { this.UserAuthorization.write_LagerAusgang = bAuth; }
                        break;
                    case "read_User":
                        if (!bSetVal) { reVal = this.UserAuthorization.read_User; }
                        else { this.UserAuthorization.read_User = bAuth; }
                        break;
                    case "write_User":
                        if (!bSetVal) { reVal = this.UserAuthorization.write_User; }
                        else { this.UserAuthorization.write_User = bAuth; }
                        break;
                    case "read_Arbeitsbereich":
                        if (!bSetVal) { reVal = this.UserAuthorization.read_Arbeitsbereich; }
                        else { this.UserAuthorization.read_Arbeitsbereich = bAuth; }
                        break;
                    case "write_Arbeitsbereich":
                        if (!bSetVal) { reVal = this.UserAuthorization.write_Arbeitsbereich; }
                        else { this.UserAuthorization.write_Arbeitsbereich = bAuth; }
                        break;
                    case "read_Mandant":
                        if (!bSetVal) { reVal = this.UserAuthorization.read_Mandant; }
                        else { this.UserAuthorization.read_Mandant = bAuth; }
                        break;
                    case "write_Mandant":
                        if (!bSetVal) { reVal = this.UserAuthorization.write_Mandant; }
                        else { this.UserAuthorization.write_Mandant = bAuth; }
                        break;
                    case "read_Statistik":
                        if (!bSetVal) { reVal = this.UserAuthorization.read_Statistik; }
                        else { this.UserAuthorization.read_Statistik = bAuth; }
                        break;
                    case "read_Einheit":
                        if (!bSetVal) { reVal = this.UserAuthorization.read_Einheit; }
                        else { this.UserAuthorization.read_Einheit = bAuth; }
                        break;
                    case "write_Einheit":
                        if (!bSetVal) { reVal = this.UserAuthorization.write_Einheit; }
                        else { this.UserAuthorization.write_Einheit = bAuth; }
                        break;
                    case "read_Schaden":
                        if (!bSetVal) { reVal = this.UserAuthorization.read_Schaden; }
                        else { this.UserAuthorization.read_Schaden = bAuth; }
                        break;
                    case "write_Schaden":
                        if (!bSetVal) { reVal = this.UserAuthorization.write_Schaden; }
                        else { this.UserAuthorization.write_Schaden = bAuth; }
                        break;
                    case "read_LagerOrt":
                        if (!bSetVal) { reVal = this.UserAuthorization.read_LagerOrt; }
                        else { this.UserAuthorization.read_LagerOrt = bAuth; }
                        break;
                    case "write_LagerOrt":
                        if (!bSetVal) { reVal = this.UserAuthorization.write_LagerOrt; }
                        else { this.UserAuthorization.write_LagerOrt = bAuth; }
                        break;
                    case "read_ASNTransfer":
                        if (!bSetVal) { reVal = this.UserAuthorization.read_ASNTransfer; }
                        else { this.UserAuthorization.read_ASNTransfer = bAuth; }
                        break;
                    case "write_ASNTransfer":
                        if (!bSetVal) { reVal = this.UserAuthorization.write_ASNTransfer; }
                        else { this.UserAuthorization.write_ASNTransfer = bAuth; }
                        break;
                    case "read_FaktExtraCharge":
                        if (!bSetVal) { reVal = this.UserAuthorization.read_FaktExtraCharge; }
                        else { this.UserAuthorization.read_FaktExtraCharge = bAuth; }
                        break;
                    case "write_FaktExtraCharge":
                        if (!bSetVal) { reVal = this.UserAuthorization.write_FaktExtraCharge; }
                        else { this.UserAuthorization.write_FaktExtraCharge = bAuth; }
                        break;
                    case "access_StKV":
                        if (!bSetVal) { reVal = this.UserAuthorization.access_StKV; }
                        else { this.UserAuthorization.access_StKV = bAuth; }
                        break;

                    case "access_App":
                        if (!bSetVal) { reVal = this.UserAuthorization.access_App; }
                        else { this.UserAuthorization.access_App = bAuth; }
                        break;
                    case "access_AppStoreIn":
                        if (!bSetVal) { reVal = this.UserAuthorization.access_AppStoreIn; }
                        else { this.UserAuthorization.access_AppStoreIn = bAuth; }
                        break;
                    case "access_AppStoreOut":
                        if (!bSetVal) { reVal = this.UserAuthorization.access_AppStoreOut; }
                        else { this.UserAuthorization.access_AppStoreOut = bAuth; }
                        break;
                    case "access_AppInventory":
                        if (!bSetVal) { reVal = this.UserAuthorization.access_AppInventory; }
                        else { this.UserAuthorization.access_AppInventory = bAuth; }
                        break;
                    case "read_Inventory":
                        if (!bSetVal) { reVal = this.UserAuthorization.read_Inventory; }
                        else { this.UserAuthorization.read_Inventory = bAuth; }
                        break;
                    case "write_Inventory":
                        if (!bSetVal) { reVal = this.UserAuthorization.write_Inventory; }
                        else { this.UserAuthorization.write_Inventory = bAuth; }
                        break;

                    default:
                        reVal = false;
                        break;
                }
                if (strCol == myDBCol)
                {
                    break;
                }
            }
            return reVal;
        }

        public void GetUserBerechtigungLogin(ref Globals._GL_USER GL_User)
        {
            this._GL_User = GL_User;
            this.UserId = (int)GL_User.User_ID;
            this.FillByUserID();
            GL_User = this._GL_User;
        }

        public void FillByUserID()
        {
            if (UserId > 0)
            {
                string strSQL = sql_GetByUserId;
                DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "UserAuth");
                //Fill(dt);
                SetValue(dt);
                InitDataTableUserAuth();
            }
        }

        public void FillByID()
        {
            if (UserAuthorization.Id > 0)
            {
                string strSQL = sql_Get;
                DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "UserAuth");
                //Fill(dt);
                SetValue(dt);
                InitDataTableUserAuth();
            }
        }
        ///<summary>UsersViewModel / Add</summary>
        ///<remarks></remarks>
        public void Add()
        {
            string strSql = sql_Add;
            strSql = strSql + "Select @@IDENTITY as 'ID';";

            string strTmp = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSql, "InsertUser", BenutzerID);
            int.TryParse(strTmp, out int iTmp);
            UserAuthorization.Id = iTmp;
        }

        public void CreateAdminAuth()
        {
            string strSQL = sql_Get;
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "UserAuth");
            //Berechtiugen füllen alles auf true
            for (Int32 i = 0; i <= dt.Columns.Count - 1; i++)
            {
                switch (i)
                {
                    case 0:
                        //dt.Rows[0][i]=0; 
                        break;

                    case 1:
                        //dt.Rows[0][i]= myUserID;
                        break;

                    default:
                        dt.Rows[0][i] = true;
                        break;
                }
            }
            //Berechtigungen aus dem Table auslesen und die Klasse füllen
            //Fill(dt);
            SetValue(dt);
            //Insert der Daten
            Update();
        }

        /// <summary>
        ///             Update Prozess
        /// </summary>
        public void Update()
        {
            bool bOK = false;

            if (
                (UserAuthorization is UserAuthorizations) &&
                (UserAuthorization.Id > 0)
                )
            {
                //bOK = clsSQLcon.ExecuteSQL(this.sql_Update, BenutzerID);
                bOK = clsSQLcon.ExecuteSQLWithTRANSACTION(sql_Update, "UpdateAccess", BenutzerID);
            }
            else
            {
                //bOK = clsSQLcon.ExecuteSQL(this.sql_Add, BenutzerID);                
                bOK = clsSQLcon.ExecuteSQLWithTRANSACTION(sql_Add, "UpdateAccess", BenutzerID);
            }

            //bool bOK = clsSQLcon.ExecuteSQL(this.sql_Update, BenutzerID);
            if (bOK)
            {
                //Update der Berechtigungen
                //Userau
                //this.Berechtigung.Update();

                //Add Logbucheintrag update
                string Beschreibung = "User: ID:" + this.UserAuthorization.User.Id.ToString() +
                                      " Name/Loginname:" + this.UserAuthorization.User.Name + "/" + this.UserAuthorization.User.LoginName + " geändert";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
            }
        }

        private void SetValue(System.Data.DataTable dt)
        {
            foreach (DataRow r in dt.Rows)
            {
                SetValue(r);
            }
        }
        private void SetValue(DataRow myRow)
        {
            InitCls();
            this.UserAuthorization.Id = (decimal)myRow["ID"];
            this.UserAuthorization.UserID = (decimal)myRow["UserID"];
            this.UserAuthorization.read_ADR = (bool)myRow["read_ADR"];
            this.UserAuthorization.write_ADR = (bool)myRow["write_ADR"];
            this.UserAuthorization.read_Kunde = (bool)myRow["read_Kunde"];
            this.UserAuthorization.write_Kunde = (bool)myRow["write_Kunde"];
            this.UserAuthorization.read_Personal = (bool)myRow["read_Personal"];
            this.UserAuthorization.write_Personal = (bool)myRow["write_Personal"];
            this.UserAuthorization.read_KFZ = (bool)myRow["read_KFZ"];
            this.UserAuthorization.write_KFZ = (bool)myRow["write_KFZ"];
            this.UserAuthorization.read_Gut = (bool)myRow["read_Gut"];
            this.UserAuthorization.write_Gut = (bool)myRow["write_Gut"];
            this.UserAuthorization.read_Relation = (bool)myRow["read_Relation"];
            this.UserAuthorization.write_Relation = (bool)myRow["write_Relation"];
            this.UserAuthorization.read_Order = (bool)myRow["read_Order"];
            this.UserAuthorization.write_Order = (bool)myRow["write_Order"];
            this.UserAuthorization.write_TransportOrder = (bool)myRow["write_TransportOrder"];
            this.UserAuthorization.read_TransportOrder = (bool)myRow["read_TransportOrder"];
            this.UserAuthorization.write_Disposition = (bool)myRow["write_Disposition"];
            this.UserAuthorization.read_Disposition = (bool)myRow["read_Disposition"];
            this.UserAuthorization.read_FaktLager = (bool)myRow["read_FaktLager"];
            this.UserAuthorization.write_FaktLager = (bool)myRow["write_FaktLager"];
            this.UserAuthorization.read_FaktSpedition = (bool)myRow["read_FaktSpedition"];
            this.UserAuthorization.write_FaktSpedition = (bool)myRow["write_FaktSpedition"];
            this.UserAuthorization.read_Bestand = (bool)myRow["read_Bestand"];
            this.UserAuthorization.read_LagerEingang = (bool)myRow["read_LagerEingang"];
            this.UserAuthorization.write_LagerEingang = (bool)myRow["write_LagerEingang"];
            this.UserAuthorization.read_LagerAusgang = (bool)myRow["read_LagerAusgang"];
            this.UserAuthorization.write_LagerAusgang = (bool)myRow["write_LagerAusgang"];
            this.UserAuthorization.read_User = (bool)myRow["read_User"];
            this.UserAuthorization.write_User = (bool)myRow["write_User"];
            this.UserAuthorization.read_Arbeitsbereich = (bool)myRow["read_Arbeitsbereich"];
            this.UserAuthorization.write_Arbeitsbereich = (bool)myRow["write_Arbeitsbereich"];
            this.UserAuthorization.read_Mandant = (bool)myRow["read_Mandant"];
            this.UserAuthorization.write_Mandant = (bool)myRow["write_Mandant"];
            this.UserAuthorization.read_Statistik = (bool)myRow["read_Statistik"];
            this.UserAuthorization.read_Einheit = (bool)myRow["read_Einheit"];
            this.UserAuthorization.write_Einheit = (bool)myRow["write_Einheit"];
            this.UserAuthorization.read_Schaden = (bool)myRow["read_Schaden"];
            this.UserAuthorization.write_Schaden = (bool)myRow["write_Schaden"];
            this.UserAuthorization.read_LagerOrt = (bool)myRow["read_LagerOrt"];
            this.UserAuthorization.write_LagerOrt = (bool)myRow["write_LagerOrt"];
            this.UserAuthorization.read_ASNTransfer = (bool)myRow["read_ASNTransfer"];
            this.UserAuthorization.write_ASNTransfer = (bool)myRow["write_ASNTransfer"];
            this.UserAuthorization.read_FaktExtraCharge = (bool)myRow["read_FaktExtraCharge"];
            this.UserAuthorization.write_FaktExtraCharge = (bool)myRow["write_FaktExtraCharge"];
            this.UserAuthorization.access_StKV = (bool)myRow["access_StKV"];
            this.UserAuthorization.access_App = (bool)myRow["access_App"];
            this.UserAuthorization.access_AppStoreIn = (bool)myRow["access_AppStoreIn"];
            this.UserAuthorization.access_AppStoreOut = (bool)myRow["access_AppStoreOut"];
            this.UserAuthorization.access_AppInventory = (bool)myRow["access_AppInventory"];
            this.UserAuthorization.read_Inventory = (bool)myRow["read_Inventory"];
            this.UserAuthorization.write_Inventory = (bool)myRow["write_Inventory"];
        }

        public void SetValueToUserAuth(bool bInsertData)
        {
            GetUserAuthByDBCol(string.Empty, true);
            if (bInsertData)
            {
                if (UserAuthorization.UserID > 0)
                {
                    Add();
                }
            }

        }

        ///-----------------------------------------------------------------------------------------------------
        ///                             sql Statements
        ///-----------------------------------------------------------------------------------------------------

        /// <summary>
        ///             Update sql - String
        /// </summary>
        public string sql_Add
        {
            get
            {
                string strSQL = "INSERT INTO Userberechtigungen ( UserID " +
                                                        ",read_ADR" +
                                                        ",write_ADR" +
                                                        ",read_Kunde" +
                                                        ",write_Kunde" +        //5

                                                        ",read_Personal" +
                                                        ",write_Personal" +
                                                        ",read_KFZ" +
                                                        ",write_KFZ" +
                                                        ",read_Gut" +           //10

                                                        ",write_Gut" +
                                                        ",read_Relation" +
                                                        ",write_Relation" +
                                                        ",read_Order" +
                                                        ",write_Order" +        //15

                                                        ",write_TransportOrder" +
                                                        ",read_TransportOrder" +
                                                        ",write_Disposition" +
                                                        ",read_Disposition" +
                                                        ",read_FaktLager" +     //20

                                                        ",write_FaktLager" +
                                                        ",read_FaktSpedition" +
                                                        ",write_FaktSpedition" +
                                                        ",read_Bestand" +
                                                        ",read_LagerEingang" +  //25

                                                        ",write_LagerEingang" +
                                                        ",read_LagerAusgang" +
                                                        ",write_LagerAusgang" +
                                                        ",read_User" +
                                                        ",write_User" +         //30    


                                                        ",read_Arbeitsbereich" +
                                                        ",write_Arbeitsbereich" +
                                                        ",read_Mandant" +
                                                        ",write_Mandant" +
                                                        ",read_Statistik" +      //35

                                                        ",read_Einheit" +
                                                        ",write_Einheit" +
                                                        ",read_Schaden" +
                                                        ",write_Schaden" +
                                                        ",read_LagerOrt" +      //40

                                                        ", write_LagerOrt" +
                                                        ", read_ASNTransfer" +
                                                        ", write_ASNTransfer" +
                                                        ", read_FaktExtraCharge" +
                                                        ", write_FaktExtraCharge" + //45

                                                        ", access_StKV" +
                                                        ", access_App" +
                                                        ", access_AppStoreIn" +
                                                        ", access_AppStoreOut" +
                                                        ", access_AppInventory" +  // 50

                                                        ", read_Inventory" +
                                                        ", write_Inventory " +

                                                        ") " +

                                            "VALUES (" + this.UserId +
                                                    "," + Convert.ToInt32(UserAuthorization.read_ADR) +
                                                    "," + Convert.ToInt32(UserAuthorization.write_ADR) +
                                                    "," + Convert.ToInt32(UserAuthorization.read_Kunde) +
                                                    "," + Convert.ToInt32(UserAuthorization.write_Kunde) +    //5

                                                    "," + Convert.ToInt32(UserAuthorization.read_Personal) +
                                                    "," + Convert.ToInt32(UserAuthorization.write_Personal) +
                                                    "," + Convert.ToInt32(UserAuthorization.read_KFZ) +
                                                    "," + Convert.ToInt32(UserAuthorization.write_KFZ) +
                                                    "," + Convert.ToInt32(UserAuthorization.read_Gut) +       //10

                                                    "," + Convert.ToInt32(UserAuthorization.write_Gut) +
                                                    "," + Convert.ToInt32(UserAuthorization.read_Relation) +
                                                    "," + Convert.ToInt32(UserAuthorization.write_Relation) +
                                                    "," + Convert.ToInt32(UserAuthorization.read_Order) +
                                                    "," + Convert.ToInt32(UserAuthorization.write_Order) +    //15

                                                    "," + Convert.ToInt32(UserAuthorization.write_TransportOrder) +
                                                    "," + Convert.ToInt32(UserAuthorization.read_TransportOrder) +
                                                    "," + Convert.ToInt32(UserAuthorization.write_Disposition) +
                                                    "," + Convert.ToInt32(UserAuthorization.read_Disposition) +
                                                    "," + Convert.ToInt32(UserAuthorization.read_FaktLager) +     //20

                                                    "," + Convert.ToInt32(UserAuthorization.write_FaktLager) +
                                                    "," + Convert.ToInt32(UserAuthorization.read_FaktSpedition) +
                                                    "," + Convert.ToInt32(UserAuthorization.write_FaktSpedition) +
                                                    "," + Convert.ToInt32(UserAuthorization.read_Bestand) +
                                                    "," + Convert.ToInt32(UserAuthorization.read_LagerEingang) +  //25

                                                    "," + Convert.ToInt32(UserAuthorization.write_LagerEingang) +
                                                    "," + Convert.ToInt32(UserAuthorization.read_LagerAusgang) +
                                                    "," + Convert.ToInt32(UserAuthorization.write_LagerAusgang) +
                                                    "," + Convert.ToInt32(UserAuthorization.read_User) +
                                                    "," + Convert.ToInt32(UserAuthorization.write_User) +         //30

                                                    "," + Convert.ToInt32(UserAuthorization.read_Arbeitsbereich) +
                                                    "," + Convert.ToInt32(UserAuthorization.write_Arbeitsbereich) +
                                                    "," + Convert.ToInt32(UserAuthorization.read_Mandant) +
                                                    "," + Convert.ToInt32(UserAuthorization.write_Mandant) +
                                                    "," + Convert.ToInt32(UserAuthorization.read_Statistik) +     //35

                                                    "," + Convert.ToInt32(UserAuthorization.read_Einheit) +
                                                    "," + Convert.ToInt32(UserAuthorization.write_Einheit) +
                                                    "," + Convert.ToInt32(UserAuthorization.read_Schaden) +
                                                    "," + Convert.ToInt32(UserAuthorization.write_Schaden) +
                                                    "," + Convert.ToInt32(UserAuthorization.read_LagerOrt) +      //40

                                                    "," + Convert.ToInt32(UserAuthorization.write_LagerOrt) +
                                                    "," + Convert.ToInt32(UserAuthorization.read_ASNTransfer) +
                                                    "," + Convert.ToInt32(UserAuthorization.write_ASNTransfer) +
                                                     "," + Convert.ToInt32(UserAuthorization.read_FaktExtraCharge) +
                                                    "," + Convert.ToInt32(UserAuthorization.write_FaktExtraCharge) +  //45

                                                    "," + Convert.ToInt32(UserAuthorization.access_StKV) +
                                                    "," + Convert.ToInt32(UserAuthorization.access_App) +
                                                    "," + Convert.ToInt32(UserAuthorization.access_AppStoreIn) +
                                                    "," + Convert.ToInt32(UserAuthorization.access_AppStoreOut) +
                                                    "," + Convert.ToInt32(UserAuthorization.access_AppInventory) +  // 50

                                                    "," + Convert.ToInt32(UserAuthorization.read_Inventory) +
                                                    "," + Convert.ToInt32(UserAuthorization.write_Inventory) +


                                                    "); ";
                return strSQL;
            }
        }
        /// <summary>
        ///             Update sql - String
        /// </summary>
        public string sql_Update
        {
            get
            {
                GetUserAuthByDBCol(string.Empty, true);
                string strSQL = "Update Userberechtigungen SET " +
                                                     "read_ADR=" + Convert.ToInt32(UserAuthorization.read_ADR) +
                                                     ", write_ADR=" + Convert.ToInt32(UserAuthorization.write_ADR) +
                                                     ", read_Kunde=" + Convert.ToInt32(UserAuthorization.read_Kunde) +
                                                     ", write_Kunde=" + Convert.ToInt32(UserAuthorization.write_Kunde) +
                                                     ", read_Personal=" + Convert.ToInt32(UserAuthorization.read_Personal) +
                                                     ", write_Personal=" + Convert.ToInt32(UserAuthorization.write_Personal) +
                                                     ", read_KFZ=" + Convert.ToInt32(UserAuthorization.read_KFZ) +
                                                     ", write_KFZ=" + Convert.ToInt32(UserAuthorization.write_KFZ) +
                                                     ", read_Gut=" + Convert.ToInt32(UserAuthorization.read_Gut) +
                                                     ", write_Gut=" + Convert.ToInt32(UserAuthorization.write_Gut) +
                                                     ", read_Relation=" + Convert.ToInt32(UserAuthorization.read_Relation) +
                                                     ", write_Relation=" + Convert.ToInt32(UserAuthorization.write_Relation) +
                                                     ", read_Order=" + Convert.ToInt32(UserAuthorization.read_Order) +
                                                     ", write_Order=" + Convert.ToInt32(UserAuthorization.write_Order) +
                                                     ", write_TransportOrder=" + Convert.ToInt32(UserAuthorization.write_TransportOrder) +
                                                     ", read_TransportOrder=" + Convert.ToInt32(UserAuthorization.read_TransportOrder) +
                                                     ", write_Disposition=" + Convert.ToInt32(UserAuthorization.write_Disposition) +
                                                     ", read_Disposition=" + Convert.ToInt32(UserAuthorization.read_Disposition) +
                                                     ", read_FaktLager=" + Convert.ToInt32(UserAuthorization.read_FaktLager) +
                                                     ", write_FaktLager=" + Convert.ToInt32(UserAuthorization.write_FaktLager) +
                                                     ", read_FaktSpedition=" + Convert.ToInt32(UserAuthorization.read_FaktSpedition) +
                                                     ", write_FaktSpedition=" + Convert.ToInt32(UserAuthorization.write_FaktSpedition) +
                                                     ", read_Bestand=" + Convert.ToInt32(UserAuthorization.read_Bestand) +
                                                     ", read_LagerEingang=" + Convert.ToInt32(UserAuthorization.read_LagerEingang) +
                                                     ", write_LagerEingang=" + Convert.ToInt32(UserAuthorization.write_LagerEingang) +
                                                     ", read_LagerAusgang=" + Convert.ToInt32(UserAuthorization.read_LagerAusgang) +
                                                     ", write_LagerAusgang=" + Convert.ToInt32(UserAuthorization.write_LagerAusgang) +
                                                     ", read_User=" + Convert.ToInt32(UserAuthorization.read_User) +
                                                     ", write_User=" + Convert.ToInt32(UserAuthorization.write_User) +
                                                     ", read_Arbeitsbereich=" + Convert.ToInt32(UserAuthorization.read_Arbeitsbereich) +
                                                     ", write_Arbeitsbereich=" + Convert.ToInt32(UserAuthorization.write_Arbeitsbereich) +
                                                     ", read_Mandant=" + Convert.ToInt32(UserAuthorization.read_Mandant) +
                                                     ", write_Mandant=" + Convert.ToInt32(UserAuthorization.write_Mandant) +
                                                     ", read_Statistik=" + Convert.ToInt32(UserAuthorization.read_Statistik) +
                                                     ", read_Einheit=" + Convert.ToInt32(UserAuthorization.read_Einheit) +
                                                     ", write_Einheit=" + Convert.ToInt32(UserAuthorization.write_Einheit) +
                                                     ", read_Schaden=" + Convert.ToInt32(UserAuthorization.read_Schaden) +
                                                     ", write_Schaden=" + Convert.ToInt32(UserAuthorization.write_Schaden) +
                                                     ", read_LagerOrt=" + Convert.ToInt32(UserAuthorization.read_LagerOrt) +
                                                     ", write_LagerOrt=" + Convert.ToInt32(UserAuthorization.write_LagerOrt) +
                                                     ", read_ASNTransfer=" + Convert.ToInt32(UserAuthorization.read_ASNTransfer) +
                                                     ", write_ASNTransfer=" + Convert.ToInt32(UserAuthorization.write_ASNTransfer) +
                                                     ", read_FaktExtraCharge=" + Convert.ToInt32(UserAuthorization.read_FaktExtraCharge) +
                                                     ", write_FaktExtraCharge=" + Convert.ToInt32(UserAuthorization.write_FaktExtraCharge) +
                                                     ", access_StKV=" + Convert.ToInt32(UserAuthorization.access_StKV) +
                                                     ", access_App =" + Convert.ToInt32(UserAuthorization.access_App) +
                                                     ", access_AppStoreIn =" + Convert.ToInt32(UserAuthorization.access_AppStoreIn) +
                                                     ", access_AppStoreOut =" + Convert.ToInt32(UserAuthorization.access_AppStoreOut) +
                                                     ", access_AppInventory =" + Convert.ToInt32(UserAuthorization.access_AppInventory) +
                                                     ", read_Inventory = " + Convert.ToInt32(UserAuthorization.read_Inventory) +
                                                     ", write_Inventory = " + Convert.ToInt32(UserAuthorization.write_Inventory) +

                                                     " WHERE UserID=" + UserId + " ;";
                return strSQL;
            }
        }
        /// <summary>
        ///             GET sql - String
        /// </summary>
        public string sql_Get
        {
            get
            {
                string strSql = "SELECT * FROM Userberechtigungen WHERE ID=" + UserAuthorization.Id;
                return strSql;
            }
        }
        /// <summary>
        ///             GET sql - String
        /// </summary>
        public string sql_GetByUserId
        {
            get
            {
                string strSql = "SELECT * FROM Userberechtigungen WHERE UserID=" + UserId;
                return strSql;
            }
        }
        /// <summary>
        ///             DELETE sql - String
        /// </summary>
        public string sql_Delete
        {
            get
            {
                string strSql = "DELETE FROM Userberechtigungen WHERE Id =" + UserAuthorization.Id;
                return strSql;
            }
        }












    }
}

