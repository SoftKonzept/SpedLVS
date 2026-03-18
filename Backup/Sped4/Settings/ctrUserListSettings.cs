using LVS;
using System;
using System.Data;

namespace Sped4.Settings
{
    public class ctrUserListSettings
    {

        ///<summary>Globals / InitTableJournalarten</summary>
        ///<remarks>Spaltennamen aus den folgenden Tabelle werden gelesen:
        ///         - Artikel
        ///         - LEingang
        ///         - LAusgang</remarks>
        public static DataTable InitTableDBColumns(Globals._GL_USER myGLUser)
        {
            /********************************************
             *Spalten der Datenbanken:
             *Artikel
             *LEingang
             *LAusgang
             * *****************************************/
            DataTable dt = new DataTable();
            string strSQL = string.Empty;
            strSQL = "SELECT Table_Name as 'Table'" +
                            ", column_name as Col" +
                            ", Table_Name+'.'+column_name as SQLString" +
                            ", CAST(0 as Bit) as 'Selected'" +
                            ", column_name as Datenfeld" +
                            " FROM INFORMATION_SCHEMA.COLUMNS " +
                            "WHERE " +
                            "TABLE_NAME = 'Artikel' " +
                            "OR TABLE_NAME = 'LEingang' " +
                            "OR TABLE_NAME = 'LAusgang' ";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "Tabellen");
            if (dt.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    //Spalten, die nicht angezeigt werden sollen
                    string strColCheck = dt.Rows[i]["SQLString"].ToString();
                    switch (strColCheck)
                    {
                        case "Artikel.AuftragID":
                        case "Artikel.AuftragPos":
                        case "Artikel.gemGewicht":
                        case "Artikel.BKZ":
                        case "Artikel.CheckArt":
                        case "Artikel.Storno":
                        case "Artikel.StornoDate":
                        case "Artikel.UB":
                        case "Artikel.AB_ID":
                        case "Artikel.Mandanten_ID":
                        case "Artikel.AuftragPosTableID":
                        case "Artikel.LA_Checked":
                        case "Artikel.IDvorUB":
                        case "Artikel.LEingangTableID":
                        case "Artikel.LAusgangTableID":

                        case "LAusgang.ID":
                        case "LAusgang.Netto":
                        case "LAusgang.Brutto":
                        case "LAusgang.Checked":
                        case "LAusgang.AbBereich":
                        case "LAusgang.MandantenID":
                        case "LAusgang.USER":
                        case "LAusgang.Auftraggeber":
                        case "LAusgang.Versender":
                        case "LAusgang.Empfaenger":
                        case "LAusgang.Entladestelle":
                        case "LAusgang.Lieferant":
                        case "LAusgang.SpedID":
                        case "LAusgang.KFZ":
                        case "LAusgang.DirectDelivery":

                        case "LEingang.ID":
                        case "LEingang.Check":
                        case "LEingang.GewichtNetto":
                        case "LEingang.GewichtBrutto":
                        case "LEingang.Auftraggeber":
                        case "LEingang.Empfaenger":
                        case "LEingang.Lieferant":
                        case "LEingang.Versender":
                        case "LEingang.SpedID":
                        case "LEingang.KFZ":
                        case "LEingang.AbBereich":
                        case "LEingang.Mandant":
                        case "LEingang.DirectDelivery":

                            //true, da diese direkt ausgeblendet werden
                            dt.Rows[i]["Selected"] = true;
                            break;

                        default:
                            dt.Rows[i]["Selected"] = false;
                            break;
                    }


                    //Angabe Spaltenname für Userliste
                    if (strColCheck == "Artikel.ID")
                    {
                        dt.Rows[i]["Datenfeld"] = "ArtikelID";
                    }
                    if (strColCheck == "Artikel.LVS_ID")
                    {
                        dt.Rows[i]["Datenfeld"] = "LVSNr";
                    }
                    if (strColCheck == "Artikel.GArt")
                    {
                        dt.Rows[i]["Datenfeld"] = "Gut";
                    }


                    if (strColCheck == "LEingang.LEingangID")
                    {
                        dt.Rows[i]["Datenfeld"] = "Eingang";
                    }
                    if (strColCheck == "LEingang.Date")
                    {
                        dt.Rows[i]["Datenfeld"] = "E-Datum";
                    }


                    if (strColCheck == "LAusgang.LAusgangID")
                    {
                        dt.Rows[i]["Datenfeld"] = "A-Datum";
                    }
                    if (strColCheck == "LAusgang.Datum")
                    {
                        dt.Rows[i]["Datenfeld"] = "Ausgang";
                    }
                    if (strColCheck == "LAusgang.LfsNr")
                    {
                        dt.Rows[i]["Datenfeld"] = "Lfs-Nr";
                    }
                    if (strColCheck == "LAusgang.LfsDate")
                    {
                        dt.Rows[i]["Datenfeld"] = "Lfs-Datum";
                    }

                }
            }
            return dt;
        }
    }
}
