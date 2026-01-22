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
    public partial class me_wArticleInput_wizHost : ContentPage
    {
        public me_wArticleInput_wizHost()
        {
            var wizardPageStart = new WizardItemViewModel("Start Artikel - Eingabe", typeof(me_StartInput), new me_StartInputViewModel());
            var wizardPageInputProductionNo = new WizardItemViewModel("Erfassung: Artikeldaten -  Schritt 1 von 8", typeof(me_art_InputProductionNo), new me_art_InputProductionNoViewModel());
            var wizardPageInputDimension = new WizardItemViewModel("Erfassung: Abmessungen -  Schritt 2 von 8", typeof(me_art_InputDimensions), new me_art_InputDimensionsNoViewModel());
            var wizardPageInputWeight = new WizardItemViewModel("Erfassung: Gewichte -  Schritt 3 von 8", typeof(me_art_InputWeight), new me_art_InputWeightViewModel());
            var wizardPageReferences = new WizardItemViewModel("Erfassung: Referenzen -  Schritt 4 von 8", typeof(me_art_InputReferences), new me_art_InputReferencesViewModel());
            var wizardPageOrt = new WizardItemViewModel("Erfassung: Lagerort -  Schritt 5 von 8", typeof(wiz_ScanStoredLocationArticle), new wiz_ScanStoredLocationArticleViewModel());
            var wizardPageDamage = new WizardItemViewModel("Erfassung: Schäden -  Schritt 6 von 8", typeof(wiz_DamageSelection), new wiz_DamageSelectionViewModel());
            var wizardPageImage = new WizardItemViewModel("Erfassung: Foto -  Schritt 7 von 8", typeof(wiz_TakePhoto), new wiz_TakePhotoViewModel());
            var wizardPageCheck = new WizardItemViewModel("Erfassung: Check -  Schritt 8 von 8", typeof(wiz_ArticleCheck), new wiz_ArticleCheckViewModel());



            Content = new WizardContentView
                        (
                            new List<WizardItemViewModel>()
                            {
                                wizardPageStart
                                ,wizardPageInputProductionNo
                                ,wizardPageInputDimension
                                ,wizardPageInputWeight
                                ,wizardPageReferences
                                ,wizardPageOrt
                                ,wizardPageDamage
                                ,wizardPageImage
                                ,wizardPageCheck
                            }
                            , false
                            , ">>"
                            , "<"
                            , "MENU"
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