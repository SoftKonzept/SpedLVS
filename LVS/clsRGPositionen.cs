using System;
using System.Data;

namespace LVS
{
    public class clsRGPositionen
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
        public decimal RGID { get; set; }
        public Int32 Position { get; set; }
        public string RGText { get; set; }
        public string AbrEinheit { get; set; }
        public decimal TarifPosID { get; set; }
        public string Tariftext { get; set; }
        public decimal MargeEuro { get; set; }
        public decimal MargeProzent { get; set; }
        public decimal Menge { get; set; }
        public decimal EinzelPreis { get; set; }
        public decimal NettoPreis { get; set; }
        public string AbrechnungsArt { get; set; }
        public decimal Anfangsbestand { get; set; }
        public decimal Abgang { get; set; }
        public decimal Zugang { get; set; }
        public decimal Endbestand { get; set; }
        public string RGPosText { get; set; }
        public Int32 FibuKto { get; set; }
        //public Int32 CalcModus { get; set; }
        public enumCalcultationModus CalcModus { get; set; }
        public Int32 CalcModValue { get; set; }

        public decimal TarifID { get; set; }
        public decimal PricePerUnitFactor { get; set; }
        public decimal TarifPricePerUnit { get; set; }

        /*************************************************************************************
         * 
         * ***********************************************************************************/
        ///<summary>clsRGPositionen / Fill</summary>
        ///<remarks>Füll die Klasse anhand der ID.</remarks>
        public void Fill()
        {
            //if (clsTarifPosition.ExistTarifPosition(ID, BenutzerID))
            //{
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select a.* " +
                            "FROM RGPositionen a WHERE a.ID=" + ID + "; ";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "RGPositionen");
            if (dt.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    this.ID = (decimal)dt.Rows[i]["ID"];
                    this.RGID = (decimal)dt.Rows[i]["RGTableID"]; ;
                    this.Position = (Int32)dt.Rows[i]["Position"];
                    this.AbrEinheit = dt.Rows[i]["AbrechnungsEinheit"].ToString();
                    this.Menge = (decimal)dt.Rows[i]["Menge"];
                    this.EinzelPreis = (decimal)dt.Rows[i]["EinzelPreis"];
                    this.NettoPreis = (decimal)dt.Rows[i]["NettoPreis"];
                    this.AbrechnungsArt = dt.Rows[i]["AbrechnungsArt"].ToString();
                    this.TarifPosID = (decimal)dt.Rows[i]["TarifPosID"];
                    this.TarifID = 0;
                    if (this.TarifPosID > 0)
                    {
                        this.TarifID = clsTarif.GetTarifIDByTarifPosID(this._GL_User, this.TarifPosID);
                    }
                    this.Tariftext = dt.Rows[i]["Tariftext"].ToString();
                    this.MargeEuro = (decimal)dt.Rows[i]["MargeEuro"];
                    this.MargeProzent = (decimal)dt.Rows[i]["MargeProzent"];
                    this.Anfangsbestand = (decimal)dt.Rows[i]["Anfangsbestand"];
                    this.Abgang = (decimal)dt.Rows[i]["Abgang"];
                    this.Zugang = (decimal)dt.Rows[i]["Zugang"];
                    this.Endbestand = (decimal)dt.Rows[i]["Endbestand"];
                    this.RGPosText = dt.Rows[i]["RGPosText"].ToString();
                    Int32 iTmp = 0;
                    Int32.TryParse(dt.Rows[i]["FibuKto"].ToString(), out iTmp);
                    this.FibuKto = iTmp;
                    iTmp = 0;
                    Int32.TryParse(dt.Rows[i]["CalcModus"].ToString(), out iTmp);
                    this.CalcModus = EnumConverter.GetEnumObjectByValue<enumCalcultationModus>(iTmp);
                    iTmp = 0;
                    Int32.TryParse(dt.Rows[i]["CalcModValue"].ToString(), out iTmp);
                    this.CalcModValue = iTmp;
                    decimal decTmp = 0;
                    decimal.TryParse(dt.Rows[i]["PricePerUnitFactor"].ToString(), out decTmp);
                    this.PricePerUnitFactor = decTmp;
                    decTmp = 0;
                    decimal.TryParse(dt.Rows[i]["TarifPricePerUnit"].ToString(), out decTmp);
                    this.TarifPricePerUnit = decTmp;
                    
                }
            }
            //}
        }
        ///<summary>clsRGPositionen / FillFirstRGPosOfRG</summary>
        ///<remarks>Ermittel die erste Position einer Rechnung und füllt damit die Klasse.</remarks>
        public void FillFirstRGPosOfRG()
        {
            string strSql = string.Empty;
            strSql = "Select MIN(a.ID) " +
                            "FROM RGPositionen a WHERE a.RGTableID=" + RGID + "; ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0;
            Decimal.TryParse(strTmp, out decTmp);
            ID = decTmp;
            if (ID > 0)
            {
                Fill();
            }
        }
        ///<summary>clsRGPositionen / GetRGPositionByRGTableID</summary>
        ///<remarks></remarks>
        public static DataTable GetRGPositionByRGTableID(Globals._GL_USER myGLUser, decimal myRGTableID)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select a.* " +
                            "FROM RGPositionen a WHERE a.RGTableID=" + myRGTableID + "; ";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGLUser.User_ID, "TarifPosition");
            return dt;
        }
        ///<summary>clsRGPositionen / Fill</summary>
        ///<remarks>Füll die Klasse anhand der ID.</remarks>
        public string GetSQLStornoUpdateItems(Globals._GL_USER myGLUser, decimal myRGTableID, decimal myStornoRGTableID)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "INSERT INTO RGPositionen (RGTableID, Position, RGText, Abrechnungseinheit, Menge, EinzelPreis, " +
                                                "NettoPreis, Abrechnungsart, TarifPosID, Tariftext, MargeEuro, " +
                                                "MargeProzent, Anfangsbestand, Abgang, Zugang, Endbestand, RGPosText, FibuKto, CalcModus, CalcModValue" +
                                                ",PricePerUnitFactor, TarifPricePerUnit) " +

                                                "Select '" + myRGTableID + "' as RGTableID, Position, RGText, Abrechnungseinheit, Menge, EinzelPreis, " +
                                                "NettoPreis, Abrechnungsart, TarifPosID, Tariftext, MargeEuro, " +
                                                "MargeProzent, Anfangsbestand, Abgang, Zugang, Endbestand, RGPosText, FibuKto, CalcModus, CalcModValue " +
                                                ", PricePerUnitFactor, TarifPricePerUnit " +

                                                " FROM RGPositionen a WHERE a.RGTableID=" + myStornoRGTableID + "; ";
            return strSql;
        }
        ///<summary>clsRGPositionen / GetSQLAddRechnungsPositionString</summary>
        ///<remarks>SQL String zum Datenbank Insert in RGPositionen.</remarks>
        public string GetSQLAddRechnungsPositionString(ref clsRGPositionen RGPos)
        {
            string strSQL = string.Empty;
            strSQL = "INSERT INTO RGPositionen " +
                                        "(RGTableID, Position,RGText, Abrechnungseinheit, Menge, EinzelPreis," +
                                        "NettoPreis, Abrechnungsart, TarifPosID, Tariftext, MargeEuro, MargeProzent, " +
                                        "RGPosText, FibuKto, CalcModus, CalcModValue, PricePerUnitFactor, TarifPricePerUnit)" +
                                        "VALUES(" + RGPos.RGID +
                                                "," + RGPos.Position +
                                                ",'" + RGPos.RGText + "'" +
                                                ",'" + RGPos.AbrEinheit + "'" +
                                                ",'" + RGPos.Menge.ToString().Replace(',', '.') + "'" +
                                                ",'" + RGPos.EinzelPreis.ToString().Replace(',', '.') + "'" +
                                                ",'" + RGPos.NettoPreis.ToString().Replace(',', '.') + "'" +
                                                ",'" + RGPos.AbrechnungsArt + "'" +
                                                "," + RGPos.TarifPosID +
                                                ",'" + RGPos.Tariftext + "'" +
                                                ",'" + RGPos.MargeEuro.ToString().Replace(',', '.') + "'" +
                                                ",'" + RGPos.MargeProzent.ToString().Replace(',', '.') + "'" +
                                                ",'" + RGPos.Anfangsbestand.ToString().Replace(',', '.') + "'" +
                                                ",'" + RGPos.Abgang.ToString().Replace(',', '.') + "'" +
                                                ",'" + RGPos.Zugang.ToString().Replace(',', '.') + "'" +
                                                ",'" + RGPos.Endbestand.ToString().Replace(',', '.') + "'" +
                                                ",'" + RGPos.RGPosText + "' " +
                                                ", " + FibuKto +
                                                ", " + this.CalcModus +
                                                ", " + this.CalcModValue +
                                                ",'" + RGPos.PricePerUnitFactor.ToString().Replace(',', '.') + "'" +
                                                ",'" + RGPos.TarifPricePerUnit.ToString().Replace(',', '.') + "'" +                                                
                                                "); ";
            return strSQL;
        }

        public void Delete()
        {
            //FK in RGPositionen noch anpassen,damit die 
            string strSql = string.Empty;
            strSql = "Delete RGPositionen WHERE ID=" + ID + "; ";
            if (clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "Delete", this._GL_User.User_ID))
            {
                ////Add Logbucheintrag Löschen
                //string Txt = "ID:[" + this.ID.ToString() + "]/ RG-/GS-Nr:[" + this.RGNr + "] / Mandant:[" + this.MandantenID + "]";
                //string Beschreibung = "Rechnung: " + Txt + "  gelöscht";
                //Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Loeschung.ToString(), Beschreibung);
            }
        }


    }
}
