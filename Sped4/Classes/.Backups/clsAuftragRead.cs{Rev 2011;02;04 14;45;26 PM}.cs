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
  class clsAuftragRead
  {
    private int _ID;
    private int _AuftragPosID;
    private int _UserID;


    public int ID
    {
      get { return _ID;  }
      set { _ID = value; }
    }
    public int AuftragPosID
    {
      get { return _AuftragPosID; }
      set { _AuftragPosID = value; }
    }
    public int UserID
    {
      get { return _UserID; }
      set { _UserID = value; }
    }
 

    //**********************************************************************************
    //--------------              Methoden
    //**********************************************************************************
    //
    //
    //
    public void Add()
    {
      if(
        ((AuftragPosID > 0) | (AuftragPosID != null))
        &
        ((UserID > 0) | (UserID != null))
        )
      {
        if (!UserReadAuftragExist())
        {
          InsertToDB();
        }
      }
    }
    //
    //
    //
    private void InsertToDB()
    {
      try
      {
        //--- initialisierung des sqlcommand---
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;

        //----- SQL Abfrage -----------------------
        Command.CommandText = ("INSERT INTO AuftragRead (UserID, AuftragPosID) " +
                                "VALUES ('" + UserID + "','"     // ist 0 steht momentan in Artikel
                                            + AuftragPosID + "')");

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
        
      }
      finally
      { 
        //ID auslesen
        GetID();
      }
    }
    //
    //
    //
    private void GetID()
    {      
      try
      {
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        ada.SelectCommand = Command;
        Command.CommandText = "SELECT ID FROM AuftragRead WHERE UserID='" + UserID + "' "+
                                                               "AND AuftragPosID='"+AuftragPosID+"'";
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
        //MessageBox.Show(ex.ToString());
      }
    }
    //
    //
    //
    public void DeleteReadAuftragByID(Int32 iID)
    {
      if ((iID != null) | (iID > 0))
      {
        ID = iID;
        //--- initialisierung des sqlcommand---
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        Command.CommandText = "DELETE FROM AuftragRead WHERE ID='" + ID + "'";

        Globals.SQLcon.Open();
        Command.ExecuteNonQuery();
        Command.Dispose();
        Globals.SQLcon.Close();
        if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
        {
          Command.Connection.Close();
        }
      }
    }
    //
    //---------- löscht alle Datensätze des Users -------------------
    //
    public void DeleteReadAuftragByUser(Int32 iUser)
    {
      if (iUser > 0)
      {
        UserID = iUser;
        //--- initialisierung des sqlcommand---
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        Command.CommandText = "DELETE FROM AuftragRead WHERE UserID='" + UserID + "'";

        Globals.SQLcon.Open();
        Command.ExecuteNonQuery();
        Command.Dispose();
        Globals.SQLcon.Close();
        if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
        {
          Command.Connection.Close();
        }
      }
    }
    //
    //------------ löscht alle Datensätze der AuftragPositionen
    //
    public void DeleteReadAuftragAuftragPosID(Int32 iAuftragPosID)
    {
      if (iAuftragPosID > 0)
      {
        AuftragPosID = iAuftragPosID;
        //--- initialisierung des sqlcommand---
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;
        Command.CommandText = "DELETE FROM AuftragRead WHERE AuftragPosID='" + AuftragPosID + "'";

        Globals.SQLcon.Open();
        Command.ExecuteNonQuery();
        Command.Dispose();
        Globals.SQLcon.Close();
        if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
        {
          Command.Connection.Close();
        }
      }
    }
    //
    //------------ Auftrag gelesen von dem User ------------------
    //
    public bool UserReadAuftrag(Int32 iUser, Int32 iAuftragPosID)
    {
      bool retVal = true;
      if(
        ((iUser>0) | (iUser!= null))
        &
        ((iAuftragPosID>0) | (iAuftragPosID!= null))
        )
      {
          UserID=iUser;
          AuftragPosID=iAuftragPosID;

          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          Command.CommandText = "Select ID FROM AuftragRead WHERE UserID='" + UserID + "' AND AuftragPosID='"+AuftragPosID+"'";
          Globals.SQLcon.Open();
          object obj = Command.ExecuteScalar();
          if (obj != null)
          {
            retVal=false;
          }
          Command.Dispose();
          Globals.SQLcon.Close();
          
      }
      return retVal;
    }
    //
    private bool UserReadAuftragExist()
    {
      bool retVal = false;
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      Command.CommandText = "Select ID FROM AuftragRead WHERE UserID='" + UserID + "' AND AuftragPosID='" + AuftragPosID + "'";
      Globals.SQLcon.Open();
      object obj = Command.ExecuteScalar();
      if (obj != null)
      {
        retVal = true;
      }
      Command.Dispose();
      Globals.SQLcon.Close();
      return retVal;
    }
 
  }
}
