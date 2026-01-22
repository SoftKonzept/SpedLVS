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
    public partial class me_InputClient : ContentView, IWizardView
    {
        public me_InputClientViewModel ViewModel { get; set; }
        public me_InputClient(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);
        }
        public me_InputClient(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);

        }
        private void InitView(BaseViewModel currentViewModel)
        {
            if (currentViewModel != null)
            {
                BindingContext = ViewModel = currentViewModel as me_InputClientViewModel;
            }
            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.Teststring += "me_art_InputProductionNo " + Environment.NewLine;
                ViewModel.WizardData.LoggedUser = ((App)Application.Current).LoggedUser.Copy();
                ViewModel.WizardData.AppProcess = Common.Enumerations.enumAppProcess.StoreIn;

                //ViewModel.StoreInArt = ViewModel.WizardData.Wiz_StoreIn.StoreInArt;
                ViewModel.SelectedEingang = ViewModel.WizardData.Wiz_StoreIn.SelectedEingang.Copy();
                ViewModel.EingangOriginal = ViewModel.SelectedEingang;

                ViewModel.WizardData.Wiz_StoreIn.StoreInArt = ViewModel.StoreInArt;
                ViewModel.WizardData.Wiz_StoreIn = ViewModel.WizardData.Wiz_StoreIn.Copy();
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

        public async Task<bool> OnNext(BaseViewModel viewModel)
        {
            bool bReturn = false;

            if (!ViewModel.SelectedEingang.Equals(ViewModel.EingangOriginal))
            {
                //ViewModel.IsBusy = true;
                Task.Run(() => Task.Delay(100)).Wait();
                var result = Task.Run(() => ViewModel.UpdateEingang()).Result;
                bReturn = result.Success;
                //ViewModel.IsBusy = false;
                if (!result.Success)
                {
                    string mesInfo = "FEHLER";
                    string message = result.Error;
                    message += "Es ist ein Fehler aufgetreten! Es muss ein Auftraggeber und Empfänger gesetzt werden.";
                    await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
                }
            }
            else
            {
                bReturn = (ViewModel.SelectedEingang.Auftraggeber > 0) && (ViewModel.SelectedEingang.Empfaenger > 0);
                if (!bReturn)
                {
                    string mesInfo = "FEHLER";
                    string message = "Es ist ein Fehler aufgetreten! Es muss ein Auftraggeber und Empfänger gesetzt werden.";
                    await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
                }
            }

            ViewModel.WizardData.Wiz_StoreIn.SelectedEingang = ViewModel.SelectedEingang.Copy();
            //ViewModel.WizardData.Wiz_StoreIn = ViewModel.WizardData.Wiz_StoreIn.Copy();
            ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
            if (bReturn)
            {
                await Navigation.PushAsync(new me_wArticleInput_wizHost());
            }

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
        private void btnClient_Clicked(object sender, EventArgs e)
        {
            string str = string.Empty;
            ViewModel.IsInputClientEnabled = !ViewModel.IsInputClientEnabled;
            //ViewModel.GetClientAddresses();
            ViewModel.IsBusy = true;
            Task.Run(() => Task.Delay(1000)).Wait();
            Task.Run(() => ViewModel.GetClientAddresses()).Wait();
            ViewModel.IsBusy = false;
        }

        private void btnReceipin_Clicked(object sender, EventArgs e)
        {
            string str = string.Empty;
            ViewModel.IsInputReciepinEnabled = !ViewModel.IsInputReciepinEnabled;
            ViewModel.IsBusy = true;
            Task.Run(() => Task.Delay(100)).Wait();
            Task.Run(() => ViewModel.GetReceipinAddresses()).Wait();
            ViewModel.IsBusy = false;
        }
        private void tabView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }

        private void btnClearProductionNo_Clicked(object sender, EventArgs e)
        {
            //ViewModel.Produktionsnummer = string.Empty;
        }

        private void btnClearWerksnummer_Clicked(object sender, EventArgs e)
        {
            //ViewModel.Werksnummer = string.Empty;
        }


        private void btnClearProductionNoManual_Clicked(object sender, EventArgs e)
        {
            //ViewModel.Produktionsnummer = string.Empty;
        }

        private void btnClearWerksnummerManual_Clicked(object sender, EventArgs e)
        {
            //ViewModel.Werksnummer = string.Empty;
        }
    }
}