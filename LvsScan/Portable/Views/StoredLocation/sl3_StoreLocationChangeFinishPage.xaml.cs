using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels.StoredLocation;
using LvsScan.Portable.Views.Menu;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.StoredLocation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class sl3_StoreLocationChangeFinishPage : ContentPage
    {
        public sl3_StoreLocationChangeFinishViewModel ViewModel;
        public sl3_StoreLocationChangeFinishPage()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new sl3_StoreLocationChangeFinishViewModel();

            if (((App)Application.Current).WizardData is WizardData)
            {
                ((App)Application.Current).WizardData.Teststring += "StoreLocationChangeFinish" + Environment.NewLine;
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                //Step3
                ViewModel.Article = ViewModel.WizardData.Wiz_StoreLocationChange.ArticleChanged;
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }
        private async void tbiHome_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new FlyoutMenuPage();
        }

        private async void tbiUB_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new sl1_ArticleSelectionPage());
        }
    }
}