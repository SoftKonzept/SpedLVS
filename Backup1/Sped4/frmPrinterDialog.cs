using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Sped4
{
    public partial class frmPrinterDialog : Form
    {
        public string selectedPrinter { get; private set; }
        public string selectedView { get; private set; }
        public bool isQuerformat { get; private set; }

        internal Globals._GL_SYSTEM GL_System;
        internal string katName;
        DataTable dt;

        public frmPrinterDialog(DataTable _dt, Globals._GL_SYSTEM _Gl_System, string _katName)
        {
            InitializeComponent();
            PrinterSettings ps = new PrinterSettings();
            this.GL_System = _Gl_System;
            selectedPrinter = new PrinterSettings().PrinterName;
            string defaultPrinter = "";
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                ps.PrinterName = printer;
                if (ps.IsDefaultPrinter)
                    defaultPrinter = printer;
                cbPrinter.Items.Add(printer);
            }
            this.cbPrinter.SelectedItem = defaultPrinter;
            this.dt = _dt;
            this.katName = _katName;
            InitTscbViews();
            cbFormat.SelectedIndex = 0;


        }
        private void InitTscbViews()
        {
            cbView.Enabled = false;
            //DictPrintViews.TryGetValue("Bestand", out dicView);
            Dictionary<string, List<string>> dicView = this.GL_System.DictPrintViews.GetValueOrNull(katName);

            if (dicView != null)
            {

                cbView.Items.AddRange(dicView.Keys.ToArray());
                if (cbView.Items.Count > 0)
                {
                    cbView.Enabled = true;
                    cbView.SelectedIndex = 0;
                }

            }
            
        }
        private void tsbtnPrint_Click(object sender, EventArgs e)
        {
            if (cbPrinter.SelectedIndex > -1)
            {
                isQuerformat = false;
                if (cbFormat.SelectedIndex == 0)
                    isQuerformat = true;
                this.selectedPrinter = cbPrinter.Text;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void cbView_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedView = cbView.SelectedItem.ToString();
            this.dgv.DataSource = null;
            setView(dt, ref this.dgv, selectedView, katName);
        }
        private void setView(DataTable dt, ref RadGridView dgv, string viewname, string katName)
        {
            string MissingOnes = "";
            if (viewname != "")
            {
                Dictionary<string, List<string>> dicViews = this.GL_System.DictPrintViews.GetValueOrNull(katName);
                List<string> tmpList;
                dicViews.TryGetValue(viewname, out tmpList);
                Int32 j = 0;
                for (Int32 i = 0; i < tmpList.Count; i++)
                {
                    string temp = tmpList.ElementAt(i);
                    try
                    {
                        dt.Columns[temp].SetOrdinal(j++);
                    }
                    catch (Exception ex)
                    {
                        // Spalte des Views in DB Query nicht enthalten ...
                        if (MissingOnes != "")
                            MissingOnes += ", ";
                        MissingOnes += temp;
                        j--;
                    }

                }

                dgv.DataSource = dt;
                for (Int32 i = j; i < dgv.Columns.Count; i++)
                {
                    dgv.Columns[i].IsVisible = false;
                }
                dgv.BestFitColumns();
                Console.WriteLine(MissingOnes);
            }
        }
    }

}
