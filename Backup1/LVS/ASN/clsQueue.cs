using System;
using System.Data;


namespace LVS
{
    public class clsQueueLVS
    {
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
        public string TableName { get; set; }
        public decimal TableID { get; set; }
        public DateTime Datum { get; set; }
        public decimal ASNTypID { get; set; }
        public bool activ { get; set; }
        public decimal ASNID { get; set; }
        public decimal AdrVerweisID { get; set; }
        public Int32 ASNAction { get; set; }     // Vorgang nicht die ID

        public bool bSqlOK { get; set; }

        /*****************************************************************************************
         *                                      Methoden 
         * **************************************************************************************/

        ///<summary>clsQueue / GetQueueActiv</summary>
        ///<remarks>Ermittelt die aktiven / passiven Prozesse</remarks>
        public DataTable GetQueue(bool bActiv)
        {
            string strSQL = string.Empty;
            strSQL = "Select a.*, b.Typ as ASNTyp " +
                                "From Queue a " +
                                "INNER JOIN ASNTyp b ON b.ID=a.ASNTypID ";

            if (bActiv)
            {
                strSQL = strSQL + " WHERE activ=1;";
            }
            else
            {
                strSQL = strSQL + " WHERE activ=0;";
            }
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "aktiveProzess");
            return dt;
        }
        ///<summary>clsQueue / Add</summary>
        ///<remarks></remarks>
        public void Add()
        {
            bSqlOK = false;
            string strSQL = "INSERT INTO QUEUE (TableName, TableID, Datum, ASNTypID, activ, ASNID, AdrVerweisID, ASNAction) " +
                                  "VALUES ('" + TableName + "' " +
                                         "," + TableID +
                                         ",'" + Datum + "'" +
                                         "," + ASNTypID +
                                         "," + Convert.ToInt32(activ) +
                                         "," + ASNID +
                                         ", " + AdrVerweisID +
                                         ", " + ASNAction +
                                         ")";
            strSQL = strSQL + "Select @@IDENTITY as 'ID' ";
            string strTmp = clsSQLCOM.ExecuteSQL_GetValue(strSQL, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                this.ID = decTmp;
                bSqlOK = true;
            }
        }
        ///<summary>clsQueue / Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            DataTable dt = new DataTable("Queue");
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM Queue WHERE ID=" + ID + ";";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Queue");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                this.ID = (decimal)dt.Rows[i]["ID"];
                this.TableName = dt.Rows[i]["TableName"].ToString();
                this.TableID = (decimal)dt.Rows[i]["TableID"];
                this.Datum = (DateTime)dt.Rows[i]["Datum"];
                this.ASNTypID = (decimal)dt.Rows[i]["ASNTypID"];
                this.activ = (bool)dt.Rows[i]["activ"];
                this.ASNID = (decimal)dt.Rows[i]["ASNID"];
                this.AdrVerweisID = (decimal)dt.Rows[i]["AdrVerweisID"];
                Int32 iTmp = 0;
                Int32.TryParse(dt.Rows[i]["ASNAction"].ToString(), out iTmp);
                this.ASNAction = iTmp;
            }
        }

    }
}
