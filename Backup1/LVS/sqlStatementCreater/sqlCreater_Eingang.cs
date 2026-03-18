using Common.Models;
using Convert = System.Convert;

namespace LVS.sqlStatementCreater
{
    public class sqlCreater_Eingang
    {
        internal Eingaenge eingang { get; set; }
        public sqlCreater_Eingang(Eingaenge myEingang)
        {
            this.eingang = myEingang;
        }

        /// <summary>
        /// 
        /// </summary>
        public string AddBatch
        {
            get
            {
                string sql = string.Empty;
                sql += "INSERT INTO LEingang(";
                sql += "LEingangID, Date, Auftraggeber, Empfaenger, Lieferant, AbBereich, Mandant, LfsNr, ASN, ";
                sql += "Versender, SpedID, KFZ, DirectDelivery, Retoure, Vorfracht, LagerTransport, WaggonNo, ";
                sql += "BeladeID, EntladeID, ExTransportRef, ExAuftragRef, ASNRef, IsWaggon, Fahrer, Ship, ";
                sql += "IsShip, Verlagerung, Umbuchung, CreatedByScanner ";
                sql += ") VALUES (";

                sql += $"{eingang.LEingangID}, ";
                sql += $"'{eingang.Eingangsdatum.ToString("yyyy-MM-dd HH:mm:ss")}', ";
                sql += $"{eingang.Auftraggeber}, ";
                sql += $"{eingang.Empfaenger}, ";
                sql += $"'{eingang.Lieferant}', ";
                sql += $"{eingang.ArbeitsbereichId}, ";
                sql += $"{eingang.MandantenId}, ";
                sql += $"'{eingang.LfsNr}', ";
                sql += $"{eingang.ASN}, ";
                sql += $"{eingang.Versender}, ";
                sql += $"{eingang.SpedId}, ";
                sql += $"'{eingang.KFZ}', ";
                sql += $"{Convert.ToInt32(eingang.DirektDelivery)}, ";
                sql += $"{Convert.ToInt32(eingang.Retoure)}, ";
                sql += $"{Convert.ToInt32(eingang.Vorfracht)}, ";
                sql += $"{Convert.ToInt32(eingang.LagerTransport)}, ";
                sql += $"'{eingang.WaggonNr}', ";
                sql += $"{eingang.BeladeID}, ";
                sql += $"{eingang.EntladeID}, ";
                sql += $"'{eingang.ExTransportRef}', ";
                sql += $"'{eingang.ExAuftragRef}', ";
                sql += $"'{eingang.ASNRef}', ";
                sql += $"{Convert.ToInt32(eingang.IsWaggon)}, ";
                sql += $"'{eingang.Fahrer}', ";
                sql += $"'{eingang.Ship}', ";
                sql += $"{Convert.ToInt32(eingang.IsShip)}, ";
                sql += $"{Convert.ToInt32(eingang.Verlagerung)}, ";
                sql += $"{Convert.ToInt32(eingang.Umbuchung)}, ";
                sql += $"{Convert.ToInt32(eingang.CreatedByScanner)} ";

                sql += ");";

                return sql;
            }
        }

        public string DeleteById
        {
            get
            {
                string sql = string.Empty;
                sql += "DELETE FROM LEingang WHERE ID=" + eingang.Id;
                return sql;
            }
        }

    }
}
