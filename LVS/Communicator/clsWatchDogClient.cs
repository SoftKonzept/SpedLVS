using LVS.InitValueWDService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
//using System.Windows.Forms;

namespace LVS
{
    public class clsWatchDogClient
    {
        /*********************************************************************************************
          *                       WD - WATCHDOG
          * ******************************************************************************************/
        internal clsSystem Sys;
        public Globals._GL_USER GL_User;

        const Int32 const_Default_Alarmfactor = 1;
        private Int32 _CMailCount;
        public Int32 CMailCount
        {
            get
            {
                return _CMailCount;
            }
            set { _CMailCount = value; }
        }
        private Int32 _Alarmfactor;
        public Int32 Alarmfactor
        {
            get
            {
                return _Alarmfactor;
            }
            set { _Alarmfactor = value; }
        }
        private DateTime _AlarmTime;
        public DateTime AlarmTime
        {
            get { return _AlarmTime; }
            set { _AlarmTime = value; }
        }
        private Int32 _Intervall;
        public Int32 Intervall
        {
            get
            {
                return _Intervall;
            }
            set { _Intervall = value; }
        }
        public DateTime CreateTime { get; set; }
        public bool IsOdetteActiv { get; set; }
        private string _FTPPaht;
        public string FTPPaht
        {
            get
            {
                return _FTPPaht;
            }
            set { _FTPPaht = value; }
        }
        //public string FilePath { get; set; }

        public const string const_Watchdog_Path = "Watchdog";
        public string FilePath
        {
            get
            {
                //return Application.StartupPath + "\\" + const_Watchdog_Path + "\\";
                return StartupPath + "\\" + const_Watchdog_Path + "\\";
            }
            //set { _FTPPaht = value; }
        }
        public string wdVitalFile
        {
            get
            {
                return "WD_" + this.Sys.Client.MatchCode.TrimEnd('_');
            }
        }
        public string HeaderClientInfo { get; set; }
        public string WDDogFileText { get; set; }
        public string StartupPath
        {
            get
            {
                return GlobalINI.GetStartUpPath();
            }
        }
        public List<string> ListCMail { get; set; }

        internal clsINI.clsINI wdINI = new clsINI.clsINI();
        /***********************************************************************************************
         *                      Procedure / Methoden
         * ********************************************************************************************/
        ///<summary>clsWatchDogClient / InitClass</summary>
        ///<remarks>Funktionsauftruf aus dem Watchdog. Hier muss die </remarks>
        public void InitClass(Globals._GL_USER myGLUser, clsSystem mySystem, string myPath, string myClientMC)
        {
            this.GL_User = myGLUser;
            Sys = mySystem;

            //StartupPath = Sys.StartupPath;
            //wdINI = new clsINI.clsINI(myPath + "\\config_WatchDog_" + myClientMC + ".ini");
            string strIniPath = myPath + "\\config_WatchDog_" + myClientMC + ".ini";
            wdINI = GlobalINI.GetINI(strIniPath);

            string strClient = InitValue.InitValue_Client.Value();

            this.Alarmfactor = const_Default_Alarmfactor;
            Int32 iTmp = InitValueWDService.InitValue_VE_Alarmfactor.Value(strClient);
            if (iTmp > 1)
            {
                this.Alarmfactor = iTmp;
            }
            //Int32.TryParse(wdINI.ReadString(myClientMC + "WATCHDOG", clsSystem.const_WD_SectionKey_Alarmfactor), out iTmp);

            //Intervall
            this.Intervall = 100;
            iTmp = InitValue_CIntervall.Value(strClient);
            if (iTmp > 100)
            {
                this.Intervall = iTmp;
            }
            //Int32.TryParse(wdINI.ReadString(myClientMC + "WATCHDOG", clsSystem.const_WD_SectionKey_CIntervall), out iTmp);

            //AlarmTime
            this.AlarmTime = DateTime.Now;
            TimeSpan Alarm = new TimeSpan(0, 0, 0, 0, (this.Intervall * this.Alarmfactor));
            this.AlarmTime.Add(Alarm);

            //Mail Count
            this.CMailCount = 1;
            iTmp = InitValue_CMailCount.Value(strClient);
            if (iTmp > 100)
            {
                this.CMailCount = iTmp;
            }
            //Int32.TryParse(wdINI.ReadString(myClientMC + "WATCHDOG", clsSystem.const_WD_SectionKey_CMailCount), out iTmp);
            //this.CMailCount = iTmp;



            //Maillist
            this.ListCMail = InitValue_WD_ListCMail.Value(strClient);
            //for (Int32 i = 1; i <= this.CMailCount; i++)
            //{
            //    string strRead = clsSystem.const_WD_SectionKey_CMail + i.ToString();
            //    string strMail = string.Empty;
            //    strMail = wdINI.ReadString(myClientMC + "WATCHDOG", strRead);
            //    if (!strMail.Equals(string.Empty))
            //    {
            //        if (strMail.Contains('@'))
            //        {
            //            ListCMail.Add(strMail);
            //        }
            //    }
            //}
        }
        ///<summary>clsWatchDogClient / InitClassByClient</summary>
        ///<remarks>Funktionsaufruf aus dem Communicator</remarks>
        public void InitClassByClient(Globals._GL_USER myGLUser, clsSystem mySystem)
        {
            this.GL_User = myGLUser;
            this.Sys = mySystem;
            this.HeaderClientInfo = "[" + this.Sys.Client.MatchCode + "_WATCHDOG] - " + mySystem.SystemInfo;
            this.Alarmfactor = mySystem.WD_Alarmfactor;
            this.Intervall = mySystem.VE_MainThreadDuration;
            //AlarmTime
            this.CreateTime = DateTime.Now;
            this.AlarmTime = this.CreateTime;
            TimeSpan Alarm = new TimeSpan(0, 0, 0, 0, (this.Intervall * this.Alarmfactor));
            if (
                 (Alarm.Minutes > 30) || (Alarm.Minutes < 15)
              )
            {
                Alarm = new TimeSpan(0, 0, 20, 0, 0);
            }
            this.AlarmTime = this.AlarmTime.Add(Alarm);
            this.CMailCount = this.Sys.WD_CMailCount;
            this.ListCMail = new List<string>();
            this.ListCMail = this.Sys.WD_ListCMail.ToList();
            this.FTPPaht = this.Sys.WD_FTPPath;
        }
        /// <summary>
        ///             Setzt die Properties der Klasse anhand der List, die durch einlesen der WD-Datei gefüllt wurde
        /// </summary>
        /// <param name="myFileData"></param>
        public void SetWDClientPropertiesbyFileData(List<string> myFileData)
        {
            WDDogFileText = string.Empty;
            this.ListCMail = new List<string>();
            foreach (string itm in myFileData)
            {
                try
                {
                    int iTmp = 1;
                    itm.Replace(" ", "");
                    if ((itm.IndexOf("=") > 0))
                    {
                        WDDogFileText += itm + Environment.NewLine;

                        string strKey = itm.Substring(0, itm.IndexOf("="));
                        string strValue = itm.Replace(strKey + "=", "");
                        switch (strKey)
                        {
                            case clsSystem.const_WD_SectionKey_CreatedTimeStamp:
                                DateTime tmpDateTime = DateTime.MinValue;
                                DateTime.TryParse(strValue, out tmpDateTime);
                                this.CreateTime = tmpDateTime;
                                break;
                            //case clsSystem.const_WD_SectionKey_Alarmfactor:
                            case InitValue_VE_Alarmfactor.const_Section:
                                iTmp = 1;
                                int.TryParse(strValue, out iTmp);
                                this.Alarmfactor = iTmp;
                                break;
                            //case clsSystem.const_WD_SectionKey_CIntervall:
                            case InitValue_CIntervall.const_Section:
                                iTmp = 1;
                                int.TryParse(strValue, out iTmp);
                                this.Intervall = iTmp;
                                break;
                            //case clsSystem.const_WD_SectionKey_CMailCount:
                            case InitValue_CMailCount.const_Section:

                                iTmp = 1;
                                int.TryParse(strValue, out iTmp);
                                this.CMailCount = iTmp;
                                break;
                            case clsWatchDog.const_WD_OdetteIsActiv:
                                this.IsOdetteActiv = false;
                                bool bTmp = false;
                                if (bool.TryParse(strValue, out bTmp))
                                {
                                    this.IsOdetteActiv = bTmp;
                                }
                                break;
                            case clsWatchDog.const_WD_AlarmTime:
                                this.AlarmTime = DateTime.MinValue;
                                DateTime tmpAlarm = DateTime.MinValue;
                                if (DateTime.TryParse(strValue, out tmpDateTime))
                                {
                                    this.AlarmTime = tmpDateTime;
                                }
                                break;
                            default:
                                //if (strKey.Contains(clsSystem.const_WD_SectionKey_CMail.ToString()))
                                //{
                                //    if (strValue.Contains("@"))
                                //    {
                                //        ListCMail.Add(strValue);
                                //    }
                                //}
                                if (strKey.Contains(InitValue_WD_ListCMail.const_Section.ToString()))
                                {
                                    if (strValue.Contains("@"))
                                    {
                                        ListCMail.Add(strValue);
                                    }
                                }
                                break;
                        }
                    }
                    else
                    {
                        this.HeaderClientInfo = itm;
                    }
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                }
            }
        }

        /// <summary>
        ///             Erstellt die WatchDog Datei 
        /// </summary>
        /// <param name="mySystem"></param>
        /// <param name="strAppPath"></param>
        public string CreateWDFile()
        {
            string strReturn = string.Empty;
            this.CreateTime = DateTime.Now;
            List<string> listFile = new List<string>();
            listFile.Add(this.HeaderClientInfo);

            //createdTimeStamp
            listFile.Add(clsSystem.const_WD_SectionKey_CreatedTimeStamp + "=" + this.CreateTime.ToString());
            //AlarmTime
            listFile.Add(clsWatchDog.const_WD_AlarmTime + "=" + this.AlarmTime.ToString());
            //Alarmfactor
            listFile.Add(InitValue_VE_Alarmfactor.const_Section + "=" + this.Alarmfactor.ToString());
            //Intervall
            listFile.Add(InitValue_CIntervall.const_Section + "=" + this.Intervall.ToString());

            //Mailcount
            listFile.Add(InitValue_CMailCount.const_Section + "=" + this.CMailCount.ToString());

            //neu laden
            this.ListCMail = this.Sys.WD_ListCMail.ToList();
            for (Int32 i = 1; i <= this.CMailCount; i++)
            {
                string strTmp = string.Empty;
                if (i <= this.ListCMail.Count - 1)
                {
                    strTmp = this.ListCMail[i].ToString();
                }
                //listFile.Add(clsSystem.const_WD_SectionKey_CMail + i.ToString() + "=" + strTmp);
                listFile.Add(InitValue_WD_ListCMail.const_Section + i.ToString() + "=" + strTmp);
            }
            //--- check Odtte gestartet
            this.IsOdetteActiv = Check_IsOdetteActiv();
            listFile.Add(clsWatchDog.const_WD_OdetteIsActiv + "=" + this.IsOdetteActiv.ToString());

            foreach (string itm in listFile)
            {
                strReturn += itm + Environment.NewLine;
            }
            //Schreiben der Datei
            if (listFile.Count > 0)
            {
                helper_IOFile.CheckPath(FilePath);
                //string strStartupPath = StartupPath
                string strFilePath = FilePath + "\\" + wdVitalFile + ".txt";
                System.IO.File.WriteAllLines(strFilePath, listFile.ToArray());
                strReturn += "File: " + strFilePath;
            }
            return strReturn;
        }
        /// <summary>
        ///             CHeck ob Odette gestartet ist
        /// </summary>
        /// <returns></returns>
        private bool Check_IsOdetteActiv()
        {
            bool bReturn = false;
            //Process[] odette = Process.GetProcessesByName(this.Sys.WD_OdetteProcessName);
            Process[] odette = Process.GetProcesses();

            //List<string> tmp = new List<string>();
            foreach (Process p in odette)
            {
                string strName = p.ProcessName;
                //tmp.Add(p.ProcessName);
                if (p.ProcessName.Contains(this.Sys.WD_OdetteProcessName))
                {
                    bReturn = true;
                    break;
                }
            }
            //bReturn = tmp.Contains(this.Sys.WD_OdetteProcessName);
            return bReturn;
        }
        /// <summary>
        ///             Löschen der vorhanden Files im WatchDogOrdner
        /// </summary>
        public string DeleteOldWatchDogFiles()
        {
            string strReturn = string.Empty;
            bool bReturn = true;
            try
            {
                string strSearchDatei = "*" + this.Sys.Client.MatchCode.TrimEnd('_') + ".txt";

                helper_IOFile.CheckPath(this.FilePath);

                DirectoryInfo di = new DirectoryInfo(this.FilePath);
                FileInfo[] files = di.GetFiles(strSearchDatei);
                foreach (FileInfo f in files)
                {
                    f.Delete();
                    strReturn += "File:" + f.FullName;
                }
                bReturn = true;
            }
            catch (Exception ex)
            {
                clsError Error = new clsError();
                Error.InitClass(this.GL_User, this.Sys);
                Error.Aktion = "clsWatchDogClient - DeleteOldWatchDogFiles";
                Error.Datum = DateTime.Now;
                Error.ErrorText = string.Empty;
                Error.exceptText = ex.InnerException.ToString();
                Error.WriteError();
                bReturn = false;
                strReturn = ex.InnerException.ToString();
            }
            return strReturn;
        }
        ///





    }
}
