using Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LVS.ViewData
{
    public class InventoryViewData
    {
        public List<int> ListArticleAddToInventory = new List<int>();
        public List<Common.Models.Inventories> ListInventories = new List<Common.Models.Inventories>();
        public DataTable dtInventories = new DataTable();

        public Inventories Inventory { get; set; }
        public InvnetoryArticleViewData InventoryArticleViewData { get; set; }
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

        public InventoryViewData(bool getListAndTable)
        {
            InventoryArticleViewData = new InvnetoryArticleViewData();
            InitCls();
            GetInventoryDatatable();
            if (getListAndTable)
            {
                ListInventories = GetInventoriesList().ToList();
            }
        }

        public InventoryViewData(int myInventoryId, int myInventoryArticleId)
        {
            InventoryArticleViewData = new InvnetoryArticleViewData();
            InitCls();
            GetInventoryDatatable();
            //ListInventories = GetInventoriesList().ToList();

            Inventory.Id = myInventoryId;
            if (Inventory.Id > 0)
            {
                Fill();
            }
            if (myInventoryArticleId > 0)
            {
                InventoryArticleViewData = new InvnetoryArticleViewData(myInventoryId, myInventoryArticleId, true);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitCls()
        {
            Inventory = new Inventories();
            this.Inventory.Id = 0;
            this.Inventory.Name = string.Empty;
            this.Inventory.Description = string.Empty;
            this.Inventory.Art = Common.Enumerations.enumInventoryArt.NotSet;
            this.Inventory.UserId = 0;
            this.Inventory.Created = DateTime.Now;
            this.Inventory.ArbeitsbereichId = 0;
            this.Inventory.Status = Common.Enumerations.enumInventoryStatus.NotSet;
            this.Inventory.CloseDate = new DateTime(1900, 1, 1);
            this.Inventory.CloseUserId = 0;
            this.Inventory.InventoryArticleList = new List<InventoryArticles>();
            this.Inventory.InventoryArticle = new Common.Models.InventoryArticles();
            this.ListArticleAddToInventory = new List<int>();
            this.Inventory.CountArticle = 0;

            InventoryArticleViewData = new InvnetoryArticleViewData();
            ListInventories = new List<Inventories>();
        }
        public List<Inventories> GetInventoriesList()
        {
            GetInventoryDatatable();
            List<Inventories> list = new List<Inventories>();
            //dtInventories = new DataTable();
            //dtInventories = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(sqlGetListOpen, "InventoryList", "InventoryList", 0);
            foreach (DataRow r in dtInventories.Rows)
            {
                Inventory = new Inventories();
                SetValue(r);
                list.Add(Inventory);
            }
            return list;
        }

        public void GetInventoryDatatable()
        {
            dtInventories = new DataTable();
            dtInventories = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(sqlGetListOpen, "InventoryList", "InventoryList", 0);
        }


        /// <summary>
        ///             add item to db inventories
        /// </summary>
        public void Add()
        {
            string strSql = sqlAdd;
            strSql = strSql + "Select @@IDENTITY as 'ID';";

            string strTmp = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSql, "InsertInventory", BenutzerID);
            int.TryParse(strTmp, out int iTmp);
            Inventory.Id = iTmp;
        }
        /// <summary>
        /// 
        /// </summary>
        public void AddAll()
        {
            if (this.ListArticleAddToInventory.Count > 0)
            {
                if (this.InventoryArticleViewData.InventoryArticle is InventoryArticles)
                {
                    this.Add();
                    if (Inventory.Id > 0)
                    {
                        string strSql = string.Empty;
                        foreach (int item in this.ListArticleAddToInventory)
                        {
                            InventoryArticleViewData.InventoryArticle = new Common.Models.InventoryArticles();
                            InventoryArticleViewData.InventoryArticle.InventoryId = Inventory.Id;
                            InventoryArticleViewData.InventoryArticle.Description = string.Empty;
                            InventoryArticleViewData.InventoryArticle.ArtikelId = item;
                            InventoryArticleViewData.InventoryArticle.Status = Common.Enumerations.enumInventoryArticleStatus.Neu;
                            InventoryArticleViewData.InventoryArticle.Text = string.Empty;
                            InventoryArticleViewData.InventoryArticle.Created = DateTime.Now;

                            strSql += InventoryArticleViewData.sqlAdd;
                        }

                        clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "InsertInventory", BenutzerID);
                        //Add Logbucheintrag Eintrag
                        string strInfo = "Neue Inventur erstellt erstellt: ID [" + Inventory.Id.ToString() + "] / Name: " + Inventory.Name;
                        Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), strInfo);
                    }
                }
            }
        }
        /// <summary>
        ///             update item in db inventories by id
        /// </summary>
        public void Update()
        {
            string strSql = sqlUpdate;
            bool bReturn = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            // Logbucheintrag Eintrag
            string strInfo = "Inventur geändert: Id: " + Inventory.Id.ToString() + " / Name: " + Inventory.Name;
            Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), strInfo);
        }
        /// <summary>
        ///             delete item from db inventories by id
        /// </summary>
        public void Delete()
        {
            string strSql = sqlDelete;
            if (InventoryArticleViewData.InventoryArticle is InventoryArticles)
            {
                strSql += InventoryArticleViewData.sqlDeleteByInventoryId;
            }

            if (clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "InventoryDelete", this._GL_User.User_ID))
            {
                //Add Logbucheintrag 
                string myBeschreibung = "Inventur gelöscht: ID: " + Inventory.Id.ToString() + " / Name: " + Inventory.Name;
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);
            }
        }
        /// <summary>
        ///             get item without sub inventoryarticle
        /// </summary>
        public void Fill()
        {
            DataTable dt = new DataTable();
            string strSql = sqlGet;

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Inventory");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                }
            }
        }
        /// <summary>
        ///             get item with sub inventoryarticle
        /// </summary>
        public void FillAll()
        {
            DataTable dt = new DataTable();
            dt = clsSQLcon.ExecuteSQL_GetDataTable(sqlGet, BenutzerID, "Inventory");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                }
                InventoryArticleViewData = new InvnetoryArticleViewData(0, Inventory.Id, false);
            }
        }
        private void SetValue(DataRow myRow)
        {
            Inventory.Id = (int)myRow["Id"];
            Inventory.Name = myRow["Name"].ToString();
            Inventory.Description = myRow["Description"].ToString();
            Enum.TryParse(myRow["Art"].ToString(), out Common.Enumerations.enumInventoryArt enumTmp);
            Inventory.Art = enumTmp;
            Inventory.Created = (DateTime)myRow["Created"];
            Inventory.UserId = (int)myRow["UserId"];
            Inventory.ArbeitsbereichId = (int)myRow["ArbeitsbereichId"];
            Enum.TryParse(myRow["Status"].ToString(), out Common.Enumerations.enumInventoryStatus enumTmpStatus);
            Inventory.Status = enumTmpStatus;
            Inventory.CloseDate = (DateTime)myRow["CloseDate"];
            Inventory.CloseUserId = (int)myRow["CloseUserId"];
            if (myRow["CountInventoryArticle"] != null)
            {
                Inventory.CountArticle = (int)myRow["CountInventoryArticle"];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string sqlAdd
        {
            get
            {
                string strSql = string.Empty;
                strSql = "INSERT INTO Inventories (Name, Description, Art, Created, UserId, ArbeitsbereichId, [Status], CloseDate, CloseUserId) " +
                                                "VALUES (" +
                                                       " '" + Inventory.Name + "' " +
                                                       ", '" + Inventory.Description + "' " +
                                                       ", " + (int)Inventory.Art + " " +
                                                       ", '" + Inventory.Created + "' " +
                                                       ", " + Inventory.UserId + " " +
                                                       ", " + Inventory.ArbeitsbereichId + " " +
                                                       ", " + (int)Inventory.Status +
                                                       ", '" + Inventory.CloseDate + "' " +
                                                       ", " + Inventory.CloseUserId + " " +
                                                       "); ";
                return strSql;
            }
        }
        public string sqlUpdate
        {
            get
            {
                string strSql = "Update Inventories SET" +
                                        " Name = '" + Inventory.Name + "' " +
                                        ", [Description] = '" + Inventory.Description + "' " +
                                        ", Art = " + (int)Inventory.Art +
                                        ", Created = '" + Inventory.Created + "' " +
                                        ", UserId = " + Inventory.UserId +
                                        ", ArbeitsbereichId = " + Inventory.ArbeitsbereichId +
                                        ", [Status] = " + (int)Inventory.Status +
                                        ", CloseDate = '" + Inventory.CloseDate + "' " +
                                        ", CloseUserId = " + Inventory.CloseUserId +

                                        " WHERE Id = " + Inventory.Id + " ;";
                return strSql;
            }
        }
        /// <summary>
        ///             delete item by id
        /// </summary>
        public string sqlDelete
        {
            get
            {
                string strSql = "DELETE Inventories WHERE Id = " + Inventory.Id + " ;";
                return strSql;
            }
        }
        /// <summary>
        ///             item of db inventories by id
        /// </summary>
        public string sqlGet
        {
            get
            {
                string strSql = "Select i.* " +
                                        ", (SELECT COUNT(Id) FROM InventoryArticle where InventoryId=i.Id) as CountInventoryArticle " +
                                        "FROM Inventories i " +
                                         " WHERE Id=" + Inventory.Id + " ;";
                return strSql;
            }
        }
        /// <summary>
        ///             All all item of db Inventories
        /// </summary>
        public string sqlGetListAll
        {
            get
            {
                string strSql = "Select * FROM Inventories;";
                return strSql;
            }
        }
        /// <summary>
        ///             all item of db Inventories are not closed
        /// </summary>
        public string sqlGetListOpen
        {
            get
            {
                string strSql = "SELECT i.* " +
                                        ", (SELECT Count(Id) From InventoryArticle where InventoryId= i.Id) as CountInventoryArticle " +

                                        "FROM Inventories i " +
                                        " where i.[Status]<>3; ";
                return strSql;
            }
        }


    }
}

