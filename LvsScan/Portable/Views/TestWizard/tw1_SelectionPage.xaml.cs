using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.TestWizard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class tw1_SelectionPage : ContentPage
    {
        public tw1_SelectionPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new tw2_wizHost());
        }
    }
}