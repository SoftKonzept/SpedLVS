using Common.ApiModels;
using Common.Enumerations;
using Common.Helper;
using Common.Models;
using Common.Views;
using LvsScan.Portable.Models;
using LvsScan.Portable.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Telerik.XamarinForms.Primitives;

namespace LvsScan.Portable.ViewModels.Wizard
{
    public class wiz_ScanSearchArticleViewModel : BaseViewModel
    {
        public wiz_ScanSearchArticleViewModel()
        {
            //--- button not activ
            //IsBaseNextEnabeld = true;
            BackgroundColorSearchLvsNr = ValueToColorConverter.BooleanConvert(false);
            BackgroundColorSearchProductionNo = ValueToColorConverter.BooleanConvert(false);
            helper = new wiz_ScanSearchArticle_Helper();
        }

        private enumAppProcess _AppProcess = enumAppProcess.NotSet;
        internal wiz_ScanSearchArticle_Helper helper;
        public enumAppProcess AppProcess
        {
            get { return _AppProcess; }
            set
            {
                SetProperty(ref _AppProcess, value);
                helper = new wiz_ScanSearchArticle_Helper(_AppProcess, StoreInArt, StoreOutArt);
                IsLvsInputVisible = helper.IsLvsInputVisible;
                IsProductionnoInputVisible = helper.IsProductionnoInputVisible;
            }
        }

        private enumStoreOutArt _StoreOutArt = enumStoreOutArt.NotSet;
        public enumStoreOutArt StoreOutArt
        {
            get { return _StoreOutArt; }
            set
            {
                SetProperty(ref _StoreOutArt, value);
                helper = new wiz_ScanSearchArticle_Helper(AppProcess, StoreInArt, _StoreOutArt);
            }
        }
        private enumStoreInArt _StoreInArt = enumStoreInArt.NotSet;
        public enumStoreInArt StoreInArt
        {
            get { return _StoreInArt; }
            set
            {
                SetProperty(ref _StoreInArt, value);
                helper = new wiz_ScanSearchArticle_Helper(AppProcess, _StoreInArt, StoreOutArt);
                switch (_StoreInArt)
                {
                    case enumStoreInArt.edi:
                        ShowCarouselViewArticleAsn = true;
                        ShowCarouselViewArticle = false;
                        break;
                    case enumStoreInArt.open:
                        ShowCarouselViewArticle = true;
                        ShowCarouselViewArticleAsn = false;
                        break;
                    default:
                        ShowCarouselViewArticle = true;
                        ShowCarouselViewArticleAsn = false;
                        break;
                }
            }
        }

        private enumStoreInArt_Steps _StoreInArtSteps = enumStoreInArt_Steps.NotSet;
        public enumStoreInArt_Steps StoreInArtSteps
        {
            get { return _StoreInArtSteps; }
            set { SetProperty(ref _StoreInArtSteps, value); }
        }

        private enumStoreOutArt_Steps _StoreOutArtSteps = enumStoreOutArt_Steps.NotSet;
        public enumStoreOutArt_Steps StoreOutArtSteps
        {
            get { return _StoreOutArtSteps; }
            set { SetProperty(ref _StoreOutArtSteps, value); }
        }

        private object _SelectedEA;
        public object SelectedEA
        {
            get { return _SelectedEA; }
            set { SetProperty(ref _SelectedEA, value); }
        }

        private List<Articles> _ArticlesInEA = new List<Articles>();
        public List<Articles> ArticlesInEA
        {
            get { return _ArticlesInEA; }
            set { SetProperty(ref _ArticlesInEA, value); }
        }

        private AsnLfsView _SelectedLfsView = new AsnLfsView();
        public AsnLfsView SelectedLfsView
        {
            get { return _SelectedLfsView; }
            set { SetProperty(ref _SelectedLfsView, value); }
        }

        private AsnArticleView _SelectedArticleView = new AsnArticleView();
        public AsnArticleView SelectedArticleView
        {
            get { return _SelectedArticleView; }
            set { SetProperty(ref _SelectedArticleView, value); }
        }


        private bool _LoadASNValues = false;
        public bool LoadASNValues
        {
            get { return _LoadASNValues; }
            set
            {
                SetProperty(ref _LoadASNValues, value);
                if (_LoadASNValues)
                {
                    Task.Run(async () => await GetAsnLfsAndArticleList()).Wait();
                }
            }
        }

        public void SetArticleToCheckList()
        {
            List<Articles> tmpList = new List<Articles>();

            switch (AppProcess)
            {
                case enumAppProcess.StoreOut:
                    tmpList = _ArticlesInEA.Where(x => x.AusgangChecked == false).ToList();
                    break;
                case enumAppProcess.StoreIn:
                    tmpList = _ArticlesInEA.Where(x => x.EingangChecked == false).ToList();
                    break;
            }
            ArticlesToCheck = new ObservableCollection<Articles>(tmpList);
        }

        private ObservableCollection<Articles> _ArticlesToCheck = new ObservableCollection<Articles>();
        public ObservableCollection<Articles> ArticlesToCheck
        {
            get { return _ArticlesToCheck; }
            set { SetProperty(ref _ArticlesToCheck, value); }
        }

        private ObservableCollection<AsnArticleView> _AsnArticlesToCheck = new ObservableCollection<AsnArticleView>();
        public ObservableCollection<AsnArticleView> AsnArticlesToCheck
        {
            get { return _AsnArticlesToCheck; }
            set { SetProperty(ref _AsnArticlesToCheck, value); }
        }

        private ObservableCollection<AsnLfsView> _AsnLfsToCheck = new ObservableCollection<AsnLfsView>();
        public ObservableCollection<AsnLfsView> AsnLfsToCheck
        {
            get { return _AsnLfsToCheck; }
            set { SetProperty(ref _AsnLfsToCheck, value); }
        }

        public async Task GetAsnLfsAndArticleList()
        {
            try
            {
                this.IsBusy = true;
                api_AsnRead apiAsnRead = new api_AsnRead();
                var result = await apiAsnRead.GET_GetLfsArticleListFromAsn();
                if (result.Success)
                {
                    AsnArticlesToCheck = new ObservableCollection<AsnArticleView>(result.ListAsnArticleView.ToList());
                    AsnLfsToCheck = new ObservableCollection<AsnLfsView>(result.ListAsnLfsView.ToList());
                }
                else
                {
                    await MessageService.ShowAsync("ACHTUNG", result.Error);
                }

                this.IsBusy = false;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }



        private bool _ShowCarouselViewArticle = true;
        public bool ShowCarouselViewArticle
        {
            get { return _ShowCarouselViewArticle; }
            set { SetProperty(ref _ShowCarouselViewArticle, value); }
        }

        private bool _ShowCarouselViewArticleAsn = false;
        public bool ShowCarouselViewArticleAsn
        {
            get { return _ShowCarouselViewArticleAsn; }
            set { SetProperty(ref _ShowCarouselViewArticleAsn, value); }
        }

        private TabViewItem _SelectedTabViewItem;
        public TabViewItem SelectedTabViewItem
        {
            get { return _SelectedTabViewItem; }
            set
            {
                SetProperty(ref _SelectedTabViewItem, value);
                if ((_SelectedTabViewItem != null) && (!_SelectedTabViewItem.HeaderText.ToUpper().Equals("SCAN")))
                {
                    IsManual = true;
                }
                else
                {
                    IsManual = false;
                }
            }
        }

        private int _positionCarouselView = 0;
        public int PositionCarouselView
        {
            get { return _positionCarouselView; }
            set
            {
                //SetProperty(ref _positionCarouselView, value);

                if ((value > ArticlesToCheck.Count - 1) || (value < 0))
                {
                    SetProperty(ref _positionCarouselView, 0);
                }
                else
                {
                    SetProperty(ref _positionCarouselView, value);
                }
            }
        }
        private int _positionCarouselViewAsn = 0;
        public int PositionCarouselViewAsn
        {
            get { return _positionCarouselViewAsn; }
            set
            {
                //SetProperty(ref _positionCarouselViewAsn, value);

                if ((value > AsnArticlesToCheck.Count - 1) || (value < 0))
                {
                    SetProperty(ref _positionCarouselViewAsn, 0);
                }
                else
                {
                    SetProperty(ref _positionCarouselViewAsn, value);
                }
            }
        }


        private bool _IsManual;
        public bool IsManual
        {
            get { return _IsManual; }
            set { SetProperty(ref _IsManual, value); }
        }

        private System.Drawing.Color _BackgroundColorSearchLvsNr;
        public System.Drawing.Color BackgroundColorSearchLvsNr
        {
            get { return _BackgroundColorSearchLvsNr; }
            set { SetProperty(ref _BackgroundColorSearchLvsNr, value); }
        }

        private System.Drawing.Color _BackgroundColorSearchProductionNo;
        public System.Drawing.Color BackgroundColorSearchProductionNo
        {
            get { return _BackgroundColorSearchProductionNo; }
            set { SetProperty(ref _BackgroundColorSearchProductionNo, value); }
        }

        private Articles _selectedArticle = new Articles();
        public Articles SelectedArticle
        {
            get { return _selectedArticle; }
            set
            {
                SetProperty(ref _selectedArticle, value);
                //PositionCarouselView = ArticlesToCheck.IndexOf(_selectedArticle);

                //if ((_selectedArticle != null) && (_selectedArticle.Id > 0))
                //{
                //    int Index1 = ArticlesToCheck.IndexOf(_selectedArticle);
                //    int iLoop = 0;
                //    foreach (var x in ArticlesToCheck)
                //    {
                //        if (x.Id.Equals(_selectedArticle.Id))
                //        {

                //        }
                //        iLoop++;
                //    }
                //    //PositionCarouselView =
                //    string str = string.Empty;
                //}
                //else
                //{
                //    PositionCarouselView = 0;
                //}              
            }
        }

        //private int _CarouselViewPosition;
        //public int CarouselViewPosition
        //{
        //    get { return _CarouselViewPosition; }
        //    set { SetProperty(ref _CarouselViewPosition, value); }
        //}

        //private int _CarouselViewAsnPosition;
        //public int CarouselViewAsnPosition
        //{
        //    get { return _CarouselViewAsnPosition; }
        //    set { SetProperty(ref _CarouselViewAsnPosition, value); }
        //}

        private AsnArticleView _selectedAsnArticle = new AsnArticleView();
        public AsnArticleView SelectedAsnArticle
        {
            get { return _selectedAsnArticle; }
            set { SetProperty(ref _selectedAsnArticle, value); }
        }

        private bool _IsLvsInputVisible = true;
        public bool IsLvsInputVisible
        {
            get { return _IsLvsInputVisible; }
            set { SetProperty(ref _IsLvsInputVisible, value); }
        }

        private string _searchLvsNo;
        public string SearchLvsNo
        {
            get { return _searchLvsNo; }
            set
            {
                SetProperty(ref _searchLvsNo, value);

                int iLvsNr = 0;
                int.TryParse(_searchLvsNo, out iLvsNr);
                if (iLvsNr > 0)
                {
                    ExistLVSNr = helper.ExistLvsNo(iLvsNr, ArticlesToCheck.ToList());
                }
                else
                {
                    ExistLVSNr = false;
                }
            }
        }

        private bool _IsProductionnoInputVisible = true;
        public bool IsProductionnoInputVisible
        {
            get { return _IsProductionnoInputVisible; }
            set { SetProperty(ref _IsProductionnoInputVisible, value); }
        }
        private string _searchProduktionsnummer = String.Empty;
        public string SearchProduktionsnummer
        {
            get { return _searchProduktionsnummer; }
            set
            {
                SetProperty(ref _searchProduktionsnummer, value.ToUpper());
                ExistProductionNo = false;
                switch (StoreInArt)
                {
                    case enumStoreInArt.open:
                        if ((_searchProduktionsnummer != null) && (_searchProduktionsnummer.Length > 0))
                        {
                            ExistProductionNo = helper.ExistProductionNo(_searchProduktionsnummer, ArticlesToCheck.ToList());
                            Articles tmpArt = ArticlesToCheck.FirstOrDefault(x => x.Produktionsnummer.Equals(_searchProduktionsnummer));
                            if (tmpArt != null)
                            {
                                int iPosition = ArticlesToCheck.IndexOf(tmpArt);
                                SelectedArticle = tmpArt.Copy();
                                PositionCarouselView = iPosition;

                                //if (!SelectedArticle.Id.Equals(tmpArt.Id))
                                //{
                                //    SelectedArticle = tmpArt.Copy();
                                //    //PositionCarouselView = ArticlesToCheck.IndexOf(SelectedArticle);
                                //}
                                //SelectedArticle = tmpArt.Copy();


                                //SelectedArticle = ArticlesToCheck.Skip(iPosition).FirstOrDefault();
                            }
                        }
                        break;
                    case enumStoreInArt.edi:
                        if ((_searchProduktionsnummer != null) && (_searchProduktionsnummer.Length > 0))
                        {
                            ExistProductionNo = helper.ExistProductionNo(_searchProduktionsnummer, AsnArticlesToCheck.ToList());
                            if (helper.SearchedAsnArticleView != null)
                            {
                                if (!SelectedAsnArticle.Equals(helper.SearchedAsnArticleView))
                                {
                                    SelectedAsnArticle = helper.SearchedAsnArticleView.Copy();
                                }
                            }
                        }
                        break;
                }
            }
        }

        private bool _existLVSNr = false;
        public bool ExistLVSNr
        {
            get { return _existLVSNr; }
            set
            {
                SetProperty(ref _existLVSNr, value);
                IsBaseNextEnabeld = helper.IsBaseNextEnabeld(_existLVSNr, ExistProductionNo);
                BackgroundColorSearchLvsNr = ValueToColorConverter.BooleanConvert(_existLVSNr);
            }
        }

        private bool _existProductionNo = false;
        public bool ExistProductionNo
        {
            get { return _existProductionNo; }
            set
            {
                SetProperty(ref _existProductionNo, value);
                IsBaseNextEnabeld = helper.IsBaseNextEnabeld(ExistLVSNr, _existProductionNo);
                BackgroundColorSearchProductionNo = ValueToColorConverter.BooleanConvert(_existProductionNo);
            }
        }

        public async Task<bool> UpdateArticleScanCheck()
        {
            IsBusy = true;

            switch (AppProcess)
            {
                case enumAppProcess.StoreIn:
                    SelectedArticle.ScanIn = DateTime.Now;
                    SelectedArticle.ScanInUser = (int)WizardData.LoggedUser.Id;
                    break;

                case enumAppProcess.StoreOut:
                    SelectedArticle.ScanOut = DateTime.Now;
                    SelectedArticle.ScanOutUser = (int)WizardData.LoggedUser.Id;
                    break;
            }

            ResponseArticle resArticle = new ResponseArticle();
            resArticle.Article = SelectedArticle.Copy();
            resArticle.AppProcess = AppProcess;

            api_Article _apiArticle = new api_Article();
            var result = await _apiArticle.POST_Article_Update_ScanValue(resArticle);
            IsBusy = false;
            if (result.Success)
            {
                SelectedArticle = result.Article.Copy();
            }
            return result.Success;
        }



        public async Task<bool> UpdateArticleScanIdentification()
        {
            IsBusy = true;

            ResponseArticle resArticle = new ResponseArticle();
            resArticle.Article = SelectedArticle.Copy();
            resArticle.AppProcess = AppProcess;

            api_Article _apiArticle = new api_Article();
            var result = await _apiArticle.POST_Article_Update_ScanIdentification(resArticle);
            IsBusy = false;
            if (result.Success)
            {
                SelectedArticle = result.Article.Copy();
            }
            return result.Success;
        }


        public async Task<ResponseASN> CreateStoreInFromAsn()
        {
            ResponseASN response = new ResponseASN();
            if (AppProcess.Equals(enumAppProcess.StoreIn))
            {
                IsBusy = true;

                response.UserId = (int)WizardData.LoggedUser.Id;
                response.AsnArticle = SelectedAsnArticle.Copy();

                api_AsnRead _api = new api_AsnRead(WizardData.LoggedUser);
                var result = await _api.POST_ASN_CreateStoreIn(response);
                response = result.Copy();
                IsBusy = false;
                if (result.Success)
                {
                    WizardData.Wiz_StoreIn.SelectedEingang = result.Eingang.Copy();
                    WizardData.Wiz_StoreIn.ArticleInEingang = result.ArticlesInEingang.ToList();

                    ////--- seletcted Productionnumber in Article Eingang 
                    Articles selArticle = result.ArticlesInEingang.FirstOrDefault(x => x.Produktionsnummer == SearchProduktionsnummer);
                    if (
                            (selArticle != null) &&
                            (selArticle.Id > 0) &&
                            (selArticle.Produktionsnummer.Equals(SearchProduktionsnummer))
                       )
                    {
                        SelectedArticle = selArticle;
                        WizardData.Wiz_StoreIn.ArticleToCheck = SelectedArticle.Copy();

                    }
                }
            }
            return response;
        }


    }
}
