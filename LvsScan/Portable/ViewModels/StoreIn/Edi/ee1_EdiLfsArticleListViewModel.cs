using Common.Enumerations;
using Common.Views;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Telerik.XamarinForms.Primitives;

namespace LvsScan.Portable.ViewModels.StoreIn.Edi
{
    public class ee1_EdiLfsArticleListViewModel : BaseViewModel
    {
        public ee1_EdiLfsArticleListViewModel()
        {
            //Title = "oa1_OpenStoreOutListViewModel";
        }

        public enumAppProcess AppProcess = enumAppProcess.StoreIn;
        public enumStoreInArt StoreInArt = enumStoreInArt.edi;

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
                    //Task.Run(() => GetAsnLfsAndArticleList()).Wait();
                }
            }
        }

        private ObservableCollection<AsnArticleView> _ListAsnArticleViews = new ObservableCollection<AsnArticleView>();
        public ObservableCollection<AsnArticleView> ListAsnArticleViews
        {
            get { return _ListAsnArticleViews; }
            set
            {
                SetProperty(ref _ListAsnArticleViews, value);
                CountArticle = _ListAsnArticleViews.Count;
            }
        }
        private ObservableCollection<AsnLfsView> _ListAsnLfsViews = new ObservableCollection<AsnLfsView>();
        public ObservableCollection<AsnLfsView> ListAsnLfsViews
        {
            get { return _ListAsnLfsViews; }
            set
            {
                SetProperty(ref _ListAsnLfsViews, value);
                CountLfs = _ListAsnLfsViews.Count;
            }
        }

        public async Task GetAsnLfsAndArticleList()
        {
            try
            {
                //if(this.IsBusy==false)
                //{
                //    this.IsBusy = true;
                //}

                //api_AsnRead apiAsnRead = new api_AsnRead();
                //var result = await apiAsnRead.GET_GetLfsArticleListFromAsn();
                //if (result.Success)
                //{
                //    ListAsnArticleViews = new ObservableCollection<AsnArticleView>(result.ListAsnArticleView.ToList());
                //    ListAsnLfsViews = new ObservableCollection<AsnLfsView>(result.ListAsnLfsView.ToList());
                //}
                //else
                //{
                //    await MessageService.ShowAsync("ACHTUNG", result.Error);
                //}


                //if (this.IsBusy == true)
                //{
                //    this.IsBusy = false;
                //}
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        private TabViewItem _SelectedTabViewItem;
        public TabViewItem SelectedTabViewItem
        {
            get { return _SelectedTabViewItem; }
            set
            {
                SetProperty(ref _SelectedTabViewItem, value);
            }
        }

        private AsnLfsView _SelectedLfsView = new AsnLfsView();
        public AsnLfsView SelectedLfsView
        {
            get { return _SelectedLfsView; }

            set
            {
                SetProperty(ref _SelectedLfsView, value);
            }
        }

        private AsnArticleView _SelectedArticleView = new AsnArticleView();
        public AsnArticleView SelectedArticleView
        {
            get { return _SelectedArticleView; }
            set
            {
                SetProperty(ref _SelectedArticleView, value);
            }
        }

        private int _CountArticle = 0;
        public int CountArticle
        {
            get { return _CountArticle; }
            set { SetProperty(ref _CountArticle, value); }
        }
        private int _CountLfs = 0;
        public int CountLfs
        {
            get { return _CountLfs; }
            set { SetProperty(ref _CountLfs, value); }
        }

        /// <summary>
        ///             Button AutomationId = "4" Eingang Edit
        /// </summary>
        public bool IsArticleScanStartVisible
        {
            get
            {
                bool _IsArticleScanStartVisible = false;
                //_IsArticleScanStartVisible = (CountArticle>0) && ();
                return _IsArticleScanStartVisible;
            }
            //set { SetProperty(ref _IsArticleScanStartVisible, value); }
        }

        /// <summary>
        ///             Button AutomationId = "5" 
        /// </summary>
        public bool IsProcessStartVisible
        {
            get
            {
                bool _IsProcessStartVisible = false;
                if (SelectedArticleView != null)
                {
                    _IsProcessStartVisible = (SelectedArticleView.AsnId > 0);
                }
                return _IsProcessStartVisible;
            }
            //set { SetProperty(ref _IsArticleScanStartVisible, value); }
        }
    }
}
