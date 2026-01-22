using Common.ApiModels;
using Common.Enumerations;
using Common.Models;
using Common.SqlStatementCreater;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using static LVS.Globals;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class ArticleViewData
    {
        public ArticleViewData()
        {
            InitCls();
        }
        public ArticleViewData(Articles myArticle, int myUserId) : this()
        {
            _GL_User = new _GL_USER();
            BenutzerID = myUserId;
            Artikel = myArticle.Copy();
        }
        public ArticleViewData(Articles myArticle, Globals._GL_USER GLUser) : this()
        {
            _GL_User = GLUser;
            BenutzerID = 1;
            Artikel = myArticle.Copy();
        }
        public ArticleViewData(Globals._GL_USER GLUser) : this()
        {
            _GL_User = GLUser;
            BenutzerID = (int)GLUser.User_ID;
        }
        public ArticleViewData(int myUserId) : this()
        {
            _GL_User = new _GL_USER();
            BenutzerID = myUserId;
        }
        public ArticleViewData(int myArticleId, Globals._GL_USER GLUser) : this()
        {
            _GL_User = GLUser;
            BenutzerID = (int)GLUser.User_ID;
            Artikel.Id = myArticleId;
            if (Artikel.Id > 0)
            {
                Fill();
            }
        }
        public ArticleViewData(int myArticleId, int myUserId, bool myFillClsOnly = false) : this()
        {
            _GL_User = new _GL_USER();
            BenutzerID = myUserId;
            Artikel.Id = myArticleId;
            FillClsOnly = myFillClsOnly;
            if (Artikel.Id > 0)
            {
                Fill();
            }
        }
        public ArticleViewData(int myLvsId, int myUserId, int myWorkspaceId) : this()
        {
            _GL_User = new _GL_USER();
            BenutzerID = myUserId;

            workspaceViewData = new WorkspaceViewData(myWorkspaceId);
            if (myLvsId > 0)
            {
                Artikel = new Articles();
                Artikel.LVS_ID = myLvsId;
                Artikel.AbBereichID = myWorkspaceId;
                GetArticleByLvsId();
            }
        }



        public Articles Artikel { get; set; }
        public Eingaenge Eingang { get; set; }
        internal EingangViewData eViewData { get; set; }

        public Ausgaenge Ausgang { get; set; }
        internal AusgangViewData aViewData { get; set; }
        public WorkspaceViewData workspaceViewData { get; set; }

        public string ArticleChangingValue { get; set; }
        public bool FillClsOnly { get; set; } = false;

        public Globals._GL_USER _GL_User;
        private int BenutzerID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private void InitCls()
        {
            Artikel = new Articles();
            this.Artikel.Id = 0;
            this.Artikel.LVS_ID = 0;
            this.Artikel.Produktionsnummer = String.Empty;
            this.Artikel.Werksnummer = String.Empty;
            this.Artikel.Charge = String.Empty;

        }


        ///<summary>ArticleViewModel / Add</summary>
        ///<remarks></remarks>
        public bool AddbyScanner()
        {
            bool bReturn = false;
            string strSql = sql_Add;
            //strSql = strSql + "Select @@IDENTITY as 'ID';";
            if (strSql.Length > 0)
            {
                string strTmp = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSql, "Insert", BenutzerID);
                int.TryParse(strTmp, out int iTmp);
                if (iTmp > 0)
                {
                    bReturn = true;
                    Artikel.Id = iTmp;
                    Fill();
                    //Vita
                    clsArtikelVita.AddArtikelLEingangScanner(Artikel, BenutzerID);
                }
            }
            return bReturn;
        }
        ///<summary>ArticleViewModel / Delete</summary>
        public void Delete()
        {
            if (Artikel.Id > 0)
            {
                //ID = myArtID;
                //GetArtikeldatenByTableID();
                //decimal decTmpLagerEingangID = clsLager.GetLEingangIDByLEingangTableID(this._GL_User.User_ID, this.LEingangTableID);

                string strSql = string.Empty;
                strSql = sql_Delete;
                //bool mybExecOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
                bool mybExecOK = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "DELETE", BenutzerID);
                //Logbuch Eintrag
                if (mybExecOK)
                {
                    //Add Logbucheintrag 
                    string myBeschreibung = "Artikel gelöscht: Artikel ID [" + Artikel.Id.ToString() + "] ";
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);

                    //fehlen Lagereingang ID / Lagereingangtable
                    //clsArtikelVita.ArtikelDelete(_GL_User, this.Artikel.LEingangTableID, decTmpLagerEingangID, this.Artikel.LVS_ID);

                    //this.Eingang.LEingangTableID = this.LEingangTableID;
                    //this.Eingang.FillEingang();

                    //Update der anderen Artikelpositionen
                    //if (this.Eingang.dtArtInLEingang.Rows.Count > 0)
                    //{
                    //    int iCount = 0;
                    //    foreach (DataRow row in this.Eingang.dtArtInLEingang.Rows)
                    //    {
                    //        if (row["ID"] != null)
                    //        {
                    //            decimal decTmp = (decimal)row["ID"];
                    //            clsArtikel tmpArt = new clsArtikel();
                    //            tmpArt.InitClass(this._GL_User, this._GL_System);
                    //            tmpArt.ID = decTmp;
                    //            tmpArt.GetArtikeldatenByTableID();
                    //            iCount++;
                    //            tmpArt.Position = iCount.ToString();
                    //            tmpArt.UpdatePosition();
                    //        }
                    //    }
                    //}
                }
            }
        }


        ///-----------------------------------------------------------------------------------------------------
        ///                             sql Statements
        ///-----------------------------------------------------------------------------------------------------



        public string sql_Add_Main
        {
            get
            {
                string strSql = string.Empty;
                if ((Artikel.AbBereichID > 0) && (Artikel.MandantenID > 0))
                {

                    //Artikel.GlowDate = new DateTime(1900, 1, 1);
                    //Artikel.LZZ = new DateTime(1900, 1, 1);
                    Artikel.CreatedByScanner = true;
                    Artikel.IdentifiedByScan = DateTime.Now;

                    //strSql = "DECLARE @LvsID as int; ";
                    //strSql += "DECLARE @ArtID int; ";
                    strSql += " INSERT INTO Artikel (AuftragID, AuftragPos, LVS_ID, Mandanten_ID, AB_ID, BKZ, GArtID, " +
                                                    "Dicke, Breite, Laenge, Hoehe, Anzahl, Einheit, gemGewicht, Netto, " +
                                                    "Brutto, Werksnummer, Produktionsnummer, exBezeichnung, Charge, " +
                                                    "Bestellnummer, exMaterialnummer, Position, GutZusatz, CheckArt, " +
                                                    "AbrufRef, TARef, LEingangTableID, LAusgangTableID, " +
                                                    "ArtIDRef, AuftragPosTableID, ArtIDAlt, Info, LagerOrt, LOTable, exLagerOrt," +
                                                    "ADRLagerNr, FreigabeAbruf, LZZ, Werk, Halle, Ebene, Reihe, Platz, exAuftrag, " +
                                                    "exAuftragPos, ASNVerbraucher, UB_AltCalcEinlagerung, UB_AltCalcAuslagerung, " +
                                                    "UB_AltCalcLagergeld, UB_NeuCalcEinlagerung, UB_NeuCalcAuslagerung, UB_NeuCalcLagergeld, " +
                                                    "IsVerpackt, intInfo, exInfo, Guete, IsStackable, GlowDate,IdentifiedByScan, CreatedByScanner" +

                                                    ") VALUES ("
                                                                + "0"   //AuftragID
                                                                + ",0"  //AuftragPos
                                                                + ",0";   //LVSNR

                    strSql += "," + Artikel.MandantenID;
                    strSql += "," + Artikel.AbBereichID;
                    strSql += ",1";              //BKZ
                    strSql += "," + Artikel.GArtID;
                    strSql += ",'" + Artikel.Dicke.ToString().Replace(",", ".") + "'";
                    strSql += ",'" + Artikel.Breite.ToString().Replace(",", ".") + "'" +
                              ",'" + Artikel.Laenge.ToString().Replace(",", ".") + "'" +
                              ",'" + Artikel.Hoehe.ToString().Replace(",", ".") + "'" +
                              "," + Artikel.Anzahl +
                              ",'" + Artikel.Einheit + "'";
                    strSql += ",'" + Artikel.gemGewicht.ToString().Replace(",", ".") + "'" +
                              ",'" + Artikel.Netto.ToString().Replace(",", ".") + "'" +
                              ",'" + Artikel.Brutto.ToString().Replace(",", ".") + "'" +
                              ",'" + Artikel.Werksnummer + "'" +
                              ",'" + Artikel.Produktionsnummer + "'" +
                              ",'" + Artikel.exBezeichnung + "'" +
                              ",'" + Artikel.Charge + "'" +
                              ",'" + Artikel.Bestellnummer + "'" +
                              ",'" + Artikel.exMaterialnummer + "'" +
                              ",'" + Artikel.Position + "'" +
                              ",'" + Artikel.GutZusatz + "'" +
                              "," + Convert.ToInt32(Artikel.EingangChecked) +
                              ",'" + Artikel.AbrufReferenz + "'" +                             //AbrufRef
                              ",'" + Artikel.TARef + "'" +                         //TARef
                              "," + Artikel.LEingangTableID +
                              "," + Artikel.LAusgangTableID +                            //LagerausgangTableID              
                              ",'" + Artikel.ArtIDRef + "'" +
                              "," + Artikel.AuftragPosTableID +
                              "," + Artikel.ArtIDAlt +
                               ",'" + Artikel.Info + "'" +
                              "," + Artikel.LagerOrt +
                              ", '" + Artikel.LagerOrtTable + "'" +
                              ",'" + Artikel.exLagerOrt + "'" +
                              ", " + Artikel.ADRLagerNr +
                              ", " + Convert.ToInt32(Artikel.FreigabeAbruf) +
                              ", '" + Artikel.LZZ + "'" +
                              ", '" + Artikel.Werk + "' " +
                              ", '" + Artikel.Halle + "'" +
                              ", '" + Artikel.Ebene + "'" +
                              ", '" + Artikel.Reihe + "'" +
                              ", '" + Artikel.Platz + "'" +
                              ", '" + Artikel.exAuftrag + "'" +
                              ", '" + Artikel.exAuftragPos + "'" +
                              ", '" + Artikel.ASNVerbraucher + "'" +
                              ", " + Convert.ToInt32(Artikel.UB_AltCalcEinlagerung) +
                              ", " + Convert.ToInt32(Artikel.UB_AltCalcAuslagerung) +
                              ", " + Convert.ToInt32(Artikel.UB_AltCalcLagergeld) +
                              ", " + Convert.ToInt32(Artikel.UB_NeuCalcEinlagerung) +
                              ", " + Convert.ToInt32(Artikel.UB_NeuCalcAuslagerung) +
                              ", " + Convert.ToInt32(Artikel.UB_NeuCalcLagergeld) +
                              ", " + Convert.ToInt32(Artikel.IsVerpackt) +
                              ", '" + Artikel.interneInfo + "'" +
                              ", '" + Artikel.externeInfo + "'" +
                              ", '" + Artikel.Guete + "'" +
                              ", " + Convert.ToInt32(Artikel.IsStackable) +
                              ", '" + Artikel.GlowDate + "'" +
                               ", '" + Artikel.IdentifiedByScan + "'" +
                              ", " + Convert.ToInt32(Artikel.CreatedByScanner) +

                              "); ";
                    //strSql = strSql + " Select @@IDENTITY as 'ID' ;";
                    //strSql += "SET @ArtID = (Select @@IDENTITY); ";
                    //strSql += "SET  @LvsID=" + PrimeKeyViewData.SQLStringNewLVSNr(Artikel.AbBereichID, Artikel.MandantenID, true);
                    //strSql += " Update Artikel SET LVS_ID=@LvsID WHERE ID = @ArtID; ";
                    //strSql += " UPDATE PrimeKeys SET LvsNr = @LvsID; ";
                    //strSql += " Select @@IDENTITY as 'ID' ;";
                }
                return strSql;
            }
        }
        /// <summary>
        ///             Add sql - String
        /// </summary>
        public string sql_Add
        {
            get
            {
                string strSql = string.Empty;
                if ((Artikel.AbBereichID > 0) && (Artikel.MandantenID > 0))
                {

                    //Artikel.GlowDate = new DateTime(1900, 1, 1);
                    //Artikel.LZZ = new DateTime(1900, 1, 1);
                    //Artikel.CreatedByScanner = true;
                    //Artikel.IdentifiedByScan = DateTime.Now;

                    strSql = "DECLARE @LvsID as int; ";
                    strSql += "DECLARE @ArtID int; ";
                    strSql += sql_Add_Main;

                    //strSql += " INSERT INTO Artikel (AuftragID, AuftragPos, LVS_ID, Mandanten_ID, AB_ID, BKZ, GArtID, " +
                    //                                "Dicke, Breite, Laenge, Hoehe, Anzahl, Einheit, gemGewicht, Netto, " +
                    //                                "Brutto, Werksnummer, Produktionsnummer, exBezeichnung, Charge, " +
                    //                                "Bestellnummer, exMaterialnummer, Position, GutZusatz, CheckArt, " +
                    //                                "AbrufRef, TARef, LEingangTableID, LAusgangTableID, " +
                    //                                "ArtIDRef, AuftragPosTableID, ArtIDAlt, Info, LagerOrt, LOTable, exLagerOrt," +
                    //                                "ADRLagerNr, FreigabeAbruf, LZZ, Werk, Halle, Ebene, Reihe, Platz, exAuftrag, " +
                    //                                "exAuftragPos, ASNVerbraucher, UB_AltCalcEinlagerung, UB_AltCalcAuslagerung, " +
                    //                                "UB_AltCalcLagergeld, UB_NeuCalcEinlagerung, UB_NeuCalcAuslagerung, UB_NeuCalcLagergeld, " +
                    //                                "IsVerpackt, intInfo, exInfo, Guete, IsStackable, GlowDate,IdentifiedByScan, CreatedByScanner" +

                    //                                ") VALUES ("
                    //                                            + "0"   //AuftragID
                    //                                            + ",0"  //AuftragPos
                    //                                            + ",0";   //LVSNR

                    //strSql += "," + Artikel.MandantenID;
                    //strSql += "," + Artikel.AbBereichID;
                    //strSql += ",1";              //BKZ
                    //strSql += "," + Artikel.GArtID;
                    //strSql += ",'" + Artikel.Dicke.ToString().Replace(",", ".") + "'";
                    //strSql += ",'" + Artikel.Breite.ToString().Replace(",", ".") + "'" +
                    //          ",'" + Artikel.Laenge.ToString().Replace(",", ".") + "'" +
                    //          ",'" + Artikel.Hoehe.ToString().Replace(",", ".") + "'" +
                    //          "," + Artikel.Anzahl +
                    //          ",'" + Artikel.Einheit + "'";
                    //strSql += ",'" + Artikel.gemGewicht.ToString().Replace(",", ".") + "'" +
                    //          ",'" + Artikel.Netto.ToString().Replace(",", ".") + "'" +
                    //          ",'" + Artikel.Brutto.ToString().Replace(",", ".") + "'" +
                    //          ",'" + Artikel.Werksnummer + "'" +
                    //          ",'" + Artikel.Produktionsnummer + "'" +
                    //          ",'" + Artikel.exBezeichnung + "'" +
                    //          ",'" + Artikel.Charge + "'" +
                    //          ",'" + Artikel.Bestellnummer + "'" +
                    //          ",'" + Artikel.exMaterialnummer + "'" +
                    //          ",'" + Artikel.Position + "'" +
                    //          ",'" + Artikel.GutZusatz + "'" +
                    //          "," + Convert.ToInt32(Artikel.EingangChecked) +
                    //          ",'" + Artikel.AbrufReferenz + "'" +                             //AbrufRef
                    //          ",'" + Artikel.TARef + "'" +                         //TARef
                    //          "," + Artikel.LEingangTableID +
                    //          "," + Artikel.LAusgangTableID +                            //LagerausgangTableID              
                    //          ",'" + Artikel.ArtIDRef + "'" +
                    //          "," + Artikel.AuftragPosTableID +
                    //          "," + Artikel.ArtIDAlt +
                    //           ",'" + Artikel.Info + "'" +
                    //          "," + Artikel.LagerOrt +
                    //          ", '" + Artikel.LagerOrtTable + "'" +
                    //          ",'" + Artikel.exLagerOrt + "'" +
                    //          ", " + Artikel.ADRLagerNr +
                    //          ", " + Convert.ToInt32(Artikel.FreigabeAbruf) +
                    //          ", '" + Artikel.LZZ + "'" +
                    //          ", '" + Artikel.Werk + "' " +
                    //          ", '" + Artikel.Halle + "'" +
                    //          ", '" + Artikel.Ebene + "'" +
                    //          ", '" + Artikel.Reihe + "'" +
                    //          ", '" + Artikel.Platz + "'" +
                    //          ", '" + Artikel.exAuftrag + "'" +
                    //          ", '" + Artikel.exAuftragPos + "'" +
                    //          ", '" + Artikel.ASNVerbraucher + "'" +
                    //          ", " + Convert.ToInt32(Artikel.UB_AltCalcEinlagerung) +
                    //          ", " + Convert.ToInt32(Artikel.UB_AltCalcAuslagerung) +
                    //          ", " + Convert.ToInt32(Artikel.UB_AltCalcLagergeld) +
                    //          ", " + Convert.ToInt32(Artikel.UB_NeuCalcEinlagerung) +
                    //          ", " + Convert.ToInt32(Artikel.UB_NeuCalcAuslagerung) +
                    //          ", " + Convert.ToInt32(Artikel.UB_NeuCalcLagergeld) +
                    //          ", " + Convert.ToInt32(Artikel.IsVerpackt) +
                    //          ", '" + Artikel.interneInfo + "'" +
                    //          ", '" + Artikel.externeInfo + "'" +
                    //          ", '" + Artikel.Guete + "'" +
                    //          ", " + Convert.ToInt32(Artikel.IsStackable) +
                    //          ", '" + Artikel.GlowDate + "'" +
                    //           ", '" + Artikel.IdentifiedByScan + "'" +
                    //          ", " + Convert.ToInt32(Artikel.CreatedByScanner) +

                    //          "); ";
                    //strSql = strSql + " Select @@IDENTITY as 'ID' ;";
                    strSql += "SET @ArtID = (Select @@IDENTITY); ";
                    strSql += "SET  @LvsID=" + PrimeKeyViewData.SQLStringNewLVSNr(Artikel.AbBereichID, Artikel.MandantenID, true);
                    strSql += " Update Artikel SET LVS_ID=@LvsID WHERE ID = @ArtID; ";
                    strSql += " UPDATE PrimeKeys SET LvsNr = @LvsID; ";
                    strSql += " Select @@IDENTITY as 'ID' ;";
                }
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
                string strSql = "Delete FROM Artikel WHERE ID =" + Artikel.Id; ;
                return strSql;
            }
        }
        /// <summary>
        ///             DELETE sql - String
        /// </summary>
        public string sql_ArticleByLvsId
        {
            get
            {
                string strSql = string.Empty;
                strSql += sql_Get_Main;
                strSql += " WHERE LVS_ID=" + Artikel.LVS_ID;
                strSql += " and AB_ID=" + workspaceViewData.Workspace.Id;

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
                strSql = "Update Artikel SET " +
                                        "AuftragID=" + Artikel.AuftragID +
                                        ", AuftragPos=" + Artikel.AuftragPos +
                                        ", Mandanten_ID=" + Artikel.MandantenID +
                                        ", AB_ID=" + Artikel.AbBereichID +
                                        ", BKZ = " + Artikel.BKZ +
                                        //ID REF
                                        ", LVS_ID=" + Artikel.LVS_ID +
                                        ", GArtID='" + Artikel.GArtID + "'" +
                                        ", GutZusatz='" + Artikel.GutZusatz + "'" +
                                        ", Werksnummer='" + Artikel.Werksnummer + "'" +
                                        ", Produktionsnummer='" + Artikel.Produktionsnummer + "'" +
                                        ", Charge='" + Artikel.Charge + "'" +
                                        ", Bestellnummer='" + Artikel.Bestellnummer + "'" +
                                        ", exMaterialnummer='" + Artikel.exMaterialnummer + "'" +
                                        ", exBezeichnung='" + Artikel.exBezeichnung + "'" +
                                        ", Position='" + Artikel.Position + "'" +
                                        ", ArtIDRef='" + Artikel.ArtIDRef + "'" +

                                        //Maße - Gewichte
                                        ", Anzahl=" + Artikel.Anzahl +
                                        ", Einheit='" + Artikel.Einheit + "'" +
                                        ", Dicke='" + Artikel.Dicke.ToString().Replace(",", ".") + "'" +
                                        ", Breite='" + Artikel.Breite.ToString().Replace(",", ".") + "'" +
                                        ", Laenge='" + Artikel.Laenge.ToString().Replace(",", ".") + "'" +
                                        ", Hoehe='" + Artikel.Hoehe.ToString().Replace(",", ".") + "'" +
                                        ", Netto='" + Artikel.Netto.ToString().Replace(",", ".") + "'" +
                                        ", Brutto='" + Artikel.Brutto.ToString().Replace(",", ".") + "'" +
                                        ", TARef= '" + Artikel.TARef + "'" +
                                        ", LEingangTableID=" + Artikel.LEingangTableID +
                                        ", LAusgangTableID=" + Artikel.LAusgangTableID +
                                        ", ArtIDAlt =" + Artikel.ArtIDAlt +
                                        //Flags
                                        ", UB=" + Convert.ToInt32(Artikel.Umbuchung) +
                                        ", AbrufRef ='" + Artikel.AbrufReferenz + "'" +
                                        ", CheckArt= '" + Artikel.EingangChecked + "'" +
                                        ", LA_Checked ='" + Artikel.AusgangChecked + "'" +
                                        ", Info='" + Artikel.Info + "'" +
                                        ", LagerOrt=" + Artikel.LagerOrt +
                                        ", LOTable='" + Artikel.LagerOrtTable + "'" +
                                        ", exLagerOrt = '" + Artikel.exLagerOrt + "'" +
                                        //", IsLagerArtikel ="+ Convert.ToInt32(IsLagerArtikel)+
                                        ", ADRLagerNr=" + Artikel.ADRLagerNr +
                                        //", FreigabeAbruf="+Convert.ToInt32(FreigabeAbruf)+   //Flag wird nicht hier upgedatet
                                        ", LZZ ='" + Artikel.LZZ + "'" +
                                        ", Werk ='" + Artikel.Werk + "'" +
                                        ", Halle ='" + Artikel.Halle + "'" +
                                        ", Reihe ='" + Artikel.Reihe + "'" +
                                        ", Ebene = '" + Artikel.Ebene + "'" +
                                        ", Platz ='" + Artikel.Platz + "'" +
                                        ", exAuftrag ='" + Artikel.exAuftrag + "'" +
                                        ", exAuftragPos ='" + Artikel.exAuftragPos + "'" +
                                        ", ASNVerbraucher ='" + Artikel.ASNVerbraucher + "'" +
                                        ", UB_AltCalcEinlagerung =" + Convert.ToInt32(Artikel.UB_AltCalcEinlagerung) +  //nur über UB
                                        ", UB_AltCalcAuslagerung =" + Convert.ToInt32(Artikel.UB_AltCalcAuslagerung) +
                                        ", UB_AltCalcLagergeld =" + Convert.ToInt32(Artikel.UB_AltCalcLagergeld) +
                                        ", UB_NeuCalcEinlagerung =" + Convert.ToInt32(Artikel.UB_NeuCalcEinlagerung) +
                                        ", UB_NeuCalcAuslagerung =" + Convert.ToInt32(Artikel.UB_NeuCalcAuslagerung) +
                                        ", UB_NeuCalcLagergeld =" + Convert.ToInt32(Artikel.UB_NeuCalcLagergeld) +
                                        ", IsVerpackt =" + Convert.ToInt32(Artikel.IsVerpackt) +
                                        ", intInfo='" + Artikel.interneInfo + "'" +
                                        ", exInfo='" + Artikel.externeInfo + "'" +
                                        ", Guete='" + Artikel.Guete + "'" +
                                        ", IsMulde=" + Convert.ToInt32(Artikel.IsMulde) +
                                        ", IsLabelPrint=" + Convert.ToInt32(Artikel.IsLabelPrint) +
                                        ", IsProblem=" + Convert.ToInt32(Artikel.IsProblem) +
                                        ", IsStackable=" + Convert.ToInt32(this.Artikel.IsStackable) +
                                        ", GlowDate='" + this.Artikel.GlowDate + "'" +

                                        "  WHERE ID=" + Artikel.Id + "; ";
                return strSql;
            }
        }
        /// <summary>
        ///             Update Lagerort sql - String
        /// </summary>
        public string sql_Update_StoredLocation
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Update Artikel SET " +
                                        "LagerOrt=" + Artikel.LagerOrt +
                                        ", LOTable ='" + Artikel.LagerOrtTable + "'" +
                                        ", Werk ='" + Artikel.Werk + "'" +
                                        ", Halle ='" + Artikel.Halle + "'" +
                                        ", Reihe ='" + Artikel.Reihe + "'" +
                                        ", Ebene = '" + Artikel.Ebene + "'" +
                                        ", Platz ='" + Artikel.Platz + "'" +

                                        "  WHERE ID=" + Artikel.Id + "; ";
                return strSql;
            }
        }


        public string sql_Update_Checked_StoreIn
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Update Artikel SET " +
                                        "BKZ=1" +
                                        ", CheckArt = 1" +
                                        ", IdentifiedByScan='" + new DateTime(1900, 1, 1) + "'" +
                                        "  WHERE ID=" + Artikel.Id + "; ";
                return strSql;
            }
        }

        public string sql_Update_Checked_StoreOut
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Update Artikel SET " +
                                        "BKZ=0" +
                                        ", LA_Checked = 1" +
                                        "  WHERE ID=" + Artikel.Id + "; ";
                return strSql;
            }
        }

        public string sql_Update_ScanVal_StoreIn
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Update Artikel SET " +
                                        "BKZ=1" +
                                        ", CheckArt = 0" +
                                        ", ScanIn='" + DateTime.Now + "' " +
                                        ", ScanInUser = " + Artikel.ScanInUser +

                                        "  WHERE ID=" + Artikel.Id + "; ";
                return strSql;
            }
        }

        public string sql_Update_ScanVal_StoreOut
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Update Artikel SET " +
                                        "BKZ=1" +
                                        ", LA_Checked = 0" +
                                        ", ScanOut = '" + DateTime.Now + "' " +
                                        ", ScanOutUser = " + Artikel.ScanOutUser +

                                        "  WHERE ID=" + Artikel.Id + "; ";
                return strSql;
            }
        }

        public string sql_Update_ScanIdentification
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Update Artikel SET IdentifiedByScan='" + DateTime.Now.ToString() + "'" +
                                        "  WHERE ID=" + Artikel.Id + "; ";
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
                strSql = "SELECT a.* " +
                                    ", CASE " +
                                            "WHEN (Select COUNT(ID) FROM SchadenZuweisung WHERE ArtikelID=a.ID)>0  THEN CAST(1 as bit) " +
                                            "ELSE CAST(0 as bit) END  as IsSchaden " +
                                    //", CASE " +
                                    //        "WHEN (Select COUNT(ID) FROM Ruecklieferung WHERE ArtikelID=a.ID)>0  THEN CAST(1 as bit) " +
                                    //        "ELSE CAST(0 as bit) END  as IsRL " +

                                    ", (SELECT ub.LVS_ID FROM Artikel ub where ub.Id = a.ArtIDAlt) as LVSNrBeforeUB " +
                                    ", (Select Top(1)b.ID FROM Artikel b WHERE b.ArtIDAlt = a.ID Order by ID desc ) as ArtIdAfterUB " +
                                    ", (Select Top(1)b.LVS_ID FROM Artikel b WHERE b.ArtIDAlt = a.ID Order by ID desc) as LVSNrAfterUB " +
                                    "FROM Artikel a ";
                return strSql;
            }
        }
        /// <summary>
        ///             GET sql - String
        /// </summary>
        public string sql_Get
        {
            get
            {
                string strSql = string.Empty;
                strSql += sql_Get_Main;
                strSql += " WHERE a.ID=" + Artikel.Id + ";";
                return strSql;
            }
        }

        ///-----------------------------------------------------------------------------------------------------
        ///                             Function / Procedure
        ///-----------------------------------------------------------------------------------------------------

        public void GetArticleByLvsId()
        {
            DataTable dt = new DataTable();
            string strSql = sql_ArticleByLvsId;
            dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "GET", "Artikel", BenutzerID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    SetValue(r);
                }
            }
        }
        ///<summary>ArticleViewModel / Fill</summary>
        ///<remarks>Ermittel die Daten anhand der TableID.</remarks>
        public void Fill()
        {
            DataTable dt = new DataTable();
            string strSql = sql_Get;
            dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "GET", "Artikel", BenutzerID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    SetValue(r);
                }
            }
        }
        private void SetValue(DataRow myRow)
        {
            InitCls();

            int iTmp = 0;
            int.TryParse(myRow["ID"].ToString(), out iTmp);
            this.Artikel.Id = iTmp;
            iTmp = 0;
            int.TryParse(myRow["AuftragID"].ToString(), out iTmp);
            this.Artikel.AuftragID = iTmp;
            iTmp = 0;
            int.TryParse(myRow["AuftragPos"].ToString(), out iTmp);
            this.Artikel.AuftragPos = iTmp;
            iTmp = 0;
            int.TryParse(myRow["LVS_ID"].ToString(), out iTmp);
            this.Artikel.LVS_ID = iTmp;
            iTmp = 0;
            int.TryParse(myRow["GArtID"].ToString(), out iTmp);
            this.Artikel.GArtID = iTmp;


            decimal decTmp = 0;
            decimal.TryParse(myRow["Dicke"].ToString(), out decTmp);
            this.Artikel.Dicke = decTmp;
            decTmp = 0;
            decimal.TryParse(myRow["Breite"].ToString(), out decTmp);
            this.Artikel.Breite = decTmp;
            decTmp = 0;
            decimal.TryParse(myRow["Laenge"].ToString(), out decTmp);
            this.Artikel.Laenge = decTmp;
            decTmp = 0;
            decimal.TryParse(myRow["Hoehe"].ToString(), out decTmp);
            this.Artikel.Hoehe = decTmp;
            iTmp = 0;
            int.TryParse(myRow["Anzahl"].ToString(), out iTmp);
            this.Artikel.Anzahl = iTmp;
            this.Artikel.Einheit = myRow["Einheit"].ToString();
            decTmp = 0;
            decimal.TryParse(myRow["gemGewicht"].ToString(), out decTmp);
            this.Artikel.gemGewicht = decTmp;
            decTmp = 0;
            decimal.TryParse(myRow["Netto"].ToString(), out decTmp);
            this.Artikel.Netto = decTmp;
            decTmp = 0;
            decimal.TryParse(myRow["Brutto"].ToString(), out decTmp);
            this.Artikel.Brutto = decTmp;

            this.Artikel.Werksnummer = myRow["Werksnummer"].ToString();
            this.Artikel.Produktionsnummer = myRow["Produktionsnummer"].ToString();
            this.Artikel.exBezeichnung = myRow["exBezeichnung"].ToString();
            this.Artikel.Charge = myRow["Charge"].ToString();
            this.Artikel.Bestellnummer = myRow["Bestellnummer"].ToString();
            this.Artikel.exMaterialnummer = myRow["exMaterialnummer"].ToString();
            this.Artikel.Position = myRow["Position"].ToString();
            this.Artikel.GutZusatz = myRow["GutZusatz"].ToString();
            this.Artikel.BKZ = Convert.ToInt32(myRow["BKZ"]);
            this.Artikel.Umbuchung = (bool)myRow["UB"];
            this.Artikel.AbrufReferenz = myRow["AbrufRef"].ToString();
            this.Artikel.TARef = myRow["TARef"].ToString();
            this.Artikel.EingangChecked = (bool)myRow["CheckArt"];

            iTmp = 0;
            int.TryParse(myRow["AB_ID"].ToString(), out iTmp);
            this.Artikel.AbBereichID = iTmp;
            iTmp = 0;
            int.TryParse(myRow["Mandanten_ID"].ToString(), out iTmp);
            this.Artikel.MandantenID = iTmp;
            iTmp = 0;
            int.TryParse(myRow["LEingangTableID"].ToString(), out iTmp);
            this.Artikel.LEingangTableID = iTmp;
            iTmp = 0;
            int.TryParse(myRow["LAusgangTableID"].ToString(), out iTmp);
            this.Artikel.LAusgangTableID = iTmp;
            this.Artikel.ArtIDRef = myRow["ArtIDRef"].ToString();
            iTmp = 0;
            int.TryParse(myRow["AuftragPosTableID"].ToString(), out iTmp);
            this.Artikel.AuftragPosTableID = iTmp;
            this.Artikel.AusgangChecked = (bool)myRow["LA_Checked"];
            iTmp = 0;
            int.TryParse(myRow["ArtIDAlt"].ToString(), out iTmp);
            this.Artikel.ArtIDAlt = iTmp;
            this.Artikel.Info = myRow["Info"].ToString();

            iTmp = 0;
            int.TryParse(myRow["LagerOrt"].ToString(), out iTmp);
            this.Artikel.LagerOrt = iTmp;

            //decTmp = 0;
            //Decimal.TryParse(myRow["LagerOrt"].ToString(), out decTmp);
            //this.Artikel.LagerOrt =(int) decTmp;
            this.Artikel.LagerOrtTable = myRow["LOTable"].ToString();
            this.Artikel.exLagerOrt = myRow["exLagerOrt"].ToString();

            //Schaden
            //if (myRow["IsSchaden"] != DBNull.Value)
            //{
            //    this.Artikel.bSchaden = (bool)myRow["IsSchaden"];
            //}

            this.Artikel.EAEingangAltLVS = myRow["EAEingangAltLVS"].ToString();
            this.Artikel.EAAusgangAltLVS = myRow["EAAusgangAltLVS"].ToString();
            this.Artikel.IsLagerArtikel = (bool)myRow["IsLagerArtikel"];
            //decTmp = 0;
            //decimal.TryParse(myRow["ADRLagerNr"].ToString(), out decTmp);
            //this.Artikel.ADRLagerNr =(int) decTmp;
            iTmp = 0;
            int.TryParse(myRow["ADRLagerNr"].ToString(), out iTmp);
            this.Artikel.ADRLagerNr = iTmp;

            this.Artikel.FreigabeAbruf = (bool)myRow["FreigabeAbruf"];
            DateTime dtTmp = DateTime.MaxValue;
            DateTime.TryParse(myRow["LZZ"].ToString(), out dtTmp);
            this.Artikel.LZZ = dtTmp;
            this.Artikel.Werk = myRow["Werk"].ToString();
            this.Artikel.Halle = myRow["Halle"].ToString();
            this.Artikel.Ebene = myRow["Ebene"].ToString();
            this.Artikel.Reihe = myRow["Reihe"].ToString();
            this.Artikel.Platz = myRow["Platz"].ToString();
            this.Artikel.exAuftrag = myRow["exAuftrag"].ToString();
            this.Artikel.exAuftragPos = myRow["exAuftragPos"].ToString();
            this.Artikel.ASNVerbraucher = myRow["ASNVerbraucher"].ToString();
            this.Artikel.UB_AltCalcEinlagerung = (bool)myRow["UB_AltCalcEinlagerung"];
            this.Artikel.UB_AltCalcAuslagerung = (bool)myRow["UB_AltCalcAuslagerung"];
            this.Artikel.UB_AltCalcLagergeld = (bool)myRow["UB_AltCalcLagergeld"];
            this.Artikel.UB_NeuCalcEinlagerung = (bool)myRow["UB_NeuCalcEinlagerung"];
            this.Artikel.UB_NeuCalcAuslagerung = (bool)myRow["UB_NeuCalcAuslagerung"];
            this.Artikel.UB_NeuCalcLagergeld = (bool)myRow["UB_NeuCalcLagergeld"];
            this.Artikel.IsVerpackt = (bool)myRow["IsVerpackt"];
            this.Artikel.interneInfo = myRow["intInfo"].ToString();
            this.Artikel.externeInfo = myRow["exInfo"].ToString();
            this.Artikel.Guete = myRow["Guete"].ToString();
            this.Artikel.IsMulde = (bool)myRow["IsMulde"];
            this.Artikel.IsLabelPrint = (bool)myRow["IsLabelPrint"];
            this.Artikel.IsProblem = (bool)myRow["IsProblem"];
            this.Artikel.IsKorStVerUse = (bool)myRow["IsKorStVerUse"];
            this.Artikel.ASNProduktionsnummer = myRow["ASNProduktionsnummer"].ToString();
            this.Artikel.IsStackable = (bool)myRow["IsStackable"];
            dtTmp = new DateTime(1900, 1, 1);
            DateTime.TryParse(myRow["GlowDate"].ToString(), out dtTmp);
            this.Artikel.GlowDate = dtTmp;

            dtTmp = new DateTime(1900, 1, 1);
            DateTime.TryParse(myRow["ScanIn"].ToString(), out dtTmp);
            this.Artikel.ScanIn = dtTmp;

            iTmp = 0;
            int.TryParse(myRow["ScanInUser"].ToString(), out iTmp);
            this.Artikel.ScanInUser = iTmp;

            dtTmp = new DateTime(1900, 1, 1);
            DateTime.TryParse(myRow["ScanOut"].ToString(), out dtTmp);
            this.Artikel.ScanOut = dtTmp;

            iTmp = 0;
            int.TryParse(myRow["ScanOutUser"].ToString(), out iTmp);
            this.Artikel.ScanOutUser = iTmp;

            dtTmp = new DateTime(1900, 1, 1);
            DateTime.TryParse(myRow["IdentifiedByScan"].ToString(), out dtTmp);
            this.Artikel.IdentifiedByScan = dtTmp;

            this.Artikel.CreatedByScanner = (bool)myRow["CreatedByScanner"];

            //--- Zusatz INFOS
            decTmp = 0;
            Decimal.TryParse(myRow["LVSNrBeforeUB"].ToString(), out decTmp);
            this.Artikel.LVSNrBeforeUB = (int)decTmp;
            decTmp = 0;
            Decimal.TryParse(myRow["LVSNrAfterUB"].ToString(), out decTmp);
            this.Artikel.LVSNrAfterUB = (int)decTmp;
            decTmp = 0;
            Decimal.TryParse(myRow["ArtIDAfterUB"].ToString(), out decTmp);
            this.Artikel.ArtIDAfterUB = (int)decTmp;
            this.Artikel.IsRL = false;


            clsSPL SPL = new clsSPL();
            SPL._GL_User = this._GL_User;
            SPL.ArtikelID = (decimal)this.Artikel.Id;
            SPL.FillLastINByArtikelID();
            this.Artikel.bSPL = SPL.IsInSPL;

            this.Artikel.IsEMECreate = false;
            this.Artikel.IsEMLCreate = false;

            //if (this.Artikel.IsLagerArtikel)
            if (!FillClsOnly)
            {
                //Eingang
                eViewData = new EingangViewData(this.Artikel.LEingangTableID, BenutzerID, false);
                this.Artikel.Eingang = eViewData.Eingang.Copy();
                this.Eingang = new Eingaenge();
                this.Eingang = this.Artikel.Eingang.Copy();


                //Ausgang
                if (this.Artikel.LAusgangTableID > 0)
                {
                    aViewData = new AusgangViewData(this.Artikel.LAusgangTableID, BenutzerID, false);
                    this.Artikel.Ausgang = aViewData.Ausgang.Copy();
                    this.Ausgang = new Ausgaenge();
                    this.Ausgang = Artikel.Ausgang.Copy();
                }

                GoodstypeViewData gtVD = new GoodstypeViewData(this.Artikel.GArtID, BenutzerID, false);
                this.Artikel.Gut = gtVD.Gut.Copy();
                //GetArtIDAfterUB();


                //artTable = clsArtikel.GetArtikelInEingangByArtID(this._GL_User, this.ID);
                //Eingang
                //eViewData = new EingangViewData(this.Artikel.LEingangTableID, BenutzerID, false);
                //Eingang = eViewData.Eingang.Copy();

                //Ausgang
                //if (this.Artikel.LAusgangTableID > 0)
                //{
                //    aViewData = new AusgangViewData(this.Artikel.LAusgangTableID, BenutzerID, false);
                //    Ausgang = aViewData.Ausgang.Copy();
                //}

                //ExtraCharge / Sonderkosten
                //ExtraChargeAssignment = new clsExtraChargeAssignment();
                //ExtraChargeAssignment.InitClass(this._GL_User);

                //Lagermeldungen
                //Lagermeldungen = new clsLagerMeldungen();
                //Lagermeldungen.InitLagerMeldungen(this);

                //Lagermeldungen.FillDictLagermeldungenSender(this.ID);
                //Lagermeldungen.FillDictLagermeldungenReceiver(this.ID);

                //Call
                //Call = new clsASNCall();
                //Call.InitClass(this._GL_User, this._GL_System, this.sys);
                //Call.ArtikelID = (Int32)this.ID;
                //Call.FillbyArtikelID();
            }
        }

        /// <summary>
        ///             GET Search Article sql - String
        /// </summary>
        public void SearchArtikelByLvsAndProductionNo(string myLvsNo, string myProductionNo)
        {
            string strSql = string.Empty;
            strSql += sql_Get_Main;
            if (!myLvsNo.Equals(string.Empty))
            {

                strSql += " WHERE " +
                            "LVS_ID ='" + myLvsNo + "' ";

                if (!myProductionNo.Equals(string.Empty))
                {

                    strSql += " AND Produktionsnummer ='" + myProductionNo + "' ";
                }
                DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "Search", "Artikel", BenutzerID);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        SetValue(r);
                    }
                }
            }
        }

        /// <summary>
        ///             GET Search Article sql - String
        /// </summary>
        public void SearchArtikelByProductionNoAndLfsNo(string myProductionNo, string myLfsNr)
        {
            string strSql = string.Empty;

            if (!myProductionNo.Equals(string.Empty))
            {
                strSql += sql_Get_Main;
                strSql += "INNER JOIN LEingang e on e.ID=a.LEingangTableID ";
                strSql += " WHERE ";
                strSql += " a.Produktionsnummer ='" + myProductionNo + "' ";
                strSql += " and LTRIM(e.LfsNr, '0') = LTRIM('" + myLfsNr + "', '0') ";
                strSql += " and a.LEingangTableID > 0 ";
                strSql += " and a.LAusgangTableID = 0 ";
                DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "Search", "Artikel", BenutzerID);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        SetValue(r);
                    }
                }
            }
        }

        public void SearchArtikelByArtIdAlt(int myId)
        {
            string strSql = string.Empty;
            strSql += sql_Get_Main;
            if (myId > 0)
            {
                strSql += " WHERE ArtIDAlt =" + myId + "; ";
                DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "Search", "Artikel", BenutzerID);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        SetValue(r);
                    }
                }
            }
        }


        public void SearchArtikelByLvsInclUB(string myLvsNo)
        {
            string strSql = string.Empty;
            if (!myLvsNo.Equals(string.Empty))
            {
                strSql += "DECLARE @Lvs as int; ";
                strSql += "SET @Lvs = " + myLvsNo + "; ";
                strSql += "IF(IsNull((Select ID FROM Artikel where ArtIDAlt = (Select ID FROM Artikel where LVS_ID = @Lvs)),0)> 0) ";
                strSql += "BEGIN ";
                strSql += " " + sql_Get_Main;
                strSql += " where ID in ((Select ID FROM Artikel where ArtIDAlt = (Select ID FROM Artikel where LVS_ID = @Lvs))); ";
                strSql += "END ";
                strSql += "ELSE ";
                strSql += "BEGIN ";
                strSql += " " + sql_Get_Main;
                strSql += " where ID in ((Select ID FROM Artikel where LVS_ID = @Lvs)); ";
                strSql += "END; ";

                DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "Search", "Artikel", BenutzerID);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        SetValue(r);
                    }
                }
            }
        }


        public List<Articles> GetArticleInStoreOutbyProductionNo(string myProductionNo)
        {
            List<Articles> lstArticles = new List<Articles>();
            string strSql = string.Empty;
            strSql += sql_Get_Main;
            if (!myProductionNo.Equals(string.Empty))
            {
                strSql += " INNER JOIN LEingang e on e.ID= a.LEingangTableID ";
                strSql += " WHERE ";
                strSql += " a.LAusgangTableID=0 ";
                strSql += " and ";
                strSql += "(CHARINDEX('" + myProductionNo + "', Produktionsnummer ) > 0) ";  //" Produktionsnummer LIKE '%" + myProductionNo + "%' ";

                //if (myProductionNo.Length > 7)
                //{ 
                //    string cuttedPCNr = myProductionNo.Substring(0, 7);
                //    strSql += " or ";
                //    strSql += " Produktionsnummer LIKE '%" + cuttedPCNr + "%' ";
                //}
                strSql += " order by a.Produktionsnummer";


                DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "Search", "Artikel", BenutzerID);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        Artikel = new Articles();
                        SetValue(r);
                        if (!lstArticles.Contains(Artikel))
                        {
                            lstArticles.Add(Artikel);
                        }
                    }
                }
            }
            return lstArticles;
        }
        /// <summary>
        ///             Check if article exist 
        /// </summary>
        /// <param name="mySearchValue"></param>
        /// <returns></returns>
        public bool ExistSearchArtikelValue(string mySearchValue, enumArticle_Datafields SearchValueDatafield)
        {
            bool bReturn = false;
            string strSql = string.Empty;
            strSql += sql_Get_Main;
            strSql += " WHERE BKZ = 1 ";
            switch (SearchValueDatafield)
            {
                case enumArticle_Datafields.LVS_ID:
                    int iTmp = 0;
                    if (int.TryParse(mySearchValue, out iTmp))
                    {
                        strSql += " AND CAST(LVS_ID as int) = " + iTmp.ToString();
                        bReturn = true;
                    }
                    break;
                case enumArticle_Datafields.Produktionsnummer:
                    if (!mySearchValue.Equals(String.Empty))
                    {
                        strSql += " AND Produktionsnummer = '" + mySearchValue + "' ";
                        bReturn = true;
                    }
                    break;
                default:
                    break;
            }


            if (bReturn)
            {
                DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "Search", "Artikel", BenutzerID);
                bReturn = (dt.Rows.Count > 0);
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                }
            }
            return bReturn;
        }

        public bool Update_WizStoreIN(ResponseEingang resEA)
        {
            bool bReturn = false;
            //string strSql = string.Empty;
            //bool isLastStep = false;
            //switch (resEA.StoreInArt)
            //{
            //    case enumStoreInArt.open:
            //        strSql = sqlCreater_WizStoreIn_Eingang.sql_String_Update_StoreIn_Open(Eingang, resEA.StoreInArt_Steps);
            //        isLastStep = resEA.StoreInArt_Steps.Equals(enumStoreInArt_Steps.wizStepLastCheckComplete);
            //        break;

            //    case enumStoreInArt.edi:
            //        strSql = sqlCreater_WizStoreIn_Eingang.sql_String_Update_StoreIn_Edi(Eingang, resEA.StoreInArt_Steps);
            //        isLastStep = resEA.StoreInArt_Steps.Equals(enumStoreInArt_Steps.wizStepLastCheckComplete);
            //        break;
            //    case enumStoreInArt.manually:
            //        strSql = sqlCreater_WizStoreIn_Eingang.sql_String_Update_StoreIn_Open(Eingang, resEA.StoreInArt_Steps);
            //        isLastStep = resEA.StoreInArt_Steps.Equals(enumStoreInArt_Steps.wizStepLastCheckComplete);
            //        break;
            //}
            //if (strSql.Length > 0)
            //{
            //    bReturn = Update_WizStoreIn(strSql, isLastStep, resEA);
            //}
            return bReturn;
        }


        public bool Update_ArticleEdit(ResponseArticle resArt)
        {
            Artikel = resArt.Article.Copy();

            bool bReturn = false;
            string strSql = string.Empty;
            bool isLastStep = false;

            strSql = sqlCreater_Article.sql_String_Update(Artikel, resArt.ArticleEdit_Step);
            //isLastStep = resEA.StoreInArt_Steps.Equals(enumStoreInArt_Steps.wizStepLastCheckComplete);
            if (strSql.Length > 0)
            {
                //+EingangViewData eVD = new EingangViewData(Eingang.Id, 1, false);
                //Eingaenge eingangOriginal = eVD.Eingang.Copy();
                bReturn = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "ArticleUpdate", BenutzerID);

                ////ArtikelVita - nicht bei dem ersten Erfassen
                //Eingang_ComparisonClass compare = new Eingang_ComparisonClass(Eingang, eingangOriginal);
                //if (!compare.LEingangChangingText.Equals(string.Empty))
                //{
                //    clsArtikelVita.LagerEingangChangeBySanner(BenutzerID, Eingang.Id, Eingang.LEingangID, compare.LEingangChangingText);
                //}

                Fill();
                eViewData = new EingangViewData(this.Artikel.LEingangTableID, BenutzerID, true);
                Artikel.Eingang = eViewData.Eingang.Copy();
            }
            return bReturn;
        }
        /// <summary>
        ///         TEST MR
        /// </summary>
        /// <param name="propDestination"></param>
        /// <param name="strReplaceValue"></param>
        public void SetValue(string propDestination, string strReplaceValue)
        {
            string strProperty = propDestination.Remove(0, propDestination.IndexOf('.') + 1);
            try
            {
                this.GetType().GetProperty(strProperty).SetValue(this, strReplaceValue, null);
                //this.GetType().GetProperty(strProperty).SetValue(this, SourceArt.GetType().GetProperty(strProperty).GetValue(SourceArt, null), null);
            }
            catch (Exception ex)
            { }
        }
        public string GetValueByProperty(string porpName)
        {
            string strReturn = string.Empty;
            try
            {
                string strProperty = porpName.Remove(0, porpName.IndexOf('.') + 1);
                PropertyInfo propInfo = this.Artikel.GetType().GetProperty(strProperty);
                if (propInfo != null)
                {
                    object value = propInfo.GetValue(this.Artikel, null);
                    if (value != null)
                    {
                        strReturn = value.ToString();
                    }
                }
            }
            catch (Exception ex)
            { }
            return strReturn;
        }
        /// <summary>
        ///             Update Prozess
        /// </summary>
        public bool Update()
        {
            //bool bOK = clsSQLcon.ExecuteSQL(this.sql_Update, BenutzerID);
            ArticleViewData tmpArtVM = new ArticleViewData(this.Artikel.Id, this._GL_User);

            bool bOK = clsSQLcon.ExecuteSQLWithTRANSACTION(sql_Update, "UpdateUser", BenutzerID);
            if (bOK)
            {
                clsLager lager = new clsLager();
                lager._GL_User = _GL_User;
                lager.LEingangTableID = Artikel.LEingangTableID;
                lager.FillLagerDaten(true);
                //ArtikelVita
                string strChangeInfo = this.CheckArtikelChangingValue(tmpArtVM.Artikel);
                if (!strChangeInfo.Equals(string.Empty))
                {
                    clsArtikelVita.ArtikelChange(this._GL_User, this.Artikel.Id, lager.Eingang.LEingangID, strChangeInfo);
                }
            }
            return bOK;
        }

        ///<summary>clsLager / UpdateArtikelLager</summary>
        ///<remarks>Update eines Datensatzes in der DB über die ID.</remarks>
        public void UpdateArtikelCheck()
        {
            string strSql = string.Empty;
            if (Artikel.EingangChecked)
            {
                strSql = "Update Artikel SET CheckArt='1' WHERE ID=" + Artikel.Id;
            }
            else
            {
                strSql = "Update Artikel SET CheckArt='0' WHERE ID=" + Artikel.Id;
            }
            bool bExecOK = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "update", BenutzerID);

            if ((bExecOK) && (Artikel.EingangChecked))
            {
                decimal tmpLEingangTableID = Artikel.Eingang.Id; // clsArtikel.GetLEingangTableIDByID(myGLUser, myDecArtTableID);
                decimal tmpLEingangID = Artikel.Eingang.LEingangID; // clsLager.GetLEingangIDByLEingangTableID(myGLUser.User_ID, tmpLEingangTableID);
                decimal tmpLVSNR = Artikel.LVS_ID; // clsArtikel.GetLVSNrByArtikelTableID(myGLUser, myDecArtTableID);
                clsArtikelVita.ArtikelChecked(BenutzerID, Artikel.Id, tmpLEingangID, tmpLVSNR);
            }
        }
        //public void Update()
        //{
        //    //bool bOK = clsSQLcon.ExecuteSQL(this.sql_Update, BenutzerID);
        //    ArticleViewData tmpArtVM = new ArticleViewData(this.Artikel.Id, this._GL_User);

        //    bool bOK = clsSQLcon.ExecuteSQLWithTRANSACTION(sql_Update, "UpdateUser", BenutzerID);
        //    if (bOK)
        //    {
        //        clsLager lager = new clsLager();
        //        lager._GL_User = _GL_User;
        //        lager.LEingangTableID = Artikel.LEingangTableID;
        //        lager.FillLagerDaten(true);
        //        //ArtikelVita
        //        string strChangeInfo = this.CheckArtikelChangingValue(tmpArtVM.Artikel);
        //        if (!strChangeInfo.Equals(string.Empty))
        //        {
        //            clsArtikelVita.ArtikelChange(this._GL_User, this.Artikel.Id, lager.Eingang.LEingangID, strChangeInfo);
        //            //Add Logbucheintrag 
        //            //string myBeschreibung = "Artikel geändert: LVS-NR [" + Artikel.LVS_ID.ToString() + "] / Eingang [" + lager.Eingang.LEingangID.ToString() + "] ";
        //            //myBeschreibung = myBeschreibung + Environment.NewLine + strChangeInfo;
        //            //Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), myBeschreibung);
        //        }
        //    }
        //}
        /// <summary>
        ///             Update Prozess store location
        /// </summary>
        public bool Update_StoreLocation()
        {
            ArticleViewData tmpArtVM = new ArticleViewData(this.Artikel.Id, this._GL_User);
            bool bOK = clsSQLcon.ExecuteSQLWithTRANSACTION(sql_Update_StoredLocation, "UpdateUser", BenutzerID);
            if (bOK)
            {
                Fill();
                //ArtikelVita
                ArticleChangingValue = string.Empty;
                ArticleChangingValue = this.CheckArtikelChangingValue(tmpArtVM.Artikel);
                if (!this.ArticleChangingValue.Equals(string.Empty))
                {
                    EingangViewData eVD = new EingangViewData(tmpArtVM.Artikel.LEingangTableID, BenutzerID, false);
                    clsArtikelVita.ArtikelChangeByScan(this._GL_User, this.Artikel.Id, eVD.Eingang.LEingangID, this.ArticleChangingValue);
                }
            }
            return bOK;
        }

        /// <summary>
        ///             Update
        /// </summary>
        public bool Update_ScanValue(enumAppProcess appProcess)
        {
            bool bReturn = false;
            string strSql = string.Empty;
            //ArticleViewData tmpArtVM = new ArticleViewData(this.Artikel.Id, this._GL_User);
            switch (appProcess)
            {
                case enumAppProcess.StoreIn:
                    bReturn = clsSQLcon.ExecuteSQLWithTRANSACTION(sql_Update_ScanVal_StoreIn, "UpdateUser", Artikel.ScanInUser);
                    if (bReturn)
                    {
                        Update_ScanIdentification();
                    }
                    break;
                case enumAppProcess.StoreOut:
                    bReturn = clsSQLcon.ExecuteSQLWithTRANSACTION(sql_Update_ScanVal_StoreOut, "UpdateUser", Artikel.ScanOutUser);
                    if (bReturn)
                    {
                        Update_ScanIdentification();
                    }
                    break;
            }
            if (bReturn)
            {
                Fill();
                EingangViewData eVD = new EingangViewData(Artikel.LEingangTableID, BenutzerID, Artikel.AbBereichID, false);

                //ArtikelVita
                ArticleChangingValue = string.Empty;
                ArticleChangingValue = this.CheckArtikelChangingValue(this.Artikel);

                if (!this.ArticleChangingValue.Equals(string.Empty))
                {
                    clsArtikelVita.ArtikelChangeByScan(this._GL_User, this.Artikel.Id, this.Eingang.LEingangID, this.ArticleChangingValue);
                }
            }
            return bReturn;
        }


        /// <summary>
        ///             Update Prozess store location
        /// </summary>
        public bool Update_Checked(enumAppProcess appProcess)
        {
            bool bReturn = false;
            string strSql = string.Empty;
            //ArticleViewData tmpArtVM = new ArticleViewData(this.Artikel.Id, this._GL_User);
            switch (appProcess)
            {
                case enumAppProcess.StoreIn:
                    bReturn = clsSQLcon.ExecuteSQLWithTRANSACTION(sql_Update_Checked_StoreIn, "UpdateUser", Artikel.ScanInUser);
                    break;
                case enumAppProcess.StoreOut:
                    bReturn = clsSQLcon.ExecuteSQLWithTRANSACTION(sql_Update_Checked_StoreOut, "UpdateUser", Artikel.ScanOutUser);
                    break;
            }
            if (bReturn)
            {
                Fill();
                EingangViewData eVD = new EingangViewData(Artikel.LEingangTableID, BenutzerID, Artikel.AbBereichID, false);
                clsArtikelVita.ArtikelCheckedByScan(BenutzerID, Artikel.Id, eVD.Eingang.LEingangID, Artikel.LVS_ID);
            }
            return bReturn;
        }

        /// <summary>
        ///             Update Prozess store location
        /// </summary>
        public bool Update_ScanIdentification()
        {
            bool bReturn = false;
            string strSql = string.Empty;
            bReturn = clsSQLcon.ExecuteSQLWithTRANSACTION(sql_Update_ScanIdentification, "UpdateUser", BenutzerID);
            if (bReturn)
            {
                Fill();
                EingangViewData eVD = new EingangViewData(Artikel.LEingangTableID, BenutzerID, Artikel.AbBereichID, false);
                //clsArtikelVita.ArtikelCheckedByScan(BenutzerID, Artikel.Id, eVD.Eingang.LEingangID, Artikel.LVS_ID);
            }
            return bReturn;
        }
        /// <summary>
        ///             Update Bestellnummer BMW Update
        /// </summary>
        public bool Update_BestellNrUpdateByBMW()
        {
            bool bReturn = false;
            string strSql = "Update Artikel SET ";
            strSql += "Bestellnummer = '" + Artikel.Bestellnummer + "' ";
            strSql += ", intInfo = '" + Artikel.interneInfo + "' ";
            strSql += " where ";
            strSql += " ID = " + Artikel.Id;
            bReturn = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "UpdateBestellNrArticel", 1);
            return bReturn;
        }
        /// <summary>
        ///             Update Prozess store location
        /// </summary>
        public bool Update_ManualEdit()
        {
            bool bReturn = false;
            string strSql = string.Empty;
            bReturn = clsSQLcon.ExecuteSQLWithTRANSACTION(sql_Update_ScanIdentification, "UpdateUser", BenutzerID);
            if (bReturn)
            {
                Fill();
                EingangViewData eVD = new EingangViewData(Artikel.LEingangTableID, BenutzerID, Artikel.AbBereichID, false);
                //clsArtikelVita.ArtikelCheckedByScan(BenutzerID, Artikel.Id, eVD.Eingang.LEingangID, Artikel.LVS_ID);
            }
            return bReturn;
        }

        ///<summary> / CheckArtikelChangingValue</summary>
        ///<remarks></remarks>
        private string CheckArtikelChangingValue(Articles clsArtToCompare)
        {
            string ArtikelChangingText = string.Empty;
            Type typeSource = this.Artikel.GetType();
            PropertyInfo[] pInfoSource = typeSource.GetProperties();
            Type typeCompare = clsArtToCompare.GetType();
            PropertyInfo[] pInfoCompare = typeCompare.GetProperties();

            clsObjPropertyChanges objPropCh = new clsObjPropertyChanges();
            List<clsObjPropertyChanges> listToAdd = new List<clsObjPropertyChanges>();

            foreach (PropertyInfo info in pInfoSource)
            {
                objPropCh = new clsObjPropertyChanges();
                objPropCh.TableId = (int)this.Artikel.Id;
                objPropCh.TableName = clsObjPropertyChanges.TableName_Artikel;
                objPropCh.UserId = (int)this.BenutzerID;

                if ((info.CanRead) & (info.CanWrite))
                {
                    object NewValue;
                    object oldValue;
                    string PropName = info.Name.ToString();
                    objPropCh.Property = PropName;

                    switch ("Artikel." + PropName)
                    {
                        case clsArtikel.ArtikelField_Bestellnummer:
                        case clsArtikel.ArtikelField_Charge:
                        case clsArtikel.ArtikelField_exAuftrag:
                        case clsArtikel.ArtikelField_exAuftragPos:
                        case clsArtikel.ArtikelField_exBezeichnung:
                        case clsArtikel.ArtikelField_exMaterialnummer:
                        case clsArtikel.ArtikelField_Gut:
                        case clsArtikel.ArtikelField_Produktionsnummer:
                        case clsArtikel.ArtikelField_Werksnummer:
                        case clsArtikel.ArtikelField_Einheit:
                        case clsArtikel.ArtikelField_Werk:
                        case clsArtikel.ArtikelField_Reihe:
                        case clsArtikel.ArtikelField_Ebene:
                        case clsArtikel.ArtikelField_Platz:
                        case clsArtikel.ArtikelField_Halle:
                        case clsArtikel.ArtikelField_Position:

                            NewValue = string.Empty;
                            oldValue = string.Empty;
                            NewValue = info.GetValue(this.Artikel, null);
                            oldValue = typeCompare.GetProperty(PropName).GetValue(clsArtToCompare, null);
                            if (NewValue is object)
                            {
                                if (!NewValue.ToString().Equals(oldValue.ToString()))
                                {
                                    objPropCh.ValueOld = oldValue.ToString();
                                    objPropCh.ValueNew = NewValue.ToString();
                                    listToAdd.Add(objPropCh);

                                    ArtikelChangingText += String.Format("{0}\t{1}{2}", PropName + ":", "[" + oldValue.ToString() + "] ", ">>> [" + NewValue.ToString() + "]") + Environment.NewLine;
                                }
                            }
                            break;

                        case clsArtikel.ArtikelField_Anzahl:
                            Int32 iNewValue = 0;
                            Int32 ioldValue = 0;
                            NewValue = info.GetValue(this.Artikel, null);
                            oldValue = typeCompare.GetProperty(PropName).GetValue(clsArtToCompare, null);
                            Int32.TryParse(NewValue.ToString(), out iNewValue);
                            Int32.TryParse(oldValue.ToString(), out ioldValue);
                            if (iNewValue != ioldValue)
                            {
                                objPropCh.ValueOld = oldValue.ToString();
                                objPropCh.ValueNew = NewValue.ToString();
                                listToAdd.Add(objPropCh);

                                ArtikelChangingText += String.Format("{0}\t{1}{2}", PropName + ":", "[" + ioldValue.ToString() + "] ", ">>> [" + NewValue.ToString() + "]") + Environment.NewLine;
                            }
                            break;

                        case clsArtikel.ArtikelField_Dicke:
                        case clsArtikel.ArtikelField_Breite:
                        case clsArtikel.ArtikelField_Länge:
                        case clsArtikel.ArtikelField_Höhe:
                        case clsArtikel.ArtikelField_Netto:
                        case clsArtikel.ArtikelField_Brutto:
                            decimal decNewValue = 0;
                            decimal decoldValue = 0;
                            NewValue = info.GetValue(this.Artikel, null);
                            oldValue = typeCompare.GetProperty(PropName).GetValue(clsArtToCompare, null);
                            decimal.TryParse(NewValue.ToString(), out decNewValue);
                            decimal.TryParse(oldValue.ToString(), out decoldValue);
                            if (decNewValue != decoldValue)
                            {
                                objPropCh.ValueOld = oldValue.ToString();
                                objPropCh.ValueNew = NewValue.ToString();
                                listToAdd.Add(objPropCh);

                                ArtikelChangingText += String.Format("{0}\t{1}{2}", PropName + ":", "[" + Functions.FormatDecimal(decoldValue) + "] ", ">>> [" + Functions.FormatDecimal(decNewValue) + "]") + Environment.NewLine;
                            }
                            break;
                    }
                }
            }
            if (!ArtikelChangingText.Equals(string.Empty))
            {
                ArtikelChangingText = "Folgende Ängerungen wurden vorgenommen: " + Environment.NewLine + ArtikelChangingText;

                clsObjPropertyChanges.AddObjPropertyChanges(this._GL_User, listToAdd);
            }
            return ArtikelChangingText;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propDestination"></param>
        /// <param name="listPropSource"></param>
        public void CombinateValue(string myKeyColTarget, clsASNTableCombiValue myCombiValue)
        {
            try
            {
                List<string> listPropSource = myCombiValue.ListColsForCombination;

                // Initialisiere den kombinierten Wert
                string combinedValue = string.Empty;
                foreach (var propName in listPropSource)
                {
                    // Hole die Property-Info des aktuellen Property-Namens
                    var property = this.Artikel.GetType().GetProperty(propName);
                    if (property != null)
                    {
                        // Hole den Wert des Properties
                        var value = property.GetValue(this.Artikel);
                        if (value != null)
                        {
                            // Kombiniere die Werte (z. B. durch ein Leerzeichen getrennt)
                            combinedValue += value.ToString();
                            if (myCombiValue.UseValueSeparator)
                            {
                                combinedValue += myCombiValue.ValueSeparator;
                            }
                        }
                    }
                }
                // Entferne das letzte ValueSeparator
                if (myCombiValue.UseValueSeparator)
                {
                    combinedValue = combinedValue.TrimEnd(myCombiValue.ValueSeparator.ToCharArray());
                }

                // Setze den kombinierten Wert in das Ziel-Property
                var targetProperty = this.Artikel.GetType().GetProperty(myKeyColTarget);
                if (targetProperty != null && targetProperty.CanWrite)
                {
                    targetProperty.SetValue(this.Artikel, combinedValue);
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mySourceProperty"></param>
        /// <param name="myDestinationProprty"></param>
        public void CopyPropertyValueToProperty(string mySourceProperty, string myDestinationProprty)
        {
            try
            {
                string s = string.Empty;
                string SourceProp = mySourceProperty.StartsWith("Artikel.")
                                    ? mySourceProperty.Substring("Artikel.".Length)
                                    : mySourceProperty;

                string DestinationProp = myDestinationProprty.StartsWith("Artikel.")
                                    ? myDestinationProprty.Substring("Artikel.".Length)
                                    : myDestinationProprty;

                // Hole die Property-Info des aktuellen Property-Namens
                var propertySource = this.Artikel.GetType().GetProperty(SourceProp);
                // Setze den kombinierten Wert in das Ziel-Property
                var propertyDestination = this.Artikel.GetType().GetProperty(DestinationProp);

                if ((propertySource != null) && (propertyDestination != null) && (propertyDestination.CanWrite))
                {
                    // Hole den Wert des Properties
                    var valueSource = propertySource.GetValue(this.Artikel);
                    if (valueSource != null)
                    {
                        propertyDestination.SetValue(this.Artikel, valueSource);
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
        }

        ///-----------------------------------------------------------------------------------------------------
        ///                             STATIC  Function / Procedure
        ///-----------------------------------------------------------------------------------------------------
        ///<summary>UsersViewModel / SetUserFontSize</summary>
        ///<remarks></remarks>
        public static void SetUserFontSize(Globals._GL_USER _GL_User)
        {
            string strSql = string.Empty;
            strSql = "Update [User] SET FontSize='" + _GL_User.us_decFontSize.ToString().Replace(",", ".") + "' " +
                                                     "WHERE ID=" + (int)_GL_User.User_ID + " ;";
            clsSQLcon.ExecuteSQL(strSql, _GL_User.User_ID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataTableArtikelSchema()
        {
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM Artikel WHERE ID=0";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, 1, "Artikel");
            return dt;
        }

    }
}

