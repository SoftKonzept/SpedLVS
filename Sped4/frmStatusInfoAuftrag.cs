using LVS;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sped4
{
    public partial class frmStatusInfoAuftrag : Form
    {
        /************************************************************************************
         * Übersicht Status und entsprechende Images
         * - 1 unvollständig  - delete_16.png   (rotes Kreuz)
         * - 2 vollständig    - add.png         (grünes Kreuz)
         * - 3 storniert      - form_green_delete.png (grünes Form mit rotem Kreuz)
         * - 4 disponiert     - disponiert.png  (rotes Fähnchen)
         * - 5 durchgeführt   - done.png        (blaues Fähnchen)
         * - 6 Freigabe Berechnung - Freigabe_Berechnung.png  (grünes Fähnchen)
         * - 7 berechnet      - check       (gründer Haken) 
         * 
         * ***********************************************************************************/


        public decimal AuftragID;
        public decimal Gesamtgewicht;
        public frmStatusInfoAuftrag(decimal _AuftragID)
        {
            InitializeComponent();
            AuftragID = _AuftragID;
            initFrm();
        }



        private void initFrm()
        {
            clsAuftragsstatus ast = new clsAuftragsstatus();
            ast.Auftrag_ID = AuftragID;
            DataTable dt = new DataTable();
            dt = ast.GetStatusListeForAuftrag();
            dgv.DataSource = dt;
            dgv.Columns["Status"].Visible = false;
            dgv.Columns["AuftragID"].Visible = false;
            dgv.Columns["AuftragPos"].Visible = false;
            dgv.Columns["PosGemGewicht"].Visible = false;
            dgv.Columns["PosBrutto"].Visible = false;
            dgv.Columns["ID"].Visible = false;
            dgv.Columns["GesamtGemGewicht"].Visible = false;
            dgv.Columns["GesamtBrutto"].Visible = false;

        }
        //
        //
        //
        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                Int32 col = e.ColumnIndex;
                clsAuftragsstatus ast = new clsAuftragsstatus();

                if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["Status"].Value, DBNull.Value)))
                {
                    if (dgv.Rows[e.RowIndex].Cells["Status"].Value != null)
                    {
                        ast.Status = (Int32)dgv.Rows[e.RowIndex].Cells["Status"].Value;
                    }
                }
                if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["AuftragID"].Value, DBNull.Value)))
                {
                    if (dgv.Rows[e.RowIndex].Cells["AuftragID"].Value != null)
                    {
                        ast.Auftrag_ID = (decimal)dgv.Rows[e.RowIndex].Cells["AuftragID"].Value;
                    }
                }
                if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["AuftragPos"].Value, DBNull.Value)))
                {
                    if (dgv.Rows[e.RowIndex].Cells["AuftragPos"].Value != null)
                    {
                        ast.AuftragPos = (decimal)dgv.Rows[e.RowIndex].Cells["AuftragPos"].Value;
                    }
                }
                if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["ID"].Value, DBNull.Value)))
                {
                    if (dgv.Rows[e.RowIndex].Cells["ID"].Value != null)
                    {
                        ast.ID = (decimal)dgv.Rows[e.RowIndex].Cells["ID"].Value;
                    }
                }
                if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["PosGemGewicht"].Value, DBNull.Value)))
                {
                    if (dgv.Rows[e.RowIndex].Cells["PosGemGewicht"].Value != null)
                    {
                        ast.gemGewicht = (decimal)dgv.Rows[e.RowIndex].Cells["PosGemGewicht"].Value;
                    }
                }
                if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["PosBrutto"].Value, DBNull.Value)))
                {
                    if (dgv.Rows[e.RowIndex].Cells["PosBrutto"].Value != null)
                    {
                        ast.Brutto = (decimal)dgv.Rows[e.RowIndex].Cells["PosBrutto"].Value;
                    }
                }
                if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["GesamtGemGewicht"].Value, DBNull.Value)))
                {
                    if (dgv.Rows[e.RowIndex].Cells["GesamtGemGewicht"].Value != null)
                    {
                        ast.GesamtGemGewicht = (decimal)dgv.Rows[e.RowIndex].Cells["GesamtGemGewicht"].Value;
                    }
                }
                if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["GesamtBrutto"].Value, DBNull.Value)))
                {
                    if (dgv.Rows[e.RowIndex].Cells["GesamtBrutto"].Value != null)
                    {
                        ast.GesamtBrutto = (decimal)dgv.Rows[e.RowIndex].Cells["GesamtBrutto"].Value;
                    }
                }
                //********************
                if (e.ColumnIndex == 0)
                {
                    e.Value = Functions.GetDataGridCellStatusImage(ast.Status);
                    /***
                  switch (ast.Status)
                  {
                    case 1: //unvollständig
                      e.Value = Sped4.Properties.Resources.delete_16;
                      break;
                    case 2: // vollständige 
                      e.Value = Sped4.Properties.Resources.add;
                      break;
                    case 3: // storniert
                      e.Value = Sped4.Properties.Resources.form_green_delete;
                      break;
                    case 4: // disponiert
                      e.Value = Sped4.Properties.Resources.disponiert;
                      break;
                    case 5: // durchgeführt
                      e.Value = Sped4.Properties.Resources.done;
                      break;
                    case 6: //Freigabe Berechnung
                      e.Value = Sped4.Properties.Resources.Freigabe_Berechnung;
                      break;
                    case 7: //berechnet
                      e.Value = Sped4.Properties.Resources.check;
                      break;
                   }
                     * ***/
                }
                //Auftrag
                if (e.ColumnIndex == 1)
                {
                    e.Value = ast.Auftrag_ID;
                }
                //Position
                if (e.ColumnIndex == 2)
                {
                    e.Value = ast.AuftragPos;
                }
                //Positionsgewicht
                if (e.ColumnIndex == 3)
                {
                    e.Value = Functions.FormatDecimal(ast.PosGewicht);
                }
                lAuftragID.Text = ast.Auftrag_ID.ToString();
                lGesamtgewicht.Text = Functions.FormatDecimal(ast.Gewicht);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
