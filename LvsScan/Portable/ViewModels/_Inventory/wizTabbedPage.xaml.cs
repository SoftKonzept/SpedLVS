using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LvsScan.Portable.ViewModels.Inventory;
using LvsScan.Portable.Views.Inventory;
using Newtonsoft.Json;

namespace LvsScan.Portable.ViewModels.Inventory
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class wizTabbedPage : TabbedPage
    {
        public wizTabbedPage(string myContent)
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new wizInventoryBaseViewModel();
            ViewModel.Content = myContent;
            InitChildPages();
        }
        public wizInventoryBaseViewModel ViewModel { get; set; }

        private void InitChildPages()
        {
            var jsonStr = JsonConvert.SerializeObject(ViewModel.wizInventory);
            //var pageCarousel = new wizInventory_Start(jsonStr);

            //var startPage = new wizStartPage(jsonStr);
            //Children.Add(startPage);

            //1. Step
            var searchPage = new wizInventory_Search(jsonStr);
            NavigationPage navPage = new NavigationPage(searchPage);
            navPage.Title = "Step 1";
            Children.Add(navPage);

            //1. Step
            var resultPage = new wizInventory_SearchResult(jsonStr);
            navPage = new NavigationPage(resultPage);
            navPage.Title = "Step 2";
            Children.Add(navPage);




        }

        private void TabbedPage_CurrentPageChanged(object sender, EventArgs e)
        {
            var index = this.Children.IndexOf(this.CurrentPage);
            int iTmp = 0;
            int.TryParse(index.ToString(), out iTmp);
            ViewModel.CurrentTabbedPageIndex= iTmp;
        }
    }
}