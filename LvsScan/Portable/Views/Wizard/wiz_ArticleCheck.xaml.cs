using Common.Enumerations;
using LvsScan.Portable.Interfaces;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels;
using LvsScan.Portable.ViewModels.Wizard;
using LvsScan.Portable.Views.StoreIn.Manual;
using LvsScan.Portable.Views.StoreIn.Open;
using LvsScan.Portable.Views.StoreOut.Open;
//using Plugin.Media.Abstractions;
//using Plugin.Media;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.KeyboardHelper;

namespace LvsScan.Portable.Views.Wizard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class wiz_ArticleCheck : ContentView, IWizardView
    {
        public wiz_ArticleCheckViewModel ViewModel;
        public wiz_ArticleCheck(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            initViewModel(currentViewModel);
        }
        public wiz_ArticleCheck(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            initViewModel(currentViewModel);
        }

        private void initViewModel(BaseViewModel currentViewModel)
        {
            this.BindingContext = ViewModel = currentViewModel as wiz_ArticleCheckViewModel;

            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;
            // set inactiv
            //ViewModel.IsBaseNextEnabeld = true;
            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.Teststring += "wiz_ArticleCheckViewModel " + System.Environment.NewLine;
                ViewModel.AppProcess = ViewModel.WizardData.AppProcess;

                switch (ViewModel.AppProcess)
                {
                    case enumAppProcess.StoreIn:
                        ViewModel.StoreInArt = ViewModel.WizardData.Wiz_StoreIn.StoreInArt;
                        ViewModel.SelectedEingang = ViewModel.WizardData.Wiz_StoreIn.SelectedEingang.Copy();
                        ViewModel.SelectedArticle = ViewModel.WizardData.Wiz_StoreIn.ArticleToCheck.Copy();

                        //ViewModel.SelectedArticle = ViewModel.WizardData.Wiz_StoreIn.WizDamage.Article.Copy();
                        //ViewModel.CurrentStepStoreInArt = enumStoreInArt_Steps.wizStepInputPhoto;
                        //ViewModel.SelectedEingang = ViewModel.WizardData.Wiz_StoreIn.SelectedEingang.Copy();
                        break;

                    case enumAppProcess.StoreOut:
                        ViewModel.StoreOutArt = ViewModel.WizardData.Wiz_StoreOut.StoreOutArt;
                        ViewModel.SelectedAusgang = ViewModel.WizardData.Wiz_StoreOut.SelectedAusgang.Copy();
                        ViewModel.SelectedArticle = ViewModel.WizardData.Wiz_StoreOut.ArticleToCheck.Copy();
                        break;

                    case enumAppProcess.StoreLocationChange:
                        break;
                    default:
                        break;
                }

            }

        }


        Task<bool> IWizardView.OnPrevious(BaseViewModel viewModel)
        {
            string str = string.Empty;
            //throw new NotImplementedException();

            return Task.FromResult(true);
        }

        Task IWizardView.OnAppearing()
        {
            string str = string.Empty;

            //throw new NotImplementedException();
            return Task.CompletedTask;
        }

        Task IWizardView.OnDissapearing()
        {
            string str = string.Empty;
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }


        async Task<bool> IWizardView.OnNext(BaseViewModel viewModel)
        {
            string message = string.Empty;
            string mesInfo = string.Empty;
            bool bReturn = ViewModel.IsChecked;
            bool bUpdateOK = false;

            if (!ViewModel.IsChecked)
            {
                mesInfo = "ACHTUNG";
                message = "Der Artikel ist nicht gecheckt. Soll der Artikel noch als gecheckt markiert werden?";
                var resonseDisplayAlert = await App.Current.MainPage.DisplayAlert(mesInfo, message, "Check Artikel", "Abbrechen");
                if (resonseDisplayAlert)
                {
                    ViewModel.IsChecked = true;
                }
                bReturn = ViewModel.IsChecked;
            }

            switch (ViewModel.AppProcess)
            {
                case enumAppProcess.StoreIn:
                    if (ViewModel.IsChecked)
                    {
                        bUpdateOK = await ViewModel.UpdateArticleChecked();
                        if (!bUpdateOK)
                        {
                            message = "Es ist ein Fehler beim Update der Artikeldaten aufgetreten!" + System.Environment.NewLine;
                            mesInfo = "ACHTUNG";
                            await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
                        }
                    }

                    ViewModel.WizardData.Wiz_StoreIn.SelectedEingang = ViewModel.SelectedEingang.Copy();
                    ViewModel.WizardData.Wiz_StoreIn.ArticleToCheck = new Common.Models.Articles();
                    ViewModel.WizardData.AppProcess = ViewModel.AppProcess;
                    ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();

                    if (ViewModel.StoreInArt.Equals(enumStoreInArt.manually))
                    {
                        await Navigation.PushAsync(new me_wArticleInput_wizHost());
                    }
                    else
                    {
                        await Navigation.PushAsync(new oe2_ArticleListPage());
                    }
                    break;
                case enumAppProcess.StoreOut:
                    if (ViewModel.IsChecked)
                    {
                        bUpdateOK = await ViewModel.UpdateArticleChecked();
                        if (!bUpdateOK)
                        {
                            message = "Es ist ein Fehler beim Update der Artikeldaten aufgetreten!" + System.Environment.NewLine;
                            mesInfo = "ACHTUNG";
                            await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
                        }
                    }

                    ViewModel.WizardData.Wiz_StoreOut.SelectedAusgang = ViewModel.SelectedAusgang.Copy();
                    ViewModel.WizardData.Wiz_StoreOut.ArticleToCheck = new Common.Models.Articles();
                    ViewModel.WizardData.AppProcess = ViewModel.AppProcess;
                    ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();

                    await Navigation.PushAsync(new oa2_ArticleListPage());

                    //if (bReturn)
                    //{
                    //    await Navigation.PushAsync(new oa2_ArticleListPage());
                    //}
                    break;

                case enumAppProcess.StoreLocationChange:
                    break;
                default:
                    break;
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

        private void btnCheck_Clicked(object sender, EventArgs e)
        {
            ViewModel.IsChecked = !ViewModel.IsChecked;
        }

        // ------------------------------------------------------------------------------------------------------------------------------------


    }
}