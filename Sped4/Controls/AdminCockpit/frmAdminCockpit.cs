using LVS;
using Sped4.Controls.ASNCenter;
using Sped4.Controls.Edifact;
using Sped4.Controls.Processes;
using Sped4.Controls.ToDo;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sped4.Controls.AdminCockpit
{
    public partial class frmAdminCockpit : rfrmTmp
    {
        public ctrMenu _ctrMenu;
        internal const string const_BackStageTabName_ReportSettings = "btabReportSettings";

        internal const string const_pageViewPageName_Report = "pvpReport";
        internal const string const_pageViewPageName_ASN = "pvpASN";
        internal const string const_pageViewPageName_CronJobs = "pvpCronJobs";
        internal const string const_pageViewPageName_EDIFACT = "pvpEDIFACT";
        internal const string const_pageViewPageName_CustomProcesses = "pvpCustomProcesses";
        internal const string const_pageViewPageName_EdiBaseData = "pvpEdiBaseData";
        internal const string const_pageViewPageName_ToDo = "pvpToDo";

        //--- tabReport

        internal const string const_tabReport_PrinterAssignment = "tabPage_PrinterAssignment";
        internal const string const_tabReport_tabPage_ReportSettings = "tabPage_ReportSettings";
        internal const string const_ASNSetting_tabPage_ASNAction = "tabPageASNSetting_ASNAction";
        internal const string const_ASNSetting_tabPage_ArtFieldAssignment = "tabPageASNSetting_ArtFieldAssignment";
        internal const string const_tabCronJob_tabPage_CronJobEdit = "tabPage_CronJobEdit";
        internal const string const_tabCronJob_tabPage_CronJobMailingList = "tabPage_CronJobMailingList";

        //EDIFACT
        internal const string const_Edifact_tabPage_AsnArt = "tabPage_AsnArt";
        internal const string const_Edifact_tabPage_EdiAdrWorkspaceAssignment = "tabPage_EdiAdrWorkspaceAssignment";
        internal const string const_Edifact_tabPage_CreateEdiStructure = "tabPage_CreateEdiStruckture";

        //CustomProcesses
        internal const string const_Processes_customProcess = "tabPage_CustomProcess";
        internal const string const_Processes_customProcessException = "tabPage_CustomProcessException";

        //ToDo
        internal const string const_ToDo_tabPage_AnonymousDatatable = "tabPage_AnonymousDatatable";
        internal const string const_ToDo_tabPage_PdfCombination = "tabPage_PdfCombination";
        internal const string const_ToDo_tabPage_CleanAsn = "tabPage_CleanAsn";

        ///<summary>frmAdminCockpit/ InitBackViewPageReportSettings</summary>
        ///<remarks>Init beider Tabs in Report Setting</remarks>
        public frmAdminCockpit(ctrMenu myMenu)
        {
            InitializeComponent();
            this._ctrMenu = myMenu;
        }
        ///<summary>frmAdminCockpit/ frmAdminCockpit_cs_Load</summary>
        ///<remarks>Init beider Tabs in Report Setting</remarks>

        private void frmAdminCockpit_cs_Load(object sender, EventArgs e)
        {
            this.pageViewAdminCockpit.SelectedPage = pvpReport;

            //erstmal wieder rausnehmen
            this.tab_CronJob.TabPages.Remove(tabPage_CronJobMailingList);

            this.Text = this.Text + " | Arbeitsbereich: [" + this._ctrMenu._frmMain.system.AbBereich.ID.ToString() + "] - " + this._ctrMenu._frmMain.system.AbBereich.ABName;
            this.tbArbeitsbereich.Text = "[" + this._ctrMenu._frmMain.system.AbBereich.ID.ToString() + "] - " + this._ctrMenu._frmMain.system.AbBereich.ABName;
            FillComboRefArbeitsbereich();
        }


        ///<summary>frmAdminCockpit/ InitBackViewPageReportSettings</summary>
        ///<remarks>Init beider Tabs in Report Setting</remarks>
        private void pageViewAdminCockpit_SelectedPageChanged(object sender, EventArgs e)
        {
            switch (pageViewAdminCockpit.SelectedPage.Name)
            {
                case const_pageViewPageName_Report:

                    break;

                case const_pageViewPageName_ASN:
                    OpenCtrASNCenterHead();
                    break;

                case const_pageViewPageName_CronJobs:
                    OpenCtrCronJob();
                    break;

                case const_pageViewPageName_EDIFACT:
                    tabEdifactMain.SelectedTab = tabPage_AsnArt;
                    OpenCtrAsnArt();
                    break;
                case const_pageViewPageName_CustomProcesses:
                    //tabProcesses.SelectedTab = 
                    OpenCtrCustomProcess();
                    break;
                case const_pageViewPageName_ToDo:
                    tab_ToDo.SelectedTab = tabPage_AnonymousDatatable;
                    OptenCtrAnonymousDatabase();
                    break;

            }
        }

        /// <summary>
        ///            
        /// </summary>
        private void OpenCtrCustomProcess()
        {
            ctrCustomProcess _CustomProcess = new ctrCustomProcess(this._ctrMenu);
            _CustomProcess.Parent = this.tabPage_CustomProcess;
            _CustomProcess.Dock = DockStyle.Fill;
            _CustomProcess.InitCtr();
            _CustomProcess.Show();
            _CustomProcess.BringToFront();

        }
        /// <summary>
        ///            
        /// </summary>
        private void OpenCtrCustomProcessExeption()
        {
            ctrCustomProcessExcesption _CustomProcessExeption = new ctrCustomProcessExcesption(this._ctrMenu);
            _CustomProcessExeption.Parent = this.tabPage_CustomProcessException;
            _CustomProcessExeption.Dock = DockStyle.Fill;
            //_CustomProcessExeption.InitCtr();
            _CustomProcessExeption.Show();
            _CustomProcessExeption.BringToFront();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabProcesses_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabProcesses.SelectedTab.Name)
            {
                case const_Processes_customProcess:
                    OpenCtrCustomProcess();
                    break;
                case const_Processes_customProcessException:
                    OpenCtrCustomProcessExeption();
                    break;
            }
        }
        private void tabEdifactMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabEdifactMain.SelectedTab.Name)
            {
                case frmAdminCockpit.const_Edifact_tabPage_AsnArt:
                    OpenCtrAsnArt();
                    break;
                case frmAdminCockpit.const_Edifact_tabPage_EdiAdrWorkspaceAssignment:
                    OpenCtrEdiAdrWorkspaceAssignment();
                    break;
                case frmAdminCockpit.const_Edifact_tabPage_CreateEdiStructure:
                    OpenCtrCreateEdiStruckture();
                    break;
            }
        }
        ///<summary>frmAdminCockpit/ tabReport_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void tabReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabReport.SelectedTab.Name)
            {
                case const_tabReport_tabPage_ReportSettings:
                    OpenCtrReportSettings();
                    break;
                case const_tabReport_PrinterAssignment:
                    OpenCtrPrinter();
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tab_CronJob_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tab_CronJob.SelectedTab.Name)
            {
                case const_tabCronJob_tabPage_CronJobEdit:
                    OpenCtrCronJob();
                    break;
                case const_tabCronJob_tabPage_CronJobMailingList:
                    OpenCtrComMailingList();
                    break;
            }
        }

        ///<summary>frmAdminCockpit/ OpenCtrPrinter</summary>
        ///<remarks></remarks>
        private void OpenCtrPrinter()
        {
            ctrPrinter _ctrPrinter = new ctrPrinter(this._ctrMenu);
            _ctrPrinter.Parent = this.tabPage_PrinterAssignment;
            _ctrPrinter.IsUsedByAdminCockpit = true;
            _ctrPrinter.Dock = DockStyle.Fill;
            _ctrPrinter.Show();
            _ctrPrinter.BringToFront();
        }
        ///<summary>rfrmAdminCockpit/ OpenCtrReportSettings</summary>
        ///<remarks></remarks>
        private void OpenCtrReportSettings()
        {
            ctrReportSetting _ctrRepSetting = new ctrReportSetting(this._ctrMenu);
            _ctrRepSetting.Parent = this.tabPage_ReportSettings;
            _ctrRepSetting.Dock = DockStyle.Fill;
            _ctrRepSetting.Show();
            _ctrRepSetting.BringToFront();
        }
        /// <summary>
        ///           Cronjob
        /// </summary>
        private void OpenCtrCronJob()
        {
            ctrCronJobs _ctrCronJob = new ctrCronJobs(this._ctrMenu);
            _ctrCronJob.Parent = this.tabPage_CronJobEdit;
            _ctrCronJob.Dock = DockStyle.Fill;
            _ctrCronJob.Show();
            _ctrCronJob.BringToFront();
        }
        /// <summary>
        ///             ctrComMailingList -> Mailverteiler
        /// </summary>
        private void OpenCtrComMailingList()
        {
            ctrComMailingLists _ctrComMailingLists = new ctrComMailingLists(this._ctrMenu);
            _ctrComMailingLists.Parent = this.tabPage_CronJobMailingList;
            _ctrComMailingLists.Dock = DockStyle.Fill;
            _ctrComMailingLists.Show();
            _ctrComMailingLists.BringToFront();
        }
        /// <summary>
        ///            
        /// </summary>
        private void OpenCtrAsnArt()
        {
            ctrAsnArt _asnArt = new ctrAsnArt();
            _asnArt.Parent = this.tabPage_AsnArt;
            _asnArt.Dock = DockStyle.Fill;
            _asnArt.InitCtr();
            _asnArt.Show();
            _asnArt.BringToFront();

        }
        /// <summary>
        /// 
        /// </summary>
        private void OpenCtrEdiAdrWorkspaceAssignment()
        {
            ctrEdiClientWorkspaceValue _ediAdrWorkspaceAssignment = new ctrEdiClientWorkspaceValue();
            _ediAdrWorkspaceAssignment.Parent = this.tabPage_EdiAdrWorkspaceAssignment;
            _ediAdrWorkspaceAssignment.Dock = DockStyle.Fill;
            _ediAdrWorkspaceAssignment._ctrMenu = this._ctrMenu;
            _ediAdrWorkspaceAssignment.InitCtr();
            _ediAdrWorkspaceAssignment.Show();
            _ediAdrWorkspaceAssignment.BringToFront();
        }
        /// <summary>
        /// 
        /// </summary>
        private void OpenCtrCreateEdiStruckture()
        {
            ctrCreateEdiStruckture _createEdiStruckture = new ctrCreateEdiStruckture();
            _createEdiStruckture.Parent = this.tabPage_CreateEdiStruckture;
            _createEdiStruckture.Dock = DockStyle.Fill;
            _createEdiStruckture._ctrMenu = this._ctrMenu;
            _createEdiStruckture.InitCtr();
            _createEdiStruckture.Show();
            _createEdiStruckture.BringToFront();
        }
        /// <summary>
        /// 
        /// </summary>
        //private void Open123()
        //{
        //    ctrEdiAdrWorkspaceAssignment _ediAdrWorkspaceAssignment = new ctrEdiAdrWorkspaceAssignment();
        //    _ediAdrWorkspaceAssignment.Parent = this.tabPage_EdiAdrWorkspaceAssignment;
        //    _ediAdrWorkspaceAssignment.Dock = DockStyle.Fill;
        //    _ediAdrWorkspaceAssignment._ctrMenu = this._ctrMenu;
        //    _ediAdrWorkspaceAssignment.InitCtr();
        //    _ediAdrWorkspaceAssignment.Show();
        //    _ediAdrWorkspaceAssignment.BringToFront();
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnINITReportDocSetting_Click(object sender, EventArgs e)
        {
            clsReportDocSetting repDocSet = new clsReportDocSetting();
            repDocSet.InitClass(this._ctrMenu._frmMain.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, 0, this._ctrMenu._frmMain.system.AbBereich.ID);
            repDocSet.InitFillTable();
        }
        ///<summary>rfrmAdminCockpit/ btnINITReportDocSettingAssignment_Click</summary>
        ///<remarks></remarks>
        private void btnINITReportDocSettingAssignment_Click(object sender, EventArgs e)
        {
            int iRefABId = 0;
            if (comboRefArbeitsbereich.SelectedIndex > -1)
            {
                int.TryParse(comboRefArbeitsbereich.SelectedValue.ToString(), out iRefABId);
            }

            clsReportDocSettingAssignment repDocSetAss = new clsReportDocSettingAssignment();
            repDocSetAss.InitClass(this._ctrMenu._frmMain.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system);
            repDocSetAss.InitFillTable(iRefABId);

        }
        ///<summary>rfrmAdminCockpit/ tbtnMailCheck_Click</summary>
        ///<remarks></remarks>
        private void tbtnMailCheck_Click(object sender, EventArgs e)
        {
            string strError = string.Empty;
            tbMailCheckInfo.Text = string.Empty;
            clsMail MailCheck = new clsMail();
            MailCheck.InitClass(this._ctrMenu._frmMain.GL_User, this._ctrMenu._frmMain.system);
            MailCheck.SMTPServer = tbSMTPServer.Text.Trim();
            MailCheck.SMTPUser = tbSMTPUser.Text.Trim();
            MailCheck.SMTPPasswort = tbSMTPPass.Text.Trim();
            MailCheck.MailFrom = tbMailAdress.Text.Trim();
            //MailCheck.ListMailReceiver.Add("lvsreport@comtec-noeker.de");

            MailCheck.ListMailReceiver.Add("support@softkonzept.com");

            Int32 iTmp = 0;
            Int32.TryParse(tbSMTPPort.Text.Trim(), out iTmp);
            MailCheck.SMTPPort = iTmp;
            MailCheck.SMTPSsl = cbSMTPSSL.Checked;
            MailCheck.Subject = "Check Mailaccount: " + tbMailAdress.Text.Trim();
            strError = strError + "E-Mailcheck gestartet! " + Environment.NewLine;
            MailCheck.Message = string.Empty;
            if (MailCheck.Send())
            {
                strError = strError + "Testmail wurde erfolgreich versandt!!! " + Environment.NewLine;
            }
            else
            {
                strError = strError + "Testmail konnte NICHT versendet werden - Fehlermeldung: " + Environment.NewLine;
                strError = strError + MailCheck.Message + Environment.NewLine;
            }
            tbMailCheckInfo.Text = strError;
        }
        ///<summary>frmAdminCockpit/ OpenCtrPrinter</summary>
        ///<remarks></remarks>
        private void OpenCtrAC_ASNAktion()
        {
            //ctrAC_ASNAction _ASNAktion = new ctrAC_ASNAction(this._ctrMenu);
            //_ASNAktion.Parent = this.tabPageASNSetting_ASNAction;
            //_ASNAktion.Dock = DockStyle.Fill;
            //_ASNAktion.Show();
            //_ASNAktion.BringToFront();
        }
        ///<summary>frmAdminCockpit/ pageViewASNSetting_SelectedPageChanged</summary>
        ///<remarks></remarks>
        private void pageViewASNSetting_SelectedPageChanged(object sender, EventArgs e)
        {
            //switch (pageViewASNSetting.SelectedPage.Name)
            //{
            //    case const_ASNSetting_tabPage_ASNAction:
            //        OpenCtrAC_ASNAktion();
            //        break;
            //}
        }
        ///<summary>rfrmAdminCockpit/ OpenCtrReportSettings</summary>
        ///<remarks></remarks>
        private void OpenCtrASNCenterHead()
        {
            ctrASNMain _ctrASNMain = new ctrASNMain(this._ctrMenu);
            _ctrASNMain.Parent = this.pvpASN;
            _ctrASNMain.Dock = DockStyle.Fill;
            _ctrASNMain.Show();
            _ctrASNMain.BringToFront();
        }
        /// <summary>
        ///             beinhaltet die Arbeitsbereiche:
        ///             - nicht den aktuellen
        ///             - wo mindestens 1 Report hinterlegt ist
        ///             
        /// </summary>
        private void FillComboRefArbeitsbereich()
        {
            DataTable dtSourceCombo = clsArbeitsbereiche.GetArbeitsbereichForInitReports(this._ctrMenu.GL_User, (int)this._ctrMenu._frmMain.system.AbBereich.ID);
            comboRefArbeitsbereich.DataSource = dtSourceCombo;
            comboRefArbeitsbereich.ValueMember = "ID";
            comboRefArbeitsbereich.DisplayMember = "Name";
        }

        private void comboRefArbeitsbereich_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SetTbArbeitsbereich();
        }

        private void SetTbArbeitsbereich()
        {
            //this.tbArbeitsbereich.Text = "[" + this._ctrMenu._frmMain.system.AbBereich.ID.ToString() + "] - " + this._ctrMenu._frmMain.system.AbBereich.ABName;
            //this.tbArbeitsbereich.Text = "[" + comboRefArbeitsbereich.SelectedValue.ToString() + "] - " + comboRefArbeitsbereich.SelectedItem.ToString();
        }

        //----------------------------------------------------------------------------------------------------------------- Todo
        /// <summary>
        ///                 Clear form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnClear_Click(object sender, EventArgs e)
        {
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tab_ToDo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tab_ToDo.SelectedTab.Name)
            {
                case frmAdminCockpit.const_ToDo_tabPage_AnonymousDatatable:
                    OptenCtrAnonymousDatabase();
                    break;
                case frmAdminCockpit.const_ToDo_tabPage_PdfCombination:
                    OptenCtrTest();
                    break;
                case frmAdminCockpit.const_ToDo_tabPage_CleanAsn:
                    OptenCtrCleanAsn();
                    break;

            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void OptenCtrAnonymousDatabase()
        {
            ctrAnonymousDatabase _anonymousDatabase = new ctrAnonymousDatabase(this._ctrMenu);
            _anonymousDatabase.Parent = tabPage_AnonymousDatatable;
            _anonymousDatabase.Dock = DockStyle.Fill;
            _anonymousDatabase.Show();
            _anonymousDatabase.BringToFront();
        }
        /// <summary>
        /// 
        /// </summary>
        private void OptenCtrTest()
        {
            ctrPdfCombinationTest _TestCtr = new ctrPdfCombinationTest(this._ctrMenu);
            _TestCtr.Parent = tabPage_PdfCombination;
            _TestCtr.Dock = DockStyle.Fill;
            _TestCtr.Show();
            _TestCtr.BringToFront();
        }
        /// <summary>
        /// 
        /// </summary>
        private void OptenCtrCleanAsn()
        {
            ctrCleanAsnTables _Cleantr = new ctrCleanAsnTables(this._ctrMenu);
            _Cleantr.Parent = tabPage_CleanAsn;
            _Cleantr.Dock = DockStyle.Fill;
            _Cleantr.Show();
            _Cleantr.BringToFront();
        }

    }
}
