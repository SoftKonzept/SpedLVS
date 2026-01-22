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

    public class api_Image : apiSetting
    {
        public api_Image(enumHTTPMethodeType HttpType)
        {
            client = InitHttpClient(Device.RuntimePlatform, HttpType);
        }
        public api_Image()
        {
            client = InitHttpClient();
        }
        public api_Image(Users myUser) : this()
        {
            LoggedUser = myUser.Copy();
        }

        ///<summary>
        ///                 /api/Damage/POST_Damage_Update
        ///</summary>
        public Uri Uri_Post_Image_Add
        {
            get { return new Uri(ServerUrl + "/api/Image/POST_Image_Add"); }
        }
        public async Task<ResponseImage> Post_Image_Add(ResponseImage respons)
        {
            respons.Success = false;
            respons.Error = string.Empty;
            respons.Info = string.Empty;
            try
            {
                var json = JsonConvert.SerializeObject(respons);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var res = await client.PostAsync(Uri_Post_Image_Add, httpContent);
                if (res.IsSuccessStatusCode)
                {
                    var jwt = await res.Content.ReadAsStringAsync();
                    var reply = JsonConvert.DeserializeObject<ResponseImage>(jwt);
                    respons = reply.Copy();
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
                respons.Error = mes;
            }
            return respons;
        }
    }
}
