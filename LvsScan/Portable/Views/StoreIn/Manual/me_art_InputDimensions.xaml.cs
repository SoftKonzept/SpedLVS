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
    public partial class me_art_InputDimensions : ContentView, IWizardView
    {

        //public e1w1_wizViewModel ViewModel { get; set; }
        public me_art_InputDimensionsNoViewModel ViewModel { get; set; }
        public me_art_InputDimensions(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);
        }
        public me_art_InputDimensions(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);

        }
        private void InitView(BaseViewModel currentViewModel)
        {
            if (currentViewModel != null)
            {
                BindingContext = ViewModel = currentViewModel as me_art_InputDimensionsNoViewModel;
            }
            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.Teststring += "me_art_InputDimensions " + Environment.NewLine;
                ViewModel.WizardData.LoggedUser = ((App)Application.Current).LoggedUser.Copy();

                ViewModel.StoreInArt = ViewModel.WizardData.Wiz_StoreIn.StoreInArt;
                ViewModel.SelectedEingang = ViewModel.WizardData.Wiz_StoreIn.SelectedEingang.Copy();
                ViewModel.SelectedArticle = ViewModel.WizardData.Wiz_StoreIn.ArticleToCheck.Copy();
                ViewModel.ArticleOriginal = ViewModel.WizardData.Wiz_StoreIn.ArticleToCheck.Copy();
                ViewModel.WizardData.Wiz_StoreIn = ViewModel.WizardData.Wiz_StoreIn.Copy();
                ViewModel.Init();

            }
            tabView.SelectedItem = tabView.Items[0];
        }

        public Task OnAppearing()
        {
            return Task.CompletedTask;
        }

        public Task OnDissapearing()
        {
            return Task.CompletedTask;
        }

        //public Task<bool> OnNext(BaseViewModel viewModel)
        //{
        //    bool bReturn = false;
        //    if (!ViewModel.SelectedArticle.Equals(ViewModel.ArticleOriginal))
        //    {
        //        ViewModel.IsBusy = true;
        //        Task.Run(() => Task.Delay(100)).Wait();
        //        var result = Task.Run(() => ViewModel.UpdateArticle()).Result;
        //        bReturn = result.Success;
        //        ViewModel.IsBusy = false;
        //        if (!result.Success)
        //        {
        //            string mesInfo = "FEHLER";
        //            string message = result.Error;
        //            App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
        //        }
        //    }
        //    else
        //    {
        //        bReturn = true;
        //    }

        //    ViewModel.WizardData.Wiz_StoreIn.SelectedEingang = ViewModel.SelectedEingang.Copy();
        //    ViewModel.WizardData.Wiz_StoreIn.ArticleToCheck = ViewModel.SelectedArticle.Copy();
        //    ViewModel.WizardData.Wiz_StoreIn = ViewModel.WizardData.Wiz_StoreIn.Copy();
        //    ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
        //    return Task.FromResult(bReturn);
        //}

        /// <summary>
        ///             Next Step
        ///             Aktionen, die durchgeführt werden sollen wenn die 
        ///             es weiter zur nächsten Seite gehen soll wie:
        ///             - Update Datenbanken usw.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task<bool> OnNext(BaseViewModel viewModel)
        {
            bool bReturn = false;
            if (!ViewModel.SelectedArticle.Equals(ViewModel.ArticleOriginal))
            {
                ViewModel.IsBusy = true;
                //await Task.Delay(100); // Simuliert eine kurze Wartezeit
                var result = await ViewModel.UpdateArticle();
                bReturn = result.Success;
                ViewModel.IsBusy = false;
                if (!result.Success)
                {
                    string mesInfo = "FEHLER";
                    string message = result.Error;
                    await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
                }
            }
            else
            {
                bReturn = true;
            }

            ViewModel.WizardData.Wiz_StoreIn.SelectedEingang = ViewModel.SelectedEingang.Copy();
            ViewModel.WizardData.Wiz_StoreIn.ArticleToCheck = ViewModel.SelectedArticle.Copy();
            ViewModel.WizardData.Wiz_StoreIn = ViewModel.WizardData.Wiz_StoreIn.Copy();
            ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
            return bReturn;
        }
        /// <summary>
        ///             Navigation zurück
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
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


        private void btnClearDicke_Clicked(object sender, EventArgs e)
        {
            ViewModel.Dicke = 0M;
        }

        private void btnDeleteFirstCharDicke_Clicked(object sender, EventArgs e)
        {
            ViewModel.DickeString = StringValueEdit.RemoveFirtsCharFromValue(ViewModel.DickeString);
        }

        private void btnClearBreite_Clicked(object sender, EventArgs e)
        {
            ViewModel.Breite = 0M;
        }

        private void btnDeleteFirstCharBreite_Clicked(object sender, EventArgs e)
        {
            ViewModel.BreiteString = StringValueEdit.RemoveFirtsCharFromValue(ViewModel.BreiteString);
        }

        private void btnClearLaenge_Clicked(object sender, EventArgs e)
        {
            ViewModel.Laenge = 0M;
        }

        private void btnDeleteFirstCharLaenge_Clicked(object sender, EventArgs e)
        {
            ViewModel.LaengeString = StringValueEdit.RemoveFirtsCharFromValue(ViewModel.LaengeString);
        }

        private void btnClearHoehe_Clicked(object sender, EventArgs e)
        {
            ViewModel.Hoehe = 0M;
        }

        private void btnDeleteFirstCharHoehe_Clicked(object sender, EventArgs e)
        {
            ViewModel.HoeheString = StringValueEdit.RemoveFirtsCharFromValue(ViewModel.HoeheString);
        }
    }
}