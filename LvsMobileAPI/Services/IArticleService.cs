using Common.ApiModels;
using Common.Enumerations;
using Common.Models;
using LVS.ViewData;
using LvsMobileAPI.DataConnection;

namespace LvsMobileAPI.Services
{
    public interface IArticleService
    {
        public Articles GET_Article(int ArticleId, int UserId);
        public Articles GET_Article_ExistArticleValue(string SearchValueString, int enumArticleDatafieldId);
        public Articles GET_Article_ExistArticleLvsForStoreLocationChange(string SearchLvs);
        public Articles GET_Article_SearchArticle(string LvsNoString, string ProductionnumberString);
        public Articles GET_Article_SearchArticleForStoredLocationChange(string LvsNoString);
        public List<Articles> GET_Article_SearchArticleInStoreINByProductionNo(string ProductionnumberString);
        public ResponseStoreLocationChange POST_Article_Update_StoreLocation(ResponseStoreLocationChange responseStoreLocationChange);
        public ResponseArticle POST_Article_Update_ScanValue(ResponseArticle responseArticle);
        public ResponseArticle POST_Article_Update_Checked(ResponseArticle responseArticle);
        public ResponseArticle POST_Article_Update_ScanIdentification(ResponseArticle responseArticle);
        public ResponseArticle POST_Article_AddByScanner(ResponseArticle responseArticle);

        public ResponseArticle POST_Article_Update_ManualEdit(ResponseArticle responseArticle);


    }

    public class ArticleService : IArticleService
    {
        private SvrSettings srv;
        public ArticleService()
        {
            srv = new SvrSettings();
        }

        /// <summary>
        ///             GetArticle by Id
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        public Articles GET_Article(int ArticleId, int UserId)
        {
            srv = new SvrSettings();
            Articles articles = new Articles();

            if (ArticleId > 0)
            {
                ArticleViewData articleViewData = new ArticleViewData(ArticleId, UserId);
                if (
                        (articleViewData.Artikel is Articles) &&
                        (articleViewData.Artikel.Id == ArticleId)
                    )
                {
                    articles = articleViewData.Artikel.Copy();
                }
            }
            return articles;
        }

        public Articles GET_Article_ExistArticleValue(string SearchValueString, int enumArticleDatafieldId)
        {
            srv = new SvrSettings();
            Articles articles = new Articles();
            if (!SearchValueString.Equals(string.Empty))
            {
                enumArticle_Datafields tmpDatafield = enumArticle_Datafields.NotSet;
                Enum.TryParse(enumArticleDatafieldId.ToString(), out tmpDatafield);
                if (tmpDatafield != enumArticle_Datafields.NotSet)
                {
                    ArticleViewData articleViewData = new ArticleViewData(new LVS.Globals._GL_USER());
                    bool bValueExist = articleViewData.ExistSearchArtikelValue(SearchValueString, tmpDatafield);
                    if (bValueExist)
                    {
                        articles = articleViewData.Artikel.Copy();
                    }
                }
            }
            return articles;
        }

        public Articles GET_Article_ExistArticleLvsForStoreLocationChange(string SearchLvs)
        {
            srv = new SvrSettings();
            Articles articles = new Articles();
            if (!SearchLvs.Equals(string.Empty))
            {
                ArticleViewData articleViewData = new ArticleViewData(new LVS.Globals._GL_USER());
                articleViewData.SearchArtikelByLvsInclUB(SearchLvs);

                //CHeck ist UB
                if ((articleViewData.Artikel is Articles) && (articleViewData.Artikel.Id > 0))
                {
                    if (articleViewData.Artikel.Umbuchung)
                    {
                        Articles tmpArt = articleViewData.Artikel.Copy();
                        articleViewData.SearchArtikelByArtIdAlt(articleViewData.Artikel.Id);
                        if ((articleViewData.Artikel is Articles) && (articleViewData.Artikel.Id > 0))
                        {
                            articleViewData.SearchArtikelByArtIdAlt(articleViewData.Artikel.Id);
                            articles = articleViewData.Artikel.Copy();
                        }
                    }
                    else
                    {
                        articles = articleViewData.Artikel.Copy();
                    }
                }
            }
            return articles;
        }
        public List<Articles> GET_Article_SearchArticleInStoreINByProductionNo(string ProductionnumberString)
        {
            srv = new SvrSettings();
            List<Articles> returnList = new List<Articles>();
            if (!ProductionnumberString.Equals(string.Empty))
            {
                ArticleViewData articleViewData = new ArticleViewData(new LVS.Globals._GL_USER());
                //articleViewData.FillClsOnly=false;
                returnList = articleViewData.GetArticleInStoreOutbyProductionNo(ProductionnumberString);
            }
            return returnList;
        }

        public Articles GET_Article_SearchArticle(string LvsNoString, string ProductionnumberString)
        {
            srv = new SvrSettings();
            Articles articles = new Articles();
            if (!LvsNoString.Equals(string.Empty))
            {
                ArticleViewData articleViewData = new ArticleViewData(new LVS.Globals._GL_USER());
                if (!ProductionnumberString.Equals(string.Empty))
                {
                    articleViewData.SearchArtikelByLvsAndProductionNo(LvsNoString, ProductionnumberString);
                }
                else
                {
                    articleViewData.SearchArtikelByLvsAndProductionNo(LvsNoString, string.Empty);
                }

                if ((articleViewData.Artikel is Articles) && (articleViewData.Artikel.LVS_ID.ToString().Equals(LvsNoString)))
                {
                    articles = articleViewData.Artikel.Copy();
                }
            }
            return articles;
        }

        public Articles GET_Article_SearchArticleForStoredLocationChange(string LvsNoString)
        {
            srv = new SvrSettings();
            Articles articles = new Articles();
            if (!LvsNoString.Equals(string.Empty))
            {
                ArticleViewData articleViewData = new ArticleViewData(new LVS.Globals._GL_USER());
                articleViewData.SearchArtikelByLvsAndProductionNo(LvsNoString, string.Empty);

                //CHeck ist UB
                if ((articleViewData.Artikel is Articles) && (articleViewData.Artikel.Id > 0))
                {
                    if (articleViewData.Artikel.Umbuchung)
                    {
                        Articles tmpArt = articleViewData.Artikel.Copy();
                        articleViewData.SearchArtikelByArtIdAlt(articleViewData.Artikel.Id);
                        if ((articleViewData.Artikel is Articles) && (articleViewData.Artikel.Id > 0))
                        {
                            articleViewData.SearchArtikelByArtIdAlt(articleViewData.Artikel.Id);
                            articles = articleViewData.Artikel.Copy();
                        }
                    }
                    else
                    {
                        articles = articleViewData.Artikel.Copy();
                    }
                }
            }
            return articles;
        }

        public ResponseStoreLocationChange POST_Article_Update_StoreLocation(ResponseStoreLocationChange responseStoreLocationChange)
        {
            if (responseStoreLocationChange.Article.Id > 0)
            {
                srv = new SvrSettings();
                ArticleViewData articleViewData = new ArticleViewData(responseStoreLocationChange.Article, new LVS.Globals._GL_USER());

                bool bReturn = articleViewData.Update_StoreLocation();

                responseStoreLocationChange.SuccessStoreLocationChange = bReturn;
                if (bReturn)
                {
                    articleViewData.Fill();
                    responseStoreLocationChange.Article = articleViewData.Artikel.Copy();
                    responseStoreLocationChange.Info = "Der Lagerort wurde erfolgreich geändert!";
                }
                else
                {
                    responseStoreLocationChange.Error = "Achtung - Lagerortupdate konnte nicht durchgeführt werden!";
                }
            }
            else
            {
                responseStoreLocationChange.Error = "Achtung - Artikel Id ist nicht vorhanden!";
            }
            return responseStoreLocationChange;
        }

        public ResponseArticle POST_Article_Update_ScanValue(ResponseArticle responseArticle)
        {
            responseArticle.Error = String.Empty;
            responseArticle.Success = false;
            responseArticle.Info = String.Empty;

            srv = new SvrSettings();
            ArticleViewData articleViewData = new ArticleViewData(responseArticle.Article.Id, new LVS.Globals._GL_USER());
            switch (responseArticle.AppProcess)
            {
                case enumAppProcess.StoreIn:
                    articleViewData.Artikel.ScanIn = responseArticle.Article.ScanIn;
                    articleViewData.Artikel.ScanInUser = responseArticle.Article.ScanInUser;
                    break;
                case enumAppProcess.StoreOut:
                    articleViewData.Artikel.ScanOut = responseArticle.Article.ScanOut;
                    articleViewData.Artikel.ScanOutUser = responseArticle.Article.ScanOutUser;
                    break;
            }
            responseArticle.Success = articleViewData.Update_ScanValue(responseArticle.AppProcess);
            if (responseArticle.Success)
            {
                articleViewData.Fill();
                responseArticle.Article = articleViewData.Artikel.Copy();
                responseArticle.Info = "Das Update konnte erfolgreich durchgeführt werden!";
            }
            else
            {
                responseArticle.Error = "Achtung - Das Update konnte nicht durchgeführt werden!";
            }
            return responseArticle;
        }

        public ResponseArticle POST_Article_Update_Checked(ResponseArticle responseArticle)
        {
            responseArticle.Error = String.Empty;
            responseArticle.Success = false;
            responseArticle.Info = String.Empty;

            srv = new SvrSettings();
            ArticleViewData articleViewData = new ArticleViewData(responseArticle.Article.Id, new LVS.Globals._GL_USER());
            //switch (responseArticle.AppProcess)
            //{
            //    case enumAppProcess.StoreIn:
            //        articleViewData.Artikel.ScanIn = responseArticle.Article.ScanIn;
            //        articleViewData.Artikel.ScanInUser = responseArticle.Article.ScanInUser;
            //        break;
            //    case enumAppProcess.StoreOut:
            //        articleViewData.Artikel.ScanOut = responseArticle.Article.ScanOut;
            //        articleViewData.Artikel.ScanOutUser = responseArticle.Article.ScanOutUser;
            //        break;
            //}
            responseArticle.Success = articleViewData.Update_Checked(responseArticle.AppProcess);
            if (responseArticle.Success)
            {
                articleViewData.Fill();
                responseArticle.Article = articleViewData.Artikel.Copy();
                responseArticle.Info = "Das Update konnte erfolgreich durchgeführt werden!";
            }
            else
            {
                responseArticle.Error = "Achtung - Das Update konnte nicht durchgeführt werden!";
            }
            return responseArticle;
        }


        public ResponseArticle POST_Article_Update_ScanIdentification(ResponseArticle responseArticle)
        {
            srv = new SvrSettings();
            ArticleViewData articleViewData = new ArticleViewData(responseArticle.Article.Id, new LVS.Globals._GL_USER());
            responseArticle.Success = articleViewData.Update_ScanIdentification();
            if (responseArticle.Success)
            {
                responseArticle.Article = articleViewData.Artikel.Copy();
                responseArticle.Info = "Das Update konnte erfolgreich durchgeführt werden!";
            }
            else
            {
                responseArticle.Error = "Achtung - Das Update konnte nicht durchgeführt werden!";
            }
            return responseArticle;
        }

        public ResponseArticle POST_Article_AddByScanner(ResponseArticle responseArticle)
        {
            srv = new SvrSettings();
            ArticleViewData articleViewData = new ArticleViewData(responseArticle.Article, new LVS.Globals._GL_USER());


            responseArticle.Success = articleViewData.AddbyScanner();
            if (responseArticle.Success)
            {
                responseArticle.Article = articleViewData.Artikel.Copy();
                responseArticle.Info = "Das Update konnte erfolgreich durchgeführt werden!";
            }
            else
            {
                responseArticle.Error = "Achtung - Das Update konnte nicht durchgeführt werden!";
            }
            return responseArticle;
        }
        public ResponseArticle POST_Article_Update_ManualEdit(ResponseArticle responseArticle)
        {
            srv = new SvrSettings();
            ArticleViewData articleViewData = new ArticleViewData();
            responseArticle.Success = articleViewData.Update_ArticleEdit(responseArticle);
            if (responseArticle.Success)
            {
                responseArticle.Article = articleViewData.Artikel.Copy();
                responseArticle.Info = "Das Update konnte erfolgreich durchgeführt werden!";
            }
            else
            {
                responseArticle.Error = "Achtung - Das Update konnte nicht durchgeführt werden!";
            }
            return responseArticle;
        }

    }
}
