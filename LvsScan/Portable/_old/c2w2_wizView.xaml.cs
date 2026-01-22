using Common.Models;
using LvsScan.Portable.Interfaces;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels;
using LvsScan.Portable.ViewModels.StoreOut.Call;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.KeyboardHelper;

namespace LvsScan.Portable.Views.StoreOut.Call
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class c2w2_wizView : ContentView, IWizardView
    {
        public c2w2_wizViewModel ViewModel;
        public c2w2_wizView(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            initViewModel(currentViewModel);
        }

        public c2w2_wizView(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            initViewModel(currentViewModel);
        }
        private void initViewModel(BaseViewModel currentViewModel)
        {
            this.BindingContext = ViewModel = currentViewModel as c2w2_wizViewModel;

            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;
            // set inactiv
            ViewModel.IsBaseNextEnabeld = true;
            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.Teststring += "c2w1_wizViewModel " + System.Environment.NewLine;
                ViewModel.WizardData.LoggedUser = ((App)Application.Current).LoggedUser.Copy();

                ViewModel.CallArticles = new ObservableCollection<Calls>(ViewModel.WizardData.Wiz_StoreOut.CallsList);
                ViewModel.WizardData.Wiz_StoreOut.StoreOutArt = ViewModel.StoreOutArt;
            }
        }
        async Task<bool> IWizardView.OnNext(BaseViewModel viewModel)
        {
            string str = string.Empty;
            //throw new NotImplementedException();
            await GoToNextPage();

            return true;
            //return Task.FromResult(true);
        }

        Task<bool> IWizardView.OnPrevious(BaseViewModel viewModel)
        {
            string str = string.Empty;
            //throw new NotImplementedException();

            return Task.FromResult(true);
        }

        async Task IWizardView.OnAppearing()
        {
            string str = string.Empty;
            //// set inactiv
            ViewModel.IsBaseNextEnabeld = true;
            await GoToNextPage();
            //throw new NotImplementedException();
            //return Task.CompletedTask;
        }

        Task IWizardView.OnDissapearing()
        {
            string str = string.Empty;
            //throw new NotImplementedException();
            return Task.CompletedTask;
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


        //---------------------------------------------------------------------------------------------------------------
        private void Button_Clicked(object sender, EventArgs e)
        {
            ViewModel.IsBaseNextEnabeld = (!ViewModel.IsBaseNextEnabeld);
        }

        private async Task GoToNextPage()
        {
            string message = string.Empty;
            string mesInfo = string.Empty;
            message = "Artikeldaten konnten erfolgreich upgedatet werden!" + System.Environment.NewLine;
            mesInfo = "INFORMATION";
            await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");

            ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
            if (ViewModel.CountUncheckedArticles > 0)
            {
                await Navigation.PushAsync(new c2_w0_wizHost());
            }
            else
            {
                await Navigation.PushAsync(new c1_CallArticleListPage());
            }
        }
    }
}