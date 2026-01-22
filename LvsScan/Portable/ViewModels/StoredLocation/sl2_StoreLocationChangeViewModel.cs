using Common.ApiModels;
using Common.Enumerations;
using Common.Helper;
using Common.Models;
using LvsScan.Portable.Models;
using System;
using System.Drawing;
using System.Threading.Tasks;
using Telerik.XamarinForms.Primitives;

namespace LvsScan.Portable.ViewModels.StoredLocation
{
    public class sl2_StoreLocationChangeViewModel : BaseViewModel
    {
        internal Services.api_Article apiArticle;
        internal StoredLocationStringToStoreLocationItem locItem;
        public sl2_StoreLocationChangeViewModel()
        {
            locItem = new StoredLocationStringToStoreLocationItem();
            TabScanInputSelected = true;
        }

        public enumAppProcess AppProcess { get; set; } = enumAppProcess.StoreLocationChange;

        private TabViewItem selectedTabViewItem;
        public TabViewItem SelectedTabViewItem
        {
            get { return selectedTabViewItem; }
            set
            {
                selectedTabViewItem = value;
                //if ((selectedTabViewItem != null) && (!selectedTabViewItem.HeaderText.ToUpper().Equals("SCAN")))
                //{
                //    TabScanInputSelected = (selectedTabViewItem.HeaderText.ToUpper().Equals("SCAN"));
                //    TabManuelInputSelected = !(selectedTabViewItem.HeaderText.ToUpper().Equals("SCAN"));
                //}
                if (selectedTabViewItem != null)
                {
                    TabScanInputSelected = (selectedTabViewItem.HeaderText.ToUpper().Equals("SCAN"));
                    TabManuelInputSelected = !(selectedTabViewItem.HeaderText.ToUpper().Equals("SCAN"));
                }
            }
        }

        private bool _tabScanInputSelected = false;
        public bool TabScanInputSelected
        {
            get { return _tabScanInputSelected; }
            set { SetProperty(ref _tabScanInputSelected, value); }
        }
        private bool _tabManuelInputSelected = false;
        public bool TabManuelInputSelected
        {
            get { return _tabManuelInputSelected; }
            set { SetProperty(ref _tabManuelInputSelected, value); }
        }

        private Articles _article = new Articles();
        public Articles Article
        {
            get { return _article; }
            set
            {
                SetProperty(ref _article, value);
                //OldStoreLocation = _article.LagerOrtAsString;               
                //if ((WizardData is WizardData) && (WizardData.Wiz_StoreLocationChange is wizStoreLocationChanged))
                //{
                //    this.WizardData.Wiz_StoreLocationChange.ArticleToChange = _article.Copy();
                //}
                if (_article.Id > 0)
                {
                    if ((WizardData is WizardData) && (WizardData.Wiz_StoreLocationChange is wizStoreLocationChanged))
                    {
                        this.WizardData.Wiz_StoreLocationChange.ArticleToChange = _article.Copy();
                    }
                    WerkInput = _article.Werk;
                    HalleInput = _article.Halle;
                    ReiheInput = _article.Reihe;
                    EbeneInput = _article.Ebene;
                    PlatzInput = _article.Platz;

                    StoredLocationStringToStoreLocationItem tmpItem = new StoredLocationStringToStoreLocationItem();
                    tmpItem.ArticledataToStoreLocationString(Article);
                    //NewStoreLocation = tmpItem.StoredLocationString;

                    switch (AppProcess)
                    {
                        case enumAppProcess.StoreIn:
                            BackgroundColorNewStoreLocation = ValueToColorConverter.BooleanConvert(true);
                            BackgroundColorOldStoreLocation = ValueToColorConverter.TranspartenBackgroundColor();
                            break;
                        case enumAppProcess.StoreOut:
                            break;
                        case enumAppProcess.StoreLocationChange:
                            InputStoreLocationEqualOldStoreLocation = (NewStoreLocation.Equals(OldStoreLocation));
                            break;
                        case enumAppProcess.NotSet:
                            BackgroundColorNewStoreLocation = ValueToColorConverter.BooleanConvert(true);
                            BackgroundColorOldStoreLocation = ValueToColorConverter.TranspartenBackgroundColor();
                            break;
                    }
                }
            }
        }

        private Articles _articleOriginal = new Articles();
        public Articles ArticleOriginal
        {
            get { return _articleOriginal; }
            set
            {
                SetProperty(ref _articleOriginal, value);
                StoredLocationStringToStoreLocationItem tmpItem = new StoredLocationStringToStoreLocationItem();
                tmpItem.ArticledataToStoreLocationString(_articleOriginal);
                OldStoreLocation = tmpItem.StoredLocationString;
            }
        }
        private bool _InputStoreLocationEqualOldStoreLocation = false;
        public bool InputStoreLocationEqualOldStoreLocation
        {
            get { return _InputStoreLocationEqualOldStoreLocation; }
            set { SetProperty(ref _InputStoreLocationEqualOldStoreLocation, value); }
        }

        private string _oldStoreLocation = string.Empty;
        public string OldStoreLocation
        {
            get { return _oldStoreLocation; }
            set { SetProperty(ref _oldStoreLocation, value); }
        }

        private string _newStoreLocation = string.Empty;
        public string NewStoreLocation
        {
            get { return _newStoreLocation; }
            set
            {
                SetProperty(ref _newStoreLocation, value);
                if (_newStoreLocation.Equals("#####"))
                {
                    _newStoreLocation = string.Empty;
                }
                if (!_newStoreLocation.Equals(string.Empty))
                {
                    if (!OldStoreLocation.Equals(_newStoreLocation))
                    {
                        if (TabScanInputSelected)
                        {
                            locItem.Convert(_newStoreLocation);
                            if (!ArticleOriginal.Werk.Equals(locItem.WerkInput))
                            {
                                WerkInput = locItem.WerkInput;
                                ArticleOriginal.Werk = locItem.WerkInput;
                            }
                            if (!ArticleOriginal.Halle.Equals(locItem.HalleInput))
                            {
                                HalleInput = locItem.HalleInput;
                                ArticleOriginal.Halle = locItem.HalleInput;
                            }
                            if (!ArticleOriginal.Reihe.Equals(locItem.ReiheInput))
                            {
                                ReiheInput = locItem.ReiheInput;
                                ArticleOriginal.Reihe = locItem.ReiheInput;
                            }
                            if (!ArticleOriginal.Ebene.Equals(locItem.EbeneInput))
                            {
                                EbeneInput = locItem.EbeneInput;
                                ArticleOriginal.Ebene = locItem.EbeneInput;
                            }
                            if (!ArticleOriginal.Platz.Equals(locItem.PlatzInput))
                            {
                                PlatzInput = locItem.PlatzInput;
                                ArticleOriginal.Platz = locItem.PlatzInput;
                            }
                            //ReiheInput = locItem.ReiheInput;
                            //EbeneInput = locItem.EbeneInput;
                            //PlatzInput = locItem.PlatzInput;


                        }
                        else if (TabManuelInputSelected)
                        {
                            locItem.WerkInput = WerkInput;
                            locItem.HalleInput = HalleInput;
                            locItem.EbeneInput = EbeneInput;
                            locItem.ReiheInput = ReiheInput;
                            locItem.PlatzInput = PlatzInput;
                        }

                    }
                }
                BackgroundColorOldStoreLocation = ValueToColorConverter.BooleanConvert(!_newStoreLocation.Equals(string.Empty));
                if (_newStoreLocation.Equals(string.Empty))
                {
                    BackgroundColorNewStoreLocation = ValueToColorConverter.TranspartenBackgroundColor();
                    BackgroundColorOldStoreLocation = ValueToColorConverter.BooleanConvert(false);
                }
                else
                {
                    BackgroundColorNewStoreLocation = ValueToColorConverter.BooleanConvert(true);
                    BackgroundColorOldStoreLocation = ValueToColorConverter.TranspartenBackgroundColor();
                }
            }
        }
        private string _WerkInput = String.Empty;
        public string WerkInput
        {
            get { return _WerkInput; }
            set
            {
                SetProperty(ref _WerkInput, value);
                if ((Article.Werk != null) && (!Article.Werk.Equals(_WerkInput)))
                {
                    Articles art = Article.Copy();
                    art.Werk = _WerkInput;
                    Article = art.Copy();
                }
            }
        }
        private string _HalleInput = String.Empty;
        public string HalleInput
        {
            get { return _HalleInput; }
            set
            {
                SetProperty(ref _HalleInput, value);
                if ((Article.Halle != null) && (!Article.Halle.Equals(_HalleInput)))
                {
                    Articles art = Article.Copy();
                    art.Halle = _HalleInput;
                    Article = art.Copy();
                }
            }
        }
        private string _ReiheInput = String.Empty;
        public string ReiheInput
        {
            get { return _ReiheInput; }
            set
            {
                SetProperty(ref _ReiheInput, value);
                if ((Article.Reihe != null) && (!Article.Reihe.Equals(_ReiheInput)))
                {
                    Articles art = Article.Copy();
                    art.Reihe = _ReiheInput;
                    Article = art.Copy();
                }
            }
        }
        private string _EbeneInput = String.Empty;
        public string EbeneInput
        {
            get { return _EbeneInput; }
            set
            {
                SetProperty(ref _EbeneInput, value);
                if ((Article.Ebene != null) && (!Article.Ebene.Equals(_EbeneInput)))
                {
                    Articles art = Article.Copy();
                    art.Ebene = _EbeneInput;
                    Article = art.Copy();
                }
            }
        }
        private string _PlatzInput = String.Empty;
        public string PlatzInput
        {
            get { return _PlatzInput; }
            set
            {
                SetProperty(ref _PlatzInput, value);
                if ((Article.Platz != null) && (!Article.Platz.Equals(_PlatzInput)))
                {
                    Articles art = Article.Copy();
                    art.Platz = _PlatzInput;
                    Article = art.Copy();
                }
            }
        }

        private Color _WerkColor;
        public Color WerkColor
        {
            get { return _WerkColor; }
            set { SetProperty(ref _WerkColor, value); }
        }
        private Color _HalleColor;
        public Color HalleColor
        {
            get { return _HalleColor; }
            set { SetProperty(ref _HalleColor, value); }
        }
        private Color _ReiheColor;
        public Color ReiheColor
        {
            get { return _ReiheColor; }
            set { SetProperty(ref _ReiheColor, value); }
        }
        private Color _EbeneColor;
        public Color EbeneColor
        {
            get { return _EbeneColor; }
            set { SetProperty(ref _EbeneColor, value); }
        }
        private Color _PlatzColor;
        public Color PlatzColor
        {
            get { return _PlatzColor; }
            set { SetProperty(ref _PlatzColor, value); }
        }


        private System.Drawing.Color _backgroundColorNewStoreLocation;
        public System.Drawing.Color BackgroundColorNewStoreLocation
        {
            get { return _backgroundColorNewStoreLocation; }
            set { SetProperty(ref _backgroundColorNewStoreLocation, value); }
        }

        private System.Drawing.Color _backgroundColorOldStoreLocation;
        public System.Drawing.Color BackgroundColorOldStoreLocation
        {
            get { return _backgroundColorOldStoreLocation; }
            set { SetProperty(ref _backgroundColorOldStoreLocation, value); }
        }

        private ResponseStoreLocationChange _responseStoreLocChange;
        public ResponseStoreLocationChange ResponseStoreLocChange
        {
            get { return _responseStoreLocChange; }
            set { SetProperty(ref _responseStoreLocChange, value); }
        }
        public async Task ChangeStoreLocation()
        {
            IsBusy = true;
            Article.Werk = WerkInput;
            Article.Halle = HalleInput;
            Article.Reihe = ReiheInput;
            Article.Ebene = EbeneInput;
            Article.Platz = PlatzInput;



            apiArticle = new Services.api_Article();
            var result = await apiArticle.POST_Article_Update_StoreLocation(Article);
            ResponseStoreLocChange = result.Copy();
            Article = result.Article.Copy();
            IsBusy = false;
        }

    }
}
