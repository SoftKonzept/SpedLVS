using Common.Enumerations;
using Common.Models;
using Common.SqlStatementCreater;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class SperrlagerViewData
    {
        private int BenutzerID { get; set; } = 0;
        public Sperrlager Spl { get; set; }
        public Articles Article { get; set; }
        public List<Sperrlager> ListSPL { get; set; } = new List<Sperrlager>();
        public List<Sperrlager> ListArticleInSPL { get; set; } = new List<Sperrlager>();


        public Globals._GL_SYSTEM GLSystem { get; set; }
        public Globals._GL_USER GL_USER { get; set; }
        public clsSystem System { get; set; }

        public string LogText { get; set; }
        public List<string> ListLogText { get; set; }

        public SperrlagerViewData()
        {
            InitCls();
        }
        public SperrlagerViewData(int myUserId) : this()
        {
            BenutzerID = myUserId;
        }
        public SperrlagerViewData(Sperrlager mySpl, int myUserId) : this()
        {
            Spl = mySpl.Copy();
            BenutzerID = myUserId;
        }
        //public SpeerlagerViewData(Globals._GL_SYSTEM myGLSystem, Globals._GL_USER myGLUser, clsSystem mySystem) : this()
        //{
        //    GLSystem = myGLSystem;
        //    GL_USER = myGLUser;
        //    System = mySystem;

        //    BenutzerID = (int)GL_USER.User_ID;
        //}
        public SperrlagerViewData(int myId, int myUserId) : this()
        {
            Spl.Id = myId;
            BenutzerID = myUserId;
            if (Spl.Id > 0)
            {
                Fill();
            }
        }
        public SperrlagerViewData(Articles myArticle, int myUserId) : this()
        {
            Spl.Id = 0;
            BenutzerID = myUserId;
            Article = myArticle;
            if ((Article != null) && (Article.Id > 0))
            {
                Spl.ArtikelID = Article.Id;
                FillLastINByArtikelID();
            }
        }
        private void InitCls()
        {
            Spl = new Sperrlager();
        }
        public void Fill()
        {
            //string strSQL = sql_Get;
            sqlCreater_Spl sql = new sqlCreater_Spl(Spl);
            string strSQL = sql.sql_GetItem();// sql_Get;
            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "SPL", "SPL", BenutzerID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                }
            }
        }
        private void FillLastINByArtikelID()
        {
            sqlCreater_Spl sql = new sqlCreater_Spl(Spl, Article);
            string strSQL = sql.sql_FillLastINByArtikelId();
            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "SPL", "SPL", BenutzerID);
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
            Spl = new Sperrlager();
            Int32 iTmp = 0;
            Int32.TryParse(row["ID"].ToString(), out iTmp);
            Spl.Id = iTmp;
            iTmp = 0;
            Int32.TryParse(row["ArtikelID"].ToString(), out iTmp);
            Spl.ArtikelID = iTmp;
            iTmp = 0;
            Int32.TryParse(row["UserID"].ToString(), out iTmp);
            Spl.UserID = iTmp;
            Spl.Datum = (DateTime)row["Datum"];
            Spl.BKZ = row["BKZ"].ToString();
            iTmp = 0;
            Int32.TryParse(row["SPLIDIn"].ToString(), out iTmp);
            Spl.SPLIDIn = iTmp;
            iTmp = 0;
            Int32.TryParse(row["DefWindungen"].ToString(), out iTmp);
            Spl.DefWindungen = iTmp;
            Spl.Sperrgrund = row["Sperrgrund"].ToString();
            Spl.Vermerk = row["Vermerk"].ToString();
            Spl.IsCustomCertificateMissing = (bool)row["IsCustomCertificateMissing"];
        }

        public void GetSPLArticleINCertificateMissing()
        {
            ListArticleInSPL = new List<Sperrlager>();
            string strSQL = string.Empty;
            sqlCreater_Spl sqlSpl = new sqlCreater_Spl();
            strSQL = sqlSpl.sql_GetArticlesInSPLMissingCertificates();
            DataTable dt = new DataTable("SPL");
            dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "SPL", "SPLIn", BenutzerID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                    var tmp = ListArticleInSPL.Where(x => x.ArtikelID == Spl.ArtikelID && x.Sperrgrund.Equals(Spl.Sperrgrund) && x.IsCustomCertificateMissing == Spl.IsCustomCertificateMissing);
                    if (ListArticleInSPL.Where(x => x.ArtikelID == Spl.ArtikelID && x.Sperrgrund.Equals(Spl.Sperrgrund) && x.IsCustomCertificateMissing == Spl.IsCustomCertificateMissing).ToList().Count == 0)
                    {
                        ListArticleInSPL.Add(Spl);
                    }
                    else
                    {
                        SperrlagerViewData splVD = new SperrlagerViewData(Spl, 1);
                        splVD.Delete();
                        Task.Delay(100);
                    }
                }
            }

        }
        ///
        /// 
        ///
        public void CheckForDoubleBookingRecord()
        {
            bool reVal = false;
            sqlCreater_Spl sql = new sqlCreater_Spl(Spl);
            string strSql = sql.sql_DeleteDoubleBookingRecord();
            if (!strSql.Equals(string.Empty))
            {
                reVal = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "CheckForDoubleBookingRecord", BenutzerID);
            }
        }
        public void Delete()
        {
            bool reVal = false;
            sqlCreater_Spl sql = new sqlCreater_Spl(Spl);
            string strSql = sql.sql_Delete();
            if (!strSql.Equals(string.Empty))
            {
                reVal = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "Delete", BenutzerID);
            }
        }
        /// <summary>
        ///             ADD
        /// </summary>
        public void Add(bool myEinbuchung)
        {
            sqlCreater_Spl sql = new sqlCreater_Spl(Spl);
            string strSql = sql.AddStrSQL(myEinbuchung);
            if (!strSql.Equals(string.Empty))
            {
                string tmpBeschreibung = string.Empty;
                string tmpAktion = string.Empty;
                string tmpTableName = string.Empty;
                //---- Eintrag ArtikelVita
                if (myEinbuchung)
                {
                    tmpBeschreibung = "Sperrlager IN: ArtikelID [" + Article.Id.ToString() + "] / LVSNr [" + Article.LVS_ID.ToString() + "]";
                    tmpAktion = enumLagerAktionen.SperrlagerIN.ToString();
                    tmpTableName = enumDatabaseSped4_TableNames.Artikel.ToString();
                }
                else
                {
                    tmpBeschreibung = "Sperrlager OUT: ArtikelID [" + Article.Id.ToString() + "] / LVSNr [" + Article.LVS_ID.ToString() + "]";
                    tmpAktion = enumLagerAktionen.SperrlagerOUT.ToString();
                    tmpTableName = enumDatabaseSped4_TableNames.Artikel.ToString();

                }
                strSql += " INSERT INTO ArtikelVita (TableID, TableName, Aktion, Datum, UserID, Beschreibung) " +
                                                    "VALUES (" + Article.Id +
                                                            ",'" + tmpTableName + "'" +
                                                            ",'" + tmpAktion + "'" +
                                                            ",'" + DateTime.Now + "'" +
                                                            ",'" + BenutzerID + "'" +
                                                            ",'" + tmpBeschreibung + "'" +
                                                            "); ";


                string strTmp = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSql, "Insert", BenutzerID);
                int.TryParse(strTmp, out int iTmp);
                Spl.Id = iTmp;
            }
        }

        /// <summary>
        ///             ADD
        /// </summary>
        public bool ExistSPLID()
        {
            bool reVal = false;
            sqlCreater_Spl sql = new sqlCreater_Spl(Spl);
            string strSql = sql.sql_ExistSPLID();
            if (strSql.Equals(string.Empty))
            {
                reVal = clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            }
            return reVal;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CheckArtikelInSPL()
        {
            bool reVal = false;
            sqlCreater_Spl sql = new sqlCreater_Spl(Spl);
            string strSql = sql.sql_CheckArtikelInSPL();
            reVal = clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            return reVal;
        }



        //===========================================================================================================
        public static bool BookingArticleOutSpl(int myArticleID, int myBenutzerId)
        {
            bool bReturn = false;
            if (myArticleID > 0)
            {
                Articles art = new Articles();
                art.Id = myArticleID;
                SperrlagerViewData splVd = new SperrlagerViewData(art, myBenutzerId);
                if ((splVd.Spl.Id > 0) && (splVd.CheckArtikelInSPL()))
                {
                    splVd.Spl.SPLIDIn = splVd.Spl.Id;
                    splVd.Spl.UserID = myBenutzerId;
                    splVd.Spl.Id = 0;
                    splVd.Add(false); // false = OUT
                    bReturn = (splVd.Spl.Id > 0);
                }
            }
            return bReturn;
        }

    }
}

