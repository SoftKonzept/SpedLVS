using Common.Models;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels.Inventory;
using LvsScan.Portable.Views.Menu;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.Inventory
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class i2_InventoryArtikelListPage : ContentPage
    {
        public i2_InventoryArtikelListPage()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new i2_InventoryArtikelListViewModel();
            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.Teststring += "InventoryArtikelListPage " + Environment.NewLine;
                ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();

                ViewModel.SelectedInventory = ViewModel.WizardData.Wiz_Inventory.SelectedInventory.Copy();
                ViewModel.LoadValues = true;
                ViewModel.IsInventoryStartEnabled = false;
            }
        }
        public i2_InventoryArtikelListViewModel ViewModel { get; set; }

        private async void StartInventory_Clicked(object sender, System.EventArgs e)
        {
            if (ViewModel != null)
            {
                if (ViewModel.IsInventoryStartEnabled)
                {
                    //----- Takeover of the view model valaue
                    ViewModel.WizardData.Wiz_Inventory.SelectedInventory = this.ViewModel.SelectedInventory.Copy();
                    ViewModel.WizardData.Wiz_Inventory.InventoryArticlesList = this.ViewModel.InventoryArticles.ToList();
                    ViewModel.WizardData.Wiz_Inventory.InvnetoryArticle = this.ViewModel.selectedInventoryArticle;

                    //----- Update Property in App 
                    ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();

                    //var jsonStr = JsonConvert.SerializeObject(ViewModel.WizardData);
                    await Navigation.PushAsync(new i3w0_wizInventoryHost());
                }
            }
        }

        private async void RefreshList_Clicked(object sender, EventArgs e)
        {
            //ViewModel.LoadValues = true;
            await Navigation.PushAsync(new i2_InventoryArtikelListPage());
        }

        private void tbibHome_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new FlyoutMenuPage();
        }

        private async void tbiHome_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new i2_InventoryArtikelListPage());
        }

        private void viewInventoryArticlesUnedited_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InventoryArticles iaPrevious = (e.PreviousSelection.FirstOrDefault()) as InventoryArticles;
            InventoryArticles iaCurrent = (e.CurrentSelection.FirstOrDefault()) as InventoryArticles;
            int iPrevious = 0;
            int iCurrent = 0;
            if (iaPrevious is InventoryArticles)
            {
                iPrevious = iaPrevious.Id;
            }

            if (iaCurrent is InventoryArticles)
            {
                iCurrent = iaCurrent.Id;
            }

            if (iPrevious == iCurrent)
            {
                string str = string.Empty;
            }
        }
    }
}