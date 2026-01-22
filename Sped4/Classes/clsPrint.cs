using LVS;
using LVS.ReportClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

using Telerik.WinControls.Data;
using Telerik.WinControls.UI;
//using Telerik.WinControls.RichTextBox;

namespace Sped4.Classes
{
    class clsPrint
    {
        //public Globals._GL_SYSTEM _GL_System;
        public Globals._GL_USER _GL_User;
        private Globals._GL_SYSTEM _GL_System;
        // Ausbau .. eigene Views erzeugen un drucken .. 

        internal string vName;
        internal Int32 vId;
        internal RadGridView dgv;
        internal DataTable dtable;
        internal DataTable dtSource;
        internal ctrMenu ctrMenu;
        internal FilterDescriptorCollection dgvFilters;
        internal GroupDescriptorCollection dgvGroups;
        internal SortDescriptorCollection dgvSorts;
        public string printTitel { get; set; }
        public string SelectedView { get; set; }
        public DateTime Stichtag = DateTime.Now;

        public static List<string> getPrinter()
        {
            return new List<string>();
        }

        //public clsPrint(RadGridView _dgv, Globals._GL_SYSTEM mySystem, string name = "Bestand", Int32 viewId = 1)
        public clsPrint(RadGridView _dgv, Globals._GL_SYSTEM mySystem, string name, Int32 viewId = 1)
        {
            _GL_System = mySystem;
            vName = name;
            vId = viewId;
            printTitel = "";
            dgv = _dgv;

        }
        //public clsPrint(DataTable _dt, Globals._GL_SYSTEM mySystem, string name = "Bestand", Int32 viewId = 1)
        public clsPrint(DataTable _dt, Globals._GL_SYSTEM mySystem, string name, Int32 viewId = 1)
        {

            _GL_System = mySystem;
            vName = name;
            vId = viewId;
            printTitel = "";
            dtable = _dt;
            clsRepBestand.dtGridToReport = dtable;
        }

        public clsPrint(RadGridView myGrid, ctrMenu myMenu, string name, Int32 viewId = 1)
        {
            this.ctrMenu = myMenu;
            this._GL_System = this.ctrMenu._frmMain.GL_System;
            this._GL_User = this.ctrMenu._frmMain.GL_User;
            vName = name;
            vId = viewId;
            printTitel = "";
            //dtSource = ((DataTable)myGrid.DataSource).Copy();
            dtSource = ((DataTable)myGrid.DataSource).Select(myGrid.FilterDescriptors.Expression).CopyToDataTable();
            clsRepBestand.dtGridToReport = dtSource.Copy();
            dtable = dtSource.Copy();
            //--Filter
            dgvFilters = new FilterDescriptorCollection();
            foreach (FilterDescriptor item in myGrid.FilterDescriptors)
            {
                dgvFilters.Add(item);
            }
            //---Groups
            dgvGroups = new GroupDescriptorCollection();
            foreach (GroupDescriptor item in myGrid.GroupDescriptors)
            {
                dgvGroups.Add(item);
            }
            //---Sorts
            dgvSorts = new SortDescriptorCollection();
            foreach (SortDescriptor item in myGrid.SortDescriptors)
            {
                dgvSorts.Add(item);
            }

        }

        public bool PrintGrid(bool showPrinterDialog, bool bPrintByGridFunction)
        {
            dtable = null;

            bool bPrinted = false;
            Dictionary<string, Dictionary<string, List<string>>> DictPrintViews = _GL_System.DictPrintViews;

            Dictionary<string, List<string>> dictCheck;
            DictPrintViews.TryGetValue(vName, out dictCheck);

            //if (DictPrintViews.GetValueOrNull(vName) != null && DictPrintViews.GetValueOrNull(vName).Count > 0)
            if (dictCheck != null && dictCheck.Count > 0)
            {
                //Dictionary<string, List<string>> tmpDict = DictPrintViews.GetValueOrNull(vName);

                Dictionary<string, List<string>> tmpDict;
                DictPrintViews.TryGetValue(vName, out tmpDict);

                if (tmpDict != null && tmpDict.Count > 0)
                {
                    DialogResult res = System.Windows.Forms.DialogResult.OK;
                    frmDialog ps;
                    if (dtSource != null)
                        ps = new frmDialog(new ctrGridPrinter(dtSource, this._GL_System, vName, SelectedView), ctrMenu);
                    else
                    {
                        DataTable dt1 = ((DataTable)dgv.DataSource).Copy();
                        dgv = dgv;
                        ps = new frmDialog(new ctrGridPrinter(dgv, this._GL_System, vName, SelectedView), ctrMenu);
                    }
                    if (ps.ctrGridPrinter.dgv != null)
                    {
                        //---Sortierung 
                        ps.ctrGridPrinter.dgv.SortDescriptors.Clear();
                        if (this.dgvSorts is SortDescriptorCollection)
                        {
                            ps.ctrGridPrinter.dgv.EnableSorting = true;
                            ps.ctrGridPrinter.dgv.MasterTemplate.EnableSorting = ps.ctrGridPrinter.dgv.EnableSorting;
                            foreach (SortDescriptor item in this.dgvSorts)
                            {
                                ps.ctrGridPrinter.dgv.SortDescriptors.Add(item);
                            }
                        }
                        //---Filter
                        ps.ctrGridPrinter.dgv.FilterDescriptors.Clear();
                        if (this.dgvFilters is FilterDescriptorCollection)
                        {
                            ps.ctrGridPrinter.dgv.EnableFiltering = true;
                            ps.ctrGridPrinter.dgv.MasterTemplate.EnableFiltering = ps.ctrGridPrinter.dgv.EnableFiltering;
                            foreach (FilterDescriptor item in this.dgvFilters)
                            {
                                ps.ctrGridPrinter.dgv.FilterDescriptors.Add(item);
                            }
                        }
                        // ---- Gruppierung                        
                        ps.ctrGridPrinter.dgv.GroupDescriptors.Clear();
                        if (this.dgvGroups is GroupDescriptorCollection)
                        {
                            ps.ctrGridPrinter.dgv.EnableGrouping = true;
                            ps.ctrGridPrinter.dgv.MasterTemplate.EnableGrouping = ps.ctrGridPrinter.dgv.EnableGrouping;
                            foreach (GroupDescriptor item in this.dgvGroups)
                            {
                                ps.ctrGridPrinter.dgv.GroupDescriptors.Add(item);
                            }
                        }
                        //
                        if (ps.ctrGridPrinter.dgv.Columns.Count > 0)
                        {
                            //erstellt Summary Row
                            if (ctrMenu._frmMain.system.Client.Modul.Telerik_GridPrint_SummaryRow)
                            {
                                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                                string ColName = ps.ctrGridPrinter.dgv.Columns[0].Name.ToString();
                                GridViewSummaryItem sumDaten = new GridViewSummaryItem(ColName, "Anz.: {0}", GridAggregateFunction.Count);
                                summaryRowItem.Add(sumDaten);
                                if (ps.ctrGridPrinter.dgv.Columns.Contains("Netto"))
                                {
                                    GridViewSummaryItem sumNetto = new GridViewSummaryItem("Netto", "{0:N3}", GridAggregateFunction.Sum);
                                    summaryRowItem.Add(sumNetto);
                                }
                                if (ps.ctrGridPrinter.dgv.Columns.Contains("Brutto"))
                                {
                                    GridViewSummaryItem sumBrutto = new GridViewSummaryItem("Brutto", "{0:N3}", GridAggregateFunction.Sum);
                                    summaryRowItem.Add(sumBrutto);
                                }
                                ps.ctrGridPrinter.dgv.SummaryRowsTop.Add(summaryRowItem);
                            }
                        }
                        ManipulateGrid(ref ps.ctrGridPrinter.dgv);
                    }

                    if (showPrinterDialog)
                    {
                        res = ps.ShowDialog();
                    }
                    else
                    {
                        ps.ctrGridPrinter.isQuerformat = true;
                    }

                    if (res == System.Windows.Forms.DialogResult.OK)
                    {
                        int headerFontSize = 8;
                        int cellFontSize = 7;
                        //bPrinted = CreateAndPrintDocumten(ps, headerFontSize, cellFontSize, bPrintByGridFunction, tmpDict.GetValueOrNull(ps.ctrGridPrinter.selectedView));

                        List<string> tmpList;
                        tmpDict.TryGetValue(ps.ctrGridPrinter.selectedView, out tmpList);
                        bPrinted = CreateAndPrintDocumten(ps, headerFontSize, cellFontSize, bPrintByGridFunction, tmpList);
                        //string view = ps.ctrGridPrinter.selectedView;
                        //List<string> tmpList = tmpDict.GetValueOrNull(view);
                        //if (tmpList != null && tmpList.Count > 0)
                        //{
                        //    DateTime dt = DateTime.Now;
                        //    string dateTime = String.Format("{0:dd/MM/yyyy}", dt);
                        //    string dateTimeFN = String.Format("{0:yyyyMMdd_HHmmss}", dt);

                        //    int headerFontSize = 8;
                        //    int cellFontSize = 7;

                        //    RadPrintDocument document = new RadPrintDocument();
                        //    document.DefaultPageSettings.Landscape = ps.ctrGridPrinter.isQuerformat;
                        //    document.DefaultPageSettings.Margins.Bottom = 30;
                        //    document.DefaultPageSettings.Margins.Left = 50;
                        //    document.DefaultPageSettings.Margins.Right = 70;
                        //    document.DefaultPageSettings.Margins.Top = 30;

                        //    TableViewDefinitionPrintRenderer renderer = new TableViewDefinitionPrintRenderer(dgv);
                        //    List<GridViewColumn> listGVC = new List<GridViewColumn>();
                        //    ps.ctrGridPrinter.dgv.PrintStyle.FitWidthMode = PrintFitWidthMode.FitPageWidth;

                        //    document.HeaderFont = new System.Drawing.Font("Verdana", 10, System.Drawing.FontStyle.Bold);
                        //    document.DocumentName = vName + "_" + dateTimeFN;
                        //    document.HeaderHeight = document.HeaderHeight + 10;
                        //    document.LeftHeader = printTitel; // +dateTime;//vName + " / " + printTitel + " / " + dateTime; 24.07.2014 anpassung Header ... 
                        //    document.MiddleFooter = "Seite [Page #] von [Total Pages]";
                        //    document.RightFooter = "Benutzer: " + this._GL_User.Vorname + " " + this._GL_User.Name + " - " + DateTime.Now.ToString();
                        //    document.PrinterSettings.PrinterName = ps.ctrGridPrinter.selectedPrinter;

                        //    ps.ctrGridPrinter.dgv.PrintStyle.HeaderCellBackColor = System.Drawing.Color.Transparent;
                        //    ps.ctrGridPrinter.dgv.PrintStyle.BorderColor = System.Drawing.Color.Black;
                        //    ps.ctrGridPrinter.dgv.PrintStyle.PrintHiddenRows = false;
                        //    ps.ctrGridPrinter.dgv.PrintStyle.PrintGrouping = true;


                        //    ps.ctrGridPrinter.dgv.PrintStyle.CellFont = new System.Drawing.Font("Arial", cellFontSize, System.Drawing.FontStyle.Regular);
                        //    ps.ctrGridPrinter.dgv.PrintStyle.HeaderCellFont = new System.Drawing.Font("Arial", headerFontSize, System.Drawing.FontStyle.Bold);
                        //    ps.ctrGridPrinter.dgv.PrintStyle.SummaryCellFont = new System.Drawing.Font("Arial", headerFontSize, System.Drawing.FontStyle.Regular);
                        //    ps.ctrGridPrinter.dgv.PrintStyle.SummaryCellBackColor = System.Drawing.Color.Transparent;
                        //    ps.ctrGridPrinter.dgv.PrintStyle.PrintHeaderOnEachPage = true;

                        //    // if(vId>0)
                        //    //ps.ctrGridPrinter.dgv.Columns.Add("Aufgabe");
                        //    if (bPrintByGridFunction) // Bedingung
                        //    {
                        //        try
                        //        {
                        //            ps.ctrGridPrinter.dgv.Print(false, document);

                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            string strEx = ex.ToString();
                        //        }
                        //    }
                        //    else
                        //    {
                        //        ObservableCollection<clsReportArtikel> temp = clsRepBestand.GetData();
                        //        clsRepBestand.dtGridToReport = clsRepBestand.dtGridToReport;
                        //        Telerik.Reporting.UriReportSource uriReportSource1 = new Telerik.Reporting.UriReportSource();

                        //        string strReportFile = string.Empty;
                        //        switch (vName)
                        //        {
                        //            case "Bestand":
                        //                strReportFile = this._GL_System.docPath_ViewPrintBestand;
                        //                break;

                        //            case "Journal":
                        //                strReportFile = this._GL_System.docPath_ViewPrintJournal;
                        //                break;
                        //        }
                        //        if (!strReportFile.Equals(string.Empty))
                        //        {
                        //            uriReportSource1.Uri = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + strReportFile; // "\\Reports\\DruckListeSIL.trdx";
                        //            //uriReportSource1.Uri = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Reports\\DruckListeSIL.trdx";
                        //            uriReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Titel", printTitel));
                        //            //uriReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Datum", DateTime.Now.ToShortDateString()));
                        //            uriReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Datum", this.Stichtag.ToShortDateString()));

                        //            System.Drawing.Printing.PrinterSettings ps1 = new System.Drawing.Printing.PrinterSettings();
                        //            System.Drawing.Printing.PrintController standardPrintController = new System.Drawing.Printing.StandardPrintController();
                        //            // Print the report using the custom print controller
                        //            Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
                        //            reportProcessor.PrintController = standardPrintController;

                        //            if (ps1 != null)
                        //            {
                        //                try
                        //                {
                        //                    reportProcessor.PrintReport(uriReportSource1, ps1);
                        //                }
                        //                catch (Exception ex)
                        //                {
                        //                    string strError = ex.ToString();
                        //                }
                        //            }
                        //            bPrinted = true;
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    // dgv.Print(showPrinterDialog, document);
                        //}
                    }
                }
                else
                {
                    clsMessages.Print_NoView();
                }
            }
            else
            {
                clsMessages.Print_NoView();
            }
            return bPrinted;
        }


        public bool Print(bool showPrinterDialog, bool bPrintByGridFunction, ctrMenu ctrMenu, Telerik.WinControls.Data.FilterDescriptorCollection filter = null)
        {
            bool bPrinted = false;
            Dictionary<string, Dictionary<string, List<string>>> DictPrintViews = _GL_System.DictPrintViews;

            Dictionary<string, List<string>> dictCheck;
            DictPrintViews.TryGetValue(vName, out dictCheck);

            //if (DictPrintViews.GetValueOrNull(vName) != null && DictPrintViews.GetValueOrNull(vName).Count > 0)
            if (dictCheck != null && dictCheck.Count > 0)
            {
                //Dictionary<string, List<string>> tmpDict = DictPrintViews.GetValueOrNull(vName);
                Dictionary<string, List<string>> tmpDict;
                DictPrintViews.TryGetValue(vName, out tmpDict);
                if (tmpDict != null && tmpDict.Count > 0)
                {
                    DialogResult res = System.Windows.Forms.DialogResult.OK;
                    frmDialog ps;
                    if (dtable != null)
                        ps = new frmDialog(new ctrGridPrinter(dtable, this._GL_System, vName, SelectedView), ctrMenu);
                    else
                    {
                        DataTable dt1 = ((DataTable)dgv.DataSource).Copy();
                        dgv = dgv;
                        ps = new frmDialog(new ctrGridPrinter(dgv, this._GL_System, vName, SelectedView), ctrMenu);
                    }
                    if (ps.ctrGridPrinter.dgv != null)
                    {
                        //Sort Descriptors
                        if (ctrMenu._ctrBestand != null)
                        {
                            ps.ctrGridPrinter.dgv.MasterTemplate.SortDescriptors.Clear();
                            for (Int32 i = 0; i <= ctrMenu._ctrBestand.dgv.MasterTemplate.SortDescriptors.Count - 1; i++)
                            {
                                SortDescriptor tmpSort = ctrMenu._ctrBestand.dgv.MasterTemplate.SortDescriptors[i];
                                ps.ctrGridPrinter.dgv.MasterTemplate.SortDescriptors.Add(tmpSort);
                            }
                        }
                        if (ctrMenu._ctrJournal != null)
                        {
                            ps.ctrGridPrinter.dgv.MasterTemplate.SortDescriptors.Clear();
                            for (Int32 i = 0; i <= ctrMenu._ctrJournal.rDgv.MasterTemplate.SortDescriptors.Count - 1; i++)
                            {
                                SortDescriptor tmpSort = ctrMenu._ctrJournal.rDgv.MasterTemplate.SortDescriptors[i];
                                ps.ctrGridPrinter.dgv.MasterTemplate.SortDescriptors.Add(tmpSort);
                            }
                        }

                        //
                        if (ps.ctrGridPrinter.dgv.Columns.Count > 0)
                        {
                            //erstellt Summary Row
                            if (ctrMenu._frmMain.system.Client.Modul.Telerik_GridPrint_SummaryRow)
                            {
                                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                                string ColName = ps.ctrGridPrinter.dgv.Columns[0].Name.ToString();
                                GridViewSummaryItem sumDaten = new GridViewSummaryItem(ColName, "Anz.: {0}", GridAggregateFunction.Count);
                                summaryRowItem.Add(sumDaten);
                                if (ps.ctrGridPrinter.dgv.Columns.Contains("Netto"))
                                {
                                    GridViewSummaryItem sumNetto = new GridViewSummaryItem("Netto", "{0:N3}", GridAggregateFunction.Sum);
                                    summaryRowItem.Add(sumNetto);
                                }
                                if (ps.ctrGridPrinter.dgv.Columns.Contains("Brutto"))
                                {
                                    GridViewSummaryItem sumBrutto = new GridViewSummaryItem("Brutto", "{0:N3}", GridAggregateFunction.Sum);
                                    summaryRowItem.Add(sumBrutto);
                                }
                                ps.ctrGridPrinter.dgv.SummaryRowsTop.Add(summaryRowItem);
                            }
                        }
                        ManipulateGrid(ref ps.ctrGridPrinter.dgv);
                    }

                    if (filter != null)
                    {
                        for (Int32 x = 0; x < filter.Count; x++)
                        {
                            ps.ctrGridPrinter.dgv.FilterDescriptors.Add(filter[x]);
                        }
                    }
                    if (showPrinterDialog)
                    {
                        res = ps.ShowDialog();
                    }
                    else
                    {
                        ps.ctrGridPrinter.isQuerformat = true;
                    }

                    if (res == System.Windows.Forms.DialogResult.OK)
                    {
                        int headerFontSize = 8;
                        int cellFontSize = 7;

                        //bPrinted = CreateAndPrintDocumten(ps, headerFontSize, cellFontSize, bPrintByGridFunction, tmpDict.GetValueOrNull(ps.ctrGridPrinter.selectedView));

                        List<string> tmpList;
                        tmpDict.TryGetValue(ps.ctrGridPrinter.selectedView, out tmpList);
                        bPrinted = CreateAndPrintDocumten(ps, headerFontSize, cellFontSize, bPrintByGridFunction, tmpList);
                        //string view = ps.ctrGridPrinter.selectedView;
                        //List<string> tmpList = tmpDict.GetValueOrNull(view);
                        //if (tmpList != null && tmpList.Count > 0)
                        //{
                        //    DateTime dt = DateTime.Now;
                        //    string dateTime = String.Format("{0:dd/MM/yyyy}", dt);
                        //    string dateTimeFN = String.Format("{0:yyyyMMdd_HHmmss}", dt);

                        //    int headerFontSize = 8;
                        //    int cellFontSize = 7;

                        //    RadPrintDocument document = new RadPrintDocument();
                        //    document.DefaultPageSettings.Landscape = ps.ctrGridPrinter.isQuerformat;
                        //    document.DefaultPageSettings.Margins.Bottom = 30;
                        //    document.DefaultPageSettings.Margins.Left = 50;
                        //    document.DefaultPageSettings.Margins.Right = 70;
                        //    document.DefaultPageSettings.Margins.Top = 30;

                        //    TableViewDefinitionPrintRenderer renderer = new TableViewDefinitionPrintRenderer(dgv);
                        //    List<GridViewColumn> listGVC = new List<GridViewColumn>();
                        //    ps.ctrGridPrinter.dgv.PrintStyle.FitWidthMode = PrintFitWidthMode.FitPageWidth;

                        //    document.HeaderFont = new System.Drawing.Font("Verdana", 10, System.Drawing.FontStyle.Bold);
                        //    document.DocumentName = vName + "_" + dateTimeFN;
                        //    document.HeaderHeight = document.HeaderHeight + 10;
                        //    document.LeftHeader = printTitel; // +dateTime;//vName + " / " + printTitel + " / " + dateTime; 24.07.2014 anpassung Header ... 
                        //    document.MiddleFooter = "Seite [Page #] von [Total Pages]";
                        //    document.RightFooter = "Benutzer: " + this._GL_User.Vorname + " " + this._GL_User.Name + " - " + DateTime.Now.ToString();
                        //    document.PrinterSettings.PrinterName = ps.ctrGridPrinter.selectedPrinter;                          

                        //    ps.ctrGridPrinter.dgv.PrintStyle.HeaderCellBackColor = System.Drawing.Color.Transparent;
                        //    ps.ctrGridPrinter.dgv.PrintStyle.BorderColor = System.Drawing.Color.Black;
                        //    ps.ctrGridPrinter.dgv.PrintStyle.PrintHiddenRows = false;
                        //    ps.ctrGridPrinter.dgv.PrintStyle.PrintGrouping = true;


                        //    ps.ctrGridPrinter.dgv.PrintStyle.CellFont = new System.Drawing.Font("Arial", cellFontSize, System.Drawing.FontStyle.Regular);
                        //    ps.ctrGridPrinter.dgv.PrintStyle.HeaderCellFont = new System.Drawing.Font("Arial", headerFontSize, System.Drawing.FontStyle.Bold);
                        //    ps.ctrGridPrinter.dgv.PrintStyle.SummaryCellFont = new System.Drawing.Font("Arial", headerFontSize, System.Drawing.FontStyle.Regular);
                        //    ps.ctrGridPrinter.dgv.PrintStyle.SummaryCellBackColor = System.Drawing.Color.Transparent;
                        //    ps.ctrGridPrinter.dgv.PrintStyle.PrintHeaderOnEachPage = true;

                        //    // if(vId>0)
                        //    //ps.ctrGridPrinter.dgv.Columns.Add("Aufgabe");
                        //    if (bPrintByGridFunction) // Bedingung
                        //    {
                        //        try
                        //        {
                        //            ps.ctrGridPrinter.dgv.Print(false, document);

                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            string strEx = ex.ToString();
                        //        }
                        //    }
                        //    else
                        //    {
                        //        ObservableCollection<clsReportArtikel> temp = clsRepBestand.GetData();
                        //        clsRepBestand.dtGridToReport = clsRepBestand.dtGridToReport;
                        //        Telerik.Reporting.UriReportSource uriReportSource1 = new Telerik.Reporting.UriReportSource();

                        //        string strReportFile = string.Empty;
                        //        switch (vName)
                        //        {
                        //            case "Bestand":
                        //                strReportFile = this._GL_System.docPath_ViewPrintBestand;
                        //                break;

                        //            case "Journal":
                        //                strReportFile = this._GL_System.docPath_ViewPrintJournal;
                        //                break;
                        //        }
                        //        if (!strReportFile.Equals(string.Empty))
                        //        {
                        //            uriReportSource1.Uri = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + strReportFile; // "\\Reports\\DruckListeSIL.trdx";
                        //            //uriReportSource1.Uri = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Reports\\DruckListeSIL.trdx";
                        //            uriReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Titel", printTitel));
                        //            //uriReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Datum", DateTime.Now.ToShortDateString()));
                        //            uriReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Datum", this.Stichtag.ToShortDateString()));

                        //            System.Drawing.Printing.PrinterSettings ps1 = new System.Drawing.Printing.PrinterSettings();
                        //            System.Drawing.Printing.PrintController standardPrintController = new System.Drawing.Printing.StandardPrintController();
                        //            // Print the report using the custom print controller
                        //            Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
                        //            reportProcessor.PrintController = standardPrintController;

                        //            if (ps1 != null)
                        //            {
                        //                try
                        //                {
                        //                    reportProcessor.PrintReport(uriReportSource1, ps1);
                        //                }
                        //                catch (Exception ex)
                        //                {
                        //                    string strError = ex.ToString();
                        //                }
                        //            }
                        //            bPrinted = true;
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    // dgv.Print(showPrinterDialog, document);
                        //}
                    }
                }
                else
                {
                    clsMessages.Print_NoView();
                }
            }
            else
            {
                clsMessages.Print_NoView();
            }
            return bPrinted;
        }

        private bool CreateAndPrintDocumten(frmDialog myDialog, Int32 myHeaderFontSize, Int32 myCellFontSize, bool bPrintByGridFunction, List<string> myListView)
        {
            bool bReturn = false;
            string view = myDialog.ctrGridPrinter.selectedView;
            List<string> tmpList = myListView;
            if (tmpList != null && tmpList.Count > 0)
            {
                DateTime dt = DateTime.Now;
                string dateTime = String.Format("{0:dd/MM/yyyy}", dt);
                string dateTimeFN = String.Format("{0:yyyyMMdd_HHmmss}", dt);

                //int headerFontSize = 8;
                //int cellFontSize = 7;

                RadPrintDocument document = new RadPrintDocument();
                document.DefaultPageSettings.Landscape = myDialog.ctrGridPrinter.isQuerformat;
                document.DefaultPageSettings.Margins.Bottom = 30;
                document.DefaultPageSettings.Margins.Left = 50;
                document.DefaultPageSettings.Margins.Right = 70;
                document.DefaultPageSettings.Margins.Top = 30;

                TableViewDefinitionPrintRenderer renderer = new TableViewDefinitionPrintRenderer(dgv);
                List<GridViewColumn> listGVC = new List<GridViewColumn>();
                myDialog.ctrGridPrinter.dgv.PrintStyle.FitWidthMode = PrintFitWidthMode.FitPageWidth;

                document.HeaderFont = new System.Drawing.Font("Verdana", 10, System.Drawing.FontStyle.Bold);
                document.DocumentName = vName + "_" + dateTimeFN;
                document.HeaderHeight = document.HeaderHeight + 10;
                document.LeftHeader = printTitel; // +dateTime;//vName + " / " + printTitel + " / " + dateTime; 24.07.2014 anpassung Header ... 
                document.MiddleFooter = "Seite [Page #] von [Total Pages]";
                document.RightFooter = "Benutzer: " + this._GL_User.Vorname + " " + this._GL_User.Name + " - " + DateTime.Now.ToString();
                document.PrinterSettings.PrinterName = myDialog.ctrGridPrinter.selectedPrinter;

                myDialog.ctrGridPrinter.dgv.PrintStyle.HeaderCellBackColor = System.Drawing.Color.Transparent;
                myDialog.ctrGridPrinter.dgv.PrintStyle.BorderColor = System.Drawing.Color.Black;
                myDialog.ctrGridPrinter.dgv.PrintStyle.PrintHiddenRows = false;
                myDialog.ctrGridPrinter.dgv.PrintStyle.PrintGrouping = true;


                myDialog.ctrGridPrinter.dgv.PrintStyle.CellFont = new System.Drawing.Font("Arial", myCellFontSize, System.Drawing.FontStyle.Regular);
                myDialog.ctrGridPrinter.dgv.PrintStyle.HeaderCellFont = new System.Drawing.Font("Arial", myHeaderFontSize, System.Drawing.FontStyle.Bold);
                myDialog.ctrGridPrinter.dgv.PrintStyle.SummaryCellFont = new System.Drawing.Font("Arial", myHeaderFontSize, System.Drawing.FontStyle.Regular);
                myDialog.ctrGridPrinter.dgv.PrintStyle.SummaryCellBackColor = System.Drawing.Color.Transparent;
                myDialog.ctrGridPrinter.dgv.PrintStyle.PrintHeaderOnEachPage = true;

                if (bPrintByGridFunction) // Bedingung
                {
                    try
                    {
                        myDialog.ctrGridPrinter.dgv.Print(false, document);
                        bReturn = true;
                    }
                    catch (Exception ex)
                    {
                        string strEx = ex.ToString();
                    }
                }
                else
                {


                    System.Collections.ObjectModel.ObservableCollection<clsReportArtikel> temp = clsRepBestand.GetData();
                    clsRepBestand.dtGridToReport = clsRepBestand.dtGridToReport;
                    Telerik.Reporting.UriReportSource uriReportSource1 = new Telerik.Reporting.UriReportSource();

                    string strReportFile = string.Empty;
                    switch (vName)
                    {
                        case "Bestand":
                            strReportFile = this._GL_System.docPath_ViewPrintBestand;
                            break;

                        case "Journal":
                            strReportFile = this._GL_System.docPath_ViewPrintJournal;
                            break;
                    }
                    if (!strReportFile.Equals(string.Empty))
                    {
                        uriReportSource1.Uri = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + strReportFile; // "\\Reports\\DruckListeSIL.trdx";
                                                                                                                                                            //uriReportSource1.Uri = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Reports\\DruckListeSIL.trdx";
                        uriReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Titel", printTitel));
                        //uriReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Datum", DateTime.Now.ToShortDateString()));
                        uriReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Datum", this.Stichtag.ToShortDateString()));

                        System.Drawing.Printing.PrinterSettings ps1 = new System.Drawing.Printing.PrinterSettings();
                        System.Drawing.Printing.PrintController standardPrintController = new System.Drawing.Printing.StandardPrintController();
                        // Print the report using the custom print controller
                        Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
                        reportProcessor.PrintController = standardPrintController;

                        if (ps1 != null)
                        {
                            try
                            {
                                reportProcessor.PrintReport(uriReportSource1, ps1);
                            }
                            catch (Exception ex)
                            {
                                string strError = ex.ToString();
                            }
                        }
                        bReturn = true;
                    }
                }
            }
            return bReturn;
        }


        private void ManipulateGrid(ref RadGridView dgv)
        {
            for (Int32 i = 0; i <= dgv.Columns.Count - 1; i++)
            {
                string strColName = dgv.Columns[i].Name;
                switch (strColName)
                {
                    case "LVSNr":
                    case "Reihe":
                        dgv.Columns[i].Width = 20;
                        break;

                    case "neueReihe":
                    case "n.Reihe":
                        //dgv.Columns[i].Name = "n.Reihe";
                        dgv.Columns[i].Width = 20;
                        break;

                    case "Breite":
                    case "Länge":
                    case "Laenge":
                    case "Höhe":
                    case "Hoehe":
                    case "Ausgang":
                        dgv.Columns[i].Width = 20;
                        break;

                    case "iO":
                        dgv.Columns[i].Width = 10;
                        break;

                    case "Dicke":
                        dgv.Columns[i].Width = 20;
                        break;

                    case "Gut":
                        dgv.Columns[i].Width = 40;
                        break;

                    case "Brutto":
                        dgv.Columns[i].Width = 40;
                        break;

                    case "Netto":
                        dgv.Columns[i].Width = 30;
                        break;

                    case "Eingangsdatum":
                        dgv.Columns[i].Width = 30;
                        //dgv.Columns[i].Name = "E-Datum";
                        break;

                    case "Ausgangsdatum":
                        dgv.Columns[i].Width = 30;
                        //dgv.Columns[i].Name = "A-Datum";
                        break;

                    case "Werksnummer":
                        dgv.Columns[i].Width = 30;
                        //dgv.Columns[i].Name = "Werks-Nr";
                        break;
                    case "Produktionsnummer":
                        dgv.Columns[i].Width = 30;
                        //dgv.Columns[i].Name = "Prod-Nr";
                        break;

                    case "Schaden":
                        dgv.Columns[i].Width = 50;
                        break;

                    default:
                        dgv.Columns[i].Width = 30;
                        break;
                }
            }
        }


        //        private void ManipulateGrid(ref RadGridView dgv)
        //        {
        //            for (Int32 i = 0; i <= dgv.Columns.Count - 1; i++)
        //            {
        //                string strColName = dgv.Columns[i].Name;
        //                switch (strColName)
        //                {

        //                    //case "Aufraggeber":
        //                    //case "Gut":

        //                    //    //dgv.Columns[i].Width = 25;
        //                    //    break;
        //                    case "Netto":
        //                    case "Brutto":
        //dgv.Columns[i].MinWidth = 25;
        //                break;
        //                    //case "Werksnummer":
        //                    //    //dgv.Columns[i].Width = 17;
        //                    //    dgv.Columns[i].Width = 20;
        //                    //    break;
        //                        case "Aufraggeber":
        //                        case "Gut":

        //                        dgv.Columns[i].MinWidth = 30;
        //                        break;
        //                    //case "Reihe":
        //                    //case "Platz":
        //                    //    //dgv.Columns[i].Width = 9;
        //                    //    break;
        //                    //case "Dicke":
        //                    //case "Breite":
        //                    //case "Laenge":
        //                    //case "Anzahl":
        //                    //    //dgv.Columns[i].Width = 9;
        //                    //    break;
        //                    //case "Werksnummer":
        //                    //    //dgv.Columns[i].Width = 2;
        //                    //    break;
        //                    ////case "LVSNr":

        //                    ////    dgv.Columns[i].Width = 30;
        //                    ////    break;

        //                    //////case "Reihe":
        //                    //////case "neueReihe":
        //                    ////case "Aufraggeber":
        //                    ////case "Breite":
        //                    ////case "Länge":
        //                    ////case "Laenge":
        //                    ////    dgv.Columns[i].Width = 30;
        //                    ////    break;

        //                    //////case "Höhe":
        //                    //////case "Hoehe":
        //                    //////case "iO":
        //                    //////    dgv.Columns[i].Width = 20;
        //                    //////    break;

        //                    ////case "Dicke":
        //                    ////    dgv.Columns[i].Width = 25;
        //                    ////    break;

        //                    ////case "Gut":
        //                    ////    dgv.Columns[i].Width = 40;
        //                    ////    break;

        //                    ////case "Brutto":
        //                    ////    dgv.Columns[i].Width = 35;
        //                    ////    break;

        //                    ////case "Netto":
        //                    ////    dgv.Columns[i].Width = 35;
        //                    ////    break;

        //                    ////case "Eingangsdatum":
        //                    ////    dgv.Columns[i].Width = 30;
        //                    ////    //dgv.Columns[i].Name = "E-Datum";
        //                    ////    break;

        //                    ////case "Ausgangsdatum":
        //                    ////    dgv.Columns[i].Width = 30;
        //                    ////    //dgv.Columns[i].Name = "A-Datum";
        //                    ////    break;

        //                    ////case "Werksnummer":
        //                    ////    dgv.Columns[i].Width = 40;
        //                    ////    //dgv.Columns[i].Name = "Werks-Nr";
        //                    ////    break;
        //                    ////case "Produktionsnummer":
        //                    ////    dgv.Columns[i].Width = 37;
        //                    ////    //dgv.Columns[i].Name = "Prod-Nr";
        //                    ////    break;

        //                    default:
        //                        //dgv.Columns[i].Width = 11;
        //                        dgv.Columns[i].Width = 12;
        //                        break;
        //                }
        //            }
        //        }
    }

}
