using LVS;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sped4
{
    public partial class frmFDocs : Sped4.frmTEMPLATE
    {
        public Globals._GL_USER GL_User;
        public DataTable dtLfsDaten = new DataTable("LfsDaten");
        public DataTable dtArtikelDetails = new DataTable("ArtikelDatails");
        public DataTable dtAuftragsdaten = new DataTable("Auftragsdaten");
        public DataTable dtPrintdaten = new DataTable("Printdaten");
        //public DataSet dsHRLfs = new DataSet();
        public Int32 PrintAnzahl = 1;
        internal decimal decAuftragID = 0;
        internal decimal decAuftragPos = 0;
        internal decimal decAP_ID = 0;
        internal decimal _ArtikelTableID = 0;
        internal string DocName = string.Empty;
        internal string DocArt = string.Empty;
        public bool boDirectPrint = false;

        public frmFDocs()
        {
            InitializeComponent();
        }
        //
        private void frmFDocs_Load(object sender, EventArgs e)
        {
            SetFrmText();
            SetInternalVarValue();
            SetValueToFrm();
            InitdtLfsDatenDBColumns();
        }
        //
        private void SetFrmText()
        {
            this.Text = "Erfassung Lieferscheindaten: Holzrichter";
        }
        //
        private void SetInternalVarValue()
        {

            if (dtPrintdaten.Rows.Count > 0)
            {
                decAuftragPos = (decimal)dtPrintdaten.Rows[0]["AuftragPos"];
                decAuftragID = (decimal)dtPrintdaten.Rows[0]["AuftragID"];
                decAP_ID = (decimal)dtPrintdaten.Rows[0]["AuftragPosTableID"];

                DocName = dtPrintdaten.Rows[0]["DocName"].ToString();
                DocArt = dtPrintdaten.Rows[0]["DocArt"].ToString();
            }
        }
        //
        private void SetValueToFrm()
        {
            //Lieferscheinnummer wird Manuelle eingegeben
            tbLfsNr.Text = string.Empty;

            dtpDate.Value = DateTime.Today.Date;
            dtpLfsDate.Value = DateTime.Today.Date;
            dtpAbruf.Value = DateTime.Today.Date;
            tbZeichen.Text = string.Empty;
            tbBestNr.Text = string.Empty;
        }
        //
        private void InitdtLfsDatenDBColumns()
        {
            if (dtLfsDaten.Columns["Datum"] == null)
            {
                DataColumn c0 = new DataColumn();
                c0.DataType = System.Type.GetType("System.String");
                c0.ColumnName = "Datum";
                dtLfsDaten.Columns.Add(c0);
            }
            if (dtLfsDaten.Columns["Lieferscheindatum"] == null)
            {
                DataColumn c1 = new DataColumn();
                c1.DataType = System.Type.GetType("System.String");
                c1.ColumnName = "Lieferscheindatum";
                dtLfsDaten.Columns.Add(c1);
            }
            if (dtLfsDaten.Columns["IhrZeichen"] == null)
            {
                DataColumn c2 = new DataColumn();
                c2.DataType = System.Type.GetType("System.String");
                c2.ColumnName = "IhrZeichen";
                dtLfsDaten.Columns.Add(c2);
            }
            if (dtLfsDaten.Columns["BestellNr"] == null)
            {
                DataColumn c3 = new DataColumn();
                c3.DataType = System.Type.GetType("System.String");
                c3.ColumnName = "BestellNr";
                dtLfsDaten.Columns.Add(c3);
            }
            if (dtLfsDaten.Columns["Abruf"] == null)
            {
                DataColumn c4 = new DataColumn();
                c4.DataType = System.Type.GetType("System.String");
                c4.ColumnName = "Abruf";
                dtLfsDaten.Columns.Add(c4);
            }
            if (dtLfsDaten.Columns["LfsArt"] == null)
            {
                DataColumn c5 = new DataColumn();
                c5.DataType = System.Type.GetType("System.String");
                c5.ColumnName = "LfsArt";
                dtLfsDaten.Columns.Add(c5);
            }
            if (dtLfsDaten.Columns["LfsNr"] == null)
            {
                DataColumn c6 = new DataColumn();
                c6.DataType = System.Type.GetType("System.String");
                c6.ColumnName = "LfsNr";
                dtLfsDaten.Columns.Add(c6);
            }
        }
        //
        private void SetDatadtLfsDatenDBColumns()
        {
            DataRow row = dtLfsDaten.NewRow();
            row["Datum"] = dtpDate.Value.ToShortDateString();
            row["Lieferscheindatum"] = dtpLfsDate.Value.ToShortDateString();
            row["IhrZeichen"] = tbZeichen.Text;
            row["BestellNr"] = tbBestNr.Text;
            row["Abruf"] = dtpAbruf.Value.ToShortDateString();
            row["LfsArt"] = enumFDocs.Holzrichter.ToString();
            row["LfsNr"] = tbLfsNr.Text;
            dtLfsDaten.Rows.Add(row);
        }
        //
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //
        private void PrintDirect(bool boPrintDirect)
        {
            SetDatadtLfsDatenDBColumns();
            DataSet dsDummi = new DataSet();
            for (Int32 i = 1; i <= PrintAnzahl; i++)
            {
                if (decAP_ID > 0)
                {
                    string docArt = dtPrintdaten.Rows[0]["DocArt"].ToString();
                    frmReportViewer reportview = new frmReportViewer(dsDummi, docArt);
                    reportview.GL_User = GL_User;
                    //reportview._ArtikelTableID = this._ArtikelTableID;
                    //reportview._AuftragID = this._AuftragID;
                    reportview._AuftragPosTableID = decAP_ID; ;
                    //reportview._MandantenID = _MandantenID;
                    reportview.boFDocs = true;
                    reportview.dtArtikelDetails = dtArtikelDetails;
                    reportview.dtPrintdaten = dtPrintdaten;
                    reportview.dtLfsDaten = dtLfsDaten;

                    if (boPrintDirect)
                    {
                        reportview.Hide();
                        reportview.PrintDirect();
                        reportview.Close();
                    }
                    else
                    {
                        reportview.StartPosition = FormStartPosition.CenterParent;
                        reportview.Show();
                        reportview.BringToFront();

                    }
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            PrintDirect(boDirectPrint);
            this.Close();
        }


    }
}
