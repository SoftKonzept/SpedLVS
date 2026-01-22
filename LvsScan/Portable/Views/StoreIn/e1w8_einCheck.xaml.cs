using Common.Enumerations;
using LvsScan.Portable.Interfaces;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels;
using LvsScan.Portable.ViewModels.StoreIn;
using LvsScan.Portable.Views.StoreIn.Edi;
using LvsScan.Portable.Views.StoreIn.Open;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.KeyboardHelper;

namespace LvsScan.Portable.Views.StoreIn
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class e1w8_einCheck : ContentView, IWizardView
    {
        public e1w8_einInputCheckViewModel ViewModel { get; set; }
        public e1w8_einCheck(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);
        }
        public e1w8_einCheck(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);
        }
        private void InitView(BaseViewModel currentViewModel)
        {
            if (currentViewModel != null)
            {
                BindingContext = ViewModel = currentViewModel as e1w8_einInputCheckViewModel;
            }
            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.Teststring += "a1w1_wizView " + Environment.NewLine;
                ViewModel.WizardData.LoggedUser = ((App)Application.Current).LoggedUser.Copy();
                ViewModel.StoreInArt = ViewModel.WizardData.Wiz_StoreIn.StoreInArt;

                ViewModel.SelectedEingang = ViewModel.WizardData.Wiz_StoreIn.SelectedEingang.Copy();
                ViewModel.EingangOriginal = ViewModel.SelectedEingang;

                if (ViewModel.StoreInArt.Equals(enumStoreOutArt.NotSet))
                {
                    string mesInfo = "FEHLER";
                    string message = "Es ist keine Einlagerungsart gesetzt. Starten Sie den Prozess erneut vom Eingang Submenü.";
                    App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
                }
            }
            ViewModel.IsBaseNextEnabeld = false;
        }
        public Task OnAppearing()
        {
            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;
            return Task.CompletedTask;
        }

        public Task OnDissapearing()
        {
            SoftKeyboard.Current.VisibilityChanged -= Current_VisibilityChanged;
            return Task.CompletedTask;
        }

        public async Task<bool> OnNext(BaseViewModel viewModel)
        {
            bool bReturn = ViewModel.IsChecked;
            if (!ViewModel.IsChecked)
            {
                string mesInfo = "ACHTUNG";
                string message = "Der Eingang ist nicht gecheckt. Soll der Eingang noch als gecheckt markiert werden?";
                var resonseDisplayAlert = await App.Current.MainPage.DisplayAlert(mesInfo, message, "Check Eingang", "Abbrechen");
                if (resonseDisplayAlert)
                {
                    ViewModel.IsChecked = true;
                }
                bReturn = ViewModel.IsChecked;
            }

            if (!ViewModel.SelectedEingang.Equals(ViewModel.EingangOriginal))
            {
                ViewModel.IsBusy = true;
                try
                {
                    //Task.Run(() => Task.Delay(1000)).Wait();
                    //var result = Task.Run(() => ViewModel.UpdateEingang()).Result;
                    var result = await Task.Run(() => ViewModel.UpdateEingang());
                    bReturn = result.Success;
                    //ViewModel.IsBusy = false;
                    if (!result.Success)
                    {
                        string mesInfo = "FEHLER";
                        string message = result.Error;
                        await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
                    }
                    else
                    {
                        if ((!ViewModel.SelectedEingang.Check) && (result.Info.Length > 0))
                        {
                            string mesInfo = "INFORMATION";
                            string message = result.Info;
                            await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
                        }
                    }
                }
                finally
                {
                    ViewModel.IsBusy = false;
                }
            }
            else
            {
                bReturn = true;
            }
            ViewModel.WizardData.Wiz_StoreIn.SelectedEingang = ViewModel.SelectedEingang.Copy();
            ViewModel.WizardData.Wiz_StoreIn = ViewModel.WizardData.Wiz_StoreIn.Copy();
            ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
            if (bReturn)
            {
                await GoToNextPage();
            }
            return bReturn;
            //return Task.FromResult(bReturn);
        }

        private async Task GoToNextPage()
        {
            switch (ViewModel.WizardData.Wiz_StoreIn.StoreInArt)
            {
                case enumStoreInArt.open:
                    await Navigation.PushAsync(new oe1_OpenStoreInListPage());
                    break;
                case enumStoreInArt.edi:
                    await Navigation.PushAsync(new ee1_EdiLfsArticleList());
                    break;
                case enumStoreInArt.manually:

                    break;
                default:
                    await Navigation.PushAsync(new SubMenuStoreInPage());
                    break;
            }
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

        private void btnChecked_Clicked(object sender, EventArgs e)
        {
            string str = string.Empty;
            ViewModel.IsChecked = !ViewModel.IsChecked;
        }



    }
}