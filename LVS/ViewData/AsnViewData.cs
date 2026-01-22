using Common.Models;
using LVS.Communicator.EdiVDA;
using LVS.Constants;
using LVS.Converter;
using LVS.Models;
using LVS.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class AsnViewData
    {
        internal clsSystem system { get; set; } = new clsSystem();
        public WorkspaceViewData workspaceVD { get; set; }
        public Asn asnHead { get; set; }
        private int BenutzerID { get; set; } = 1;
        public DataTable dtAsnValue { get; set; } = new DataTable();

        public AsnValueViewData AsnValueVD { get; set; } = new AsnValueViewData();

        public List<ctrASNRead_AsnEdifactView> List_ctrAsnRead_AsnEdifactView { get; set; } = new List<ctrASNRead_AsnEdifactView>();
        public List<ctrASNRead_AsnArticleEdifactView> List_ctrAsnRead_AsnArticelEdifactView { get; set; } = new List<ctrASNRead_AsnArticleEdifactView>();
        public List<Asn> ListAsn { get; set; } = new List<Asn>();

        public event Action<int> ProgressMaxValue;

        public AsnViewData()
        {
            InitCls();
        }
        public AsnViewData(Asn myAsn) : this()
        {
            this.asnHead = myAsn;
        }
        public AsnViewData(int myWorkspaceId) : this()
        {
            workspaceVD = new WorkspaceViewData(myWorkspaceId);

        }
        public AsnViewData(clsSystem mySystem) : this()
        {
            this.system = mySystem;
            workspaceVD = new WorkspaceViewData((int)this.system.AbBereich.ID);

        }
        public AsnViewData(int myAsnId, Globals._GL_USER myGLUser) : this()
        {
            this.BenutzerID = (int)myGLUser.User_ID;
            if (myAsnId > 0)
            {
                asnHead.AsnNr = myAsnId;
                asnHead.Id = myAsnId;
                Fill();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitCls()
        {
            system = new clsSystem();
            asnHead = new Asn();
            List_ctrAsnRead_AsnEdifactView = new List<ctrASNRead_AsnEdifactView>();
            List_ctrAsnRead_AsnArticelEdifactView = new List<ctrASNRead_AsnArticleEdifactView>();
            ListAsn = new List<Asn>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mybInclSub"></param>
        public void Fill()
        {
            string strSQL = sql_Get;
            dtAsnValue = new DataTable();
            dtAsnValue = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "edifactValue");
            if (dtAsnValue.Rows.Count > 0)
            {
                foreach (DataRow dr in dtAsnValue.Rows)
                {
                    SetValue(dr);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        public void SetValue(DataRow row)
        {
            asnHead = new Asn();

            int iTmp = 0;
            int.TryParse(row["ID"].ToString(), out iTmp);
            asnHead.Id = iTmp;

            asnHead.ASNFileTyp = row["ASNFileTyp"].ToString();

            iTmp = 0;
            int.TryParse(row["ASNNr"].ToString(), out iTmp);
            asnHead.AsnNr = iTmp;

            iTmp = 0;
            int.TryParse(row["ASNFieldID"].ToString(), out iTmp);
            asnHead.AsnFieldId = iTmp;

            iTmp = 0;
            int.TryParse(row["ASNTypID"].ToString(), out iTmp);
            asnHead.AsnTypId = iTmp;

            asnHead.Path = row["Path"].ToString();
            asnHead.FileName = row["FileName"].ToString();

            DateTime dtTmp = new DateTime(1900, 1, 1);
            DateTime.TryParse(row["Datum"].ToString(), out dtTmp);
            asnHead.Datum = dtTmp;

            asnHead.IsRead = (bool)row["IsRead"];
            asnHead.Direction = row["Direction"].ToString();
            iTmp = 0;
            int.TryParse(row["MandantenID"].ToString(), out iTmp);
            asnHead.MandantenId = iTmp;

            iTmp = 0;
            int.TryParse(row["ArbeitsbereichID"].ToString(), out iTmp);
            asnHead.WorkspaceId = iTmp;
            asnHead.EdiMessageValue = EdifactToTextToSaveInDBConverter.ConvertTextFromDBToEdifact(row["EdiMessageValue"].ToString());

            iTmp = 0;
            int.TryParse(row["AsnArtId"].ToString(), out iTmp);
            asnHead.AsnArtId = iTmp;

            dtTmp = new DateTime(1900, 1, 1);
            DateTime.TryParse(row["Created"].ToString(), out dtTmp);
            asnHead.Created = dtTmp;
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
                asnHead.Id = iTmp;
            }
        }
        /// <summary>
        ///             DELETE
        /// </summary>
        public bool Delete()
        {
            bool bReturn = false;
            switch (asnHead.ASNFileTyp)
            {
                case constValue_AsnArt.const_Art_VDA4913:
                case constValue_AsnArt.const_Art_VDA4905:
                    AsnValueViewData asnValueVD = new AsnValueViewData(asnHead);
                    bReturn = asnValueVD.DeleteAllByAsnId(asnHead);
                    break;

                case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                case constValue_AsnArt.const_Art_EDIFACT_DESADV_D07A:
                case constValue_AsnArt.const_Art_EDIFACT_ASN_D97A:
                    EdifactValueViewData edifactValueViewData = new EdifactValueViewData();
                    bReturn = edifactValueViewData.DeleteByAsnId(asnHead);
                    break;
                default:
                    break;
            }
            if (bReturn)
            {
                string strSQL = "DELETE FROM ASN WHERE ID=" + asnHead.Id + "; ";
                bReturn = clsSQLCOM.ExecuteSQLWithTRANSACTION(strSQL, "DeleteAsn", 1);
            }
            return bReturn;
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

        public void UpdateFileName()
        {
            string strSQL = sql_Update_Filename;
            clsSQLCOM.ExecuteSQL(strSQL, BenutzerID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mybInclSub"></param>
        public void FillList(bool mybInclSub)
        {
            //ListEdiDelforValue = new List<EdiDelforD97AValues>();
            //string strSQL = sql_GetList;
            //DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EdiDelforValue");
            //if (dt.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        SetValue(dr, mybInclSub);
            //        ListEdiDelforValue.Add(EdiDelforValue);
            //    }
            //}
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Asn> GetListEDIFACT()
        {
            List<Asn> listReturn = new List<Asn>();
            string strSQL = sql_GetList;
            strSQL += " WHERE IsRead = 0 ";
            strSQL += " AND ASNFileTyp IN (";
            strSQL += "'" + Constants.constValue_AsnArt.const_Art_EDIFACT_ASN_D97A + "'";
            strSQL += ",'" + Constants.constValue_AsnArt.const_Art_EDIFACT_ASN_D96A + "'";
            strSQL += ",'" + Constants.constValue_AsnArt.const_Art_EDIFACT_DESADV_D07A + "'";
            strSQL += ") ";
            strSQL += "AND  ArbeitsbereichID=" + workspaceVD.Workspace.Id;
            strSQL += " ; ";


            DataTable dt = clsSQLCOM.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "AsnList", "AsnList", BenutzerID);
            foreach (DataRow r in dt.Rows)
            {
                this.asnHead = new Asn();
                SetValue(r);
                if (!listReturn.Contains(this.asnHead))
                {
                    listReturn.Add(this.asnHead);
                }
            }
            ListAsn = listReturn;
            return listReturn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Asn> GetListVda()
        {
            List<Asn> listReturn = new List<Asn>();
            string strSQL = sql_GetList;
            strSQL += " WHERE IsRead = 0 ";
            strSQL += " AND ASNFileTyp IN (";
            strSQL += "'" + Constants.constValue_AsnArt.const_Art_VDA4913 + "'";
            strSQL += ",'" + Constants.constValue_AsnArt.const_Art_VDA4905 + "'";
            strSQL += ") ";
            strSQL += "AND  ArbeitsbereichID=" + workspaceVD.Workspace.Id;
            strSQL += " ; ";


            DataTable dt = clsSQLCOM.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "dtAsnValue", "dtAsnValue", BenutzerID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    this.asnHead = new Asn();
                    SetValue(r);
                    if (!listReturn.Contains(this.asnHead))
                    {
                        listReturn.Add(this.asnHead);
                    }
                }
            }
            ListAsn = listReturn;
            return listReturn;
        }
        /// <summary>
        ///             Ermittelt die Asn Liste nach vorgegebenen Ids
        /// </summary>
        /// <param name="myListId"></param>
        /// <returns></returns>
        public void GetListbyAsnId(List<int> myListId)
        {
            if (myListId.Count > 0)
            {
                ListAsn = new List<Asn>();
                string strSQL = sql_GetList;
                strSQL += " WHERE ";
                strSQL += " Id IN (";
                strSQL += "'" + string.Join(",", myListId) + "'";
                strSQL += ") ";
                strSQL += " ; ";

                dtAsnValue = new DataTable();
                dtAsnValue = clsSQLCOM.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "AsnList", "AsnList", BenutzerID);
                foreach (DataRow r in dtAsnValue.Rows)
                {
                    this.asnHead = new Asn();
                    SetValue(r);
                    if (!ListAsn.Contains(this.asnHead))
                    {
                        ListAsn.Add(this.asnHead);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myListId"></param>
        public void GetListAll()
        {
            ListAsn = new List<Asn>();
            string strSQL = sql_GetList;
            dtAsnValue = new DataTable();
            dtAsnValue = clsSQLCOM.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "AsnList", "AsnList", BenutzerID);
            foreach (DataRow r in dtAsnValue.Rows)
            {
                this.asnHead = new Asn();
                SetValue(r);
                if (!ListAsn.Contains(this.asnHead))
                {
                    ListAsn.Add(this.asnHead);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myListId"></param>
        public void GetListForClean()
        {
            DateTime dtCompare = DateTime.Now.AddDays(-30);
            ListAsn = new List<Asn>();
            string strSQL = sql_GetList;
            strSQL += " WHERE Datum < CAST('" + dtCompare.ToString("dd.MM.yyyy") + "' as Date)";
            strSQL += " AND IsRead = 1 AND Direction='IN' ";
            dtAsnValue = new DataTable();
            dtAsnValue = clsSQLCOM.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "AsnList", "AsnList", BenutzerID);
            foreach (DataRow r in dtAsnValue.Rows)
            {
                this.asnHead = new Asn();
                SetValue(r);
                if (!ListAsn.Contains(this.asnHead))
                {
                    ListAsn.Add(this.asnHead);
                }
            }
        }
        /// <summary>
        ///             EDIFACT
        ///             Erstellt die AsnEdifactView und die AsnArticleEdifactView, 
        ///             die jeweils die Daten für Eingang und Artikel enthalten
        /// </summary>
        /// <param name="myAsnList"></param>
        public void FillAsnEdifactViewAndArticleEdifactView(List<Asn> myAsnList)
        {
            foreach (var item in myAsnList)
            {
                if (item.Id == 74740)
                {
                    string s = string.Empty;
                }

                EdifactMessageToClasses edi = new EdifactMessageToClasses(item, BenutzerID);
                if (edi != null)
                {
                    if (
                            (edi.eingangViewData is EingangViewData) &&
                            (edi.eingangViewData.ListArticleInEingang.Count > 0)
                       )
                    {
                        ctrASNRead_AsnEdifactView v = new ctrASNRead_AsnEdifactView(item, edi.eingangViewData);
                        List_ctrAsnRead_AsnEdifactView.Add(v);

                        //foreach (Articles art in edi.eingangViewData.ListArticleInEingang)
                        foreach (Articles art in v.ListArticleInEingang)
                        {
                            ctrASNRead_AsnArticleEdifactView a = new ctrASNRead_AsnArticleEdifactView(edi.eingang, art);
                            List_ctrAsnRead_AsnArticelEdifactView.Add(a);
                        }
                    }
                    else
                    {
                        try
                        {
                            string strMes = "Infos zur fehlerhaften ASN:" + Environment.NewLine;
                            strMes += "ASN:  " + edi.eingang.ASN + Environment.NewLine;
                            strMes += "Workspace:  " + edi.eingang.ArbeitsbereichId + Environment.NewLine;
                            strMes += "Auftraggeber:  " + edi.eingang.AuftraggeberString + Environment.NewLine;
                            strMes += "Emfpänger:  " + edi.eingang.EmpfaengerString + Environment.NewLine;
                            strMes += Environment.NewLine;
                            strMes += "ListSegment_Head:  " + Environment.NewLine;
                            int iCount = 0;
                            foreach (var s in edi.listEdiSegments_Head)
                            {
                                iCount++;
                                strMes += iCount.ToString("000") + " - " + s + Environment.NewLine;
                            }
                            strMes += Environment.NewLine;
                            strMes += "ListEdiSegments_Article:  " + Environment.NewLine;
                            iCount = 0;
                            foreach (var s in edi.listEdiSegments_Article)
                            {
                                iCount++;
                                strMes += iCount.ToString("000") + " - " + s + Environment.NewLine;
                            }
                            strMes += Environment.NewLine;
                            strMes += "edi.ErrorLog:  " + Environment.NewLine;
                            strMes += edi.ErrorLog + Environment.NewLine;

                            clsMail EMail = new clsMail();
                            EMail.InitClass(new Globals._GL_USER(), new clsSystem());
                            EMail.Subject = this.system.Client.MatchCode + DateTime.Now.ToShortDateString() + " - Error: AsnViewData|FillAsnEdifactViewAndArticleEdifactView Fehler bei der ASN Verarbeitung!";
                            EMail.Message = strMes;
                            EMail.SendError();
                        }
                        catch (Exception ex)
                        {
                            //clsLog.WriteErrorLog(ex, "AsnViewData.FillAsnEdifactViewAndArticleEdifactView");
                        }
                    }
                }
            }
        }
        /// <summary>
        ///             Bildet eine Liste vor zu verarbeitenden ASN
        /// </summary>
        /// <param name="myAsnList"></param>
        /// <returns></returns>
        public List<ctrASNRead_AsnVdaView> FillAsnVdaView(List<Asn> myAsnList, BackgroundWorker myWorker)
        {
            List<ctrASNRead_AsnVdaView> listReturn = new List<ctrASNRead_AsnVdaView>();
            int iAsnLoopCount = 0;
            foreach (var item in myAsnList)
            {
                iAsnLoopCount++;
                AsnValueViewData asnValueVD = new AsnValueViewData(item);
                VdaMessageToClasses vda = new VdaMessageToClasses(this.system);
                vda.CreateHeadVdaMessageValue(item, asnValueVD.dtAsnValues);

                if (!listReturn.Contains(vda.AsnVdaView))
                {
                    listReturn.Add(vda.AsnVdaView);
                }
                myWorker.ReportProgress(iAsnLoopCount);
            }
            return listReturn;
        }
        /// <summary>
        ///             Bildet eine Liste der Artikeldaten der ASN Meldung
        /// </summary>
        /// <param name="myAsnVdaVList"></param>
        /// <returns></returns>
        public List<ctrASNRead_AsnArticleVdaView> AsnArticleVdaViewInit(List<ctrASNRead_AsnVdaView> myAsnVdaVList)
        {
            List<ctrASNRead_AsnArticleVdaView> listReturn = new List<ctrASNRead_AsnArticleVdaView>();
            foreach (var item in myAsnVdaVList)
            {
                VdaMessageToClasses vda = new VdaMessageToClasses(this.system);
                var list = vda.CreateArtikelVdaMessageValueList(item.dtAsnValues, item.eingang);
                if (list.Count > 0)
                {
                    listReturn.AddRange(list);
                }
            }
            return listReturn;
        }
        /// <summary>
        ///             VDA
        ///             Erstellt die AsnVdaView und die AsnArticleVdaView, 
        ///             die jeweils die Daten für Eingang und Artikel enthalten
        /// </summary>
        /// <param name="myAsnList"></param>
        public void FillAsnVdaViewAndArticleEdifactView(List<Asn> myAsnList)
        {
            foreach (var item in myAsnList)
            {
                EdifactMessageToClasses edi = new EdifactMessageToClasses(item, BenutzerID);
                if (edi != null)
                {
                    if (
                            (edi.eingangViewData is EingangViewData) &&
                            (edi.eingangViewData.ListArticleInEingang.Count > 0)
                       )
                    {
                        ctrASNRead_AsnEdifactView v = new ctrASNRead_AsnEdifactView(item, edi.eingangViewData);
                        List_ctrAsnRead_AsnEdifactView.Add(v);

                        //foreach (Articles art in edi.eingangViewData.ListArticleInEingang)
                        foreach (Articles art in v.ListArticleInEingang)
                        {
                            ctrASNRead_AsnArticleEdifactView a = new ctrASNRead_AsnArticleEdifactView(edi.eingang, art);
                            List_ctrAsnRead_AsnArticelEdifactView.Add(a);
                        }
                    }
                    else
                    {
                        try
                        {
                            clsMail EMail = new clsMail();
                            EMail.InitClass(new Globals._GL_USER(), new clsSystem());
                            EMail.Subject = this.system.Client.MatchCode + DateTime.Now.ToShortDateString() + " - Error: AsnViewData|FillAsnEdifactViewAndArticleEdifactView Fehler bei der ASN Verarbeitung!";
                            EMail.Message = edi.ErrorLog;
                            EMail.SendError();
                        }
                        catch (Exception ex)
                        {
                            //clsLog.WriteErrorLog(ex, "AsnViewData.FillAsnEdifactViewAndArticleEdifactView");
                        }
                    }
                }
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
                string strSQL = "INSERT INTO ASN ([ASNFileTyp], " +
                                                  "[ASNNr], " +
                                                  "[ASNFieldID], " +
                                                  "[ASNTypID], " +
                                                  "[Path], " +
                                                  "[FileName], " +
                                                  "[Datum], " +
                                                  "[IsRead], " +
                                                  "[Direction], " +
                                                  "[MandantenID], " +
                                                  "[ArbeitsbereichID], " +
                                                  "[EdiMessageValue]," +
                                                  "[AsnArtId], " +
                                                  "[Created] " +
                                                  ")" +
                                                  " VALUES " +
                                                  "(" +
                                                        "'" + asnHead.ASNFileTyp + "'" +
                                                        ", " + asnHead.AsnNr +
                                                        ", " + asnHead.AsnFieldId +
                                                        ", " + asnHead.AsnTypId +
                                                        ", '" + asnHead.Path + "'" +
                                                        ", '" + asnHead.FileName + "'" +
                                                        ", '" + asnHead.Datum + "'" +
                                                        ", " + Convert.ToInt32(asnHead.IsRead) +
                                                        ", '" + asnHead.Direction + "'" +
                                                        ", " + asnHead.MandantenId +
                                                        ", " + asnHead.WorkspaceId +
                                                        ", '" + EdifactToTextToSaveInDBConverter.ConvertEdifactToTextToDb(asnHead.EdiMessageValue) + "'" +
                                                        ", " + asnHead.AsnArtId +
                                                        ", '" + asnHead.Created + "'" +

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
                strSql = "SELECT * FROM ASN WHERE ID=" + asnHead.Id + " ";
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
                strSql = "SELECT * FROM ASN ";
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
                strSql = "SELECT * FROM ASN";
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
                strSql = "Update ASN SET " +
                                            " [ASNFileTyp]  = '" + asnHead.ASNFileTyp + "'" +
                                            ", [ASNNr]  = " + asnHead.AsnNr +
                                            ", [ASNFieldID]  = " + asnHead.AsnFieldId +
                                            ", [ASNTypID]  = " + asnHead.AsnTypId +
                                            ", [Path]  = '" + asnHead.Path + "'" +
                                            ", [FileName] = '" + asnHead.FileName + "'" +
                                            ", [Datum]  = '" + asnHead.Datum + "'" +
                                            ", [IsRead]  = " + Convert.ToInt32(asnHead.IsRead) +
                                            ", [Direction]  = '" + asnHead.Direction + "'" +
                                            ", [MandantenID]  = " + asnHead.MandantenId +
                                            ", [ArbeitsbereichID] = " + asnHead.WorkspaceId +
                                            ", [EdiMessageValue] = '" + EdifactToTextToSaveInDBConverter.ConvertEdifactToTextToDb(asnHead.EdiMessageValue) + "'" +
                                            ", [AsnArtId]  = " + asnHead.AsnArtId +
                                            "WHERE ID=" + asnHead.Id;
                return strSql;
            }
        }
        /// <summary>
        ///             Update sql - String
        /// </summary>
        public string sql_Update_Filename
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Update ASN SET FileName = '" + asnHead.FileName + "' WHERE ID=" + asnHead.Id + " ;";
                return strSql;
            }
        }

    }
}

