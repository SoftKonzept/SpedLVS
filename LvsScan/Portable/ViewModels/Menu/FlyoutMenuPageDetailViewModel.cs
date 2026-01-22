using LvsScan.Portable.Models;
using LvsScan.Portable.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LvsScan.Portable.ViewModels.Menu
{
    public class FlyoutMenuPageDetailViewModel : BaseViewModel
    {
        public FlyoutMenuPageDetailViewModel()
        {
            serviceAPI = new ServiceAPI();
        }
        public ServiceAPI serviceAPI;

        public async void GetInventoryArticleList()
        {
            try
            {
                List<InventoryArticlePageViewCls> list = new List<InventoryArticlePageViewCls>();
                InventoryArticles = new ObservableCollection<InventoryArticlePageViewCls>(list);
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
        private ObservableCollection<InventoryArticlePageViewCls> inventoryArticles;
        public ObservableCollection<InventoryArticlePageViewCls> InventoryArticles
        {
            get
            {
                return inventoryArticles;
            }
            set
            {
                inventoryArticles = value;
                OnPropertyChanged();
            }
        }
    }
}
