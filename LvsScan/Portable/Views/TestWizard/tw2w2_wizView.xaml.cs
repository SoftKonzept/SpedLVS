using LvsScan.Portable.Interfaces;
using LvsScan.Portable.ViewModels;
using LvsScan.Portable.ViewModels.TestWizard;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.KeyboardHelper;

namespace LvsScan.Portable.Views.TestWizard
{
    public partial class tw2w2_wizView : ContentView, IWizardView
    {
        /// <summary>
        ///             Wizzard
        ///             Step 2
        /// </summary>
        public tw2w2_wizViewModel ViewModel;
        public tw2w2_wizView(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            initViewModel(currentViewModel);
        }

        public tw2w2_wizView(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            initViewModel(currentViewModel);
        }
        private void initViewModel(BaseViewModel currentViewModel)
        {
            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;
            this.BindingContext = ViewModel = currentViewModel as tw2w2_wizViewModel;

        }

        async Task<bool> IWizardView.OnNext(BaseViewModel viewModel)
        {
            string str = string.Empty;
            //throw new NotImplementedException();


            //-- zürück zur Schleife, des Wizards
            //await Navigation.PushAsync(new tw2_wizHost());

            await Navigation.PushAsync(new tw1_SelectionPage());
            return true;
        }

        Task<bool> IWizardView.OnPrevious(BaseViewModel viewModel)
        {
            string str = string.Empty;
            //throw new NotImplementedException();
            return Task.FromResult(true);
        }

        Task IWizardView.OnAppearing()
        {
            string str = string.Empty;
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }

        Task IWizardView.OnDissapearing()
        {
            string str = string.Empty;
            //throw new NotImplementedException();
            SoftKeyboard.Current.VisibilityChanged -= Current_VisibilityChanged;
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
    }
}