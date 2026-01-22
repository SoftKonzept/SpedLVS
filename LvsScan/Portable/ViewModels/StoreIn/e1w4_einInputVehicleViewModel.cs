using Common.ApiModels;
using Common.Enumerations;
using Common.Helper;
using Common.Models;
using LvsScan.Portable.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LvsScan.Portable.ViewModels.StoreIn
{
    public class e1w4_einInputVehicleViewModel : BaseViewModel
    {
        public e1w4_einInputVehicleViewModel()
        {
            Init();
        }
        public e1w4_einInputVehicleViewModel(enumStoreInArt myStoreInArt)
        {
            StoreInArt = myStoreInArt;
            Init();
        }

        public void Init()
        {
            IsBusy = false;
            IsBtnWaggonEnabled = true;
            IsBtnKFZEnabled = true;
            IsBtnDriverEnabled = true;

        }


        public enumStoreInArt_Steps CurrentStep = enumStoreInArt_Steps.wizStepInputVehicle;

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

                    //// 4. Step
                    SelectedEingang.IsShip = false;
                    SelectedEingang.Ship = string.Empty;
                    if (_SelectedEingang.IsWaggon)
                    {
                        _SelectedEingang.KFZ = string.Empty;
                    }
                    else
                    {
                        _SelectedEingang.WaggonNr = string.Empty;
                    }
                    Waggon = _SelectedEingang.WaggonNr;
                    IsWaggon = _SelectedEingang.IsWaggon;
                    IsKfz = (!IsWaggon);
                    KFZ = _SelectedEingang.KFZ;

                    Driver = _SelectedEingang.Fahrer;
                    switch (StoreInArt)
                    {
                        case enumStoreInArt.manually:
                        case enumStoreInArt.open:
                        case enumStoreInArt.edi:
                            //IsStepFinished = ((!KFZ.Equals(string.Empty)) && (!Driver.Equals(string.Empty)));
                            break;
                        case enumStoreInArt.NotSet:
                            IsStepFinished = false;
                            break;
                    }
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


        ///-----------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///                 KFZ Data
        /// </summary>
        /// 
        private bool _IsKfz = true;
        public bool IsKfz
        {
            get { return _IsKfz; }
            set
            {
                SetProperty(ref _IsKfz, value);
                if (IsWaggon.Equals(_IsKfz))
                {
                    IsWaggon = (!_IsKfz);
                }
                //IsWaggon = (!_IsKfz);
            }
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
                        //tmpEA.WaggonNr = string.Empty;
                        //tmpEA.IsWaggon = false;
                        tmpEA.KFZ = kfz;
                        SelectedEingang = tmpEA.Copy();
                    }
                }
            }
        }
        private bool isBtnKFZEnabled = false;
        public bool IsBtnKFZEnabled
        {
            get { return isBtnKFZEnabled; }
            set { SetProperty(ref isBtnKFZEnabled, value); }
        }
        private bool _IsInputKFZEnabled = false;
        public bool IsInputKFZEnabled
        {
            get { return _IsInputKFZEnabled; }
            set
            {
                SetProperty(ref _IsInputKFZEnabled, value);
                BackgroundColorInputKFZ = ValueToColorConverter.ButtonBackgroundColor(_IsInputKFZEnabled);
            }
        }
        private System.Drawing.Color _BackgroundColorInputKFZ = ValueToColorConverter.ButtonBackgroundColor(false);
        public System.Drawing.Color BackgroundColorInputKFZ
        {
            get { return _BackgroundColorInputKFZ; }
            set { SetProperty(ref _BackgroundColorInputKFZ, value); }
        }

        private Vehicles _PickerSelectedItemVehicleZM;
        public Vehicles PickerSelectedItemVehicleZM
        {
            get { return _PickerSelectedItemVehicleZM; }
            set
            {
                SetProperty(ref _PickerSelectedItemVehicleZM, value);
                if (_PickerSelectedItemVehicleZM.Id > 0)
                {
                    KFZ = _PickerSelectedItemVehicleZM.KFZ;
                }
            }
        }

        ///-----------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///                 Waggon Data
        /// </summary>
        /// 
        private bool _IsWaggon = false;
        public bool IsWaggon
        {
            get { return _IsWaggon; }
            set
            {
                SetProperty(ref _IsWaggon, value);
                if (!SelectedEingang.IsWaggon.Equals(_IsWaggon))
                {
                    Eingaenge tmpEA = SelectedEingang.Copy();
                    tmpEA.IsWaggon = _IsWaggon;
                    //tmpEA.WaggonNr = Waggon;
                    SelectedEingang = tmpEA.Copy();

                }
            }
        }

        private string _Waggon = string.Empty;
        public string Waggon
        {
            get { return _Waggon; }
            set
            {
                SetProperty(ref _Waggon, value);
                if ((SelectedEingang is Eingaenge) && (SelectedEingang.Id > 0))
                {
                    if (!SelectedEingang.WaggonNr.Equals(_Waggon))
                    {
                        Eingaenge tmpEA = SelectedEingang.Copy();
                        //tmpEA.IsWaggon = true;
                        tmpEA.WaggonNr = Waggon;
                        SelectedEingang = tmpEA.Copy();
                    }
                }
            }
        }
        private bool _IsBtnWaggonEnabled = false;
        public bool IsBtnWaggonEnabled
        {
            get { return _IsBtnWaggonEnabled; }
            set { SetProperty(ref _IsBtnWaggonEnabled, value); }
        }
        private bool _IsInputWaggonEnabled = false;
        public bool IsInputWaggonEnabled
        {
            get { return _IsInputWaggonEnabled; }
            set
            {
                SetProperty(ref _IsInputWaggonEnabled, value);
                BackgroundColorInputWaggon = ValueToColorConverter.ButtonBackgroundColor(_IsInputWaggonEnabled);
            }
        }
        private System.Drawing.Color _BackgroundColorInputWaggon = ValueToColorConverter.ButtonBackgroundColor(false);
        public System.Drawing.Color BackgroundColorInputWaggon
        {
            get { return _BackgroundColorInputWaggon; }
            set { SetProperty(ref _BackgroundColorInputWaggon, value); }
        }

        ///-----------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///                 Driver Data
        /// </summary>
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
                        tmpEA.WaggonNr = string.Empty;
                        tmpEA.IsWaggon = false;
                        tmpEA.Fahrer = driver;
                        SelectedEingang = tmpEA;
                    }
                }
            }
        }
        private bool isBtnDriverEnabled = false;
        public bool IsBtnDriverEnabled
        {
            get { return isBtnDriverEnabled; }
            set { SetProperty(ref isBtnDriverEnabled, value); }
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
        private System.Drawing.Color _BackgroundColorInputDriver = ValueToColorConverter.ButtonBackgroundColor(false);
        public System.Drawing.Color BackgroundColorInputDriver
        {
            get { return _BackgroundColorInputDriver; }
            set { SetProperty(ref _BackgroundColorInputDriver, value); }
        }



        private ImageSource _BackgroundImage; // = Android.Resource.Drawable.lock_open
        public ImageSource BackgroundImage
        {
            get { return _BackgroundImage; }
            set { SetProperty(ref _BackgroundImage, value); }
        }

        private List<Vehicles> vehiclesZM = new List<Vehicles>();
        public List<Vehicles> VehiclesZM
        {
            get { return vehiclesZM; }
            set { SetProperty(ref vehiclesZM, value); }
        }

        private List<Vehicles> vehiclesTrailer = new List<Vehicles>();
        public List<Vehicles> VehiclesTrailer
        {
            get { return vehiclesTrailer; }
            set { SetProperty(ref vehiclesTrailer, value); }
        }

        public async Task GetVehicleList()
        {
            this.IsBusy = true;
            try
            {
                api_Vehicle api = new api_Vehicle();
                var result = await api.GET_VehicleList();
                if (result.Success)
                {
                    if (result.ListVehicles.Count > 0)
                    {
                        VehiclesZM = result.ListVehicles.Where(x => x.ZM == 'T').ToList();
                        VehiclesTrailer = result.ListVehicles.Where(x => x.Anhaenger == 'T').ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            this.IsBusy = false;
        }
    }
}
