using LVS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Sped4
{
    public partial class ctrGridPrinter : UserControl
    {
        public ctrGridPrinter(DataTable _dt, Globals._GL_SYSTEM _Gl_System, string _katName, string _selViewForPrint)
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

            Functions.InitComboViews(GL_System, ref cbView, katName, true);
            cbFormat.SelectedIndex = 0;
            selectedView = _selViewForPrint;
            SetToolStripComboToSelectedValue(ref cbView, selectedView);
        }
        public ctrGridPrinter(RadGridView _dgv, Globals._GL_SYSTEM _Gl_System, string _katName, string _selViewForPrint)
        {
            InitializeComponent();
            PrinterSettings ps = new PrinterSettings();
            this.GL_System = _Gl_System;
            selectedPrinter = new PrinterSettings().PrinterName;
            selectedView = _selViewForPrint;
            string defaultPrinter = "";
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                ps.PrinterName = printer;
                if (ps.IsDefaultPrinter)
                    defaultPrinter = printer;
                cbPrinter.Items.Add(printer);
            }
            this.cbPrinter.SelectedItem = defaultPrinter;
            this.dgv = _dgv;
            this.dgv.AllowRowResize = true;

            if (this.dgv.Columns.Contains("ArtikelID"))
            {
                this.dgv.Columns["ArtikelID"].IsVisible = false;
            }
            this.katName = _katName;
            selectedView = _selViewForPrint;
            SetToolStripComboToSelectedValue(ref cbView, selectedView);
            Functions.InitComboViews(GL_System, ref cbView, katName, true);
            cbFormat.SelectedIndex = 0;
        }

        public List<string> cols = new List<string>();
        public List<string> colNames { get { return cols; } }
        public string selectedPrinter { get; private set; }
        public string selectedView { get; private set; }
        public bool isQuerformat { get; set; }

        internal Globals._GL_SYSTEM GL_System;
        internal string katName;
        DataTable dt;

        private void SetToolStripComboToSelectedValue(ref ToolStripComboBox combo, string myVal)
        {
            Int32 iSel = 0;
            for (Int32 i = 0; i <= combo.Items.Count - 1; i++)
            {
                string strTmp = combo.Items[i].ToString();
                if (strTmp == myVal)
                {
                    iSel = i;
                    break;
                }
            }
            combo.SelectedIndex = iSel;
            selectedView = combo.Text;
        }



        private void InitTscbViews()
        {
            cbView.Enabled = false;
            //DictPrintViews.TryGetValue("Bestand", out dicView);
            //Dictionary<string, List<string>> dicView = this.GL_System.DictPrintViews.GetValueOrNull(katName);

            Dictionary<string, List<string>> dicView;
            this.GL_System.DictPrintViews.TryGetValue(katName, out dicView);

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

        public void add(frmDialog fd)
        {
            this.frmDialog = fd;
        }

        private void tsbtnPrint_Click(object sender, EventArgs e)
        {
            if (cbPrinter.SelectedIndex > -1)
            {
                this.cols = new List<string>();
                isQuerformat = false;
                if (cbFormat.SelectedIndex == 0)
                    isQuerformat = true;
                this.selectedPrinter = cbPrinter.Text;

                for (int i = 0; i < dgv.ColumnCount; i++)
                {
                    if (dgv.Columns[i].IsVisible == true)
                    {
                        cols.Add(dgv.Columns[i].Name);
                    }
                }

                frmDialog.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            frmDialog.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void cbView_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedView = cbView.SelectedItem.ToString();
            this.dgv.DataSource = null;
            Functions.setPrintView(ref dt, ref this.dgv, katName, selectedView, GL_System);
            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                dgv.Columns[i].HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;

            }
        }
        private frmDialog frmDialog;

        private void dgv_PrintCellFormatting(object sender, PrintCellFormattingEventArgs e)
        {
            try
            {
                if (e.Column != null)
                {
                    string strColName = e.Column.Name.ToString();
                    //Int32 i = e.Column.GetActualWidth();
                    switch (strColName)
                    {
                        case "LVSNr":
                        case "Tage":
                        case "Dauer":
                            e.PrintCell.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

                            break;

                        case "Lagerdauer":
                            if (e.Row.Index == -1)
                            {
                                if (e.PrintCell.Text.Equals("Lagerdauer"))
                                {
                                    e.PrintCell.Text = "Tage";
                                }
                            }
                            e.PrintCell.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                            break;

                        case "Dicke":
                        case "Breite":
                        case "Länge":
                        case "Laenge":
                        case "Höhe":
                        case "Hoehe":
                            if (e.Row.Index == -1)
                            {
                                e.PrintCell.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                            }
                            if (e.Row.Index > -1)
                            {
                                e.PrintCell.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
                                //e.Column.Width = 50;
                                decimal decTmp = 0;
                                Decimal.TryParse(e.PrintCell.Text, out decTmp);
                                e.PrintCell.Text = Functions.FormatDecimal(decTmp);
                            }
                            break;

                        case "Brutto":
                            if (e.Row.Index == -1)
                            {
                                if (!e.PrintCell.Text.Equals("Brutto"))
                                {
                                    decimal decTmp = 0;
                                    Decimal.TryParse(e.PrintCell.Text, out decTmp);
                                    e.PrintCell.Text = decTmp.ToString("#,##0.000");
                                    e.PrintCell.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
                                }
                                else
                                {
                                    e.PrintCell.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                                }
                            }
                            if (e.Row.Index > -1)
                            {
                                e.PrintCell.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
                                //e.Column.Width = 65;
                                decimal decTmp = 0;
                                Decimal.TryParse(e.PrintCell.Text, out decTmp);
                                e.PrintCell.Text = decTmp.ToString("#,##0.000");
                            }
                            break;

                        case "Netto":
                            if (e.Row.Index == -1)
                            {
                                if (!e.PrintCell.Text.Equals("Netto"))
                                {
                                    decimal decTmp = 0;
                                    Decimal.TryParse(e.PrintCell.Text, out decTmp);
                                    e.PrintCell.Text = decTmp.ToString("#,##0.000");
                                    e.PrintCell.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
                                }
                                else
                                {
                                    e.PrintCell.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                                }
                            }
                            if (e.Row.Index > -1)
                            {
                                e.PrintCell.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
                                decimal decTmp = 0;
                                Decimal.TryParse(e.PrintCell.Text, out decTmp);
                                e.PrintCell.Text = decTmp.ToString("#,##0.000");
                            }
                            break;

                        case "Eingangsdatum":
                            if (e.Row.Index == -1)
                            {
                                if (e.PrintCell.Text.Equals("Eingangsdatum"))
                                {
                                    e.PrintCell.Text = "E-Datum";
                                }
                            }
                            e.PrintCell.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                            break;

                        case "Ausgangsdatum":
                            if (e.Row.Index == -1)
                            {
                                if (e.PrintCell.Text.Equals("Ausgangsdatum"))
                                {
                                    e.PrintCell.Text = "A-Datum";
                                }
                            }
                            e.PrintCell.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                            break;

                        case "Werksnummer":
                            if (e.Row.Index == -1)
                            {
                                if (e.PrintCell.Text.Equals("Werksnummer"))
                                {
                                    e.PrintCell.Text = "Werks-Nr";
                                    e.PrintCell.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                                }
                            }
                            break;
                        case "Produktionsnummer":
                            //e.Column.Width = 25;
                            if (e.Row.Index == -1)
                            {
                                if (e.PrintCell.Text.Equals("Produktionsnummer"))
                                {
                                    e.PrintCell.Text = "Prod-Nr";
                                    e.PrintCell.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                                }
                            }
                            break;

                        case "iO":
                        case "Reihe":
                            e.PrintCell.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                            break;

                        case "neueReihe":
                        case "n.Reihe":
                            if (e.Row.Index == -1)
                            {
                                if (e.PrintCell.Text.Equals("neueReihe"))
                                {
                                    e.PrintCell.Text = "n.Reihe";
                                }
                            }
                            e.PrintCell.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                            break;

                        case "Schaden":
                            e.Column.WrapText = true;
                            //e.Column.AutoSizeMode = BestFitColumnMode.AllCells;
                            //e.Column.AutoSizeMode = BestFitColumnMode.DisplayedCells;
                            //e.Column.BestFit();
                            //string strValue = e.Row.Cells["Schaden"].Value.ToString();
                            //if (!strValue.Equals(String.Empty))
                            //{
                            //    e.Column.WrapText = true;
                            //    Int32 iHeight = e.Row.Height;

                            //    //e.Row.Height = e.Column.Width               
                            //}
                            //else
                            //{
                            //    e.Row.AllowResize = false;
                            //}
                            break;

                        default:
                            if (e.Row.Index > -1)
                            {
                                e.PrintCell.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }
        }

        private void dgv_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            //e.RowElement.AutoSize = true;            
            //e.RowElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            //e.RowElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.FitToAvailableSize;
        }

        private void dgv_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            //e.Column.BestFit();
            //e.Column.AutoSizeMode = BestFitColumnMode.DisplayedCells;
            //if (e.Column.UniqueName.Equals("Schaden"))
            //{
            //   // e.CellElement.AutoSize = true;
            //   // e.CellElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.Auto;
            //    string strValue = e.Row.Cells["Schaden"].Value.ToString();
            //    if (!strValue.Equals(String.Empty))
            //    {
            //        //e.Row.AllowResize = true;
            //        e.Column.WrapText = true;
            //        e.CellElement.TextWrap = true;
            //        //e.Row.MaxHeight = (Int32)e.CellElement.DesiredSize.Height+1;
            //        //e.Row.Height = (Int32)e.CellElement.DesiredSize.Height;
            //        Int32 iHeight = Math.Max(e.Row.Height, (Int32)e.CellElement.DesiredSize.Height);
            //        if (iHeight > 0)
            //        {
            //            iHeight = iHeight;
            //        }
            //        e.Row.Height = iHeight;
            //    }
            //}
        }



    }
}
