using Common.Views;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels.StoreIn.Edi;
using LvsScan.Portable.Views.Menu;
using System;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.StoreIn.Edi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ee1_EdiLfsArticleList : ContentPage
    {
        public ee1_EdiLfsArticleListViewModel ViewModel { get; set; }
        public ee1_EdiLfsArticleList()
        {
            InitializeComponent();
            BindingContext = ViewModel = new ee1_EdiLfsArticleListViewModel();

            //this.ViewModel.IsBusy = true;
            this.ViewModel.IsBusy = false;
            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.AppProcess = ViewModel.AppProcess;
                ViewModel.WizardData.LoggedUser = ((App)Application.Current).LoggedUser.Copy();
                ViewModel.WizardData.Teststring += "ee1_EdiLfsArticleList " + Environment.NewLine;

                ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
                if (ViewModel.WizardData.Wiz_StoreIn.IsSearchProcess)
                {
                    //ViewModel.ListAsnArticleViews = new ObservableCollection<AsnArticleView>(ViewModel.WizardData.Wiz_StoreIn.AsnArticleList.OrderBy(x => x.Produktionsnummer).ToList());
                    ViewModel.ListAsnArticleViews = new ObservableCollection<AsnArticleView>(ViewModel.WizardData.Wiz_StoreIn.AsnArticleList.OrderByDescending(x => x.IsSearchResult).ToList());
                    if (ViewModel.ListAsnArticleViews.Count > 0)
                    {
                        foreach (var item in ViewModel.ListAsnArticleViews)
                        {
                            if (item.IsSearchResult)
                            {
                                ViewModel.SelectedArticleView = item;
                            }
                        }
                    }
                    ViewModel.ListAsnLfsViews = new ObservableCollection<AsnLfsView>(ViewModel.WizardData.Wiz_StoreIn.AsnLfsList.ToList());
                }
                else
                {
                    TimeSpan timeSpan = DateTime.Now - ((App)Application.Current).WizardData.SaveAsnListTimeStamp;
                    if ((timeSpan.Days < 1) && (timeSpan.Hours < 4))
                    {
                        ViewModel.ListAsnArticleViews = new ObservableCollection<AsnArticleView>(((App)Application.Current).WizardData.SaveAsnArticleList.ToList());
                        ViewModel.ListAsnLfsViews = new ObservableCollection<AsnLfsView>(((App)Application.Current).WizardData.SaveAsnLfsList.ToList());
                    }
                    else
                    {
                        this.ViewModel.IsBusy = false;
                        ViewModel.LoadValues = true;
                    }
                }
            }
            tabViewEdi.SelectedItem = tabViewEdi.Items[0];
            //ViewModel.LoadValues = true;
            if (this.ViewModel.IsBusy == true)
            {
                this.ViewModel.IsBusy = false;
            }
            //InitToolMenu();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void InitToolMenu()
        {
            string str = string.Empty;

            //this.ToolbarItems.Clear();
            //List<Xamarin.Forms.ToolbarItem> tmpList = new List<Xamarin.Forms.ToolbarItem>();
            //if (this.ViewModel != null)
            //{
            //    tmpList = ctr_MenuToolBarItem.CreateMenuStoreIn(false, ViewModel.IsProcessStartVisible, false);
            //}
            //else 
            //{
            //    tmpList = ctr_MenuToolBarItem.CreateMenuStoreIn(false, false, false);
            //}            

            ////
            //foreach (var item in tmpList)
            //{
            //    item.Clicked += toolBarItem_Clicked;
            //    this.ToolbarItems.Add(item);
            //}
        }
        private async void toolBarItem_Clicked(object sender, EventArgs e)
        {
            //ViewModel.WizardData.SaveAsnLfsList = new ObservableCollection<AsnLfsView>(ViewModel.ListAsnLfsViews.ToList());
            //ViewModel.WizardData.SaveAsnArticleList = new ObservableCollection<AsnArticleView>(ViewModel.ListAsnArticleViews.ToList());

            //ViewModel.WizardData.Wiz_StoreIn.AsnLfsList = new ObservableCollection<AsnLfsView>(ViewModel.ListAsnLfsViews.ToList());
            //ViewModel.WizardData.Wiz_StoreIn.AsnArticleList = new ObservableCollection<AsnArticleView>(ViewModel.ListAsnArticleViews.ToList());
            //ViewModel.WizardData.Wiz_StoreIn.SelectedArticleView = ViewModel.SelectedArticleView;
            ////ViewModel.WizardData.Wiz_StoreIn.SelectedLfsView = ViewModel.SelectedLfsView;

            //((App)Application.Current).WizardData = ViewModel.WizardData.Copy();

            //Xamarin.Forms.ToolbarItem tmpTbi = (Xamarin.Forms.ToolbarItem)sender;
            //switch (tmpTbi.AutomationId)
            //{
            //    //Home
            //    case "1":
            //        Application.Current.MainPage = new FlyoutMenuPage();
            //        break;
            //    //Submenu Auslagerung / Einlagerung
            //    case "2":
            //        await Navigation.PushAsync(new SubMenuStoreInPage());
            //        break;
            //    // Refresh
            //    case "3":
            //        await Navigation.PushAsync(new ee1_EdiLfsArticleList());
            //        break;
            //    // Scan Artikel
            //    case "4":
            //        //await Navigation.PushAsync(new ee1_w0_wizHost());
            //        break;
            //    // Start Process
            //    case "5":
            //        await Navigation.PushAsync(new ee1_w0_wizHost());
            //        //await Navigation.PushAsync(new c2_w0_wizHost());
            //        break;
            //    case "6":
            //        //ViewModel.IsBusy = true;
            //        //Task.Run(() => Task.Delay(100)).Wait();
            //        //var result = Task.Run(() => ViewModel.CreateStoreOutbyCall()).Result;
            //        //ViewModel.IsBusy = false;

            //        //string mesInfo = string.Empty;
            //        //string message = string.Empty;
            //        //if (result.Success)
            //        //{
            //        //    mesInfo = "INFORMATION";
            //        //    message = result.Info;
            //        //}
            //        //else
            //        //{
            //        //    mesInfo = "FEHLER";
            //        //    message = result.Error;
            //        //}
            //        //await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");

            //        //await Navigation.PushAsync(new c1_CallArticleListPage());
            //        break;
            //}
        }

        private async void Refresh_Clicked(object sender, EventArgs e)
        {
            //ViewModel.LoadValues = true;
            await Navigation.PushAsync(new ee1_EdiLfsArticleList());
        }
        private void Home_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new FlyoutMenuPage();
        }
        private async void AsnCheck_Clicked(object sender, EventArgs e)
        {
            if ((this.ViewModel.SelectedArticleView is AsnArticleView) && (this.ViewModel.SelectedArticleView.AsnId > 0))
            {
                ViewModel.WizardData.SaveAsnLfsList = new ObservableCollection<AsnLfsView>(ViewModel.ListAsnLfsViews.ToList());
                ViewModel.WizardData.SaveAsnArticleList = new ObservableCollection<AsnArticleView>(ViewModel.ListAsnArticleViews.ToList());

                ViewModel.WizardData.Wiz_StoreIn.AsnLfsList = new ObservableCollection<AsnLfsView>(ViewModel.ListAsnLfsViews.ToList());
                ViewModel.WizardData.Wiz_StoreIn.AsnArticleList = new ObservableCollection<AsnArticleView>(ViewModel.ListAsnArticleViews.ToList());
                ViewModel.WizardData.Wiz_StoreIn.SelectedArticleView = ViewModel.SelectedArticleView;
                //ViewModel.WizardData.Wiz_StoreIn.SelectedLfsView = ViewModel.SelectedLfsView;

                ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
                await Navigation.PushAsync(new ee1_w0_wizHost());
            }
            else
            {
                string mesInfo = "ACHTUNG";
                string message = "Es ist kein Artikeldatensatz ausgewählt! Wählen Sie den gewünschten Artikelsatz zur Bearbeitung aus.";
                await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
            }
        }


        private void tgrArticleItem_Tapped(object sender, EventArgs e)
        {
            if (ViewModel.ListAsnArticleViews.Count > 0)
            {
                int iTmp = 0;
                if ((sender as StackLayout).AutomationId != null)
                {
                    string strTmp = (sender as StackLayout).AutomationId.ToString();
                    if (int.TryParse(strTmp, out iTmp))
                    {
                        AsnArticleView tmpSelected = ViewModel.ListAsnArticleViews.FirstOrDefault(x => x.LfdNr == iTmp);
                        if (tmpSelected != null)
                        {
                            if (ViewModel.SelectedArticleView is null)
                            {
                                viewItemArticle.SelectedItem = tmpSelected;
                            }
                            else
                            {
                                if (ViewModel.SelectedArticleView.LfdNr.Equals(tmpSelected.LfdNr))
                                {
                                    viewItemArticle.SelectedItem = null;
                                }
                                else
                                {
                                    viewItemArticle.SelectedItem = tmpSelected;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void tgrLfsItem_Tapped(object sender, EventArgs e)
        {
            //if (ViewModel.ListAsnLfsViews.Count > 0)
            //{
            //    int iTmp = 0;
            //    if ((sender as StackLayout).AutomationId != null)
            //    {
            //        string strTmp = (sender as StackLayout).AutomationId.ToString();
            //        if (int.TryParse(strTmp, out iTmp))
            //        {
            //            AsnLfsView tmpSelected = ViewModel.ListAsnLfsViews.FirstOrDefault(x => x.LfdNr == iTmp);
            //            if (tmpSelected != null)
            //            {
            //                if (ViewModel.SelectedLfsView is null)
            //                {
            //                    viewItemLfs.SelectedItem = tmpSelected;
            //                }
            //                else
            //                {
            //                    if (ViewModel.SelectedLfsView.LfdNr.Equals(tmpSelected.LfdNr))
            //                    {
            //                        viewItemLfs.SelectedItem = null;
            //                    }
            //                    else
            //                    {
            //                        viewItemLfs.SelectedItem = tmpSelected;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
        }
        private async void Submenu_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SubMenuStoreInPage());
        }

        private void viewItemArticle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string str = string.Empty;
            InitToolMenu();
        }
    }
}