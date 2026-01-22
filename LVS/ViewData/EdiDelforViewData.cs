using Common.Models;
using LVS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class EdiDelforViewData
    {
        public AddressViewData adrVD { get; set; }
        public WorkspaceViewData workspaceVD { get; set; }
        public EdiDelforD97AValues EdiDelforValue { get; set; }
        private Globals._GL_USER GL_USER = new Globals._GL_USER();
        private int BenutzerID { get; set; }
        public List<EdiDelforD97AValues> ListEdiDelforValue { get; set; }

        public EdiDelforViewData()
        {
            InitCls();
        }
        public EdiDelforViewData(Globals._GL_USER myGLUser) : this()
        {
            GL_USER = myGLUser;
            BenutzerID = (int)GL_USER.User_ID;
        }

        public EdiDelforViewData(EdiDelforD97AValues myEdiDelforD97AValues)
        {
            this.EdiDelforValue = myEdiDelforD97AValues;
        }
        public EdiDelforViewData(int myId, Globals._GL_USER myGLUser, bool mybInclSub) : this()
        {
            EdiDelforValue.Id = myId;
            GL_USER = myGLUser;
            BenutzerID = (int)GL_USER.User_ID;
            if (EdiDelforValue.Id > 0)
            {
                Fill(mybInclSub);
            }
        }
        public EdiDelforViewData(int myId, int myUserId, bool mybInclSub) : this()
        {
            EdiDelforValue.Id = myId;
            BenutzerID = myUserId;
            if (EdiDelforValue.Id > 0)
            {
                Fill(mybInclSub);
            }
        }

        private void InitCls()
        {
            EdiDelforValue = new EdiDelforD97AValues();
        }

        public void Fill(bool mybInclSub)
        {
            string strSQL = sql_Get;
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EdiDelforValue");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr, mybInclSub);
                }
            }
        }


        public void SetValue(DataRow row, bool mybInclSub)
        {
            EdiDelforValue = new EdiDelforD97AValues();

            int iTmp = 0;
            int.TryParse(row["ID"].ToString(), out iTmp);
            EdiDelforValue.Id = iTmp;

            DateTime tmpDate = new DateTime(1900, 1, 1);
            DateTime.TryParse(row["DocumentDate"].ToString(), out tmpDate);
            EdiDelforValue.DocumentDate = tmpDate;

            iTmp = 0;
            int.TryParse(row["DocumentNo"].ToString(), out iTmp);
            EdiDelforValue.DocumentNo = iTmp;

            iTmp = 0;
            int.TryParse(row["DeliveryScheduleNumber"].ToString(), out iTmp);
            EdiDelforValue.DeliveryScheduleNumber = iTmp;

            iTmp = 0;
            int.TryParse(row["Position"].ToString(), out iTmp);
            EdiDelforValue.Position = iTmp;

            iTmp = 0;
            int.TryParse(row["Client"].ToString(), out iTmp);
            EdiDelforValue.Client = iTmp;

            iTmp = 0;
            int.TryParse(row["Supplier"].ToString(), out iTmp);
            EdiDelforValue.Supplier = iTmp;

            iTmp = 0;
            int.TryParse(row["Recipient"].ToString(), out iTmp);
            EdiDelforValue.Recipient = iTmp;

            EdiDelforValue.Werksnummer = row["Werksnummer"].ToString();
            EdiDelforValue.OrderNo = row["OrderNo"].ToString();
            iTmp = 0;
            int.TryParse(row["CumQuantityReceived"].ToString(), out iTmp);
            EdiDelforValue.CumQuantityReceived = iTmp;

            tmpDate = new DateTime(1900, 1, 1);
            DateTime.TryParse(row["CumQuantityStartDate"].ToString(), out tmpDate);
            EdiDelforValue.CumQuantityStartDate = tmpDate;

            iTmp = 0;
            int.TryParse(row["ReceivedQuantity"].ToString(), out iTmp);
            EdiDelforValue.ReceivedQuantity = iTmp;

            EdiDelforValue.SID = row["SID"].ToString();

            tmpDate = new DateTime(1900, 1, 1);
            DateTime.TryParse(row["GoodReceiptDate"].ToString(), out tmpDate);
            EdiDelforValue.GoodReceiptDate = tmpDate;

            iTmp = 0;
            int.TryParse(row["SchedulingConditions"].ToString(), out iTmp);
            EdiDelforValue.SchedulingConditions = iTmp;

            iTmp = 0;
            int.TryParse(row["CallQuantity"].ToString(), out iTmp);
            EdiDelforValue.CallQuantity = iTmp;

            tmpDate = new DateTime(1900, 1, 1);
            DateTime.TryParse(row["DeliveryDate"].ToString(), out tmpDate);
            EdiDelforValue.DeliveryDate = tmpDate;

            EdiDelforValue.IsActive = (bool)row["IsActive"];

            iTmp = 0;
            int.TryParse(row["WorkspaceId"].ToString(), out iTmp);
            EdiDelforValue.WorkspaceId = iTmp;

            EdiDelforValue.Description = row["Description"].ToString();

            if (mybInclSub)
            {
                if (EdiDelforValue.Client > 0)
                {
                    adrVD = new AddressViewData(EdiDelforValue.Client, 1);
                    EdiDelforValue.ClientAdr = adrVD.Address.Copy();
                }
                if (EdiDelforValue.Supplier > 0)
                {
                    adrVD = new AddressViewData(EdiDelforValue.Supplier, 1);
                    EdiDelforValue.SupplierAdr = adrVD.Address.Copy();
                }
                if (EdiDelforValue.Recipient > 0)
                {
                    adrVD = new AddressViewData(EdiDelforValue.Recipient, 1);
                    EdiDelforValue.RecipientAdr = adrVD.Address.Copy();
                }
                if (EdiDelforValue.WorkspaceId > 0)
                {
                    workspaceVD = new WorkspaceViewData(EdiDelforValue.WorkspaceId);
                    EdiDelforValue.Workspace = workspaceVD.Workspace.Copy();
                }

                if ((!EdiDelforValue.IsActive) && (!EdiDelforValue.DocumentNo.Equals(string.Empty)))
                {
                    // EdiDelforValue.DictDelivered = new Dictionary<Ausgaenge, List<Articles>>(StockViewData.GetList_DeliveredArcicleDelfor(EdiDelforValue.DocumentNo, EdiDelforValue.WorkspaceId, EdiDelforValue.Client));
                }
                AusgangViewData ausgangViewData = new AusgangViewData();
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
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                EdiDelforValue.Id = decTmp;
            }
        }
        /// <summary>
        ///             DELETE
        /// </summary>
        public void Delete()
        {
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
        /// <returns></returns>
        public bool Update_RebookCall()
        {
            string strSql = sql_Update_RebookCall;
            bool retVal = clsSQLCOM.ExecuteSQL(strSql, BenutzerID);
            return retVal;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mybInclSub"></param>
        public void FillList(bool mybInclSub)
        {
            ListEdiDelforValue = new List<EdiDelforD97AValues>();
            string strSQL = sql_GetList;
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EdiDelforValue");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr, mybInclSub);
                    ListEdiDelforValue.Add(EdiDelforValue);
                }
            }
        }
        public List<EdiDelforD97AValues> FillListDelivered(bool mybInclSub)
        {
            List<EdiDelforD97AValues> ListEdiDelforValueDelivered = new List<EdiDelforD97AValues>();
            string strSQL = sql_GetListDelivered;
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EdiDelforValue");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr, mybInclSub);
                    ListEdiDelforValueDelivered.Add(EdiDelforValue);
                }
            }
            return ListEdiDelforValueDelivered;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetList()
        {
            string strSQL = sql_GetList;
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EdiDelforValue");
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myArticle"></param>
        /// <returns></returns>
        public bool CreateCallInsert(Articles myArticle)
        {
            bool bReturn = false;
            if (myArticle.Id > 0)
            {
                CallViewData callVD = new CallViewData();
                callVD.Abruf = new Calls();
                callVD.Abruf.IsRead = false;
                callVD.Abruf.ArtikelId = myArticle.Id;
                callVD.Abruf.LVSNr = myArticle.LVS_ID;
                callVD.Abruf.Werksnummer = myArticle.Werksnummer;
                callVD.Abruf.Produktionsnummer = myArticle.Produktionsnummer;
                callVD.Abruf.Charge = myArticle.Charge;
                callVD.Abruf.Brutto = myArticle.Brutto;
                callVD.Abruf.CompanyId = 0;
                callVD.Abruf.CompanyName = string.Empty;
                callVD.Abruf.ArbeitsbereichId = myArticle.AbBereichID;
                callVD.Abruf.Datum = new DateTime(1900, 1, 1);
                callVD.Abruf.EintreffDatum = new DateTime(this.EdiDelforValue.DeliveryDate.Year, this.EdiDelforValue.DeliveryDate.Month, this.EdiDelforValue.DeliveryDate.Day);
                callVD.Abruf.EintreffZeit = new DateTime(1900, 1, 1, this.EdiDelforValue.DeliveryDate.Hour, this.EdiDelforValue.DeliveryDate.Minute, 0);
                callVD.Abruf.BenutzerID = (int)this.GL_USER.User_ID;
                callVD.Abruf.Benutzername = this.GL_USER.Name;
                callVD.Abruf.Schicht = string.Empty;
                callVD.Abruf.Referenz = this.EdiDelforValue.DocumentNo.ToString();
                callVD.Abruf.Abladestelle = string.Empty;
                callVD.Abruf.Aktion = clsASNCall.const_AbrufAktion_Abruf;
                callVD.Abruf.Erstellt = DateTime.Now;
                callVD.Abruf.Status = Common.Enumerations.enumCallStatus.erstellt;
                callVD.Abruf.LiefAdrId = this.EdiDelforValue.Client;
                callVD.Abruf.EmpAdrId = this.EdiDelforValue.Recipient;
                callVD.Abruf.SpedAdrId = 0;
                callVD.Abruf.ASNFile = string.Empty;
                callVD.Abruf.ASNLieferant = string.Empty;
                callVD.Abruf.ASNQuantity = 0;
                callVD.Abruf.ASNUnit = string.Empty;
                callVD.Abruf.Description = "[Id]|[DelforeNo]|[Datum] : " + this.EdiDelforValue.Id.ToString() + " | " + this.EdiDelforValue.DocumentNo.ToString() + " | " + this.EdiDelforValue.DocumentDate.ToString("dd.MM.yyyy");
                callVD.Abruf.ScanCheckForStoreOut = new DateTime(1900, 1, 1);
                callVD.Abruf.ScanUserId = 0;
                callVD.Abruf.EdiDelforD97AValueId = (int)this.EdiDelforValue.Id;

                callVD.Add();
                bReturn = (callVD.Abruf.Id > 0);

            }
            return bReturn;
        }

        public bool DeleteDelforInCall()
        {
            bool bReturn = false;
            if (this.EdiDelforValue.Id > 0)
            {
                CallViewData callVD = new CallViewData();
                callVD.Abruf = new Calls();


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
                if (this.EdiDelforValue.DeliveryDate <= DateTime.Now)
                {
                    this.EdiDelforValue.IsActive = false;
                    this.EdiDelforValue.Description = "Termin bereits abgelaufen!";
                }

                string strSQL = "INSERT INTO EdiDelforD97AValue ([DocumentDate], [DocumentNo], [DeliveryScheduleNumber], [Position], [Client], [Supplier], [Recipient], " +
                                                                "[Werksnummer], [OrderNo], [CumQuantityReceived], [CumQuantityStartDate], [ReceivedQuantity]," +
                                                                "[SID], [GoodReceiptDate], [SchedulingConditions], [CallQuantity], [DeliveryDate], " +
                                                                "[IsActive], [WorkspaceId], [Description])" +

                                                                  "VALUES ('" + EdiDelforValue.DocumentDate + "'" +
                                                                            ", " + EdiDelforValue.DocumentNo +
                                                                            ", " + EdiDelforValue.DeliveryScheduleNumber +
                                                                            ", " + EdiDelforValue.Position +
                                                                            ", " + EdiDelforValue.Client +
                                                                            ", " + EdiDelforValue.Supplier +
                                                                            ", " + EdiDelforValue.Recipient +

                                                                            ", '" + EdiDelforValue.Werksnummer + "'" +
                                                                            ", '" + EdiDelforValue.OrderNo + "'" +
                                                                            ", " + EdiDelforValue.CumQuantityReceived +
                                                                            ", '" + EdiDelforValue.CumQuantityStartDate + "'" +
                                                                            ", " + EdiDelforValue.ReceivedQuantity +

                                                                            ", '" + EdiDelforValue.SID + "'" +
                                                                            ", '" + EdiDelforValue.GoodReceiptDate + "'" +
                                                                            ", " + EdiDelforValue.SchedulingConditions +
                                                                            ", " + EdiDelforValue.CallQuantity +
                                                                            ", '" + EdiDelforValue.DeliveryDate + "'" +

                                                                            ", " + Convert.ToInt32(EdiDelforValue.IsActive) +
                                                                            ", " + EdiDelforValue.WorkspaceId +
                                                                            ", '" + EdiDelforValue.Description + "' " +
                                                                            ") ";
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
                strSql = "SELECT * FROM EdiDelforD97AValue WHERE ID=" + EdiDelforValue.Id + "; ";
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
                strSql = "SELECT * FROM EdiDelforD97AValue WHERE IsActive=1; ";
                return strSql;
            }
        }
        /// <summary>
        ///             GET ListDelivered
        /// </summary>
        public string sql_GetListDelivered
        {
            get
            {
                string strSql = string.Empty;
                strSql = "SELECT * FROM EdiDelforD97AValue WHERE IsActive=0 order by id desc; ";
                return strSql;
            }
        }
        public string sql_GetBestandForForecast
        {
            get
            {
                string strSql = string.Empty;
                //strSql = "SELECT * FROM EdiDelforD97AValue WHERE IsActive=1; ";
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
                strSql = "SELECT * FROM EdiDelforD97AValue";
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
                strSql = "Update EdiDelforD97AValue SET " +
                                            "IsActive=" + Convert.ToInt32(EdiDelforValue.IsActive) + " " +
                                            "WHERE ID=" + EdiDelforValue.Id + " ;";
                return strSql;
            }
        }
        public string sql_Update_RebookCall
        {
            get
            {
                string strSql = string.Empty;
                EdiDelforValue.IsActive = true;
                string strDesc = "Rebook Abruf am " + DateTime.Now.ToString("dd.MM.yyyy HH:mm") + " Uhr";
                EdiDelforValue.Description += Environment.NewLine + strDesc;

                strSql = "Update EdiDelforD97AValue SET " +
                                            "IsActive=" + Convert.ToInt32(EdiDelforValue.IsActive) + " " +
                                            ", Description = '" + EdiDelforValue.Description + "' " +
                                            "WHERE ID=" + EdiDelforValue.Id + " ;";
                return strSql;
            }
        }
        /// <summary>
        ///             Update sql - String
        /// </summary>
        public string sql_Update_DeactivateOldDelforCalls
        {
            get
            {
                string strDesc = "neuer Abruf " + EdiDelforValue.DocumentNo + " vom " + EdiDelforValue.DocumentDate.ToString("dd.MM.yyyy");

                string strSql = string.Empty;
                strSql = "Update EdiDelforD97AValue SET " +
                                            "IsActive=0 " +
                                            ", Description = '" + strDesc + "' " +
                                            "WHERE ID IN (" +
                                                "SELECT Id FROM EdiDelforD97AValue " +
                                                            "WHERE " +
                                                                "IsActive=1 " +
                                                                "and Werksnummer='" + EdiDelforValue.Werksnummer + "'" +
                                                                "and DocumentDate <= '" + DateTime.Now.ToString("dd.MM.yyyy") + "' " +
                                                         ");";
                return strSql;
            }
        }
        ///// <summary>
        /////             Update sql - String
        ///// </summary>
        //public string sql_UpdateActive
        //{
        //    get
        //    {
        //        string strSql = string.Empty;
        //        strSql = "Update EdiDelforD97AValue SET " +
        //                                    "IsActive=" + Convert.ToInt32(EdiDelforValue.IsActive) + " " +
        //                                    "WHERE ID=" + EdiDelforValue.Id + " ;";
        //        return strSql;
        //    }
        //}


        //-------------------------------------------------------------------------------------------------------
        //                                          static
        //-------------------------------------------------------------------------------------------------------

        public static bool ExistNewDelforCallToProceed(int myUserId, int myWorkspaceId)
        {
            string strSql = "SELECT Id FROM EdiDelforD97AValue where IsActive=1 and WorkspaceId=" + myWorkspaceId + ";";
            bool bReturn = clsSQLCOM.ExecuteSQL_GetValueBool(strSql, myUserId);
            return bReturn;
        }
    }
}

