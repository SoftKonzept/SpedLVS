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
  class clsLager
  {

    //************  User  ***************
    private Int32 _BenutzerID;
    public Int32 BenutzerID
    {
      get { return _BenutzerID; }
      set { _BenutzerID = value; }
    }
    //************************************#

    internal DataSet ds = new DataSet();

    private Int32 _ID;
    private Int32 _EAID;
    private Int32 _EANr;
    private Int32 _iTyp;  //Einlagerung = 1 - Auslagerung = 0
    private Int32 _LVS;
    private Int32 _iBit;
    private Int32 _LVSNr;
    private Int32 _Artikel;
    private DateTime _Date;
    private decimal _aME;
    private decimal _aNetto;
    private decimal _aBrutto;
    private Int32 _Auftraggeber;
    private Int32 _Versender;
    private Int32 _Emfpaenger;
    private Int32 _Entladestelle;

    private string _Werksnummer;
    private string _CPNr;
    private DateTime _vonZeitraum;
    private DateTime _bisZeitraum;

    public Int32 ID
    {
      get { return _ID; }
      set { _ID = value; }
    }
    public Int32 EANr
    {
      get { return _EANr; }
      set { _EANr = value; }
    }
    public Int32 iTyp
    {
      get { return _iTyp; }
      set { _iTyp = value; }
    }
    public Int32 LVSNr
    {
      get { return _LVSNr; }
      set { _LVSNr = value; }
    }
    public Int32 iBit
    {
      get { return _iBit; }
      set { _iBit = value; }
    }
    public Int32 Artikel
    {
      get { return _Artikel; }
      set { _Artikel = value; }
    }
    public DateTime Date
    {
      get { return _Date; }
      set { _Date = value; }
    }
    public decimal aME
    {
      get { return _aME; }
      set { _aME = value; }
    }
    public decimal aNetto
    {
      get { return _aNetto; }
      set { _aNetto = value; }
    }
    public decimal aBrutto
    {
      get { return _aBrutto; }
      set { _aBrutto = value; }
    }
    public Int32 Auftraggeber
    {
      get { return _Auftraggeber; }
      set { _Auftraggeber = value; }
    }
    public Int32 Versender
    {
      get { return _Versender; }
      set { _Versender = value; }
    }
    public Int32 Emfpaenger
    {
      get { return _Emfpaenger; }
      set { _Emfpaenger = value; }
    }
    public Int32 Entladestelle
    {
      get { return _Entladestelle; }
      set { _Entladestelle = value; }
    }
    public DateTime vonZeitraum
    {
      get { return _vonZeitraum; }
      set { _vonZeitraum = value; }
    }
    public DateTime bisZeitraum
    {
      get { return _bisZeitraum; }
      set { _bisZeitraum = value; }
    }

    //*********************+ Artikel
    private decimal _Dicke;
    private decimal _Breite;
    private decimal _Laenge;
    private decimal _Hoehe;
    private Int32 _Menge;
    private Int32 _ME;
    private decimal _tatGewicht;
    private decimal _gemGewicht;
    private string _exBezeichnung;
    private string _Werk;
    private string _Halle;
    private string _Reihe;
    private string _Platz;
    private Int32 _iSchaden;
    private string _Schadensbeschreibung;
    private string _Einheit;

    public decimal Dicke
    {
      get { return _Dicke; }
      set { _Dicke = value; }
    }
    public decimal Breite
    {
      get { return _Breite; }
      set { _Breite = value; }
    }
    public decimal Laenge
    {
      get { return _Laenge; }
      set { _Laenge = value; }
    }
    public decimal Hoehe
    {
      get { return _Hoehe; }
      set { _Hoehe = value; }
    }

    public Int32 ME
    {
      get { return _ME; }
      set { _ME = value; }
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
    public string Einheit
    {
      get { return _Einheit; }
      set { _Einheit = value; }
    }


    public string exBezeichnung
    {
      get { return _exBezeichnung; }
      set { _exBezeichnung = value; }
    }
    public string Halle
    {
      get { return _Halle; }
      set { _Halle = value; }
    }
    public string Werk
    {
      get { return _Werk; }
      set { _Werk = value; }
    }
    public string Reihe
    {
      get { return _Reihe; }
      set { _Reihe = value; }
    }
    public string Platz
    {
      get { return _Platz; }
      set { _Platz = value; }
    }
    public Int32 iSchaden
    {
      get { return _iSchaden; }
      set { _iSchaden = value; }
    }
    public string Schadensbeschreibung
    {
      get { return _Schadensbeschreibung; }
      set { _Schadensbeschreibung = value; }
    }








    //**************************************************************************
    public Int32 LVS
    {
      get 
      {
        try
        {
          SqlDataAdapter ada = new SqlDataAdapter();
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          ada.SelectCommand = Command;
          //Command.CommandText = "SELECT MAX(ID) FROM Auftragsnummer";
          Command.CommandText = "DECLARE @NewID table( NewAID int ); " +
                                  "UPDATE LVSNummer SET LVS_ID= LVS_ID + 1 " +
                                  "OUTPUT INSERTED.LVS_ID INTO @NewID; " +
                                  "SELECT * FROM @NewId;";

          Globals.SQLcon.Open();

          _LVS = (Int32)Command.ExecuteScalar();
          Command.Dispose();
          Globals.SQLcon.Close();
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.ToString());
        }

        return _LVS; 
      }
      set { _LVS = value; }
    }

    public Int32 EAID
    {
      get
      {
        try
        {
          SqlDataAdapter ada = new SqlDataAdapter();
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          ada.SelectCommand = Command;
          Command.CommandText = "DECLARE @NewID table( NewAID int ); " +
                                  "UPDATE LVSNummer SET EA_ID= EA_ID + 1 " +
                                  "OUTPUT INSERTED.EA_ID INTO @NewID; " +
                                  "SELECT * FROM @NewId;";

          Globals.SQLcon.Open();

          _EAID = (Int32)Command.ExecuteScalar();
          Command.Dispose();
          Globals.SQLcon.Close();
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.ToString());
        }
        return _EAID; 
      }
      set { _EAID = value; }
    }

      public string Werksnummer
      {
        get { return _Werksnummer; }
        set { _Werksnummer = value; }
      }
      public string CPNr
      {
        get { return _CPNr; }
        set { _CPNr = value; }
      }

    private Int32 _iSearchTxt;
    public Int32 iSearchTxt
    {
      get { return _iSearchTxt; }
      set { _iSearchTxt = value; }
    }
    private string _strSearchTxt;
    public string strSearchTxt
    {
      get { return _strSearchTxt; }
      set { _strSearchTxt = value; }
    }
    //
    //
    //
    /*********************************************************************************************
     * 
     *                                FUnktionen
     * 
     * ******************************************************************************************/
    //
    //------ Startfunktion Ein-/Auslagerung ---------------
    //
    public void EinOderAuslagerung(DataTable dtArtikel, DataTable dtAuftrag, Int32 EinAuslgerung)
    {
      if ((dtArtikel != null) & (dtAuftrag != null))
      {
        ds.Tables.Add(dtArtikel);
        ds.Tables.Add(dtAuftrag);

        // 1: Einlagerung
        // 2: Ausalgerung
        // 3: Update Einlagerung
        // 4: Update Auslagerung
        switch (EinAuslgerung)
        { 
          case 1:
          case 3:
            DoEinlagerung();
            break;

          case 2:
          case 4:
            DoAuslagerung();
            break;
/**
          case 3:
            DoUpdateEinlagerung();
            break;
                
          case 4:
            DoUpdateAuslagerung();
            break;****/
        }
        ds.Tables.Remove(dtArtikel);
        ds.Tables.Remove(dtAuftrag);
      }
    }
    //
    //----------------------- Einlagerung -----------------
    //
    private void DoEinlagerung()
    { 
      //Eintrag in Artikel DB
      clsArtikel artikel = new clsArtikel();
      ds=artikel.AddArtikelLager(ds);

      //Eintrag in E/A DB
      AddToEAEingang();
    }
    //
    //--------------- Eintrag Ein-Auslagerung--------------
    //
    private void AddToEAEingang()
    {
      DataTable dt = ds.Tables["Lagerauftrag"];

      if (ds.Tables["Lagerauftrag"].Rows.Count > 0)
      {
        for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
        {
          //-- Val der einzelen Rows ----
          //EAID=0;
          iTyp=1;    //Einlagerung = true  Auslagerung = false
          EANr=(Int32)dt.Rows[i]["EA_ID"];
          Date = (DateTime)dt.Rows[i]["Datum"];
          aME = (decimal)dt.Rows[i]["aAnzahl"];
          aNetto=(decimal)dt.Rows[i]["aNetto"];
          aBrutto = (decimal)dt.Rows[i]["aBrutto"];
          Auftraggeber = (Int32)dt.Rows[i]["Auftraggeber"];
          Versender = (Int32)dt.Rows[i]["Versender"];
          Emfpaenger=(Int32)dt.Rows[i]["Empfänger"];
          Entladestelle = (Int32)dt.Rows[i]["Entladestelle"];
          Artikel = (Int32)dt.Rows[i]["LVS_ID"]; //LVS Nr

          if (ExistsLVS_ID(Artikel))
          {
              UpdateEAByLVS_ID();
          }
          else
          {
              InsertSQLLagerEA();
          }
        }
      }
    }
   //
   //
   //
    private void InsertSQLLagerEA()
    {
        try
        {
            //--- initialisierung des sqlcommand---
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;

            //----- SQL Abfrage -----------------------
            Command.CommandText = ("INSERT INTO EA (" +
                                                        "Einlagerung, " +
                                                        "EA_ID, " +
                                                        "LVS_ID, " +
                                                        "Datum, " +
                                                        "aAnzahl, " +
                                                        "aNetto, " +
                                                        "aBrutto, " +
                                                        "Auftraggeber, " +
                                                        "Versender, " +
                                                        "Empfänger, " +
                                                        "Entladestelle)" +

                                           "VALUES ('" + iTyp + "','"
                                                       + EANr + "','"
                                                       + Artikel + "','"
                                                       + Date + "','"
                                                       + aME.ToString().Replace(',', '.') + "','"
                                                       + aNetto.ToString().Replace(',', '.') + "','"
                                                       + aBrutto.ToString().Replace(',', '.') + "','"
                                                       + Auftraggeber + "','"
                                                       + Versender + "','"
                                                       + Emfpaenger + "','"
                                                       + Entladestelle + "')");

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
            //Add Logbucheintrag Eintrag
            string Beschreibung = "Lager - Eingang: E" + EANr + " hinzugefügt";
            Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Eintrag.ToString(), Beschreibung);

        }
    }
    //
    //
    //
    private void DoUpdateEinlagerung()
    {
      //Eintrag in Artikel DB
      clsArtikel artikel = new clsArtikel();
      ds = artikel.AddArtikelLager(ds);

      //Eintrag in E/A DB
      AddToEAAusgang();
    }
    //
    //raus
    private void UpdateEinlagerung()
    {
      DataTable dt = ds.Tables["Lagerauftrag"];

      if (ds.Tables["Lagerauftrag"].Rows.Count > 0)
      {
        for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
        {
          //-- Val der einzelen Rows ----
          //EAID=0;
          iTyp = 1;    //Einlagerung = true  Auslagerung = false
          EANr = (Int32)dt.Rows[i]["EA_ID"];
          Date = (DateTime)dt.Rows[i]["Datum"];
          aME = (decimal)dt.Rows[i]["aAnzahl"];
          aNetto = (decimal)dt.Rows[i]["aNetto"];
          aBrutto = (decimal)dt.Rows[i]["aBrutto"];
          Auftraggeber = (Int32)dt.Rows[i]["Auftraggeber"];
          Versender = (Int32)dt.Rows[i]["Versender"];
          Emfpaenger=(Int32)dt.Rows[i]["Empfänger"];
          Entladestelle = (Int32)dt.Rows[i]["Entladestelle"];
          Artikel = (Int32)dt.Rows[i]["LVS_ID"]; //LVS Nr

          UpdateEAByLVS_ID();
        }
      }
    }
    //
    //------------ sQL Update Lager EA --------------------
    //
    private void UpdateEAByLVS_ID()
    {
        try
          {
            //--- initialisierung des sqlcommand---
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;

            Command.CommandText = "Update EA SET " +
                                        "Einlagerung='" + iTyp + "', " +
                                        "EA_ID='" + EANr + "', " +
                                        "LVS_ID='" + Artikel + "', " +
                                        "Datum='" + Date + "', " +
                                        "aAnzahl='" +aME.ToString().Replace(',', '.') + "', " +
                                        "aNetto='" + aNetto.ToString().Replace(",", ".") + "', " +
                                        "aBrutto='" + aBrutto.ToString().Replace(",", ".") + "'," +
                                        "Auftraggeber='" + Auftraggeber + "', " +
                                        "Versender='" + Versender + "', " +
                                        "Empfänger='" + Emfpaenger + "', " +
                                        "Entladestelle='" + Entladestelle + "' "+
                                        "WHERE LVS_ID='"+Artikel+"'";
                                       
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
            //Add Logbucheintrag Eintrag
            string Beschreibung = "Lager - Eingang: E" + EANr + " - LVS Nr."+Artikel+" geändert";
            Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
          }
    
    }
    //
    //---------------- Auslagerung -------------------------
    //
    private void DoAuslagerung()
    { 
      AddToEAAusgang();
    }
    //
    //
    //
    private void AddToEAAusgang()
    {
      DataTable dt = ds.Tables["Auftrag"];

      if (ds.Tables["Auftrag"].Rows.Count > 0)
      {
        for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
        {
          //-- Val der einzelen Rows ----
          //EAID=0;
          iTyp = 0;    //Einlagerung = true  Auslagerung = false
          EANr = (Int32)dt.Rows[i]["EA_ID"];
          Date = (DateTime)dt.Rows[i]["Datum"];
          aME = (decimal)dt.Rows[i]["aAnzahl"];
          aNetto = (decimal)dt.Rows[i]["aNetto"];
          aBrutto = (decimal)dt.Rows[i]["aBrutto"];
          Auftraggeber = (Int32)dt.Rows[i]["Auftraggeber"];
          Versender = (Int32)dt.Rows[i]["Versender"];
          Emfpaenger = (Int32)dt.Rows[i]["Empfänger"];
          Entladestelle = (Int32)dt.Rows[i]["Entladestelle"];
          Artikel = (Int32)dt.Rows[i]["LVS_ID"]; //LVS Nr

          try
          {
            //--- initialisierung des sqlcommand---
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;

            //----- SQL Abfrage -----------------------
            Command.CommandText = ("INSERT INTO EA (" +
                                                        "Einlagerung, " +
                                                        "EA_ID, " +
                                                        "LVS_ID, " +
                                                        "Datum, " +
                                                        "aAnzahl, " +
                                                        "aNetto, " +
                                                        "aBrutto, "+
                                                        "Auftraggeber, " +
                                                        "Versender, " +
                                                        "Empfänger, "+
                                                        "Entladestelle)" +

                                           "VALUES ('" + iTyp + "','"
                                                       + EANr + "','"
                                                       + Artikel + "','"
                                                       + Date + "','"
                                                       + aME.ToString().Replace(',', '.') + "','"
                                                       + aNetto.ToString().Replace(',', '.') + "','"
                                                       + aBrutto.ToString().Replace(',', '.') + "','"
                                                       + Auftraggeber + "','"
                                                       + Versender + "','"
                                                       + Emfpaenger + "','"
                                                       + Entladestelle +"')");

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
            //Add Logbucheintrag Eintrag
            string Beschreibung = "Lager - Ausgang: A" + EANr + " hinzugefügt";
            Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Eintrag.ToString(), Beschreibung);

          }
        }
      }
    }
    //
    //
    //
    private void DoUpdateAuslagerung()
    { 
    
    }
    //
    //----------- Check Einlagerungsnummer exist ------------------- 
    //
    public static bool ExistEinlagerungsID(Int32 EID)
    {
      bool IsIn = false;
      try
      {
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;
        Command.CommandText = "SELECT ID FROM EA WHERE EA_ID='"+EID+"'";
        Globals.SQLcon.Open();

        object obj = Command.ExecuteScalar();

        if ( obj != null)
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
    //----------- Read Auftrag Einlagerung by ID -------------
    //
    public DataTable GetAuftragEinlagerung(string AbfrageFilter)
    { /**********************+
       * AbfrageFilter
       * 1: EA_ID
       * 2: LVS_ID
       * 
       * ********************/
      string sql = string.Empty;
      DataTable dt = new DataTable("Auftrag");
      dt.Clear();
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      sql =           "SELECT " +
                              "ID, " +
                              "Einlagerung, "+
                              "EA_ID, "+
                              "LVS_ID, "+
                              "Datum, "+
                              "aAnzahl, "+
                              "aNetto, "+
                              "aBrutto, "+
                              "Auftraggeber, "+
                              "Versender, "+
                              "Empfänger, "+
                              "Entladestelle "+
                              "FROM EA ";

      switch(AbfrageFilter)
      {
        //nach Einlagerung
        case "EA_ID":
          sql = sql +"WHERE EA.EA_ID='"+EANr+"' and Einlagerung='1'"; 
          break;
        case "LVS_ID":
          sql = sql +"WHERE EA.LVS_ID='"+LVSNr+"' AND Einlagerung='1'";
          break;
      }

      Command.CommandText =sql; 

      ada.Fill(dt);
      Command.Dispose();
      Globals.SQLcon.Close();
      return dt;
    }
    /***********************************************************************+
     *                            SEARCH LAGER
     * **********************************************************************/
    //
    //
    //
    public DataTable GetLagerDaten(ref DataTable dt, string strSuchKriterium, string strSuchValue)
    {
      string strSQL = string.Empty;

      /**************************************
       * Suchkriterium
       * 0: aktuellerArtikelbestand
       * 1: LVS
       * 2:Eingang
       * 3:Ausgang
       * 4:Produktionsnummer
       * 5:Werksnummer
       * 6: Kunde-gelagerte Artikel
       * 7: Dicke
       * 8. Breite
       * 9. Laenge
       * 10. Hoehe
       * 11. Nettogewicht
       * 12. Bruttogewicht
       * 13. Gueterart
       * 14. Charge
       * 15. exBezeichnung
       * 16. exMaterialnummer
       * 17. Bestellnummer
       * 18. Werk
       * 19. Halle
       * 20. Reihe
       * 21. Platz
       * 22. Ausgang_nach_Kunde
       * 23. Ausgang_nach_Artikel
       * **********************************/

      decimal decTMP = decimal.Zero;
      Int32 iTMP = 0;

      switch (strSuchKriterium)
      { 
        // aktueller Artikelbestand
        case "aktuellerArtikelbestand":
          strSQL = GetLagerArtikelMain();
          strSQL= strSQL +
                          "AND Artikel.LVS_ID NOT IN " +
                          "(SELECT EA.LVS_ID FROM EA WHERE EA.Einlagerung='0')";
          break;

        //LVS
        case "LVS_ID":
          //strSQL = GetSQLLagerSearchBy_LVS();
          strSQL = GetLagerArtikelMain();
          strSQL = strSQL +
                          "AND EA.LVS_ID ='" + LVSNr + "'";
          break;

        //Eingang
        case "Eingang":
          if (EANr>0)
          {
            strSQL = GetLagerArtikelMain();
            strSQL = strSQL +
                              "AND EA.EA_ID ='" + EANr + "' " +
                              "AND Artikel.LVS_ID NOT IN " +
                              "(SELECT EA.LVS_ID FROM EA WHERE EA.Einlagerung='0')";
          }
          break;

        //Auslagerung
        case "Ausgang":
          if (EANr >0)
          {
            strSQL = GetLagerArtikelMain();
            strSQL = strSQL +
                              "AND EA.EA_ID ='" + EANr + "' AND EA.Einlagerung='0' ";
          }
          break;

        case "Produktionsnummer":
        case "Werksnummer":
        case "Gueterart":
        case "Charge":
        case "exBezeichnung":
        case "exMaterialnummer":  
        case "Bestellnummer":
        case "Werk":
        case "Halle":
        case "Reihe":
        case "Platz":
          //string
          if (strSuchKriterium != null)
          {
            string tmp = string.Empty;
            tmp = strSuchValue;
              strSQL = GetLagerArtikelMain();
              strSQL = strSQL +
                                "AND Artikel." + strSuchKriterium + " ='" + tmp + "' " +
                                "AND Artikel.LVS_ID NOT IN " +
                                "(SELECT EA.LVS_ID FROM EA WHERE EA.Einlagerung='0')";

          }
          break;
       
        case "Dicke":
        case "Breite":
        case "Laenge":
        case "Hoehe":
          //decimal
          if (strSuchKriterium != null)
          {
            if(decimal.TryParse(strSuchValue, out decTMP))
            {
              
              strSQL = GetLagerArtikelMain();
              strSQL = strSQL +
                                "AND Artikel." + strSuchKriterium + " ='" + decTMP.ToString().Replace(",", ".") + "' " +
                                "AND Artikel.LVS_ID NOT IN " +
                                "(SELECT EA.LVS_ID FROM EA WHERE EA.Einlagerung='0')";
            }
          }
          break;

        //Kunde gelagerte Artikel
        case "Kunde-gelagerte Artikel":
          strSQL = GetSQLAllArtikelByKunde();
          break;

        case "Nettogewicht":
          
          if(decimal.TryParse(strSuchValue, out decTMP))
          {
            strSQL = GetLagerArtikelMain();
            strSQL = strSQL +
                              "AND Artikel.gemGewicht ='" + decTMP.ToString().Replace(",", ".") + "' " +
                              "AND Artikel.LVS_ID NOT IN " +
                              "(SELECT EA.LVS_ID FROM EA WHERE EA.Einlagerung='0')";
          }
          break;
        case "Bruttogewicht":
          //Bruttogewicht
          if(decimal.TryParse(strSuchValue, out decTMP))
          {
            strSQL = GetLagerArtikelMain();
            strSQL = strSQL +
                              "AND Artikel.tatGewicht ='" + decTMP.ToString().Replace(",", ".") + "' " +
                              "AND Artikel.LVS_ID NOT IN " +
                              "(SELECT EA.LVS_ID FROM EA WHERE EA.Einlagerung='0')";
          }
          break;


          //Ausgänge

        case "Ausgang_nach_Kunde":
          if (Int32.TryParse(strSuchValue, out iTMP))
          {
            strSQL = GetSQLLagerArtikelMainAuslagerung();
            strSQL = strSQL +
                     "WHERE EA.Auftraggeber='" + iTMP + "' AND EA.Einlagerung='0'" +
                     "AND EA.Datum >='" + vonZeitraum + "' AND EA.Datum<=" + bisZeitraum + "'";
          }
          break;

        case "Ausgang_nach_Artikel":

            strSQL = GetSQLLagerArtikelMainAuslagerung();
            strSQL = strSQL +
                         "AND EA.Einlagerung='0'";
                         //"AND EA.Datum >='" + vonZeitraum + "' AND EA.Datum<=" + bisZeitraum + "'";
          break;
      }

      
      //DataTable dt = new DataTable("Lager");
      dt.Clear();
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = strSQL;
      if (Command.CommandText != string.Empty)
      {
        ada.Fill(dt);
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      return dt;
    }
    //
    //-------------- Main SQL Abfrage für Lagersuche ----------------
    //
    private string GetLagerArtikelMain()
    {
      string sql = string.Empty;
      sql = "SELECT " +
                  "Artikel.ID as 'ArtikelID', " +
                  "EA.ID as 'EAID', " +
                  "EA.LVS_ID as 'LVS', " +
                  "EA.EA_ID as 'Eingang', " +
                  "EA.Datum as 'Datum', " +
                  "EA.Auftraggeber as 'ADRID', "+
                  "(SELECT ADR.Name1 FROM ADR WHERE ID=EA.Auftraggeber) as 'Auftraggeber', "+
                  "Artikel.Einheit as 'Einheit', "+
                  "Artikel.ME as 'ME', "+
                  "Artikel.GArt as 'Gueterart', " +
                  "Artikel.gemGewicht as 'Nettogewicht', " +
                  "Artikel.tatGewicht as 'Bruttogewicht', " +
                  "Artikel.Dicke as 'Dicke', " +
                  "Artikel.Breite as 'Breite', " +
                  "Artikel.Hoehe as 'Hoehe', " +
                  "Artikel.Laenge as 'Laenge', " +
                  "Artikel.Produktionsnummer as 'Produktionsnummer', " +
                  "Artikel.exBezeichnung as 'exBezeichnung', " +
                  "Artikel.exMaterialnummer as 'exMaterialnummer', " +
                  "Artikel.Charge as 'Charge', " +
                  "Artikel.Werk as 'Werk', " +
                  "Artikel.Halle as 'Halle', " +
                  "Artikel.Reihe as 'Reihe', " +
                  "Artikel.Platz as 'Platz', " +
                  "Artikel.Position as 'Position', "+
                  "Artikel.Schaden as 'Schaden', " +
                  "Artikel.Schadensbeschreibung as 'Schadensbeschreibung' "+
                  "FROM " +
                  "EA " +
                  "INNER JOIN Artikel ON EA.LVS_ID=Artikel.LVS_ID ";
      return sql;
    }
    //
    private string GetSQLLagerArtikelMainAuslagerung()
    {
      string sql = "SELECT " +
              "Artikel.ID as 'ArtikelID', " +
              "EA.ID as 'EAID', " +
              "EA.LVS_ID as 'LVS', " +
              "EA.EA_ID as 'Ausgang', " +
              "EA.Datum as 'A_Datum', " +
              "(SELECT EA.EA_ID FROM EA WHERE Artikel.LVS_ID = EA.LVS_ID AND EA.Einlagerung='1') as 'Eingang', "+
              "(SELECT EA.Datum FROM EA WHERE Artikel.LVS_ID = EA.LVS_ID AND EA.Einlagerung='1') as 'E_Datum', "+
              "EA.Auftraggeber as 'ADRID', " +
              "(SELECT ADR.Name1 FROM ADR WHERE ID=EA.Auftraggeber) as 'Auftraggeber', " +
              "CONVERT(DECIMAL(18,2), Artikel.ME) as 'ME', " +
              "Artikel.Einheit as 'Einheit', " +
              "Artikel.GArt as 'Gueterart', " +
              "Artikel.Dicke as 'Dicke', " +
              "Artikel.Breite as 'Breite', " +
              "Artikel.Laenge as 'Laenge', " +
              "Artikel.Hoehe as 'Hoehe', " +
              "Artikel.gemGewicht as 'Nettogewicht', " +
              "Artikel.tatGewicht as 'Bruttogewicht', " +
              "Artikel.Werksnummer as 'Werksnummer', " +
              "Artikel.Produktionsnummer  as 'Produktionsnummer', " +
              "Artikel.exBezeichnung as 'exBezeichnung', " +
              "Artikel.Charge as 'Charge', " +
              "Artikel.Bestellnummer as 'Bestellnummer', " +
              "Artikel.exMaterialnummer as 'exMaterialnummer', " +
              "Artikel.Position as 'Position', " +
              "Artikel.Werk as 'Werk', " +
              "Artikel.Halle as 'Halle', " +
              "Artikel.Reihe as 'Reihe', " +
              "Artikel.Platz as 'Platz', " +
              "Artikel.Schaden as 'Schaden', " +
              "Artikel.Schadensbeschreibung as 'Schadensbeschreibung' " +
             // "(CONVERT(DECIMAL(18,2), Artikel.ME))-(SELECT SUM(aAnzahl) FROM EA WHERE Einlagerung='0' AND EA.LVS_ID=Artikel.LVS_ID) as 'Rest ME', " +
             // "(Artikel.gemGewicht)-(SELECT SUM(aNetto) FROM EA WHERE Einlagerung='0' AND EA.LVS_ID=Artikel.LVS_ID) as 'Rest Netto', " +
             // "(Artikel.tatGewicht)-(SELECT SUM(aBrutto) FROM EA WHERE Einlagerung='0' AND EA.LVS_ID=Artikel.LVS_ID) as 'Rest Brutto' " +


              "FROM [Sped4].[dbo].[EA] " +
              "INNER JOIN Artikel ON Artikel.LVS_ID = EA.LVS_ID ";
      return sql;
    }
    //---------- SQL Abfragen ----------------
    private string GetSQLLagerSearchBy_LVS()
    {
      string sql=string.Empty;

      sql = "Select " +
                  "Artikel.LVS_ID, " +
                  "(Select CONVERT(DECIMAL(18,2), Artikel.ME) FROM Artikel WHERE Artikel.LVS_ID='" + LVSNr + "') as 'Eingang', " +
                  "(SELECT Einheit FROM Artikel WHERE Artikel.LVS_ID='" + LVSNr + "') as 'Einheit', " +
                  "(SELECT gemGewicht FROM Artikel WHERE Artikel.LVS_ID='" + LVSNr + "')  as 'Nettogewicht', " +
                  "(SELECT tatGewicht FROM Artikel WHERE Artikel.LVS_ID='" + LVSNr + "')  as 'Bruttogewicht', " +
                  "(SELECT GArt FROM Artikel WHERE Artikel.LVS_ID='" + LVSNr + "')  as 'Güterart', " +
                  "(SELECT Werksnummer FROM Artikel WHERE Artikel.LVS_ID='200')  as 'Werksnummer', " +
                  "(SELECT Produktionsnummer FROM Artikel WHERE Artikel.LVS_ID='" + LVSNr + "')  as 'Produktionsnummer', " +
                  "(SELECT EA_ID FROM EA WHERE Einlagerung='1' AND EA.LVS_ID='" + LVSNr + "' ) as 'Einlagerung', " +
                  "(SELECT Datum FROM EA WHERE Einlagerung='1' AND EA.LVS_ID='" + LVSNr + "' ) as 'Einlagerungsdatum', " +
                  "(SELECT TOP(1) EA_ID FROM EA WHERE Einlagerung='0' AND EA.LVS_ID='" + LVSNr + "' ) as 'Auslagerung', " +
                  "(SELECT TOP(1) Datum FROM EA WHERE Einlagerung='0' AND EA.LVS_ID='" + LVSNr + "') as 'A-Datum', " +
                  "(SELECT TOP(1) aAnzahl FROM EA WHERE Einlagerung='0' AND EA.LVS_ID='" + LVSNr + "') as 'A-ME', " +
                  "(SELECT TOP(1) aNetto FROM EA WHERE Einlagerung='0' AND EA.LVS_ID='" + LVSNr + "' ) as 'A-Nettogewicht', " +
                  "(SELECT TOP(1) aBrutto FROM EA WHERE Einlagerung='0' AND EA.LVS_ID='" + LVSNr + "' ) as 'A-Bruttogewicht', " +
                  "(SELECT Count(ID) FROM EA WHERE Einlagerung='0' AND EA.LVS_ID='" + LVSNr + "' ) as 'A-Anzahl', " +
                  "((Select CONVERT(DECIMAL(18,2), Artikel.ME) FROM Artikel WHERE Artikel.LVS_ID='" + LVSNr + "')-(SELECT SUM(aAnzahl) FROM EA WHERE Einlagerung='0' AND EA.LVS_ID='" + LVSNr + "')) as 'Rest ME', " +
                  "((Select gemGewicht FROM Artikel WHERE Artikel.LVS_ID='" + LVSNr + "')-(SELECT SUM(aNetto) FROM EA WHERE Einlagerung='0' AND EA.LVS_ID='" + LVSNr + "')) as 'Rest Netto', " +
                  "((Select tatGewicht FROM Artikel WHERE Artikel.LVS_ID='" + LVSNr + "')-(SELECT SUM(aBrutto) FROM EA WHERE Einlagerung='0' AND EA.LVS_ID='" + LVSNr + "')) as 'Rest Brutto' " +
                  "From " +
                  "Artikel " +
                  "WHERE " +
                  "Artikel.LVS_ID ='" + LVSNr + "'";
      return sql; 
    }
    //
    //--------------- alle gelagerten Artikel eines Kunden --------------------
    //
    private string GetSQLAllArtikelByKunde()
    {
      string sql = string.Empty;

      sql = "SELECT " +
              "Artikel.ID as 'ArtikelID', " +
              "EA.ID as 'EAID', " +
              "Artikel.LVS_ID, " +
              "EA.EA_ID as 'Eingang', " +
              "EA.Datum as 'E-Datum', " +
              "CONVERT(DECIMAL(18,2), Artikel.ME) as 'ME', " +
              "Artikel.Einheit as 'Einheit', " +
              "Artikel.GArt as 'Güterart', " +
              "Artikel.Dicke as 'Dicke', " +
              "Artikel.Breite as 'Breite', " +
              "Artikel.Laenge as 'Laenge', " +
              "Artikel.Hoehe as 'Hoehe', " +
              "Artikel.gemGewicht as 'Nettogewicht', " +
              "Artikel.tatGewicht as 'Bruttogewicht', " +
              "Artikel.Werksnummer as 'Werksnummer', " +
              "Artikel.Produktionsnummer  as 'Produktionsnummer', " +
              "Artikel.exBezeichnung as 'exBezeichnung', " +
              "Artikel.Charge as 'Charge', " +
              "Artikel.Bestellnummer as 'Bestellnummer', " +
              "Artikel.exMaterialnummer as 'exMaterialnummer', " +
              "Artikel.Position as 'Position', " +
              "Artikel.Werk as 'Werk', " +
              "Artikel.Halle as 'Halle', " +
              "Artikel.Reihe as 'Reihe', " +
              "Artikel.Platz as 'Platz', " +
              "Artikel.Schaden as 'Schaden', " +
              "Artikel.Schadensbeschreibung as 'Schadensbeschreibung', " +
              "(CONVERT(DECIMAL(18,2), Artikel.ME))-(SELECT SUM(aAnzahl) FROM EA WHERE Einlagerung='0' AND EA.LVS_ID=Artikel.LVS_ID) as 'Rest ME', " +
              "(Artikel.gemGewicht)-(SELECT SUM(aNetto) FROM EA WHERE Einlagerung='0' AND EA.LVS_ID=Artikel.LVS_ID) as 'Rest Netto', " +
              "(Artikel.tatGewicht)-(SELECT SUM(aBrutto) FROM EA WHERE Einlagerung='0' AND EA.LVS_ID=Artikel.LVS_ID) as 'Rest Brutto' " +


              "FROM [Sped4].[dbo].[EA] " +
              "INNER JOIN Artikel ON Artikel.LVS_ID = EA.LVS_ID "+
              "WHERE EA.Auftraggeber ='" + Auftraggeber + "' AND " +
              "EA.LVS_ID NOT IN " +
              "(SELECT EA.LVS_ID FROM EA WHERE Einlagerung='0')";

      return sql;
    }

    //
    //
    //
    public DataTable GetBestandKundenGesamt()
    {
      DataTable dt = new DataTable("Bestand");
      dt.Clear();
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT " +
                                  "EA.Auftraggeber as 'ADR_ID', " +
                                  "(SELECT KD_ID FROM ADR WHERE EA.Auftraggeber=ADR.ID) as 'Kundennummer', " +
                                  "(SELECT NAME1 FROM ADR WHERE EA.Auftraggeber=ADR.ID) as 'Auftraggeber' " +
                                  "FROM EA " +
                                  "INNER JOIN Artikel ON Artikel.LVS_ID = EA.LVS_ID " +
                                  "GROUP By " +
                                  "EA.Auftraggeber";

      ada.Fill(dt);
      Command.Dispose();
      Globals.SQLcon.Close();
      GetBestandsSalden(ref dt, false);
      return dt;
    }
    //
    //
    public DataTable GetAusgangBestandKundenGesamt()
    {
      DataTable dt = new DataTable("Bestand");
      dt.Clear();
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT " +
                                  "EA.Auftraggeber as 'ADR_ID', " +
                                  "(SELECT KD_ID FROM ADR WHERE EA.Auftraggeber=ADR.ID) as 'Kundennummer', " +
                                  "(SELECT NAME1 FROM ADR WHERE EA.Auftraggeber=ADR.ID) as 'Auftraggeber' " +
                                  "FROM EA " +
                                  "INNER JOIN Artikel ON Artikel.LVS_ID = EA.LVS_ID AND EA.Einlagerung='0' " +
                                  "GROUP By " +
                                  "EA.Auftraggeber";

      ada.Fill(dt);
      Command.Dispose();
      Globals.SQLcon.Close();
      GetBestandsSalden(ref dt, true);
      return dt;
    }
    //
    //------------ Bestände hinzufügen -------------------
    //
    private void GetBestandsSalden(ref DataTable dt, bool Auslagerung)
    {
      AddCol(ref dt);

      for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
      {
        /**********************+
         * E_Anzahl
         * A_Anzahl
         * E_Netto
         * A_Netto
         * E_Brutto
         * A_Brutto         
         * ********************/

        Int32 eAn = 0;
        Int32 aAn = 0;
        decimal eNet = 0.0m;
        decimal aNet = 0.0m;
        decimal eBru = 0.0m;
        decimal aBru = 0.0m;

        Int32 iAuftraggeber = 0;
        iAuftraggeber = (Int32)dt.Rows[i]["ADR_ID"];
        DataTable tmp = new DataTable();
        tmp = GetTableSaldenFromKunde(iAuftraggeber, Auslagerung);

        if ((tmp.Rows[0]["E_Anzahl"] != DBNull.Value))
        {
          eAn = (Int32)tmp.Rows[0]["E_Anzahl"];
        }
        if ((tmp.Rows[0]["A_Anzahl"] != DBNull.Value))
        {
          aAn = (Int32)tmp.Rows[0]["A_Anzahl"];
        }
        if ((tmp.Rows[0]["E_Netto"] != DBNull.Value))
        {
          eNet = (decimal)tmp.Rows[0]["E_Netto"];
        }
        if ((tmp.Rows[0]["A_Netto"] != DBNull.Value))
        {
          aNet = (decimal)tmp.Rows[0]["A_Netto"];
        }
        if ((tmp.Rows[0]["E_Brutto"] != DBNull.Value))
        {
          eBru = (decimal)tmp.Rows[0]["E_Brutto"];
        }
        if ((tmp.Rows[0]["A_Brutto"] != DBNull.Value))
        {
          aBru = (decimal)tmp.Rows[0]["A_Brutto"];
        }

        if (Auslagerung)
        {
          dt.Rows[i]["Saldo Artikel"] = Convert.ToDecimal(aAn);
          dt.Rows[i]["Saldo Netto"] = aNet;
          dt.Rows[i]["Saldo Brutto"] = aBru;
        }
        else
        {
          dt.Rows[i]["Saldo Artikel"] = Convert.ToDecimal(eAn) - Convert.ToDecimal(aAn);
          dt.Rows[i]["Saldo Netto"] = eNet - aNet;
          dt.Rows[i]["Saldo Brutto"] = eBru - aBru;
        }
        tmp.Dispose();
      }
    }
    //
    private void AddCol(ref DataTable dt)
    {
      if (dt.Columns["Saldo Artikel"] == null)
      {
        dt.Columns.Add("Saldo Artikel", typeof(decimal));
      }
      if (dt.Columns["Saldo Netto"] == null)
      {
        dt.Columns.Add("Saldo Netto", typeof(decimal));
      }
      if (dt.Columns["Saldo Brutto"] == null)
      {
        dt.Columns.Add("Saldo Brutto", typeof(decimal));
      }
    }
    //
    //---------- Salden für Kudenübersicht ----------------
    //
    private DataTable GetTableSaldenFromKunde(Int32 iAuftraggeber, bool Ausgang)
    {
      DataTable dt = new DataTable("Bestandssaldo");
      dt.Clear();
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;

      string sql = string.Empty;

      if (Ausgang)
      {
        sql = GetSQLTableSaldenAusgang(iAuftraggeber);
      }
      else
      {
        sql = GetSQLTableSaldenEingang(iAuftraggeber);
      }    
      
      Command.CommandText = sql;
      
      ada.Fill(dt);
      Command.Dispose();
      Globals.SQLcon.Close();
      return dt;
    }
    private string GetSQLTableSaldenEingang(Int32 iAuftraggeber)
    {
      string sql = "SELECT " +
                             "(Select Count(Artikel.ID) FROM Artikel INNER JOIN EA ON EA.LVS_ID=Artikel.LVS_ID WHERE EA.Auftraggeber = '" + iAuftraggeber + "' AND EA.Einlagerung='1') as 'E_Anzahl', " +
                             "(Select Count(Artikel.ID) FROM Artikel INNER JOIN EA ON EA.LVS_ID=Artikel.LVS_ID WHERE EA.Auftraggeber = '" + iAuftraggeber + "' AND EA.Einlagerung='0') as 'A_Anzahl', " +
                             "(Select SUM(Artikel.gemGewicht) FROM Artikel INNER JOIN EA ON EA.LVS_ID=Artikel.LVS_ID WHERE EA.Auftraggeber = '" + iAuftraggeber + "' AND Einlagerung='1') as 'E_Netto', " +
                             "(SELECT SUM(EA.aNetto) FROM EA WHERE EA.Auftraggeber='" + iAuftraggeber + "' AND EA.Einlagerung='0') as 'A_Netto', " +
                             "(Select SUM(Artikel.tatGewicht) FROM Artikel INNER JOIN EA ON EA.LVS_ID=Artikel.LVS_ID WHERE EA.Auftraggeber = '" + iAuftraggeber + "' AND Einlagerung='1') as 'E_Brutto'," +
                             "(SELECT SUM(EA.aBrutto) FROM EA WHERE EA.Auftraggeber='" + iAuftraggeber + "' AND EA.Einlagerung='0') as 'A_Brutto' " +
                             "FROM EA INNER JOIN Artikel ON Artikel.LVS_ID = EA.LVS_ID " +
                             "WHERE EA.Auftraggeber='" + iAuftraggeber + "' " +
                             "GROUP By EA.Auftraggeber";
      return sql;
    }
    private string GetSQLTableSaldenAusgang(Int32 iAuftraggeber)
    {
      string sql = "SELECT " +
                             "(Select Count(Artikel.ID) FROM Artikel INNER JOIN EA ON EA.LVS_ID=Artikel.LVS_ID WHERE EA.Auftraggeber = '" + iAuftraggeber + "' AND EA.Einlagerung='1') as 'E_Anzahl', " +
                             "(Select Count(Artikel.ID) FROM Artikel INNER JOIN EA ON EA.LVS_ID=Artikel.LVS_ID WHERE EA.Auftraggeber = '" + iAuftraggeber + "' AND EA.Einlagerung='0') as 'A_Anzahl', " +
                             "(Select SUM(Artikel.gemGewicht) FROM Artikel INNER JOIN EA ON EA.LVS_ID=Artikel.LVS_ID WHERE EA.Auftraggeber = '" + iAuftraggeber + "' AND Einlagerung='1') as 'E_Netto', " +
                             "(SELECT SUM(EA.aNetto) FROM EA WHERE EA.Auftraggeber='" + iAuftraggeber + "' AND EA.Einlagerung='0') as 'A_Netto', " +
                             "(Select SUM(Artikel.tatGewicht) FROM Artikel INNER JOIN EA ON EA.LVS_ID=Artikel.LVS_ID WHERE EA.Auftraggeber = '" + iAuftraggeber + "' AND Einlagerung='1') as 'E_Brutto'," +
                             "(SELECT SUM(EA.aBrutto) FROM EA WHERE EA.Auftraggeber='" + iAuftraggeber + "' AND EA.Einlagerung='0') as 'A_Brutto' " +
                             "FROM EA INNER JOIN Artikel ON Artikel.LVS_ID = EA.LVS_ID " +
                             "WHERE EA.Auftraggeber='" + iAuftraggeber + "' " +
                             "GROUP By EA.Auftraggeber";
      return sql;
    }
    //
    //----------------- Abfrage alle lagernden Artikel eines Kunden -----------
    //
    public DataTable GetBestandArtikelKunde()
    { 
      DataTable dt = new DataTable("Beastand");
      dt.Clear();
      
      if ((Auftraggeber != null) | (Auftraggeber > 0))
      {
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;
        Command.CommandText = "SELECT " +
                                      "EA.Auftraggeber as 'ADR_ID', " +
                                      "(SELECT KD_ID FROM ADR WHERE EA.Auftraggeber=ADR.ID) as 'KD Nr.', " +
                                      "(SELECT NAME1 FROM ADR WHERE EA.Auftraggeber=ADR.ID) as 'Kunde', " +
                                      "COUNT(Artikel.ID) as 'Artikelanzahl', " +
                                      "(SUM(Artikel.gemGewicht) -SUM(EA.aNetto) ) as 'Saldo Nettogewicht', " +
                                      "(SUM(Artikel.tatGewicht) - SUM(EA.aBrutto)) as 'Saldo Bruttogewicht' " +
                                      "FROM [Sped4].[dbo].[Artikel] " +
                                      "INNER JOIN EA ON Artikel.LVS_ID=EA.LVS_ID " +
                                      "GROUP BY EA.Auftraggeber ";

        ada.Fill(dt);
        Command.Dispose();
        Globals.SQLcon.Close();
      }
      return dt;
    }
    //
    //
    //
    public void DeleteArtikelLager(DataTable dt)
    {
      for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
      {
        LVSNr= (Int32)dt.Rows[i]["LVS"];
        Int32 ArtikelID = 0;
        Int32 EAID = 0;
        ArtikelID=(Int32)dt.Rows[i]["ArtikelID"];
        EAID = (Int32)dt.Rows[i]["EAID"];

        if (ArtikelID > 0)
        {
          clsArtikel art = new clsArtikel();
          art.DeleteArtikelByID(ArtikelID);
        }
        if (EAID > 0)
        {
          DeleteLagerEingangByID(EAID);
        }
        string Beschreibung = "Lagereingang : LVS Nr. " + LVSNr + " gelöscht";
        Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Loeschung.ToString(), Beschreibung);
      }
      
    }
    //
    //
    //
    public void DeleteLagerEingangByID(Int32 _dbID)
    {
      //--- initialisierung des sqlcommand---
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;

      //----- SQL Abfrage -----------------------
      Command.CommandText = "DELETE FROM EA WHERE ID='" + _dbID + "'";
      Globals.SQLcon.Open();
      Command.ExecuteNonQuery();
      Command.Dispose();
      Globals.SQLcon.Close();
    }
    //
    //
    //
    public Int32 GetADR_IDbyLVS_ID(Int32 lvsID, string ADRBesitzer)
    {
      Int32 ADR_ID = 0;
      if (clsLager.ExistsLVS_ID(lvsID))
      {
        string sql = string.Empty;
        sql = GetLagerADRSQL(lvsID, ADRBesitzer);

        try
        {
          SqlDataAdapter ada = new SqlDataAdapter();
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          ada.SelectCommand = Command;
          Command.CommandText = sql;
          Globals.SQLcon.Open();

          object obj = Command.ExecuteScalar();

          if (obj != null)
          {
            ADR_ID = (Int32)obj;
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

      }
      return ADR_ID;
    }
    //
    //
    //
    private string GetLagerADRSQL(Int32 lvsID, string ADRBesitzer)
    {
      string sql = string.Empty;
      switch (ADRBesitzer)
      {
        case "Auftraggeber":
          sql = "SELECT EA.Auftraggeber FROM EA ";
          break;
        case "Versender":
          sql = "SELECT EA.Versender FROM EA ";
          break;
        case "Empfänger":
          sql = "SELECT EA.Empfänger FROM EA ";
          break;
        case "Entladestelle":
          sql = "SELECT EA.Entladestelle FROM EA ";
          break;
      }

      sql = sql + "WHERE EA.LVS_ID='" + lvsID + "' AND Einlagerung='1'";
      return sql;
    }
    //
    //
    //
    public void UpdateADRinEA(Int32 eaID, bool Einlagerung, Int32 newADR_ID, string ADRBesitzer)
    {
      //Ein- oder Auslagerung
      Int32 iBit = 0;
      if(Einlagerung)
      {
        iBit=1;
      }
      
      try
      {
        //--- initialisierung des sqlcommand---
        SqlCommand UpCommand = new SqlCommand();
        UpCommand.Connection = Globals.SQLcon.Connection;
        string sql = string.Empty;
        sql = GetLagerUpdateADRSQL(eaID, ADRBesitzer, newADR_ID, iBit);
        UpCommand.CommandText = sql;

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
        string action = string.Empty;
        if (iBit == 0)
        {
          action = "A"+eaID;
        }
        else
        {
          action = "E" + eaID;
        }
        string Beschreibung = "Adresse: " + action + " "+ADRBesitzer+ " geändert";
        Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
      }
    }
    //
    //--------------- UPDATE ADR DATEN in DB --------------------
    //
    private string GetLagerUpdateADRSQL(Int32 eaID, string ADRBesitzer, Int32 nADR_ID, Int32 iBit)
    {
      string sql = string.Empty;
      switch (ADRBesitzer)
      {
        case "Auftraggeber":
          sql = "Update EA SET EA.Auftraggeber='" + nADR_ID + "' ";                                                                     
          break;
        case "Versender":
          sql = "Update EA SET EA.Versender='" + nADR_ID + "' "; 
          break;
        case "Empfänger":
          sql = "Update EA SET EA.Empfänger='" + nADR_ID + "' "; 
          break;
        case "Entladestelle":
          sql = "Update EA SET EA.Entladestelle='" + nADR_ID + "' "; 
          break;
      }

      sql = sql + "WHERE EA.EA_ID='" + eaID + "' AND EA.Einlagerung='"+iBit+"'";
      return sql;
    }
    //
    //
    //
    public static bool ExistsLVS_ID(Int32 lvsID)
    {
      bool IsIn = false;
      try
      {
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;
        Command.CommandText = "SELECT ID FROM EA WHERE EA.LVS_ID='" + lvsID + "'";
        Globals.SQLcon.Open();

        object obj = Command.ExecuteScalar();

        if (obj != null)
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
