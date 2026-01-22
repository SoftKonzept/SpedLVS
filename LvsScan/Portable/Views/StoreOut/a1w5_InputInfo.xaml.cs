using Common.Models;
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
    public partial class a1w5_InputInfo : ContentView, IWizardView
    {
        public a1w5_InputInfoViewModel ViewModel { get; set; }
        public a1w5_InputInfo(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);
        }

        public a1w5_InputInfo(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);
        }

        private void InitView(BaseViewModel currentViewModel)
        {
            if (currentViewModel != null)
            {
                BindingContext = ViewModel = currentViewModel as a1w5_InputInfoViewModel;
            }
            ViewModel.IsBusy = false;

            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.Teststring += "a1w3_wizViewModel " + Environment.NewLine;
                ViewModel.WizardData.LoggedUser = ((App)Application.Current).LoggedUser.Copy();
                if (ViewModel.WizardData.Wiz_StoreOut is null)
                {
                    ViewModel.WizardData.Wiz_StoreOut = new wizStoreOut();
                    if (ViewModel.WizardData.Wiz_StoreOut.SelectedAusgang is null)
                    {
                        ViewModel.WizardData.Wiz_StoreOut.SelectedAusgang = new Ausgaenge();
                    }
                }
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

        public Task<bool> OnNext(BaseViewModel viewModel)
        {
            bool bReturn = true;
            //throw new NotImplementedException();
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
                    App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
                }
            }
            ViewModel.WizardData.Wiz_StoreOut.SelectedAusgang = ViewModel.SelectedAusgang.Copy();
            ViewModel.WizardData.Wiz_StoreOut = ViewModel.WizardData.Wiz_StoreOut.Copy();
            ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
            return Task.FromResult(bReturn);
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

        private void btnInfo_Clicked(object sender, EventArgs e)
        {
            ViewModel.IsInputInfoEnabled = !ViewModel.IsInputInfoEnabled;
            editorInfo.Focus();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            ViewModel.IsInputInfoEnabled = !ViewModel.IsInputInfoEnabled;
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            ViewModel.IsBtnInfoEnabled = !ViewModel.IsBtnInfoEnabled;
        }

        ///-----------------------------------------------------------------------------------------------------------------------------
    }
}