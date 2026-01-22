using LvsScan.Portable.ViewModels.StoreOut.Call;
using LvsScan.Portable.ViewModels.Wizard;
using LvsScan.Portable.Views.Menu;
using LvsScan.Portable.Views.Wizard;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.StoreOut.Call
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class c2_w0_wizHost : ContentPage
    {
        public c2_w0_wizHost()
        {
            //var wizardPageItem1 = new WizardItemViewModel("Suche Artikel im Abruf-Pool - Schritt 1 von 1", typeof(c2w1_wizView), new c2w1_wizViewModel());
            var wizardPageItem1 = new WizardItemViewModel("Suche Artikel im Abruf-Pool", typeof(c2w1_wizView), new c2w1_wizViewModel());

            Content = new WizardContentView
                        (
                            new List<WizardItemViewModel>() { wizardPageItem1 }  //, wizardPageItem2 }
                            , false
                            , "AUSGANG ERSTELLEN"
                            , "<"
                            , "ZUR ARTIKELLISTE"
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