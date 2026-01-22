using Common.Enumerations;
using Common.Views;
using LvsScan.Portable.Interfaces;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels;
using LvsScan.Portable.ViewModels.Wizard;
using System;
using System.Linq;
using System.Threading.Tasks;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.KeyboardHelper;

namespace LvsScan.Portable.Views.Wizard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class wiz_DamageSelection : ContentView, IWizardView
    {
        public wiz_DamageSelectionViewModel ViewModel;
        public wiz_DamageSelection(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            initViewModel(currentViewModel);
        }
        public wiz_DamageSelection(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            initViewModel(currentViewModel);
        }

        private void initViewModel(BaseViewModel currentViewModel)
        {
            this.BindingContext = ViewModel = currentViewModel as wiz_DamageSelectionViewModel;

            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;
            // set inactiv
            ViewModel.IsBaseNextEnabeld = true;

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                //ViewModel.WizardData.Teststring += "wiz_DamageSelection " + System.Environment.NewLine;
                ViewModel.WizardData.Wiz_StoreOut.WizDamage = new wizDamage();
                ViewModel.AppProcess = ViewModel.WizardData.AppProcess;

                switch (ViewModel.WizardData.AppProcess)
                {
                    case enumAppProcess.StoreIn:
                        ViewModel.WizardData.Wiz_StoreIn.WizDamage.Article = ViewModel.WizardData.Wiz_StoreIn.ArticleToCheck.Copy();
                        ViewModel.SelectedArticle = ViewModel.WizardData.Wiz_StoreIn.WizDamage.Article.Copy();
                        break;

                    case enumAppProcess.StoreOut:
                        ViewModel.WizardData.Wiz_StoreOut.WizDamage.Article = ViewModel.WizardData.Wiz_StoreOut.ArticleToCheck.Copy();
                        ViewModel.SelectedArticle = ViewModel.WizardData.Wiz_StoreOut.ArticleToCheck.Copy();
                        break;

                    case enumAppProcess.StoreLocationChange:
                        break;
                    default:
                        break;
                }
                ViewModel.LoadValues = true;
            }
            else
            {
                string str = string.Empty;
            }
            tabView.SelectedItem = tabView.Items[0];
            ViewModel.ShowArticleDamageList = true;
            ViewModel.ShowDamageAssignment = false;
        }


        Task<bool> IWizardView.OnPrevious(BaseViewModel viewModel)
        {
            string str = string.Empty;
            return Task.FromResult(true);
        }

        Task IWizardView.OnAppearing()
        {
            string str = string.Empty;
            return Task.CompletedTask;
        }

        Task IWizardView.OnDissapearing()
        {
            string str = string.Empty;
            return Task.CompletedTask;
        }


        async Task<bool> IWizardView.OnNext(BaseViewModel viewModel)
        {
            //bool bUpdateOK = false;
            string message = string.Empty;
            string mesInfo = string.Empty;

            //bUpdateOK = true; // await ViewModel.UpdateArticleScanCheck();
            switch (ViewModel.WizardData.AppProcess)
            {
                case enumAppProcess.StoreIn:
                    ViewModel.WizardData.Wiz_StoreIn.WizDamage.Article = ViewModel.SelectedArticle.Copy();
                    ViewModel.WizardData.Wiz_StoreIn.ArticleToCheck = ViewModel.SelectedArticle.Copy();
                    ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
                    //await Navigation.PushAsync(new ArticleListPage());
                    break;
                case enumAppProcess.StoreOut:
                    ViewModel.WizardData.Wiz_StoreOut.WizDamage.Article = ViewModel.SelectedArticle.Copy();
                    ViewModel.WizardData.Wiz_StoreOut.ArticleToCheck = ViewModel.SelectedArticle.Copy();

                    ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
                    //await Navigation.PushAsync(new oa2_ArticleListPage());
                    break;

                case enumAppProcess.StoreLocationChange:
                    break;
                default:
                    break;
            }
            return true; // Task.FromResult(true);
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

        // ------------------------------------------------------------------------------------------------------------------------------------

        private async void tabView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
        }

        private void RadButton_Clicked(object sender, EventArgs e)
        {
            ViewModel.IsBusy = !ViewModel.IsBusy;
        }

        private async void btnAdd_Clicked(object sender, EventArgs e)
        {
            string str = string.Empty;
            if (
                (ViewModel.SelectedDamage != null) &&
                (ViewModel.SelectedDamage.Id > 0)
              )
            {
                await ViewModel.AddDamageArticleAssignmentItem();
            }
            else
            {
                string message = string.Empty;
                string mesInfo = string.Empty;
                message = "Es wurde kein Artikel ausgewählt! Der Vorgang wird abgebrochen.";
                mesInfo = "ACHTUNG";
                await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
            }
        }

        private void btnRefresh_Clicked(object sender, EventArgs e)
        {

        }

        private void btnExistDamages_Clicked(object sender, EventArgs e)
        {
            EnabledShowControl();
        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            EnabledShowControl();
        }

        private void EnabledShowControl()
        {
            ViewModel.ShowDamageAssignment = !ViewModel.ShowDamageAssignment;
            ViewModel.ShowArticleDamageList = !ViewModel.ShowDamageAssignment;
        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            string str = string.Empty;
            if (
                (ViewModel.SelectedArticleDamage is DamageArticleAssignmentView) &&
                (ViewModel.SelectedArticleDamage.Id > 0)
              )
            {
                int iTmp = 0;
                iTmp = (int)((RadButton)sender).CommandParameter;
                await ViewModel.DeleteDamageArticleAssignmentItem();

            }
        }

        private void viewArticleDamageAssignments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string str = string.Empty;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            string str = string.Empty;
            if (ViewModel.DamageArticleAssignmentList.Count > 0)
            {
                int iTmp = 0;
                string strTmp = (sender as StackLayout).AutomationId.ToString();
                if (int.TryParse(strTmp, out iTmp))
                {
                    DamageArticleAssignmentView tmpSelected = ViewModel.DamageArticleAssignmentList.FirstOrDefault(x => x.Id == iTmp);
                    if (tmpSelected != null)
                    {
                        if (ViewModel.SelectedArticleDamage is null)
                        {
                            viewArticleDamageAssignments.SelectedItem = tmpSelected;
                        }
                        else
                        {
                            if (ViewModel.SelectedArticleDamage.Id.Equals(tmpSelected.Id))
                            {
                                viewArticleDamageAssignments.SelectedItem = null;
                            }
                            else
                            {
                                viewArticleDamageAssignments.SelectedItem = tmpSelected;
                            }
                        }
                    }
                }
            }
        }
    }
}