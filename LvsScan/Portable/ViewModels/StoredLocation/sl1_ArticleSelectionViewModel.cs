using Common.Enumerations;
using Common.Helper;
using Common.Models;
using LvsScan.Portable.Models;
using LvsScan.Portable.Services;
using System;
using System.Drawing;
using System.Threading.Tasks;
using Telerik.XamarinForms.Primitives;

namespace LvsScan.Portable.ViewModels.StoredLocation
{
    public class sl1_ArticleSelectionViewModel : BaseViewModel
    {
        internal Services.api_Article apiArticle;
        public sl1_ArticleSelectionViewModel()
        {
            BackgroudColorLvsSearch = ValueToColorConverter.BooleanConvert(false);
            BackgroudColorProductionNoSearch = ValueToColorConverter.BooleanConvert(false);
        }


        private TabViewItem selectedTabViewItem;
        public TabViewItem SelectedTabViewItem
        {
            get { return selectedTabViewItem; }
            set
            {
                selectedTabViewItem = value;
                if ((selectedTabViewItem != null) && (!selectedTabViewItem.HeaderText.ToUpper().Equals("SCAN")))
                {
                    IsManual = true;
                }
                else
                {
                    IsManual = false;
                }
            }
        }

        private bool isManual;
        public bool IsManual
        {
            get { return isManual; }
            set
            {
                isManual = value;
            }
        }


        private string _searchLvsNo = string.Empty;
        public string SearchLvsNo
        {
            get { return _searchLvsNo; }
            set
            {
                SetProperty(ref _searchLvsNo, value);
                if (_searchLvsNo.Length > 0)
                {
                    int iTmp = 0;
                    if (int.TryParse(_searchLvsNo, out iTmp))
                    {
                        ArticleSearch.ExistLvsNoSearchValue = false;
                        ArticleSearch.CurrentSearchValueDatafield = enumArticle_Datafields.LVS_ID;
                        ArticleSearch.LvsNoSearch = _searchLvsNo;
                        Task task = CheckLvsNo();
                    }
                }
                else
                {
                    ArticleSearch = new ArticleSearch();
                    ExistLvsNo = ArticleSearch.ExistLvsNoSearchValue;
                }
            }
        }

        private ArticleSearch _articleSearch = new ArticleSearch();
        public ArticleSearch ArticleSearch
        {
            get { return _articleSearch; }
            set
            {
                SetProperty(ref _articleSearch, value);
                if ((WizardData is WizardData) && (WizardData.Wiz_StoreLocationChange is wizStoreLocationChanged))
                {
                    WizardData.Wiz_StoreLocationChange.ArticleSearch = _articleSearch;
                }
            }
        }

        private Articles _articleSearched;
        public Articles ArticleSearched
        {
            get { return _articleSearched; }
            set
            {
                SetProperty(ref _articleSearched, value);

                if ((WizardData is WizardData) && (WizardData.Wiz_StoreLocationChange is wizStoreLocationChanged))
                {
                    WizardData.Wiz_StoreLocationChange.ArticleToChange = _articleSearched;
                }
            }
        }

        private bool _existLvsNo = false;
        public bool ExistLvsNo
        {
            get { return _existLvsNo; }
            set
            {
                SetProperty(ref _existLvsNo, value);
                BackgroudColorLvsSearch = ValueToColorConverter.BooleanConvert(_existLvsNo);
                //IsSearchComplete = ((_existLvsNo) && (ExistProduktionNoInCombinationLvsNo));
            }
        }

        private bool _isSearchComplete = false;
        public bool IsSearchComplete
        {
            get { return _isSearchComplete; }
            set { SetProperty(ref _isSearchComplete, value); }
        }

        private async Task CheckLvsNo()
        {
            this.IsBusy = true;
            apiArticle = new api_Article();
            //var result = await apiArticle.GET_Article_ExistArticleValue(ArticleSearch);
            var result = await apiArticle.GET_Article_ExistArticleLvsForStoreLocationChange(ArticleSearch);
            if (result is ArticleSearch)
            {
                ArticleSearch = result.Copy();
                ExistLvsNo = ArticleSearch.ExistLvsNoSearchValue;

                try
                {
                    ArticleSearched = ArticleSearch.ArticleSearcheResult.Copy();
                }
                catch (Exception ex)
                {
                    string str = ex.Message;
                }

                if (result.IsRebookedArticle)
                {
                    string message = string.Empty;
                    string mesInfo = string.Empty;
                    message = "Die LVSNr " + ArticleSearch.LvsNoSearch + " wurde umgebucht in die neue LVSNr " + result.ArticleSearcheResult.LVS_ID.ToString() + " !" + System.Environment.NewLine;
                    mesInfo = "INFORMATION";
                    await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");

                }
            }
            this.IsBusy = false;
        }


        public async Task SearchArticle()
        {
            this.IsBusy = true;

            await Task.Run(async () =>
            {
                apiArticle = new api_Article();
                //ArticleSearched = await apiArticle.GET_Article_Search(this.ArticleSearch);
                ArticleSearched = await apiArticle.GET_Article_SearchArticleForStoredLocationChange(this.ArticleSearch);
            });
            this.IsBusy = false;
        }

        private Color _backgroudColorLvsSearch;
        public Color BackgroudColorLvsSearch
        {
            get { return _backgroudColorLvsSearch; }
            set { SetProperty(ref _backgroudColorLvsSearch, value); }
        }
        private Color _backgroudColorProductionNoSearch = ValueToColorConverter.BooleanConvert(false);
        public Color BackgroudColorProductionNoSearch
        {
            get { return _backgroudColorProductionNoSearch; }
            set { SetProperty(ref _backgroudColorProductionNoSearch, value); }
        }



    }
}
