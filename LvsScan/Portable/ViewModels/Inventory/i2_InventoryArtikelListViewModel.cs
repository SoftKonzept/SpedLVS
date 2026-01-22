using Common.Enumerations;
using Common.Models;
using LvsScan.Portable.Models;
using LvsScan.Portable.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LvsScan.Portable.ViewModels.Inventory
{
    //[QueryProperty(nameof(Content), nameof(Content))]
    public class i2_InventoryArtikelListViewModel : BaseViewModel
    {
        public i2_InventoryArtikelListViewModel()
        {
            serviceAPI = new ServiceAPI();
            IsInventoryStartEnabled = false;
        }
        public ServiceAPI serviceAPI;

        private bool loadValues = false;
        public bool LoadValues
        {
            get { return loadValues; }
            set
            {
                loadValues = value;
                if (loadValues)
                {
                    this.IsBusy = true;
                    GetInventoryArticleList();
                }
                OnPropertyChanged();
            }
        }
        private bool isInventoryStartEnabled = false;
        public bool IsInventoryStartEnabled
        {
            get { return isInventoryStartEnabled; }
            set { SetProperty(ref isInventoryStartEnabled, value); }
        }
        public Inventories selectedInventory;
        public Inventories SelectedInventory
        {
            get { return selectedInventory; }
            set { SetProperty(ref selectedInventory, value); }
        }

        public InventoryArticles selectedInventoryArticle;
        public InventoryArticles SelectedInventoryArticle
        {
            get { return selectedInventoryArticle; }
            set
            {
                SetProperty(ref selectedInventoryArticle, value);
                IsInventoryStartEnabled = ((selectedInventoryArticle is InventoryArticles) && (selectedInventoryArticle.Id > 0));
            }
        }

        public async void GetInventoryArticleList()
        {
            this.IsBusy = true;
            if (this.SelectedInventory is Inventories)
            {
                try
                {
                    var result = await serviceAPI.GetInventroyArticleList(this.SelectedInventory.Id);
                    InventoryArticles = new ObservableCollection<InventoryArticles>(result.ListInventoryArticle);
                }
                catch (Exception ex)
                {
                    string str = string.Empty;
                }
            }
            this.IsBusy = false;
        }

        private ObservableCollection<InventoryArticles> inventoryArticles = new ObservableCollection<InventoryArticles>();
        public ObservableCollection<InventoryArticles> InventoryArticles
        {
            get { return inventoryArticles; }
            set
            {
                inventoryArticles = value;
                InventoryArticlesCount = inventoryArticles.Count.ToString();

                OnPropertyChanged();
                GetUneditedArticles();
                GetCheckedArticles();
                GetErrorArticles();

                InventoryArticlesCheckedCount = InventoryArticlesChecked.Count.ToString();
                InventoryArticlesErrorCount = InventoryArticlesError.Count.ToString();
                InventoryArticlesUneditedCount = InventoryArticlesUnedited.Count.ToString();

                WizardData.Wiz_Inventory.InventoryArticlesList = new List<InventoryArticles>();
                WizardData.Wiz_Inventory.InventoryArticlesList = InventoryArticles.ToList();
            }
        }

        private ObservableCollection<InventoryArticles> inventoryArticlesUnedited = new ObservableCollection<InventoryArticles>();
        public ObservableCollection<InventoryArticles> InventoryArticlesUnedited
        {
            get { return inventoryArticlesUnedited; }
            set { SetProperty(ref inventoryArticlesUnedited, value); }
        }

        private ObservableCollection<InventoryArticles> inventoryArticlesChecked = new ObservableCollection<InventoryArticles>();
        public ObservableCollection<InventoryArticles> InventoryArticlesChecked
        {
            get { return inventoryArticlesChecked; }
            set { SetProperty(ref inventoryArticlesChecked, value); }
        }

        private ObservableCollection<InventoryArticles> inventoryArticlesError = new ObservableCollection<InventoryArticles>();
        public ObservableCollection<InventoryArticles> InventoryArticlesError
        {
            get { return inventoryArticlesError; }
            set { SetProperty(ref inventoryArticlesError, value); }
        }

        private void GetCheckedArticles()
        {
            List<InventoryArticles> origList = InventoryArticles.ToList();
            var list1 = origList.Where(x => x.Status == enumInventoryArticleStatus.OK).ToList();
            InventoryArticlesChecked = new ObservableCollection<InventoryArticles>(list1);
        }
        private void GetErrorArticles()
        {
            List<InventoryArticles> origList = InventoryArticles.ToList();
            var list2 = origList.Where(x => x.Status == enumInventoryArticleStatus.Fehlt).ToList();
            InventoryArticlesError = new ObservableCollection<InventoryArticles>(list2);
        }
        private void GetUneditedArticles()
        {
            List<InventoryArticles> origList = InventoryArticles.ToList();
            var list2 = origList.Where(x => x.Status == enumInventoryArticleStatus.Neu | x.Status == enumInventoryArticleStatus.NotSet).ToList();
            InventoryArticlesUnedited = new ObservableCollection<InventoryArticles>(list2);
        }

        private string inventoryArticlesUneditedCount = string.Empty;
        public string InventoryArticlesUneditedCount
        {
            get { return inventoryArticlesUneditedCount; }
            set { SetProperty(ref inventoryArticlesUneditedCount, value); }
        }

        private string inventoryArticlesCheckedCount = string.Empty;
        public string InventoryArticlesCheckedCount
        {
            get { return inventoryArticlesCheckedCount; }
            set { SetProperty(ref inventoryArticlesCheckedCount, value); }
        }

        private string _inventoryArticlesErrorCount = string.Empty;
        public string InventoryArticlesErrorCount
        {
            get { return _inventoryArticlesErrorCount; }
            set { SetProperty(ref _inventoryArticlesErrorCount, value); }
        }

        private string inventoryArticlesCount = string.Empty;
        public string InventoryArticlesCount
        {
            get { return inventoryArticlesCount; }
            set { SetProperty(ref inventoryArticlesCount, value); }
        }
    }
}
