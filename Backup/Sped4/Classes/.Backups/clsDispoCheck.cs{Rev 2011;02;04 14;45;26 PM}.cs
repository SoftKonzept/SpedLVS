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
  class clsDispoCheck
  {
    private Int32 _ID;
    private Int32 _AuftragID;
    private Int32 _AuftragPos;
    private Int32 _ZM_ID;   //Zugmaschine
    private Int32 _A_ID;    //Auflieger
    private Int32 _P_ID;    //Personal
    private Int32 _KommiID;
    private decimal _maxGewicht;//maxLadungsgewicht=maxGesamtgewicht-Leergewicht ZM - Leergewicht Auflieger
    private decimal _TourGewicht;
    private decimal _LGZM;      //Leergewicht ZM
    private decimal _LGA;       // Leergewicht Auftlieger
    private decimal _zlGG;      // max Gesamtgewicht
    private bool _disponieren;
    private bool _GewichtZuHoch;
    private bool _GewichtFreigabe;
    private decimal _AuftragPosGewicht;
    private decimal _gemGewicht;
    private decimal _tatGewicht;
    private DateTime _oldBeladezeit;
    private DateTime _oldEntladezeit;
    private Int32 _oldZM;
    private Int32 _KommiAnzahlTour;
    private bool _init;
    private Int32 _iInit;
    private Int32 _iBackToOldZM;
    private Int32 _iGewichtFreigabe;
    private bool _bo_BackToOldZM;
    private bool _bo_RessourcenCheckOK;

    public Int32 ID
    {
      get { return _ID; }
      set { _ID = value; }
    }
    public bool bo_RessourcenCheckOK
    {
      get { return _bo_RessourcenCheckOK; }
      set { _bo_RessourcenCheckOK = value; }
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
    public Int32 ZM_ID
    {
      get { return _ZM_ID; }
      set { _ZM_ID = value; }
    }
    public Int32 A_ID
    {
      get { return _A_ID; }
      set { _A_ID = value; }
    }
    public Int32 KommiID
    {
      get { return _KommiID; }
      set { _KommiID = value; }
    }
    public Int32 P_ID
    {
      get { return _P_ID; }
      set { _P_ID = value; }
    }
    public decimal maxGewicht
    {
      get { return _maxGewicht; }
      set { _maxGewicht = value; }
    }
    public decimal TourGewicht
    {
      get { return _TourGewicht; }
      set { _TourGewicht = value; }
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
    public decimal LGZM
    {
      get { return _LGZM; }
      set { _LGZM = value; }
    }
    public decimal LGA
    {
      get { return _LGA; }
      set { _LGA = value; }
    }
    public decimal zlGG
    {
      get { return _zlGG; }
      set { _zlGG = value; }
    }
    public bool disponieren
    {
      get { return _disponieren; }
      set { _disponieren = value; }
    }
    public bool GewichtZuHoch
    {
      get { return _GewichtZuHoch; }
      set { _GewichtZuHoch = value; }
    }
    public bool GewichtFreigabe
    {
      get { return _GewichtFreigabe; }
      set { _GewichtFreigabe = value; }
    }
    public decimal AuftragPosGewicht
    {
      get{ return _AuftragPosGewicht; }
      set { _AuftragPosGewicht = value; }
    }
    public DateTime oldBeladezeit
    {
      get 
      {

        if (_oldBeladezeit < DateTime.MinValue)
        {
          _oldBeladezeit = DateTime.MinValue;
        }
        return _oldBeladezeit; }
      set { _oldBeladezeit = value; }
    }
    public DateTime oldEntladezeit
    {
      get { return _oldEntladezeit; }
      set { _oldEntladezeit = value; }
    }
    public Int32 KommiAnzahlTour
    {
      get { return _KommiAnzahlTour; }
      set { _KommiAnzahlTour = value; }
    }
    public Int32 oldZM
    {
      get { return _oldZM; }
      set { _oldZM = value; }
    }
    public bool init
    {
      get { return _init; }
      set { _init = value; }
    }

    public Int32 iBackToOldZM
    {
        get 
        {
          _iBackToOldZM = 0;
          if (bo_BackToOldZM == true)
          {
            _iBackToOldZM = 1;
          }
        
          return _iBackToOldZM; 
        }
        set { _iBackToOldZM = value; }
    }
    public Int32 iGewichtFreigabe
    {
      get
      {
        _iGewichtFreigabe = 0;
        if (GewichtFreigabe == true)
        {
          _iGewichtFreigabe = 1;
        }

        return _iGewichtFreigabe;
      }
      set { _iGewichtFreigabe = value; }
    }
    public Int32 iInit
    {
      get
      {
        _iInit = 0;
        if (init == true)
        {
          _iInit = 1;
        }

        return _iInit;
      }
      set { _iInit = value; }
    }
    public bool bo_BackToOldZM
    {
        get { return _bo_BackToOldZM; }
        set { _bo_BackToOldZM = value; }
    }
    //
    //******************************************************************************
    //
    public Sped4.Controls.AFKalenderItemKommi FillData(Sped4.Controls.AFKalenderItemKommi KommiCtr, bool bo_Init)
    {
      KommiCtr.DispoCheck.bo_RessourcenCheckOK = true;
      if (!bo_Init)
      {
        //breits disponierte Kommissionenn
        LoadDispoCheckDaten(ref KommiCtr);
        
        if (KommiCtr.DispoCheck.oldZM != KommiCtr.Kommission.KFZ_ZM)
        {
          KommiCtr.DispoCheck.bo_BackToOldZM = true;
          GetAndCheckRecourcen(ref KommiCtr);
          if (KommiCtr.DispoCheck.disponieren)
          {
            DispoGewichtsCheck(ref KommiCtr);
          }
        }
        else
        {
          if (KommiCtr.DispoCheck.GewichtFreigabe)
          {
            KommiCtr.DispoCheck.disponieren = true;
          }
          else
          {
            KommiCtr.DispoCheck.disponieren = true;
            DispoGewichtsCheck(ref KommiCtr);
          }
        }
        SetInterneVariablen(ref KommiCtr);
      }
      else
      {
        SetDataForDispoCheck(ref KommiCtr);
        KommiCtr.DispoCheck.init = bo_Init;

        GetAndCheckRecourcen(ref KommiCtr);
        if (KommiCtr.DispoCheck.disponieren)
        {
          DispoGewichtsCheck(ref KommiCtr);
          KommiCtr.DispoCheck.bo_BackToOldZM = false;
        }
        KommiCtr.DispoCheck.init = false;
      }

      //löschen wenn disponieren = false
      if ((!KommiCtr.DispoCheck.disponieren) & (!KommiCtr.DispoCheck.bo_BackToOldZM))
      {
        DeleteDispoCheck();
      }
      else
      {
        SetInterneVariablen(ref KommiCtr);
        UpdateDispoCheckbyID();
      }
      return KommiCtr;
    }
    //
    //
    //
    public void SetDataForDispoCheck(ref Sped4.Controls.AFKalenderItemKommi KommiCtr)
    {
      Int32 iKommiID = 0;
      Int32 iDCID = 0;

      KommiCtr.DispoCheck.AuftragID = KommiCtr.Kommission.AuftragID;
      KommiCtr.DispoCheck.AuftragPos = KommiCtr.Kommission.AuftragPos;
      KommiCtr.DispoCheck.oldZM = KommiCtr.Kommission.KFZ_ZM;
      KommiCtr.DispoCheck.oldBeladezeit = KommiCtr.Kommission.BeladeZeit;
      KommiCtr.DispoCheck.oldEntladezeit = KommiCtr.Kommission.EntladeZeit;
      KommiCtr.DispoCheck._bo_BackToOldZM = false;

      if (KommiCtr.DispoCheck.KommiID == 0)
      {
        clsKommission kom = new clsKommission();
        kom.AuftragID = KommiCtr.Kommission.AuftragID;
        kom.AuftragPos = KommiCtr.Kommission.AuftragPos;
        kom.AuftragPos_ID = KommiCtr.Kommission.AuftragPos_ID;
        iKommiID = kom.GetIDfromKommission();
      }
      else
      {
        iKommiID = KommiCtr.DispoCheck.KommiID;
      }

      if (KommiCtr.DispoCheck.ID == 0)
      {
        iDCID = KommiCtr.DispoCheck.GetID();
      }
      else
      {
        iDCID = KommiCtr.DispoCheck.ID;
      }

      KommiCtr.DispoCheck.ID = iDCID;
      KommiCtr.DispoCheck.KommiID = iKommiID;
      KommiCtr.Kommission.ID = iKommiID;

      SetInterneVariablen(ref KommiCtr);


      if (IsKommiIn())
      {
        UpdateDispoCheckbyID();
      }
      else
      {
        KommiCtr.DispoCheck.Add();
      }
    }
    //
    //
    //
    private void SetInterneVariablen(ref Sped4.Controls.AFKalenderItemKommi KommiCtr)
    {
      AuftragID = KommiCtr.Kommission.AuftragID;
      AuftragPos = KommiCtr.Kommission.AuftragPos;

      if (KommiCtr.Kommission.oldZM == KommiCtr.Kommission.KFZ_ZM)
      {
        oldZM = KommiCtr.Kommission.oldZM;
      }
      else
      {
        if (KommiCtr.DispoCheck.disponieren)
        {
          oldZM = KommiCtr.Kommission.KFZ_ZM;
        }
        else
        {
          if (KommiCtr.DispoCheck.bo_RessourcenCheckOK)
          {
            oldZM = KommiCtr.Kommission.KFZ_ZM;
          }
          else
          {
            // keine Resscourcen vorhanden, dann muss die Kommission zurück zum alten Fahrzeug
            oldZM = KommiCtr.DispoCheck.oldZM;
          }
          //oldZM = KommiCtr.Kommission.oldZM;
          //oldZM = KommiCtr.Kommission.KFZ_ZM;
        }
      }

      oldBeladezeit = KommiCtr.DispoCheck.oldBeladezeit;
      oldEntladezeit = KommiCtr.DispoCheck.oldEntladezeit;
      KommiID = KommiCtr.Kommission.ID;
      bo_BackToOldZM = KommiCtr.DispoCheck.bo_BackToOldZM;
      disponieren = KommiCtr.DispoCheck.disponieren;
      GewichtFreigabe = KommiCtr.DispoCheck.GewichtFreigabe; 
      if (KommiCtr.DispoCheck.ID == 0)
      {
        KommiCtr.DispoCheck.ID = GetID();
      }
      ID = KommiCtr.DispoCheck.ID;
    }
    //
    //
    //
    private void LoadDispoCheckDaten(ref Sped4.Controls.AFKalenderItemKommi KommiCtr)
    {
      DataTable dt = new DataTable();
      dt.Clear();
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();

      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;

      if (KommiCtr.DispoCheck.ID > 0)
      {
        Command.CommandText = "SELECT " +
                                 "* " +
                                 "FROM DispoCheck WHERE ID='" + KommiCtr.DispoCheck.ID + "'";
      }
      else
      {
        if (KommiCtr.DispoCheck.KommiID > 0)
        {
          Command.CommandText = "SELECT " +
                                  "* " +
                                  "FROM DispoCheck WHERE KommiID='" + KommiCtr.DispoCheck.KommiID + "'";
        }
      }
      ada.Fill(dt);
      ada.Dispose();
      Command.Dispose();
      Globals.SQLcon.Close();

      if (dt.Rows.Count == 0)
      {
          init = true;
      }
      else
      {
          for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
          {
              KommiCtr.DispoCheck.oldZM = (Int32)dt.Rows[i]["oldZM"];
              KommiCtr.DispoCheck.oldBeladezeit = (DateTime)dt.Rows[i]["oldBeladezeit"];
              KommiCtr.DispoCheck.oldEntladezeit = (DateTime)dt.Rows[i]["oldEntladezeit"];

              if (Convert.ToInt32(dt.Rows[i]["OKGewicht"]) == 0)
              {
                  KommiCtr.DispoCheck.GewichtFreigabe = false;
              }
              else
              {
                  KommiCtr.DispoCheck.GewichtFreigabe = true;
              }
              KommiCtr.DispoCheck.bo_BackToOldZM = (bool)dt.Rows[i]["BackToOldZM"];
              KommiCtr.DispoCheck.init = (bool)dt.Rows[i]["NeuDisponiert"];
          }
      }
    }
    //
    //-- DB insert --------
    private void Add()
    {
      try
      {
        //--- initialisierung des sqlcommand---
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;

        //----- SQL Abfrage -----------------------
        Command.CommandText = ("INSERT INTO DispoCheck (AuftragID, AuftragPos, oldZM, oldBeladezeit, oldEntladezeit, KommiID, BackToOldZM, NeuDisponiert) " +
                                        "VALUES ('" + AuftragID + "', '" 
                                                    + AuftragPos + "', '"                                                 
                                                    + oldZM + "','"
                                                    + oldBeladezeit + "','"
                                                    + oldEntladezeit + "','"
                                                    + KommiID + "','"
                                                    + iBackToOldZM + "','"
                                                    + iInit + "')");

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
    //
    //
    public void UpdateDispoCheckbyID()
    {
       if(ID>0)
       {
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          Command.CommandText = "Update DispoCheck SET AuftragID ='"+ AuftragID + "', "+
                                                      "AuftragPos='"+AuftragPos +"', " +
                                                      "oldZM='" + oldZM + "', " +
                                                      "oldBeladezeit ='"+oldBeladezeit+"', " +
                                                      "oldEntladezeit='"+oldEntladezeit+"', " +
                                                      "OKGewicht='" + iGewichtFreigabe + "', " +
                                                      "KommiID='"+KommiID+"', " +
                                                      "BackToOldZM='" + iBackToOldZM + "', " +
                                                      "NeuDisponiert='" + iInit + "' " +
                                                      "WHERE ID='" + ID + "'";

          Globals.SQLcon.Open();
          Command.ExecuteNonQuery();
          Command.Dispose();
          Globals.SQLcon.Close();
       }
    }
    //
    //---------- Kommi ID bereits vorhanden? ---- 
    //
    private bool IsKommiIn()
    {
      bool IsUsed = false;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT ID From DispoCheck WHERE AuftragID ='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "'";

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
    public Int32 GetID()
    {
      Int32 Check_ID = 0;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT ID From DispoCheck WHERE AuftragID ='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "'";

      Globals.SQLcon.Open();
      if (Command.ExecuteScalar() == null)
      {
        Check_ID = 0;
      }
      else
      {
        Check_ID  = (Int32)Command.ExecuteScalar();
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      return Check_ID;
    }
    //
    //----------- Delete / Löschen der Recource vom TimePanel  -------------------
    //
    public void DeleteDispoCheck()
    {
        //--- initialisierung des sqlcommand---
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        Command.CommandText = "DELETE FROM DispoCheck WHERE AuftragID='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "'";
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
    //----------- OK - disposition erhöhtes Gewicht  -------------------
    //
    public void SetOKGewichtTrue()
    {
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      Command.CommandText = "Update DispoCheck SET OKGewicht='1' WHERE ID='" + ID + "'";

      Globals.SQLcon.Open();
      Command.ExecuteNonQuery();
      Command.Dispose();
      Globals.SQLcon.Close();
    }
    public void SetOKGewichtFalse()
    {
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      Command.CommandText = "Update DispoCheck SET OKGewicht='0' WHERE ID='" + ID + "'";

      Globals.SQLcon.Open();
      Command.ExecuteNonQuery();
      Command.Dispose();
      Globals.SQLcon.Close();
    }
    //
      //
      //
    public void SetDispoCheckInit()
    {
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      if (init)
      {
        Command.CommandText = "Update DispoCheck SET NeuDisponiert='1' WHERE ID='" + ID + "'";
      }
      else
      {
        Command.CommandText = "Update DispoCheck SET NeuDisponiert='0' WHERE ID='" + ID + "'";
      }
      Globals.SQLcon.Open();
      Command.ExecuteNonQuery();
      Command.Dispose();
      Globals.SQLcon.Close();
    }
    //
    //
    //
    public void SetBackToOldZM(bool Back)
    {
        Int32 iBit = 0;
        if (Back)
        {
            iBit = 1;
        }
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        Command.CommandText = "Update DispoCheck SET BackToOldZM='"+iBit+"' WHERE KommiID='" + KommiID + "'";

        Globals.SQLcon.Open();
        Command.ExecuteNonQuery();
        Command.Dispose();
        Globals.SQLcon.Close();
    }
    //--------Beim Refresh des Dispoplans werden alle Daten noch einmal upgedatet-----------
    //------ dadurch wir nur noch die Abfrage bei erhöhtem Gewicht nur noch einmal ---------
    //
    public Sped4.Controls.AFKalenderItemKommi UpdateForRefresh(Sped4.Controls.AFKalenderItemKommi KommiCtr)
    {
        KommiCtr.DispoCheck.AuftragID = KommiCtr.Kommission.AuftragID;
        KommiCtr.DispoCheck.AuftragPos = KommiCtr.Kommission.AuftragPos;
        KommiCtr.DispoCheck.oldZM = KommiCtr.Kommission.KFZ_ZM;
        KommiCtr.DispoCheck.oldBeladezeit = KommiCtr.Kommission.BeladeZeit;
        KommiCtr.DispoCheck.oldEntladezeit = KommiCtr.Kommission.EntladeZeit;
        KommiCtr.DispoCheck.KommiID = KommiCtr.Kommission.ID;
        KommiCtr.DispoCheck.ID=GetID();
        KommiCtr.DispoCheck.bo_BackToOldZM = KommiCtr.DispoCheck.bo_BackToOldZM;
        UpdateDispoCheckbyID();

        if (KommiCtr.DispoCheck.GewichtFreigabe)
        {
            SetOKGewichtTrue();
        }
        else
        {
            SetOKGewichtFalse();
        }
        return KommiCtr;
    }

    /*******************************************************************************************************
     *                        Get Daten - Check - Auswertung 
     ******************************************************************************************************/
    //
    //--------- Recourcen -------
    private void GetAndCheckRecourcen(ref Sped4.Controls.AFKalenderItemKommi KommiCtr)
    {
      DataSet ds1 = clsResource.GetUsedTruckRecource(KommiCtr.Kommission.BeladeZeit, KommiCtr.Kommission.EntladeZeit, KommiCtr.Kommission.KFZ_ZM);
      if (ds1.Tables[0].Rows.Count > 0)
      {
        for (Int32 i = 0; i < ds1.Tables[0].Rows.Count; i++)
        {
          string typ = ds1.Tables[0].Rows[i]["RecourceTyp"].ToString();
          if (typ == "A")
          {
            if (ds1.Tables[0].Rows[i]["VehicleID"] != DBNull.Value)
            {
              KommiCtr.DispoCheck.A_ID = (Int32)ds1.Tables[0].Rows[i]["VehicleID"];
            }
            if (ds1.Tables[0].Rows[i]["LG_A"] != DBNull.Value)
            {
              KommiCtr.DispoCheck.LGA = Convert.ToDecimal(ds1.Tables[0].Rows[i]["LG_A"]);
            }
            if (ds1.Tables[0].Rows[i]["zlGG"] != DBNull.Value)
            {
              KommiCtr.DispoCheck.zlGG = Convert.ToDecimal(ds1.Tables[0].Rows[i]["zlGG"]);
            }
          }
          if (typ == "Z")
          {
            if (ds1.Tables[0].Rows[i]["VehicleID"] != DBNull.Value)
            {
              KommiCtr.DispoCheck.ZM_ID = (Int32)ds1.Tables[0].Rows[i]["VehicleID"];
            }
            if (ds1.Tables[0].Rows[i]["LG_ZM"] != DBNull.Value)
            {
              KommiCtr.DispoCheck.LGZM = Convert.ToDecimal(ds1.Tables[0].Rows[i]["LG_ZM"]);
            }
            if (ds1.Tables[0].Rows[i]["zlGG"] != DBNull.Value)
            {
              //DispoCheck.zlGG = Convert.ToDecimal(ds1.Tables[0].Rows[i]["zlGG"]);
            }
          }
          if (typ == "F")
          {
            if (ds1.Tables[0].Rows[i]["PersonalID"] != DBNull.Value)
            {
              KommiCtr.DispoCheck.P_ID = (Int32)ds1.Tables[0].Rows[i]["PersonalID"];
            }
          }
        }
      }
      else
      {
        KommiCtr.DispoCheck.A_ID = 0;
        KommiCtr.DispoCheck.P_ID = 0;
      }
      CheckRecource(ref KommiCtr);
      //return KommiCtr;  
    }
    //
    private void CheckRecource(ref Sped4.Controls.AFKalenderItemKommi KommiCtr)
    {
        KommiCtr.DispoCheck.disponieren = true;
        //Check und notfalls disponieren auf false
        string strFehler = string.Empty;
        if (KommiCtr.DispoCheck.A_ID == 0)
        {
          KommiCtr.DispoCheck.disponieren = false;
          strFehler = strFehler + "\r\n" + "Ressource Auflieger\r\n";
        }
        if (KommiCtr.DispoCheck.P_ID == 0)
        {
          KommiCtr.DispoCheck.disponieren = false;
          strFehler = strFehler + "Ressource Fahrer\r\n";
        }
        if (KommiCtr.DispoCheck.disponieren == false)
        {
          clsMessages.Disposition_RecourcenFehlen(strFehler);
          KommiCtr.DispoCheck.disponieren = false;
          KommiCtr.DispoCheck.bo_RessourcenCheckOK = false;
        }

        /****
        if (!KommiCtr.DispoCheck.disponieren)
        {
            KommiCtr.DispoCheck.bo_BackToOldZM = true;
            SetBackToOldZM(true);
        }
        else
        {
            KommiCtr.DispoCheck.bo_BackToOldZM = false;
            SetBackToOldZM(false);
        }
        ****/
    }
    //
    //--------- Gewichtscheck  -------------
    //
    private void DispoGewichtsCheck(ref Sped4.Controls.AFKalenderItemKommi KommiCtr)
    {
      decimal Ladungsgewicht = 0.0M;
      SetGewichtsangaben(ref KommiCtr);
      
      //test
      Ladungsgewicht = KommiCtr.DispoCheck.TourGewicht;
      Ladungsgewicht = Ladungsgewicht+KommiCtr.DispoCheck.AuftragPosGewicht;

      if(!KommiCtr.DispoCheck.init)
      {
        Ladungsgewicht=Ladungsgewicht-KommiCtr.DispoCheck.AuftragPosGewicht;
        KommiCtr.DispoCheck.bo_BackToOldZM = true;
      }

      if (Ladungsgewicht > KommiCtr.DispoCheck.maxGewicht)
      {
        KommiCtr.DispoCheck.GewichtZuHoch = true;

        if (KommiCtr.DispoCheck.init)
        {
          if (clsMessages.Disposition_DragDropGewichtsCheck(Functions.FromatDecimal(Ladungsgewicht), Functions.FromatDecimal(KommiCtr.DispoCheck.maxGewicht)))
          {
            KommiCtr.DispoCheck.GewichtFreigabe = true;
            SetOKGewichtTrue();
            KommiCtr.DispoCheck.disponieren = true;
          }
          else
          {
            KommiCtr.DispoCheck.GewichtFreigabe = false;
            KommiCtr.DispoCheck.disponieren = false;
            SetOKGewichtFalse();
            DeleteDispoCheck();
            KommiCtr.DeleteKommissionFromDispoKalender();
          }
        }
        else
        {
          if (KommiCtr.Kommission.KFZ_ZM == KommiCtr.DispoCheck.oldZM)
          {
            if (KommiCtr.DispoCheck.GewichtFreigabe)
            {
              KommiCtr.DispoCheck.disponieren = true;
            }
            else
            {
              KommiCtr.DispoCheck.disponieren = false;
            }
          }
          else
          {
            if (clsMessages.Disposition_DragDropGewichtsCheck(Functions.FromatDecimal(Ladungsgewicht), Functions.FromatDecimal(KommiCtr.DispoCheck.maxGewicht)))
            {
              KommiCtr.DispoCheck.GewichtFreigabe = true;
              SetOKGewichtTrue();
              KommiCtr.DispoCheck.disponieren = true;
            }
            else
            {
              KommiCtr.DispoCheck.GewichtFreigabe = false;
              KommiCtr.DispoCheck.disponieren = false;
              SetOKGewichtFalse();
              KommiCtr.Kommission.KFZ_ZM = KommiCtr.DispoCheck.oldZM;

              if (KommiCtr.DispoCheck.bo_BackToOldZM)
              {
                KommiCtr.DispoCheck.GewichtFreigabe = true;
                
              }
              //DeleteDispoCheck();
              //KommiCtr.DeleteKommissionFromDispoKalender();
            }
          }
        }
      }
      else
      {
        KommiCtr.DispoCheck.GewichtFreigabe = true;
        SetOKGewichtTrue();
      }
    }
    //
    //------------- Gewichtsberechnung -----------------
    //
    private void SetGewichtsangaben(ref Sped4.Controls.AFKalenderItemKommi KommiCtr)
    {
      //MaxGewicht
      KommiCtr.DispoCheck.maxGewicht = KommiCtr.DispoCheck.zlGG - KommiCtr.DispoCheck.LGA; // -KommiCtr.DispoCheck.LGZM;
      //
      if (KommiCtr.Kommission.tatGewicht > 0.0m)
      {
        KommiCtr.DispoCheck.AuftragPosGewicht = clsAuftragPos.GetGewichtFromAuftragPos(KommiCtr.Kommission.AuftragID, KommiCtr.Kommission.AuftragPos, true);
      }
      else
      {
        KommiCtr.DispoCheck.AuftragPosGewicht = clsAuftragPos.GetGewichtFromAuftragPos(KommiCtr.Kommission.AuftragID, KommiCtr.Kommission.AuftragPos, false);
      }

      clsKommission kommi = new clsKommission();
      kommi.BeladeZeit = KommiCtr.Kommission.BeladeZeit;
      kommi.EntladeZeit = KommiCtr.Kommission.EntladeZeit;
      kommi.KFZ_ZM = KommiCtr.Kommission.KFZ_ZM;

      if (KommiCtr.Kommission.tatGewicht > 0.0m) //tat Gewicht
      {
        KommiCtr.DispoCheck.TourGewicht = kommi.GetGesamtGewichtOnSameTime(true);
      }
      else  //gem Gewicht
      {
        KommiCtr.DispoCheck.TourGewicht = kommi.GetGesamtGewichtOnSameTime(false);
      }
      KommiCtr.DispoCheck.KommiAnzahlTour = kommi.GetCountKommiOnSameTime();

      if (KommiCtr.Kommission.tatGewicht > 0.0m) //tat Gewicht
      {
        KommiCtr.DispoCheck.TourGewicht = kommi.GetGesamtGewichtOnSameTime(true);
      }
      else  //gem Gewicht
      {
        KommiCtr.DispoCheck.TourGewicht = kommi.GetGesamtGewichtOnSameTime(false);
      }
      KommiCtr.DispoCheck.KommiAnzahlTour = kommi.GetCountKommiOnSameTime();
    }
  }
}




   