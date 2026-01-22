using Common.Enumerations;
using LvsScan.Portable.Interfaces;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels;
using LvsScan.Portable.ViewModels.Inventory;
using System;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LvsScan.Portable.Views.Inventory
{
    public partial class i3w3_wizInventory_Edit : ContentView, IWizardView
    {
        public i3w3_wizInventory_Edit(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            initViewModel(currentViewModel);
        }
        public i3w3_wizInventoryEditViewMode ViewModel { get; set; }
        public i3w3_wizInventory_Edit(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            initViewModel(currentViewModel);
        }

        private void initViewModel(BaseViewModel currentViewModel)
        {
            this.BindingContext = ViewModel = currentViewModel as i3w3_wizInventoryEditViewMode;

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.Teststring += "wizInventoryResultViewMode " + Environment.NewLine;

                ViewModel.SelectedInventory = ViewModel.WizardData.Wiz_Inventory.SelectedInventory.Copy();
                ViewModel.SelectedInventoryArticle = ViewModel.WizardData.Wiz_Inventory.SelectedInventoryArticle_ShowResult.Copy();
                ViewModel.ErrorMessageList_InputSearch = ViewModel.WizardData.Wiz_Inventory.ErrorMessageList_InputSearch.ToList();
            }
        }
        public async Task OnDissapearing()
        {
        }

        public async Task<bool> OnNext(BaseViewModel viewModel)
        {
            //perform validation here
            ViewModel.WizardData.Wiz_Inventory.SelectedInventory = ViewModel.SelectedInventory.Copy();
            ViewModel.WizardData.Wiz_Inventory.SelectedInventoryArticle_ShowResult = null;
            ViewModel.WizardData.Wiz_Inventory.SelectedInventoryArticle_InputSearch = null;
            ViewModel.WizardData.Wiz_Inventory.ErrorMessageList_InputSearch.Clear();


            var list = ViewModel.WizardData.Wiz_Inventory.InventoryArticlesList.Where(x => x.Status == enumInventoryArticleStatus.Neu | x.Status == enumInventoryArticleStatus.NotSet).ToList();
            if (list.Count > 0)
            {
                // next article for inventory
                ViewModel.WizardData.Wiz_Inventory.InvnetoryArticle = list[0];
                await Navigation.PushAsync(new i3w0_wizInventoryHost());
            }
            else
            {
                await Navigation.PushAsync(new i2_InventoryArtikelListPage());
            }
            return true;
        }

        public async Task<bool> OnPrevious(BaseViewModel viewModel)
        {

            //perform validation here
            return true;
        }

        async Task IWizardView.OnAppearing()
        {
        }
    }
}