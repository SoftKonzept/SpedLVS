using Common.Enumerations;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.IO;

namespace Common.Helper
{
    public class FileImportToJson<T>
    {
        public string FileName { get; set; }
        public string ImportPath { get; set; }
        public string FilePath { get; set; }
        public string FileContent { get; set; }
        public DateTime LastModified { get; set; }
        public List<T> ListImported { get; set; } = new List<T>();
        public string ErrorMessage { get; set; }
        public bool IsImported { get; set; } = false;

        public FileImportToJson(string myImportFilePath)
        {
            ErrorMessage = string.Empty;
            if (File.Exists(myImportFilePath))
            {
                FilePath = myImportFilePath;
                FileName = Path.GetFileName(myImportFilePath);
                ImportPath = Path.GetDirectoryName(myImportFilePath);
                ImportFile();
                IsImported = File.Exists(FilePath);
            }
            else
            {
                ErrorMessage += "Die angegebene Datei wurde nicht gefunden.";
                ErrorMessage += "Datei: " + myImportFilePath;
            }
        }
        /// <summary>
        ///           Logic to create an XML file from XmlSource
        ///           This method should convert XmlSource DataTable to XML format and save it to FilePath
        /// </summary>
        private void ImportFile()
        {
            try
            {
                // Liest den Inhalt der ausgewählten Datei
                string jsonContent = File.ReadAllText(FilePath);

                // Deserialisiert die JSON-Daten in eine Liste von Objekten des Typs T
                ListImported = JsonConvert.DeserializeObject<List<T>>(jsonContent);

                if (ListImported != null && ListImported.Count > 0)
                {
                    // Markiere den Import als erfolgreich
                    IsImported = true;
                }
                else
                {
                    ErrorMessage += "Die Datei enthält keine gültigen Daten.";
                }
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung bei Problemen mit dem Import
                //MessageBox.Show($"Fehler beim Importieren der Einstellungen: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorMessage += "Fehler beim Importieren der Einstellungen: {ex.Message}";
            }
        }

    }
}
