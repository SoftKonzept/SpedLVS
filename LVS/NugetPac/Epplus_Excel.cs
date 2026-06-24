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

                //--- PRÜFUNG: Sind Daten vorhanden?
                if (myDt == null || myDt.Rows.Count == 0)
                {
                    // KEINE DATEN: "NOstock" einfügen
                    ws.Cells["A2"].Value = "NOstock";
                    ws.Cells["A2"].Style.Font.Bold = true;
                    ws.Cells["A2"].Style.Font.Size = 12;
                    ws.Cells["A2"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Cells["A2"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightCoral);
                }
                else
                {
                    
                    ////-- Überschrift
                    //for (int i = 1; i <= myDt.Columns.Count; i++)
                    //{
                    //    ws.Cells[1, i].Style.Font.Bold = true;
                    //    ws.Cells[1, i].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    //    ws.Cells[1, i].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                    //}

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
                    ws.Cells.AutoFitColumns();
                }
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
