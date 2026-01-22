using LvsScan.Portable.Controls;
using LvsScan.Portable.ViewModels.Wizard;
using LvsScan.Portable.Views.Menu;
using LvsScan.Portable.Views.Wizard;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.StoreIn.Edi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ee1_w0_wizHost : ContentPage
    {
        public ee1_w0_wizHost()
        {
            var wizardPageItem1 = new WizardItemViewModel(string.Empty, typeof(wiz_ScanSearchArticle), new wiz_ScanSearchArticleViewModel());
            //var wizardPageItem2 = new WizardItemViewModel("Schritt 2 von 3", typeof(wiz_ScanStoredLocationArticle), new wiz_ScanStoredLocationArticleViewModel());

            Content = new WizardContentView
                        (
                            new List<WizardItemViewModel>() { wizardPageItem1 } //, wizardPageItem2, wizardPageItem3 }
                            , false
                            , "Eingang erstellen"
                            , "<"
                            , "ZUR DFÜ Liste"
                            , "<<"
                            , Color.Blue
                        );
            InitializeComponent();
        }

        private void InitToolMenu()
        {
            this.ToolbarItems.Clear();
            List<Xamarin.Forms.ToolbarItem> tmpList = ctr_MenuToolBarItem.CreateMenuStoreIn(false, false, false);
            foreach (var item in tmpList)
            {
                item.Clicked += toolBarItem_Clicked;
                this.ToolbarItems.Add(item);
            }
        }

        private async void toolBarItem_Clicked(object sender, EventArgs e)
        {
            Xamarin.Forms.ToolbarItem tmpTbi = (Xamarin.Forms.ToolbarItem)sender;
            switch (tmpTbi.AutomationId)
            {
                case "1":
                    Application.Current.MainPage = new FlyoutMenuPage();
                    break;
                case "2":
                    await Navigation.PushAsync(new SubMenuStoreInPage());
                    break;
            }
        }
    }
}