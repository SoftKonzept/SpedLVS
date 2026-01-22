using System;
using System.Data;
using System.IO;
using System.Xml;

namespace Common.Helper
{
    public class xmlFileExport
    {
        public string FileName { get; set; }
        public string ExportPath { get; set; }
        public string FilePath => Path.Combine(ExportPath, FileName);
        public string FileContent { get; set; }
        public DateTime LastModified { get; set; }
        public DataTable XmlSource { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsExported { get; set; } = false;

        public xmlFileExport(string fileName, string exportPath, DataTable xmlSource)
        {
            FileName = fileName;
            ExportPath = exportPath;
            XmlSource = xmlSource;

            CreateXmlFile();
            IsExported = File.Exists(FilePath);
        }
        /// <summary>
        ///           Logic to create an XML file from XmlSource
        ///           This method should convert XmlSource DataTable to XML format and save it to FilePath
        /// </summary>
        private void CreateXmlFile()
        {
            // Daten in eine XML-Datei schreiben
            try
            {
                using (FileStream fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write))
                {
                    using (XmlWriter writer = XmlWriter.Create(fs, new XmlWriterSettings { Indent = true }))
                    {
                        XmlSource.WriteXml(writer, XmlWriteMode.WriteSchema);
                    }
                }
                //MessageBox.Show("Daten erfolgreich exportiert.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Fehler beim Exportieren der Daten: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorMessage = ex.Message;
            }
        }

    }
}
