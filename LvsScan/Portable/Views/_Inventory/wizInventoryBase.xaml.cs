using LvsScan.Portable.Services;
using LvsScan.Portable.ViewModels.Inventory;
using Newtonsoft.Json;
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
    public partial class wizInventoryBase : ContentPage
    {
        public wizInventoryBase(string myContent)
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new wizInventoryBaseViewModel();
            this.ViewModel.Content = myContent;


            LvsScan.Portable.Models.wizInventory tmpWizInventory = this.ViewModel.wizInventory.Copy();
            var jsonStr = JsonConvert.SerializeObject(tmpWizInventory);
            //var pageCarousel = new wizInventory_Start(jsonStr);
            //Page pCarousel = (Page)pageCarousel;
            Navigation.PushAsync(new wizInventory_Start(jsonStr));
        }
        public wizInventoryBaseViewModel ViewModel { get; set; }
    }
}