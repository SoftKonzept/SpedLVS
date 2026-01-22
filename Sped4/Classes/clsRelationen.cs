using LVS;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Sped4.Classes
{
    class clsRelationen
    {
        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get { return _BenutzerID; }
            set { _BenutzerID = value; }
        }
        //************************************

        private decimal _ID;
        private string _Relation;

        public decimal ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string Relation
        {
            get { return _Relation; }
            set { _Relation = value; }
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
            string Beschreibung = "Relation: " + Relation + " hinzugefügt";
            Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), Beschreibung);
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
                Command.CommandText = "Update Relationen SET Relation ='" + Relation + "' " +
                                                            "WHERE ID='" + ID + "'";

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
            Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
        }
        //
        //----------- Check Verwendung ------------------- 
        //
        public static bool IsRelationUsed(string _Relation)
        {
            bool RelationIsIn = true;
            try
            {
                SqlDataAdapter ada = new SqlDataAdapter();
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                ada.SelectCommand = Command;
                Command.CommandText = "SELECT Auftrag.ID FROM Auftrag " +
                                                              "INNER JOIN AuftragPos ON Auftrag.ANr=AuftragPos.Auftrag_ID " +
                                                              "WHERE Relation='" + _Relation + "'";
                Globals.SQLcon.Open();
                if (Command.ExecuteScalar() == null)
                {
                    RelationIsIn = false;
                }
                else
                {
                    RelationIsIn = true;
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
            Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), Beschreibung);

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
                    reader.Dispose();
                    reader.Close();
                    Command.Dispose();
                    return true;
                }
                else
                {
                    reader.Dispose();
                    reader.Close();
                    Command.Dispose();
                    return false;
                }
            }
            finally
            {
                Globals.SQLcon.Close();
            }
        }
        //
        //
        //
        private string GetRelationByID(decimal ID)
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
