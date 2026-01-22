using LvsScan.Portable.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.Inventory
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class wizInventory : ContentPage
    {
        public wizInventory(string myContent)
        {
            InitializeComponent();
            //var pageCarousel = new wizInventory_Start(String.Empty);
            //Page pCarousel = (Page)pageCarousel;
            Navigation.PushAsync(new wizInventory_Start(myContent), false);
        }
    }
}