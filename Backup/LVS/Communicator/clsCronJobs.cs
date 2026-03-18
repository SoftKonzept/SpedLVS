using LVS.Models;
using LVS.sqlStatementCreater;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using Telerik.Reporting;


namespace LVS
{
    public class clsCronJobs
    {
        public const string const_autoBestand = "#autoBestand#";
        public const string const_autoBestandExcel = "#autoBestandExcel#";
        public const string const_autoGewBestand = "#autoGewBestand#";
        public const string const_autoGewBestandExcel = "#autoGewBestandExcel#";
        public const string const_autoJournal = "#autoJournal#";
        public const string const_autoJournalExcel = "#autoJournalExcel#";


        public InstanceReportSource repSource;
        public UriReportSource uRepSource;
        public clsINI.clsINI INI_Rep;
        public clsSystem SYSTEM;
        public Globals._GL_SYSTEM GLSystem;
        public Globals._GL_USER GL_User;

        internal clsMailingList Mailinglist;
        internal clsMail Mail;
        public DataTable dtCronJobs;
        public List<clsLogbuchCon> ListLogInsert;

        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = GL_User.User_ID;
                return _BenutzerID;
            }
            set { _BenutzerID = value; }
        }

        public decimal ID { get; set; }
        //public string Aktion { get; set; }
        public enumCronJobAction Aktion { get; set; }
        public string Beschreibung { get; set; }
        public DateTime Aktionsdatum { get; set; }
        public string Periode { get; set; }
        public DateTime vZeitraum { get; set; }
        public DateTime bZeitraum { get; set; }
        public bool aktiv { get; set; }
        public int AdrId { get; set; } = 0;
        public string StartupPath { get; set; }

        public string AttachmentPath = "C:\\LVS\\ComData\\";
        public string FileJournalName = DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + "_Journal.PDF";
        public string FileBestandName = DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + "_Bestandsliste.PDF";
        /*****************************************************************************************
         *                              Methoden / Procedure
         * **************************************************************************************/
        ///<summary>clsCronJobs / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_SYSTEM myGLSystem, Globals._GL_USER myGLUser, clsSystem mySystem)
        {
            this.SYSTEM = mySystem;
            this.GL_User = myGLUser;
            this.GLSystem = myGLSystem;
            this.StartupPath = mySystem.StartupPath;
            //dtCronJobs = GetCronJobs();
            string strTest = this.SYSTEM.WorkingPathExport;
        }
        ///<summary>clsCronJobs / Add</summary>
        ///<remarks></remarks>
        public void Add()
        {
            string strSql = "INSERT INTO CronJobs ( Aktion, Beschreibung, Aktionsdatum, Periode, vZeitraum, bZeitraum, aktiv, AdrId" +
                                                ") " +
                                            "VALUES ('" + Aktion.ToString() + "'" +
                                                    ", '" + Beschreibung + "'" +
                                                    ", '" + Aktionsdatum + "'" +
                                                    ", '" + Periode + "'" +
                                                    ", '" + this.vZeitraum + "'" +
                                                    ", '" + this.bZeitraum + "'" +
                                                    ", " + Convert.ToInt32(this.aktiv) +
                                                    ", " + this.AdrId +
                                                    ");";
            strSql = strSql + " Select  @@IDENTITY; ";
            string strTmp = clsSQLCOM.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0;
            Decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                this.ID = decTmp;
            }
        }
        ///<summary>clsCronJobs / Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            try
            {
                enumCronJobAction tmpEnum = enumCronJobAction.Default;
                DataTable dt = new DataTable("CronJobs");
                string strSQL = string.Empty;
                strSQL = "SELECT * FROM CronJobs WHERE ID=" + ID + ";";
                dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "CronJobs");
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    this.ID = (decimal)dt.Rows[i]["ID"];
                    Enum.TryParse(dt.Rows[i]["Aktion"].ToString(), out tmpEnum);
                    this.Aktion = tmpEnum;
                    this.Beschreibung = dt.Rows[i]["Beschreibung"].ToString();
                    DateTime dtTmp = DateTime.MinValue;
                    DateTime.TryParse(dt.Rows[i]["Aktionsdatum"].ToString(), out dtTmp);
                    this.Aktionsdatum = dtTmp;
                    this.Periode = dt.Rows[i]["Periode"].ToString().ToString();
                    dtTmp = DateTime.MinValue;
                    DateTime.TryParse(dt.Rows[i]["vZeitraum"].ToString(), out dtTmp);
                    this.vZeitraum = dtTmp;
                    dtTmp = DateTime.MinValue;
                    DateTime.TryParse(dt.Rows[i]["bZeitraum"].ToString(), out dtTmp);
                    this.bZeitraum = dtTmp;
                    bool bTmp = false;
                    int iTmp = 0;
                    int.TryParse(dt.Rows[i]["aktiv"].ToString(), out iTmp);
                    if (iTmp == 1)
                    {
                        bTmp = true;
                    }
                    this.aktiv = bTmp;
                    iTmp = 0;
                    int.TryParse(dt.Rows[i]["AdrId"].ToString(), out iTmp);
                    this.AdrId = iTmp;
                }
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }
        }
        ///<summary>clsCronJobs / UpdateNextAktionsdatum</summary>
        ///<remarks></remarks>
        //public void UpdateNextAktionsdatum(CronJobs myCronJob)
        //{
        //    switch (myCronJob.Periode)
        //    {
        //        case "stündlich":
        //            myCronJob.Aktionsdatum = myCronJob.Aktionsdatum.AddHours(1);
        //            break;
        //        case "täglich":
        //            //this.Aktionsdatum = this.Aktionsdatum.AddDays(1);
        //            while (myCronJob.Aktionsdatum < DateTime.Now)
        //            {
        //                myCronJob.Aktionsdatum = myCronJob.Aktionsdatum.AddDays(1);
        //            }
        //            break;
        //        case "wöchentlich":
        //            myCronJob.Aktionsdatum = myCronJob.Aktionsdatum.AddDays(7);
        //            while (myCronJob.Aktionsdatum < DateTime.Now)
        //            {
        //                myCronJob.Aktionsdatum = myCronJob.Aktionsdatum.AddDays(7);
        //            }

        //            break;
        //        case "monatlich":
        //            myCronJob.Aktionsdatum = myCronJob.Aktionsdatum.AddMonths(1);
        //            while (myCronJob.Aktionsdatum < DateTime.Now)
        //            {
        //                myCronJob.Aktionsdatum = myCronJob.Aktionsdatum.AddMonths(1);
        //            }
        //            break;
        //        case "jährlich":
        //            myCronJob.Aktionsdatum = myCronJob.Aktionsdatum.AddYears(1);
        //            while (myCronJob.Aktionsdatum < DateTime.Now)
        //            {
        //                myCronJob.Aktionsdatum = myCronJob.Aktionsdatum.AddYears(1);
        //            }
        //            break;
        //    }
        //    string strSQL = string.Empty;
        //    strSQL = "Update CronJobs " +
        //                    "SET Aktionsdatum='" + myCronJob.Aktionsdatum + "' " +
        //                    "WHERE ID=" + myCronJob.Id + ";";
        //    clsSQLCOM.ExecuteSQL(strSQL, BenutzerID);
        //}
        /// <summary>
        ///             Löscht den Datensatz
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            string strSql = "DELETE FROM CronJobs WHERE ID=" + this.ID + ";";
            bool bReturn = clsSQLCOM.ExecuteSQL(strSql, BenutzerID);
            return bReturn;
        }
        ///<summary>clsCronJobs / Update</summary>
        ///<remarks></remarks>
        public void Update()
        {
            string strSQL = string.Empty;
            strSQL = "Update CronJobs " +
                            "SET " +
                                "Aktion='" + this.Aktion + "'" +
                                ", Beschreibung='" + this.Beschreibung + "'" +
                                ", Aktionsdatum='" + this.Aktionsdatum + "'" +
                                ", vZeitraum ='" + this.vZeitraum + "'" +
                                ", bZeitraum ='" + this.bZeitraum + "'" +
                                ", Periode='" + this.Periode + "'" +
                                ", AdrId=" + this.AdrId +
                            " WHERE ID=" + ID + ";";
            clsSQLCOM.ExecuteSQL(strSQL, BenutzerID);
        }
        ///<summary>clsCronJobs / Fill</summary>
        ///<remarks></remarks>
        //public DataTable GetCronJobs()
        //{
        //    DataTable dt = new DataTable("CronJobs");
        //    string strSQL = string.Empty;
        //    strSQL = "SELECT * FROM CronJobs " +
        //                        " WHERE " +
        //                            //"DATEDIFF(dd, Aktionsdatum, '" + DateTime.Now + "')>=0 " +
        //                            "DATEDIFF(MI, Aktionsdatum, '" + DateTime.Now + "')>=0 " +
        //                            " AND Aktionsdatum between vZeitraum and bZeitraum " +
        //                            " AND aktiv=1 ;";
        //    dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "CronJobs");
        //    return dt;
        //}
        ///<summary>clsCronJobs / DoAutoJournal</summary>
        ///<remarks></remarks>
        //public bool DoAutoBestand(int myAdrId)
        public bool DoAutoBestand(CronJobs myCronJob)
        {
            bool bSendOK = false;
            ListLogInsert = new List<clsLogbuchCon>();
            //Ermitteln aller Adressen mit den hinterlegten Verteiler mit dem Name #autJournal#
            List<decimal> ListAdrIDExcel = clsMailingList.GetAutoMailingList(myCronJob.AdrId, const_autoBestandExcel);
            if (ListAdrIDExcel.Count > 0)
            {
                Mailinglist = new clsMailingList();
                for (Int32 i = 0; i <= ListAdrIDExcel.Count - 1; i++)
                {
                    decimal AdrID = (decimal)ListAdrIDExcel[i];

                    clsADR tmpADR = new clsADR();
                    tmpADR._GL_User = this.GL_User;
                    tmpADR.ID = AdrID;
                    tmpADR.Fill();

                    Mailinglist.InitClass(this.GL_User, this.GLSystem, AdrID);
                    Mailinglist.FillListMailAdressenForAutoBestand(AdrID, const_autoBestandExcel);
                    if (Mailinglist.ListMailadressen.Count > 0)
                    {
                        //--- mr muss überarbeitet werden
                        string strSql = string.Empty;
                        sqlCreater_Stocks_DailyStockAcrossAllWorkspaces sql = new sqlCreater_Stocks_DailyStockAcrossAllWorkspaces((int)tmpADR.ID, 0, string.Empty, DateTime.Now, DateTime.Now, false, false);
                        strSql += sqlCreater_Stocks.Sql_Main_Communicator();
                        strSql += sql.sql_Statement;

                        DataTable dtGewBestand = new DataTable("Bestand");
                        dtGewBestand = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Bestand");
                        if (dtGewBestand.Columns.Contains("SPL"))
                        {
                            dtGewBestand.Columns.Remove("SPL");
                        }
                        LVS.clsExcel Excel = new clsExcel();

                        //string FileName = "BestandKW" + (Functions.GetCalendarWeek(DateTime.Now)) + "_" + tmpADR.ViewID;
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
                        FileName += "_KD_" + tmpADR.ViewID;
                        helper_IOFile.CheckPath(AttachmentPath);
                        //string FilePath = Excel.ExportDataTableToWorksheet(dtGewBestand, AttachmentPath + "\\" + FileName);
                        //string FilePath = Excel.ExportDataTableToExcel(dtGewBestand, FileName, AttachmentPath);
                        string FilePath = Path.Combine(AttachmentPath, FileName + ".xlsx");
                        //LVS.Packages.OfficeInteropExcel_Export export = new Packages.OfficeInteropExcel_Export(dtGewBestand, FilePath);
                        //LVS.ExcelExport.closedXml_Excel ExcelExport = new LVS.ExcelExport.closedXml_Excel(dtGewBestand, FilePath);
                        //closedXml_Excel ExcelExport = new closedXml_Excel(dtGewBestand, FilePath);
                        //clsExcel exp = new clsExcel();
                        //exp.ExportDataTableToExcel(dtGewBestand, FileName, AttachmentPath);
                        LVS.NugetPac.Epplus_Excel export = new NugetPac.Epplus_Excel(dtGewBestand, FilePath);

                        List<string> listAttach = new List<string>();
                        listAttach.Add(FilePath);
                        if (listAttach.Count > 0)
                        {
                            Mail = new clsMail();
                            Mail.InitClass(this.GL_User, this.SYSTEM);
                            Mail.ListAttachment = listAttach;

                            try
                            {
                                string strSubject = string.Empty;
                                if (this.SYSTEM.DebugModeCOM)
                                {
                                    strSubject += "[DEGBUG] - ";
                                }
                                strSubject += "Bestand Kunde:" + tmpADR.ViewID + " - " + tmpADR.Name1 + " vom " + DateTime.Now.ToString("dd.MM.yyyy HH:mm");


                                //string strSubj = this.GLSystem.sy
                                Mail.ListMailReceiver = Mailinglist.ListMailadressen;
                                //Mail.Subject = "Bestand Kunde:" + tmpADR.ViewID + " - "+ tmpADR.Name1 +" vom " + DateTime.Now.ToString("dd.MM.yyyy HH:mm");
                                Mail.Subject = strSubject;
                                bSendOK = Mail.SendNoReply();
                            }
                            finally
                            {
                                if (bSendOK)
                                {
                                    //UpdateNextAktionsdatum(myCronJob);
                                    clsLogbuchCon tmpLog = new clsLogbuchCon();
                                    tmpLog.ID = 0;
                                    tmpLog.Typ = enumLogArtItem.autoMail.ToString();
                                    tmpLog.LogText = " -> Adresse ID/Matchcode: [" + tmpADR.ID.ToString() + "/" + tmpADR.ViewID + "] " + tmpADR.Name1 + " -> Mail erfolgreich versendet...";
                                    ListLogInsert.Add(tmpLog);
                                }
                                else
                                {
                                    clsLogbuchCon tmpLog = new clsLogbuchCon();
                                    tmpLog.ID = 0;
                                    tmpLog.Typ = enumLogArtItem.autoMail.ToString();
                                    tmpLog.LogText = " -> Adresse ID/Matchcode: [" + tmpADR.ID.ToString() + "/" + tmpADR.ViewID + "] " + tmpADR.Name1 + " -> nicht Mail erfolgreich versendet...";
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
                            tmpLog.LogText = " -> Adresse ID/Matchcode: [" + tmpADR.ID.ToString() + "/" + tmpADR.ViewID + "] " + tmpADR.Name1 + " -> es konnte kein Anhang gefunden werden...";
                            ListLogInsert.Add(tmpLog);
                        }
                        Thread.Sleep(1000);
                    }
                }
            }


            //List<decimal> ListAdrID = clsMailingList.GetAutoMailingList(this.GL_User, const_autoBestand);
            //if (ListAdrID.Count > 0)
            //{
            //    Mailinglist = new clsMailingList();

            //    for (Int32 i = 0; i <= ListAdrID.Count - 1; i++)
            //    {
            //        decimal AdrID = (decimal)ListAdrID[i];

            //        clsADR tmpADR = new clsADR();
            //        tmpADR._GL_User = this.GL_User;
            //        tmpADR.ID = AdrID;
            //        tmpADR.Fill();

            //        Mailinglist.InitClass(this.GL_User, this.GLSystem, AdrID);
            //        Mailinglist.FillListMailAdressenForAutoBestand(AdrID, const_autoBestand);
            //        if (Mailinglist.ListMailadressen.Count > 0)
            //        {
            //            uRepSource = new UriReportSource();
            //            uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("AuftraggeberID", AdrID));
            //            uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("Stichtag", DateTime.Now.AddDays(-1)));
            //            //uRepSource.Uri = Application.StartupPath + this.GLSystem.Doc_DocBestandAutoMail;
            //            uRepSource.Uri = StartupPath + this.GLSystem.Doc_DocBestandAutoMail;
            //            string FilePath = AttachmentPath + "\\" + tmpADR.ID.ToString() + "_" + FileBestandName;

            //            clsPrint.PrintDirectToPDF(FileBestandName, FilePath, uRepSource);
            //            //PrintDirectToPDF(FileBestandName, FilePath, uRepSource);

            //            List<string> listAttach = new List<string>();
            //            listAttach.Add(FilePath);
            //            if (listAttach.Count > 0)
            //            {
            //                Mail = new clsMail();
            //                Mail.InitClass(this.GL_User, this.SYSTEM);
            //                Mail.ListAttachment = listAttach;
            //                bool bSendOK = true;
            //                try
            //                {
            //                    Mail.ListMailReceiver = Mailinglist.ListMailadressen;
            //                    Mail.Subject = "Bestand";
            //                    bSendOK = Mail.SendNoReply();
            //                }
            //                finally
            //                {
            //                    if (bSendOK)
            //                    {
            //                        clsLogbuchCon tmpLog = new clsLogbuchCon();
            //                        tmpLog.ID = 0;
            //                        tmpLog.Typ = enumLogArtItem.autoMail.ToString();
            //                        tmpLog.LogText = " -> Adresse ID/Matchcode: [" + tmpADR.ID.ToString() + "/" + tmpADR.ViewID + "] -> Mail erfolgreich versendet...";
            //                        ListLogInsert.Add(tmpLog);
            //                    }
            //                    else
            //                    {
            //                        clsLogbuchCon tmpLog = new clsLogbuchCon();
            //                        tmpLog.ID = 0;
            //                        tmpLog.Typ = enumLogArtItem.autoMail.ToString();
            //                        tmpLog.LogText = " -> Adresse ID/Matchcode: [" + tmpADR.ID.ToString() + "/" + tmpADR.ViewID + "] -> nicht Mail erfolgreich versendet...";
            //                        ListLogInsert.Add(tmpLog);
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                //kein Attachment - keine Anhang
            //                clsLogbuchCon tmpLog = new clsLogbuchCon();
            //                tmpLog.ID = 0;
            //                tmpLog.Typ = enumLogArtItem.autoMail.ToString();
            //                tmpLog.LogText = " -> Adresse ID/Matchcode: [" + tmpADR.ID.ToString() + "/" + tmpADR.ViewID + "] -> es konnte kein Anhang gefunden werden...";
            //                ListLogInsert.Add(tmpLog);
            //            }
            //            Thread.Sleep(1000);
            //        }
            //    }
            //}
            return bSendOK;
        }
        ///<summary>clsCronJobs / DoAutoJournal</summary>
        ///<remarks></remarks>
        public void DoAutoSendToDispo()
        {

            //--------- FUNKTION noch nicht komplett / korrekt
            //-- war gedacht um Dispo Stände automatisch zu übermitteln

            ListLogInsert = new List<clsLogbuchCon>();
            //Ermitteln aller Adressen mit den hinterlegten Verteiler mit dem Name #autoDispo#
            List<decimal> ListAdrIDExcel = clsMailingList.GetAutoMailingList(this.GL_User, const_autoBestandExcel);
            if (ListAdrIDExcel.Count > 0)
            {
                Mailinglist = new clsMailingList();

                for (Int32 i = 0; i <= ListAdrIDExcel.Count - 1; i++)
                {
                    decimal AdrID = (decimal)ListAdrIDExcel[i];

                    clsADR tmpADR = new clsADR();
                    tmpADR._GL_User = this.GL_User;
                    tmpADR.ID = AdrID;
                    tmpADR.Fill();

                    Mailinglist.InitClass(this.GL_User, this.GLSystem, AdrID);
                    Mailinglist.FillListMailAdressenForAutoBestand(AdrID, const_autoBestandExcel);
                    if (Mailinglist.ListMailadressen.Count > 0)
                    {

                        string strSql = string.Empty;
                        strSql = "select count(*) from ASN where Datediff(MINUTE,Datum,GETDATE())<60*10 and ASNTypID=14 ";
                        string strAnzahl = clsSQLCOM.ExecuteSQL_GetValue(strSql, this.BenutzerID);
                        decimal decTmp = 0;
                        decimal.TryParse(strAnzahl, out decTmp);
                        if (decTmp > 0)
                        {
                            uRepSource = new UriReportSource();
                            uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("AuftraggeberID", AdrID));
                            uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("Stichtag", DateTime.Now.AddDays(-1)));
                            //uRepSource.Uri = Application.StartupPath + this.GLSystem.Doc_DocBestandAutoMail;
                            uRepSource.Uri = StartupPath + this.GLSystem.Doc_DocBestandAutoMail;
                            string FilePath = AttachmentPath + "\\" + tmpADR.ID.ToString() + "_" + FileBestandName;

                            clsPrint.PrintDirectToPDF(FileBestandName, FilePath, uRepSource);
                            //PrintDirectToPDF(FileBestandName, FilePath, uRepSource);

                            List<string> listAttach = new List<string>();
                            listAttach.Add(FilePath);
                            if (listAttach.Count > 0)
                            {
                                Mail = new clsMail();
                                Mail.InitClass(this.GL_User, this.SYSTEM);
                                Mail.ListAttachment = listAttach;
                                bool bSendOK = true;
                                try
                                {
                                    Mail.ListMailReceiver = Mailinglist.ListMailadressen;
                                    Mail.Subject = "Bestand " + tmpADR.ViewID;
                                    bSendOK = Mail.SendNoReply();
                                }
                                finally
                                {
                                    if (bSendOK)
                                    {
                                        clsLogbuchCon tmpLog = new clsLogbuchCon();
                                        tmpLog.ID = 0;
                                        tmpLog.Typ = enumLogArtItem.autoMail.ToString();
                                        tmpLog.LogText = " -> Adresse ID/Matchcode: [" + tmpADR.ID.ToString() + "/" + tmpADR.ViewID + "] -> Mail erfolgreich versendet...";
                                        ListLogInsert.Add(tmpLog);
                                    }
                                    else
                                    {
                                        clsLogbuchCon tmpLog = new clsLogbuchCon();
                                        tmpLog.ID = 0;
                                        tmpLog.Typ = enumLogArtItem.autoMail.ToString();
                                        tmpLog.LogText = " -> Adresse ID/Matchcode: [" + tmpADR.ID.ToString() + "/" + tmpADR.ViewID + "] -> nicht Mail erfolgreich versendet...";
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
                                tmpLog.LogText = " -> Adresse ID/Matchcode: [" + tmpADR.ID.ToString() + "/" + tmpADR.ViewID + "] -> es konnte kein Anhang gefunden werden...";
                                ListLogInsert.Add(tmpLog);
                            }
                        }
                        Thread.Sleep(1000);
                    }
                }
            }
        }

        ///<summary>clsCronJobs / DoAutoGewBestand</summary>
        ///<remarks></remarks>
        public void DoAutoGewBestand()
        {
            ListLogInsert = new List<clsLogbuchCon>();
            //Ermitteln aller Adressen mit den hinterlegten Verteiler mit dem Name #autJournal#
            List<decimal> ListAdrID;

            List<decimal> ListAdrIDExcel = clsMailingList.GetAutoMailingList(this.GL_User, const_autoGewBestandExcel);
            if (ListAdrIDExcel.Count > 0)
            {
                Mailinglist = new clsMailingList();

                for (Int32 i = 0; i <= ListAdrIDExcel.Count - 1; i++)
                {
                    decimal AdrID = (decimal)ListAdrIDExcel[i];

                    clsADR tmpADR = new clsADR();
                    tmpADR._GL_User = this.GL_User;
                    tmpADR.ID = AdrID;
                    tmpADR.Fill();

                    Mailinglist.InitClass(this.GL_User, this.GLSystem, AdrID);
                    Mailinglist.FillListMailAdressenForAuto(AdrID, const_autoGewBestandExcel);
                    if (Mailinglist.ListMailadressen.Count > 0)
                    {
                        /*
                  
                         // KEIN REPORT SONDERN EINE TABELLE ALS EXCEL FORMAT 
                        uRepSource = new UriReportSource();
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("AuftraggeberID", AdrID));
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("Stichtag", DateTime.Now.AddDays(-1)));
                        uRepSource.Uri = Application.StartupPath + this.GLSystem.Doc_DocBestandAutoMail;
                        string FilePath = AttachmentPath + "\\" + tmpADR.ID.ToString() + "_" + FileBestandName;

                        clsPrint.PrintDirectToPDF(FileBestandName, FilePath, uRepSource);
                        //PrintDirectToPDF(FileBestandName, FilePath, uRepSource);
                        */

                        string strSql = "Select ViewID as KDNR, (Select sum(a1.Brutto) from Artikel a1 " +
                                          "left join LEingang b1 on a1.LEingangTableID=b1.ID   " +
                                          "left join (Select * from LAusgang) c1 on a1.LAusgangTableID=c1.ID  " +
                                          "left join ADR d1 on b1.Auftraggeber=d1.ID " +
                                          "where d1.ViewID=tmp.ViewID and a1.GArtID=1 and  b1.[Check]=1 and (Checked=0 or Checked is null)) as WG1, " +
                                          "(Select sum(a1.Brutto) from Artikel a1 " +
                                          "left join LEingang b1 on a1.LEingangTableID=b1.ID   " +
                                          "left join (Select * from LAusgang) c1 on a1.LAusgangTableID=c1.ID  " +
                                          "left join ADR d1 on b1.Auftraggeber=d1.ID " +
                                          "where d1.ViewID=tmp.ViewID and GArtID=2 and  b1.[Check]=1 and (Checked=0 or Checked is null)) as WG2, " +
                                          "(Select sum(a1.Brutto) from Artikel a1 " +
                                          "left join LEingang b1 on a1.LEingangTableID=b1.ID   " +
                                          "left join (Select * from LAusgang) c1 on a1.LAusgangTableID=c1.ID  " +
                                          "left join ADR d1 on b1.Auftraggeber=d1.ID " +
                                          "where d1.ViewID=tmp.ViewID and GArtID=3 and  b1.[Check]=1 and (Checked=0 or Checked is null)) as WG3, " +
                                          "(Select sum(a1.Brutto) from Artikel a1 " +
                                          "left join LEingang b1 on a1.LEingangTableID=b1.ID   " +
                                          "left join (Select * from LAusgang) c1 on a1.LAusgangTableID=c1.ID  " +
                                          "left join ADR d1 on b1.Auftraggeber=d1.ID " +
                                          "where d1.ViewID=tmp.ViewID and GArtID=4 and  b1.[Check]=1 and (Checked=0 or Checked is null)) as WG4, " +
                                          "(Select sum(a1.Brutto) from Artikel a1 " +
                                          "left join LEingang b1 on a1.LEingangTableID=b1.ID   " +
                                          "left join (Select * from LAusgang) c1 on a1.LAusgangTableID=c1.ID  " +
                                          "left join ADR d1 on b1.Auftraggeber=d1.ID " +
                                          "where d1.ViewID=tmp.ViewID and GArtID=5 and  b1.[Check]=1 and (Checked=0 or Checked is null)) as WG5, " +
                                          "(Select sum(a1.Brutto) from Artikel a1 " +
                                          "left join LEingang b1 on a1.LEingangTableID=b1.ID   " +
                                          "left join (Select * from LAusgang) c1 on a1.LAusgangTableID=c1.ID  " +
                                          "left join ADR d1 on b1.Auftraggeber=d1.ID " +
                                          "where d1.ViewID=tmp.ViewID and GArtID=6 and  b1.[Check]=1 and (Checked=0 or Checked is null)) as WG6, " +
                                          "Sum(Brutto) as Summe " +
                                          "from (select ViewID,b.[Check] as Ein,CASE when c.checked IS null then 0 else c.Checked end as Aus,Cast(a.Brutto as Decimal(18,0)) as Brutto from Artikel a " +
                                          "left join LEingang b on a.LEingangTableID=b.ID   " +
                                          "left join (Select * from LAusgang) c on a.LAusgangTableID=c.ID  " +
                                          "left join ADR d on b.Auftraggeber=d.ID) tmp " +
                                          "Group by ViewID,Ein,Aus having Ein=1 and Aus=0 order by ViewID ";



                        DataTable dtGewBestand = new DataTable("GewBestand");
                        dtGewBestand = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "GewBestand");
                        LVS.clsExcel Excel = new clsExcel();
                        string FileName = "BestandKW" + (Functions.GetCalendarWeek(DateTime.Now) - 1);
                        string FilePath = Excel.ExportDataTableToWorksheet(dtGewBestand, AttachmentPath + "\\" + FileName);
                        //string FilePath = AttachmentPath + "\\" + FileName;
                        List<string> listAttach = new List<string>();
                        listAttach.Add(FilePath);
                        if (listAttach.Count > 0)
                        {
                            Mail = new clsMail();
                            Mail.InitClass(this.GL_User, this.SYSTEM);
                            Mail.ListAttachment = listAttach;
                            bool bSendOK = true;
                            try
                            {
                                Mail.ListMailReceiver = Mailinglist.ListMailadressen;
                                Mail.Subject = "Bestand";
                                bSendOK = Mail.SendNoReply();
                            }
                            finally
                            {
                                if (bSendOK)
                                {
                                    clsLogbuchCon tmpLog = new clsLogbuchCon();
                                    tmpLog.ID = 0;
                                    tmpLog.Typ = enumLogArtItem.autoMail.ToString();
                                    tmpLog.LogText = " -> Adresse ID/Matchcode: [" + tmpADR.ID.ToString() + "/" + tmpADR.ViewID + "] -> Mail erfolgreich versendet...";
                                    ListLogInsert.Add(tmpLog);
                                }
                                else
                                {
                                    clsLogbuchCon tmpLog = new clsLogbuchCon();
                                    tmpLog.ID = 0;
                                    tmpLog.Typ = enumLogArtItem.autoMail.ToString();
                                    tmpLog.LogText = " -> Adresse ID/Matchcode: [" + tmpADR.ID.ToString() + "/" + tmpADR.ViewID + "] -> nicht Mail erfolgreich versendet...";
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
                            tmpLog.LogText = " -> Adresse ID/Matchcode: [" + tmpADR.ID.ToString() + "/" + tmpADR.ViewID + "] -> es konnte kein Anhang gefunden werden...";
                            ListLogInsert.Add(tmpLog);
                        }
                        Thread.Sleep(1000);
                    }
                }
            }
        }
        ///<summary>clsCronJobs / DoAutoJournal</summary>
        ///<remarks></remarks>
        public void DoAutoJournal()
        {
            ListLogInsert = new List<clsLogbuchCon>();
            //Ermitteln aller Adressen mit den hinterlegten Verteiler mit dem Name #autJournal#
            List<decimal> ListAdrID = clsMailingList.GetAutoMailingList(this.GL_User, const_autoBestand);
            if (ListAdrID.Count > 0)
            {
                Mailinglist = new clsMailingList();
                for (Int32 i = 0; i <= ListAdrID.Count - 1; i++)
                {
                    decimal AdrID = (decimal)ListAdrID[i];

                    clsADR tmpADR = new clsADR();
                    tmpADR._GL_User = this.GL_User;
                    tmpADR.ID = AdrID;
                    tmpADR.Fill();

                    Mailinglist.InitClass(this.GL_User, this.GLSystem, AdrID);
                    Mailinglist.FillListMailAdressenForAutoJournal(AdrID);
                    if (Mailinglist.ListMailadressen.Count > 0)
                    {
                        uRepSource = new UriReportSource();
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("AuftraggeberID", AdrID));
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("Datum", DateTime.Now.AddDays(-1)));
                        //uRepSource.Uri = Application.StartupPath + this.GLSystem.Doc_DocJournalAutoMail;
                        uRepSource.Uri = StartupPath + this.GLSystem.Doc_DocJournalAutoMail;
                        string FilePath = AttachmentPath + "\\" + tmpADR.ID.ToString() + "_" + FileJournalName;
                        clsPrint.PrintDirectToPDF(FileBestandName, FilePath, uRepSource);
                        //PrintDirectToPDF(FileBestandName, FilePath, uRepSource);
                        List<string> listAttach = new List<string>();
                        listAttach.Add(FilePath);

                        bool bSendOK = true;

                        if (listAttach.Count > 0)
                        {
                            Mail = new clsMail();
                            Mail.InitClass(this.GL_User, this.SYSTEM);
                            Mail.ListAttachment = listAttach;
                            try
                            {
                                Mail.ListMailReceiver = Mailinglist.ListMailadressen;
                                Mail.Subject = "Journal";
                                bSendOK = Mail.SendNoReply();
                            }
                            finally
                            {
                                if (bSendOK)
                                {
                                    clsLogbuchCon tmpLog = new clsLogbuchCon();
                                    tmpLog.ID = 0;
                                    tmpLog.Datum = DateTime.Now;
                                    tmpLog.Typ = enumLogArtItem.autoMail.ToString();
                                    tmpLog.LogText = " -> Adresse ID/Matchcode: [" + tmpADR.ID.ToString() + "/" + tmpADR.ViewID + "] -> Mail erfolgreich versendet...";
                                    ListLogInsert.Add(tmpLog);
                                }
                                else
                                {
                                    clsLogbuchCon tmpLog = new clsLogbuchCon();
                                    tmpLog.ID = 0;
                                    tmpLog.Datum = DateTime.Now;
                                    tmpLog.Typ = enumLogArtItem.autoMail.ToString();
                                    tmpLog.LogText = " -> Adresse ID/Matchcode: [" + tmpADR.ID.ToString() + "/" + tmpADR.ViewID + "] -> nicht Mail erfolgreich versendet...";
                                    ListLogInsert.Add(tmpLog);
                                }
                            }
                        }
                        else
                        {
                            //kein Attachment - keine Anhang
                            clsLogbuchCon tmpLog = new clsLogbuchCon();
                            tmpLog.ID = 0;
                            tmpLog.Datum = DateTime.Now;
                            tmpLog.Typ = enumLogArtItem.autoMail.ToString();
                            tmpLog.LogText = " -> Adresse ID/Matchcode: [" + tmpADR.ID.ToString() + "/" + tmpADR.ViewID + "] -> es konnte kein Anhang gefunden werden...";
                            ListLogInsert.Add(tmpLog);
                        }
                        Thread.Sleep(500);
                    }
                }
            }
        }
        ///<summary>clsCronJobs / DoAutoJournal</summary>
        ///3
        ///<remarks></remarks>
        public DataTable GetJournalDaten(decimal myAdrID, DateTime myVon, DateTime myBis)
        {
            string strSql = string.Empty;
            DataTable dt = new DataTable();

            strSql = "Select ";
            strSql = strSql + "distinct ";
            strSql = strSql +
                            "CAST(a.ID as INT) as ArtikelID " +
                            ", CAST(a.LVS_ID as INT) as LVSNr " +
                            ", a.Produktionsnummer" +
                            ", a.Dicke" +
                            ", a.Breite" +
                            ", a.Laenge" +
                            ", a.Netto" +
                            ", a.Brutto" +
                            ", b.Date as EDatum" +
                            ", a.exMaterialnummer as MaterialNr" +
                            ", d.ID as Schaden" +

                            " From Artikel a " +
                                "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                                "LEFT JOIN Gueterart e ON e.ID=a.GArtID " +
                                "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                                "LEFT JOIN SchadenZuweisung d ON d.ArtikelID=a.ID " +
                                " WHERE " +
                                       "b.Auftraggeber=" + myAdrID + " " +
                                       " AND " +
                                       "(" +
                                            "b.[Check]=1 AND b.DirectDelivery=0 " +
                                            "AND (b.Date between '" + myVon.Date.ToShortDateString() + "' AND '" + myBis.Date.AddDays(1).ToShortDateString() + "') " +
                                        ") OR (" +
                                            "c.Checked=1 AND c.DirectDelivery=0 " +
                                            "AND (c.Datum between '" + myVon.Date.ToShortDateString() + "' AND '" + myBis.Date.AddDays(1).ToShortDateString() + "') " +
                                        ")";

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Journal");
            return dt;
        }
        ///<summary>clsCronJobs / DoAutoCalculation</summary>
        ///<remarks></remarks>
        public void DoAutoCalculation()
        {
            ListLogInsert = new List<clsLogbuchCon>();
            LVS.Globals._GL_SYSTEM _GLSystem = new LVS.Globals._GL_SYSTEM();

            List<Int32> lstMandanten = new List<Int32>();
            string strSql = "Select ID from Mandanten where aktiv=1 and Default_Lager=1";
            DataTable dtMandanten = clsSQLcon.ExecuteSQL_GetDataTable(strSql, this.BenutzerID, "Mandanten");
            foreach (DataRow row in dtMandanten.Rows)
            {
                Int32 iTmp = 0;
                Int32.TryParse(row["ID"].ToString(), out iTmp);
                if (iTmp > 0)
                {
                    lstMandanten.Add(iTmp);
                }
            }
            //Durchlaufen aller aktiven Mandanten
            foreach (Int32 mandant in lstMandanten)
            {
                _GLSystem.sys_MandantenID = mandant;
                _GLSystem.sys_ArbeitsbereichID = 1;  // aktuell hardcodiert CalcAll hat keinen Bezug auf den Arbeitsbereich sondern nur auf den Mandanten
                LVS.clsFaktLager FaktLager = new LVS.clsFaktLager();
                FaktLager.bUseBKZ = this.SYSTEM.Client.Modul.Lager_USEBKZ;
                FaktLager.GL_System = _GLSystem;
                FaktLager.MandantenID = _GLSystem.sys_MandantenID;
                DateTime now = DateTime.Now.AddMonths(-1);
                DateTime CalcMonth = new DateTime(now.Year, now.Month, 1);
                FaktLager.CalcAll(CalcMonth, new DateTime(now.Year, now.Month, DateTime.DaysInMonth(CalcMonth.Year, CalcMonth.Month)), DateTime.Now, false);
            }
        }


        //***********************************************************************************************************************
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myGLUser"></param>
        /// <returns></returns>
        public static DataTable GetCronJobList(Globals._GL_USER myGLUser)
        {
            DataTable dt = new DataTable("CronJobs");
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM CronJobs ;";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "CronJobs");
            return dt;
        }
    }
}
