using Common.Enumerations;
using LvsScan.Portable.Controls;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels.StoreIn;
using LvsScan.Portable.Views.Menu;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.StoreIn
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class e1_EingangInputDataPage : ContentPage
    {
        public e1_EingangInputDataViewModel ViewModel { get; set; }
        public e1_EingangInputDataPage()
        {
            InitializeComponent();
            //BindingContext = ViewModel = new e1_EingangInputDataViewModel(enumStoreInArt.NotSet);
            //Init();
        }
        public e1_EingangInputDataPage(enumStoreInArt myStoreInArt) : this()
        {
            //InitializeComponent();
            BindingContext = ViewModel = new e1_EingangInputDataViewModel(enumStoreInArt.NotSet);
            Init();
        }

        private void Init()
        {
            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.LoggedUser = ((App)Application.Current).LoggedUser.Copy();
                ViewModel.WizardData.Teststring += "e1_EingangInputDataPage " + Environment.NewLine;

                ViewModel.WizardData.Wiz_StoreIn = new wizStoreIn();
                ViewModel.WizardData.Wiz_StoreIn.StoreInArt = ViewModel.StoreInArt;

                ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
            }
            ViewModel.LoadValues = true;
            InitToolMenu();
        }

        private void InitToolMenu()
        {
            this.ToolbarItems.Clear();
            List<Xamarin.Forms.ToolbarItem> tmpList = ctr_MenuToolBarItem.CreateMenuStoreOut(false, false, false, false);
            foreach (var item in tmpList)
            {
                item.Clicked += toolBarItem_Clicked;
                this.ToolbarItems.Add(item);
            }
        }

        private async void toolBarItem_Clicked(object sender, EventArgs e)
        {
            ViewModel.WizardData.Wiz_StoreIn.SelectedEingang = ViewModel.SelectedEingang.Copy();
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
                case "4":
                    //await Navigation.PushAsync(new c1_CallArticleListPage());
                    break;
                case "5":
                    //await Navigation.PushAsync(new c2_w0_wizHost());
                    break;
                case "6":
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
    }
}