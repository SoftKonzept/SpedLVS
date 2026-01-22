using Common.Models;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels.StoreIn.Open;
using LvsScan.Portable.Views.Menu;
using LvsScan.Portable.Views.StoreIn.Manual;
using LvsScan.Portable.Views.StoreOut.Open;
using System;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.StoreIn.Open
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class oe1_OpenStoreInListPage : ContentPage
    {
        public oe1_OpenStoreOutListViewModel ViewModel { get; set; }
        public oe1_OpenStoreInListPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new oe1_OpenStoreOutListViewModel();

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.AppProcess = ViewModel.AppProcess;
                ViewModel.WizardData.LoggedUser = ((App)Application.Current).LoggedUser.Copy();
                ViewModel.WizardData.Teststring += "oe1_OpenStoreInListPage " + Environment.NewLine;
                ViewModel.WizardData.Wiz_StoreIn = new wizStoreIn();
                ViewModel.WizardData.Wiz_StoreIn.StoreInArt = ViewModel.StoreInArt;

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
                await ViewModel.GetEingangList();
            }
            finally
            {
                ViewModel.IsBusy = false;
            }
        }
        private void Refresh_Clicked(object sender, EventArgs e)
        {
            //ViewModel.LoadValues = true;
            // Asynchrones Laden der Liste
            Task.Run(async () => await LoadDataAsync());
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
            if (this.ViewModel != null)
            {
                ViewModel.WizardData.Wiz_StoreIn.SelectedEingang = this.ViewModel.SelectedEingang.Copy();
                ViewModel.WizardData.Wiz_StoreIn.ArticleInEingang.Clear();
                ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();

                await Navigation.PushAsync(new oe2_ArticleListPage());
            }
        }
        private async void btnEditManualStoreIn_Clicked(object sender, EventArgs e)
        {
            string str = string.Empty;
            Button tmpBtn = (Button)sender;
            if (tmpBtn != null)
            {
                if (tmpBtn.AutomationId.Length > 0)
                {
                    int iTmp = 0;
                    int.TryParse(tmpBtn.AutomationId, out iTmp);
                    if (iTmp > 0)
                    {
                        Eingaenge tmpEA = ViewModel.EingangOpen.FirstOrDefault(x => x.Id == iTmp);
                        if (tmpEA is Eingaenge)
                        {
                            ViewModel.SelectedEingang = tmpEA.Copy();
                            //äandern der Art
                            ViewModel.WizardData.Wiz_StoreIn.StoreInArt = Common.Enumerations.enumStoreInArt.manually;
                            ViewModel.WizardData.Wiz_StoreIn.SelectedEingang = this.ViewModel.SelectedEingang.Copy();
                            ViewModel.WizardData.Wiz_StoreIn.ArticleInEingang.Clear();
                            ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();

                            await Navigation.PushAsync(new me_wArticleInput_wizHost());
                        }
                    }
                }
            }
        }
    }
}