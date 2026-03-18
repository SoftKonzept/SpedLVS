using LVS.Models;
using System;

namespace LVS.sqlStatementCreater
{
    public class sqlCreater_Invoice
    {
        public sqlCreater_Invoice()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myInvoice"></param>
        /// <returns></returns>
        public static string sql_GetInvoice(int myId)
        {
            string strSql = string.Empty;
            strSql = "Select a.* " +
                //", a.RGNr" +
                //", a.Datum" +
                //", b.RGTableID" +
                //", b.Position" +
                //", b.RGText as Text" +
                //", b.Abrechnungseinheit" +
                //", b.Einzelpreis" +
                //", b.Menge" +
                //", b.NettoPreis" +
                //", b.CalcModus" +
                //", b.CalcModValue" +
                //", b.Abrechnungsart" +
                //", b.TarifPosID" +
                //", b.Tariftext" +
                //", b.MargeEuro" +
                //", b.MargeProzent" +
                //", b.Anfangsbestand" +
                //", b.Abgang" +
                //", b.Zugang" +
                //", b.Endbestand" +
                //", b.RGPosText" +
                //", b.FibuKto" +
                //", a.InfoText" +
                //", a.FibuInfo" +
                " FROM Rechnungen a " +
                        //"INNER JOIN RGPositionen b ON b.RGTableID = a.ID " +
                        "WHERE a.ID=" + myId + "; ";
            return strSql;
        }
        public static string sql_GetInvoiceByInvoiceNo(int myInvoiceNo)
        {
            string strSql = string.Empty;
            strSql = "Select a.* " +
                //", a.RGNr" +
                //", a.Datum" +
                //", b.RGTableID" +
                //", b.Position" +
                //", b.RGText as Text" +
                //", b.Abrechnungseinheit" +
                //", b.Einzelpreis" +
                //", b.Menge" +
                //", b.NettoPreis" +
                //", b.CalcModus" +
                //", b.CalcModValue" +
                //", b.Abrechnungsart" +
                //", b.TarifPosID" +
                //", b.Tariftext" +
                //", b.MargeEuro" +
                //", b.MargeProzent" +
                //", b.Anfangsbestand" +
                //", b.Abgang" +
                //", b.Zugang" +
                //", b.Endbestand" +
                //", b.RGPosText" +
                //", b.FibuKto" +
                //", a.InfoText" +
                //", a.FibuInfo" +
                " FROM Rechnungen a " +
                        "INNER JOIN RGPositionen b ON b.RGTableID = a.ID " +
                        "WHERE a.RGNr =" + myInvoiceNo + "; ";
            return strSql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myInvoice"></param>
        /// <returns></returns>
        public static string sql_Exist(Invoices myInvoice)
        {
            string strSql = string.Empty;
            strSql = "Select a.* FROM Rechnungen a WHERE a.ID=" + myInvoice.Id + "; ";
            return strSql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myInvoice"></param>
        /// <returns></returns>
        public static string sql_GetNotPrintedInvoices(int myWorkspaceId)
        {
            string strSql = string.Empty;
            strSql = "Select DISTINCT a.* " +
                " FROM Rechnungen a " +
                        "INNER JOIN RGPositionen b ON b.RGTableID = a.ID " +
                        "WHERE " +
                            "a.Druck=0 " +
                            "and a.RGArt='" + clsRechnung.const_RechnungsArt_Manuell + "' " +
                            "and a.FibuInfo <>'" + clsRechnung.const_RGText_Dummy + "' " +
                            "and a.ArBereichID=" + myWorkspaceId;
            return strSql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bGetAllWorkspaces"></param>
        /// <param name="myBillingPeriodeStart"></param>
        /// <param name="myBillingPeriodeEnd"></param>
        /// <param name="myMandantId"></param>
        /// <param name="myWorkspaceId"></param>
        /// <param name="myReceiver"></param>
        /// <returns></returns>
        public static string sql_GetRechnungListForPeriode(bool bGetAllWorkspaces
                                                    , DateTime myBillingPeriodeStart
                                                    , DateTime myBillingPeriodeEnd
                                                    , int myMandantId
                                                    , int myWorkspaceId
                                                    , int myReceiver
                                                    )
        {
            string strSql = string.Empty;
            strSql = "Select DISTINCT " +
                            "a.ID " +
                            ",a.RGNr as Beleg " +
                            ",CASE " +
                                    "WHEN (a.GS=0) AND (a.Storno=0) THEN 'RG' " +
                                    "WHEN (a.GS=1) AND (a.Storno=0) THEN 'GS' " +
                                    "WHEN (a.GS=0) AND (a.Storno=1) THEN 'RG-ST' " +
                                    "WHEN (a.GS=1) AND (a.Storno=1) THEN 'GS-ST' " +
                                    "END as Art " +
                            ", d.ViewID as Kunde " +
                            ", a.Datum" +
                            ", a.NettoBetrag as [Netto €]" +
                            ", a.MwStBetrag as [MWSt €]" +
                            ", a.BruttoBetrag as [Brutto €]" +
                            ", (Select SUM(NettoPreis) FROM RGPositionen WHERE Abrechnungsart='Einlagerungskosten' AND RGTableID=a.ID) as Einlagerung " +
                            ", (Select SUM(NettoPreis) FROM RGPositionen WHERE Abrechnungsart='Auslagerungskosten' AND RGTableID=a.ID) as Auslagerung " +
                            ", (Select SUM(NettoPreis) FROM RGPositionen WHERE Abrechnungsart='Lagerkosten' AND RGTableID=a.ID) as Lagerkosten " +
                            ", (Select SUM(NettoPreis) FROM RGPositionen WHERE Abrechnungsart='Nebenkosten' AND RGTableID=a.ID) as Nebenkosten " +
                            ", a.RGBookPrintDate " +
                            ", a.Druck" +
                            ", a.FibuInfo" +
                            ", a.RGArt as RGArt" +
                            ", e.Name as Arbeitsbereich " +
                            " FROM Rechnungen a " +
                                    "LEFT JOIN RGPositionen b ON b.RGTableID = a.ID " +
                                    "LEFT JOIN RGPosArtikel c ON c.RGPosID=b.ID " +
                                    "LEFT JOIN ADR d ON d.ID=a.Empfaenger " +
                                    "LEFT JOIN Arbeitsbereich e ON e.ID=a.ArBereichID " +
                                    "WHERE " +
                                            "a.Datum between '" + myBillingPeriodeStart.ToString() + "' AND '" + myBillingPeriodeEnd.AddDays(1).ToString() + "' ";

            if (!bGetAllWorkspaces)
            {
                strSql = strSql + "AND a.MandantenID=" + myMandantId +
                                  " AND a.ArBereichID=" + myWorkspaceId + " ";
            }
            if (myReceiver > 0)
            {
                strSql = strSql + "AND a.Empfaenger=" + myReceiver + " ";
            }
            strSql += "Order by Datum,a.RGNr ";
            return strSql;
        }



    }
}
