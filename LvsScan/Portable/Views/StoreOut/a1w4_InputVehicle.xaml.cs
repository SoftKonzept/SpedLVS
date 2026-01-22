using LvsScan.Portable.Interfaces;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels;
using LvsScan.Portable.ViewModels.StoreOut;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.KeyboardHelper;

namespace LvsScan.Portable.Views.StoreOut
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class a1w4_InputVehicle : ContentView, IWizardView
    {
        public a1w4_InputVehicleViewModel ViewModel { get; set; }
        public a1w4_InputVehicle(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);
        }

        public a1w4_InputVehicle(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);
        }

        private void InitView(BaseViewModel currentViewModel)
        {
            if (currentViewModel != null)
            {
                BindingContext = ViewModel = currentViewModel as a1w4_InputVehicleViewModel;
            }
            ViewModel.IsBusy = false;

            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.Teststring += "a1w3_InputVehicleViewModel " + Environment.NewLine;
                ViewModel.WizardData.LoggedUser = ((App)Application.Current).LoggedUser.Copy();
                ViewModel.SelectedAusgang = ViewModel.WizardData.Wiz_StoreOut.SelectedAusgang.Copy();
                ViewModel.AusgangOriginal = ViewModel.WizardData.Wiz_StoreOut.SelectedAusgang.Copy();
                ViewModel.StoreOutArt = ViewModel.WizardData.Wiz_StoreOut.StoreOutArt;

                Task.Run(() => ViewModel.GetVehicleList());
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

        public Task<bool> OnNext(BaseViewModel viewModel)
        {
            //throw new NotImplementedException();
            bool bReturn = false;
            if (!ViewModel.SelectedAusgang.Equals(ViewModel.AusgangOriginal))
            {
                ViewModel.IsBusy = true;
                //Task.Run(() => Task.Delay(1000)).Wait();

                var result = Task.Run(() => ViewModel.UpdateAusgang()).Result;
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
            ViewModel.WizardData.Wiz_StoreOut.SelectedAusgang = ViewModel.SelectedAusgang.Copy();
            ViewModel.WizardData.Wiz_StoreOut = ViewModel.WizardData.Wiz_StoreOut.Copy();
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

        private void btnDriver_Clicked(object sender, EventArgs e)
        {
            ViewModel.IsInputDriverEnabled = !ViewModel.IsInputDriverEnabled;
            inputDriver.Focus();
        }
        private void btnTrailer_Clicked(object sender, EventArgs e)
        {
            ViewModel.IsInputTrailerEnabled = !ViewModel.IsInputTrailerEnabled;
            inputTrailer.Focus();
        }

        private void btnKFZ_Clicked(object sender, EventArgs e)
        {
            ViewModel.IsInputKFZEnabled = !ViewModel.IsInputKFZEnabled;
            inputKFZ.Focus();
        }

        private void inputKFZ_Completed(object sender, EventArgs e)
        {
            ViewModel.IsInputKFZEnabled = !ViewModel.IsInputKFZEnabled;
        }

        private void inputTrailer_Completed(object sender, EventArgs e)
        {
            ViewModel.IsInputTrailerEnabled = !ViewModel.IsInputTrailerEnabled;
        }

        private void inputDriver_Completed(object sender, EventArgs e)
        {
            ViewModel.IsInputDriverEnabled = !ViewModel.IsInputDriverEnabled;
        }



    }
}