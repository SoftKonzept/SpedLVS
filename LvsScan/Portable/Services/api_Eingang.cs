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

    public class api_Eingang : apiSetting
    {
        public api_Eingang(enumHTTPMethodeType HttpType)
        {
            client = InitHttpClient(Device.RuntimePlatform, HttpType);
        }
        public api_Eingang()
        {
            client = InitHttpClient();
        }
        public api_Eingang(Users myUser) : this()
        {
            //client = InitHttpClient();
            LoggedUser = myUser.Copy();
        }

        /// <summary>
        ///             /api/Eingang/GET_Ausgang/67455
        /// </summary>
        public Uri Uri_GET_Eingang_byId
        {
            get
            {
                string tmp = ServerUrl + "/api/Eingang/GET_Eingang/";
                Uri retUri = new Uri(tmp);
                return retUri;
            }
        }
        public async Task<ResponseEingang> GET_Eingang_byId(int myId)
        {
            string strReturn = string.Empty;
            ResponseEingang resEingang = new ResponseEingang();
            resEingang.Success = false;
            resEingang.UserId = 1;
            try
            {
                string tmpUri = Uri_GET_Eingang_byId.ToString() + myId.ToString();
                HttpResponseMessage response = await client.GetAsync(tmpUri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    resEingang = JsonConvert.DeserializeObject<ResponseEingang>(content);
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
            }
            return resEingang;
        }
        /// <summary>
        ///             get list of open store out
        ///             /api/Call/GET_CallList_Open
        /// </summary>
        public Uri Uri_GET_EingangList_Open
        {
            get
            {
                string tmp = ServerUrl + "/api/Eingang/GET_EingangList_Open";
                Uri retUri = new Uri(tmp);
                return retUri;
            }
        }
        public async Task<ResponseEingang> GET_EingangList_Open()
        {
            string strReturn = string.Empty;
            ResponseEingang resEingang = new ResponseEingang();
            resEingang.Success = false;
            try
            {
                HttpResponseMessage response = await client.GetAsync(Uri_GET_EingangList_Open);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    resEingang = JsonConvert.DeserializeObject<ResponseEingang>(content);
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
            }
            return resEingang;
        }

        ///<summary>
        ///                 Update Article Scan Values
        ///                 /api/Eingang/POST_Eingang_Update_WizStoreIn
        ///</summary>
        public Uri Uri_POST_Eingang_Update_WizStoreIn
        {
            get { return new Uri(ServerUrl + "/api/Eingang/POST_Eingang_Update_WizStoreIn"); }
        }
        public async Task<ResponseEingang> POST_Eingang_Update_WizStoreIn(ResponseEingang resStoreIn)
        {
            resStoreIn.Success = false;
            resStoreIn.Error = string.Empty;
            resStoreIn.Info = string.Empty;
            try
            {
                var json = JsonConvert.SerializeObject(resStoreIn);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync(Uri_POST_Eingang_Update_WizStoreIn, httpContent);

                // immer den Body lesen, auch bei Fehlern
                var body = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var reply = JsonConvert.DeserializeObject<ResponseEingang>(body);
                        if (reply != null)
                            resStoreIn = reply.Copy();
                        else
                            resStoreIn.Error = "Leere oder ungültige Antwort vom Server.";
                    }
                    catch (Exception exDes)
                    {
                        resStoreIn.Error = "Deserialisierungsfehler: " + exDes.Message;
                        //System.Diagnostics.Debug.WriteLine("Deserialisierungsfehler POST_Eingang_Update_WizStoreIn: " + exDes.ToString());
                    }
                }
                else
                {
                    // setze aussagekräftige Fehlermeldung mit Status und Body
                    resStoreIn.Error = $"HTTP {(int)response.StatusCode} {response.ReasonPhrase}: {body}";
                    //System.Diagnostics.Debug.WriteLine("API-Fehler POST_Eingang_Update_WizStoreIn: " + resStoreIn.Error);
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                resStoreIn.Error = "Request-Fehler: " + mes;
                //System.Diagnostics.Debug.WriteLine("Exception POST_Eingang_Update_WizStoreIn: " + ex.ToString());
            }



            //--- alt

            //resStoreIn.Success = false;
            //resStoreIn.Error = string.Empty;
            //resStoreIn.Info = string.Empty;
            //try
            //{
            //    var json = JsonConvert.SerializeObject(resStoreIn);
            //    HttpContent httpContent = new StringContent(json);
            //    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //    var response = await client.PostAsync(Uri_POST_Eingang_Update_WizStoreIn, httpContent);
            //    string stringResponse = response.ToString();
            //    if (response.IsSuccessStatusCode)
            //    {                    
            //        var jwt = await response.Content.ReadAsStringAsync();
            //        var reply = JsonConvert.DeserializeObject<ResponseEingang>(jwt);
            //        resStoreIn = reply.Copy();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string mes = ex.InnerException.Message;
            //    resStoreIn.Error = mes;
            //}
            return resStoreIn;
        }

        ///<summary>
        ///                 Update Article Scan Values
        ///                 /api/Eingang/POST_Eingang_Add
        ///</summary>
        public Uri Uri_POST_Eingang_Add
        {
            get { return new Uri(ServerUrl + "/api/Eingang/POST_Eingang_Add"); }
        }
        public async Task<ResponseEingang> POST_Eingang_Add(ResponseEingang resStoreIn)
        {
            resStoreIn.Success = false;
            resStoreIn.Error = string.Empty;
            resStoreIn.Info = string.Empty;
            try
            {
                var json = JsonConvert.SerializeObject(resStoreIn);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(Uri_POST_Eingang_Add, httpContent);
                //if (response.IsSuccessStatusCode)
                //{
                //    var jwt = await response.Content.ReadAsStringAsync();
                //    var reply = JsonConvert.DeserializeObject<ResponseEingang>(jwt);
                //    resStoreIn = reply.Copy();
                //}
                var jwt = await response.Content.ReadAsStringAsync();
                var reply = JsonConvert.DeserializeObject<ResponseEingang>(jwt);
                resStoreIn = reply.Copy();
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
                resStoreIn.Error = mes;
            }
            return resStoreIn;
        }
    }
}
