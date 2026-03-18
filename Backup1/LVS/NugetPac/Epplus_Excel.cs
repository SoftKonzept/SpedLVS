using OfficeOpenXml;
using System;
using System.IO;


namespace LVS.NugetPac
{
    /// <summary>
    ///             This class is used to export a DataTable to an Excel file using the EPPlusFree library. 
    ///             EPPlusFree is an unofficial EPPlus library, it is a continuous version of EPPlus Free Edition 4.5.3.3.
    /// </summary>
    public class Epplus_Excel
    {
        public Epplus_Excel(System.Data.DataTable myDt, string myExcelExportFilePath)
        {
            string str = string.Empty;
            try
            {
                ExcelPackage ep = new ExcelPackage();
                ExcelWorksheet ws = ep.Workbook.Worksheets.Add("Bestand");

                ws.Cells["A1"].LoadFromDataTable(myDt, true);
                //-- Überschrift
                for (int i = 1; i <= myDt.Columns.Count; i++)
                {
                    ws.Cells[1, i].Style.Font.Bold = true;
                    ws.Cells[1, i].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Cells[1, i].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                }

                // Beispielhafte Formatierungen:
                for (int col = 1; col <= myDt.Columns.Count; col++)
                {
                    string columnName = myDt.Columns[col - 1].ColumnName;

                    switch (columnName)
                    {
                        case "ArtikelID":
                        case "LVSNr":
                        case "Anzahl":
                        case "Eingang":
                            ws.Column(col).Style.Numberformat.Format = "0"; // z. B. 1234
                            break;

                        case "Dicke":
                        case "Breite":
                        case "Laenge":
                        case "Hoehe":
                        case "Netto":
                        case "Brutto":
                            ws.Column(col).Style.Numberformat.Format = "#,##0.00"; // z. B. 1.234,56
                            break;

                        case "Eingangsdatum":
                            ws.Column(col).Style.Numberformat.Format = "dd.MM.yyyy"; // z. B. 1.234,56
                            break;
                    }
                }



                //int iColCount = myDt.Columns.Count;
                //using (ExcelRange Rng = ws.Cells[1,1,1,iColCount])
                //{
                //    //Rng.Value = "Everyday Be Coding - Format Table using EPPlus .Net Library - Part 15(B)";
                //    Rng.Merge = false;
                //    Rng.Style.Font.Size = 12;
                //    Rng.Style.Font.Bold = true;
                //    Rng.Style.Font.Italic = false;
                //    //Rng.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(255, 255, 255));
                //    Rng.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                //}

                ws.Cells.AutoFitColumns();
                FileInfo excelFile = new FileInfo(myExcelExportFilePath);
                ep.SaveAs(excelFile);
            }
            catch (Exception ex)
            {
                // Handle exception
                // Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
