using Common.Models;
using LvsScan.Portable.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LvsScan.Portable.ViewModels.Inventory
{
    public class i1_InventoryViewModel : BaseViewModel
    {
        public ServiceAPI serviceAPI;
        public i1_InventoryViewModel()
        {
            serviceAPI = new ServiceAPI();

        }

        private bool loadValues = false;
        public bool LoadValues
        {
            get { return loadValues; }
            set
            {
                loadValues = value;
                OnPropertyChanged();
                if (loadValues)
                {
                    Task.Run(() => GetInventoryList()).Wait();
                }
            }
        }

        public async Task GetInventoryList()
        {
            try
            {
                this.IsBusy = true;
                var result = await serviceAPI.GetInventroyList();
                Inventories = new ObservableCollection<Inventories>(result.ListInventories);
                this.IsBusy = false;

                if (!result.Success)
                {
                    await MessageService.ShowAsync("ACHTUNG", result.Error);
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        private ObservableCollection<Inventories> inventories = new ObservableCollection<Inventories>();
        public ObservableCollection<Inventories> Inventories
        {
            get { return inventories; }
            set { SetProperty(ref inventories, value); }
        }

        private int inventoriesCount;
        public int InventoriesCount
        {
            get { return inventoriesCount; }
            set { SetProperty(ref inventoriesCount, value); }
        }

        private Inventories selectedItem;
        public Inventories SelectedItem
        {
            get { return selectedItem; }
            set { SetProperty(ref selectedItem, value); }
        }

    }
}
