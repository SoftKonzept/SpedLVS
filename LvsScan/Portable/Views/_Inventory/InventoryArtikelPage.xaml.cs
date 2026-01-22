using LvsScan.Portable.ViewModels.Inventory;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace LvsScan.Portable.Views.Inventory
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InventoryArtikelPage : ContentPage
    {
        public InventoryArtikelViewModel ViewModel { get; set; }

        public InventoryArtikelPage(string myContent)
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new InventoryArtikelViewModel();
            this.ViewModel.Content = myContent;

        }


    }
}