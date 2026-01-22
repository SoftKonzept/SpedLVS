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

namespace Sped4
{
    class clsKunde
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
        private int _KD_ID;
        private int _NewKD_ID;
        private int _ADR_ID;
        private string _SteuerNr;
        private string _USt_ID;
        private char _MwSt;
        private decimal _MwStSatz;
        private string _Bank1;
        private int _Kto1;
        private int _BLZ1;
        private string _Swift1;
        private string _IBAN1;
        private string _Bank2;
        private int _Kto2;
        private int _BLZ2;
        private string _Swift2;
        private string _IBAN2;
        private Int32 _Debitor;
        private Int32 _Kreditor;

       
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }

        }
        public int KD_ID
        {
          get { return _KD_ID; }
          set { _KD_ID = value; }

        }
        public Int32 NewKD_ID
        {
          get             
          {
            bool NewKDisUsed = true;
            try
            {
              while (NewKDisUsed)
              {
                SqlDataAdapter ada = new SqlDataAdapter();
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                ada.SelectCommand = Command;
                //Command.CommandText = "SELECT MAX(ID) FROM Auftragsnummer";
                Command.CommandText = "DECLARE @NewID table( NewAID int ); " +
                                        "UPDATE Kundennummer SET ID= ID + 1 " +
                                        "OUTPUT INSERTED.ID INTO @NewID; " +
                                        "SELECT * FROM @NewId;";

                Globals.SQLcon.Open();

                _NewKD_ID = (Int32)Command.ExecuteScalar();
                Command.Dispose();
                Globals.SQLcon.Close();

                NewKDisUsed = clsKunde.CheckKundenNummerIsUsed(_NewKD_ID);
              }
            }
            catch (Exception ex)
            {
              MessageBox.Show(ex.ToString());
            }
            return _NewKD_ID;
          }
          set
          {

            _NewKD_ID = value;
          }
        }

        public int ADR_ID
        {
            get { return _ADR_ID; }
            set { _ADR_ID = value; }

        }
        public string SteuerNr 
        {
            get { return _SteuerNr; }
            set { _SteuerNr = value; }
        }
        public string USt_ID
        {
            get { return _USt_ID; }
            set { _USt_ID = value; }

        }
        public char MwSt
        {
            get { return _MwSt; }
            set { _MwSt = value; }

        }
        public decimal MwStSatz
        {
            get { return _MwStSatz; }
            set { _MwStSatz = value; }

        }
        public string Bank1
        {
            get { return _Bank1; }
            set { _Bank1 = value; }

        }
        public int Kto1
        {
            get { return _Kto1; }
            set { _Kto1 = value; }

        }
        public int BLZ1
        {
            get { return _BLZ1; }
            set { _BLZ1 = value; }

        }
        public string Swift1 
        {
            get { return _Swift1; }
            set { _Swift1 = value; }

        }
        public string IBAN1
        {
            get { return _IBAN1; }
            set { _IBAN1 = value; }

        }
        public string Bank2
        {
            get { return _Bank2; }
            set { _Bank2 = value; }

        }
        public int Kto2
        {
            get { return _Kto2; }
            set { _Kto2 = value; }

        }
        public int BLZ2
        {
            get { return _BLZ2; }
            set { _BLZ2 = value; }

        }
        public string Swift2
        {
            get { return _Swift2; }
            set { _Swift2 = value; }

        }
        public string IBAN2
        {
            get { return _IBAN2; }
            set { _IBAN2 = value; }

        }
        public Int32 Debitor
        {
            get { return _Debitor; }
            set { _Debitor = value; }

        }
        public Int32 Kreditor
        {
            get { return _Kreditor; }
            set { _Kreditor = value; }

        }



        
        //**************************************************************
        //-----------------         Methoden
        //**************************************************************

        public void SetDefValueToKDDaten(ref clsKunde clsKD)
        { 
          //Standardwerte pro Kunde auslesen
          clsKD.Debitor = 10000;
          clsKD.SteuerNr = "123 / 4567 / 890";
          clsKD.MwSt = 'T';
          clsKD.MwStSatz = 19;
          clsKD.USt_ID = "DE 123 456 78";
          clsKD.Bank1 = string.Empty;
          clsKD.BLZ1 = 0;
          clsKD.Kto1 = 0;
          clsKD.Swift1 = string.Empty;
          clsKD.IBAN1 = string.Empty;
        }
  
      
      //
        //
        //-------------------- Eintrag Kundendaten in DB --------------
        //
        public void Add()
        {           
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;

                //----- SQL Abfrage -----------------------
                Command.CommandText = ("INSERT INTO Kunde (KD_ID, ADR_ID, SteuerNr, USt_ID, MwSt, MwStSatz, Bank1, BLZ1, Kto1, Swift1, IBAN1, Bank2, BLZ2, Kto2,Swift2, IBAN2, Kreditor, Debitor) " +
                                                        "VALUES ('" + KD_ID + "','" 
                                                                    + ADR_ID + "','"
                                                                    + SteuerNr + "','" 
                                                                    + USt_ID + "','" 
                                                                    + MwSt + "','" 
                                                                    + MwStSatz.ToString().Replace(",",".") + "','" 
                                                                    + Bank1 + "','" 
                                                                    + BLZ1 + "','"
                                                                    + Kto1 + "','"
                                                                    + Swift1 + "','" 
                                                                    + IBAN1 + "','" 
                                                                    + Bank2 + "','"
                                                                    + BLZ2 + "','" 
                                                                    + Kto2 + "','"
                                                                    + Swift2 + "','"
                                                                    + IBAN2 + "','"
                                                                    + Kreditor + "','"
                                                                    + Debitor + "')");

                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
            }
            catch (Exception ex)
            {
               // System.Windows.Forms.MessageBox.Show(ex.ToString());
                //Add Logbucheintrag Exception
                string Beschreibung = "Exception: " + ex;
                Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Exception.ToString(), Beschreibung);
            }
            finally
            {
                //MessageBox.Show("Der Adressdatensatz wurde erfolgreich in die Datenbank geschrieben!");
                clsADR.updateADRforKD(KD_ID, ADR_ID, BenutzerID);
                //Add Logbucheintrag Eintrag
                string ViewID = clsADR.GetMatchCodeByID(ADR_ID);
                string Beschreibung = "Kunde: " + ViewID + " ID:" + ADR_ID + " Kundennummer:"+KD_ID+ "  hinzugefügt";
                Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Eintrag.ToString(), Beschreibung);
            }
        }
        //
        //------------------------ update Kunde--------------------------
        //
        public void updateKD()
        {
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand UpCommand = new SqlCommand();
                UpCommand.Connection = Globals.SQLcon.Connection;

                UpCommand.CommandText = "Update Kunde SET KD_ID='" + KD_ID + "', " +
                                                        "ADR_ID='" + ADR_ID + "', " +
                                                        "SteuerNr='" + SteuerNr + "', " +
                                                        "USt_ID='" + USt_ID + "', " +
                                                        "MwSt='" + MwSt + "', " +
                                                        "MwStSatz='" + MwStSatz.ToString().Replace(",",".") + "', " +
                                                        "Bank1='" + Bank1 + "'," +
                                                        "BLZ1='" + BLZ1 + "', " +
                                                        "Kto1='" + Kto1 + "', " +
                                                        "Swift1='" + Swift1 + "', " +
                                                        "IBAN1='" + IBAN1 + "', " +
                                                        "Bank2='" + Bank2 + "', " +
                                                        "BLZ2='" + BLZ2 + "'," +
                                                        "Kto2='" + Kto2 + "', " +
                                                        "Swift2='" + Swift2 + "', " +
                                                        "IBAN2='" + IBAN2 + "', " +
                                                        "Kreditor='" + Kreditor + "', " +
                                                        "Debitor='" + Debitor + "' " +
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
              string ViewID = clsADR.GetMatchCodeByID(ADR_ID);
              string Beschreibung = "Kunde: " + ViewID + " ID:" + ADR_ID + " Kundennummer:" + KD_ID + " geändert";
              Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
            }
        }
        //
        //------ füllt ComboBox Kunde / Auftraggeber -------------------
        //
        public static DataTable dataTableKD()
        {    
            DataTable KDTable = new DataTable();
            KDTable.Clear();
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT " +
                                    "ID, ViewID as 'Suchbegriff', KD_ID, Name1, PLZ, Ort " +
                                    "FROM ADR WHERE KD_ID!=0";
            
            ada.Fill(KDTable);
            Command.Dispose();
            ada.Dispose();
            Globals.SQLcon.Close();
            if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
            {
              Command.Connection.Close();
            }
            return KDTable;
        }
        //
        //----------------- liest die Kontaktdaten  zu einer ADR-ID --------------
        //
        public static DataSet ReadKDbyID(int ID)
        {
            DataSet ds = new DataSet();
            ds.Clear();
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();

            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT " +
                                            "ID, " +
                                            "KD_ID, " +
                                            "SteuerNr, " +
                                            "USt_ID, " +
                                            "MwSt, " +
                                            "MwStSatz, " +
                                            "Bank1, " +
                                            "BLZ1, " +
                                            "Kto1, " +
                                            "Swift1, " +
                                            "IBAN1, " +
                                            "Bank2, " +
                                            "BLZ2, " +
                                            "Kto2, " +
                                            "Swift2, " +
                                            "IBAN2, " +
                                            "Kreditor, " +
                                            "Debitor " +
                                                        "FROM Kunde WHERE ADR_ID='" + ID + "'";
            ada.Fill(ds);
            ada.Dispose();
            Command.Dispose();
            Globals.SQLcon.Close();
            return ds;
        }
        //
        //------------ check auf vergebene Kundennummer -----
        //
        public static bool IsADR_KD(Int32 adr_ID)
        {
            bool IsKD = false;
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT ID FROM Kunde WHERE ADR_ID='" + adr_ID + "'";
            Globals.SQLcon.Open();

            if (Command.ExecuteScalar() == null)
            {
                IsKD = false;
            }
            else
            {
                IsKD = true;
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
            {
                Command.Connection.Close();
            }
            return IsKD;
        }
        //
        //------ löschen Datensatz ------------------ 
        //
        public void DeleteKunde(string LogBuchViewID)
        {
            if (IsADR_KD(ADR_ID))
            {
              //Add Logbucheintrag Löschen
              KD_ID = GetKD_IDbyADR_ID(ADR_ID);
              string Beschreibung = "ID:" + ADR_ID + " Kundennummer:" + KD_ID + " gelöscht";
              Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Loeschung.ToString(), Beschreibung);
              
              //--- initialisierung des sqlcommand---
              SqlCommand Command = new SqlCommand();
              Command.Connection = Globals.SQLcon.Connection;
              Command.CommandText = "DELETE FROM Kunde WHERE ADR_ID='" + ADR_ID + "'";
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
        //
        //
        public decimal GetMwStSatz()
        {
          decimal MwStSatz = 0.00m;
          SqlDataAdapter ada = new SqlDataAdapter();
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          ada.SelectCommand = Command;
          Command.CommandText = "SELECT MwStSatz FROM Kunde WHERE ADR_ID='" + ADR_ID + "'";
          Globals.SQLcon.Open();

          object obj = Command.ExecuteScalar();

          if (obj != null)
          {
            MwStSatz = (decimal)obj;
          }
          Command.Dispose();
          Globals.SQLcon.Close();
          if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
          {
            Command.Connection.Close();
          }
          return MwStSatz;
        }
        //
        //--------------- MwSt-Satz für einen kunden ----------------------
        //
        public static decimal GetMwStSatz(Int32 adrID)
        {
          decimal MwStSatz = 0.00m;
          SqlDataAdapter ada = new SqlDataAdapter();
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          ada.SelectCommand = Command;
          Command.CommandText = "SELECT MwStSatz FROM Kunde WHERE ADR_ID='" + adrID + "'";
          Globals.SQLcon.Open();

          object obj = Command.ExecuteScalar();

          if (obj != null)
          {
            MwStSatz = (decimal)obj;
          }
          Command.Dispose();
          Globals.SQLcon.Close();
          if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
          {
            Command.Connection.Close();
          }
          return MwStSatz;
        }
        //
        //--------------- MwSt-Satz für einen kunden ----------------------
        //
        public static Int32 GetKD_IDbyADR_ID(Int32 adrID)
        {
          Int32 KD = 0;
          SqlDataAdapter ada = new SqlDataAdapter();
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          ada.SelectCommand = Command;
          Command.CommandText = "SELECT KD_ID FROM Kunde WHERE ADR_ID='" + adrID + "'";
          Globals.SQLcon.Open();

          object obj = Command.ExecuteScalar();

          if (obj != null)
          {
            KD = (Int32)obj;
          }
          Command.Dispose();
          Globals.SQLcon.Close();
          if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
          {
            Command.Connection.Close();
          }
          return KD;
        }
        //
        //--------- Check ob Kundenummer schon verwendet wird ---------------------
        //
        public static bool CheckKundenNummerIsUsed(Int32 Kundennummer)
        {
          bool IsUsed = false;
          SqlDataAdapter ada = new SqlDataAdapter();
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          ada.SelectCommand = Command;
          Command.CommandText = "SELECT ID FROM Kunde WHERE KD_ID='" + Kundennummer + "'";
          Globals.SQLcon.Open();

          object obj = Command.ExecuteScalar();

          if (obj != null)
          {
            IsUsed = true;
          }
          Command.Dispose();
          Globals.SQLcon.Close();
          if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
          {
            Command.Connection.Close();
          }
          return IsUsed;
        }
    }
}
