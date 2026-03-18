using LVS.Models;
using LVS.sqlStatementCreater;
using LVS.ViewData;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;

namespace LVS.Communicator.CronJob
{

    public class CronJob_StoclistAutoSend
    {
        /// <summary>
        /// 
        /// </summary>
        public bool ProzessExcecuted { get; set; } = false;
        public List<clsLogbuchCon> ListLogInsert { get; set; } = new List<clsLogbuchCon>();
        public string AttachmentPath = "C:\\LVS\\ComData\\";

        public CronJob_StoclistAutoSend(CronJobs myCronJob, Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSystem, clsSystem mySystem)
        {
            ListLogInsert = new List<clsLogbuchCon>();
            Globals._GL_USER GLUser = myGLUser;
            Globals._GL_SYSTEM GLSystem = myGLSystem;
            clsSystem system = mySystem;

            List<decimal> ListAdrIDExcel = clsMailingList.GetAutoMailingList(myCronJob.AdrId, CronJobViewData.const_autoBestandExcel);
            if (ListAdrIDExcel.Count > 0)
            {
                //clsMailingList Mailinglist = new clsMailingList();
                for (Int32 i = 0; i <= ListAdrIDExcel.Count - 1; i++)
                {
                    decimal AdrID = (decimal)ListAdrIDExcel[i];

                    LVS.ViewData.AddressViewData adrVD = new LVS.ViewData.AddressViewData(myCronJob.AdrId, 1);
                    clsMailingList Mailinglist = new clsMailingList();
                    //Mailinglist.InitClass(this.GL_User, this.GLSystem, AdrID);
                    Mailinglist.InitClass(GLUser, GLSystem, myCronJob.AdrId);
                    Mailinglist.FillListMailAdressenForAutoBestand(AdrID, CronJobViewData.const_autoBestandExcel);
                    if (Mailinglist.ListMailadressen.Count > 0)
                    {
                        //--- mr muss überarbeitet werden
                        string strSql = string.Empty;
                        sqlCreater_Stocks_DailyStockAcrossAllWorkspaces sql = new sqlCreater_Stocks_DailyStockAcrossAllWorkspaces((int)adrVD.Address.Id, 0, string.Empty, DateTime.Now, DateTime.Now, false, false);
                        strSql += sqlCreater_Stocks.Sql_Main_Communicator();
                        strSql += sql.sql_Statement;

                        DataTable dtGewBestand = new DataTable("Bestand");
                        dtGewBestand = clsSQLcon.ExecuteSQL_GetDataTable(strSql, 1, "Bestand");
                        if (dtGewBestand.Columns.Contains("SPL"))
                        {
                            dtGewBestand.Columns.Remove("SPL");
                        }
                        LVS.clsExcel Excel = new clsExcel();
                        string FileName = DateTime.Now.ToString("yyyy_MM_dd_HHmmsss_");

                        switch (myCronJob.Periode)
                        {
                            case "stündlich":
                                FileName += "BestandHH_" + DateTime.Now.ToString("HH");
                                break;
                            case "täglich":
                                FileName += "BestandTag_" + DateTime.Now.ToString("yyyy_MM_dd");
                                break;
                            case "wöchentlich":
                                FileName += "BestandKW_" + (Functions.GetCalendarWeek(DateTime.Now));
                                break;
                            case "monatlich":
                                FileName += "BestandMonat_" + DateTime.Now.ToString("MM");
                                break;
                            case "jährlich":
                                FileName += "BestandJahr_" + DateTime.Now.ToString("yyyy");
                                break;
                        }
                        FileName += "_KD_" + adrVD.Address.ViewId + "_" + adrVD.Address.Name1;
                        helper_IOFile.CheckPath(AttachmentPath);
                        string FilePath = Path.Combine(AttachmentPath, FileName + ".xlsx");
                        LVS.NugetPac.Epplus_Excel export = new NugetPac.Epplus_Excel(dtGewBestand, FilePath);
                        //if(export.pa)

                        List<string> listAttach = new List<string>();
                        listAttach.Add(FilePath);
                        if (listAttach.Count > 0)
                        {
                            clsMail Mail = new clsMail();
                            Mail.InitClass(GLUser, system);
                            Mail.ListAttachment = listAttach;

                            try
                            {
                                string strSubject = string.Empty;
                                if (system.DebugModeCOM)
                                {
                                    strSubject += "[DEGBUG] - ";
                                }
                                strSubject += "Bestand Kunde:" + adrVD.Address.ViewId + " - " + adrVD.Address.Name1 + " vom " + DateTime.Now.ToString("dd.MM.yyyy HH:mm");

                                Mail.ListMailReceiver = Mailinglist.ListMailadressen;
                                Mail.Subject = strSubject;
                                ProzessExcecuted = Mail.SendNoReply();
                            }
                            finally
                            {
                                if (ProzessExcecuted)
                                {
                                    clsLogbuchCon tmpLog = new clsLogbuchCon();
                                    tmpLog.ID = 0;
                                    tmpLog.Typ = enumLogArtItem.autoMail.ToString();
                                    tmpLog.LogText = "  -> Adresse ID/Matchcode: [" + adrVD.Address.Id.ToString() + "/" + adrVD.Address.ViewId + "] " + adrVD.Address.Name1 + " -> Mail erfolgreich versendet...";
                                    ListLogInsert.Add(tmpLog);
                                }
                                else
                                {
                                    clsLogbuchCon tmpLog = new clsLogbuchCon();
                                    tmpLog.ID = 0;
                                    tmpLog.Typ = enumLogArtItem.autoMail.ToString();
                                    tmpLog.LogText = "  -> Adresse ID/Matchcode: [" + adrVD.Address.Id.ToString() + "/" + adrVD.Address.ViewId + "] " + adrVD.Address.Name1 + " -> Mail NICHT versendet...";
                                    ListLogInsert.Add(tmpLog);
                                }
                            }
                        }
                        else
                        {
                            //kein Attachment - keine Anhang
                            clsLogbuchCon tmpLog = new clsLogbuchCon();
                            tmpLog.ID = 0;
                            tmpLog.Typ = enumLogArtItem.autoMail.ToString();
                            tmpLog.LogText = "  -> Adresse ID/Matchcode: [" + adrVD.Address.Id.ToString() + "/" + adrVD.Address.ViewId + "] " + adrVD.Address.Name1 + " -> es konnte kein Anhang gefunden werden...";
                            ListLogInsert.Add(tmpLog);
                        }
                        Thread.Sleep(1000);
                    }
                }
            }
        }
    }
}
