using Common.Enumerations;
using Common.Helper;
using Common.Models;
using LvsScan.Portable.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.XamarinForms.Primitives;

namespace LvsScan.Portable.ViewModels.Inventory
{
    public class i3w1_wizInventorySearchViewMode : BaseViewModel
    {
        public i3w1_wizInventorySearchViewMode()
        {
            BackgroundColorSearchLvsNr = ValueToColorConverter.BooleanConvert(false);
            //BackgroundColorSearchProductionNo = ValueToColorConverter.BooleanConvert(false);
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
            }
        }

        private Inventories _selectedInventory = new Inventories();
        public Inventories SelectedInventory
        {
            get { return _selectedInventory; }
            set { SetProperty(ref _selectedInventory, value); }
        }

        private ObservableCollection<InventoryArticles> _inventoriesArticlesList = new ObservableCollection<InventoryArticles>();
        public ObservableCollection<InventoryArticles> InventoriesArticlesList
        {
            get { return _inventoriesArticlesList; }
            set
            {
                SetProperty(ref _inventoriesArticlesList, value);

                CountArticleEdited = _inventoriesArticlesList.Where(x => x.Status == enumInventoryArticleStatus.OK).ToList().Count;
                CountArticle = _inventoriesArticlesList.Count;
                Set_InventoriesArticlesToEdit();
            }
        }

        private void Set_InventoriesArticlesToEdit()
        {
            InventoriesArticlesToEdit = new ObservableCollection<InventoryArticles>(InventoriesArticlesList.Where(x => x.Status != enumInventoryArticleStatus.OK).ToList());
            PositionCarouselView = InventoriesArticlesToEdit.ToList().FindIndex(x => x.Id == SelectedInventoryArticle.Id);
        }

        private ObservableCollection<InventoryArticles> _inventoriesArticlesToEdit = new ObservableCollection<InventoryArticles>();
        public ObservableCollection<InventoryArticles> InventoriesArticlesToEdit
        {
            get { return _inventoriesArticlesToEdit; }
            set
            {
                SetProperty(ref _inventoriesArticlesToEdit, value);
            }
        }

        private int _positionCarouselView = 0;
        public int PositionCarouselView
        {
            get { return _positionCarouselView; }
            set
            {
                if ((value > InventoriesArticlesToEdit.Count - 1) || (value < 0))
                {
                    SetProperty(ref _positionCarouselView, 0);
                }
                else
                {
                    SetProperty(ref _positionCarouselView, value);
                }
            }
        }

        private int _countArticleEdited = 0;
        public int CountArticleEdited
        {
            get { return _countArticleEdited; }
            set { SetProperty(ref _countArticleEdited, value); }
        }


        private int _countArticle = 0;
        public int CountArticle
        {
            get { return _countArticle; }
            set { SetProperty(ref _countArticle, value); }
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

                if (WizardData is WizardData)
                {
                    WizardData.Wiz_Inventory.SearchLvsNo = iLvsNr;
                }
                if (iLvsNr > 0)
                {
                    List<InventoryArticles> listArt = InventoriesArticlesToEdit.ToList();
                    List<InventoryArticles> list = listArt.Where(x => x.LvsNummer == iLvsNr).ToList();

                    if (list.Count > 0)
                    {
                        InventoriesArticlesToEdit = new ObservableCollection<InventoryArticles>(list);
                        ExistLVSNr = true;
                    }
                    else
                    {
                        ExistLVSNr = false;
                    }
                }
                else
                {
                    Set_InventoriesArticlesToEdit();
                    ExistLVSNr = false;
                }
            }
        }


        //private string _searchProduktionsnummer = String.Empty;
        //public string SearchProduktionsnummer
        //{
        //    get { return _searchProduktionsnummer; }
        //    set 
        //    { 
        //        SetProperty(ref _searchProduktionsnummer, value.ToUpper());
        //        if (WizardData is WizardData)
        //        {
        //            WizardData.Wiz_Inventory.SearchProduktionsnummer = _searchProduktionsnummer;
        //        }
        //        if ((_searchProduktionsnummer != null) && (_searchProduktionsnummer.Length > 0))
        //        {
        //            ExistProductionNo = (_searchProduktionsnummer == SelectedInventoryArticle.Artikel.Produktionsnummer);
        //        }
        //        else
        //        {
        //            ExistProductionNo = false;
        //        }
        //    }
        //}


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

        private bool _existLVSNr = false;
        public bool ExistLVSNr
        {
            get { return _existLVSNr; }
            set
            {
                SetProperty(ref _existLVSNr, value);
                if (WizardData is WizardData)
                {
                    WizardData.Wiz_Inventory.StepInputSearchFinished = _existLVSNr; // (_existLVSNr && ExistProductionNo);                  
                }
                IsBaseNextEnabeld = !_existLVSNr; // (_existLVSNr && ExistProductionNo);
                BackgroundColorSearchLvsNr = ValueToColorConverter.BooleanConvert(_existLVSNr);
            }
        }

        //private bool _existProductionNo = false;
        //public bool ExistProductionNo
        //{
        //    get { return _existProductionNo; }
        //    set 
        //    { 
        //        SetProperty(ref _existProductionNo, value);
        //        if (WizardData is WizardData)
        //        {
        //            WizardData.Wiz_Inventory.StepInputSearchFinished = (_existProductionNo && ExistLVSNr);
        //        }
        //        IsBaseNextEnabeld = (_existProductionNo && ExistLVSNr);
        //        BackgroundColorSearchProductionNo = ValueToColorConverter.BooleanConvert(_existProductionNo);
        //    }
        //}
        private System.Drawing.Color _backgroundColorSearchLvsNr;
        public System.Drawing.Color BackgroundColorSearchLvsNr
        {
            get { return _backgroundColorSearchLvsNr; }
            set { SetProperty(ref _backgroundColorSearchLvsNr, value); }
        }

        //private System.Drawing.Color _backgroundColorSearchProductionNo;
        //public System.Drawing.Color BackgroundColorSearchProductionNo
        //{
        //    get { return _backgroundColorSearchProductionNo; }
        //    set { SetProperty(ref _backgroundColorSearchProductionNo, value); }
        //}
    }
}
