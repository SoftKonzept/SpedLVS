using System;
using System.Data;

namespace LVS
{
    public class clsInventoryArticle
    {

        private int _Id;
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }


        private int _InventoryId;
        public int InventoryId
        {
            get { return _InventoryId; }
            set { _InventoryId = value; }
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private int _ArtikelId;
        public int ArtikelId
        {
            get { return _ArtikelId; }
            set { _ArtikelId = value; }
        }


        private enumInventoryArticleStatus _Status;
        public enumInventoryArticleStatus Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        private string _Text;
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }
        public DateTime Created { get; set; }
        public DateTime Scanned { get; set; }
        public int ScannedUserId { get; set; }

        public clsArtikel Artikel { get; set; }

        public DataTable dtInventoryArticleList { get; set; }
        public string sqlAdd
        {
            get
            {
                string strSql = string.Empty;
                strSql = "INSERT INTO InventoryArticle (InventoryId, Description, ArtikelId, [Status], Text, Created, Scanned,ScannedUserId) " +
                                                "VALUES (" +
                                                       " " + InventoryId + " " +
                                                       ", '" + Description + "' " +
                                                       ", " + ArtikelId + " " +
                                                       ", " + (int)Status + " " +
                                                       ", '" + Text + "' " +
                                                       ", '" + Created + "' " +
                                                       ", '" + Scanned + "' " +
                                                        ", " + ScannedUserId +
                                                       "); ";
                return strSql;
            }
        }
        public string sqlUpdate
        {
            get
            {
                string strSql = "Update InventoryArticle SET" +
                                        " InventoryId = " + InventoryId +
                                        ", Description = '" + Description + "' " +
                                        ", ArtikelId = " + ArtikelId +
                                        ", [Status] = " + (int)Status +
                                        ", Text = '" + Text + "' " +
                                        ", Created = '" + Created + "'" +
                                        ", Scanned = '" + Scanned + "'" +
                                        ", ScannedUserId = " + ScannedUserId +
                                        " WHERE Id = " + Id + "; ";
                return strSql;
            }
        }
        public string sqlDelete
        {
            get
            {
                string strSql = "DELETE InventoryArticle WHERE Id = " + Id + "; ";
                return strSql;
            }
        }
        public string sqlDeleteByInventoryId
        {
            get
            {
                string strSql = "DELETE InventoryArticle WHERE InventoryId = " + InventoryId + "; ";
                return strSql;
            }
        }

        public string sqlGet
        {
            get
            {
                string strSql = "Select * FROM InventoryArticle WHERE Id=" + Id + "; ";
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
                                                        "InventoryId=" + InventoryId + "; ";
                return strSql;
            }
        }
        /**********************************************************************************************************************
        *                                               Methoden
        *********************************************************************************************************************/

        public clsInventoryArticle()
        {
            InitCls();
        }
        public clsInventoryArticle(int myId, int myInventoryId)
        {
            InitCls();
            this.Id = myId;
            this.InventoryId = myInventoryId;
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
            this.Id = 0;
            this.InventoryId = 0;
            this.Description = string.Empty;
            this.ArtikelId = 0;
            this.Status = enumInventoryArticleStatus.NotSet;
            this.Text = string.Empty;
            this.Created = DateTime.Now;
            this.Scanned = new DateTime(1900, 1, 1);
            this.ScannedUserId = 0;

            dtInventoryArticleList = new DataTable();
        }
        ///<summary>clsInventoryArticle / Add</summary>
        ///<remarks></remarks>
        public void Add()
        {
            string strSql = sqlAdd;
            strSql = strSql + "Select @@IDENTITY as 'ID' ";

            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, 0);
            int.TryParse(strTmp, out int iTmp);
            Id = iTmp;

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
                    this.Id = (int)dt.Rows[i]["Id"];
                    this.InventoryId = (int)dt.Rows[i]["InventoryId"];
                    this.Description = dt.Rows[i]["Description"].ToString();
                    this.ArtikelId = (int)dt.Rows[i]["ArtikelId"];
                    int iTmp = (int)dt.Rows[i]["Status"];
                    Enum.TryParse(iTmp.ToString(), out enumInventoryArticleStatus enumTmp);
                    this.Status = enumTmp;
                    this.Text = dt.Rows[i]["Text"].ToString();
                    this.Created = (DateTime)dt.Rows[i]["Created"];

                    DateTime dtTmp = new DateTime(1900, 1, 1);
                    if (dt.Rows[i]["Scanned"] != null)
                    {
                        DateTime.TryParse(dt.Rows[i]["Scanned"].ToString(), out dtTmp);
                    }
                    this.Scanned = dtTmp;
                    this.ScannedUserId = (int)dt.Rows[i]["ScannedUserId"];

                    if (this.ArtikelId > 0)
                    { 
                        this.Artikel = new clsArtikel();
                        //Artikel.InitClass(this._GL_User, this._GL_System);
                        Artikel.ID = this.ArtikelId;
                        Artikel.GetArtikeldatenByTableID();

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
    }
}
