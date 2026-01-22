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
    public class wizInventoryViewModel_SearchResult : BaseViewModel
    {
        public wizInventoryViewModel_SearchResult()
        {
            serviceAPI = new ServiceAPI();

        }
        public ServiceAPI serviceAPI;

        public wizInventory wizInventory { get; set; }
 

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

        private int artikelId;
        public int ArtikelId
        {
            get { return artikelId; }
            set
            {
                artikelId = value;
                OnPropertyChanged();
            }
        }

        private int lvsNr;
        public int LvsNr
        {
            get { return lvsNr; }
            set
            {
                lvsNr = value;
                OnPropertyChanged();
            }
        }

        private string produktionsnummer;
        public string Produktionsnummer
        {
            get { return produktionsnummer; }
            set
            {
                produktionsnummer = value;
                OnPropertyChanged();
            }
        }

        private string werk;
        public string Werk
        {
            get { return werk; }
            set
            {
                werk = value;
                OnPropertyChanged();
            }
        }
        private string halle;
        public string Halle
        {
            get { return halle; }
            set
            {
                halle = value;
                OnPropertyChanged();
            }
        }

        private string reihe;
        public string Reihe
        {
            get { return reihe; }
            set
            {
                reihe = value;
                OnPropertyChanged();
            }
        }

        private string ebene;
        public string Ebene
        {
            get { return ebene; }
            set
            {
                ebene = value;
                OnPropertyChanged();
            }
        }

        private string platz;
        public string Platz
        {
            get { return platz; }
            set
            {
                platz = value;
                OnPropertyChanged();
            }
        }


    }
}
