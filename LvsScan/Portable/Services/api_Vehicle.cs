using Common.ApiModels;
using Common.Models;
using LvsScan.Portable.Enumerations;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LvsScan.Portable.Services
{

    public class api_Vehicle : apiSetting
    {

        public api_Vehicle()
        {
            client = InitHttpClient();
        }
        public api_Vehicle(Users myUser) : this()
        {
            LoggedUser = myUser.Copy();
        }
        public api_Vehicle(enumHTTPMethodeType HttpType)
        {
            client = InitHttpClient(Device.RuntimePlatform, HttpType);
        }

        ///<summary>
        ///                 list of all Vehicle
        ///                 /api/Vehicle/GET_VehicleList
        ///</summary>
        public Uri Uri_GET_VehicleList
        {
            get { return new Uri(ServerUrl + "/api/Vehicle/GET_VehicleList"); }
        }
        public async Task<ResponseVehicle> GET_VehicleList()
        {
            ResponseVehicle responseVehicle = new ResponseVehicle();
            try
            {
                var response = await client.GetAsync(Uri_GET_VehicleList).ConfigureAwait(true);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResponseVehicle>(content);
                    responseVehicle = result.Copy();
                }
            }
            catch (Exception ex)
            {
                responseVehicle.Error = ex.InnerException.Message;
            }
            return responseVehicle;
        }


    }
}
