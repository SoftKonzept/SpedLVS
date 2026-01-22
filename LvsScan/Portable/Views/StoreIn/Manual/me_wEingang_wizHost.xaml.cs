using LvsScan.Portable.ViewModels.StoreIn.Manual;
using LvsScan.Portable.ViewModels.Wizard;
using LvsScan.Portable.Views.Menu;
using LvsScan.Portable.Views.Wizard;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.StoreIn.Manual
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class me_wEingang_wizHost : ContentPage
    {
        public me_wEingang_wizHost()
        {
            var wizardPageWorkspaceSelection = new WizardItemViewModel("Auswahl: Arbeitsbereich", typeof(me_WorkspaceSelection), new me_WorkspaceSelectionViewModel());
            var wizardPageInputProductionNo = new WizardItemViewModel("Auswahl: Auftraggeber", typeof(me_InputClient), new me_InputClientViewModel());

            Content = new WizardContentView
                        (
                            new List<WizardItemViewModel>()
                            {
                                wizardPageWorkspaceSelection
                                ,wizardPageInputProductionNo
                            } //, wizardPageItem2, wizardPageItem3 }
                            , false
                            , "NEXT"
                            , "<"
                            , "ZUR ARTIKELERFASSUNG"
                            , "<<"
                            , Color.Blue
                        );
            InitializeComponent();
        }

        private void Home_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new FlyoutMenuPage();
        }
        private async void SubmenueEinlagerung_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SubMenuStoreInPage());
        }
    }
}