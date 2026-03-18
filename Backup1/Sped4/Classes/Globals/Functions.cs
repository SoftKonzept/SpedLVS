using LVS;
using LVS.Dokumente;
using Sped4;
using Sped4.Classes;
using Sped4.Controls;
using Sped4.Struct;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;


public static class Functions
{

    ///<summary>Functions</summary>
    ///<remarks>Die statische Klasse Funktionen beinhaltet diverse Hilfsfunktionen.
    ///         Aufbau:
    ///         1. SQL - Connection        
    ///         2. CTR - Forms
    ///         3. Formatierungen
    ///         4. 
    /// </remarks>

    /*********************************************************************************************
     *  1.                           SQL - Connection
     * ******************************************************************************************/
    ///<summary>Functions / init_con</summary>
    ///<remarks>Initialisierung der SQL-Connection. Die einzelnen Parameter werden aus der 
    ///         config.ini ermittelt.</remarks>
    ///<return>bool for aktiv connection</param>
    //public static bool init_con(ref Globals._GL_SYSTEM GLSystem)
    //{
    //    clsSQLcon.Server = GLSystem.con_Server;
    //    clsSQLcon.Database = GLSystem.con_Database;
    //    clsSQLcon.User = GLSystem.con_UserDB;
    //    clsSQLcon.Password = GLSystem.con_PassDB;

    //    if (Globals.SQLcon.init() == false)
    //    {
    //        return false;
    //    }
    //    try
    //    {
    //        Globals.SQLcon.Open();
    //        Globals.SQLcon.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        decimal decUser = -1.0M;
    //        Functions.AddLogbuch(decUser, "init_con", ex.ToString());
    //        Globals.SQLcon.Close();
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
    //    clsSQLCOM.Server = GLSystem.con_Server_COM;
    //    clsSQLCOM.Database = GLSystem.con_Database_COM;
    //    clsSQLCOM.User = GLSystem.con_UserDB_COM;
    //    clsSQLCOM.Password = GLSystem.con_PassDB_COM;

    //    if (Globals.SQLconCom.init() == false)
    //    {
    //        return false;
    //    }
    //    try
    //    {
    //        Globals.SQLconCom.Open();
    //        Globals.SQLconCom.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        decimal decUser = -1.0M;
    //        Functions.AddLogbuch(decUser, "init_con", ex.ToString());
    //        Globals.SQLconCom.Close();
    //        return false;
    //    }
    //    return true;
    //}
    ///<summary>Functions / init_con</summary>
    ///<remarks>Initialisierung der SQL-Connection. Die einzelnen Parameter werden aus der 
    ///         config.ini ermittelt.</remarks>
    ///<return>bool for aktiv connection</param>
    //public static bool init_conCALL(ref Globals._GL_SYSTEM GLSystem)
    //{
    //    clsSQLCall.Server = GLSystem.con_Server_CALL;
    //    clsSQLCall.Database = GLSystem.con_Database_COM;
    //    clsSQLCall.User = GLSystem.con_UserDB_COM;
    //    clsSQLCall.Password = GLSystem.con_PassDB_COM;

    //    if (Globals.SQLconCall.init() == false)
    //    {
    //        return false;
    //    }
    //    try
    //    {
    //        Globals.SQLconCall.Open();
    //        Globals.SQLconCall.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        decimal decUser = -1.0M;
    //        Functions.AddLogbuch(decUser, "init_con", ex.ToString());
    //        Globals.SQLconCom.Close();
    //        return false;
    //    }
    //    return true;
    //}
    //
    /*********************************************************************************************
     *  2.                           CTR - Forms
     * ******************************************************************************************/
    ///<summary>Functions / SetDateTimePickerValue</summary>
    ///<remarks>Der übergebene Datumswert wird mit dem Min/Maxwert des Datetimepickers verglichen
    ///         und dann der entsprechende Datumswert gesetzt</remarks>
    public static void SetDateTimePickerValue(ref DateTimePicker dtp, DateTime dtValue)
    {
        DateTime dtTmp = clsSystem.const_DefaultDateTimeValue_Min;
        DateTime.TryParse(dtValue.ToString(), out dtTmp);
        if (dtValue <= dtp.MinDate)
        {
            dtp.Value = dtp.MinDate;
        }
        else
        {
            if (dtValue >= dtp.MaxDate)
            {
                dtp.Value = dtp.MaxDate;
            }
            else
            {
                dtp.Value = dtValue;
            }
        }
    }
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
    ///<summary>Functions / IsFormAlreadyOpen</summary>
    ///<remarks>Prüft, ob die übergebene Form/CTR bereits geöffnet ist und gibt diese zurück.</remarks>
    ///<return>geöffnete CTR / Form</param>
    public static ctrAufträge IsCtrAlreadyOpen(ref ctrMenu ctrMenu)
    {
        for (Int32 i = 0; i <= ctrMenu.ParentForm.Controls.Count - 1; i++)
        {
            if (ctrMenu.ParentForm.Controls[i].GetType() == typeof(ctrAufträge))
            {
                return (ctrAufträge)ctrMenu.ParentForm.Controls[i];
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
    ///<summary>Functions / tscbMandanten_SelectedIndexChanged</summary>
    ///<remarks>Initialisiert Fahrzeugcombobox.</remarks>
    public static void InitComboFahrzeuge(Globals._GL_USER myGLUser, ref ComboBox myCombo)
    {
        DataTable dtFahrzeuge = new DataTable("Fahrzeuge");
        dtFahrzeuge = clsFahrzeuge.GetVehicleListZM(myGLUser);

        DataRow row = dtFahrzeuge.NewRow();
        row["ID"] = ctrEinlagerung.const_cbFahrzeugValue_Waggon;
        row["KFZ"] = ctrEinlagerung.const_cbFahrzeugText_Waggon;
        dtFahrzeuge.Rows.InsertAt(row, 0);

        row = dtFahrzeuge.NewRow();
        row["ID"] = ctrEinlagerung.const_cbFahrzeugValue_Fremdfahrzeug;
        row["KFZ"] = ctrEinlagerung.const_cbFahrzeugText_Fremdfahrzeug;
        dtFahrzeuge.Rows.InsertAt(row, 1);

        row = dtFahrzeuge.NewRow();
        row["ID"] = ctrEinlagerung.const_cbFahrzeugValue_Schiff;
        row["KFZ"] = ctrEinlagerung.const_cbFahrzeugText_Schiff;
        dtFahrzeuge.Rows.InsertAt(row, 2);

        myCombo.DataSource = dtFahrzeuge.DefaultView;
        myCombo.DisplayMember = "KFZ";
        myCombo.ValueMember = "ID";
        //myCombo.SelectedIndex = 1;
    }
    ///<summary>Functions / tscbMandanten_SelectedIndexChanged</summary>
    ///<remarks>Initialisiert Fahrzeugcombobox.</remarks>
    public static void InitComboTrailer(Globals._GL_USER myGLUser, ref ComboBox myCombo)
    {
        DataTable dtTrailer = new DataTable("Trailer");
        dtTrailer = clsFahrzeuge.GetFahrzeuge_AufliegerforCombo(myGLUser);

        DataRow row = dtTrailer.NewRow();

        row = dtTrailer.NewRow();
        row["ID"] = ctrEinlagerung.const_cbFahrzeugValue_Fremdfahrzeug;
        row["KFZ"] = ctrEinlagerung.const_cbFahrzeugText_Fremdfahrzeug;
        dtTrailer.Rows.InsertAt(row, 0);

        myCombo.DataSource = dtTrailer.DefaultView;
        myCombo.DisplayMember = "KFZ";
        myCombo.ValueMember = "ID";
        //myCombo.SelectedIndex = 0;
    }
    ///<summary>Functions / tscbMandanten_SelectedIndexChanged</summary>
    ///<remarks>Initialisiert Fahrzeugcombobox.</remarks>
    public static void InitComboViews(Globals._GL_SYSTEM myGLSystem, ref ToolStripComboBox myCombo, string katName, bool bPrint = false)
    {
        if (myCombo.Items.Count == 0)
        {
            myCombo.Enabled = false;
            Dictionary<string, List<string>> dicView;
            if (bPrint == false)
            {
                myGLSystem.DictViews.TryGetValue(katName, out dicView);
            }
            else
            {
                myGLSystem.DictPrintViews.TryGetValue(katName, out dicView);
            }
            if (dicView != null)
            {
                myCombo.Items.AddRange(dicView.Keys.ToArray());
                if (myCombo.Items.Count > 0)
                {
                    myCombo.Enabled = true;
                    myCombo.SelectedIndex = 0;
                }
            }
        }
    }
    ///<summary>Functions / InitComboMandanten</summary>
    ///<remarks>Ermittel die aktiven Mandanten und füllt eine Combobox mit dem Ergebnis. Ist nur ein Mandant vorhanden,
    ///         so wird dieser direkt fest als ausgewählt gesetzt.</remarks>
    ///<param name="myGL_User">User</param>
    ///<param name="myTSCombox">zufüllende Combobox</param>
    ///<param name="myDT">Datasorce der Combobox</param>
    public static void InitComboMandanten(Globals._GL_USER myGL_User, ref ToolStripComboBox myTSCombox, ref DataTable myDT, bool mybLager)
    {
        myDT.Clear();
        myDT = clsMandanten.GetMandatenListAktiv(myGL_User.User_ID);

        if (myTSCombox.ComboBox != null)
        {
            myTSCombox.ComboBox.DisplayMember = "Matchcode";
            myTSCombox.ComboBox.ValueMember = "Mandanten_ID";
            myTSCombox.ComboBox.DataSource = myDT;

            if (myTSCombox.ComboBox.Items.Count == 0)
            {
                myTSCombox.Enabled = false;
                myTSCombox.ComboBox.SelectedIndex = -1;
            }
            if (myTSCombox.ComboBox.Items.Count == 1)
            {
                myTSCombox.Enabled = false;
                myTSCombox.ComboBox.SelectedIndex = 0;
            }
            if (myTSCombox.Items.Count > 1)
            {
                myTSCombox.Enabled = false;
                //Auswahl auf Defaul setzen
                if (mybLager)
                {
                    //Lager
                    //decimal decTmp = 0;
                    for (Int32 i = 0; i <= myDT.Rows.Count - 1; i++)
                    {
                        if ((bool)myDT.Rows[i]["Default_Lager"] == true)
                        {
                            string strTmp = myDT.Rows[i]["Matchcode"].ToString();
                            myTSCombox.Text = strTmp;
                            myTSCombox.SelectedIndex = i;
                            break;
                        }
                    }
                }
                else
                {
                    //Spedition
                    //decimal decTmp = 0;
                    for (Int32 i = 0; i <= myDT.Rows.Count - 1; i++)
                    {
                        if ((bool)myDT.Rows[i]["Default_Sped"] == true)
                        {
                            string strTmp = myDT.Rows[i]["Matchcode"].ToString();
                            myTSCombox.Text = strTmp;
                            myTSCombox.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
        }
    }
    ///<summary>Functions / InitSearchCombo</summary>
    ///<remarks>Füllt die Combox mit den Spaltenüberschriften der Table.</remarks>
    public static void InitComboSearch(ref ToolStripComboBox myTSCombox, DataTable mydt, clsSystem mySystem)
    {
        //Datasource TmpTable füllen mit Spaltenname und Type
        DataTable dt = new DataTable();
        dt.Columns.Add("ColumnName", typeof(String));
        dt.Columns.Add("Type", typeof(String));

        for (Int32 i = 0; i <= mydt.Columns.Count - 1; i++)
        {
            string strColName = mydt.Columns[i].ColumnName.ToString();
            //TEst
            if (clsClient.ctrSearch_CustomizeComboSearchField(mySystem.Client.MatchCode, strColName))
            {

                if (
                    (strColName != "Check") &&      //Umbuchung
                    (strColName != "Selected")      //Umbuchung
                   )
                {
                    DataRow row = dt.NewRow();

                    row["ColumnName"] = mydt.Columns[i].ColumnName.ToString();
                    row["Type"] = mydt.Columns[i].DataType.ToString();
                    dt.Rows.Add(row);
                }
            }
        }
        dt.DefaultView.Sort = "ColumnName";
        myTSCombox.ComboBox.DataSource = null;
        myTSCombox.ComboBox.DisplayMember = "ColumnName";
        myTSCombox.ComboBox.ValueMember = "Type";
        myTSCombox.ComboBox.DataSource = dt.DefaultView;
        myTSCombox.ComboBox.SelectedIndex = -1;
    }
    ///<summary>Functions / setView</summary>
    ///<remarks></remarks>
    public static void FillSearchColumnFromDGV(ref RadGridView myGrid, ref ToolStripComboBox myCombo, clsSystem mySystem)
    {
        DataTable dt = new DataTable();
        for (Int32 i = 0; i <= myGrid.Columns.Count - 1; i++)
        {
            if (myGrid.Columns[i].IsVisible)
            {
                DataColumn col = new DataColumn();
                col.ColumnName = myGrid.Columns[i].Name;
                col.DataType = myGrid.Columns[i].DataType;
                dt.Columns.Add(col);
            }
        }
        //myCombo.Items.Clear();
        if (dt.Columns.Count > 0)
        {
            Functions.InitComboSearch(ref myCombo, dt, mySystem);
            if (myCombo.SelectedIndex > -1)
            {
                myCombo.SelectedIndex = -1;
            }
        }
    }
    ///<summary>Functions / SetMandantenDaten</summary>
    ///<remarks>Ermittelt den gewählten Mandanten und setzt entsprechend die internen Variablen.</remarks>
    public static void SetMandantenDaten(ref ToolStripComboBox myTSCombox, ref decimal myMandantenID, ref string myMandantenName)
    {
        if (myTSCombox.ComboBox.SelectedIndex > -1)
        {
            myMandantenName = myTSCombox.ComboBox.Text.ToString().Trim();
            myMandantenID = (decimal)myTSCombox.ComboBox.SelectedValue;
        }
        else
        {
            myMandantenID = 0;
            myMandantenName = string.Empty;
        }
    }
    ///<summary>Functions / SetComboToSelecetedItem</summary>
    ///<remarks>Jedes Item der Combobox wird mit dem strValue verglichen und das entsprechende Item als selektiert 
    ///         gekennzeichnet.</remarks>
    ///<param name="combo">ComboBox</param>
    ///<param name="strValue">Vergleichswert</param>
    public static void SetComboToSelecetedItem(ref ComboBox combo, string strValue)
    {
        Int32 iTmp = combo.FindString(strValue);
        if (iTmp != combo.SelectedIndex)
        {
            combo.SelectedIndex = iTmp;
        }
        /*
        for (Int32 i = 0; i <= combo.Items.Count - 1; i++)
        {
            combo.SelectedIndex = i;
            string strTmp = string.Empty;
            strTmp = combo.Text.ToString();
            if (strTmp == strValue)
            {
                break;
            }
            else
            {
                combo.SelectedIndex = -1;
            }
        }*/
    }
    ///<summary>Functions / CheckUserForAdminComtec</summary>
    ///<remarks></remarks>
    public static bool CheckUserForAdminComtec(Globals._GL_USER myGLUser)
    {
        bool bReturn = false;
        if (
                (myGLUser.LoginName.ToString().ToUpper() == "ADMINISTRATOR")
                ||
                (myGLUser.LoginName.ToString().ToUpper() == "ADMIN")
            )
        {
            bReturn = true;
        }
        return bReturn;
    }
    ///<summary>Functions / SetComboToSelecetedItem</summary>
    ///<remarks>Jedes Item der Combobox wird mit dem strValue verglichen und das entsprechende Item als selektiert 
    ///         gekennzeichnet.</remarks>
    ///<param name="combo">ComboBox</param>
    ///<param name="strValue">Vergleichswert</param>
    public static void SetComboToSelecetedValue(ref ComboBox combo, string strValue)
    {
        for (Int32 i = 0; i <= combo.Items.Count - 1; i++)
        {
            combo.SelectedIndex = i;
            string strTmp = string.Empty;
            if (combo.SelectedValue != null)
            {
                strTmp = combo.SelectedValue.ToString();
                if (strTmp == strValue)
                {
                    break;
                }
                else
                {
                    if (combo.SelectedIndex > -1)
                    {
                        combo.SelectedIndex = -1;
                    }
                }
            }
        }
    }
    ///<summary>Functions / SetComboToSelecetedItem</summary>
    ///<remarks>Jedes Item der Combobox wird mit dem strValue verglichen und das entsprechende Item als selektiert 
    ///         gekennzeichnet.</remarks>
    ///<param name="combo">ComboBox</param>
    ///<param name="strValue">Vergleichswert</param>
    public static void SetComboToSelecetedValue(ref MultiColumnComboBox combo, string strValue)
    {
        for (Int32 i = 0; i <= combo.Items.Count - 1; i++)
        {
            combo.SelectedIndex = i;
            string strTmp = string.Empty;
            strTmp = combo.SelectedValue.ToString();
            if (strTmp == strValue)
            {
                break;
            }
            else
            {
                combo.SelectedIndex = -1;
            }
        }
    }
    ///<summary>Functions / SetComboToSelecetedItem</summary>
    ///<remarks>Jedes Item der Combobox wird mit dem strValue verglichen und das entsprechende Item als selektiert 
    ///         gekennzeichnet.</remarks>
    ///<param name="combo">ComboBox</param>
    ///<param name="strValue">Vergleichswert</param>
    public static void SetToolStripComboToSelecetedItem(ref ToolStripComboBox combo, string strValue)
    {
        for (Int32 i = 0; i <= combo.Items.Count - 1; i++)
        {
            combo.SelectedIndex = i;
            string strTmp = string.Empty;
            strTmp = combo.SelectedItem.ToString();
            if (strTmp == strValue)
            {
                break;
            }
            else
            {
                combo.SelectedIndex = -1;
            }
        }
    }
    ///<summary>Functions / SetRADMultiColumnBoxComboToSelecetedValue</summary>
    ///<remarks>Jedes Item der Combobox wird mit dem strValue verglichen und das entsprechende Item als selektiert 
    ///         gekennzeichnet.</remarks>
    ///<param name="combo">ComboBox</param>
    ///<param name="strValue">Vergleichswert</param>
    public static void SetRADMultiColumnBoxComboToSelecetedValueByValue(ref RadMultiColumnComboBox myCombo, string myValue)
    {
        for (Int32 i = 0; i <= myCombo.EditorControl.Rows.Count - 1; i++)
        {
            myCombo.SelectedIndex = i;
            string strTmp = string.Empty;
            strTmp = myCombo.SelectedValue.ToString();
            if (strTmp == myValue)
            {
                break;
            }
            else
            {
                myCombo.SelectedIndex = -1;
            }
        }
    }
    ///<summary>Functions / SetComboToSelecetedItem</summary>
    ///<remarks>Jedes Item der Combobox wird mit dem strValue verglichen und das entsprechende Item als selektiert 
    ///         gekennzeichnet.</remarks>
    ///<param name="combo">ComboBox</param>
    ///<param name="strValue">Vergleichswert</param>
    public static void SetComboToSelecetedText(ref ComboBox combo, string strValue)
    {
        for (Int32 i = 0; i <= combo.Items.Count - 1; i++)
        {
            combo.SelectedIndex = i;
            string strTmp = string.Empty;
            strTmp = combo.SelectedText.ToString();
            //strTmp = combo.Items[i].ToString();
            //strTmp = combo.SelectedValue.ToString();
            if (strTmp == strValue)
            {
                break;
            }
            else
            {
                combo.SelectedIndex = -1;
            }
        }
    }
    ///<summary>Functions / SetComboToSelecetedItem</summary>
    ///<remarks>Jedes Item der Combobox wird mit dem strValue verglichen und das entsprechende Item als selektiert 
    ///         gekennzeichnet.</remarks>
    ///<param name="combo">ComboBox</param>
    ///<param name="strValue">Vergleichswert</param>
    public static DataTable InitEnumAuftragStatusToTableForDataSource(Int32 iStart, Int32 iEnd)
    {
        DataTable dt = new DataTable("Status");
        dt.Columns.Add("StatusID", typeof(Int32));
        dt.Columns.Add("Status", typeof(String));

        DataRow rowFirst = dt.NewRow();
        rowFirst["StatusID"] = 0;
        rowFirst["Status"] = "-- bitte wählen --";
        dt.Rows.Add(rowFirst);

        for (Int32 i = iStart; i <= iEnd; i++)
        {
            DataRow row = dt.NewRow();
            switch (i)
            {
                case 1:
                    row["StatusID"] = i;
                    row["Status"] = enumSpedAuftragStatus.unvollständig.ToString();
                    break;
                case 2:
                    row["StatusID"] = i;
                    row["Status"] = enumSpedAuftragStatus.vollständig.ToString();
                    break;
                case 3:
                    row["StatusID"] = i;
                    row["Status"] = enumSpedAuftragStatus.storniert.ToString();
                    break;
                case 4:
                    row["StatusID"] = i;
                    row["Status"] = enumSpedAuftragStatus.disponiert.ToString();
                    break;
                case 5:
                    row["StatusID"] = i;
                    row["Status"] = enumSpedAuftragStatus.durchgeführt.ToString();
                    break;
                case 6:
                    row["StatusID"] = i;
                    row["Status"] = enumSpedAuftragStatus.FreigabeBerechnung.ToString();
                    break;
                case 7:
                    row["StatusID"] = i;
                    row["Status"] = enumSpedAuftragStatus.berechnet.ToString();
                    break;
                case 8:
                    row["StatusID"] = i;
                    row["Status"] = enumSpedAuftragStatus.bezahlt.ToString();
                    break;
            }
            dt.Rows.Add(row);
        }
        return dt;
    }
    ///<summary>Functions / InitActionImageToArtikelVitaGrid</summary>
    ///<remarks>Weist der entsprechenden Aktion das richtige Image im ArtikelVita Grid zu</remarks>
    ///<param name="combo">ComboBox</param>
    ///<param name="strValue">Vergleichswert</param>
    public static void InitActionImageToArtikelVitaGrid(ref DataGridViewCellFormattingEventArgs e, string myAction)
    {
        //Lager Eingang
        if (
                (myAction == enumLagerAktionen.EingangErstellt.ToString()) |
                (myAction == "Eingang erstellt") |
                (myAction == "Eingang Erstellt")
            )
        {
            e.Value = Sped4.Properties.Resources.box_into_16x16;
        }
        if (myAction == enumLagerAktionen.EingangChanged.ToString())
        {
            e.Value = Sped4.Properties.Resources.box_edit_16x16;
        }
        if (myAction == enumLagerAktionen.EingangChecked.ToString())
        {
            e.Value = Sped4.Properties.Resources.box_preferences_16x16;
        }
        if (myAction == enumLagerAktionen.EingangReset.ToString())
        {
            e.Value = Sped4.Properties.Resources.box_16x16;
        }
        if (myAction == enumLagerAktionen.PrintEingangAnzeige.ToString())
        {
            e.Value = Sped4.Properties.Resources.printer2_16x16;
        }
        if (myAction == enumLagerAktionen.PrintEingangDoc.ToString())
        {
            e.Value = Sped4.Properties.Resources.printer2_16x16;
        }
        if (myAction == enumLagerAktionen.PrintEingangLfs.ToString())
        {
            e.Value = Sped4.Properties.Resources.printer2_16x16;
        }
        //Lager Ausgang
        if (
         (myAction == enumLagerAktionen.AusgangErstellt.ToString()) |
         (myAction == "Ausgang erstellt") |
         (myAction == "Ausgang Erstellt")
        )
        {
            e.Value = Sped4.Properties.Resources.box_out_16x16;
        }
        if (myAction == enumLagerAktionen.AusgangChanged.ToString())
        {
            e.Value = Sped4.Properties.Resources.box_closed_edit_16x16;
        }
        if (myAction == enumLagerAktionen.AusgangChecked.ToString())
        {
            e.Value = Sped4.Properties.Resources.box_previous_16x16;
        }
        if (myAction == enumLagerAktionen.PrintAusgangAnzeige.ToString())
        {
            e.Value = Sped4.Properties.Resources.printer2_16x16;
        }
        if (myAction == enumLagerAktionen.PrintAusgangDoc.ToString())
        {
            e.Value = Sped4.Properties.Resources.printer2_16x16;
        }
        if (myAction == enumLagerAktionen.PrintAusganLfs.ToString())
        {
            e.Value = Sped4.Properties.Resources.printer2_16x16;
        }
        //Artikel
        if (myAction == enumLagerAktionen.ArtikelAdd_Eingang.ToString())
        {
            e.Value = Sped4.Properties.Resources.gear_ok_16x16;
        }
        if (myAction == enumLagerAktionen.ArtikelAdd_Ausgang.ToString())
        {
            e.Value = Sped4.Properties.Resources.gear_forbidden_16x16;
        }
        if (myAction == enumLagerAktionen.ArtikelAdd_Artikel.ToString())
        {
            e.Value = Sped4.Properties.Resources.gear_add_16x16;
        }
        if (myAction == enumLagerAktionen.ArtikelChange.ToString())
        {
            e.Value = Sped4.Properties.Resources.gear_edit_16x16;
        }
        if (myAction == enumLagerAktionen.ArtikelDelete.ToString())
        {
            e.Value = Sped4.Properties.Resources.gear_delete_16x16;
        }
        if (myAction == enumLagerAktionen.ArtikelRL.ToString())
        {
            e.Value = Sped4.Properties.Resources.truck_blue_16x16;
        }
        if (myAction == enumLagerAktionen.ArtikelStorno.ToString())
        {
            e.Value = Sped4.Properties.Resources.gear_delete_16x16;
        }
        if (myAction == enumLagerAktionen.ArtikelUmbuchung.ToString())
        {
            e.Value = Sped4.Properties.Resources.gear_forbidden_16x16;
        }
        if (myAction == enumLagerAktionen.ArtikelChecked.ToString())
        {
            e.Value = Sped4.Properties.Resources.gear_ok_16x16;
        }
        if (myAction == enumLagerAktionen.ArtikelSchadenAdd.ToString())
        {
            e.Value = Sped4.Properties.Resources.gear_warning_12x12;
        }
        if (myAction == enumLagerAktionen.ArtikelSchadenDel.ToString())
        {
            e.Value = Sped4.Properties.Resources.gear_warning_12x12;
        }

        if (myAction == enumLagerAktionen.ArtikelSonderkosteAdd.ToString())
        {
            e.Value = Sped4.Properties.Resources.money2_add_16x16;
        }
        if (myAction == enumLagerAktionen.ArtikelSonderkostenChange.ToString())
        {
            e.Value = Sped4.Properties.Resources.money;
        }
        if (myAction == enumLagerAktionen.ArtikelSonderkostenDel.ToString())
        {
            e.Value = Sped4.Properties.Resources.money2_delete_16x16;
        }
        //Sperrlager
        if (myAction == enumLagerAktionen.SperrlagerIN.ToString())
        {
            e.Value = Sped4.Properties.Resources.cabinet_warning_16x16;
        }
        if (myAction == enumLagerAktionen.SperrlagerOUT.ToString())
        {
            e.Value = Sped4.Properties.Resources.cabinet_warning_16x16;
        }
        //StornoKorrekturVerfahren
        if (myAction == enumLagerAktionen.StornoKorrekturVerfahren.ToString())
        {
            e.Value = Sped4.Properties.Resources.sign_warning_16x16;
        }
        //RLL
        if (myAction == enumLagerAktionen.ArtikelRL.ToString())
        {
            e.Value = Sped4.Properties.Resources.sign_warning_16x16;
        }
        //Lagermeldungen
        if (
                (myAction == enumLagerMeldungen.LSL.ToString()) ||
                (myAction == enumLagerMeldungen.EML.ToString()) ||
                (myAction == enumLagerMeldungen.EME.ToString()) ||
                (myAction == enumLagerMeldungen.BME.ToString()) ||
                (myAction == enumLagerMeldungen.BML.ToString()) ||
                (myAction == enumLagerMeldungen.AML.ToString()) ||
                (myAction == enumLagerMeldungen.AME.ToString()) ||
                (myAction == enumLagerMeldungen.AbL.ToString()) ||
                (myAction == enumLagerMeldungen.AbE.ToString()) ||
                (myAction == enumLagerMeldungen.STL.ToString()) ||
                (myAction == enumLagerMeldungen.STE.ToString()) ||
                (myAction == enumLagerMeldungen.AVL.ToString()) ||
                (myAction == enumLagerMeldungen.AVE.ToString()) ||
                (myAction == enumLagerMeldungen.TAA.ToString()) ||
                (myAction == enumLagerMeldungen.AVA.ToString()) ||
                (myAction == enumLagerMeldungen.AVE.ToString()) ||
                (myAction == enumLagerMeldungen.AbA.ToString()) ||
                (myAction == enumLagerMeldungen.RLL.ToString()) ||
                (myAction == enumLagerMeldungen.RLE.ToString()) ||
                (myAction == enumLagerMeldungen.TSL.ToString()) ||
                (myAction == enumLagerMeldungen.TSE.ToString())
           )
        {
            e.Value = Sped4.Properties.Resources.mail_exchange_16x16;
        }
        if (myAction == enumLagerAktionen.ImageToArtikel.ToString())
        {
            e.Value = Sped4.Properties.Resources.camera2_16x16;
        }

        //Abrufe / Call
        if (myAction == enumLagerAktionen.AbrufCreate.ToString())
        {
            e.Value = Sped4.Properties.Resources.note_add_16x16;
        }
        if (myAction == enumLagerAktionen.AbrufChange.ToString())
        {
            e.Value = Sped4.Properties.Resources.note_edit_16x16;
        }
        if (myAction == enumLagerAktionen.AbrufDelete.ToString())
        {
            e.Value = Sped4.Properties.Resources.note_delete_16x16;
        }
        //CustomProcesses
        if (myAction == enumLagerAktionen.ArtikelCustomerProcessExceptionExist.ToString())
        {
            e.Value = Sped4.Properties.Resources.sign_warning_16x16;
        }

    }
    ///<summary>Functions / GetDataGridCellStatusImage</summary>
    ///<remarks>Weist dem Status das entsprechende Image im Auftragslisten Grid zu</remarks>
    ///<param name="e">DataGridViewCellFormattingEventArgs</param>
    ///<param name="iStatus">Status</param>
    public static Bitmap GetDataGridCellStatusImage(Int32 myStatus)
    {
        /************************************************************************************
         * Übersicht Status und entsprechende Images
         * - 1 unvollständig  
         * - 2 vollständig     
         * - 3 storniert       
         * - 4 disponiert     
         * - 5 durchgeführt   
         * - 6 Freigabe Berechnung  
         * - 7 berechnet       
         * -8 bezhalt
         * ***********************************************************************************/
        Bitmap ReturnImage = Sped4.Properties.Resources.bullet_ball_grey;

        switch (myStatus)
        {
            case 1:
                ReturnImage = Sped4.Properties.Resources.bullet_ball_red;
                break;
            case 2:
                ReturnImage = Sped4.Properties.Resources.bullet_ball_green;
                break;
            case 3:
                ReturnImage = Sped4.Properties.Resources.bullet_ball_grey;
                break;
            case 4:
                ReturnImage = Sped4.Properties.Resources.bullet_ball_blue;
                break;
            case 5:
                ReturnImage = Sped4.Properties.Resources.Leer;
                break;
            case 6:
                ReturnImage = Sped4.Properties.Resources.Leer;
                break;
            case 7:
                ReturnImage = Sped4.Properties.Resources.Leer;
                break;

            default:
                ReturnImage = Sped4.Properties.Resources.bullet_ball_grey;
                break;
        }
        return ReturnImage;
    }
    ///<summary>Functions / GetDataGridCellStatusImage</summary>
    ///<remarks>Weist dem Status das entsprechende Image im Auftragslisten Grid zu</remarks>
    ///<param name="e">DataGridViewCellFormattingEventArgs</param>
    ///<param name="iStatus">Status</param>
    public static Bitmap GetFileExtensionImage(string myExtention)
    {
        /************************************************************************************
         * Übersicht Status und entsprechende Images
         * ***********************************************************************************/
        Bitmap ReturnImage = Sped4.Properties.Resources.bullet_ball_grey;

        switch (myExtention)
        {
            case ".pdf":
                ReturnImage = Sped4.Properties.Resources.docPDF;
                break;
            default:
                ReturnImage = Sped4.Properties.Resources.document_delete;
                break;
        }
        return ReturnImage;
    }
    ///<summary>Functions / GetInfoImage</summary>
    ///<remarks></remarks>
    public static Bitmap GetImageForBSInfo4905SZG(decimal decFaktor)
    {
        decimal decFaktor1 = 1;
        decimal decFaktor2 = 1.5M;
        Bitmap ReturnImage = Sped4.Properties.Resources.warning.ToBitmap();

        if (decFaktor < decFaktor1)
        {
            ReturnImage = Sped4.Properties.Resources.bullet_ball_red_16x16;
        }
        if ((decFaktor >= decFaktor1) && (decFaktor < decFaktor2))
        {
            ReturnImage = Sped4.Properties.Resources.bullet_ball_yellow_16x16;
        }
        if (decFaktor >= decFaktor2)
        {
            ReturnImage = Sped4.Properties.Resources.bullet_ball_green_16x16;
        }
        return ReturnImage;
    }
    ///<summary>Functions / GetInfoImage</summary>
    ///<remarks></remarks>
    public static Bitmap GetImageForBSInfo4905SIL(decimal decFaktor)
    {
        decimal decFaktor1 = 0.49M;
        decimal decFaktor2 = 0.99M;
        Bitmap ReturnImage = Sped4.Properties.Resources.warning.ToBitmap();

        if (decFaktor < decFaktor1)
        {
            ReturnImage = Sped4.Properties.Resources.bullet_ball_red_16x16;
        }
        if ((decFaktor >= decFaktor1) && (decFaktor < decFaktor2))
        {
            ReturnImage = Sped4.Properties.Resources.bullet_ball_yellow_16x16;
        }
        if (decFaktor >= decFaktor2)
        {
            ReturnImage = Sped4.Properties.Resources.bullet_ball_green_16x16;
        }
        return ReturnImage;
    }
    ///<summary>Functions / GetInfoImage</summary>
    ///<remarks></remarks>
    public static Color GetBackgroudColorBSInfo4905SIL(decimal decFaktor)
    {
        decimal decFaktor1 = 0.49M;
        decimal decFaktor2 = 0.99M;
        Color ReturnColor = Color.White;

        if (decFaktor < decFaktor1)
        {
            // ReturnColor = Color.Red;
            ReturnColor = Color.Tomato;
        }
        if ((decFaktor >= decFaktor1) && (decFaktor < decFaktor2))
        {
            ReturnColor = Color.Yellow;
        }
        if (decFaktor >= decFaktor2)
        {
            //ReturnColor = Color.Green;
            ReturnColor = Color.LimeGreen;
        }
        return ReturnColor;
    }
    ///<summary>Functions / GetTxtBSInfo4905SIL</summary>
    ///<remarks></remarks>
    public static string GetTxtBSInfo4905SIL(decimal decFaktor)
    {
        decimal decFaktor1 = 0.49M;
        decimal decFaktor2 = 0.99M;
        string strReturn = string.Empty;

        if (decFaktor < decFaktor1)
        {
            strReturn = "!!!";
        }
        if ((decFaktor >= decFaktor1) && (decFaktor < decFaktor2))
        {
            strReturn = "OK?";
        }
        if (decFaktor >= decFaktor2)
        {
            strReturn = "OK";
        }
        return strReturn;
    }
    ///<summary>Functions / GetTxtBSInfo4905SIL</summary>
    ///<remarks></remarks>
    public static Color GetFontColorBSInfo4905SIL(decimal decFaktor)
    {
        decimal decFaktor1 = 0.49M;
        decimal decFaktor2 = 0.99M;
        Color ReturnColor = Color.Black;
        //vorerst Color Black
        //if (decFaktor < decFaktor1)
        //{
        //    ReturnColor = Color.Red;
        //}
        //if ((decFaktor >= decFaktor1) && (decFaktor < decFaktor2))
        //{
        //    ReturnColor = Color.Yellow;
        //}
        //if (decFaktor >= decFaktor2)
        //{
        //    ReturnColor = Color.Green;
        //}
        return ReturnColor;
    }
    ///<summary>Functions / GetColorByDringlichkeit</summary>
    ///<remarks>Weist dem Status das entsprechende Image im Auftragslisten Grid zu</remarks>
    public static Color GetColorByDringlichkeit(TimeSpan mySpan)
    {
        //Beschreibung: Dringlichkeitsstufen 1-5
        // 1: Liefertermin innerhalb 2 W-Tage
        // 2: Liefertermin in 2 W-Tagen
        // 3: Liefertermin größer 2 Tage entfernt
        // 5: Liefertermin = Today oder schon vergangen

        if (mySpan.Days <= 0)
        {
            return Color.Red;
        }
        else if (mySpan.Days == 1)
        {
            return Color.RoyalBlue;
        }
        else if (mySpan.Days == 2)
        {
            return Color.Blue;
        }
        else //if (mySpan.Days > 2)
        {
            return Color.Gray;
        }
    }
    ///<summary>Functions / GetInfoImage</summary>
    ///<remarks></remarks>
    public static Bitmap GetInfoImage(bool myRead, bool myScan)
    {
        Bitmap ReturnImage = Sped4.Properties.Resources.document_new_24x24;
        if (!myRead)
        {
            ReturnImage = Sped4.Properties.Resources.document_new_24x24;
        }
        else
        {
            // Wenn Auftrag noch nicht hinterlegt, dann Warnung
            if (myScan)
            {
                ReturnImage = Sped4.Properties.Resources.document_attachment;
            }
            else
            {
                ReturnImage = Sped4.Properties.Resources.document_delete;
            }
        }
        return ReturnImage;
    }
    ///<summary>Functions / FormatGridCellByType</summary>
    ///<remarks>Formatiert die Spalten eines Grid entsprechend dem Typ</param>
    ///<param name="iStatus">Status</param>
    public static void FormatGridCellByType(ref AFGrid myGrd, ref DataTable dt)
    {

        for (Int32 i = 0; i <= dt.Columns.Count - 1; i++)
        {
            string strType = dt.Columns[i].DataType.ToString();

            switch (strType)
            {
                case "System.String":
                    myGrd.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft;
                    break;

                case "System.Decimal":
                    myGrd.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                    myGrd.Columns[i].DefaultCellStyle.Format = "N2";
                    break;

                case "System.Integer":
                    myGrd.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
                    myGrd.Columns[i].DefaultCellStyle.Format = "N2";
                    break;

                case "System.DateTime":
                    myGrd.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
                    myGrd.Columns[i].DefaultCellStyle.Format = "d";
                    break;
            }

            //Für alle Spalten
            myGrd.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.False;
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
    ///<summary>Functions / SetTabPagesEnabled</summary>
    ///<remarks>Blendet die entsprechende Tabpage aus / ein.</remarks>
    ///<param name="FormType">Übergabe Form</param>
    //public static void ShowOrHideTabPage(ref TabControl myTab, bool bShow, string myPageName)
    public static void HideTabPage(ref TabControl myTab, string myPageName)
    {
        for (Int32 i = 0; i <= myTab.TabPages.Count - 1; i++)
        {
            if (myTab.TabPages[i].Name.Equals(myPageName, StringComparison.OrdinalIgnoreCase))
            {
                myTab.TabPages.RemoveAt(i);
            }
        }
    }
    ///<summary>Functions / SetTabPagesEnabled</summary>
    ///<remarks>Blendet die entsprechende Tabpage aus / ein.</remarks>
    ///<param name="FormType">Übergabe Form</param>
    //public static void ShowOrHideTabPage(ref TabControl myTab, bool bShow, string myPageName)
    public static void ShowTabPage(ref TabControl myTab, string myPageName)
    {
        for (Int32 i = 0; i <= myTab.TabPages.Count - 1; i++)
        {
            if (myTab.TabPages[i].Name.Equals(myPageName, StringComparison.OrdinalIgnoreCase))
            {
                myTab.SelectedIndex = i;
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
    ///<summary>Functions / FromatShortDateTime</summary>
    ///<remarks>Liefert folgendes DatetimeFormat als String:
    ///         "MM.dd.yyyy HH:mm"</remarks>
    ///<param name="Value">DateTime</param>
    ///<return name="Value">String ("MM.dd.yyyy HH:mm")</return>
    public static string GetDateTImeStringForFileName()
    {
        string strTmp = string.Format("{0:yyyy}", DateTime.Now) +
                        "_" +
                        string.Format("{0:MM}", DateTime.Now) +
                        "_" +
                        string.Format("{0:dd}", DateTime.Now) +
                        "_" +
                        string.Format("{0:HH}", DateTime.Now) +
                        string.Format("{0:mm}", DateTime.Now) +
                        "_" +
                        string.Format("{0:fff}", DateTime.Now);
        return strTmp;
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
        strDate = ((DateTime)Globals.DefaultDateTimeMinValue).ToShortDateString() + " " + strZeit;
        return Convert.ToDateTime(strDate);
    }
    ///<summary>Functions / FormatToHHMM</summary>
    ///<remarks>Anhand der Übergabeparameter wird eine Uhrzeit zusammengesetzt ( Std und Min ) für 
    ///         die Uhrzeit des Zeitfensters in der Disposition.</remarks>
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
    ///<summary>Functions / CheckForEmail</summary>
    ///<remarks></remarks> 
    public static bool CheckForEmail(string email)
    {
        string re1 = "([\\w-+]+(?:\\.[\\w-+]+)*@(?:[\\w-]+\\.)+[a-zA-Z]{2,7})";	// Email Address 1

        Regex regex = new Regex(re1, RegexOptions.IgnoreCase | RegexOptions.Singleline);
        Match match = regex.Match(email);

        return match.Success;
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
    ///<summary>Functions / CreateArtikelIDRef_VW</summary>
    ///<remarks>ArtikelIDRef für VW 
    ///            Lieferantennummer (9stellig)+
    ///            LVSNR ( 8Stellig)+
    ///            Produktionsnummer (9stellig)
    ///</remarks>
    //public static string CreateArtikelIDRef_VW(string myLieferangenNr, string myLVSNr, string myProdNr)
    //{
    //    string strReturn = string.Empty;

    //    string strLieferantennummer = "000000000";
    //    string strLVSNr = "00000000";
    //    string strProduktionsnummer = "000000000";

    //    //1. Teil Lieferantennummer 
    //    strLieferantennummer = strLieferantennummer + myLieferangenNr.Trim();
    //    strLieferantennummer = strLieferantennummer.Substring(strLieferantennummer.Length - 9);
    //    //2. Teil LVSNR
    //    strLVSNr = strLVSNr + myLVSNr.Trim();
    //    strLVSNr = strLVSNr.Substring(strLVSNr.Length - 8);

    //    //3. Teil Produktionsnummer
    //    strProduktionsnummer = strProduktionsnummer + myProdNr.Trim();
    //    strProduktionsnummer = strProduktionsnummer.Substring(strProduktionsnummer.Length - 9);

    //    string strTmp = strLieferantennummer + strLVSNr + strProduktionsnummer;
    //    if (strTmp.Length == 26)
    //    {
    //        strReturn = strTmp;
    //    }
    //    else
    //    {
    //        if (strTmp.Length > 26)
    //        {
    //            strTmp = strTmp.Substring(strTmp.Length - 26);
    //        }
    //        if (strTmp.Length < 26)
    //        {
    //            strTmp = strTmp + "0000000000";
    //            strTmp = strTmp.Substring(1, 26);
    //        }
    //        strReturn = strTmp;
    //    }
    //    strReturn = strTmp;
    //    return strReturn;
    //}
    /****************************************************************************
    * 5.                Fremdkomponente Telerik
    ****************************************************************************/
    ///<summary>Functions / Telerik_RunExportToExcelML</summary>
    ///<remarks>Excelexport für Telerikkomponente Grid</remarks>
    public static void Telerik_RunExportToExcelML(ref frmMAIN myFrmMain, ref RadGridView myGrid, string fileName, ref bool openExportFile, Globals._GL_USER myGLUser, bool bAskToOpenInExcel)
    {
        myFrmMain.ResetStatusBar();
        myFrmMain.InitStatusBar(3);
        myFrmMain.StatusBarWork(false, string.Empty);

        myFrmMain.StatusBarWork(false, "Daten werden übergeben...");
        ExportToExcelML excelExporter = new ExportToExcelML(myGrid);
        excelExporter.SheetName = "Bestand";
        excelExporter.SummariesExportOption = SummariesOption.ExportAll;
        excelExporter.SheetMaxRows = ExcelMaxRows._1048576;
        excelExporter.ExportVisualSettings = true;

        //Hier können spezielle Formatierung für den Export vorgenommen werden
        //Datumformat im Export einstellen
        for (Int32 i = 0; i <= myGrid.Columns.Count - 1; i++)
        {
            //string strTmp = myGrid.Columns[i].DataType.ToString();
            if (myGrid.Columns[i].DataType.ToString() == "System.DateTime")
            {
                myGrid.Columns[i].ExcelExportType = DisplayFormatType.Custom;
                myGrid.Columns[i].ExcelExportFormatString = "dd.MM.yyyy hh:mm";
            }
        }

        try
        {
            excelExporter.RunExport(fileName);
            myFrmMain.StatusBarWork(false, string.Empty);
            if (bAskToOpenInExcel)
            {
                if (clsMessages.Export_OpenFileInExcel())
                {
                    openExportFile = true;
                }
            }
        }
        catch (Exception ex)
        {
            myFrmMain.StatusBarWork(false, "Übergabe NICHT erfolgreich...");

            clsError error = new clsError();
            error._GL_User = myGLUser;
            error.Aktion = "Excelexport";
            error.exceptText = ex.ToString();
        }
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

    //
    /****************************************************************************
     * 
     *                              DataTable 
     * 
     * **************************************************************************/
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
    //  //
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
    //

    public static DataTable FilterDataTableLIKEForString(DataTable dt, string SearchText, string Columnname)
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
    public static Int32 GetValueRowNrFromDataTable(ref DataTable dt, string ColName, string strCellVal)
    {
        Int32 iVal = -1;
        Int32 iCol = 0;

        //Suche der entsprechenden Spalte
        for (Int32 j = 0; j <= dt.Columns.Count - 1; j++)
        {
            if (dt.Columns[j].ColumnName == ColName)
            {
                iCol = j;
                j = dt.Columns.Count;
            }
        }
        //Jetzt kann in der entsprechenden Spalte der Table der Wert gesucht werden
        for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
        {
            if (dt.Rows[i][iCol] != null)
            {
                if (dt.Rows[i][iCol].ToString() == strCellVal)
                {
                    iVal = i;
                    i = dt.Rows.Count;
                }
            }
        }
        return iVal;
    }
    /*******************************************************************
     * 
     *                  Rechnungen / Rechnungsnummern
     * 
     * *****************************************************************/
    //
    //----------- Get and Set RG bzw. GS Nr aus Rechnungsnummer ------------
    //
    public static DataSet GetAndSetRGGSNr(DataSet dsRG, bool PrintAgain)
    {
        DataTable dtRechnung = new DataTable("Rechnung");
        dtRechnung.Columns.Add("RGNr", typeof(decimal));
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
            dtRechnung.Rows[0]["RGNr"] = (decimal)dsRG.Tables["Frachtdaten"].Rows[0]["RG_ID"];
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
    //
    //--------- Userdaten werden zugewiesen ------------------
    //
    public static void GlobalUserSet(ref Globals._GL_USER _GL_User, DataTable dt)
    {
        _GL_User.User_ID = (decimal)dt.Rows[0]["ID"];
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

        // expl. User - Einstellungen

        // dtDispoVon = akteulle Datum 
        _GL_User.us_dtDispoVon = DateTime.Now.Date;

        //dtDispoBis = aktuelle Datum + 2 Tage
        _GL_User.us_dtDispoBis = DateTime.Now.Date.AddDays(2);

        if (dt.Rows[0]["FontSize"] != DBNull.Value)
        {
            _GL_User.us_decFontSize = (decimal)dt.Rows[0]["FontSize"];
        }
        else
        {
            _GL_User.us_decFontSize = 7.5M;
        }
    }
    /*****************************************************************************************************
     * 
     *                      Dantenbankeinträge - SQL Anweisungen
     * 
     * **************************************************************************************************/
    ///<summary>Functions / AddLogbuch</summary>
    ///<remarks></remarks>
    public static void AddLogbuch(Decimal myBenutzerID, string myAktion, string myBeschreibung)
    {
        clsLogbuch log = new clsLogbuch();
        log.BenutzerID = myBenutzerID;
        log.Aktion = myAktion;
        log.Beschreibung = myBeschreibung;
        log.LogbuchInsert();
    }
    ///<summary>Functions / CheckAndRemoveTableFromDataSet</summary>
    ///<remarks></remarks>
    public static void CheckAndRemoveTableFromDataSet(ref DataSet ds, string RemoveTableName)
    {
        if (ds.Tables[RemoveTableName] != null)
        {
            ds.Tables.Remove(RemoveTableName);
        }
    }
    ///<summary>Functions / GetMaxReccourcenEndzeit</summary>
    ///<remarks></remarks>
    public static DateTime GetMaxReccourcenEndzeit(ref structRecources recource, Globals._GL_USER myGLUser)
    {
        DateTime maxREZ = DateTime.Today;
        clsResource res = new clsResource();
        res.m_i_RecourceID = recource.RecourceID;
        //Get RecourceID
        decimal RecDB_ID = res.GetTruckIDbyRecourceID();  //ID der DB der jeweilingen Recource (Fahrzeug oder Faherer)

        maxREZ = clsKommission.GetMaxEntladeZeit(RecDB_ID, myGLUser);
        return maxREZ;
    }
    ///<summary>Functions / CutString</summary>
    ///<remarks></remarks>
    public static String CutString(string strToCut)
    {
        string tmp = string.Empty;
        if (strToCut != string.Empty)
        {
            tmp = strToCut.ToString().Trim();
        }
        return tmp;
    }
    ///<summary>Functions / TrimmTexBox</summary>
    ///<remarks></remarks>
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
    ///<summary>Functions / dgv_ColAutoResize</summary>
    ///<remarks></remarks>
    public static void dgv_ColAutoResize(ref Sped4.Controls.AFGrid dgv)
    {
        for (Int32 i = 0; i <= dgv.Columns.Count - 1; i++)
        {
            dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }
    }
    ///<summary>Functions / dgv_ColAutoResize_Fill</summary>
    ///<remarks></remarks>
    public static void dgv_ColAutoResize_Fill(ref Sped4.Controls.AFGrid dgv)
    {
        for (Int32 i = 0; i <= dgv.Columns.Count - 1; i++)
        {
            dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
    ///<summary>Functions / dgv_ColAutoResize_AllCells</summary>
    ///<remarks></remarks>
    public static void dgv_ColAutoResize_AllCells(ref Sped4.Controls.AFGrid dgv)
    {
        for (Int32 i = 0; i <= dgv.Columns.Count - 1; i++)
        {
            dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
    }
    ///<summary>Functions / dgv_GetWidthShownGrid</summary>
    ///<remarks></remarks>
    public static Int32 dgv_GetWidthShownGrid(ref Sped4.Controls.AFGrid dgv)
    {
        Int32 retVal = 0;
        for (Int32 j = 0; (j <= (dgv.Columns.Count - 1)); j++)
        {
            if (dgv.Columns[j].Visible == true)
            {
                retVal = retVal + dgv.Columns[j].Width;
            }
        }
        return retVal;
    }
    ///<summary>Functions / CreateCheckTable</summary>
    ///<remarks></remarks>
    public static DataTable CreateCheckTable()
    {
        DataTable dt = new DataTable("CheckEingabe");

        if (dt.Columns["Element"] == null)
        {
            DataColumn col1 = new DataColumn();
            col1.DataType = System.Type.GetType("System.String");
            col1.ColumnName = "Element";
            col1.ReadOnly = false;
            dt.Columns.Add(col1);
        }
        if (dt.Columns["Value"] == null)
        {
            DataColumn col2 = new DataColumn();
            col2.DataType = System.Type.GetType("System.String");
            col2.ColumnName = "Value";
            col2.ReadOnly = false;
            dt.Columns.Add(col2);
        }
        if (dt.Columns["Type"] == null)
        {
            DataColumn col3 = new DataColumn();
            col3.DataType = System.Type.GetType("System.String");
            col3.ColumnName = "Type";
            col3.ReadOnly = false;
            dt.Columns.Add(col3);
        }
        if (dt.Columns["Changed"] == null)
        {
            DataColumn col4 = new DataColumn();
            col4.DataType = System.Type.GetType("System.Boolean");
            col4.ColumnName = "Changed";
            col4.ReadOnly = false;
            dt.Columns.Add(col4);
        }
        return dt;
    }
    ///<summary>Functions / GetADRStringFromTable</summary>
    ///<remarks></remarks>
    public static String GetADRStringFromTable(DataTable dt)
    {
        string strE = string.Empty;
        for (Int32 i = 0; i < dt.Rows.Count; i++)
        {
            strE = strE + dt.Rows[i]["Name1"].ToString().Trim() + " - ";
            strE = strE + dt.Rows[i]["PLZ"].ToString().Trim() + " - ";
            strE = strE.ToString().Trim();
            strE = strE + dt.Rows[i]["Ort"].ToString().Trim();
            i = dt.Rows.Count;
        }
        return strE;
    }
    ///<summary>Functions / GetADR_IDFromTable</summary>
    ///<remarks></remarks>
    public static Decimal GetADR_IDFromTable(DataTable dt)
    {
        decimal decTmp = 0.0M;
        for (Int32 i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["ID"] != null)
            {
                if (!decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp))
                {
                    decTmp = 0.0M;
                }
            }
        }
        return decTmp;
    }
    ///<summary>Functions / GetADR_IDFromTable</summary>
    ///<remarks></remarks>
    public static DataTable GetADRTableSearchResultTable(string SearchText, Globals._GL_USER myGLUser)
    {
        DataTable dt = new DataTable();
        dt = clsADR.GetADRList(myGLUser.User_ID);
        string strResult = string.Empty;
        string Ausgabe = string.Empty;
        DataTable tmpDT = new DataTable();
        if (dt.Rows.Count > 0)
        {
            DataRow[] rows = dt.Select("Suchbegriff LIKE '" + SearchText + "'", "Suchbegriff");
            tmpDT = dt.Clone();
            foreach (DataRow row in rows)
            {
                Ausgabe = Ausgabe + row["Suchbegriff"].ToString() + "\n";
                tmpDT.ImportRow(row);
            }
            //strResult = Functions.GetADRStringFromTable(tmpDT);
            //tmpDT.Dispose();
        }
        return tmpDT;
        //tmpDT.Dispose();
    }
    ///<summary>Functions / GetADRTableSearchResult</summary>
    ///<remarks></remarks>
    public static string GetADRTableSearchResult(DataTable dt, string SearchText)
    {
        string strResult = string.Empty;
        string Ausgabe = string.Empty;
        if (dt.Rows.Count > 0)
        {
            DataTable tmpDT = new DataTable();
            DataRow[] rows = dt.Select("Suchbegriff LIKE '" + SearchText + "'", "Suchbegriff");
            tmpDT = dt.Clone();
            foreach (DataRow row in rows)
            {
                Ausgabe = Ausgabe + row["Suchbegriff"].ToString() + "\n";
                tmpDT.ImportRow(row);
            }
            strResult = Functions.GetADRStringFromTable(tmpDT);
            tmpDT.Dispose();
        }
        return strResult;
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
    ///<summary>Functions / CheckAndGetDimension</summary>
    ///<remarks></remarks>
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
    ///<summary>Functions / SearchInDataTableByFilter</summary>
    ///<remarks></remarks>
    public static DataTable SearchInDataTableByFilter(string Spalte, string SearchText, DataTable dt)
    {
        DataTable dtTmp = new DataTable();
        dtTmp = dt.Clone();
        dtTmp.Clear();
        string Ausgabe = string.Empty;

        if (dt.Rows.Count > 0)
        {
            //DataRow[] rows = dataTable.Select("Relation LIKE '%" + SearchText + "%'", "Relation");
            DataRow[] rows = dt.Select(Spalte + " Like '" + SearchText + "%'", Spalte);

            foreach (DataRow row in rows)
            {
                Ausgabe = Ausgabe + row[Spalte].ToString() + "\n";
                dtTmp.ImportRow(row);
            }
        }
        return dtTmp;
    }
    ///<summary>Functions / GetCalendarWeek</summary>
    ///<remarks></remarks>
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

    public static string SetLZZtoString(DateTime dateTime)
    {
        return dateTime.Year.ToString() + "/" + GetCalendarWeek(dateTime).ToString();
    }

    public static void SetKWValue(ref NumericUpDown nudValue, DateTime dateTime)
    {
        Int32 iKW = GetCalendarWeek(dateTime);
        Decimal decVal = Convert.ToDecimal(iKW);
        nudValue.Value = decVal;
    }
    public static void SetYearValue(ref NumericUpDown nudValue, DateTime dateTime)
    {
        string year = dateTime.Year.ToString();
        Int32 iYear = (Int32)nudValue.Minimum + 1;
        Int32.TryParse(year, out iYear);
        if (
            (iYear < ((Int32)nudValue.Minimum + 1)) ||
            (iYear > ((Int32)nudValue.Maximum))
           )
        {
            iYear = (Int32)nudValue.Minimum + 1;
        }
        Decimal decVal = Convert.ToDecimal(iYear);
        nudValue.Value = decVal;
    }

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


    ///<summary>Functions / setPrintView</summary>
    ///<remarks></remarks>
    public static void setPrintView(ref DataTable dt, ref RadGridView dgv, string katName, string viewname, Globals._GL_SYSTEM myGLSystem, string strFirst = "", List<String> strLast = null)
    {
        Functions.setView(ref dt, ref dgv, katName, viewname, myGLSystem, false, null, true, strFirst, strLast);
    }
    ///<summary>Functions / setView</summary>
    ///<remarks></remarks>
    public static void setView(ref DataTable dt, ref RadGridView dgv, string katName, string viewname, Globals._GL_SYSTEM myGLSystem, bool bShowAll = true, DataColumn[] dts = null, bool bPrint = false, string strFirst = "", List<String> strLast = null)
    {
        if (dt != null && dt.Rows.Count >= 0) //CF 28.06
        {
            dgv.DataSource = null;
            string MissingOnes = "";
            Dictionary<string, List<string>> dicViews;
            if (bPrint == false)
            {
                //dicViews = myGLSystem.DictViews.GetValueOrNull(katName);
                myGLSystem.DictViews.TryGetValue(katName, out dicViews);
            }
            else
            {
                //dicViews = myGLSystem.DictPrintViews.GetValueOrNull(katName);
                myGLSystem.DictPrintViews.TryGetValue(katName, out dicViews);
                if (strLast != null)
                {
                    //Spalte Aufgabe ist für die Worklist
                    if (dt.Columns["Aufgabe"] == null)
                    {
                        dt.Columns.Add("Aufgabe", typeof(string));
                    }
                    if (dt.Columns["Aufgaben"] != null)
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["Aufgabe"] = dt.Rows[i]["Aufgaben"].ToString();
                        }
                }
            }
            if ((dicViews != null) && (dicViews.Count > 0))
            {
                List<string> tmpList;
                dicViews.TryGetValue(viewname, out tmpList);

                Int32 j = 0;
                //Customlist wird durchlaufen und die Spalten entsprechend sortiert
                if ((tmpList is List<string>) && (tmpList.Count > 0))
                {
                    for (Int32 i = 0; i < tmpList.Count; i++)
                    {
                        string temp = tmpList.ElementAt(i);
                        if (dt.Columns[temp] != null)
                        {
                            dt.Columns[temp].SetOrdinal(j++);
                        }
                        else
                        {
                            if (MissingOnes != "")
                            {
                                MissingOnes += ", ";
                            }
                            MissingOnes += temp;
                            // Spalte des Views in DB Query nicht enthalten ...
                            // j--;
                        }
                    }
                    //Ab hier muss die Table durchlaufen werden, da noch Spalten, die nicht zur Liste gehören sich in der
                    // sichtbaren Teil der Auflistung befinden können
                    if (dt.Columns.Count > 0)
                    {
                        for (Int32 i = 0; i < tmpList.Count; i++)
                        {
                            //if (dt.Columns.Count <= tmpList.Count - 1)
                            //{
                            if ((i <= dt.Columns.Count - 1) && (dt.Columns[i].ColumnName != null))
                            {
                                string strColName = dt.Columns[i].ColumnName;
                                //Spalte nicht in Auflistung vorhanden
                                if (!tmpList.Contains(strColName))
                                {
                                    if (dt.Columns.Count < tmpList.Count + 1)
                                    {
                                        dt.Columns[strColName].SetOrdinal(i);
                                    }
                                    else
                                    {
                                        dt.Columns[strColName].SetOrdinal(tmpList.Count + 1);
                                    }
                                }
                            }
                            //}
                        }
                    }
                }
                else
                {
                    if (dts != null)
                    {
                        for (Int32 i = 0; i < dts.Length; i++)
                        {
                            string temp = dts[i].ColumnName;
                            if (dt.Columns[temp] != null)
                            {
                                dt.Columns[temp].SetOrdinal(j++);
                            }
                        }
                    }
                }

                if (strFirst != string.Empty)
                {
                    if (dt.Columns[strFirst] != null)
                    {
                        dt.Columns[strFirst].SetOrdinal(0);
                        j++;
                    }
                }

                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["Aufgabe"] != null)
                    {
                        dt.Columns["Aufgabe"].SetOrdinal(j++);
                    }
                    if (strLast != null && strLast.Count > 0)
                    {
                        foreach (string tmp in strLast)
                        {
                            if (dt.Columns[tmp] == null)
                            {
                                dt.Columns.Add(tmp, typeof(clsExtraCharge));
                            }
                            dt.Columns[tmp].SetOrdinal(j);
                        }
                    }
                }
                dgv.DataSource = dt;
                if (viewname == "Materialnr.")
                {
                    //DataView dv = new DataView(dt);
                    //dv.Sort = "exMaterialnummer,LZZ";
                    dt.DefaultView.Sort = "exMaterialnummer,LZZ";
                    //dgv.DataSource = dv;
                }
                else if (viewname == "LVS Nr.")
                {
                    //DataView dv = new DataView(dt);
                    //dv.Sort = "exMaterialnummer,LZZ";
                    dt.DefaultView.Sort = "LVSNr";
                    //dgv.DataSource = dv;
                }
                else
                {
                    if (dt.Columns.Contains("ArtikelID"))
                    {
                        dt.DefaultView.Sort = "ArtikelID";
                    }
                    else if (dt.Columns.Contains("LVSNr"))
                    {
                        dt.DefaultView.Sort = "LVSNr";
                    }
                }

                if (dgv.Columns["Eingangsdatum"] != null)
                    dgv.Columns["Eingangsdatum"].FormatString = "{0:d}";

                if (dgv.Columns["Ausgangsdatum"] != null)
                    dgv.Columns["Ausgangsdatum"].FormatString = "{0:d}";

                if (dgv.Columns["SPL_IN"] != null)
                    dgv.Columns["SPL_IN"].FormatString = "{0:d}";

                if (dgv.Columns["SPL_OUT"] != null)
                    dgv.Columns["SPL_OUT"].FormatString = "{0:d}";

                if (dgv.Columns["Glühdatum"] != null)
                    dgv.Columns["Glühdatum"].FormatString = "{0:d}";

                //Spalten ein/ausblenden 
                //mr 25.02.2015 
                for (Int32 i = 0; i < dgv.Columns.Count; i++)
                {
                    if ((tmpList is List<string>) && (tmpList.Count > 0))
                    {
                        if ((bShowAll) || (tmpList.Count == 0))
                        {
                            dgv.Columns[i].IsVisible = true;
                        }
                        else
                        {
                            string ColName = dt.Columns[i].ColumnName;
                            dgv.Columns[i].IsVisible = tmpList.Contains(ColName);
                        }
                    }
                }
                if (MissingOnes != "")
                {
                    Console.WriteLine(MissingOnes);

                }
                dgv.BestFitColumns();
            }
        }
    }

}


