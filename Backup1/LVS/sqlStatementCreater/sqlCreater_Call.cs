using System;
using System.Collections.Generic;

namespace LVS.sqlStatementCreater
{
    public class sqlCreater_Call
    {

        public sqlCreater_Call()
        {

        }
        public string sql_Main
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Select CAST(0 as bit) as 'Select' " +
                                ", ab.EintreffDatum as Eintreffdatum " +
                                ", ab.EintreffZeit as Eintreffzeit" +
                                ", ab.ID as AbrufID" +
                                ", ab.Status" +
                                ", ab.Benutzername as Bearbeiter" +
                                ", ab.Schicht" +
                                ", ab.Referenz" +
                                ", ab.Abladestelle" +
                                ", ab.Aktion" +
                                ", ab.erstellt" +
                                //", ab.Status"+ 
                                ", a.ID as ArtikelID" +
                                ", a.LVS_ID as LVSNr" +
                                ", a.Werksnummer" +
                                ", a.Produktionsnummer" +
                                ", a.Charge" +
                                ", a.Brutto" +
                                ", a.Dicke" +
                                ", a.Breite" +
                                ", a.Anzahl as Menge" +
                                ", (a.Reihe+'/'+a.Platz) as 'Reihe/Platz'" +
                                ", (Select Name1 FROM ADR WHERE ID=e.Auftraggeber) as Lieferant" +
                                ", (Select Name1 FROM ADR WHERE ID=ab.LiefAdrID) as Entladestelle" +
                                ", (Select Name1 FROM ADR WHERE ID=ab.EmpAdrID) as Empfänger" +
                                ", (Select Name1 FROM ADR WHERE ID=ab.SpedAdrID) as Spediteur" +
                                ", e.Auftraggeber" +
                                ",CASE " +
                                      "WHEN (SELECT COUNT (*) " +
                                        " FROM Artikel a1 " +
                                        " INNER JOIN LEingang c1 ON c1.ID=a1.LEingangTableID " +
                                        " INNER JOIN SchadenZuweisung d1 ON d1.ArtikelID=a1.ID " +
                                        " INNER JOIN Schaeden e1 ON e1.ID=d1.SchadenID " +
                                        " WHERE a1.ID=a.ID) > 0 " +
                                      " THEN (SELECT e2.Bezeichnung + char(10) " +
                                          " FROM Artikel a2 " +
                                          " INNER JOIN LEingang c2 ON c2.ID=a2.LEingangTableID " +
                                          " LEFT OUTER JOIN SchadenZuweisung d2 ON d2.ArtikelID=a2.ID " +
                                          " LEFT OUTER JOIN Schaeden e2 ON e2.ID=d2.SchadenID " +
                                          " WHERE a2.ID=a.ID " +
                                          " FOR XML PATH ('')) " +
                                      " ELSE '' " +
                                      " END as Schaden " +

                                    " FROM Abrufe ab " +
                                    "INNER JOIN Artikel a on a.ID=ab.ArtikelID " +
                                    "INNER JOIN LEingang e on e.ID=a.LEingangTableID ";
                return strSql;
            }
        }
        public string sqlString_Call_GetOpenCall(string myAction, int myWorkspaceId, int myCallId, bool myChecked)
        {
            string strSql = sql_Main;

            strSql += "WHERE " +
                        "ab.IsRead=0 " +
                        " AND ab.IsCreated=1 ";

            if (
                (myAction.Equals(clsASNCall.const_AbrufAktion_UB)) ||
                (myAction.Equals(clsASNCall.const_AbrufAktion_Abruf))
               )
            {
                strSql += " AND ab.Aktion='" + myAction + "'";
            }

            if (myWorkspaceId > 0)
            {
                strSql += " AND ab.AbBereich=" + myWorkspaceId;
            }
            if (myCallId > 0)
            {
                strSql += " AND ab.ID= " + myCallId;
            }
            if (myChecked)
            {
                strSql += " AND ab.ScanCheckForStoreOut > '" + new DateTime(1900, 1, 1).ToString("dd.MM.yyyy") + "'";
            }
            strSql += " ORDER BY ab.EintreffDatum, ab.EintreffZeit ";
            return strSql;
        }

        public string sqlString_Call_GetOpenCallById(List<int> myIdList)
        {
            string strSql = string.Empty;
            if (myIdList.Count > 0)
            {
                strSql = sql_Main;
                strSql += "WHERE ab.ID IN (" + string.Join(", ", myIdList.ToArray()) + "); ";
            }
            return strSql;
        }
    }
}
