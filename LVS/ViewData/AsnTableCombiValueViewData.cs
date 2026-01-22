using LVS.Models;
using System;
using System.Collections.Generic;
using System.Data;


namespace LVS.ViewData
{
    public class AsnTableCombiValueViewData
    {


        public AsnTableCombiValues asnTabelCombiValue { get; set; }
        private int BenutzerID { get; set; }

        public List<AsnTableCombiValues> ListAsnTableCombiValues { get; set; }
        public List<string> ListColsForCombination = new List<string>();
        public Dictionary<string, AsnTableCombiValues> DictAsnTableCombiValues { get; set; }

        public AsnTableCombiValueViewData()
        {
            InitCls();
        }

        public AsnTableCombiValueViewData(AsnTableCombiValues myAsnTabelCombiValue) : this()
        {
            this.asnTabelCombiValue = myAsnTabelCombiValue;
            if (asnTabelCombiValue.Id > 0)
            {
                FillListColsForCombination();
            }
        }

        public AsnTableCombiValueViewData(int myId, int myUserId, bool mybInclSub) : this()
        {
            asnTabelCombiValue.Id = myId;
            BenutzerID = myUserId;
            if (asnTabelCombiValue.Id > 0)
            {
                Fill(mybInclSub);
            }
        }

        public AsnTableCombiValueViewData(int mySenderId, int myReceiver, int myWorkspaceId, bool mybInclSub) : this()
        {
            asnTabelCombiValue.Sender = mySenderId;
            asnTabelCombiValue.WorkspaceId = myWorkspaceId;
            asnTabelCombiValue.Receiver = myReceiver;

            //BenutzerID = myUserId;
            if ((asnTabelCombiValue.Sender > 0) && (asnTabelCombiValue.WorkspaceId > 0) && (asnTabelCombiValue.Receiver > 0))
            {
                GetListBySenderReceiverWorkspace();
            }
        }
        private void InitCls()
        {
            asnTabelCombiValue = new AsnTableCombiValues();
            ListAsnTableCombiValues = new List<AsnTableCombiValues>();
            DictAsnTableCombiValues = new Dictionary<string, AsnTableCombiValues>();
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
        public void GetListBySenderReceiverWorkspace()
        {
            string strSql = string.Empty;
            if ((asnTabelCombiValue.Sender > 0) && (asnTabelCombiValue.Receiver > 0))
            {
                strSql = sql_Get_ListBySenderReceiver;
                if (asnTabelCombiValue.WorkspaceId > 0)
                {
                    strSql += " AND AbBereichID = " + asnTabelCombiValue.WorkspaceId;
                }
                DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "asnTabelCombiValue");
                if (dt.Rows.Count > 0)
                {
                    this.ListAsnTableCombiValues = new List<AsnTableCombiValues>();
                    foreach (DataRow r in dt.Rows)
                    {
                        this.asnTabelCombiValue = new AsnTableCombiValues();
                        SetValue(r, true);
                        if (!this.ListAsnTableCombiValues.Contains(asnTabelCombiValue)) // Verhindert Duplikate
                        {
                            this.ListAsnTableCombiValues.Add(asnTabelCombiValue);
                        }
                        if (DictAsnTableCombiValues.ContainsKey(asnTabelCombiValue.ColValue) == false)
                        {
                            DictAsnTableCombiValues.Add(asnTabelCombiValue.ColValue, asnTabelCombiValue);
                        }
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
            asnTabelCombiValue.Id = iTmp;
            iTmp = 0;
            int.TryParse(row["AbBereichID"].ToString(), out iTmp);
            asnTabelCombiValue.WorkspaceId = iTmp;
            iTmp = 0;
            int.TryParse(row["Sender"].ToString(), out iTmp);
            asnTabelCombiValue.Sender = iTmp;
            iTmp = 0;
            int.TryParse(row["Receiver"].ToString(), out iTmp);
            asnTabelCombiValue.Receiver = iTmp;
            asnTabelCombiValue.TableName = row["TableName"].ToString();
            asnTabelCombiValue.ColValue = row["ColValue"].ToString();
            asnTabelCombiValue.ColsForCombination = row["ColsForCombination"].ToString();
            asnTabelCombiValue.UseValueSeparator = Convert.ToBoolean(row["UseValueSeparator"]);
            asnTabelCombiValue.ValueSeparator = row["ValueSeparator"].ToString();

            // Zusätzliche Verarbeitung, falls mybInclSub true ist
            if (mybInclSub)
            {
                // Beispiel: Weitere Eigenschaften oder Sub-Objekte setzen
                // Hier kannst du zusätzliche Logik hinzufügen, falls erforderlich
                //if (AsnFieldAssign.Sender > 0)
                //{
                //    AddressViewData addressView = new AddressViewData(AsnFieldAssign.Sender, BenutzerID);
                //    AsnFieldAssign.SenderAdress = addressView.Address;
                //}
                //if (AsnFieldAssign.Receiver > 0)
                //{
                //    AddressViewData addressView = new AddressViewData(AsnFieldAssign.Receiver, BenutzerID);
                //    AsnFieldAssign.ReceiverAdress = addressView.Address;
                //}
                //if (AsnFieldAssign.WorkspaceId > 0)
                //{
                //    WorkspaceViewData workspaceViewData = new WorkspaceViewData(AsnFieldAssign.WorkspaceId);
                //    AsnFieldAssign.Workspace = workspaceViewData.Workspace;
                //}
            }
            //List mit ColForCombination
            FillListColsForCombination();
        }
        ///<summary>clsASNTableCombiValue / FillListColsForCombination</summary>
        ///<remarks>extrahiert die einzelnen Spalten aus dem String</remarks>>
        private void FillListColsForCombination()
        {
            this.ListColsForCombination = new List<string>();
            string ColString = asnTabelCombiValue.ColsForCombination;
            while (ColString.Length > 0)
            {
                string strTmp = ColString;
                string strCol = string.Empty;
                Int32 iStringLength = strTmp.Length;
                Int32 iPosPlaceHolder = 0;
                iPosPlaceHolder = strTmp.IndexOf(clsASNTableCombiValue.const_PlaceHolder);
                if (iPosPlaceHolder > 0)
                {
                    iPosPlaceHolder = strTmp.IndexOf(clsASNTableCombiValue.const_PlaceHolder);
                    strCol = strTmp.Substring(0, iPosPlaceHolder);
                }
                else
                {
                    strCol = strTmp;
                }
                this.ListColsForCombination.Add(strCol);
                iPosPlaceHolder++;
                try
                {
                    if (strCol != strTmp)
                    {
                        ColString = strTmp.Substring(iPosPlaceHolder, iStringLength - iPosPlaceHolder);
                    }
                    else
                    {
                        ColString = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    ColString = string.Empty;
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

        public bool AddbyImport(List<AsnTableCombiValues> myListImported)
        {
            bool bReturn = false;
            InitCls();
            //this.ListASNArtFieldAssignment = myListImported;
            //if (this.ListASNArtFieldAssignment != null && this.ListASNArtFieldAssignment.Count > 0)
            //{
            //    List<string> listSqlStatements = new List<string>();
            //    foreach (ASNArtFieldAssignment assign in this.ListASNArtFieldAssignment)
            //    {
            //        this.AsnFieldAssign = new ASNArtFieldAssignment();
            //        this.AsnFieldAssign = assign.Copy();
            //        listSqlStatements.Add(sql_Add);                    
            //    }
            //    // Execute all SQL statements in a transaction
            //    if(listSqlStatements.Count > 0)
            //    {
            //        string strSql = string.Empty;
            //        foreach (string sql in listSqlStatements)
            //        {
            //            strSql += sql + Environment.NewLine;
            //        }   

            //        bReturn = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "AddASNArtFieldAssignment", this.BenutzerID);
            //        //if (!bReturn)
            //        //{
            //        //    throw new Exception("Fehler beim Hinzufügen der ASNArtFieldAssignments.");
            //        //}
            //    }   
            //}
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
                //strSQL = "INSERT INTO ASNArtFieldAssignment(";
                //strSQL += "[Sender]";
                //strSQL += ",[Receiver] ";
                //strSQL += ",[ASNField] ";
                //strSQL += ",[ArtField] ";
                //strSQL += ",[IsDefValue] ";
                //strSQL += ",[DefValue] ";
                //strSQL += ",[CopyToField] ";
                //strSQL += ",[FormatFunction] ";
                //strSQL += ",[AbBereichID] ";
                //strSQL += ",[IsGlobalFieldVar] ";
                //strSQL += ",[GlobalFieldVar] ";
                //strSQL += ",[SubASNField] ";
                //strSQL += ") ";

                //strSQL += "VALUES (";

                //strSQL += this.AsnFieldAssign.Sender;
                //strSQL += ", " + this.AsnFieldAssign.Receiver;
                //strSQL += ", '" + this.AsnFieldAssign.ASNField + "'";
                //strSQL += ", '" + this.AsnFieldAssign.ArtField + "'";
                //strSQL += ", " + Convert.ToInt32(this.AsnFieldAssign.IsDefValue);
                //strSQL += ", '" + this.AsnFieldAssign.DefValue + "'";
                //strSQL += ", '" + this.AsnFieldAssign.CopyToField + "'";
                //strSQL += ", '" + this.AsnFieldAssign.FormatFunction + "'";
                //strSQL += ", " + this.AsnFieldAssign.WorkspaceId;
                //strSQL += ", " + +Convert.ToInt32(this.AsnFieldAssign.IsGlobalFieldVar);
                //strSQL += ", '" + this.AsnFieldAssign.GlobalFieldVar + "'";
                //strSQL += ", '" + this.AsnFieldAssign.SubASNField + "'";

                //strSQL += "); ";

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
                strSql = "SELECT * FROM ASNTableCombiValue WHERE ID=" + asnTabelCombiValue.Id;
                return strSql;
            }
        }
        /// <summary>
        ///             GET list
        /// </summary>
        public string sql_Get_ListBySenderReceiver
        {
            get
            {
                string strSql = string.Empty;
                strSql = "SELECT * FROM ASNTableCombiValue " +
                                        "WHERE " +
                                            "Sender=" + asnTabelCombiValue.Sender +
                                            " AND Receiver=" + asnTabelCombiValue.Receiver;
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
                //strSql = "DELETE ASNArtFieldAssignment WHERE ID=" + this.AsnFieldAssign.Id + ";";
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
                strSql = "SELECT * FROM ASNTableCombiValue ";
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
                //strSql = "Update ASNArtFieldAssignment SET " +
                //                    "Sender = " + (int)this.AsnFieldAssign.Sender +
                //                    ", Receiver = " + (int)this.AsnFieldAssign.Receiver +
                //                    ", ASNField = '" + this.AsnFieldAssign.ASNField + "'" +
                //                    ", ArtField = '" + this.AsnFieldAssign.ArtField + "'" +
                //                    ", IsDefValue = " + Convert.ToInt32(this.AsnFieldAssign.IsDefValue) +
                //                    ", DefValue = '" + this.AsnFieldAssign.DefValue + "'" +
                //                    ", CopyToField = '" + this.AsnFieldAssign.CopyToField + "'" +
                //                    ", FormatFunction = '" + this.AsnFieldAssign.FormatFunction + "'" +
                //                    ", AbBereichID = " + Convert.ToInt32(this.AsnFieldAssign.WorkspaceId) +
                //                    ", IsGlobalFieldVar = " + Convert.ToInt32(this.AsnFieldAssign.IsGlobalFieldVar) +
                //                    ", GlobalFieldVar = '" + this.AsnFieldAssign.GlobalFieldVar + "'" +
                //                    ", SubASNField = '" + this.AsnFieldAssign.SubASNField + "'" +

                //                    " WHERE ID = " + this.AsnFieldAssign.Id + "; ";
                return strSql;
            }
        }



        //-------------------------------------------------------------------------------------------------------
        //                                          static
        //-------------------------------------------------------------------------------------------------------
        //public static bool InsertShapeByRefAdr(List<int> myListID, int myDestAdrId, decimal myUserId, decimal myAbBereichID)
        //{
        //    bool bReturn = false;
        //    string strSql = string.Empty;
        //    strSql = " INSERT INTO ASNArtFieldAssignment ([Sender] ,[Receiver],[ASNField],[ArtField] ,[IsDefValue] " +
        //                                                  " ,[DefValue],[CopyToField] ,[FormatFunction] ,[AbBereichID]" +
        //                                                  " ,[IsGlobalFieldVar] ,[GlobalFieldVar], [SubASNField]) " +
        //            "SELECT " +
        //                    myDestAdrId +
        //                    ", a.Receiver " +
        //                    ", a.ASNField " +
        //                    ", a.ArtField" +
        //                    ", a.IsDefValue " +
        //                    ", a.DefValue " +
        //                    ", a.CopyToField " +
        //                    ", a.FormatFunction " +
        //                    // ", a.AbBereichID " +
        //                    ", " + (int)myAbBereichID +
        //                    ", a.IsGlobalFieldVar " +
        //                    ", a.GlobalFieldVar " +
        //                    ", a.SubASNField " +

        //                        " FROM ASNArtFieldAssignment a " +
        //                            "WHERE " +
        //                            " a.ID in (" + string.Join(",", myListID.ToArray()) + ");";
        //    //"a.Sender =" + mySourceAdrId + "; ";
        //    bReturn = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "InsertASNArtFieldAssignment", myUserId);
        //    return bReturn;
        //}

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

