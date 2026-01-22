using LvsScan.Portable.Interfaces;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels;
using LvsScan.Portable.ViewModels.Inventory;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.KeyboardHelper;

namespace LvsScan.Portable.Views.Inventory
{
    public partial class i3w2_wizInventory_Result : ContentView, IWizardView
    {
        public i3w2_wizInventoryResultViewMode ViewModel { get; set; }
        public i3w2_wizInventory_Result(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            initViewModel(currentViewModel);
        }
        public i3w2_wizInventory_Result(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            initViewModel(currentViewModel);
        }
        private void initViewModel(BaseViewModel currentViewModel)
        {
            this.BindingContext = ViewModel = currentViewModel as i3w2_wizInventoryResultViewMode;

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.Teststring += "wizInventoryResultViewMode " + Environment.NewLine;

                ViewModel.SelectedInventory = ViewModel.WizardData.Wiz_Inventory.SelectedInventory.Copy();
                ViewModel.SelectedInventoryArticle = ViewModel.WizardData.Wiz_Inventory.InventoryArticleList_InputSearchResult.FirstOrDefault();
            }

            tabView.SelectedItem = tabView.Items[0];
            ViewModel.IsBaseNextEnabeld = false;
            ViewModel.SelectedInventoryArticle.Status = Common.Enumerations.enumInventoryArticleStatus.OK;
        }

        async Task IWizardView.OnAppearing()
        {
            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;
            ViewModel.IsBaseNextEnabeld = false;
            //ViewModel.IsStoreLocationChange = false;

            await Task.Delay(10);
            await Task.Run(() =>
            {
                entryScannedStorePlace.Focus();
            });
        }

        public async Task OnDissapearing()
        {
            SoftKeyboard.Current.VisibilityChanged -= Current_VisibilityChanged;
            //ViewModel.DoStoreLocationChange = false;
            //ViewModel.IsStoreLocationChange = false;
        }

        public async Task<bool> OnNext(BaseViewModel viewModel)
        {
            bool bSuccess = false;

            string message = string.Empty;
            string mesInfo = string.Empty;

            //if ((!ViewModel.IsDefaultLagerOrtString) || (ViewModel.IsStoreLocationChange))
            //{
            ViewModel.SelectedInventoryArticle.Status = Common.Enumerations.enumInventoryArticleStatus.OK;
            ViewModel.SelectedInventoryArticle.ScannedUserId = (int)((App)Application.Current).LoggedUser.Id;

            //--- update InventoryArticle Status
            bSuccess = await ViewModel.UpdateInventoryArticleStatus();
            if (bSuccess)
            {
                ViewModel.WizardData.Wiz_Inventory.StepCheckShowFinished = true;
                ViewModel.WizardData.Wiz_Inventory.ScannedLagerOrt = ViewModel.ScannedLagerOrt;
                ViewModel.WizardData.Wiz_Inventory.InventoryArticleStatus = ViewModel.SelectedInventoryArticle.Status;
                ViewModel.WizardData.Wiz_Inventory.SelectedInventoryArticle_ShowResult = ViewModel.SelectedInventoryArticle;
            }
            else
            {
                message = "Der Artikel-Inventurdatensatz konnte nicht upgedatet werden!" + Environment.NewLine;
                mesInfo = "FEHLER";
                await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
            }
            //}
            //else
            //{
            //    message = "Es wurde kein Lagerort hinterlegt und konnte nicht geprüft werden!" + Environment.NewLine;
            //    mesInfo = "Achtung";
            //    await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
            //}
            return bSuccess;
        }

        public async Task<bool> OnPrevious(BaseViewModel viewModel)
        {
            ViewModel.IsBaseNextEnabeld = true;
            //perform validation here
            return true;
        }

        private async void WriteBackup()
        {
            //string str = string.Empty;
            //try
            //{
            //    enumFileStorageArt enumfileArt = enumFileStorageArt.publicExternStorage;
            //    enumMainMenu enumMenu = enumMainMenu.Inventory;
            //    var jsonStr = JsonConvert.SerializeObject(ViewModel.WizardData.Wiz_Inventory);
            //    DependencyService.Get<IFileService>().CreateFile(jsonStr, ViewModel.WizardData.Wiz_Inventory.Fielname, ViewModel.WizardData.Wiz_Inventory.Path);
            //}
            //catch (Exception ex)
            //{
            //    _ = ex.Message.ToString();
            //}
        }

        private void btnClearScanInput_Clicked(object sender, EventArgs e)
        {
            entryScannedStorePlace.Text = String.Empty;
        }

        private void entryScannedStorePlace_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void btnInventoryArticleStatus_Clicked(object sender, EventArgs e)
        {
            ViewModel.WizardData.Wiz_Inventory.InventoryArticleStatus = Common.Enumerations.enumInventoryArticleStatus.Fehlt;
        }
        private async void btnStoreLocationChange_Clicked(object sender, EventArgs e)
        {
            string message = "Die Eingabe entspricht nicht der korrekten Lagerort - Formatierung!" + Environment.NewLine;
            string mesInfo = string.Empty;

            if (ViewModel.ScannedLagerOrt.Where(f => (f == '#')).Count() == 5)
            {
                // Umbuchung durchführen
                var response = await ViewModel.ChangeStoreLocation();
                if (response != null)
                {
                    //ViewModel.IsStoreLocationChange = response.SuccessStoreLocationChange;

                    if (response.SuccessStoreLocationChange)
                    {
                        ViewModel.SelectedInventoryArticle.Text += DateTime.Now.ToString("dd.MM.yyyy - HH:mm") + " - Umlagerung durchgeführt" + Environment.NewLine;
                        mesInfo = "Information";
                        message = "Die Lagerort wurde erfolgreich korrigiert!" + Environment.NewLine;
                    }
                    else
                    {
                        mesInfo = "Fehler";
                        message = "Die Eingabe entspricht nicht der korrekten Lagerort - Formatierung!" + Environment.NewLine;
                    }
                }
            }
            else
            {
                mesInfo = "Fehler";
                message = "Die Eingabe entspricht nicht der korrekten Lagerort - Formatierung!" + Environment.NewLine;
            }
            ViewModel.ClearStoredLocation();
            await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
        }

        private async void tabView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            string str = string.Empty;
            if ((e != null) && (e.PropertyName.Equals("SelectedItem")))
            {
                if (ViewModel != null)
                {
                    if (ViewModel.IsManual)
                    {
                        await Task.Delay(1000);
                        await Task.Run(() =>
                        {
                            entryWerk.Focus();
                            entryHalle.Unfocus();
                            entryReihe.Unfocus();
                            entryEbene.Unfocus();
                            entryPlatz.Unfocus();
                            entryScannedStorePlace.Unfocus();
                        });
                    }
                    else
                    {
                        await Task.Delay(1000);
                        await Task.Run(() =>
                        {
                            entryScannedStorePlace.Focus();
                            entryWerk.Unfocus();
                            entryHalle.Unfocus();
                            entryReihe.Unfocus();
                            entryEbene.Unfocus();
                            entryPlatz.Unfocus();
                        });
                    }
                }
            }
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

    }
}