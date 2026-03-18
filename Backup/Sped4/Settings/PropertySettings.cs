using System.Drawing;

namespace Sped4.Settings
{
    public class PropertySettings
    {
        public static Color DefaultBackColor = Color.FromArgb(255, 255, 255);
        public static Color DefaultBaseColor = Color.FromArgb(4, 72, 117);
        public static Color DefaultBaseColor2 = Color.FromArgb(255, 128, 0);
        public static Color DefaultEffectColor = Color.FromArgb(191, 219, 255);
        public static Color DefaultEffectColor2 = Color.FromArgb(255, 177, 99);

        public static void SetBackColor_Default(frmMAIN myMain)
        {
            //Sped4.Properties.Settings.Default.BackColor = Globals.DefaultBackColor;
            //Sped4.Properties.Settings.Default.BaseColor = Globals.DefaultBaseColor;
            //Sped4.Properties.Settings.Default.BaseColor2 = Globals.DefaultBaseColor2;
            //Sped4.Properties.Settings.Default.EffectColor = Globals.DefaultEffectColor;
            //Sped4.Properties.Settings.Default.EffectColor2 = Globals.DefaultEffectColor2;

            Sped4.Properties.Settings.Default.BackColor = PropertySettings.DefaultBackColor;
            Sped4.Properties.Settings.Default.BaseColor = PropertySettings.DefaultBaseColor;
            Sped4.Properties.Settings.Default.BaseColor2 = PropertySettings.DefaultBaseColor2;
            Sped4.Properties.Settings.Default.EffectColor = PropertySettings.DefaultEffectColor;
            Sped4.Properties.Settings.Default.EffectColor2 = PropertySettings.DefaultEffectColor2;
            myMain.RefreshColor();
        }

        public static void SetBackColor_TestSystem(frmMAIN myMain)
        {
            Sped4.Properties.Settings.Default.BaseColor = System.Drawing.Color.Red;
            myMain.RefreshColor();
        }
    }
}
