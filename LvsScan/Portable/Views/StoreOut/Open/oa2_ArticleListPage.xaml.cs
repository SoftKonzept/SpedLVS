using LvsScan.Portable.Controls;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels.StoreOut.Open;
using LvsScan.Portable.Views.Inventory;
using LvsScan.Portable.Views.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.StoreOut.Open
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class oa2_ArticleListPage : ContentPage
    {
        public oa2_ArticleListViewModel ViewModel;
        public oa2_ArticleListPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new oa2_ArticleListViewModel();
            ViewModel.Title = "Artikelliste";

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.Teststring += "eaw1_ArticleListViewModel " + Environment.NewLine;
                ViewModel.SelectedAusgang = ViewModel.WizardData.Wiz_StoreOut.SelectedAusgang.Copy();
                ViewModel.WizardData.AppProcess = ViewModel.AppProcess;

                ViewModel.WizardData.Wiz_StoreOut.StoreOutArt = ViewModel.StoreOutArt;
                ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
            }

            ViewModel.LoadValues = true;
            if (ViewModel.CountAll == ViewModel.CountCheck)
            {
                tabViewArticle.SelectedItem = tabViewArticle.Items[1];
            }
            InitToolMenu();
        }

        private void InitToolMenu()
        {
            this.ToolbarItems.Clear();
            bool bShowAusgangEdit = ViewModel.CountAll == ViewModel.CountCheck;
            bool bShowScanStart = ViewModel.CountCheck < ViewModel.CountAll;
            List<ToolbarItem> tmpList = ctr_MenuToolBarItem.CreateMenuStoreOut(bShowAusgangEdit, bShowScanStart, false, true);
            foreach (var item in tmpList)
            {
                item.Clicked += toolBarItem_Clicked;
                this.ToolbarItems.Add(item);
            }
        }

        private async void toolBarItem_Clicked(object sender, EventArgs e)
        {
            ToolbarItem tmpTbi = (ToolbarItem)sender;
            await DoMenuProcessAsync(tmpTbi.AutomationId);
        }


        private async Task DoMenuProcessAsync(string myAutomationId)
        {
            ViewModel.WizardData.Wiz_StoreOut.SelectedAusgang = ViewModel.SelectedAusgang.Copy();
            ViewModel.WizardData.Wiz_StoreOut.ListArticleInAusgang.Clear();
            ViewModel.WizardData.Wiz_StoreOut.ListArticleInAusgang = ViewModel.ArticlesInAusgang.ToList();
            ViewModel.WizardData.AppProcess = ViewModel.AppProcess;
            ViewModel.WizardData.Wiz_StoreOut.StoreOutArt = ViewModel.StoreOutArt;
            ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();

            //ToolbarItem tmpTbi = (ToolbarItem)sender;
            int iAutoId = 0;
            int.TryParse(myAutomationId, out iAutoId);
            switch (iAutoId)
            {
                case ctr_MenuToolBarItem.const_AutomationId_Home:
                    Application.Current.MainPage = new FlyoutMenuPage();
                    break;
                case ctr_MenuToolBarItem.const_AutomationId_Submenue:
                    await Navigation.PushAsync(new SubMenuStoreOutPage());
                    break;
                case ctr_MenuToolBarItem.const_AutomationId_Refresh:
                    ViewModel.LoadValues = true;
                    break;
                case ctr_MenuToolBarItem.const_AutomationId_Edit:
                    if (ViewModel.IsStoreOutStartVisible)
                    {
                        await Navigation.PushAsync(new a1w0_StoreOutHost());
                    }
                    else
                    {
                        string message = "Dieser Ausgang enthält noch unbearbeitet Artikel!" + Environment.NewLine;
                        message += "Nach der Bearbeitung aller Artikel können Sie im Auslagerungsprozess fortfahren!";
                        string mesInfo = "ACHTUNG";
                        await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
                    }
                    break;
                case ctr_MenuToolBarItem.const_AutomationId_ArticleScanStart:
                    if (ViewModel.IsArticleScanStartVisible)
                    {
                        if ((ViewModel.SelectedArticle is null) || (ViewModel.SelectedArticle.Id == 0))
                        {
                            ViewModel.SelectedArticle = ViewModel.ArticlesUnChecked[0];
                            ViewModel.WizardData.Wiz_StoreOut.ArticleToCheck = ViewModel.SelectedArticle;
                        }
                        await Navigation.PushAsync(new oa3_wizHost());
                    }
                    else
                    {
                        string message = "Alle Artikel dieses Ausgangs sind geprüft und freigegeben!" + Environment.NewLine;
                        message += "Sie können nun im Prozess fortfahren!";
                        string mesInfo = "INFORMATION";
                        await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
                    }
                    break;
                case ctr_MenuToolBarItem.const_AutomationId_Print:
                    await Navigation.PushAsync(new pw0_PrintHost());
                    break;
            }

        }
        private async void viewList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string str = string.Empty;
            await Navigation.PushAsync(new i3w0_wizInventoryHost());
        }



    }
}