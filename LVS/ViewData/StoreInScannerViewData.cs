using Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class StoreInScannerViewData
    {
        public StoreInScannerViewData()
        {
            InitCls();
        }
        public StoreInScannerViewData(int myUserId) : this()
        {
            BenutzerID = myUserId;
        }
        public StoreInScannerViewData(int myId, int myUserId) : this()
        {
            //Abruf.Id = myId;
            //BenutzerID = myUserId;
            //if (Abruf.Id > 0)
            //{
            //    Fill();
            //}
        }


        public StoreInScanner StoreInScan { get; set; }
        private int BenutzerID { get; set; } = 0;

        public ArticleViewData articleViewData { get; set; }
        public WorkspaceViewData workspaceViewData { get; set; }

        //public List<Articles> ListArticleInAusgang { get; set; }
        public List<Calls> ListCallOpen { get; set; }

        public Globals._GL_SYSTEM GLSystem { get; set; }
        public Globals._GL_USER GL_USER { get; set; }
        public clsSystem System { get; set; }

        public string LogText { get; set; }
        public List<string> ListLogText { get; set; }

        public bool IsScanProcess { get; set; } = false;

        public Ausgaenge CreatedAusgang { get; set; } = new Ausgaenge();


        private void InitCls()
        {
            //Abruf = new Calls();
        }
        public void Fill()
        {
            string strSQL = sql_Get;
            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "Call", "Abruf", BenutzerID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                }
            }
        }
        private void SetValue(DataRow row)
        {
            //Abruf = new Calls();
            //Int32 iTmp = 0;
            //Int32.TryParse(row["ID"].ToString(), out iTmp);
            //Abruf.Id = iTmp;
            //Abruf.IsRead = (bool)row["IsRead"];
            //iTmp = 0;
            //Int32.TryParse(row["ArtikelID"].ToString(), out iTmp);
            //Abruf.ArtikelId = iTmp;
            //iTmp = 0;
            //Int32.TryParse(row["LVSNr"].ToString(), out iTmp);
            //Abruf.LVSNr = iTmp;
            //Abruf.Werksnummer = row["Werksnummer"].ToString();
            //Abruf.Produktionsnummer = row["Produktionsnummer"].ToString();
            //Abruf.Charge = row["Charge"].ToString();
            //decimal decTmp = 0;
            //Decimal.TryParse(row["Brutto"].ToString(), out decTmp);
            //Abruf.Brutto = decTmp;
            //iTmp = 0;
            //Int32.TryParse(row["CompanyID"].ToString(), out iTmp);
            //Abruf.CompanyId = iTmp;
            //Abruf.CompanyName = row["CompanyName"].ToString();
            //iTmp = 0;
            //Int32.TryParse(row["AbBereich"].ToString(), out iTmp);
            //Abruf.ArbeitsbereichId = iTmp;
            //Abruf.Datum = (DateTime)row["Datum"];
            //Abruf.EintreffDatum = (DateTime)row["EintreffDatum"];
            //Abruf.EintreffZeit = (DateTime)row["EintreffZeit"];
            //iTmp = 0;
            //Int32.TryParse(row["BenutzerID"].ToString(), out iTmp);
            //this.BenutzerID = iTmp;
            //Abruf.Benutzername = row["Benutzername"].ToString();
            //Abruf.Schicht = row["Schicht"].ToString();
            //Abruf.Referenz = row["Referenz"].ToString();
            //Abruf.Abladestelle = row["Abladestelle"].ToString();
            //Abruf.Aktion = row["Aktion"].ToString();
            //Abruf.Erstellt = (DateTime)row["Erstellt"];
            //Abruf.Status = enumCallStatus.NotExist;
            //iTmp = 0;
            //Int32.TryParse(row["LiefAdrId"].ToString(), out iTmp);
            //Abruf.LiefAdrId = iTmp;
            //iTmp = 0;
            //Int32.TryParse(row["EmpAdrId"].ToString(), out iTmp);
            //Abruf.EmpAdrId = iTmp;
            //iTmp = 0;
            //Int32.TryParse(row["SpedAdrId"].ToString(), out iTmp);
            //Abruf.SpedAdrId = iTmp;
            //Abruf.ASNFile = row.ToString();
            //Abruf.ASNLieferant = row["ASNLieferant"].ToString();
            //iTmp = 0;
            //Int32.TryParse(row["ASNQuantity"].ToString(), out iTmp);
            //Abruf.ASNQuantity = iTmp;
            //Abruf.ASNUnit = row["ASNUnit"].ToString();
            //Abruf.Description = row["Description"].ToString();

            //DateTime dtTmp = new DateTime(19, 1, 1);
            //if ((DateTime)row["ScanCheckForStoreOut"] < dtTmp)
            //{
            //    Abruf.ScanCheckForStoreOut = dtTmp;
            //}
            //else
            //{
            //    Abruf.ScanCheckForStoreOut = (DateTime)row["ScanCheckForStoreOut"];
            //}
            //iTmp = 0;
            //Int32.TryParse(row["ScanUserId"].ToString(), out iTmp);
            //Abruf.ScanUserId = iTmp;

            //if (Abruf.ArbeitsbereichId > 0)
            //{
            //    workspaceViewData = new WorkspaceViewData(Abruf.ArbeitsbereichId);
            //    Abruf.Workspace = workspaceViewData.Workspace.Copy();
            //}

            //if (Abruf.ArtikelId > 0)
            //{
            //    articleViewData = new ArticleViewData(Abruf.ArtikelId, BenutzerID, false);
            //    Abruf.Artikel = articleViewData.Artikel.Copy();
            //}

            //this.IsError = (bool)myDT.Rows[i]["IsError"];

            //Company
            //this.Company = new clsCompany();
            //this.Company._GL_User = this._GL_User;
            //this.Company.ID = this.CompanyID;
            //this.Company.Fill();
            ////Status

            ////ArtikelReferenz = ARtikel, der Abgerufen wird
            //this.ArtikelReferenz = new clsArtikel();
            //this.ArtikelReferenz.InitClass(this._GL_User, this._GL_System);
        }



        /// <summary>
        ///             ADD
        /// </summary>
        public void Add()
        {
            //string strSql = sql_Add;
            //strSql = strSql + "Select @@IDENTITY as 'ID';";

            //string strTmp = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSql, "Insert", BenutzerID);
            //int.TryParse(strTmp, out int iTmp);
            //Abruf.Id = iTmp;
        }


        public bool CreateLEiangangByCall()
        {
            bool bReturn = false;



            return bReturn;
        }
        /// <summary>
        ///             DELETE
        /// </summary>
        public void Delete()
        {

        }
        /// <summary>
        ///             UPDATE
        /// </summary>
        /// <returns></returns>
        public void Update()
        {
            //bool retVal = false;
            //string strSql = string.Empty;
            //bool IsLastStep = false;
            //switch (myStoreOutArt)
            //{
            //    case enumStoreOutArt.call:
            //        strSql = sqlCreater_WizStoreOut_Call.sql_String_StoreOut_Call(Abruf, myStoreOutArt_Steps);
            //        retVal = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "AusgangUpdate", BenutzerID);
            //        break;

            //    case enumStoreOutArt.manually:
            //    case enumStoreOutArt.open:                    
            //        break;
            //}
            //return retVal;
        }

        ///-----------------------------------------------------------------------------------------------------
        ///                             sql Statements
        ///-----------------------------------------------------------------------------------------------------

        /// <summary>
        ///             Add sql - String
        /// </summary>
        public string sql_Add
        {
            get
            {
                string strSql = string.Empty;
                //Abruf.Erstellt = DateTime.Now;
                //strSql = "INSERT INTO Abrufe (IsRead, ArtikelID, LVSNr, Werksnummer, Produktionsnummer, Charge, Brutto, CompanyID, CompanyName" +
                //                             ", AbBereich, Datum, EintreffDatum, EintreffZeit,BenutzerID, Benutzername, Schicht, Referenz "+"" +
                //                             ", Abladestelle, Aktion, Erstellt, IsCreated, Status, LiefAdrID, EmpAdrID, SpedAdrID, ASNFile "+
                //                             ", ASNLieferant, ASNQuantity, ASNUnit, Description " +
                //                            ") " +
                //                         "VALUES (" + Convert.ToInt32(Abruf.IsRead) +
                //                                     ", " + Abruf.ArtikelId +
                //                                     ", " + Abruf.LVSNr +
                //                                     ", '" + Abruf.Werksnummer + "'" +
                //                                     ", '" + Abruf.Produktionsnummer + "'" +
                //                                     ", '" + Abruf.Charge + "'" +
                //                                     ", '" + Abruf.Brutto.ToString().Replace(",", ".") + "'" +
                //                                     ", " + Abruf.CompanyId +
                //                                     ", '" + Abruf.CompanyName + "'" +
                //                                     ", " + Abruf.ArbeitsbereichId +
                //                                     ", '" + Abruf.Datum + "'" +
                //                                     ", '" + Abruf.EintreffDatum + "'" +
                //                                     ", '" + Abruf.EintreffZeit + "' " +
                //                                     ", " + Abruf.BenutzerID +
                //                                     ", '" + Abruf.Benutzername + "'" +
                //                                     ", '" + Abruf.Schicht + "'" +
                //                                     ", '" + Abruf.Referenz + "'" +
                //                                     ", '" + Abruf.Abladestelle + "'" +
                //                                     ", '" + Abruf.Aktion + "'" +
                //                                     ", '" + Abruf.Erstellt + "'" +
                //                                     ", " + Convert.ToInt32(true) +
                //                                     ", '" + Abruf.Status + "'" +
                //                                     ", " + Abruf.LiefAdrId +
                //                                     ", " + Abruf.EmpAdrId +
                //                                     ", " + Abruf.SpedAdrId +
                //                                     ", '" + Abruf.ASNFile + "'" +
                //                                     ", '" + Abruf.ASNLieferant + "'" +
                //                                     ", '" + Abruf.ASNQuantity + "'" +
                //                                     ", '" + Abruf.ASNUnit + "'" +
                //                                     ", '" + Abruf.Description + "'" +
                //                                     //", " + Convert.ToInt32(this.IsError) +
                //                                     ");";
                return strSql;
            }
        }
        /// <summary>
        ///             GET
        /// </summary>
        public string sql_Get
        {
            get
            {
                string strSql = string.Empty;
                //strSql = "Select * FROM LAusgang WHERE ID=" + Ausgang.Id + ";";
                //strSql = "Select a.* " +
                //                ", b.Name as Workspace " +
                //                "FROM Abrufe a " +
                //                "INNER JOIN Arbeitsbereich b on b.Id = a.AbBereich " +

                //                    " WHERE a.ID = " + Abruf.Id + " ";
                return strSql;
            }
        }

        /// <summary>
        ///             DELETE sql - String
        /// </summary>
        public string sql_Delete
        {
            get
            {
                string strSql = string.Empty;
                //strSql = "Delete FROM LAusgang WHERE ID =" + Ausgang.Id; ;
                return strSql;
            }
        }
        /// <summary>
        ///             Update sql - String
        /// </summary>
        public string sql_Update
        {
            get
            {
                string strSql = string.Empty;
                //strSql = "Update Artikel SET " +
                //                        "AuftragID=" + Artikel.AuftragID +
                //                        ", AuftragPos=" + Artikel.AuftragPos +
                //                        ", Mandanten_ID=" + Artikel.MandantenID +
                //                        ", AB_ID=" + Artikel.AbBereichID +
                //                        ", BKZ = " + Artikel.BKZ +
                //                        //ID REF
                //                        ", LVS_ID=" + Artikel.LVS_ID +
                //                        ", GArtID='" + Artikel.GArtID + "'" +
                //                        ", GutZusatz='" + Artikel.GutZusatz + "'" +
                //                        ", Werksnummer='" + Artikel.Werksnummer + "'" +
                //                        ", Produktionsnummer='" + Artikel.Produktionsnummer + "'" +
                //                        ", Charge='" + Artikel.Charge + "'" +
                //                        ", Bestellnummer='" + Artikel.Bestellnummer + "'" +
                //                        ", exMaterialnummer='" + Artikel.exMaterialnummer + "'" +
                //                        ", exBezeichnung='" + Artikel.exBezeichnung + "'" +
                //                        ", Position='" + Artikel.Position + "'" +
                //                        ", ArtIDRef='" + Artikel.ArtIDRef + "'" +

                //                        //Maße - Gewichte
                //                        ", Anzahl=" + Artikel.Anzahl +
                //                        ", Einheit='" + Artikel.Einheit + "'" +
                //                        ", Dicke='" + Artikel.Dicke.ToString().Replace(",", ".") + "'" +
                //                        ", Breite='" + Artikel.Breite.ToString().Replace(",", ".") + "'" +
                //                        ", Laenge='" + Artikel.Laenge.ToString().Replace(",", ".") + "'" +
                //                        ", Hoehe='" + Artikel.Hoehe.ToString().Replace(",", ".") + "'" +
                //                        ", Netto='" + Artikel.Netto.ToString().Replace(",", ".") + "'" +
                //                        ", Brutto='" + Artikel.Brutto.ToString().Replace(",", ".") + "'" +
                //                        ", TARef= '" + Artikel.TARef + "'" +
                //                        ", LEingangTableID=" + Artikel.LEingangTableID +
                //                        ", LAusgangTableID=" + Artikel.LAusgangTableID +
                //                        ", ArtIDAlt =" + Artikel.ArtIDAlt +
                //                        //Flags
                //                        ", UB=" + Convert.ToInt32(Artikel.Umbuchung) +
                //                        ", AbrufRef ='" + Artikel.AbrufReferenz + "'" +
                //                        ", CheckArt= '" + Artikel.EingangChecked + "'" +
                //                        ", LA_Checked ='" + Artikel.AusgangChecked + "'" +
                //                        ", Info='" + Artikel.Info + "'" +
                //                        ", LagerOrt=" + Artikel.LagerOrt +
                //                        ", LOTable='" + Artikel.LagerOrtTable + "'" +
                //                        ", exLagerOrt = '" + Artikel.exLagerOrt + "'" +
                //                        //", IsLagerArtikel ="+ Convert.ToInt32(IsLagerArtikel)+
                //                        ", ADRLagerNr=" + Artikel.ADRLagerNr +
                //                        //", FreigabeAbruf="+Convert.ToInt32(FreigabeAbruf)+   //Flag wird nicht hier upgedatet
                //                        ", LZZ ='" + Artikel.LZZ + "'" +
                //                        ", Werk ='" + Artikel.Werk + "'" +
                //                        ", Halle ='" + Artikel.Halle + "'" +
                //                        ", Reihe ='" + Artikel.Reihe + "'" +
                //                        ", Ebene = '" + Artikel.Ebene + "'" +
                //                        ", Platz ='" + Artikel.Platz + "'" +
                //                        ", exAuftrag ='" + Artikel.exAuftrag + "'" +
                //                        ", exAuftragPos ='" + Artikel.exAuftragPos + "'" +
                //                        ", ASNVerbraucher ='" + Artikel.ASNVerbraucher + "'" +
                //                        ", UB_AltCalcEinlagerung =" + Convert.ToInt32(Artikel.UB_AltCalcEinlagerung) +  //nur über UB
                //                        ", UB_AltCalcAuslagerung =" + Convert.ToInt32(Artikel.UB_AltCalcAuslagerung) +
                //                        ", UB_AltCalcLagergeld =" + Convert.ToInt32(Artikel.UB_AltCalcLagergeld) +
                //                        ", UB_NeuCalcEinlagerung =" + Convert.ToInt32(Artikel.UB_NeuCalcEinlagerung) +
                //                        ", UB_NeuCalcAuslagerung =" + Convert.ToInt32(Artikel.UB_NeuCalcAuslagerung) +
                //                        ", UB_NeuCalcLagergeld =" + Convert.ToInt32(Artikel.UB_NeuCalcLagergeld) +
                //                        ", IsVerpackt =" + Convert.ToInt32(Artikel.IsVerpackt) +
                //                        ", intInfo='" + Artikel.interneInfo + "'" +
                //                        ", exInfo='" + Artikel.externeInfo + "'" +
                //                        ", Guete='" + Artikel.Guete + "'" +
                //                        ", IsMulde=" + Convert.ToInt32(Artikel.IsMulde) +
                //                        ", IsLabelPrint=" + Convert.ToInt32(Artikel.IsLabelPrint) +
                //                        ", IsProblem=" + Convert.ToInt32(Artikel.IsProblem) +
                //                        ", IsStackable=" + Convert.ToInt32(this.Artikel.IsStackable) +
                //                        ", GlowDate='" + this.Artikel.GlowDate + "'" +

                //                        "  WHERE ID=" + Artikel.Id + "; ";
                return strSql;
            }
        }
        /// <summary>
        ///             GET sql - String
        /// </summary>
        public string sql_Get_Main
        {
            get
            {
                string strSql = string.Empty;
                return strSql;
            }
        }

        public void InitAndSaveEingang(DataTable myTable, string myAction, bool myIsSpedAction = true)
        {
            try
            {
                //LogText = string.Empty;
                //ListLogText = new List<string>();

                //if ((myTable != null) && (myTable.Rows.Count > 0))
                //{
                //    //Daten nach Lieferanten sortieren
                //    myTable.DefaultView.Sort = "Lieferant";
                //    DataTable dtSelArtikel = myTable.DefaultView.ToTable();
                //    DataTable dtLieferant = dtSelArtikel.DefaultView.ToTable(true, "Auftraggeber");
                //    foreach (DataRow row in dtLieferant.Rows)
                //    {
                //        clsASNCall tmpCall = new clsASNCall();
                //        tmpCall.InitClass(GL_USER, GLSystem, System);
                //        //clsASNCall tmpCall = new clsASNCall(BenutzerID);

                //        decimal decAuftraggeberID = 0;
                //        Decimal.TryParse(row["Auftraggeber"].ToString(), out decAuftraggeberID);
                //        if (decAuftraggeberID > 0)
                //        {
                //            dtSelArtikel.DefaultView.RowFilter = "Auftraggeber=" + (Int32)decAuftraggeberID;
                //            Int32 iAbrufID = 0;
                //            Int32.TryParse((dtSelArtikel.DefaultView.ToTable()).Rows[0]["AbrufID"].ToString(), out iAbrufID);
                //            if (iAbrufID > 0)
                //            {
                //                tmpCall.ID = iAbrufID;
                //                tmpCall.Fill();

                //                clsArbeitsbereiche workspace = new clsArbeitsbereiche((int)tmpCall.AbBereichID, BenutzerID);
                //                tmpCall.sys.AbBereich = workspace;
                //                tmpCall.sys.Mandant = workspace.Mandant;

                //                //string tmpAction = row["Aktion"].ToString();

                //                switch (myAction)
                //                {
                //                    /************************************************************
                //                    *              Verarbeitung Call / Abrufe
                //                    * *********************************************************/
                //                    case clsASNCall.const_AbrufAktion_Abruf:
                //                        tmpCall.ProzessCall(dtSelArtikel.DefaultView.ToTable(), myIsSpedAction);
                //                        if (tmpCall.AusgangToCreate.LAusgangTableID > 0)
                //                        {
                //                            AusgangViewData ausgangViewData = new AusgangViewData((int)tmpCall.AusgangToCreate.LAusgangTableID, BenutzerID, false);
                //                            CreatedAusgang = ausgangViewData.Ausgang.Copy();
                //                        }
                //                        else 
                //                        {
                //                            CreatedAusgang = new Ausgaenge();
                //                        }                                        
                //                        break;

                //                    /************************************************************
                //                    *              Verarbeitung Umbuchung / Rebooking
                //                    *************************************************************/
                //                    case clsASNCall.const_AbrufAktion_UB:
                //                        tmpCall.ProzessRebooking(dtSelArtikel.DefaultView.ToTable());
                //                        break;
                //                }

                //                LogText = string.Empty;
                //                LogText = tmpCall.LogText;
                //                ListLogText.Add(LogText);
                //                //this.tbLog.Text = this.tbLog.Text + LogTxt;
                //                //Eintrag ins Logbuch
                //                //Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.ASNAbruf.ToString(), LogText);

                //                clsLogbuch log = new clsLogbuch(BenutzerID, enumLogbuchAktion.ASNAbruf.ToString(), LogText);
                //                log.LogbuchInsert();
                //            }
                //            dtSelArtikel.DefaultView.RowFilter = string.Empty;
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                LogText = ex.Message;
                ListLogText.Add(LogText);
            }
        }


    }
}

