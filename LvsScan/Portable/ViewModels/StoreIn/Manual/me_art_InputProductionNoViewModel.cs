using Common.ApiModels;
using Common.Customize;
using Common.Enumerations;
using Common.Models;
using LvsScan.Portable.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LvsScan.Portable.ViewModels.StoreIn.Manual
{
    public class me_art_InputProductionNoViewModel : BaseViewModel
    {
        public me_art_InputProductionNoViewModel()
        {
            Init();
        }
        public me_art_InputProductionNoViewModel(enumStoreInArt myStoreInArt)
        {
            StoreInArt = myStoreInArt;
            Init();
        }

        public async void Init()
        {
            IsBusy = false;
            await Task.Run(() => GetGoodstypesbyWorkspace());
        }

        public async Task<ResponseEingang> GetEingang()
        {
            IsBusy = true;
            ResponseEingang resEA = new ResponseEingang();

            api_Eingang _api = new api_Eingang(WizardData.LoggedUser);
            try
            {
                resEA = await _api.GET_Eingang_byId(SelectedEingang.Id);
                if (resEA.Success)
                {
                    SelectedEingang = resEA.Eingang.Copy();
                    EingangOriginal = resEA.Eingang.Copy();
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            IsBusy = false;
            return resEA;
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
            }
        }

        private Articles _ArticleCreated = new Articles();
        public Articles ArticleCreated
        {
            get { return _ArticleCreated; }
            set
            {
                SetProperty(ref _ArticleCreated, value);
            }
        }

        private Articles _ArticleToCreate = new Articles();
        public Articles ArticleToCreate
        {
            get { return _ArticleToCreate; }
            set
            {
                SetProperty(ref _ArticleToCreate, value);
            }
        }

        private string _GoodstypesString = string.Empty;
        public string GoodstypesString
        {
            get { return _GoodstypesString; }
            set
            {
                SetProperty(ref _GoodstypesString, value);
            }
        }

        private Goodstypes _SelectedGut = new Goodstypes();
        public Goodstypes SelectedGut
        {
            get { return _SelectedGut; }
            set
            {
                SetProperty(ref _SelectedGut, value);
                if (_SelectedGut != null)
                {
                    GoodstypesString = string.Empty;
                    GoodstypesString = "[Id:" + _SelectedGut.Id + "] - " + _SelectedGut.GoodstypeString;
                }
            }
        }
        private Goodstypes _SelectedGutByCombo = new Goodstypes();
        public Goodstypes SelectedGutByCombo
        {
            get { return _SelectedGutByCombo; }
            set
            {
                SetProperty(ref _SelectedGutByCombo, value);
                if (!_SelectedGutByCombo.Id.Equals(SelectedGut.Id))
                {
                    SelectedGut = new Goodstypes();
                    GoodstypesString = string.Empty;
                    Task.Run(() => Task.Delay(1000)).Wait();
                    SelectedGut = _SelectedGutByCombo;
                }
            }
        }
        private ObservableCollection<Goodstypes> _GoodstypesList = new ObservableCollection<Goodstypes>();
        public ObservableCollection<Goodstypes> GoodstypesList
        {
            get { return _GoodstypesList; }
            set { SetProperty(ref _GoodstypesList, value); }
        }

        public async void InitArticleToCreate()
        {
            ArticleToCreate = new Articles();
            ArticleToCreate.Eingang = SelectedEingang.Copy();
            ArticleToCreate.AbBereichID = SelectedEingang.ArbeitsbereichId;
            ArticleToCreate.MandantenID = SelectedEingang.MandantenId;
            ArticleToCreate.LEingangTableID = SelectedEingang.Id;
            ArticleToCreate.Produktionsnummer = Produktionsnummer;
            ArticleToCreate.Charge = ArticleToCreate.Produktionsnummer;
            ArticleToCreate.Werksnummer = Werksnummer;
            ArticleToCreate.GArtID = 1;
            ArticleToCreate.Anzahl = 1;
            ArticleToCreate.Einheit = "kg";
            ArticleToCreate.Position = (SelectedEingang.ArticleCount + 1).ToString();
            ArticleToCreate.IdentifiedByScan = DateTime.Now;

            //var result = GetGoodstypebyWerksnummer();
            if (!Werksnummer.Equals(string.Empty))
            {
                var result = Task.Run(() => GetGoodstypebyWerksnummer()).Result;
                if ((result != null) && (!result.Id.Equals(SelectedGut.Id)))
                {
                    string mesInfo = "ACHTUNG";
                    string message = "Es wurde für die Werksnummer eine hinterlegte Güterart gefunden. Soll diese Güterart übernommen werden?";
                    var resonseDisplayAlert = await App.Current.MainPage.DisplayAlert(mesInfo, message, "Güterart übernehmen", "Abbrechen");
                    if (resonseDisplayAlert)
                    {
                        SelectedGut = result.Copy();
                    }
                }
            }

            if ((SelectedGut is Goodstypes) && (SelectedGut.Id > 0))
            {

                ArticleToCreate.GArtID = SelectedGut.Id;
                ArticleToCreate.GutZusatz = SelectedGut.Zusatz;
                if (SelectedGut.Menge > 0)
                {
                    ArticleToCreate.Anzahl = SelectedGut.Menge;
                }
                ArticleToCreate.Dicke = SelectedGut.Dicke;
                ArticleToCreate.Hoehe = SelectedGut.Hoehe;
                ArticleToCreate.Laenge = SelectedGut.Laenge;
                ArticleToCreate.Breite = SelectedGut.Breite;
                ArticleToCreate.Netto = SelectedGut.Netto;
                ArticleToCreate.Brutto = SelectedGut.Brutto;
                ArticleToCreate.Bestellnummer = SelectedGut.BestellNr;
                if (SelectedGut.Einheit.Length > 0)
                {
                    ArticleToCreate.Einheit = SelectedGut.Einheit;
                }
                ArticleToCreate.IsStackable = SelectedGut.IsStackable;
                ArticleToCreate.Werksnummer = SelectedGut.Werksnummer;
            }
            ArticleToCreate.ArtIDRef = vw_ArticleIdReferenz.CreateByModel(ArticleToCreate);
        }

        public async Task<Goodstypes> GetGoodstypebyWerksnummer()
        {
            this.IsBusy = true;

            ResponseGoodstype resGut = new ResponseGoodstype();
            resGut.Success = false;
            try
            {
                api_Goodstype _api = new api_Goodstype();
                var res = await _api.GET_GoodstypelistByWorkspaceAndAddressAndWerksnummer(SelectedEingang.Workspace.Id, SelectedEingang.Auftraggeber, Werksnummer);
                if (res != null)
                {
                    resGut = res.Copy();
                    Goodstypes tmpGut = GoodstypesList.FirstOrDefault(x => x.Id == resGut.Goodstype.Id);
                    SelectedGutByCombo = tmpGut;
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            this.IsBusy = false;
            return resGut.Goodstype;
        }

        private async Task<Goodstypes> GetGoodstypesbyWorkspace()
        {
            this.IsBusy = true;

            ResponseGoodstype resGut = new ResponseGoodstype();
            resGut.Success = false;
            try
            {
                api_Goodstype _api = new api_Goodstype();
                var res = await _api.GET_GoodstypelistByWorkspace(SelectedEingang.ArbeitsbereichId);
                //if (res != null)
                //{
                //    resGut = res.Copy();
                //}

                if (res.ListGoodstypes.Count > 0)
                {
                    List<Goodstypes> tmpList = new List<Goodstypes>();
                    Goodstypes tmp = res.ListGoodstypes.FirstOrDefault(x => x.Id == 1);
                    tmpList.Add(tmp);
                    res.ListGoodstypes.Remove(tmp);
                    tmpList.AddRange(res.ListGoodstypes.OrderBy(x => x.Bezeichnung));
                    GoodstypesList = new ObservableCollection<Goodstypes>(tmpList);
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            this.IsBusy = false;
            return resGut.Goodstype;
        }
        public async Task<ResponseArticle> CreateNewArticle()
        {
            bool bReturn = false;
            this.IsBusy = true;

            ResponseArticle resArticle = new ResponseArticle();
            resArticle.Success = false;
            resArticle.Article = ArticleToCreate.Copy();
            resArticle.AppProcess = enumAppProcess.StoreIn;
            resArticle.UserId = (int)WizardData.LoggedUser.Id;
            try
            {
                api_Article _api = new api_Article();
                resArticle = await _api.POST_Article_AddByScanner(resArticle);

                this.IsBusy = false;
                if (resArticle.Success)
                {
                    bReturn = true;
                    ArticleCreated = resArticle.Article.Copy();
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            return resArticle;
        }




        private int _CountAll;
        public int CountAll
        {
            get { return _CountAll; }
            set { SetProperty(ref _CountAll, value); }
        }

        public async Task<ResponseEingang> UpdateEingang(enumStoreInArt_Steps myCurrentStep)
        {
            ResponseEingang resEA = new ResponseEingang();
            resEA.Eingang = SelectedEingang.Copy();
            resEA.UserId = (int)WizardData.LoggedUser.Id;
            resEA.StoreInArt = WizardData.Wiz_StoreIn.StoreInArt;
            resEA.StoreInArt_Steps = myCurrentStep;

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



        private ImageSource _BackgroundImage; // = Android.Resource.Drawable.lock_open
        public ImageSource BackgroundImage
        {
            get { return _BackgroundImage; }
            set { SetProperty(ref _BackgroundImage, value); }
        }


        private string _Produktionsnummer = string.Empty;
        public string Produktionsnummer
        {
            get { return _Produktionsnummer; }
            set
            {
                SetProperty(ref _Produktionsnummer, value);
            }
        }
        private string _Werksnummer = string.Empty;
        public string Werksnummer
        {
            get { return _Werksnummer; }
            set
            {
                SetProperty(ref _Werksnummer, value);
            }
        }


    }
}
