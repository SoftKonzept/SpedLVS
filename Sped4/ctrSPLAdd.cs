using Common.Enumerations;
using LVS;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Telerik.WinControls.UI;


namespace Sped4
{
    public partial class ctrSPLAdd : UserControl
    {
        internal clsReportDocSetting RepDocSettings;

        public clsLager Lager;
        public ctrMenu ctrMenu;
        public ctrEinlagerung ctrEinlagerung;
        public Globals._GL_USER GLUser;
        public frmTmp _frmTmp;
        public frmPrintRepViewer _frmPrintRepViewer;
        internal string AttachmentPath;
        internal clsMail Mail;
        public const string const_SPLAdd_DocNameSPL = "Sperrlagermeldung";
        public const string const_SPLAdd_DocNameSchaden = "Schadensmeldung";

        internal string DokumentenArt;
        internal string DocPath;
        public List<string> ListAttachmentPath = new List<string>();
        ///<summary>ctrSPLAdd / ctrSPLAdd</summary>
        ///<remarks></remarks>
        public ctrSPLAdd(ctrEinlagerung myEinlagerung)
        {
            InitializeComponent();
            ctrEinlagerung = myEinlagerung;
            Lager = ctrEinlagerung.Lager;
        }
        ///<summary>ctrSPLAdd / ctrSPLAdd_Load</summary>
        ///<remarks></remarks>
        private void ctrSPLAdd_Load(object sender, EventArgs e)
        {
            ClearCtr();
            InitDGVSchaden();
            tbSperrdatum.Text = this.Lager.SPL.Datum.ToShortDateString();
            AttachmentPath = this.ctrMenu._frmMain.GL_System.sys_WorkingPathExport;

            //Check ob schon eine Meldung existiert, dann laden und anzeigen
            if (this.Lager.Artikel.bSPL)
            {
                SetSPLDatenToCtr();
            }
        }
        ///<summary>ctrSPLAdd / SetSPLDatenToCtr</summary>
        ///<remarks></remarks>
        private void SetSPLDatenToCtr()
        {
            tbSperrdatum.Text = this.Lager.Artikel.SPL.Datum.ToString();
            tbSperrgrund.Text = this.Lager.Artikel.SPL.Sperrgrund;
            tbVermerk.Text = this.Lager.Artikel.SPL.Vermerk;
            Int32 iTmp = 0;
            Int32.TryParse(this.Lager.Artikel.SPL.DefWindungen.ToString(), out iTmp);
            nudDefWindungen.Value = iTmp;
        }
        ///<summary>ctrSPLAdd / ClearCtr</summary>
        ///<remarks></remarks>
        private void ClearCtr()
        {
            tbSperrdatum.Text = DateTime.Now.Date.ToShortDateString();
            tbSperrgrund.Text = string.Empty;
            tbVermerk.Text = string.Empty;
            nudDefWindungen.Value = 0;
        }
        ///<summary>ctrSPLAdd / ctrSPLAdd_Load</summary>
        ///<remarks></remarks>
        private void InitDGVSchaden()
        {
            DataTable dtSchaden = new DataTable();
            Lager.Schaeden.ArtikelID = Lager.Artikel.ID;
            dtSchaden.Clear();
            dtSchaden = Lager.Schaeden.GetArtikelSchäden();
            if (!dtSchaden.Columns.Contains("Select"))
            {
                dtSchaden.Columns.Add("Select", typeof(Boolean));

            }
            this.dgvSchaden.DataSource = dtSchaden.DefaultView;
            if (this.dgvSchaden.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= this.dgvSchaden.Columns.Count - 1; i++)
                {
                    string strColName = this.dgvSchaden.Columns[i].Name;
                    switch (strColName)
                    {
                        case "Select":
                            this.dgvSchaden.Columns[i].IsPinned = true;
                            this.dgvSchaden.Columns[i].IsVisible = true;
                            break;
                        case "ID":
                            this.dgvSchaden.Columns[i].IsVisible = false;
                            break;
                        case "Datum":
                            this.dgvSchaden.Columns[i].FormatString = "{0:d}";
                            break;
                        case "Bezeichnung":
                            this.dgvSchaden.Columns[i].BestFit();
                            this.dgvSchaden.Columns[i].WrapText = true;
                            break;
                    }
                }
                this.dgvSchaden.BestFitColumns();
            }
            SetSperrgrund();
        }
        ///<summary>ctrSPLAdd / dgvSchaden_CellClick</summary>
        ///<remarks></remarks>
        private void dgvSchaden_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dgvSchaden.Rows.Count > 0)
                {
                    if (this.dgvSchaden.CurrentRow != null)
                    {
                        this.dgvSchaden.Rows[e.RowIndex].Cells["Select"].Value = (!(bool)this.dgvSchaden.Rows[e.RowIndex].Cells["Select"].Value);
                        SetSperrgrund();
                    }
                }
            }
        }
        ///<summary>ctrSPLAdd / SetSperrgrund</summary>
        ///<remarks></remarks>
        private void SetSperrgrund()
        {
            string strTmp = string.Empty;
            for (Int32 i = 0; i <= dgvSchaden.Rows.Count - 1; i++)
            {
                if ((bool)this.dgvSchaden.Rows[i].Cells["Select"].Value)
                {
                    strTmp = strTmp + this.dgvSchaden.Rows[i].Cells["Bezeichnung"].Value.ToString().Trim() + Environment.NewLine;
                }
            }
            tbSperrgrund.Text = strTmp;
        }
        ///<summary>ctrSPLAdd / tsbtnPrintSPL_Click</summary>
        ///<remarks></remarks>
        private void tsbtnPrintSPL_Click(object sender, EventArgs e)
        {
            if (this.ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
            {
                if (this.ctrMenu._frmMain.system.Client.Modul.Lager_SPL_PrintSPLDocument)
                {
                    //eintrag db
                    this.Lager.SPL.DefWindungen = (Int32)nudDefWindungen.Value;
                    this.Lager.SPL.Sperrgrund = tbSperrgrund.Text;
                    this.Lager.SPL.Vermerk = tbVermerk.Text;
                    this.Lager.SPL.Update();

                    ctrPrintLager TmpPrint = new ctrPrintLager();
                    TmpPrint.Hide();
                    TmpPrint._ctrMenu = this.ctrMenu;
                    TmpPrint._ctrEinlagerung = this.ctrEinlagerung;
                    TmpPrint._DokumentenArt = enumDokumentenArt.SPLLabel.ToString();
                    TmpPrint.SetLagerDatenToFrm();

                    TmpPrint.nudPrintCount.Value = this.ctrMenu._frmMain.system.GetCountPrintSPLLabel(ref this.ctrMenu._frmMain.GL_System, this.ctrMenu._frmMain.GL_System.sys_MandantenID);
                    TmpPrint.StartPrintSPLLable(true);
                    TmpPrint.Dispose();
                    //this._frmTmp.CloseFrmTmp();
                }
            }
            else
            {
                //direct Print
                if (this.ctrMenu._frmMain.system.Client.Modul.Lager_SPL_PrintSPLDocument)
                {
                    //eintrag db
                    this.Lager.SPL.DefWindungen = (Int32)nudDefWindungen.Value;
                    this.Lager.SPL.Sperrgrund = tbSperrgrund.Text;
                    this.Lager.SPL.Vermerk = tbVermerk.Text;
                    this.Lager.SPL.Update();
                    DokumentenArt = Common.Enumerations.enumDokumentenArt.SPLLabel.ToString();
                    DoPrint(Common.Enumerations.enumDokumentenArt.SPLLabel);
                    this._frmTmp.CloseFrmTmp();
                }
            }
        }
        ///<summary>ctrSPLAdd / DoPrint</summary>
        ///<remarks></remarks>
        private void DoPrint(enumDokumentenArt myDocArt)
        {
            ctrPrintLager TmpPrint = new ctrPrintLager();
            TmpPrint.Hide();
            TmpPrint._ctrMenu = this.ctrMenu;
            TmpPrint._ctrEinlagerung = this.ctrEinlagerung;
            DokumentenArt = myDocArt.ToString();

            if (this.ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
            {
                this.ctrMenu._frmMain.system.CustomizeDocPath(ref this.ctrMenu._frmMain.GL_System, this.ctrEinlagerung.Lager.Eingang.Auftraggeber);

                TmpPrint.nudPrintCount.Value = this.ctrMenu._frmMain.system.GetCountPrintSPLLabel(ref this.ctrMenu._frmMain.GL_System, this.ctrMenu._frmMain.GL_System.sys_MandantenID);

                TmpPrint._DokumentenArt = DokumentenArt;
                TmpPrint._DocPath = DocPath;
                TmpPrint.SetLagerDatenToFrm();
                TmpPrint.LagerPrint.Eingang.FillEingang();
                TmpPrint.LagerPrint.Artikel.GetArtikeldatenByTableID();
                if (TmpPrint.LagerPrint.Artikel.bSPL)
                {
                    TmpPrint._ctrMenu.OpenFrmReporView(TmpPrint, true, TmpPrint.bCountfromGUI);
                }
            }
            else
            {
                this.ctrMenu._frmMain.system.ReportDocSetting.InitClass(this.GLUser, this.ctrMenu._frmMain.GL_System, this.ctrMenu._frmMain.system, this.ctrEinlagerung.Lager.Eingang.Auftraggeber, this.ctrMenu._frmMain.system.AbBereich.ID);
                this.RepDocSettings = null; ;
                this.RepDocSettings = this.ctrMenu._frmMain.system.ReportDocSetting.ListReportDocEingang.Find(x => x.DocKey.Equals(DokumentenArt));
                if (this.RepDocSettings is clsReportDocSetting)
                {
                    DokumentenArt = this.RepDocSettings.DocKey;
                    DocPath = this.RepDocSettings.DocFileNameAndPath;
                    TmpPrint.RepDocSettings = this.RepDocSettings;


                    TmpPrint._DokumentenArt = DokumentenArt;
                    TmpPrint._DocPath = DocPath;
                    TmpPrint.SetLagerDatenToFrm();
                    TmpPrint.LagerPrint.Eingang.FillEingang();
                    TmpPrint.LagerPrint.Artikel.GetArtikeldatenByTableID();
                    if (TmpPrint.LagerPrint.Artikel.bSPL)
                    {
                        TmpPrint._ctrMenu.OpenFrmReporView(TmpPrint, true, TmpPrint.bCountfromGUI);
                    }
                }
            }
            TmpPrint.Dispose();
        }
        ///<summary>ctrSPLAdd / tsbtnSPLDocPrint_Click</summary>
        ///<remarks></remarks>
        private void tsbtnSPLDocPrint_Click(object sender, EventArgs e)
        {
            if (this.ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
            {
                ctrPrintLager TmpPrint = new ctrPrintLager();
                TmpPrint.Hide();
                TmpPrint._ctrMenu = this.ctrMenu;
                TmpPrint._ctrEinlagerung = this.ctrEinlagerung;
                if (this.ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
                {
                    TmpPrint._DokumentenArt = enumIniDocKey.SPLDoc.ToString();
                    TmpPrint.nudPrintCount.Value = this.ctrMenu._frmMain.GL_System.docPath_SPLDoc_Count;
                }
                else
                {
                    this.ctrMenu._frmMain.system.ReportDocSetting.InitClass(this.GLUser, this.ctrMenu._frmMain.GL_System, this.ctrMenu._frmMain.system, this.ctrEinlagerung.Lager.Eingang.Auftraggeber, this.ctrMenu._frmMain.system.AbBereich.ID);
                    TmpPrint.RepDocSettings = null; ;
                    TmpPrint.RepDocSettings = this.ctrMenu._frmMain.system.ReportDocSetting.ListReportDocEingang.Find(x => x.DocKey.Equals(enumIniDocKey.SPLDoc.ToString()));
                    TmpPrint.nudPrintCount.Value = TmpPrint.RepDocSettings.PrintCount;
                    TmpPrint._DokumentenArt = TmpPrint.RepDocSettings.DocKey;
                }
                TmpPrint.SetLagerDatenToFrm();
                TmpPrint.StartPrintSPLDoc(true);
                TmpPrint.Dispose();
                this._frmTmp.CloseFrmTmp();
            }
            else
            {
                if (this.ctrMenu._frmMain.system.Client.Modul.Lager_SPL_PrintSPLDocument)
                {
                    this.Lager.SPL.DefWindungen = (Int32)nudDefWindungen.Value;
                    this.Lager.SPL.Sperrgrund = tbSperrgrund.Text;
                    this.Lager.SPL.Vermerk = tbVermerk.Text;

                    if (this.ctrMenu._frmMain.system.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_SZG + "_"))
                    {
                        DokumentenArt = enumDokumentenArt.SchadenDoc.ToString();
                        DoPrint(enumDokumentenArt.SchadenDoc);
                    }
                    else
                    {
                        DokumentenArt = enumDokumentenArt.SPLDoc.ToString();
                        DoPrint(enumDokumentenArt.SPLDoc);
                    }
                    this._frmTmp.CloseFrmTmp();
                }
            }
        }
        ///<summary>ctrSPLAdd / tsbtnClose_Click</summary>
        ///<remarks></remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this._frmTmp.CloseFrmTmp();
        }
        ///<summary>ctrSPLAdd / tsbtnClearCtr_Click</summary>
        ///<remarks></remarks>
        private void tsbtnClearCtr_Click(object sender, EventArgs e)
        {
            this.ClearCtr();
            InitDGVSchaden();
        }
        ///<summary>ctrSPLAdd / tsbtnMail_Click</summary>
        ///<remarks></remarks>
        private void tsbtnMail_Click(object sender, EventArgs e)
        {
            string strPDFName = string.Empty;
            bool openExportFile = false;
            string strPDFPath = "";
            DateTime FileDateForMail = DateTime.Now;
            string FileName = FileDateForMail.ToString("yyyy_MM_dd_HHmmss") + "_" + ctrSPLAdd.const_SPLAdd_DocNameSPL + ".pdf";

            saveFileDialog.InitialDirectory = AttachmentPath;
            saveFileDialog.FileName = AttachmentPath + "\\" + FileName;
            saveFileDialog.ShowDialog();
            FileName = saveFileDialog.FileName;

            if (saveFileDialog.FileName.Equals(String.Empty))
            {
                return;
            }
            //Zusweisung der Dokumentenart und Path
            this.DokumentenArt = enumDokumentenArt.SPLDoc.ToString();
            this.DocPath = this.ctrMenu._frmMain.GL_System.docPath_SPLDoc;

            this._frmPrintRepViewer = new frmPrintRepViewer();
            this._frmPrintRepViewer.GL_System = this.ctrMenu._frmMain.GL_System;
            this._frmPrintRepViewer._SPLAdd = this;

            this._frmPrintRepViewer.iPrintCount = 1;
            this._frmPrintRepViewer.DokumentenArt = enumIniDocKey.SPLDoc.ToString();
            strPDFName = ctrSPLAdd.const_SPLAdd_DocNameSPL;

            if (this.ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
            {
                //this._frmPrintRepViewer.iPrintCount = 1;
                //this._frmPrintRepViewer.DokumentenArt = Globals.enumIniDocKey.SPLDoc.ToString();
                //strPDFName = ctrSPLAdd.const_SPLAdd_DocNameSPL;
            }
            else
            {
                this.ctrMenu._frmMain.system.ReportDocSetting.InitClass(this.GLUser, this.ctrMenu._frmMain.GL_System, this.ctrMenu._frmMain.system, this.ctrEinlagerung.Lager.Eingang.Auftraggeber, this.ctrMenu._frmMain.system.AbBereich.ID);
                this._frmPrintRepViewer._SPLAdd.RepDocSettings = null;
                this._frmPrintRepViewer._SPLAdd.RepDocSettings = this.ctrMenu._frmMain.system.ReportDocSetting.GetClassByDocKey(this._frmPrintRepViewer.DokumentenArt);
                this._frmPrintRepViewer._SPLAdd.DokumentenArt = this._frmPrintRepViewer._SPLAdd.RepDocSettings.DocKey;
                this._frmPrintRepViewer._SPLAdd.DocPath = this._frmPrintRepViewer._SPLAdd.RepDocSettings.DocFileNameAndPath;
            }
            this._frmPrintRepViewer.InitFrm();
            this._frmPrintRepViewer.PrintDirectToPDF(strPDFName, FileName);

            ListAttachmentPath = new List<string>();
            //Check File exist
            if (File.Exists(FileName))
            {
                ListAttachmentPath.Add(FileName);
            }
            if (ListAttachmentPath.Count > 0)
            {
                if (!FileName.Equals(string.Empty))
                {
                    //System.Threading.Thread.Sleep(100);
                    this.ctrMenu.OpenCtrMailCockpitInFrm(this);
                }
            }
        }


    }
}
