using System;
using System.Data;


namespace LVS
{
    public class clsAuftragRead
    {

        public Globals._GL_USER _GL_User;
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

        public decimal ID { get; set; }
        public decimal AuftragPosID { get; set; }
        public decimal UserID { get; set; }
        public decimal IDAuftragScan { get; set; }

        /****************************************************************************
         *                  Methoden Artikel 
         * *************************************************************************/
        ///<summary>clsAuftragRead / Add</summary>
        ///<remarks></remarks>
        public void Add()
        {
            if (
              (AuftragPosID > 0)
              &
              (UserID > 0)
              )
            {
                InsertToDB();
            }
        }
        ///<summary>clsAuftragRead / InsertToDB</summary>
        ///<remarks></remarks>
        private void InsertToDB()
        {
            string strSQL = string.Empty;
            strSQL = "INSERT INTO AuftragRead (UserID, AuftragPosID, IDAuftragScan) " +
                                    "VALUES ('" + UserID + "','"
                                                + AuftragPosID + "','"
                                                + IDAuftragScan + "'); ";
            strSQL = strSQL + "Select @@IDENTITY as 'ID' ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, this.BenutzerID);
            Decimal decTmp = 0;
            Decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                this.ID = decTmp;
                this.Fill();
            }
        }
        ///<summary>clsAuftragRead / Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select * FROM AuftragRead WHERE ID=" + ID + ";";

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "AuftragRead");
            if (dt.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    decimal decTmp = 0;
                    Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                    this.ID = decTmp;
                    decTmp = 0;
                    Decimal.TryParse(dt.Rows[i]["UserID"].ToString(), out decTmp);
                    this.UserID = decTmp;
                    decTmp = 0;
                    Decimal.TryParse(dt.Rows[i]["AuftragPosID"].ToString(), out decTmp);
                    this.AuftragPosID = decTmp;
                    decTmp = 0;
                    Decimal.TryParse(dt.Rows[i]["IDAuftragScan"].ToString(), out decTmp);
                    this.IDAuftragScan = decTmp;
                }
            }
        }
        ///<summary>clsAuftragRead / DeleteReadAuftragByUser</summary>
        ///<remarks></remarks>
        public void DeleteReadAuftragByUser(decimal iUser)
        {
            if (iUser > 0)
            {
                UserID = iUser;
                string strSQL = string.Empty;
                strSQL = "DELETE FROM AuftragRead WHERE UserID='" + UserID + "'";
                clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
            }
        }
        ///<summary>clsAuftragRead / DeleteReadAuftragAuftragPosID</summary>
        ///<remarks>Löscht alle Datensätze einer Auftragsposition</remarks>
        public void DeleteReadAuftragAuftragPosID(decimal iAuftragPosID)
        {
            if (iAuftragPosID > 0)
            {
                AuftragPosID = iAuftragPosID;
                string strSQL = string.Empty;
                strSQL = "DELETE FROM AuftragRead WHERE AuftragPosID='" + AuftragPosID + "'";
                clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
            }
        }
        ///<summary>clsAuftragRead / DeleteReadAuftragAuftragTableID</summary>
        ///<remarks>Löscht alle Datensätze eines Auftrags</remarks>
        public void DeleteReadAuftragAuftragTableID(decimal myAuftragTableID)
        {
            //Ermitteln der AuftragPosID
            string strSQL = clsAuftragRead.GetSQLDeleteReadAuftragAuftragTableID(myAuftragTableID);
            clsSQLcon.ExecuteSQLWithTRANSACTION(strSQL, "AuftragRead löschen", this.BenutzerID);
        }
        ///<summary>clsAuftragRead / UserReadAuftrag</summary>
        ///<remarks></remarks>
        public bool UserReadAuftrag(decimal iUser, decimal iAuftragPosID)
        {
            bool retVal = false;
            if (
              ((iUser > 0))
              &
              ((iAuftragPosID > 0))
              )
            {
                UserID = iUser;
                AuftragPosID = iAuftragPosID;

                string strSQL = string.Empty;
                strSQL = "Select ID FROM AuftragRead WHERE UserID='" + UserID + "' AND AuftragPosID='" + AuftragPosID + "'";
                retVal = clsSQLcon.ExecuteSQL_GetValueBool(strSQL, BenutzerID);
            }
            return retVal;
        }
        ///<summary>clsAuftragRead / Update</summary>
        ///<remarks>Wird momentan nicht verwendet</remarks>
        public void Update()
        {
            try
            {
                string strSQL = string.Empty;
                strSQL = "Update AuftragRead SET " +
                                            "IDAuftragScan =" + IDAuftragScan +
                                            "WHERE " +
                                                    "UserID=" + UserID +
                                                    " AND AuftragPosID =" + AuftragPosID + ";";
                clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }
        /************************************************************************************************
         *                      public static
         * *********************************************************************************************/
        ///<summary>clsAuftragRead / DeleteReadAuftragAuftragTableID</summary>
        ///<remarks>SQL Anweisung zum Löschen alle Datensätze eines Auftrags</remarks>
        public static string GetSQLDeleteReadAuftragAuftragTableID(decimal myAuftragTableID)
        {
            string strSQL = string.Empty;
            strSQL = "DELETE FROM AuftragRead WHERE AuftragPosID IN ( Select ID FROM AuftragPos WHERE AuftragTableID=" + myAuftragTableID + ");";
            return strSQL;
        }
        ///<summary>clsAuftragRead / UserReadDoc</summary>
        ///<remarks></remarks>
        public static bool UserReadDoc(Globals._GL_USER myGLUser, decimal IDDocScan)
        {
            bool retVal = false;
            string strSQL = string.Empty;
            strSQL = "Select AuftragPosID FROM AuftragRead WHERE IDAuftragScan=" + IDDocScan + " " +
                                                                "AND UserID=" + myGLUser.User_ID + ";";
            retVal = clsSQLcon.ExecuteSQL_GetValueBool(strSQL, myGLUser.User_ID);
            return retVal;
        }
        ///<summary>clsAuftragRead / UserReadDoc</summary>
        ///<remarks>Erweitert um die PosID</remarks>
        public static bool UserReadAuftragPosDoc(Globals._GL_USER myGLUser, decimal IDDocScan, decimal iPosID)
        {
            bool retVal = false;
            string strSQL = string.Empty;
            strSQL = "Select ID FROM AuftragRead WHERE IDAuftragScan=" + IDDocScan + " AND " +
                                                                        "UserID=" + myGLUser.User_ID + " AND " +
                                                                        "AuftragPosID=" + iPosID + " ;";
            retVal = clsSQLcon.ExecuteSQL_GetValueBool(strSQL, myGLUser.User_ID);
            return retVal;
        }

    }
}
