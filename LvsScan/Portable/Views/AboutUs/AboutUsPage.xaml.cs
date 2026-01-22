using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.ViewModels.AboutUs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutUsPage : ContentPage
    {
        public AboutUsPage()
        {
            InitializeComponent();
            GoToWebLink();
        }

        private async void GoToWebLink()
        {
            Uri LinkComtec = new Uri("https://comtec-noeker.de");
            await Browser.OpenAsync(LinkComtec);
            //await Navigation.PushAsync(new HomePage());
            //Helper_Navigation.ClearNavigationStack(Navigation);
        }
    }
}