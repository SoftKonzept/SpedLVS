using Common.Models;
using CustomizedUpdates.MainSystem;
using LVS;
using LVS.ViewData;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;


namespace CustomizedUpdates.ArcelorUmstellung2024
{
    public class UpdateViewData
    {
        public const string const_LieferantennummerNEU = "17548910";
        public const string const_LieferantennummerALT = "16301610";
        public const int const_WorkspaceIdBMW = 5;
        internal SystemMain systemMain = new SystemMain();
        internal List<UpdateData> ListDataToUpdate = new List<UpdateData>();
        public List<string> ListInfos { get; set; }
        public UpdateData data { get; set; }

        public UpdateViewData()
        {
            data = new UpdateData();
        }

        /// <summary>
        ///             UPDATE
        /// </summary>
        /// <returns></returns>
        //public bool Update()
        //{
        //    //string strSql = sql_Update;
        //    //bool retVal = clsSQLCOM.ExecuteSQL(strSql, BenutzerID);
        //    //return retVal;
        //}

        private string sql_EingaengeByLieferantennummer(string myLieferant)
        {
            string strSql = "SELECT DISTINCT ";
            strSql += "e.Id, e.Date , e.Auftraggeber ";
            strSql += ", (SELECT Name1 FROM ADR WHERE ID = e.Auftraggeber) as AuftraggeberName ";
            strSql += ", e.Lieferant ";
            strSql += "FROM LEingang e ";
            strSql += "INNER JOIN Artikel a on e.ID = a.LEingangTableID ";
            strSql += " where e.Lieferant = '" + myLieferant + "' and a.LAusgangTableID = 0 and e.Auftraggeber<>195 ";

            return strSql;
        }

        public void EingangToUpdate()
        {
            ListInfos = new List<string>();
            string strSql = sql_EingaengeByLieferantennummer(const_LieferantennummerALT);
            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "Eingang", "Update", 1);

            List<int> listEingang = new List<int>();
            int iRows = dt.Rows.Count;
            string Mes = "Update Lieferantennummer in den Eingängen!"; // string.Empty;
            ListInfos.Add(Mes);
            if (iRows > 0)
            {
                Mes = "zu ändernde Daten: " + iRows + Environment.NewLine;
                ListInfos.Add(Mes);
                foreach (DataRow row in dt.Rows)
                {
                    int iTmp = 0;
                    int.TryParse(row["Id"].ToString(), out iTmp);
                    if (!listEingang.Contains(iTmp))
                    {
                        listEingang.Add(iTmp);
                    }
                }

                //--- Eingänge laden und updaten
                EingangViewData eVD = new EingangViewData();
                foreach (int x in listEingang)
                {
                    eVD = new EingangViewData(x, 1, false);
                    string strLiefNrALT = eVD.Eingang.Lieferant;
                    eVD.Eingang.Lieferant = const_LieferantennummerNEU;
                    if (eVD.Update_Datafield_Lieferant(const_LieferantennummerNEU))
                    {
                        Mes = "Eingang " + x + " | Lieferant = " + strLiefNrALT + " > " + eVD.Eingang.Lieferant + " geändert ";
                        ListInfos.Add(Mes);
                    }
                    else
                    {
                        Mes = "!!! Eingang " + x + " | Lieferant = " + strLiefNrALT + " konnte nicht geändert werden ";
                        ListInfos.Add(Mes);
                    }
                }

                Mes = " - " + Environment.NewLine;
                ListInfos.Add(Mes);
                Mes = " - " + Environment.NewLine;
                ListInfos.Add(Mes);

                //--- CHeck mit alter Lieferantennummer
                DataTable dtCheck = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "Eingang", "Update", 1);
                Mes = "Anzahl nach Update: " + dtCheck.Rows.Count;
                ListInfos.Add(Mes);
                if (dtCheck.Rows.Count > 0)
                {
                    Mes = " -> Folgende Daten wurden nicht upgedatet ";
                    ListInfos.Add(Mes);
                    foreach (DataRow row in dt.Rows)
                    {
                        int iTmp = 0;
                        int.TryParse(row["Id"].ToString(), out iTmp);

                        Mes = " > Eingang Id: " + iTmp;
                        ListInfos.Add(Mes);
                    }
                }

                //-- Check mit neuer Lieferantennummer
                string strSqlCheckNeu = sql_EingaengeByLieferantennummer(const_LieferantennummerNEU);
                DataTable dtCheckNeu = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSqlCheckNeu, "Eingang", "Update", 1);
                Mes = "Anzahl Eingänge (neue Lieferantennummer): " + dtCheckNeu.Rows.Count;
                ListInfos.Add(Mes);
            }
            else
            {
                Mes = "zu ändernde Daten: " + iRows;
                ListInfos.Add(Mes);
                Mes = "Es liegen keine Daten für das Update vor!";
                ListInfos.Add(Mes);

            }
            string FileName = DateTime.Now.ToString("yyyy_MM_dd_HH_mms") + "_LogEingangChange.txt";
            string FilePath = Path.Combine(systemMain.StartupPath, FileName);
            helper_IOFile.WriteFileInLine(FilePath, ListInfos);
        }


        private void GetImportSourceDataToChange()
        {
            ListDataToUpdate = new List<UpdateData>();
            string strSql = "Select * from ImportBMW";
            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "Import", "Update", 1);
            foreach (DataRow dr in dt.Rows)
            {
                SetValue(dr);
                ListDataToUpdate.Add(data);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        public void SetValue(DataRow row)
        {
            data = new UpdateData();

            int iTmp = 0;
            int.TryParse(row["ID"].ToString(), out iTmp);
            data.Id = iTmp;

            data.BestellNrNeu = row["BestellNrNeu"].ToString();
            data.Werksnummer = row["Werksnummer"].ToString();
            data.BestellNrALT = row["BestellNrALT"].ToString();
            data.WerksnummerALT = row["WerksnummerALT"].ToString();
        }

        public void UpdateGueterartenBestellnummer()
        {
            ListInfos = new List<string>();
            string Mes = string.Empty;
            GetImportSourceDataToChange();
            if (ListDataToUpdate.Count > 0)
            {
                Mes = "Anzahl Änderungsdatensätze: " + ListDataToUpdate.Count;
                ListInfos.Add(Mes);
                GoodstypeViewData gVD = new GoodstypeViewData();
                gVD.GetGoodtypeListByWorkspace(const_WorkspaceIdBMW);

                int iCountLoop = 0;
                foreach (UpdateData ud in ListDataToUpdate)
                {
                    iCountLoop++;
                    Goodstypes gt = gVD.ListGueterarten.FirstOrDefault(x => x.Werksnummer == ud.Werksnummer && x.BestellNr == ud.BestellNrALT);
                    if ((gt != null) && (gt.Id > 0))
                    {
                        if (gt.Besonderheit.Length > 0)
                        {
                            gt.Besonderheit += Environment.NewLine + "Neue Lieferantennummer: " + const_LieferantennummerNEU;
                        }
                        else
                        {
                            gt.Besonderheit += Environment.NewLine + "Bestellnummer: " + gt.BestellNr + " > " + ud.BestellNrNeu;
                        }
                        string strBestellNrALT = gt.BestellNr;
                        gt.BestellNr = ud.BestellNrNeu;

                        gVD.Gut = gt.Copy();
                        if (gVD.Update_BestellNrChange())
                        {
                            Mes = iCountLoop.ToString("00") + ".  + Id [" + gt.Id + "] ViewId:" + gt.ViewID + " - Bestellnummer: " + strBestellNrALT + " > " + ud.BestellNrNeu + " | WerksNr:" + gt.Werksnummer;
                            ListInfos.Add(Mes);
                        }
                        else
                        {
                            Mes = iCountLoop.ToString("00") + ".  - Id [" + gt.Id + "] ViewId:" + gt.ViewID + " - Bestellnummer: " + strBestellNrALT + " | WerksNr:" + gt.Werksnummer;
                            ListInfos.Add(Mes);
                        }
                    }
                    else
                    {
                        Mes = iCountLoop.ToString("00") + ".  - BestellNr [alt]: " + ud.BestellNrALT + " | [neu]: " + ud.BestellNrNeu + "| WerksNr:" + ud.Werksnummer;
                        ListInfos.Add(Mes);
                    }
                }
                string FileName = DateTime.Now.ToString("yyyy_MM_dd_HH_mms") + "_LogGüterartChange.txt";
                string FilePath = Path.Combine(systemMain.StartupPath, FileName);
                helper_IOFile.WriteFileInLine(FilePath, ListInfos);
            }
        }

        private string sql_ArticleByChangeData()
        {
            string strSql = "SELECT a.Id, a.LVS_ID ";
            strSql += ", a.Bestellnummer ";
            //strSql += "e.Id, e.Date , e.Auftraggeber ";
            //strSql += ", (SELECT Name1 FROM ADR WHERE ID = e.Auftraggeber) as AuftraggeberName ";
            //strSql += ", e.Lieferant ";
            strSql += "FROM [Artikel] a ";
            strSql += "INNER JOIN [LEingang] e on e.ID = a.LEingangTableID ";
            strSql += " where ";
            //strSql += " e.Lieferant = '" + myLieferant + "' ";
            strSql += " a.Bestellnummer in (SELECT BestellNrALT FROM ImportBMW where BestellNrALT<>'') ";
            strSql += " and a.LAusgangTableID = 0 ";
            strSql += " and a.AB_ID=5 ";
            strSql += " and a.AB_ID=5 ";

            return strSql;
        }

        public void UpdateArticleBestellnummer()
        {
            ListInfos = new List<string>();
            string Mes = string.Empty;
            GetImportSourceDataToChange();

            string strSql = sql_ArticleByChangeData();
            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "Article", "Update", 1);

            if ((dt != null) && (dt.Rows.Count > 0) && (ListDataToUpdate.Count > 0))
            {
                Mes = "Anzahl zu ändernde Artikel: " + dt.Rows.Count;
                ListInfos.Add(Mes);

                int iCountLoop = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    iCountLoop++;
                    int iArtId = 0;
                    int.TryParse(dr["ID"].ToString(), out iArtId);
                    if (iArtId > 0)
                    {
                        ArticleViewData aVD = new ArticleViewData(iArtId, 1, false);

                        UpdateData ud = ListDataToUpdate.FirstOrDefault(x => x.BestellNrALT == aVD.Artikel.Bestellnummer && x.Werksnummer == aVD.Artikel.Werksnummer);
                        if ((ud != null) && (ud.Id > 0))
                        {
                            if (aVD.Artikel.interneInfo.Length > 0)
                            {
                                aVD.Artikel.interneInfo += Environment.NewLine + "Neue Lieferantennummer: " + const_LieferantennummerNEU;
                            }
                            else
                            {
                                aVD.Artikel.interneInfo += "Neue Lieferantennummer: " + const_LieferantennummerNEU;
                            }
                            aVD.Artikel.interneInfo += Environment.NewLine + "Bestellnummer: " + aVD.Artikel.Bestellnummer + " > " + ud.BestellNrNeu;
                            string strArtBestellNrALT = aVD.Artikel.Bestellnummer;
                            aVD.Artikel.Bestellnummer = ud.BestellNrNeu;

                            if (aVD.Update_BestellNrUpdateByBMW())
                            {
                                Mes = iCountLoop.ToString("00") + ". + Id/LVSNR [" + aVD.Artikel.Id + "|" + aVD.Artikel.LVS_ID + "] - Bestellnummer: " + strArtBestellNrALT + " > " + aVD.Artikel.Bestellnummer + " | WerksNr:" + aVD.Artikel.Werksnummer;
                                ListInfos.Add(Mes);
                            }
                            else
                            {
                                Mes = iCountLoop.ToString("00") + ". - Id/LVSNR [" + aVD.Artikel.Id + "|" + aVD.Artikel.LVS_ID + "] - Bestellnummer: " + strArtBestellNrALT + " | WerksNr:" + aVD.Artikel.Werksnummer;
                                ListInfos.Add(Mes);
                            }
                        }

                    }
                }

                //--CHeck Artikel
                strSql = sql_ArticleByChangeData();
                DataTable dtCheck = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "Article", "UpdateCheck", 1);

                Mes = "Anzahl Artikel nach Update: " + dtCheck.Rows.Count;
                ListInfos.Add(Mes);
                if (dtCheck.Rows.Count > 0)
                {
                    Mes = " -> Folgende Daten wurden nicht upgedatet ";
                    ListInfos.Add(Mes);
                    foreach (DataRow row in dt.Rows)
                    {
                        int iTmp = 0;
                        int.TryParse(row["Id"].ToString(), out iTmp);
                        Mes = " > Id: " + iTmp;

                        iTmp = 0;
                        int.TryParse(row["LVS_ID"].ToString(), out iTmp);
                        Mes += " | LvsNr: " + iTmp;
                        ListInfos.Add(Mes);
                    }
                }

                //-- Check mit neuer Lieferantennummer
                string strSqlCheckNeu = sql_ArticleByChangeData();
                DataTable dtCheckNeu = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSqlCheckNeu, "Article", "UpdateCheck2", 1);
                Mes = "Anzahl geänderte Artikel: " + dtCheckNeu.Rows.Count;
                ListInfos.Add(Mes);

            }
            else
            {
                Mes = "zu ändernde Daten: 0";
                ListInfos.Add(Mes);
                Mes = "Es liegen keine Daten für das Update vor!";
                ListInfos.Add(Mes);
            }
            string FileName = DateTime.Now.ToString("yyyy_MM_dd_HH_mms") + "_LogArtikelChange.txt";
            string FilePath = Path.Combine(systemMain.StartupPath, FileName);
            helper_IOFile.WriteFileInLine(FilePath, ListInfos);

        }
    }
}
