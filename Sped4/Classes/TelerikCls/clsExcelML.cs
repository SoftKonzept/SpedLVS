using LVS;
using System;
using System.Collections.Generic;
using System.Drawing;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;
using Telerik.WinControls.UI.Export.ExcelML;



namespace Sped4.Classes.TelerikCls
{
    class clsExcelML
    {


        internal frmMAIN frmMain;
        internal string ExportFileName;
        public List<string> ListHeaderText = new List<string>();
        public DateTime Stichtag;
        public Int32 iExportFormat = 0;
        public clsSystem system;
        internal string ExcelColWidth = "100";
        internal Dictionary<string, string> DictExcelExportColumnWidth;
        public Globals._GL_USER _GL_User;
        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = _GL_User.User_ID;
                return _BenutzerID;
            }
            set { _BenutzerID = value; }
        }
        ///<summary>clsExcelML / Telerik_RunExportToExcelML</summary>
        ///<remarks>Excelexport für Telerikkomponente Grid</remarks>
        public void InitClass(ref frmMAIN myFrmMain, Globals._GL_USER myGLUser)
        {
            this.frmMain = myFrmMain;
            this._GL_User = myGLUser;
        }
        ///<summary>clsExcelML / Telerik_RunExportToExcelML</summary>
        ///<remarks>Excelexport für Telerikkomponente Grid</remarks>
        public void Telerik_RunExportToExcelML(ref RadGridView myGrid
                                               , string fileName
                                               , ref bool openExportFile
                                               , bool bAskToOpenInExcel
                                               , string strSheetName)
        {
            this.frmMain.ResetStatusBar();
            this.frmMain.InitStatusBar(3);
            this.frmMain.StatusBarWork(false, string.Empty);

            this.frmMain.StatusBarWork(false, "Daten werden übergeben...");
            ExportToExcelML excelExporter = new ExportToExcelML(myGrid);
            excelExporter.SheetName = strSheetName;

            excelExporter.SheetMaxRows = ExcelMaxRows._1048576;
            excelExporter.HiddenColumnOption = HiddenOption.DoNotExport;
            excelExporter.SummariesExportOption = SummariesOption.ExportAll;
            excelExporter.ExportVisualSettings = true;

            excelExporter.ExcelTableCreated += excelExporter_ExcelTableCreated;
            excelExporter.ExcelCellFormatting += exporter_ExcelCellFormattingDefault;
            excelExporter.ExcelRowFormatting += ExcelExporter_ExcelRowFormatting;

            DictExcelExportColumnWidth = new Dictionary<string, string>();

            //Hier können spezielle Formatierung für den Export vorgenommen werden
            //Datumformat im Export einstellen
            //for (Int32 i = 0; i <= myGrid.Columns.Count - 1; i++)
            //{
            //    string strColName = myGrid.Columns[i].Name;
            //    string strColWidth = myGrid.Columns[i].Width.ToString();
            //    if (!DictExcelExportColumnWidth.ContainsKey(strColName))
            //    {
            //        DictExcelExportColumnWidth.Add(strColName, strColWidth);
            //    }

            //    switch (myGrid.Columns[i].DataType.ToString())
            //    {
            //        //--- string
            //        case "System.string":
            //        case "System.String":
            //            myGrid.Columns[i].ExcelExportType = DisplayFormatType.Text;
            //            myGrid.Columns[i].TextAlignment = ContentAlignment.MiddleLeft;
            //            break;

            //        //--- DateTime
            //        case "System.DateTime":
            //            myGrid.Columns[i].ExcelExportType = DisplayFormatType.Custom;
            //            myGrid.Columns[i].ExcelExportFormatString = "dd.MM.yyyy";
            //            myGrid.Columns[i].TextAlignment = ContentAlignment.MiddleCenter;
            //            //myGrid.Columns[i].SetDefaultValueOverride()
            //            break;

            //        //--- INT
            //        case "System.Int32":
            //        case "System.Int64":
            //            myGrid.Columns[i].ExcelExportType = DisplayFormatType.Custom;
            //            myGrid.Columns[i].ExcelExportFormatString = "##0";
            //            myGrid.Columns[i].TextAlignment = ContentAlignment.MiddleLeft;
            //            break;

            //        //--- Decimal
            //        case "System.Decimal":
            //            myGrid.Columns[i].ExcelExportType = DisplayFormatType.Custom;
            //            myGrid.Columns[i].ExcelExportFormatString = "#,##0.000";
            //            myGrid.Columns[i].TextAlignment = ContentAlignment.MiddleRight;
            //            break;
            //    } //--- switch               


            //}

            //for (Int32 i = 0; i <= myGrid.Columns.Count - 1; i++)
            foreach (GridViewDataColumn col in myGrid.Columns)
            {
                string strColName = col.Name;
                string strColWidth = col.Width.ToString();
                if (!DictExcelExportColumnWidth.ContainsKey(strColName))
                {
                    DictExcelExportColumnWidth.Add(strColName, strColWidth);
                }
                switch (col.DataType.ToString())
                {
                    //--- string
                    case "System.string":
                    case "System.String":
                        col.ExcelExportType = DisplayFormatType.Text;
                        col.TextAlignment = ContentAlignment.MiddleLeft;
                        break;

                    //--- DateTime
                    case "System.DateTime":
                        col.ExcelExportType = DisplayFormatType.Custom;
                        col.ExcelExportFormatString = "dd.MM.yyyy";
                        col.TextAlignment = ContentAlignment.MiddleCenter;
                        //myGrid.Columns[i].SetDefaultValueOverride()
                        break;

                    //--- INT
                    case "System.Int32":
                    case "System.Int64":
                        col.ExcelExportType = DisplayFormatType.Custom;
                        col.ExcelExportFormatString = "##0";
                        col.TextAlignment = ContentAlignment.MiddleLeft;
                        break;

                    //--- Decimal
                    case "System.Decimal":
                        col.ExcelExportType = DisplayFormatType.Custom;
                        col.ExcelExportFormatString = "#,##0.000";
                        col.TextAlignment = ContentAlignment.MiddleRight;
                        break;
                } //--- switch             
            }

            try
            {
                excelExporter.RunExport(fileName);
                this.frmMain.StatusBarWork(false, string.Empty);
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
                this.frmMain.StatusBarWork(false, "Übergabe NICHT erfolgreich...");

                clsError error = new clsError();
                error._GL_User = _GL_User;
                error.Aktion = "Excelexport";
                error.exceptText = ex.ToString();
                error.WriteError();
            }
        }

        ///<summary>clsExcelML / ExcelExporter_ExcelRowFormatting</summary>
        ///<remarks></remarks>
        private void ExcelExporter_ExcelRowFormatting(object sender, ExcelRowFormattingEventArgs e)
        {
            if (e.ExcelRowElement.Attributes.Contains("ss:Height"))
            {
                var att = e.ExcelRowElement.Attributes["ss:Height"];
                if (Convert.ToInt32(att) > 16)
                {
                    e.ExcelRowElement.Attributes["ss:Height"] = "16";
                }
            }
        }
        ///<summary>clsExcelML / Telerik_RunExportToExcelMLBSInfo4905</summary>
        ///<remarks>Excelexport für Telerikkomponente Grid</remarks>
        public void Telerik_RunExportToExcelMLBSInfo4905_SIL(RadGridView myGrid
                                               , string fileName
                                               , ref bool openExportFile
                                               , bool bAskToOpenInExcel
                                               , string strSheetName)
        {
            this.frmMain.ResetStatusBar();
            this.frmMain.InitStatusBar(3);
            this.frmMain.StatusBarWork(false, string.Empty);

            this.frmMain.StatusBarWork(false, "Daten werden übergeben...");
            //SIL möchte gerne ein zusätzliche Spalte hinzufgefügt haben
            //GridViewDataColumn ColAdd = new GridViewDataColumn("Zugang");
            if (!myGrid.Columns.Contains("Zugang"))
            {
                GridViewTextBoxColumn textBoxColumn = new GridViewTextBoxColumn("Zugang");
                myGrid.MasterTemplate.Columns.Add(textBoxColumn);
            }

            ExportToExcelML excelExporter = new ExportToExcelML(myGrid);
            excelExporter.SheetName = strSheetName;
            excelExporter.SummariesExportOption = SummariesOption.ExportAll;
            excelExporter.SheetMaxRows = ExcelMaxRows._1048576;
            excelExporter.ExportVisualSettings = true;
            excelExporter.ExcelTableCreated += excelExporter_ExcelTableCreated;
            excelExporter.ExcelCellFormatting += exporter_ExcelCellFormattingSIL;

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
                if (myGrid.Columns[i].DataType.ToString() == "System.Decimal")
                {
                    myGrid.Columns[i].ExcelExportType = DisplayFormatType.Custom;
                }
                string strColName = myGrid.Columns[i].Name;
                switch (strColName)
                {
                    case "Zugang":
                        myGrid.Columns.Move(i, 1);
                        break;
                    case "Nr":
                    case "Menge":
                    case "BS Brutto":
                    case "min.BS":
                    case "Diff":
                        myGrid.Columns[i].ExcelExportFormatString = "##0";
                        break;

                    case "LVSNr":
                        myGrid.Columns[i].TextAlignment = ContentAlignment.MiddleCenter;
                        break;

                    default:
                        myGrid.Columns[i].ExcelExportFormatString = "#,##0.00";
                        break;
                }
            }

            try
            {
                excelExporter.RunExport(fileName);
                this.frmMain.StatusBarWork(false, string.Empty);
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
                this.frmMain.StatusBarWork(false, "Übergabe NICHT erfolgreich...");

                clsError error = new clsError();
                error._GL_User = _GL_User;
                error.Aktion = "Excelexport";
                error.exceptText = ex.ToString();
            }
        }
        ///<summary>clsExcelML / Telerik_RunExportToExcelMLVDA4905SZG</summary>
        ///<remarks></remarks>
        public void Telerik_RunExportToExcelMLVDA4905_SZG(RadGridView myGrid
                                               , string fileName
                                               , ref bool openExportFile
                                               , bool bAskToOpenInExcel
                                               , string strSheetName)
        {
            this.frmMain.ResetStatusBar();
            this.frmMain.InitStatusBar(3);
            this.frmMain.StatusBarWork(false, string.Empty);

            this.frmMain.StatusBarWork(false, "Daten werden übergeben...");
            ExportToExcelML excelExporter = new ExportToExcelML(myGrid);
            excelExporter.SheetName = strSheetName;
            excelExporter.SummariesExportOption = SummariesOption.ExportAll;
            excelExporter.SheetMaxRows = ExcelMaxRows._1048576;
            excelExporter.ExportVisualSettings = true;
            excelExporter.HiddenColumnOption = HiddenOption.DoNotExport;
            excelExporter.ExcelTableCreated += excelExporter_ExcelTableCreated;
            excelExporter.ExcelCellFormatting += exporter_ExcelCellFormattingSZG;
            //excelExporter.ExcelRowFormatting += exporter_ExcelRowFormatting;

            //Hier können spezielle Formatierung für den Export vorgenommen werden
            //Datumformat im Export einstellen
            for (Int32 i = 0; i <= myGrid.Columns.Count - 1; i++)
            {
                string strDataTyp = myGrid.Columns[i].DataType.ToString();
                string strColName = myGrid.Columns[i].Name;

                switch (strDataTyp)
                {
                    case "System.string":
                    case "System.String":
                        myGrid.Columns[i].ExcelExportType = DisplayFormatType.Standard;
                        myGrid.Columns[i].TextAlignment = ContentAlignment.MiddleLeft;
                        break;

                    //--- DateTime
                    case "System.DateTime":
                        switch (strColName)
                        {
                            case "4905":
                            case "Prüfpunkt":
                                myGrid.Columns[i].ExcelExportType = DisplayFormatType.Custom;
                                myGrid.Columns[i].ExcelExportFormatString = "dd.MM.yyyy";
                                break;
                            default:
                                myGrid.Columns[i].ExcelExportType = DisplayFormatType.GeneralDate;
                                //myGrid.Columns[i].ExcelExportFormatString = "dd.MM.yyyy";
                                break;
                        }
                        break;

                    //--- Decimal
                    case "System.Decimal":
                        switch (strColName)
                        {
                            case "Nr":
                            case "Menge":
                            case "BS Brutto":
                            case "min.BS":
                            case "Diff":
                            case "ASNID":
                                myGrid.Columns[i].ExcelExportType = DisplayFormatType.Custom;
                                myGrid.Columns[i].ExcelExportFormatString = "##0";
                                break;
                            default:
                                myGrid.Columns[i].ExcelExportType = DisplayFormatType.Custom;
                                myGrid.Columns[i].ExcelExportFormatString = "#,##0.00";
                                break;
                        }
                        break;

                    //-- INT 32
                    case "System.Int32":
                    case "System.Int64":
                        switch (strColName)
                        {
                            case "Nr":
                            case "Menge":
                            case "BS Brutto":
                            case "min.BS":
                            case "Diff":
                            case "FZ dazu":
                            case "PP FZ dazu":
                            case "FZ Diff":
                            case "Bestand":
                            case "Ausgang":
                            case "B+A":
                            case "PP zu IST":
                                myGrid.Columns[i].ExcelExportType = DisplayFormatType.Custom;
                                myGrid.Columns[i].ExcelExportFormatString = "##0";
                                break;
                            default:
                                myGrid.Columns[i].ExcelExportType = DisplayFormatType.Custom;
                                myGrid.Columns[i].ExcelExportFormatString = "##0";
                                break;
                        }
                        break;

                    default:
                        break;
                }//switch
            } // for
            try
            {
                excelExporter.RunExport(fileName);
                this.frmMain.StatusBarWork(false, string.Empty);
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
                this.frmMain.StatusBarWork(false, "Übergabe NICHT erfolgreich...");

                clsError error = new clsError();
                error._GL_User = _GL_User;
                error.Aktion = "Excelexport";
                error.exceptText = ex.ToString();
            }
        }
        ///<summary>clsExcelML / excelExporter_ExcelTableCreated</summary>
        ///<remarks></remarks>
        private void excelExporter_ExcelTableCreated(object sender, global::Telerik.WinControls.UI.Export.ExcelML.ExcelTableCreatedEventArgs e)
        {
            if (this.ListHeaderText.Count > 0)
            {
                for (Int32 i = 0; i <= this.ListHeaderText.Count - 1; i++)
                {
                    Telerik.WinControls.UI.Export.ExcelML.SingleStyleElement style = ((ExportToExcelML)sender).AddCustomExcelRow(e.ExcelTableElement, 22, this.ListHeaderText[i]);
                    style.FontStyle.Bold = true;
                    style.FontStyle.Size = 12;
                    style.FontStyle.Color = Color.Black;
                    //style.InteriorStyle.Color = Color.Transparent;
                    style.InteriorStyle.Pattern = Telerik.WinControls.UI.Export.ExcelML.InteriorPatternType.Solid;
                    style.AlignmentElement.HorizontalAlignment = Telerik.WinControls.UI.Export.ExcelML.HorizontalAlignmentType.Left;
                    style.AlignmentElement.VerticalAlignment = Telerik.WinControls.UI.Export.ExcelML.VerticalAlignmentType.Top;
                }
            }
        }
        ///<summary>clsExcelML / exporter_ExcelCellFormattingSIL</summary>
        ///<remarks>Exportformatiuerng spezielle für Kunde SIL</remarks>
        private void exporter_ExcelCellFormattingDefault(object sender, Telerik.WinControls.UI.Export.ExcelML.ExcelCellFormattingEventArgs e)
        {
            if (e.GridRowIndex > -1)
            {
                //----Hintergrundfarbe
                object obType = e.GridCellInfo.RowInfo.RowElementType;
                if (obType.Equals(typeof(GridDataRowElement)))
                {
                    e.ExcelStyleElement.InteriorStyle.Color = Color.Transparent;
                }
                else if (obType.Equals(typeof(GridSummaryRowElement)))
                {
                    e.ExcelStyleElement.InteriorStyle.Color = Color.LightGray;
                }
                else if (obType.Equals(typeof(GridGroupHeaderRowElement)))
                {
                    e.ExcelStyleElement.InteriorStyle.Color = Color.Lavender;
                }
                else if (obType.Equals(typeof(GridTableHeaderRowElement)))
                {
                    e.ExcelStyleElement.InteriorStyle.Color = Color.CornflowerBlue;
                }
                else //(obType.Equals(typeof(GridTableHeaderRowElement)))
                {
                    e.ExcelStyleElement.InteriorStyle.Color = Color.CornflowerBlue;
                }

                //--- Trim Inhalt
                if (e.GridCellInfo.RowInfo.Cells[e.GridColumnIndex].Value != null)
                {
                    try
                    {
                        if (
                             (e.GridCellInfo.RowInfo.Cells[e.GridColumnIndex].Value != DBNull.Value) &&
                             (e.GridCellInfo.RowInfo.Cells[e.GridColumnIndex].Value != string.Empty)
                           )
                        {
                            if (
                                 (!e.ExcelCellElement.Data.DataType.ToString().Equals("DateTime")) &&
                                 (!e.ExcelCellElement.Data.DataType.ToString().Equals(string.Empty))
                               )
                            {
                                string strCellValue = string.Empty;
                                strCellValue = e.GridCellInfo.RowInfo.Cells[e.GridColumnIndex].Value.ToString().Trim();
                                e.GridCellInfo.RowInfo.Cells[e.GridColumnIndex].Value = strCellValue;
                            }
                            //else
                            //{ 
                            //  string strCellValue = string.Empty;
                            //}
                        }
                    }
                    catch (Exception ex)
                    {
                        clsError error = new clsError();
                        error._GL_User = _GL_User;
                        error.Aktion = "Excelexport - exporter_ExcelCellFormattingDefault";
                        error.exceptText = ex.ToString();
                        error.WriteError();
                    }
                }
            }
        }
        ///<summary>clsExcelML / exporter_ExcelCellFormattingSIL</summary>
        ///<remarks>Exportformatiuerng spezielle für Kunde SIL</remarks>
        private void exporter_ExcelCellFormattingSIL(object sender, Telerik.WinControls.UI.Export.ExcelML.ExcelCellFormattingEventArgs e)
        {
            if (e.GridRowIndex > -1)
            {
                decimal decFaktor = 0;
                Color CellBackColor = Color.Transparent;
                Color CellFontColor = Color.Black;
                if (e.GridCellInfo.RowInfo.Cells["Faktor"] != null)
                {
                    decimal.TryParse(e.GridCellInfo.RowInfo.Cells["Faktor"].Value.ToString(), out decFaktor);
                    CellBackColor = Functions.GetBackgroudColorBSInfo4905SIL(decFaktor);
                }
                e.ExcelStyleElement.InteriorStyle.Color = CellBackColor;

                //switch (e.GridCellInfo.ColumnInfo.UniqueName)
                switch (e.GridCellInfo.ColumnInfo.Name)
                {
                    case "MB":
                        e.ExcelStyleElement.FontStyle.Color = Functions.GetFontColorBSInfo4905SIL(decFaktor);
                        e.ExcelStyleElement.FontStyle.Bold = true;
                        e.ExcelStyleElement.AlignmentElement.VerticalAlignment = Telerik.WinControls.UI.Export.ExcelML.VerticalAlignmentType.Center;
                        e.ExcelCellElement.Data.DataItem = Functions.GetTxtBSInfo4905SIL(decFaktor);
                        break;
                }
            }
        }
        ///<summary>clsExcelML / exporter_ExcelCellFormatting</summary>
        ///<remarks></remarks>
        private void exporter_ExcelCellFormattingSZG(object sender, Telerik.WinControls.UI.Export.ExcelML.ExcelCellFormattingEventArgs e)
        {
            if (e.GridRowIndex > -1)
            {
                string strTmp = string.Empty;

                switch (e.GridCellInfo.ColumnInfo.Name)
                {
                    case "Prüfpunkt":
                    case "4905":
                        strTmp = e.GridCellInfo.RowInfo.Cells[e.GridCellInfo.ColumnInfo.Name].Value.ToString();
                        DateTime dateTmp = Globals.DefaultDateTimeMinValue;
                        if (DateTime.TryParse(strTmp, out dateTmp))
                        {
                            strTmp = string.Empty;
                            if (dateTmp > Globals.DefaultDateTimeMinValue)
                            {
                                strTmp = dateTmp.ToShortDateString();
                                e.ExcelCellElement.Data.DataType = Telerik.WinControls.Export.DataType.DateTime;
                                e.ExcelCellElement.Data.DataItem = dateTmp;
                            }
                            else
                            {
                                strTmp = string.Empty;
                                e.ExcelCellElement.Data.DataType = Telerik.WinControls.Export.DataType.String;
                                e.ExcelCellElement.Data.DataItem = strTmp;
                            }
                        }
                        else
                        {
                            strTmp = string.Empty;
                            e.ExcelCellElement.Data.DataType = Telerik.WinControls.Export.DataType.String;
                            e.ExcelCellElement.Data.DataItem = strTmp;
                        }
                        //e.ExcelCellElement.Data.DataType = Telerik.WinControls.Export.DataType.String;
                        //e.ExcelCellElement.Data.DataItem = strTmp;
                        break;

                    case "MB":
                        break;
                }
            }
        }
    }
}



