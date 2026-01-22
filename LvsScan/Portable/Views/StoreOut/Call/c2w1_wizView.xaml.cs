using Common.Models;
using LvsScan.Portable.Interfaces;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels;
using LvsScan.Portable.ViewModels.StoreOut.Call;
using LvsScan.Portable.Views.StoreOut.Open;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.KeyboardHelper;

namespace LvsScan.Portable.Views.StoreOut.Call
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class c2w1_wizView : ContentView, IWizardView
    {
        public c2w1_wizViewModel ViewModel;
        public c2w1_wizView(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            initViewModel(currentViewModel);
        }
        public c2w1_wizView(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            initViewModel(currentViewModel);
        }

        private void initViewModel(BaseViewModel currentViewModel)
        {
            this.BindingContext = ViewModel = currentViewModel as c2w1_wizViewModel;

            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;
            // set inactiv
            //ViewModel.IsBaseNextEnabeld = true;
            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.Teststring += "c2w1_wizViewModel " + System.Environment.NewLine;

                //--neu geladen
                //ViewModel.CallArticles = new System.Collections.ObjectModel.ObservableCollection<Calls>(ViewModel.WizardData.Wiz_StoreOut.CallsList);

                //---- Wechsel zu StoreOut Open
                ViewModel.WizardData.Wiz_StoreOut.StoreOutArt = ViewModel.StoreOutArt;
                ViewModel.WizardData.AppProcess = Common.Enumerations.enumAppProcess.StoreOut;
                ViewModel.LoadValues = true;
                ViewModel.CallArticles = new System.Collections.ObjectModel.ObservableCollection<Calls>(ViewModel.WizardData.Wiz_StoreOut.CallsList);
            }
            //tabView.SelectedItem = tabView.Items[0];

            ViewModel.IsBaseNextEnabeld = true;
            if (ViewModel.CallArticles.Count > 0)
            {
                if (ViewModel.CallArticles.Count == 1)
                {
                    ViewModel.SelectedCallArticle = ViewModel.CallArticles.First();
                    ViewModel.IsBaseNextEnabeld = !((ViewModel.SelectedCallArticle is Calls) && (ViewModel.SelectedCallArticle.Id > 0));
                }
            }
        }


        Task<bool> IWizardView.OnPrevious(BaseViewModel viewModel)
        {
            string str = string.Empty;
            //throw new NotImplementedException();
            return Task.FromResult(true);
        }

        async Task IWizardView.OnAppearing()
        {
            string str = string.Empty;
            //// set inactiv
            //ViewModel.IsBaseNextEnabeld = true;
            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;
            await Task.Run(() =>
            {
                // set value empty and set focus
                ViewModel.SearchLvsNo = string.Empty;
                ViewModel.SearchProduktionsnummer = string.Empty;
                //searchLvsNo.Focus();
                //searchProductionsNo.Unfocus();
            });
        }

        Task IWizardView.OnDissapearing()
        {
            SoftKeyboard.Current.VisibilityChanged -= Current_VisibilityChanged;
            string str = string.Empty;
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }


        async Task<bool> IWizardView.OnNext(BaseViewModel viewModel)
        {
            //throw new NotImplementedException();
            string message = string.Empty;
            string mesInfo = string.Empty;
            //save Article data

            //-- set Scan value for update article data
            ViewModel.SelectedCallArticle.ScanCheckForStoreOut = DateTime.Now;
            ViewModel.SelectedCallArticle.ScanUserId = (int)((App)Application.Current).LoggedUser.Id;

            viewModel.IsBusy = true;
            List<Calls> listSelectedCalls = new List<Calls>();
            listSelectedCalls.Add(ViewModel.SelectedCallArticle);
            var result = await ViewModel.CreateStoreOut(listSelectedCalls);
            viewModel.IsBusy = false;

            if (!result.Success)
            {
                message = "Es ist ein Fehler beim Update der Abrufdaten aufgetreten!" + System.Environment.NewLine;
                mesInfo = "ACHTUNG";
                await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
            }
            else
            {
                message = result.Info;
                mesInfo = "ACHTUNG";
                await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");


                ////--- backup in WizardData
                ViewModel.WizardData.Wiz_StoreOut.CallsList = ViewModel.CallArticles.ToList();
                ViewModel.WizardData.Wiz_StoreOut.StoreOutArt = Common.Enumerations.enumStoreOutArt.open;
                ViewModel.WizardData.Wiz_StoreOut.SelectedAusgang = result.CreatedAusgang.Copy();
                ViewModel.WizardData.Wiz_StoreOut.WorkingInProcess = true;
                ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();

                //await Navigation.PushAsync(new oa1_OpenStoreOutListPage());
                await Navigation.PushAsync(new oa2_ArticleListPage());
            }
            //////--- backup in WizardData
            //ViewModel.WizardData.Wiz_StoreOut.CallsList = ViewModel.CallArticles.ToList();
            //ViewModel.WizardData.Wiz_StoreOut.StoreOutArt = ViewModel.StoreOutArt;
            //((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
            return result.Success;
        }
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

        // ------------------------------------------------------------------------------------------------------------------------------------

        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    ViewModel.IsBaseNextEnabeld = (!ViewModel.IsBaseNextEnabeld);
        //}

        //private async void tabView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    if ((e != null) && (e.PropertyName.Equals("SelectedItem")))
        //    {
        //        if (ViewModel != null)
        //        {
        //            string str = string.Empty;

        //            if (ViewModel.IsManual)
        //            {
        //                await Task.Delay(100);
        //                await Task.Run(() =>
        //                {
        //                    if (ViewModel.ExistLVSNr)
        //                    {
        //                        searchProductionsNoManual.Focus();
        //                        searchLvsNoManual.Unfocus();
        //                        searchLvsNo.Unfocus();
        //                        searchProductionsNo.Unfocus();
        //                    }
        //                    else
        //                    {
        //                        searchLvsNoManual.Focus();
        //                        searchLvsNo.Unfocus();
        //                        searchProductionsNo.Unfocus();
        //                        searchProductionsNoManual.Unfocus();
        //                    }
        //                });
        //            }
        //            else
        //            {
        //                await Task.Delay(100);
        //                await Task.Run(() =>
        //                {
        //                    if (ViewModel.ExistLVSNr)
        //                    {
        //                        searchProductionsNo.Focus();
        //                        searchProductionsNoManual.Unfocus();
        //                        searchLvsNoManual.Unfocus();
        //                        searchLvsNo.Unfocus();
        //                    }
        //                    else
        //                    {
        //                        searchLvsNo.Focus();
        //                        searchLvsNoManual.Unfocus();
        //                        searchProductionsNo.Unfocus();
        //                        searchProductionsNoManual.Unfocus();
        //                    }
        //                });
        //            }
        //        }
        //    }
        //}

        private void carouselViewArticle_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            if (e.CurrentItem is Articles)
            {
                ViewModel.SelectedCallArticle = (Calls)e.CurrentItem;
            }
        }

        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {

        }

        private void btnClearLVSNo_Clicked(object sender, EventArgs e)
        {
            ViewModel.SearchLvsNo = string.Empty;
        }

        private void btnClearPrductionNo_Clicked(object sender, EventArgs e)
        {
            ViewModel.SearchProduktionsnummer = string.Empty;
        }

        //private void btnTakeOver_Clicked(object sender, EventArgs e)
        //{
        //    //if ((ViewModel.ExistLVSNr) && (ViewModel.ExistProductionNo))
        //    //{
        //    //    if ((ViewModel.WorkspaceForStoreOut.Id > 0) && (ViewModel.WorkspaceForStoreOut.MaxCountArticleInStoreOut>0))
        //    //    {
        //    //        if (ViewModel.WorkspaceForStoreOut.MaxCountArticleInStoreOut > ViewModel.CallArticlesChecked.Count)
        //    //        {
        //    //            ViewModel.CallArticlesChecked.Add(ViewModel.SelectedCallArticle);
        //    //            ViewModel.CountCheck = ViewModel.CallArticlesChecked.Count();
        //    //            ViewModel.CallArticlesUnChecked.Remove(ViewModel.SelectedCallArticle);
        //    //            ViewModel.SearchLvsNo = string.Empty;
        //    //            ViewModel.SearchProduktionsnummer = string.Empty;
        //    //        }
        //    //        else
        //    //        {
        //    //            string mesInfo = "ACHTUNG";
        //    //            string message = "Die max. Artikelanzahl pro Ausgang ist erreicht! Erstellen Sie den Ausgang.";
        //    //            App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
        //    //        }
        //    //    }
        //    //    else 
        //    //    {
        //    //        ViewModel.CallArticlesChecked.Add(ViewModel.SelectedCallArticle);
        //    //        ViewModel.CountCheck = ViewModel.CallArticlesChecked.Count();
        //    //        ViewModel.CallArticlesUnChecked.Remove(ViewModel.SelectedCallArticle);
        //    //        ViewModel.SearchLvsNo = string.Empty;
        //    //        ViewModel.SearchProduktionsnummer = string.Empty;
        //    //    }
        //    //}
        //}

        /// <summary>
        ///             
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnTakeOverLvs_Clicked(object sender, EventArgs e)
        {
            if ((ViewModel.ExistLVSNr) && (ViewModel.ExistProductionNo))
            {
                if ((ViewModel.WorkspaceForStoreOut.Id > 0) && (ViewModel.WorkspaceForStoreOut.MaxCountArticleInStoreOut > 0))
                {
                    if (ViewModel.WorkspaceForStoreOut.MaxCountArticleInStoreOut > ViewModel.CallArticlesChecked.Count)
                    {
                        if (!ViewModel.CallArticlesChecked.Contains(ViewModel.SelectedCallArticle))
                        {
                            ViewModel.CallArticlesChecked.Add(ViewModel.SelectedCallArticle);
                            ViewModel.CallArticlesUnChecked.Remove(ViewModel.SelectedCallArticle);
                        }
                        ViewModel.SearchLvsNo = string.Empty;
                        ViewModel.SearchProduktionsnummer = string.Empty;
                    }
                    else
                    {
                        string mesInfo = "ACHTUNG";
                        string message = "Die max. Artikelanzahl pro Ausgang ist erreicht! Erstellen Sie den Ausgang.";
                        App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
                    }
                }
                else
                {
                    if (!ViewModel.CallArticlesChecked.Contains(ViewModel.SelectedCallArticle))
                    {
                        ViewModel.CallArticlesChecked.Add(ViewModel.SelectedCallArticle);
                        ViewModel.CallArticlesUnChecked.Remove(ViewModel.SelectedCallArticle);
                    }
                    ViewModel.SearchLvsNo = string.Empty;
                    ViewModel.SearchProduktionsnummer = string.Empty;
                }

                ViewModel.CountCheck = ViewModel.CallArticlesChecked.Count();
                ViewModel.CountUnCheck = ViewModel.CallArticlesUnChecked.Count;
            }
            else
            {
                string mesInfo = "ACHTUNG";
                string message = "LvsNr und Produktionsnummer sind nicht ausgewählt!";
                App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
            }
        }
    }
}