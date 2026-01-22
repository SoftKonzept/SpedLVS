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
    public partial class a1w6_InputPrint : ContentView, IWizardView
    {
        public a1w6_InputPrintViewModel ViewModel { get; set; }
        public a1w6_InputPrint(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);
        }

        public a1w6_InputPrint(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);
        }

        private void InitView(BaseViewModel currentViewModel)
        {
            if (currentViewModel != null)
            {
                BindingContext = ViewModel = currentViewModel as a1w6_InputPrintViewModel;
            }
            ViewModel.IsBusy = false;

            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.Teststring += "a1w6_InputPrintViewModel " + Environment.NewLine;
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
            //ViewModel.WizardData.Wiz_StoreOut.PrintCount = ViewModel.PrintCount;
            //ViewModel.WizardData.Wiz_StoreOut = ViewModel.WizardData.Wiz_StoreOut.Copy();
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

    }
}