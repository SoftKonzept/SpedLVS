using System;
using System.Data;

namespace LVS
{
    public class clsPrimeKeys
    {
        ///<summary>clsPrimeKeys
        ///         Eigenschaften:
        ///             - ID
        ///             - ABName
        ///             - Bemerkung
        ///             - aktiv
        ///             - BenutzerID</summary>

        //************  User  ***************
        public clsSystem sys;
        public Globals._GL_USER _GL_User;
        public Globals._GL_SYSTEM _GL_System;
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
        //************************************

        public decimal ID { get; set; }
        public decimal Mandanten_ID { get; set; }
        public decimal AbBereichID { get; set; }
        public decimal AuftragsNr { get; set; }
        public decimal LfsNr { get; set; }
        public decimal LvsNr { get; set; }
        public decimal RGNr { get; set; }
        public decimal GSNr { get; set; }
        public decimal LEingangID { get; set; }
        public decimal LAusgangID { get; set; }

        public decimal MAXAuftragsNr { get; set; }
        public decimal MAXLfsNr { get; set; }
        public decimal MAXLvsNr { get; set; }
        public decimal MAXRGNr { get; set; }
        public decimal MAXGSNr { get; set; }
        public decimal MAXLEingangID { get; set; }
        public decimal MAXLAusgangID { get; set; }


        /***************************************************************************************************
        *                                     Methoden und Funktionen
        ***************************************************************************************************/
        ///<summary>clsPrimeKeys / InitClass</summary>
        ///<remarks></remarks>       
        public void InitClass(Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSystem)
        {
            this._GL_User = myGLUser;
            this._GL_System = myGLSystem;
            this.Mandanten_ID = this._GL_System.sys_MandantenID;

        }
        ///<summary>clsPrimeKeys / AddNewPrimeKeys</summary>
        ///<remarks>Ein neuer Primekey - Datensatz soll angelegt werden für einen Mandanten.Ablauf:
        ///         - prüfen >>> für den Mandanten darf noch kein Datensatz vorliegen
        ///         - Standardwerte setzen
        ///         - Eintrag</remarks>       
        ///<returns>Returns BOOL</returns>
        public bool AddNewPrimeKeys()
        {
            bool bEintragOK = false;
            if (Mandanten_ID > 0)
            {
                if (clsPrimeKeys.DBPrimeKeysHasRows(BenutzerID))
                {
                    //MandantenCheck
                    if (!ExistPrimeKeysMandant(Mandanten_ID, AbBereichID, BenutzerID))
                    {
                        //Standardwerte setzen 
                        //Einfacher Eintrag reicht, das Default 1 in der DB hinterlegt ist
                        bEintragOK = AddPrimekeysDefault();
                    }
                }
                else
                {
                    //Standardwerte setzen 
                    //Einfacher Eintrag reicht, das Default 1 in der DB hinterlegt ist
                    bEintragOK = AddPrimekeysDefault();
                }
            }
            return bEintragOK;
        }
        ///<summary>clsPrimeKeys / ExistPrimeKeysMandant</summary>
        ///<remarks>Prüft, ob der Arbeitsbereichsname bereits in der DB existiert.</remarks>
        ///<returns>Returns BOOL</returns>
        public static bool ExistPrimeKeysMandant(decimal decMandant, decimal decAbBereich, decimal decBenutzerID)
        {
            bool boVal = false;
            if (decMandant > 0)
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = "Select * FROM PrimeKeys WHERE Mandanten_ID=" + decMandant + " AND AbBereichID=" + decAbBereich + ";";
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, decBenutzerID, "PrimeKeys");

                if (dt.Rows.Count > 0)
                {
                    boVal = true;
                }
            }
            return boVal;
        }
        ///<summary>clsPrimeKeys / AddPrimekeysDefault</summary>
        ///<remarks>Eintrag des neuen default Datensatzes in die DB.</remarks>
        public bool AddPrimekeysDefault()
        {
            string strSql = string.Empty;
            strSql = "INSERT INTO PrimeKeys (Mandanten_ID, AuftragsNr, LfsNr, LvsNr, RGNr, GSNr, LEingangID, LAusgangID, AbBereichID) " +
                                           "VALUES (" +
                                                        (int)this.Mandanten_ID +
                                                        ", " + 1 +          // Auftragsnummer
                                                        ", " + 1 +          // LfsNr
                                                        ", (SELECT Top(1) LvsNr FROM PrimeKeys where Mandanten_ID=" + (int)Mandanten_ID + ")" +   // LvsNr
                                                        ", (SELECT Top(1) RGNr FROM PrimeKeys where Mandanten_ID=" + (int)Mandanten_ID + ")" +   // RGNr 
                                                        ", (SELECT Top(1) GSNr FROM PrimeKeys where Mandanten_ID=" + (int)Mandanten_ID + ")" +   // GSNr 
                                                        ", " + 1 +           // LEingangID
                                                        ", " + 1 +           // LAusgangID
                                                        ", " + (int)this.AbBereichID +
                                                        ")";


            return clsSQLcon.ExecuteSQL(strSql, BenutzerID);
        }
        ///<summary>clsPrimeKeys / DBPrimeKeysHasRows</summary>
        ///<remarks>Prüft, ob die Table Primekeys bereits Datensätze enthält. Diese Abfrage wird für erste Installation von Sped4 benötigt.</remarks>
        ///<returns>Returns BOOL</returns>
        public static bool DBPrimeKeysHasRows(decimal decBenutzerID)
        {
            bool boVal = false;
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select * FROM PrimeKeys";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, decBenutzerID, "PrimeKeys");

            if (dt.Rows.Count > 0)
            {
                boVal = true;
            }
            return boVal;
        }
        ///<summary>clsPrimeKeys / GetPrimeKeys</summary>
        ///<remarks>Füllt die Eigenschaften der Klasse mit den Daten des Mandanten.</remarks>
        public void Fill()
        {
            if (Mandanten_ID > 0)
            {
                DataTable dt = new DataTable();
                dt = ReadPrimeKeyByMandant();
                if (dt.Rows.Count > 0)
                {
                    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        ID = (decimal)dt.Rows[i]["ID"];
                        Mandanten_ID = (decimal)dt.Rows[i]["Mandanten_ID"];
                        AuftragsNr = (decimal)dt.Rows[i]["AuftragsNr"];
                        LfsNr = (decimal)dt.Rows[i]["LfsNr"];
                        LvsNr = (decimal)dt.Rows[i]["LvsNr"];
                        RGNr = (decimal)dt.Rows[i]["RGNr"];
                        GSNr = (decimal)dt.Rows[i]["GSNr"];
                        LEingangID = (decimal)dt.Rows[i]["LEingangID"];
                        LAusgangID = (decimal)dt.Rows[i]["LAusgangID"];
                        AbBereichID = (decimal)dt.Rows[i]["AbBereichID"];
                        FillMAXValue();
                    }
                }
            }
        }
        ///<summary>clsPrimeKeys/FillMAXValue</summary>
        ///<remarks>Füllt die Klasse</remarks>ks>
        public void FillMAXValue()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            //strSql = "SELECT " +
            //                "(Select ISNULL(MAX(ANr),1) FROM Auftrag WHERE MandantenID=a.Mandanten_ID) as MaxAuftragsNr " +
            //                ", (Select ISNULL(MAX(LfsNr),1) FROM Lieferschein d " +
            //                                        "INNER JOIN AuftragPos b ON b.ID=d.AP_ID " +
            //                                        "INNER JOIN Auftrag c ON c.ID=b.AuftragTableID " +
            //                                        "WHERE c.MandantenID=a.Mandanten_ID) as MaxLfsNr " +
            //                ", (Select ISNULL(MAX(b.LVS_ID),1) FROM Artikel b " +
            //                                        "INNER JOIN LEingang c ON c.ID=b.LEingangTableID " +
            //                                        "WHERE c.Mandant=a.Mandanten_ID) as MaxLvsNr " +
            //                ", (SELECT ISNULL(MAX(RGNr),1) FROM Rechnungen WHERE GS=0 AND MandantenID=a.Mandanten_ID) as MaxRGNr " +
            //                ", (SELECT ISNULL(MAX(RGNr),1) FROM Rechnungen WHERE GS=1 AND MandantenID=a.Mandanten_ID) as MaxGSNr " +
            //                ", (SELECT ISNULL(MAX(LEingangID),1) FROM LEingang WHERE Mandanten_ID=a.Mandanten_ID) as MaxLEingangID " +
            //                ", (SELECT ISNULL(MAX(LAusgangID),1) FROM lAusgang WHERE MandantenID=a.Mandanten_ID)as MaxLAusgangID " +

            //                "FROM PrimeKeys a WHERE a.ID="+Mandanten_ID+"; ";

            strSql = "SELECT " +
                "(Select ISNULL(MAX(ANr),1) FROM Auftrag WHERE MandantenID=a.Mandanten_ID AND ArbeitsbereichID=a.AbBereichID) as MaxAuftragsNr " +
                ", (Select ISNULL(MAX(LfsNr),1) FROM Lieferschein d " +
                                        "INNER JOIN AuftragPos b ON b.ID=d.AP_ID " +
                                        "INNER JOIN Auftrag c ON c.ID=b.AuftragTableID " +
                                        "WHERE c.MandantenID=a.Mandanten_ID AND c.ArbeitsbereichID=a.AbBereichID) as MaxLfsNr " +
                ", (Select ISNULL(MAX(b.LVS_ID),1) FROM Artikel b " +
                                        "INNER JOIN LEingang c ON c.ID=b.LEingangTableID " +
                                        "WHERE c.Mandant=a.Mandanten_ID AND c.AbBereich=a.AbBereichID ) as MaxLvsNr " +
                ", (SELECT ISNULL(MAX(RGNr),1) FROM Rechnungen WHERE GS=0 AND MandantenID=a.Mandanten_ID) as MaxRGNr " +
                ", (SELECT ISNULL(MAX(RGNr),1) FROM Rechnungen WHERE GS=1 AND MandantenID=a.Mandanten_ID) as MaxGSNr " +
                ", (SELECT ISNULL(MAX(LEingangID),1) FROM LEingang WHERE Mandanten_ID=a.Mandanten_ID AND AbBereich=a.AbBereichID) as MaxLEingangID " +
                ", (SELECT ISNULL(MAX(LAusgangID),1) FROM lAusgang WHERE MandantenID=a.Mandanten_ID AND AbBereich=a.AbBereichID)as MaxLAusgangID " +

                "FROM PrimeKeys a WHERE a.ID=" + Mandanten_ID + "; ";

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Primekeys");

            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                this.MAXAuftragsNr = (decimal)dt.Rows[i]["MaxAuftragsNr"];
                this.MAXLfsNr = (decimal)dt.Rows[i]["MaxLfsNr"];
                this.MAXLvsNr = (decimal)dt.Rows[i]["MaxLvsNr"];
                this.MAXRGNr = (decimal)dt.Rows[i]["MaxRGNr"];
                this.MAXGSNr = (decimal)dt.Rows[i]["MaxGSNr"];
                this.MAXLEingangID = (decimal)dt.Rows[i]["MaxLEingangID"];
                this.MAXLAusgangID = (decimal)dt.Rows[i]["MaxLAusgangID"];
            }
        }
        ///<summary>clsPrimeKeys / ReadPrimeKeyByMandant</summary>
        ///<remarks>Ermittelt anhand der Mandanten ID die Primekeys für diesen Mandanten.</remarks>
        ///<returns>Returns DataTable</returns>
        private DataTable ReadPrimeKeyByMandant()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select * FROM PrimeKeys WHERE Mandanten_ID=" + Mandanten_ID + " AND AbBereichID=" + AbBereichID + " ";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Primekeys");
            return dt;
        }
        ///<summary>clsPrimeKeys / UpdatePrimeKeyAuftragsNr</summary>
        ///<remarks>Update der Auftragsnummer anhand der Mandanten ID.</remarks>
        ///<returns>Returns DataTable</returns>
        public void UpdatePrimeKeyAuftragsNr()
        {
            if (Mandanten_ID > 0)
            {
                FillMAXValue();
                if (AuftragsNr + 1 < MAXAuftragsNr)
                {
                    AuftragsNr = MAXAuftragsNr;
                }
                string strSql = string.Empty;
                strSql = "Update PrimeKeys SET AuftragsNr ='" + AuftragsNr + "' WHERE Mandanten_ID=" + Mandanten_ID + "  AND AbBereich=" + AbBereichID + " ;";
                clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            }
        }
        ///<summary>clsPrimeKeys / UpdatePrimeKeyLfsNr</summary>
        ///<remarks>Update der Lieferscheinnummer anhand der Mandanten ID.</remarks>
        ///<returns>Returns DataTable</returns>
        public void UpdatePrimeKeyLfsNr()
        {
            if (Mandanten_ID > 0)
            {
                FillMAXValue();
                if (LfsNr + 1 < MAXLfsNr)
                {
                    LfsNr = MAXLfsNr;
                }
                string strSql = string.Empty;
                strSql = "Update PrimeKeys SET LfsNr ='" + LfsNr + "' WHERE Mandanten_ID=" + Mandanten_ID + "  AND AbBereich=" + AbBereichID + "";
                clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            }
        }
        ///<summary>clsPrimeKeys / UpdatePrimeKeyLvsNr</summary>
        ///<remarks>Update der Lvs- Nummer anhand der Mandanten ID.</remarks>
        ///<returns>Returns DataTable</returns>
        public void UpdatePrimeKeyLvsNr()
        {
            if (Mandanten_ID > 0)
            {
                FillMAXValue();
                if (LvsNr + 1 < MAXLvsNr)
                {
                    LvsNr = MAXLvsNr;
                }
                string strSql = string.Empty;
                strSql = "Update PrimeKeys SET LvsNr ='" + LvsNr + "' WHERE Mandanten_ID=" + Mandanten_ID + "  AND AbBereich=" + AbBereichID + ";";
                clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            }
        }
        ///<summary>clsPrimeKeys / UpdatePrimeKeyRGNr</summary>
        ///<remarks>Update der Rechnungsnummer anhand der Mandanten ID.</remarks>
        ///<returns>Returns DataTable</returns>
        public void UpdatePrimeKeyRGNr()
        {
            if (Mandanten_ID > 0)
            {
                FillMAXValue();
                if (RGNr + 1 < MAXRGNr)
                {
                    RGNr = MAXRGNr;
                }
                string strSql = string.Empty;
                if (this.sys.Client.Modul.Fakt_GetRGGSNrFromTable_Mandant)
                {
                    strSql = "Update Mandanten SET RGNr ='" + RGNr + "' WHERE ID=" + Mandanten_ID + ";";
                }
                else
                {
                    strSql = "Update PrimeKeys SET RGNr ='" + RGNr + "' WHERE Mandanten_ID=" + Mandanten_ID + " AND AbBereich=" + AbBereichID + ";";
                }
                clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            }
        }
        ///<summary>clsPrimeKeys / UpdatePrimeKeyGSNr</summary>
        ///<remarks>Update der Rechnungsnummer anhand der Mandanten ID.</remarks>
        ///<returns>Returns DataTable</returns>
        public void UpdatePrimeKeyGSNr()
        {
            if (Mandanten_ID > 0)
            {
                FillMAXValue();
                if (GSNr + 1 < MAXGSNr)
                {
                    GSNr = MAXGSNr;
                }
                string strSql = string.Empty;

                if (this.sys.Client.Modul.Fakt_GetRGGSNrFromTable_Mandant)
                {
                    strSql = "Update Mandanten SET GSNr ='" + GSNr + "' WHERE ID=" + Mandanten_ID + ";";
                }
                else
                {
                    strSql = "Update PrimeKeys SET GSNr ='" + GSNr + "' WHERE Mandanten_ID=" + Mandanten_ID + " AND AbBereich=" + AbBereichID + ";";
                }
                clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            }
        }
        ///<summary>clsPrimeKeys / UpdatePrimeKeyLfsNr</summary>
        ///<remarks>Update der Lieferscheinnummer anhand der Mandanten ID.</remarks>
        ///<returns>Returns DataTable</returns>
        public void UpdatePrimeKeyLEingangID()
        {
            if (Mandanten_ID > 0)
            {
                FillMAXValue();
                if (LEingangID + 1 < MAXLEingangID)
                {
                    LEingangID = MAXLEingangID;
                }
                string strSql = string.Empty;
                strSql = "Update PrimeKeys SET LEingangID ='" + LEingangID + "' WHERE Mandanten_ID=" + Mandanten_ID + " AND AbBereich=" + AbBereichID + ";";
                clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            }
        }
        ///<summary>clsPrimeKeys / UpdatePrimeKeyLfsNr</summary>
        ///<remarks>Update der Lieferscheinnummer anhand der Mandanten ID.</remarks>
        ///<returns>Returns DataTable</returns>
        public void UpdatePrimeKeyLAusgangID()
        {
            if (Mandanten_ID > 0)
            {
                FillMAXValue();
                if (LAusgangID + 1 < MAXLAusgangID)
                {
                    LAusgangID = MAXLAusgangID;
                }
                string strSql = string.Empty;
                strSql = "Update PrimeKeys SET LAusgangID ='" + LAusgangID + "' WHERE Mandanten_ID=" + Mandanten_ID + " AND AbBereich=" + AbBereichID + ";";
                clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            }
        }
        ///<summary>clsPrimeKeys / GetNEWAuftragsNr</summary>
        ///<remarks>Ermittelt die Auftragsnummer anhand der Mandanten ID.</remarks>
        ///<returns>Returns DataTable</returns>
        public void GetNEWAuftragsNr()
        {
            decimal decTmp = 0;
            if (Mandanten_ID > 0)
            {
                string strSql = string.Empty;
                //strSql = "DECLARE @NewID table( NewAID decimal ); " +
                //         "UPDATE PrimeKeys SET AuftragsNr= AuftragsNr + 1 " +
                //         "OUTPUT INSERTED.AuftragsNr INTO @NewID; " +
                //         "SELECT * FROM @NewId;";

                strSql = "DECLARE @NewID table( NewAID decimal ); " +
                         "UPDATE PrimeKeys SET " +
                                           "AuftragsNr = (CASE " +
                                                            "WHEN (AuftragsNr = (SELECT MAX(ANr) FROM Auftrag WHERE MandantenID=" + Mandanten_ID + " AND AbBereichID=" + AbBereichID + ")) " +
                                                            "THEN AuftragsNr + 1 " +
                                                            "ELSE (SELECT MAX(ANr) FROM Auftrag WHERE MandantenID=" + Mandanten_ID + " AND AbBereichID=" + AbBereichID + ")+1 " +
                                                        "END) " +
                        "OUTPUT INSERTED.AuftragsNr INTO @NewID WHERE Mandanten_ID=" + Mandanten_ID + " AND AbBereichID=" + AbBereichID + "; " +
                        "SELECT * FROM @NewId; ";

                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
                Decimal.TryParse(strTmp, out decTmp);
                //wenn noch keine Aufträge erfasst wurden, dann bleibt die ID immer =0
                //also muss wenn ID=0 erst um 1 erhöht werden, da die Auftragnummer nicht 0 sein darf
                if (decTmp == 0)
                {
                    decTmp++;
                }
                AuftragsNr = decTmp;
            }
            AuftragsNr = decTmp;
        }
        ///<summary>clsPrimeKeys / {</summary>
        ///<remarks>Ermittelt die Auftragsnummer anhand der Mandanten ID.</remarks>
        ///<returns></returns>
        public string GetNEWLvsNrSQL()
        {
            string strSql =
                         "UPDATE PrimeKeys SET " +
                                                "LvsNr = ( " +
                                                            "CASE " +
                                                                "WHEN ((SELECT MAX(Artikel.LVS_ID)FROM Artikel INNER JOIN LEingang ON LEingang.ID=Artikel.LEingangTableID WHERE LEingang.Mandant=" + Mandanten_ID + " AND LEingang.AbBereich=" + AbBereichID + ") IS NULL) THEN 1 " +
                                                            "ELSE " +
                                                              "CASE " +
                                                                "WHEN (LvsNr = (SELECT MAX(Artikel.LVS_ID)FROM Artikel INNER JOIN LEingang ON LEingang.ID=Artikel.LEingangTableID WHERE LEingang.Mandant=" + Mandanten_ID + " AND LEingang.AbBereich=" + AbBereichID + "))" +
                                                                "THEN LvsNr + 1 " +
                                                                "ELSE(SELECT MAX(Artikel.LVS_ID)FROM Artikel INNER JOIN LEingang ON LEingang.ID=Artikel.LEingangTableID WHERE LEingang.Mandant=" + Mandanten_ID + " AND LEingang.AbBereich=" + AbBereichID + ")+1 " +
                                                              "END " +
                                                            "END " +
                                                        ") " +
                           "OUTPUT INSERTED.LvsNr INTO @NewID WHERE Mandanten_ID=" + Mandanten_ID + " AND AbBereich=" + AbBereichID + "; " +
                           "SELECT * FROM @NewLVSId; ";
            return strSql;
        }
        ///<summary>clsPrimeKeys / GetNEWLfsNr</summary>
        ///<remarks>Ermittelt die Lieferscheinnummer anhand der Mandanten ID.</remarks>
        public void GetNEWLfsNr()
        {
            if (Mandanten_ID > 0)
            {
                string strSql = string.Empty;
                strSql = "DECLARE @NewID table( NewAID decimal ); " +
                         "UPDATE PrimeKeys SET LfsNr= LfsNr + 1 " +
                         "OUTPUT INSERTED.LfsNr INTO @NewID WHERE Mandanten_ID=" + Mandanten_ID + " AND AbBereichID=" + AbBereichID + "; " +
                         "SELECT * FROM @NewId;";
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
                LfsNr = Convert.ToDecimal(strTmp);
            }
            else
            {
                LfsNr = 0;
            }
        }
        ///<summary>clsPrimeKeys / GetNEWLvsNr</summary>
        ///<remarks>Ermittelt die Lvs-Nr anhand der Mandanten ID.</remarks>
        ///<returns>Returns DataTable</returns>
        public void GetNEWLvsNr(bool bUpdate = true)
        {
            if (Mandanten_ID > 0)
            {
                string strSql = string.Empty;
                if (bUpdate)
                {
                    strSql = "DECLARE @NewID table( NewAID decimal ); ";
                    //"UPDATE PrimeKeys SET " +
                    //                    "LvsNr = ( " +
                    //                                "CASE " +
                    //                                    "WHEN ((SELECT MAX(Artikel.LVS_ID)FROM Artikel INNER JOIN LEingang ON LEingang.ID=Artikel.LEingangTableID WHERE LEingang.Mandant=" + Mandanten_ID + " AND LEingang.AbBereich=" + AbBereichID + ") IS NULL) THEN 1 " +
                    //                                "ELSE " +
                    //                                  "CASE " +
                    //                                    "WHEN (LvsNr = (SELECT MAX(Artikel.LVS_ID)FROM Artikel INNER JOIN LEingang ON LEingang.ID=Artikel.LEingangTableID WHERE LEingang.Mandant=" + Mandanten_ID + " AND LEingang.AbBereich=" + AbBereichID + "))" +
                    //                                    "THEN LvsNr + 1 " +
                    //                                    "ELSE(SELECT MAX(Artikel.LVS_ID)FROM Artikel INNER JOIN LEingang ON LEingang.ID=Artikel.LEingangTableID WHERE LEingang.Mandant=" + Mandanten_ID + " AND LEingang.AbBereich=" + AbBereichID + ")+1 " +
                    //                                  "END " +
                    //                                "END " +
                    //                            ") ";

                    if (this.sys.Client.Modul.PrimeyKey_LVSNRUseOneIDRange)
                    {
                        //strSql += "UPDATE PrimeKeys SET " +
                        //           "LvsNr = ( " +
                        //                       "CASE " +
                        //                           "WHEN ((SELECT MAX(Artikel.LVS_ID)FROM Artikel INNER JOIN LEingang ON LEingang.ID=Artikel.LEingangTableID ) IS NULL) THEN 1 " +
                        //                       "ELSE " +
                        //                         "CASE " +
                        //                           "WHEN (LvsNr = (SELECT MAX(Artikel.LVS_ID)FROM Artikel INNER JOIN LEingang ON LEingang.ID=Artikel.LEingangTableID))" +
                        //                           "THEN LvsNr + 1 " +
                        //                           "ELSE(SELECT MAX(Artikel.LVS_ID)FROM Artikel INNER JOIN LEingang ON LEingang.ID=Artikel.LEingangTableID )+1 " +
                        //                         "END " +
                        //                       "END " +
                        //                   ") " +
                        //                   " OUTPUT INSERTED.LvsNr INTO @NewID; ";
                        strSql += "UPDATE PrimeKeys SET " +
                                   "LvsNr =" + clsPrimeKeys.SQLStringNewLVSNr(this.sys) +
                                           " OUTPUT INSERTED.LvsNr INTO @NewID; ";
                    }
                    else
                    {
                        //strSql+= "UPDATE PrimeKeys SET " +
                        //       "LvsNr = ( " +
                        //                   "CASE " +
                        //                       "WHEN ((SELECT MAX(Artikel.LVS_ID)FROM Artikel INNER JOIN LEingang ON LEingang.ID=Artikel.LEingangTableID WHERE LEingang.Mandant=" + Mandanten_ID + " AND LEingang.AbBereich=" + AbBereichID + ") IS NULL) THEN 1 " +
                        //                   "ELSE " +
                        //                     "CASE " +
                        //                       "WHEN (LvsNr = (SELECT MAX(Artikel.LVS_ID)FROM Artikel INNER JOIN LEingang ON LEingang.ID=Artikel.LEingangTableID WHERE LEingang.Mandant=" + Mandanten_ID + " AND LEingang.AbBereich=" + AbBereichID + "))" +
                        //                       "THEN LvsNr + 1 " +
                        //                       "ELSE(SELECT MAX(Artikel.LVS_ID)FROM Artikel INNER JOIN LEingang ON LEingang.ID=Artikel.LEingangTableID WHERE LEingang.Mandant=" + Mandanten_ID + " AND LEingang.AbBereich=" + AbBereichID + ")+1 " +
                        //                     "END " +
                        //                   "END " +
                        //               ") "+
                        //          " OUTPUT INSERTED.LvsNr INTO @NewID WHERE Mandanten_ID=" + Mandanten_ID + " AND AbBereichID=" + AbBereichID + "; ";

                        strSql += "UPDATE PrimeKeys SET " +
                                   "LvsNr =" + clsPrimeKeys.SQLStringNewLVSNr(this.sys) +
                                      " OUTPUT INSERTED.LvsNr INTO @NewID WHERE Mandanten_ID=" + Mandanten_ID + " AND AbBereichID=" + AbBereichID + "; ";
                    }
                    strSql += "SELECT * FROM @NewId; ";
                }
                else
                {
                    //strSql = "Select LVS_ID from PrimeKeys WHERE Mandanten_ID=" + Mandanten_ID + "; ";

                    if (this.sys.Client.Modul.PrimeyKey_LVSNRUseOneIDRange)
                    {
                        strSql = "Select MAX(LVS_ID) from PrimeKeys; "; // WHERE Mandanten_ID=" + Mandanten_ID + "; ";
                    }
                    else
                    {
                        strSql = "Select LVS_ID from PrimeKeys WHERE Mandanten_ID=" + Mandanten_ID + "; ";
                    }
                }
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
                LvsNr = Convert.ToDecimal(strTmp);
            }
            else
            {
                LvsNr = 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mySystem"></param>
        /// <returns></returns>
        public static string SQLStringNewLVSNr(clsSystem mySystem)
        {
            string strSQLReturn = string.Empty;
            if (mySystem.Client.Modul.PrimeyKey_LVSNRUseOneIDRange)
            {
                strSQLReturn += "( " +
                                       "CASE " +
                                           "WHEN ((SELECT MAX(Artikel.LVS_ID) FROM Artikel INNER JOIN LEingang ON LEingang.ID=Artikel.LEingangTableID ) IS NULL) THEN 1 " +
                                           "WHEN ((SELECT MAX(Artikel.LVS_ID) FROM Artikel INNER JOIN LEingang ON LEingang.ID=Artikel.LEingangTableID) = 0) THEN 1 " +
                                           "ELSE (SELECT MAX(Artikel.LVS_ID) FROM Artikel INNER JOIN LEingang ON LEingang.ID=Artikel.LEingangTableID )+1 " +
                                         "END " +
                                   ") ";
            }
            else
            {
                strSQLReturn += "( " +
                                   "CASE " +
                                       "WHEN ((SELECT MAX(Artikel.LVS_ID) FROM Artikel INNER JOIN LEingang ON LEingang.ID=Artikel.LEingangTableID WHERE LEingang.Mandant=" + (int)mySystem.AbBereich.MandantenID + " AND LEingang.AbBereich=" + (int)mySystem.AbBereich.ID + ") IS NULL) THEN 1 " +
                                       "WHEN ((SELECT MAX(Artikel.LVS_ID) FROM Artikel INNER JOIN LEingang ON LEingang.ID=Artikel.LEingangTableID WHERE LEingang.Mandant=" + (int)mySystem.AbBereich.MandantenID + " AND LEingang.AbBereich=" + (int)mySystem.AbBereich.ID + ") = 0) THEN 1 " +
                                   "ELSE (SELECT MAX(Artikel.LVS_ID) FROM Artikel INNER JOIN LEingang ON LEingang.ID=Artikel.LEingangTableID WHERE LEingang.Mandant=" + (int)mySystem.AbBereich.MandantenID + " AND LEingang.AbBereich=" + (int)mySystem.AbBereich.ID + ")+1 " +
                                   "END " +
                               ") ";
            }
            return strSQLReturn;
        }
        ///<summary>clsPrimeKeys / GetNEWRGNr</summary>
        ///<remarks>Ermittelt die Rechnungsnummer anhand der Mandanten ID.</remarks>
        public void GetNEWRGNrWOUpdate()
        {
            if (Mandanten_ID > 0)
            {
                string strSql = string.Empty;


                if (this.sys.Client.Modul.Fakt_GetRGGSNrFromTable_Primekey)
                {
                    strSql = "SELECT RGNr FROM PrimeKeys WHERE Mandanten_ID=" + Mandanten_ID + " AND AbBereichID=" + AbBereichID + " ; ";
                }
                if (this.sys.Client.Modul.Fakt_GetRGGSNrFromTable_Mandant)
                {
                    strSql = "SELECT RGNr FROM Mandanten WHERE ID=" + Mandanten_ID + "; ";
                }
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
                RGNr = Convert.ToDecimal(strTmp) + 1;
            }
            else
            {
                RGNr = 0;
            }
        }
        ///<summary>clsPrimeKeys / GetNEWRGNr</summary>
        ///<remarks>Ermittelt die RGNr anhand der Mandanten ID.
        ///         02.08.2016 mr -> Korrektur</remarks>
        ///<returns>Returns DataTable</returns>
        public void GetNEWRGNr()
        {
            if (Mandanten_ID > 0)
            {
                string strSql = string.Empty;
                if (this.sys.Client.Modul.Fakt_GetRGGSNrFromTable_Primekey)
                {
                    //strSql = "DECLARE @NewID table( NewAID decimal ); " +
                    //         "UPDATE PrimeKeys SET RGNr= RGNr + 1 " +
                    //         "OUTPUT INSERTED.RGNr INTO @NewID WHERE Mandanten_ID=" + this.sys.AbBereich.MandantenID + "; " +
                    //         "UPDATE Mandanten SET RGNr= (SELECT * FROM @NewId) WHERE ID=" + this.sys.AbBereich.MandantenID + "; " +
                    //         "SELECT * FROM @NewId;";

                    strSql = "DECLARE @NewID table( NewAID decimal ); " +
                             "UPDATE PrimeKeys SET RGNr = (CASE " +
                                                            "when (SELECT MAX(Rechnungen.RGNr) + 1 FROM Rechnungen WHERE MandantenID =" + this.sys.AbBereich.MandantenID + ") = RGNr + 1 " +
                                                            "then RGNr+1 " +
                                                            "else (SELECT MAX(Rechnungen.RGNr) + 1 FROM Rechnungen WHERE MandantenID = " + this.sys.AbBereich.MandantenID + ") " +
                                                            " end) " +
                             "OUTPUT INSERTED.RGNr INTO @NewID WHERE Mandanten_ID=" + this.sys.AbBereich.MandantenID + "; " +
                             "UPDATE Mandanten SET RGNr= (SELECT * FROM @NewId) WHERE ID=" + this.sys.AbBereich.MandantenID + "; " +
                             "SELECT * FROM @NewId;";

                }
                if (this.sys.Client.Modul.Fakt_GetRGGSNrFromTable_Mandant)
                {
                    //strSql = "DECLARE @NewID table( NewAID decimal ); " +
                    //         "UPDATE Mandanten SET RGNr= RGNr + 1 " +
                    //         "OUTPUT INSERTED.RGNr INTO @NewID WHERE ID=" + Mandanten_ID + "; " +
                    //         "UPDATE PrimeKeys SET RGNr=(SELECT * FROM @NewId) WHERE Mandanten_ID=" + this.sys.AbBereich.MandantenID + " ;" +
                    //    //" AND AbBereichID="+this.sys.AbBereich.ID+" ;" +
                    //         "SELECT * FROM @NewId;";

                    strSql = "DECLARE @NewID table( NewAID decimal ); " +
                             "UPDATE Mandanten SET RGNr = (CASE " +
                                                            "when ISNULL((SELECT MAX(Rechnungen.RGNr) + 1 FROM Rechnungen WHERE MandantenID =" + this.sys.AbBereich.MandantenID + "),0)=0 " +
                                                                "then Mandanten.RGNr + 1  " +
                                                            "when (SELECT MAX(Rechnungen.RGNr) + 1 FROM Rechnungen WHERE MandantenID =" + this.sys.AbBereich.MandantenID + ") = RGNr + 1 " +
                                                                "then RGNr+1 " +
                                                            "else (SELECT MAX(Rechnungen.RGNr) + 1 FROM Rechnungen WHERE MandantenID = " + this.sys.AbBereich.MandantenID + ") " +
                                                            " end) " +
                            " OUTPUT INSERTED.RGNr INTO @NewID WHERE Mandanten.ID=" + this.sys.AbBereich.MandantenID + ";" +
                            " UPDATE PrimeKeys SET RGNr = (SELECT * FROM @NewId) WHERE Mandanten_ID = " + this.sys.AbBereich.MandantenID + "; " +
                            " SELECT* FROM @NewId; ";
                }
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
                RGNr = Convert.ToDecimal(strTmp);
            }
            else
            {
                RGNr = 0;
            }
        }
        ///<summary>clsPrimeKeys / GetNEWGSNr</summary>
        ///<remarks>Ermittelt die Gutschriftsnummer anhand der Mandanten ID.</remarks>
        ///<returns>Returns DataTable</returns>
        public void GetNEWGSNr()
        {
            if (Mandanten_ID > 0)
            {
                string strSql = string.Empty;
                if (this.sys != null)
                {
                    if (this.sys.Client.Modul.Fakt_GetRGGSNrFromTable_Primekey)
                    {
                        strSql = "DECLARE @NewID table( NewAID decimal ); " +
                                 "UPDATE PrimeKeys SET GSNr= GSNr + 1 " +
                                 "OUTPUT INSERTED.GSNr INTO @NewID WHERE Mandanten_ID=" + +this.sys.AbBereich.MandantenID + " ; " +
                                 "UPDATE Mandanten SET GSNr= (SELECT * FROM @NewId) WHERE ID=" + this.sys.AbBereich.MandantenID + " ; " +
                                 "SELECT * FROM @NewId;";
                    }
                    if (this.sys.Client.Modul.Fakt_GetRGGSNrFromTable_Mandant)
                    {

                        strSql = "DECLARE @NewID table( NewAID decimal ); " +
                                 "UPDATE Mandanten SET GSNr= GSNr + 1 " +
                                 "OUTPUT INSERTED.GSNr INTO @NewID WHERE ID=" + this.sys.AbBereich.MandantenID + "; " +
                                 "UPDATE PrimeKeys SET GSNr=(SELECT * FROM @NewId) WHERE Mandanten_ID=" + this.sys.AbBereich.MandantenID + " ;" +
                                 //" AND AbBereichID=" + this.sys.AbBereich.ID + " ;" +
                                 "SELECT * FROM @NewId;";
                    }
                }
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
                //RG und GS aus einem RG-Kreis
                if (this.sys.Client.Modul.Fakt_UseOneRGNrKreisForRGandGS)
                {
                    this.GetNEWRGNr();
                    GSNr = this.RGNr;
                }
                else
                {
                    GSNr = Convert.ToDecimal(strTmp);
                }
            }
            else
            {
                GSNr = 0;
            }
        }
        ///<summary>clsPrimeKeys / GetNEWRGNr</summary>
        ///<remarks>Ermittelt die Rechnungsnummer anhand der Mandanten ID.</remarks>
        public void GetNEWGSNrWOUpdate()
        {
            if (Mandanten_ID > 0)
            {
                string strSql = string.Empty;
                if (this.sys.Client.Modul.Fakt_GetRGGSNrFromTable_Primekey)
                {
                    strSql = "SELECT GSNr FROM PrimeKeys WHERE Mandanten_ID=" + Mandanten_ID + " AND AbBereichID=" + AbBereichID + "; ";
                }
                if (this.sys.Client.Modul.Fakt_GetRGGSNrFromTable_Mandant)
                {
                    strSql = "SELECT GSNr FROM Mandanten WHERE ID=" + Mandanten_ID + "; ";
                }
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
                //RG und GS aus einem RG-Kreis
                if (this.sys.Client.Modul.Fakt_UseOneRGNrKreisForRGandGS)
                {
                    this.GetNEWRGNrWOUpdate();
                    GSNr = this.RGNr;
                }
                else
                {
                    GSNr = Convert.ToDecimal(strTmp) + 1;
                }
            }
            else
            {
                GSNr = 0;
            }
        }
        ///<summary>clsPrimeKeys / GetNEWLEingnagID</summary>
        ///<remarks>Ermittelt die nächste Lager EingangsID anhand der Mandanten ID.</remarks>
        public void GetNEWLEingnagID()
        {
            if (Mandanten_ID > 0)
            {
                string strSql = string.Empty;
                /****
                strSql = "DECLARE @NewID table( NewAID decimal ); " +
                         "UPDATE PrimeKeys SET LEingangID= LEingangID + 1 " +
                         "OUTPUT INSERTED.LEingangID INTO @NewID WHERE Mandanten_ID=" + Mandanten_ID + "; " +
                         "SELECT * FROM @NewId;";
                *****/
                // ÄNDERUNG                
                /**
                strSql = "DECLARE @NewID table( NewAID decimal ); " +
                         "UPDATE PrimeKeys SET " +
                                "LEingangID = (CASE " +
                                                "WHEN (LEingangID = (SELECT MAX(LEingangID)FROM LEingang WHERE Mandant=" + Mandanten_ID + ")) "+ 
				                                "THEN LEingangID + 1 "+
                                                "ELSE(SELECT MAX(LEingangID)FROM LEingang WHERE Mandant=" + Mandanten_ID + ")+1 " +
                                              "END) " +
                        "OUTPUT INSERTED.LEingangID INTO @NewID WHERE Mandanten_ID=" + Mandanten_ID + "; " +
                        "SELECT * FROM @NewId; "; 
                 */
                strSql = GetNEWLEingangIDSQL(Mandanten_ID, AbBereichID, 0);
                strSql += GetNEWLEingangIDSQL(Mandanten_ID, AbBereichID, 1);
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
                decimal decTmp = 0;
                Decimal.TryParse(strTmp, out decTmp);
                LEingangID = decTmp;
            }
            else
            {
                LEingangID = 0;
            }
        }
        ///<summary>clsPrimeKeys / GetNEWLAusgangID</summary>
        ///<remarks>Ermittelt die nächste Lager AUsgangsID anhand der Mandanten ID.</remarks>
        public void GetNEWLAusgangID()
        {
            if (Mandanten_ID > 0)
            {
                string strSql = string.Empty;
                strSql = "DECLARE @NewID table( NewAID decimal ); " +
                         "UPDATE PrimeKeys SET " +
                                           "LAusgangID = (CASE " +
                                                            "WHEN (LAusgangID = (SELECT MAX(LAusgangID) FROM LAusgang WHERE MandantenID=" + Mandanten_ID + " AND AbBereich=" + AbBereichID + ")) " +
                                                            "THEN LAusgangID + 1 " +
                                                            "ELSE (SELECT MAX(LAusgangID) FROM LAusgang WHERE MandantenID=" + Mandanten_ID + " AND AbBereich=" + AbBereichID + ")+1 " +
                                                        "END) " +
                        "OUTPUT INSERTED.LAusgangID INTO @NewID WHERE Mandanten_ID=" + Mandanten_ID + " AND AbBereichID=" + AbBereichID + "; " +
                        "SELECT * FROM @NewId; ";

                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
                decimal decTmp = 0;
                Decimal.TryParse(strTmp, out decTmp);
                if (decTmp == 0)
                {
                    decTmp = 1;
                }
                LAusgangID = decTmp;
            }
            else
            {
                LAusgangID = 0;
            }
        }
        ///<summary>clsPrimeKeys / GetNEWLAusgangIDSQL</summary>
        ///<remarks>Ermittelt die nächste Lager AUsgangsID anhand der Mandanten ID.</remarks>
        internal static string GetNEWLAusgangIDSQL(decimal _Mandanten_ID, decimal myAbBereichID, int part)
        {
            string strSql = string.Empty;
            if (_Mandanten_ID > 0)
            {
                switch (part)
                {
                    case 0:
                        strSql = "DECLARE @AusgangID table( NewAusgangID decimal ); " +
                                "UPDATE PrimeKeys SET " +
                                                  "LAusgangID = (CASE " +
                                                                   "WHEN ISNULL((SELECT MAX(LAusgangID) FROM LAusgang WHERE MandantenID=" + _Mandanten_ID + " AND AbBereich=" + myAbBereichID + "),0)=0 THEN LAusgangID " +
                                                                   //"WHEN ISNULL((SELECT MAX(LAusgangID) FROM LAusgang WHERE MandantenID=1),0)>0  THEN LAusgangID + 1 "+
                                                                   "ELSE (SELECT MAX(LAusgangID)FROM LAusgang WHERE MandantenID=" + _Mandanten_ID + " AND AbBereich=" + myAbBereichID + ")+1 " +
                                                               "END) " +
                               "OUTPUT INSERTED.LAusgangID INTO @AusgangID WHERE Mandanten_ID=" + _Mandanten_ID + " AND AbBereichID=" + myAbBereichID + "; ";
                        break;
                    case 1:
                        strSql = "SELECT * FROM @AusgangID ";
                        break;
                }
            }
            return strSql;
        }
        ///<summary>clsPrimeKeys / GetNEWLEingangIDSQL</summary>
        ///<remarks>Ermittelt die nächste Lager EingangsID anhand der Mandanten ID.</remarks>
        public static string GetNEWLEingangIDSQL(decimal _Mandanten_ID, decimal myAbBereichID, int part, bool save = true)
        {
            string strSql = string.Empty;
            if (_Mandanten_ID > 0)
            {
                switch (part)
                {
                    case 0:
                        strSql = "DECLARE @EingangID table( NewEingangID decimal ); " +
                             "UPDATE PrimeKeys SET " +
                                    "LEingangID = (" +
                                                   "CASE " +
                                                        "WHEN ((SELECT MAX(LEingangID) FROM LEingang WHERE Mandant=" + _Mandanten_ID + " AND AbBereich=" + myAbBereichID + ") IS NULL) THEN 1 " +
                                                        "ELSE " +
                                                            "CASE " +
                                                               "WHEN (LEingangID = (SELECT MAX(LEingangID) FROM LEingang WHERE Mandant=" + _Mandanten_ID + " AND AbBereich=" + myAbBereichID + ")) " +
                                                               "THEN LEingangID + 1 " +
                                                               "ELSE(SELECT MAX(LEingangID) FROM LEingang WHERE Mandant=" + _Mandanten_ID + " AND AbBereich=" + myAbBereichID + ")+1 " +
                                                            "END " +
                                                        "END " +
                                                    ") " +
                            "OUTPUT INSERTED.LEingangID INTO @EingangID WHERE Mandanten_ID=" + _Mandanten_ID + " AND AbBereichID=" + myAbBereichID + "; ";
                        break;
                    case 1:
                        strSql = "SELECT * FROM @EingangID ";
                        break;

                    case 2:
                        strSql = "Select(SELECT MAX(LEingangID) FROM LEingang WHERE Mandant=" + _Mandanten_ID + " AND AbBereich=" + myAbBereichID + ")+1 as LEingangID;";
                        break;
                }
            }
            return strSql;
        }
    }
}
