using Common.ApiModels;
using Common.Enumerations;
using Common.Models;
using LvsScan.Portable.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LvsScan.Portable.ViewModels.StoreIn.Manual
{
    public class me_art_InputWeightViewModel : BaseViewModel
    {
        public me_art_InputWeightViewModel()
        {
            Init();
        }
        public me_art_InputWeightViewModel(enumStoreInArt myStoreInArt)
        {
            StoreInArt = myStoreInArt;
            Init();
        }

        public void Init()
        {
            IsBusy = false;
        }

        public enumArticleEdit_Steps CurrentArticleEditStep = enumArticleEdit_Steps.ediWeight;

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
                    AnzahlString = _SelectedArticle.Anzahl.ToString();
                    BruttoString = _SelectedArticle.Brutto.ToString("#0");
                    NettoString = _SelectedArticle.Netto.ToString("#0");
                }
            }
        }

        private string _AnzahlString;
        public string AnzahlString
        {
            get { return _AnzahlString; }
            set
            {
                SetProperty(ref _AnzahlString, value);
                int iTmp = 1;
                int.TryParse(_AnzahlString, out iTmp);
                Anzahl = iTmp;
            }
        }

        private int _Anzahl;
        public int Anzahl
        {
            get { return _Anzahl; }
            set
            {
                SetProperty(ref _Anzahl, value);
                if (!SelectedArticle.Anzahl.Equals(_Anzahl))
                {
                    Articles tmpArt = SelectedArticle.Copy();
                    tmpArt.Anzahl = _Anzahl;
                    SelectedArticle = tmpArt.Copy();
                }
            }
        }

        private string _Einheit;
        public string Einheit
        {
            get { return _Einheit; }
            set
            {
                SetProperty(ref _Einheit, value);
                if (!SelectedArticle.Einheit.Equals(_Einheit))
                {
                    Articles tmpArt = SelectedArticle.Copy();
                    tmpArt.Einheit = _Einheit;
                    SelectedArticle = tmpArt.Copy();
                }
            }
        }

        private string _BruttoString;
        public string BruttoString
        {
            get { return _BruttoString; }
            set
            {
                SetProperty(ref _BruttoString, value);
                decimal decTmp = 0M;
                decimal.TryParse(_BruttoString, out decTmp);
                Brutto = decTmp;
            }
        }

        private decimal _Brutto;
        public decimal Brutto
        {
            get { return _Brutto; }
            set
            {
                SetProperty(ref _Brutto, value);
                if (!SelectedArticle.Brutto.Equals(_Brutto))
                {
                    Articles tmpArt = SelectedArticle.Copy();
                    tmpArt.Brutto = _Brutto;
                    if (tmpArt.Netto == 0)
                    {
                        tmpArt.Netto = _Brutto;
                    }
                    SelectedArticle = tmpArt.Copy();
                }
            }
        }


        private string _NettoString;
        public string NettoString
        {
            get { return _NettoString; }
            set
            {
                SetProperty(ref _NettoString, value);
                decimal decTmp = 0M;
                decimal.TryParse(_NettoString, out decTmp);
                Netto = decTmp;
            }
        }

        private decimal _Netto;
        public decimal Netto
        {
            get { return _Netto; }
            set
            {
                SetProperty(ref _Netto, value);
                if (!SelectedArticle.Netto.Equals(_Netto))
                {
                    Articles tmpArt = SelectedArticle.Copy();
                    tmpArt.Netto = _Netto;
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
