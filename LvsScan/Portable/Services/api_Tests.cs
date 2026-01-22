using LvsScan.Portable.Enumerations;
using LvsScan.Portable.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LvsScan.Portable.Services
{
    public class api_Tests : apiSetting
    {
        public api_Tests(enumHTTPMethodeType HttpType)
        {
            this.client = InitHttpClient(Device.RuntimePlatform, HttpType);
        }

        //----------------------------------------- Westerforecast ---------------------------------------------
        // Check Connection to Webserver

        /// <summary>
        ///             https://127.0.0.1:90/WeatherForecast
        ///             https://192.168.1.55/WeatherForecast
        /// </summary>
        private Uri uri_WeatherForecast_List;
        public Uri Uri_WeatherForecast_List
        {
            get
            {
                string tmp = ServerUrl + "/WeatherForecast";
                uri_WeatherForecast_List = new Uri(tmp);
                return uri_WeatherForecast_List;
            }
        }
        public async Task<List<WeatherForecast>> GetWeatherForecastList()
        {
            List<WeatherForecast> forecast = new List<WeatherForecast>();
            try
            {
                var response = await client.GetStringAsync(Uri_WeatherForecast_List);
                forecast = JsonConvert.DeserializeObject<List<WeatherForecast>>(response);
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
            }
            return forecast;
        }




        /// <summary>
        ///             https://127.0.0.1:90/api/TestConnection/GetDBConnectionInfo
        ///             https://192.168.1.55/api/TestConnection/GetDBConnectionInfo
        /// </summary>
        private Uri uri_DbConnecstionInfoIntern;
        public Uri Uri_DbConnecstionInfoIntern
        {
            get
            {
                string tmp = ServerUrl + "/api/TestConnection/GetDBConnectionInfo";
                uri_DbConnecstionInfoIntern = new Uri(tmp);
                return uri_DbConnecstionInfoIntern;
            }
        }
        public async Task<string> GetDBConnectionInfoIntern()
        {
            string strReturn = string.Empty;
            try
            {
                string tmpUri = Uri_DbConnecstionInfoIntern.ToString();
                var response = await client.GetStringAsync(tmpUri);
                //var response = await this.client.GetAsync(tmpUri);
                //var response = await this.client.GetAsync

                strReturn = response.ToString();

                //strReturn = JsonConvert.DeserializeObject<String>(response.ToString());
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
            }
            if (strReturn == string.Empty)
            {
                strReturn = "keine Verbindung möglich!";
            }
            return strReturn;
        }




        public async Task<string> GetDBConnectionInfoExtern()
        {
            string strReturn = string.Empty;
            try
            {
                //string tmpUri = Uri_DbConnecstionInfo.ToString();

                string tmpUri = $"https://www.comtec-web.de/MobileAPI/api/EventLocation/GetEventLocationList";

                var response = await client.GetStringAsync(tmpUri);
                //var response = await this.client.GetAsync(tmpUri);
                //var response = await this.client.GetAsync

                strReturn = response.ToString();

                //strReturn = JsonConvert.DeserializeObject<String>(response.ToString());
            }
            catch (Exception ex)
            {
                string mes = ex.InnerException.Message;
            }
            if (strReturn == string.Empty)
            {
                strReturn = "keine Verbindung möglich!";
            }
            return strReturn;
        }
    }
}
