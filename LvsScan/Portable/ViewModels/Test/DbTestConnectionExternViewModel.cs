using System;

namespace LvsScan.Portable.ViewModels.Test
{
    public class DbTestConnectionExternViewModel : BaseViewModel
    {
        public DbTestConnectionExternViewModel()
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
                string result = await serviceAPI.GetDBConnectionInfoExtern();

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
