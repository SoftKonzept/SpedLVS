using Common.Enumerations;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels.StoreOut;
using LvsScan.Portable.Views.Menu;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.StoreOut
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class a1_AusgangInputDataPage : ContentPage
    {
        public a1_AusgangInputDataViewModel ViewModel;

        public a1_AusgangInputDataPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new a1_AusgangInputDataViewModel(enumStoreOutArt.NotSet);
            Init();
        }
        public a1_AusgangInputDataPage(enumStoreOutArt myStoreOutArt) : this()
        {
            InitializeComponent();
            BindingContext = ViewModel = new a1_AusgangInputDataViewModel(enumStoreOutArt.NotSet);
            Init();
        }
        private void Init()
        {
            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.LoggedUser = ((App)Application.Current).LoggedUser.Copy();
                ViewModel.WizardData.Teststring += "a1_AusgangInputDataViewModel " + Environment.NewLine;

                ViewModel.WizardData.Wiz_StoreOut = new wizStoreOut();
                ViewModel.WizardData.Wiz_StoreOut.StoreOutArt = enumStoreOutArt.open;

                ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
            }
            //ViewModel.SelectedAusgang = new Ausgaenge();
            //ViewModel.SelectedAusgang.Id = 53976;
            ViewModel.LoadValues = true;
        }

        private void Home_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new FlyoutMenuPage();
        }
        private async void SubmenuStoreOut_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SubMenuStoreOutPage());
        }

        private void button_Clicked(object sender, EventArgs e)
        {
            string str = string.Empty;
            if (ViewModel.IsInputKFZEnabled)
            {
                ViewModel.IsInputKFZEnabled = false;
            }
            else
            {
                ViewModel.IsInputKFZEnabled = true;
            }

            if (ViewModel.IsBusy)
            {
                ViewModel.IsBusy = false;
            }
            else
            {
                ViewModel.IsBusy = true;
            }
        }

        private void btnClient_Clicked(object sender, EventArgs e)
        {
            string str = string.Empty;
            ViewModel.IsInputClientEnabled = !ViewModel.IsInputClientEnabled;
            //ViewModel.GetClientAddresses();
            ViewModel.IsBusy = true;
            Task.Run(() => ViewModel.GetClientAddresses()).Wait();
            ViewModel.IsBusy = false;
        }

        private void btnReceipin_Clicked(object sender, EventArgs e)
        {
            string str = string.Empty;
            ViewModel.IsInputReciepinEnabled = !ViewModel.IsInputReciepinEnabled;
            ViewModel.IsBusy = true;
            Task.Run(() => ViewModel.GetReceipinAddresses()).Wait();
            ViewModel.IsBusy = false;
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            //ViewModel.ShowBusyIndicator=!ViewModel.ShowBusyIndicator;
            //ViewModel.ShowContent = !ViewModel.ShowBusyIndicator;
            if (ViewModel.IsBusy)
            {
                ViewModel.IsBusy = false;
            }
            else
            {
                ViewModel.IsBusy = true;
            }
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            ViewModel.StoreOutArt = enumStoreOutArt.open;
            ViewModel.WizardData = new WizardData();
            ViewModel.WizardData.Wiz_StoreOut = new wizStoreOut();
            ViewModel.WizardData.Wiz_StoreOut.SelectedAusgang = ViewModel.SelectedAusgang.Copy();
            ViewModel.WizardData.Wiz_StoreOut.StoreOutArt = ViewModel.StoreOutArt;

            ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
            await Navigation.PushAsync(new a1w0_StoreOutHost());
        }

        private void Button_Clicked_3(object sender, EventArgs e)
        {
            if (ViewModel.IsBusy)
            {
                ViewModel.IsBusy = false;
            }
            else
            {
                ViewModel.IsBusy = true;
            }
        }

        private void btnTermin_Clicked(object sender, EventArgs e)
        {

        }

        private async void Button_Clicked_4(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new a1_AusgangInputDataPage());
        }

        private void ASNTest_Clicked(object sender, EventArgs e)
        {
            string str = string.Empty;

            //LVS.ViewData.AusgangViewData ausVD = new LVS.ViewData.AusgangViewData(ViewModel.SelectedAusgang, (int)ViewModel.WizardData.LoggedUser.Id);
            //var result = ausVD.PrintDocuments();

            str = string.Empty;

        }
    }
}