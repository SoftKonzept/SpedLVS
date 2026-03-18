using System;
using System.Collections.Generic;

namespace LVS
{
    public class clsWatchDog
    {
        internal Globals._GL_USER GLUser;
        internal clsSystem Sys;
        public const string const_WD_OdetteIsActiv = "OdetteIsActiv";
        public const string const_WD_AlarmTime = "AlarmTime";
        internal clsWatchDogClient WDClient;
        public string InfoText { get; set; }

        public bool IsOdetteActiv
        {
            get
            {
                bool bReturn = false;
                if (this.WDClient is clsWatchDogClient)
                {
                    bReturn = this.WDClient.IsOdetteActiv;
                }
                return bReturn;
            }
        }
        public bool IsDateTimeDiffAlarm
        {
            get
            {
                bool bReturn = false;
                if (this.WDClient is clsWatchDogClient)
                {
                    bReturn = (DateTime.Now > this.WDClient.AlarmTime);
                }
                return bReturn;
            }
        }
        public string MailSubject
        {
            get
            {
                string strTxt = "!!! WatchDog-ALARM !!! - ";
                strTxt += "[" + InitValue.InitValue_SystemInfo_ID.Value() + "] - ";
                if (this.WDClient is clsWatchDogClient)
                {
                    strTxt += this.WDClient.HeaderClientInfo;
                }
                return strTxt;
            }
        }
        public string MailMessage
        {
            get
            {
                string strTxt = "WatchDog-File-Daten: " + Environment.NewLine;
                if (this.WDClient is clsWatchDogClient)
                {
                    strTxt += "[" + InitValue.InitValue_SystemInfo_ID.Value() + "]" + Environment.NewLine;
                    strTxt += this.WDClient.WDDogFileText;
                }
                return strTxt;
            }
        }

        //******************************************************************************************************************************
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myGLUser"></param>
        /// <param name="mySys"></param>
        public clsWatchDog(Globals._GL_USER myGLUser, clsSystem mySys)
        {
            this.GLUser = myGLUser;
            this.Sys = mySys;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myWDFileData"></param>
        public void AnlayseWDFileData(List<string> myWDFileData)
        {
            InfoText = string.Empty;
            if (myWDFileData.Count > 0)
            {
                WDClient = new clsWatchDogClient();
                WDClient.SetWDClientPropertiesbyFileData(myWDFileData);

                //Verarbeitung aufgrund der gefüllten WDClient-Klasse
                if ((!this.IsOdetteActiv) || (this.IsDateTimeDiffAlarm))
                {
                    //Alarmmail erstellen
                    clsMail AlarmMail = new clsMail();
                    AlarmMail.InitClass(this.GLUser, this.Sys);
                    AlarmMail.Subject = this.MailSubject;
                    AlarmMail.Message = this.MailMessage;
                    AlarmMail.ListMailReceiver = this.WDClient.ListCMail;
                    if (AlarmMail.ListMailReceiver.Count > 0)
                    {
                        AlarmMail.Send_WDAlarm();
                    }
                    else
                    {
                        AlarmMail.Subject += " -> !!! keine EMail-Adresse angegeben !!!";
                        AlarmMail.SendError();
                    }
                    InfoText += "Alarm-Mail versendet.... !!!";
                }
                else
                {
                    InfoText += "Prozesse OK !!!!";
                }
            }
            else
            {
                InfoText += "Keine Filedaten vorhanden !!!";
            }
        }


    }
}
