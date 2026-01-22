using Common.Enumerations;
using LVS;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Telerik.WinControls.Data;
//using Telerik.WinControls.RichTextBox.FormatProviders.Txt;
using Telerik.WinControls.UI;

namespace Sped4
{
    public partial class ctrMailCockpit : UserControl
    {
        public delegate void ThreadCtrInvokeEventHandler();

        internal const string const_Headline = "E-Mail Cockpit";
        internal const string const_MailText = "<Nachrichtentext>";
        internal const string const_MailBetreff = "<Betreff>";
        internal const string const_MailReceiver = "<Mailempfänger>";
        internal const string const_MailAttachinglist = "<Anhänge>";
        private clsADR ADR;
        private clsMail Mail;
        public Globals._GL_USER GL_User;
        public frmTmp frmTmp;
        public ctrMenu ctrMenu;
        internal string MailText = string.Empty;
        internal DataTable dtProzesses = new DataTable("Prozesse");
        internal DataTable dtContactSource;
        public string MailBetreff = string.Empty;
        public Common.Enumerations.enumDokumentenArt eDocumentArt;

        /**********************************************************************************
         *                          Procedure
         * *******************************************************************************/
        ///<summary>ctrMailCockpit / ctrMailCockpit</summary>
        ///<remarks>.</remarks>
        public ctrMailCockpit()
        {
            InitializeComponent();
            afColorLabel1.Text = const_Headline;
            eDocumentArt = enumDokumentenArt.DEFAULT;
        }
        ///<summary>ctrMailCockpit / ctrMailCockpit_Load</summary>
        ///<remarks>.</remarks>
        private void ctrMailCockpit_Load(object sender, EventArgs e)
        {
            //User Daten auf das CTR
            SetUserDatenToCtr();
            //Init clsMail
            Mail = new clsMail();
            Mail.InitClass(this.GL_User, this.ctrMenu._frmMain.system);
            ClearCtr();
            InitDGVContactSource(true);
            this.tbMailSender.Text = Mail.MailFrom;
            this.MailText = const_MailText;


            /***********************************************************************************************
             * Übernahme der Anhänge aus den entsprechenden CTRs, aus denen das MailCockpit gestartet wurde
             *    Hier können noch weitere CTR hinzugefügt werden
             * ********************************************************************************************/
            if (this.ctrMenu._ctrJournal != null)
            {
                if (this.ctrMenu._ctrJournal.GetType() == typeof(ctrJournal))
                {
                    Mail.ListAttachment.AddRange(this.ctrMenu._ctrJournal.ListAttachmentPath);
                }
            }
            if (this.ctrMenu._ctrBestand != null)
            {
                if (this.ctrMenu._ctrBestand.GetType() == typeof(ctrBestand))
                {
                    if (this.ctrMenu._ctrBestand.ListAttachmentPath.Count > 0)
                    {
                        Mail.ListAttachment.AddRange(this.ctrMenu._ctrBestand.ListAttachmentPath);
                    }
                }
            }
            if (this.ctrMenu._ctrFreeForCall != null)
            {
                if (this.ctrMenu._ctrFreeForCall.GetType() == typeof(ctrFreeForCall))
                {
                    if (this.ctrMenu._ctrFreeForCall.ListAttachmentPath.Count > 0)
                    {
                        Mail.ListAttachment.AddRange(this.ctrMenu._ctrFreeForCall.ListAttachmentPath);
                        this.ADR = new clsADR();
                        this.ADR.ID = this.ctrMenu._ctrFreeForCall.Lager.ADR.ID;
                        this.ADR.Fill();
                    }
                }
            }
            if (this.ctrMenu._ctrSPLAdd != null)
            {
                if (this.ctrMenu._ctrSPLAdd.GetType() == typeof(ctrSPLAdd))
                {
                    if (this.ctrMenu._ctrSPLAdd.ListAttachmentPath.Count > 0)
                    {
                        Mail.ListAttachment.AddRange(this.ctrMenu._ctrSPLAdd.ListAttachmentPath);
                    }
                }
            }
            if (this.ctrMenu._ctrFaktLager != null)
            {
                if (this.ctrMenu._ctrFaktLager.GetType() == typeof(ctrFaktLager))
                {
                    if (this.ctrMenu._ctrFaktLager.AttachmentList.Count > 0)
                    {
                        Mail.ListAttachment.AddRange(this.ctrMenu._ctrFaktLager.AttachmentList);
                    }
                }
            }
            if (this.ctrMenu._ctrRGList != null)
            {
                if (this.ctrMenu._ctrRGList.GetType() == typeof(ctrRGList))
                {
                    if (this.ctrMenu._ctrRGList.AttachmentList.Count > 0)
                    {
                        Mail.ListAttachment.AddRange(this.ctrMenu._ctrRGList.AttachmentList);
                    }
                }
            }
            InitLVAttachment();
            InitDGVMails();
            InitMailText();
        }
        ///<summary>ctrMailCockpit / InitMailText</summary>
        ///<remarks>.</remarks>
        private void InitMailText()
        {
            MailText = const_MailText;
            if (this.ctrMenu._ctrJournal != null)
            {
                if (this.ctrMenu._ctrJournal.GetType() == typeof(ctrJournal))
                {
                    MailText = "Sehr geehrte Damen und Herren," + Environment.NewLine +
                                Environment.NewLine +
                                "anhängend finden Sie die Excel-Tabelle: " + Environment.NewLine +
                                "Journal vom: " + this.ctrMenu._ctrJournal.FileDateForMail.ToShortDateString() + " " + this.ctrMenu._ctrJournal.FileDateForMail.ToShortTimeString() + Environment.NewLine +
                                this.ctrMenu._ctrJournal.strJournalTyp + Environment.NewLine +
                                this.ctrMenu._ctrJournal.strJournalZeitraum + Environment.NewLine +
                                Environment.NewLine +
                                "Mit freundlichen Grüßen" + Environment.NewLine +
                                Environment.NewLine +
                                this.ctrMenu._frmMain.system.Client.ADR.Name1;
                }
            }
            if (this.ctrMenu._ctrBestand != null)
            {
                if (this.ctrMenu._ctrBestand.GetType() == typeof(ctrBestand))
                {
                    MailText = "Sehr geehrte Damen und Herren," + Environment.NewLine +
                                Environment.NewLine +
                                "anhängend finden Sie die Excel-Tabelle: " + Environment.NewLine +
                                "Bestand vom: " + this.ctrMenu._ctrBestand.FileDateForMail.ToShortDateString() + " " + this.ctrMenu._ctrBestand.FileDateForMail.ToShortTimeString() + Environment.NewLine +
                                this.ctrMenu._ctrBestand.strBestandZeitraum + Environment.NewLine +
                                Environment.NewLine +
                                "Mit freundlichen Grüßen" + Environment.NewLine +
                                Environment.NewLine +
                                this.ctrMenu._frmMain.system.Client.ADR.Name1;
                }
            }
            if (this.ctrMenu._ctrSPLAdd != null)
            {
                if (this.ctrMenu._ctrSPLAdd.GetType() == typeof(ctrSPLAdd))
                {

                    MailText = "Sehr geehrte Damen und Herren," + Environment.NewLine + Environment.NewLine;

                    switch (this.eDocumentArt)
                    {
                        case enumDokumentenArt.SPLDoc:
                            tbBetreff.Text = "Sperrlagermeldung";
                            MailText += "in der Anlage erhalten Sie die beigf. Sperrlagermeldung zur Kenntnisnahme." + Environment.NewLine;
                            break;
                        case enumDokumentenArt.SchadenDoc:
                            tbBetreff.Text = "Schadensmeldung";
                            MailText += "in der Anlage erhalten Sie die beigf. Schadensmeldung zur Kenntnisnahme." + Environment.NewLine;
                            break;


                    }

                    //tbBetreff.Text = ctrSPLAdd.const_SPLAdd_DocName;
                    //tbBetreff.Text = this.MailBetreff;
                    //MailText = "Sehr geehrte Damen und Herren," + Environment.NewLine +
                    //            Environment.NewLine +
                    //"in der Anlage erhalten Sie die beigf. "+ this.MailBetreff +" zur Kenntnisnahme." + Environment.NewLine +
                    MailText += "Artikeldaten: " + Environment.NewLine +
                                "LVSNr: " + this.ctrMenu._ctrSPLAdd.Lager.Artikel.LVS_ID.ToString() + Environment.NewLine +
                                "ProduktionsNr.: " + this.ctrMenu._ctrSPLAdd.Lager.Artikel.Produktionsnummer.ToString() + Environment.NewLine +
                                "Material-/Werksnr.: " + this.ctrMenu._ctrSPLAdd.Lager.Artikel.Werksnummer.ToString() + Environment.NewLine +
                                Environment.NewLine +
                                "Mit freundlichen Grüßen" + Environment.NewLine +
                                Environment.NewLine +
                                this.ctrMenu._frmMain.system.Client.ADR.Name1;
                }
            }
            if (this.ctrMenu._ctrFaktLager != null)
            {
                if (this.ctrMenu._ctrFaktLager.GetType() == typeof(ctrFaktLager))
                {
                    tbBetreff.Text = "Rechnung incl. Anlage als PDF";
                    MailText = "Sehr geehrte Damen und Herren," + Environment.NewLine +
                                Environment.NewLine +
                                "in der Anlage erhalten Sie die beigf. Rechnung incl. Anhang  zur Kenntnisnahme." + Environment.NewLine +
                                "Rechnungsdaten: " + Environment.NewLine +
                                "Nr.: " + this.ctrMenu._ctrFaktLager.FaktLager.Rechnung.RGNr.ToString() + Environment.NewLine +
                                "Datum: " + this.ctrMenu._ctrFaktLager.FaktLager.Rechnung.Datum.ToShortDateString() + Environment.NewLine +
                                Environment.NewLine +
                                "Mit freundlichen Grüßen" + Environment.NewLine +
                                Environment.NewLine +
                                this.ctrMenu._frmMain.system.Client.ADR.Name1;
                }
            }
            if (this.ctrMenu._ctrRGList != null)
            {
                if (this.ctrMenu._ctrRGList.GetType() == typeof(ctrRGList))
                {
                    tbBetreff.Text = "Rechnung incl. Anlage als PDF";
                    MailText = "Sehr geehrte Damen und Herren," + Environment.NewLine +
                                Environment.NewLine +
                                "in der Anlage erhalten Sie die beigf. Rechnung incl. Anhang  zur Kenntnisnahme." + Environment.NewLine +
                                "Rechnungsdaten: " + Environment.NewLine +
                                "Nr.: " + this.ctrMenu._ctrRGList.FaktLager.Rechnung.RGNr.ToString() + Environment.NewLine +
                                "Datum: " + this.ctrMenu._ctrRGList.FaktLager.Rechnung.Datum.ToShortDateString() + Environment.NewLine +
                                Environment.NewLine +
                                "Mit freundlichen Grüßen" + Environment.NewLine +
                                Environment.NewLine +
                                this.ctrMenu._frmMain.system.Client.ADR.Name1;
                }
            }
            this.tbMailText.Text = MailText;
        }
        ///<summary>ctrMailCockpit / SetUserDatenToCtr</summary>
        ///<remarks>Setzt die Userdaten (MAIL/SMTP) ins CTR</remarks>
        private void SetUserDatenToCtr()
        {
            string str = string.Empty;

            if (!this.ctrMenu._frmMain.system.Client.Modul.Mail_UsingMainMailForMailing)
            {
                this.tbUserName.Text = this.GL_User.Name + " " + this.GL_User.Vorname;
                this.tbUserMail.Text = this.GL_User.Mail;
                this.tbSMTPUser.Text = this.GL_User.SMTPUser;
                this.tbSMTPPass.Text = this.GL_User.SMTPPass;
                this.tbSMTPServer.Text = this.GL_User.SMTPServer;
                this.tbSMTPPort.Text = this.GL_User.SMTPPort.ToString();
            }
            else
            {
                //this.tbUserName.Text = this.GL_User.Name + " " + this.GL_User.Vorname;
                //this.tbUserMail.Text = this.ctrMenu._frmMain.system.Client.Modul.Mail_MailAdress;
                //this.tbSMTPUser.Text = this.ctrMenu._frmMain.system.Client.Modul.Mail_SMTPUser;
                //this.tbSMTPPass.Text = this.ctrMenu._frmMain.system.Client.Modul.Mail_SMTPPasswort;
                //this.tbSMTPServer.Text = this.ctrMenu._frmMain.system.Client.Modul.Mail_SMTPServer;
                //this.tbSMTPPort.Text = this.ctrMenu._frmMain.system.Client.Modul.Mail_SMTPPort.ToString();

                panMailUserEditContainer.Enabled = false;
            }
            this.mmPanelUserEdit.SetExpandCollapse(Sped4.Controls.AFMinMaxPanel.EStatus.Expanded);
        }
        ///<summary>ctrMailCockpit / tsbtnMailCockpitClose_Click</summary>
        ///<remarks>.</remarks>
        private void tsbtnMailCockpitClose_Click(object sender, EventArgs e)
        {
            this.frmTmp.CloseFrmTmp();
        }
        ///<summary>ctrMailCockpit / InitDGVContactSource</summary>
        ///<remarks></remarks>
        private void InitDGVContactSource(bool bLoadMailingList)
        {
            dtContactSource = new DataTable("ContactSource");
            if (bLoadMailingList)
            {
                dtContactSource = clsMailingList.GetAllMailingList(this.GL_User);
                ////Ermittelt alle MAilverteiler    
                //this.dgvContactSource.DataSource = dtContactMailingList;
                ////Subtable Kontakte
                //GridViewTemplate tmpContact = new GridViewTemplate();
                //tmpContact.DataSource = dtContactAP;
                //this.dgvContactSource.MasterTemplate.Templates.Add(tmpContact);
                ////Verknüpfung zwischen Table und Subtable
                ////////Setzt die Verknüpfung zwischen den Templates            
                //GridViewRelation relContact = new GridViewRelation(this.dgvContactSource.MasterTemplate);
                //relContact.ChildTemplate = tmpContact;
                //relContact.RelationName = "Artikel";
                //relContact.ParentColumnNames.Add("ID");
                //relContact.ChildColumnNames.Add("AuftragPosTableID");
                //this.dgvContactSource.Relations.Add(relContact);
            }
            else
            {
                //ermittelt alle E-MailKontakte
                dtContactSource = clsKontakte.GetAllMailContacts(this.GL_User);
            }

            this.dgvContactSource.DataSource = dtContactSource;
            for (Int32 i = 0; i <= this.dgvContactSource.Columns.Count - 1; i++)
            {
                string strColName = this.dgvContactSource.Columns[i].HeaderText.ToString();

                switch (strColName)
                {
                    case "Select":
                        this.dgvContactSource.Columns.Move(this.dgvContactSource.Columns[i].Index, 0);
                        break;
                    case "Nachname":
                        this.dgvContactSource.Columns.Move(this.dgvContactSource.Columns[i].Index, 1);
                        break;
                    case "Vorname":
                        this.dgvContactSource.Columns.Move(this.dgvContactSource.Columns[i].Index, 2);
                        break;
                    case "Abteilung":
                        this.dgvContactSource.Columns.Move(this.dgvContactSource.Columns[i].Index, 3);
                        break;
                    case "Mail":
                        this.dgvContactSource.Columns.Move(this.dgvContactSource.Columns[i].Index, 4);
                        break;
                    case "Bezeichnung":
                        //this.dgvContactSource.Columns.Move(this.dgvContactSource.Columns[i].Index, 5);
                        //this.dgvContactSource.Columns[i].FieldName = "Verteiler";
                        //this.dgvContactSource.Columns[i].Name = "Verteiler";
                        break;

                    //Spalten ausblenden
                    default:
                        this.dgvContactSource.Columns[i].IsVisible = false;
                        break;
                }
            }
            this.dgvContactSource.BestFitColumns();
            //Grouping
            GroupDescriptor grFirma = new GroupDescriptor();
            grFirma.GroupNames.Add("Firma", System.ComponentModel.ListSortDirection.Ascending);
            if (this.dgvContactSource.Columns["Bezeichnung"] != null)
            {
                grFirma.GroupNames.Add("Bezeichnung", System.ComponentModel.ListSortDirection.Ascending);
            }
            this.dgvContactSource.GroupDescriptors.Clear();
            this.dgvContactSource.GroupDescriptors.Add(grFirma);

            this.dgvContactSource.MasterTemplate.EnableGrouping = true;
            this.dgvContactSource.MasterTemplate.AllowDragToGroup = false;
            this.dgvContactSource.MasterTemplate.AutoExpandGroups = true;
            this.dgvContactSource.ShowGroupPanel = false;
        }
        ///<summary>ctrMailCockpit / InitLVAttachment</summary>
        ///<remarks></remarks>
        private void InitLVAttachment()
        {
            this.lvAttachment.DataSource = null;
            this.lvAttachment.DataSource = Mail.ListAttachment;
        }
        ///<summary>ctrMailCockpit / tsbtnContactSource_Click</summary>
        ///<remarks></remarks>
        private void tsbtnContactSource_Click(object sender, EventArgs e)
        {
            InitDGVContactSource(true);
        }
        ///<summary>ctrMailCockpit / tsbtnContactSourceMailingList_Click</summary>
        ///<remarks></remarks>
        private void tsbtnContactSourceMailingList_Click(object sender, EventArgs e)
        {
            InitDGVContactSource(false);
        }
        ///<summary>ctrMailCockpit / tsbtnMailSourceAdd_Click</summary>
        ///<remarks>Die ausgewählten Kontakte sollen übernommen werden</remarks>
        private void tsbtnMailSourceAdd_Click(object sender, EventArgs e)
        {
            Mail.FillMailReceiverList(dtContactSource);
            tbMailReceiver.Text = Mail.MailReceivers;
        }
        ///<summary>ctrMailCockpit / SetProzessBarMailSend</summary>
        ///<remarks></remarks>
        private void SetProzessBarMailSend(bool bReset, Int32 iVal)
        {
            if (bReset)
            {
                this.tspBarMailSend.Value = 0;
                this.tslMailSendInfo.Text = string.Empty;
            }
            else
            {
                Int32 tmpValue = this.tspBarMailSend.Value + iVal;
                if (tmpValue < this.tspBarMailSend.Maximum)
                {
                    this.tspBarMailSend.Value = tmpValue;
                    this.tslMailSendInfo.Text = "E-Mail wird versendet - bitte warten....";
                }
                else
                {
                    this.tspBarMailSend.Value = this.tspBarMailSend.Maximum;
                    this.tslMailSendInfo.Text = "E-Mail wurde erfolgreich versendet....";
                }
            }
        }
        ///<summary>ctrMailCockpit / tsbtnSendMail_Click</summary>
        ///<remarks>Mail versenden</remarks>
        private void tsbtnSendMail_Click(object sender, EventArgs e)
        {
            if (
                (tbMailReceiver.Text != const_MailReceiver) &&
                (tbMailReceiver.Text != string.Empty)
               )
            {
                //Prozessbar init
                SetProzessBarMailSend(true, 0);
                bool bError = false;
                try
                {
                    SetProzessBarMailSend(false, 1);
                    string[] reciever = tbMailReceiver.Text.Split(',');
                    Mail.ListMailReceiver = new List<string>();
                    foreach (string strEmail in reciever)
                    {
                        string strTmpEmail = strEmail.Trim();
                        if (Functions.CheckForEmail(strTmpEmail))
                        {
                            Mail.ListMailReceiver.Add(strTmpEmail);
                        }
                    }
                    SetProzessBarMailSend(false, 2);
                    //BlindCopy Mail
                    if (cbMailCopy.Checked)
                    {
                        if (!this.GL_User.Mail.Equals(string.Empty))
                        {
                            Mail.MailBCC = this.GL_User.Mail;
                        }
                        clsClient.ctrMailCockpi_CustomizeAddMailBCC(this.ctrMenu._frmMain.system.Client.MatchCode, ref this.Mail);
                    }
                }
                catch (Exception ex)
                {
                    bError = true;
                    SetProzessBarMailSend(true, 0);
                    Mail.strErrorInfo = ex.ToString();
                    clsMessages.Allgemein_ERRORTextShow(Mail.strErrorInfo);
                }
                finally
                {
                    if (bError == false)
                    {
                        SetProzessBarMailSend(false, 1);
                        Thread.Sleep(500);
                        Mail.Subject = this.tbBetreff.Text;
                        Mail.Message = this.tbMailText.Text;
                        SetProzessBarMailSend(false, 1);
                        bool bSendOK = Mail.Send();
                        SetProzessBarMailSend(false, 1);

                        if (bSendOK)
                        {
                            ////Eingabefelder leeren
                            ClearCtr();
                            ////Attachment / Anhänge Liste leeren und init
                            this.lvAttachment.Items.Clear();
                            Mail.ListAttachment.Clear();
                            InitLVAttachment();
                            //Init Mailkontakte Source
                            InitDGVContactSource(true);
                            InitDGVMails();
                        }
                        else
                        {
                            SetProzessBarMailSend(true, 0);
                            clsMessages.Allgemein_ERRORTextShow(Mail.strErrorInfo);
                        }
                    }
                }
            }
            else
            {
                SetProzessBarMailSend(true, 0);
                clsMessages.Mail_MailReceiverMissing();
            }
        }
        ///<summary>ctrMailCockpit / ClearCtr</summary>
        ///<remarks>Mail versenden</remarks>
        private void ClearCtr()
        {
            this.tbBetreff.Text = string.Empty;
            this.tbBetreff.NullText = const_MailBetreff;
            this.tbMailReceiver.Text = string.Empty;
            this.tbMailReceiver.NullText = const_MailReceiver;
            this.tbMailText.Text = MailText;
            this.tbMailText.NullText = const_MailText;
            this.cbMailCopy.Checked = false;
            SetProzessBarMailSend(true, 0);
        }
        ///<summary>ctrMailCockpit / lvAttachment_ItemDataBound</summary>
        ///<remarks></remarks>
        private void lvAttachment_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //Baustelle
            string strItem = e.Item.Text;
            if ((strItem.EndsWith(".pdf")) || (strItem.EndsWith(".PDF")))
            {
                e.Item.Image = Sped4.Properties.Resources.box_16x16;
            }
            if ((strItem.EndsWith(".doc")) || (strItem.EndsWith(".DOC")))
            {
                e.Item.Image = Sped4.Properties.Resources.box_closed_edit_16x16;
            }
            if ((strItem.EndsWith(".xls")) || (strItem.EndsWith(".XLS")))
            {
                e.Item.Image = Sped4.Properties.Resources.box_into_16x16;
            }
            if ((strItem.EndsWith(".txt")) || (strItem.EndsWith(".TXT")))
            {
                e.Item.Image = Sped4.Properties.Resources.box_previous_16x16;
            }
        }
        ///<summary>ctrMailCockpit / listViewTsm_Click</summary>
        ///<remarks></remarks>
        private void listViewTsm_Click(object sender, EventArgs e)
        {
            this.lvAttachment.ViewType = ListViewType.ListView;
        }
        ///<summary>ctrMailCockpit / iconsViewTsm_Click</summary>
        ///<remarks></remarks>
        private void iconsViewTsm_Click(object sender, EventArgs e)
        {
            this.lvAttachment.ViewType = ListViewType.IconsView;
        }
        ///<summary>ctrMailCockpit / detailsViewTsm_Click</summary>
        ///<remarks></remarks>
        private void detailsViewTsm_Click(object sender, EventArgs e)
        {
            this.lvAttachment.ViewType = ListViewType.DetailsView;
        }
        ///<summary>ctrMailCockpit / tsbtnAttachmentAdd_Click</summary>
        ///<remarks></remarks>
        private void tsbtnAttachmentAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofDialog = new OpenFileDialog();
            ofDialog.InitialDirectory = ctrMenu._frmMain.GL_System.sys_WorkingPathExport;
            ofDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (ofDialog.ShowDialog(this) == DialogResult.OK)
            {
                string strFilePath = ofDialog.FileName;
                Mail.ListAttachment.Add(strFilePath);
                InitLVAttachment();
            }
        }
        ///<summary>ctrMailCockpit / dgvContactSource_CellClick</summary>
        ///<remarks></remarks>
        private void dgvContactSource_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column != null && e.Column.Name.Equals("Select"))
            {
                if ((bool)e.Value == true)
                {
                    this.dgvContactSource.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
                }
                else
                {
                    this.dgvContactSource.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
                }
            }
        }
        ///<summary>ctrMailCockpit / tsbtnClearMail_Click</summary>
        ///<remarks></remarks>
        private void tsbtnClearMail_Click(object sender, EventArgs e)
        {
            ClearCtr();
            InitDGVContactSource(true);
            InitMailText();
        }
        ///<summary>ctrMailCockpit / tsbtnAttachmentDelete_Click</summary>
        ///<remarks></remarks>
        private void tsbtnAttachmentDelete_Click(object sender, EventArgs e)
        {
            List<string> ListTmp = new List<string>();
            for (Int32 i = 0; i <= this.lvAttachment.Items.Count - 1; i++)
            {
                if (!this.lvAttachment.Items[i].Selected)
                {
                    ListTmp.Add(this.lvAttachment.Items[i].Value.ToString());
                }
            }
            this.Mail.ListAttachment.Clear();
            this.Mail.ListAttachment.AddRange(ListTmp);
            InitLVAttachment();
        }
        ///<summary>ctrMailCockpit / tsbtnUserdatenSave_Click</summary>
        ///<remarks>Userdaten speichern</remarks>
        private void tsbtnUserdatenSave_Click(object sender, EventArgs e)
        {
            clsUser User = new clsUser();
            User._GL_User = this.GL_User;
            User.ID = this.GL_User.User_ID;
            User.Fill();

            //nur die Mail / SMTP Daten
            User.Mail = this.tbUserMail.Text.Trim();
            User.SMTPUser = this.tbSMTPUser.Text.Trim();
            User.SMTPPasswort = this.tbSMTPPass.Text.Trim();
            User.SMTPServer = this.tbSMTPServer.Text.Trim();
            Int32 iTmp = clsUser.Default_SMTPPort;
            Int32.TryParse(this.tbSMTPPort.Text.Trim(), out iTmp);
            User.SMTPPort = iTmp;

            //Check - Alle User-Variablen müssen ausgefüllt sein,
            //sonst kein speichern
            if (
                (!User.Mail.Equals(string.Empty)) &
                (!User.SMTPUser.Equals(string.Empty)) &
                (!User.SMTPPasswort.Equals(string.Empty)) &
                (!User.SMTPServer.Equals(string.Empty)) &
                (!User.SMTPPort.Equals(string.Empty))
               )
            {
                User.Update();
                this.GL_User = User.Fill();
                this.ctrMenu.GL_User = this.GL_User;
            }
        }
        ///<summary>ctrMailCockpit / tsbtnCloseCtr_Click</summary>
        ///<remarks>Ctr schliessen</remarks>
        private void tsbtnCloseCtr_Click(object sender, EventArgs e)
        {
            this.frmTmp.CloseFrmTmp();
        }
        ///<summary>ctrMailCockpit / tsbtnClose_Click</summary>
        ///<remarks>Ctr schliessen</remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this.frmTmp.CloseFrmTmp();
        }
        /****************************************************************************************
         *                          DGV Mails
         * *************************************************************************************/
        ///<summary>ctrMailCockpit / InitDGVMails</summary>
        ///<remarks></remarks>
        private void InitDGVMails()
        {
            this.dgvMails.DataSource = Mail.emails.GetEMails();
            for (Int32 i = 0; i <= this.dgvMails.Columns.Count - 1; i++)
            {
                string strColName = this.dgvMails.Columns[i].Name;
                switch (strColName)
                {
                    case "Datum":
                        this.dgvMails.Columns[i].IsVisible = true;
                        this.dgvMails.Columns[i].IsPinned = true;
                        this.dgvMails.Columns[i].Width = 80;
                        break;
                    case "Betreff":
                        this.dgvMails.Columns[i].IsVisible = true;
                        this.dgvMails.Columns[i].IsPinned = true;
                        this.dgvMails.Columns[i].Width = 100;
                        break;
                    case "Absender":
                    case "Empfaenger":
                        this.dgvMails.Columns[i].IsVisible = true;
                        this.dgvMails.Columns[i].Width = 80;
                        break;
                    case "Text":
                        this.dgvMails.Columns[i].IsVisible = true;
                        this.dgvMails.Columns[i].Width = 250;
                        break;
                    case "Dateien":
                        this.dgvMails.Columns[i].IsVisible = true;
                        this.dgvMails.Columns[i].MinWidth = 80;
                        break;
                    default:
                        this.dgvMails.Columns[i].IsVisible = false;
                        break;
                }
            }
        }
        ///<summary>ctrMailCockpit / tsbtnAttachmentDelete_Click_1</summary>
        ///<remarks></remarks>
        private void tsbtnAttachmentDelete_Click_1(object sender, EventArgs e)
        {
            List<string> ListTmp = new List<string>();
            for (Int32 i = 0; i <= this.lvAttachment.Items.Count - 1; i++)
            {
                if (!this.lvAttachment.Items[i].Selected)
                {
                    ListTmp.Add(this.lvAttachment.Items[i].Value.ToString());
                }
            }
            this.Mail.ListAttachment.Clear();
            this.Mail.ListAttachment.AddRange(ListTmp);
            InitLVAttachment();
        }
        ///<summary>ctrMailCockpit / tsbtnRefreshMails_Click</summary>
        ///<remarks></remarks>
        private void tsbtnRefreshMails_Click(object sender, EventArgs e)
        {
            InitDGVMails();
        }
        ///<summary>ctrMailCockpit / toolStripButton1_Click</summary>
        ///<remarks></remarks>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ////Eingabefelder leeren
            ClearCtr();
            ////Attachment / Anhänge Liste leeren und init
            Mail.ListAttachment.Clear();
            InitLVAttachment();
            ////Init Mailkontakte Source
            InitDGVContactSource(true);
            InitDGVMails();
            InitMailText();
        }
        ///<summary>ctrMailCockpit / dgvMails_ToolTipTextNeeded</summary>
        ///<remarks></remarks>
        private void dgvMails_ToolTipTextNeeded(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
        {
            GridDataCellElement cell = sender as GridDataCellElement;
            if (cell != null)
            {
                e.ToolTipText = cell.Value.ToString();
            }
        }
        ///<summary>ctrMailCockpit / dgvMails_CellClick</summary>
        ///<remarks></remarks>
        private void dgvMails_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (this.dgvMails.Rows.Count > 0)
            {
                if (e.RowIndex > -1)
                {
                    decimal decTmp = 0;
                    Decimal.TryParse(this.dgvMails.Rows[e.RowIndex].Cells["ID"].Value.ToString(), out decTmp);
                    if (decTmp > 0)
                    {
                        this.Mail.emails.ID = decTmp;
                        this.Mail.emails.Fill();
                    }
                }
            }
        }
        ///<summary>ctrMailCockpit / dgvMails_CellDoubleClick</summary>
        ///<remarks></remarks>
        private void dgvMails_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (this.Mail.emails.ID > 0)
            {
                SetValueToCtr();
            }
        }
        ///<summary>ctrMailCockpit / SetValueToCtr</summary>
        ///<remarks></remarks>
        private void SetValueToCtr()
        {
            this.tbMailReceiver.Text = this.Mail.emails.Empfaenger;
            this.tbBetreff.Text = this.Mail.emails.Betreff;
            this.tbMailText.Text = this.Mail.emails.Text;
            //Attachment
            string[] attachment = this.Mail.emails.Dateien.Split(',');
            Mail.ListAttachment = new List<string>();
            foreach (string datei in attachment)
            {
                //this.ctrMenu._frmMain.system.Workin
                string strPath = this.ctrMenu._frmMain.system.WorkingPathExport + "\\" + datei;
                if (File.Exists(strPath))
                {
                    Mail.ListAttachment.Add(strPath);
                }
            }
            InitLVAttachment();
        }
        ///<summary>ctrMailCockpit / cbMailCopy_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbMailCopy_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMailCopy.Checked)
            {
                if (this.GL_User.Mail.Equals(string.Empty))
                {
                    cbMailCopy.Checked = false;

                    clsMessages.User_NoMailadress();
                }
            }
        }
    }
}
