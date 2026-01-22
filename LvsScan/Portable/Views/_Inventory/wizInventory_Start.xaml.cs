using LvsScan.Portable.Models;
using LvsScan.Portable.Services;
using Newtonsoft.Json;
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
    public partial class wizInventory_Start : CarouselPage
    {
        public wizInventory_Start(string myContent)
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new wizInventoryBaseViewModel();
            this.ViewModel.Content = myContent;
        }
        public wizInventoryBaseViewModel ViewModel { get; set; }


    }
}