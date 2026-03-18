using LVS;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace Sped4
{
    public partial class ctrLogbuch : UserControl
    {
        public Globals._GL_USER GL_User;

        DataTable dt = new DataTable();
        DataTable dtExcel = new DataTable();

        internal delegate void ThreadCtrInvokeEventHandler();
        BackgroundWorker worker;
        //bool boAll = false;
        //bool bCommunicator = false;
        // MODI : 0= LVS Logbuch ; 1=ComLogbuch ; 2=Emails
        decimal decMode = 0;


        public ctrLogbuch(decimal myMode = 0)
        {
            InitializeComponent();
            decMode = myMode;
        }
        //
        //------------- ON LOAD ----------------
        //
        private void ctrLogbuch_Load(object sender, EventArgs e)
        {
            //Filter
            cbDatum.Checked = true;
            dtpDatumAb.Enabled = true;
            dtpDatumBis.Enabled = true;
            pbFilter.Enabled = true;



            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted +=
                 new RunWorkerCompletedEventHandler(worker_CompleteWork);

            if (!worker.IsBusy)
            {
                worker.RunWorkerAsync();
            }

            //
            this.Width = 900;
            //if (cbDatum.Checked == true)
            //{
            //  InitDGVLogbuch(false);
            //}
            //else
            //{
            //  InitDGVLogbuch(true);
            //}
        }

        /// <summary>
        /// ctrADR_List / worker_DoWork
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (decMode == 0)
            {
                clsLogbuch log = new clsLogbuch();
                dt = log.GetLogbuch(!cbDatum.Checked, dtpDatumAb.Value, dtpDatumBis.Value);
            }
            else if (decMode == 1)
            {
                clsLogbuchCon log = new clsLogbuchCon();
                dt = log.GetLogbuch(!cbDatum.Checked, dtpDatumAb.Value, dtpDatumBis.Value);
            }
            else if (decMode == 2)
            {
                dt = clsEmails.GetLogbuch(this.GL_User, !cbDatum.Checked, dtpDatumAb.Value, dtpDatumBis.Value);
            }
            dtExcel = dt;
        }
        /// <summary>
        /// ctrADR_List / worker_CompleteWork
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void worker_CompleteWork(object sender, RunWorkerCompletedEventArgs e)
        {
            dgv.DataSource = dt;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }



        //
        //
        //
        private void InitDGVLogbuch(bool boAll)
        {
            //clsLogbuch log = new clsLogbuch();
            //dt = log.GetLogbuch(boAll, dtpDatumAb.Value, dtpDatumBis.Value);
            //dtExcel = dt;

            //dgv.DataSource = dt;
            //dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            if (!worker.IsBusy)
            {
                worker.RunWorkerAsync();
            }
        }
        //
        //--------- Checkbox Datum --------------------
        //
        private void cbDatum_CheckedChanged(object sender, EventArgs e)
        {
            //Datum voreinstellen
            dtpDatumAb.Value = DateTime.Today.Date.AddDays(-3);
            dtpDatumBis.Value = DateTime.Today.Date;

            if (cbDatum.Checked == true)
            {
                cbText.Checked = false;
                dtpDatumAb.Enabled = true;
                dtpDatumBis.Enabled = true;
                pbFilter.Enabled = true;
            }
            else
            {
                dtpDatumAb.Enabled = false;
                dtpDatumBis.Enabled = false;
                pbFilter.Enabled = false;
                InitDGVLogbuch(true);
            }
        }
        //
        //--------------- Suche ------------------------
        //
        private void pbFilter_Click(object sender, EventArgs e)
        {
            if ((dtpDatumAb.Enabled == true) & (dtpDatumBis.Enabled == true))
            {
                InitDGVLogbuch(false);
            }

            //SearchText reset
            tbSearch.Text = string.Empty;
        }
        //
        //------------ Checkbox Text ---------------
        //
        private void cbText_CheckedChanged(object sender, EventArgs e)
        {
            if (cbText.Checked == true)
            {
                //cbDatum.Checked = false;
                tbSearch.Enabled = true;
            }
            else
            {
                tbSearch.Enabled = false;
            }
            tbSearch.Text = string.Empty;
        }
        //
        private void FilterSearchInDataTable(Int32 Searchart, string SearchText)
        {

            DataTable tmpTable = new DataTable();
            string Ausgabe = string.Empty;
            string Column = string.Empty;

            if (cbDatum.Checked == true)
            {
                InitDGVLogbuch(false);
            }
            else
            {
                InitDGVLogbuch(true);
            }


            switch (Searchart)
            {
                case 1:
                    //Datum
                    DateTime date = Convert.ToDateTime(SearchText);
                    Column = "Datum";
                    DataRow[] rows1 = dt.Select(Column + " >'" + date + "'", Column);

                    tmpTable.Clear();
                    tmpTable = dt.Clone();

                    foreach (DataRow row in rows1)
                    {
                        Ausgabe = Ausgabe + row[Column].ToString() + "\n";
                        tmpTable.ImportRow(row);
                    }
                    break;

                case 2:
                    //Text
                    Column = "Beschreibung";
                    DataRow[] rows2 = dt.Select(Column + " LIKE '%" + SearchText + "%'", Column);

                    tmpTable.Clear();
                    tmpTable = dt.Clone();

                    foreach (DataRow row in rows2)
                    {
                        Ausgabe = Ausgabe + row[Column].ToString() + "\n";
                        tmpTable.ImportRow(row);
                    }
                    break;
            }
            dgv.DataSource = tmpTable;
            dtExcel.Clear();
            dtExcel = tmpTable;
        }
        //
        //-------------  Suche in Beschreibung nach Text --------------
        //
        private void tbSearch_TextChanged(object sender, EventArgs e)
        {

            string SearchText = tbSearch.Text.ToString();
            string Ausgabe = string.Empty;

            if (tbSearch.Text.ToString() != "")
            {
                FilterSearchInDataTable(2, tbSearch.Text);
            }
            else
            {
                if (cbDatum.Checked == false)
                {
                    InitDGVLogbuch(true);
                }
                else
                {
                    InitDGVLogbuch(false);
                }
            }
        }
        //
        //------------ Ctr Close  ----------------------------
        //
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].Name == "TempSplitterLogbuch")
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    //i = Count - 1;      // ist nur ein Controll vorhanden
                }
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrLogbuch))
                {
                    //this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]); // CF
                    this.ParentForm.Controls[i].Dispose();  // CF 
                    i = Count - 1;      // ist nur ein Controll vorhanden
                }
            }
        }
        //
        //------------------ Logbuch aktualisieren ------------------
        //
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (cbDatum.Checked == true)
            {
                InitDGVLogbuch(false);
            }
            else
            {
                InitDGVLogbuch(true);
            }
        }
    }
}
