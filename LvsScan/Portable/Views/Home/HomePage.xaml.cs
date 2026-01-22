using LvsScan.Portable.ViewModels.Home;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.KeyboardHelper;

namespace LvsScan.Portable.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomeViewModel ViewModel;

        public HomePage()
        {
            InitializeComponent();
            this.Appearing += HomePage_Appearing;
            this.Disappearing += HomePage_Disappearing;

            this.BindingContext = ViewModel = new HomeViewModel();

        }

        private void HomePage_Disappearing(object sender, EventArgs e)
        {
            SoftKeyboard.Current.VisibilityChanged -= Current_VisibilityChanged;
        }

        private void HomePage_Appearing(object sender, EventArgs e)
        {
            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;
        }

        private void Current_VisibilityChanged(SoftKeyboardEventArgs e)
        {
            if (e.IsVisible)
            {
                // do your things
            }
            else
            {
                // do your things
            }
        }


        private async void btnCam_Clicked(object sender, EventArgs e)
        {
            //try
            //{
            //    var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
            //    {
            //        DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Rear,
            //        Directory = "Xamarin",
            //        SaveToAlbum = true
            //    });

            //    if (photo != null)
            //    {
            //        imgCamera.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
            //        imgBytes = helper_XamImage.XamImageToByteArray(photo.GetStream());
            //    }
            //}
            //catch (Exception ex)
            //{
            //    await DisplayAlert("Error", ex.Message.ToString(), "Ok");
            //}
        }
    }
}