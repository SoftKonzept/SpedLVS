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
    public class e1w3_einInputCarrierViewModel : BaseViewModel
    {
        public e1w3_einInputCarrierViewModel()
        {
            Init();
        }
        public e1w3_einInputCarrierViewModel(enumStoreInArt myStoreInArt)
        {
            StoreInArt = myStoreInArt;
            Init();
        }

        public void Init()
        {
            IsBusy = false;
            IsBtnCarrierEnabled = true;
            IsInputCarrierEnabled = false;
        }


        public enumStoreInArt_Steps CurrentStep = enumStoreInArt_Steps.wizStepInputCarrier;

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
                    //ClientString = _SelectedEingang.AuftraggeberString;
                    //RecipientString = _SelectedEingang.EmpfaengerString;

                    // 2. Step
                    //EADate = _SelectedEingang.Eingangsdatum;
                    //LfsNr = _SelectedEingang.LfsNr;

                    // 3. Step
                    CarriertString = _SelectedEingang.Spediteur;

                    //// 4. Step
                    //SelectedEingang.IsShip = false;
                    //SelectedEingang.Ship = string.Empty;
                    //Waggon = _SelectedEingang.WaggonNr;
                    //IsWaggon = _SelectedEingang.IsWaggon;
                    //KFZ = _SelectedEingang.KFZ;
                    //Driver = _SelectedEingang.Fahrer;
                    //switch (StoreInArt)
                    //{
                    //    case enumStoreInArt.manually:
                    //    case enumStoreInArt.open:
                    //    case enumStoreInArt.edi:
                    //        //IsStepFinished = ((!KFZ.Equals(string.Empty)) && (!Driver.Equals(string.Empty)));
                    //        break;
                    //    case enumStoreInArt.NotSet:
                    //        IsStepFinished = false;
                    //        break;
                    //}

                    //// 5. Step
                    //Lagertransport = _SelectedEingang.LagerTransport;
                    //DirectDelivery = _SelectedEingang.DirektDelivery;
                    //Retoure = _SelectedEingang.Retoure;    

                    //// 6. Step
                    //PrintDocuments = _SelectedEingang.PrintActionByScanner;

                    //// 7. Step
                    //IsChecked = _SelectedEingang.Check;
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
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            return resEA;
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
                    Eingaenge tmpEA = SelectedEingang.Copy();
                    if (tmpEA.Id > 0)
                    {
                        tmpEA.Spediteur = _PickerSelectedItemCarrierAddress.Name1 + " - " +
                                                             _PickerSelectedItemCarrierAddress.ZIP + " " +
                                                             _PickerSelectedItemCarrierAddress.City;
                        tmpEA.SpedId = _PickerSelectedItemCarrierAddress.Id;
                    }
                    SelectedEingang = tmpEA.Copy();
                    //IsInputCarrierEnabled = !IsInputCarrierEnabled;
                    IsInputCarrierEnabled = false;
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

        public async Task GetCarrierAddresses()
        {
            try
            {
                this.IsBusy = true;
                api_Address _api_ADR = new api_Address(WizardData.LoggedUser);
                var result = await _api_ADR.GET_Addresslist(enumAppProcess.StoreIn, SelectedEingang.ArbeitsbereichId);
                if (result.Success)
                {
                    ListAddressesCarrier = new ObservableCollection<Addresses>(result.ListAddresses);
                }
                this.IsBusy = false;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        /////-----------------------------------------------------------------------------------------------------------------------------------------
        ///// <summary>
        /////                 KFZ Data
        ///// </summary>
        ///// 
        //private bool _IsKfz = true;
        //public bool IsKfz
        //{
        //    get { return _IsKfz; }
        //    set
        //    {
        //        SetProperty(ref _IsKfz, value);
        //        if (SelectedEingang.IsWaggon.Equals(_IsKfz))
        //        {
        //            //IsWaggon = (!_IsKfz);
        //        }
        //    }
        //}


        //private string kfz = string.Empty;
        //public string KFZ
        //{
        //    get { return kfz; }
        //    set
        //    {
        //        SetProperty(ref kfz, value);
        //        if ((SelectedEingang is Eingaenge) && (SelectedEingang.Id > 0))
        //        {
        //            if (!SelectedEingang.KFZ.Equals(kfz))
        //            {
        //                Eingaenge tmpEA = SelectedEingang.Copy();
        //                tmpEA.WaggonNr = string.Empty;
        //                tmpEA.IsWaggon=false;
        //                tmpEA.KFZ = kfz;
        //                SelectedEingang = tmpEA.Copy();                        
        //            }
        //        }
        //    }
        //}
        //private bool isBtnKFZEnabled = false;
        //public bool IsBtnKFZEnabled
        //{
        //    get { return isBtnKFZEnabled; }
        //    set { SetProperty(ref isBtnKFZEnabled, value); }
        //}
        //private bool _IsInputKFZEnabled = false;
        //public bool IsInputKFZEnabled
        //{
        //    get { return _IsInputKFZEnabled; }
        //    set
        //    {
        //        SetProperty(ref _IsInputKFZEnabled, value);
        //        BackgroundColorInputKFZ = ValueToColorConverter.ButtonBackgroundColor(_IsInputKFZEnabled);
        //    }
        //}
        //private System.Drawing.Color _BackgroundColorInputKFZ = ValueToColorConverter.ButtonBackgroundColor(false);
        //public System.Drawing.Color BackgroundColorInputKFZ
        //{
        //    get { return _BackgroundColorInputKFZ; }
        //    set { SetProperty(ref _BackgroundColorInputKFZ, value); }
        //}

        /////-----------------------------------------------------------------------------------------------------------------------------------------
        ///// <summary>
        /////                 Waggon Data
        ///// </summary>
        ///// 
        //private bool _IsWaggon = false;
        //public bool IsWaggon
        //{
        //    get { return _IsWaggon; }
        //    set 
        //    { 
        //        SetProperty(ref _IsWaggon, value);               
        //        if (!SelectedEingang.IsWaggon.Equals(_IsWaggon))
        //        {
        //            IsInputWaggonEnabled = false;
        //            IsInputKFZEnabled = false;
        //            IsInputDriverEnabled = false;

        //            //IsKfz = !_IsWaggon;
        //            if (_IsWaggon)
        //            {
        //                Eingaenge tmpEA = SelectedEingang.Copy();
        //                tmpEA.KFZ = string.Empty;
        //                tmpEA.Fahrer = string.Empty;
        //                SelectedEingang = tmpEA.Copy();

        //                IsBtnKFZEnabled = false;
        //                IsBtnDriverEnabled = false;
        //            }
        //            else
        //            {
        //                Waggon = string.Empty;
        //                IsBtnKFZEnabled = true;
        //                IsBtnDriverEnabled = true;
        //            }
        //        } 
        //    }
        //}

        //private string _Waggon = string.Empty;
        //public string Waggon
        //{
        //    get { return _Waggon; }
        //    set
        //    {
        //        SetProperty(ref _Waggon, value);
        //        if ((SelectedEingang is Eingaenge) && (SelectedEingang.Id > 0))
        //        {
        //            if (!SelectedEingang.WaggonNr.Equals(_Waggon))
        //            {
        //                Eingaenge tmpEA = SelectedEingang.Copy();
        //                tmpEA.IsWaggon = true;
        //                tmpEA.WaggonNr =Waggon;
        //                SelectedEingang = tmpEA.Copy();
        //            }
        //        }
        //    }
        //}
        //private bool _IsBtnWaggonEnabled = false;
        //public bool IsBtnWaggonEnabled
        //{
        //    get { return _IsBtnWaggonEnabled; }
        //    set { SetProperty(ref _IsBtnWaggonEnabled, value); }
        //}
        //private bool _IsInputWaggonEnabled = false;
        //public bool IsInputWaggonEnabled
        //{
        //    get { return _IsInputWaggonEnabled; }
        //    set
        //    {
        //        SetProperty(ref _IsInputWaggonEnabled, value);
        //        BackgroundColorInputWaggon = ValueToColorConverter.ButtonBackgroundColor(_IsInputWaggonEnabled);
        //    }
        //}
        //private System.Drawing.Color _BackgroundColorInputWaggon = ValueToColorConverter.ButtonBackgroundColor(false);
        //public System.Drawing.Color BackgroundColorInputWaggon
        //{
        //    get { return _BackgroundColorInputWaggon; }
        //    set { SetProperty(ref _BackgroundColorInputWaggon, value); }
        //}

        /////-----------------------------------------------------------------------------------------------------------------------------------------
        ///// <summary>
        /////                 Driver Data
        ///// </summary>
        //private string driver = string.Empty;
        //public string Driver
        //{
        //    get { return driver; }
        //    set
        //    {
        //        SetProperty(ref driver, value);
        //        if ((SelectedEingang is Eingaenge) && (SelectedEingang.Id > 0))
        //        {
        //            if (!SelectedEingang.Fahrer.Equals(driver))
        //            {
        //                Eingaenge tmpEA = SelectedEingang.Copy();
        //                tmpEA.WaggonNr = string.Empty;
        //                tmpEA.IsWaggon = false;
        //                tmpEA.Fahrer = driver;
        //                SelectedEingang = tmpEA;
        //            }
        //        }
        //    }
        //}
        //private bool isBtnDriverEnabled = false;
        //public bool IsBtnDriverEnabled
        //{
        //    get { return isBtnDriverEnabled; }
        //    set { SetProperty(ref isBtnDriverEnabled, value); }
        //}

        //private bool _IsInputDriverEnabled = true;
        //public bool IsInputDriverEnabled
        //{
        //    get { return _IsInputDriverEnabled; }
        //    set
        //    {
        //        SetProperty(ref _IsInputDriverEnabled, value);
        //        BackgroundColorInputDriver = ValueToColorConverter.ButtonBackgroundColor(_IsInputDriverEnabled);
        //    }
        //}
        //private System.Drawing.Color _BackgroundColorInputDriver = ValueToColorConverter.ButtonBackgroundColor(false);
        //public System.Drawing.Color BackgroundColorInputDriver
        //{
        //    get { return _BackgroundColorInputDriver; }
        //    set { SetProperty(ref _BackgroundColorInputDriver, value); }
        //}

        ////--------------------------------------------------------------------------------------------------------
        ///// <summary>
        /////             LAgertransport Chekbox
        ///// </summary>
        //private bool _Lagertransport = false;
        //public bool Lagertransport
        //{
        //    get { return _Lagertransport; }
        //    set
        //    {
        //        SetProperty(ref _Lagertransport, value);
        //        if (!SelectedEingang.LagerTransport.Equals(_Lagertransport))
        //        {
        //            Eingaenge tmpEA = SelectedEingang.Copy();
        //            tmpEA.LagerTransport = _Lagertransport;
        //            SelectedEingang = tmpEA.Copy();

        //        }
        //    }
        //}

        //private bool _DirectDelivery = false;
        //public bool DirectDelivery
        //{
        //    get { return _DirectDelivery; }
        //    set
        //    {
        //        SetProperty(ref _DirectDelivery, value);
        //        if (!_DirectDelivery.Equals(SelectedEingang.DirektDelivery))
        //        {
        //            Eingaenge tmpEA = SelectedEingang.Copy();
        //            tmpEA.DirektDelivery = _DirectDelivery;
        //            SelectedEingang = tmpEA.Copy();
        //        }
        //    }
        //}

        //private bool _Retoure = false;
        //public bool Retoure
        //{
        //    get { return _Retoure; }
        //    set
        //    {
        //        SetProperty(ref _Retoure, value);
        //        if (!_Retoure.Equals(SelectedEingang.Retoure))
        //        {
        //            Eingaenge tmpEA = SelectedEingang.Copy();
        //            tmpEA.Retoure = _Retoure;
        //            SelectedEingang = tmpEA.Copy();
        //        }
        //    }
        //}

        //private bool _PrintDocuments = false;
        //public bool PrintDocuments
        //{
        //    get { return _PrintDocuments; }
        //    set
        //    {
        //        SetProperty(ref _PrintDocuments, value);
        //        if (!SelectedEingang.PrintActionByScanner.Equals(_PrintDocuments))
        //        {
        //            Eingaenge tmpEA = SelectedEingang.Copy();
        //            tmpEA.PrintActionByScanner = _PrintDocuments;
        //            SelectedEingang = tmpEA.Copy();
        //        }
        //    }
        //}

        //--------------------------------------------------------------------------------------------------------
        /// <summary>
        ///             Checked
        /// </summary>
        /// 

        //private bool _IsChecked = false;
        //public bool IsChecked
        //{
        //    get { return _IsChecked; }
        //    set
        //    {
        //        SetProperty(ref _IsChecked, value);
        //        if (!SelectedEingang.Check.Equals(_IsChecked))
        //        {
        //            Eingaenge tmpEA = SelectedEingang.Copy();
        //            tmpEA.Check = _IsChecked;
        //            SelectedEingang = tmpEA.Copy();
        //        }
        //        if (_IsChecked)
        //        {
        //            BackgroundImage = "lock_128x128.png";
        //        }
        //        else
        //        {
        //            BackgroundImage = "lock_open_128x128.png";
        //        }
        //    }
        //}

        private ImageSource _BackgroundImage; // = Android.Resource.Drawable.lock_open
        public ImageSource BackgroundImage
        {
            get { return _BackgroundImage; }
            set { SetProperty(ref _BackgroundImage, value); }
        }
    }
}
