using Android.App;
using LvsScan.Portable.Enumerations;
using LvsScan.Portable.Interfaces;
using LvsScan.Portable.Services;
using System;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(FileService))]
namespace LvsScan.Portable.Services
{
    public class FileService : IFileService
    {
        /// <summary>
        ///             [https://docs.microsoft.com/de-de/xamarin/android/platform/files/external-storage?tabs=windows]
        ///             
        ///             - Private externe Dateien
        ///               > /storage/emulated/0/Android/data/com.companyname.scanapp/files
        ///               Aufbau Ordnerstruktur für die App
        ///               /storage/emulated/0/Android/data/com.companyname.scanapp/files
        ///                                                                           -> /LvsScan
        /// 
        ///             - Öffentliche externe Dateien
        ///               Abfrage über Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDocuments).AbsolutePath        ///               
        ///               > /storage/emulated/0/
        ///               > /storage/emulated/0/Documents 
        ///               
        ///             Aufbau Ordnerstruktur für die App
        ///             /storage/emulated/0/Documents 
        ///                                 -> /LvsScan
        ///                                          -> /Settings
        ///                                          -> /Lvs
        ///                                          
        /// </summary>
        /// <returns></returns>
        //private string GetRootPath(enumFileStorageArt fileStorageArt)
        //{
        //    string filePath = String.Empty;
        //    switch (fileStorageArt)
        //    {
        //        case enumFileStorageArt.privateExternStorage:
        //            filePath = Application.Context.GetExternalFilesDir(null).ToString();
        //            break;
        //        case enumFileStorageArt.publicExternStorage:
        //            filePath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).AbsolutePath;
        //            break;
        //    }
        //    return filePath;
        //}


        public static string GetFilePath(enumFileStorageArt fileStorageArt, enumMainMenu mainMenu)
        {
            string filePath = String.Empty;
            //filePath = GetRootPath(fileStorageArt);
            switch (fileStorageArt)
            {
                case enumFileStorageArt.privateExternStorage:
                    filePath = Application.Context.GetExternalFilesDir(null).ToString();
                    break;
                case enumFileStorageArt.publicExternStorage:
                    filePath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).AbsolutePath;
                    break;
            }

            filePath += "/LvsScan";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            switch (mainMenu)
            {
                case enumMainMenu.Inventory:
                case enumMainMenu.StoreIn:
                case enumMainMenu.Logout:
                case enumMainMenu.Login:

                    filePath += "/Lvs";
                    break;
                case enumMainMenu.Settings:
                    filePath += "/Settings";
                    break;
            }

            //-- Check Directory exist before return
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            return filePath;
        }

        public static string GetTextFilename(enumMainMenu mainMenu)
        {
            //var filename = GetFilename(mainMenu) + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
            string fileName = String.Empty;
            switch (mainMenu)
            {
                case enumMainMenu.Inventory:
                    fileName = "Inventory";
                    break;
                case enumMainMenu.StoreIn:
                    fileName += "StoreIn";
                    break;
                case enumMainMenu.StoreOut:
                    fileName += "StoreOut";
                    break;
                case enumMainMenu.Logout:
                    fileName += "Logout";
                    break;
                case enumMainMenu.Login:
                    fileName += "Login";
                    break;
                case enumMainMenu.Settings:
                    fileName = "Setting";
                    break;
                default:
                    fileName = "Test";
                    break;
            }
            fileName += "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
            return fileName;
        }
        //private string GetFilename(enumMainMenu mainMenu)
        //{
        //    string fileName= String.Empty;
        //    switch (mainMenu)
        //    {
        //        case enumMainMenu.Inventory:
        //            fileName = "Inventory";
        //            break;
        //        case enumMainMenu.StoreIn:
        //            fileName += "StoreIn";
        //            break;
        //        case enumMainMenu.StoreOut:
        //            fileName += "StoreOut";
        //            break;
        //        case enumMainMenu.Logout:
        //            fileName += "Logout";
        //            break;
        //        case enumMainMenu.Login:
        //            fileName += "Login";
        //            break;
        //        case enumMainMenu.Settings:
        //            fileName = "Setting";
        //            break;
        //            default:
        //            fileName = "Test";
        //            break;
        //    }
        //    return fileName;
        //}

        public void CreateFile(string fileText, string fileName, string filePath)
        {
            //var filename = GetFilename(mainMenu) + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
            //var path = GetFilePath(fileStorageArt, mainMenu);

            var fileDestination = System.IO.Path.Combine(filePath, fileName);
            File.WriteAllText(fileDestination, fileText);
        }



        //public void CreateFile1(string fileText, enumFileStorageArt fileStorageArt)
        //{
        //    enumMainMenu mainMenu = enumMainMenu.Settings;

        //    var filename = GetFilename(mainMenu) + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
        //    var path = GetFilePath(fileStorageArt, mainMenu);
        //    var fileDestination = System.IO.Path.Combine(path, filename);
        //    File.WriteAllText(fileDestination, fileText);
        //}

        public void CreateFile2(string fileText)
        {
            enumFileStorageArt fileStorageArt = enumFileStorageArt.publicExternStorage;
            enumMainMenu mainMenu = enumMainMenu.Settings;

            var filename = FileService.GetTextFilename(enumMainMenu.Inventory);
            var path = GetFilePath(fileStorageArt, mainMenu);
            var fileDestination = System.IO.Path.Combine(path, filename);
            File.WriteAllText(fileDestination, fileText);
        }
    }
}
