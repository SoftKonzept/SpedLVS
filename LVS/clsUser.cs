using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace LVS
{
    public class clsUser
    {
        public Globals._GL_USER _GL_User;
        public clsUserBerechtigungen Berechtigung = new clsUserBerechtigungen();
        public clsArbeitsbereichUser ArbeitsbereichAccess = new clsArbeitsbereichUser();

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
        //***********************************

        public DataTable dt = new DataTable("Userberechtigungen");
        public DataSet ds = new DataSet();
        public decimal decBenutzer = -1;


        public const decimal Default_FontSize = 7.5M;
        public const Int32 Default_SMTPPort = 25;



        /*************************************************************************************+
         *                                Userverwaltung 
         * ************************************************************************************/
        public decimal ID { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }
        public string pass { get; set; }
        public string Initialen { get; set; }
        public string LoginName { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Mail { get; set; }
        public decimal FontSize { get; set; }
        public DateTime dtDispoVon { get; set; }
        public DateTime dtDispoBis { get; set; }
        public string SMTPServer { get; set; }
        public Int32 SMTPPort { get; set; }
        public string SMTPUser { get; set; }
        public string SMTPPasswort { get; set; }
        public bool SMTPSSL { get; set; }
        public bool IsAdmin { get; set; }

        public List<decimal> ListArbeitsbereichAccess { get; set; }
        /****************************************************************************************************************
         * 
         * **************************************************************************************************************/

        public clsUser()
        {

        }
        public clsUser(int myUserId) : this()
        {
            this.ID = myUserId;
            if (this.ID > 0)
            {
                this._GL_User = Fill();
            }
        }

        ///<summary>clsUser / SetUserFontSize</summary>
        ///<remarks></remarks>
        public static void SetUserFontSize(Globals._GL_USER _GL_User)
        {
            string strSql = string.Empty;
            strSql = "Update [User] SET FontSize='" + _GL_User.us_decFontSize.ToString().Replace(",", ".") + "' " +
                                                     "WHERE ID=" + (int)_GL_User.User_ID + " ;";
            clsSQLcon.ExecuteSQL(strSql, _GL_User.User_ID);
        }
        ///<summary>clsUser / AddUserDaten</summary>
        ///<remarks></remarks>
        public void AddUserDaten()
        {
            if (ID == 0)
            {
                //neuer Datensatz
                AddNewDatenToUser();
            }
        }
        ///<summary>clsUser / AddNewDatenToUser</summary>
        ///<remarks></remarks>
        private void AddNewDatenToUser()
        {
            string strSQL = "INSERT INTO [User] (Name, LoginName, Pass, Initialen, Vorname, Tel, Fax, Mail, " +
                                                "SMTPUser, SMTPPass, SMTPServer, SMTPPort, IsAdmin) " +
                                              "VALUES ('" + Name + "'" +
                                                        ", '" + LoginName + "'" +
                                                        ", '" + pass + "'" +
                                                        ", '" + Initialen + "'" +
                                                        ", '" + Vorname + "'" +
                                                        ", '" + Tel + "'" +
                                                        ", '" + Fax + "'" +
                                                        ", '" + Mail + "'" +
                                                        ", '" + SMTPUser + "'" +
                                                        ", '" + SMTPPasswort + "'" +
                                                        ", '" + SMTPServer + "'" +
                                                        ", " + SMTPPort +
                                                        ", " + Convert.ToInt32(IsAdmin) +
                                                        ")";
            strSQL = strSQL + "Select @@IDENTITY as 'ID' ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                this.ID = decTmp;
                //Add Logbucheintrag Eintrag
                string Beschreibung = "User: Name:" + Name + " - Loginname:" + LoginName + " hinzugefügt";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), Beschreibung);

                //Eintrag der Userberechtigugne
                Berechtigung.UserID = this.ID;
                Berechtigung.SetValueToUserAuth(true);
            }
        }
        ///<summary>clsUser / Update</summary>
        ///<remarks>Ändern Userdatensatz</remarks>
        public void Update()
        {
            string strSQL = "Update [User] SET Name='" + Name + "' " +
                                              ", LoginName='" + LoginName + "' " +
                                              ", Pass='" + pass + "' " +
                                              ", Initialen='" + Initialen + "' " +
                                              ", Vorname='" + Vorname + "' " +
                                              ", Tel='" + Tel + "' " +
                                              ", Fax='" + Fax + "' " +
                                              ", Mail='" + Mail + "' " +
                                              ", SMTPUser='" + SMTPUser + "' " +
                                              ", SMTPPass='" + SMTPPasswort + "' " +
                                              ", SMTPServer='" + SMTPServer + "' " +
                                              ", SMTPPort=" + SMTPPort +
                                              ", IsAdmin = " + Convert.ToInt32(IsAdmin) +
                                                      " WHERE ID=" + (int)ID;

            bool bOK = clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
            if (bOK)
            {
                //Update der Berechtigungen
                this.Berechtigung.Update();
                //Add Logbucheintrag update
                string Beschreibung = "User: ID:" + ID + " Name/Loginname:" + Name + "/" + LoginName + " geändert";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
            }
        }
        ///<summary>clsUser / Fill</summary>
        ///<remarks></remarks>
        public Globals._GL_USER Fill()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM [User] WHERE ID=" + (int)ID + " ";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "USER");

            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                this.ID = (decimal)dt.Rows[i]["ID"];
                this.Name = dt.Rows[i]["Name"].ToString();
                this.Vorname = dt.Rows[i]["Vorname"].ToString();
                this.pass = dt.Rows[i]["pass"].ToString();
                this.Initialen = dt.Rows[i]["Initialen"].ToString();
                this.LoginName = dt.Rows[i]["LoginName"].ToString();
                this.Tel = dt.Rows[i]["Tel"].ToString();
                this.Fax = dt.Rows[i]["Fax"].ToString();
                this.Mail = dt.Rows[i]["Mail"].ToString();
                if (dt.Rows[i]["FontSize"] != DBNull.Value)
                {
                    this.FontSize = (decimal)dt.Rows[i]["FontSize"];
                }
                else
                {
                    this.FontSize = clsUser.Default_FontSize;
                }
                if (dt.Rows[i]["dtDispoVon"] != DBNull.Value)
                {
                    this.dtDispoVon = (DateTime)dt.Rows[i]["dtDispoVon"];
                }
                else
                {
                    this.dtDispoVon = DateTime.Now.Date;
                }
                if (dt.Rows[i]["dtDispoVon"] != DBNull.Value)
                {
                    this.dtDispoBis = (DateTime)dt.Rows[i]["dtDispoBis"];
                }
                else
                {
                    this.dtDispoBis = DateTime.Now.Date.AddDays(2);
                }
                this.SMTPUser = dt.Rows[i]["SMTPUser"].ToString();
                this.SMTPPasswort = dt.Rows[i]["SMTPPass"].ToString();
                this.SMTPServer = dt.Rows[i]["SMTPServer"].ToString();
                Int32 iTmp = clsSystem.const_Mail_SMTPPort;
                Int32.TryParse(dt.Rows[i]["SMTPPort"].ToString(), out iTmp);
                this.SMTPPort = iTmp;
                this.IsAdmin = (bool)dt.Rows[i]["IsAdmin"];

                //GLobal User setzen
                _GL_User.User_ID = this.ID;
                _GL_User.Name = this.Name;
                _GL_User.LoginName = this.LoginName;
                _GL_User.initialen = this.Initialen;
                _GL_User.Vorname = this.Vorname;
                _GL_User.Telefon = this.Tel;
                _GL_User.Fax = this.Fax;
                _GL_User.Mail = this.Mail;
                _GL_User.SMTPUser = this.SMTPUser;
                _GL_User.SMTPPass = this.SMTPPasswort;
                _GL_User.SMTPServer = this.SMTPServer;
                _GL_User.SMTPPort = this.SMTPPort;
                _GL_User.IsAdmin = this.IsAdmin;

                _GL_User.us_dtDispoVon = this.dtDispoVon;
                _GL_User.us_dtDispoBis = this.dtDispoBis;
                _GL_User.us_decFontSize = this.FontSize;


                //Berechtigungen setzen
                Berechtigung = new clsUserBerechtigungen();
                Berechtigung._GL_User = this._GL_User;
                Berechtigung.UserID = this.ID;
                Berechtigung.FillByUserID();

                _GL_User.Menu_System = ((this.Berechtigung.read_User) ||
                                        (this.Berechtigung.write_User) ||
                                        (this.Berechtigung.read_Arbeitsbereich) ||
                                        (this.Berechtigung.write_Arbeitsbereich) ||
                                        (this.Berechtigung.read_Mandant) ||
                                        (this.Berechtigung.write_Mandant));

                _GL_User.Menu_Auftragserfassung = ((this.Berechtigung.read_Order) ||
                                                    (this.Berechtigung.write_Order));

                _GL_User.Menu_Disposition = ((this.Berechtigung.read_Disposition) ||
                                             (this.Berechtigung.write_Disposition));

                _GL_User.Menu_Lager = ((this.Berechtigung.read_Bestand) ||
                                       (this.Berechtigung.read_LagerEingang) ||
                                       (this.Berechtigung.write_LagerEingang) ||
                                       (this.Berechtigung.read_LagerAusgang) ||
                                       (this.Berechtigung.write_LagerAusgang));

                _GL_User.Menu_Statistik = (this.Berechtigung.read_Statistik);

                _GL_User.Menue_Fakturierung = ((this.Berechtigung.read_FaktLager) ||
                                               (this.Berechtigung.write_FaktLager) ||
                                               (this.Berechtigung.read_FaktSpedition) ||
                                               (this.Berechtigung.write_FaktSpedition));

                _GL_User.Menu_Stammdaten = ((this.Berechtigung.read_ADR) ||
                                            (this.Berechtigung.write_ADR) ||
                                            (this.Berechtigung.read_Kunde) ||
                                            (this.Berechtigung.write_Kunde) ||
                                            (this.Berechtigung.read_Personal) ||
                                            (this.Berechtigung.write_Personal) ||
                                            (this.Berechtigung.read_KFZ) ||
                                            (this.Berechtigung.write_KFZ) ||
                                            (this.Berechtigung.read_Gut) ||
                                            (this.Berechtigung.write_Gut) ||
                                            (this.Berechtigung.read_Relation) ||
                                            (this.Berechtigung.write_Relation));

                _GL_User.read_ADR = Berechtigung.read_ADR;
                _GL_User.write_ADR = Berechtigung.write_ADR;
                _GL_User.read_Kunde = Berechtigung.read_Kunde;
                _GL_User.write_Kunde = Berechtigung.write_Kunde;
                _GL_User.read_Personal = Berechtigung.read_Personal;
                _GL_User.write_Personal = Berechtigung.write_Personal;
                _GL_User.read_KFZ = Berechtigung.read_KFZ;
                _GL_User.write_KFZ = Berechtigung.write_KFZ;
                _GL_User.read_Gut = Berechtigung.read_Gut;
                _GL_User.write_Gut = Berechtigung.write_Gut;
                _GL_User.read_Relation = Berechtigung.read_Relation;
                _GL_User.write_Relation = Berechtigung.write_Relation;
                _GL_User.read_Order = Berechtigung.read_Order;
                _GL_User.write_Order = Berechtigung.write_Order;
                _GL_User.write_TransportOrder = Berechtigung.write_TransportOrder;
                _GL_User.read_TransportOrder = Berechtigung.read_TransportOrder;
                _GL_User.write_Disposition = Berechtigung.write_Disposition;
                _GL_User.read_Disposition = Berechtigung.read_Disposition;
                _GL_User.read_FaktLager = Berechtigung.read_FaktLager;
                _GL_User.write_FaktLager = Berechtigung.write_FaktLager;
                _GL_User.read_FaktSpedition = Berechtigung.read_FaktSpedition;
                _GL_User.write_FaktSpedition = Berechtigung.write_FaktSpedition;
                _GL_User.read_Bestand = Berechtigung.read_Bestand;
                _GL_User.read_LagerEingang = Berechtigung.read_LagerEingang;
                _GL_User.write_LagerEingang = Berechtigung.write_LagerEingang;
                _GL_User.read_LagerAusgang = Berechtigung.read_LagerAusgang;
                _GL_User.write_LagerAusgang = Berechtigung.write_LagerAusgang;
                _GL_User.read_User = Berechtigung.read_User;
                _GL_User.write_User = Berechtigung.write_User;
                _GL_User.read_Arbeitsbereich = Berechtigung.read_Arbeitsbereich;
                _GL_User.write_Arbeitsbereich = Berechtigung.write_Arbeitsbereich;
                _GL_User.read_Mandant = Berechtigung.read_Mandant;
                _GL_User.write_Mandant = Berechtigung.write_Mandant;
                _GL_User.read_Statistik = Berechtigung.read_Statistik;
                _GL_User.read_Einheit = Berechtigung.read_Einheit;
                _GL_User.write_Einheit = Berechtigung.write_Einheit;
                _GL_User.read_Schaden = Berechtigung.read_Schaden;
                _GL_User.write_Schaden = Berechtigung.write_Schaden;
                _GL_User.read_LagerOrt = Berechtigung.read_LagerOrt;
                _GL_User.write_LagerOrt = Berechtigung.write_LagerOrt;
                _GL_User.read_ASNTransfer = Berechtigung.read_ASNTransfer;
                _GL_User.write_ASNTransfer = Berechtigung.write_ASNTransfer;
                _GL_User.read_FaktExtraCharge = Berechtigung.read_FaktExtraCharge;
                _GL_User.write_FaktExtraCharge = Berechtigung.write_FaktExtraCharge;
                _GL_User.write_FibuExport = false;  //Baustelle     
                _GL_User.access_StKV = Berechtigung.access_StKV;

                _GL_User.read_Inventory = Berechtigung.read_Inventory;
                _GL_User.write_Inventory = Berechtigung.write_Inventory;

                //Arbeitsbereiche
                ArbeitsbereichAccess = new clsArbeitsbereichUser();
                ArbeitsbereichAccess.InitClass(this._GL_User);
                this.ListArbeitsbereichAccess = new List<decimal>();
                this.ListArbeitsbereichAccess = ArbeitsbereichAccess.ListArbeitsbereichAccess;

            }
            return this._GL_User;
        }
        ///<summary>clsUser / Fill</summary>
        ///<remarks>Folgenden Table werden über die MSSQL - Beziehung der Tabelle gelöscht
        ///         - UserBerechtiungen</remarks>
        public void DeleteUser()
        {
            if (ID > 0)
            {
                //Add Logbucheintrag Löschen
                string Benutzername = GetBenutzerNameByID(ID);
                string Beschreibung = "User: ID:" + ID + " Name: " + Benutzername + " gelöscht";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), Beschreibung);

                string strSQL = string.Empty;
                strSQL = "DELETE FROM [User] WHERE ID=" + (int)ID + " ;";
                clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
                //Tabelle AuftragRead aktuallisieren - Baustelle über DB - Beziehungen löschen lassen
                DeleteUserFromAuftragRead(ID);
            }
        }
        ///<summary>clsUser / GetUserList</summary>
        ///<remarks></remarks>
        public static DataTable GetUserList(Globals._GL_USER myGLUser)
        {
            string strSQL = "SELECT ID, LoginName  FROM [User]";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "UserList");
            return dt;
        }
        //
        //
        //
        public bool CheckLogin(string LoginName, string Pass)
        {
            bool LoginOK = false;
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT ID FROM [USER] WHERE LoginName='" + LoginName + "' AND Pass='" + Pass + "'";
            Globals.SQLcon.Open();

            object obj = Command.ExecuteScalar();
            if (obj != null)
            {
                ID = (decimal)obj;
                LoginOK = true;
            }
            else
            {
                LoginOK = false;
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            return LoginOK;
        }



        //
        //--------------- GET ID BY LOGIN and PASS -------
        //
        public static decimal GetUserIDByLoginNameAndPass(string LoginName, string Pass)
        {
            decimal id = 0;
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT ID FROM [USER] WHERE LoginName='" + LoginName + "' AND Pass='" + Pass + "'";
            Globals.SQLcon.Open();

            object obj = Command.ExecuteScalar();
            if (obj != null)
            {
                id = (decimal)obj;
            }
            else
            {
                id = 0;
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            return id;
        }

        public void CreateAdminUser()
        {
            this.BenutzerID = 0;
            this.Name = "Administrator";
            this.LoginName = "Admin";
            this.pass = "lvs@comtec";
            this.Initialen = "admin";
            this.Vorname = string.Empty;
            this.Tel = string.Empty;
            this.Fax = string.Empty;
            this.Mail = clsSystem.const_MailAdress;
            this.SMTPUser = clsSystem.const_Mail_SMTPUser;
            this.SMTPPasswort = clsSystem.const_Mail_SMTPPasswort;
            this.SMTPServer = clsSystem.const_Mail_SMTPServer;
            this.SMTPPort = clsSystem.const_Mail_SMTPPort;
            AddNewDatenToUser();
            Fill();
            this.Berechtigung.CreateAdminAuth();
        }
        //
        //------------- Get Name mit ID -------------------------
        //
        public static string GetBenutzerNameByID(decimal BenID)
        {
            string strName = string.Empty;
            if (BenID > 0)
            {
                string strSQL = string.Empty;
                strSQL = "SELECT Name FROM [User] WHERE ID=" + (int)BenID + " ;";
                if (BenID > 0)
                {
                    strName = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenID);
                }
            }
            return strName;
        }


        /// <summary>
        /// GetBenutzerFullNameByID
        /// </summary>
        /// <param name="BenID">Die ID des auszugebenen Benutzers</param>
        /// <returns></returns>
        public static string GetBenutzerFullNameByID(decimal BenID)
        {
            string name = string.Empty;
            string strSQL = string.Empty;
            strSQL = "SELECT Vorname,Name FROM [User] WHERE ID='" + BenID + "'";
            DataTable user = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenID, "User");
            if (user.Rows.Count == 1)
            {
                name = user.Rows[0]["Vorname"].ToString() + " " + user.Rows[0]["Name"].ToString();
            }
            return name;
        }
        //
        //----- Check ob Username schon vorhanden ----------------
        //
        public static bool CheckLoginNameIsUsed(string LoginName)
        {
            string strSQL = string.Empty;
            strSQL = "SELECT ID FROM [USER] WHERE LoginName='" + LoginName + "'";
            return clsSQLcon.ExecuteSQL_GetValueBool(strSQL, -1);
        }
        //
        //----- Check ob User ID ist schon vorhanden ----------------
        //
        public static bool CheckUserIDIsUsed(decimal userID)
        {
            string strSQL = string.Empty;
            strSQL = "SELECT Name FROM [USER] WHERE ID='" + userID + "'";
            return clsSQLcon.ExecuteSQL_GetValueBool(strSQL, userID);
        }

        //
        private void DeleteUserFromAuftragRead(decimal iID)
        {
            clsAuftragRead read = new clsAuftragRead();
            read.DeleteReadAuftragByUser(iID);
        }

        /// <summary>clsUser / CheckUserIsComTECAdmin</summary>
        public static bool CheckUserIsComTECAdmin(Globals._GL_USER myGLUserAdmin)
        {
            bool bReturn = false;
            if (
                (myGLUserAdmin.LoginName.ToString().ToUpper() == "ADMINISTRATOR")
                ||
                (myGLUserAdmin.LoginName.ToString().ToUpper() == "ADMIN")
                )
            {
                bReturn = true;
            }
            return bReturn;
        }
    }
}
