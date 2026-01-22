using System;
using System.Data;

namespace Sped4.Settings
{
    public class ctrJournalSettings
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataTable GetInventoryListBy()
        {
            /********************************************
             *alle
           *nur Eingänge
           *nur Ausgänge
           *mit Schaden
           *ohne Schaden
           *Sperrlager
           *Rücklieferungen
             *Direktanlieferungen
             *Lagertransporte IN
             * *****************************************/

            DataTable dt = new DataTable("Journalarten");
            dt.Columns.Add("ID", typeof(Int32));
            dt.Columns.Add("Journalart", typeof(String));

            Int32 i = -1;

            i++; //0
            DataRow row = dt.NewRow();
            row["ID"] = i;
            row["Journalart"] = "alle";
            dt.Rows.Add(row);

            i++; //1
            row = dt.NewRow();
            row["ID"] = i;
            row["Journalart"] = "nur Eingänge";
            dt.Rows.Add(row);

            i++; //2
            row = dt.NewRow();
            row["ID"] = i;
            row["Journalart"] = "nur Ausgänge";
            dt.Rows.Add(row);

            i++;//3
            row = dt.NewRow();
            row["ID"] = i;
            row["Journalart"] = "mit Schaden";
            dt.Rows.Add(row);

            i++; //4
            row = dt.NewRow();
            row["ID"] = i;
            row["Journalart"] = "ohne Schaden";
            dt.Rows.Add(row);

            i++; //5
            row = dt.NewRow();
            row["ID"] = i;
            row["Journalart"] = "Sperrlager";
            dt.Rows.Add(row);

            i++; //6
            row = dt.NewRow();
            row["ID"] = i;
            row["Journalart"] = "Rücklieferungen";
            dt.Rows.Add(row);

            i++;//7
            row = dt.NewRow();
            row["ID"] = i;
            row["Journalart"] = "Direktanlieferungen";
            dt.Rows.Add(row);

            i++;//8
            row = dt.NewRow();
            row["ID"] = i;
            row["Journalart"] = "Lagertransporte [IN]";
            dt.Rows.Add(row);

            i++; //9
            row = dt.NewRow();
            row["ID"] = i;
            row["Journalart"] = "Lagertransporte [OUT]";
            dt.Rows.Add(row);

            return dt;
        }

        ///<summary>Globals / InitTableJournalarten</summary>
        ///<remarks></remarks>
        public static DataTable InitTableJournalarten()
        {
            /********************************************
             *alle
           *nur Eingänge
           *nur Ausgänge
           *mit Schaden
           *ohne Schaden
           *Sperrlager
           *Rücklieferungen
             *Direktanlieferungen
             *Lagertransporte IN
             * *****************************************/

            DataTable dt = new DataTable("Journalarten");
            dt.Columns.Add("ID", typeof(Int32));
            dt.Columns.Add("Journalart", typeof(String));

            Int32 i = -1;

            i++; //0
            DataRow row = dt.NewRow();
            row["ID"] = i;
            row["Journalart"] = "alle";
            dt.Rows.Add(row);

            i++; //1
            row = dt.NewRow();
            row["ID"] = i;
            row["Journalart"] = "nur Eingänge";
            dt.Rows.Add(row);

            i++; //2
            row = dt.NewRow();
            row["ID"] = i;
            row["Journalart"] = "nur Ausgänge";
            dt.Rows.Add(row);

            i++;//3
            row = dt.NewRow();
            row["ID"] = i;
            row["Journalart"] = "mit Schaden";
            dt.Rows.Add(row);

            i++; //4
            row = dt.NewRow();
            row["ID"] = i;
            row["Journalart"] = "ohne Schaden";
            dt.Rows.Add(row);

            i++; //5
            row = dt.NewRow();
            row["ID"] = i;
            row["Journalart"] = "Sperrlager";
            dt.Rows.Add(row);

            i++; //6
            row = dt.NewRow();
            row["ID"] = i;
            row["Journalart"] = "Rücklieferungen";
            dt.Rows.Add(row);

            i++;//7
            row = dt.NewRow();
            row["ID"] = i;
            row["Journalart"] = "Direktanlieferungen";
            dt.Rows.Add(row);

            i++;//8
            row = dt.NewRow();
            row["ID"] = i;
            row["Journalart"] = "Lagertransporte [IN]";
            dt.Rows.Add(row);

            i++; //9
            row = dt.NewRow();
            row["ID"] = i;
            row["Journalart"] = "Lagertransporte [OUT]";
            dt.Rows.Add(row);

            return dt;
        }
    }
}
