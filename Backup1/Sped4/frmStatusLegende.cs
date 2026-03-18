using System;
using System.Data;
using System.Windows.Forms;

namespace Sped4
{
    public partial class frmStatusLegende : Sped4.frmTEMPLATE
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


        ///<summary>frmStatusLegende / frmStatusLegende</summary>
        ///<remarks></remarks>
        public frmStatusLegende()
        {
            InitializeComponent();
            InitCtr();
        }
        ///<summary>frmStatusLegende / InitCtr</summary>
        ///<remarks></remarks>
        private void InitCtr()
        {
            dgv.DataSource = GetTable();
            dgv.Columns["Beschreibung"].Visible = false;
            dgv.Columns["ID"].Visible = false;
        }
        ///<summary>frmStatusLegende / GetTable</summary>
        ///<remarks></remarks>
        private DataTable GetTable()
        {
            //Create Datatable mit der Legende für die verschiednene Stati
            DataTable dt = new DataTable();
            DataColumn col1 = dt.Columns.Add("Beschreibung", typeof(string));
            DataColumn col2 = dt.Columns.Add("ID", typeof(decimal));

            //unvollständig  - gesperrt (rotes Kreuz)
            DataRow dr1 = dt.NewRow();
            dr1["Beschreibung"] = "Die Angaben für den Auftrag sind unvollständig";
            dr1["ID"] = 1;
            dt.Rows.Add(dr1);

            //vollständig - Add(grünes Kreuz)
            DataRow dr2 = dt.NewRow();
            dr2["Beschreibung"] = "Auftrag kann disponiert werden";
            dr2["ID"] = 2;
            dt.Rows.Add(dr2);

            //storniert (grünesForm mit rotem Kreuz)
            DataRow dr3 = dt.NewRow();
            //dr3["Symbol"] = Sped4.Properties.Resources.preferences;
            dr3["Beschreibung"] = "Auftragsposition wurde storniert";
            dr3["ID"] = 3;
            dt.Rows.Add(dr3);

            //--------- Stati  ----------------------
            //disponiert (rotes Fähnchen)
            DataRow dr4 = dt.NewRow();
            dr4["Beschreibung"] = "Auftrag ist disponiert";
            dr4["ID"] = 4;
            dt.Rows.Add(dr4);

            //durchgeführt (blaues Fähnchen)
            DataRow dr5 = dt.NewRow();
            dr5["Beschreibung"] = "Transport wurde durchgeführt";
            dr5["ID"] = 5;
            dt.Rows.Add(dr5);

            //Freigabe Berechnung (grünes Fähnchen)
            DataRow dr6 = dt.NewRow();
            dr6["Beschreibung"] = "Auftrag ist freigegeben zur Berechnung";
            dr6["ID"] = 6;
            dt.Rows.Add(dr6);

            //berechnet - GS erhalten (grüner Haken)
            DataRow dr7 = dt.NewRow();
            dr7["Beschreibung"] = "Auftrag ist berechnet / Gutschrift erhalten";
            dr7["ID"] = 7;
            dt.Rows.Add(dr7);


            //--------------- weitere Inofs  ---------------------
            //Neuer Auftrag in Liste
            DataRow dr10 = dt.NewRow();
            dr10["Beschreibung"] = "Auftrag ist neu in der Auftragsliste";
            dr10["ID"] = 10;
            dt.Rows.Add(dr10);

            //AuftragImage hinterlegt - Büroklammer
            DataRow dr11 = dt.NewRow();
            dr11["Beschreibung"] = "Auftrag als Bild hinterlegt";
            dr11["ID"] = 11;
            dt.Rows.Add(dr11);

            //kein Auftrag als Image hinterlegt (graues Kreuz)
            DataRow dr12 = dt.NewRow();
            dr12["Beschreibung"] = "kein Auftrag als Bild hinterlegt";
            dr12["ID"] = 12;
            dt.Rows.Add(dr12);

            //Priorität
            DataRow dr13 = dt.NewRow();
            dr13["Beschreibung"] = "Priorität und Liefertermin < 2 Werktage";
            dr13["ID"] = 13;
            dt.Rows.Add(dr13);

            //Schriftfarbe Blau
            DataRow dr14 = dt.NewRow();
            dr14["Beschreibung"] = "Liefertermin < 2 Werktage";
            dr14["ID"] = 14;
            dt.Rows.Add(dr14);

            //Schriftfarbe blau Royal
            DataRow dr15 = dt.NewRow();
            dr15["Beschreibung"] = "Liefertermin = 2 Werktage";
            dr15["ID"] = 15;
            dt.Rows.Add(dr15);

            //Schriftfarbe grau
            DataRow dr16 = dt.NewRow();
            dr16["Beschreibung"] = "Liefertermin > 2 Werktage";
            dr16["ID"] = 16;
            dt.Rows.Add(dr16);

            return dt;
        }
        ///<summary>frmStatusLegende / dgv_CellFormatting</summary>
        ///<remarks></remarks>
        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                decimal ID = 0;
                string strBeschreibung = string.Empty;
                if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["ID"].Value, DBNull.Value)))
                {
                    if (dgv.Rows[e.RowIndex].Cells["ID"].Value != null)
                    {
                        ID = Convert.ToDecimal(dgv.Rows[e.RowIndex].Cells["ID"].Value);
                    }
                }
                //********************
                if (e.ColumnIndex == 0)
                {
                    switch (Convert.ToInt32(ID))
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                            e.Value = Functions.GetDataGridCellStatusImage(Convert.ToInt32(ID));
                            break;
                        //------------------ weitere Infos
                        case 10:
                            //e.Value=Sped4.Properties.Resources.New;
                            e.Value = Functions.GetInfoImage(false, false);
                            break;
                        case 11:
                            //e.Value=Sped4.Properties.Resources.document_attachment;
                            e.Value = Functions.GetInfoImage(true, true);
                            break;
                        case 12:
                            //e.Value = Sped4.Properties.Resources.document_delete;
                            e.Value = Functions.GetInfoImage(true, false);
                            break;
                        case 13:
                            e.Value = Sped4.Properties.Resources.Leer;
                            break;
                        case 14:
                            e.Value = Sped4.Properties.Resources.Leer;
                            break;
                        case 15:
                            e.Value = Sped4.Properties.Resources.Leer;
                            break;
                        case 16:
                            e.Value = Sped4.Properties.Resources.Leer;
                            break;
                    }
                }
                if (e.ColumnIndex == 1)
                {
                    TimeSpan span = new TimeSpan();
                    switch (Convert.ToInt32(ID))
                    {
                        case 13:
                            //Rot = 0
                            span = DateTime.Now - DateTime.Now.AddDays(1);
                            e.CellStyle.BackColor = Functions.GetColorByDringlichkeit(span);
                            break;
                        case 14:
                            //Blau= 2
                            span = DateTime.Now.AddDays(2) - DateTime.Now;
                            e.Value = "Schrift";
                            e.CellStyle.ForeColor = Functions.GetColorByDringlichkeit(span);
                            break;
                        case 15:
                            //RoyalBlau = 1
                            span = DateTime.Now.AddDays(1) - DateTime.Now;
                            e.Value = "Schrift";
                            e.CellStyle.ForeColor = Functions.GetColorByDringlichkeit(span);
                            break;
                        case 16:
                            //schwarz/Grau = > 2
                            span = DateTime.Now.AddDays(5) - DateTime.Now;
                            e.Value = "Schrift";
                            e.CellStyle.ForeColor = Functions.GetColorByDringlichkeit(span);
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
}
