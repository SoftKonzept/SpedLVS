using Common.ApiModels;
using Common.Enumerations;
using Common.Models;
using LvsScan.Portable.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LvsScan.Portable.ViewModels.Wizard
{
    public class wiz_SignatureViewModel : BaseViewModel
    {
        public wiz_SignatureViewModel()
        {
            Init();
        }

        public void Init()
        {
            IsBusy = false;

        }

        private enumStoreInArt_Steps _CurrentStepStoreInArt = enumStoreInArt_Steps.NotSet;
        public enumStoreInArt_Steps CurrentStepStoreInArt
        {
            get { return _CurrentStepStoreInArt; }
            set { SetProperty(ref _CurrentStepStoreInArt, value); }
        }
        private enumStoreOutArt_Steps _CurrentStepStoreOutArt = enumStoreOutArt_Steps.NotSet;
        public enumStoreOutArt_Steps CurrentStepStoreOutArt
        {
            get { return _CurrentStepStoreOutArt; }
            set { SetProperty(ref _CurrentStepStoreOutArt, value); }
        }

        private enumAppProcess _AppProcess = enumAppProcess.NotSet;
        public enumAppProcess AppProcess
        {
            get { return _AppProcess; }
            set { SetProperty(ref _AppProcess, value); }
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
            set
            {
                SetProperty(ref _SelectedAusgang, value);
                Workspace = _SelectedAusgang.Workspace.Copy();
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

        private bool _BtnSaveEnabeld = false;
        public bool BtnSaveEnabeld
        {
            get { return _BtnSaveEnabeld; }
            set { SetProperty(ref _BtnSaveEnabeld, value); }
        }

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

        private bool _SignatureSavedOk = false;
        public bool SignatureSavedOk
        {
            get { return _SignatureSavedOk; }
            set { SetProperty(ref _SignatureSavedOk, value); }
        }

        private bool _Signed = false;
        public bool Signed
        {
            get { return _Signed; }
            set { SetProperty(ref _Signed, value); }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private Images InitImages()
        {
            Images img = new Images();
            img.AuftragTableID = 0;
            img.AuftragPosTableID = 0;
            img.LEingangTableID = 0;
            img.LAusgangTableID = 0;
            img.PicNum = 0;
            img.Pfad = string.Empty;
            img.ScanFilename = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_IMAGE.jpg";
            //img.ImageArt = "Bilder";
            img.ImageArt = enumImageArt.Signature.ToString();
            img.TableId = 0;
            img.IsForSPLMessage = false;
            img.Created = DateTime.Now;
            img.WorkspaceId = 0;

            switch (AppProcess)
            {
                //case enumAppProcess.StoreIn:
                //    switch (WizardData.Wiz_StoreIn.ImageOwner)
                //    {
                //        case enumImageOwner.Artikel:
                //            if ((SelectedArticle is Articles) && (SelectedArticle.Id > 0))
                //            {
                //                img.ScanFilename = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "Artikel" + SelectedArticle.LVS_ID + "_" + SelectedArticle.Id + ".jpg";
                //                img.TableName = enumDatabaseTableNames.Artikel.ToString();
                //                img.TableId = SelectedArticle.Id;
                //                img.WorkspaceId = SelectedArticle.AbBereichID;
                //            }
                //            break;
                //        case enumImageOwner.Eingang:
                //            if ((SelectedEingang is Eingaenge) && (SelectedEingang.Id > 0))
                //            {
                //                img.LEingangTableID = SelectedEingang.Id;
                //                img.ScanFilename = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_E" + SelectedEingang.LEingangID + "_" + SelectedEingang.Id + ".jpg";
                //                img.TableName = enumDatabaseTableNames.LEingang.ToString();
                //                img.TableId = SelectedEingang.Id;
                //                img.WorkspaceId = SelectedEingang.Workspace.Id;
                //            }
                //            break;
                //    }
                //    break;
                case enumAppProcess.StoreOut:
                    switch (WizardData.Wiz_StoreOut.ImageOwner)
                    {
                        //case enumImageOwner.Artikel:
                        //    if ((SelectedArticle is Articles) && (SelectedArticle.Id > 0))
                        //    {
                        //        img.ScanFilename = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "Artikel" + SelectedArticle.LVS_ID + "_" + SelectedArticle.Id + ".jpg";
                        //        img.TableName = enumDatabaseTableNames.Artikel.ToString();
                        //        img.TableId = SelectedArticle.Id;
                        //        img.WorkspaceId = SelectedArticle.AbBereichID;
                        //    }
                        //    break;
                        case enumImageOwner.Ausgang:
                            if ((SelectedAusgang is Ausgaenge) && (SelectedAusgang.Id > 0))
                            {
                                img.LAusgangTableID = SelectedAusgang.Id;
                                img.ScanFilename = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "A" + SelectedAusgang.LAusgangID + "_" + SelectedAusgang.Id + ".jpg";
                                img.TableName = enumDatabaseSped4_TableNames.LAusgang.ToString();
                                img.TableId = SelectedAusgang.Id;
                                img.WorkspaceId = SelectedAusgang.Workspace.Id;
                            }
                            break;
                    }
                    break;

            }
            return img;
        }

        private byte[] _PhotoToSave;
        public byte[] PhotoToSave
        {
            get { return _PhotoToSave; }
            set { SetProperty(ref _PhotoToSave, value); }
        }

        private Images _ImageToSave;
        public Images ImageToSave
        {
            get { return _ImageToSave; }
            set { SetProperty(ref _ImageToSave, value); }
        }

        private byte[] _ByteArrayToSave;
        public byte[] ByteArrayToSave
        {
            get { return _ByteArrayToSave; }
            set
            {
                SetProperty(ref _ByteArrayToSave, value);
                if ((_ByteArrayToSave != null) && (_ByteArrayToSave.Length > 0))
                {
                    ImageToSave = InitImages();
                    ImageToSave.DocImage = _ByteArrayToSave;
                    ImageToSave.Thumbnail = _ByteArrayToSave;
                    //ImageToSave.Thumbnail = helper_XamImage.ResizeImageAndroid(_ByteArrayToSave);
                }
            }
        }

        public async Task<bool> SavePhoto()
        {
            IsBusy = true;
            ResponseImage response = new ResponseImage();
            response.Success = false;
            response.Image = ImageToSave.Copy();
            response.UserId = (int)WizardData.LoggedUser.Id;

            if ((response.Image.DocImage != null) && (response.Image.DocImage.Length > 0))
            {
                api_Image api = new api_Image();
                var res = await api.Post_Image_Add(response);
                if (res.Success)
                {
                    response = res.Copy();
                }
                SignatureSavedOk = res.Success;
            }
            IsBusy = false;
            return response.Success; // Task.FromResult(res);
        }
    }
}
