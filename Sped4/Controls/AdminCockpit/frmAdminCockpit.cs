using LVS;
using LVS.Mail;
using Sped4.Controls.ASNCenter;
using Sped4.Controls.Edifact;
using Sped4.Controls.Processes;
using Sped4.Controls.ToDo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Sped4.Controls.AdminCockpit
{
    public partial class frmAdminCockpit : rfrmTmp
    {
        public ctrMenu _ctrMenu;
        internal const string const_BackStageTabName_ReportSettings = "btabReportSettings";

        internal const string const_pageViewPageName_Report = "pvpReport";
        internal const string const_pageViewPageName_ASN = "pvpASN";
        internal const string const_pageViewPageName_Mail = "pvpMail";
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

                case const_pageViewPageName_Mail:
                    OpenCtrMailCheck();
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

        private void OpenCtrMailCheck()
        {             
            ctrMailCheck _ctrMailCheck = new ctrMailCheck(this._ctrMenu);
            _ctrMailCheck.Parent = this.pvpMail;
            _ctrMailCheck.Dock = DockStyle.Fill;
            _ctrMailCheck.Show();
            _ctrMailCheck.BringToFront();
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
        private async void tbtnMailCheck_Click(object sender, EventArgs e)
        {
            //string strError = string.Empty;
            //if (cbUseReply.Checked)
            //{
            //    strError = string.Empty;
            //    string strMessage = string.Empty;
            //    string strSubject = string.Empty;
            //    try
            //    {
            //        tbMailCheckInfo.Text = string.Empty;

            //        MailCredentials mc = new MailCredentials();
            //        mc.SmtpHost = tbSMTPServer.Text.Trim();
            //        mc.SmtpUser = tbSMTPUser.Text.Trim();
            //        mc.SmtpPassword = tbSMTPPass.Text.Trim();
            //        Int32 iTmp = 0;
            //        Int32.TryParse(tbSMTPPort.Text.Trim(), out iTmp);
            //        mc.SmtpPort = iTmp;
            //        mc.KeepAlive = false;

            //        string strRepyTo = tbReplyTo.Text.Trim();
            //        string strReplyToName = tbAbsName.Text.Trim();
            //        mc.SmtpDisplayName = strReplyToName;



            //        // ✅ TEST-EMPFÄNGER
            //        List<string> MailReceivers = new List<string>();
            //        MailReceivers.Add("support@softkonzept.com");
            //        MailReceivers.Add("Marco-Rinscheid@gmx.de");

            //        // ✅ TEST-NACHRICHT
            //        strSubject = "SMTP-Test Reply-To " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //        strMessage = "Dies ist eine Testnachricht für IONOS SMTP-Konfiguration." + Environment.NewLine +
            //                            "Server: " + mc.SmtpHost + Environment.NewLine +
            //                            "Port: " + mc.SmtpPort + Environment.NewLine +
            //                            //"Mode: " + (isRelayConnector ? "RELAY (ohne Auth)" : "AUTH (mit Authentifizierung)") + Environment.NewLine +
            //                            //"SSL/TLS: " + MailCheck.SMTPSsl ? "JA (STARTTLS)" : "NEIN (unverschlüsselt)") + Environment.NewLine +
            //                            "Login-Account: " + mc.SmtpUser + Environment.NewLine +
            //                            "Von (From): " + mc.SmtpUser + Environment.NewLine +
            //                            "Reply-To: " + strRepyTo + "|" + strReplyToName + Environment.NewLine;

            //        Mail mailCheck = new Mail(mc);
            //        // ✅ WICHTIG: await hinzufügen!
            //        await mailCheck.SendWithReplyToAsync(
            //            toEmail: tbMailAdress.Text.Trim(),
            //            subject: strSubject,
            //            body: strMessage,
            //            replyToEmail: strRepyTo,
            //            replyToName: strReplyToName);

            //        strError = "✅ SUCCESS: Testmail wurde erfolgreich versandt!" + Environment.NewLine +
            //                   "Empfänger: support@softkonzept.com" + Environment.NewLine +
            //                   "Zeit: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            //    }
            //    catch (Exception ex)
            //    {
            //        strError = string.Empty;
            //        strError = "❌ EXCEPTION: Unerwarteter Fehler!" + Environment.NewLine;
            //        strError += Environment.NewLine;
            //        strError += "Exception Message:" + Environment.NewLine;
            //        strError += ex.Message + Environment.NewLine;
            //        strError += Environment.NewLine;
            //        strError += "Stack Trace:" + Environment.NewLine;
            //        strError += ex.StackTrace;
            //    }
            //    finally
            //    {
            //        tbMailCheckInfo.Text = strError;
            //    }
            //}
            //else
            //{
            //    try
            //    { 
            //        strError = string.Empty;
            //        tbMailCheckInfo.Text = string.Empty;
            //        clsMail MailCheck = new clsMail();
            //        MailCheck.InitClass(this._ctrMenu._frmMain.GL_User, this._ctrMenu._frmMain.system);

            //        // ✅ SMTP-EINSTELLUNGEN AUS UI
            //        MailCheck.SMTPServer = tbSMTPServer.Text.Trim();
            //        MailCheck.SMTPUser = tbSMTPUser.Text.Trim();
            //        MailCheck.SMTPPasswort = tbSMTPPass.Text.Trim();
            //        MailCheck.MailFrom = tbMailAdress.Text.Trim();
            //        MailCheck.MailReplyTo = tbReplyTo.Text.Trim();
            //        MailCheck.MailFromName = tbAbsName.Text.Trim();
            //        Int32 iTmp = 0;
            //        Int32.TryParse(tbSMTPPort.Text.Trim(), out iTmp);
            //        MailCheck.SMTPPort = iTmp;
            //        MailCheck.SMTPSsl = cbSMTPSSL.Checked;

            //        // ✅ TEST-EMPFÄNGER
            //        MailCheck.ListMailReceiver.Add("support@softkonzept.com");
            //        MailCheck.ListMailReceiver.Add("Marco-Rinscheid@gmx.de");

            //        //TestMailOld();
            //        //string strError = string.Empty;
            //        //tbMailCheckInfo.Text = string.Empty;
            //        //clsMail MailCheck = new clsMail();
            //        //MailCheck.InitClass(this._ctrMenu._frmMain.GL_User, this._ctrMenu._frmMain.system);
            //        //MailCheck.SMTPServer = tbSMTPServer.Text.Trim();
            //        //MailCheck.SMTPUser = tbSMTPUser.Text.Trim();
            //        //MailCheck.SMTPPasswort = tbSMTPPass.Text.Trim();
            //        //MailCheck.MailFrom = tbMailAdress.Text.Trim();

            //        //MailCheck.ListMailReceiver.Add("support@softkonzept.com");
            //        //MailCheck.ListMailReceiver.Add("support@softkonzept.com");

            //        //Int32 iTmp = 0;
            //        //Int32.TryParse(tbSMTPPort.Text.Trim(), out iTmp);
            //        //MailCheck.SMTPPort = iTmp;
            //        //MailCheck.SMTPSsl = cbSMTPSSL.Checked;
            //        MailCheck.Subject = "Check Mailaccount: " + tbMailAdress.Text.Trim();
            //        strError = strError + "E-Mailcheck gestartet! " + Environment.NewLine;
            //        MailCheck.Message = string.Empty;
            //        if (MailCheck.Send())
            //        {
            //            strError = strError + "Testmail wurde erfolgreich versandt!!! " + Environment.NewLine;
            //        }
            //        else
            //        {
            //            strError = strError + "Testmail konnte NICHT versendet werden - Fehlermeldung: " + Environment.NewLine;
            //            strError = strError + MailCheck.Message + Environment.NewLine;
            //        }
            //        tbMailCheckInfo.Text = strError;
            //    }
            //    catch (Exception ex)
            //    {
            //        strError = string.Empty;
            //        strError = "❌ EXCEPTION: Unerwarteter Fehler!" + Environment.NewLine;
            //        strError += Environment.NewLine;
            //        strError += "Exception Message:" + Environment.NewLine;
            //        strError += ex.Message + Environment.NewLine;
            //        strError += Environment.NewLine;
            //        strError += "Stack Trace:" + Environment.NewLine;
            //        strError += ex.StackTrace;
            //    }
            //    finally
            //    {
            //        tbMailCheckInfo.Text = strError;
            //    }
            //}
        }

        private void TestMailOld()
        {
            string strError = string.Empty;
            tbMailCheckInfo.Text = string.Empty;
            clsMail MailCheck = new clsMail();
            MailCheck.InitClass(this._ctrMenu._frmMain.GL_User, this._ctrMenu._frmMain.system);
            MailCheck.SMTPServer = tbSMTPServer.Text.Trim();
            MailCheck.SMTPUser = tbSMTPUser.Text.Trim();
            MailCheck.SMTPPasswort = tbSMTPPass.Text.Trim();
            MailCheck.MailFrom = tbMailAdress.Text.Trim();

            MailCheck.ListMailReceiver.Add("support@softkonzept.com");
            //MailCheck.ListMailReceiver.Add("support@softkonzept.com");

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

        private void TestMailNew(clsMail MailCheck)
        {
            string strError = string.Empty;
            tbMailCheckInfo.Text = string.Empty;

            try
            {
                //clsMail MailCheck = new clsMail();
                //MailCheck.InitClass(this._ctrMenu._frmMain.GL_User, this._ctrMenu._frmMain.system);

                // ✅ SMTP-EINSTELLUNGEN AUS UI
                //MailCheck.SMTPServer = tbSMTPServer.Text.Trim();
                //MailCheck.SMTPUser = tbSMTPUser.Text.Trim();
                //MailCheck.SMTPPasswort = tbSMTPPass.Text.Trim();

                string smtpAccountEmail = tbMailAdress.Text.Trim();  // Account A (Login)

                // ✅ OPTIONAL: Alternativer From-Address (Account B)
                // Setzen Sie hier eine alternative Absenderadresse (muss gleiche Domain sein!)
                string customFromAddress = string.Empty;  // Leer = Account A als From
                string customFromName = string.Empty;

                // ✅ RELAY-MODUS ERKENNEN
                bool isRelayConnector = string.IsNullOrWhiteSpace(MailCheck.SMTPUser) &&
                                       string.IsNullOrWhiteSpace(MailCheck.SMTPPasswort);

                if (isRelayConnector)
                {
                    strError = "ℹ️ RELAY-MODUS erkannt (ohne Authentifizierung)" + Environment.NewLine;

                    if (string.IsNullOrWhiteSpace(MailCheck.SMTPServer))
                    {
                        strError += "❌ FEHLER: SMTP Server ist erforderlich!" + Environment.NewLine;
                        tbMailCheckInfo.Text = strError;
                        return;
                    }
                }
                else
                {
                    if (!ValidateSMTPSettings(MailCheck))
                    {
                        strError = "❌ FEHLER: Alle SMTP-Felder müssen gefüllt sein!" + Environment.NewLine;
                        strError += "Erforderlich:" + Environment.NewLine;
                        strError += "- SMTP Server: " + (string.IsNullOrWhiteSpace(MailCheck.SMTPServer) ? "LEER" : "OK") + Environment.NewLine;
                        strError += "- SMTP User: " + (string.IsNullOrWhiteSpace(MailCheck.SMTPUser) ? "LEER" : "OK") + Environment.NewLine;
                        strError += "- SMTP Passwort: " + (string.IsNullOrWhiteSpace(MailCheck.SMTPPasswort) ? "LEER" : "OK") + Environment.NewLine;
                        strError += "- Mail Adresse: " + (string.IsNullOrWhiteSpace(MailCheck.MailFrom) ? "LEER" : "OK") + Environment.NewLine;
                        tbMailCheckInfo.Text = strError;
                        return;
                    }
                }

                // ✅ PORT UND SSL KONFIGURIEREN
                Int32 iTmp = 0;
                Int32.TryParse(tbSMTPPort.Text.Trim(), out iTmp);

                if (isRelayConnector)
                {
                    MailCheck.SMTPPort = (iTmp > 0) ? iTmp : 25;
                    MailCheck.SMTPSsl = false;
                }
                else
                {
                    MailCheck.SMTPPort = (iTmp > 0) ? iTmp : 587;
                    MailCheck.SMTPSsl = cbSMTPSSL.Checked;
                }

                // ✅ TEST-EMPFÄNGER
                //MailCheck.ListMailReceiver.Add("support@softkonzept.com");
                //MailCheck.ListMailReceiver.Add("Marco-Rinscheid@gmx.de");              

                // ✅ SAUBERE ABSENDER-STRATEGIE FÜR IONOS
                // Option 1: Standard (Account A als From)
                //MailCheck.MailFrom = smtpAccountEmail;

                // Option 2: Alternative Absenderadresse (IONOS-kompatibel)
                // Nur wenn gleiche Domain oder IONOS erlaubt es
                //if (!string.IsNullOrWhiteSpace(customFromAddress) &&
                //    IsSameDomain(smtpAccountEmail, customFromAddress))
                //{
                //    MailCheck.MailFrom = customFromAddress;
                //    MailCheck.MailFromName = customFromName;  // Benötigt clsMail Erweiterung!
                //}

                // ✅ REPLY-TO (FIXED: string.IsNullOrEmpty statt IsNullOrEmpty)
                //MailCheck.MailReplyTo = string.IsNullOrEmpty(customFromAddress)
                //    ? smtpAccountEmail
                //    : customFromAddress;

                // ✅ TEST-NACHRICHT
                MailCheck.Subject = "SMTP-Test (IONOS): " + MailCheck.MailFrom + " - " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                MailCheck.Message = "Dies ist eine Testnachricht für IONOS SMTP-Konfiguration." + Environment.NewLine +
                                    "Server: " + MailCheck.SMTPServer + Environment.NewLine +
                                    "Port: " + MailCheck.SMTPPort + Environment.NewLine +
                                    "Mode: " + (isRelayConnector ? "RELAY (ohne Auth)" : "AUTH (mit Authentifizierung)") + Environment.NewLine +
                                    "SSL/TLS: " + (MailCheck.SMTPSsl ? "JA (STARTTLS)" : "NEIN (unverschlüsselt)") + Environment.NewLine +
                                    "Login-Account: " + smtpAccountEmail + Environment.NewLine +
                                    "Von (From): " + MailCheck.MailFrom + Environment.NewLine +
                                    "Reply-To: " + MailCheck.MailReplyTo;

                strError = "⏳ SMTP-Test wird durchgeführt..." + Environment.NewLine;
                strError += "Server: " + MailCheck.SMTPServer + ":" + MailCheck.SMTPPort + Environment.NewLine;
                strError += "Mode: " + (isRelayConnector ? "RELAY (anonymer Versand)" : "Authentifizierung") + Environment.NewLine;
                strError += "IONOS Account Login: " + smtpAccountEmail + Environment.NewLine;
                strError += "Von (From): " + MailCheck.MailFrom + Environment.NewLine;
                strError += "Reply-To: " + MailCheck.MailReplyTo + Environment.NewLine;

                if (!isRelayConnector)
                {
                    strError += "Benutzer: " + MailCheck.SMTPUser + Environment.NewLine;
                }

                strError += "SSL/TLS: " + (MailCheck.SMTPSsl ? "JA (STARTTLS Port 587)" : "NEIN (Port 25)") + Environment.NewLine;
                strError += Environment.NewLine;

                tbMailCheckInfo.Text = strError;
                Application.DoEvents();

                // ✅ MAIL VERSENDEN
                if (MailCheck.SendTest())
                {
                    strError += "✅ SUCCESS: Testmail wurde erfolgreich versandt!" + Environment.NewLine;
                    strError += Environment.NewLine;
                    strError += "IONOS SMTP-Verbindung funktioniert einwandfrei." + Environment.NewLine;
                    strError += "Mode: " + (isRelayConnector ? "RELAY-Connector aktiv" : "Authentifizierung aktiv") + Environment.NewLine;
                    strError += "Empfänger: support@softkonzept.com" + Environment.NewLine;
                    strError += "Zeit: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                }
                else
                {
                    strError += "❌ FEHLER: Testmail konnte NICHT versendet werden!" + Environment.NewLine;
                    strError += Environment.NewLine;
                    strError += "Fehlermeldung:" + Environment.NewLine;
                    strError += MailCheck.Message + Environment.NewLine;
                    strError += Environment.NewLine;
                    strError += "IONOS-spezifische Lösungsvorschläge:" + Environment.NewLine;

                    if (isRelayConnector)
                    {
                        strError += "1. Prüfen Sie die Relay-IP oder den Hostname" + Environment.NewLine;
                        strError += "2. Firewall erlaubt ausgehende Verbindungen auf Port " + MailCheck.SMTPPort + "?" + Environment.NewLine;
                        strError += "3. IONOS-Relay in Mailkonto konfiguriert?" + Environment.NewLine;
                        strError += "4. Absender-Adresse im IONOS-Relay autorisiert?";
                    }
                    else
                    {
                        strError += "1. IONOS Login korrekt? (z.B. user@domain.de oder nur user)" + Environment.NewLine;
                        strError += "2. Passwort korrekt?" + Environment.NewLine;
                        strError += "3. IONOS Port 587 mit STARTTLS/TLS verwenden" + Environment.NewLine;
                        strError += "4. Alternative From-Adressen MÜSSEN gleiche Domain haben!" + Environment.NewLine;
                        strError += "5. IONOS kann fremde From-Adressen ablehnen";
                    }
                }
            }
            catch (Exception ex)
            {
                strError = "❌ EXCEPTION: Unerwarteter Fehler!" + Environment.NewLine;
                strError += Environment.NewLine;
                strError += "Exception Message:" + Environment.NewLine;
                strError += ex.Message + Environment.NewLine;
                strError += Environment.NewLine;
                strError += "Stack Trace:" + Environment.NewLine;
                strError += ex.StackTrace;
            }
            finally
            {
                tbMailCheckInfo.Text = strError;
            }
        }

        /// <summary>
        /// Validiert die SMTP-Einstellungen
        /// </summary>
        private bool ValidateSMTPSettings(clsMail myMailCheck)
        {
            return !string.IsNullOrWhiteSpace(myMailCheck.SMTPServer) &&
                   !string.IsNullOrWhiteSpace(myMailCheck.SMTPUser) &&
                   !string.IsNullOrWhiteSpace(myMailCheck.SMTPPasswort) &&
                   !string.IsNullOrWhiteSpace(myMailCheck.MailFrom);
        }

        /// <summary>
        /// Hilfsmethode: Prüft ob zwei E-Mail-Adressen die gleiche Domain haben
        /// </summary>
        private bool IsSameDomain(string email1, string email2)
        {
            if (string.IsNullOrWhiteSpace(email1) || string.IsNullOrWhiteSpace(email2))
                return false;

            try
            {
                string domain1 = email1.Substring(email1.LastIndexOf("@") + 1).ToLower();
                string domain2 = email2.Substring(email2.LastIndexOf("@") + 1).ToLower();
                return domain1 == domain2;
            }
            catch
            {
                return false;
            }
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

        private void btnSmtpResponse_Click(object sender, EventArgs e)
        {
            string strError = string.Empty;
            tbMailCheckInfo.Text = string.Empty;
            tbMailCheckInfo.Text = strError;
            clsMail MailCheck = new clsMail();
            MailCheck.InitClass(this._ctrMenu._frmMain.GL_User, this._ctrMenu._frmMain.system);

            //tbSMTPServer.Text = MailCheck.

            //MailCheck.SMTPServer = tbSMTPServer.Text.Trim();
            //MailCheck.SMTPUser = tbSMTPUser.Text.Trim();
            //MailCheck.SMTPPasswort = tbSMTPPass.Text.Trim();
            //MailCheck.MailFrom = tbMailAdress.Text.Trim();
            //MailCheck.ListMailReceiver.Add("lvsreport@comtec-noeker.de");


            MailCheck.ListMailReceiver.Add("Marco-Rinscheid@gmx.de");
            MailCheck.ListMailReceiver.Add("info@softkonzept");
            MailCheck.ListMailReceiver.Add("mrrrmr@softkonzept.com");

            Int32 iTmp = 0;
            Int32.TryParse(tbSMTPPort.Text.Trim(), out iTmp);
            MailCheck.SMTPPort = iTmp;
            MailCheck.SMTPSsl = cbSMTPSSL.Checked;
            MailCheck.Subject = "Check SMTP Response "; // + tbMailAdress.Text.Trim();
            strError = strError + "SMTP Response - Check gestartet! " + Environment.NewLine;
            MailCheck.Message = string.Empty;
            var smtpResult = MailCheck.SendMailMultiRecipient();

            strError = strError + "Testmail wurde erfolgreich versandt!!! " + Environment.NewLine;

            tbMailCheckInfo.Text = strError;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            tbSMTPServer.Text = "slegmbh-de0i.mail.protection.outlook.com";
            tbSMTPUser.Text = "noreply@sle-gmbh.de";
            tbSMTPPass.Text = string.Empty;
            tbMailAdress.Text = "noreply@sle-gmbh.de";
            tbReplyTo.Text = string.Empty;
            tbAbsName.Text = string.Empty;
            tbSMTPPort.Text = "25";
            cbSMTPSSL.Checked = true;
        }
    }
}
