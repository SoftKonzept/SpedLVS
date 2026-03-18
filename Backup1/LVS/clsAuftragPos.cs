using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
//using System.Windows.Forms;

namespace LVS
{
    public class clsAuftragPos
    {
        public Globals._GL_USER _GL_User;
        public Globals._GL_SYSTEM _GL_System;
        public clsSystem Sys;

        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get { return _BenutzerID; }
            set { _BenutzerID = value; }
        }
        //************************************


        public clsArtikel Artikel;
        public DataTable dtAuftrPosArtikel;
        public bool Prioritaet { get; set; }

        public decimal ID { get; set; }
        public decimal AuftragPos { get; set; }
        public decimal AuftragTableID { get; set; }
        public decimal Auftrag_ID { get; set; }
        public DateTime LieferTermin { get; set; }
        public DateTime LieferZF { get; set; }
        public DateTime VSB { get; set; }
        public Int32 Status { get; set; }
        public string Ladenummer { get; set; }
        public decimal BruttoSumme { get; set; }
        public decimal NettoSumme { get; set; }
        public decimal AnzahlArtikel { get; set; }
        public string Notiz { get; set; }
        public bool LadeNrRequire { get; set; }
        public bool LieferZFRequire { get; set; }
        public Int32 vKW { get; set; }
        public Int32 bKW { get; set; }
        public DateTime LadeTermin { get; set; }
        public DateTime LadeZF { get; set; }
        public bool LadeZFRequire { get; set; }

        public bool ExistAuftragPos { get; set; }
        private decimal _NextAuftragPos;
        public decimal NextAufragPos
        {
            get
            {
                GetNextAuftPosNr();
                return _NextAuftragPos;
            }
            set { _NextAuftragPos = value; }
        }
        private decimal _MinAuftragPosID;  // ID der Auftragposition der niedrigsten AuftragPos mit Status<3
        public decimal MinAuftragPosID
        {
            get
            {
                GetMinAuftragPosID();
                return _MinAuftragPosID;
            }
            set { _MinAuftragPosID = value; }
        }
        private bool _StatusOK;
        public bool StatusOK
        {
            get
            {
                _StatusOK = CheckStatus();
                return _StatusOK;
            }
            set { _StatusOK = value; }
        }
        public string StatusInfo { get; set; }
        private bool CheckStatus()
        {
            StatusInfo = string.Empty;
            string strHelp = string.Empty;
            bool bCheckOK = true;

            clsAuftrag CheckAuftrag = new clsAuftrag();
            CheckAuftrag.InitClass(this._GL_User, this._GL_System, this.Sys);
            CheckAuftrag.ID = this.AuftragTableID;
            CheckAuftrag.Fill();

            //Auftraggeber
            if (CheckAuftrag.KD_ID == 0)
            {
                strHelp = strHelp + "- Auftraggeber \t -> NICHT OK !" + Environment.NewLine;
                bCheckOK = false;
            }
            else
            {
                strHelp = strHelp + "- Auftraggeber \t -> ok" + Environment.NewLine;
            }
            //Beladeadress
            if (CheckAuftrag.B_ID == 0)
            {
                strHelp = strHelp + "- Beladestelle \t -> NICHT OK !" + Environment.NewLine;
                bCheckOK = false;
            }
            else
            {
                strHelp = strHelp + "- Beladestelle \t -> ok !" + Environment.NewLine;
            }
            //Entladestelle
            if (CheckAuftrag.E_ID == 0)
            {
                strHelp = strHelp + "- Entladestelle \t -> NICHT OK !" + Environment.NewLine;
                bCheckOK = false;
            }
            else
            {
                strHelp = strHelp + "- Entladestelle \t -> ok !" + Environment.NewLine;
            }
            //Artikelanzahl der Position >0
            if (this.dtAuftrPosArtikel.Rows.Count > 0)
            {
                strHelp = strHelp + "- Artikelanzahl > 0 \t -> ok !" + Environment.NewLine;
            }
            else
            {
                strHelp = strHelp + "- Artikelanzahl > 0 \t -> NICHT OK !" + Environment.NewLine;
                bCheckOK = false;
            }
            //VSB
            if ((this.VSB.Date != Globals.DefaultDateTimeMinValue.Date) && (this.VSB.Date != Globals.DefaultDateTimeMaxValue.Date))
            {
                strHelp = strHelp + "- VSB-Datum \t -> ok !" + Environment.NewLine;
            }
            else
            {
                strHelp = strHelp + "- VSB-Datum  > 0 \t -> NICHT OK !" + Environment.NewLine;
                bCheckOK = false;
            }
            //Ladetermin
            if ((this.LadeTermin.Date != Globals.DefaultDateTimeMinValue.Date) && (this.LadeTermin.Date != Globals.DefaultDateTimeMaxValue.Date))
            {
                strHelp = strHelp + "- Lade-Datum \t -> ok !" + Environment.NewLine;
            }
            else
            {
                strHelp = strHelp + "- Lade-Datum  > 0 \t -> NICHT OK !" + Environment.NewLine;
                bCheckOK = false;
            }
            //Ladezeitfenster
            if (this.LadeZFRequire)
            {
                if ((this.LadeZF != Globals.DefaultDateTimeMinValue))
                {
                    strHelp = strHelp + "- Lade- ZF \t -> ok !" + Environment.NewLine;
                }
                else
                {
                    strHelp = strHelp + "- Lade- ZF  > 0 \t -> NICHT OK !" + Environment.NewLine;
                    bCheckOK = false;
                }
            }
            //Liefertermin
            if ((this.LieferTermin.Date != Globals.DefaultDateTimeMinValue.Date) && (this.LieferTermin.Date != Globals.DefaultDateTimeMaxValue.Date))
            {
                strHelp = strHelp + "- Liefer-Datum \t -> ok !" + Environment.NewLine;
            }
            else
            {
                strHelp = strHelp + "- Liefer-Datum  > 0 \t -> NICHT OK !" + Environment.NewLine;
                bCheckOK = false;
            }
            //Ladezeitfenster
            if (this.LieferZFRequire)
            {
                if ((this.LieferZF != Globals.DefaultDateTimeMinValue))
                {
                    strHelp = strHelp + "- Liefer- ZF \t -> ok !" + Environment.NewLine;
                }
                else
                {
                    strHelp = strHelp + "- Liefer- ZF  > 0 \t -> NICHT OK !" + Environment.NewLine;
                    bCheckOK = false;
                }
            }
            StatusInfo = "STATUS: " + Environment.NewLine + strHelp;
            return bCheckOK;
        }

        /*************************************************************************
         *                          Methoden / Procedure
         * **********************************************************************/
        ///<summary>clsAuftragPos / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSystem, clsSystem mySys)
        {
            this._GL_User = myGLUser;
            this._GL_System = myGLSystem;
            this.Sys = mySys;

            dtAuftrPosArtikel = new DataTable();
            dtAuftrPosArtikel = clsArtikel.GetDataTableForArtikelGrd(this._GL_User, this.ID);

            Artikel = new clsArtikel();
            Artikel.InitClass(this._GL_User, this._GL_System);
        }
        ///<summary>clsAuftragPos / Copy</summary>
        ///<remarks></remarks>
        public clsAuftragPos Copy()
        {
            return (clsAuftragPos)this.MemberwiseClone();
        }
        ///<summary>clsAuftragPos / FillByAuftragID</summary>
        ///<remarks></remarks>
        public void FillByAuftragID()
        {
            string strSQL = string.Empty;
            DataTable dt = new DataTable("Auftrag");
            dt.Clear();
            strSQL = "SELECT * FROM AuftragPos WHERE AuftragTableID=" + this.AuftragTableID + " ";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Auftrag");
            FillClass(ref dt);
        }
        ///<summary>clsAuftragPos / SetAuftragPosClassDatenbyAuftragPosTableID</summary>
        ///<remarks>Ermittel anhand der AuftragPosTableID alle Datensatz und setzt die entsprechenden Werte
        ///         in die Klasse.</remarks>
        public void Fill()
        {
            string strSQL = string.Empty;
            DataTable dt = new DataTable("Auftrag");
            dt.Clear();
            strSQL = "SELECT * FROM AuftragPos WHERE ID=" + ID + " ";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Auftrag");
            FillClass(ref dt);
        }
        ///<summary>clsAuftragPos / FillClass</summary>
        ///<remarks></remarks>
        private void FillClass(ref DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    DateTime dateTmp = Globals.DefaultDateTimeMinValue;

                    ID = (decimal)dt.Rows[i]["ID"];
                    AuftragTableID = (decimal)dt.Rows[i]["AuftragTableID"];
                    Auftrag_ID = (decimal)dt.Rows[i]["Auftrag_ID"];
                    AuftragPos = (decimal)dt.Rows[i]["AuftragPos"];
                    LieferTermin = (DateTime)dt.Rows[i]["LieferTermin"];
                    LieferZF = (DateTime)dt.Rows[i]["LieferZF"];
                    VSB = (DateTime)dt.Rows[i]["VSB"];
                    Status = (Int32)dt.Rows[i]["Status"];
                    Ladenummer = dt.Rows[i]["Ladenummer"].ToString();
                    Notiz = dt.Rows[i]["Notiz"].ToString();
                    LadeNrRequire = (bool)dt.Rows[i]["LadeNrRequire"];
                    Prioritaet = (bool)dt.Rows[i]["Prioritaet"];
                    LieferZFRequire = (bool)dt.Rows[i]["LieferZFRequire"];
                    vKW = (Int32)dt.Rows[i]["vKW"];
                    bKW = (Int32)dt.Rows[i]["bKW"];
                    dateTmp = Globals.DefaultDateTimeMinValue;
                    DateTime.TryParse(dt.Rows[i]["LadeTermin"].ToString(), out dateTmp);
                    LadeTermin = dateTmp;
                    dateTmp = Globals.DefaultDateTimeMinValue;
                    DateTime.TryParse(dt.Rows[i]["LadeZF"].ToString(), out dateTmp);
                    LadeZF = dateTmp;
                    LadeZFRequire = (bool)dt.Rows[i]["LadeZFRequire"];
                    ExistAuftragPos = true;

                    dtAuftrPosArtikel = new DataTable();
                    dtAuftrPosArtikel = clsArtikel.GetDataTableForArtikelGrd(this._GL_User, this.ID);
                    Int32 iAnzahl = 0;
                    decimal decBrutto = 0;
                    decimal decNetto = 0;
                    if (dtAuftrPosArtikel.Rows.Count > 0)
                    {
                        iAnzahl = dtAuftrPosArtikel.Rows.Count;
                        object objBrutto;
                        objBrutto = dtAuftrPosArtikel.Compute("SUM(Brutto)", "");
                        Decimal.TryParse(objBrutto.ToString(), out decBrutto);
                        object objbNetto;
                        objbNetto = dtAuftrPosArtikel.Compute("SUM(Netto)", "");
                        Decimal.TryParse(objbNetto.ToString(), out decNetto);
                    }
                    AnzahlArtikel = iAnzahl;
                    NettoSumme = decNetto;
                    BruttoSumme = decBrutto;
                }
            }
        }
        ///<summary>clsAuftragPos / GetSQLAdd</summary>
        ///<remarks></remarks>
        private string GetSQLAdd()
        {
            string strSql = "INSERT INTO AuftragPos (" +
                                              "AuftragPos" +
                                              ", Auftrag_ID" +
                                              ", LieferTermin " +
                                              ", LieferZF" +
                                              ", VSB" +
                                              ", Status" +
                                              ", Ladenummer" +
                                              ", LadeNrRequire " +
                                              ", Notiz" +
                                              ", Prioritaet" +
                                              ", LieferZFRequire" +
                                              ", vKW" +
                                              ", bKW " +
                                              ", AuftragTableID" +
                                              ", LadeTermin" +
                                              ", LadeZF" +
                                              ", LadeZFRequire" +
                                              ") " +
                                      "VALUES (" + (Int32)AuftragPos +
                                             ", " + (Int32)Auftrag_ID +
                                             ", '" + LieferTermin + "'" +
                                             ", '" + LieferZF + "'" +
                                             ", '" + VSB + "'" +
                                             ", " + Status +
                                             ", '" + Ladenummer + "'" +
                                             ", " + Convert.ToInt32(LadeNrRequire) +
                                             ", '" + Notiz + "'" +
                                             ", " + Convert.ToInt32(Prioritaet) +
                                             ", " + Convert.ToInt32(LieferZFRequire) +
                                             ", " + vKW +
                                             ", " + bKW +
                                             ", " + AuftragTableID +
                                             ", '" + LadeTermin + "'" +
                                             ", '" + LadeZF + "'" +
                                             ", " + Convert.ToInt32(LadeZFRequire) +
                                             ") ; ";
            strSql = strSql + "Select @@IDENTITY as 'ID'; ";
            return strSql;
        }
        ///<summary>clsAuftragPos / Add</summary>
        ///<remarks>Ermittel anhand der AuftragPosTableID alle Datensatz und setzt die entsprechenden Werte
        ///         in die Klasse.</remarks>
        public void Add(bool AusAuftragSplitting)
        {
            DateTime dtPapiere = clsSystem.const_DefaultDateTimeValue_Min;

            string strSql = GetSQLAdd();
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0;
            Decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                this.ID = decTmp;
                this.Status = 1; //fehlende Angaben
                if (this.CheckStatus())
                {
                    this.Status = 2; // komplette Angaben
                }
                this.Update();
                Fill();
                //Add Logbucheintrag Eintrag
                if (AusAuftragSplitting)
                {
                    string Beschreibung = "Auftragsplitting - Auftrag ID:" + Auftrag_ID + " - Auftragsposition: " + AuftragPos + " hinzugefügt";
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Auftragsplitting.ToString(), Beschreibung);
                }
                else
                {
                    string Beschreibung = "Auftrag - Auftrag ID:" + Auftrag_ID + " - Auftragsposition: " + AuftragPos + " hinzugefügt";
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), Beschreibung);
                }
            }
        }
        ///<summary>clsAuftragPos / GetNextAuftPosNr</summary>
        ///<remarks>Ermittelt die nächste frei AuftragPosNr</remarks>
        private void GetNextAuftPosNr()
        {
            string strSQL = "SELECT MAX(a.AuftragPos)+1 FROM AuftragPos a " +
                                                    "INNER JOIN Auftrag b ON b.ID = a.AuftragTableID " +
                                                    "WHERE " +
                                                            "b.ID = " + AuftragTableID;
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            decimal decTmp = -1;
            Decimal.TryParse(strTmp, out decTmp);
            this._NextAuftragPos = decTmp;
        }
        ///<summary>clsAuftragPos / GetNextAuftPosNr</summary>
        ///<remarks>Ermittelt die nächste frei AuftragPosNr</remarks>
        private void GetMinAuftragPosID()
        {
            string strSQL = "SELECT TOP(1) a.ID FROM AuftragPos a " +
                                                    "INNER JOIN Auftrag b ON b.ID = a.AuftragTableID " +
                                                    "WHERE " +
                                                            "b.ID = " + this.AuftragTableID +
                                                            " AND a.Status<3 " +
                                                            " Order BY a.AuftragPos";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            decimal decTmp = -1;
            Decimal.TryParse(strTmp, out decTmp);
            this._MinAuftragPosID = decTmp;
        }
        ///<summary>clsAuftragPos / DoAuftragPosSplitt</summary>
        ///<remarks></remarks>
        public void DoAuftragPosSplitt(List<decimal> listArtikelToSplit)
        {
            //SQLSTatement Add new AuftragPos
            string strSQL = GetSQLAdd();
            //ArtikelUpdate
            strSQL = strSQL + "Update Artikel SET " +
                                            "AuftragPosTableID=(Select @@IDENTITY) " +
                                            "WHERE ID IN(" + string.Join(",", listArtikelToSplit.ToArray()) + ");";
            if (clsSQLcon.ExecuteSQLWithTRANSACTION(strSQL, "AuftragPosSplit", BenutzerID))
            {
                //BAustelle
                //Logbucheintrag
            }
        }
        ///<summary>clsAuftragPos / DoCancelAuftragPos</summary>
        ///<remarks></remarks>
        public void DoCancelAuftragPos(List<decimal> listArtikelToSplit)
        {
            string strSQL = string.Empty;
            //UPdate Artikel
            strSQL = clsArtikel.GetSQLUpdateArtikelFieldAuftragPosTableID(listArtikelToSplit, this.MinAuftragPosID);
            //Delete AuftragPos
            strSQL = strSQL + GetSQLDeleteByID(this.ID);
            //Vorgang durchführen
            if (clsSQLcon.ExecuteSQLWithTRANSACTION(strSQL, "CancelAuftragPos", BenutzerID))
            {
                //BAustelle
                //Logbucheintrag

                //Instanz auf die MinAuftragPos setzen
                this.ID = this.MinAuftragPosID;
                this.Fill();
            }
        }
        ///<summary>clsAuftragPos / updateAuftragPos</summary>
        ///<remarks></remarks>
        public void Update()
        {
            string strSql = "Update AuftragPos SET " +
                                                "AuftragPos=" + (Int32)AuftragPos +
                                                ", LieferTermin='" + LieferTermin + "'" +
                                                ", LieferZF='" + LieferZF + "'" +
                                                ", VSB='" + VSB + "'" +
                                                ", Status=" + Status +
                                                ", Ladenummer='" + Ladenummer + "' " +
                                                ", Notiz ='" + Notiz + "'" +
                                                ", LadeNrRequire=" + Convert.ToInt32(LadeNrRequire) +
                                                ", Prioritaet=" + Convert.ToInt32(Prioritaet) +
                                                ", LieferZFRequire=" + Convert.ToInt32(LieferZFRequire) +
                                                ", vKW=" + vKW +
                                                ", bKW=" + bKW +
                                                ", LadeTermin='" + LadeTermin + "'" +
                                                ", LadeZF='" + LadeZF + "'" +
                                                ", LadeZFRequire=" + Convert.ToInt32(LadeZFRequire) +

                                                " WHERE ID=" + ID;

            bool bOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            if (bOK)
            {
                this.Fill();
                //Add Logbucheintrag update
                string Beschreibung = "Auftrag - Auftrag ID:" + Auftrag_ID + " - Auftragsposition: " + AuftragPos + " geändert";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
            }
        }
        /////<summary>clsAuftragPos / updateAuftragPos</summary>
        /////<remarks></remarks>
        //public void updateAuftragPos()
        //{
        //    string strSQL = "Update AuftragPos SET " +
        //                                            "AuftragPos=" + AuftragPos +
        //                                            ", T_Date='" + T_Date + "'" +
        //                                            ", ZF='" + ZF + "'" +
        //                                            ", VSB='" + VSB + "'" +
        //                                            ", Status=" + Status +
        //                                            ", Ladenummer='" + Ladenummer + "'" +
        //                                            ", Notiz ='" + Notiz + "'" +
        //                                            ", LadeNrRequire= " + Convert.ToInt32(LadeNrRequire) +
        //                                            ", Prioritaet='" + Prioritaet + "'" +
        //                                            ", ZFRequire=" + Convert.ToInt32(ZFRequire) +
        //                                            ", vKW=" + vKW +
        //                                            ", bKW=" + bKW +

        //                                            " WHERE ID=" + ID + ";";
        //    bool bOK = clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
        //    if (bOK)
        //    {
        //        //Add Logbucheintrag update
        //        string Beschreibung = "Auftrag - Auftrag ID:" + Auftrag_ID + " - Auftragsposition: " + AuftragPos + " geändert";
        //        Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
        //    }
        //}
        ///<summary>clsAuftragPos / Delete</summary>
        ///<remarks></remarks>
        public void Delete()
        {
            if (ExistAuftragPos)
            {

                //Add Logbucheintrag Löschen
                string Beschreibung = "Auftragsplitting - ID: " + ID + " - Auftrag ID: " + Auftrag_ID + " - Auftragsposition: " + AuftragPos + " aufgelöst";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), Beschreibung);

                string strSQL = string.Empty;
                strSQL = "DELETE FROM AuftragPos WHERE ID=" + ID + ";";
                clsSQLcon.ExecuteSQL_GetValueBool(strSQL, BenutzerID);
            }
        }




        /***************************************************************************************
         *                              public static
         * ************************************************************************************/
        ///<summary>clsAuftragPos / DeleteAuftragPos</summary>
        ///<remarks></remarks>
        public static void DeleteAuftragPos(decimal AuftragID, decimal AuftragPos, decimal decBenutzerID)
        {
            //Add Logbucheintrag Löschen
            string Beschreibung = "automtisches LÖSCHEN nach 4 Wochen - Auftrag ID: " + AuftragID + " - Auftragsposition: " + AuftragPos + "";
            Functions.AddLogbuch(decBenutzerID, enumLogbuchAktion.Loeschung.ToString(), Beschreibung);

            string strSql = string.Empty;
            strSql = "DELETE FROM AuftragPos WHERE Auftrag_ID='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "' AND Status=3";
            clsSQLcon.ExecuteSQL(strSql, decBenutzerID);

        }
        ///<summary>clsAuftragPos / DeleteAuftragPos</summary>
        ///<remarks></remarks>
        public static string GetSQLDeleteByID(decimal myAuftragPosTableID)
        {
            ////Add Logbucheintrag Löschen
            //string Beschreibung = "automtisches LÖSCHEN nach 4 Wochen - Auftrag ID: " + AuftragID + " - Auftragsposition: " + AuftragPos + "";
            //Functions.AddLogbuch(decBenutzerID, Globals.enumLogbuchAktion.Loeschung.ToString(), Beschreibung);

            string strSql = string.Empty;
            strSql = "DELETE FROM AuftragPos WHERE ID=" + myAuftragPosTableID + " ;";
            return strSql;
        }
        ///<summary>clsAuftragPos / IsAuftragPosAuftragIn</summary>
        ///<remarks></remarks>
        public static bool IsAuftragPosAuftragIn(decimal decAuftrag, decimal decBenutzerID)
        {
            bool IsIn = false;
            string strSql = string.Empty;
            strSql = "Select ID FROM AuftragPos WHERE Auftrag_ID='" + decAuftrag + "'";
            IsIn = clsSQLcon.ExecuteSQL_GetValueBool(strSql, decBenutzerID);
            return IsIn;
        }
        ///<summary>clsAuftragPos / GetStatusByAuftragPosTableID</summary>
        ///<remarks></remarks>
        public static Int32 GetStatusByAuftragPosTableID(Globals._GL_USER myGLUser, decimal myAuftragPosTableID)
        {
            Int32 reVal = 0;
            if (IsAuftragPosInByID(myGLUser, myAuftragPosTableID))
            {
                string strSQL = string.Empty;
                strSQL = "Select Status FROM AuftragPos WHERE ID ='" + myAuftragPosTableID + "'; ";
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, myGLUser.User_ID);
                Int32 iTmp = 0;
                Int32.TryParse(strTmp, out iTmp);
                reVal = iTmp;
            }
            return reVal;
        }

        ///<summary>clsAuftragPos / updateTDatInAuftragPosition</summary>
        ///<remarks></remarks>
        public static void updateTDatInAuftragPosition(decimal AP_ID, DateTime T_Date, decimal BenutzerID, bool FVergabe)
        {
            string strSQL = "Update AuftragPos SET  T_Date='" + T_Date + "' " +
                                                                    "WHERE ID=" + AP_ID + ";";

            bool bOK = clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
            if (bOK)
            {
                string Beschreibung = string.Empty;
                //Add Logbucheintrag update
                if (FVergabe)
                {
                    Beschreibung = "AuftragPos ID:" + AP_ID + " - Liefertermin auf " + T_Date.ToShortDateString() + "nach Frachtvergabe geändert";
                }
                else
                {
                    Beschreibung = "AuftragPos ID:" + AP_ID + " - Liefertermin auf " + T_Date.ToShortDateString() + " geändert";
                }
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
            }
        }
        ///<summary>clsAuftragPos / ReadDataByID</summary>
        ///<remarks>Get Auftragsdaten aus DB über Auftragsnummer und Auftragsposition</remarks>
        public static DataSet ReadDataByID(decimal AuftragID, decimal Pos)
        {
            DataSet ds = new DataSet();
            ds.Clear();
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();

            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT " +
                                              "Auftrag.ID as AuftragTableID ," +
                                              "Auftrag.ANr, " +
                                              "AuftragPos.ID as AuftragPosTableID, " +
                                              "AuftragPos.AuftragPos," +
                                              "Auftrag.ADate," +
                                              "(Select Top(1)Artikel.GArt From Artikel WHERE Artikel.AuftragID=Auftrag.ANr) as 'Gut', " +
                                              "(SELECT ADR.ID FROM ADR WHERE ADR.ID=Auftrag.KD_ID) as 'KD_ID'," +
                                              "(SELECT ADR.Name1 FROM ADR WHERE ADR.ID=Auftrag.KD_ID) as 'Auftraggeber'," +

                                              "(SELECT ADR.ID FROM ADR WHERE ADR.ID=Auftrag.B_ID) as 'B_ID'," +
                                              "(Select ADR.Name1 FROM ADR WHERE ADR.ID=Auftrag.B_ID) as 'Beladestelle', " +
                                              "(Select ADR.PLZ FROM ADR WHERE ADR.ID= Auftrag.B_ID) as 'B_PLZ', " +
                                              "(Select ADR.Ort FROM ADR WHERE ADR.ID=Auftrag.B_ID) as 'B_Ort', " +

                                              "(SELECT ADR.ID FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'E_ID'," +
                                              "(Select ADR.Name1 FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'Entladestelle', " +
                                              "(Select ADR.PLZ FROM ADR WHERE ADR.ID= Auftrag.E_ID) as 'E_PLZ', " +
                                              "(Select ADR.Ort FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'E_Ort', " +

                                              "AuftragPos.T_Date as 'Liefertermin', " +
                                              "AuftragPos.ZF as 'ZF', " +
                                              "AuftragPos.VSB, " +
                                              "(Select SUM(Netto) FROM Artikel WHERE AuftragID=Auftrag.ANr AND AuftragPos=AuftragPos.AuftragPos) as 'Netto', " +
                                              "(Select SUM(Brutto) FROM Artikel WHERE AuftragID=Auftrag.ANr AND AuftragPos=AuftragPos.AuftragPos) as 'Brutto', " +
                                              "AuftragPos.Ladenummer, " +
                                              "AuftragPos.Status " +

                                              "FROM Auftrag " +
                                              "INNER JOIN AuftragPos ON Auftrag.ANr = AuftragPos.Auftrag_ID " +
                                              "WHERE AuftragPos.AuftragPos='" + Pos + "' " +
                                              "AND AuftragPos.Auftrag_ID='" + AuftragID + "'";

            ada.Fill(ds);
            Command.Dispose();
            ada.Dispose();
            Globals.SQLcon.Close();
            if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
            {
                Command.Connection.Close();
            }
            return ds;
        }
        ///<summary>clsAuftragPos / IsAuftragPosInByID</summary>
        ///<remarks></remarks>
        public static bool IsAuftragPosInByID(Globals._GL_USER myGLUser, decimal myAuftragPosTableID)
        {
            string strSQL = string.Empty;
            strSQL = "Select ID FROM AuftragPos WHERE ID='" + myAuftragPosTableID + "' ;";
            return clsSQLcon.ExecuteSQL_GetValueBool(strSQL, myGLUser.User_ID);
        }
        ///<summary>clsAuftragPos / IsAuftragPosInByID</summary>
        ///<remarks>Auftragsdaten für die Frachtvergabe an SU</remarks>
        public static DataTable ReadDataByAuftragIDandAuftragPos(decimal AuftragID, decimal Pos)
        {
            DataTable dt = new DataTable("Auftrag");
            dt.Clear();
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();

            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT " +
                                              "AuftragPos.Auftrag_ID as 'Auftrag_ID', " +
                                              "AuftragPos.AuftragPos as 'AuftragPos'," +
                                              "(SELECT ADate FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID) as 'Auftragsdatum', " +
                                              "(Select Top(1)Artikel.GArt From Artikel WHERE Artikel.AuftragID=AuftragPos.Auftrag_ID) as 'Gut', " +
                                              "(SELECT KD_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID) as 'KD_ID', " +
                                              "(SELECT ADR.Name1 FROM ADR WHERE ADR.ID=(SELECT KD_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'Auftraggeber'," +

                                              "(SELECT B_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID) as 'B_ID', " +
                                              "(Select ADR.Name1 FROM ADR WHERE ADR.ID=(SELECT B_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'Beladestelle', " +
                                              "(Select ADR.PLZ FROM ADR WHERE ADR.ID= (SELECT B_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'B_PLZ', " +
                                              "(Select ADR.Ort FROM ADR WHERE ADR.ID=(SELECT B_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'B_Ort', " +

                                              "(SELECT E_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID) as 'E_ID', " +
                                              "(SELECT ADR.ID FROM ADR WHERE ADR.ID=(SELECT E_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'E_ID'," +
                                              "(Select ADR.Name1 FROM ADR WHERE ADR.ID=(SELECT E_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'Entladestelle', " +
                                              "(Select ADR.PLZ FROM ADR WHERE ADR.ID= (SELECT E_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'E_PLZ', " +
                                              "(Select ADR.Ort FROM ADR WHERE ADR.ID=(SELECT E_ID FROM Auftrag WHERE Auftrag.ANr=AuftragPos.Auftrag_ID)) as 'E_Ort', " +

                                              "AuftragPos.T_Date as 'Liefertermin', " +
                                              "AuftragPos.ZF as 'ZF', " +
                                              "AuftragPos.VSB, " +
                                              "(Select SUM(gemGewicht) FROM Artikel WHERE AuftragID=Auftrag_ID AND AuftragPos=AuftragPos.AuftragPos) as 'GesamtGemGewicht', " +
                                              "(Select SUM(Brutto) FROM Artikel WHERE AuftragID=Auftrag_ID AND AuftragPos=AuftragPos.AuftragPos) as 'GesamtBrutto', " +
                                              "AuftragPos.Ladenummer, " +
                                              "AuftragPos.Status " +
                                              "FROM Auftrag " +
                                              "INNER JOIN AuftragPos ON Auftrag.ANr = AuftragPos.Auftrag_ID " +
                                              "WHERE AuftragPos.AuftragPos='" + Pos + "' " +
                                              "AND AuftragPos.Auftrag_ID='" + AuftragID + "'";
            ada.Fill(dt);
            Command.Dispose();
            ada.Dispose();
            Globals.SQLcon.Close();
            if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
            {
                Command.Connection.Close();
            }
            return dt;
        }
        ///<summary>clsAuftragPos / GetIDbyAuftragAndAuftragPos</summary>
        ///<remarks>ID by Auftrag and AuftragPos </remarks>
        public static decimal GetIDbyAuftragAndAuftragPos(Globals._GL_USER myGLUser, decimal myAuftrag, decimal myAuftragPos, decimal myMandantenID, decimal myABereichID)
        {
            if (myMandantenID > 0)
            {
                string strSQL = string.Empty;
                strSQL = "Select a.ID FROM AuftragPos a " +
                                                     "INNER JOIN Auftrag b ON b.ID = a.AuftragTableID " +
                                                                         "WHERE a.Auftrag_ID='" + myAuftrag + "'  " +
                                                                         "AND a.AuftragPos='" + myAuftragPos + "' " +
                                                                         "AND b.MandantenID='" + myMandantenID + "' " +
                                                                         "AND b.ArbeitsbereichID='" + myABereichID + "' ";

                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, myGLUser.User_ID);
                if (strTmp != string.Empty)
                {
                    decimal decTmp = 0;
                    decimal.TryParse(strTmp, out decTmp);
                    return decTmp;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        ///<summary>clsAuftragPos / GetAuftragIDByID</summary>
        ///<remarks> </remarks>
        public static decimal GetAuftragIDByID(decimal AP_ID)
        {
            decimal apID = 0;
            try
            {
                SqlDataAdapter ada = new SqlDataAdapter();
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                ada.SelectCommand = Command;
                Command.CommandText = "SELECT Auftrag_ID FROM AuftragPos WHERE ID='" + AP_ID + "'";
                Globals.SQLcon.Open();

                object returnVal = Command.ExecuteScalar();

                if (returnVal != null)
                {
                    apID = (decimal)returnVal;
                }

                Command.Dispose();
                Globals.SQLcon.Close();
                if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                {
                    Command.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
            return apID;
        }
        ///<summary>clsAuftragPos / GetAuftragPosByID</summary>
        ///<remarks> </remarks>
        public static decimal GetAuftragPosByID(decimal AP_ID)
        {
            decimal apID = 0;
            try
            {
                SqlDataAdapter ada = new SqlDataAdapter();
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                ada.SelectCommand = Command;
                Command.CommandText = "SELECT AuftragPos FROM AuftragPos WHERE ID='" + AP_ID + "'";
                Globals.SQLcon.Open();

                object returnVal = Command.ExecuteScalar();

                if (returnVal != null)
                {
                    apID = (decimal)returnVal;
                }

                Command.Dispose();
                Globals.SQLcon.Close();
                if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                {
                    Command.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
            return apID;
        }
        //  


    }
}
