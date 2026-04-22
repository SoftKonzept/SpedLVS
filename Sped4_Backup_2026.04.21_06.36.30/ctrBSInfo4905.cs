using LVS;
using Sped4.Classes.TelerikCls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;

namespace Sped4
{
    public partial class ctrBSInfo4905 : UserControl
    {
        public Globals._GL_USER GL_User;
        const string const_FileName = "_BSInfo4984";
        const string const_Headline = "Bestandsinformationen [VDA4984]";
        internal List<string> ListAttachmentPath;
        internal string AttachmentPath;
        internal string FileName;
        internal clsASN ASN;
        internal ctrMenu _ctrMenu;
        List<decimal> ListAdrToSelect = new List<decimal>();
        public frmTmp _frmTmp;
        public delegate void ThreadCtrInvokeEventHandler();
        internal DataTable dtSource;

        internal LVS.ASN.clsVDA4984 vda4984;

        ///<summary>ctrBSInfo4905 / ctrBSInfo4905</summary>
        ///<remarks></remarks>
        public ctrBSInfo4905()
        {
            InitializeComponent();
            this.afColorLabel1.myText = ctrBSInfo4905.const_Headline;
        }
        ///<summary>ctrBSInfo4905 / ctrBSInfo4905_Load</summary>
        ///<remarks></remarks>
        private void ctrBSInfo4905_Load(object sender, EventArgs e)
        {

            dtSource = new DataTable();
            AttachmentPath = this._ctrMenu._frmMain.system.WorkingPathExport;
            //Init Class
            //this.ASN = new clsASN();
            //this.ASN.InitClass(this._ctrMenu._frmMain.GL_System, this.GL_User);
            //this.ASN.Sys = this._ctrMenu._frmMain.system;
            //this.ASN.EventWorkingReport += ASN_EventWorkingReport;

            this.vda4984 = new LVS.ASN.clsVDA4984();
            this.vda4984.InitClass(this._ctrMenu._frmMain.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system);

            this.dgv.EnableFiltering = Functions.CheckUserForAdminComtec(this.GL_User);
            this.dgv.ShowFilteringRow = this.dgv.EnableFiltering;
            this.dgv.EnableCustomFiltering = this.dgv.EnableFiltering;

            this.tsbtnCtrInWindow.Visible = Functions.CheckUserForAdminComtec(this.GL_User);
            this.tsbtnIntegrateCtr.Visible = this.tsbtnCtrInWindow.Visible;

            this.dgv.MasterTemplate.EnableSorting = true;
            CustomerSettings();
        }
        ///<summary>ctrASNCall / BSWorker_DoWork</summary>
        ///<remarks></remarks>
        private void BSWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            SetTsBar4905ProcessValue(0);
            InitDGV();
        }
        ///<summary>ctrASNCall / CustomerSettings</summary>
        ///<remarks>Hier kann den Kundenwünschen entsprechend ein / mehere Elemente 
        ///         ein-/ausgeblendet werden</remarks>
        private void CustomerSettings()
        {
            this._ctrMenu._frmMain.system.Client.ctrBSInfo4905_CustomizeCheckBoxChecked(ref this.cbChecked);
            this._ctrMenu._frmMain.system.Client.ctrBSInfo4905_CustomizeCheckBoxRueckstand(ref this.cbRuckstand);
            this.dgv.EnableCustomFiltering = false;
            //this.dgv.ShowFilteringRow = Functions.CheckUserForAdminComtec(this.GL_User);

            this.dgv.EnableFiltering = true;
            this.dgv.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgv.MasterTemplate.ShowFilteringRow = false;
        }
        ///<summary>ctrBSInfo4905 / ASN_EventWorkingReport</summary>
        ///<remarks></remarks>
        private void ASN_EventWorkingReport(object sender, EventArgs e)
        {
            SetInfoText(this.ASN.WorkingReportText);
        }
        ///<summary>ctrBSInfo4905 / tsbtnSearch_Click</summary>
        ///<remarks></remarks>
        private void tsbtnSearch_Click(object sender, EventArgs e)
        {
            if (!this.BSWorker.IsBusy)
            {
                this.BSWorker.RunWorkerAsync();
            }
        }
        ///<summary>ctrBSInfo4905 / SetTsBar4905ProcessStartValue</summary>
        ///<remarks></remarks>
        public void SetTsBar4905ProcessValue(Int32 iVal)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            SetTsBar4905ProcessValue(iVal);
                                                                        }
                                                                    )
                                    );
                return;
            }
            this.tsbar4905.Value = iVal;
        }
        ///<summary>ctrBSInfo4905 / SetTsBar4905ProcessMaxValue</summary>
        ///<remarks></remarks>
        public void SetTsBar4905ProcessMaxValue(Int32 iMax)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            SetTsBar4905ProcessMaxValue(iMax);
                                                                        }
                                                                    )
                                    );
                return;
            }
            this.tsbar4905.Maximum = iMax;
        }
        ///<summary>ctrBSInfo4905 / SetTsBar4905ProcessStep</summary>
        ///<remarks></remarks>
        public void SetTsBar4905ProcessStep()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            SetTsBar4905ProcessStep();
                                                                        }
                                                                    )
                                    );
                return;
            }
            this.tsbar4905.Value++;
        }
        ///<summary>ctrBSInfo4905 / SetInfoText</summary>
        ///<remarks></remarks>
        public void SetInfoText(string strTxt)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            SetInfoText(strTxt);
                                                                        }
                                                                    )
                                    );
                return;
            }
            this.tslVDA4905Info.Text = strTxt;
        }
        ///<summary>ctrBSInfo4905 / InitDGV</summary>
        ///<remarks></remarks>
        public void InitDGV()
        {
            this.SetTsBar4905ProcessMaxValue(5);
            //---Counter 1
            SetTsBar4905ProcessValue(1);
            SetInfoText("Verarbeitung beginnt...");

            //---Counter 2
            SetTsBar4905ProcessStep();
            //this.ASN.InitBSInfoVDA4905(cbChecked.Checked, cbInclSPL.Checked, this.cbActivGut.Checked);
            this.vda4984.LoadBSInfo4984(cbChecked.Checked, cbInclSPL.Checked, this.cbActivGut.Checked);

            //---Counter 3
            SetTsBar4905ProcessStep();
            SetInfoText("Liefereinteilungen für die Güterarten werden ermittelt...");
            //-- detaillierte Lieferantenaufstellung
            if (this._ctrMenu._frmMain.system.Client.Modul.ASN_VDA4905LiefereinteilungenAktiv_SupplierDetails)
            {
                //---Counter 1
                SetTsBar4905ProcessValue(1);
                //---Counter 2
                SetTsBar4905ProcessStep();
                SetInfoText("Liefereinteilungen für die Güterarten/SL werden ermittelt...");
                //---Counter 3
                SetTsBar4905ProcessStep();
                //this.ASN.VDA4905.InitSubBSInfoSL(this.ASN.VDA4905.dtBSInfoSource.DefaultView.ToTable(), cbRuckstand.Checked, this.cbChecked.Checked, this.cbInclSPL.Checked);
                this.vda4984.InitSubBSInfoSL(this.vda4984.dtBSInfoSource.DefaultView.ToTable(), cbRuckstand.Checked, this.cbChecked.Checked, this.cbInclSPL.Checked);
                dtSource = this.vda4984.dtSL4984LE.DefaultView.ToTable();
                //dtSource = this.vda4984.dtBSInfoSource.DefaultView.ToTable();
            }
            else
            {
                //dtSource = this.ASN.VDA4905.dtBSInfoSource.DefaultView.ToTable();
                dtSource = this.vda4984.dtBSInfoSource.DefaultView.ToTable();
            }
            //---Counter 4
            SetTsBar4905ProcessStep();
            SetInfoText("Liefereinteilungen für die Güterarten ermittelt...");
            SetTsBar4905ProcessValue(this.tsbar4905.Maximum);

            /****************************************************************************************************************
            *                                        alt VDA 4905
            ******************************************************************************************************************/
            //if (this._ctrMenu._frmMain.system.Mandant.VDA4905Verweis.Equals(string.Empty))
            //{
            //    string strError = "Es wurde kein VDA4905 Verweis für diesen Mandanten hinterlegt. Der Vorgang kann somit nicht durchgeführt werden.";
            //    clsMessages.Allgemein_ERRORTextShow(strError);
            //}
            //else
            //{
            //    //---Counter 2
            //    SetTsBar4905ProcessStep();
            //    this.ASN.InitBSInfoVDA4905(cbChecked.Checked, cbInclSPL.Checked, this.cbActivGut.Checked);
            //    //---Counter 3
            //    SetTsBar4905ProcessStep();
            //    SetInfoText("Liefereinteilungen für die Güterarten werden ermittelt...");

            //    //-- detaillierte Lieferantenaufstellung
            //    if (this._ctrMenu._frmMain.system.Client.Modul.ASN_VDA4905LiefereinteilungenAktiv_SupplierDetails)
            //    {
            //        //---Counter 1
            //        SetTsBar4905ProcessValue(1);
            //        //---Counter 2
            //        SetTsBar4905ProcessStep();
            //        SetInfoText("Liefereinteilungen für die Güterarten/SL werden ermittelt...");
            //        //---Counter 3
            //        SetTsBar4905ProcessStep();
            //        this.ASN.VDA4905.InitSubBSInfoSL(this.ASN.VDA4905.dtBSInfoSource.DefaultView.ToTable(), cbRuckstand.Checked, this.cbChecked.Checked, this.cbInclSPL.Checked);
            //        dtSource = this.ASN.VDA4905.dtSL4905LE;
            //    }
            //    else
            //    {
            //        dtSource = this.ASN.VDA4905.dtBSInfoSource.DefaultView.ToTable();
            //    }
            //    //---Counter 4
            //    SetTsBar4905ProcessStep();
            //    SetInfoText("Liefereinteilungen für die Güterarten ermittelt...");
            //    SetTsBar4905ProcessValue(this.tsbar4905.Maximum);
            //}
            //}
            SetDGVDataSource();
            SetTsBar4905ProcessValue(0);
            SetInfoText("Prozess abgeschlossen....");
        }
        ///<summary>ctrBSInfo4905 / SetDGVDataSource</summary>
        ///<remarks></remarks>
        private void SetDGVDataSource()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            SetDGVDataSource();
                                                                        }
                                                                    )
                                    );
                return;
            }
            dgv.DataSource = dtSource;
            foreach (GridViewColumn col in this.dgv.Columns)
            {
                GridViewDataColumn tmpDataCol;
                switch (col.Name)
                {
                    case "Nr":
                        col.IsVisible = Functions.CheckUserForAdminComtec(this.GL_User);
                        break;

                    case "MB":
                        col.ImageLayout = ImageLayout.Center;
                        col.AutoSizeMode = BestFitColumnMode.DisplayedCells;
                        break;
                    case "BS Brutto":
                    case "min.BS":
                    case "Diff":
                        tmpDataCol = col as GridViewDataColumn;
                        tmpDataCol.FormatString = "{0:n0}";
                        break;

                    case "Faktor":
                        tmpDataCol = col as GridViewDataColumn;
                        tmpDataCol.FormatString = "{0:n2}";
                        break;

                    case "4905":
                    case "4984":
                    case "Prüfpunkt":
                        //GridViewDataColumn tmpDataCol = colSub as GridViewDataColumn;
                        //tmpDataCol.FormatString = "{0:d}";
                        tmpDataCol = col as GridViewDataColumn;
                        tmpDataCol.FormatString = "{0:d}";
                        break;

                    case "FZ dazu":
                    case "PP FZ dazu":
                    case "FZ Diff":
                    case "Bestand":
                    case "Ausgang":
                    case "B+A":
                    case "PP zu IST":
                        //GridViewDataColumn tmpDataColInt = colSub as GridViewDataColumn;
                        //tmpDataColFaktor.FormatString = "{0:n2}";
                        tmpDataCol = col as GridViewDataColumn;
                        tmpDataCol.FormatString = "{0:n0}";
                        break;
                    case "Log":
                        col.ImageLayout = ImageLayout.Center;
                        col.AutoSizeMode = BestFitColumnMode.DisplayedCells;
                        col.IsVisible = Functions.CheckUserForAdminComtec(this.GL_User);
                        break;

                    case "GArt":
                    case "TableRowNo":
                        col.IsVisible = Functions.CheckUserForAdminComtec(this.GL_User);
                        break;

                    default:
                        col.ImageLayout = ImageLayout.None;
                        col.AutoSizeMode = BestFitColumnMode.DisplayedCells;
                        break;
                }
            }
            this.dgv.BestFitColumns();
        }

        ///<summary>ctrBSInfo4905 / dgv_CellFormatting</summary>
        ///<remarks></remarks>
        private void dgv_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            //e.CellElement.RowInfo.Cells["Status"].Value.ToString();
            if (this.dgv.Rows.Count > 0)
            {
                if (e.RowIndex > -1)
                {
                    string ColName = e.Column.Name.ToString();
                    if (e.CellElement.Value != null)
                    {
                        decimal decFaktor = 0;
                        switch (ColName)
                        {
                            case "MB":
                                //Status
                                decFaktor = 0;
                                string strStatus = e.CellElement.RowInfo.Cells["Faktor"].Value.ToString();
                                decimal.TryParse(strStatus, out decFaktor);
                                switch (this._ctrMenu._frmMain.system.Client.MatchCode)
                                {
                                    case clsClient.const_ClientMatchcode_SIL + "_":
                                        e.CellElement.Image = Functions.GetImageForBSInfo4905SIL(decFaktor);
                                        break;
                                    case clsClient.const_ClientMatchcode_SZG + "_":
                                        e.CellElement.Image = Functions.GetImageForBSInfo4905SZG(decFaktor);
                                        break;
                                }
                                e.CellElement.ForeColor = System.Drawing.Color.Transparent;
                                break;
                            case "MB SL":
                                //Status
                                decFaktor = 0;
                                string strStatusSL = e.CellElement.RowInfo.Cells["Faktor SL"].Value.ToString();
                                decimal.TryParse(strStatusSL, out decFaktor);
                                switch (this._ctrMenu._frmMain.system.Client.MatchCode)
                                {
                                    case clsClient.const_ClientMatchcode_SIL + "_":
                                        e.CellElement.Image = Functions.GetImageForBSInfo4905SIL(decFaktor);
                                        break;
                                    case clsClient.const_ClientMatchcode_SZG + "_":
                                        e.CellElement.Image = Functions.GetImageForBSInfo4905SZG(decFaktor);
                                        break;
                                }
                                e.CellElement.ForeColor = System.Drawing.Color.Transparent;
                                break;

                            case "4905":

                                break;
                            default:
                                e.CellElement.Image = null;
                                e.CellElement.ForeColor = System.Drawing.Color.Black;
                                break;
                        }
                    }
                }
            }
        }
        ///<summary>ctrBSInfo4905 / tsbtnClose_Click</summary>
        ///<remarks></remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            CloseCtr();
        }
        ///<summary>ctrBSInfo4905 / CloseCtr</summary>
        ///<remarks></remarks>
        private void CloseCtr()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            CloseCtr();
                                                                        }
                                                                    )
                                    );
                return;
            }
            this._ctrMenu.CloseCtrBSInfo4905();
        }
        ///<summary>ctrBSInfo4905 / tsbtnPrint_Click</summary>
        ///<remarks></remarks>
        private void tsbtnPrint_Click(object sender, EventArgs e)
        {
            //Print();
        }
        ///<summary>ctrBSInfo4905 / tsbtnExcel_Click</summary>
        ///<remarks></remarks>
        private void Print()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            Print();
                                                                        }
                                                                    )
                                    );
                return;
            }
        }
        ///<summary>ctrBSInfo4905 / tsbtnExcel_Click</summary>
        ///<remarks></remarks>
        private void tsbtnExcel_Click(object sender, EventArgs e)
        {
            if (this.dgv.Rows.Count > 0)
            {
                bool openExportFile = false;
                FileName = DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + const_FileName + ".xls";
                saveFileDialog.InitialDirectory = AttachmentPath;
                saveFileDialog.FileName = AttachmentPath + "\\" + FileName;
                saveFileDialog.ShowDialog();
                FileName = saveFileDialog.FileName;

                if (saveFileDialog.FileName.Equals(String.Empty))
                {
                    return;
                }
                string strFileName = this.saveFileDialog.FileName;

                Sped4.Classes.TelerikCls.clsExcelML exPort = new clsExcelML();
                exPort.InitClass(ref this._ctrMenu._frmMain, this.GL_User);

                //Headertext erstellt
                string txt = string.Empty;
                txt = "Bestandsinformation vom " + DateTime.Now.ToString("dd.MM.yyyy");
                exPort.ListHeaderText.Add(txt);

                switch (this._ctrMenu._frmMain.system.Client.MatchCode)
                {
                    case clsClient.const_ClientMatchcode_SIL + "_":
                        //Baustelle für SIL
                        exPort.iExportFormat = 1;

                        this.dgv.MasterTemplate.EnableSorting = true;

                        //Spalte NR ausblenden
                        if (this.dgv.Columns.Contains("Nr"))
                        {
                            this.dgv.Columns["Nr"].IsVisible = false;
                        }

                        SortDescriptor sortLieferant = new SortDescriptor();
                        sortLieferant.PropertyName = "Text";
                        sortLieferant.Direction = System.ComponentModel.ListSortDirection.Ascending;
                        this.dgv.MasterTemplate.SortDescriptors.Add(sortLieferant);

                        SortDescriptor sortFaktor = new SortDescriptor();
                        sortFaktor.PropertyName = "Faktor";
                        sortFaktor.Direction = System.ComponentModel.ListSortDirection.Ascending;
                        this.dgv.MasterTemplate.SortDescriptors.Add(sortFaktor);

                        exPort.Telerik_RunExportToExcelMLBSInfo4905_SIL(this.dgv, strFileName, ref openExportFile, true, "Mindesbestände");
                        if (this.dgv.Columns.Contains("Zugang"))
                        {
                            this.dgv.Columns.Remove("Zugang");
                        }
                        //Spalte NR einblenden
                        if (this.dgv.Columns.Contains("Nr"))
                        {
                            this.dgv.Columns["Nr"].IsVisible = true;
                        }
                        break;

                    case clsClient.const_ClientMatchcode_SZG + "_":

                        this.dgv.MasterTemplate.Columns["GArt"].IsVisible = false;
                        this.dgv.MasterTemplate.Columns["TableRowNo"].IsVisible = false;
                        exPort.Telerik_RunExportToExcelMLVDA4905_SZG(this.dgv, strFileName, ref openExportFile, true, "Liefereinteilungen");
                        if (this.dgv.Columns.Contains("Zugang"))
                        {
                            this.dgv.Columns.Remove("Zugang");
                        }
                        this.dgv.MasterTemplate.Columns["GArt"].IsVisible = Functions.CheckUserForAdminComtec(this.GL_User);
                        this.dgv.MasterTemplate.Columns["TableRowNo"].IsVisible = Functions.CheckUserForAdminComtec(this.GL_User);
                        break;

                    default:
                        exPort.Telerik_RunExportToExcelML(ref this.dgv, strFileName, ref openExportFile, true, "Mindesbestände");
                        //exPort.Telerik_RunExportToExcelML(this.dgv, strFileName, ref openExportFile, true, "Mindesbestände");
                        break;
                }
                //
                if (openExportFile)
                {
                    try
                    {
                        System.Diagnostics.Process.Start(strFileName);
                    }
                    catch (Exception ex)
                    {
                        clsError error = new clsError();
                        error._GL_User = this.GL_User;
                        error.Code = clsError.code1_501;
                        error.Aktion = "Mindesbestände / VDA4905 - Excelexport öffnen";
                        error.exceptText = ex.ToString();
                    }
                }
            }
        }
        ///<summary>ctrBSInfo4905 / dgv_ContextMenuOpening</summary>
        ///<remarks></remarks>
        private void dgv_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            //-- detaillierte Lieferantenaufstellung
            if (this._ctrMenu._frmMain.system.Client.Modul.ASN_VDA4905LiefereinteilungenAktiv_SupplierDetails)
            {
                //if (this.dgv.Columns.Contains("ASNID"))
                if (this.dgv.Columns.Contains("LE#"))
                {
                    RadMenuSeparatorItem separator;
                    RadMenuItem customMenuItem;
                    //if (this.dgv.SelectedRows.Count > 0)
                    //{
                    //    separator = new RadMenuSeparatorItem();
                    //    e.ContextMenu.Items.Add(separator);
                    //    customMenuItem = new RadMenuItem();
                    //    customMenuItem.Text = "VDA4984 anzeigen";
                    //    customMenuItem.Click += new EventHandler(ShowSelectedVDA4905);
                    //    e.ContextMenu.Items.Add(customMenuItem);
                    //}
                    if (this.dgv.SelectedRows.Count > 0)
                    {
                        separator = new RadMenuSeparatorItem();
                        e.ContextMenu.Items.Add(separator);
                        customMenuItem = new RadMenuItem();
                        customMenuItem.Text = "VDA4984 anzeigen";
                        customMenuItem.Click += new EventHandler(ShowSelectedVDA4984);
                        e.ContextMenu.Items.Add(customMenuItem);
                    }

                }
            }
        }
        /////<summary>ctrBSInfo4905 / ShowSelectedVDA4905</summary>
        /////<remarks></remarks>
        //private void ShowSelectedVDA4905(object sender, EventArgs e)
        //{
        //    //-- detaillierte Lieferantenaufstellung
        //    if (this._ctrMenu._frmMain.system.Client.Modul.ASN_VDA4905LiefereinteilungenAktiv_SupplierDetails)
        //    {
        //        if (this.dgv.Columns.Contains("TableRowNo"))
        //        {
        //            Int32 iTmp = 0;
        //            Int32.TryParse(this.dgv.SelectedRows[0].Cells["TableRowNo"].Value.ToString(), out iTmp);
        //            if (iTmp > 0)
        //            {
        //                DataTable dtTmp = ((DataTable)this.dgv.DataSource).Copy();

        //                //check auf ASNID
        //                decimal decTmp = 0;
        //                decimal.TryParse(this.dgv.SelectedRows[0].Cells["AsnID"].Value.ToString(), out decTmp);
        //                if (decTmp > 0)
        //                {
        //                    dtTmp.DefaultView.RowFilter = "TableRowNo=" + iTmp;
        //                    DataRow RowDetails = (dtTmp.DefaultView.ToTable()).Rows[0];
        //                    this._ctrMenu.OpenCtrVDA4905Details(RowDetails);
        //                    dtTmp.DefaultView.RowFilter = string.Empty;
        //                }
        //                else
        //                {
        //                    string strTxt = "Hier liegt keine VDA4905 vor!";
        //                    clsMessages.Allgemein_ERRORTextShow(strTxt);
        //                }
        //            }
        //            else
        //            {
        //                string strTxt = "Hier liegt keine VDA4905 vor!";
        //                clsMessages.Allgemein_ERRORTextShow(strTxt);
        //            }
        //        }
        //        else
        //        {
        //            string strTxt = "Hier liegt keine VDA4905 vor!";
        //            clsMessages.Allgemein_ERRORTextShow(strTxt);
        //        }
        //    }
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowSelectedVDA4984(object sender, EventArgs e)
        {
            //-- detaillierte Lieferantenaufstellung
            if (this._ctrMenu._frmMain.system.Client.Modul.ASN_VDA4905LiefereinteilungenAktiv_SupplierDetails)
            {
                if (this.dgv.Columns.Contains("TableRowNo"))
                {
                    Int32 iTmp = 0;
                    Int32.TryParse(this.dgv.SelectedRows[0].Cells["TableRowNo"].Value.ToString(), out iTmp);
                    if (iTmp > 0)
                    {
                        DataTable dtTmp = ((DataTable)this.dgv.DataSource).Copy();

                        //check auf LE#
                        decimal decTmp = 0;
                        decimal.TryParse(this.dgv.SelectedRows[0].Cells["LE#"].Value.ToString(), out decTmp);
                        if (decTmp > 0)
                        {
                            dtTmp.DefaultView.RowFilter = "TableRowNo=" + iTmp;
                            DataRow RowDetails = (dtTmp.DefaultView.ToTable()).Rows[0];
                            this._ctrMenu.OpenCtrVDA4984Details(RowDetails);
                            dtTmp.DefaultView.RowFilter = string.Empty;
                        }
                        else
                        {
                            string strTxt = "Hier liegt keine VDA4984 vor!";
                            clsMessages.Allgemein_ERRORTextShow(strTxt);
                        }
                    }
                    else
                    {
                        string strTxt = "Hier liegt keine VDA4984 vor!";
                        clsMessages.Allgemein_ERRORTextShow(strTxt);
                    }
                }
                else
                {
                    string strTxt = "Hier liegt keine VDA4984 vor!";
                    clsMessages.Allgemein_ERRORTextShow(strTxt);
                }
            }
        }




        ///<summary>ctrBSInfo4905 / tsbtnCtrInWindow_Click</summary>
        ///<remarks></remarks>
        private void tsbtnCtrInWindow_Click(object sender, EventArgs e)
        {
            this._ctrMenu.OpenCtrBSInfo4905InFrm(this);
            this._ctrMenu.CloseCtrBSInfo4905();
        }
        ///<summary>ctrBSInfo4905 / tsbtnIntegrateCtr_Click</summary>
        ///<remarks></remarks>
        private void tsbtnIntegrateCtr_Click(object sender, EventArgs e)
        {
            if (this._frmTmp != null)
            {
                this._frmTmp.CloseFrmTmp();
            }
            else
            {
                this._ctrMenu.CloseCtrBSInfo4905FrmTmp();
            }
            this._ctrMenu.OpenCtrBSInfo4905(this);
        }
        ///<summary>ctrBSInfo4905 / tsbtnReSizeGrid_Click</summary>
        ///<remarks></remarks>
        private void tsbtnReSizeGrid_Click(object sender, EventArgs e)
        {
            SetDGVDataSource();
        }
        ///<summary>ctrBSInfo4905 / cbOnlyVDA4905_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbOnlyVDA4905_CheckedChanged(object sender, EventArgs e)
        {
            Set4905Filter();
        }
        ///<summary>ctrBSInfo4905 / Set4905Filter</summary>
        ///<remarks></remarks>
        private void Set4905Filter()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            Set4905Filter();
                                                                        }
                                                                    )
                                    );
                return;
            }

            this.dgv.FilterDescriptors.Clear();
            if (cbOnlyVDA4984.Checked)
            {
                FilterDescriptor filter = new FilterDescriptor();
                filter.PropertyName = "ASNID";
                filter.Operator = FilterOperator.IsGreaterThan;
                filter.Value = 0;
                filter.IsFilterEditor = true;
                if (this.dgv.Columns.Contains("ASNID"))
                {
                    this.dgv.FilterDescriptors.Add(filter);
                }
            }
            if (this.dgv.Rows.Count > 0)
            {
                SetDGVDataSource();
            }
        }
    }
}
