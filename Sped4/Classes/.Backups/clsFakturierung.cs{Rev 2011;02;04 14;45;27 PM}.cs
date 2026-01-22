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
  class clsFakturierung
  {
     public DataTable dataTable = new DataTable();
    public DataSet ds = new DataSet();
    public string FrachtBezeichnung = string.Empty;

    private Int32 _ID;
    private Int32 _RGNr;
    private bool _RG;
    private Int32 _AuftragID;
    private Int32 _AuftragPos;
    private Int32 _KundeID;
    private string _Beladeort;
    private string _Entladeort;
    private decimal _Fracht;
    private decimal _Fracht_RG;
    private decimal _Fracht_GS;
    private decimal _Fracht_GSSU;
    private decimal _Fracht_FVGS;
    private string _KD_Name;
    private string _ADRMatchcode;
    private Int32 _SU_ID;
    private Int32 _B_ID;
    private string _B_Name;
    private string _B_PLZ;
    private string _B_Ort;
    private Int32 _E_ID;
    private string _E_Name;
    private string _E_PLZ;
    private string _E_Ort;
    private decimal _Gewicht;
    private decimal _GesamtGemGewicht;
    private decimal _GesamtTatGewicht;
    private string _SU_Name;

    private Int32 _Status;
    private Int32 _km;
    private decimal _PreisTo;
    private decimal _PreisPal;
    private decimal _PreisKm;
    private Int32 _Abrechnungsliste;
    private DateTime _vonZeitraum;
    private DateTime _bisZeitraum;
    
    //Artikel
    private Int32 _ME;
    private string _Gut;
    private decimal _Dicke;
    private decimal _Breite;
    private decimal _Hoehe;
    private decimal _Laenge;
    private decimal _ArtikelGewicht;
    private string _Abmessungen;
    private Int32 _Artikel_ID;
    private decimal _ArtikelFracht;
    private decimal _gemGewicht;
    private decimal _tatGewicht;
   

    //Berechnung SUMME
    private decimal _SummeArtikel;
    private decimal _ZusatzKosten;
    private decimal _SummeAP;
    private string _Beschreibung1 = "Summe Fracht Artikel";
    private string _Beschreibung2 = "";
    private string _Beschreibung3 = "Summe Fracht Auftragposition";
    private string _TextZusatzkosten;

    //DB Enfernungen
    private Int32 _Start_ADR_ID;
    private Int32 _Ziel_ADR_ID;
    private Int32 _EntfernungsID;


    private decimal _fpflGewicht;
    private string _FrachtText;
    private decimal _MargeProzent;
    private decimal _MargeEuro;
    private Int32 _AP_ID;         //ID AuftragPos
    private bool _Pauschal;

    //Gutschrifteingabe
    private string _GS;
    private DateTime _GS_Date;
    private bool _GSanSU;
    private decimal _MwStSatz;

    //manuelle RG / GS
    private Int32 _zwZM;
    private Int32 _zwAufliefer;
    private Int32 _zwSU;

    //Druck
    private DateTime _Druck_DateRG;
    private DateTime _Druck_DateGS;
    private DateTime _Druck_DateGSSU;

    public Int32 zwSU
    {
      get { return _zwSU; }
      set { _zwSU = value; }
    }
    public Int32 zwAufliefer
    {
      get { return _zwAufliefer; }
      set { _zwAufliefer = value; }
    }
    public Int32 zwZM
    {
      get { return _zwZM; }
      set { _zwZM = value; }
    }
    public Int32 ID
    {
      get { return _ID; }
      set { _ID = value; }
    }
    public Int32 RGNr
    {
      get { return _RGNr; }
      set { _RGNr = value; }
    }
    public Int32 AuftragID
    {
      get { return _AuftragID; }
      set { _AuftragID = value; }
    }
    public Int32 AuftragPos
    {
      get { return _AuftragPos; }
      set { _AuftragPos = value; }
    }
    public Int32 KundeID
    {
      get { return _KundeID; }
      set { _KundeID = value; }
    }
    public string Beladeort
    {
      get { return _Beladeort; }
      set { _Beladeort = value; }
    }
    public string TextZusatzkosten
    {
        get { return _TextZusatzkosten; }
        set { _TextZusatzkosten = value; }
    }
    public string SU_Name
    {
      get { return _SU_Name; }
      set { _SU_Name = value; }
    }
    public string Entladeort
    {
      get { return _Entladeort; }
      set { _Entladeort = value; }
    }
    public decimal Fracht_GS
    {
      get 
      {
        //_Fracht_GS = Fracht + ZusatzKosten;
        return _Fracht_GS; 
      }
      set { _Fracht_GS = value; }
    }
    public decimal Fracht_RG
    {
      get 
      {
        //_Fracht_RG = Fracht + ZusatzKosten;
        return _Fracht_RG; 
      }
      set { _Fracht_RG = value; }
    }
    public decimal Fracht_GSSU
    {
      get
      {
        //_Fracht_GS = Fracht + ZusatzKosten;
        return _Fracht_GSSU;
      }
      set { _Fracht_GSSU = value; }
    }
    public decimal Fracht_FVGS
    {
      get
      {
        //_Fracht_GS = Fracht + ZusatzKosten;
        return _Fracht_FVGS;
      }
      set { _Fracht_FVGS = value; }
    }
    public decimal Fracht
    {
      get { return _Fracht; }
      set { _Fracht = value; }
    }
    public decimal MwStSatz
    {
        get { return _MwStSatz; }
        set { _MwStSatz = value; }
    }
    public string KD_Name
    {
      get { return _KD_Name; }
      set { _KD_Name = value; }
    }
    public string ADRMatchcode
    {
        get { return _ADRMatchcode; }
        set { _ADRMatchcode = value; }
    }
    public Int32 SU_ID
    {
        get { return _SU_ID; }
        set { _SU_ID = value; }
    }
    public Int32 B_ID
    {
      get { return _B_ID; }
      set { _B_ID = value; }
    }
    public string B_Name
    {
      get { return _B_Name; }
      set { _B_Name = value; }
    }
    public string B_PLZ
    {
      get { return _B_PLZ; }
      set { _B_PLZ = value; }
    }
    public string B_Ort
    {
      get { return _B_Ort; }
      set { _B_Ort = value; }
    }
    public Int32 E_ID
    {
      get { return _E_ID; }
      set { _E_ID = value; }
    }
    public string E_Name
    {
      get { return _E_Name; }
      set { _E_Name = value; }
    }
    public string E_PLZ
    {
      get { return _E_PLZ; }
      set { _E_PLZ = value; }
    }
    public string E_Ort
    {
      get { return _E_Ort; }
      set { _E_Ort = value; }
    }
    public Int32 Status
    {
      get { return _Status; }
      set { _Status = value; }
    }
    public string Gut
    {
      get { return _Gut; }
      set { _Gut = value; }
    }
    public Int32 km
    {
      get { return _km; }
      set { _km = value; }
    }
    public decimal PreisTo
    {
      get { return _PreisTo; }
      set { _PreisTo = value; }
    }
    public decimal PreisPal
    {
      get { return _PreisPal; }
      set { _PreisPal = value; }
    }
    public decimal PreisKm
    {
      get { return _PreisKm; }
      set { _PreisKm = value; }
    }
    public decimal Gewicht
    {
      get
      {
        if (tatGewicht > 0)
        {
          _Gewicht = GesamtTatGewicht;
        }
        else
        {
          _Gewicht = GesamtGemGewicht;
        }
        return _Gewicht; }
      set { _Gewicht = value; }
    }
    public Int32 Abrechnungsliste
    {
      get { return _Abrechnungsliste; }
      set { _Abrechnungsliste = value; }
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
    //Artikel
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
    public decimal ArtikelGewicht
    {
      get
      {
        if (tatGewicht > 0)
        {
          _ArtikelGewicht = tatGewicht;
        }
        else
        {
          _ArtikelGewicht = gemGewicht;
        }
        return _ArtikelGewicht; 
      }
      set { _ArtikelGewicht = value; }
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
    public decimal GesamtGemGewicht
    {
      get { return _GesamtGemGewicht; }
      set { _GesamtGemGewicht = value; }
    }
    public decimal GesamtTatGewicht
    {
      get { return _GesamtTatGewicht; }
      set { _GesamtTatGewicht = value; }
    }
    public string Abmessungen
    {
      get
      {
        _Abmessungen = Dicke.ToString() + "x" + Breite.ToString() + "x" + Laenge.ToString() + "x" + Hoehe.ToString();
        return _Abmessungen; 
      }
        set { _Abmessungen = value; }
    }
    public Int32 Artikel_ID
    {
      get { return _Artikel_ID; }
      set { _Artikel_ID = value; }
    }
    public decimal ArtikelFracht
    {
      get { return _ArtikelFracht; }
      set { _ArtikelFracht = value; }
    }

    //Berechnung Summe
    public decimal SummeArtikel
    {
      get { return _SummeArtikel; }
      set { _SummeArtikel = value; }
    }
    public decimal ZusatzKosten
    {
      get { return _ZusatzKosten; }
      set { _ZusatzKosten = value; }
    }
    public decimal SummeAP
    {
      get 
      { //Berechnung Gesamtsumme Auftragposition
        return _SummeAP; 
      }
      set { _SummeAP = value; }
    }
    public string Beschreibung1
    {
      get 
      {
        _Beschreibung1 = "Summe Fracht Artikel";
        return _Beschreibung1; }
      set { _Beschreibung1 = value; }
    }
    public string Beschreibung2
    {
      get { return _Beschreibung2; }
      set { _Beschreibung2 = value; }
    }
    public string Beschreibung3
    {
      get 
      {
        _Beschreibung3="Summe Fracht Auftragposition";
        return _Beschreibung3; }
      set { _Beschreibung3 = value; }
    }

    //Entfernungen
    public Int32 AP_ID
    {
      get { return _AP_ID; }
      set { _AP_ID = value; }
    }
    public Int32 Start_ADR_ID
    {
      get { return _Start_ADR_ID; }
      set { _Start_ADR_ID = value; }
    }
    public Int32 Ziel_ADR_ID
    {
      get { return _Ziel_ADR_ID; }
      set { _Ziel_ADR_ID = value; }
    }
    public Int32 EntfernungsID
    {
      get { return _EntfernungsID; }
      set { _EntfernungsID = value; }
    }
    public decimal fpflGewicht
    {
      get { return _fpflGewicht; }
      set { _fpflGewicht = value; }
    }
    public string FrachtText
    {
      get { return _FrachtText; }
      set { _FrachtText = value; }
    }
    public decimal MargeProzent
    {
      get { return _MargeProzent; }
      set { _MargeProzent = value; }
    }
    public decimal MargeEuro
    {
      get { return _MargeEuro; }
      set { _MargeEuro = value; }
    }
    public bool Pauschal
    {
      get { return _Pauschal; }
      set { _Pauschal = value; }
    }
    public bool GSanSU
    {
        get { return _GSanSU; }
        set { _GSanSU = value; }
    }
    public string GS
    {
      get { return _GS; }
      set { _GS = value; }
    }
    public DateTime GS_Date
    {
      get { return _GS_Date; }
      set { _GS_Date = value; }
    }
     public bool RG
    {
        get { return _RG; }
        set { _RG = value; }
    }

    // Druck
     public DateTime Druck_DateRG
     {
       get 
       {
         _Druck_DateRG =clsRechnungen.GetDruckDate(ID, true, false);
         return _Druck_DateRG; 
       }
       set { _Druck_DateRG = value; }
     }
     public DateTime Druck_DateGS
     {
       get
       {
         _Druck_DateGS =clsRechnungen.GetDruckDate(ID, false, false);
         return _Druck_DateGS;
       }
       set { _Druck_DateGS = value; }
     }
     public DateTime Druck_DateGSSU
     {
       get
       {
         _Druck_DateGSSU = clsRechnungen.GetDruckDate(ID, false, true);
         return _Druck_DateGSSU;
       }
       set { _Druck_DateGSSU = value; }
     }
    //
    //
    //
    /********************************************************
     * Abrechnungsliste:
     * 0. aktuellle Liste
     * 1. durchgeführte Aufträge an Kunde
     * 2. durchgeführte Aufträge nach Zeitraum
     * 3. RG nach Zeitraum
     * 4. GS nach Zeitraum
     * //zusätzliche
     * 5. Daten nach Auftragspositions-ID 
     * 6. RG - Daten nach Kunde
     * 7. GS - Daten nach Kunde
     * ******************************************************/
    //
    //
    public DataTable GetAuftragForAbbrechnungListe()
    {
      //Abrechnungsliste = 0;
      DataTable dataTable = new DataTable("Auftragsdaten");
      try
      {
        string sql = string.Empty;
        dataTable.Clear();
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;

        sql = "SELECT " +
                        "DISTINCT AuftragPos.ID as 'ID', " +
                        "AuftragPos.Auftrag_ID as 'AuftragID', " +
                        "AuftragPos.AuftragPos as 'AuftragPos', " +
                        "AuftragPos.Status as 'Status', " +
                        "(Select ADR.ViewID FROM ADR WHERE ADR.ID=Auftrag.KD_ID) as 'Suchbegriff', " +
                        "(Select ADR.ID FROM ADR WHERE ADR.ID=Auftrag.KD_ID) as 'KD', " +
                        "(Select ADR.Name1 FROM ADR WHERE ADR.ID= Auftrag.KD_ID) as 'KD_Name', " +
                        "(Select ADR.ID FROM ADR WHERE ADR.ID=Auftrag.B_ID) as 'BSID', " +
                        "(Select ADR.Name1 FROM ADR WHERE ADR.ID= Auftrag.B_ID) as 'Beladestelle', " +
                        "(Select ADR.Str FROM ADR WHERE ADR.ID= Auftrag.B_ID) as 'B_Strasse', " +
                        "(Select ADR.PLZ FROM ADR WHERE ADR.ID= Auftrag.B_ID) as 'B_PLZ', " +
                        "(Select ADR.Ort FROM ADR WHERE ADR.ID=Auftrag.B_ID) as 'B_Ort', " +
                        "(Select ADR.ID FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'ESID', " +
                        "(Select ADR.Name1 FROM ADR WHERE ADR.ID= Auftrag.E_ID) as 'Entladestelle', " +
                        "(Select ADR.PLZ FROM ADR WHERE ADR.ID= Auftrag.E_ID) as 'E_PLZ', " +
                        "(Select ADR.Ort FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'E_Ort', " +
                        "(Select Frachtvergabe.SU FROM Frachtvergabe WHERE ID_AP=AuftragPos.ID) as 'SU_ID', " +
                        "(Select ADR.Name1 FROM ADR WHERE ADR.ID=(Select Frachtvergabe.SU FROM Frachtvergabe WHERE ID_AP=AuftragPos.ID)) as 'SU', " +
                        "(Select Top(1) Artikel.GArt FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID) as 'Gut', " +
                        "(Select SUM(Artikel.gemGewicht) FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID AND Artikel.AuftragPos=AuftragPos.AuftragPos) as 'gemGewicht', " +
                        "(Select SUM(Artikel.tatGewicht) FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID AND Artikel.AuftragPos=AuftragPos.AuftragPos) as 'tatGewicht', " +
                        "(Select SUM(Frachten.Fracht) FROM Frachten WHERE AP_ID=AuftragPos.ID AND GS='0') as 'Fracht_RG', "+  
                        "(Select SUM(Frachten.Fracht) FROM Frachten WHERE AP_ID=AuftragPos.ID AND GS='1' AND GS_SU='0' AND FVGS='0') as 'Fracht_GS', "+
                        "(Select SUM(Frachten.Fracht) FROM Frachten WHERE AP_ID=AuftragPos.ID AND GS='1' AND GS_SU='1') as 'Fracht_GSSU', "+
                        "(Select SUM(Frachten.Fracht) FROM Frachten WHERE AP_ID=AuftragPos.ID AND GS='1' AND FVGS='1') as 'Fracht_FVGS', " +
                        "(Select SUM(Frachten.ZusatzKosten) FROM Frachten WHERE AP_ID=AuftragPos.ID) as 'ZusatzKosten' " +
                        
                        "FROM AuftragPos " +
                        "INNER JOIN Auftrag ON Auftrag.ANr=AuftragPos.Auftrag_ID ";

        switch (Abrechnungsliste)
        { 
          //aktuelle
          case 0:
            sql = sql + "WHERE AuftragPos.Status>'4' AND AuftragPos.Status<'7' AND( " +
                      "AuftragPos.Auftrag_ID IN (SELECT Auftrag FROM Kommission) OR "+
                      "AuftragPos.ID IN (SELECT ID_AP FROM Frachtvergabe))";
            break;
          //aktuell - durchgeführte
          case 1:
            sql = sql + "WHERE AuftragPos.Status='5' AND( " +
                        "AuftragPos.Auftrag_ID IN (SELECT Auftrag FROM Kommission) OR " +
                        "AuftragPos.ID IN (SELECT ID_AP FROM Frachtvergabe))";
            break;
          //aktuelle - Freigabe Berechnung
          case 2:
            sql = sql + "WHERE AuftragPos.Status='6' AND( " +
                        "AuftragPos.Auftrag_ID IN (SELECT Auftrag FROM Kommission) OR " +
                        "AuftragPos.ID IN (SELECT ID_AP FROM Frachtvergabe))";
            break;
          // RG nach Zeitraum
          case 3:
            sql = sql + "WHERE AuftragPos.Status>'4' AND AuftragPos.Status<'7' " +
                        "(SELECT Kommission.E_Zeit FROM Kommission WHERE Kommission.Auftrag=AuftragPos.Auftrag_ID AND Kommission.AuftragPos=AuftragPos.AuftragPos)>'" + vonZeitraum + "' AND " +
                        "(SELECT Kommission.E_Zeit FROM Kommission WHERE Kommission.Auftrag=AuftragPos.Auftrag_ID AND Kommission.AuftragPos=AuftragPos.AuftragPos)<'" + bisZeitraum + "' " +
                        "AuftragPos.Auftrag_ID IN (SELECT Auftrag FROM Kommission) ";
            break;
          // GS nach Zeitraum
          case 4:
            sql = sql + "WHERE AuftragPos.Status>'4' AND AuftragPos.Status<'7' " +
                        "(SELECT Kommission.E_Zeit FROM Kommission WHERE Kommission.Auftrag=AuftragPos.Auftrag_ID AND Kommission.AuftragPos=AuftragPos.AuftragPos)>'" + vonZeitraum + "' AND " +
                        "(SELECT Kommission.E_Zeit FROM Kommission WHERE Kommission.Auftrag=AuftragPos.Auftrag_ID AND Kommission.AuftragPos=AuftragPos.AuftragPos)<'" + bisZeitraum + "' " +
                        "AuftragPos.Auftrag_ID IN (SELECT Auftrag FROM Kommission) ";
            break;

          case 5:
            sql = sql + "WHERE AuftragPos.Status>'4' AND AuftragPos.Status<'7' " +
                        "(SELECT Kommission.E_Zeit FROM Kommission WHERE Kommission.Auftrag=AuftragPos.Auftrag_ID AND Kommission.AuftragPos=AuftragPos.AuftragPos)>'" + vonZeitraum + "' AND " +
                        "(SELECT Kommission.E_Zeit FROM Kommission WHERE Kommission.Auftrag=AuftragPos.Auftrag_ID AND Kommission.AuftragPos=AuftragPos.AuftragPos)<'" + bisZeitraum + "' " +
                        "AuftragPos.Auftrag_ID IN (SELECT Auftrag FROM Kommission) ";
            break;



/***************
          case 5:
            sql = sql + "WHERE AuftragPos.ID='" + AP_ID + "' ";
            break;

          case 6:
            //RG
            sql = sql + "WHERE Auftrag.KD_ID='" + KundeID + "' AND AuftragPos.Status='6' ";
            break;

          case 7:
            //GS
            sql = sql + "WHERE AuftragPos.Status='6' AND "+
                        "AuftragPos.ID IN (Select ID_AP FROM Frachtvergabe WHERE SU='"+KundeID+"')";
            break;
          case 8:
            //FVGS
            sql = sql + "WHERE AuftragPos.ID='"+AP_ID+"' AND (" +
                        "AuftragPos.Auftrag_ID IN (SELECT Auftrag FROM Kommission) OR " +
                        "AuftragPos.ID IN (SELECT ID_AP FROM Frachtvergabe))";
            break;
 * ******/

        }

        Command.CommandText = sql;
        ada.Fill(dataTable);
        ada.Dispose();
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
      return dataTable;
    }
    //
    //-------- Get Auftragsdaten für neuen Ausdruck ---------------
    //
    public DataTable GetAuftragForAbbrechnungListeByRGID(Int32 RG_ID)
    {
      DataTable dataTable = new DataTable("Auftragsdaten");
      dataTable.Clear();
      if (RG_ID > 0)
      {
        try
        {
          string sql = string.Empty;
          SqlDataAdapter ada = new SqlDataAdapter();
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          ada.SelectCommand = Command;

          sql = "SELECT " +
                          "DISTINCT AuftragPos.ID as 'ID', " +
                          "AuftragPos.Auftrag_ID as 'AuftragID', " +
                          "AuftragPos.AuftragPos as 'AuftragPos', " +
                          "AuftragPos.Status as 'Status', " +
                          "(Select ADR.ViewID FROM ADR WHERE ADR.ID=Auftrag.KD_ID) as 'Suchbegriff', " +
                          "(Select ADR.ID FROM ADR WHERE ADR.ID=Auftrag.KD_ID) as 'KD', " +
                          "(Select ADR.Name1 FROM ADR WHERE ADR.ID= Auftrag.KD_ID) as 'KD_Name', " +
                          "(Select ADR.ID FROM ADR WHERE ADR.ID=Auftrag.B_ID) as 'BSID', " +
                          "(Select ADR.Name1 FROM ADR WHERE ADR.ID= Auftrag.B_ID) as 'Beladestelle', " +
                          "(Select ADR.Str FROM ADR WHERE ADR.ID= Auftrag.B_ID) as 'B_Strasse', " +
                          "(Select ADR.PLZ FROM ADR WHERE ADR.ID= Auftrag.B_ID) as 'B_PLZ', " +
                          "(Select ADR.Ort FROM ADR WHERE ADR.ID=Auftrag.B_ID) as 'B_Ort', " +
                          "(Select ADR.ID FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'ESID', " +
                          "(Select ADR.Name1 FROM ADR WHERE ADR.ID= Auftrag.E_ID) as 'Entladestelle', " +
                          "(Select ADR.PLZ FROM ADR WHERE ADR.ID= Auftrag.E_ID) as 'E_PLZ', " +
                          "(Select ADR.Ort FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'E_Ort', " +
                          "(Select Frachtvergabe.SU FROM Frachtvergabe WHERE ID_AP=AuftragPos.ID) as 'SU_ID', " +
                          "(Select ADR.Name1 FROM ADR WHERE ADR.ID=(Select Frachtvergabe.SU FROM Frachtvergabe WHERE ID_AP=AuftragPos.ID)) as 'SU', " +
                          "(Select Top(1) Artikel.GArt FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID) as 'Gut', " +
                          "(Select SUM(Artikel.gemGewicht) FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID AND Artikel.AuftragPos=AuftragPos.AuftragPos) as 'gemGewicht', " +
                          "(Select SUM(Artikel.tatGewicht) FROM Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID AND Artikel.AuftragPos=AuftragPos.AuftragPos) as 'tatGewicht', " +
                          "(Select SUM(Frachten.Fracht) FROM Frachten WHERE AP_ID=AuftragPos.ID AND GS='0') as 'Fracht_RG', " +
                          "(Select SUM(Frachten.Fracht) FROM Frachten WHERE AP_ID=AuftragPos.ID AND GS='1') as 'Fracht_GS', " +
                          "(Select SUM(Frachten.ZusatzKosten) FROM Frachten WHERE AP_ID=AuftragPos.ID) as 'ZusatzKosten' " +
                          "FROM AuftragPos " +
                          "INNER JOIN Frachten ON Frachten.AP_ID=AuftragPos.ID " +
                          "INNER JOIN Auftrag ON Auftrag.ANr=AuftragPos.Auftrag_ID " +
                          "WHERE AuftragPos.ID IN ( SELECT AP_ID FROM Frachten WHERE RG_ID='" + RG_ID + "')";


          Command.CommandText = sql;
          ada.Fill(dataTable);
          ada.Dispose();
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
      else
      {
        clsMessages.Fakturierung_LesenFrachtdatenByRGID();
      }
      return dataTable;
    }  
    //
    //-------- Insert Frachtkonditionen -------------------------------
    //
    private void SetFrachtKonditionen()
    {
      try
      {
        //--- initialisierung des sqlcommand---
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;

        //----- SQL Abfrage -----------------------
        Command.CommandText = ("INSERT INTO Frachtkonditionen " +
                                        "(KD_ID, V_ARD, E_ADR, V_PLZ, E_PLZ, km, Gewicht, Gut_ID, PreisTo, PreisPal, PreisKm) " +
                                        "VALUES ('" + KundeID + "','"
                                                    + E_ID + "','"
                                                    + B_ID + "','"
                                                    + B_PLZ + "','"
                                                    + E_PLZ + "','"
                                                    + km + "','"
                                                    + Gewicht + "','"
                                                    + Gut + "','"
                                                    + PreisTo + "','"
                                                    + PreisPal + "','"
                                                    + PreisKm + "')");

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
        //MessageBox.Show("Der Adressdatensatz wurde erfolgreich in die Datenbank geschrieben!");

      }
    }
    //
    //
    //
    public void SetColKonditionen()
    {
          //DataColumn col;

          //----  init and add the columns ----
          //Gut
          DataColumn col1 = new DataColumn();
          col1.DataType = System.Type.GetType("System.Int32");
          col1.ColumnName = "Gut";
          col1.ReadOnly = false;
          dataTable.Columns.Add(col1);

          //km
          DataColumn col2 = new DataColumn();
          col2.DataType = System.Type.GetType("System.Int32");
          col2.ColumnName = "km";
          col2.ReadOnly = false;
          dataTable.Columns.Add(col2);

          //€/km
          DataColumn col3 = new DataColumn();
          col3.DataType = System.Type.GetType("System.Decimal");
          col3.ColumnName = "PreisKm";
          col3.ReadOnly = false;
          dataTable.Columns.Add(col3);

          //Gewicht
          DataColumn col4 = new DataColumn();
          col4.DataType = System.Type.GetType("System.Decimal");
          col4.ColumnName = "Gewicht";
          col4.ReadOnly = false;
          dataTable.Columns.Add(col4);

          //€/to
          DataColumn col5 = new DataColumn();
          col5.DataType = System.Type.GetType("System.Decimal");
          col5.ColumnName = "PreisTo";
          col5.ReadOnly = false;
          dataTable.Columns.Add(col5);
          
          //Pal
          DataColumn col6 = new DataColumn();
          col6.DataType = System.Type.GetType("System.Int32");
          col6.ColumnName = "EP";
          col6.ReadOnly = false;
          dataTable.Columns.Add(col6);

          //€/Pal
          DataColumn col7 = new DataColumn();
          col7.DataType = System.Type.GetType("System.Decimal");
          col7.ColumnName = "PreisPal";
          col7.ReadOnly = false;
          dataTable.Columns.Add(col7);

          //ID
          DataColumn col8 = new DataColumn();
          col8.DataType = System.Type.GetType("System.Int32");
          col8.ColumnName = "Table_ID";
          col8.ReadOnly = false;
          dataTable.Columns.Add(col8);   

    }
    //
    //-------------- Dataset mit allen Angaben für Fakturierung --------
    //
    public DataSet GetAuftragDatenForFakturierung(bool imSelbsteintritt)
    {
      if ((AuftragID != null) & (AuftragPos != null))
      {
        DataSet ds = new DataSet();
        //AuftragDaten
        DataTable dtAuftrag = new DataTable();
        dtAuftrag.TableName = "Auftragsdaten";
        try
        {
          string sql = string.Empty;
          dataTable.Clear();
          SqlDataAdapter ada = new SqlDataAdapter();
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          ada.SelectCommand = Command;

          sql = "SELECT " +
                          "AuftragPos.ID as 'ID', " +
                          "AuftragPos.Auftrag_ID as 'AuftragID', " +
                          "AuftragPos.AuftragPos as 'AuftragPos', " +
              //Resourcen
                          "(SELECT Fahrzeuge.KFZ FROM Fahrzeuge WHERE Fahrzeuge.ID=(SELECT KFZ_ZM FROM Kommission WHERE PosID=AuftragPos.ID))as 'ZM', " +
                          "(SELECT Fahrzeuge.KFZ FROM Fahrzeuge WHERE Fahrzeuge.ID=(SELECT KFZ_A FROM Kommission WHERE PosID=AuftragPos.ID))as 'Auflieger', " +
                          "(SELECT Name FROM Personal WHERE ID=(SELECT Personal FROM Kommission WHERE PosID=AuftragPos.ID))as 'Fahrer', " +

                          "(SELECT B_Zeit FROM Kommission WHERE PosID=AuftragPos.ID) as 'B_Datum', " +
                          "(SELECT E_Zeit FROM Kommission WHERE PosID=AuftragPos.ID) as 'E_Datum', " +

                          //Auftraggeber
                          "(Select ADR.ID FROM ADR WHERE ADR.ID=(SELECT KD_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'A_ID', " +
                          "(Select ADR.Name1 FROM ADR WHERE ADR.ID=(SELECT KD_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'Auftraggeber', " +
                          "(Select ADR.Str FROM ADR WHERE ADR.ID=(SELECT KD_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'A_Strasse', " +
                          "(Select ADR.PLZ FROM ADR WHERE ADR.ID=(SELECT KD_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'A_PLZ', " +
                          "(Select ADR.Ort FROM ADR WHERE ADR.ID=(SELECT KD_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'A_Ort', " +
              //Versender
                          "(Select ADR.ID FROM ADR WHERE ADR.ID=(SELECT B_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'V_ID', " +
                          "(Select ADR.Name1 FROM ADR WHERE ADR.ID=(SELECT B_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'Versender', " +
                          "(Select ADR.Str FROM ADR WHERE ADR.ID=(SELECT B_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'V_Strasse', " +
                          "(Select ADR.PLZ FROM ADR WHERE ADR.ID=(SELECT B_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'V_PLZ', " +
                          "(Select ADR.Ort FROM ADR WHERE ADR.ID=(SELECT B_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'V_Ort', " +
              //Empfänger
                          "(Select ADR.ID FROM ADR WHERE ADR.ID=(SELECT E_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'E_ID', " +
                          "(Select ADR.Name1 FROM ADR WHERE ADR.ID=(SELECT E_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'Empfänger', " +
                          "(Select ADR.Str FROM ADR WHERE ADR.ID=(SELECT E_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'E_Strasse', " +
                          "(Select ADR.PLZ FROM ADR WHERE ADR.ID=(SELECT E_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'E_PLZ', " +
                          "(Select ADR.Ort FROM ADR WHERE ADR.ID=(SELECT E_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'E_Ort'";


          if (imSelbsteintritt)
          {
            sql = sql +     " FROM AuftragPos "+
                            "WHERE AuftragPos.Auftrag_ID='" + AuftragID + "' AND " +
                            "AuftragPos.AuftragPos='" + AuftragPos + "' AND " +
                            "AuftragPos.Status>4 AND "+
                            "AuftragPos.ID IN (SELECT PosID FROM Kommission) ";
          }
          else
          { 
            sql = sql +     ", "+
                            "(SELECT SU FROM Frachtvergabe WHERE ID_AP=AuftragPos.ID) as 'SU_ID', "+
                            "(Select ADR.Name1 FROM ADR WHERE ADR.ID=(SELECT SU FROM Frachtvergabe WHERE ID_AP=AuftragPos.ID)) as 'SU', "+
                            "(Select ADR.Str FROM ADR WHERE ADR.ID=(SELECT SU FROM Frachtvergabe WHERE ID_AP=AuftragPos.ID)) as 'SU_Strasse', "+ 
                            "(Select ADR.PLZ FROM ADR WHERE ADR.ID=(SELECT SU FROM Frachtvergabe WHERE ID_AP=AuftragPos.ID)) as 'SU_PLZ', "+
                            "(Select ADR.Ort FROM ADR WHERE ADR.ID=(SELECT SU FROM Frachtvergabe WHERE ID_AP=AuftragPos.ID)) as 'SU_Ort'  "+
                
                            "FROM AuftragPos "+
                            "WHERE AuftragPos.Auftrag_ID='" + AuftragID + "' AND " +
                            "AuftragPos.AuftragPos='" + AuftragPos + "' AND " +
                            "AuftragPos.Status>4 AND " +
                            "AuftragPos.ID IN (SELECT ID_AP FROM Frachtvergabe) ";
          }

          Globals.SQLcon.Open();
          Command.CommandText = sql;
          ada.Fill(dtAuftrag);
          ada.Dispose();
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

        //ARtikel
        DataTable dtArtikel = new DataTable();
        dtArtikel = clsArtikel.GetDataTableForArtikelGrd(AuftragID, AuftragPos);
        dtArtikel.TableName = "Artikel";

        //Übersicht weitere AuftragPos der Aufträge

        clsAuftragsstatus status = new clsAuftragsstatus();
        status.Auftrag_ID = AuftragID;
        DataTable dtStatus= new DataTable();
        dtStatus = status.GetStatusListeForAuftrag();
        dtStatus.TableName = "Statusliste";

        //Tabellen zum DataSet hinzufügen
        ds.Tables.Add(dtAuftrag);
        ds.Tables.Add(dtArtikel);
        ds.Tables.Add(dtStatus);
        return ds;
      }
      else
      {
        return null;
      }
    }

    /********************************************************************************************++
     *                                DB Entfernungen
     * ********************************************************************************************/
    //
    //------- Insert Entfernung ---------------
    //
    public void AddDistance()
    {
      try
      {
        //--- initialisierung des sqlcommand---
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;

        //----- SQL Abfrage -----------------------
        Command.CommandText = ("INSERT INTO Entfernungen (Start_ADR_ID, Ziel_ADR_ID, km) " +
                                        "VALUES ('" + Start_ADR_ID + "','"
                                                    + Ziel_ADR_ID + "','"
                                                    + km + "')");

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
    //------------- update km - Entfernung -------------------
    //
    public void UpdateDistance()
    {
      try
      {
        //--- initialisierung des sqlcommand---
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;

        //----- SQL Abfrage -----------------------
        Command.CommandText = "Update Entfernungen SET km='"+km+"' " +
                                        "WHERE ID='"+EntfernungsID+"'";
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
    //------------ Prüft,ob die km für die Entfernung bereits vorhanden ist ------------
    //
    public bool ExistDistance()
    {
      bool exist = false;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT ID From Entfernungen WHERE Start_ADR_ID= '" + Start_ADR_ID + "' AND " +
                                                               "Ziel_ADR_ID='"+Ziel_ADR_ID+"'";
      Globals.SQLcon.Open();
      object obj = Command.ExecuteScalar();

      if (obj == null)
      {
        exist = false;
      }
      else
      {
        exist = true;
        Int32 ID = (Int32)obj;
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      return exist;
    }
    //
    //------------ Entfernung zwischen zwei ADR ------------
    //
    public Int32 GetDistanceKm()
    {
      Int32 distance=0;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT km From Entfernungen WHERE Start_ADR_ID= '" + Start_ADR_ID + "' AND " +
                                                               "Ziel_ADR_ID='" + Ziel_ADR_ID + "'";
      Globals.SQLcon.Open();
      object obj = Command.ExecuteScalar();

      if (obj == null)
      {
        distance = 0;
      }
      else
      {
        distance = (Int32)obj;
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      return distance;
    }
    //
    //------------ ID Entfernung------------
    //
    public Int32 GetDistanceID()
    {
      Int32 distanceID = 0;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT ID From Entfernungen WHERE Start_ADR_ID= '" + Start_ADR_ID + "' AND " +
                                                               "Ziel_ADR_ID='" + Ziel_ADR_ID + "'";
      Globals.SQLcon.Open();
      object obj = Command.ExecuteScalar();

      if (obj == null)
      {
        distanceID = 0;
      }
      else
      {
        distanceID = (Int32)obj;
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      return distanceID;
    }
    //
    //
    //
    /*****************************************************************************************************
     * 
     *                          DB INSERT für Fracht ( RG / GS ) in Frachten
     * 
     * **************************************************************************************************/
    //
    //
    //
    public static Int32 GetRGEforFVGS(Int32 ap_id)
    { 
      Int32 ADR_ID = 0;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT Fracht_ADR FROM Frachten WHERE AP_ID='"+ap_id+"' AND GS='1' AND FVGS='1'";

      Globals.SQLcon.Open();
      object obj = Command.ExecuteScalar();

      if (obj == null)
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
    //
    //
    public void InsertToFrachten()
    {
        for (Int32 i = 0; i <= ds.Tables.Count - 1; i++)
        {
          string table = string.Empty;
          table = ds.Tables[i].ToString();
          string Tabelle=string.Empty;

          switch (table)
          { 
              case "FVGS":
                  Tabelle = "FVGS";
                  FrachtBezeichnung = "FVGS";
                  AssignValue(Tabelle);
                  break;

              case "FVGS_Daten":
                  Tabelle = "FVGS_Daten";
                  FrachtBezeichnung = "FVGS";
                  AssignValue(Tabelle);
                  break;

              case "Frachtdaten":
                  if ((bool)ds.Tables["Fracht"].Rows[0]["RG"] == true)
                  {
                    FrachtBezeichnung = "Rechnung";
                  }
                  else
                  {
                    FrachtBezeichnung = "Gutschrift";
                  }
                  Tabelle = "Frachtdaten";
                  AssignValue(Tabelle);
                  break;

              case "GS_SU":
                  Tabelle = "GS_SU";
                  FrachtBezeichnung = "GutschriftSU";
                  AssignValue(Tabelle);
                  break;

              case "RGGSDaten":
                  Tabelle = "RGGSDaten";
                  if ((bool)ds.Tables["RGGSDaten"].Rows[0]["RG"] == true)
                  {
                    FrachtBezeichnung = "Rechnung";
                  }
                  else
                  {
                    FrachtBezeichnung = "Gutschrift";
                  }
                  AssignValueRGGS(Tabelle);
                  break;
          }
        }
    }
    //
    //--------- Daten zuweisen für FVGS ------------------
    //
    private void AssignValueRGGS(string Tabelle)
    {

        for (Int32 i = 0; i <= ds.Tables[Tabelle].Rows.Count - 1; i++)
        {
            if ((Int32)ds.Tables[Tabelle].Rows[i]["AP_ID"] != null)
            {
                AP_ID = (Int32)ds.Tables[Tabelle].Rows[i]["AP_ID"];
            }
            if ((Int32)ds.Tables[Tabelle].Rows[i]["Artikel_ID"] != null)
            {
                Artikel_ID = (Int32)ds.Tables[Tabelle].Rows[i]["Artikel_ID"];
            }
            if ((decimal)ds.Tables[Tabelle].Rows[i]["Fracht"] != null)
            {
                Fracht = (decimal)ds.Tables[Tabelle].Rows[i]["Fracht"];
            }
            if ((string)ds.Tables[Tabelle].Rows[i]["Frachttext"] != null)
            {
                FrachtText = (string)ds.Tables[Tabelle].Rows[i]["Frachttext"];
            }
            if ((Int32)ds.Tables[Tabelle].Rows[i]["km"] != null)
            {
                km = (Int32)ds.Tables[Tabelle].Rows[i]["km"];
            }
            if ((decimal)ds.Tables[Tabelle].Rows[i]["fpflGewicht"] != null)
            {
                fpflGewicht = (decimal)ds.Tables[Tabelle].Rows[i]["fpflGewicht"];
            }
            if ((bool)ds.Tables[Tabelle].Rows[i]["Pauschal"] != null)
            {
                Pauschal = (bool)ds.Tables[Tabelle].Rows[i]["Pauschal"];
            }

            MargeEuro = (decimal)ds.Tables[Tabelle].Rows[i]["MargeEuro"];
            if (!Pauschal)
            {
                PreisTo = (decimal)ds.Tables[Tabelle].Rows[i]["Frachtsatz"];
            }
            else
            {
                PreisTo = 0.0m;
            }
            // Auftraggeber / Kunde 
            if (Tabelle == "FVGS_Daten")
            {
              KundeID=(Int32)ds.Tables[Tabelle].Rows[i]["Fracht_ADR"];
            }
            else
            {
              KundeID = (Int32)ds.Tables[Tabelle].Rows[i]["KD_ID"];
            }
            ZusatzKosten = (decimal)ds.Tables[Tabelle].Rows[i]["ZusatzKosten"];
            TextZusatzkosten = (string)ds.Tables[Tabelle].Rows[i]["TextZusatzKosten"];

            if (ds.Tables["FVGS"] != null)
            {
                if(ds.Tables[Tabelle].Rows[i]["FV_B_ID"]!=DBNull.Value)
                {
                  B_ID = (Int32)ds.Tables[Tabelle].Rows[i]["FV_B_ID"];
                }
                if(ds.Tables[Tabelle].Rows[i]["FV_E_ID"]!=DBNull.Value)
                {
                  E_ID = (Int32)ds.Tables[Tabelle].Rows[i]["FV_E_ID"];      
                }
            }
            if (ds.Tables["GS_SU"] != null)
            {
               GSanSU = (bool)ds.Tables[Tabelle].Rows[i]["GS_SU"];
                SU_ID = (Int32)ds.Tables[Tabelle].Rows[i]["KD_ID"];
            }
            MwStSatz = (decimal)ds.Tables[Tabelle].Rows[i]["MwStSatz"];
            if (ds.Tables[Tabelle].Rows[i]["GS_ID"] != DBNull.Value)
            {
              GS = (string)ds.Tables[Tabelle].Rows[i]["GS_ID"];
            }
            else
            {
              GS = string.Empty;
            }
            GS_Date = (DateTime)ds.Tables[Tabelle].Rows[i]["GS_Datum"];
          
            //

            // Eintrag in DB
            if(Tabelle=="Frachtdaten")
            {
              if (Artikel_ID > 0)
              {
                if (IsAPisInRGArtikel(AP_ID, Artikel_ID, true))
                {
                  UpdateFracht();
                }
                else
                {
                  InsertFracht();
                }
              }
              else
              {
              if (IsAPisInRG(AP_ID, true))
              {
                UpdateFracht();
              }
              else
              {
                InsertFracht();
              }
                }
            }
            if((Tabelle=="FVGS") | (Tabelle=="FVGS_Daten"))
            {
              if(IsAPisInFVGS(AP_ID))
              {
                UpdateFracht();
              }
              else
              {
                InsertFracht();
              }
            }
            //noch ungetestet
            if (Tabelle == "GS_SU")
            {
              if (IsAPisInRG(AP_ID, false))
              {
                UpdateFracht();
              }
              else
              {
                InsertFracht();
              }

            }
        }
    }
    //
    //--------- Daten zuweisen für FVGS ------------------
    //
    private void AssignValue(string Tabelle)
    {

      for (Int32 i = 0; i <= ds.Tables[Tabelle].Rows.Count - 1; i++)
      {
        if ((Int32)ds.Tables[Tabelle].Rows[i]["AP_ID"] != null)
        {
          AP_ID = (Int32)ds.Tables[Tabelle].Rows[i]["AP_ID"];
        }
        if ((Int32)ds.Tables[Tabelle].Rows[i]["Artikel_ID"] != null)
        {
          Artikel_ID = (Int32)ds.Tables[Tabelle].Rows[i]["Artikel_ID"];
        }
        if ((decimal)ds.Tables[Tabelle].Rows[i]["Fracht"] != null)
        {
          Fracht = (decimal)ds.Tables[Tabelle].Rows[i]["Fracht"];
        }
        if ((string)ds.Tables[Tabelle].Rows[i]["Frachttext"] != null)
        {
          FrachtText = (string)ds.Tables[Tabelle].Rows[i]["Frachttext"];
        }
        if ((Int32)ds.Tables[Tabelle].Rows[i]["km"] != null)
        {
          km = (Int32)ds.Tables[Tabelle].Rows[i]["km"];
        }
        if ((decimal)ds.Tables[Tabelle].Rows[i]["fpflGewicht"] != null)
        {
          fpflGewicht = (decimal)ds.Tables[Tabelle].Rows[i]["fpflGewicht"];
        }
        if ((bool)ds.Tables[Tabelle].Rows[i]["Pauschal"] != null)
        {
          Pauschal = (bool)ds.Tables[Tabelle].Rows[i]["Pauschal"];
        }

        MargeEuro = (decimal)ds.Tables[Tabelle].Rows[i]["MargeEuro"];
        if (!Pauschal)
        {
          PreisTo = (decimal)ds.Tables[Tabelle].Rows[i]["Frachtsatz"];
        }
        else
        {
          PreisTo = 0.0m;
        }
        // Auftraggeber / Kunde 
        if (Tabelle == "FVGS_Daten")
        {
          KundeID = (Int32)ds.Tables[Tabelle].Rows[i]["Fracht_ADR"];
        }
        else
        {
          KundeID = (Int32)ds.Tables[Tabelle].Rows[i]["KD_ID"];
        }
        ZusatzKosten = (decimal)ds.Tables[Tabelle].Rows[i]["ZusatzKosten"];
        TextZusatzkosten = (string)ds.Tables[Tabelle].Rows[i]["TextZusatzKosten"];

        if (Tabelle=="FVGS")
        {
          if (ds.Tables[Tabelle].Rows[i]["FV_B_ID"] != DBNull.Value)
          {
            B_ID = (Int32)ds.Tables[Tabelle].Rows[i]["FV_B_ID"];
          }
          if (ds.Tables[Tabelle].Rows[i]["FV_E_ID"] != DBNull.Value)
          {
            E_ID = (Int32)ds.Tables[Tabelle].Rows[i]["FV_E_ID"];
          }
        }
        if (Tabelle == "FVGSDaten")
        {
          if (ds.Tables[Tabelle].Rows[i]["FV_V_ID"] != DBNull.Value)
          {
            B_ID = (Int32)ds.Tables[Tabelle].Rows[i]["FV_V_ID"];
          }
          if (ds.Tables[Tabelle].Rows[i]["FV_E_ID"] != DBNull.Value)
          {
            E_ID = (Int32)ds.Tables[Tabelle].Rows[i]["FV_E_ID"];
          }
        }
        if (ds.Tables["GS_SU"] != null)
        {
          GSanSU = (bool)ds.Tables[Tabelle].Rows[i]["GSanSU"];
          SU_ID = (Int32)ds.Tables[Tabelle].Rows[i]["KD_ID"];
        }
        MwStSatz = (decimal)ds.Tables[Tabelle].Rows[i]["MwStSatz"];
        if (ds.Tables[Tabelle].Rows[i]["GS_ID"] != DBNull.Value)
        {
          GS = (string)ds.Tables[Tabelle].Rows[i]["GS_ID"];
        }
        else
        {
          GS = string.Empty;
        }
        GS_Date = (DateTime)ds.Tables[Tabelle].Rows[i]["GS_Datum"];

        //

        // Eintrag in DB
        if (Tabelle == "Frachtdaten")
        {
          if (Artikel_ID > 0)
          {
            if (IsAPisInRGArtikel(AP_ID, Artikel_ID, true))
            {
              UpdateFracht();
            }
            else
            {
              InsertFracht();
            }
          }
          else
          {
            if (IsAPisInRG(AP_ID, true))
            {
              UpdateFracht();
            }
            else
            {
              InsertFracht();
            }
          }
        }
        if ((Tabelle == "FVGS") | (Tabelle == "FVGS_Daten"))
        {
          if (IsAPisInFVGS(AP_ID))
          {
            UpdateFracht();
          }
          else
          {
            InsertFracht();
          }
        }
        //noch ungetestet
        if (Tabelle == "GS_SU")
        {
          //if (IsAPisInRG(AP_ID, false))
          if (IsAPisInRGSU(AP_ID))
          {
            UpdateFracht();
          }
          else
          {
            InsertFracht();
          }

        }
      }
    }
    /******************************* SQL DB Frachten  ******************************************/ 
    //
    //-----------  Insert --------
    //
    private void InsertFracht()
    {
        string sql = string.Empty;
        try
        {
            //--- initialisierung des sqlcommand---
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;

            //----- SQL Abfrage -----------------------
            switch (FrachtBezeichnung)
            {
                case "Rechnung":
                sql = "INSERT INTO Frachten (AP_ID, Artikel_ID, Fracht, Fracht_ADR, Frachttext, km, fpflGewicht, Pauschal, GS, MargeEuro, MwStSatz, ZusatzKosten, TextZusatzkosten, Frachtsatz) " +
                                            "VALUES ('" + AP_ID + "','"
                                                        + Artikel_ID + "', '"
                                                        + Fracht.ToString().Replace(',', '.') + "', '"
                                                        + KundeID + "', '"
                                                        + FrachtText + "', '"
                                                        + km + "','"
                                                        + fpflGewicht.ToString().Replace(',','.') + "', '"
                                                        + Pauschal + "', "  
                                                        + "'0', '"
                                                        + MargeEuro.ToString().Replace(',', '.') +"', '"
                                                        + MwStSatz.ToString().Replace(',', '.') + "', '"
                                                        + ZusatzKosten.ToString().Replace(',', '.') + "', '"
                                                        + TextZusatzkosten + "', '"
                                                        + PreisTo.ToString().Replace(',', '.') + "')";
                    break;
                case "GutschriftSU":
                    sql = "INSERT INTO Frachten (AP_ID, Artikel_ID, Fracht, Fracht_ADR, Frachttext, km, fpflGewicht, Pauschal, GS, GS_SU, MargeEuro, MwStSatz, ZusatzKosten, TextZusatzkosten, Frachtsatz) " +
                                    "VALUES ('" + AP_ID + "','"
                                                + Artikel_ID + "','"
                                                + Fracht.ToString().Replace(',', '.') + "', '"
                                                + SU_ID + "','"
                                                + FrachtText + "','"
                                                + km + "','"
                                                + fpflGewicht.ToString().Replace(',','.') + "', '"
                                                + Pauschal + "', "
                                                + "'1', "
                                                + "'1', '"
                                                + MargeEuro.ToString().Replace(',', '.') + "', '"
                                                + MwStSatz.ToString().Replace(',', '.') + "', '"
                                                + ZusatzKosten.ToString().Replace(',', '.') + "', '"
                                                + TextZusatzkosten + "', '"
                                                + PreisTo.ToString().Replace(',', '.')+ "')";
                                                
                    break;
                case "Gutschrift":
                    sql = "INSERT INTO Frachten (AP_ID, Artikel_ID, Fracht, Fracht_ADR, Frachttext, km, fpflGewicht, Pauschal, GS, MargeEuro, MwStSatz, ZusatzKosten, TextZusatzkosten, GS_ID, GS_Datum, Frachtsatz) " +
                                    "VALUES ('" + AP_ID + "','"
                                                + Artikel_ID + "','"
                                                + Fracht.ToString().Replace(',', '.') + "', '"
                                                + KundeID + "','"
                                                + FrachtText + "','"
                                                + km + "','"
                                                + fpflGewicht.ToString().Replace(',','.') + "', '"
                                                + Pauschal + "', "
                                                + "'1', '"
                                                + MargeEuro.ToString().Replace(',', '.') + "', '"
                                                + MwStSatz.ToString().Replace(',', '.') + "', '"
                                                + ZusatzKosten.ToString().Replace(',', '.') + "', '"
                                                + TextZusatzkosten + "', '"
                                                + GS + "', '"
                                                + GS_Date + "', '"
                                                + PreisTo.ToString().Replace(',', '.')+"')";
                    break;
                case "FVGS":
                    sql = "INSERT INTO Frachten (AP_ID, Artikel_ID, Fracht, Fracht_ADR, Frachttext, km, fpflGewicht, Pauschal, GS, FVGS, FV_V_ID, FV_E_ID, MargeEuro, MwStSatz, ZusatzKosten, TextZusatzkosten, GS_ID, GS_Datum, Frachtsatz) " +
                                    "VALUES ('" + AP_ID + "','"
                                                + Artikel_ID + "','"
                                                + Fracht.ToString().Replace(',', '.') + "', '"
                                                + KundeID + "','"
                                                + FrachtText + "','"
                                                + km + "','"
                                                + fpflGewicht.ToString().Replace(',','.') + "', '"
                                                + Pauschal + "', "
                                                + "'1'," 
                                                + "'1', '"
                                                + B_ID+ "','"
                                                + E_ID + "', '"
                                                + PreisTo.ToString().Replace(',', '.') + "', '"
                                                + MwStSatz.ToString().Replace(',', '.') + "', '"
                                                + ZusatzKosten.ToString().Replace(',', '.') + "', '"
                                                + TextZusatzkosten + "', '"
                                                + GS + "', '"
                                                + GS_Date + "', '"
                                                + PreisTo.ToString().Replace(',', '.') + "')";
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
    //--------- Update vorhandener Datensäzte ---------------------
    //
    private void UpdateFracht()
    {
      string sql = string.Empty;
      try
      {
        //--- initialisierung des sqlcommand---
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;

        //----- SQL Abfrage -----------------------
        switch (FrachtBezeichnung)
        {
          case "Rechnung":
            sql = "Update Frachten SET Artikel_ID='" + Artikel_ID + "', " +
                                       "Fracht='" + Fracht.ToString().Replace(',', '.') + "', " +
                                       "Fracht_ADR='" + KundeID + "', " +
                                       "Frachttext='" + FrachtText + "', " +
                                       "km ='" + km + "', " +
                                       "fpflGewicht ='" + fpflGewicht.ToString().Replace(',', '.') + "', " +
                                       "Pauschal ='" + Pauschal + "', " +
                                       "GS='0', " +
                                       "MargeEuro='" + MargeEuro.ToString().Replace(',', '.') + "', " +
                                       "MwStSatz='" + MwStSatz.ToString().Replace(',', '.') + "', " +
                                       "ZusatzKosten='" + ZusatzKosten.ToString().Replace(',', '.') + "', " +
                                       "TextZusatzkosten='" + TextZusatzkosten + "', " +
                                       "Frachtsatz='" + PreisTo.ToString().Replace(',', '.') + "' " +
                                       "WHERE AP_ID='" + AP_ID + "' " +
                                       "AND GS='0'";
            break;
          case "GutschriftSU":
            sql = "Update Frachten SET Artikel_ID='" + Artikel_ID + "', " +
                                       "Fracht='" + Fracht.ToString().Replace(',', '.') + "', " +
                                       "Fracht_ADR='" + KundeID + "', " +
                                       "Frachttext='" + FrachtText + "', " +
                                       "km ='" + km + "', " +
                                       "fpflGewicht ='" + fpflGewicht.ToString().Replace(',', '.') + "', " +
                                       "Pauschal ='" + Pauschal + "', " +
                                       "GS='1', " +
                                       "GS_SU='1', " +
                                       "MargeEuro='" + MargeEuro.ToString().Replace(',', '.') + "', " +
                                       "MwStSatz='" + MwStSatz.ToString().Replace(',', '.') + "', " +
                                       "ZusatzKosten='" + ZusatzKosten.ToString().Replace(',', '.') + "', " +
                                       "TextZusatzkosten='" + TextZusatzkosten + "', " +
                                       "Frachtsatz='" + PreisTo.ToString().Replace(',', '.') + "' " +
                                       "WHERE AP_ID='" + AP_ID + "' "+
                                       "AND GS='1' AND GS_SU='1'";
             break;
          case "Gutschrift":

             sql = "Update Frachten SET Artikel_ID='" + Artikel_ID + "', " +
                                          "Fracht='" + Fracht.ToString().Replace(',', '.') + "', " +
                                          "Fracht_ADR='" + KundeID + "', " +
                                          "Frachttext='" + FrachtText + "', " +
                                          "km ='" + km + "','" +
                                          "fpflGewicht ='" + fpflGewicht.ToString().Replace(',', '.') + "', " +
                                          "Pauschal ='" + Pauschal + "', " +
                                          "GS='1', " +
                                          "MargeEuro='" + MargeEuro.ToString().Replace(',', '.') + "', " +
                                          "MwStSatz='" + MwStSatz.ToString().Replace(',', '.') + "', " +
                                          "ZusatzKosten='" + ZusatzKosten.ToString().Replace(',', '.') + "', " +
                                          "TextZusatzkosten='" + TextZusatzkosten + "', " +
                                          "GS_ID ='"+GS + "', "+
                                          "GS_Datum='"+GS_Date + "', "+
                                          "Frachtsatz='" + PreisTo.ToString().Replace(',', '.') + "' " +
                                          "WHERE AP_ID='" + AP_ID + "'"+
                                          "AND GS='1' AND GS_SU='0'";
            break;
          case "FVGS":

            sql = "Update Frachten SET Artikel_ID='" + Artikel_ID + "', " +
                                         "Fracht='" + Fracht.ToString().Replace(',', '.') + "', " +
                                         "Fracht_ADR='" + KundeID + "', " +
                                         "Frachttext='" + FrachtText + "', " +
                                         "km ='" + km + "', " +
                                         "fpflGewicht ='" + fpflGewicht.ToString().Replace(',', '.') + "', " +
                                         "Pauschal ='" + Pauschal + "', " +
                                         "GS='1', " +
                                         "FVGS='1', " +
                                         "FV_V_ID ='" + B_ID + "', " +
                                         "FV_E_ID ='" + E_ID + "', " +
                                         "MargeEuro='" + MargeEuro.ToString().Replace(',', '.') + "', " +
                                         "MwStSatz='" + MwStSatz.ToString().Replace(',', '.') + "', " +
                                         "ZusatzKosten='" + ZusatzKosten.ToString().Replace(',', '.') + "', " +
                                         "TextZusatzkosten='" + TextZusatzkosten + "', " +
                                         "GS_ID ='" + GS + "', " +
                                         "GS_Datum='" + GS_Date + "', " +
                                         "Frachtsatz='" + PreisTo.ToString().Replace(',', '.') + "' " +
                                         "WHERE AP_ID='" + AP_ID + "'"+
                                         "AND GS='1' AND FVGS='1'";
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
    //---------- Update FV ADRESSEN Versender - Empfänger    -------------------------
    //
    public static void updateFVADR(Int32 AP_ID, string ADRBezeichnung, Int32 ADR_ID)
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
                case "Versender":
                    sql = "Update Frachten SET FV_V_ID='" + ADR_ID + "' WHERE AP_ID='" + AP_ID + "'";
                    break;
                case "Empfänger":
                    sql = "Update Frachten SET FV_E_ID='" + ADR_ID + "' WHERE AP_ID='" + AP_ID + "'";
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
    //------------ AuftragspositionsIDs für Statusänderung in Auftragspositionen --------
    //
    public static DataTable GetAP_IDFromFrachten(Int32 RGNummer)
    {
      DataTable dt = new DataTable("Auftragspositionen");

      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT AP_ID From Frachten WHERE RG_ID= '" + RGNummer + "'";

      Globals.SQLcon.Open();
      ada.Fill(dt);
      ada.Dispose();
      Command.Dispose();
      Globals.SQLcon.Close();

      if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
      {
        Command.Connection.Close();
      }
      return dt;
    }
    
    //
    //------------ RGNummer aus DB Frachten --------
    //
    public static Int32 GetRGNummerFromFrachten(Int32 AP_ID, bool GS, bool FVGS)
    {
      Int32 RGNr = 0;
      string sql = string.Empty;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;

      sql = "Select RG_ID FROM Frachten WHERE AP_ID='" + AP_ID + "' AND ";

      if (GS)
      {
        if (FVGS)
        {
          //FVGS
          sql = sql + "GS='1' AND FVGS='1'";
        }
        else
        {
          sql = sql + "GS='1' AND FVGS='0'";
        }
      }
      else
      { 
        //RG
        sql = sql + "GS='0'";
      }
      
      Command.CommandText = sql;

      Globals.SQLcon.Open();
      object obj = Command.ExecuteScalar();
      if (obj != null)
      {
        RGNr = (Int32)obj; ;
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
      {
        Command.Connection.Close();
      }
      return RGNr;
    }
    //
    //---------- Fracht AuftragPosition bereits vorhanden -----------------
    //
    public static bool IsAPisInFVGS(Int32 AP_ID)
    {
        bool IsUsed = false;
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;
        Command.CommandText = "SELECT ID From Frachten WHERE AP_ID= '" + AP_ID + "' AND FVGS='1'";

        Globals.SQLcon.Open();
        object obj = Command.ExecuteScalar();
        if (obj == null)
        {
            IsUsed = false;
        }
        else
        {
            IsUsed = true;
            Int32 ID = (Int32)obj;
        }
        Command.Dispose();
        Globals.SQLcon.Close();
        return IsUsed;
    }
    //
    //---------- DB Fracht - AuftragPosition bereits vorhanden -----------------
    //
    public static bool IsAPisInRG(Int32 AP_ID, bool RG)
    {
      bool IsUsed = false;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      if (!RG)  //GUtschrift
      {
        Int32 ibit = 1;
        Command.CommandText = "SELECT ID From Frachten WHERE AP_ID= '" + AP_ID + "' AND GS='" + ibit + "' AND GS_SU='0' AND FVGS='0'";
      }
      else // Rechnung
      {
        Int32 ibit = 0;
        Command.CommandText = "SELECT ID From Frachten WHERE AP_ID= '" + AP_ID + "' AND GS='" + ibit + "'  AND GS_SU='0'";
      }

      Globals.SQLcon.Open();
      object obj = Command.ExecuteScalar();
      if (obj == null)
      {
        IsUsed = false;
      }
      else
      {
        IsUsed = true;
        Int32 ID = (Int32)obj;
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      return IsUsed;
    }    
    //
    //---------- Gibt an ob GS, GSSU, FVGS nach Abfrage -----------------
    //
    public static bool CheckForAbrechnungsArt(Int32 AP_ID, bool GS, bool FVGS, bool GSSU)
    {
      bool IsUsed = false;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      if (GS)
      {
        if (GSSU)
        {
          //GSanSU
          Command.CommandText = "SELECT ID From Frachten WHERE AP_ID= '" + AP_ID + "' AND GS='1' AND GS_SU='1'";
        }
        else
        {
          if (FVGS)
          {
            Command.CommandText = "SELECT ID From Frachten WHERE AP_ID= '" + AP_ID + "' AND GS='1' AND FVGS='1'";
          }
          else
          {
            Command.CommandText = "SELECT ID From Frachten WHERE AP_ID= '" + AP_ID + "' AND GS='0' AND FVGS='0'";
          }
        }
      }
      else
      { 
        //RG
        Command.CommandText = "SELECT ID From Frachten WHERE AP_ID= '" + AP_ID + "' AND GS='0'";
      }

      Globals.SQLcon.Open();
      object obj = Command.ExecuteScalar();
      if (obj == null)
      {
        IsUsed = false;
      }
      else
      {
        IsUsed = true;
        Int32 ID = (Int32)obj;
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      return IsUsed;
    }
    //
    //
    //
    public static bool IsAPinFrachtenFVGS(Int32 AP_ID)
    {
      bool IsUsed = false;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT ID From Frachten WHERE AP_ID= '" + AP_ID + "' AND GS='1' AND FVGS='1'";

      Globals.SQLcon.Open();
      object obj = Command.ExecuteScalar();
      if (obj == null)
      {
        IsUsed = false;
      }
      else
      {
        IsUsed = true;
        Int32 ID = (Int32)obj;
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      return IsUsed;
    }
    //
    //----------- Fracht Auftragsposition SU vorhanden ----------
    //
    public static bool IsAPisInRGSU(Int32 AP_ID)
    {
      bool IsUsed = false;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT ID From Frachten WHERE AP_ID= '" + AP_ID + "' AND GS='1' AND GS_SU='1'";

      Globals.SQLcon.Open();
      object obj = Command.ExecuteScalar();
      if (obj == null)
      {
        IsUsed = false;
      }
      else
      {
        IsUsed = true;
        Int32 ID = (Int32)obj;
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      return IsUsed;
    }
    //
    //---------- Fracht AuftragPosition bereits vorhanden -----------------
    //
    public static bool IsAPisInRGArtikel(Int32 AP_ID, Int32 ArtikelID, bool RG)
    {
      Int32 ibit = 0;
      if (!RG)
      {
        ibit = 1;
      }
      bool IsUsed = false;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT ID From Frachten WHERE AP_ID= '" + AP_ID + "' AND GS='" + ibit + "' AND Artikel_ID='"+ArtikelID+"'";

      Globals.SQLcon.Open();
      object obj = Command.ExecuteScalar();
      if (obj == null)
      {
        IsUsed = false;
      }
      else
      {
        IsUsed = true;
        Int32 ID = (Int32)obj;
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      return IsUsed;
    }
    //
    //----------- Auslesen FVGS -------------------
    //
    public DataTable GetFVGSDaten()
    {
      DataTable dt = new DataTable("FVGS_Daten");
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT * FROM Frachten WHERE AP_ID='" + AP_ID + "' AND FVGS='1'"; 
      Globals.SQLcon.Open();

      ada.Fill(dt);
      Command.Dispose();
      Globals.SQLcon.Close();
      return dt;
    }
    //
    //------------ Read Datensatz ----------------
    //
    public DataTable GetFrachtDatenByRGID(Int32 RG_ID)
    {
      string sql = string.Empty;
      DataTable dataTable = new DataTable("Frachtdaten");
      dataTable.Clear();

      if (RG_ID > 0)
      {
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;
        sql = "SELECT " +
                                  "ID, " +
                                  "AP_ID, " +
                                  "(Select Auftrag_ID FROM AuftragPos WHERE ID=AP_ID) as 'AuftragID', " +
                                  "(Select AuftragPos FROM AuftragPos WHERE ID=AP_ID) as 'AuftragPos'," +
                                  "Fracht, " +
                                  "Fracht_ADR, " +
                                  "(SELECT FBez FROM ADR WHERE ID=Frachten.Fracht_ADR) as 'KD_FBez', " +
                                  "(SELECT Name1 FROM ADR WHERE ID=Frachten.Fracht_ADR) as 'KD_Name1', " +
                                  "(SELECT Name2 FROM ADR WHERE ID=Frachten.Fracht_ADR) as 'KD_Name2', " +
                                  "(SELECT Name3 FROM ADR WHERE ID=Frachten.Fracht_ADR) as 'KD_Name3', " +
                                  "(SELECT Str FROM ADR WHERE ID=Frachten.Fracht_ADR) as 'KD_Strasse', " +
                                  "(SELECT PF FROM ADR WHERE ID=Frachten.Fracht_ADR) as 'KD_PF', " +
                                  "(SELECT PLZ FROM ADR WHERE ID=Frachten.Fracht_ADR) as 'KD_PLZ', " +
                                  "(SELECT PLZPF FROM ADR WHERE ID=Frachten.Fracht_ADR) as 'KD_PLZPF', " +
                                  "(SELECT PLZPF FROM ADR WHERE ID=Frachten.Fracht_ADR) as 'KD_Ort', " +
                                  "Frachttext, " +
                                  "km, " +
                                  "fpflGewicht, " +
                                  "Pauschal, " +
                                  "GS, " +
                                  "GS_SU, " +
                                  "FVGS, " +
                                  "FV_V_ID, " +
                                  "FV_E_ID, " +
                                  "MargeEuro, " +
                                  "Frachtsatz, " +
                                  "MwStSatz, " +
                                  "ZusatzKosten, " +
                                  "TextZusatzkosten, " +
                                  "GS_ID, " +
                                  "GS_Datum, " +
                                  "RG_ID, " +
                                  "(Select Datum FROM Rechnungen WHERE RGNr=Frachten.RG_ID) as 'RG_Datum',  " +
                                  "(SELECT B_Zeit FROM Kommission WHERE PosID=AP_ID) as 'B_Datum', " +
                                  "(SELECT B_Date FROM Frachtvergabe WHERE ID_AP=AP_ID) as 'B_Datum_SU' " +
                                  "FROM Frachten " +
                                  "WHERE RG_ID='" + RG_ID + "'";

 
        Command.CommandText = sql;
        ada.Fill(dataTable);
        ada.Dispose();
        Command.Dispose();
        Globals.SQLcon.Close();
      }
      else
      {
        clsMessages.Fakturierung_LesenFrachtdatenByRGID();
        
      }
      return dataTable;
    }
    //
    //------------ Read Datensatz ----------------
    //
    public DataTable GetFrachtDaten(bool Einzelposition, bool GS)
    {
      string sql = string.Empty;
      DataTable dataTable = new DataTable("Frachtdaten");
      dataTable.Clear();

      if (AP_ID > 0)
      {
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;
        sql = "SELECT " +
                                  "ID, " +
                                  "AP_ID, " +
                                  "(Select Auftrag_ID FROM AuftragPos WHERE ID=AP_ID) as 'AuftragID', " +
                                  "(Select AuftragPos FROM AuftragPos WHERE ID=AP_ID) as 'AuftragPos'," +
                                  "Fracht, " +
                                  "Fracht_ADR, " +
                                  "(SELECT FBez FROM ADR WHERE ID=Frachten.Fracht_ADR) as 'KD_FBez', " +
                                  "(SELECT Name1 FROM ADR WHERE ID=Frachten.Fracht_ADR) as 'KD_Name1', " +
                                  "(SELECT Name2 FROM ADR WHERE ID=Frachten.Fracht_ADR) as 'KD_Name2', " +
                                  "(SELECT Name3 FROM ADR WHERE ID=Frachten.Fracht_ADR) as 'KD_Name3', " +
                                  "(SELECT Str FROM ADR WHERE ID=Frachten.Fracht_ADR) as 'KD_Strasse', " +
                                  "(SELECT PF FROM ADR WHERE ID=Frachten.Fracht_ADR) as 'KD_PF', " +
                                  "(SELECT PLZ FROM ADR WHERE ID=Frachten.Fracht_ADR) as 'KD_PLZ', " +
                                  "(SELECT PLZPF FROM ADR WHERE ID=Frachten.Fracht_ADR) as 'KD_PLZPF', " +
                                  "(SELECT PLZPF FROM ADR WHERE ID=Frachten.Fracht_ADR) as 'KD_Ort', " +
                                  "Frachttext, " +
                                  "km, " +
                                  "fpflGewicht, " +
                                  "Pauschal, " +
                                  "GS, " +
                                  "GS_SU, " +
                                  "FVGS, " +
                                  "FV_V_ID, " +
                                  "FV_E_ID, " +
                                  "MargeEuro, " +
                                  "Frachtsatz, " +
                                  "MwStSatz, " +
                                  "ZusatzKosten, " +
                                  "TextZusatzkosten, " +
                                  "GS_ID, " +
                                  "GS_Datum, " +
                                  "RG_ID, " +
                                  "(SELECT B_Zeit FROM Kommission WHERE PosID=AP_ID) as 'B_Datum', " +
                                  "(SELECT B_Date FROM Frachtvergabe WHERE ID_AP=AP_ID) as 'B_Datum_SU' " +
                                  "FROM Frachten ";

               
        if (Einzelposition == true)
        {
          sql = sql + "WHERE AP_ID='" + AP_ID + "' ";
        }
        else
        {
          sql = sql + "WHERE Fracht_ADR='" + KundeID + "' AND RG_ID='0' ";
        }
        /***
        switch (Abrechnungsart)
        { 
          case "RG":
            sql = sql + "AND GS='0' ";
            break;
          case "GS":
            sql = sql + "AND GS='1' AND GS_SU='0'";
            break;
          case "GSSU":
            sql = sql + "AND GS='1' AND GS_SU='1'";
            break;
        }
        ****/
        if (GS)
        {
          sql = sql + "AND GS='1' ";
        }
        else
        {
          sql = sql + "AND GS='0' ";
        }
    


        Command.CommandText = sql;
        ada.Fill(dataTable);
        ada.Dispose();
        Command.Dispose();
        Globals.SQLcon.Close();
      }
      else
      {
        clsMessages.Fakturierung_LesenFrachtdaten();

      }
      return dataTable;
    }
    //
    //------------------- löschen von Rechnung - Update RGnr = 0 ---------------
    //
    public static void UpdateRGNrToZero(Int32 RG_ID)
    {
      string sql = string.Empty;
      try
      {
        //--- initialisierung des sqlcommand---
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;

        sql = "Update Frachten SET RG_ID ='0' WHERE RG_ID='" + RG_ID + "'";

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
    /**************************************************************************************+
     *                    manuelle Erfassung RG / GS
     * 
     * ************************************************************************************/
    //
    //
    public DataSet AddToDB(DataSet ds)
    {
      ds=SetDatenFromDS(ds);
      return ds;
    }
    //
    private DataSet SetDatenFromDS(DataSet ds)
    {
      if (ds.Tables["RGGSDaten"].Rows.Count > 0)
      {
        for (Int32 i = 0; i <= ds.Tables["RGGSDaten"].Rows.Count - 1; i++)
        {
          AP_ID = (Int32)ds.Tables["RGGSDaten"].Rows[i]["AP_ID"];
          Fracht = (decimal)ds.Tables["RGGSDaten"].Rows[i]["Fracht"];
          KundeID = (Int32)ds.Tables["RGGSDaten"].Rows[i]["Fracht_ADR"];
          FrachtText = (string)ds.Tables["RGGSDaten"].Rows[i]["Frachttext"];
          MwStSatz = (decimal)ds.Tables["RGGSDaten"].Rows[i]["MwStSatz"];
          zwZM = (Int32)ds.Tables["RGGSDaten"].Rows[i]["zw_ZM"];
          zwAufliefer = (Int32)ds.Tables["RGGSDaten"].Rows[i]["zw_Auflieger"];
          zwSU = (Int32)ds.Tables["RGGSDaten"].Rows[i]["zw_SU"];
          bool boGS = (bool)ds.Tables["RGGSDaten"].Rows[i]["GS"];
          InsertFrachtRGGS(boGS);
          ds.Tables["RGGSDaten"].Rows[i]["Frachten_ID"] = GetIDFromDBbyDaten(boGS);
        }
      }
      return ds;
    }
    //
    //----------- Add to DB ----------------
    //
    private void InsertFrachtRGGS(bool boolGS)
    {
      Int32 iBit = 0;
      if(boolGS)
      {
        iBit=1;
      }
      string sql = string.Empty;
      try
      {
        //--- initialisierung des sqlcommand---
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;

        //GS-Datum ist nur intern um den Datensatz weiter zu spezifizieren
        //----- SQL Abfrage -----------------------
         sql = "INSERT INTO Frachten (AP_ID, Fracht, Fracht_ADR, Frachttext, GS, GS_Datum, MwStSatz, zw_ZM, zw_Auflieger, zw_SU) " +
                                        "VALUES ('" + AP_ID + "', '"
                                                    + Fracht.ToString().Replace(',', '.') + "', '"
                                                    + KundeID + "', '"
                                                    + FrachtText + "', '"
                                                    + iBit+ "', '"
                                                    + DateTime.Now.Date+"', '"
                                                    + MwStSatz.ToString().Replace(',', '.') + "', '"
                                                    + zwZM + "', '"
                                                    + zwAufliefer+ "', '"
                                                    + zwSU + "')";

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
    //------------ ID DB Frachten ----------
    //
    private Int32 GetIDFromDBbyDaten(bool boolGS)
    {
      Int32 iBit = 0;
      if(boolGS)
      {
        iBit=1;
      }
      Int32 Frachten_ID = 0;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT ID FROM Frachten WHERE AP_ID='"+AP_ID+"' AND "+ 
                                                          "Fracht='" + Fracht.ToString().Replace(',', '.') + "' AND " +
                                                          "Frachttext='"+FrachtText+"' AND "+
                                                          "Fracht_ADR='"+KundeID+"' AND "+
                                                          "GS='"+iBit+"' AND " +
                                                          "GS_Datum='"+DateTime.Now.Date+"' AND " +
                                                          "zw_ZM='"+zwZM+"' AND "+
                                                          "zw_Auflieger='"+zwAufliefer+"' AND "+
                                                          "zw_SU='"+zwSU+"'";

      Globals.SQLcon.Open();

      object obj = Command.ExecuteScalar();
      if (obj != null)
      {
        Frachten_ID = (Int32)obj; ;
      }

      Command.Dispose();
      Globals.SQLcon.Close();
      return Frachten_ID;
    }
    //
    //------- Daten aus manuelle RG- / GS-Erstellung --------------
    //
    public DataTable GetManRGGSDaten(Int32 KD)
    {
      DataTable dataTable = new DataTable("Auftragsdaten");
      try
      {
        string sql = string.Empty;
        dataTable.Clear();
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;

        sql = "SELECT " +
                        "* "+
                        "FROM Frachten "+
                        "WHERE RG_ID='0' "+
                        "AND Fracht_ADR='"+KD+"'";

        Command.CommandText = sql;
        ada.Fill(dataTable);
        ada.Dispose();
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
      return dataTable;
    }
    //
    //---------- Auftrag Pos. ID by Fracht ID -----------
    //
    public static Int32 GetAPByFrachtID(Int32 FrachtID)
    {
      Int32 AP_ID = 0;
      try
      {
        string sql = string.Empty;
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;

        sql = "SELECT AP_ID FROM Frachten WHERE ID='" + FrachtID + "'";
        Command.CommandText = sql;
        Globals.SQLcon.Open();

        object obj = Command.ExecuteScalar();
        if (obj != null)
        {
          AP_ID = (Int32)obj; ;
        }

        Command.Dispose();
        Globals.SQLcon.Close();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }
      return AP_ID;
    } 
  }
}
