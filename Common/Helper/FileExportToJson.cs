using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common.Helper
{
    public class FileExportToJson<T>
    {
        public string FileName { get; set; }
        public string ExportPath { get; set; }
        public string FilePath => Path.Combine(ExportPath, FileName);
        public string FileContent { get; set; }
        public DateTime LastModified { get; set; }
        public List<T> ListToExportSource { get; set; } = new List<T>();
        public string ErrorMessage { get; set; }
        public bool IsExported { get; set; } = false;

        public FileExportToJson(string fileName, string exportPath, List<T> myListToExport)
        {
            FileName = fileName;
            ExportPath = exportPath;
            ListToExportSource = myListToExport;
            // Sicherstellen, dass der Exportpfad existiert
            if (!Directory.Exists(ExportPath))
            {
                Directory.CreateDirectory(ExportPath);
            }
            CreateFile();
            IsExported = File.Exists(FilePath);
        }
        /// <summary>
        ///           Logic to create an XML file from XmlSource
        ///           This method should convert XmlSource DataTable to XML format and save it to FilePath
        /// </summary>
        private void CreateFile()
        {
            // Daten in eine XML-Datei schreiben
            try
            {
                // JSON-Serialisierung der Liste
                string jsonContent = JsonConvert.SerializeObject(ListToExportSource, Newtonsoft.Json.Formatting.Indented);

                // Schreiben in die Datei
                File.WriteAllText(FilePath, jsonContent, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Fehler beim Exportieren der Daten: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorMessage = ex.Message;
            }
        }

    }
}
