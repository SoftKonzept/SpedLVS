using LVS;
using LVS.ViewData;
using Sped4.TelerikControls;
using System;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;

namespace Sped4.Classes
{
    public partial class ctrASNCall : UserControl
    {
        public Globals._GL_USER GL_User;
        //internal clsADR ADR;
        internal ctrMenu _ctrMenu;
        internal clsLager Lager = new clsLager();
        const string const_FileName = "_ASNList";
        private string AttachmentPath;
        internal clsASNCall Abruf;
        public string AktionModus = string.Empty;
        public const string const_Headline_Call = "Abrufe";
        public const string const_Headline_Rebooking = "Umbuchung";

        //internal clsASNCall CallEdit;
        DataTable dtSourceArtikelSelection = new DataTable();
        internal enum enumTabPage_ctrASNCall
        {
            pageView_Call,
            pageView_CallError
        }

        /****************************************************************************
        *                       Procedure / Methoden
        ****************************************************************************/
        ///<summary>ctrASNCall / ctrASNCall</summary>
        ///<remarks></remarks>
        public ctrASNCall()
        {
            InitializeComponent();
        }
        ///<summary>ctrASNCall / ctrASNCall_Load</summary>
        ///<remarks></remarks>
        private void ctrASNCall_Load(object sender, EventArgs e)
        {
            if (!AktionModus.Equals(string.Empty))
            {
                AttachmentPath = this._ctrMenu._frmMain.GL_System.sys_WorkingPathExport;
                InitCtr();
            }
            else
            {
                string strTxt = "Es ist kein Modus gesetzt. Bitte setzen Sie sich mit dem Support der Firma ComTEC Nöker GmbH in Verbindung.";
                clsMessages.Allgemein_ERRORTextShow(strTxt);
            }
            CustomerSettings();
        }
        ///<summary>ctrASNCall / CustomerSettings</summary>
        ///<remarks>Hier kann den Kundenwünschen entsprechend ein / mehere Elemente 
        ///         ein-/ausgeblendet werden</remarks>
        private void CustomerSettings()
        {
            //DeleteButton ausblenden
            this._ctrMenu._frmMain.system.Client.ctrASNCall_CustomizeTsbtnDeleteASN(ref this.tsbtnDeleteASN);
            //this.mmpManuelSearch.ExpandedCallapsed = Sped4.Controls.AFMinMaxPanel.EStatus.Collapsed;
            //this.mmpManuelSearch.SetExpandCollapse(Sped4.Controls.AFMinMaxPanel.EStatus.Collapsed);
        }
        ///<summary>ctrASNCall / InitCtr</summary>
        ///<remarks></remarks>
        public void InitCtr()
        {
            Abruf = new clsASNCall();
            Abruf.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system);

            //Headline
            string strHeadline = string.Empty;

            switch (this.AktionModus)
            {
                case clsASNCall.const_AbrufAktion_Abruf:
                    this.afColorLabel1.myText = const_Headline_Call;
                    this.pageView_Call.Text = const_Headline_Call;
                    break;

                case clsASNCall.const_AbrufAktion_UB:
                    this.afColorLabel1.myText = const_Headline_Rebooking;
                    this.pageView_Call.Text = const_Headline_Rebooking;
                    this.pageView_CallError.Enabled = false;
                    this.pageView_CallError.Text = string.Empty;
                    this.tsbtnDeaktivate.Visible = false;
                    this.tsbtnCreateAusgang.Text = "ausgewählte Umbuchungen durchführen";
                    break;
            }
            InitDGV();
            this.tabPage_Call.SelectedPage = this.pageView_Call;
        }
        ///<summary>ctrASNCall / InitDGV</summary>
        ///<remarks></remarks>
        private void InitDGV()
        {
            try
            {
                Abruf.GetCallOrRebookingList(AktionModus);
                Abruf.dtAbrufUBList.DefaultView.Sort = "EintreffDatum, EintreffZeit";
                dgv.DataSource = Abruf.dtAbrufUBList.DefaultView;

                Functions.setView(ref Abruf.dtAbrufUBList, ref dgv, clsClient.const_ViewKategorie_Abruf, clsClient.const_ViewName_Abruf, this._ctrMenu._frmMain.GL_System, false);

                SortDescriptor sortED = new SortDescriptor();
                sortED.PropertyName = "EintreffDatum";
                sortED.Direction = System.ComponentModel.ListSortDirection.Ascending;

                SortDescriptor sortEZ = new SortDescriptor();
                sortEZ.PropertyName = "EintreffZeit";
                sortEZ.Direction = System.ComponentModel.ListSortDirection.Ascending;

                dgv.MasterTemplate.SortDescriptors.Clear();
                dgv.MasterTemplate.SortDescriptors.Add(sortED);
                dgv.MasterTemplate.SortDescriptors.Add(sortEZ);

                if (this.dgv.Rows.Count > 0)
                {

                    for (Int32 i = 0; i <= this.dgv.Columns.Count - 1; i++)
                    {
                        string ColName = dgv.Columns[i].Name.ToString();
                        this.dgv.Columns[i].ReadOnly = true;
                        switch (ColName)
                        {
                            case "Select":
                                this.dgv.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                                break;
                            case "Eintreffdatum":
                                this.dgv.Columns[i].FormatString = "{0:d}";
                                this.dgv.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                                break;
                            case "Eintreffzeit":
                                this.dgv.Columns[i].FormatString = "{0:t}";
                                this.dgv.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                                break;

                            case "Dicke":
                            case "Brutto":
                            case "Breite":
                                this.dgv.Columns[i].FormatString = "{0:N2}";
                                this.dgv.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
                                break;
                            case "erstellt":
                                this.dgv.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                                break;

                            default:
                                this.dgv.Columns[i].IsVisible = true;
                                break;
                        }
                    }
                    this.dgv.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        ///<summary>ctrASNCall / tsbtnClose_Click</summary>
        ///<remarks></remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this._ctrMenu.CloseCtrASNCall();
        }
        ///<summary>ctrASNCall / dgv_CellClick</summary>
        ///<remarks></remarks>
        private void dgv_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                string strTmpArtikelID = this.dgv.Rows[e.RowIndex].Cells["ArtikelID"].Value.ToString();
                int iTmp = 0;
                int.TryParse(strTmpArtikelID, out iTmp);
                if (iTmp > 0)
                {
                    ArticleViewData artVM = new ArticleViewData(iTmp, (int)_ctrMenu.GL_User.User_ID, false);
                    if (artVM.Artikel.bSPL)
                    {
                        string strMes = "Der Abruf / Artikel befindet sich im Speerlager und kann nicht ausgebucht werden!" + Environment.NewLine;
                        clsMessages.Allgemein_ERRORTextShow(strMes);
                    }
                    else
                    {
                        bool CellValue = (bool)this.dgv.Rows[e.RowIndex].Cells["Select"].Value;
                        if (CellValue == true)
                        {
                            this.dgv.Rows[e.RowIndex].Cells["Select"].Value = false;
                        }
                        else
                        {
                            this.dgv.Rows[e.RowIndex].Cells["Select"].Value = true;
                        }

                        int iTmp1 = 0;
                        decimal decTmp2 = 0;
                        if (this.dgv.Rows.Count > 0)
                        {
                            foreach (GridViewRowInfo rowInfo in this.dgv.Rows)
                            {
                                if ((bool)rowInfo.Cells["Select"].Value)
                                {
                                    decTmp2 += (decimal)rowInfo.Cells["Brutto"].Value;
                                    iTmp1++;
                                }
                            }
                        }
                        tbSelCount.Text = iTmp1.ToString();
                        tbSelGross.Text = Functions.FormatDecimal(decTmp2);
                    }
                }
            }


            //int iTmp1 = 0;
            //decimal decTmp2 = 0;
            //if (this.dgv.Rows.Count > 0)
            //{
            //    foreach (GridViewRowInfo rowInfo in this.dgv.Rows)
            //    {
            //        if ((bool)rowInfo.Cells["Select"].Value)
            //        {
            //            decTmp2 += (decimal)rowInfo.Cells["Brutto"].Value;
            //            iTmp1++;
            //        }
            //    }
            //}
            //tbSelCount.Text = iTmp1.ToString();
            //tbSelGross.Text = Functions.FormatDecimal(decTmp2);
        }
        ///<summary>ctrASNCall / dgv_CellClick</summary>
        ///<remarks></remarks>
        private void SetAllASNSelectOrUnselect(bool bSelect)
        {
            for (Int32 i = 0; i <= dgv.Rows.Count - 1; i++)
            {
                this.dgv.Rows[i].Cells["Select"].Value = bSelect;
            }
        }
        ///<summary>ctrASNCall / tsbtnAllCheck_Click</summary>
        ///<remarks></remarks>
        private void tsbtnAllCheck_Click(object sender, EventArgs e)
        {
            SetAllASNSelectOrUnselect(true);
        }
        ///<summary>ctrASNCall / tsbtnAllUncheck_Click</summary>
        ///<remarks></remarks>
        private void tsbtnAllUncheck_Click(object sender, EventArgs e)
        {
            SetAllASNSelectOrUnselect(false);
        }
        ///<summary>ctrASNCall / tsbtnExcel_Click</summary>
        ///<remarks></remarks>
        private void tsbtnExcel_Click(object sender, EventArgs e)
        {
            bool openExportFile = false;
            LVS.clsExcel Excel = new clsExcel();
            string FileName = DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + const_FileName;
            //DataTable dtTmp = ASN.dtASNForEingang.DefaultView.ToTable(true, "ASN", "ASN-Datum", "AuftraggeberView", "EmpfaengerView", "VS-Datum", "LfsNr");
            string FilePath = Excel.ExportDataTableToWorksheet(Abruf.dtAbrufUBList, AttachmentPath + "\\" + FileName);
            openExportFile = true;

            if (openExportFile)
            {
                try
                {
                    System.Diagnostics.Process.Start(FilePath);
                }
                catch (Exception ex)
                {
                    clsError error = new clsError();
                    error._GL_User = this.GL_User;
                    error.Code = clsError.code1_501;
                    error.Aktion = "Abruf / UB List - Excelexport öffnen";
                    error.exceptText = ex.ToString();
                    error.WriteError();
                }
            }
        }
        ///<summary>ctrASNCall / tsbtnRefresh_Click</summary>
        ///<remarks></remarks>
        private void tsbtnDeleteASN_Click(object sender, EventArgs e)
        {
            if (clsMessages.DeleteAllgemein())
            {
                this.Abruf.dtAbrufUBList.DefaultView.RowFilter = "Select=True";
                DataTable dtTmpASNToDelete = this.Abruf.dtAbrufUBList.DefaultView.ToTable();
                if (dtTmpASNToDelete.Rows.Count > 0)
                {
                    this.Abruf.DisableCallOrRebooking(dtTmpASNToDelete);
                    this.Abruf.dtAbrufUBList.DefaultView.RowFilter = string.Empty;
                    this.tstbProdNr.Text = string.Empty;
                    InitDGV();
                }
            }
        }
        ///<summary>ctrASNCall / tsbtnRefresh_Click</summary>
        ///<remarks></remarks>
        private void tsbtnRefresh_Click(object sender, EventArgs e)
        {
            InitCtr();
        }
        ///<summary>ctrASNCall / tsbtnCreateAusgang_Click</summary>
        ///<remarks></remarks>
        private void tsbtnCreateAusgang_Click(object sender, EventArgs e)
        {
            this.tbLog.Text = string.Empty;
            //Log
            string LogTxt = DateTime.Now.ToString() + " - Verarbeitung wird gestartet..." + Environment.NewLine + Environment.NewLine;
            this.tbLog.Text = this.tbLog.Text + LogTxt;

            //Prozessbar
            this.tspbCall.Minimum = 0;
            this.tspbCall.Value = 0;
            this.tspbCall.Step = 1;

            //Ermitteln der selektierten Abrufe
            this.Abruf.dtAbrufUBList.DefaultView.RowFilter = "Select=True";
            //Check Daten ausgewählt
            if (this.Abruf.dtAbrufUBList.DefaultView.Count > 0)
            {
                Int32 iAnzahl = this.Abruf.dtAbrufUBList.DefaultView.Count;
                //Log
                LogTxt = string.Empty;
                LogTxt = "Anzahl Datensätze: " + iAnzahl.ToString() + Environment.NewLine;
                this.tbLog.Text = this.tbLog.Text + LogTxt;

                //Prozessbar
                this.tspbCall.Maximum = iAnzahl;

                CallViewData callViewData = new CallViewData(_ctrMenu._frmMain.GL_System, _ctrMenu._frmMain.GL_User, _ctrMenu._frmMain.system);
                callViewData.InitAndSaveEingang(this.Abruf.dtAbrufUBList.DefaultView.ToTable(), AktionModus, true);
                foreach (string s in callViewData.ListLogText)
                {
                    this.tbLog.Text = this.tbLog.Text + s;
                }
                ////Daten nach Lieferanten sortieren
                //this.Abruf.dtAbrufUBList.DefaultView.Sort = "Lieferant";
                //DataTable dtSelArtikel = this.Abruf.dtAbrufUBList.DefaultView.ToTable();
                //if (dtSelArtikel.Rows.Count > 0)
                //{
                //    DataTable dtLieferant = dtSelArtikel.DefaultView.ToTable(true, "Auftraggeber");
                //    foreach (DataRow row in dtLieferant.Rows)
                //    {
                //        clsASNCall tmpCall = new clsASNCall();
                //        tmpCall.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system);
                //        decimal decAuftraggeberID = 0;
                //        Decimal.TryParse(row["Auftraggeber"].ToString(), out decAuftraggeberID);
                //        if (decAuftraggeberID > 0)
                //        {
                //            dtSelArtikel.DefaultView.RowFilter = "Auftraggeber=" + (Int32)decAuftraggeberID;
                //            Int32 iAbrufID = 0;
                //            Int32.TryParse((dtSelArtikel.DefaultView.ToTable()).Rows[0]["AbrufID"].ToString(), out iAbrufID);
                //            if (iAbrufID > 0)
                //            {
                //                tmpCall.ID = iAbrufID;
                //                tmpCall.Fill();

                //                switch (AktionModus)
                //                {
                //                    /************************************************************
                //                    *              Verarbeitung Call / Abrufe
                //                    * *********************************************************/
                //                    case clsASNCall.const_AbrufAktion_Abruf:
                //                        tmpCall.ProzessCall(dtSelArtikel.DefaultView.ToTable());
                //                        break;

                //                    /************************************************************
                //                    *              Verarbeitung Umbuchung / Rebooking
                //                    *************************************************************/
                //                    case clsASNCall.const_AbrufAktion_UB:
                //                        tmpCall.ProzessRebooking(dtSelArtikel.DefaultView.ToTable());
                //                        break;
                //                }
                //                //Prozess
                //                this.tspbCall.Value = this.tspbCall.Value + dtSelArtikel.DefaultView.ToTable().Rows.Count;

                //                //Log
                //                LogTxt = string.Empty;
                //                LogTxt = tmpCall.LogText;
                //                this.tbLog.Text = this.tbLog.Text + LogTxt;
                //                //Eintrag ins Logbuch
                //                Functions.AddLogbuch(this.GL_User.User_ID, Globals.enumLogbuchAktion.ASNAbruf.ToString(), LogTxt);


                //            }
                //            dtSelArtikel.DefaultView.RowFilter = string.Empty;
                //        }
                //    }


                //}
            }
            else
            {
                //Log
                LogTxt = string.Empty;
                LogTxt = "Es wurden keine Dateien zur Verarbeitung ausgewählt...";
                this.tbLog.Text = this.tbLog.Text + LogTxt;
            }
            LogTxt = string.Empty;
            LogTxt = Environment.NewLine + DateTime.Now.ToString() + " - Verarbeitung wird beendet...";
            this.tbLog.Text = this.tbLog.Text + LogTxt;
            this.Abruf.dtAbrufUBList.DefaultView.RowFilter = string.Empty;
            InitDGV();
            //Prozessbar
            this.tspbCall.Minimum = 0;
            this.tspbCall.Value = 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_SelectionChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        ///             Abrufe sollen deaktiviert werden können
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnDeaktivate_Click(object sender, EventArgs e)
        {
            this.tbLog.Text = string.Empty;
            //Log
            string LogTxt = DateTime.Now.ToString() + " - Abrufe werden deaktiviert..." + Environment.NewLine + Environment.NewLine;
            this.tbLog.Text = this.tbLog.Text + LogTxt;

            //Prozessbar
            this.tspbCall.Minimum = 0;
            this.tspbCall.Value = 0;
            this.tspbCall.Step = 1;

            //Ermitteln der selektierten Abrufe
            this.Abruf.dtAbrufUBList.DefaultView.RowFilter = "Select=True";
            //Check Daten ausgewählt
            if (this.Abruf.dtAbrufUBList.DefaultView.Count > 0)
            {
                Int32 iAnzahl = this.Abruf.dtAbrufUBList.DefaultView.Count;
                //Log
                LogTxt = string.Empty;
                LogTxt = "Anzahl Datensätze: " + iAnzahl.ToString() + Environment.NewLine;
                this.tbLog.Text = this.tbLog.Text + LogTxt;

                //Prozessbar
                this.tspbCall.Maximum = iAnzahl;

                DataTable dtSelArtikel = this.Abruf.dtAbrufUBList.DefaultView.ToTable();
                if (dtSelArtikel.Rows.Count > 0)
                {
                    foreach (DataRow row in dtSelArtikel.Rows)
                    {
                        clsASNCall tmpCall = new clsASNCall();
                        tmpCall.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system);
                        int iAbrufId = 0;
                        int.TryParse(row["AbrufID"].ToString(), out iAbrufId);
                        if (iAbrufId > 0)
                        {
                            tmpCall.ID = iAbrufId;
                            tmpCall.Fill();
                            tmpCall.DeactivateCall();

                            //Log
                            LogTxt = string.Empty;
                            LogTxt = tmpCall.LogText;
                            this.tbLog.Text = LogTxt;
                            //Eintrag ins Logbuch
                            Functions.AddLogbuch(this.GL_User.User_ID, enumLogbuchAktion.ASNAbruf.ToString(), LogTxt);

                            //Prozess
                            int iTmp = this.tspbCall.Value + dtSelArtikel.DefaultView.ToTable().Rows.Count;
                            if ((iTmp >= this.tspbCall.Minimum) && (iTmp <= tspbCall.Maximum))
                            {
                                this.tspbCall.Value = iTmp;
                            }

                        }
                    }
                }
            }
            else
            {
                //Log
                LogTxt = string.Empty;
                LogTxt = "Es wurden keine Dateien zur Deaktivierung ausgewählt...";
                this.tbLog.Text = this.tbLog.Text + LogTxt;
            }
            LogTxt = string.Empty;
            LogTxt = Environment.NewLine + DateTime.Now.ToString() + " - Verarbeitung wird beendet...";
            this.tbLog.Text = this.tbLog.Text + LogTxt;
            this.Abruf.dtAbrufUBList.DefaultView.RowFilter = string.Empty;
            InitDGV();
            //Prozessbar
            this.tspbCall.Minimum = 0;
            this.tspbCall.Value = 0;
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitDGVCallError()
        {
            this.mmpManuelSearch.ExpandedCallapsed = Sped4.Controls.AFMinMaxPanel.EStatus.Expanded;
            this.mmpManuelSearch.SetExpandCollapse(Sped4.Controls.AFMinMaxPanel.EStatus.Expanded);
            try
            {
                Abruf.GetCallOrRebookingErrorList(AktionModus);
                Abruf.dtAbrufUBListError.DefaultView.Sort = "EintreffDatum, EintreffZeit";
                dgvCallError.DataSource = Abruf.dtAbrufUBListError.DefaultView;

                //Functions.setView(ref Abruf.dtAbrufUBListError, ref dgvCallError, clsClient.const_ViewKategorie_Abruf, clsClient.const_ViewName_Abruf, this._ctrMenu._frmMain.GL_System, false);

                SortDescriptor sortED = new SortDescriptor();
                sortED.PropertyName = "EintreffDatum";
                sortED.Direction = System.ComponentModel.ListSortDirection.Ascending;

                SortDescriptor sortEZ = new SortDescriptor();
                sortEZ.PropertyName = "EintreffZeit";
                sortEZ.Direction = System.ComponentModel.ListSortDirection.Ascending;

                dgvCallError.MasterTemplate.SortDescriptors.Clear();
                dgvCallError.MasterTemplate.SortDescriptors.Add(sortED);
                dgvCallError.MasterTemplate.SortDescriptors.Add(sortEZ);

                if (this.dgvCallError.Rows.Count > 0)
                {

                    for (Int32 i = 0; i <= this.dgvCallError.Columns.Count - 1; i++)
                    {
                        string ColName = dgv.Columns[i].Name.ToString();
                        this.dgvCallError.Columns[i].ReadOnly = true;
                        switch (ColName)
                        {
                            case "Select":
                                this.dgvCallError.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                                this.dgvCallError.Columns[i].IsVisible = false;
                                break;
                            case "Eintreffdatum":
                                this.dgvCallError.Columns[i].FormatString = "{0:d}";
                                this.dgvCallError.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                                break;
                            case "Eintreffzeit":
                                this.dgvCallError.Columns[i].FormatString = "{0:t}";
                                this.dgvCallError.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                                break;

                            case "Dicke":
                            case "Brutto":
                            case "Breite":
                                this.dgvCallError.Columns[i].FormatString = "{0:N2}";
                                this.dgvCallError.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
                                break;
                            case "erstellt":
                                this.dgvCallError.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                                break;

                            default:
                                this.dgvCallError.Columns[i].IsVisible = true;
                                break;
                        }
                    }
                    this.dgvCallError.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            ClearCallEditInputFields();
            SetCallEditInputFieldsEnabled((this.dgvCallError.Rows.Count > 0));
            SetMenueCallEdit();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabPage_Call_SelectedPageChanged(object sender, EventArgs e)
        {
            enumTabPage_ctrASNCall enumCall = enumTabPage_ctrASNCall.pageView_Call;
            if (Enum.TryParse(tabPage_Call.SelectedPage.Name.ToString(), out enumCall))
            {
                switch (enumCall)
                {
                    case enumTabPage_ctrASNCall.pageView_Call:
                        InitDGV();
                        break;
                    case enumTabPage_ctrASNCall.pageView_CallError:
                        InitDGVCallError();
                        break;
                    default:

                        break;
                }
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtn_CallError_Refresh_Click(object sender, EventArgs e)
        {
            InitDGVCallError();
            Abruf = new clsASNCall();
            Abruf.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, 0);
            InitValToCtr_Edit();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtn_CallError_ExcelExport_Click(object sender, EventArgs e)
        {
            bool openExportFile = false;
            LVS.clsExcel Excel = new clsExcel();
            string FileName = DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + const_FileName + "Error";
            //DataTable dtTmp = ASN.dtASNForEingang.DefaultView.ToTable(true, "ASN", "ASN-Datum", "AuftraggeberView", "EmpfaengerView", "VS-Datum", "LfsNr");
            string FilePath = Excel.ExportDataTableToWorksheet(Abruf.dtAbrufUBListError, AttachmentPath + "\\" + FileName);
            openExportFile = true;

            if (openExportFile)
            {
                try
                {
                    System.Diagnostics.Process.Start(FilePath);
                }
                catch (Exception ex)
                {
                    clsError error = new clsError();
                    error._GL_User = this.GL_User;
                    error.Code = clsError.code1_501;
                    error.Aktion = "Abruf / UB List - Excelexport öffnen";
                    error.exceptText = ex.ToString();
                    error.WriteError();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetCallValueToCtr()
        {
            if (this.Abruf is clsASNCall)
            {
                this.tbCallId.Text = this.Abruf.ID.ToString();
                this.tbArtikelID.Text = this.Abruf.ArtikelID.ToString();
                this.tbLVSNr.Text = this.Abruf.LVSNr.ToString();
                this.tbProdNr.Text = this.Abruf.Produktionsnummer.ToString();
                this.tbWerksNr.Text = this.Abruf.Werksnummer.ToString();
                this.tbCharge.Text = this.Abruf.Charge.ToString();
                this.tbBrutto.Text = this.Abruf.Brutto.ToString();
                this.tbDescription.Text = this.Abruf.Description + Environment.NewLine +
                                          DateTime.Now.ToString("dd.MM.yyyy hh:mm") +
                                          " - Abruf korrigiert von " + this.GL_User.LoginName +
                                          " [" + this.GL_User.User_ID.ToString() + "]";
            }
            if (this.Abruf.ArtikelReferenz is clsArtikel)
            {
                this.tbArtikelID_Edit.Text = this.Abruf.ArtikelReferenz.ID.ToString();
                this.tbLVSNr_Edit.Text = this.Abruf.ArtikelReferenz.LVS_ID.ToString();
                this.tbProdNr_Edit.Text = this.Abruf.ArtikelReferenz.Produktionsnummer;
                this.tbWerksNr_Edit.Text = this.Abruf.ArtikelReferenz.Werksnummer;
                this.tbCharge_Edit.Text = this.Abruf.ArtikelReferenz.Charge;
                this.tbBrutto_Edit.Text = this.Abruf.ArtikelReferenz.Brutto.ToString();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void ClearCallEditInputFields()
        {
            this.tbCallId.Text = string.Empty;
            this.tbArtikelID.Text = string.Empty;
            this.tbArtikelID_Edit.Text = string.Empty;
            this.tbLVSNr.Text = string.Empty;
            this.tbLVSNr_Edit.Text = string.Empty;
            this.tbProdNr.Text = string.Empty;
            this.tbProdNr_Edit.Text = string.Empty;
            this.tbWerksNr.Text = string.Empty;
            this.tbWerksNr_Edit.Text = string.Empty;
            this.tbCharge.Text = string.Empty;
            this.tbCharge_Edit.Text = string.Empty;
            this.tbBrutto.Text = string.Empty;
            this.tbBrutto_Edit.Text = string.Empty;
            this.tbDescription.Text = string.Empty;

            this.tbSearchLVSNR.Text = string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bEnabled"></param>
        private void SetCallEditInputFieldsEnabled(bool bEnabled)
        {
            this.tbCallId.Enabled = false;
            this.tbArtikelID.Enabled = bEnabled;
            this.tbArtikelID_Edit.Enabled = bEnabled;
            this.tbLVSNr.Enabled = bEnabled;
            this.tbLVSNr_Edit.Enabled = bEnabled;
            this.tbProdNr.Enabled = bEnabled;
            this.tbProdNr_Edit.Enabled = bEnabled;
            this.tbWerksNr.Enabled = bEnabled;
            this.tbWerksNr_Edit.Enabled = bEnabled;
            this.tbCharge.Enabled = bEnabled;
            this.tbCharge_Edit.Enabled = bEnabled;
            this.tbBrutto.Enabled = bEnabled;
            this.tbBrutto_Edit.Enabled = bEnabled;
            this.tbDescription.Enabled = bEnabled;

            this.dgvArtikelSelection.Enabled = bEnabled;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvCallError_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (this.dgvCallError.Rows.Count > 0)
            {
                int iCallId = 0;
                int.TryParse(this.dgvCallError.Rows[e.RowIndex].Cells["AbrufID"].Value.ToString(), out iCallId);
                if (iCallId > 0)
                {
                    ClearCallEditInputFields();
                    SetCallEditInputFieldsEnabled(false);
                    Abruf = new clsASNCall();
                    Abruf.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, iCallId);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvCallError_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            InitValToCtr_Edit();
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitValToCtr_Edit()
        {
            if ((Abruf is clsASNCall) && (Abruf.ID > 0))
            {
                SetCallEditInputFieldsEnabled(true);
                SetCallValueToCtr();
                InitDgvArtikelSelection();
                SetMenueCallEdit();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitDgvArtikelSelection()
        {
            dtSourceArtikelSelection.Rows.Clear();
            this.dgvArtikelSelection.Refresh();

            if (this.Abruf.AbBereichID > 0)
            {
                dtSourceArtikelSelection = Abruf.ArtikelReferenz.GetArtikellistForCallCorrection(this.Abruf, this.tbSearchLVSNR.Text.Trim());
            }
            this.dgvArtikelSelection.DataSource = dtSourceArtikelSelection;
            if (this.dgvArtikelSelection.Rows.Count > 0)
            {

                for (Int32 i = 0; i <= this.dgvArtikelSelection.Columns.Count - 1; i++)
                {
                    string ColName = "Artikel." + this.dgvArtikelSelection.Columns[i].Name.ToString();
                    this.dgvArtikelSelection.Columns[i].ReadOnly = true;
                    switch (ColName)
                    {
                        case clsArtikel.ArtikelField_ID:
                            this.dgvArtikelSelection.Columns[i].HeaderText = "ArtikelID";
                            //this.dgvArtikelSelection.Columns[i].FormatString = "{N0}";
                            this.dgvArtikelSelection.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                            break;

                        case clsArtikel.ArtikelField_LVSID:
                            this.dgvArtikelSelection.Columns[i].HeaderText = "LVSNr";
                            this.dgvArtikelSelection.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                            break;

                        case clsArtikel.ArtikelField_Produktionsnummer:
                        case clsArtikel.ArtikelField_Werksnummer:
                            this.dgvArtikelSelection.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                            break;

                        case "Artikel.OrderID":
                            this.dgvArtikelSelection.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                            break;

                        case "Artikel.Match":
                            this.dgvArtikelSelection.Columns[i].TextAlignment = System.Drawing.ContentAlignment.TopLeft;
                            this.dgvArtikelSelection.Columns[i].WrapText = true;
                            break;


                        default:
                            this.dgvArtikelSelection.Columns[i].IsVisible = false;
                            break;
                    }
                }
                this.dgvArtikelSelection.BestFitColumns();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvArtikelSelection_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (this.dgvArtikelSelection.Rows.Count > 0)
            {
                int iArtId = 0;
                //int.TryParse(this.dgvArtikelSelection.Rows[e.RowIndex].Cells["ArtikelID"].Value.ToString(), out iArtId);
                int.TryParse(this.dgvArtikelSelection.Rows[e.RowIndex].Cells["ID"].Value.ToString(), out iArtId);
                if (iArtId > 0)
                {
                    Abruf.ArtikelReferenz = new clsArtikel();
                    Abruf.ArtikelReferenz.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System);
                    Abruf.ArtikelReferenz.ID = iArtId;

                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvArtikelSelection_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if ((Abruf.ArtikelReferenz is clsArtikel) && (Abruf.ArtikelReferenz.ID > 0))
            {
                Abruf.ArtikelReferenz.GetArtikeldatenByTableID();
                //SetCallEditInputFieldsEnabled(true);
                SetCallValueToCtr();
                //InitDgvArtikelSelection();
                SetMenueCallEdit();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbtnClearField_Click(object sender, EventArgs e)
        {
            ClearCallEditInputFields();
            SetCallEditInputFieldsEnabled(false);
            this.Abruf.ArtikelReferenz = null;
            SetMenueCallEdit();

            this.dgvArtikelSelection.DataSource = null;
            //LVS.Helper.helper_Telerik_GridView.Rows_SetUnselected(ref this.dgvCallError);
            GridViewRowSetUnselected.Rows_SetUnselected(ref this.dgvCallError);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbtnCallClose_Click(object sender, EventArgs e)
        {
            this._ctrMenu.CloseCtrASNCall();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbtnCallChange_Click(object sender, EventArgs e)
        {
            SetClsCallChangeValueAndSave();
            Abruf = new clsASNCall();
            Abruf.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, 0);

            //this.mmpManuelSearch.ExpandedCallapsed = Sped4.Controls.AFMinMaxPanel.EStatus.Collapsed;
            //this.mmpManuelSearch.ExpandedCallapsed = Sped4.Controls.AFMinMaxPanel.EStatus.Expanded;
            ClearCallEditInputFields();
            SetCallEditInputFieldsEnabled(false);
            InitDGVCallError();
            InitDgvArtikelSelection();
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetClsCallChangeValueAndSave()
        {
            if ((Abruf.ArtikelReferenz is clsArtikel) && (Abruf.ArtikelReferenz.ID > 0))
            {
                int iArtId = 0;
                int.TryParse(this.tbArtikelID_Edit.Text, out iArtId);
                this.Abruf.ArtikelID = iArtId;
                int iLvsNr = 0;
                int.TryParse(this.tbLVSNr_Edit.Text, out iLvsNr);
                this.Abruf.LVSNr = iLvsNr;
                this.Abruf.Produktionsnummer = this.tbProdNr_Edit.Text;
                this.Abruf.Werksnummer = this.tbWerksNr_Edit.Text;
                this.Abruf.Charge = this.tbCharge_Edit.Text;
                this.Abruf.Brutto = this.Abruf.ArtikelReferenz.Brutto;
                this.Abruf.Description = this.tbDescription.Text.Trim();

                if (this._ctrMenu._frmMain.system.AbBereich.Company is clsCompany)
                {
                    this.Abruf.CompanyID = this._ctrMenu._frmMain.system.AbBereich.Company.ID;
                    this.Abruf.CompanyName = this._ctrMenu._frmMain.system.AbBereich.Company.Shortname;
                }
                this.Abruf.Status = clsASNCall.const_Status_erstellt;
                this.Abruf.LiefAdrID = (int)this.Abruf.ArtikelReferenz.Eingang.Auftraggeber;
                this.Abruf.EmpAdrID = (int)this.Abruf.ArtikelReferenz.Eingang.Empfaenger;
                this.Abruf.ASNLieferant = this.Abruf.ArtikelReferenz.Eingang.Lieferant;
                this.Abruf.ASNQuantity = (int)this.Abruf.ArtikelReferenz.Brutto;
                this.Abruf.ASNUnit = this.Abruf.ArtikelReferenz.Einheit;
                this.Abruf.Update();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetMenueCallEdit()
        {
            this.tsbtnCallChange.Enabled = ((this.Abruf.ArtikelReferenz is clsArtikel) && (this.Abruf.ArtikelReferenz.ID > 0));
            this.tsbtnClearField.Enabled = (!this.tbCallId.Text.Equals(string.Empty));
            this.tsbtnRefreshSelCallValueToInputFields.Enabled = (!this.tbCallId.Text.Equals(string.Empty));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbtnRefreshSelCallValueToInputFields_Click(object sender, EventArgs e)
        {
            InitValToCtr_Edit();
        }
        /// <summary>
        ///             manuelle Suche starten
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnManuellSearch_Click(object sender, EventArgs e)
        {
            InitValToCtr_Edit();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnClearLVSNR_Click(object sender, EventArgs e)
        {
            this.tbSearchLVSNR.Text = string.Empty;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (this.mmpManuelSearch.ExpandedCallapsed == Sped4.Controls.AFMinMaxPanel.EStatus.Collapsed)
            {
                this.mmpManuelSearch.SetExpandCollapse(Sped4.Controls.AFMinMaxPanel.EStatus.Collapsed);
                //this.mmpManuelSearch.SetExpandCollapse(Sped4.Controls.AFMinMaxPanel.EStatus.Expanded);
                ;
            }
            else
            {
                //this.mmpManuelSearch.SetExpandCollapse(Sped4.Controls.AFMinMaxPanel.EStatus.Collapsed);
                this.mmpManuelSearch.SetExpandCollapse(Sped4.Controls.AFMinMaxPanel.EStatus.Expanded);
            }
        }
    }

}
