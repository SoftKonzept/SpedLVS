using Common.Enumerations;
using LvsScan.Portable.Interfaces;
using LvsScan.Portable.Models;
using LvsScan.Portable.ViewModels;
using LvsScan.Portable.ViewModels.Wizard;
//using Plugin.Media.Abstractions;
//using Plugin.Media;
using System;
using System.IO;
using System.Threading.Tasks;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.KeyboardHelper;

namespace LvsScan.Portable.Views.Wizard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class wiz_Signature : ContentView, IWizardView
    {
        public wiz_SignatureViewModel ViewModel;
        public wiz_Signature(BaseViewModel currentViewModel, BaseViewModel PreviousViewModel)
        {
            InitializeComponent();
            initViewModel(currentViewModel);
        }
        public wiz_Signature(BaseViewModel currentViewModel)
        {
            InitializeComponent();
            initViewModel(currentViewModel);
        }

        private void initViewModel(BaseViewModel currentViewModel)
        {
            this.BindingContext = ViewModel = currentViewModel as wiz_SignatureViewModel;

            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;
            // set inactiv
            //ViewModel.IsBaseNextEnabeld = true;
            if (((App)Application.Current).WizardData is WizardData)
            {
                ViewModel.WizardData = ((App)Application.Current).WizardData.Copy();
                ViewModel.WizardData.Teststring += "wiz_Signature" + System.Environment.NewLine;

                ViewModel.AppProcess = ViewModel.WizardData.AppProcess;
                ViewModel.StoreOutArt = ViewModel.WizardData.Wiz_StoreOut.StoreOutArt;
                ViewModel.CurrentStepStoreOutArt = enumStoreOutArt_Steps.wizStepInputPhoto;
                ViewModel.WizardData.Wiz_StoreOut.ImageOwner = enumImageOwner.Ausgang;
                ViewModel.SelectedAusgang = ViewModel.WizardData.Wiz_StoreOut.SelectedAusgang.Copy();
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

        //Task<bool> IWizardView.OnNext(BaseViewModel viewModel)
        public async Task<bool> OnNext(BaseViewModel viewModel)
        {
            bool bReturn = false;
            string message = string.Empty;
            string mesInfo = string.Empty;

            switch (ViewModel.AppProcess)
            {
                //case enumAppProcess.StoreIn:
                //    ViewModel.WizardData.Wiz_StoreIn.SelectedEingang = ViewModel.SelectedEingang.Copy();
                //    ViewModel.WizardData.Wiz_StoreIn.ArticleToCheck = ViewModel.SelectedArticle.Copy();
                //    break;
                case enumAppProcess.StoreOut:
                    //if (ViewModel.ByteArrayToSave == null)
                    if ((!ViewModel.SignatureSavedOk) && (!ViewModel.Signed))
                    {
                        mesInfo = "ACHTUNG";
                        message = "Es wurde keine Unterschrift hinterlegt. Wollen Sie eine Unterschrift hinterlegen?";
                        bReturn = await App.Current.MainPage.DisplayAlert(mesInfo, message, "Nein", "Neue Unterschrift");
                    }
                    else
                    {
                        if ((!ViewModel.SignatureSavedOk) && (ViewModel.Signed))
                        {
                            mesInfo = "ACHTUNG";
                            message = "Die Unterschrift wurde nicht gespeichert! Bitte speichern Sie die Unterschrift!";
                            bReturn = await App.Current.MainPage.DisplayAlert(mesInfo, message, "Nicht speichern", "OK");
                        }
                        else
                        {
                            bReturn = true;
                            //if (ViewModel.SignatureSavedOk)
                            //{
                            //    mesInfo = "I";
                            //    message = "Es hat ein Problem beim Speichern der Unterschrift gegeben! Möchten Sie es erneut versuchen?";
                            //    bReturn = await App.Current.MainPage.DisplayAlert(mesInfo, message, "Nein", "Ja");
                            //}
                            //else
                            //{
                            //    if (ViewModel.Signed)
                            //    {

                            //    }
                            //}
                        }
                    }
                    ViewModel.WizardData.Wiz_StoreOut.SelectedAusgang = ViewModel.SelectedAusgang.Copy();
                    break;

                case enumAppProcess.StoreLocationChange:
                    break;
                default:
                    break;
            }
            ViewModel.WizardData.AppProcess = ViewModel.AppProcess;
            ((App)Application.Current).WizardData = ViewModel.WizardData.Copy();
            return bReturn;
            //return Task.FromResult(bReturn); // return true;
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

        private async void btnSavePhoto_Clicked(object sender, EventArgs e)
        {
            string str = string.Empty;
            var settings = new SaveImageSettings()
            {
                ImageFormat = ImageFormat.Jpeg,
                ScaleFactor = 0.7,
                ImageQuality = 1,
                BackgroundColor = Color.Transparent,
                StrokeColor = Color.Black,
                StrokeThickness = 5
            };

            using (var stream = new MemoryStream())
            {
                await this.signaturePad.SaveImageAsync(stream, settings);
                //array = stream.ToArray();
                ViewModel.ByteArrayToSave = stream.ToArray();
            }
            string mesInfo = "Achtung";
            string message = string.Empty;

            if (
                (ViewModel.ByteArrayToSave != null) &&
                (ViewModel.ByteArrayToSave.Length > 0)
              )
            {
                var IsSaved = await ViewModel.SavePhoto();
                if (IsSaved)
                {
                    mesInfo = "Information";
                    message = "Die Unterschrift wurde erfolgreich gespeichert!" + Environment.NewLine;
                }
                else
                {
                    mesInfo = "Achtung";
                    message = "Die Unterschrift konnte nicht gespeichert werden! + Environment.NewLine;" + Environment.NewLine;
                }

                await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
            }
            else
            {
                mesInfo = "Achtung";
                message = "Es liegt keine Unterschrift vor!" + Environment.NewLine;
                await App.Current.MainPage.DisplayAlert(mesInfo, message, "OK");
            }
            str = string.Empty;
        }

        private void btnGarbage_Clicked(object sender, EventArgs e)
        {

        }

        private void signaturePad_Cleared(object sender, EventArgs e)
        {
            ViewModel.BtnSaveEnabeld = false;
            ViewModel.Signed = false;
        }

        private void signaturePad_StrokeStarted(object sender, EventArgs e)
        {

        }

        private void signaturePad_StrokeCompleted(object sender, EventArgs e)
        {
            ViewModel.BtnSaveEnabeld = true;
            ViewModel.Signed = true;
        }
    }
}