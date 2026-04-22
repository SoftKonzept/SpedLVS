using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Windows.Forms;
using Sped4;
using Sped4.Classes;


namespace Sped4.Classes
{
    class clsADR
    { 
      //************  User  ***************
      private Int32 _BenutzerID;
      public Int32 BenutzerID
      {
          get { return _BenutzerID; }
          set { _BenutzerID = value;}
      }
      //************************************#


        private int _ID;
        private string _ViewID;
        private int _KD_ID;
        private string _FBez;
        private string _Name1;
        private string _Name2;
        private string _Name3;
        private string _Str;
        private string _PF;
        private string _PLZ;
        private string _PLZPF;
        private string _Ort;
        private string _OrtPF;
        private string _Land;
        private DateTime _WAvon=default(DateTime);         // Warenannahme von
        private DateTime _WAbis=default(DateTime);         // Warenannhame bis
        private int _A;                 //Status für Auftraggeber 
        private int _V;                 // Status für Versender
        private int _E;                 // Status für Empfänger
        private DateTime _Date_Add= default(DateTime);


        public int ID
        {
            get { return _ID; }
            set { _ID = value; }

        }
        public string ViewID
        {
            get { return _ViewID; }
            set { _ViewID = value; }

        }
        public int KD_ID
        {
            get { return _KD_ID; }
            set { _KD_ID = value; }

        }
        public string FBez
        {
            get { return _FBez; }
            set { _FBez = value; }
        }
        public string Name1
        {
            get { return _Name1; }
            set { _Name1 = value; }

        }
        public string Name2
        {
            get { return _Name2; }
            set { _Name2 = value; }

        }
        public string Name3
        {
            get { return _Name3; }
            set { _Name3 = value; }

        }
        public string PF
        {
            get { return _PF; }
            set { _PF = value; }

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
        public string PLZPF
        {
            get { return _PLZPF; }
            set { _PLZPF = value; }

        }
        public string Ort
        {
            get { return _Ort; }
            set { _Ort = value; }

        }
        public string OrtPF
        {
            get { return _OrtPF; }
            set { _OrtPF = value; }

        }
        public string Land
        {
            get { return _Land; }
            set { _Land = value; }

        }
        public DateTime WAvon
        {
            get { return _WAvon; }
            set { _WAvon = value; }

        }
        public DateTime WAbis
        {
            get { return _WAbis; }
            set { _WAbis = value; }

        }
        public int A
        {
            get { return _A; }
            set { _A = value; }

        }
        public int V
        {
            get { return _V; }
            set { _V = value; }

        }
        public int E
        {
            get { return _E; }
            set { _E = value; }

        }
        public DateTime Date_Add
        {
            get { return _Date_Add; }
            set
            {
                //value = DateTime.Today; //(DateTime.Now).ToString("dd.MM.yyyy"); // heutige Datumv
                _Date_Add = value; 
            }

        }


        //*******************************************************************************************************
        //**************                         Methoden    
        //*******************************************************************************************************
        //
        //--------------------------------------------------- Speichern neue Adressdaten ---------------------
        //
        public void Add()
        {
            Date_Add = DateTime.Now;
            try
            {
              //--- initialisierung des sqlcommand---
              SqlCommand Command = new SqlCommand();
              Command.Connection = Globals.SQLcon.Connection;

              //----- SQL Abfrage -----------------------
              Command.CommandText = ("INSERT INTO ADR (ViewID, KD_ID, FBez, Name1, Name2, Name3, Str,PF, PLZ, PLZPF, Ort, OrtPF, Land, WAvon, WAbis, A, V, E) " +
                                              "VALUES ('" + ViewID + "','"
                                                          + KD_ID + "','"
                                                          + FBez + "','"
                                                          + Name1 + "','"
                                                          + Name2 + "','"
                                                          + Name3 + "','"
                                                          + Str + "','"
                                                          + PF + "','"
                                                          + PLZ + "','"
                                                          + PLZPF + "','"
                                                          + Ort + "','"
                                                          + OrtPF + "','"
                                                          + Land + "','"
                                                          + WAvon + "','"
                                                          + WAbis + "','"
                                                          + A + "','"
                                                          + V + "','"
                                                          + E +  "')");

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
              string Beschreibung = "Adresse: " + ViewID + " - " + Name1 + " hinzugefügt";
              Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Eintrag.ToString(), Beschreibung);
            }
        }
        //
        //----- Get ADR ID mit Hilfe aller Daten nach ADD() -----------
        //
        public Int32 GetADR_ID()
        {
          Int32 adrID = 0;
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          Command.CommandText = "Select ID FROM ADR WHERE ViewID='" + ViewID + "' AND KD_ID='"+KD_ID+"'";
          Globals.SQLcon.Open();
          object obj = Command.ExecuteScalar();
          if (obj == null)
          {
            adrID = 0;
          }
          else
          {
            adrID =(Int32)obj;
          }
          Command.Dispose();
          Globals.SQLcon.Close();

          return adrID;
        }
       //
       //--------------
       //
       public static DataTable GetADRList()
       {
            DataTable dataTable = new DataTable();
            dataTable.Clear();
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();

            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT " +
                                            "ID, " +
                                            "ViewID as 'Suchbegriff', " +
                                            "KD_ID as 'KD-Nr', " +
                                            "FBez as 'Bezeichnung', " +
                                            "Name1, " +
                                            "Name2, " +
                                            "Name3, " +
                                            "Str as 'Strasse', " +
                                            "PF as 'Postfach', " +
                                            "PLZ, " +
                                            "PLZPF, " +
                                            "Ort, " +
                                            "OrtPF, " +
                                            "Land " + 
                                                         "FROM ADR ORDER BY ViewID";

            ada.Fill(dataTable);
            ada.Dispose();
            Command.Dispose();
            Globals.SQLcon.Close();
            return dataTable;
       }
        //
        //-----------------------------------------------------   Update Column aus ADR - Liste  ----------------
        //
        public void UpdateColumn(string ID, string Column, string Update)
        {
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand UpCommand = new SqlCommand();
                UpCommand.Connection = Globals.SQLcon.Connection;

                switch (Column)
                {
                    case "Suchbegriff":
                        UpCommand.CommandText = "Update ADR SET ViewID='" + Update + "' WHERE ID='" + ID + "'";
                        break;
                    case "Strasse":
                        UpCommand.CommandText = "Update ADR SET Str='" + Update + "' WHERE ID='" + ID + "'";
                        break;
                    case "Postfach":
                        UpCommand.CommandText = "Update ADR SET PF='" + Update + "' WHERE ID='" + ID + "'";
                        break;
                    default:
                        UpCommand.CommandText = "Update ADR SET " + Column + "='" + Update + "' WHERE ID='" + ID + "'";
                        break;
                }

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
              ViewID = GetMatchCodeByID(Convert.ToInt32(ID));
              string Beschreibung = "Adresse: " + ViewID + "  ID:" + ID + " geändert";
              Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
            }
        }
        //
        //---------------------------------------- update ADR ---------------------------------
        //
        public void updateADR(Int32 adrID)
        {
            ID = adrID;
            Date_Add = DateTime.Now;
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand UpCommand = new SqlCommand();
                UpCommand.Connection = Globals.SQLcon.Connection;

                UpCommand.CommandText = "Update ADR SET ViewID='" + ViewID + "', " +
                                                        "FBez='" + FBez + "', " +
                                                        "Name1='" + Name1 + "', " +
                                                        "Name2='" + Name2 + "', " +
                                                        "Name3='" + Name3 + "', " +
                                                        "Str='" + Str + "', " +
                                                        "PF='" + PF + "'," +
                                                        "PLZ='" + PLZ + "', " +
                                                        "PLZPF='" + PLZPF + "', " +
                                                        "Ort='" + Ort + "', " +
                                                        "OrtPF='" + OrtPF + "', " +
                                                        "Land='" + Land + "', " +
                                                        "WAvon='" + WAvon + "'," +
                                                        "WAbis='" + WAbis + "', " +
                                                        "A='" + A + "', " +
                                                        "V='" + V + "', " +
                                                        "E='" + E + "' " +
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
              ViewID = GetMatchCodeByID(ID);
              string Beschreibung = "Adresse: " + ViewID + "  ID:" + ID + " geändert";
              Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
            }
        }
        //
        //---------------------- Update ADR mit KDNr ------------------------
        //
        public static void updateADRforKD(Int32 KDNr, Int32 ADR_ID, Int32 BenutzerID)
        {
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand UpCommand = new SqlCommand();
                UpCommand.Connection = Globals.SQLcon.Connection;

                UpCommand.CommandText = "Update ADR SET KD_ID='" + KDNr + "' WHERE ID='" + ADR_ID + "'";

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
              string ViewID = GetMatchCodeByID(ADR_ID);
              string Beschreibung = "Adresse: " + ViewID + "  ID:" + ADR_ID + " Kundennummer:"+KDNr +" geändert";
              Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
            }
        }
        //
        //---------- DataTabel Versender/Beladestelle und Empfänger/Entladestelle  ------
        //
        public static DataTable ADRTable()
        {
            DataTable adrTable = new DataTable();
            adrTable.Clear();
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT " +
                                    "ID, ViewID as 'Suchbegriff', KD_ID, Name1, PLZ, Ort " +
                                    "FROM ADR";

            ada.Fill(adrTable);
            Command.Dispose();
            Globals.SQLcon.Close();
            return adrTable;
        }
        //
        //
        //
        public static DataSet ReadADRbyID(int ID)
        {           
            DataSet ds = new DataSet();
            ds.Clear();
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();

            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT * FROM ADR WHERE ID='" + ID + "'";

            ada.Fill(ds);
            ada.Dispose();
            Command.Dispose();
            Globals.SQLcon.Close();

            return ds;
        }
        //
        //------------------ Get Suchname by ADR ID  ------------------
        //
        public static string ReadViewIDbyID(int adrID)
        {   
            string ReturnValue="";
            if (adrID > 1)
            {

                
                SqlDataAdapter ada = new SqlDataAdapter();
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                ada.SelectCommand = Command;
                Command.CommandText = "SELECT ViewID FROM ADR WHERE ID='" + adrID + "'";
                Globals.SQLcon.Open();
                ReturnValue = Command.ExecuteScalar().ToString();
                Command.Dispose();
                Globals.SQLcon.Close();
                return ReturnValue;
            }
            else
            {
                return ReturnValue;
            }
        }
        //
        //--------- String einer Adresse ---------------
        //
        public static String GetADRString(int ID)
        {
            DataSet ds=ReadADRbyID(ID);
            string strADR = string.Empty;

            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
              //strADR = strADR + ds.Tables[0].Rows[i]["KD_ID"].ToString() + " - ";
              strADR = strADR + ds.Tables[0].Rows[i]["Name1"].ToString().Trim() + " - ";
              strADR = strADR + ds.Tables[0].Rows[i]["PLZ"].ToString().Trim() + " - ";
              strADR = strADR + ds.Tables[0].Rows[i]["Ort"].ToString().Trim();     
            }
            return strADR;
        }
        //
        //----------- Check Verwendung ------------------- 
        //
        public static bool IsADRUsed(Int32 _ID)
        {
            bool IsIn = true;
            try
            {
                SqlDataAdapter ada = new SqlDataAdapter();
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                ada.SelectCommand = Command;
                Command.CommandText = "SELECT ID FROM Auftrag WHERE KD_ID='" + _ID + "' OR B_ID='"+_ID+"' OR E_ID='"+_ID+"'";    //Stauts 1-2 sind in der Auftragsliste angezeigt
                Globals.SQLcon.Open();
                if (Command.ExecuteScalar() == null)
                {
                    IsIn = false;
                }
                else
                {
                    IsIn = true;
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
            return IsIn;
        }
        //
        //------ löschen Datensatz ------------------ 
        //
        public void DeleteADR()
        {
          //Add Logbucheintrag Löschen
          string ViewID = GetMatchCodeByID(ID);
          string Beschreibung = "Adressen: " + ViewID + " ID: " + ID + " gelöscht";
          Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Loeschung.ToString(), Beschreibung);

          //--- initialisierung des sqlcommand---
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          Command.CommandText = "DELETE FROM ADR WHERE ID='" + ID + "'";
          Globals.SQLcon.Open();
          Command.ExecuteNonQuery();
          Command.Dispose();
          Globals.SQLcon.Close();
          if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
          {
              Command.Connection.Close();
          }

          //Löschen der Kundendaten wenn vorhanden
          clsKunde kd = new clsKunde();
          kd.BenutzerID = BenutzerID;
          kd.ADR_ID = ID;
          kd.DeleteKunde(ViewID);

          //Löschen der Kontaktdaten wenn vorhanden
          clsKontakte kt = new clsKontakte();
          kt.BenutzerID = BenutzerID;
          kt.ADR_ID = ID;
          kt.DeleteKontakte(ViewID);

        }
        //
        //--------------------- Suchbegriff  ------------------------
        //
        public static string GetMatchCodeByID(Int32 _ADR_ID)
        {
            string returnString = string.Empty;
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = "Select ViewID FROM ADR WHERE ID='" + _ADR_ID + "'";
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
        //----------- prüft Matchcode - darf nicht doppelt sein -------------
        //
        public static bool ViewIDExists(string ViewIDADR)
        {
            try
            {
                //--- Initialisierung der Connection ------------------
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                Command.CommandText = ("SELECT ID FROM ADR WHERE ViewID='" + ViewIDADR + "'");

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
                reader.Close();
                Command.Dispose();
            }
            finally
            {
                Globals.SQLcon.Close();
            }
        }
        //
        //--------------------- Kundenummer lt. ADR-ID ------------------------
        //
        public static Int32 GetKD_IDByID(Int32 _ADR_ID)
        {
          Int32 KD_ID = 0;
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = "Select KD_ID FROM ADR WHERE ID='" + _ADR_ID + "'";
            Globals.SQLcon.Open();
            object obj = Command.ExecuteScalar();
            if (obj is DBNull)
            {
              KD_ID = 0;
            }
            else
            {
              KD_ID = (Int32)obj;
            }
            Command.Dispose();
            Globals.SQLcon.Close();

          return KD_ID;
        }
        //
        //--------------------- Kundenummer lt. ADR-ID ------------------------
        //
        public static Int32 GetKD_IDByMatchcode(string mc)
        {
          Int32 KD_ID = 0;
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          Command.CommandText = "Select KD_ID FROM ADR WHERE ViewID='" + mc + "'";
          Globals.SQLcon.Open();
          object obj = Command.ExecuteScalar();
          if (obj is DBNull)
          {
            KD_ID = 0;
          }
          else
          {
            KD_ID = (Int32)obj;
          }
          Command.Dispose();
          Globals.SQLcon.Close();

          return KD_ID;
        }
        //
        //--------------------- ADR ID BY MC  ------------------------
        //
        public static Int32 GetIDByMatchcode(string mc)
        {
          Int32 ADR_ID = 0;
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          Command.CommandText = "Select ID FROM ADR WHERE ViewID='" + mc + "'";
          Globals.SQLcon.Open();
          object obj = Command.ExecuteScalar();
          if (obj is DBNull)
          {
            ADR_ID = 0;
          }
          else
          {
            ADR_ID = (Int32)obj;
          }
          Command.Dispose();
          Globals.SQLcon.Close();

          return ADR_ID;
        }
        //
        //--------------------- Kundenummer lt. ADR-ID ------------------------
        //
        public static bool IsADRKunde(Int32 _ADR_ID)
        {
          bool IsKd = false;

          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          Command.CommandText = "Select ViewID FROM ADR WHERE ID='" + _ADR_ID + "'";
          Globals.SQLcon.Open();
          object obj = Command.ExecuteScalar();
          if (obj is DBNull)
          {
            IsKd = false;
          }
          else
          {
            IsKd = true;
          }
          Command.Dispose();
          Globals.SQLcon.Close();
          return IsKd;
        }
        //
        //--------------------- Ort einer Adresse ------------------------
        //--- bsp. FVGS von nach ------------
        public static string GetOrtByID(Int32 _ADR_ID)
        {
          string Ort = string.Empty;
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          Command.CommandText = "Select Ort FROM ADR WHERE ID='" + _ADR_ID + "'";
          Globals.SQLcon.Open();
          object obj = Command.ExecuteScalar();
          if (obj is DBNull)
          {
            Ort = "";
          }
          else
          {
            Ort = (string)obj;
          }
          Command.Dispose();
          Globals.SQLcon.Close();

          return Ort;
        }
        //
        //--------------------- Ort einer Adresse ------------------------
        //--- bsp. FVGS von nach ------------
        public static bool CheckMatchcodeIsUsed(string Matchcode)
        {
          bool mc = false;
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          Command.CommandText = "Select ID FROM ADR WHERE ViewID='" + Matchcode + "'";
          Globals.SQLcon.Open();
          object obj = Command.ExecuteScalar();
          if (obj == null)
          {
            mc = false;
          }
          else
          {
            mc = true;
            //Int32 id = (Int32)obj;
          }
          Command.Dispose();
          Globals.SQLcon.Close();

          return mc;
        }
    }
}
