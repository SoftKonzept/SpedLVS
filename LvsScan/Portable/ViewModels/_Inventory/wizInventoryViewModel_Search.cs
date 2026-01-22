using LvsScan.Portable.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Common.Models;
using System.Linq;
using LvsScan.Portable.Models;
using System.Threading.Tasks;
using Xamarin.Forms;
using System;
using Newtonsoft.Json;

namespace LvsScan.Portable.ViewModels.Inventory
{
    [QueryProperty(nameof(Content), nameof(Content))]
    public class wizInventoryViewModel_Search : BaseViewModel
    {
        public wizInventoryViewModel_Search()
        {
            serviceAPI = new ServiceAPI();

        }
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

        public wizInventory wizInventory { get; set; }
        private int searchArtikelId;
        public int SearchArtikelId
        {
            get { return searchArtikelId; }
            set
            {
                searchArtikelId = value;
                OnPropertyChanged();
            }
        }


        private string searchProduktionsnummer;
        public string SearchProduktionsnummer
        {
            get { return searchProduktionsnummer; }
            set
            {
                searchProduktionsnummer=value;
                OnPropertyChanged();
            }
        }



    }
}
