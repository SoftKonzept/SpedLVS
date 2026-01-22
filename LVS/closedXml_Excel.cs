//using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVS
{
    public class closedXml_Excel
    {

        public closedXml_Excel(DataTable myDt, string myExportExcelFilePath)
        {
            try
            {
                //// Create a new workbook
                //using (var workbook = new XLWorkbook())
                //{
                //    // Add a worksheet to the workbook
                //    if (myDt.TableName.Equals(string.Empty))
                //    {
                //        myDt.TableName = "Bestand";
                //    }

                //    var worksheet = workbook.Worksheets.Add(myDt.TableName);

                //    // Insert the DataTable into the worksheet, starting from cell A1
                //    worksheet.Cell(1, 1).InsertTable(myDt);

                //    // Save the workbook to the specified file path
                //    workbook.SaveAs(myExportExcelFilePath);
                //}
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                Console.WriteLine("Error: " + ex.Message);
            }

        }

    }
}
