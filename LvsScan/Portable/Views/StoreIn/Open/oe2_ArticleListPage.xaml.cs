using LvsScan.Portable.Controls;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels.StoreIn.Open;
using LvsScan.Portable.Views.Inventory;
using LvsScan.Portable.Views.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.XamarinForms.Primitives;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.StoreIn.Open
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class oe2_ArticleListPage : ContentPage
    {
        public oe2_ArticleListViewModel ViewModel;
        public oe2_ArticleListPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new oe2_ArticleListViewModel();
            ViewModel.Title = "Artikelliste";

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.Teststring += "eaw1_ArticleListViewModel " + Environment.NewLine;
                ViewModel.SelectedEingang = ViewModel.WizardData.Wiz_StoreIn.SelectedEingang.Copy();
                ViewModel.WizardData.Wiz_StoreIn.StoreInArt = ViewModel.StoreInArt;
                ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
            }

            ViewModel.LoadValues = true;
            tabViewArticle.SelectedItem = tabViewArticle.Items[0];
            if (ViewModel.CountAll == ViewModel.CountCheck)
            {
                tabViewArticle.SelectedItem = tabViewArticle.Items[1];
            }
            CheckSelectedTabAndSetValue();
            InitToolMenu();
        }

        private void InitToolMenu()
        {
            this.ToolbarItems.Clear();
            List<Xamarin.Forms.ToolbarItem> tmpList = ctr_MenuToolBarItem.CreateMenuStoreIn(ViewModel.IsStoreInEditVisible
                                                                                             , ViewModel.IsArticleScanStartVisible
                                                                                             , ViewModel.IsCreateEingangVisible);
            foreach (var item in tmpList)
            {
                item.Clicked += toolBarItem_Clicked;
                this.ToolbarItems.Add(item);
            }
        }

        private async void toolBarItem_Clicked(object sender, EventArgs e)
        {
            string message = string.Empty;
            string mesInfo = string.Empty;

            ViewModel.WizardData.Wiz_StoreIn.SelectedEingang = ViewModel.SelectedEingang.Copy();
            ViewModel.WizardData.Wiz_StoreIn.ArticleInEingang.Clear();
            ViewModel.WizardData.Wiz_StoreIn.ArticleInEingang = ViewModel.ArticlesInEingang.ToList();
            ViewModel.WizardData.Wiz_StoreIn.StoreInArt = ViewModel.StoreInArt;
            ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();

            Xamarin.Forms.ToolbarItem tmpTbi = (Xamarin.Forms.ToolbarItem)sender;
            switch (tmpTbi.AutomationId)
            {
                case "1":
                    Application.Current.MainPage = new FlyoutMenuPage();
                    break;
                case "2":
                    await Navigation.PushAsync(new SubMenuStoreInPage());
                    break;

                case "3":
                    ViewModel.LoadValues = true;
                    break;
                //edit EA
                case "4":
                    if (ViewModel.IsStoreInEditVisible)
                    {
                        await Navigation.PushAsync(new e1w0_StorInHost());
                    }
                    else
                    {
                        message = string.Empty;
                        mesInfo = string.Empty;
                        message = "Dieser Eingang enthält noch unbearbeitet Artikel!" + Environment.NewLine;
                        message += "Nach der Bearbeitung aller Artikel können Sie im Einlagerungsprozess fortfahren!";
                        mesInfo = "ACHTUNG";
                        await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
                    }
                    break;

                // edit article
                case "5":
                    if (ViewModel.IsArticleScanStartVisible)
                    {
                        if ((ViewModel.SelectedArticle is null) || (ViewModel.SelectedArticle.Id == 0))
                        {
                            ViewModel.SelectedArticle = ViewModel.ArticlesUnChecked[0];
                            ViewModel.WizardData.Wiz_StoreIn.ArticleToCheck = ViewModel.SelectedArticle;

                            //change to StoreInArt = open
                            ViewModel.WizardData.Wiz_StoreIn.StoreInArt = Common.Enumerations.enumStoreInArt.open;
                            ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
                        }
                        // - wizard Artikel
                        await Navigation.PushAsync(new oe3_w0_wizArticleHost());
                    }
                    else
                    {
                        message = "Alle Artikel dieses Ausgangs sind geprüft und freigegeben!" + Environment.NewLine;
                        message += "Sie können nun im Prozess fortfahren!";
                        mesInfo = "INFORMATION";
                        await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
                    }
                    break;
                case "6":
                    //ViewModel.IsBusy = true;
                    //Task.Run(() => Task.Delay(100)).Wait();
                    //var result = Task.Run(() => ViewModel.CreateStoreOutbyCall()).Result;
                    //ViewModel.IsBusy = false;

                    //mesInfo = string.Empty;
                    //message = string.Empty;
                    //if (result.Success)
                    //{
                    //    mesInfo = "INFORMATION";
                    //    message = result.Info;
                    //}
                    //else
                    //{
                    //    mesInfo = "FEHLER";
                    //    message = result.Error;
                    //}
                    //await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");

                    //await Navigation.PushAsync(new c1_CallArticleListPage());
                    break;

                case "7":
                    //ViewModel.IsBusy = true;
                    //Task.Run(() => Task.Delay(100)).Wait();
                    //var result = Task.Run(() => ViewModel.CreateStoreOutbyCall()).Result;
                    //ViewModel.IsBusy = false;

                    //string mesInfo = string.Empty;
                    //string message = string.Empty;
                    //if (result.Success)
                    //{
                    //    mesInfo = "INFORMATION";
                    //    message = result.Info;
                    //}
                    //else
                    //{
                    //    mesInfo = "FEHLER";
                    //    message = result.Error;
                    //}
                    //await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");

                    //await Navigation.PushAsync(new c1_CallArticleListPage());
                    break;
            }
        }
        private async void viewList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string str = string.Empty;
            await Navigation.PushAsync(new i3w0_wizInventoryHost());
        }

        private void tabViewArticle_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if ((ViewModel != null) && (e.PropertyName == "SelectedItem"))
            {
                CheckSelectedTabAndSetValue();
                InitToolMenu();
            }

        }
        private void CheckSelectedTabAndSetValue()
        {
            var selectedTab = tabViewArticle.SelectedItem as TabViewItem;
            if (selectedTab.HeaderText != null)
            {
                switch (selectedTab.HeaderText.ToUpper())
                {
                    case "CHECKED":
                        ViewModel.IsTabCheckedSelected = true;
                        ViewModel.IsTabUnCheckedSelected = false;
                        break;
                    case "OFFEN":
                        ViewModel.IsTabCheckedSelected = false;
                        ViewModel.IsTabUnCheckedSelected = true;
                        break;
                    default:
                        ViewModel.IsTabCheckedSelected = false;
                        ViewModel.IsTabUnCheckedSelected = false;
                        break;
                }
            }
        }
    }
}