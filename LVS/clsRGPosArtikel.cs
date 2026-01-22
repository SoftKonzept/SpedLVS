using System;
using System.Data;


namespace LVS
{
    public class clsRGPosArtikel
    {
        public Globals._GL_USER _GL_User;
        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = _GL_User.User_ID;
                return _BenutzerID;
            }
            set { _BenutzerID = value; }
        }


        public decimal ID { get; set; }
        public decimal RGPosID { get; set; }
        public decimal ArtikelID { get; set; }
        public DateTime AbgerechnetVon { get; set; }
        public DateTime AbgerechnetBis { get; set; }
        public bool Storno { get; set; }
        public string TransDirection { get; set; }
        public decimal Menge { get; set; }
        public decimal Preis { get; set; }
        public Int32 Dauer { get; set; }
        public decimal Kosten { get; set; }
        public decimal TarifPosID { get; set; }
        public bool IsUBCalc { get; set; }
        public string ArtRGTxt { get; set; }
        public DateTime LstDatum { get; set; }
        public string LstEinheit { get; set; }

        ///<summary>clsRGPosArtikel / Fill</summary>
        ///<remarks>Füll die Klasse anhand der ID.</remarks>
        public void Fill()
        {
            if (ID > 0)
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = "Select a.* " +
                                "FROM RGPosArtikel a WHERE a.ID=" + ID + "; ";
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "RGPosArtikel");
                if (dt.Rows.Count > 0)
                {
                    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        this.ID = (decimal)dt.Rows[i]["ID"];
                        this.RGPosID = (decimal)dt.Rows[i]["RGPosID"]; ;
                        this.ArtikelID = (decimal)dt.Rows[i]["ArtikelID"];
                        this.AbgerechnetVon = (DateTime)dt.Rows[i]["AbgerechnetVon"];
                        this.AbgerechnetBis = (DateTime)dt.Rows[i]["AbgerechnetBis"];
                        this.Storno = (bool)dt.Rows[i]["Storno"];
                        this.TransDirection = dt.Rows[i]["TransDirection"].ToString();
                        decimal decTmp = 0;
                        Decimal.TryParse(dt.Rows[i]["Menge"].ToString(), out decTmp);
                        this.Menge = decTmp;
                        decTmp = 0;
                        Decimal.TryParse(dt.Rows[i]["Preis"].ToString(), out decTmp);
                        this.Preis = decTmp;
                        Int32 iTmp = 0;
                        Int32.TryParse(dt.Rows[i]["Dauer"].ToString(), out iTmp);
                        this.Dauer = iTmp;
                        decTmp = 0;
                        Decimal.TryParse(dt.Rows[i]["Kosten"].ToString(), out decTmp);
                        this.Kosten = decTmp;
                        decTmp = 0;
                        Decimal.TryParse(dt.Rows[i]["TarifPosID"].ToString(), out decTmp);
                        this.TarifPosID = decTmp;
                        this.IsUBCalc = (bool)dt.Rows[i]["IsUBCalc"];
                        this.ArtRGTxt = dt.Rows[i]["ArtRGTxt"].ToString();
                        DateTime dtTmp = Globals.DefaultDateTimeMinValue;
                        DateTime.TryParse(dt.Rows[i]["LstDatum"].ToString(), out dtTmp);
                        this.LstDatum = dtTmp;
                        this.LstEinheit = dt.Rows[i]["LstEinheit"].ToString();
                    }
                }
            }
        }
        ///<summary>clsRGPosArtikel / GetArtikelListByRGPosID</summary>
        ///<remarks>Füll die Klasse anhand der ID.</remarks>
        public void GetArtikelListByRGPosID(decimal myRGPosID)
        {
            /***
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select a.* " +
                            "FROM RGPosArtikel a WHERE a.ID=" + ID + "; ";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "RGPosArtikel");
            if (dt.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    this.ID = (decimal)dt.Rows[i]["ID"];
                    this.RGPosID = (decimal)dt.Rows[i]["RGPosID"]; ;
                    this.ArtikelID = (decimal)dt.Rows[i]["ArtikelID"];
                    this.AbgerechnetVon = (DateTime)dt.Rows[i]["AbgerechnetVon"];
                    this.AbgerechnetBis = (DateTime)dt.Rows[i]["AbgerechnetBis"];
                }
            }
             * ****/
        }
        ///<summary>clsRGPosArtikel / GetArtikelListByRGID</summary>
        ///<remarks>
        ///             Füll die Klasse anhand der ID.
        ///             03.06.2025 mr
        ///             geändert von RGNr auf RGTableId
        /// 
        /// </remarks>
        //public DataTable GetArtikelListByRGID(decimal myRGID, decimal myMandantenID)
        public DataTable GetArtikelListByRGID(int myRGTableId, decimal myMandantenID)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;

            strSql = "Select " +
                         "pos.Abrechnungsart " +
                         ", b.ArtikelID " +
                         ", a.LVS_ID " +
                         ", a.Produktionsnummer" +
                         ", f.Bezeichnung as Gut" +
                         ", e.LEingangID " +
                         ", e.Date as Eingangsdatum " +
                         ", g.LAusgangID " +
                         ", g.Datum as Ausgangsdatum " +
                         ", DATEDIFF(day, e.Date, g.Datum)+1 as Lagerdauer " +
                         ", a.Anzahl " +
                         ", a.Einheit " +
                         ", a.Netto " +
                         ", a.Brutto " +

                       "From RGPosArtikel b " +
                            "INNER JOIN RGPositionen pos ON pos.ID=b.RGPosID " +
                            "INNER JOIN Rechnungen re ON re.ID=pos.RGTableID " +
                            "INNER JOIN Artikel a ON a.ID = b.ArtikelID " +
                            "INNER JOIN Gueterart f ON f.ID = a.GArtID " +
                            "INNER JOIN LEingang e ON e.ID = a.LEingangTableID " +
                            "LEFT JOIN LAusgang g ON g.ID = a.LAusgangTableID " +
                        "WHERE " +
                            //"re.RGNr=" + myRGID + " " +
                            "re.ID = " + myRGTableId +
                        " Order by pos.Abrechnungsart ";

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ArtikelList");
            return dt;
        }
        ///<summary>clsRGPosArtikel / GetArtikelListVorschau</summary>
        ///<remarks></remarks>
        public DataTable GetArtikelListVorschau(DataTable mydt)
        {
            DataTable dt = new DataTable();
            if (mydt.Rows.Count > 0)
            {
                string strArtikel = string.Empty;
                string myAbrechnungsart = string.Empty;
                //artikel String erstellen
                for (Int32 i = 0; i <= mydt.Rows.Count - 1; i++)
                {
                    if (i == 0)
                    {
                        strArtikel = mydt.Rows[i]["ID"].ToString();
                        myAbrechnungsart = mydt.Rows[i]["Abrechnungsart"].ToString();
                    }
                    else
                    {
                        strArtikel = strArtikel + "," + mydt.Rows[i]["ID"].ToString();
                    }
                }


                string strSql = string.Empty;
                strSql = "Select DISTINCT " +
                                        "'" + myAbrechnungsart + "' as Abrechnungsart" +
                                        ", e.ID as ArtikelID" +
                                        ", e.LVS_ID" +
                                        ", f.LEingangID" +
                                        ", f.Date as Eingangsdatum" +
                                        ", g.LAusgangID" +
                                        ", g.Datum as Ausgangsdatum" +
                                        ", DATEDIFF(day, f.Date, g.Datum)+1 as Lagerdauer" +
                                        ", e.Anzahl" +
                                        ", e.Einheit" +
                                        ", e.Netto" +
                                        ", e.Brutto" +

                                        " FROM Artikel e " +
                                                "INNER JOIN LEingang f ON f.ID = e.LEingangTableID " +
                                                "LEFT JOIN LAusgang g ON g.ID = e.LAusgangTableID " +
                                        "WHERE e.ID IN (" + strArtikel + ") " +
                                        "ORDER BY e.ID; ";

                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ArtikelList");
            }
            return dt;
        }
        ///<summary>clsRGPositionen / GetSQLAddRechnungsPositionString</summary>
        ///<remarks>SQL String zum Datenbank Insert in RGPositionen.</remarks>
        public string GetSQLUpdateRGPosArtikelString(decimal myStornoRGTableID)
        {
            string strSQL = string.Empty;
            strSQL = "Update RGPosArtikel SET Storno =1 " +
                                "WHERE ID IN ( " +
                                                    "SELECT a.ID FROM RGPosArtikel a " +
                                                                 "INNER JOIN RGPositionen b ON b.ID=a.RGPosID " +
                                                                 "WHERE b.RGTableID=" + myStornoRGTableID +
                                             ")";
            return strSQL;
        }
    }
}
