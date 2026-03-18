using LVS;
using Sped4.Classes;
using Sped4.Settings;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sped4
{
    public partial class ctrUserList : UserControl
    {
        public Globals._GL_USER GL_User;
        public frmTmp _frmTmp;
        public ctrMenu _ctrMenu = null;
        internal clsUserList UserList;

        internal DataTable dtNewList = new DataTable();
        internal DataTable dtColAuswahl = new DataTable();
        internal bool bUpdate = false;


        public ctrUserList()
        {
            InitializeComponent();
        }
        ///<summary>ctrUserList / tsbtnClose_Click</summary>
        ///<remarks></remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this._ctrMenu.CloseCtrUserList();
        }
        ///<summary>ctrUserList / ctrUserList_Load</summary>
        ///<remarks></remarks>
        private void ctrUserList_Load(object sender, EventArgs e)
        {
            UserList = new clsUserList();
            UserList._GL_User = this.GL_User;
            InitDGVUserList();
            InitDGVAuswahlTable();
            InitDGVNewListItem();
        }
        ///<summary>ctrUserList / InitDGVUserList</summary>
        ///<remarks>Datagrid beinhaltet die aktuell gespeicherten User-Listen</remarks>
        private void InitDGVUserList()
        {
            if (this.GL_User.User_ID > 0)
            {
                DataTable dt = UserList.GetUserListByUser();
                this.dgvUserList.DataSource = dt;
                for (Int32 i = 0; i <= this.dgvUserList.Columns.Count - 1; i++)
                {
                    //Nur Bezeichnung soll angezeigt werden
                    if (this.dgvUserList.Columns[i].Name == "Bezeichnung")
                    {
                        this.dgvUserList.Columns[i].Visible = true;
                        this.dgvUserList.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                    else
                    {
                        this.dgvUserList.Columns[i].Visible = false;
                    }
                }
            }
        }
        ///<summary>ctrUserList / InitDGVUserList</summary>
        ///<remarks>Datagrid beinhaltet die wählbaren Felder aus der Datenbank.</remarks>
        private void InitDGVAuswahlTable()
        {
            dtColAuswahl = ctrUserListSettings.InitTableDBColumns(this.GL_User);
            SetDGVAuswahlTablePref();
        }
        ///<summary>ctrUserList / SetDGVAuswahlTablePref</summary>
        ///<remarks></remarks>
        private void SetDGVAuswahlTablePref()
        {
            if (dtColAuswahl.Rows.Count > 0)
            {
                dtColAuswahl.DefaultView.RowFilter = "Selected=0";
                this.dgvTable.DataSource = null;
                this.dgvTable.DataSource = dtColAuswahl.DefaultView;
                this.dgvTable.Columns["Table"].Visible = false;
                this.dgvTable.Columns["Col"].Visible = false;
                this.dgvTable.Columns["SQLString"].Visible = false;
                this.dgvTable.Columns["Selected"].Visible = false;
                this.dgvTable.Columns["Datenfeld"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
        ///<summary>ctrUserList / InitDGVUserList</summary>
        ///<remarks>Datagrid beinhaltet die vom User gewählten Felder, die in der neuen Liste 
        ///         angezeigt werden sollen.</remarks>
        private void InitDGVNewListItem()
        {
            dtNewList.Clear();
            dtNewList = UserList.GetUserListByBezeichnung();
            this.dgvNewList.DataSource = dtNewList;
            if (this.dgvNewList.Rows.Count > 0)
            {
                this.dgvNewList.Columns["ID"].Visible = false;
                this.dgvNewList.Columns["Table"].Visible = false;
                this.dgvNewList.Columns["Column"].Visible = false;
                this.dgvNewList.Columns["Aktion"].Visible = false;
                this.dgvNewList.Columns["UserID"].Visible = false;
                this.dgvNewList.Columns["public"].Visible = false;
                this.dgvNewList.Columns["Spalte"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            SetUserListToFrm();
        }
        ///<summary>ctrUserList / SetUserListToFrm</summary>
        ///<remarks></remarks>
        private void SetUserListToFrm()
        {
            this.dgvTable.Enabled = true;
            this.dgvNewList.Enabled = true;
            tbBezeichnung.Text = string.Empty;
            cbPrivate.Checked = false;
            gbListePref.Enabled = true;
            cbListe.Enabled = true;

            tbBezeichnung.Text = UserList.Bezeichnung;
            cbPrivate.Checked = !UserList.IsPublic;
            cbListe.Text = UserList.Action;
        }
        ///<summary>ctrUserList / dgvUserList_CellDoubleClick</summary>
        ///<remarks></remarks>
        private void dgvUserList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvUserList.Rows.Count > 0)
            {
                bUpdate = true;
                string strBezeichnung = this.dgvUserList.Rows[e.RowIndex].Cells["Bezeichnung"].Value.ToString();
                UserList.Bezeichnung = strBezeichnung;
                UserList._GL_User = this.GL_User;
                dtNewList.Clear();
                dtNewList = UserList.GetUserListByBezeichnung();
                this.dgvNewList.DataSource = dtNewList;

                InitDGVAuswahlTable();
                CompareColumns();
                SetDGVAuswahlTablePref();
                SetUserListToFrm();
            }
        }
        ///<summary>ctrUserList / CompareColumns</summary>
        ///<remarks></remarks>
        private void CompareColumns()
        {
            //die verwendeten Spalten müssen nun aus dem Auswahlgrid entfernt werden
            for (Int32 j = 0; j <= dtNewList.Rows.Count - 1; j++)
            {
                string strCol1 = dtNewList.Rows[j]["Table"].ToString() + "." + dtNewList.Rows[j]["Column"].ToString();
                for (Int32 i = 0; i <= dtColAuswahl.Rows.Count - 1; i++)
                {
                    string strCol2 = dtColAuswahl.Rows[i]["SQLString"].ToString();
                    if (strCol1 == strCol2)
                    {
                        dtColAuswahl.Rows[i]["Selected"] = true;
                    }
                }
            }
        }
        ///<summary>ctrUserList / tsbtnNewList_Click</summary>
        ///<remarks></remarks>
        private void tsbtnNewList_Click(object sender, EventArgs e)
        {
            bUpdate = false;
            this.dgvTable.Enabled = true;
            this.dgvNewList.Enabled = true;
            tbBezeichnung.Text = string.Empty;
            cbPrivate.Checked = false;
            gbListePref.Enabled = true;
            cbListe.Enabled = true;
            InitDGVAuswahlTable();
            //dgv Neue Liste leeren
            dtNewList.Clear();
            this.dgvNewList.DataSource = dtNewList;

        }
        ///<summary>ctrUserList / tsbtnSave_Click</summary>
        ///<remarks></remarks>
        private void tsbtnSave_Click(object sender, EventArgs e)
        {
            UserList.Bezeichnung = tbBezeichnung.Text.Trim();
            UserList.IsPublic = !cbPrivate.Checked;
            UserList.Action = cbListe.Text;
            UserList.dtNewList = dtNewList;
            if (bUpdate)
            {
                UserList.Update();
            }
            else
            {
                UserList.Add();
            }
            this.dgvTable.Enabled = false;
            this.dgvNewList.Enabled = false;
            tbBezeichnung.Text = string.Empty;
            cbPrivate.Checked = false;
            gbListePref.Enabled = false;
            cbListe.Enabled = false;

            InitDGVNewListItem();
            InitDGVUserList();

            InitDGVAuswahlTable();
            CompareColumns();
            SetDGVAuswahlTablePref();
            SetUserListToFrm();
        }
        ///<summary>ctrUserList / CopyTableRow</summary>
        ///<remarks>Kopiert die Inhalt /Spaltenname</remarks>
        private void CopyTableRow(Int32 iRow)
        {
            DataRow row = dtNewList.NewRow();
            row["ID"] = 0;
            row["Bezeichnung"] = tbBezeichnung.Text.Trim();
            row["Table"] = this.dgvTable.Rows[iRow].Cells["Table"].Value.ToString();
            row["Column"] = this.dgvTable.Rows[iRow].Cells["Col"].Value.ToString();
            row["Spalte"] = this.dgvTable.Rows[iRow].Cells["Datenfeld"].Value.ToString();
            row["Aktion"] = cbListe.Text;
            row["UserID"] = this.GL_User.User_ID;
            row["public"] = cbPrivate.Checked;
            dtNewList.Rows.Add(row);
        }
        ///<summary>ctrUserList / tsbtnSelectedArtToAusgang_Click</summary>
        ///<remarks></remarks>
        private void tsbtnSelectedArtToAusgang_Click(object sender, EventArgs e)
        {
            if (this.dgvTable.CurrentCell != null)
            {
                CopyTableRow(this.dgvTable.CurrentCell.RowIndex);
                this.dgvTable.Rows[this.dgvTable.CurrentCell.RowIndex].Cells["Selected"].Value = false;
                CompareColumns();
                SetDGVAuswahlTablePref();
            }
        }
        ///<summary>ctrUserList / tsbtnSelectedArtToAusgang_Click</summary>
        ///<remarks></remarks>
        private void tsbtnDelete_Click(object sender, EventArgs e)
        {
            UserList.dtNewList = this.dtNewList;
            UserList.Delete();
            InitDGVAuswahlTable();
            InitDGVNewListItem();
            InitDGVUserList();
        }
        ///<summary>ctrUserList / tsbtnSelectedArtToAusgang_Click</summary>
        ///<remarks></remarks>
        private void tsbtnDelSelectedFromList_Click(object sender, EventArgs e)
        {
            if (this.dgvNewList.CurrentCell != null)
            {
                decimal decTmp = 0;
                string strTmp = this.dgvNewList.Rows[this.dgvNewList.CurrentCell.RowIndex].Cells["ID"].Value.ToString();
                Decimal.TryParse(strTmp, out decTmp);

                for (Int32 i = 0; i <= dtNewList.Rows.Count - 1; i++)
                {
                    if (decTmp == (decimal)dtNewList.Rows[i]["ID"])
                    {
                        dtNewList.Rows.RemoveAt(i);
                    }
                }
                InitDGVAuswahlTable();
                CompareColumns();
                SetDGVAuswahlTablePref();
                SetUserListToFrm();
            }
        }
        ///<summary>ctrUserList / tsbtnAllToList_Click</summary>
        ///<remarks>Datenfelder werden komplett übernommen, dazu werden die Spalten aus dem Grid dgvTable 
        ///         in dgvColAuswahl</remarks>
        private void tsbtnAllToList_Click(object sender, EventArgs e)
        {
            CopyAllToNewUserList(true);
        }
        ///<summary>ctrUserList / tsbtnDelAllFromList_Click</summary>
        ///<remarks></remarks>
        private void tsbtnDelAllFromList_Click(object sender, EventArgs e)
        {
            CopyAllToNewUserList(false);
        }
        ///<summary>ctrUserList / CopyAllToNewUserList</summary>
        ///<remarks>Datenfelder werden komplett übernommen, dazu werden die Spalten aus dem Grid dgvTable 
        ///         in dgvColAuswahl</remarks>
        private void CopyAllToNewUserList(bool bColToUserList)
        {
            if (bColToUserList)
            {
                if (this.dgvTable.Rows.Count > 0)
                {
                    //Kopieren der einzelnen Spalten
                    for (Int32 i = 0; i <= dgvTable.Rows.Count - 1; i++)
                    {
                        CopyTableRow(i);
                    }
                    dtColAuswahl.DefaultView.RowFilter = string.Empty;
                    //Alle Datensätze in der Table dtColAuswahl werden auf 
                    //Selected = true gesetzt und sind nicht mehr wählbar
                    for (Int32 i = 0; i <= dtColAuswahl.Rows.Count - 1; i++)
                    {
                        dtColAuswahl.Rows[i]["Selected"] = true;
                    }
                }
            }
            else
            {
                dtColAuswahl.DefaultView.RowFilter = string.Empty;
                //Alle Datensätze in der Table dtColAuswahl werden auf 
                //Selected = false gesetzt und sind nicht mehr wählbar
                for (Int32 i = 0; i <= dtColAuswahl.Rows.Count - 1; i++)
                {
                    dtColAuswahl.Rows[i]["Selected"] = false;
                }
                //die gewählten Spalten der UserList werden gelöscht
                dtNewList.Clear();
            }
            SetDGVAuswahlTablePref();
        }






    }
}
