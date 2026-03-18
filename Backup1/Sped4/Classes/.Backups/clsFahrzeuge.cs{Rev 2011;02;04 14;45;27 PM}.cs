using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Windows.Forms;
using Sped4;

namespace Sped4
{
  public class clsFahrzeuge
  {
    //************  User  ***************
    private Int32 _BenutzerID;
    public Int32 BenutzerID
    {
      get { return _BenutzerID; }
      set { _BenutzerID = value; }
    }
    //************************************

    public ArrayList AuflRecourceList = new ArrayList();
    public ArrayList FahrRecourceList = new ArrayList();
    public ArrayList VehiRecourceList = new ArrayList();

    private int _ID;
    private string _KFZ;
    private string _Fabrikat;
    private string _Bezeichnung;
    // private string m_strDesignation;   m_ Memeber of class str -> Type string i d f
    private string _FGNr;               // Fahrgestellnummer
    private DateTime _Tuev;
    private DateTime _SP;
    private DateTime _BJ;
    private DateTime _seit;
    private DateTime _Abmeldung;
    private int _Laufleistung;
    private char _ZM;
    private char _Anhaenger;
    private char _Plane;
    private char _Sattel;               // Sattelauflieger
    private char _Coil;                 // Fahrzeug mit Coilmulde??
    private int _Leergewicht;
    private int _zlGG;                  // zulässige Gesamtgewicht
    private decimal _Innenhoehe;         // bei Planfahrzeugen
    private int _Stellplaetze;           // Europalettenstellplätze
    private string _Besonderheit;
    private decimal _Laenge;
    private Int32 _KIntern;          //Firmeninternes Kennzeichen
    private string _AbgasNorm;
    private Int32 _Achsen;
    private string _Besitzer;

    public int ID
    {
      get { return _ID; }
      set
      {
        _ID = value;
        CheckZM(value);
      }
    }
    public string KFZ
    {
      get { return _KFZ; }
      set { _KFZ = value; }
    }
    public string Fabrikat
    {
      get { return _Fabrikat; }
      set { _Fabrikat = value; }
    }
    public string Bezeichnung
    {
      get { return _Bezeichnung; }
      set { _Bezeichnung = value; }
    }
    public Int32 KIntern
    {
      get { return _KIntern; }
      set { _KIntern = value; }
    }
    public string FGNr
    {
      get { return _FGNr; }
      set { _FGNr = value; }
    }
    public DateTime Tuev
    {
      get { return _Tuev; }
      set { _Tuev = value; }
    }
    public DateTime SP
    {
      get { return _SP; }
      set { _SP = value; }
    }
    public DateTime BJ
    {
      get { return _BJ; }
      set { _BJ = value; }
    }
    public DateTime seit
    {
      get { return _seit; }
      set { _seit = value; }
    }
    public DateTime Abmeldung
    {
      get { return _Abmeldung; }
      set { _Abmeldung = value; }
    }
    public int Laufleistung
    {
      get { return _Laufleistung; }
      set { _Laufleistung = value; }
    }
    public char ZM
    {
      get { return _ZM; }
      set { _ZM = value; }
    }
    public char Anhaenger
    {
      get { return _Anhaenger; }
      set { _Anhaenger = value; }
    }
    public char Plane
    {
      get { return _Plane; }
      set { _Plane = value; }
    }
    public char Sattel
    {
      get { return _Sattel; }
      set { _Sattel = value; }
    }
    public char Coil
    {
      get { return _Coil; }
      set { _Coil = value; }
    }
    public int Leergewicht
    {
      get { return _Leergewicht; }
      set { _Leergewicht = value; }
    }
    public int zlGG
    {
      get { return _zlGG; }
      set { _zlGG = value; }
    }
    public decimal Innenhoehe
    {
      get { return _Innenhoehe; }
      set { _Innenhoehe = value; }
    }
    public int Stellplaetze
    {
      get { return _Stellplaetze; }
      set { _Stellplaetze = value; }
    }
    public string Besonderheit
    {
      get { return _Besonderheit; }
      set { _Besonderheit = value; }
    }
    public decimal Laenge
    {
      get { return _Laenge; }
      set { _Laenge = value; }
    }
    public string AbgasNorm
    {
      get { return _AbgasNorm; }
      set { _AbgasNorm = value; }
    }
    public Int32 Achsen
    {
      get { return _Achsen; }
      set { _Achsen = value; }
    }
    public string Besitzer
    {
      get { return _Besitzer; }
      set { _Besitzer = value; }
    }



    //************************************************************************************
    //**********              Methoden
    //***********************************************************************************
    public void AddItem()
    {
      try
      {
        //--- initialisierung des sqlcommand---
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;

        //----- SQL Abfrage -----------------------
        Command.CommandText = ("INSERT INTO Fahrzeuge " +
                                            "( KFZ, " +
                                            "KIntern, " +
                                            "Fabrikat, " +
                                            "Bezeichnung, " +
                                            "FGNr, " +
                                            "Tuev, " +
                                            "SP, " +
                                            "BJ, " +
                                            "seit, " +
                                            "Laufleistung, " +
                                            "ZM, " +
                                            "Anhaenger, " +
                                            "Plane, " +
                                            "Sattel, " +
                                            "Coil, " +
                                            "Leergewicht, " +
                                            "zlGG, " +
                                            "Innenhoehe, " +
                                            "Stellplaetze, " +
                                            "Besonderheit, " +
                                            "bis, " +
                                            "Laenge," +
                                            "Abgas, " +
                                            "Achsen, "+
                                            "Besitzer)" +

                                    "VALUES " +
                                             "('" + KFZ + "','"
                                             + KIntern + "','"
                                             + Fabrikat + "','"
                                             + Bezeichnung + "','"
                                             + FGNr + "','"
                                             + Tuev + "','"
                                             + SP + "','"
                                             + BJ + "','"
                                             + seit + "','"
                                             + Laufleistung + "','"
                                             + ZM + "','"
                                            + Anhaenger + "','"
                                            + Plane + "','"
                                            + Sattel + "','"
                                            + Coil + "','"
                                            + Leergewicht + "','"
                                            + zlGG + "','"
                                            + Innenhoehe.ToString().Replace(",", ".") + "','"
                                            + Stellplaetze + "','"
                                            + Besonderheit + "','"
                                            + Abmeldung + "', '"
                                            + Laenge.ToString().Replace(",", ".") + "', '"
                                            + AbgasNorm + "', '"
                                            + Achsen + "', '"
                                            + Besitzer + "')");

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
        //MessageBox.Show("Der Fahrzeugdatensatz wurde erfolgreich in die Datenbank geschrieben!");
      }
      //Add Logbucheintrag Eintrag
      string Beschreibung = "Fahrzeug: " + KFZ + " hinzugefügt";
      Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Eintrag.ToString(), Beschreibung);
    }
    //
    //----------------------- Load List   ------------------------------------
    //
    public static DataTable GetVehicleList(bool aktuelleListe)
    {
      DataTable dataTable = new DataTable();
      dataTable.Clear();
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();

      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      if (aktuelleListe)
      {
        Command.CommandText = "SELECT " +
                                        "ID, " +
                                        "KFZ, " +
                                        "Fabrikat," +
                                        "ZM, " +
                                        "Anhaenger, " +
                                        "Tuev, " +
                                        "SP, " +
                                        "Besitzer "+
                                                 "FROM Fahrzeuge WHERE bis='" + DateTime.MaxValue.Date + "' ORDER BY ZM DESC, KFZ";
      }
      else
      {
        Command.CommandText = "SELECT " +
                                        "ID, " +
                                        "KFZ, " +
                                        "Fabrikat," +
                                        "ZM, " +
                                        "Anhaenger, " +
                                        "Tuev, " +
                                        "SP, " +
                                        "bis as 'Abmeldung', " +
                                        "Besitzer "+

                                                 "FROM Fahrzeuge ORDER BY ZM DESC, KFZ";

      }

      ada.Fill(dataTable);
      ada.Dispose();
      Command.Dispose();
      Globals.SQLcon.Close();
      return dataTable;
    }
    //
    //
    //---------------------------------------- update Fahrzeuge ---------------------------------
    //
    public void updateItem(int fID)
    {
      ID = fID;
      try
      {
        //--- initialisierung des sqlcommand---
        SqlCommand UpCommand = new SqlCommand();
        UpCommand.Connection = Globals.SQLcon.Connection;

        UpCommand.CommandText = "Update Fahrzeuge SET KFZ='" + KFZ + "', " +
                                                      "KIntern='" + KIntern + "' ," +
                                                      "Fabrikat='" + Fabrikat + "', " +
                                                      "Bezeichnung='" + Bezeichnung + "', " +
                                                      "FGNr='" + FGNr + "', " +
                                                      "Tuev='" + Tuev + "', " +
                                                      "SP='" + SP + "', " +
                                                      "BJ='" + BJ + "', " +
                                                      "seit='" + seit + "', " +
                                                      "Laufleistung='" + Laufleistung + "', " +
                                                      "ZM='" + ZM + "', " +
                                                      "Anhaenger='" + Anhaenger + "', " +
                                                      "Plane='" + Plane + "', " +
                                                      "Sattel='" + Sattel + "', " +
                                                      "Coil='" + Coil + "', " +
                                                      "Leergewicht='" + Leergewicht + "', " +
                                                      "zlGG='" + zlGG + "', " +
                                                      "Innenhoehe='" + Innenhoehe.ToString().Replace(",", ".") + "', " +
                                                      "Stellplaetze='" + Stellplaetze + "', " +
                                                      "Besonderheit='" + Besonderheit + "', " +
                                                      "bis ='" + Abmeldung + "', " +
                                                      "Laenge ='" + Laenge.ToString().Replace(",", ".") + "', " +
                                                      "Abgas='" + AbgasNorm + "', " +
                                                      "Achsen ='" + Achsen + "', " +
                                                      "Besitzer ='" + Besitzer + "' " +
                                                                                "WHERE ID='" + ID + "'";

        Globals.SQLcon.Open();
        UpCommand.ExecuteNonQuery();
        UpCommand.Dispose();
        Globals.SQLcon.Close();
      }
      catch (Exception ex)
      {
        System.Windows.Forms.MessageBox.Show(ex.ToString());

      }
      finally
      {
        //MessageBox.Show("Update OK!");

      }
      //Add Logbucheintrag update
      string Beschreibung = "Fahrzeug: " + KFZ + " - ID: " + ID + " geändert";
      Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
    }
    //
    //-------- Fill Data für die Disposition ---------------
    //liest Daten aus Table Fahrzeuge für die Dispo  
    public void FillData()
    {
      DataTable FahrzTable = new DataTable();
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = ("SELECT ID, KFZ, KIntern, Fabrikat, ZM, Abgas  FROM FAHRZEUGE WHERE ID = " + _ID);
      ada.Fill(FahrzTable);
      if (FahrzTable.Rows.Count > 0)
      {
        ID = (Int32)FahrzTable.Rows[0]["ID"];
        KFZ = (string)FahrzTable.Rows[0]["KFZ"];
        KIntern = (Int32)FahrzTable.Rows[0]["KIntern"];
        Fabrikat = (string)FahrzTable.Rows[0]["Fabrikat"];
        ZM = Convert.ToChar(FahrzTable.Rows[0]["ZM"]);
        AbgasNorm = (string)FahrzTable.Rows[0]["Abgas"];
        //Achsen = (Int32)FahrzTable.Rows[0]["Achsen"];
      }
      Command.Dispose();
      Globals.SQLcon.Close();
    }
    //
    //
    //
    private void CheckZM(int ID)
    {
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = ("SELECT ZM  FROM FAHRZEUGE WHERE ID = " + ID);
      Globals.SQLcon.Open();
      this.ZM = Convert.ToChar(Command.ExecuteScalar());
      Command.Dispose();
      Globals.SQLcon.Close();
    }
    //
    // -------- Read Record from Vehicle ------------------
    //
    public static DataSet GetRecByID(int ID)
    {
      DataSet ds = new DataSet();
      ds.Clear();
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();

      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT * FROM Fahrzeuge WHERE ID='" + ID + "'";

      ada.Fill(ds);
      ada.Dispose();
      Command.Dispose();
      Globals.SQLcon.Close();

      return ds;
    }

    //
    //
    //
    public static string GetKFZByID(int ID)
    {

      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = ("SELECT KFZ  FROM FAHRZEUGE WHERE ID = " + ID);
      Globals.SQLcon.Open();
      string KFZ = (string)Command.ExecuteScalar();
      Command.Dispose();
      Globals.SQLcon.Close();
      return KFZ;
    }
    //
    //------------ Aufliegerdaten für Aufliegerliste Disposition  ------------------
    //
    public DataTable GetAufliegerListe()
    {
      DataTable dataTable = new DataTable();
      dataTable.Clear();
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();

      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT " +
                                      "ID, " +
                                      "KFZ, " +
                                      "Fabrikat " +
                                      "FROM Fahrzeuge WHERE Anhaenger='T' ORDER BY KFZ ";

      ada.Fill(dataTable);
      ada.Dispose();
      Command.Dispose();
      Globals.SQLcon.Close();
      return dataTable;

    }
    //
    //------ löschen Datensatz  -----------------------
    //
    public void DeleteFahrzeug()
    {
      //Add Logbucheintrag Löschen
      KFZ = GetKFZByID(ID);
      string Beschreibung = "Fahrzeug: " + KFZ + " - ID: " + ID + " gelöscht";
      Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Loeschung.ToString(), Beschreibung);

      //--- initialisierung des sqlcommand---
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      Command.CommandText = "DELETE FROM Fahrzeuge WHERE ID='" + ID + "'";
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
    //------ Check ob KFZ Kennzeichen schon vorhanden -------
    //
    public static bool IsKFZIn(string _Kennzeichen)
    {
      bool IsIn = false;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT ID From Fahrzeuge WHERE KFZ= '" + _Kennzeichen + "'";

      Globals.SQLcon.Open();
      if (Command.ExecuteScalar() == null)
      {
        IsIn = false;
      }
      else
      {
        IsIn = true;
        Int32 ID = (Int32)Command.ExecuteScalar();
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      return IsIn;
    }
    //
    //------------------------------------ Read Fahrzeuge ---------------
    //
    public DataSet ReadDataByID(int fID)
    {
      ID = fID;
      DataSet ds = new DataSet();
      try
      {
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();

        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;
        Command.CommandText = "SELECT * FROM Fahrzeuge WHERE ID='" + ID + "'";
        ada.Fill(ds);
        ada.Dispose();
        Command.Dispose();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }
      Globals.SQLcon.Close();
      return ds;
    }
    //
    //------------------- Read ZM for Dispoplan -------------------
    //
    public static DataTable GetFahrzeuge_ZM()
    {
      DataTable FahrzTable = new DataTable();
      try
      {
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;
        Command.CommandText = "SELECT ID FROM FAHRZEUGE WHERE ZM='T' Order By KIntern";
        ada.Fill(FahrzTable);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }
      return FahrzTable;
    }
    public static DataTable GetFahrzeuge_ZMforCombo()
    {
      DataTable FahrzTable = new DataTable();
      try
      {
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;
        Command.CommandText = "SELECT ID, KFZ FROM FAHRZEUGE WHERE ZM='T' Order By KFZ";
        ada.Fill(FahrzTable);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }
      return FahrzTable;
    }
    public static DataTable GetFahrzeuge_AufliegerforCombo()
    {
      DataTable FahrzTable = new DataTable();
      try
      {
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;
        Command.CommandText = "SELECT ID, KFZ FROM FAHRZEUGE WHERE ZM='F' Order By KFZ";
        ada.Fill(FahrzTable);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }
      return FahrzTable;
    }
    //
    //------ Max KIntern-------
    //
    public static Int32 GetMaxKennIntern()
    {
      Int32 max = 0;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT MAX(KIntern) From Fahrzeuge WHERE ZM= 'T'";

      Globals.SQLcon.Open();
      object obj = Command.ExecuteScalar();

      if (obj is DBNull)
      {
        max = 0;
      }
      else
      {
        max = (Int32)obj;
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      return max;
    }
    //
    //-------------------- CHeck of Interne Nummer bereits vergeben  -----------------
    //
    public static bool ExistInterneNr(Int32 iNummer)
    {
      bool exist = false;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT KFZ From Fahrzeuge WHERE KIntern='" + iNummer + "' AND ZM='T'";

      Globals.SQLcon.Open();
      object obj = Command.ExecuteScalar();

      if (obj != null)
      {
        exist = true;
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      return exist;
    }
  }
}
