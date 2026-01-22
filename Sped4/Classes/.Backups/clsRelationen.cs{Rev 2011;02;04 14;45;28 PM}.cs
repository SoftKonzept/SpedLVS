using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Windows.Forms;
using Sped4;

namespace Sped4.Classes
{
  class clsRelationen
  {
      //************  User  ***************
      private Int32 _BenutzerID;
      public Int32 BenutzerID
      {
          get { return _BenutzerID; }
          set { _BenutzerID = value;}
      }
      //************************************

        private Int32 _ID;
        private string _Relation;

        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value;}
        }
        public string Relation
        {
            get { return _Relation; }
            set { _Relation = value;}
        }

    //
    //--------------- DataTable Relationsliste ----------------
    //
    public static DataTable GetRelationsliste()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;
        Command.CommandText = "SELECT ID, Relation From Relationen Order By Relation";
        ada.Fill(dt);
        Command.Dispose();
        Globals.SQLcon.Close();
        return dt;
    }
    //
    //
    //
    public void AddRelation()
    {
      try
      {
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        Command.CommandText = "INSERT INTO Relationen " +
                                                 "(Relation) " +
                                             "VALUES ('" + Relation + "')";

        Globals.SQLcon.Open();
        Command.ExecuteNonQuery();
        Command.Dispose();
        Globals.SQLcon.Close();
        if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
        {
          Command.Connection.Close();
        }
      }
      catch (Exception ex)
      {
        System.Windows.Forms.MessageBox.Show(ex.ToString());
      }
    
      //Add Logbucheintrag
      string Beschreibung="Relation: "+Relation + " hinzugefügt";
      Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Eintrag.ToString(), Beschreibung);
    }
    //
    //
    //
    public void UpdateRelation()
    {
      try
      {
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        Command.CommandText = "Update Relationen SET Relation ='"+ Relation + "' "+
                                                    "WHERE ID='"+ID+"'";

        Globals.SQLcon.Open();
        Command.ExecuteNonQuery();
        Command.Dispose();
        Globals.SQLcon.Close();
        if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
        {
          Command.Connection.Close();
        }
      }
      catch (Exception ex)
      {
        System.Windows.Forms.MessageBox.Show(ex.ToString());
      }
      //Add Logbucheintrag
      string Beschreibung = "Relation: " + Relation + " geändert";
      Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
    }
    //
    //----------- Check Verwendung ------------------- 
    //
    public static bool IsRelationUsed(string _Relation)
    {
      bool RelationIsIn=true;
      try
        {
          SqlDataAdapter ada = new SqlDataAdapter();
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          ada.SelectCommand = Command;
          Command.CommandText = "SELECT ID FROM Auftrag WHERE Relation='"+_Relation+"' AND Status<'3' ";    //Stauts 1-2 sind in der Auftragsliste angezeigt
          Globals.SQLcon.Open();
          if (Command.ExecuteScalar()== null)
          {
              RelationIsIn=false;
          }
          else
          { 
                RelationIsIn=true;             
          }      
          Command.Dispose();
          Globals.SQLcon.Close();
          if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
          {
            Command.Connection.Close();
          }
          
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }
        return RelationIsIn;
    }
    //
    //------ löschen Datensatz ------------------ 
    //
    public void DeleteRelation()
    {      
      //Add Logbucheintrag
      Relation = GetRelationByID(ID);
      string Beschreibung = "Relation: " + Relation + " gelöscht";
      Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Loeschung.ToString(), Beschreibung);
    
      //--- initialisierung des sqlcommand---
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      Command.CommandText = "DELETE FROM Relationen WHERE ID='" + ID + "'";
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
    //----------- prüft Matchcode - darf nicht doppelt sein -------------
    //
    public static bool RelationExists(string Relationsname)
    {
        try
        {
            //--- Initialisierung der Connection ------------------
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = ("SELECT ID FROM Relationen WHERE Relation='" + Relationsname + "'");

            Globals.SQLcon.Open();
            SqlDataReader reader = Command.ExecuteReader();

            if (reader.HasRows)
            {
                //MessageBox.Show("Daten vorhanden");
                return true;
            }
            else
            {
                return false;
            }

            reader.Dispose();
            reader.Close();
            Command.Dispose();
        }
        finally
        {
            Globals.SQLcon.Close();
        }
    }
    //
    //
    //
    private string GetRelationByID(Int32 ID)
    {
      string rel = string.Empty;
      try
      {
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;
        Command.CommandText = "SELECT Relation FROM Relationen WHERE ID='" + ID + "'";  
        Globals.SQLcon.Open();
        object obj = Command.ExecuteScalar();
        if (obj != null)
        {
          rel = (string)obj;
        }
        Command.Dispose();
        Globals.SQLcon.Close();
        if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
        {
          Command.Connection.Close();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }
      return rel;
    }
  }
}
