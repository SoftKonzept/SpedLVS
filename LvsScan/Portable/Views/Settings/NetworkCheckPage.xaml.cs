using LvsScan.Portable.Enumerations;
using LvsScan.Portable.Interfaces;
using LvsScan.Portable.ViewModels.Settings;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NetworkCheckPage : ContentPage
    {
        public NetworkCheckViewModel ViewModel { get; set; }
        public NetworkCheckPage()
        {
            InitializeComponent();
            this.BindingContext = this.ViewModel = new NetworkCheckViewModel();
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }
        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Achtung", "Networt Connectivity has changed!", "OK");
            }
            );
        }

        private void SaveButton_Clicked(object sender, System.EventArgs e)
        {
            string fileText = string.Empty;
            fileText = this.ViewModel.NetworkInfo;

            string fileName = Services.FileService.GetTextFilename(enumMainMenu.Settings);
            string filePath = Services.FileService.GetFilePath(enumFileStorageArt.publicExternStorage, enumMainMenu.Settings);

            enumFileStorageArt enumfileArt = enumFileStorageArt.publicExternStorage;
            enumMainMenu enumMenu = enumMainMenu.Settings;

            try
            {
                DependencyService.Get<IFileService>().CreateFile(fileText, fileName, filePath);
            }
            catch (Exception ex)
            {
                string str = ex.Message.ToString();
            }

        }

        private async void btnAnalyse_Clicked(object sender, EventArgs e)
        {
            await ViewModel.DoAnalyzing();
        }
    }
}