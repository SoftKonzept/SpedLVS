using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;

namespace LVS
{
    public class clsMail
    {
        //internal clsADR ADR;
        internal clsError Error;
        internal clsSystem system;
        internal MailMessage MailToSend;
        internal SmtpClient MailSMTPClient;
        internal Attachment Attachment;
        internal string MailReceiverString;

        //internal ctrMailCockpit _ctrMailCockpit;
        // internal ctrMenu _ctrMenu;
        public clsEmails emails;

        const Int32 const_DefaultSMTPPort = 587;

        public Globals._GL_USER _GL_User;
        public Globals._GL_SYSTEM _GL_System;
        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = _GL_User.User_ID;
                return _BenutzerID;
            }
            set { _BenutzerID = value; }
        }

        public List<string> ListMailReceiver { get; set; }
        public List<string> ListMailReceiverAdmin { get; set; }

        private string _MailReceivers;
        public string MailReceivers
        {
            get
            {
                _MailReceivers = string.Join(",", ListMailReceiver.ToArray());
                return _MailReceivers;
            }
            set { _MailReceivers = value; }
        }

        public List<string> ListAttachment { get; set; } = new List<string>();

        public string Subject { get; set; }
        public string Message { get; set; }
        public string MailFrom { get; set; }
        public string MailBCC { get; set; }
        public string SMTPServer { get; set; }
        public Int32 SMTPPort { get; set; }
        public string SMTPUser { get; set; }
        public string SMTPPasswort { get; set; }
        public bool SMTPSsl { get; set; }
        public string strErrorInfo { get; set; }
        /********************************************************************************************************
         *                                              Methoden
         * ******************************************************************************************************/

        public clsMail()
        {
            MailReceiverString = string.Empty;
        }

        public clsMail(bool myUseSystemSettings) : this()
        {
            if (myUseSystemSettings)
            {
                SetLVSConstMailConfig();
            }
        }


        ///<summary>clsMail / InitClass</summary>
        ///<remarks>Initialisiert die Klasse ADR und deren Subclasses</remarks>
        public void InitClass(Globals._GL_USER myGLUser, clsSystem mySystem)
        {
            //this._ctrMenu = ctrMenu;
            this._GL_User = myGLUser;
            this.system = mySystem;
            this.ListMailReceiver = new List<string>();
            this.ListAttachment = new List<string>();
            //List Adminreceiverlist
            this.ListMailReceiverAdmin = new List<string>();
            this.ListMailReceiverAdmin.Add(clsSystem.const_MailAdress);

            Error = new clsError();
            Error._GL_User = this._GL_User;

            emails = new clsEmails();
            emails.InitClass(this._GL_User, this._GL_System);

            if (system == null)
            {
                system = new clsSystem();
            }

            if (system.VE_IsWatchDog)
            {
                SetLVSConstMailConfig();
            }
            else
            {
                if (
                        (this._GL_User.IsAdmin) &&
                        (this._GL_User.Name.ToUpper().Equals("ADMINISTRATOR"))
                   )
                {
                    SetLVSConstMailConfig();
                }
                else
                {
                    if (system.Client is clsClient)
                    {
                        if (system.Client.Modul.Mail_UsingMainMailForMailing)
                        {
                            SetSystemMailConfig();
                        }
                        else
                        {
                            this.SMTPServer = this._GL_User.SMTPServer;
                            this.SMTPUser = this._GL_User.SMTPUser;
                            this.SMTPPasswort = this._GL_User.SMTPPass;
                            this.SMTPPort = this._GL_User.SMTPPort;
                            this.MailFrom = this._GL_User.Mail;
                            this.SMTPSsl = true;
                        }
                    }
                }
            }
        }
        ///<summary>clsMail / SetSystemMailConfig</summary>
        ///<remarks></remarks>
        private void SetSystemMailConfig()
        {
            this.SMTPServer = system.Client.Modul.Mail_SMTPServer;
            this.SMTPUser = system.Client.Modul.Mail_SMTPUser;
            this.SMTPPasswort = system.Client.Modul.Mail_SMTPPasswort;
            this.SMTPPort = system.Client.Modul.Mail_SMTPPort;
            this.MailFrom = system.Client.Modul.Mail_MailAdress;
            this.SMTPSsl = system.Client.Modul.Mail_SMTPSSL;

        }
        ///<summary>clsMail / SetSystemMailConfig</summary>
        ///<remarks></remarks>
        private void SetLVSConstMailConfig()
        {
            this.SMTPServer = clsSystem.const_Mail_SMTPServer;
            this.SMTPUser = clsSystem.const_Mail_SMTPUser;
            this.SMTPPasswort = clsSystem.const_Mail_SMTPPasswort;
            this.SMTPPort = clsSystem.const_Mail_SMTPPort;
            this.MailFrom = clsSystem.const_MailAdress;
            this.SMTPSsl = true;
        }
        ///<summary>clsMail / SendError</summary>
        ///<remarks></remarks>
        public void SendError()
        {
            this.SMTPServer = clsSystem.const_Mail_SMTPServer;
            this.SMTPPort = clsSystem.const_Mail_SMTPPort;
            this.SMTPUser = clsSystem.const_Mail_SMTPUser;
            this.SMTPPasswort = clsSystem.const_Mail_SMTPPasswort;

            this.SMTPSsl = true;

            MailToSend = new MailMessage();
            //Absender
            MailToSend.From = new MailAddress(clsSystem.const_MailAdress);
            //Empfänger
            MailToSend.To.Add(clsSystem.const_MailAdress);
            //Betreff
            MailToSend.Subject = Subject;
            //Mailnachrichtstext
            MailToSend.Body = Message;
            //Ausgangsserver initialisieren
            InitSMTPClient();
            //Attachment
            InitAttachment();
            //Mail senden
            MailSMTPClient.Send(MailToSend);

            if (!this.system.VE_IsWatchDog)
            {
                clsLogbuch maillog = new clsLogbuch();
                maillog.Aktion = "Email";
                maillog.BenutzerID = this._BenutzerID;
                maillog.Beschreibung = "Email :" + Subject + " an " + clsSystem.const_MailAdress + " versand";
                maillog.LogbuchInsert();
            }
        }

        ///<summary>clsMail / SendError</summary>
        ///<remarks></remarks>
        public void SendCheckMail()
        {
            this.SMTPServer = clsSystem.const_Mail_SMTPServer;
            this.SMTPPort = clsSystem.const_Mail_SMTPPort;
            this.SMTPUser = clsSystem.const_Mail_SMTPUser;
            this.SMTPPasswort = clsSystem.const_Mail_SMTPPasswort;

            this.SMTPSsl = true;

            MailToSend = new MailMessage();
            //Absender
            MailToSend.From = new MailAddress(clsSystem.const_MailAdress);
            //Empfänger
            MailToSend.To.Add(clsSystem.const_MailAdress);
            //Betreff
            MailToSend.Subject = Subject;
            //Mailnachrichtstext
            MailToSend.Body = Message;
            //Ausgangsserver initialisieren
            InitSMTPClient();
            //Attachment
            InitAttachment();
            //Mail senden
            MailSMTPClient.Send(MailToSend);
        }
        /// <summary>
        /// 
        /// </summary>
        public void Send_WDAlarm()
        {
            try
            {
                this.SMTPServer = clsSystem.const_Mail_SMTPServer;
                this.SMTPPort = clsSystem.const_Mail_SMTPPort;
                this.SMTPUser = clsSystem.const_Mail_SMTPUser;
                this.SMTPPasswort = clsSystem.const_Mail_SMTPPasswort;

                MailToSend = new MailMessage();
                //Absender
                MailToSend.From = new MailAddress(clsSystem.const_MailAdress);
                //Empfänger
                //MailToSend.To.Add(clsSystem.const_MailAdress);
                InitMailReceiver();
                //Betreff
                MailToSend.Subject = Subject;
                //Mailnachrichtstext
                MailToSend.Body = Message;
                //Ausgangsserver initialisieren
                InitSMTPClient();
                //Attachment
                InitAttachment();
                //Mail senden
                MailSMTPClient.Send(MailToSend);
            }
            catch (Exception ex)
            {
                clsError Error = new clsError();
                //Error.Sys = new clsSystem();
                //Error._GL_User = null;
                Error.Aktion = "frmMainCom - clsMail.cs ->  Send_WDAlarm";
                Error.Datum = DateTime.Now;
                Error.ErrorText = ex.Message.ToString();
                Error.exceptText = ex.ToString();
                Error.WriteError();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Send_WD_SignOfLife()
        {
            try
            {
                this.SMTPServer = clsSystem.const_Mail_SMTPServer;
                this.SMTPPort = clsSystem.const_Mail_SMTPPort;
                this.SMTPUser = clsSystem.const_Mail_SMTPUser;
                this.SMTPPasswort = clsSystem.const_Mail_SMTPPasswort;

                MailToSend = new MailMessage();
                //Absender
                MailToSend.From = new MailAddress(clsSystem.const_MailAdress);
                //Empfänger
                //MailToSend.To.Add(clsSystem.const_MailAdress);
                InitMailReceiver();
                //Betreff
                MailToSend.Subject = Subject;
                //Mailnachrichtstext
                MailToSend.Body = Message;
                //Ausgangsserver initialisieren
                InitSMTPClient();
                //Attachment
                InitAttachment();
                //Mail senden
                MailSMTPClient.Send(MailToSend);
            }
            catch (Exception ex)
            {
                clsError Error = new clsError();
                //Error.Sys = new clsSystem();
                //Error._GL_User = null;
                Error.Aktion = "frmMainCom - clsMail.cs ->  Send_WD_SignOfLife";
                Error.Datum = DateTime.Now;
                Error.ErrorText = ex.Message.ToString();
                Error.exceptText = ex.ToString();
                Error.WriteError();
            }
        }

        ///<summary>clsMail / InitClass</summary>
        ///<remarks>Initialisiert die Klasse ADR und deren Subclasses</remarks>
        public bool SendNoReply()
        {
            bool bReturn = false;
            try
            {
                this.MailFrom = this.system.Client.Modul.Mail_Noreply_MailAdress;
                //Check Mailangaben
                if (
                    //(this.SMTPUser != string.Empty) &
                    //(this.SMTPServer != string.Empty) &
                    //(this.SMTPPasswort != string.Empty) &
                    //(this.MailFrom != string.Empty)
                    (this.system.Client.Modul.Mail_Noreply_SMTPUser != string.Empty) &
                    (this.system.Client.Modul.Mail_Noreply_SMTPServer != string.Empty) &
                    (this.system.Client.Modul.Mail_Noreply_SMTPPasswort != string.Empty) &
                    (this.system.Client.Modul.Mail_Noreply_SMTPPort > 0) &
                    (this.MailFrom != string.Empty)
                  )
                {
                    this.MailFrom = this.system.Client.Modul.Mail_Noreply_MailAdress;
                    this.SMTPUser = this.system.Client.Modul.Mail_Noreply_SMTPUser;
                    this.SMTPServer = this.system.Client.Modul.Mail_Noreply_SMTPServer;
                    this.SMTPPasswort = this.system.Client.Modul.Mail_Noreply_SMTPPasswort;
                    this.SMTPPort = this.system.Client.Modul.Mail_Noreply_SMTPPort;
                    this.SMTPSsl = this.system.Client.Modul.Mail_Noreply_SMTPSSL;

                    MailToSend = new MailMessage();
                    //Absender
                    if (this.system.DebugModeCOM)
                    {
                        MailToSend.From = new MailAddress(clsSystem.const_MailAdress);
                    }
                    else
                    {
                        MailToSend.From = new MailAddress(this.MailFrom);
                    }
                    //Empfänger
                    InitMailReceiver();
                    //Betreff
                    MailToSend.Subject = Subject;
                    //Mailnachrichtstext
                    MailToSend.Body = "Dies ist eine automatisch generierte Nachricht, bitte antworten Sie nicht an diesen Absender.";
                    if (this.system.DebugModeCOM)
                    {
                        MailToSend.Body += Environment.NewLine + Environment.NewLine + "---- DEBUG MODE COM - MAIL AN ADMIN ----" + Environment.NewLine;
                        MailToSend.Body += MailReceiverString;
                    }
                    //Ausgangsserver initialisieren
                    InitSMTPClient();
                    //Attachment
                    InitAttachment();
                    //Mail senden
                    MailSMTPClient.Send(MailToSend);
                    Log();
                    bReturn = true;

                }
            }
            catch (Exception ex)
            {
                /****
                 * bReturn = false;
                clsMessages.Allgemein_ERRORTextShow(ex.ToString());
                Error = new clsError();
                Error.Mail = this;
                Error._GL_User = this._GL_User;
                Error.Aktion = clsError.code1_401;
                Error.exceptText = ex.ToString();
                Error.WriteError();
                 * ***/
            }
            finally
            {
                if (this.Attachment != null)
                {
                    this.Attachment.Dispose();
                }
            }
            return bReturn;
        }
        ///<summary>clsMail / CheckForErrorInfo</summary>
        ///<remarks></remarks>
        private void CheckForErrorInfo()
        {
            strErrorInfo = string.Empty;
            if (this.MailFrom == string.Empty)
            {
                strErrorInfo = strErrorInfo + "- Absendermailadresse fehlt" + Environment.NewLine;
            }
            if (this.SMTPUser == string.Empty)
            {
                strErrorInfo = strErrorInfo + "- SMTPUser fehlt" + Environment.NewLine;
            }
            if (this.SMTPPasswort == string.Empty)
            {
                strErrorInfo = strErrorInfo + "- SMTPServer fehlt" + Environment.NewLine;
            }
            if (this.SMTPPasswort == string.Empty)
            {
                strErrorInfo = strErrorInfo + "- Passwort fehlt" + Environment.NewLine;
            }
            if (strErrorInfo != string.Empty)
            {
                strErrorInfo = "Folgende Angaben fehlen:" + Environment.NewLine + strErrorInfo;
            }
        }
        ///<summary>clsMail / InitClass</summary>
        ///<remarks>Initialisiert die Klasse ADR und deren Subclasses</remarks>
        public bool Send()
        {
            bool bReturn = false;
            try
            {
                CheckForErrorInfo();
                if (strErrorInfo == string.Empty)
                {
                    MailToSend = new MailMessage();
                    //Absender
                    MailToSend.From = new MailAddress(this.MailFrom);
                    if ((this.MailBCC != null) && (this.MailBCC != string.Empty))
                    {
                        MailToSend.Bcc.Add(this.MailBCC);                        //MailToSend.Bcc.Add(this._GL_User.Mail);
                    }
                    //Empfänger
                    InitMailReceiver();
                    //Betreff
                    MailToSend.Subject = Subject;
                    //Mailnachrichtstext
                    MailToSend.Body = Message;
                    //Ausgangsserver initialisieren
                    InitSMTPClient();
                    if (this.MailSMTPClient is SmtpClient)
                    {
                        //Attachment
                        InitAttachment();
                        //Mail senden
                        MailSMTPClient.Send(MailToSend);
                        Log();
                        bReturn = true;
                    }
                    else
                    {
                        bReturn = false;
                        Error = new clsError();
                        Error.Mail = this;
                        Error._GL_User = this._GL_User;
                        Error.Aktion = clsError.code1_401;
                        Error.exceptText = "MailSMTPClient is NULL !!!";
                        Error.WriteError();

                        this.Subject = "Error bei Mailversand - [Originalbetreff: " + this.Subject + "]";
                        this.Message = "Errortext: " + Environment.NewLine +
                                        "Host: " + this.SMTPServer + Environment.NewLine +
                                        "Port: " + this.SMTPPort + Environment.NewLine +
                                        "User: " + this.SMTPUser + Environment.NewLine +
                                        "SSL:" + Convert.ToInt32(this.SMTPSsl) + Environment.NewLine +

                                        this.Message;
                        SendError();
                        clsMessages.Allgemein_ERRORTextShow(this.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                bReturn = false;
                clsMessages.Allgemein_ERRORTextShow(ex.ToString());
                Error = new clsError();
                Error.Mail = this;
                Error._GL_User = this._GL_User;
                Error.Aktion = clsError.code1_401;
                Error.exceptText = ex.ToString();
                Error.WriteError();

                this.Subject = "Error bei Mailversand - [Originalbetreff: " + this.Subject + "]";
                this.Message = "Errortext: " + Environment.NewLine +
                                ex.ToString() + Environment.NewLine +
                                "[Originaltext]: " + Environment.NewLine +
                                this.Message;
                SendError();
            }
            finally
            {

            }
            return bReturn;
        }
        ///<summary>clsMail / InitMailReceiver</summary>
        ///<remarks></remarks>
        private void InitMailReceiver()
        {
            if (this.system.DebugModeCOM)
            {                 //Im Debugmodus immer an den Admin senden
                for (Int32 i = 0; i <= ListMailReceiverAdmin.Count - 1; i++)
                {
                    string strTmpReceiver = this.ListMailReceiverAdmin[i].ToString();
                    MailReceiverString += (i+1).ToString("00") + " : " + strTmpReceiver + Environment.NewLine;
                }
                MailToSend.To.Add(clsSystem.const_MailAdress);
                return;
            }
            else
            {
                if (this.ListMailReceiver.Count > 0)
                {
                    for (Int32 i = 0; i <= ListMailReceiver.Count - 1; i++)
                    {
                        string strTmpReceiver = this.ListMailReceiver[i].ToString();
                        if (!strTmpReceiver.Equals(string.Empty))
                        {
                            try
                            {
                                MailToSend.To.Add(strTmpReceiver);
                            }
                            finally
                            { }
                        }
                    }
                }
            }
        }
        ///<summary>clsMail / InitSMTPClient</summary>
        ///<remarks>Initialisiert den SMTP Server</remarks>
        private void InitSMTPClient()
        {
            if (
                (!this.SMTPUser.Equals(string.Empty)) &
                (this.SMTPPort > 0)
               )
            {
                try
                {
                    MailSMTPClient = new SmtpClient(this.SMTPServer, this.SMTPPort);
                    //Login Konfigurieren
                    System.Net.NetworkCredential Credentials = new System.Net.NetworkCredential(this.SMTPUser, this.SMTPPasswort);
                    //MailSMTPClient.Credentials = Credentials;
                    //MailSMTPClient.UseDefaultCredentials = true;
                    // ALS OPTION?
                    MailSMTPClient.UseDefaultCredentials = false;
                    // BAUSTELLE AKTUELL IMMER SSL .. gibt aktuell kaum noch Anbieter ohne
                    // HONSELMANN verwendet 1und1 .. SSL aktiv 
                    MailSMTPClient.EnableSsl = this.SMTPSsl;
                    MailSMTPClient.Credentials = Credentials;
                }
                finally
                { }
            }
        }
        ///<summary>clsMail / InitAttachment</summary>
        ///<remarks>Initialisiert die Mailanhänge</remarks>
        private void InitAttachment()
        {
            if (this.ListAttachment.Count > 0)
            {
                for (Int32 i = 0; i <= this.ListAttachment.Count - 1; i++)
                {
                    if (File.Exists(this.ListAttachment[i].ToString()))
                    {
                        try
                        {
                            Attachment = new Attachment(this.ListAttachment[i].ToString());
                            MailToSend.Attachments.Add(Attachment);
                        }
                        finally
                        {

                        }
                    }
                }
            }
        }
        ///<summary>clsMail / FillMailReceiverList</summary>
        ///<remarks>Füllt die Receiverstringlist </remarks>
        public void FillMailReceiverList(DataTable myDT)
        {
            ListMailReceiver = new List<string>();
            for (Int32 i = 0; i <= myDT.Rows.Count - 1; i++)
            {
                if (myDT.Rows[i]["Select"] != null)
                {
                    if ((bool)myDT.Rows[i]["Select"])
                    {
                        string strMail = myDT.Rows[i]["Mail"].ToString();
                        ListMailReceiver.Add(strMail);
                    }
                }
            }
        }
        ///<summary>clsMail / sendTest</summary>
        ///<remarks></remarks>
        public bool SendTest(string myMailFrom, string myMailTo, string mySMTPServer, string mySMTPUser, string mySMTPPass, Int32 mySMTPPort)
        {
            bool retValue = false;

            if (
                (myMailFrom != string.Empty) &&
                (myMailTo != string.Empty) &&
                (mySMTPServer != string.Empty) &&
                (mySMTPUser != string.Empty) &&
                (mySMTPPass != string.Empty)
              )
            {
                try
                {
                    retValue = true;
                    this.SMTPServer = mySMTPServer;
                    this.SMTPPasswort = mySMTPPass;
                    this.SMTPUser = mySMTPUser;
                    this.SMTPPort = mySMTPPort;
                    MailToSend = new MailMessage();
                    //Absender
                    MailToSend.From = new MailAddress(myMailFrom);
                    MailToSend.Bcc.Add(myMailTo);
                    //Betreff
                    MailToSend.Subject = "E-Mail Test";
                    //Mailnachrichtstext
                    MailToSend.Body = "";
                    //Ausgangsserver initialisieren
                    InitSMTPClient();
                    //Mail senden
                    MailSMTPClient.Send(MailToSend);
                }
                catch (Exception ex)
                {
                    retValue = false;
                }

            }
            return retValue;
        }
        /// <summary>
        /// clsMail / Log
        /// </summary>
        private void Log()
        {

            string Attachments = string.Empty;
            for (int i = 0; i < MailToSend.Attachments.Count; i++)
            {
                Attachments += MailToSend.Attachments.ElementAt(i).Name.ToString();
                if ((i + 1) < MailToSend.Attachments.Count)
                {
                    Attachments += ", ";
                }
            }
            emails = new clsEmails();
            emails.InitClass(this._GL_User, this._GL_System);
            emails.Absender = MailToSend.From.Address.ToString();
            emails.Empfaenger = MailToSend.To.ToString();
            emails.Text = MailToSend.Body.ToString();
            emails.Dateien = Attachments;
            emails.Betreff = MailToSend.Subject;
            emails.Add();
        }



    }
}
