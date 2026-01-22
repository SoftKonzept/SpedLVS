using Common.Models;
using LvsScan.Portable.Interfaces;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels;
using LvsScan.Portable.ViewModels.StoreIn.Open;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.KeyboardHelper;

namespace LvsScan.Portable.Views.StoreIn.Open
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class oe3_w2_wizArticleView : ContentView, IWizardView
    {
        public oe3_wizArticleViewModel ViewModel;
        public oe3_w2_wizArticleView(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            initViewModel(currentViewModel);
        }
        public oe3_w2_wizArticleView(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            initViewModel(currentViewModel);
        }

        private void initViewModel(BaseViewModel currentViewModel)
        {
            this.BindingContext = ViewModel = currentViewModel as oe3_wizArticleViewModel;

            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;
            // set inactiv
            ViewModel.IsBaseNextEnabeld = true;
            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.Teststring += "oe3_w2_wizArticleView " + System.Environment.NewLine;

                ViewModel.SelectedEingang = ViewModel.WizardData.Wiz_StoreIn.SelectedEingang.Copy();
                ViewModel.SelectedArticle = ViewModel.WizardData.Wiz_StoreIn.ArticleToCheck.Copy();
                ViewModel.ArticlesInEingang = ViewModel.WizardData.Wiz_StoreIn.ArticleInEingang.ToList();
                ViewModel.WizardData.Wiz_StoreIn.StoreInArt = ViewModel.StoreOutArt;
                ViewModel.SetArticleToCheckList();
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
            //throw new NotImplementedException();
            string message = string.Empty;
            string mesInfo = string.Empty;
            //save Article data

            //-- set Scan value for update article data
            ViewModel.SelectedArticle.ScanOut = DateTime.Now;
            ViewModel.SelectedArticle.ScanOutUser = (int)((App)Application.Current).LoggedUser.Id;
            bool bUpdateOK = true;
            bUpdateOK = await ViewModel.UpdateArticleScanCheck();

            ////--- backup in WizardData
            ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();

            if (!bUpdateOK)
            {
                //    message = "Artikeldaten konnten erfolgreich upgedatet werden!" + System.Environment.NewLine;
                //    mesInfo = "INFORMATION";
                //    await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");

                //    await Navigation.PushAsync(new ());
                //    //Application.Current.MainPage = new FlyoutMenuPage();
                //}
                //else
                //{
                message = "Es ist ein Fehler beim Update der Artikeldaten aufgetreten!" + System.Environment.NewLine;
                mesInfo = "ACHTUNG";
                await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
            }
            return bUpdateOK;
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

        private void Button_Clicked(object sender, EventArgs e)
        {
            ViewModel.IsBaseNextEnabeld = (!ViewModel.IsBaseNextEnabeld);
        }

        private async void tabView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if ((e != null) && (e.PropertyName.Equals("SelectedItem")))
            {
                if (ViewModel != null)
                {
                    string str = string.Empty;

                    if (ViewModel.IsManual)
                    {
                        await Task.Delay(1000);
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
                        await Task.Delay(1000);
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
                ViewModel.SelectedArticle = (Articles)e.CurrentItem;
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
    }
}