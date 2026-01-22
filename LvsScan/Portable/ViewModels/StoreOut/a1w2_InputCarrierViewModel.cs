using Common.ApiModels;
using Common.Enumerations;
using Common.Helper;
using Common.Models;
using LvsScan.Portable.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LvsScan.Portable.ViewModels.StoreOut
{
    public class a1w2_InputCarrierViewModel : BaseViewModel
    {
        public a1w2_InputCarrierViewModel()
        {
            init();
        }

        private void init()
        {
            IsBusy = false;

            IsBtnCarrierEnabled = true;
            IsInputCarrierEnabled = false;
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

        public enumStoreOutArt_Steps CurrentStep = enumStoreOutArt_Steps.wizStepInputCarrier;
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
                    WorkspaceString = "Arbeitsbereich: " + selectedAusgang.Workspace.WorkspaceString;
                    PrintDocumentStoreOutStatus_Frachtbrief = SelectedAusgang.PrintDocumentStoreOutStatus_Frachtbrief;
                    PrintDocumentStoreOutStatus_Lfs = SelectedAusgang.PrintDocumentStoreOutStatus_Lfs;
                    PrintDocumentStoreOutStatus_List = SelectedAusgang.PrintDocumentStoreOutStatus_List;

                    CarriertString = selectedAusgang.Spediteur;

                    if (selectedAusgang.Workspace is Workspaces)
                    {
                        Workspace = selectedAusgang.Workspace.Copy();
                    }
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

        private Ausgaenge _AusgangOriginal = new Ausgaenge();
        public Ausgaenge AusgangOriginal
        {
            get { return _AusgangOriginal; }
            set { SetProperty(ref _AusgangOriginal, value); }
        }
        ///------------------------------------------------------------------------------------------------------
        /// <summary>
        ///                         Head 
        ///                         Values for store out information
        /// </summary>
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

        ///------------------------------------------------------------------------------------------------------
        /// <summary>
        ///                 Carrier Data
        /// </summary>

        private string _CarriertString = string.Empty;
        public string CarriertString
        {
            get { return _CarriertString; }
            set { SetProperty(ref _CarriertString, value); }
        }
        private System.Drawing.Color _BackgroundColorInputCarrier = ValueToColorConverter.ButtonBackgroundColor(false);
        public System.Drawing.Color BackgroundColorInputCarrier
        {
            get { return _BackgroundColorInputCarrier; }
            set { SetProperty(ref _BackgroundColorInputCarrier, value); }
        }
        private ObservableCollection<Addresses> _ListAddressesCarrier = new ObservableCollection<Addresses>();
        public ObservableCollection<Addresses> ListAddressesCarrier
        {
            get { return _ListAddressesCarrier; }
            set { SetProperty(ref _ListAddressesCarrier, value); }
        }
        private Addresses _PickerSelectedItemCarrierAddress;
        public Addresses PickerSelectedItemCarrierAddress
        {
            get { return _PickerSelectedItemCarrierAddress; }
            set
            {
                SetProperty(ref _PickerSelectedItemCarrierAddress, value);
                if (_PickerSelectedItemCarrierAddress.Id > 0)
                {
                    Ausgaenge tmpAusgang = SelectedAusgang.Copy();
                    if (tmpAusgang.Id > 0)
                    {
                        tmpAusgang.Spediteur = _PickerSelectedItemCarrierAddress.Name1 + " - " +
                                                             _PickerSelectedItemCarrierAddress.ZIP + " " +
                                                             _PickerSelectedItemCarrierAddress.City;
                        tmpAusgang.SpedId = _PickerSelectedItemCarrierAddress.Id;
                    }
                    SelectedAusgang = tmpAusgang.Copy();
                    IsInputCarrierEnabled = !IsInputCarrierEnabled;
                }
            }
        }
        private bool _IsBtnCarrierEnabled = false;
        public bool IsBtnCarrierEnabled
        {
            get { return _IsBtnCarrierEnabled; }
            set { SetProperty(ref _IsBtnCarrierEnabled, value); }
        }
        private bool _IsInputCarrierEnabled = true;
        public bool IsInputCarrierEnabled
        {
            get { return _IsInputCarrierEnabled; }
            set
            {
                SetProperty(ref _IsInputCarrierEnabled, value);
                BackgroundColorInputCarrier = ValueToColorConverter.ButtonBackgroundColor(_IsInputCarrierEnabled);
            }
        }

        private Workspaces workspace = new Workspaces();
        public Workspaces Workspace
        {
            get { return workspace; }
            set { SetProperty(ref workspace, value); }
        }

        public async Task GetCarrierAddresses()
        {
            try
            {
                //this.IsBusy = true;
                api_Address _api_ADR = new api_Address(WizardData.LoggedUser);
                var result = await _api_ADR.GET_Addresslist(enumAppProcess.StoreOut, SelectedAusgang.ArbeitsbereichId);
                if (result.Success)
                {
                    ListAddressesCarrier = new ObservableCollection<Addresses>(result.ListAddresses);
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
        public async Task<ResponseAusgang> UpdateAusgang()
        {
            //this.IsBusy = true;
            api_Ausgang _apiAusgang = new api_Ausgang(WizardData.LoggedUser);
            ResponseAusgang resAusgang = new ResponseAusgang();
            resAusgang.UserId = (int)WizardData.LoggedUser.Id;
            resAusgang.StoreOutArt = WizardData.Wiz_StoreOut.StoreOutArt;
            resAusgang.StoreOutArt_Steps = CurrentStep;
            resAusgang.Ausgang = SelectedAusgang.Copy();
            resAusgang.UserId = (int)WizardData.LoggedUser.Id;
            try
            {
                var result = await _apiAusgang.POST_Ausgang_Update_WizStoreOut(resAusgang);
                resAusgang.Success = result.Success;
                if (result.Success)
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
