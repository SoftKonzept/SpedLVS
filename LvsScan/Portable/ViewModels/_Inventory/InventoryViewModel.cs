using LvsScan.Portable.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Common.Models;
using System.Linq;
using System.Threading.Tasks;

namespace LvsScan.Portable.ViewModels.Inventory
{
    public class InventoryViewModel : BaseViewModel
    {
        public InventoryViewModel()
        {
            serviceAPI = new ServiceAPI();
            GetInventoryList();
        }
        public ServiceAPI serviceAPI;
        public async void GetInventoryList()
        {
            List<Inventories> inventoryList = await serviceAPI.GetInventroyList();
            Inventories = new ObservableCollection<Inventories>(inventoryList.ToList());
        }

        private ObservableCollection<Inventories> inventories;
        public ObservableCollection<Inventories> Inventories
        {
            get
            {
                if (inventories == null)
                {
                    Inventories = new ObservableCollection<Inventories>();
                }
                return inventories;
            }
            set
            {
                inventories = value;
                OnPropertyChanged();
            }
        }

        private Inventories selectedItem;
        public Inventories SelectedItem
        {
            get
            {
                if (inventories == null)
                {
                    Inventories = new ObservableCollection<Inventories>();
                }
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }

    }
}
