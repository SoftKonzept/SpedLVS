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
    public class api_User : apiSetting
    {
        public api_User(enumHTTPMethodeType HttpType)
        {
            this.client = InitHttpClient(Device.RuntimePlatform, HttpType);
        }

        /// <summary>
        ///             /api/User/GetCurrentUser
        /// </summary>
        private Uri uri_Login;
        public Uri Uri_Login
        {
            get
            {
                string tmp = ServerUrl + "/api/User/GetCurrentUser";
                uri_Login = new Uri(tmp);
                return uri_Login;
            }
        }
        public async Task<ResponseUser> GetCurrentUser()
        {
            //- initialization
            ResponseUser cUser = new ResponseUser();
            cUser.Error = String.Empty;
            cUser.IsSuccess = true;
            cUser.User = new Users();

            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(uri_Login);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var currentUser = JsonConvert.DeserializeObject<Users>(content);
                    cUser.User = currentUser;
                    cUser.IsSuccess = true;
                }
                else
                {
                    string error = response.StatusCode.ToString();
                    error += " - " + response.Headers.ToString();
                    cUser.Error = error;
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
                cUser.Error = mes;
            }
            return cUser;
        }
    }
}
