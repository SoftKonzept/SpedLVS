using Common.Enumerations;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LvsScan.Portable.ViewModels.Inventory
{
    public class i3w3_wizInventoryEditViewMode : BaseViewModel
    {
        public i3w3_wizInventoryEditViewMode()
        {
            IsBaseNextEnabeld = false;
        }

        public List<string> StatusList
        {
            get
            {
                return Enum.GetNames(typeof(enumInventoryArticleStatus)).ToList();
            }
        }

        private string _ArticleStausString = String.Empty;
        public string ArticleStausString
        {
            get { return _ArticleStausString; }
            set
            {
                SetProperty(ref _ArticleStausString, value);
                enumInventoryArticleStatus tmpEnum = enumInventoryArticleStatus.NotSet;
                Enum.TryParse<enumInventoryArticleStatus>(value, out tmpEnum);
                SelectedInventoryArticleStaus = tmpEnum;
            }
        }

        private enumInventoryArticleStatus _selectedInventoryArticleStaus = enumInventoryArticleStatus.NotSet;
        public enumInventoryArticleStatus SelectedInventoryArticleStaus
        {
            get { return _selectedInventoryArticleStaus; }
            set
            {
                SetProperty(ref _selectedInventoryArticleStaus, value);
            }
        }
        private Inventories _selectedInventory = new Inventories();
        public Inventories SelectedInventory
        {
            get { return _selectedInventory; }
            set { SetProperty(ref _selectedInventory, value); }
        }
        private InventoryArticles _selectedInventoryArticle = new InventoryArticles();
        public InventoryArticles SelectedInventoryArticle
        {
            get { return _selectedInventoryArticle; }
            set
            {
                SetProperty(ref _selectedInventoryArticle, value);
                if ((_selectedInventoryArticle != null) && (_selectedInventoryArticle.Artikel is Articles))
                {
                    Article = _selectedInventoryArticle.Artikel.Copy();
                }
            }
        }

        private Articles _article;
        public Articles Article
        {
            get { return _article; }
            set { SetProperty(ref _article, value); }
        }


        private List<string> _ErrorMessageList_InputSearch = new List<string>();
        public List<string> ErrorMessageList_InputSearch
        {
            get { return _ErrorMessageList_InputSearch; }
            set
            {
                SetProperty(ref _ErrorMessageList_InputSearch, value);
            }
        }

        public DateTime MinDatePicker = new DateTime(1900, 1, 1);
        public DateTime MaxDatePicker = DateTime.Now;

        private DateTime _selectedDate = DateTime.Now;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { SetProperty(ref _selectedDate, value); }
        }
    }
}
