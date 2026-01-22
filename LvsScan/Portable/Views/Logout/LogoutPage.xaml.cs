using LvsScan.Portable.ViewModels;
using LvsScan.Portable.ViewModels.Logout;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.Logout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogoutPage : ContentPage
    {
        public LogoutViewModel ViewModel { get; set; }
        public LogoutPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.BindingContext = this.ViewModel = new LogoutViewModel();
            Task.Delay(2000);
            //ShowExitConfirmation();
            ExitApp();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            this.BindingContext = new BaseViewModel();

            //// Inform the view model that it is disappearing so that it can remove event handlers
            //// and perform any other clean-up required..
            ////viewModel?.OnDisappearing();
        }

        private void ShowExitConfirmation()
        {
            //Action extiAction = new Action(ExitApp);
            //Action goBackAction = new Action(GoBack);

            //ConfirmConfig confirmConfig = new ConfirmConfig();
            //confirmConfig.Title = "Beenden";
            //confirmConfig.Message = "Soll die Ticketing Scan App wirklich geschlossen werden?";
            //confirmConfig.OkText = "Beenden";
            //confirmConfig.CancelText = "Abbruch";
            //confirmConfig.OnAction = (confirmed) =>
            //{
            //    if (confirmed)
            //        extiAction();
            //    else
            //        goBackAction();
            //};
            //UserDialogs.Instance.Confirm(confirmConfig);
        }

        public void ExitApp()
        {
            // Abfrage vor Beendigung der App
            Process.GetCurrentProcess().Kill();
        }

        public async void GoBack()
        {
            await Shell.Current.GoToAsync("//Main");
        }
    }
}