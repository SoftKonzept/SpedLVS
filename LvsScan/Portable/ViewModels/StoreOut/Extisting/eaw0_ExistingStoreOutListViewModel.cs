using Common.Enumerations;
using Common.Helper;
using Common.Models;
using LvsScan.Portable.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LvsScan.Portable.ViewModels.StoreOut.Extisting
{
    public class eaw0_ExistingStoreOutListViewModel : BaseViewModel
    {
        public api_Ausgang _api_Ausgang;
        public eaw0_ExistingStoreOutListViewModel()
        {
            _api_Ausgang = new api_Ausgang();
        }

        private bool loadValues = false;
        public bool LoadValues
        {
            get { return loadValues; }
            set
            {
                loadValues = value;
                OnPropertyChanged();
                if (loadValues)
                {
                    Task.Run(() => GetAusaengeList()).Wait();
                }
            }
        }

        public enumStoreOutArt StoreOutArt { set; get; } = enumStoreOutArt.open;

        private ObservableCollection<Ausgaenge> ausgaengeOpen = new ObservableCollection<Ausgaenge>();
        public ObservableCollection<Ausgaenge> AusgaengeOpen
        {
            get { return ausgaengeOpen; }
            set
            {
                SetProperty(ref ausgaengeOpen, value);
                AusgaengeCount = AusgaengeOpen.Count;
            }
        }

        public async Task GetAusaengeList()
        {
            try
            {
                this.IsBusy = true;
                var result = await _api_Ausgang.GET_AusgangList_Open();
                AusgaengeOpen = new ObservableCollection<Ausgaenge>(result.ListAusgaengeOpen.OrderBy(x => x.Termin).ToList());
                this.IsBusy = false;

                if (!result.Success)
                {
                    await MessageService.ShowAsync("ACHTUNG", result.Error);
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        private int ausgaengeCount;
        public int AusgaengeCount
        {
            get { return ausgaengeCount; }
            set { SetProperty(ref ausgaengeCount, value); }
        }

        private int checkedCount;
        public int CheckedCount
        {
            get { return checkedCount; }
            set { SetProperty(ref checkedCount, value); }
        }

        private Ausgaenge selectedAusgang;
        public Ausgaenge SelectedAusgang
        {
            get { return selectedAusgang; }
            set
            {
                SetProperty(ref selectedAusgang, value);
                if (
                        (selectedAusgang is Ausgaenge) &&
                        (selectedAusgang.Id > 0)
                  )
                {
                    this.WizardData.Wiz_StoreOut.SelectedAusgang = selectedAusgang.Copy();
                }
            }
        }

        private System.Drawing.Color backgroundColorBadge = ValueToColorConverter.BadgeBackgroundColor_Default();
        public System.Drawing.Color BackgroundColorBadge
        {
            get { return backgroundColorBadge; }
            set { SetProperty(ref backgroundColorBadge, value); }
        }

    }
}
