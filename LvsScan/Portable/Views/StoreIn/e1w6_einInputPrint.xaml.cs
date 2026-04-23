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
    public partial class e1w6_einInputPrint : ContentView, IWizardView
    {
        public e1w6_einInputPrintViewModel ViewModel { get; set; }
        public e1w6_einInputPrint(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);
        }
        public e1w6_einInputPrint(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);

        }
        private void InitView(BaseViewModel currentViewModel)
        {
            if (currentViewModel != null)
            {
                BindingContext = ViewModel = currentViewModel as e1w6_einInputPrintViewModel;
            }
            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.Teststring += "e1w6_einInputPrintViewModel " + Environment.NewLine;
                ViewModel.WizardData.LoggedUser = ((App)Application.Current).LoggedUser.Copy();
                ViewModel.StoreInArt = ViewModel.WizardData.Wiz_StoreIn.StoreInArt;
                ViewModel.SelectedEingang = ViewModel.WizardData.Wiz_StoreIn.SelectedEingang.Copy();
                ViewModel.EingangOriginal = ViewModel.WizardData.Wiz_StoreIn.SelectedEingang.Copy();
                ViewModel.Init();

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
            return Task.CompletedTask;
        }

        public Task OnDissapearing()
        {
            return Task.CompletedTask;
        }

        public async Task<bool> OnNext(BaseViewModel viewModel)
        {
            bool bReturn = false;

            // Verwende Id‑Vergleich statt unsicheren Equals, falls verfügbar
            bool changed = (ViewModel.SelectedEingang?.Id ?? 0) != (ViewModel.EingangOriginal?.Id ?? 0)
                           || !Equals(ViewModel.SelectedEingang, ViewModel.EingangOriginal); // Fallback

            if (changed)
            {
                ViewModel.IsBusy = true;
                try
                {
                    // Optional: kurze nicht‑blockierende Pause für UX (oder ganz weglassen)
                    // await Task.Delay(200);

                    // CHANGED: entferne ConfigureAwait(false) damit die Fortsetzung auf dem UI‑Thread läuft
                    var result = await ViewModel.UpdateEingang();

                    // CHANGED: IsBusy auf UI‑Thread zurücksetzen (ist hier auf UI‑Thread, weil kein ConfigureAwait(false) verwendet wurde)
                    ViewModel.IsBusy = false;

                    if (result == null || !result.Success)
                    {
                        //string message = result?.Error ?? "Das Update konnte nicht durchgeführt werden.";
                        string message = "Das Update konnte nicht durchgeführt werden.";
                        // DisplayAlert jetzt sicher auf dem UI‑Thread
                        await App.Current.MainPage.DisplayAlert("FEHLER", message, "OK");
                        bReturn = false;
                    }
                    else
                    {
                        // Optional: Schritt als abgeschlossen markieren
                        //ViewModel.IsStepFinished = true;
                        bReturn = true;
                    }
                }
                catch (Exception ex)
                {
                    ViewModel.IsBusy = false;
                    await App.Current.MainPage.DisplayAlert("FEHLER", ex.Message, "OK");
                    bReturn = false;
                }
            }
            else
            {
                bReturn = true;
            }


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
            ViewModel.WizardData.Wiz_StoreIn = ViewModel.WizardData.Wiz_StoreIn.Copy();
            ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();

            //return Task.FromResult(bReturn);
            return bReturn;
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