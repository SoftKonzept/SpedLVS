using Common.ApiModels;
using Common.Enumerations;
using Common.Models;
using LvsScan.Portable.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LvsScan.Portable.ViewModels.StoreIn.Manual
{
    public class me_art_InputDimensionsNoViewModel : BaseViewModel
    {
        public me_art_InputDimensionsNoViewModel()
        {
            Init();
        }
        public me_art_InputDimensionsNoViewModel(enumStoreInArt myStoreInArt)
        {
            StoreInArt = myStoreInArt;
            Init();
        }

        public void Init()
        {
            IsBusy = false;

        }

        public enumArticleEdit_Steps CurrentArticleEditStep = enumArticleEdit_Steps.editDimension;

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
                    //Dicke = _SelectedArticle.Dicke;
                    //Breite = _SelectedArticle.Breite;
                    //Laenge = _SelectedArticle.Laenge;
                    //Hoehe = _SelectedArticle.Hoehe;

                    //DickeString = _SelectedArticle.Dicke.ToString("#0.00");
                    //BreiteString = _SelectedArticle.Breite.ToString("#0.00");
                    //LaengeString = _SelectedArticle.Laenge.ToString("#0.00");
                    //HoeheString = _SelectedArticle.Hoehe.ToString("#0.00");
                    DickeString = _SelectedArticle.Dicke.ToString();
                    BreiteString = _SelectedArticle.Breite.ToString();
                    LaengeString = _SelectedArticle.Laenge.ToString();
                    HoeheString = _SelectedArticle.Hoehe.ToString();
                }
            }
        }

        private string _DickeString;
        public string DickeString
        {
            get { return _DickeString; }
            set
            {
                SetProperty(ref _DickeString, value);
                decimal decTmp = 0M;
                decimal.TryParse(_DickeString, out decTmp);
                Dicke = decTmp;
            }
        }

        private decimal _Dicke;
        public decimal Dicke
        {
            get { return _Dicke; }
            set
            {
                SetProperty(ref _Dicke, value);
                if (!SelectedArticle.Dicke.Equals(_Dicke))
                {
                    Articles tmpArt = SelectedArticle.Copy();
                    tmpArt.Dicke = _Dicke;
                    SelectedArticle = tmpArt.Copy();
                }
            }
        }


        private string _BreiteString;
        public string BreiteString
        {
            get { return _BreiteString; }
            set
            {
                SetProperty(ref _BreiteString, value);
                decimal decTmp = 0M;
                decimal.TryParse(_BreiteString, out decTmp);
                Breite = decTmp;
            }
        }
        private decimal _Breite;
        public decimal Breite
        {
            get { return _Breite; }
            set
            {
                SetProperty(ref _Breite, value);
                if (!SelectedArticle.Breite.Equals(_Breite))
                {
                    Articles tmpArt = SelectedArticle.Copy();
                    tmpArt.Breite = _Breite;
                    SelectedArticle = tmpArt.Copy();
                }
            }
        }

        private string _LaengeString;
        public string LaengeString
        {
            get { return _LaengeString; }
            set
            {
                SetProperty(ref _LaengeString, value);
                decimal decTmp = 0M;
                decimal.TryParse(_LaengeString, out decTmp);
                Laenge = decTmp;
            }
        }

        private decimal _Laenge;
        public decimal Laenge
        {
            get { return _Laenge; }
            set
            {
                SetProperty(ref _Laenge, value);
                if (!SelectedArticle.Laenge.Equals(_Laenge))
                {
                    Articles tmpArt = SelectedArticle.Copy();
                    tmpArt.Laenge = _Laenge;
                    SelectedArticle = tmpArt.Copy();
                }
            }
        }


        private string _HoeheString;
        public string HoeheString
        {
            get { return _HoeheString; }
            set
            {
                SetProperty(ref _HoeheString, value);
                decimal decTmp = 0M;
                decimal.TryParse(_HoeheString, out decTmp);
                Hoehe = decTmp;
            }
        }
        private decimal _Hoehe;
        public decimal Hoehe
        {
            get { return _Hoehe; }
            set
            {
                SetProperty(ref _Hoehe, value);
                if (!SelectedArticle.Hoehe.Equals(_Hoehe))
                {
                    Articles tmpArt = SelectedArticle.Copy();
                    tmpArt.Hoehe = _Hoehe;
                    SelectedArticle = tmpArt.Copy();
                }
            }
        }
        /// <summary>
        ///             Update über den Artikeldatensatz wird durchgeführt
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseArticle> UpdateArticle()
        {
            bool bReturn = false;
            this.IsBusy = true;

            if (SelectedArticle == null || SelectedArticle.Id <= 0)
            {
                await Application.Current.MainPage.DisplayAlert("Fehler", "Der Artikel ist ungültig oder nicht ausgewählt.", "OK");
                return new ResponseArticle { Success = false };
            }
            else
            {
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

                    this.IsBusy = false;
                    if (resArticle.Success)
                    {
                        bReturn = true;
                        SelectedArticle = resArticle.Article.Copy();
                    }
                }
                catch (Exception ex)
                {
                    string str = ex.Message;
                }
                return resArticle;
            }
        }


        private ImageSource _BackgroundImage; // = Android.Resource.Drawable.lock_open
        public ImageSource BackgroundImage
        {
            get { return _BackgroundImage; }
            set { SetProperty(ref _BackgroundImage, value); }
        }
    }
}
