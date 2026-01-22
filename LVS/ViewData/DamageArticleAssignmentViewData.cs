using Common.Models;
using Common.Views;
using System;
using System.Collections.Generic;
using System.Data;

namespace LVS.ViewData
{
    public class DamageArticleAssignmentViewData
    {
        public Damages Damage { get; set; }
        public DamageArticleAssignmentView DamageArticleView { get; set; }
        public Articles Article { get; set; }
        public ArticleViewData artViewData { get; set; }
        public Damages Damages { get; set; }
        public DamageViewData damageViewData { get; set; }

        public List<DamageArticleAssignmentView> ArticleDamagesList { get; set; } = new List<DamageArticleAssignmentView>();

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

        public DamageArticleAssignmentViewData()
        {
            Article = new Articles();
            Damage = new Damages();
            DamageArticleView = new DamageArticleAssignmentView();
            //GetArticleDamageList();
        }

        public DamageArticleAssignmentViewData(int myDamageArticleAssignmentId, int myUserId) : this()
        {
            _GL_User = new Globals._GL_USER();
            _GL_User.User_ID = myUserId;

            DamageArticleView.Id = myDamageArticleAssignmentId;

            if (DamageArticleView.Id > 0)
            {
                FillById();
                Article = DamageArticleView.Article.Copy();
            }
        }
        public DamageArticleAssignmentViewData(int myArticleId, int myUserId, bool myLoadDamageArticleList) : this()
        {
            _GL_User = new Globals._GL_USER();
            _GL_User.User_ID = myUserId;

            Article.Id = myArticleId;
            if (Article.Id > 0)
            {
                Article = new Articles();
                Article.Id = myArticleId;
                GetArticleDamageList();
            }
        }
        public DamageArticleAssignmentViewData(Damages myDamage, Articles myArticle, int myUserId, bool myLoadDamageArticleList) : this()
        {
            _GL_User = new Globals._GL_USER();
            _GL_User.User_ID = myUserId;

            Article = myArticle.Copy();
            Damage = myDamage.Copy();

            if ((Article.Id > 0) && (myLoadDamageArticleList))
            {
                GetArticleDamageList();
            }
        }

        public DamageArticleAssignmentViewData(DamageArticleAssignmentView myDamageArticleView, Articles myArticle, int myUserId, bool myLoadDamageArticleList) : this()
        {
            _GL_User = new Globals._GL_USER();
            _GL_User.User_ID = myUserId;

            DamageArticleView = myDamageArticleView.Copy();
            Article = myArticle.Copy();

            if ((Article.Id > 0) && (myLoadDamageArticleList))
            {
                GetArticleDamageList();
            }
        }

        public DamageArticleAssignmentViewData(int myId) : this()
        {
            DamageArticleView.Id = myId;
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
                strSql = strSql + " INSERT INTO SchadenZuweisung (ArtikelID, SchadenID, UserID, Datum) " +
                                        "VALUES (" + Article.Id +
                                                 "," + Damage.Id +
                                                 "," + BenutzerID +
                                                 ",'" + DateTime.Now + "'" +
                                                 "); ";
                return strSql;
            }
        }
        public string sql_Delete
        {
            get
            {
                string strSql = "DELETE FROM SchadenZuweisung WHERE Id=" + DamageArticleView.Id;
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
                return strSql;
            }
        }
        public string sql_Main
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Select " +
                                " a.ID" +
                                ", a.ArtikelID" +
                                ", a.SchadenID" +
                                ", a.UserID" +
                                ", a.Datum " +
                                " FROM SchadenZuweisung a ";
                return strSql;
            }
        }

        public string sql_GetListByArticleId
        {
            get
            {
                string strSql = string.Empty;
                strSql = sql_Main + "WHERE a.ArtikelID= " + Article.Id;
                return strSql;
            }
        }
        public string sql_Get
        {
            get
            {
                string strSql = string.Empty;
                strSql = sql_Main + "WHERE a.Id= " + DamageArticleView.Id;
                return strSql;
            }
        }

        ///-----------------------------------------------------------------------------------------------------
        ///                             Function / Procedure
        ///-----------------------------------------------------------------------------------------------------
        public void FillById()
        {
            string strSql = string.Empty;
            strSql = this.sql_Get;
            System.Data.DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "Get", "DamagesArticle", BenutzerID);
            SetValue(dt);
        }

        public void FillByArticleId()
        {
            string strSql = string.Empty;
            strSql = this.sql_GetListByArticleId;
            System.Data.DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "GetList", "DamagesArticles", BenutzerID);
            SetValue(dt);
        }
        private void SetValue(System.Data.DataTable dt)
        {
            foreach (DataRow r in dt.Rows)
            {
                SetValue(r);
            }
        }
        private void SetValue(DataRow myRow)
        {
            DamageArticleView = new DamageArticleAssignmentView();

            int iTmp = 0;
            int.TryParse(myRow["ID"].ToString(), out iTmp);
            this.DamageArticleView.Id = iTmp;

            iTmp = 0;
            int.TryParse(myRow["ArtikelID"].ToString(), out iTmp);
            this.DamageArticleView.ArticleId = iTmp;

            iTmp = 0;
            int.TryParse(myRow["SchadenID"].ToString(), out iTmp);
            this.DamageArticleView.DamageId = iTmp;

            iTmp = 0;
            int.TryParse(myRow["UserID"].ToString(), out iTmp);
            this.DamageArticleView.UserId = iTmp;

            DateTime dtTmp = new DateTime(1900, 1, 1);
            DateTime.TryParse(myRow["Datum"].ToString(), out dtTmp);
            this.DamageArticleView.Datum = dtTmp;

            if (this.DamageArticleView.ArticleId > 0)
            {
                artViewData = new ArticleViewData(this.DamageArticleView.ArticleId, (int)BenutzerID);
                Article = artViewData.Artikel.Copy();
                DamageArticleView.Article = artViewData.Artikel.Copy();
            }

            if (this.DamageArticleView.DamageId > 0)
            {
                damageViewData = new DamageViewData(this.DamageArticleView.DamageId, (int)BenutzerID);
                Damage = damageViewData.Damage.Copy();
                DamageArticleView.Damage = damageViewData.Damage.Copy();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteItem()
        {
            bool bReturn = false;
            string strSql = string.Empty;
            strSql = this.sql_Delete;

            bReturn = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "DeleteAssignment", BenutzerID);
            if (bReturn)
            {
                EingangViewData eVD = new EingangViewData(Article.LEingangTableID, (int)BenutzerID, false);
                clsArtikelVita.ArtikelDelSchadenByScan(BenutzerID, Article.Id, eVD.Eingang.LEingangID, Article.LVS_ID, Damage.Descrition);
                GetArticleDamageList();
            }
            return bReturn;
        }

        public void GetArticleDamageList()
        {
            string strSql = string.Empty;
            ArticleDamagesList = new List<DamageArticleAssignmentView>();
            strSql = this.sql_GetListByArticleId;

            System.Data.DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "GetArticleDamageList", "DamagesArticleAssignment", BenutzerID);
            foreach (DataRow r in dt.Rows)
            {
                SetValue(r);
                if ((DamageArticleView.Id > 0) && (DamageArticleView.ArticleId.Equals(Article.Id)))
                {
                    ArticleDamagesList.Add(DamageArticleView);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Update()
        {
            string strSql = string.Empty;
            strSql = this.sql_Update;
            var result = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "Update", BenutzerID);
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myDamageIds"></param>
        /// <param name="myArticle"></param>
        /// <returns></returns>
        public bool AddDamageAssignmentToArticle()
        {
            bool bReturn = false;
            string strSql = string.Empty;
            strSql = this.sql_Add;

            bReturn = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "AddZuweisung", BenutzerID);
            if (bReturn)
            {
                EingangViewData eVD = new EingangViewData(Article.LEingangTableID, (int)BenutzerID, false);
                clsArtikelVita.ArtikelAddSchadenByScan(BenutzerID, Article.Id, eVD.Eingang.LEingangID, Article.LVS_ID, Damage.Descrition);
                GetArticleDamageList();
            }
            return bReturn;
        }
    }
}

