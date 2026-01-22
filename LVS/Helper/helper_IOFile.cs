using System;
using System.Collections.Generic;
using System.IO;

namespace LVS
{
    public class helper_IOFile
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myPath"></param>
        public static void CheckPath(string myPath)
        {
            string currentDirectory = System.IO.Path.GetDirectoryName(myPath);
            if (!Directory.Exists(currentDirectory))
            {
                Directory.CreateDirectory(currentDirectory);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myFilePath"></param>
        /// <returns></returns>
        public static bool CheckFile(string myFilePath)
        {
            bool bReturn = File.Exists(myFilePath);
            return bReturn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myFilePath"></param>
        /// <returns></returns>
        public static string ReadFileInLine(string myFilePath)
        {
            string strLine = string.Empty;
            if (File.Exists(myFilePath))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(myFilePath))
                    {
                        strLine = sr.ReadToEnd().Replace(Environment.NewLine, "");
                    }
                }
                catch (Exception ex)
                {
                    //Fehlermeldung
                    string strEx = ex.ToString();
                }
            }
            return strLine;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myFilePath"></param>
        /// <param name="myListToWrite"></param>
        /// <returns></returns>
        public static bool WriteFileInLine(string myFilePath, List<string> myListToWrite)
        {
            bool bReturn = true;
            try
            {
                helper_IOFile.CheckPath(System.IO.Path.GetDirectoryName(myFilePath));
                File.WriteAllLines(myFilePath, myListToWrite.ToArray());
            }
            catch (Exception ex)
            {
                bReturn = false;
            }
            return bReturn;
        }

        public static bool CanOpenFile(string myFileNamePath)
        {
            try
            {
                using (File.Open(myFileNamePath, FileMode.Open))
                {
                    return true;
                }
            }
            catch
            { return false; }
        }

        public static bool IsFileAvailable(string filePath)
        {
            string s = string.Empty;
            try
            {
                // Versuche, die Datei im exklusiven Modus zu öffnen
                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    // Wenn die Datei erfolgreich geöffnet wurde, ist sie verfügbar
                    return true;
                }
            }
            catch (IOException)
            {
                // Wenn eine IOException auftritt, ist die Datei nicht verfügbar
                return false;
            }
        }


        public static byte[] FileToByteArray(string fileName)
        {
            byte[] fileData = null;

            using (FileStream fs = File.OpenRead(fileName))
            {
                using (BinaryReader binaryReader = new BinaryReader(fs))
                {
                    fileData = binaryReader.ReadBytes((int)fs.Length);
                }
            }
            return fileData;
        }

        /// <summary>
        ///             aus einem gegebenen Pfad einen bestimmten Ordnernamen entfernt
        /// </summary>
        /// <param name="pfad"></param>
        /// <param name="ordnerName"></param>
        /// <returns></returns>
        public static string EntferneOrdnerAusPfad(string pfad, string ordnerName)
        {
            // Zerlege den Pfad in einzelne Teile
            var teile = pfad.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            // Filtere den gewünschten Ordnernamen heraus
            var gefilterteTeile = Array.FindAll(teile, teil => !teil.Equals(ordnerName, StringComparison.OrdinalIgnoreCase));

            // Setze den Pfad wieder zusammen
            var neuerPfad = string.Join(Path.DirectorySeparatorChar.ToString(), gefilterteTeile);

            return neuerPfad;
        }


    }
}
