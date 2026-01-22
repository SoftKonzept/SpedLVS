using Common.ApiModels;
using Common.Enumerations;
using Common.Models;
using LvsScan.Portable.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LvsScan.Portable.ViewModels.Wizard
{
    public class wiz_PrintSettingsViewModel : BaseViewModel
    {
        public wiz_PrintSettingsViewModel()
        {
            Init();
        }

        public void Init()
        {
            IsBusy = false;

        }

        private enumAppProcess _AppProcess = enumAppProcess.NotSet;
        public enumAppProcess AppProcess
        {
            get { return _AppProcess; }
            set { SetProperty(ref _AppProcess, value); }
        }

        private enumStoreInArt _StoreInArt = enumStoreInArt.NotSet;
        public enumStoreInArt StoreInArt
        {
            get { return _StoreInArt; }
            set { SetProperty(ref _StoreInArt, value); }
        }

        private enumStoreOutArt _StoreOutArt = enumStoreOutArt.NotSet;
        public enumStoreOutArt StoreOutArt
        {
            get { return _StoreOutArt; }
            set { SetProperty(ref _StoreOutArt, value); }
        }


        private Ausgaenge _SelectedAusgang = new Ausgaenge();
        public Ausgaenge SelectedAusgang
        {
            get { return _SelectedAusgang; }
            set { SetProperty(ref _SelectedAusgang, value); }
        }

        private Articles _SelectedArticle = new Articles();
        public Articles SelectedArticle
        {
            get { return _SelectedArticle; }
            set
            {
                SetProperty(ref _SelectedArticle, value);
            }
        }

        private Workspaces workspace = new Workspaces();
        public Workspaces Workspace
        {
            get { return workspace; }
            set { SetProperty(ref workspace, value); }
        }

        private ImageSource _BackgroundImage; // = Android.Resource.Drawable.lock_open
        public ImageSource BackgroundImage
        {
            get { return _BackgroundImage; }
            set { SetProperty(ref _BackgroundImage, value); }
        }

        private int _PrintCount = 1;
        public int PrintCount
        {
            get { return _PrintCount; }
            set { SetProperty(ref _PrintCount, value); }
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


        private string _WorkspaceString = string.Empty;
        public string WorkspaceString
        {
            get { return _WorkspaceString; }
            set { SetProperty(ref _WorkspaceString, value); }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public async Task<bool> UpdateArticleScanCheck()
        {
            IsBusy = true;

            SelectedArticle.ScanOut = DateTime.Now;
            SelectedArticle.ScanOutUser = (int)((App)Application.Current).LoggedUser.Id;

            ResponseArticle resArticle = new ResponseArticle();
            resArticle.Article = SelectedArticle.Copy();
            resArticle.AppProcess = AppProcess;
            resArticle.UserId = (int)((App)Application.Current).LoggedUser.Id;



            api_Article _apiArticle = new api_Article();
            var result = await _apiArticle.POST_Article_Update_ScanValue(resArticle);
            if (result.Success)
            {

            }
            IsBusy = false;
            return result.Success;
        }
    }
}
