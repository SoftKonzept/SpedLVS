using Common.ApiModels;
using Common.Models;
using LvsScan.Portable.Enumerations;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LvsScan.Portable.Services
{

    public class api_AsnRead : apiSetting
    {
        public api_AsnRead()
        {
            client = InitHttpClient();
        }
        public api_AsnRead(enumHTTPMethodeType HttpType)
        {
            client = InitHttpClient(Device.RuntimePlatform, HttpType);
        }
        public api_AsnRead(Users myUser) : this()
        {
            LoggedUser = myUser.Copy();
        }

        /// <summary>
        ///             /api/AsnRead/GET_ASN_GetLfsArticleListFromAsn
        /// </summary>
        public Uri Uri_GET_ASN_GetLfsArticleListFromAsn
        {
            get
            {
                string tmp = ServerUrl + "/api/AsnRead/GET_ASN_GetLfsArticleListFromAsn";
                Uri retUri = new Uri(tmp);
                return retUri;
            }
        }
        public async Task<ResponseASN> GET_GetLfsArticleListFromAsn()
        {
            string strReturn = string.Empty;
            ResponseASN response = new ResponseASN();
            response.Success = false;
            try
            {
                HttpResponseMessage result = await client.GetAsync(Uri_GET_ASN_GetLfsArticleListFromAsn);
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<ResponseASN>(content);
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
            }
            return response;
        }

        /// <summary>
        ///             /api/AsnRead/GET_ASN_GetLfsArticleByProductionnumber/{Poductionnumber}/{UserId}
        /// </summary>
        public Uri GET_ASN_GetLfsArticleByProductionnumber
        {
            get
            {
                string tmp = ServerUrl + "/api/AsnRead/GET_ASN_GetLfsArticleByProductionnumber/";
                Uri retUri = new Uri(tmp);
                return retUri;
            }
        }
        public async Task<ResponseASN> GET_ASN_GetAsnLfsArticleByProductionnumber(string mySearchValue, int myUserId)
        {
            string strReturn = string.Empty;
            ResponseASN response = new ResponseASN();
            response.Success = false;
            response.Error = string.Empty;
            response.Info = string.Empty;
            try
            {
                string tmpUri = GET_ASN_GetLfsArticleByProductionnumber.ToString() + mySearchValue;
                tmpUri += "/" + myUserId;
                HttpResponseMessage result = await client.GetAsync(tmpUri);
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<ResponseASN>(content);
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
            }
            return response;
        }

        /// <summary>
        ///             api/AsnRead/POST_ASN_CreateStoreIn
        /// </summary>
        public Uri Uri_POST_ASN_CreateStoreIn
        {
            get
            {
                string tmp = ServerUrl + "/api/AsnRead/POST_ASN_CreateStoreIn";
                Uri retUri = new Uri(tmp);
                return retUri;
            }
        }
        public async Task<ResponseASN> POST_ASN_CreateStoreIn(ResponseASN responseASN)
        {
            string strReturn = string.Empty;
            //ResponseASN response = new ResponseASN();
            responseASN.Success = false;
            responseASN.Error = string.Empty;
            responseASN.Info = string.Empty;
            try
            {
                //var json = JsonConvert.SerializeObject(responseASN);
                //HttpContent httpContent = new StringContent(json);
                //httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //var response = await client.PostAsync(Uri_POST_ASN_CreateStoreIn, httpContent);
                //if (response.IsSuccessStatusCode)
                //{
                //    //var jwt = await response.Content.ReadAsStringAsync();
                //    //var reply = JsonConvert.DeserializeObject<ResponseASN>(jwt);
                //    //responseASN = reply.Copy();
                //}

                var json = JsonConvert.SerializeObject(responseASN);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync(Uri_POST_ASN_CreateStoreIn, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine("Antwortinhalt: " + responseContent);
                    var reply = JsonConvert.DeserializeObject<ResponseASN>(responseContent);
                    //Console.WriteLine($"Info: {reply.Info}, Error: {reply.Error}");
                    responseASN = reply.Copy();
                }
                else
                {
                    string error = string.Empty;
                    error += $"HTTP-Fehler: {response.StatusCode}" + Environment.NewLine;
                    if (responseASN.AsnArticle != null && responseASN.AsnArticle.AsnId > 0)
                    {
                        error += $"Der Eingang zu ASN-ID: {responseASN.AsnArticle.AsnId}";
                        error += $" konnte nicht erstellt werden! " + Environment.NewLine;
                    }
                    responseASN.Error = error;
                }
            }
            catch (Exception ex)
            {
                //string mes = ex.InnerException.Message;
                responseASN.Error = ex.InnerException?.Message ?? ex.Message;
            }
            return responseASN;
        }
    }
}
