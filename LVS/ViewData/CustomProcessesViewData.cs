using LVS.Constants;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class CustomProcessesViewData
    {

        public WorkspaceViewData workspaceVD { get; set; }
        public ArticleViewData artikelVD { get; set; } = new ArticleViewData();
        public bool ExistCustomProcess { get; set; } = false;
        public LVS.Models.CustomProcesses CustomProcess { get; set; }
        public List<LVS.Models.CustomProcesses> ListCustomProcesses { get; set; }
        public CustomProcesses.CustomProcess_Novelis_AccessByArticleCert Process_Novelis_AccessByArticleCert;
        public CustomProcesses.CustomProcess_SLEArcelor_CreateTeslaPNNo Process_SLEArcelor_CreateTeslaPNNo;
        public DataTable dtCustomProcesses { get; set; }
        public Globals._GL_USER GL_USER { get; set; }
        private int _BenutzerID;
        public int BenutzerID
        {
            get
            {
                if (GL_USER.User_ID > 0)
                {
                    _BenutzerID = (int)GL_USER.User_ID;
                }
                return _BenutzerID;
            }
            set
            {
                _BenutzerID = value;
            }
        }



        public CustomProcessesViewData()
        {
            InitCls();
        }
        public CustomProcessesViewData(Globals._GL_USER myGLUser)
        {
            this.GL_USER = myGLUser;
            InitCls();
        }


        public CustomProcessesViewData(int myId, int myUserId, bool myFillClsOnly)
        {
            InitCls();
            this.BenutzerID = myUserId;
            this.CustomProcess.Id = myId;
            if (this.CustomProcess.Id > 0)
            {
                Fill(myFillClsOnly);
            }
        }

        public CustomProcessesViewData(int myAuftraggeber, int myWorkspaceId, Globals._GL_USER myGLUser)
        {
            this.GL_USER = myGLUser;
            InitCls();
            CheckExistProcessByAdrAndWorkspace(myAuftraggeber, myWorkspaceId);
        }
        public CustomProcessesViewData(int myArticleId, Globals._GL_USER myGLUser)
        {
            this.GL_USER = myGLUser;
            InitCls();
            if (myArticleId > 0)
            {
                //artikelVD = new ArticleViewData(myArticleId, (int)myGLUser.User_ID,true);
                artikelVD = new ArticleViewData(myArticleId, (int)myGLUser.User_ID, false);
                CheckExistProcessByAdrAndWorkspace(artikelVD.Eingang.Auftraggeber, artikelVD.Artikel.AbBereichID);
            }
        }
        public CustomProcessesViewData(LVS.Models.CustomProcesses myCustomProcess)
        {
            this.CustomProcess = myCustomProcess;
        }

        private void CheckExistProcessByAdrAndWorkspace(int myAuftraggeber, int myWorkspaceId)
        {
            if (
                    (ListCustomProcesses.Count > 0) &&
                    (myAuftraggeber > 0) &&
                    (myWorkspaceId > 0)
               )
            {
                var list = ListCustomProcesses.Where(x => x.AdrId == myAuftraggeber).ToList();
                ListCustomProcesses = new List<Models.CustomProcesses>();
                ListCustomProcesses = list.Where(x => x.ListProcessWorkspaces.Contains(myWorkspaceId)).ToList();

                foreach (var item in ListCustomProcesses)
                {
                    if (item is LVS.Models.CustomProcesses)
                    {
                        switch (item.ProcessName)
                        {
                            case constValue_CustomProcesses.const_Process_Novelis_ArticleAccessByCertifacte:
                                CustomProcess = item.Copy();
                                Process_Novelis_AccessByArticleCert = new CustomProcesses.CustomProcess_Novelis_AccessByArticleCert(this.GL_USER);
                                ExistCustomProcess = true;
                                break;

                            case constValue_CustomProcesses.const_Process_SLEArcelor_CreateTeslaPNNo:
                                CustomProcess = item.Copy();
                                ExistCustomProcess = true;
                                Process_SLEArcelor_CreateTeslaPNNo = new CustomProcesses.CustomProcess_SLEArcelor_CreateTeslaPNNo(this.GL_USER);

                                break;
                        }
                    }
                }
            }
        }
        /// <summary>
        ///             returns bool true, if anything is done
        ///            
        /// <param name="myArticleId"></param>
        /// <param name="myEingangId"></param>
        /// <param name="myAusgangId"></param>
        /// <param name="myProcessLocationString"></param>
        /// <returns></returns>
        public bool CheckAndExecuteCustomProcess(int myArticleId, int myEingangId, int myAusgangId, string myProcessLocationString)
        {
            bool bReturn = false;
            if (
                    (CustomProcess is LVS.Models.CustomProcesses) &&
                    (CustomProcess.Id > 0)
               )
            {

                ////-- Ermittlen der Ausnahmen CustomerProcessException
                //CustomProcessExceptions cpException = cpVD.CustomProcess.ListCustomProcessExceptions.FirstOrDefault(x => x.GoodsTypeId == art.GArtID);
                //bool bExistProcessException = ((cpException is CustomProcessExceptions) && (cpException.Id > 0));



                switch (CustomProcess.ProcessName)
                {
                    case constValue_CustomProcesses.const_Process_Novelis_ArticleAccessByCertifacte:
                        bReturn = Process_Novelis_AccessByArticleCert.ExecuteProcess(myArticleId, myEingangId, myAusgangId, myProcessLocationString);
                        break;
                    case constValue_CustomProcesses.const_Process_SLEArcelor_CreateTeslaPNNo:
                        bReturn = Process_SLEArcelor_CreateTeslaPNNo.ExecuteProcess(myArticleId, myEingangId, myAusgangId, myProcessLocationString);
                        break;
                }
            }
            return bReturn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myEingangVD"></param>
        /// <returns></returns>
        public bool CheckForCustomProzessForASNTransfer(EingangViewData myEingangVD)
        {
            bool bReturn = myEingangVD.Eingang.Workspace.ASNTransfer;
            var list = ListCustomProcesses.Where(x => x.AdrId == myEingangVD.Eingang.Auftraggeber).ToList();
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    if (item is LVS.Models.CustomProcesses)
                    {
                        if (item.ListProcessWorkspaces.Contains(myEingangVD.Eingang.ArbeitsbereichId))
                        {
                            switch (item.ProcessName)
                            {
                                case constValue_CustomProcesses.const_Process_Novelis_ArticleAccessByCertifacte:
                                    var listInSPL = myEingangVD.ListArticleInEingang.Select(x => x.bSPL == true).ToList();
                                    if (listInSPL.Count > 0)
                                    {
                                        //- ARtikel im SPL, also keine EDI Meldung
                                        bReturn = false;

                                        myEingangVD.Eingang.Check = false;
                                        myEingangVD.Update_Datafield_Check(myEingangVD.Eingang.Check);
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
            return bReturn;
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitCls()
        {
            CustomProcess = new LVS.Models.CustomProcesses();
            ExistCustomProcess = false;
            ListCustomProcesses = new List<LVS.Models.CustomProcesses>();
            FillList(true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mybInclSub"></param>
        public void Fill(bool myFillClsOnly)
        {
            string strSQL = sql_Get;
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "CustomProcesses");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr, myFillClsOnly);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        public void SetValue(DataRow row, bool myFillClsOnly)
        {
            CustomProcess = new LVS.Models.CustomProcesses();

            int iTmp = 0;
            int.TryParse(row["ID"].ToString(), out iTmp);
            CustomProcess.Id = iTmp;

            iTmp = 0;
            int.TryParse(row["AdrId"].ToString(), out iTmp);
            CustomProcess.AdrId = iTmp;

            CustomProcess.ProcessName = row["ProcessName"].ToString();
            CustomProcess.IsActive = (bool)row["IsActive"];
            CustomProcess.ProcessWorkspaces = row["ProcessWorkspaces"].ToString();
            if (CustomProcess.AdrId > 0)
            {
                AddressViewData vd = new AddressViewData(CustomProcess.AdrId, 1);
                CustomProcess.Adress = vd.Address.Copy();
            }
            if ((!myFillClsOnly) && (CustomProcess.Id > 0))
            {
                CustomProcess.ListCustomProcessExceptions = CustomProcessExceptionsViewData.GetListCustomerProcessExceptionByCustomerProcessId(CustomProcess.Id);
            }
        }

        /// <summary>
        ///             ADD
        /// </summary>
        public void Add()
        {
            string strSQL = sql_Add;
            strSQL = strSQL + " Select @@IDENTITY as 'ID' ";
            string strTmp = clsSQLCOM.ExecuteSQL_GetValue(strSQL, BenutzerID);
            int iTmp = 0;
            int.TryParse(strTmp, out iTmp);
            if (iTmp > 0)
            {
                CustomProcess.Id = iTmp;
            }
        }
        /// <summary>
        ///             DELETE
        /// </summary>
        public bool Delete()
        {
            string strSql = sql_Delete;
            bool retVal = clsSQLCOM.ExecuteSQL(strSql, BenutzerID);
            return retVal;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mybInclSub"></param>
        public void FillList(bool mybInclSub)
        {
            ListCustomProcesses = new List<LVS.Models.CustomProcesses>();
            string strSQL = sql_Get_Main;
            dtCustomProcesses = new DataTable();
            dtCustomProcesses = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "CustomProcesses");
            if (dtCustomProcesses.Rows.Count > 0)
            {
                foreach (DataRow dr in dtCustomProcesses.Rows)
                {
                    SetValue(dr, mybInclSub);
                    ListCustomProcesses.Add(CustomProcess);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DataTable GetList()
        {
            ListCustomProcesses = new List<LVS.Models.CustomProcesses>();
            string strSQL = sql_GetListActive;
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EdiDelforValue");
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
                CustomProcess.Created = DateTime.Now;
                string strSQL = "INSERT INTO CustomProcesses ([AdrId], [ProcessName], [IsActive], [ProcessWorkspaces],[Created])" +
                                                            " VALUES " +
                                                            "(" +
                                                                CustomProcess.AdrId +
                                                                ", '" + CustomProcess.ProcessName + "'" +
                                                                ", " + Convert.ToInt32(CustomProcess.IsActive) +
                                                                ", '" + CustomProcess.ProcessWorkspaces + "'" +
                                                                ", '" + CustomProcess.Created + "'" +
                                                             "); ";
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
                strSql = "SELECT * FROM CustomProcesses WHERE ID=" + CustomProcess.Id + "; ";
                return strSql;
            }
        }

        /// <summary>
        ///             GET List
        /// </summary>
        public string sql_GetListActive
        {
            get
            {
                string strSql = string.Empty;
                strSql = "SELECT * FROM CustomProcesses WHERE IsActive=1; ";
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
                strSql = "SELECT * FROM CustomProcesses";
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
                strSql = "DELETE CustomProcesses WHERE Id =" + CustomProcess.Id + "; ";
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
                strSql = "Update CustomProcesses SET " +
                                            " [AdrId] = " + CustomProcess.AdrId +
                                            ", [ProcessName]= '" + CustomProcess.ProcessName + "'" +
                                            ", [IsActive] = " + Convert.ToInt32(CustomProcess.IsActive) +
                                            ", [ProcessWorkspaces] = '" + CustomProcess.ProcessWorkspaces + "'" +

                                            "WHERE ID=" + CustomProcess.Id + " ;";
                return strSql;
            }
        }
        /// <summary>
        ///             Update sql - String
        /// </summary>
        public string sql_Update_IsActive
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Update CustomProcesses SET " +
                                            "IsActive=" + Convert.ToInt32(CustomProcess.IsActive) + " " +
                                            "WHERE ID=" + CustomProcess.Id + " ;";
                return strSql;
            }
        }



        //-------------------------------------------------------------------------------------------------------
        //                                          static
        //-------------------------------------------------------------------------------------------------------

        //public static bool ExistNewDelforCallToProceed(int myUserId, int myWorkspaceId)
        //{
        //    string strSql = "SELECT Id FROM EdiDelforD97AValue where IsActive=1 and WorkspaceId="+myWorkspaceId+";";
        //    bool bReturn = clsSQLCOM.ExecuteSQL_GetValueBool(strSql, myUserId);
        //    return bReturn;
        //}
    }
}

