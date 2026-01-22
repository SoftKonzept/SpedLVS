using Common.Models;
using LvsScan.Portable.Interfaces;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels;
using LvsScan.Portable.ViewModels.Test;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.KeyboardHelper;

namespace LvsScan.Portable.Views.Test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class me_TestFirstPage : ContentView, IWizardView
    {
        public me_TestFirstPageViewModel ViewModel { get; set; }
        public me_TestFirstPage(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);
        }
        public me_TestFirstPage(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);

        }
        private async void InitView(BaseViewModel currentViewModel)
        {
            if (currentViewModel != null)
            {
                BindingContext = ViewModel = currentViewModel as me_TestFirstPageViewModel;
            }
            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                if (ViewModel.WizardData.Wiz_StoreIn is null)
                {
                    ViewModel.WizardData.Wiz_StoreIn = new wizStoreIn();
                }

                ViewModel.WizardData.Teststring += "me_TestFirstPage " + Environment.NewLine;
                ViewModel.WizardData.LoggedUser = ((App)Application.Current).LoggedUser.Copy();
                ViewModel.Init();
            }
        }

        public Task OnAppearing()
        {
            return Task.CompletedTask;
        }

        public Task OnDissapearing()
        {
            return Task.CompletedTask;
        }

        public Task<bool> OnNext(BaseViewModel viewModel)
        {
            bool bReturn = false;
            bReturn = ((ViewModel.SelectedArticle is Articles) && (ViewModel.SelectedArticle.Id > 0));
            if (bReturn)
            {
                ViewModel.WizardData.Wiz_StoreIn.SelectedEingang = ViewModel.SelectedArticle.Eingang.Copy();
                ViewModel.WizardData.Wiz_StoreIn.ArticleToCheck = ViewModel.SelectedArticle.Copy();
                ViewModel.WizardData.Wiz_StoreIn.StoreInArt = ViewModel.StoreInArt;
                ViewModel.WizardData.AppProcess = Common.Enumerations.enumAppProcess.StoreIn;
                ViewModel.WizardData.Wiz_StoreIn = ViewModel.WizardData.Wiz_StoreIn.Copy();
                ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
            }
            return Task.FromResult(bReturn);
        }

        public Task<bool> OnPrevious(BaseViewModel viewModel)
        {
            //throw new NotImplementedException();
            string str = string.Empty;

            return Task.FromResult(true);
        }


        private void Current_VisibilityChanged(SoftKeyboardEventArgs e)
        {
            if (e.IsVisible)
            {
                // do your things
                string str = string.Empty;
            }
            else
            {
                // do your things
                string str = string.Empty;
            }
        }

        private void stepperArticleId_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            string str = string.Empty;
        }

        private async void btnGetArticle_Clicked(object sender, EventArgs e)
        {
            await ViewModel.GetArticle();
        }

        ///-----------------------------------------------------------------------------------------------------------------------------
        ///


    }
}