using Common.ApiModels;
using Common.Enumerations;
using Common.Models;
using LvsScan.Portable.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LvsScan.Portable.ViewModels.StoreOut.Open
{
    public class oa3w2_wizViewModel : BaseViewModel
    {
        public oa3w2_wizViewModel()
        {
        }

        public enumStoreOutArt StoreOutArt = enumStoreOutArt.open;
        public async Task GetArticleInAusgang()
        {
            try
            {
                this.IsBusy = true;
                api_Ausgang _api_Ausgang = new api_Ausgang();
                var result = await _api_Ausgang.GET_Ausgang_byId(SelectedAusgang.Id);
                if (result is ResponseAusgang)
                {
                    ArticlesInAusgang = new ObservableCollection<Articles>(result.ListAusgangArticle);
                }
                this.IsBusy = false;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        private ObservableCollection<Articles> articlesInAusgang = new ObservableCollection<Articles>();
        public ObservableCollection<Articles> ArticlesInAusgang
        {
            get { return articlesInAusgang; }
            set
            {
                SetProperty(ref articlesInAusgang, value);
                this.WizardData.Wiz_StoreOut.ListArticleInAusgang = new List<Articles>(articlesInAusgang);
                CountUncheckedArticles = articlesInAusgang.Where(x => x.AusgangChecked == false).ToList().Count;
            }
        }

        private int countUncheckedArticles = 0;
        public int CountUncheckedArticles
        {
            get { return countUncheckedArticles; }
            set { SetProperty(ref countUncheckedArticles, value); }
        }
        private Ausgaenge selectedAusgang = new Ausgaenge();
        public Ausgaenge SelectedAusgang
        {
            get { return selectedAusgang; }
            set { SetProperty(ref selectedAusgang, value); }
        }
    }
}
