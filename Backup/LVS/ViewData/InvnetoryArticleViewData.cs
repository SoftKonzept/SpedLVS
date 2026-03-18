using Common.Enumerations;
using Common.Models;
using Common.SqlStatementCreater;
using System;
using System.Collections.Generic;
using System.Data;


namespace LVS.ViewData
{
    public class InvnetoryArticleViewData
    {
        public Common.Models.InventoryArticles InventoryArticle { get; set; }
        public List<Common.Models.InventoryArticles> ListInventoryArticle = new List<Common.Models.InventoryArticles>();
        public System.Data.DataTable dtInventoryArticleList { get; set; }
        public System.Data.DataTable dtInventoryArticleViewList { get; set; }

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

        public InvnetoryArticleViewData()
        {
            InitCls();
        }

        public InvnetoryArticleViewData(InventoryArticles myInvArticle)
        {
            InitCls();
            this.InventoryArticle = myInvArticle.Copy();
        }

        public InvnetoryArticleViewData(DatabaseManipulations databaseManuipulation)
        {
            InitCls();
            ManipulateDB(databaseManuipulation);
        }

        private void ManipulateDB(DatabaseManipulations databaseManuipulation)
        {
            //-- sort by Action
            sqlCreater_InventoryArticles creator = new sqlCreater_InventoryArticles(databaseManuipulation);
            switch (databaseManuipulation.Action)
            {
                case enumDatabaseAction.Add:
                    break;
                case enumDatabaseAction.Update:
                    sqlUpdate = creator.Sql_Upate;
                    break;
                case enumDatabaseAction.Delete:
                    break;
            }
        }
        public InvnetoryArticleViewData(int myInventoryId, int myInventoryArticleId, bool bFillClassOnly)
        {
            InitCls();
            InventoryArticle.Id = myInventoryArticleId;
            InventoryArticle.InventoryId = myInventoryId;
            if (!bFillClassOnly)
            {
                GetInventoryArticleList();
            }
            if (InventoryArticle.Id > 0)
            {
                Fill();
            }
        }

        public InvnetoryArticleViewData(int myInventoryId, bool getView)
        {
            InitCls();
            InventoryArticle.InventoryId = myInventoryId;
            if (getView)
            {
                GetInventoryArticleDatatable();
            }
            else
            {
                GetInventoryArticleList();
            }
        }


        private void InitCls()
        {
            this.InventoryArticle = new Common.Models.InventoryArticles();

            this.InventoryArticle.Id = 0;
            this.InventoryArticle.InventoryId = 0;
            this.InventoryArticle.Description = string.Empty;
            this.InventoryArticle.ArtikelId = 0;
            this.InventoryArticle.LvsNummer = 0;
            this.InventoryArticle.Status = Common.Enumerations.enumInventoryArticleStatus.NotSet;
            this.InventoryArticle.Text = string.Empty;
            this.InventoryArticle.Created = DateTime.Now;
            this.InventoryArticle.Scanned = new DateTime(1900, 1, 1);
            this.InventoryArticle.ScannedUserId = 0;

            //dtInventoryArticleList = new DataTable();
        }
        ///<summary>clsInventoryArticle / Add</summary>
        ///<remarks></remarks>
        public void Add()
        {
            string strSql = sqlAdd;
            strSql = strSql + "Select @@IDENTITY as 'ID' ";

            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, 0);
            int.TryParse(strTmp, out int iTmp);
            InventoryArticle.Id = iTmp;

            ////Add Logbucheintrag Eintrag
            //string strInfo = "Neue Inventur erstellt erstellt: ID [" + Id.ToString() + "] / Name: " + Name;
            //Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Eintrag.ToString(), strInfo);
        }
        ///<summary>clsInventory / Update</summary>
        ///<remarks>Update eines Datensatzes in der DB über die ID.</remarks>
        public bool Update()
        {
            string strSql = sqlUpdate;
            //bool bReturn = clsSQLcon.ExecuteSQL(strSql, 0);
            bool bReturn = clsSQLcon.ExecuteSQLWithTRANSACTION(sqlUpdate, "InventoryArtikelUpdate", 0);
            return bReturn;
            // Logbucheintrag Eintrag
            //string strInfo = "Inventur geändert: Id: " + Id.ToString() + " / Name: " + Name;
            //Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), strInfo);
        }

        public bool Update_ArticleStatus()
        {
            string strSql = sqlUpdate_ArticleStatus;
            bool bReturn = clsSQLcon.ExecuteSQLWithTRANSACTION(sqlUpdate, "InventoryArtikelUpdate", 0);
            return bReturn;
        }

        ///<summary>clsInventory / Delete</summary>
        ///<remarks></remarks>
        public void Delete()
        {
            string strSql = sqlDelete;
            if (clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "InventoryArtikelDelete", 0))
            {
                //Add Logbucheintrag 
                //string myBeschreibung = "Inventur gelöscht: ID: " + Id.ToString() + " / Name: " + Name;
                //Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);
            }
        }
        ///<summary>clsInventory / FillDaten</summary>
        ///<remarks>Ermittel die Daten anhand der TableID.</remarks>
        public void Fill()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string strSql = sqlGet;

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, 0, "InventoryArticle");
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
            InventoryArticle.Id = (int)myRow["Id"];
            InventoryArticle.InventoryId = (int)myRow["InventoryId"];
            InventoryArticle.Description = myRow["Description"].ToString();
            InventoryArticle.ArtikelId = (int)myRow["ArtikelId"];



            int iTmp = 0;
            if (myRow["LVSNr"] != null)
            {
                int.TryParse(myRow["LVSNr"].ToString(), out iTmp);
                InventoryArticle.LvsNummer = iTmp;
            }

            iTmp = 0;
            iTmp = (int)myRow["Status"];
            Enum.TryParse(iTmp.ToString(), out Common.Enumerations.enumInventoryArticleStatus enumTmp);
            InventoryArticle.Status = enumTmp;
            InventoryArticle.Text = myRow["Text"].ToString();
            InventoryArticle.Created = (DateTime)myRow["Created"];

            DateTime dtTmp = new DateTime(1900, 1, 1);
            if (myRow["Scanned"] != null)
            {
                DateTime.TryParse(myRow["Scanned"].ToString(), out dtTmp);
            }
            InventoryArticle.Scanned = dtTmp;
            InventoryArticle.ScannedUserId = (int)myRow["ScannedUserId"];

            if (InventoryArticle.ArtikelId > 0)
            {
                InventoryArticle.Artikel = new Common.Models.Articles();
                ArticleViewData artVM = new ArticleViewData(InventoryArticle.ArtikelId, this._GL_User);
                InventoryArticle.Artikel = artVM.Artikel.Copy();
            }
        }

        public void GetInventoryArticleList()
        {
            ListInventoryArticle = new List<Common.Models.InventoryArticles>();
            GetInventoryArticleDatatable();
            //System.Data.DataTable dt = new System.Data.DataTable();
            //string strSql = sqlGetInventoryArticleList;
            //dtInventoryArticleList = clsSQLcon.ExecuteSQL_GetDataTable(strSql, 0, "InventoryArticle");
            foreach (DataRow r in dtInventoryArticleList.Rows)
            {
                InventoryArticle = new Common.Models.InventoryArticles();
                SetValue(r);
                ListInventoryArticle.Add(InventoryArticle);
            }
        }

        public void GetInventoryArticleDatatable()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string strSql = sqlGetInventoryArticleList;
            dtInventoryArticleList = clsSQLcon.ExecuteSQL_GetDataTable(strSql, 0, "InventoryArticle");
        }

        public string sqlAdd
        {
            get
            {
                string strSql = string.Empty;
                strSql = "INSERT INTO InventoryArticle (InventoryId, Description, ArtikelId, [Status], Text, Created, Scanned,ScannedUserId) " +
                                                "VALUES (" +
                                                       " " + InventoryArticle.InventoryId + " " +
                                                       ", '" + InventoryArticle.Description + "' " +
                                                       ", " + InventoryArticle.ArtikelId + " " +
                                                       ", " + (int)InventoryArticle.Status + " " +
                                                       ", '" + InventoryArticle.Text + "' " +
                                                       ", '" + InventoryArticle.Created + "' " +
                                                       ", '" + InventoryArticle.Scanned + "' " +
                                                        ", " + InventoryArticle.ScannedUserId +
                                                       "); ";
                return strSql;
            }
        }
        private string _sqlUpdate = string.Empty;
        public string sqlUpdate
        {
            get
            {
                if (_sqlUpdate.Equals(string.Empty))
                {
                    _sqlUpdate = "Update InventoryArticle SET" +
                                            //" InventoryId = " + InventoryArticle.InventoryId +
                                            " Description = '" + InventoryArticle.Description + "' " +
                                            //", ArtikelId = " + InventoryArticle.ArtikelId +
                                            ", [Status] = " + (int)InventoryArticle.Status +
                                            ", Text = '" + InventoryArticle.Text + "' " +
                                            //", Created = '" + InventoryArticle.Created + "'" +
                                            ", Scanned = '" + InventoryArticle.Scanned + "'" +
                                            ", ScannedUserId = " + InventoryArticle.ScannedUserId +
                                            " WHERE Id = " + InventoryArticle.Id + "; ";
                }
                return _sqlUpdate;
            }
            set { _sqlUpdate = value; }
        }

        public string sqlUpdate_ArticleStatus
        {
            get
            {
                if (_sqlUpdate.Equals(string.Empty))
                {
                    _sqlUpdate = "Update InventoryArticle SET" +
                                            " [Status] = " + (int)InventoryArticle.Status +
                                            ", Text = '" + InventoryArticle.Text + "' " +
                                            ", Scanned = '" + InventoryArticle.Scanned + "'" +
                                            ", ScannedUserId = " + InventoryArticle.ScannedUserId +
                                            " WHERE Id = " + InventoryArticle.Id + "; ";
                }
                return _sqlUpdate;
            }
            set { _sqlUpdate = value; }
        }

        public string sqlDelete
        {
            get
            {
                string strSql = "DELETE InventoryArticle WHERE Id = " + InventoryArticle.Id + "; ";
                return strSql;
            }
        }
        public string sqlDeleteByInventoryId
        {
            get
            {
                string strSql = "DELETE InventoryArticle WHERE InventoryId = " + InventoryArticle.InventoryId + "; ";
                return strSql;
            }
        }


        public string sqlGetMain
        {
            get
            {
                string strSql = "Select i.* " +
                                //", a.Id as ArtikelId " +  // ArtikelId ist FS in InventoryArticle
                                ", a.LVS_Id as LVSNr " +
                                ", a.Produktionsnummer " +
                                ", a.Werksnummer " +
                                    " FROM InventoryArticle i " +
                                        "inner join Artikel a on a.Id = i.ArtikelId " +
                                        "left join [User] u on u.id = i.ScannedUserId ";
                return strSql;
            }
        }
        public string sqlGet
        {
            get
            {
                string strSql = sqlGetMain;
                strSql += " WHERE i.Id=" + InventoryArticle.Id.ToString() + "; ";

                return strSql;
            }
        }
        public string sqlGetInventoryArticleList
        {
            get
            {
                string strSql = sqlGetMain;
                strSql += " WHERE i.InventoryId=" + InventoryArticle.InventoryId.ToString() + "; ";
                return strSql;
            }
        }


    }
}
