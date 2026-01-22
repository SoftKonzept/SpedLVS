using LVS;
using LVS.Constants;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Sped4
{
    public partial class ctrLieferEinteilung : UserControl
    {
        public Globals._GL_USER GL_User;
        const string const_FileName = "_Liefereinteilungen";
        const string const_Headline = "Liefereintelungen [VDA4905]";
        internal List<string> ListAttachmentPath;
        internal string AttachmentPath;
        internal string FileName;
        internal clsASN ASN;
        internal clsADRVerweis AdrVerweis;
        internal clsADR AdrVDA4905Receiver;
        internal ctrMenu _ctrMenu;

        List<decimal> ListAdrToSelect = new List<decimal>();


        ///<summary>ctrLieferEinteilung / ctrLieferEinteilung</summary>
        ///<remarks></remarks>
        public ctrLieferEinteilung()
        {
            InitializeComponent();
            this.afColorLabel1.myText = ctrLieferEinteilung.const_Headline;
            this.dtpVon.Value = DateTime.Now.Date.AddDays(-2);

            //this.scLET.Panel1Collapsed = true;
        }
        ///<summary>ctrLieferEinteilung / ctrLieferEinteilung_Load</summary>
        ///<remarks></remarks>
        private void ctrLieferEinteilung_Load(object sender, EventArgs e)
        {
            AttachmentPath = this._ctrMenu._frmMain.system.WorkingPathExport;
            //Init Class
            ASN = new clsASN();
            ASN.InitClass(this._ctrMenu._frmMain.GL_System, this.GL_User);
            ASN.Sys = this._ctrMenu._frmMain.system;

            AdrVerweis = new clsADRVerweis();
            AdrVerweis.InitClass(this.GL_User);

            AdrVDA4905Receiver = new clsADR();
            AdrVDA4905Receiver.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, 0, true);

            InitDGV_Select();
        }
        ///<summary>ctrLieferEinteilung / InitDGV_Select</summary>
        ///<remarks>Grid Auswahl Lieferant / Material</remarks>
        private void InitDGV_Select()
        {
            //Alle Verweise ermitteln, die für VDA4905 hinterlegt sind
            Dictionary<string, clsADRVerweis> DictAdrVerweis = clsADRVerweis.FillDictAdrVerweis(this._ctrMenu._frmMain.system.AbBereich.MandantenID, this._ctrMenu._frmMain.system.AbBereich.ID, this.GL_User.User_ID, constValue_AsnArt.const_Art_VDA4905);
            //Liste der Adressen ermitteln, die hier hinterlegt sind
            ListAdrToSelect.Clear();
            foreach (var item in DictAdrVerweis.Values)
            {
                clsADRVerweis tmpV = (clsADRVerweis)item;
                ListAdrToSelect.Add(tmpV.VerweisAdrID);
            }
            DataTable dtVDA4905Receiver = clsADR.GetADRListByIDList(ListAdrToSelect, this.GL_User.User_ID);
            dgvSelect.DataSource = dtVDA4905Receiver;
            for (Int32 i = 0; i <= this.dgvSelect.Columns.Count - 1; i++)
            {
                string colName = this.dgvSelect.Columns[i].Name.ToString();
                switch (colName)
                {
                    case "ViewID":
                        this.dgvSelect.Columns[i].HeaderText = "Matchcode";
                        this.dgvSelect.Columns[i].IsVisible = true;
                        break;
                    case "Name":
                        this.dgvSelect.Columns[i].IsVisible = true;
                        break;
                    default:
                        this.dgvSelect.Columns[i].IsVisible = false;
                        break;
                }
                this.dgvSelect.Columns[i].AutoSizeMode = Telerik.WinControls.UI.BestFitColumnMode.DisplayedCells;
            }
            this.dgvSelect.BestFitColumns();
        }
        ///<summary>ctrLieferEinteilung / InitDGV_LET</summary>
        ///<remarks>Grid Liefereinteilungen LET</remarks>
        private void InitDGV_LET()
        {
            ASN.GetVDA4905(dtpVon.Value, AdrVDA4905Receiver.ID);
            this.dgvLET.DataSource = ASN.VDA4905.dtHead;
            for (Int32 i = 0; i <= dgvLET.Columns.Count - 1; i++)
            {
                string strColName = dgvLET.Columns[i].HeaderText;
                switch (strColName)
                {
                    case "ID":
                        dgvLET.Columns[i].TextAlignment = System.Drawing.ContentAlignment.TopCenter;
                        break;
                    case "Datum":
                        dgvLET.Columns[i].FormatString = "{0:d}";
                        dgvLET.Columns[i].TextAlignment = System.Drawing.ContentAlignment.TopCenter;
                        break;
                    case "TransDatum":
                        dgvLET.Columns[i].HeaderText = "Ü-Datum";
                        dgvLET.Columns[i].FormatString = "{0:d}";
                        dgvLET.Columns[i].TextAlignment = System.Drawing.ContentAlignment.TopCenter;
                        break;
                    case "TransID":
                        dgvLET.Columns[i].HeaderText = "Ü-Nr";
                        dgvLET.Columns[i].TextAlignment = System.Drawing.ContentAlignment.TopLeft;
                        break;

                    case "Werksnummer":
                        dgvLET.Columns[i].TextAlignment = System.Drawing.ContentAlignment.TopRight;
                        //dgvLET.Columns[i].PinPosition = PinnedColumnPosition.Left;
                        dgvLET.Columns.Move(i, 0);
                        break;
                    case "Einheit":
                        dgvLET.Columns[i].TextAlignment = System.Drawing.ContentAlignment.TopRight;
                        //dgvLET.Columns[i].PinPosition = PinnedColumnPosition.Left;
                        dgvLET.Columns.Move(i, 1);
                        break;
                }
            }
            dgvLET.BestFitColumns();

            //Artikel Template
            GridViewTemplate tmpArt = new GridViewTemplate();
            tmpArt.DataSource = ASN.VDA4905.dtQuantity;
            for (Int32 i = 0; i <= tmpArt.Columns.Count - 1; i++)
            {
                string strColName = tmpArt.Columns[i].HeaderText;
                switch (strColName)
                {
                    case "ASN":
                        tmpArt.Columns[i].TextAlignment = System.Drawing.ContentAlignment.TopCenter;
                        break;

                    case "Zeitraum":
                        tmpArt.Columns[i].TextAlignment = System.Drawing.ContentAlignment.TopLeft;
                        break;

                    case "Menge":
                        tmpArt.Columns[i].TextAlignment = System.Drawing.ContentAlignment.TopRight;
                        tmpArt.Columns[i].FormatString = "{0:N2}";
                        break;
                }
            }
            tmpArt.BestFitColumns();

            dgvLET.MasterTemplate.Templates.Add(tmpArt);

            GridViewRelation Einteilungen = new GridViewRelation(dgvLET.MasterTemplate);
            Einteilungen.ChildTemplate = tmpArt;
            Einteilungen.RelationName = "Liefereinteilungen";
            Einteilungen.ParentColumnNames.Add("ID");
            Einteilungen.ChildColumnNames.Add("ASN");
            dgvLET.Relations.Add(Einteilungen);

        }
        ///<summary>ctrLieferEinteilung / dgvSelect_CellClick</summary>
        ///<remarks></remarks>
        private void dgvSelect_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                decimal decTmp = 0;
                Decimal.TryParse(this.dgvSelect.Rows[e.RowIndex].Cells["ID"].Value.ToString(), out decTmp);
                if (decTmp > 0)
                {
                    AdrVDA4905Receiver.ID = decTmp;
                    AdrVDA4905Receiver.FillClassOnly();
                }
            }
        }
        ///<summary>ctrLieferEinteilung / dgvSelect_CellDoubleClick</summary>
        ///<remarks></remarks>
        private void dgvSelect_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                InitDGV_LET();
            }
        }
        ///<summary>ctrLieferEinteilung / tsbtnClose_Click</summary>
        ///<remarks></remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this._ctrMenu.CloseCtrLieferEinteilung();
        }
        ///<summary>ctrLieferEinteilung / tsbtnClose_Click</summary>
        ///<remarks></remarks>
        private void tsbtnGridSelectShow_Click(object sender, EventArgs e)
        {
            this.scLET.Panel1Collapsed = (!this.scLET.Panel1Collapsed);
        }
        ///<summary>ctrLieferEinteilung / tsbtnRefresh_Click</summary>
        ///<remarks></remarks>
        private void tsbtnRefresh_Click(object sender, EventArgs e)
        {
            InitDGV_Select();
        }
        ///<summary>ctrLieferEinteilung / tsbtnPrint_Click</summary>
        ///<remarks></remarks>
        private void tsbtnPrint_Click(object sender, EventArgs e)
        {

        }
        ///<summary>ctrLieferEinteilung / tsbtnExcel_Click</summary>
        ///<remarks></remarks>
        private void tsbtnExcel_Click(object sender, EventArgs e)
        {
            bool openExportFile = false;
            FileName = Functions.GetDateTImeStringForFileName() + "_" + ctrLieferEinteilung.const_FileName + ".xls";
            saveFileDialog.InitialDirectory = AttachmentPath;
            saveFileDialog.FileName = AttachmentPath + "\\" + FileName;
            saveFileDialog.ShowDialog();
            FileName = saveFileDialog.FileName;

            if (saveFileDialog.FileName.Equals(String.Empty))
            {
                return;
            }
            Functions.Telerik_RunExportToExcelML(ref this._ctrMenu._frmMain, ref this.dgvLET, FileName, ref openExportFile, this.GL_User, true);

            if (openExportFile)
            {
                try
                {
                    System.Diagnostics.Process.Start(FileName);
                }
                catch (Exception ex)
                {
                    clsError error = new clsError();
                    error._GL_User = this.GL_User;
                    error.Code = clsError.code1_501;
                    error.Aktion = "Liefereinteilungen - Excelexport öffnen";
                    error.exceptText = ex.ToString();
                    error.WriteError();
                }
            }
        }
        ///<summary>ctrLieferEinteilung / tsbtnMail_Click</summary>
        ///<remarks></remarks>
        private void tsbtnMail_Click(object sender, EventArgs e)
        {
            bool openExportFile = false;
            this._ctrMenu._frmPrintRepView = new frmPrintRepViewer();
            this._ctrMenu._frmPrintRepView.GL_System = this._ctrMenu._frmMain.GL_System;
            this._ctrMenu._frmPrintRepView._ctrLieferEinteilungen = this;
            this._ctrMenu._frmPrintRepView.iPrintCount = 1;
            //this._ctrMenu._frmPrintRepView.DokumentenArt = "Bestandsliste";
            this._ctrMenu._frmPrintRepView.InitFrm();
            this._ctrMenu._frmPrintRepView.InitReportView();
            string strPDFPath = this.AttachmentPath + "\\" + Functions.GetDateTImeStringForFileName() + "_" + this._ctrMenu._frmPrintRepView.rViewer.Name + ".pdf";
            this._ctrMenu._frmPrintRepView.PrintDirectToPDF(this._ctrMenu._frmPrintRepView.rViewer.Name, strPDFPath);

            // Functions.Telerik_RunExportToExcelML(ref this._ctrMenu._frmMain, ref this.dgv, FileName, ref openExportFile, this.GL_User, false); // CF PDF statt EXCEL
            //if (!FileName.Equals(string.Empty))
            if (!strPDFPath.Equals(string.Empty))
            {
                System.Threading.Thread.Sleep(500);
                ListAttachmentPath = new List<string>();
                ListAttachmentPath.Add(strPDFPath);//FileName);
                this._ctrMenu.OpenCtrMailCockpitInFrm(this);
            }
        }


    }
}
