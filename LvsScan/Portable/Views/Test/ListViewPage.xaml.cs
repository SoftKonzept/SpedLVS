using LvsScan.Portable.ViewModels.Test;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.Test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewPage : ContentPage
    {
        public ListViewModel Viewmodel { get; set; }
        public ListViewPage()
        {
            InitializeComponent();
            BindingContext = Viewmodel = new ListViewModel();
        }

        private void viewTest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // object obj = Viewmodel.SelectedItem;

            object obj2 = sender;
            object obj1 = e.CurrentSelection;

            //Viewmodel.ListSelectedItems = new List<TestString>((IEnumerable<TestString>)e.CurrentSelection.ToList());
        }
    }
}