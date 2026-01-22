using Common.ApiModels;
using Common.Models;
using LVS.ViewData;
using LvsMobileAPI.DataConnection;

namespace LvsMobileAPI.Services
{
    public interface IEingangService
    {
        public ResponseEingang GET_Eingang(int myId);
        public ResponseEingang GET_EingangList_Open();
        public ResponseEingang POST_Eingang_UpdateWizStoreIn(ResponseEingang resStoreIn);
        public ResponseEingang POST_Eingang_Add(ResponseEingang resStoreIn);
    }

    public class EingangService : IEingangService
    {
        private SvrSettings srv;
        public EingangService()
        {
            srv = new SvrSettings();
        }

        /// <summary>
        ///             GET Eingang by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseEingang GET_Eingang(int myId)
        {
            srv = new SvrSettings();
            ResponseEingang responseEingang = new ResponseEingang();
            responseEingang.Success = false;
            if (myId > 0)
            {
                EingangViewData viewData = new EingangViewData(myId, 0, true);
                if (
                        (viewData.Eingang is Eingaenge) &&
                        (viewData.Eingang.Id == myId)
                    )
                {
                    responseEingang.Success = true;
                    responseEingang.Eingang = viewData.Eingang.Copy();
                    responseEingang.ListEingangArticle = viewData.ListArticleInEingang.ToList();
                }
            }
            return responseEingang;
        }

        /// <summary>
        ///             GET open storeout list 
        /// </summary>
        public ResponseEingang GET_EingangList_Open()
        {
            srv = new SvrSettings();
            ResponseEingang responseEingang = new ResponseEingang();
            responseEingang.Success = false;

            EingangViewData viewData = new EingangViewData();
            try
            {
                viewData.GetOpenStoreOutList();
                responseEingang.ListEingaengeOpen = viewData.ListEingaengeOpen.ToList();
                responseEingang.Success = true;
            }
            catch (Exception ex)
            {
                responseEingang.Success = false;
                responseEingang.Error = ex.Message;
            }
            return responseEingang;
        }


        /// <summary>
        ///             POST finished StoreOut 
        /// </summary>
        public ResponseEingang POST_Eingang_UpdateWizStoreIn(ResponseEingang resStoreIn)
        {
            srv = new SvrSettings();
            resStoreIn.Success = false;
            //EingangViewData viewData = new EingangViewData(resStoreIn.Eingang, resStoreIn.UserId);
            EingangViewData viewData = new EingangViewData(resStoreIn);

            try
            {
                var result = viewData.Update_WizStoreIN(resStoreIn);
                resStoreIn.Success = result;
                if (result)
                {
                    viewData.Fill();
                    resStoreIn.Eingang = viewData.Eingang.Copy();

                    if (!viewData.Eingang.Check)
                    {
                        resStoreIn.Info = viewData.Info;
                    }
                    else
                    {
                        resStoreIn.Info = "Das Update wurde erfolgreich durchgeführt!";
                    }
                }
                else
                {
                    resStoreIn.Error = "Das Update konnte nicht durchgeführt werden!";
                }
            }
            catch (Exception ex)
            {
                resStoreIn.Success = false;
                resStoreIn.Error = ex.Message;
            }
            return resStoreIn;
        }

        /// <summary>
        ///             POST Add Eingang 
        /// </summary>
        public ResponseEingang POST_Eingang_Add(ResponseEingang resStoreIn)
        {
            srv = new SvrSettings();
            resStoreIn.Success = false;
            EingangViewData viewData = new EingangViewData(resStoreIn);

            try
            {
                var result = viewData.AddByScanner();
                resStoreIn.Success = result;
                if (result)
                {
                    viewData.Fill();
                    resStoreIn.Eingang = viewData.Eingang.Copy();
                    resStoreIn.Info = "Der Eintrag wurde erfolgreich durchgeführt!";
                }
                else
                {
                    resStoreIn.Error = "Der Eintrag konnte nicht durchgeführt werden!";
                }
            }
            catch (Exception ex)
            {
                resStoreIn.Success = false;
                resStoreIn.Error = ex.Message;
            }
            return resStoreIn;
        }

    }
}
