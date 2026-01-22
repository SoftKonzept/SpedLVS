using Sped4.Classes;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Sped4
{
    public partial class frmKDFrachtKonditionen : Sped4.frmTEMPLATE
    {

        public Int32 Konditionsart;
        public decimal KD_ID;
        public DataTable dt = new DataTable();
        public bool HasKondition = false;

        //-------------- DGV Cellstyle
        public System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyleDecimal = new DataGridViewCellStyle();
        public System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyleString = new DataGridViewCellStyle();
        public System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyleImage = new DataGridViewCellStyle();
        public System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyleInt = new DataGridViewCellStyle();


        public frmKDFrachtKonditionen(decimal _KD, Int32 _Konditionsart)
        {
            InitializeComponent();
            KD_ID = _KD;
            Konditionsart = _Konditionsart;
        }
        //
        private void frmKDFrachtKonditionen_Load(object sender, EventArgs e)
        {
            clsFrachtKonditionen fk = new clsFrachtKonditionen();
            fk.KD_ID = KD_ID;

            if (fk.IsKDKonditionIn(Konditionsart))
            {
                dt = fk.LoadKDKonditionenByKonditionsart(Konditionsart);


            }
            SetDataGrdViewCellStyle();
            LoadDgv();

            //this.dgv.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgv_EditingControlShowing);
        }
        //
        //************************* Formatierung Load DGV ****************************
        //
        /*********************************************************************************
         * Konditionsart:
         *  1: €/to und km
         *  2: €/EP 
         *  3: €/km
         *  ******************************************************************************/
        //
        private void LoadDgv()
        {
            LoadGrd();
            clsFrachtKonditionen fk = new clsFrachtKonditionen();
            fk.KD_ID = KD_ID;
            HasKondition = fk.IsKDKonditionIn(Konditionsart);
            if (HasKondition)
            {
                dt = fk.LoadKDKonditionenByKonditionsart(Konditionsart);
            }
            else
            {
                AddColToTable();
            }

            //SetColForDataTable();
            AddRowGrd();
            dgv.DataSource = dt;
            HideTableCol();

            switch (Konditionsart)
            {
                case 1:
                    dgv.Columns["to"].Visible = true;
                    dgv.Columns["to"].DefaultCellStyle = dataGridViewCellStyleInt;
                    dgv.Columns["to"].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
                    dgv.Columns["km"].Visible = true;
                    dgv.Columns["km"].DefaultCellStyle = dataGridViewCellStyleInt;
                    dgv.Columns["km"].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
                    dgv.Columns["€/To"].Visible = true;
                    dgv.Columns["€/To"].DefaultCellStyle = dataGridViewCellStyleDecimal;
                    dgv.Columns["€/To"].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
                    break;
                case 2:
                    dgv.Columns["EP"].Visible = true;
                    dgv.Columns["EP"].DefaultCellStyle = dataGridViewCellStyleInt;
                    dgv.Columns["EP"].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
                    dgv.Columns["km"].Visible = true;
                    dgv.Columns["km"].DefaultCellStyle = dataGridViewCellStyleInt;
                    dgv.Columns["km"].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
                    dgv.Columns["€/EP"].Visible = true;
                    dgv.Columns["€/EP"].DefaultCellStyle = dataGridViewCellStyleDecimal;
                    dgv.Columns["€/EP"].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
                    break;
                case 3:
                    dgv.Columns["km"].Visible = true;
                    dgv.Columns["km"].DefaultCellStyle = dataGridViewCellStyleInt;
                    dgv.Columns["km"].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
                    dgv.Columns["€/km"].Visible = true;
                    dgv.Columns["€/km"].DefaultCellStyle = dataGridViewCellStyleDecimal;
                    dgv.Columns["€/km"].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;

                    break;
            }
            //Grd fügt automatisch eine neue Zeile hinzu, diese wird wieder gelöscht
            if (this.dgv.Rows.Count > 0)
            {
                dgv.Rows.Remove(dgv.Rows[this.dgv.Rows.Count - 1]);
            }
        }
        //--------- ausblenden der Table  --------------
        private void HideTableCol()
        {
            dgv.Columns["to"].Visible = false;
            //dgv.Columns["PreisTo"].Visible = false;
            dgv.Columns["€/To"].Visible = false;
            dgv.Columns["EP"].Visible = false;
            dgv.Columns["€/EP"].Visible = false;
            dgv.Columns["km"].Visible = false;
            dgv.Columns["€/km"].Visible = false;
            dgv.Columns["ID"].Visible = false;
        }
        //
        //----------- Init Cellstyle  ------------------
        //
        private void SetDataGrdViewCellStyle()
        {
            //Font myFont = new Font("Microsoft Sans Serif", 7.25F); 
            Font myFont = new Font("Microsoft Sans Serif", 7.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            //-------------------------------- Cellstyle -----------------------
            //Decimal Preis
            //System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyleDecimal = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyleDecimal.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyleDecimal.Font = myFont;
            dataGridViewCellStyleDecimal.BackColor = Color.White;
            dataGridViewCellStyleDecimal.ForeColor = Color.Black;
            dataGridViewCellStyleDecimal.Format = "N2";
            dataGridViewCellStyleDecimal.NullValue = "0";

            //string
            //System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyleString = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyleString.Font = myFont;
            dataGridViewCellStyleString.BackColor = Color.White;
            dataGridViewCellStyleString.ForeColor = Color.Black;
            dataGridViewCellStyleString.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyleString.NullValue = "";

            //Int32
            //System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyleInt = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyleInt.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyleInt.Font = myFont;
            dataGridViewCellStyleInt.BackColor = Color.White;
            dataGridViewCellStyleInt.ForeColor = Color.Black;
            dataGridViewCellStyleInt.Format = "N0";
            dataGridViewCellStyleInt.NullValue = "0";

            //Image Zentriert
            //System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyleImage = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyleImage.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        }

        //
        //
        //
        private void LoadGrd()
        {
            dgv.Columns.Clear();
            dgv.Rows.Clear();


            System.Windows.Forms.DataGridViewImageColumn Delete = new System.Windows.Forms.DataGridViewImageColumn();
            Delete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            Delete.DefaultCellStyle = dataGridViewCellStyleImage;
            Delete.HeaderText = "";
            Delete.Name = "Delete";
            Delete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            Delete.ToolTipText = "Konditionen löschen";
            Delete.Width = 5;
            dgv.Columns.Add(Delete);
        }

        //
        //
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //
        //
        private void AddColToTable()
        {
            //Gewicht
            DataColumn Gewicht;
            Gewicht = new DataColumn();
            Gewicht.DataType = System.Type.GetType("System.Int32");
            Gewicht.ColumnName = "to";
            dt.Columns.Add(Gewicht);

            //tonne
            DataColumn EP;
            EP = new DataColumn();
            EP.DataType = System.Type.GetType("System.Int32");
            EP.ColumnName = "EP";
            dt.Columns.Add(EP);

            //km
            DataColumn km;
            km = new DataColumn();
            km.DataType = System.Type.GetType("System.Int32");
            km.ColumnName = "km";
            dt.Columns.Add(km);


            //tonne
            DataColumn PreisTo;
            PreisTo = new DataColumn();
            PreisTo.DataType = System.Type.GetType("System.Decimal");
            PreisTo.ColumnName = "€/To";
            dt.Columns.Add(PreisTo);

            //tonne
            DataColumn PreisPal;
            PreisPal = new DataColumn();
            PreisPal.DataType = System.Type.GetType("System.Decimal");
            PreisPal.ColumnName = "€/EP";
            dt.Columns.Add(PreisPal);

            //€/km
            DataColumn PreisKm;
            PreisKm = new DataColumn();
            PreisKm.DataType = System.Type.GetType("System.Decimal");
            PreisKm.ColumnName = "€/km";
            dt.Columns.Add(PreisKm);

            //ID
            DataColumn ID;
            ID = new DataColumn();
            ID.DataType = System.Type.GetType("System.Int32");
            ID.ColumnName = "ID";
            dt.Columns.Add(ID);

        }

        //************************************************ DGV ***********************************************
        //
        //----------- neue Zeile hinzufügen ---------------
        //
        private void tsbNeu_Click(object sender, EventArgs e)
        {
            AddRowGrd();
        }
        //
        //
        private void AddRowGrd()
        {
            DataRow newRow;
            newRow = dt.NewRow();
            newRow["to"] = 0;
            newRow["EP"] = 0;
            newRow["km"] = 0;
            newRow["€/To"] = 0;
            newRow["€/EP"] = 0;
            newRow["€/km"] = 0;
            newRow["ID"] = 0;
            dt.Rows.Add(newRow);
        }

        //
        //----------- löschen über Löschbutton in Zeile -----------
        //
        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //löschen
            if (e.ColumnIndex == 0)
            {
                if (Convert.ToInt32(dgv["ID", e.RowIndex].Value) > 0)
                {
                    clsFrachtKonditionen fk = new clsFrachtKonditionen();
                    fk.ID = Convert.ToInt32(dgv["ID", e.RowIndex].Value);
                    fk.DeleteKDKonditionen();
                }
                //dgv.Rows.Remove(this.dgv.Rows[this.dgv.CurrentRow.Index]);
                if (dt.Rows.Count > 0)
                {
                    dt.Rows.RemoveAt(this.dgv.CurrentRow.Index);
                }
            }
        }
        //
        //------------ deletebutton in jeder Zeile im datagrid -----------
        //
        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //DeleteButton
            if (e.ColumnIndex < 1)
            {
                e.Value = Sped4.Properties.Resources.delete_16;
            }
            else
            {
                string colName = string.Empty;
                colName = dgv.Columns[e.ColumnIndex].Name.ToString();
                e.CellStyle = GetStyle(colName);
            }
        }
        //
        private DataGridViewCellStyle GetStyle(string ColName)
        {
            DataGridViewCellStyle returnStyle = new DataGridViewCellStyle();
            switch (ColName)
            {
                case "EP":
                    returnStyle = dataGridViewCellStyleInt;
                    break;
                case "to":
                    returnStyle = dataGridViewCellStyleInt;
                    break;
                case "km":
                    returnStyle = dataGridViewCellStyleInt;
                    break;
                case "€/To":
                    returnStyle = dataGridViewCellStyleDecimal;
                    break;
                case "€/EP":
                    returnStyle = dataGridViewCellStyleDecimal;
                    break;
                case "€/km":
                    returnStyle = dataGridViewCellStyleDecimal;
                    break;
            }
            return returnStyle;
        }
        //
        //
        //
        private void dgv_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            Int32 newInt;

            if (e.ColumnIndex > 0)
            {
                if (dgv.Rows[e.RowIndex].IsNewRow) { return; }

                if (!Functions.CheckNum(e.FormattedValue.ToString()))
                {
                    MessageBox.Show("Bitte nur die reinen Zahlenwerte größer 0 eingeben!", "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
                else
                {
                    string colName = dgv.Columns[e.ColumnIndex].Name.ToString();
                    decimal price = 0.0m;

                    switch (colName)
                    {
                        case "EP":
                            if ((!Int32.TryParse(e.FormattedValue.ToString(), out newInt)) || (newInt < 0))
                            {
                                e.Cancel = true;
                                MessageBox.Show("Bitte nur die reinen Zahlenwerte größer 0 eingeben!", "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                            }
                            break;
                        case "to":
                            if ((!Int32.TryParse(e.FormattedValue.ToString(), out newInt)) || (newInt < 0))
                            {
                                e.Cancel = true;
                                MessageBox.Show("Bitte nur die reinen Zahlenwerte größer 0 eingeben!", "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                            }
                            break;
                        case "km":
                            if ((!Int32.TryParse(e.FormattedValue.ToString(), out newInt)) || (newInt < 0))
                            {
                                e.Cancel = true;
                                MessageBox.Show("Bitte nur die reinen Zahlenwerte größer 0 eingeben!", "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                            }
                            break;
                        case "€/To":
                            if ((!Decimal.TryParse(e.FormattedValue.ToString(), out price)) | (price <= 0))
                            {
                                e.Cancel = true;
                                MessageBox.Show("Bitte nur die €-Beträge größer 0 eingeben!", "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                            }
                            break;
                        case "€/EP":
                            if ((!Decimal.TryParse(e.FormattedValue.ToString(), out price)) | (price <= 0))
                            {
                                e.Cancel = true;
                                MessageBox.Show("Bitte nur die €-Beträge größer 0 eingeben!", "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                            }
                            break;
                        case "€/km":
                            if ((!Decimal.TryParse(e.FormattedValue.ToString(), out price)) | (price <= 0))
                            {
                                e.Cancel = true;
                                MessageBox.Show("Bitte nur die €-Beträge größer 0 eingeben!", "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                            }
                            break;
                    }
                }
            }
        }
        //
        //------------- Konditionen speichern ------------------------
        //
        private void tsbSpeichern_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                clsFrachtKonditionen fk = new clsFrachtKonditionen();
                fk.KD_ID = KD_ID;
                fk.InsertKDKonditionen(Konditionsart, dt);
                this.Close();
            }
        }
        //
        //

        /***
            private void dgv_CurrentCellDirtyStateChanged(object sender, EventArgs e)
            {
              if (dgv.IsCurrentCellDirty)
              {
                 dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
              }
            }
         * ***/
    }
}
