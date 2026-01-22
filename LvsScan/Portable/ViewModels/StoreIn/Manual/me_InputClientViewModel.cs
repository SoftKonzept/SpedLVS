using Common.ApiModels;
using Common.Enumerations;
using Common.Helper;
using Common.Models;
using LvsScan.Portable.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LvsScan.Portable.ViewModels.StoreIn.Manual
{
    public class me_InputClientViewModel : BaseViewModel
    {
        public me_InputClientViewModel()
        {
            Init();
        }
        public me_InputClientViewModel(enumStoreInArt myStoreInArt)
        {
            StoreInArt = myStoreInArt;
            Init();
        }

        public void Init()
        {
            IsBusy = false;
            IsBtnClientEnabled = true;
            IsBtnReciepinEnabled = true;
        }

        //private enumStoreInArt _StoreInArt = enumStoreInArt.NotSet;
        //public enumStoreInArt StoreInArt = enumStoreInArt.manually;
        //public enumStoreInArt_Steps CurrentStep = enumStoreInArt_Steps.wizStepInputAdr;
        //private enumStoreInArt _StoreInArt = enumStoreInArt.NotSet;
        public enumStoreInArt_Steps CurrentStep
        {
            get { return enumStoreInArt_Steps.wizStepInputAdr; }
        }

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

        private Eingaenge _SelectedEingang = new Eingaenge();
        public Eingaenge SelectedEingang
        {
            get { return _SelectedEingang; }
            set
            {
                SetProperty(ref _SelectedEingang, value);
                if (_SelectedEingang != null)
                {
                    EingangIdString = "Eingang ID: " + _SelectedEingang.LEingangID.ToString() + " - [" + _SelectedEingang.Id.ToString() + "]";
                    EingangDateString = "vom " + _SelectedEingang.Eingangsdatum.ToString("dd.MM.yyyy");
                    //AusgangDateString += " | Termin: " + _SelectedEingang.Termin.ToString("dd.MM.yyyy HH:mm") + " Uhr";
                    BackgroundColorHead = _SelectedEingang.ViewBackgroundcolor;
                    WorkspaceString = "Arbeitsbereich: " + _SelectedEingang.Workspace.WorkspaceString;

                    // 1. Step
                    ClientString = _SelectedEingang.AuftraggeberString;
                    //if ((_SelectedEingang.Auftraggeber>0) && (_SelectedEingang.Empfaenger.Equals(0)))
                    //{
                    //    SelectedEingang.Empfaenger = _SelectedEingang.Auftraggeber;
                    //}
                    RecipientString = _SelectedEingang.EmpfaengerString;
                }
            }
        }
        private Eingaenge _EingangOriginal = new Eingaenge();
        public Eingaenge EingangOriginal
        {
            get { return _EingangOriginal; }
            set { SetProperty(ref _EingangOriginal, value); }
        }
        public async Task<ResponseEingang> UpdateEingang()
        {
            IsBusy = true;

            ResponseEingang resEA = new ResponseEingang();
            resEA.Eingang = SelectedEingang.Copy();
            resEA.UserId = (int)WizardData.LoggedUser.Id;
            resEA.StoreInArt = StoreInArt;
            resEA.StoreInArt_Steps = CurrentStep;

            api_Eingang _api = new api_Eingang(WizardData.LoggedUser);
            try
            {
                var result = await _api.POST_Eingang_Update_WizStoreIn(resEA);
                resEA.Success = result.Success;
                if (result.Success)
                {
                    SelectedEingang = result.Eingang.Copy();
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }

            IsBusy = false;
            return resEA;
        }

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




        //--------------------------------------------- Client --------------------------------------
        private bool _IsBtnClientEnabled;
        public bool IsBtnClientEnabled
        {
            get { return _IsBtnClientEnabled; }
            set { SetProperty(ref _IsBtnClientEnabled, value); }
        }
        private Addresses _PickerSelectedItemClientAddress;
        public Addresses PickerSelectedItemClientAddress
        {
            get { return _PickerSelectedItemClientAddress; }
            set
            {
                SetProperty(ref _PickerSelectedItemClientAddress, value);
                if ((_PickerSelectedItemClientAddress.Id > 0) && (!SelectedEingang.Auftraggeber.Equals(_PickerSelectedItemClientAddress.Id)))
                {
                    Eingaenge tmpEA = SelectedEingang.Copy();
                    if (tmpEA.Id > 0)
                    {
                        tmpEA.AuftraggeberString = _PickerSelectedItemClientAddress.Name1 + " - " +
                                                        _PickerSelectedItemClientAddress.ZIP + " " +
                                                        _PickerSelectedItemClientAddress.City;
                        tmpEA.Auftraggeber = _PickerSelectedItemClientAddress.Id;
                        if (tmpEA.Empfaenger == 0)
                        {
                            tmpEA.Empfaenger = tmpEA.Auftraggeber;
                            tmpEA.EmpfaengerString = tmpEA.AuftraggeberString;
                        }
                    }
                    SelectedEingang = tmpEA.Copy();
                    //IsInputClientEnabled = !IsInputClientEnabled;
                    IsInputClientEnabled = false;
                }
            }
        }

        private bool _IsInputClientEnabled;
        public bool IsInputClientEnabled
        {
            get { return _IsInputClientEnabled; }
            set { SetProperty(ref _IsInputClientEnabled, value); }
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
                    var result = await _api_ADR.GET_Addresslist(enumAppProcess.StoreIn, SelectedEingang.ArbeitsbereichId);
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

        /////---------------------------------------------------------------------------------------------------
        ///// <summary>
        /////             reciepin - Empfänger 
        ///// </summary>

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
                if ((_PickerSelectedItemReceipinAddress.Id > 0) && (!SelectedEingang.Empfaenger.Equals(_PickerSelectedItemReceipinAddress.Id)))
                {
                    Eingaenge tmpEA = SelectedEingang.Copy();
                    if (tmpEA.Id > 0)
                    {
                        tmpEA.EmpfaengerString = _PickerSelectedItemReceipinAddress.Name1 + " - " +
                                                             _PickerSelectedItemReceipinAddress.ZIP + " " +
                                                             _PickerSelectedItemReceipinAddress.City;
                        tmpEA.Empfaenger = _PickerSelectedItemReceipinAddress.Id;
                        tmpEA.EntladeID = _PickerSelectedItemReceipinAddress.Id;
                    }
                    SelectedEingang = tmpEA.Copy();
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
                    var result = await _api_ADR.GET_Addresslist(enumAppProcess.StoreIn, SelectedEingang.ArbeitsbereichId);
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


        private string searchString = string.Empty;
        public string SearchString
        {
            get { return searchString; }
            set { SetProperty(ref searchString, value); }
        }

    }
}
