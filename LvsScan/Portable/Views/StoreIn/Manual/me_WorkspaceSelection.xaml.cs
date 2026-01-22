using Common.Models;
using LvsScan.Portable.Interfaces;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels;
using LvsScan.Portable.ViewModels.StoreIn.Manual;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.KeyboardHelper;

namespace LvsScan.Portable.Views.StoreIn.Manual
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class me_WorkspaceSelection : ContentView, IWizardView
    {
        public me_WorkspaceSelectionViewModel ViewModel { get; set; }
        public me_WorkspaceSelection(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);
        }
        public me_WorkspaceSelection(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);

        }
        private void InitView(BaseViewModel currentViewModel)
        {
            if (currentViewModel != null)
            {
                BindingContext = ViewModel = currentViewModel as me_WorkspaceSelectionViewModel;
            }
            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                if (ViewModel.WizardData.Wiz_StoreIn is null)
                {
                    ViewModel.WizardData.Wiz_StoreIn = new wizStoreIn();
                }

                ViewModel.WizardData.Teststring += "me_WorkspaceSelection " + Environment.NewLine;
                ViewModel.WizardData.LoggedUser = ((App)Application.Current).LoggedUser.Copy();
                ViewModel.Init();
                ViewModel.LoadValues = true;
            }
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

            if (ViewModel.EingangToCreate != null)
            {
                string mesInfo = "ACHTUNG";
                string message = "Soll der Eingang im Arbeitsbereich " + ViewModel.SelectedWorkspace.Name + " erstellt werden?";
                var resonseDisplayAlert = await App.Current.MainPage.DisplayAlert(mesInfo, message, "Eingang erstellen", "Abbrechen");
                if (resonseDisplayAlert)
                {
                    Task.Run(() => Task.Delay(100)).Wait();
                    var result = Task.Run(() => ViewModel.CreateEingang()).Result;
                    bReturn = result.Success;

                    if (!result.Success)
                    {
                        mesInfo = "FEHLER";
                        message = result.Error;
                        await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
                        bReturn = false;
                    }
                }
            }

            ViewModel.WizardData.Wiz_StoreIn.SelectedEingang = ViewModel.EingangToCreate.Copy();
            ViewModel.WizardData.Wiz_StoreIn = ViewModel.WizardData.Wiz_StoreIn.Copy();
            ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
            //await Navigation.PushAsync(new me_wArticleInput_wizHost());
            return bReturn; // Task.FromResult(bReturn);
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
            //ViewModel.IsInputClientEnabled = !ViewModel.IsInputClientEnabled;
            ////ViewModel.GetClientAddresses();
            //ViewModel.IsBusy = true;
            //Task.Run(() => Task.Delay(1000)).Wait();
            //Task.Run(() => ViewModel.GetClientAddresses()).Wait();
            //ViewModel.IsBusy = false;
        }

        private void btnReceipin_Clicked(object sender, EventArgs e)
        {
            string str = string.Empty;
            //ViewModel.IsInputReciepinEnabled = !ViewModel.IsInputReciepinEnabled;
            //ViewModel.IsBusy = true;
            //Task.Run(() => Task.Delay(1000)).Wait();
            //Task.Run(() => ViewModel.GetReceipinAddresses()).Wait();
            //ViewModel.IsBusy = false;
        }

        private async void viewListWorkspaces_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string str = string.Empty;

            var item = e.CurrentSelection[0] as Workspaces;
            if (item == null)
                return;

            if (item != null)
            {

            }
            else
            {
                string message = "Es ist keine Zielseite hinterlegt!" + Environment.NewLine;
                string mesInfo = "FEHLER";
                await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
            }
        }

        //private void btnLfs_Clicked(object sender, EventArgs e)
        //{
        //    ViewModel.IsInputLfsEnabled = !ViewModel.IsInputLfsEnabled;            
        //    //inputLfs.Focus();
        //}

        //private void inputLfs_Completed(object sender, EventArgs e)
        //{
        //    ViewModel.IsInputLfsEnabled = !ViewModel.IsInputLfsEnabled;
        //}




        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    ViewModel.IsBusy = !ViewModel.IsBusy;
        //}

        //private void btnDate_Clicked(object sender, EventArgs e)
        //{
        //    if (ViewModel != null)
        //    {
        //        if (ViewModel.EADate.Equals(new DateTime(1900, 1, 1)))
        //        {
        //            ViewModel.EADate = DateTime.Now;
        //        }
        //    }
        //}
    }
}