using LvsScan.Portable.ViewModels.Test;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.Test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DbTestConnectionInternPage : ContentPage
    {
        public DbTestConnectionInternViewModel ViewModel { get; set; }
        public DbTestConnectionInternPage()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new DbTestConnectionInternViewModel();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            ViewModel.GetResult();
        }

    }
}