using Common.Enumerations;
using LvsScan.Portable.Helper;
using LvsScan.Portable.Interfaces;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels;
using LvsScan.Portable.ViewModels.Wizard;
//using Plugin.Media.Abstractions;
//using Plugin.Media;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.KeyboardHelper;

namespace LvsScan.Portable.Views.Wizard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class wiz_TakePhoto : ContentView, IWizardView
    {
        public wiz_TakePhotoViewModel ViewModel;
        public wiz_TakePhoto(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            initViewModel(currentViewModel);
        }
        public wiz_TakePhoto(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            initViewModel(currentViewModel);
        }

        private void initViewModel(BaseViewModel currentViewModel)
        {
            this.BindingContext = ViewModel = currentViewModel as wiz_TakePhotoViewModel;

            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;
            // set inactiv
            //ViewModel.IsBaseNextEnabeld = true;
            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.Teststring += "wiz_TakePhoto " + System.Environment.NewLine;
                ViewModel.AppProcess = ViewModel.WizardData.AppProcess;

                switch (ViewModel.AppProcess)
                {
                    case enumAppProcess.StoreIn:
                        //ViewModel.SelectedArticle = ViewModel.WizardData.Wiz_StoreIn.WizDamage.Article.Copy();
                        ViewModel.StoreInArt = ViewModel.WizardData.Wiz_StoreIn.StoreInArt;
                        ViewModel.CurrentStepStoreInArt = enumStoreInArt_Steps.wizStepInputPhoto;
                        ViewModel.SelectedArticle = ViewModel.WizardData.Wiz_StoreIn.ArticleToCheck.Copy();
                        ViewModel.SelectedEingang = ViewModel.WizardData.Wiz_StoreIn.SelectedEingang.Copy();
                        break;

                    case enumAppProcess.StoreOut:
                        ViewModel.StoreOutArt = ViewModel.WizardData.Wiz_StoreOut.StoreOutArt;
                        ViewModel.CurrentStepStoreOutArt = enumStoreOutArt_Steps.wizStepInputPhoto;
                        ViewModel.SelectedArticle = ViewModel.WizardData.Wiz_StoreOut.ArticleToCheck.Copy();
                        ViewModel.SelectedAusgang = ViewModel.WizardData.Wiz_StoreOut.SelectedAusgang.Copy();
                        break;

                    case enumAppProcess.StoreLocationChange:
                        break;
                    default:
                        break;
                }
            }

        }


        Task<bool> IWizardView.OnPrevious(BaseViewModel viewModel)
        {
            string str = string.Empty;
            //throw new NotImplementedException();

            return Task.FromResult(true);
        }

        Task IWizardView.OnAppearing()
        {
            string str = string.Empty;

            //throw new NotImplementedException();
            return Task.CompletedTask;
        }

        Task IWizardView.OnDissapearing()
        {
            string str = string.Empty;
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }


        Task<bool> IWizardView.OnNext(BaseViewModel viewModel)
        {
            string message = string.Empty;
            string mesInfo = string.Empty;

            switch (ViewModel.AppProcess)
            {
                case enumAppProcess.StoreIn:
                    ViewModel.WizardData.Wiz_StoreIn.SelectedEingang = ViewModel.SelectedEingang.Copy();
                    ViewModel.WizardData.Wiz_StoreIn.ArticleToCheck = ViewModel.SelectedArticle.Copy();
                    break;
                case enumAppProcess.StoreOut:
                    ViewModel.WizardData.Wiz_StoreOut.SelectedAusgang = ViewModel.SelectedAusgang.Copy();
                    //ViewModel.WizardData.Wiz_StoreIn.ArticleToCheck = ViewModel.SelectedArticle.Copy();
                    break;

                case enumAppProcess.StoreLocationChange:
                    break;
                default:
                    break;
            }
            ViewModel.WizardData.AppProcess = ViewModel.AppProcess;
            ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
            return Task.FromResult(true); // return true;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnCamera_Clicked(object sender, EventArgs e)
        {
            try
            {
                var status = await Permissions.RequestAsync<Permissions.Camera>();
                if (status == PermissionStatus.Granted)
                {
                    var photoStream = await CapturePhotoAsync();
                    if (photoStream != null)
                    {
                        // Byte-Array für die Speicherung vorbereiten
                        ViewModel.ByteArrayToSave = helper_XamImage.XamImageToByteArray(photoStream);
                        // Beispiel: Foto anzeigen
                        if (
                             (ViewModel.ByteArrayToSave != null) && (ViewModel.ByteArrayToSave.Length > 0)
                           )
                        {
                            imgCamera.Source = ImageSource.FromStream(() => new MemoryStream(ViewModel.ByteArrayToSave));
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Info", "Kein Foto aufgenommen.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                string mesInfo = "Achtung";
                string message = "Est ist ein Fehler aufgetreten. Es konnte kein Foto übernommen werden!" + Environment.NewLine;
                message += "Est ist ein Fehler aufgetreten. Es konnte kein Foto übernommen werden!" + Environment.NewLine;
                await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task<Stream> CapturePhotoAsync()
        {
            try
            {
                // Kamera-Berechtigung anfordern
                //var status = await Permissions.RequestAsync<Permissions.Camera>();
                //if (status != PermissionStatus.Granted)
                //{
                //    await App.Current.MainPage.DisplayAlert(
                //        "Kamera-Berechtigung erforderlich",
                //        "Bitte aktivieren Sie die Kamera-Berechtigung in den App-Einstellungen.",
                //        "OK"
                //    );
                //    AppInfo.ShowSettingsUI(); // Öffnet die App-Einstellungen
                //    return null;
                //}
                // Foto aufnehmen
                //var photo = await MediaPicker.CapturePhotoAsync();
                //if (photo != null)
                //{
                //    // Stream des aufgenommenen Fotos zurückgeben
                //    return await photo.OpenReadAsync();
                //}
                var status = await Permissions.RequestAsync<Permissions.Camera>();
                if (status == PermissionStatus.Granted)
                {
                    // Foto aufnehmen
                    var photo = await MediaPicker.CapturePhotoAsync();
                    if (photo != null)
                    {
                        // Stream des aufgenommenen Fotos zurückgeben
                        return await photo.OpenReadAsync();
                    }
                }

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Fehler",
                    "Es konnte kein Foto aufgenommen werden: " + ex.Message,
                    "OK"
                );
            }

            return null; // Rückgabe null, falls kein Foto aufgenommen wurde
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSavePhoto_Clicked(object sender, EventArgs e)
        {
            if (this.imgCamera.Source != null)
            {
                var IsSaved = await ViewModel.SavePhoto();
                if (IsSaved)
                {
                    this.imgCamera.Source = ViewModel.ImgSourceSaveOK;
                    //await App.Current.MainPage.DisplayAlert("Erfolg", "Das Foto wurde erfolgreich gespeichert.", "OK");
                }
                else
                {
                    this.imgCamera.Source = ViewModel.ImgSourceSaveFailure;
                    await App.Current.MainPage.DisplayAlert("Fehler", "Das Foto konnte nicht gespeichert werden.", "OK");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Achtung", "Es wurde kein Foto hinterlegt!", "OK");
            }
        }
    }
}