using Common.Helper;
using Common.Models;
using LvsScan.Portable.Models;
using LvsScan.Portable.Services;
using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Telerik.XamarinForms.Primitives;

namespace LvsScan.Portable.ViewModels.Inventory
{
    public class i3w2_wizInventoryResultViewMode : BaseViewModel
    {
        public StoredLocationStringToStoreLocationItem locItem = new StoredLocationStringToStoreLocationItem();
        public i3w2_wizInventoryResultViewMode()
        {
            BackgroundColorStoreLocation = ValueToColorConverter.TranspartenBackgroundColor();
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
            set { isManual = value; }
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
                if (WizardData is WizardData)
                {
                    WizardData.Wiz_Inventory.SelectedInventoryArticle_InputSearch = _selectedInventoryArticle;
                }
                if (_selectedInventoryArticle != null)
                {
                    Article = _selectedInventoryArticle.Artikel.Copy();
                }
            }
        }

        private Articles _article = new Articles();
        public Articles Article
        {
            get { return _article; }
            set { SetProperty(ref _article, value); }
        }

        private bool _ShowScanInput = true;
        public bool ShowScanInput
        {
            get { return _ShowScanInput; }
            set { SetProperty(ref _ShowScanInput, value); }
        }

        private bool _ShowOrtCheck = false;
        public bool ShowOrtCheck
        {
            get { return _ShowOrtCheck; }
            set { SetProperty(ref _ShowOrtCheck, value); }
        }

        private int _countArticleChecked = 0;
        public int CountArticleChecked
        {
            get { return _countArticleChecked; }
            set { SetProperty(ref _countArticleChecked, value); }
        }

        private int _positionCarouselView = 0;
        public int PositionCarouselView
        {
            get { return _positionCarouselView; }
            set { SetProperty(ref _positionCarouselView, (value + 1)); }
        }

        private bool _isDefaultLagerOrtString = false;
        public bool IsDefaultLagerOrtString
        {
            get { return _isDefaultLagerOrtString; }
            set { SetProperty(ref _isDefaultLagerOrtString, value); }
        }



        private string scannedLagerOrt = String.Empty;
        public string ScannedLagerOrt
        {
            get { return scannedLagerOrt; }
            set
            {
                SetProperty(ref scannedLagerOrt, value);
                if (scannedLagerOrt.Equals("#####"))
                {
                    scannedLagerOrt = string.Empty;
                }
                IsDefaultLagerOrtString = (
                                            (scannedLagerOrt.Equals(StoredLocationStringToStoreLocationItem.const_NullStoredLocationString)) ||
                                            (scannedLagerOrt.Equals(string.Empty))
                                         );
                if (!IsDefaultLagerOrtString)
                {
                    if (SelectedInventoryArticle != null)
                    {
                        locItem = new StoredLocationStringToStoreLocationItem();
                        locItem.ArticledataToStoreLocationString(SelectedInventoryArticle.Artikel);
                        string tmpFormatSL = locItem.FormatToCompareLocationString(scannedLagerOrt);
                        bool tmpCompare = locItem.StoredLocationString.Equals(tmpFormatSL);

                        BackgroundColorStoreLocation = ValueToColorConverter.StringCompareConvert(tmpFormatSL, locItem.StoredLocationString);
                        DoStoreLocationChange = ((SelectedInventoryArticle.Artikel.Id > 0) && (!tmpCompare));

                        // Article was find and stored location ok
                        //IsBaseNextEnabeld = ((!IsStoreLocationChange) && (tmpCompare));                      
                    }
                }
                else
                {
                    //if (IsStoreLocationChange)
                    //{
                    //    IsBaseNextEnabeld = true;
                    //}
                    BackgroundColorStoreLocation = ValueToColorConverter.TranspartenBackgroundColor();
                    DoStoreLocationChange = false;
                }
            }
        }

        private bool _doStoreLocationChange = false;
        public bool DoStoreLocationChange
        {
            get { return _doStoreLocationChange; }
            set { SetProperty(ref _doStoreLocationChange, value); }
        }

        //private bool _isStoreLocationChange = false;
        //public bool IsStoreLocationChange
        //{
        //    get { return _isStoreLocationChange; }
        //    set { SetProperty(ref _isStoreLocationChange, value); }
        //}
        public async Task<Common.ApiModels.ResponseStoreLocationChange> ChangeStoreLocation()
        {
            IsBusy = true;
            locItem = new StoredLocationStringToStoreLocationItem();
            locItem.Convert(ScannedLagerOrt);

            SelectedInventoryArticle.Artikel.Werk = locItem.WerkInput;
            SelectedInventoryArticle.Artikel.Halle = locItem.HalleInput;
            SelectedInventoryArticle.Artikel.Reihe = locItem.ReiheInput;
            SelectedInventoryArticle.Artikel.Ebene = locItem.EbeneInput;
            SelectedInventoryArticle.Artikel.Platz = locItem.PlatzInput;


            api_Article apiArticle = new Services.api_Article();
            var result = await apiArticle.POST_Article_Update_StoreLocation(SelectedInventoryArticle.Artikel);
            var ResponseStoreLocChange = result.Copy();
            SelectedInventoryArticle.Artikel = result.Article.Copy();
            Article = result.Article.Copy();
            //BackgroundColorStoreLocation = ValueToColorConverter.TranspartenBackgroundColor();
            IsBusy = false;
            return ResponseStoreLocChange;
        }

        public async Task<bool> UpdateInventoryArticleStatus()
        {
            IsBusy = true;
            api_Inventory apiInv = new Services.api_Inventory();
            var response = await apiInv.POST_Update_InventoryArticle_Status(SelectedInventoryArticle);
            SelectedInventoryArticle = response.InventoryArticle.Copy();

            InventoryArticles toRemove = WizardData.Wiz_Inventory.InventoryArticlesList.FirstOrDefault(x => x.Id == SelectedInventoryArticle.Id);
            WizardData.Wiz_Inventory.InventoryArticlesList.Remove(toRemove);
            WizardData.Wiz_Inventory.InventoryArticlesList.Add(SelectedInventoryArticle);

            IsBusy = false;
            return response.Success;
        }

        private string _WerkInput = String.Empty;
        public string WerkInput
        {
            get { return _WerkInput; }
            set
            {
                SetProperty(ref _WerkInput, value);
                WerkColor = SetResultColor(SelectedInventoryArticle.Artikel.Werk, _WerkInput);

                locItem = new StoredLocationStringToStoreLocationItem();
                locItem.Reverse(_WerkInput, HalleInput, ReiheInput, EbeneInput, PlatzInput);
                ScannedLagerOrt = locItem.StoredLocationString;
            }
        }
        private string _HalleInput = String.Empty;
        public string HalleInput
        {
            get { return _HalleInput; }
            set
            {
                SetProperty(ref _HalleInput, value);
                HalleColor = SetResultColor(SelectedInventoryArticle.Artikel.Halle, _HalleInput);

                locItem = new StoredLocationStringToStoreLocationItem();
                locItem.Reverse(WerkInput, _HalleInput, ReiheInput, EbeneInput, PlatzInput);
                ScannedLagerOrt = locItem.StoredLocationString;
            }
        }
        private string _ReiheInput = String.Empty;
        public string ReiheInput
        {
            get { return _ReiheInput; }
            set
            {
                SetProperty(ref _ReiheInput, value);
                ReiheColor = SetResultColor(SelectedInventoryArticle.Artikel.Reihe, _ReiheInput);

                locItem = new StoredLocationStringToStoreLocationItem();
                locItem.Reverse(WerkInput, HalleInput, _ReiheInput, EbeneInput, PlatzInput);
                ScannedLagerOrt = locItem.StoredLocationString;
            }
        }
        private string _EbeneInput = String.Empty;
        public string EbeneInput
        {
            get { return _EbeneInput; }
            set
            {
                SetProperty(ref _EbeneInput, value);
                EbeneColor = SetResultColor(SelectedInventoryArticle.Artikel.Ebene, _EbeneInput);

                locItem = new StoredLocationStringToStoreLocationItem();
                locItem.Reverse(WerkInput, HalleInput, ReiheInput, _EbeneInput, PlatzInput);
                ScannedLagerOrt = locItem.StoredLocationString;
            }
        }
        private string _PlatzInput = String.Empty;
        public string PlatzInput
        {
            get { return _PlatzInput; }
            set
            {
                SetProperty(ref _PlatzInput, value);
                PlatzColor = SetResultColor(SelectedInventoryArticle.Artikel.Platz, _PlatzInput);

                locItem = new StoredLocationStringToStoreLocationItem();
                locItem.Reverse(WerkInput, HalleInput, ReiheInput, EbeneInput, _PlatzInput);
                ScannedLagerOrt = locItem.StoredLocationString;
            }
        }


        /// <summary>
        ///             Color 
        ///              - salmon #FFFA8072
        ///              - greenyellow #FFADFF2F 
        /// </summary>
        /// <param name="valOne"></param>
        /// <param name="valTwo"></param>
        /// <returns></returns>
        private Color SetResultColor(string valOne, string valTwo)
        {
            string colorcode = "#FFFA8072";
            int argb = Int32.Parse(colorcode.Replace("#", ""), NumberStyles.HexNumber);
            Color colorReturn = Color.FromArgb(argb);

            if ((valOne != null) && (valTwo != null))
            {
                if (
                        (valOne.Equals(valTwo)) ||
                        ((valOne.Equals("0")) && (valTwo.Equals(string.Empty))) ||
                        ((valTwo.Equals("0")) && (valOne.Equals("0")))
                   )
                {
                    colorcode = "#FFADFF2F";
                    argb = Int32.Parse(colorcode.Replace("#", ""), NumberStyles.HexNumber);
                    colorReturn = Color.FromArgb(argb);
                }
            }
            return colorReturn;
        }


        private Color _backgroundColorStoreLocation;
        public Color BackgroundColorStoreLocation
        {
            get { return _backgroundColorStoreLocation; }
            set { SetProperty(ref _backgroundColorStoreLocation, value); }
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

        public void ClearStoredLocation()
        {
            WerkInput = string.Empty;
            HalleInput = string.Empty;
            ReiheInput = string.Empty;
            EbeneInput = string.Empty;
            PlatzInput = string.Empty;
            ScannedLagerOrt = string.Empty; // StoredLocationStringToStoreLocationItem.const_NullStoredLocationString;
        }
    }
}

