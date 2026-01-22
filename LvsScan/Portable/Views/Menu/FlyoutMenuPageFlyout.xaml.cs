using Common.Models;
using LvsScan.Portable.ViewModels.Menu;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutMenuPageFlyout : ContentPage
    {
        public ListView ListView;
        public FlyoutMenuPageFlyoutViewModel ViewModel;

        public FlyoutMenuPageFlyout()
        {
            InitializeComponent();

            this.BindingContext = ViewModel = new FlyoutMenuPageFlyoutViewModel();
            if (((App)Application.Current).LoggedUser is Users)
            {
                ViewModel.InfoUser = ((App)Application.Current).LoggedUser;
            }
            ListView = MenuItemsListView;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}