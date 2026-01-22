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

    public class api_Call : apiSetting
    {

        public api_Call()
        {
            client = InitHttpClient();
        }
        public api_Call(Users myUser) : this()
        {
            LoggedUser = myUser.Copy();
        }
        public api_Call(enumHTTPMethodeType HttpType)
        {
            client = InitHttpClient(Device.RuntimePlatform, HttpType);
        }

        /// <summary>
        ///             /api/Call/GET_Call/54374
        /// </summary>
        public Uri Uri_GET_Call_byId
        {
            get
            {
                string tmp = ServerUrl + "/api/Call/GET_Call/";
                Uri retUri = new Uri(tmp);
                return retUri;
            }
        }
        public async Task<ResponseCall> GET_Call_byId(int myId)
        {
            string strReturn = string.Empty;
            ResponseCall resCall = new ResponseCall();
            resCall.Success = false;
            try
            {
                string tmpUri = Uri_GET_Call_byId.ToString() + myId.ToString();
                HttpResponseMessage response = await client.GetAsync(tmpUri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    resCall = JsonConvert.DeserializeObject<ResponseCall>(content);
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
            }
            return resCall;
        }

        /// <summary>
        ///             GET_CallByLVSNr/{LVSNr}/{UserId}
        ///            api/Call/GET_CallByLVSNr/244591/1
        /// </summary>
        public Uri Uri_GET_Call_byLVSNr
        {
            get
            {
                string tmp = ServerUrl + "/api/Call/GET_CallByLVSNr/";
                Uri retUri = new Uri(tmp);
                return retUri;
            }
        }
        public async Task<ResponseCall> GET_Call_byLVSNr(int myLVSNr)
        {
            string strReturn = string.Empty;
            ResponseCall resCall = new ResponseCall();
            resCall.Success = false;
            try
            {
                string tmpUri = Uri_GET_Call_byLVSNr.ToString() + myLVSNr.ToString();
                tmpUri += "/" + ((int)this.LoggedUser.Id).ToString();

                HttpResponseMessage response = await client.GetAsync(tmpUri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    resCall = JsonConvert.DeserializeObject<ResponseCall>(content);
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
            }
            return resCall;
        }

        ///<summary>
        ///                 list of open calls
        ///                 /api/Call/GET_CallList_Open
        ///</summary>
        public Uri Uri_GET_CallList_Open
        {
            get { return new Uri(ServerUrl + "/api/Call/GET_CallList_Open"); }
        }
        public async Task<ResponseCall> GET_CallList_Open()
        {
            ResponseCall responseCall = new ResponseCall();
            try
            {
                var response = await client.GetAsync(Uri_GET_CallList_Open).ConfigureAwait(true);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResponseCall>(content);
                    responseCall = result.Copy();
                }
            }
            catch (Exception ex)
            {
                responseCall.Error = ex.InnerException.Message;
            }
            return responseCall;
        }
        ///<summary>
        ///                 Update Scan Values
        ///                 /api/Call/POST_Call_Update_WizStoreOut
        ///</summary>
        public Uri Uri_POST_Call_Update_ScanValue
        {
            get { return new Uri(ServerUrl + "/api/Call/POST_Call_Update_WizStoreOut"); }
        }
        public async Task<ResponseCall> POST_Call_Update_WizStoreOut(ResponseCall responsCall)
        {
            responsCall.Success = false;
            responsCall.Error = string.Empty;
            responsCall.Info = string.Empty;

            try
            {
                var json = JsonConvert.SerializeObject(responsCall);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(Uri_POST_Call_Update_ScanValue, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    var jwt = await response.Content.ReadAsStringAsync();
                    var reply = JsonConvert.DeserializeObject<ResponseCall>(jwt);
                    responsCall = reply.Copy();
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
                responsCall.Error = mes;
            }
            return responsCall;
        }

        ///<summary>
        ///                 Update Scan Values
        ///                 /api/Call/POST_Call_CreateStoreOut
        ///</summary>
        public Uri Uri_POST_Call_CreateStoreOut
        {
            get { return new Uri(ServerUrl + "/api/Call/POST_Call_CreateStoreOut"); }
        }
        public async Task<ResponseCall> POST_Call_CreateStoreOut(ResponseCall responsCall)
        {
            responsCall.Success = false;
            responsCall.Error = string.Empty;
            responsCall.Info = string.Empty;

            try
            {
                var json = JsonConvert.SerializeObject(responsCall);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(Uri_POST_Call_CreateStoreOut, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    var jwt = await response.Content.ReadAsStringAsync();
                    var reply = JsonConvert.DeserializeObject<ResponseCall>(jwt);
                    responsCall = reply.Copy();
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
                responsCall.Error = mes;
            }
            return responsCall;
        }



    }
}
