using Common.ApiModels;
using Common.Enumerations;
using Common.Models;
using LVS;
using LVS.ViewData;
using LvsMobileAPI.DataConnection;

namespace LvsMobileAPI.Services
{
    public interface ICallService
    {
        public ResponseCall GetCall(int CallId, int UserId);
        public ResponseCall GetCallByLvsNr(int LvsNr, int UserId);
        public ResponseCall GET_CallList_Open();
        public ResponseCall POST_Call_UpdateWizStoreOut(ResponseCall resCall);
        public ResponseCall POST_Call_CreateStoreOut(ResponseCall resCall);

    }


    public class CallService : ICallService
    {
        private SvrSettings srv;

        public CallService()
        {
            srv = new SvrSettings();
        }

        public ResponseCall GetCall(int CallId, int UserId)
        {
            ResponseCall resCall = new ResponseCall();
            resCall.Success = false;
            if (CallId > 0)
            {
                CallViewData callVD = new CallViewData(CallId, UserId);
                var call = callVD.Abruf.Copy();
                if (
                        (callVD.Abruf is Calls) &&
                        (callVD.Abruf.Id == CallId)
                    )
                {
                    resCall.Success = true;
                    resCall.Call = callVD.Abruf.Copy();
                }
            }
            return resCall;
        }

        public ResponseCall GetCallByLvsNr(int LvsNr, int UserId)
        {
            ResponseCall resCall = new ResponseCall();
            resCall.Success = false;
            if (LvsNr > 0)
            {
                CallViewData callVD = new CallViewData(UserId);
                callVD.GetCallbyLvsNr(LvsNr);

                resCall.Success = (callVD.ListCallOpen.Count > 0);
                if (resCall.Success)
                {
                    resCall.ListCallOpen = callVD.ListCallOpen;
                }
            }
            return resCall;
        }

        public ResponseCall GET_CallList_Open()
        {
            ResponseCall resCall = new ResponseCall();
            resCall.Success = false;

            CallViewData callVD = new CallViewData();
            try
            {
                callVD.GetCallListOpen();
                resCall.ListCallOpen = callVD.ListCallOpen.ToList();
                resCall.Success = true;
            }
            catch (Exception ex)
            {
                resCall.Success = false;
                resCall.Error = ex.Message;
            }
            return resCall;
        }

        public ResponseCall POST_Call_UpdateWizStoreOut(ResponseCall resCall)
        {
            srv = new SvrSettings();
            resCall.Success = false;

            CallViewData callViewData = new CallViewData(resCall.Call, resCall.UserId);
            try
            {
                var result = callViewData.Update_WizStoreOut(resCall.StoreOutArt, resCall.StoreOutArt_Steps);
                resCall.Success = result;
                if (result)
                {
                    callViewData.Fill();
                    resCall.Call = callViewData.Abruf.Copy();
                    resCall.Info = "Das Update wurde erfolgreich durchgeführt!";
                }
                else
                {
                    resCall.Error = "Das Update konnte nicht durchgeführt werden!";
                }
            }
            catch (Exception ex)
            {
                resCall.Success = false;
                resCall.Error = ex.Message;
            }
            return resCall;
        }
        public ResponseCall POST_Call_CreateStoreOut(ResponseCall resCall)
        {
            srv = new SvrSettings();
            bool bReturn = false;
            if (resCall.StoreOutArt_Steps.Equals(enumStoreOutArt_Steps.wizStepDoStoreOut))
            {
                //bool myChecked = true;
                CallViewData callViewData = new CallViewData(resCall.UserId);
                callViewData.IsScanProcess = true;

                try
                {
                    resCall = callViewData.CreateLAusgang(clsASNCall.const_AbrufAktion_Abruf, resCall);

                    //bReturn = Task.Run(() => callViewData.CreateLAusgang(clsASNCall.const_AbrufAktion_Abruf, resCall.ListCallForStoreOut)).Result;
                    //string str = string.Empty;

                    if (callViewData.ListLogText != null)
                    {
                        foreach (string s in callViewData.ListLogText)
                        {
                            resCall.Info += s + Environment.NewLine;
                        }
                    }

                }
                catch (Exception ex)
                {
                    resCall.Success = false;
                    resCall.Error = ex.Message;
                }
            }
            else
            {
                resCall.Error = "Dieser Prozess ist falsch deklariert!";
                resCall.Success = false;
            }
            return resCall;
        }

    }
}
