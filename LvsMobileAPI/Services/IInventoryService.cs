using Common.ApiModels;
using Common.Models;
using LVS.ViewData;
using LvsMobileAPI.DataConnection;

namespace LvsMobileAPI.Services
{
    public interface IInventoryService
    {
        //-------------------------------------------- Inventory --------------------------------------------
        public Inventories GetInventory(int InventoryId);
        public List<Inventories> GetInventoryList();

        //----------------------------------------- InventoryArticle --------------------------------------------
        public InventoryArticles GetInventoryArticle(int InventoryArticleId);
        public List<InventoryArticles> GetInventoryArticleList(int InventoryId);
        public bool POST_Update_InventoryArticle_Status(ResponseInventoryArticle ResonsdeInventoryArticle);
    }

    public class InventoryService : IInventoryService
    {
        private SvrSettings srv;
        public InventoryService()
        {
            srv = new SvrSettings();
        }

        //-------------------------------------------- Inventory --------------------------------------------
        public Inventories GetInventory(int InventoryId)
        {
            Inventories inventories = new Inventories();
            InventoryViewData inventoryViewData = new InventoryViewData(InventoryId, 0);
            if (
                    (inventoryViewData != null) &&
                    (inventoryViewData.Inventory != null)
               )
            {
                inventories = inventoryViewData.Inventory.Copy();
            }
            return inventories;
        }

        public List<Inventories> GetInventoryList()
        {
            List<Inventories> ListInventories = new List<Inventories>();

            InventoryViewData inventoryViewData = new InventoryViewData(true);
            if ((inventoryViewData.Inventory != null) && (inventoryViewData.ListInventories != null))
            {
                ListInventories.AddRange(inventoryViewData.ListInventories);
            }
            return ListInventories;
        }



        //----------------------------------------- InventoryArticle --------------------------------------------

        public InventoryArticles GetInventoryArticle(int InventoryArticleId)
        {
            InventoryArticles inventoryArticle = new InventoryArticles();

            InvnetoryArticleViewData inventoryArticleViewData = new InvnetoryArticleViewData(0, InventoryArticleId, false);
            if ((inventoryArticleViewData.InventoryArticle != null) && (inventoryArticleViewData.InventoryArticle.Id > 0))
            {
                inventoryArticle = inventoryArticleViewData.InventoryArticle.Copy();
            }
            return inventoryArticle;
        }

        public List<InventoryArticles> GetInventoryArticleList(int InventoryId)
        {
            List<InventoryArticles> ListInventoryArticle = new List<InventoryArticles>();

            InvnetoryArticleViewData inventoryArticleViewData = new InvnetoryArticleViewData(InventoryId, false);
            if ((inventoryArticleViewData.InventoryArticle != null) && (inventoryArticleViewData.ListInventoryArticle != null))
            {
                ListInventoryArticle.AddRange(inventoryArticleViewData.ListInventoryArticle);
            }
            return ListInventoryArticle;
        }

        public bool POST_Update_InventoryArticle_Status(ResponseInventoryArticle ResonsdeInventoryArticle)
        {
            bool bReturn = false;
            InvnetoryArticleViewData inventoryArticleViewData = new InvnetoryArticleViewData(ResonsdeInventoryArticle.InventoryArticle);
            bReturn = inventoryArticleViewData.Update_ArticleStatus();
            return bReturn;
        }
    }
}
