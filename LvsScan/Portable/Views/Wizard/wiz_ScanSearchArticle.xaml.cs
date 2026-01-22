using Common.Enumerations;
using Common.Helper;
using Common.Models;
using Common.Views;
using LvsScan.Portable.Interfaces;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels;
using LvsScan.Portable.ViewModels.Wizard;
using LvsScan.Portable.Views.StoreIn;
using LvsScan.Portable.Views.StoreIn.Open;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.KeyboardHelper;

namespace LvsScan.Portable.Views.Wizard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class wiz_ScanSearchArticle : ContentView, IWizardView
    {
        public wiz_ScanSearchArticleViewModel ViewModel;
        public wiz_ScanSearchArticle(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            initViewModel(currentViewModel);
        }
        public wiz_ScanSearchArticle(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            initViewModel(currentViewModel);
        }

        private void initViewModel(BaseViewModel currentViewModel)
        {
            this.BindingContext = ViewModel = currentViewModel as wiz_ScanSearchArticleViewModel;

            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;
            // set inactiv
            ViewModel.IsBaseNextEnabeld = true;
            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.Teststring += "wiz_ScanSearchArticle " + System.Environment.NewLine;
                ViewModel.AppProcess = ViewModel.WizardData.AppProcess;

                switch (ViewModel.WizardData.AppProcess)
                {
                    case enumAppProcess.StoreIn:
                        ViewModel.WizardData.Wiz_StoreIn.ImageOwner = Common.Enumerations.enumImageOwner.Artikel;
                        ViewModel.StoreInArt = ViewModel.WizardData.Wiz_StoreIn.StoreInArt;
                        ViewModel.StoreInArtSteps = enumStoreInArt_Steps.wizStepLastCheckComplete;
                        if (ViewModel.StoreInArt.Equals(enumStoreInArt.edi))
                        {
                            ViewModel.AsnArticlesToCheck = new ObservableCollection<AsnArticleView>(ViewModel.WizardData.Wiz_StoreIn.AsnArticleList);
                            ViewModel.AsnLfsToCheck = new ObservableCollection<AsnLfsView>(ViewModel.WizardData.SaveAsnLfsList);

                            ViewModel.SelectedAsnArticle = ViewModel.WizardData.Wiz_StoreIn.SelectedArticleView;


                            DateTime dtIdentification = new DateTime(1900, 1, 1);
                            if (DateTime.TryParse(ViewModel.SelectedArticle.IdentifiedByScan.ToString(), out dtIdentification))
                            {
                                if (dtIdentification != new DateTime(1900, 1, 1))
                                {
                                    ViewModel.SearchLvsNo = string.Empty;
                                    ViewModel.SearchProduktionsnummer = ViewModel.SelectedAsnArticle.Produktionsnummer.ToString();
                                    //ViewModel.SearchProduktionsnummer = ViewModel.SelectedArticle.Produktionsnummer.ToString();
                                }
                            }
                            //ViewModel.LoadASNValues = true;
                        }
                        else
                        {
                            ViewModel.SelectedEA = (object)ViewModel.WizardData.Wiz_StoreIn.SelectedEingang.Copy();
                            ViewModel.SelectedArticle = ViewModel.WizardData.Wiz_StoreIn.ArticleToCheck.Copy();
                            ViewModel.WizardData.Wiz_StoreIn.WizDamage.Article = ViewModel.SelectedArticle.Copy();
                            ViewModel.ArticlesInEA = ViewModel.WizardData.Wiz_StoreIn.ArticleInEingang.ToList();

                            ViewModel.SetArticleToCheckList();
                            DateTime dtIdentification = new DateTime(1900, 1, 1);
                            if (DateTime.TryParse(ViewModel.SelectedArticle.IdentifiedByScan.ToString(), out dtIdentification))
                            {
                                if (dtIdentification != new DateTime(1900, 1, 1))
                                {
                                    ViewModel.SearchLvsNo = string.Empty;
                                    ViewModel.SearchProduktionsnummer = ViewModel.SelectedArticle.Produktionsnummer.ToString();
                                    //ViewModel.SearchProduktionsnummer = ViewModel.SelectedArticle.Produktionsnummer.ToString();
                                }
                            }
                        }
                        searchProductionsNo.Focus();
                        break;

                    case enumAppProcess.StoreOut:

                        break;
                    default:
                        break;
                }
            }
            tabView.SelectedItem = tabView.Items[0];
        }


        Task<bool> IWizardView.OnPrevious(BaseViewModel viewModel)
        {
            string str = string.Empty;
            //throw new NotImplementedException();

            return Task.FromResult(true);
        }

        async Task IWizardView.OnAppearing()
        {
            string str = string.Empty;

            //// set inactiv
            //ViewModel.IsBaseNextEnabeld = true;

            await Task.Delay(10);
            await Task.Run(() =>
            {
                // set value empty and set focus
                ViewModel.SearchLvsNo = string.Empty;
                ViewModel.SearchProduktionsnummer = string.Empty;
                searchLvsNo.Focus();
                searchProductionsNo.Unfocus();
            });

            //throw new NotImplementedException();
            //return Task.CompletedTask;
        }

        Task IWizardView.OnDissapearing()
        {
            string str = string.Empty;
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }


        async Task<bool> IWizardView.OnNext(BaseViewModel viewModel)
        {
            bool bUpdateOK = false;
            //throw new NotImplementedException();
            string message = string.Empty;
            string mesInfo = string.Empty;
            message = "Die notwendigen Datenfelder Eingabe fehlt! " + System.Environment.NewLine;
            mesInfo = "ACHTUNG";
            //save Article data

            //bUpdateOK = await ViewModel.UpdateArticleScanCheck();
            ////--- backup in WizardData
            //switch (ViewModel.WizardData.AppProcess)
            switch (ViewModel.WizardData.AppProcess)
            {
                case enumAppProcess.StoreIn:
                    switch (ViewModel.StoreInArt)
                    {
                        case enumStoreInArt.open:
                            if (ViewModel.ExistProductionNo)
                            {
                                //ViewModel.WizardData.Wiz_StoreIn.WizDamage.Article = ViewModel.SelectedArticle.Copy();
                                ViewModel.WizardData.Wiz_StoreIn.ArticleToCheck = ViewModel.SelectedArticle.Copy();
                                bUpdateOK = await ViewModel.UpdateArticleScanCheck();
                            }
                            break;
                        case enumStoreInArt.edi:
                            //--- Eingang erstellen
                            if (ViewModel.ExistProductionNo)
                            {
                                var result = await ViewModel.CreateStoreInFromAsn();
                                if (result.Success)
                                {
                                    bUpdateOK = result.Success;

                                    ViewModel.WizardData.Wiz_StoreIn.ArticleToCheck = ViewModel.SelectedArticle.Copy();
                                    var res = await ViewModel.UpdateArticleScanCheck();

                                    message = result.Info;
                                    mesInfo = "INFORMATION";
                                    await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");

                                    // eingang ist ertellt, 
                                    // artikel müssen geprüft werden
                                    // somit ändert sich StoreInArt von edi nach open
                                    this.ViewModel.WizardData.Wiz_StoreIn.StoreInArt = enumStoreInArt.open;
                                    this.ViewModel.WizardData.Wiz_StoreIn.StoreInArtSteps = enumStoreInArt_Steps.NotSet;
                                }
                                else
                                {
                                    message = result.Error;
                                    mesInfo = "FEHLER";
                                    await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
                                }
                            }
                            break;
                    }
                    break;
                case enumAppProcess.StoreOut:
                    if ((ViewModel.ExistProductionNo) && (ViewModel.ExistLVSNr))
                    {
                        ViewModel.WizardData.Wiz_StoreOut.WizDamage.Article = ViewModel.SelectedArticle.Copy();
                    }
                    break;
                default:
                    break;
            }
            ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();

            if (!bUpdateOK)
            {
                //message = "Es ist ein Fehler beim Update der Artikeldaten aufgetreten!" + System.Environment.NewLine;
                //mesInfo = "ACHTUNG";
                await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
                //await Navigation.PushAsync(new SubMenuStoreInPage());
            }
            else
            {
                await GoToNextPage();
            }
            return bUpdateOK;
        }

        private async Task GoToNextPage()
        {
            switch (ViewModel.WizardData.AppProcess)
            {
                case enumAppProcess.StoreIn:
                    switch (ViewModel.StoreInArt)
                    {
                        case enumStoreInArt.open:
                            // Weiterleitung zur nächsten Seite hier über oe3_w0_wizArticleHost.xaml
                            // Schritt 2 von 3 in Prozess OPEN
                            break;
                        case enumStoreInArt.edi:
                            await Navigation.PushAsync(new oe2_ArticleListPage());
                            break;
                    }
                    break;
                case enumAppProcess.StoreOut:

                    break;
                default:
                    await Navigation.PushAsync(new SubMenuStoreInPage());
                    break;
            }
        }
        private void Current_VisibilityChanged(SoftKeyboardEventArgs e)
        {
            if (e.IsVisible)
            {   // do your things
                string str = string.Empty;
            }
            else
            {   // do your things
                string str = string.Empty;
            }
        }

        // ------------------------------------------------------------------------------------------------------------------------------------

        private async void tabView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if ((e != null) && (e.PropertyName.Equals("SelectedItem")))
            {
                if (ViewModel != null)
                {
                    string str = string.Empty;

                    if (ViewModel.IsManual)
                    {
                        await Task.Delay(100);
                        await Task.Run(() =>
                        {
                            if (ViewModel.ExistLVSNr)
                            {
                                searchProductionsNoManual.Focus();
                                searchLvsNoManual.Unfocus();
                                searchLvsNo.Unfocus();
                                searchProductionsNo.Unfocus();
                            }
                            else
                            {
                                searchLvsNoManual.Focus();
                                searchLvsNo.Unfocus();
                                searchProductionsNo.Unfocus();
                                searchProductionsNoManual.Unfocus();
                            }
                        });
                    }
                    else
                    {
                        await Task.Delay(100);
                        await Task.Run(() =>
                        {
                            if (ViewModel.ExistLVSNr)
                            {
                                searchProductionsNo.Focus();
                                searchProductionsNoManual.Unfocus();
                                searchLvsNoManual.Unfocus();
                                searchLvsNo.Unfocus();
                            }
                            else
                            {
                                searchLvsNo.Focus();
                                searchLvsNoManual.Unfocus();
                                searchProductionsNo.Unfocus();
                                searchProductionsNoManual.Unfocus();
                            }
                        });
                    }
                }
            }
        }

        private void carouselViewArticle_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            if (e.CurrentItem is Articles)
            {
                //ViewModel.SelectedArticle = (Articles)e.CurrentItem;

                //int iIndex = ViewModel.ArticlesToCheck.IndexOf(ViewModel.SelectedArticle);
                ////if (!ViewModel.PositionCarouselView.Equals(iIndex))
                ////{
                ////    //ViewModel.PositionCarouselView = iIndex;
                ////}

                //int iLoop = 0;
                //for (int i = 0; i <= ViewModel.ArticlesToCheck.Count-1; i++)
                //{
                //    if (ViewModel.ArticlesToCheck[i].Id.Equals(ViewModel.SelectedArticle.Id))
                //    {
                //        iLoop = i;
                //    }
                //}
            }
        }
        private void carouselViewArticleAsn_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            if (e.CurrentItem is AsnArticleView)
            {
                ViewModel.SelectedAsnArticle = (AsnArticleView)e.CurrentItem;
            }
        }
        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {

        }

        private void btnClearLVSNo_Clicked(object sender, EventArgs e)
        {
            ViewModel.SearchLvsNo = string.Empty;
        }

        private void btnClearPrductionNo_Clicked(object sender, EventArgs e)
        {
            ViewModel.SearchProduktionsnummer = string.Empty;
        }

        private void btnView_Clicked(object sender, EventArgs e)
        {
            ViewModel.ShowCarouselViewArticle = true;
            ViewModel.ShowCarouselViewArticleAsn = false;
        }

        private void btnViewAsn_Clicked(object sender, EventArgs e)
        {
            ViewModel.ShowCarouselViewArticle = false;
            ViewModel.ShowCarouselViewArticleAsn = true;
        }

        private void btnProductionNoDeleteFirstChar_Clicked(object sender, EventArgs e)
        {
            ViewModel.SearchProduktionsnummer = StringValueEdit.RemoveFirtsCharFromValue(ViewModel.SearchProduktionsnummer);
        }

        private void btnLvsNoDeleteFirstChar_Clicked(object sender, EventArgs e)
        {
            ViewModel.SearchLvsNo = StringValueEdit.RemoveFirtsCharFromValue(ViewModel.SearchLvsNo);
        }


    }
}