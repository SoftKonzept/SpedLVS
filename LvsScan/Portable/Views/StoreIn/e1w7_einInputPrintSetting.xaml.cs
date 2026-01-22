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
    public partial class e1w7_einInputPrintSetting : ContentView, IWizardView
    {
        public e1w7_einInputPrintSettingViewModel ViewModel { get; set; }
        public e1w7_einInputPrintSetting(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);
        }
        public e1w7_einInputPrintSetting(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);

        }
        private void InitView(BaseViewModel currentViewModel)
        {
            if (currentViewModel != null)
            {
                BindingContext = ViewModel = currentViewModel as e1w7_einInputPrintSettingViewModel;
            }
            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.Teststring += "e1w7_einInputPrintSetting " + Environment.NewLine;
                ViewModel.WizardData.LoggedUser = ((App)Application.Current).LoggedUser.Copy();
                ViewModel.StoreInArt = ViewModel.WizardData.Wiz_StoreIn.StoreInArt;

                ViewModel.SelectedEingang = ViewModel.WizardData.Wiz_StoreIn.SelectedEingang.Copy();
                ViewModel.EingangOriginal = ViewModel.WizardData.Wiz_StoreIn.SelectedEingang.Copy();
                ViewModel.Init();
                ViewModel.GetAvailablePrinter();

                //if (ViewModel.StoreInArt.Equals(enumStoreOutArt.NotSet))
                //{
                //    string mesInfo = "FEHLER";
                //    string message = "Es ist keine Einlagerungsart gesetzt. Starten Sie den Prozess erneut vom Eingang Submenü.";
                //    App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
                //}
            }
            ViewModel.IsBaseNextEnabeld = false;
        }

        public Task OnAppearing()
        {
            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;
            InitView(BindingContext as e1w7_einInputPrintSettingViewModel);
            return Task.CompletedTask;
        }

        public Task OnDissapearing()
        {
            SoftKeyboard.Current.VisibilityChanged -= Current_VisibilityChanged;
            return Task.CompletedTask;
        }

        public Task<bool> OnNext(BaseViewModel viewModel)
        {
            bool bReturn = true;
            //if (!ViewModel.SelectedEingang.Equals(ViewModel.EingangOriginal))
            //{
            //    ViewModel.IsBusy = true;
            //    Task.Run(() => Task.Delay(1000)).Wait();
            //    var result = Task.Run(() => ViewModel.UpdateEingang()).Result;
            //    bReturn = result.Success;
            //    ViewModel.IsBusy = false;
            //    if (!result.Success)
            //    {
            //        string mesInfo = "FEHLER";
            //        string message = result.Error;
            //        App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
            //    }
            //}
            //else
            //{
            //    bReturn = true;
            //}
            ViewModel.WizardData.Wiz_StoreIn.SelectedEingang = ViewModel.SelectedEingang.Copy();
            ViewModel.WizardData.Wiz_StoreIn.PrintCount = ViewModel.PrintCount;
            ViewModel.WizardData.Wiz_StoreIn.PrinterName = ViewModel.SelectedPrinter;

            //ViewModel.WizardData.Wiz_StoreIn = ViewModel.WizardData.Wiz_StoreIn.Copy();
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
    }
}