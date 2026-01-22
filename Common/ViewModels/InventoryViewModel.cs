using System;
using System.Collections.Generic;
using System.Data;
using Common.Models;


namespace Common.ViewModels
{
    public class InventoryViewModel
    {
        public List<int> ListArticleAddToInventory = new List<int>();
        public List<Inventories> ListInventories = new List<Inventories>();
        public DataTable dtInventories = new DataTable();

        public Inventories Inventory { get; set; }

        //public InventoryArticleViewModel InventoryArticleVM { get; set; }
        //public Globals._GL_USER _GL_User;
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

        public InventoryViewModel()
        {
            //InitCls();
        }

        public InventoryViewModel(int myId)
        {
            InitCls();
            Inventory.Id = myId;
            if (myId > 0)
            {
                Fill();
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
            this.Inventory.Art = enumInventoryArt.NotSet;
            this.Inventory.UserId = 0;
            this.Inventory.Created = DateTime.Now;
            this.Inventory.ArbeitsbereichId = 0;
            this.Inventory.Status = enumInventoryStatus.NotSet;
            this.Inventory.CloseDate = new DateTime(1900, 1, 1);
            this.Inventory.CloseUserId = 0;

            //this.Inventory.InventoryArticle = new InventoryArticles();
            this.ListArticleAddToInventory = new List<int>();

            InventoryArticleVM = new InvnetoryArticleViewModel();
            GetInventoryList();
        }


        ///<summary>clsInventory / Add</summary>
        ///<remarks></remarks>
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
                if (this.InventoryArticleVM.InventoryArticle is InventoryArticles)
                {
                    this.Add();
                    if (Inventory.Id > 0)
                    {
                        string strSql = string.Empty;
                        foreach (int item in this.ListArticleAddToInventory)
                        {
                            InventoryArticleVM.InventoryArticle = new InventoryArticles();
                            InventoryArticleVM.InventoryArticle.InventoryId = Inventory.Id;
                            InventoryArticleVM.InventoryArticle.Description = string.Empty;
                            InventoryArticleVM.InventoryArticle.ArtikelId = item;
                            InventoryArticleVM.InventoryArticle.Status = enumInventoryArticleStatus.Neu;
                            InventoryArticleVM.InventoryArticle.Text = string.Empty;
                            InventoryArticleVM.InventoryArticle.Created = DateTime.Now;

                            strSql += InventoryArticleVM.sqlAdd;
                        }

                        clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "InsertInventory", BenutzerID);
                        //Add Logbucheintrag Eintrag
                        string strInfo = "Neue Inventur erstellt erstellt: ID [" + Inventory.Id.ToString() + "] / Name: " + Inventory.Name;
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
            string strInfo = "Inventur geändert: Id: " + Inventory.Id.ToString() + " / Name: " + Inventory.Name;
            Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), strInfo);
        }

        ///<summary>clsInventory / Delete</summary>
        ///<remarks></remarks>
        public void Delete()
        {
            string strSql = sqlDelete;
            if (InventoryArticleVM.InventoryArticle is InventoryArticles)
            {
                strSql += InventoryArticleVM.sqlDeleteByInventoryId;
            }

            if (clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "InventoryDelete", this._GL_User.User_ID))
            {
                //Add Logbucheintrag 
                string myBeschreibung = "Inventur gelöscht: ID: " + Inventory.Id.ToString() + " / Name: " + Inventory.Name;
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
                InventoryArticleVM = new InvnetoryArticleViewModel(0, Inventory.Id);
                //this.InventoryArticle = new clsInventoryArticle(0, this.Id);
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
                InventoryArticleVM = new InvnetoryArticleViewModel(0, Inventory.Id);
                //this.InventoryArticle = new clsInventoryArticle(0, this.Id);
            }
        }
        ///<summary>clsInventory / SetValue</summary>
        /// <param name="dt"></param>
        private void SetValue(DataTable dt)
        {
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                Inventory.Id = (int)dt.Rows[i]["Id"];
                Inventory.Name = dt.Rows[i]["Name"].ToString();
                Inventory.Description = dt.Rows[i]["Description"].ToString();
                Enum.TryParse(dt.Rows[i]["Art"].ToString(), out enumInventoryArt enumTmp);
                Inventory.Art = enumTmp;
                Inventory.Created = (DateTime)dt.Rows[i]["Created"];
                Inventory.UserId = (int)dt.Rows[i]["UserId"];
                Inventory.ArbeitsbereichId = (int)dt.Rows[i]["ArbeitsbereichId"];
                Enum.TryParse(dt.Rows[i]["Status"].ToString(), out enumInventoryStatus enumTmpStatus);
                Inventory.Status = enumTmpStatus;
                Inventory.CloseDate = (DateTime)dt.Rows[i]["CloseDate"];
                Inventory.CloseUserId = (int)dt.Rows[i]["CloseUserId"];
            }
        }
        private void SetValue(DataRow myRow)
        {
            Inventory.Id = (int)myRow["Id"];
            Inventory.Name = myRow["Name"].ToString();
            Inventory.Description = myRow["Description"].ToString();
            Enum.TryParse(myRow["Art"].ToString(), out enumInventoryArt enumTmp);
            Inventory.Art = enumTmp;
            Inventory.Created = (DateTime)myRow["Created"];
            Inventory.UserId = (int)myRow["UserId"];
            Inventory.ArbeitsbereichId = (int)myRow["ArbeitsbereichId"];
            Enum.TryParse(myRow["Status"].ToString(), out enumInventoryStatus enumTmpStatus);
            Inventory.Status = enumTmpStatus;
            Inventory.CloseDate = (DateTime)myRow["CloseDate"];
            Inventory.CloseUserId = (int)myRow["CloseUserId"];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void GetInventoryList()
        {
            //Inventory inv = new clsInventory();
            //string strSql = inv.sqlGetList;
            string strSql = "Select * FROM Inventories;";
            dtInventories = new DataTable();
            dtInventories = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "InventoryList", "InventoryList", 0);
            foreach (DataRow r in dtInventories.Rows)
            {
                Inventory = new Inventories();
                SetValue(r);
                ListInventories.Add(Inventory);
            }
        }


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
        public string sqlDelete
        {
            get
            {
                string strSql = "DELETE Inventories WHERE Id = " + Inventory.Id + " ;";
                return strSql;
            }
        }

        public string sqlGet
        {
            get
            {
                string strSql = "Select * FROM Inventories WHERE Id=" + Inventory.Id + " ;";
                return strSql;
            }
        }

        public string sqlGetAll
        {
            get
            {
                string strSql = "Select * FROM Inventories WHERE Id=" + Inventory.Id + " ;";
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
    }
}

