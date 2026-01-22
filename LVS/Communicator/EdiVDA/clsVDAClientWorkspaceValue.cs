using System;
using System.Collections.Generic;
using System.Data;

namespace LVS
{
    public class clsVDAClientWorkspaceValue
    {

        public clsSQLCOM SQLConIntern = new clsSQLCOM();
        public Globals._GL_SYSTEM GLSystem;
        public Globals._GL_USER GL_User;
        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = GL_User.User_ID;
                return _BenutzerID;
            }
            set { _BenutzerID = value; }
        }


        public decimal ID { get; set; }
        public decimal AdrID { get; set; }
        public decimal Receiver { get; set; }
        public decimal AbBereichID { get; set; }
        public decimal ASNFieldID { get; set; }
        public string Kennung { get; set; }
        public string Value { get; set; }
        public bool aktiv { get; set; }
        public bool IsFunction { get; set; }

        public Dictionary<decimal, clsVDAClientWorkspaceValue> DictVDAClientWorkspaceValue { get; set; }

        public DataTable dtVdaClientWorkspaceValue
        {
            get
            {
                DataTable dt = this.GetVdaClientWorkspaceValueByAdr();
                return dt;
            }
        }

        public clsADR ReceiverAdr
        {
            get
            {
                clsADR tmpAdr = new clsADR();
                tmpAdr.InitClass(this.GL_User, this.GLSystem, this.Receiver, true);
                return tmpAdr;
            }
        }
        /**********************************************************************************
         *                      Methoden / Procedure
         * *******************************************************************************/
        ///<summary>clsVDAClientConstValue / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUser, decimal myAdrID, decimal myReceiver, decimal myAbBereichID)
        {
            this.GL_User = myGLUser;
            this.AdrID = myAdrID;
            this.Receiver = myReceiver;
            this.AbBereichID = myAbBereichID;
            InitDictVDAClientValue();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myGLUser"></param>
        /// <param name="myAdrID"></param>
        /// <param name="myAbBereichID"></param>
        public void InitClass(Globals._GL_USER myGLUser, decimal myAdrID, decimal myAbBereichID)
        {
            this.GL_User = myGLUser;
            this.AdrID = myAdrID;
            this.Receiver = 0;
            this.AbBereichID = myAbBereichID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public clsVDAClientWorkspaceValue Copy()
        {
            return (clsVDAClientWorkspaceValue)this.MemberwiseClone();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetVdaClientWorkspaceValueByAdr()
        {
            DataTable dt = new DataTable("VDAClientWorkspaceValue");
            string strSQL = string.Empty;
            strSQL = "SELECT a.* " +
                                "FROM VDAClientWorkspaceValue a " +
                                "WHERE " +
                                    "a.AdrID=" + (Int32)this.AdrID +
                                    //" AND a.Receiver =" + (Int32)this.Receiver +
                                    //" AND a.AbBereichID=" + (Int32)this.AbBereichID +
                                    //" AND a.activ=1 " +
                                    " Order by a.ASNFieldID " +
                                    ";";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "VDAClientConstValue");
            return dt;
        }
        ///<summary>clsVDAClientConstValue / InitDictVDAClientValue</summary>
        ///<remarks></remarks>
        public void InitDictVDAClientValue()
        {
            DictVDAClientWorkspaceValue = new Dictionary<decimal, clsVDAClientWorkspaceValue>();

            DataTable dt = new DataTable("VDAClientConstValue");
            string strSQL = string.Empty;
            strSQL = "SELECT a.* " +
                                "FROM VDAClientWorkspaceValue a " +
                                "WHERE " +
                                    "a.AdrID=" + (Int32)this.AdrID +
                                    " AND a.Receiver =" + (Int32)this.Receiver +
                                    " AND a.AbBereichID=" + (Int32)this.AbBereichID +
                                    " AND a.activ=1 " +
                                    " Order by a.ASNFieldID " +
                                    ";";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "VDAClientConstValue");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                clsVDAClientWorkspaceValue tmp = new clsVDAClientWorkspaceValue();
                tmp.GL_User = this.GL_User;
                tmp.ID = (decimal)dt.Rows[i]["ID"];
                tmp.Fill();
                if (!DictVDAClientWorkspaceValue.ContainsKey(tmp.ASNFieldID))
                {
                    DictVDAClientWorkspaceValue.Add(tmp.ASNFieldID, tmp);
                }
            }
        }
        ///<summary>clsVDAClientConstValue / Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            DataTable dt = new DataTable("VDAClientValue");
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM VDAClientWorkspaceValue WHERE ID=" + this.ID + ";";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "VDAClientValue");
            FillClassByTable(dt);
        }
        ///<summary>clsVDAClientConstValue / FillClassByTable</summary>
        ///<remarks></remarks>
        private void FillClassByTable(DataTable dt)
        {
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                this.ID = (decimal)dt.Rows[i]["ID"];
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["AdrID"].ToString(), out decTmp);
                this.AdrID = decTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["Receiver"].ToString(), out decTmp);
                this.Receiver = decTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["AbBereichID"].ToString(), out decTmp);
                this.AbBereichID = decTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ASNFieldID"].ToString(), out decTmp);
                this.ASNFieldID = decTmp;
                this.Value = dt.Rows[i]["Value"].ToString();
                this.Kennung = dt.Rows[i]["Kennung"].ToString();
                this.aktiv = (bool)dt.Rows[i]["activ"];
                this.IsFunction = (bool)dt.Rows[i]["IsFunction"];
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            string strSQL = string.Empty;
            strSQL = "Update VDAClientWorkspaceValue SET " +
                                    " AdrID=" + (int)this.AdrID +
                                    ", Receiver=" + (int)this.Receiver +
                                    ", AbBereichID=" + (int)this.AbBereichID +
                                    ", ASNFieldID=" + (int)this.ASNFieldID +
                                    ", Kennung='" + this.Kennung + "'" +
                                    ", Value='" + this.Value + "'" +
                                    ", activ=" + Convert.ToInt32(this.aktiv) +
                                    ", IsFunction=" + Convert.ToInt32(this.IsFunction) +

                                    " Where ID=" + (int)this.ID;

            bool bRet = clsSQLCOM.ExecuteSQLWithTRANSACTION(strSQL, "update", this.BenutzerID);
            return bRet;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            string strSQL = string.Empty;
            strSQL = "DELETE FROM VDAClientWorkspaceValue WHERE ID=" + this.ID + ";";
            bool bRet = clsSQLCOM.ExecuteSQLWithTRANSACTION(strSQL, "update", this.BenutzerID);
            return bRet;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Add()
        {
            bool bReturn = false;
            string strSql = string.Empty;
            strSql = "INSERT INTO [VDAClientWorkspaceValue] ([AdrID],[Receiver],[AbBereichID] ,[ASNFieldID]" +
                                                            ",[Kennung],[Value],[activ],[IsFunction])" +
                    "VALUES (" +
                             (int)this.AdrID +
                             ", " + (int)this.Receiver +
                             ", " + (int)this.AbBereichID +
                             ", " + (int)this.ASNFieldID +
                             ", '" + this.Kennung + "'" +
                             ", '" + this.Value + "'" +
                             ", " + Convert.ToInt32(this.aktiv) +
                             ", " + Convert.ToInt32(this.IsFunction) +
                             ") ;";
            strSql = strSql + " Select @@IDENTITY as 'ID' ";
            string strTmp = clsSQLCOM.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                this.ID = decTmp;
                Fill();
                bReturn = true;
            }
            return bReturn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myGLUser"></param>
        /// <param name="myAdrId"></param>
        /// <returns></returns>
        public static DataTable GetVDAClientWorkspaceValueByAdrId(Globals._GL_USER myGLUser, decimal myAdrId)
        {
            DataTable dt = new DataTable("VDAClientValue");
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM VDAClientWorkspaceValue WHERE AdrID=" + (int)myAdrId + ";";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "VDAClientValue");
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myListID"></param>
        /// <param name="myAdrId"></param>
        /// <param name="myReceiver"></param>
        /// <param name="myUserId"></param>
        /// <param name="myAbBereichID"></param>
        /// <param name="myMandandtId"></param>
        /// <returns></returns>
        public static bool InsertShapeByRefAdr(List<int> myListID, int myAdrId, decimal myUserId)
        {
            bool bReturn = false;
            //Eintrag
            string strSql = string.Empty;
            strSql = "INSERT INTO VDAClientWorkspaceValue ([AdrID],[Receiver],[AbBereichID],[ASNFieldID] " +
                                                           ",[Kennung],[Value],[activ] ,[IsFunction]) " +
                            "SELECT " +
                                    myAdrId +
                                    ", v.Receiver" +
                                    ", v.AbBereichID" +
                                    ", v.ASNFieldID " +
                                    ", v.Kennung" +
                                    ", v.Value " +
                                    ", v.activ " +
                                    ", v.IsFunction " +

                                        " FROM VDAClientWorkspaceValue v " +
                                            "WHERE " +
                                            " v.ID in (" + string.Join(",", myListID.ToArray()) + ");";
            bReturn = clsSQLCOM.ExecuteSQLWithTRANSACTION(strSql, "InsertJobs", myUserId);
            return bReturn;
        }
    }
}
