using Common.ApiModels;
using Common.Enumerations;
using Common.Models;
using LvsScan.Portable.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LvsScan.Portable.ViewModels.StoreOut.Call
{
    public class c1_CallArticleListViewModel : BaseViewModel
    {
        public api_Call _api_Call;
        public c1_CallArticleListViewModel()
        {
            //IsBusy= false;
        }

        //private bool loadValues = false;
        //public bool LoadValues
        //{
        //    get { return loadValues; }
        //    set
        //    {
        //        loadValues = value;
        //        OnPropertyChanged();
        //        if (loadValues)
        //        {
        //            Task.Run(() => GetCallList()).Wait();
        //        }
        //    }
        //}

        public enumStoreOutArt StoreOutArt = enumStoreOutArt.call;

        private System.Drawing.Color backgroundColorCall = System.Drawing.Color.WhiteSmoke;
        public System.Drawing.Color BackgroundColorCall
        {
            get
            {
                //backgroundColorCall = ValueToColorConverter.ViewBckgroundColorWorkspace_BooleanConvert()
                return backgroundColorCall;
            }
            set { SetProperty(ref backgroundColorCall, value); }
        }

        private ObservableCollection<Calls> _ListCallsAll = new ObservableCollection<Calls>();
        public ObservableCollection<Calls> ListCallsAll
        {
            get { return _ListCallsAll; }
            set
            {
                SetProperty(ref _ListCallsAll, value);
                CountAll = _ListCallsAll.Count;
                //GetSubLists();
                Task.Run(() => GetSubLists()).Wait();
            }
        }

        private ObservableCollection<Calls> _ListCallSelectionSource = new ObservableCollection<Calls>();
        public ObservableCollection<Calls> ListCallSelectionSource
        {
            get { return _ListCallSelectionSource; }
            set
            {
                SetProperty(ref _ListCallSelectionSource, value);
                //CountCheck = _ListCallSelectionSource.Count;
            }
        }


        private ObservableCollection<Calls> _ListCallArticlesUnChecked = new ObservableCollection<Calls>();
        public ObservableCollection<Calls> ListCallArticlesUnChecked
        {
            get { return _ListCallArticlesUnChecked; }
            set
            {
                SetProperty(ref _ListCallArticlesUnChecked, value);
                CountUnCheck = _ListCallArticlesUnChecked.Count;
            }
        }

        private ObservableCollection<Calls> _ListCallArticlesChecked = new ObservableCollection<Calls>();
        public ObservableCollection<Calls> ListCallArticlesChecked
        {
            get { return _ListCallArticlesChecked; }
            set
            {
                SetProperty(ref _ListCallArticlesChecked, value);
                CountCheck = _ListCallArticlesChecked.Count;
            }
        }
        public void GetSubLists()
        {
            if (ListCallsAll.Count > 0)
            {
                List<Calls> tmpOriginal = new List<Calls>();

                if ((SelectedCall is Calls) && (SelectedCall.Id > 0))
                {

                    tmpOriginal = ListCallsAll.Where(x => x.ArbeitsbereichId == SelectedCall.ArbeitsbereichId & x.LiefAdrId == SelectedCall.LiefAdrId & x.EmpAdrId == SelectedCall.EmpAdrId).ToList();

                    List<Calls> tmpCallWs = ListCallsAll.Where(x => x.ArbeitsbereichId == SelectedCall.ArbeitsbereichId).ToList();
                    List<Calls> tmpCalLiefAdr = tmpCallWs.Where(x => x.LiefAdrId == SelectedCall.LiefAdrId).ToList();
                    List<Calls> tmpCalLiefEm = tmpCalLiefAdr.Where(x => x.EmpAdrId == SelectedCall.EmpAdrId).ToList();

                    int iCount = tmpCalLiefEm.Count;
                }
                else
                {
                    tmpOriginal = ListCallsAll.ToList();
                }
                ListCallSelectionSource = new ObservableCollection<Calls>(tmpOriginal.OrderBy(x => x.EintreffDatum));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task GetCallList()
        {
            try
            {
                this.IsBusy = true;
                _api_Call = new api_Call(WizardData.LoggedUser);
                var result = await _api_Call.GET_CallList_Open();
                this.IsBusy = false;

                if (result.Success)
                {
                    ListCallsAll = new ObservableCollection<Calls>(result.ListCallOpen);
                }
                else
                {
                    await MessageService.ShowAsync("ACHTUNG", result.Error);
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        private List<Calls> _SelectedCallsToAusgang = new List<Calls>();
        public List<Calls> SelectedCallsToAusgang
        {
            get { return _SelectedCallsToAusgang; }
            set
            {
                SetProperty(ref _SelectedCallsToAusgang, value);
            }
        }

        //--First Selected Call
        private Calls selectedCall = new Calls();
        public Calls SelectedCall
        {
            get { return selectedCall; }
            set
            {
                SetProperty(ref selectedCall, value);
                GetSubLists();
            }
        }

        private int countAll = 0;
        public int CountAll
        {
            get { return countAll; }
            set { SetProperty(ref countAll, value); }
        }

        private int countCheck = 0;
        public int CountCheck
        {
            get { return countCheck; }
            set { SetProperty(ref countCheck, value); }
        }

        private int countUnCheck = 0;
        public int CountUnCheck
        {
            get { return countUnCheck; }
            set { SetProperty(ref countUnCheck, value); }
        }

        //private bool _IsStoreOutStartVisible = false;
        public bool IsStoreOutStartVisible
        {
            get
            {
                bool _IsStoreOutStartVisible = false;
                //_IsStoreOutStartVisible = (CountCheck> 0) && (IsTabCheckedSelected);
                return _IsStoreOutStartVisible;
            }
            //set { SetProperty(ref _IsStoreOutStartVisible, value); }
        }
        /// <summary>
        ///             ctr_MenuToolBarItemStoreOut
        ///             Button AutomationId = "4"
        /// </summary>

        //private bool _IsArticleScanStartVisible = false;
        public bool IsArticleScanStartVisible
        {
            get
            {
                bool _IsArticleScanStartVisible = false;
                _IsArticleScanStartVisible = (CountCheck < CountAll) && (IsTabUnCheckedSelected);
                return _IsArticleScanStartVisible;
            }
            //set { SetProperty(ref _IsArticleScanStartVisible, value); }
        }
        /// <summary>
        ///             ctr_MenuToolBarItemStoreOut
        ///             Button AutomationId = "6"
        /// </summary>
        /// 
        private bool _IsCreateAusgangVisible = false;
        public bool IsCreateAusgangVisible
        {
            get
            {
                bool _IsCreateAusgangVisible = false;
                //_IsCreateAusgangVisible = (CountCheck > 0) && (IsTabCheckedSelected) && (SelectedCallsToAusgang.Count>0);
                _IsCreateAusgangVisible = (SelectedCallsToAusgang.Count > 0);
                return _IsCreateAusgangVisible;
            }
            //set { SetProperty(ref _IsCreateAusgangVisible, value); }
        }
        private bool _IsTabCheckedSelected = false;
        public bool IsTabCheckedSelected
        {
            get { return _IsTabCheckedSelected; }
            set { SetProperty(ref _IsTabCheckedSelected, value); }
        }

        private bool _IsTabUnCheckedSelected = false;
        public bool IsTabUnCheckedSelected
        {
            get { return _IsTabUnCheckedSelected; }
            set { SetProperty(ref _IsTabUnCheckedSelected, value); }
        }

        public async Task<ResponseCall> CreateStoreOutbyCall()
        {
            ResponseCall resCall = new ResponseCall();
            resCall.UserId = (int)WizardData.LoggedUser.Id;
            resCall.StoreOutArt = StoreOutArt;
            resCall.StoreOutArt_Steps = enumStoreOutArt_Steps.wizStepDoStoreOut; //   enumWizStoreOutSteps_Ausgang_Call.wizStepDoStoreOut;
            resCall.ListCallOpen = new List<Calls>();

            foreach (var item in SelectedCallsToAusgang.ToList())
            {
                resCall.ListCallForStoreOut.Add(item.Id);
            }

            if (SelectedCallsToAusgang.Count > 0)
            {
                try
                {
                    _api_Call = new api_Call(WizardData.LoggedUser);
                    var result = await _api_Call.POST_Call_CreateStoreOut(resCall).ConfigureAwait(false);
                    //resCall = result.Copy();
                    resCall = result;
                }
                catch (Exception ex)
                {
                    string str = ex.Message;
                    resCall.Error += Environment.NewLine + str;
                }
            }
            else
            {
                resCall.Error += Environment.NewLine + "Es liegen keine gescannten Abrufe vor!";
            }
            return resCall;
        }
    }
}
