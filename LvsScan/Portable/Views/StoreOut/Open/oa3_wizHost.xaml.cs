using LvsScan.Portable.ViewModels.StoreOut.Open;
using LvsScan.Portable.ViewModels.Wizard;
using LvsScan.Portable.Views.Wizard;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.StoreOut.Open
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class oa3_wizHost : ContentPage
    {
        public oa3_wizHost()
        {
            int iCountList = 4;
            int iCount = 1;

            var wizardPageItemScan = new WizardItemViewModel("Artikel-Scan - Schritt " + iCount.ToString() + " von " + iCountList.ToString(), typeof(oa3w1_wizArticelScan), new oa3w1_wizArticelScanViewModel());
            iCount++;
            var wizardPageItemDamage = new WizardItemViewModel("Schadenserfassung - Schritt " + iCount.ToString() + " von " + iCountList.ToString(), typeof(wiz_DamageSelection), new wiz_DamageSelectionViewModel());
            iCount++;
            var wizardPageItemImage = new WizardItemViewModel("Bilder hinterlegen - Schritt " + iCount.ToString() + " von " + iCountList.ToString(), typeof(wiz_TakePhoto), new wiz_TakePhotoViewModel());
            iCount++;
            var wizardPageItemCheck = new WizardItemViewModel("Artikel checken - Schritt " + iCount.ToString() + " von " + iCountList.ToString(), typeof(wiz_ArticleCheck), new wiz_ArticleCheckViewModel());

            //var wizardPageItem3 = new WizardItemViewModel("Schritt 3 von 3", typeof(wiz_DamageSelection), new wiz_DamageSelectionViewModel());
            //var wizardPageItem4 = new WizardItemViewModel("Schritt 2 von 2", typeof(wiz_p), new wiz_DamageSelectionViewModel());
            //var wizardPageItem2 = new WizardItemViewModel("Schritt 2 von 2", typeof(wiz_DamageSelection), new wiz_DamageSelectionViewModel());

            Content = new WizardContentView
                        (
                            new List<WizardItemViewModel>() { wizardPageItemScan, wizardPageItemDamage, wizardPageItemImage, wizardPageItemCheck }
                            , false
                            , "NEXT"
                            , "<"
                            , "CHECK UND ZUR ARTIKELLISTE"
                            , "<<"
                            , Color.Blue
                        );
            InitializeComponent();
        }
    }
}