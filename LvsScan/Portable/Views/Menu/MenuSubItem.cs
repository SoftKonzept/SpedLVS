using LvsScan.Portable.Enumerations;
using System;
using System.Drawing;

namespace LvsScan.Portable.Views.Menu
{
    public class MenuSubItem
    {
        public MenuSubItem()
        {
            TargetType = typeof(MenuSubItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubText { get; set; }

        public enumMainMenu ArtMainMenu { get; set; }
        public Type TargetType { get; set; }

        public bool ShowButton { get; set; }
        public Color BackgroundColor { get; set; }

    }
}
