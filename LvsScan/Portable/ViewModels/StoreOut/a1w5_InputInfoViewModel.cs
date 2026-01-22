using Common.ApiModels;
using Common.Enumerations;
using Common.Helper;
using Common.Models;
using LvsScan.Portable.Services;
using System;
using System.Threading.Tasks;

namespace LvsScan.Portable.ViewModels.StoreOut
{
    public class a1w5_InputInfoViewModel : BaseViewModel
    {
        public a1w5_InputInfoViewModel()
        {
            init();
        }
        private void init()
        {
            IsBusy = false;
            IsInputInfoEnabled = false;
            IsBtnInfoEnabled = true;
        }

        public enumStoreOutArt_Steps CurrentStep = enumStoreOutArt_Steps.wizStepInputInfo;
        public enumStoreOutArt StoreOutArt = enumStoreOutArt.open;

        private Ausgaenge selectedAusgang = new Ausgaenge();
        public Ausgaenge SelectedAusgang
        {
            get { return selectedAusgang; }
            set
            {
                SetProperty(ref selectedAusgang, value);
                if (selectedAusgang.Id > 0)
                {
                    AusgangIdString = "Ausgang ID: " + selectedAusgang.LAusgangID.ToString() + " - [" + selectedAusgang.Id.ToString() + "]";
                    AusgangDateString = "vom " + selectedAusgang.Datum.ToString("dd.MM.yyyy");
                    BackgroundColorHead = selectedAusgang.ViewBackgroundcolor;
                    WorkspaceString = "[Arbeitsbereich: " + selectedAusgang.Workspace.WorkspaceString + "]";
                    PrintDocumentStoreOutStatus_Frachtbrief = SelectedAusgang.PrintDocumentStoreOutStatus_Frachtbrief;
                    PrintDocumentStoreOutStatus_Lfs = SelectedAusgang.PrintDocumentStoreOutStatus_Lfs;
                    PrintDocumentStoreOutStatus_List = SelectedAusgang.PrintDocumentStoreOutStatus_List;

                    InfoString = selectedAusgang.Info;
                    Lagertransport = selectedAusgang.LagerTransport;
                    IsStepFinished = true;
                }
            }
        }

        private enumPrintDocumentStoreOutStatus_Frachtbrief printDocumentStoreOutStatus_Frachtbrief = enumPrintDocumentStoreOutStatus_Frachtbrief.NotSet;
        public enumPrintDocumentStoreOutStatus_Frachtbrief PrintDocumentStoreOutStatus_Frachtbrief
        {
            get { return printDocumentStoreOutStatus_Frachtbrief; }
            set { SetProperty(ref printDocumentStoreOutStatus_Frachtbrief, value); }
        }
        private enumPrintDocumentStoreOutStatus_Lfs printDocumentStoreOutStatus_Lfs = enumPrintDocumentStoreOutStatus_Lfs.NotSet;
        public enumPrintDocumentStoreOutStatus_Lfs PrintDocumentStoreOutStatus_Lfs
        {
            get { return printDocumentStoreOutStatus_Lfs; }
            set { SetProperty(ref printDocumentStoreOutStatus_Lfs, value); }
        }
        private enumPrintDocumentStoreOutStatus_List printDocumentStoreOutStatus_List = enumPrintDocumentStoreOutStatus_List.NotSet;
        public enumPrintDocumentStoreOutStatus_List PrintDocumentStoreOutStatus_List
        {
            get { return printDocumentStoreOutStatus_List; }
            set { SetProperty(ref printDocumentStoreOutStatus_List, value); }
        }

        private bool _IsStepFinished = false;
        public bool IsStepFinished
        {
            get { return _IsStepFinished; }
            set
            {
                SetProperty(ref _IsStepFinished, value);
                IsBaseNextEnabeld = !_IsStepFinished;
            }
        }
        private string ausgangIdString = string.Empty;
        public string AusgangIdString
        {
            get { return ausgangIdString; }
            set { SetProperty(ref ausgangIdString, value); }
        }

        private string ausgangDateString = string.Empty;
        public string AusgangDateString
        {
            get { return ausgangDateString; }
            set { SetProperty(ref ausgangDateString, value); }
        }

        private string _WorkspaceString = string.Empty;
        public string WorkspaceString
        {
            get { return _WorkspaceString; }
            set { SetProperty(ref _WorkspaceString, value); }
        }

        private System.Drawing.Color backgroundColorHead = System.Drawing.Color.WhiteSmoke;
        public System.Drawing.Color BackgroundColorHead
        {
            get { return backgroundColorHead; }
            set { SetProperty(ref backgroundColorHead, value); }
        }
        private Workspaces workspace = new Workspaces();
        public Workspaces Workspace
        {
            get { return workspace; }
            set { SetProperty(ref workspace, value); }
        }
        private Ausgaenge _AusgangOriginal = new Ausgaenge();
        public Ausgaenge AusgangOriginal
        {
            get { return _AusgangOriginal; }
            set { SetProperty(ref _AusgangOriginal, value); }
        }


        //--------------------------------------------------------------------------------------------------------
        /// <summary>
        ///             info edit
        /// </summary>
        private string _InfoString = string.Empty;
        public string InfoString
        {
            get { return _InfoString; }
            set
            {
                SetProperty(ref _InfoString, value);
                if (!SelectedAusgang.Info.Equals(_InfoString))
                {
                    Ausgaenge tmpA = SelectedAusgang.Copy();
                    tmpA.Info = _InfoString;
                    SelectedAusgang = tmpA;
                }
            }
        }
        private bool _IsBtnInfoEnabled = false;
        public bool IsBtnInfoEnabled
        {
            get { return _IsBtnInfoEnabled; }
            set { SetProperty(ref _IsBtnInfoEnabled, value); }
        }
        private bool _IsInputInfoEnabled = true;
        public bool IsInputInfoEnabled
        {
            get { return _IsInputInfoEnabled; }
            set
            {
                SetProperty(ref _IsInputInfoEnabled, value);
                BackgroundColorInputInfo = ValueToColorConverter.ButtonBackgroundColor(_IsInputInfoEnabled);
            }
        }
        private System.Drawing.Color _BackgroundColorInputInfo = ValueToColorConverter.ButtonBackgroundColor(false);
        public System.Drawing.Color BackgroundColorInputInfo
        {
            get { return _BackgroundColorInputInfo; }
            set { SetProperty(ref _BackgroundColorInputInfo, value); }
        }


        //--------------------------------------------------------------------------------------------------------
        /// <summary>
        ///             LAgertransport Chekbox
        /// </summary>
        private bool _Lagertransport = false;
        public bool Lagertransport
        {
            get { return _Lagertransport; }
            set
            {
                SetProperty(ref _Lagertransport, value);
                if (!selectedAusgang.LagerTransport.Equals(_Lagertransport))
                {
                    Ausgaenge tmpAusgang = SelectedAusgang.Copy();
                    tmpAusgang.LagerTransport = _Lagertransport;
                    SelectedAusgang = tmpAusgang.Copy();
                }
            }
        }
        public async Task<ResponseAusgang> UpdateAusgang()
        {

            //this.IsBusy = true;
            api_Ausgang _apiAusgang = new api_Ausgang(WizardData.LoggedUser);
            ResponseAusgang resAusgang = new ResponseAusgang();
            //resAusgang.WizStoreOut_Ausgang = enumWizStoreOut_Ausgang.wizStepThree;
            resAusgang.StoreOutArt = WizardData.Wiz_StoreOut.StoreOutArt;
            resAusgang.StoreOutArt_Steps = CurrentStep;
            resAusgang.Ausgang = SelectedAusgang.Copy();
            resAusgang.UserId = (int)WizardData.LoggedUser.Id;
            try
            {
                var result = await _apiAusgang.POST_Ausgang_Update_WizStoreOut(resAusgang);
                resAusgang.Success = result.Success;
                if (resAusgang.Success)
                {
                    SelectedAusgang = result.Ausgang.Copy();
                }
                //this.IsBusy = false;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            return resAusgang;
        }
    }
}
