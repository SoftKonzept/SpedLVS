using Common.ApiModels;
using Common.Enumerations;
using Common.Models;
using LvsScan.Portable.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LvsScan.Portable.ViewModels.StoreIn.Manual
{
    public class me_art_InputReferencesViewModel : BaseViewModel
    {
        public me_art_InputReferencesViewModel()
        {
            Init();
        }
        public me_art_InputReferencesViewModel(enumStoreInArt myStoreInArt)
        {
            StoreInArt = myStoreInArt;
            Init();
        }

        public void Init()
        {
            IsBusy = false;
        }

        public enumArticleEdit_Steps CurrentArticleEditStep = enumArticleEdit_Steps.editReferences;

        private enumStoreInArt _StoreInArt = enumStoreInArt.manually;
        public enumStoreInArt StoreInArt
        {
            get { return _StoreInArt; }
            set { SetProperty(ref _StoreInArt, value); }
        }

        private Articles _ArticleOriginal = new Articles();
        public Articles ArticleOriginal
        {
            get { return _ArticleOriginal; }
            set { SetProperty(ref _ArticleOriginal, value); }
        }

        private Eingaenge _SelectedEingang = new Eingaenge();
        public Eingaenge SelectedEingang
        {
            get { return _SelectedEingang; }
            set
            {
                SetProperty(ref _SelectedEingang, value);
            }
        }


        private Articles _SelectedArticle = new Articles();
        public Articles SelectedArticle
        {
            get { return _SelectedArticle; }
            set
            {
                SetProperty(ref _SelectedArticle, value);
                if (
                    (_SelectedArticle is Articles) &&
                    (_SelectedArticle.Id > 0)
                  )
                {
                    ExMaterialnummer = _SelectedArticle.exMaterialnummer;
                    ExBezeichnung = _SelectedArticle.exBezeichnung;
                    GlowDate = _SelectedArticle.GlowDate;
                }
            }
        }

        private string _ExMaterialnummer;
        public string ExMaterialnummer
        {
            get { return _ExMaterialnummer; }
            set
            {
                SetProperty(ref _ExMaterialnummer, value);
                if (!SelectedArticle.exMaterialnummer.Equals(_ExMaterialnummer))
                {
                    Articles tmpArt = SelectedArticle.Copy();
                    tmpArt.exMaterialnummer = _ExMaterialnummer;
                    SelectedArticle = tmpArt.Copy();
                }
            }
        }

        private string _ExBezeichnung;
        public string ExBezeichnung
        {
            get { return _ExBezeichnung; }
            set
            {
                SetProperty(ref _ExBezeichnung, value);
                if (!SelectedArticle.exBezeichnung.Equals(_ExBezeichnung))
                {
                    Articles tmpArt = SelectedArticle.Copy();
                    tmpArt.exBezeichnung = _ExBezeichnung;
                    SelectedArticle = tmpArt.Copy();
                }
            }
        }


        private DateTime _GlowDate = new DateTime(1900, 1, 1);
        public DateTime GlowDate
        {
            get { return _GlowDate; }
            set
            {
                SetProperty(ref _GlowDate, value);
                if (!SelectedArticle.GlowDate.Equals(_GlowDate))
                {
                    Articles tmpArt = SelectedArticle.Copy();
                    tmpArt.GlowDate = _GlowDate;
                    SelectedArticle = tmpArt.Copy();
                }
            }
        }

        public async Task<ResponseArticle> UpdateArticle()
        {
            this.IsBusy = true;

            ResponseArticle resArticle = new ResponseArticle();
            resArticle.Success = false;
            resArticle.Article = SelectedArticle.Copy();
            resArticle.AppProcess = enumAppProcess.StoreIn;
            resArticle.ArticleEdit_Step = CurrentArticleEditStep;
            resArticle.UserId = (int)WizardData.LoggedUser.Id;

            try
            {
                api_Article _api = new api_Article();
                resArticle = await _api.POST_Article_Update_ManualEdit(resArticle);
                if (resArticle.Success)
                {
                    //bReturn = true;
                    SelectedArticle = resArticle.Article.Copy();
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            this.IsBusy = false;
            return resArticle;
        }


        private ImageSource _BackgroundImage; // = Android.Resource.Drawable.lock_open
        public ImageSource BackgroundImage
        {
            get { return _BackgroundImage; }
            set { SetProperty(ref _BackgroundImage, value); }
        }
    }
}
