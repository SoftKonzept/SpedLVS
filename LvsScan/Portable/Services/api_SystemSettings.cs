using Common.ApiModels;
using LvsScan.Portable.Enumerations;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LvsScan.Portable.Services
{
    public class api_SystemSettings : apiSetting
    {
        public api_SystemSettings(enumHTTPMethodeType HttpType)
        {
            this.client = InitHttpClient(Device.RuntimePlatform, HttpType);
        }

        //----------------------------------------- Westerforecast ---------------------------------------------
        // Check Connection to Webserver

        /// <summary>
        ///             https://127.0.0.1:90/WeatherForecast
        ///             https://192.168.1.55/WeatherForecast
        /// </summary>
        private Uri uri_Setting_GET_Printerlist;
        public Uri Uri_Setting_GET_Printerlist
        {
            get
            {
                string tmp = ServerUrl + "/api/Setting/GET_Printerlist";
                uri_Setting_GET_Printerlist = new Uri(tmp);
                return uri_Setting_GET_Printerlist;
            }
        }
        public async Task<ResponseSettings> GET_Printerlist()
        {
            ResponseSettings res = new ResponseSettings();
            try
            {
                var response = await client.GetStringAsync(Uri_Setting_GET_Printerlist);
                res = JsonConvert.DeserializeObject<ResponseSettings>(response);
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
                res.Error = mes;
            }
            return res;
        }

    }
}
