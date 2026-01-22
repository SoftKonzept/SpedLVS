using System;

namespace LvsScan.Portable.Views.Menu
{
    public class FlyoutMenuPageFlyoutMenuItem
    {
        public FlyoutMenuPageFlyoutMenuItem()
        {
            TargetType = typeof(FlyoutMenuPageFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }

        public string IconSource { get; set; }
    }
}