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
    class clsGut
    {
      //************  User  ***************
      private Int32 _BenutzerID;
      public Int32 BenutzerID
      {
        get { return _BenutzerID; }
        set { _BenutzerID = value; }
      }
      //************************************


        private int _ID;
        private string _ViewID;
        private int _Gut_ID;
        private int _ME;
        private string _Bezeichnung;
        private decimal _Gewicht;
        private decimal _Di;            // Dicke
        //private decimal _Br;            // Breite
        private decimal _Ho;            // Höhe
        private decimal _La;            // Länge

        public int ID
        {
            get { return _ID; }
            set { _ID = value;}
    
        }
        public string ViewID
        {
            get { return _ViewID; }
            set { _ViewID = value; }

        }
        public int Gut_ID
        {
            get{ return _Gut_ID; }
            set{ _Gut_ID = value; }

        }
        public int ME
        {
            get { return _ME; }
            set { _ME = value; }
        }
        public string Bezeichnung
        {
            get { return _Bezeichnung; }
            set { _Bezeichnung = value; }

        }
        public decimal Gewicht
        {
            get { return _Gewicht; }
            set { _Gewicht = value; }

        }
        public decimal Di
        {
            get { return _Di; }
            set { _Di = value; }

        }
        public decimal Ho
        {
            get { return _Ho; }
            set {  _Ho = value; }

        }

        public decimal La
        {
            get { return _La; }
            set { _La = value; }

        }

        //************************************************************************************
        //--------                 Methoden
        //************************************************************************************
        //
        //-------------------- Güterarten -------------------------------
        //
        public static DataTable GueterArtTable()
        {
            DataTable gueterartTable = new DataTable();
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT " +
                                    "ID, ViewID as 'Suchbegriff', Bezeichnung " +
                                        "FROM Gueterart ORDER BY Suchbegriff";
            ada.Fill(gueterartTable);
            Command.Dispose();
            Globals.SQLcon.Close();
            return gueterartTable;
        }
      //
      //
      //
        public static DataSet GueterArtDS()
        {
          DataSet ds = new DataSet();
          SqlDataAdapter ada = new SqlDataAdapter();
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          ada.SelectCommand = Command;
          Command.CommandText = "SELECT " +
                                  "ID, ViewID as 'Suchbegriff', Bezeichnung " +
                                      "FROM Gueterart ORDER BY Suchbegriff";
          ada.Fill(ds);
          Command.Dispose();
          Globals.SQLcon.Close();
          return ds;
        }
      //
      //
      //
        public static DataSet GueterArtDataSet()
        {
          DataSet ds = new DataSet();
          SqlDataAdapter ada = new SqlDataAdapter();
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          ada.SelectCommand = Command;
          Command.CommandText = "SELECT " +
                                  "ID, Bezeichnung " +
                                      "FROM Gueterart ORDER BY Bezeichnung";
          ada.Fill(ds);
          Command.Dispose();
          Globals.SQLcon.Close();
          return ds;
        }
        //
        //
        //
        public void AddGueterArt()
        {
          try
          {
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = "INSERT INTO Gueterart " +
                                                     "(ViewID, Bezeichnung) " +
                                                 "VALUES ('" + ViewID + "', '" + Bezeichnung + "')";

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
          string Beschreibung = "Güterart: " + ViewID +" " + Bezeichnung+" hinzugefügt";
          Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Eintrag.ToString(), Beschreibung);
        }
        //
      //
      //
        public void UpdateGueterArt()
        {
          try
          {
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = "Update Gueterart SET ViewID ='"+ViewID+"', "+
                                                        "Bezeichnung ='"+ Bezeichnung + "' "+
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
          string Beschreibung = "Güterart: " + ViewID + " " + Bezeichnung + " geändert";
          Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
        }
        //
        //------ löschen Datensatz ------------------ 
        //
        public void Delete()
        {
          //Add Logbucheintrag
          Bezeichnung = GetBezeichnungByID(ID);
          ViewID=GetMatchCodeByBezeichnung(Bezeichnung);         
          string Beschreibung = "Güterart: " + ViewID + " " + Bezeichnung + " gelöscht";
          Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Loeschung.ToString(), Beschreibung);

          //--- initialisierung des sqlcommand---
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          Command.CommandText = "DELETE FROM Gueterart WHERE ID='" + ID + "'";
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
        //--------------------- Suchbegriff  ------------------------
        //
        public static string GetMatchCodeByBezeichnung(string _Bezeichnung)
        {
            string returnString = string.Empty;
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = "Select ViewID FROM Gueterart WHERE Bezeichnung ='" + _Bezeichnung + "'";
            Globals.SQLcon.Open();
            if (Command.ExecuteScalar() is DBNull)
            {
                returnString = string.Empty;
            }
            else
            {
                returnString = (string)Command.ExecuteScalar();
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            return returnString;
        }
      //
      //
      //
        public static string GetBezeichnungByID(Int32 id)
        {
          string returnString = string.Empty;
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          Command.CommandText = "Select Bezeichnung FROM Gueterart WHERE ID ='" + id + "'";
          Globals.SQLcon.Open();

          object obj =Command.ExecuteScalar();
          if (obj is DBNull)
          {
            returnString = string.Empty;
          }
          else
          {
            returnString = (string)obj;
          }
          Command.Dispose();
          Globals.SQLcon.Close();
          return returnString;
        }
        //
        //----------- prüft Matchcode - darf nicht doppelt sein -------------
        //
        public static bool ViewIDExists(string ViewIDG)
        {
            try
            {
                //--- Initialisierung der Connection ------------------
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                Command.CommandText = ("SELECT ID FROM Gueterart WHERE ViewID='" + ViewIDG + "'");

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
        //--- Get DatasetRec by ID
        //
        public DataSet GetGArtRecByID()
        {
          DataSet ds = new DataSet();
          SqlDataAdapter ada = new SqlDataAdapter();
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          ada.SelectCommand = Command;
          Command.CommandText = "SELECT " +
                                  "ID, ViewID as 'Suchbegriff', Bezeichnung " +
                                      "FROM Gueterart WHERE ID='"+ID+"' ORDER BY Suchbegriff";
          ada.Fill(ds);
          Command.Dispose();
          Globals.SQLcon.Close();
          return ds;
        }


    }
}
