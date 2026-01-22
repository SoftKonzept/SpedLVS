using Common.ApiModels;
using LVS.ViewData;
using LvsMobileAPI.DataConnection;

namespace LvsMobileAPI.Services
{
    public interface IVehicleService
    {
        public ResponseVehicle GET_VehicleList();

    }


    public class VehicleService : IVehicleService
    {
        private SvrSettings srv;

        public VehicleService()
        {
            srv = new SvrSettings();
        }

        public ResponseVehicle GET_VehicleList()
        {
            ResponseVehicle resVehicle = new ResponseVehicle();
            resVehicle.Success = false;

            VehicleViewData vehicleVD = new VehicleViewData();
            try
            {
                vehicleVD.GetVehicleList();
                resVehicle.ListVehicles = vehicleVD.ListVehicles;
                resVehicle.Success = true;
            }
            catch (Exception ex)
            {
                resVehicle.Success = false;
                resVehicle.Error = ex.Message;
            }
            return resVehicle;
        }







    }
}
