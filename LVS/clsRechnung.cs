using Common.Models;
using LVS.ViewData;
using System;
using System.Data;


namespace LVS
{
    public class clsRechnung
    {
        public const string const_RechnungsArt_Manuell = "Manuell";
        public const string const_RechnungsArt_Lager = "Lager";

        public const string const_RGDocTitel_RG = "RECHNUNG Nr";
        public const string const_RGDocTitel_GS = "GUTSCHRIFT Nr";
        public const string const_RGDocTitel_RGStorno = "RECHNUNGSSTORNO Nr";
        public const string const_RGDocTitel_GSStorno = "GUTSCHRIFTSSTORNO Nr";
        public const string const_RGDocTitel_GSKorrektur = "GUTSCHRIFTSKORREKTUR Nr";
        public const string const_RGDocTitel_RGKorrektur = "RECHNUNGSKORREKTUR Nr";

        public const string const_RGText_Dummy = "Rechnungsplatzhalter für FIBU.";

        public clsSystem sys;
        public Globals._GL_SYSTEM _GL_System;
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

        public clsADR ADR_RGEmpfaenger = new clsADR();
        public clsRGPositionen RGPos = new clsRGPositionen();
        public clsRGPosArtikel RGPosArtikel = new clsRGPosArtikel();
        public clsRGPositionen clRGPos = new clsRGPositionen();
        public clsRGPositionen RGPosEinlagerung = new clsRGPositionen();
        public clsRGPositionen RGPosAuslagerung = new clsRGPositionen();
        public clsRGPositionen RGPosLagerbestand = new clsRGPositionen();
        public clsRGPositionen RGPosSperrlager = new clsRGPositionen();
        public clsRGPositionen RGPosLagerTransporte = new clsRGPositionen();
        public clsRGPositionen RGPosDirektanlieferung = new clsRGPositionen();
        public clsRGPositionen RGPosRuecklieferung = new clsRGPositionen();
        public clsRGPositionen RGPosVorfracht = new clsRGPositionen();
        public clsRGPositionen RGPosNebenkosten = new clsRGPositionen();
        public clsRGPositionen RGPosGleiskosten = new clsRGPositionen();
        public clsRGPositionen RGPosToll = new clsRGPositionen();

        public clsError Error = new clsError();

        public DataTable dtArtikel = new DataTable();
        public DataTable dtRGPositionen = new DataTable();     // ist zur Verarbeitung bei der Berechnung
        public DataTable dtRechnungsPositionen = new DataTable();  //Beinhaltet die gespeicherten Rechnungspositionen einer Rechnung

        public DataTable dtArtikelEinlagerung = new DataTable("ArtikelEinlagerung");
        public DataTable dtArtikelAnfangsbestand = new DataTable("ArtikelAnfangsbestand");
        public DataTable dtArtikelAuslagerung = new DataTable("ArtikelAuslagerung");
        public DataTable dtArtikelSperrlager = new DataTable("ArtikelSperrlager");
        public DataTable dtArtikelLagerTransporte = new DataTable("ArtikelLagerTransporte");
        public DataTable dtArtikelDirektanlieferung = new DataTable("ArtikelDirektanlieferung");
        public DataTable dtArtikelRuecklieferung = new DataTable("ArtikelRuecklieferung");
        public DataTable dtArtikelLagerbestand = new DataTable("ArtikelLagerbestand");
        public DataTable dtArtikelVorfracht = new DataTable("ArtikelVorfracht");
        public DataTable dtArtikelNebenkosten = new DataTable("ArtikelNebenkosten");
        public DataTable dtArtikelGleis = new DataTable("Gleisstellgebühren");
        public DataTable dtArtikelToll = new DataTable("ArtikelToll");

        public clsRechnung RGRevision;
        internal MandantenViewData MandantenViewData { get; set; }
        public Mandanten Mandant { get; set; }
        public decimal ID { get; set; }
        public decimal RGNr { get; set; }
        public DateTime Datum { get; set; }
        public DateTime faellig { get; set; }
        public bool Druck { get; set; }
        public DateTime Druckdatum { get; set; }
        public decimal Benutzer { get; set; }
        public bool GS { get; set; }
        public DateTime bezahlt { get; set; }
        public bool exFibu { get; set; }
        public string RGArt { get; set; }
        public decimal MandantenID { get; set; }
        public decimal ArBereichID { get; set; }
        public decimal NettoBetrag { get; set; }
        public decimal MwStBetrag { get; set; }
        public decimal BruttoBetrag { get; set; }
        public decimal MwStSatz { get; set; }
        public DateTime AbrZeitraumVon { get; set; }
        public DateTime AbrZeitraumBis { get; set; }
        public decimal Empfaenger { get; set; }
        public bool Storno { get; set; }
        public decimal Anfangsbestand { get; set; }
        public bool RechnungExist { get; set; }
        public bool ExistStornoZurRG { get; set; }
        public decimal Auftraggeber { get; set; }
        public decimal StornoID { get; set; }
        public string AbrechnungsTarifName { get; set; }
        public decimal VersPraemie { get; set; }
        public DateTime RGBookPrintDate { get; set; }
        public string InfoText { get; set; }
        public string FibuInfo { get; set; }
        public string DocName { get; set; }
        public DateTime RGDatumVon { get; set; }
        public DateTime RGDatumBis { get; set; }

        public decimal IDtoRevision { get; set; }
        private decimal _RGNrMAX;
        public decimal RGNrMAX
        {
            get
            {
                _RGNrMAX = GetMAxRGNr();
                return _RGNrMAX;
            }
        }

        /**********************************************************************************
         *                                       Methoden
         * *******************************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mySystem"></param>
        /// <param name="myGLSys"></param>
        /// <param name="myGLUser"></param>
        /// <param name="iId"></param>
        public void InitCls(clsSystem mySystem, Globals._GL_SYSTEM myGLSys, Globals._GL_USER myGLUser, int iId)
        {
            this.sys = mySystem;
            this._GL_System = myGLSys;
            this._GL_User = myGLUser;
            this.ID = iId;
            MandantenViewData = new MandantenViewData();

            if (this.ID > 0)
            {
                this.Fill();
            }
        }
        ///<summary>clsRechnung / Copy</summary>
        ///<remarks></remarks>
        public clsRechnung Copy()
        {
            return (clsRechnung)this.MemberwiseClone();
        }
        ///<summary>clsRechnung / Add</summary>
        ///<remarks>Eintrag neuer Rechnungsdatensätze in die Datenbank</remarks>
        public void Add(bool bAddFibu = false)
        {
            //neue Rechnungsnummer ermitteln
            if (this.RGRevision != null)
            {
                if (this.RGRevision.RGNr > 0)
                {
                    this.RGNr = this.RGRevision.RGNr;
                }
                else
                {
                    GetRechnungsnummer(true, false);
                }
            }
            else
            {
                GetRechnungsnummer(true, false);
            }

            string strSql = string.Empty;
            if (
                (dtRGPositionen.Rows.Count > 0)
               )
            {
                Benutzer = this._GL_User.User_ID;
                //ArBereichID = this._GL_User.sys_ArbeitsbereichID;
                //ArBereichID = this._GL_System.sys_ArbeitsbereichID;
                bezahlt = Globals.DefaultDateTimeMaxValue;

                strSql = "DECLARE @RGTableID as decimal(28,0); " +
                         "DECLARE @RGPosTableID as decimal(28,0); ";

                //Eintrag Rechnung
                strSql = strSql + GetAddRechnungSQLString() +
                                    " Select @RGTableID = @@IDENTITY; ";

                for (Int32 i = 0; i <= dtRGPositionen.Rows.Count - 1; i++)
                {
                    //Eintrag der einezelnen Rechnungspositionen
                    bool bSumCalc = (bool)dtRGPositionen.Rows[i]["SumCalc"];
                    if (bSumCalc)
                    {
                        decimal decTmp = 0;
                        RGPos = new clsRGPositionen();
                        RGPos._GL_User = this._GL_User;
                        RGPos.Position = (Int32)dtRGPositionen.Rows[i]["Pos"];

                        RGPos.RGText = dtRGPositionen.Rows[i]["Text"].ToString();
                        RGPos.AbrEinheit = dtRGPositionen.Rows[i]["Einheit"].ToString();
                        decTmp = 0;
                        Decimal.TryParse(dtRGPositionen.Rows[i]["Menge"].ToString(), out decTmp);
                        RGPos.Menge = decTmp;
                        decTmp = 0;
                        Decimal.TryParse(dtRGPositionen.Rows[i]["€/Einheit"].ToString(), out decTmp);
                        RGPos.EinzelPreis = decTmp;
                        decTmp = 0;
                        Decimal.TryParse(dtRGPositionen.Rows[i]["Netto €"].ToString(), out decTmp);
                        RGPos.NettoPreis = decTmp;
                        RGPos.AbrechnungsArt = dtRGPositionen.Rows[i]["Abrechnungsart"].ToString();
                        decTmp = 0;
                        Decimal.TryParse(dtRGPositionen.Rows[i]["TarifPosID"].ToString(), out decTmp);
                        RGPos.TarifPosID = decTmp;

                        //--- das Feld Tariftext hat in der Tabelle RGPositionen eine Zeichenlänge von 50 deshalb muss hier gekürzt werden
                        string strTText = dtRGPositionen.Rows[i]["Tariftext"].ToString();
                        if (strTText.Length > 50)
                        {
                            strTText = strTText.Substring(0, 46);
                            strTText += "...";
                        }
                        RGPos.Tariftext = strTText;
                        //RGPos.Tariftext = dtRGPositionen.Rows[i]["Tariftext"].ToString();
                        decTmp = 0;
                        Decimal.TryParse(dtRGPositionen.Rows[i]["Marge €"].ToString(), out decTmp);
                        RGPos.MargeEuro = decTmp;
                        decTmp = 0;
                        Decimal.TryParse(dtRGPositionen.Rows[i]["Marge %"].ToString(), out decTmp);
                        RGPos.MargeProzent = decTmp;
                        decTmp = 0;
                        Decimal.TryParse(dtRGPositionen.Rows[i]["Anfangsbestand"].ToString(), out decTmp);
                        RGPos.Anfangsbestand = decTmp;
                        decTmp = 0;
                        Decimal.TryParse(dtRGPositionen.Rows[i]["Abgang"].ToString(), out decTmp);
                        RGPos.Abgang = decTmp;
                        decTmp = 0;
                        Decimal.TryParse(dtRGPositionen.Rows[i]["Zugang"].ToString(), out decTmp);
                        RGPos.Zugang = decTmp;
                        decTmp = 0;
                        Decimal.TryParse(dtRGPositionen.Rows[i]["Endbestand"].ToString(), out decTmp);
                        RGPos.Endbestand = decTmp;
                        RGPos.RGPosText = dtRGPositionen.Rows[i]["RGPosText"].ToString();
                        Int32 iTmp = 0;
                        if (bAddFibu == true)
                        {
                            Int32.TryParse(dtRGPositionen.Rows[i]["FibuKto"].ToString(), out iTmp);
                        }
                        RGPos.FibuKto = iTmp;
                        iTmp = 0;
                        if (dtRGPositionen.Columns.Contains("CalcModus"))
                        {
                            Int32.TryParse(dtRGPositionen.Rows[i]["CalcModus"].ToString(), out iTmp);
                        }
                        RGPos.CalcModus = EnumConverter.GetEnumObjectByValue<enumCalcultationModus>(iTmp);
                        iTmp = 0;
                        if (dtRGPositionen.Columns.Contains("CalcModValue"))
                        {
                            Int32.TryParse(dtRGPositionen.Rows[i]["CalcModValue"].ToString(), out iTmp);
                        }
                        RGPos.CalcModValue = iTmp;
                        decTmp = 0;
                        Decimal.TryParse(dtRGPositionen.Rows[i]["PricePerUnitFactor"].ToString(), out decTmp);
                        RGPos.PricePerUnitFactor = decTmp;

                        decTmp = 0;
                        Decimal.TryParse(dtRGPositionen.Rows[i]["TarifPricePerUnit"].ToString(), out decTmp);
                        RGPos.TarifPricePerUnit = decTmp;                       


                        //Änderung an der Insert Anweisung für RGPositionen auch in der Klasse 
                        //abändern
                        strSql = strSql + "INSERT INTO RGPositionen " +
                                                          "(RGTableID, Position,RGText, Abrechnungseinheit, Menge, EinzelPreis," +
                                                          "NettoPreis, Abrechnungsart, TarifPosID, Tariftext, MargeEuro, MargeProzent, " +
                                                          "Anfangsbestand, Abgang, Zugang, Endbestand, RGPosText, FibuKto, CalcModus, CalcModValue, " +
                                                          "PricePerUnitFactor, TarifPricePerUnit)" +
                                                          "VALUES(@RGTableID" +
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
                                                                    ",'" + RGPos.RGPosText + "'" +
                                                                    ", " + RGPos.FibuKto +
                                                                    ", " + (int)RGPos.CalcModus +
                                                                    ", " + RGPos.CalcModValue +
                                                                    ",'" + RGPos.PricePerUnitFactor.ToString().Replace(',', '.') + "'" +
                                                                    ",'" + RGPos.TarifPricePerUnit.ToString().Replace(',', '.') + "'" +
                                                                    "); " +
                                                                    
                                            " Select @RGPosTableID = @@IDENTITY; ";

                        //Eintrag der Artikel ID in die DB RGPosArtikel
                        switch (RGPos.AbrechnungsArt)
                        {
                            case "Einlagerungskosten":
                                if (dtArtikelEinlagerung.Columns["ID"] != null)
                                {
                                    dtArtikelEinlagerung.DefaultView.RowFilter = "TarifPosID =" + RGPos.TarifPosID.ToString();
                                    strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtArtikelEinlagerung.DefaultView.ToTable(), string.Empty);
                                }
                                break;

                            case "Lagerkosten":
                                if (dtArtikelLagerbestand.Columns["ID"] != null)
                                {
                                    dtArtikelLagerbestand.DefaultView.RowFilter = "TarifPosID =" + RGPos.TarifPosID.ToString();
                                    strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtArtikelLagerbestand.DefaultView.ToTable(), string.Empty);
                                }
                                break;

                            case "Auslagerungskosten":
                                if (dtArtikelAuslagerung.Columns["ID"] != null)
                                {
                                    dtArtikelAuslagerung.DefaultView.RowFilter = "TarifPosID =" + RGPos.TarifPosID.ToString();
                                    strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtArtikelAuslagerung.DefaultView.ToTable(), string.Empty);
                                }
                                break;

                            case "LagerTransportkosten":
                                if (dtArtikelLagerTransporte.Columns["TransDirection"] != null)
                                {
                                    dtArtikelLagerTransporte.DefaultView.RowFilter = "TransDirection = 'IN'";
                                    DataTable dtIN = dtArtikelLagerTransporte.DefaultView.ToTable();
                                    dtArtikelLagerTransporte.DefaultView.RowFilter = "TransDirection = 'OUT'";
                                    DataTable dtOUT = dtArtikelLagerTransporte.DefaultView.ToTable();

                                    //IN
                                    if (dtIN.Rows.Count > 0)
                                    {
                                        dtIN.DefaultView.RowFilter = "TarifPosID =" + RGPos.TarifPosID.ToString();
                                        strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtIN.DefaultView.ToTable(), "IN");
                                    }
                                    //OUT
                                    if (dtOUT.Rows.Count > 0)
                                    {
                                        dtOUT.DefaultView.RowFilter = "TarifPosID =" + RGPos.TarifPosID.ToString();
                                        strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtOUT.DefaultView.ToTable(), "OUT");
                                    }
                                }
                                break;

                            case "Sperrlagerkosten":
                                if (dtArtikelSperrlager.Columns["TarifPosID"] != null)
                                {
                                    dtArtikelSperrlager.DefaultView.RowFilter = "TarifPosID =" + RGPos.TarifPosID.ToString();
                                    strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtArtikelSperrlager.DefaultView.ToTable(), string.Empty);
                                }
                                break;

                            case "Direktanlieferung":
                                if (dtArtikelDirektanlieferung.Columns["TarifPosID"] != null)
                                {
                                    dtArtikelDirektanlieferung.DefaultView.RowFilter = "TarifPosID =" + RGPos.TarifPosID.ToString();
                                    strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtArtikelDirektanlieferung.DefaultView.ToTable(), string.Empty);
                                }
                                break;

                            case "Rücklieferung":
                                if (dtArtikelRuecklieferung.Columns["TarifPosID"] != null)
                                {
                                    dtArtikelRuecklieferung.DefaultView.RowFilter = "TarifPosID =" + RGPos.TarifPosID.ToString();
                                    strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtArtikelRuecklieferung.DefaultView.ToTable(), string.Empty);
                                }
                                break;

                            case "Vorfracht":
                                if (dtArtikelVorfracht.Columns["TarifPosID"] != null)
                                {
                                    dtArtikelVorfracht.DefaultView.RowFilter = "TarifPosID =" + RGPos.TarifPosID.ToString();
                                    strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtArtikelVorfracht.DefaultView.ToTable(), string.Empty);
                                }
                                break;

                            case "Nebenkosten":
                                if (dtArtikelNebenkosten.Columns["TarifPosID"] != null)
                                {
                                    dtArtikelNebenkosten.DefaultView.RowFilter = "TarifPosID =" + RGPos.TarifPosID.ToString();
                                    strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtArtikelNebenkosten.DefaultView.ToTable(), string.Empty);
                                }
                                break;
                            case clsFaktLager.const_Abrechnungsart_Gleisstellgebuehr:
                                if (dtArtikelGleis.Columns["TarifPosID"] != null)
                                {
                                    dtArtikelGleis.DefaultView.RowFilter = "TarifPosID =" + RGPos.TarifPosID.ToString();
                                    strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtArtikelGleis.DefaultView.ToTable(), string.Empty);
                                }
                                break;
                            case clsFaktLager.const_Abrechnungsart_Maut:
                                if (dtArtikelToll.Columns["TarifPosID"] != null)
                                {
                                    dtArtikelToll.DefaultView.RowFilter = "TarifPosID =" + RGPos.TarifPosID.ToString();
                                    strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtArtikelToll.DefaultView.ToTable(), string.Empty);
                                }
                                break;
                        }

                        strSql = strSql + " Select @RGTableID as RGTableID; ";
                    }
                }
            }

            if (strSql != string.Empty)
            {
                //bool bSqlOK=clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "FakturierungLager", Benutzer);
                string strValue = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSql, "FakturierungLager", Benutzer);
                decimal decTmp = 0;
                Decimal.TryParse(strValue, out decTmp);
                ID = decTmp;
                RechnungExist = ExistRechnung(this._GL_User, ID);
                if (RechnungExist)
                {

                }
                else
                {
                    //Errordatei schreiben
                    Error = new clsError();
                    Error.BenutzerName = this._GL_User.Name;
                    Error.SQLString = strSql;
                    Error.Aktion = "Fakturierung - Lagerrechnung erstellen";
                    Error.WriteError();
                }
            }
        }
        ///<summary>clsRechnung / UpdateSQL</summary>
        ///<remarks></remarks>
        private string UpdateSQL()
        {
            string strSql = string.Empty;
            strSql = "Update Rechnungen SET " +
                                "RGNr =" + this.RGNr +
                                ", Datum='" + this.Datum.ToString() + "'" +
                                ", faellig='" + this.faellig.ToString() + "'" +
                                ", MwStSatz='" + MwStSatz.ToString().Replace(',', '.') + "'" +
                                ", MwStBetrag='" + MwStBetrag.ToString().Replace(',', '.') + "'" +
                                ", NettoBetrag='" + NettoBetrag.ToString().Replace(',', '.') + "'" +
                                ", BruttoBetrag='" + BruttoBetrag.ToString().Replace(',', '.') + "'" +
                                ", GS=" + Convert.ToInt32(GS) +
                                ", bezahlt='" + bezahlt.ToString() + "'" +
                                ", Druck=" + Convert.ToInt32(Druck) +
                                ", Druckdatum='" + Druckdatum.ToString() + "'" +
                                ", Benutzer=" + Benutzer +
                                ", RGArt='" + RGArt + "'" +
                                ", MandantenID=" + MandantenID +
                                ", ArBereichID=" + ArBereichID +
                                ", exFibu=" + Convert.ToInt32(exFibu) +
                                ", AbrZeitraumVon='" + AbrZeitraumVon.ToString() + "'" +
                                ", AbrZeitraumBis='" + AbrZeitraumBis + "'" +
                                ", Empfaenger=" + Empfaenger +
                                ", Storno=" + Convert.ToInt32(Storno) +
                                ", Anfangsbestand='" + Anfangsbestand.ToString().Replace(',', '.') + "'" +
                                ", Auftraggeber=" + Auftraggeber +
                                ", StornoID=" + StornoID +
                                ", AbrTarifName='" + AbrechnungsTarifName + "' " +
                                ", VersPraemie='" + VersPraemie.ToString().Replace(',', '.') + "'" +
                                ", RGBookPrintDate='" + RGBookPrintDate + "'" +
                                ", InfoText='" + this.InfoText + "'" +
                                ", FibuInfo='" + this.FibuInfo + "'" +
                                ", DocName='" + this.DocName + "'" +
                                   "WHERE ID=" + ID + " ;  ";
            return strSql;
        }
        ///<summary>clsRechnung / GetMAxRGNr</summary>
        ///<remarks></remarks>
        private decimal GetMAxRGNr()
        {
            decimal decTmp = 0;
            string strSql = string.Empty;
            strSql = "Select MAX(RGNR) FROM Rechnungen " +
                                            "WHERE " +
                                                "MandantenID=" + this.sys.AbBereich.MandantenID +
                                                " AND ArBereichID=" + this.sys.AbBereich.ID +
                                                " ;";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, this.Benutzer);
            Decimal.TryParse(strTmp, out decTmp);
            return decTmp;
        }
        ///<summary>clsRechnung / Update</summary>
        ///<remarks></remarks>
        public void Update(bool bRGKopfOnley)
        {
            //Korrektur RGKopf
            string strSql = string.Empty;
            strSql = UpdateSQL();
            if (!bRGKopfOnley)
            {
                for (Int32 i = 0; i <= dtRGPositionen.Rows.Count - 1; i++)
                {
                    decimal decTmp = 0;
                    RGPos = new clsRGPositionen();
                    RGPos._GL_User = this._GL_User;
                    decTmp = 0;
                    Decimal.TryParse(dtRGPositionen.Rows[i]["ID"].ToString(), out decTmp);
                    RGPos.ID = decTmp;
                    RGPos.Position = (Int32)dtRGPositionen.Rows[i]["Pos"];
                    RGPos.RGText = dtRGPositionen.Rows[i]["Text"].ToString();
                    RGPos.AbrEinheit = dtRGPositionen.Rows[i]["Einheit"].ToString();
                    decTmp = 0;
                    Decimal.TryParse(dtRGPositionen.Rows[i]["Menge"].ToString(), out decTmp);
                    RGPos.Menge = decTmp;
                    decTmp = 0;
                    Decimal.TryParse(dtRGPositionen.Rows[i]["€/Einheit"].ToString(), out decTmp);
                    RGPos.EinzelPreis = decTmp;
                    decTmp = 0;
                    Decimal.TryParse(dtRGPositionen.Rows[i]["Netto €"].ToString(), out decTmp);
                    RGPos.NettoPreis = decTmp;
                    RGPos.AbrechnungsArt = dtRGPositionen.Rows[i]["Abrechnungsart"].ToString();
                    decTmp = 0;
                    Decimal.TryParse(dtRGPositionen.Rows[i]["TarifPosID"].ToString(), out decTmp);
                    RGPos.TarifPosID = decTmp;
                    RGPos.Tariftext = dtRGPositionen.Rows[i]["Tariftext"].ToString();
                    decTmp = 0;
                    Decimal.TryParse(dtRGPositionen.Rows[i]["Marge €"].ToString(), out decTmp);
                    RGPos.MargeEuro = decTmp;
                    decTmp = 0;
                    Decimal.TryParse(dtRGPositionen.Rows[i]["Marge %"].ToString(), out decTmp);
                    RGPos.MargeProzent = decTmp;
                    decTmp = 0;
                    Decimal.TryParse(dtRGPositionen.Rows[i]["Anfangsbestand"].ToString(), out decTmp);
                    RGPos.Anfangsbestand = decTmp;
                    decTmp = 0;
                    Decimal.TryParse(dtRGPositionen.Rows[i]["Abgang"].ToString(), out decTmp);
                    RGPos.Abgang = decTmp;
                    decTmp = 0;
                    Decimal.TryParse(dtRGPositionen.Rows[i]["Zugang"].ToString(), out decTmp);
                    RGPos.Zugang = decTmp;
                    decTmp = 0;
                    Decimal.TryParse(dtRGPositionen.Rows[i]["Endbestand"].ToString(), out decTmp);
                    RGPos.Endbestand = decTmp;
                    RGPos.RGPosText = dtRGPositionen.Rows[i]["RGPosText"].ToString();
                    Int32 intTmp = 0;
                    Int32.TryParse(dtRGPositionen.Rows[i]["FibuKto"].ToString(), out intTmp);
                    RGPos.FibuKto = intTmp;
                    decTmp = 0;
                    Decimal.TryParse(dtRGPositionen.Rows[i]["PricePerUnitFactor"].ToString(), out decTmp);
                    RGPos.PricePerUnitFactor = decTmp;
                    decTmp = 0;
                    Decimal.TryParse(dtRGPositionen.Rows[i]["TarifPricePerUnit"].ToString(), out decTmp);
                    RGPos.TarifPricePerUnit = decTmp;                    

                    if (RGPos.ID > 0)
                    {
                        //Änderung an der Insert Anweisung für RGPositionen auch in der Klasse 
                        //abändern
                        strSql = strSql + "Update RGPositionen SET" +
                                                        " Position=" + RGPos.Position +
                                                        ", RGText='" + RGPos.RGText + "'" +
                                                        ", Abrechnungseinheit ='" + RGPos.AbrEinheit + "'" +
                                                        ", Menge='" + RGPos.Menge.ToString().Replace(',', '.') + "'" +
                                                        ", EinzelPreis='" + RGPos.EinzelPreis.ToString().Replace(',', '.') + "'" +
                                                        ", NettoPreis='" + RGPos.NettoPreis.ToString().Replace(',', '.') + "'" +
                                                        ", Abrechnungsart='" + RGPos.AbrechnungsArt + "'" +
                                                        ", TarifPosID=" + RGPos.TarifPosID +
                                                        ", Tariftext='" + RGPos.Tariftext + "'" +
                                                        ", MargeEuro='" + RGPos.MargeEuro.ToString().Replace(',', '.') + "'" +
                                                        ", MargeProzent='" + RGPos.MargeProzent.ToString().Replace(',', '.') + "'" +
                                                        ", Anfangsbestand='" + RGPos.Anfangsbestand.ToString().Replace(',', '.') + "'" +
                                                        ", Abgang='" + RGPos.Abgang.ToString().Replace(',', '.') + "'" +
                                                        ", Zugang='" + RGPos.Zugang.ToString().Replace(',', '.') + "'" +
                                                        ", Endbestand='" + RGPos.Endbestand.ToString().Replace(',', '.') + "'" +
                                                        ", RGPosText='" + RGPos.RGPosText + "'" +
                                                        ", PricePerUnitFactor='" + RGPos.PricePerUnitFactor.ToString().Replace(',', '.') + "'" +
                                                        ", TarifPricePerUnit='" + RGPos.TarifPricePerUnit.ToString().Replace(',', '.') + "'" +
                                                        
                                                        " WHERE ID=" + RGPos.ID + " ;  ";
                    }
                    else
                    {
                        strSql = strSql + "INSERT INTO RGPositionen " +
                                          "(RGTableID, Position,RGText, Abrechnungseinheit, Menge, EinzelPreis," +
                                          "NettoPreis, Abrechnungsart, TarifPosID, Tariftext, MargeEuro, MargeProzent, " +
                                          "Anfangsbestand, Abgang, Zugang, Endbestand, RGPosText, PricePerUnitFactor, TarifPricePerUnit)" +
                                          "VALUES(" + this.ID +
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
                                                    ",'" + RGPos.RGPosText + "'" +
                                                    ",'" + RGPos.PricePerUnitFactor.ToString().Replace(',', '.') + "'" +
                                                    ",'" + RGPos.TarifPricePerUnit.ToString().Replace(',', '.') + "'" +
                                                    "); ";
                    }
                }
            }

            if (clsSQLcon.ExecuteSQL(strSql, BenutzerID))
            {
                //Add Logbucheintrag Korrektur
                string Txt = "ID:[" + this.ID.ToString() + "]/ RG-/GS-Nr:[" + this.RGNr.ToString() + "] / Arbeitsbereicht:[" + this.ArBereichID.ToString() + "]";
                string Beschreibung = "Rechnung korrigiert: " + Txt + "  ";
                Functions.AddLogbuch(this.Benutzer, enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
            }
        }
        ///<summary>clsRechnung / AddAllTarifInOne</summary>
        ///<remarks></remarks>
        public void AddAllTarifInOne()
        {
            //neue Rechnungsnummer ermitteln
            GetRechnungsnummer();

            string strSql = string.Empty;
            if (
                (dtRGPositionen.Rows.Count > 0)
               )
            {
                Benutzer = this._GL_User.User_ID;
                ArBereichID = this._GL_System.sys_ArbeitsbereichID;
                bezahlt = Globals.DefaultDateTimeMaxValue;

                strSql = "DECLARE @RGTableID as decimal(28,0); " +
                         "DECLARE @RGPosTableID as decimal(28,0); ";

                //Eintrag Rechnung
                strSql = strSql + GetAddRechnungSQLString() +
                                    " Select @RGTableID = @@IDENTITY; ";

                for (Int32 i = 0; i <= dtRGPositionen.Rows.Count - 1; i++)
                {
                    //Eintrag der einezelnen Rechnungspositionen
                    bool bSumCalc = (bool)dtRGPositionen.Rows[i]["SumCalc"];
                    if (bSumCalc)
                    {
                        decimal decTmp = 0;
                        RGPos = new clsRGPositionen();
                        RGPos._GL_User = this._GL_User;
                        RGPos.Position = (Int32)dtRGPositionen.Rows[i]["Pos"];

                        RGPos.RGText = dtRGPositionen.Rows[i]["Text"].ToString();
                        RGPos.AbrEinheit = dtRGPositionen.Rows[i]["Einheit"].ToString();
                        decTmp = 0;
                        Decimal.TryParse(dtRGPositionen.Rows[i]["Menge"].ToString(), out decTmp);
                        RGPos.Menge = decTmp;
                        decTmp = 0;
                        Decimal.TryParse(dtRGPositionen.Rows[i]["€/Einheit"].ToString(), out decTmp);
                        RGPos.EinzelPreis = decTmp;
                        decTmp = 0;
                        Decimal.TryParse(dtRGPositionen.Rows[i]["Netto €"].ToString(), out decTmp);
                        RGPos.NettoPreis = decTmp;
                        RGPos.AbrechnungsArt = dtRGPositionen.Rows[i]["Abrechnungsart"].ToString();
                        decTmp = 0;
                        Decimal.TryParse(dtRGPositionen.Rows[i]["TarifPosID"].ToString(), out decTmp);
                        RGPos.TarifPosID = decTmp;
                        RGPos.Tariftext = dtRGPositionen.Rows[i]["Tariftext"].ToString();
                        decTmp = 0;
                        Decimal.TryParse(dtRGPositionen.Rows[i]["Marge €"].ToString(), out decTmp);
                        RGPos.MargeEuro = decTmp;
                        decTmp = 0;
                        Decimal.TryParse(dtRGPositionen.Rows[i]["Marge %"].ToString(), out decTmp);
                        RGPos.MargeProzent = decTmp;
                        decTmp = 0;
                        Decimal.TryParse(dtRGPositionen.Rows[i]["Anfangsbestand"].ToString(), out decTmp);
                        RGPos.Anfangsbestand = decTmp;
                        decTmp = 0;
                        Decimal.TryParse(dtRGPositionen.Rows[i]["Abgang"].ToString(), out decTmp);
                        RGPos.Abgang = decTmp;
                        decTmp = 0;
                        Decimal.TryParse(dtRGPositionen.Rows[i]["Zugang"].ToString(), out decTmp);
                        RGPos.Zugang = decTmp;
                        decTmp = 0;
                        Decimal.TryParse(dtRGPositionen.Rows[i]["Endbestand"].ToString(), out decTmp);
                        RGPos.Endbestand = decTmp;
                        RGPos.RGPosText = dtRGPositionen.Rows[i]["RGPosText"].ToString();
                        Int32 iTmp = 0;
                        Int32.TryParse(dtRGPositionen.Rows[i]["FibuKto"].ToString(), out iTmp);
                        RGPos.FibuKto = iTmp;
                        decTmp = 0;
                        Decimal.TryParse(dtRGPositionen.Rows[i]["PricePerUnitFactor"].ToString(), out decTmp);
                        RGPos.PricePerUnitFactor = decTmp;
                        decTmp = 0;
                        Decimal.TryParse(dtRGPositionen.Rows[i]["TarifPricePerUnit"].ToString(), out decTmp);
                        RGPos.TarifPricePerUnit = decTmp;

                        //Änderung an der Insert Anweisung für RGPositionen auch in der Klasse 
                        //abändern
                        strSql = strSql + "INSERT INTO RGPositionen " +
                                                          "(RGTableID, Position,RGText, Abrechnungseinheit, Menge, EinzelPreis," +
                                                          "NettoPreis, Abrechnungsart, TarifPosID, Tariftext, MargeEuro, MargeProzent, " +
                                                          "Anfangsbestand, Abgang, Zugang, Endbestand, RGPosText, FibuKto)" +
                                                          "VALUES(@RGTableID" +
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
                                                                    ",'" + RGPos.RGPosText + "'" +
                                                                    ", " + RGPos.FibuKto +
                                                                    "); " +
                                            " Select @RGPosTableID = @@IDENTITY; ";

                        //Eintrag der Artikel ID in die DB RGPosArtikel
                        switch (RGPos.AbrechnungsArt)
                        {
                            //case "Einlagerungskosten":
                            case clsFaktLager.const_Abrechnungsart_Einlagerung:
                                if (dtArtikelEinlagerung.Columns["ID"] != null)
                                {
                                    //dtArtikelEinlagerung.DefaultView.RowFilter = "TarifPosID =" + RGPos.TarifPosID.ToString();
                                    // strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtArtikelEinlagerung.DefaultView.ToTable(true, "ID"), string.Empty);
                                    strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtArtikelEinlagerung.DefaultView.ToTable(), string.Empty);
                                }
                                break;

                            //case "Lagerkosten":
                            case clsFaktLager.const_Abrechnungsart_Lagerkosten:
                                if (dtArtikelLagerbestand.Columns["ID"] != null)
                                {
                                    //dtArtikelLagerbestand.DefaultView.RowFilter = "TarifPosID =" + RGPos.TarifPosID.ToString();
                                    //strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtArtikelLagerbestand.DefaultView.ToTable(true, "ID"), string.Empty);
                                    strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtArtikelLagerbestand.DefaultView.ToTable(), string.Empty);
                                }
                                break;

                            //case "Auslagerungskosten":
                            case clsFaktLager.const_Abrechnungsart_Auslagerung:
                                if (dtArtikelAuslagerung.Columns["ID"] != null)
                                {
                                    //dtArtikelAuslagerung.DefaultView.RowFilter = "TarifPosID =" + RGPos.TarifPosID.ToString();
                                    //strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtArtikelAuslagerung.DefaultView.ToTable(true, "ID"), string.Empty);
                                    strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtArtikelAuslagerung.DefaultView.ToTable(), string.Empty);
                                }
                                break;

                            //case "LagerTransportkosten":
                            case clsFaktLager.const_Abrechnungsart_LagerTransportkosten:
                                if (dtArtikelLagerTransporte.Columns["TransDirection"] != null)
                                {
                                    dtArtikelLagerTransporte.DefaultView.RowFilter = "TransDirection = 'IN'";
                                    DataTable dtIN = dtArtikelLagerTransporte.DefaultView.ToTable();
                                    dtArtikelLagerTransporte.DefaultView.RowFilter = "TransDirection = 'OUT'";
                                    DataTable dtOUT = dtArtikelLagerTransporte.DefaultView.ToTable();

                                    //IN
                                    if (dtIN.Rows.Count > 0)
                                    {
                                        dtIN.DefaultView.RowFilter = "TarifPosID =" + RGPos.TarifPosID.ToString();
                                        //strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtIN.DefaultView.ToTable(true, "ID"), "IN");
                                        strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtIN.DefaultView.ToTable(), "IN");
                                    }
                                    //OUT
                                    if (dtOUT.Rows.Count > 0)
                                    {
                                        dtOUT.DefaultView.RowFilter = "TarifPosID =" + RGPos.TarifPosID.ToString();
                                        //strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtOUT.DefaultView.ToTable(true, "ID"), "OUT");
                                        strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtOUT.DefaultView.ToTable(), "OUT");
                                    }
                                }
                                break;

                            //case "Sperrlagerkosten":
                            case clsFaktLager.const_Abrechnungsart_SPL:
                                if (dtArtikelSperrlager.Columns["TarifPosID"] != null)
                                {
                                    dtArtikelSperrlager.DefaultView.RowFilter = "TarifPosID =" + RGPos.TarifPosID.ToString();
                                    //strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtArtikelSperrlager.DefaultView.ToTable(true, "ID"), string.Empty);
                                    strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtArtikelSperrlager.DefaultView.ToTable(), string.Empty);
                                }
                                break;

                            //case "Direktanlieferung":
                            case clsFaktLager.const_Abrechnungsart_Direktanlieferung:
                                if (dtArtikelDirektanlieferung.Columns["TarifPosID"] != null)
                                {
                                    dtArtikelDirektanlieferung.DefaultView.RowFilter = "TarifPosID =" + RGPos.TarifPosID.ToString();
                                    //strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtArtikelDirektanlieferung.DefaultView.ToTable(true, "ID"), string.Empty);
                                    strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtArtikelDirektanlieferung.DefaultView.ToTable(), string.Empty);
                                }
                                break;

                            //case "Rücklieferung":
                            case clsFaktLager.const_Abrechnungsart_Ruecklieferung:
                                if (dtArtikelRuecklieferung.Columns["TarifPosID"] != null)
                                {
                                    dtArtikelRuecklieferung.DefaultView.RowFilter = "TarifPosID =" + RGPos.TarifPosID.ToString();
                                    strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtArtikelRuecklieferung.DefaultView.ToTable(), string.Empty);
                                }
                                break;

                            //case "Vorfracht":
                            case clsFaktLager.const_Abrechnungsart_Vorfracht:
                                if (dtArtikelVorfracht.Columns["TarifPosID"] != null)
                                {
                                    dtArtikelVorfracht.DefaultView.RowFilter = "TarifPosID =" + RGPos.TarifPosID.ToString();
                                    //strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtArtikelVorfracht.DefaultView.ToTable(true, "ID"), string.Empty);
                                    strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtArtikelVorfracht.DefaultView.ToTable(), string.Empty);
                                }
                                break;

                            //case "Nebenkosten":
                            case clsFaktLager.const_Abrechnungsart_Nebenkosten:
                                if (dtArtikelNebenkosten.Columns["TarifPosID"] != null)
                                {
                                    strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtArtikelNebenkosten.DefaultView.ToTable(), string.Empty);
                                }
                                break;

                            //case "Gleisstellgebühr":
                            case clsFaktLager.const_Abrechnungsart_Gleisstellgebuehr:
                                //Bei der Gleisstellgebühr werden keine Artikel gespeichert
                                if (dtArtikelGleis.Columns["TarifPosID"] != null)
                                {
                                    strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtArtikelGleis.DefaultView.ToTable(), string.Empty);
                                }
                                break;

                            case clsFaktLager.const_Abrechnungsart_Maut:
                                if (dtArtikelToll.Columns["TarifPosID"] != null)
                                {
                                    dtArtikelToll.DefaultView.RowFilter = "TarifPosID =" + RGPos.TarifPosID.ToString();
                                    strSql = strSql + GetSQLInsertRGPosArtikel(ref RGPosArtikel, dtArtikelToll.DefaultView.ToTable(), string.Empty);
                                }
                                break;

                        }

                        strSql = strSql + " Select @RGTableID as RGTableID; ";
                    }
                }
            }


            if (strSql != string.Empty)
            {
                //bool bSqlOK=clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "FakturierungLager", Benutzer);
                string strValue = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSql, "FakturierungLager", Benutzer);
                decimal decTmp = 0;
                Decimal.TryParse(strValue, out decTmp);
                ID = decTmp;
                RechnungExist = ExistRechnung(this._GL_User, ID);
                if (RechnungExist)
                {

                }
                else
                {
                    //Errordatei schreiben
                    Error = new clsError();
                    Error.BenutzerName = this._GL_User.Name;
                    Error.SQLString = strSql;
                    Error.Aktion = "Fakturierung - Lagerrechnung erstellen";
                    Error.WriteError();
                }
            }
        }
        ///<summary>clsRechnung / GetSQLInsertRGPosArtikel</summary>
        ///<remarks></remarks>
        private string GetSQLInsertRGPosArtikel(ref clsRGPosArtikel myRGPosArtikel, DataTable dtArtID, string myTransDirection)
        {
            string strSQL = string.Empty;
            decimal decTmp = 0;
            if (dtArtID.Rows.Count > 0)
            {
                for (Int32 j = 0; j <= dtArtID.Rows.Count - 1; j++)
                {
                    decimal decArtID = (decimal)dtArtID.Rows[j]["ID"];
                    myRGPosArtikel = new clsRGPosArtikel();
                    myRGPosArtikel.ArtikelID = decArtID;
                    myRGPosArtikel.AbgerechnetVon = this.AbrZeitraumVon;
                    myRGPosArtikel.AbgerechnetBis = this.AbrZeitraumBis;
                    decTmp = 0;
                    if (dtArtID.Columns.Contains("Menge"))
                    {
                        Decimal.TryParse(dtArtID.Rows[j]["Menge"].ToString(), out decTmp);
                    }
                    myRGPosArtikel.Menge = decTmp;
                    decTmp = 0;
                    if (dtArtID.Columns.Contains("Preis"))
                    {
                        Decimal.TryParse(dtArtID.Rows[j]["Preis"].ToString(), out decTmp);
                    }
                    myRGPosArtikel.Preis = decTmp;
                    Int32 iTmp = 0;
                    if (dtArtID.Columns.Contains("Dauer"))
                    {
                        Int32.TryParse(dtArtID.Rows[j]["Dauer"].ToString(), out iTmp);
                    }
                    myRGPosArtikel.Dauer = iTmp;
                    decTmp = 0;
                    if (dtArtID.Columns.Contains("Kosten"))
                    {
                        Decimal.TryParse(dtArtID.Rows[j]["Kosten"].ToString(), out decTmp);
                    }
                    myRGPosArtikel.Kosten = decTmp;
                    decTmp = 0;
                    if (dtArtID.Columns.Contains("TarifPosID"))
                    {
                        Decimal.TryParse(dtArtID.Rows[j]["TarifPosID"].ToString(), out decTmp);
                    }
                    myRGPosArtikel.TarifPosID = decTmp;
                    bool bTmp = false;
                    if (dtArtID.Columns.Contains("IsUBCalc"))
                    {
                        Boolean.TryParse(dtArtID.Rows[j]["IsUBCalc"].ToString(), out bTmp);
                    }
                    myRGPosArtikel.IsUBCalc = bTmp;
                    myRGPosArtikel.TransDirection = myTransDirection;
                    if (dtArtID.Columns.Contains("ArtRGTxt"))
                    {
                        myRGPosArtikel.ArtRGTxt = dtArtID.Rows[j]["ArtRGTxt"].ToString();
                    }
                    else
                    {
                        myRGPosArtikel.ArtRGTxt = string.Empty;
                    }
                    DateTime DateTmp = Globals.DefaultDateTimeMinValue;
                    if (dtArtID.Columns.Contains("LstDatum"))
                    {
                        DateTime.TryParse(dtArtID.Rows[j]["LstDatum"].ToString(), out DateTmp);
                        myRGPosArtikel.LstDatum = DateTmp;
                    }
                    else
                    {
                        myRGPosArtikel.LstDatum = DateTmp;
                    }
                    if (dtArtID.Columns.Contains("LstEinheit"))
                    {
                        myRGPosArtikel.LstEinheit = dtArtID.Rows[j]["LstEinheit"].ToString();
                    }
                    else
                    {
                        myRGPosArtikel.LstEinheit = string.Empty;
                    }
                    strSQL = strSQL + "INSERT INTO RGPosArtikel " +
                                                        "(RGPosID, ArtikelID, AbgerechnetVon, AbgerechnetBis, Storno, TransDirection, " +
                                                         "Menge, Preis, Dauer, Kosten, TarifPosID, IsUBCalc, ArtRGTxt, LstDatum, LstEinheit" +
                                                        ")" +
                                                        "VALUES(@RGPosTableID" +
                                                                "," + myRGPosArtikel.ArtikelID +
                                                                ",'" + myRGPosArtikel.AbgerechnetVon + "'" +
                                                                ",'" + myRGPosArtikel.AbgerechnetBis + "'" +
                                                                ", 0 " +
                                                                ",'" + myRGPosArtikel.TransDirection + "'" +
                                                                ", '" + myRGPosArtikel.Menge.ToString().Replace(",", ".") + "'" +
                                                                ", '" + myRGPosArtikel.Preis.ToString().Replace(",", ".") + "'" +
                                                                ", " + myRGPosArtikel.Dauer +
                                                                ", '" + myRGPosArtikel.Kosten.ToString().Replace(",", ".") + "'" +
                                                                ", " + myRGPosArtikel.TarifPosID +
                                                                ", " + Convert.ToInt32(myRGPosArtikel.IsUBCalc) +
                                                                ", '" + myRGPosArtikel.ArtRGTxt + "'" +
                                                                ", '" + myRGPosArtikel.LstDatum + "'" +
                                                                ", '" + myRGPosArtikel.LstEinheit + "'" +

                                                                "); ";
                }
            }
            return strSQL;
        }
        ///<summary>clsRechnung / ExistRGId</summary>
        ///<remarks>Check, ob die ID existiert.</remarks>
        public bool ExistRGId()
        {
            bool bReturn = false;
            if (ID > 0)
            {
                string strSql = string.Empty;
                strSql = "Select * FROM Rechnungen WHERE ID=" + ID;
                bReturn = clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            }
            return bReturn;
        }
        ///<summary>clsRechnung / GetAddRechnungSQLString</summary>
        ///<remarks>SQL String für den Datenbank Insert in Rechnungen.</remarks>
        private string GetAddRechnungSQLString()
        {
            RGBookPrintDate = clsSystem.const_DefaultDateTimeValue_Min;
            string stSQL = string.Empty;
            stSQL = "INSERT INTO Rechnungen " +
                                        "(RGNr, Datum, faellig, MwStSatz, MwStBetrag, NettoBetrag, BruttoBetrag, " +
                                        "GS, Bezahlt, Druck, Druckdatum, Benutzer, RGArt, MandantenID, ArBereichID, exFibu, " +
                                        "AbrZeitraumVon, AbrZeitraumBis, Empfaenger, Storno, Anfangsbestand, Auftraggeber, " +
                                        "StornoID, AbrTarifName, VersPraemie, RGBookPrintDate, InfoText, FibuInfo, DocName)" +
                                        "VALUES(" + RGNr +
                                                ",'" + Datum + "'" +
                                                ",'" + faellig + "'" +
                                                ",'" + MwStSatz.ToString().Replace(',', '.') + "'" +
                                                ",'" + MwStBetrag.ToString().Replace(',', '.') + "'" +
                                                ",'" + NettoBetrag.ToString().Replace(',', '.') + "'" +
                                                ",'" + BruttoBetrag.ToString().Replace(',', '.') + "'" +
                                                "," + Convert.ToInt32(GS) +
                                                ",'" + bezahlt + "'" +
                                                "," + Convert.ToInt32(Druck) +
                                                ",'" + Druckdatum + "'" +
                                                "," + Benutzer +
                                                ",'" + RGArt + "'" +
                                                "," + MandantenID +
                                                "," + ArBereichID +
                                                "," + Convert.ToInt32(exFibu) +
                                                ",'" + AbrZeitraumVon + "'" +
                                                ",'" + AbrZeitraumBis + "'" +
                                                "," + Empfaenger +
                                                "," + Convert.ToInt32(Storno) +
                                                ",'" + Anfangsbestand.ToString().Replace(',', '.') + "'" +
                                                "," + Auftraggeber +
                                                "," + StornoID +
                                                ",'" + AbrechnungsTarifName + "' " +
                                                 ",'" + VersPraemie.ToString().Replace(',', '.') + "'" +
                                                 ",'" + RGBookPrintDate + "'" +
                                                 ", '" + this.InfoText + "'" +
                                                 ", '" + this.FibuInfo + "'" +
                                                 ", '" + this.DocName + "'" +
                                                "); ";
            return stSQL;
        }
        ///<summary>clsRechnung / GetRechnungsnummer</summary>
        ///<remarks>Ermittelt eine neue Rechnungsnummer.</remarks>
        public void GetRechnungsnummer(bool bNewRGNr = true, bool bMonthly = false)
        {
            if (bMonthly)
            {
                clsRGNr rgnr = new clsRGNr();
                rgnr.GL_User = this._GL_User;
                rgnr.getNewRGNr(AbrZeitraumVon);
                this.RGNr = rgnr.RGNr;

            }
            else
            {
                clsPrimeKeys pk = new clsPrimeKeys();
                pk.sys = this.sys;
                pk._GL_User = this._GL_User;
                pk.Mandanten_ID = this.MandantenID;
                pk.AbBereichID = this.sys.AbBereich.ID;

                if (this.GS)
                {
                    if (bNewRGNr == true)
                    {
                        pk.GetNEWGSNr();
                    }
                    else
                    {
                        pk.GetNEWGSNrWOUpdate();
                    }
                    this.RGNr = pk.GSNr;
                }
                else
                {
                    if (bNewRGNr == true)
                    {
                        pk.GetNEWRGNr();
                    }
                    else
                    {
                        pk.GetNEWRGNrWOUpdate();
                    }
                    this.RGNr = pk.RGNr;
                }
            }
        }
        ///<summary>clsRechnung / GetPrviousLEingangsID</summary>
        ///<remarks>Ermittel die vorhergehende LEingangID für den Mandanten und Arbeitsbereich.</remarks>
        public void GetNextLManRG(bool SearchDirection)
        {
            string strSql = string.Empty;
            strSql = "Select Top(1) ID FROM Rechnungen WHERE MandantenID=" + this.MandantenID +
                                                             " AND ArBereichID=" + this.sys.AbBereich.ID +
                                                             " AND RGArt='" + clsRechnung.const_RechnungsArt_Manuell + "'" +
                                                             " AND Druck=0 ";
            if (this.RGNr > 0)
            {
                if (SearchDirection)
                {
                    //forward
                    strSql = strSql + "AND RGNr>" + this.RGNr + " ORDER BY RGNr; ";
                }
                else
                {
                    //back
                    strSql = strSql + "AND RGNr<" + this.RGNr + " ORDER BY RGNr DESC;";
                }
            }
            else
            {
                strSql = strSql + " ORDER BY RGNr;";
            }
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            if (strTmp != string.Empty)
            {
                decimal decTmp = 0;
                if (Decimal.TryParse(strTmp, out decTmp))
                {
                    this.ID = decTmp;
                    Fill();
                }
            }
        }
        ///<summary>clsRechnung / GetIDbyRNr</summary>
        ///<remarks></remarks>
        private void GetIDbyRNr()
        {
            string strSql = string.Empty;
            strSql = "Select ID FROM Rechnungen WHERE MandantenID=" + this.MandantenID +
                                                    " AND ArBereichID=" + this.sys.AbBereich.ID +
                                                    " AND RGNr=" + this.RGNr + ";";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            if (strTmp != string.Empty)
            {
                decimal decTmp = 0;
                if (Decimal.TryParse(strTmp, out decTmp))
                {
                    this.ID = decTmp;
                    this.Fill();
                }
            }
        }
        ///<summary>clsRechnung / Fill</summary>
        ///<remarks>Füll die Klasse anhand der ID.</remarks>
        public void Fill()
        {
            if (clsRechnung.ExistRechnung(this._GL_User, ID))
            {
                this.RechnungExist = false;

                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = "Select a.* " +
                                "FROM Rechnungen a WHERE a.ID=" + ID + "; ";
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Rechnung");
                if (dt.Rows.Count > 0)
                {
                    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        this.ID = (decimal)dt.Rows[i]["ID"];
                        this.RGNr = (decimal)dt.Rows[i]["RGNr"]; ;
                        this.Datum = (DateTime)dt.Rows[i]["Datum"];
                        this.faellig = (DateTime)dt.Rows[i]["faellig"];
                        this.MwStSatz = (decimal)dt.Rows[i]["MwStSatz"];
                        this.MwStBetrag = (decimal)dt.Rows[i]["MwStBetrag"];
                        this.NettoBetrag = (decimal)dt.Rows[i]["NettoBetrag"];
                        this.BruttoBetrag = (decimal)dt.Rows[i]["BruttoBetrag"];
                        this.GS = (bool)dt.Rows[i]["GS"];
                        this.bezahlt = (DateTime)dt.Rows[i]["Bezahlt"];
                        this.Druck = (bool)dt.Rows[i]["Druck"];
                        this.Druckdatum = (DateTime)dt.Rows[i]["Druckdatum"];
                        //this.Benutzer       = (decimal)dt.Rows[i]["Benutzer"];
                        this.RGArt = dt.Rows[i]["RGArt"].ToString();
                        this.MandantenID = (decimal)dt.Rows[i]["MandantenID"];
                        this.ArBereichID = (decimal)dt.Rows[i]["ArBereichID"];
                        this.exFibu = (bool)dt.Rows[i]["exFibu"];
                        this.AbrZeitraumVon = (DateTime)dt.Rows[i]["AbrZeitraumVon"];
                        this.AbrZeitraumBis = (DateTime)dt.Rows[i]["AbrZeitraumBis"];
                        this.Empfaenger = (decimal)dt.Rows[i]["Empfaenger"];
                        this.Storno = (bool)dt.Rows[i]["Storno"];
                        this.Auftraggeber = (decimal)dt.Rows[i]["Auftraggeber"];
                        this.StornoID = (decimal)dt.Rows[i]["StornoID"];
                        this.AbrechnungsTarifName = dt.Rows[i]["AbrTarifName"].ToString();
                        Decimal decTmp = 0;
                        Decimal.TryParse(dt.Rows[i]["VersPraemie"].ToString(), out decTmp);
                        this.VersPraemie = decTmp;
                        DateTime dtTmp = clsSystem.const_DefaultDateTimeValue_Min;
                        DateTime.TryParse(dt.Rows[i]["RGBookPrintDate"].ToString(), out dtTmp);
                        this.RGBookPrintDate = dtTmp;
                        this.RechnungExist = true;
                        this.InfoText = dt.Rows[i]["InfoText"].ToString();
                        this.FibuInfo = dt.Rows[i]["FibuInfo"].ToString();
                        this.DocName = dt.Rows[i]["DocName"].ToString();
                    }

                    //Füllt die erste Rechnungspositionsklasse
                    if (!this.RGArt.Equals(string.Empty))
                    {
                        RGPos = new clsRGPositionen();
                        RGPos._GL_User = this._GL_User;
                        RGPos.RGID = this.ID;
                        RGPos.FillFirstRGPosOfRG();
                    }
                    //ADR klasse füllen
                    ADR_RGEmpfaenger = new clsADR();
                    ADR_RGEmpfaenger._GL_User = this._GL_User;
                    //ADR_RGEmpfaenger.ID = this.Auftraggeber;
                    ADR_RGEmpfaenger.ID = this.Empfaenger;
                    ADR_RGEmpfaenger.Fill();

                    //Gibt es einen Storno zur RG/GS?
                    ExistStornoZurRG = ExistStornoZurRGGS();

                    //Table mit allen RGPositionen zur Rechnung
                    dtRechnungsPositionen = clsRGPositionen.GetRGPositionByRGTableID(this._GL_User, ID);

                    if (this.MandantenID > 0)
                    {
                        MandantenViewData = new MandantenViewData((int)this.MandantenID);
                        Mandant = MandantenViewData.Mandant.Copy();
                    }
                }
            }
        }
        ///<summary>clsRechnung / ExistRechnung</summary>
        ///<remarks>Prüft, ob der gesuchte Datensatz mit der entsprechenden ID existiert.</remarks>
        public static bool ExistRechnung(Globals._GL_USER myGLUser, decimal myID)
        {
            string strSql = string.Empty;
            strSql = "SELECT ID FROM Rechnungen WHERE ID=" + myID + ";";
            bool reVal = clsSQLcon.ExecuteSQL_GetValueBool(strSql, myGLUser.User_ID);
            return reVal;
        }
        ///<summary>clsRechnung / GetRechnung</summary>
        ///<remarks>Ermittel den Rechnungsdatensatz anhand der RechnungsTableID.</remarks>
        public DataTable GetRechnung()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select a.ID " +
                            ", a.RGNr" +
                            ", a.Datum" +
                            ", b.RGTableID" +
                            ", b.Position" +
                            ", b.RGText as Text" +
                            ", b.Abrechnungseinheit" +
                            ", b.Einzelpreis" +
                            ", b.Menge" +
                            ", b.NettoPreis" +
                            ", b.CalcModus" +
                            ", b.CalcModValue" +
                            ", b.Abrechnungsart" +
                            ", b.TarifPosID" +
                            ", b.Tariftext" +
                            ", b.MargeEuro" +
                            ", b.MargeProzent" +
                            ", b.Anfangsbestand" +
                            ", b.Abgang" +
                            ", b.Zugang" +
                            ", b.Endbestand" +
                            ", b.RGPosText" +
                            ", b.FibuKto" +
                            ", a.InfoText" +
                            ", a.FibuInfo" +
                            ", b.PricePerUnitFactor " +


                            " FROM Rechnungen a " +
                                    "INNER JOIN RGPositionen b ON b.RGTableID = a.ID " +
                                    "WHERE a.ID=" + ID + "; ";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Rechnung");
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataTable GetNotPrintedInvoices(int myAbBereichID)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select DISTINCT a.* " +
                            " FROM Rechnungen a " +
                                    "INNER JOIN RGPositionen b ON b.RGTableID = a.ID " +
                                    "WHERE " +
                                        "a.Druck=0 " +
                                        "and a.RGArt='" + clsRechnung.const_RechnungsArt_Manuell + "' " +
                                        "and a.FibuInfo <>'" + clsRechnung.const_RGText_Dummy + "' " +
                                        "and a.ArBereichID=" + myAbBereichID;
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, 0, "NotPrintedInoivces");
            return dt;
        }
        ///<summary>clsRechnung / GetRechnung</summary>
        ///<remarks>Ermittel den Rechnungsdatensatz anhand der RechnungsTableID.</remarks>
        public DataTable GetRechnungListForPeriode(bool bGetAllWorkspaces)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select DISTINCT " +
                            "a.ID " +
                            ",a.RGNr as Beleg " +
                            ",CASE " +
                                    "WHEN (a.GS=0) AND (a.Storno=0) THEN 'RG' " +
                                    "WHEN (a.GS=1) AND (a.Storno=0) THEN 'GS' " +
                                    "WHEN (a.GS=0) AND (a.Storno=1) THEN 'RG-ST' " +
                                    "WHEN (a.GS=1) AND (a.Storno=1) THEN 'GS-ST' " +
                                    "END as Art " +
                            ", d.ViewID as Kunde " +
                            ", a.Datum" +
                            ", a.NettoBetrag as [Netto €]" +
                            ", a.MwStBetrag as [MWSt €]" +
                            ", a.BruttoBetrag as [Brutto €]" +
                            ", (Select SUM(NettoPreis) FROM RGPositionen WHERE Abrechnungsart='Einlagerungskosten' AND RGTableID=a.ID) as Einlagerung " +
                            ", (Select SUM(NettoPreis) FROM RGPositionen WHERE Abrechnungsart='Auslagerungskosten' AND RGTableID=a.ID) as Auslagerung " +
                            ", (Select SUM(NettoPreis) FROM RGPositionen WHERE Abrechnungsart='Lagerkosten' AND RGTableID=a.ID) as Lagerkosten " +
                            ", (Select SUM(NettoPreis) FROM RGPositionen WHERE Abrechnungsart='Nebenkosten' AND RGTableID=a.ID) as Nebenkosten " +
                            ", a.RGBookPrintDate " +
                            ", a.Druck" +
                            ", a.FibuInfo" +
                            ", a.RGArt as RGArt" +
                            ", e.Name as Arbeitsbereich " +
                            " FROM Rechnungen a " +
                                    "LEFT JOIN RGPositionen b ON b.RGTableID = a.ID " +
                                    "LEFT JOIN RGPosArtikel c ON c.RGPosID=b.ID " +
                                    "LEFT JOIN ADR d ON d.ID=a.Empfaenger " +
                                    "LEFT JOIN Arbeitsbereich e ON e.ID=a.ArBereichID " +
                                    "WHERE " +
                                            "a.Datum between '" + RGDatumVon.ToString() + "' AND '" + RGDatumBis.AddDays(1).ToString() + "' ";

            if (!bGetAllWorkspaces)
            {
                strSql = strSql + "AND a.MandantenID=" + this.MandantenID +
                                  " AND a.ArBereichID=" + this.sys.AbBereich.ID + " ";
            }
            if (this.Empfaenger > 0)
            {
                strSql = strSql + "AND a.Empfaenger=" + this.Empfaenger + " ";
            }
            strSql += "Order by Datum,a.RGNr ";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "RGList");
            return dt;
        }
        ///<summary>clsRechnung / GetRechnung</summary>
        ///<remarks>Ermittel den Rechnungsdatensatz anhand der RechnungsTableID.</remarks>
        public DataTable GetExistRG(decimal myAdrID)
        {
            DataTable dt = new DataTable();
            if (clsADR.ExistAdrID(myAdrID, this._GL_User.User_ID))
            {
                string strSql = string.Empty;
                strSql = "Select a.ID " +
                                ", a.RGNr as RGNr" +
                                ", a.Datum" +
                                ", a.AbrTarifName as Bemerkung" +
                                ",CASE " +
                                    "WHEN (a.GS=0) AND (a.Storno=0) THEN 'RG' " +
                                    "WHEN (a.GS=1) AND (a.Storno=0) THEN 'GS' " +
                                    "WHEN (a.GS=0) AND (a.Storno=1) THEN 'RG-ST' " +
                                    "WHEN (a.GS=1) AND (a.Storno=1) THEN 'GS-ST' " +
                                    "END as Art " +
                                ", a.Druck" +
                                ", Year(a.Datum) as Jahr" +
                                " FROM Rechnungen a " +
                                            "WHERE a.Auftraggeber=" + myAdrID + " " +
                                            "AND a.ArBereichID=" + this.sys.AbBereich.ID + " " +
                                            "AND a.MandantenID=" + this.sys.AbBereich.MandantenID + " " +
                                            "AND a.RGArt IN ('" + clsRechnung.const_RechnungsArt_Lager + "') " +
                                            "Order By a.RGNr, a.Datum DESC ";
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, this._GL_User.User_ID, "Rechnung");
            }
            return dt;
        }
        ///<summary>clsRechnung / CreateStorno</summary>
        ///<remarks></remarks>
        public void CreateStorno()
        {
            //Daten für den neuen Stornodatensatz
            clsRechnung StornoDoc = new clsRechnung();
            StornoDoc.sys = this.sys;
            StornoDoc._GL_User = this._GL_User;
            StornoDoc.ID = this.ID;
            StornoDoc.Fill();
            //folgende Daten müssen nun noch abgeändert werden
            //Rechnungs/Gutschriftnummer
            clsPrimeKeys pk = new clsPrimeKeys();
            pk.sys = this.sys;
            pk._GL_User = this._GL_User;
            pk.Mandanten_ID = this.MandantenID;
            //Unterscheidung ob RG-Storno oder GS-Storno
            //wenn Original Daten GS=0 dann liegt im Original eine Rechnung vor 
            if (this.GS)
            {
                pk.GetNEWGSNr();
                StornoDoc.RGNr = pk.GSNr;
            }
            else
            {
                pk.GetNEWRGNr();
                StornoDoc.RGNr = pk.RGNr;
            }
            //Stornodatum
            StornoDoc.Datum = DateTime.Now;
            //Verknüpfung auf die alte original Rechnung / Gutschrift
            StornoDoc.StornoID = this.ID;
            StornoDoc.Storno = true;
            StornoDoc.Druck = false;
            StornoDoc.RGArt = this.RGArt; // clsRechnung.const_RechnungsArt_Lager;
            StornoDoc.Druckdatum = Globals.DefaultDateTimeMaxValue;
            //Baustelle Zahlungsziel mit ienfügen
            StornoDoc.faellig = DateTime.Now.AddDays(14);

            //-- DocName
            if (this.GS)
            {
                StornoDoc.DocName = "GUTSCHRIFTSKORREKTUR Nr";
            }
            else
            {
                StornoDoc.DocName = "RECHNUNGSKORREKTUR Nr";
            }

            string strSql = string.Empty;
            strSql = StornoDoc.GetAddRechnungSQLString();
            strSql = strSql + " Select @@IDENTITY; ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTemp = 0;
            Decimal.TryParse(strTmp, out decTemp);
            if (decTemp > 0)
            {
                //ID zusweisen
                StornoDoc.ID = decTemp;

                strSql = string.Empty;
                //Eintrag der neuen RGPositioenn
                strSql = strSql + StornoDoc.RGPos.GetSQLStornoUpdateItems(this._GL_User, StornoDoc.ID, StornoDoc.StornoID);
                //Update der RGPosArtikel Storno=1 setzen
                strSql = strSql + StornoDoc.RGPosArtikel.GetSQLUpdateRGPosArtikelString(StornoDoc.StornoID);
                //SQL zusammengstell und nun als Transaction einfügen
                bool bTAOK = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "Storno", Benutzer);
            }
        }
        ///<summary>clsRechnung / Delete</summary>
        ///<remarks>Rechnung löschen</remarks>
        public void Delete(bool bIsResivion)
        {
            //FK in RGPositionen noch anpassen,damit die 
            string strSql = string.Empty;
            strSql = "Delete Rechnungen WHERE ID=" + ID + "; ";
            if (!bIsResivion)
            {
                strSql = strSql + " Update Mandanten SET RGNr =" +
                                                    "CASE " +
                                                        "WHEN (Select MAX(RGNr) FROM Rechnungen WHERE MandantenID=1 AND ArBereichID=1 ) IS NULL THEN 1 " +
                                                        "else (Select MAX(RGNr) FROM Rechnungen WHERE MandantenID=1 AND ArBereichID=1 ) " +
                                                        "END " +
                                                " WHERE ID=" + this.sys.AbBereich.MandantenID + " ";
                strSql = strSql + " Update PrimeKeys SET RGNr =" +
                                                        "CASE " +
                                                        "WHEN (Select MAX(RGNr) FROM Rechnungen WHERE MandantenID=1 AND ArBereichID=1 ) IS NULL THEN 1 " +
                                                        "else (Select MAX(RGNr) FROM Rechnungen WHERE MandantenID=1 AND ArBereichID=1 ) " +
                                                        "END " +
                                    " WHERE " +
                                    "Mandanten_ID=" + this.sys.AbBereich.MandantenID +
                                    " AND AbBereichID=" + this.sys.AbBereich.ID + " ;";

            }
            if (clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "Delete", Benutzer))
            {
                if (!bIsResivion)
                {
                    //Add Logbucheintrag Löschen
                    string Txt = "ID:[" + this.ID.ToString() + "]/ RG-/GS-Nr:[" + this.RGNr + "] / Mandant:[" + this.MandantenID + "]";
                    string Beschreibung = "Rechnung: " + Txt + "  gelöscht";
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), Beschreibung);
                }
            }
        }
        ///<summary>clsRechnung / RGRevision</summary>
        ///<remarks></remarks>
        public void Revision()
        {
            this.RGRevision = this.Copy();
        }
        ///<summary>clsRechnung / UpdateTarif</summary>
        ///<remarks>Ändern des Tarifdatensatzes.</remarks>
        public void UpdateRechnungPrint()
        {
            string strSql = string.Empty;
            strSql = "Update Rechnungen SET Druck = " + Convert.ToInt32(Druck) + " " +
                                            ", Druckdatum='" + Druckdatum + "' " +
                                            "WHERE ID=" + ID + " ;";


            if (clsSQLcon.ExecuteSQL(strSql, BenutzerID))
            {
                //Baustellel Add in Logbuch
            }
        }
        ///<summary>clsRechnung / UpdateTarif</summary>
        ///<remarks>Ändern des Tarifdatensatzes.</remarks>
        public bool UpdateRechnungInfoText()
        {
            string strSql = string.Empty;
            strSql = "Update Rechnungen SET InfoText = '" + InfoText + "' " +
                                            "WHERE ID=" + ID + " ;";
            bool bReturn = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            return bReturn;
        }
        ///<summary>clsRechnung / UpdateTarif</summary>
        ///<remarks>Ändern des Tarifdatensatzes.</remarks>
        public bool UpdateRechnungFibuInfo()
        {
            string strSql = string.Empty;
            strSql = "Update Rechnungen SET FibuInfo = '" + FibuInfo + "' " +
                                            "WHERE ID=" + ID + " ;";
            bool bReturn = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            return bReturn;
        }
        ///<summary>clsRechnung / UpdateTarif</summary>
        ///<remarks>Ändern des Tarifdatensatzes.</remarks>
        public bool ExistStornoZurRGGS()
        {
            string strSql = string.Empty;
            strSql = "Select a.ID FROM Rechnungen a WHERE a.StornoID=" + this.ID + " ;";
            bool reVal = clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            return reVal;

            //if (this.StornoID > 0)
            //{
            //    string strSql = string.Empty;
            //    strSql = "Select a.ID FROM Rechnungen a WHERE a.StornoID=" + this.StornoID + " ;";
            //    bool reVal = clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            //    return reVal;
            //}
            //else
            //{
            //    return false;
            //}
        }
        ///<summary>clsRechnung / GetRechnung</summary>
        ///<remarks>Ermittel den Rechnungsdatensatz anhand der RechnungsTableID.</remarks>
        public static DateTime GetLastRGDate(Globals._GL_USER GLUser, clsSystem mySys, bool bIsRG)
        {
            DateTime dtRet = DateTime.Now;
            string strSql = string.Empty;
            strSql = "Select MAX(Datum) FROM Rechnungen ";
            if (bIsRG)
            {
                strSql = strSql + " WHERE GS=0 ";
            }
            else
            {
                strSql = strSql + " WHERE GS=1 ";
            }
            // 2020_06_09
            strSql = strSql + " AND ArBereichID=" + (int)mySys.AbBereich.ID;

            string strDate = clsSQLcon.ExecuteSQL_GetValue(strSql, GLUser.User_ID);
            DateTime.TryParse(strDate, out dtRet);
            return dtRet;
        }
        ///<summary>clsRechnung / UpdatePrintRGBook</summary>
        ///<remarks>Setzt das Datum in der Datenbank</remarks>
        public static bool UpdatePrintRGBook(Globals._GL_USER GLUser)
        {
            DateTime dtRet = DateTime.Now;
            string strSql = string.Empty;
            strSql = "Update Rechnungen set RGBookPrintDate='" + dtRet + "' where RGBookPrintDate='1900-01-01 00:00:00.000';";
            return clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "RGBOOK", GLUser.User_ID);
        }
        ///<summary>clsRechnung / UpdatePrintRGBook</summary>
        ///<remarks>Setzt das Datum in der Datenbank</remarks>
        public static decimal CountRGForRGBook(Globals._GL_USER GLUser)
        {
            string strSql = string.Empty;
            strSql = "Select count(ID) from Rechnungen where RGBookPrintDate='1900-01-01 00:00:00.000';";
            decimal decRet = 0;
            decimal.TryParse(clsSQLcon.ExecuteSQL_GetValue(strSql, GLUser.User_ID), out decRet);
            return decRet;
        }

    }
}
