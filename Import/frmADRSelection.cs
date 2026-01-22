using LVS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Import
{
    public partial class frmADRSelection : Form
    {
        internal List<clsADR> ListDestination;
        public ADR SourceAdr;
        public clsADR SelectedAdrDest;
        internal Globals._GL_SYSTEM GLSys;
        internal Globals._GL_USER GLUser;
        internal DataTable dtSource;
        public frmADRSelection(List<clsADR> myDestList, ADR mySourceAdr, Globals._GL_SYSTEM myGLSys, Globals._GL_USER myGLUser)
        {
            InitializeComponent();
            ListDestination = myDestList.ToList();
            SourceAdr = mySourceAdr;
            this.GLSys = myGLSys;
            this.GLUser = myGLUser;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmADRSelection_Load(object sender, EventArgs e)
        {
            ClearSourceADRFilds();
            SetSourceAdrToFrm();
            InitDGV();
        }
        /// <summary>
        /// 
        /// </summary>
        private void ClearSourceADRFilds()
        {
            tbADR_SUCHB.Text = string.Empty;
            tbADR_F1.Text = string.Empty;
            tbADR_F2.Text = string.Empty;
            tbADR_STRA.Text = string.Empty;
            tbADR_PLZ.Text = string.Empty;
            tbADR_Ort.Text = string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetSourceAdrToFrm()
        {
            tbADR_SUCHB.Text = this.SourceAdr.SUCHB.Trim();
            tbADR_F1.Text = this.SourceAdr.F1.Trim();
            tbADR_F2.Text = this.SourceAdr.F2.Trim();
            tbADR_STRA.Text = this.SourceAdr.STRA.Trim();
            tbADR_PLZ.Text = this.SourceAdr.PLZ.Trim();
            tbADR_Ort.Text = this.SourceAdr.ORT.Trim();
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitDGV()
        {
            dtSource = GetSource();
            this.dgvSelection.TableElement.ViewInfo.TableAddNewRow.Height = 20;
            this.dgvSelection.TableElement.ViewInfo.TableAddNewRow.MinHeight = 20;
            this.dgvSelection.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvSelection.DataSource = dtSource;
            this.dgvSelection.BestFitColumns();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DataTable GetSource()
        {
            DataTable dt = new DataTable("clsViewAdrSelection");
            dt.Columns.Add("IsSelected", typeof(Boolean));
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Adresse", typeof(string));

            foreach (clsADR a in ListDestination)
            {
                DataRow row = dt.NewRow();
                row["IsSelected"] = false;
                row["ID"] = (int)a.ID; ;
                row["Adresse"] = a.ADRString;
                dt.Rows.Add(row);
            }

            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            int iIDSel = (int)this.dgvSelection.CurrentRow.Cells["ID"].Value;
            for (int i = 0; i <= dgvSelection.SelectedRows.Count - 1; i++)
            {
                if ((int)this.dgvSelection.SelectedRows[i].Cells["ID"].Value == iIDSel)
                {
                    this.SelectedAdrDest = new clsADR();
                    this.SelectedAdrDest.InitClass(this.GLUser, this.GLSys, iIDSel, true);
                    i = dgvSelection.SelectedRows.Count;
                }
            }
            this.Close();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnAbbruch_Click(object sender, EventArgs e)
        {

        }

        public void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvSelection_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                //bool CellValue = (bool)this.dgvSelection.CurrentRow.Cells["IsSelected"].Value;
                int iId = (int)this.dgvSelection.Rows[e.RowIndex].Cells["ID"].Value;
                bool CellValue = (bool)this.dgvSelection.Rows[e.RowIndex].Cells["IsSelected"].Value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSelection_CreateRow(object sender, Telerik.WinControls.UI.GridViewCreateRowEventArgs e)
        {
            e.RowInfo.Height = 20;
            e.RowInfo.MinHeight = e.RowInfo.Height;
            
        }
    }
}
