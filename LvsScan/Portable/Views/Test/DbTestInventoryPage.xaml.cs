using LvsScan.Portable.ViewModels.Test;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.Test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DbTestInventoryPage : ContentPage
    {
        public DbTestInventoryViewModel ViewModel { get; set; }
        public DbTestInventoryPage()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new DbTestInventoryViewModel();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            ViewModel.GetResult();
        }


        //private async void testMenuSubItemView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    var item = e.SelectedItem as MenuSubItem;
        //    if (item == null)
        //        return;

        //    if (item.TargetType != null)
        //    {
        //        try
        //        {
        //            var page = (Page)Activator.CreateInstance(item.TargetType);
        //            page.Title = item.Title;
        //            await Navigation.PushAsync(page);
        //        }
        //        catch (Exception ex)
        //        {
        //            string str = ex.Message.ToString();
        //        }
        //    }
        //    else
        //    {
        //        DisplayAlert("FEHLER", "Es ist keine Zielsiete hinterlegt!", "OK");
        //    }
        //    testMenuSubItemView.SelectedItem = null;
        //}
    }
}