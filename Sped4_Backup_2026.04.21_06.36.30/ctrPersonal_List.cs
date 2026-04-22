using LVS;
using Sped4.Classes;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Sped4
{
    public partial class ctrPersonal_List : UserControl
    {
        public Globals._GL_USER GL_User;
        public ctrMenu _ctrMenu;

        public bool update = false;
        public bool aktuelleListe = true;
        public DataTable dataTable = new DataTable();
        public DataTable tmpTable = new DataTable();
        public DataTable ExcelTable = new DataTable();

        public ctrPersonal_List()
        {
            InitializeComponent();
        }
        //
        //---------- Form Personal öffenen -----------------------
        //
        public void FormOpen()
        {
            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmPersonal)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmPersonal));
            }
            frmPersonal Personal = new frmPersonal(this, update);
            Personal.StartPosition = FormStartPosition.CenterScreen;
            Personal.Show();
            Personal.BringToFront();
        }
        //
        //--------------------- Ctr schliessen ----------------------------------
        //
        public void CtrClose()
        {
            Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].Name == "TempSplitterPersonal")
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    //i = Count - 1;      // ist nur ein Controll vorhanden
                }
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrPersonal_List))
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    i = Count - 1;      // ist nur ein Controll vorhanden
                }
            }
        }

        //
        //-------------------------------------- Init der der Gridlist ----------
        //
        public void initList()
        {

            dataTable = clsPersonal.GetPersonalList(this.GL_User, aktuelleListe);
            grdList.DataSource = dataTable;
            grdList.Columns["ID"].Visible = false;
            grdList.AutoResizeColumns();
            ExcelTable.Clear();
            ExcelTable = dataTable;
        }
        //
        //----- Search Methode for the Grid -----------------------------------------------------
        //
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
                    if (grdList.Columns[_Column].Visible)
                    {
                        // Do all comparing in UpperCase so it is case insensitive
                        if (grdList[_Column, _Row].Value.ToString().ToUpper().Contains(Search))
                        {
                            // If found position on the item
                            grdList.FirstDisplayedScrollingRowIndex = _Row;
                            grdList[_Column, _Row].Selected = true;
                            isFound = true;
                        }
                    }

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

        //
        //-------------------------------------- ContextMenü für rechten Mausclick--------
        //
        private void grdList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }
        //
        //******************************************************************************************
        //---------------     Menü rechte Maustaste
        //******************************************************************************************
        //---------- Datensatz ändern -  Update  -------------------------
        private void grdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                miUpdate_Click(sender, e);
            }
        }
        //
        //
        private void miUpdate_Click(object sender, EventArgs e)
        {
            if (GL_User.write_Personal)
            {
                string strID = string.Empty;

                if (grdList.Rows.Count >= 1)
                {
                    if (grdList.Rows[grdList.CurrentRow.Index].Cells[0].Value != null)
                    {
                        if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmPersonal)) != null)
                        {
                            Functions.frm_FormTypeClose(typeof(frmPersonal));
                        }
                        //---------- ID des gewählten Datensatzes-----
                        strID = grdList.Rows[grdList.CurrentRow.Index].Cells[0].Value.ToString();
                        update = true;
                        // Update Form einfügen
                        frmPersonal Personal = new frmPersonal(this, update);
                        Personal.StartPosition = FormStartPosition.CenterScreen;
                        Personal.Show();
                        Personal.BringToFront();
                        Personal.SetUpdateItem(clsPersonal.ReadDataByID(Convert.ToInt32(strID)));
                        //Personal.ReadDataByID(Convert.ToInt32(strID));
                        initList();
                    }
                }
            }
            else
            {
                clsMessages.User_NoAuthen();
            }
        }
        //
        //----------------- Personaldaten ändern -----------------
        //
        private void miUpdate_Click_1(object sender, EventArgs e)
        {
            miUpdate_Click(sender, e);
        }
        //
        //------------- Kontakt hinzufügen   --------------------------
        //
        private void miNeu_Click(object sender, EventArgs e)
        {
            if (GL_User.write_Personal)
            {
                FormOpen();
            }
            else
            {
                clsMessages.User_NoAuthen();
            }
        }
        //
        //
        //
        private void miListeClose_Click(object sender, EventArgs e)
        {
            CtrClose();
        }
        //
        //---------------- Text Search   ----------------------------
        //
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            string SearchText = txtSearch.Text.ToString();
            string Ausgabe = string.Empty;
            dataTable = clsPersonal.GetPersonalList(this.GL_User, aktuelleListe);
            if (txtSearch.Text.ToString() == "")
            {
                initList();
            }
            else
            {
                DataRow[] rows = dataTable.Select("Name LIKE '%" + SearchText + "%'", "Name");
                tmpTable.Clear();
                tmpTable = dataTable.Clone();

                foreach (DataRow row in rows)
                {
                    Ausgabe = Ausgabe + row["Name"].ToString() + "\n";
                    tmpTable.ImportRow(row);
                }
                grdList.DataSource = tmpTable;
                ExcelTable.Clear();
                ExcelTable = tmpTable;
                dataTable = tmpTable;
            }
        }
        //
        //------------ Excel Export  -------------------
        //
        private void miExcel_Click(object sender, EventArgs e)
        {
            clsExcel excel = new clsExcel();
            //excel.ExportDataTableToExcel(ref this._ctrMenu._frmMain, ExcelTable, "Personal");
            excel.ExportDataTableToExcel(ExcelTable, "Personal");
        }
        //
        //--------- Listenart aktuell -------------------------
        //
        private void tstbAktuell_Click(object sender, EventArgs e)
        {
            aktuelleListe = true;
            afColorLabel1.myText = "Personalliste [aktuell]";
            initList();
        }
        //
        //----------- Listenart komplett ---------------------
        //incl. Personal, dass bereits ausgeschieden ist 
        private void tstbAll_Click(object sender, EventArgs e)
        {
            aktuelleListe = false;
            afColorLabel1.myText = "Personalliste [komplett]";
            initList();
        }
        //
        //---------- Liste aktualisieren  -----------------------
        //
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            initList();
        }
        //
        //-------- löschen, nur wenn der Datensatz nicht verwendet wird ------
        //
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (GL_User.write_Personal)
            {
                if (!clsResource.IsPersonalIDIn((decimal)this.grdList.Rows[grdList.CurrentRow.Index].Cells["ID"].Value))
                {
                    clsMessages mes = new clsMessages();
                    if (clsMessages.Personal_DeleteDatenSatz())
                    {
                        clsPersonal Personal = new clsPersonal();
                        Personal.BenutzerID = GL_User.User_ID;
                        Personal.ID = (decimal)this.grdList.Rows[grdList.CurrentRow.Index].Cells["ID"].Value;
                        Personal.DeletePersonal();
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
        //
        //
        //
        public void ctrPersonalListe_Resize()
        {
            Int32 gridGr = Functions.dgv_GetWidthShownGrid(ref grdList);
            for (Int32 i = 0; (i <= (this.ParentForm.Controls.Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrPersonal_List))
                {
                    this.ParentForm.Controls[i].Width = gridGr;
                    this.ParentForm.Controls[i].Refresh();
                }
            }
        }
        //
        //------------------ Fensterbreite anpassen ---------------
        //
        private void tsbtnAnpassen_Click(object sender, EventArgs e)
        {
            ctrPersonalListe_Resize();
        }
    }
}
