using Common.ApiModels;
using Common.Enumerations;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using static LVS.Globals;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class PrintQueueViewData
    {
        public PrintQueues PrintQueue { get; set; } = new PrintQueues();
        private int BenutzerID { get; set; } = 0;

        public List<PrintQueues> ListPrintQueue { get; set; }

        public string LogText { get; set; }
        public List<string> ListLogText { get; set; }
        public PrintQueueViewData()
        {
            PrintQueue = new PrintQueues();
        }
        public PrintQueueViewData(int myUserId) : this()
        {
            BenutzerID = myUserId;
        }
        public PrintQueueViewData(int myId, int myUserId) : this()
        {
            PrintQueue.Id = myId;
            BenutzerID = myUserId;
            if (PrintQueue.Id > 0)
            {
                Fill();
            }
        }

        public void Fill()
        {
            string strSQL = sql_Get;
            DataTable dt = clsSQLARCHIVE.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "PrintQueue", "PrintQueue", BenutzerID);
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
            PrintQueue = new PrintQueues();
            Int32 iTmp = 0;
            Int32.TryParse(row["Id"].ToString(), out iTmp);
            PrintQueue.Id = iTmp;

            iTmp = 0;
            Int32.TryParse(row["RepoortDocSettingId"].ToString(), out iTmp);
            PrintQueue.ReportDocSettingId = iTmp;

            iTmp = 0;
            Int32.TryParse(row["RepoortDocSettingAssignmentId"].ToString(), out iTmp);
            PrintQueue.ReportDocSettingAssignmentId = iTmp;

            PrintQueue.Created = (DateTime)row["Created"];
            PrintQueue.IsActiv = (bool)row["IsActiv"];
            PrintQueue.TableName = row["TableName"].ToString();

            iTmp = 0;
            Int32.TryParse(row["TableId"].ToString(), out iTmp);
            PrintQueue.TableId = iTmp;

            iTmp = 0;
            Int32.TryParse(row["WorkspaceId"].ToString(), out iTmp);
            PrintQueue.WorkspaceId = iTmp;

            iTmp = 0;
            Int32.TryParse(row["PrintCount"].ToString(), out iTmp);
            PrintQueue.PrintCount = iTmp;

            PrintQueue.PrinterName = row["PrinterName"].ToString();
        }

        public void GetPrintQueueList()
        {
            ListPrintQueue = new List<PrintQueues>();
            string strSQL = string.Empty;
            DataTable dt = new DataTable("PrintQueues");
            strSQL = sql_Get;
            dt = clsSQLARCHIVE.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "GetPrintQueue", "PrintQueue", BenutzerID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                    ListPrintQueue.Add(PrintQueue);
                    Update_Deactivate();
                }
            }
        }


        /// <summary>
        ///             ADD
        /// </summary>
        public bool Add()
        {
            string strSql = sql_Add;
            strSql = strSql + " Select @@IDENTITY as 'ID';";

            string strTmp = clsSQLARCHIVE.ExecuteSQLWithTRANSACTIONGetValue(strSql, "Insert", BenutzerID);
            int.TryParse(strTmp, out int iTmp);
            PrintQueue.Id = iTmp;

            return (PrintQueue.Id > 0);
        }

        /// <summary>
        ///             Exist
        /// </summary>
        public bool Exist()
        {
            string strSql = sql_Exist;
            bool bReturn = clsSQLARCHIVE.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            return bReturn;
        }
        public void Update_Deactivate()
        {
            if (PrintQueue.Id > 0)
            {
                string strSql = string.Empty;
                strSql = sql_Update_Deactivate;
                bool mybExecOK = clsSQLARCHIVE.ExecuteSQLWithTRANSACTION(strSql, "Deactivate", BenutzerID);
            }
        }
        /// <summary>
        ///             DELETE
        /// </summary>
        public void Delete()
        {
            if (PrintQueue.Id > 0)
            {
                string strSql = string.Empty;
                strSql = sql_Delete;
                bool mybExecOK = clsSQLARCHIVE.ExecuteSQLWithTRANSACTION(strSql, "DELETE", BenutzerID);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myPrintQueue"></param>
        /// <returns></returns>
        public ResponsePrintQueue CreatePrintInsertByAusgang(ResponsePrintQueue myPrintQueue)
        {
            bool bReturn = false;
            try
            {
                if ((myPrintQueue.SelectedAusgang is Ausgaenge) && (myPrintQueue.SelectedAusgang.Id > 0))
                {
                    Ausgaenge ausgang = new Ausgaenge();
                    ausgang = myPrintQueue.SelectedAusgang.Copy();

                    LVS.Globals._GL_USER GLUser = new _GL_USER();
                    GLUser.User_ID = BenutzerID;

                    _GL_SYSTEM GLSystem = new _GL_SYSTEM();
                    clsSystem Sys = new clsSystem();

                    clsReportDocSetting ReportDocSetting = new clsReportDocSetting();
                    ReportDocSetting.InitClass(GLUser, GLSystem, Sys, ausgang.Auftraggeber, ausgang.ArbeitsbereichId);
                    clsReportDocSetting tmpSetting = new clsReportDocSetting();

                    string strTmpInfo = string.Empty;
                    for (int i = 1; i <= 3; i++)
                    {
                        strTmpInfo = string.Empty;
                        enumDokumentenArt PrintDocumentArt = enumDokumentenArt.NotSet;
                        switch (i)
                        {
                            case 1:
                                if (ausgang.PrintActionScannerLfs)
                                {
                                    PrintDocumentArt = enumDokumentenArt.AusgangLfs;
                                    strTmpInfo = " > Lieferschein";
                                }
                                break;
                            case 2:
                                if (ausgang.PrintActionScannerAusgangsliste)
                                {
                                    PrintDocumentArt = enumDokumentenArt.Ausgangsliste;
                                    strTmpInfo = " > Ausgangsliste";
                                }
                                break;
                            case 3:
                                if (ausgang.PrintActionScannerKVOFrachtbrief)
                                {
                                    PrintDocumentArt = enumDokumentenArt.KVOFrachtbrief;
                                    strTmpInfo = " > Frachtbrief";
                                }
                                break;
                        }

                        tmpSetting = new clsReportDocSetting();
                        tmpSetting = ReportDocSetting.GetReportDocSettingForPrintAusgangDocument(ausgang, PrintDocumentArt);

                        if ((tmpSetting is clsReportDocSetting) && (tmpSetting.ID > 0))
                        {
                            PrintQueue = new PrintQueues();
                            PrintQueue.ReportDocSettingId = tmpSetting.ID;
                            PrintQueue.ReportDocSettingAssignmentId = tmpSetting.RSAId;
                            PrintQueue.IsActiv = true;
                            PrintQueue.TableName = enumDatabaseSped4_TableNames.LAusgang.ToString();
                            PrintQueue.TableId = ausgang.Id;
                            PrintQueue.WorkspaceId = ausgang.Workspace.Id;
                            PrintQueue.PrintCount = myPrintQueue.PrintCount;
                            PrintQueue.PrinterName = myPrintQueue.PrinterName;

                            bool bExist = Exist();

                            if (!bExist)
                            {
                                if (Add())
                                {
                                    strTmpInfo += "-Druckauftrag wurde erstellt! ";
                                }
                                else
                                {
                                    strTmpInfo += "-Druckauftrag konnte NICHT erstellt werden!!!";
                                    strTmpInfo += " Es ist ein Fehler aufgetreten!!! ";
                                }
                            }
                            else
                            {
                                strTmpInfo += "-Druckauftrag existiert bereits! ";
                            }
                            myPrintQueue.Info += strTmpInfo + Environment.NewLine;
                        }
                    }
                    myPrintQueue.Success = true;
                    myPrintQueue.Error = "Bearbeitet Druckaufträge:" + Environment.NewLine + myPrintQueue.Info;
                    myPrintQueue.Info = myPrintQueue.Error;


                    AusgangViewData vdAusgang = new AusgangViewData(ausgang.Id, myPrintQueue.UserId, true);
                    myPrintQueue.SelectedAusgang = vdAusgang.Ausgang.Copy();

                }
            }
            catch (Exception ex)
            {
                myPrintQueue.Success = false;
            }
            return myPrintQueue;
        }

        public static Ausgaenge GetPrintDocStatus_StoreOut(Ausgaenge myAusgang)
        {
            myAusgang.PrintDocumentStoreOutStatus_Frachtbrief = enumPrintDocumentStoreOutStatus_Frachtbrief.NotSet;
            myAusgang.PrintDocumentStoreOutStatus_Lfs = enumPrintDocumentStoreOutStatus_Lfs.NotSet;
            myAusgang.PrintDocumentStoreOutStatus_List = enumPrintDocumentStoreOutStatus_List.NotSet;

            if (
                (myAusgang.PrintActionScannerKVOFrachtbrief) ||
                (myAusgang.PrintActionScannerAusgangsliste) ||
                (myAusgang.PrintActionScannerLfs)
              )
            {
                LVS.Globals._GL_USER GLUser = new _GL_USER();
                GLUser.User_ID = 1;

                _GL_SYSTEM GLSystem = new _GL_SYSTEM();
                clsSystem Sys = new clsSystem();

                clsReportDocSetting ReportDocSetting = new clsReportDocSetting();
                ReportDocSetting.InitClass(GLUser, GLSystem, Sys, myAusgang.Auftraggeber, myAusgang.ArbeitsbereichId);

                clsReportDocSetting tmpSetting = new clsReportDocSetting();

                PrintQueueViewData pqVD = new PrintQueueViewData();

                string strSql = string.Empty;
                strSql = "Select TOP(1) * FROM PrintQueue  ";
                strSql += "WHERE TableName='LAusgang'";
                strSql += " and TableId = " + myAusgang.Id;
                strSql += " and WorkspaceId = " + myAusgang.Workspace.Id;

                if (myAusgang.PrintActionScannerKVOFrachtbrief)
                {
                    tmpSetting = ReportDocSetting.GetReportDocSettingForPrintAusgangDocument(myAusgang, enumDokumentenArt.KVOFrachtbrief);
                    if (tmpSetting != null)
                    {
                        string strSqlFB = strSql;
                        strSqlFB += " and RepoortDocSettingId = " + tmpSetting.ID;
                        strSqlFB += " and RepoortDocSettingAssignmentId = " + tmpSetting.RSAId;
                        strSqlFB += " Order by Created desc";

                        DataTable dt = clsSQLARCHIVE.ExecuteSQL_GetDataTable(strSqlFB, 1, "PrintQueue");
                        if (dt.Rows.Count > 0)
                        {
                            bool bIsActiv = (bool)dt.Rows[0]["IsActiv"];
                            if (bIsActiv)
                            {
                                myAusgang.PrintDocumentStoreOutStatus_Frachtbrief = enumPrintDocumentStoreOutStatus_Frachtbrief.PrintOrderSet;
                            }
                            else
                            {
                                myAusgang.PrintDocumentStoreOutStatus_Frachtbrief = enumPrintDocumentStoreOutStatus_Frachtbrief.Printed;
                            }
                        }
                    }
                }
                if (myAusgang.PrintActionScannerLfs)
                {
                    tmpSetting = ReportDocSetting.GetReportDocSettingForPrintAusgangDocument(myAusgang, enumDokumentenArt.AusgangLfs);
                    if (tmpSetting != null)
                    {
                        string strSqlLfs = strSql;
                        strSqlLfs += " and RepoortDocSettingId = " + tmpSetting.ID;
                        strSqlLfs += " and RepoortDocSettingAssignmentId = " + tmpSetting.RSAId;
                        strSqlLfs += " Order by Created desc";

                        DataTable dt = clsSQLARCHIVE.ExecuteSQL_GetDataTable(strSqlLfs, 1, "PrintQueue");
                        if (dt.Rows.Count > 0)
                        {
                            bool bIsActiv = (bool)dt.Rows[0]["IsActiv"];
                            if (bIsActiv)
                            {
                                myAusgang.PrintDocumentStoreOutStatus_Lfs = enumPrintDocumentStoreOutStatus_Lfs.PrintOrderSet;
                            }
                            else
                            {
                                myAusgang.PrintDocumentStoreOutStatus_Lfs = enumPrintDocumentStoreOutStatus_Lfs.Printed;
                            }
                        }
                    }
                }
                if (myAusgang.PrintActionScannerAusgangsliste)
                {
                    tmpSetting = ReportDocSetting.GetReportDocSettingForPrintAusgangDocument(myAusgang, enumDokumentenArt.Ausgangsliste);
                    if (tmpSetting != null)
                    {
                        string strSqlList = strSql;
                        strSqlList += " and RepoortDocSettingId = " + tmpSetting.ID;
                        strSqlList += " and RepoortDocSettingAssignmentId = " + tmpSetting.RSAId;
                        strSqlList += " Order by Created desc";

                        DataTable dt = clsSQLARCHIVE.ExecuteSQL_GetDataTable(strSqlList, 1, "PrintQueue");
                        if (dt.Rows.Count > 0)
                        {
                            bool bIsActiv = (bool)dt.Rows[0]["IsActiv"];
                            if (bIsActiv)
                            {
                                myAusgang.PrintDocumentStoreOutStatus_List = enumPrintDocumentStoreOutStatus_List.PrintOrderSet;
                            }
                            else
                            {
                                myAusgang.PrintDocumentStoreOutStatus_List = enumPrintDocumentStoreOutStatus_List.Printed;
                            }
                        }
                    }
                }
            }
            return myAusgang;
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
                PrintQueue.Created = DateTime.Now;
                strSql = "INSERT INTO PrintQueue (RepoortDocSettingId, RepoortDocSettingAssignmentId, Created, IsActiv, TableName, TableId, WorkspaceId, PrintCount, PrinterName " +
                                            ") " +
                                                    "VALUES (" + PrintQueue.ReportDocSettingId +
                                                     ", " + PrintQueue.ReportDocSettingAssignmentId +
                                                     ", '" + PrintQueue.Created + "'" +
                                                     ", " + Convert.ToInt32(PrintQueue.IsActiv) +
                                                     ", '" + PrintQueue.TableName + "'" +
                                                     ", " + PrintQueue.TableId +
                                                     ", " + PrintQueue.WorkspaceId +
                                                     ", " + PrintQueue.PrintCount +
                                                     ", '" + PrintQueue.PrinterName + "'" +
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
                strSql = "Select * " +
                                "FROM PrintQueue  " +
                                "WHERE IsActiv=1 " +
                                 "ORDER BY Created ";

                return strSql;
            }
        }
        /// <summary>
        ///             GET
        /// </summary>
        public string sql_Exist
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Select * FROM PrintQueue  ";
                strSql += "WHERE IsActiv=1 ";
                strSql += " and WorkspaceId =" + PrintQueue.WorkspaceId;
                strSql += " and TableName = '" + PrintQueue.TableName + "'";
                strSql += " and TableId = " + PrintQueue.TableId;
                strSql += " and RepoortDocSettingId = " + PrintQueue.ReportDocSettingId;
                strSql += " and RepoortDocSettingAssignmentId = " + PrintQueue.ReportDocSettingAssignmentId;
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
                strSql = "Delete FROM PrintQueue WHERE ID =" + PrintQueue.Id; ;
                return strSql;
            }
        }
        /// <summary>
        ///             Update sql - String
        /// </summary>
        public string sql_Update_Deactivate
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Update PrintQueue SET " +
                                        "IsActiv= 0" +
                                        "  WHERE ID=" + PrintQueue.Id + "; ";
                return strSql;
            }
        }
    }
}

