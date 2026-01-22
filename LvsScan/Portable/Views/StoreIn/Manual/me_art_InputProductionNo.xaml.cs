using Common.Helper;
using Common.Models;
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
    public partial class me_art_InputProductionNo : ContentView, IWizardView
    {
        public me_art_InputProductionNoViewModel ViewModel { get; set; }
        public me_art_InputProductionNo(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);
        }
        public me_art_InputProductionNo(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);

        }
        private async void InitView(BaseViewModel currentViewModel)
        {
            if (currentViewModel != null)
            {
                BindingContext = ViewModel = currentViewModel as me_art_InputProductionNoViewModel;
            }
            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.Teststring += "me_art_InputProductionNo " + Environment.NewLine;
                ViewModel.WizardData.LoggedUser = ((App)Application.Current).LoggedUser.Copy();

                if (ViewModel.WizardData.Wiz_StoreIn is wizStoreIn)
                {
                    if (ViewModel.WizardData.Wiz_StoreIn.SelectedEingang is Eingaenge)
                    {
                        ViewModel.SelectedEingang = ViewModel.WizardData.Wiz_StoreIn.SelectedEingang.Copy();
                        ViewModel.EingangOriginal = ViewModel.WizardData.Wiz_StoreIn.SelectedEingang.Copy();
                    }
                    ViewModel.StoreInArt = ViewModel.WizardData.Wiz_StoreIn.StoreInArt;

                }
                //ViewModel.WizardData.Wiz_StoreIn = ViewModel.WizardData.Wiz_StoreIn.Copy();
                ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
            }
            tabView.SelectedItem = tabView.Items[0];
            await Task.Delay(10);
            await Task.Run(() =>
            {
                // set value empty and set focus
                eProduktionsnummer.Focus();
                eProduktionsnummerManual.Unfocus();
                eWerksnummer.Unfocus();
                eWerksnummerManual.Unfocus();
            });

            ViewModel.Init();
        }

        public Task OnAppearing()
        {
            return Task.CompletedTask;
        }

        public Task OnDissapearing()
        {
            return Task.CompletedTask;
        }

        public async Task<bool> OnNext(BaseViewModel viewModel)
        {
            bool bReturn = false;
            await Task.Delay(10);
            await Task.Run(() => ViewModel.InitArticleToCreate());

            bReturn = false;

            if (
                    (ViewModel.ArticleToCreate is Articles) &&
                    (ViewModel.ArticleToCreate.Produktionsnummer != null) &&
                    (ViewModel.ArticleToCreate.Werksnummer != null) &&
                    (ViewModel.ArticleToCreate.Produktionsnummer.Length > 0) &&
                    (ViewModel.ArticleToCreate.Werksnummer.Length > 0)
               )
            {
                Task.Run(() => Task.Delay(100)).Wait();
                var result = Task.Run(() => ViewModel.CreateNewArticle()).Result;
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
                bReturn = false;
                string mesInfo = "FEHLER";
                string message = "Es ist ein Fehler aufgetreten! Das Feld Produktionsnummer und Werksnummer muss ausgefüllt sein.";
                await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
            }

            ViewModel.WizardData.Wiz_StoreIn.SelectedEingang = ViewModel.SelectedEingang.Copy();
            ViewModel.WizardData.Wiz_StoreIn.ArticleToCheck = ViewModel.ArticleCreated.Copy();
            ViewModel.WizardData.Wiz_StoreIn.StoreInArt = ViewModel.StoreInArt;
            ViewModel.WizardData.Wiz_StoreIn = ViewModel.WizardData.Wiz_StoreIn.Copy();
            ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();

            //ViewModel.ArticleToCreate = null;
            //ViewModel.ArticleCreated = null;

            return bReturn; // Task.FromResult(bReturn);
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

        private void tabView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }

        private void btnClearProductionNo_Clicked(object sender, EventArgs e)
        {
            ViewModel.Produktionsnummer = string.Empty;
        }

        private void btnClearWerksnummer_Clicked(object sender, EventArgs e)
        {
            ViewModel.Werksnummer = string.Empty;
        }


        private void btnClearProductionNoManual_Clicked(object sender, EventArgs e)
        {
            ViewModel.Produktionsnummer = string.Empty;
        }

        private void btnClearWerksnummerManual_Clicked(object sender, EventArgs e)
        {
            ViewModel.Werksnummer = string.Empty;
        }

        private void btnWerksnummerDeleteFirstChar_Clicked(object sender, EventArgs e)
        {
            ViewModel.Werksnummer = StringValueEdit.RemoveFirtsCharFromValue(ViewModel.Werksnummer);
        }

        private void btnProductionNoDeleteFirstChar_Clicked(object sender, EventArgs e)
        {
            ViewModel.Produktionsnummer = StringValueEdit.RemoveFirtsCharFromValue(ViewModel.Produktionsnummer);
        }

        private async void btnCheckGoodstype_Clicked(object sender, EventArgs e)
        {
            if (ViewModel.Werksnummer.Length > 0)
            {
                await ViewModel.GetGoodstypebyWerksnummer();
            }
            else
            {
                string mesInfo = "FEHLER";
                string message = "Für die Suche nach einer hinterlegten Güterart muss das Feld Werksnummer gefüllt sein!";
                await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
            }
        }
    }
}