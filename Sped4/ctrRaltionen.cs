using LVS;
using Sped4.Classes;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Sped4
{
    public partial class ctrRelationen : UserControl
    {
        public Globals._GL_USER GL_User;
        public ctrMenu _ctrMenu;


        DataTable ExcelTable = new DataTable();
        DataTable relationTable = new DataTable();
        public bool update = false;
        public ctrRelationen()
        {
            InitializeComponent();
            this.Width = 200;
            lSSuchbegriff.Visible = false;
            txtSearch.Visible = false;

            initCtr();
        }
        //
        //------------- Initialisierung ----------------------------------
        //
        public void initCtr()
        {
            //Grid
            relationTable = clsRelationen.GetRelationsliste();
            ExcelTable = relationTable;
            this.afGrid1.DataSource = relationTable;
            this.afGrid1.Columns["ID"].Visible = false;
            this.afGrid1.Columns["Relation"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        //
        //----- Search Methode for the Grid -----------------------------------------------------
        //
        public void SearchGrd(string Search)
        {
            Int32 _Column = 0;
            Int32 _Row = 0;

            //maxSearches = the # of cells in the grid
            Int32 maxSearches = this.afGrid1.Rows.Count * this.afGrid1.Columns.Count + 1;
            //int maxSearches = dataGridView1.Rows.Count * dataGridView1.Columns.Count + 1;
            Int32 idx = 1;
            bool isFound = false;

            if (Convert.ToBoolean(Search.Length))
            {
                // If the item is not found and you haven't looked at every cell, keep searching
                while ((!isFound) & (idx < maxSearches))
                {
                    // Only search visible cells
                    if (this.afGrid1.Columns[_Column].Visible)
                    {
                        // Do all comparing in UpperCase so it is case insensitive
                        if (this.afGrid1[_Column, _Row].Value.ToString().ToUpper().Contains(Search))
                        {
                            // If found position on the item
                            this.afGrid1.FirstDisplayedScrollingRowIndex = _Row;
                            this.afGrid1[_Column, _Row].Selected = true;
                            isFound = true;
                        }
                    }

                    // Increment the column.
                    _Column++;

                    // If it exceeds the column count
                    if (_Column == this.afGrid1.Columns.Count)
                    {
                        _Column = 0; //Go to 0 column
                        _Row++;      //Go to the next row

                        // If it exceeds the row count
                        if (_Row == this.afGrid1.Rows.Count)
                        {
                            _Row = 0; //Start over at the top
                        }
                    }

                }
            }
        }
        //
        //----------- schliesst das Control ---------------------
        //
        private void miCloseCtr_Click(object sender, EventArgs e)
        {
            Int32 Count = this.ParentForm.Controls.Count;

            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].Name == "TempSplitterRelation")
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    //i = Count - 1;      // ist nur ein Controll vorhanden
                }
                if (this.ParentForm.Controls[i].GetType() == typeof(ctrRelationen))
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                    i = Count - 1;      // ist nur ein Controll vorhanden
                }
            }
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++ Menü rechte Maustaste  +++++++++++++++++++++++++++
        //
        //
        //
        private void afGrid1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(new Point(Cursor.Position.X, Cursor.Position.Y));
        }
        //
        //
        public void FormOpen()
        {
            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmRelation)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmRelation));
            }
            update = false;
            //-- Update Forma einfügen ---
            frmRelation Relation = new frmRelation(this, update);
            Relation.GL_User = GL_User;
            Relation.StartPosition = FormStartPosition.CenterScreen;
            Relation.ShowDialog();
            Relation.BringToFront();
        }
        //
        //
        private void miAdd_Click(object sender, EventArgs e)
        {
            FormOpen();
        }
        //
        //--------------- Guterart update ---------------------------
        //
        private void miUpdate_Click(object sender, EventArgs e)
        {
            if (this.afGrid1.Rows.Count >= 1)
            {
                //--- ausgewählte Datensatz ------
                if (this.afGrid1.Rows[this.afGrid1.CurrentRow.Index].Cells[0].Value != null)
                {
                    string strID = this.afGrid1.Rows[this.afGrid1.CurrentRow.Index].Cells[0].Value.ToString();

                    if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmRelation)) != null)
                    {
                        Functions.frm_FormTypeClose(typeof(frmRelation));
                    }
                    update = true;
                    //-- Update Forma einfügen ---
                    frmRelation Relation = new frmRelation(this, update);
                    Relation.GL_User = GL_User;
                    Relation.StartPosition = FormStartPosition.CenterScreen;
                    Relation.ShowDialog();
                    Relation.BringToFront();
                }
            }
        }
        //
        //----------- ArrayList mit den Angabe des zu ändernden Gutes ------------------
        //
        public ArrayList RelationList()
        {
            ArrayList al = new ArrayList();
            al.Add(this.afGrid1.Rows[this.afGrid1.CurrentRow.Index].Cells["ID"].Value.ToString());
            al.Add(this.afGrid1.Rows[this.afGrid1.CurrentRow.Index].Cells["Relation"].Value.ToString());
            return al;
        }
        //
        //-------------------- aktualisieren  ---------------------
        //
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            initCtr();
        }

        private void afGrid1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                miUpdate_Click(sender, e);
            }
        }
        //
        //---------- Relation löschen  -----------------------
        //
        private void miDelete_Click(object sender, EventArgs e)
        {
            if (GL_User.write_Relation)
            {
                if (this.afGrid1.Rows.Count >= 1)
                {
                    //--- ausgewählte Datensatz ------
                    if (this.afGrid1.Rows[this.afGrid1.CurrentRow.Index].Cells[0].Value != null)
                    {
                        string strRelation = this.afGrid1.Rows[this.afGrid1.CurrentRow.Index].Cells["Relation"].Value.ToString();
                        if (!clsRelationen.IsRelationUsed(strRelation))
                        {
                            if (clsMessages.Relation_DeleteDatenSatz())
                            {
                                clsRelationen rel = new clsRelationen();
                                rel.BenutzerID = GL_User.User_ID;
                                rel.ID = (decimal)this.afGrid1.Rows[this.afGrid1.CurrentRow.Index].Cells["ID"].Value;
                                rel.DeleteRelation();
                                initCtr();
                            }
                        }
                        else
                        {
                            clsMessages.DeleteDenied();
                        }
                    }
                }
            }
            else
            {
                clsMessages.User_NoAuthen();
            }
        }
        //
        //------------- Excel Export  ---------------------------
        //
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //clsExcel excel = new clsExcel();
            //excel.ExportDataTableToExcel(ref this._ctrMenu._frmMain, ExcelTable, "Relationen");
        }



    }
}
