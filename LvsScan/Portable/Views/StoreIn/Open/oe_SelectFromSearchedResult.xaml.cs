using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels.StoreIn.Open;
using LvsScan.Portable.Views.Menu;
using LvsScan.Portable.Views.StoreOut.Open;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.StoreIn.Open
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class oe_SelectFromSearchedResult : ContentPage
    {
        public oe_SelectFromSearchedResultViewModel ViewModel { get; set; }
        public oe_SelectFromSearchedResult()
        {
            InitializeComponent();
            BindingContext = ViewModel = new oe_SelectFromSearchedResultViewModel();

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.AppProcess = ViewModel.AppProcess;
                ViewModel.WizardData.LoggedUser = ((App)Application.Current).LoggedUser.Copy();
                ViewModel.WizardData.Teststring += "oe1_OpenStoreInListPage " + Environment.NewLine;
                //ViewModel.WizardData.Wiz_StoreIn = new wizStoreIn();
                ViewModel.WizardData.Wiz_StoreIn.StoreInArt = ViewModel.StoreInArt;

                ViewModel.ArticlesToCheck = new System.Collections.ObjectModel.ObservableCollection<Common.Models.Articles>(ViewModel.WizardData.Wiz_StoreIn.ArticlesToCheck);

                ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
            }
        }

        private void Refresh_Clicked(object sender, EventArgs e)
        {
            //ViewModel.LoadValues = true;
        }
        private async void StoreOutStart_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new oa2_ArticleListPage());
        }
        private void Home_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new FlyoutMenuPage();
        }
        private async void Submenu_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SubMenuStoreInPage());
        }
        private async void viewList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((this.ViewModel != null) && (this.ViewModel.SelectedEingang.Id > 0))
            {
                ViewModel.WizardData.Wiz_StoreIn.SelectedEingang = this.ViewModel.SelectedEingang.Copy();
                ViewModel.WizardData.Wiz_StoreIn.ArticleInEingang.Clear();
                ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();

                await Navigation.PushAsync(new oe2_ArticleListPage());
            }

        }
    }
}