using Common.ApiModels;
using Common.Enumerations;
using Common.Helper;
using Common.Models;
using Common.Views;
using LvsScan.Portable.Models;
using LvsScan.Portable.Services;
using LvsScan.Portable.ViewModels.Wizard;
using LvsScan.Portable.Views.Menu;
using LvsScan.Portable.Views.StoreIn.Manual;
using LvsScan.Portable.Views.StoreIn.Open;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Telerik.XamarinForms.Primitives;
using Xamarin.Forms;

namespace LvsScan.Portable.ViewModels.StoreIn
{
    public class SubStoreInScannerViewModel : BaseViewModel
    {
        public SubStoreInScannerViewModel()
        {
            IsBusy = false;
            InitMenu();
        }

        public void InitMenu()
        {
            List<MenuSubItem> tmpSubMenuItems = new List<MenuSubItem>();

            MenuSubItem tmpSub = new MenuSubItem
            {
                Id = 1,
                Title = "offene Eingänge",
                SubText = "Bearbeitung von bestehenden, offenen Eingänge",
                TargetType = typeof(oe1_OpenStoreInListPage),
                ArtMainMenu = Enumerations.enumMainMenu.StoreIn,
                ShowButton = false,
                BackgroundColor = ValueToColorConverter.ViewBackgroundColorSubmenu()
            };
            tmpSubMenuItems.Add(tmpSub);

            tmpSub = new MenuSubItem
            {
                Id = 1,
                Title = "manuelle Eingänge",
                SubText = "Erfassung und Bearbeitung manuellen Eingängen",
                TargetType = typeof(me_wEingang_wizHost),
                ArtMainMenu = Enumerations.enumMainMenu.StoreIn,
                ShowButton = false,
                BackgroundColor = ValueToColorConverter.ViewBackgroundColorSubmenu()
            };
            tmpSubMenuItems.Add(tmpSub);




            SubMenuItems = null;
            SubMenuItems = new ObservableCollection<MenuSubItem>(tmpSubMenuItems);
        }

        private ObservableCollection<MenuSubItem> _SubMenuItems;
        public ObservableCollection<MenuSubItem> SubMenuItems
        {
            get { return _SubMenuItems; }
            set { SetProperty(ref _SubMenuItems, value); }
        }

        private MenuSubItem _SelectedMenuItem;
        public MenuSubItem SelectedMenuItem
        {
            get { return _SelectedMenuItem; }
            set { SetProperty(ref _SelectedMenuItem, value); }
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



        private enumAppProcess _AppProcess = enumAppProcess.StoreIn;
        internal wiz_ScanSearchArticle_Helper helper;

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
            }
        }
        public enumAppProcess AppProcess
        {
            get { return _AppProcess; }
            set
            {
                SetProperty(ref _AppProcess, value);
                helper = new wiz_ScanSearchArticle_Helper(_AppProcess, StoreInArt, StoreOutArt);
                IsProdctionNoInputVisible = helper.IsProductionnoInputVisible;
            }
        }

        private ObservableCollection<AsnArticleView> _AsnArticleList = new ObservableCollection<AsnArticleView>();
        public ObservableCollection<AsnArticleView> AsnArticleList
        {
            get { return _AsnArticleList; }
            set
            {
                SetProperty(ref _AsnArticleList, value);
                if (_AsnArticleList.Count > 0)
                {
                    foreach (var item in _AsnArticleList)
                    {
                        item.IsSearchResult = false;
                        if (item.Produktionsnummer.Equals(SearchProductionNo))
                        {
                            item.IsSearchResult = true;
                        }
                    }
                }
            }
        }

        private ObservableCollection<AsnLfsView> _AsnLfsList = new ObservableCollection<AsnLfsView>();
        public ObservableCollection<AsnLfsView> AsnLfsList
        {
            get { return _AsnLfsList; }
            set
            {
                SetProperty(ref _AsnLfsList, value);

            }
        }

        private DateTime _AsnListTimeStamp = new DateTime(1900, 1, 1);
        public DateTime AsnListTimeStamp
        {
            get { return _AsnListTimeStamp; }
            set
            {
                SetProperty(ref _AsnListTimeStamp, value);
            }
        }

        private Eingaenge _EingangToSearch = new Eingaenge();
        public Eingaenge AusgangToSearch
        {
            get { return _EingangToSearch; }
            set
            {
                SetProperty(ref _EingangToSearch, value);
            }
        }
        public bool IsBaseNextEnabeld(bool myExistProductionNo)
        {
            bool bReturn = false;
            switch (AppProcess)
            {
                case enumAppProcess.StoreIn:
                    switch (StoreInArt)
                    {
                        case enumStoreInArt.open:
                        case enumStoreInArt.edi:
                            //if ((!IsLvsInputVisible) && (IsProductionnoInputVisible))
                            //{
                            //    //Beispiel Einlagerung, da hat man nur die Produktionsnummer
                            //    bReturn = !(myExistProductionNo);
                            //}
                            if (!IsProdctionNoInputVisible)
                            {
                                //Beispiel Einlagerung, da hat man nur die Produktionsnummer
                                bReturn = !(myExistProductionNo);
                            }
                            break;
                    }
                    break;
                case enumAppProcess.StoreOut:
                    //if ((IsLvsInputVisible) && (IsProductionnoInputVisible))
                    //{
                    //    bReturn = !(myExistLvsNr && myExistProductionNo);
                    //}
                    //if (IsLvsInputVisible)
                    //{
                    //    bReturn = !(myExistLvsNr && myExistProductionNo);
                    //}
                    break;
                case enumAppProcess.StoreLocationChange:
                case enumAppProcess.Inventory:
                    //if (!IsProductionnoInputVisible)
                    //{
                    //    //Beipiel Umlagerung
                    //    bReturn = !(myExistLvsNr);
                    //}
                    break;

            }
            return bReturn;
        }

        private bool _IsProdctionNoInputVisible = true;
        public bool IsProdctionNoInputVisible
        {
            get { return _IsProdctionNoInputVisible; }
            set { SetProperty(ref _IsProdctionNoInputVisible, value); }
        }

        private string _searchProductionNo;
        public string SearchProductionNo
        {
            get { return _searchProductionNo; }
            set
            {
                SetProperty(ref _searchProductionNo, value);
            }
        }
        private bool _ExistProductionNo = false;
        public bool ExistProductionNo
        {
            get { return _ExistProductionNo; }
            set
            {
                SetProperty(ref _ExistProductionNo, value);
                //IsBaseNextEnabeld = helper.IsBaseNextEnabeld(_existLVSNr, ExistProductionNo);
                BackgroundColorSearch = ValueToColorConverter.BooleanConvert(_ExistProductionNo);
            }
        }

        private System.Drawing.Color _BackgroundColorSearch;
        public System.Drawing.Color BackgroundColorSearch
        {
            get { return _BackgroundColorSearch; }
            set { SetProperty(ref _BackgroundColorSearch, value); }
        }

        private Eingaenge _SearchEingang = new Eingaenge();
        public Eingaenge SearchEingang
        {
            get { return _SearchEingang; }
            set { SetProperty(ref _SearchEingang, value); }
        }

        private AsnArticleView _SearchAsnArticleView = new AsnArticleView();
        public AsnArticleView SearchAsnArticleView
        {
            get { return _SearchAsnArticleView; }
            set { SetProperty(ref _SearchAsnArticleView, value); }
        }

        private List<Articles> _ListArticleSearch = new List<Articles>();
        public List<Articles> ListArticleSearch
        {
            get { return _ListArticleSearch; }
            set
            {
                SetProperty(ref _ListArticleSearch, value);
            }
        }

        /// <summary>
        ///             Da viele Lieferanten, aber nicht alle, die Produktionsnummer mit einem führenden Buchstaben
        ///             oder Zahl versehen muss dies hier berücksichtigt werden.
        ///             Vorgehen:
        ///             1. Der Check findet in zwei durchgängen statt
        ///             -> 1. Durchgang Check Produktionsnummer ohne das erste Zeichen der gescannten Produktionsnummer
        ///             -> 2. Durchgang Check Produktionsnummer mit der gescannten Produktionsnummer
        /// </summary>
        /// <returns></returns>
        public async Task<bool> GetArticleInEingangByProductionNo()
        {
            bool bReturn = false;
            this.IsBusy = true;
            try
            {
                string ProdCheckValue = string.Empty;

                //for(int x=1; x<3; x++) 
                //{ 
                //    switch(x) 
                //    {
                //        case 1:
                //            ProdCheckValue = SearchProductionNo.Substring(1, SearchProductionNo.Length - 1);
                //            break;
                //        case 2:
                //            ProdCheckValue = SearchProductionNo;
                //            break;
                //    }

                //    api_Article api = new api_Article();
                //    var result = await api.GET_Article_GetArticleInStoreInByProductionNo(ProdCheckValue);
                //    ListArticleSearch = new System.Collections.Generic.List<Articles>(result.ListArticles);
                //    if (result.ListArticles.Count > 0)
                //    {
                //        x = 5;
                //        bReturn = true;
                //        Articles tmpArticle = ListArticleSearch.FirstOrDefault(x => x.Produktionsnummer.Equals(ProdCheckValue));
                //        if (tmpArticle != null)
                //        {
                //            bReturn = true;
                //            SearchEingang = tmpArticle.Eingang.Copy();
                //        }
                //        else
                //        {
                //            List<AsnArticleView> listArt = new List<AsnArticleView>(AsnArticleList.Where(x => x.Produktionsnummer.Contains(ProdCheckValue)));
                //            AsnArticleList = new ObservableCollection<AsnArticleView>(listArt);
                //            List<int> tmpAsnId = new List<int>(AsnArticleList.Select(x => x.AsnId).Distinct().ToList());
                //            List<AsnLfsView> listLfs = new List<AsnLfsView>(AsnLfsList.Where(x => tmpAsnId.Contains(x.AsnId)));
                //        }
                //    }
                //}

                ProdCheckValue = SearchProductionNo;

                api_Article api = new api_Article();
                var result = await api.GET_Article_GetArticleInStoreInByProductionNo(ProdCheckValue);
                ListArticleSearch = new System.Collections.Generic.List<Articles>(result.ListArticles);
                if (result.ListArticles.Count > 0)
                {
                    //x = 5;
                    bReturn = true;
                    Articles tmpArticle = ListArticleSearch.FirstOrDefault(x => x.Produktionsnummer.ToUpper().Equals(ProdCheckValue.ToUpper()));
                    if (tmpArticle != null)
                    {
                        bReturn = true;
                        SearchEingang = tmpArticle.Eingang.Copy();

                        //----- Update Article IdentificationByScan -----------
                        ResponseArticle resArticle = new ResponseArticle();
                        resArticle.Article = tmpArticle.Copy();
                        resArticle.AppProcess = AppProcess;

                        api_Article _apiArticle = new api_Article();
                        var resultIdentification = await _apiArticle.POST_Article_Update_ScanIdentification(resArticle);

                        if (resultIdentification.Success)
                        {
                            //SelectedArticle = result.Article.Copy();
                        }
                    }
                    else
                    {
                        List<AsnArticleView> listArt = new List<AsnArticleView>(AsnArticleList.Where(x => x.Produktionsnummer.Contains(ProdCheckValue)));
                        AsnArticleList = new ObservableCollection<AsnArticleView>(listArt);
                        List<int> tmpAsnId = new List<int>(AsnArticleList.Select(x => x.AsnId).Distinct().ToList());
                        List<AsnLfsView> listLfs = new List<AsnLfsView>(AsnLfsList.Where(x => tmpAsnId.Contains(x.AsnId)));
                    }
                }
            }
            catch (System.Exception ex)
            {
                string str = ex.Message;
            }
            finally
            {
                this.IsBusy = false;
            }

            return bReturn;
        }


        ///             Da viele Lieferanten, aber nicht alle, die Produktionsnummer mit einem führenden Buchstaben
        ///             oder Zahl versehen muss dies hier berücksichtigt werden.
        ///             Vorgehen:
        ///             1. Der Check findet in zwei durchgängen statt
        ///             -> 1. Durchgang Check Produktionsnummer ohne das erste Zeichen der gescannten Produktionsnummer
        ///             -> 2. Durchgang Check Produktionsnummer mit der gescannten Produktionsnummer
        public async Task GetAsnLfsAndArticleListByProductionNo()
        {
            this.IsBusy = true;
            try
            {
                string ProdCheckValue = string.Empty;
                ProdCheckValue = SearchProductionNo;

                api_AsnRead apiAsnRead = new api_AsnRead();
                var result = await apiAsnRead.GET_ASN_GetAsnLfsArticleByProductionnumber(ProdCheckValue, (int)WizardData.LoggedUser.Id);
                if (result.Success)
                {
                    //x = 5;
                    AsnArticleList = new ObservableCollection<AsnArticleView>(result.ListAsnArticleView.ToList());
                    foreach (var item in AsnArticleList)
                    {
                        if (item.Produktionsnummer.Equals(ProdCheckValue))
                        {
                            item.IsSearchResult = true;
                        }
                    }
                    AsnLfsList = new ObservableCollection<AsnLfsView>(result.ListAsnLfsView.ToList());
                    AsnListTimeStamp = DateTime.Now;
                }
                else
                {
                    //await MessageService.ShowAsync("ACHTUNG", result.Error);
                }
            }
            catch (System.Exception ex)
            {
                string str = ex.Message;
            }
            finally
            {
                //InitMenu();
                this.IsBusy = false;
            }
        }



        public async Task GetAsnLfsAndArticleList()
        {
            this.IsBusy = true;
            try
            {
                api_AsnRead apiAsnRead = new api_AsnRead();
                var result = await apiAsnRead.GET_GetLfsArticleListFromAsn();
                if (result.Success)
                {
                    AsnArticleList = new ObservableCollection<AsnArticleView>(result.ListAsnArticleView.ToList());

                    foreach (var item in AsnArticleList)
                    {
                        if (item.Produktionsnummer.Equals(SearchProductionNo))
                        {
                            item.IsSearchResult = true;
                        }
                    }

                    if (((App)Application.Current).WizardData is WizardData)
                    {
                        ((App)Application.Current).WizardData.SaveAsnArticleList = new ObservableCollection<AsnArticleView>(AsnArticleList.ToList());
                    }
                    AsnLfsList = new ObservableCollection<AsnLfsView>(result.ListAsnLfsView.ToList());
                    if (((App)Application.Current).WizardData is WizardData)
                    {
                        ((App)Application.Current).WizardData.SaveAsnLfsList = new ObservableCollection<AsnLfsView>(_AsnLfsList.ToList());
                    }
                    AsnListTimeStamp = DateTime.Now;
                    if (((App)Application.Current).WizardData is WizardData)
                    {
                        ((App)Application.Current).WizardData.SaveAsnListTimeStamp = _AsnListTimeStamp;
                    }
                }
                else
                {
                    await MessageService.ShowAsync("ACHTUNG", result.Error);
                }

                //this.IsBusy = false;
            }
            catch (System.Exception ex)
            {
                string str = ex.Message;
            }
            finally
            {
                InitMenu();
                this.IsBusy = false;
            }
        }

        public bool CheckAsnArticleListForProductionNo()
        {
            bool bReturn = false;
            if (AsnArticleList.Count > 0)
            {
                AsnArticleView asnArticleView = AsnArticleList.FirstOrDefault(x => x.Produktionsnummer.Equals(SearchProductionNo));
                if (asnArticleView != null)
                {
                    SearchAsnArticleView = asnArticleView.Copy();
                    bReturn = true;
                }
            }
            return bReturn;
        }
        private AsnArticleView _SearchAsnArticle = new AsnArticleView();
        public AsnArticleView SearchAsnArticle
        {
            get { return _SearchAsnArticle; }
            set { SetProperty(ref _SearchAsnArticle, value); }
        }





    }
}
