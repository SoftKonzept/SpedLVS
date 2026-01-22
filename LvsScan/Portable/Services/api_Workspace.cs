using Common.ApiModels;
using LvsScan.Portable.Enumerations;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LvsScan.Portable.Services
{

    public class api_Workspace : apiSetting
    {
        public api_Workspace(enumHTTPMethodeType HttpType)
        {
            client = InitHttpClient(Device.RuntimePlatform, HttpType);
        }
        public api_Workspace()
        {
            client = InitHttpClient();
        }

        /// <summary>
        ///             /api/Workspace/GET_Workspace/67455
        /// </summary>
        public Uri Uri_GET_Workspace_byId
        {
            get
            {
                string tmp = ServerUrl + "/api/Workspace/GET_Workspace/";
                Uri retUri = new Uri(tmp);
                return retUri;
            }
        }
        public async Task<ResponseWorkspace> GET_Workspace_ById(int workspaceId)
        {
            string strReturn = string.Empty;
            ResponseWorkspace resWork = new ResponseWorkspace();
            resWork.Success = false;
            try
            {
                //var strinSer = JsonConvert.SerializeObject(myArticleSearch);
                string tmpUri = Uri_GET_Workspace_byId.ToString() + workspaceId.ToString();
                HttpResponseMessage response = await client.GetAsync(tmpUri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    resWork = JsonConvert.DeserializeObject<ResponseWorkspace>(content);
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
            }
            return resWork;
        }

        /// <summary>
        ///             /api/Workspace/GET_Workspacelist
        /// </summary>
        public Uri Uri_GET_Workspacelist
        {
            get
            {
                string tmp = ServerUrl + "/api/Workspace/GET_Workspacelist";
                Uri retUri = new Uri(tmp);
                return retUri;
            }
        }
        public async Task<ResponseWorkspace> GET_Workspacelist()
        {
            string strReturn = string.Empty;
            ResponseWorkspace resWork = new ResponseWorkspace();
            resWork.Success = false;

            try
            {
                //var json = JsonConvert.SerializeObject(resAdr);
                //HttpContent httpContent = new StringContent(json);
                //httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string strUri = Uri_GET_Workspacelist.ToString();

                HttpResponseMessage response = await client.GetAsync(strUri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    resWork = JsonConvert.DeserializeObject<ResponseWorkspace>(content);
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
                resWork.Error = mes;
            }
            return resWork;
        }


    }
}
