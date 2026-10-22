using LVS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class ReiheGutAssignmentViewData
    {
        public Globals._GL_USER _GL_User;
        public ReiheGutAssignment ReiheGutAssignment;
        public List<ReiheGutAssignment> ListReiheGutAssignment;

        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = _GL_User.User_ID;
                return _BenutzerID;
            }
            set { _BenutzerID = value; }
        }
        public ReiheGutAssignmentViewData()
        {
            InitCls();
        }
        public ReiheGutAssignmentViewData(int myId) : this()
        {
            if (myId > 0)
            {
                ReiheGutAssignment.Id = myId;
                FillbyId();
            }
        }
        public ReiheGutAssignmentViewData(int myId, int myReiheId) : this()
        {
            if(myId>0)
            {
                ReiheGutAssignment.Id = myId;
                FillbyId();
            }

            if (myReiheId>0)
            {
                ReiheGutAssignment.ReiheId = myReiheId;
                FillbyReiheId();
            }
        }
 
        /// <summary>
        /// 
        /// </summary>
        private void InitCls()
        {
            ReiheGutAssignment = new ReiheGutAssignment();
            ListReiheGutAssignment = new List<ReiheGutAssignment>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mybInclSub"></param>
        public bool FillbyId()
        {
            if (ExistReiheGutAssignment())
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = sql_Get_Main + " WHERE Id = " + ReiheGutAssignment.Id;

                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ReiheGutAssignment");
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                }

                return (dt.Rows.Count > 0);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mybInclSub"></param>
        public bool FillbyReiheId()
        {
            ListReiheGutAssignment= new List<ReiheGutAssignment>();
            if (ExistReiheGutAssignment())
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = sql_Get_Main + " WHERE ReiheId = " + ReiheGutAssignment.ReiheId;

                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ReiheGutAssignment");
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                    if(ListReiheGutAssignment.Contains(ReiheGutAssignment)==false)
                    {
                        ListReiheGutAssignment.Add(ReiheGutAssignment);
                    }
                }
                return (dt.Rows.Count > 0);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        public void SetValue(DataRow row)
        {
            ReiheGutAssignment = new ReiheGutAssignment();

            int iTmp = 0;
            int.TryParse(row["Id"].ToString(), out iTmp);
            ReiheGutAssignment.Id = iTmp;

            iTmp = 0;
            int.TryParse(row["ReiheId"].ToString(), out iTmp);
            ReiheGutAssignment.ReiheId = iTmp;

            iTmp = 0;
            int.TryParse(row["GArtId"].ToString(), out iTmp);
            ReiheGutAssignment.GArtId = iTmp;
            ReiheGutAssignment.Gut = new Common.Models.Goodstypes();
            if (ReiheGutAssignment.GArtId>0)
            {
                GoodstypeViewData myGoodstypeViewData = new GoodstypeViewData((int)ReiheGutAssignment.GArtId);
                ReiheGutAssignment.Gut = myGoodstypeViewData.Gut;
            }
        }

        /// <summary>
        ///             ADD
        /// </summary>
        public void Add()
        {
            if (ReiheGutAssignment.ReiheId > 0 && ReiheGutAssignment.GArtId > 0)
            {
                string strSql = string.Empty;
                strSql = this.sql_Add;

                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
                Int32 iTmp = 0;
                Int32.TryParse(strTmp, out iTmp);
                ReiheGutAssignment.Id = iTmp;

                //Add Logbucheintrag
                string strInfo = "Reihe-Güterart-Zuweisung erstellt: ReiheId [" + ReiheGutAssignment.ReiheId.ToString() + "] / GArtId: " + ReiheGutAssignment.GArtId.ToString();
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), strInfo);
            }
        }
        /// <summary>
        ///             DELETE
        /// </summary>
        public bool Delete()
        {
            if (ExistReiheGutAssignment())
            {
                string strSql = string.Empty;
                strSql = "DELETE FROM ReiheGutAssignment WHERE Id = " + ReiheGutAssignment.Id;

                bool bReturn = clsSQLcon.ExecuteSQL(strSql, BenutzerID);

                //Add Logbucheintrag
                string strInfo = "Reihe-Güterart-Zuweisung gelöscht: Id [" + ReiheGutAssignment.Id.ToString() + "] / ReiheId: " + ReiheGutAssignment.ReiheId.ToString() + " / GArtId: " + ReiheGutAssignment.GArtId.ToString();
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), strInfo);

                return bReturn;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        ///             UPDATE
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            string strSql = sql_Update;
            bool retVal = clsSQLCOM.ExecuteSQL(strSql, BenutzerID);
            return retVal;
        }
        ///<summary>clsReiheGutAssignment / ExistReiheGutAssignment</summary>
        ///<remarks>Prüft ob der Datensatz mit der Id existiert.</remarks>
        ///<returns>Returns BOOL</returns>
        private bool ExistReiheGutAssignment()
        {
            if (this.ReiheGutAssignment.Id > 0)
            {
                string strSql = string.Empty;
                strSql = "SELECT * FROM ReiheGutAssignment WHERE Id = " + this.ReiheGutAssignment.Id;
                return clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsReiheGutAssignment / ExistByReiheAndGArt</summary>
        ///<remarks>Prüft ob eine Zuweisung für Reihe und Güterart existiert.</remarks>
        ///<returns>Returns BOOL</returns>
        public bool ExistByReihe()
        {
            if (this.ReiheGutAssignment.ReiheId > 0)
            {
                string strSql = string.Empty;
                strSql = sql_Get_Main+ " WHERE ReiheId = " + this.ReiheGutAssignment.ReiheId;
                return clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsReiheGutAssignment / GetAssignmentsByReiheId</summary>
        ///<remarks>Lädt alle Zuweisungen für eine Reihe.</remarks>
        public DataTable GetAssignmentsByReiheId()
        {
            DataTable dt = new DataTable();
            if (this.ReiheGutAssignment.ReiheId > 0)
            {
                string strSql = string.Empty;
                strSql = "SELECT * FROM ReiheGutAssignment " +
                         "WHERE ReiheId = " + this.ReiheGutAssignment.ReiheId;

                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ReiheGutAssignment");
            }
            return dt;
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
                string strSql = "INSERT INTO ReiheGutAssignment (ReiheId, GArtId) " +
                                     "VALUES (" +
                                                this.ReiheGutAssignment.ReiheId + ", " +
                                                this.ReiheGutAssignment.GArtId + 
                                             ")";
                strSql = strSql + "Select @@IDENTITY as 'Id' ";
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
                strSql = "SELECT * FROM ReiheGutAssignment WHERE Id=" + this.ReiheGutAssignment.Id + " ";
                return strSql;
            }
        }

        /// <summary>
        ///             GET List
        /// </summary>
        public string sql_GetList
        {
            get
            {
                string strSql = string.Empty;
                strSql = this.sql_Get_Main;
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
                strSql = "SELECT * FROM ReiheGutAssignment";
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
                strSql = "DELETE FROM ReiheGutAssignment WHERE Id = " + this.ReiheGutAssignment.Id;
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
                strSql = "Update ReiheGutAssignment SET " +
                                            " [ReiheId]  = " + this.ReiheGutAssignment.ReiheId +
                                            ", [GArtId]  = " + this.ReiheGutAssignment.GArtId +
                                            "WHERE Id=" + this.ReiheGutAssignment.Id;
                return strSql;
            }
        }

        //------------------------------------------------------------------------------------------------
        ///<summary>clsReiheGutAssignment / ExistByReiheAndGArt</summary>
        ///<remarks>Prüft ob eine Zuweisung für Reihe und Güterart existiert.</remarks>
        ///<returns>Returns BOOL</returns>
        public static bool ExistByReiheAndGArt(int myReiheId, int GutId, int myUserId)
        {
            if (myReiheId > 0 && GutId > 0)
            {
                string strSql = string.Empty;
                strSql = "SELECT * FROM ReiheGutAssignment WHERE ReiheId = " + myReiheId + " AND GArtId = " + GutId;
                return clsSQLcon.ExecuteSQL_GetValueBool(strSql, myUserId);
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsReiheGutAssignment / ExistByReiheAndGArt</summary>
        ///<remarks>Prüft ob eine Zuweisung für Reihe und Güterart existiert.</remarks>
        ///<returns>Returns BOOL</returns>
        public static bool ExistByReiheGutAssignmentById(int myId, int myUserId)
        {
            if (myId > 0)
            {
                string strSql = string.Empty;
                strSql = "SELECT * FROM ReiheGutAssignment WHERE Id = " + myId ;
                return clsSQLcon.ExecuteSQL_GetValueBool(strSql, myUserId);
            }
            else
            {
                return false;
            }
        }
    }
}

