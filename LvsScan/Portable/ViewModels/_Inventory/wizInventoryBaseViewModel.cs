using LvsScan.Portable.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Common.Models;
using System.Linq;
using System.Threading.Tasks;
using LvsScan.Portable.Models;
using Xamarin.Forms;
using Newtonsoft.Json;
using System;

namespace LvsScan.Portable.ViewModels.Inventory
{
    [QueryProperty(nameof(Content), nameof(Content))]
    public class wizInventoryBaseViewModel : BaseViewModel
    {
        public wizInventoryBaseViewModel()
        {

        }

        public wizInventory wizInventory { get; set; }
        public ServiceAPI serviceAPI;
        string content = string.Empty;
        public string Content
        {
            get { return content; }
            set
            {
                content = Uri.UnescapeDataString(value ?? string.Empty);
                if (content != string.Empty)
                {
                    var resContent = JsonConvert.DeserializeObject<wizInventory>(content);
                    this.wizInventory = (wizInventory)resContent;
                }
                OnPropertyChanged();
            }
        }
        public int CurrentTabbedPageIndex { get; set; }

    }
}
