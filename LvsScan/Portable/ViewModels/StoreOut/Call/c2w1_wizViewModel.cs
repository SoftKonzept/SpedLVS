using Common.ApiModels;
using Common.Enumerations;
using Common.Helper;
using Common.Models;
using LvsScan.Portable.Models;
using LvsScan.Portable.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Telerik.XamarinForms.Primitives;

namespace LvsScan.Portable.ViewModels.StoreOut.Call
{
    public class c2w1_wizViewModel : BaseViewModel
    {
        public c2w1_wizViewModel()
        {
            //--- button not activ
            //IsBaseNextEnabeld = true;
            BackgroundColorSearchLvsNr = ValueToColorConverter.BooleanConvert(false);
            BackgroundColorSearchProductionNo = ValueToColorConverter.BooleanConvert(false);
        }

        private bool loadValues = false;
        public bool LoadValues
        {
            get { return loadValues; }
            set
            {
                loadValues = value;
                OnPropertyChanged();
            }
        }

        public enumStoreOutArt StoreOutArt = enumStoreOutArt.call;
        public enumStoreOutArt_Steps CurrentStoreOutSpep = enumStoreOutArt_Steps.wizStepDoStoreOut;

        private ObservableCollection<Calls> _CallArticles = new ObservableCollection<Calls>();
        public ObservableCollection<Calls> CallArticles
        {
            get { return _CallArticles; }
            set
            {
                SetProperty(ref _CallArticles, value);
                CountAll = _CallArticles.Count;
                GetSubLists();
            }
        }


        private void GetSubLists()
        {
            List<Calls> tmpOriginal = CallArticles.ToList();
            //CallArticlesChecked = new ObservableCollection<Calls>(tmpOriginal.Where(x => x.ScanCheckForStoreOut == true).ToList());
            //CallArticlesUnChecked = new ObservableCollection<Calls>(tmpOriginal.Where(x => x.ScanCheckForStoreOut <= new DateTime(1900, 1, 1)).ToList());
            if (WorkspaceForStoreOut.Id == 0)
            {
                CallArticlesUnChecked = new ObservableCollection<Calls>(tmpOriginal.Where(x => x.ScanCheckForStoreOut <= new DateTime(1900, 1, 1)).ToList());
            }
            else
            {

                List<Calls> tmpCallWorkspace = tmpOriginal.Where(x => x.ScanCheckForStoreOut <= new DateTime(1900, 1, 1)).ToList();
                //CallArticlesUnChecked = new ObservableCollection<Calls>(tmpCallWorkspace.Where(x => x.Workspace.Id == WorkspaceForStoreOut.Id & x.LiefAdrId == SelectedCallArticle.LiefAdrId).ToList());
                CallArticlesUnChecked = new ObservableCollection<Calls>(tmpCallWorkspace.Where(x => x.Workspace.Id == WorkspaceForStoreOut.Id).ToList());
            }

            if (CallArticlesChecked.Count > 0)
            {
                foreach (Calls call in CallArticlesChecked)
                {
                    if (CallArticlesUnChecked.Contains(call))
                    {
                        CallArticlesUnChecked.Remove(call);
                    }
                }
            }
        }

        private ObservableCollection<Calls> _CallArticlesUnChecked = new ObservableCollection<Calls>();
        public ObservableCollection<Calls> CallArticlesUnChecked
        {
            get { return _CallArticlesUnChecked; }
            set
            {
                SetProperty(ref _CallArticlesUnChecked, value);
                CountUnCheck = _CallArticlesUnChecked.Count;
            }
        }

        private ObservableCollection<Calls> _CallArticlesChecked = new ObservableCollection<Calls>();
        public ObservableCollection<Calls> CallArticlesChecked
        {
            get { return _CallArticlesChecked; }
            set
            {
                SetProperty(ref _CallArticlesChecked, value);
                CountCheck = _CallArticlesChecked.Count;
            }
        }

        private int countAll;
        public int CountAll
        {
            get { return countAll; }
            set { SetProperty(ref countAll, value); }
        }

        private int countUnCheck;
        public int CountUnCheck
        {
            get { return countUnCheck; }
            set
            {
                SetProperty(ref countUnCheck, value);

            }
        }

        private int countCheck;
        public int CountCheck
        {
            get { return countCheck; }
            set
            {
                SetProperty(ref countCheck, value);
                if (countCheck == 0)
                {
                    WorkspaceForStoreOut = new Workspaces();
                }
                else
                {
                    WorkspaceForStoreOut = CallArticlesChecked[0].Workspace.Copy();
                }
                //GetSubLists();
                IsBaseNextEnabeld = !(countCheck > 0);
            }
        }
        private bool isStoreOutStartVisible = false;
        public bool IsStoreOutStartVisible
        {
            get { return isStoreOutStartVisible; }
            set { SetProperty(ref isStoreOutStartVisible, value); }
        }
        private bool isArticleScanStartVisible = false;
        public bool IsArticleScanStartVisible
        {
            get { return isArticleScanStartVisible; }
            set { SetProperty(ref isArticleScanStartVisible, value); }
        }



        private TabViewItem selectedTabViewItem;
        public TabViewItem SelectedTabViewItem
        {
            get { return selectedTabViewItem; }
            set
            {
                selectedTabViewItem = value;
                if ((selectedTabViewItem != null) && (!selectedTabViewItem.HeaderText.ToUpper().Equals("SCAN")))
                {
                    IsManual = true;
                }
                else
                {
                    IsManual = false;
                }
            }
        }

        private int _positionCarouselView = 0;
        public int PositionCarouselView
        {
            get { return _positionCarouselView; }
            set
            {
                if ((value > CallArticlesUnChecked.Count - 1) || (value < 0))
                {
                    SetProperty(ref _positionCarouselView, 0);
                }
                else
                {
                    SetProperty(ref _positionCarouselView, value);
                }
            }
        }

        private int _positionCarouselViewChecked = 0;
        public int PositionCarouselViewChecked
        {
            get { return _positionCarouselViewChecked; }
            set
            {
                if ((value > CallArticlesChecked.Count - 1) || (value < 0))
                {
                    SetProperty(ref _positionCarouselViewChecked, 0);
                }
                else
                {
                    SetProperty(ref _positionCarouselViewChecked, value);
                }
            }
        }

        private bool isManual;
        public bool IsManual
        {
            get { return isManual; }
            set { isManual = value; }
        }

        private System.Drawing.Color _backgroundColorSearchLvsNr;
        public System.Drawing.Color BackgroundColorSearchLvsNr
        {
            get { return _backgroundColorSearchLvsNr; }
            set { SetProperty(ref _backgroundColorSearchLvsNr, value); }
        }

        private System.Drawing.Color _backgroundColorSearchProductionNo;
        public System.Drawing.Color BackgroundColorSearchProductionNo
        {
            get { return _backgroundColorSearchProductionNo; }
            set { SetProperty(ref _backgroundColorSearchProductionNo, value); }
        }

        private Calls _selectedCallArticle = new Calls();
        public Calls SelectedCallArticle
        {
            get { return _selectedCallArticle; }
            set { SetProperty(ref _selectedCallArticle, value); }
        }

        private string _searchLvsNo;
        public string SearchLvsNo
        {
            get { return _searchLvsNo; }
            set
            {
                SetProperty(ref _searchLvsNo, value);

                int iLvsNr = 0;
                int.TryParse(_searchLvsNo, out iLvsNr);

                if (iLvsNr > 0)
                {
                    List<Calls> listArt = CallArticlesUnChecked.ToList();
                    List<Calls> TempSearchResultList = listArt.Where(x => x.LVSNr == iLvsNr).ToList();
                    ExistLVSNr = (TempSearchResultList.Count > 0);
                    if (ExistLVSNr)
                    {
                        SelectedCallArticle = TempSearchResultList.FirstOrDefault(x => x.LVSNr == iLvsNr);
                    }
                }
                else
                {
                    ExistLVSNr = false;
                }
            }
        }

        private List<Calls> _TempSearchResultList = new List<Calls>();
        public List<Calls> TempSearchResultList
        {
            get { return _TempSearchResultList; }
            set { SetProperty(ref _TempSearchResultList, value); }
        }

        private string _searchProduktionsnummer = String.Empty;
        public string SearchProduktionsnummer
        {
            get { return _searchProduktionsnummer; }
            set
            {
                SetProperty(ref _searchProduktionsnummer, value.ToUpper());
                ExistProductionNo = false;
                if ((_searchProduktionsnummer != null) && (_searchProduktionsnummer.Length > 0))
                {
                    if ((SelectedCallArticle != null) && (SelectedCallArticle.Id > 0))
                    {
                        if (_searchProduktionsnummer.Length > 7)
                        {
                            ExistProductionNo = _searchProduktionsnummer.ToUpper().Contains(SelectedCallArticle.Produktionsnummer.ToUpper());
                        }
                        else if (_searchProduktionsnummer == SelectedCallArticle.Produktionsnummer)
                        {
                            ExistProductionNo = (_searchProduktionsnummer == SelectedCallArticle.Produktionsnummer);
                        }
                    }

                }
            }
        }

        private bool _existLVSNr = false;
        public bool ExistLVSNr
        {
            get { return _existLVSNr; }
            set
            {
                SetProperty(ref _existLVSNr, value);
                //IsBaseNextEnabeld = !(_existLVSNr && ExistProductionNo);
                BackgroundColorSearchLvsNr = ValueToColorConverter.BooleanConvert(_existLVSNr);
            }
        }

        private bool _existProductionNo = false;
        public bool ExistProductionNo
        {
            get { return _existProductionNo; }
            set
            {
                SetProperty(ref _existProductionNo, value);
                //IsBaseNextEnabeld = !(_existProductionNo && ExistLVSNr);
                BackgroundColorSearchProductionNo = ValueToColorConverter.BooleanConvert(_existProductionNo);
            }
        }
        //public ResponseCall CreateStoreOut()
        public async Task<ResponseCall> CreateStoreOut(List<Calls> myList)
        {
            api_Call apiCall = new api_Call(WizardData.LoggedUser);

            ResponseCall resCall = new ResponseCall();
            resCall.UserId = (int)WizardData.LoggedUser.Id;
            resCall.StoreOutArt = StoreOutArt;
            resCall.AppProcess = enumAppProcess.StoreOut;

            //--- check noch notwendig
            resCall.StoreOutArt_Steps = CurrentStoreOutSpep;
            resCall.ListCallForStoreOut = new List<int>();

            foreach (var item in myList)
            {
                resCall.ListCallForStoreOut.Add(item.Id);
            }
            if (resCall.ListCallForStoreOut.Count > 0)
            {
                var result = await apiCall.POST_Call_CreateStoreOut(resCall);
                //resCall = result.Copy();
                resCall = result;
            }
            return resCall;
        }


        private Workspaces _WorkspaceForStoreOut = new Workspaces();
        public Workspaces WorkspaceForStoreOut
        {
            get { return _WorkspaceForStoreOut; }
            set
            {
                SetProperty(ref _WorkspaceForStoreOut, value);
            }
        }
    }
}
