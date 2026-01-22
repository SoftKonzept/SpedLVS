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
    class clsKontakte
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
        private int _ADR_ID;
        private string _Mail;
        private string _Ansprechpartner;
        private string _Abteilung;
        private string _Telefon;
        private string _Fax;
        private string _Info;


        public int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }

        }
        public string ViewID
        {
            get
            {
                return _ViewID;
            }
            set
            {
                _ViewID = value;
            }

        }
        public int ADR_ID
        {
            get
            {
                return _ADR_ID;
            }
            set
            {
                _ADR_ID = value;
            }

        }
        public string Mail
        {
            get
            {
                return _Mail;
            }
            set
            {
                _Mail= value;
            }

        }
        public string Ansprechpartner
        {
            get
            {
                return _Ansprechpartner;
            }
            set
            {
                _Ansprechpartner = value;
            }

        }
        public string Abteilung
        {
            get
            {
                return _Abteilung;
            }
            set
            {
                _Abteilung= value;
            }

        }
        public string Telefon
        {
            get
            {
                return _Telefon;
            }
            set
            {
                _Telefon = value;
            }

        }
        public string Fax
        {
            get
            {
                return _Fax;
            }
            set
            {
                _Fax = value;
            }

        }
        public string Info
        {
            get
            {
                return _Info;
            }
            set
            {
                _Info = value;
            }

        }



        //**************************************************************************************************************************

        public void add()
        { 
           
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;

                //----- SQL Abfrage -----------------------
                Command.CommandText = ("INSERT INTO Kontakte (ViewID, ADR_ID, Mail, Ansprechpartner, Abteilung, Telefon, Fax, Info) " +
                    "VALUES ('" + ViewID + "','" + ADR_ID + "','" + Mail + "','" + Ansprechpartner + "','" + Abteilung + "','" + Telefon + "','" + Fax + "','" +Info + "')");

                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
            }
            catch (Exception ex)
            {
              //System.Windows.Forms.MessageBox.Show(ex.ToString());
              //Add Logbucheintrag Exception
              string Beschreibung = "Exception: " + ex;
              Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Exception.ToString(), Beschreibung);
            }
            finally
            {
              //Add Logbucheintrag Eintrag
              string Beschreibung = "Kontakte: " + ViewID + " hinzugefügt";
              Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Eintrag.ToString(), Beschreibung);
            }
        }
        //
        //---------------------------------------- update Kontaktdaten---------------------------------
        //
        public void updateKontakt(int Kontakt_ID)
        {
            ID = Kontakt_ID;

            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand UpCommand = new SqlCommand();
                UpCommand.Connection = Globals.SQLcon.Connection;

                UpCommand.CommandText = "Update Kontakte SET ViewID='" + ViewID + "', " +
                                                        "ADR_ID='" + ADR_ID + "', " +
                                                        "Abteilung='" + Abteilung + "', " +
                                                        "Ansprechpartner='" + Ansprechpartner + "', " +
                                                        "Mail='" + Mail + "', " +
                                                        "Telefon='" + Telefon + "', " +
                                                        "Fax='" + Fax + "'," +
                                                        "Info='" + Info + "' " +
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
                string Beschreibung = "Exception: " + ex;
                Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Exception.ToString(), Beschreibung);
            }
            finally
            {
              //Add Logbucheintrag update
              string Beschreibung = "Kontakt: " + ViewID +" geändert";
              Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
            }
        }
        //
        //----------------- liest die Kontaktdaten  zu einer ADR-ID --------------
        //
        public static DataTable ReadKontaktebyID(int ID)
        {
            DataTable dt = new DataTable();
            dt.Clear();
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();

            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT " +
                                                "ID, " +
                                                "ViewID as 'Suchbegriff', " +
                                                "Abteilung, " +
                                                "Ansprechpartner, " +
                                                "Telefon, " +
                                                "Fax, " +
                                                "Mail, " +
                                                "Info " +
                                                          "FROM Kontakte WHERE ADR_ID='"+ID+"' ORDER BY Abteilung";
            ada.Fill(dt);
            ada.Dispose();
            Command.Dispose();
            Globals.SQLcon.Close();

            return dt;
        }
      //
      //------------- löscht DB-Eintrag nach ID ----------------
      //
        public void DeleteKontaktEintrag()
        {
          //Add Logbucheintrag Löschen
          string ViewID = GetViewIDbyKontaktID(ID);
          string Beschreibung = "Kontakt: " +ViewID + " gelöscht";
          Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Loeschung.ToString(), Beschreibung);


           try
          {
            //--- initialisierung des sqlcommand---
            SqlCommand UpCommand = new SqlCommand();
            UpCommand.Connection = Globals.SQLcon.Connection;

            UpCommand.CommandText = "DELETE FROM Kontakte WHERE ID='" + ID + "'";

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
        }
        //
        //------------ check auf vergebene Kundennummer -----
        //
        public static bool ADRhasKontakte(Int32 adr_ID)
        {
            bool IsIn = false;
            DataTable dt = new DataTable();
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT ID FROM Kontakte WHERE ADR_ID='" + adr_ID + "'";
            Globals.SQLcon.Open();
            ada.Fill(dt);

            Command.Dispose();
            Globals.SQLcon.Close();
            if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
            {
                Command.Connection.Close();
            }
            if (dt.Rows.Count > 0)
            {
                IsIn = true;
            }
            else
            {
                IsIn = false;
            }
            return IsIn;
        }
        //
        //------ löschen Datensatz ------------------ 
        //
        public void DeleteKontakte(string ViewID)
        {
            if (ADRhasKontakte(ADR_ID))
            {

              //Add Logbucheintrag Löschen
              string Beschreibung = "Kontakte: " + ViewID + " gelöscht";             
              Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Loeschung.ToString(), Beschreibung);

                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                Command.CommandText = "DELETE FROM Kontakte WHERE ADR_ID='" + ADR_ID + "'";
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
        //
        //------------------ ViewID ---------------------------
        //
        private string GetViewIDbyKontaktID(Int32 KontaktID)
        {
          string ViewID = string.Empty;
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          Command.CommandText = "Select ViewID FROM Kontakte WHERE ID='" + KontaktID + "'";
          Globals.SQLcon.Open();
          object obj = Command.ExecuteScalar();
          if (obj is DBNull)
          {
            ViewID = "";
          }
          else
          {
            ViewID = (string)obj;
          }
          Command.Dispose();
          Globals.SQLcon.Close();

          return ViewID;
        }
    }
}
