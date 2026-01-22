using Common.Enumerations;
using Common.Helper;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels.StoreIn;
using LvsScan.Portable.Views.Menu;
using LvsScan.Portable.Views.StoreIn.Edi;
using LvsScan.Portable.Views.StoreIn.Open;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.StoreIn
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubMenuStoreInPage : ContentPage
    {
        public SubStoreInScannerViewModel ViewModel;
        public SubMenuStoreInPage()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new SubStoreInScannerViewModel();

            //--- mr Test
            //ReturnCommand = new Command(OnReturnPressed);

            try
            {
                if (((App)Application.Current).WizardData is WizardData)
                {
                    ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                    if (ViewModel.WizardData is WizardData)
                    {
                        if (ViewModel.WizardData.Wiz_StoreIn is wizStoreIn)
                        { }
                        else
                        {
                            ViewModel.WizardData.Wiz_StoreIn = new wizStoreIn();
                        }
                        ViewModel.WizardData.LoggedUser = ((App)Application.Current).LoggedUser.Copy();
                        ViewModel.WizardData.Teststring += "SubMenuStoreInPage " + Environment.NewLine;
                        ViewModel.WizardData.Wiz_StoreIn.StoreInArt = ViewModel.StoreInArt;
                        ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            ViewModel.InitMenu();
            tabView.SelectedItem = tabView.Items[0];
        }

        private async void tabView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if ((e != null) && (e.PropertyName.Equals("SelectedItem")))
            {
                if (ViewModel != null)
                {
                    string str = string.Empty;
                    if (ViewModel.IsManual)
                    {
                        await Task.Delay(100);
                        await Task.Run(() =>
                        {
                            if (ViewModel.ExistProductionNo)
                            {
                                searchProductionNoManual.Focus();
                                searchProductionNo.Unfocus();
                            }
                            else
                            {
                                searchProductionNoManual.Unfocus();
                                searchProductionNo.Unfocus();
                            }
                        });
                    }
                    else
                    {
                        await Task.Delay(100);
                        await Task.Run(() =>
                        {
                            if (ViewModel.ExistProductionNo)
                            {
                                searchProductionNoManual.Unfocus();
                                searchProductionNo.Unfocus();
                            }
                            else
                            {
                                searchProductionNo.Focus();
                                searchProductionNoManual.Unfocus();
                            }
                        });
                    }
                }
            }

        }

        private void Home_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new FlyoutMenuPage();
        }
        private async void viewListMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string str = string.Empty;

            var item = e.CurrentSelection[0] as MenuSubItem;
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
                    str = ex.Message.ToString();
                }
            }
            else
            {
                string message = "Es ist keine Zielseite hinterlegt!" + Environment.NewLine;
                string mesInfo = "FEHLER";
                await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
            }
        }

        private void btnClearProductionNo_Clicked(object sender, EventArgs e)
        {
            ViewModel.SearchProductionNo = string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSearchProductionNo_Clicked(object sender, EventArgs e)
        {
            string s = string.Empty;

            if ((ViewModel.SearchProductionNo != null) && (ViewModel.SearchProductionNo.Length > 4))
            {
                ViewModel.IsBusy = true;
                ViewModel.WizardData.Wiz_StoreIn.IsSearchProcess = true;

                //CHecke Produktionsnummer in offnen Eingängen enthalten
                var resultOPEN = await ViewModel.GetArticleInEingangByProductionNo();
                if (resultOPEN)
                {
                    ViewModel.WizardData.Wiz_StoreIn.ArticlesToCheck = ViewModel.ListArticleSearch.ToList();
                    ViewModel.WizardData.Wiz_StoreIn.SelectedEingang = ViewModel.SearchEingang.Copy();
                    ViewModel.WizardData.Wiz_StoreIn.StoreInArt = enumStoreInArt.open;
                    ViewModel.WizardData.AppProcess = ViewModel.AppProcess;
                    ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();

                    ViewModel.IsBusy = false;
                    if (ViewModel.SearchEingang.Id > 0)
                    {
                        await Navigation.PushAsync(new oe2_ArticleListPage());
                    }
                    else
                    {
                        await Navigation.PushAsync(new oe_SelectFromSearchedResult());
                    }
                }
                else
                {
                    //var resultEdi = ViewModel.CheckAsnArticleListForProductionNo();
                    await ViewModel.GetAsnLfsAndArticleListByProductionNo();
                    bool resultEdi = (ViewModel.AsnArticleList.Count > 0);

                    ViewModel.IsBusy = false;
                    if (resultEdi)
                    {
                        ViewModel.WizardData.AppProcess = ViewModel.AppProcess;
                        ViewModel.WizardData.Wiz_StoreIn.StoreInArt = enumStoreInArt.edi;
                        ViewModel.WizardData.Wiz_StoreIn.AsnArticleList = ViewModel.AsnArticleList;
                        ViewModel.WizardData.Wiz_StoreIn.AsnLfsList = ViewModel.AsnLfsList;

                        ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
                        //await Navigation.PushAsync(new ee1_w0_wizHost());
                        await Navigation.PushAsync(new ee1_EdiLfsArticleList());
                    }
                    else
                    {
                        if ((!resultOPEN) && (!resultEdi))
                        {
                            string mesInfo = "ACHTUNG";
                            string message = "Der gesuchte Artikel ist in den offenen Eingängen und in den EDI Meldungen nicht enthalten!";
                            await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");

                            //await Navigation.PushAsync(new me_wEingang_wizHost());
                        }
                    }
                }
            }
            else
            {
                string mesInfo = "ACHTUNG";
                string message = string.Empty;
                if ((ViewModel.SearchProductionNo != null) && (ViewModel.SearchProductionNo.Length == 0))
                {
                    mesInfo = "ACHTUNG";
                    message = "Es wurde kein Suchwert eingegeben!";
                    await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
                }
                else if ((ViewModel.SearchProductionNo != null) && (ViewModel.SearchProductionNo.Length < 5))
                {
                    mesInfo = "ACHTUNG";
                    message = "Der Suchwert muss mindestens 5 Zeichen enthalten, damit eine Suche durchgeführt werden kann!";
                    await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
                }
            }
            //ViewModel.InitMenu();
        }

        private async void btnLoadEdi_Clicked(object sender, EventArgs e)
        {
            ViewModel.WizardData.Wiz_StoreIn.IsSearchProcess = false;
            await ViewModel.GetAsnLfsAndArticleList();
            ViewModel.WizardData.AppProcess = ViewModel.AppProcess;
            ViewModel.WizardData.Wiz_StoreIn.StoreInArt = enumStoreInArt.edi;
            ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
        }

        private void btnSearchProductionNoDeleteFirtsChar_Clicked(object sender, EventArgs e)
        {
            ViewModel.SearchProductionNo = StringValueEdit.RemoveFirtsCharFromValue(ViewModel.SearchProductionNo);
        }

        private void btnSearchProductionNoDeleteLastChar_Clicked(object sender, EventArgs e)
        {
            ViewModel.SearchProductionNo = StringValueEdit.RemoveLastCharFromValue(ViewModel.SearchProductionNo);
        }

        private void searchProductionNoManual_Completed(object sender, EventArgs e)
        {
            try
            {
                string str = string.Empty;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        private void OnEntryCompleted(object sender, EventArgs e)
        {
            //try
            //{
            //    // Null-Prüfung für den Sender
            //    if (sender == null)
            //    {
            //        DisplayAlert("Fehler", "Der Sender ist null.", "OK");
            //        return;
            //    }

            //    // Cast des Senders in ein Entry-Objekt
            //    var entry = sender as Entry;
            //    if (entry == null)
            //    {
            //        DisplayAlert("Fehler", "Der Sender ist kein gültiges Entry-Objekt.", "OK");
            //        return;
            //    }

            //    // Null- oder Leer-Prüfung für den Text
            //    string input = entry.Text;
            //    if (string.IsNullOrEmpty(input))
            //    {
            //        DisplayAlert("Fehler", "Eingabe war leer oder ungültig.", "OK");
            //        return;
            //    }

            //    // Erfolgreiche Verarbeitung
            //    DisplayAlert("Eingabe", $"Du hast eingegeben: {input}", "OK");
            //}
            //catch (Exception ex)
            //{
            //    DisplayAlert("Fehler", $"Ein unerwarteter Fehler ist aufgetreten: {ex.Message}", "OK");
            //}
        }


    }
}