using LVS;
using Sped4.Classes;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace Sped4
{
    public partial class ctrFahrzeug_List : UserControl
    {
        public Globals._GL_USER GL_User;
        public ctrMenu _ctrMenu;
        public string aktuelleListe = string.Empty;
        public DataTable dtVehicle = new DataTable("Fahrzeuge");
        public DataTable ExcelTable = new DataTable();
        public DataTable tmpTable = new DataTable();
        internal DataTable dtBesitzer = new DataTable("Mandanten");
        public clsFahrzeuge Fahrzeug;


        ///<summary>clsFahrzeuge/ctrFahrzeug_List</summary>
        ///<remarks></remarks>
        public ctrFahrzeug_List()
        {
            InitializeComponent();
            AddColToGrd();
        }
        ///<summary>clsFahrzeuge/ctrFahrzeug_List_Load</summary>
        ///<remarks></remarks>
        private void ctrFahrzeug_List_Load(object sender, EventArgs e)
        {
            Fahrzeug = new clsFahrzeuge();
            Fahrzeug.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system);

            aktuelleListe = enumFahrzeuglistenSortierung.snKIntern.ToString();
            //initList();

            //cbBesitzer.DataSource = Enum.GetNames(typeof(Sped4.Dokumente.clsBriefkopfdaten.enumMandant));
            cbBesitzer.Enabled = true;
            dtBesitzer = clsMandanten.GetMandatenList(this.GL_User.User_ID);
            clsMandanten.AddRowEmptyToDataTableMandenList(ref dtBesitzer);
            cbBesitzer.DataSource = dtBesitzer.DefaultView;
            cbBesitzer.DisplayMember = "Matchcode";
            cbBesitzer.ValueMember = "Mandanten_ID";
            cbBesitzer.SelectedIndex = 0;
        }
        ///<summary>clsFahrzeuge/miListClose_Click</summary>
        ///<remarks>Schliesst das CTR</remarks>
        private void miListClose_Click(object sender, EventArgs e)
        {
            this._ctrMenu.CloseCtrFahrzeugList();
        }
        ///<summary>clsFahrzeuge/initList</summary>
        ///<remarks></remarks>
        public void initList()
        {
            dtVehicle = clsFahrzeuge.GetVehicleList(this.GL_User, aktuelleListe);
            ExcelTable.Clear();
            ExcelTable = dtVehicle;
            tmpTable.Clear();
            tmpTable = dtVehicle.Clone();

            grdList.DataSource = dtVehicle;
            grdList.Columns["ID"].Visible = false;
            grdList.Columns["ZM"].Visible = false;
            grdList.Columns["Anhaenger"].Visible = false;
            grdList.Columns["Tuev"].Visible = false;
            grdList.Columns["SP"].Visible = false;
            grdList.Columns["KFZ"].Visible = false;
            grdList.Columns["Fabrikat"].Visible = false;
            grdList.Columns["Besitzer"].Visible = false;
            grdList.Columns["MandantenID"].Visible = false;
            grdList.Columns["intID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdList.Columns["intID"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //cbBesitzer reset
            cbBesitzer.SelectedIndex = -1;
        }
        ///<summary>clsFahrzeuge/AddColToGrd</summary>
        ///<remarks></remarks>
        private void AddColToGrd()
        {

            // Fahrzeugart
            System.Windows.Forms.DataGridViewImageColumn Column1 = new System.Windows.Forms.DataGridViewImageColumn();
            Column1.Name = "Column1";
            Column1.HeaderText = "Fahrzeugart";
            Column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns.Add(Column1);

            //KFZ
            System.Windows.Forms.DataGridViewTextBoxColumn Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Column2.Name = "Column2";
            Column2.HeaderText = "KFZ";
            Column2.Width = 70;
            Column2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            grdList.Columns.Add(Column2);

            // Fabrikat
            System.Windows.Forms.DataGridViewTextBoxColumn Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Column3.Name = "Column3";
            Column3.HeaderText = "Fabrikat";
            Column3.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            Column3.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns.Add(Column3);

            // Termine SP/Tuev
            System.Windows.Forms.DataGridViewTextBoxColumn Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Column4.Name = "Column4";
            Column4.HeaderText = "Termine";
            Column4.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            Column4.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdList.Columns.Add(Column4);


        }
        ///<summary>clsFahrzeuge/grdList_CellFormatting</summary>
        ///<remarks>formatiert Grid beim erstellen</remarks>
        private void grdList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                string ZM = string.Empty;
                string AN = string.Empty;
                DateTime TUEV = clsSystem.const_DefaultDateTimeValue_Min;
                DateTime SP = clsSystem.const_DefaultDateTimeValue_Min;
                DateTime dtTmp = clsSystem.const_DefaultDateTimeValue_Min;
                string strKFZ = string.Empty;
                string strFabrikat = string.Empty;

                if ((!object.ReferenceEquals(grdList.Rows[e.RowIndex].Cells["KFZ"].Value, DBNull.Value)))
                {
                    strKFZ = (string)grdList.Rows[e.RowIndex].Cells["KFZ"].Value;
                }
                if ((!object.ReferenceEquals(grdList.Rows[e.RowIndex].Cells["Fabrikat"].Value, DBNull.Value)))
                {
                    strFabrikat = (string)grdList.Rows[e.RowIndex].Cells["Fabrikat"].Value;
                }
                if ((!object.ReferenceEquals(grdList.Rows[e.RowIndex].Cells["ZM"].Value, DBNull.Value)))
                {
                    ZM = (string)grdList.Rows[e.RowIndex].Cells["ZM"].Value;
                }
                if ((!object.ReferenceEquals(grdList.Rows[e.RowIndex].Cells["Anhaenger"].Value, DBNull.Value)))
                {
                    AN = (string)grdList.Rows[e.RowIndex].Cells["Anhaenger"].Value;
                }
                if ((!object.ReferenceEquals(grdList.Rows[e.RowIndex].Cells["Tuev"].Value, DBNull.Value)))
                {
                    dtTmp = clsSystem.const_DefaultDateTimeValue_Min;
                    DateTime.TryParse(grdList.Rows[e.RowIndex].Cells["Tuev"].Value.ToString(), out dtTmp);
                    TUEV = dtTmp;
                }
                if ((!object.ReferenceEquals(grdList.Rows[e.RowIndex].Cells["SP"].Value, DBNull.Value)))
                {
                    dtTmp = clsSystem.const_DefaultDateTimeValue_Min;
                    DateTime.TryParse(grdList.Rows[e.RowIndex].Cells["SP"].Value.ToString(), out dtTmp);
                    SP = dtTmp;
                }


                //---- Ausgabe der Spalten -----------------------------------------
                //
                if (e.ColumnIndex == 0)                 // durchläuft die Spalten
                {
                    if ((ZM == "T") & (AN == "F"))
                    {
                        //e.Value = "SZM / Motorwagen";
                        e.Value = Sped4.Properties.Resources.truck_green16;

                    }

                    if ((ZM == "F") & (AN == "T"))
                    {
                        //e.Value = "Auflieger / Anhänger";
                        e.Value = Sped4.Properties.Resources.package;
                    }
                }
                // KFZ / Kennzeichen
                if (e.ColumnIndex == 1)
                {
                    e.Value = strKFZ;
                }
                // Fabrikat / Bemerkung
                if (e.ColumnIndex == 2)
                {
                    e.Value = strFabrikat;
                }
                // Termine SP / Tüv
                if (e.ColumnIndex == 3)
                {
                    string strTUEV = string.Empty;
                    strTUEV = TUEV.Month.ToString() + "/" + TUEV.Year.ToString();
                    string strSP = string.Empty;
                    strSP = SP.Month.ToString() + "/" + SP.Year.ToString();
                    //AUSGABE
                    e.Value = String.Format("{0}\t{1}", "TÜV: ", strTUEV) + Environment.NewLine +
                              String.Format("{0}\t{1}", "SP : ", strSP);

                }

            }
            catch
            { }
        }
        ///<summary>clsFahrzeuge/SearchGrd</summary>
        ///<remarks>Search Methode for the Grid</remarks>
        public void SearchGrd(string Search)
        {
            Int32 _Column = 0;
            Int32 _Row = 0;

            //maxSearches = the # of cells in the grid
            Int32 maxSearches = grdList.Rows.Count * grdList.Columns.Count + 1;
            //int maxSearches = dataGridView1.Rows.Count * dataGridView1.Columns.Count + 1;
            Int32 idx = 1;
            bool isFound = false;

            if (Convert.ToBoolean(Search.Length))
            {
                // If the item is not found and you haven't looked at every cell, keep searching
                while ((!isFound) & (idx < maxSearches))
                {
                    // Only search visible cells
                    // if (grdList.Columns[_Column].Visible)
                    // {
                    // Do all comparing in UpperCase so it is case insensitive
                    if (grdList[_Column, _Row].Value.ToString().ToUpper().Contains(Search))
                    {
                        // If found position on the item
                        grdList.FirstDisplayedScrollingRowIndex = _Row;
                        grdList[_Column, _Row].Selected = true;
                        isFound = true;
                    }
                    //}

                    // Increment the column.
                    _Column++;

                    // If it exceeds the column count
                    if (_Column == grdList.Columns.Count)
                    {
                        _Column = 0; //Go to 0 column
                        _Row++;      //Go to the next row

                        // If it exceeds the row count
                        if (_Row == grdList.Rows.Count)
                        {
                            _Row = 0; //Start over at the top
                        }
                    }

                }
            }
        }
        ///<summary>clsFahrzeuge/SearchGrd</summary>
        ///<remarks>ContextMenü für rechten Mausclick</remarks>
        private void grdADRList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
            if ((e.Button == MouseButtons.Left) & (e.Clicks == 2))
            {
                miUpdate_Click(sender, e);
            }
        }
        ///<summary>clsGut/miNeu_Click</summary>
        ///<remarks></remarks>
        private void miNeu_Click(object sender, EventArgs e)
        {
            //OpenForm();
            this._ctrMenu.OpenFrmFahrzeuge(this, true);
        }
        ///<summary>clsFahrzeuge/pictureBox1_Click</summary>
        ///<remarks></remarks>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Int32 Count = this.ParentForm.Controls.Count;
            Int32 i = 0;
            //for (int i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            while (i <= Count)
            {
                try
                {
                    if (this.ParentForm.Controls[i].Name == "TempSplitterFahrzeuge")
                    {
                        this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    }
                    if (this.ParentForm.Controls[i].GetType() == typeof(ctrFahrzeug_List))
                    {
                        this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    }
                }
                catch
                {

                }
                finally
                {
                    if (this.ParentForm == null)
                        i = Count + 1;
                }

            }
        }
        ///<summary>clsFahrzeuge/txtSearch_TextChanged</summary>
        ///<remarks>TEXT Search </remarks>
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string SearchText = txtSearch.Text.ToString();
            string Ausgabe = string.Empty;

            if (txtSearch.Text.ToString() == "")
            {
                initList();
            }
            else
            {
                // dataTable = ExcelTable;
                dtVehicle = clsFahrzeuge.GetVehicleList(this.GL_User, aktuelleListe);
                DataRow[] rows = dtVehicle.Select("KFZ LIKE '%" + SearchText + "%'", "KFZ");
                tmpTable.Clear();
                tmpTable = dtVehicle.Clone();

                foreach (DataRow row in rows)
                {
                    Ausgabe = Ausgabe + row["KFZ"].ToString() + "\n";
                    tmpTable.ImportRow(row);
                }

                grdList.DataSource = tmpTable;
                ExcelTable.Clear();
                ExcelTable = tmpTable;
            }
        }
        ///<summary>clsFahrzeuge/miExportExcel_Click</summary>
        ///<remarks>Fahrzeugliste an Excel exportieren </remarks>
        private void miExportExcel_Click(object sender, EventArgs e)
        {
            //clsExcel excel = new clsExcel();
            //excel.ExportDataTableToExcel(ref this._ctrMenu._frmMain, ExcelTable, "Fahrzeuge");
        }
        ///<summary>clsFahrzeuge/miUpdate_Click</summary>
        ///<remarks></remarks>
        private void miUpdate_Click(object sender, EventArgs e)
        {
            if (grdList.Rows.Count >= 1)
            {
                if (this.grdList.Rows[this.grdList.CurrentRow.Index].Cells["ID"].Value != null)
                {
                    decimal decTmp = 0;
                    decimal.TryParse(this.grdList.Rows[this.grdList.CurrentRow.Index].Cells["ID"].Value.ToString(), out decTmp);
                    if (decTmp > 0)
                    {
                        this.Fahrzeug.ID = decTmp;
                        this.Fahrzeug.Fill();
                        this._ctrMenu.OpenFrmFahrzeuge(this, false);
                    }
                }
            }
        }
        ///<summary>clsFahrzeuge/aktuelle Fahrzeugliste </summary>
        ///<remarks></remarks>
        private void fahrzeuglisteAktuellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //aktuelleListe = "snKIntern";
            aktuelleListe = enumFahrzeuglistenSortierung.snKIntern.ToString();
            afColorLabel1.myText = "Fahrzeugliste [aktuell soriert nach interne ID]";
            initList();
        }
        ///<summary>clsFahrzeuge/aktuelle Fahrzeugliste </summary>
        ///<remarks>Fahrzeugliste komplett - incl bereits verkaufter / abgemeledeter Fahrzeuge </remarks>
        private void fahrzeuglisteKomplettToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //aktuelleListe = "snKFZKennAktuell";
            aktuelleListe = enumFahrzeuglistenSortierung.snKFZKennAktuell.ToString();
            afColorLabel1.myText = "Fahrzeugliste [aktuell sortiert nach Fahrzeug]";
            initList();
        }
        ///<summary>clsFahrzeuge/fahrzeuglisteToolStripMenuItem_Click</summary>
        ///<remarks></remarks>
        private void fahrzeuglisteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aktuelleListe = "snKFZKennALL";
            aktuelleListe = enumFahrzeuglistenSortierung.snKFZKennALL.ToString();
            afColorLabel1.myText = "Fahrzeugliste [sortiert nach Fahrzeug]";
            initList();
        }
        ///<summary>clsFahrzeuge/fahrzeuglisteToolStripMenuItem_Click</summary>
        ///<remarks>aktualisieren</remarks>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            initList();
        }
        ///<summary>clsFahrzeuge/grdList_MouseDoubleClick</summary>
        ///<remarks></remarks
        private void grdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                miUpdate_Click(sender, e);
            }
        }
        ///<summary>clsFahrzeuge/toolStripButton2_Click</summary>
        ///<remarks></remarks
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (GL_User.write_KFZ)
            {
                bool FahrzeugIsUsed = true;

                if (!clsResource.IsFahrzeugIDIn((decimal)this.grdList.Rows[grdList.CurrentRow.Index].Cells["ID"].Value))
                {
                    clsMessages mes = new clsMessages();
                    if (clsMessages.Fahrzeug_DeleteDatenSatz())
                    {
                        clsFahrzeuge fahrzeug = new clsFahrzeuge();
                        fahrzeug.BenutzerID = GL_User.User_ID;
                        fahrzeug.ID = (decimal)this.grdList.Rows[grdList.CurrentRow.Index].Cells["ID"].Value;
                        fahrzeug.DeleteFahrzeug();
                        initList();
                    }
                }
                else
                {
                    clsMessages.DeleteDenied();
                }
            }
            else
            {
                clsMessages.User_NoAuthen();
            }
        }
        ///<summary>clsFahrzeuge/cbBesitzer_SelectedIndexChanged</summary>
        ///<remarks></remarks
        private void cbBesitzer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBesitzer.SelectedIndex > -1)
            {
                Int32 iTmp = 0;
                Int32.TryParse(cbBesitzer.SelectedValue.ToString(), out iTmp);
                switch (iTmp)
                {
                    case 0:
                        dtVehicle.DefaultView.RowFilter = string.Empty;
                        break;
                    default:
                        dtVehicle.DefaultView.RowFilter = "MandantenID=" + iTmp.ToString();
                        break;
                }
            }
        }
        ///<summary>clsFahrzeuge/ctrFahrzeugListe_Resize</summary>
        ///<remarks></remarks
        public void ctrFahrzeugListe_Resize()
        {
            Int32 gridGr = Functions.dgv_GetWidthShownGrid(ref grdList);
            for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrFahrzeug_List))
                {
                    this.ParentForm.Controls[i].Width = gridGr;
                    this.ParentForm.Controls[i].Refresh();
                }

            }
        }
        ///<summary>clsFahrzeuge/tsbtnAnpassen_Click</summary>
        ///<remarks>Fensterbreite optimieren - anpassen</remarks
        private void tsbtnAnpassen_Click(object sender, EventArgs e)
        {
            ctrFahrzeugListe_Resize();
        }





    }
}
