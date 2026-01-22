using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels.StoreOut.Open;
using LvsScan.Portable.Views.Menu;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.StoreOut.Open
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class oa1_OpenStoreOutListPage : ContentPage
    {
        public oa1_OpenStoreOutListViewModel ViewModel;
        public oa1_OpenStoreOutListPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new oa1_OpenStoreOutListViewModel();

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = new WizardData();
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.LoggedUser = ((App)Application.Current).LoggedUser.Copy();
                ViewModel.WizardData.Teststring += "eaw0_ExistingStoreOutListPage " + Environment.NewLine;
                ViewModel.WizardData.AppProcess = ViewModel.AppProcess;
                ViewModel.WizardData.Wiz_StoreOut = new wizStoreOut();
                ViewModel.WizardData.Wiz_StoreOut.StoreOutArt = ViewModel.StoreOutArt;
                //ViewModel.WizardData.Wiz_StoreOut.SelectedAusgang = ViewModel.SelectedAusgang.Copy();            

                ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
            }
            //ViewModel.LoadValues = true;
            //-- test mr 20250709
            // Asynchrones Laden der Liste
            Task.Run(async () => await LoadDataAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task LoadDataAsync()
        {
            ViewModel.IsBusy = true;
            try
            {
                await ViewModel.GetAusaengeList();
            }
            finally
            {
                ViewModel.IsBusy = false;
            }
        }
        /// <summary>
        ///             Refresh der Ausgangsliste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Refresh_Clicked(object sender, EventArgs e)
        {
            //-- list wird im Property LoadValue neu geladen
            //ViewModel.LoadValues = true;
            // Asynchrones Laden der Liste
            Task.Run(async () => await LoadDataAsync());
        }
        /// <summary>
        ///             Zur Artikelliste der Ausgänge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void StoreOutStart_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new oa2_ArticleListPage());
        }
        private void Home_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new FlyoutMenuPage();
        }
        private async void viewList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ViewModel != null)
            {
                ViewModel.WizardData.Wiz_StoreOut.SelectedAusgang = this.ViewModel.SelectedAusgang.Copy();
                ViewModel.WizardData.Wiz_StoreOut.ListArticleInAusgang.Clear();
                ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();

                await Navigation.PushAsync(new oa2_ArticleListPage());
            }
        }
    }
}