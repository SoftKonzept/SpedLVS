using Common.Models;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels.StoredLocation;
using LvsScan.Portable.Views.Menu;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.StoredLocation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class sl2_StoreLocationChangePage : ContentPage
    {
        public sl2_StoreLocationChangeViewModel ViewModel;
        public sl2_StoreLocationChangePage()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new sl2_StoreLocationChangeViewModel();

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                //ViewModel.WizardData.Wiz_StoreLocationChange = ((App)Application.Current).WizardData.Wiz_StoreLocationChange.Copy();
                ViewModel.WizardData.Teststring += "StoreLocationChange" + Environment.NewLine;
                ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();

                //Step2
                ViewModel.ArticleOriginal = ViewModel.WizardData.Wiz_StoreLocationChange.ArticleToChange.Copy();
                ViewModel.Article = ViewModel.WizardData.Wiz_StoreLocationChange.ArticleToChange.Copy();
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(10);
            await Task.Run(() =>
            {
                entryScannedStorePlace.Focus();
            });

            if (ViewModel.Article.Id == 0)
            {
                await DisplayAlert("FEHLER", "Es liegen keine Artikeldaten vor. Wie werden zurück zur Artikelauswahl geleitet.", "OK");
                await Navigation.PushAsync(new sl1_ArticleSelectionPage());
            }
        }
        private async void tbiStoreLocationChange_Clicked(object sender, EventArgs e)
        {
            if (
                    (
                        (ViewModel.NewStoreLocation.Length > 0) &&
                        (ViewModel.TabScanInputSelected)
                    )
                    ||
                    (
                        (ViewModel.NewStoreLocation.Length == 0) &&
                        (ViewModel.TabManuelInputSelected)
                    )
               )
            {
                //if (ViewModel.NewStoreLocation.Where(f => (f == '#')).Count() == 5)
                //{
                //    // Umbuchung durchführen
                //    if (!ViewModel.NewStoreLocation.Equals(string.Empty))
                //    {
                await ViewModel.ChangeStoreLocation();
                if ((ViewModel.Article is Articles) && (ViewModel.Article.Id > 0))
                {
                    if (ViewModel.ResponseStoreLocChange.SuccessStoreLocationChange)
                    {
                        //Step 3
                        ((App)Application.Current).WizardData.Wiz_StoreLocationChange.ArticleChanged = ViewModel.Article;
                        await DisplayAlert("Information", "Das Update wurde erfolgreich durchgeführt!", "OK");
                        await Navigation.PushAsync(new sl3_StoreLocationChangeFinishPage());
                    }
                    else
                    {
                        string message = "Das Update konnte nicht durchgeführt werden!" + Environment.NewLine;
                        message += ViewModel.ResponseStoreLocChange.Error;
                        await DisplayAlert("Information", message, "OK");
                    }
                }
                //        }
                //}
                //else
                //{
                //    string message = "Die Eingabe entspricht nicht der korrekten Formatierung!" + Environment.NewLine;
                //    await DisplayAlert("Fehler", message, "OK");
                //}
            }
        }

        private void btnScannedStorePlace_Clicked(object sender, EventArgs e)
        {
            ViewModel.NewStoreLocation = string.Empty;
        }

        private void entryScannedStorePlace_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private async void tbiHome_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new FlyoutMenuPage();
        }

        private void tabView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("SelectedItem"))
            {
                string s = string.Empty;
            }
        }
    }
}