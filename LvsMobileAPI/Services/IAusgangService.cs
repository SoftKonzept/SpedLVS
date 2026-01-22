using Common.ApiModels;
using Common.Models;
using LVS.ViewData;
using LvsMobileAPI.DataConnection;

namespace LvsMobileAPI.Services
{
    public interface IAusgangService
    {
        public ResponseAusgang GET_Ausgang(int myId);
        public ResponseAusgang GET_AusgangByLvsNr(int myLvsNr);
        public ResponseAusgang GET_AusgangList_Open();

        public ResponseAusgang POST_Ausgang_UpdateWizStoreOut(ResponseAusgang resStoreOut);

        public bool POST_Ausgang_Test();

    }

    public class AusgangService : IAusgangService
    {
        private SvrSettings srv;
        public AusgangService()
        {
            srv = new SvrSettings();
        }

        /// <summary>
        ///             GET Ausgang by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseAusgang GET_Ausgang(int myId)
        {
            srv = new SvrSettings();
            ResponseAusgang responseAusgang = new ResponseAusgang();
            responseAusgang.Success = false;
            if (myId > 0)
            {
                AusgangViewData ausgangViewData = new AusgangViewData(myId, 0, true);
                if (
                        (ausgangViewData.Ausgang is Ausgaenge) &&
                        (ausgangViewData.Ausgang.Id == myId)
                    )
                {
                    responseAusgang.Success = true;
                    responseAusgang.Ausgang = ausgangViewData.Ausgang.Copy();
                    responseAusgang.ListAusgangArticle = ausgangViewData.ListArticleInAusgang.ToList();
                }
            }
            return responseAusgang;
        }


        /// <summary>
        ///             GET Ausgang by LvsNr
        /// </summary>
        /// <param name="LvsNr"></param>
        /// <returns></returns>
        public ResponseAusgang GET_AusgangByLvsNr(int myLvsNr)
        {
            srv = new SvrSettings();
            ResponseAusgang responseAusgang = new ResponseAusgang();
            responseAusgang.Success = false;
            if (myLvsNr > 0)
            {
                AusgangViewData ausgangViewData = new AusgangViewData();
                ausgangViewData.GetAusgangByLvsNr(myLvsNr);


                if (
                        (ausgangViewData.Ausgang is Ausgaenge) &&
                        (ausgangViewData.Ausgang.Id > 0)
                    )
                {
                    responseAusgang.Success = true;
                    responseAusgang.Ausgang = ausgangViewData.Ausgang.Copy();
                    responseAusgang.ListAusgangArticle = ausgangViewData.ListArticleInAusgang.ToList();
                }
            }
            return responseAusgang;
        }
        /// <summary>
        ///             GET open storeout list 
        /// </summary>
        public ResponseAusgang GET_AusgangList_Open()
        {
            srv = new SvrSettings();
            ResponseAusgang responseAusgang = new ResponseAusgang();
            responseAusgang.Success = false;

            AusgangViewData ausgangViewData = new AusgangViewData();
            try
            {
                ausgangViewData.GetOpenStoreOutList();
                responseAusgang.ListAusgaengeOpen = ausgangViewData.ListAusgaengeOpen.ToList();
                responseAusgang.Success = true;
            }
            catch (Exception ex)
            {
                responseAusgang.Success = false;
                responseAusgang.Error = ex.Message;
            }
            return responseAusgang;
        }


        /// <summary>
        ///             POST finished StoreOut 
        /// </summary>
        public ResponseAusgang POST_Ausgang_UpdateWizStoreOut(ResponseAusgang resStoreOut)
        {
            srv = new SvrSettings();
            resStoreOut.Success = false;
            //AusgangViewData ausgangViewData = new AusgangViewData(resStoreOut.Ausgang, resStoreOut.UserId);

            AusgangViewData ausgangViewData = new AusgangViewData(resStoreOut);
            try
            {
                var result = ausgangViewData.Update_WizStoreOut_Ausgang(resStoreOut.StoreOutArt, resStoreOut.StoreOutArt_Steps);
                resStoreOut.Success = result;
                if (result)
                {
                    ausgangViewData.Fill();
                    resStoreOut.Ausgang = ausgangViewData.Ausgang.Copy();
                    resStoreOut.Info = "Das Update wurde erfolgreich durchgeführt!";
                }
                else
                {
                    resStoreOut.Error = "Das Update konnte nicht durchgeführt werden!";
                }
            }
            catch (Exception ex)
            {
                resStoreOut.Success = false;
                resStoreOut.Error = ex.Message;
            }
            return resStoreOut;
        }

        public bool POST_Ausgang_Test()
        {
            srv = new SvrSettings();

            int iAusId = 53976;
            int iUser = 1;

            AusgangViewData ausgangViewData = new AusgangViewData(iAusId, iUser, false);
            ausgangViewData.PrintDocuments();
            return false;
        }

    }
}
