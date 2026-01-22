using System.Data;

namespace Common.Helper
{
    public class xmlFile
    {
        //public string FileName { get; set; }
        //public string ExportPath { get; set; }
        //public string FilePath => Path.Combine(ExportPath, FileName);
        //public string FileContent { get; set; }
        //public DateTime LastModified { get; set; }
        public DataTable XmlDestination { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsImported { get; set; } = false;

        //public List<clsASNArtFieldAssignment> ListImported { get; set; } = new List<clsASNArtFieldAssignment>();

        public xmlFile(string importFilePath)
        {
            ErrorMessage = string.Empty;
            LoadXmlFromSource(importFilePath);
            IsImported = XmlDestination.Rows.Count > 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myFilePath"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        private void LoadXmlFromSource(string myFilePath)
        {
            //if (File.Exists(myFilePath))
            //{
            //    // DataTable aus der XML-Datei laden
            //    XmlDestination = new DataTable();
            //    try
            //    {
            //        XmlDestination.ReadXml(myFilePath);
            //    }
            //    catch (Exception ex)
            //    {
            //        //throw new InvalidOperationException("Fehler beim Einlesen der XML-Datei.", ex);
            //        ErrorMessage+= $"Fehler beim Einlesen der XML-Datei: {ex.Message}";
            //    }

            //    // Liste für die importierten Objekte erstellen
            //    List<clsASNArtFieldAssignment> assignments = new List<clsASNArtFieldAssignment>();

            //    // Jede Zeile der DataTable durchlaufen und Objekte erstellen
            //    foreach (DataRow row in XmlDestination.Rows)
            //    {
            //        clsASNArtFieldAssignment assignment = new clsASNArtFieldAssignment
            //        {
            //            ID = row.Field<decimal>("ID"),
            //            Sender = row.Field<decimal>("Sender"),
            //            Receiver = row.Field<decimal>("Receiver"),
            //            AbBereichID = row.Field<int>("AbBereichID"),
            //            ASNField = row.Field<string>("ASNField"),
            //            ArtField = row.Field<string>("ArtField"),
            //            IsDefValue = row.Field<bool>("IsDefValue"),
            //            DefValue = row.Field<string>("DefValue"),
            //            CopyToField = row.Field<string>("CopyToField"),
            //            FormatFunction = row.Field<string>("FormatFunction"),
            //            IsGlobalFieldVar = row.Field<bool>("IsGlobalFieldVar"),
            //            GlobalFieldVar = row.Field<string>("GlobalFieldVar"),
            //            SubASNField = row.Field<string>("SubASNField")
            //        };

            //        // Objekt zur Liste hinzufügen
            //        assignments.Add(assignment);
            //    }
            //}
            //else
            //{
            //    throw new FileNotFoundException($"Die Datei '{importFilePath}' wurde nicht gefunden.");
            //}
        }

    }
}
