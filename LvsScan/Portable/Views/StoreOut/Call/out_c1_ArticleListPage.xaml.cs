using Common.Models;
using LvsScan.Portable.Controls;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels.StoreOut.Open;
using LvsScan.Portable.Views.Menu;
using LvsScan.Portable.Views.StoreOut.Call;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.StoreOut.Open
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class out_c1_ArticleListPage : ContentPage
    {
        public c1_ArticleListViewModel ViewModel;
        public out_c1_ArticleListPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new c1_ArticleListViewModel();
            ViewModel.Title= "Aktuelle Abrufe";

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.LoggedUser = ((App)Application.Current).LoggedUser.Copy();
                ViewModel.WizardData.Teststring += "c1_ArticleListPage " + Environment.NewLine;

                ViewModel.WizardData.Wiz_StoreOut = new wizStoreOut();
                ViewModel.WizardData.Wiz_StoreOut.StoreOutArt = ViewModel.StoreOutArt;

                ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
            }
         
            ViewModel.LoadValues = true;
            InitToolMenu();
            if (ViewModel.CountAll == ViewModel.CountCheck)
            {
                //tabViewArticle.SelectedItem = tabViewArticle.Items[1];
            }
        }

        private void InitToolMenu()
        {
            this.ToolbarItems.Clear();
            bool bShowAusgangEdit = ViewModel.CountAll == ViewModel.CountCheck;
            bool bShowScanStart = ViewModel.CountCheck < ViewModel.CountAll;
            bool bShowCreateAusgang = (ViewModel.CountCheck>0);
            List<ToolbarItem> tmpList = ctr_MenuToolBarItemStoreOut.CreateMenu(bShowAusgangEdit, bShowScanStart, bShowCreateAusgang);
            foreach (var item in tmpList)
            {
                item.Clicked += toolBarItem_Clicked;
                this.ToolbarItems.Add(item);
            }
        }

        private async void toolBarItem_Clicked(object sender, EventArgs e)
        {
            ViewModel.WizardData.Wiz_StoreOut.CallsList = new List<Calls>(ViewModel.ListCallsAll.ToList());
            ViewModel.WizardData.Wiz_StoreOut.StoreOutArt = ViewModel.StoreOutArt;
            ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();

            ToolbarItem tmpTbi = (ToolbarItem)sender;
            switch (tmpTbi.AutomationId)
            {
                case "1":
                    Application.Current.MainPage = new FlyoutMenuPage();
                    break;
                case "2":
                    await Navigation.PushAsync(new SubMenuStoreOutPage());
                    break;

                case "3":
                    ViewModel.LoadValues = true;
                    break;
                case "4":
                    await Navigation.PushAsync(new c1_ArticleListPage());
                    break;
                case "5":
                    await Navigation.PushAsync(new c2_w0_wizHost());
                    break;
                case "6":
                    await Navigation.PushAsync(new c2_w0_wizHost());
                    break;
            }
        }
        private async void viewList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //await Navigation.PushAsync(new i3w0_wizInventoryHost());
            string str = string.Empty;
        }

        private void viewCheckedItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string str = string.Empty;
            if (
                (e.CurrentSelection != null) && (e.CurrentSelection.Count > 0)
              )
            {
                if (ViewModel != null)
                {
                    ViewModel.SelectedCallsToAusgang.Clear();
                    foreach (var item in e.CurrentSelection)
                    {
                        ViewModel.SelectedCallsToAusgang.Add((Calls)item);
                    }                   
                }
            }
            else
            {
                if (ViewModel != null)
                {
                    ViewModel.SelectedCallsToAusgang.Clear();
                }
            }
        }
    }
}