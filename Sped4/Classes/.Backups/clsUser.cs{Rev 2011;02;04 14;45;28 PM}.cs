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
  class clsUser
  {
    //************  User  ***************
    private Int32 _BenutzerID;
    public Int32 BenutzerID
    {
      get { return _BenutzerID; }
      set { _BenutzerID = value; }
    }
    //************************************#



    public DataTable dt = new DataTable("Userberechtigungen");
    internal DataSet ds = new DataSet();


    internal string[,] BerechArray = {
                            {"Stammdaten - Adressen anlegen", "S_ADR_an"},
                            {"Stammdaten - Adressen löschen", "S_ADR_loe"},
                            {"Stammdaten - Adressen ändern", "S_ADR_aen" },
                            {"Stammdaten - Kunden anlegen", "S_KD_an" },
                            {"Stammdaten - Kunden löschen", "S_KD_loe" },
                            {"Stammdaten - Kunden ändern", "S_KD_aen" },
                            {"Stammdaten - Personal anlegen", "S_P_an" },
                            {"Stammdaten - Personal löschen", "S_P_loe" },
                            {"Stammdaten - Personal ändern", "S_P_aen" },
                            {"Stammdaten - Fahrzeuge anlegen", "S_FZ_an" },
                            {"Stammdaten - Fahrzeuge löschen", "S_FZ_loe" },
                            {"Stammdaten - Fahrzeuge ändern", "S_FZ_aen" },
                            {"Stammdaten - Güterart anlegen", "S_GA_an" },
                            {"Stammdaten - Güterart löschen", "S_GA_loe" },
                            {"Stammdaten - Güterart ändern", "S_GA_aen" },
                            {"Stammdaten - Relationen anlegen", "S_R_an" },
                            {"Stammdaten - Relationen löschen", "S_R_loe" },
                            {"Stammdaten - Relationen ändern", "S_R_aen" },
                            {"Auftragserfassung - Aufträge anlegen", "S_AT_an" },
                            {"Auftragserfassung - Aufträge löschen", "S_AT_loe" },
                            {"Auftragserfassung - Aufträge ändern", "S_AT_aen" },
                            {"Auftragserfassung - Aufträge teilen", "S_AT_teil" },
                            {"Disposition - Frachtvergabe an SU", "D_FVSU" },
                            {"Disposition - Auftrag an SU stornieren", "D_ATSUstorno" },
                            {"Disposition - disponieren", "D_dispo" },
                            {"Fakturierung - Liste anzeigen", "FK_Lanzeigen" },
                            {"Fakturierung - Status ändern", "FK_StatusAen" },
                            {"Fakturierung - Frachten berechnen", "FK_Frachten" },
                            {"Fakturierung - RG / GS drucken", "FK_drucken" },
                            {"Lager - Bestand anlegen", "L_LB_an" },
                            {"Lager - Bestand löschen", "L_LB_loe" },
                            {"Lager - Bestand ändern", "L_LB_aen" },
                            {"System - User anlegen", "Sy_User_an" },
                            {"System - User löschen", "Sy_User_loe" },
                            {"System - User ändern", "Sy_User_aen" }
                         };



    /*************************************************************************************+
     *                                Userverwaltung 
     * ************************************************************************************/
    private Int32 _ID;
    private string _Name;
    private string _Vorname;
    private string _pass;
    private string _Initialen;
    private string _LoginName;
    private string _Tel;
    private string _Fax;
    private string _Mail;

    public Int32 ID
    {
      get { return _ID; }
      set { _ID = value; }
    }
    public string Name
    {
      get { return _Name; }
      set { _Name = value; }
    }
    public string pass
    {
      get { return _pass; }
      set { _pass = value; }
    }
    public string Initialen
    {
      get { return _Initialen; }
      set { _Initialen = value; }
    }
    public string LoginName
    {
      get { return _LoginName; }
      set { _LoginName = value; }
    }
    public string Vorname
    {
      get { return _Vorname; }
      set { _Vorname = value; }
    }
    public string Tel
    {
      get { return _Tel; }
      set { _Tel = value; }
    }
    public string Fax
    {
      get { return _Fax; }
      set { _Fax = value; }
    }
    public string Mail
    {
      get { return _Mail; }
      set { _Mail = value; }
    }
    /*****************************************************
     * Berechtigungen
     * **************************************************/

    private bool _ber_ADRanlegen;
    private bool _ber_ADRloeschen;
    private bool _ber_ADRaendern;
    private bool _ber_KDanlegen;
    private bool _ber_KDloeschen;
    private bool _ber_KDaendern;
    private bool _ber_PERanlesen;
    private bool _ber_PERloeschen;
    private bool _ber_PERaendern;
    private bool _ber_FZanlegen;
    private bool _ber_FZloeschen;
    private bool _ber_FZaendern;
    private bool _ber_GAanlegen;
    private bool _ber_GAloeschen;
    private bool _ber_GAaendern;
    private bool _ber_ATanlegen;
    private bool _ber_ATloeschen;
    private bool _ber_ATaendern;
    private bool _ber_DFVanSU;
    private bool _ber_DATanSUstorno;
    private bool _ber_Ddisponieren;
    private bool _ber_FKListeAnzeigen;
    private bool _ber_FKStatusChange;
    private bool _ber_FKFrachtBerechnen;
    private bool _ber_FKdrucken;
    private bool _ber_LBanlegen;
    private bool _ber_LBloeschen;
    private bool _ber_LBaendern;
    private bool _ber_SUserAnlegen;
    private bool _ber_SUserLoeschen;
    private bool _ber_SUserAendern;

    public bool ber_ADRanlegen
    { 
      get { return _ber_ADRanlegen; }
      set { _ber_ADRanlegen = value; } 
    }
    public bool ber_ADRloeschen
    {
      get { return _ber_ADRloeschen; }
      set { _ber_ADRloeschen = value; }
    }
    public bool ber_ADRaendern
    {
      get { return _ber_ADRaendern; }
      set { _ber_ADRaendern = value; }
    }
    public bool ber_KDanlegen
    {
      get { return _ber_KDanlegen; }
      set { _ber_KDanlegen = value; }
    }
    public bool ber_KDloeschen
    {
      get { return _ber_KDloeschen; }
      set { _ber_KDloeschen = value; }
    }
    public bool ber_KDaendern
    {
      get { return _ber_KDaendern; }
      set { _ber_KDaendern = value; }
    }
    public bool ber_PERanlesen
    {
      get { return _ber_PERanlesen; }
      set { _ber_PERanlesen = value; }
    }
    public bool ber_PERloeschen
    {
      get { return _ber_PERloeschen; }
      set { _ber_PERloeschen = value; }
    }
    public bool ber_PERaendern
    {
      get { return _ber_PERaendern; }
      set { _ber_PERaendern = value; }
    }
    public bool ber_FZanlegen
    {
      get { return _ber_FZanlegen; }
      set { _ber_FZanlegen = value; }
    }
    public bool ber_FZloeschen
    {
      get { return _ber_FZloeschen; }
      set { _ber_FZloeschen = value; }
    }
    public bool ber_FZaendern
    {
      get { return _ber_FZaendern; }
      set { _ber_FZaendern = value; }
    }
    public bool ber_GAanlegen
    {
      get { return _ber_GAanlegen; }
      set { _ber_GAanlegen = value; }
    }
    public bool ber_GAloeschen
    {
      get { return _ber_GAloeschen; }
      set { _ber_GAloeschen = value; }
    }
    public bool ber_GAaendern
    {
      get { return _ber_GAaendern; }
      set { _ber_GAaendern = value; }
    }
    public bool ber_ATanlegen
    {
      get { return _ber_ATanlegen; }
      set { _ber_ATanlegen = value; }
    }
    public bool ber_ATloeschen
    {
      get { return _ber_ATloeschen; }
      set { _ber_ATloeschen = value; }
    }
    public bool ber_ATaendern
    {
      get { return _ber_ATaendern; }
      set { _ber_ATaendern = value; }
    }
    public bool ber_DFVanSU
    {
      get { return _ber_DFVanSU; }
      set { _ber_DFVanSU = value; }
    }
    public bool ber_DATanSUstorno
    {
      get { return _ber_DATanSUstorno; }
      set { _ber_DATanSUstorno = value; }
    }
    public bool ber_Ddisponieren
    {
      get { return _ber_Ddisponieren; }
      set { _ber_Ddisponieren = value; }
    }
    public bool ber_FKListeAnzeigen
    {
      get { return _ber_FKListeAnzeigen; }
      set { _ber_FKListeAnzeigen = value; }
    }
    public bool ber_FKStatusChange
    {
      get { return _ber_FKStatusChange; }
      set { _ber_FKStatusChange = value; }
    }
    public bool ber_FKFrachtBerechnen
    {
      get { return _ber_FKFrachtBerechnen; }
      set { _ber_FKFrachtBerechnen = value; }
    }
    public bool ber_FKdrucken
    {
      get { return _ber_FKdrucken; }
      set { _ber_FKdrucken = value; }
    }
    public bool ber_LBanlegen
    {
      get { return _ber_LBanlegen; }
      set { _ber_LBanlegen = value; }
    }
    public bool ber_LBloeschen
    {
      get { return _ber_LBloeschen; }
      set { _ber_LBloeschen = value; }
    }
    public bool ber_LBaendern
    {
      get { return _ber_LBaendern; }
      set { _ber_LBaendern = value; }
    }
    public bool ber_SUserAnlegen
    {
      get { return _ber_SUserAnlegen; }
      set { _ber_SUserAnlegen = value; }
    }
    public bool ber_SUserLoeschen
    {
      get { return _ber_SUserLoeschen; }
      set { _ber_SUserLoeschen = value; }
    }
    public bool ber_SUserAendern
    {
      get { return _ber_SUserAendern; }
      set { _ber_SUserAendern = value; }
    }
   


    /************************************************************************************************
     * 
     *                              Funktionen / Abfragen
     * 
     * *********************************************************************************************/
    //
    //
    //
    public void NewAprovalTable()
    {
      InitColumns();
      InitRows();
    }
    //
    private void InitColumns()
    {
      dt.Clear();

      DataColumn column1 = new DataColumn();
      column1.DataType = System.Type.GetType("System.String");
      column1.AllowDBNull = false;
      column1.Caption = "Berechtigung";
      column1.ColumnName = "Berechtigung";
      column1.DefaultValue = string.Empty;

      dt.Columns.Add(column1);

      DataColumn column2 = new DataColumn();
      column2.DataType = System.Type.GetType("System.Boolean");
      column2.AllowDBNull = false;
      column2.Caption = "Freigabe";
      column2.ColumnName = "Freigabe";
      column2.DefaultValue = false;

      dt.Columns.Add(column2);

      DataColumn column3 = new DataColumn();
      column3.DataType = System.Type.GetType("System.String");
      column3.Caption = "dbCol";
      column3.ColumnName = "dbCol";

      dt.Columns.Add(column3);
    }
    //
    private void InitRows()
    {
      Int32 Count = BerechArray.Length/2;
      for (Int32 i=0; i <= Count-1 ; i++)
      {
        DataRow row = dt.NewRow();
        row["Berechtigung"] = BerechArray[i, 0].ToString();
        row["Freigabe"] = false;
        row["dbCol"] = BerechArray[i, 1].ToString();
        dt.Rows.Add(row);
      }
    }
    //
    //-------------------- Insert Daten ----------------------
    // incl. Check Insert oder Update
    //
    public void AddUserDaten()
    {
      if (ds != null)
      {
        ID = (Int32)ds.Tables["Userdaten"].Rows[0]["ID"];
        Name = (string)ds.Tables["Userdaten"].Rows[0]["Name"];
        LoginName = (string)ds.Tables["Userdaten"].Rows[0]["LoginName"];
        pass = (string)ds.Tables["Userdaten"].Rows[0]["Pass"];
        Initialen = (string)ds.Tables["Userdaten"].Rows[0]["Initialen"];
        Vorname = (string)ds.Tables["Userdaten"].Rows[0]["Vorname"];
        Tel = (string)ds.Tables["Userdaten"].Rows[0]["Tel"];
        Fax = (string)ds.Tables["Userdaten"].Rows[0]["Fax"];
        Mail = (string)ds.Tables["Userdaten"].Rows[0]["Mail"];

        clsUserBerechtigungen ber = new clsUserBerechtigungen();
        ber.BenutzerID = BenutzerID;
        if (ID == 0)
        {

          //neuer Datensatz
          AddNewDatenToUser();
          //ID auslesen
          ds.Tables["Userdaten"].Rows[0]["ID"] = GetAddedUserID();
          //Berechtigungen
          ber.GetBerechtigungen(ds, true);

        }
        else
        {
          //update Datensatz
          UpdateUser();
          //update Berechtigungen
          ber.GetBerechtigungen(ds, false);
        }
      }
    }
    //
    //---------- ID des eingetragenen Datensatzes auslesen ----------------
    //
    private Int32 GetAddedUserID()
    {
      Int32 iID = 0;
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      Command.CommandText = "Select ID FROM [User] WHERE "+
                                                      "Name='" + Name + "' AND "+
                                                      "LoginName='"+LoginName+"' AND "+
                                                      "Pass='"+pass+"' AND "+
                                                      "Initialen='"+Initialen+"' AND "+
                                                      "Vorname='" + Vorname + "' AND " +
                                                      "Tel='" + Tel + "' AND " +
                                                      "Fax='" + Fax + "' AND " +
                                                      "Mail='" + Mail + "'";
      Globals.SQLcon.Open();
      object obj = Command.ExecuteScalar();
      if (obj is DBNull)
      {
        iID = 0;
      }
      else
      {
        iID = (Int32)obj;
      }
      Command.Dispose();
      Globals.SQLcon.Close();

      return iID;
    }
    //
    //------------------ Insert neuen Datensatz in DB User -----------------
    //
    private void AddNewDatenToUser()
    { 
      try
      {
        //--- initialisierung des sqlcommand---
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;

        //----- SQL Abfrage -----------------------
        Command.CommandText = "INSERT INTO [User] (Name, "+
                                                  "LoginName, "+
                                                  "Pass, "+
                                                  "Initialen, "+
                                                  "Vorname, "+
                                                  "Tel, "+
                                                  "Fax, "+
                                                  "Mail) " +
                                        "VALUES ('" + Name + "','"
                                                    + LoginName + "','"
                                                    + pass + "','"
                                                    + Initialen + "','"
                                                    + Vorname + "','"
                                                    + Tel + "','"
                                                    + Fax + "','"
                                                    + Mail +"')";

        Globals.SQLcon.Open();
        Command.ExecuteNonQuery();
        Command.Dispose();
        Globals.SQLcon.Close();
      }
      catch (Exception ex)
      {
        //System.Windows.Forms.MessageBox.Show(ex.ToString());
        //Add Logbucheintrag Exception
        string BeschreibungEx = "Exception: " + ex;
        Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Exception.ToString(), BeschreibungEx);
      }
      finally
      {
        //Add Logbucheintrag Eintrag
        string Beschreibung = "User: Name:" + Name + " - Loginname:" + LoginName + " hinzugefügt";
        Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Eintrag.ToString(), Beschreibung);
      }
    }
    //
    //--------------------------- Update Datensatz ---------------------------
    //
    private void UpdateUser()
    {
      try
      {
        //--- initialisierung des sqlcommand---
        SqlCommand UpCommand = new SqlCommand();
        UpCommand.Connection = Globals.SQLcon.Connection;

        UpCommand.CommandText = "Update [User] SET Name='" + Name + "', " +
                                                "LoginName='" + LoginName + "', " +
                                                "Pass='" + pass + "', " +
                                                "Initialen='" + Initialen + "', " +
                                                "Vorname='" + Vorname + "', " +
                                                "Tel='" + Tel + "', " +
                                                "Fax='" + Fax + "', " +
                                                "Mail='"+Mail+"'" +
                                                "WHERE ID='" + ID + "'";

        Globals.SQLcon.Open();
        UpCommand.ExecuteNonQuery();
        UpCommand.Dispose();
        Globals.SQLcon.Close();
      }
      catch (Exception ex)
      {
        //System.Windows.Forms.MessageBox.Show(ex.ToString());
        //Add Logbucheintrag Exception
        string BeschreibungEx = "Exception: " + ex;
        Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Exception.ToString(), BeschreibungEx);
      }
      finally
      {
        //Add Logbucheintrag update
        string Beschreibung = "User: ID:" + ID + " Name/Loginname:" + Name + "/"+LoginName+" geändert";
        Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
      }
    }
    //
    //
    //
    public bool CheckLogin(string LoginName, string Pass)
    { 
      bool LoginOK=false;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT ID FROM [USER] WHERE LoginName='"+LoginName+"' AND Pass='"+Pass+"'";
      Globals.SQLcon.Open();
      
      object obj = Command.ExecuteScalar();
      if (obj != null)
      {
          ID = (Int32)obj;
          LoginOK=true;

          //LOGIN eintrag 
          clsLogin lg = new clsLogin();
          lg.BenutzerID = ID;
          lg.Login();
      }
      else
      {
        LoginOK=false;
      }      
      Command.Dispose();
      Globals.SQLcon.Close();
      return LoginOK;
    }
    //
    //------------------- GET USER DATEN ----------------------
    //
    public static DataTable GetUserDaten(Int32 id)
    {
      DataTable dataTable = new DataTable();
      dataTable.Clear();
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();

      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT * FROM [User] WHERE ID='"+id+"'";

      ada.Fill(dataTable);
      ada.Dispose();
      Command.Dispose();
      Globals.SQLcon.Close();
      return dataTable;
    }
    //
    //------------------- GET Userlist ----------------------
    //
    public static DataTable GetUserList()
    {
      DataTable dataTable = new DataTable();
      dataTable.Clear();
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();

      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT ID, LoginName  FROM [User]";

      ada.Fill(dataTable);
      ada.Dispose();
      Command.Dispose();
      Globals.SQLcon.Close();
      return dataTable;
    }
    //
    //--------------- GET ID BY LOGIN and PASS -------
    //
    public static Int32 GetUserIDByLoginNameAndPass(string LoginName, string Pass)
    {
      Int32 id = 0;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT ID FROM [USER] WHERE LoginName='" + LoginName + "' AND Pass='" + Pass + "'";
      Globals.SQLcon.Open();

      object obj = Command.ExecuteScalar();
      if (obj != null)
      {
        id = (Int32)obj;
      }
      else
      {
        id = 0;
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      return id;
    }
    //
    //------------- Get Name mit ID -------------------------
    //
    public static string GetBenutzerNameByID(Int32 BenID)
    {
      string name = string.Empty;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT Name FROM [User] WHERE ID='" + BenID + "'";
      Globals.SQLcon.Open();

      object obj = Command.ExecuteScalar();
      if (obj != null)
      {
        name = (string)obj;
      }
      else
      {
        name = string.Empty;
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      return name;
    }
    //
    //----- Check ob Username schon vorhanden ----------------
    //
    public static bool CheckLoginNameIsUsed(string LoginName)
    {
      bool LoginOK = false;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT ID FROM [USER] WHERE LoginName='" + LoginName + "'";
      Globals.SQLcon.Open();

      object obj = Command.ExecuteScalar();
      if (obj != null)
      {
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
    //----- Check ob User ID ist schon vorhanden ----------------
    //
    public static bool CheckUserIDIsUsed(Int32 userID)
    {
      bool IsUsed = false;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT Name FROM [USER] WHERE ID='" + userID + "'";
      Globals.SQLcon.Open();

      object obj = Command.ExecuteScalar();
      if (obj != null)
      {
        IsUsed = true;
      }
      else
      {
        IsUsed = false;
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      return IsUsed;
    }
    //
    //-------------- DELETE USer ---------------
    //
    public void DeleteUser()
    {
      if (ID > 0)
      {
        //Add Logbucheintrag Löschen
        string Benutzername = GetBenutzerNameByID(ID);
        string Beschreibung = "User: ID:" + ID + " Name: " + Benutzername + " gelöscht";
        Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Loeschung.ToString(), Beschreibung);

        //--- initialisierung des sqlcommand---
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        Command.CommandText = "DELETE FROM [User] WHERE ID='" + ID + "'";
        Globals.SQLcon.Open();
        Command.ExecuteNonQuery();
        Command.Dispose();
        Globals.SQLcon.Close();
        if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
        {
          Command.Connection.Close();
        }
        DeleteUserFromAuftragRead(ID);
      }
    }
    //
    private void DeleteUserFromAuftragRead(Int32 iID)
    {
      clsAuftragRead read = new clsAuftragRead();
      read.DeleteReadAuftragByUser(iID);
    }
  }
}
