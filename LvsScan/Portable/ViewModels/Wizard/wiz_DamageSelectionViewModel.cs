using Common.ApiModels;
using Common.Enumerations;
using Common.Models;
using Common.Views;
using LvsScan.Portable.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.XamarinForms.Primitives;
using Xamarin.Forms;

namespace LvsScan.Portable.ViewModels.Wizard
{
    public class wiz_DamageSelectionViewModel : BaseViewModel
    {
        public wiz_DamageSelectionViewModel()
        {

        }

        private bool _LoadValues;
        public bool LoadValues
        {
            get { return _LoadValues; }
            set
            {
                SetProperty(ref _LoadValues, value);
                if (_LoadValues)
                {
                    Task.Run(() => GetDamagesList()).Wait();
                    Task.Run(() => GetArticle()).Wait();
                    Task.Run(() => GetArticleDamagesList()).Wait();
                }
            }
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
        private enumStoreInArt _StoreInArt = enumStoreInArt.NotSet;
        public enumStoreInArt StoreInArt
        {
            get { return _StoreInArt; }
            set { SetProperty(ref _StoreInArt, value); }
        }

        private Damages _SelectedDamage;
        public Damages SelectedDamage
        {
            get { return _SelectedDamage; }
            set { SetProperty(ref _SelectedDamage, value); }
        }

        private DamageArticleAssignmentView _SelectedArticleDamage;
        public DamageArticleAssignmentView SelectedArticleDamage
        {
            get { return _SelectedArticleDamage; }
            set { SetProperty(ref _SelectedArticleDamage, value); }
        }

        private List<Damages> _Damages = new List<Damages>();
        public List<Damages> Damages
        {
            get { return _Damages; }
            set
            {
                SetProperty(ref _Damages, value);
                DamagesCritical = _Damages.Where(x => x.DamageArt == enumDamageArt.criticalDamage).ToList();
                DamagesSlight = _Damages.Where(x => x.DamageArt == enumDamageArt.slightDamage).ToList();
            }
        }

        private List<Damages> _DamagesCritical = new List<Damages>();
        public List<Damages> DamagesCritical
        {
            get { return _DamagesCritical; }
            set { SetProperty(ref _DamagesCritical, value); }
        }
        private List<Damages> _DamagesSlight = new List<Damages>();
        public List<Damages> DamagesSlight
        {
            get { return _DamagesSlight; }
            set { SetProperty(ref _DamagesSlight, value); }
        }

        private List<DamageArticleAssignmentView> _DamageArticleAssignmentList = new List<DamageArticleAssignmentView>();
        public List<DamageArticleAssignmentView> DamageArticleAssignmentList
        {
            get { return _DamageArticleAssignmentList; }
            set
            {
                SetProperty(ref _DamageArticleAssignmentList, value);
                List<Damages> tmpList = new List<Damages>(Damages);
                foreach (var item in _DamageArticleAssignmentList)
                {
                    Damages tmp = tmpList.FirstOrDefault(x => x.Id == item.DamageId);
                    if ((tmp != null) && (tmp.Id.Equals(item.DamageId)))
                    {
                        tmpList.Remove(tmp);
                    }
                }
                Damages = tmpList;
            }
        }

        private TabViewItem _SelectedTabViewItem;
        public TabViewItem SelectedTabViewItem
        {
            get { return _SelectedTabViewItem; }
            set
            {
                SetProperty(ref _SelectedTabViewItem, value);
            }
        }

        private bool _ShowDamageAssignment = false;
        public bool ShowDamageAssignment
        {
            get { return _ShowDamageAssignment; }
            set { SetProperty(ref _ShowDamageAssignment, value); }
        }

        private bool _ShowArticleDamageList = true;
        public bool ShowArticleDamageList
        {
            get { return _ShowArticleDamageList; }
            set { SetProperty(ref _ShowArticleDamageList, value); }
        }

        private Articles _selectedArticle = new Articles();
        public Articles SelectedArticle
        {
            get { return _selectedArticle; }
            set { SetProperty(ref _selectedArticle, value); }
        }

        public async Task<bool> GetDamagesList()
        {
            IsBusy = true;

            ResponseDamage response = new ResponseDamage();
            response.Success = false;

            api_Damage _api = new api_Damage();
            if (Damages.Count < 1)
            {
                var result = await _api.GET_DamageList();
                response = result.Copy();
                if (response.Success)
                {
                    Damages = response.ListDamages.ToList();
                }
            }
            IsBusy = false;

            return response.Success;
        }

        public async Task<bool> GetArticleDamagesList()
        {
            IsBusy = true;
            api_Damage _api = new api_Damage();
            var result = await _api.GET_DamageArticleList(SelectedArticle.Id, (int)((App)Application.Current).LoggedUser.Id);
            if (result.Success)
            {
                DamageArticleAssignmentList = result.ListDamagesArticleAssignments.ToList();
            }
            IsBusy = false;

            return result.Success;
        }

        public async Task<bool> GetArticle()
        {
            if (SelectedArticle.Id > 0)
            {
                IsBusy = true;

                ResponseArticle resArticle = new ResponseArticle();
                resArticle.AppProcess = AppProcess;

                api_Article _apiArticle = new api_Article();
                var result = await _apiArticle.GET_Article_ById(SelectedArticle.Id, (int)((App)Application.Current).LoggedUser.Id);
                IsBusy = false;
                SelectedArticle = result.Copy();
            }
            return true;
        }

        public async Task<bool> DeleteDamageArticleAssignmentItem()
        {
            IsBusy = true;

            ResponseDamage response = new ResponseDamage();
            response.Article = SelectedArticle.Copy();
            response.DamageArticleAssignment = SelectedArticleDamage.Copy();
            response.Success = false;
            response.UserId = (int)((App)Application.Current).LoggedUser.Id;

            api_Damage _api = new api_Damage();
            var result = await _api.DELETE_Damage_DamageArticleAssignmentItem(response);
            response = result.Copy();
            if (response.Success)
            {
                DamageArticleAssignmentList = response.ListDamagesArticleAssignments.ToList();
            }

            IsBusy = false;
            return true;
        }

        public async Task<bool> AddDamageArticleAssignmentItem()
        {
            IsBusy = true;

            ResponseDamage response = new ResponseDamage();
            response.Article = SelectedArticle.Copy();
            response.Damage = SelectedDamage.Copy();
            response.DamageArticleAssignment = new DamageArticleAssignmentView();
            response.Success = false;
            response.UserId = (int)((App)Application.Current).LoggedUser.Id;

            api_Damage _api = new api_Damage();
            var result = await _api.POST_Damage_AddDamageArticleAssignment(response);
            response = result.Copy();
            if (response.Success)
            {
                DamageArticleAssignmentList = response.ListDamagesArticleAssignments.ToList();
                ShowArticleDamageList = true;
                SelectedDamage = new Damages();
            }
            IsBusy = false;
            return true;
        }

    }
}
