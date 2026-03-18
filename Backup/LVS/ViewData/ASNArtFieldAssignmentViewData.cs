using LVS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace LVS.ViewData
{
    public class ASNArtFieldAssignmentViewData
    {


        public ASNArtFieldAssignment AsnFieldAssign { get; set; }
        private int BenutzerID { get; set; }

        public List<ASNArtFieldAssignment> ListASNArtFieldAssignment { get; set; }
        public Dictionary<string, ASNArtFieldAssignment> DictASNArtFieldAssignment { get; set; } = new Dictionary<string, ASNArtFieldAssignment>();
        public Dictionary<string, ASNArtFieldAssignment> DictASNArtFieldAssCopyFieldValue { get; set; } = new Dictionary<string, ASNArtFieldAssignment>();
        public ASNArtFieldAssignmentViewData()
        {
            InitCls();
        }

        public ASNArtFieldAssignmentViewData(ASNArtFieldAssignment myAsnFieldAssign) : this()
        {
            this.AsnFieldAssign = myAsnFieldAssign;
        }

        public ASNArtFieldAssignmentViewData(int myId, int myUserId, bool mybInclSub) : this()
        {
            AsnFieldAssign.Id = myId;
            BenutzerID = myUserId;
            if (AsnFieldAssign.Id > 0)
            {
                Fill(mybInclSub);
            }
        }

        public ASNArtFieldAssignmentViewData(int mySenderId, int myUserId, int myWorkspaceId, bool mybInclSub) : this()
        {
            AsnFieldAssign.Sender = mySenderId;
            AsnFieldAssign.WorkspaceId = myWorkspaceId;
            BenutzerID = myUserId;
            if (AsnFieldAssign.Sender > 0)
            {
                GetListBySenderAndWorkspace();
            }
        }

        public ASNArtFieldAssignmentViewData(int mySenderId, int myReceiver, int myUserId, int myWorkspaceId, bool mybInclSub) : this()
        {
            AsnFieldAssign.Sender = mySenderId;
            AsnFieldAssign.Receiver = myReceiver;
            BenutzerID = myUserId;
            AsnFieldAssign.WorkspaceId = myWorkspaceId;

            if (AsnFieldAssign.Sender > 0)
            {
                GetListBySenderAndWorkspace();
                GetListBySenderAndWorkspaceCopyFields();
            }
        }

        private void InitCls()
        {
            AsnFieldAssign = new ASNArtFieldAssignment();
            DictASNArtFieldAssignment = new Dictionary<string, ASNArtFieldAssignment>();
            ListASNArtFieldAssignment = new List<ASNArtFieldAssignment>();
        }

        public void Fill(bool mybInclSub)
        {
            string strSQL = sql_Get;
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "AsnArt");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr, mybInclSub);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void GetListBySenderAndWorkspace()
        {
            string strSql = string.Empty;
            if (this.AsnFieldAssign.Sender > 0)
            {
                strSql = sql_Get_ListBySender;
                if (this.AsnFieldAssign.WorkspaceId > 0)
                {
                    strSql += " AND AbBereichID = " + this.AsnFieldAssign.WorkspaceId;
                }
                DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "AsnArtFieldAssignment");
                if (dt.Rows.Count > 0)
                {
                    this.ListASNArtFieldAssignment = new List<ASNArtFieldAssignment>();
                    foreach (DataRow r in dt.Rows)
                    {
                        this.AsnFieldAssign = new ASNArtFieldAssignment();
                        SetValue(r, true);
                        if (!this.ListASNArtFieldAssignment.Contains(AsnFieldAssign)) // Verhindert Duplikate
                        {
                            this.ListASNArtFieldAssignment.Add(AsnFieldAssign.Copy());
                        }
                        if (DictASNArtFieldAssignment.ContainsKey(AsnFieldAssign.ASNField) == false)
                        {
                            DictASNArtFieldAssignment.Add(AsnFieldAssign.ASNField, AsnFieldAssign);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mySender"></param>
        /// <param name="myReceiver"></param>
        /// <param name="myWorkspaceId"></param>
        /// <returns></returns>
        public void GetListBySenderAndWorkspaceCopyFields()
        {
            DictASNArtFieldAssCopyFieldValue = new Dictionary<string, ASNArtFieldAssignment>();
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = sql_Get_ListBySender;
            if (this.AsnFieldAssign.WorkspaceId > 0)
            {
                strSql += " AND AbBereichID = " + this.AsnFieldAssign.WorkspaceId;
                strSql += " AND (CopyToField<>'' OR CopyToField IS NOT NULL);";
            }

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ASNArtFieldAssignment");
            decimal decTmp = 0;
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    ASNArtFieldAssignmentViewData asnArtFieldAssignView = new ASNArtFieldAssignmentViewData((int)decTmp, BenutzerID, false);
                    if (!DictASNArtFieldAssCopyFieldValue.Keys.Contains(asnArtFieldAssignView.AsnFieldAssign.ASNField))
                    {
                        DictASNArtFieldAssCopyFieldValue.Add(asnArtFieldAssignView.AsnFieldAssign.ASNField, asnArtFieldAssignView.AsnFieldAssign);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="mybInclSub"></param>
        private void SetValue(DataRow row, bool mybInclSub)
        {
            int iTmp = 0;
            int.TryParse(row["ID"].ToString(), out iTmp);
            AsnFieldAssign.Id = iTmp;
            iTmp = 0;
            int.TryParse(row["Sender"].ToString(), out iTmp);
            AsnFieldAssign.Sender = iTmp;
            iTmp = 0;
            int.TryParse(row["Receiver"].ToString(), out iTmp);
            AsnFieldAssign.Receiver = iTmp;
            AsnFieldAssign.ASNField = row.Field<string>("ASNField");
            AsnFieldAssign.ArtField = row.Field<string>("ArtField");
            AsnFieldAssign.IsDefValue = (bool)row["IsDefValue"];
            AsnFieldAssign.DefValue = row.Field<string>("DefValue");
            AsnFieldAssign.CopyToField = row.Field<string>("CopyToField");
            AsnFieldAssign.FormatFunction = row.Field<string>("FormatFunction");
            iTmp = 0;
            int.TryParse(row["AbBereichID"].ToString(), out iTmp);
            AsnFieldAssign.WorkspaceId = iTmp;
            AsnFieldAssign.IsGlobalFieldVar = (bool)row["IsGlobalFieldVar"];
            AsnFieldAssign.GlobalFieldVar = row.Field<string>("GlobalFieldVar");
            AsnFieldAssign.SubASNField = row.Field<string>("SubASNField");

            // Zusätzliche Verarbeitung, falls mybInclSub true ist
            if (mybInclSub)
            {
                // Beispiel: Weitere Eigenschaften oder Sub-Objekte setzen
                // Hier kannst du zusätzliche Logik hinzufügen, falls erforderlich
                if (AsnFieldAssign.Sender > 0)
                {
                    AddressViewData addressView = new AddressViewData(AsnFieldAssign.Sender, BenutzerID);
                    AsnFieldAssign.SenderAdress = addressView.Address;
                }
                if (AsnFieldAssign.Receiver > 0)
                {
                    AddressViewData addressView = new AddressViewData(AsnFieldAssign.Receiver, BenutzerID);
                    AsnFieldAssign.ReceiverAdress = addressView.Address;
                }
                if (AsnFieldAssign.WorkspaceId > 0)
                {
                    WorkspaceViewData workspaceViewData = new WorkspaceViewData(AsnFieldAssign.WorkspaceId);
                    AsnFieldAssign.Workspace = workspaceViewData.Workspace;
                }
            }
        }
        /// <summary>
        ///             ADD
        /// </summary>
        public void Add()
        {
            string strSQL = sql_Add;
            //strSQL = strSQL + " Select @@IDENTITY as 'ID' ";
            //string strTmp = clsSQLCOM.ExecuteSQL_GetValue(strSQL, BenutzerID);
            //decimal decTmp = 0;
            //decimal.TryParse(strTmp, out decTmp);
            //if (decTmp > 0)
            //{
            //    AsnArt.Id = decTmp;
            //}
        }
        /// <summary>
        ///             DELETE
        /// </summary>
        public void Delete()
        {
            string strSQL = sql_Delete;
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

        public bool AddbyImport(List<ASNArtFieldAssignment> myListImported)
        {
            bool bReturn = false;
            InitCls();
            this.ListASNArtFieldAssignment = myListImported;
            if (this.ListASNArtFieldAssignment != null && this.ListASNArtFieldAssignment.Count > 0)
            {
                List<string> listSqlStatements = new List<string>();
                foreach (ASNArtFieldAssignment assign in this.ListASNArtFieldAssignment)
                {
                    this.AsnFieldAssign = new ASNArtFieldAssignment();
                    this.AsnFieldAssign = assign.Copy();
                    listSqlStatements.Add(sql_Add);
                }
                // Execute all SQL statements in a transaction
                if (listSqlStatements.Count > 0)
                {
                    string strSql = string.Empty;
                    foreach (string sql in listSqlStatements)
                    {
                        strSql += sql + Environment.NewLine;
                    }

                    bReturn = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "AddASNArtFieldAssignment", this.BenutzerID);
                    //if (!bReturn)
                    //{
                    //    throw new Exception("Fehler beim Hinzufügen der ASNArtFieldAssignments.");
                    //}
                }
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
                string strSQL = string.Empty;
                //AsnArt.Datum = DateTime.Now;
                strSQL = "INSERT INTO ASNArtFieldAssignment(";
                strSQL += "[Sender]";
                strSQL += ",[Receiver] ";
                strSQL += ",[ASNField] ";
                strSQL += ",[ArtField] ";
                strSQL += ",[IsDefValue] ";
                strSQL += ",[DefValue] ";
                strSQL += ",[CopyToField] ";
                strSQL += ",[FormatFunction] ";
                strSQL += ",[AbBereichID] ";
                strSQL += ",[IsGlobalFieldVar] ";
                strSQL += ",[GlobalFieldVar] ";
                strSQL += ",[SubASNField] ";
                strSQL += ") ";

                strSQL += "VALUES (";

                strSQL += this.AsnFieldAssign.Sender;
                strSQL += ", " + this.AsnFieldAssign.Receiver;
                strSQL += ", '" + this.AsnFieldAssign.ASNField + "'";
                strSQL += ", '" + this.AsnFieldAssign.ArtField + "'";
                strSQL += ", " + Convert.ToInt32(this.AsnFieldAssign.IsDefValue);
                strSQL += ", '" + this.AsnFieldAssign.DefValue + "'";
                strSQL += ", '" + this.AsnFieldAssign.CopyToField + "'";
                strSQL += ", '" + this.AsnFieldAssign.FormatFunction + "'";
                strSQL += ", " + this.AsnFieldAssign.WorkspaceId;
                strSQL += ", " + +Convert.ToInt32(this.AsnFieldAssign.IsGlobalFieldVar);
                strSQL += ", '" + this.AsnFieldAssign.GlobalFieldVar + "'";
                strSQL += ", '" + this.AsnFieldAssign.SubASNField + "'";

                strSQL += "); ";

                return strSQL;
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
                strSql = "SELECT * FROM ASNArtFieldAssignment WHERE ID=" + this.AsnFieldAssign.Id + ";";
                return strSql;
            }
        }
        /// <summary>
        ///             Delete
        /// </summary>
        public string sql_Delete
        {
            get
            {
                string strSql = string.Empty;
                strSql = "DELETE ASNArtFieldAssignment WHERE ID=" + this.AsnFieldAssign.Id + ";";
                return strSql;
            }
        }


        /// <summary>
        ///             GET list
        /// </summary>
        public string sql_Get_ListBySender
        {
            get
            {
                string strSql = string.Empty;
                strSql = "SELECT * FROM ASNArtFieldAssignment WHERE Sender=" + this.AsnFieldAssign.Sender;
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
                strSql = "SELECT * FROM ASNArtFieldAssignment";
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
                strSql = "Update ASNArtFieldAssignment SET " +
                                    "Sender = " + (int)this.AsnFieldAssign.Sender +
                                    ", Receiver = " + (int)this.AsnFieldAssign.Receiver +
                                    ", ASNField = '" + this.AsnFieldAssign.ASNField + "'" +
                                    ", ArtField = '" + this.AsnFieldAssign.ArtField + "'" +
                                    ", IsDefValue = " + Convert.ToInt32(this.AsnFieldAssign.IsDefValue) +
                                    ", DefValue = '" + this.AsnFieldAssign.DefValue + "'" +
                                    ", CopyToField = '" + this.AsnFieldAssign.CopyToField + "'" +
                                    ", FormatFunction = '" + this.AsnFieldAssign.FormatFunction + "'" +
                                    ", AbBereichID = " + Convert.ToInt32(this.AsnFieldAssign.WorkspaceId) +
                                    ", IsGlobalFieldVar = " + Convert.ToInt32(this.AsnFieldAssign.IsGlobalFieldVar) +
                                    ", GlobalFieldVar = '" + this.AsnFieldAssign.GlobalFieldVar + "'" +
                                    ", SubASNField = '" + this.AsnFieldAssign.SubASNField + "'" +

                                    " WHERE ID = " + this.AsnFieldAssign.Id + "; ";
                return strSql;
            }
        }



        //-------------------------------------------------------------------------------------------------------
        //                                          static
        //-------------------------------------------------------------------------------------------------------
        public static bool InsertShapeByRefAdr(List<int> myListID, int myDestAdrId, decimal myUserId, decimal myAbBereichID)
        {
            bool bReturn = false;
            string strSql = string.Empty;
            strSql = " INSERT INTO ASNArtFieldAssignment ([Sender] ,[Receiver],[ASNField],[ArtField] ,[IsDefValue] " +
                                                          " ,[DefValue],[CopyToField] ,[FormatFunction] ,[AbBereichID]" +
                                                          " ,[IsGlobalFieldVar] ,[GlobalFieldVar], [SubASNField]) " +
                    "SELECT " +
                            myDestAdrId +
                            ", a.Receiver " +
                            ", a.ASNField " +
                            ", a.ArtField" +
                            ", a.IsDefValue " +
                            ", a.DefValue " +
                            ", a.CopyToField " +
                            ", a.FormatFunction " +
                            // ", a.AbBereichID " +
                            ", " + (int)myAbBereichID +
                            ", a.IsGlobalFieldVar " +
                            ", a.GlobalFieldVar " +
                            ", a.SubASNField " +

                                " FROM ASNArtFieldAssignment a " +
                                    "WHERE " +
                                    " a.ID in (" + string.Join(",", myListID.ToArray()) + ");";
            //"a.Sender =" + mySourceAdrId + "; ";
            bReturn = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "InsertASNArtFieldAssignment", myUserId);
            return bReturn;
        }

        /// <summary>
        ///         
        /// </summary>
        /// <param name="myGLUser"></param>
        /// <param name="mySenderAdrID"></param>
        /// <returns></returns>
        public static bool ExistbySender(Globals._GL_USER myGLUser, decimal mySenderAdrID)
        {
            string strSql = string.Empty;
            strSql = "SELECT ID FROM ASNArtFieldAssignment WHERE Sender=" + (int)mySenderAdrID + ";";
            bool bReturn = clsSQLcon.ExecuteSQL_GetValueBool(strSql, myGLUser.User_ID);
            return bReturn;
        }
        /// <summary>
        ///         
        /// </summary>
        /// <param name="myGLUser"></param>
        /// <param name="mySenderAdrID"></param>
        /// <returns></returns>
        public static DataTable GetASNArtFieldAssignmentsBySender(Globals._GL_USER myGLUser, decimal mySenderAdrID)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ASNArtFieldAssignment WHERE Sender=" + (int)mySenderAdrID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGLUser.User_ID, "ASNArtFieldAss");
            return dt;
        }

    }
}

