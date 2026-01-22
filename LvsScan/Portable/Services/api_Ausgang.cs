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

    public class api_Ausgang : apiSetting
    {
        public api_Ausgang(enumHTTPMethodeType HttpType)
        {
            client = InitHttpClient(Device.RuntimePlatform, HttpType);
        }
        public api_Ausgang()
        {
            client = InitHttpClient();
        }
        public api_Ausgang(Users myUser) : this()
        {
            //client = InitHttpClient();
            LoggedUser = myUser.Copy();
        }

        /// <summary>
        ///             /api/Ausgang/GET_Ausgang/67455
        /// </summary>
        public Uri Uri_GET_Ausgang_byId
        {
            get
            {
                string tmp = ServerUrl + "/api/Ausgang/GET_Ausgang/";
                Uri retUri = new Uri(tmp);
                return retUri;
            }
        }
        public async Task<ResponseAusgang> GET_Ausgang_byId(int myId)
        {
            string strReturn = string.Empty;
            ResponseAusgang resAusgang = new ResponseAusgang();
            resAusgang.Success = false;
            try
            {
                string tmpUri = Uri_GET_Ausgang_byId.ToString() + myId.ToString();
                HttpResponseMessage response = await client.GetAsync(tmpUri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    resAusgang = JsonConvert.DeserializeObject<ResponseAusgang>(content);
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
            }
            return resAusgang;
        }

        /// <summary>
        ///             /api/Ausgang/GET_Ausgang/
        /// </summary>
        public Uri Uri_GET_Ausgang_byLvsNr
        {
            get
            {
                string tmp = ServerUrl + "/api/Ausgang/GET_AusgangByLvsNr/";
                Uri retUri = new Uri(tmp);
                return retUri;
            }
        }
        public async Task<ResponseAusgang> GET_Ausgang_ByLvsNr(int myLvsNr)
        {
            string strReturn = string.Empty;
            ResponseAusgang resAusgang = new ResponseAusgang();
            resAusgang.Success = false;
            try
            {
                string tmpUri = Uri_GET_Ausgang_byLvsNr.ToString() + myLvsNr.ToString();
                HttpResponseMessage response = await client.GetAsync(tmpUri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    resAusgang = JsonConvert.DeserializeObject<ResponseAusgang>(content);
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
            }
            return resAusgang;
        }




        /// <summary>
        ///             get list of open store out
        ///             /api/Call/GET_CallList_Open
        /// </summary>
        public Uri Uri_GET_AusgangList_Open
        {
            get
            {
                string tmp = ServerUrl + "/api/Ausgang/GET_AusgangList_Open";
                Uri retUri = new Uri(tmp);
                return retUri;
            }
        }
        public async Task<ResponseAusgang> GET_AusgangList_Open()
        {
            string strReturn = string.Empty;
            ResponseAusgang resAusgang = new ResponseAusgang();
            resAusgang.Success = false;
            try
            {
                HttpResponseMessage response = await client.GetAsync(Uri_GET_AusgangList_Open);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    resAusgang = JsonConvert.DeserializeObject<ResponseAusgang>(content);
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
            }
            return resAusgang;
        }

        ///<summary>
        ///                 Update Article Scan Values
        ///                 /api/Ausgang/POST_Ausgang_Update_WizStoreOut
        ///</summary>
        public Uri Uri_POST_Ausgang_Update_WizStoreOut
        {
            get { return new Uri(ServerUrl + "/api/Ausgang/POST_Ausgang_Update_WizStoreOut"); }
        }
        public async Task<ResponseAusgang> POST_Ausgang_Update_WizStoreOut(ResponseAusgang responsStoreOut)
        {
            responsStoreOut.Success = false;
            responsStoreOut.Error = string.Empty;
            responsStoreOut.Info = string.Empty;
            try
            {
                var json = JsonConvert.SerializeObject(responsStoreOut);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(Uri_POST_Ausgang_Update_WizStoreOut, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    var jwt = await response.Content.ReadAsStringAsync();
                    var reply = JsonConvert.DeserializeObject<ResponseAusgang>(jwt);
                    responsStoreOut = reply.Copy();
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
                responsStoreOut.Error = mes;
            }
            return responsStoreOut;
        }

    }
}
