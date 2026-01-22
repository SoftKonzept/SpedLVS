using LvsScan.Portable.Models;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutMenuPage : FlyoutPage
    {
        public FlyoutMenuPage()
        {
            InitializeComponent();
            if (FlyoutPage != null)
            {
                FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;
            }

            WizardData wizData = ((App)Application.Current).WizardData;
            wizData.Teststring += "FlyoutMenuPage " + Environment.NewLine;
            ((App)Application.Current).WizardData = wizData;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as FlyoutMenuPageFlyoutMenuItem;
            if (item == null)
                return;

            if (item.TargetType != null)
            {
                try
                {
                    var page = (Page)Activator.CreateInstance(item.TargetType);
                    page.Title = item.Title;

                    Detail = new NavigationPage(page);
                    IsPresented = false;
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                }
            }
            else
            {
                DisplayAlert("FEHLER", "Es ist keine Zielsiete hinterlegt!", "OK");
            }
            FlyoutPage.ListView.SelectedItem = null;
        }
    }
}