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
    class clsAuftrag
    {
        // Anzahl 10
        private int _ID;
        private int _ANr;           // Auftragsnummer
        private DateTime _ADate=default(DateTime);
        private int _KD_ID;
        private int _B_ID;
        private int _E_ID;
        private DateTime? _T_Date;
        private DateTime? _VSB;
        private DateTime? _ZF;
        private int _Status;
        private decimal _Gewicht;
        private string _Notiz;
        private char _Prioritaet;
        private DateTime _Date_Add = default(DateTime);
        private string _Relation;
        private DateTime _SearchDateVon;
        private DateTime _SearchDateBis;
        private decimal _vFracht;

       


        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public int ANr
        {
            get             // Auftragsnummer wird ausgelesen
            {                
                try
                {  
                SqlDataAdapter ada = new SqlDataAdapter();
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                ada.SelectCommand = Command;
                //Command.CommandText = "SELECT MAX(ID) FROM Auftragsnummer";
                Command.CommandText = "DECLARE @NewID table( NewAID int ); " +
                                        "UPDATE Auftragsnummer SET ID= ID + 1 " +
                                        "OUTPUT INSERTED.ID INTO @NewID; " +
                                        "SELECT * FROM @NewId;";
                     
                Globals.SQLcon.Open();

                _ANr =(Int32) Command.ExecuteScalar();
                Command.Dispose();
                Globals.SQLcon.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                return _ANr;
            }
            set 
            {                
 
                _ANr = value; 
            }
        }
        public DateTime ADate
        {
            get { return _ADate; }
            set { _ADate = value; }
        }
        public int KD_ID
        {
            get { return _KD_ID; }
            set { _KD_ID = value; }
        }
        public int B_ID
        {
            get { return _B_ID; }
            set { _B_ID = value; }
        }
        public int E_ID
        {
            get { return _E_ID; }
            set { _E_ID = value; }
        }
        public DateTime? T_Date
        {
            get { return _T_Date; }
            set { _T_Date = value; }
        }
        public DateTime? VSB
        {
            get { return _VSB; }
            set { _VSB = value; }
        }
        public DateTime? ZF
        {
            get { return _ZF; }
            set { _ZF = value; }
        }
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public decimal Gewicht
        {
          get { return _Gewicht; }
          set { _Gewicht = value; }
        }
        public string Notiz
        {
            get { return _Notiz; }
            set { _Notiz = value; }
        }
        public char Prioritaet
        {
            get { return _Prioritaet; }
            set { _Prioritaet = value; }
        }
        public DateTime Date_Add
        {
            get { return _Date_Add; }
            set { _Date_Add = value; }

        }
        public string Relation
        {
            get { return _Relation; }
            set { _Relation = value; }
        }
        public DateTime SearchDateVon
        {
          get { return _SearchDateVon; }
          set { _SearchDateVon = value; }

        }
        public DateTime SearchDateBis
        {
          get { return _SearchDateBis; }
          set { _SearchDateBis = value; }
        }
        public decimal vFracht
        {
          get { return _vFracht; }
          set { _vFracht = value; }
        }

        //**********************************************************************************
        //--------------              Methoden
        //**********************************************************************************
        //
        //--- Add AuftragsDaten  ----
        //
        public void Add(int Auftrag)
        {
            Date_Add = DateTime.Now;
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;

                //----- SQL Abfrage -----------------------
                Command.CommandText = ("INSERT INTO Auftrag (ANr, "+
                                                             "ADate, "+
                                                             "KD_ID, "+ 
                                                             "B_ID, "+
                                                             "E_ID, "+ 
                                                             "T_Date, "+
                                                             "VSB, "+
                                                             "ZF, "+ 
                                                             "Status, "+
                                                             "Gewicht, "+
                                                             "Notiz, "+
                                                             "Priorität, "+
                                                             "Date_Add, "+
                                                             "Relation, "+
                                                             "vFracht) " +
                                        "VALUES ('" + Auftrag + "','"
                                                    + ADate + "','" 
                                                    + KD_ID + "','"
                                                    + B_ID + "','"
                                                    + E_ID + "','" 
                                                    + T_Date + "','" 
                                                    + VSB+ "','" 
                                                    + ZF+ "','"
                                                    + Status + "','" 
                                                    + Gewicht.ToString().Replace(",",".") + "','" 
                                                    + Notiz + "','"
                                                    + Prioritaet+ "','"
                                                    + Date_Add + "','"
                                                    + Relation +"','"
                                                    + vFracht.ToString().Replace(",", ".") + "')");

                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());

            }
            finally
            {
               // MessageBox.Show("Der Auftrag wurde aufgenommen wurde erfolgreich in die Datenbank geschrieben!");

            }
        }
        //
        //----------- Auftragsnummer zu den richtigen Auftragswerten  ------------------
        //
        public static int GetIDbyValue(int kd, decimal gewicht, DateTime auftragsdate)
        { 
            int auftrag = 0; ;
            try
            {
                DataTable ANr_Table = new DataTable();
                SqlDataAdapter ada = new SqlDataAdapter();
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                ada.SelectCommand = Command;
                Command.CommandText = "SELECT ID FROM Artikel WHERE KD_ID='" + kd + 
                                                                    "' and Gewicht='" + gewicht + 
                                                                    "' and ADate='"+auftragsdate+"'";
                Globals.SQLcon.Open();

                if (Command.ExecuteScalar() is DBNull)
                { 
                    auftrag= 0; 
                }
                else
                {
                    auftrag = (int)Command.ExecuteScalar();
                }
                Command.Dispose();
                Globals.SQLcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return auftrag;
        }
        //
        //--------------- Get Auftragsdaten aus DB ----------------
        //
        public static DataSet ReadDataByID(int AuftragID, int AuftragPos)
        {
            DataSet ds = new DataSet();
            ds.Clear();
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();

            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT T_Date, ZF,VSB, Status, Ladenummer, LadeNrRequire, " +
                                                     "(Select SUM(gemGewicht) FROM Artikel WHERE Artikel.AuftragID='"+AuftragID+"' AND AuftragPos='"+AuftragPos+"') as 'gemPosGewicht', "+
                                                     "(Select SUM(tatGewicht) FROM Artikel WHERE Artikel.AuftragID='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "') as 'tatPosGewicht', " +
                                                     "(Select ADate FROM Auftrag WHERE Auftrag.ANr='" + AuftragID + "') as 'ADate', " +
                                                     "(Select KD_ID FROM Auftrag WHERE Auftrag.ANr='" + AuftragID + "') as 'KD_ID', " +
                                                     "(Select B_ID FROM Auftrag WHERE Auftrag.ANr='" + AuftragID + "') as 'B_ID', " +
                                                     "(Select E_ID FROM Auftrag WHERE Auftrag.ANr='" + AuftragID + "') as 'E_ID', " +
                                                     "(Select Priorität FROM Auftrag WHERE Auftrag.ANr='" + AuftragID + "') as 'Prio', " +
                                                     "(Select Notiz FROM Auftrag WHERE Auftrag.ANr='" + AuftragID + "') as 'Notiz', " +
                                                     "(Select Relation FROM Auftrag WHERE Auftrag.ANr='"+AuftragID+"') as 'Relation', " +
                                                     "(Select vFracht FROM Auftrag WHERE Auftrag.ANr='141') as 'vFracht' "+ 
                                                     "FROM AuftragPos " +
                                                     "WHERE Auftrag_ID='"+AuftragID+"' AND AuftragPos='"+AuftragPos+"' ";

            ada.Fill(ds);
            ada.Dispose();
            Command.Dispose();
            Globals.SQLcon.Close();

            return ds; 
        }
        //
        //---------- Update DB     -------------------------
        //
        public void updateAuftrag(int AuftragNr)
        {
            Date_Add = DateTime.Now;
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;

                //----- SQL Abfrage -----------------------
                Command.CommandText = "Update Auftrag SET KD_ID='" + KD_ID + "', " +
                                                            "B_ID='" + B_ID + "', " +
                                                            "E_ID='" + E_ID + "', " +
                                                            "T_Date='" + T_Date + "', " +
                                                            "ZF='" + ZF+ "', " +
                                                            "VSB='" + VSB+ "', " +
                                                            "Status='" + Status + "'," +
                                                            "Gewicht='" + Gewicht.ToString().Replace(",", ".") + "', " +
                                                
                                                            "Notiz='" + Notiz + "', " +
                                                            "Priorität='" + Prioritaet + "', " +
                                                            "Relation='" + Relation + "', " +
                                                            "vFracht='" + vFracht.ToString().Replace(",", ".") + "' " +
                                                                                   "WHERE ANr='" + AuftragNr + "'";

                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());

            }
            finally
            {
                //MessageBox.Show("Update durchgeführt");
            }
        }
        //
        //---------- Update DB     -------------------------
        //
        public static void updateAuftragsGesamtGewicht(int AuftragNr)
        {
          decimal Gewicht = clsArtikel.GetSumGewichtArtikelForAuftrag(AuftragNr);
          try
          {
            //--- initialisierung des sqlcommand---
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;

            //----- SQL Abfrage -----------------------
            Command.CommandText = "Update Auftrag SET Gewicht='" + Gewicht.ToString().Replace(",", ".") + "' " +
                                                                    "WHERE ANr='" + AuftragNr + "'";

            Globals.SQLcon.Open();
            Command.ExecuteNonQuery();
            Command.Dispose();
            Globals.SQLcon.Close();
          }
          catch (Exception ex)
          {
            System.Windows.Forms.MessageBox.Show(ex.ToString());

          }
          finally
          {
            //MessageBox.Show("Update durchgeführt");
          }
        }
        //
        //---------- Update DB ADR     -------------------------
        //
        public static void updateADR_ID(Int32 Auftragnummer, string ADRBezeichnung, Int32 ADR_ID)
        {
          string sql = string.Empty;
          try
          {
            //--- initialisierung des sqlcommand---
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;

            //----- SQL Abfrage -----------------------
            switch (ADRBezeichnung)
            { 
              case "Auftraggeber":
                sql = "Update Auftrag SET KD_ID='" + ADR_ID + "' WHERE ANr='" + Auftragnummer + "'";
                break;
              case "Versender":
                sql = "Update Auftrag SET B_ID='" + ADR_ID + "' WHERE ANr='" + Auftragnummer + "'";
                break;
              case "Empfänger":
                sql = "Update Auftrag SET E_ID='" + ADR_ID + "' WHERE ANr='" + Auftragnummer + "'";
                break;
            }

            Command.CommandText = sql;

            Globals.SQLcon.Open();
            Command.ExecuteNonQuery();
            Command.Dispose();
            Globals.SQLcon.Close();
          }
          catch (Exception ex)
          {
            System.Windows.Forms.MessageBox.Show(ex.ToString());

          }
          finally
          {
            //MessageBox.Show("Update durchgeführt");
          }
        }
        //
        //---------- Update Termine im Hauptauftrag     -------------------------
        //
        public static void updateTermine(int Auftrag, DateTime T_Date, DateTime VSB)
        {

            //--- initialisierung des sqlcommand---
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;

            //----- SQL Abfrage -----------------------
            Command.CommandText = "Update Auftrag SET  T_Date='" + T_Date.ToShortDateString() + "', " +
                                                       // "ZF='" + ZF.ToShortTimeString() + "', " +
                                                        "VSB='" + VSB.ToShortDateString()  + "' " +
                                                                               "WHERE ANr='" + Auftrag + "'";

            Globals.SQLcon.Open();
            Command.ExecuteNonQuery();
            Command.Dispose();
            Globals.SQLcon.Close();
            MessageBox.Show("Update durchgeführt");
        }
        //
        //---------------  Auftrag delete  -----------------------
        //
        public static void DeleteAuftrag(int AuftragID)
        {
            //--- initialisierung des sqlcommand---
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;

            //----- SQL Abfrage -----------------------
            Command.CommandText = "DELETE FROM Auftrag WHERE ANr='" + AuftragID + "'";
            Globals.SQLcon.Open();
            Command.ExecuteNonQuery();
            Command.Dispose();
            Globals.SQLcon.Close();
        }
        //
        //----------- Abfrage ctrAuftrag ------------------------------------
        //
        public static DataTable GetAuftragsdatenByZeitraumAndStatus(DateTime Date_von, DateTime Date_bis, Int32 Status, bool vergabe)
        {
          DataTable dataTable = new DataTable();
          try
          {
            dataTable.Clear();
            dataTable.Columns.Clear();
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            if (!vergabe)
            {
              if (Status == 3)
              {
                string strSQL = string.Empty;
                strSQL = "SELECT " +
                                              "AuftragPos.ID as 'ID', " +
                                              "Auftrag.ANr as 'AuftragID', " +
                                              "AuftragPos.AuftragPos as 'Pos' ," +
                                              "(Select ViewID FROM ADR WHERE ADR.ID =Auftrag.KD_ID) as 'Suchbegriff', " +
                                              "(Select ADR.A FROM ADR WHERE ADR.ID=Auftrag.KD_ID) as 'Ber_A', " +

                                              "(Select ADR.ID FROM ADR WHERE ADR.ID=Auftrag.B_ID) as 'BSID', " +
                                              "(Select ADR.Name1 FROM ADR WHERE ADR.ID= Auftrag.B_ID) as 'Beladestelle', " +
                                              "(Select ADR.Str FROM ADR WHERE ADR.ID= Auftrag.B_ID) as 'B_Strasse', " +
                                              "(Select ADR.PLZ FROM ADR WHERE ADR.ID= Auftrag.B_ID) as 'B_PLZ', " +
                                              "(Select ADR.Ort FROM ADR WHERE ADR.ID=Auftrag.B_ID) as 'B_Ort', " +
                                              "(Select ADR.V FROM ADR WHERE ADR.ID=Auftrag.B_ID) as 'Ber_V', " +

                                              "(Select ADR.ID FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'ESID', " +
                                              "(Select ADR.Name1 FROM ADR WHERE ADR.ID= Auftrag.E_ID) as 'Entladestelle', " +
                                              "(Select ADR.Str FROM ADR WHERE ADR.ID= Auftrag.E_ID) as 'E_Strasse', " +
                                              "(Select ADR.PLZ FROM ADR WHERE ADR.ID= Auftrag.E_ID) as 'E_PLZ', " +
                                              "(Select ADR.Ort FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'E_Ort', " +
                                              "(Select ADR.E FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'Ber_E', " +
                                              "(Select ADR.WAvon FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'WAv', " +
                                              "(Select ADR.WAbis FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'WAb', " +

                                              "(Select Top(1) Artikel.GArt FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID) as 'Gut', " +
                                              "(Select SUM(Artikel.gemGewicht) FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID AND Artikel.AuftragPos=AuftragPos.AuftragPos) as 'gemPosGewicht', " +
                                              "(Select SUM(Artikel.tatGewicht) FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID AND Artikel.AuftragPos=AuftragPos.AuftragPos) as 'tatPosGewicht', " +

                                              "(Select SUM(Artikel.gemGewicht) FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID) as 'gemGesamtGewicht', " +
                                              "(Select SUM(Artikel.tatGewicht) FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID) as 'tatGesamtGewicht', " +
                                              "(SELECT drDate FROM Lieferschein WHERE Lieferschein.AP_ID=AuftragPos.ID) as 'drDate', " +

                                              "AuftragPos.T_Date as 'faellig', " +
                                              "AuftragPos.VSB as 'VSB', " +
                                              "AuftragPos.ZF as 'ZF', " +
                                              "AuftragPos.Status as 'Stat', " +
                                              "Priorität as 'Pri', " +
                                              "AuftragPos.Papiere as 'Papiere', " +
                                              "AuftragPos.Fahrer as 'Fahrer', " +
                                              "AuftragPos.New as 'New', " +
                                              "AuftragPos.Ladenummer, " +
                                              "((Auftrag.Gewicht)- Gewicht) as 'VerlGewicht', " +
                                              "Auftrag.Relation as 'Relation' ";

                if ((Date_von == DateTime.MinValue) & (Date_bis == DateTime.MaxValue))
                {
                  strSQL = strSQL + "FROM AuftragPos " +
                                        "INNER JOIN Auftrag ON Auftrag.ANr=AuftragPos.Auftrag_ID " +
                                        "WHERE AuftragPos.Status='3' AND " +
                                        "AuftragPos.ID NOT IN (SELECT PosID FROM Kommission) " +
                                        "Order by Auftrag.T_Date ";
                }
                else
                {
                  strSQL = strSQL + "FROM AuftragPos " +
                      "INNER JOIN Auftrag ON Auftrag.ANr=AuftragPos.Auftrag_ID " +
                      "WHERE AuftragPos.Status='3' AND " +
                      "AuftragPos.T_Date>'" + Date_von.AddDays(-1).ToShortDateString() + "' AND AuftragPos.T_Date<'" + Date_bis.AddDays(1).ToShortDateString() + "' AND " +
                      "AuftragPos.ID NOT IN (SELECT PosID FROM Kommission) " +
                      "Order by 'faellig' ";
                }
                Command.CommandText = strSQL;
              }
              if (Status < 3)
              {
                Command.CommandText = "SELECT " +
                                              "AuftragPos.ID as 'ID', " +
                                              "Auftrag.ANr as 'AuftragID', " +
                                              "AuftragPos.AuftragPos as 'Pos' ," +
                                              "(Select ViewID FROM ADR WHERE ADR.ID =Auftrag.KD_ID) as 'Suchbegriff', " +
                                              "(Select ADR.A FROM ADR WHERE ADR.ID=Auftrag.KD_ID) as 'Ber_A', " +

                                              "(Select ADR.ID FROM ADR WHERE ADR.ID=Auftrag.B_ID) as 'BSID', " +
                                              "(Select ADR.Name1 FROM ADR WHERE ADR.ID= Auftrag.B_ID) as 'Beladestelle', " +
                                              "(Select ADR.Str FROM ADR WHERE ADR.ID= Auftrag.B_ID) as 'B_Strasse', " +
                                              "(Select ADR.PLZ FROM ADR WHERE ADR.ID= Auftrag.B_ID) as 'B_PLZ', " +
                                              "(Select ADR.Ort FROM ADR WHERE ADR.ID=Auftrag.B_ID) as 'B_Ort', " +
                                              "(Select ADR.V FROM ADR WHERE ADR.ID=Auftrag.B_ID) as 'Ber_V', " +

                                              "(Select ADR.ID FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'ESID', " +
                                              "(Select ADR.Name1 FROM ADR WHERE ADR.ID= Auftrag.E_ID) as 'Entladestelle', " +
                                              "(Select ADR.Str FROM ADR WHERE ADR.ID= Auftrag.E_ID) as 'E_Strasse', " +
                                              "(Select ADR.PLZ FROM ADR WHERE ADR.ID= Auftrag.E_ID) as 'E_PLZ', " +
                                              "(Select ADR.Ort FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'E_Ort', " +
                                              "(Select ADR.E FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'Ber_E', " +
                                              "(Select ADR.WAvon FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'WAv', " +
                                              "(Select ADR.WAbis FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'WAb', " +

                                              "(Select Top(1) Artikel.GArt FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID) as 'Gut', " +
                                              "(Select SUM(Artikel.gemGewicht) FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID AND Artikel.AuftragPos=AuftragPos.AuftragPos) as 'gemPosGewicht', " +
                                              "(Select SUM(Artikel.tatGewicht) FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID AND Artikel.AuftragPos=AuftragPos.AuftragPos) as 'tatPosGewicht', " +

                                              "(Select SUM(Artikel.gemGewicht) FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID) as 'gemGesamtGewicht', " +
                                              "(Select SUM(Artikel.tatGewicht) FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID) as 'tatGesamtGewicht', " +
                                              "(SELECT drDate FROM Lieferschein WHERE Lieferschein.AP_ID=AuftragPos.ID) as 'drDate', "+

                                              "AuftragPos.T_Date as 'faellig', " +
                                              "AuftragPos.VSB as 'VSB', " +
                                              "AuftragPos.ZF as 'ZF', " +
                                              "AuftragPos.Status as 'Stat', " +
                                              "Priorität as 'Pri', " +
                                              "AuftragPos.Papiere as 'Papiere', " +
                                              "AuftragPos.Fahrer as 'Fahrer', " +
                                              "AuftragPos.New as 'New', " +
                                              "AuftragPos.Ladenummer, " +
                                              "((Auftrag.Gewicht)- Gewicht) as 'VerlGewicht', " +
                                              "Auftrag.Relation as 'Relation' " +

                                              "FROM AuftragPos " +
                                                                    "INNER JOIN Auftrag ON Auftrag.ANr=AuftragPos.Auftrag_ID " +
                                                                    "WHERE AuftragPos.Status<'3' AND " +
                                                                    //"((AuftragPos.T_Date>'" + Date_von.AddDays(-1).ToShortDateString() + "' AND AuftragPos.T_Date<'" + Date_bis.AddDays(1).ToShortDateString() + "') "+
                                                                    "AuftragPos.T_Date<'" + Date_bis.AddDays(1).ToShortDateString() + "' " +
                                                                    "OR (AuftragPos.T_Date='"+DateTime.MaxValue+"') AND " +
                                                                    "AuftragPos.ID NOT IN (SELECT PosID FROM Kommission) " +
                                                                    "Order by 'faellig' ";
              }
              if (Status >= 4) //disponiert oder durchgeführt
              {
                Command.CommandText = "SELECT " +
                                              "AuftragPos.ID as 'ID', " +
                                              "Auftrag.ANr as 'AuftragID', " +
                                              "AuftragPos.AuftragPos as 'Pos' ," +
                                              "(Select ViewID FROM ADR WHERE ADR.ID =Auftrag.KD_ID) as 'Suchbegriff', " +
                                              "(Select ADR.A FROM ADR WHERE ADR.ID=Auftrag.KD_ID) as 'Ber_A', " +

                                              "(Select ADR.ID FROM ADR WHERE ADR.ID=Auftrag.B_ID) as 'BSID', " +
                                              "(Select ADR.Name1 FROM ADR WHERE ADR.ID= Auftrag.B_ID) as 'Beladestelle', " +
                                              "(Select ADR.Str FROM ADR WHERE ADR.ID= Auftrag.B_ID) as 'B_Strasse', " +
                                              "(Select ADR.PLZ FROM ADR WHERE ADR.ID= Auftrag.B_ID) as 'B_PLZ', " +
                                              "(Select ADR.Ort FROM ADR WHERE ADR.ID=Auftrag.B_ID) as 'B_Ort', " +
                                              "(Select ADR.V FROM ADR WHERE ADR.ID=Auftrag.B_ID) as 'Ber_V', " +

                                              "(Select ADR.ID FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'ESID', " +
                                              "(Select ADR.Name1 FROM ADR WHERE ADR.ID= Auftrag.E_ID) as 'Entladestelle', " +
                                              "(Select ADR.Str FROM ADR WHERE ADR.ID= Auftrag.E_ID) as 'E_Strasse', " +
                                              "(Select ADR.PLZ FROM ADR WHERE ADR.ID= Auftrag.E_ID) as 'E_PLZ', " +
                                              "(Select ADR.Ort FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'E_Ort', " +
                                              "(Select ADR.E FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'Ber_E', " +
                                              "(Select ADR.WAvon FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'WAv', " +
                                              "(Select ADR.WAbis FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'WAb', " +

                                              "(Select Top(1) Artikel.GArt FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID) as 'Gut', " +
                                              "(Select SUM(Artikel.gemGewicht) FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID AND Artikel.AuftragPos=AuftragPos.AuftragPos) as 'gemPosGewicht', " +
                                              "(Select SUM(Artikel.tatGewicht) FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID AND Artikel.AuftragPos=AuftragPos.AuftragPos) as 'tatPosGewicht', " +

                                              "(Select SUM(Artikel.gemGewicht) FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID) as 'gemGesamtGewicht', " +
                                              "(Select SUM(Artikel.tatGewicht) FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID) as 'tatGesamtGewicht', " +
                                              "(SELECT drDate FROM Lieferschein WHERE Lieferschein.AP_ID=AuftragPos.ID) as 'drDate', " +

                                              "AuftragPos.T_Date as 'faellig', " +
                                              "AuftragPos.VSB as 'VSB', " +
                                              "AuftragPos.ZF as 'ZF', " +
                                              "AuftragPos.Status as 'Stat', " +
                                              "Priorität as 'Pri', " +
                                              "AuftragPos.Papiere as 'Papiere', " +
                                              "AuftragPos.Fahrer as 'Fahrer', " +
                                              "AuftragPos.New as 'New', " +
                                              "AuftragPos.Ladenummer, " +
                                              "((Auftrag.Gewicht)- Gewicht) as 'VerlGewicht', " +
                                              "Auftrag.Relation as 'Relation', " +
                                              "(Select Kommission.B_Zeit FROM Kommission WHERE Kommission.Auftrag=Auftrag.ANr AND Kommission.AuftragPos=AuftragPos.AuftragPos)  as 'Beladezeit', " +
                                              "(Select Kommission.E_Zeit FROM Kommission WHERE Kommission.Auftrag=Auftrag.ANr AND Kommission.AuftragPos=AuftragPos.AuftragPos)   as 'Entladezeit' " +
                                              "FROM AuftragPos " +
                                                                    "INNER JOIN Auftrag ON Auftrag.ANr=AuftragPos.Auftrag_ID " +
                                                                    "WHERE AuftragPos.Status='" + Status + "' AND " +
                                                                    "((AuftragPos.T_Date>'" + Date_von.AddDays(-1).ToShortDateString() + "' AND AuftragPos.T_Date<'" + Date_bis.AddDays(1).ToShortDateString() + "') " +
                                                                    "OR (AuftragPos.T_Date='" + DateTime.MaxValue + "')) AND " +
                                                                    "AuftragPos.ID IN (SELECT PosID FROM Kommission) " +
                                                                    "Order by 'faellig' ";
              }
            }
            else // an SU vergeben
            {
              

              Command.CommandText = "SELECT " +
                                        "AuftragPos.ID as 'ID', " +
                                        "Auftrag.ANr as 'AuftragID', " +
                                        "AuftragPos.AuftragPos as 'Pos' ," +
                                        "(Select ViewID FROM ADR WHERE ADR.ID =Auftrag.KD_ID) as 'Suchbegriff', " +
                                        "(Select ADR.A FROM ADR WHERE ADR.ID=Auftrag.KD_ID) as 'Ber_A', " +

                                        "(Select ADR.ID FROM ADR WHERE ADR.ID=Auftrag.B_ID) as 'BSID', " +
                                        "(Select ADR.Name1 FROM ADR WHERE ADR.ID= Auftrag.B_ID) as 'Beladestelle', " +
                                        "(Select ADR.Str FROM ADR WHERE ADR.ID= Auftrag.B_ID) as 'B_Strasse', " +
                                        "(Select ADR.PLZ FROM ADR WHERE ADR.ID= Auftrag.B_ID) as 'B_PLZ', " +
                                        "(Select ADR.Ort FROM ADR WHERE ADR.ID=Auftrag.B_ID) as 'B_Ort', " +
                                        "(Select ADR.V FROM ADR WHERE ADR.ID=Auftrag.B_ID) as 'Ber_V', " +

                                        "(Select ADR.ID FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'ESID', " +
                                        "(Select ADR.Name1 FROM ADR WHERE ADR.ID= Auftrag.E_ID) as 'Entladestelle', " +
                                        "(Select ADR.Str FROM ADR WHERE ADR.ID= Auftrag.E_ID) as 'E_Strasse', " +
                                        "(Select ADR.PLZ FROM ADR WHERE ADR.ID= Auftrag.E_ID) as 'E_PLZ', " +
                                        "(Select ADR.Ort FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'E_Ort', " +
                                        "(Select ADR.E FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'Ber_E', " +
                                        "(Select ADR.WAvon FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'WAv', " +
                                        "(Select ADR.WAbis FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'WAb', " +

                                        "(Select Top(1) Artikel.GArt FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID) as 'Gut', " +
                                        "(Select SUM(Artikel.gemGewicht) FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID AND Artikel.AuftragPos=AuftragPos.AuftragPos) as 'gemPosGewicht', " +
                                        "(Select SUM(Artikel.tatGewicht) FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID AND Artikel.AuftragPos=AuftragPos.AuftragPos) as 'tatPosGewicht', " +

                                        "(Select SUM(Artikel.gemGewicht) FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID) as 'gemGesamtGewicht', " +
                                        "(Select SUM(Artikel.tatGewicht) FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID) as 'tatGesamtGewicht', " +
                                        "(SELECT drDate FROM Lieferschein WHERE Lieferschein.AP_ID=AuftragPos.ID) as 'drDate', " +

                                        "AuftragPos.T_Date as 'faellig', " +
                                        "AuftragPos.VSB as 'VSB', " +
                                        "AuftragPos.ZF as 'ZF', " +
                                        "AuftragPos.Status as 'Stat', " +
                                        "Priorität as 'Pri', " +
                                        "AuftragPos.Papiere as 'Papiere', " +
                                        "AuftragPos.Fahrer as 'Fahrer', " +
                                        "AuftragPos.New as 'New', " +
                                        "AuftragPos.Ladenummer, " +
                                        "((Auftrag.Gewicht)- Gewicht) as 'VerlGewicht', " +
                                        "Auftrag.Relation as 'Relation', " +
                                        "(Select Frachtvergabe.B_Date FROM Frachtvergabe WHERE Frachtvergabe.ID_AP=AuftragPos.ID) as 'Beladezeit', " +
                                        "(Select Frachtvergabe.E_Date FROM Frachtvergabe WHERE Frachtvergabe.ID_AP=AuftragPos.ID) as 'Entladezeit', " +
                                        "(Select Frachtvergabe.SU FROM Frachtvergabe WHERE Frachtvergabe.ID_AP=AuftragPos.ID) as 'SU_ID' " +
                                        "FROM AuftragPos " +
                                                      "INNER JOIN Auftrag ON Auftrag.ANr=AuftragPos.Auftrag_ID "+
                                                      "WHERE (AuftragPos.Status>'3' AND AuftragPos.Status<'7') AND " +
                                                      "((AuftragPos.T_Date>'" + Date_von.AddDays(-1).ToShortDateString() + "' AND AuftragPos.T_Date<'" + Date_bis.AddDays(1).ToShortDateString() + "') " +
                                                      "OR (AuftragPos.T_Date='" + DateTime.MaxValue + "')) AND " + 
                                                      "AuftragPos.ID IN (SELECT ID_AP FROM Frachtvergabe) " +
                                                      "Order by 'Beladezeit' ";

            }
            ada.Fill(dataTable);

            ada.Dispose();
            Command.Dispose();
            Globals.SQLcon.Close();

            if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
            {
              Command.Connection.Close();
            }
            return dataTable;
          }
          catch (Exception ex)
          {
            MessageBox.Show(ex.ToString());
          }
          return dataTable;
        }
        //
        public static DataSet GetGewichtAndVerteiltesGewicht(Int32 AuftragID)
        {
          DataSet ds = new DataSet();
          SqlDataAdapter ada = new SqlDataAdapter();
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          ada.SelectCommand = Command;
          Command.CommandText = "SELECT (SELECT SUM(gemGewicht) FROM Artikel WHERE AuftragID='"+AuftragID+"') as 'Gewicht', " +
                                       "(SELECT SUM(gemGewicht) FROM Artikel WHERE AuftragID='" + AuftragID + "' AND AuftragPos>'0') as 'verteiltesGewicht' " +
                                       "From Auftrag WHERE Auftrag.ANr='" + AuftragID + "'";

          ada.Fill(ds);
          Command.Dispose();
          ada.Dispose();
          Globals.SQLcon.Close();
          if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
          {
            Command.Connection.Close();
          }
          return ds;
        }
      //
      //
      public static DataTable GetADR_IDbyAuftragID(Int32 auftrag)
      {
          DataTable ADR = new DataTable();
          ADR.Clear();
          SqlDataAdapter ada = new SqlDataAdapter();
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          ada.SelectCommand = Command;
          Command.CommandText = "SELECT " +
                                  "ID, KD_ID, B_ID, E_ID " +
                                  "FROM Auftrag WHERE ANr='" + auftrag + "'";

          ada.Fill(ADR);
          Command.Dispose();
          ada.Dispose();
          Globals.SQLcon.Close();
          if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
          {
            Command.Connection.Close();
          }
          return ADR;

      }
      //
      //---------------- vorgemerkte Fracht --------------------
      //
      public static decimal GetVFrachtByAuftragID(Int32 auftrag)
      {
        decimal vFracht = 0.0m;
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;
        Command.CommandText = "SELECT vFracht FROM Auftrag WHERE ANr='" + auftrag + "'";
        Globals.SQLcon.Open();
        object obj = Command.ExecuteScalar();
        if (obj != null)
        {
          vFracht = (decimal)obj;
        }
        Command.Dispose();
        Globals.SQLcon.Close();
        return vFracht;

      }
    }
}
