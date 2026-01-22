using LVS;
using System;
using System.Data;

namespace Sped4.Settings
{
    public class ctrBestandSettings
    {
        ///<summary>Globals / InitTableBestandslistenarten</summary>
        ///<remarks></remarks>
        //public static DataTable InitTableBestandslistenarten(clsClient myClient)
        public static DataTable InitTableBestandslistenarten(clsClient myClient)
        {
            /********************************************
             * -bitte wählen Sie-
             * Tagesanfangbestand 
             * Tagesendbestand
             * Tagesendbestand [Empfänger]
             * Tagesbestand [über alle Arbeitsbereiche]
             * Inventur
             * Bestand nach Zeitraum          * 
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
             * LagergeldproTag
             * 
             * *****************************************/

            DataTable dt = new DataTable("Bestandsarten");
            dt.Columns.Add("ID", typeof(Int32));
            dt.Columns.Add("Bestandsart", typeof(String));
            dt.Columns.Add("ADRRequire", typeof(bool));
            dt.Columns.Add("DateRequire", typeof(bool));
            Int32 i = -1;

            i++;
            DataRow row = dt.NewRow();
            row["ID"] = i;
            row["Bestandsart"] = "-bitte wählen Sie-";
            row["ADRRequire"] = false;
            row["DateRequire"] = false;
            dt.Rows.Add(row);

            i++;
            row = dt.NewRow();
            row["ID"] = i;
            row["Bestandsart"] = "Tagesbestand";
            row["ADRRequire"] = true;
            row["DateRequire"] = false;
            dt.Rows.Add(row);

            i++;
            row = dt.NewRow();
            row["ID"] = i;
            row["Bestandsart"] = "Tagesbestand [ohne SPL]";
            row["ADRRequire"] = true;
            row["DateRequire"] = false;
            dt.Rows.Add(row);

            i++;
            row = dt.NewRow();
            row["ID"] = i;
            row["Bestandsart"] = "Tagesendbestand[Empfänger]";
            row["ADRRequire"] = true;
            row["DateRequire"] = false;
            dt.Rows.Add(row);

            i++;
            row = dt.NewRow();
            row["ID"] = i;
            row["Bestandsart"] = "Tagesbestand [Lager komplett]";
            row["ADRRequire"] = false;
            row["DateRequire"] = false;
            dt.Rows.Add(row);

            i++;
            row = dt.NewRow();
            row["ID"] = i;
            row["Bestandsart"] = clsLager.const_Bestandsart_TagesbestandAllExclDam.ToString();
            row["ADRRequire"] = false;
            row["DateRequire"] = false;
            dt.Rows.Add(row);

            i++;
            row = dt.NewRow();
            row["ID"] = i;
            row["Bestandsart"] = clsLager.const_Bestandsart_TagesbestandAllExclSPL.ToString();
            row["ADRRequire"] = false;
            row["DateRequire"] = false;
            dt.Rows.Add(row);

            i++;
            row = dt.NewRow();
            row["ID"] = i;
            row["Bestandsart"] = clsLager.const_Bestandsart_TagesbestandAllExclDamSPL.ToString();
            row["ADRRequire"] = false;
            row["DateRequire"] = false;
            dt.Rows.Add(row);

            if (myClient.Modul.Lager_Bestandsliste_BestandOverAllWorkspaces)
            {
                i++;
                row = dt.NewRow();
                row["ID"] = i;
                row["Bestandsart"] = clsLager.const_Bestandsart_TagesbestandAccrossAllWorkspaces.ToString();
                row["ADRRequire"] = true;
                row["DateRequire"] = false;
                dt.Rows.Add(row);
            }

            i++;
            row = dt.NewRow();
            row["ID"] = i;
            row["Bestandsart"] = "Inventur";
            row["ADRRequire"] = true;
            row["DateRequire"] = false;
            dt.Rows.Add(row);

            //i++;
            //row = dt.NewRow();
            //row["ID"] = i;
            //row["Bestandsart"] = "Empfänger[geprüft]";
            //row["ADRRequire"] = true;
            //row["DateRequire"] = true;
            //dt.Rows.Add(row);

            i++;
            row = dt.NewRow();
            row["ID"] = i;
            row["Bestandsart"] = "Sperrlager[SPL]";
            row["ADRRequire"] = true;
            row["DateRequire"] = false;
            dt.Rows.Add(row);

            i++;
            row = dt.NewRow();
            row["ID"] = i;
            row["Bestandsart"] = "Rücklieferungen[RL]";
            row["ADRRequire"] = true;
            row["DateRequire"] = true;
            dt.Rows.Add(row);

            i++;
            row = dt.NewRow();
            row["ID"] = i;
            row["Bestandsart"] = "Direktanlieferungen";
            row["ADRRequire"] = true;
            row["DateRequire"] = true;
            dt.Rows.Add(row);

            i++;
            row = dt.NewRow();
            row["ID"] = i;
            row["Bestandsart"] = "Ungeprüfte Artikel im Eingang";
            row["ADRRequire"] = true;
            row["DateRequire"] = false;
            dt.Rows.Add(row);

            i++;
            row = dt.NewRow();
            row["ID"] = i;
            row["Bestandsart"] = "Ungeprüfte Artikel im Ausgang";
            row["ADRRequire"] = true;
            row["DateRequire"] = false;
            dt.Rows.Add(row);


            i++;
            row = dt.NewRow();
            row["ID"] = i;
            row["Bestandsart"] = "Artikel in offenen Eingängen";
            row["ADRRequire"] = true;
            row["DateRequire"] = false;
            dt.Rows.Add(row);

            i++;
            row = dt.NewRow();
            row["ID"] = i;
            row["Bestandsart"] = "Artikel in offenen Ausgängen";
            row["ADRRequire"] = true;
            row["DateRequire"] = false;
            dt.Rows.Add(row);

            i++;
            row = dt.NewRow();
            row["ID"] = i;
            row["Bestandsart"] = "Nicht abgeschlossene Eingänge";
            row["ADRRequire"] = true;
            row["DateRequire"] = false;
            dt.Rows.Add(row);

            i++;
            row = dt.NewRow();
            row["ID"] = i;
            row["Bestandsart"] = "Nicht abgeschlossene Ausgänge";
            row["ADRRequire"] = true;
            row["DateRequire"] = false;
            dt.Rows.Add(row);

            i++;
            row = dt.NewRow();
            row["ID"] = i;
            row["Bestandsart"] = clsLager.const_Bestandsart_LagergeldTag;
            row["ADRRequire"] = true;
            row["DateRequire"] = true;
            dt.Rows.Add(row);

            return dt;
        }

        public static DataTable BestandsArt_Customized(DataTable myDt, clsClient myClient)
        {
            DataTable dt = myDt.Copy();
            foreach (DataRow r in myDt.Rows)
            {
                //DataRow newRow = r;
                if (r["Bestandsart"].ToString().Equals(clsLager.const_Bestandsart_TagesbestandAccrossAllWorkspaces.ToString()))
                {
                    if (!myClient.Modul.Lager_Bestandsliste_BestandOverAllWorkspaces)
                    {
                        dt.Rows.Remove(r);
                        //dt.Rows.Add(newRow);
                    }
                }
                else
                {
                    //dt.Rows.Add(newRow);
                }
            }
            return myDt;
        }


    }
}
