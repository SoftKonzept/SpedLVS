using System;
using System.Data;
namespace LVS
{
    public class clsOrga
    {
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
        public bool activ { get; set; }
        public Int32 SendNrOld { get; set; }
        public Int32 SendNrNew { get; set; }


        public DataTable dtOrgaByAdress
        {
            get
            {
                return GetOrgaListbyAdr();
            }
        }
        /*********************************************************************
         *                  Methoden / Procedure
         * *******************************************************************/
        ///<summary>clsOrga / Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            DataTable dt = new DataTable("Orga");
            string strSql = string.Empty;
            strSql = "SELECT * FROM Orga WHERE ID=" + ID + ";";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Orga");
            SetClassValue(dt);
        }
        /// <summary>
        /// 
        /// </summary>
        public void FillByAdrID()
        {
            SetClassValue(dtOrgaByAdress);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetOrgaListbyAdr()
        {
            string strSql = string.Empty;
            strSql = "SELECT * FROM Orga WHERE AdrID=" + AdrID + ";";
            DataTable dtReturn = clsSQLCOM.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Orga");
            return dtReturn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        private void SetClassValue(DataTable dt)
        {
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                this.ID = decTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["AdrID"].ToString(), out decTmp);
                this.AdrID = decTmp;
                this.activ = (bool)dt.Rows[i]["activ"];
                Int32 iTmp = 0;
                Int32.TryParse(dt.Rows[i]["SendNrOld"].ToString(), out iTmp);
                this.SendNrOld = iTmp;
                iTmp = 0;
                Int32.TryParse(dt.Rows[i]["SendNrNew"].ToString(), out iTmp);
                this.SendNrNew = iTmp;
            }
        }
        ///<summary>clsOrga /() UpdateSendNr</summary>
        ///<remarks></remarks>
        public void UpdateSendNr()
        {
            string strSql = string.Empty;
            strSql = "Update Orga " +
                            "SET SendNrOld=SendNrOld+1 " +
                            ", SendNrNew=SendNrNew+1 " +
                            "WHERE ID = " + this.ID + ";";
            bool bok = clsSQLCOM.ExecuteSQL(strSql, BenutzerID);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Update()
        {
            string strSql = string.Empty;
            strSql = "Update Orga SET " +
                            " AdrID = " + this.AdrID +
                            ", activ = " + Convert.ToInt32(this.activ) +
                            ", SendNrOld = " + this.SendNrOld +
                            ", SendNrNew = " + this.SendNrNew +
                            "WHERE ID = " + this.ID + ";";
            bool bok = clsSQLCOM.ExecuteSQL(strSql, BenutzerID);
            return bok;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Add()
        {
            bool bOK = false;
            string strSQL = string.Empty;
            strSQL = "INSERT INTO Orga ([AdrID],[activ],[SendNrOld],[SendNrNew]) " +
                                         "VALUES (" + this.AdrID +
                                                     ", " + Convert.ToInt32(this.activ) +
                                                     ", " + this.SendNrOld +
                                                     ", " + this.SendNrNew +
                                                     ");";
            strSQL = strSQL + " Select @@IDENTITY as 'ID'; ";
            try
            {
                bOK = clsSQLcon.ExecuteSQL(strSQL, this.BenutzerID);
                string strTmp = clsSQLCOM.ExecuteSQL_GetValue(strSQL, this.BenutzerID);
                decimal decTmp = 0;
                if (decimal.TryParse(strTmp, out decTmp))
                {
                    this.ID = decTmp;
                    this.Fill();
                }
            }
            catch (Exception ex)
            {
                //this.InfoText = "Der neue Datensatz konnte nicht eingetragen werden. Fehlercode [" + clsErrorCode.const_ErrorCode_200 + "]";
            }
            return bOK;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            string strSql = string.Empty;
            strSql = "DELETE Orga WHERE ID = " + this.ID + ";";
            bool bok = clsSQLCOM.ExecuteSQL(strSql, BenutzerID);
            return bok;
        }
    }
}
