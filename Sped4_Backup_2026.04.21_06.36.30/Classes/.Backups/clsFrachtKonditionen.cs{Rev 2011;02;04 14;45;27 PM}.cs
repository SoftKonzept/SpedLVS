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
  class clsFrachtKonditionen
  {

    private Int32 _ID;
    private Int32 _KD_ID;
    private Int32 _EP;
    //private Int32 _E_ADR;
    //private string _V_PLZ;
    //private string _E_PLZ;
    private Int32 _km;
    private Int32 _Gewicht;
    private Int32 _Gut_ID;
    private double _PreisTonne;
    private double _PreisPal;
    private double _PreisKm;
    private bool _GNT;
    private bool _GNT200;
    private bool _GNTalt;
    private bool _GFT;





    public Int32 ID
    {
      get { return _ID; }
      set { _ID = value; }
    }
    public Int32 KD_ID
    {
      get { return _KD_ID; }
      set { _KD_ID = value; }
    }

    public Int32 EP
    {
      get { return _EP; }
      set { _EP = value; }
    }
    public bool GNT
    {
      get { return _GNT; }
      set { _GNT = value; }
    }
    public bool GNTalt
    {
      get { return _GNTalt; }
      set { _GNTalt = value; }
    }
    public bool GNT200
    {
      get { return _GNT200; }
      set { _GNT200 = value; }
    }
    public bool GFT
    {
      get { return _GFT; }
      set { _GFT = value; }
    }
    public Int32 km
    {
      get { return _km; }
      set { _km = value; }
    }
    public Int32 Gewicht
    {
      get { return _Gewicht; }
      set { _Gewicht = value; }
    }
    public double PreisTonne
    {
      get { return _PreisTonne; }
      set { _PreisTonne = value; }
    }
    public double PreisPal
    {
      get { return _PreisPal; }
      set { _PreisPal = value; }
    }
    public double PreisKm
    {
      get { return _PreisKm; }
      set { _PreisKm = value; }
    }

    /*********************************************************************************
     * Konditionsart:
     *  1: €/to und km
     *  2: €/EP 
     *  3: €/km
     *  ******************************************************************************/

    //*******************************************************************************************
    //
    //
    public void InsertKDKonditionen(Int32 Konditionsart, DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                ID = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                Gewicht = Convert.ToInt32(dt.Rows[i]["to"].ToString());
                PreisTonne = Convert.ToDouble(dt.Rows[i]["€/To"]);
                EP = Convert.ToInt32(dt.Rows[i]["EP"].ToString());
                PreisPal = Convert.ToDouble(dt.Rows[i]["€/EP"]);
                km = Convert.ToInt32(dt.Rows[i]["km"].ToString());
                PreisKm = Convert.ToDouble(dt.Rows[i]["€/km"]);

                //update
                if (ID>0)
                {
                    
                    UpdateKonditionenByID(Konditionsart);
                }
                else //Insert
                {
                    AddKonditionen(Konditionsart);
                }
            }
        }
    }
    //
    //--- €/to und km
    public void AddKonditionen(Int32 Konditionsart)
    {
      try
      {
          string sql = string.Empty;
        //--- initialisierung des sqlcommand---
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;

        sql = "INSERT INTO Frachtkonditionen (KD_ID, Gewicht, PreisTo, EP, PreisPal, km, PreisKm, Pto, PEP, Pkm) " +
                                                    "VALUES ('" + KD_ID + "','"
                                                    + Gewicht + "','"
                                                    + PreisTonne.ToString().Replace(",", ".") + "','"
                                                    + EP + "','"
                                                    + PreisPal.ToString().Replace(",", ".") + "','"
                                                    + km + "','"
                                                    + PreisKm.ToString().Replace(",",".") + "', ";                                                  
        switch (Konditionsart)
        {
            case 1:
                //€/to 
                sql = sql+ "'1', '0', '0')";
                break;
            case 2:
                //€/EP 
                sql = sql+ "'0', '1', '0')";
                break;
            case 3:
                //€/to 
                sql = sql+ "'0', '0', '1')";
                break;
        }

        //----- SQL Abfrage -----------------------
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
    }
    //--------------- Update  ------------------------
    //
    private void UpdateKonditionenByID(Int32 Konditionsart)
    {
        try
        {
            string sql = string.Empty;
            //--- initialisierung des sqlcommand---
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;

            switch (Konditionsart)
            {
                case 1:
                    //€/to 
                sql = "Update Frachtkonditionen SET Gewicht='" + Gewicht + "', PreisTo='" + PreisTonne.ToString().Replace(",", ".") + "', km='" + km + "' WHERE ID='" + ID + "'";
                    break;
                case 2:
                    //€/EP 
                    sql = "Update Frachtkonditionen SET EP='" + EP + "', PreisPal='" + PreisPal.ToString().Replace(",", ".") + "', km='" + km + "' WHERE ID='" + ID + "'";
                    break;
                case 3:
                    //€/to 
                    sql = "Update Frachtkonditionen SET PreisKm='" + PreisKm.ToString().Replace(",", ".") + "', km='" + km + "' WHERE ID='" + ID + "'";
                    break;

            }
            //----- SQL Abfrage -----------------------
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
    }
    //
    //--------------------- Konditionenart für den Kunden vorhanden ----------------------------
    //
    public bool IsKDKonditionIn(Int32 Konditionsart)
    {
        bool IsUsed = false;
        string sql = string.Empty;

        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;

        switch (Konditionsart)
        {
            case 1:
                //€/to 
                sql="SELECT ID FROM Frachtkonditionen WHERE KD_ID='"+ KD_ID+"' AND Pto='1'";
                break;
            case 2:
                //€/EP 
                sql = "SELECT ID FROM Frachtkonditionen WHERE KD_ID='" + KD_ID + "' AND PEP='1'";
                break;
            case 3:
                //€/to 
                sql = "SELECT ID FROM Frachtkonditionen WHERE KD_ID='" + KD_ID + "' AND Pkm='1'";
                break;
        }
        Command.CommandText = sql;

        Globals.SQLcon.Open();
        if (Command.ExecuteScalar() == null)
        {
            IsUsed = false;
        }
        else
        {
            IsUsed = true;
            Int32 ID = (Int32)Command.ExecuteScalar();
        }
        Command.Dispose();
        Globals.SQLcon.Close();
        return IsUsed;
    }
    //
    //----------------- Get KD Konditionen nach Konditionsart -------------
    //
    public DataTable LoadKDKonditionenByKonditionsart(Int32 Konditionsart)
    {
        string sql = string.Empty;
        DataTable dt = new DataTable();
        dt.Clear();
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        sql = "SELECT ID, Gewicht as 'to', EP, km, PreisTo as '€/To', PreisPal as '€/EP', PreisKm as '€/km' FROM Frachtkonditionen WHERE KD_ID='" + KD_ID + "' ";
        switch (Konditionsart)
        {
            case 1:
                //€/to 
                sql=sql+"AND Pto='1'";
                break;
            case 2:
                //€/EP 
                sql = sql+"AND PEP='1'";
                break;
            case 3:
                //€/to 
                sql = sql + "AND Pkm='1'";
                break;
        }
    
        ada.SelectCommand = Command;
        Command.CommandText = sql;

        ada.Fill(dt);
        Command.Dispose();
        Globals.SQLcon.Close();

        if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
            Command.Connection.Close();
        return dt;
    }
    //
    //---------- löschen von Konditionen -------------------
    //
    public void DeleteKDKonditionen()
    {
      //--- initialisierung des sqlcommand---
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      Command.CommandText = "DELETE FROM Frachtkonditionen WHERE ID='" + ID + "'";
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
    //--------------- Kondition für Kunde hinterlegt ----------------------------
    //
    public static bool IsKDhinterlegt(Int32 KD_ID)
    {
      bool IsIn = false;
      string sql = string.Empty;

      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      sql = "SELECT ID FROM Frachtkonditionen WHERE KD_ID='" + KD_ID + "'";
      Command.CommandText = sql;

      Globals.SQLcon.Open();
      object obj = Command.ExecuteScalar();
      if (obj == null)
      {
        IsIn = false;
      }
      else
      {
        IsIn = true;
        Int32 ID = (Int32)obj;
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      return IsIn;
    }






    /****************************************************************************************************
     *                            DB Tarife
     * 
     * **************************************************************************************************/
    //
    //---------- hinterlegte Tarife auslesen ------------
    //
    public void LoadTarife(ref frmADRPanelKonditionsErfassung Kondition)
    {
      DataTable dt = new DataTable();
      dt.Clear();
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();

      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT " +
                                      "GNT, GNTalt, GNT200, GFT " +
                                      "FROM Tarife " +
                                      "WHERE KD_ID='" + KD_ID + "'";

      ada.Fill(dt);
      ada.Dispose();
      Command.Dispose();
      Globals.SQLcon.Close();

      if (dt.Rows.Count == 0)
      {
        Kondition.TarifeHinterlegt = false;
      }
      else
      {
        Kondition.TarifeHinterlegt = true;
        
        for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
        {
          if (Convert.ToInt32(dt.Rows[i]["GNT"]) == 0)
          {
            Kondition.GNT = false;
          }
          else
          {
            Kondition.GNT = true;
          }
          if (Convert.ToInt32(dt.Rows[i]["GNTalt"]) == 0)
          {
            Kondition.GNTalt = false;
          }
          else
          {
            Kondition.GNTalt = true;
          }
          if (Convert.ToInt32(dt.Rows[i]["GNT200"]) == 0)
          {
            Kondition.GNT200 = false;
          }
          else
          {
            Kondition.GNT200 = true;
          }
          if (Convert.ToInt32(dt.Rows[i]["GFT"]) == 0)
          {
            Kondition.GFT = false;
          }
          else
          {
            Kondition.GFT = true;
          }
        }
      }
    }
    //
    //
    //
    public void AddTarife()
    {
      try
      {
        //--- initialisierung des sqlcommand---
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;

        //SQLString
        string sql = string.Empty;

        sql= "INSERT INTO Tarife (KD_ID, GNT, GNTalt, GNT200, GFT) " +
                                        "VALUES ('" + KD_ID + "', ";

        if(GNT)
        {
          sql = sql + "'1', ";
        }
        else
        {
          sql = sql + "'0', ";        
        }
        if(GNTalt)
        {
          sql = sql + "'1', ";
        }
        else
        {
          sql = sql + "'0', ";        
        }
        if(GNT200)
        {
          sql = sql + "'1', ";
        }
        else
        {
          sql = sql + "'0', ";        
        }
        if(GFT)
        {
          sql = sql + "'1')";
        }
        else
        {
          sql = sql + "'0')";
        }
        //----- SQL Abfrage -----------------------
        if (sql != "")
        {
          Command.CommandText = sql;


          Globals.SQLcon.Open();
          Command.ExecuteNonQuery();
          Command.Dispose();
          Globals.SQLcon.Close();
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
    public void UpdateTarife()
    {
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      string sql = string.Empty;

      //SQL String 
      sql = "Update Tarife SET ";

      if (GNT)
      {
        sql = sql + " GNT='1', ";
      }
      else
      {
        sql = sql + " GNT='0', ";
      }
      if (GNTalt)
      {
        sql = sql + " GNTalt='1', ";
      }
      else
      {
        sql = sql + " GNTalt='0', ";
      }
      if (GNT200)
      {
        sql = sql + " GNT200='1', ";
      }
      else
      {
        sql = sql + " GNT200='0', ";
      }
      if (GFT)
      {
        sql = sql + " GFT='1' ";
      }
      else
      {
        sql = sql + " GFT='0' ";
      }

      sql=sql + "WHERE KD_ID='" + KD_ID + "'";

      if (sql != "")
      {
        Command.CommandText = sql;
        Globals.SQLcon.Open();
        Command.ExecuteNonQuery();
        Command.Dispose();
        Globals.SQLcon.Close();
      }
    }
    //
    //------------- KD bereit in DB Vorhanden ------------- ?????
    //
    public bool KD_Tarif()
    {
      bool IsUsed = false;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT ID From Tarife WHERE KD_ID ='" + KD_ID + "'";

      Globals.SQLcon.Open();
      if (Command.ExecuteScalar() == null)
      {
        IsUsed = false;
      }
      else
      {
        IsUsed = true;
        //Int32 ID = (Int32)Command.ExecuteScalar();
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      return IsUsed;
    }
  }
}
