using Common.ApiModels;
using Common.Helper;
using LVS.Constants;
using LVS.ViewData;
using LvsMobileAPI.DataConnection;

namespace LvsMobileAPI.Services
{
    public interface IAsnReadService
    {
        public ResponseASN GET_ASN_GetLfsArticleListFromAsn();
        public ResponseASN GET_ASN_GetLfsArticleByProductionnumber(string Poductionnumber, int UserId);
        public ResponseASN POST_ASN_CreateStoreInByAsnId(ResponseASN myResponse);

    }


    public class AsnReadService : IAsnReadService
    {
        private SvrSettings srv;

        public AsnReadService()
        {
            srv = new SvrSettings();
        }

        public ResponseASN GET_ASN_GetLfsArticleListFromAsn()
        {
            ResponseASN response = new ResponseASN();
            response.Success = false;
            AsnReadViewData viewData = new AsnReadViewData();

            List<int> tmpList = new System.Collections.Generic.List<int>() { 0 };
            response.Success = viewData.GetStoreinArticleValueList(tmpList);
            if (response.Success)
            {
                response.ListAsnArticleView = viewData.ListAsnArticleValues.ToList();
                response.ListAsnLfsView = viewData.ListAsnLfsValues.ToList();
            }
            return response;
        }

        public ResponseASN GET_ASN_GetLfsArticleByProductionnumber(string Poductionnumber, int UserId)
        {
            ResponseASN response = new ResponseASN();
            response.Success = false;
            if (Poductionnumber.Length > 4)
            {
                AsnReadViewData viewData = new AsnReadViewData(Poductionnumber, UserId);
                var result = viewData.GetStoreinArticleValueIdListBySearchValue(Poductionnumber);
                if (result.Count > 0)
                {
                    List<AsnSearchByProductionNoHelper> listVDA = result.Where(x => x.AsnFileTyp.Equals(constValue_AsnArt.const_Art_VDA4913)).ToList();
                    ResponseASN responseVDA = new ResponseASN();
                    responseVDA.Success = false;
                    if (listVDA.Count > 0)
                    {
                        responseVDA.Success = viewData.GetStoreinArticleValueListBySearchValueVDA(listVDA);
                        if (responseVDA.Success)
                        {
                            response.ListAsnArticleView.AddRange(viewData.ListAsnArticleValues.ToList());
                            response.ListAsnLfsView.AddRange(viewData.ListAsnLfsValues.ToList());
                        }
                    }

                    List<AsnSearchByProductionNoHelper> listEdifact = result.Where(x => x.AsnFileTyp.Equals(constValue_AsnArt.const_Art_EDIFACT_DESADV_D07A)).ToList();
                    ResponseASN responseEdifact = new ResponseASN();
                    responseEdifact.Success = false;
                    if (listEdifact.Count > 0)
                    {
                        responseEdifact.Success = viewData.GetStoreinArticleValueListBySearchValueEdifact(listEdifact);
                        if (responseEdifact.Success)
                        {
                            response.ListAsnArticleView.AddRange(viewData.ListAsnArticleValues.ToList());
                            response.ListAsnLfsView.AddRange(viewData.ListAsnLfsValues.ToList());
                        }
                    }
                    response.Success = (responseEdifact.Success) || (responseVDA.Success);
                    //if (response.Success)
                    //{
                    //    response.ListAsnArticleView = viewData.ListAsnArticleValues.ToList();
                    //    response.ListAsnLfsView = viewData.ListAsnLfsValues.ToList();
                    //}
                }
                response.Info = viewData.LogText;
            }
            return response;
        }

        //public ResponseASN POST_ASN_CreateStoreInByAsnId(int AsnId, int UserId)
        public ResponseASN POST_ASN_CreateStoreInByAsnId(ResponseASN myResponse)
        {
            srv = new SvrSettings();

            AsnReadViewData viewData = new AsnReadViewData(myResponse.UserId);
            viewData.AsnArticleValue = myResponse.AsnArticle.Copy();
            try
            {
                var result = viewData.CreateStoreInByAsnId(myResponse.AsnArticle.AsnId);
                myResponse.Success = result;
                if (result)
                {
                    myResponse.Info = viewData.LogText;
                    myResponse.Error = viewData.Errortext;

                    myResponse.Eingang = viewData.EingangCreated.Copy();
                    myResponse.ArticlesInEingang = viewData.ArticleInEingang.ToList();
                }
                else
                {
                    myResponse.Success = false;
                    if ((viewData.Errortext != null) && (viewData.Errortext.Length > 0))
                    {
                        myResponse.Error = viewData.Errortext + Environment.NewLine;
                    }
                    myResponse.Error += "Der Vorgang konnte nicht durchgeführt werden!" + Environment.NewLine;
                    myResponse.Error += viewData.Errortext;
                }

                //myResponse.InfoList = new List<string>();
                //myResponse.InfoList.Add("Test Zeile 1");
                //myResponse.InfoList.Add("  ->Test Zeile 2");
                //myResponse.InfoList.Add("  ->Test Zeile 3");
                //myResponse.InfoList.Add("     ->Test Zeile 4");
            }
            catch (Exception ex)
            {
                myResponse.Success = false;
                myResponse.Error = ex.Message;
            }
            return myResponse;
        }



    }
}
