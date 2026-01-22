using Common.Enumerations;
using LvsScan.Portable.Interfaces;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels;
using LvsScan.Portable.ViewModels.StoreIn;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.KeyboardHelper;

namespace LvsScan.Portable.Views.StoreIn
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class e1w1_einInputAdr : ContentView, IWizardView
    {

        //public e1w1_wizViewModel ViewModel { get; set; }
        public e1w1_einInputAdrViewModel ViewModel { get; set; }
        public e1w1_einInputAdr(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);
        }
        public e1w1_einInputAdr(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);

        }
        private void InitView(BaseViewModel currentViewModel)
        {
            if (currentViewModel != null)
            {
                BindingContext = ViewModel = currentViewModel as e1w1_einInputAdrViewModel;
            }
            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.Teststring += "e1w1_einInputAdrViewModel " + Environment.NewLine;
                ViewModel.WizardData.LoggedUser = ((App)Application.Current).LoggedUser.Copy();
                ViewModel.StoreInArt = ViewModel.WizardData.Wiz_StoreIn.StoreInArt;
                ViewModel.Init();

                ViewModel.SelectedEingang = ViewModel.WizardData.Wiz_StoreIn.SelectedEingang.Copy();
                ViewModel.EingangOriginal = ViewModel.SelectedEingang;

                if (ViewModel.StoreInArt.Equals(enumStoreOutArt.NotSet))
                {
                    string mesInfo = "FEHLER";
                    string message = "Es ist keine Einlagerungsart gesetzt. Starten Sie den Prozess erneut vom Eingang Submenü.";
                    App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
                }
            }
            ViewModel.IsBusy = false;
            ViewModel.IsBaseNextEnabeld = false;
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
            if (!ViewModel.SelectedEingang.Equals(ViewModel.EingangOriginal))
            {
                ViewModel.IsBusy = true;
                Task.Run(() => Task.Delay(1000)).Wait();
                var result = Task.Run(() => ViewModel.UpdateEingang()).Result;
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
            ViewModel.WizardData.Wiz_StoreIn.SelectedEingang = ViewModel.SelectedEingang.Copy();
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
            Task.Run(() => Task.Delay(1000)).Wait();
            Task.Run(() => ViewModel.GetReceipinAddresses()).Wait();
            ViewModel.IsBusy = false;
        }

    }
}