using Common.Enumerations;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    /// <summary>
    ///                 Güterart
    /// </summary>
    public class GoodstypeViewData
    {
        public decimal BenutzerID { get; set; } = 0;
        public Goodstypes Gut { get; set; }
        public List<Goodstypes> ListGueterarten { get; set; } = new List<Goodstypes>();
        public GoodstypeViewData()
        {
            InitCls();
        }
        public GoodstypeViewData(int myUserId) : this()
        {
            BenutzerID = myUserId;
        }
        public GoodstypeViewData(Goodstypes myGoodtypes, int myUserId) : this()
        {
            Gut = myGoodtypes;
            BenutzerID = myUserId;
        }
        public GoodstypeViewData(int myGoodTypeId, int myUserId, bool myLoadAll = false) : this()
        {
            BenutzerID = myUserId;
            if (myGoodTypeId > 0)
            {
                Gut.Id = myGoodTypeId;
                Fill();
                if (myLoadAll)
                {

                }
            }
        }

        private void InitCls()
        {
            Gut = new Goodstypes();
        }
        public void Fill()
        {
            string strSQL = sql_Get;
            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "GoodArt", "GoodArt", BenutzerID);
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
            Gut = new Goodstypes();

            int iTmp = 0;
            int.TryParse(row["ID"].ToString(), out iTmp);
            Gut.Id = iTmp;
            Gut.ViewID = row["ViewID"].ToString();
            Gut.Bezeichnung = row["Bezeichnung"].ToString().Trim();
            decimal decTmp = 0M;
            decimal.TryParse(row["Dicke"].ToString(), out decTmp);
            Gut.Dicke = decTmp;
            decTmp = 0;
            decimal.TryParse(row["Breite"].ToString(), out decTmp);
            Gut.Breite = decTmp;
            decTmp = 0;
            decimal.TryParse(row["Laenge"].ToString(), out decTmp);
            Gut.Laenge = decTmp;
            decTmp = 0;
            decimal.TryParse(row["Hoehe"].ToString(), out decTmp);
            Gut.Hoehe = decTmp;
            iTmp = 0;
            int.TryParse(row["MassAnzahl"].ToString(), out iTmp);
            Gut.MassAnzahl = iTmp;
            decTmp = 0;
            decimal.TryParse(row["Netto"].ToString(), out decTmp);
            Gut.Netto = decTmp;
            decTmp = 0;
            Decimal.TryParse(row["Brutto"].ToString(), out decTmp);
            Gut.Brutto = decTmp;
            Gut.ArtikelArt = row["ArtikelArt"].ToString().Trim(); ;
            Gut.Besonderheit = row["Besonderheit"].ToString().Trim();
            Gut.Verpackung = row["Verpackung"].ToString().Trim(); ;
            Gut.AbsteckBolzenNr = row["AbsteckBolzenNr"].ToString().Trim();
            iTmp = 0;
            int.TryParse(row["MEAbsteckBolzen"].ToString(), out iTmp);
            Gut.MEAbsteckBolzen = iTmp;
            iTmp = 0;
            int.TryParse(row["Arbeitsbereich"].ToString(), out iTmp);
            Gut.ArbeitsbereichId = iTmp;

            iTmp = 0;
            int.TryParse(row["LieferantenID"].ToString(), out iTmp);
            Gut.LieferantenId = iTmp;
            Gut.Aktiv = (bool)row["aktiv"];
            decTmp = 0;
            int.TryParse(row["Mindestbestand"].ToString(), out iTmp);
            Gut.MindestBestand = iTmp;
            Gut.BestellNr = row["BestellNr"].ToString().Trim();
            Gut.Zusatz = row["Zusatz"].ToString();
            Gut.Einheit = row["Einheit"].ToString();
            Gut.Verweis = row["Verweis"].ToString();
            Gut.Werksnummer = row["Werksnummer"].ToString().Trim();
            Gut.IsStackable = (bool)row["IsStackable"];
            Gut.UseProdNrCheck = (bool)row["UseProdNrCheck"];
            Gut.tmpLiefVerweis = string.Empty;
            if (row["tmpLiefVerweis"] != null)
            {
                Gut.tmpLiefVerweis = row["tmpLiefVerweis"].ToString();
            }
            Gut.DelforVerweis = string.Empty;
            if (row["DelforVerweis"] != null)
            {
                Gut.DelforVerweis = row["DelforVerweis"].ToString();
            }
            Gut.IgnoreEdi = false;
            if (row["IgnoreEdi"] != null)
            {
                Gut.IgnoreEdi = (bool)row["IgnoreEdi"]; ;
            }

            //StyleSheet
            //Style = new clsStyleSheetColumn();
            //Style._GL_User = this.GLUser;
            //Style.FTable = "Gueterarten";
            //Style.FTableID = this.ID;
            //Style.GetDataTableStyleSheet();

            if (Gut.ViewID.Equals("418"))
            {
                string st = string.Empty;
            }

            //GutADR = new clsGueterartADR();
            //GutADR.InitClass(this.GLUser, this.GLSystem);
            //GutADR.GArtID = this.ID;
            //GutADR.AbBereichID = this.GLSystem.sys_ArbeitsbereichID;
            //this.VDA4905LieferantenInfo = GutADR.AssignAdrsAsString;

            //AbBereichGut = new clsArbeitsbereichGArten();
            //AbBereichGut.AbBereichID = this.GLSystem.sys_ArbeitsbereichID;
            //AbBereichGut.GArtID = this.ID;

        }

        /// <summary>
        ///             ADD
        /// </summary>
        public void Add()
        {
            string strSql = sql_Add;
            strSql = strSql + "Select @@IDENTITY as 'ID';";

            string strTmp = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSql, "Insert", BenutzerID);
            int.TryParse(strTmp, out int iTmp);
            Gut.Id = iTmp;
        }

        /// <summary>
        ///             GET sql - String
        /// </summary>
        public string sql_Get_aktiv
        {
            get
            {
                string strSql = string.Empty;
                strSql += "SELECT DISTINCT g.* ";
                strSql += " FROM Gueterart g ";
                strSql += " INNER JOIN ArbeitsbereichGArten ag on ag.AbBereichID=g.Arbeitsbereich ";
                strSql += " LEFT JOIN GueterartADR ga on ga.GArtID = g.ID ";
                strSql += " where ";
                strSql += " (g.Id=1) or ";
                strSql += " g.aktiv=1";
                return strSql;
            }
        }

        public string sql_GetListAll
        {
            get
            {
                string strSql = string.Empty;
                strSql += "SELECT g.* ";
                strSql += " FROM Gueterart g ";
                //strSql += " INNER JOIN ArbeitsbereichGArten ag on ag.AbBereichID=g.Arbeitsbereich ";
                //strSql += " LEFT JOIN GueterartADR ga on ga.GArtID = g.ID ";
                //strSql += " where ";
                //strSql += " (g.Id=1) or ";
                //strSql += " g.aktiv=1";
                return strSql;
            }
        }
        public string sql_Get_aktivWithoutAllGoodtypes
        {
            get
            {
                string strSql = string.Empty;
                strSql += "SELECT DISTINCT g.* ";
                strSql += " FROM Gueterart g ";
                strSql += " INNER JOIN ArbeitsbereichGArten ag on ag.AbBereichID=g.Arbeitsbereich ";
                strSql += " LEFT JOIN GueterartADR ga on ga.GArtID = g.ID ";
                strSql += " where ";
                //strSql += " (g.Id=1) or ";
                strSql += " g.aktiv=1";
                return strSql;
            }
        }

        public void GetGoodtypeListAll()
        {
            ListGueterarten = new List<Goodstypes>();
            string strSql = string.Empty;
            strSql += sql_GetListAll;

            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "GetList", "ListGut", BenutzerID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    SetValue(r);
                    if (!ListGueterarten.Contains(Gut))
                    {
                        ListGueterarten.Add(Gut);
                    }
                }
            }
        }
        public void GetGoodtypeListByWorkspace(int myWorkspaceId)
        {
            ListGueterarten = new List<Goodstypes>();
            string strSql = string.Empty;
            strSql += sql_Get_aktiv;
            strSql += " and g.Arbeitsbereich=" + myWorkspaceId;
            strSql += " order by g.ViewID";

            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "GetList", "ListGut", BenutzerID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    SetValue(r);
                    if (!ListGueterarten.Contains(Gut))
                    {
                        ListGueterarten.Add(Gut);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myWorkspaceId"></param>
        /// <param name="myAdrId"></param>
        public void GetGoodtypeListByWorkspaceAndAddress(int myWorkspaceId, int myAdrId)
        {
            ListGueterarten = new List<Goodstypes>();
            string strSql = string.Empty;
            strSql += sql_Get_aktiv;
            strSql += " and g.Arbeitsbereich=" + myWorkspaceId;
            strSql += " and ga.AdrID=" + myAdrId;
            strSql += " order by g.ViewID";

            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "GetList", "ListGut", BenutzerID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    SetValue(r);
                    if (!ListGueterarten.Contains(Gut))
                    {
                        ListGueterarten.Add(Gut);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myWorkspaceId"></param>
        /// <param name="myAdrId"></param>
        public Goodstypes GetGoodtypeByWorkspaceAndAddressAndWerksnummer(int myWorkspaceId, int myAdrId, string myWerksnummer)
        {
            string strSql = string.Empty;
            strSql += sql_Get_aktivWithoutAllGoodtypes;
            strSql += " and g.Arbeitsbereich=" + myWorkspaceId;
            strSql += " and ga.AdrID=" + myAdrId;
            strSql += " and REPLACE(g.Werksnummer, ' ', '') = REPLACE('" + myWerksnummer + "', ' ', '') ";
            strSql += " order by g.ViewID";

            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "GetList", "ListGut", BenutzerID);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        SetValue(r);
                    }
                }
                else
                {
                    DataRow r = dt.Rows[1];
                    SetValue(r);
                }
            }
            return Gut;
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
            //if (Artikel.Id > 0)
            //{
            //    //ID = myArtID;
            //    //GetArtikeldatenByTableID();
            //    //decimal decTmpLagerEingangID = clsLager.GetLEingangIDByLEingangTableID(this._GL_User.User_ID, this.LEingangTableID);

            //    string strSql = string.Empty;
            //    strSql = sql_Delete;
            //    //bool mybExecOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            //    bool mybExecOK = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "DELETE",BenutzerID);
            //    //Logbuch Eintrag
            //    if (mybExecOK)
            //    {
            //        //Add Logbucheintrag 
            //        string myBeschreibung = "Artikel gelöscht: Artikel ID [" + Artikel.Id.ToString() + "] ";
            //        Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);
            //    }
            //}
        }
        /// <summary>
        ///             UPDATE
        /// </summary>
        /// <returns></returns>
        public bool Update_WizStoreOut(enumStoreOutArt myStoreOutArt, enumStoreOutArt_Steps myStoreOutArt_Steps)
        {
            bool retVal = false;
            string strSql = string.Empty;
            bool IsLastStep = false;
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
            return retVal;
        }

        public bool Update_BestellNrChange()
        {
            bool retVal = false;
            string strSql = "Update Gueterart SET ";
            strSql += "BestellNr = '" + Gut.BestellNr + "' ";
            strSql += ", Besonderheit = '" + Gut.Besonderheit + "' ";
            strSql += "where ID = " + Gut.Id;

            retVal = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "Update", BenutzerID);
            return retVal;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            bool retVal = false;
            string strSql = sql_Update;
            retVal = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "Update", BenutzerID);
            return retVal;
        }


        public Articles SetGoodtypeValueToArticle(Articles myArticle)
        {
            myArticle.GArtID = Gut.Id;
            if (!Gut.Werksnummer.Equals(string.Empty))
            {
                myArticle.Werksnummer = Gut.Werksnummer;
            }
            myArticle.Einheit = Gut.Einheit;
            if (myArticle.Dicke == 0M)
            {
                myArticle.Dicke = Gut.Dicke;
            }
            if (myArticle.Breite == 0M)
            {
                myArticle.Breite = Gut.Breite;
            }
            if (myArticle.Laenge == 0M)
            {
                myArticle.Laenge = Gut.Laenge;
            }
            if (myArticle.Hoehe == 0M)
            {
                myArticle.Hoehe = Gut.Hoehe;
            }
            if ((myArticle.Netto == 0) && (Gut.Netto > 0))
            {
                myArticle.Netto = Gut.Netto;
            }
            if ((myArticle.Brutto == 0) && (Gut.Brutto > 0))
            {
                myArticle.Brutto = Gut.Brutto;
            }
            //-- IsMulde
            if (
                    (Gut.ArtikelArt.IndexOf("COIL") > -1) ||
                    (Gut.ArtikelArt.IndexOf("Coil") > -1)
                )
            {
                myArticle.IsMulde = true;
            }
            //-- IsStackable
            myArticle.IsStackable = Gut.IsStackable;
            return myArticle;
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
                strSql = "SELECT * FROM Gueterart WHERE ID=" + Gut.Id + ";";
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
                if (Gut.Id == 1)
                {
                    Gut.ViewID = "0";
                    Gut.Bezeichnung = "Alle Güter";
                    Gut.Dicke = 0;
                    Gut.Breite = 0;
                    Gut.Laenge = 0;
                    Gut.Hoehe = 0;
                    Gut.Aktiv = true;
                    Gut.MassAnzahl = 4;
                }
                strSql = "Update Gueterart SET ViewID ='" + Gut.ViewID + "' " +
                                              ", Bezeichnung ='" + Gut.Bezeichnung + "' " +
                                              ", Dicke ='" + Gut.Dicke.ToString().Replace(",", ".") + "' " +
                                              ", Breite ='" + Gut.Breite.ToString().Replace(",", ".") + "' " +
                                              ", Laenge ='" + Gut.Laenge.ToString().Replace(",", ".") + "' " +
                                              ", Hoehe ='" + Gut.Hoehe.ToString().Replace(",", ".") + "' " +
                                              ", MassAnzahl =" + Gut.MassAnzahl +
                                              ", Netto='" + Gut.Netto.ToString().Replace(",", ".") + "' " +
                                              ", Brutto='" + Gut.Brutto.ToString().Replace(",", ".") + "' " +
                                              ", ArtikelArt='" + Gut.ArtikelArt + "' " +
                                              ", Besonderheit='" + Gut.Besonderheit + "' " +
                                              ", Verpackung ='" + Gut.Verpackung + "' " +
                                              ", AbsteckBolzenNr='" + Gut.AbsteckBolzenNr + "' " +
                                              ", MEAbsteckBolzen =" + Gut.MEAbsteckBolzen +
                                              ", Arbeitsbereich =" + Gut.ArbeitsbereichId +
                                              ", LieferantenID =" + Gut.LieferantenId +
                                              ", aktiv=" + Convert.ToInt32(Gut.Aktiv) +
                                              ", Mindestbestand='" + Gut.MindestBestand.ToString().Replace(",", ".") + "'" +
                                              ", BestellNr='" + Gut.BestellNr + "' " +
                                              ", Zusatz ='" + Gut.Zusatz + "'" +
                                              ", Einheit ='" + Gut.Einheit + "'" +
                                              ", Verweis ='" + Gut.Verweis + "'" +
                                              ", Werksnummer ='" + Gut.Werksnummer + "'" +
                                              ", IsStackable = " + Convert.ToInt32(Gut.IsStackable) +
                                              ", UseProdNrCheck= " + Convert.ToInt32(Gut.UseProdNrCheck) +
                                              ", tmpLiefVerweis= '" + this.Gut.tmpLiefVerweis + "'" +
                                              ", DelforVerweis = '" + this.Gut.DelforVerweis + "'" +
                                              ", IgnoreEdi =" + Convert.ToInt32(Gut.IgnoreEdi) +

                                                                " WHERE ID=" + Gut.Id + ";";
                return strSql;
            }
        }

        //*************************************************************************************
        //                                          static
        //************************************************************************************

        public static int GetGutByADRAndVerweis(int myUserId, int myAdrId, string myVerweis, int WorkspaceId)
        {
            int iReturn = 0;
            string strSQL = "Select Top(1) g.ID FROM Gueterart g ";
            strSQL += "INNER JOIN GueterartADR gADR on gADR.GArtID=g.ID ";
            strSQL += "WHERE ";
            strSQL += "REPLACE(g.Verweis, ' ', '') = REPLACE('" + myVerweis + "', ' ', '') ";
            strSQL += " AND gADR.AbBereichID= " + WorkspaceId;
            strSQL += " AND gADR.AdrID=" + myAdrId;
            strSQL += " Order by g.ID ;";

            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, myUserId);
            int iTmp = 0;
            int.TryParse(strTmp, out iTmp);
            iReturn = iTmp;
            if (iReturn == 0)
            {
                //es liegt keine Verweis vor jetzt prüfen, ob wir für diese Adresse eine Default Güterart haben

                strSQL = string.Empty;
                strSQL = "Select Top(1) GArtID FROM KundGArtDefault ";
                strSQL += "WHERE ";
                strSQL += "AdrID = " + myAdrId;
                strSQL += " AND AbBereichID = " + WorkspaceId;

                strTmp = string.Empty;
                strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, myUserId);
                iTmp = 0;
                int.TryParse(strTmp, out iTmp);
                if (iTmp > 0)
                {
                    iReturn = iTmp;
                }
            }
            return iReturn;
        }

        public static Goodstypes CreateNewGArtByASN(int myAdrID, string myVerweis, int myWorkspaceId)
        {
            //bool bReturn = false;

            GoodstypeViewData gtVD = new GoodstypeViewData();
            gtVD.Gut.ViewID = myVerweis;
            gtVD.Gut.Bezeichnung = myVerweis;
            gtVD.Gut.Einheit = "KG";
            gtVD.Gut.Verweis = myVerweis;
            gtVD.Gut.Werksnummer = myVerweis;
            gtVD.Gut.tmpLiefVerweis = string.Empty;
            gtVD.Gut.IgnoreEdi = false;

            string strSQL = string.Empty;
            //neu GArt anlegen
            strSQL = "DECLARE @GArtID as int; ";
            strSQL = strSQL + gtVD.sql_Add;
            strSQL = strSQL + " SET @GArtID=(Select @@IDENTITY as 'ID' ); ";
            //Verweis Arbeitsberech/Güterart
            strSQL = strSQL + "INSERT INTO ArbeitsbereichGArten (AbBereichID, GArtID) " +
                                               "VALUES (" + myWorkspaceId +
                                                        ", @GArtID" +
                                                        "); ";
            //Verweis Güterart Adresse
            strSQL = strSQL + "INSERT INTO GueterartADR (GArtID, AdrID, AbBereichID) " +
                                       "VALUES (@GArtID " +
                                                 ", " + myAdrID +
                                                 ", " + myWorkspaceId +
                                                "); ";
            strSQL = strSQL + "Select @GArtID;";
            string strTmp = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSQL, "NeueGArt", 1);
            int iTmp = 0;
            int.TryParse(strTmp, out iTmp);
            if (iTmp > 0)
            {
                gtVD.Gut.Id = iTmp;
                //this.Fill();
                //bReturn = true;
            }
            return gtVD.Gut;
        }

    }
}

