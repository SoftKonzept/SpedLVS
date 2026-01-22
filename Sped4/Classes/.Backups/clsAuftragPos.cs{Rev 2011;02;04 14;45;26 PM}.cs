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
  class clsAuftragPos
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
    private int _AuftragPos;
    private int _Auftrag_ID;
    private DateTime? _T_Date;
    private DateTime? _ZF;
    private DateTime? _VSB;
    private int _Status;
    private decimal _gemPosGewicht;
    private decimal _tatPosGewicht;
    private decimal _gemGesamtGewicht;
    private decimal _tatGesamtGewicht;
    private string _Ladenummer;
    private bool _Papiere;
    private char _Fahrer;
    private char _NewAuftrag;
    private string _Notiz;
    private bool _LadeNrRequire;


    public int ID
    {
      get
      {
        try
        {
          DataTable PosTable = new DataTable();
          SqlDataAdapter ada = new SqlDataAdapter();
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          ada.SelectCommand = Command;
          Command.CommandText = "SELECT MAX(ID) FROM AuftragPos";
          Globals.SQLcon.Open();

          string returnVal = Command.ExecuteScalar().ToString();

          if (returnVal == "")
          {
            _ID = 1;
          }
          else
          {
            _ID = 1 + (int)Command.ExecuteScalar();
          }
          Command.Dispose();
          Globals.SQLcon.Close();
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.ToString());
        }

        return _ID;
      }
      set { _ID = value; }
    }
    public int AuftragPos
    {
      get { return _AuftragPos; }
      set { _AuftragPos = value; }
    }
    public int Auftrag_ID
    {
      get { return _Auftrag_ID; }
      set { _Auftrag_ID = value; }
    }
    public DateTime? T_Date
    {
      get { return _T_Date; }
      set { _T_Date = value; }
    }
    public DateTime? ZF
    {
      get { return _ZF; }
      set { _ZF = value; }
    }
    public DateTime? VSB
    {
      get { return _VSB; }
      set { _VSB = value; }
    }
    public int Status
    {
      get { return _Status; }
      set { _Status = value; }
    }
    public string Ladenummer
    {
      get { return _Ladenummer; }
      set { _Ladenummer = value; }
    }
    public bool Papiere
    {
      get { return _Papiere; }
      set { _Papiere = value; }
    }
    public char Fahrer
    {
      get { return _Fahrer; }
      set { _Fahrer = value; }
    }
    public char NewAuftrag
    {
      get { return _NewAuftrag; }
      set { _NewAuftrag = value; }
    }
    public decimal gemPosGewicht
    {
      get { return _gemPosGewicht; }
      set { _gemPosGewicht = value; }
    }
    public decimal tatPosGewicht
    {
      get { return _tatPosGewicht; }
      set { _tatPosGewicht = value; }
    }
    public decimal gemGesamtGewicht
    {
      get { return _gemGesamtGewicht; }
      set { _gemGesamtGewicht = value; }
    }
    public decimal tatGesamtGewicht
    {
      get { return _tatGesamtGewicht; }
      set { _tatGesamtGewicht = value; }
    }
    public string Notiz
    {
      get { return _Notiz; }
      set { _Notiz = value; }
    }
    public bool LadeNrRequire
    {
      get { return _LadeNrRequire; }
      set { _LadeNrRequire = value; }
    }

    //**********************************************************************************
    //--------------              Methoden
    //**********************************************************************************
    //
    //
    //
    public void Add(bool AusAuftragSplitting)
    {
      Int32 iPapiere = 0;
      DateTime dtPapiere = DateTime.MaxValue;
      if (Papiere)
      {
        iPapiere = 1;
        dtPapiere = DateTime.Today;
      }
      Fahrer = Convert.ToChar('F');
      NewAuftrag = Convert.ToChar('T');

      Int32 valLadeNrReq = 0;
      if (LadeNrRequire)
      {
        valLadeNrReq = 1;
      }

      try
      {
        //--- initialisierung des sqlcommand---
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;

        //----- SQL Abfrage -----------------------
        Command.CommandText = ("INSERT INTO AuftragPos (AuftragPos, Auftrag_ID, T_Date, ZF, VSB, Status, Ladenummer, LadeNrRequire, Fahrer, New, Notiz,  Papiere) " +
                                "VALUES ('" + AuftragPos + "','"     // ist 0 steht momentan in Artikel
                                            + Auftrag_ID + "','"
                                            + T_Date + "','"
                                            + ZF + "','"
                                            + VSB + "','"
                                            + Status + "','"
                                            + Ladenummer + "','"
                                            + valLadeNrReq + "','"
                                            + Fahrer + "','"
                                            + NewAuftrag + "','"
                                            + Notiz + "','"
                                            + iPapiere +  "')");

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
        //System.Windows.Forms.MessageBox.Show(ex.ToString());
        //Add Logbucheintrag Exception
        string Beschreibung = "Exception: " + ex;
        Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Exception.ToString(), Beschreibung);
      }
      finally
      {
        //Add Logbucheintrag Eintrag
        if (AusAuftragSplitting)
        {
          string Beschreibung = "Auftragsplitting - Auftrag ID:" + Auftrag_ID + " - Auftragsposition: " + AuftragPos + " hinzugefügt";
          Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Auftragsplitting.ToString(), Beschreibung);
        }
        else
        {
          string Beschreibung = "Auftrag - Auftrag ID:" + Auftrag_ID + " - Auftragsposition: " + AuftragPos + " hinzugefügt";
          Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Eintrag.ToString(), Beschreibung);
        }
      }
    }
    //
    //---------- Update DB     -------------------------
    //
    public void updateAuftragPos(int AuftragNr, int AuftragPos)
    {
      Auftrag_ID = AuftragNr;
      NewAuftrag = 'T';
      Int32 valLadeNrReq = 0;
      if (LadeNrRequire)
      {
        valLadeNrReq = 1;
      }
      try
      {
        //--- initialisierung des sqlcommand---
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;

        //----- SQL Abfrage -----------------------
        Command.CommandText = "Update AuftragPos SET AuftragPos='" + AuftragPos + "', " +
                                                        "T_Date='" + T_Date + "', " +
                                                        "ZF='" + ZF + "', " +
                                                        "VSB='" + VSB + "', " +
                                                        "Status='" + Status + "', " +
                                                        "Ladenummer='" + Ladenummer + "', " +
                                                        "LadeNrRequire='" + valLadeNrReq + "', " +
                                                        "New='" + NewAuftrag + "' " +
                                                        "WHERE Auftrag_ID='" + AuftragNr + "' AND AuftragPos='" + AuftragPos + "'";

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
        //System.Windows.Forms.MessageBox.Show(ex.ToString());
        //Add Logbucheintrag Exception
        string Beschreibung = "Exception: " + ex;
        Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Exception.ToString(), Beschreibung);
      }
      finally
      {
        //Add Logbucheintrag update
        string Beschreibung = "Auftrag - Auftrag ID:" + Auftrag_ID + " - Auftragsposition: " + AuftragPos + " geändert";
        Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
      }
    }
    //
    //---------- Update DB / CHange POS ID    -------------------------
    //
    public void updateAuftragPosByID(Int32 AuftragPosID)
    {
      //AuftragPosID = ID der DB nicht die AuftragPos Nummer
      NewAuftrag = 'T';
      Int32 valLadeNrReq = 0;
      if (LadeNrRequire)
      {
        valLadeNrReq = 1;
      }
      try
      {
        //--- initialisierung des sqlcommand---
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;

        //----- SQL Abfrage -----------------------
        Command.CommandText = "Update AuftragPos SET AuftragPos='" + AuftragPos + "', " +
                                                        "T_Date='" + T_Date + "', " +
                                                        "ZF='" + ZF + "', " +
                                                        "VSB='" + VSB + "', " +
                                                        "Status='" + Status + "', " +
                                                        "Ladenummer='" + Ladenummer + "', " +
                                                        "LadeNrRequire='" + valLadeNrReq + "', " +
                                                        "New='" + NewAuftrag + "' " +
                                                        "WHERE ID='"+AuftragPosID+"'";

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
        //System.Windows.Forms.MessageBox.Show(ex.ToString());
        //Add Logbucheintrag Exception
        string Beschreibung = "Exception: " + ex;
        Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Exception.ToString(), Beschreibung);
      }
      finally
      {
        //Add Logbucheintrag update
        string Beschreibung = "Auftrag - ID: "+AuftragPosID+" Auftrag ID:" + Auftrag_ID + " - Auftragsposition: " + AuftragPos + " geändert";
        Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
      }
    }
    //
    //---------- Update Status -----------------------
    //
    public static void updateStatusByAuftragAuftragPos(Int32 AuftragNr, Int32 AuftragPos, Int32 status, Int32 BenutzerID)
    {
      try
      {
        //--- initialisierung des sqlcommand---
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;

        //----- SQL Abfrage -----------------------
        Command.CommandText = "Update AuftragPos SET  Status='" + status + "' " +
                                               "WHERE Auftrag_ID='" + AuftragNr + "' AND AuftragPos='" + AuftragPos + "'";

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
        //System.Windows.Forms.MessageBox.Show(ex.ToString());
        //Add Logbucheintrag Exception
        string Beschreibung = "Exception: " + ex;
        Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Exception.ToString(), Beschreibung);
      }
      finally
      {
        //Add Logbucheintrag update
        string stat = clsAuftragsstatus.GetStatusbeschreibung(status);
        string Beschreibung = "Auftrag Statusänderung - Auftrag ID:" + AuftragNr+ " - Auftragsposition: " + AuftragPos + " - Status: "+ stat  +" geändert";
        Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
      }
    }
    //
    //--------------- Get Auftragsdaten aus DB über Auftragsnummer und Auftragsposition ----------------
    //
    public static DataSet ReadDataByID(int AuftragID, int Pos)  //muss raus 20.07.2010
    {
      DataSet ds = new DataSet();
      ds.Clear();
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();

      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT " +
                                        "Auftrag.ANr, " +
                                        "AuftragPos.AuftragPos," +
                                        "Auftrag.ADate," +
                                        "(Select Top(1)Artikel.GArt From Artikel WHERE Artikel.AuftragID=Auftrag.ANr) as 'Gut', " +
                                        "(SELECT ADR.ID FROM ADR WHERE ADR.ID=Auftrag.KD_ID) as 'KD_ID'," +
                                        "(SELECT ADR.Name1 FROM ADR WHERE ADR.ID=Auftrag.KD_ID) as 'Auftraggeber'," +

                                        "(SELECT ADR.ID FROM ADR WHERE ADR.ID=Auftrag.B_ID) as 'B_ID'," +
                                        "(Select ADR.Name1 FROM ADR WHERE ADR.ID=Auftrag.B_ID) as 'Beladestelle', " +
                                        "(Select ADR.PLZ FROM ADR WHERE ADR.ID= Auftrag.B_ID) as 'B_PLZ', " +
                                        "(Select ADR.Ort FROM ADR WHERE ADR.ID=Auftrag.B_ID) as 'B_Ort', " +

                                        "(SELECT ADR.ID FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'E_ID'," +
                                        "(Select ADR.Name1 FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'Entladestelle', " +
                                        "(Select ADR.PLZ FROM ADR WHERE ADR.ID= Auftrag.E_ID) as 'E_PLZ', " +
                                        "(Select ADR.Ort FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'E_Ort', " +

                                        "AuftragPos.T_Date as 'Liefertermin', " +
                                        "AuftragPos.ZF as 'ZF', " +
                                        "AuftragPos.VSB, " +
                                        "(Select SUM(tatGewicht) FROM Artikel WHERE AuftragID=Auftrag.ANr AND AuftragPos=AuftragPos.AuftragPos) as 'Gewicht', " +
                                        "AuftragPos.Ladenummer, " +
                                        "AuftragPos.Status " +

                                        "FROM Auftrag " +
                                        "INNER JOIN AuftragPos ON Auftrag.ANr = AuftragPos.Auftrag_ID " +
                                        "WHERE AuftragPos.AuftragPos='" + Pos + "' " +
                                        "AND AuftragPos.Auftrag_ID='" + AuftragID + "'";

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
    //--------------- Get Auftragsdaten aus DB  über die ID der Tabelle----------------
    //
    public static DataSet GetAuftragPosRecByID(Int32 AuftragPosID, bool bo_tatGewicht)
    {
      string strSql = string.Empty;
      DataSet ds = new DataSet();
      ds.Clear();
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();

      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      strSql = "SELECT " +
                                        "AuftragPos.Auftrag_ID, " +
                                        "AuftragPos.AuftragPos," +
                                        "Auftrag.ADate," +
                                        "(Select TOP(1)Artikel.GArt From Artikel WHERE Artikel.AuftragID=Auftrag.ANr) as 'Gut', " +
                                        "(SELECT ADR.Name1 FROM ADR WHERE ADR.ID=Auftrag.KD_ID) as 'Auftraggeber'," +

                                        "(Select ADR.Name1 FROM ADR WHERE ADR.ID=Auftrag.B_ID) as 'Beladestelle', " +
                                         "(Select ADR.Str FROM ADR WHERE ADR.ID=Auftrag.B_ID) as 'B_Strasse', " +
                                        "(Select ADR.PLZ FROM ADR WHERE ADR.ID= Auftrag.B_ID) as 'B_PLZ', " +
                                        "(Select ADR.Ort FROM ADR WHERE ADR.ID=Auftrag.B_ID) as 'B_Ort', " +

                                        "(Select ADR.Name1 FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'Entladestelle', " +
                                        "(Select ADR.Str FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'E_Strasse', " +
                                        "(Select ADR.PLZ FROM ADR WHERE ADR.ID= Auftrag.E_ID) as 'E_PLZ', " +
                                        "(Select ADR.Ort FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'E_Ort', " +

                                        "AuftragPos.T_Date," +
                                        "AuftragPos.ZF, " +
                                        "AuftragPos.VSB, ";
      if (bo_tatGewicht)
      {
        strSql = strSql + "(Select SUM(tatGewicht) FROM Artikel WHERE AuftragID=AuftragPos.Auftrag_ID AND AuftragPos=AuftragPos.AuftragPos) as 'Gewicht', ";
      }
      else
      {
        strSql = strSql + "(Select SUM(gemGewicht) FROM Artikel WHERE AuftragID=AuftragPos.Auftrag_ID AND AuftragPos=AuftragPos.AuftragPos) as 'Gewicht', ";
      }

      strSql = strSql + "AuftragPos.Ladenummer " +

                                          "FROM AuftragPos " +
                                          "INNER JOIN Auftrag ON Auftrag.ANr = AuftragPos.Auftrag_ID " +
                                          "WHERE AuftragPos.ID='" + AuftragPosID + "'";

      Command.CommandText = strSql;
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
    //------------ MAx AuftragsPos -----------------
    //
    public static int GetAuftPosNr(int AuftragID)
    {
      int AufPos = 0;
      try
      {
        DataTable PosTable = new DataTable();
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;
        Command.CommandText = "SELECT MAX(AuftragPos) FROM AuftragPos WHERE Auftrag_ID='" + AuftragID + "'";
        Globals.SQLcon.Open();

        object returnVal = Command.ExecuteScalar().ToString();

        if (returnVal != null)
        {
          AufPos = 1 + (Int32)Command.ExecuteScalar();
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
      return AufPos;
    }
    //
    //--------------- Update des Gewichts bei Splittung von Unteraufträgen  ---------------
    //
    public static void UpdateGewichtAuftragPosition(int AuftragID, int AuftragPosition, decimal NeuesGewicht, Int32 BenutzerID)
    {
      try
      {
        //--- initialisierung des sqlcommand---
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;

        //----- SQL Abfrage -----------------------
        Command.CommandText = "Update AuftragPos SET Gewicht='" + NeuesGewicht.ToString().Replace(",", ".") + "' " +
                                                            "WHERE Auftrag_ID='" + AuftragID + "' AND AuftragPos='" + AuftragPosition + "'";

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
        //MessageBox.Show(ex.ToString());
        //Add Logbucheintrag Exception
        string Beschreibung = "Exception: " + ex;
        Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Exception.ToString(), Beschreibung);
      }
      finally
      {
        //Add Logbucheintrag update
        string Beschreibung = "Auftrag - Auftrag ID: " + AuftragID + " - Auftragsposition: " + AuftragPosition + " - Gewicht auf "+NeuesGewicht+" kg geändert";
        Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
      }
    }
    //
    //---------------  AuftragPos delete / auflösen -----------------------
    //
    public static void DeleteAuftrag(Int32 AuftragID, Int32 AuftragPos, Int32 BenutzerID)
    {
      //Add Logbucheintrag Löschen
      string Beschreibung = "Auftragsplitting - Auftrag ID: " + AuftragID + " - Auftragsposition: " + AuftragPos + " aufgelöst";
      Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Loeschung.ToString(), Beschreibung);

      //--- initialisierung des sqlcommand---
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;

      //----- SQL Abfrage -----------------------
      if (AuftragPos == 0)
      {
        Command.CommandText = "DELETE FROM AuftragPos WHERE Auftrag_ID='" + AuftragID + "'";
      }
      else
      {
        Command.CommandText = "DELETE FROM AuftragPos WHERE Auftrag_ID='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "'";
      }

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
    //--- Max AuftragPos auf 0 setzten --------------------------
    //
    public static void ChangeAuftragPosToZero(Int32 Auftrag, Int32 altePos)
    {
      try
      {
        //--- initialisierung des sqlcommand---
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;

        //----- SQL Abfrage -----------------------
        Command.CommandText = ("Update AuftragPos SET AuftragPos ='0' " +
                                                      "WHERE AuftragID='" + Auftrag + "' AND AuftragPos='" + altePos + "'");
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
    //
    //--------------- Update NEW / gelesen   ---------------
    //
    public static void NewAuftragGelesen(int AuftragID)
    {
      try
      {
        //--- initialisierung des sqlcommand---
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;

        //----- SQL Abfrage -----------------------
        Command.CommandText = "Update AuftragPos SET New='F' " +
                                                            "WHERE Auftrag_ID='" + AuftragID + "'"; // AND AuftragPos='" + AuftragPosition + "'";

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
        MessageBox.Show(ex.ToString());
      }
    }
    //
    //--------------- Check Gewicht Pos ------------------
    //
    public static Decimal GetGewichtFromAuftragPos(Int32 AuftragID, Int32 AuftragPos, bool bo_tatGewicht)
    {
      string strSql = string.Empty;
      decimal ReturnValue;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      if (bo_tatGewicht)
      {
        strSql = "SELECT SUM(tatGewicht) FROM Artikel " +
                         "WHERE AuftragID='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "'";
      }
      else
      {
        strSql = "SELECT SUM(gemGewicht) FROM Artikel " +
                         "WHERE AuftragID='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "'";
      }
      Command.CommandText = strSql;
      Globals.SQLcon.Open();
      ReturnValue = Convert.ToDecimal(Command.ExecuteScalar());
      Globals.SQLcon.Close();
      if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
      {
        Command.Connection.Close();
      }
      return ReturnValue;
    }

    //
    //----------------------MAX AuftragPos ------------------------
    //
    public Int32 GetMaxAuftragsPos(Int32 Auftrag)
    {
      Int32 reVal = 0;
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      Command.CommandText = "Select MAX(AuftragPos) FROM AuftragPos WHERE Auftrag_ID='" + Auftrag + "'";
      Globals.SQLcon.Open();
      if (Command.ExecuteScalar() is DBNull)
      {
        reVal = 0;
      }
      else
      {
        reVal = (Int32)Command.ExecuteScalar();
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      return reVal;
    }
    //
    //----------------------MAX AuftragPos ------------------------
    //
    public Int32 GetIDbyAuftragIDandAuftragPos(Int32 auftrag, Int32 auftragPos)
    {
      Int32 reVal = 0;
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      Command.CommandText = "Select ID FROM AuftragPos WHERE Auftrag_ID='" + auftrag + "' AND AuftragPos='"+auftragPos+"'";
      Globals.SQLcon.Open();
      object obj = Command.ExecuteScalar();
      if (obj == null)
      {
        reVal = 0;
      }
      else
      {
        reVal = (Int32)obj;
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      return reVal;
    }
    //
    //----------------------Status abfrage------------------------
    //
    public static Int32 GetStatus(Int32 Auftrag, Int32 AuftragsPos)
    {
      Int32 reVal = 0;
      if (IsAuftragPosIn(Auftrag, AuftragsPos))
      {

        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        Command.CommandText = "Select Status FROM AuftragPos WHERE Auftrag_ID='" + Auftrag + "' AND AuftragPos='" + AuftragsPos + "'";
        Globals.SQLcon.Open();

        if (Command.ExecuteScalar() is DBNull)
        {
          reVal = 0;
        }
        else
        {
          reVal = (Int32)Command.ExecuteScalar();
        }
        Command.Dispose();
        Globals.SQLcon.Close();
      }
      return reVal;
    }
    //
    //----------------------AuftragPos vorhanden?------------------------
    //
    public static bool IsAuftragPosIn(Int32 Auftrag, Int32 AuftragsPos)
    {
      bool IsIn = false;
      Int32 reVal = 0;
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      Command.CommandText = "Select ID FROM AuftragPos WHERE Auftrag_ID='" + Auftrag + "' AND AuftragPos='" + AuftragsPos + "'";
      Globals.SQLcon.Open();

      if (Command.ExecuteScalar() == null)
      {
        IsIn = false;
      }
      else
      {
        IsIn = true;
        reVal = (Int32)Command.ExecuteScalar();
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      return IsIn;
    }
    //
    //---------- Auftragsdaten für die Frachtvergabe an SU --------------------------------
    //
    public static DataTable ReadDataByAuftragIDandAuftragPos(int AuftragID, int Pos)
    {
      DataTable dt = new DataTable("Auftrag");
      dt.Clear();
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();

      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT " +
                                        "AuftragPos.Auftrag_ID as 'Auftrag_ID', " +
                                        "AuftragPos.AuftragPos as 'AuftragPos'," +
                                        "(SELECT ADate FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID) as 'Auftragsdatum', " +
                                        "(Select Top(1)Artikel.GArt From Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID) as 'Gut', " +
                                        "(SELECT KD_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID) as 'KD_ID', " +
                                        "(SELECT ADR.Name1 FROM ADR WHERE ADR.ID=(SELECT KD_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'Auftraggeber'," +

                                        "(SELECT B_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID) as 'B_ID', " +
                                        "(Select ADR.Name1 FROM ADR WHERE ADR.ID=(SELECT B_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'Beladestelle', " +
                                        "(Select ADR.PLZ FROM ADR WHERE ADR.ID= (SELECT B_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'B_PLZ', " +
                                        "(Select ADR.Ort FROM ADR WHERE ADR.ID=(SELECT B_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'B_Ort', " +

                                        "(SELECT E_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID) as 'E_ID', " +
                                        "(SELECT ADR.ID FROM ADR WHERE ADR.ID=(SELECT E_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'E_ID'," +
                                        "(Select ADR.Name1 FROM ADR WHERE ADR.ID=(SELECT E_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'Entladestelle', " +
                                        "(Select ADR.PLZ FROM ADR WHERE ADR.ID= (SELECT E_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'E_PLZ', " +
                                        "(Select ADR.Ort FROM ADR WHERE ADR.ID=(SELECT E_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'E_Ort', " +

                                        "AuftragPos.T_Date as 'Liefertermin', " +
                                        "AuftragPos.ZF as 'ZF', " +
                                        "AuftragPos.VSB, " +
                                        "(Select SUM(gemGewicht) FROM Artikel WHERE AuftragID=Auftrag_ID AND AuftragPos=AuftragPos.AuftragPos) as 'GesamtGemGewicht', "+
                                        "(Select SUM(tatGewicht) FROM Artikel WHERE AuftragID=Auftrag_ID AND AuftragPos=AuftragPos.AuftragPos) as 'GesamtTatGewicht', "+
                                        "AuftragPos.Ladenummer, "+
                                        "AuftragPos.Status " +
                                        "FROM Auftrag " +
                                        "INNER JOIN AuftragPos ON Auftrag.ANr = AuftragPos.Auftrag_ID " +
                                        "WHERE AuftragPos.AuftragPos='" + Pos + "' " +
                                        "AND AuftragPos.Auftrag_ID='" + AuftragID + "'";
      ada.Fill(dt);
      Command.Dispose();
      ada.Dispose();
      Globals.SQLcon.Close();
      if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
      {
        Command.Connection.Close();
      }
      return dt;
    }
    //
    //------------------ ID by Auftrag and AuftragPos ---------------
    //
    public static Int32 GetIDbyAuftragAndAuftragPos(Int32 Auftrag, Int32 AuftragPos)
    {
      Int32 ID = 0;
      try
      {
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;
        Command.CommandText = "SELECT ID FROM AuftragPos WHERE Auftrag_ID='" + Auftrag + "' AND AuftragPos='" + AuftragPos + "'";
        Globals.SQLcon.Open();

        object returnVal = Command.ExecuteScalar();

        if (returnVal != null)
        {
            ID = (Int32)returnVal;
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
      return ID;
    }    
    //
    //------------------ Auftrag By ID ---------------
    //
    public static Int32 GetAuftragIDByID(Int32 AP_ID)
    {
      Int32 apID = 0;
      try
      {
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;
        Command.CommandText = "SELECT Auftrag_ID FROM AuftragPos WHERE ID='" + AP_ID + "'";
        Globals.SQLcon.Open();

        object returnVal = Command.ExecuteScalar();

        if (returnVal != null)
        {
          apID = (Int32)returnVal;
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
      return apID;
    }
    //
    //------------------ Auftragpos By ID ---------------
    //
    public static Int32 GetAuftragPosByID(Int32 AP_ID)
    {
      Int32 apID = 0;
      try
      {
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;
        Command.CommandText = "SELECT AuftragPos FROM AuftragPos WHERE ID='" + AP_ID + "'";
        Globals.SQLcon.Open();

        object returnVal = Command.ExecuteScalar();

        if (returnVal != null)
        {
          apID = (Int32)returnVal;
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
      return apID;
    }
    //
    //---------------- Update Papiere erstellt - fehlt noch der User (20.07.2010)-------------
    //
    public void UdpadtePapiere(bool PapiereErstellt, Int32 AuftragID, Int32 AuftragPos)
    {
      Int32 iPapiere = 0;
      if (PapiereErstellt)
      {
        iPapiere = 1;
      }
      try
      {
        //--- initialisierung des sqlcommand---
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;

        //----- SQL Abfrage -----------------------
        Command.CommandText = "Update AuftragPos SET Papiere='" + iPapiere + "' " +
                                           "WHERE Auftrag_ID='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "'";

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
        MessageBox.Show(ex.ToString());
      }
    }
  }
}
