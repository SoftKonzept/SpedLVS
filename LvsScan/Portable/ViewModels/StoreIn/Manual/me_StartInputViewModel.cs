using Common.Enumerations;
using Common.Helper;
using Common.Models;
using LvsScan.Portable.Services;
using LvsScan.Portable.Views.Menu;
using LvsScan.Portable.Views.StoreIn.Open;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LvsScan.Portable.ViewModels.StoreIn.Manual
{
    public class me_StartInputViewModel : BaseViewModel
    {
        public me_StartInputViewModel()
        {
            Init();
        }
        public me_StartInputViewModel(enumStoreInArt myStoreInArt)
        {
            StoreInArt = myStoreInArt;
            Init();
        }

        public void Init()
        {
            IsBusy = false;
        }

        private bool loadValues = false;
        public bool LoadValues
        {
            get { return loadValues; }
            set
            {
                loadValues = value;
                OnPropertyChanged();
                if (loadValues)
                {
                    Task.Run(() => GetEingang()).Wait();
                }
            }
        }


        //private enumStoreInArt _StoreInArt = enumStoreInArt.NotSet;
        private enumStoreInArt _StoreInArt = enumStoreInArt.manually;
        public enumStoreInArt StoreInArt
        {
            get { return _StoreInArt; }
            set { SetProperty(ref _StoreInArt, value); }
        }

        public void InitMenu()
        {
            List<MenuSubItem> tmpSubMenuItems = new List<MenuSubItem>();

            MenuSubItem tmpSub = new MenuSubItem
            {
                Id = 1,
                Title = "+ Artikel dem Eingang hinzufügen",
                SubText = "Bearbeitung und hinzufügen von Artikel zum aktuellen Eingang",
                TargetType = typeof(oe1_OpenStoreInListPage),
                ArtMainMenu = Enumerations.enumMainMenu.StoreIn,
                ShowButton = false,
                BackgroundColor = ValueToColorConverter.ViewBackgroundColorSubmenu()
            };
            tmpSubMenuItems.Add(tmpSub);

            //tmpSub = new MenuSubItem
            //{
            //    Id = 1,
            //    Title = "manuelle Eingänge",
            //    SubText = "Erfassung und Bearbeitung manuellen Eingängen",
            //    TargetType = typeof(me_wEingang_wizHost),
            //    ArtMainMenu = Enumerations.enumMainMenu.StoreIn,
            //    ShowButton = false,
            //    BackgroundColor = ValueToColorConverter.ViewBackgroundColorSubmenu()
            //};
            tmpSubMenuItems.Add(tmpSub);
            SubMenuItems = null;
            SubMenuItems = new ObservableCollection<MenuSubItem>(tmpSubMenuItems);
        }
        private ObservableCollection<MenuSubItem> _SubMenuItems;
        public ObservableCollection<MenuSubItem> SubMenuItems
        {
            get { return _SubMenuItems; }
            set { SetProperty(ref _SubMenuItems, value); }
        }

        private MenuSubItem _SelectedMenuItem;
        public MenuSubItem SelectedMenuItem
        {
            get { return _SelectedMenuItem; }
            set { SetProperty(ref _SelectedMenuItem, value); }
        }

        private int _SelectedEingangTableId;
        public int SelectedEingangTableId
        {
            get { return _SelectedEingangTableId; }
            set { SetProperty(ref _SelectedEingangTableId, value); }
        }

        private Eingaenge _SelectedEingang;
        public Eingaenge SelectedEingang
        {
            get { return _SelectedEingang; }
            set
            {
                SetProperty(ref _SelectedEingang, value);
                if (_SelectedEingang != null)
                {
                    EingangIdString = "Eingang ID: " + _SelectedEingang.LEingangID.ToString() + " - [" + _SelectedEingang.Id.ToString() + "]";
                    EingangDateString = "vom " + _SelectedEingang.Eingangsdatum.ToString("dd.MM.yyyy");
                    //AusgangDateString += " | Termin: " + _SelectedEingang.Termin.ToString("dd.MM.yyyy HH:mm") + " Uhr";
                    BackgroundColorHead = _SelectedEingang.ViewBackgroundcolor;
                    WorkspaceString = "Arbeitsbereich: " + _SelectedEingang.Workspace.WorkspaceString;

                }
            }
        }

        private List<Articles> _ArticlesInEingang = new List<Articles>();
        public List<Articles> ArticlesInEingang
        {
            get { return _ArticlesInEingang; }
            set { SetProperty(ref _ArticlesInEingang, value); }
        }

        public async Task GetEingang()
        {
            try
            {
                this.IsBusy = true;
                api_Eingang _api = new api_Eingang(WizardData.LoggedUser);
                //(int)WizardData.LoggedUser.Id;
                var result = await _api.GET_Eingang_byId(SelectedEingangTableId);
                this.IsBusy = false;

                if (result.Success)
                {
                    SelectedEingang = result.Eingang.Copy();
                    ArticlesInEingang = new List<Articles>(result.ListEingangArticle);
                }
                else
                {
                    await MessageService.ShowAsync("ACHTUNG", result.Error);
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        private System.Drawing.Color backgroundColorHead = System.Drawing.Color.WhiteSmoke;
        public System.Drawing.Color BackgroundColorHead
        {
            get { return backgroundColorHead; }
            set { SetProperty(ref backgroundColorHead, value); }
        }

        private string _EingangIdString = string.Empty;
        public string EingangIdString
        {
            get { return _EingangIdString; }
            set { SetProperty(ref _EingangIdString, value); }
        }

        private string _EingangDateString = string.Empty;
        public string EingangDateString
        {
            get { return _EingangDateString; }
            set { SetProperty(ref _EingangDateString, value); }
        }
        private string _WorkspaceString = string.Empty;
        public string WorkspaceString
        {
            get { return _WorkspaceString; }
            set { SetProperty(ref _WorkspaceString, value); }
        }

        private string clientString = string.Empty;
        public string ClientString
        {
            get { return clientString; }
            set { SetProperty(ref clientString, value); }
        }
    }
}
