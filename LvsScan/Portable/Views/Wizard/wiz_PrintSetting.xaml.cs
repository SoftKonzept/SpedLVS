using Common.Enumerations;
using LvsScan.Portable.Interfaces;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels;
using LvsScan.Portable.ViewModels.Wizard;
using LvsScan.Portable.Views.StoreOut.Open;
//using Plugin.Media.Abstractions;
//using Plugin.Media;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.KeyboardHelper;

namespace LvsScan.Portable.Views.Wizard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class wiz_PrintSetting : ContentView, IWizardView
    {
        public wiz_PrintSettingsViewModel ViewModel;
        public wiz_PrintSetting(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            initViewModel(currentViewModel);
        }
        public wiz_PrintSetting(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            initViewModel(currentViewModel);
        }

        private void initViewModel(BaseViewModel currentViewModel)
        {
            this.BindingContext = ViewModel = currentViewModel as wiz_PrintSettingsViewModel;

            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;
            // set inactiv
            //ViewModel.IsBaseNextEnabeld = true;
            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.Teststring += "wiz_ArticleCheckViewModel " + System.Environment.NewLine;
                ViewModel.AppProcess = ViewModel.WizardData.AppProcess;

                switch (ViewModel.AppProcess)
                {
                    case enumAppProcess.StoreIn:
                        //ViewModel.SelectedArticle = ViewModel.WizardData.Wiz_StoreIn.WizDamage.Article.Copy();
                        //ViewModel.StoreInArt = ViewModel.WizardData.Wiz_StoreIn.StoreInArt;
                        //ViewModel.CurrentStepStoreInArt = enumStoreInArt_Steps.wizStepInputPhoto;
                        //ViewModel.SelectedEingang = ViewModel.WizardData.Wiz_StoreIn.SelectedEingang.Copy();
                        break;

                    case enumAppProcess.StoreOut:
                        ViewModel.StoreOutArt = ViewModel.WizardData.Wiz_StoreOut.StoreOutArt;
                        ViewModel.SelectedAusgang = ViewModel.WizardData.Wiz_StoreOut.SelectedAusgang.Copy();
                        ViewModel.SelectedArticle = ViewModel.WizardData.Wiz_StoreOut.ArticleToCheck.Copy();
                        break;

                    case enumAppProcess.StoreLocationChange:
                        break;
                    default:
                        break;
                }

            }

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
            return Task.CompletedTask;
        }


        async Task<bool> IWizardView.OnNext(BaseViewModel viewModel)
        {
            string message = string.Empty;
            string mesInfo = string.Empty;

            bool bUpdateOK = false;

            switch (ViewModel.AppProcess)
            {
                case enumAppProcess.StoreIn:

                    break;
                case enumAppProcess.StoreOut:
                    //if (ViewModel.IsChecked)
                    //{
                    //    bUpdateOK = await ViewModel.UpdateArticleScanCheck();
                    //    ViewModel.WizardData.Wiz_StoreOut.SelectedAusgang = ViewModel.SelectedAusgang.Copy();
                    //    ViewModel.WizardData.Wiz_StoreOut.ArticleToCheck = new Common.Models.Articles();

                    //    if (!bUpdateOK)
                    //    {
                    //        message = "Es ist ein Fehler beim Update der Artikeldaten aufgetreten!" + System.Environment.NewLine;
                    //        mesInfo = "ACHTUNG";
                    //        await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
                    //    }
                    //}
                    break;

                case enumAppProcess.StoreLocationChange:
                    break;
                default:
                    break;
            }
            ViewModel.WizardData.AppProcess = ViewModel.AppProcess;
            ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
            //return Task.FromResult(true); 
            await Navigation.PushAsync(new oa2_ArticleListPage());

            return bUpdateOK;
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




    }
}