using Common.ApiModels;
using Common.Enumerations;
using Common.Models;
using Common.SqlStatementCreater;
using System;
using System.Collections.Generic;
using System.Data;
using static LVS.Globals;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class AusgangViewData
    {
        public Ausgaenge Ausgang { get; set; }
        private int BenutzerID { get; set; } = 0;
        public ArticleViewData articleViewData { get; set; }
        public WorkspaceViewData workspaceViewData { get; set; }
        //public clsLEingang Eingang { get; set; }

        public List<Articles> ListArticleInAusgang { get; set; }
        public List<Ausgaenge> ListAusgaengeOpen { get; set; }
        private int PrintCount { get; set; } = 1;
        private string PrinterName { get; set; } = string.Empty;

        public AusgangViewData()
        {
            InitCls();
        }

        public AusgangViewData(ResponseAusgang myResponseAusgang) : this()
        {
            this.Ausgang = myResponseAusgang.Ausgang.Copy();
            this.PrintCount = myResponseAusgang.PrintCount;
            this.PrinterName = myResponseAusgang.PrinterName;
            this.BenutzerID = myResponseAusgang.UserId;
        }
        public AusgangViewData(Ausgaenge myAusgang, int myUserId) : this()
        {
            this.Ausgang = myAusgang.Copy();
            this.BenutzerID = myUserId;
        }
        public AusgangViewData(int myId, int myUserId, bool myLoadArticleList) : this()
        {
            //InitCls();
            Ausgang.Id = myId;
            BenutzerID = myUserId;
            if (Ausgang.Id > 0)
            {
                Fill();
                if (myLoadArticleList)
                {
                    GetAusgangArticleData();
                }
            }
        }

        public AusgangViewData(int myAusgangID, int myUserId, int myWorkspaceId, bool myLoadArticleList) : this()
        {
            //InitCls();
            Ausgang.LAusgangID = myAusgangID;
            Ausgang.ArbeitsbereichId = myWorkspaceId;
            BenutzerID = myUserId;
            if (Ausgang.LAusgangID > 0)
            {
                GetAusgangByAusgangID();
                if (myLoadArticleList)
                {
                    GetAusgangArticleData();
                }
            }
        }
        private void InitCls()
        {
            Ausgang = new Ausgaenge();
            ListArticleInAusgang = new List<Articles>();
        }
        public void Fill()
        {
            string strSQL = sql_Get;
            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "AusgangAction", "Ausgang", BenutzerID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                }
            }
        }

        public void GetAusgangByAusgangID()
        {
            string strSQL = sql_GetAusgangByAusgangID;
            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "AusgangAction", "Ausgang", BenutzerID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                }
            }
        }
        public bool GetAusgangByLvsNr(int myLvsNr)
        {
            string strSql = string.Empty;
            strSql = sql_Get_Main;
            strSql += "INNER JOIN Artikel art on art.LAusgangTableID = a.ID ";
            strSql += "WHERE ";
            strSql += "a.Checked = 0 ";
            strSql += "and art.LVS_ID =" + myLvsNr;

            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "CheckAusgang", "Ausgang", 1);
            bool retVal = (dt.Rows.Count > 0);
            if (retVal)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                }
            }
            return retVal;
        }
        private void SetValue(DataRow row)
        {
            Ausgang = new Ausgaenge();
            int iTmp = 0;
            int.TryParse(row["ID"].ToString(), out iTmp);
            Ausgang.Id = iTmp;
            iTmp = 0;
            int.TryParse(row["LAusgangID"].ToString(), out iTmp);
            Ausgang.LAusgangID = iTmp;
            Ausgang.Datum = (DateTime)row["Datum"];
            Ausgang.Netto = (decimal)row["Netto"];
            Ausgang.Brutto = (decimal)row["Brutto"];
            iTmp = 0;
            int.TryParse(row["Auftraggeber"].ToString(), out iTmp);
            Ausgang.Auftraggeber = iTmp;
            iTmp = 0;
            int.TryParse(row["Versender"].ToString(), out iTmp);
            Ausgang.Versender = iTmp;
            iTmp = 0;
            int.TryParse(row["Empfaenger"].ToString(), out iTmp);
            Ausgang.Empfaenger = iTmp;
            iTmp = 0;
            int.TryParse(row["Entladestelle"].ToString(), out iTmp);
            Ausgang.Entladestelle = iTmp;
            Ausgang.Lieferant = row["Lieferant"].ToString();
            iTmp = 0;
            int.TryParse(row["SLB"].ToString(), out iTmp);
            Ausgang.SLB = iTmp;
            Ausgang.MAT = row["MAT"].ToString().Trim();
            Ausgang.Checked = (bool)row["Checked"];
            iTmp = 0;
            int.TryParse(row["SpedID"].ToString(), out iTmp);
            Ausgang.SpedId = iTmp;
            Ausgang.KFZ = row["KFZ"].ToString().Trim();
            iTmp = 0;
            int.TryParse(row["USER"].ToString(), out iTmp);
            Ausgang.User = iTmp;
            iTmp = 0;
            int.TryParse(row["ASN"].ToString(), out iTmp);
            Ausgang.ASN = iTmp;
            Ausgang.Info = row["Info"].ToString().Trim();
            iTmp = 0;
            int.TryParse(row["AbBereich"].ToString(), out iTmp);
            Ausgang.ArbeitsbereichId = iTmp;
            Ausgang.WorkspaceName = row["Workspace"].ToString();
            Ausgang.LfsNr = row["LfsNr"].ToString();

            iTmp = 0;
            int.TryParse(row["MandantenID"].ToString(), out iTmp);
            Ausgang.MandantenID = iTmp;

            DateTime dtTmp = clsSystem.const_DefaultDateTimeValue_Min;
            DateTime.TryParse(row["Termin"].ToString(), out dtTmp);
            Ausgang.Termin = dtTmp;
            Ausgang.DirectDelivery = (bool)row["DirectDelivery"];
            iTmp = 0;
            int.TryParse(row["neutrAuftraggeber"].ToString(), out iTmp);
            Ausgang.neutrAuftraggeber = iTmp;
            iTmp = 0;
            int.TryParse(row["neutrEmpfaenger"].ToString(), out iTmp);
            Ausgang.neutrEmpfaenger = iTmp;
            Ausgang.LagerTransport = (bool)row["LagerTransport"];
            Ausgang.WaggonNo = row["WaggonNo"].ToString().Trim();
            iTmp = 0;
            int.TryParse(row["BeladeID"].ToString(), out iTmp);
            Ausgang.BeladeId = iTmp;
            Ausgang.IsPrintDoc = (bool)row["IsPrintDoc"];
            Ausgang.IsPrintAnzeige = (bool)row["IsPrintAnzeige"];
            Ausgang.IsPrintLfs = (bool)row["IsPrintLfs"];
            iTmp = 0;
            int.TryParse(row["LockedBy"].ToString(), out iTmp);
            Ausgang.LockedBy = iTmp;
            Ausgang.IsWaggon = (bool)row["IsWaggon"];
            Ausgang.ExTransportRef = row["exTransportRef"].ToString();
            Ausgang.Fahrer = row["Fahrer"].ToString();
            Ausgang.IsRL = (bool)row["IsRL"];
            Ausgang.IsPrintList = (bool)row["IsPrintList"];
            Ausgang.Trailer = row["Trailer"].ToString().Trim();
            Ausgang.PrintActionScannerLfs = (bool)row["PrintActionScannerLfs"];
            Ausgang.PrintActionScannerAusgangsliste = (bool)row["PrintActionScannerAusgangsliste"];
            Ausgang.PrintActionScannerKVOFrachtbrief = (bool)row["PrintActionScannerKVOFrachtbrief"];

            iTmp = 0;
            int.TryParse(row["ArticleCount"].ToString(), out iTmp);
            Ausgang.ArticleCount = iTmp;

            iTmp = 0;
            int.TryParse(row["AArticleCheckedCount"].ToString(), out iTmp);
            Ausgang.ArticleCheckedCountStoreOut = iTmp;

            Ausgang.Status = enumEAStatus.loaded;

            Ausgang.AuftraggeberString = row["AdrAuftraggeber"].ToString();
            Ausgang.VersenderString = row["AdrVersender"].ToString();
            Ausgang.EmpfaengerString = row["AdrEmpfaenger"].ToString();
            Ausgang.EntladestelleString = row["AdrEnt"].ToString();
            Ausgang.Spediteur = row["AdrSpedition"].ToString();

            if (Ausgang.ArbeitsbereichId > 0)
            {
                workspaceViewData = new WorkspaceViewData(Ausgang.ArbeitsbereichId);
                Ausgang.Workspace = workspaceViewData.Workspace.Copy();
            }

            // Set DocPrint Status
            Ausgaenge tmpAusgang = PrintQueueViewData.GetPrintDocStatus_StoreOut(Ausgang);
            Ausgang.PrintDocumentStoreOutStatus_Frachtbrief = tmpAusgang.PrintDocumentStoreOutStatus_Frachtbrief;
            Ausgang.PrintDocumentStoreOutStatus_Lfs = tmpAusgang.PrintDocumentStoreOutStatus_Lfs;
            Ausgang.PrintDocumentStoreOutStatus_List = tmpAusgang.PrintDocumentStoreOutStatus_List;

        }

        public void GetOpenStoreOutList()
        {
            ListAusgaengeOpen = new List<Ausgaenge>();
            string strSQL = string.Empty;
            DataTable dt = new DataTable("Ausgänge");
            strSQL = sql_Get_open;
            dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "GetAusgang", "OpenAusgang", BenutzerID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                    ListAusgaengeOpen.Add(Ausgang);
                }
            }

        }
        private void GetAusgangArticleData()
        {
            ListArticleInAusgang = new List<Articles>();
            string strSQL = string.Empty;
            DataTable dt = new DataTable("Artikel");
            if (Ausgang.Id > 0)
            {
                strSQL = "SELECT  a.Id " +
                          " FROM Artikel a " +
                          "INNER JOIN LAusgang b ON b.ID = a.LAusgangTableID " +
                          "INNER JOIN Gueterart e ON e.ID = a.GArtID " +
                          "INNER JOIN LEingang c ON c.ID = a.LEingangTableID " + // anpassung für die Views
                          "INNER JOIN ADR d ON d.ID = c.Auftraggeber " +
                          "WHERE b.ID=" + Ausgang.Id + " ";

                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, dt.TableName.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    int iTmp = 0;
                    if (int.TryParse(dr["Id"].ToString(), out iTmp))
                    {
                        articleViewData = new ArticleViewData(iTmp, new _GL_USER());
                        ListArticleInAusgang.Add(articleViewData.Artikel.Copy());
                    }
                }
            }
        }

        /// <summary>
        ///             ADD
        /// </summary>
        public void Add()
        {
            string strSql = sql_Add;
            strSql = strSql + "Select @@IDENTITY as 'ID';";
            string strTmp = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSql, "Insert", BenutzerID);
            int.TryParse(strTmp, out int iTmp);
            this.Ausgang.Id = iTmp;
        }
        /// <summary>
        ///             DELETE
        /// </summary>
        public void Delete()
        {
            //if (Artikel.Id > 0)
            //{
            //    //ID = myArtID;
            //    //GetArtikeldatenByTableID();
            //    //decimal decTmpLagerEingangID = clsLager.GetLEingangIDByLEingangTableID(this._GL_User.User_ID, this.LEingangTableID);

            //    string strSql = string.Empty;
            //    strSql = sql_Delete;
            //    //bool mybExecOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            //    bool mybExecOK = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "DELETE",BenutzerID);
            //    //Logbuch Eintrag
            //    if (mybExecOK)
            //    {
            //        //Add Logbucheintrag 
            //        string myBeschreibung = "Artikel gelöscht: Artikel ID [" + Artikel.Id.ToString() + "] ";
            //        Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);
            //    }
            //}
        }
        /// <summary>
        ///             UPDATE
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            string strSql = sql_Update;
            bool retVal = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            //ArtikelVita
            clsArtikelVita.LagerAusgangChange(BenutzerID, this.Ausgang.Id, this.Ausgang.LAusgangID);
            // Logbucheintrag Eintrag
            string Beschreibung = "Lager Ausgang geändert: Nr [" + this.Ausgang.LAusgangID.ToString() + "] " +
                                  "/ Mandant [" + this.Ausgang.MandantenID.ToString() + "] " +
                                  "/ Arbeitsbereich [" + this.Ausgang.ArbeitsbereichId.ToString() + "]";

            //Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
            return retVal;
        }
        /// <summary>
        ///             Update - StoreOut finished
        /// </summary>
        //public bool Update_WizStoreOut_Ausgang(enumWizStoreOutSteps_Ausgang_Open myWizStoreOut_AusgangOpen, 
        //                                       enumWizStoreOutSteps_Ausgang_Call myWizStoreOut_AusgangCall,
        //                                       enumWizStoreOutSteps_Ausgang_Manually myWizStoreOut_AusgangManually,
        //                                       enumStoreOutArt myStoreOutArt)
        public bool Update_WizStoreOut_Ausgang(enumStoreOutArt myStoreOutArt, enumStoreOutArt_Steps myStoreOutArt_Steps)
        {
            string strSql = string.Empty;
            bool isLastStep = false;
            switch (myStoreOutArt)
            {
                case enumStoreOutArt.open:
                    strSql = sqlCreater_WizStoreOut_Ausgang.sql_String_StoreOut_Open(Ausgang, myStoreOutArt_Steps);
                    isLastStep = myStoreOutArt_Steps.Equals(enumStoreOutArt_Steps.wizStepLast);
                    break;
                case enumStoreOutArt.call:
                    //strSql = sqlCreater_WizStoreOut_Ausgang.sql_String_StoreOut_Call(Ausgang, myWizStoreOut_AusgangCall);
                    //isLastStep = myWizStoreOut_AusgangCall.Equals(enumWizStoreOut_Ausgang_Call.wizStepLast);
                    break;
                case enumStoreOutArt.manually:
                    //strSql = sqlCreater_WizStoreOut_Ausgang.sql_String_StoreOut_Manually(Ausgang, myWizStoreOut_AusgangManually);
                    //isLastStep = myWizStoreOut_AusgangManually.Equals(enumWizStoreOut_Ausgang_Call.wizStepLast);
                    break;
            }
            //bool retVal = Update_WizStoreOut(strSql, isLastStep);
            bool retVal = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "AusgangUpdate", BenutzerID);
            if (retVal)
            {
                //ArtikelVita
                clsArtikelVita.LagerAusgangChange(BenutzerID, this.Ausgang.Id, this.Ausgang.LAusgangID);

                if ((isLastStep) && (Ausgang.Checked))
                {
                    LVS.Globals._GL_USER GLUser = new _GL_USER();
                    GLUser.User_ID = BenutzerID;

                    _GL_SYSTEM GLSystem = new _GL_SYSTEM();
                    clsSystem Sys = new clsSystem();

                    //-- ASN / EDI Kommunication
                    if (Ausgang.Workspace.ASNTransfer)
                    {
                        //LVS.Globals._GL_USER GLUser = new _GL_USER();
                        //GLUser.User_ID = BenutzerID;
                        WarehouseViewData whVD = new WarehouseViewData(0, 0, Ausgang.Id, BenutzerID, Ausgang.Workspace, clsASNAction.const_ASNAction_Ausgang);
                        clsLager Lager = new clsLager(whVD);
                        clsASNTransfer AsnTransfer = new clsASNTransfer();
                        AsnTransfer.IsCreateByASNMessageTestCtr = false;
                        AsnTransfer.CreateLM(ref Lager);
                    }
                }
            }
            return retVal;
        }

        /// <summary>
        ///             check for id 
        /// </summary>
        /// <returns></returns>
        public bool ExistLAusgangTableID()
        {
            if (Ausgang.Id > 0)
            {
                string strSql = string.Empty;
                strSql = "Select * FROM LAusgang WHERE ID='" + Ausgang.Id + "'";
                return clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            }
            else
            {
                return false;
            }
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
                this.Ausgang.MAT = this.Ausgang.Datum.ToString();
                string strSql = string.Empty;
                strSql = clsPrimeKeys.GetNEWLAusgangIDSQL(this.Ausgang.MandantenID, this.Ausgang.ArbeitsbereichId, 0) +
                         " INSERT INTO LAusgang (LAusgangID, Datum, Netto, Brutto, Auftraggeber, Versender, Empfaenger, " +
                                               "Entladestelle, Lieferant, LfsNr, SLB, MAT, Checked, " +
                                               "SpedID, KFZ, [USER], ASN, Info, AbBereich, MandantenID, Termin, DirectDelivery, IsPrintDoc,ExTransportRef, " +
                                               "LagerTransport, WaggonNo, BeladeID, Fahrer, IsRL) " +
                                                "VALUES (( " + clsPrimeKeys.GetNEWLAusgangIDSQL(this.Ausgang.MandantenID, this.Ausgang.ArbeitsbereichId, 1) + ")" +
                                                        ",'" + this.Ausgang.Datum + "'" +
                                                        ",'" + this.Ausgang.Netto.ToString().Replace(",", ".") + "'" +
                                                        ",'" + this.Ausgang.Brutto.ToString().Replace(",", ".") + "'" +
                                                        "," + this.Ausgang.Auftraggeber +
                                                        "," + this.Ausgang.Versender +
                                                        "," + this.Ausgang.Empfaenger +
                                                        "," + this.Ausgang.Entladestelle +
                                                        ",'" + this.Ausgang.Lieferant + "'" +
                                                        ",'" + this.Ausgang.LfsNr + "'" +
                                                        ",'" + this.Ausgang.SLB + "'" +
                                                        ",'" + this.Ausgang.MAT + "'" +
                                                        ",'" + Convert.ToBoolean(this.Ausgang.Checked) + "'" +
                                                        "," + this.Ausgang.SpedId +
                                                        ",'" + this.Ausgang.KFZ + "'" +
                                                        ", " + this.Ausgang.User +
                                                        "," + this.Ausgang.ASN +
                                                        ",'" + this.Ausgang.Info + "'" +
                                                        "," + this.Ausgang.ArbeitsbereichId +
                                                        "," + this.Ausgang.MandantenID +
                                                        ",'" + this.Ausgang.Termin + "'" +
                                                        ",'" + Convert.ToBoolean(this.Ausgang.DirectDelivery) + "'" +
                                                        ", " + Convert.ToInt32(this.Ausgang.IsPrintDoc) +
                                                        ", '" + this.Ausgang.ExTransportRef + "'" +
                                                        ",'" + this.Ausgang.LagerTransport + "'" +
                                                        ",'" + this.Ausgang.WaggonNo + "' " +
                                                        ", " + this.Ausgang.BeladeId +
                                                        ", '" + this.Ausgang.Fahrer + "'" +
                                                         ", " + Convert.ToInt32(this.Ausgang.IsRL) +
                                                        "); ";
                return strSql;
            }
        }
        /// <summary>
        ///             GET
        /// </summary>
        public string sql_Get_Main
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Select a.* " +
                            ", (SELECT Count(Id) FROM Artikel where LAusgangTableID = a.Id) as ArticleCount " +
                            ", (SELECT Count(Id) FROM Artikel where LAusgangTableID = a.Id and LA_Checked = 1) as AArticleCheckedCount " +
                            ", (SELECT Name FROM Arbeitsbereich where ID = a.AbBereich) as Workspace " +
                            ", (SELECT ViewID + ' - ' + PLZ + ' ' + ORT  FROM ADR WHERE ID = a.Auftraggeber) as AdrAuftraggeber " +
                            ", (SELECT ViewID + ' - ' + PLZ + ' ' + ORT  FROM ADR WHERE ID = a.Versender) as AdrVersender " +
                            ", (SELECT ViewID + ' - ' + PLZ + ' ' + ORT  FROM ADR WHERE ID = a.Empfaenger) as AdrEmpfaenger " +
                            ", (SELECT ViewID + ' - ' + PLZ + ' ' + ORT  FROM ADR WHERE ID = a.Entladestelle) as AdrEnt " +
                            ", (SELECT ViewID + ' - ' + PLZ + ' ' + ORT  FROM ADR WHERE ID = a.SpedID) as AdrSpedition " +
                            " FROM LAusgang a ";
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
                string strSql = this.sql_Get_Main + " WHERE a.ID = " + Ausgang.Id + "; ";
                return strSql;
            }
        }

        public string sql_GetAusgangByAusgangID
        {
            get
            {
                string strSql = this.sql_Get_Main + " WHERE a.LAusgangID = " + Ausgang.LAusgangID + " and a.AbBereich=" + Ausgang.ArbeitsbereichId;
                return strSql;
            }
        }

        public string sql_GetAusgangByLvsNr
        {
            get
            {
                string strSql = this.sql_Get_Main + " WHERE a.LAusgangID = " + Ausgang.LAusgangID + " and a.AbBereich=" + workspaceViewData.Workspace.Id;
                return strSql;
            }
        }

        /// <summary>
        ///             GET
        /// </summary>
        public string sql_Get_open
        {
            get
            {
                string strSql = this.sql_Get_Main + " WHERE a.checked= 0 ";

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
                string strSql = "Delete FROM LAusgang WHERE ID =" + Ausgang.Id; ;
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
                strSql = "Update LAusgang ";
                return strSql;
            }
        }

        public bool PrintDocuments()
        {
            bool bRet = false;

            if (
                (Ausgang.Workspace is Workspaces) &&
                (Ausgang.Workspace.Id > 0) &&
                (Ausgang.Workspace.IsActiv) &&
                (Ausgang.Workspace.ASNTransfer)
              )
            {
                LVS.Globals._GL_USER GLUser = new _GL_USER();
                GLUser.User_ID = BenutzerID;
                WarehouseViewData whVD = new WarehouseViewData(0, 0, Ausgang.Id, BenutzerID, Ausgang.Workspace, clsASNAction.const_ASNAction_Ausgang);
                clsLager Lager = new clsLager(whVD);
                clsASNTransfer AsnTransfer = new clsASNTransfer();
                AsnTransfer.IsCreateByASNMessageTestCtr = true; // nur für die Test
                AsnTransfer.CreateLM(ref Lager);

            }


            return bRet;
        }




    }
}

