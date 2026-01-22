using LVS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using System.Timers;


namespace WatchDogService
{
    public partial class WDService : ServiceBase
    {
        System.Timers.Timer timer = new System.Timers.Timer();
        public const string const_Prozess_SystemStart = "System-Start";
        public const string const_Prozess_SystemStop = "System-STOP";
        public LVS.Globals._GL_SYSTEM GLSystem = new LVS.Globals._GL_SYSTEM();
        public LVS.Globals._GL_USER GLUser = new LVS.Globals._GL_USER();
        //internal LVS.clsSystem system = new LVS.clsSystem();
        internal LVS.clsSystem system = new clsSystem();
        internal DateTime dtWDStart;
        internal DateTime dtWDNextStart;
        internal LVS.clsSystem systemLVS = new LVS.clsSystem();
        internal bool OnElapsedProcess = true;
        internal bool IsDebugMode { get; set; } = true;
        public WDService(bool myDebugMode)
        {
            InitializeComponent();
            IsDebugMode = myDebugMode;

            eventLogWD = new EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("MySource"))
            {
                System.Diagnostics.EventLog.CreateEventSource("MySource", "MyNewLog");
            }
            eventLogWD.Source = "MySource";
            eventLogWD.Log = "MyNewLog";

        }
        public void onDebug()
        {
            OnStart(null);
        }
        protected override void OnStart(string[] args)
        {
            eventLogWD.WriteEntry("In OnStart.");
            try
            {
                // initialisieren der DB Daten
                system = new clsSystem();
                //system.VE_IsWatchDog = true;
                system.InitSystem(ref this.GLSystem);

                string strTmp = string.Empty;

                //--- Der WatchDog benötigt keine DB-Verbindung
                strTmp = GetLogString(strTmp, "[WatchDog].[Service] - WatchDog WD - Communicator 2015 @ Comtec Nöker GmbH", false);
                this.systemLVS.listLogToFileSystem.Add(strTmp);

                strTmp = string.Empty;
                strTmp = GetLogString(strTmp, "[WatchDog].[Service].[" + const_Prozess_SystemStart + "] - WatchDog wird initialisiert.", false);
                this.systemLVS.listLogToFileSystem.Add(strTmp);
                clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, AppDomain.CurrentDomain.BaseDirectory, clsLogbuchCon.const_Task_System);
                dtWDNextStart = DateTime.Now;
                SetWDInfo();
            }
            catch (Exception ex)
            {
                clsMessages.Allgemein_ERRORTextShow(ex.ToString());
            }

            if (this.system.VE_Autostart)
            {
                string strTmp = string.Empty;
                strTmp = Functions.GetLogString(strTmp, "------------------------------------------------------", false);
                this.systemLVS.listLogToFileSystem.Add(strTmp);
                clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, AppDomain.CurrentDomain.BaseDirectory, clsLogbuchCon.const_Task_System);

                strTmp = Functions.GetLogString(strTmp, "[WatchDog].[Service].[" + const_Prozess_SystemStart + "].[AUTOSTART] - WatchDog-Prozesse gestartet....", false);
                this.systemLVS.listLogToFileSystem.Add(strTmp);
                clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, AppDomain.CurrentDomain.BaseDirectory, clsLogbuchCon.const_Task_System);

                clsWD_SignOfLife SoL = new clsWD_SignOfLife(this.GLUser, this.system, false);
                //tWatchDog.Interval = 1000;
                //tWatchDog.Start();
            }

            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = this.system.VE_MainThreadDuration;
            timer.Enabled = true;
            OnElapsedProcess = false;

            if (IsDebugMode)
            {
                ProcessOnElapsedTime();
            }
        }
        protected override void OnStop()
        {
            string strTmp = string.Empty;
            strTmp = Functions.GetLogString(strTmp, Environment.NewLine, false);
            this.systemLVS.listLogToFileSystem.Add(strTmp);

            strTmp = string.Empty;
            strTmp = Functions.GetLogString(strTmp, "[WatchDog].[Service] - Sped4 WatchDog Service wurde gestoppt!", false);
            this.systemLVS.listLogToFileSystem.Add(strTmp);

            strTmp = string.Empty;
            strTmp = Functions.GetLogString(strTmp, Environment.NewLine, false);
            this.systemLVS.listLogToFileSystem.Add(strTmp);

            clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, AppDomain.CurrentDomain.BaseDirectory, clsLogbuchCon.const_Task_System);
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            ProcessOnElapsedTime();
        }

        private void ProcessOnElapsedTime()
        {
            if (!OnElapsedProcess)
            {
                OnElapsedProcess = true;
                if (dtWDNextStart < DateTime.Now)
                {
                    string strTmp = Environment.NewLine;
                    strTmp = Functions.GetLogString(strTmp, strTmp, false);
                    this.systemLVS.listLogToFileSystem.Clear();
                    this.systemLVS.listLogToFileSystem.Add(strTmp);

                    strTmp = string.Empty;
                    strTmp = Functions.GetLogString(strTmp, "[WatchDog].[Service] - Task_WatchDog:  START", false);
                    this.systemLVS.listLogToFileSystem.Add(strTmp);
                    clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, AppDomain.CurrentDomain.BaseDirectory, clsLogbuchCon.const_Task_System);

                    Task_WatchDog();

                    strTmp = string.Empty;
                    strTmp = Functions.GetLogString(strTmp, "[WatchDog].[Service] - Task_WatchDog:  ENDE", false);
                    this.systemLVS.listLogToFileSystem.Clear();
                    this.systemLVS.listLogToFileSystem.Add(strTmp);
                    clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, AppDomain.CurrentDomain.BaseDirectory, clsLogbuchCon.const_Task_System);


                    strTmp = Environment.NewLine;
                    strTmp = Functions.GetLogString(strTmp, strTmp, false);
                    this.systemLVS.listLogToFileSystem.Clear();
                    this.systemLVS.listLogToFileSystem.Add(strTmp);

                    strTmp = string.Empty;
                    strTmp = Functions.GetLogString(strTmp, "[WatchDog].[Service] - Task_SignOfLife:  START", false);
                    this.systemLVS.listLogToFileSystem.Add(strTmp);

                    Task_SignOfLife();

                    //---Logeintrag
                    strTmp = string.Empty;
                    strTmp = Functions.GetLogString(strTmp, "[WatchDog].[Service] - Task_SignOfLife:  ENDE", false);
                    this.systemLVS.listLogToFileSystem.Add(strTmp);
                    clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, AppDomain.CurrentDomain.BaseDirectory, clsLogbuchCon.const_Task_System);

                    double dbTmp = (double)this.system.VE_MainThreadDuration;
                    this.dtWDNextStart = DateTime.Now.AddMilliseconds(dbTmp);
                    Thread.Sleep(this.system.VE_MainThreadDuration);
                }
                else
                {
                    Thread.Sleep(this.system.VE_MainThreadDuration);
                }
                OnElapsedProcess = false;
            }
        }
        private void Task_WatchDog()
        {
            try
            {
                if ((this.system is clsSystem) && (this.system.VE_IsWatchDog))
                {
                    string strProzessName = string.Empty;
                    strProzessName = "[WatchDog].[Service]";
                    //string strTmp = string.Empty;

                    if (this.system.WD_List_WDClient != null)
                    {
                        string strTmp = string.Empty;
                        List<string> ListWDClient = this.system.WD_List_WDClient;
                        foreach (string wdClient in ListWDClient)
                        {
                            strProzessName = "[WatchDog].[Service]";
                            //string strTmp = string.Empty;
                            strTmp = string.Empty;
                            this.systemLVS.listLogToFileSystem.Clear();

                            strProzessName = strProzessName + ".[" + wdClient + "]";

                            strTmp = string.Empty;
                            strTmp = strProzessName + " --------------------------------------------------------------------------------------";
                            strTmp = Functions.GetLogString(strTmp, strTmp, false);
                            this.systemLVS.listLogToFileSystem.Add(strTmp);

                            strTmp = string.Empty;
                            strTmp = strProzessName + " >>> Check WDClient: " + wdClient;
                            strTmp = Functions.GetLogString(strTmp, strTmp, false);
                            this.systemLVS.listLogToFileSystem.Add(strTmp);

                            strProzessName = strProzessName + ".[Check File] >>> ";

                            if (!wdClient.Equals(string.Empty))
                            {
                                string strFileToCheckFilePath = string.Empty;
                                if (!this.system.VE_GetFilesByFTP)
                                {
                                    string strConfigPath = this.system.VE_FilePathCheckNotFTP; // + @"\" + wdClient;
                                    string strClientPath = System.IO.Path.Combine(strConfigPath, wdClient);
                                    //bool b1 = System.IO.Directory.Exists(strConfigPath);
                                    //bool b2 = System.IO.Directory.Exists(strClientPath);
                                    strFileToCheckFilePath = strClientPath + "\\WD_" + wdClient + ".txt";
                                    //bool b3 = System.IO.File.Exists(strFileToCheckFilePath);
                                }
                                else
                                {
                                    // letzte Datei per ftp holen
                                    clsftp ftp = new clsftp();
                                    ftp.WDClientMC = wdClient;
                                    ftp.InitClass(null, this.system);

                                    strTmp = strProzessName + " FTP-Verbindung wird initialisiert...";
                                    strTmp = Functions.GetLogString(strTmp, strTmp, false);
                                    this.systemLVS.listLogToFileSystem.Add(strTmp);

                                    if (ftp.CheckConnection())
                                    {
                                        strTmp = strProzessName + "Der Aufbau der FTP-Verbindung ist erfolgreich...";
                                        strTmp = Functions.GetLogString(strTmp, strTmp, false);
                                        //this.systemLVS.listLogToFileSystem.Clear();
                                        this.systemLVS.listLogToFileSystem.Add(strTmp);

                                        ftp.Download_WDFiles();

                                        strFileToCheckFilePath = ftp.WD_DownloadFilePath;
                                        //---Logeintrag
                                        strTmp = strProzessName + ftp.WD_InfoText;
                                        strTmp = Functions.GetLogString(strTmp, strTmp, false);
                                        this.systemLVS.listLogToFileSystem.Add(strTmp);
                                    }
                                    else
                                    {
                                        strTmp = strProzessName + " " + ftp.WD_InfoText;
                                        strTmp = Functions.GetLogString(strTmp, strTmp, false);
                                        //this.systemLVS.listLogToFileSystem.Clear();
                                        this.systemLVS.listLogToFileSystem.Add(strTmp);
                                        //clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, AppDomain.CurrentDomain.BaseDirectory, clsLogbuchCon.const_Task_System);

                                        strTmp = strProzessName + ftp.WD_InfoText + " -> konnte nicht gefunden werden !!!";
                                        strTmp = Functions.GetLogString(strTmp, strTmp, false);
                                        //this.systemLVS.listLogToFileSystem.Clear();
                                        this.systemLVS.listLogToFileSystem.Add(strTmp);
                                        clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, AppDomain.CurrentDomain.BaseDirectory, clsLogbuchCon.const_Task_System);
                                    }
                                }

                                FileInfo fileInfo = new FileInfo(strFileToCheckFilePath);
                                bool b4 = fileInfo.Exists;
                                // Datei prüfen                    
                                //if (System.IO.File.Exists(strFileToCheckFilePath))
                                if (fileInfo.Exists)
                                {
                                    //---Logeintrag
                                    strTmp = strProzessName + "File wird analysiert: " + strFileToCheckFilePath;
                                    strTmp = Functions.GetLogString(strTmp, strTmp, false);
                                    this.systemLVS.listLogToFileSystem.Add(strTmp);

                                    List<string> ListFileToCheck = new List<string>();

                                    try
                                    {
                                        string line = string.Empty;
                                        using (StreamReader reader = new StreamReader(strFileToCheckFilePath))
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
                                        clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, AppDomain.CurrentDomain.BaseDirectory, clsLogbuchCon.const_Task_System);
                                    }
                                    else
                                    {
                                        //---Logeintrag
                                        strTmp = strProzessName + "Keine Filedaten vorhanden !!!";
                                        strTmp = Functions.GetLogString(strTmp, strTmp, false);
                                        this.systemLVS.listLogToFileSystem.Add(strTmp);
                                        clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, AppDomain.CurrentDomain.BaseDirectory, clsLogbuchCon.const_Task_System);
                                    }
                                }
                                else
                                {
                                    //---Logeintrag
                                    strTmp = strProzessName + "Datei: " + strFileToCheckFilePath + " konnte nicht gefunden werden!";
                                    strTmp = Functions.GetLogString(strTmp, strTmp, false);
                                    this.systemLVS.listLogToFileSystem.Clear();
                                    this.systemLVS.listLogToFileSystem.Add(strTmp);
                                    clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, AppDomain.CurrentDomain.BaseDirectory, clsLogbuchCon.const_Task_System);

                                    clsMail ErrorMail = new clsMail();
                                    ErrorMail.InitClass(this.GLUser, this.system);
                                    ErrorMail.Subject = "!!!WatchDog Datei nicht vorhanden!!! - " + wdClient;
                                    ErrorMail.Message = "Zeit: " + DateTime.Now.ToString() + Environment.NewLine +
                                                        "Datei: " + strFileToCheckFilePath;
                                    ErrorMail.SendError();
                                }
                            }
                            else
                            {
                                strTmp = strProzessName + "WD_InfoText ist Empty";
                                strTmp = Functions.GetLogString(strTmp, strTmp, false);
                                this.systemLVS.listLogToFileSystem.Add(strTmp);
                                clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, AppDomain.CurrentDomain.BaseDirectory, clsLogbuchCon.const_Task_System);
                            }

                            Thread.Sleep(1000);
                        }// foreach      

                        this.systemLVS.listLogToFileSystem.Clear();
                        //---Logeintrag
                        strTmp = string.Empty;
                        strTmp = Functions.GetLogString(strTmp, "[WatchDog].[Service] - Prozessdurchlauf beendet....", false);
                        this.systemLVS.listLogToFileSystem.Add(strTmp);
                        clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, AppDomain.CurrentDomain.BaseDirectory, clsLogbuchCon.const_Task_System);
                    }
                }
            }
            catch (Exception ex)
            {
                string s = ex.ToString();
                clsError Error = new clsError();
                Error.Sys = this.systemLVS;
                Error._GL_User = this.GLUser;
                Error.Aktion = "WatchDog Service - Task_WatchDog() ";
                Error.Datum = DateTime.Now;
                Error.ErrorText = ex.ToString();
                Error.exceptText = ex.InnerException.ToString();
                Error.WriteError();
            }
        }

        private void Task_SignOfLife()
        {
            string strProzessName = string.Empty;
            string strTmp = string.Empty;

            try
            {
                //---SignOfLife
                clsWD_SignOfLife SoL = new clsWD_SignOfLife(this.GLUser, this.system, true);

                if (!SoL.Infotext.Equals(string.Empty))
                {
                    strProzessName = "[WatchDog].[Service].[SignOfLife]";
                    strTmp = string.Empty;
                    strTmp = strProzessName;
                    strTmp = Functions.GetLogString(strTmp, strTmp, false);
                    this.systemLVS.listLogToFileSystem.Add(strTmp);

                    strTmp = string.Empty;
                    strTmp = strProzessName + " > " + SoL.Infotext;
                    strTmp = Functions.GetLogString(strTmp, strTmp, false);
                    this.systemLVS.listLogToFileSystem.Add(strTmp);
                }
                clsLogbuchCon.WriteLogToFile(ref this.systemLVS.listLogToFileSystem, this.system, AppDomain.CurrentDomain.BaseDirectory, clsLogbuchCon.const_Task_System);
            }
            catch (Exception ex)
            {
                string s = ex.ToString();
                clsError Error = new clsError();
                Error.Sys = this.systemLVS;
                Error._GL_User = this.GLUser;
                Error.Aktion = "WatchDog Service - Task_SignOfLife() ";
                Error.Datum = DateTime.Now;
                Error.ErrorText = ex.ToString();
                Error.exceptText = ex.InnerException.ToString();
                Error.WriteError();
            }
        }

        private void SetWDInfo()
        {

            string strInfo = string.Empty;
            //strInfo += "WD Info:" + Environment.NewLine + Environment.NewLine;

            strInfo += String.Format("{0} - {1}\t{2}", "WD Info:", "Datum:", DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")) + Environment.NewLine; // + Environment.NewLine;
            strInfo += String.Format("{0}", "------------------------------------------------") + Environment.NewLine; // + Environment.NewLine;

            if (DateTime.MinValue < this.dtWDStart)
            {
                strInfo += String.Format("{0,-25}\t{1}", "letzter Start:", this.dtWDStart.ToString("dd.MM.yyyy HH:mm:ss")) + Environment.NewLine;
            }

            strInfo += String.Format("{0,-25}\t{1}", "nächster Start:", this.dtWDNextStart.ToString("dd.MM.yyyy HH:mm:ss")) + Environment.NewLine;
            strInfo += String.Format("{0,-25}\t{1}", "Intervall Ticks:", timer.Interval.ToString("#,###,##0")) + Environment.NewLine;
            decimal decIntervall = (decimal)timer.Interval / 1000;
            if (decIntervall > 1)
            {
                strInfo += String.Format("{0-25}\t{1}", "Intervall Sek./Min.:", decIntervall.ToString("####.##")) + "/" + (decIntervall / 60).ToString("####.##") + Environment.NewLine;
            }
            //else
            //{
            //    strInfo += String.Format("{0-10}\t{1}", "Intervall Sek./Min.:", decIntervall.ToString("####.##")) + "/" + (decIntervall / 60).ToString("####.##") + Environment.NewLine;
            //}

            strInfo += Environment.NewLine; // + Environment.NewLine;
            strInfo += "System Info:" + Environment.NewLine; // + Environment.NewLine;
            strInfo += String.Format("{0}", "------------------------------------------------") + Environment.NewLine; // + Environment.NewLine;
            if (this.system.VE_Autostart)
            {
                strInfo += String.Format("{0,-25}\t{1}", "System - Autostart:", "true") + Environment.NewLine;
            }
            else
            {
                strInfo += String.Format("{0,-25}\t{1}", "Autostart:", "false") + Environment.NewLine;
            }
            strInfo += String.Format("{0,-25}\t{1}", "SignOfLifeIntervall:", this.system.VE_SignOfLifeIntervall.ToString("#,###,##0")) + Environment.NewLine;
            strInfo += String.Format("{0,-25}\t{1}", "TaskReadThreadDuration:", this.system.VE_TaskReadThreadDuration.ToString("#,###,##0")) + Environment.NewLine;
            strInfo += String.Format("{0,-25}\t{1}", "TaskWriteThreadDuration:", this.system.VE_TaskWriteThreadDuration.ToString("#,###,##0")) + Environment.NewLine;
            strInfo += String.Format("{0,-25}\t{1}", "MainThreadDuration:", this.system.VE_MainThreadDuration.ToString("#,###,##0")) + Environment.NewLine;
            strInfo += Environment.NewLine;

            WriteToFile(strInfo);
        }

        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Log";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Log\\ServiceInfo.txt";

            if (!System.IO.File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = System.IO.File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = System.IO.File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
        private string GetLogString(string LogString, string LogAdd, bool bNewLine)
        {
            LogString = clsViewLog.GetLogViewDateTimeString() + " - " + LogAdd;
            if (bNewLine)
            {
                LogString = LogString + Environment.NewLine;
            }
            return LogString;
        }
    }
}
