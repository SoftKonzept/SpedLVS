using Common.Models;

namespace LvsScan.Portable.Models
{
    public class wizStoreLocationChanged
    {

        //Step 1
        private ArticleSearch _articleSearch = new ArticleSearch();
        public ArticleSearch ArticleSearch
        {
            get { return _articleSearch; }
            set { _articleSearch = value; }
        }
        //Step 2   
        private Articles _articleToChange = new Articles();
        public Articles ArticleToChange
        {
            get { return _articleToChange; }
            set { _articleToChange = value; }
        }
        //Step 3
        private Articles _articleChanged = new Articles();
        public Articles ArticleChanged
        {
            get { return _articleChanged; }
            set { _articleChanged = value; }
        }


        public wizStoreLocationChanged Copy()
        {
            return (wizStoreLocationChanged)this.MemberwiseClone();
        }
    }
}
