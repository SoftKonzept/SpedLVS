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

namespace LvsScan.Portable.ViewModels.StoreIn
{
    public class e1_EingangInputDataViewModel : BaseViewModel
    {
        public e1_EingangInputDataViewModel()
        {
            Init();
        }
        public e1_EingangInputDataViewModel(enumStoreInArt myStoreInArt) : this()
        {
            StoreInArt = myStoreInArt;
            Init();
        }
        public enumStoreInArt StoreInArt { set; get; } = enumStoreInArt.NotSet;


        private void Init()
        {
            IsBusy = false;

            switch (StoreInArt)
            {
                case enumStoreInArt.manually:
                case enumStoreInArt.NotSet:
                    IsBtnClientEnabled = true;
                    IsBtnReciepinEnabled = true;

                    break;
                case enumStoreInArt.edi:
                case enumStoreInArt.open:
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

        private Eingaenge _EingangOriginal = new Eingaenge();
        public Eingaenge EingangOriginal
        {
            get { return _EingangOriginal; }
            set { SetProperty(ref _EingangOriginal, value); }
        }


        private Eingaenge _SelectedEingang = new Eingaenge();
        public Eingaenge SelectedEingang
        {
            get { return _SelectedEingang; }
            set
            {
                SetProperty(ref _SelectedEingang, value);
                if (_SelectedEingang.Id > 0)
                {
                    EingangIdString = "Eingang ID: " + _SelectedEingang.LEingangID.ToString() + " - [" + _SelectedEingang.Id.ToString() + "]";
                    EingangDateString = "vom " + _SelectedEingang.Eingangsdatum.ToString("dd.MM.yyyy");
                    //AusgangDateString += " | Termin: " + _SelectedEingang.Termin.ToString("dd.MM.yyyy HH:mm") + " Uhr";
                    BackgroundColorHead = _SelectedEingang.ViewBackgroundcolor;
                    WorkspaceString = "[Arbeitsbereich: " + _SelectedEingang.Workspace.WorkspaceString + "]";

                    ClientString = _SelectedEingang.AuftraggeberString;
                    RecipientString = _SelectedEingang.EmpfaengerString;


                    //KFZ = selectedAusgang.KFZ;
                    //Trailer = selectedAusgang.Trailer;
                    //Driver = selectedAusgang.Fahrer;

                    //if (selectedAusgang.Workspace is Workspaces)
                    //{
                    //    Workspace = selectedAusgang.Workspace.Copy();
                    //}
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
                    Task.Run(() => GetArticleInEingang()).Wait();
                    this.IsBusy = false;
                }
            }
        }
        public async Task GetArticleInEingang()
        {
            try
            {
                api_Eingang _api = new api_Eingang(WizardData.LoggedUser);
                var result = await _api.GET_Eingang_byId(SelectedEingang.Id);
                if (result.Success)
                {
                    SelectedEingang = result.Eingang.Copy();
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
                var result = await _api_Workspace.GET_Workspace_ById(SelectedEingang.ArbeitsbereichId);
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

        private string kfz = string.Empty;
        public string KFZ
        {
            get { return kfz; }
            set
            {
                SetProperty(ref kfz, value);
                if ((SelectedEingang is Eingaenge) && (SelectedEingang.Id > 0))
                {
                    if (!SelectedEingang.KFZ.Equals(kfz))
                    {
                        Eingaenge tmpEA = SelectedEingang.Copy();
                        tmpEA.KFZ = kfz;
                        SelectedEingang = tmpEA;
                    }
                }
            }
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

        private string driver = string.Empty;
        public string Driver
        {
            get { return driver; }
            set
            {
                SetProperty(ref driver, value);
                if ((SelectedEingang is Eingaenge) && (SelectedEingang.Id > 0))
                {
                    if (!SelectedEingang.Fahrer.Equals(driver))
                    {
                        Eingaenge tmpEA = SelectedEingang.Copy();
                        tmpEA.Fahrer = driver;
                        SelectedEingang = tmpEA;
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
                    Eingaenge tmpEA = SelectedEingang.Copy();
                    if (tmpEA.Id > 0)
                    {
                        tmpEA.AuftraggeberString = _PickerSelectedItemClientAddress.Name1 + " - " +
                                                             _PickerSelectedItemClientAddress.ZIP + " " +
                                                             _PickerSelectedItemClientAddress.City;
                        tmpEA.Auftraggeber = _PickerSelectedItemClientAddress.Id;
                    }
                    SelectedEingang = tmpEA.Copy();
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
                    Eingaenge tmpEA = SelectedEingang.Copy();
                    if (tmpEA.Id > 0)
                    {
                        tmpEA.EmpfaengerString = _PickerSelectedItemReceipinAddress.Name1 + " - " +
                                                             _PickerSelectedItemReceipinAddress.ZIP + " " +
                                                             _PickerSelectedItemReceipinAddress.City;
                        tmpEA.Empfaenger = _PickerSelectedItemReceipinAddress.Id;
                    }
                    SelectedEingang = tmpEA.Copy();
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
                    var result = await _api_ADR.GET_Addresslist(enumAppProcess.StoreOut, SelectedEingang.ArbeitsbereichId);
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
                    var result = await _api_ADR.GET_Addresslist(enumAppProcess.NotSet, SelectedEingang.ArbeitsbereichId);
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
                _IsArticleScanStartVisible = (CountCheck < CountAll);
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
                _IsCreateAusgangVisible = (CountCheck > 0); //; && (IsTabCheckedSelected) && (SelectedCallsToAusgang.Count > 0);
                return _IsCreateAusgangVisible;
            }
            //set { SetProperty(ref _IsCreateAusgangVisible, value); }
        }
    }
}
