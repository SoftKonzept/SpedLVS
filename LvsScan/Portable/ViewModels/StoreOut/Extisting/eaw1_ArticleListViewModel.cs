using Common.Models;
using LvsScan.Portable.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LvsScan.Portable.ViewModels.StoreOut.Extisting
{
    public class eaw1_ArticleListViewModel : BaseViewModel
    {
        public api_Ausgang _api_Ausgang;
        public eaw1_ArticleListViewModel()
        {
            _api_Ausgang = new api_Ausgang();
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
                    Task.Run(() => GetArticleInAusgang()).Wait();
                }
            }
        }

        private ObservableCollection<Articles> articlesInAusgang = new ObservableCollection<Articles>();
        public ObservableCollection<Articles> ArticlesInAusgang
        {
            get { return articlesInAusgang; }
            set
            {
                SetProperty(ref articlesInAusgang, value);
                CountAll = articlesInAusgang.Count;
                if (articlesInAusgang != null)
                {
                    this.WizardData.Wiz_StoreOut.ListArticleInAusgang = articlesInAusgang.ToList();
                }
                GetSubLists();
            }
        }

        private void GetSubLists()
        {
            List<Articles> tmpOriginal = articlesInAusgang.ToList();
            var tmpListChecked = tmpOriginal.Where(x => x.AusgangChecked == true).ToList();
            var tmpListUnChecked = tmpOriginal.Where(x => x.AusgangChecked == false).ToList();

            ArticlesChecked = new ObservableCollection<Articles>(tmpListChecked);
            ArticlesUnChecked = new ObservableCollection<Articles>(tmpListUnChecked);
        }

        private ObservableCollection<Articles> articlesChecked = new ObservableCollection<Articles>();
        public ObservableCollection<Articles> ArticlesChecked
        {
            get { return articlesChecked; }
            set
            {
                SetProperty(ref articlesChecked, value);
                CountCheck = articlesChecked.Count;
            }
        }

        private ObservableCollection<Articles> articlesUnChecked = new ObservableCollection<Articles>();
        public ObservableCollection<Articles> ArticlesUnChecked
        {
            get { return articlesUnChecked; }
            set
            {
                SetProperty(ref articlesUnChecked, value);
                CountUnCheck = articlesUnChecked.Count;
            }
        }

        public async Task GetArticleInAusgang()
        {
            try
            {
                this.IsBusy = true;
                var result = await _api_Ausgang.GET_Ausgang_byId(SelectedAusgang.Id);
                this.IsBusy = false;

                if (result.Success)
                {
                    ArticlesInAusgang = new ObservableCollection<Articles>(result.ListAusgangArticle);
                }
                else
                {
                    await MessageService.ShowAsync("ACHTUNG", result.Error);
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        private int countAll;
        public int CountAll
        {
            get { return countAll; }
            set { SetProperty(ref countAll, value); }
        }

        private int countCheck;
        public int CountCheck
        {
            get { return countCheck; }
            set
            {
                SetProperty(ref countCheck, value);
                IsStoreOutStartVisible = (CountCheck == CountAll);
                isArticleScanStartVisible = (CountCheck < CountAll);
            }
        }

        private int countUnCheck;
        public int CountUnCheck
        {
            get { return countUnCheck; }
            set
            {
                SetProperty(ref countUnCheck, value);

            }
        }

        private Ausgaenge selectedAusgang;
        public Ausgaenge SelectedAusgang
        {
            get { return selectedAusgang; }
            set { SetProperty(ref selectedAusgang, value); }
        }

        private Articles selectedArticle;
        public Articles SelectedArticle
        {
            get { return selectedArticle; }
            set
            {
                SetProperty(ref selectedArticle, value);
                if (selectedArticle.Id > 0)
                {
                    this.WizardData.Wiz_StoreOut.ArticleToCheck = selectedArticle.Copy();
                }
            }
        }

        private bool isStoreOutStartVisible = false;
        public bool IsStoreOutStartVisible
        {
            get { return isStoreOutStartVisible; }
            set { SetProperty(ref isStoreOutStartVisible, value); }
        }
        private bool isArticleScanStartVisible = false;
        public bool IsArticleScanStartVisible
        {
            get { return isArticleScanStartVisible; }
            set { SetProperty(ref isArticleScanStartVisible, value); }
        }

    }
}
