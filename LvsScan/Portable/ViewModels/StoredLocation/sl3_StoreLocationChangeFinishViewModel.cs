using Common.Models;

namespace LvsScan.Portable.ViewModels.StoredLocation
{
    public class sl3_StoreLocationChangeFinishViewModel : BaseViewModel
    {
        internal Services.api_Article apiArticle;
        public sl3_StoreLocationChangeFinishViewModel()
        {

        }

        private Articles _article = new Articles();
        public Articles Article
        {
            get { return _article; }
            set
            {
                SetProperty(ref _article, value);

            }
        }


    }
}
