using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Windows.Forms;
using Sped4;
using System.Drawing.Imaging;
using System.Drawing;


namespace Sped4
{
    class clsPersonal
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
        private string _Name;
        private string _Vorname;
        private string _Str;
        private string _PLZ;
        private string _Ort;
        private string _Telefon;
        private string _Mail;
        private string _Abteilung;
        private string _Beruf;
        private DateTime _seit= default(DateTime);
        private DateTime _bis;
        private string _Notiz;
        private string _Anrede;
        public Image _Passbild;
        public Image tmpPassbild { get; set; }


        
  
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string Vorname
        {
            get { return _Vorname; }
            set { _Vorname = value; }
        }
        public string Str
        {
            get { return _Str; }
            set { _Str = value; }
        }
        public string PLZ
        {
            get { return _PLZ; }
            set { _PLZ = value; }
        }
        public string Ort
        {
            get { return _Ort; }
            set { _Ort = value; }
        }
        public string Telefon
        {
            get { return _Telefon; }
            set { _Telefon = value; }
        }
        public string Mail
        {
            get { return _Mail; }
            set { _Mail = value; }
        }
        public string Abteilung
        {
            get { return _Abteilung; }
            set { _Abteilung = value; }
        }
        public string Beruf
        {
            get { return _Beruf; }
            set { _Beruf = value; }
        }
        public DateTime seit 
        {
            get { return _seit; }
            set { _seit = value; }
        }
        public DateTime bis
        {
          get 
          {
            
            //_bis = Convert.ToDateTime("31.12.9999");
            return _bis; 
          }
          set { _bis = value; }
        }
        public string Notiz
        {
            get { return _Notiz; }
            set { _Notiz = value; }
        }
        public string Anrede
        {
            get { return _Anrede; }
            set { _Anrede = value; }
        }
        public Image Passbild
        {
          get 
          {
            clsImages img = new clsImages();
            
            try
            {
              SqlDataAdapter ada = new SqlDataAdapter();
              SqlCommand Command = new SqlCommand();
              Command.Connection = Globals.SQLcon.Connection;
              ada.SelectCommand = Command;
              Command.CommandText = "SELECT Passbild FROM Personal WHERE ID='"+ID+"'";
              Globals.SQLcon.Open();
              if (Command.ExecuteScalar() is DBNull)
              {

              }
              else
              { 
                  img.byteArrayIn = (byte[])Command.ExecuteScalar();
                  //img.byteArrayToImage();
                  _Passbild = img.ConvertByteArrayToImage();  
                  
              }
              //_Passbild.Save("C:\\Bild.jpg");
                      
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
            return _Passbild; 
          }
          set { _Passbild = value; }
        }
        //
        //

        //************************************************************************************
        //**********              Methoden
        //***********************************************************************************
        //
        //
        //
        public static DataTable GetPersonalList(bool _aktuelleListe)
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();

            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            if (_aktuelleListe)
            {
              Command.CommandText = "SELECT " +
                                              "ID, " +
                                              "Anrede, " +
                                              "Name, " +
                                              "Vorname, " +
                                              "Str as 'Strasse'," +
                                              "PLZ, " +
                                              "Ort, " +
                                              "Abteilung, " +
                                              "Beruf, " +
                                              "seit as 'Beschäftigt ab' " +
                                                          "FROM Personal WHERE bis='" + DateTime.MaxValue + "' " +
                                                          "ORDER BY Name";
            }
            else
            {
              Command.CommandText = "SELECT " +
                                              "ID, " +
                                              "Anrede, "+
                                              "Name, " +
                                              "Vorname, " +
                                              "Str as 'Strasse'," +
                                              "PLZ, " +
                                              "Ort, " +
                                              "Abteilung, " +
                                              "Beruf, " +
                                              "seit as 'Beschäftigt seit', " +
                                              "bis as 'Beschäftigt bis' "+
                                                          "FROM Personal ORDER BY Name";              
            }
            ada.Fill(dataTable);
            ada.Dispose();
            Command.Dispose();
            Globals.SQLcon.Close();
            return dataTable;
        }
         //
        //------------------------------------------------- add new Personalitem to DB ---------
        //
        public void AddItem()
        {

            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;

                //----- SQL Abfrage -----------------------
                Command.CommandText = ("INSERT INTO Personal (Name, Vorname, Str, PLZ, Ort, Telefon, Mail, Abteilung, Beruf, seit, bis, Notiz, Anrede) " +
                    "VALUES ('" + Name + "','" + Vorname + "','" + Str + "','" + PLZ + "','" + Ort + "','" + Telefon + "','" + Mail + "','" + Abteilung + "','" + Beruf + "','" + seit + "','" + DateTime.MaxValue + "','" + Notiz + "','" + Anrede + "')");

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
                //MessageBox.Show("Der Personendaten wurde erfolgreich in die Datenbank geschrieben!");

              //Add Logbucheintrag Eintrag
              string Beschreibung = "Personal: " + Name + ", " + Vorname + " hinzugefügt";
              Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Eintrag.ToString(), Beschreibung);
              
              //Passbild wird hinzugefügt
              ID = GetIDbyDaten();
              if (ID > 0)
              {
                clsImages img = new clsImages();
                img.ImageIn = tmpPassbild;
                img.WriteToPersonalImage(ID);
              }
            }
        }
        //
        //---------------------------------------------------- update the DB Personal ----------
        //
        public void updatePersonal()
        {
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand UpCommand = new SqlCommand();
                UpCommand.Connection = Globals.SQLcon.Connection;
                  UpCommand.CommandText = "Update Personal SET " +
                                                        "Name='" + Name + "', " +
                                                        "Vorname='" + Vorname + "', " +
                                                        "Str='" + Str + "', " +
                                                        "PLZ='" + PLZ + "', " +
                                                        "Ort='" + Ort + "', " +
                                                        "Telefon='" + Telefon + "', " +
                                                        "Mail='" + Mail + "', " +
                                                        "Abteilung='" + Abteilung + "'," +
                                                        "Beruf='" + Beruf + "', " +
                                                        "seit='" + seit + "', " +
                                                        "bis='" + bis + "', " +
                                                        "Notiz='" + Notiz + "', " +
                                                        "Anrede='" + Anrede + "' " +
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
              //Add Logbucheintrag Update
              string Beschreibung = "Personal: " + Name + ", " + Vorname + " geändert";
              Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
            }
        }
        //
        //--------------------- Read Datensatz from ID  --------------------------------
        //
        public static DataSet ReadDataByID(int dataID)
        {
            DataSet ds = new DataSet();
            ds.Clear();
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();

            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT * FROM Personal WHERE ID='" + dataID + "'";

            ada.Fill(ds);
            ada.Dispose();
            Command.Dispose();
            Globals.SQLcon.Close();

            return ds;
        }
        //
        //------------ Aufliegerdaten für Fahrerliste Disposition  ------------------
        //
        public static DataTable GetFahrerListe()
        {
          DataTable dataTable = new DataTable();
          dataTable.Clear();
          SqlDataAdapter ada = new SqlDataAdapter();
          SqlCommand Command = new SqlCommand();

          Command.Connection = Globals.SQLcon.Connection;
          ada.SelectCommand = Command;
          Command.CommandText = "SELECT " +
                                          "ID, " +
                                          "Name, " +
                                          "Vorname " +
                                          "FROM Personal WHERE Abteilung='Fahrpersonal' AND bis='"+DateTime.MaxValue+"' ORDER BY Name ";

          ada.Fill(dataTable);
          ada.Dispose();
          Command.Dispose();
          Globals.SQLcon.Close();
          return dataTable;
        }
        //
        //---------- Name zur Personal ID  -----------------------
        //
        public static string GetNameByID(int ID)
        {
          string Name = string.Empty;
          SqlDataAdapter ada = new SqlDataAdapter();
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          ada.SelectCommand = Command;
          Command.CommandText = ("SELECT Name  FROM Personal WHERE ID = " + ID);
          Globals.SQLcon.Open();

          object obj = Command.ExecuteScalar();

          if (obj != null)
          {
            Name = (string)obj;
          }
          Command.Dispose();
          Globals.SQLcon.Close();
          return Name;
        }
        //
        //---------- Name zur Personal ID  -----------------------
        //
        private Int32 GetIDbyDaten()
        {
          Int32 ID = 0;
          SqlDataAdapter ada = new SqlDataAdapter();
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          ada.SelectCommand = Command;
          Command.CommandText = ("SELECT ID FROM Personal WHERE Name='" + Name + "' AND " +
                                                               "Vorname='" + Vorname + "' AND " +
                                                               "Str='" + Str + "' AND " +
                                                               "PLZ='" + PLZ + "' AND " +
                                                               "Ort ='" + Ort + "' AND " +
                                                               "Telefon ='" + Telefon + "' AND " +
                                                               "Mail ='" + Mail + "' AND " +
                                                               "Abteilung  ='" + Abteilung + "' AND " +
                                                               "Beruf ='" + Beruf + "' AND " +
                                                               "seit ='" + seit + "' AND " +
                                                               "bis ='" + bis + "' AND " +
                                                               "Notiz ='" + Notiz + "' AND " +
                                                               "Anrede ='" + Anrede + "' ");
                                                               
          Globals.SQLcon.Open();

          object obj = Command.ExecuteScalar();

          if (obj != null)
          {
            ID = (Int32)obj;
          }
          Command.Dispose();
          Globals.SQLcon.Close();
          return ID;
        }
        //
        //---------- Vorname zur Personal ID  -----------------------
        //
        public static string GetVornameByID(int ID)
        {
          string Vorname = string.Empty;
          SqlDataAdapter ada = new SqlDataAdapter();
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          ada.SelectCommand = Command;
          Command.CommandText = ("SELECT Vorname  FROM Personal WHERE ID = " + ID);
          Globals.SQLcon.Open();
          object obj = Command.ExecuteScalar();

          if (obj != null)
          {
            Vorname = (string)obj;
          }
          Command.Dispose();
          Globals.SQLcon.Close();
          return Vorname;
        }
        //
        //------ löschen Datensatz  -----------------------
        //
        public void DeletePersonal()
        {
            //Add Logbucheintrag Löschen
            string Name = GetNameByID(ID);
            string Vorname = GetVornameByID(ID);
            string Beschreibung = "Personal: " + Name + ", " + Vorname + " ID: "+ID+ " gelöscht";
            Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Loeschung.ToString(), Beschreibung);
          
            //--- initialisierung des sqlcommand---
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = "DELETE FROM Personal WHERE ID='" + ID + "'";
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
