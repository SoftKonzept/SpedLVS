using System;
using System.Data;

namespace Sped4.Settings
{
    public class ctrWaggonbuchSettings
    {
        public static DataTable InitTableWaggonBuchAuswahl()
        {
            /********************************************
             * -bitte wählen Sie-
             * Tagesanfangbestand 
             * Tagesendbestand
             * Bestand nach Zeitraum
             * Empfänger[geprüft]
             * Sperrlager
             * Rücklieferungen
             * Direktanlieferungen
             * Ungeprüfte Artikel in Eingang
             * Ungeprüfte Artikel in Ausgang
             * Nicht abgeschlossene Eingänge
             * Nicht abgeschlossene Ausgänge
             * Alle Kunden kumuliert
             * Alle Kunde detailliert
             * Abzurechnende Kunden
             * Inventurliste Scanner
             * *****************************************/

            DataTable dt = new DataTable("Waggonlisten");
            dt.Columns.Add("ID", typeof(Int32));
            dt.Columns.Add("Waggonansicht", typeof(String));
            dt.Columns.Add("ADRRequire", typeof(bool));
            dt.Columns.Add("DateRequire", typeof(bool));
            Int32 i = -1;

            i++;
            DataRow row = dt.NewRow();
            row["ID"] = i;
            row["Waggonansicht"] = "-bitte wählen Sie-";
            row["ADRRequire"] = false;
            row["DateRequire"] = true;
            dt.Rows.Add(row);

            i++;
            row = dt.NewRow();
            row["ID"] = i;
            row["Waggonansicht"] = "Alle";
            row["ADRRequire"] = true;
            row["DateRequire"] = true;
            dt.Rows.Add(row);

            i++;
            row = dt.NewRow();
            row["ID"] = i;
            row["Waggonansicht"] = "Waggons Eingänge";
            row["ADRRequire"] = true;
            row["DateRequire"] = true;
            dt.Rows.Add(row);

            i++;
            row = dt.NewRow();
            row["ID"] = i;
            row["Waggonansicht"] = "Waggons Ausgänge";
            row["ADRRequire"] = true;
            row["DateRequire"] = true;
            dt.Rows.Add(row);

            i++;
            row = dt.NewRow();
            row["ID"] = i;
            row["Waggonansicht"] = "Private Waggons";
            row["ADRRequire"] = true;
            row["DateRequire"] = true;
            dt.Rows.Add(row);
            i++;
            row = dt.NewRow();
            row["ID"] = i;
            row["Waggonansicht"] = "Private Waggons Eingänge";
            row["ADRRequire"] = true;
            row["DateRequire"] = true;
            dt.Rows.Add(row);

            i++;
            row = dt.NewRow();
            row["ID"] = i;
            row["Waggonansicht"] = "Private Waggons Ausgänge";
            row["ADRRequire"] = true;
            row["DateRequire"] = true;
            dt.Rows.Add(row);

            i++;
            row = dt.NewRow();
            row["ID"] = i;
            row["Waggonansicht"] = "DB Waggons";
            row["ADRRequire"] = true;
            row["DateRequire"] = true;
            dt.Rows.Add(row);
            i++;
            row = dt.NewRow();
            row["ID"] = i;
            row["Waggonansicht"] = "DB Waggons Eingänge";
            row["ADRRequire"] = true;
            row["DateRequire"] = true;
            dt.Rows.Add(row);

            i++;
            row = dt.NewRow();
            row["ID"] = i;
            row["Waggonansicht"] = "DB Waggons Ausgänge";
            row["ADRRequire"] = true;
            row["DateRequire"] = true;
            dt.Rows.Add(row);

            return dt;
        }
    }
}
