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

    public class api_Damage : apiSetting
    {
        public api_Damage(enumHTTPMethodeType HttpType)
        {
            client = InitHttpClient(Device.RuntimePlatform, HttpType);
        }
        public api_Damage()
        {
            client = InitHttpClient();
        }
        public api_Damage(Users myUser) : this()
        {
            LoggedUser = myUser.Copy();
        }

        /// <summary>
        ///             /api/Damage/GET_Damage/175
        /// </summary>
        public Uri Uri_GET_Damage_byId
        {
            get
            {
                string tmp = ServerUrl + "/api/Damage/GET_Damage/";
                Uri retUri = new Uri(tmp);
                return retUri;
            }
        }
        public async Task<ResponseDamage> GET_Damage_byId(int myId)
        {
            string strReturn = string.Empty;
            ResponseDamage responseDamage = new ResponseDamage();
            responseDamage.Success = false;
            try
            {
                string tmpUri = Uri_GET_Damage_byId.ToString() + myId.ToString();
                HttpResponseMessage response = await client.GetAsync(tmpUri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    responseDamage = JsonConvert.DeserializeObject<ResponseDamage>(content);
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
            }
            return responseDamage;
        }
        /// <summary>
        ///             /api/Damage/GET_Damage_List
        /// </summary>
        public Uri Uri_GET_DamageList
        {
            get
            {
                string tmp = ServerUrl + "/api/Damage/GET_Damage_List";
                Uri retUri = new Uri(tmp);
                return retUri;
            }
        }
        public async Task<ResponseDamage> GET_DamageList()
        {
            string strReturn = string.Empty;
            ResponseDamage resDamage = new ResponseDamage();
            resDamage.Success = false;
            try
            {
                HttpResponseMessage response = await client.GetAsync(Uri_GET_DamageList);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    resDamage = JsonConvert.DeserializeObject<ResponseDamage>(content);
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
            }
            return resDamage;
        }

        /// <summary>
        ///             /api/Damage/GET_Damage_DamageArticleList/{ArticleId}/{UserId}
        /// </summary>
        public Uri Uri_GET_Damage_ArticleDamageList
        {
            get
            {
                string tmp = ServerUrl + "/api/Damage/GET_Damage_DamageArticleList/";
                Uri retUri = new Uri(tmp);
                return retUri;
            }
        }
        public async Task<ResponseDamage> GET_DamageArticleList(int myArticleId, int myUserId)
        {
            string strReturn = string.Empty;
            ResponseDamage responseDamage = new ResponseDamage();
            responseDamage.Success = false;
            responseDamage.Error = string.Empty;
            responseDamage.Info = string.Empty;
            try
            {
                string tmpUri = Uri_GET_Damage_ArticleDamageList.ToString() + myArticleId;
                tmpUri += "/" + myUserId;
                HttpResponseMessage response = await client.GetAsync(tmpUri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    responseDamage = JsonConvert.DeserializeObject<ResponseDamage>(content);
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
            }
            return responseDamage;
        }

        ///<summary>
        ///                 /api/Damage/POST_Damage_Update
        ///</summary>
        public Uri Uri_POST_Damage_Update
        {
            get { return new Uri(ServerUrl + "/api/Damage/POST_Damage_Update"); }
        }
        public async Task<ResponseDamage> POST_Damage_Update(ResponseDamage responsDamage)
        {
            responsDamage.Success = false;
            responsDamage.Error = string.Empty;
            responsDamage.Info = string.Empty;
            try
            {
                var json = JsonConvert.SerializeObject(responsDamage);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(Uri_POST_Damage_Update, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    var jwt = await response.Content.ReadAsStringAsync();
                    var reply = JsonConvert.DeserializeObject<ResponseDamage>(jwt);
                    responsDamage = reply.Copy();
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
                responsDamage.Error = mes;
            }
            return responsDamage;
        }
        ///<summary>
        ///                 Update Article Scan Values
        ///                 /api/Damage/POST_Damage_AddDamageArticleAssignment
        ///</summary>
        public Uri Uri_POST_Damage_AddDamageArticleAssignment
        {
            get { return new Uri(ServerUrl + "/api/Damage/POST_Damage_AddDamageArticleAssignment"); }
        }
        public async Task<ResponseDamage> POST_Damage_AddDamageArticleAssignment(ResponseDamage responsDamage)
        {
            responsDamage.Success = false;
            responsDamage.Error = string.Empty;
            responsDamage.Info = string.Empty;
            try
            {
                var json = JsonConvert.SerializeObject(responsDamage);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(Uri_POST_Damage_AddDamageArticleAssignment, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    var jwt = await response.Content.ReadAsStringAsync();
                    var reply = JsonConvert.DeserializeObject<ResponseDamage>(jwt);
                    responsDamage = reply.Copy();
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
                responsDamage.Error = mes;
            }
            return responsDamage;
        }



        ///-----------------------------------------------------------------------------------------------------------------
        ///                                             DELETE
        /// ----------------------------------------------------------------------------------------------------------------
        ///<summary>
        ///                 /api/Damage/DELETE_Damage_DamageArticleAssignmentItem/3160/1
        ///</summary>
        public Uri Uri_DELETE_Damage_DamageArticleAssignmentItem
        {
            get { return new Uri(ServerUrl + "/api/Damage/DELETE_Damage_DamageArticleAssignmentItem/"); }
        }
        public async Task<ResponseDamage> DELETE_Damage_DamageArticleAssignmentItem(ResponseDamage responsDamage)
        {
            responsDamage.Success = false;
            responsDamage.Error = string.Empty;
            responsDamage.Info = string.Empty;
            try
            {
                string tmpUri = Uri_DELETE_Damage_DamageArticleAssignmentItem.ToString() + responsDamage.DamageArticleAssignment.Id;
                tmpUri += "/" + responsDamage.UserId;
                HttpResponseMessage response = await client.DeleteAsync(tmpUri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    responsDamage = JsonConvert.DeserializeObject<ResponseDamage>(content);
                }
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
            }
            return responsDamage;
        }
    }
}
