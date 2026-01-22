using System.Collections.Generic;
using Xamarin.Forms;

namespace LvsScan.Portable.Controls
{
    public class ctr_MenuToolBarItem
    {
        public const int const_AutomationId_Home = 1;
        public const int const_AutomationId_Submenue = 2;
        public const int const_AutomationId_Refresh = 3;
        public const int const_AutomationId_Edit = 4;
        public const int const_AutomationId_ArticleScanStart = 5;
        public const int const_AutomationId_CreateAusgang = 6;
        public const int const_AutomationId_CreateEingang = 7;
        public const int const_AutomationId_Print = 8;

        public static List<ToolbarItem> CreateMenuStoreIn(bool myEingangEdit, bool myScanStart, bool myCreateEingang)
        {
            List<ToolbarItem> retList = new List<ToolbarItem>();

            // ID = 7
            if (myCreateEingang)
            {
                retList.Add(ctr_MenuToolBarItem.ToolBarItem_CreateEingang());
            }
            // ID=5
            if (myScanStart)
            {
                retList.Add(ctr_MenuToolBarItem.ToolBarItem_ArticleScanStart());
            }
            // ID=4
            if (myEingangEdit)
            {
                retList.Add(ctr_MenuToolBarItem.ToolBarItem_Edit());
            }
            //ID=3
            retList.Add(ctr_MenuToolBarItem.ToolBarItem_Refresh());
            //ID=2
            retList.Add(ctr_MenuToolBarItem.ToolBarItem_Submenu());
            //ID=1
            retList.Add(ctr_MenuToolBarItem.ToolBarItem_Home());
            return retList;
        }

        public static List<ToolbarItem> CreateMenuStoreOut(bool myAusgangEdit, bool myScanStart, bool myCreateAusgang, bool myShowPrinter)
        {
            List<ToolbarItem> retList = new List<ToolbarItem>();
            //ID=8
            if (myShowPrinter)
            {
                retList.Add(ctr_MenuToolBarItem.ToolBarItem_Print());
            }
            if (myCreateAusgang)
            {
                retList.Add(ctr_MenuToolBarItem.ToolBarItem_CreateAusgang());
            }
            if (myScanStart)
            {
                retList.Add(ctr_MenuToolBarItem.ToolBarItem_ArticleScanStart());
            }
            if (myAusgangEdit)
            {
                retList.Add(ctr_MenuToolBarItem.ToolBarItem_Edit());
            }
            retList.Add(ctr_MenuToolBarItem.ToolBarItem_Refresh());
            retList.Add(ctr_MenuToolBarItem.ToolBarItem_Submenu());
            retList.Add(ctr_MenuToolBarItem.ToolBarItem_Home());
            return retList;
        }

        public static ToolbarItem ToolBarItem_Home()
        {
            ImageSource img = "mi_home_32x32.png";
            ToolbarItem item = new ToolbarItem
            {
                Text = "Home",
                IconImageSource = img,
                Order = ToolbarItemOrder.Primary,
                Priority = 0,
                AutomationId = ctr_MenuToolBarItem.const_AutomationId_Home.ToString()
                //AutomationId = "1"
            };
            //item += "toolBarItem_Clicked";
            return item;
        }
        public static ToolbarItem ToolBarItem_Submenu()
        {
            ImageSource img = "mi_elements2_32x32.png";
            ToolbarItem item = new ToolbarItem
            {
                Text = "Auslagerung Submenu",
                IconImageSource = img,
                Order = ToolbarItemOrder.Primary,
                Priority = 0,
                //AutomationId = "2"
                AutomationId = ctr_MenuToolBarItem.const_AutomationId_Submenue.ToString()
            };
            //item += ScanArticleStart_Clicked;
            return item;
        }
        public static ToolbarItem ToolBarItem_Refresh()
        {
            ImageSource img = "mi_arrow_circle2_32x32.png";
            ToolbarItem item = new ToolbarItem
            {
                Text = "Refresh",
                IconImageSource = img,
                Order = ToolbarItemOrder.Primary,
                Priority = 0,
                //AutomationId = "3"
                AutomationId = ctr_MenuToolBarItem.const_AutomationId_Refresh.ToString()
            };
            //item += ScanArticleStart_Clicked;
            return item;
        }

        /// <summary>
        ///             offene Ausgänge
        /// </summary>
        /// <returns></returns>
        public static ToolbarItem ToolBarItem_Edit()
        {
            ImageSource img = "mi_edit_32x32.png";
            ToolbarItem item = new ToolbarItem
            {
                Text = "Scan Artikel",
                IconImageSource = img,
                Order = ToolbarItemOrder.Primary,
                Priority = 0,
                //AutomationId = "4"
                AutomationId = ctr_MenuToolBarItem.const_AutomationId_Edit.ToString()
            };
            //item += StoreOutStart_Clicked;
            return item;
        }
        /// <summary>
        ///             Liste unchecked Abrufe ausgewählt
        ///             Liste unchecked Abrufe > 0
        /// </summary>
        /// <returns></returns>
        public static ToolbarItem ToolBarItem_ArticleScanStart()
        {
            ImageSource img = "mi_arrow_from_32x32.png";
            ToolbarItem item = new ToolbarItem
            {
                Text = "Prozess starten",
                IconImageSource = img,
                Order = ToolbarItemOrder.Primary,
                Priority = 0,
                //AutomationId = "5"
                AutomationId = ctr_MenuToolBarItem.const_AutomationId_ArticleScanStart.ToString()
            };
            //item += ScanArticleStart_Clicked;
            return item;
        }
        /// <summary>
        ///             Liste checked Abrufe >0
        ///             Liste checked Abrufe ist ausgewählt 
        ///             
        ///             Erstellt die Ausgänge anhander der gecheckten Abrufe
        /// </summary>
        /// <returns></returns>
        public static ToolbarItem ToolBarItem_CreateAusgang()
        {
            ImageSource img = "mi_box_out_32x32.png";
            ToolbarItem item = new ToolbarItem
            {
                Text = "Ausgänge aus den Abrufen erstellen",
                IconImageSource = img,
                Order = ToolbarItemOrder.Primary,
                Priority = 0,
                //AutomationId = "6"
                AutomationId = ctr_MenuToolBarItem.const_AutomationId_CreateAusgang.ToString()
            };
            //item += ScanArticleStart_Clicked;
            return item;
        }

        public static ToolbarItem ToolBarItem_CreateEingang()
        {
            ImageSource img = "mi_box_into_32x32.png";
            ToolbarItem item = new ToolbarItem
            {
                Text = "Eingänge erstellen",
                IconImageSource = img,
                Order = ToolbarItemOrder.Primary,
                Priority = 0,
                //AutomationId = "7"
                AutomationId = ctr_MenuToolBarItem.const_AutomationId_CreateAusgang.ToString()
            };
            //item += ScanArticleStart_Clicked;
            return item;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public static ToolbarItem ToolBarItem_Print()
        {
            ImageSource img = "mi_printer_32x32.png";
            ToolbarItem item = new ToolbarItem
            {
                Text = "Ausgangsdokumente Drucken",
                IconImageSource = img,
                Order = ToolbarItemOrder.Primary,
                Priority = 0,
                //AutomationId = "7"
                AutomationId = ctr_MenuToolBarItem.const_AutomationId_Print.ToString()
            };
            //item += ScanArticleStart_Clicked;
            return item;
        }

    }
}
