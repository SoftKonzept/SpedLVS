using System;

namespace LvsScan.Portable.ViewModels.Test
{
    public class DbTestInventoryViewModel : BaseViewModel
    {
        public DbTestInventoryViewModel()
        {
            serviceAPI = new Services.ServiceAPI();
        }
        internal Services.ServiceAPI serviceAPI;

        public Common.Models.Inventories _Result;
        public Common.Models.Inventories Result
        {
            get { return _Result; }
            set { SetProperty(ref _Result, value); }
        }
        public async void GetResult()
        {
            try
            {
                IsBusy = true;
                var res = await serviceAPI.GetInventroy(1, 0);
                Result = res.Inventory.Copy();
                IsBusy = false;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
    }
}
