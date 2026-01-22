using LvsScan.Portable.Controls;
using LvsScan.Portable.ViewModels.StoreIn.Manual;
using LvsScan.Portable.ViewModels.Wizard;
using LvsScan.Portable.Views.Menu;
using LvsScan.Portable.Views.StoreIn.Manual;
using LvsScan.Portable.Views.Wizard;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.StoreIn.Open
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class oe3_w0_wizArticleHost : ContentPage
    {
        public oe3_w0_wizArticleHost()
        {
            var wizardPageItemScan = new WizardItemViewModel("Artikel-Scan - Schritt 1 von 7", typeof(wiz_ScanSearchArticle), new wiz_ScanSearchArticleViewModel());

            var wizardPageInputDimension = new WizardItemViewModel("Check: Abmessungen -  Schritt 2 von 7", typeof(me_art_InputDimensions), new me_art_InputDimensionsNoViewModel());
            var wizardPageInputWeight = new WizardItemViewModel("Check: Gewichte -  Schritt 3 von 7", typeof(me_art_InputWeight), new me_art_InputWeightViewModel());

            var wizardPageItemDamage = new WizardItemViewModel("Schadenserfassung - Schritt 4 von 7", typeof(wiz_DamageSelection), new wiz_DamageSelectionViewModel());
            var wizardPageItemImage = new WizardItemViewModel("Bilder hinterlegen - Schritt 5 von 7", typeof(wiz_TakePhoto), new wiz_TakePhotoViewModel());
            var wizardPageItemLOrt = new WizardItemViewModel("Lagerort Erfassung - Schritt 6 von 7", typeof(wiz_ScanStoredLocationArticle), new wiz_ScanStoredLocationArticleViewModel());
            var wizardPageItemCheck = new WizardItemViewModel("Artikel checken - Schritt 7 von 7", typeof(wiz_ArticleCheck), new wiz_ArticleCheckViewModel());

            Content = new WizardContentView
                        (
                            new List<WizardItemViewModel>()
                            {
                                wizardPageItemScan,
                                wizardPageInputDimension,
                                wizardPageInputWeight,
                                wizardPageItemDamage,
                                wizardPageItemImage,
                                wizardPageItemLOrt,
                                wizardPageItemCheck
                            }
                            , false
                            , "CHECK"
                            , "<"
                            , "ZUR ARTIKELLISTE"
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