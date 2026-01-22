using System;
using System.Drawing;
using System.Globalization;

namespace Common.Helper
{
    public class ValueToColorConverter
    {
        public static readonly Color Default_BackgroundColor_Badge = System.Drawing.Color.Gray;

        /// <summary>
        ///             Do string compare and return color 
        ///             Color 
        ///              - strings are not equal -> salmon #FFFA8072
        ///              - strings are eqaul -> greenyellow #FFADFF2F 
        /// </summary>
        /// <param name="valOne"></param>
        /// <param name="valTwo"></param>
        /// <returns></returns>
        public static System.Drawing.Color StringCompareConvert(string valOne, string valTwo)
        {
            string colorcode = "#FFFA8072";
            int argb = Int32.Parse(colorcode.Replace("#", ""), NumberStyles.HexNumber);
            System.Drawing.Color colorReturn = System.Drawing.Color.FromArgb(argb);

            if ((valOne != null) && (valTwo != null))
            {
                if (
                        (!valOne.Equals(StoredLocationStringToStoreLocationItem.const_NullStoredLocationString)) &&
                        (!valTwo.Equals(StoredLocationStringToStoreLocationItem.const_NullStoredLocationString))

                  )
                {
                    if (valOne.Equals(valTwo))
                    {
                        colorcode = "#FFADFF2F";
                        argb = Int32.Parse(colorcode.Replace("#", ""), NumberStyles.HexNumber);
                        colorReturn = System.Drawing.Color.FromArgb(argb);
                    }
                }
            }
            return colorReturn;
        }

        /// <summary>
        ///             Return Color 
        ///              - false -> salmon #FFFA8072
        ///              - true -> greenyellow #FFADFF2F 
        /// </summary>
        /// <param name="valOne"></param>
        /// <param name="valTwo"></param>
        /// <returns></returns>
        public static System.Drawing.Color BooleanConvert(bool bVal)
        {
            string colorcode = "#FFFA8072";
            int argb = Int32.Parse(colorcode.Replace("#", ""), NumberStyles.HexNumber);
            System.Drawing.Color colorReturn = System.Drawing.Color.FromArgb(argb);

            if (bVal)
            {
                colorcode = "#FFADFF2F";
                argb = Int32.Parse(colorcode.Replace("#", ""), NumberStyles.HexNumber);
                colorReturn = System.Drawing.Color.FromArgb(argb);
            }
            return colorReturn;
        }
        /// <summary>
        ///             Return Color transparent
        /// <returns></returns>
        public static System.Drawing.Color TranspartenBackgroundColor()
        {
            System.Drawing.Color colorReturn = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            return colorReturn;
        }

        /// <summary>
        ///             set the background color for the badge
        ///             true -> limegreen
        ///             false -> gray
        /// </summary>
        /// <param name="bVal"></param>
        /// <returns></returns>
        public static System.Drawing.Color BadgeBackgroundColor_BooleanConvert(bool bVal)
        {
            System.Drawing.Color colorReturn = ValueToColorConverter.Default_BackgroundColor_Badge;
            if (bVal)
            {
                colorReturn = Color.LimeGreen;
            }
            return colorReturn;
        }
        public static System.Drawing.Color BadgeBackgroundColor_Default()
        {
            return ValueToColorConverter.Default_BackgroundColor_Badge;
        }

        public static System.Drawing.Color FrameBorderColor_Default()
        {
            string colorcode = "#4488F6";
            int argb = Int32.Parse(colorcode.Replace("#", ""), NumberStyles.HexNumber);
            System.Drawing.Color colorReturn = System.Drawing.Color.FromArgb(argb);
            return colorReturn;
        }

        public static System.Drawing.Color ButtonBackgroundColor(bool myBool)
        {
            System.Drawing.Color colorReturn = Color.Snow;
            if (myBool)
            {
                colorReturn = Color.Tomato;
            }
            return colorReturn;
        }

        /// <summary>
        ///             set the background color for each different Workspace
        /// </summary>
        /// <param name="myWorkspaceId"></param>
        /// <returns></returns>
        public static System.Drawing.Color ViewBackgroundColorWorkspace_IdConvert(int myWorkspaceId)
        {
            System.Drawing.Color colorReturn = Color.White;
            switch (myWorkspaceId)
            {
                case 1:
                    colorReturn = Color.AntiqueWhite;
                    break;
                case 2:
                    colorReturn = Color.Aquamarine;
                    break;
                case 3:
                    colorReturn = Color.Beige;
                    break;
                case 4:
                    colorReturn = Color.LemonChiffon;
                    break;
                case 5:
                    colorReturn = Color.LightCyan;
                    break;
                case 6:
                    colorReturn = Color.LightYellow;
                    break;
                case 7:
                    colorReturn = Color.White;
                    break;
                case 8:
                    colorReturn = Color.White;
                    break;
                default:
                    colorReturn = Color.White;
                    break;
            }
            return colorReturn;
        }
        /// <summary>
        ///             set the background color for each different Workspace
        /// </summary>
        /// <param name="myWorkspaceId"></param>
        /// <returns></returns>
        public static System.Drawing.Color ViewBackgroundColorSubmenu(TimeSpan mySpan)
        {
            System.Drawing.Color colorReturn = Color.White;
            if (mySpan.Days > 0)
            {
                colorReturn = Color.Tomato;
            }
            else
            {
                if (mySpan.Hours < 2)
                {
                    colorReturn = Color.White;
                }
                else if ((mySpan.Hours > 2) && (mySpan.Hours < 6))
                {
                    colorReturn = Color.Orange;
                }
                else
                {
                    colorReturn = Color.Tomato;
                }
            }

            return colorReturn;
        }
        public static System.Drawing.Color ViewBackgroundColorSubmenu()
        {
            TimeSpan tmpSpan = DateTime.Now - DateTime.Now.AddHours(-1);
            System.Drawing.Color colorReturn = ValueToColorConverter.ViewBackgroundColorSubmenu(tmpSpan);
            return colorReturn;
        }
    }
}
