using System;
using System.Data;


namespace LVS
{
    public class clsExtraChargeAssignment
    {
        public Globals._GL_USER _GL_User;
        public clsExtraCharge ExtraCharge;

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
        public decimal ExtraChargeID { get; set; }
        public decimal ArtikelID { get; set; }
        public DateTime Datum { get; set; }
        public string Einheit { get; set; }
        public decimal Preis { get; set; }
        public Int32 Menge { get; set; }
        public string RGText { get; set; }

        /***********************************************************************************
         *                          Procedure / Methoden
         * ********************************************************************************/
        ///<summary>clsExtraChargeAssignment / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUser)
        {
            this._GL_User = myGLUser;

            ExtraCharge = new clsExtraCharge();
            ExtraCharge.InitClass(this._GL_User);
        }
        ///<summary>clsExtraChargeAssignment / Add</summary>
        ///<remarks></remarks>
        public void Add(clsExtraCharge myClsExtraCharge, bool myBSaveArt, decimal myArtTableID, decimal myLEingangTableID, Int32 myMenge, DateTime myDatetime)
        {
            decimal decTmp = 0;
            string strSQL = string.Empty;

            //clsEingang füllen
            clsLEingang ein = new clsLEingang();
            ein._GL_User = this._GL_User;
            ein.LEingangTableID = myLEingangTableID;
            ein.FillEingang();
            if (ein.dtArtInLEingang.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= ein.dtArtInLEingang.Rows.Count - 1; i++)
                {
                    decTmp = 0;
                    Decimal.TryParse(ein.dtArtInLEingang.Rows[i]["ID"].ToString(), out decTmp);
                    if (decTmp > 0)
                    {
                        clsExtraChargeAssignment ecAss = new clsExtraChargeAssignment();
                        ecAss.InitClass(this._GL_User);

                        ecAss.ExtraChargeID = myClsExtraCharge.ID;
                        ecAss.ArtikelID = decTmp;
                        ecAss.Datum = myDatetime;
                        ecAss.Einheit = myClsExtraCharge.Einheit;
                        clsExtraChargeADR ExtraChargeADR = new clsExtraChargeADR();
                        ExtraChargeADR.ExtraChargeID = myClsExtraCharge.ID;
                        ExtraChargeADR.AdrID = ein.Auftraggeber;
                        ExtraChargeADR.Fill();
                        ecAss.Preis = ExtraChargeADR.Preis;
                        // ecAss.Preis = myClsExtraCharge.Preis;
                        ecAss.Menge = myMenge;
                        ecAss.RGText = myClsExtraCharge.RGText;

                        //Unterscheidung einzelner Artikel oder Eingang
                        if (myBSaveArt)
                        {
                            if (decTmp == myArtTableID)
                            {
                                strSQL = strSQL + "INSERT INTO ExtraChargeAssignment (ExtraChargeID, ArtikelID, Datum, Einheit, Preis, " +
                                                  "Menge, RGText) " +
                                                                "VALUES (" + ecAss.ExtraChargeID +
                                                                        ", " + ecAss.ArtikelID +
                                                                        ",'" + ecAss.Datum + "'" +
                                                                        ", '" + ecAss.Einheit + "'" +
                                                                        ", '" + ecAss.Preis.ToString().Replace(",", ".") + "'" +
                                                                        ", " + ecAss.Menge +
                                                                        ", '" + ecAss.RGText + "'" +
                                                                        ");  ";
                                strSQL = strSQL + clsArtikelVita.ArtikelSonderkostenAdd(BenutzerID, ecAss.ArtikelID, ein.LEingangID, 0, myClsExtraCharge.ID, myClsExtraCharge.Bezeichnung);
                            }
                        }
                        else
                        {
                            strSQL = strSQL + "INSERT INTO ExtraChargeAssignment (ExtraChargeID, ArtikelID, Datum, Einheit, Preis, " +
                                              "Menge, RGText) " +
                                                        "VALUES (" + ecAss.ExtraChargeID +
                                                                ", " + ecAss.ArtikelID +
                                                                ",'" + ecAss.Datum + "'" +
                                                                ", '" + ecAss.Einheit + "'" +
                                                                ", '" + ecAss.Preis.ToString().Replace(",", ".") + "'" +
                                                                ", " + ecAss.Menge +
                                                                ", '" + ecAss.RGText + "'" +
                                                                ");  ";
                            strSQL = strSQL + clsArtikelVita.ArtikelSonderkostenAdd(BenutzerID, ecAss.ArtikelID, ein.LEingangID, 0, myClsExtraCharge.ID, myClsExtraCharge.Bezeichnung);
                        }
                    }
                }
            }
            bool bInsertOK = clsSQLcon.ExecuteSQLWithTRANSACTION(strSQL, "ExtraChargeAssignment", BenutzerID);
            if (bInsertOK)
            {


            }
        }
        ///<summary>clsExtraChargeAssignment / Delete</summary>
        ///<remarks>Datensatz löschen</remarks>
        public void Delete()
        {
            if (this.ID > 0)
            {
                clsArtikel art = new clsArtikel();
                art._GL_User = this._GL_User;
                art.ID = this.ArtikelID;
                art.GetArtikeldatenByTableID();

                string strSql = string.Empty;
                strSql = "DELETE FROM ExtraChargeAssignment WHERE ID=" + ID + "; ";
                strSql = strSql + clsArtikelVita.ArtikelSonderkostenDelete(BenutzerID, art.ID, art.Eingang.LEingangID, 0, this.ExtraChargeID, this.ExtraCharge.Bezeichnung);

                bool bDeleteOK = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "ExtraChargeAssignmentDelete", BenutzerID);
                if (bDeleteOK)
                {
                }
            }
        }

        ///<summary>clsExtraChargeAssignment / Update</summary>
        ///<remarks>Update Daten.</remarks>
        public void Update()
        {
            if (this.ID > 0)
            {
                string strSQL = string.Empty;
                strSQL = "Update ExtraChargeAssignment SET " +
                                            "Einheit='" + Einheit + "'" +
                                            ", Preis='" + Preis.ToString().Replace(",", ".") + "'" +
                                            ", Menge=" + Menge +

                                                            " WHERE ID=" + ID + ";";

                bool bUpdateOK = clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
                if (bUpdateOK)
                {
                    /**+*
                    string logBeschreibung = "Sonderkosten: [" + this.ID + "] - " + Bezeichnung + "  geändert";
                    Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), logBeschreibung);
                     * ****/
                }
            }
        }
        ///<summary>clsExtraChargeAssignment / Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ExtraChargeAssignment WHERE ID=" + ID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ExtraChargeAssignment");

            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                this.ID = (decimal)dt.Rows[i]["ID"];

                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ExtraChargeID"].ToString(), out decTmp);
                this.ExtraChargeID = decTmp;

                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ArtikelID"].ToString(), out decTmp);
                this.ArtikelID = decTmp;

                this.Datum = (DateTime)dt.Rows[i]["Datum"];
                this.Einheit = dt.Rows[i]["Einheit"].ToString();

                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["Preis"].ToString(), out decTmp);
                this.Preis = decTmp;

                Int32 iTmp = 0;
                Int32.TryParse(dt.Rows[i]["Menge"].ToString(), out iTmp);
                this.Menge = iTmp;
                this.RGText = dt.Rows[i]["RGText"].ToString();

                //ClsExtraCharge
                ExtraCharge = new clsExtraCharge();
                ExtraCharge.InitClass(this._GL_User);
                ExtraCharge.ID = this.ExtraChargeID;
                ExtraCharge.Fill();
            }
        }
        ///<summary>clsExtraChargeAssignment / GetExtraChargeAssignmentListForArtikel</summary>
        ///<remarks></remarks>
        public DataTable GetExtraChargeAssignmentList(bool bForArtikel, decimal myArtID, decimal myLEingangTableID)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT e.* " +
                            ", l.LEingangID " +
                            " FROM ExtraChargeAssignment e " +
                            "INNER JOIN Artikel a ON a.ID= e.ArtikelID " +
                            "INNER JOIN LEingang l ON l.ID=a.LEingangTableID";
            if (bForArtikel)
            {
                strSql = strSql + " WHERE a.ID=" + myArtID + " ;";
            }
            else
            {
                strSql = strSql + " WHERE l.ID=" + myLEingangTableID + " ;";
            }
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ExtraChargeAssignment");
            return dt;
        }

    }
}
