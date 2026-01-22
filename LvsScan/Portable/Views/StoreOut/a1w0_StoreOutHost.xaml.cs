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
    public partial class a1w0_StoreOutHost : ContentPage
    {
        public a1w0_StoreOutHost()
        {
            int iCountList = 4;
            int iCount = 1;
            var wizardPageItemTermin = new WizardItemViewModel("Erfassung Terminvorgabe - Schritt " + iCount.ToString() + " von " + iCountList.ToString(), typeof(a1w3_InputTermin), new a1w3_InputTerminViewModel());

            iCount++;
            var wizardPageItemVehicle = new WizardItemViewModel("Erfassung Fahrzeugdaten - Schritt " + iCount.ToString() + " von " + iCountList.ToString(), typeof(a1w4_InputVehicle), new a1w4_InputVehicleViewModel());

            iCount++;
            var wizardPageItemInfo = new WizardItemViewModel("Zusatzinformationen - Schritt " + iCount.ToString() + " von " + iCountList.ToString(), typeof(a1w5_InputInfo), new a1w5_InputInfoViewModel());

            iCount++;
            var wizardPageItemCheck = new WizardItemViewModel("Ausgang abschließen - Schritt " + iCount.ToString() + " von " + iCountList.ToString(), typeof(a1w8_InputCheck), new a1w8_InputCheckViewModel());
            Content = new WizardContentView
                        (
                            new List<WizardItemViewModel>()
                            {
                                wizardPageItemTermin,
                                wizardPageItemVehicle,
                                wizardPageItemInfo,
                                wizardPageItemCheck
                            }
                            , false
                            , "NEXT"
                            , "<"
                            , "Ausgang abschließen"
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