using Common.Enumerations;
using Common.Helper;
using Common.Models;
using LvsScan.Portable.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LvsScan.Portable.ViewModels.StoreIn.Open
{
    public class oe1_OpenStoreOutListViewModel : BaseViewModel
    {
        public api_Eingang _api_Eingang;
        public oe1_OpenStoreOutListViewModel()
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
        //            Task.Run(() => GetEingangList()).Wait();
        //        }
        //    }
        //}

        public enumAppProcess AppProcess = enumAppProcess.StoreIn;
        public enumStoreInArt StoreInArt = enumStoreInArt.open;

        private ObservableCollection<Eingaenge> _EingangOpen = new ObservableCollection<Eingaenge>();
        public ObservableCollection<Eingaenge> EingangOpen
        {
            get { return _EingangOpen; }
            set
            {
                SetProperty(ref _EingangOpen, value);
                EingangCount = _EingangOpen.Count;
            }
        }

        public async Task GetEingangList()
        {
            try
            {
                this.IsBusy = true;
                _api_Eingang = new api_Eingang(WizardData.LoggedUser);
                var result = await _api_Eingang.GET_EingangList_Open();
                List<Eingaenge> tmpList1 = result.ListEingaengeOpen.Where(x => x.ArticleCount > 0 && x.CreatedByScanner == false).ToList();
                List<Eingaenge> tmpList2 = result.ListEingaengeOpen.Where(x => x.CreatedByScanner == true).ToList();
                List<Eingaenge> list = new List<Eingaenge>();
                list.AddRange(tmpList1);
                list.AddRange(tmpList2);

                EingangOpen = new ObservableCollection<Eingaenge>(list.OrderBy(x => x.Eingangsdatum).ToList());
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

        private int _EingangCount;
        public int EingangCount
        {
            get { return _EingangCount; }
            set { SetProperty(ref _EingangCount, value); }
        }

        private int checkedCount;
        public int CheckedCount
        {
            get { return checkedCount; }
            set { SetProperty(ref checkedCount, value); }
        }

        private Eingaenge _SelectedEingang;
        public Eingaenge SelectedEingang
        {
            get { return _SelectedEingang; }
            set
            {
                SetProperty(ref _SelectedEingang, value);
                if (
                        (_SelectedEingang is Eingaenge) &&
                        (_SelectedEingang.Id > 0)
                  )
                {
                    this.WizardData.Wiz_StoreIn.SelectedEingang = _SelectedEingang.Copy();
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
