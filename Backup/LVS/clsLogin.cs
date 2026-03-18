using System;
using System.Data;
using System.Data.SqlClient;


namespace LVS
{
    public class clsLogin
    {
        private decimal _BenutzerID;

        public decimal BenutzerID
        {
            get { return _BenutzerID; }
            set { _BenutzerID = value; }
        }

        /*************************************************************+
         * 
         * 
         * ************************************************************/
        //
        //
        //
        public void Login()
        {
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;

                //----- SQL Abfrage -----------------------
                Command.CommandText = "INSERT INTO Login (BenutzerID, " +
                                                            "Datum) " +
                                                "VALUES ('" + BenutzerID + "','"
                                                            + DateTime.Now + "')";

                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());

            }
            Logbuch_Login();
        }
        //
        //
        //
        public void Logout()
        {
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;

                //----- SQL Abfrage -----------------------
                Command.CommandText = "DELETE FROM Login WHERE BenutzerID='" + BenutzerID + "'";

                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());

            }
            Logbuch_Logout();
        }
        //
        private void Logbuch_Login()
        {
            string Beschreibung = "Login Sped4";
            Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Login.ToString(), Beschreibung);
        }
        //
        private void Logbuch_Logout()
        {
            string Beschreibung = "Logout Sped4";
            Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Logout.ToString(), Beschreibung);
        }
        //
        public static void Logbuch_LoginFehlversuch(Int32 Versuchsanzahl)
        {
            decimal BenutzerID = 0;
            string Beschreibung = Versuchsanzahl.ToString() + ". Fehlversuche Login";
            Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.LoginFehlversuch.ToString(), Beschreibung);
        }
        //
        //--------------- CHeck ob User eingeloggt -------------
        //
        public static bool CheckUserIsLoggedIn(decimal userID)
        {
            bool LoggedIn = false;
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT ID FROM Login WHERE BenutzerID='" + userID + "'";
            Globals.SQLcon.Open();

            object obj = Command.ExecuteScalar();
            if (obj != null)
            {
                LoggedIn = true;
            }
            else
            {
                LoggedIn = false;
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            return LoggedIn;
        }
        //
        //
        //
        public static void DeleteOldLogin()
        {
            DateTime dtToday = DateTime.Today.Date;
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = "DELETE FROM Login WHERE Datum<'" + dtToday + "'";
            Globals.SQLcon.Open();
            Command.ExecuteNonQuery();
            Command.Dispose();
            Globals.SQLcon.Close();
            if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
            {
                Command.Connection.Close();
            }
        }
        //
        //------------------- GET login ----------------------
        //
        public static DataTable GetLoginDaten()
        {
            DataTable dataTable = new DataTable("Login");
            dataTable.Clear();
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();

            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT LoginName, " +
                                          "Name, " +
                                          "Vorname " +
                                          "FROM [LOGIN] " +
                                          "JOIN [USER] ON Login.BenutzerID=[User].ID";

            ada.Fill(dataTable);
            ada.Dispose();
            Command.Dispose();
            Globals.SQLcon.Close();
            return dataTable;
        }

        //
        //------------------- GET login ----------------------
        //
        public static Int32 GetCountLoggedInUser(Globals._GL_USER myGLUser)
        {
            string strSQL = "SELECT DISTINCT COUNT(l.ID) as UserAnzahl " +
                                                "FROM Login l " +
                                                "INNER JOIN [User] u ON u.ID=l.BenutzerID ";

            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, myGLUser.User_ID);
            Int32 iTmp = 0;
            Int32.TryParse(strTmp, out iTmp);
            return iTmp;
        }
    }
}
