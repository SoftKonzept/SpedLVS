using Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class WorkspaceViewData
    {
        public Workspaces Workspace { get; set; }
        private int BenutzerID { get; set; }
        public List<Workspaces> ListWorkspace { get; set; }

        internal MandantenViewData mandantVD { get; set; }
        internal AddressViewData adrVD { get; set; }

        public WorkspaceViewData()
        {
            InitCls();
        }

        public WorkspaceViewData(Workspaces myWorkspace) : this()
        {
            Workspace = myWorkspace;
        }

        public WorkspaceViewData(int myId) : this()
        {
            InitCls();
            Workspace.Id = myId;
            if (Workspace.Id > 0)
            {
                Fill();
            }
        }

        private void InitCls()
        {
            Workspace = new Workspaces();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Fill()
        {
            string strSQL = sql_Get_Main + " WHERE ID=" + Workspace.Id.ToString();
            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "Workspace", "Workspace", 0);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        private void SetValue(DataRow row)
        {
            int iTmp = 0;
            int.TryParse(row["ID"].ToString(), out iTmp);
            Workspace.Id = iTmp;
            Workspace.Name = row["Name"].ToString().Trim();
            Workspace.Descrition = row["Bemerkung"].ToString().Trim();
            Workspace.IsActiv = (bool)row["aktiv"];
            Workspace.ASNTransfer = (bool)row["ASNTransfer"];
            iTmp = 0;
            int.TryParse(row["MandantenID"].ToString(), out iTmp);
            Workspace.MandantId = iTmp;
            Workspace.IsLager = (bool)row["IsLager"];
            Workspace.IsSpedition = (bool)row["IsSpedition"];
            Workspace.UseAutoRowAssignment = (bool)row["UseAutoRowAssignment"];
            iTmp = 0;
            int.TryParse(row["ArtMaxCountInAusgang"].ToString(), out iTmp);
            Workspace.MaxCountArticleInStoreOut = iTmp;
            iTmp = 0;
            int.TryParse(row["AdrId"].ToString(), out iTmp);
            Workspace.WorkspaceOwner = iTmp;

            iTmp = 0;
            int.TryParse(row["AbrufDefEmpfaengerId"].ToString(), out iTmp);
            Workspace.AbrufDefEmpfaengerId = iTmp;
            iTmp = 0;
            int.TryParse(row["EingangDefEmpfaengerId"].ToString(), out iTmp);
            Workspace.EingangDefEmpfaengerId = iTmp;
            iTmp = 0;
            int.TryParse(row["EingangDefEntladeId"].ToString(), out iTmp);
            Workspace.EingangDefEntladeId = iTmp;
            iTmp = 0;
            int.TryParse(row["EingangDefBeladeId"].ToString(), out iTmp);
            Workspace.EingangDefBeladeId = iTmp;
            iTmp = 0;
            int.TryParse(row["AusgangDefEmpfaengerId"].ToString(), out iTmp);
            Workspace.AusgangDefEmpfaengerId = iTmp;
            iTmp = 0;
            int.TryParse(row["AusgangDefVersenderId"].ToString(), out iTmp);
            Workspace.AusgangDefVersenderId = iTmp;
            iTmp = 0;
            int.TryParse(row["AusgangDefEntladeId"].ToString(), out iTmp);
            Workspace.AusgangDefEntladeId = iTmp;
            iTmp = 0;
            int.TryParse(row["AusgangDefBeladeId"].ToString(), out iTmp);
            Workspace.AusgangDefBeladeId = iTmp;
            iTmp = 0;
            int.TryParse(row["UBDefEmpfaengerId"].ToString(), out iTmp);
            Workspace.UBDefEmpfaengerId = iTmp;
            iTmp = 0;
            int.TryParse(row["UBDefAuftraggeberNeuId"].ToString(), out iTmp);
            Workspace.UBDefAuftraggeberNeuId = iTmp;

            if (Workspace.MandantId > 0)
            {
                mandantVD = new MandantenViewData(Workspace.MandantId);
                Workspace.Mandant = mandantVD.Mandant.Copy();
            }
            if (Workspace.WorkspaceOwner > 0)
            {
                adrVD = new AddressViewData(Workspace.WorkspaceOwner, BenutzerID);
                Workspace.WorkspaceOwnerAddress = adrVD.Address.Copy();
            }
        }

        public void GetWorkspaceList()
        {
            ListWorkspace = new List<Workspaces>();

            string strSQL = sql_Get_Main;
            DataTable dt = new DataTable("Workspace");
            dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "Workspace", "Workspace", 0);
            foreach (DataRow dr in dt.Rows)
            {
                Workspace = new Workspaces();
                SetValue(dr);
                ListWorkspace.Add(Workspace);
            }
        }

        /// <summary>
        ///             ADD
        /// </summary>
        //public void Add()
        //{
        //}
        /// <summary>
        ///             DELETE
        /// </summary>
        //public void Delete()
        //{
        //}
        /// <summary>
        ///             UPDATE
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            string strSql = sql_Update;
            bool retVal = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            return retVal;
        }



        ///-----------------------------------------------------------------------------------------------------
        ///                             sql Statements
        ///-----------------------------------------------------------------------------------------------------

        /// <summary>
        ///             Add sql - String
        /// </summary>
        public string sql_Add
        {
            get
            {
                string strSql = string.Empty;
                return strSql;
            }
        }
        /// <summary>
        ///             GET
        /// </summary>
        public string sql_Get_Workspacelist
        {
            get
            {
                string strSql = string.Empty;
                strSql = clsArbeitsbereiche.sql_GetArbeitsbereichList();
                return strSql;
            }
        }
        /// <summary>
        ///             GET_Main
        /// </summary>
        public string sql_Get_Main
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Select * FROM Arbeitsbereich ";
                return strSql;
            }
        }

        /// <summary>
        ///             DELETE sql - String
        /// </summary>
        public string sql_Delete
        {
            get
            {
                string strSql = string.Empty;
                return strSql;
            }
        }
        /// <summary>
        ///             Update sql - String
        /// </summary>
        public string sql_Update
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Update Arbeitsbereich SET " +
                                            "Name ='" + Workspace.Name + "'" +
                                            ", Bemerkung = '" + Workspace.Descrition + "'" +
                                            ", aktiv = " + Convert.ToInt32(Workspace.IsActiv) +
                                            ", ASNTransfer=" + Convert.ToInt32(Workspace.ASNTransfer) +
                                            ", MandantenID =" + Workspace.MandantId +
                                            ", IsLager = " + Convert.ToInt32(Workspace.IsLager) +
                                            ", IsSpedition = " + Convert.ToInt32(Workspace.IsSpedition) +
                                            ", UseAutoRowAssignment=" + Convert.ToInt32(Workspace.UseAutoRowAssignment) +
                                            ", ArtMaxCountInAusgang = " + Workspace.MaxCountArticleInStoreOut +
                                            ", AdrId = " + Workspace.WorkspaceOwner +

                                    " WHERE ID='" + Workspace.Id + "'";
                return strSql;
            }
        }

    }
}

