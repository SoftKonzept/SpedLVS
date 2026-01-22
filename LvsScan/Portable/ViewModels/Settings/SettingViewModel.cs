using LvsScan.Portable.Views.Menu;
using LvsScan.Portable.Views.Settings;
using LvsScan.Portable.Views.Test;
using System.Collections.ObjectModel;

namespace LvsScan.Portable.ViewModels.Settings
{
    public class SettingViewModel : BaseViewModel
    {
        public SettingViewModel()
        {
            SubMenuItems = new ObservableCollection<MenuSubItem>(new[]
                {
                    new MenuSubItem
                    {
                        Id = 0,
                        Title = "Netzwerk-Check",
                        SubText = "Führt eine Prüfung der erreichbaren Netzwerke durch",
                        TargetType= typeof(NetworkCheckPage),
                        ArtMainMenu = Enumerations.enumMainMenu.Settings
                    },
                    //new MenuSubItem
                    //{
                    //    Id = 1,
                    //    Title = "Server Connection Check" ,
                    //    SubText = "Führt eine Prüfung der Erreichbarkeit des Webserver (WeatherForecast)",
                    //    TargetType=typeof(WeatherForecastPage),
                    //    ArtMainMenu = Enumerations.enumMainMenu.Settings
                    //},
                    new MenuSubItem
                    {
                        Id = 2,
                        Title = "DB Connection Check Intern",
                        SubText = "Führt eine Prüfung der Erreichbarkeit des DB-Server aus",
                        TargetType=typeof(DbTestConnectionInternPage),
                        ArtMainMenu = Enumerations.enumMainMenu.Settings
                    },
                                        //new MenuSubItem
                    //{
                    //    Id = 3,
                    //    Title = "DB Connection Check Extern",
                    //    SubText = "Führt eine Prüfung der Erreichbarkeit des DB-Server aus",
                    //    TargetType=typeof(DbTestConnectionExternPage),
                    //    ArtMainMenu = Enumerations.enumMainMenu.Settings
                    //},

                });
        }

        public ObservableCollection<MenuSubItem> SubMenuItems { get; set; }
    }
}
