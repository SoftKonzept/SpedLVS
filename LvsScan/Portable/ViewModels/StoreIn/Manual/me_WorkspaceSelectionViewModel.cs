using Common.ApiModels;
using Common.Enumerations;
using Common.Models;
using LvsScan.Portable.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LvsScan.Portable.ViewModels.StoreIn.Manual
{
    public class me_WorkspaceSelectionViewModel : BaseViewModel
    {
        public me_WorkspaceSelectionViewModel()
        {
            Init();
        }
        public me_WorkspaceSelectionViewModel(enumStoreInArt myStoreInArt)
        {
            StoreInArt = myStoreInArt;
            Init();
        }

        public void Init()
        {
            IsBusy = false;
        }

        private bool loadValues = false;
        public bool LoadValues
        {
            get { return loadValues; }
            set
            {
                loadValues = value;
                OnPropertyChanged();
                if (loadValues)
                {
                    Task.Run(() => GetWorkspaceList()).Wait();
                }
            }
        }

        public async Task GetWorkspaceList()
        {
            try
            {
                this.IsBusy = true;
                api_Workspace _api = new api_Workspace();
                var result = await _api.GET_Workspacelist();
                if (result.Success)
                {
                    WorkspaceList = new ObservableCollection<Workspaces>(result.ListWorkspaces);
                }
                this.IsBusy = false;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                await MessageService.ShowAsync("ACHTUNG", str);
            }
        }

        //private enumStoreInArt _StoreInArt = enumStoreInArt.NotSet;
        private enumStoreInArt _StoreInArt = enumStoreInArt.manually;
        public enumStoreInArt StoreInArt
        {
            get { return _StoreInArt; }
            set { SetProperty(ref _StoreInArt, value); }
        }

        private ObservableCollection<Workspaces> _WorkspaceList = new ObservableCollection<Workspaces>();
        public ObservableCollection<Workspaces> WorkspaceList
        {
            get { return _WorkspaceList; }
            set { SetProperty(ref _WorkspaceList, value); }
        }


        private Workspaces _SelectedWorkspace = new Workspaces();
        public Workspaces SelectedWorkspace
        {
            get { return _SelectedWorkspace; }
            set
            {
                SetProperty(ref _SelectedWorkspace, value);
                if (_SelectedWorkspace.Id > 0)
                {
                    //InitEingang();
                    //init new Eingang
                    EingangToCreate = new Eingaenge();
                    EingangToCreate.Workspace = _SelectedWorkspace.Copy();
                    EingangToCreate.Eingangsdatum = DateTime.Now;

                    //DEfault Values
                    EingangToCreate.Auftraggeber = _SelectedWorkspace.WorkspaceOwner;
                    EingangToCreate.Empfaenger = _SelectedWorkspace.WorkspaceOwner;
                    if (_SelectedWorkspace.EingangDefEntladeId > 0)
                    {
                        EingangToCreate.Empfaenger = _SelectedWorkspace.EingangDefEmpfaengerId;
                    }
                    EingangToCreate.EntladeID = _SelectedWorkspace.EingangDefEntladeId;
                    EingangToCreate.SpedId = 0;
                    EingangToCreate.CreatedByScanner = true;
                }
            }
        }

        private Eingaenge _EingangToCreate = new Eingaenge();
        public Eingaenge EingangToCreate
        {
            get { return _EingangToCreate; }
            set { SetProperty(ref _EingangToCreate, value); }
        }

        public void InitEingang()
        {
            EingangToCreate = new Eingaenge();
        }

        //private Eingaenge _SelectedEingang = new Eingaenge();
        //public Eingaenge SelectedEingang
        //{
        //    get { return _SelectedEingang; }
        //    set
        //    {
        //        SetProperty(ref _SelectedEingang, value);
        //    }
        //}

        private System.Drawing.Color backgroundColorHead = System.Drawing.Color.WhiteSmoke;
        public System.Drawing.Color BackgroundColorHead
        {
            get { return backgroundColorHead; }
            set { SetProperty(ref backgroundColorHead, value); }
        }
        private string _EingangIdString = string.Empty;
        public string EingangIdString
        {
            get { return _EingangIdString; }
            set { SetProperty(ref _EingangIdString, value); }
        }

        private string _EingangDateString = string.Empty;
        public string EingangDateString
        {
            get { return _EingangDateString; }
            set { SetProperty(ref _EingangDateString, value); }
        }
        private string _WorkspaceString = string.Empty;
        public string WorkspaceString
        {
            get { return _WorkspaceString; }
            set { SetProperty(ref _WorkspaceString, value); }
        }

        private string clientString = string.Empty;
        public string ClientString
        {
            get { return clientString; }
            set { SetProperty(ref clientString, value); }
        }



        private List<Articles> articlesInEingang = new List<Articles>();
        public List<Articles> ArticlesInEingang
        {
            get { return articlesInEingang; }
            set
            {
                SetProperty(ref articlesInEingang, value);
                CountAll = articlesInEingang.Count;
                if (articlesInEingang != null)
                {
                    this.WizardData.Wiz_StoreIn.ArticleInEingang = articlesInEingang.ToList();
                }
                GetSubLists();
            }
        }

        private List<Articles> _ArticlesChecked = new List<Articles>();
        public List<Articles> ArticlesChecked
        {
            get { return _ArticlesChecked; }
            set { SetProperty(ref _ArticlesChecked, value); }
        }
        private List<Articles> _ArticlesUnChecked = new List<Articles>();
        public List<Articles> ArticlesUnChecked
        {
            get { return _ArticlesUnChecked; }
            set { SetProperty(ref _ArticlesUnChecked, value); }
        }
        private void GetSubLists()
        {
            List<Articles> tmpOriginal = ArticlesInEingang.ToList();
            var tmpListChecked = tmpOriginal.Where(x => x.EingangChecked == true).ToList();
            var tmpListUnChecked = tmpOriginal.Where(x => x.EingangChecked == false).ToList();

            ArticlesChecked = new List<Articles>(tmpListChecked);
            ArticlesUnChecked = new List<Articles>(tmpListUnChecked);
        }

        private int _CountAll;
        public int CountAll
        {
            get { return _CountAll; }
            set { SetProperty(ref _CountAll, value); }
        }

        private int _CountCheck;
        public int CountCheck
        {
            get { return _CountCheck; }
            set { SetProperty(ref _CountCheck, value); }
        }

        private int _CountUnCheck;
        public int CountUnCheck
        {
            get { return _CountUnCheck; }
            set { SetProperty(ref _CountUnCheck, value); }
        }

        public async Task<ResponseEingang> CreateEingang()
        {
            IsBusy = true;

            ResponseEingang resEA = new ResponseEingang();
            resEA.Eingang = EingangToCreate.Copy();
            resEA.UserId = (int)WizardData.LoggedUser.Id;
            resEA.StoreInArt = WizardData.Wiz_StoreIn.StoreInArt;
            resEA.StoreInArt_Steps = enumStoreInArt_Steps.NotSet;

            api_Eingang _api = new api_Eingang(WizardData.LoggedUser);
            try
            {
                var result = await _api.POST_Eingang_Add(resEA);
                resEA = result.Copy();
                if (result.Success)
                {
                    EingangToCreate = result.Eingang.Copy();
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            IsBusy = false;
            return resEA;
        }

    }
}
