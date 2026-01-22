using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Sped4;
using System.Data;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using System.Xml;
using System.Windows.Forms;



namespace Sped4
{
  class clsExcel
  {

 
    public void ExportDataTableToExcel(DataTable dt)
    {
      string filename = string.Empty;
      SaveFileDialog sfd = new SaveFileDialog();
      sfd.Filter = "(*.xls)|*.xls|All files(*.*)|*.*";
      sfd.Title = "ExelExport";
      sfd.FileName = DateTime.Now.ToString("yyyyMMdd") + "_ExcelExport.xls";
      sfd.OverwritePrompt = true;

      if (sfd.ShowDialog() == DialogResult.OK)
      {
        filename = sfd.FileName;

        Excel.Application xlApp;
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;

        xlApp = new Excel.ApplicationClass();
        xlWorkBook = xlApp.Workbooks.Add(misValue);

        xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

        Excel.Range workSheetRange = xlWorkSheet.get_Range(xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[1, dt.Columns.Count]);
        workSheetRange.Font.Bold = true;
        //workSheetRange.Font.Italic = italic;
        //workSheetRange.Font.Color = color.ToArgb();
        //workSheetRange.Font.Underline = underline;


        //Daten aus DataTable in Worksheet
        for (int j = 1; j < dt.Columns.Count; j++)
        {
          //string test = dt.Columns[j].ColumnName.ToString();
          xlWorkSheet.Cells[1, j] = dt.Columns[j].ColumnName.ToString();
        }

        for (int i = 2; i < dt.Rows.Count + 2; i++)
        {
          for (int j = 1; j < dt.Columns.Count; j++)
          {
            //string test = dt.Rows[i - 2][j].ToString();
            xlWorkSheet.Cells[i, j] = dt.Rows[i - 2][j];
          }
        }

        xlWorkBook.SaveAs(filename, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
        xlWorkBook.Close(true, misValue, misValue);
        xlApp.Quit();

        releaseObject(xlWorkSheet);
        releaseObject(xlWorkBook);
        releaseObject(xlApp);
      }
      //MessageBox.Show("Excel file created , you can find the file c:\\csharp-Excel.xls");
    }
    //
    //
    //
    private void releaseObject(object obj)
    {
      try
      {
        System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
        obj = null;
      }
      catch (Exception ex)
      {
        obj = null;
        MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
      }
      finally
      {
        GC.Collect();
      }
    }


  }
}
/*****************+

public string ColumnIndexToName(int index)
{
    string columnName = "";
    int rest = 0;
    if (index > 26)
    {
        do
        {
            index = Math.DivRem(index, 26, out rest);
            if (rest == 0)
            {
                index -= 1;
                rest = 26;
            }
            columnName = (char)(rest + 64) + columnName;
        } while (index > 26);
    }
    columnName = (char)(index + 64) + columnName;
    return columnName;
}

public void SetData(int row, int column, string text)
{
    _worksheet.Cells[row, column] = text;
}

public string GetData(int row, int column)
{
    return _worksheet.Cells[row, column] as string;
}

public void SetCellBorder(string cell1, string cell2,
                          Excel.XlBordersIndex position,
                          Excel.XlLineStyle style,
                          Excel.XlBorderWeight weight,
                          System.Drawing.Color color)
{
    Excel.Range workSheetRange = _worksheet.get_Range(cell1, cell2);
    workSheetRange.Borders[position].Weight = weight;
    workSheetRange.Borders[position].LineStyle = style;
    workSheetRange.Borders[position].Color = color.ToArgb();
}

public void SetCellColor(string cell1, string cell2, System.Drawing.Color color)
{
    Excel.Range workSheetRange = _worksheet.get_Range(cell1, cell2);
    workSheetRange.Interior.Color = color.ToArgb();
}

public void SetCellFont(string cell1, string cell2,
                        bool bold, bool italic, bool underline,
                        System.Drawing.Color color)
{
    Excel.Range workSheetRange = _worksheet.get_Range(cell1, cell2);
    workSheetRange.Font.Bold = bold;
    workSheetRange.Font.Italic = italic;
    workSheetRange.Font.Color = color.ToArgb();
    workSheetRange.Font.Underline = underline;
}

public void SetColumnWidth(int column, double width)
{
    ((Excel.Range)_worksheet.Columns[column, Type.Missing]).ColumnWidth = width;
}

public void SetRowHeight(int row, double height)
{
    ((Excel.Range)_worksheet.Rows[row, Type.Missing]).RowHeight = height;
}

public void Save(string fileName)
{
    _workbook.SaveAs(fileName, Excel.XlFileFormat.xlExcel8,
    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
    Excel.XlSaveAsAccessMode.xlNoChange,
    Excel.XlSaveConflictResolution.xlUserResolution,
    Type.Missing, Type.Missing, Type.Missing, Type.Missing);
} 
***************/