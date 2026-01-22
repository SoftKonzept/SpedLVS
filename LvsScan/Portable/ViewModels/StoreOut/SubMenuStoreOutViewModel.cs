using Common.ApiModels;
using Common.Enumerations;
using Common.Helper;
using Common.Models;
using LvsScan.Portable.Services;
using LvsScan.Portable.ViewModels.Wizard;
using LvsScan.Portable.Views.Menu;
using LvsScan.Portable.Views.StoreOut.Call;
using LvsScan.Portable.Views.StoreOut.Open;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LvsScan.Portable.ViewModels.StoreOut
{
    public class SubMenuStoreOutViewModel : BaseViewModel
    {
        /// <summary>
        ///            Submenü StoreOut 
        /// </summary>
        public SubMenuStoreOutViewModel()
        {
            string str = string.Empty;

            SubMenuItems = new ObservableCollection<MenuSubItem>(new[]
                {
                    //new MenuSubItem
                    //{
                    //    Id = 0,
                    //    Title = "manueller Ausgang",
                    //    SubText = "Erstellung von manuellen Ausgängen",
                    //    TargetType= typeof(ManuallyStart),
                    //    ArtMainMenu = Enumerations.enumMainMenu.StoreOut
                    //},
                    new MenuSubItem
                    {
                        Id = 1,
                        Title = "offene Ausgänge" ,
                        SubText = "Bearbeitung von bestehenden, offenen Ausgängen",
                        TargetType=typeof(oa1_OpenStoreOutListPage),
                        ArtMainMenu = Enumerations.enumMainMenu.StoreOut,
                        ShowButton = true,
                        BackgroundColor = ValueToColorConverter.ViewBackgroundColorSubmenu()

                    },
                    new MenuSubItem
                    {
                        Id = 2,
                        Title = "Abrufe",
                        SubText = "Erstellen von Ausgängen aufgrund von aktuellen Abrufen",
                        TargetType=typeof(c1_CallArticleListPage),
                        ArtMainMenu = Enumerations.enumMainMenu.StoreOut,
                        ShowButton = true,
                        BackgroundColor = ValueToColorConverter.ViewBackgroundColorSubmenu()
                    },
                }); ;
        }

        public ObservableCollection<MenuSubItem> SubMenuItems { get; set; }

        private enumAppProcess _AppProcess = enumAppProcess.NotSet;
        internal wiz_ScanSearchArticle_Helper helper;
        public enumAppProcess AppProcess
        {
            get { return _AppProcess; }
            set
            {
                SetProperty(ref _AppProcess, value);
                helper = new wiz_ScanSearchArticle_Helper(_AppProcess, StoreInArt, StoreOutArt);
                IsLvsInputVisible = helper.IsLvsInputVisible;
                //IsProductionnoInputVisible = helper.IsProductionnoInputVisible;
            }
        }
        private enumStoreOutArt _StoreOutArt = enumStoreOutArt.NotSet;
        public enumStoreOutArt StoreOutArt
        {
            get { return _StoreOutArt; }
            set
            {
                SetProperty(ref _StoreOutArt, value);
                helper = new wiz_ScanSearchArticle_Helper(AppProcess, StoreInArt, _StoreOutArt);
            }
        }
        private enumStoreInArt _StoreInArt = enumStoreInArt.NotSet;
        public enumStoreInArt StoreInArt
        {
            get { return _StoreInArt; }
            set
            {
                SetProperty(ref _StoreInArt, value);
                helper = new wiz_ScanSearchArticle_Helper(AppProcess, _StoreInArt, StoreOutArt);
            }
        }

        private string _searchLvsNo;
        public string SearchLvsNo
        {
            get { return _searchLvsNo; }
            set
            {
                SetProperty(ref _searchLvsNo, value);
            }
        }


        private Ausgaenge _AusgangToSearch = new Ausgaenge();
        public Ausgaenge AusgangToSearch
        {
            get { return _AusgangToSearch; }
            set
            {
                SetProperty(ref _AusgangToSearch, value);
                ExistLVSNr = (_AusgangToSearch.Id > 0);
            }
        }

        private Calls _CallToSearch = new Calls();
        public Calls CallToSearch
        {
            get { return _CallToSearch; }
            set
            {
                SetProperty(ref _CallToSearch, value);
                ExistLVSNr = (_CallToSearch.LVSNr > 0);
            }
        }

        private bool _IsLvsInputVisible = true;
        public bool IsLvsInputVisible
        {
            get { return _IsLvsInputVisible; }
            set { SetProperty(ref _IsLvsInputVisible, value); }
        }

        private bool _IsManual;
        public bool IsManual
        {
            get { return _IsManual; }
            set { SetProperty(ref _IsManual, value); }
        }

        private bool _existLVSNr = false;
        public bool ExistLVSNr
        {
            get { return _existLVSNr; }
            set
            {
                SetProperty(ref _existLVSNr, value);
                //IsBaseNextEnabeld = helper.IsBaseNextEnabeld(_existLVSNr, ExistProductionNo);
                BackgroundColorSearchLvsNr = ValueToColorConverter.BooleanConvert(_existLVSNr);
            }
        }
        public bool IsBaseNextEnabeld(bool myExistLvsNr, bool myExistProductionNo)
        {
            bool bReturn = false;
            switch (AppProcess)
            {
                case enumAppProcess.StoreIn:
                    //switch (StoreInArt)
                    //{
                    //    case enumStoreInArt.open:
                    //    case enumStoreInArt.edi:
                    //        //if ((!IsLvsInputVisible) && (IsProductionnoInputVisible))
                    //        //{
                    //        //    //Beispiel Einlagerung, da hat man nur die Produktionsnummer
                    //        //    bReturn = !(myExistProductionNo);
                    //        //}
                    //        if (!IsLvsInputVisible)
                    //        {
                    //            //Beispiel Einlagerung, da hat man nur die Produktionsnummer
                    //            bReturn = !(myExistProductionNo);
                    //        }
                    //        break;
                    //}
                    break;
                case enumAppProcess.StoreOut:
                    //if ((IsLvsInputVisible) && (IsProductionnoInputVisible))
                    //{
                    //    bReturn = !(myExistLvsNr && myExistProductionNo);
                    //}
                    if (IsLvsInputVisible)
                    {
                        bReturn = !(myExistLvsNr && myExistProductionNo);
                    }
                    break;
                case enumAppProcess.StoreLocationChange:
                case enumAppProcess.Inventory:
                    //if ((IsLvsInputVisible) && (!IsProductionnoInputVisible))
                    //{
                    //    //Beipiel Umlagerung
                    //    bReturn = !(myExistLvsNr);
                    //}
                    break;

            }



            return bReturn;
        }
        private System.Drawing.Color _BackgroundColorSearchLvsNr;
        public System.Drawing.Color BackgroundColorSearchLvsNr
        {
            get { return _BackgroundColorSearchLvsNr; }
            set { SetProperty(ref _BackgroundColorSearchLvsNr, value); }
        }


        public async Task<ResponseAusgang> CheckAusgangByLvs()
        {
            IsBusy = true;
            api_Ausgang _apiAusgang = new api_Ausgang(WizardData.LoggedUser);
            ResponseAusgang resAusgang = new ResponseAusgang();
            //hier in dieser Abfrage OPEN
            resAusgang.StoreOutArt = enumStoreOutArt.open;
            //resAusgang.StoreOutArt_Steps = CurrentStep;
            resAusgang.UserId = (int)WizardData.LoggedUser.Id;
            try
            {
                int iLvsNo = 0;
                int.TryParse(SearchLvsNo, out iLvsNo);
                if (iLvsNo > 0)
                {
                    var result = await _apiAusgang.GET_Ausgang_ByLvsNr(iLvsNo);
                    resAusgang.Success = result.Success;
                    if (result.Success)
                    {
                        AusgangToSearch = result.Ausgang.Copy();
                    }
                }
                //
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            this.IsBusy = false;
            return resAusgang;
        }

        public async Task<ResponseCall> CheckCallByLvs()
        {
            IsBusy = true;

            api_Call _apiCall = new api_Call(WizardData.LoggedUser);
            ResponseCall resCall = new ResponseCall();
            //hier in dieser Abfrage CALL
            resCall.StoreOutArt = enumStoreOutArt.call;
            resCall.UserId = (int)WizardData.LoggedUser.Id;

            try
            {
                int iLvsNo = 0;
                int.TryParse(SearchLvsNo, out iLvsNo);
                if (iLvsNo > 0)
                {
                    var result = await _apiCall.GET_Call_byLVSNr(iLvsNo);
                    resCall = result;
                    if (resCall.Success)
                    {
                        CallToSearch = (Calls)resCall.ListCallOpen[0];
                    }
                }
                //
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            this.IsBusy = false;
            return resCall;
        }


    }
}
