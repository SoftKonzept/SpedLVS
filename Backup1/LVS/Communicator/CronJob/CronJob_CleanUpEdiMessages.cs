using LVS.Models;
using LVS.sqlStatementCreater;
using LVS.ViewData;
using LVS.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace LVS.Communicator.CronJob
{
    public class CronJob_CleanUpEdiMessages
    {
        /// <summary>
        ///                 Es sollen alle Edi-Messages ermittelt werden,
        ///                 deren enthaltene Artikel nicht mehr im Lager sich befinden.
        ///                 
        ///                 Diese Edi Daten sollen dann aus der DB Tabelle ASNValues gelöscht werden.
        /// </summary>
        /// 

        //public List<string> LogList { get; set; } = new List<string>();

        //public event Action<string> OnLogMessage;
        //public event Action<int> ProgressChanged;
        public event Action<int> ProgressMaxValue;

        internal sqlCreater_CronJobCleanUpEdiMessages_Eingang sqlCronJobCleanUpEingang;
        internal sqlCreater_CronJobCleanUpEdiMessages_EingangArticle sqlCronJobCleanUpEingangArticle;
        internal int iRetentionDays = 30;
        internal bool IsDebug { get; set; }
        public List<string> LogList { get; set; } = new List<string>();
        public CronJob_CleanUpEdiMessages()
        {
            iRetentionDays = InitValueCommunicator.InitValueCom_RetentionDays.Value(); 
            IsDebug = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myIsDebug"></param>
        public CronJob_CleanUpEdiMessages(bool myIsDebug) : this()
        {
            IsDebug = myIsDebug;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myWorkerBar"></param>
        /// <param name="bIsCommuncator"></param>
        public void StartCleaning(BackgroundWorker myWorkerBar, bool bIsCommuncator=false)
        {
            LogList = new List<string>();
            //-- Ermittle die Liste aller ASN/DFÜ
            AsnViewData asnViewData = new AsnViewData();
            asnViewData.GetListForClean();

            //-- Eingänge
            EingangViewData eingangViewData = new EingangViewData();
            int iLoopAsn = 0;
            List<Asn> ListAsn = new List<Asn>(asnViewData.ListAsn);
            ProgressMaxValue?.Invoke(ListAsn.Count);

            foreach (var asn in ListAsn)
            {
                string log = string.Empty;
                bool bDeleteAsn = false;
                log = $"Starte Prüfung ASN ID: {asn.Id}" + Environment.NewLine;
                //-- Ermittle alle Eingänge zu dieser ASN
                List<ctrCleanAsnTables_Eingang> tmpListEingaenge = GetEingangList(asn);
                if (tmpListEingaenge.Count > 0)
                {
                    int iLoopEingang = 0;
                    foreach (var eingang in tmpListEingaenge)
                    {
                        iLoopEingang++;
                        log += $" > Eingang: [{eingang.EingangTableId}] {eingang.EingangID}" + Environment.NewLine;
                        //-- check Eingang hat Artikel
                        if (eingang.ListEingangArtikel.Count > 0)
                        {
                            //-- wenn firstordefault == null, dann sind alle Artikel ausgelagert
                            if (eingang.ListEingangArtikel.FirstOrDefault(x => x.LAusgangTableId == 0) is null)
                            {
                                //-- check alle Artikel ausgelagert und letzte Ausgangsdatum älter als xx Tage
                                DateTime dtCompare = DateTime.Now.AddDays((iRetentionDays * (-1)));
                                var tmpArt = eingang.ListEingangArtikel.Where(x => x.Ausgangdatum > dtCompare).ToList();
                                if (tmpArt.Count == 0)
                                {
                                    log += $" > alle Article ausgelagert!" + Environment.NewLine;
                                    bDeleteAsn = true;
                                }
                            }
                            else
                            {
                                var tmpArt = eingang.ListEingangArtikel.Where(x => x.LAusgangTableId == 0).ToList();
                                foreach (var art in tmpArt)
                                {
                                    log += $" > Article: [{art.AritkelId}] {art.LvsNr} nicht ausgelagert!" + Environment.NewLine;
                                }
                            }
                        }
                        else
                        {
                            log += $" > keine Artikel" + Environment.NewLine;
                        }
                    }
                }
                else
                {
                    log += $" > keine Eingänge" + Environment.NewLine;
                    //-- keien Eingänge / keine ARtikel vorhanden
                    bDeleteAsn = true;
                }

                if (bDeleteAsn)
                {
                    if (IsDebug)
                    {
                        log += $" >>> [DEBUG] > ASN nicht gelöscht" + Environment.NewLine;
                    }
                    else
                    {
                        //--- asn wird gelöscht
                        asnViewData = new AsnViewData(asn);
                        if (asnViewData.Delete())
                        {
                            log += $" >>>  ASN gelöscht" + Environment.NewLine;
                        }
                    }
                }
                else
                {
                    log += $" >>> NICHT bearbeitet" + Environment.NewLine;
                }
                iLoopAsn++;

                int progress = iLoopAsn;  //(iLoopAsn + 1) * 100 / ListAsn.Count;
                //ProgressChanged?.Invoke(progress);

                if (bIsCommuncator)
                {
                    LogList.Add(log);
                }
                else 
                {
                    myWorkerBar.ReportProgress(progress, log);
                }               
                log = string.Empty;
                if (iLoopAsn > 500)
                {
                    break;
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="myAsn"></param>
        /// <returns></returns>
        private List<ctrCleanAsnTables_Eingang> GetEingangList(Asn myAsn)
        {
            List<ctrCleanAsnTables_Eingang> lst = new List<ctrCleanAsnTables_Eingang>();
            string strSql = string.Empty;
            DataTable dt = new DataTable("Artikel");
            if (myAsn.Id > 0)
            {
                //--- Init SQL
                sqlCronJobCleanUpEingang = new sqlCreater_CronJobCleanUpEdiMessages_Eingang(myAsn);
                strSql = sqlCronJobCleanUpEingang.sql_GetEingaengeByAsn;
                dt = LVS.clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "EingangArtikel", "ArticleEingang", 1);

                foreach (DataRow r in dt.Rows)
                {
                    ctrCleanAsnTables_Eingang tmpEingang = new ctrCleanAsnTables_Eingang();
                    tmpEingang.AsnId = myAsn.Id;
                    int iTmp = 0;
                    int.TryParse(r["Id"].ToString(), out iTmp);
                    tmpEingang.EingangTableId = iTmp;

                    iTmp = 0;
                    int.TryParse(r["LEingangID"].ToString(), out iTmp);
                    tmpEingang.EingangID = iTmp;

                    DateTime dtTmp = DateTime.MaxValue;
                    DateTime.TryParse(r["Date"].ToString(), out dtTmp);
                    tmpEingang.Eingangdatum = dtTmp;

                    tmpEingang.ListEingangArtikel = GetEingangArtikelList(tmpEingang.EingangTableId);

                    if (lst.Contains(tmpEingang) == false)
                        lst.Add(tmpEingang);
                }
            }
            return lst;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myEingangTableId"></param>
        /// <returns></returns>
        private List<ctrCleanAsnTables_EingangArtikel> GetEingangArtikelList(int myEingangTableId)
        {
            List<ctrCleanAsnTables_EingangArtikel> lst = new List<ctrCleanAsnTables_EingangArtikel>();
            string strSQL = string.Empty;
            DataTable dt = new DataTable("Artikel");
            if (myEingangTableId > 0)
            {
                sqlCronJobCleanUpEingangArticle = new sqlCreater_CronJobCleanUpEdiMessages_EingangArticle(myEingangTableId);
                strSQL = sqlCronJobCleanUpEingangArticle.sql_GetEingaengArtikel;

                dt = LVS.clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "EingangArtikel", "ArticleEingang", 1);

                foreach (DataRow r in dt.Rows)
                {
                    ctrCleanAsnTables_EingangArtikel tmpArt = new ctrCleanAsnTables_EingangArtikel();
                    int iTmp = 0;
                    int.TryParse(r["ID"].ToString(), out iTmp);
                    tmpArt.AritkelId = iTmp;

                    iTmp = 0;
                    int.TryParse(r["LVSNR"].ToString(), out iTmp);
                    tmpArt.LvsNr = iTmp;

                    iTmp = 0;
                    int.TryParse(r["ASN"].ToString(), out iTmp);
                    tmpArt.AsnId = iTmp;

                    iTmp = 0;
                    int.TryParse(r["LAusgangTableId"].ToString(), out iTmp);
                    tmpArt.LAusgangTableId = iTmp;

                    DateTime dtTmp = DateTime.MaxValue;
                    DateTime.TryParse(r["Ausgangdatum"].ToString(), out dtTmp);
                    tmpArt.Ausgangdatum = dtTmp;

                    if (!lst.Contains(tmpArt))
                    {
                        lst.Add(tmpArt);
                    }
                }
            }
            return lst;
        }

    }
}
