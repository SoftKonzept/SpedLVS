using Common.Enumerations;
using Common.Helper;
using Common.Models;
using LvsScan.Portable.Services;
using System.Collections.ObjectModel;

namespace LvsScan.Portable.ViewModels.StoreIn.Open
{
    public class oe_SelectFromSearchedResultViewModel : BaseViewModel
    {
        public api_Eingang _api_Eingang;
        public oe_SelectFromSearchedResultViewModel()
        {
            IsBusy = false;
            //Title = "oa1_OpenStoreOutListViewModel";
        }

        //private bool loadValues = false;
        //public bool LoadValues
        //{
        //    get { return loadValues; }
        //    set
        //    {
        //        loadValues = value;
        //        OnPropertyChanged();
        //        if (loadValues)
        //        {
        //            Task.Run(() => GetEingangList()).Wait();
        //        }
        //    }
        //}

        public enumAppProcess AppProcess = enumAppProcess.StoreIn;
        public enumStoreInArt StoreInArt = enumStoreInArt.open;

        private ObservableCollection<Articles> _ArticlesToCheck = new ObservableCollection<Articles>();
        public ObservableCollection<Articles> ArticlesToCheck
        {
            get { return _ArticlesToCheck; }
            set
            {
                SetProperty(ref _ArticlesToCheck, value);
                ArticleCount = _ArticlesToCheck.Count;
            }
        }

        private int _ArticleCount = 0;
        public int ArticleCount
        {
            get { return _ArticleCount; }
            set
            {
                SetProperty(ref _ArticleCount, value);
            }
        }

        private Articles _SelectedArticle;
        public Articles SelectedArticle
        {
            get { return _SelectedArticle; }
            set
            {
                SetProperty(ref _SelectedArticle, value);
                if (_SelectedArticle.Eingang.Id > 0)
                {
                    SelectedEingang = _SelectedArticle.Eingang.Copy();
                }

            }
        }

        private Eingaenge _SelectedEingang;
        public Eingaenge SelectedEingang
        {
            get { return _SelectedEingang; }
            set
            {
                SetProperty(ref _SelectedEingang, value);
                if (
                        (_SelectedEingang is Eingaenge) &&
                        (_SelectedEingang.Id > 0)
                  )
                {
                    this.WizardData.Wiz_StoreIn.SelectedEingang = _SelectedEingang.Copy();
                }
            }
        }

        private System.Drawing.Color backgroundColorBadge = ValueToColorConverter.BadgeBackgroundColor_Default();
        public System.Drawing.Color BackgroundColorBadge
        {
            get { return backgroundColorBadge; }
            set { SetProperty(ref backgroundColorBadge, value); }
        }





        //private ObservableCollection<Eingaenge> _EingangOpen = new ObservableCollection<Eingaenge>();
        //public ObservableCollection<Eingaenge> EingangOpen
        //{
        //    get { return _EingangOpen; }
        //    set
        //    {
        //        SetProperty(ref _EingangOpen, value);
        //        EingangCount = _EingangOpen.Count;
        //    }
        //}

        //public async Task GetEingangList()
        //{
        //    try
        //    {
        //        this.IsBusy = true;
        //        _api_Eingang = new api_Eingang(WizardData.LoggedUser);
        //        var result = await _api_Eingang.GET_EingangList_Open();
        //        EingangOpen = new ObservableCollection<Eingaenge>(result.ListEingaengeOpen.Where(x => x.ArticleCount>0).OrderBy(x => x.Eingangsdatum).ToList());
        //        this.IsBusy = false;

        //        if (!result.Success)
        //        {
        //            await MessageService.ShowAsync("ACHTUNG", result.Error);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string str = ex.Message;
        //    }
        //}

        //private int _EingangCount;
        //public int EingangCount
        //{
        //    get { return _EingangCount; }
        //    set { SetProperty(ref _EingangCount, value); }
        //}

        //private int checkedCount;
        //public int CheckedCount
        //{
        //    get { return checkedCount; }
        //    set { SetProperty(ref checkedCount, value); }
        //}



    }
}
