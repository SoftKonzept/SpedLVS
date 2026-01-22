using Common.ApiModels;
using Common.Models;
using LvsScan.Portable.Enumerations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LvsScan.Portable.Services
{

    public class api_Article : apiSetting
    {
        public api_Article(enumHTTPMethodeType HttpType)
        {
            client = InitHttpClient(Device.RuntimePlatform, HttpType);
        }
        public api_Article()
        {
            client = InitHttpClient();
        }


        /// <summary>
        ///             /api/Article/GET_Article/2
        /// </summary>
        public Uri Uri_GET_Article_ById
        {
            get
            {
                string tmp = ServerUrl + "/api/Article/GET_Article/";
                Uri retUri = new Uri(tmp);
                return retUri;
            }
        }
        public async Task<Articles> GET_Article_ById(int ArticleId, int UserId)
        {
            string strReturn = string.Empty;
            Articles articles = new Articles();
            try
            {
                //var strinSer = JsonConvert.SerializeObject(myArticleSearch);
                string tmpUri = Uri_GET_Article_ById.ToString() + ArticleId.ToString();
                tmpUri += "/" + UserId;
                HttpResponseMessage response = await client.GetAsync(tmpUri);
                //if (response.IsSuccessStatusCode)
                //{
                //    var content = await response.Content.ReadAsStringAsync();
                //    articles = JsonConvert.DeserializeObject<Articles>(content);
                //}
                var content = await response.Content.ReadAsStringAsync();
                articles = JsonConvert.DeserializeObject<Articles>(content);
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
            }
            return articles;
        }

        /// <summary>
        ///             /api/Article/GET_Article_SearchArticle/235873/148301
        /// </summary>
        public Uri Uri_GET_Article_Search
        {
            get
            {
                string tmp = ServerUrl + "/api/Article/GET_Article_SearchArticle/";
                Uri retUri = new Uri(tmp);
                return retUri;
            }
        }
        public async Task<Articles> GET_Article_Search(ArticleSearch myArticleSearch)
        {
            string strReturn = string.Empty;
            Articles articles = new Articles();
            try
            {
                string strValString = string.Empty;
                strValString += myArticleSearch.LvsNoSearch;
                strValString += "/" + myArticleSearch.ProductionsNoSearch;
                string tmpUri = Uri_GET_Article_Search.ToString() + strValString;
                HttpResponseMessage response = await client.GetAsync(tmpUri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    articles = JsonConvert.DeserializeObject<Articles>(content);
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
            }
            return articles;
        }

        /// <summary>
        ///             /api/Article/GET_Article_SearchArticleForStoredLocationChange/235873
        /// </summary>
        public Uri Uri_GET_Article_SearchArticleForStoredLocationChange
        {
            get
            {
                string tmp = ServerUrl + "/api/Article/GET_Article_SearchArticleForStoredLocationChange/";
                Uri retUri = new Uri(tmp);
                return retUri;
            }
        }
        public async Task<Articles> GET_Article_SearchArticleForStoredLocationChange(ArticleSearch myArticleSearch)
        {
            string strReturn = string.Empty;
            Articles articles = new Articles();
            try
            {
                string strValString = string.Empty;
                strValString += myArticleSearch.LvsNoSearch;
                string tmpUri = Uri_GET_Article_SearchArticleForStoredLocationChange.ToString() + strValString;
                HttpResponseMessage response = await client.GetAsync(tmpUri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    articles = JsonConvert.DeserializeObject<Articles>(content);
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
            }
            return articles;
        }

        /// <summary>
        ///             /api/Article/GetExistArticleValue/{SearchValueString}/{enumArticleDatafieldId }
        /// </summary>
        public Uri Uri_GET_Article_ExistArticleValue
        {
            get
            {
                string tmp = ServerUrl + "/api/Article/GET_Article_ExistArticleValue/";
                Uri retUri = new Uri(tmp);
                return retUri;
            }
        }
        public async Task<ArticleSearch> GET_Article_ExistArticleValue(ArticleSearch myArticleSearch)
        {
            string strReturn = string.Empty;
            try
            {
                string strValString = string.Empty;
                switch (myArticleSearch.CurrentSearchValueDatafield)
                {
                    case Common.Enumerations.enumArticle_Datafields.LVS_ID:
                        strValString = myArticleSearch.LvsNoSearch;
                        break;
                    case Common.Enumerations.enumArticle_Datafields.Produktionsnummer:
                        strValString = myArticleSearch.LvsNoSearch;
                        break;
                    default:
                        break;
                }
                strValString += "/" + ((int)myArticleSearch.CurrentSearchValueDatafield).ToString();

                string tmpUri = Uri_GET_Article_ExistArticleValue + strValString;
                HttpResponseMessage response = await client.GetAsync(tmpUri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    //myArticleSearch = new ArticleSearch();
                    myArticleSearch.ArticleSearcheResult = new Articles();
                    myArticleSearch.ArticleSearcheResult = JsonConvert.DeserializeObject<Articles>(content);
                    myArticleSearch.ExistLvsNoSearchValue = true;
                }
                else
                {
                    myArticleSearch.ErrorText = response.StatusCode.ToString();
                    myArticleSearch.ErrorText += Environment.NewLine + "Es konnte kein Datensatz gefunden werden!";
                    myArticleSearch.ExistLvsNoSearchValue = false;
                    // + " - " + response.
                }
            }
            catch (Exception ex)
            {
                myArticleSearch.ErrorText = ex.InnerException.Message;
                //myArticleSearch.ErrorText += Environment.NewLine + "Es konnte kein Datensatz gefunden werden!";
                myArticleSearch.ExistLvsNoSearchValue = false;
            }
            return myArticleSearch;
        }

        /// <summary>
        ///             /api/Article/GetExistArticleValue/{SearchValueString}/{enumArticleDatafieldId }
        /// </summary>
        public Uri Uri_GET_Article_ExistArticleLvsForStoreLocationChange
        {
            get
            {
                string tmp = ServerUrl + "/api/Article/GET_Article_ExistArticleLvsForStoreLocationChange/";
                Uri retUri = new Uri(tmp);
                return retUri;
            }
        }
        public async Task<ArticleSearch> GET_Article_ExistArticleLvsForStoreLocationChange(ArticleSearch myArticleSearch)
        {
            string strReturn = string.Empty;
            try
            {
                if (!myArticleSearch.LvsNoSearch.Equals(string.Empty))
                {
                    string strValString = string.Empty;
                    strValString += myArticleSearch.LvsNoSearch;
                    string tmpUri = Uri_GET_Article_ExistArticleLvsForStoreLocationChange.ToString() + strValString;
                    HttpResponseMessage response = await client.GetAsync(tmpUri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        Articles articles = new Articles();
                        articles = JsonConvert.DeserializeObject<Articles>(content);
                        myArticleSearch.ArticleSearcheResult = articles.Copy();

                        if (articles.LVSNrBeforeUB.ToString().Equals(myArticleSearch.LvsNoSearch))
                        {
                            myArticleSearch.IsRebookedArticle = (articles.LVSNrBeforeUB.ToString().Equals(myArticleSearch.LvsNoSearch));
                        }
                        myArticleSearch.ExistLvsNoSearchValue = true;
                    }
                }

                //string strValString = string.Empty;
                //switch (myArticleSearch.CurrentSearchValueDatafield)
                //{
                //    case Common.Enumerations.enumArticle_Datafields.LVS_ID:
                //        strValString = myArticleSearch.LvsNoSearch;
                //        break;
                //    case Common.Enumerations.enumArticle_Datafields.Produktionsnummer:
                //        strValString = myArticleSearch.LvsNoSearch;
                //        break;
                //    default:
                //        break;
                //}
                //strValString += "/" + ((int)myArticleSearch.CurrentSearchValueDatafield).ToString();

                //string tmpUri = Uri_GET_Article_ExistArticleValue + strValString;
                //HttpResponseMessage response = await client.GetAsync(tmpUri);
                //if (response.IsSuccessStatusCode)
                //{
                //    var content = await response.Content.ReadAsStringAsync();
                //    //myArticleSearch = new ArticleSearch();
                //    myArticleSearch.ArticleSearcheResult = new Articles();
                //    myArticleSearch.ArticleSearcheResult = JsonConvert.DeserializeObject<Articles>(content);
                //    myArticleSearch.ExistLvsNoSearchValue = true;
                //}
                //else
                //{
                //    myArticleSearch.ErrorText = response.StatusCode.ToString();
                //    myArticleSearch.ErrorText += Environment.NewLine + "Es konnte kein Datensatz gefunden werden!";
                //    myArticleSearch.ExistLvsNoSearchValue = false;
                //    // + " - " + response.
                //}
            }
            catch (Exception ex)
            {
                myArticleSearch.ErrorText = ex.InnerException.Message;
                //myArticleSearch.ErrorText += Environment.NewLine + "Es konnte kein Datensatz gefunden werden!";
                myArticleSearch.ExistLvsNoSearchValue = false;
            }
            return myArticleSearch;
        }


        /// <summary>
        ///             /api/Article/GET_Article_GetArticleInStoreInByProductionNo/{enumArticleDatafieldId }
        ///             api/Article/GET_Article_GetArticleInStoreInByProductionNo/1897
        /// </summary>
        public Uri Uri_GET_Article_GetArticleInStoreInByProductionNo
        {
            get
            {
                string tmp = ServerUrl + "/api/Article/GET_Article_GetArticleInStoreInByProductionNo/";
                Uri retUri = new Uri(tmp);
                return retUri;
            }
        }
        public async Task<ArticleSearch> GET_Article_GetArticleInStoreInByProductionNo(string myProductionSearch)
        {
            string strReturn = string.Empty;
            ArticleSearch myArticleSearch = new ArticleSearch();
            try
            {

                string tmpUri = Uri_GET_Article_GetArticleInStoreInByProductionNo + myProductionSearch; // +"/"+ LoggedUser.Id ;
                HttpResponseMessage response = await client.GetAsync(tmpUri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<Articles>>(content);
                    myArticleSearch.ListArticles = new List<Articles>();
                    myArticleSearch.ListArticles = result;
                }
                else
                {
                    myArticleSearch.ErrorText = response.StatusCode.ToString();
                    myArticleSearch.ErrorText += Environment.NewLine + "Es konnte kein Datensatz gefunden werden!";
                    myArticleSearch.ExistLvsNoSearchValue = false;
                    // + " - " + response.
                }
            }
            catch (Exception ex)
            {
                myArticleSearch.ErrorText = ex.InnerException.Message;
                //myArticleSearch.ErrorText += Environment.NewLine + "Es konnte kein Datensatz gefunden werden!";
                myArticleSearch.ExistLvsNoSearchValue = false;
            }
            return myArticleSearch;
        }





        //------ POST
        ///<summary>
        ///                 Update InventoryArticle
        ///                 /api/Article/POST_Update_Article_StoreLocation
        ///</summary>
        public Uri Uri_POST_Article_Update_StoredLocation
        {
            get { return new Uri(ServerUrl + "/api/Article/POST_Article_Update_StoreLocation"); }
        }
        public async Task<ResponseStoreLocationChange> POST_Article_Update_StoreLocation(Articles article)
        {
            ResponseStoreLocationChange responseStoreLoc = new ResponseStoreLocationChange();
            responseStoreLoc.Article = article.Copy();
            responseStoreLoc.UserId = (int)((App)Application.Current).LoggedUser.Id;
            responseStoreLoc.Error = string.Empty;
            responseStoreLoc.ArticleId = article.Id;

            //ArticleStoreLocation loc = new ArticleStoreLocation();
            //loc.ArticleId = article.Id;
            //loc.Werk = article.Werk;
            //loc.Halle = article.Halle;
            //loc.Reihe = article.Reihe;
            //loc.Ebene = article.Ebene;
            //loc.Platz = article.Platz;
            //loc.LagerOrt = article.LagerOrt;
            //loc.LagerOrtTable = article.LagerOrtTable;

            try
            {
                //responseSLC.ArtStoreLocation = loc.Copy();
                //var json = JsonConvert.SerializeObject(loc);

                var json = JsonConvert.SerializeObject(responseStoreLoc);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(Uri_POST_Article_Update_StoredLocation, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    var jwt = await response.Content.ReadAsStringAsync();
                    var reply = JsonConvert.DeserializeObject<ResponseStoreLocationChange>(jwt);
                    responseStoreLoc = reply.Copy();
                    responseStoreLoc.SuccessStoreLocationChange = true;
                }
                else
                {
                    responseStoreLoc.Error += Environment.NewLine + response.ReasonPhrase;
                    responseStoreLoc.SuccessStoreLocationChange = false;
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
                responseStoreLoc.Error += mes;
            }
            return responseStoreLoc;
        }
        ///<summary>
        ///                 Update Article Scan Values
        ///                 /api/Article/POST_Article_Update_ScanValue
        ///</summary>
        public Uri Uri_POST_Article_Update_ScanValue
        {
            get { return new Uri(ServerUrl + "/api/Article/POST_Article_Update_ScanValue"); }
        }
        public async Task<ResponseArticle> POST_Article_Update_ScanValue(ResponseArticle responsArticle)
        {
            responsArticle.Success = false;
            responsArticle.Error = string.Empty;
            responsArticle.Info = string.Empty;

            try
            {
                var json = JsonConvert.SerializeObject(responsArticle);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(Uri_POST_Article_Update_ScanValue, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    var jwt = await response.Content.ReadAsStringAsync();
                    var reply = JsonConvert.DeserializeObject<ResponseArticle>(jwt);
                    responsArticle = reply.Copy();
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
                responsArticle.Error = mes;
            }
            return responsArticle;
        }

        ///<summary>
        ///                 Update Article Checked for SToreIn und StoreOut
        ///                 /api/Article/POST_Article_Update_Checked
        ///</summary>
        public Uri Uri_POST_Article_Update_Checked
        {
            get { return new Uri(ServerUrl + "/api/Article/POST_Article_Update_Checked"); }
        }
        public async Task<ResponseArticle> POST_Article_Update_Checked(ResponseArticle responsArticle)
        {
            responsArticle.Success = false;
            responsArticle.Error = string.Empty;
            responsArticle.Info = string.Empty;

            try
            {
                var json = JsonConvert.SerializeObject(responsArticle);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(Uri_POST_Article_Update_Checked, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    var jwt = await response.Content.ReadAsStringAsync();
                    var reply = JsonConvert.DeserializeObject<ResponseArticle>(jwt);
                    responsArticle = reply.Copy();
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
                responsArticle.Error = mes;
            }
            return responsArticle;
        }

        ///<summary>
        ///                 Update Article Checked for SToreIn und StoreOut
        ///                 /api/Article/POST_Article_Update_ScanIdentification
        ///</summary>
        public Uri Uri_POST_Article_Update_ScanIdentification
        {
            get { return new Uri(ServerUrl + "/api/Article/POST_Article_Update_ScanIdentification"); }
        }
        public async Task<ResponseArticle> POST_Article_Update_ScanIdentification(ResponseArticle responsArticle)
        {
            responsArticle.Success = false;
            responsArticle.Error = string.Empty;
            responsArticle.Info = string.Empty;

            try
            {
                var json = JsonConvert.SerializeObject(responsArticle);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(Uri_POST_Article_Update_ScanIdentification, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    var jwt = await response.Content.ReadAsStringAsync();
                    var reply = JsonConvert.DeserializeObject<ResponseArticle>(jwt);
                    responsArticle = reply.Copy();
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
                responsArticle.Error = mes;
            }
            return responsArticle;
        }

        ///<summary>
        ///                 Update Article Checked for SToreIn und StoreOut
        ///                 /api/Article/POST_Article_Update_ManualEdit
        ///</summary>
        public Uri Uri_POST_Article_Update_ManualEdit
        {
            get { return new Uri(ServerUrl + "/api/Article/POST_Article_Update_ManualEdit"); }
        }
        public async Task<ResponseArticle> POST_Article_Update_ManualEdit(ResponseArticle responsArticle)
        {
            responsArticle.Success = false;
            responsArticle.Error = string.Empty;
            responsArticle.Info = string.Empty;

            try
            {
                var json = JsonConvert.SerializeObject(responsArticle);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(Uri_POST_Article_Update_ManualEdit, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    var jwt = await response.Content.ReadAsStringAsync();
                    var reply = JsonConvert.DeserializeObject<ResponseArticle>(jwt);
                    responsArticle = reply.Copy();
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
                responsArticle.Error = mes;
            }
            return responsArticle;
        }



        ///<summary>
        ///                 Add Article
        ///                 /api/Article/POST_Article_AddByScanner
        ///</summary>
        public Uri Uri_POST_Article_AddByScanner
        {
            get { return new Uri(ServerUrl + "/api/Article/POST_Article_AddByScanner"); }
        }
        public async Task<ResponseArticle> POST_Article_AddByScanner(ResponseArticle responsArticle)
        {
            try
            {
                var json = JsonConvert.SerializeObject(responsArticle);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(Uri_POST_Article_AddByScanner, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    var jwt = await response.Content.ReadAsStringAsync();
                    var reply = JsonConvert.DeserializeObject<ResponseArticle>(jwt);
                    responsArticle = reply.Copy();
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
                responsArticle.Error = mes;
            }
            return responsArticle;
        }
    }
}
