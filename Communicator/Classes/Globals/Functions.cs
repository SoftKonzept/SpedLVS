using LVS;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Windows.Forms;



public static class FunctionsCom
{


    /*********************************************************************************************
     *  1.                           SQL - Connection
     * ******************************************************************************************/
    ///<summary>Functions / init_con</summary>
    ///<remarks>Initialisierung der SQL-Connection. Die einzelnen Parameter werden aus der 
    ///         config.ini ermittelt.</remarks>
    ///<return>bool for aktiv connection</param>
    //public static bool init_conLVS(ref Globals._GL_SYSTEM GLSystem)
    //{
    //    Globals.SQLconLvs.Server = GLSystem.con_LVSServer;
    //    Globals.SQLconLvs.Database = GLSystem.con_LVSDatabase;
    //    Globals.SQLconLvs.User = GLSystem.con_LVSUserDB;
    //    Globals.SQLconLvs.Password = GLSystem.con_LVSPassDB;


    //    if (Globals.SQLconLvs.init() == false)
    //    {
    //        return false;
    //    }
    //    try
    //    {
    //        Globals.SQLconLvs.Open();
    //        Globals.SQLconLvs.Close();
    //    }
    //    catch (Exception)
    //    {
    //        Globals.SQLconLvs.Close();
    //        return false;
    //    }
    //    return true;
    //}

    ///<summary>Functions / init_con</summary>
    ///<remarks>Initialisierung der SQL-Connection. Die einzelnen Parameter werden aus der 
    ///         config.ini ermittelt.</remarks>
    ///<return>bool for aktiv connection</param>
    //public static bool init_conCOM(ref Globals._GL_SYSTEM GLSystem)
    //{
    //    Globals.SQLconCom.Server = GLSystem.con_ComServer;
    //    Globals.SQLconCom.Database = GLSystem.con_ComDatabase;
    //    Globals.SQLconCom.User = GLSystem.con_ComUserDB;
    //    Globals.SQLconCom.Password = GLSystem.con_ComPassDB;


    //    if (Globals.SQLconCom.init() == false)
    //    {
    //        return false;
    //    }
    //    try
    //    {
    //        Globals.SQLconCom.Open();
    //        Globals.SQLconCom.Close();
    //    }
    //    catch (Exception)
    //    {
    //        Globals.SQLconCom.Close();
    //        return false;
    //    }
    //    return true;
    //}




    //
    /*********************************************************************************************
     *  2.                           CTR - Forms
     * ******************************************************************************************/
    ///<summary>Functions / frm_IsFormAlreadyOpen</summary>
    ///<remarks>Prüft, ob die übergebene Form/CTR bereits geöffnet ist und gibt diese zurück.</remarks>
    ///<return>geöffnete CTR / Form</param>
    public static Form frm_IsFormTypeAlreadyOpen(Type FormType)
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
    ///<summary>Functions / frm_IsFormAlreadyOpen</summary>
    ///<remarks>Prüft, ob die übergebene Form/CTR bereits geöffnet ist und gibt diese zurück.</remarks>
    ///<return>geöffnete CTR / Form</param>
    public static Form frm_IsFormAlreadyOpen(Type myFormType)
    {
        foreach (Form OpenForm in Application.OpenForms)
        {
            if (OpenForm.GetType() == myFormType)
            {
                if (OpenForm.Equals(myFormType))
                {
                    return OpenForm;
                }
            }
        }
        return null;
    }

    ///<summary>Functions / frm_FormClose</summary>
    ///<remarks>Schließt / Beendet die übergebene Form.</remarks>
    ///<param name="FormType">Übergabe Form</param>
    public static void frm_FormTypeClose(Type myFormType)
    {
        foreach (Form OpenForm in Application.OpenForms)
        {
            if (OpenForm.GetType() == myFormType)
            {
                OpenForm.Close();
                break;
            }
        }
    }
    ///<summary>Functions / frm_FormClose</summary>
    ///<remarks>Schließt / Beendet die übergebene Form.</remarks>
    ///<param name="FormType">Übergabe Form</param>
    public static void frm_FormClose(Type myFormType, string myFrmName)
    {
        foreach (Form OpenForm in Application.OpenForms)
        {
            if (OpenForm.GetType() == myFormType)
            {
                if (OpenForm.Name.ToString().ToUpper() == myFrmName.ToUpper())
                {
                    OpenForm.Close();
                    break;
                }
            }
        }
    }



    ///<summary>Functions / GetSearchFilterString</summary>
    ///<remarks>Ermittel aufgrund des Celltyps den korrekten Filter</param>
    public static string GetSearchFilterString(ref ToolStripComboBox myTSCB, string myColName, string mySearchTxt)
    {
        string strFilter = string.Empty;
        if (myColName != string.Empty)
        {
            if (mySearchTxt != string.Empty)
            {
                DataRowView rowv = (DataRowView)myTSCB.SelectedItem;
                string strColName = rowv["ColumnName"].ToString();
                string strType = rowv["Type"].ToString();
                switch (strType)
                {
                    case "System.Decimal":
                        if (Functions.CheckNum(mySearchTxt))
                        {
                            decimal decTmp = 0;
                            Decimal.TryParse(mySearchTxt, out decTmp);
                            mySearchTxt = decTmp.ToString().Replace(",", ".");
                            strFilter = myColName + " >= " + mySearchTxt;
                        }
                        break;
                    case "System.Int32":
                        if (Functions.CheckNum(mySearchTxt))
                        {
                            strFilter = myColName + " >= " + mySearchTxt;
                        }
                        break;

                    case "System.String":
                        strFilter = myColName + " LIKE '" + mySearchTxt + "%'";
                        break;

                    case "System.DateTime":
                        strFilter = myColName + " LIKE '" + mySearchTxt + "%'";
                        break;
                }
            }
        }
        return strFilter;
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
    ///<remarks>Liefert das Versionsformat für Sped4 als String zurück.
    ///         Version: 1.234</remarks>
    ///<param name="Value">Decimalwert</param>
    ///<return name="Value">String (Bsp.: 1.234)</return>
    public static string FormatDecimalVersion(decimal Value)
    {
        return Value.ToString("0,000");
    }
    ///<summary>Functions / FromatShortDateTime</summary>
    ///<remarks>Liefert folgendes DatetimeFormat als String:
    ///         "MM.dd.yyyy HH:mm"</remarks>
    ///<param name="Value">DateTime</param>
    ///<return name="Value">String ("MM.dd.yyyy HH:mm")</return>
    public static string FormatShortDateTime(DateTime Value)
    {
        return Value.ToString("dd.MM.yyyy HH:mm");
    }
    ///<summary>Functions / FromatShortTime</summary>
    ///<remarks>Liefert folgendes DatetimeFormat als String:
    ///         "HH:mm"</remarks>
    ///<param name="Value">DateTime</param>
    ///<return name="Value">String ("HH:mm")</return>
    public static string FormatShortTime(DateTime Value)
    {
        return Value.ToString("HH:mm");
    }
    ///<summary>Functions / GetStrTimeZF</summary>
    ///<remarks>Anhand der Übergabeparameter wird eine Uhrzeit zusammengesetzt ( Std und Min ) für 
    ///         die Uhrzeit des Zeitfensters in der Disposition.</remarks>
    ///<param name="numStd">Stunden als Decimal</param>
    ///<param name="numMin">Minuten als Decimal</param>
    ///<return name="Value">DateTime ("MM.dd.yyyy HH:mm")</return>
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
    // Baustelle mr
    //---------------- Zeitangabe Studnen und Minunten ----------
    //
    public static string FormatToHHMM(DateTime time)
    {
        string returnTime = string.Empty;

        //HH
        if (time.Hour < 10)
        {
            returnTime = returnTime + "0" + time.Hour.ToString();
        }
        else
        {
            returnTime = returnTime + time.Hour.ToString();
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
    ///<summary>Functions / GetFirstDayOfMonth</summary>
    ///<remarks>Ermittelt das Datum für den 01. im Monat</remarks>
    ///<param name="myDateTime">Datum</param>
    ///<return name="Value">Datum</return>
    public static DateTime GetFirstDayOfMonth(DateTime myDateTime)
    {
        DateTime dtTmp = DateTime.Now;
        DateTime.TryParse("01." + myDateTime.Month + "." + myDateTime.Year, out dtTmp);
        return dtTmp;
    }
    ///<summary>Functions / GetFirstDayOfMonth</summary>
    ///<remarks>Ermittelt das Datum für den 01. im Monat</remarks>
    ///<param name="myDateTime">Datum</param>
    ///<return name="Value">Datum</return>
    public static DateTime GetLastDayOfMonth(DateTime myDateTime)
    {
        DateTime dtTmp = new DateTime(myDateTime.Year, myDateTime.Month, DateTime.DaysInMonth(myDateTime.Year, myDateTime.Month));
        return dtTmp;
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
    /****************************************************************************
    * 4.                Funktionen - Auswertungen - Zählen - Berechnen
    ****************************************************************************/
    ///<summary>Functions / CheckEingabeDecimal</summary>
    ///<remarks>Checkt die Eingabe auf Decimal und wandelt die Eingabe um.</remarks>
    ///<param name="Value">Decimalwert</param>
    ///<return name="Value">String (Bsp.: 1.234,5678)</return>
    public static decimal CheckEingabeDecimal(string myInput)
    {
        decimal decTmp = 0;
        Decimal.TryParse(myInput, out decTmp);
        return decTmp;
    }

    //
    //----------- Check Datum Differenz --------------
    //
    public static bool CheckDateDifferenceTillToday(DateTime DateToCheck, Int32 Tage)
    {
        bool dif = false;
        TimeSpan tmpDif = DateToCheck.Subtract(DateTime.Today);

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
    ///<summary>Functions / CheckForInt</summary>
    ///<remarks>Prüft einen String (Zahlen) auf Punkt und Komma</remarks>
    ///<param name="strEingabe">Stunden als Decimal</param>
    ///<return name="eingabeOK">Bool</return>
    public static bool CheckForInt(string strEingabe)
    {
        bool eingabeOK = true;
        char[] KommaPunkt = { ',', '.' };
        if (strEingabe.IndexOfAny(KommaPunkt) > -1)
        {
            eingabeOK = false;
        }
        return eingabeOK;
    }



    //-----------------
    // ZAHLEN
    //
    //------------ Ist die Eingabe ein Zahlenwert -----------
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


    /*************************************************************************************
     *                              Datatable
     * **********************************************************************************/
    ///<summary>Functions / FilterDataTable</summary>
    ///<remarks></remarks>
    ///<param name="strEingabe"></param>
    ///<return name="eingabeOK"></return>
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
                        rows = dt.Select(Columnname + " = '" + SearchText + "'", Columnname);
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
    ///<summary>Functions / GetMonatsnamen</summary>
    ///<remarks></remarks>
    ///<param name="strEingabe"></param>
    ///<return name="eingabeOK"></return>
    public static Array GetMonatsnamen()
    {
        string[] Monatsnamen = new string[] {"Januar",
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

    /*****************************************************************************************************
     * 
     *                      Dantenbankeinträge - SQL Anweisungen
     * 
     * **************************************************************************************************/

    //
    //------------ Eintrag ins Logbuch ------------------------------
    //
    public static void AddLogbuch(Decimal myBenutzerID, string myTyp, string myLogText)
    {


        //Baustelle
        /****
      clsLogbuch log = new clsLogbuch();
      log.BenutzerID = myBenutzerID;
      log.Aktion = myAktion;
      log.Beschreibung = myBeschreibung;
      log.LogbuchInsert();
         * ***/
    }





    //
    //
    //
    public static String CutString(string strToCut)
    {
        string tmp = string.Empty;
        if (strToCut != string.Empty)
        {
            tmp = strToCut.ToString().Trim();
        }
        return tmp;
    }
    //
    //-------------- Trimm TextBox ----------------
    //
    public static void TrimmTexBox(ref TextBox tb)
    {
        tb.Text = tb.Text.ToString().Trim();
    }
    //
    //
    //
    /***********************************************************************
     * 
     *              DatagridView - Funktionen
     * 
     * *********************************************************************/



    //
    //
    //
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

    public static void SetKWValue(ref NumericUpDown nudValue, DateTime dateTime)
    {
        Int32 iKW = GetCalendarWeek(dateTime);
        Decimal decVal = Convert.ToDecimal(iKW);
        nudValue.Value = decVal;
    }


    /********************************************************************************
     *                      Dateien
     * *****************************************************************************/
    ///<summary>Functions / IsFileLocked</summary>
    ///<remarks></remarks>
    ///<param name="strEingabe"></param>
    ///<return name="eingabeOK"></return>
    public static bool IsFileLocked(string filePath)
    {
        FileStream stream = null;
        bool bIsLocked = false;
        try
        {
            using (stream = new FileStream(filePath, FileMode.OpenOrCreate))
            {

            }
            bIsLocked = false;
        }
        catch (IOException e)
        {
            string strTmp = e.ToString();
            bIsLocked = true;
        }
        finally
        {
            if (stream != null)
                stream.Close();
        }
        return bIsLocked;
    }

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
                //Baustelle Error
                /***
                clsError error = new clsError();
                //error._GL_User = this.;
                error.Code = clsError.code1_501;
                error.Aktion = "Directory.CreateDirectory(@'C:\\LVS\\Export)";
                error.exceptText = ex.ToString();
                error.WriteError();
                    * ****/
                string strError = ex.ToString();
            }
        }
    }
    ///<summary>Functions / MoveAndDeleteFile</summary>
    ///<remarks></remarks>
    ///<param name="strEingabe"></param>
    ///<return name="eingabeOK"></return>
    public static void CopyFile(string myDestFilePath, string mySourceFilePath, string myErrorPath)
    {
        bool bCanDelete = false;
        try
        {
            //Datum wird an die Datei gehängt
            //myDestFilePath = myDestFilePath;
            //Datei wird kopiert
            System.IO.File.Copy(mySourceFilePath, myDestFilePath, true);
            bCanDelete = true;
        }
        catch (Exception ex)
        {
            try
            {
                //Datum wird an die Datei gehängt
                //myErrorPath = myErrorPath;
                //Datei wird kopiert
                System.IO.File.Copy(mySourceFilePath, myErrorPath, true);
                bCanDelete = true;
                //Baustelle Error Mail versenden
                string strError = ex.ToString();

            }
            catch (Exception exx)
            {
                string strError = exx.ToString();
            }
        }
        finally
        {
            //wenn das Kopieren funktioniert, dann wird das File gelöscht
            if (bCanDelete)
            {
                try
                {
                    //System.IO.File.Delete(mySourceFilePath);
                }
                catch (Exception e)
                {
                    string st = e.ToString();
                }
            }
        }
    }
    ///<summary>Functions / GetMaxArray</summary>
    ///<remarks></remarks>
    public static Int32 GetMaxArray(int[] array)
    {
        Int32 iRetValue = 0;
        for (Int32 i = 0; i < array.Length; i++)
        {
            if (array[i] > iRetValue)
            {
                iRetValue = array[i];
            }
        }
        return iRetValue;
    }
    ///<summary>Functions / MoveAndDeleteFile</summary>
    ///<remarks></remarks>
    ///<param name="strEingabe"></param>
    ///<return name="eingabeOK"></return>
    public static bool MoveAndDeleteFile(string myDestFilePath, string mySourceFilePath, string myErrorPath, ref string strErrorText)
    {
        bool bCanDelete = false;
        try
        {
            //Datum wird an die Datei gehängt
            //myDestFilePath = myDestFilePath + "@" + DateTime.Now.ToString("yyyyMMdd_hhmm_ss");
            //Datei wird kopiert
            System.IO.File.Copy(mySourceFilePath, myDestFilePath, true);
            bCanDelete = true;
        }
        catch (Exception ex)
        {

            strErrorText += "ERROR Function MoveAndDeleteFile " + Environment.NewLine;
            strErrorText += String.Format("{0}\t{1}", "DestinationFile:", "[ " + myDestFilePath + " ]") + Environment.NewLine;
            strErrorText += String.Format("{0}\t{1}", "SourceFilePath:", "[ " + mySourceFilePath + " ]") + Environment.NewLine;
            strErrorText += String.Format("{0}\t{1}", "ErrorPath:", "[ " + myErrorPath + " ]") + Environment.NewLine;

            //Exception 1
            strErrorText += Environment.NewLine;
            strErrorText += "Exception 1 :" + Environment.NewLine;
            strErrorText += ex.ToString() + Environment.NewLine;
            try
            {
                //Datum wird an die Datei gehängt
                //myErrorPath = myErrorPath;
                //Datei wird kopiert
                System.IO.File.Copy(mySourceFilePath, myErrorPath, true);
                bCanDelete = true;
            }
            catch (Exception exx)
            {
                bCanDelete = false;
                //Exception 2
                strErrorText += Environment.NewLine;
                strErrorText += "Exception 2 :" + Environment.NewLine;
                strErrorText += exx.ToString() + Environment.NewLine;
            }
        }
        finally
        {
            //wenn das Kopieren funktioniert, dann wird das File gelöscht
            if (bCanDelete)
            {
                try
                {
                    //-- es muss die EBSDIC und die ASCII Datei gelöscht werden
                    //-- ASCII
                    System.IO.File.Delete(mySourceFilePath);
                    //-- EBSDIC liegt in Eingang
                    string strPath = Path.GetDirectoryName(mySourceFilePath);
                    string strFile = Path.GetFileName(mySourceFilePath);
                    string strPathToEingang = helper_IOFile.EntferneOrdnerAusPfad(strPath, "Backup");
                    string strFileWithoutHash = strFile;

                    int index = strFile.IndexOf('#');
                    if (index >= 0)
                    {
                        strFileWithoutHash = strFile.Substring(0, index);
                    }
                    string strFilePathToDeleteEBSCDIC = Path.Combine(strPathToEingang, strFileWithoutHash);
                    if (File.Exists(strFilePathToDeleteEBSCDIC))
                    {
                        System.IO.File.Delete(strFilePathToDeleteEBSCDIC);
                    }
                }
                catch (Exception e)
                {
                    //string st = e.ToString();
                    bCanDelete = false;
                    //Exception 3
                    strErrorText += Environment.NewLine;
                    strErrorText += "Exception 3 :" + Environment.NewLine;
                    strErrorText += e.ToString() + Environment.NewLine;
                }
            }
        }
        return bCanDelete;
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
    ///<summary>Functions / MoveAndDeleteFile</summary>
    ///<remarks></remarks>
    ///<param name="strEingabe"></param>
    ///<return name="eingabeOK"></return>
    public static void MoveFile(string myDestFilePath, string mySourceFilePath, bool bAddDateTimeStamp)
    {
        string strErrorPath = mySourceFilePath + "\\ERROR";
        bool bCanDelete = false;
        try
        {
            if (bAddDateTimeStamp)
            {
                //Datum wird an die Datei gehängt
                myDestFilePath = myDestFilePath + "@" + DateTime.Now.ToString("yyyyMMdd_hhmm_ss");
            }
            //Datei wird kopiert
            System.IO.File.Copy(mySourceFilePath, myDestFilePath, true);
            bCanDelete = true;
        }
        catch (Exception ex)
        {
            string strError = ex.ToString();
            try
            {
                //Datum wird an die Datei gehängt
                strErrorPath = strErrorPath + Path.GetFileName(mySourceFilePath) + "@" + DateTime.Now.ToString("yyyyMMdd_hhmm_ss");
                //Datei wird kopiert
                System.IO.File.Copy(mySourceFilePath, strErrorPath, true);
                bCanDelete = true;
                //Baustelle Error Mail versenden

            }
            catch (Exception exx)
            {
                strError = exx.ToString();
            }
        }
        finally
        {
            //wenn das Kopieren funktioniert, dann wird das File gelöscht
            if (bCanDelete)
            {
                try
                {
                    System.IO.File.Delete(mySourceFilePath);
                }
                catch (Exception e)
                {
                    string st = e.ToString();
                }
            }
        }
    }

}


