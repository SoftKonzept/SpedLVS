////using Microsoft.Office.Interop.Excel;
//using Microsoft.Office.Interop.Excel;
//using Excel = Microsoft.Office.Interop.Excel;

namespace LVS.NugetPac
{
    public class OfficeInteropExcel_Export
    {
        public OfficeInteropExcel_Export(System.Data.DataTable myDt, string myExcelExportFilePath)
        {
            // Constructor logic here
            //CreateExcelFile(myDt, myExcelExportFilePath);
            //CreateExcelFileEEPPlus(myDt, myExcelExportFilePath); // Use EPPlus for Excel file creation
        }
        //public void CreateExcelFile(System.Data.DataTable myDt, string myExcelExportFilePath)
        //{
        //    try
        //    {
        //        object missing = System.Reflection.Missing.Value;
        //        //Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();

        //        Excel.Application excelApp = new Excel.Application();
        //        Excel.Workbook wb = excelApp.Workbooks.Add("");
        //        Excel._Worksheet ws = (Excel._Worksheet)wb.ActiveSheet;

        //        excelApp.Visible = true;
        //        excelApp.Workbooks.Add();
        //        //excelApp.Application.Workbooks.Add(Type.Missing);
        //        //Workbook wb = excelApp.Workbooks.Add(true);
        //        //wb.SaveAs(myExcelExportFilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, missing, missing, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, missing, missing, missing, missing, missing);

        //        excelApp.Application.Workbooks.Add(missing);
        //        excelApp.Columns.AutoFit();

        //        //
        //        //Worksheet ws = (Worksheet)wb.Worksheets[1];

        //        //-- create header
        //        foreach (DataColumn column in myDt.Columns)
        //        {
        //            Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)excelApp.Cells[1, column.Ordinal + 1];
        //            range.Font.Bold = true;
        //            range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
        //            excelApp.Cells[1, column.Ordinal + 1] = column.ColumnName;
        //        }

        //        //-- create row and fill
        //        for (int i = 0; i < myDt.Rows.Count; i++)
        //        {
        //            for (int j = 0; j < myDt.Columns.Count; j++)
        //            {
        //                if (myDt.Rows[i][j] is DBNull)
        //                {
        //                    myDt.Rows[i][j] = string.Empty;
        //                }
        //                else
        //                {
        //                    string CellType = myDt.Rows[i][j].GetType().ToString();
        //                    switch (CellType)
        //                    {
        //                        case "System.String":
        //                            excelApp.Cells[i + 2, j + 1] = myDt.Rows[i][j].ToString();
        //                            break;
        //                        case "System.Int32":
        //                            excelApp.Cells[i + 2, j + 1] = Convert.ToInt32(myDt.Rows[i][j]);
        //                            break;
        //                        case "System.Double":
        //                        case "System.Decimal":
        //                            excelApp.Cells[i + 2, j + 1] = Convert.ToDecimal(myDt.Rows[i][j]);
        //                            break;
        //                        case "System.DateTime":
        //                            excelApp.Cells[i + 2, j + 1] = Convert.ToDateTime(myDt.Rows[i][j]);
        //                            break;
        //                        default:
        //                            excelApp.Cells[i + 2, j + 1] = myDt.Rows[i][j].ToString();
        //                            break;
        //                    }
        //                }
        //            }
        //        }
        //        excelApp.Columns.AutoFit();
        //        System.Windows.Forms.Application.DoEvents();

        //        string strPath = Path.GetDirectoryName(myExcelExportFilePath);
        //        helper_IOFile.CheckPath(strPath);
        //        //-- save excel file
        //        excelApp.ActiveWorkbook.SaveCopyAs(myExcelExportFilePath);
        //        excelApp.ActiveWorkbook.Saved = true;
        //        //excelApp.Close();
        //        excelApp.Quit();

        //        //System.Runtime.InteropServices.Marshal.ReleaseComObject(ws);
        //        //System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        //    }
        //    catch(Exception ex)
        //    {
        //        // Handle exception
        //       // Console.WriteLine("Error: " + ex.Message);
        //    }   
        //}


    }
}
