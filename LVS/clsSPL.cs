
using Common.Enumerations;
using Common.Models;
using LVS.ViewData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LVS
{
    public class clsSPL
    {

        public Globals._GL_USER _GL_User;
        //public Globals._GL_SYSTEM _GL_SYSTEM;

        public DataTable dtCheckOut = new DataTable();
        public List<Int32> lstCheckOut = new List<Int32>();
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

        public decimal SPLID { get; set; }
        public decimal ArtikelID { get; set; }
        public DateTime Datum { get; set; }
        public string BKZ { get; set; }
        public decimal SPLIDIn { get; set; }
        private bool _IsInSPL;
        public bool IsInSPL
        {
            get
            {
                _IsInSPL = CheckArtikelInSPL();
                return _IsInSPL;
            }
            set { _IsInSPL = value; }
        }
        public bool IsCustomCertificateMissing { get; set; }
        public Int32 DefWindungen { get; set; }
        public string Sperrgrund { get; set; }
        public string Vermerk { get; set; }

        /******************************************************************************
         *                          Mehtoden
         * ***************************************************************************/
        ///<summary>clsSPL / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUser)
        {
            this._GL_User = myGLUser;
            //this._GL_SYSTEM = myGLSystem;
        }
        ///<summary>clsSPL / AddStrSQL</summary>
        ///<remarks></remarks>
        public string AddStrSQL(bool bEinbuchung)
        {
            if (bEinbuchung)
            {
                BKZ = "IN";
                SPLIDIn = 0;
            }
            else
            {
                BKZ = "OUT";
            }
            Datum = DateTime.Now;

            string strSql = string.Empty;
            strSql = " INSERT INTO Sperrlager (ArtikelID, UserID, Datum, BKZ, SPLIDIn, DefWindungen, Sperrgrund, Vermerk, IsCustomCertificateMissing ) VALUES ("
                                                        + (Int32)ArtikelID +
                                                        "," + BenutzerID +
                                                        ",'" + Datum + "'" +
                                                        ",'" + BKZ + "'" +
                                                        "," + (Int32)SPLIDIn +
                                                        ", " + DefWindungen +
                                                        ",'" + Sperrgrund + "'" +
                                                        ",'" + Vermerk + "'" +
                                                        "," + Convert.ToInt32(IsCustomCertificateMissing) +
                                                        "); ";

            return strSql;
        }
        ///<summary>clsSPL / Add</summary>
        ///<remarks></remarks>
        public bool Add(bool bEinbuchung)
        {
            bool bOK = false;
            string strSQL = string.Empty;
            strSQL = AddStrSQL(bEinbuchung);
            strSQL = strSQL + "Select @@IDENTITY as 'ID' ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            SPLID = decTmp;
            if (SPLID > 0)
            {
                bOK = true;
                clsArtikel art = new clsArtikel();
                art._GL_User = this._GL_User;
                art.ID = ArtikelID;
                art.GetArtikeldatenByTableID();

                clsLEingang Eingang = new clsLEingang();
                Eingang._GL_User = this._GL_User;
                Eingang.LEingangTableID = art.LEingangTableID;
                Eingang.FillEingang();

                //VITA
                if (bEinbuchung)
                {
                    //Add Logbucheintrag 
                    string myBeschreibung = "Sperrlager IN: LVS-NR [" + art.LVS_ID.ToString() + "] / Eingang [" + Eingang.LEingangID.ToString() + "]";
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), myBeschreibung);
                    clsArtikelVita.SperrlagerCheckIN(_GL_User, art.ID, Eingang.LEingangID, art.ArtIDAlt, art.LVSNrNachUB);
                }
                else
                {
                    //Add Logbucheintrag 
                    string myBeschreibung = "Sperrlager OUT: LVS-NR [" + art.LVS_ID.ToString() + "] / Eingang [" + Eingang.LEingangID.ToString() + "]";
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), myBeschreibung);
                    clsArtikelVita.SperrlagerCheckOUT(_GL_User, art.ID, Eingang.LEingangID, art.ArtIDAlt, art.LVSNrNachUB);
                }
            }
            return bOK;
        }
        ///<summary>clsSPL / UpdateDefWindungen</summary>
        ///<remarks></remarks>
        public bool Update()
        {
            string strSql = string.Empty;
            strSql = "Update Sperrlager " +
                                "SET DefWindungen = " + this.DefWindungen +
                                     ", Sperrgrund='" + this.Sperrgrund + "'" +
                                     ", Vermerk ='" + this.Vermerk + "'" +
                                " WHERE ID=" + this.SPLID + " ;";

            bool bOK = clsSQLcon.ExecuteSQL(strSql, this.BenutzerID);
            return bOK;
        }
        ///<summary>clsSPL / Fill</summary>
        ///<remarks></remarks>
        public bool Fill()
        {
            if (SPLID > 0)
            {
                try
                {
                    DataTable dt = new DataTable();
                    string strSql = string.Empty;
                    strSql = "SELECT * FROM Sperrlager WHERE ID=" + SPLID + "; ";
                    dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Sperrlager");
                    FillClass(ref dt, false);
                }
                catch (Exception ex)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsSPL / Fill</summary>
        ///<remarks></remarks>
        public bool FillLastINByArtikelID(bool bComplete = true)
        {
            if (ArtikelID > 0)
            {
                try
                {
                    DataTable dt = new DataTable();
                    string strSql = string.Empty;
                    strSql = "SELECT TOP(1) * FROM Sperrlager WHERE ArtikelID=" + ArtikelID + " AND BKZ='IN' ORDER BY Datum DESC;";
                    dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Sperrlager");
                    FillClass(ref dt, bComplete);
                }
                catch (Exception ex)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsSPL / Fill</summary>
        ///<remarks></remarks>
        public bool FillLastINByArtikelIDByCustomCertificate(bool bComplete = true)
        {
            if (ArtikelID > 0)
            {
                try
                {
                    DataTable dt = new DataTable();
                    string strSql = string.Empty;
                    strSql = "SELECT TOP(1) * FROM Sperrlager ";
                    strSql += "WHERE ArtikelID=" + ArtikelID;
                    strSql += " AND BKZ='IN' ";
                    strSql += " AND IsCustomCertificateMissing=1 ";
                    strSql += " ORDER BY Datum DESC;";
                    dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Sperrlager");
                    FillClass(ref dt, bComplete);
                }
                catch (Exception ex)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsSPL / Fill</summary>
        ///<remarks></remarks>
        public bool FillLastOUTByArtikelID()
        {
            if (ArtikelID > 0)
            {
                try
                {
                    DataTable dt = new DataTable();
                    string strSql = string.Empty;
                    strSql = "SELECT TOP(1) * FROM Sperrlager WHERE ArtikelID=" + ArtikelID + " AND BKZ='OUT' ORDER BY Datum DESC;";
                    dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Sperrlager");
                    FillClass(ref dt, false);
                }
                catch (Exception ex)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsSPL / FillClass</summary>
        ///<remarks></remarks>
        private void FillClass(ref DataTable dt, bool bComplete)
        {
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                this.SPLID = (decimal)dt.Rows[i]["ID"];
                this.ArtikelID = (decimal)dt.Rows[i]["ArtikelID"];
                this.Datum = (DateTime)dt.Rows[i]["Datum"];
                this.BKZ = dt.Rows[i]["BKZ"].ToString();
                this.SPLIDIn = (decimal)dt.Rows[i]["SPLIDIn"];
                this.DefWindungen = (Int32)dt.Rows[i]["DefWindungen"];
                this.Sperrgrund = dt.Rows[i]["Sperrgrund"].ToString();
                this.Vermerk = dt.Rows[i]["Vermerk"].ToString();
                if (bComplete = true)
                {
                    this.BenutzerID = (decimal)dt.Rows[i]["UserID"];
                }
                this.IsCustomCertificateMissing = (bool)dt.Rows[i]["IsCustomCertificateMissing"];
            }
        }
        ///<summary>clsSPL / CheckArtikelInSPL</summary>
        ///<remarks></remarks>
        public bool CheckArtikelInSPL()
        {
            if (ExistSPLID())
            {
                string strSql = string.Empty;
                strSql = "SELECT * FROM Sperrlager " +
                                    "WHERE BKZ = 'IN' AND ID NOT IN " +
                                    "(SELECT DISTINCT SPLIDIn FROM Sperrlager WHERE SPLIDIn>0) AND ArtikelID=" + ArtikelID + ";";
                //"SPLIDIn=" + SPLIDIn + " ;";
                bool reVal = clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);

                return reVal;
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsSPL / CheckArtikelInSPL</summary>
        ///<remarks></remarks>
        public bool ExistSPLID()
        {
            string strSql = string.Empty;
            strSql = "SELECT ID FROM Sperrlager WHERE ID=" + SPLID + ";";
            bool reVal = clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            return reVal;
        }
        ///<summary>clsSPL / DoSPLCheckOutInNEWEingnag</summary>
        ///<remarks>für SZG SLE UDN SIL</remarks>
        public string SQL_DoSPLCheckOutByCall(clsArtikel myArt)
        {
            string strSQL = string.Empty;
            if (myArt is clsArtikel)
            {
                if (myArt.ID > 0)
                {
                    //Ausbuchung SPL
                    clsSPL spl = new clsSPL();
                    spl._GL_User = this._GL_User;
                    spl.ArtikelID = myArt.ID;
                    spl.BKZ = "OUT";
                    spl.Datum = DateTime.Now;
                    strSQL = strSQL + spl.AddStrSQL(false);
                    //ArtikelVita - Sperrlagerausbuchung
                    string tmpAktion = enumLagerAktionen.SperrlagerOUT.ToString();
                    string tmpTableName = "Artikel";
                    string tmpBeschreibung = "Sperrlager OUT: ArtikelID [" + myArt.ID.ToString() + "] / LVSNr [" + myArt.LVS_ID.ToString() + "] per Abruf autom. erstellt [SPL CheckOut]";
                    tmpAktion = enumLagerAktionen.SperrlagerOUT.ToString();
                    tmpTableName = "Artikel";

                    if (myArt.ID > 0)
                    {
                        strSQL = strSQL + " INSERT INTO ArtikelVita (TableID, TableName, Aktion, Datum, UserID, Beschreibung) " +
                                            "VALUES (" + myArt.ID +
                                                    ",'" + tmpTableName +
                                                    "','" + tmpAktion +
                                                    "','" + DateTime.Now +
                                                    "', " + BenutzerID +
                                                    ",'" + tmpBeschreibung + "'); ";

                    }
                    else
                    {
                        strSQL = strSQL + " INSERT INTO ArtikelVita (TableID, TableName, Aktion, Datum, UserID, Beschreibung) " +
                                "VALUES (@ArtikelID ,'"
                                        + tmpTableName + "','"
                                        + tmpAktion + "','"
                                        + DateTime.Now + "','"
                                        + BenutzerID + "','"
                                        + tmpBeschreibung + "'); ";
                    }

                }
            }
            return strSQL;
        }
        ///<summary>clsSPL / DoSPLCheckOutInNEWEingnag</summary>
        ///<remarks>für SZG SLE UDN SIL</remarks>
        public void DoSPLCheckOutInNEWEingnag()
        {
            // muss noch getestet werden
            /***
            if (dtCheckOut.Rows.Count > 0)
            {                
                clsArtikel artA = new clsArtikel();
                artA._GL_User = this._GL_User;
                artA.ID = Convert.ToDecimal(dtCheckOut.Rows[0]["ArtikelID"].ToString());
                artA.GetArtikeldatenByTableID();

                //Neue LEingangID wird ermittelt
                clsPrimeKeys pk = new clsPrimeKeys();
                pk.sys = this.sys;
                pk._GL_User = this._GL_User;
                pk.Mandanten_ID = artA.MandantenID;
                pk.GetNEWLEingnagID();

                //Neuer Lagereingang wird angelegt
                clsLEingang Eingang = new clsLEingang();
                Eingang._GL_User = this._GL_User;
                Eingang.LEingangTableID = artA.LEingangTableID;
                Eingang.FillEingang();
                //anpassen
                Eingang.LEingangTableID = 0;
                Eingang.LEingangID=pk.LEingangID;
                Eingang.LEingangDate=DateTime.Now;
                Eingang.Checked=false;
                                
                string strSQL = string.Empty;
                strSQL = "DECLARE @LEingangTableID as decimal(28,0); " +
                         "DECLARE @ArtikelID as decimal(28,0); ";
                strSQL = strSQL + Eingang.AddLagerEingangSQL() +
                        " Select @LEingangTableID = @@IDENTITY; ";
                               
                //ArtikelVita;
                decimal tmpLEingangID = Eingang.LEingangID;
                string tmpBeschreibung = "Lagereingang [" + Eingang.LEingangID.ToString() + "] autom. erstellt [SPL CheckOut]";
                string tmpAktion = Globals.enumLagerAktionen.EingangErstellt.ToString();
                string tmpTableName = "LEingang";
                strSQL = strSQL + "INSERT INTO ArtikelVita (TableID, TableName, Aktion, Datum, UserID, Beschreibung) " +
                                    "VALUES (@LEingangTableID ,'"
                                            + tmpTableName + "','"
                                            + tmpAktion + "','"
                                            + DateTime.Now + "','"
                                            + BenutzerID + "','"
                                            + tmpBeschreibung + "')";

                //Add Logbucheintrag EINGANG
                string Beschreibung = "Lager Eingang erstellt: Nr [" + Eingang.LEingangID.ToString() + "] / Mandant [" + Eingang.MandantenID.ToString() + "] / Arbeitsbereich [" + Eingang.AbBereichID.ToString() + "]";
                strSQL =strSQL + "INSERT INTO Logbuch (BenutzerID, BenutzerName, Datum, Aktion, Beschreibung) " +
                                "VALUES (" + BenutzerID + 
                                    ",'"+ _GL_User.Name + "'"+
                                    ", '"+ DateTime.Now + "'"+
                                    ",'" +  Globals.enumLogbuchAktion.Eintrag.ToString() + "'"+
                                    ",'" + Beschreibung.Replace("'", "") + "'"+
                                    ")";                              
                
                for (Int32 i = 0; i <= dtCheckOut.Rows.Count - 1; i++)
                {
                    if ((bool)dtCheckOut.Rows[i]["ausbuchen"])
                    {
                        clsArtikel artAlt = new clsArtikel();
                        artAlt._GL_User = this._GL_User;
                        artAlt.ID = Convert.ToDecimal(dtCheckOut.Rows[i]["ArtikelID"].ToString());
                        artAlt.GetArtikeldatenByTableID();

                        //Artikel neu speichern
                        clsArtikel artNeu = artAlt;

                        artNeu.ArtIDAlt = artAlt.ID;
                        artNeu.ID = 0;
                        artNeu.EingangChecked = false;
                        artNeu.AusgangChecked = false;
                        artNeu.LEingangTableID = 0;
                        artNeu.BKZ = 1;
                        pk.GetNEWLvsNr();
                        artNeu.LVS_ID = pk.LvsNr;

                        //Eintrag neuen Artikel 
                        strSQL = strSQL + artNeu.AddArtikelLager_SQL() +
                                "Select @ArtikelID = @@IDENTITY;";
                        //Update neuen Artikel mit der LEingangTableID
                        strSQL = strSQL + " Update Artikel SET LEingangTableID=@LEingangTableID WHERE ID=@ArtikelID; ";

                        //Update alten Artikel BKZ=0 -> ausbuchen (alte ID wird in IDvorUB gespeichert
                        strSQL = strSQL + " Update Artikel SET BKZ=0 WHERE ID=" + artNeu.ArtIDAlt + ";";

                        //ArtikelVita - Artikeleintrag
                        tmpLEingangID = Eingang.LEingangID;
                        tmpBeschreibung = "Artikel hinzugefügt: LVSNr ["+artNeu.LVS_ID.ToString()+"] / Eingang [" + Eingang.LEingangID.ToString() + "]";
                        tmpAktion = Globals.enumLagerAktionen.ArtikelAdd_Eingang.ToString();
                        tmpTableName = "Artikel";
                        strSQL = strSQL + "INSERT INTO ArtikelVita (TableID, TableName, Aktion, Datum, UserID, Beschreibung) " +
                                            "VALUES (@ArtikelID ,'"
                                                    + tmpTableName + "','"
                                                    + tmpAktion + "','"
                                                    + DateTime.Now + "','"
                                                    + BenutzerID + "','"
                                                    + tmpBeschreibung + "')";

                        //Add Logbucheintrag Artikel
                        Beschreibung = "Artikel hinzugefügt: LVS-NR [" + artNeu.LVS_ID.ToString() + "] / Eingang [" + Eingang.LEingangID.ToString() + "]";
                        strSQL =strSQL + "INSERT INTO Logbuch (BenutzerID, BenutzerName, Datum, Aktion, Beschreibung) " +
                                            "VALUES (" + BenutzerID + 
                                                     ",'"+ _GL_User.Name + "'"+
                                                     ", '"+ DateTime.Now + "'"+
                                                     ",'" +  Globals.enumLogbuchAktion.Eintrag.ToString() + "'"+
                                                     ",'" + Beschreibung.Replace("'", "") + "'"+
                                                     ")";  
                        
                        //Ausbuchung SPL
                        clsSPL spl = new clsSPL();
                        spl._GL_User = this._GL_User;
                        spl.ArtikelID = Convert.ToDecimal(dtCheckOut.Rows[i]["ArtikelID"].ToString());
                        spl.BKZ = "OUT";
                        spl.Datum = DateTime.Now;
                        strSQL = strSQL + spl.AddStrSQL(false);

                        //ArtikelVita - Sperrlagerausbuchung
                        tmpLEingangID = Eingang.LEingangID;
                        tmpBeschreibung = "Sperrlager OUT: ArtikelID ["+artAlt.ID.ToString()+"] / LVSNr ["+artAlt.LVS_ID.ToString()+"] - ArtikelID neu ["+artNeu.ID.ToString()+"] / LVSNr neu ["+artNeu.LVS_ID.ToString()+"]";
                        tmpAktion = Globals.enumLagerAktionen.SperrlagerOUT.ToString();
                        tmpTableName = "Artikel";
                        strSQL = strSQL + "INSERT INTO ArtikelVita (TableID, TableName, Aktion, Datum, UserID, Beschreibung) " +
                                            "VALUES (@ArtikelID ,'"
                                                    + tmpTableName + "','"
                                                    + tmpAktion + "','"
                                                    + DateTime.Now + "','"
                                                    + BenutzerID + "','"
                                                    + tmpBeschreibung + "')";
                    }                                    
                }

                //START Transaction
                bool bOK= clsSQLcon.ExecuteSQLWithTRANSACTION(strSQL, "SPLCheckOut", BenutzerID);
                if(bOK)
                {
                    clsMessages.Sperrlager_CheckOutOK();
                }
                else
                {
                    clsMessages.Sperrlager_CheckOutFailed();
                }
            
            }  
             * ****/
        }
        ///<summary>clsSPL / DoSPLCheckOutInOldEingang</summary>
        ///<remarks>Artikel wird aus dem Sperrlager in den alten Eingang / Bestand zurückgebucht.</remarks>
        public bool DoSPLCheckOutInOldEingang(bool bUmbuchung = false)
        {
            bool bOK = false;
            if (dtCheckOut.Rows.Count > 0 || (dtCheckOut.Rows.Count == 0 && lstCheckOut.Count > 0))
            {
                string strSQL = string.Empty;
                Int32 rows = 0;
                bool bUseDt = false;
                if (dtCheckOut.Rows.Count > 0)
                {
                    rows = dtCheckOut.Rows.Count;
                    bUseDt = true;
                }
                else if (lstCheckOut.Count > 0)
                {
                    rows = lstCheckOut.Count;
                    bUseDt = false;
                }
                List<string> listError = new List<string>();
                for (Int32 i = 0; i <= rows - 1; i++)
                {
                    if ((bUseDt && (bool)dtCheckOut.Rows[i]["ausbuchen"]) || !bUseDt)
                    {
                        int iArticleId = 0;
                        if (bUseDt)
                        {
                            int.TryParse(dtCheckOut.Rows[i]["ArtikelID"].ToString(), out iArticleId);
                            //iArticleId = Convert.ToInt32(dtCheckOut.Rows[i]["ArtikelID"].ToString());
                        }
                        else
                        {
                            iArticleId = lstCheckOut.ElementAt(i);
                        }
                        if (iArticleId > 0)
                        {
                            bool bIsArticleCertificate = false;
                            if (dtCheckOut.Columns.Contains("Zert"))
                            {
                                bool.TryParse(dtCheckOut.Rows[i]["Zert"].ToString(), out bIsArticleCertificate);
                            }
                            //--Custom Processes
                            CustomProcessesViewData cpVD = new CustomProcessesViewData(iArticleId, _GL_User);
                            if ((cpVD.ExistCustomProcess) && (bIsArticleCertificate))
                            {
                                string strInfo = "- ID|LVS : [" + cpVD.artikelVD.Artikel.Id + "] | " + cpVD.artikelVD.Artikel.LVS_ID + " ->  Artikleprüfung / Custom Process / SPL - Artikel Zertifikat fehlt " + Environment.NewLine;
                                listError.Add(strInfo);
                                //bool IsAction = cpVD.CheckAndExecuteCustomProcess(cpVD.artikelVD.Artikel.Id, 0, 0, CustomProcess_Novelis_AccessByArticleCert.const_ProcessLocation_ctrEinlagerung_CheckEingangElementsToFinish);
                                //if ((IsAction) && (!cpVD.Process_Novelis_AccessByArticleCert.IsArticleZertifacteProcessComplete))
                                //{

                                //    string strInfo = "- ID|LVS : [" + cpVD.artikelVD.Artikel.Id + "] | " + cpVD.artikelVD.Artikel.LVS_ID + " ->  Artikleprüfung / Custom Process / SPL - Artikel Zertifikat fehlt " + Environment.NewLine;
                                //    listError.Add(strInfo);
                                //}
                            }
                            else
                            {
                                SperrlagerViewData splVD = new SperrlagerViewData(cpVD.artikelVD.Artikel, (int)BenutzerID);
                                //-- check Artikel im Sperrlager
                                if (splVD.Spl.Id > 0)
                                {
                                    //-- SPL ausbuchen
                                    splVD.Spl.SPLIDIn = splVD.Spl.Id;
                                    splVD.Spl.Id = 0;
                                    splVD.Add(false);
                                    bOK = splVD.Spl.Id > 0;
                                }
                            }
                        }

                        //clsArtikel Artikel = new clsArtikel();
                        //Artikel._GL_User = this._GL_User;
                        //if (bUseDt)
                        //{
                        //    Artikel.ID = Convert.ToDecimal(dtCheckOut.Rows[i]["ArtikelID"].ToString());
                        //}
                        //else
                        //{
                        //    Artikel.ID = lstCheckOut.ElementAt(i);
                        //}
                        //Artikel.GetArtikeldatenByTableID();

                        //--Custom Processes
                        //CustomProcessesViewData cpVD = new CustomProcessesViewData((int)Artikel.ID, _GL_User);
                        //if (cpVD.ExistCustomProcess)
                        //{
                        //    bool IsAction = cpVD.CheckAndExecuteCustomProcess((int)Artikel.ID, 0, 0, CustomProcess_Novelis_AccessByArticleCert.const_ProcessLocation_ctrEinlagerung_CheckEingangElementsToFinish);
                        //    if ((IsAction) && (!cpVD.Process_Novelis_AccessByArticleCert.IsArticleZertifacteProcessComplete))
                        //    {

                        //        string strInfo = "- ID|LVS : ["+Artikel.ID+"] | "+Artikel.LVS_ID+" ->  Artikleprüfung / Custom Process / SPL - Artikel Zertifikat fehlt " + Environment.NewLine;
                        //        listError.Add(strInfo);
                        //    }
                        //}
                        //else
                        //{
                        //    strSQL = strSQL + " " + SQLArtikelBookOutSPL(Artikel, true);
                        //}
                    }
                }
                //START Transaction
                //bOK = clsSQLcon.ExecuteSQLWithTRANSACTION(strSQL, "SPLCheckOut", BenutzerID);
                if (!bUmbuchung)
                {
                    if (bOK)
                    {
                        clsMessages.Sperrlager_CheckOutOK(listError);
                    }
                    else
                    {
                        clsMessages.Sperrlager_CheckOutFailed(listError);
                    }
                }
            }
            return bOK;
        }
        ///<summary>clsSPL / SQLArtikelBookOutSPL</summary>
        ///<remarks></remarks>
        public string SQLArtikelBookOutSPL(clsArtikel myArikel, bool bInclVita)
        {
            string strSQL = string.Empty;
            //Einbuchung Sperrlager
            clsSPL splIN = new clsSPL();
            splIN._GL_User = this._GL_User;
            splIN.ArtikelID = myArikel.ID;
            splIN.FillLastINByArtikelID();

            //nicht im SPL
            if (splIN.SPLID > 0)
            {
                //Ausbuchung SPL
                clsSPL spl = new clsSPL();
                spl._GL_User = this._GL_User;
                spl.ArtikelID = myArikel.ID;
                spl.BKZ = "OUT";
                spl.SPLIDIn = splIN.SPLID;
                spl.Datum = DateTime.Now;
                strSQL = strSQL + spl.AddStrSQL(false);
                if (bInclVita)
                {
                    //ArtikelVita - Sperrlagerausbuchung
                    //tmpLEingangID = Artikel.LEingangID;
                    string tmpBeschreibung = "Sperrlager OUT: ArtikelID [" + myArikel.ID.ToString() + "] / LVSNr [" + myArikel.LVS_ID.ToString() + "]";
                    string tmpAktion = enumLagerAktionen.SperrlagerOUT.ToString();
                    string tmpTableName = enumDatabaseSped4_TableNames.Artikel.ToString();
                    strSQL = strSQL + "INSERT INTO ArtikelVita (TableID, TableName, Aktion, Datum, UserID, Beschreibung) " +
                                        "VALUES (" + myArikel.ID +
                                                ",'" + tmpTableName + "'" +
                                                ",'" + tmpAktion + "'" +
                                                ",'" + DateTime.Now + "'" +
                                                ",'" + BenutzerID + "'" +
                                                ",'" + tmpBeschreibung + "'" +
                                                "); ";
                }

            }
            return strSQL;
        }

        ///<summary>clsSPL / SQLArtikelBookOutSPL</summary>
        ///<remarks></remarks>
        public string SQLArtikelBookOutSPLByCertificate(Articles myArikel, bool bInclVita)
        {
            string strSQL = string.Empty;
            //Einbuchung Sperrlager
            clsSPL splIN = new clsSPL();
            splIN._GL_User = this._GL_User;
            splIN.ArtikelID = myArikel.Id;
            splIN.FillLastINByArtikelIDByCustomCertificate();

            if (myArikel.Id == 104715)
            {
                string st = string.Empty;
            }

            //nicht im SPL
            //if (splIN.SPLID > 0)
            if (splIN.IsInSPL)
            {
                //-- zu dieser SPL IN Buchung kann es nur eine OUT Buchung geben
                //-- alle weiteren Buchungssätze werden gelöscht
                ViewData.SperrlagerViewData splOutCheckVD = new ViewData.SperrlagerViewData((int)splIN.SPLID, 0);
                splOutCheckVD.CheckForDoubleBookingRecord();

                //Ausbuchung SPL
                clsSPL spl = new clsSPL();
                spl._GL_User = this._GL_User;
                spl.ArtikelID = myArikel.Id;
                spl.BKZ = "OUT";
                spl.SPLIDIn = splIN.SPLID;
                spl.Datum = DateTime.Now;
                spl.IsCustomCertificateMissing = true;
                strSQL = strSQL + spl.AddStrSQL(false);

                if (bInclVita)
                {
                    //ArtikelVita - Sperrlagerausbuchung
                    //tmpLEingangID = Artikel.LEingangID;
                    string tmpBeschreibung = "Artikelfreigabe durch Zertifikat: ArtikelID [" + myArikel.Id.ToString() + "] / LVSNr [" + myArikel.LVS_ID.ToString() + "]";
                    string tmpAktion = enumLagerAktionen.SperrlagerOUT.ToString();
                    string tmpTableName = enumDatabaseSped4_TableNames.Artikel.ToString();
                    strSQL = strSQL + " INSERT INTO ArtikelVita (TableID, TableName, Aktion, Datum, UserID, Beschreibung) " +
                                        "VALUES (" + myArikel.Id +
                                                ",'" + tmpTableName + "'" +
                                                ",'" + tmpAktion + "'" +
                                                ",'" + DateTime.Now + "'" +
                                                ",'" + BenutzerID + "'" +
                                                ",'" + tmpBeschreibung + "'" +
                                                "); ";
                }

            }
            return strSQL;
        }

        ///<summary>clsSPL / DoSPLCheckOutInOldEingang</summary>
        ///<remarks>Artikel wird aus dem Sperrlager in den alten Eingang / Bestand zurückgebucht.</remarks>
        public void DoSPLUmbuchung(decimal newArtikelID, decimal newEingang)
        {
            DoSPLCheckOutInOldEingang(true);
            string strSQL = string.Empty;
            this.Datum = DateTime.Now;
            strSQL = this.AddStrSQL(false);
            bool bOK = clsSQLcon.ExecuteSQLWithTRANSACTION(strSQL, "SPLCheckOut", BenutzerID);

            this.ArtikelID = newArtikelID;
            this.Add(true);
        }
        /*************************************************************************************
         *                      pubic static
         * ***********************************************************************************/
        ///<summary>clsSPL / CheckArtikelInSPL</summary>
        ///<remarks></remarks>
        public static List<decimal> GetArtikelEingangInSPL(Globals._GL_USER myGLUser, decimal myLEingangTableID)
        {
            List<decimal> listReturn = new List<decimal>();
            string strSQL = string.Empty;
            strSQL = "SELECT DISTINCT ArtikelID FROM Sperrlager " +
                                    "WHERE BKZ = 'IN' AND ID NOT IN " +
                                    "(SELECT DISTINCT SPLIDIn FROM Sperrlager WHERE SPLIDIn>0) AND ArtikelID IN (SELECT ID FROM Artikel WHERE LEingangTableID=" + (Int32)myLEingangTableID + ");";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "ArtikelSPL");
            foreach (DataRow row in dt.Rows)
            {
                decimal decTmp = 0;
                Decimal.TryParse(row["ArtikelID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    listReturn.Add(decTmp);
                }
            }
            return listReturn;
        }
    }
}
