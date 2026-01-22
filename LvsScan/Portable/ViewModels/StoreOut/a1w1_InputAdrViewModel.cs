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
    public class a1w1_InputAdrViewModel : BaseViewModel
    {
        public a1w1_InputAdrViewModel()
        {
            StoreOutArt = enumStoreOutArt.NotSet;
            Init();
        }
        public a1w1_InputAdrViewModel(enumStoreOutArt myStoreOutArt)
        {
            StoreOutArt = myStoreOutArt;
            Init();
        }
        private void Init()
        {
            IsBusy = false;
            //IsBaseNextEnabeld = false;

            switch (StoreOutArt)
            {
                case enumStoreOutArt.manually:
                case enumStoreOutArt.NotSet:
                    IsBtnClientEnabled = true;
                    IsBtnReciepinEnabled = true;

                    break;
                case enumStoreOutArt.call:
                case enumStoreOutArt.open:
                    IsBtnClientEnabled = false;
                    IsBtnReciepinEnabled = false;
                    break;

                default:
                    IsBtnClientEnabled = false;
                    IsBtnReciepinEnabled = false;
                    break;
            }
            //IsBtnTerminEnabled = true;

            IsInputClientEnabled = false;
            IsInputReciepinEnabled = false;
            //IsInputTerminEnabled = false;        
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
                    Task.Run(() => GetArticleInAusgang()).Wait();
                }
            }
        }

        public enumStoreOutArt_Steps CurrentStep = enumStoreOutArt_Steps.wizStepInputAdr;
        public enumStoreOutArt StoreOutArt = enumStoreOutArt.open;

        private Ausgaenge _AusgangOriginal = new Ausgaenge();
        public Ausgaenge AusgangOriginal
        {
            get { return _AusgangOriginal; }
            set { SetProperty(ref _AusgangOriginal, value); }
        }

        //-------------------------------------------------------------------------------------------------------
        /// <summary>
        ///             Ausgang - Infos für den aktuellen Ausgang
        /// </summary>
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

                    ClientString = selectedAusgang.AuftraggeberString;
                    RecipientString = selectedAusgang.EmpfaengerString;
                    WorkspaceString = "[Arbeitsbereich: " + selectedAusgang.Workspace.WorkspaceString + "]";
                    PrintDocumentStoreOutStatus_Frachtbrief = SelectedAusgang.PrintDocumentStoreOutStatus_Frachtbrief;
                    PrintDocumentStoreOutStatus_Lfs = SelectedAusgang.PrintDocumentStoreOutStatus_Lfs;
                    PrintDocumentStoreOutStatus_List = SelectedAusgang.PrintDocumentStoreOutStatus_List;
                    //SelectedTermin = selectedAusgang.Termin;

                    if (selectedAusgang.Workspace is Workspaces)
                    {
                        Workspace = selectedAusgang.Workspace.Copy();
                        if (
                            (WizardData != null) &&
                            (WizardData.Wiz_StoreOut != null) &&
                            (Workspace.Id > 0)
                          )
                        {
                            WizardData.Wiz_StoreOut.Workspace = Workspace.Copy();
                        }
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

        private System.Drawing.Color backgroundColorHead = System.Drawing.Color.WhiteSmoke;
        public System.Drawing.Color BackgroundColorHead
        {
            get { return backgroundColorHead; }
            set { SetProperty(ref backgroundColorHead, value); }
        }
        public async Task GetArticleInAusgang()
        {
            try
            {
                this.IsBusy = true;
                api_Ausgang _api_Ausgang = new api_Ausgang(WizardData.LoggedUser);
                var result = await _api_Ausgang.GET_Ausgang_byId(SelectedAusgang.Id);
                this.IsBusy = false;

                if (result.Success)
                {
                    SelectedAusgang = result.Ausgang.Copy();
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        ///---------------------------------------------------------------------------------------------------
        /// <summary>
        ///             Workspace / Arbeitsbereich
        /// </summary>
        private Workspaces workspace = new Workspaces();
        public Workspaces Workspace
        {
            get { return workspace; }
            set { SetProperty(ref workspace, value); }
        }
        private string workspaceString = string.Empty;
        public string WorkspaceString
        {
            get { return workspaceString; }
            set { SetProperty(ref workspaceString, value); }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        ///             client - Auftraggeber 
        /// </summary>
        private string clientString = string.Empty;
        public string ClientString
        {
            get { return clientString; }
            set { SetProperty(ref clientString, value); }
        }
        private bool isBtnClientEnabled = false;
        public bool IsBtnClientEnabled
        {
            get { return isBtnClientEnabled; }
            set { SetProperty(ref isBtnClientEnabled, value); }
        }
        private bool _IsInputClientEnabled = true;
        public bool IsInputClientEnabled
        {
            get { return _IsInputClientEnabled; }
            set
            {
                SetProperty(ref _IsInputClientEnabled, value);
                BackgroundColorInputClient = ValueToColorConverter.ButtonBackgroundColor(_IsInputClientEnabled);
            }
        }
        private System.Drawing.Color _BackgroundColorInputClient = ValueToColorConverter.ButtonBackgroundColor(false);
        public System.Drawing.Color BackgroundColorInputClient
        {
            get { return _BackgroundColorInputClient; }
            set { SetProperty(ref _BackgroundColorInputClient, value); }
        }
        private Addresses _PickerSelectedItemClientAddress;
        public Addresses PickerSelectedItemClientAddress
        {
            get { return _PickerSelectedItemClientAddress; }
            set
            {
                SetProperty(ref _PickerSelectedItemClientAddress, value);
                if (_PickerSelectedItemClientAddress.Id > 0)
                {
                    Ausgaenge tmpAusgang = SelectedAusgang.Copy();
                    if (tmpAusgang.Id > 0)
                    {
                        tmpAusgang.AuftraggeberString = _PickerSelectedItemClientAddress.Name1 + " - " +
                                                        _PickerSelectedItemClientAddress.ZIP + " " +
                                                        _PickerSelectedItemClientAddress.City;
                        tmpAusgang.Auftraggeber = _PickerSelectedItemClientAddress.Id;
                    }
                    SelectedAusgang = tmpAusgang.Copy();
                    IsInputClientEnabled = !IsInputClientEnabled;
                }
            }
        }

        private ObservableCollection<Addresses> _ListAddressesClients = new ObservableCollection<Addresses>();
        public ObservableCollection<Addresses> ListAddressesClients
        {
            get { return _ListAddressesClients; }
            set { SetProperty(ref _ListAddressesClients, value); }
        }
        public async Task GetClientAddresses()
        {
            try
            {
                if (IsBtnClientEnabled)
                {
                    //this.IsBusy = true;
                    api_Address _api_ADR = new api_Address(WizardData.LoggedUser);
                    var result = await _api_ADR.GET_Addresslist(enumAppProcess.StoreOut, SelectedAusgang.ArbeitsbereichId);
                    if (result.Success)
                    {
                        ListAddressesClients = new ObservableCollection<Addresses>(result.ListAddresses);
                    }
                    else
                    {
                        //await MessageService.ShowAsync("ACHTUNG", result.Error);
                    }
                    //this.IsBusy = false;
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
        ///---------------------------------------------------------------------------------------------------
        /// <summary>
        ///             reciepin - Empfänger 
        /// </summary>
        private string recipientString = string.Empty;
        public string RecipientString
        {
            get { return recipientString; }
            set { SetProperty(ref recipientString, value); }
        }
        private bool isBtnReciepinEnabled = false;
        public bool IsBtnReciepinEnabled
        {
            get { return isBtnReciepinEnabled; }
            set { SetProperty(ref isBtnReciepinEnabled, value); }
        }
        private bool _IsInputReciepinEnabled = true;
        public bool IsInputReciepinEnabled
        {
            get { return _IsInputReciepinEnabled; }
            set
            {
                SetProperty(ref _IsInputReciepinEnabled, value);
                BackgroundColorInputReceipin = ValueToColorConverter.ButtonBackgroundColor(_IsInputReciepinEnabled);
            }
        }
        private System.Drawing.Color _BackgroundColorInputReceipin = ValueToColorConverter.ButtonBackgroundColor(false);
        public System.Drawing.Color BackgroundColorInputReceipin
        {
            get { return _BackgroundColorInputReceipin; }
            set { SetProperty(ref _BackgroundColorInputReceipin, value); }
        }
        private Addresses _PickerSelectedItemReceipinAddress;
        public Addresses PickerSelectedItemReceipinAddress
        {
            get { return _PickerSelectedItemReceipinAddress; }
            set
            {
                SetProperty(ref _PickerSelectedItemReceipinAddress, value);
                if (_PickerSelectedItemReceipinAddress.Id > 0)
                {
                    Ausgaenge tmpAusgang = SelectedAusgang.Copy();
                    if (tmpAusgang.Id > 0)
                    {
                        tmpAusgang.EmpfaengerString = _PickerSelectedItemReceipinAddress.Name1 + " - " +
                                                             _PickerSelectedItemReceipinAddress.ZIP + " " +
                                                             _PickerSelectedItemReceipinAddress.City;
                        tmpAusgang.Empfaenger = _PickerSelectedItemReceipinAddress.Id;
                        tmpAusgang.Entladestelle = _PickerSelectedItemReceipinAddress.Id;
                    }
                    SelectedAusgang = tmpAusgang.Copy();
                    IsInputReciepinEnabled = !IsInputReciepinEnabled;
                }
            }
        }
        private ObservableCollection<Addresses> _ListAddressesRecipien = new ObservableCollection<Addresses>();
        public ObservableCollection<Addresses> ListAddressesRecipien
        {
            get { return _ListAddressesRecipien; }
            set { SetProperty(ref _ListAddressesRecipien, value); }
        }

        public async Task GetReceipinAddresses()
        {
            try
            {
                if (IsBtnReciepinEnabled)
                {
                    this.IsBusy = true;
                    api_Address _api_ADR = new api_Address(WizardData.LoggedUser);
                    var result = await _api_ADR.GET_Addresslist(enumAppProcess.NotSet, SelectedAusgang.ArbeitsbereichId);
                    if (result.Success)
                    {
                        ListAddressesRecipien = new ObservableCollection<Addresses>(result.ListAddresses);
                    }
                    this.IsBusy = false;
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
        ///-------------------------------------------------------------------------------------------------------
        /// <summary>
        ///             termin - Termin 
        /// </summary>


        //public DateTime MinDate = new DateTime(2023, 1, 1,0,0,0);

        //private DateTime _SelectedTermin;// = new DateTime(1900,1,1);
        //public DateTime SelectedTermin
        //{
        //    get { return _SelectedTermin; }
        //    set 
        //    {
        //        SetProperty(ref _SelectedTermin, value);

        //        if (
        //                (_SelectedTermin > MinDate) &&
        //                (!_SelectedTermin.Equals(SelectedAusgang.Termin))
        //           )
        //        {
        //            Ausgaenge tmpAusgang = SelectedAusgang.Copy();
        //            if (tmpAusgang.Id > 0)
        //            {
        //                tmpAusgang.Termin = _SelectedTermin;
        //            }
        //            SelectedAusgang = tmpAusgang.Copy();
        //            IsInputTerminEnabled = false;
        //        }

        //    }
        //}
        //private bool isBtnTerminEnabled = false;
        //public bool IsBtnTerminEnabled
        //{
        //    get { return isBtnTerminEnabled; }
        //    set { SetProperty(ref isBtnTerminEnabled, value); }
        //}
        //private bool _IsInputTerminEnabled = true;
        //public bool IsInputTerminEnabled
        //{
        //    get { return _IsInputTerminEnabled; }
        //    set
        //    {
        //        SetProperty(ref _IsInputTerminEnabled, value);
        //        BackgroundColorInputTermin = ValueToColorConverter.ButtonBackgroundColor(_IsInputTerminEnabled);
        //    }
        //}
        //private System.Drawing.Color _BackgroundColorInputTermin = ValueToColorConverter.ButtonBackgroundColor(false);
        //public System.Drawing.Color BackgroundColorInputTermin
        //{
        //    get { return _BackgroundColorInputTermin; }
        //    set { SetProperty(ref _BackgroundColorInputTermin, value); }
        //}

        public async Task<ResponseAusgang> UpdateAusgang()
        {
            //this.IsBusy = true;
            api_Ausgang _apiAusgang = new api_Ausgang(WizardData.LoggedUser);
            ResponseAusgang resAusgang = new ResponseAusgang();
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
