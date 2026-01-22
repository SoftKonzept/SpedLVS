using Common.Enumerations;
using Common.Models;
using LvsScan.Portable.Services;
using System;
using System.Threading.Tasks;

namespace LvsScan.Portable.ViewModels.Test
{
    public class me_TestFirstPageViewModel : BaseViewModel
    {
        public me_TestFirstPageViewModel()
        {
            Init();
        }
        public me_TestFirstPageViewModel(enumStoreInArt myStoreInArt)
        {
            StoreInArt = myStoreInArt;
            Init();
        }

        public void Init()
        {
            IsBusy = false;
        }

        public async Task GetArticle()
        {
            try
            {
                int iTmp = 0;
                int.TryParse(ArticleIdString, out iTmp);

                this.IsBusy = true;
                api_Article _api = new api_Article();
                var result = await _api.GET_Article_ById(iTmp, 1);
                SelectedArticle = result.Copy();
                this.IsBusy = false;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                await MessageService.ShowAsync("ACHTUNG", str);
            }
        }

        //private enumStoreInArt _StoreInArt = enumStoreInArt.NotSet;
        private enumStoreInArt _StoreInArt = enumStoreInArt.manually;
        public enumStoreInArt StoreInArt
        {
            get { return _StoreInArt; }
            set { SetProperty(ref _StoreInArt, value); }
        }

        private Articles _SelectedArticle = new Articles();
        public Articles SelectedArticle
        {
            get { return _SelectedArticle; }
            set { SetProperty(ref _SelectedArticle, value); }
        }
        private int _ArticleId = 0;
        public int ArticleId
        {
            get { return _ArticleId; }
            set { SetProperty(ref _ArticleId, value); }
        }

        private string _ArticleIdString = "0";
        public string ArticleIdString
        {
            get { return _ArticleIdString; }
            set { SetProperty(ref _ArticleIdString, value); }
        }






    }
}
