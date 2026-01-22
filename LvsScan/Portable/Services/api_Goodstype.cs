using Common.ApiModels;
using LvsScan.Portable.Enumerations;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LvsScan.Portable.Services
{

    public class api_Goodstype : apiSetting
    {
        public api_Goodstype(enumHTTPMethodeType HttpType)
        {
            client = InitHttpClient(Device.RuntimePlatform, HttpType);
        }
        public api_Goodstype()
        {
            client = InitHttpClient();
        }

        /// <summary>
        ///             /api/GoodsType/GET_GoodsType/1
        /// </summary>
        public Uri Uri_GET_GoodsType_byId
        {
            get
            {
                string tmp = ServerUrl + "/api/GoodsType/GET_GoodsType/";
                Uri retUri = new Uri(tmp);
                return retUri;
            }
        }
        public async Task<ResponseGoodstype> GET_GoodsType_ById(int goodstypdeId)
        {
            string strReturn = string.Empty;
            ResponseGoodstype resGut = new ResponseGoodstype();
            resGut.Success = false;
            try
            {
                string tmpUri = Uri_GET_GoodsType_byId.ToString() + goodstypdeId.ToString();
                HttpResponseMessage response = await client.GetAsync(tmpUri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    resGut = JsonConvert.DeserializeObject<ResponseGoodstype>(content);
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
            }
            return resGut;
        }

        /// <summary>
        ///             /api/GoodsType/GET_GoodstypelistByWorkspace/
        /// </summary>
        public Uri Uri_GET_GoodstypelistByWorkspace
        {
            get
            {
                string tmp = ServerUrl + "/api/GoodsType/GET_GoodstypelistByWorkspace/";
                Uri retUri = new Uri(tmp);
                return retUri;
            }
        }
        public async Task<ResponseGoodstype> GET_GoodstypelistByWorkspace(int workspaceId)
        {
            string strReturn = string.Empty;
            ResponseGoodstype resWork = new ResponseGoodstype();
            resWork.Success = false;

            try
            {
                //var json = JsonConvert.SerializeObject(resAdr);
                //HttpContent httpContent = new StringContent(json);
                //httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string strUri = Uri_GET_GoodstypelistByWorkspace.ToString() + workspaceId.ToString();

                HttpResponseMessage response = await client.GetAsync(strUri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    resWork = JsonConvert.DeserializeObject<ResponseGoodstype>(content);
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
                resWork.Error = mes;
            }
            return resWork;
        }

        /// <summary>
        ///             /api/GoodsType/GET_GoodstypelistByWorkspace/1/234
        /// </summary>
        public Uri Uri_GET_GoodstypelistByWorkspaceAndAddress
        {
            get
            {
                string tmp = ServerUrl + "/api/GoodsType/GET_GoodstypelistByWorkspaceAndAddress/";
                Uri retUri = new Uri(tmp);
                return retUri;
            }
        }
        public async Task<ResponseGoodstype> GET_GoodstypelistByWorkspaceAndAddress(int workspaceId, int addressId)
        {
            string strReturn = string.Empty;
            ResponseGoodstype resWork = new ResponseGoodstype();
            resWork.Success = false;

            try
            {
                string strUri = Uri_GET_GoodstypelistByWorkspace.ToString();
                strUri += workspaceId.ToString();
                strUri += "/" + addressId.ToString();

                HttpResponseMessage response = await client.GetAsync(strUri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    resWork = JsonConvert.DeserializeObject<ResponseGoodstype>(content);
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
                resWork.Error = mes;
            }
            return resWork;
        }

        /// <summary>
        ///             /api/GoodsType/GET_GoodstypeByWorkspaceAndAddressAndWerksnummer/{WorkspaceId}/{AddressId}/{Werksnummer}/1/234/EVB sskk
        /// </summary>
        public Uri Uri_GET_GoodstypelistByWorkspaceAndAddressAndWerksnummer
        {
            get
            {
                string tmp = ServerUrl + "/api/GoodsType/GET_GoodstypeByWorkspaceAndAddressAndWerksnummer/";
                Uri retUri = new Uri(tmp);
                return retUri;
            }
        }
        public async Task<ResponseGoodstype> GET_GoodstypelistByWorkspaceAndAddressAndWerksnummer(int workspaceId, int addressId, string werksnummer)
        {
            string strReturn = string.Empty;
            ResponseGoodstype resGut = new ResponseGoodstype();
            resGut.Success = false;

            try
            {
                string strUri = Uri_GET_GoodstypelistByWorkspaceAndAddressAndWerksnummer.ToString();
                strUri += workspaceId.ToString();
                strUri += "/" + addressId.ToString();
                strUri += "/" + werksnummer;

                HttpResponseMessage response = await client.GetAsync(strUri);
                //if (response.IsSuccessStatusCode)
                //{
                //    var content = await response.Content.ReadAsStringAsync();
                //    resGut = JsonConvert.DeserializeObject<ResponseGoodstype>(content);
                //}
                resGut.Success = response.IsSuccessStatusCode;
                var content = await response.Content.ReadAsStringAsync();
                resGut = JsonConvert.DeserializeObject<ResponseGoodstype>(content);
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
                resGut.Error = mes;
            }
            return resGut;
        }


    }
}
