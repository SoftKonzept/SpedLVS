using System;
using System.IO;
//using System.Windows.Forms;



namespace LVS
{
    public class clsError
    {
        internal clsMail Mail;
        public clsSystem Sys;
        public Globals._GL_USER _GL_User;
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

        //Error Codes
        /***********************************************************************************
         * 1 - System
         *     1.001 usw
         * 2 - Stammdaten
         * 3 - Auftragserfassung
         * 4 - Disposition
         * 5 - Lager
         *     5.001
         * 6 - Statistik
         * 
         * ********************************************************************************/

        //System
        //--Datenbankverbindung
        public const string code1_101 = "System | DB Connection | Versuch eine DB-Connection herzustellen";
        //--Mail
        public const string code1_401 = "System | Mailverkehr | Mail konnte nicht versand werden";
        //--Excelexport
        public const string code1_501 = "System - Fehler Exceldatei kann nicht aus dem System gestartet werden";


        // Eingang
        public const string code5_101 = "5_101";  // Eingang
        // Ausgang
        public const string code5_201 = "5_201";  // Ausgang
        // Bestand
        public const string code5_301 = "Bestand - Fehler Excel Export Telerik";

        //CTR 
        //...|ctrRGList
        public const string code5_401 = "5_401";

        //Print
        public const string code6_101 = "Print - Fehler DirectPrint";
        public const string code6_201 = "Print - Fehler Mail/PDF";

        private string _FileName;
        private string _Path;
        private string _BenutzerName;
        private DateTime _Datum;
        private bool _ErrorOrdnerExist;



        public string FileName
        {
            get
            {
                _FileName = Datum.ToString("yyyy_MM_dd_hhss_ms") + "_Error.txt";
                return _FileName;
            }
            set { _FileName = value; }
        }
        public string Path
        {
            get
            {
                //_Path = Application.StartupPath + "\\Error\\";
                _Path = StartupPath + "\\Error\\";
                return _Path;
            }
            set { _Path = value; }
        }
        public string StartupPath
        {
            get
            {
                return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
        }
        public string ErrorText { get; set; }
        public string BenutzerName
        {
            get
            {
                _BenutzerName = this._GL_User.Name;
                return _BenutzerName;
            }
            set { _BenutzerName = value; }
        }
        public string Aktion { get; set; }
        public DateTime Datum
        {
            get
            {
                _Datum = DateTime.Now;
                return _Datum;
            }
            set { _Datum = value; }
        }
        public bool ErrorOrdnerExist
        {
            get
            {
                _ErrorOrdnerExist = System.IO.Directory.Exists(Path);
                if (!_ErrorOrdnerExist)
                {
                    System.IO.Directory.CreateDirectory(Path);
                }
                _ErrorOrdnerExist = System.IO.Directory.Exists(Path);
                return _ErrorOrdnerExist;
            }
            set { _ErrorOrdnerExist = value; }
        }
        public string SQLString { get; set; }
        public string exceptText { get; set; }
        public string Code { get; set; }
        public string Details { get; set; }

        internal clsINI.clsINI INI = new clsINI.clsINI();

        /***************************************************************************
         *                          Methoden /  Procedure
         * ************************************************************************/
        ///<summary>clsError / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUser, clsSystem mySystem)
        {
            INI = GlobalINI.GetINI();
            this._GL_User = myGLUser;
            this.Sys = mySystem;
            //this.StartupPath = this.Sys.StartupPath;
            this.Details = string.Empty;
            this.Mail = new clsMail();
            this.Mail.InitClass(this._GL_User, this.Sys);
        }
        ///<summary>clsError / GetBestandsauflistung</summary>
        ///<remarks>Im Ordner Error wird eine Errordatei angelegt mit folgenden Merkmalen:
        ///         - Dateiname: Datum +"_Error.txt"
        ///         - Dateiinhalt:
        ///             a. Benutzername, bei dem der Fehler aufgetreten ist
        ///             b. Datum des Fehlers
        ///             c. Aktion - Wobei ist der Fehler entstanden
        ///             d. SQL - Anweisung zum Nachstellen des Fehlers</remarks>
        public void WriteError()
        {
            //Check Path
            if (ErrorOrdnerExist)
            {
                StreamWriter writer = File.CreateText(Path + FileName);
                //Errortext erstellen
                if (INI.DataSet == null)
                {
                    INI = GlobalINI.GetINI();
                }
                string strSystemInfo = INI.ReadString("SYSTEMINFO", "ID");

                ErrorText += "Error aufgetreten bei: " + BenutzerID.ToString() + "_" + BenutzerName +
                           Environment.NewLine + "Systeminfo: " + strSystemInfo +
                           Environment.NewLine + "Datum: " + Datum.ToString() +
                           Environment.NewLine + "Error-Code: " + Code +
                           Environment.NewLine + "Aktion: " + Aktion +
                           Environment.NewLine;
                if (this.Details != null)
                {
                    ErrorText += Environment.NewLine + "Details                                                    " +
                                 Environment.NewLine + "-----------------------------------------------------------" +
                                 Environment.NewLine + this.Details;
                }
                ErrorText += Environment.NewLine +
                           Environment.NewLine + "Exception:  " +
                           Environment.NewLine + exceptText +
                           Environment.NewLine + "SQL-Anweisung:  " +
                           Environment.NewLine + SQLString;

                //Testdatei schreiben
                try
                {
                    writer.WriteLine(ErrorText);
                }
                catch (Exception ex)
                { }
                finally
                {
                    writer.Close();
                }

                this.Mail = new clsMail();
                this.Mail.InitClass(this._GL_User, this.Sys);
                //Error direkt als Mail versenden
                if (this.Mail is clsMail)
                {
                    this.Mail.Subject = strSystemInfo + ">>> ERROR-MESSAGE";
                    this.Mail.Message = this.ErrorText;
                    this.Mail.SendError();
                }
            }
        }
        /// <summary>
        ///             E-Mail Informationenn zu einer Bewegung von Dateien im LVS 
        /// </summary>
        /// <param name="myGLUser"></param>
        /// <param name="mySystem"></param>
        /// <param name="myAsn"></param>
        /// <param name="myAttachmentFilePath"></param>
        public static void SendMovementInfoMail(Globals._GL_USER myGLUser, clsSystem mySystem, clsASN myAsn, string myAttachmentFilePath)
        {
            string MailText = string.Empty;

            // Prüfen, ob der Anhang existiert
            if (!File.Exists(myAttachmentFilePath))
            {
                //throw new FileNotFoundException("Die angegebene Datei wurde nicht gefunden.", myAttachmentFilePath);

                MailText += "Die angegebene Datei wurde nicht gefunden." + Environment.NewLine;
                MailText += "Pfad: " + myAttachmentFilePath + Environment.NewLine;
            }
            else
            {
                // Initialisiere die Mail-Klasse
                clsMail iMail = new clsMail();
                iMail.InitClass(myGLUser, mySystem);
                iMail.Subject = "Communicator - ACHTUNG - iDoc wurde in Check/Error Ordner verschoben!";

                MailText += string.Format("{0,-30}: {1}", "Prozess", myAsn.Prozess) + Environment.NewLine;
                MailText += string.Format("{0,-30}: {1}", "Datum", DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")) + Environment.NewLine;
                MailText += string.Format("{0,-30}: {1}", "Dateipfad", myAttachmentFilePath) + Environment.NewLine;

                MailText += Environment.NewLine;
                MailText += Environment.NewLine;
                MailText += "Job" + Environment.NewLine;
                MailText += "-----------------------------------------------------------" + Environment.NewLine;
                MailText += string.Format("{0,-30}: {1}", "Job.AdrVerweis", myAsn.Job.AdrVerweisID) + Environment.NewLine;
                MailText += string.Format("{0,-30}: {1}", "Job.ASNFileTyp", myAsn.Job.ASNFileTyp) + Environment.NewLine;
                MailText += string.Format("{0,-30}: {1}", "Job.ASNTypID", myAsn.Job.ASNTypID) + Environment.NewLine;
                MailText += string.Format("{0,-30}: {1}", "Job.Path", myAsn.Job.Path) + Environment.NewLine;
                MailText += string.Format("{0,-30}: {1}", "Job.FileName", myAsn.Job.FileName) + Environment.NewLine;
                MailText += Environment.NewLine;
                MailText += "Bitte finden Sie die angehängte Datei mit den Bewegungsinformationen.";


                iMail.Message = MailText;

                // Anhang hinzufügen
                iMail.ListAttachment.Add(myAttachmentFilePath);

                // E-Mail senden
                try
                {
                    iMail.SendError();
                }
                catch (Exception ex)
                {
                    //throw new InvalidOperationException("Fehler beim Senden der E-Mail.", ex);
                }
            }
        }
    }
}
