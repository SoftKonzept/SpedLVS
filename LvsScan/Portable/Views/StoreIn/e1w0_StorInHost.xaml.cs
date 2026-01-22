using LvsScan.Portable.ViewModels.StoreIn;
using LvsScan.Portable.ViewModels.Wizard;
using LvsScan.Portable.Views.Menu;
using LvsScan.Portable.Views.Wizard;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.StoreIn
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class e1w0_StorInHost : ContentPage
    {
        public e1w0_StorInHost()
        {
            var wizardPageItemAuftraggeber = new WizardItemViewModel("Erfassung Auftraggeber|Empfänger - Schritt 1 von 8", typeof(e1w1_einInputAdr), new e1w1_einInputAdrViewModel());
            var wizardPageItemLfs = new WizardItemViewModel("Erfassung Lfs-Daten - Schritt 2 von 8", typeof(e1w2_einInputLfs), new e1w2_einInputLfsViewModel());
            var wizardPageItemSped = new WizardItemViewModel("Erfassung Spedition - Schritt 3 von 8", typeof(e1w3_einInputCarrier), new e1w3_einInputCarrierViewModel());
            var wizardPageItemVehicle = new WizardItemViewModel("Erfassung Fahrzeugdaten - Schritt 4 von 8", typeof(e1w4_einInputVehicle), new e1w4_einInputVehicleViewModel());
            var wizardPageItemArt = new WizardItemViewModel("Auswahl Art - Schritt 5 von 8", typeof(e1w5_einInputArt), new e1w5_einInputArtViewModel());
            var wizardPageItemPrint = new WizardItemViewModel("Auswahl Print - Schritt 6 von 8", typeof(e1w6_einInputPrint), new e1w6_einInputPrintViewModel());
            var wizardPageItemPrintSetting = new WizardItemViewModel("Auswahl Print - Schritt 7 von 8", typeof(e1w7_einInputPrintSetting), new e1w7_einInputPrintSettingViewModel());
            var wizardPageItemCheck = new WizardItemViewModel("Abschluss Eingang - Schritt 8 von 8", typeof(e1w8_einCheck), new e1w8_einInputCheckViewModel());

            Content = new WizardContentView
                        (
                            new List<WizardItemViewModel>()
                            {
                                wizardPageItemAuftraggeber
                                , wizardPageItemLfs
                                , wizardPageItemSped
                                , wizardPageItemVehicle
                                , wizardPageItemArt
                                , wizardPageItemPrint
                                , wizardPageItemPrintSetting
                                , wizardPageItemCheck
                            }
                            , false
                            , "NEXT STEP"
                            , "<"
                            , "Eingang abschließen"
                            , "<<"
                            , Color.Blue
                        );
            InitializeComponent();
        }

        private void Home_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new FlyoutMenuPage();
        }
        private async void SubmenuStoreIn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SubMenuStoreInPage());
        }
    }
}