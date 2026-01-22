using Common.ApiModels;
using LvsScan.Portable.Enumerations;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LvsScan.Portable.Services
{
    public class api_Login : apiSetting
    {
        public api_Login(enumHTTPMethodeType HttpType)
        {
            this.client = InitHttpClient(Device.RuntimePlatform, HttpType);
        }
        public api_Login()
        {
            this.client = InitHttpClient();
        }


        /// <summary>
        ///             /api/Login
        /// </summary>
        private Uri uri_Login;
        public Uri Uri_Login
        {
            get { return new Uri(ServerUrl + "/api/Login"); }
        }
        public async Task<ResponseLogin> Login(UserLogin myLogin)
        {
            //// initialization
            ResponseLogin resLog = new ResponseLogin();
            resLog.AccessGranted = false;
            resLog.Error = string.Empty;
            resLog.AccessToken = string.Empty;

            try
            {
                var jsonLogin = JsonConvert.SerializeObject(myLogin);
                HttpContent httpContent = new StringContent(jsonLogin);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(Uri_Login, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    var jwt = await response.Content.ReadAsStringAsync();
                    var reply = JsonConvert.DeserializeObject<ResponseLogin>(jwt);
                    resLog = reply.Copy();
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
                resLog.Error = "Zu den Eingabedaten konnte kein User ermittelt werden." + Environment.NewLine;
                resLog.Error += "Exception: " + ex.InnerException.Message.ToString();
            }
            return resLog;
        }





    }
}
