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

namespace Sped4.Classes
{
  class clsResource
  {
    

    public ArrayList AuflRecourceList = new ArrayList();
    public ArrayList FahrRecourceList = new ArrayList();
    public ArrayList VehiRecourceList = new ArrayList();

    private Int32 _m_i_ID;
    private Int32 _m_i_RecourceID;
    private string _m_str_KFZ;
    private Int32 _m_i_PersonalID;
    private DateTime _m_dt_TimeFrom;
    private DateTime _m_dt_TimeTo;
    private Int32 _VehicleID;
    private Int32 _VehicleID_Truck;
    private Int32 _VehicleID_Trailer;
    private char _m_ch_RecourceTyp;
    private Int32 _newRecourceID;
    private DateTime _m_dt_RecourceUnEnd;
    private DateTime _m_dt_RecourceStart;
    private DateTime _m_dt_RecourceEnd;
    private string _m_str_Name;
    private string _InfoText;


    public DateTime m_dt_RecourceUnEnd
    {
      get
      {
          _m_dt_RecourceUnEnd = DateTime.MaxValue;
        return _m_dt_RecourceUnEnd;
      }
      set { _m_dt_RecourceUnEnd = value; }
    }
    public DateTime m_dt_RecourceStart
    {
      get
      {
        _m_dt_RecourceStart = DateTime.Now;
        return _m_dt_RecourceStart;
      }
      set { _m_dt_RecourceStart = value; }
    }
    public DateTime m_dt_RecourceEnd
    {
      get { return _m_dt_RecourceEnd; }
      set { _m_dt_RecourceEnd = value; }
    }
    public Int32 m_i_ID
    {
      get { return _m_i_ID; }
      set { _m_i_ID = value; }
    }
    public Int32 m_i_RecourceID
    {
      get { return _m_i_RecourceID; }
      set { _m_i_RecourceID = value; }
    }

    public string m_str_KFZ
    {
      get { return _m_str_KFZ; }
      set { _m_str_KFZ = value; }
    }
    public Int32 m_i_PersonalID
    {
      get { return _m_i_PersonalID; }
      set { _m_i_PersonalID = value; }
    }
    public DateTime m_dt_TimeFrom
    {
      get { return _m_dt_TimeFrom; }
      set { _m_dt_TimeFrom = value; }
    }
    public DateTime m_dt_TimeTo
    {
      get { return _m_dt_TimeTo; }
      set { _m_dt_TimeTo = value; }
    }
    public Int32 m_i_VehicleID
    {
      get { return _VehicleID; }
      set { _VehicleID = value; }
    }
    public Int32 m_i_VehicleID_Truck
    {
      get { return _VehicleID_Truck; }
      set { _VehicleID_Truck = value; }
    }
    public Int32 m_i_VehicleID_Trailer
    {
      get { return _VehicleID_Trailer; }
      set { _VehicleID_Trailer = value; }
    }
    public char m_ch_RecourceTyp
    {
      get { return _m_ch_RecourceTyp; }
      set { _m_ch_RecourceTyp = value; }
    }
    public string m_str_Name
    {
      get 
      {
        _m_str_Name=clsPersonal.GetNameByID(m_i_PersonalID);
        return _m_str_Name; 
      }
      set { _m_str_Name = value; }
    }

    public string InfoText
    {
      get
      {
        _InfoText = GetRecourceInfoAuflieger();
        return _InfoText;
      }
      set { _InfoText = value; }
    }

    //************************************************************************************************************
    //
    //-------------  ZM ID über die Recource ID --------------------
    //
    public int GetTruckIDbyRecourceID()
    {
      Int32 VehicleID = 0;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT VehicleID FROM VehicleRecource " +
                                     "WHERE RecourceID='" + m_i_RecourceID + "' AND RecourceTyp='Z'";

      Globals.SQLcon.Open();
      if (Command.ExecuteScalar()==null)
      {
        VehicleID = 0;
      }
      else
      { 
          VehicleID = (Int32)Command.ExecuteScalar();
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      return VehicleID;
    }
    //
    //-------------- Auflieger ID über Recource   --------------------
    //
    //public static int GetVehicleIDFromRecource(DateTime DateFrom, DateTime DateTo, int RecourceID, string Typ)
    public static int GetVehicleIDFromRecource(Int32 RecourceID, string Typ)
    {

      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;

/***      Command.CommandText = "SELECT VehicleID FROM VehicleRecource " +
                                     "WHERE " +
                                          "((DateFrom < '" + DateFrom + "' " + "AND " + "DateTo <= '" + DateTo + "') " +
                                            "OR " +
                                          "(DateFrom >= '" + DateFrom + "' " + "AND " + "DateTo <= '" + DateTo + "') " +
                                              "OR " +
                                          "(DateFrom < '" + DateFrom.AddDays(1) + "' " + "AND " + "DateTo >= '" + DateTo + "')" +
                                              "OR " +
                                          "(DateFrom <= '" + DateFrom + "' " + "AND " + "DateTo > '" + DateTo.AddDays(-1) + "')" +
                                              "OR " +
                                          "(DateFrom < '" + DateFrom + "' " + "AND " + "DateTo > '" + DateTo + "')) " +
                                       "AND " +
                                          "(RecourceID= " + RecourceID + ") AND (RecourceTyp='" + Typ + "')";

      Command.CommandText = "SELECT Distinct VehicleID FROM VehicleRecource " +
                                 "WHERE " +
                                        "(((DateFrom >= '" + DateFrom + "') AND ((DateTo >= '" + DateFrom + "') OR (DateTo<='" + DateTo + "'))) " +
                                            "OR " +
                                             "(((DateFrom >='" + DateFrom + "') OR (DateFrom<='" + DateTo + "')) AND (DateTo > '" + DateTo + "')) " +
                                            "OR " +
                                             "((DateFrom >= '" + DateFrom + "') AND (DateTo <= '" + DateTo + "'))) " + "AND " +
                                      "(RecourceID= " + RecourceID + ") AND (RecourceTyp='" + Typ + "')";

 * ***/
      Command.CommandText = "SELECT Distinct VehicleID FROM VehicleRecource " +
                           "WHERE (RecourceID= " + RecourceID + ") AND (RecourceTyp='" + Typ + "')";

      Globals.SQLcon.Open();
      int VehicleID = (int)Command.ExecuteScalar();
      Command.Dispose();
      Globals.SQLcon.Close();
      return VehicleID;
    }
    //
    // --------  GET Recource ID -------------------------------
    //
    public static int GetRecourceIDbyVehicle(DateTime TimeFrom, DateTime TimeTo, int VehicleID)
    {
      Int32 RecID = 0;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT RecourceID FROM VehicleRecource " +
                                     "WHERE " +
                                        "(((DateFrom >= '" + TimeFrom + "') AND ((DateTo >= '" + TimeFrom + "') OR (DateTo<='" + TimeTo + "'))) " +
                                            "OR " +
                                             "(((DateFrom >='" + TimeFrom + "') OR (DateFrom<='" + TimeTo + "')) AND (DateTo > '" + TimeTo + "')) " +
                                            "OR " +
                                             "((DateFrom >= '" + TimeFrom + "') AND (DateTo <= '" + TimeTo + "'))) " + "AND " +

                                          "(VehicleID = " + VehicleID + ") " +
                                       "ORDER BY DateFrom";


      Globals.SQLcon.Open();
        if (Command.ExecuteScalar() != null)
        {
            RecID = (Int32)Command.ExecuteScalar();
        }
        else
        {
            RecID = 0;
        }
      Command.Dispose();
      Globals.SQLcon.Close();
      return RecID;
    }
    //
    public static int GetRecourceIDbyPersonal(DateTime TimeFrom, DateTime TimeTo, Int32 PersonalID)
    {
      Int32 RecID = 0;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
/*** 
      Command.CommandText = "SELECT RecourceID FROM VehicleRecource " +
                                     "WHERE " +

                                         "((DateFrom < '" + TimeFrom + "' " + "AND " + "DateTo <= '" + TimeTo + "') " +
                                            "OR " +
                                          "(DateFrom >= '" + TimeFrom + "' " + "AND " + "DateTo <= '" + TimeTo + "') " +
                                              "OR " +
                                          "(DateFrom < '" + TimeFrom.AddDays(1) + "' " + "AND " + "DateTo >= '" + TimeTo + "')" +
                                              "OR " +
                                          "(DateFrom <= '" + TimeFrom + "' " + "AND " + "DateTo > '" + TimeTo.AddDays(-1) + "')" +
                                              "OR " +
                                          "(DateFrom < '" + TimeFrom + "' " + "AND " + "DateTo > '" + TimeTo + "')) " +
                                       "AND " +
                                          "(PersonalID = " + PersonalID + ") " +
                                       "ORDER BY DateFrom";
****/

      Command.CommandText = "SELECT RecourceID FROM VehicleRecource " +
                                     "WHERE " +
                                        "(((DateFrom >= '" + TimeFrom + "') AND ((DateTo >= '" + TimeFrom + "') OR (DateTo<='" + TimeTo + "'))) " +
                                            "OR " +
                                             "(((DateFrom >='" + TimeFrom + "') OR (DateFrom<='" + TimeTo + "')) AND (DateTo > '" + TimeTo + "')) " +
                                            "OR " +
                                             "((DateFrom >= '" + TimeFrom + "') AND (DateTo <= '" + TimeTo + "'))) " +
                                       "AND " +
                                          "(PersonalID = " + PersonalID + ") " +
                                       "ORDER BY DateFrom";

      Globals.SQLcon.Open();
      if (Command.ExecuteScalar() != null)
      {
          RecID = (int)Command.ExecuteScalar();
      }
      else
      {
          RecID = 0;
      }      
      Command.Dispose();
      Globals.SQLcon.Close();
      return RecID;
    }
    
    //wird zurzeit nicht verwendet
    //
    //----------- Zugmaschine by Ressource
    //
    public static DataSet GetTrailerAsRecourceByRecourceID(DateTime TimeFrom, DateTime TimeTo, int RecourceID)
    {

      DataSet ds = new DataSet();
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT VehicleID FROM VehicleRecource " +
                                     "WHERE " +
                         /***
                                          "((DateFrom < '" + TimeFrom + "' " + "AND " + "DateTo <= '" + TimeTo + "') " +
                                            "OR " +
                                          "(DateFrom >= '" + TimeFrom + "' " + "AND " + "DateTo <= '" + TimeTo + "') " +
                                              "OR " +
                                          "(DateFrom < '" + TimeFrom.AddDays(1) + "' " + "AND " + "DateTo >= '" + TimeTo + "')" +
                                              "OR " +
                                          "(DateFrom <= '" + TimeFrom + "' " + "AND " + "DateTo > '" + TimeTo.AddDays(-1) + "')" +
                                              "OR " +
                                          "(DateFrom < '" + TimeFrom + "' " + "AND " + "DateTo > '" + TimeTo + "')) " +
                          * ***/
                                        "(((DateFrom >= '" + TimeFrom + "') AND ((DateTo >= '" + TimeFrom + "') OR (DateTo<='" + TimeTo + "'))) " +
                                        "OR " +
                                        "(((DateFrom >='" + TimeFrom + "') OR (DateFrom<='" + TimeTo + "')) AND (DateTo > '" + TimeTo + "')) " +
                                        "OR " +
                                        "((DateFrom >= '" + TimeFrom + "') AND (DateTo <= '" + TimeTo + "'))) " +
                                       "AND " +
                                          "(RecourceID = " + RecourceID + ") " +
                                          "ORDER BY DateFrom";
      Globals.SQLcon.Open();

      ada.Fill(ds);
      Command.Dispose();
      Globals.SQLcon.Close();
      return ds;
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
      Command.CommandText = ("SELECT ID, KFZ FROM FAHRZEUGE WHERE ID = " + _m_i_ID);
      ada.Fill(FahrzTable);
      if (FahrzTable.Rows.Count > 0)
      {
        m_i_ID = (int)FahrzTable.Rows[0]["ID"];
        m_str_KFZ = (string)FahrzTable.Rows[0]["KFZ"];
      }
      Command.Dispose();
      Globals.SQLcon.Close();
    }
    //
    //--------------------------------  Insert neue Recourcen  ------------------
    //
    public void Insert_Truck()
    {
      //neue Recource ID wird nur hier ausgelesen - sonster immer 
      _newRecourceID = GetMaxRecourceID() + 1;
      m_i_RecourceID = _newRecourceID;
      try
      {
        SqlCommand InsertCommand = new SqlCommand();
        InsertCommand.Connection = Globals.SQLcon.Connection;
        InsertCommand.CommandText = "INSERT INTO VehicleRecource " +
                                                 "(RecourceID, RecourceTyp, DateFrom, DateTo, VehicleID) " +
                                             "VALUES (" + m_i_RecourceID + ", " +
                                                             "'Z' , " +
                                                           "'" + m_dt_TimeFrom + "', " +
                                                           "'" + m_dt_TimeTo + "', " +
                                                            m_i_VehicleID_Truck + ")";

        Globals.SQLcon.Open();
        InsertCommand.ExecuteNonQuery();
        InsertCommand.Dispose();
        Globals.SQLcon.Close();
      }
      catch (Exception ex)
      {
        System.Windows.Forms.MessageBox.Show(ex.ToString());
      }
    }
    //
    //------------- Einfügen Auflieger / Trailer --------------
    //
    public void Insert_Trailer()
    {
      try
      {
        SqlCommand InsertCommand = new SqlCommand();
        InsertCommand.Connection = Globals.SQLcon.Connection;
        InsertCommand.CommandText = "INSERT INTO VehicleRecource " +
                                                 "(RecourceID, RecourceTyp, DateFrom, DateTo, VehicleID) " +
                                             "VALUES (" + m_i_RecourceID + ", " +
                                                            "'" + m_ch_RecourceTyp + "', " +
                                                            "'" + m_dt_TimeFrom + "', " +
                                                            "'" + m_dt_TimeTo + "', " +
                                                            m_i_VehicleID_Trailer + ")";

        Globals.SQLcon.Open();
        InsertCommand.ExecuteNonQuery();
        InsertCommand.Dispose();
        Globals.SQLcon.Close();
      }
      catch (Exception ex)
      {
        System.Windows.Forms.MessageBox.Show(ex.ToString());
      }
    }
    //
    //------------- Einfügen Fahrer  -------------------------
    //
    public void Insert_Fahrer()
    {
      try
      {
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        Command.CommandText = "INSERT INTO VehicleRecource " +
                                                 "(RecourceID, RecourceTyp, DateFrom, DateTo, PersonalID) " +
                                             "VALUES (" + m_i_RecourceID + ", " +
                                                            "'" + m_ch_RecourceTyp + "', " +
                                                            "'" + m_dt_TimeFrom + "', " +
                                                            "'" + m_dt_TimeTo + "', " +
                                                            m_i_PersonalID+ ")";

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
    private int GetMaxRecourceID()
    {
      Int32 reVal = 0;
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      Command.CommandText = "Select MAX(RecourceID) FROM VehicleRecource ";
      Globals.SQLcon.Open();
      if (Command.ExecuteScalar() is DBNull)
      {
        reVal = 1;
      }
      else
      {
        reVal =(Int32)Command.ExecuteScalar();
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
      {
        Command.Connection.Close();
      }
      return reVal;
    }

    //
    //------------------------------ UPDATE der Recource DB  ---------------
    //  
    public void UpdateTrailer()
    {
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      Command.CommandText = "Update VehicleRecource SET VehicleID='" + m_i_VehicleID_Trailer + "' " +
                                                                     "WHERE RecourceID='" + m_i_RecourceID + "' AND RecourceTyp='A'";

      Globals.SQLcon.Open();
      Command.ExecuteNonQuery();
      Command.Dispose();
      Globals.SQLcon.Close(); ;
    }
    //
    public void UpdateTrailerStart()
    {
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      Command.CommandText = "Update VehicleRecource SET DateFrom='" + m_dt_TimeFrom + "' " +
                                                                     "WHERE RecourceID='" + m_i_RecourceID + "' AND VehicleID='" + m_i_VehicleID_Trailer + "'";
      Globals.SQLcon.Open();
      Command.ExecuteNonQuery();
      Command.Dispose();
      Globals.SQLcon.Close();
    }
    //
    public void UpdateTrailerEnd()
    {
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      Command.CommandText = "Update VehicleRecource SET DateTo='" + m_dt_TimeTo + "' " +
                                                                     "WHERE RecourceID='" + m_i_RecourceID + "' AND VehicleID='" + m_i_VehicleID_Trailer + "'";
      Globals.SQLcon.Open();
      Command.ExecuteNonQuery();
      Command.Dispose();
      Globals.SQLcon.Close();
    }
    //
    public void UpdateTruck()
    {
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      Command.CommandText = "Update VehicleRecource SET DateFrom='" + m_dt_TimeFrom + "', " +
                                                       "DateTo='" + m_dt_TimeTo + "', " +
                                                       "VehicleID='" + m_i_VehicleID_Truck + "' " +
                                                                         "WHERE RecourceID='" + m_i_RecourceID + "' and RecourceTyp='Z'";
      Globals.SQLcon.Open();
      Command.ExecuteNonQuery();
      Command.Dispose();
      Globals.SQLcon.Close();
    }
    //
    public void UpdateFahrer()
    {
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      Command.CommandText = "Update VehicleRecource SET PersonalID='" + m_i_PersonalID + "' " +
                                                                     "WHERE RecourceID='" + m_i_RecourceID + "' AND RecourceTyp='F'";

      Globals.SQLcon.Open();
      Command.ExecuteNonQuery();
      Command.Dispose();
      Globals.SQLcon.Close(); ;
    }
    //
    public void UpdateFahrerStart()
    {
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      Command.CommandText = "Update VehicleRecource SET DateFrom='" + m_dt_TimeFrom + "' " +
                                                                     "WHERE RecourceID='" + m_i_RecourceID + "' AND PersonalID='" + m_i_PersonalID + "'";
      Globals.SQLcon.Open();
      Command.ExecuteNonQuery();
      Command.Dispose();
      Globals.SQLcon.Close();
    }
    //
    public void UpdateFahrerEnd()
    {
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      Command.CommandText = "Update VehicleRecource SET DateTo='" + m_dt_TimeTo + "' " +
                                                                     "WHERE RecourceID='" + m_i_RecourceID + "' AND PersonalID='" + m_i_PersonalID + "'";
      Globals.SQLcon.Open();
      Command.ExecuteNonQuery();
      Command.Dispose();
      Globals.SQLcon.Close();
    }
    
    //
    //----------- Delete / Löschen der Recource vom TimePanel  -------------------
    //
    public void DeleteRecource(Int32 RecourceID)
    {
        //--- initialisierung des sqlcommand---
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        Command.CommandText = "DELETE FROM VehicleRecource WHERE RecourceID='" +RecourceID + "'";
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
    //
    //
    public static DataSet GetUsedTruckRecource(DateTime DateFrom, DateTime DateTo, Int32 ZM_ID)
    {
      DataSet ds = new DataSet();
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT RecourceTyp, VehicleID, PersonalID, " +
                                   "(Select Leergewicht FROM Fahrzeuge WHERE Fahrzeuge.ID=VehicleID AND Fahrzeuge.ZM='F') as 'LG_A', " +
                                   "(Select Leergewicht FROM Fahrzeuge WHERE Fahrzeuge.ID=VehicleID AND Fahrzeuge.ZM='T') as 'LG_ZM', " +
                                   "(Select zlGG FROM Fahrzeuge WHERE Fahrzeuge.ID=VehicleID AND Fahrzeuge.ZM='F') as 'zlGG' " +
                                   "FROM VehicleRecource " +
                                    "WHERE " +
                                           "("+
                                           "((DateFrom <= '" + DateFrom + "') AND (DateTo >='" + DateTo + "')) " +
                                           ") " +
                                           "AND RecourceID IN " +
                                               "(" +
                                                 "SELECT RecourceID FROM VehicleRecource WHERE " +
                                                 "("+
                                                 "((DateFrom <= '" + DateFrom + "') AND (DateTo >='" + DateTo + "')) "+ 
                                                 ") " +
                                                  "AND VehicleID='" + ZM_ID + "' " +
                                                ")";

      Globals.SQLcon.Open();

      ada.Fill(ds);
      Command.Dispose();
      Globals.SQLcon.Close();
      return ds;
    }
    //
    // Suche über  ID und Zeitraum ------------------
    //
    public static bool IsTrailerUsed(DateTime DateFrom, DateTime DateTo, Int32 VehicleID, string Typ, Int32 RecourceID)
    {
      bool IsUsed = false;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT ID FROM VehicleRecource " +
                                     "WHERE " +
                                            "(((DateFrom <= '" + DateFrom + "') AND (DateTo >'" + DateFrom + "')) " +
                                            "OR " +
                                            "((DateFrom = '" + DateFrom + "') AND (DateTo <'" + DateTo + "'))" +
                                            "OR " +
                                            "((DateFrom <'" + DateTo + "') AND (DateTo > '" + DateTo + "')) " +
                                            "OR " +
                                            "((DateFrom >'" + DateFrom + "') AND (DateTo = '" + DateTo + "')) " +
                                            "OR " +
                                             "((DateFrom < '" + DateFrom + "') AND (DateTo > '" + DateTo + "')) " +
                                            "OR " +
                                             "((DateFrom > '" + DateFrom + "') AND (DateTo < '" + DateTo + "'))) " +

                                       "AND " +
                                          "(VehicleID= " + VehicleID + ") AND (RecourceTyp='" + Typ + "') AND (RecourceID<>'"+RecourceID+"')";

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
    //
    //
    public static bool IsFahrerUsed(DateTime DateFrom, DateTime DateTo, Int32 PersonalID, string Typ, Int32 RecourceID)
    {
      bool IsUsed = false;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT ID FROM VehicleRecource " +
                                     "WHERE " +

                                            "(((DateFrom <= '" + DateFrom + "') AND (DateTo >'" + DateFrom + "')) " +
                                            "OR " +
                                            "((DateFrom = '" + DateFrom + "') AND (DateTo <'" + DateTo+ "'))"+
                                            "OR " +
                                            "((DateFrom <'" + DateTo + "') AND (DateTo > '" + DateTo + "')) " +
                                            "OR " +
                                            "((DateFrom >'" + DateFrom + "') AND (DateTo = '" + DateTo + "')) " +
                                            "OR " +
                                             "((DateFrom < '" + DateFrom + "') AND (DateTo > '" + DateTo + "')) " +
                                            "OR " +
                                             "((DateFrom > '" + DateFrom + "') AND (DateTo < '" + DateTo + "'))) " +

                                       "AND " +
                                          "(PersonalID= " + PersonalID + ") AND (RecourceTyp='" + Typ + "') AND (RecourceID<>'" + RecourceID + "')";


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
    //------ Check ob Personaldatensatz verwendet wird-------------
    //kann nur gelöscht werden, wenn Sie nicht vorhanden ist
    public static bool IsPersonalIDIn(Int32 _PersonalID)
    {
      bool IsUsed = false;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT ID From VehicleRecource WHERE PersonalID= '" + _PersonalID + "'";

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
    //------ Check ob FahrzeugID verwendet wird-------------
    //kann nur gelöscht werden, wenn Sie nicht vorhanden ist
    //
    public static bool IsFahrzeugIDIn(Int32 _VehicleID)
    {
      bool IsUsed = false;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT ID From VehicleRecource WHERE VehicleID= '" + _VehicleID + "'";

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
    //--- erstellt den Infotext der Recource für den Auflieger ----------------------
    //
    private string GetRecourceInfoAuflieger()
    {
      string strInfo = string.Empty;
      DataSet ds = new DataSet();
      ds = clsFahrzeuge.GetRecByID(m_i_VehicleID_Trailer);

      for (Int32 i = 0; i < ds.Tables[0].Rows.Count; i++)
      {
        strInfo =ds.Tables[0].Rows[i]["KFZ"].ToString();
        strInfo = strInfo + "-Achsen:" + ds.Tables[0].Rows[i]["Achsen"].ToString();
        strInfo = strInfo +"-Länge:"+ ds.Tables[0].Rows[i]["Laenge"].ToString() + "m";
        strInfo = strInfo +"-LG:"+ ds.Tables[0].Rows[i]["Leergewicht"].ToString() + "kg";
        strInfo = strInfo +"-IH:" + ds.Tables[0].Rows[i]["Innenhoehe"].ToString() + "m";
        if (ds.Tables[0].Rows[i]["Innenhoehe"].ToString() == "T")
        {
          strInfo = strInfo + "-Mulde"; 
        }
      }
      return strInfo;
    }
    //
    //
    //
    public DataTable LoadRecouce(string RecourceTyp, bool RecourceLoaded)
    {        
      DataTable RecourceTable = new DataTable();
      RecourceTable.Clear();
      if (!RecourceLoaded)
      {
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;
        Command.CommandText = "SELECT" +
                                          " * " +

                                     "FROM " +
                                          "VehicleRecource " +
                                     "WHERE " +
                                          "(((DateFrom < '" + m_dt_TimeFrom + "') AND (DateTo >'" + m_dt_TimeFrom + "')) " +
                                          "OR " +
                                          "((DateFrom = '" + m_dt_TimeFrom + "') AND (DateTo <'" + m_dt_TimeTo + "'))" +
                                          "OR " +
                                          "((DateFrom <'" + m_dt_TimeTo + "') AND (DateTo > '" + m_dt_TimeTo + "')) " +
                                          "OR " +
                                          "((DateFrom >'" + m_dt_TimeFrom + "') AND (DateTo = '" + m_dt_TimeTo + "')) " +
                                          "OR " +
                                           "((DateFrom < '" + m_dt_TimeFrom + "') AND (DateTo > '" + m_dt_TimeTo + "')) " +
                                          "OR " +
                                           "((DateFrom > '" + m_dt_TimeFrom + "') AND (DateTo < '" + m_dt_TimeTo + "'))) " +

                                       "AND (RecourceTyp='" + m_ch_RecourceTyp + "' OR RecourceTyp='Z') ORDER BY DateFrom";

        ada.Fill(RecourceTable);
        Command.Dispose();
        Globals.SQLcon.Close();

        if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
          Command.Connection.Close();
      }
      return RecourceTable;
    }
  }
}


