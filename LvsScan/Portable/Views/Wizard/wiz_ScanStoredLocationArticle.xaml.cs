using Common.Enumerations;
using Common.Models;
using LvsScan.Portable.Interfaces;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels;
using LvsScan.Portable.ViewModels.Wizard;
using LvsScan.Portable.Views.StoredLocation;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.KeyboardHelper;

namespace LvsScan.Portable.Views.Wizard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class wiz_ScanStoredLocationArticle : ContentView, IWizardView
    {
        public wiz_ScanStoredLocationArticleViewModel ViewModel;
        public wiz_ScanStoredLocationArticle(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            initViewModel(currentViewModel);
        }
        public wiz_ScanStoredLocationArticle(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            initViewModel(currentViewModel);
        }

        private void initViewModel(BaseViewModel currentViewModel)
        {
            this.BindingContext = ViewModel = currentViewModel as wiz_ScanStoredLocationArticleViewModel;

            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;
            // set inactiv
            ViewModel.IsBaseNextEnabeld = true;
            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.Teststring += "wiz_ScanSearchArticle " + System.Environment.NewLine;
                ViewModel.AppProcess = ViewModel.WizardData.AppProcess;

                switch (ViewModel.AppProcess)
                {
                    case enumAppProcess.StoreIn:
                        ViewModel.ArticleOriginal = ViewModel.WizardData.Wiz_StoreIn.ArticleToCheck.Copy();
                        ViewModel.Article = ViewModel.WizardData.Wiz_StoreIn.ArticleToCheck.Copy();
                        break;

                    case enumAppProcess.StoreOut:
                        break;

                    case enumAppProcess.StoreLocationChange:
                        break;
                    default:
                        break;
                }
            }
            tabView.SelectedItem = tabView.Items[0];
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

            Task.Delay(10);
            Task.Run(() =>
           {
               entryScannedStorePlace.Focus();
           });

            if (ViewModel.Article.Id == 0)
            {
                string mesInfo = "FEHLER";
                string message = "Es liegen keine Artikeldaten vor. Wie werden zurück zur Artikelauswahl geleitet.";
                App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");

                Task.Delay(10);
                Task.Run(() =>
                {
                    Navigation.PushAsync(new sl1_ArticleSelectionPage());
                });
            }
            //return false;
            return Task.CompletedTask;
        }

        Task IWizardView.OnDissapearing()
        {
            string str = string.Empty;
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }


        async Task<bool> IWizardView.OnNext(BaseViewModel viewModel)
        {
            bool bUpdateOK = false;
            //throw new NotImplementedException();
            string message = string.Empty;
            string mesInfo = string.Empty;
            //save Article data

            switch (ViewModel.WizardData.AppProcess)
            {
                case enumAppProcess.StoreIn:
                    ViewModel.Article.ScanIn = DateTime.Now;
                    ViewModel.Article.ScanInUser = (int)((App)Application.Current).LoggedUser.Id;
                    break;

                case enumAppProcess.StoreOut:
                    ViewModel.Article.ScanOut = DateTime.Now;
                    ViewModel.Article.ScanOutUser = (int)((App)Application.Current).LoggedUser.Id;
                    break;
                default:
                    break;
            }

            var result = await ViewModel.ChangeStoreLocation();
            //////--- backup in WizardData
            ///
            ViewModel.WizardData.Wiz_StoreIn.ArticleToCheck = ViewModel.ArticleOriginal.Copy();
            ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();


            if (!result.SuccessStoreLocationChange)
            {
                message = "Es ist ein Fehler beim Update der Artikeldaten aufgetreten!" + System.Environment.NewLine;
                message += result.Error;
                mesInfo = "ACHTUNG";
                await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
            }
            return result.SuccessStoreLocationChange;
        }
        private void Current_VisibilityChanged(SoftKeyboardEventArgs e)
        {
            if (e.IsVisible)
            {   // do your things
                string str = string.Empty;
            }
            else
            {   // do your things
                string str = string.Empty;
            }
        }



        private async void tbiStoreLocationChange_Clicked(object sender, EventArgs e)
        {
            string mesInfo = string.Empty;
            string message = string.Empty;
            if (ViewModel.NewStoreLocation.Length > 0)
            {
                if (ViewModel.NewStoreLocation.Where(f => (f == '#')).Count() == 5)
                {
                    // Umbuchung durchführen
                    var result = await ViewModel.ChangeStoreLocation();
                    if ((ViewModel.Article is Articles) && (ViewModel.Article.Id > 0))
                    {
                        if (result.SuccessStoreLocationChange)
                        {
                            //Step 3
                            ((App)Application.Current).WizardData.Wiz_StoreLocationChange.ArticleChanged = ViewModel.Article;
                            //await DisplayAlert("Information", "Das Update wurde erfolgreich durchgeführt!", "OK");
                            mesInfo = "Information";
                            message = "Das Update wurde erfolgreich durchgeführt!";
                            await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
                            await Navigation.PushAsync(new sl3_StoreLocationChangeFinishPage());
                        }
                        else
                        {
                            message = "Das Update konnte nicht durchgeführt werden!" + Environment.NewLine;
                            message += result.Error;
                            mesInfo = "Information";
                            await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");

                            //await DisplayAlert("Information", message, "OK");
                        }
                    }
                }
                else
                {
                    mesInfo = "Achtung";
                    message = "Die Eingabe entspricht nicht der korrekten Formatierung!" + Environment.NewLine;
                    //await DisplayAlert("Fehler", message, "OK");
                    await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");

                }
            }
        }

        private void btnScannedStorePlace_Clicked(object sender, EventArgs e)
        {
            ViewModel.NewStoreLocation = string.Empty;
        }

        private void entryScannedStorePlace_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
    }
}