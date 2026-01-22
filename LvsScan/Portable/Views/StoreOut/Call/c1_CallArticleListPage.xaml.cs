using Common.Models;
using LvsScan.Portable.Controls;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels.StoreOut.Call;
using LvsScan.Portable.Views.Menu;
using LvsScan.Portable.Views.StoreOut.Open;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.StoreOut.Call
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class c1_CallArticleListPage : ContentPage
    {
        public c1_CallArticleListViewModel ViewModel;
        public c1_CallArticleListPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new c1_CallArticleListViewModel();
            ViewModel.Title = "CallArticleListPage";

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.LoggedUser = ((App)Application.Current).LoggedUser.Copy();
                ViewModel.WizardData.Teststring += "CallArticleListPage " + Environment.NewLine;

                ViewModel.WizardData.Wiz_StoreOut = new wizStoreOut();
                ViewModel.WizardData.Wiz_StoreOut.StoreOutArt = ViewModel.StoreOutArt;

                ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
            }

            //ViewModel.LoadValues = true;

            //-- test mr 20250709
            // Asynchrones Laden der Liste
            Task.Run(async () => await LoadDataAsync());

            InitToolMenu();
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
                await ViewModel.GetCallList();
            }
            finally
            {
                ViewModel.IsBusy = false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitToolMenu()
        {
            //this.ToolbarItems.Clear();
            //List<Xamarin.Forms.ToolbarItem> tmpList = ctr_MenuToolBarItem.CreateMenuStoreOut(ViewModel.IsStoreOutStartVisible
            //                                                                                 , ViewModel.IsArticleScanStartVisible
            //                                                                                 , ViewModel.IsCreateAusgangVisible
            //                                                                                 , false);
            //foreach (var item in tmpList)
            //{
            //    item.Clicked += toolBarItem_Clicked;
            //    this.ToolbarItems.Add(item);
            //}
            if (ToolbarItems.Count > 0)
            {
                ToolbarItems.Clear();
            }

            var tmpList = ctr_MenuToolBarItem.CreateMenuStoreOut(
                ViewModel.IsStoreOutStartVisible,
                ViewModel.IsArticleScanStartVisible,
                ViewModel.IsCreateAusgangVisible,
                false
            );

            foreach (var item in tmpList)
            {
                if (!ToolbarItems.Contains(item))
                {
                    item.Clicked += toolBarItem_Clicked;
                    ToolbarItems.Add(item);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void toolBarItem_Clicked(object sender, EventArgs e)
        {
            ViewModel.WizardData.Wiz_StoreOut.CallsList = ViewModel.SelectedCallsToAusgang;
            ViewModel.WizardData.Wiz_StoreOut.StoreOutArt = ViewModel.StoreOutArt;

            ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();

            Xamarin.Forms.ToolbarItem tmpTbi = (Xamarin.Forms.ToolbarItem)sender;
            switch (tmpTbi.AutomationId)
            {
                //---------------------------------------------------------------------- Start/Menü Home
                case "1":
                    Application.Current.MainPage = new FlyoutMenuPage();
                    break;
                //---------------------------------------------------------------------- StoreOut
                case "2":
                    await Navigation.PushAsync(new SubMenuStoreOutPage());
                    break;

                //---------------------------------------------------------------------- Refresh
                case "3":
                    //ViewModel.LoadValues = true;
                    await LoadDataAsync();
                    break;
                //---------------------------------------------------------------------- Call
                case "4":
                    //await Navigation.PushAsync(new c1_CallArticleListPage());
                    if (ViewModel.IsBusy) return;
                    ViewModel.IsBusy = true;
                    try
                    {
                        await Navigation.PushAsync(new c1_CallArticleListPage());
                    }
                    finally
                    {
                        ViewModel.IsBusy = false;
                    }
                    break;
                //---------------------------------------------------------------------- Create Ausgang
                case "5":
                    await Navigation.PushAsync(new c2_w0_wizHost());
                    //await Navigation.PushAsync(new c2w1_wizView());
                    break;
                //---------------------------------------------------------------------- Create StoreOut by Call
                case "6":
                    //ViewModel.IsBusy = true;
                    //var result = await ViewModel.CreateStoreOutbyCall(); // Asynchron ausführen
                    //ViewModel.IsBusy = false;

                    //string mesInfo = string.Empty;
                    //string message = string.Empty;

                    ////--- neu
                    //if (!result.Success)
                    //{
                    //    message = "Es ist ein Fehler beim Update der Abrufdaten aufgetreten!" + System.Environment.NewLine;
                    //    mesInfo = "ACHTUNG";
                    //    await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
                    //}
                    //else
                    //{
                    //    message = result.Info;
                    //    mesInfo = "ACHTUNG";
                    //    await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");


                    //    ////--- backup in WizardData
                    //    ViewModel.WizardData.Wiz_StoreOut.CallsList = new List<Calls>(); // ViewModel.ListCallsAll.ToList();
                    //    ViewModel.WizardData.Wiz_StoreOut.StoreOutArt = Common.Enumerations.enumStoreOutArt.open;
                    //    ViewModel.WizardData.Wiz_StoreOut.SelectedAusgang = result.CreatedAusgang.Copy();
                    //    ViewModel.WizardData.Wiz_StoreOut.WorkingInProcess = true;
                    //    ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();

                    //    //await Navigation.PushAsync(new oa1_OpenStoreOutListPage());
                    //    await Navigation.PushAsync(new oa2_ArticleListPage());
                    //}

                    await HandleStoreOutCreation();
                    break;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task HandleStoreOutCreation()
        {
            ViewModel.IsBusy = true;
            var result = await ViewModel.CreateStoreOutbyCall();
            ViewModel.IsBusy = false;

            string mesInfo = "ACHTUNG";
            string message;

            if (!result.Success)
            {
                message = "Es ist ein Fehler beim Update der Abrufdaten aufgetreten!" + Environment.NewLine;
                await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
            }
            else
            {
                message = result.Info;
                await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");

                ViewModel.WizardData.Wiz_StoreOut = new wizStoreOut
                {
                    CallsList = new List<Calls>(),
                    StoreOutArt = Common.Enumerations.enumStoreOutArt.open,
                    SelectedAusgang = result.CreatedAusgang.Copy(),
                    WorkingInProcess = true
                };

                if (Application.Current is App app)
                {
                    app.WizardData = ViewModel.WizardData.Copy();
                }

                await Navigation.PushAsync(new oa2_ArticleListPage());
            }
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (sender is StackLayout stackLayout && int.TryParse(stackLayout.AutomationId, out int iTmp))
            {
                var selectedCall = ViewModel.ListCallSelectionSource.FirstOrDefault(x => x.Id == iTmp);

                if (ViewModel.SelectedCall.Id == 0 && selectedCall != null)
                {
                    ViewModel.SelectedCall = selectedCall.Copy();
                    viewItemOffen.SelectedItems.Clear();
                    viewItemOffen.SelectedItems.Add(selectedCall);
                }
                else
                {
                    HandleSelectionChange(selectedCall);
                }

                UpdateSelectedCallsToAusgang();
                InitToolMenu();
            }

            //if ((sender as StackLayout).AutomationId != null)
            //{
            //    int iTmp = 0;
            //    string strTmp = (sender as StackLayout).AutomationId.ToString();
            //    if (int.TryParse(strTmp, out iTmp))
            //    {
            //        //Check Call exist Selected list
            //        Calls selectedCall = ViewModel.ListCallSelectionSource.FirstOrDefault(x => x.Id == iTmp);

            //        if ((ViewModel.SelectedCall.Id == 0) && (selectedCall != null))
            //        {
            //            ViewModel.SelectedCall = selectedCall.Copy();
            //            viewItemOffen.SelectedItems.Clear();
            //            viewItemOffen.SelectedItems.Add(selectedCall);
            //        }
            //        else
            //        {
            //            if (viewItemOffen.SelectedItems.Contains(selectedCall))
            //            {
            //                viewItemOffen.SelectedItems.Remove(selectedCall);
            //                if (viewItemOffen.SelectedItems.Count == 0)
            //                {
            //                    ViewModel.SelectedCall = new Calls();
            //                }
            //                else
            //                {
            //                    ViewModel.GetSubLists();
            //                }
            //            }
            //            else
            //            {
            //                if (ViewModel.SelectedCall.Workspace.MaxCountArticleInStoreOut == 0)
            //                {
            //                    //keine Begrenzung der Anzahl
            //                    viewItemOffen.SelectedItems.Add(selectedCall);
            //                }
            //                else
            //                {
            //                    if (viewItemOffen.SelectedItems.Count < ViewModel.SelectedCall.Workspace.MaxCountArticleInStoreOut)
            //                    {
            //                        viewItemOffen.SelectedItems.Add(selectedCall);
            //                    }
            //                    else
            //                    {
            //                        string mesInfo = "ACHTUNG";
            //                        string message = "Die max. Artikelanzahl pro Ausgang ist erreicht! Erstellen Sie den Ausgang.";
            //                        App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
            //                    }
            //                }
            //            }
            //        }

            //        if (viewItemOffen.SelectedItems.Count == 0)
            //        {
            //            ViewModel.SelectedCallsToAusgang.Clear();
            //        }
            //        else
            //        {
            //            ViewModel.SelectedCallsToAusgang.Clear(); // Problem: Clear hinzugefügt, um Duplikate zu vermeiden
            //            //foreach (Calls c in viewItemOffen.SelectedItems)
            //            //{
            //            //    ViewModel.SelectedCallsToAusgang.Add(c);
            //            //}
            //            //ViewModel.SelectedCallsToAusgang = new List<Calls>(viewItemOffen.SelectedItems.Cast<Calls>());
            //            ViewModel.SelectedCallsToAusgang = viewItemOffen.SelectedItems
            //                                                                        .Cast<Calls>()
            //                                                                        .Distinct()
            //                                                                        .ToList();

            //        }
            //        InitToolMenu();
            //    }
            //}
            //else
            //{

            //}
        }
        private void HandleSelectionChange(Calls selectedCall)
        {
            if (viewItemOffen.SelectedItems.Contains(selectedCall))
            {
                viewItemOffen.SelectedItems.Remove(selectedCall);
                if (viewItemOffen.SelectedItems.Count == 0)
                {
                    ViewModel.SelectedCall = new Calls();
                }
                else
                {
                    ViewModel.GetSubLists();
                }
            }
            else if (ViewModel.SelectedCall.Workspace.MaxCountArticleInStoreOut == 0 ||
                     viewItemOffen.SelectedItems.Count < ViewModel.SelectedCall.Workspace.MaxCountArticleInStoreOut)
            {
                viewItemOffen.SelectedItems.Add(selectedCall);
            }
            else
            {
                string mesInfo = "ACHTUNG";
                string message = "Die max. Artikelanzahl pro Ausgang ist erreicht! Erstellen Sie den Ausgang.";
                App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
            }
        }

        private void UpdateSelectedCallsToAusgang()
        {
            ViewModel.SelectedCallsToAusgang = viewItemOffen.SelectedItems
                .Cast<Calls>()
                .Distinct()
                .ToList();
        }
        private void viewItemOffen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}