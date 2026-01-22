using Common.Models;
using LvsScan.Portable.Interfaces;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels;
using LvsScan.Portable.ViewModels.StoreIn.Manual;
using LvsScan.Portable.Views.Menu;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.KeyboardHelper;

namespace LvsScan.Portable.Views.StoreIn.Manual
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class me_StartInput : ContentView, IWizardView
    {
        public me_StartInputViewModel ViewModel { get; set; }
        public me_StartInput(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);
        }
        public me_StartInput(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            InitView(currentViewModel);

        }
        private void InitView(BaseViewModel currentViewModel)
        {
            if (currentViewModel != null)
            {
                BindingContext = ViewModel = currentViewModel as me_StartInputViewModel;
            }
            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                //if (ViewModel.WizardData.Wiz_StoreIn is null) 
                //{
                //    ViewModel.WizardData.Wiz_StoreIn = new wizStoreIn();
                //}

                ViewModel.WizardData.Teststring += "me_WorkspaceSelection " + Environment.NewLine;
                ViewModel.WizardData.LoggedUser = ((App)Application.Current).LoggedUser.Copy();
                ViewModel.SelectedEingangTableId = ViewModel.WizardData.Wiz_StoreIn.SelectedEingang.Id;
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

        public Task<bool> OnNext(BaseViewModel viewModel)
        {
            bool bReturn = true;

            ViewModel.WizardData.Wiz_StoreIn.SelectedEingang = ViewModel.SelectedEingang.Copy();
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
        private async void viewListMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string str = string.Empty;

            var item = e.CurrentSelection[0] as MenuSubItem;
            if (item == null)
                return;

            if (item.TargetType != null)
            {
                try
                {
                    var page = (Page)Activator.CreateInstance(item.TargetType);
                    page.Title = item.Title;
                    await Navigation.PushAsync(page);
                }
                catch (Exception ex)
                {
                    str = ex.Message.ToString();
                }
            }
            else
            {
                string message = "Es ist keine Zielseite hinterlegt!" + Environment.NewLine;
                string mesInfo = "FEHLER";
                await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
            }
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

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SubMenuStoreInPage());
        }
    }
}