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
  class clsLogbuch
  {
    private Int32 _ID;
    private Int32 _BenutzerID;
    private string _BenutzerName;
    private string _Aktion;
    private string _Beschreibung;

    public Int32 ID
    {
      get { return _ID; }
      set { _ID = value; }
    }
    public Int32 BenutzerID
    {
      get { return _BenutzerID; }
      set { _BenutzerID = value; }
    }
    public string BenutzerName
    {
      get 
      {
        _BenutzerName = clsUser.GetBenutzerNameByID(BenutzerID);
        return _BenutzerName; }
      set { _BenutzerName = value; }
    }
    public string Aktion
    {
      get { return _Aktion; }
      set { _Aktion = value; }
    }
    public string Beschreibung
    {
      get { return _Beschreibung; }
      set { _Beschreibung = value; }
    }
    

    /***************************************************************************************
     * 
     * 
     * *************************************************************************************/
    //
    //
    //
    public void LogbuchInsert()
    {
      if (BenutzerID > 0)
      {
        try
        {
          //--- initialisierung des sqlcommand---
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;

          //----- SQL Abfrage -----------------------
          Command.CommandText = "INSERT INTO Logbuch (BenutzerID, " +
                                                      "BenutzerName, " +
                                                      "Datum, " +
                                                      "Aktion, " +
                                                      "Beschreibung) " +
                                          "VALUES ('" + BenutzerID + "','"
                                                      + BenutzerName + "', '"
                                                      + DateTime.Now + "','"
                                                      + Aktion + "','"
                                                      + Beschreibung + "')";

          Globals.SQLcon.Open();
          Command.ExecuteNonQuery();
          Command.Dispose();
          Globals.SQLcon.Close();
        }
        catch (Exception ex)
        {
          System.Windows.Forms.MessageBox.Show(ex.ToString());

        }
      }
    }
    //
    //----------- Read Logbuch komplett ---------------------------
    //
    public static DataTable GetLogbuch()
    {
      DataTable dt = new DataTable("Logbuch");
      dt.Clear();
      try
      {
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;
        Command.CommandText = "SELECT * FROM Logbuch ORDER BY Datum Desc";

        ada.Fill(dt);
        Command.Dispose();
        Globals.SQLcon.Close();
      }
      catch (Exception ex)
      { 
      
      }
      return dt;
    }
  }
}
