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
  class clsTarif
  {
    public string TarifArt;
    public bool MoreMaxKm = false; 
    public DataTable dtMaxKm = new DataTable("MaxKm");
    internal bool kmGrMax = false;

    private Int32 _km;
    private Int32 _Maxkm;
    private Int32 _zuKm;
    private decimal _PreisTo;
    private decimal _aufPreisTo;
    private decimal _fpflGewicht;
    private string _FrachtText;
    private decimal _Fracht;
    private decimal _MargeProzent;
    private decimal _MargeEuro;
    private bool _Pauschal;
    private string _Col;
    private string _TarifAngabe;
    public decimal Fracht
    {
      get { return _Fracht; }
      set { _Fracht = value; }
    }
    public Int32 km
    {
      get { return _km; }
      set { _km = value; }
    }
    public Int32 Maxkm
    {
      get { return _Maxkm; }
      set { _Maxkm = value; }
    }
    public Int32 zuKm
    {
      get { return _zuKm; }
      set { _zuKm = value; }
    }
    public decimal PreisTo
    {
      get { return _PreisTo; }
      set { _PreisTo = value; }
    }
    public decimal aufPreisTo
    {
      get { return _aufPreisTo; }
      set { _aufPreisTo = value; }
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
    public string Col
    {
      get { return _Col; }
      set { _Col = value; }
    }
    public string TarifAngabe
    {
      get 
      {
       // _TarifAngabe = TarifArt + "-" + Functions.FromatDecimal(Math.Round((fpflGewicht / 1000), 1, MidpointRounding.AwayFromZero)) + "to-" + km + "km-" + Functions.FromatDecimal(PreisTo) + "€/to";
        _TarifAngabe = TarifArt + "-" + Functions.FromatDecimal((Math.Ceiling((fpflGewicht / 100))/10)) + "to-" + km + "km-" + Functions.FromatDecimal(PreisTo) + "€/to";
        return _TarifAngabe; 
      }
      set { _TarifAngabe = value; }
    }

    /*****************************************************************************************************
     * 
     * 
     * **************************************************************************************************/
    //
    //
    //
    public void GetFracht()
    {
      //Daten einlesen
      Int32 Suchgewicht =1+ Convert.ToInt32(Math.Round((fpflGewicht / 1000),0));
      Suchgewicht = SetColToMin(Suchgewicht); 
      Col =Convert.ToString(Suchgewicht)+"t";
        

      GetMaxKmTable();
      SetMaxKmAndZugschlag();

      switch (TarifArt)
      { 
        case "GNT":
          if (Maxkm < km)
          {
             kmGrMax = true;
             //Tarif geht bis max 200 km ab dann alle 5 km + Frachtsatz
             kmGNT();
             GetMaxKmFrachtrate();
             GetAufPreisFrachtrate();
             Frachtberechnung();
          }
          else
          {
            kmGNT();
            GetFrachtrate();
            Frachtberechnung();
          }
          break;

        case "GFT":
          if (Maxkm < km)
          {

          }
          else
          { 
            kmGFT();
            GetFrachtrate();
            Frachtberechnung();          
          }

          break;

        case "GNTalt":
          if (Maxkm < km)
          {
            kmGrMax = true;
            //Tarif geht bis max 200 km ab dann alle 5 km + Frachtsatz
            kmGNT();
            GetMaxKmFrachtrate();
            GetAufPreisFrachtrate();
            Frachtberechnung();
          }
          else
          {
            kmGNT();
            GetFrachtrate();
            Frachtberechnung();
          }
          break;

        case "Kundentarif":

          break;
      }
    }
    //
    //
    //
    private Int32 SetColToMin(Int32 _Suchgewicht)
    {
        Int32 SGewicht = _Suchgewicht;
        switch (TarifArt)
        {
            case "GNT":
                if (SGewicht <= 5)
                {
                    SGewicht = 5;
                    fpflGewicht = 5000;
                }
                if (SGewicht >= 29)
                {
                    SGewicht = 29;
                }
                break;

            case "GFT":
                if (SGewicht <= 5)
                {
                    SGewicht = 5;
                    fpflGewicht = 5000;
                }
                if (SGewicht >= 26)
                {
                    SGewicht = 26;
                }
                break;

            case "GNTalt":
                if (SGewicht <= 5)
                {
                    SGewicht = 5;
                    fpflGewicht = 5000;
                }
                if (SGewicht >= 29)
                {
                    SGewicht = 29;
                }
                break;

            case "Kundentarif":

                break;
        }
        return SGewicht;
    }
    //
    //
    //
    private void GetFrachtrate()
    {
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;


      Command.CommandText = GetSQL();

      Globals.SQLcon.Open();
      object obj = Command.ExecuteScalar();

      if (obj == null)
      {
         PreisTo= 0.00m;
      }
      else
      {
        PreisTo =(decimal)obj;
      }
      Command.Dispose();
      Globals.SQLcon.Close();
    }
    //
    //----------- Frachtrate für die Maxmimale Entfernung ------------------
    //
    private void GetMaxKmFrachtrate()
    {
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;
        Command.CommandText = GetSQLMaxKM();

        Globals.SQLcon.Open();
        object obj = Command.ExecuteScalar();

        if (obj == null)
        {
            PreisTo = 0.00m;
        }
        else
        {
            PreisTo = (decimal)obj;
        }
        Command.Dispose();
        Globals.SQLcon.Close();
    }
    //
    //
    //
    private string GetSQL()
    {
      string sql = string.Empty;
      switch (TarifArt)
      {
        case "GNT":
              sql = "SELECT [" + Col + "] From GNT WHERE km= '" + km + "'";
              break;

        case "GFT":
              sql = "SELECT [" + Col + "] From GFT WHERE km= '" + km + "'";
              break;

        case "GNTalt":
              sql = "SELECT [" + Col + "] From GNTalt WHERE km= '" + km + "'";
              break;

        case "Kundentarif":

          break;
      }
      return sql;
    }
    //
    //-------------------  SQL Max Km -----------------------
    //
    private string GetSQLMaxKM()
    {
        string sql = string.Empty;
        switch (TarifArt)
        {
            case "GNT":
                    sql = "SELECT [" + Col + "] From GNT WHERE km='"+Maxkm+"'";
                break;

            case "GNTalt":
                sql = "SELECT [" + Col + "] From GFTalt WHERE km='" + Maxkm + "'";
                break;
        }
        return sql;
    }
    //
    //------------------ sql zusätzl km ---------------------
    //
    private string GetSQLZusatzKM()
    {
        string sql = string.Empty;
        switch (TarifArt)
        {
            case "GNT":
                sql = "SELECT [" + Col + "] From GNT WHERE km='" + zuKm*1000 + "'";
                break;

            case "GNTalt":
                sql = "SELECT [" + Col + "] From GFTalt WHERE km='" + zuKm*1000 + "'";
                break;
        }
        return sql;
    }
    //
    //--------------- Frachtrate Aufpreis -------------------
    //
    private void GetAufPreisFrachtrate()
    {
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = GetSQLZusatzKM();

      Globals.SQLcon.Open();
      object obj = Command.ExecuteScalar();

      if (obj == null)
      {
        aufPreisTo = 0.00m;
      }
      else
      {
        aufPreisTo = (decimal)obj;
      }
      Command.Dispose();
      Globals.SQLcon.Close();
    }
    //
    //
    //
    private void GetMaxKmTable()
    {
      try
      {
      
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT TOP(2)km From "+TarifArt+" ORDER BY km DESC"; 

      Globals.SQLcon.Open();
      ada.Fill(dtMaxKm);
      ada.Dispose();
      Command.Dispose();
      Globals.SQLcon.Close();
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }
    }
    //
    //----------- setzt max Km und zuschlagkm ------------------
    //
    private void SetMaxKmAndZugschlag()
    {
      Maxkm = (Int32)dtMaxKm.Rows[1]["km"];
      zuKm = (Int32)dtMaxKm.Rows[0]["km"];
      zuKm = zuKm / 1000;                       //Zeile: je weitere X km - X wurd in DB mit 1000 multipliziert
    }
    //
    //--------- da die km gestaffelt sind, muss hier der richtige km Wert errechnet werden ---------------
    //
    private void kmGNT()
    {
      //siehe DB GNT
      if(km<=29)
      {
        Int32 mod = km%2;
        if(mod!=0)
        {
          km=km+(2-mod);
          kmGNT();
        }
      }
      if((km>=30) & (km<99))
      {
        Int32 mod = km%5;
        if(mod!=0)
        {
          km=km+(5-mod);
          kmGNT();
        }
      }
      if((km>=100) & (km<=200))
      {
        Int32 mod = km%10;
        if(mod!=0)
        {
          km=km+(10-mod);
          kmGNT();
        }
      }
    }
    //
    //--------- km Berechnung nach GFT ----------------
    //
    private void kmGFT()
    {
      //siehe DB GNT
      if (km <= 99)
      {
        Int32 mod = km % 5;
        if (mod != 0)
        {
          km = km + (5-mod);
          kmGFT();
        }
      }
      if ((km >= 100) & (km < 299))
      {
        Int32 mod = km % 10;
        if (mod != 0)
        {
          km = km + (10-mod);
          kmGFT();
        }
      }
      if ((km >= 300) & (km < 799))
      {
        Int32 mod = km % 20;
        if (mod != 0)
        {
          km = km +(20- mod);
          kmGFT();
        }
      }
      if ((km >= 800) & (km <= 900))
      {
        Int32 mod = km % 50;
        if (mod != 0)
        {
          km = km + (50-mod);
          kmGFT();
        }
      }
    }
    //
    //----------- berechnung der Fracht ---------------
    //
    private void Frachtberechnung()
    {
      //public static decimal RoundStep( this decimal d, decimal step ) {
        //      return Math.Round(d / step + step) * step;
      if (kmGrMax)
      {
          Int32 moreKm = km - Maxkm;
          Int32 mod = moreKm % zuKm;
          

          if (mod == 0)
          {
              PreisTo = PreisTo + ((moreKm / zuKm) * aufPreisTo);
          }
          else
          {
              moreKm = moreKm + (zuKm - mod);
              PreisTo = PreisTo + ((moreKm / zuKm) * aufPreisTo);
          }
             
      }

      //decimal gewicht = Math.Round((fpflGewicht / 1000), 1, MidpointRounding.AwayFromZero);
      // Gewicht wird aufgerundet auf volle hundert kg
      decimal gewicht = Math.Ceiling((fpflGewicht / 100));
      gewicht = gewicht / 10;

      PreisTo = Math.Round(PreisTo, 2, MidpointRounding.AwayFromZero);
      Fracht = gewicht * PreisTo;
      Fracht = Math.Round(Fracht, 2, MidpointRounding.AwayFromZero);

      if (MargeEuro > 0.00m)
      {
          Fracht = Fracht - MargeEuro;
          Fracht = Math.Round(Fracht, 2, MidpointRounding.AwayFromZero);
      }
      else
      {
          if (MargeProzent > 0.0m)
          {
              Fracht = Fracht - (Fracht * (MargeProzent / 100));
              Fracht = Math.Round(Fracht, 2, MidpointRounding.AwayFromZero);
          }
      }
    }
  }
}
