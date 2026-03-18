using System;
using System.Collections.Generic;
using System.Data;

namespace LVS
{
    public class clsMailingListCombiAdr
    {
        public Globals._GL_USER GL_User { get; set; }

        private int id;

        public int ID
        {
            get { return id; }
        }

        private int mailingListId;

        public int MailingListId
        {
            get { return mailingListId; }
            set { mailingListId = value; }
        }

        private int adrId;

        public int AdrId
        {
            get { return adrId; }
            set { adrId = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string KundenViewID
        {
            get
            {
                return clsADR.GetMatchCodeByID(adrId, this.GL_User.User_ID);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myMailingList"></param>
        /// <returns></returns>
        public static List<clsMailingListCombiAdr> GetAll(clsMailingList myMailingList)
        {
            string sql = "select * from MailingListCombiAdr where MailingListID=" + myMailingList.ID;
            DataTable dtTmp = clsSQLcon.ExecuteSQL_GetDataTable(sql, myMailingList._GL_User.User_ID, "Table");
            List<clsMailingListCombiAdr> returnList = new List<clsMailingListCombiAdr>();
            foreach (DataRow row in dtTmp.Rows)
            {
                clsMailingListCombiAdr mlca = new clsMailingListCombiAdr();


                mlca.GL_User = myMailingList._GL_User;
                Int32 decTmp = 0;
                Int32.TryParse(row["ID"].ToString(), out decTmp);
                mlca.id = decTmp;
                decTmp = 0;
                Int32.TryParse(row["MailingListID"].ToString(), out decTmp);
                mlca.MailingListId = decTmp;
                decTmp = 0;
                Int32.TryParse(row["AdrID"].ToString(), out decTmp);
                mlca.AdrId = decTmp;

                returnList.Add(mlca);
            }
            return returnList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="adrID"></param>
        /// <param name="mlID"></param>
        /// <param name="myGLUser"></param>
        /// <returns></returns>
        public static clsMailingListCombiAdr GetSingle(int adrID, int mlID, Globals._GL_USER myGLUser)
        {
            string sql = "select top(1) * from MailingListCombiAdr where AdrID=" + adrID + " and MailingListID=" + mlID;

            clsMailingListCombiAdr returnValue = null;
            DataTable dtTmp = clsSQLcon.ExecuteSQL_GetDataTable(sql, myGLUser.User_ID, "TableName");

            if (dtTmp.Rows.Count == 1)
            {
                returnValue = new clsMailingListCombiAdr();
                returnValue.GL_User = myGLUser;
                Int32 decTmp = 0;
                Int32.TryParse(dtTmp.Rows[0]["ID"].ToString(), out decTmp);
                returnValue.id = decTmp;
                decTmp = 0;
                Int32.TryParse(dtTmp.Rows[0]["MailingListID"].ToString(), out decTmp);
                returnValue.MailingListId = decTmp;
                decTmp = 0;
                Int32.TryParse(dtTmp.Rows[0]["AdrID"].ToString(), out decTmp);
                returnValue.AdrId = decTmp;

            }
            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clsMailingListCombiAdr"></param>
        /// <returns></returns>
        public static bool Remove(clsMailingListCombiAdr clsMailingListCombiAdr)
        {
            string sql = "delete from MailingListCombiAdr where ID=" + clsMailingListCombiAdr.ID;
            return clsSQLcon.ExecuteSQL(sql, clsMailingListCombiAdr.GL_User.User_ID);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            if (clsMailingListCombiAdr.GetSingle(AdrId, MailingListId, GL_User) == null)
            {
                string sql = "insert into MailingListCombiAdr (AdrID,MailingListID) VALUES (" + this.AdrId + "," + this.MailingListId + ")";
                return clsSQLcon.ExecuteSQL(sql, this.GL_User.User_ID);
            }
            return false;
        }
    }
}
