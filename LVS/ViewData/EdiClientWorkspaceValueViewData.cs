using LVS.Constants;
using LVS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class EdiClientWorkspaceValueViewData
    {
        public EdiClientWorkspaceValue AdrWorkspaceAssingment { get; set; }
        public List<EdiClientWorkspaceValue> ListEdiAdrWorkspaceAssignments { get; set; }
        private int BenutzerID { get; set; } = 0;
        public Globals._GL_SYSTEM GLSystem { get; set; }
        public Globals._GL_USER GL_USER { get; set; }
        public clsSystem System { get; set; }

        public EdiClientWorkspaceValueViewData()
        {
            //InitCls();
            ListEdiAdrWorkspaceAssignments = new List<EdiClientWorkspaceValue>();
            GetEdiAdrWorkspaceAssignmentList();
            InitCls();
        }
        public EdiClientWorkspaceValueViewData(int myUserId) : this()
        {
            BenutzerID = myUserId;
        }
        public EdiClientWorkspaceValueViewData(EdiClientWorkspaceValue myAssingment, int myUserId) : this()
        {
            AdrWorkspaceAssingment = myAssingment.Copy();
            BenutzerID = myUserId;
        }
        public EdiClientWorkspaceValueViewData(int myId, int myUserId) : this()
        {
            AdrWorkspaceAssingment.Id = myId;
            BenutzerID = myUserId;
            if (AdrWorkspaceAssingment.Id > 0)
            {
                Fill();
            }
        }
        public EdiClientWorkspaceValueViewData(int myAdrId, int myAsnArtId, int myWorkspaceId, int myUserId) : this()
        {
            AdrWorkspaceAssingment.Id = 0;
            AdrWorkspaceAssingment.AdrId = myAdrId;
            AdrWorkspaceAssingment.WorkspaceId = myWorkspaceId;
            AdrWorkspaceAssingment.AsnArtId = myAsnArtId;
            AdrWorkspaceAssingment.Property = string.Empty;
            BenutzerID = myUserId;

            GetByProperties();
        }

        public EdiClientWorkspaceValueViewData(int myAdrId, int myAsnArtId, int myWorkspaceId, string myProperty, int myUserId) : this()
        {
            AdrWorkspaceAssingment.Id = 0;
            AdrWorkspaceAssingment.AdrId = myAdrId;
            AdrWorkspaceAssingment.WorkspaceId = myWorkspaceId;
            AdrWorkspaceAssingment.AsnArtId = myAsnArtId;
            AdrWorkspaceAssingment.Property = myProperty;
            BenutzerID = myUserId;

            GetByProperties();
        }

        private void InitCls()
        {
            AdrWorkspaceAssingment = new EdiClientWorkspaceValue();
        }
        public void Fill()
        {
            string strSQL = sql_Get;
            DataTable dt = clsSQLCOM.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "EdiArdWorkspaceAssignment", "EdiArdWorkspaceAssignment", BenutzerID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                }
            }
        }
        public void GetByProperties()
        {
            string strSQL = sql_GetByProperties;
            DataTable dt = clsSQLCOM.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "EdiArdWorkspaceAssignment", "EdiArdWorkspaceAssignment", BenutzerID);
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
            AdrWorkspaceAssingment = new EdiClientWorkspaceValue();
            Int32 iTmp = 0;
            Int32.TryParse(row["ID"].ToString(), out iTmp);
            AdrWorkspaceAssingment.Id = iTmp;
            iTmp = 0;
            Int32.TryParse(row["AdrId"].ToString(), out iTmp);
            AdrWorkspaceAssingment.AdrId = iTmp;

            if (AdrWorkspaceAssingment.AdrId > 0)
            {
                AddressViewData adrVD = new AddressViewData(AdrWorkspaceAssingment.AdrId, 1);
                if (adrVD.Address.Id > 0)
                {
                    AdrWorkspaceAssingment.Address = adrVD.Address.Copy();
                }
            }
            iTmp = 0;
            Int32.TryParse(row["WorkspaceId"].ToString(), out iTmp);
            AdrWorkspaceAssingment.WorkspaceId = iTmp;
            iTmp = 0;
            Int32.TryParse(row["AsnArtId"].ToString(), out iTmp);
            AdrWorkspaceAssingment.AsnArtId = iTmp;
            AdrWorkspaceAssingment.Property = row["Property"].ToString();
            AdrWorkspaceAssingment.Value = row["Value"].ToString();

            DateTime dtTmp = new DateTime(1900, 1, 1);
            DateTime.TryParse(row["Created"].ToString(), out dtTmp);
            AdrWorkspaceAssingment.Created = dtTmp;
            AdrWorkspaceAssingment.Direction = row["Direction"].ToString(); ;
        }

        public void GetEdiAdrWorkspaceAssignmentList()
        {
            ListEdiAdrWorkspaceAssignments = new List<EdiClientWorkspaceValue>();

            string strSQL = string.Empty;
            DataTable dt = new DataTable("EdiAdrWorkspaceAssignment");
            strSQL = sql_Get_List;
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EdiAdrWorkspaceAssignment");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                    ListEdiAdrWorkspaceAssignments.Add(AdrWorkspaceAssingment);
                }
            }
        }
        /// <summary>
        ///             ADD
        /// </summary>
        public void Add()
        {
            string strSql = sql_Add;
            strSql = strSql + " Select @@IDENTITY as 'ID';";
            string strTmp = clsSQLCOM.ExecuteSQLWithTRANSACTIONGetValue(strSql, "Insert", BenutzerID);
            int.TryParse(strTmp, out int iTmp);
            AdrWorkspaceAssingment.Id = iTmp;
        }
        /// <summary>
        ///             DELETE
        /// </summary>
        public bool Delete()
        {
            bool retVal = false;
            string strSql = sql_Delete;
            retVal = clsSQLCOM.ExecuteSQLWithTRANSACTION(strSql, "DELETE", BenutzerID);
            return retVal;
        }
        /// <summary>
        ///             UPDATE
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            bool retVal = false;
            string strSql = sql_Update;
            retVal = clsSQLCOM.ExecuteSQLWithTRANSACTION(strSql, "Update", BenutzerID);
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
                AdrWorkspaceAssingment.Created = DateTime.Now;
                strSql = "INSERT INTO EdiClientWorkspaceValue ([AdrId] " +
                                                                ",[WorkspaceId] " +
                                                                ",[AsnArtId] " +
                                                                ",[Property] " +
                                                                ",[Value] " +
                                                                ",[Created] " +
                                                                ",[Direction] " +
                                                                ") " +
                                         "VALUES (" + AdrWorkspaceAssingment.AdrId +
                                                  ", " + AdrWorkspaceAssingment.WorkspaceId +
                                                  ", " + AdrWorkspaceAssingment.AsnArtId +
                                                  ", '" + AdrWorkspaceAssingment.Property + "'" +
                                                  ", '" + AdrWorkspaceAssingment.Value + "'" +
                                                  ", '" + AdrWorkspaceAssingment.Created + "'" +
                                                  ", '" + AdrWorkspaceAssingment.Direction + "'" +
                                                  ");";
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
                strSql = "Select * FROM EdiClientWorkspaceValue WHERE Id = " + AdrWorkspaceAssingment.Id + " ";
                return strSql;
            }
        }
        /// <summary>
        ///             GET_
        /// </summary>
        public string sql_GetByProperties
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Select * FROM EdiClientWorkspaceValue " +
                                                " WHERE " +
                                                    "AdrId = " + AdrWorkspaceAssingment.AdrId +
                                                    " and WorkspaceId = " + AdrWorkspaceAssingment.WorkspaceId +
                                                    " and AsnArtId = " + AdrWorkspaceAssingment.AsnArtId +
                                                    " and Direction = 'OUT' ";
                if (!AdrWorkspaceAssingment.Property.Equals(string.Empty))
                {
                    strSql += " and Property = '" + AdrWorkspaceAssingment.Property + "' ";
                }

                return strSql;
            }
        }
        /// <summary>
        ///             GET
        /// </summary>
        public string sql_Get_List
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Select * FROM EdiClientWorkspaceValue; ";

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
                strSql = "DELETE EdiClientWorkspaceValue ";
                strSql += "WHERE ";
                strSql += "Id = " + AdrWorkspaceAssingment.Id + "; ";
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

                strSql = "Update EdiClientWorkspaceValue ";
                strSql += "SET ";
                strSql += "AdrId = " + AdrWorkspaceAssingment.AdrId + " ";
                strSql += ", WorkspaceId = " + AdrWorkspaceAssingment.WorkspaceId + " ";
                strSql += ", AsnArtId = " + AdrWorkspaceAssingment.AsnArtId + " ";
                strSql += ", Property = '" + AdrWorkspaceAssingment.Property + "' ";
                strSql += ", Value = '" + AdrWorkspaceAssingment.Value + "' ";
                strSql += ", Direction = '" + AdrWorkspaceAssingment.Direction + "' ";
                strSql += "WHERE ";
                strSql += "Id = " + AdrWorkspaceAssingment.Id + "; ";

                return strSql;
            }
        }


        ///***************************************************************************************************
        ///                             static
        ///***************************************************************************************************
        ///
        public static string GetSuppliertCode_NAD_CZ(int myAdrId, int myWorkspaceId, string myAsnArtTyp)
        {
            string strSql = string.Empty;
            string strSupplierNo = string.Empty;
            if (
                (myAdrId > 0) &&
                (myWorkspaceId > 0) &&
                (myAsnArtTyp.Length > 0)
               )
            {
                Globals._GL_USER gL_USER = new Globals._GL_USER();
                gL_USER.User_ID = 1;
                strSql = " SELECT Top (1) Value FROM EdiClientWorkspaceValue " +
                                                    " WHERE " +
                                                        " AdrId=" + myAdrId +
                                                        " AND WorkspaceId =" + myWorkspaceId +
                                                        " AND AsnArtId= (SELECT Id FROM ASNArt WHERE Typ='" + myAsnArtTyp + "'" +
                                                                            " AND Property='" + constValue_Edifact.const_Edifact_NAD_CZ + "' " +
                                                                        " ); ";
                strSupplierNo = clsSQLCOM.ExecuteSQL_GetValue(strSql, gL_USER.User_ID);
            }
            return strSupplierNo;
        }

    }
}

