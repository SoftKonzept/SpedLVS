using Common.ApiModels;
using Common.Models;
using LvsScan.Portable.Enumerations;
using LvsScan.Portable.Interfaces;
using LvsScan.Portable.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

[assembly: Xamarin.Forms.Dependency(typeof(IHTTPClientHandlerCreationService))]

namespace LvsScan.Portable.Services
{
    public class ServiceAPI : apiSetting
    {
        public ServiceAPI()
        {
        }
        ///-------------------------------------------- Inventory --------------------------------------------
        public async Task<ResponseInventory> GetInventroyList()
        {
            api_Inventory api_inv = new api_Inventory();
            var result = await api_inv.GET_InventoryList();
            return result;
        }
        public async Task<ResponseInventoryArticle> GetInventroyArticleList(int inventoryId)
        {
            api_Inventory api_inv = new api_Inventory();
            var result = await api_inv.GET_InventoryArticleList(inventoryId);
            return result;
        }
        public async Task<ResponseInventory> GetInventroy(int inventoryId, int inventroyArticleId)
        {
            api_Inventory api_inv = new api_Inventory();
            var result = await api_inv.GET_Inventory(inventoryId, inventroyArticleId);
            return result;
        }
        //public async Task<ResponseInventoryArticle> GetInventroyArticle(int inventoryId, int inventroyArticleId)
        public async Task<ResponseInventoryArticle> GetInventroyArticle(int inventroyArticleId)
        {
            api_Inventory api_inv = new api_Inventory();
            var result = await api_inv.GET_InventoryArticle(inventroyArticleId);
            return result;
        }

        public async Task<ResponseInventoryArticle> POST_Update_InventoryArticle_Status(InventoryArticles myInventoryArticle)
        {
            api_Inventory api_inv = new api_Inventory();
            var response = await api_inv.POST_Update_InventoryArticle_Status(myInventoryArticle);
            return response;
        }






        //public async Task<bool> Patch_InventoryArticle_Update(InventoryArticles myInventoryArticles)
        //{
        //    //alles unnötige löschen, so dass nur die Klasse übermittelt wird
        //    //myInventoryArticles.Artikel = null;

        //    //api_Inventory api_inv = new api_Inventory();
        //    //var result = await api_inv.PATCH_InventoryArticleUpdate(myInventoryArticles);
        //    //bool bReturn = Boolean.Parse(result);
        //    return false;
        //}



        // ------------------------------------TEST ---------------------------------------------------


        public async Task<List<WeatherForecast>> GetWeatherForecastList()
        {
            api_Tests api_test = new api_Tests(enumHTTPMethodeType.GET);
            List<WeatherForecast> list = await api_test.GetWeatherForecastList();
            return list;
        }

        public async Task<string> GetDBConnectionInfoIntern()
        {
            api_Tests api_test = new api_Tests(enumHTTPMethodeType.GET);
            string strReturn = await api_test.GetDBConnectionInfoIntern();
            return strReturn;
        }

        public async Task<string> GetDBConnectionInfoExtern()
        {
            api_Tests api_test = new api_Tests(enumHTTPMethodeType.GET);
            string strReturn = await api_test.GetDBConnectionInfoExtern();
            return strReturn;
        }


        //--------------------------------------- Database ---------------------------------------------

        /// <summary>
        ///                         /api/Database/PATCH_DatabaseAction_Update/
        /// </summary>
        public Uri Uri_PATCH_DatabaseAction_Update
        {
            get
            {
                string tmp = ServerUrl + "/api/Database/PATCH_DatabaseAction_Update/";
                Uri retUri = new Uri(tmp);
                return retUri;
            }
        }
        public async Task<bool> Patch_Datatable_Update(DatabaseManipulations myDatabaseAction)
        {
            string strReturn = string.Empty;
            try
            {

                var result1 = Uri_PATCH_DatabaseAction_Update + JsonConvert.SerializeObject(myDatabaseAction);
                string finalUri1 = HttpUtility.UrlEncode(result1.ToString());


                //var method = new HttpMethod("PATCH");
                //var request = new HttpRequestMessage(method, finalUri)
                //{
                //    Content = new StringContent(
                //                    JsonConvert.SerializeObject(myDatabaseAction),
                //                    Encoding.UTF8, "application/json")
                //};

                //var response = await client.SendAsync(request);


                //var result = HttpUtility.UrlEncode(JsonConvert.SerializeObject(myDatabaseAction));
                //string finalUri = Uri_PATCH_DatabaseAction_Update + result.ToString();


                string uriTmp = String.Empty;
                uriTmp = Uri_PATCH_DatabaseAction_Update + JsonConvert.SerializeObject(myDatabaseAction);

                if (uriTmp.Length > 0)
                {
                    var requestContent = new StringContent(string.Empty);
                    var method = enumHTTPMethodeType.PATCH.ToString();
                    var httpVerb = new HttpMethod(method);
                    var httpRequestMessage =
                        new HttpRequestMessage(httpVerb, uriTmp)
                        {
                            Content = requestContent
                        };
                    var response = await client.SendAsync(httpRequestMessage);
                    //res = JsonConvert.DeserializeObject<string>(response);
                    strReturn = response.ToString();
                }

                //strReturn = response1.ToString();
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
            }
            bool bReturn = Boolean.Parse(strReturn);
            return bReturn;
        }

    }

}
