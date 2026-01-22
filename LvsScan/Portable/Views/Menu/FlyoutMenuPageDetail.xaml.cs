using LvsScan.Portable.ViewModels.Menu;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutMenuPageDetail : ContentPage
    {

        public FlyoutMenuPageDetail()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new FlyoutMenuPageDetailViewModel();
        }
        public FlyoutMenuPageDetailViewModel ViewModel { get; set; }


    }
}