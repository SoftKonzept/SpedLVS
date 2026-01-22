using Common.Models;
using LvsScan.Portable.Models;
using LvsScan.Portable.Services;
using Newtonsoft.Json;
using System;
using Xamarin.Forms;

namespace LvsScan.Portable.ViewModels.Inventory
{
    [QueryProperty(nameof(Content), nameof(Content))]
    public class InventoryArtikelViewModel : BaseViewModel
    {
        public InventoryArtikelViewModel()
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
                    var resContent = JsonConvert.DeserializeObject<InventoryArticles>(content);
                    this.SelectedInventoryArticle = (InventoryArticles)resContent;
                }
                OnPropertyChanged();
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

                InventoryArticleShow = new InventoryArticlePageViewCls();
                inventoryArticleShow.InventoryArticle = SelectedInventoryArticle.Copy();
            }
        }

        public InventoryArticlePageViewCls inventoryArticleShow;
        public InventoryArticlePageViewCls InventoryArticleShow
        {
            get
            {
                return inventoryArticleShow;
            }
            set
            {
                inventoryArticleShow = value;
                OnPropertyChanged();
            }
        }

    }
}
