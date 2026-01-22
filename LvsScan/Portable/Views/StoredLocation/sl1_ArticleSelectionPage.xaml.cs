using Common.Models;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels.StoredLocation;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.KeyboardHelper;

namespace LvsScan.Portable.Views.StoredLocation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class sl1_ArticleSelectionPage : ContentPage
    {
        public sl1_ArticleSelectionViewModel ViewModel { get; set; }

        public sl1_ArticleSelectionPage()
        {
            try
            {
                InitializeComponent();

                this.Appearing += Page_Appearing;
                this.Disappearing += Page_Disappearing;

                this.BindingContext = ViewModel = new sl1_ArticleSelectionViewModel();

                if (((App)Application.Current).WizardData is WizardData)
                {
                    ((App)Application.Current).WizardData.Wiz_StoreLocationChange = new wizStoreLocationChanged();
                    ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                    ViewModel.WizardData.Teststring += "StoreLocationChange - ArticleSelectionPage " + Environment.NewLine;
                }

                tabView.SelectedItem = tabView.Items[0];
            }
            catch (Exception ex)
            {
                string str = ex.InnerException.Message;
            }
        }
        private void Page_Appearing(object sender, EventArgs e)
        {
            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;
        }

        private void Page_Disappearing(object sender, EventArgs e)
        {
            SoftKeyboard.Current.VisibilityChanged -= Current_VisibilityChanged;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(1000);
            await Task.Run(() =>
            {
                searchLvsNo.Focus();
                //searchProductionsNo.Unfocus();
            });
        }

        private void Current_VisibilityChanged(SoftKeyboardEventArgs e)
        {
            if (e.IsVisible)
            {
                // do your things
                string str = string.Empty;

                //searchLvsNoManual.Focus();
            }
            else
            {
                // do your things
                string str = string.Empty;
                //searchLvsNo.Focus();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSearchArticle_Clicked(object sender, EventArgs e)
        {
            if (ViewModel.ExistLvsNo)
            {
                //await ViewModel.SearchArticle();
                if (
                    (ViewModel.ArticleSearched is Articles) &&
                    (ViewModel.ArticleSearched.Id > 0)
                  )
                {
                    ViewModel.WizardData.Wiz_StoreLocationChange.ArticleSearch = ViewModel.ArticleSearch;
                    ViewModel.WizardData.Wiz_StoreLocationChange.ArticleToChange = ViewModel.ArticleSearched;

                    await Navigation.PushAsync(new sl2_StoreLocationChangePage());
                }
                else
                {
                    await DisplayAlert("FEHLER", "Es konnte kein Artikel ermittelt werden!", "OK");
                    ClearViewModelValues();
                }
            }
            else
            {
                await DisplayAlert("FEHLER", "LVS-Nr muss ausgefüllt sein!", "OK");
                ClearViewModelValues();
            }
        }

        private void ClearViewModelValues()
        {
            ViewModel.ArticleSearch = new ArticleSearch();
            ViewModel.SearchLvsNo = string.Empty;
            //ViewModel.SearchProductionNo = string.Empty;
            ViewModel.ArticleSearched = new Articles();

            ViewModel.WizardData.Wiz_StoreLocationChange.ArticleSearch = ViewModel.ArticleSearch;
            ViewModel.WizardData.Wiz_StoreLocationChange.ArticleToChange = ViewModel.ArticleSearched;
        }

        private void btnClearProductionsNo_Clicked(object sender, EventArgs e)
        {
            //ViewModel.SearchProductionNo = string.Empty;
            //SetFocus();
        }

        private void btnClearLvsNo_Clicked(object sender, EventArgs e)
        {
            ViewModel.SearchLvsNo = string.Empty;
            //ViewModel.SearchProductionNo = String.Empty;           
            //SetFocus();
        }

        private void searchLvsNo_Completed(object sender, EventArgs e)
        {
            //SetFocus();
        }



        private void Button_Clicked(object sender, EventArgs e)
        {
            if (ViewModel.IsManual)
            {
                searchLvsNo.Focus();
            }
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            if (ViewModel.IsManual)
            {
                //searchProductionsNo.Focus();
            }
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            if (ViewModel.IsManual)
            {
                searchLvsNoManual.Focus();
            }
        }

        private void Button_Clicked_3(object sender, EventArgs e)
        {

            if (ViewModel.IsManual)
            {
                //searchProductionsNoManual.Focus();
            }
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
                        await Task.Delay(1000);
                        await Task.Run(() =>
                        {
                            if (ViewModel.ExistLvsNo)
                            {
                                //searchProductionsNoManual.Focus();
                                searchLvsNoManual.Unfocus();
                                searchLvsNo.Unfocus();
                                //searchProductionsNo.Unfocus();
                            }
                            else
                            {
                                searchLvsNoManual.Focus();
                                searchLvsNo.Unfocus();
                                //searchProductionsNo.Unfocus();
                                //searchProductionsNoManual.Unfocus();
                            }
                        });
                    }
                    else
                    {
                        await Task.Delay(1000);
                        await Task.Run(() =>
                        {
                            if (ViewModel.ExistLvsNo)
                            {
                                //searchProductionsNo.Focus();
                                //searchProductionsNoManual.Unfocus();
                                searchLvsNoManual.Unfocus();
                                searchLvsNo.Unfocus();
                            }
                            else
                            {
                                searchLvsNo.Focus();
                                searchLvsNoManual.Unfocus();
                                //searchProductionsNo.Unfocus();
                                //searchProductionsNoManual.Unfocus();
                            }
                        });
                    }
                }
            }
        }
    }
}