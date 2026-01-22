using Common.Enumerations;
using Common.Helper;
using Common.Models;
using LvsScan.Portable.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LvsScan.Portable.ViewModels.StoreOut.Open
{
    public class oa1_OpenStoreOutListViewModel : BaseViewModel
    {
        public api_Ausgang _api_Ausgang;
        public oa1_OpenStoreOutListViewModel()
        {
            //Title = "oa1_OpenStoreOutListViewModel";
        }

        //private bool loadValues = false;
        //public bool LoadValues
        //{
        //    get { return loadValues; }
        //    set
        //    {
        //        loadValues = value;
        //        OnPropertyChanged();
        //        if (loadValues)
        //        {
        //            Task.Run(() => GetAusaengeList()).Wait();
        //        }
        //    }
        //}

        public enumStoreOutArt StoreOutArt = enumStoreOutArt.open;
        public enumAppProcess AppProcess = enumAppProcess.StoreOut;


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
        /// <summary>
        ///                 Ermittelt die Lister der offenen Ausgänge
        /// </summary>
        /// <returns></returns>
        public async Task GetAusaengeList()
        {
            try
            {
                this.IsBusy = true;
                //-- test 260602025
                _api_Ausgang = new api_Ausgang(WizardData.LoggedUser);
                var result = await _api_Ausgang.GET_AusgangList_Open();
                AusgaengeOpen = new ObservableCollection<Ausgaenge>(result.ListAusgaengeOpen.Where(x => x.ArticleCount > 0).OrderBy(x => x.Termin).ToList());
                if (!result.Success)
                {
                    await MessageService.ShowAsync("ACHTUNG", result.Error + " -> Zeile 64");
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            finally
            {
                //-- test 260602025
                IsBusy = false; // Setzt den Status zurück
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
