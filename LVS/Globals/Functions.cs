using LVS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

public static class Functions
{

    /////<summary>Functions</summary>
    /////<remarks>Die statische Klasse Funktionen beinhaltet diverse Hilfsfunktionen.
    /////         Aufbau:
    /////         1. SQL - Connection        
    /////         2. CTR - Forms
    /////         3. Formatierungen
    /////         4. 
    ///// </remarks>

    ///*********************************************************************************************
    // *  1.                           SQL - Connection
    // * ******************************************************************************************/
    ///<summary>Functions / init_con</summary>
    ///<remarks>Initialisierung der SQL-Connection. Die einzelnen Parameter werden aus der 
    ///         config.ini ermittelt.</remarks>
    ///<return>bool for aktiv connection</param>
    public static bool init_con(ref Globals._GL_SYSTEM GLSystem)
    {
        clsSQLcon.Server = GLSystem.con_Server;
        clsSQLcon.Database = GLSystem.con_Database;
        clsSQLcon.User = GLSystem.con_UserDB;
        clsSQLcon.Password = GLSystem.con_PassDB;

        clsSQLcon sql = new clsSQLcon();
        if (Globals.SQLcon.init() == false)
        {
            return false;
        }
        try
        {
            sql.init();
        }
        catch (Exception ex)
        {
            decimal decUser = -1.0M;
            Functions.AddLogbuch(decUser, "init_con", ex.ToString());
            sql.Close();
            return false;
        }
        return true;
    }
    ///<summary>Functions / init_con</summary>
    ///<remarks>Initialisierung der SQL-Connection. Die einzelnen Parameter werden aus der 
    ///         config.ini ermittelt.</remarks>
    ///<return>bool for aktiv connection</param>
    public static bool init_conCOM(ref Globals._GL_SYSTEM GLSystem, ref List<string> listLogSys)
    {
        clsSQLCOM.Server = GLSystem.con_Server_COM;
        clsSQLCOM.Database = GLSystem.con_Database_COM;
        clsSQLCOM.User = GLSystem.con_UserDB_COM;
        clsSQLCOM.Password = GLSystem.con_PassDB_COM;
        string strTmp = string.Empty;
        try
        {
            if (Globals.SQLconCom.init() == false)
            {
                strTmp = Functions.GetLogString(strTmp, "[System-Start] - Datenbankverbindung konnte NICHT hergestellt werden....", false);
                listLogSys.Add(strTmp);

                string strInfoDB = "Server= " + GLSystem.con_Server_COM.ToString() + Environment.NewLine +
                                   "Database= " + GLSystem.con_Database_COM.ToString() + Environment.NewLine +
                                   "User= " + GLSystem.con_UserDB_COM.ToString() + Environment.NewLine +
                                   "Password= " + GLSystem.con_PassDB_COM.ToString() + Environment.NewLine;
                strTmp = string.Empty;
                strTmp = Functions.GetLogString(strTmp, "[System-Start] - Communicator Zugangsdaten:" + Environment.NewLine + strInfoDB, false);
                listLogSys.Add(strTmp);
                return false;
            }
        }
        catch (Exception ex)
        {
            decimal decUser = -1.0M;
            Functions.AddLogbuch(decUser, "init_con", ex.ToString());
            strTmp = string.Empty;
            strTmp = Functions.GetLogString(strTmp, "[System-Start] - Exception:" + Environment.NewLine + ex.ToString(), false);
            listLogSys.Add(strTmp);
            Globals.SQLconCom.Close();
            return false;
        }
        return true;
    }
    ///<summary>Functions / init_con</summary>
    ///<remarks>Initialisierung der SQL-Connection. Die einzelnen Parameter werden aus der 
    ///         config.ini ermittelt.</remarks>
    ///<return>bool for aktiv connection</param>
    public static bool init_conCALL(ref Globals._GL_SYSTEM GLSystem, ref List<string> listLogSys)
    {
        clsSQLCall.Server = GLSystem.con_Server_CALL;
        clsSQLCall.Database = GLSystem.con_Database_CALL;
        clsSQLCall.User = GLSystem.con_UserDB_CALL;
        clsSQLCall.Password = GLSystem.con_PassDB_CALL;

        string strTmp = string.Empty;
        try
        {
            if (Globals.SQLconCall.init() == false)
            {
                strTmp = Functions.GetLogString(strTmp, "[System-Start] - Datenbankverbindung konnte NICHT hergestellt werden....", false);
                listLogSys.Add(strTmp);

                string strInfoDB = "Server= " + GLSystem.con_Server_CALL.ToString() + Environment.NewLine +
                                   "Database= " + GLSystem.con_Database_CALL.ToString() + Environment.NewLine +
                                   "User= " + GLSystem.con_UserDB_CALL.ToString() + Environment.NewLine +
                                   "Password= " + GLSystem.con_PassDB_CALL.ToString() + Environment.NewLine;
                strTmp = string.Empty;
                strTmp = Functions.GetLogString(strTmp, "[System-Start] - CALL Zugangsdaten:" + Environment.NewLine + strInfoDB, false);
                listLogSys.Add(strTmp);
                return false;
            }
        }
        catch (Exception ex)
        {
            decimal decUser = -1.0M;
            Functions.AddLogbuch(decUser, "init_con", ex.ToString());
            strTmp = string.Empty;
            strTmp = Functions.GetLogString(strTmp, "[System-Start] - Exception:" + Environment.NewLine + ex.ToString(), false);
            listLogSys.Add(strTmp);
            Globals.SQLconCom.Close();
            return false;
        }

        return true;
    }

    public static bool init_conARCHIV(ref Globals._GL_SYSTEM GLSystem, ref List<string> listLogSys)
    {
        clsSQLARCHIVE.Server = GLSystem.con_Server_ARCHIVE;
        clsSQLARCHIVE.Database = GLSystem.con_Database_ARCHIVE;
        clsSQLARCHIVE.User = GLSystem.con_UserDB_ARCHIVE;
        clsSQLARCHIVE.Password = GLSystem.con_PassDB_ARCHIVE;

        string strTmp = string.Empty;
        try
        {
            if (Globals.SQLconArchive.init() == false)
            {
                strTmp = Functions.GetLogString(strTmp, "[System-Start] - Datenbankverbindung konnte NICHT hergestellt werden....", false);
                listLogSys.Add(strTmp);

                string strInfoDB = "Server= " + GLSystem.con_Server_ARCHIVE.ToString() + Environment.NewLine +
                                   "Database= " + GLSystem.con_Database_ARCHIVE.ToString() + Environment.NewLine +
                                   "User= " + GLSystem.con_UserDB_ARCHIVE.ToString() + Environment.NewLine +
                                   "Password= " + GLSystem.con_PassDB_ARCHIVE.ToString() + Environment.NewLine;
                strTmp = string.Empty;
                strTmp = Functions.GetLogString(strTmp, "[System-Start] - ARCHIVE Zugangsdaten:" + Environment.NewLine + strInfoDB, false);
                listLogSys.Add(strTmp);
                return false;
            }
        }
        catch (Exception ex)
        {
            decimal decUser = -1.0M;
            Functions.AddLogbuch(decUser, "init_con", ex.ToString());
            strTmp = string.Empty;
            strTmp = Functions.GetLogString(strTmp, "[System-Start] - Exception:" + Environment.NewLine + ex.ToString(), false);
            listLogSys.Add(strTmp);
            Globals.SQLconCom.Close();
            return false;
        }

        return true;
    }
    ///<summary>Functions / init_con</summary>
    ///<remarks>Initialisierung der SQL-Connection. Die einzelnen Parameter werden aus der 
    ///         config.ini ermittelt.</remarks>
    ///<return>bool for aktiv connection</param>
    public static bool init_conARCHIVE(ref Globals._GL_SYSTEM GLSystem, ref List<string> listLogSys)
    {
        clsSQLARCHIVE.Server = GLSystem.con_Server_ARCHIVE;
        clsSQLARCHIVE.Database = GLSystem.con_Database_ARCHIVE;
        clsSQLARCHIVE.User = GLSystem.con_UserDB_ARCHIVE;
        clsSQLARCHIVE.Password = GLSystem.con_PassDB_ARCHIVE;

        string strTmp = string.Empty;
        try
        {
            if (Globals.SQLcon.init() == false)
            {
                strTmp = Functions.GetLogString(strTmp, "[System-Start] - Datenbankverbindung konnte NICHT hergestellt werden....", false);
                listLogSys.Add(strTmp);

                string strInfoDB = "Server= " + GLSystem.con_Server_ARCHIVE.ToString() + Environment.NewLine +
                                   "Database= " + GLSystem.con_Database_ARCHIVE.ToString() + Environment.NewLine +
                                   "User= " + GLSystem.con_UserDB_ARCHIVE.ToString() + Environment.NewLine +
                                   "Password= " + GLSystem.con_PassDB_ARCHIVE.ToString() + Environment.NewLine;
                strTmp = string.Empty;
                strTmp = Functions.GetLogString(strTmp, "[System-Start] - ARCHIVE Zugangsdaten:" + Environment.NewLine + strInfoDB, false);
                listLogSys.Add(strTmp);
                return false;
            }
        }
        catch (Exception ex)
        {
            decimal decUser = -1.0M;
            Functions.AddLogbuch(decUser, "init_con", ex.ToString());
            strTmp = string.Empty;
            strTmp = Functions.GetLogString(strTmp, "[System-Start] - Exception:" + Environment.NewLine + ex.ToString(), false);
            listLogSys.Add(strTmp);
            Globals.SQLconCom.Close();
            return false;
        }

        return true;
    }
    ///<summary>Functions / GetDateFromLastDayOfCalWeek</summary>
    ///<remarks>Ermittelt das Datum für den 01. im Monat</remarks>
    ///<param name="myDateTime">Datum</param>
    ///<return name="Value">Datum</return>
    public static DateTime GetDateFromLastDayOfCalWeek(Int32 myKW, Int32 myYear)
    {
        // die 1. KW ist die mit mindestens 4 Tagen im Januar des nächsten Jahres
        DateTime dt = new DateTime(myYear, 1, 4);

        dt = dt.AddDays(--myKW * 7);

        // Beginn auf Montag setzten
        while (dt.DayOfWeek != DayOfWeek.Sunday)
        {
            DateTime dtTmp = dt.AddDays(1);
            dt = dtTmp;
        }
        return dt;
    }

    /// <summary>
    /// SetLZZtoString
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static string SetLZZtoString(DateTime dateTime)
    {
        string week = GetCalendarWeek(dateTime).ToString();
        if (week.Length == 1)
            week = "0" + week;
        return dateTime.Year.ToString() + week;
    }
    //*********************************************************************************************
    // *  2.                           CTR - Forms
    // * ******************************************************************************************/
    ///<summary>Functions / CheckDirectory</summary>
    ///<remarks></remarks>
    ///<param name="strEingabe"></param>
    ///<return name="eingabeOK"></return>
    public static void CheckDirectory(string myCheckPath)
    {
        if (!Directory.Exists(myCheckPath))
        {
            try
            {
                Directory.CreateDirectory(myCheckPath);
            }
            catch (Exception ex)
            {
            }
        }
    }

    /***********************************************************************************************
     *3.                                      Formatierungen                                     
     * ********************************************************************************************/
    ///<summary>Functions / FromatDecimal</summary>
    ///<remarks>Formatiert einen Decimalwert in 1.234,56 und gibt diesen Wert als String zurück.</remarks>
    ///<param name="Value">Decimalwert</param>
    ///<return name="Value">String (Bsp.: 1.234,56)</return>
    public static string FormatDecimal(decimal Value)
    {
        return Value.ToString("#,##0.00");
    }
    ///<summary>Functions / FromatDecimal</summary>
    ///<remarks>Formatiert einen Decimalwert in 1.234,56 und gibt diesen Wert als String zurück.</remarks>
    ///<param name="Value">Decimalwert</param>
    ///<return name="Value">String (Bsp.: 1.234,56)</return>
    public static string FormatDecimalNoDiggits(decimal Value)
    {
        return Value.ToString("##0");
    }
    ///<summary>Functions / FromatDecimal</summary>
    ///<remarks>Formatiert einen Decimalwert in 1.234,5678 und gibt diesen Wert als String zurück. Geldwerte werden 4 stellig gespeichert.</remarks>
    ///<param name="Value">Decimalwert</param>
    ///<return name="Value">String (Bsp.: 1.234,5678)</return>
    public static string FormatDecimalMoney(decimal Value)
    {
        return Value.ToString("#,##0.0000");
    }
    ///<summary>Functions / FromatDecimalVersion</summary>
    ///<remarks>Liefert das Versionsformat für LVS als String zurück.
    ///         Version: 1.234</remarks>
    ///<param name="Value">Decimalwert</param>
    ///<return name="Value">String (Bsp.: 1.234)</return>
    public static string FormatDecimalVersion(decimal Value)
    {
        return Value.ToString("0,000");
    }
    ///<summary>Functions / GetFirstDayOfMonth</summary>
    ///<remarks>Ermittelt das Datum für den 01. im Monat
    ///         MSSQL Server kann nicht kleiner 01.01.1753</remarks>
    ///<param name="myDateTime">Datum</param>
    ///<return name="Value">Datum</return>
    public static DateTime GetFirstDayOfMonth(DateTime myDateTime)
    {
        DateTime dtTmp = DateTime.Now;
        DateTime.TryParse("01." + myDateTime.Month + "." + myDateTime.Year, out dtTmp);
        if (dtTmp < Convert.ToDateTime("01.01.1753"))
        {
            dtTmp = Convert.ToDateTime("01.01.1753");
        }
        return dtTmp;
    }
    ///<summary>Functions / GetFirstDayOfMonth</summary>
    ///<remarks>Ermittelt das Datum für den 01. im Monat
    ///         MSSQL Server kann nicht kleiner 01.01.1753</remarks>
    ///<param name="myDateTime">Datum</param>
    ///<return name="Value">Datum</return>
    public static DateTime GetLastDayOfMonth(DateTime myDateTime)
    {
        DateTime dtTmp = new DateTime(myDateTime.Year, myDateTime.Month, DateTime.DaysInMonth(myDateTime.Year, myDateTime.Month));
        if (dtTmp < Convert.ToDateTime("01.01.1753"))
        {
            dtTmp = Convert.ToDateTime("01.01.1753");
        }
        return dtTmp;
    }
    /// <summary>Functions / GetLogString </summary>
    /// <param name="LogString"></param>
    /// <param name="LogAdd"></param>
    /// <returns></returns>
    public static string GetLogString(string LogString, string LogAdd, bool bNewLine)
    {
        LogString = DateTime.Now.ToString() + " - " + LogAdd;
        if (bNewLine)
        {
            LogString = LogString + Environment.NewLine;
        }
        return LogString;
    }
    ///<summary>Functions / CreateArtikelIDRef_VW</summary>
    ///<remarks>ArtikelIDRef für VW 
    ///            Lieferantennummer (9stellig)+
    ///            LVSNR ( 8Stellig)+
    ///            Produktionsnummer (9stellig)</remarks>
    public static string CreateArtikelIDRef_VW(string myLieferangenNr, string myLVSNr, string myProdNr)
    {
        string strReturn = string.Empty;

        string strLieferantennummer = "000000000";
        string strLVSNr = "00000000";
        string strProduktionsnummer = "000000000";

        //1. Teil Lieferantennummer 
        strLieferantennummer = strLieferantennummer + myLieferangenNr.Trim();
        strLieferantennummer = strLieferantennummer.Substring(strLieferantennummer.Length - 9);
        //2. Teil LVSNR
        strLVSNr = strLVSNr + myLVSNr.Trim();
        strLVSNr = strLVSNr.Substring(strLVSNr.Length - 8);

        //3. Teil Produktionsnummer
        strProduktionsnummer = strProduktionsnummer + myProdNr.Trim();
        strProduktionsnummer = strProduktionsnummer.Substring(strProduktionsnummer.Length - 9);

        string strTmp = strLieferantennummer + strLVSNr + strProduktionsnummer;
        if (strTmp.Length == 26)
        {
            strReturn = strTmp;
        }
        else
        {
            if (strTmp.Length > 26)
            {
                strTmp = strTmp.Substring(strTmp.Length - 26);
            }
            if (strTmp.Length < 26)
            {
                strTmp = strTmp + "0000000000";
                strTmp = strTmp.Substring(1, 26);
            }
            strReturn = strTmp;
        }
        strReturn = strTmp;
        return strReturn;
    }
    ////-----------------
    //// ZAHLEN
    ////
    ////------------ Ist die Eingabe ein Zahlenwert -----------
    ////
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

    ////
    ///****************************************************************************
    // * 
    // *                              DataTable 
    // * 
    // * **************************************************************************/
    public static bool IsRowInDataTable(DataTable dt, DataRow row)
    {
        bool result = true;
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
    ////
    ////------------ Eintrag ins Logbuch ------------------------------
    ////
    public static void AddLogbuch(Decimal myBenutzerID, string myAktion, string myBeschreibung)
    {
        clsLogbuch log = new clsLogbuch();
        log.BenutzerID = myBenutzerID;
        log.Aktion = myAktion;
        log.Beschreibung = myBeschreibung;
        log.LogbuchInsert();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="combo"></param>
    /// <param name="strValue"></param>
    public static void SetComboToSelecetedItem(ref ComboBox combo, string strValue)
    {
        Int32 iTmp = combo.FindString(strValue);
        if (iTmp != combo.SelectedIndex)
        {
            combo.SelectedIndex = iTmp;
        }
    }
    //
    //
    //
    public static string CheckAndGetDimension(decimal iDimension)
    {
        //gedacht für die Abmessungen, es sollen nur Abmessungen >0 angegeben werden
        string strDimension = string.Empty;
        decimal tmpDec = 0.0M;
        if (decimal.TryParse(iDimension.ToString(), out tmpDec))
        {
            if (tmpDec > 0.0M)
            {
                strDimension = tmpDec.ToString();
            }
        }
        return strDimension;
    }
    ////
    ////
    ////
    public static Int32 GetCalendarWeek(DateTime date)
    {
        // Aktuelle Kultur ermitteln
        CultureInfo currentCulture = CultureInfo.CurrentCulture;

        // Aktuellen Kalender ermitteln
        Calendar calendar = currentCulture.Calendar;

        // Kalenderwoche über das Calendar-Objekt ermitteln
        int calendarWeek = calendar.GetWeekOfYear(date,
           currentCulture.DateTimeFormat.CalendarWeekRule,
           currentCulture.DateTimeFormat.FirstDayOfWeek);

        // Überprüfen, ob eine Kalenderwoche größer als 52
        // ermittelt wurde und ob die Kalenderwoche des Datums
        // in einer Woche 2 ergibt: In diesem Fall hat
        // GetWeekOfYear die Kalenderwoche nicht nach ISO 8601 
        // berechnet (Montag, der 31.12.2007 wird z. B.
        // fälschlicherweise als KW 53 berechnet). 
        // Die Kalenderwoche wird dann auf 1 gesetzt
        if (calendarWeek > 52)
        {
            date = date.AddDays(7);
            int testCalendarWeek = calendar.GetWeekOfYear(date,
               currentCulture.DateTimeFormat.CalendarWeekRule,
               currentCulture.DateTimeFormat.FirstDayOfWeek);
            if (testCalendarWeek == 2)
                calendarWeek = 1;
        }
        return calendarWeek;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static int TotalMonths(DateTime start, DateTime end)
    {
        int iTmp1 = (start.Year * 12 + start.Month);
        int iTMp2 = (end.Year * 12 + end.Month);
        int iReturn = iTmp1 - iTMp2;
        return iReturn;
    }




}


