using Common.Enumerations;
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
    public partial class a1w3_InputTermin : ContentView, IWizardView
    {
        public a1w3_InputTerminViewModel ViewModel { get; set; }
        public a1w3_InputTermin(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);
        }
        public a1w3_InputTermin(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);

        }
        private void InitView(BaseViewModel currentViewModel)
        {
            if (currentViewModel != null)
            {
                BindingContext = ViewModel = currentViewModel as a1w3_InputTerminViewModel;
            }
            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.Teststring += "a1w1_wizView " + Environment.NewLine;
                ViewModel.WizardData.LoggedUser = ((App)Application.Current).LoggedUser.Copy();
                ViewModel.SelectedAusgang = ViewModel.WizardData.Wiz_StoreOut.SelectedAusgang.Copy();
                ViewModel.AusgangOriginal = ViewModel.SelectedAusgang;
                ViewModel.StoreOutArt = ViewModel.WizardData.Wiz_StoreOut.StoreOutArt;

                if (ViewModel.StoreOutArt.Equals(enumStoreOutArt.NotSet))
                {
                    string mesInfo = "FEHLER";
                    string message = "Es ist keine Auslagerungsart gesetzt. Starten Sie den Prozess erneut vom Ausgang Submenü.";
                    App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
                }
            }
            ViewModel.IsBaseNextEnabeld = false;
        }

        Task IWizardView.OnAppearing()
        {
            //throw new NotImplementedException();
            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;
            InitView(BindingContext as a1w1_InputAdrViewModel);
            if (ViewModel.StoreOutArt.Equals(enumStoreOutArt.NotSet))
            {
                string mesInfo = "FEHLER";
                string message = "Es ist keine Auslagerungsart gesetzt. Starten Sie den Prozess erneut vom Ausgang Submenü.";
                App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
            }

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
            //compare SelectedAusgang and AusgangOriginal for changes to save
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
        ///
        //private void btnClient_Clicked(object sender, EventArgs e)
        //{
        //    string str = string.Empty;
        //    ViewModel.IsInputClientEnabled = !ViewModel.IsInputClientEnabled;
        //    //ViewModel.GetClientAddresses();
        //    ViewModel.IsBusy = true;
        //    Task.Run(() => Task.Delay(1000)).Wait();
        //    Task.Run(() => ViewModel.GetClientAddresses()).Wait();
        //    ViewModel.IsBusy = false;
        //}

        //private void btnReceipin_Clicked(object sender, EventArgs e)
        //{
        //    string str = string.Empty;
        //    ViewModel.IsInputReciepinEnabled = !ViewModel.IsInputReciepinEnabled;
        //    ViewModel.IsBusy = true;
        //    Task.Run(() => Task.Delay(1000)).Wait();
        //    Task.Run(() => ViewModel.GetReceipinAddresses()).Wait();
        //    ViewModel.IsBusy = false;
        //}
        private void btnTermin_Clicked(object sender, EventArgs e)
        {
            ViewModel.IsInputTerminEnabled = true;
            //ViewModel.IsInputTerminEnabled = !ViewModel.IsInputTerminEnabled;
            if (ViewModel.IsInputTerminEnabled)
            {
                termin_DateTimePicker.Focus();
            }
            else
            {
                termin_DateTimePicker.Unfocus();
            }
        }

        /// <summary>
        ///             Test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    ViewModel.IsBusy = !ViewModel.IsBusy;
        //}
        /// <summary>
        ///             Test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void Button_Clicked_1(object sender, EventArgs e)
        //{
        //    ViewModel.IsBaseNextEnabeld = !ViewModel.IsBaseNextEnabeld;
        //}

        //private void termin_DateTimePicker_Unfocused(object sender, FocusEventArgs e)
        //{
        //    string str=string.Empty;
        //}

        //private void termin_DateTimePicker_SelectionChanged(object sender, EventArgs e)
        //{
        //    string str = string.Empty;
        //}
    }
}