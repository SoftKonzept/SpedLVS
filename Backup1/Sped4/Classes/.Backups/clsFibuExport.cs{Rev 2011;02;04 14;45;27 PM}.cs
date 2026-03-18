using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Sped4.Classes
{
  class clsFibuExport
  {
    Int32 iBit1 = 1;
    Int32 iBit0 = 0;
    
    private Int32 _ID;
    private Int32 _Rechnungen_ID;         //ID DB Rechnungen
    private Int32 _Frachten_ID;           //ID DB Frachten für Gutschriften
    private decimal _MwStSatz;
    private decimal _MwStBetrag;
    private Int32 _EmpfaengerID;
    private string _Empfaenger;
    private DateTime _Beladedatum;
    private Int32 _Benutzer;
    private DateTime _BenutzerDatum;
    private bool _GS;
    private Int32 _iBitGS;
    private bool _FVGS;
    private Int32 _iBitFVGS;
    private Int32 _iBitGSanSU;
    private bool _GSanSU;
    private bool _gesendet;

    //aus der Typenbibl.
    private Int32 _BelegNummer;     //RG-GS ID
    private DateTime _Belegdatum;   // RG Datum
    private DateTime _Valutadatum;
    private Int32 _OPNummer;
    private Int32 _KontoNummer;   //KD ID - Debitoren Nummer
    private decimal _BruttoBetrag;
    private Int32 _ZBDNummer;
    private string _BuchungsText;
    private Int32 _Sachkonto;
    private bool _Sammelbuchung;
    private Int32 _iBitSammelbuchung;
    private decimal _NettoBetrag;

    private Int32 _RGGSArt;


    public Int32 ID
    {
      get { return _ID; }
      set { _ID = value; }
    }
    public Int32 Rechnungen_ID
    {
      get { return _Rechnungen_ID; }
      set { _Rechnungen_ID = value; }
    }
    public Int32 RGGSArt
    {
      get { return _RGGSArt; }
      set { _RGGSArt = value; }
    }
    public Int32 Frachten_ID
    {
      get { return _Frachten_ID; }
      set { _Frachten_ID = value; }
    }
    public decimal MwStSatz
    {
      get { return _MwStSatz; }
      set { _MwStSatz = value; }
    }
    public decimal MwStBetrag
    {
      get { return _MwStBetrag; }
      set { _MwStBetrag = value; }
    }
    public Int32 EmpfaengerID
    {
      get { return _EmpfaengerID; }
      set { _EmpfaengerID = value; }
    }
    public string Empfaenger
    {
      get { return _Empfaenger; }
      set { _Empfaenger = value; }
    }
    public DateTime Beladedatum
    {
      get { return _Beladedatum; }
      set { _Beladedatum = value; }
    }
    public bool gesendet
    {
      get { return _gesendet; }
      set { _gesendet = value; }
    }
    public Int32 BelegNummer
    {
      get { return _BelegNummer; }
      set { _BelegNummer = value; }
    }
    public DateTime Belegdatum
    {
      get { return _Belegdatum; }
      set { _Belegdatum = value; }
    }
    public DateTime Valutadatum
    {
      get 
      {
        _Valutadatum = Belegdatum;
        return _Valutadatum; }
      set { _Valutadatum = value; }
    }
    public Int32 OPNummer
    {
      get { return _OPNummer; }
      set { _OPNummer = value; }
    }
    public Int32 KontoNummer
    {
      get { return _KontoNummer; }
      set { _KontoNummer = value; }
    }
    public decimal BruttoBetrag
    {
      get { return _BruttoBetrag; }
      set { _BruttoBetrag = value; }
    }
    public Int32 ZBDNummer
    {
      get { return _ZBDNummer; }
      set { _ZBDNummer = value; }
    }
    public string BuchungsText
    {
      get { return _BuchungsText; }
      set { _BuchungsText = value; }
    }
    public Int32 Sachkonto
    {
      get { return _Sachkonto; }
      set { _Sachkonto = value; }
    }
    public bool Sammelbuchung
    {
      get { return _Sammelbuchung; }
      set { _Sammelbuchung = value; }
    }
    public decimal NettoBetrag
    {
      get { return _NettoBetrag; }
      set { _NettoBetrag = value; }
    }
    public Int32 Benutzer
    {
      get { return _Benutzer; }
      set { _Benutzer = value; }
    }
    public DateTime BenutzerDatum
    {
      get { return _BenutzerDatum; }
      set { _BenutzerDatum = value; }
    }
    public bool GS
    {
      get { return _GS; }
      set { _GS = value; }
    }
    public bool FVGS
    {
      get { return _FVGS; }
      set { _FVGS = value; }
    }
    public bool GSanSU
    {
      get { return _GSanSU; }
      set { _GSanSU = value; }
    }
    public Int32 iBitGSanSU
    {
      get 
      {
        if (GSanSU)
        {
          _iBitGSanSU = iBit1;
        }
        else
        {
          _iBitGSanSU = iBit0;
        }
        return _iBitGSanSU; }
      set { _iBitGSanSU = value; }
    }
    public Int32 iBitGS
    {
      get
      {
        if (GS)
        {
          _iBitGS = iBit1;
        }
        else
        {
          _iBitGS = iBit0;
        }
        return _iBitGS;
      }
      set { _iBitGS = value; }
    }
    public Int32 iBitFVGS
    {
      get
      {
        if (FVGS)
        {
          _iBitFVGS = iBit1;
        }
        else
        {
          _iBitFVGS = iBit0;
        }
        return _iBitFVGS;
      }
      set { _iBitFVGS = value; }
    }
    public Int32 iBitSammelbuchung
    {
      get
      {
        if (Sammelbuchung)
        {
          _iBitSammelbuchung = iBit1;
        }
        else
        {
          _iBitSammelbuchung = iBit0;
        }
        return _iBitSammelbuchung;
      }
      set { _iBitSammelbuchung = value; }
    }
    /***********************************************************************************************
     * 
     * 
     * ********************************************************************************************/
    //
    //--------- Eintrag in DB xFibu -----------------
    //
    private void AddToxFibu()
    { 
      try
      {
        //--- initialisierung des sqlcommand---
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        Command.CommandText = ("INSERT INTO Rechnungen (Rechnungen_ID, "+
                                                       "Belegnummer, "+
                                                       "GS, "+
                                                       "FVGS, "+
                                                       "GSanSU, "+
                                                       "KontoNummer, "+
                                                       "NettoBetrag, "+
                                                       "BruttoBetrag, "+
                                                       "ZBDNummer, "+
                                                       "BuchungsText, "+
                                                       "Sachkonto, " +
                                                       "Sammelbuchung, "+
                                                       "EmpfaengerID, "+
                                                       "Empfaenger, "+
                                                       "DebKto, " +
                                                       "Benutzer, "+
                                                       "Benutzer_Datum "+
                                                        
                                          "VALUES ('" + Rechnungen_ID + "','" 
                                                      + BelegNummer + "','"
                                                      + iBitGS + "','"
                                                      + iBitFVGS + "','"
                                                      + iBitGSanSU + "','"
                                                      + KontoNummer+ "','"
                                                      + NettoBetrag.ToString().Replace(",", ".") + "','"
                                                      + BruttoBetrag.ToString().Replace(",", ".") + "','"
                                                      + ZBDNummer + "','"
                                                      + BuchungsText + "','"                                                                                            + FVGS + "','"
                                                      + Sachkonto + "','"
                                                      + iBitSammelbuchung + "','"
                                                      + EmpfaengerID + "','"
                                                      + Empfaenger + "','"
                                                      + KontoNummer + "','"
                                                      + Benutzer + "','"
                                                      + BenutzerDatum + "')");

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
    //-------- Daten der zu exportierenden RG/GS ------------------
    //
    public DataTable GetDatenExportToFibu(string Exportliste)
    {
      DataTable dt = new DataTable("FIBUExport");
      dt.Clear();
      if (Exportliste != string.Empty)
      {
        string sql = string.Empty;
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();

        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;

        sql = "SELECT " +
                      "Rechnungen.ID as 'Rechnungs_ID', " +
                      "Frachten.ID as 'Frachten_ID', " +
                      "Rechnungen.RGNr as 'BelegNummer', " +
                      "Rechnungen.Datum as 'Belegdatum' ," +
                      "Frachten.GS_ID as 'GS_ID', " +
                      "Frachten.GS as 'GS', " +
                      "Frachten.GS_SU as 'GS_SU', " +
                      "Frachten.FVGS as 'FVGS', " +
                      "(SELECT KD_ID FROM Kunde WHERE ADR_ID=Frachten.Fracht_ADR) as 'KontoNummer', " +
                      "(SELECT Name1 FROM ADR WHERE ID=Frachten.Fracht_ADR) as 'Empfaenger', " +
                      "(SELECT SUM(Frachten.Fracht)+SUM(Frachten.ZusatzKosten) FROM Frachten WHERE RG_ID=Rechnungen.RGNr) as 'Nettobetrag', " +
                      "Rechnungen.MwStSatz as 'MwStSatz', " +
                      "(SELECT ((SUM(Frachten.Fracht)+SUM(Frachten.ZusatzKosten))*Rechnungen.MwStSatz)/100 FROM Frachten WHERE RG_ID=Rechnungen.RGNr) as 'MwStBetrag', " +
                      "(SELECT (SUM(Frachten.Fracht)+SUM(Frachten.ZusatzKosten))+(((SUM(Frachten.Fracht)+SUM(Frachten.ZusatzKosten))*Rechnungen.MwStSatz)/100) FROM Frachten WHERE RG_ID=Rechnungen.RGNr) as 'Bruttobetrag' " ;
        
        switch (Exportliste)
        { 
          case "RG":
            sql = sql + "FROM Rechnungen " +
                                      "INNER JOIN Frachten ON Rechnungen.RGNr=Frachten.RG_ID " +
                                      "WHERE " +
                                      "Frachten.GS='0' AND " +
                                      "Rechnungen.ID NOT IN (SELECT Rechnungen_ID FROM xFIBU)";
            break;

          case "GS":

            break;
          case "GSSU":
            sql = sql + "FROM Rechnungen " +
                          "INNER JOIN Frachten ON Rechnungen.RGNr=Frachten.RG_ID " +
                          "WHERE " +
                          "Frachten.GS='1' AND Frachten.GS_SU='1' AND "+
                          "Rechnungen.ID NOT IN (SELECT Rechnungen_ID FROM xFIBU)";
            break;
          case "FVGS":

            break;
          case "All":

            break;
        }

        Command.CommandText = sql;
        ada.Fill(dt);
        ada.Dispose();
        Command.Dispose();
        Globals.SQLcon.Close();

        AddColToDataTable(ref dt);
        SetRGGSArt(ref dt);
        
      }
      return dt;
    }
    //
    private void AddColToDataTable(ref DataTable dt)
    {
      DataColumn dtc = new DataColumn();
      dtc.DataType = System.Type.GetType("System.Int32");
      dtc.ColumnName = "RGGSArt";
      dt.Columns.Add(dtc);

      DataColumn dtc1 = new DataColumn();
      dtc1.DataType = System.Type.GetType("System.Boolean");
      dtc1.ColumnName = "gesendet";
      dt.Columns.Add(dtc1);
    }
    //
    //------ Setzt ID für RG/GS-Art ----------------
    // 1 = Rechnung (Ausgangsrechnung)
    // 2 = Gutschrift (Eingangsgutschrift)
    // 3 = FVGS (Eingangsgutschrift Frachtvorlage)
    // 4 = GS an Unternehmer 
    //
    private void SetRGGSArt(ref DataTable dt)
    {
      for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
      { 
        //Check auf FVGS
        if ((bool)dt.Rows[i]["FVGS"])
        {
          if(((bool)dt.Rows[i]["GS"]) & (!(bool)dt.Rows[i]["FVanSU"]))
          {
            //FVGS
            dt.Rows[i]["RGGSArt"] = 3;
          }
        }
        else
        {
          if ((bool)dt.Rows[i]["GS"])
          {
            if ((bool)dt.Rows[i]["GS_SU"])
            {
              //GS an SU
              dt.Rows[i]["RGGSArt"] = 4;
            }
            else
            {
              dt.Rows[i]["RGGSArt"] = 2;
            }
          }
          else
          {
            //RG
            dt.Rows[i]["RGGSArt"] = 1;
          }
          
        }
      }
    }
  }
}
