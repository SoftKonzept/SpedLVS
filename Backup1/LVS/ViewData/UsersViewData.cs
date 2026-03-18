using Common.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace LVS.ViewData
{
    public class UsersViewData
    {
        public const decimal Default_FontSize = 7.5M;
        public const Int32 Default_SMTPPort = 25;

        public string Loginname = string.Empty;
        public string Password = string.Empty;
        public bool IsWebLogin = false;
        public Users User { get; set; }
        public UserAuthorizationsViewData UserAuthorizationsVM { get; set; }

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

        public List<Users> ListUsers { get; set; } = new List<Users>();

        public UsersViewData()
        {
            InitCls();
        }
        public UsersViewData(Users myUser, Globals._GL_USER myGLUser) : this()
        {
            this.User = myUser;
            this._GL_User = myGLUser;
            UserAuthorizationsVM = new UserAuthorizationsViewData(this._GL_User);
        }
        public UsersViewData(Globals._GL_USER myGLUser) : this()
        {
            this._GL_User = myGLUser;
            InitCls();
            UserAuthorizationsVM = new UserAuthorizationsViewData(this._GL_User);
        }

        public UsersViewData(string myLoginName, string myPass, bool myIsWebCheck) : this()
        {
            IsWebLogin = myIsWebCheck;
            Loginname = myLoginName;
            Password = myPass;
            InitCls();

            System.Data.DataTable dt = new System.Data.DataTable();
            //string strSql = string.Empty;
            //strSql = this.sql_Get;
            //dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "USER");
            if (CheckLogin())
            {
                SetValue(dt);
                UserAuthorizationsVM = new UserAuthorizationsViewData(this._GL_User, (int)User.Id);
                UserAuthorizationsVM.UserAuthorization.User = this.User;
                this.User.UserAuthorization = UserAuthorizationsVM.UserAuthorization;
            }
            else
            {
                UserAuthorizationsVM = new UserAuthorizationsViewData(this._GL_User);
            }
        }

        public UsersViewData(Globals._GL_USER myGLUser, int myId) : this()
        {
            this._GL_User = myGLUser;
            InitCls();
            User.Id = myId;
            if (myId > 0)
            {
                Fill();
                UserAuthorizationsVM = new UserAuthorizationsViewData(this._GL_User, (int)User.Id);
                UserAuthorizationsVM.UserAuthorization.User = this.User;
                this.User.UserAuthorization = UserAuthorizationsVM.UserAuthorization;
            }
            else
            {
                UserAuthorizationsVM = new UserAuthorizationsViewData(this._GL_User);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitCls()
        {
            User = new Users();
            this.User.Id = 0;
            this.User.Name = string.Empty;
            this.User.Vorname = string.Empty;
            this.User.pass = string.Empty;
            this.User.Initialen = string.Empty;
            this.User.LoginName = string.Empty;
            this.User.Tel = string.Empty;
            this.User.Fax = string.Empty;
            this.User.Mail = string.Empty;
            this.User.FontSize = Default_FontSize;
            this.User.dtDispoVon = new DateTime(1900, 1, 1); //  Globals.DefaultDateTimeMinValue;
            this.User.dtDispoBis = new DateTime(1900, 1, 1); //Globals.DefaultDateTimeMinValue;
            this.User.SMTPServer = string.Empty;
            this.User.SMTPPort = Default_SMTPPort;
            this.User.SMTPUser = string.Empty;
            this.User.SMTPPasswort = string.Empty;
            this.User.SMTPSSL = false;
            this.User.IsAdmin = false;
        }
        /// <summary>
        /// 
        /// </summary>
        public void GetUsersList()
        {
            ListUsers = new List<Users>();

            string strSQL = sql_GetList;
            DataTable dt = new DataTable("Users");
            dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "Users", "Users", 0);
            foreach (DataRow dr in dt.Rows)
            {
                User = new Users();
                SetValue(dr);
                ListUsers.Add(User);
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
            User.Id = iTmp;
        }

        public void Delete()
        {
            if (User.Id > 0)
            {
                //Add Logbucheintrag Löschen
                //string Benutzername = GetBenutzerNameByID(ID);
                //string Beschreibung = "User: ID:" + ID + " Name: " + Benutzername + " gelöscht";


                string strSQL = string.Empty;
                //strSQL = sql_Delete;
                //strSQL += UserAuthorizationsVM.sql_Delete;

                strSQL = UserAuthorizationsVM.sql_Delete;
                strSQL += sql_Delete;
                clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
                clsSQLcon.ExecuteSQLWithTRANSACTION(strSQL, "UserDelete", BenutzerID);
                //Tabelle AuftragRead aktuallisieren - Baustelle über DB - Beziehungen löschen lassen
                //DeleteUserFromAuftragRead(ID);


                string Benutzername = User.Name;
                string Beschreibung = "User: ID:" + User.Id.ToString() + " Name: " + Benutzername + " gelöscht";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), Beschreibung);
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
                string strSql = "INSERT INTO [User] (Name, LoginName, Pass, Initialen, Vorname, Tel, Fax, Mail, " +
                                                    "SMTPUser, SMTPPass, SMTPServer, SMTPPort, IsAdmin) " +
                                                      "VALUES ('" + this.User.Name + "'" +
                                                                ", '" + this.User.LoginName + "'" +
                                                                ", '" + this.User.pass + "'" +
                                                                ", '" + this.User.Initialen + "'" +
                                                                ", '" + this.User.Vorname + "'" +
                                                                ", '" + this.User.Tel + "'" +
                                                                ", '" + this.User.Fax + "'" +
                                                                ", '" + this.User.Mail + "'" +
                                                                ", '" + this.User.SMTPUser + "'" +
                                                                ", '" + this.User.SMTPPasswort + "'" +
                                                                ", '" + this.User.SMTPServer + "'" +
                                                                ", " + this.User.SMTPPort +
                                                                ", " + Convert.ToInt32(this.User.IsAdmin) +
                                                                ")";
                return strSql;
            }
        }
        public string sql_Delete
        {
            get
            {
                string strSql = "DELETE FROM [User] WHERE ID=" + (int)User.Id + " ;";
                return strSql;
            }
        }


        /// <summary>
        ///             Update sql - String
        /// </summary>
        public string sql_Update
        {
            get
            {
                string strSql = "Update [User] SET Name='" + this.User.Name + "' " +
                                  ", LoginName='" + this.User.LoginName + "' " +
                                  ", Pass='" + this.User.pass + "' " +
                                  ", Initialen='" + this.User.Initialen + "' " +
                                  ", Vorname='" + this.User.Vorname + "' " +
                                  ", Tel='" + this.User.Tel + "' " +
                                  ", Fax='" + this.User.Fax + "' " +
                                  ", Mail='" + this.User.Mail + "' " +
                                  ", SMTPUser='" + this.User.SMTPUser + "' " +
                                  ", SMTPPass='" + this.User.SMTPPasswort + "' " +
                                  ", SMTPServer='" + this.User.SMTPServer + "' " +
                                  ", SMTPPort=" + this.User.SMTPPort +
                                  ", IsAdmin = " + Convert.ToInt32(this.User.IsAdmin) +
                                          " WHERE ID=" + (int)this.User.Id;
                return strSql;
            }
        }
        /// <summary>
        ///             GET sql - String
        /// </summary>
        public string sql_Get
        {
            get
            {
                string strSql = "SELECT * FROM [User] WHERE ID=" + (int)this.User.Id;
                return strSql;
            }
        }
        /// <summary>
        ///             GET sql_GetList - String
        /// </summary>
        public string sql_GetList
        {
            get
            {
                string strSql = "SELECT * FROM [User] ";
                return strSql;
            }
        }
        public string sql_Authentification
        {
            get
            {
                string strSql = "SELECT ID FROM [USER] WHERE LoginName='" + Loginname + "' AND Pass='" + Password + "'";
                return strSql;
            }
        }

        public string sql_AuthentificationWeb
        {
            get
            {
                string strSql = "SELECT u.ID FROM [USER] u " +
                                            "INNER JOIN Userberechtigungen ub on ub.UserID = u.ID " +
                                            " WHERE " +
                                                "u.LoginName='" + Loginname + "' AND " +
                                                "u.Pass='" + Password + "' AND " +
                                                "ub.access_App = 1 ";
                return strSql;
            }
        }

        ///-----------------------------------------------------------------------------------------------------
        ///                             Function / Procedure
        ///-----------------------------------------------------------------------------------------------------


        private bool CheckLogin()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string strSql = string.Empty;
            if (IsWebLogin)
            {
                strSql = this.sql_AuthentificationWeb;
            }
            else
            {
                strSql = this.sql_Authentification;
            }

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "USER");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    this.User.Id = (decimal)dr["ID"];
                }
                if (this.User.Id > 0)
                {
                    Fill();
                }
            }
            return (dt.Rows.Count > 0);
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
            this.User.Id = (decimal)myRow["ID"];
            this.User.Name = myRow["Name"].ToString();
            this.User.Vorname = myRow["Vorname"].ToString();
            this.User.pass = myRow["pass"].ToString();
            this.User.Initialen = myRow["Initialen"].ToString();
            this.User.LoginName = myRow["LoginName"].ToString();
            this.User.Tel = myRow["Tel"].ToString();
            this.User.Fax = myRow["Fax"].ToString();
            this.User.Mail = myRow["Mail"].ToString();
            if (myRow["FontSize"] != DBNull.Value)
            {
                this.User.FontSize = (decimal)myRow["FontSize"];
            }
            else
            {
                this.User.FontSize = clsUser.Default_FontSize;
            }
            if (myRow["dtDispoVon"] != DBNull.Value)
            {
                this.User.dtDispoVon = (DateTime)myRow["dtDispoVon"];
            }
            else
            {
                this.User.dtDispoVon = DateTime.Now.Date;
            }
            if (myRow["dtDispoVon"] != DBNull.Value)
            {
                this.User.dtDispoBis = (DateTime)myRow["dtDispoBis"];
            }
            else
            {
                this.User.dtDispoBis = DateTime.Now.Date.AddDays(2);
            }
            this.User.SMTPUser = myRow["SMTPUser"].ToString();
            this.User.SMTPPasswort = myRow["SMTPPass"].ToString();
            this.User.SMTPServer = myRow["SMTPServer"].ToString();
            Int32 iTmp = clsSystem.const_Mail_SMTPPort;
            Int32.TryParse(myRow["SMTPPort"].ToString(), out iTmp);
            this.User.SMTPPort = iTmp;
            this.User.IsAdmin = (bool)myRow["IsAdmin"];
        }


        /// <summary>
        ///         TEST MR
        /// </summary>
        /// <param name="propDestination"></param>
        /// <param name="strReplaceValue"></param>
        public void SetValue(string propDestination, string strReplaceValue)
        {
            string strProperty = propDestination.Remove(0, propDestination.IndexOf('.') + 1);
            try
            {
                this.GetType().GetProperty(strProperty).SetValue(this, strReplaceValue, null);
                //this.GetType().GetProperty(strProperty).SetValue(this, SourceArt.GetType().GetProperty(strProperty).GetValue(SourceArt, null), null);
            }
            catch (Exception ex)
            { }
        }

        ///<summary>clsUser / Fill</summary>
        ///<remarks></remarks>
        public Globals._GL_USER Fill()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string strSql = string.Empty;
            strSql = this.sql_Get;
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "USER");
            SetValue(dt);

            if (this.User is Users)
            {
                //GLobal User setzen
                _GL_User.User_ID = this.User.Id;
                _GL_User.Name = this.User.Name;
                _GL_User.LoginName = this.User.LoginName;
                _GL_User.initialen = this.User.Initialen;
                _GL_User.Vorname = this.User.Vorname;
                _GL_User.Telefon = this.User.Tel;
                _GL_User.Fax = this.User.Fax;
                _GL_User.Mail = this.User.Mail;
                _GL_User.SMTPUser = this.User.SMTPUser;
                _GL_User.SMTPPass = this.User.SMTPPasswort;
                _GL_User.SMTPServer = this.User.SMTPServer;
                _GL_User.SMTPPort = this.User.SMTPPort;
                _GL_User.IsAdmin = this.User.IsAdmin;

                _GL_User.us_dtDispoVon = this.User.dtDispoVon;
                _GL_User.us_dtDispoBis = this.User.dtDispoBis;
                _GL_User.us_decFontSize = this.User.FontSize;

                UserAuthorizationsVM = new UserAuthorizationsViewData(this._GL_User, (int)User.Id);
                User.UserAuthorization = UserAuthorizationsVM.UserAuthorization;

                _GL_User.Menu_System = ((User.UserAuthorization.read_User) ||
                                        (User.UserAuthorization.write_User) ||
                                        (User.UserAuthorization.read_Arbeitsbereich) ||
                                        (User.UserAuthorization.write_Arbeitsbereich) ||
                                        (User.UserAuthorization.read_Mandant) ||
                                        (User.UserAuthorization.write_Mandant));

                _GL_User.Menu_Auftragserfassung = ((User.UserAuthorization.read_Order) ||
                                                    (User.UserAuthorization.write_Order));

                _GL_User.Menu_Disposition = ((User.UserAuthorization.read_Disposition) ||
                                             (User.UserAuthorization.write_Disposition));

                _GL_User.Menu_Lager = ((User.UserAuthorization.read_Bestand) ||
                                       (User.UserAuthorization.read_LagerEingang) ||
                                       (User.UserAuthorization.write_LagerEingang) ||
                                       (User.UserAuthorization.read_LagerAusgang) ||
                                       (User.UserAuthorization.write_LagerAusgang));

                _GL_User.Menu_Statistik = (User.UserAuthorization.read_Statistik);

                _GL_User.Menue_Fakturierung = ((User.UserAuthorization.read_FaktLager) ||
                                               (User.UserAuthorization.write_FaktLager) ||
                                               (User.UserAuthorization.read_FaktSpedition) ||
                                               (User.UserAuthorization.write_FaktSpedition));

                _GL_User.Menu_Stammdaten = ((User.UserAuthorization.read_ADR) ||
                                            (User.UserAuthorization.write_ADR) ||
                                            (User.UserAuthorization.read_Kunde) ||
                                            (User.UserAuthorization.write_Kunde) ||
                                            (User.UserAuthorization.read_Personal) ||
                                            (User.UserAuthorization.write_Personal) ||
                                            (User.UserAuthorization.read_KFZ) ||
                                            (User.UserAuthorization.write_KFZ) ||
                                            (User.UserAuthorization.read_Gut) ||
                                            (User.UserAuthorization.write_Gut) ||
                                            (User.UserAuthorization.read_Relation) ||
                                            (User.UserAuthorization.write_Relation));

                _GL_User.read_ADR = User.UserAuthorization.read_ADR;
                _GL_User.write_ADR = User.UserAuthorization.write_ADR;
                _GL_User.read_Kunde = User.UserAuthorization.read_Kunde;
                _GL_User.write_Kunde = User.UserAuthorization.write_Kunde;
                _GL_User.read_Personal = User.UserAuthorization.read_Personal;
                _GL_User.write_Personal = User.UserAuthorization.write_Personal;
                _GL_User.read_KFZ = User.UserAuthorization.read_KFZ;
                _GL_User.write_KFZ = User.UserAuthorization.write_KFZ;
                _GL_User.read_Gut = User.UserAuthorization.read_Gut;
                _GL_User.write_Gut = User.UserAuthorization.write_Gut;
                _GL_User.read_Relation = User.UserAuthorization.read_Relation;
                _GL_User.write_Relation = User.UserAuthorization.write_Relation;
                _GL_User.read_Order = User.UserAuthorization.read_Order;
                _GL_User.write_Order = User.UserAuthorization.write_Order;
                _GL_User.write_TransportOrder = User.UserAuthorization.write_TransportOrder;
                _GL_User.read_TransportOrder = User.UserAuthorization.read_TransportOrder;
                _GL_User.write_Disposition = User.UserAuthorization.write_Disposition;
                _GL_User.read_Disposition = User.UserAuthorization.read_Disposition;
                _GL_User.read_FaktLager = User.UserAuthorization.read_FaktLager;
                _GL_User.write_FaktLager = User.UserAuthorization.write_FaktLager;
                _GL_User.read_FaktSpedition = User.UserAuthorization.read_FaktSpedition;
                _GL_User.write_FaktSpedition = User.UserAuthorization.write_FaktSpedition;
                _GL_User.read_Bestand = User.UserAuthorization.read_Bestand;
                _GL_User.read_LagerEingang = User.UserAuthorization.read_LagerEingang;
                _GL_User.write_LagerEingang = User.UserAuthorization.write_LagerEingang;
                _GL_User.read_LagerAusgang = User.UserAuthorization.read_LagerAusgang;
                _GL_User.write_LagerAusgang = User.UserAuthorization.write_LagerAusgang;
                _GL_User.read_User = User.UserAuthorization.read_User;
                _GL_User.write_User = User.UserAuthorization.write_User;
                _GL_User.read_Arbeitsbereich = User.UserAuthorization.read_Arbeitsbereich;
                _GL_User.write_Arbeitsbereich = User.UserAuthorization.write_Arbeitsbereich;
                _GL_User.read_Mandant = User.UserAuthorization.read_Mandant;
                _GL_User.write_Mandant = User.UserAuthorization.write_Mandant;
                _GL_User.read_Statistik = User.UserAuthorization.read_Statistik;
                _GL_User.read_Einheit = User.UserAuthorization.read_Einheit;
                _GL_User.write_Einheit = User.UserAuthorization.write_Einheit;
                _GL_User.read_Schaden = User.UserAuthorization.read_Schaden;
                _GL_User.write_Schaden = User.UserAuthorization.write_Schaden;
                _GL_User.read_LagerOrt = User.UserAuthorization.read_LagerOrt;
                _GL_User.write_LagerOrt = User.UserAuthorization.write_LagerOrt;
                _GL_User.read_ASNTransfer = User.UserAuthorization.read_ASNTransfer;
                _GL_User.write_ASNTransfer = User.UserAuthorization.write_ASNTransfer;
                _GL_User.read_FaktExtraCharge = User.UserAuthorization.read_FaktExtraCharge;
                _GL_User.write_FaktExtraCharge = User.UserAuthorization.write_FaktExtraCharge;
                _GL_User.write_FibuExport = false;  //Baustelle     
                _GL_User.access_StKV = User.UserAuthorization.access_StKV;
                _GL_User.access_App = User.UserAuthorization.access_App;
                _GL_User.access_AppStoreIn = User.UserAuthorization.access_AppStoreIn;
                _GL_User.access_AppStoreOut = User.UserAuthorization.access_AppStoreOut;
                _GL_User.access_AppInventory = User.UserAuthorization.access_AppInventory;


                //Berechtigungen setzen
                //Berechtigung = new clsUserBerechtigungen();
                //Berechtigung._GL_User = this._GL_User;
                //Berechtigung.UserID = this.User.Id;
                //Berechtigung.FillByUserID();


                //Arbeitsbereiche
                ArbeitsbereichAccess = new clsArbeitsbereichUser();
                ArbeitsbereichAccess.InitClass(this._GL_User);
                //this.ListArbeitsbereichAccess = new List<decimal>();
                //this.ListArbeitsbereichAccess = ArbeitsbereichAccess.ListArbeitsbereichAccess;

            }
            return this._GL_User;
        }

        ///<summary>UsersViewModel / AddUserDaten</summary>
        ///<remarks></remarks>
        public void AddUserDaten()
        {
            if (this.User.Id == 0)
            {
                //neuer Datensatz
                AddNewDatenToUser();
            }
        }
        ///<summary>UsersViewModel / AddNewDatenToUser</summary>
        ///<remarks></remarks>
        private void AddNewDatenToUser()
        {
            if (this.User is Users)
            {
                string strSQL = this.sql_Add;
                strSQL = strSQL + "Select @@IDENTITY as 'ID' ";
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                decimal decTmp = 0;
                decimal.TryParse(strTmp, out decTmp);
                if (decTmp > 0)
                {
                    this.User.Id = decTmp;
                    //Add Logbucheintrag Eintrag
                    string Beschreibung = "User: Name:" + this.User.Name + " - Loginname:" + this.User.LoginName + " hinzugefügt";
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), Beschreibung);

                    //Eintrag der Userberechtigugne
                    //Berechtigung.UserID = this.User.Id;
                    //Berechtigung.SetValueToUserAuth(true);

                    UserAuthorizationsVM.UserAuthorization.UserID = User.Id;
                    UserAuthorizationsVM.SetValueToUserAuth(true);
                    User.UserAuthorization = UserAuthorizationsVM.UserAuthorization;
                }
            }
        }
        /// <summary>
        ///             Update Prozess
        /// </summary>
        public void Update()
        {
            //bool bOK = clsSQLcon.ExecuteSQL(this.sql_Update, BenutzerID);
            bool bOK = clsSQLcon.ExecuteSQLWithTRANSACTION(sql_Update, "UpdateUser", BenutzerID);
            if (bOK)
            {
                //Update der Berechtigungen
                //this.Berechtigung.Update();
                UserAuthorizationsVM.Update();
                Fill();

                UserAuthorizationsVM = new UserAuthorizationsViewData(this._GL_User);
                User.UserAuthorization = UserAuthorizationsVM.UserAuthorization;

                //Add Logbucheintrag update
                string Beschreibung = "User: ID:" + this.User.Id.ToString() + " Name/Loginname:" + this.User.Name + "/" + this.User.LoginName + " geändert";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
            }
        }


        ///-----------------------------------------------------------------------------------------------------
        ///                             STATIC  Function / Procedure
        ///-----------------------------------------------------------------------------------------------------
        ///<summary>UsersViewModel / SetUserFontSize</summary>
        ///<remarks></remarks>
        public static void SetUserFontSize(Globals._GL_USER _GL_User)
        {
            string strSql = string.Empty;
            strSql = "Update [User] SET FontSize='" + _GL_User.us_decFontSize.ToString().Replace(",", ".") + "' " +
                                                     "WHERE ID=" + (int)_GL_User.User_ID + " ;";
            clsSQLcon.ExecuteSQL(strSql, _GL_User.User_ID);
        }


    }
}

