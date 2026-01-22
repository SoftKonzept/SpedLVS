using Common.ApiModels;
using Common.Enumerations;
using Common.Helper;
using Common.Models;
using LvsScan.Portable.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Telerik.XamarinForms.Primitives;

namespace LvsScan.Portable.ViewModels.StoreOut.Open
{
    public class oa3w1_wizArticelScanViewModel : BaseViewModel
    {
        public oa3w1_wizArticelScanViewModel()
        {
            //--- button not activ
            //IsBaseNextEnabeld = true;
            BackgroundColorSearchLvsNr = ValueToColorConverter.BooleanConvert(false);
            BackgroundColorSearchProductionNo = ValueToColorConverter.BooleanConvert(false);
        }

        public enumStoreOutArt StoreOutArt = enumStoreOutArt.open;

        private Ausgaenge selectedAusgang;
        public Ausgaenge SelectedAusgang
        {
            get { return selectedAusgang; }
            set { selectedAusgang = value; }
        }

        private List<Articles> articlesInAusgang = new List<Articles>();
        public List<Articles> ArticlesInAusgang
        {
            get { return articlesInAusgang; }
            set { SetProperty(ref articlesInAusgang, value); }
        }

        public void SetArticleToCheckList()
        {
            List<Articles> tmpList = ArticlesInAusgang.Where(x => x.AusgangChecked == false).ToList();
            ArticlesToCheck = new ObservableCollection<Articles>(tmpList);
        }

        private ObservableCollection<Articles> articlesToCheck = new ObservableCollection<Articles>();
        public ObservableCollection<Articles> ArticlesToCheck
        {
            get { return articlesToCheck; }
            set { SetProperty(ref articlesToCheck, value); }
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

        private bool isManual;
        public bool IsManual
        {
            get { return isManual; }
            set { isManual = value; }
        }

        private System.Drawing.Color _backgroundColorSearchLvsNr;
        public System.Drawing.Color BackgroundColorSearchLvsNr
        {
            get { return _backgroundColorSearchLvsNr; }
            set { SetProperty(ref _backgroundColorSearchLvsNr, value); }
        }

        private System.Drawing.Color _backgroundColorSearchProductionNo;
        public System.Drawing.Color BackgroundColorSearchProductionNo
        {
            get { return _backgroundColorSearchProductionNo; }
            set { SetProperty(ref _backgroundColorSearchProductionNo, value); }
        }

        private Articles _selectedArticle = new Articles();
        public Articles SelectedArticle
        {
            get { return _selectedArticle; }
            set { SetProperty(ref _selectedArticle, value); }
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
                    List<Articles> listArt = ArticlesToCheck.ToList();
                    List<Articles> list = listArt.Where(x => x.LVS_ID == iLvsNr).ToList();
                    ExistLVSNr = (list.Count > 0);
                }
                else
                {
                    ExistLVSNr = false;
                }
            }
        }
        private string _searchProduktionsnummer = String.Empty;
        public string SearchProduktionsnummer
        {
            get { return _searchProduktionsnummer; }
            set
            {
                SetProperty(ref _searchProduktionsnummer, value.ToUpper());
                if ((_searchProduktionsnummer != null) && (_searchProduktionsnummer.Length > 0))
                {
                    ExistProductionNo = (_searchProduktionsnummer == SelectedArticle.Produktionsnummer);
                }
                else
                {
                    ExistProductionNo = false;
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
                IsBaseNextEnabeld = !(_existLVSNr && ExistProductionNo);
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
                IsBaseNextEnabeld = !(_existProductionNo && ExistLVSNr);
                BackgroundColorSearchProductionNo = ValueToColorConverter.BooleanConvert(_existProductionNo);
            }
        }

        public async Task<bool> UpdateArticleScanCheck()
        {
            IsBusy = true;

            ResponseArticle resArticle = new ResponseArticle();
            resArticle.Article = SelectedArticle.Copy();
            resArticle.AppProcess = Common.Enumerations.enumAppProcess.StoreOut;

            api_Article _apiArticle = new api_Article();
            var result = await _apiArticle.POST_Article_Update_ScanValue(resArticle);
            IsBusy = false;
            return result.Success;
        }
    }
}
