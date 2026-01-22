using Common.Models;
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
    public partial class a1w7_InputPrintSetting : ContentView, IWizardView
    {
        public a1w7_InputPrintSettingsViewModel ViewModel { get; set; }
        public a1w7_InputPrintSetting(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);
        }

        public a1w7_InputPrintSetting(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);
        }

        private void InitView(BaseViewModel currentViewModel)
        {
            if (currentViewModel != null)
            {
                BindingContext = ViewModel = currentViewModel as a1w7_InputPrintSettingsViewModel;
            }
            ViewModel.IsBusy = false;

            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.Teststring += "a1w7_InputPrintSetting " + Environment.NewLine;
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

                ViewModel.GetAvailablePrinter();
            }
        }

        Task IWizardView.OnAppearing()
        {
            //throw new NotImplementedException();
            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;
            InitView(BindingContext as a1w7_InputPrintSettingsViewModel);
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
            bool bReturn = true;
            //throw new NotImplementedException();
            if (!ViewModel.SelectedAusgang.Equals(ViewModel.AusgangOriginal))
            {
                ViewModel.IsBusy = true;
                Task.Run(() => Task.Delay(500)).Wait();
                var result = Task.Run(() => ViewModel.AddPrintAction()).Result;
                //bReturn = result.Success;
                ViewModel.IsBusy = false;

                string mesInfo = string.Empty;
                string message = string.Empty;
                message = result.Info;
                //if (!result.Success)
                //{
                //    mesInfo = "FEHLER";
                //    message = result.Error;
                //    //App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
                //}
                //else 
                //{
                //    mesInfo = "Information";
                //    message = "Der Druckauftrag wurde erstellt!";
                //    //App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
                //}
                await Application.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
            }
            ViewModel.WizardData.Wiz_StoreOut.SelectedAusgang = ViewModel.SelectedAusgang.Copy();
            ViewModel.WizardData.Wiz_StoreOut.PrintCount = ViewModel.PrintCount;
            viewModel.WizardData.Wiz_StoreOut.PrinterName = ViewModel.SelectedPrinter;

            ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();

            await Navigation.PushAsync(new oa2_ArticleListPage());

            return true; // Task.FromResult(bReturn);
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

        //private void btnInfo_Clicked(object sender, EventArgs e)
        //{
        //   ViewModel.IsInputInfoEnabled= !ViewModel.IsInputInfoEnabled;
        //   editorInfo.Focus();
        //}

        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    ViewModel.IsInputInfoEnabled= !ViewModel.IsInputInfoEnabled;
        //}

        //private void Button_Clicked_1(object sender, EventArgs e)
        //{
        //    ViewModel.IsBtnInfoEnabled = !ViewModel.IsBtnInfoEnabled;
        //}

    }
}