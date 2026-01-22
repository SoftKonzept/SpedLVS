using Communicator.Classes;
using LVS;
using LVS.Communicator.CronJob;
using LVS.Communicator.EdiVDA;
using LVS.Constants;
using LVS.CustomProcesses;
using LVS.Enumerations;
using LVS.Models;
using LVS.ViewData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Communicator
{
    public partial class frmMainCom : Telerik.WinControls.UI.RadRibbonForm
    {
        public const string const_Prozess_SystemStart = "System-Start";
        public const string const_Prozess_ComProzessName = "Communicator";

        internal clsLogbuchCon log;
        public const Int32 const_ThreadTaskDuration = 600000;
        public const Int32 const_ThreadMainDuration = 600000;

        public const Int32 const_ThreadTaskDuration_Debug = 6000;
        public const Int32 const_LogMaxItems = 50;

        internal clsSQLCOM conLVS = new clsSQLCOM();
        internal clsSQLCOM conCom = new clsSQLCOM();
        internal clsSystem system = new clsSystem();
        //internal clsUpdate Update;

        internal LVS.clsSystem systemLVS = new LVS.clsSystem();

        internal clsLogbuchCon Log = new clsLogbuchCon();
        internal clsQueue Queue = new clsQueue();
        internal clsJobs Jobs = new clsJobs();

        public Globals._GL_SYSTEM GLSystem = new Globals._GL_SYSTEM();
        public Globals._GL_USER GLUser = new Globals._GL_USER();


        internal bool bDoWork;
        internal bool bThreadMain = true;
        internal List<clsViewLog> SourceLogInfo = new List<clsViewLog>();

        // THREADS 
        internal bool bThreadASNread = true;
        internal bool bThreadASNwrite = true;
        internal bool bThreadAbruf = true;
        internal bool bThreadAutoCreateEA = true;
        internal bool bThreadCronjobs = true;
        internal bool bThreadFTPDownload = true;
        internal bool bThreadProzesse = true;
        internal bool bThreadCustomProcess = true;

        internal bool bThreadASNreadSleep = false;
        internal bool bThreadASNwriteSleep = false;
        internal bool bThreadAutoCreateEASleep = false;
        internal bool bThreadCronjobsSleep = false;
        internal bool bThreadCustomProcessSleep = false;

        BackgroundWorker workerFinish;
        BackgroundWorker workerStart;

        Thread threadASNwrite;
        Thread threadASNread;
        Thread threadAutoCreateEA;
        Thread threadCronjobs;
        Thread threadWatchDog;
        Thread threadMain;
        Thread threadCustomProcess;


        internal List<string> ListLogAddString;

        internal DataTable dtProzesses = new DataTable("Prozesse");
        internal DataTable dtSourceProzesses = new DataTable("TMPProzesses");

        List<string> listInfoBox = new List<string>();
        List<string> listLogToFileASNRead = new List<string>();
        List<string> listLogToFileASNWrite = new List<string>();
        List<string> listLogToFileCreateEA = new List<string>();
        List<string> listLogToCronJob = new List<string>();
        List<string> listLogToCustomProcess = new List<string>();
        //List<string> listLogToFileSystem = new List<string>();

        //List<string> lisLtLogToWrite_ASNRead = new List<string>();
        //List<string> lisLtLogToWrite_ASNWrite = new List<string>();
        List<string> listLLogToWrite_System = new List<string>();

        public delegate void ThreadCtrInvokeEventHandler();
        internal DataTable dtInfoBox = new DataTable("Info");

        internal DateTime dtWDStart;
        internal DateTime dtWDNextStart;

        private List<string> LogTaskExceuted = new List<string>();


        /*********************************************************************
         *                  Process / Methoden
         * *****************************************************************/
        ///<summary>frmMainCom / frmMainCom</summary>
        ///<remarks></remarks>
        public frmMainCom()
        {
            InitializeComponent();
            //Button aktivieren / deaktivieren
            btnMainStart.Enabled = true;
            btnMainStop.Enabled = false;
            btnMainExit.Enabled = true;
        }
        ///<summary>frmMainCom / frmMainCom_Load</summary>
        ///<remarks></remarks>
        private void frmMainCom_Load(object sender, EventArgs e)
        {
            if (Check_IsComActive())
            {
                //CloseApp();
                string txt = "Das Programm läuft bereits und kann nicht mehrfach gestartet werden!";
                clsMessages.Allgemein_ERRORTextShow(txt);
                Application.Exit();
            }
            else
            {

                try
                {
                    if (system.VE_IsWatchDog)
                    {
                        if (clsMessages.Allgemein_ModulNotInstalled_WatchDog())
                        {
                            Thread.Sleep(10000);
                            string strTmp = string.Empty;
                            strTmp = Functions.GetLogString(strTmp, "[System-Stopp] - Communicator-Anwendung Modus WatchDog wird nicht unterstützt - auto. geschlossen....", false);
                            this.systemLVS.listLogToFileSystem.Add(strTmp);
                            clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, Application.StartupPath, clsLogbuchCon.const_Task_System);
                            Application.Exit();
                        }
                    }
                    else
                    {
                        this.tabMain.IsSelected = true;

                        system = new clsSystem();
                        // initialisieren der DB Daten
                        system.InitSystem(ref this.GLSystem);  // zahl durch konst

                        this.Text = this.Text + " - [" + this.system.Client.ADR.ADRStringShort + "]";

                        bDoWork = true;

                        List<TabPage> listTabPageRemove = new List<TabPage>();
                        foreach (TabPage page in this.tabViewGrid.TabPages)
                        {
                            switch (page.Name)
                            {
                                case "tabViewPageLog":
                                case "tabPage_LogIN":
                                case "tabPage_LogOUT":
                                case "tabPage_LogSYS":
                                case "tabPage_LogCronJob":
                                    break;

                                case "tabPage_WatchDog":
                                    listTabPageRemove.Add(page);
                                    break;
                            }
                        }

                        //-- ein/ausblenden (kann nur entfernen) der pages
                        foreach (TabPage page in listTabPageRemove)
                        {
                            this.tabViewGrid.TabPages.Remove(page);
                        }

                        string strTmp = string.Empty;
                        strTmp = GetLogString(strTmp, "[" + const_Prozess_SystemStart + "] - Communicator wird initialisiert.", false);
                        this.system.listLogToFileSystem.Add(strTmp);
                        SetInfoInInfoBox2("Communicator wird initialisiert.");

                        strTmp = string.Empty;
                        strTmp = GetLogString(strTmp, "[" + const_Prozess_SystemStart + "] - Datenbankverbindung wird geprüft....", false);
                        this.system.listLogToFileSystem.Add(strTmp);

                        SetInfoInInfoBox2("Datenbankverbindung wird geprüft....");
                        clsLogbuchCon.WriteLogToFile(ref this.system.listLogToFileSystem, this.system, Application.StartupPath, clsLogbuchCon.const_Task_System);
                        //this.system.listLogToFileSystem.Clear();

                        DBConnectionCheck();
                        btnMainExit.Enabled = true;

                        //CHeck auf Update
                        clsUpdate UpdateApp = new clsUpdate(this, this.GLUser.User_ID);

                        //wenn das Update nicht erfolgreich ist, dann wird nach 5 Sekunden der Communicator beenedet
                        strTmp = string.Empty;
                        strTmp = GetLogString(strTmp, "[" + const_Prozess_SystemStart + "] - Check auf Update....", false);
                        this.system.listLogToFileSystem.Add(strTmp);
                        clsLogbuchCon.WriteLogToFile(ref this.system.listLogToFileSystem, this.system, Application.StartupPath, clsLogbuchCon.const_Task_System);
                        //this.system.listLogToFileSystem.Clear();

                        if (!UpdateApp.InitUpdate())
                        {
                            Thread.Sleep(3000);
                            //Application.Exit();
                            rbgroupStart_cmdPageLog.Enabled = false;
                            rbgroupStop_cmdPageLog.Enabled = false;
                        }

                        tAutostart.Enabled = false;
                        if (this.system.VE_Autostart)
                        {
                            int iMilSec = 60000;
                            if (this.system.DebugModeCOM)
                            {
                                iMilSec = 5000;
                            }
                            tAutostart.Interval = iMilSec;
                            tAutostart.Start();

                            strTmp = string.Empty;
                            strTmp = GetLogString(strTmp, "[" + const_Prozess_SystemStart + "] - AUTOSTART erfolgt in " + iMilSec / 1000 + " Sekunden...", false);
                            this.system.listLogToFileSystem.Add(strTmp);
                            clsLogbuchCon.WriteLogToFile(ref this.system.listLogToFileSystem, this.system, Application.StartupPath, clsLogbuchCon.const_Task_System);
                            //this.system.listLogToFileSystem.Clear();
                            SetInfoInInfoBox2("AUTOSTART erfolgt in " + iMilSec / 1000 + " Sekunden...");
                        }
                        InitLogs();
                    }
                }
                catch (Exception ex)
                {
                    clsMessages.Allgemein_ERRORTextShow(ex.ToString());
                }
            }
            LogTaskExceuted = new List<string>();
        }
        /// <summary>
        ///             Prüft, ob Communicator bereits gestartet ist und startet dann nicht
        /// </summary>
        /// <returns></returns>
        public bool Check_IsComActive()
        {
            bool bReturn = false;
            Process[] communicator = Process.GetProcesses();
            int iCount = 0;
            foreach (Process p in communicator)
            {
                string strName = p.ProcessName;
                if (p.ProcessName.Contains(const_Prozess_ComProzessName))
                {
                    iCount++;
                }
            }
            if (iCount > 1)
            {
                bReturn = true;
            }

            return bReturn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TAutostart_Tick(object sender, EventArgs e)
        {
            StartComProcess();
            tAutostart.Enabled = false;
        }
        ///<summary>frmMainCom / DBConnectionCheck</summary>
        ///<remarks></remarks>
        private void DBConnectionCheck()
        {
            //Check Connection
            if (
                (Functions.init_conCOM(ref this.GLSystem, ref this.systemLVS.listLogToFileSystem)) &
                (Functions.init_con(ref this.GLSystem))
              )
            {
                //Global User
                clsUser us = new clsUser();
                this.GLUser = us.Fill();

                Log.GL_User = this.GLUser;
                Queue.GL_User = this.GLUser;

                string strTmp = string.Empty;
                strTmp = GetLogString(strTmp, "[" + const_Prozess_SystemStart + "] - Datenbankverbindung konnte hergestellt werden....", false);
                this.systemLVS.listLogToFileSystem.Add(strTmp);
                SetInfoInInfoBox2("Datenbankverbindung konnte hergestellt werden....");

                strTmp = string.Empty;
                strTmp = GetLogString(strTmp, "[" + const_Prozess_SystemStart + "] - Communicator wird gestartet....", false);
                this.systemLVS.listLogToFileSystem.Add(strTmp);
                SetInfoInInfoBox2("Communicator wird gestartet....");
                //Initialisieren Datatable dtProzesse
                InitDataTableProzesse();

                //Logbucheintrag Start Communicator
                Log.Typ = enumLogArtItem.Start.ToString();
                Log.LogText = "Communicator neu gestartet...";
                Log.TableName = string.Empty;
                Log.TableID = 0;
                //Log.Add(Log.GetAddLogbuchSQLString());

                clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, Application.StartupPath, clsLogbuchCon.const_Task_System);
                //windowQueue
                InitQueue();
            }
            else
            {
                string strTmp = string.Empty;
                strTmp = GetLogString(strTmp, "[" + const_Prozess_SystemStart + "] - Communicator wird geschlossen....", false);
                this.systemLVS.listLogToFileSystem.Add(strTmp);
                clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, Application.StartupPath, clsLogbuchCon.const_Task_System);

                //keine Datenbank verbindung möglich
                SetInfoInInfoBox2("Communicator wird geschlossen....");
                Thread.Sleep(2000);
                Application.Exit();
            }
        }
        ///<summary>frmMainCom / InitDataTableProzesse</summary>
        ///<remarks>Zur leeren Datatable werden die Spalten erstellt</remarks>
        private void InitDataTableProzesse()
        {
            dtProzesses.Clear();
            if (dtProzesses.Columns["Name"] == null)
            {
                System.Data.DataColumn col = new System.Data.DataColumn();
                col.ColumnName = "Name";
                col.DataType = typeof(String);
                dtProzesses.Columns.Add(col);
            }
            if (dtProzesses.Columns["iVal"] == null)
            {
                System.Data.DataColumn col = new System.Data.DataColumn();
                col.ColumnName = "iVal";
                col.DataType = typeof(decimal);
                dtProzesses.Columns.Add(col);
            }
            if (dtProzesses.Columns["iValMax"] == null)
            {
                System.Data.DataColumn col = new System.Data.DataColumn();
                col.ColumnName = "iValMax";
                col.DataType = typeof(Int32);
                dtProzesses.Columns.Add(col);
            }
            if (dtProzesses.Columns["Daten"] == null)
            {
                System.Data.DataColumn col = new System.Data.DataColumn();
                col.ColumnName = "Daten";
                col.DataType = typeof(String);
                dtProzesses.Columns.Add(col);
            }

        }
        ///<summary>frmMainCom / frmMain_FormClosing</summary>
        ///<remarks>Eintrag in Log wenn Communicator geschlossen wird</remarks>
        private void InitQueue(BindingList<string> lvSource = null)
        {
            if (bThreadMain)
            {
                if (this.InvokeRequired)
                {
                    DataTable dt = Queue.GetQueue(true, this.system.VE_TaskWriteSingleModus);
                    BindingList<string> lvSourceQueue = new BindingList<string>();
                    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        string strTMp = string.Empty;
                        strTMp = ((DateTime)dt.Rows[i]["Datum"]).ToString() +
                                " - [" + dt.Rows[i]["ASNTyp"].ToString() + "] - " +
                                dt.Rows[i]["TableName"].ToString() + "/" +
                                dt.Rows[i]["TableID"].ToString();
                        lvSourceQueue.Add(strTMp);
                    }
                    this.BeginInvoke(
                                        new ThreadCtrInvokeEventHandler(
                                                                            delegate ()
                                                                            {
                                                                                InitQueue(lvSourceQueue);
                                                                            }
                                                                        )
                                     );
                    return;
                }
            }
        }
        ///<summary>frmMainCom / frmMainCom_FormClosing</summary>
        ///<remarks>Eintrag in Log wenn Communicator geschlossen wird</remarks>
        private void frmMainCom_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
        ///<summary>frmMainCom / radButtonElement6_Click</summary>
        ///<remarks>Communicator soll beendet werden. Hierzu wird der letzte Aktion noch ausgeführt und dann
        ///         die Anwendung beendet</remarks>
        private void radButtonElement6_Click(object sender, EventArgs e)
        {
            CloseApp();
        }
        /// <summary>
        /// 
        /// </summary>
        private void CloseApp()
        {
            bDoWork = false;
            string strTmp = string.Empty;
            strTmp = Functions.GetLogString(strTmp, "[System-STOP] - Communicator-Anwendung wird manuell geschlossen....", false);
            this.systemLVS.listLogToFileSystem.Add(strTmp);
            clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, Application.StartupPath, clsLogbuchCon.const_Task_System);
            Application.Exit();
        }

        /*****************************************************************************************
         *                              Haupmenü
         * ***************************************************************************************/
        ///<summary>frmMainCom / btnRefreshProzesse_Click</summary>
        ///<remarks></remarks>
        private void btnRefreshProzesse_Click(object sender, EventArgs e)
        {
            //InitDgvProzesse();
        }
        ///<summary>frmMainCom / btnStart_Click</summary>
        ///<remarks>Startet alle Prozesse im Communicator</remarks>
        private void btnStart_Click(object sender, EventArgs e)
        {
            StartComProcess();
        }
        /// <summary>
        /// 
        /// </summary>
        private void StartComProcess()
        {
            bDoWork = true;

            string strTmp = string.Empty;
            strTmp = Functions.GetLogString(strTmp, "[System-START] - Communicator-Prozesse werden manuell gestartet....", false);
            this.systemLVS.listLogToFileSystem.Add(strTmp);
            clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, Application.StartupPath, clsLogbuchCon.const_Task_System);

            workerStart = new BackgroundWorker();
            workerStart.WorkerReportsProgress = true;
            workerStart.WorkerSupportsCancellation = true;
            workerStart.DoWork += new DoWorkEventHandler(workerStart_DoWork);
            workerStart.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerStart_Completed);

            this.btnMainStart.Enabled = false;
            this.btnMainStop.Enabled = true;
            this.btnMainExit.Enabled = false;

            //string strTmp = string.Empty;
            //strTmp = Functions.GetLogString(strTmp, "[System-START] - Communicator-Prozesse werden manuell gestartet....", false);
            //this.systemLVS.listLogToFileSystem.Add(strTmp);
            //clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, Application.StartupPath, clsLogbuchCon.const_Task_System);
            workerStart.RunWorkerAsync();
        }
        /// <summary>
        /// workerStart_Completed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void workerStart_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            //throw new NotImplementedException();
        }
        /// <summary>
        /// workerStart_DoWork
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void workerStart_DoWork(object sender, DoWorkEventArgs e)
        {
            //if (this.system.VE_TaskReadThreadDuration == 0)
            //{
            //    tWatchDog.Interval = frmMainCom.const_ThreadTaskDuration;
            //}
            //else
            //{
            //    tWatchDog.Interval = this.system.VE_MainThreadDuration;
            //}
            DoProzess();
        }
        /********************************************************************************************
         *                          Threads
         * *****************************************************************************************/
        private bool CheckThreadASNReadFinished()
        {
            bool bReturn = true;
            if (threadASNread is Thread)
            {
                try
                {
                    bReturn = (!threadASNread.IsAlive);
                }
                catch (Exception e)
                {
                    string strError = e.ToString();
                }
            }
            return bReturn;
        }
        private bool CheckThreadASNWriteFinished()
        {
            bool bReturn = true;
            if (threadASNwrite is Thread)
            {
                try
                {
                    bReturn = (!threadASNwrite.IsAlive);
                }
                catch (Exception e)
                {
                    string strError = e.ToString();
                }
            }
            return bReturn;
        }
        ///<summary>frmMainCom / CheckThreadsAreFinished</summary>
        ///<remarks>Prüft ob alle Theads abgeschlossen sind, damit dann entsprechende Procedures durchgeführt werden 
        ///         können</remarks> 
        private bool CheckThreadsAreFinished(bool bExit)
        {
            //--- Taks_ASNread
            try
            {
                if (!bThreadASNreadSleep)
                {
                    threadASNread.Join();
                }
                else
                {
                    threadASNread.Abort();
                    while (threadASNread.IsAlive)
                    {
                        Thread.Sleep(1000);
                    }
                }
            }
            catch (Exception e)
            {
                string strError = e.ToString();
            }

            //--- Task_AutoCreate
            try
            {
                if (threadAutoCreateEA != null)
                {
                    if (!bThreadAutoCreateEASleep)
                    {
                        threadAutoCreateEA.Join();
                    }
                    else
                    {
                        threadAutoCreateEA.Abort();
                    }
                }
            }
            catch (Exception e)
            {
                string strError = e.ToString();
            }

            //--- Task_CronJob
            try
            {
                if (threadCronjobs != null)
                {
                    if (!bThreadCronjobsSleep)
                    {
                        threadCronjobs.Join();
                    }
                    else
                    {
                        threadCronjobs.Abort();
                    }
                }
            }
            catch (Exception e)
            {
                string strError = e.ToString();
            }

            //--- CustomProcess
            try
            {
                if (threadCustomProcess != null)
                {
                    if (!bThreadCustomProcessSleep)
                    {
                        threadCustomProcess.Join();
                    }
                    else
                    {
                        threadCustomProcess.Abort();
                    }
                }
            }
            catch (Exception e)
            {
                string strError = e.ToString();
            }

            //--- Task_AsnRead
            try
            {
                if (!threadASNread.IsAlive)
                {
                    threadASNread.Join();
                }
                else
                {
                    threadASNread.Abort();
                    while (threadASNread.IsAlive)
                    {
                        Thread.Sleep(1000);
                    }
                }
            }
            catch (Exception e)
            {
                string strError = e.ToString();
            }

            //--- Task_ASNWrite
            try
            {
                if (!bThreadASNwriteSleep)
                {
                    threadASNwrite.Join();
                }
                else
                {
                    threadASNwrite.Abort();
                    //treadASNwrite = treadASNwrite;
                }
            }
            catch (Exception e)
            {
                string strError = e.ToString();
            }
            try
            {
                if (threadMain != null)
                {
                    threadMain.Join();
                }
            }
            catch (Exception e)
            {
                string strError = e.ToString();
            }
            return true;
        }
        ///<summary>frmMainCom / btnStart_Click</summary>
        ///<remarks>Startet alle Prozesse im Communicator. In dieser Prozedure werden alle
        ///         verschiedenen Thread gestartet, die dann unabhängig von einander laufen.</remarks>
        private void DoProzess()
        {
            bDoWork = true;
            InitThreadASNread();
            InitThreadASNWrite();
            InitThreadCronjobs();
            InitThreadCustomizedProcess();
            InitThreadWatchDog();
            InitLogs();
        }
        ///<summary>frmMainCom / InitThreadProzesse</summary>
        ///<remarks></remarks>  
        private bool InitThreadASNread()
        {
            string strLogTaskExecuted = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss:f") + " - Task -> InitThreadASNread |-> Start" + Environment.NewLine;
            LogTaskExceuted.Add(strLogTaskExecuted);

            //if (this.system.VE_TaskReadThreadDuration == 0)
            //{
            //    tASNread.Interval = frmMainCom.const_ThreadTaskDuration;
            //}
            //else
            //{
            //    tASNread.Interval = this.system.VE_TaskReadThreadDuration;
            //}

            if (this.system.DebugModeCOM)
            {
                tASNread.Interval = frmMainCom.const_ThreadTaskDuration_Debug;
            }
            else
            {
                if (this.system.VE_TaskReadThreadDuration == 0)
                {
                    tASNread.Interval = frmMainCom.const_ThreadTaskDuration;
                }
                else
                {
                    tASNread.Interval = this.system.VE_TaskReadThreadDuration;
                }
            }

            threadASNread = new Thread(Task_ASNread);
            threadASNread.Name = "ASNread";
            threadASNread.Start();
            bThreadASNread = true;

            strLogTaskExecuted = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss:f") + " - Task -> InitThreadASNread |-> Executed" + Environment.NewLine;
            LogTaskExceuted.Add(strLogTaskExecuted);
            clsLogbuchCon.WriteLogToFile(ref LogTaskExceuted, this.system, Application.StartupPath, clsLogbuchCon.const_LogTaskExcecution);

            return bThreadASNread;
        }
        ///<summary>frmMainCom / InitThreadASNWrite</summary>
        ///<remarks></remarks>
        private bool InitThreadASNWrite()
        {
            string strLogTaskExecuted = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss:f") + " - Task -> InitThreadASNWrite |-> Start" + Environment.NewLine;
            LogTaskExceuted.Add(strLogTaskExecuted);

            //if (this.system.VE_TaskWriteThreadDuration == 0)
            //{
            //    tASNwrite.Interval = frmMainCom.const_ThreadTaskDuration;
            //}
            //else
            //{
            //    tASNwrite.Interval = this.system.VE_TaskWriteThreadDuration;
            //}


            if (this.system.DebugModeCOM)
            {
                tASNwrite.Interval = frmMainCom.const_ThreadTaskDuration_Debug;
            }
            else
            {
                if (this.system.VE_TaskWriteThreadDuration == 0)
                {
                    tASNwrite.Interval = frmMainCom.const_ThreadTaskDuration;
                }
                else
                {
                    tASNwrite.Interval = this.system.VE_TaskWriteThreadDuration;
                }
            }


            threadASNwrite = new Thread(Task_ASNwrite);
            threadASNwrite.Name = "ASNwrite";
            threadASNwrite.Start();
            bThreadASNread = true;

            strLogTaskExecuted = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss:f") + " - Task -> InitThreadASNWrite |-> Executed" + Environment.NewLine;
            LogTaskExceuted.Add(strLogTaskExecuted);
            clsLogbuchCon.WriteLogToFile(ref LogTaskExceuted, this.system, Application.StartupPath, clsLogbuchCon.const_LogTaskExcecution);

            return bThreadASNread;
        }

        ///<summary>frmMainCom / InitThreadCronjobs</summary>
        ///<remarks></remarks>
        private bool InitThreadCronjobs()
        {
            string strLogTaskExecuted = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss:f") + " - Task -> InitThreadCronjobs |-> Start" + Environment.NewLine;
            LogTaskExceuted.Add(strLogTaskExecuted);

            //tCronjob.Interval = frmMainCom.const_ThreadTaskDuration;

            if (this.system.DebugModeCOM)
            {
                tCronjob.Interval = frmMainCom.const_ThreadTaskDuration_Debug;
            }
            else
            {
                tCronjob.Interval = frmMainCom.const_ThreadTaskDuration;
            }

            threadCronjobs = new Thread(Task_CronJobs);
            threadCronjobs.Name = "CronJobs";
            threadCronjobs.Start();
            bThreadCronjobs = true;

            strLogTaskExecuted = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss:f") + " - Task -> InitThreadCronjobs |-> Executed" + Environment.NewLine;
            LogTaskExceuted.Add(strLogTaskExecuted);
            clsLogbuchCon.WriteLogToFile(ref LogTaskExceuted, this.system, Application.StartupPath, clsLogbuchCon.const_LogTaskExcecution);

            return bThreadCronjobs;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool InitThreadCustomizedProcess()
        {
            string strLogTaskExecuted = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss:f") + " - Task -> InitThreadCustomizedProcess |-> Start" + Environment.NewLine;
            LogTaskExceuted.Add(strLogTaskExecuted);

            //tCustomizeProcess.Interval = frmMainCom.const_ThreadTaskDuration;

            if (this.system.DebugModeCOM)
            {
                tCustomizeProcess.Interval = frmMainCom.const_ThreadTaskDuration_Debug;
            }
            else
            {
                //tCustomizeProcess.Interval = frmMainCom.const_ThreadTaskDuration;
                tCustomizeProcess.Interval = this.system.VE_TaskCustomizedProcessThreadDuration;
            }
            tCustomizeProcess.Interval = this.system.VE_TaskCustomizedProcessThreadDuration;

            threadCustomProcess = new Thread(Task_CustomizedlProcesses);
            threadCustomProcess.Name = "CustomizedProcess";
            threadCustomProcess.Start();
            bThreadCustomProcess = true;

            strLogTaskExecuted = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss:f") + " - Task -> InitThreadCustomizedProcess |-> Executed" + Environment.NewLine;
            LogTaskExceuted.Add(strLogTaskExecuted);
            clsLogbuchCon.WriteLogToFile(ref LogTaskExceuted, this.system, Application.StartupPath, clsLogbuchCon.const_LogTaskExcecution);

            return bThreadCustomProcess; // bThreadCronjobs;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool InitThreadWatchDog()
        {
            string strLogTaskExecuted = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss:f") + " - Task -> InitThreadWatchDog |-> Start" + Environment.NewLine;
            LogTaskExceuted.Add(strLogTaskExecuted);

            //int iCountToBreak = 0;
            //while ((threadWatchDog is Thread) && (threadWatchDog.ThreadState == System.Threading.ThreadState.Running))
            //{
            //    iCountToBreak++;
            //    Thread.Sleep(5000);
            //    if (iCountToBreak > 5)
            //    {
            //        break;
            //    }
            //}

            if (this.system.DebugModeCOM)
            {
                tWatchDog.Interval = frmMainCom.const_ThreadTaskDuration_Debug;
            }
            else
            {
                tWatchDog.Interval = frmMainCom.const_ThreadTaskDuration;
            }

            threadWatchDog = new Thread(Task_SendVitalSign);
            threadWatchDog.Name = "WatchDog";
            threadWatchDog.Start();

            strLogTaskExecuted = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss:f") + " - Task -> InitThreadWatchDog |-> Executed" + Environment.NewLine;
            LogTaskExceuted.Add(strLogTaskExecuted);

            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool InitThreadAutoCreateEA()
        {
            string strLogTaskExecuted = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss:f") + " - Task -> InitThreadAutoCreateEA |-> Start" + Environment.NewLine;
            LogTaskExceuted.Add(strLogTaskExecuted);

            threadAutoCreateEA = new Thread(Task_AutoCreateEA);
            threadAutoCreateEA.Name = "AutoCreateEA";
            threadAutoCreateEA.Start();
            bThreadAutoCreateEA = true;

            strLogTaskExecuted = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + " - Task -> InitThreadAutoCreateEA |-> Executed" + Environment.NewLine;
            LogTaskExceuted.Add(strLogTaskExecuted);

            return bThreadAutoCreateEA;
        }





        ///<summary>frmMainCom / Task_SendVitaSign</summary>
        ///<remarks>Startet alle Prozesse im Communicator. In dieser Prozedure werden alle
        ///         verschiedenen Thread gestartet, die dann unabhängig von einander laufen.</remarks>
        private void Task_SendVitalSign()
        {
            string strProzessName = " [Send VitalFile to WatchDog ]";
            string strTmp = string.Empty;
            try
            {
                //Logbucheintrag
                clsLogbuchCon tmpLog = new clsLogbuchCon();
                tmpLog.GL_User = this.GLUser;

                tmpLog.Typ = enumLogArtItem.Start.ToString();
                tmpLog.LogText = strProzessName + " - Prozessbearbeitung gestartet.....";
                tmpLog.TableName = string.Empty;
                decimal decTmp = 0;
                tmpLog.TableID = decTmp;
                // tmpLog.Add(tmpLog.GetAddLogbuchSQLString());
                strTmp = string.Empty;
                strTmp = GetLogString(strTmp, tmpLog.LogText, false);
                this.system.listLogToFileSystem.Add(strTmp);

                strTmp = string.Empty;
                strTmp = GetLogString(strTmp, strProzessName + " - Task gestartet......", false);
                this.system.listLogToFileSystem.Add(strTmp);
                SetInfoInInfoBox2(strTmp);

                clsJobs vsJob = new clsJobs();
                vsJob.InitClass(GLSystem, GLUser, true);
                vsJob.FTP = new clsftp();
                vsJob.FTP.InitClass(vsJob, this.system, false);

                if (vsJob.ID > 0)
                {
                    if (vsJob.FTP.CheckConnection())
                    {
                        tmpLog.LogText = strProzessName + " - Der Aufbau der FTP-Verbindung ist erfolgreich...";
                        tmpLog.TableName = string.Empty;
                        decTmp = 0;
                        tmpLog.TableID = decTmp;
                        strTmp = string.Empty;
                        strTmp = GetLogString(strTmp, tmpLog.LogText, false);
                        SetInfoInInfoBox2(strTmp);
                        this.system.listLogToFileSystem.Add(strTmp);
                        //tmpLog.Add(tmpLog.GetAddLogbuchSQLString());

                        //Datei erstellen
                        clsWatchDogClient wdClient = new clsWatchDogClient();
                        wdClient.InitClassByClient(this.GLUser, this.system);

                        tmpLog.LogText = strProzessName + " - Löschen alter Watchdog-Files...";
                        strTmp = string.Empty;
                        strTmp = GetLogString(strTmp, tmpLog.LogText, false);
                        SetInfoInInfoBox2(strTmp);
                        this.system.listLogToFileSystem.Add(strTmp);
                        //tmpLog.Add(tmpLog.GetAddLogbuchSQLString());

                        string strInfoTxt = string.Empty;
                        strInfoTxt = wdClient.DeleteOldWatchDogFiles();
                        tmpLog.LogText = strProzessName + " - " + strInfoTxt;
                        strTmp = string.Empty;
                        strTmp = GetLogString(strTmp, tmpLog.LogText, false);
                        this.system.listLogToFileSystem.Add(strTmp);


                        tmpLog.LogText = strProzessName + " - Create Watchdog-Files...";
                        strInfoTxt = string.Empty;
                        strInfoTxt = wdClient.CreateWDFile();
                        tmpLog.LogText = strProzessName + " - " + strInfoTxt;
                        strTmp = string.Empty;
                        strTmp = GetLogString(strTmp, tmpLog.LogText, true);
                        this.system.listLogToFileSystem.Add(strTmp);

                        //ermitteln der Liste der Dateien die vom Communicator zum Empfänger übertragen werden sollen
                        //---TEST mr

                        clsJobs WDJob = new clsJobs();
                        WDJob = vsJob.Copy();

                        List<string> listFileToUpload = new List<string>();
                        string strSearchDatei = "*" + WDJob.FileName + ".txt";

                        //--- Files zur Übermittelung werden ermittelt
                        string[] aFiles = Directory.GetFiles(Application.StartupPath + WDJob.Path, strSearchDatei);

                        strTmp = string.Empty;
                        strTmp = " - > Pfad:" + Application.StartupPath + WDJob.Path + strSearchDatei;
                        strTmp = GetLogString(strTmp, tmpLog.LogText, true);

                        foreach (string strFileName in aFiles)
                        {
                            strTmp = string.Empty;
                            strTmp = " - > " + strFileName;
                            strTmp = GetLogString(strTmp, tmpLog.LogText, true);
                            listFileToUpload.Add(strFileName);
                        }

                        if (WDJob.PostBy == clsJobs.const_PostBy_FTP.ToString() || WDJob.PostBy == clsJobs.const_PostBy_SFTP.ToString())
                        {
                            strTmp = string.Empty;
                            if (WDJob.PostBy == clsJobs.const_PostBy_FTP.ToString())
                            {
                                WDJob.FTP = new clsftp();
                                //WDJob.FTP.InitClass(null, this.system);
                                //wenn nun Dateien vorhanden sind, dann werden diese übertragen
                                if (listFileToUpload.Count > 0)
                                {
                                    //myJob.TP = new clsftp();
                                    WDJob.FTP.InitClass(WDJob, null);
                                    strTmp = strProzessName + " - " + WDJob.FTP.Upload_WDFiles(ref WDJob, listFileToUpload);
                                }
                            }
                            strTmp = GetLogString(strTmp, strTmp, false);
                            this.system.listLogToFileSystem.Add(strTmp);
                        }
                    }
                    else
                    {
                        tmpLog.LogText = strProzessName + " - Vebindung zum FTP/SFTP Server fehlgeschlagen...";
                        strTmp = string.Empty;
                        strTmp = GetLogString(strTmp, tmpLog.LogText, true);
                        this.system.listLogToFileSystem.Add(strTmp);
                    }
                }
            }
            catch (Exception ex)
            {
                string strError = ex.ToString();
            }
            clsLogbuchCon.WriteLogToFile(ref this.system.listLogToFileSystem, this.system, Application.StartupPath, clsLogbuchCon.const_Task_System);
            //this.listLLogToWrite_System.AddRange(this.system.listLogToFileSystem);
            this.Invoke(new EventHandler(m_StartTimerWatchDog));
        }
        ///<summary>frmMainCom / btnStop_Click</summary>
        ///<remarks>Stop alle laufenden Prozesse des Communicators</remarks>
        private void btnStop_Click(object sender, EventArgs e)
        {
            StopApp();
        }
        /// <summary>
        /// 
        /// </summary>
        private void StopApp()
        {
            SetInfoInInfoBox2("Prozesse beenden -> bitte warten bis alle TASK-Prozesse beendet sind....");
            string strTmp = string.Empty;
            strTmp = Functions.GetLogString(strTmp, "[System-Stopp] - Prozesse beenden -> bitte warten bis alle TASK-Prozesse beendet sind....", false);
            this.systemLVS.listLogToFileSystem.Add(strTmp);

            bDoWork = false;
            workerFinish = new BackgroundWorker();
            workerFinish.WorkerReportsProgress = true;
            workerFinish.WorkerSupportsCancellation = true;
            workerFinish.DoWork += new DoWorkEventHandler(workerFinish_DoWork);
            workerFinish.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerFinish_Completed);
            this.btnMainStop.Enabled = false;
            workerFinish.RunWorkerAsync();
        }
        ///<summary>frmMainCom / workerFinish_Completed</summary>
        ///<remarks></remarks>
        private void workerFinish_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            this.btnMainStop.Enabled = false;
            this.btnMainExit.Enabled = true;
            this.btnMainStart.Enabled = true;

            bThreadMain = false;

            string strTmp = string.Empty;
            strTmp = "[MainThread] - alle Prozesse wurden beendet......";

            if (!this.system.VE_IsWatchDog)
            {
                //InitDgvProzesse();

                log = new clsLogbuchCon();
                log.GL_User = this.GLUser;

                log.Typ = enumLogArtItem.Stop.ToString();
                log.LogText = strTmp;
                log.TableName = string.Empty;
                decimal decTmp = 0;
                log.TableID = decTmp;
                //log.Add(log.GetAddLogbuchSQLString());
                SetInfoInInfoBox2(log.LogText);

            }
            else
            {
                //try
                //{
                //    threadWatchDog.Abort();
                //    while (threadWatchDog.IsAlive)
                //    {
                //        Thread.Sleep(1000);
                //    }
                //}
                //catch (Exception ex)
                //{ }
            }

            strTmp = Functions.GetLogString(strTmp, "[System-Stopp]." + strTmp, false);
            this.systemLVS.listLogToFileSystem.Add(strTmp);
            clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, Application.StartupPath, clsLogbuchCon.const_Task_System);
        }
        ///<summary>frmMainCom / workerFinish_DoWork</summary>
        ///<remarks></remarks>
        private void workerFinish_DoWork(object sender, DoWorkEventArgs e)
        {
            CheckThreadsAreFinished(true);
        }
        ///<summary>frmMainCom / InitThreadMain</summary>
        ///<remarks></remarks>
        private void InitThreadMain()
        {
            this.BringToFront();
            //bThreadMain = true;
            threadMain = new Thread(Task_Main);
            threadMain.Name = "Main";
            threadMain.Start();
        }
        ///<summary>frmMainCom / Task_Main</summary>
        ///<remarks></remarks>
        private void Task_Main()
        {
            bThreadMain = true;
            while (bDoWork)
            {
                InitQueue();
                Thread.Sleep(frmMainCom.const_ThreadMainDuration);
            }
            bThreadMain = false;
        }



        ///<summary>frmMainCom / Task_FTP</summary>
        ///<remarks></remarks> 
        private void Task_FTPdownload(ref clsJobs myJob, string strProzess)
        {
            string strProzessName = strProzess + ".[" + myJob.ASNFileTyp + "].[FTP_Down].[" + myJob.AsnTyp.Typ + "] ";
            try
            {
                //Logbucheintrag
                clsLogbuchCon tmpLog = new clsLogbuchCon();
                tmpLog.GL_User = this.GLUser;

                tmpLog.Typ = enumLogArtItem.Start.ToString();
                tmpLog.LogText = strProzessName + " - Prozessbearbeitung gestartet.....";
                tmpLog.TableName = string.Empty;
                decimal decTmp = 0;
                tmpLog.TableID = decTmp;
                tmpLog.Add(tmpLog.GetAddLogbuchSQLString());
                //LogText = GetLogString(LogText, tmpLog.LogText);
                SetInfoInInfoBox2(tmpLog.LogText);

                //FTPVerbindung aufbauen
                if (myJob.FtpOrSftp.CheckConnection())
                {
                    SetInfoInInfoBox2("CheckConnection" + myJob.FtpOrSftp.CheckConnection().ToString());
                    //SetInfoInInfoBox2(tmpLog.LogText);
                    tmpLog.LogText = strProzessName + " - Der Aufbau der FTP-Verbindung ist erfolgreich...";
                    tmpLog.TableName = string.Empty;
                    decTmp = 0;
                    tmpLog.TableID = decTmp;
                    tmpLog.Add(tmpLog.GetAddLogbuchSQLString());
                    //LogText = GetLogString(LogText, tmpLog.LogText);
                    SetInfoInInfoBox2(tmpLog.LogText);

                    myJob.FtpOrSftp.DownloadFiles(ref myJob);

                    //Ausgabe der übermittelten Dateien
                    if (myJob.FtpOrSftp.ListTransferedFilesToDelete.Count > 0)
                    {
                        foreach (string fileName in myJob.FtpOrSftp.ListTransferedFilesToDelete)
                        {
                            tmpLog.LogText = strProzessName + " - Datei:[" + myJob.PathDirectory + "\\" + fileName + "]";
                            tmpLog.TableName = string.Empty;
                            decTmp = 0;
                            tmpLog.TableID = decTmp;
                            tmpLog.Add(tmpLog.GetAddLogbuchSQLString());
                            //LogText = GetLogString(LogText, tmpLog.LogText);
                            SetInfoInInfoBox2(tmpLog.LogText);
                        }
                    }
                    else
                    {
                        //keine Dateien vorhanden
                        tmpLog.LogText = strProzessName + " - aktuell liegen keine Dateien zum download bereit...";
                        tmpLog.TableName = string.Empty;
                        decTmp = 0;
                        tmpLog.TableID = decTmp;
                        tmpLog.Add(tmpLog.GetAddLogbuchSQLString());
                        //LogText = GetLogString(LogText, tmpLog.LogText);
                        SetInfoInInfoBox2(tmpLog.LogText);
                    }
                }
                else
                {
                    tmpLog.LogText = strProzessName + " - Der Aufbau der FTP-Verbindung ist fehlgeschlagen...";
                    tmpLog.TableName = string.Empty;
                    decTmp = 0;
                    tmpLog.TableID = decTmp;
                    tmpLog.Add(tmpLog.GetAddLogbuchSQLString());
                    //LogText = GetLogString(LogText, tmpLog.LogText);
                    SetInfoInInfoBox2(tmpLog.LogText);
                }
            }
            catch (Exception ex)
            {
                string s = ex.ToString();
                clsError Error = new clsError();
                Error.Sys = this.systemLVS;
                Error._GL_User = this.GLUser;
                Error.Aktion = "frmMainCom - Task_FTPdownload() ";
                Error.Datum = DateTime.Now;
                Error.ErrorText = string.Empty;
                Error.exceptText = ex.ToString();
                Error.WriteError();

                Log = new clsLogbuchCon();
                Log.GL_User = this.GLUser;
                Log.Typ = enumLogArtItem.Start.ToString();
                Log.LogText = strProzessName + "  >>> Exception Error -> Error - Mail versendet...";
                Log.TableName = string.Empty;
                decimal decTmp = 0;
                Log.TableID = decTmp;
                ListLogAddString.Add(Log.GetAddLogbuchSQLString());
            }
            finally
            {
                Log = new clsLogbuchCon();
                Log.GL_User = this.GLUser;
                Log.Typ = enumLogArtItem.Stop.ToString();
                Log.LogText = strProzessName + " - Prozesse abgeschlossen...";
                Log.TableName = string.Empty;
                Log.TableID = 0;
                ListLogAddString.Add(Log.GetAddLogbuchSQLString());
                SetInfoInInfoBox2(Log.LogText);
            }
            bThreadFTPDownload = true;
        }
        ///<summary>frmMainCom / Task_FTP</summary>
        ///<remarks></remarks> 
        private void Task_FTPupload(ref clsJobs myJob, string strProzess)
        {
            string strProzessName = strProzess + ".[" + myJob.ASNFileTyp + "].[" + myJob.PostBy + "_Up].[" + myJob.AsnTyp.Typ + "] ";
            try
            {
                //Logbucheintrag
                clsLogbuchCon tmpLog = new clsLogbuchCon();
                tmpLog.GL_User = this.GLUser;

                tmpLog.Typ = enumLogArtItem.Start.ToString();
                tmpLog.LogText = strProzessName + " - Prozessbearbeitung gestartet.....";
                tmpLog.TableName = string.Empty;
                decimal decTmp = 0;
                tmpLog.TableID = decTmp;
                tmpLog.Add(tmpLog.GetAddLogbuchSQLString());
                SetInfoInInfoBox2(tmpLog.LogText);
                //FTPVerbindung aufbauen
                if (myJob.FtpOrSftp.CheckConnection())
                {
                    tmpLog.LogText = strProzessName + " - Der Aufbau der " + myJob.PostBy + "-Verbindung ist erfolgreich...";
                    tmpLog.TableName = string.Empty;
                    decTmp = 0;
                    tmpLog.TableID = decTmp;
                    tmpLog.Add(tmpLog.GetAddLogbuchSQLString());
                    SetInfoInInfoBox2(tmpLog.LogText);

                    //ermitteln der Liste die vom Communicator zum Empfänger übertragen werden sollen
                    List<string> listFileToUpload = new List<string>();
                    string strSearchDatei = "*" + myJob.FileName;
                    string[] aFiles = Directory.GetFiles(myJob.PathDirectory, strSearchDatei);
                    foreach (string strFileName in aFiles)
                    {
                        //this.Jobs.FileName = Path.GetFileName(strFileName);
                        listFileToUpload.Add(strFileName);
                    }

                    //wenn nun Dateien vorhanden sind, dann werden diese übertragen
                    if (listFileToUpload.Count > 0)
                    {
                        //myJob.TP = new clsftp();
                        myJob.FtpOrSftp.InitClass(myJob, null);
                        myJob.FtpOrSftp.UploadFiles(ref myJob, listFileToUpload);
                    }

                    string destFile = this.GLSystem.VE_OdettePath + myJob.Path + "\\Transfer";
                    string ErrorFile = this.GLSystem.VE_OdettePath + myJob.Path + "\\ERROR";
                    //Files mit Error werden in den ERROR Ordner kopiert
                    if (myJob.FtpOrSftp.ListErrorTransferFiles.Count > 0)
                    {
                        foreach (string fileName in myJob.FtpOrSftp.ListErrorTransferFiles)
                        {
                            log.LogText = strProzessName + " - Datei Übertragung fehlgeschlagen.....";
                            log.TableName = string.Empty;
                            decTmp = 0;
                            log.TableID = decTmp;
                            log.Add(log.GetAddLogbuchSQLString());
                            SetInfoInInfoBox2(log.LogText);

                            //Datei in Transferordner verschieben
                            string sourceFile = fileName;
                            string strFilePathTransfer = ErrorFile + "\\" + Path.GetFileName(fileName);

                            //Check Directory Transfer und Error
                            FunctionsCom.CheckDirectory(destFile);
                            FunctionsCom.CheckDirectory(ErrorFile);
                            string strDestFileTmp = destFile + "\\" + Path.GetFileName(fileName);
                            string strErrorFileTmp = ErrorFile + "\\" + Path.GetFileName(fileName);

                            string strTmpError = string.Empty;
                            if (!FunctionsCom.MoveAndDeleteFile(ErrorFile, fileName, ErrorFile, ref strTmpError))
                            {
                                log.LogText = strProzessName + " - " + strTmpError;

                                clsError Error = new clsError();
                                Error.InitClass(this.GLUser, this.system);
                                //Error.Sys = this.systemLVS;
                                //Error._GL_User = this.GLUser;
                                Error.Aktion = strProzessName + " - FunctionsCom.MoveAndDeleteFile(ErrorFile, fileName, ErrorFile, ref strTmpError)";
                                Error.Datum = DateTime.Now;
                                Error.ErrorText = string.Empty;
                                Error.exceptText = log.LogText;
                                Error.WriteError();
                            }
                            else
                            {
                                log.LogText = strProzessName + " - Datei in ERROR-Ordner verschoben:[" + strFilePathTransfer + "]";
                            }
                            log.TableName = string.Empty;
                            decTmp = 0;
                            log.TableID = decTmp;
                            log.Add(log.GetAddLogbuchSQLString());
                            SetInfoInInfoBox2(log.LogText);
                        }
                    }
                    //Erneut, da sonst die Variablen noch gefüllt sein können 
                    destFile = this.GLSystem.VE_OdettePath + myJob.Path + "\\Transfer";
                    ErrorFile = this.GLSystem.VE_OdettePath + myJob.Path + "\\ERROR";
                    //Ausgabe der übermittelten Dateien mit gleichzeitigem Transfer der Datei in den 
                    //order Transfer
                    if (myJob.FtpOrSftp.ListTransferedFilesToDelete.Count > 0)
                    {
                        foreach (string fileName in myJob.FtpOrSftp.ListTransferedFilesToDelete)
                        {
                            log.LogText = strProzessName + " - Datei übermittelt:[" + myJob.PathDirectory + "\\" + Path.GetFileName(fileName) + "]";
                            log.TableName = string.Empty;
                            decTmp = 0;
                            log.TableID = decTmp;
                            log.Add(log.GetAddLogbuchSQLString());
                            SetInfoInInfoBox2(log.LogText);

                            //Datei in Transferordner verschieben
                            string strFilePathTransfer = myJob.ASNFileStorePath + "\\" + Path.GetFileName(fileName);

                            //Check Directory Transfer und Error
                            FunctionsCom.CheckDirectory(destFile);
                            FunctionsCom.CheckDirectory(ErrorFile);

                            string strDestFileTmp = destFile + "\\" + Path.GetFileName(fileName);
                            string strErrorFileTmp = ErrorFile + "\\" + Path.GetFileName(fileName);

                            string strTmpError = string.Empty;
                            if (!FunctionsCom.MoveAndDeleteFile(strDestFileTmp, fileName, strErrorFileTmp, ref strTmpError))
                            {
                                log.LogText = strProzessName + " - " + strTmpError;

                                clsError Error = new clsError();
                                Error.InitClass(this.GLUser, this.system);
                                //Error.Sys = this.systemLVS;
                                //Error._GL_User = this.GLUser;
                                Error.Aktion = strProzessName + " - FunctionsCom.MoveAndDeleteFile";
                                Error.Datum = DateTime.Now;
                                Error.ErrorText = string.Empty;
                                Error.exceptText = log.LogText;
                                Error.WriteError();
                            }
                            else
                            {
                                log.LogText = strProzessName + " - Datei in Transferordner verschoben:[" + strFilePathTransfer + "]";
                            }
                            log.TableName = string.Empty;
                            decTmp = 0;
                            log.TableID = decTmp;
                            log.Add(log.GetAddLogbuchSQLString());
                            SetInfoInInfoBox2(log.LogText);
                        }
                    }
                    else
                    {
                        //keine Dateien vorhanden
                        tmpLog.LogText = strProzessName + " - aktuell liegen keine Dateien zum upload bereit...";
                        tmpLog.TableName = string.Empty;
                        decTmp = 0;
                        tmpLog.TableID = decTmp;
                        tmpLog.Add(tmpLog.GetAddLogbuchSQLString());
                        SetInfoInInfoBox2(tmpLog.LogText);
                    }
                }
                else
                {
                    tmpLog.LogText = strProzessName + " - Der Aufbau der " + myJob.PostBy + "-Verbindung ist fehlgeschlagen...";
                    tmpLog.TableName = string.Empty;
                    decTmp = 0;
                    tmpLog.TableID = decTmp;
                    tmpLog.Add(tmpLog.GetAddLogbuchSQLString());
                    SetInfoInInfoBox2(tmpLog.LogText);
                }
            }
            catch (Exception ex)
            {
                string s = ex.ToString();

                clsError Error = new clsError();
                Error.Sys = this.systemLVS;
                Error._GL_User = this.GLUser;
                Error.Aktion = "Task_FTPupload() ";
                Error.Datum = DateTime.Now;
                Error.ErrorText = string.Empty;
                Error.exceptText = ex.ToString();
                Error.WriteError();

                Log = new clsLogbuchCon();
                Log.GL_User = this.GLUser;
                Log.Typ = enumLogArtItem.Start.ToString();
                Log.LogText = strProzessName + "  >>> Exception Error -> Error - Mail versendet...";
                Log.TableName = string.Empty;
                decimal decTmp = 0;
                Log.TableID = decTmp;
                ListLogAddString.Add(Log.GetAddLogbuchSQLString());
            }
            finally
            {
                Log = new clsLogbuchCon();
                Log.GL_User = this.GLUser;
                Log.Typ = enumLogArtItem.Stop.ToString();
                Log.LogText = strProzessName + " - Prozesse abgeschlossen...";
                Log.TableName = string.Empty;
                Log.TableID = 0;
                ListLogAddString.Add(Log.GetAddLogbuchSQLString());

                SetInfoInInfoBox2(Log.LogText);
            }
        }
        ///<summary>frmMainCom / Task_CallRead</summary>
        ///<remarks>
        ///         Vorgehen: 
        ///         1. Check der angegebenen Eingangs-Ordner in der Table Job aus "falsche" Dateien => verschieben in den angegebenen CheckPath
        ///         2. Jobs aus der Datenbank für die Abrufe
        ///         3. Ahhand der Liste die entsprechenden Eingangsordener auf die passende Dateien prüfen auf Verweis und somit Arbeitsbereich
        ///         4. Datei gefunden - einlesen und speichern , sonst verschieben in CheckPath
        ///         </remarks>
        private void Task_CALLRead()
        {
            //Int32 iCountToBreakWhile = 1;
            string strTmp = string.Empty;
            //Init Var/Classes
            listLogToFileASNRead = new List<string>();
            listLogToFileASNRead.Add(clsLogbuchCon.const_Logbuch_Trennzeichen);

            ListLogAddString = new List<string>();
            string strProzessMain = "[CALLRead]";
            string strProzessName = strProzessMain;
            decimal decTmp = 0;

            strTmp = string.Empty;
            strTmp = GetLogString(strTmp, strProzessName + " - Task gestartet......", false);
            listLogToFileASNRead.Add(strTmp);
            SetInfoInInfoBox2(strTmp);

            String LogText = String.Empty;
            try
            {
                Jobs = new clsJobs();
                Jobs.GL_User = this.GLUser;
                Jobs.GLSystem = this.GLSystem;
                DataTable dtJobs = Jobs.dtJobsAbrufe;

                Int32 iValMax = dtJobs.Rows.Count;
                if (iValMax > 0)
                {
                    LogText = GetLogString(LogText, strProzessName + " - Anzahl der zu erledigenden Jobs: " + iValMax.ToString(), true);

                    strTmp = string.Empty;
                    strTmp = GetLogString(strTmp, strProzessName + " - Anzahl der zu erledigenden Jobs: " + iValMax.ToString(), false);
                    listLogToFileASNRead.Add(strTmp);
                    SetInfoInInfoBox2(strTmp);
                    for (Int32 i = 0; i <= dtJobs.Rows.Count - 1; i++)
                    {
                        decTmp = 0;
                        Decimal.TryParse(dtJobs.Rows[i]["ID"].ToString(), out decTmp);
                        if (decTmp > 0)
                        {
                            Jobs.ID = decTmp;
                            Jobs.Fill();
                            strProzessName = strProzessMain + ".[" + Jobs.ASNFileTyp + "]";
                            //                            
                            if (Jobs.PostBy == clsJobs.const_PostBy_FTP.ToString() || Jobs.PostBy == clsJobs.const_PostBy_SFTP.ToString())
                            {
                                if (Jobs.PostBy == clsJobs.const_PostBy_FTP.ToString())
                                {
                                    Jobs.FtpOrSftp = new clsftp();
                                }
                                else
                                {
                                    Jobs.FtpOrSftp = new clsSFTP();
                                }
                                Jobs.FtpOrSftp.InitClass(this.Jobs, null);
                                Task_FTPdownload(ref this.Jobs, strProzessName);
                            }

                            //iCountToBreakWhile = 1;
                            Int32 iTask = i + 1;

                            strTmp = string.Empty;
                            strTmp = GetLogString(strTmp, strProzessName + " - " + "Status: Eingangsordner [ " + Jobs.PathDirectory + " ] wird  überprüft", false);
                            listLogToFileASNRead.Add(strTmp);
                            SetInfoInInfoBox2(strTmp);
                            //Ermittelt alle Dateien in dem Ordner
                            string strSearchDatei = Jobs.SearchFileName;
                            //Fremddateien verschieben
                            string[] aFiles = Directory.GetFiles(Jobs.PathDirectory, strSearchDatei + "*");
                            foreach (string strSourceFilePathAndName in aFiles)
                            {
                                Thread.Sleep(1000);
                                this.Jobs.FileName = Path.GetFileName(strSourceFilePathAndName);
                                if (!FunctionsCom.IsFileLocked(strSourceFilePathAndName))
                                {
                                    clsASN asn = new clsASN();
                                    asn.InitClass(this.GLUser, this.GLSystem);
                                    asn.Sys = this.system;
                                    asn.Job = this.Jobs;
                                    asn.Prozess = strProzessName;
                                    asn.ASNArt.ID = asn.Job.ASNArtID;
                                    asn.ASNArt.Fill();

                                    asn.ReadVDAorXML(strSourceFilePathAndName);

                                    string sourceFile = Path.GetFileName(strSourceFilePathAndName);
                                    string destFile = this.GLSystem.VE_OdettePath + this.Jobs.ASNFileStorePath;
                                    string ErrorFile = this.GLSystem.VE_OdettePath + this.Jobs.ErrorPath;
                                    string CheckTransferFile = this.GLSystem.VE_OdettePath + this.Jobs.CheckTransferPath;

                                    //Check Directory Transfer und Error
                                    FunctionsCom.CheckDirectory(destFile);
                                    FunctionsCom.CheckDirectory(ErrorFile);
                                    FunctionsCom.CheckDirectory(CheckTransferFile);

                                    //check Error
                                    if (asn.ListError.Count > 0)
                                    {
                                        for (Int32 l = 0; l <= asn.ListError.Count - 1; l++)
                                        {
                                            clsLogbuchCon tmpLog = (clsLogbuchCon)asn.ListError[l];
                                            strTmp = string.Empty;
                                            strTmp = GetLogString(strTmp, strProzessName + " - " + tmpLog.LogText, false);
                                            listLogToFileASNRead.Add(strTmp);
                                            SetInfoInInfoBox2(strTmp);
                                        }
                                        //checkTransferFile
                                        if (asn.IsTransferFile)
                                        {
                                            strTmp = string.Empty;
                                            if (this.system.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_SZG + "_"))
                                            {
                                                //Damit strFileEnd auch eindeutig wird
                                                Thread.Sleep(1000);
                                                //umbennennen für BMW
                                                if (!this.Jobs.TransferFileName.Equals(string.Empty))
                                                {
                                                    string strFileEnd = string.Empty;
                                                    string strDateTimeEnd = string.Empty;
                                                    strFileEnd = "~" + DateTime.Now.ToString("HH") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");
                                                    string strTmpFileName = this.Jobs.TransferFileName + strFileEnd + strDateTimeEnd;
                                                    CheckTransferFile = CheckTransferFile + "\\" + strTmpFileName;
                                                }
                                                else
                                                {
                                                    CheckTransferFile = CheckTransferFile + "\\" + this.Jobs.FileName;
                                                }
                                            }
                                            else
                                            {
                                                CheckTransferFile = CheckTransferFile + "\\" + this.Jobs.FileName;
                                            }// check Client SZG

                                            string strTmpError = string.Empty;
                                            if (!FunctionsCom.MoveAndDeleteFile(CheckTransferFile, strSourceFilePathAndName, CheckTransferFile, ref strTmpError))
                                            {
                                                strTmp = strProzessName + " - " + strTmpError;

                                                clsError Error = new clsError();
                                                Error.InitClass(this.GLUser, this.system);
                                                Error.Aktion = strProzessName + " - FunctionsCom.MoveAndDeleteFile";
                                                Error.Datum = DateTime.Now;
                                                Error.ErrorText = string.Empty;
                                                Error.exceptText = strTmp;
                                                Error.WriteError();
                                            }
                                            else
                                            {
                                                strTmp = GetLogString(strTmp, strProzessName + " - " + "Datei: [ " + strSourceFilePathAndName + " ] wird  in Ordern [ " + CheckTransferFile + " ] verschoben", false);
                                            }
                                        }
                                        else
                                        {
                                            destFile = ErrorFile + "\\" + sourceFile;
                                            ErrorFile = ErrorFile + "\\" + sourceFile;

                                            string strTmpError = string.Empty;
                                            if (!FunctionsCom.MoveAndDeleteFile(destFile, strSourceFilePathAndName, ErrorFile, ref strTmpError))
                                            {
                                                strTmp = strProzessName + " - " + strTmpError;

                                                clsError Error = new clsError();
                                                Error.InitClass(this.GLUser, this.system);
                                                Error.Aktion = strProzessName + " - FunctionsCom.MoveAndDeleteFile";
                                                Error.Datum = DateTime.Now;
                                                Error.ErrorText = string.Empty;
                                                Error.exceptText = strTmp;
                                                Error.WriteError();
                                            }
                                            else
                                            {
                                                if (!this.Jobs.CheckCloneFileName.Equals(string.Empty))
                                                {
                                                    string tmpCloneFileName = this.Jobs.CheckCloneFileName.Replace("*", "");
                                                    string tmpSourceFileExtension = string.Empty;
                                                    if (Path.GetFileName(sourceFile).Contains("~"))
                                                    {
                                                        int iSubStart = sourceFile.IndexOf("~");
                                                        //int iStringLenth = (sourceFile.Length - 1) - iSubStart;
                                                        tmpSourceFileExtension = sourceFile.Substring(iSubStart);
                                                    }

                                                    string sourceCheckCloneFile = this.Jobs.CheckCloneFileName.Replace("*", "") + tmpSourceFileExtension;
                                                    string destCheckClonePath = this.GLSystem.VE_OdettePath + this.Jobs.ErrorPath;
                                                    string destCheckCloneFilePathError = destCheckClonePath + "\\" + sourceCheckCloneFile;
                                                    string strCheckCloneFilePath = this.GLSystem.VE_OdettePath + this.Jobs.CheckCloneFilePath + "\\" + sourceCheckCloneFile;
                                                    if (File.Exists(strCheckCloneFilePath))
                                                    {
                                                        //string destCheckClonePathTransfer = this.GLSystem.VE_OdettePath + this.Jobs.ASNFileStorePath;
                                                        FunctionsCom.MoveAndDeleteFile(destCheckCloneFilePathError, strCheckCloneFilePath, ErrorFile, ref strTmpError);
                                                    }
                                                }
                                                strTmp = GetLogString(strTmp, strProzessName + " - " + "Datei: [" + strSourceFilePathAndName + "] wird  in Ordern [" + ErrorFile + "] verschoben", false);
                                            }
                                        }//CheckTransferFile

                                        listLogToFileASNRead.Add(strTmp);
                                        SetInfoInInfoBox2(strTmp);
                                    }
                                    else
                                    {
                                        //-- der unbenannte Dateinnamen, unter dem die Datei im Transferorder gespeichert werden soll
                                        //--- steht nun in asn.Filename nach dem asn.Add()
                                        destFile = destFile + "\\" + asn.FileName;
                                        ErrorFile = ErrorFile + "\\" + sourceFile;

                                        string strTmpError = string.Empty;
                                        strTmp = string.Empty;

                                        if (!FunctionsCom.MoveAndDeleteFile(destFile, strSourceFilePathAndName, ErrorFile, ref strTmpError))
                                        {
                                            strTmp = GetLogString(strTmp, strProzessName + " - " + strTmpError, false);

                                            clsError Error = new clsError();
                                            Error.InitClass(this.GLUser, this.system);
                                            Error.Aktion = strProzessName + " - FunctionsCom.MoveAndDeleteFile";
                                            Error.Datum = DateTime.Now;
                                            Error.ErrorText = string.Empty;
                                            Error.exceptText = log.LogText;
                                            Error.WriteError();
                                        }
                                        else
                                        {
                                            if (!this.Jobs.CheckCloneFileName.Equals(string.Empty))
                                            {
                                                string tmpCloneFileName = this.Jobs.CheckCloneFileName.Replace("*", "");
                                                string tmpSourceFileExtension = string.Empty;
                                                if (Path.GetFileName(strSourceFilePathAndName).Contains("~"))
                                                {
                                                    //int iSubStart = strSourceFilePathAndName.IndexOf("~");
                                                    //int iStringLenth = strSourceFilePathAndName.Length - 1;
                                                    //tmpSourceFileExtension = strSourceFilePathAndName.Substring(iSubStart, iStringLenth);

                                                    int iSubStart = sourceFile.IndexOf("~");
                                                    //int iStringLenth = (sourceFile.Length - 1) - iSubStart;
                                                    tmpSourceFileExtension = sourceFile.Substring(iSubStart);
                                                }

                                                string sourceCheckCloneFile = this.Jobs.CheckCloneFileName.Replace("*", "") + tmpSourceFileExtension;
                                                string destCheckClonePath = this.GLSystem.VE_OdettePath + this.Jobs.CheckCloneFilePath;
                                                string strCheckCloneFilePath = destCheckClonePath + "\\" + sourceCheckCloneFile;
                                                if (File.Exists(strCheckCloneFilePath))
                                                {
                                                    string destCheckClonePathTransfer = this.GLSystem.VE_OdettePath + this.Jobs.ASNFileStorePath + "\\" + sourceCheckCloneFile;
                                                    FunctionsCom.MoveAndDeleteFile(destCheckClonePathTransfer, strCheckCloneFilePath, ErrorFile, ref strTmpError);
                                                }
                                            }

                                            //string ErrorFile = this.GLSystem.VE_OdettePath + this.Jobs.ErrorPath;
                                            CheckTransferFile = this.GLSystem.VE_OdettePath + this.Jobs.CheckTransferPath;

                                            //Check Directory Transfer und Error
                                            //FunctionsCom.CheckDirectory(destCheckClonePath);
                                            FunctionsCom.CheckDirectory(ErrorFile);
                                            FunctionsCom.CheckDirectory(CheckTransferFile);


                                            strTmp = GetLogString(strTmp, strProzessName + " - " + "Datei: [" + destFile + "] verarbeitet", false);
                                        }
                                        //InitThreadProzesse();
                                        //iCountToBreakWhile = 1;
                                        listLogToFileASNRead.Add(strTmp);
                                        SetInfoInInfoBox2(strTmp);
                                    }// check Error
                                }
                                else
                                {
                                    ;
                                    //Datei gelockt
                                    strTmp = string.Empty;
                                    strTmp = GetLogString(LogText, strProzessName + " - " + "Datei: [" + strSourceFilePathAndName + "] konnte nicht verarbeitet werden", false);
                                    listLogToFileASNRead.Add(strTmp);
                                    SetInfoInInfoBox2(strTmp);
                                }
                            }
                            Thread.Sleep(500);
                        }
                    }
                }
                else
                {
                    //LogText = GetLogString(LogText, strProzessName + " - Es liegt nichts an.....", true);
                    //SetInfoInInfoBox2(strProzessName + " - Es liegt nichts an.....");
                    strTmp = string.Empty;
                    strTmp = GetLogString(strTmp, strProzessName + " - Es liegt nichts an.....", false);
                    listLogToFileASNRead.Add(strTmp);
                }
            }
            catch (Exception ex)
            {
                string s = ex.ToString();

                clsError Error = new clsError();
                Error.Sys = this.systemLVS;
                Error._GL_User = this.GLUser;
                Error.Sys = this.system;
                Error.Aktion = "frmMainCom - Task_CALLread() ";
                Error.Datum = DateTime.Now;
                Error.ErrorText = string.Empty;
                Error.exceptText = ex.ToString();
                Error.WriteError();

                strTmp = string.Empty;
                strTmp = GetLogString(strTmp, strProzessName + " - FEHLER -> " + ex.ToString(), false);
                listLogToFileASNRead.Add(strTmp);
                SetInfoInInfoBox2(strTmp);
            }
            finally
            {
                strTmp = string.Empty;
                strTmp = GetLogString(strTmp, strProzessName + " - Prozesse abgeschlossen...", false);
                listLogToFileASNRead.Add(strTmp);
                SetInfoInInfoBox2(strTmp);
            }
            //this.Invoke(new EventHandler(m_StartTimerRead));
            //bThreadASNread = false;
            clsLogbuchCon.WriteLogToFile(ref listLogToFileASNRead, this.system, Application.StartupPath, clsLogbuchCon.const_Task_CALLRead);
        }
        ///<summary>frmMainCom / Task_ASNread</summary>
        ///<remarks>Vorgehen: 
        ///         1. Check der angegebenen Ordner in der Table Job aus "falsche" Dateien => verschieben in den angegebenen CheckPath
        ///         2. Jobs aus der Datenbank für die Überwachten Lfs - VDA Eingänge (nicht Abrufe)
        ///         3. Ahhand der Liste die entsprechenden Eingangsordener auf die passende Dateien prüfen auf Verweis und somit Arbeitsbereich
        ///         4. Datei gefunden - einlesen und speichern , sonst verschieben in CheckPath
        ///         </remarks>
        private void Task_ASNread()
        {
            //erst einmal hier als erstes
            Task_CALLRead();

            Int32 iCountToBreakWhile = 1;

            string strTmp = string.Empty;
            //Init Var/Classes
            listLogToFileASNRead = new List<string>();
            listLogToFileASNRead.Add(clsLogbuchCon.const_Logbuch_Trennzeichen);

            ListLogAddString = new List<string>();
            string strProzessMain = "[ASNread]";
            string strProzessName = strProzessMain;
            decimal decTmp = 0;

            strTmp = string.Empty;
            strTmp = GetLogString(strTmp, strProzessName + " - Task gestartet......", false);
            listLogToFileASNRead.Add(strTmp);
            SetInfoInInfoBox2(strTmp);

            String LogText = String.Empty;
            try
            {
                Jobs = new clsJobs();
                Jobs.GL_User = this.GLUser;
                Jobs.GLSystem = this.GLSystem;
                DataTable dtJobs = Jobs.dtJobsToDoIN;

                Int32 iValMax = dtJobs.Rows.Count;
                if (iValMax > 0)
                {
                    LogText = GetLogString(LogText, strProzessName + " - Anzahl der zu erledigenden Jobs: " + iValMax.ToString(), true);

                    strTmp = string.Empty;
                    strTmp = GetLogString(strTmp, strProzessName + " - Anzahl der zu erledigenden Jobs: " + iValMax.ToString(), false);
                    listLogToFileASNRead.Add(strTmp);
                    SetInfoInInfoBox2(strTmp);
                    for (Int32 i = 0; i <= dtJobs.Rows.Count - 1; i++)
                    {
                        decTmp = 0;
                        Decimal.TryParse(dtJobs.Rows[i]["ID"].ToString(), out decTmp);
                        if (decTmp > 0)
                        {
                            Jobs.ID = decTmp;
                            Jobs.Fill();
                            strProzessName = strProzessMain + ".[" + Jobs.ASNFileTyp + "]";
                            //                            
                            if (Jobs.PostBy == clsJobs.const_PostBy_FTP.ToString() || Jobs.PostBy == clsJobs.const_PostBy_SFTP.ToString())
                            {
                                if (Jobs.PostBy == clsJobs.const_PostBy_FTP.ToString())
                                {
                                    Jobs.FtpOrSftp = new clsftp();
                                }
                                else
                                {
                                    Jobs.FtpOrSftp = new clsSFTP();
                                }
                                Jobs.FtpOrSftp.InitClass(this.Jobs, null);
                                Task_FTPdownload(ref this.Jobs, strProzessName);
                            }

                            iCountToBreakWhile = 1;
                            Int32 iTask = i + 1;

                            strTmp = string.Empty;
                            if (!Jobs.ViewProzessName.Equals(string.Empty))
                            {
                                strProzessName = strProzessName + ".[" + Jobs.ViewProzessName + "]";
                            }

                            strTmp = GetLogString(strTmp, strProzessName + " - " + "Status: Eingangsordner [" + Jobs.PathDirectory + "] wird  überprüft", false);
                            listLogToFileASNRead.Add(strTmp);
                            SetInfoInInfoBox2(strTmp);
                            //Ermittelt alle Dateien in dem Ordner
                            string strSearchDatei = Jobs.SearchFileName;
                            //Fremddateien verschieben
                            string[] aFiles = Directory.GetFiles(Jobs.PathDirectory, strSearchDatei + "*");
                            foreach (string strSourceFilePathAndName in aFiles)
                            {
                                Thread.Sleep(1000);
                                this.Jobs.FileName = Path.GetFileName(strSourceFilePathAndName);
                                if (!FunctionsCom.IsFileLocked(strSourceFilePathAndName))
                                {
                                    clsASN asn = new clsASN();
                                    asn.InitClass(this.GLUser, this.GLSystem);
                                    asn.Sys = this.system;
                                    asn.Job = this.Jobs;
                                    asn.Prozess = strProzessName;
                                    asn.ASNArt.ID = asn.Job.ASNArtID;
                                    asn.ASNArt.Fill();

                                    //hier werden die Meldungen (XML oder VDA) eingelesen
                                    //Die Unterscheidung findet in der Funktion statt
                                    if (this.Jobs.ASNFileTyp.Equals(constValue_AsnArt.const_Art_EdifactVDA4984))
                                    {
                                        if (ediHelper_VDA4984_CheckProcessableASN.IsASNFileProcessable(asn.Job, strSourceFilePathAndName))
                                        {
                                            asn.ReadVDAorXML(strSourceFilePathAndName);
                                        }
                                        else
                                        {
                                            clsLogbuchCon tmpLog = new clsLogbuchCon();
                                            tmpLog.LogText = "Datei: [ " + strSourceFilePathAndName + " ] ist nicht für den Empfänger bestimmt und wird nicht verarbeiten!!!";
                                            if (asn.ListError is null)
                                            {
                                                asn.ListError = new List<clsLogbuchCon>();
                                            }
                                            asn.ListError.Add(tmpLog);
                                            asn.IsTransferFile = false;
                                        }
                                    }
                                    else if (this.Jobs.ASNFileTyp.Equals(constValue_AsnArt.const_Art_EDIFACT_DELFOR_D97A))
                                    {
                                        if (ediHelper_EdiDelfor_CheckProcessableASN.IsASNFileProcessable(asn.Job, strSourceFilePathAndName))
                                        {
                                            asn.ReadVDAorXML(strSourceFilePathAndName);
                                        }
                                        else
                                        {
                                            clsLogbuchCon tmpLog = new clsLogbuchCon();
                                            tmpLog.LogText = "Datei: [ " + strSourceFilePathAndName + " ] ist nicht für den Empfänger bestimmt und wird nicht verarbeiten!!!";
                                            if (asn.ListError is null)
                                            {
                                                asn.ListError = new List<clsLogbuchCon>();
                                            }
                                            asn.ListError.Add(tmpLog);
                                            asn.IsTransferFile = false;
                                        }
                                    }
                                    else if (this.Jobs.ASNFileTyp.Equals(constValue_AsnArt.const_Art_EDIFACT_Qality_D96A))
                                    {
                                        //asn.ReadVDAorXML(strSourceFilePathAndName);
                                        if (ediHelper_EdiQuality_CheckProcessableASN.IsASNFileProcessable(asn.Job, strSourceFilePathAndName))
                                        {
                                            asn.ReadVDAorXML(strSourceFilePathAndName);
                                        }
                                        else
                                        {
                                            clsLogbuchCon tmpLog = new clsLogbuchCon();
                                            tmpLog.LogText = "Datei: [ " + strSourceFilePathAndName + " ] ist nicht für den Empfänger bestimmt und wird nicht verarbeiten!!!";
                                            if (asn.ListError is null)
                                            {
                                                asn.ListError = new List<clsLogbuchCon>();
                                            }
                                            asn.ListError.Add(tmpLog);
                                            asn.IsTransferFile = false;
                                        }
                                    }
                                    else if (this.Jobs.ASNFileTyp.Equals(constValue_AsnArt.const_Art_XML_ZQM_QALITY02))
                                    {
                                        if (ediHelper_EdiXML_ZQMQALITY02_CheckProcessable.IsXMLFileProcessable(asn.Job, strSourceFilePathAndName))
                                        {
                                            asn.ReadVDAorXML(strSourceFilePathAndName);
                                        }
                                        else
                                        {
                                            clsLogbuchCon tmpLog = new clsLogbuchCon();
                                            tmpLog.LogText = "Datei: [ " + strSourceFilePathAndName + " ] kann nicht zugewiesen und verarbeiten!!!";
                                            if (asn.ListError is null)
                                            {
                                                asn.ListError = new List<clsLogbuchCon>();
                                            }
                                            asn.ListError.Add(tmpLog);
                                            asn.IsTransferFile = false;
                                        }
                                    }
                                    else if (this.Jobs.ASNFileTyp.Equals(constValue_AsnArt.const_Art_EDIFACT_ASN_D96A))
                                    {
                                        //asn.ReadVDAorXML(strSourceFilePathAndName);
                                        //if (ediHelper_EdiEDIFACT_ASN_D96A_CheckProcessableASN.IsASNFileProcessable(asn.Job, strSourceFilePathAndName))
                                        //{
                                        //    asn.ReadVDAorXML(strSourceFilePathAndName);
                                        //}
                                        if (ediHelper_EdiEDIFACT_ASN_CheckProcessableASN.IsASNFileProcessable(asn.Job, strSourceFilePathAndName))
                                        {
                                            asn.ReadVDAorXML(strSourceFilePathAndName);
                                        }
                                        else
                                        {
                                            clsLogbuchCon tmpLog = new clsLogbuchCon();
                                            tmpLog.LogText = "Datei: [ " + strSourceFilePathAndName + " ] kann keiner Adresse zugeordnet werden und wird nicht verarbeiten!!!";
                                            if (asn.ListError is null)
                                            {
                                                asn.ListError = new List<clsLogbuchCon>();
                                            }
                                            asn.ListError.Add(tmpLog);
                                            asn.IsTransferFile = false;
                                        }
                                    }
                                    else if (this.Jobs.ASNFileTyp.Equals(constValue_AsnArt.const_Art_EDIFACT_DESADV_D07A))
                                    {
                                        //asn.ReadVDAorXML(strSourceFilePathAndName);
                                        if (ediHelper_EdiEDIFACT_ASN_D07A_CheckProcessableASN.IsASNFileProcessable(asn.Job, strSourceFilePathAndName))
                                        {
                                            asn.ReadVDAorXML(strSourceFilePathAndName);
                                        }
                                        else
                                        {
                                            clsLogbuchCon tmpLog = new clsLogbuchCon();
                                            tmpLog.LogText = "Datei: [ " + strSourceFilePathAndName + " ] kann keiner Adresse zugeordnet werden und wird nicht verarbeiten!!!";
                                            if (asn.ListError is null)
                                            {
                                                asn.ListError = new List<clsLogbuchCon>();
                                            }
                                            asn.ListError.Add(tmpLog);
                                            asn.IsTransferFile = false;
                                        }
                                    }
                                    else
                                    {
                                        asn.ReadVDAorXML(strSourceFilePathAndName);
                                    }

                                    string sourceFile = Path.GetFileName(strSourceFilePathAndName);
                                    string destFile = this.GLSystem.VE_OdettePath + this.Jobs.ASNFileStorePath;
                                    string ErrorFile = this.GLSystem.VE_OdettePath + this.Jobs.ErrorPath;
                                    string CheckTransferFile = this.GLSystem.VE_OdettePath + this.Jobs.CheckTransferPath;

                                    //Check Directory Transfer und Error
                                    FunctionsCom.CheckDirectory(destFile);
                                    FunctionsCom.CheckDirectory(ErrorFile);
                                    FunctionsCom.CheckDirectory(CheckTransferFile);

                                    //check Error
                                    if (asn.ListError.Count > 0)
                                    {
                                        for (Int32 l = 0; l <= asn.ListError.Count - 1; l++)
                                        {
                                            clsLogbuchCon tmpLog = (clsLogbuchCon)asn.ListError[l];
                                            strTmp = string.Empty;
                                            strTmp = GetLogString(strTmp, strProzessName + " - " + tmpLog.LogText, false);
                                            listLogToFileASNRead.Add(strTmp);
                                            SetInfoInInfoBox2(strTmp);
                                        }
                                        //checkTransferFile
                                        if (asn.IsTransferFile)
                                        {
                                            strTmp = string.Empty;
                                            if (this.system.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_SZG + "_"))
                                            {
                                                //Damit strFileEnd auch eindeutig wird
                                                Thread.Sleep(1000);
                                                //umbennennen für BMW
                                                if (!this.Jobs.TransferFileName.Equals(string.Empty))
                                                {
                                                    string strFileEnd = string.Empty;
                                                    string strDateTimeEnd = string.Empty;
                                                    strFileEnd = "~" + DateTime.Now.ToString("HH") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");
                                                    string strTmpFileName = this.Jobs.TransferFileName + strFileEnd + strDateTimeEnd;
                                                    CheckTransferFile = CheckTransferFile + "\\" + strTmpFileName;
                                                }
                                                else
                                                {
                                                    CheckTransferFile = CheckTransferFile + "\\" + this.Jobs.FileName;
                                                }
                                            }
                                            else
                                            {
                                                CheckTransferFile = CheckTransferFile + "\\" + this.Jobs.FileName;
                                            }// check Client SZG

                                            string strTmpError = string.Empty;
                                            if (!FunctionsCom.MoveAndDeleteFile(CheckTransferFile, strSourceFilePathAndName, CheckTransferFile, ref strTmpError))
                                            {
                                                strTmp = strProzessName + " - " + strTmpError;

                                                clsError Error = new clsError();
                                                Error.InitClass(this.GLUser, this.system);
                                                Error.Aktion = strProzessName + " - FunctionsCom.MoveAndDeleteFile";
                                                Error.Datum = DateTime.Now;
                                                Error.ErrorText = string.Empty;
                                                Error.exceptText = strTmp;
                                                Error.WriteError();
                                            }
                                            else
                                            {
                                                strTmp = GetLogString(strTmp, strProzessName + " - " + "Datei: [" + strSourceFilePathAndName + "] wird  in Ordner [" + CheckTransferFile + "] verschoben", false);
                                                clsError.SendMovementInfoMail(this.GLUser, this.system, asn, CheckTransferFile);
                                            }
                                        }
                                        else
                                        {
                                            destFile = ErrorFile + "\\" + sourceFile + "#" + DateTime.Now.ToString("yyyy_MM_dd") + "_" + DateTime.Now.ToString("HH") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");
                                            ErrorFile = ErrorFile + "\\" + sourceFile + "#" + DateTime.Now.ToString("yyyy_MM_dd") + "_" + DateTime.Now.ToString("HH") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");

                                            string strTmpError = string.Empty;
                                            if (!FunctionsCom.MoveAndDeleteFile(destFile, strSourceFilePathAndName, ErrorFile, ref strTmpError))
                                            {
                                                strTmp = strProzessName + " - " + strTmpError;

                                                clsError Error = new clsError();
                                                Error.InitClass(this.GLUser, this.system);
                                                Error.Aktion = strProzessName + " - FunctionsCom.MoveAndDeleteFile";
                                                Error.Datum = DateTime.Now;
                                                Error.ErrorText = string.Empty;
                                                Error.exceptText = strTmp;
                                                Error.WriteError();
                                            }
                                            else
                                            {
                                                strTmp = GetLogString(strTmp, strProzessName + " - " + "Datei: [" + strSourceFilePathAndName + "] wird  in Ordner [" + ErrorFile + "] verschoben", false);
                                                //clsError.SendMovementInfoMail(this.GLUser, this.system, asn, ErrorFile);
                                            }
                                        }//CheckTransferFile

                                        listLogToFileASNRead.Add(strTmp);
                                        SetInfoInInfoBox2(strTmp);
                                    }
                                    else
                                    {
                                        //-- der unbenannte Dateinnamen, unter dem die Datei im Transferorder gespeichert werden soll
                                        //--- steht nun in asn.Filename nach dem asn.Add()
                                        destFile = destFile + "\\" + asn.FileName;
                                        ErrorFile = ErrorFile + "\\" + sourceFile;

                                        string strTmpError = string.Empty;
                                        strTmp = string.Empty;

                                        if (!FunctionsCom.MoveAndDeleteFile(destFile, strSourceFilePathAndName, ErrorFile, ref strTmpError))
                                        {
                                            strTmp = GetLogString(strTmp, strProzessName + " - " + strTmpError, false);

                                            clsError Error = new clsError();
                                            Error.InitClass(this.GLUser, this.system);
                                            Error.Aktion = strProzessName + " - FunctionsCom.MoveAndDeleteFile";
                                            Error.Datum = DateTime.Now;
                                            Error.ErrorText = string.Empty;
                                            Error.exceptText = log.LogText;
                                            Error.WriteError();
                                        }
                                        else
                                        {
                                            strTmp = GetLogString(strTmp, strProzessName + " - " + "Datei: [" + destFile + "] verarbeitet", false);
                                        }
                                        //InitThreadProzesse();
                                        iCountToBreakWhile = 1;
                                        listLogToFileASNRead.Add(strTmp);
                                        SetInfoInInfoBox2(strTmp);
                                    }// check Error
                                }
                                else
                                {
                                    //Datei gelockt
                                    strTmp = string.Empty;
                                    strTmp = GetLogString(LogText, strProzessName + " - " + "Datei: [" + strSourceFilePathAndName + "] konnte nicht verarbeitet werden", false);
                                    listLogToFileASNRead.Add(strTmp);
                                    SetInfoInInfoBox2(strTmp);
                                }
                            }
                            Thread.Sleep(500);
                        }
                    }
                }
                else
                {
                    LogText = GetLogString(LogText, strProzessName + " - Es liegt nichts an.....", true);
                    SetInfoInInfoBox2(strProzessName + " - Es liegt nichts an.....");
                    strTmp = string.Empty;
                    strTmp = GetLogString(strTmp, strProzessName + " - Es liegt nichts an.....", false);
                    listLogToFileASNRead.Add(strTmp);
                }
            }
            catch (Exception ex)
            {
                string s = ex.ToString();

                clsError Error = new clsError();
                Error.Sys = this.systemLVS;
                Error._GL_User = this.GLUser;
                Error.Sys = this.system;
                Error.Aktion = "frmMainCom - Task_VDAread() ";
                Error.Datum = DateTime.Now;
                Error.ErrorText = string.Empty;
                Error.exceptText = ex.ToString();
                Error.WriteError();

                strTmp = string.Empty;
                strTmp = GetLogString(strTmp, strProzessName + " - FEHLER -> " + ex.ToString(), false);
                listLogToFileASNRead.Add(strTmp);
                SetInfoInInfoBox2(strTmp);
            }
            finally
            {
                strTmp = string.Empty;
                strTmp = GetLogString(strTmp, strProzessName + " - Prozesse abgeschlossen...", false);
                listLogToFileASNRead.Add(strTmp);
                SetInfoInInfoBox2(strTmp);

                if (this.system.Client.Modul.ASN_Create_Auto)
                {
                    Task_AutoCreateEA();
                }
            }
            this.Invoke(new EventHandler(m_StartTimerRead));
            bThreadASNread = false;
            clsLogbuchCon.WriteLogToFile(ref listLogToFileASNRead, this.system, Application.StartupPath, clsLogbuchCon.const_Task_ASNRead);
        }
        ///<summary>frmMainCom / Task_VDAwrite</summary>
        ///<remarks>Vorgehen: 
        ///         1. Jobs aus der dtQueue 
        ///         2. Wenn die Table leer ist, dann wird ein Timer gesetzt.
        ///         2. Ermitteln aller notwendigen Daten aus der Sped4-Datenbank
        ///         3. ASN Datei (XML oder VDA-Textdatei) erstellen</remarks>
        private void Task_ASNwrite()
        {
            string strTmp = string.Empty;

            List<string> listFileToTransfer = new List<string>();
            //SetInfoInInfobox(clsSystem.const_Default_TaskSeparator.ToString());
            string strProzessName = "[ASNwrite]";
            listLogToFileASNWrite = new List<string>();
            listLogToFileASNWrite.Add(clsLogbuchCon.const_Logbuch_Trennzeichen);

            strTmp = string.Empty;
            strTmp = GetLogString(strTmp, strProzessName + " - Task gestartet", false);
            listLogToFileASNWrite.Add(strTmp);
            SetInfoInInfoBox2(strTmp);

            String LogText = String.Empty;
            decimal decTmp = 0;
            try
            {
                DataTable dtQueue = Queue.GetQueue(true, this.system.VE_TaskWriteSingleModus);
                Int32 iValMax = dtQueue.Rows.Count;
                LogText = GetLogString(LogText, strProzessName + " - Anzahl zu erstellende ASN-Meldungen: " + iValMax.ToString(), true);

                strTmp = string.Empty;
                strTmp = GetLogString(strTmp, strProzessName + " - Anzahl zu erstellende ASN-Meldungen: " + iValMax.ToString(), false);
                listLogToFileASNWrite.Add(strTmp);
                SetInfoInInfoBox2(strTmp);

                if (dtQueue.Rows.Count > 0)
                {
                    string ASNTypString = string.Empty;
                    for (Int32 i = 0; i <= dtQueue.Rows.Count - 1; i++)
                    {
                        decTmp = 0;
                        Decimal.TryParse(dtQueue.Rows[i]["ID"].ToString(), out decTmp);
                        if (decTmp > 0)
                        {
                            clsASN tmpASN = new clsASN();
                            tmpASN.InitClass(this.GLSystem, this.GLUser);
                            tmpASN.Sys = this.system;
                            tmpASN.Queue.ID = decTmp;
                            tmpASN.Queue.Fill();
                            tmpASN.ASNTypID = tmpASN.Queue.ASNTypID;
                            tmpASN.Prozess = strProzessName;

                            //---- Test mr 12.01.2024
                            if (tmpASN.Queue.ASNID > 0)
                            {
                                tmpASN.ID = tmpASN.Queue.ASNID;
                                tmpASN.Fill();
                            }

                            if ((tmpASN.Queue.ASNArt is clsASNArt) && (tmpASN.Queue.ASNArtId > 0))
                            {
                                tmpASN.ASNArt = tmpASN.Queue.ASNArt;
                                ASNTypString = tmpASN.ASNArt.Typ;
                                tmpASN.ASNFileTyp = ASNTypString;
                            }
                            if (tmpASN.ASNFileTyp == null)
                            {
                                ASNTypString = clsJobs.GetASNFileTypByAdress(this.GLUser, tmpASN.Queue.AdrVerweisID, "OUT");
                                tmpASN.ASNFileTyp = ASNTypString;
                                //tmpASN.ASNNR = clsJobs.GetASNArtIDByAdress(this.GLUser, tmpASN.Queue.AdrVerweisID);
                            }

                            // VDA 4913 / XML Datei wird erzeugt
                            tmpASN.WriteVDAorXML();

                            //check Error
                            if (tmpASN.ListError.Count > 0)
                            {
                                for (Int32 l = 0; l <= tmpASN.ListError.Count - 1; l++)
                                {
                                    clsLogbuchCon tmpLog = (clsLogbuchCon)tmpASN.ListError[l];
                                    strTmp = string.Empty;
                                    strTmp = GetLogString(strTmp, tmpLog.LogText, false);
                                    listLogToFileASNWrite.Add(strTmp);
                                    SetInfoInInfoBox2(strTmp);
                                }
                            }
                            else
                            {
                                if (
                                    (tmpASN.tmpWriteFile != string.Empty) |
                                    (tmpASN != null)
                                    )
                                {
                                    //Liste mit den Dateiname der erzeugten Meldungen
                                    listFileToTransfer.Add(tmpASN.tmpWriteFile);

                                    //--- für TEST Speicherung auskommentieren
                                    tmpASN.Queue.UpdateActivToFalse();

                                    //Eintrag in Lagermeldungen
                                    switch (tmpASN.Queue.TableName)
                                    {
                                        case "LEingang":
                                            if (tmpASN.Queue.TableID > 0)
                                            {
                                                List<string> listArtikelInEingang = clsArtikel.GetArtikelByLEingangTableID(this.GLUser.User_ID, tmpASN.Queue.TableID);
                                                if (listArtikelInEingang.Count > 0)
                                                {
                                                    //Lagermeldungen für jeden Artikel in DB eintragen
                                                    clsLagerMeldungen LMInsert = new clsLagerMeldungen();
                                                    LMInsert.GL_User = this.GLUser;
                                                    LMInsert.ASNTypID = tmpASN.Queue.ASNTypID;
                                                    LMInsert.ArtikelID = 0;
                                                    LMInsert.ASNID = tmpASN.ID;
                                                    LMInsert.FileName = tmpASN.tmpStorFilePath;
                                                    LMInsert.Sender = 0;
                                                    LMInsert.Receiver = tmpASN.Queue.AdrVerweisID;
                                                    LMInsert.ASNAction = tmpASN.Queue.ASNAction;
                                                    LMInsert.AddArtikelList(listArtikelInEingang, tmpASN.Queue.ASNTyp.Typ);
                                                }
                                            }
                                            break;

                                        case "LAusgang":
                                            ASNTypString = tmpASN.ASNFileTyp;
                                            List<string> listArtikelInAusgang = clsArtikel.GetArtikelByLAusgangTableID(this.GLUser.User_ID, tmpASN.Queue.TableID);
                                            if (listArtikelInAusgang.Count > 0)
                                            {
                                                //Lagermeldungen für jeden Artikel in DB eintragen
                                                clsLagerMeldungen LMInsert = new clsLagerMeldungen();
                                                LMInsert.GL_User = this.GLUser;
                                                LMInsert.ASNTypID = tmpASN.Queue.ASNTypID;
                                                LMInsert.ArtikelID = 0;
                                                LMInsert.ASNID = tmpASN.ID;
                                                LMInsert.FileName = tmpASN.tmpStorFilePath;
                                                LMInsert.Sender = 0;
                                                LMInsert.Receiver = tmpASN.Queue.AdrVerweisID;
                                                LMInsert.ASNAction = tmpASN.Queue.ASNAction;
                                                LMInsert.AddArtikelList(listArtikelInAusgang, tmpASN.Queue.ASNTyp.Typ);
                                            }
                                            break;
                                        case "Artikel":
                                            ASNTypString = tmpASN.ASNFileTyp;
                                            List<string> listArtikel = new List<string>();
                                            listArtikel.Add(tmpASN.Queue.TableID.ToString());
                                            if (listArtikel.Count > 0)
                                            {
                                                //Lagermeldungen für jeden Artikel in DB eintragen
                                                clsLagerMeldungen LMInsert = new clsLagerMeldungen();
                                                LMInsert.GL_User = this.GLUser;
                                                LMInsert.ASNTypID = tmpASN.Queue.ASNTypID;
                                                LMInsert.ArtikelID = tmpASN.Queue.TableID;
                                                LMInsert.ASNID = tmpASN.ID;
                                                LMInsert.FileName = tmpASN.tmpStorFilePath;
                                                LMInsert.Sender = 0;
                                                LMInsert.Receiver = tmpASN.Queue.AdrVerweisID;
                                                LMInsert.ASNAction = tmpASN.Queue.ASNAction;
                                                LMInsert.AddArtikelList(listArtikel, tmpASN.Queue.ASNTyp.Typ);
                                            }
                                            break;
                                    }
                                }
                                //SetInfoInInfobox("[ASNwrite].[" + ASNTypString + "]  - " + tmpASN.Queue.TableName + " [" + tmpASN.Queue.TableID.ToString() + "] -> File erstellt: [" + tmpASN.tmpWriteFile + "]");
                                strTmp = string.Empty;
                                strProzessName = "[ASNwrite]";
                                strProzessName = strProzessName + ".[" + ASNTypString + "].[" + dtQueue.Rows[i]["ASNTyp"].ToString() + "]";
                                if (!tmpASN.Job.ViewProzessName.Equals(string.Empty))
                                {
                                    strProzessName = strProzessName + " - [" + tmpASN.Job.ViewProzessName + "]";
                                }
                                strTmp = strProzessName + " - " + tmpASN.Queue.TableName + " [" + tmpASN.Queue.TableID.ToString() + "]";

                                if (tmpASN.Queue.IsVirtFile)
                                {
                                    strTmp = GetLogString(strTmp, strTmp + "-> virt. File erstellt: [" + tmpASN.tmpWriteFile + "]", false);
                                    //strTmp = GetLogString(strTmp, strProzessName + ".[" + ASNTypString + "].[" + dtQueue.Rows[i]["ASNTyp"].ToString() + "] - " + tmpASN.Queue.TableName + " [" + tmpASN.Queue.TableID.ToString() + "] -> virt. File erstellt: [" + tmpASN.tmpWriteFile + "]", false);
                                }
                                else
                                {
                                    strTmp = GetLogString(strTmp, strTmp + "-> File erstellt: [" + tmpASN.tmpWriteFile + "]", false);
                                    //strTmp = GetLogString(strTmp, strProzessName + ".[" + ASNTypString + "].[" + dtQueue.Rows[i]["ASNTyp"].ToString() + "] - " + tmpASN.Queue.TableName + " [" + tmpASN.Queue.TableID.ToString() + "] -> File erstellt: [" + tmpASN.tmpWriteFile + "]", false);
                                }
                                listLogToFileASNWrite.Add(strTmp);
                                SetInfoInInfoBox2(strTmp);


                                //versenden der erzeugten Dateien
                                if (tmpASN.Job.PostBy == clsJobs.const_PostBy_FTP.ToString() || tmpASN.Job.PostBy == clsJobs.const_PostBy_SFTP.ToString())
                                {
                                    if (tmpASN.Job.PostBy == clsJobs.const_PostBy_FTP.ToString())
                                    {
                                        tmpASN.Job.FtpOrSftp = new clsftp();

                                    }
                                    if (tmpASN.Job.PostBy == clsJobs.const_PostBy_SFTP.ToString())
                                    {
                                        tmpASN.Job.FtpOrSftp = new clsSFTP();
                                    }
                                    tmpASN.Job.FtpOrSftp.InitClass(tmpASN.Job, null);
                                    Task_FTPupload(ref tmpASN.Job, strProzessName);
                                }
                                tmpASN = null;
                            }
                        }
                        if (this.cbQueueTopSelected.Checked)
                        {
                            i = dtQueue.Rows.Count;
                        }
                    }//for
                }
                else
                {
                    strTmp = string.Empty;
                    strTmp = GetLogString(strTmp, strProzessName + " - es liegt nichts an....", false);
                    listLogToFileASNWrite.Add(strTmp);
                }
            }
            catch (Exception ex)
            {
                string st = ex.ToString();

                clsError Error = new clsError();
                Error.Sys = this.systemLVS;
                Error._GL_User = this.GLUser;
                Error.Aktion = "frmMainCom - Task_VDAwrite() ";
                Error.Datum = DateTime.Now;
                Error.ErrorText = string.Empty;
                Error.exceptText = ex.ToString();
                Error.WriteError();

                strTmp = string.Empty;
                strTmp = GetLogString(strTmp, Log.LogText, false);
                listLogToFileASNWrite.Add(strTmp);
            }
            finally
            {
                strTmp = string.Empty;
                strTmp = GetLogString(strTmp, strProzessName + " - Prozesse abgeschlossen...", false);
                listLogToFileASNWrite.Add(strTmp);
            }
            this.Invoke(new EventHandler(m_StartTimerWrite));
            bThreadASNwrite = false;
            clsLogbuchCon.WriteLogToFile(ref listLogToFileASNWrite, this.system, Application.StartupPath, clsLogbuchCon.const_Task_ASNWrite);
        }
        ///<summary>frmMainCom / Task_CronJobs</summary>
        ///<remarks></remarks>
        private void Task_CronJobs()
        {
            //Init Var/Classes
            ListLogAddString = new List<string>();

            //clsCronJobs CronJob = new clsCronJobs();
            //CronJob.InitClass(this.GLSystem, this.GLUser, this.system);

            CronJobViewData cronJobViewData = new CronJobViewData();
            //SetInfoInInfobox(clsSystem.const_Default_TaskSeparator.ToString());
            string strProzessName = "[CronJobs]";

            string strTmp = string.Empty;
            strTmp = GetLogString(strTmp, strProzessName + " - Task gestartet...", false);
            listLogToCronJob.Add(strTmp);
            SetInfoInInfoBox2(strTmp);

            Log = new clsLogbuchCon();
            Log.GL_User = this.GLUser;
            Log.Typ = enumLogArtItem.Start.ToString();
            Log.LogText = strTmp;
            Log.TableName = string.Empty;
            decimal decTmp = 0;
            Log.TableID = decTmp;
            ListLogAddString.Add(Log.GetAddLogbuchSQLString());
            //SetInfoInInfobox(Log.LogText);
            String LogText = String.Empty;

            try
            {
                //Int32 iValMax = CronJob.dtCronJobs.Rows.Count;
                Int32 iValMax = cronJobViewData.ListCronJobs.Count;
                strTmp = strProzessName + "Anzahl abzuarbeitender Cron Jobs: " + iValMax.ToString();
                strTmp = GetLogString(strTmp, strTmp, false);
                listLogToCronJob.Add(strTmp);
                SetInfoInInfoBox2(strTmp);
                LogText = GetLogString(LogText, strTmp, true);

                if (iValMax > 0)
                {
                    string ASNTypString = string.Empty;
                    foreach (CronJobs cronJob in cronJobViewData.ListCronJobs)
                    {
                        //bool bCronJobExcecuted = false;
                        Thread.Sleep(1000);
                        strTmp = strProzessName + ".[" + cronJob.Aktion + "].[" + cronJob.Id + " - " + cronJob.Beschreibung + "] - wird durchgeführt...";
                        strTmp = GetLogString(strTmp, strTmp, false);
                        listLogToCronJob.Add(strTmp);
                        SetInfoInInfoBox2(strTmp);
                        LogText = GetLogString(LogText, strTmp, true);

                        switch (cronJob.Aktion)
                        {
                            //case "CreateAutoRGLager":
                            case enumCronJobAction.CreateAutoRGLager:
                                //if (!system.DebugModeCOM)
                                //{
                                //    CronJob.DoAutoCalculation();
                                //}
                                break;

                            //case "JournaleAutoSend":
                            case enumCronJobAction.JournaleAutoSend:
                                //if (!system.DebugModeCOM)
                                //{
                                //    CronJob.DoAutoJournal();
                                //}
                                break;

                            //case "BestandslisteAutoSend":
                            case enumCronJobAction.BestandslisteAutoSend:
                                //if (!system.DebugModeCOM)
                                //{
                                //    CronJobViewData tmpCronJobVD = new CronJobViewData(cronJob, 1);
                                //    CronJob_StoclistAutoSend stoclistAutoSend = new CronJob_StoclistAutoSend(tmpCronJobVD.CronJob, this.GLUser, this.GLSystem, this.system);
                                //    if (stoclistAutoSend.ProzessExcecuted)
                                //    {
                                //        tmpCronJobVD.UpdateNextActiondate();
                                //    }
                                //    if (stoclistAutoSend.ListLogInsert.Count > 0)
                                //    {
                                //        foreach (clsLogbuchCon item in stoclistAutoSend.ListLogInsert)
                                //        {
                                //            strTmp = item.LogText;
                                //            listLogToCronJob.Add(strTmp);
                                //            SetInfoInInfoBox2(strTmp);
                                //        }
                                //    }
                                //}
                                //else
                                //{
                                //    //CronJobViewData tmpCronJobVD = new CronJobViewData(cronJob, 1);
                                //    //CronJob_StoclistAutoSend stoclistAutoSend = new CronJob_StoclistAutoSend(tmpCronJobVD.CronJob, this.GLUser, this.GLSystem, this.system);
                                //    //if (stoclistAutoSend.ProzessExcecuted)
                                //    //{
                                //    //    tmpCronJobVD.UpdateNextActiondate();
                                //    //}
                                //    //if (stoclistAutoSend.ListLogInsert.Count > 0)
                                //    //{
                                //    //    foreach (clsLogbuchCon item in stoclistAutoSend.ListLogInsert)
                                //    //    {
                                //    //        strTmp = item.LogText;
                                //    //        listLogToCronJob.Add(strTmp);
                                //    //        SetInfoInInfoBox2(strTmp);
                                //    //    }
                                //    //}
                                //}
                                CronJobViewData tmpCronJobVD = new CronJobViewData(cronJob, 1);
                                CronJob_StoclistAutoSend stoclistAutoSend = new CronJob_StoclistAutoSend(tmpCronJobVD.CronJob, this.GLUser, this.GLSystem, this.system);
                                if (stoclistAutoSend.ProzessExcecuted)
                                {
                                    if (!system.DebugModeCOM)
                                    {
                                        tmpCronJobVD.UpdateNextActiondate();
                                    }
                                }
                                if (stoclistAutoSend.ListLogInsert.Count > 0)
                                {
                                    foreach (clsLogbuchCon item in stoclistAutoSend.ListLogInsert)
                                    {
                                        strTmp = item.LogText;
                                        listLogToCronJob.Add(strTmp);
                                        SetInfoInInfoBox2(strTmp);
                                    }
                                }

                                break;

                            //case "GEWStatistikAutoSend":
                            case enumCronJobAction.GEWStatistikAutoSend:
                                //if (!system.DebugModeCOM)
                                //{
                                //    CronJob.DoAutoGewBestand();
                                //}
                                //CronJob.DoAutoGewBestand();
                                break;

                            //case "DispoAutoSend":
                            case enumCronJobAction.DispoAutoSend:
                                //if (!system.DebugModeCOM)
                                //{
                                //    CronJob.DoAutoSendToDispo();
                                //}
                                break;

                            case enumCronJobAction.BestandsmeldungVDA4913:
                                //---Ermittelt alle notwendingen Jobs für diesen Zeitpunkt
                                if (!system.DebugModeCOM)
                                {
                                    List<clsJobs> tmpJob = new List<clsJobs>();
                                    tmpJob = clsJobs.GetJobByActionDateAndDirection(this.GLSystem, this.GLUser, Directions.OUT.ToString());

                                    strTmp = strProzessName + ".[" + cronJob.Aktion + "] - Anzahl:" + tmpJob.Count.ToString();
                                    strTmp = GetLogString(strTmp, strTmp, false);
                                    listLogToCronJob.Add(strTmp);
                                    SetInfoInInfoBox2(strTmp);

                                    foreach (clsJobs itmJob in tmpJob)
                                    {
                                        this.system.AbBereich = new clsArbeitsbereiche();
                                        this.system.AbBereich.InitCls(this.GLUser, itmJob.ArbeitsbereichID);


                                        clsASN tmpASN = new clsASN();
                                        tmpASN.InitClass(this.GLSystem, this.GLUser);
                                        tmpASN.Sys = this.system;
                                        tmpASN.Queue = new clsQueue();
                                        tmpASN.Prozess = strProzessName;
                                        tmpASN.Job = itmJob;

                                        //Log = new clsLogbuchCon();
                                        if (tmpASN.Job.ID > 0)
                                        {
                                            tmpASN.ASNFileTyp = tmpASN.Job.ASNFileTyp;
                                            tmpASN.ASNArt = new clsASNArt();
                                            tmpASN.ASNArt.ID = tmpASN.Job.ASNArtID;
                                            tmpASN.ASNArt.Fill();
                                            tmpASN.ASNNR = 0;
                                            tmpASN.ASNTyp = tmpASN.Job.AsnTyp;

                                            tmpASN.WriteVDAorXML();

                                            //---Fehlermeldung
                                            if (tmpASN.ListError.Count > 0)
                                            {
                                                strTmp = strProzessName + ".[" + cronJob.Aktion + "].[" + tmpASN.Job.ADR.ViewID + "] -> File konnte nicht erstellt werden / ErrorMail wurde versendet!!!";
                                            }
                                            else
                                            {
                                                strTmp = strProzessName + ".[" + cronJob.Aktion + "].[" + tmpASN.Job.ADR.ViewID + "] -> File erstellt: [" + tmpASN.tmpWriteFile + "]";
                                                //bCronJobExcecuted = true;
                                            }
                                            strTmp = GetLogString(strTmp, strTmp, false);
                                        }
                                        else
                                        {
                                            strTmp = strProzessName + ".[" + cronJob.Aktion + "].[" + tmpASN.Job.ADR.ViewID + "] --> File NICHT erstellt => Job.ID=0  !!!";
                                            strTmp = GetLogString(strTmp, strTmp, false);

                                        }
                                        Log.LogText = strTmp;

                                        listLogToCronJob.Add(strTmp);
                                        clsLogbuchCon.WriteLogToFile(ref this.listLogToCronJob, this.system, Application.StartupPath, clsLogbuchCon.const_Task_CronJob);
                                        SetInfoInInfoBox2(strTmp);

                                        //ListLogAddString.Add(Log.GetAddLogbuchSQLString());
                                        LogText = GetLogString(LogText, Log.LogText, true);
                                        SetInfoInInfoBox2(Log.LogText);
                                    }
                                }
                                break;
                            case enumCronJobAction.CleanUpEdiMessages:
                                tmpCronJobVD = new CronJobViewData(cronJob, 1);
                                CronJob_CleanUpEdiMessages clean = new CronJob_CleanUpEdiMessages(this.system.DebugModeCOM);
                                clean.StartCleaning(null, true);
                                if (clean.LogList.Count > 0)
                                {
                                    foreach (string s in clean.LogList)
                                    {
                                        listLogToCronJob.Add(s);
                                        SetInfoInInfoBox2(s);
                                    }
                                }
                                if (!system.DebugModeCOM)
                                {
                                    tmpCronJobVD.UpdateNextActiondate();
                                }
                                //tmpCronJobVD.UpdateNextActiondate();
                                break;

                            case enumCronJobAction.Default:

                                break;
                        }
                    }
                }
                else
                {
                    strTmp = strProzessName + "es liegt nichts an....";
                    strTmp = GetLogString(strTmp, strTmp, false);
                    listLogToCronJob.Add(strTmp);
                    SetInfoInInfoBox2(strTmp);

                    Log = new clsLogbuchCon();
                    Log.GL_User = this.GLUser;
                    Log.Typ = enumLogArtItem.autoMail.ToString();
                    Log.LogText = strTmp;
                    Log.TableName = string.Empty;
                    Log.TableID = 0;
                    ListLogAddString.Add(Log.GetAddLogbuchSQLString());
                }
            }
            catch (Exception ex)
            {
                string st = ex.ToString();

                clsError Error = new clsError();
                Error.Sys = this.systemLVS;
                Error._GL_User = this.GLUser;
                Error.Aktion = "frmMainCom - Task_CronJobs() ";
                Error.Datum = DateTime.Now;
                Error.ErrorText = string.Empty;
                Error.exceptText = ex.ToString();
                Error.WriteError();

                strTmp = strProzessName + ".[ERROR] --> Error - Mail versendet...";
                strTmp = GetLogString(strTmp, strTmp, false);
                listLogToCronJob.Add(strTmp);
                SetInfoInInfoBox2(strTmp);

                Log = new clsLogbuchCon();
                Log.GL_User = this.GLUser;
                Log.Typ = enumLogArtItem.Start.ToString();
                Log.LogText = strTmp;
                Log.TableName = string.Empty;
                decTmp = 0;
                Log.TableID = decTmp;
                ListLogAddString.Add(Log.GetAddLogbuchSQLString());
            }
            finally
            {
                Thread.Sleep(1000);

                strTmp = strProzessName + "Prozesse abgeschlossen...";
                strTmp = GetLogString(strTmp, strTmp, false);
                listLogToCronJob.Add(strTmp);
                clsLogbuchCon.WriteLogToFile(ref this.listLogToCronJob, this.system, Application.StartupPath, clsLogbuchCon.const_Task_CronJob);
                SetInfoInInfoBox2(strTmp);

                Log = new clsLogbuchCon();
                Log.GL_User = this.GLUser;
                Log.Typ = enumLogArtItem.Stop.ToString();
                Log.LogText = strTmp;
                Log.TableName = string.Empty;
                Log.TableID = 0;
                ListLogAddString.Add(Log.GetAddLogbuchSQLString());
                LogText = GetLogString(LogText, Log.LogText, true);
                SetInfoInInfoBox2(Log.LogText);
                //clsLogbuchCon.AddCollectedSQLStringToLog(ListLogAddString, this.GLUser);
            }
            this.Invoke(new EventHandler(m_StartTimerCronJob));
            bThreadCronjobs = false;
        }
        ///<summary>frmMainCom / Task_AutoCreateEA</summary>
        ///<remarks></remarks>
        private void Task_AutoCreateEA()
        {
            //init Var/Classes
            ListLogAddString = new List<string>();
            clsLagerdaten Lager = new clsLagerdaten();
            String LogText = string.Empty;
            //SetInfoInInfobox(clsSystem.const_Default_TaskSeparator.ToString());
            string strProzessName = "[EA_AutoCreate]";
            listLogToFileCreateEA = new List<string>();

            Log = new clsLogbuchCon();
            Log.GL_User = this.GLUser;
            Log.Typ = enumLogArtItem.Start.ToString();
            Log.LogText = strProzessName + "Task gestartet....";
            Log.TableName = string.Empty;
            decimal decTmp = 0;
            Log.TableID = decTmp;
            ListLogAddString.Add(Log.GetAddLogbuchSQLString());
            SetInfoInInfoBox2(Log.LogText);

            LogText = String.Empty;
            //einzulesende ASN holen
            DataTable dtASN = clsASN.GetAsnToCreateEA(this.GLUser.User_ID);
            Int32 iValMax = dtASN.Rows.Count;
            LogText = GetLogString(LogText, strProzessName + " - Anzahl ASN-Meldungen ermittelt: " + iValMax.ToString(), true);
            SetInfoInInfoBox2(strProzessName + " - Anzahl ASN-Meldungen ermittelt: " + iValMax.ToString());
            if (iValMax > 0)
            {
                for (Int32 i = 0; i <= dtASN.Rows.Count - 1; i++)
                {
                    decTmp = 0;
                    Decimal.TryParse(dtASN.Rows[i]["ID"].ToString(), out decTmp);
                    if (decTmp > 0)
                    {
                        clsASN tmpASN = new clsASN();
                        tmpASN.InitClass(this.GLSystem, this.GLUser);
                        tmpASN.ID = decTmp;
                        tmpASN.Fill();

                        Log = new clsLogbuchCon();
                        Log.GL_User = this.GLUser;
                        Log.Typ = enumLogArtItem.Start.ToString();
                        Log.LogText = strProzessName + "  - Daten von ASN:[" + tmpASN.FileName + "] werden ermittelt...";
                        LogText += Log.LogText + Environment.NewLine;

                        Log.TableName = string.Empty;
                        decTmp = 0;
                        Log.TableID = decTmp;
                        ListLogAddString.Add(Log.GetAddLogbuchSQLString());
                        SetInfoInInfoBox2(Log.LogText);
                        LogText = GetLogString(LogText, Log.LogText, true);
                        DataTable dtAsnValue = clsASNValue.GetASNValueDataTableByASNId(this.GLUser.User_ID, tmpASN.ID);
                        //Unterscheidung XML / VDA
                        switch (tmpASN.ASNFileTyp)
                        {
                            case constValue_AsnArt.const_Art_XML_Uniport:
                                //Hier muss über TASK ermittelt werden um was für eine Meldung es sich handelt
                                if (dtAsnValue.Columns.Contains("FieldName"))
                                {
                                    dtAsnValue.DefaultView.RowFilter = "FieldName='TASK'";
                                    DataTable dtTmp = dtAsnValue.DefaultView.ToTable();
                                    if (dtTmp.Rows.Count > 0)
                                    {
                                        Lager = new clsLagerdaten();
                                        Lager.GLUser = this.GLUser;
                                        Lager.GLSystem = this.GLSystem;
                                        Lager.Eingang = new LVS.clsLEingang();
                                        Lager.Eingang._GL_User = this.GLUser;
                                        Lager.Eingang.MandantenID = tmpASN.MandantenID;
                                        Lager.Eingang.AbBereichID = tmpASN.ArbeitsbereichID;

                                        string strTask = dtTmp.Rows[0]["Value"].ToString();
                                        switch (strTask)
                                        {
                                            case "AVIS":
                                                //EML Meldung
                                                Lager.AddNewEingangXMLUniport(dtAsnValue);
                                                if (Lager.ListError.Count > 0)
                                                {
                                                    //Eintrag LOG ERROR
                                                    for (Int32 y = 0; y <= Lager.ListError.Count - 1; y++)
                                                    {
                                                        //Datei in Error Ordner verschieben
                                                        clsLogbuchCon lg = (clsLogbuchCon)Lager.ListError[y];
                                                        MoveToErrorDirectory(tmpASN, lg, strProzessName);
                                                        SetInfoInInfoBox2(lg.LogText);
                                                        ListLogAddString.Add(lg.GetAddLogbuchSQLString());
                                                    }
                                                }
                                                foreach (string strItem in Lager.ListInfoAsnLfsInserted)
                                                {
                                                    Log = new clsLogbuchCon();
                                                    Log.GL_User = this.GLUser;
                                                    Log.LogText = strProzessName + strItem;
                                                    ListLogAddString.Add(Log.GetAddLogbuchSQLString());
                                                    LogText = GetLogString(LogText, Log.LogText, true);
                                                    SetInfoInInfoBox2(Log.LogText);
                                                }
                                                tmpASN.IsRead = true;
                                                tmpASN.UpdateIsReadToTrue();
                                                break;
                                            case "DISP":
                                                //Abruf
                                                Lager.AddLAusgang(dtAsnValue, (Int32)Lager.Eingang.AbBereichID, (Int32)Lager.Eingang.MandantenID);
                                                if (Lager.ListError.Count > 0)
                                                {
                                                    //Eintrag LOG ERROR
                                                    for (Int32 y = 0; y <= Lager.ListError.Count - 1; y++)
                                                    {
                                                        Log = new clsLogbuchCon();
                                                        Log = (clsLogbuchCon)Lager.ListError[y];
                                                        Log.GL_User = this.GLUser;
                                                        String t = "Meldungart: " + tmpASN.ASNFileTyp.ToString() + Environment.NewLine + " ASN ID: [" + tmpASN.ID.ToString() + "] - File: [" + tmpASN.FileName + "]" + Environment.NewLine + Log.LogText;

                                                        ListLogAddString.Add(Log.GetAddLogbuchSQLString());
                                                        SetInfoInInfoBox2(t);
                                                    }
                                                    LogText = GetLogString(LogText, Log.LogText, true);
                                                }
                                                else
                                                {
                                                    //hier, da sobald ein Error Auftritt, die Meldung im Systme bleibt und 
                                                    //dadurch immer wieder versucht wird die Meldung einzulesen
                                                    tmpASN.IsRead = true;
                                                    tmpASN.UpdateIsReadToTrue();
                                                }
                                                foreach (string strItem in Lager.ListInfoAsnLfsInserted)
                                                {
                                                    log = new clsLogbuchCon();
                                                    log.GL_User = this.GLUser;
                                                    log.LogText = strProzessName + strItem;
                                                    // log.Add(log.GetAddLogbuchSQLString());
                                                    ListLogAddString.Add(log.GetAddLogbuchSQLString());
                                                    LogText = GetLogString(LogText, log.LogText, true);
                                                    SetInfoInInfoBox2(log.LogText);
                                                }
                                                break;
                                            default:
                                                //unbekannte Task Fehler
                                                Log = new clsLogbuchCon();
                                                Log.GL_User = this.GLUser;
                                                Log.LogText = "Meldungart: " + tmpASN.ASNFileTyp.ToString() + Environment.NewLine +
                                                                "ASN ID: [" + tmpASN.ID.ToString() + "] - File: [" + tmpASN.FileName + "]" + Environment.NewLine +
                                                                "TASK : [" + strTask + "] nicht bekannt....";

                                                ListLogAddString.Add(Log.GetAddLogbuchSQLString());
                                                LogText = GetLogString(LogText, Log.LogText, true);
                                                SetInfoInInfoBox2(Log.LogText);
                                                break;
                                        }
                                        Lager = null;
                                    }
                                }
                                break;

                            case constValue_AsnArt.const_Art_VDA4913:
                                Lager = new clsLagerdaten();
                                Lager.GLUser = this.GLUser;
                                Lager.GLSystem = this.GLSystem;
                                switch (tmpASN.ASNTyp.Typ)
                                {
                                    case "LSL":
                                        //EML Meldung Lieferant
                                        Lager.AddNEWEingangVDA4913(tmpASN);

                                        if (Lager.ListError.Count > 0)
                                        {
                                            //Eintrag LOG ERROR
                                            for (Int32 y = 0; y <= Lager.ListError.Count - 1; y++)
                                            {
                                                //Datei in Error Ordner verschieben
                                                clsLogbuchCon lg = (clsLogbuchCon)Lager.ListError[y];
                                                MoveToErrorDirectory(tmpASN, lg, strProzessName);
                                                ListLogAddString.Add(lg.GetAddLogbuchSQLString());
                                                LogText = GetLogString(LogText, Log.LogText, true);
                                                SetInfoInInfoBox2(Log.LogText);
                                            }
                                        }

                                        foreach (string strItem in Lager.ListInfoAsnLfsInserted)
                                        {
                                            Log = new clsLogbuchCon();
                                            Log.GL_User = this.GLUser;
                                            Log.LogText = strProzessName + strItem;
                                            Log.Add(Log.GetAddLogbuchSQLString());
                                            ListLogAddString.Add(Log.GetAddLogbuchSQLString());
                                            LogText = GetLogString(LogText, Log.LogText, true);
                                            SetInfoInInfoBox2(Log.LogText);
                                        }
                                        break;
                                    case "AML":
                                        break;
                                    default:
                                        //unbekannte Task Fehler
                                        //log = new clsLogbuchCon();
                                        //log.GL_User = this.GLUser;
                                        //log.LogText = "Meldungart: " + tmpASN.ASNFileTyp.ToString() + Environment.NewLine +
                                        //             "ASN ID: [" + tmpASN.ID.ToString() + "] - File: [" + tmpASN.FileName + "]" + Environment.NewLine +
                                        //             "TASK : [" + strTask + "] nicht bekannt....";
                                        //log.Add(log.GetAddLogbuchSQLString());
                                        break;
                                }
                                tmpASN.IsRead = true;
                                tmpASN.UpdateIsReadToTrue();

                                //Test
                                Lager = null;
                                break;

                            default:
                                break;
                        }
                        tmpASN = null;
                    }
                }
            }
            else
            {
                Log = new clsLogbuchCon();
                Log.GL_User = this.GLUser;
                Log.LogText = strProzessName + " - es liegt nichts an.....";
                Log.Add(Log.GetAddLogbuchSQLString());
                ListLogAddString.Add(Log.GetAddLogbuchSQLString());
            }

            Log = new clsLogbuchCon();
            Log.GL_User = this.GLUser;
            Log.Typ = enumLogArtItem.Stop.ToString();
            Log.LogText = strProzessName + " - Prozesse abgeschlossen...";
            Log.TableName = string.Empty;
            Log.TableID = 0;
            ListLogAddString.Add(Log.GetAddLogbuchSQLString());
            LogText = GetLogString(LogText, Log.LogText, true);
            SetInfoInInfoBox2(Log.LogText);

            clsLogbuchCon.AddCollectedSQLStringToLog(ListLogAddString, this.GLUser);
            bThreadAutoCreateEA = false;
            clsLogbuchCon.WriteLogToFile(ref listLogToFileASNRead, this.system, Application.StartupPath, clsLogbuchCon.const_Task_ASNWrite);
        }
        /// <summary>
        ///             Customized Processe
        ///             kundenspezifische Prozess, die abgearbeitet werden
        /// </summary>
        private void Task_CustomizedlProcesses()
        {
            listLogToCustomProcess = new List<string>();
            listLogToCustomProcess.Add(clsLogbuchCon.const_Logbuch_Trennzeichen);

            //Init Var/Classes
            ListLogAddString = new List<string>();
            string strProzessName = "[kundenspezifische Prozesse]";

            string strTmp = string.Empty;
            strTmp = GetLogString(strTmp, strProzessName + " - Task gestartet...", false);
            listLogToCustomProcess.Add(strTmp);
            SetInfoInInfoBox2(strTmp);

            Log = new clsLogbuchCon();
            Log.GL_User = this.GLUser;
            Log.Typ = enumLogArtItem.Start.ToString();
            Log.LogText = strTmp;
            Log.TableName = string.Empty;
            decimal decTmp = 0;
            Log.TableID = decTmp;
            ListLogAddString.Add(Log.GetAddLogbuchSQLString());
            //SetInfoInInfobox(Log.LogText);
            String LogText = String.Empty;

            try
            {
                List<string> ListCustomerProzesses = constValue_CustomProcesses.GetCustomProcessList();
                CustomProcessesViewData cpVD = new CustomProcessesViewData();

                Int32 iValMax = cpVD.ListCustomProcesses.Count;
                strTmp = strProzessName + " - Anzahl: " + iValMax.ToString();
                strTmp = GetLogString(strTmp, strTmp, false);
                listLogToCustomProcess.Add(strTmp);
                SetInfoInInfoBox2(strTmp);
                LogText = GetLogString(LogText, strTmp, true);

                if (iValMax > 0)
                {
                    foreach (CustomProcesses cp in cpVD.ListCustomProcesses)
                    {
                        switch (cp.ProcessName)
                        {
                            case constValue_CustomProcesses.const_Process_Novelis_ArticleAccessByCertifacte:
                                strTmp = strProzessName + ".[" + constValue_CustomProcesses.const_Process_Novelis_ArticleAccessByCertifacte + "] - wird durchgeführt...";
                                strTmp = GetLogString(strTmp, strTmp, false);
                                listLogToCustomProcess.Add(strTmp);
                                SetInfoInInfoBox2(strTmp);

                                CustomProcess_Novelis_AccessByArticleCert pro = new CustomProcess_Novelis_AccessByArticleCert(this.GLUser);
                                pro.ListLog.Clear();
                                //pro.CheckForArticleCertificateByCommunicatorForLastDays();
                                pro.CheckForArticleCertificateByCommunicator(true);
                                if (pro.ListLog.Count > 0)
                                {
                                    foreach (string s in pro.ListLog)
                                    {
                                        //strTmp += s + Environment.NewLine;
                                        strTmp = strProzessName + ".[" + constValue_CustomProcesses.const_Process_Novelis_ArticleAccessByCertifacte + "].[ByDate] - " + s;
                                        strTmp = GetLogString(strTmp, strTmp, false);
                                        listLogToCustomProcess.Add(strTmp);
                                        SetInfoInInfoBox2(strTmp);
                                    }
                                }
                                pro.ListLog.Clear();
                                pro.CheckForArticleCertificateByCommunicator();
                                if (pro.ListLog.Count > 0)
                                {
                                    foreach (string s in pro.ListLog)
                                    {
                                        //strTmp += s + Environment.NewLine;
                                        strTmp = strProzessName + ".[" + constValue_CustomProcesses.const_Process_Novelis_ArticleAccessByCertifacte + "].[Active] - " + s;
                                        strTmp = GetLogString(strTmp, strTmp, false);
                                        listLogToCustomProcess.Add(strTmp);
                                        SetInfoInInfoBox2(strTmp);
                                    }
                                }
                                pro.ListLog.Clear();
                                pro.CheckZertificateByArticleInSPL();
                                if (pro.ListLog.Count > 0)
                                {
                                    foreach (string s in pro.ListLog)
                                    {
                                        //strTmp += s + Environment.NewLine;
                                        strTmp = strProzessName + ".[" + constValue_CustomProcesses.const_Process_Novelis_ArticleAccessByCertifacte + "].[SPL] - " + s;
                                        strTmp = GetLogString(strTmp, strTmp, false);
                                        listLogToCustomProcess.Add(strTmp);
                                        SetInfoInInfoBox2(strTmp);

                                    }
                                }
                                break;
                        }
                    }
                }
                else
                {
                    strTmp = strProzessName + "es liegt nichts an....";
                    strTmp = GetLogString(strTmp, strTmp, false);
                    listLogToCustomProcess.Add(strTmp);
                    SetInfoInInfoBox2(strTmp);

                    Log = new clsLogbuchCon();
                    Log.GL_User = this.GLUser;
                    Log.Typ = enumLogArtItem.autoMail.ToString();
                    Log.LogText = strTmp;
                    Log.TableName = string.Empty;
                    Log.TableID = 0;
                    ListLogAddString.Add(Log.GetAddLogbuchSQLString());
                }
            }
            catch (Exception ex)
            {
                string st = ex.ToString();

                clsError Error = new clsError();
                Error.Sys = this.systemLVS;
                Error._GL_User = this.GLUser;
                Error.Aktion = "frmMainCom - Task_CustomizedlProcesses() ";
                Error.Datum = DateTime.Now;
                Error.ErrorText = string.Empty;
                Error.exceptText = ex.ToString();
                Error.WriteError();

                strTmp = strProzessName + ".[ERROR] --> Error - Mail versendet...";
                strTmp = GetLogString(strTmp, strTmp, false);
                listLogToCustomProcess.Add(strTmp);
                SetInfoInInfoBox2(strTmp);

                Log = new clsLogbuchCon();
                Log.GL_User = this.GLUser;
                Log.Typ = enumLogArtItem.Start.ToString();
                Log.LogText = strTmp;
                Log.TableName = string.Empty;
                decTmp = 0;
                Log.TableID = decTmp;
                ListLogAddString.Add(Log.GetAddLogbuchSQLString());
            }
            finally
            {
                Thread.Sleep(1000);

                strTmp = strProzessName + "Prozesse abgeschlossen...";
                strTmp = GetLogString(strTmp, strTmp, false);
                listLogToCustomProcess.Add(strTmp);
                clsLogbuchCon.WriteLogToFile(ref this.listLogToCustomProcess, this.system, Application.StartupPath, clsLogbuchCon.const_Task_CustomProcess);
                SetInfoInInfoBox2(strTmp);

                Log = new clsLogbuchCon();
                Log.GL_User = this.GLUser;
                Log.Typ = enumLogArtItem.Stop.ToString();
                Log.LogText = strTmp;
                Log.TableName = string.Empty;
                Log.TableID = 0;
                ListLogAddString.Add(Log.GetAddLogbuchSQLString());
                LogText = GetLogString(LogText, Log.LogText, true);
                SetInfoInInfoBox2(Log.LogText);
                //clsLogbuchCon.AddCollectedSQLStringToLog(ListLogAddString, this.GLUser);
            }

            this.Invoke(new EventHandler(m_StartTimerCustomizedProcess));
            bThreadCustomProcess = false;
        }
        /*****************************************************************************************
         *                      Datagridview Prozesse
         * **************************************************************************************/
        ///<summary>frmMainCom / InitDgvProzesse</summary>
        ///<remarks></remarks>
        private void dgvProzesses_CreateCell(object sender, GridViewCreateCellEventArgs e)
        {
            string strColumnName = e.Column.Name;
            if (strColumnName == "iVal" && e.Row.Data is GridViewDataRowInfo)
            {
                e.CellType = typeof(GridProgressCellElement);
            }
        }
        ///<summary>frmMainCom / btnQueue_Click</summary>
        ///<remarks></remarks>
        private void btnQueue_Click(object sender, EventArgs e)
        {
            InitQueue();
        }
        /// <summary>
        /// frmMainCom / GetLogString
        /// </summary>
        /// <param name="LogString"></param>
        /// <param name="LogAdd"></param>
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
        ///<summary>frmMainCom / SetInfoInInfoBox2</summary>
        ///<remarks></remarks>
        public void SetInfoInInfoBox2(string strInfo)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            SetInfoInInfoBox2(strInfo);
                                                                        }
                                                                    )
                                    );
                return;
            }

            this.dgvInfoBox.DataSource = null;
            SourceLogInfo.Add(clsViewLog.GetViewLogItem(strInfo));
            List<clsViewLog> tmpSource = new List<clsViewLog>();
            tmpSource.AddRange(SourceLogInfo);
            tmpSource = tmpSource.OrderByDescending(x => x.ID).ToList();

            SourceLogInfo = new List<clsViewLog>();

            int iCount = 0;
            foreach (clsViewLog itm in tmpSource)
            {
                iCount++;
                if (iCount <= 30)
                {
                    SourceLogInfo.Add(itm);
                }
            }
            this.dgvInfoBox.DataSource = SourceLogInfo;
            foreach (GridViewColumn itm in this.dgvInfoBox.Columns)
            {
                switch (itm.HeaderText)
                {
                    case "ID":
                        itm.IsVisible = false;
                        break;
                }
            }
            this.dgvInfoBox.BestFitColumns();
        }
        ///<summary>frmMainCom / MoveToErrorDirectory</summary>
        ///<remarks></remarks>
        private void MoveToErrorDirectory(clsASN myASN, clsLogbuchCon myLog, string myProzess)
        {
            string ErrorPath = this.GLSystem.VE_OdettePath + myASN.Job.ErrorPath + "\\" + myASN.FileName;
            string ErrorPathFile = ErrorPath + "\\" + myASN.FileName;
            string FilePath = this.GLSystem.VE_OdettePath + myASN.Path + "\\" + myASN.FileName;

            // --- Log für Grund aus myLog -> Fehlerbeschreibung 
            myLog.GL_User = this.GLUser;
            myLog.LogText = myProzess + ".[ERROR]" +
                            " - Meldungart: " + myASN.ASNFileTyp.ToString() + Environment.NewLine +
                            "ASN ID: [" + myASN.ID.ToString() + "] - File: [" + myASN.FileName + "]" +
                            myLog.LogText;
            myLog.Add(myLog.GetAddLogbuchSQLString());
            SetInfoInInfoBox2(myLog.LogText);

            //--- Date in Error kopieren
            FunctionsCom.CheckDirectory(ErrorPath);
            FunctionsCom.MoveFile(ErrorPath, FilePath, false);

            //---- Logbucheintrag für das verschieben in den Errorordner
            Log = new clsLogbuchCon();
            Log.GL_User = this.GLUser;
            Log.LogText = myProzess + ".[ERROR]" +
                          "Meldungart: " + myASN.ASNFileTyp.ToString() + Environment.NewLine +
                          "ASN ID: [" + myASN.ID.ToString() + "] - File: [" + myASN.FileName + "]" +
                          " -> File in Errorordner verschoben.....";
            Log.Add(Log.GetAddLogbuchSQLString());

            SetInfoInInfoBox2(log.LogText);
        }
        ///<summary>frmMainCom / ribbonTab6_Click</summary>
        ///<remarks></remarks>
        private void ribbonTab6_Click(object sender, EventArgs e)
        {
            InitThreadASNread();
        }
        ///<summary>frmMainCom / tASNwrite_Tick</summary>
        ///<remarks></remarks>
        private void tASNwrite_Tick(object sender, EventArgs e)
        {
            tASNwrite.Enabled = false;
            InitThreadASNWrite();
            InitLogs();
        }
        ///<summary>frmMainCom / tCronjob_Tick</summary>
        ///<remarks></remarks>
        private void tCronjob_Tick(object sender, EventArgs e)
        {
            tCronjob.Enabled = false;
            InitThreadCronjobs();
            InitLogs();
        }
        ///<summary>frmMainCom / tASNread_Tick</summary>
        ///<remarks></remarks>
        private void tASNread_Tick(object sender, EventArgs e)
        {
            tASNread.Enabled = false;
            InitThreadASNread();
            InitLogs();
        }
        private void tCustomizeProcess_Tick(object sender, EventArgs e)
        {
            tCustomizeProcess.Enabled = false;
            InitThreadCustomizedProcess();
            InitLogs();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tWatchDog_Tick(object sender, EventArgs e)
        {
            tWatchDog.Enabled = false;
            InitThreadWatchDog();
            InitLogs();
        }
        ///<summary>frmMainCom / m_StartTimerWrite</summary>
        ///<remarks></remarks>
        private void m_StartTimerWrite(object sender, EventArgs e)
        {
            this.tASNwrite.Start();
        }
        ///<summary>frmMainCom / m_StartTimerRead</summary>
        ///<remarks></remarks>
        private void m_StartTimerRead(object sender, EventArgs e)
        {
            this.tASNread.Start();
        }
        ///<summary>frmMainCom / m_StartTimerCronJob</summary>
        ///<remarks></remarks>
        private void m_StartTimerCronJob(object sender, EventArgs e)
        {
            this.tCronjob.Start();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_StartTimerCustomizedProcess(object sender, EventArgs e)
        {
            this.tCustomizeProcess.Start();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_StartTimerWatchDog(object sender, EventArgs e)
        {
            this.tWatchDog.Start();
        }
        ///<summary>frmMainCom / radRibbonBar1_CommandTabSelecting</summary>
        ///<remarks></remarks>
        private void radRibbonBar1_CommandTabSelecting(object sender, CommandTabSelectingEventArgs args)
        {
            if (args.NewCommandTab.Tab.Tag != null)
            {
                Int32 iTmp = 0;
                Int32.TryParse(args.NewCommandTab.Tab.Tag.ToString(), out iTmp);

                switch (iTmp)
                {
                    case 0:
                        tabViewGrid.SelectedTab = tabViewPageLog;
                        break;

                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabViewGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabViewGrid.SelectedTab.Name)
            {
                case "tabViewPageLog":
                    this.tabMain.IsSelected = true;
                    break;
                case "tabPage_WatchDog":

                    break;

                case "tabPage_LogIN":
                    InitDGVLogIN();
                    break;

                case "tabPage_LogOUT":
                    InitDGVLogOUT();
                    break;

                case "tabPage_LogSYS":
                    InitDGVLogSYS();
                    break;
                case "tabPage_LogCronJob":
                    InitDGVLogCronJobs();
                    break;

                case "tabPage_CustimizedProcess":
                    InitDGVLogCustomizedProcess();
                    break;

                case "tabPage_SystemInfo":
                    InitSystemInfo();
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitSystemInfo()
        {
            string strTmp = "Verion : " + this.GLSystem.sys_VersionApp + "/ Built :" + this.GLSystem.sys_VersionSystemBuilt;
            this.radLabelVersion.Text = strTmp;

            strTmp = this.GLSystem.client_CompanyName + Environment.NewLine +
                            this.GLSystem.client_Strasse + Environment.NewLine +
                            this.GLSystem.client_PLZOrt + Environment.NewLine;
            this.radLabelLizenznehmer.Text = strTmp;

            tbInfoText.Text = string.Empty;
            ctrAppInfoClssText info = new ctrAppInfoClssText();
            this.tbInfoText.Text = info.InfoText;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radButtonElement4_Click(object sender, EventArgs e)
        {
            this.tabMain.IsSelected = true;
        }
        /// <summary>
        ///             Init all LOG DGV
        /// </summary>
        private void InitLogs()
        {
            //schreiben der LOGS


            //Log IN 
            InitDGVLogIN();
            //Log OUT
            InitDGVLogOUT();
            //Log CronJobs
            InitDGVLogCronJobs();
            //Log CustomizedProcess
            InitDGVLogCustomizedProcess();
            //Log SYSTEM
            InitDGVLogSYS();
        }
        /// <summary>
        ///             DGB IN
        /// </summary>
        private void InitDGVLogIN()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            InitDGVLogIN();
                                                                        }
                                                                    )
                                    );
                return;
            }
            List<clsViewLog> SourceIN = new List<clsViewLog>();
            SourceIN = clsLogbuchCon.GetLogText(null, Application.StartupPath, clsLogbuchCon.const_Task_ASNRead);
            this.dgvLogIN.DataSource = null;
            this.dgvLogIN.DataSource = SourceIN;
            foreach (GridViewColumn itm in this.dgvLogIN.Columns)
            {
                switch (itm.HeaderText)
                {
                    case "ID":
                        itm.IsVisible = false;
                        break;
                }
            }
            this.dgvLogIN.BestFitColumns();
        }
        /// <summary>
        ///             DGV OUT
        /// </summary>
        private void InitDGVLogOUT()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            InitDGVLogOUT();
                                                                        }
                                                                    )
                                    );
                return;
            }
            List<clsViewLog> SourceOUT = new List<clsViewLog>();
            SourceOUT = clsLogbuchCon.GetLogText(null, Application.StartupPath, clsLogbuchCon.const_Task_ASNWrite);
            this.dgvLogOUT.DataSource = null;
            this.dgvLogOUT.DataSource = SourceOUT;
            foreach (GridViewColumn itm in this.dgvLogOUT.Columns)
            {
                switch (itm.HeaderText)
                {
                    case "ID":
                        itm.IsVisible = false;
                        break;
                }
            }
            this.dgvLogOUT.BestFitColumns();
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitDGVLogCronJobs()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            InitDGVLogCronJobs();
                                                                        }
                                                                    )
                                    );
                return;
            }
            List<clsViewLog> SourceCronJobs = new List<clsViewLog>();
            SourceCronJobs = clsLogbuchCon.GetLogText(null, Application.StartupPath, clsLogbuchCon.const_Task_CronJob);
            this.dgvLogCronJobs.DataSource = null;
            this.dgvLogCronJobs.DataSource = SourceCronJobs;
            foreach (GridViewColumn itm in this.dgvLogCronJobs.Columns)
            {
                switch (itm.HeaderText)
                {
                    case "ID":
                        itm.IsVisible = false;
                        break;
                }
            }
            this.dgvLogCronJobs.BestFitColumns();
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitDGVLogCustomizedProcess()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            InitDGVLogCustomizedProcess();
                                                                        }
                                                                    )
                                    );
                return;
            }
            List<clsViewLog> SourceCustomizedProcess = new List<clsViewLog>();
            SourceCustomizedProcess = clsLogbuchCon.GetLogText(null, Application.StartupPath, clsLogbuchCon.const_Task_CustomProcess);
            this.dgvLogCustomizedProcess.DataSource = null;
            this.dgvLogCustomizedProcess.DataSource = SourceCustomizedProcess;
            foreach (GridViewColumn itm in this.dgvLogCustomizedProcess.Columns)
            {
                switch (itm.HeaderText)
                {
                    case "ID":
                        itm.IsVisible = false;
                        break;
                }
            }
            this.dgvLogCustomizedProcess.BestFitColumns();
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
            foreach (GridViewColumn itm in this.dgvLogSYS.Columns)
            {
                switch (itm.HeaderText)
                {
                    case "ID":
                        itm.IsVisible = false;
                        break;
                }
            }
            this.dgvLogSYS.BestFitColumns();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radRibbonBarGroup5_Click(object sender, EventArgs e)
        {
            InitLogs();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //Task_ASNwrite();
            //Task_ASNread();
            //LogToXML();

        }



        private void LogToXML()
        {
            string strPath = @"F:\";
            DateTime Start = new DateTime(2000, 1, 1);
            DateTime End = new DateTime(2018, 3, 31);
            clsLogbuch log = new clsLogbuch();
            DataTable dt = log.GetLogbuch(false, Start, End);

            System.IO.StringWriter writer = new System.IO.StringWriter();
            dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
            string result = writer.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWDTest_Click(object sender, EventArgs e)
        {
            //Task_WatchDog();
            Task_SendVitalSign();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTest_TaskWrite_Click(object sender, EventArgs e)
        {
            this.Task_ASNwrite();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTestRead_Click(object sender, EventArgs e)
        {
            this.Task_ASNread();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radButtonElement5_Click(object sender, EventArgs e)
        {
            Task_CALLRead();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTask_CronJobs_Click(object sender, EventArgs e)
        {
            Task_CronJobs();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTask_CustomerProcess_Click(object sender, EventArgs e)
        {
            Task_CustomizedlProcesses();
        }

        private void btnAdminCockpit_Click(object sender, EventArgs e)
        {
            //-- Admin Cockpit öffnen
            frmAdminCockpit ac = new frmAdminCockpit();
            ac.Show();
        }

        private void pbComtecLogo_Click(object sender, EventArgs e)
        {

        }

        private void radLabelComtec_Click(object sender, EventArgs e)
        {

        }
    }
}
