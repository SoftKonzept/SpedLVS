using LVS;
using Sped4.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Sped4
{
    public partial class ctrFilterSelect : UserControl
    {
        public ctrMenu ctrMenu;

        internal clsUserList UserList;
        internal Globals._GL_SYSTEM GL_System;
        internal Globals._GL_USER GL_User;
        internal DataTable dtUserFilter;

        internal List<string> listOperatoren = new List<string>()
        {
            "=",
            "<>",
            "<",
            "<=",
            ">",
            ">="
        };

        public ctrFilterSelect()
        {
            InitializeComponent();
        }
        ///<summary>ctrFilterSelect / InitCtr</summary>
        ///<remarks></remarks>
        public void InitCtr()
        {
            this.GL_System = this.ctrMenu._frmMain.GL_System;
            this.GL_User = this.ctrMenu._frmMain.GL_User;
            //Init UserList
            UserList = new clsUserList();
            UserList._GL_User = this.GL_User;
            UserList.InitSubClass();
            //Filter ermitteln
            InitDGVFilter();
        }
        ///<summary>ctrFilterSelect / InitDGVFilter</summary>
        ///<remarks></remarks>
        public void InitDGVFilter()
        {
            UserList.UserListDaten.dtUserListDaten.Clear();
            UserList.Action = enumUserListAction.Statistik.ToString();
            this.dgvFilter.DataSource = UserList.dtUserFilterAvailable;
        }
        ///<summary>ctrFilterSelect / InitDGVFilterDaten</summary>
        ///<remarks></remarks>
        private void InitDGVFilterDaten()
        {
            dtUserFilter = new DataTable();
            dtUserFilter = UserList.UserListDaten.dtUserListDaten;
            if (dtUserFilter.Rows.Count > 0)
            {
                dtUserFilter.Columns.Add("Filterwert", typeof(string));
                dtUserFilter.Columns.Add("Operator", typeof(string));
                dtUserFilter.Columns["Filterwert"].SetOrdinal(0);
                dtUserFilter.Columns["Operator"].SetOrdinal(1);
                dtUserFilter.Columns["ColViewName"].SetOrdinal(2);
                dtUserFilter.Columns["Table"].SetOrdinal(3);
            }
            this.dgvFilterDaten.DataSource = dtUserFilter;
            this.dgvFilterDaten.AllowEditRow = true;
            this.dgvFilterDaten.Columns["Table"].HeaderText = "Tabelle";
            this.dgvFilterDaten.Columns["Table"].ReadOnly = true;
            this.dgvFilterDaten.Columns["ColViewName"].HeaderText = "Feldname";
            this.dgvFilterDaten.Columns["ColViewName"].ReadOnly = true;

            for (Int32 i = 0; i <= this.dgvFilterDaten.Columns.Count - 1; i++)
            {
                string strHeaderText = this.dgvFilterDaten.Columns[i].HeaderText.ToString();
                if (
                    (strHeaderText != "Tabelle") &
                    (strHeaderText != "Feldname") &
                    (strHeaderText != "Operator") &
                    (strHeaderText != "Filterwert")
                   )
                {
                    this.dgvFilterDaten.Columns[i].IsVisible = false;
                }
            }
            this.dgvFilterDaten.BestFitColumns();
        }
        ///<summary>ctrFilterSelect / GetFilterValue</summary>
        ///<remarks></remarks>
        public DataTable GetFilterValue()
        {
            //initialisieren der Datatable
            DataTable dt = new DataTable();
            dt.Columns.Add("Filterwert", typeof(string));
            dt.Columns.Add("Operator", typeof(string));
            dt.Columns.Add("Spalte", typeof(string));
            dt.Columns.Add("Tabelle", typeof(string));

            //Datatable füllen
            for (Int32 i = 0; i <= this.dgvFilterDaten.Rows.Count - 1; i++)
            {
                DataRow row = dt.NewRow();
                row["Filterwert"] = this.dgvFilterDaten.Rows[i].Cells["Filterwert"].Value.ToString();
                row["Operator"] = this.dgvFilterDaten.Rows[i].Cells["Operator"].Value.ToString();
                row["Spalte"] = this.dgvFilterDaten.Rows[i].Cells["Spalte"].Value.ToString();
                row["Tabelle"] = this.dgvFilterDaten.Rows[i].Cells["Tabelle"].Value.ToString();
                dt.Rows.Add(row);
            }
            return dt;
        }
        ///<summary>ctrFilterSelect / dgvFilter_MouseDoubleClick</summary>
        ///<remarks>Ausgewählten Filter übernehmen</remarks>
        private void dgvFilter_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            decimal decTmp = 0;
            string strTmp = this.dgvFilter.Rows[this.dgvFilter.CurrentRow.Index].Cells["ID"].Value.ToString();
            Decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                UserList.UserListDaten.UserListID = decTmp;
                InitDGVFilterDaten();
            }
        }



    }
}
