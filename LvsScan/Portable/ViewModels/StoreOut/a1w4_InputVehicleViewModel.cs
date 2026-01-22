using Common.ApiModels;
using Common.Enumerations;
using Common.Helper;
using Common.Models;
using LvsScan.Portable.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LvsScan.Portable.ViewModels.StoreOut
{
    public class a1w4_InputVehicleViewModel : BaseViewModel
    {
        public a1w4_InputVehicleViewModel()
        {
            init();
        }

        private async void init()
        {
            IsBusy = false;

            //IsBtnCarrierEnabled = true;
            IsBtnDriverEnabled = true;
            IsBtnKFZEnabled = true;
            IsBtnTrailerEnabled = true;

            //IsInputCarrierEnabled = false;
            IsInputDriverEnabled = false;
            IsInputKFZEnabled = false;
            IsInputTrailerEnabled = false;

            //await Task.Run(() => GetVehicleList());
        }

        private bool _IsStepFinished = false;
        public bool IsStepFinished
        {
            get { return _IsStepFinished; }
            set
            {
                SetProperty(ref _IsStepFinished, value);
                //IsBaseNextEnabeld = !_IsStepFinished;
            }
        }

        public enumStoreOutArt_Steps CurrentStep = enumStoreOutArt_Steps.wizStepInputVehicle;
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

                    //CarriertString = selectedAusgang.Spediteur;

                    KFZ = selectedAusgang.KFZ;
                    Trailer = selectedAusgang.Trailer;
                    Driver = selectedAusgang.Fahrer;

                    if (selectedAusgang.Workspace is Workspaces)
                    {
                        Workspace = selectedAusgang.Workspace.Copy();
                    }

                    IsStepFinished = ((!KFZ.Equals(string.Empty)) && (!Driver.Equals(string.Empty)));
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


        ///-----------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///                 KFZ Data
        /// </summary>
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
                BackgroundColorInputKFZ = ValueToColorConverter.ButtonBackgroundColor(kfz.Length == 0);
            }
        }
        private bool isBtnKFZEnabled = false;
        public bool IsBtnKFZEnabled
        {
            get { return isBtnKFZEnabled; }
            set { SetProperty(ref isBtnKFZEnabled, value); }
        }
        private bool _IsInputKFZEnabled = true;
        public bool IsInputKFZEnabled
        {
            get { return _IsInputKFZEnabled; }
            set
            {
                SetProperty(ref _IsInputKFZEnabled, value);
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
        ///                 Trailer Data
        /// </summary>
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
                BackgroundColorInputTrailer = ValueToColorConverter.ButtonBackgroundColor(trailer.Length == 0);
            }
        }
        private bool isBtnTrailerEnabled = false;
        public bool IsBtnTrailerEnabled
        {
            get { return isBtnTrailerEnabled; }
            set { SetProperty(ref isBtnTrailerEnabled, value); }
        }
        private bool _IsInputTrailerEnabled = true;
        public bool IsInputTrailerEnabled
        {
            get { return _IsInputTrailerEnabled; }
            set
            {
                SetProperty(ref _IsInputTrailerEnabled, value);
            }
        }
        private System.Drawing.Color _BackgroundColorInputTrailer = ValueToColorConverter.ButtonBackgroundColor(false);
        public System.Drawing.Color BackgroundColorInputTrailer
        {
            get { return _BackgroundColorInputTrailer; }
            set { SetProperty(ref _BackgroundColorInputTrailer, value); }
        }

        private Vehicles _PickerSelectedItemVehicleTrailer;
        public Vehicles PickerSelectedItemVehicleTrailer
        {
            get { return _PickerSelectedItemVehicleTrailer; }
            set
            {
                SetProperty(ref _PickerSelectedItemVehicleTrailer, value);
                if (_PickerSelectedItemVehicleTrailer.Id > 0)
                {
                    Trailer = _PickerSelectedItemVehicleTrailer.KFZ;
                }
            }
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
                if ((SelectedAusgang is Ausgaenge) && (SelectedAusgang.Id > 0))
                {
                    if (!SelectedAusgang.Fahrer.Equals(driver))
                    {
                        Ausgaenge tmpAusgang = SelectedAusgang.Copy();
                        tmpAusgang.Fahrer = driver;
                        SelectedAusgang = tmpAusgang;
                    }
                }
                BackgroundColorInputDriver = ValueToColorConverter.ButtonBackgroundColor(driver.Length == 0);
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

            }
        }
        private System.Drawing.Color _BackgroundColorInputDriver = ValueToColorConverter.ButtonBackgroundColor(false);
        public System.Drawing.Color BackgroundColorInputDriver
        {
            get { return _BackgroundColorInputDriver; }
            set { SetProperty(ref _BackgroundColorInputDriver, value); }
        }



        private Workspaces workspace = new Workspaces();
        public Workspaces Workspace
        {
            get { return workspace; }
            set { SetProperty(ref workspace, value); }
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
