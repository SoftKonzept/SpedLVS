using Common.ApiModels;
using Common.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LvsScan.Portable.Services
{

    public class api_Inventory : apiSetting
    {
        //public api_Inventory(enumHTTPMethodeType HttpType)
        //{
        //    client = InitHttpClient(Device.RuntimePlatform, HttpType);
        //}

        public api_Inventory()
        {
            client = InitHttpClient();
        }
        //---------------------------------------------- Inventory -----------------------------------------------
        ///<summary>
        ///                 Class Inventory  
        ///                 /api/Inventory/GetInventory/1
        ///</summary>
        public Uri Uri_Inventory_GET_GetInventory
        {
            get { return new Uri(ServerUrl + "/api/Inventory/GetInventory"); }
        }
        public async Task<ResponseInventory> GET_Inventory(int inventoryId, int inventoryArticleId)
        {
            ResponseInventory responseInventory = new ResponseInventory();
            try
            {
                string tmpUri = Uri_Inventory_GET_GetInventory + "/" + inventoryId.ToString();
                HttpResponseMessage response = await client.GetAsync(tmpUri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResponseInventory>(content);
                    responseInventory = result.Copy();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
            }
            return responseInventory;
        }

        ///<summary>
        ///                 Liste der Inventuren
        ///                 /api/Inventory/GetInventoryList
        ///</summary>
        public Uri Uri_Inventory_GET_GetInventoryList
        {
            get { return new Uri(ServerUrl + "/api/Inventory/GetInventoryList"); }
        }
        public async Task<ResponseInventory> GET_InventoryList()
        {
            //string str = Uri_Inventory_GET_GetInventoryList.ToString();
            ResponseInventory responseInventory = new ResponseInventory();
            try
            {
                var response = await client.GetAsync(Uri_Inventory_GET_GetInventoryList).ConfigureAwait(true);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResponseInventory>(content);
                    responseInventory = result.Copy();
                }
            }
            catch (Exception ex)
            {
                responseInventory.Error = ex.InnerException.Message;
            }
            return responseInventory;
        }


        //-------------------------------------------------------- InventoryArticle ------------------------------------
        ///<summary>
        ///                 Class InventoryArticle 
        ///                 /api/Inventory/GetInventoryArticle/1
        ///</summary>
        public Uri Uri_Inventory_GET_GetInventoryArticle
        {
            get
            {
                string tmp = ServerUrl + "/api/Inventory/GetInventoryArticle";
                Uri retUri = new Uri(tmp);
                return retUri;
            }
        }
        //public async Task<ResponseInventoryArticle> GET_InventoryArticle(int inventoryId, int inventoryArticleId)
        public async Task<ResponseInventoryArticle> GET_InventoryArticle(int inventoryArticleId)
        {
            ResponseInventoryArticle InventoryArticle = new ResponseInventoryArticle();
            try
            {
                string tmpUri = Uri_Inventory_GET_GetInventoryArticle + "/" + inventoryArticleId.ToString();
                var response = await client.GetStringAsync(tmpUri);
                InventoryArticle = JsonConvert.DeserializeObject<ResponseInventoryArticle>(response);
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
            }
            return InventoryArticle;
        }
        ///<summary>
        ///                 Liste der Inventurenartikel
        ///                 /api/Inventory/GetInventoryArticleList/1
        ///</summary>
        public Uri Uri_Inventory_GET_GetInventoryArticleList
        {
            get { return new Uri(ServerUrl + "/api/Inventory/GetInventoryArticleList"); }
        }
        public async Task<ResponseInventoryArticle> GET_InventoryArticleList(int inventoryId)
        {
            ResponseInventoryArticle resInvArt = new ResponseInventoryArticle();
            try
            {
                string tmpUri = Uri_Inventory_GET_GetInventoryArticleList + "/" + inventoryId.ToString();

                var response = await client.GetStringAsync(tmpUri);
                var result = JsonConvert.DeserializeObject<ResponseInventoryArticle>(response);
                resInvArt.ListInventoryArticle.AddRange(result.ListInventoryArticle);
            }
            catch (Exception ex)
            {
                resInvArt.Error = ex.InnerException.Message;
            }
            return resInvArt;
        }


        //------------ POST
        ///<summary>
        ///                 Update InventoryArticle Status
        ///                 /api/Inventory/POST_InventoryArticle_Update_Status
        ///</summary>
        public Uri Uri_POST_InventoryArticle_Update_Status
        {
            //get { return new Uri(ServerUrl + "/api/Inventory/POST_InventoryArticle_Update_Status"); }
            get { return new Uri(ServerUrl + "/api/Inventory/POST_Update_InventoryArticle_Status"); }
        }
        public async Task<ResponseInventoryArticle> POST_Update_InventoryArticle_Status(InventoryArticles invArticle)
        {
            invArticle.Scanned = DateTime.Now;

            ResponseInventoryArticle resIA = new ResponseInventoryArticle();
            resIA.InventoryArticle = invArticle;

            var json = JsonConvert.SerializeObject(resIA);
            HttpContent httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            try
            {
                var response = await client.PostAsync(Uri_POST_InventoryArticle_Update_Status, httpContent);

                resIA.Success = response.IsSuccessStatusCode;
                if (response.IsSuccessStatusCode)
                {
                    var jwt = await response.Content.ReadAsStringAsync();
                    var reply = JsonConvert.DeserializeObject<ResponseInventoryArticle>(jwt);
                    resIA = reply.Copy();
                }
                else
                {
                    resIA.Error = "Es ist ein Fehler aufgetreten. Es konnte keine Rückmeldung vom Server erhalten!";
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
                resIA.Error = mes;
            }

            return resIA;
        }


    }
}
