using LvsScan.Portable.Interfaces;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels;
using LvsScan.Portable.ViewModels.StoreOut;
using LvsScan.Portable.Views.StoreOut.Open;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.KeyboardHelper;

namespace LvsScan.Portable.Views.StoreOut
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class a1w8_InputCheck : ContentView, IWizardView
    {
        public a1w8_InputCheckViewModel ViewModel { get; set; }
        public a1w8_InputCheck(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);
        }

        public a1w8_InputCheck(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);
        }

        private void InitView(BaseViewModel currentViewModel)
        {
            if (currentViewModel != null)
            {
                BindingContext = ViewModel = currentViewModel as a1w8_InputCheckViewModel;
            }
            ViewModel.IsBusy = false;

            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.Teststring += "a1w8_InputCheck " + Environment.NewLine;
                ViewModel.WizardData.LoggedUser = ((App)Application.Current).LoggedUser.Copy();
                ViewModel.SelectedAusgang = ViewModel.WizardData.Wiz_StoreOut.SelectedAusgang.Copy();
                ViewModel.AusgangOriginal = ViewModel.WizardData.Wiz_StoreOut.SelectedAusgang.Copy();
                ViewModel.StoreOutArt = ViewModel.WizardData.Wiz_StoreOut.StoreOutArt;
            }
        }

        Task IWizardView.OnAppearing()
        {
            //throw new NotImplementedException();
            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;
            InitView(BindingContext as a1w1_InputAdrViewModel);
            return Task.CompletedTask;
        }

        Task IWizardView.OnDissapearing()
        {
            //throw new NotImplementedException();
            SoftKeyboard.Current.VisibilityChanged -= Current_VisibilityChanged;
            return Task.CompletedTask;
        }

        public async Task<bool> OnNext(BaseViewModel viewModel)
        {
            //throw new NotImplementedException();
            bool bReturn = ViewModel.IsChecked;


            if (!ViewModel.IsChecked)
            {
                string mesInfo = "ACHTUNG";
                string message = "Der Ausgang ist nicht gecheckt. Soll der Ausgang noch als gecheckt markiert werden?";
                var resonseDisplayAlert = await App.Current.MainPage.DisplayAlert(mesInfo, message, "Check Ausgang", "Abbrechen");
                if (resonseDisplayAlert)
                {
                    ViewModel.IsChecked = true;
                }
                bReturn = ViewModel.IsChecked;
            }


            if (!ViewModel.SelectedAusgang.Equals(ViewModel.AusgangOriginal))
            {
                ViewModel.IsBusy = true;
                Task.Run(() => Task.Delay(1000)).Wait();
                var result = Task.Run(() => ViewModel.UpdateAusgang()).Result;
                bReturn = result.Success;
                ViewModel.IsBusy = false;
                if (!result.Success)
                {
                    string mesInfo = "FEHLER";
                    string message = result.Error;
                    await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
                }

                //bReturn = result.Success;
            }
            ViewModel.WizardData.Wiz_StoreOut.SelectedAusgang = ViewModel.SelectedAusgang.Copy();
            ViewModel.WizardData.Wiz_StoreOut = ViewModel.WizardData.Wiz_StoreOut.Copy();
            ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
            if (bReturn)
            {
                await Navigation.PushAsync(new oa1_OpenStoreOutListPage());
            }
            return bReturn; // Task.FromResult(bReturn);
        }

        public Task<bool> OnPrevious(BaseViewModel viewModel)
        {
            //throw new NotImplementedException();
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

        private void btnChecked_Clicked(object sender, EventArgs e)
        {
            string str = string.Empty;
            ViewModel.IsChecked = !ViewModel.IsChecked;
        }
        ///-----------------------------------------------------------------------------------------------------------------------------
        ///


    }
}