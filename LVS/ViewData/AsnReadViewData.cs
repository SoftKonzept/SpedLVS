using Common.Enumerations;
using Common.Helper;
using Common.Models;
using Common.Views;
using LVS.Communicator;
using LVS.Constants;
using LVS.Converter;
using LVS.CustomProcesses;
using LVS.Models;
using LVS.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LVS.ViewData
{
    public class AsnReadViewData
    {
        internal AsnViewData asnVD { get; set; } = new AsnViewData();
        public List<ctrASNRead_AsnEdifactView> List_ctrAsnRead_AsnEdifactView { get; set; } = new List<ctrASNRead_AsnEdifactView>();
        public List<ctrASNRead_AsnArticleEdifactView> List_ctrAsnRead_AsnArticelEdifactView { get; set; } = new List<ctrASNRead_AsnArticleEdifactView>();
        public AsnReadViewData()
        {
            Lager = new clsLager();
            Lagerdaten = new clsLagerdaten();
            Asn = new clsASN();

            _GL_User = new Globals._GL_USER();
            _GLSystem = new Globals._GL_SYSTEM();
            System = new clsSystem();
            System.Client = new clsClient();
            System.Client.InitClass(clsClient.const_ClientMatchcode_SZG + "_");

        }
        public AsnReadViewData(string myProductionnumberValue, int myUserId) : this()
        {
            string str = string.Empty;
            _GL_User.User_ID = myUserId;
            Productionnumber = myProductionnumberValue;
        }
        public AsnReadViewData(int myUserId) : this()
        {
            _GL_User.User_ID = myUserId;
        }

        public AsnReadViewData(Globals._GL_USER myGLUser, clsSystem mySystem) : this()
        {
            this._GL_User = myGLUser;
            this.System = mySystem;
        }

        public string Productionnumber { get; set; } = string.Empty;
        internal Globals._GL_SYSTEM _GLSystem;
        internal Globals._GL_USER _GL_User;
        internal clsSystem System { get; set; }
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

        public AsnArticleView AsnArticleValue { get; set; } = new AsnArticleView();
        public AsnLfsView AsnLfsValue { get; set; } = new AsnLfsView();
        internal clsLager Lager { get; set; }
        internal clsLagerdaten Lagerdaten { get; set; }
        internal clsASN Asn { get; set; }
        public WorkspaceViewData worspaceVD { get; set; }

        public AddressViewData adressViewData { get; set; }

        public List<AsnArticleView> ListAsnArticleValues { get; set; } = new List<AsnArticleView>();
        public List<AsnLfsView> ListAsnLfsValues { get; set; } = new List<AsnLfsView>();

        public Eingaenge EingangCreated { get; set; }
        public List<Articles> ArticleInEingang { get; set; } = new List<Articles>();
        public List<string> LogTextList { get; set; } = new List<string>();
        public List<string> ErrortextList { get; set; } = new List<string>();

        public string LogText { get; set; } = string.Empty;
        public string Errortext { get; set; } = string.Empty;
        public void Add()
        {
        }
        public void Delete()
        {
        }


        ///-----------------------------------------------------------------------------------------------------
        ///                             sql Statements
        ///-----------------------------------------------------------------------------------------------------

        /// <summary>
        ///             Update sql - String
        /// </summary>
        public string sql_Add
        {
            get
            {
                string strSql = string.Empty;
                return strSql;
            }
        }
        public string sql_Delete
        {
            get
            {
                string strSql = string.Empty;
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
                string strSql = "Update Schaeden SET ";
                //strSql += " Bezeichnung ='" + Damage.Designation + "'";
                //strSql += ", Beschreibung ='" + Damage.Descrition + "'";
                //strSql += ", aktiv =" + Convert.ToInt32(Damage.IsActiv);
                //strSql += ", Art =" + Convert.ToInt32(Damage.Art);
                //strSql += ", Code ='" + Damage.Code + "'";
                //strSql += ", AutoSPL =" + Convert.ToInt32(Damage.AutoSPL);
                //strSql += " WHERE ID=" + Damage.Id;
                return strSql;
            }
        }


        ///-----------------------------------------------------------------------------------------------------
        ///                             Function / Procedure
        ///-----------------------------------------------------------------------------------------------------

        /// <summary>
        ///            Diese Funktion wird Mobile über den Scanner verwendet 
        ///            oder zum einlesen von EDIFACT
        ///            VDA beim GMB Scanner müsste ersetzt werden, da sehr viele Artikel
        ///            CreateStoreInByAsnEdifactView
        /// </summary>
        public bool CreateStoreInByAsnId(int myAsnId)
        {
            bool bReturn = false;
            AsnViewData asnVD = new AsnViewData(myAsnId, this._GL_User);
            //--- Unterscheidung VDA und EDIFACT
            if (asnVD.asnHead.IsRead)
            {
                //Message ASN wurde schon eingelesen
                Errortext = "Die DFÜ/ASN [" + myAsnId + "] ist nicht mehr verfügbar!";
                ErrortextList.Add(Errortext);
            }
            else
            {
                switch (asnVD.asnHead.ASNFileTyp)
                {
                    case constValue_AsnArt.const_Art_VDA4913:
                        clsASN tmpASN = new clsASN();
                        tmpASN.InitClass(_GLSystem, _GL_User);
                        tmpASN.Sys = System;
                        tmpASN.ID = myAsnId;
                        tmpASN.Fill();
                        bReturn = CreateStoreInVDA(tmpASN);
                        break;

                    case constValue_AsnArt.const_Art_EDIFACT_DESADV_D07A:
                        AsnCreateStoreIn asnCreateStoreIn = new AsnCreateStoreIn(asnVD.asnHead);
                        bReturn = asnCreateStoreIn.InitEdifactCreationStoreIn();
                        this.EingangCreated = asnCreateStoreIn.Eingang;
                        this.LogText = asnCreateStoreIn.LogText;
                        break;
                    default:

                        break;
                }
            }
            return bReturn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tmpASN"></param>
        /// <returns></returns>
        public bool CreateStoreInVDA(clsASN myAsn)
        {
            bool bReturn = false;
            clsASN tmpASN = new clsASN();
            tmpASN = myAsn.Copy();
            //tmpASN.InitClass(_GLSystem, _GL_User);
            //tmpASN.Sys = System;
            //tmpASN.ID = myAsnId;
            //tmpASN.Fill();

            if (tmpASN.IsRead)
            {
                //Message ASN wurde schon eingelesen
                Errortext = "Die DFÜ/ASN [" + tmpASN.ID + "] ist nicht mehr verfügbar!";
                ErrortextList.Add(Errortext);
            }
            else
            {
                System.AbBereich = new clsArbeitsbereiche((int)tmpASN.ArbeitsbereichID, (int)BenutzerID);
                tmpASN.Sys = System;

                AsnCreateStoreIn create = new AsnCreateStoreIn(tmpASN);
                if (create.dtInserted.Rows.Count > 0)
                {
                    string tmp = string.Empty;
                    tmp += "ASN : " + create.dtInserted.Rows[0]["ASN"] + Environment.NewLine;

                    for (int i = 0; i < create.dtInserted.Rows.Count; i++)
                    {
                        int iEId = 0;
                        int.TryParse(create.dtInserted.Rows[i]["ID"].ToString(), out iEId);
                        if (iEId > 0)
                        {
                            ViewData.EingangViewData eVd = new EingangViewData(iEId, 1, true);
                            EingangCreated = eVd.Eingang.Copy();
                            ArticleInEingang = eVd.ListArticleInEingang;

                            if (ArticleInEingang.Count > 0)
                            {
                                tmp += "-> Eingang " + create.dtInserted.Rows[i]["LeingangID"] + " erstellt" + Environment.NewLine;
                                tmp += "   Artikel:" + Environment.NewLine;

                                foreach (Articles art in ArticleInEingang)
                                {
                                    tmp += "    LVS-Nr: [" + art.Id + "] -> " + art.LVS_ID + Environment.NewLine;
                                    CustomProcessesViewData cpVD = new CustomProcessesViewData(art.Eingang.Auftraggeber, art.AbBereichID, this._GL_User);
                                    if (cpVD.ExistCustomProcess)
                                    {
                                        if (cpVD.CheckAndExecuteCustomProcess(art.Id, art.LEingangTableID, 0, CustomProcess_Novelis_AccessByArticleCert.const_ProcessLocation_AsnReadViewData_CreateStoreInByAsnId))
                                        {
                                            if (
                                                (cpVD.Process_Novelis_AccessByArticleCert is CustomProcess_Novelis_AccessByArticleCert) &&
                                                (cpVD.Process_Novelis_AccessByArticleCert.Novelis_AccessByArticleCertStatus.Equals(enumCustumerProcessStatus_Novelis_AccessByArticleCert.ArticleBookedInSPL))
                                               )
                                            {
                                                tmp += "      SPL: Artikel Zertifikat liegt nicht vor!" + Environment.NewLine;
                                                //lvTempLog.Items.Add(tmp);
                                            }
                                        }
                                    }
                                }
                                tmp += Environment.NewLine;
                            }
                        }
                    }
                    LogText = tmp;
                    LogTextList.Add(tmp);
                }
                if ((EingangCreated is Eingaenge) && (EingangCreated.Id > 0))
                {
                    bReturn = (EingangCreated.Id > 0);
                }
                else
                {
                    bReturn = false;
                }
            }
            return bReturn;
        }
        /// <summary>
        ///             Erstellung von Eingängen auf Basis der ASN
        /// </summary>
        /// <param name="myAsn"></param>
        /// <param name="myEingang"></param>
        /// <param name="myArticleList"></param>
        /// <param name="myBenutzerId"></param>
        /// <returns></returns>
        //public bool CreateStoreInByAsnEdifactView(ctrASNRead_AsnEdifactView myAsnEdifactView, int myBenutzerId)
        public bool CreateStoreInByAsnEdifactView(Asn myAsn, Eingaenge myEingang, List<Articles> myArticleList, int myBenutzerId)
        {
            bool bReturn = false;
            try
            {
                EingangViewData eingangVD = new EingangViewData();
                //asnVD = new AsnViewData(myAsnEdifactView.Id, this._GL_User);
                asnVD = new AsnViewData(myAsn);

                if (asnVD.asnHead.IsRead)
                {
                    //Message ASN wurde schon eingelesen
                    //Errortext = "Die DFÜ/ASN [" + asnVD.asnHead.Id + "] ist nicht mehr verfügbar!";
                    Errortext = "Die DFÜ/ASN [" + asnVD.asnHead.Id + "] ist nicht mehr verfügbar!";
                    ErrortextList.Add(Errortext);
                }
                else if (myArticleList.Count == 0)
                {
                    Errortext = "Es sind keine Artikeldaten vorhanden!" + Environment.NewLine;
                    Errortext += "Der Prozess wird abgebrochen!";
                    ErrortextList.Add(Errortext);
                }
                else
                {
                    System.AbBereich = new clsArbeitsbereiche((int)asnVD.asnHead.WorkspaceId, (int)BenutzerID);
                    eingangVD = new EingangViewData(myEingang, myBenutzerId);
                    eingangVD.ListArticleInEingang = new List<Articles>(myArticleList);
                    eingangVD.Eingang.Eingangsdatum = DateTime.Now;
                    string strSql = string.Empty;

                    strSql += "BEGIN TRANSACTION; ";

                    strSql += "DECLARE @LEingangTableID as int; ";
                    strSql += "DECLARE @LvsID as int; ";
                    strSql += "DECLARE @ArtID as int; ";

                    strSql += eingangVD.sql_Add;
                    strSql += " Select @@IDENTITY as 'ID'; ";
                    strSql += " SET @LEingangTableID= (Select @@IDENTITY as 'ID'); ";

                    //Vita
                    string tmpAktion = enumLagerAktionen.EingangErstellt.ToString();
                    strSql += " INSERT INTO ArtikelVita (TableID, TableName, Aktion, Datum, UserID, Beschreibung) " +
                                        "VALUES (@LEingangTableID" +
                                                ",'LEingang'" +
                                                ",'" + tmpAktion + "'" +
                                                ",'" + DateTime.Now + "'" +
                                                ", " + myBenutzerId +
                                                ",'Lagereingang ['+CAST(@LEingangTableID as nvarchar)+'] autom. erstellt'" +
                                                "); ";

                    foreach (Articles art in eingangVD.ListArticleInEingang)
                    {
                        art.MandantenID = eingangVD.Eingang.MandantenId;
                        art.AbBereichID = eingangVD.Eingang.ArbeitsbereichId;
                        ArticleViewData artVD = new ArticleViewData(art, myBenutzerId);
                        strSql += artVD.sql_Add_Main;
                        strSql += " SET @ArtID=(Select @@IDENTITY); ";

                        strSql += " SET  @LvsID=" + clsPrimeKeys.SQLStringNewLVSNr(System) + ";";
                        strSql += " Update Artikel SET LVS_ID=@LvsID, LEingangTableID = @LEingangTableID WHERE ID = @ArtID; ";
                        strSql += "UPDATE PrimeKeys SET LvsNr = @LvsID;";
                        //Vita
                        tmpAktion = enumLagerAktionen.ArtikelAdd_Eingang.ToString();
                        strSql = strSql + " INSERT INTO ArtikelVita (TableID, TableName, Aktion, Datum, UserID, Beschreibung" +
                                                    ") " +
                                                    "VALUES (@ArtID" +
                                                            ",'Artikel'" +
                                                            ",'" + tmpAktion + "'" +
                                                            ",'" + DateTime.Now + "'" +
                                                            ", " + myBenutzerId +
                                                            ", 'autom. Artikel hinzugefügt: LVS-NR [ ' + CAST((Select LVS_ID from Artikel where ID=@ArtID) as nvarchar) + ' ] / Eingang ID ['+CAST(@LEingangTableID as nvarchar)+']'" +
                                                            "); ";
                    }
                    strSql = strSql + " Select @LEingangTableID; ";
                    strSql += "COMMIT; ";
                    string strEID = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSql, "ASNInsert", myBenutzerId);
                    int iTmp = 0;
                    int.TryParse(strEID, out iTmp);
                    if (iTmp > 0)
                    {
                        bReturn = true;
                        eingangVD = new EingangViewData(iTmp, myBenutzerId, true);
                        string strSql1 = string.Empty;
                        strSql1 = "Update ASN SET IsRead=1 WHERE ID=" + asnVD.asnHead.Id + ";";
                        bool bUpdatet = clsSQLCOM.ExecuteSQLWithTRANSACTION(strSql1, "UpdateASN", myBenutzerId);
                    }
                    string tmp = string.Empty;
                    if (eingangVD.Eingang.Id > 0)
                    {
                        tmp += "ASN : " + eingangVD.Eingang.ASN + Environment.NewLine;
                        tmp += "-> Eingang [" + eingangVD.Eingang.Id + "] - " + eingangVD.Eingang.LEingangID + " erstellt" + Environment.NewLine;
                        tmp += "   Artikel: " + Environment.NewLine;
                        foreach (Articles art in eingangVD.ListArticleInEingang)
                        {
                            tmp += "    LVS-Nr: [" + art.Id + "]  - " + art.LVS_ID + Environment.NewLine;
                            CustomProcessesViewData cpVD = new CustomProcessesViewData(eingangVD.Eingang.Auftraggeber, art.AbBereichID, this._GL_User);
                            if (cpVD.ExistCustomProcess)
                            {
                                if (cpVD.CheckAndExecuteCustomProcess(art.Id, art.LEingangTableID, 0, CustomProcess_Novelis_AccessByArticleCert.const_ProcessLocation_AsnReadViewData_CreateStoreInByAsnId))
                                {
                                    if (
                                        (cpVD.Process_Novelis_AccessByArticleCert is CustomProcess_Novelis_AccessByArticleCert) &&
                                        (cpVD.Process_Novelis_AccessByArticleCert.Novelis_AccessByArticleCertStatus.Equals(enumCustumerProcessStatus_Novelis_AccessByArticleCert.ArticleBookedInSPL))
                                       )
                                    {
                                        tmp += "      SPL: Artikel Zertifikat liegt nicht vor!" + Environment.NewLine;
                                        //lvTempLog.Items.Add(tmp);
                                    }
                                }
                            }
                        }
                        tmp += Environment.NewLine;
                    }
                    else
                    {
                        tmp += "ASN : " + eingangVD.Eingang.ASN + Environment.NewLine;
                        tmp += "-> Eingang konnte NICHT erstellt" + Environment.NewLine;
                    }
                    LogText = tmp;
                }

                EingangCreated = new Eingaenge();
                EingangCreated = eingangVD.Eingang.Copy();
            }
            catch (Exception ex)
            {
                bReturn = false;
                LogText += "ACHTUNG !" + Environment.NewLine;
                LogText += ex.ToString();
                Errortext = LogText;
            }
            return bReturn;
        }

        public bool CreateStoreInByAsnEdifactViewTest(Asn myAsn, Eingaenge myEingang, List<Articles> myArticleList, int myBenutzerId)
        {
            bool bReturn = false;
            try
            {
                EingangViewData eingangVD = new EingangViewData();
                //asnVD = new AsnViewData(myAsnEdifactView.Id, this._GL_User);
                asnVD = new AsnViewData(myAsn);

                if (asnVD.asnHead.IsRead)
                {
                    //Message ASN wurde schon eingelesen
                    //Errortext = "Die DFÜ/ASN [" + asnVD.asnHead.Id + "] ist nicht mehr verfügbar!";
                    Errortext = "Die DFÜ/ASN [" + asnVD.asnHead.Id + "] ist nicht mehr verfügbar!";
                    ErrortextList.Add(Errortext);
                }
                else if (myArticleList.Count == 0)
                {
                    Errortext = "Es sind keine Artikeldaten vorhanden!" + Environment.NewLine;
                    Errortext += "Der Prozess wird abgebrochen!";
                    ErrortextList.Add(Errortext);
                }
                else
                {
                    System.AbBereich = new clsArbeitsbereiche((int)asnVD.asnHead.WorkspaceId, (int)BenutzerID);
                    eingangVD = new EingangViewData(myEingang, myBenutzerId);
                    eingangVD.ListArticleInEingang = new List<Articles>(myArticleList);
                    eingangVD.Eingang.Eingangsdatum = DateTime.Now;
                    string strSql = string.Empty;

                    strSql += "BEGIN TRANSACTION; ";

                    strSql += "DECLARE @LEingangTableID as int; ";
                    //strSql += "DECLARE @LvsID as int; ";
                    //strSql += "DECLARE @ArtID as int; ";

                    strSql += eingangVD.sql_Add;
                    strSql += " Select @@IDENTITY as 'ID'; ";
                    strSql += " SET @LEingangTableID= (Select @@IDENTITY as 'ID'); ";

                    //Vita
                    string tmpAktion = enumLagerAktionen.EingangErstellt.ToString();
                    strSql += " INSERT INTO ArtikelVita (TableID, TableName, Aktion, Datum, UserID, Beschreibung) " +
                                        "VALUES (@LEingangTableID" +
                                                ",'LEingang'" +
                                                ",'" + tmpAktion + "'" +
                                                ",'" + DateTime.Now + "'" +
                                                ", " + myBenutzerId +
                                                ",'Lagereingang ['+CAST(@LEingangTableID as nvarchar)+'] autom. erstellt'" +
                                                "); ";
                    strSql = strSql + " Select @LEingangTableID; ";
                    strSql += "COMMIT; ";

                    string strEID = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSql, "EingangInsert", myBenutzerId);
                    int iTmp = 0;
                    int.TryParse(strEID, out iTmp);
                    if (iTmp > 0)
                    {
                        eingangVD.Eingang.Id = iTmp;
                        bReturn = true;
                        //strSql = string.Empty;
                        //strSql += "BEGIN TRANSACTION; ";
                        //strSql += "DECLARE @LvsID as int; ";
                        //strSql += "DECLARE @ArtID as int; ";
                        //strSql += "DECLARE @LEingangTableID as int; ";
                        //strSql += "SET @LEingangTableID="+eingangVD.Eingang.Id;

                        int iArticleCount = eingangVD.ListArticleInEingang.Count;
                        foreach (Articles art in eingangVD.ListArticleInEingang)
                        {
                            art.MandantenID = eingangVD.Eingang.MandantenId;
                            art.AbBereichID = eingangVD.Eingang.ArbeitsbereichId;
                            art.LEingangTableID = eingangVD.Eingang.Id;

                            strSql = string.Empty;
                            strSql += "BEGIN TRANSACTION; ";
                            strSql += "DECLARE @LvsID as int; ";
                            strSql += "DECLARE @ArtID as int; ";
                            strSql += "DECLARE @LEingangTableID as int; ";
                            strSql += "SET @LEingangTableID=" + eingangVD.Eingang.Id + ";";

                            ArticleViewData artVD = new ArticleViewData(art, myBenutzerId);
                            strSql += artVD.sql_Add_Main;
                            strSql += " SET @ArtID=(Select @@IDENTITY); ";

                            strSql += " SET  @LvsID=" + clsPrimeKeys.SQLStringNewLVSNr(System) + "; ";
                            strSql += " Update Artikel SET LVS_ID=@LvsID, LEingangTableID = @LEingangTableID WHERE ID = @ArtID; ";
                            strSql += " UPDATE PrimeKeys SET LvsNr = @LvsID; ";
                            //Vita
                            tmpAktion = enumLagerAktionen.ArtikelAdd_Eingang.ToString();
                            strSql = strSql + " INSERT INTO ArtikelVita (TableID, TableName, Aktion, Datum, UserID, Beschreibung" +
                                                        ") " +
                                                        "VALUES (@ArtID" +
                                                                ",'Artikel'" +
                                                                ",'" + tmpAktion + "'" +
                                                                ",'" + DateTime.Now + "'" +
                                                                ", " + myBenutzerId +
                                                                ", 'autom. Artikel hinzugefügt: LVS-NR [ ' + CAST((Select LVS_ID from Artikel where ID=@ArtID) as nvarchar) + ' ] / Eingang ID ['+CAST(@LEingangTableID as nvarchar)+']'" +
                                                                "); ";
                            //strSql += "UPDATE PrimeKeys SET LvsNr = @LvsID; ";
                            strSql += "COMMIT; ";
                            bool bInsertArtOk = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "ASNInsert", myBenutzerId);
                            if (!bInsertArtOk)
                            {
                                eingangVD.ListArticleInEingang.Clear();
                                bReturn = false;
                                break;
                            }

                        }
                        //strSql += "UPDATE PrimeKeys SET LvsNr = @LvsID;";
                        //strSql += "COMMIT; ";
                        //bool bOk = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "ASNInsert", myBenutzerId);                      

                        eingangVD.Eingang.Id = iTmp;
                        eingangVD = new EingangViewData(iTmp, myBenutzerId, true);
                        bReturn = (eingangVD.ListArticleInEingang.Count == iArticleCount);

                        if (bReturn)
                        {
                            string strSql1 = string.Empty;
                            strSql1 = "Update ASN SET IsRead=1 WHERE ID=" + asnVD.asnHead.Id + ";";
                            bool bUpdatet = clsSQLCOM.ExecuteSQLWithTRANSACTION(strSql1, "UpdateASN", myBenutzerId);

                            //eingangVD = new EingangViewData(iTmp, myBenutzerId, true);
                        }
                        else
                        {
                            eingangVD.Delete();
                            eingangVD = new EingangViewData();
                        }
                        //bReturn = bOk;
                    }

                    string tmp = string.Empty;
                    if (eingangVD.Eingang.Id > 0)
                    {
                        tmp += "ASN : " + eingangVD.Eingang.ASN + Environment.NewLine;
                        tmp += "-> Eingang [" + eingangVD.Eingang.Id + "] - " + eingangVD.Eingang.LEingangID + " erstellt" + Environment.NewLine;
                        tmp += "   Artikel: " + Environment.NewLine;
                        foreach (Articles art in eingangVD.ListArticleInEingang)
                        {
                            tmp += "    LVS-Nr: [" + art.Id + "] - " + art.LVS_ID + Environment.NewLine;
                            CustomProcessesViewData cpVD = new CustomProcessesViewData(eingangVD.Eingang.Auftraggeber, eingangVD.Eingang.ArbeitsbereichId, this._GL_User);
                            if (cpVD.ExistCustomProcess)
                            {
                                if (cpVD.CheckAndExecuteCustomProcess(art.Id, art.LEingangTableID, 0, CustomProcess_Novelis_AccessByArticleCert.const_ProcessLocation_AsnReadViewData_CreateStoreInByAsnId))
                                {
                                    if (
                                        (cpVD.Process_Novelis_AccessByArticleCert is CustomProcess_Novelis_AccessByArticleCert) &&
                                        (cpVD.Process_Novelis_AccessByArticleCert.Novelis_AccessByArticleCertStatus.Equals(enumCustumerProcessStatus_Novelis_AccessByArticleCert.ArticleBookedInSPL))
                                       )
                                    {
                                        tmp += "      SPL: Artikel Zertifikat liegt nicht vor!" + Environment.NewLine;
                                        //lvTempLog.Items.Add(tmp);
                                    }
                                }
                            }
                        }
                        tmp += Environment.NewLine;
                    }
                    else
                    {
                        tmp += "ASN : " + eingangVD.Eingang.ASN + Environment.NewLine;
                        tmp += "-> Eingang konnte NICHT erstellt" + Environment.NewLine;
                    }
                    LogText = tmp;
                }

                EingangCreated = new Eingaenge();
                EingangCreated = eingangVD.Eingang.Copy();
            }
            catch (Exception ex)
            {
                bReturn = false;
                LogText += "ACHTUNG !" + Environment.NewLine;
                LogText += ex.ToString();
                Errortext = LogText;
            }
            return bReturn;
        }
        /// <summary>
        ///                 Durchsucht die ASNValue und EdifactValue Tabelle nach der gesuchten Produktionsnummer
        /// </summary>
        /// <param name="mySearchValue"></param>
        /// <returns></returns>
        public List<AsnSearchByProductionNoHelper> GetStoreinArticleValueIdListBySearchValue(string mySearchValue)
        {
            List<AsnSearchByProductionNoHelper> list = new List<AsnSearchByProductionNoHelper>();

            DataTable dt = new DataTable();
            string strSql = Asn.sql_GetASNByAsnValue(mySearchValue);

            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ASN");


            //------------------ Report info ------------------------------
            string strMessage = string.Empty;

            if (dt.Rows.Count > 0)
            {
                strMessage = "Produktionnummer gefunden: " + Environment.NewLine;
                strMessage += "-------------------------------------------------------" + Environment.NewLine;
                strMessage += "gesuchte Produktionnummer: " + mySearchValue + Environment.NewLine;
                strMessage += "Zeit: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm") + Environment.NewLine;
                strMessage += Environment.NewLine;
                strMessage += "SQL STRING: " + Environment.NewLine;
                strMessage += strSql + Environment.NewLine;

                //bool bProductionNoExist = false;
                foreach (DataRow row in dt.Rows)
                {
                    AsnSearchByProductionNoHelper tmp = new AsnSearchByProductionNoHelper();
                    int iTmp = 0;
                    int.TryParse(row["ASNID"].ToString(), out iTmp);
                    tmp.AsnId = iTmp;
                    tmp.AsnValue = row["AsnValue"].ToString();

                    iTmp = 0;
                    int.TryParse(row["WorkspaceId"].ToString(), out iTmp);
                    tmp.WorkspaceId = iTmp;
                    tmp.AsnFileTyp = row["AsnFileTyp"].ToString();

                    if ((tmp.AsnId > 0) && (tmp.WorkspaceId > 0) && (tmp.AsnValue.Length > 0))
                    {
                        if (!list.Contains(tmp))
                        {
                            list.Add(tmp);
                        }
                    }

                    strMessage += Environment.NewLine;
                    strMessage += "ASNID: " + tmp.AsnId + Environment.NewLine;
                    strMessage += "ASNValue: " + tmp.AsnValue + Environment.NewLine;
                    strMessage += "Arbeitsbereich: " + tmp.WorkspaceId + Environment.NewLine;
                    strMessage += Environment.NewLine;

                    if (strMessage.Length > 0)
                    {

                        try
                        {
                            clsMail checkMail = new clsMail(true);
                            checkMail.Message = strMessage;
                            checkMail.Subject += DateTime.Now.ToShortDateString() + "- LVSScan - Suche Produktionsnummer in ASN erfolgreich !!!";
                            checkMail.SendCheckMail();
                        }
                        catch (Exception ex)
                        {

                            clsMail EMail = new clsMail();
                            EMail.InitClass(this._GL_User, this.System);
                            EMail.Subject = this.System.Client.MatchCode + DateTime.Now.ToShortDateString() + "- Error LVSScan - Send CheckMail / Produktionsnummer Suche!!!";
                            EMail.Message = ex.Message;
                            EMail.SendError();
                        }
                    }
                }
                bool bExistSearchValue = false;

                foreach (var item in list)
                {
                    if (item.AsnValue.ToUpper().Contains(mySearchValue.ToUpper()))
                    {
                        item.AsnValue = mySearchValue;
                    }
                    if (item.AsnValue.ToUpper().Equals(mySearchValue.ToUpper()))
                    {
                        bExistSearchValue = true;
                    }
                }

                if ((!bExistSearchValue) || (list.Count > 1))
                {
                    try
                    {
                        clsMail checkMail = new clsMail(true);
                        checkMail.Message = strMessage;
#if DEBUG
                        checkMail.Subject += "DEBUG -> ";

#endif
                        checkMail.Subject += DateTime.Now.ToShortDateString() + "- LVSScan - Check Suche Produktionsnummer in ASN !!!";
                        checkMail.SendCheckMail();
                    }
                    catch (Exception ex)
                    {

                        clsMail EMail = new clsMail();
                        EMail.InitClass(this._GL_User, this.System);
                        EMail.Subject = this.System.Client.MatchCode + DateTime.Now.ToShortDateString() + "- Error LVSScan - Send CheckMail / Produktionsnummer Suche!!!";
                        EMail.Message = ex.Message;
                        EMail.SendError();
                    }
                }
            }
            else
            {
                strMessage = "Produktionnummer in ASNValue nicht vorhanden: " + Environment.NewLine;
                strMessage += "-------------------------------------------------------" + Environment.NewLine;
                strMessage += "gesuchte Produktionnummer: " + mySearchValue + Environment.NewLine;
                strMessage += "Zeit: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm") + Environment.NewLine;
                strMessage += Environment.NewLine;
                strMessage += "SQL STRING: " + Environment.NewLine;
                strMessage += strSql + Environment.NewLine;

                try
                {
                    strMessage += Environment.NewLine;
                    strMessage += "Ergebnis: RowsCount=" + dt.Rows.Count + Environment.NewLine;


                    clsMail checkMail = new clsMail(true);
                    checkMail.Message = strMessage;
                    checkMail.Subject += DateTime.Now.ToShortDateString() + "- LVSScan - Check Suche Produktionsnummer in ASN !!!";
                    checkMail.SendCheckMail();
                }
                catch (Exception ex)
                {

                    clsMail EMail = new clsMail();
                    EMail.InitClass(this._GL_User, this.System);
                    EMail.Subject = this.System.Client.MatchCode + DateTime.Now.ToShortDateString() + "- Error LVSScan - Send CheckMail / Produktionsnummer Suche!!!";
                    EMail.Message = ex.Message;
                    EMail.SendError();
                }
            }
            this.LogText = strMessage;
            return list;
        }

        public bool GetStoreinArticleValueList(List<int> myListAsnId)
        {
            bool bReturn = true;

            ListAsnArticleValues = new List<AsnArticleView>();

            worspaceVD = new WorkspaceViewData();
            worspaceVD.GetWorkspaceList();

            DataTable dtAsn = new DataTable();
            DataTable dtAsnLfsView = new DataTable();
            DataTable dtEdiArtikel = new DataTable();

            foreach (Workspaces ws in worspaceVD.ListWorkspace)
            {
                if (ws.ASNTransfer)
                {
                    System.AbBereich = new clsArbeitsbereiche(ws.Id, (int)BenutzerID);
                    _GLSystem.sys_ArbeitsbereichID = ws.Id;
                    _GLSystem.sys_Arbeitsbereichsname = ws.Name;
                    _GLSystem.sys_Arbeitsbereich_ASNTransfer = ws.ASNTransfer;

                    Asn.InitClass(_GLSystem, _GL_User);
                    Asn.Sys = System.Copy();

                    DataTable dtTmp = new DataTable();
                    if (myListAsnId.Count > 0)
                    {
                        dtTmp = Asn.GetASNByASNId(myListAsnId);
                        dtAsn = Asn.EditTableForUse(dtTmp);
                    }
                    else
                    {
                        dtTmp = Asn.GetASN();
                        dtAsn = Asn.EditTableForUse(dtTmp);
                    }

                    if (dtAsn.Rows.Count > 0)
                    {
                        AsnCreateLfsView lfsView = new AsnCreateLfsView(_GLSystem, _GL_User, System);
                        dtAsnLfsView = lfsView.GetLfsKopfdaten(ref dtAsn);
                        ListAsnLfsValues = lfsView.ConvertDataTableToList(dtAsnLfsView);

                        AsnConvertToStoreInValue asnConvertValue = new AsnConvertToStoreInValue(_GLSystem, _GL_User, System);
                        dtEdiArtikel = asnConvertValue.GetArtikelDaten1(ref dtAsn);

                        foreach (DataRow r in dtEdiArtikel.Rows)
                        {
                            SetValueArticle(r);
                            AsnArticleValue.Workspace = ws.Copy();
                            AsnArticleValue.WorkspaceId = ws.Id;
                            if (AsnArticleValue.AsnId > 0)
                            {
                                AsnArticleValue.LfdNr = ListAsnArticleValues.Count + 1;
                                ListAsnArticleValues.Add(AsnArticleValue);
                            }
                        }
                    }
                }
            }
            bReturn = (ListAsnArticleValues.Count > 0);
            return bReturn;
        }

        public bool GetStoreinArticleValueListBySearchValueVDA(List<AsnSearchByProductionNoHelper> myListAsnId)
        {
            bool bReturn = true;
            ListAsnArticleValues = new List<AsnArticleView>();

            worspaceVD = new WorkspaceViewData();
            worspaceVD.GetWorkspaceList();

            DataTable dtAsn = new DataTable();
            DataTable dtAsnLfsView = new DataTable();
            DataTable dtEdiArtikel = new DataTable();

            foreach (Workspaces ws in worspaceVD.ListWorkspace)
            {
                //worspaceVD = new WorkspaceViewData(item.WorkspaceId);
                //Workspaces ws = worspaceVD.Workspace.Copy();
                if (ws.ASNTransfer)
                {
                    System.AbBereich = new clsArbeitsbereiche(ws.Id, (int)BenutzerID);
                    _GLSystem.sys_ArbeitsbereichID = ws.Id;
                    _GLSystem.sys_Arbeitsbereichsname = ws.Name;
                    _GLSystem.sys_Arbeitsbereich_ASNTransfer = ws.ASNTransfer;

                    Asn.InitClass(_GLSystem, _GL_User);
                    Asn.Sys = System.Copy();

                    var resAsnidWs = myListAsnId.Where(x => x.WorkspaceId == ws.Id).ToList();
                    List<int> tmpAsnId = new List<int>();
                    foreach (AsnSearchByProductionNoHelper x in resAsnidWs)
                    {
                        if (!tmpAsnId.Contains(x.AsnId))
                        {
                            tmpAsnId.Add(x.AsnId);
                        }
                    }

                    DataTable dtTmp = new DataTable();
                    if (tmpAsnId.Count > 0)
                    {
                        dtTmp = Asn.GetASNByASNId(tmpAsnId);
                        dtAsn = Asn.EditTableForUse(dtTmp);

                        if (dtAsn.Rows.Count > 0)
                        {
                            AsnCreateLfsView lfsView = new AsnCreateLfsView(_GLSystem, _GL_User, System);
                            dtAsnLfsView = lfsView.GetLfsKopfdaten(ref dtAsn);
                            ListAsnLfsValues = lfsView.ConvertDataTableToList(dtAsnLfsView);

                            AsnConvertToStoreInValue asnConvertValue = new AsnConvertToStoreInValue(_GLSystem, _GL_User, System);
                            dtEdiArtikel = asnConvertValue.GetArtikelDaten1(ref dtAsn);

                            foreach (DataRow r in dtEdiArtikel.Rows)
                            {
                                SetValueArticle(r);
                                AsnArticleValue.Workspace = ws.Copy();
                                AsnArticleValue.WorkspaceId = ws.Id;
                                if (AsnArticleValue.AsnId > 0)
                                {
                                    AsnArticleValue.LfdNr = ListAsnArticleValues.Count + 1;
                                    ListAsnArticleValues.Add(AsnArticleValue);
                                }
                            }
                        }
                    }
                }
            }
            bReturn = (ListAsnArticleValues.Count > 0);
            return bReturn;
        }

        public bool GetStoreinArticleValueListBySearchValueEdifact(List<AsnSearchByProductionNoHelper> myListAsnId)
        {
            bool bReturn = true;
            ListAsnArticleValues = new List<AsnArticleView>();

            worspaceVD = new WorkspaceViewData();
            worspaceVD.GetWorkspaceList();

            AsnViewData asnVD = new AsnViewData();

            foreach (Workspaces ws in worspaceVD.ListWorkspace)
            {
                if (ws.ASNTransfer)
                {
                    System.AbBereich = new clsArbeitsbereiche(ws.Id, (int)BenutzerID);
                    _GLSystem.sys_ArbeitsbereichID = ws.Id;
                    _GLSystem.sys_Arbeitsbereichsname = ws.Name;
                    _GLSystem.sys_Arbeitsbereich_ASNTransfer = ws.ASNTransfer;

                    Asn.InitClass(_GLSystem, _GL_User);
                    Asn.Sys = System.Copy();

                    var resAsnidWs = myListAsnId.Where(x => x.WorkspaceId == ws.Id).ToList();
                    if (resAsnidWs.Count > 0)
                    {
                        List<int> tmpAsnId = new List<int>();
                        foreach (AsnSearchByProductionNoHelper x in resAsnidWs)
                        {
                            if (!tmpAsnId.Contains(x.AsnId))
                            {
                                tmpAsnId.Add(x.AsnId);
                            }
                        }


                        asnVD = new AsnViewData(0, this._GL_User);
                        asnVD.GetListbyAsnId(tmpAsnId);
                        if (asnVD.ListAsn.Count > 0)
                        {
                            asnVD.FillAsnEdifactViewAndArticleEdifactView(asnVD.ListAsn);
                            List_ctrAsnRead_AsnEdifactView = asnVD.List_ctrAsnRead_AsnEdifactView;
                            ListAsnLfsValues = new List<AsnLfsView>();
                            int iCountLfs = 0;
                            //--- konvertieren in AsnLfsView für den Scanner
                            foreach (ctrASNRead_AsnEdifactView asnEdifactView in asnVD.List_ctrAsnRead_AsnEdifactView)
                            {
                                AsnEdifactViewToAsnLfsView tmpLfs = new AsnEdifactViewToAsnLfsView(asnEdifactView);
                                if (!ListAsnLfsValues.Contains(tmpLfs.AsnLfsView))
                                {
                                    iCountLfs++;
                                    tmpLfs.AsnLfsView.LfdNr = iCountLfs;
                                    ListAsnLfsValues.Add(tmpLfs.AsnLfsView);
                                }
                            }

                            List_ctrAsnRead_AsnArticelEdifactView = asnVD.List_ctrAsnRead_AsnArticelEdifactView;
                            ListAsnArticleValues = new List<AsnArticleView>();
                            int iCountArticle = 0;
                            foreach (ctrASNRead_AsnArticleEdifactView asnArticleEdifactView in asnVD.List_ctrAsnRead_AsnArticelEdifactView)
                            {
                                AsnArticleEdifactViewToAsnArticleView tmpArticle = new AsnArticleEdifactViewToAsnArticleView(asnArticleEdifactView);
                                if (!ListAsnArticleValues.Contains(tmpArticle.asnArticleView))
                                {
                                    iCountArticle++;
                                    tmpArticle.asnArticleView.LfdNr = iCountArticle;
                                    ListAsnArticleValues.Add(tmpArticle.asnArticleView);
                                }
                            }
                        }
                    }
                }
            }
            bReturn = (ListAsnArticleValues.Count > 0);
            return bReturn;
        }
        public void Fill()
        {
            //string strSql = string.Empty;
            //strSql = this.sql_GetDamage;
            //System.Data.DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "GetList", "Damages", BenutzerID);
            //SetValue(dt);
        }

        private void SetValueArticle(DataRow myRow)
        {
            AsnArticleValue = new AsnArticleView();

            //int iTmp = 0;
            //int.TryParse(myRow["Lf"].ToString(), out iTmp);
            //this.AsnArticleValue.Id = iTmp;

            int iTmp = 0;
            int.TryParse(myRow["ASN"].ToString(), out iTmp);
            this.AsnArticleValue.AsnId = iTmp;

            this.AsnArticleValue.Dicke = (decimal)myRow["Dicke"];
            this.AsnArticleValue.Breite = (decimal)myRow["Breite"];
            this.AsnArticleValue.Laenge = (decimal)myRow["Laenge"];
            this.AsnArticleValue.Netto = (decimal)myRow["Netto"];
            this.AsnArticleValue.Brutto = (decimal)myRow["Brutto"];
            this.AsnArticleValue.Werksnummer = myRow["Werksnummer"].ToString();
            this.AsnArticleValue.Produktionsnummer = myRow["Produktionsnummer"].ToString();
            this.AsnArticleValue.Charge = myRow["Charge"].ToString();
            this.AsnArticleValue.exMaterialnummer = myRow["exMaterialnummer"].ToString();
            this.AsnArticleValue.Bestellnummer = myRow["Bestellnummer"].ToString();
            this.AsnArticleValue.Position = myRow["Position"].ToString();
            this.AsnArticleValue.Gut = myRow["Gut"].ToString();
            this.AsnArticleValue.LfsNr = myRow["LfsNr"].ToString();
            this.AsnArticleValue.VehicleNo = myRow["VehicleNo"].ToString();

        }
        /// <summary>
        ///             Ermittelt die Liste der vorliegenden ASN im Darstellungsformat im Grid
        /// </summary>
        public void GetASNListForCtrAsnRead()
        {
            List_ctrAsnRead_AsnEdifactView = new List<ctrASNRead_AsnEdifactView>();
            List_ctrAsnRead_AsnArticelEdifactView = new List<ctrASNRead_AsnArticleEdifactView>();

            //AsnViewData asnVD = new AsnViewData((int)this.System.AbBereich.ID);
            AsnViewData asnVD = new AsnViewData(this.System);
            var list = asnVD.GetListEDIFACT();
            asnVD.FillAsnEdifactViewAndArticleEdifactView(list);
            List_ctrAsnRead_AsnEdifactView = asnVD.List_ctrAsnRead_AsnEdifactView;
            List_ctrAsnRead_AsnArticelEdifactView = asnVD.List_ctrAsnRead_AsnArticelEdifactView;
        }

        ///********************************************************************************************
        ///                             static
        ///*******************************************************************************************
        public static bool ExistNewASNToProceed(decimal myBenuzter, decimal myAbBereich)
        {
            string strSQL = string.Empty;
            strSQL = "SELECT ID FROM ASN " +
                                    "WHERE " +
                                        " IsRead=0 " +
                                        " AND Direction='IN' " +
                                        " AND ArbeitsbereichID=" + (Int32)myAbBereich +
                                        " AND ASNFileTyp IN ('" + constValue_AsnArt.const_Art_VDA4913 + "'" +
                                        ",'" + constValue_AsnArt.const_ArtBeschreibung_EDIFACT_ASN_96A + "'" +
                                        ",'" + constValue_AsnArt.const_ArtBeschreibung_EDIFACT_ASN_D97A + "'" +
                                        ",'" + constValue_AsnArt.const_ArtBeschreibung_EDIFACT_DESADV_D07A + "'" +
                                        ")"
                                        ;
            return clsSQLCOM.ExecuteSQL_GetValueBool(strSQL, myBenuzter);
        }

        public static bool ExistNewASNToProceedByAsnFileType(decimal myBenuzter, decimal myAbBereich, List<string> myListEdifact)
        {
            string strSQL = string.Empty;
            strSQL = "SELECT ID FROM ASN " +
                                    "WHERE " +
                                        " IsRead=0 " +
                                        " AND Direction='IN' " +
                                        " AND ArbeitsbereichID=" + (Int32)myAbBereich +
                                        " AND ASNFileTyp IN (" + string.Join(", ", myListEdifact.Select(item => $"'{item}'")) + "); ";

            return clsSQLCOM.ExecuteSQL_GetValueBool(strSQL, myBenuzter);
        }
    }
}
