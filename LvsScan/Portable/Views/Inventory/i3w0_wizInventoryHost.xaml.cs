using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels.Inventory;
using LvsScan.Portable.ViewModels.Wizard;
using LvsScan.Portable.Views.Menu;
using LvsScan.Portable.Views.Wizard;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.Inventory
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class i3w0_wizInventoryHost : ContentPage
    {
        // public wizInventroyHost(string myContent)
        public i3w0_wizInventoryHost()
        {
            var wizardPageItem1 = new WizardItemViewModel("Schritt 1 von 3", typeof(i3w1_wizInventory_Search), new i3w1_wizInventorySearchViewMode());
            var wizardPageItem2 = new WizardItemViewModel("Schritt 2 von 3", typeof(i3w2_wizInventory_Result), new i3w2_wizInventoryResultViewMode());
            var wizardPageItem3 = new WizardItemViewModel("Schritt 3 von 3", typeof(i3w3_wizInventory_Edit), new i3w3_wizInventoryEditViewMode());


            Content = new WizardContentView
                        (
                            new List<WizardItemViewModel>() { wizardPageItem1, wizardPageItem2, wizardPageItem3 }
                            , false
                            , "CHECK"
                            , "<"
                            , "NEXT"
                            , "<<"
                            , Color.Blue
                        );
            InitializeComponent();



            if (((App)Application.Current).WizardData is WizardData)
            {
                ((App)Application.Current).WizardData.Teststring += "wizInventroyHost " + Environment.NewLine;
            }
        }
        private void Home_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new FlyoutMenuPage();
        }
        private async void SubmenuStoreOut_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new i1_InventoryPage());
        }

    }
}