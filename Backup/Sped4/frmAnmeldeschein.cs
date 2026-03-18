using LVS;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sped4
{
    public partial class frmAnmeldeschein : Sped4.frmTEMPLATE
    {
        public Globals._GL_USER GL_User;
        public decimal _AuftragID;
        public decimal _AuftragPos;
        public decimal _ArtikelTableID;
        public decimal _AuftragPosTableID;
        public string DocArt;
        internal string FormText = string.Empty;

        internal DataTable dt = new DataTable("Anmeldeschein");

        public frmAnmeldeschein()
        {
            InitializeComponent();
        }
        //
        //-------------- Form Load -----------------------
        //
        private void frmAnmeldeschein_Load(object sender, EventArgs e)
        {
            InitColTable();

            switch (DocArt)
            {
                case "Abholschein":
                    tbDocName.Text = "Abholschein";
                    FormText = "Infotext für Abholschein";
                    break;

                case "Lieferschein":
                    tbDocName.Text = "Lieferschein";
                    FormText = "Infotext für Lieferschein";
                    break;

                case "Anmeldeschein":
                    tbDocName.Text = "Anmeldeschein";
                    FormText = "Bitte die o. a. Ware zur Verladung bereitstellen !";
                    break;
            }
            tbText.Text = FormText;
        }
        //
        //------------ Form Close ----------------------
        //
        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //
        //------------ Spalten werden zur UserTable hinzugefügt --------------
        //
        private void InitColTable()
        {
            DataColumn column1 = new DataColumn();
            column1.DataType = System.Type.GetType("System.Int32");
            column1.Caption = "AuftragID";
            column1.ColumnName = "AuftragID";
            dt.Columns.Add(column1);

            DataColumn column2 = new DataColumn();
            column2.DataType = System.Type.GetType("System.Int32");
            column2.Caption = "AuftragPos";
            column2.ColumnName = "AuftragPos";
            dt.Columns.Add(column2);

            DataColumn column3 = new DataColumn();
            column3.DataType = System.Type.GetType("System.String");
            column3.Caption = "ZM";
            column3.ColumnName = "ZM";
            dt.Columns.Add(column3);

            DataColumn column4 = new DataColumn();
            column4.DataType = System.Type.GetType("System.String");
            column4.Caption = "Auflieger";
            column4.ColumnName = "Auflieger";
            dt.Columns.Add(column4);

            DataColumn column5 = new DataColumn();
            column5.DataType = System.Type.GetType("System.String");
            column5.Caption = "Text";
            column5.ColumnName = "Text";
            dt.Columns.Add(column5);

            DataColumn column6 = new DataColumn();
            column6.DataType = System.Type.GetType("System.String");
            column6.Caption = "DocName";
            column6.ColumnName = "DocName";
            dt.Columns.Add(column6);
        }
        //
        //--------------- Anmeldeschein erstellen ----------------
        //
        private void tsBtnSpeichern_Click(object sender, EventArgs e)
        {
            AssignValueToTable();
            OpenReportView();
            this.Close();
        }
        //
        //--------------- Assign Value -------------------
        //
        private void AssignValueToTable()
        {
            DataRow row = dt.NewRow();
            row["AuftragID"] = _AuftragID;
            row["AuftragPos"] = _AuftragPos;
            row["ZM"] = tbZM.Text;
            row["Auflieger"] = tbAuflieger.Text;
            row["Text"] = tbText.Text;
            row["DocName"] = tbDocName.Text;
            dt.Rows.Add(row);
        }
        //
        //----------- Anmeldeschein erstellen -----------------
        //
        private void OpenReportView()
        {
            DataSet ds = new DataSet(); //Leer
            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmReportViewer)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmReportViewer));
            }
            frmReportViewer reportview = new frmReportViewer(ds, DocArt);
            reportview.GL_User = GL_User;
            reportview._ArtikelTableID = this._ArtikelTableID;
            reportview._AuftragID = this._AuftragID;
            reportview._AuftragPosTableID = _AuftragPosTableID;
            // reportview._MandantenID = _MandantenID;
            reportview.dtPrintdaten = dt;  //Table Checken  Baustelle mr
            reportview.StartPosition = FormStartPosition.CenterParent;
            reportview.Show();
            reportview.BringToFront();
        }
    }
}
