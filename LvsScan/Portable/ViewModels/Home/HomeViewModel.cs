using LvsScan.Portable.Views.Menu;
using System.Collections.Generic;

namespace LvsScan.Portable.ViewModels.Home
{
    public class HomeViewModel : BaseViewModel
    {

        public HomeViewModel()
        {

            CallArticlesUnChecked = new List<MenuSubItem>(new[]
                {
                    new MenuSubItem
                    {
                        Id = 1,
                        Title = "manueller Eingang",
                        SubText = "Erstellung von manuellen Eingängen",
                        TargetType= null,
                        ArtMainMenu = Enumerations.enumMainMenu.StoreIn
                    },
                    new MenuSubItem
                    {
                        Id = 2,
                        Title = "offene Eingänge" ,
                        SubText = "Bearbeitung von bestehenden, offenen Eingänge",
                        TargetType= null,
                        ArtMainMenu = Enumerations.enumMainMenu.StoreIn
                    },
                    new MenuSubItem
                    {
                        Id = 3,
                        Title = "Edi - Eingänge",
                        SubText = "Erstellen von Eingängen aus aktuellen EDI-Meldungen",
                        TargetType= null,
                        ArtMainMenu = Enumerations.enumMainMenu.StoreIn
                    },
                });
        }

        public List<MenuSubItem> CallArticlesUnChecked { get; set; }

    }
}
