using Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class UnitViewData
    {
        public Units Unit { get; set; }
        private int BenutzerID { get; set; } = 0;
        public UnitViewData unitViewData { get; set; }
        public List<Units> ListUnits { get; set; }

        public UnitViewData()
        {
            InitCls();
            GetListUnits();
        }
        public UnitViewData(int myUserId) : this()
        {
            BenutzerID = myUserId;
        }
        public UnitViewData(Units myUnit, int myUserId) : this()
        {
            Unit = myUnit.Copy();
            BenutzerID = myUserId;
        }
        public UnitViewData(int myId, int myUserId) : this()
        {
            Unit.Id = myId;
            BenutzerID = myUserId;
            if (Unit.Id > 0)
            {
                Fill();
            }
        }

        private void InitCls()
        {
            Unit = new Units();
            ListUnits = new List<Units>();
        }
        public void Fill()
        {
            string strSQL = sql_Get;
            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "Unit", "Units", BenutzerID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                }
            }
        }
        private void SetValue(DataRow row)
        {
            Unit = new Units();
            Int32 iTmp = 0;
            Int32.TryParse(row["ID"].ToString(), out iTmp);
            Unit.Id = iTmp;
            Unit.Bezeichnung = row["Bezeichnung"].ToString();
        }

        public void GetListUnits()
        {
            ListUnits = new List<Units>();
            string strSQL = string.Empty;
            DataTable dt = new DataTable("Unit");
            strSQL = sql_GetList;

            dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "GetUnit", "GetUnits", BenutzerID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                    ListUnits.Add(Unit);
                }
            }
        }
        /// <summary>
        ///             ADD
        /// </summary>
        public void Add()
        {
            string strSql = sql_Add;
            strSql = strSql + "Select @@IDENTITY as 'ID';";

            string strTmp = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSql, "Insert", BenutzerID);
            int.TryParse(strTmp, out int iTmp);
            Unit.Id = iTmp;
        }
        /// <summary>
        ///             DELETE
        /// </summary>
        public bool Delete()
        {
            bool bReturn = false;
            if (this.Unit.Id > 0)
            {
                string strSql = string.Empty;
                strSql = sql_Delete;
                bReturn = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "DELETE", BenutzerID);
            }
            return bReturn;
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
                strSql = "INSERT INTO Einheiten (Bezeichnung) VALUES (" +
                                                            "'" + Unit.Bezeichnung + "'" +
                                                            "); " +
                         "Select @@IDENTITY as 'ID'; ";
                return strSql;
            }
        }
        /// <summary>
        ///             GET
        /// </summary>
        public string sql_Get
        {
            get
            {
                string strSql = string.Empty;
                strSql = "SELECT * FROM Einheiten WHERE ID=" + Unit.Id + "; ";
                return strSql;
            }
        }
        /// <summary>
        ///             GET
        /// </summary>
        public string sql_GetList
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Select * FROM Einheiten ORDER BY Bezeichnung; ";
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
                strSql = "Delete Einheiten WHERE ID=" + Unit.Id + "; ";
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
                strSql = "Update Einheiten SET " +
                                        "Bezeichnung = '" + Unit.Bezeichnung + "' " +
                                        "WHERE ID=" + Unit.Id + "; ";
                return strSql;
            }
        }
    }
}

