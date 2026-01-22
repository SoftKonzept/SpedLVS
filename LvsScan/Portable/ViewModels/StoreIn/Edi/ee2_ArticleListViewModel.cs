using Common.Enumerations;
using Common.Models;
using LvsScan.Portable.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LvsScan.Portable.ViewModels.StoreIn.Edi
{
    public class ee2_ArticleListViewModel : BaseViewModel
    {
        public api_Eingang _api_Eingang;

        public ee2_ArticleListViewModel()
        {
            _api_Eingang = new api_Eingang();
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
                    Task.Run(() => GetArticleInEingang()).Wait();
                }
            }
        }

        public enumStoreInArt StoreInArt = enumStoreInArt.edi;

        private ObservableCollection<Articles> articlesInEingang = new ObservableCollection<Articles>();
        public ObservableCollection<Articles> ArticlesInEingang
        {
            get { return articlesInEingang; }
            set
            {
                SetProperty(ref articlesInEingang, value);
                CountAll = articlesInEingang.Count;
                if (articlesInEingang != null)
                {
                    this.WizardData.Wiz_StoreIn.ArticleInEingang = articlesInEingang.ToList();
                }
                GetSubLists();
            }
        }

        private Eingaenge _SelectedEingang;
        public Eingaenge SelectedEingang
        {
            get { return _SelectedEingang; }
            set
            {
                SetProperty(ref _SelectedEingang, value);
                if (_SelectedEingang.Id > 0)
                {
                    EingangIdString = "Eingang ID: " + _SelectedEingang.LEingangID.ToString() + " - [" + _SelectedEingang.Id.ToString() + "]";
                    EingangDateString = "vom " + _SelectedEingang.Eingangsdatum.ToString("dd.MM.yyyy");
                    //AusgangDateString += " | Termin: " + _SelectedEingang.Termin.ToString("dd.MM.yyyy HH:mm") + " Uhr";
                    BackgroundColorHead = _SelectedEingang.ViewBackgroundcolor;
                    WorkspaceString = "[Arbeitsbereich: " + _SelectedEingang.Workspace.WorkspaceString + "]";
                }
            }
        }


        private System.Drawing.Color backgroundColorHead = System.Drawing.Color.WhiteSmoke;
        public System.Drawing.Color BackgroundColorHead
        {
            get { return backgroundColorHead; }
            set { SetProperty(ref backgroundColorHead, value); }
        }
        private string _EingangIdString = string.Empty;
        public string EingangIdString
        {
            get { return _EingangIdString; }
            set { SetProperty(ref _EingangIdString, value); }
        }

        private string _EingangDateString = string.Empty;
        public string EingangDateString
        {
            get { return _EingangDateString; }
            set { SetProperty(ref _EingangDateString, value); }
        }
        private string _WorkspaceString = string.Empty;
        public string WorkspaceString
        {
            get { return _WorkspaceString; }
            set { SetProperty(ref _WorkspaceString, value); }
        }

        private void GetSubLists()
        {
            List<Articles> tmpOriginal = ArticlesInEingang.ToList();
            var tmpListChecked = tmpOriginal.Where(x => x.EingangChecked == true).ToList();
            var tmpListUnChecked = tmpOriginal.Where(x => x.EingangChecked == false).ToList();

            ArticlesChecked = new ObservableCollection<Articles>(tmpListChecked);
            ArticlesUnChecked = new ObservableCollection<Articles>(tmpListUnChecked);
        }

        private ObservableCollection<Articles> _ArticlesChecked = new ObservableCollection<Articles>();
        public ObservableCollection<Articles> ArticlesChecked
        {
            get { return _ArticlesChecked; }
            set
            {
                SetProperty(ref _ArticlesChecked, value);
                CountCheck = _ArticlesChecked.Count;
            }
        }

        private ObservableCollection<Articles> _ArticlesUnChecked = new ObservableCollection<Articles>();
        public ObservableCollection<Articles> ArticlesUnChecked
        {
            get { return _ArticlesUnChecked; }
            set
            {
                SetProperty(ref _ArticlesUnChecked, value);
                CountUnCheck = _ArticlesUnChecked.Count;
            }
        }

        public async Task GetArticleInEingang()
        {
            try
            {
                this.IsBusy = true;
                var result = await _api_Eingang.GET_Eingang_byId(SelectedEingang.Id);
                this.IsBusy = false;

                if (result.Success)
                {
                    ArticlesInEingang = new ObservableCollection<Articles>(result.ListEingangArticle);
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

        private Articles selectedArticle;
        public Articles SelectedArticle
        {
            get { return selectedArticle; }
            set
            {
                SetProperty(ref selectedArticle, value);
                if (selectedArticle.Id > 0)
                {
                    this.WizardData.Wiz_StoreIn.ArticleToCheck = selectedArticle.Copy();
                }
            }
        }

        public bool IsStoreInEditVisible
        {
            get
            {
                bool _IsStoreInEditVisible = false;
                _IsStoreInEditVisible = (CountCheck == CountAll) && (IsTabCheckedSelected);
                return _IsStoreInEditVisible;
            }
        }
        /// <summary>
        ///             ctr_MenuToolBarItemStoreOut
        ///             Button AutomationId = "4"
        /// </summary>
        //private bool _IsArticleScanStartVisible = false;
        public bool IsArticleScanStartVisible
        {
            get
            {
                bool _IsArticleScanStartVisible = false;
                _IsArticleScanStartVisible = (CountUnCheck > 0) && (IsTabUnCheckedSelected);
                return _IsArticleScanStartVisible;
            }
        }
        /// <summary>
        ///             ctr_MenuToolBarItemStoreOut
        ///             Button AutomationId = "6"
        /// </summary>
        /// 
        //private bool _IsCreateEingangVisible = false;
        public bool IsCreateEingangVisible
        {
            get
            {
                bool _IsCreateEingangVisible = false;
                switch (WizardData.Wiz_StoreIn.StoreInArt)
                {
                    case enumStoreInArt.edi:
                        _IsCreateEingangVisible = (CountCheck == CountAll) && (IsTabCheckedSelected);
                        break;
                    case enumStoreInArt.open:
                    case enumStoreInArt.NotSet:
                        _IsCreateEingangVisible = false;
                        break;
                }
                return _IsCreateEingangVisible;
            }
        }

        private bool _IsTabCheckedSelected = false;
        public bool IsTabCheckedSelected
        {
            get { return _IsTabCheckedSelected; }
            set { SetProperty(ref _IsTabCheckedSelected, value); }
        }

        private bool _IsTabUnCheckedSelected = false;
        public bool IsTabUnCheckedSelected
        {
            get { return _IsTabUnCheckedSelected; }
            set { SetProperty(ref _IsTabUnCheckedSelected, value); }
        }






    }
}
