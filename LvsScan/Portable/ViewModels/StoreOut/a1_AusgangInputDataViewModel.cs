using Common.Enumerations;
using Common.Helper;
using Common.Models;
using LvsScan.Portable.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LvsScan.Portable.ViewModels.StoreOut
{
    public class a1_AusgangInputDataViewModel : BaseViewModel
    {
        public a1_AusgangInputDataViewModel()
        {
            StoreOutArt = enumStoreOutArt.NotSet;
            Init();
        }
        public a1_AusgangInputDataViewModel(enumStoreOutArt myStoreOutArt) : this()
        {
            StoreOutArt = myStoreOutArt;
            Init();
        }

        private void Init()
        {
            IsBusy = false;
            //ShowContent = true;
            //ShowBusyIndicator = false;

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
                    IsBtnClientEnabled = true;
                    IsBtnReciepinEnabled = true;
                    break;
            }

            IsBtnDriverEnabled = true;
            IsBtnKFZEnabled = true;
            IsBtnTrailerEnabled = true;

            IsInputClientEnabled = false;
            IsInputReciepinEnabled = false;
            IsInputKFZEnabled = false;
            IsInputTrailerEnabled = false;
            IsInputDriverEnabled = false;
        }
        public enumStoreOutArt StoreOutArt { set; get; } = enumStoreOutArt.NotSet;


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
                    WorkspaceString = "[Arbeitsbereich: " + selectedAusgang.Workspace + "]";

                    KFZ = selectedAusgang.KFZ;
                    Trailer = selectedAusgang.Trailer;
                    Driver = selectedAusgang.Fahrer;

                    if (selectedAusgang.Workspace is Workspaces)
                    {
                        Workspace = selectedAusgang.Workspace.Copy();
                    }
                }
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
                    this.IsBusy = true;
                    Task.Run(() => GetArticleInAusgang()).Wait();
                    this.IsBusy = false;
                }
            }
        }
        public async Task GetArticleInAusgang()
        {
            try
            {
                api_Ausgang _api_Ausgang = new api_Ausgang(WizardData.LoggedUser);
                var result = await _api_Ausgang.GET_Ausgang_byId(SelectedAusgang.Id);
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
        public async Task GetWorkspace()
        {
            try
            {
                this.IsBusy = true;
                api_Workspace _api_Workspace = new api_Workspace();
                var result = await _api_Workspace.GET_Workspace_ById(SelectedAusgang.ArbeitsbereichId);
                this.IsBusy = false;

                if (result.Success)
                {
                    Workspace = result.Workspace.Copy();
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        private Workspaces workspace = new Workspaces();
        public Workspaces Workspace
        {
            get { return workspace; }
            set { SetProperty(ref workspace, value); }
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

        private string workspaceString = string.Empty;
        public string WorkspaceString
        {
            get { return workspaceString; }
            set { SetProperty(ref workspaceString, value); }
        }


        private string recipientString = string.Empty;
        public string RecipientString
        {
            get { return recipientString; }
            set { SetProperty(ref recipientString, value); }
        }

        private string clientString = string.Empty;
        public string ClientString
        {
            get { return clientString; }
            set { SetProperty(ref clientString, value); }
        }

        private string slb = string.Empty;
        public string SLB
        {
            get { return slb; }
            set { SetProperty(ref slb, value); }
        }
        private string kfz = string.Empty;
        public string KFZ
        {
            get { return kfz; }
            set
            {
                SetProperty(ref kfz, value);
                if ((SelectedAusgang is Ausgaenge) && (SelectedAusgang.Id > 0))
                {
                    if (!SelectedAusgang.KFZ.Equals(kfz))
                    {
                        Ausgaenge tmpAusgang = SelectedAusgang.Copy();
                        tmpAusgang.KFZ = kfz;
                        SelectedAusgang = tmpAusgang;
                    }
                }
            }
        }

        private string trailer = string.Empty;
        public string Trailer
        {
            get { return trailer; }
            set
            {
                SetProperty(ref trailer, value);
                if ((SelectedAusgang is Ausgaenge) && (SelectedAusgang.Id > 0))
                {
                    if (!SelectedAusgang.Trailer.Equals(trailer))
                    {
                        Ausgaenge tmpAusgang = SelectedAusgang.Copy();
                        tmpAusgang.Trailer = trailer;
                        SelectedAusgang = tmpAusgang;
                    }
                }
            }
        }

        private string driver = string.Empty;
        public string Driver
        {
            get { return driver; }
            set
            {
                SetProperty(ref driver, value);
                if ((SelectedAusgang is Ausgaenge) && (SelectedAusgang.Id > 0))
                {
                    if (!SelectedAusgang.Fahrer.Equals(driver))
                    {
                        Ausgaenge tmpAusgang = SelectedAusgang.Copy();
                        tmpAusgang.Fahrer = driver;
                        SelectedAusgang = tmpAusgang;
                    }
                }
            }
        }


        private bool isBtnClientEnabled = false;
        public bool IsBtnClientEnabled
        {
            get { return isBtnClientEnabled; }
            set { SetProperty(ref isBtnClientEnabled, value); }
        }

        private bool isBtnReciepinEnabled = false;
        public bool IsBtnReciepinEnabled
        {
            get { return isBtnReciepinEnabled; }
            set { SetProperty(ref isBtnReciepinEnabled, value); }
        }

        private bool isBtnKFZEnabled = false;
        public bool IsBtnKFZEnabled
        {
            get { return isBtnKFZEnabled; }
            set { SetProperty(ref isBtnKFZEnabled, value); }
        }

        private bool isBtnTrailerEnabled = false;
        public bool IsBtnTrailerEnabled
        {
            get { return isBtnTrailerEnabled; }
            set { SetProperty(ref isBtnTrailerEnabled, value); }
        }

        private bool isBtnDriverEnabled = false;
        public bool IsBtnDriverEnabled
        {
            get { return isBtnDriverEnabled; }
            set { SetProperty(ref isBtnDriverEnabled, value); }
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
                    }
                    SelectedAusgang = tmpAusgang.Copy();
                    IsInputReciepinEnabled = !IsInputReciepinEnabled;
                }
            }
        }




        private ObservableCollection<Addresses> _ListAddressesClients = new ObservableCollection<Addresses>();
        public ObservableCollection<Addresses> ListAddressesClients
        {
            get { return _ListAddressesClients; }
            set { SetProperty(ref _ListAddressesClients, value); }
        }

        private ObservableCollection<Addresses> _ListAddressesRecipien = new ObservableCollection<Addresses>();
        public ObservableCollection<Addresses> ListAddressesRecipien
        {
            get { return _ListAddressesRecipien; }
            set { SetProperty(ref _ListAddressesRecipien, value); }
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

        public async Task GetReceipinAddresses()
        {
            try
            {
                if (IsBtnReciepinEnabled)
                {
                    //this.IsBusy = true;
                    api_Address _api_ADR = new api_Address(WizardData.LoggedUser);
                    var result = await _api_ADR.GET_Addresslist(enumAppProcess.NotSet, SelectedAusgang.ArbeitsbereichId);
                    if (result.Success)
                    {
                        ListAddressesRecipien = new ObservableCollection<Addresses>(result.ListAddresses);
                    }
                    //this.IsBusy = false;
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
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


        private bool _IsInputDriverEnabled = true;
        public bool IsInputDriverEnabled
        {
            get { return _IsInputDriverEnabled; }
            set
            {
                SetProperty(ref _IsInputDriverEnabled, value);
                BackgroundColorInputDriver = ValueToColorConverter.ButtonBackgroundColor(_IsInputDriverEnabled);
            }
        }
        private bool _IsInputTrailerEnabled = true;
        public bool IsInputTrailerEnabled
        {
            get { return _IsInputTrailerEnabled; }
            set
            {
                SetProperty(ref _IsInputTrailerEnabled, value);
                BackgroundColorInputTrailer = ValueToColorConverter.ButtonBackgroundColor(_IsInputTrailerEnabled);
            }
        }

        private bool _IsInputKFZEnabled = true;
        public bool IsInputKFZEnabled
        {
            get { return _IsInputKFZEnabled; }
            set
            {
                SetProperty(ref _IsInputKFZEnabled, value);
                BackgroundColorInputKFZ = ValueToColorConverter.ButtonBackgroundColor(_IsInputKFZEnabled);
            }
        }

        private System.Drawing.Color _BackgroundColorInputClient = ValueToColorConverter.ButtonBackgroundColor(false);
        public System.Drawing.Color BackgroundColorInputClient
        {
            get { return _BackgroundColorInputClient; }
            set { SetProperty(ref _BackgroundColorInputClient, value); }
        }

        private System.Drawing.Color _BackgroundColorInputReceipin = ValueToColorConverter.ButtonBackgroundColor(false);
        public System.Drawing.Color BackgroundColorInputReceipin
        {
            get { return _BackgroundColorInputReceipin; }
            set { SetProperty(ref _BackgroundColorInputReceipin, value); }
        }

        private System.Drawing.Color _BackgroundColorInputKFZ = ValueToColorConverter.ButtonBackgroundColor(false);
        public System.Drawing.Color BackgroundColorInputKFZ
        {
            get { return _BackgroundColorInputKFZ; }
            set { SetProperty(ref _BackgroundColorInputKFZ, value); }
        }

        private System.Drawing.Color _BackgroundColorInputTrailer = ValueToColorConverter.ButtonBackgroundColor(false);
        public System.Drawing.Color BackgroundColorInputTrailer
        {
            get { return _BackgroundColorInputTrailer; }
            set { SetProperty(ref _BackgroundColorInputTrailer, value); }
        }

        private System.Drawing.Color _BackgroundColorInputDriver = ValueToColorConverter.ButtonBackgroundColor(false);
        public System.Drawing.Color BackgroundColorInputDriver
        {
            get { return _BackgroundColorInputDriver; }
            set { SetProperty(ref _BackgroundColorInputDriver, value); }
        }
        //----------------------------------------------------------------------------------------

    }
}
