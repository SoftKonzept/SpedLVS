using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.ViewModels.Inventory
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class wizStartPage : ContentPage
    {
        public wizStartPage(string myContent)
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new wizInventoryBaseViewModel();
            ViewModel.Content = myContent;
        }
        public wizInventoryBaseViewModel ViewModel { get; set; }
    }
}