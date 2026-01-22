using Common.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace LVS.ViewData
{
    public class DamageViewData
    {
        public Damages Damage { get; set; }
        public Articles Article { get; set; }

        public DamageArticleAssignmentViewData daaViewData { get; set; }
        public ArticleViewData artViewData { get; set; }
        public List<Damages> DamagesList { get; set; } = new List<Damages>();
        //public List<Damages> ArticleDamagesList { get; set; } = new List<Damages>();

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

        public DamageViewData()
        {
            Damage = new Damages();

        }
        public DamageViewData(int myUserId) : this()
        {
            _GL_User = new Globals._GL_USER();
            _GL_User.User_ID = myUserId;
        }
        public DamageViewData(int myDamageId, int myUserId) : this()
        {
            _GL_User = new Globals._GL_USER();
            _GL_User.User_ID = myUserId;
            if (myDamageId > 0)
            {
                Damage.Id = myDamageId;
                Fill();
            }
        }
        public DamageViewData(int myDamageId, int myArticleId, int myUserId) : this()
        {
            _GL_User = new Globals._GL_USER();
            _GL_User.User_ID = myUserId;
            if (myDamageId > 0)
            {
                Damage.Id = myDamageId;
                Fill();
            }
            if (myArticleId > 0)
            {
                Article = new Articles();
                Article.Id = myArticleId;
                GetArticleDamageList();
            }
        }

        public DamageViewData(int myUserId, Damages myDamage) : this()
        {
            _GL_User = new Globals._GL_USER();
            _GL_User.User_ID = myUserId;
            Damage = myDamage.Copy();
        }
        ///<summary>UsersViewModel / Add</summary>
        ///<remarks></remarks>
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
                strSql += " Bezeichnung ='" + Damage.Designation + "'";
                strSql += ", Beschreibung ='" + Damage.Descrition + "'";
                strSql += ", aktiv =" + Convert.ToInt32(Damage.IsActiv);
                strSql += ", Art =" + Convert.ToInt32(Damage.Art);
                strSql += ", Code ='" + Damage.Code + "'";
                strSql += ", AutoSPL =" + Convert.ToInt32(Damage.AutoSPL);
                strSql += " WHERE ID=" + Damage.Id;
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
                                ", a.Bezeichnung" +
                                ", a.Beschreibung" +
                                ", a.aktiv" +
                                ", a.Art " +
                                ", CASE " +
                                    "WHEN a.Art = 0 THEN 'kritischer Schaden'" +
                                    "WHEN a.Art = 1 THEN 'leichte Beschädigung/Mangel'" +
                                    "ELSE '' " +
                                    "END as Schadensart " +
                                ", a.Code" +
                                ", a.AutoSPL" +
                                " FROM Schaeden a ";
                return strSql;
            }
        }

        public string sql_GetDamage
        {
            get
            {
                string strSql = string.Empty;
                strSql = sql_Main + "WHERE a.ID= " + Damage.Id;
                return strSql;
            }
        }
        public string sql_DamageList
        {
            get
            {
                string strSql = sql_Main + "WHERE a.aktiv=1 Order by Bezeichnung ";
                return strSql;
            }
        }

        ///-----------------------------------------------------------------------------------------------------
        ///                             Function / Procedure
        ///-----------------------------------------------------------------------------------------------------


        public void Fill()
        {
            string strSql = string.Empty;
            strSql = this.sql_GetDamage;
            System.Data.DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "GetList", "Damages", BenutzerID);
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
            Damage = new Damages();

            int iTmp = 0;
            int.TryParse(myRow["ID"].ToString(), out iTmp);
            this.Damage.Id = iTmp;
            this.Damage.Designation = myRow["Bezeichnung"].ToString();
            this.Damage.Descrition = myRow["Beschreibung"].ToString();
            this.Damage.IsActiv = (bool)myRow["aktiv"];
            iTmp = 0;
            int.TryParse(myRow["Art"].ToString(), out iTmp);
            this.Damage.Art = iTmp;
            this.Damage.ArtString = myRow["Schadensart"].ToString();
            iTmp = 0;
            int.TryParse(myRow["Code"].ToString(), out iTmp);
            this.Damage.Code = iTmp;
            this.Damage.AutoSPL = (bool)myRow["AutoSPL"];
        }
        /// <summary>
        /// 
        /// </summary>
        public void GetList()
        {
            string strSql = string.Empty;
            DamagesList = new List<Damages>();
            strSql = this.sql_DamageList;
            System.Data.DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "GetList", "Damages", BenutzerID);
            foreach (DataRow r in dt.Rows)
            {
                SetValue(r);
                if (Damage.Id > 0)
                {
                    DamagesList.Add(Damage);
                }
            }
        }

        public void GetArticleDamageList()
        {
            daaViewData = new DamageArticleAssignmentViewData(Article.Id, (int)BenutzerID, true);
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

    }
}
