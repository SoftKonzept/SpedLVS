using LvsScan.Portable.ViewModels.StoreOut;
using LvsScan.Portable.ViewModels.Wizard;
using LvsScan.Portable.Views.Menu;
using LvsScan.Portable.Views.Wizard;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.StoreOut
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pw0_PrintHost : ContentPage
    {
        public pw0_PrintHost()
        {
            int iCountList = 4;
            int iCount = 1;
            var wizardPageItemVehicle = new WizardItemViewModel("Erfassung Fahrzeugdaten - Schritt " + iCount.ToString() + " von " + iCountList.ToString(), typeof(a1w4_InputVehicle), new a1w4_InputVehicleViewModel());
            iCount++;
            var wizardPageSignature = new WizardItemViewModel("Unterschrift - Schritt " + iCount.ToString() + " von " + iCountList.ToString(), typeof(wiz_Signature), new wiz_SignatureViewModel());
            iCount++;
            var wizardPageItemPrint = new WizardItemViewModel("Druckaufträge - Schritt " + iCount.ToString() + " von " + iCountList.ToString(), typeof(a1w6_InputPrint), new a1w6_InputPrintViewModel());
            iCount++;
            var wizardPageItemPrintSetting = new WizardItemViewModel("Druck Settings - Schritt " + iCount.ToString() + " von " + iCountList.ToString(), typeof(a1w7_InputPrintSetting), new a1w7_InputPrintSettingsViewModel());

            Content = new WizardContentView
                        (
                            new List<WizardItemViewModel>()
                            {
                                wizardPageItemVehicle,
                                wizardPageSignature,
                                wizardPageItemPrint,
                                wizardPageItemPrintSetting,
                            }
                            , false
                            , "NEXT"
                            , "<"
                            , "Drucken"
                            , "<<"
                            , Color.Blue
                        );

            InitializeComponent();
        }

        private void Home_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new FlyoutMenuPage();
        }
        private async void SubmenuStoreOut_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SubMenuStoreOutPage());
        }
    }
}