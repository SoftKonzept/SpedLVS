using Common.Models;
using Common.Views;
using LVS.Models;
using LVS.ViewData;
using LVS.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LVS.Communicator
{
    public class AsnCreateStoreIn
    {
        internal clsSystem Sys { get; set; } = new clsSystem();
        internal Globals._GL_USER GLUser { get; set; } = new Globals._GL_USER();
        internal Globals._GL_SYSTEM GLSystem { get; set; } = new Globals._GL_SYSTEM();
        internal clsASN Asn { get; set; } = new clsASN();
        internal AsnViewData asnVD { get; set; } = new AsnViewData();
        public string ErrorText { get; set; } = string.Empty;
        public string LogText { get; set; } = string.Empty;

        //public DataTable dtASN { get; set; }
        public DataTable dtEingang { get; set; }
        public DataTable dtArtikel { get; set; }
        public Eingaenge Eingang { get; set; }
        public DataTable dtInserted { get; set; }
        public EingangViewData eingangViewData { get; set; }
        public List<Articles> ListEingangArticle { get; set; }


        //public List<ctrASNRead_AsnEdifactView> List_ctrAsnRead_AsnEdifactView { get; set; } = new List<ctrASNRead_AsnEdifactView>();
        //public List<ctrASNRead_AsnArticleEdifactView> List_ctrAsnRead_AsnArticelEdifactView { get; set; } = new List<ctrASNRead_AsnArticleEdifactView>();

        public AsnCreateStoreIn()
        {
            InitCls();
        }
        public AsnCreateStoreIn(Asn myAsn) : this()
        {
            asnVD = new AsnViewData(myAsn);
            GLSystem = Asn.GLSystem;
            GLUser = Asn._GL_User;
            Sys = Asn.Sys;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool InitEdifactCreationStoreIn()
        {
            bool bReturn = false;
            List<Asn> asnList = new List<Asn>()
            {
                asnVD.asnHead
            };
            asnVD.FillAsnEdifactViewAndArticleEdifactView(asnList);

            foreach (var item in asnVD.List_ctrAsnRead_AsnEdifactView)
            {
                //lvLogEDIFACT.Items.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
                AsnReadViewData readVD = new AsnReadViewData();
                //bReturn = readVD.CreateStoreInByAsnEdifactView(item, (int)this.GLUser.User_ID);
                bReturn = readVD.CreateStoreInByAsnEdifactView(item.AsnMessage, item.eingang, item.ListArticleInEingang, (int)this.GLUser.User_ID);
                LogText = string.Empty;
                LogText = readVD.LogText;
                Eingang = new Eingaenge();
                Eingang = readVD.EingangCreated.Copy();

                //if (!bReturn)
                //{
                //    //lvLogEDIFACT.Items.Add(asnReadViewData.Errortext);
                //}
                //else
                //{
                //    //lvLogEDIFACT.Items.Add(asnReadViewData.LogText);
                //}
            }
            return bReturn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myAsn"></param>
        public AsnCreateStoreIn(clsASN myAsn) : this()
        {
            Asn = myAsn;
            GLSystem = Asn.GLSystem;
            GLUser = Asn._GL_User;
            Sys = Asn.Sys;

            Eingang = new Eingaenge();
            ListEingangArticle = new List<Articles>();

            List<int> tmpIds = new List<int>();
            tmpIds.Add((int)myAsn.ID);
            DataTable dtTmp = myAsn.GetASNByASNId(tmpIds);
            DataTable dtAsn = myAsn.EditTableForUse(dtTmp);

            if (dtAsn.Rows.Count > 0)
            {
                dtEingang = new DataTable("Eingang");
                AsnCreateLfsView lfsView = new AsnCreateLfsView(GLSystem, GLUser, Sys);
                dtEingang = lfsView.GetLfsKopfdaten(ref dtAsn);
                List<AsnLfsView> ListAsnLfsView = lfsView.ConvertDataTableToList(dtEingang);

                AsnConvertToStoreInValue asnConvertValue = new AsnConvertToStoreInValue(GLSystem, GLUser, Sys);
                DataTable dtArtikel = asnConvertValue.GetArtikelDaten1(ref dtAsn);

                string strLfs = string.Empty;
                List<string> listLfs = new List<string>();

                foreach (DataRow row in dtArtikel.Rows)
                {
                    strLfs = row["LfsNr"].ToString();
                    if (!strLfs.Equals(string.Empty))
                    {
                        if (!listLfs.Contains(strLfs))
                        {
                            listLfs.Add(strLfs);
                        }
                    }
                }
                foreach (var item in ListAsnLfsView)
                {
                    for (Int32 n = 0; n <= listLfs.Count - 1; n++)
                    {
                        string strLfsNr = string.Empty;
                        strLfsNr = listLfs[n].ToString();
                        //strLfsNr = item.LfsNr;
                        decimal decTmp = 0;
                        decimal decTmpASN = Asn.ID;
                        //Decimal.TryParse(strASN, out decTmpASN);
                        if (decTmpASN > 0)
                        {
                            try
                            {
                                dtArtikel.DefaultView.RowFilter = string.Empty;
                                dtArtikel.DefaultView.RowFilter = "ChildID='" + Asn.ID.ToString() + strLfsNr + "'";
                                DataTable dtTmpArt = dtArtikel.DefaultView.ToTable();

                                clsLEingang ein = new clsLEingang();
                                ein.sys = Sys;
                                ein.LEingangDate = DateTime.Now;
                                ein.Auftraggeber = item.Auftraggeber;
                                ein.Empfaenger = item.Empfaenger;
                                ein.AbBereichID = item.WorkspaceId;
                                ein.MandantenID = item.Workspace.MandantId; // Sys.AbBereich.MandantenID;
                                ein.LEingangLfsNr = strLfsNr;

                                ein.KFZ = string.Empty;
                                ein.WaggonNr = string.Empty;
                                ein.Ship = string.Empty;

                                if (dtTmpArt.Rows.Count > 0)
                                {
                                    ediHelper_712_TM e712TM = new ediHelper_712_TM(dtTmpArt.Rows[0]["TMS"].ToString(), dtTmpArt.Rows[0]["VehicleNo"].ToString());
                                    //ein.WaggonNr = dtTmpArt.Rows[0]["VehicleNo"].ToString();
                                    //ein.IsWaggon = (dtTmpArt.Rows[0]["TMS"].ToString() == "08");

                                    switch (e712TM.enumTMS)
                                    {
                                        case enumVDA4913_712F14_TMS.KFZ:
                                            ein.KFZ = e712TM.VehicleNo;
                                            ein.IsWaggon = false;
                                            ein.IsShip = false;
                                            break;
                                        case enumVDA4913_712F14_TMS.Waggonnummer:
                                            ein.WaggonNr = e712TM.VehicleNo;
                                            ein.IsWaggon = true;
                                            ein.IsShip = false;
                                            break;
                                        case enumVDA4913_712F14_TMS.Schiffsname:
                                            ein.Ship = e712TM.VehicleNo;
                                            ein.IsShip = true;
                                            ein.IsWaggon = false;
                                            //this.IsShip = ein.IsShip;
                                            break;
                                    }

                                }
                                else
                                {
                                    ein.WaggonNr = string.Empty;
                                    ein.IsWaggon = false;
                                }
                                ein.ExTransportRef = item.ExTransportRef;
                                ein.ASNRef = item.ASNRef;
                                ein.Lieferant = item.Lieferantennummer;
                                ein.Checked = false;
                                ein.DirektDelivery = false;
                                ein.Retoure = false;
                                ein.Vorfracht = false;
                                ein.LagerTransport = false;

                                ein.ASN = decTmpASN;

                                clsPrimeKeys pk = new clsPrimeKeys();
                                pk.sys = Sys;
                                pk.AbBereichID = Sys.AbBereich.ID;
                                pk.Mandanten_ID = Sys.AbBereich.MandantenID;
                                pk._GL_User = GLUser;
                                pk.GetNEWLEingnagID();
                                ein.LEingangID = pk.LEingangID;
                                string strSql = string.Empty;

                                strSql = "DECLARE @LEingangTableID as decimal(28,0); " +
                                    "DECLARE @LvsID as decimal(28,0); " +
                                    "DECLARE @ArtID as decimal(28,0); ";

                                strSql = strSql +
                                        ein.AddLagerEingangSQL() +
                                        " Select @LEingangTableID= @@IDENTITY; ";

                                //Vita
                                string tmpAktion = enumLagerAktionen.EingangErstellt.ToString();
                                strSql = strSql + " INSERT INTO ArtikelVita (TableID, TableName, Aktion, Datum, UserID, Beschreibung" +
                                                    ") " +
                                                    "VALUES (@LEingangTableID" +
                                                            ",'LEingang'" +
                                                            ",'" + tmpAktion + "'" +
                                                            ",'" + DateTime.Now + "'" +
                                                            ", " + (int)GLUser.User_ID +
                                                            ",'Lagereingang ['+CAST(@LEingangTableID as nvarchar)+'] autom. erstellt'" +
                                                            "); ";

                                for (Int32 x = 0; x <= dtTmpArt.Rows.Count - 1; x++)
                                {
                                    //Achtung die LVSNR wird innerhalber der SQL Anweisung ermittelt 
                                    //und muss nicht über die Klasse Primekeys ermittelt werden
                                    clsArtikel art = new clsArtikel();
                                    art.sys = Sys;
                                    art.LVS_ID = 0;
                                    art.MandantenID = Sys.AbBereich.MandantenID;
                                    art.AbBereichID = Sys.AbBereich.ID;
                                    art.Eingang = ein.Copy();
                                    decTmp = 0;
                                    Decimal.TryParse(dtTmpArt.Rows[x]["GArtID"].ToString(), out decTmp);

                                    art.GArtID = decTmp;
                                    decTmp = 0;
                                    Decimal.TryParse(dtTmpArt.Rows[x]["Dicke"].ToString(), out decTmp);
                                    art.Dicke = decTmp;
                                    decTmp = 0;
                                    Decimal.TryParse(dtTmpArt.Rows[x]["Breite"].ToString(), out decTmp);
                                    art.Breite = decTmp;
                                    decTmp = 0;
                                    Decimal.TryParse(dtTmpArt.Rows[x]["Laenge"].ToString(), out decTmp);
                                    art.Laenge = decTmp;
                                    decTmp = 0;
                                    Decimal.TryParse(dtTmpArt.Rows[x]["Hoehe"].ToString(), out decTmp);
                                    art.Hoehe = decTmp;
                                    Int32 iTmp = 0;
                                    Int32.TryParse(dtTmpArt.Rows[x]["Anzahl"].ToString(), out iTmp);
                                    art.Anzahl = iTmp;
                                    art.Einheit = dtTmpArt.Rows[x]["Einheit"].ToString();

                                    if (art.GArtID > 0)
                                    {
                                        art.Einheit = art.GArt.Einheit;
                                        art.IsStackable = art.GArt.IsStackable;
                                    }
                                    art.gemGewicht = 0;
                                    decTmp = 0;
                                    Decimal.TryParse(dtTmpArt.Rows[x]["Netto"].ToString(), out decTmp);
                                    art.Netto = decTmp;
                                    decTmp = 0;
                                    Decimal.TryParse(dtTmpArt.Rows[x]["Brutto"].ToString(), out decTmp);
                                    art.Brutto = decTmp;
                                    art.Werksnummer = dtTmpArt.Rows[x]["Werksnummer"].ToString();
                                    art.Produktionsnummer = dtTmpArt.Rows[x]["Produktionsnummer"].ToString();
                                    art.exBezeichnung = dtTmpArt.Rows[x]["exBezeichnung"].ToString();
                                    art.Charge = dtTmpArt.Rows[x]["Charge"].ToString();
                                    art.Bestellnummer = dtTmpArt.Rows[x]["Bestellnummer"].ToString();
                                    art.exMaterialnummer = dtTmpArt.Rows[x]["exMaterialnummer"].ToString();
                                    art.TARef = dtTmpArt.Rows[x]["TARef"].ToString();
                                    iTmp = 0;
                                    Int32.TryParse(dtTmpArt.Rows[x]["Position"].ToString(), out iTmp);
                                    string strGlowDate = dtTmpArt.Rows[x]["GlowDate"].ToString();
                                    DateTime tmpGlowDate = new DateTime(1900, 1, 1);
                                    DateTime.TryParse(strGlowDate, out tmpGlowDate);
                                    art.GlowDate = tmpGlowDate;
                                    art.Position = iTmp.ToString();
                                    art.GutZusatz = string.Empty;
                                    art.ArtIDRef = Sys.Client.CreateArtikelIDRef(art);
                                    art.AuftragPosTableID = 0;
                                    art.ArtIDAlt = 0;
                                    art.Info = string.Empty;
                                    art.LagerOrt = 0;
                                    art.LagerOrtTable = string.Empty;
                                    art.exLagerOrt = string.Empty;
                                    art.ADRLagerNr = 0;
                                    art.FreigabeAbruf = false;
                                    art.LZZ = DateTime.ParseExact("01.01.2001", "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                    art.Werk = string.Empty;
                                    art.Halle = string.Empty;
                                    art.Ebene = string.Empty;
                                    art.Reihe = string.Empty;
                                    art.Platz = string.Empty;
                                    art.exAuftrag = dtTmpArt.Rows[x]["exAuftrag"].ToString();
                                    art.exAuftragPos = dtTmpArt.Rows[x]["exAuftragPos"].ToString();
                                    art.ASNVerbraucher = dtTmpArt.Rows[x]["ASNVerbraucher"].ToString();

                                    strSql += art.AddArtikelLager_SQL(true, true);
                                    strSql = strSql + "SET @ArtID=(Select @@IDENTITY); ";

                                    strSql = strSql + "SET  @LvsID=" + clsPrimeKeys.SQLStringNewLVSNr(Sys);

                                    strSql = strSql + "Update Artikel SET LVS_ID=@LvsID " +
                                                                         "WHERE ID = @ArtID; ";

                                    if (Sys.Client.Modul.PrimeyKey_LVSNRUseOneIDRange)
                                    {
                                        strSql = strSql + "UPDATE PrimeKeys SET LvsNr = @LvsID;";
                                    }
                                    else
                                    {
                                        strSql = strSql + "UPDATE PrimeKeys SET LvsNr = @LvsID WHERE Mandanten_ID=" + ein.MandantenID + ";";
                                    }
                                    //strSql = strSql + " Select  @ArtikelTableID  = @@IDENTITY; ";
                                    //Vita
                                    tmpAktion = enumLagerAktionen.ArtikelAdd_Eingang.ToString();
                                    strSql = strSql + " INSERT INTO ArtikelVita (TableID, TableName, Aktion, Datum, UserID, Beschreibung" +
                                                                ") " +
                                                                "VALUES (@ArtID" +
                                                                        ",'Artikel'" +
                                                                        ",'" + tmpAktion + "'" +
                                                                        ",'" + DateTime.Now + "'" +
                                                                        ", " + (int)GLUser.User_ID +
                                                                        ", 'autom. Artikel hinzugefügt: LVS-NR [ ' + CAST((Select LVS_ID from Artikel where ID=@ArtID) as nvarchar) + ' ] / Eingang ID ['+CAST(@LEingangTableID as nvarchar)+']'" +
                                                                        "); ";
                                } // schleife Artikel
                                  //Insert

                                strSql = strSql + " Select @LEingangTableID";
                                string strEID = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSql, "ASNInsert", (int)GLUser.User_ID);
                                decTmp = 0;
                                decimal.TryParse(strEID, out decTmp);
                                if (decTmp > 0)
                                {
                                    eingangViewData = new EingangViewData((int)decTmp, (int)GLUser.User_ID, true);
                                    Eingang = eingangViewData.Eingang.Copy();
                                    ListEingangArticle = eingangViewData.ListArticleInEingang.ToList();

                                    //ein.LEingangTableID = decTmp;
                                    //ein.FillEingang();

                                    string strSql1 = string.Empty;
                                    strSql1 = "Update ASN SET IsRead=1 WHERE ID=" + (Int32)Asn.ID + ";";
                                    bool bUpdatet = clsSQLCOM.ExecuteSQLWithTRANSACTION(strSql1, "UpdateASN", (int)GLUser.User_ID);
                                    //List ASN read füllen
                                    //ListASNRead.Add(strASN);

                                    //if (myUseAutoRowAssign)
                                    //{
                                    //    if (ein.dtArtInLEingang.Rows.Count > 0)
                                    //    {
                                    //        foreach (DataRow r in ein.dtArtInLEingang.Rows)
                                    //        {
                                    //            decTmp = 0;
                                    //            decimal.TryParse(r["ID"].ToString(), out decTmp);
                                    //            if (decTmp > 0)
                                    //            {
                                    //                clsArtikel tmpArt = new clsArtikel();
                                    //                tmpArt.InitClass(GLUser, GLSystem);
                                    //                tmpArt.ID = decTmp;
                                    //                tmpArt.GetArtikeldatenByTableID();
                                    //                if (tmpArt.GArt is clsGut)
                                    //                {
                                    //                    clsReihe Reihe = new clsReihe();
                                    //                    Reihe._GL_User = GLUser;
                                    //                    tmpArt.Reihe = Reihe.GetVorschlag(tmpArt.Dicke.ToString()
                                    //                                                    , tmpArt.Breite.ToString()
                                    //                                                    , tmpArt.Laenge.ToString()
                                    //                                                    , tmpArt.Hoehe.ToString()
                                    //                                                    , tmpArt.Brutto.ToString()
                                    //                                                    , tmpArt.GArtID.ToString());
                                    //                    tmpArt.SetArtValueLagerOrt(clsArtikel.ArtikelField_Reihe, tmpArt.Reihe, false);
                                    //                }
                                    //            }
                                    //        }
                                    //    }
                                    //}

                                }
                            }
                            catch (Exception ex)
                            {
                                clsMessages.Allgemein_ERRORTextShow(ex.ToString());
                            }
                        }
                        //iCountLoop++;
                    }//Ende Group By
                }
            }

            string strSQL = "Select * from LEingang where ASN in (" + Asn.ID.ToString() + ") order by LEingangID;";
            dtInserted = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, 1, "Eingänge");
        }

        private void InitCls()
        {
            Eingang = new Eingaenge();
            ListEingangArticle = new List<Articles>();

            asnVD = new AsnViewData();
            //List_ctrAsnRead_AsnEdifactView = new List<ctrASNRead_AsnEdifactView>();
            //List_ctrAsnRead_AsnArticelEdifactView = new List<ctrASNRead_AsnArticleEdifactView>();
        }
    }
}
