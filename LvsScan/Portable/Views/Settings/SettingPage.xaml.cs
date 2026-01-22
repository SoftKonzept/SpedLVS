using LvsScan.Portable.Views.Menu;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingPage : ContentPage
    {
        public SettingPage()
        {
            InitializeComponent();
            SettingMenuSubItemView.ItemSelected += SettingMenuSubItemView_ItemSelected;
        }

        private async void SettingMenuSubItemView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MenuSubItem;
            if (item == null)
                return;

            if (item.TargetType != null)
            {
                try
                {
                    var page = (Page)Activator.CreateInstance(item.TargetType);
                    page.Title = item.Title;
                    await Navigation.PushAsync(page);
                }
                catch (Exception ex)
                {
                    string str = ex.Message.ToString();
                }
            }
            else
            {
                DisplayAlert("FEHLER", "Es ist keine Zielseite hinterlegt!", "OK");
            }
            SettingMenuSubItemView.SelectedItem = null;
        }
    }
}