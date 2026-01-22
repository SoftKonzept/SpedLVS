using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Sped4
{
    public partial class frmInfoSchadstoffklassen : Sped4.frmTEMPLATE
    {
        internal DataTable dt = new DataTable();

        public frmInfoSchadstoffklassen()
        {
            InitializeComponent();
        }
        //
        //--------- Load Form ----------------------
        //
        private void frmInfoSchadstoffklassen_Load(object sender, EventArgs e)
        {
            InitColUserTable();
            SetTableInfoSchadstoffklassen();
            dgv.DataSource = dt;
            //dgv.SelectNextControl(afToolStrip1, true, true, false, false);
            Int32 row = dgv.Rows.Count;
            dgv.Rows[row - 1].Selected = true;
        }
        //
        //------------ Spalten werden zur UserTable hinzugefügt --------------
        //
        private void InitColUserTable()
        {
            /***
            DataColumn column1 = new DataColumn();
            column1.DataType = System.Type.GetType("System.String");
            column1.Caption = "Farbe";
            column1.ColumnName = "Farbe";
            column1.DefaultValue = 0;
            dt.Columns.Add(column1);
            ***/

            DataColumn column2 = new DataColumn();
            column2.DataType = System.Type.GetType("System.String");
            column2.Caption = "Schadstoffklasse";
            column2.ColumnName = "Schadstoffklasse";
            dt.Columns.Add(column2);
        }
        //
        //----------- Inhalt Tabelle
        //
        private void SetTableInfoSchadstoffklassen()
        {
            DataRow row = dt.NewRow();
            //row["Farbe"] = string.Empty;
            row["Schadstoffklasse"] = "Euro 3";
            dt.Rows.Add(row);

            DataRow row1 = dt.NewRow();
            //row1["Farbe"] = string.Empty;
            row1["Schadstoffklasse"] = "Euro 4";
            dt.Rows.Add(row1);

            DataRow row2 = dt.NewRow();
            // row2["Farbe"] = string.Empty;
            row2["Schadstoffklasse"] = "Euro 5";
            dt.Rows.Add(row2);

            DataRow row3 = dt.NewRow();
            //row3["Farbe"] = string.Empty;
            row3["Schadstoffklasse"] = "Euro 6";
            dt.Rows.Add(row3);
        }
        //
        //
        //
        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string schadstoffklasse = string.Empty;

            if (e.RowIndex <= this.dgv.Rows.Count - 1)
            {
                try
                {
                    if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["Schadstoffklasse"].Value, DBNull.Value)))
                    {
                        if (dgv.Rows[e.RowIndex].Cells["Schadstoffklasse"].Value != null)
                        {
                            schadstoffklasse = (string)dgv.Rows[e.RowIndex].Cells["Schadstoffklasse"].Value;
                        }
                    }
                    //*************************
                    // Column 1  >>> Status
                    if (e.ColumnIndex == 0)
                    {
                        switch (schadstoffklasse)
                        {
                            case "Euro 3":
                                e.CellStyle.BackColor = Color.Red;

                                break;
                            case "Euro 4":
                                e.CellStyle.BackColor = Color.Yellow;

                                break;
                            case "Euro 5":
                                e.CellStyle.BackColor = Color.Green;

                                break;
                            case "Euro 6":
                                e.CellStyle.BackColor = Color.Green;

                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        //
        //------------- Form Close -----------------------
        //
        private void tsbSpeichern_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
