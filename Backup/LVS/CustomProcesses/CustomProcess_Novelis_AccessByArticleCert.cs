using Common.Enumerations;
using Common.Models;
using LVS.Communicator.ZQM;
using LVS.Constants;
using LVS.Models;
using LVS.sqlStatementCreater;
using LVS.ViewData;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

namespace LVS.CustomProcesses
{
    public class CustomProcess_Novelis_AccessByArticleCert
    {
        public string ProcessName = constValue_CustomProcesses.const_Process_Novelis_ArticleAccessByCertifacte;
        public string ProcessViewName = "Novelis Artikelzertifikatsprozess";
        public const string const_ProcessLocation_ctrEinlagerung_CheckEingangElementsToFinish = "#ctrEinlagerung_CheckEingangElementsToFinish#";
        public const string const_ProcessLocation_EingangViewData_Update_WizStoreIn_CheckEingangElementsToFinish = "#EingangViewData_Update_WizStoreIn_CheckEingangElementsToFinish#";
        public const string const_ProcessLocation_EingangViewData_Update_WizStoreIn_CheckForCustomProzessForASNTransfer = "#EingangViewData_Update_WizStoreIn_CheckForCustomProzessForASNTransfer#";
        public const string const_ProcessLocation_CtrASNRead_tsbtnCreateEingang_Click = "#CtrASNRead_tsbtnCreateEingang_Click#";
        public const string const_ProcessLocation_AsnReadViewData_CreateStoreInByAsnId = "#AsnReadViewData_CreateStoreInByAsnId#";
        public const string const_ProcessLocation_Task_CustomizedlProcesses = "#Task_CustomizedlProcesses#";

        public bool IsArticleZertifacteProcessComplete { get; set; } = false;

        public EdiZQMQalityXmlViewData zqmVD { get; set; }
        public enumCustumerProcessStatus_Novelis_AccessByArticleCert Novelis_AccessByArticleCertStatus;
        public SperrlagerViewData sqlVD { get; set; }
        public ArticleViewData artVD { get; set; }
        public EingangViewData eVD { get; set; }
        private Globals._GL_USER GL_USER { get; set; }
        public List<string> ListLog { get; set; } = new List<string>();
        internal sqlCreater_CustomProcess_Novelis_AccessByArticleCert sqlCreater_CustomProcess_Novelis_AccessByArticleCert;
        public CustomProcess_Novelis_AccessByArticleCert(Globals._GL_USER myGLUser)
        {
            GL_USER = myGLUser;
            Novelis_AccessByArticleCertStatus = enumCustumerProcessStatus_Novelis_AccessByArticleCert.NotSet;
        }

        /// <summary>
        ///             returns bool true, if anything is done
        ///             else false
        /// </summary>
        /// <param name="myArticleId"></param>
        /// <param name="myEingangId"></param>
        /// <param name="myAusgangId"></param>
        /// <param name="myProcessLocationString"></param>
        /// <returns></returns>
        public bool ExecuteProcess(int myArticleId, int myEingangId, int myAusgangId, string myProcessLocationString)
        {
            bool bReturn = false;
            switch (myProcessLocationString)
            {

                case CustomProcess_Novelis_AccessByArticleCert.const_ProcessLocation_ctrEinlagerung_CheckEingangElementsToFinish:
                case CustomProcess_Novelis_AccessByArticleCert.const_ProcessLocation_EingangViewData_Update_WizStoreIn_CheckEingangElementsToFinish:
                case CustomProcess_Novelis_AccessByArticleCert.const_ProcessLocation_EingangViewData_Update_WizStoreIn_CheckForCustomProzessForASNTransfer:
                    if (myEingangId > 0)
                    {
                        eVD = new EingangViewData(myEingangId, (int)GL_USER.User_ID, true);
                        sqlCreater_CustomProcess_Novelis_AccessByArticleCert = new sqlCreater_CustomProcess_Novelis_AccessByArticleCert(eVD);
                        IsArticleZertifacteProcessComplete = IsCertificateProcessCompelte(eVD.ListArticleInEingang);
                        bReturn = true;
                    }
                    break;
                case CustomProcess_Novelis_AccessByArticleCert.const_ProcessLocation_AsnReadViewData_CreateStoreInByAsnId:
                case CustomProcess_Novelis_AccessByArticleCert.const_ProcessLocation_CtrASNRead_tsbtnCreateEingang_Click:
                    if (myArticleId > 0)
                    {
                        CheckForArticleCertificateByStoreIn(myArticleId);
                        bReturn = true;
                    }
                    break;
                case CustomProcess_Novelis_AccessByArticleCert.const_ProcessLocation_Task_CustomizedlProcesses:
                    CheckForArticleCertificateByCommunicator();
                    bReturn = true;
                    break;
            }
            return bReturn;
        }
        /// <summary>
        ///             prüft, ob die Anzahl der vorliegenden Artikel Zertifikate der Anzahl der Artikel im Eingang entspicht
        ///             und von diesen Artikel sich keiner im SPL befindet
        /// </summary>
        /// <param name="myArticleInEingangList"></param>
        /// <returns></returns>

        private bool IsCertificateProcessCompelte(List<Articles> myArticleInEingangList)
        {
            bool bReturn = false;
            string strSql = string.Empty;

            int iCountArtInEingang = myArticleInEingangList.Count;
            int iCountCertificates = 0;
            int iCountSPL = 0;

            //--Check Zertifikat für alle Artikel vorhanden
            strSql = string.Empty;
            strSql = sqlCreater_CustomProcess_Novelis_AccessByArticleCert.sqlCountArtCert;

            ///strSql += "SELECT Count(ID) as Anzahl FROM SZG_COM.dbo.EdiZQMQalityXml ";
            //strSql += "SELECT Count(ID) as Anzahl FROM EdiZQMQalityXml ";
            //strSql += "where ";
            //strSql += "ArticleId in (" + string.Join(",", myArticleInEingangList.Select(x => x.Id).ToList()) + ") ";
            //strSql += "and Produktionsnummer in (SELECT Produktionsnummer FROM SZG_LVS.dbo.Artikel where ID in (" + string.Join(",", myArticleInEingangList.Select(x => x.Id).ToList()) + ")) ";
            //strSql += "and LfsNr in (SELECT LfsNr FROM SZG_LVS.dbo.LEingang where ID in (SELECT LEingangTableID FROM SZG_LVS.dbo.Artikel where ID in (" + string.Join(",", myArticleInEingangList.Select(x => x.Id).ToList()) + "))) ";

            object objCountCert = clsSQLCOM.ExecuteSQL_GetValue(strSql, this.GL_USER.User_ID);
            int.TryParse(objCountCert.ToString(), out iCountCertificates);

            //--gleichzeitig Artikel nicht mehr im SPL
            strSql = string.Empty;
            strSql = sqlCreater_CustomProcess_Novelis_AccessByArticleCert.sqlCountArtSplCert;

            //strSql = string.Empty;
            //strSql += "SELECT Count(ID) as Anzahl FROM SZG_LVS.dbo.Sperrlager ";
            //strSql += "WHERE ";
            //strSql += "BKZ = 'IN' ";
            //strSql += "AND ID NOT IN (SELECT DISTINCT SPLIDIn FROM SZG_LVS.dbo.Sperrlager WHERE SPLIDIn>0) ";
            //strSql += "AND ArtikelID IN (" + string.Join(",", myArticleInEingangList.Select(x => x.Id).ToList()) + ") ";
            ////strSql += " AND IsCustomCertificateMissing=1";

            object objCountSPL = clsSQLCOM.ExecuteSQL_GetValue(strSql, this.GL_USER.User_ID);
            int.TryParse(objCountSPL.ToString(), out iCountSPL);

            //--- TRUE = (Anzahl Artikel im Eingang == Anzahl vorliegender Zertifikate) && (Anzahl Artikel im Eingang im SPL == 0) 
            bReturn = ((iCountArtInEingang == iCountCertificates) && (iCountSPL == 0));

            return bReturn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myArtId"></param>
        private void CheckForArticleCertificateByStoreIn(int myArtId)
        {
            if (myArtId > 0)
            {
                string strSql = string.Empty;
                artVD = new ArticleViewData(myArtId, GL_USER);

                zqmVD = new EdiZQMQalityXmlViewData();
                zqmVD.GetListActivItems();
                Novelis_AccessByArticleCertStatus = enumCustumerProcessStatus_Novelis_AccessByArticleCert.NotSet;

                foreach (EdiZQMQalityXml item in zqmVD.ListZQMQalityActive)
                {
                    string strProduktionsnummer = string.Empty;
                    string strLfsNr = string.Empty;

                    if ((item.iDocXml != null) && (item.iDocXml.Length > 0))
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(item.iDocXml);
                        ZQM_Quality02 zqm = new ZQM_Quality02(xmlDoc);

                        if ((zqm != null) && (zqm.Produktionsnummer.Length > 0) && (zqm.LfsNo.Length > 0))
                        {
                            strProduktionsnummer = zqm.Produktionsnummer;
                            strLfsNr = zqm.LfsNo;
                        }
                    }

                    if ((strProduktionsnummer.Length > 0) && (strLfsNr.Length > 0))
                    {
                        if (
                            (artVD.Artikel.Produktionsnummer.Equals(strProduktionsnummer))
                            &&
                            (artVD.Artikel.Eingang.LfsNr.Equals(strLfsNr))
                           )
                        {
                            strSql = string.Empty;
                            zqmVD.ediZQMQalityXml.ArticleId = artVD.Artikel.Id;
                            zqmVD.ediZQMQalityXml.WorkspaceId = artVD.Artikel.AbBereichID;
                            zqmVD.ediZQMQalityXml.IsActive = false;
                            zqmVD.ediZQMQalityXml.LfsNr = artVD.Artikel.Eingang.LfsNr;
                            zqmVD.ediZQMQalityXml.Produktionsnummer = artVD.Artikel.Produktionsnummer;
                            bool bOK = zqmVD.Update();
                            if (bOK)
                            {
                                //-- Status Zertifikat liegt vor
                                Novelis_AccessByArticleCertStatus = enumCustumerProcessStatus_Novelis_AccessByArticleCert.ArticleCertifateExist;
                                //-- Eintrag in Artikel Vita
                                CustomProcess_Novelis_AccessByArticleCert.ArtikelVitaInsert_CertificateCheckOK(artVD.Artikel, GL_USER, zqmVD.ediZQMQalityXml);
                                break;
                            }
                        }
                    }
                }
                string str = string.Empty;
                //--- Buchungs ins SPL
                if (!Novelis_AccessByArticleCertStatus.Equals(enumCustumerProcessStatus_Novelis_AccessByArticleCert.ArticleCertifateExist))
                {
                    if (CustomProcess_Novelis_AccessByArticleCert.ArticleCertificateIsMissing_BookingToSpl(artVD.Artikel, GL_USER))
                    {
                        Novelis_AccessByArticleCertStatus = enumCustumerProcessStatus_Novelis_AccessByArticleCert.ArticleBookedInSPL;
                    }
                }
            }
        }
        /// <summary>
        ///                 obsolet ?
        /// </summary>
        public void CheckActiveZqmQualityItems()
        {
            zqmVD = new EdiZQMQalityXmlViewData();
            zqmVD.GetListActivItems();

            foreach (EdiZQMQalityXml item in zqmVD.ListZQMQalityActive)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(item.iDocXml);
                ZQM_Quality02 zqm = new ZQM_Quality02(doc);
            }
        }
        ///// <summary>
        ///// 
        ///// </summary>
        //public void CheckForArticleCertificateByCommunicator()
        //{
        //    ListLog = new List<string>();
        //    zqmVD = new EdiZQMQalityXmlViewData();
        //    zqmVD.GetListActivItems();

        //    foreach (EdiZQMQalityXml item in zqmVD.ListZQMQalityActive)
        //    {
        //        if (item.LfsNr.Equals("")) 
        //        {
        //            string strP = string.Empty;
        //        }
        //        ProcessCheckAndCorrectItemData(item);
        //    }
        //}
        ///// <summary>
        /////             Überprüft die alle Datensätze der letzten 10 Tage
        ///// </summary>
        //public void CheckForArticleCertificateByCommunicatorForLastDays()
        //{
        //    ListLog = new List<string>();
        //    zqmVD = new EdiZQMQalityXmlViewData();
        //    zqmVD.GetListByDate(DateTime.Now.Date.AddDays(-12));

        //    foreach (EdiZQMQalityXml item in zqmVD.ListZQMQalityActive)
        //    {
        //        if (item.LfsNr.Equals(""))
        //        {
        //            string strP = string.Empty;
        //        }
        //        ProcessCheckAndCorrectItemData(item);
        //    }
        //}
        /// <summary>
        ///             Überprüft die alle Datensätze der letzten 10 Tage
        ///             oder alle aktiven
        /// </summary>
        public void CheckForArticleCertificateByCommunicator(bool myForLastDays = false)
        {
            ListLog = new List<string>();
            zqmVD = new EdiZQMQalityXmlViewData();
            if (myForLastDays)
            {
                zqmVD.GetListByDate(DateTime.Now.Date.AddDays(-12));
            }
            else
            {
                zqmVD.GetListActivItems();

            }
            foreach (EdiZQMQalityXml item in zqmVD.ListZQMQalityActive)
            {
                if (item.LfsNr.Equals(""))
                {
                    string strP = string.Empty;
                }
                ProcessCheckAndCorrectItemData(item);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        private void ProcessCheckAndCorrectItemData(EdiZQMQalityXml item)
        {
            string strLog = string.Empty;
            strLog = Environment.NewLine + "   |- ZQMXml-Id: [ " + item.Id + " ] | Lfs: " + item.LfsNr + " | Produktionsnummer: " + item.Produktionsnummer + " | Workspace: " + item.WorkspaceXmlRef + " | Workspace-Id: " + item.WorkspaceId;


            if (
                    (item is EdiZQMQalityXml) &&
                    (item.Produktionsnummer.Length > 0) &&
                    (item.LfsNr.Length > 0) &&
                    (item.iDocXml.Length > 0)
               )
            {
                //strLog = string.Empty;
                ZQM_Quality02 zqm = ReadAndCheckXMLiDoc(ref item, ref strLog);
                int iWorkspaceCheckId = AddressReferenceViewData.GETWorkspaceBySupplierReference(zqm.iRCVPRN, constValue_AsnArt.const_Art_XML_ZQM_QALITY02, 1);

                if (item is EdiZQMQalityXml)
                {
                    //-- freigabe für den Artikel
                    //-- ausbuchen aus SPL
                    //-- Lagermeldungen erstellen
                    //-- aktiv = false
                    artVD = new ArticleViewData();
                    artVD.SearchArtikelByProductionNoAndLfsNo(item.Produktionsnummer, item.LfsNr);

                    if ((artVD.Artikel.Id > 0) && (artVD.Artikel.LAusgangTableID == 0) && (artVD.Artikel.AbBereichID == iWorkspaceCheckId))
                    {
                        strLog += Environment.NewLine + "   |-> vorhanden Artikel LVSNR: " + artVD.Artikel.LVS_ID + "[" + artVD.Artikel.Id + "]";

                        clsSPL spl = new clsSPL();
                        string strSql = spl.SQLArtikelBookOutSPLByCertificate(artVD.Artikel, true);
                        if (strSql.Length > 0)
                        {
                            bool bOK = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "SPLCheckOut", 1);
                            if (bOK)
                            {
                                strSql = string.Empty;
                                item.IsActive = false;
                                item.ArticleId = artVD.Artikel.Id;
                                item.WorkspaceId = artVD.Artikel.AbBereichID;
                                zqmVD.ediZQMQalityXml = item.Copy();
                                item.WorkspaceXmlRef = zqm.iRCVPRN;

                                if (item.Description.Length > 100)
                                {
                                    item.Description = string.Empty;
                                    item.Description += "E1EDP02|069 =" + zqm.Produktionsnummer + " ";
                                    item.Description += Environment.NewLine + "E1EDK02|012 =" + zqm.LfsNo;
                                }

                                sqlCreater_EdiZQMQalityXml sql = new sqlCreater_EdiZQMQalityXml(item);
                                strSql += sql.sql_Update();

                                clsSQLCOM.ExecuteSQLWithTRANSACTION(strSql, "UpdateZQMA", 1);

                                clsLager lager = new clsLager();
                                lager.FillLagerDatenByArtikelId(artVD.Artikel.Id);

                                ExecuteProcess(0, (int)lager.LEingangTableID, 0, CustomProcess_Novelis_AccessByArticleCert.const_ProcessLocation_ctrEinlagerung_CheckEingangElementsToFinish);
                                if (this.IsArticleZertifacteProcessComplete)
                                {
                                    bool bCheckEingang = true;

                                    if (
                                            (artVD.Artikel.Anzahl < 1) ||
                                            (artVD.Artikel.bSPLartCert)
                                       )
                                    {
                                        bCheckEingang = false;
                                    }

                                    if ((bCheckEingang) && (!eVD.Eingang.Check))
                                    {
                                        eVD = new EingangViewData(artVD.Artikel.LEingangTableID, 1, true);
                                        if (eVD.ListArticleInEingang.Count > 0)
                                        {
                                            foreach (var a in eVD.ListArticleInEingang)
                                            {
                                                ArticleViewData aVD = new ArticleViewData(a, 1);
                                                aVD.FillClsOnly = false;
                                                aVD.Fill();
                                                aVD.Artikel.EingangChecked = true;
                                                aVD.UpdateArtikelCheck();
                                            }

                                            //--- Eingang abschließen
                                            eVD.Eingang.Check = true;
                                            eVD.Update_Datafield_Check(eVD.Eingang.Check);
                                            //--- Edi Meldung versenden
                                            lager.ASNAction.ASNActionProcessNr = clsASNAction.const_ASNAction_Eingang;
                                            clsASNTransfer AsnTransfer = new clsASNTransfer();
                                            AsnTransfer.CreateLM(ref lager);
                                        }
                                    }

                                    //EingangViewData eVD = new EingangViewData(artVD.Artikel.LEingangTableID, 1, false);
                                    //eVD.Eingang.Check = true;
                                    //eVD.Update_Datafield_Check(eVD.Eingang.Check);
                                    //lager.FillLagerDatenByArtikelId(artVD.Artikel.Id);
                                    //artVD.Artikel.EingangChecked=true;
                                    //artVD.UpdateArtikelCheck();

                                    //lager.Artikel.EingangChecked = true;
                                    //lager.Artikel.UpdateArtikelALLDispo();
                                    //lager.ASNAction.ASNActionProcessNr = clsASNAction.const_ASNAction_Eingang;
                                    //clsASNTransfer AsnTransfer = new clsASNTransfer();
                                    //AsnTransfer.CreateLM(ref lager);
                                }
                            }
                        }
                        else
                        {
                            //-- Artikel vorhanden aber bereits ausgebucht und Zertifikat ist noch nicht deaktiviert
                            strLog += Environment.NewLine + "   |-> bereits ausgebucht Artikel LVSNR: " + artVD.Artikel.LVS_ID + "[" + artVD.Artikel.Id + "] - Zert. deaktiviert";
                            if (
                                (artVD.Artikel.Produktionsnummer.Equals(item.Produktionsnummer)) &&
                                (artVD.Artikel.Eingang.LfsNr.Equals(item.LfsNr))
                               )
                            {
                                strSql = string.Empty;
                                item.IsActive = false;
                                item.ArticleId = artVD.Artikel.Id;
                                item.WorkspaceId = artVD.Artikel.AbBereichID;
                            }
                            else
                            {
                                item.IsActive = true;
                                strLog = string.Empty;
                                ReadAndCheckXMLiDoc(ref item, ref strLog);
                            }
                            sqlCreater_EdiZQMQalityXml sql = new sqlCreater_EdiZQMQalityXml(item);
                            strSql += sql.sql_Update();

                            clsSQLCOM.ExecuteSQLWithTRANSACTION(strSql, "UpdateZQMA", 1);
                        }
                    }
                    else
                    {
                        if (artVD.Artikel.LAusgangTableID == 0)
                        {
                            strLog += Environment.NewLine + "   |-> Artikel mit dieser Produktionsnummer und Lieferschein nicht vorhanden !" + Environment.NewLine;
                        }
                        else
                        {
                            strLog += Environment.NewLine + "   |-> Artikel ist bereits ausgelagert !" + Environment.NewLine;
                        }
                    }
                }
            }
            else
            {
                strLog += " -> Nicht vorhanden! Daten upgedated!" + Environment.NewLine;

                EdiZQMQalityXmlViewData zqmVD = new EdiZQMQalityXmlViewData();
                zqmVD.ediZQMQalityXml = zqmVD.ReadEdiZQMQalityXmlByIdocXmlString(item);
                zqmVD.Update();
                strLog += Environment.NewLine + "   |- iDoc neu verarbeitet: Lfs: " + zqmVD.ediZQMQalityXml.LfsNr + " | Produktionsnummer: " + zqmVD.ediZQMQalityXml.Produktionsnummer;
            }
            if (!strLog.Equals(string.Empty))
            {
                ListLog.Add(strLog);
            }
        }

        private ZQM_Quality02 ReadAndCheckXMLiDoc(ref EdiZQMQalityXml item, ref string strLog)
        {
            //-- CHeck Kontrolle, korrekte Daten in DB geschrieben
            //-- XML erneut auslesen
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(item.iDocXml);
            ZQM_Quality02 zqm = new ZQM_Quality02(doc);
            string s = string.Empty;

            int iWorkspaceCheckId = AddressReferenceViewData.GETWorkspaceBySupplierReference(zqm.iRCVPRN, constValue_AsnArt.const_Art_XML_ZQM_QALITY02, 1);

            if (
                 (!item.Produktionsnummer.Equals(zqm.Produktionsnummer)) ||
                 (!item.LfsNr.Equals(zqm.LfsNo)) ||
                 (!item.WorkspaceXmlRef.Equals(zqm.iRCVPRN))
              )
            {
                //strLog = string.Empty;
                //strLog += "  - ";
                if (!item.LfsNr.Equals(zqm.LfsNo))
                {
                    strLog += Environment.NewLine;
                    strLog += "   |- Lfs: " + item.LfsNr + " ->  in Lfs: " + zqm.LfsNo + "  !!!";
                    item.LfsNr = zqm.LfsNo;
                }
                else
                {
                    strLog += Environment.NewLine;
                    strLog += "   |- Lfs: " + item.LfsNr;
                }

                if (!item.Produktionsnummer.Equals(zqm.Produktionsnummer))
                {
                    strLog += Environment.NewLine;
                    strLog += "   |- Produktionsnummer: " + item.Produktionsnummer + " -> " + zqm.Produktionsnummer + "  !!!";
                    item.Produktionsnummer = zqm.Produktionsnummer;
                }
                else
                {
                    strLog += Environment.NewLine;
                    strLog += "   |- Produktionsnummer: " + item.Produktionsnummer;
                }

                if (!item.WorkspaceXmlRef.Equals(zqm.iRCVPRN))
                {
                    strLog += Environment.NewLine;
                    strLog += "   |- Workspace: " + item.WorkspaceXmlRef + " -> " + zqm.iRCVPRN + "  !!!";
                    item.WorkspaceXmlRef = zqm.iRCVPRN;
                }
                else
                {
                    strLog += Environment.NewLine;
                    strLog += "   |- Workspace: " + item.WorkspaceXmlRef;
                }

                if (!item.WorkspaceId.Equals(iWorkspaceCheckId))
                {
                    strLog += Environment.NewLine;
                    strLog += "   |- Workspace-Id: " + item.WorkspaceId + " -> " + iWorkspaceCheckId + "  !!!";
                    item.WorkspaceId = iWorkspaceCheckId;
                }
                else
                {
                    strLog += Environment.NewLine;
                    strLog += "   |- Workspace-Id: " + item.WorkspaceId;
                }

                item.Description = strLog;

                EdiZQMQalityXmlViewData zqmVDCheck = new EdiZQMQalityXmlViewData(item);
                zqmVDCheck.Update();
            }
            return zqm;
        }

        public void CheckZertificateByArticleInSPL()
        {
            string strLog = string.Empty;
            sqlVD = new SperrlagerViewData();
            sqlVD.GetSPLArticleINCertificateMissing();
            if (sqlVD.ListArticleInSPL.Count > 0)
            {
                foreach (var item in sqlVD.ListArticleInSPL)
                {
                    Task.Delay(300);
                    strLog = string.Empty;
                    artVD = new ArticleViewData(item.ArtikelID, 1, false);
                    if (artVD.Artikel.Id > 0)
                    {
                        //strLog = "Artikel: "+ artVD.Artikel.LVS_ID+ "["+ artVD.Artikel.Id + "]| Lfs-Nr: "+ artVD.Artikel.Eingang.LfsNr + " | Produktionsnummer: " + artVD.Artikel.Produktionsnummer;
                        strLog = Environment.NewLine + "   |- Artikel: " + artVD.Artikel.LVS_ID + "[" + artVD.Artikel.Id + "]| Lfs-Nr: " + artVD.Artikel.Eingang.LfsNr + " | Produktionsnummer: " + artVD.Artikel.Produktionsnummer;



                        zqmVD = new EdiZQMQalityXmlViewData();
                        zqmVD.GetListItemsByLfsAndProductionNo(artVD.Artikel.Eingang.LfsNr, artVD.Artikel.Produktionsnummer);
                        if (zqmVD.ListZQMQalityActive.Count > 0)
                        {
                            foreach (var zItem in zqmVD.ListZQMQalityActive)
                            {
                                if (zItem.iDocXml.Length > 0)
                                {

                                    XmlDocument doc = new XmlDocument();
                                    doc.LoadXml(zItem.iDocXml);
                                    ZQM_Quality02 zqmTmp = new ZQM_Quality02(doc);

                                    if (
                                          (artVD.Artikel.Produktionsnummer.Equals(zqmTmp.Produktionsnummer)) &&
                                          (artVD.Artikel.Eingang.LfsNr.Equals(zqmTmp.LfsNo))
                                       )
                                    {
                                        strLog += Environment.NewLine;
                                        strLog += "   |-> ZQMXml-Id: [ " + zItem.Id + " ]  Zertifikatdaten vorhanden - Lfs-Nr: " + artVD.Artikel.Eingang.LfsNr + " | Produktionsnummer: " + artVD.Artikel.Produktionsnummer;

                                        zItem.LfsNr = zqmTmp.LfsNo;
                                        zItem.IsActive = true;
                                        zItem.Produktionsnummer = zqmTmp.Produktionsnummer;
                                        zItem.Description = Environment.NewLine + "Korrektur: ";
                                        zItem.Description += Environment.NewLine + "E1EDP02|069 =" + zqmTmp.Produktionsnummer + " ";
                                        zItem.Description += Environment.NewLine + "E1EDK02|012 =" + zqmTmp.LfsNo;

                                        EdiZQMQalityXmlViewData zqmVDCheck = new EdiZQMQalityXmlViewData(zItem);
                                        zqmVDCheck.Update();

                                        strLog += Environment.NewLine;
                                        strLog += "   |-> Korrektur in Lfs: " + zqmTmp.LfsNo + " | Produktionsnummer: " + zqmTmp.Produktionsnummer;

                                    }
                                    else
                                    {
                                        if (
                                            (zItem.LfsNr.Equals(zqmTmp.LfsNo)) &&
                                             (zItem.Produktionsnummer.Equals(zqmTmp.Produktionsnummer))
                                          )
                                        {
                                            strLog += Environment.NewLine;
                                            strLog += "   |-> ZQMXml-Id: [ " + zItem.Id + " ] -  Zertifikatdaten nicht passend für diesen Artikel!";
                                        }
                                        else
                                        {
                                            strLog += Environment.NewLine;
                                            strLog += "   |-> ZQMXml-Id: [ " + zItem.Id + " ] - Zertifikatdaten fehlerhaft - ID: " + zItem.Id + "| Lfs-Nr: " + zqmTmp.LfsNo + " | Produktionsnummer: " + zqmTmp.Produktionsnummer;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            strLog += Environment.NewLine;
                            strLog += "   |-> kein Zertifikat vorhanden!";
                        }
                    }
                    if (!strLog.Equals(string.Empty))
                    {
                        ListLog.Add(strLog);
                    }

                }
            }

        }
        /// <summary>
        ///             EIntrag in Artikel-Vita
        ///             Zertifikat fehlt - Buchung ins SPL
        /// </summary>
        /// <param name="myArticle"></param>
        /// <param name="myGLUser"></param>
        public static void ArtikelVitaInsert_ArticleToSPL(Articles myArticle, Globals._GL_USER myGLUser)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myArticle.Id;
            decimal tmpLEingangID = myArticle.Eingang.Id;
            decimal tmpLVSNr = myArticle.LVS_ID;
            string tmpAktion = enumLagerAktionen.SperrlagerOUT.ToString();
            string tmpBeschreibung = "Sperrlager IN: LVSNr [" + tmpLVSNr.ToString() + "] / Eingang [" + tmpLEingangID.ToString() + "] | Zertifikat fehlt!";
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myGLUser.User_ID, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myGLUser.User_ID);
        }
        /// <summary>
        ///             EIntrag in Artikel-Vita
        ///             Zertifikats-Check OK
        /// </summary>
        /// <param name="myArticle"></param>
        /// <param name="myGLUser"></param>
        /// <param name="myZqmItem"></param>
        public static void ArtikelVitaInsert_CertificateCheckOK(Articles myArticle, Globals._GL_USER myGLUser, EdiZQMQalityXml myZqmItem)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myArticle.Id;
            decimal tmpLEingangID = myArticle.Eingang.Id;
            decimal tmpLVSNr = myArticle.LVS_ID;
            string tmpAktion = enumLagerAktionen.SperrlagerOUT.ToString();
            string tmpBeschreibung = "Zertifikat Check OK - Zertifikat [" + myZqmItem.Id + "] : LVSNr [" + tmpLVSNr.ToString() + "] / Eingang [" + tmpLEingangID.ToString() + "]";
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myGLUser.User_ID, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myGLUser.User_ID);
        }
        public static bool ArticleCertificateIsMissing_BookingToSpl(Articles myArticle, Globals._GL_USER myGLUser)
        {
            bool bReturn = false;
            string strSql = string.Empty;
            clsSPL spl = new clsSPL();
            spl.InitClass(myGLUser);
            spl.ArtikelID = myArticle.Id;

            if (!spl.IsInSPL)
            {
                spl.Sperrgrund = "Artikel Zertifikat fehlt";
                spl.IsCustomCertificateMissing = true;
                //spl.Add(true);
                strSql += spl.AddStrSQL(true);
            }
            bool bOK = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "SPLCheckIN", 1);
            if (bOK)
            {
                CustomProcess_Novelis_AccessByArticleCert.ArtikelVitaInsert_ArticleToSPL(myArticle, myGLUser);
                //CustomProcess_Novelis_AccessByArticleCert.ArticleCertificateIsMissing_BookingToSpl(myArticle, myGLUser);
                bReturn = true;
            }
            return bReturn;
        }

    }
}
