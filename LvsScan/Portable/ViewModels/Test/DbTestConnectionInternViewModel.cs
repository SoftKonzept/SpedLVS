using System;

namespace LvsScan.Portable.ViewModels.Test
{
    public class DbTestConnectionInternViewModel : BaseViewModel
    {
        public DbTestConnectionInternViewModel()
        {
            serviceAPI = new Services.ServiceAPI();
        }
        internal Services.ServiceAPI serviceAPI;

        public string _ConnectionResult;
        public string ConnectionResult
        {
            get { return _ConnectionResult; }
            set { SetProperty(ref _ConnectionResult, value); }
        }
        public async void GetResult()
        {
            try
            {
                IsBusy = true;
                string result = await serviceAPI.GetDBConnectionInfoIntern();

                ConnectionResult = result;
                IsBusy = false;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
    }
}
