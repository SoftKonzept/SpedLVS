using LvsScan.Portable.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LvsScan.Portable.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

namespace LvsScan.Portable.Views.Inventory
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class _wizInventory_Search : ContentPage
    {
        //public wizInventory_Search()
        //{
        //    InitializeComponent();
        //}
        public _wizInventory_Search(string myContent):base()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new wizInventoryBaseViewModel();
            this.ViewModel.Content = myContent;
        }
        //public wizInventoryViewModel_Search ViewModel { get; set; }
        public wizInventoryBaseViewModel ViewModel { get; set; }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            var jsonStr = JsonConvert.SerializeObject(ViewModel.wizInventory);
            //var pages = 

            //await Navigation.PushAsync(new wizTabbedPage(jsonStr));
        }

        private void btnNext_Clicked(object sender, EventArgs e)
        {
            var tabbedPage = Application.Current.MainPage as TabbedPage;
            tabbedPage.CurrentPage = tabbedPage.Children[1];
        }
    }
}