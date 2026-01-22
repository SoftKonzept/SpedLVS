using Common.Helper;
using LVS.Constants;
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
    public partial class me_art_InputWeight : ContentView, IWizardView
    {

        //public e1w1_wizViewModel ViewModel { get; set; }
        public me_art_InputWeightViewModel ViewModel { get; set; }
        public me_art_InputWeight(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);
        }
        public me_art_InputWeight(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);

        }
        private void InitView(BaseViewModel currentViewModel)
        {
            if (currentViewModel != null)
            {
                BindingContext = ViewModel = currentViewModel as me_art_InputWeightViewModel;
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
            eAnzahl.Focus();

            //MessagingCenter.Subscribe<me_art_InputWeight, string>(this, "WeightError", (sender, message) =>
            //{
            //    //DisplayAlert("Message Received", message, "OK");
            //    //await MessageService.ShowAsync("ACHTUNG", message);

            //    App.Current.MainPage.DisplayAlert("ACHTUNG", message, "OK");
            //});
        }

        public Task OnAppearing()
        {
            return Task.CompletedTask;
        }

        public Task OnDissapearing()
        {
            //MessagingCenter.Unsubscribe<me_art_InputWeight, string>(this, "WeightError");
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

        private void btnClearBrutto_Clicked(object sender, EventArgs e)
        {
            ViewModel.BruttoString = "0";
        }

        private void btnDeleteFirstCharBrutto_Clicked(object sender, EventArgs e)
        {
            ViewModel.BruttoString = StringValueEdit.RemoveFirtsCharFromValue(ViewModel.BruttoString);
        }

        private void btnClearNetto_Clicked(object sender, EventArgs e)
        {
            ViewModel.NettoString = "0";
        }

        private void btnDeleteFirstCharNetto_Clicked(object sender, EventArgs e)
        {
            ViewModel.NettoString = StringValueEdit.RemoveFirtsCharFromValue(ViewModel.NettoString);
        }

        private void btnClearAnzahl_Clicked(object sender, EventArgs e)
        {
            ViewModel.Anzahl = 1;
        }

        private void btnDeleteFirstCharAnzahl_Clicked(object sender, EventArgs e)
        {
            ViewModel.AnzahlString = StringValueEdit.RemoveFirtsCharFromValue(ViewModel.AnzahlString);
        }



        private async void eBruttoManual_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckInput(e.NewTextValue.ToString());
        }

        private async void CheckInput(string myInput)
        {
            string str = string.Empty;
            int iTmp = 0;
            int.TryParse(myInput, out iTmp);
            if (constValue_ScanApp_IniDefault.const_ScanApp_Default_MaxArticleWeight < iTmp)
            {
                string message = string.Empty;
                message = "Der eingegebene Wert liegt über dem Konrollwert (" + constValue_ScanApp_IniDefault.const_ScanApp_Default_MaxArticleWeight + ")!" + Environment.NewLine;
                message += "Bitte Eingabe wiederholen und prüfen!";
                await App.Current.MainPage.DisplayAlert("ACHTUNG", message, "OK");
            }
        }

        private void eNettoManual_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckInput(e.NewTextValue.ToString());
        }

        private void eNetto_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckInput(e.NewTextValue.ToString());
        }

        private void eBrutto_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckInput(e.NewTextValue.ToString());
        }
    }
}