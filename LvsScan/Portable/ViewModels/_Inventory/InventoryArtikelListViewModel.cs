using Common.Models;
using LvsScan.Portable.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LvsScan.Portable.ViewModels.Inventory
{
    [QueryProperty(nameof(Content), nameof(Content))]
    public class InventoryArtikelListViewModel : BaseViewModel
    {
        public InventoryArtikelListViewModel()
        {
            serviceAPI = new ServiceAPI();
            
        }
        public ServiceAPI serviceAPI;
        string content = string.Empty;
        public string Content
        {
            get { return content; }
            set
            {
                content = Uri.UnescapeDataString(value ?? string.Empty);
                if (content != string.Empty)
                {
                    var resContent = JsonConvert.DeserializeObject<Inventories>(content);
                    this.SelectedInventory = (Inventories)resContent;
                }
                OnPropertyChanged();
            }
        }

        public Inventories selectedInventory;
        public Inventories SelectedInventory
        {
            get
            {
                return selectedInventory;
            }
            set
            {
                selectedInventory = value;
                OnPropertyChanged();
                GetInventoryArticleList();
            }
        }

        public InventoryArticles selectedInventoryArticle;
        public InventoryArticles SelectedInventoryArticle
        {
            get
            {
                return selectedInventoryArticle;
            }
            set
            {
                selectedInventoryArticle = value;
                OnPropertyChanged();
            }
        }

        public async Task GetInventoryArticleList()
        {
            if (this.SelectedInventory is Inventories)
            {
                try
                {
                    List<InventoryArticles> inventoryArticleList = await serviceAPI.GetInventroyArticleList(this.SelectedInventory.Id);
                    InventoryArticles = new ObservableCollection<InventoryArticles>(inventoryArticleList);
                }
                catch (Exception ex)
                {
                    string str = ex.Message;
                }
            }
            else
            {
                InventoryArticles = new ObservableCollection<InventoryArticles>();
            }
        }

        private ObservableCollection<InventoryArticles> inventoryArticles;
        public ObservableCollection<InventoryArticles> InventoryArticles
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

        private ObservableCollection<InventoryArticlePageViewCls> dataList;
        public ObservableCollection<InventoryArticlePageViewCls> DataList
        {
            get
            {
                //if (inventoryArticles == null)
                //{
                //    InventoryArticles = new ObservableCollection<InventoryArticles>();
                //}
                //List<Data> tmp = new List<Data>
                //                         {
                //                            new Data { Country = "India", Capital = "New Delhi"},
                //                            new Data { Country = "South Africa", Capital = "Cape Town"},
                //                            new Data { Country = "Nigeria", Capital = "Abuja" },
                //                            new Data { Country = "Singapore", Capital = "Singapore" }
                //                         };
                //dataList = new ObservableCollection<Data>(tmp);
                return dataList;
            }
            set
            {
                dataList = value;
                OnPropertyChanged();
            }
        }

        public class Data
        {
            public string Country { get; set; }

            public string Capital { get; set; }
        }
    }
}
