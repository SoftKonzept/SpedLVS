using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace LVS
{
    public class clsFileIconLoader
    {
        private const uint SHGFI_ICON = 0x100;
        private const uint SHGFI_LARGEICON = 0x0;
        private const uint SHGFI_SMALLICON = 0x1;
        private const uint SHGFI_EXTRALARGE = 0x2;
        private const uint SHGFI_jumbo = 0x4;
        private const uint SHGFI_USEFILEATTRIBUTES = 0x10;

        private const uint FILE_ATTRIBUTE_NORMAL = 0x80;

        [DllImport("shell32.dll")]
        private static extern IntPtr SHGetFileInfo(string pszPath,
        uint dwFileAttributes,
        ref SHFILEINFO psfi,
        uint cbSizeFileInfo,
        uint uFlags);

        ///
        /// Get the icon for a file. File must have a valid extension.
        ///
        ///
        ///
        public static Icon GetFileIcon(string fileName, bool largeIcon)
        {
            string extension = Path.GetExtension(fileName);
            fileName = "*" + extension; // just use wildcard in case file doesn't exist.

            SHFILEINFO shinfo = new SHFILEINFO();
            IntPtr hImg;
            if (largeIcon)
            {
                hImg = SHGetFileInfo(fileName, FILE_ATTRIBUTE_NORMAL, ref shinfo,
                (uint)Marshal.SizeOf(shinfo),
                SHGFI_ICON |
                SHGFI_jumbo |
                SHGFI_USEFILEATTRIBUTES);
            }
            else
            {
                hImg = SHGetFileInfo(fileName, FILE_ATTRIBUTE_NORMAL, ref shinfo,
                (uint)Marshal.SizeOf(shinfo),
                SHGFI_ICON |
                SHGFI_SMALLICON |
                SHGFI_USEFILEATTRIBUTES);
            }
            try
            {
                return Icon.FromHandle(shinfo.hIcon);
            }
            catch
            { // file no longer exists or not available.
                return null;
            }
        } // GetFileIcon

    }
    [StructLayout(LayoutKind.Sequential)]
    public struct SHFILEINFO
    {
        public IntPtr hIcon;
        public IntPtr iIcon;
        public uint dwAttributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szDisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        public string szTypeName;
    };
}
