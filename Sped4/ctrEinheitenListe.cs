using LVS;
using Sped4.Classes;
using System;
using System.Data;
using System.Windows.Forms;
namespace Sped4
{
    public partial class ctrEinheitenListe : UserControl
    {
        public Globals._GL_USER GL_User;
        internal ctrMenu _ctrMenu;
        internal clsEinheiten Einheit;
        internal DataTable dtEinheiten;
        internal bool bUpdate = false;


        public ctrEinheitenListe()
        {
            InitializeComponent();
        }
        ///<summary>ctrEinheitenListe / ctrEinheitenListe_Load</summary>
        ///<remarks>Setzt die Standardwerte</remarks>
        private void ctrEinheitenListe_Load(object sender, EventArgs e)
        {
            dtEinheiten = new DataTable();
            Einheit = new clsEinheiten();
            InitDGV();
        }
        ///<summary>ctrEinheitenListe / InitDGV</summary>
        ///<remarks></remarks>
        private void InitDGV()
        {
            dtEinheiten = clsEinheiten.GetEinheiten(this.GL_User);
            this.dgv.DataSource = dtEinheiten;
            this.dgv.Columns["Bezeichnung"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        ///<summary>ctrEinheitenListe / ctrEinheitenListe_Load</summary>
        ///<remarks>Setzt die Standardwerte</remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this._ctrMenu.CloseCtrEinheitenListe();
        }
        ///<summary>ctrEinheitenListe / tsbNeu_Click</summary>
        ///<remarks>Einheit anlegen</remarks>
        private void tsbNeu_Click(object sender, EventArgs e)
        {
            tbBezeichnung.Text = string.Empty;
            tbBezeichnung.Enabled = true;
            tbBezeichnung.Focus();
        }
        ///<summary>ctrEinheitenListe / tsbtnSave_Click</summary>
        ///<remarks>Einheit speichern</remarks>
        private void tsbtnSave_Click(object sender, EventArgs e)
        {
            if (tbBezeichnung.Text.Trim() != string.Empty)
            {
                Einheit._GL_User = this.GL_User;
                Einheit.Bezeichnung = tbBezeichnung.Text.Trim();

                if (bUpdate)
                {
                    Einheit.Update();
                    InitDGV();
                    tbBezeichnung.Text = string.Empty;
                    tbBezeichnung.Enabled = false;
                }
                else
                {
                    if (!Einheit.ExistEinheitsbezeichnung())
                    {
                        Einheit.Add();
                        InitDGV();
                        tbBezeichnung.Text = string.Empty;
                        tbBezeichnung.Enabled = false;
                    }
                    else
                    {
                        clsMessages.Einheiten_BezeichnungExist();
                        tbBezeichnung.Focus();
                    }
                }
            }
        }
        ///<summary>ctrEinheitenListe / SetEinheitToFrm</summary>
        ///<remarks></remarks>
        private void SetEinheitToFrm()
        {
            tbBezeichnung.Text = Einheit.Bezeichnung;
        }
        ///<summary>ctrEinheitenListe / dgv_CellDoubleClick</summary>
        ///<remarks></remarks>
        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgv.Rows.Count > 0)
            {
                decimal decTmp = 0;
                string strTmp = this.dgv.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                Decimal.TryParse(strTmp, out decTmp);
                if (decTmp > 0)
                {
                    Einheit.ID = decTmp;
                    Einheit.Bezeichnung = this.dgv.Rows[e.RowIndex].Cells["Bezeichnung"].Value.ToString();
                    SetEinheitToFrm();
                    bUpdate = true;
                    tbBezeichnung.Enabled = true;
                }
            }
        }
        ///<summary>ctrEinheitenListe / tsbtnRefresh_Click</summary>
        ///<remarks></remarks>
        private void tsbtnRefresh_Click(object sender, EventArgs e)
        {
            InitDGV();
        }
        ///<summary>ctrEinheitenListe / tsbtnRefresh_Click</summary>
        ///<remarks></remarks>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            tbBezeichnung.Enabled = false;
            tbBezeichnung.Text = string.Empty;
            Einheit.Delete();
            InitDGV();
        }



    }
}
