using Common.Models;
using LVS.Models;
using System.Collections.Generic;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class StockViewData
    {
        /// <summary>
        ///             Stock = Lagerbestand
        ///             ViewData Klasse für Bestände usw.
        /// </summary>


        public StockViewData()
        {

        }
        /// <summary>
        ///             ctrDelforDeliveryForecast
        ///             Datenbestand DELFOR DGV
        /// </summary>
        /// <param name="myDelforVerweise"></param>
        /// <returns></returns>
        public static DataTable GetList_AvailableStockForDeliveryForecast(string myDelforVerweise)
        {
            DataTable dt = new DataTable();
            if (myDelforVerweise.Length > 0)
            {
                string strSQL = "Select " +
                        "a.ID " +
                        ", a.LVS_ID as LvsNr " +
                        ", ein.Date as EDatum " +
                        ", a.Anzahl " +
                        ", a.GlowDate as Glühdatum " +
                        ", a.Produktionsnummer " +
                        ", a.Werksnummer " +
                        ", a.Brutto " +
                        ", g.Bezeichnung " +
                        ", g.DelforVerweis " +

                        "From Artikel a " +
                            "INNER JOIN LEingang ein ON ein.ID = a.LEingangTableID " +
                            "LEFT JOIN Gueterart g ON g.ID = a.GArtID " +
                            "LEFT JOIN LAusgang aus ON aus.ID = a.LAusgangTableID " +
                        "where " +
                            "g.DelforVerweis = '" + myDelforVerweise + "' " +
                            "AND a.LAusgangTableID = 0 " +
                            "AND a.ID NOT IN (SELECT ArtikelId FROM Abrufe) " +
                            "Order by ein.Date, a.ID ";


                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, 1, "EdiDelforStock");
            }
            return dt;
        }

        public static DataTable GetList_DeliveredArticleDelfor(EdiDelforD97AValues myEdiDelforValue)
        {
            Dictionary<Ausgaenge, List<Articles>> retDict = new Dictionary<Ausgaenge, List<Articles>>();

            string strSql = string.Empty;

            strSql += "Select a.ID ";
            strSql += ", a.LVS_ID as LVSNr";
            strSql += ", ein.Date as EDatum";
            strSql += ", a.Anzahl";
            strSql += ", a.Produktionsnummer";
            strSql += ", a.Werksnummer";
            strSql += ", a.Brutto";
            strSql += ", a.GlowDate as Glühdatum";
            strSql += ", g.Bezeichnung";
            strSql += ", g.DelforVerweis";
            strSql += ", ab.Id as AbrufId";
            strSql += ", ab.EdiDelforD97AValueId as DelforId";

            strSql += " From Artikel a ";
            strSql += "INNER JOIN LEingang ein ON ein.ID = a.LEingangTableID ";
            strSql += "LEFT JOIN LAusgang aus ON aus.ID = a.LAusgangTableID ";
            strSql += "LEFT JOIN Gueterart g ON g.ID = a.GArtID ";
            strSql += "LEFT JOIN Abrufe ab on ab.ArtikelID = a.ID ";

            strSql += " WHERE ";
            strSql += "ein.Auftraggeber = " + myEdiDelforValue.Client;
            strSql += " and ";
            strSql += "ein.AbBereich = " + myEdiDelforValue.WorkspaceId;
            strSql += " and ";
            strSql += "ab.EdiDelforD97AValueId = " + myEdiDelforValue.Id;
            strSql += " and ";
            strSql += "a.LAusgangTableID = 0 ";
            strSql += "Order by ein.Date, a.ID ";

            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "LAusgang", "Ausgang", 1);
            return dt;
        }


    }
}

