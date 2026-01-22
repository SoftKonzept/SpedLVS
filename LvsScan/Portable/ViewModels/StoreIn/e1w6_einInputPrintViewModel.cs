using Common.ApiModels;
using Common.Enumerations;
using Common.Helper;
using Common.Models;
using LvsScan.Portable.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LvsScan.Portable.ViewModels.StoreIn
{
    public class e1w6_einInputPrintViewModel : BaseViewModel
    {
        public e1w6_einInputPrintViewModel()
        {
            Init();
        }
        public e1w6_einInputPrintViewModel(enumStoreInArt myStoreInArt)
        {
            StoreInArt = myStoreInArt;
            Init();
        }

        public void Init()
        {
            IsBusy = false;
        }

        public enumStoreInArt_Steps CurrentStep = enumStoreInArt_Steps.wizStepInputPrintAction;

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
                    Task.Run(() => GetArticleInEingang()).Wait();
                }
            }
        }

        private enumStoreInArt _StoreInArt = enumStoreInArt.NotSet;
        public enumStoreInArt StoreInArt
        {
            get { return _StoreInArt; }
            set { SetProperty(ref _StoreInArt, value); }
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
                    WorkspaceString = "Arbeitsbereich: " + _SelectedEingang.Workspace.WorkspaceString;

                    // 1. Step
                    ClientString = _SelectedEingang.AuftraggeberString;
                    RecipientString = _SelectedEingang.EmpfaengerString;

                    // 6. Step Print
                    //PrintDocuments = _SelectedEingang.PrintActionByScanner;
                    PrintAllLable = _SelectedEingang.PrintActionScannerAllLable;
                    PrintEingangsliste = _SelectedEingang.PrintActionScannerEingangsliste;
                }
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

        private string clientString = string.Empty;
        public string ClientString
        {
            get { return clientString; }
            set { SetProperty(ref clientString, value); }
        }

        public async Task GetArticleInEingang()
        {
            try
            {
                this.IsBusy = true;
                api_Eingang _api = new api_Eingang(WizardData.LoggedUser);

                var result = await _api.GET_Eingang_byId(SelectedEingang.Id);
                this.IsBusy = false;

                if (result.Success)
                {
                    ArticlesInEingang = new List<Articles>(result.ListEingangArticle);
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

        public async Task<ResponseEingang> UpdateEingang()
        {
            ResponseEingang resEA = new ResponseEingang();
            resEA.Eingang = SelectedEingang.Copy();
            resEA.UserId = (int)WizardData.LoggedUser.Id;
            resEA.StoreInArt = WizardData.Wiz_StoreIn.StoreInArt;
            resEA.StoreInArt_Steps = CurrentStep;

            api_Eingang _api = new api_Eingang(WizardData.LoggedUser);
            try
            {
                var result = await _api.POST_Eingang_Update_WizStoreIn(resEA);
                resEA.Success = result.Success;
                if (result.Success)
                {
                    SelectedEingang = result.Eingang.Copy();
                    if (!SelectedEingang.Equals(EingangOriginal))
                    {
                        EingangOriginal = result.Eingang.Copy();
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            return resEA;
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
                    var result = await _api_ADR.GET_Addresslist(enumAppProcess.NotSet, SelectedEingang.ArbeitsbereichId);
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

        private ImageSource _BackgroundImage; // = Android.Resource.Drawable.lock_open
        public ImageSource BackgroundImage
        {
            get { return _BackgroundImage; }
            set { SetProperty(ref _BackgroundImage, value); }
        }

        //------------------------------------------------------------------------------------------------------------------
        //     PRINT 
        //------------------------------------------------------------------------------------------------------------------
        private bool _PrintAllLable;
        public bool PrintAllLable
        {
            get { return _PrintAllLable; }
            set
            {
                SetProperty(ref _PrintAllLable, value);
                if ((SelectedEingang is Eingaenge) && (!SelectedEingang.PrintActionScannerAllLable.Equals(_PrintAllLable)))
                {
                    SelectedEingang.PrintActionScannerAllLable = _PrintAllLable;
                }
            }
        }

        private bool _PrintEingangsliste;
        public bool PrintEingangsliste
        {
            get { return _PrintEingangsliste; }
            set
            {
                SetProperty(ref _PrintEingangsliste, value);
                if ((SelectedEingang is Eingaenge) && (!SelectedEingang.PrintActionScannerEingangsliste.Equals(_PrintEingangsliste)))
                {
                    SelectedEingang.PrintActionScannerEingangsliste = _PrintEingangsliste;
                }
            }
        }
    }
}
