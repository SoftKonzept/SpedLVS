using Common.ApiModels;
using Common.Enumerations;
using Common.Models;
using LvsScan.Portable.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LvsScan.Portable.ViewModels.Wizard
{
    public class wiz_ArticleCheckViewModel : BaseViewModel
    {
        public wiz_ArticleCheckViewModel()
        {
            Init();
        }

        public void Init()
        {
            IsBusy = false;

        }

        //private enumStoreInArt_Steps _CurrentStepStoreInArt = enumStoreInArt_Steps.NotSet;
        //public enumStoreInArt_Steps CurrentStepStoreInArt
        //{
        //    get { return _CurrentStepStoreInArt; }
        //    set { SetProperty(ref _CurrentStepStoreInArt, value); }
        //}
        //private enumStoreOutArt_Steps _CurrentStepStoreOutArt = enumStoreOutArt_Steps.;
        //public enumStoreOutArt_Steps CurrentStepStoreOutArt
        //{
        //    get { return _CurrentStepStoreOutArt; }
        //    set { SetProperty(ref _CurrentStepStoreOutArt, value); }
        //}

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
                IsChecked = _SelectedArticle.AusgangChecked;
            }
        }

        private Eingaenge _SelectedEingang = new Eingaenge();
        public Eingaenge SelectedEingang
        {
            get { return _SelectedEingang; }
            set { SetProperty(ref _SelectedEingang, value); }
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
        private bool _IsChecked = false;
        public bool IsChecked
        {
            get { return _IsChecked; }
            set
            {
                SetProperty(ref _IsChecked, value);
                if (!SelectedArticle.AusgangChecked.Equals(_IsChecked))
                {
                    Articles tmpArticle = SelectedArticle.Copy();
                    tmpArticle.AusgangChecked = _IsChecked;
                    SelectedArticle = tmpArticle.Copy();
                }
                if (_IsChecked)
                {
                    BackgroundImage = "lock_128x128.png";
                }
                else
                {
                    BackgroundImage = "lock_open_128x128.png";
                }
            }
        }





        //private System.Drawing.Color backgroundColorHead = System.Drawing.Color.WhiteSmoke;
        //public System.Drawing.Color BackgroundColorHead
        //{
        //    get { return backgroundColorHead; }
        //    set { SetProperty(ref backgroundColorHead, value); }
        //}
        //private string _EingangIdString = string.Empty;
        //public string EingangIdString
        //{
        //    get { return _EingangIdString; }
        //    set { SetProperty(ref _EingangIdString, value); }
        //}

        //private string _EingangDateString = string.Empty;
        //public string EingangDateString
        //{
        //    get { return _EingangDateString; }
        //    set { SetProperty(ref _EingangDateString, value); }
        //}
        private string _WorkspaceString = string.Empty;
        public string WorkspaceString
        {
            get { return _WorkspaceString; }
            set { SetProperty(ref _WorkspaceString, value); }
        }

        //private string clientString = string.Empty;
        //public string ClientString
        //{
        //    get { return clientString; }
        //    set { SetProperty(ref clientString, value); }
        //}


        //private bool _BtnSaveEnabeld = false;
        //public bool BtnSaveEnabeld
        //{
        //    get { return _BtnSaveEnabeld; }
        //    set { SetProperty(ref _BtnSaveEnabeld, value); }
        //}

        public ImageSource ImgSourceSaveOK
        {
            get
            {
                return ImageSource.FromFile("check_256x256.png");
            }
        }
        public ImageSource ImgSourceSaveFailure
        {
            get
            {
                return ImageSource.FromFile("delete_256x256.png");
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        //public async Task<bool> UpdateArticleScanCheck()
        //{
        //    IsBusy = true;

        //    switch (AppProcess)
        //    {
        //        case enumAppProcess.StoreIn:
        //            SelectedArticle.ScanIn = DateTime.Now;
        //            SelectedArticle.ScanInUser = (int)((App)Application.Current).LoggedUser.Id;
        //            break;
        //        case enumAppProcess.StoreOut:
        //            SelectedArticle.ScanOut = DateTime.Now;
        //            SelectedArticle.ScanOutUser = (int)((App)Application.Current).LoggedUser.Id;
        //            break;
        //        case enumAppProcess.StoreLocationChange:
        //            break;
        //        default:
        //            break;
        //    }

        //    ResponseArticle resArticle = new ResponseArticle();
        //    resArticle.Article = SelectedArticle.Copy();
        //    resArticle.AppProcess = AppProcess;
        //    resArticle.UserId= (int)((App)Application.Current).LoggedUser.Id;

        //    api_Article _apiArticle = new api_Article();
        //    var result = await _apiArticle.POST_Article_Update_ScanValue(resArticle);
        //    if(result.Success) 
        //    { 

        //    }
        //    IsBusy = false;
        //    return result.Success;
        //}

        public async Task<bool> UpdateArticleChecked()
        {
            IsBusy = true;

            ResponseArticle resArticle = new ResponseArticle();
            resArticle.Article = SelectedArticle.Copy();
            resArticle.AppProcess = AppProcess;
            resArticle.UserId = (int)((App)Application.Current).LoggedUser.Id;

            api_Article _apiArticle = new api_Article();
            var result = await _apiArticle.POST_Article_Update_Checked(resArticle);
            if (result.Success)
            {

            }
            IsBusy = false;
            return result.Success;
        }
    }
}
