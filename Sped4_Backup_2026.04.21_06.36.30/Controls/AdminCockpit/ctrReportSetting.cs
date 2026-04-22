using LVS;
using LVS.Helper;
using LVS.ViewData;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Sped4.Controls.AdminCockpit
{
    public partial class ctrReportSetting : UserControl
    {

        internal clsReportDocSetting ReportDocSetting;
        internal clsDocKey DocKeys;
        internal clsReportDocSettingAssignment ReportDocSettingAss;
        public Globals._GL_USER GLUser;
        public ctrMenu _ctrMenu;
        public Int32 SearchButton = 0;
        internal AddressViewData adrVD = new AddressViewData();
        //internal clsADR RepADR;
        internal string strListRange = string.Empty;
        internal clsArbeitsbereiche SelArbeitsbereich;
        /************************************************************
        *               Procedure / Methode
        ************************************************************/
        ///<summary>ctrReportSetting/ ctrReportSetting</summary>
        ///<remarks></remarks>
        public ctrReportSetting(ctrMenu myMenu)
        {
            InitializeComponent();
            this._ctrMenu = myMenu;
        }
        ///<summary>ctrReportSetting/ ctrReportSetting_Load</summary>
        ///<remarks></remarks>
        private void ctrReportSetting_Load(object sender, EventArgs e)
        {
            this.GLUser = this._ctrMenu._frmMain.GL_User;
            ReportDocSetting = new clsReportDocSetting();

            this.ReportDocSetting.GLUser = this.GLUser;
            DocKeys = new clsDocKey();
            adrVD = new AddressViewData();
            //RepADR = new clsADR();
            //RepADR.InitClass(this.GLUser, this._ctrMenu._frmMain.GL_System, 0, true);

            this.comboDocArt.DataSource = clsReportDocSetting.GetDocArt(this.GLUser);

            this.comboDocKey.DataSource = DocKeys.dtDocKeys;
            this.comboDocKey.DisplayMember = "DocKey";
            this.comboDocKey.ValueMember = "ID";

            this.comboDocKey2.DataSource = DocKeys.dtDocKeys;
            this.comboDocKey2.DisplayMember = "DocKey";
            this.comboDocKey2.ValueMember = "ID";

            //DataTable dtWorkspaces = new DataTable();
            //dtWorkspaces = 

            this.comboArbeitsbereich.DataSource = clsArbeitsbereiche.GetArbeitsbereichList(this.GLUser.User_ID);
            this.comboArbeitsbereich.DisplayMember = "Arbeitsbereich";
            this.comboArbeitsbereich.ValueMember = "ID";

            //this.CheckListWorkspace.Items
            //this.CheckListWorkspace.DataSource = clsArbeitsbereiche.GetArbeitsbereichList(this.GLUser.User_ID); ; // clsArbeitsbereiche.GetArbeitsbereichList(this.GLUser.User_ID);
            //this.CheckListWorkspace.DisplayMember = "Arbeitsbereich";
            //this.CheckListWorkspace.ValueMember = "ID";

            this.CheckListWorkspace.DataSource = clsArbeitsbereiche.GetArbeitsbereichList(this.GLUser.User_ID);
            this.CheckListWorkspace.DisplayMember = "Arbeitsbereich";
            this.CheckListWorkspace.ValueMember = "ID";


            this.comboSelArbeitsbereich.DataSource = clsArbeitsbereiche.GetArbeitsbereichList(this.GLUser.User_ID);
            this.comboSelArbeitsbereich.DisplayMember = "Arbeitsbereich";
            this.comboSelArbeitsbereich.ValueMember = "ID";

            this.gbReportDocSetting.Enabled = true;
            this.gbReportDocSettingAssignment.Enabled = true;
            strListRange = clsReportDocSetting.const_ReportSetting.ToString();
            InitDGV(strListRange);
        }
        ///<summary>ctrReportSetting/ InitDGV</summary>
        ///<remarks></remarks>
        private void InitDGV(string myRange)
        {
            int iTmp = 0;
            int.TryParse(this.comboSelArbeitsbereich.SelectedValue.ToString(), out iTmp);
            if (iTmp > 0)
            {
                SelArbeitsbereich = new clsArbeitsbereiche();
                SelArbeitsbereich.InitCls(this.GLUser, iTmp);

                lInfoDGV.Text = "[" + myRange + "]";
                DataTable dtSource = clsReportDocSetting.GetReportSettings(this.GLUser, myRange, this.SelArbeitsbereich);
                DataColumn column = new DataColumn();
                column.ColumnName = "FileInfo";
                dtSource.Columns.Add(column);
                this.dgv.DataSource = dtSource;

                if (dgv.Columns["FileInfo"] != null)
                {
                    dgv.Columns.Move(dgv.Columns["FileInfo"].Index, 0);
                }

                foreach (GridViewColumn col in this.dgv.Columns)
                {
                    GridViewDataColumn tmpDataCol;
                    switch (col.Name)
                    {
                        case "FileInfo":
                            col.HeaderText = "Datei";
                            col.ImageLayout = ImageLayout.Center;
                            col.AutoSizeMode = BestFitColumnMode.DisplayedCells;
                            break;
                        case "Report":
                        case "FileExist":
                            col.IsVisible = false;
                            break;
                    }
                }
                this.dgv.BestFitColumns();
            }
        }

        ///<summary>ctrReportSetting/ dgv_SelectionChanged</summary>
        ///<remarks></remarks>
        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            Int32 iTmp = 0;
            if (this.dgv.Rows.Count > 0)
            {
                if (this.ReportDocSetting is clsReportDocSetting)
                {
                    Int32.TryParse(dgv.SelectedRows[0].Cells["ID"].Value.ToString(), out iTmp);
                    this.ReportDocSetting.ID = iTmp;
                    this.ReportDocSetting.FillClsbyID();
                }
                if (this.ReportDocSettingAss is clsReportDocSettingAssignment)
                {
                    Int32.TryParse(dgv.SelectedRows[0].Cells["ID"].Value.ToString(), out iTmp);
                    this.ReportDocSettingAss.ID = iTmp;
                    this.ReportDocSettingAss.Fill();
                }
            }

            this.gbReportDocSetting.Enabled = (this.ReportDocSetting is clsReportDocSetting);
            ClearReportDocSettingInputFields();
            this.gbReportDocSettingAssignment.Enabled = (this.ReportDocSettingAss is clsReportDocSettingAssignment);
            ClearReportDocSettingAssignmentInputFields();

            if (this.ReportDocSetting is clsReportDocSetting)
            {
                SetReportDocSettingValueToCtr();
            }
            if (this.ReportDocSettingAss is clsReportDocSettingAssignment)
            {
                SetReportDocSettingAssignmentToCtr();
            }

        }
        ///<summary>ctrReportSetting/ dgv_MouseDoubleClick</summary>
        ///<remarks></remarks>
        private void dgv_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //this.gbReportDocSetting.Enabled = (this.ReportDocSetting is clsReportDocSetting);
            //ClearReportDocSettingInputFields();
            //this.gbReportDocSettingAssignment.Enabled = (this.ReportDocSettingAss is clsReportDocSettingAssignment);
            //ClearReportDocSettingAssignmentInputFields();

            //if (this.ReportDocSetting is clsReportDocSetting)
            //{
            //    SetReportDocSettingValueToCtr();
            //}
            //if (this.ReportDocSettingAss is clsReportDocSettingAssignment)
            //{
            //    SetReportDocSettingAssignmentToCtr();
            //}
        }

        ///<summary>ctrReportSetting/ rmiRefreshDGV_Click</summary>
        ///<remarks></remarks>
        private void rmiRefreshDGV_Click(object sender, EventArgs e)
        {
            DGVRefresh();
        }
        /// <summary>
        /// 
        /// </summary>
        private void DGVRefresh()
        {
            if (strListRange.Equals(string.Empty))
            {
                if (this.ReportDocSetting is clsReportDocSetting)
                {
                    strListRange = clsReportDocSetting.const_ReportSetting.ToString();
                }
                if (this.ReportDocSettingAss is clsReportDocSettingAssignment)
                {
                    strListRange = clsReportDocSetting.const_ReportSettingAssignment_All.ToString();
                }
            }
            InitDGV(strListRange);
        }
        ///<summary>ctrReportSetting/ ClearReportDocSettingInputFields</summary>
        ///<remarks></remarks>
        private void ClearReportDocSettingInputFields()
        {
            this.tbRepDocSetID.Text = string.Empty;
            cbActiv.Checked = true;
            cbCanUseTxtModul.Checked = true;
            comboDocKey.SelectedIndex = -1;
            comboDocArt.SelectedIndex = -1;
            tbViewID.Text = string.Empty;
            nudPrintCount.Value = 1;
        }
        ///<summary>ctrReportSetting/ SetReportDocSettingValueToCtr</summary>
        ///<remarks></remarks>
        private void SetReportDocSettingValueToCtr()
        {
            if (this.ReportDocSetting is clsReportDocSetting)
            {
                this.tbRepDocSetID.Text = this.ReportDocSetting.ID.ToString();
                this.cbActiv.Checked = this.ReportDocSetting.activ;
                this.cbCanUseTxtModul.Checked = this.ReportDocSetting.CanUseTxtModul;
                Functions.SetComboToSelecetedItem(ref comboDocKey, this.ReportDocSetting.DocKey);
                Functions.SetComboToSelecetedItem(ref comboDocArt, this.ReportDocSetting.Art);
                this.tbViewID.Text = this.ReportDocSetting.ViewID;
                this.nudPrintCount.Value = (decimal)this.ReportDocSetting.PrintCount;
            }
        }
        ///<summary>ctrReportSetting/ ClearReportDocSettingAssignmentInputFields</summary>
        ///<remarks></remarks>
        private void ClearReportDocSettingAssignmentInputFields()
        {
            this.tbRepDocSetAssID.Text = string.Empty;
            this.cbIsDefault.Checked = true;
            comboArbeitsbereich.SelectedIndex = -1;
            comboDocKey2.SelectedIndex = -1;
            this.tbPath.Text = helper_MandantenReportPath.GetPath(this._ctrMenu._frmMain.system.Mandant);
            this.tbReportFile.Text = string.Empty;
            this.tbAdr.Text = string.Empty;
            this.nudAdrId.Value = 0;
            this.pbReportData.Image = null;
        }
        ///<summary>ctrReportSetting/ SetReportDocSettingAssignmentToCtr</summary>
        ///<remarks></remarks>
        private void SetReportDocSettingAssignmentToCtr()
        {
            if (this.ReportDocSettingAss is clsReportDocSettingAssignment)
            {
                this.tbRepDocSetAssID.Text = this.ReportDocSettingAss.ID.ToString();
                this.cbIsDefault.Checked = this.ReportDocSettingAss.IsDefault;
                Functions.SetComboToSelecetedItem(ref comboArbeitsbereich, this.ReportDocSettingAss.AbBereichName);
                Functions.SetComboToSelecetedItem(ref comboDocKey2, this.ReportDocSettingAss.DocKey);
                this.tbPath.Text = this.ReportDocSettingAss.Path;
                this.tbReportFile.Text = this.ReportDocSettingAss.ReportFileName;
                this.tbAdr.Text = this.ReportDocSettingAss.AdrStringShort;
                this.nudAdrId.Value = this.ReportDocSettingAss.AdrID;
                if (this.ReportDocSettingAss.ExistReportData)
                {
                    this.pbReportData.Image = Sped4.Properties.Resources.bullet_ball_green_16x16;
                }
                else
                {
                    this.pbReportData.Image = Sped4.Properties.Resources.bullet_ball_red_16x16;
                }
            }
        }
        ///<summary>ctrReportSetting/ miListReportDocSetting_Click</summary>
        ///<remarks></remarks>
        private void miListReportDocSetting_Click(object sender, EventArgs e)
        {
            strListRange = clsReportDocSetting.const_ReportSetting.ToString();
            this.ReportDocSetting = new clsReportDocSetting();
            this.ReportDocSetting.InitClass(this.GLUser, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, 0, this._ctrMenu._frmMain.system.AbBereich.ID);
            this.ReportDocSettingAss = null;
            InitDGV(strListRange);
        }
        ///<summary>ctrReportSetting/ miRepDocSetAssAll_Click</summary>
        ///<remarks></remarks>
        private void miRepDocSetAssAll_Click(object sender, EventArgs e)
        {
            //strListRange = clsReportDocSetting.const_ReportSettingAssignment_All.ToString();
            //this.ReportDocSetting = null;
            //this.ReportDocSettingAss = new clsReportDocSettingAssignment();
            //this.ReportDocSettingAss.InitClass(this.GLUser, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system);
            //InitDGV(strListRange);
            RefreshRepDocSetAssAll();
        }

        private void RefreshRepDocSetAssAll()
        {
            strListRange = clsReportDocSetting.const_ReportSettingAssignment_All.ToString();
            this.ReportDocSetting = null;
            this.ReportDocSettingAss = new clsReportDocSettingAssignment();
            this.ReportDocSettingAss.InitClass(this.GLUser, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system);
            InitDGV(strListRange);
        }

        ///<summary>ctrReportSetting/ miRepDocSetAssDefault_Click</summary>
        ///<remarks></remarks>
        private void miRepDocSetAssDefault_Click(object sender, EventArgs e)
        {
            //strListRange = clsReportDocSetting.const_ReportSettingAssignment_default.ToString();
            //this.ReportDocSetting = null;
            //this.ReportDocSettingAss = new clsReportDocSettingAssignment();
            //this.ReportDocSettingAss.InitClass(this.GLUser, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system);
            //InitDGV(strListRange);
        }
        ///<summary>ctrReportSetting/ miRepDocSetAssCustomize_Click</summary>
        ///<remarks></remarks>
        private void miRepDocSetAssCustomize_Click(object sender, EventArgs e)
        {
            strListRange = clsReportDocSetting.const_ReportSettingAssignment_customize.ToString();
            this.ReportDocSetting = null;
            this.ReportDocSettingAss = new clsReportDocSettingAssignment();
            this.ReportDocSettingAss.InitClass(this.GLUser, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system);
            InitDGV(strListRange);
        }
        ///<summary>ctrReportSetting/ miAddRepDocSet_Click</summary>
        ///<remarks></remarks>
        private void miAddRepDocSet_Click(object sender, EventArgs e)
        {
            //this.ReportDocSetting = new clsReportDocSetting();
            //this.ReportDocSetting.InitClass(this.GLUser, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, 0, this._ctrMenu._frmMain.system.AbBereich.ID);
            //this.ReportDocSettingAss = null;
            //this.gbReportDocSetting.Enabled = (this.ReportDocSetting is clsReportDocSetting);
            //ClearReportDocSettingInputFields();
            //this.gbReportDocSettingAssignment.Enabled = (this.ReportDocSettingAss is clsReportDocSettingAssignment);
            //ClearReportDocSettingAssignmentInputFields();

            InitForAddNew();
        }

        private void InitForAddNew()
        {
            this.ReportDocSetting = new clsReportDocSetting();
            this.ReportDocSetting.InitClass(this.GLUser, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, 0, this._ctrMenu._frmMain.system.AbBereich.ID);
            this.ReportDocSettingAss = null;
            this.gbReportDocSetting.Enabled = (this.ReportDocSetting is clsReportDocSetting);
            ClearReportDocSettingInputFields();
            this.gbReportDocSettingAssignment.Enabled = (this.ReportDocSettingAss is clsReportDocSettingAssignment);
            ClearReportDocSettingAssignmentInputFields();
        }
        ///<summary>ctrReportSetting/ miAddRepDocSetAss_Click</summary>
        ///<remarks></remarks>
        private void miAddRepDocSetAss_Click(object sender, EventArgs e)
        {
            this.ReportDocSetting = null;
            this.ReportDocSettingAss = new clsReportDocSettingAssignment();
            this.ReportDocSettingAss.InitClass(this.GLUser, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system);
            this.gbReportDocSetting.Enabled = (this.ReportDocSetting is clsReportDocSetting);
            ClearReportDocSettingInputFields();
            this.gbReportDocSettingAssignment.Enabled = (this.ReportDocSettingAss is clsReportDocSettingAssignment);
            ClearReportDocSettingAssignmentInputFields();
        }
        ///<summary>ctrReportSetting/ btnPathSearch_Click</summary>
        ///<remarks>Pfadsuche</remarks>
        private void btnPathSearch_Click(object sender, EventArgs e)
        {
            FolderBrowser.Description = "Pfadsuche";
            FolderBrowser.SelectedPath = helper_MandantenReportPath.GetPath(this._ctrMenu._frmMain.system.Mandant);

            DialogResult objResult = FolderBrowser.ShowDialog(this);
            if (objResult == DialogResult.OK)
                tbPath.Text = FolderBrowser.SelectedPath + "\\";
        }
        ///<summary>ctrReportSetting/ btnRepSearch_Click</summary>
        ///<remarks>Suche Reportdatei</remarks>
        private void btnRepSearch_Click(object sender, EventArgs e)
        {
            FileDialog.Title = "Suche Reportdatei";
            FileDialog.Filter = "Report|*.trdx; *.trbp";
            FileDialog.InitialDirectory = helper_MandantenReportPath.GetPath(this._ctrMenu._frmMain.system.Mandant);
            FileDialog.FileName = string.Empty;
            DialogResult result = FileDialog.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                tbReportFile.Text = System.IO.Path.GetFileName(FileDialog.FileName);
            }
        }
        ///<summary>ctrReportSetting/ btnADRSearch_Click</summary>
        ///<remarks>Suche Adresse</remarks>
        private void btnADRSearch_Click(object sender, EventArgs e)
        {
            SearchButton = 1;
            this._ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrReportSetting/ btnPathClear_Click</summary>
        ///<remarks></remarks>
        private void btnPathClear_Click(object sender, EventArgs e)
        {
            this.tbPath.Text = string.Empty;
        }
        ///<summary>ctrReportSetting/ btnRepClear_Click</summary>
        ///<remarks></remarks>
        private void btnRepClear_Click(object sender, EventArgs e)
        {
            this.tbReportFile.Text = string.Empty;
        }
        ///<summary>ctrReportSetting/ btnADRClear_Click</summary>
        ///<remarks></remarks>
        private void btnADRClear_Click(object sender, EventArgs e)
        {
            this.tbAdr.Text = string.Empty;
            if (this.tbAdr.Text.Equals(string.Empty))
            {
                nudAdrId.Value = 0;
                TakeOverAdrID(nudAdrId.Value);
            }
        }
        ///<summary>ctrReportSetting/ TakeOverAdrID</summary>
        ///<remarks></remarks>
        public void TakeOverAdrID(decimal myDecADR_ID)
        {
            if (this.ReportDocSettingAss is clsReportDocSettingAssignment)
            {
                this.ReportDocSettingAss.AdrID = 0;
                this.tbAdr.Text = string.Empty;
                adrVD = new AddressViewData();
                if (myDecADR_ID > 0)
                {
                    adrVD = new AddressViewData((int)myDecADR_ID, (int)_ctrMenu.GL_User.User_ID);
                    this.ReportDocSettingAss.AdrID = adrVD.Address.Id;
                    this.tbAdr.Text = adrVD.Address.AddressStringShort;
                }
            }
        }
        ///<summary>ctrReportSetting/ btnSave_Click</summary>
        ///<remarks></remarks>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ReportDocSetting is clsReportDocSetting)
            {
                this.ReportDocSetting.DocKeyID = (int)this.comboDocKey.SelectedValue;
                this.ReportDocSetting.DocKey = DocKeys.GetDocKey(this.ReportDocSetting.DocKeyID);
                this.ReportDocSetting.ViewID = this.tbViewID.Text.Trim();
                this.ReportDocSetting.PrintCount = (Int32)nudPrintCount.Value;
                this.ReportDocSetting.Art = this.comboDocArt.SelectedText.ToString();
                this.ReportDocSetting.activ = this.cbActiv.Checked;
                this.ReportDocSetting.CanUseTxtModul = this.cbCanUseTxtModul.Checked;
                this.ReportDocSetting.AdrID = nudAdrId.Value;

                if (this.ReportDocSetting.ID > 0)
                {
                    this.ReportDocSetting.Update();
                }
                else
                {
                    this.ReportDocSetting.ID = 0;
                    if (!this.ReportDocSetting.ExistDocKey)
                    {
                        this.ReportDocSetting.Add();
                    }
                }
            }
            if (this.ReportDocSettingAss is clsReportDocSettingAssignment)
            {
                if (
                    ((Int32)this.comboDocKey2.SelectedIndex > -1) &&
                    (comboArbeitsbereich.SelectedIndex > -1)
                   )
                {

                    Int32 iTmp = 0;
                    Int32.TryParse(tbRepDocSetAssID.Text, out iTmp);
                    this.ReportDocSettingAss.ID = iTmp;
                    this.ReportDocSettingAss.DocKeyID = (Int32)this.comboDocKey2.SelectedValue;
                    this.ReportDocSettingAss.DocKey = comboDocKey2.Text;
                    this.ReportDocSettingAss.Path = tbPath.Text.Trim();
                    this.ReportDocSettingAss.ReportFileName = tbReportFile.Text.Trim();
                    this.ReportDocSettingAss.AdrID = nudAdrId.Value;
                    this.ReportDocSettingAss.IsDefault = cbIsDefault.Checked;
                    if (this.ReportDocSettingAss.AdrID > 0)
                    {
                        this.ReportDocSettingAss.IsDefault = false;
                    }
                    decimal decTmp = 0;
                    decimal.TryParse(comboArbeitsbereich.SelectedValue.ToString(), out decTmp);

                    WorkspaceViewData workspaceViewData = new WorkspaceViewData((int)decTmp);
                    this.ReportDocSettingAss.AbBereichID = workspaceViewData.Workspace.Id;
                    this.ReportDocSettingAss.MandantenID = workspaceViewData.Workspace.MandantId;

                    string strFilePath = Path.Combine(this.ReportDocSettingAss.Path, this.ReportDocSettingAss.ReportFileName);
                    if (!strFilePath.StartsWith(this.ReportDocSettingAss.StartupPath))
                    {
                        strFilePath = helper_CheckAndGetFilePathWithStartupPath.GetPathValueRange(this.ReportDocSettingAss.StartupPath, strFilePath);
                        //strFilePath = Path.Combine(this.ReportDocSettingAss.StartupPath, strFilePath);
                    }
                    else
                    {
                        strFilePath = Path.Combine(this.ReportDocSettingAss.StartupPath, strFilePath);
                    }

                    if ((this.ReportDocSettingAss.AbBereichID > 0) && (File.Exists(strFilePath)))
                    {
                        this.ReportDocSettingAss.Path = helper_CheckAndGetPathWithoutStartupPath.GetPathValueRange(this.ReportDocSettingAss.StartupPath, strFilePath);
                        this.ReportDocSettingAss.FileExtension = Path.GetExtension(@strFilePath);

                        if (this.ReportDocSettingAss.ID > 0)
                        {
                            this.ReportDocSettingAss.Update();
                        }
                        else
                        {
                            this.ReportDocSettingAss.ID = 0;
                            this.ReportDocSettingAss.Add();
                        }
                    }
                    else
                    {
                        string Mes = "Folgende Fehler liegen vor: " + Environment.NewLine;
                        if (this.ReportDocSettingAss.AbBereichID < 1)
                        {
                            Mes += " - Es wurde kein Arbeitsbereich ausgewählt" + Environment.NewLine;
                            Mes += "   ID: " + this.ReportDocSettingAss.AbBereichID.ToString() + Environment.NewLine;
                        }
                        if (File.Exists(strFilePath))
                        {
                            Mes += " - Der Report konnte nicht gefunden werden" + Environment.NewLine;
                            Mes += "   Pfad: " + strFilePath + Environment.NewLine;
                        }

                        MessageBox.Show(Mes, "ACHTUNG");
                    }
                }
                else
                {
                    MessageBox.Show("Bitte prüfen Sie, ob DocKey und Arbeitsbereich ausgewählt wurde!", "ACHTUNG");
                }
            }
            InitForAddNew();
            InitDGV(strListRange);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopyToWorkspace_Click(object sender, EventArgs e)
        {
            clsArbeitsbereiche tmpAB = new clsArbeitsbereiche();

            //if (this.ReportDocSetting is clsReportDocSetting)
            //{
            //}
            if (this.ReportDocSettingAss is clsReportDocSettingAssignment)
            {
                if (CheckListWorkspace.CheckedItems.Count > 0)
                {
                    clsReportDocSettingAssignment tmpCopy = this.ReportDocSettingAss.Copy();

                    foreach (var ws in CheckListWorkspace.CheckedItems)
                    {
                        int iSelWs = 0;
                        int.TryParse(ws.Value.ToString(), out iSelWs);
                        if (iSelWs > 0)
                        {
                            tmpCopy.ID = 0;
                            tmpAB.InitCls(this.GLUser, iSelWs);
                            tmpCopy.AbBereichID = tmpAB.ID;
                            tmpCopy.MandantenID = tmpAB.MandantenID;

                            //Check Eintrag nicht vorhanden?
                            if (!tmpCopy.ExistClassItem)
                            {
                                tmpCopy.ID = 0;
                                tmpCopy.Add();
                            }
                        }
                    }
                }


                //if (
                //    ((Int32)this.comboDocKey2.SelectedIndex > -1) &&
                //    (comboArbeitsbereich.SelectedIndex > -1)
                //   )
                //{

                //    Int32 iTmp = 0;
                //    Int32.TryParse(tbRepDocSetAssID.Text, out iTmp);
                //    this.ReportDocSettingAss.ID = iTmp;
                //    this.ReportDocSettingAss.DocKeyID = (Int32)this.comboDocKey2.SelectedValue;
                //    this.ReportDocSettingAss.DocKey = comboDocKey2.Text;
                //    this.ReportDocSettingAss.Path = tbPath.Text.Trim();
                //    this.ReportDocSettingAss.ReportFileName = tbReportFile.Text.Trim();
                //    this.ReportDocSettingAss.AdrID = this.RepADR.ID;
                //    this.ReportDocSettingAss.IsDefault = cbIsDefault.Checked;
                //    if (this.ReportDocSettingAss.AdrID > 0)
                //    {
                //        this.ReportDocSettingAss.IsDefault = false;
                //    }
                //    decimal decTmp = 0;
                //    decimal.TryParse(comboArbeitsbereich.SelectedValue.ToString(), out decTmp);

                //    tmpAB.InitCls(this.GLUser, decTmp);
                //    this.ReportDocSettingAss.AbBereichID = tmpAB.ID;
                //    this.ReportDocSettingAss.MandantenID = tmpAB.MandantenID;

                //    //string strFilePath = Path.Combine(Application.StartupPath + this.ReportDocSettingAss.Path, this.ReportDocSettingAss.ReportFileName);
                //    string strFilePath = Path.Combine(this.ReportDocSettingAss.Path, this.ReportDocSettingAss.ReportFileName);
                //    //this.ReportDocSettingAss.Path = helper_CheckAndGetPathWithoutStartupPath.GetPathValueRange(this._ctrMenu._frmMain.system.StartupPath, strFilePath);
                //    this.ReportDocSettingAss.Path = helper_CheckAndGetPathWithoutStartupPath.GetPathValueRange(this.ReportDocSettingAss.StartupPath, strFilePath);
                //    this.ReportDocSettingAss.FileExtension = Path.GetExtension(@strFilePath);

                //    if ((this.ReportDocSettingAss.AbBereichID > 0) && (File.Exists(strFilePath)))
                //    {
                //        if (this.ReportDocSettingAss.ID > 0)
                //        {
                //            this.ReportDocSettingAss.Update();
                //        }
                //        else
                //        {
                //            this.ReportDocSettingAss.ID = 0;
                //            this.ReportDocSettingAss.Add();
                //        }
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("Bitte prüfen Sie, ob DocKey und Arbeitsbereich ausgewählt wurde!", "ACHTUNG");
                //}
            }
            this.CheckListWorkspace.CheckedItems.Clear();
            InitForAddNew();
            InitDGV(strListRange);
        }

        ///<summary>ctrReportSetting/ btnDelete_Click</summary>
        ///<remarks></remarks>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.ReportDocSetting is clsReportDocSetting)
            {
                if (this.ReportDocSetting.ID > 0)
                {
                    this.ReportDocSetting.Delete();
                }
            }
            if (this.ReportDocSettingAss is clsReportDocSettingAssignment)
            {
                if (this.ReportDocSettingAss.ID > 0)
                {
                    this.ReportDocSettingAss.Delete();
                }
            }
            InitDGV(strListRange);
            InitForAddNew();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCopy_Click(object sender, EventArgs e)
        {
            if ((this.ReportDocSetting is clsReportDocSetting) && (this.ReportDocSetting.ID > 0))
            {
                this.ReportDocSetting.ID = 0;
                this.ReportDocSetting.activ = true;
                this.ReportDocSetting.CanUseTxtModul = false;
                this.ReportDocSetting.Add();
            }
            if ((this.ReportDocSettingAss is clsReportDocSettingAssignment) && (this.ReportDocSettingAss.ID > 0))
            {
                this.ReportDocSettingAss.ID = 0;
                this.ReportDocSettingAss.IsDefault = (this.ReportDocSettingAss.AdrID == 0);
                this.ReportDocSettingAss.Add();
            }
            InitDGV(strListRange);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboSelArbeitsbereich_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NudAdrReceiverDirect_Leave(object sender, EventArgs e)
        {
            TakeOverAdrID(nudAdrId.Value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveAllWorkspaces_Click(object sender, EventArgs e)
        {

            for (Int32 i = 0; i <= comboSelArbeitsbereich.Items.Count - 1; i++)
            {
                this.comboSelArbeitsbereich.SelectedIndex = i;
                int iAbId = 0;
                int.TryParse(this.comboSelArbeitsbereich.SelectedValue.ToString(), out iAbId);
                if (iAbId > 0)
                {
                    clsArbeitsbereiche TmpAB = new clsArbeitsbereiche();
                    TmpAB.InitCls(this.GLUser, iAbId);

                    if (this.ReportDocSettingAss is null)
                    {
                        this.ReportDocSettingAss = new clsReportDocSettingAssignment();
                    }

                    clsReportDocSettingAssignment rsa = this.ReportDocSettingAss;
                    rsa.AbBereichID = TmpAB.ID;
                    rsa.MandantenID = TmpAB.MandantenID;
                    if (!rsa.ExistClassItem)
                    {
                        rsa.Add();
                    }
                }
            }
            DGVRefresh();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadMenuItem2_Click(object sender, EventArgs e)
        {
            strListRange = clsReportDocSetting.const_ReportSettingAssignment_default.ToString();
            this.ReportDocSetting = null;
            this.ReportDocSettingAss = new clsReportDocSettingAssignment();
            this.ReportDocSettingAss.InitClass(this.GLUser, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system);
            InitDGV(strListRange);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadMenuItem4_Click(object sender, EventArgs e)
        {

            strListRange = clsReportDocSetting.const_ReportSettingAssignment_default.ToString();
            this.ReportDocSetting = null;
            this.ReportDocSettingAss = new clsReportDocSettingAssignment();
            this.ReportDocSettingAss.InitClass(this.GLUser, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system);
            InitDGV(strListRange);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            if (this.dgv.Rows.Count > 0)
            {
                if (e.RowIndex > -1)
                {
                    string ColName = e.Column.Name.ToString();
                    if (e.CellElement.Value != null)
                    {
                        e.CellElement.Image = null;

                        switch (ColName)
                        {
                            case "FileInfo":
                                if ((e.CellElement.RowInfo.Cells["FileExist"] != null) && (e.CellElement.RowInfo.Cells["FileExist"].Value != null))
                                {
                                    int iVal = 0;
                                    int.TryParse(e.CellElement.RowInfo.Cells["FileExist"].Value.ToString(), out iVal);
                                    switch (iVal)
                                    {
                                        case 0:
                                            e.CellElement.Image = Sped4.Properties.Resources.bullet_ball_red_16x16;
                                            break;
                                        case 1:
                                            e.CellElement.Image = Sped4.Properties.Resources.bullet_ball_green_16x16;
                                            break;
                                    }
                                }
                                //e.CellElement.Value = null;
                                break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mibtnDeleteReportData_Click(object sender, EventArgs e)
        {
            string str = string.Empty;
            if (
                (this.ReportDocSettingAss is clsReportDocSettingAssignment) &&
                (this.ReportDocSettingAss.ID > 0) &&
                (this.ReportDocSettingAss.ExistReportData)
              )
            {
                this.ReportDocSettingAss.ReportData = null;
                this.ReportDocSettingAss.UpdateByteData(true);
                InitDGV(strListRange);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbDeleteReport_Click(object sender, EventArgs e)
        {
            if (clsMessages.DeleteAllgemein())
            {
                this.ReportDocSettingAss.ReportData = null;
                this.ReportDocSettingAss.UpdateByteData(true);
                InitDGV(strListRange);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbExportReport_MouseClick(object sender, MouseEventArgs e)
        {
            if (
                    (this.ReportDocSettingAss is clsReportDocSettingAssignment) &&
                    (this.ReportDocSettingAss.ID > 0) &&
                    (this.ReportDocSettingAss.ExistReportData)
               )
            {

                FolderBrowser.Description = "Export - Pfad";
                FolderBrowser.SelectedPath = helper_MandantenReportPath.GetPath(this._ctrMenu._frmMain.system.Mandant);
                helper_IOFile.CheckPath(FolderBrowser.SelectedPath);
                string ExportReportPath = string.Empty;

                DialogResult objResult = FolderBrowser.ShowDialog(this);
                if (objResult == DialogResult.OK)
                    ExportReportPath = FolderBrowser.SelectedPath + "\\";
                helper_IOFile.CheckPath(ExportReportPath);

                string strFilePathTemp = System.IO.Path.Combine(ExportReportPath, this.ReportDocSettingAss.ReportFileName);
                helper_Image.SaveByteArrayToFileWithStaticMethod(this.ReportDocSettingAss.ReportData, strFilePathTemp);

                string strMes = string.Empty;
                string strInfo = string.Empty;
                if (File.Exists(strFilePathTemp))
                {
                    strInfo = "INFORMATION";
                    strMes = "Der Report wurde exportiert!";
                }
                else
                {
                    strInfo = "FEHLER";
                    strMes = "Bei dem Export ist ein Fehler aufgetreten!";
                }
                MessageBox.Show(strMes, strInfo);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbImportReport_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (
                    (this.ReportDocSettingAss is clsReportDocSettingAssignment) &&
                    (this.ReportDocSettingAss.ID > 0) &&
                    (this.ReportDocSettingAss.ExistReportData)
               )
            {
                string ExportReportPath = string.Empty;
                string strFilePathTemp = string.Empty;
                string strMes = string.Empty;
                string strInfo = string.Empty;
                try
                {
                    FileDialog.Title = "Suche Reportdatei";
                    FileDialog.Filter = "Report|*.trdx; *.trbp";
                    FileDialog.InitialDirectory = helper_MandantenReportPath.GetPath(this._ctrMenu._frmMain.system.Mandant);
                    FileDialog.FileName = string.Empty;
                    DialogResult result = FileDialog.ShowDialog();

                    if (result == DialogResult.OK) // Test result.
                    {
                        strFilePathTemp = FileDialog.FileName;
                        if (File.Exists(strFilePathTemp))
                        {
                            this.ReportDocSettingAss.UpdateByteData(true);

                            string strPath = helper_CheckAndGetPathWithoutStartupPath.GetPathValueRange(this.ReportDocSettingAss.StartupPath, strFilePathTemp);
                            string strFileName = Path.GetFileName(strFilePathTemp);
                            this.ReportDocSettingAss.Path = strPath;
                            this.ReportDocSettingAss.ReportFileName = strFileName;
                            this.ReportDocSettingAss.FileExtension = Path.GetExtension(strFilePathTemp);
                            this.ReportDocSettingAss.ReportData = null;
                            this.ReportDocSettingAss.Update();
                        }
                    }
                }
                catch (Exception ex)
                {
                    strMes = ex.Message;
                }
                if (File.Exists(strFilePathTemp))
                {
                    strInfo = "INFORMATION";
                    strMes = "Der Report wurde importiert!";
                }
                else
                {
                    strInfo = "FEHLER";
                    strMes = "Bei dem Imort ist ein Fehler aufgetreten!";
                }
                MessageBox.Show(strMes, strInfo);
                RefreshRepDocSetAssAll();
            }
        }


    }
}
