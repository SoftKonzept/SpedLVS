using Common.ApiModels;
using Common.Enumerations;
using Common.Models;
using LvsScan.Portable.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LvsScan.Portable.ViewModels.StoreOut
{
    public class a1w7_InputPrintSettingsViewModel : BaseViewModel
    {
        public a1w7_InputPrintSettingsViewModel()
        {
            init();
        }
        private void init()
        {
            IsBusy = false;
        }

        public enumStoreOutArt_Steps CurrentStep = enumStoreOutArt_Steps.wizStepInputPrint;
        public enumStoreOutArt StoreOutArt = enumStoreOutArt.open;

        public bool IsMenuPrintAction { get; set; } = false;

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


        public ICommand CustomIncreaseCommand
        {
            get { return new Command(this.IncreaseCommandExecute, this.IncreaseCommandCanExecute); }
        }
        private bool IncreaseCommandCanExecute(object arg)
        {
            return (PrintCount >= this.Minimum); ;
        }
        private void IncreaseCommandExecute(object obj)
        {
            double nextValue = this.PrintCount + this.Step;
            if (nextValue <= this.Maximum)
                this.PrintCount = (int)nextValue;
            else
                this.PrintCount = (int)this.Maximum;
        }
        public ICommand CustomDecreaseCommand
        {
            get { return new Command(this.DecreaseCommandExecute, this.DecreaseCommandCanExecute); }
        }
        private bool DecreaseCommandCanExecute(object arg)
        {
            return (PrintCount <= this.Maximum);
        }
        private void DecreaseCommandExecute(object obj)
        {
            double newValue = this.PrintCount - this.Step;
            if (newValue >= this.Minimum)
                this.PrintCount = (int)newValue;
            else
                this.PrintCount = (int)this.Minimum;
        }

        public double Maximum { get; set; } = 10;
        public double Minimum { get; set; } = 1;
        public double Step { get; set; } = 1;



        private int printCount = 1;
        public int PrintCount
        {
            get { return printCount; }
            set { SetProperty(ref printCount, value); }
        }

        private string _SelectedPrinter = string.Empty;
        public string SelectedPrinter
        {
            get { return _SelectedPrinter; }
            set { SetProperty(ref _SelectedPrinter, value); }
        }

        private List<string> _PrinterList = new List<string>();
        public List<string> PrinterList
        {
            get { return _PrinterList; }
            set { SetProperty(ref _PrinterList, value); }
        }

        public async void GetAvailablePrinter()
        {

            //this.IsBusy = true;
            api_SystemSettings _api = new api_SystemSettings(Enumerations.enumHTTPMethodeType.GET);
            try
            {
                var result = await _api.GET_Printerlist();
                if (result != null)
                {
                    if (result.Success)
                    {
                        PrinterList = new List<string>();
                        PrinterList = result.ListPrinters;
                        SelectedPrinter = PrinterList[0];
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
        //public async Task<ResponseAusgang> UpdateAusgang()
        //{
        //    //this.IsBusy = true;
        //    api_Ausgang _apiAusgang = new api_Ausgang(WizardData.LoggedUser);
        //    ResponseAusgang resAusgang = new ResponseAusgang();
        //    //resAusgang.WizStoreOut_Ausgang = enumWizStoreOut_Ausgang.wizStepLast;
        //    resAusgang.StoreOutArt = WizardData.Wiz_StoreOut.StoreOutArt;
        //    resAusgang.StoreOutArt_Steps = CurrentStep;
        //    resAusgang.Ausgang = SelectedAusgang.Copy();
        //    resAusgang.UserId = (int)WizardData.LoggedUser.Id;
        //    resAusgang.PrintCount = this.WizardData.Wiz_StoreOut.PrintCount;
        //    resAusgang.PrinterName = this.WizardData.Wiz_StoreOut.PrinterName;

        //    try
        //    {
        //        var result = await _apiAusgang.POST_Ausgang_Update_WizStoreOut(resAusgang);
        //        resAusgang.Success = result.Success;
        //        if (result.Success)
        //        {
        //            SelectedAusgang = result.Ausgang.Copy();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string str = ex.Message;
        //    }
        //    return resAusgang;
        //}


        public async Task<ResponsePrintQueue> AddPrintAction()
        {
            //this.IsBusy = true;

            ResponsePrintQueue responsePrintQueue = new ResponsePrintQueue();
            responsePrintQueue.StoreOutArt = WizardData.Wiz_StoreOut.StoreOutArt;
            responsePrintQueue.StoreOutArt_Steps = CurrentStep;
            responsePrintQueue.SelectedAusgang = SelectedAusgang.Copy();
            responsePrintQueue.UserId = (int)WizardData.LoggedUser.Id;
            responsePrintQueue.PrintCount = PrintCount;
            responsePrintQueue.PrinterName = SelectedPrinter;
            //responsePrintQueue.PrintCount = this.WizardData.Wiz_StoreOut.PrintCount;
            //responsePrintQueue.PrinterName = this.WizardData.Wiz_StoreOut.PrinterName;

            try
            {
                api_PrintQueue api = new api_PrintQueue(WizardData.LoggedUser);
                var result = await api.POST_PrintQueue_Add(responsePrintQueue);
                responsePrintQueue = result.Copy();
                if (result.Success)
                {
                    SelectedAusgang = result.SelectedAusgang.Copy();
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            return responsePrintQueue;
        }
    }
}
