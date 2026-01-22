using Common.Helper;
using LvsScan.Portable.Interfaces;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels;
using LvsScan.Portable.ViewModels.StoreIn.Manual;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.KeyboardHelper;

namespace LvsScan.Portable.Views.StoreIn.Manual
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class me_art_InputReferences : ContentView, IWizardView
    {

        //public e1w1_wizViewModel ViewModel { get; set; }
        public me_art_InputReferencesViewModel ViewModel { get; set; }
        public me_art_InputReferences(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);
        }
        public me_art_InputReferences(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);

        }
        private void InitView(BaseViewModel currentViewModel)
        {
            if (currentViewModel != null)
            {
                BindingContext = ViewModel = currentViewModel as me_art_InputReferencesViewModel;
            }
            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.Teststring += "me_art_InputProductionNo " + Environment.NewLine;
                ViewModel.WizardData.LoggedUser = ((App)Application.Current).LoggedUser.Copy();

                //ViewModel.WizardData.Wiz_StoreIn = ViewModel.WizardData.Wiz_StoreIn.Copy();
                ViewModel.StoreInArt = ViewModel.WizardData.Wiz_StoreIn.StoreInArt;

                ViewModel.SelectedArticle = ViewModel.WizardData.Wiz_StoreIn.ArticleToCheck.Copy();
                ViewModel.ArticleOriginal = ViewModel.WizardData.Wiz_StoreIn.ArticleToCheck.Copy();
                ViewModel.SelectedEingang = ViewModel.WizardData.Wiz_StoreIn.SelectedEingang.Copy();
                ViewModel.WizardData.Wiz_StoreIn = ViewModel.WizardData.Wiz_StoreIn.Copy();
                ViewModel.Init();
            }
            tabView.SelectedItem = tabView.Items[0];
            //e.Focus();
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
            if (!ViewModel.SelectedArticle.Equals(ViewModel.ArticleOriginal))
            {
                ViewModel.IsBusy = true;
                Task.Run(() => Task.Delay(100)).Wait();
                var result = Task.Run(() => ViewModel.UpdateArticle()).Result;
                bReturn = result.Success;
                ViewModel.IsBusy = false;
                if (!result.Success)
                {
                    string mesInfo = "FEHLER";
                    string message = result.Error;
                    App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
                }
            }
            else
            {
                bReturn = true;
            }

            bReturn = true;

            ViewModel.WizardData.Wiz_StoreIn.SelectedEingang = ViewModel.SelectedEingang.Copy();
            ViewModel.WizardData.Wiz_StoreIn.ArticleToCheck = ViewModel.SelectedArticle.Copy();
            ViewModel.WizardData.Wiz_StoreIn = ViewModel.WizardData.Wiz_StoreIn.Copy();
            ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
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


        ///-----------------------------------------------------------------------------------------------------------------------------
        ///
        private void btnClearExMaterialnummer_Clicked(object sender, EventArgs e)
        {
            ViewModel.ExMaterialnummer = string.Empty;
        }

        private void btnDeleteFirstCharExMaterialnummer_Clicked(object sender, EventArgs e)
        {
            ViewModel.ExMaterialnummer = StringValueEdit.RemoveFirtsCharFromValue(ViewModel.ExMaterialnummer);
        }

        private void btnClearExBezeichnung_Clicked(object sender, EventArgs e)
        {
            ViewModel.ExBezeichnung = string.Empty;
        }

        private void btnDeleteFirstCharExBezeichnung_Clicked(object sender, EventArgs e)
        {
            ViewModel.ExBezeichnung = StringValueEdit.RemoveFirtsCharFromValue(ViewModel.ExBezeichnung);
        }
    }
}