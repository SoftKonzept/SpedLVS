using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Telerik.XamarinForms.Common;
using Xamarin.Forms;
using System.Drawing;
using Common.Enumerations;
using Common.Models;
using LvsScan.Portable.Models;
using System.Collections.ObjectModel;

using System.Linq;


namespace LvsScan.Portable.Converter
{
    public class ValueToColorConverter
    {
        /// <summary>
        ///             Color 
        ///              - salmon #FFFA8072
        ///              - greenyellow #FFADFF2F 
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
                        (valOne.Equals(valTwo)) ||
                        ((valOne.Equals("0")) && (valTwo.Equals(string.Empty))) ||
                        ((valTwo.Equals("0")) && (valOne.Equals("0")))
                   )
                {
                    colorcode = "#FFADFF2F";
                    argb = Int32.Parse(colorcode.Replace("#", ""), NumberStyles.HexNumber);
                    colorReturn = System.Drawing.Color.FromArgb(argb);
                }
            }
            return colorReturn;
        }

        /// <summary>
        ///             Color 
        ///              - salmon #FFFA8072
        ///              - greenyellow #FFADFF2F 
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

        public static System.Drawing.Color TranspartenBackgroundColor()
        {
            //string colorcode = "#FFFA8072";
            //int argb = Int32.Parse(colorcode.Replace("#", ""), NumberStyles.HexNumber);
            System.Drawing.Color colorReturn = System.Drawing.Color.FromArgb(255, 255, 255,255);
            return colorReturn;
        }
    }
}
