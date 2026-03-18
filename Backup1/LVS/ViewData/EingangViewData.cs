using Common.ApiModels;
using Common.Enumerations;
using Common.Helper;
using Common.Models;
using LVS.Constants;
using LVS.CustomProcesses;
using LVS.Models;
using LVS.sqlStatementCreater;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static LVS.Globals;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class EingangViewData
    {

        private Eingaenge _Eingang;
        public Eingaenge Eingang
        {
            get
            {
                return _Eingang;
            }
            set
            {
                _Eingang = value;
            }
        }
        private int BenutzerID { get; set; } = 0;
        public ArticleViewData articleViewData { get; set; }
        public WorkspaceViewData workspaceViewData { get; set; }
        //public clsLEingang Eingang { get; set; }

        public List<Articles> ListArticleInEingang { get; set; } = new List<Articles>();
        public List<Eingaenge> ListEingaengeOpen { get; set; }

        private int PrintCount { get; set; } = 1;
        private string PrinterName { get; set; } = string.Empty;
        public string Info { get; set; } = string.Empty;
        public sqlCreater_Eingang sqlCreater_WizStoreIn_Eingang;
        public EingangViewData()
        {
            InitCls();
        }
        public EingangViewData(Eingaenge myEingang, int myUserId) : this()
        {
            Eingang = myEingang.Copy();
            BenutzerID = myUserId;
        }
        public EingangViewData(Eingaenge myEingang, int myUserId, bool myLoadArticleList) : this()
        {
            Eingang = myEingang.Copy();
            BenutzerID = myUserId;
            if (myLoadArticleList)
            {
                GetEingangArticleData();
            }
        }
        public EingangViewData(ResponseEingang myResponceEingang) : this()
        {
            Eingang = myResponceEingang.Eingang.Copy();
            BenutzerID = myResponceEingang.UserId;
            PrintCount = myResponceEingang.PrintCount;
            PrinterName = myResponceEingang.PrinterName;
        }
        public EingangViewData(int myId, int myUserId, bool myLoadArticleList) : this()
        {
            Eingang.Id = myId;
            BenutzerID = myUserId;
            if (Eingang.Id > 0)
            {
                Fill();
                if (myLoadArticleList)
                {
                    GetEingangArticleData();
                }
            }
        }

        public EingangViewData(int myLEingangId, int myUserId, int myWorkspaceId, bool myLoadArticleList) : this()
        {
            Eingang.LEingangID = myLEingangId;
            Eingang.ArbeitsbereichId = myWorkspaceId;
            BenutzerID = myUserId;
            if (Eingang.LEingangID > 0)
            {
                GetEingangByLEingangId();
                if (myLoadArticleList)
                {
                    GetEingangArticleData();
                }
            }
        }
        private void InitCls()
        {
            Eingang = new Eingaenge();
            sqlCreater_WizStoreIn_Eingang = new sqlCreater_Eingang(Eingang);
        }
        public void Fill()
        {
            string strSQL = sql_GetById;
            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "EingangAction", "Eingang", BenutzerID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void GetEingangByLEingangId()
        {
            string strSQL = sql_GetByLEingangID;
            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "EingangAction", "Eingang", BenutzerID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                }
            }
        }
        /// <summary>
        ///             Ermittlung aller Eingänge zu einer ASN
        /// </summary>
        /// <param name="myAsn"></param>
        /// <returns></returns>
        public List<Eingaenge> GetEingangByASn(Asn myAsn)
        {
            List<Eingaenge> retList = new List<Eingaenge>();
            string strSql = this.sql_Get_Main + " WHERE e.Asn = " + myAsn.Id + " ";
            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "Eingang", "Eingang", BenutzerID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Eingang = new Eingaenge();
                    SetValue(dr);
                    if (!retList.Contains(Eingang))
                    {
                        retList.Add(Eingang);
                    }
                }
            }
            return retList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        private void SetValue(DataRow row)
        {
            Eingang = new Eingaenge();
            int iTmp = 0;
            int.TryParse(row["ID"].ToString(), out iTmp);
            Eingang.Id = iTmp;
            iTmp = 0;
            int.TryParse(row["LEingangID"].ToString(), out iTmp);
            Eingang.LEingangID = iTmp;
            Eingang.Eingangsdatum = (DateTime)row["Date"];
            iTmp = 0;
            int.TryParse(row["Auftraggeber"].ToString(), out iTmp);
            Eingang.Auftraggeber = iTmp;
            iTmp = 0;
            int.TryParse(row["Empfaenger"].ToString(), out iTmp);
            Eingang.Empfaenger = iTmp;
            Eingang.Lieferant = row["Lieferant"].ToString();
            iTmp = 0;
            int.TryParse(row["AbBereich"].ToString(), out iTmp);
            Eingang.ArbeitsbereichId = iTmp;
            iTmp = 0;
            int.TryParse(row["Mandant"].ToString(), out iTmp);
            Eingang.MandantenId = iTmp;
            Eingang.LfsNr = row["LfsNr"].ToString();
            iTmp = 0;
            int.TryParse(row["ASN"].ToString(), out iTmp);
            Eingang.ASN = iTmp;
            iTmp = 0;
            int.TryParse(row["Versender"].ToString(), out iTmp);
            Eingang.Versender = iTmp;
            Eingang.Check = (bool)row["Check"];
            iTmp = 0;
            int.TryParse(row["SpedID"].ToString(), out iTmp);
            Eingang.SpedId = iTmp;
            Eingang.KFZ = row["KFZ"].ToString().Trim();
            Eingang.DirektDelivery = (bool)row["DirectDelivery"];
            Eingang.Retoure = (bool)row["Retoure"];
            Eingang.Vorfracht = (bool)row["Vorfracht"];
            Eingang.LagerTransport = (bool)row["LagerTransport"];
            Eingang.WaggonNr = row["WaggonNo"].ToString().Trim();
            iTmp = 0;
            int.TryParse(row["BeladeID"].ToString(), out iTmp);
            Eingang.BeladeID = iTmp;
            iTmp = 0;
            int.TryParse(row["EntladeID"].ToString(), out iTmp);
            Eingang.EntladeID = iTmp;
            Eingang.IsPrintDoc = (bool)row["IsPrintDoc"];
            Eingang.IsPrintAnzeige = (bool)row["IsPrintAnzeige"];
            Eingang.LagerTransport = (bool)row["LagerTransport"];
            Eingang.IsPrintLfs = (bool)row["IsPrintLfs"];
            Eingang.WorkspaceName = row["WorkspaceName"].ToString();

            Eingang.ExAuftragRef = row["ExAuftragRef"].ToString();
            Eingang.ExTransportRef = row["ExTransportRef"].ToString();
            iTmp = 0;
            int.TryParse(row["lockedBy"].ToString(), out iTmp);
            Eingang.LockedBy = iTmp;
            Eingang.IsWaggon = (bool)row["IsWaggon"];
            Eingang.Fahrer = row["Fahrer"].ToString();
            Eingang.IsPrintList = (bool)row["IsPrintList"];
            Eingang.Ship = row["Ship"].ToString();
            Eingang.IsShip = (bool)row["IsShip"];
            Eingang.Verlagerung = (bool)row["Verlagerung"];
            Eingang.Umbuchung = (bool)row["Umbuchung"];
            Eingang.PrintActionByScanner = (bool)row["PrintActionByScanner"];
            Eingang.PrintActionScannerAllLable = (bool)row["PrintActionScannerAllLable"];
            Eingang.PrintActionScannerEingangsliste = (bool)row["PrintActionScannerEingangsliste"];
            iTmp = 0;
            int.TryParse(row["ArticleCount"].ToString(), out iTmp);
            Eingang.ArticleCount = iTmp;
            iTmp = 0;
            int.TryParse(row["EArticleCheckedCount"].ToString(), out iTmp);
            Eingang.ArticleCheckedCountStoreIn = iTmp;
            Eingang.CreatedByScanner = (bool)row["CreatedByScanner"];
            Eingang.Status = enumEAStatus.loaded;
            Eingang.AuftraggeberString = row["AdrAuftraggeber"].ToString();
            Eingang.VersenderString = row["AdrVersender"].ToString();
            Eingang.EmpfaengerString = row["AdrEmpfaenger"].ToString();
            Eingang.EntladestelleString = row["AdrEnt"].ToString();
            Eingang.BeladestelleString = row["AdrBelade"].ToString();
            Eingang.Spediteur = row["AdrSpedition"].ToString();
            if (Eingang.ArbeitsbereichId > 0)
            {
                workspaceViewData = new WorkspaceViewData(Eingang.ArbeitsbereichId);
                Eingang.Workspace = workspaceViewData.Workspace.Copy();
            }

            ///--rest aus clsArtikel in LVS
            //ListArtWithSchaden = new List<decimal>();
            //ListArtWithSchaden = clsSchaeden.GetArtikelWithSchaden(this._GL_User, this.LEingangTableID);

            //ListArtInSPL = new List<decimal>();
            //ListArtInSPL = clsSPL.GetArtikelEingangInSPL(this._GL_User, this.LEingangTableID);

            //FirstCheckDateTime = clsArtikelVita.GetFirstDateTimeLEingangChecked(this._GL_User, this.LEingangTableID);

        }

        public void GetOpenStoreOutList()
        {
            ListEingaengeOpen = new List<Eingaenge>();
            string strSQL = string.Empty;
            DataTable dt = new DataTable("Eingänge");
            strSQL = sql_Get_open;
            dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "GetEingang", "OpenEingang", BenutzerID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                    ListEingaengeOpen.Add(Eingang);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void GetEingangArticleData()
        {
            ListArticleInEingang = new List<Articles>();
            string strSQL = string.Empty;
            DataTable dt = new DataTable("Artikel");
            if (Eingang.Id > 0)
            {
                strSQL = "SELECT  a.Id " +
                          " FROM Artikel a " +
                          "INNER JOIN LEingang c ON c.ID = a.LEingangTableID " +
                          "INNER JOIN Gueterart e ON e.ID = a.GArtID " +
                          // anpassung für die Views
                          "INNER JOIN ADR d ON d.ID = c.Auftraggeber " +
                          "WHERE c.ID=" + Eingang.Id + " ";

                dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "EingangArtikel", "ArticleEingang", BenutzerID);

                foreach (DataRow dr in dt.Rows)
                {
                    int iTmp = 0;
                    if (int.TryParse(dr["Id"].ToString(), out iTmp))
                    {
                        articleViewData = new ArticleViewData(iTmp, BenutzerID, true);
                        ListArticleInEingang.Add(articleViewData.Artikel.Copy());
                    }
                }
            }
        }

        public string GetSupplierNo()
        {
            string strReturn = string.Empty;
            strReturn = clsADRVerweis.GetSupplierNoBySenderAndReceiverAdr(Eingang.Auftraggeber, Eingang.Empfaenger, BenutzerID, constValue_AsnArt.const_Art_VDA4913, Eingang.ArbeitsbereichId);
            Eingang.Lieferant = strReturn;
            return strReturn;
        }

        /// <summary>
        ///             ADD
        /// </summary>
        public bool AddByScanner()
        {
            bool bAddOK = false;
            string strSql = sql_Add;
            strSql = strSql + "Select @@IDENTITY as 'ID';";
            string strTmp = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSql, "Insert", BenutzerID);
            int.TryParse(strTmp, out int iTmp);
            Eingang.Id = iTmp;
            if (Eingang.Id > 0)
            {
                Fill();
                //ArtikelVita
                clsArtikelVita.AddEinlagerungManualByScanner(BenutzerID, Eingang.Id, Eingang.LEingangID);
                //Add Logbucheintrag Eintrag
                string Beschreibung = "Lager Eingang erstellt: Nr [" + Eingang.LEingangID.ToString() + "] / Mandant [" + Eingang.MandantenId.ToString() + "] / Arbeitsbereich [" + Eingang.ArbeitsbereichId.ToString() + "]";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), Beschreibung);
                bAddOK = true;
            }
            return bAddOK;
        }
        /// <summary>
        ///             DELETE
        /// </summary>
        public void Delete()
        {
            if ((this.Eingang is Eingaenge) && (this.Eingang.Id > 0))
            {
                string strSql = string.Empty;
                strSql = sql_Delete;
                bool mybExecOK = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "DELETE", BenutzerID);
                //Logbuch Eintrag
                if (mybExecOK)
                {
                    //Add Logbucheintrag 
                    string myBeschreibung = "Eingang gelöscht Eingang " + Eingang.LEingangID + "[" + Eingang.Id + "] ";
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);
                }
            }
        }
        /// <summary>
        ///             Update - StoreIn for SCANNER 
        /// </summary>
        public bool Update_WizStoreIN(ResponseEingang resEA)
        {
            bool bReturn = false;
            string strSql = string.Empty;
            bool isLastStep = false;
            switch (resEA.StoreInArt)
            {
                case enumStoreInArt.open:
                    strSql = Common.SqlStatementCreater.sqlCreater_WizStoreIn_Eingang.sql_String_Update_StoreIn_Open(Eingang, resEA.StoreInArt_Steps);
                    isLastStep = resEA.StoreInArt_Steps.Equals(enumStoreInArt_Steps.wizStepLastCheckComplete);
                    break;

                case enumStoreInArt.edi:
                    strSql = Common.SqlStatementCreater.sqlCreater_WizStoreIn_Eingang.sql_String_Update_StoreIn_Edi(Eingang, resEA.StoreInArt_Steps);
                    isLastStep = resEA.StoreInArt_Steps.Equals(enumStoreInArt_Steps.wizStepLastCheckComplete);
                    break;
                case enumStoreInArt.manually:
                    strSql = Common.SqlStatementCreater.sqlCreater_WizStoreIn_Eingang.sql_String_Update_StoreIn_Open(Eingang, resEA.StoreInArt_Steps);
                    isLastStep = resEA.StoreInArt_Steps.Equals(enumStoreInArt_Steps.wizStepLastCheckComplete);
                    break;
            }
            if (strSql.Length > 0)
            {
                bReturn = Update_WizStoreIn(strSql, isLastStep, resEA);
            }
            return bReturn;
        }

        /// <summary>
        ///             UPDATE
        /// </summary>
        /// <returns></returns>
        public bool Update_WizStoreIn(string mySql, bool myIsLastStep, ResponseEingang resEA)
        {
            bool retVal = false;
            //EingangViewData eVD = new EingangViewData(Eingang.Id, 1, false);
            EingangViewData eVD = new EingangViewData(Eingang.Id, 1, true);
            Eingaenge eingangOriginal = eVD.Eingang.Copy();
            try
            {
                retVal = clsSQLcon.ExecuteSQLWithTRANSACTION(mySql, "EingangUpdate", BenutzerID);
            }
            catch (Exception ex)
            {
                this.Info += "Klasse: EingangViewData| public bool Update_WizStoreIn(string mySql, bool myIsLastStep, ResponseEingang resEA)" + Environment.NewLine;
                this.Info += "Fehler beim Update des Eingangs: " + Environment.NewLine;
                this.Info += ex.Message;
                return false;
            }

            if (retVal)
            {
                switch (resEA.StoreInArt)
                {
                    case enumStoreInArt.edi:
                    case enumStoreInArt.open:
                        //ArtikelVita
                        Eingang_ComparisonClass compare = new Eingang_ComparisonClass(Eingang, eingangOriginal);
                        if (!compare.LEingangChangingText.Equals(string.Empty))
                        {
                            clsArtikelVita.LagerEingangChangeBySanner(BenutzerID, Eingang.Id, Eingang.LEingangID, compare.LEingangChangingText);
                        }
                        break;
                }

                //---- Check ob Auftraggeber und oder Empfänger geändert wurden
                if (
                        (!Eingang.Auftraggeber.Equals(eingangOriginal.Auftraggeber))
                        ||
                        (!Eingang.Empfaenger.Equals(eingangOriginal.Empfaenger))
                  )
                {
                    string strSupplierNo = AddressViewData.GetSupplierNo(Eingang.Auftraggeber, Eingang.Empfaenger, Eingang.Workspace.Id);
                    if (!Eingang.Lieferant.Equals(strSupplierNo))
                    {
                        Update_Datafield_Lieferant(strSupplierNo);
                    }
                }


                //--- neu 12.06.2024 mr
                LVS.Globals._GL_USER GLUser = new _GL_USER();
                GLUser.User_ID = BenutzerID;
                _GL_SYSTEM GLSystem = new _GL_SYSTEM();
                clsSystem System = new clsSystem();

                CustomProcessesViewData cVD = new CustomProcessesViewData(Eingang.Auftraggeber, Eingang.ArbeitsbereichId, GLUser);
                if (cVD.ExistCustomProcess)
                {
                    if (cVD.CheckAndExecuteCustomProcess(0, Eingang.Id, 0, CustomProcess_Novelis_AccessByArticleCert.const_ProcessLocation_EingangViewData_Update_WizStoreIn_CheckEingangElementsToFinish))
                    {
                        bool IsComplete = cVD.Process_Novelis_AccessByArticleCert.IsArticleZertifacteProcessComplete;

                        if (!Eingang.Check.Equals(IsComplete))
                        {
                            Eingang.Check = IsComplete;
                            this.Update_Datafield_Check(Eingang.Check);
                            if (!Eingang.Check)
                            {
                                this.Info = cVD.Process_Novelis_AccessByArticleCert.ProcessViewName + ":" + Environment.NewLine;
                                this.Info += "Eingang kann noch nicht abgeschlossen werden, da das Zertifikat noch nicht vorliegt!";
                            }
                        }
                    }
                }

                if (Eingang.Check)
                {
                    //-- ASN / EDI Kommunication
                    if (Eingang.Workspace.ASNTransfer)
                    {
                        //WarehouseViewData whVD = new WarehouseViewData(0, 0, Ausgang.Id, BenutzerID, Ausgang.Workspace, clsASNAction.const_ASNAction_Ausgang);
                        WarehouseViewData whVD = new WarehouseViewData(0, Eingang.Id, 0, BenutzerID, Eingang.Workspace, clsASNAction.const_ASNAction_Eingang);

                        clsLager Lager = new clsLager(whVD);
                        clsASNTransfer AsnTransfer = new clsASNTransfer();
                        AsnTransfer.IsCreateByASNMessageTestCtr = true; // nur für die Test
                        AsnTransfer.CreateLM(ref Lager);
                    }
                }

                if (myIsLastStep)
                {
                    clsReportDocSetting ReportDocSetting = new clsReportDocSetting();
                    ReportDocSetting.InitClass(GLUser, GLSystem, System, Eingang.Auftraggeber, Eingang.ArbeitsbereichId);
                    clsReportDocSetting tmpSetting = new clsReportDocSetting();

                    PrintQueues tmpPrint = new PrintQueues();
                    PrintQueueViewData vd = new PrintQueueViewData();
                    //-- PrintQueue
                    // Print All Label
                    if (Eingang.PrintActionScannerAllLable)
                    {
                        enumDokumentenArt PrintDocumentArt = enumDokumentenArt.LabelAll;
                        if (Eingang.Auftraggeber > 0)
                        {
                            //tmpSetting = ReportDocSetting.ListReportDocSettingAll.FirstOrDefault(x => x.AdrID == Eingang.Auftraggeber);
                            tmpSetting = ReportDocSetting.ListReportDocEingang.FirstOrDefault(x => x.AdrID == Eingang.Auftraggeber);
                        }
                        if (tmpSetting is null)
                        {
                            //tmpSetting = ReportDocSetting.ListReportDocSettingAll.FirstOrDefault(x => x.DocKey == PrintDocumentArt.ToString());
                            tmpSetting = ReportDocSetting.ListReportDocEingang.FirstOrDefault(x => x.DocKey == PrintDocumentArt.ToString());
                        }

                        tmpPrint = new PrintQueues();
                        tmpPrint.ReportDocSettingId = tmpSetting.ID;
                        tmpPrint.ReportDocSettingAssignmentId = tmpSetting.RSAId;
                        tmpPrint.IsActiv = true;
                        tmpPrint.TableName = enumDatabaseSped4_TableNames.LEingang.ToString();
                        tmpPrint.TableId = Eingang.Id;
                        tmpPrint.WorkspaceId = Eingang.Workspace.Id;
                        tmpPrint.PrintCount = this.PrintCount;
                        tmpPrint.PrinterName = this.PrinterName;

                        vd = new PrintQueueViewData();
                        vd.PrintQueue = tmpPrint.Copy();
                        vd.Add();

                    }
                    if (Eingang.PrintActionScannerEingangsliste)
                    {
                        enumDokumentenArt PrintDocumentArt = enumDokumentenArt.Eingangsliste;
                        if (Eingang.Auftraggeber > 0)
                        {
                            //tmpSetting = ReportDocSetting.ListReportDocSettingAll.FirstOrDefault(x => x.AdrID == Eingang.Auftraggeber);
                            tmpSetting = ReportDocSetting.ListReportDocEingang.FirstOrDefault(x => x.AdrID == Eingang.Auftraggeber);
                        }
                        if (tmpSetting is null)
                        {
                            //tmpSetting = ReportDocSetting.ListReportDocSettingAll.FirstOrDefault(x => x.DocKey == PrintDocumentArt.ToString());
                            tmpSetting = ReportDocSetting.ListReportDocEingang.FirstOrDefault(x => x.DocKey == PrintDocumentArt.ToString());
                        }

                        tmpPrint = new PrintQueues();
                        tmpPrint.ReportDocSettingId = tmpSetting.ID;
                        tmpPrint.ReportDocSettingAssignmentId = tmpSetting.RSAId;
                        tmpPrint.IsActiv = true;
                        tmpPrint.TableName = enumDatabaseSped4_TableNames.LEingang.ToString();
                        tmpPrint.TableId = Eingang.Id;
                        tmpPrint.WorkspaceId = Eingang.Workspace.Id;
                        tmpPrint.PrintCount = this.PrintCount;
                        tmpPrint.PrinterName = this.PrinterName;

                        vd = new PrintQueueViewData();
                        vd.PrintQueue = tmpPrint.Copy();
                        vd.Add();
                    }
                }
            }
            return retVal;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myLieferant"></param>
        /// <returns></returns>
        public bool Update_Datafield_Lieferant(string myLieferant)
        {
            string strSql = string.Empty;
            strSql = "Update LEingang SET Lieferant='" + myLieferant + "' where Id=" + Eingang.Id;
            bool bReturn = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "EingangUpdate", BenutzerID);
            return bReturn;
        }
        public bool Update_Datafield_Check(bool myCheck)
        {
            string strSql = string.Empty;
            strSql = "Update LEingang SET [Check] = " + Convert.ToInt32(myCheck) + " where Id=" + Eingang.Id;
            bool bReturn = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "EingangUpdate", BenutzerID);
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
                string strSql = string.Empty;
                //Eingang.Lieferant = clsADRVerweis.GetSupplierNoBySenderAndReceiverAdr(Eingang.Auftraggeber, Eingang.Empfaenger, BenutzerID, constValue_AsnArt.const_Art_VDA4913, Eingang.ArbeitsbereichId);

                strSql = string.Empty;

                strSql =
                       //" BEGIN TRANSACTION " +
                       clsPrimeKeys.GetNEWLEingangIDSQL(Eingang.MandantenId, Eingang.ArbeitsbereichId, 0) +
                       " INSERT INTO LEingang (LEingangID, Date, Auftraggeber, Empfaenger, Lieferant, AbBereich, Mandant, LfsNr, " +
                                               "ASN, Versender, SpedID, KFZ, DirectDelivery, Retoure, Vorfracht, LagerTransport, " +
                                               "WaggonNo, BeladeID, EntladeID, ExTransportRef, ExAuftragRef, ASNRef, IsWaggon, " +
                                               "Fahrer , Ship, IsShip, Verlagerung, Umbuchung, CreatedByScanner" +
                                              ") " +
                                               "VALUES (( " + clsPrimeKeys.GetNEWLEingangIDSQL(Eingang.MandantenId, Eingang.ArbeitsbereichId, 1) + ")" +
                                                       ", '" + Eingang.Eingangsdatum + "'" +
                                                       ", " + Eingang.Auftraggeber +
                                                       ", " + Eingang.Empfaenger +
                                                       ", '" + Eingang.Lieferant + "'" +
                                                       ", " + Eingang.ArbeitsbereichId +
                                                       ", " + Eingang.MandantenId +
                                                       ", '" + Eingang.LfsNr + "'" +
                                                       ", " + Eingang.ASN +
                                                       ", " + Eingang.Versender +
                                                       ", " + Eingang.SpedId +
                                                       ", '" + Eingang.KFZ + "'" +
                                                       ", " + Convert.ToInt32(Eingang.DirektDelivery) +
                                                       ", " + Convert.ToInt32(Eingang.Retoure) +
                                                       ", " + Convert.ToInt32(Eingang.Vorfracht) +
                                                       ", " + Convert.ToInt32(Eingang.LagerTransport) +
                                                       ", '" + Eingang.WaggonNr + "'" +
                                                       ", " + Eingang.BeladeID +
                                                       ", " + Eingang.EntladeID +
                                                       ", '" + Eingang.ExTransportRef + "'" +
                                                       ", '" + Eingang.ExAuftragRef + "'" +
                                                       ", '" + Eingang.ASNRef + "'" +
                                                       ", " + Convert.ToInt32(Eingang.IsWaggon) +
                                                       ", '" + Eingang.Fahrer + "'" +
                                                       ", '" + Eingang.Ship + "'" +
                                                       ", " + Convert.ToInt32(Eingang.IsShip) +
                                                       ", " + Convert.ToInt32(Eingang.Verlagerung) +
                                                       ", " + Convert.ToInt32(Eingang.Umbuchung) +
                                                       ", " + Convert.ToInt32(Eingang.CreatedByScanner) +
                                                       ");";
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
                strSql = "Select e.* " +
                                 ", (SELECT Count(a.Id) FROM Artikel a where a.LEingangTableID = e.Id) as ArticleCount " +
                                 ", (SELECT Count(a.Id) FROM Artikel a where a.LEingangTableID = e.Id and a.[CheckArt] = 1) as EArticleCheckedCount " +
                                 ", (SELECT [Name] FROM Arbeitsbereich where ID = e.AbBereich) as WorkspaceName " +
                                 ", (SELECT ViewID + ' - ' + PLZ + ' ' + ORT  FROM ADR WHERE ID = e.Auftraggeber) as AdrAuftraggeber " +
                                 ", (SELECT ViewID + ' - ' + PLZ + ' ' + ORT  FROM ADR WHERE ID = e.Versender) as AdrVersender " +
                                 ", (SELECT ViewID + ' - ' + PLZ + ' ' + ORT  FROM ADR WHERE ID = e.Empfaenger) as AdrEmpfaenger " +
                                 ", (SELECT ViewID + ' - ' + PLZ + ' ' + ORT  FROM ADR WHERE ID = e.EntladeID) as AdrEnt " +
                                 ", (SELECT ViewID + ' - ' + PLZ + ' ' + ORT  FROM ADR WHERE ID = e.BeladeID) as AdrBelade " +
                                 ", (SELECT ViewID + ' - ' + PLZ + ' ' + ORT  FROM ADR WHERE ID = e.SpedID) as AdrSpedition " +
                                 "FROM LEingang e ";
                return strSql;
            }
        }
        /// <summary>
        ///             GET
        /// </summary>
        public string sql_GetById
        {
            get
            {
                string strSql = this.sql_Get_Main + "WHERE e.ID = " + Eingang.Id + " ";
                return strSql;
            }
        }

        /// <summary>
        ///             GET
        /// </summary>
        public string sql_GetByLEingangID
        {
            get
            {
                string strSql = this.sql_Get_Main + "WHERE e.LEingangID = " + Eingang.LEingangID + " and e.AbBereich=" + Eingang.ArbeitsbereichId;
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
                string strSql = this.sql_Get_Main + " WHERE e.[Check] = 0 and e.LEingangID>0 ";
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
                sqlCreater_WizStoreIn_Eingang = new sqlCreater_Eingang(Eingang);
                string strSql = sqlCreater_WizStoreIn_Eingang.DeleteById;
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
                strSql = "Update LEingang ";
                return strSql;
            }
        }

        public string sql_Update_Datafield_Lieferant
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Update LEingang SET Lieferant ";
                return strSql;
            }
        }



    }
}

