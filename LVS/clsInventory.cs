using System;
using System.Collections.Generic;
using System.Data;

namespace LVS
{
    public class clsInventory
    {
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


        private int _Id;
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private enumInventoryArt _Art;
        public enumInventoryArt Art
        {
            get { return _Art; }
            set { _Art = value; }
        }


        private int _UserId;
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        private enumInventoryStatus _Status;
        public enumInventoryStatus Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public int ArbeitsbereichId { get; set; }
        public DateTime Created { get; set; }
        public DateTime CloseDate { get; set; }

        private int _CloseUserId;
        public int CloseUserId
        {
            get { return _CloseUserId; }
            set { _CloseUserId = value; }
        }

        public clsInventoryArticle InventoryArticle = new clsInventoryArticle();

        public List<int> ListArticleAddToInventory = new List<int>();


        public string sqlAdd
        {
            get
            {
                string strSql = string.Empty;
                strSql = "INSERT INTO Inventories (Name, Description, Art, Created, UserId, ArbeitsbereichId, [Status], CloseDate, CloseUserId) " +
                                                "VALUES (" +
                                                       " '" + Name + "' " +
                                                       ", '" + Description + "' " +
                                                       ", " + (int)Art + " " +
                                                       ", '" + Created + "' " +
                                                       ", " + UserId + " " +
                                                       ", " + ArbeitsbereichId + " " +
                                                       ", " + (int)Status + 
                                                       ", '" + CloseDate + "' " +
                                                       ", " + CloseUserId + " " +
                                                       "); ";
                return strSql;
            }
        }
        public string sqlUpdate
        {
            get
            {
                string strSql = "Update Inventories SET" +
                                        " Name = '" + Name + "' " +
                                        ", [Description] = '" + Description + "' " +
                                        ", Art = " + (int)Art +
                                        ", Created = '" + Created + "' " +
                                        ", UserId = " + UserId +
                                        ", ArbeitsbereichId = " + ArbeitsbereichId +
                                        ", [Status] = "  + (int)Status +
                                        ", CloseDate = '" + CloseDate + "' " +
                                        ", CloseUserId = " + CloseUserId +

                                        " WHERE Id = " + Id + " ;";
                return strSql;
            }
        }
        public string sqlDelete
        {
            get
            {
                string strSql = "DELETE Inventories WHERE Id = " + Id + " ;";
                return strSql;
            }
        }

        public string sqlGet
        {
            get
            {
                string strSql = "Select * FROM Inventories WHERE Id=" + Id + " ;";
                return strSql;
            }
        }

        public string sqlGetAll
        {
            get
            {
                string strSql = "Select * FROM Inventories WHERE Id=" + Id + " ;";
                return strSql;
            }
        }

        public string sqlGetList
        {
            get
            {
                string strSql = "Select * FROM Inventories;";
                return strSql;
            }
        }
        /**********************************************************************************************************************
        *                                               Methoden
        *********************************************************************************************************************/

        public clsInventory()
        {
            InitCls();
        }

        public clsInventory(int myId)
        {
            InitCls();
            this.Id = myId;
            if (myId > 0)
            {
                Fill();
            }
        }

        private void InitCls()
        {
            this.Id = 0;
            this.Name = string.Empty;
            this.Description = string.Empty;
            this.Art = enumInventoryArt.NotSet;
            this.UserId = 0;
            this.Created = DateTime.Now;
            this.ArbeitsbereichId = 0;
            this.Status = enumInventoryStatus.NotSet;
            this.CloseDate = new DateTime(1900, 1, 1);
            this.CloseUserId = 0;

            InventoryArticle = new clsInventoryArticle();
            ListArticleAddToInventory = new List<int>();
        }


        ///<summary>clsInventory / Add</summary>
        ///<remarks></remarks>
        public void Add()
        {
            string strSql = sqlAdd;
            strSql = strSql + "Select @@IDENTITY as 'ID';";

            string strTmp = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSql, "InsertInventory", BenutzerID);
            int.TryParse(strTmp, out int iTmp);
            Id = iTmp;
        }
        /// <summary>
        /// 
        /// </summary>
        public void AddAll()
        {
            if (ListArticleAddToInventory.Count > 0)
            {
                if (this.InventoryArticle is clsInventoryArticle)
                {
                    this.Add();
                    if (this.Id > 0)
                    {
                        string strSql = string.Empty;
                        foreach (int item in ListArticleAddToInventory)
                        {
                            this.InventoryArticle = new clsInventoryArticle();
                            this.InventoryArticle.InventoryId = this.Id;
                            this.InventoryArticle.Description = string.Empty;
                            this.InventoryArticle.ArtikelId = item;
                            this.InventoryArticle.Status = enumInventoryArticleStatus.Neu;
                            this.InventoryArticle.Text = string.Empty;
                            this.InventoryArticle.Created = DateTime.Now;

                            strSql += this.InventoryArticle.sqlAdd;
                        }
                        clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "InsertInventory", BenutzerID);
                        //Add Logbucheintrag Eintrag
                        string strInfo = "Neue Inventur erstellt erstellt: ID [" + Id.ToString() + "] / Name: " + Name;
                        Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Eintrag.ToString(), strInfo);
                    }
                }
            }
        }
        ///<summary>clsInventory / Update</summary>
        ///<remarks>Update eines Datensatzes in der DB über die ID.</remarks>
        public void Update()
        {
            string strSql = sqlUpdate;
            bool bReturn = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            // Logbucheintrag Eintrag
            string strInfo = "Inventur geändert: Id: " + Id.ToString() + " / Name: " + Name;
            Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), strInfo);
        }

        ///<summary>clsInventory / Delete</summary>
        ///<remarks></remarks>
        public void Delete()
        {
            string strSql = sqlDelete;
            if (InventoryArticle is clsInventoryArticle)
            {
                strSql += InventoryArticle.sqlDeleteByInventoryId;
            }

            if (clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "InventoryDelete", this._GL_User.User_ID))
            {
                //Add Logbucheintrag 
                string myBeschreibung = "Inventur gelöscht: ID: " + Id.ToString() + " / Name: " + Name;
                Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);
            }
        }
        ///<summary>clsInventory / Fill</summary>
        ///<remarks>Ermittel die Daten anhand der TableID.</remarks>
        public void Fill()
        {
            DataTable dt = new DataTable();
            string strSql = sqlGet;

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Inventory");
            if (dt.Rows.Count > 0)
            {
                SetValue(dt);
                this.InventoryArticle = new clsInventoryArticle(0, this.Id);
            }
        }
        ///<summary>clsInventory / FillAll</summary>
        ///<remarks>Ermittel die Daten anhand der TableID.</remarks>
        public void FillAll()
        {
            DataTable dt = new DataTable();
            string strSql = sqlGet;

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Inventory");
            if (dt.Rows.Count > 0)
            {
                SetValue(dt);
                this.InventoryArticle = new clsInventoryArticle(0, this.Id);
            }
        }
        ///<summary>clsInventory / SetValue</summary>
        /// <param name="dt"></param>
        private void SetValue(DataTable dt)
        {
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                this.Id = (int)dt.Rows[i]["Id"];
                this.Name = dt.Rows[i]["Name"].ToString();
                this.Description = dt.Rows[i]["Description"].ToString();
                Enum.TryParse(dt.Rows[i]["Art"].ToString(), out enumInventoryArt enumTmp);
                this.Art = enumTmp;
                this.Created = (DateTime)dt.Rows[i]["Created"];
                this.UserId = (int)dt.Rows[i]["UserId"];
                this.ArbeitsbereichId = (int)dt.Rows[i]["ArbeitsbereichId"];
                Enum.TryParse(dt.Rows[i]["Status"].ToString(), out enumInventoryStatus enumTmpStatus);
                this.Status = enumTmpStatus;
                this.CloseDate = (DateTime)dt.Rows[i]["CloseDate"];
                this.CloseUserId = (int)dt.Rows[i]["CloseUserId"];
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataTable GetInventoryList()
        {
            clsInventory inv = new clsInventory();
            string strSql = inv.sqlGetList;
            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "InventoryList", "InventoryList", 0);
            return dt;
        }
    }
}
