using LvsScan.Portable.Enumerations;
using LvsScan.Portable.Models;
using LvsScan.Portable.Services;
using LvsScan.Portable.ViewModels.Inventory;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.Inventory
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class i1_InventoryPage : ContentPage
    {
        public i1_InventoryViewModel ViewModel { get; set; }
        public i1_InventoryPage()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new i1_InventoryViewModel();
            //ViewModel.IsStart = true;

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData;
                ViewModel.WizardData.Teststring += "InventoryPage " + Environment.NewLine;
                ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
            }
            ViewModel.LoadValues = true;
        }

        private async void viewInventories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ViewModel != null)
            {
                //------ Initialization all classes 
                if (this.ViewModel.WizardData == null)
                {
                    this.ViewModel.WizardData = new WizardData();
                }
                if (this.ViewModel.WizardData.Wiz_Inventory == null)
                {
                    this.ViewModel.WizardData.Wiz_Inventory = new wizInventory();
                }

                //----- this Settings are only set hier at the start of the process
                //----- filename for the backup
                ViewModel.WizardData.Wiz_Inventory.Fielname = FileService.GetTextFilename(enumMainMenu.Inventory);
                //---- Path ot the backup files
                ViewModel.WizardData.Wiz_Inventory.Path = FileService.GetFilePath(enumFileStorageArt.publicExternStorage, enumMainMenu.Inventory);


                ViewModel.WizardData.Wiz_Inventory.SelectedInventory = this.ViewModel.SelectedItem.Copy();
                ViewModel.WizardData.Wiz_Inventory.InventoryList = this.ViewModel.Inventories.ToList();

                ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();

                await Navigation.PushAsync(new i2_InventoryArtikelListPage());
            }
        }
        /// <summary>
        ///             Refresh Inventory List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshInventories_Clicked(object sender, EventArgs e)
        {
            this.ViewModel.LoadValues = true;
        }
    }
}