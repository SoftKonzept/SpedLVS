using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace Sped4.Classes
{
  class clsLogin
  {
    private Int32 _BenutzerID;

    public Int32 BenutzerID
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
      string Beschreibung = "Login Sped3";
      Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Login.ToString(), Beschreibung);
    }
    //
    private void Logbuch_Logout()
    {
      string Beschreibung = "Logout Sped3";
      Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Logout.ToString(), Beschreibung);
    }
    //
    public static void Logbuch_LoginFehlversuch(Int32 Versuchsanzahl)
    {
      Int32 BenutzerID = 0;
      string Beschreibung =Versuchsanzahl.ToString()+ ". Fehlversuche Login";
      Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.LoginFehlversuch.ToString(), Beschreibung);
    }
    //
    //--------------- CHeck ob User eingeloggt -------------
    //
    public static bool CheckUserIsLoggedIn(Int32 userID)
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
  }
}
