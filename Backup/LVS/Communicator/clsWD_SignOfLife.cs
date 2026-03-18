using System;
using System.Collections.Generic;
using System.IO;
//using System.Windows.Forms;

namespace LVS
{
    public class clsWD_SignOfLife
    {
        internal clsSystem System;
        internal Globals._GL_USER GLUser;

        public const string const_WDSignOfLife_FileName = "WD_SignOfLife.txt";
        public const string const_AliveMail_Subject = "WatchDog - SIGN of LIFE - Prozess OK";
        public const string const_CloseMail_Subject = "WatchDog - App Closed !!!";
        public const string const_ErrorMail_Subject = "WatchDog - SIGN of LIFE - Prozess fehlerhaft / ERROR";
        public const string const_InfoMail_Subject = "WatchDog - SIGN of LIFE - Neustart";

        internal List<string> ListSignOfLife;
        public bool IsSignOfLifeMailsendet = false;

        public string Infotext { get; set; }

        public string FilePath
        {
            get
            {
                string tmp = string.Empty;
                tmp = this.Path + "\\" + const_WDSignOfLife_FileName;
                return tmp;
            }
        }
        public string Path
        {
            get
            {
                string tmp = string.Empty;
                //tmp = Application.StartupPath + "\\" + clsWatchDogClient.const_Watchdog_Path;
                tmp = StartupPath + "\\" + clsWatchDogClient.const_Watchdog_Path;
                helper_IOFile.CheckPath(tmp);
                return tmp;
            }
        }
        public string StartupPath { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public clsWD_SignOfLife(Globals._GL_USER myGLUser, clsSystem mySystem, bool myInitProcess)
        {
            this.System = mySystem;
            this.GLUser = myGLUser;
            this.StartupPath = this.System.StartupPath;

            ListSignOfLife = new List<string>();
            Infotext = string.Empty;
            if (myInitProcess)
            {
                InitProcess();
            }
            else
            {
                DeleteSignOfLifeFile();
            }
        }
        /// <summary>
        ///          Check File Signoflife
        ///             - Datei fehlt > neuer erstellen > InfoMail senden
        ///             - Datei vorhanden
        ///                 > SignOfLife > 5 Std. > Mail Alive
        /// </summary>
        private void InitProcess()
        {
            string strSrvInfo = this.System.SystemInfo;
            if (helper_IOFile.CheckFile(this.FilePath))
            {
                string strLine = helper_IOFile.ReadFileInLine(this.FilePath);
                strLine = strLine.TrimStart();
                DateTime dtSignOfLifeFile = Globals.DefaultDateTimeMinValue;
                if (DateTime.TryParse(strLine, out dtSignOfLifeFile))
                {
                    double dTmp = (double)this.System.VE_SignOfLifeIntervall;
                    DateTime dtAlarmInfo = dtSignOfLifeFile.AddMilliseconds(dTmp);

                    if (dtAlarmInfo <= DateTime.Now)
                    {
                        //Infomail erstellen
                        clsMail AliveMail = new clsMail();
                        AliveMail.InitClass(this.GLUser, this.System);
                        AliveMail.Subject = strSrvInfo + " - " + const_AliveMail_Subject;
                        string strMes = "Alle Watchdog-Prozesse laufen problemlos......" + Environment.NewLine;
                        strMes += "aktuelles SignOfLife-Intervall: " + this.System.VE_SignOfLifeIntervall.ToString("####.##") + Environment.NewLine;
                        strMes += "Dauer SignOfLife-Sek./Min.: " + (this.System.VE_SignOfLifeIntervall / 1000).ToString("####.##") + "/" + ((this.System.VE_SignOfLifeIntervall / 1000) / 60).ToString("####.##") + Environment.NewLine;
                        AliveMail.Message = strMes;
                        if (this.System.VE_List_WDInfoMail.Count > 0)
                        {
                            AliveMail.ListMailReceiver.AddRange(this.System.VE_List_WDInfoMail);
                            AliveMail.Send_WD_SignOfLife();
                            IsSignOfLifeMailsendet = true;
                        }
                        else
                        {
                            AliveMail.Subject += " -> !!! keine EMail-Adresse angegeben !!!";
                            AliveMail.SendError();
                        }
                        this.Infotext = AliveMail.Subject;
                        CreateSignOfLifeFile();
                    }
                    else
                    {
                        this.Infotext = "(dtAlarmInfo <= DateTime.Now)";
                    }
                }
                else
                {
                    CreateSignOfLifeFile();
                    //Infomail erstellen
                    clsMail AliveMail = new clsMail();
                    AliveMail.InitClass(this.GLUser, this.System);
                    AliveMail.Subject = strSrvInfo + " - " + const_ErrorMail_Subject;
                    string strMes = "SignOfLife - File fehlerhaft - Datei neu erstellt......" + Environment.NewLine;
                    strMes += "aktuelles SignOfLife-Intervall: " + this.System.VE_SignOfLifeIntervall.ToString() + Environment.NewLine;
                    strMes += "Dauer SignOfLife-Sek./Min.: " + (this.System.VE_SignOfLifeIntervall / 1000).ToString("####.##") + "/" + ((this.System.VE_SignOfLifeIntervall / 1000) / 60).ToString("####.##") + Environment.NewLine;
                    AliveMail.Message = strMes;

                    if (this.System.VE_List_WDInfoMail.Count > 0)
                    {
                        AliveMail.ListMailReceiver.AddRange(this.System.VE_List_WDInfoMail);
                        AliveMail.Send_WD_SignOfLife();
                        IsSignOfLifeMailsendet = true;

                    }
                    else
                    {
                        AliveMail.Subject += " -> !!! keine EMail-Adresse angegeben !!!";
                        AliveMail.SendError();
                    }
                    this.Infotext = AliveMail.Subject;
                }
            }
            else
            {
                CreateSignOfLifeFile();

                //Infomail erstellen
                clsMail AliveMail = new clsMail();
                AliveMail.InitClass(this.GLUser, this.System);
                AliveMail.Subject = strSrvInfo + " - " + const_InfoMail_Subject;
                string strMes = "SignOfLife - Neustart / File liegt nicht vor - File neu erstellt......" + Environment.NewLine;
                strMes += "aktuelles SignOfLife-Intervall: " + this.System.VE_SignOfLifeIntervall.ToString() + Environment.NewLine;
                strMes += "Dauer SignOfLife-Sek./Min.: " + (this.System.VE_SignOfLifeIntervall / 1000).ToString("####.##") + "/" + ((this.System.VE_SignOfLifeIntervall / 1000) / 60).ToString("####.##") + Environment.NewLine;
                AliveMail.Message = strMes;

                if (this.System.VE_List_WDInfoMail.Count > 0)
                {
                    AliveMail.ListMailReceiver.AddRange(this.System.VE_List_WDInfoMail);
                    AliveMail.Send_WD_SignOfLife();
                    IsSignOfLifeMailsendet = true;

                }
                else
                {
                    AliveMail.Subject += " -> !!! keine EMail-Adresse angegeben !!!";
                    AliveMail.SendError();
                }
                this.Infotext = AliveMail.Subject;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateSignOfLifeFile()
        {
            ListSignOfLife.Clear();
            string strTmp = DateTime.Now.ToString();
            ListSignOfLife.Add(strTmp);
            helper_IOFile.WriteFileInLine(this.FilePath, ListSignOfLife);
        }
        /// <summary>
        /// 
        /// </summary>
        public void DeleteSignOfLifeFile()
        {
            if (File.Exists(this.FilePath))
            {
                //File löschen
                File.Delete(this.FilePath);
            }
        }





    }
}
