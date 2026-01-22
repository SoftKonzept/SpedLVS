using Common.Enumerations;
using LVS;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Sped4
{
    public partial class ctrPostCenter : UserControl
    {
        const string const_FileName = "";
        const string const_Headline = "Post Center";
        internal Int32 const_PostRange_Einlagerungsanzeige = 1;
        internal Int32 const_PostRange_Auslagerungsanzeige = 2;
        internal Int32 cont_PostRange_EinUndAuslagerungsanzeige = 3;
        public Globals._GL_USER GL_User;
        internal ctrMenu _ctrMenu;
        internal frmPrintRepViewer _frmPrintRepViewer;
        internal clsLager Lager = new clsLager();
        internal clsMail Mail = new clsMail();
        public frmTmp _frmTmp;
        //internal Thread threadPrint;
        //internal Thread threadMail;
        //internal Thread threadPostCreat;
        internal List<DateTime> ListDocDate;      //Liste mit Datum 
        internal Dictionary<string, List<DateTime>> DictDocDate;   //Dockumentenart mit jeweiliger Liste der Daten
        internal Dictionary<decimal, Dictionary<string, List<DateTime>>> DictAuftraggeberDocPrint; // Auftraggeber, Dokumenten, Liste der Daten
        internal Dictionary<decimal, Dictionary<string, List<DateTime>>> DictAuftraggeberDocMail; // Auftraggeber, Dokumenten, Liste der Daten
        //internal Dictionary<decimal, clsADR> DictAdressClasses;

        internal List<KeyValuePair<decimal, string>> listAuftraggeber;
        //internal Dictionary<decimal, clsADR> dictPrint;
        //internal Dictionary<decimal, clsADR> dictMail;
        //internal List<KeyValuePair<clsADR, string>> listMail;
        //internal List<KeyValuePair<clsADR, string>> listPrint;

        internal string DokumentenArt = string.Empty;
        internal decimal AdrPrintID = 0;
        internal DateTime LagerDate;
        internal string Vorgang = string.Empty;

        internal decimal Auftraggeber = 0;
        public string AttachmentPath = "C:\\LVS";

        public bool bNachdruck;
        ///<summary>ctrPostCenter / ctrPostCenter</summary>
        ///<remarks></remarks>
        public ctrPostCenter()
        {
            InitializeComponent();
            //Printdatetime einen Tag zurück
            dtpPrintDate.Value = DateTime.Now.AddDays(-1);
            //this.btnSendMails.Visible = frmMAIN.sGL_User.LoginName.ToLower() == "admin" ? true: false;
            //this.btnSendMailsA.Visible = frmMAIN.sGL_User.LoginName.ToLower() == "admin" ? true : false;
        }
        ///<summary>ctrPostCenter / InitPostCreate</summary>
        ///<remarks>Hier werden die Postdokumente erstellt. Über die Range wird die Dokumentenart
        ///         festgelegt:
        ///         1 = Einganganzeige
        ///         2 = Ausganganzeige
        ///         3 = Listen
        ///         4 = Rechnung</remarks>
        private void InitPostCreate(Int32 iRange)
        {
            SetInfoLog(DateTime.Now.ToString() + " - Postversand wird gestartet...");
            bool bItemExist = true;
            listAuftraggeber = new List<KeyValuePair<decimal, string>>();
            LagerDate = this.dtpPrintDate.Value;
            DataTable dtAuftraggeber = new DataTable();

            ListDocDate = new List<DateTime>();      //Liste mit Datum 
            DictDocDate = new Dictionary<string, List<DateTime>>();
            DictAuftraggeberDocPrint = new Dictionary<decimal, Dictionary<string, List<DateTime>>>();
            DictAuftraggeberDocMail = new Dictionary<decimal, Dictionary<string, List<DateTime>>>();
            bNachdruck = false;
            if (tbAuftraggeber.Text != string.Empty)
            {
                //string tmpDT = String.Format("{0:dd/MM/yyyy}", LagerDate);
                //string sqlTMP = "update Leingang set isPrintAnzeige=0 where Datediff(dd,date,'" + tmpDT + "')=0;";
                //sqlTMP += "update Lausgang set isPrintAnzeige=0 where Datediff(dd,datum,'" + tmpDT + "')=0";
                bNachdruck = true;
            }
            switch (iRange)
            {
                case 1:
                    bItemExist = false;
                    Vorgang = "[Einlagerungsanzeigen]";
                    //Alle Auftraggeber ermittlen, die bis zu dem gewählten Datum einen EIngang haben und noch 
                    //nicht gedruckt worden sind
                    SetInfoLog(DateTime.Now.ToString() + " - " + Vorgang + " - Empfangsadressen werden ermittelt...");
                    this.DokumentenArt = enumDokumentenArt.LagerEingangAnzeigePerDay.ToString();
                    dtAuftraggeber = clsLEingang.GetEingangAuftraggeberForEingangAnzeigeAtDate(this.GL_User.User_ID, LagerDate, Auftraggeber);

                    bItemExist = InitDictionarysToPrintAndMail(this.DokumentenArt, dtAuftraggeber);
                    break;
                case 2:
                    bItemExist = false;
                    Vorgang = "[Auslagerungsanzeigen]";
                    SetInfoLog(DateTime.Now.ToString() + " - " + Vorgang + " - Empfangsadressen werden ermittelt...");
                    this.DokumentenArt = enumDokumentenArt.LagerAusgangAnzeigePerDay.ToString();
                    dtAuftraggeber = clsLAusgang.GetAusgangAuftraggeberForAusgangAnzeigeAtDate(this.GL_User.User_ID, LagerDate, Auftraggeber);
                    bItemExist = InitDictionarysToPrintAndMail(this.DokumentenArt, dtAuftraggeber);
                    break;
                case 4:
                    break;

            }
            if (bItemExist)
            {
                CreatePost();
            }
        }
        ///<summary>ctrPostCenter / InitDictionarysToPrintAndMail</summary>
        ///<remarks></remarks>
        private bool InitDictionarysToPrintAndMail(string strDocArt, DataTable dtAuftraggeber)
        {
            bool bItemExist = false;
            decimal decAuftraggeberOld = 0;
            Int32 iLast = dtAuftraggeber.Rows.Count - 1;
            for (Int32 i = 0; i < dtAuftraggeber.Rows.Count; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(dtAuftraggeber.Rows[i]["Auftraggeber"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    //Auftraggeber
                    clsADR adrTmp = new clsADR();
                    adrTmp._GL_System = this._ctrMenu._frmMain.GL_System;
                    adrTmp._GL_User = this.GL_User;
                    adrTmp.ID = decTmp;
                    adrTmp.Fill();

                    //listAuftraggeber.Add(new KeyValuePair<decimal, string>(decTmp, dtAuftraggeber.Rows[i]["DokumentArt"].ToString()));
                    DateTime tmpDate = (DateTime)dtAuftraggeber.Rows[i]["Datum"];

                    if (i == iLast)
                    {
                        //Check neuer Auftraggeber
                        if (decAuftraggeberOld == decTmp)
                        {
                            ListDocDate.Add(tmpDate);
                            //-> Liste Datum zur Dict. hinzufügen
                            DictDocDate.Add(strDocArt, ListDocDate);
                            if (adrTmp.PostAnzeigeBy == 0)
                            {

                                //DictAuftraggeberDoc füllen
                                DictAuftraggeberDocPrint.Add(decAuftraggeberOld, DictDocDate);
                            }
                            else
                            {
                                //DictAuftraggeberDoc füllen
                                DictAuftraggeberDocMail.Add(decAuftraggeberOld, DictDocDate);
                            }
                            //neue Adresse -> lieste leeren
                            ListDocDate = new List<DateTime>();
                            //neue Adresse -> DictDocDate leern
                            DictDocDate = new Dictionary<string, List<DateTime>>();
                        }
                        else
                        {
                            //ListDocDate.Add(tmpDate);
                            //-> Liste Datum zur Dict. hinzufügen
                            if (decAuftraggeberOld > 0)
                            {
                                DictDocDate.Add(strDocArt, ListDocDate);
                                if (adrTmp.PostAnzeigeBy == 0)
                                {
                                    //DictAuftraggeberDoc füllen
                                    DictAuftraggeberDocPrint.Add(decAuftraggeberOld, DictDocDate);
                                }
                                else
                                {
                                    //DictAuftraggeberDoc füllen
                                    DictAuftraggeberDocMail.Add(decAuftraggeberOld, DictDocDate);
                                }
                                //neue Adresse -> lieste leeren
                                ListDocDate = new List<DateTime>();
                                //neue Adresse -> DictDocDate leern
                                DictDocDate = new Dictionary<string, List<DateTime>>();

                                //jetzt der letzte Datensatz
                                if (adrTmp.PostLfsBy == 0)
                                {
                                    ListDocDate.Add(tmpDate);
                                    //-> Liste Datum zur Dict. hinzufügen
                                    DictDocDate.Add(strDocArt, ListDocDate);
                                    //DictAuftraggeberDoc füllen
                                    DictAuftraggeberDocPrint.Add(decTmp, DictDocDate);
                                }
                                else
                                {
                                    ListDocDate.Add(tmpDate);
                                    //-> Liste Datum zur Dict. hinzufügen
                                    DictDocDate.Add(strDocArt, ListDocDate);
                                    //DictAuftraggeberDoc füllen
                                    DictAuftraggeberDocMail.Add(decTmp, DictDocDate);
                                }

                                //neue Adresse -> lieste leeren
                                ListDocDate = new List<DateTime>();
                                //neue Adresse -> DictDocDate leern
                                DictDocDate = new Dictionary<string, List<DateTime>>();
                            }
                            else
                            {
                                ListDocDate.Add(tmpDate);
                                //-> Liste Datum zur Dict. hinzufügen
                                DictDocDate.Add(strDocArt, ListDocDate);
                                //jetzt der letzte Datensatz
                                if (adrTmp.PostAnzeigeBy == 0)
                                {
                                    //DictAuftraggeberDoc füllen
                                    DictAuftraggeberDocPrint.Add(decTmp, DictDocDate);
                                }
                                else
                                {
                                    //DictAuftraggeberDoc füllen
                                    DictAuftraggeberDocMail.Add(decTmp, DictDocDate);
                                }

                                //neue Adresse -> lieste leeren
                                ListDocDate = new List<DateTime>();
                                //neue Adresse -> DictDocDate leern
                                DictDocDate = new Dictionary<string, List<DateTime>>();
                            }
                        }
                    }
                    else
                    {
                        //ersteer Durchlauf muss sofort hinzugefügt werden
                        if (i == 0)
                        {
                            ListDocDate.Add(tmpDate);
                            decAuftraggeberOld = decTmp;
                        }
                        else
                        {
                            //Check neuer Auftraggeber
                            if (decAuftraggeberOld == decTmp)
                            {
                                ListDocDate.Add(tmpDate);
                            }
                            else
                            {
                                //neue Adresse 
                                //-> Liste Datum zur Dict. hinzufügen
                                DictDocDate.Add(strDocArt, ListDocDate);
                                if (adrTmp.PostAnzeigeBy == 0)
                                {
                                    //DictAuftraggeberDoc füllen
                                    DictAuftraggeberDocPrint.Add(decAuftraggeberOld, DictDocDate);
                                }
                                else
                                {
                                    //DictAuftraggeberDoc füllen
                                    DictAuftraggeberDocMail.Add(decAuftraggeberOld, DictDocDate);
                                }
                                //neue Adresse -> lieste leeren
                                ListDocDate = new List<DateTime>();
                                //neue Adresse -> DictDocDate leern
                                DictDocDate = new Dictionary<string, List<DateTime>>();
                                //aktuelle Datum des neuen Adressdatensates muss hinzugefügt werden
                                ListDocDate.Add(tmpDate);
                            }
                            decAuftraggeberOld = decTmp;
                        }
                    }
                }
            }
            //Infoausgabe Druckliste                    
            if (DictAuftraggeberDocPrint.Count > 0)
            {
                SetInfoLog(DateTime.Now.ToString() + " - " + Vorgang + " - Druckliste mit " + DictAuftraggeberDocPrint.Count.ToString() + " Adressen wurden erstellt...");
                bItemExist = true;
            }
            else
            {
                SetInfoLog(DateTime.Now.ToString() + " - " + Vorgang + " - Druckliste -> Keine Datensätze vorhanden...");
            }
            //Infoausgabe Mailliste
            if (DictAuftraggeberDocMail.Count > 0)
            {
                SetInfoLog(DateTime.Now.ToString() + " - " + Vorgang + " - Mailliste mit " + DictAuftraggeberDocMail.Count.ToString() + " Adressen wurden erstellt...");
                bItemExist = true;
            }
            else
            {
                SetInfoLog(DateTime.Now.ToString() + " - " + Vorgang + " - Mailliste -> Keine Datensätze vorhanden...");
            }
            return bItemExist;
        }
        ///<summary>ctrPostCenter / CreatePost</summary>
        ///<remarks></remarks>
        private void CreatePost()
        {
            //hier würden nun die Threads gestartet
            CreatePost_Print();
            CreatePost_Mail();
        }
        ///<summary>ctrPostCenter / CreatePost_Print</summary>
        ///<remarks></remarks>
        private void CreatePost_Print()
        {
            SetInfoLog(DateTime.Now.ToString() + " - " + Vorgang + " - Dokumentendruck wird gestartet...");
            if (DokumentenArt != string.Empty)
            {
                //foreach (KeyValuePair<clsADR,string> item in listPrint)
                foreach (KeyValuePair<decimal, Dictionary<string, List<DateTime>>> item in DictAuftraggeberDocPrint)
                {
                    this.AdrPrintID = item.Key;
                    clsADR tmpAdr = new clsADR();
                    tmpAdr._GL_User = this.GL_User;
                    tmpAdr.ID = this.AdrPrintID;
                    tmpAdr.Fill();

                    Dictionary<string, List<DateTime>> DictTmpDocDate = new Dictionary<string, List<DateTime>>();
                    DictTmpDocDate = (Dictionary<string, List<DateTime>>)item.Value;
                    foreach (KeyValuePair<string, List<DateTime>> itemDocArt in DictTmpDocDate)
                    {
                        string strDocArt = itemDocArt.Key;
                        List<DateTime> ListTmpDatum = (List<DateTime>)itemDocArt.Value;
                        for (Int32 i = 0; i <= ListTmpDatum.Count - 1; i++)
                        {
                            this.LagerDate = (DateTime)ListTmpDatum[i];
                            try
                            {
                                this._frmPrintRepViewer = new frmPrintRepViewer();
                                this._frmPrintRepViewer._ctrPostCenter = this;
                                this._frmPrintRepViewer.iPrintCount = 1;


                                this._frmPrintRepViewer.DokumentenArt = strDocArt;
                                if (Auftraggeber > 0)
                                {
                                    this._frmPrintRepViewer.DokumentenArt = strDocArt + "Full";
                                }
                                this._frmPrintRepViewer.GL_System = this._ctrMenu._frmMain.GL_System;
                                this._frmPrintRepViewer.sys = this._ctrMenu._frmMain.system;
                                this._frmPrintRepViewer.InitFrm();
                                this._frmPrintRepViewer.InitReportView();

                                //Test
                                //this._frmPrintRepViewer.uRepSource.Uri = this._frmPrintRepViewer.uRepSource.Uri.Replace("F:", "x:");

                                string strPath = Path.GetDirectoryName(this._frmPrintRepViewer.uRepSource.Uri);
                                if (Directory.Exists(strPath))
                                {
                                    this._frmPrintRepViewer.PrintDirect();

                                    string strInfo = string.Empty;
                                    if (this._frmPrintRepViewer.ErrorText.Equals(string.Empty))
                                    {
                                        strInfo = DateTime.Now.ToString() + " - " + Vorgang + " - ID[" + tmpAdr.ID.ToString() + "]/KDNr.[" + tmpAdr.KD_ID.ToString() + "]/Datum[" + this.LagerDate.ToShortDateString() + "] - " + tmpAdr.Name1 + " gedruckt";
                                        SetInfoLog(strInfo);
                                        //Zusatzmail zum Ausdruck
                                        SendAdditionalWithPost(tmpAdr.ID, strDocArt, this.LagerDate);
                                    }
                                    else
                                    {
                                        strInfo = DateTime.Now.ToString() + " - " + Vorgang + " ERROR - ID[" + tmpAdr.ID.ToString() + "]/KDNr.[" + tmpAdr.KD_ID.ToString() + "]/Datum[" + this.LagerDate.ToShortDateString() + "] - " + this._frmPrintRepViewer.ErrorText;
                                        SetInfoLog(strInfo);
                                    }
                                }
                                else
                                {
                                    string strInfo = DateTime.Now.ToString() + " - " + Vorgang + " - ERROR - ID[" + tmpAdr.ID.ToString() + "]/KDNr.[" + tmpAdr.KD_ID.ToString() + "] - " + tmpAdr.Name1 + " ist ein Fehler aufgetreten";
                                    strInfo = strInfo + Environment.NewLine + "Fehlermeldung: Pfad ist nicht korrekt / erreichbar!";
                                    strInfo = strInfo + Environment.NewLine + "Pfad: " + this._frmPrintRepViewer.uRepSource.Uri;
                                    strInfo = strInfo + Environment.NewLine + "Funktion: CreatePost_Print";
                                    SetInfoLog(strInfo);
                                }
                                this._frmPrintRepViewer.Dispose();
                            }
                            catch (Exception ex)
                            {
                                string strInfo = DateTime.Now.ToString() + " - " + Vorgang + " - ERROR - ID[" + tmpAdr.ID.ToString() + "]/KDNr.[" + tmpAdr.KD_ID.ToString() + "] - " + tmpAdr.Name1 + " ist ein Fehler aufgetreten";
                                strInfo = strInfo + Environment.NewLine + "Fehlermeldung: " + ex.ToString();
                                SetInfoLog(strInfo);
                            }
                        }
                    }
                }
            }
        }
        ///<summary>ctrPostCenter / CreateMail</summary>
        ///<remarks></remarks>
        private void CreatePost_Mail()
        {
            SetInfoLog(DateTime.Now.ToString() + " - " + Vorgang + " - Mailversandt wird gestartet...");
            if (DokumentenArt != string.Empty)
            {
                //foreach (KeyValuePair<clsADR,string> item in listPrint)
                foreach (KeyValuePair<decimal, Dictionary<string, List<DateTime>>> item in DictAuftraggeberDocMail)
                {
                    this.AdrPrintID = item.Key;
                    clsADR tmpAdr = new clsADR();
                    tmpAdr._GL_User = this.GL_User;
                    tmpAdr._GL_System = this._ctrMenu._frmMain.GL_System;
                    tmpAdr.ID = this.AdrPrintID;
                    tmpAdr.Fill();

                    Dictionary<string, List<DateTime>> DictTmpDocDate = new Dictionary<string, List<DateTime>>();
                    DictTmpDocDate = (Dictionary<string, List<DateTime>>)item.Value;
                    foreach (KeyValuePair<string, List<DateTime>> itemDocArt in DictTmpDocDate)
                    {
                        string strDocArt = itemDocArt.Key;
                        List<DateTime> ListTmpDatum = (List<DateTime>)itemDocArt.Value;
                        for (Int32 i = 0; i <= ListTmpDatum.Count - 1; i++)
                        {
                            this.LagerDate = (DateTime)ListTmpDatum[i];
                            try
                            {
                                this._frmPrintRepViewer = new frmPrintRepViewer();
                                this._frmPrintRepViewer.GL_System = this._ctrMenu._frmMain.GL_System;
                                this.AdrPrintID = tmpAdr.ID;
                                this._frmPrintRepViewer._ctrPostCenter = this;
                                this._frmPrintRepViewer.iPrintCount = 1;
                                this._frmPrintRepViewer.DokumentenArt = strDocArt + "Mail";
                                this._frmPrintRepViewer.InitFrm();
                                this._frmPrintRepViewer.InitReportView();

                                string strPDFFilePath = @"C:\LVS\POSTCENTER";
                                if (!Directory.Exists(strPDFFilePath))
                                {
                                    Directory.CreateDirectory(strPDFFilePath);
                                }

                                strPDFFilePath = @"C:\LVS\POSTCENTER\" + this._frmPrintRepViewer.rViewer.Name + "_" + tmpAdr.ViewID + "_" + LagerDate.ToString("dd.MM.yyyy") + ".pdf";
                                string strPDFPath = Path.GetDirectoryName(strPDFFilePath);

                                if (Directory.Exists(strPDFPath))
                                {
                                    this._frmPrintRepViewer.PrintDirectToPDF(this._frmPrintRepViewer.rViewer.Name, strPDFFilePath);


                                    List<string> listAttach = new List<string>();
                                    listAttach.Add(strPDFPath);

                                    string strInfo = DateTime.Now.ToString() + " - " + Vorgang + " - PDF erstellt: " + strPDFPath;
                                    SetInfoLog(strInfo);

                                    if (listAttach.Count > 0)
                                    {
                                        Mail = new clsMail();
                                        Mail._GL_System = this._ctrMenu._frmMain.GL_System;
                                        Mail.InitClass(GL_User, _ctrMenu._frmMain.system);
                                        Mail.ListAttachment = listAttach;

                                        try
                                        {
                                            if (this._ctrMenu._frmMain.system.DebugModeLVSSPED)
                                            {
                                                Mail.ListMailReceiver = Mail.ListMailReceiverAdmin;
                                            }
                                            else
                                            {
                                                Mail.ListMailReceiver = tmpAdr.Kontakt.MailingList.MailingListAssignment.ListMailingListMailadressen;
                                            }
                                            //Mail.ListMailReceiver = tmpAdr.Kontakt.MailingList.MailingListAssignment.ListMailingListMailadressen;
                                            Mail.Subject = "Einlagerungsanzeige vom " + LagerDate.ToString("dd.MM.yyyy");

                                            //Für test ausgeschaltet
                                            if (Mail.SendNoReply())
                                            {
                                                clsLager LagerP;
                                                string strInfo1 = DateTime.Now.ToString() + " - " + Vorgang + " - ID[" + tmpAdr.ID.ToString() + "]/KDNr.[" + tmpAdr.KD_ID.ToString() + "] - " + tmpAdr.Name1 + " Mail versandt";
                                                SetInfoLog(strInfo1);
                                                if (this.DokumentenArt == "LagerAusgangAnzeigePerDay")
                                                {
                                                    LagerP = new clsLager();
                                                    LagerP.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system);
                                                    LagerP.Ausgang = new clsLAusgang();
                                                    LagerP.Ausgang.UpdatePrintLAusgang(DokumentenArt, this.AdrPrintID, this.LagerDate);
                                                }
                                                if (this.DokumentenArt == "LagerEingangAnzeigePerDay")
                                                {
                                                    LagerP = new clsLager();
                                                    LagerP.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system);
                                                    LagerP.Eingang = new clsLEingang();
                                                    LagerP.Eingang.UpdatePrintLEingang(DokumentenArt, this.AdrPrintID, this.LagerDate);
                                                }
                                            }
                                        }
                                        finally { }
                                    }
                                    else
                                    {
                                        strInfo = DateTime.Now.ToString() + " - " + Vorgang + " - ERROR - ID[" + tmpAdr.ID.ToString() + "]/KDNr.[" + tmpAdr.KD_ID.ToString() + "] - " + tmpAdr.Name1 + " ist ein Fehler aufgetreten";
                                        strInfo = strInfo + Environment.NewLine + "Fehlermeldung: Pfad existiert nicht!";
                                        strInfo = strInfo + Environment.NewLine + "Pfad: " + strPDFFilePath;
                                        strInfo = strInfo + Environment.NewLine + "Funktion: CreatePost_Mail";
                                        SetInfoLog(strInfo);
                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                string strInfo = DateTime.Now.ToString() + " - " + Vorgang + " - ERROR - ID[" + tmpAdr.ID.ToString() + "]/KDNr.[" + tmpAdr.KD_ID.ToString() + "] - " + tmpAdr.Name1 + " ist ein Fehler aufgetreten";
                                strInfo = strInfo + Environment.NewLine + "Fehlermeldung: " + ex.ToString();
                                SetInfoLog(strInfo);
                            }
                        }
                    }
                }
            }
        }
        ///<summary>ctrPostCenter / btnPrintEingangAnzeige_Click</summary>
        ///<remarks></remarks
        private void btnPrintEingangAnzeige_Click(object sender, EventArgs e)
        {
            tbLog.Text = string.Empty;
            InitPostCreate(this.const_PostRange_Einlagerungsanzeige);
            SetInfoLog(DateTime.Now.ToString() + " - Postversand beendet...");
            tbSearchA.Text = string.Empty;
        }
        ///<summary>ctrPostCenter / btnPrintAusgangAnzeige_Click</summary>
        ///<remarks></remarks
        private void btnPrintAusgangAnzeige_Click(object sender, EventArgs e)
        {
            tbLog.Text = string.Empty;
            InitPostCreate(this.const_PostRange_Auslagerungsanzeige);
            SetInfoLog(DateTime.Now.ToString() + " - Postversand beendet...");
            tbSearchA.Text = string.Empty;
        }
        ///<summary>ctrPostCenter / SetInfoLog</summary>
        ///<remarks></remarks
        private void SetInfoLog(string strInfoText)
        {
            tbLog.Text = tbLog.Text + Environment.NewLine + strInfoText;
        }
        ///<summary>ctrPostCenter / tsbtnClose_Click</summary>
        ///<remarks></remarks
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this._frmTmp.CloseFrmTmp();
        }
        ///<summary>ctrPostCenter / btnPrintAllAnzeigen_Click</summary>
        ///<remarks></remarks
        private void btnPrintAllAnzeigen_Click(object sender, EventArgs e)
        {
            tbLog.Text = string.Empty;
            InitPostCreate(this.const_PostRange_Einlagerungsanzeige);
            InitPostCreate(this.const_PostRange_Auslagerungsanzeige);
            SetInfoLog(DateTime.Now.ToString() + " - Postversand beendet...");
            tbSearchA.Text = string.Empty;
        }
        ///<summary>ctrPostCenter / tbSearchA_TextChanged</summary>
        ///<remarks></remarks
        private void tbSearchA_TextChanged(object sender, EventArgs e)
        {
            //Adressdaten laden
            DataTable dt = new DataTable();
            dt = clsADR.GetADRList(this.GL_User.User_ID);
            DataTable dtTmp = new DataTable();

            string SearchText = tbSearchA.Text.ToString();
            string Ausgabe = string.Empty;

            DataRow[] rows = dt.Select("Suchbegriff LIKE '" + SearchText + "'", "Suchbegriff");
            dtTmp = dt.Clone();
            foreach (DataRow row in rows)
            {
                Ausgabe = Ausgabe + row["Suchbegriff"].ToString() + "\n";
                dtTmp.ImportRow(row);
            }
            tbAuftraggeber.Text = Functions.GetADRStringFromTable(dtTmp);
            if (dtTmp.Rows.Count > 0)
            {
                Auftraggeber = Functions.GetADR_IDFromTable(dtTmp);
            }
            else
            {
                Auftraggeber = 0;
            }
        }
        ///<summary>ctrPostCenter / SendAdditionalWithPost</summary>
        ///<remarks></remarks
        private bool SendAdditionalWithPost(decimal myKundenID, string myDocArt, DateTime myDate, Int32 decRichtung = 0)
        {
            //List<decimal> ListAdrIDExcel = Classes.LVScom.clsMailingList.GetAutoMailingList(this.GL_User, const_autoGewBestandExcel);
            //if (ListAdrIDExcel.Count > 0)
            //{
            bool bSendOK = false;
            clsMailingList Mailinglist = new clsMailingList();

            //for (Int32 i = 0; i <= ListAdrIDExcel.Count - 1; i++)
            //{
            //   decimal AdrID = (decimal)ListAdrIDExcel[i];

            clsADR tmpADR = new clsADR();
            tmpADR._GL_User = this.GL_User;
            tmpADR.ID = myKundenID;
            tmpADR.Fill();
            string dir = string.Empty;

            switch (myDocArt)
            {
                case "LagerAusgangAnzeigePerDay":
                    dir = "Aus";
                    break;
                case "LagerEingangAnzeigePerDay":
                    dir = "Ein";
                    break;
                default:

                    break;
            }

            Mailinglist.InitClass(this.GL_User, this._frmTmp.ctrMenu._frmMain.GL_System, myKundenID);
            Mailinglist.FillListMailAdressenForAuto(myKundenID, "#JournalExcel#");
            Mailinglist.FillListMailAdressenForAuto(myKundenID, "#JournalExcel" + dir + "#", true);
            if (Mailinglist.ListMailadressen.Count > 0)
            {
                /*                  
                 // KEIN REPORT SONDERN EINE TABELLE ALS EXCEL FORMAT 
                uRepSource = new UriReportSource();
                uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("AuftraggeberID", AdrID));
                uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("Stichtag", DateTime.Now.AddDays(-1)));
                uRepSource.Uri = Application.StartupPath + this.GLSystem.Doc_DocBestandAutoMail;
                string FilePath = AttachmentPath + "\\" + tmpADR.ID.ToString() + "_" + FileBestandName;
                clsPrint.PrintDirectToPDF(FileBestandName, FilePath, uRepSource);
                //PrintDirectToPDF(FileBestandName, FilePath, uRepSource);
                */
                string strSql = string.Empty;
                string mailSubject = "Journal";

                if (myDocArt == "LagerAusgangAnzeigePerDay")
                {    // bedingung festlegen
                    strSql = "select " +
                                "'1' as A,'AUSGANG' as Ausgang, " +
                                "LVS_ID as LvsNr,a.Dicke,a.Breite,a.Laenge,a.Brutto,Cast(b.Datum as Date) as Datum, " +
                                "a.exInfo as Bemerkung,Produktionsnummer,b.exTransportRef as Anmerkung , b.KFZ, b.WaggonNo as WaggonNr,b.Info " +
                                "from Artikel a " +
                                "left join LAusgang b on a.LAusgangTableID=b.ID " +
                                "left join ADR c on b.Auftraggeber=c.ID " +
                                "where " +
                                     "DATEDIFF(DD,b.Datum,'" + myDate + "')=0 and " +
                                     "b.Checked=1 and b.Auftraggeber=" + myKundenID + " " +
                                     "order by LVS_ID";
                    //mailSubject = "Ausgangsanzeige vom " + myDate.ToString("dd.MM.yyyy");
                    mailSubject = "Ausgangsanzeige"; // vom " + myDate.ToString("dd.MM.yyyy");
                }
                if (myDocArt == "LagerEingangAnzeigePerDay")
                {
                    strSql = "select " +
                               "'1' as A,'EINGANG' as Eingang, " +
                               "LVS_ID as LvsNr,a.Dicke,a.Breite,a.Laenge,a.Brutto,Cast(b.Date as Date) as Datum, " +
                               "a.exInfo as Bemerkung,Produktionsnummer,' ' as Anmerkung " +
                               "from Artikel a " +
                               "left join Leingang b on a.LEingangTableID=b.ID " +
                               "left join ADR c on b.Auftraggeber=c.ID " +
                               "where " +
                               "DATEDIFF(DD,b.Date,'" + myDate + "')=0 and " +
                               "b.[Check]=1 and Auftraggeber=" + myKundenID + " " +
                               " order by LVS_ID";

                    //mailSubject = "Eingangsanzeige vom " + myDate.ToString("dd.MM.yyyy");
                    mailSubject = "Eingangsanzeige"; //" vom " + myDate.ToString("dd.MM.yyyy");
                }
                mailSubject = mailSubject + "_" + tmpADR.ViewID + "_" + myDate.ToString("yyyy_dd_MM");

                DataTable dtGewBestand = new DataTable("GewBestand");
                dtGewBestand = clsSQLcon.ExecuteSQL_GetDataTable(strSql, GL_User.User_ID, "Journal");
                LVS.clsExcel Excel = new clsExcel();

                string strPath = @"C:\LVS\POSTCENTER";
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }

                string FileName = mailSubject;
                string strPathFileName = strPath + "\\" + mailSubject;
                //string FilePath = Excel.ExportDataTableToWorksheet(dtGewBestand, FileName);
                string FilePath = Excel.ExportDataTableToWorksheet(dtGewBestand, strPathFileName);
                // string FilePath = Excel.ExportDataTableToExcel(dtGewBestand, FileName, AttachmentPath);

                //test Fehlernachstellung
                //FilePath = FilePath.Replace("C:", "X:");

                if (File.Exists(FilePath))
                {

                    //string FilePath = AttachmentPath + "\\" + FileName;
                    List<string> listAttach = new List<string>();
                    listAttach.Add(FilePath);
                    if (listAttach.Count > 0)
                    {
                        Mail = new clsMail();
                        Mail._GL_System = this._ctrMenu._frmMain.GL_System;
                        //Mail.InitClass(GL_User, _ctrMenu);
                        Mail.InitClass(GL_User, _ctrMenu._frmMain.system);
                        Mail.ListAttachment = listAttach;

                        try
                        {
                            if (this._ctrMenu._frmMain.system.DebugModeLVSSPED)
                            {
                                Mail.ListMailReceiver = Mail.ListMailReceiverAdmin;
                            }
                            else
                            {
                                Mail.ListMailReceiver = Mailinglist.ListMailadressen;
                            }
                            Mail.Subject = mailSubject;
                            bSendOK = Mail.SendNoReply();
                        }
                        finally
                        {
                            if (bSendOK)
                            {
                                //clsLogbuch tmpLog = new clsLogbuch();
                                //tmpLog.ID = 0;
                                //tmpLog.Typ = Globals.enumLogArtItem.autoMail.ToString();
                                //tmpLog.LogText = " -> Adresse ID/Matchcode: [" + tmpADR.ID.ToString() + "/" + tmpADR.ViewID + "] -> Mail erfolgreich versendet...";
                                //ListLogInsert.Add(tmpLog);
                            }
                            else
                            {
                                //clsLogbuch tmpLog = new clsLogbuch();
                                //tmpLog.ID = 0;
                                //tmpLog.Typ = Globals.enumLogArtItem.autoMail.ToString();
                                //tmpLog.LogText = " -> Adresse ID/Matchcode: [" + tmpADR.ID.ToString() + "/" + tmpADR.ViewID + "] -> nicht Mail erfolgreich versendet...";
                                //ListLogInsert.Add(tmpLog);
                            }
                        }
                    }
                    else
                    {
                        ////kein Attachment - keine Anhang
                        //clsLogbuch tmpLog = new clsLogbuch();
                        //tmpLog.ID = 0;
                        //tmpLog.Typ = Globals.enumLogArtItem.autoMail.ToString();
                        //tmpLog.LogText = " -> Adresse ID/Matchcode: [" + tmpADR.ID.ToString() + "/" + tmpADR.ViewID + "] -> es konnte kein Anhang gefunden werden...";
                        //ListLogInsert.Add(tmpLog);
                    }
                    Thread.Sleep(1000);
                }
                else
                {
                    string strInfo = DateTime.Now.ToString() + " - " + Vorgang + " - ERROR - ID[" + tmpADR.ID.ToString() + "]/KDNr.[" + tmpADR.KD_ID.ToString() + "] - " + tmpADR.Name1 + " ist ein Fehler aufgetreten";
                    strInfo = strInfo + Environment.NewLine + "Fehlermeldung: Datei existiert nicht!";
                    strInfo = strInfo + Environment.NewLine + "Datei/Pfad: " + FilePath;
                    strInfo = strInfo + Environment.NewLine + "Funktion: SendAdditionalWithPost";
                    SetInfoLog(strInfo);
                }
            }
            return bSendOK;
            //}
            //}
        }

        private void btnSendMails_Click(object sender, EventArgs e)
        {
            if (this.Auftraggeber > 0)
            {
                string docArt = enumDokumentenArt.LagerEingangAnzeigePerDay.ToString();
                SendAdditionalWithPost(Auftraggeber, docArt, dtpPrintDate.Value);

            }
        }

        private void btnSendMailsA_Click(object sender, EventArgs e)
        {
            if (this.Auftraggeber > 0)
            {
                string docArt = enumDokumentenArt.LagerAusgangAnzeigePerDay.ToString();
                SendAdditionalWithPost(Auftraggeber, docArt, this.dtpPrintDate.Value);

            }
        }

    }
}
