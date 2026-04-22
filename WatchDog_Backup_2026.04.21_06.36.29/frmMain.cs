using LVS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace WatchDog
{
    public partial class frmMain : Form
    {
        public const string const_Prozess_SystemStart = "System-Start";
        public const string const_Prozess_SystemStop = "System-STOP";

        public Globals._GL_SYSTEM GLSystem = new Globals._GL_SYSTEM();
        public Globals._GL_USER GLUser = new Globals._GL_USER();
        internal clsSystem system = new clsSystem();
        internal DateTime dtWDStart;
        internal DateTime dtWDNextStart;
        internal LVS.clsSystem systemLVS = new LVS.clsSystem();

        public delegate void ThreadCtrInvokeEventHandler();
        BackgroundWorker workerStart;


        /// <summary>
        ///             MAIN-Frm
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
            SetMainMenuEnabled(true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_Load(object sender, EventArgs e)
        {
            try
            {
                string str = AppDomain.CurrentDomain.BaseDirectory;

                // initialisieren der DB Daten
                system = new clsSystem();
                //system.VE_IsWatchDog = true;
                system.InitSystem(ref this.GLSystem);  // zahl durch konst

                string strTmp = string.Empty;

                //--- Der WatchDog benötigt keine DB-Verbindung
                strTmp = GetLogString(strTmp, "[WatchDog].[APP] - " + this.Text, false);
                this.systemLVS.listLogToFileSystem.Add(strTmp);

                strTmp = string.Empty;
                strTmp = GetLogString(strTmp, "[WatchDog].[" + const_Prozess_SystemStart + "] - WatchDog wird initialisiert.", false);
                this.systemLVS.listLogToFileSystem.Add(strTmp);
                clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, Application.StartupPath, clsLogbuchCon.const_Task_System);

                InitDGVLogSYS();
            }
            catch (Exception ex)
            {
                clsMessages.Allgemein_ERRORTextShow(ex.ToString());
            }

            if (this.system.VE_Autostart)
            {
                string strTmp = string.Empty;
                strTmp = Functions.GetLogString(strTmp, "[WatchDog].[" + const_Prozess_SystemStart + "].[AUTOSTART] - WatchDog-Prozesse gestartet....", false);
                this.systemLVS.listLogToFileSystem.Add(strTmp);
                clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, Application.StartupPath, clsLogbuchCon.const_Task_System);

                clsWD_SignOfLife SoL = new clsWD_SignOfLife(this.GLUser, this.system, false);
                tWatchDog.Interval = 1000;
                tWatchDog.Start();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bEnabledStart"></param>
        private void SetMainMenuEnabled(bool bEnabledStart)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            SetMainMenuEnabled(bEnabledStart);
                                                                        }
                                                                    )
                                    );
                return;
            }

            this.mbtnExit.Enabled = bEnabledStart;
            this.mbtnStart.Enabled = bEnabledStart;
            this.mbtnStop.Enabled = (!this.mbtnStart.Enabled);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TWatchDog_Tick(object sender, EventArgs e)
        {
            if (workerStart is BackgroundWorker)
            {
                while (workerStart.IsBusy)
                {
                    Thread.Sleep(5000);
                }
            }
            Start_WDProcess();
        }
        /// <summary>
        /// 
        /// </summary>
        private void Start_WDProcess()
        {
            tWatchDog.Interval = this.system.VE_MainThreadDuration;
            SetMainMenuEnabled(false);
            SetSelectedTab(tabPage_LogSYS);

            string strTmp = string.Empty;
            strTmp = Functions.GetLogString(strTmp, "[System].[WatchDog] - Prozessdurchlauf gestartet....", false);
            this.systemLVS.listLogToFileSystem.Add(strTmp);
            clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, Application.StartupPath, clsLogbuchCon.const_Task_System);


            //bDoWork = true;
            workerStart = new BackgroundWorker();
            workerStart.WorkerReportsProgress = true;
            workerStart.WorkerSupportsCancellation = true;
            workerStart.DoWork += new DoWorkEventHandler(workerStart_DoWork);
            workerStart.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerStart_Completed);


            //string strTmp = string.Empty;
            //strTmp = Functions.GetLogString(strTmp, "[System].[WatchDog] - Prozessdurchlauf gestartet....", false);
            //this.systemLVS.listLogToFileSystem.Add(strTmp);
            //clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, Application.StartupPath, clsLogbuchCon.const_Task_System);
            workerStart.RunWorkerAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void workerStart_DoWork(object sender, DoWorkEventArgs e)
        {
            Task_WatchDog();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void workerStart_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            SetSelectedTab(tabPage_WatchDog);
            this.tWatchDog.Start();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="LogString"></param>
        /// <param name="LogAdd"></param>
        /// <param name="bNewLine"></param>
        /// <returns></returns>
        private string GetLogString(string LogString, string LogAdd, bool bNewLine)
        {
            LogString = clsViewLog.GetLogViewDateTimeString() + " - " + LogAdd;
            if (bNewLine)
            {
                LogString = LogString + Environment.NewLine;
            }
            return LogString;
        }
        /// <summary>
        ///             Application close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MbtnExit_Click(object sender, EventArgs e)
        {
            string strTmp = string.Empty;
            strTmp = Functions.GetLogString(strTmp, "[System-STOP] - WatchDog wird manuell geschlossen....", false);
            this.systemLVS.listLogToFileSystem.Add(strTmp);
            clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, Application.StartupPath, clsLogbuchCon.const_Task_System);
            Application.Exit();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MbtnStart_Click(object sender, EventArgs e)
        {
            string strTmp = string.Empty;
            strTmp = Functions.GetLogString(strTmp, "[WatchDog].[" + const_Prozess_SystemStart + "] - WatchDog-Prozesse werden manuell gestartet....", false);
            this.systemLVS.listLogToFileSystem.Add(strTmp);
            clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, Application.StartupPath, clsLogbuchCon.const_Task_System);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MbtnStop_Click(object sender, EventArgs e)
        {
            SetMainMenuEnabled(true);

            string strTmp = string.Empty;
            strTmp = Functions.GetLogString(strTmp, "[WatchDog].[" + const_Prozess_SystemStop + "]  - Prozesse beenden -> bitte warten bis alle TASK-Prozesse beendet sind....", false);
            this.systemLVS.listLogToFileSystem.Add(strTmp);
            clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, Application.StartupPath, clsLogbuchCon.const_Task_System);
            InitDGVLogSYS();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myTabPage"></param>
        private void SetSelectedTab(TabPage myTabPage)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            InitDGVLogSYS();
                                                                        }
                                                                    )
                                    );
                return;
            }
            this.tabViewGrid.SelectedTab = myTabPage;
        }
        /// <summary>
        /// 
        /// </summary>
        private void Task_WatchDog()
        {
            try
            {
                if ((this.system is clsSystem) && (this.system.VE_IsWatchDog))
                {
                    string strProzessName = string.Empty;
                    strProzessName = "[WatchDog]";
                    //try
                    //{
                    this.dtWDStart = DateTime.Now;

                    string strTmp = string.Empty;
                    this.systemLVS.listLogToFileSystem.Clear();

                    //try
                    //{
                    if (this.system.WD_List_WDClient != null)
                    {
                        List<string> ListWDClient = this.system.WD_List_WDClient;
                        foreach (string wdClient in ListWDClient)
                        {
                            strProzessName = "[System].[WatchDog].[" + wdClient + "]";

                            strTmp = string.Empty;
                            strTmp = strProzessName + " --------------------------------------------------------------------------------------";
                            strTmp = Functions.GetLogString(strTmp, strTmp, false);
                            this.systemLVS.listLogToFileSystem.Add(strTmp);
                            clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, Application.StartupPath, clsLogbuchCon.const_Task_System);


                            strTmp = string.Empty;
                            strTmp = strProzessName + " >>> Check WDClient: " + wdClient;
                            strTmp = Functions.GetLogString(strTmp, strTmp, false);
                            this.systemLVS.listLogToFileSystem.Add(strTmp);
                            clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, Application.StartupPath, clsLogbuchCon.const_Task_System);

                            strProzessName += ".[Check File] >>> ";
                            if (!wdClient.Equals(string.Empty))
                            {
                                // letzte Datei per ftp holen
                                clsftp ftp = new clsftp();
                                ftp.WDClientMC = wdClient;
                                ftp.InitClass(null, this.system);
                                if (ftp.CheckConnection())
                                {
                                    strTmp = strProzessName + "Der Aufbau der FTP-Verbindung ist erfolgreich...";
                                    strTmp = Functions.GetLogString(strTmp, strTmp, false);
                                    this.systemLVS.listLogToFileSystem.Add(strTmp);
                                    clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, Application.StartupPath, clsLogbuchCon.const_Task_System);

                                    ftp.Download_WDFiles();

                                    //---Logeintrag
                                    strTmp = strProzessName + ftp.WD_InfoText;
                                    strTmp = Functions.GetLogString(strTmp, strTmp, false);
                                    this.systemLVS.listLogToFileSystem.Add(strTmp);
                                    clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, Application.StartupPath, clsLogbuchCon.const_Task_System);

                                    // Datei prüfen                    
                                    if (File.Exists(ftp.WD_DownloadFilePath))
                                    {
                                        //---Logeintrag
                                        strTmp = strProzessName + "File wird analysiert: " + ftp.WD_DownloadFilePath;
                                        strTmp = Functions.GetLogString(strTmp, strTmp, false);
                                        this.systemLVS.listLogToFileSystem.Add(strTmp);
                                        clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, Application.StartupPath, clsLogbuchCon.const_Task_System);

                                        List<string> ListFileToCheck = new List<string>();

                                        try
                                        {
                                            string line = string.Empty;
                                            using (StreamReader reader = new StreamReader(ftp.WD_DownloadFilePath))
                                            {
                                                while ((line = reader.ReadLine()) != null)
                                                {
                                                    ListFileToCheck.Add(line);
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            strTmp = string.Empty;
                                            strTmp = strProzessName + "Exception: " + ex.ToString();
                                            strTmp = Functions.GetLogString(strTmp, strTmp, false);
                                            this.systemLVS.listLogToFileSystem.Add(strTmp);
                                            clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, Application.StartupPath, clsLogbuchCon.const_Task_System);
                                        }


                                        //---Daten prüfen 
                                        if (ListFileToCheck.Count > 0)
                                        {
                                            clsWatchDog Dog = new clsWatchDog(this.GLUser, this.system);
                                            Dog.AnlayseWDFileData(ListFileToCheck);

                                            //---Logeintrag
                                            strTmp = strProzessName + Dog.InfoText;
                                            strTmp = Functions.GetLogString(strTmp, strTmp, false);
                                            this.systemLVS.listLogToFileSystem.Add(strTmp);
                                            clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, Application.StartupPath, clsLogbuchCon.const_Task_System);
                                        }
                                        else
                                        {
                                            //---Logeintrag
                                            strTmp = strProzessName + "Keine Filedaten vorhanden !!!";
                                            strTmp = Functions.GetLogString(strTmp, strTmp, false);
                                            this.systemLVS.listLogToFileSystem.Add(strTmp);
                                            clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, Application.StartupPath, clsLogbuchCon.const_Task_System);
                                        }
                                    }
                                    else
                                    {
                                        //---Logeintrag
                                        strTmp = strProzessName + "Datei: " + ftp.WD_DownloadFilePath + " konnte nicht gefunden werden!";
                                        strTmp = Functions.GetLogString(strTmp, strTmp, false);
                                        this.systemLVS.listLogToFileSystem.Add(strTmp);
                                        clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, Application.StartupPath, clsLogbuchCon.const_Task_System);

                                        clsMail ErrorMail = new clsMail();
                                        ErrorMail.InitClass(this.GLUser, this.system);
                                        ErrorMail.Subject = "!!!WatchDog Datei nicht vorhanden!!! - " + wdClient;
                                        ErrorMail.Message = "Zeit: " + DateTime.Now.ToString() + Environment.NewLine +
                                                            "Datei: " + ftp.WD_DownloadFilePath;
                                        ErrorMail.SendError();

                                    }
                                }
                                else
                                {
                                    strTmp = strProzessName + ftp.WD_InfoText + " -> konnte nicht gefunden werden !!!";
                                    strTmp = Functions.GetLogString(strTmp, strTmp, false);
                                    this.systemLVS.listLogToFileSystem.Add(strTmp);
                                    clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, Application.StartupPath, clsLogbuchCon.const_Task_System);
                                }
                            }//if
                            InitDGVLogSYS();
                            Thread.Sleep(3000);
                        }// foreach               

                        //---Logeintrag
                        strTmp = string.Empty;
                        strTmp = Functions.GetLogString(strTmp, "[System].[WatchDog] - Prozessdurchlauf beendet....", false);
                        this.systemLVS.listLogToFileSystem.Add(strTmp);
                        clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, Application.StartupPath, clsLogbuchCon.const_Task_System);

                        InitDGVLogSYS();
                        //---SignOfLife
                        clsWD_SignOfLife SoL = new clsWD_SignOfLife(this.GLUser, this.system, true);

                        if (!SoL.Infotext.Equals(string.Empty))
                        {
                            strProzessName = "[WatchDog].[SignOfLife]";
                            strTmp = string.Empty;
                            strTmp = strProzessName;
                            strTmp = Functions.GetLogString(strTmp, strTmp, false);
                            this.systemLVS.listLogToFileSystem.Add(strTmp);

                            strTmp = string.Empty;
                            strTmp = strProzessName + " > " + SoL.Infotext;
                            strTmp = Functions.GetLogString(strTmp, strTmp, false);
                            this.systemLVS.listLogToFileSystem.Add(strTmp);

                            //---Logeintrag
                            strTmp = string.Empty;
                            strTmp = strProzessName;
                            strTmp = Functions.GetLogString(strTmp, strTmp, false);
                            this.systemLVS.listLogToFileSystem.Add(strTmp);

                            clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, Application.StartupPath, clsLogbuchCon.const_Task_System);
                            InitDGVLogSYS();
                        }

                        double dbTmp = (double)tWatchDog.Interval;
                        this.dtWDNextStart = DateTime.Now.AddMilliseconds(dbTmp);
                        SetWDInfo();
                    }
                }
            }
            catch (Exception ex)
            {
                string s = ex.ToString();
                clsError Error = new clsError();
                Error.Sys = this.systemLVS;
                Error._GL_User = this.GLUser;
                Error.Aktion = "frmMainCom - Task_WatchDog() ";
                Error.Datum = DateTime.Now;
                Error.ErrorText = ex.ToString();
                Error.exceptText = ex.InnerException.ToString();
                Error.WriteError();
            }
        }
        /// <summary>
        ///             DGV SYS
        /// </summary>
        private void InitDGVLogSYS()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            InitDGVLogSYS();
                                                                        }
                                                                    )
                                    );
                return;
            }
            List<clsViewLog> SourceSys = new List<clsViewLog>();
            SourceSys = clsLogbuchCon.GetLogText(null, Application.StartupPath, clsLogbuchCon.const_Task_System);
            this.dgvLogSYS.DataSource = null;
            this.dgvLogSYS.DataSource = SourceSys;
            this.dgvLogSYS.BestFitColumns();
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetWDInfo()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            SetWDInfo();
                                                                        }
                                                                    )
                                    );
                return;
            }
            string strInfo = string.Empty;
            strInfo += "WD Info:" + Environment.NewLine + Environment.NewLine;

            strInfo += String.Format("{0}\t{1}", "letzter Start:", this.dtWDStart.ToString("dd.MM.yyyy HH:mm:ss")) + Environment.NewLine;
            strInfo += String.Format("{0}\t{1}", "nächster Start:", this.dtWDNextStart.ToString("dd.MM.yyyy HH:mm:ss")) + Environment.NewLine;
            strInfo += String.Format("{0}\t{1}", "Intervall Ticks:", tWatchDog.Interval.ToString("#,###,##0")) + Environment.NewLine;
            decimal decIntervall = (decimal)tWatchDog.Interval / 1000;
            strInfo += String.Format("{0}\t{1}", "Intervall Sek./Min.:", decIntervall.ToString("####.##")) + "/" + (decIntervall / 60).ToString("####.##") + Environment.NewLine;

            strInfo += Environment.NewLine + Environment.NewLine;
            strInfo += "System Info:" + Environment.NewLine + Environment.NewLine;
            if (this.system.VE_Autostart)
            {
                strInfo += String.Format("{0}\t{1}", "System - Autostart:     ", "true") + Environment.NewLine;
            }
            else
            {
                strInfo += String.Format("{0}\t{1}", "Autostart:", "false") + Environment.NewLine;
            }
            strInfo += String.Format("{0}\t{1}", "SignOfLifeIntervall:      ", this.system.VE_SignOfLifeIntervall.ToString("#,###,##0")) + Environment.NewLine;
            strInfo += String.Format("{0}\t{1}", "TaskReadThreadDuration:", this.system.VE_TaskReadThreadDuration.ToString("#,###,##0")) + Environment.NewLine;
            strInfo += String.Format("{0}\t{1}", "TaskWriteThreadDuration:", this.system.VE_TaskWriteThreadDuration.ToString("#,###,##0")) + Environment.NewLine;



            tbWDInfo.Text = strInfo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                string strSrvInfo = this.system.SystemInfo;

                clsMail CloseMail = new clsMail();
                CloseMail.InitClass(this.GLUser, this.system);
                CloseMail.Subject = strSrvInfo + " - " + clsWD_SignOfLife.const_CloseMail_Subject;
                string strMes = "SignOfLife - App wird geschlossen...." + Environment.NewLine;
                strMes += "Zeitpunkt: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + Environment.NewLine;
                CloseMail.Message = strMes;

                if (this.system.VE_List_WDInfoMail.Count > 0)
                {
                    CloseMail.ListMailReceiver.AddRange(this.system.VE_List_WDInfoMail);
                    CloseMail.Send_WD_SignOfLife();
                }
                else
                {
                    CloseMail.Subject += " -> !!! keine EMail-Adresse angegeben !!!";
                    CloseMail.SendError();
                }
            }
            catch (Exception ex)
            {
                clsError Error = new clsError();
                //Error.Sys = new clsSystem();
                //Error._GL_User = null;
                Error.Aktion = "frmMainCom - FrmMain_FormClosing - Mailversand ";
                Error.Datum = DateTime.Now;
                Error.ErrorText = ex.Message.ToString();
                Error.exceptText = ex.ToString();
                Error.WriteError();
            }
        }
    }
}
