using Common.Enumerations;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels.StoreOut;
using LvsScan.Portable.Views.Menu;
using LvsScan.Portable.Views.StoreOut.Call;
using LvsScan.Portable.Views.StoreOut.Open;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.KeyboardHelper;

namespace LvsScan.Portable.Views.StoreOut
{
    [XamlCompilation(XamlCompilationOptions.Compile)]


    public partial class SubMenuStoreOutPage : ContentPage
    {
        /// <summary>
        ///             Submenü StoreOut
        ///             Untermenü Auslagerung / Suche per LVSNR
        /// </summary>

        public SubMenuStoreOutViewModel ViewModel;
        public SubMenuStoreOutPage()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new SubMenuStoreOutViewModel();
            SubMenuItemView.ItemSelected += SubMenuItemView_ItemSelected;
            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;

            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.LoggedUser = ((App)Application.Current).LoggedUser.Copy();
                ViewModel.WizardData.AppProcess = enumAppProcess.StoreOut;

                ViewModel.WizardData.Wiz_StoreOut = new wizStoreOut();
                ViewModel.WizardData.Teststring += "SubMenuStoreOutViewModel " + System.Environment.NewLine;
                ViewModel.AppProcess = ViewModel.WizardData.AppProcess;
                ViewModel.StoreOutArt = ViewModel.WizardData.Wiz_StoreOut.StoreOutArt;
                tabView.SelectedItem = tabItemScan;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
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
        /// <summary>
        ///             Submenü Item 
        ///             - offene Ausgänge
        ///             - Abrufe
        ///             Weiterleitung zur entsprechenden Liste (Ausgänge oder Abrufe)
        ///             
        ///             --- menuSubItem 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SubMenuItemView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MenuSubItem;
            if (item == null)
                return;

            if (item.TargetType != null)
            {
                try
                {
                    var page = (Page)Activator.CreateInstance(item.TargetType);
                    page.Title = item.Title;
                    await Navigation.PushAsync(page);
                }
                catch (Exception ex)
                {
                    string str = ex.Message.ToString();
                }
            }
            else
            {
                string message = "keine Zielseite hinterlegt!" + Environment.NewLine;
                string mesInfo = "FEHLER";
                await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
            }
            SubMenuItemView.SelectedItem = null;
        }
        /// <summary>
        ///             Wird aufgerufen, wenn ein Menüpunkt ausgewählt wird
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void tabView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if ((e != null) && (e.PropertyName.Equals("SelectedItem")))
            {
                if (ViewModel != null)
                {
                    string str = string.Empty;

                    if (ViewModel.IsManual)
                    {
                        //await Task.Delay(1000);
                        await Task.Run(() =>
                        {
                            if (ViewModel.ExistLVSNr)
                            {
                                searchLvsNoManual.Unfocus();
                                searchLvsNo.Unfocus();
                            }
                            else
                            {
                                searchLvsNoManual.Focus();
                                searchLvsNo.Unfocus();
                            }
                        });
                    }
                    else
                    {
                        //await Task.Delay(1000);
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            searchLvsNoManual.Unfocus();
                            searchLvsNo.Unfocus();
                        });
                        await Task.Run(() =>
                        {
                            if (ViewModel.ExistLVSNr)
                            {
                                searchLvsNoManual.Unfocus();
                                searchLvsNo.Unfocus();
                            }
                            else
                            {
                                searchLvsNo.Focus();
                                searchLvsNoManual.Unfocus();
                            }
                        });
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearLVSNo_Clicked(object sender, EventArgs e)
        {
            ViewModel.SearchLvsNo = string.Empty;
        }
        /// <summary>
        ///                 Suche nach der LVSNR in Abrufe oder Ausgänge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>       
        private async void btnSearchLVSNo_Clicked(object sender, EventArgs e)
        {
            string s = string.Empty;

            int iLvsNo = 0;
            int.TryParse(ViewModel.SearchLvsNo, out iLvsNo);
            if (iLvsNo > 0)
            {
                //Abfrage für OPEN Ausgänge
                var resultOPEN = await ViewModel.CheckAusgangByLvs();
                if (resultOPEN.Success)
                {
                    ViewModel.WizardData.Wiz_StoreOut.SelectedAusgang = ViewModel.AusgangToSearch.Copy();
                    ViewModel.WizardData.Wiz_StoreOut.ArticleToCheck = new Common.Models.Articles();
                    ViewModel.WizardData.Wiz_StoreOut.StoreOutArt = enumStoreOutArt.open;
                    ViewModel.WizardData.AppProcess = ViewModel.AppProcess;
                    ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
                    await Navigation.PushAsync(new oa2_ArticleListPage());
                }
                else
                {
                    //da nicht vorhanden Abfrage für CALL
                    var resultCall = await ViewModel.CheckCallByLvs();

                    if (resultCall.Success)
                    {
                        ViewModel.WizardData.Wiz_StoreOut.CallsList = new List<Common.Models.Calls>();
                        ViewModel.WizardData.Wiz_StoreOut.CallsList.Add(ViewModel.CallToSearch);
                        ViewModel.WizardData.Wiz_StoreOut.StoreOutArt = enumStoreOutArt.call;
                        ViewModel.WizardData.AppProcess = ViewModel.AppProcess;
                        ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();

                        await Navigation.PushAsync(new c2_w0_wizHost());
                    }
                    else
                    {
                        if ((!resultOPEN.Success) && (!resultCall.Success))
                        {
                            string mesInfo = "ACHTUNG";
                            string message = "Der gesuchte Artikel ist in den offenen Ausgängen und in den Abrufen nicht enthalten!";
                            await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
                        }
                    }
                }
            }
        }

    }
}