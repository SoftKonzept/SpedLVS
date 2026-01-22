using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sped4.Classes;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Globalization;
using System.ComponentModel;
using System.Data;


public static class Functions
{


    public static string RealToSQLString(double nwert) // Wurm drin
    {
        string swert;
        CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
        swert = nwert.ToString(ci);       
        return swert;
    }

    public static bool init_con()
    {
        Globals.SQLcon.Server = Globals.INI.ReadString("Config", "Server");
        Globals.SQLcon.Database = Globals.INI.ReadString("Config", "Database");
        Globals.SQLcon.User = Globals.INI.ReadString("Config", "User");
        Globals.SQLcon.Password = Globals.INI.ReadString("Config", "pw");
        if (Globals.SQLcon.init() == false)
        {
            return false;
        }
        try
        {
            Globals.SQLcon.Open();
            Globals.SQLcon.Close();
        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.ToString());
            Globals.SQLcon.Close();
            return false;
        }
        return true;
    }

   //
    //---------- Prüft geöffnete Forms  ------------------
    //
    public static Form IsFormAlreadyOpen(Type FormType)
    {
        foreach (Form OpenForm in Application.OpenForms)
        {
            if (OpenForm.GetType() == FormType)
            {
                return OpenForm;
            }
        }
        return null;
    }
    //
    //---------- form schlissen  ---------------------
    //
    public static void FormClose(Type FormType)
    {
        foreach (Form OpenForm in Application.OpenForms)
        {
            if (OpenForm.GetType() == FormType)
            {
                OpenForm.Close();
                break;
            }
        }
    }
   //********************************************** AUSGABEN **************************************
   //
   //---- Formatiert Decimal für die Ausgabe
   //
    public static string FromatDecimal(decimal Value)
    {
      return Value.ToString("#,##0.00");
    }
    //---- Formatiert Zeitausgabe (HH:MM) -------------
    //
    public static string FromatShortDateTime(DateTime Value)
    {
      return Value.ToString("MM.dd.yyyy HH:mm");
    }
    //---- Formatiert Zeitausgabe (HH:MM) -------------
    public static string FromatShortTime(DateTime Value)
    {
      return Value.ToString("HH:mm");
    }
    //
    //---- bestimmte Tage zählen / Anzahl ermitteln
    //
    public static Int32 CountDays(DateTime DateFrom, TimeSpan Periode, string Wochentag)
    {
      
      Int32 Count = 0;
      for (Int32 i=0; i<=(Int32)Periode.Days; i++)
      {
        if (DateFrom.AddDays(i).DayOfWeek.ToString() == Wochentag)
        {
          Count++;
        }
      }
      return Count;
    }
    //
    //
    //
    public static bool CheckForInt(string strEingabe)
    {
      bool eingabeOK = true;
      char[] KommaPunkt = { ',', '.'};
      if (strEingabe.IndexOfAny(KommaPunkt) > -1)
      {
        eingabeOK = false;
      }
      return eingabeOK;
    }
    //
    //-- während der Eingabe ins Grid wird die Eingabe geprüft -----------
    //
    public static bool CheckNum(string strEingabe)
    {
      bool eingabeOK = true;
      char[] ad = { '@' };
      char[] bst = { 'a', 'A', 'b', 'c', 'C', 'd', 'D', 'e', 'E', 'F', 'f', 'g', 'G', 'h', 'H', 'i', 'I', 'j', 'J', 'k', 'K', 'l', 'L', 'm', 'M', 'n', 'N', 'o', 'O', 'p', 'P', 'q', 'Q', 'r', 'R', 's', 'S', 't', 'T', 'u', 'U', 'v', 'V', 'w', 'W', 'x', 'X', 'y', 'Y', 'z', 'Z' };
      char[] Uml = { 'ä', 'Ä', 'ö', 'Ö', 'ü', 'Ü' };
      char[] sz = { '!', '"', '§', '$', '%', '&', '/', '(', ')', '?', '`', '´', '#', '²', '³', '{', '[', ']', '}', 'ß', '-', '+', '*', ';', ':', '^', '°', '<', '>', '|' };

      if (strEingabe.IndexOfAny(ad) > -1)
      {
        eingabeOK = false;
      }
      if (strEingabe.IndexOfAny(bst) > -1)
      {
        eingabeOK = false;
      }
      if (strEingabe.IndexOfAny(Uml) > -1)
      {
        eingabeOK = false;
      }
      if (strEingabe.IndexOfAny(sz) > -1)
      {
        eingabeOK = false;
      }
      return eingabeOK;
    }
    //
    //-------------- string Time/Date ZF ---------
    //
    public static DateTime GetStrTimeZF(NumericUpDown numStd, NumericUpDown numMin)
    {
      string strStd = string.Empty;
      string strMin = string.Empty;
      string strZeit = string.Empty;
      string strDate = string.Empty;

      if (numStd.Value < 10)
      {
        strStd = "0" + numStd.Value.ToString();
      }
      else
      {
        strStd = numStd.Value.ToString();
      }
      if (numMin.Value < 10)
      {
        strMin = "0" + numMin.Value.ToString();
      }
      else
      {
        strMin = numMin.Value.ToString();
      }

      strZeit = strStd + ":" + strMin + ":00";
      strDate = "01.01.1900 " + strZeit;
      return Convert.ToDateTime(strDate);
    }
    //
    //------------- Table aus DS entfernen -----
    //
    public static void RemoveAllTableFromDataSet(DataSet ds)
    {
      for (Int32 i = 0; i <= ds.Tables.Count - 1; i++)
      {
        ds.Tables.RemoveAt(i);
      }
    }
    //
    //
    //
    public static bool IsRowInDataTable(DataTable dt, DataRow row)
    {
      bool result=true;
      Int32 iCount = 0;
      for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
      {
        for (Int32 j = 0; j <= dt.Columns.Count - 1; j++)
        {
          string test1 = dt.Rows[i][j].ToString();
          string test2 = row[j].ToString();
         
          if (dt.Rows[i][j].ToString() == row[j].ToString())
          {
            result = true;
          }
          else
          {
            result = false;
            iCount = iCount + 1;
          }
        }
      }
      if (iCount > 0)
      {
        result = false;
      }
      return result;
    }
    //
    //----------- count Rows nach Filter -----------
    //
    public static Int32 RowItemCount(ref DataRow[] rows)
    {
      Int32 iCount = 0;
      DataTable dt = new DataTable();
      foreach (DataRow row in rows)
      {
        dt.ImportRow(row);
      }
      iCount = dt.Rows.Count;
      return iCount;
    }
    //
    //----------- Get and Set RG bzw. GS Nr aus Rechnungsnummer ------------
    //
    public static DataSet GetAndSetRGGSNr(DataSet dsRG, bool PrintAgain)
    {
      DataTable dtRechnung = new DataTable("Rechnung");
      dtRechnung.Columns.Add("RGNr", typeof(Int32));
      DataRow row1;
      row1 = dtRechnung.NewRow();
      row1["RGNr"] = 0;
      dtRechnung.Rows.Add(row1);

      //Unterscheide Dokument wird wiederholt ausgedruckt
      if (PrintAgain)
      {
        if (dsRG.Tables["Rechnung"] != null)
        {
          dsRG.Tables.Remove("Rechnung");
        }
        dtRechnung.Rows[0]["RGNr"] = (Int32)dsRG.Tables["Frachtdaten"].Rows[0]["RG_ID"];
        dsRG.Tables.Add(dtRechnung);
      }
      else
      {
        clsRechnungen rg = new clsRechnungen();

        if (dsRG.Tables["Rechnung"] == null)
        {
          dtRechnung.Rows[0]["RGNr"] = rg.RGNr;
          dsRG.Tables.Add(dtRechnung);
        }
        else
        {
          DataRow row;
          row = dsRG.Tables["Rechnung"].NewRow();
          row["RGNr"] = rg.RGNr;
          dsRG.Tables["Rechnung"].Rows.Add(row);
        }
      }
      return dsRG;
    }
    //
    //----------- Cursor an den Anfang in einer TExtbox setzen --------------------
    //
    public static void SetCursorAtFirstPosInTextBox(TextBox tb)
    {
      tb.Select(0, 0);
    }
    //
    //------------- Cursor ans Ende der Texbox setzen --------------
    //
    public static void SetCursorAtEntInTextBox(TextBox tb)
    {
      tb.Select(tb.Text.Length, 0);
    }
    //
    //------------- Monatsname in DataTable -----------
    //
    public static Array GetMonatsnamen()
    {
      string[] Monatsnamen= new string[] {"Januar",
                                          "Februar",
                                          "März",
                                          "April",
                                          "Mai",
                                          "Juni",
                                          "Juli",
                                          "August",
                                          "September",
                                          "Oktober",
                                          "November",
                                          "Dezember"};

      return Monatsnamen;
    }
    //
    public static void GlobalUserSet(ref Globals._GL_USER _GL_User, DataTable dt)
    {
      _GL_User.User_ID =(Int32) dt.Rows[0]["ID"];
      _GL_User.initialen = (string)dt.Rows[0]["Initialen"];
      _GL_User.LoginName = (string)dt.Rows[0]["LoginName"];
      if (dt.Rows[0]["Name"] != DBNull.Value)
      {
        _GL_User.Name = (string)dt.Rows[0]["Name"];
      }
      else
      {
        _GL_User.Name = string.Empty;
      }
      if (dt.Rows[0]["Vorname"] != DBNull.Value)
      {
        _GL_User.Vorname = (string)dt.Rows[0]["Vorname"];
      }
      else 
      {
        _GL_User.Vorname = string.Empty;
      }
      if (dt.Rows[0]["Name"] != DBNull.Value)
      {
        _GL_User.Name = (string)dt.Rows[0]["Name"];
      }
      else
      {
        _GL_User.Name = string.Empty;
      }
      if (dt.Rows[0]["Vorname"] != DBNull.Value)
      {
        _GL_User.Vorname = (string)dt.Rows[0]["Vorname"];
      }
      else
      {
        _GL_User.Vorname = string.Empty;
      }
      if (dt.Rows[0]["Tel"] != DBNull.Value)
      {
        _GL_User.Telefon = (string)dt.Rows[0]["Tel"];
      }
      else
      {
        _GL_User.Telefon = string.Empty;
      }
      if (dt.Rows[0]["Fax"] != DBNull.Value)
      {
        _GL_User.Mail = (string)dt.Rows[0]["Mail"];
      }
      else
      {
        _GL_User.Mail = string.Empty;
      }




    }
    //
    //------------ Eintrag ins Logbuch ------------------------------
    //
    public static void AddLogbuch(Int32 BenutzerID, string Aktion, string Beschreibung)
    {
      clsLogbuch log = new clsLogbuch();
      log.BenutzerID = BenutzerID;
      log.Aktion = Aktion;
      log.Beschreibung = Beschreibung;
      log.LogbuchInsert();
    }
  //
  //---------------- Zeitangabe Studnen und Minunten ----------
  //
  public static string FormatToHHMM(DateTime time)
  {
    string returnTime = string.Empty;

    //HH
    if(time.Hour <10)
    {
      returnTime=returnTime+"0"+time.Hour.ToString();
    }
    else
    {
      returnTime=returnTime+time.Hour.ToString();
    }
    //MM
    if (time.Minute < 10)
    {
      returnTime = returnTime + ":0" + time.Minute.ToString();
    }
    else
    {
      returnTime = returnTime + ":" + time.Minute.ToString();
    }
    return returnTime;
  }
  //
  //----------- Check Datum Differenz --------------
  //
  public static bool CheckDateDifferenceTillToday(DateTime DateToCheck, Int32 Tage)
  {
    bool dif = false;
    TimeSpan tmpDif=DateToCheck.Subtract(DateTime.Today);

    if (Convert.ToInt32(tmpDif.Days.ToString()) <= Tage)
    {
      dif = true;
    }
    else
    {
      dif = false;
    }
    return dif;
  }
  //
  //--------- CHeck and Remove Table From DS -----------------
  //
  public static void CheckAndRemoveTableFromDataSet(ref DataSet ds, string RemoveTableName)
  {
    if(ds.Tables[RemoveTableName]!=null)
    {
      ds.Tables.Remove(RemoveTableName);
    }
  }
  //
  //------------------ max Recourcenentzeit ermitteln -----------
  //
  public static DateTime GetMaxRecourcenentzeit(ref Globals._Recources recource)
  {
    DateTime maxREZ = DateTime.Today;
    clsResource res = new clsResource();
    res.m_i_RecourceID = recource.RecourceID;
    //Get RecourceID
    Int32 RecDB_ID = res.GetTruckIDbyRecourceID();  //ID der DB der jeweilingen Recource (Fahrzeug oder Faherer)

    maxREZ = clsKommission.GetMaxEntladeZeit(RecDB_ID); 
    /****
    switch (recource.RecourceTyp)
    { 
      //Fahrer
      case "F":
        maxREZ = clsKommission.GetMaxEntladeZeit(RecDB_ID);     
        break;
      
      //Auflieger
      case "A":
        maxREZ = clsKommission.GetMaxEntladeZeit(RecDB_ID); 
        break;
    }
    ****/
    return maxREZ;
  }
  //
  //-------------- Search Table -----------------------
  //
  public static DataTable FilterDataTable(DataTable dt, string SearchText, string Columnname)
  {
    DataTable tmpTable = new DataTable();
    tmpTable = dt.Clone();
    tmpTable.Clear();

    if (dt.Rows.Count > 0)
    {
      if (dt.Columns[Columnname] != null)
      {
        DataRow[] rows;
        string strDataType = dt.Columns[Columnname].DataType.ToString();

        switch (strDataType)
        {
          case "System.Decimal":
            decimal decSearchText;
            if (decimal.TryParse(SearchText, out decSearchText))
            {
              rows = dt.Select(Columnname + " ='" + decSearchText + "'", Columnname);
              string Ausgabe2 = string.Empty;
              foreach (DataRow row in rows)
              {
                Ausgabe2 = Ausgabe2 + row[Columnname].ToString() + "\n";
                tmpTable.ImportRow(row);
              }
            }
            break;

          case "System.Int32":
            Int32 iSearchText;
            if (Int32.TryParse(SearchText, out iSearchText))
            {
              rows = dt.Select(Columnname + " ='" + iSearchText + "'", Columnname);
              string Ausgabe1 = string.Empty;
              foreach (DataRow row in rows)
              {
                Ausgabe1 = Ausgabe1 + row[Columnname].ToString() + "\n";
                tmpTable.ImportRow(row);
              }
            }
            break;

          case "System.String":
            rows = dt.Select(Columnname + " LIKE '%" + SearchText + "%'", Columnname);
            string Ausgabe = string.Empty;
            foreach (DataRow row in rows)
            {
              Ausgabe = Ausgabe + row[Columnname].ToString() + "\n";
              tmpTable.ImportRow(row);
            }
            break;
        }
      }
    }
    return tmpTable;
  }
  //
  //
  //
  public static String CutString(string strToCut)
  {
    string tmp=string.Empty;
    if (strToCut != string.Empty)
    {
      tmp = strToCut.ToString().Trim();
    }
    return tmp;
  }


}

