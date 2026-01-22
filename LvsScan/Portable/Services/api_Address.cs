using Common.ApiModels;
using Common.Enumerations;
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

    public class api_Address : apiSetting
    {
        public api_Address()
        {
            client = InitHttpClient();
        }

        public api_Address(enumHTTPMethodeType HttpType)
        {
            client = InitHttpClient(Device.RuntimePlatform, HttpType);
        }
        public api_Address(Users myUser) : this()
        {
            //client = InitHttpClient();
            LoggedUser = myUser.Copy();
        }

        /// <summary>
        ///             /api/Address/GET_Address/67455
        /// </summary>
        public Uri Uri_GET_Address_byId
        {
            get
            {
                string tmp = ServerUrl + "/api/Address/GET_Address/";
                Uri retUri = new Uri(tmp);
                return retUri;
            }
        }
        public async Task<ResponseAddress> GET_Address_byId(int adrId)
        {
            string strReturn = string.Empty;
            ResponseAddress resAdr = new ResponseAddress();
            resAdr.UserId = (int)LoggedUser.Id;
            resAdr.Success = false;
            try
            {
                //var strinSer = JsonConvert.SerializeObject(myArticleSearch);
                string tmpUri = Uri_GET_Address_byId.ToString() + adrId.ToString();
                HttpResponseMessage response = await client.GetAsync(tmpUri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    resAdr = JsonConvert.DeserializeObject<ResponseAddress>(content);
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
            }
            return resAdr;
        }

        /// <summary>
        ///             /api/Address/GET_Addresslist/{myEnumAppProcessId}/{myWorkspaceId}
        ///             /api/Address/GET_Addresslist/3/1
        /// </summary>
        public Uri Uri_GET_Addresslist
        {
            get
            {
                string tmp = ServerUrl + "/api/Address/GET_Addresslist/";
                Uri retUri = new Uri(tmp);
                return retUri;
            }
        }
        public async Task<ResponseAddress> GET_Addresslist(enumAppProcess myAppProcess, int myWorkspaceId)
        {
            string strReturn = string.Empty;
            ResponseAddress resAdr = new ResponseAddress();
            resAdr.UserId = (int)LoggedUser.Id;
            resAdr.Success = false;
            resAdr.AppProcess = myAppProcess;
            try
            {
                //var json = JsonConvert.SerializeObject(resAdr);
                //HttpContent httpContent = new StringContent(json);
                //httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                int iTmpAppProcess = (int)resAdr.AppProcess;

                string strUri = Uri_GET_Addresslist.ToString() + iTmpAppProcess + "/" + myWorkspaceId;

                HttpResponseMessage response = await client.GetAsync(strUri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    resAdr = JsonConvert.DeserializeObject<ResponseAddress>(content);
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
            }
            return resAdr;
        }

        /// <summary>
        ///           /api/Address/GET_AdressSupplierNo
        /// </summary>
        public Uri Uri_Post_AdressSupplierNo
        {
            get
            {
                string tmp = ServerUrl + "api/Address/Post_AdressSupplierNo";
                Uri retUri = new Uri(tmp);
                return retUri;
            }
        }
        public async Task<ResponseAddress> GET_AdressSupplierNo(ResponseAddress resAdr)
        {
            string strReturn = string.Empty;
            resAdr.UserId = (int)LoggedUser.Id;
            resAdr.Success = false;

            try
            {
                var json = JsonConvert.SerializeObject(resAdr);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(Uri_Post_AdressSupplierNo, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    var jwt = await response.Content.ReadAsStringAsync();
                    var reply = JsonConvert.DeserializeObject<ResponseAddress>(jwt);
                    resAdr = reply.Copy();
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
            }
            return resAdr;
        }


    }
}
