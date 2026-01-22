using Common.Models;
using LvsScan.Portable.Views.Home;
using LvsScan.Portable.Views.Inventory;
using LvsScan.Portable.Views.Logout;
using LvsScan.Portable.Views.Menu;
using LvsScan.Portable.Views.Settings;
using LvsScan.Portable.Views.StoredLocation;
using LvsScan.Portable.Views.StoreIn;
using LvsScan.Portable.Views.StoreOut;
using System.Collections.ObjectModel;
using Xamarin.Essentials;

namespace LvsScan.Portable.ViewModels.Menu
{
    public class FlyoutMenuPageFlyoutViewModel : BaseViewModel
    {
        /// <summary>
        ///             Flyoutmenü Hauptmenü auf der linken Seite
        /// </summary>
        public FlyoutMenuPageFlyoutViewModel()
        {
            string str = string.Empty;
            MenuItems = new ObservableCollection<FlyoutMenuPageFlyoutMenuItem>(new[]
            {
                    new FlyoutMenuPageFlyoutMenuItem { Id = 0, Title = "Home", TargetType= typeof(HomePage), IconSource="home_32x32.png" },
                    new FlyoutMenuPageFlyoutMenuItem { Id = 1, Title = "Einlagerung", TargetType=typeof(SubMenuStoreInPage) ,IconSource="mf_box_into_32x32.png"},
                    new FlyoutMenuPageFlyoutMenuItem { Id = 2, Title = "Auslagerung", TargetType=typeof(SubMenuStoreOutPage) ,IconSource="mf_box_out_32x32.png"},
                    new FlyoutMenuPageFlyoutMenuItem { Id = 3, Title = "Inventur", TargetType=typeof(i1_InventoryPage), IconSource="mf_shelf_full_32x32.png" },
                    new FlyoutMenuPageFlyoutMenuItem { Id = 4, Title = "Umlagerung", TargetType=typeof(sl1_ArticleSelectionPage), IconSource="mf_document_pinned_32x32.png"},
                    new FlyoutMenuPageFlyoutMenuItem { Id = 5, Title = "Einstellungen", TargetType=typeof(SettingPage), IconSource = "home_32x32.png"},
                    //new FlyoutMenuPageFlyoutMenuItem { Id = 6, Title = "Über uns", TargetType=typeof(AboutUsPage), IconSource="information_32x32.png"},
                    new FlyoutMenuPageFlyoutMenuItem { Id = 7, Title = "Logout", TargetType=typeof(LogoutPage), IconSource="log_in_32x32.png" },
                    //new FlyoutMenuPageFlyoutMenuItem { Id = 8, Title = "TEST", TargetType=typeof(me_wArticleInput_wizHost), IconSource="log_in_32x32.png" },
             });


            VersionTracking.Track();
            // Current app version (2.0.0)
            VersionString = VersionTracking.CurrentVersion;

            // Current build (2)
            BuiltString = VersionTracking.CurrentBuild;
        }

        public ObservableCollection<FlyoutMenuPageFlyoutMenuItem> MenuItems { get; set; }

        private Users _infoUser;
        public Users InfoUser
        {
            get { return _infoUser; }
            set
            {
                SetProperty(ref _infoUser, value);
                if ((_infoUser is Users) && (_infoUser.Id > 0))
                {
                    LoginName = string.Format("Loginname: {0}", _infoUser.LoginName);

                    string strVorname = string.Empty;
                    string strNachname = string.Empty;
                    if (!InfoUser.Vorname.Equals(string.Empty))
                    {
                        strVorname += InfoUser.Vorname + " ";
                    }
                    if (!InfoUser.Name.Equals(string.Empty))
                    {
                        strNachname += InfoUser.Name;
                    }
                    InfoText = string.Format("User: {0} {1}", strVorname, strNachname);
                }
            }
        }

        private string _loginName = string.Empty;
        public string LoginName
        {
            get { return _loginName; }
            set { SetProperty(ref _loginName, value); }
        }

        private string _infoText = string.Empty;
        public string InfoText
        {
            get { return _infoText; }
            set { SetProperty(ref _infoText, value); }
        }


        private string _VersionString = string.Empty;
        public string VersionString
        {
            get { return _VersionString; }
            set { SetProperty(ref _VersionString, value); }
        }

        private string _BuiltString = string.Empty;
        public string BuiltString
        {
            get { return _BuiltString; }
            set { SetProperty(ref _BuiltString, value); }
        }

        private string _BuiltVersionString = string.Empty;
        public string BuiltVersionString
        {
            get
            {
                _BuiltVersionString = string.Format("Version/Built: {0} / {1}", VersionString, BuiltString);
                return _BuiltVersionString;
            }
        }
    }
}
