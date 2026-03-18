using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using Sped4;
using Sped4.Classes;

namespace Sped4.Classes
{
  public class clsKommission
  {
    //************  User  ***************
    private Int32 _BenutzerID;
    public Int32 BenutzerID
    {
      get { return _BenutzerID; }
      set { _BenutzerID = value; }
    }
    //************************************

    private Int32 _ID;
    private Int32 _AuftragPos_ID;             // AuftragPos_ID = PosID aus Table
    private Int32 _AuftragID;                 // Auftrag ID / Auftragsnummer
    private Int32 _AuftragPos;                // Position des Auftrags / ID Unterauftrag
    private Int32 _B_ID;
    private Int32 _E_ID;
    private DateTime _BeladeZeit = default(DateTime);
    private DateTime _EntladeZeit = default(DateTime);
    private decimal _gemGewicht;
    private decimal _tatGewicht;
    private decimal _Menge;
    private Int32 _KFZ;
    private Int32 _KFZ_ZM;
    private Int32 _KFZ_A;
    private bool _vergabe;
    private Int32 _SU_ID;
    private bool _erledigt;
    private DateTime _Date_Add = default(DateTime);
    private bool _document;
    private bool _FahrerKontakt;
    private string _KontaktInfo;
    private Int32 _Status;
    private Int32 _oldZM;
    private Int32 _Personal;
    private string _BeladestelleBez;
    private string _EntladestelleBez;
    private string _Beladestelle;
    private string _Entladestelle;

    private bool _IsPlaced;

    public bool IsPlaced
    {
      get { return _IsPlaced; }
      set { _IsPlaced = value; }
    }

    //private EFahrzORFracht _FahrzORFrachtf;
    //private int _FahrzORFrachtID;


    public Int32 ID
    {
      get { return _ID; }
      set { _ID = value; }

    }
    public Int32 AuftragPos_ID
    {
      get
      {

        try
        {
          //DataTable ANr_Table = new DataTable();
          SqlDataAdapter ada = new SqlDataAdapter();
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          ada.SelectCommand = Command;
          Command.CommandText = "SELECT ID FROM AuftragPos WHERE Auftrag_ID='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "'";
          Globals.SQLcon.Open();
          if (Command.ExecuteScalar() is DBNull)
          {
          }
          else
          {
            _AuftragPos_ID = (int)Command.ExecuteScalar();
          }
          Command.Dispose();
          Globals.SQLcon.Close();
        }
        catch (Exception ex)
        {

          MessageBox.Show(ex.ToString());
        }
        return _AuftragPos_ID;
      }

      set { _AuftragPos_ID = value; }

    }


    public int AuftragID
    {
      get { return _AuftragID; }
      set
      {
        _AuftragID = value;
        this.B_ID = GetBIDFromAuftrag(value, Globals.enumLadestelle.Beladestelle);
        this.E_ID = GetBIDFromAuftrag(value, Globals.enumLadestelle.Entladestelle);
        // this.B_ID = GetBIDFromAuftrag(value, Globals.enumLadestelle.Beladestelle);
        // this.E_ID = GetBIDFromAuftrag(value, Globals.enumLadestelle.Entladestelle);
      }

    }
    public int AuftragPos
    {
      get { return _AuftragPos; }
      set
      {
        _AuftragPos = value;
        // this.Beladestelle = B_ID;//getLadeIDfromAuftPos(value, Globals.enumLadestelle.Beladestelle);
        // this.Entladestelle = E_ID; //getLadeIDfromAuftPos(value, Globals.enumLadestelle.Entladestelle);
      }

    }
    public int B_ID
    {
      get { return _B_ID; }
      set
      {
        _B_ID = value;

        SqlCommand SelectCommand = new SqlCommand();
        DataTable LadeTable = new DataTable();
        SqlDataAdapter LadeAda = new SqlDataAdapter();
        LadeAda.SelectCommand = SelectCommand;
        SelectCommand.Connection = Globals.SQLcon.Connection;
        SelectCommand.CommandText = "SELECT Ort FROM ADR WHERE ID = " + _B_ID;
        LadeAda.Fill(LadeTable);
        if ((LadeTable.Rows.Count > 0))
        {
          //this.BeladestelleBez = (string)LadeTable.Rows[0]["Name1"];
          this.Beladestelle = (string)LadeTable.Rows[0]["Ort"]; ;
        }
        SelectCommand.Dispose();
        Globals.SQLcon.Close();
      }
    }


    public int E_ID
    {
      get { return _E_ID; }
      set
      {
        _E_ID = value;

        SqlCommand SelectCommand = new SqlCommand();
        DataTable LadeTable = new DataTable();
        SqlDataAdapter LadeAda = new SqlDataAdapter();
        LadeAda.SelectCommand = SelectCommand;
        SelectCommand.Connection = Globals.SQLcon.Connection;
        SelectCommand.CommandText = "SELECT Ort FROM ADR WHERE ID = " + _E_ID;
        LadeAda.Fill(LadeTable);
        if ((LadeTable.Rows.Count > 0))
        {
          this.Entladestelle = (string)LadeTable.Rows[0]["Ort"]; ;
        }
        SelectCommand.Dispose();
        Globals.SQLcon.Close();
      }
    }
    public string Entladestelle
    {
      get { return _Entladestelle; }
      set { _Entladestelle = value; }
    }
    public string Beladestelle
    {
      get { return _Beladestelle; }
      set { _Beladestelle = value; }
    }
    public string BeladestelleBez
    {
      get
      {
        return _BeladestelleBez;
      }
      set
      {
        _BeladestelleBez = value;
      }
    }

    public string EntladestelleBez
    {
      get
      {
        return _EntladestelleBez;
      }
      set
      {
        _EntladestelleBez = value;
      }
    }
    public DateTime BeladeZeit
    {
      get { return _BeladeZeit; }
      set { _BeladeZeit = value; }
    }
    public DateTime EntladeZeit
    {
      get { return _EntladeZeit; }
      set { _EntladeZeit = value; }
    }
    public Int32 KFZ
    {
      get { return _KFZ; }
      set { _KFZ = value; }
    }
    public Int32 oldZM
    {
      get { return _oldZM; }
      set { _oldZM = value; }
    }
    public Int32 KFZ_ZM
    {
      get { return _KFZ_ZM; }
      set { _KFZ_ZM = value; }
    }
    public Int32 KFZ_A
    {
      get { return _KFZ_A; }
      set { _KFZ_A = value; }
    }
    public bool vergabe
    {
      get { return _vergabe; }
      set { _vergabe = value; }
    }
    public Int32 SU_ID
    {
      get { return _SU_ID; }
      set { _SU_ID = value; }
    }
    public bool erledigt
    {
      get { return _erledigt; }
      set { _erledigt = value; }
    }
    public decimal Menge
    {
      get { return _Menge; }
      set { _Menge = value; }
    }
    public DateTime Date_Add
    {
      get { return _Date_Add; }
      set { _Date_Add = value; }

    }
    public bool document
    {
      get { return _document; }
      set { _document = value; }
    }
    public bool FahrerKontakt
    {
      get { return _FahrerKontakt; }
      set { _FahrerKontakt = value; }
    }
    public string KontaktInfo
    {
      get { return _KontaktInfo; }
      set { _KontaktInfo = value; }
    }
    public Int32 Status
    {
      get { return _Status; }
      set { _Status = value; }
    }
    public Int32 Personal
    {
      get { return _Personal; }
      set { _Personal = value; }
    }
    public decimal gemGewicht
    {
      get { return _gemGewicht; }
      set { _gemGewicht = value; }
    }
    public decimal tatGewicht
    {
      get { return _tatGewicht; }
      set { _tatGewicht = value; }
    }


    /*****************************************************************************************
     * 
     * ****************************************************************************************/
    //
    //
    //
    public void UpdateKFZ(bool ZM)
    {
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      if (ZM)
      {
        Command.CommandText = "UPDATE Kommission " +
                                "SET KFZ_ZM = " + KFZ_ZM +
                                " WHERE ID = " + ID;
      }
      else
      {
        Command.CommandText = "UPDATE Kommission " +
                                "SET KFZ_A = " + KFZ_A +
                                " WHERE ID = " + ID;
      }

      Globals.SQLcon.Open();
      Command.ExecuteNonQuery();
      Command.Dispose();
      Globals.SQLcon.Close();

      if (ZM)
      {
        //Add Logbucheintrag Eintrag
        string Beschreibung = "Disposition - Auftrag ID/Pos:" + AuftragID + "/" + AuftragPos + " mit Fahrzeug: " + KFZ_ZM + " disponiert";
        Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Dispo.ToString(), Beschreibung);  
      }
    }
    //
    //----------- Update Fahrer ------------------------------
    //
    public void UpdateFahrer()
    {
      if (ID > 0)
      {
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        Command.CommandText = "UPDATE Kommission " +
                                  "SET Personal ='" + Personal +
                                  "' WHERE ID = " + ID;

        Globals.SQLcon.Open();
        Command.ExecuteNonQuery();
        Command.Dispose();
        Globals.SQLcon.Close();
      }
    }
    public void UpdateBeladeZeit(DateTime newBeladeZeit, int KomID)
    {
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      //Command.CommandText = "UPDATE Disposition SET Zeit = \'"
      //            + (BeladeZeit + ("\' WHERE ID = (SELECT BeladeID from Kommission where ID = "
      //            + (ID + ")")));
      Command.CommandText = "UPDATE Kommission " +
                                  "SET B_Zeit = '" + newBeladeZeit + "' " +
                                  "WHERE ID = " + KomID;
      Globals.SQLcon.Open();
      Command.ExecuteNonQuery();
      Command.Dispose();
      Globals.SQLcon.Close();
    }
    //
    //
    //
    public void UpdateEntladeZeit(DateTime newEntladeZeit, int KomID)
    {
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      Command.CommandText = "UPDATE Kommission " +
                                  "SET E_Zeit = '" + newEntladeZeit + "' " +
                                  "WHERE ID = " + KomID;
      Globals.SQLcon.Open();
      Command.ExecuteNonQuery();
      Command.Dispose();
      Globals.SQLcon.Close();
    }
    //
    //
    //
    public void SQLInsertData()
    {
      char verg;
      try
      {
        string FahrzORFracht = "";
        SqlCommand InsertCommand = new SqlCommand();
        InsertCommand.Connection = Globals.SQLcon.Connection;

        Date_Add = DateTime.Now;
        //ID = Functions.GetNewTableID(Globals.enumDatabaseTable.Kommission, Globals.SQLcon);
        if (vergabe)
        {
          verg = 'T';
        }
        else
        {
          verg = 'F';
        }
        InsertCommand.CommandText = "INSERT INTO Kommission " +
                                                 "(PosID, Auftrag, AuftragPos, B_ID, E_ID, B_Zeit, E_Zeit, Menge, KFZ_ZM, KFZ_A, vergabe, SU_ID, erledigt, Date_Add, Papiere, FahrerKontakt, KontaktInfo) " +
                                             "VALUES (" + AuftragPos_ID + ", " +      // FK = ID in AuftragPos
                                                            AuftragID + ", " +
                                                            AuftragPos + ", " +
                                                            B_ID + ", " +
                                                            E_ID + ", " +
                                                            "'" + BeladeZeit + "', " +
                                                            "'" + EntladeZeit + "', " +
                                                            "'" + Menge.ToString().Replace(",", ".") + "', " +
                                                            KFZ_ZM + "," +
                                                            KFZ_A + "," +
                                                            "'" + verg + "', " +
                                                            SU_ID + ", " +
                                                            "'F'," +
                                                            "'" + Date_Add + "'," +
                                                            "'F' , " +                //Papiere
                                                            "'F' , " +                // Fahrerkontakt
                                                            "'" + KontaktInfo + "')";


        Globals.SQLcon.Open();
        InsertCommand.ExecuteNonQuery();

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
        string Beschreibung = "Disposition - Auftrag ID/Pos:" + AuftragID + "/" + AuftragPos + " mit Fahrzeug: "+KFZ_ZM+ " - Datum: "+BeladeZeit+ " disponiert";
        Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Dispo.ToString(), Beschreibung);
      }
    }
    //
    //
    //
    public void InsertFahrerInfo()
    {
      char Papiere = 'F';
      char Kontakt = 'F';
      if (document)
      {
        Papiere = 'T';
      }
      if (FahrerKontakt)
      {
        Kontakt = 'T';
      }
      try
      {

        SqlCommand InsertCommand = new SqlCommand();
        InsertCommand.Connection = Globals.SQLcon.Connection;
        InsertCommand.CommandText = "Update Kommission SET Papiere='" + Papiere + "', " +
                                                           "FahrerKontakt='" + Kontakt + "', " +
                                                           "KontaktInfo='" + KontaktInfo + "' " +
                                                            "WHERE ID='" + ID + "'";

        Globals.SQLcon.Open();
        InsertCommand.ExecuteNonQuery();

        Globals.SQLcon.Close();
      }
      catch (Exception ex)
      {
        System.Windows.Forms.MessageBox.Show(ex.ToString());
      }
    }
    //
    //----------------- Update Papiere in Kommission ----------------
    //
    public static void UpdateDokumenteKommissionByKommiID(bool erstellt, Int32 ID)
    {
      char Papiere = 'F';
      if (ID > 0)
      {
        if (erstellt)
        {
          Papiere = 'T';
        }

        try
        {
          SqlCommand InsertCommand = new SqlCommand();
          InsertCommand.Connection = Globals.SQLcon.Connection;
          InsertCommand.CommandText = "Update Kommission SET Papiere='" + Papiere + "' " +
                                                              "WHERE ID='" + ID + "'";

          Globals.SQLcon.Open();
          InsertCommand.ExecuteNonQuery();

          Globals.SQLcon.Close();
        }
        catch (Exception ex)
        {
          System.Windows.Forms.MessageBox.Show(ex.ToString());
        }
      }
    }
    //
    public static void UpdateDokumenteKommissionByAuftragIDAndAuftragPos(bool erstellt, Int32 AuftragID, Int32 AuftragPos)
    {
      char Papiere = 'F';
      if (erstellt)
      {
        Papiere = 'T';
      }
      try
      {
        SqlCommand InsertCommand = new SqlCommand();
        InsertCommand.Connection = Globals.SQLcon.Connection;
        InsertCommand.CommandText = "Update Kommission SET Papiere='" + Papiere + "' " +
                                                            "WHERE Auftrag='" + AuftragID + "' AND " +
                                                            "AuftragPos='" + AuftragPos + "'";

        Globals.SQLcon.Open();
        InsertCommand.ExecuteNonQuery();

        Globals.SQLcon.Close();
      }
      catch (Exception ex)
      {
        System.Windows.Forms.MessageBox.Show(ex.ToString());
      }
    }
    //
    //
    public void InsertOldZM_ID()
    {
      try
      {
        SqlCommand InsertCommand = new SqlCommand();
        InsertCommand.Connection = Globals.SQLcon.Connection;
        InsertCommand.CommandText = "Update Kommission SET oldZM='" + KFZ_ZM + "' " +
                                                         "WHERE Auftrag='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "'";


        Globals.SQLcon.Open();
        InsertCommand.ExecuteNonQuery();

        Globals.SQLcon.Close();
      }
      catch (Exception ex)
      {
        System.Windows.Forms.MessageBox.Show(ex.ToString());
      }
    }
    //
    //
    //
    public Int32 GetOldZM_ID()
    {
      Int32 oldZM = 0;
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      Command.CommandText = "Select oldZM FROM Kommission " +
                                                       "WHERE Auftrag='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "'";
      Globals.SQLcon.Open();
      if (Command.ExecuteScalar() is DBNull)
      {
        oldZM = 0;
      }
      else
      {
        oldZM = (Int32)Command.ExecuteScalar();
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
      {
        Command.Connection.Close();
      }
      return oldZM;
    }
    //
    //------------ max. Entladezeit ----------------------
    //
    public static DateTime GetMaxEntladeZeit(Int32 ZM_ID)
    {
      DateTime maxEZ = DateTime.Now;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT MAX(E_Zeit) FROM Kommission " +
                                     "WHERE KFZ_ZM='" + ZM_ID + "'";

      Globals.SQLcon.Open();
      object obj = Command.ExecuteScalar();
      if(!object.ReferenceEquals(obj , DBNull.Value))
      //if (obj != null)
      {
          string strEZ = ((DateTime)obj).ToString();

          if (DateTime.TryParse(strEZ, out maxEZ))
          {
              maxEZ = (DateTime)obj;
          }
          else
          {
              maxEZ = DateTime.Today;
          }
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      return maxEZ;
    }

    //
    //
    //
    public void getData()
    {
      try
      {
        DataTable KommiTable = new DataTable();
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;
        Command.CommandText = "SELECT " +
                                        "KO.ID, " +
                                        "KO.PosID, " +
                                        "KO.Auftrag, " +
                                        "KO.AuftragPos, " +
                                        "KO.B_ID, " +
                                        "KO.E_ID, " +
                                        "KO.B_Zeit, " +
                                        "KO.E_Zeit, " +
                                        "KO.Menge, " +
                                        "KO.KFZ_ZM, " +
                                        "KO.KFZ_A, " +
                                        "KO.vergabe, " +
                                        "KO.SU_ID, " +
                                        "KO.erledigt, " +
          //"KO.Papiere, "+
                                        "(SELECT AuftragPos.Papiere FROM AuftragPos WHERE AuftragPos.Auftrag_ID=KO.Auftrag AND AuftragPos.AuftragPos=KO.AuftragPos) as 'Papiere', " +
                                        "KO.FahrerKontakt, " +
                                        "KO.KontaktInfo, " +
                                        "KO.oldZM " +
                                    "FROM " +
                                        "Kommission KO " +
          //"INNER JOIN AuftragPos AP on KO.PosID = AP.Pos_ID " +
                                    "WHERE KO.ID = " + ID;

        ada.Fill(KommiTable);
        if ((KommiTable.Rows.Count > 0))
        {
          ID = (Int32)KommiTable.Rows[0]["ID"];
          AuftragPos_ID = (Int32)KommiTable.Rows[0]["PosID"];
          AuftragID = (Int32)KommiTable.Rows[0]["Auftrag"];
          AuftragPos = (Int32)KommiTable.Rows[0]["AuftragPos"];
          B_ID = (Int32)KommiTable.Rows[0]["B_ID"];
          E_ID = (Int32)KommiTable.Rows[0]["E_ID"];
          BeladeZeit = (DateTime)KommiTable.Rows[0]["B_Zeit"];
          EntladeZeit = (DateTime)KommiTable.Rows[0]["E_Zeit"];
          Menge = (decimal)KommiTable.Rows[0]["Menge"];
          KFZ_ZM = (Int32)KommiTable.Rows[0]["KFZ_ZM"];
          KFZ_A = (Int32)KommiTable.Rows[0]["KFZ_A"];
          if ((string)KommiTable.Rows[0]["erledigt"] == "T")
          {
            erledigt = true;
          }
          else
          {
            erledigt = false;
          }
          SU_ID = (Int32)KommiTable.Rows[0]["SU_ID"];

          document = (bool)KommiTable.Rows[0]["Papiere"];

          if ((string)KommiTable.Rows[0]["FahrerKontakt"] == "T")
          {
            FahrerKontakt = true;
          }
          else
          {
            FahrerKontakt = false;
          }
          KontaktInfo = (string)KommiTable.Rows[0]["KontaktInfo"].ToString();
          oldZM = (Int32)KommiTable.Rows[0]["oldZM"];
        }
      }
      catch (Exception ex)
      {
        System.Windows.Forms.MessageBox.Show(ex.ToString());
      }
    }
    //
    //
    //
    public DataSet getAuftragRecByAuftragID(int AuftragID, int AuftragPos)
    {
      DataSet ds = new DataSet();
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT " +
                                      "ID, " +
                                      "AuftragPos, " +
                                      "Auftrag_ID, " +
                                      "(Select SUM(gemGewicht) FROM Artikel WHERE AuftragID='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "') as 'Menge', " +
                                      "(Select SUM(tatGewicht) FROM Artikel WHERE AuftragID='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "') as 'Menge1', " +
                                      "(SELECT Auftrag.B_ID FROM Auftrag WHERE Auftrag.ANr='" + AuftragID + "') as 'B_ID' , " +
                                      "(SELECT Auftrag.E_ID FROM Auftrag WHERE Auftrag.ANr='" + AuftragID + "') as 'E_ID' " +
                                  "FROM " +
                                      "AuftragPos " +
                                  "WHERE AuftragPos.Auftrag_ID ='" + AuftragID + "' AND AuftragPos.AuftragPos='" + AuftragPos + "'";

      ada.Fill(ds);
      Command.Dispose();
      Globals.SQLcon.Close();
      return ds;
    }
    //
    //-------- läd die B_ID von Auftrag  ------------------------------------------
    //
    private int GetBIDFromAuftrag(int Auftrag, Globals.enumLadestelle BeOREnt)
    {
      SqlCommand SelectCommand = new SqlCommand();
      int LadeID;
      SelectCommand.Connection = Globals.SQLcon.Connection;
      if (BeOREnt == Globals.enumLadestelle.Beladestelle)
      {
        //SelectCommand.CommandText = ("SELECT BeladeID FROM AuftragPos WHERE ID = " + AuftragPosID);
        SelectCommand.CommandText = ("SELECT B_ID FROM Auftrag " +
                                        "JOIN AuftragPos ON Auftrag.ANr=AuftragPos.Auftrag_ID " +
                                        "WHERE Auftrag.ANr = " + Auftrag);
      }
      else if (BeOREnt == Globals.enumLadestelle.Entladestelle)
      {
        SelectCommand.CommandText = ("SELECT E_ID FROM Auftrag " +
                                        "JOIN AuftragPos ON Auftrag.ANr=AuftragPos.Auftrag_ID " +
                                        "WHERE Auftrag.ANr = " + Auftrag);
      }
      else
      {
        return 0;
      }
      Globals.SQLcon.Open();

      LadeID = (int)SelectCommand.ExecuteScalar();
      Globals.SQLcon.Close();
      return LadeID;
    }
    //
    //------- löschen Kommissionseinträge per ID -------------------
    //
    public void DeleteKommiPos()
    {
      //Add Logbucheintrag Löschen
      getData();
      string Beschreibung = "Disposition - Auftrag/Pos: "+AuftragID+"/"+ AuftragPos +"von KFZ: "+ KFZ_ZM+" - Datum: "+ BeladeZeit+ " zurück zur Auftragsliste";
      Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Dispo.ToString(), Beschreibung);

      try
      {
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        Command.CommandText = ("DELETE FROM Kommission WHERE ID = " + ID);
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
        //Add Logbucheintrag Exception
        string BeschreibungEx = "Exception: " + ex;
        Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Exception.ToString(), BeschreibungEx);
      }
    }
    //
    //------- löschen Kommissionseinträge per Auftrag und AuftragPos-------------------
    //
    public static void DeleteKommiPosByAuftragAuftragPos(Int32 AuftragID, Int32 AuftragPos)
    {
      try
      {
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        Command.CommandText = ("DELETE FROM Kommission WHERE Auftrag='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "'");
        Globals.SQLcon.Open();
        Command.ExecuteNonQuery();

        Command.Dispose();
        Globals.SQLcon.Close();
        if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
        {
          Command.Connection.Close();
        }
      }
      catch
      {
      }
    }
    //
    //----------------- 
    //
    public Int32 GetIDfromKommission()
    {
      Int32 KommiID = 0;

      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      Command.CommandText = ("Select ID FROM Kommission WHERE PosID ='" + AuftragPos_ID + "' AND " +
                                                                    "Auftrag='" + AuftragID + "' AND " +
                                                                    "AuftragPos='" + AuftragPos + "'");
      Globals.SQLcon.Open();
      object obj = Command.ExecuteScalar(); 
      if (obj == null)
      {
        KommiID = 0;
      }
      else
      {
        KommiID = (Int32)obj;
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
      {
        Command.Connection.Close();
      }
      return KommiID;
    }
    //
    //--------- Dokumente geschrieben ------------------
    //
    public bool Dokuments(int PosID)
    {
      bool doc = false;
      char papier;
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      Command.CommandText = ("Select Papiere FROM Kommission WHERE PosID ='" + PosID + "'");
      Globals.SQLcon.Open();
      if (Command.ExecuteScalar() is DBNull)
      {
        doc = false;
      }
      else
      {
        papier = Convert.ToChar(Command.ExecuteScalar());
        if (papier.ToString() == "T")
        {
          doc = true;
        }
        else
        {
          doc = false;
        }
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
      {
        Command.Connection.Close();
      }
      return doc;
    }
    //
    //--------- KONTAKT MIT fARHER ------------------
    //
    public bool Kontakt(int PosID)
    {
      bool bolKontakt = false;
      char kontakt;
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      Command.CommandText = ("Select FahrerKontakt FROM Kommission WHERE PosID ='" + PosID + "'");
      Globals.SQLcon.Open();
      if (Command.ExecuteScalar() is DBNull)
      {
        bolKontakt = false;
      }
      else
      {
        kontakt = Convert.ToChar(Command.ExecuteScalar());
        if (kontakt.ToString() == "T")
        {
          bolKontakt = true;
        }
        else
        {
          bolKontakt = false;
        }
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
      {
        Command.Connection.Close();
      }
      return bolKontakt;
    }
    //
    //---------   ------------------
    //---- 0=neu / 1= schon disponiert ------------
    public static bool GetDispoSet(Int32 AuftragID, Int32 AuftragPos)
    {
      bool dispoSet = false;
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      Command.CommandText = "Select DispoSet FROM Kommission WHERE Auftrag='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "'";
      Globals.SQLcon.Open();

      if (Command.ExecuteScalar() == null)
      {
        dispoSet = false;
      }
      else
      {
        if ((Int32)Command.ExecuteScalar() == 0)
        {
          dispoSet = false;
        }
        else
        {
          dispoSet = true;
        }
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      return dispoSet;
    }
    //
    //
    //
    private void KommissionSetErledigt()
    {
      try
      {
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        Command.CommandText = ("Update Kommission SET erledigt='T' WHERE ID = " + ID);
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
    }
    //
    //
    //
    private void AuftragPosSetStatus(Int32 iStatus, Int32 AuftragID, Int32 AuftragPos)
    {
      try
      {
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        Command.CommandText = ("Update AuftragPos SET Status='" + iStatus + "' WHERE Auftrag_ID='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "'");
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
    }
    //
    //
    //
    public static DataSet getKommiRecByAuftragID(Int32 AuftragID, Int32 AuftragPos)
    {
      DataSet ds = new DataSet();
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT * " +
                                  "FROM " +
                                      "Kommission " +
                                  "WHERE Auftrag ='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "'";

      ada.Fill(ds);
      Command.Dispose();
      Globals.SQLcon.Close();
      return ds;
    }
    //
    //
    //--- 0 = neu / 1 = schon disponiert -----------------
    public static void KommissionSetDispoSet(Int32 DispoSet, Int32 AuftragID, Int32 AuftragPos)
    {
      try
      {
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        Command.CommandText = ("Update Kommission SET DispoSet='" + DispoSet + "' WHERE Auftrag='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "'");
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
    }
    //
    //----- Anzahl der Kommission zu einem Zeipunkt auf dem Fahrzeug  --------------
    //-- mit die größe der Fahrzeugrows berechnet werden (vergrößert werden kann)
    public Int32 GetCountKommiOnSameTime()
    {
      Int32 count = 1;

      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      Command.CommandText = "SELECT COUNT(KO.ID) FROM Kommission KO " +
                                                "WHERE " +
                                                "( " +
                                                "((KO.B_Zeit < '" + BeladeZeit + "' AND KO.E_Zeit > '" + BeladeZeit + "')) " +
                                                "OR " +
                                                "((KO.B_Zeit = '" + BeladeZeit + "' AND KO.E_Zeit < '" + EntladeZeit + "')) " +
                                                "OR " +
                                                "((KO.B_Zeit < '" + EntladeZeit + "' AND KO.E_Zeit > '" + EntladeZeit + "')) " +
                                                "OR " +
                                                "((KO.B_Zeit > '" + BeladeZeit + "' AND KO.E_Zeit = '" + EntladeZeit + "')) " +
                                                "OR " +
                                                "((KO.B_Zeit < '" + BeladeZeit + "' AND KO.E_Zeit > '" + EntladeZeit + "')) " +
                                                "OR " +
                                                "((KO.B_Zeit = '" + BeladeZeit + "' AND KO.E_Zeit = '" + EntladeZeit + "')) " +
                                                "OR " +
                                                "((KO.B_Zeit > '" + BeladeZeit + "' AND KO.E_Zeit < '" + EntladeZeit + "')) " +
                                                ") " +
                                                "AND KO.KFZ_ZM='" + KFZ_ZM + "'";
      Globals.SQLcon.Open();
      if (Command.ExecuteScalar() is DBNull)
      {
        count = 1;
      }
      else
      {
        count = (Int32)Command.ExecuteScalar();
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
      {
        Command.Connection.Close();
      }
      return count;
    }
    //
    //------------ berechnen des gesamten Gewichts aller Kommi einer Tour / Zeitraum -----
    //
    public decimal GetGesamtGewichtOnSameTime(bool bo_tatGewicht)
    {
      decimal GesamtGewicht = 0.00m;
      DataTable dt = new DataTable();
      string strSQL = string.Empty;

      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;

      if (bo_tatGewicht)
      {
        strSQL = "SELECT KO.Auftrag, KO.AuftragPos, " +
                                    "(SELECT SUM(Artikel.tatGewicht) FROM Artikel WHERE Artikel.AuftragID=KO.Auftrag AND Artikel.AuftragPos=KO.AuftragPos) as 'Gewicht' " +
                                    "FROM Kommission KO " +
                                    "WHERE " +
                                                "( " +
                                                "((KO.B_Zeit < '" + BeladeZeit + "' AND KO.E_Zeit > '" + BeladeZeit + "')) " +
                                                "OR " +
                                                "((KO.B_Zeit = '" + BeladeZeit + "' AND KO.E_Zeit < '" + EntladeZeit + "')) " +
                                                "OR " +
                                                "((KO.B_Zeit < '" + EntladeZeit + "' AND KO.E_Zeit > '" + EntladeZeit + "')) " +
                                                "OR " +
                                                "((KO.B_Zeit > '" + BeladeZeit + "' AND KO.E_Zeit = '" + EntladeZeit + "')) " +
                                                "OR " +
                                                "((KO.B_Zeit < '" + BeladeZeit + "' AND KO.E_Zeit > '" + EntladeZeit + "')) " +
                                                "OR " +
                                                "((KO.B_Zeit = '" + BeladeZeit + "' AND KO.E_Zeit = '" + EntladeZeit + "')) " +
                                                "OR " +
                                                "((KO.B_Zeit > '" + BeladeZeit + "' AND KO.E_Zeit < '" + EntladeZeit + "')) " +
                                                ") " +
                                                "AND KO.KFZ_ZM='" + KFZ_ZM + "'";
      }
      else
      {
        strSQL = "SELECT KO.Auftrag, KO.AuftragPos, " +
                                   "(SELECT SUM(Artikel.gemGewicht) FROM Artikel WHERE Artikel.AuftragID=KO.Auftrag AND Artikel.AuftragPos=KO.AuftragPos) as 'Gewicht' " +
                                   "FROM Kommission KO " +
                                   "WHERE " +
                                               "( " +
                                               "((KO.B_Zeit < '" + BeladeZeit + "' AND KO.E_Zeit > '" + BeladeZeit + "')) " +
                                               "OR " +
                                               "((KO.B_Zeit = '" + BeladeZeit + "' AND KO.E_Zeit < '" + EntladeZeit + "')) " +
                                               "OR " +
                                               "((KO.B_Zeit < '" + EntladeZeit + "' AND KO.E_Zeit > '" + EntladeZeit + "')) " +
                                               "OR " +
                                               "((KO.B_Zeit > '" + BeladeZeit + "' AND KO.E_Zeit = '" + EntladeZeit + "')) " +
                                               "OR " +
                                               "((KO.B_Zeit < '" + BeladeZeit + "' AND KO.E_Zeit > '" + EntladeZeit + "')) " +
                                               "OR " +
                                               "((KO.B_Zeit = '" + BeladeZeit + "' AND KO.E_Zeit = '" + EntladeZeit + "')) " +
                                               "OR " +
                                               "((KO.B_Zeit > '" + BeladeZeit + "' AND KO.E_Zeit < '" + EntladeZeit + "')) " +
                                               ") " +
                                               "AND KO.KFZ_ZM='" + KFZ_ZM + "'";
      }

      Command.CommandText = strSQL;
      Globals.SQLcon.Open();

      ada.Fill(dt);
      Command.Dispose();
      if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
      {
        Command.Connection.Close();
      }

      //berechnung
      for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
      {
        GesamtGewicht = GesamtGewicht + Convert.ToDecimal(dt.Rows[i]["Gewicht"]);
      }
      return GesamtGewicht;
    }
    //
    //
    //
    public DataTable GetKommission()
    {
      DataTable KommiTable = new DataTable();
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT " +
                                  "KO.ID " +
                             "FROM " +
                                  "Kommission KO " +
                             "WHERE " +
        /***
             "(KO.B_Zeit < '" + DateFrom + "' " + "AND " + "KO.E_Zeit <= '" + DateTo + "') " +
                 "OR " +
             "(KO.B_Zeit >= '" + DateFrom + "' " + "AND " + "KO.E_Zeit <= '" + DateTo + "') " +
                 "OR " +
             "(KO.B_Zeit < '" + DateFrom.AddDays(1) + "' " + "AND " + "KO.E_Zeit >= '" + DateTo + "')" +
                 "OR " +
             "(KO.B_Zeit <= '" + DateFrom + "' " + "AND " + "KO.E_Zeit > '" + DateTo.AddDays(-1) + "')" +
                 "OR " +
             "(KO.B_Zeit < '" + DateFrom + "' " + "AND " + "KO.E_Zeit > '" + DateTo + "')";
       ***/
                                  "( " +
                                  "((KO.B_Zeit < '" + BeladeZeit + "' AND KO.E_Zeit > '" + BeladeZeit + "')) " +
                                  "OR " +
                                  "((KO.B_Zeit = '" + BeladeZeit + "' AND KO.E_Zeit < '" + EntladeZeit + "')) " +
                                  "OR " +
                                  "((KO.B_Zeit < '" + EntladeZeit + "' AND KO.E_Zeit > '" + EntladeZeit + "')) " +
                                  "OR " +
                                  "((KO.B_Zeit > '" + BeladeZeit + "' AND KO.E_Zeit = '" + EntladeZeit + "')) " +
                                  "OR " +
                                  "((KO.B_Zeit < '" + BeladeZeit + "' AND KO.E_Zeit > '" + EntladeZeit + "')) " +
                                  "OR " +
                                  "((KO.B_Zeit = '" + BeladeZeit + "' AND KO.E_Zeit = '" + EntladeZeit + "')) " +
                                  "OR " +
                                  "((KO.B_Zeit > '" + BeladeZeit + "' AND KO.E_Zeit < '" + EntladeZeit + "')) " +
                                  ") ";
      ada.Fill(KommiTable);
      Command.Dispose();
      if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
      {
        Command.Connection.Close();
      }
      return KommiTable;
    }
    //
    //---------- Check ob Auftrag und Position bereits disponiert ----------
    //
    public static bool IsAuftragDisponiert(Int32 auftrag, Int32 auftragPos)
    {
      bool Disponiert = true;
      try
      {
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;
        Command.CommandText = "SELECT ID FROM Kommission WHERE Auftrag='" + auftrag + "' AND AuftragPos ='" + auftragPos + "'";
        Globals.SQLcon.Open();
        if (Command.ExecuteScalar() == null)
        {
          Disponiert = false;
        }
        else
        {
          Disponiert = true;
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
      return Disponiert;
    }
    //
    //---------------- Get Kommissionsdaten für Lieferscheine------
    //
    public static DataTable GetKommiDatenForLieferschein(Int32 AuftragID, Int32 AuftragPos)
    {
      DataTable dt = new DataTable();
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT ID, Auftrag_ID, AuftragPos, " +
                                   "(SELECT KFZ FROM Fahrzeuge WHERE ID=(Select KFZ_ZM FROM Kommission WHERE PosID=AuftragPos.ID)) as 'ZM', " +
                                   "(SELECT KFZ FROM Fahrzeuge WHERE ID=(Select KFZ_A FROM Kommission WHERE PosID=AuftragPos.ID)) as 'Auflieger', " +
                                   "(SELECT Personal.Name FROM Personal WHERE ID=(Select Personal FROM Kommission WHERE PosID=AuftragPos.ID)) as 'Nachname', " +
                                   "(SELECT Personal.Vorname FROM Personal WHERE ID=(Select Personal FROM Kommission WHERE PosID=AuftragPos.ID)) as 'Vorname' " +
                                   "FROM " +
                                   "AuftragPos " +
                                   "WHERE Auftrag_ID='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "'";
      ada.Fill(dt);
      Command.Dispose();
      Globals.SQLcon.Close();
      return dt;
    }
    //
    //---------- Check ob AP_ID in DB Kommission enthalten - Transport im Selbsteintritt ----------
    //
    public static bool IsAuftragPositionIn(Int32 AP_ID)
    {
      bool IsIn = true;
      try
      {
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;
        Command.CommandText = "SELECT ID FROM Kommission WHERE PosID='" + AP_ID + "'";
        Globals.SQLcon.Open();

        object obj = Command.ExecuteScalar();
        if (obj == null)
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
  }
}
