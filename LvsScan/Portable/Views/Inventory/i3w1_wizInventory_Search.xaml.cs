using Common.Models;
using LvsScan.Portable.Interfaces;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels;
using LvsScan.Portable.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.KeyboardHelper;

namespace LvsScan.Portable.Views.Inventory
{
    public partial class i3w1_wizInventory_Search : ContentView, IWizardView
    {
        /// <summary>
        ///             Wizzard
        ///             Step 1 
        /// </summary>
        public i3w1_wizInventorySearchViewMode ViewModel { get; set; }
        public i3w1_wizInventory_Search(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            initViewModel(currentViewModel);
        }

        public i3w1_wizInventory_Search(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            initViewModel(currentViewModel);
        }
        private void initViewModel(BaseViewModel currentViewModel)
        {
            this.BindingContext = ViewModel = currentViewModel as i3w1_wizInventorySearchViewMode;

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.Teststring += "wizInventorySearchViewMode " + Environment.NewLine;

                ViewModel.SelectedInventory = ViewModel.WizardData.Wiz_Inventory.SelectedInventory.Copy();
                ViewModel.SelectedInventoryArticle = ViewModel.WizardData.Wiz_Inventory.InvnetoryArticle.Copy();
                ViewModel.InventoriesArticlesList = new ObservableCollection<InventoryArticles>(ViewModel.WizardData.Wiz_Inventory.InventoryArticlesList.ToList());
            }
            tabView.SelectedItem = tabView.Items[0];
        }

        public Task OnAppearing()
        {
            string str = string.Empty;
            if (ViewModel.SelectedInventoryArticle is InventoryArticles)
            {
                this.carouselViewArticle.CurrentItem = ViewModel.SelectedInventoryArticle;
            }
            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;
            return Task.CompletedTask;
        }

        async Task IWizardView.OnAppearing()
        {
            string str = string.Empty;
            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;
            if (ViewModel.SelectedInventoryArticle is InventoryArticles)
            {
                this.carouselViewArticle.CurrentItem = ViewModel.SelectedInventoryArticle;
            }

            await Task.Delay(10);
            await Task.Run(() =>
            {
                ViewModel.SearchLvsNo = string.Empty;
                //ViewModel.SearchProduktionsnummer= string.Empty;
                searchLvsNo.Focus();
                //searchProductionsNo.Unfocus();
            });
        }
        public Task OnDissapearing()
        {
            string str = string.Empty;
            SoftKeyboard.Current.VisibilityChanged -= Current_VisibilityChanged;
            SetWizInventoryValue();
            return Task.CompletedTask;
        }

        public Task<bool> OnNext(BaseViewModel viewModel)
        {
            //if (!ViewModel.WizardData.Wiz_Inventory.StepInputSearchFinished)
            //{
            //    carouselViewArticle.CurrentItem = ViewModel.SelectedInventoryArticle;
            //    ViewModel.WizardData.Wiz_Inventory.SelectedInventoryArticle_InputSearch = new InventoryArticles();
            //    ViewModel.WizardData.Wiz_Inventory.ErrorMessageList_InputSearch = new List<string>();
            //    SetWizInventoryValue();
            //}
            //else
            //{
            //    ViewModel.SelectedInventory = ViewModel.WizardData.Wiz_Inventory.SelectedInventory.Copy();
            //    ViewModel.WizardData.Wiz_Inventory.InventoryArticleList_InputSearchResult = new List<InventoryArticles>();
            //    ViewModel.WizardData.Wiz_Inventory.InventoryArticleList_InputSearchResult.AddRange(ViewModel.InventoriesArticlesToEdit.ToList());
            //}

            if (!ViewModel.ExistLVSNr)
            {
                carouselViewArticle.CurrentItem = ViewModel.SelectedInventoryArticle;
                ViewModel.WizardData.Wiz_Inventory.SelectedInventoryArticle_InputSearch = new InventoryArticles();
                ViewModel.WizardData.Wiz_Inventory.ErrorMessageList_InputSearch = new List<string>();
                SetWizInventoryValue();
            }
            else
            {
                ViewModel.SelectedInventory = ViewModel.WizardData.Wiz_Inventory.SelectedInventory.Copy();
                ViewModel.WizardData.Wiz_Inventory.InventoryArticleList_InputSearchResult = new List<InventoryArticles>();
                ViewModel.WizardData.Wiz_Inventory.InventoryArticleList_InputSearchResult.AddRange(ViewModel.InventoriesArticlesToEdit.ToList());
            }
            return Task.FromResult(ViewModel.ExistLVSNr);


            //return true;  // original
        }

        public Task<bool> OnPrevious(BaseViewModel viewModel)
        {
            ViewModel.IsBaseNextEnabeld = false;
            //perform validation here
            //SetWizInventoryValue();            
            return Task.FromResult(true);
        }
        private void Current_VisibilityChanged(SoftKeyboardEventArgs e)
        {
            if (e.IsVisible)
            {
                // do your things
                string str = string.Empty;

                //searchLvsNoManual.Focus();
            }
            else
            {
                // do your things
                string str = string.Empty;
                //searchLvsNo.Focus();
            }
        }
        private bool SetWizInventoryValue()
        {
            if (ViewModel.WizardData.Wiz_Inventory != null)
            {
                int iLvsNo = 0;
                int.TryParse(this.searchLvsNo.Text, out iLvsNo);
                ViewModel.WizardData.Wiz_Inventory.SearchLvsNo = iLvsNo;
                //ViewModel.WizardData.Wiz_Inventory.SearchProduktionsnummer = ViewModel.SearchProduktionsnummer;
                ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
                //WriteBackup();
            }
            return true;
        }

        private async void WriteBackup()
        {
            //string str = string.Empty;
            //try
            //{
            //    //enumFileStorageArt enumfileArt = enumFileStorageArt.publicExternStorage;
            //    //enumMainMenu enumMenu = enumMainMenu.Inventory;
            //    var jsonStr = JsonConvert.SerializeObject(ViewModel.WizardData.Wiz_Inventory);
            //    DependencyService.Get<IFileService>().CreateFile(jsonStr, ViewModel.WizardData.Wiz_Inventory.Fielname, ViewModel.WizardData.Wiz_Inventory.Path);
            //}
            //catch (Exception ex)
            //{
            //    _ = ex.Message.ToString();
            //}
        }

        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            string str = string.Empty;
        }
        /// <summary>
        ///             during item swipe the CurrentItem is placed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void carouselViewArticle_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            if (e.CurrentItem is InventoryArticles)
            {
                //carouselViewArticle.CurrentItem = (InventoryArticles)e.CurrentItem;
                ViewModel.PositionCarouselView = carouselViewArticle.Position;
                ViewModel.SelectedInventoryArticle = (InventoryArticles)e.CurrentItem;
            }
        }
        private void searchArtikelId_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }

        private void searchProduktionsnummer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }

        private async void btnClearLVSNo_Clicked(object sender, EventArgs e)
        {
            ViewModel.SearchLvsNo = String.Empty;
        }

        private async void btnClearPrductionNo_Clicked(object sender, EventArgs e)
        {
            //ViewModel.SearchProduktionsnummer= String.Empty;
        }

        private async void tabView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if ((e != null) && (e.PropertyName.Equals("SelectedItem")))
            {
                if (ViewModel != null)
                {
                    string str = string.Empty;

                    if (ViewModel.IsManual)
                    {
                        await Task.Delay(1000);
                        await Task.Run(() =>
                        {
                            if (ViewModel.ExistLVSNr)
                            {
                                //searchProductionsNoManual.Focus();
                                searchLvsNoManual.Unfocus();
                                searchLvsNo.Unfocus();
                                //searchProductionsNo.Unfocus();
                            }
                            else
                            {
                                searchLvsNoManual.Focus();
                                searchLvsNo.Unfocus();
                                //searchProductionsNo.Unfocus();
                                //searchProductionsNoManual.Unfocus();
                            }
                        });
                    }
                    else
                    {
                        await Task.Delay(1000);
                        await Task.Run(() =>
                        {
                            if (ViewModel.ExistLVSNr)
                            {
                                //searchProductionsNo.Focus();
                                //searchProductionsNoManual.Unfocus();
                                searchLvsNoManual.Unfocus();
                                searchLvsNo.Unfocus();
                            }
                            else
                            {
                                searchLvsNo.Focus();
                                searchLvsNoManual.Unfocus();
                                //searchProductionsNo.Unfocus();
                                //searchProductionsNoManual.Unfocus();
                            }
                        });
                    }
                }
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
        }

        private void TestEnabled_Clicked(object sender, EventArgs e)
        {
            ViewModel.IsBaseNextEnabeld = (!ViewModel.IsBaseNextEnabeld);
        }
    }
}