using System;
using System.Data;

namespace Sped4.Settings
{
    public class ctrPrintSettings
    {
        ///<summary>Globals / InitDataTableArtikelDBColumns</summary>
        ///<remarks></remarks>
        public static void InitDataTableArtikelDBColumns(ref DataTable dt)
        {
            if (dt.Columns["Spalte"] == null)
            {
                DataColumn c0 = new DataColumn();
                c0.DataType = System.Type.GetType("System.String");
                c0.ColumnName = "Spalte";
                dt.Columns.Add(c0);
            }
            if (dt.Columns["Selected"] == null)
            {
                DataColumn c1 = new DataColumn();
                c1.DataType = System.Type.GetType("System.Boolean");
                c1.ColumnName = "Selected";
                dt.Columns.Add(c1);
            }
            if (dt.Columns["Text"] == null)
            {
                DataColumn c2 = new DataColumn();
                c2.DataType = System.Type.GetType("System.String");
                c2.ColumnName = "Text";
                dt.Columns.Add(c2);
            }
            if (dt.Columns["Standard"] == null)
            {
                DataColumn c3 = new DataColumn();
                c3.DataType = System.Type.GetType("System.Boolean");
                c3.ColumnName = "Standard";
                dt.Columns.Add(c3);
            }

            SetDataTableArtikelDBColumns(ref dt);
        }
        ///<summary>Globals / SetDataTableArtikelDBColumns</summary>
        ///<remarks></remarks>
        public static void SetDataTableArtikelDBColumns(ref DataTable dt)
        {
            DataRow row = dt.NewRow();
            row["Spalte"] = "ID";
            row["Selected"] = false;
            row["Text"] = "ID";
            row["Standard"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Spalte"] = "AuftragID";
            row["Selected"] = false;
            row["Text"] = "AuftragID";
            row["Standard"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Spalte"] = "AuftragPos";
            row["Selected"] = false;
            row["Text"] = "AuftragPos";
            row["Standard"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Spalte"] = "LVS_ID";
            row["Selected"] = false;
            row["Text"] = "LVS_ID";
            row["Standard"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Spalte"] = "GArt";
            row["Selected"] = false;
            row["Text"] = "Gut";
            row["Standard"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Spalte"] = "GutZusatz";
            row["Selected"] = false;
            row["Text"] = "Gut Zusatz";
            row["Standard"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Spalte"] = "Dicke";
            row["Selected"] = false;
            row["Text"] = "Dicke";
            row["Standard"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Spalte"] = "Breite";
            row["Selected"] = false;
            row["Text"] = "Breite";
            row["Standard"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Spalte"] = "Laenge";
            row["Selected"] = false;
            row["Text"] = "Länge";
            row["Standard"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Spalte"] = "Hoehe";
            row["Selected"] = false;
            row["Text"] = "Höhe";
            row["Standard"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Spalte"] = "ME";
            row["Selected"] = false;
            row["Text"] = "ME";
            row["Standard"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Spalte"] = "Einheit";
            row["Selected"] = false;
            row["Text"] = "Einheit";
            row["Standard"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Spalte"] = "gemGewicht";
            row["Selected"] = false;
            row["Text"] = "gemGewicht";
            row["Standard"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Spalte"] = "Netto";
            row["Selected"] = false;
            row["Text"] = "Netto";
            row["Standard"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Spalte"] = "Brutto";
            row["Selected"] = false;
            row["Text"] = "Brutto";
            row["Standard"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Spalte"] = "Werksnummer";
            row["Selected"] = false;
            row["Text"] = "Werksnummer";
            row["Standard"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Spalte"] = "Produktionsnummer";
            row["Selected"] = false;
            row["Text"] = "Produktionsnummer";
            row["Standard"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Spalte"] = "exBezeichnung";
            row["Selected"] = false;
            row["Text"] = "exBezeichnung";
            row["Standard"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Spalte"] = "Charge";
            row["Selected"] = false;
            row["Text"] = "Charge";
            row["Standard"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Spalte"] = "Bestellnummer";
            row["Selected"] = false;
            row["Text"] = "Bestellnummer";
            row["Standard"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Spalte"] = "exMaterialnummer";
            row["Selected"] = false;
            row["Text"] = "exMaterialnummer";
            row["Standard"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Spalte"] = "Position";
            row["Selected"] = false;
            row["Text"] = "Position";
            row["Standard"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Spalte"] = "Schaden";
            row["Selected"] = false;
            row["Text"] = "Schaden";
            row["Standard"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Spalte"] = "Schadensbeschreibung";
            row["Selected"] = false;
            row["Text"] = "Schadensbeschreibung";
            row["Standard"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Spalte"] = "Werk";
            row["Selected"] = false;
            row["Text"] = "Werk";
            row["Standard"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Spalte"] = "Halle";
            row["Selected"] = false;
            row["Text"] = "Halle";
            row["Standard"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Spalte"] = "Reihe";
            row["Selected"] = false;
            row["Text"] = "Reihe";
            row["Standard"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Spalte"] = "Platz";
            row["Selected"] = false;
            row["Text"] = "Platz";
            row["Standard"] = false;
            dt.Rows.Add(row);
        }

        ///<summary>Globals / InitTablePrintDaten</summary>
        ///<remarks>Tabelle für die Druckdatei</remarks>
        public static DataTable InitTablePrintDaten()
        {
            DataTable dt = new DataTable("Printdaten");

            DataColumn column1 = new DataColumn();
            column1.DataType = System.Type.GetType("System.Decimal");
            column1.Caption = "AuftragID";
            column1.ColumnName = "AuftragID";
            dt.Columns.Add(column1);

            DataColumn column2 = new DataColumn();
            column2.DataType = System.Type.GetType("System.Decimal");
            column2.Caption = "AuftragPos";
            column2.ColumnName = "AuftragPos";
            dt.Columns.Add(column2);

            DataColumn column3 = new DataColumn();
            column3.DataType = System.Type.GetType("System.String");
            column3.Caption = "ZM";
            column3.ColumnName = "ZM";
            dt.Columns.Add(column3);

            DataColumn column4 = new DataColumn();
            column4.DataType = System.Type.GetType("System.String");
            column4.Caption = "Auflieger";
            column4.ColumnName = "Auflieger";
            dt.Columns.Add(column4);

            DataColumn column5 = new DataColumn();
            column5.DataType = System.Type.GetType("System.String");
            column5.Caption = "Notiz";
            column5.ColumnName = "Notiz";
            dt.Columns.Add(column5);

            DataColumn column6 = new DataColumn();
            column6.DataType = System.Type.GetType("System.String");
            column6.Caption = "DocName";
            column6.ColumnName = "DocName";
            dt.Columns.Add(column6);

            DataColumn column7 = new DataColumn();
            column7.DataType = System.Type.GetType("System.String");
            column7.Caption = "Ladenummer";
            column7.ColumnName = "Ladenummer";
            dt.Columns.Add(column7);

            DataColumn column8 = new DataColumn();
            column8.DataType = System.Type.GetType("System.String");
            column8.Caption = "ZF";
            column8.ColumnName = "ZF";
            dt.Columns.Add(column8);

            DataColumn column9 = new DataColumn();
            column9.DataType = System.Type.GetType("System.String");
            column9.Caption = "Fahrer";
            column9.ColumnName = "Fahrer";
            dt.Columns.Add(column9);

            DataColumn column10 = new DataColumn();
            column10.DataType = System.Type.GetType("System.Decimal");
            column10.Caption = "ADR_ID_V";
            column10.ColumnName = "ADR_ID_V";
            dt.Columns.Add(column10);

            DataColumn column11 = new DataColumn();
            column11.DataType = System.Type.GetType("System.Decimal");
            column11.Caption = "ADR_ID_E";
            column11.ColumnName = "ADR_ID_E";
            dt.Columns.Add(column11);

            DataColumn column12 = new DataColumn();
            column12.DataType = System.Type.GetType("System.DateTime");
            column12.Caption = "Date";
            column12.ColumnName = "Date";
            dt.Columns.Add(column12);

            DataColumn column13 = new DataColumn();
            column13.DataType = System.Type.GetType("System.String");
            column13.Caption = "DocArt";
            column13.ColumnName = "DocArt";
            dt.Columns.Add(column13);

            DataColumn column14 = new DataColumn();
            column14.DataType = System.Type.GetType("System.Boolean");
            column14.Caption = "PrintNotiz";
            column14.ColumnName = "PrintNotiz";
            dt.Columns.Add(column14);

            DataColumn column15 = new DataColumn();
            column15.DataType = System.Type.GetType("System.Decimal");
            column15.Caption = "AuftragPosTableID";
            column15.ColumnName = "AuftragPosTableID";
            dt.Columns.Add(column15);

            /**
            DataColumn column15 = new DataColumn();
            column14.DataType = System.Type.GetType("System.Decimal");
            column14.Caption = "AuftragPosTableID";
            column14.ColumnName = "AuftragPosTableID";
            dt.Columns.Add(column15);
            ***/
            return dt;
        }
        ///<summary>Globals / SetArtikelColumnStandard</summary>
        ///<remarks></remarks>
        public static void SetArtikelColumnStandard(ref DataTable dt)
        {
            //die Standardspalten sollen nicht 
            /**********************************************+
             * Standardausgabe:
             * - Werksnummer
             * - Anzahl (ME)
             * - Gut
             * - Dicke
             * - Breite
             * - Länge
             * - Höhe
             * - Gewicht(tatGewicht 
             * **********************************************/

            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                String strColumn = string.Empty;
                strColumn = dt.Rows[i]["Text"].ToString();
                if (
                    //raus STandard wird nicht in der Litste dgv ctrPrint angezeigt
                    (strColumn == "Werksnummer") |
                    (strColumn == "ME") |
                    (strColumn == "Gut") |
                    (strColumn == "Dicke") |
                    (strColumn == "Breite") |
                    (strColumn == "Länge") |
                    (strColumn == "Höhe") |
                    (strColumn == "Brutto") |
                    (strColumn == "gemGewicht") |
                    (strColumn == "AuftragID") |
                    (strColumn == "AuftragPos") |
                    (strColumn == "ID") |
                    (strColumn == "Einheit") |
                    (strColumn == "LVS_ID") |
                    (strColumn == "Schaden") |
                    (strColumn == "Schadensbeschreibung") |
                    (strColumn == "Werk") |
                    (strColumn == "Halle") |
                    (strColumn == "Reihe") |
                    (strColumn == "Platz")
                    )
                {
                    dt.Rows[i]["Standard"] = true;
                }
                dt.Rows[i]["Selected"] = true;
            }
        }

        //public static void SetArtikelColumnStandard(ref DataTable dt)
        //{
        //    //die Standardspalten sollen nicht 
        //    /**********************************************+
        //     * Standardausgabe:
        //     * - Werksnummer
        //     * - Anzahl (ME)
        //     * - Gut
        //     * - Dicke
        //     * - Breite
        //     * - Länge
        //     * - Höhe
        //     * - Gewicht(tatGewicht 
        //     * **********************************************/

        //    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
        //    {
        //        String strColumn = string.Empty;
        //        strColumn = dt.Rows[i]["Text"].ToString();
        //        if (
        //            //raus STandard wird nicht in der Litste dgv ctrPrint angezeigt
        //            (strColumn == "Werksnummer") |
        //            (strColumn == "ME") |
        //            (strColumn == "Gut") |
        //            (strColumn == "Dicke") |
        //            (strColumn == "Breite") |
        //            (strColumn == "Länge") |
        //            (strColumn == "Höhe") |
        //            (strColumn == "Brutto") |
        //            (strColumn == "gemGewicht") |
        //            (strColumn == "AuftragID") |
        //            (strColumn == "AuftragPos") |
        //            (strColumn == "ID") |
        //            (strColumn == "Einheit") |
        //            (strColumn == "LVS_ID") |
        //            (strColumn == "Schaden") |
        //            (strColumn == "Schadensbeschreibung") |
        //            (strColumn == "Werk") |
        //            (strColumn == "Halle") |
        //            (strColumn == "Reihe") |
        //            (strColumn == "Platz")
        //            )
        //        {
        //            dt.Rows[i]["Standard"] = true;
        //        }
        //        dt.Rows[i]["Selected"] = true;
        //    }
        //}
    }
}
