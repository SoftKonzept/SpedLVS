using LvsScan.Portable.ViewModels.StoreIn.Manual;
using LvsScan.Portable.ViewModels.Test;
using LvsScan.Portable.ViewModels.Wizard;
using LvsScan.Portable.Views.Menu;
using LvsScan.Portable.Views.StoreIn.Manual;
using LvsScan.Portable.Views.Wizard;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.Test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class me_wArticleInput_wizHost : ContentPage
    {
        public me_wArticleInput_wizHost()
        {
            var wizardPageOne = new WizardItemViewModel("Auswahl Artikel", typeof(me_TestFirstPage), new me_TestFirstPageViewModel());
            //var wizardPageTwo = new WizardItemViewModel("Erfassung: Abessungen", typeof(me_art_InputDimensions), new me_art_InputDimensionsNoViewModel());
            //var wizardPageThree = new WizardItemViewModel("Erfassung: Gewichte", typeof(me_art_InputWeight), new me_art_InputWeightViewModel());
            var wizardPageFour = new WizardItemViewModel("Erfassung: Referenzen", typeof(me_art_InputReferences), new me_art_InputReferencesViewModel());
            var wizardPageOrt = new WizardItemViewModel("Erfassung: Lagerort", typeof(wiz_ScanStoredLocationArticle), new wiz_ScanStoredLocationArticleViewModel());
            var wizardPageFive = new WizardItemViewModel("Erfassung: Schäden", typeof(wiz_DamageSelection), new wiz_DamageSelectionViewModel());
            var wizardPageSix = new WizardItemViewModel("Erfassung: Foto", typeof(wiz_TakePhoto), new wiz_TakePhotoViewModel());
            var wizardPageSeven = new WizardItemViewModel("Erfassung: Check", typeof(wiz_ArticleCheck), new wiz_ArticleCheckViewModel());


            Content = new WizardContentView
                        (
                            new List<WizardItemViewModel>()
                            {
                                wizardPageOne
                                //,wizardPageTwo
                                //,wizardPageThree
                                ,wizardPageFour
                                ,wizardPageOrt
                                ,wizardPageFive
                                ,wizardPageSix
                                ,wizardPageSeven
                            } //, wizardPageItem2, wizardPageItem3 }
                            , false
                            , ">>"
                            , "<"
                            , "ZUM EINGANG"
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
            await Navigation.PushAsync(new me_wArticleInput_wizHost());
        }
    }
}