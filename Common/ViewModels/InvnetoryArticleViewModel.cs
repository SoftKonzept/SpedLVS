using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using Common.Enumerations;
using Common;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Common.ViewModels
{
    public class InvnetoryArticleViewModel
    {
        public InventoryArticles InventoryArticle { get; set; }
        public DataTable dtInventoryArticleList { get; set; }

        //public Globals._GL_USER _GL_User;
        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                //_BenutzerID = _GL_User.User_ID;
                return _BenutzerID;
            }
            set { _BenutzerID = value; }
        }

        public InvnetoryArticleViewModel()
        {
            InitCls();
        }
        public InvnetoryArticleViewModel(int myId, int myInventoryId)
        {
            InitCls();
            InventoryArticle.Id = myId;
            InventoryArticle.InventoryId = myInventoryId;
            if (myId > 0)
            {
                Fill();
                GetInventoryArticleList();
            }
            else if (myInventoryId > 0)
            {
                GetInventoryArticleList();
            }
        }

        private void InitCls()
        {
            this.InventoryArticle = new InventoryArticles();

            this.InventoryArticle.Id = 0;
            this.InventoryArticle.InventoryId = 0;
            this.InventoryArticle.Description = string.Empty;
            this.InventoryArticle.ArtikelId = 0;
            this.InventoryArticle.Status = enumInventoryArticleStatus.NotSet;
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
        public void Update()
        {
            string strSql = sqlUpdate;
            bool bReturn = clsSQLcon.ExecuteSQL(strSql, 0);
            // Logbucheintrag Eintrag
            //string strInfo = "Inventur geändert: Id: " + Id.ToString() + " / Name: " + Name;
            //Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), strInfo);
        }

        ///<summary>clsInventory / Delete</summary>
        ///<remarks></remarks>
        public void Delete()
        {
            string strSql = sqlDelete;
            if (clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "InventoryDelete", 0))
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
            DataTable dt = new DataTable();
            string strSql = sqlGet;

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, 0, "InventoryArticle");
            if (dt.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    InventoryArticle.Id = (int)dt.Rows[i]["Id"];
                    InventoryArticle.InventoryId = (int)dt.Rows[i]["InventoryId"];
                    InventoryArticle.Description = dt.Rows[i]["Description"].ToString();
                    InventoryArticle.ArtikelId = (int)dt.Rows[i]["ArtikelId"];
                    int iTmp = (int)dt.Rows[i]["Status"];
                    Enum.TryParse(iTmp.ToString(), out enumInventoryArticleStatus enumTmp);
                    InventoryArticle.Status = enumTmp;
                    InventoryArticle.Text = dt.Rows[i]["Text"].ToString();
                    InventoryArticle.Created = (DateTime)dt.Rows[i]["Created"];

                    DateTime dtTmp = new DateTime(1900, 1, 1);
                    if (dt.Rows[i]["Scanned"] != null)
                    {
                        DateTime.TryParse(dt.Rows[i]["Scanned"].ToString(), out dtTmp);
                    }
                    InventoryArticle.Scanned = dtTmp;
                    InventoryArticle.ScannedUserId = (int)dt.Rows[i]["ScannedUserId"];

                    if (InventoryArticle.ArtikelId > 0)
                    {
                        InventoryArticle.Artikel = new clsArtikel();
                        //Artikel.InitClass(this._GL_User, this._GL_System);
                        InventoryArticle.Artikel.ID = InventoryArticle.ArtikelId;
                        InventoryArticle.Artikel.GetArtikeldatenByTableID();

                    }
                }
            }
        }

        public void GetInventoryArticleList()
        {
            DataTable dt = new DataTable();
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
        public string sqlUpdate
        {
            get
            {
                string strSql = "Update InventoryArticle SET" +
                                        " InventoryId = " + InventoryArticle.InventoryId +
                                        ", Description = '" + InventoryArticle.Description + "' " +
                                        ", ArtikelId = " + InventoryArticle.ArtikelId +
                                        ", [Status] = " + (int)InventoryArticle.Status +
                                        ", Text = '" + InventoryArticle.Text + "' " +
                                        ", Created = '" + InventoryArticle.Created + "'" +
                                        ", Scanned = '" + InventoryArticle.Scanned + "'" +
                                        ", ScannedUserId = " + InventoryArticle.ScannedUserId +
                                        " WHERE Id = " + InventoryArticle.Id + "; ";
                return strSql;
            }
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

        public string sqlGet
        {
            get
            {
                string strSql = "Select * FROM InventoryArticle WHERE Id=" + InventoryArticle.Id + "; ";
                return strSql;
            }
        }
        public string sqlGetInventoryArticleList
        {
            get
            {
                string strSql = "Select i.* " +
                                        ", a.LVS_Id as LVSNr" +
                                        ", a.Produktionsnummer" +
                                        ", a.Werksnummer" +
                                            " FROM InventoryArticle i " +
                                                "inner join Artikel a on a.Id = i.ArtikelId " +
                                                " WHERE " +
                                                        "InventoryId=" + InventoryArticle.InventoryId + "; ";
                return strSql;
            }
        }
    }
}
