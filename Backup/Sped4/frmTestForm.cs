using System;
using System.Data;
using System.IO;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.Model;

namespace Sped4
{
    public partial class frmTestForm : frmTEMPLATE
    {
        public ctrMenu _ctrMenu;

        //bool bBit = true;
        Int32 iTest = 0;
        public frmTestForm()
        {
            InitializeComponent();
        }

        private void frmTestForm_Load(object sender, EventArgs e)
        {
            tslTest.Text = iTest.ToString();
        }

        private void tsbtnPrint_Click(object sender, System.EventArgs e)
        {
            initdgv();
        }

        private void initdgv()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(Int32));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Beschreibung", typeof(string));

            DataRow row = dt.NewRow();

            row["ID"] = 1;
            row["Name"] = "Name1";
            row["Beschreibung"] = "Die SPD verschärft die Gangart in der Flüchtlingspolitik: Bundesarbeitsministerin Andrea Nahles (SPD) kündigte am Montag Leistungskürzungen für Asylbewerber an, die nicht integrationswillig sind. Die nordrhein-westfälische ";
            dt.Rows.Add(row);

            row = dt.NewRow();

            row["ID"] = 2;
            row["Name"] = "Name2";
            row["Beschreibung"] = "Die SPD verschärft die Gangart in";
            dt.Rows.Add(row);

            this.dgv.AutoSizeRows = true;
            this.dgv.DataSource = dt;
        }

        private void toolStripButton1_Click(object sender, System.EventArgs e)
        {
            this.dgv.BestFitColumns();
        }

        private void toolStripButton2_Click(object sender, System.EventArgs e)
        {
            for (Int32 i = 0; i <= this.dgv.Columns.Count - 1; i++)
            {
                switch (iTest)
                {
                    case 0:
                        this.dgv.Columns[i].AutoSizeMode = Telerik.WinControls.UI.BestFitColumnMode.AllCells;
                        break;
                    case 1:
                        this.dgv.Columns[i].AutoSizeMode = Telerik.WinControls.UI.BestFitColumnMode.DisplayedCells;
                        break;
                    case 2:
                        this.dgv.Columns[i].AutoSizeMode = Telerik.WinControls.UI.BestFitColumnMode.DisplayedDataCells;
                        break;
                    case 3:
                        this.dgv.Columns[i].AutoSizeMode = Telerik.WinControls.UI.BestFitColumnMode.FilterCells;
                        break;
                    case 4:
                        this.dgv.Columns[i].AutoSizeMode = Telerik.WinControls.UI.BestFitColumnMode.HeaderCells;
                        break;
                    case 5:
                        this.dgv.Columns[i].AutoSizeMode = Telerik.WinControls.UI.BestFitColumnMode.SystemCells;
                        break;
                    case 6:
                        this.dgv.Columns[i].WrapText = true;
                        break;
                }
            }
            this.dgv.Refresh();

            iTest++;
            tslTest.Text = iTest.ToString();
        }


        private void toolStripButton3_Click(object sender, System.EventArgs e)
        {
            iTest = 0;
            tslTest.Text = iTest.ToString();
        }

        private void tsbtnPrintPDF_DoubleClick(object sender, EventArgs e)
        {

        }

        private void tsbtnPrintTest_Click(object sender, EventArgs e)
        {
            // Test read pdf and save in db
            string strPath = @"C:\Users\mr\Documents\";
            string pfdFile = "AN2022109030.pdf";
            string strFilePath = strPath + pfdFile;

            //pdf lesen
            //RadFixedDocument document;
            try
            {
                PdfFormatProvider provider = new PdfFormatProvider();
                using (Stream stream = File.OpenRead(strFilePath))
                {
                    RadFixedDocument document = provider.Import(stream);

                    // Do your work with the document inside the using statement. 
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }

            //string pfdFileOutput = "A_AN2022109030.pdf";
            //strFilePath = strPath + pfdFileOutput;
            //provider = new PdfFormatProvider();
            //using (Stream output = File.OpenWrite(strFilePath))
            //{
            //    //RadFixedDocument document = new FileStream("signed.pdf", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            //    provider.Export(document, output);
            //}

            //new FileStream("signed.pdf", FileMode.OpenOrCreate, FileAccess.ReadWrite)
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            //TelerikPrint tPrint = new TelerikPrint();
            //tPrint.InitClass(_ctrMenu._frmMain.GL_User, _ctrMenu._frmMain.GL_System, _ctrMenu._frmMain.system, 0,54417,0, enumPrintDocumentArt.AusgangLfs);

        }

    }
}
