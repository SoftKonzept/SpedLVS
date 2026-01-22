using Common.ApiModels;
using Common.Models;
using LVS.ViewData;
using LvsMobileAPI.DataConnection;

namespace LvsMobileAPI.Services
{
    public interface IDamageService
    {
        public ResponseDamage GET_Damage(int myId);
        public ResponseDamage GET_Damage_List();
        public ResponseDamage GET_Damage_DamageArticleList(int ArticleId, int UserId);
        public ResponseDamage POST_Damage_Update(ResponseDamage resDamage);
        public ResponseDamage POST_Damage_AddDamageArticleAssignment(ResponseDamage response);
        public ResponseDamage DELETE_Damage_DamageArticleAssignment_DeleteItem(int DamageArticleAssignmentId, int UserId);

    }

    public class DamageService : IDamageService
    {
        private SvrSettings srv;
        public DamageService()
        {
            srv = new SvrSettings();
        }

        /// <summary>
        ///             GET Ausgang by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseDamage GET_Damage(int myId)
        {
            srv = new SvrSettings();
            ResponseDamage response = new ResponseDamage();
            response.Success = false;
            if (myId > 0)
            {
                DamageViewData viewData = new DamageViewData(myId, 0, 1);
                if (
                        (viewData.Damage is Damages) &&
                        (viewData.Damage.Id == myId)
                    )
                {
                    response.Success = true;
                    response.Damage = viewData.Damage.Copy();
                    response.ListDamages = viewData.DamagesList.ToList();
                }
            }
            return response;
        }
        /// <summary>
        ///             GET DamageList() 
        /// </summary>
        public ResponseDamage GET_Damage_List()
        {
            srv = new SvrSettings();
            ResponseDamage response = new ResponseDamage();
            response.Success = false;

            DamageViewData viewData = new DamageViewData();
            try
            {
                viewData.GetList();
                response.ListDamages = viewData.DamagesList.ToList();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = ex.Message;
            }
            return response;
        }
        /// <summary>
        ///             GET_ArticleDamageList() 
        /// </summary>
        public ResponseDamage GET_Damage_DamageArticleList(int ArticleId, int UserId)
        {
            srv = new SvrSettings();

            ResponseDamage response = new ResponseDamage();
            response.Success = false;

            DamageViewData viewData = new DamageViewData(0, ArticleId, UserId);
            try
            {
                response.ListDamagesArticleAssignments = viewData.daaViewData.ArticleDamagesList.ToList();
                response.Article = viewData.daaViewData.Article.Copy();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = ex.Message;
            }
            return response;
        }

        /// <summary>
        ///             POST finished StoreOut 
        /// </summary>
        public ResponseDamage POST_Damage_Update(ResponseDamage response)
        {
            srv = new SvrSettings();
            response.Success = false;
            DamageViewData viewData = new DamageViewData(response.UserId, response.Damage);
            try
            {
                var result = viewData.Update();
                response.Success = result;
                if (result)
                {
                    viewData.Fill();
                    response.Damage = viewData.Damage.Copy();
                    response.Info = "Das Update wurde erfolgreich durchgeführt!";
                }
                else
                {
                    response.Error = "Das Update konnte nicht durchgeführt werden!";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = ex.Message;
            }
            return response;
        }

        public ResponseDamage POST_Damage_AddDamageArticleAssignment(ResponseDamage response)
        {
            srv = new SvrSettings();
            response.Success = false;
            DamageArticleAssignmentViewData viewData = new DamageArticleAssignmentViewData(response.Damage, response.Article, response.UserId, false);

            try
            {
                var result = viewData.AddDamageAssignmentToArticle();
                response.Success = result;
                if (result)
                {
                    response.ListDamagesArticleAssignments = viewData.ArticleDamagesList.ToList();
                    response.Info = "Der Schaden konnte dem Artikel zugewiesen werden!";
                }
                else
                {
                    response.Error = "Die Schadenszuweisung konnte nicht durchgeführt werden!";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = ex.Message;
            }
            return response;
        }

        public ResponseDamage DELETE_Damage_DamageArticleAssignment_DeleteItem(int DamageArticleAssignmentId, int UserId)
        {
            srv = new SvrSettings();

            ResponseDamage response = new ResponseDamage();
            response.Success = false;
            response.Damage = null;

            //DamageArticleAssignmentViewData viewData = new DamageArticleAssignmentViewData(response.DamageArticleAssignment, response.Article, response.UserId, false);

            DamageArticleAssignmentViewData viewData = new DamageArticleAssignmentViewData(DamageArticleAssignmentId, UserId);

            try
            {
                response.Success = viewData.DeleteItem();
                if (response.Success)
                {
                    response.ListDamagesArticleAssignments = viewData.ArticleDamagesList.ToList();
                    response.Article = viewData.Article.Copy();
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = ex.Message;
            }
            return response;
        }
    }
}
