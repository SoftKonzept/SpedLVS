using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Excel = Microsoft.Office.Interop.Excel;
//using Excel = Microsoft.Office.Interop.Excel.Application();



namespace LVS
{
    public class clsExcel
    {
        public string ExportDataTableToWorksheet(DataTable dataSource, string fileName)
        {
            // XML-Schreiber erzeugen
            fileName += ".xml";
            XmlTextWriter writer = new XmlTextWriter(fileName, Encoding.UTF8);

            // Ausgabedatei für bessere Lesbarkeit formatieren (einrücken etc.)
            writer.Formatting = Formatting.Indented;

            // <?xml version="1.0"?>
            writer.WriteStartDocument();

            // <?mso-application progid="Excel.Sheet"?>
            writer.WriteProcessingInstruction("mso-application", "progid=\"Excel.Sheet\"");

            // <Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet >"
            writer.WriteStartElement("Workbook", "urn:schemas-microsoft-com:office:spreadsheet");

            // Definition der Namensräume schreiben 
            writer.WriteAttributeString("xmlns", "o", null, "urn:schemas-microsoft-com:office:office");
            writer.WriteAttributeString("xmlns", "x", null, "urn:schemas-microsoft-com:office:excel");
            writer.WriteAttributeString("xmlns", "ss", null, "urn:schemas-microsoft-com:office:spreadsheet");
            writer.WriteAttributeString("xmlns", "html", null, "http://www.w3.org/TR/REC-html40");

            // <DocumentProperties xmlns="urn:schemas-microsoft-com:office:office">
            writer.WriteStartElement("DocumentProperties", "urn:schemas-microsoft-com:office:office");

            // Dokumenteingeschaften schreiben
            writer.WriteElementString("Author", Environment.UserName);
            writer.WriteElementString("LastAuthor", Environment.UserName);
            writer.WriteElementString("Created", DateTime.Now.ToString("u") + "Z");
            writer.WriteElementString("Company", "Unknown");
            writer.WriteElementString("Version", "11.8122");

            // </DocumentProperties>
            writer.WriteEndElement();

            // <ExcelWorkbook xmlns="urn:schemas-microsoft-com:office:excel">
            writer.WriteStartElement("ExcelWorkbook", "urn:schemas-microsoft-com:office:excel");

            // Arbeitsmappen-Einstellungen schreiben
            writer.WriteElementString("WindowHeight", "13170");
            writer.WriteElementString("WindowWidth", "17580");
            writer.WriteElementString("WindowTopX", "120");
            writer.WriteElementString("WindowTopY", "60");
            writer.WriteElementString("ProtectStructure", "False");
            writer.WriteElementString("ProtectWindows", "False");

            // </ExcelWorkbook>
            writer.WriteEndElement();

            // <Styles>
            writer.WriteStartElement("Styles");

            // <Style ss:ID="Default" ss:Name="Normal">
            writer.WriteStartElement("Style");
            writer.WriteAttributeString("ss", "ID", null, "Default");
            writer.WriteAttributeString("ss", "Name", null, "Normal");

            // <Alignment ss:Vertical="Bottom"/>
            writer.WriteStartElement("Alignment");
            writer.WriteAttributeString("ss", "Vertical", null, "Bottom");
            writer.WriteEndElement();

            // Verbleibende Sytle-Eigenschaften leer schreiben
            writer.WriteElementString("Borders", null);
            writer.WriteElementString("Font", null);
            writer.WriteElementString("Interior", null);
            writer.WriteElementString("NumberFormat", null);
            writer.WriteElementString("Protection", null);

            // </Style>
            writer.WriteEndElement();

            // </Styles>
            writer.WriteEndElement();

            // <Worksheet ss:Name="xxx">
            writer.WriteStartElement("Worksheet");
            writer.WriteAttributeString("ss", "Name", null, dataSource.TableName);

            // <Table ss:ExpandedColumnCount="2" ss:ExpandedRowCount="3" x:FullColumns="1" x:FullRows="1" ss:DefaultColumnWidth="60">
            writer.WriteStartElement("Table");
            writer.WriteAttributeString("ss", "ExpandedColumnCount", null, dataSource.Columns.Count.ToString());
            writer.WriteAttributeString("ss", "ExpandedRowCount", null, (dataSource.Rows.Count + 1).ToString());
            writer.WriteAttributeString("x", "FullColumns", null, "1");
            writer.WriteAttributeString("x", "FullRows", null, "1");
            writer.WriteAttributeString("ss", "DefaultColumnWidth", null, "60");

            // schreiben der Überschriften

            writer.WriteStartElement("Row");

            // Alle Spalten durchlaufen
            foreach (object cellValue in dataSource.Columns)
            {
                // <Cell>
                writer.WriteStartElement("Cell");

                // <Data ss:Type="String">xxx</Data>
                writer.WriteStartElement("Data");
                writer.WriteAttributeString("ss", "Type", null, "String");

                //string strColDataType = ((DataColumn)cellValue).DataType.Name.ToString();
                //writer.WriteAttributeString("ss", "Type", null, strColDataType);

                // Zelleninhakt schreiben
                object tmpValue2 = cellValue;
                if (tmpValue2.GetType() == typeof(DBNull))
                    tmpValue2 = string.Empty;
                writer.WriteValue(((DataColumn)tmpValue2).ColumnName);

                // </Data>
                writer.WriteEndElement();

                // </Cell>
                writer.WriteEndElement();
            }
            // </Row>
            writer.WriteEndElement();


            // Alle Zeilen der Datenquelle durchlaufen
            foreach (DataRow row in dataSource.Rows)
            {
                // <Row>
                writer.WriteStartElement("Row");

                // Alle Zellen der aktuellen Zeile durchlaufen
                foreach (object cellValue in row.ItemArray)
                {
                    // <Cell>
                    writer.WriteStartElement("Cell");

                    // <Data ss:Type="String">xxx</Data>
                    writer.WriteStartElement("Data");
                    writer.WriteAttributeString("ss", "Type", null, "String");


                    //string strCellDataType = ((DataRow)cellValue). //.ToString();
                    //writer.WriteAttributeString("ss", "Type", null, strCellDataType);

                    // Zelleninhakt schreiben
                    object tmpValue = cellValue;
                    if (tmpValue.GetType() == typeof(DBNull))
                        tmpValue = string.Empty;
                    writer.WriteValue(tmpValue);

                    // </Data>
                    writer.WriteEndElement();

                    // </Cell>
                    writer.WriteEndElement();
                }
                // </Row>
                writer.WriteEndElement();
            }
            // </Table>
            writer.WriteEndElement();

            // <WorksheetOptions xmlns="urn:schemas-microsoft-com:office:excel">
            writer.WriteStartElement("WorksheetOptions", "urn:schemas-microsoft-com:office:excel");

            // Seiteneinstellungen schreiben
            writer.WriteStartElement("PageSetup");
            writer.WriteStartElement("Header");
            writer.WriteAttributeString("x", "Margin", null, "0.4921259845");
            writer.WriteEndElement();
            writer.WriteStartElement("Footer");
            writer.WriteAttributeString("x", "Margin", null, "0.4921259845");
            writer.WriteEndElement();
            writer.WriteStartElement("PageMargins");
            writer.WriteAttributeString("x", "Bottom", null, "0.984251969");
            writer.WriteAttributeString("x", "Left", null, "0.78740157499999996");
            writer.WriteAttributeString("x", "Right", null, "0.78740157499999996");
            writer.WriteAttributeString("x", "Top", null, "0.984251969");
            writer.WriteEndElement();
            writer.WriteEndElement();

            // <Selected/>
            writer.WriteElementString("Selected", null);

            // <Panes>
            writer.WriteStartElement("Panes");

            // <Pane>
            writer.WriteStartElement("Pane");

            // Bereichseigenschaften schreiben
            writer.WriteElementString("Number", "1");
            writer.WriteElementString("ActiveRow", "1");
            writer.WriteElementString("ActiveCol", "1");

            // </Pane>
            writer.WriteEndElement();

            // </Panes>
            writer.WriteEndElement();

            // <ProtectObjects>False</ProtectObjects>
            writer.WriteElementString("ProtectObjects", "False");

            // <ProtectScenarios>False</ProtectScenarios>
            writer.WriteElementString("ProtectScenarios", "False");

            // </WorksheetOptions>
            writer.WriteEndElement();

            // </Worksheet>
            writer.WriteEndElement();

            // </Workbook>
            writer.WriteEndElement();

            // Datei auf Festplatte schreiben
            writer.Flush();
            writer.Close();
            return fileName;
        }
        public string ExportDataTableToExcel(DataTable dt, string myExportName, string myAttachmentPath = null)//ref frmMAIN myFrmMain, DataTable dt, string myExportName)
        {
            string filename = string.Empty;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "(*.xls)|*.xls|All files(*.*)|*.*";
            sfd.Title = "ExelExport";
            sfd.FileName = DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + "_" + myExportName + ".xls";
            sfd.OverwritePrompt = true;
            if (myAttachmentPath != null || sfd.ShowDialog() == DialogResult.OK)
            {
                //myFrmMain.ResetStatusBar();
                //myFrmMain.InitStatusBar(dt.Rows.Count);
                //myFrmMain.StatusLabelInfoUpdate("Daten werden an Excel übergeben...");
                if (myAttachmentPath == null)
                {
                    filename = sfd.FileName;
                }
                else
                {
                    filename = myAttachmentPath + "\\" + sfd.FileName;
                }

                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;

                //xlApp = new Excel.ApplicationClass();
                xlApp = new Excel.Application();  //mr ab 18.10.2023 wg update VS
                xlWorkBook = xlApp.Workbooks.Add(misValue);

                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                Excel.Range workSheetRange = xlWorkSheet.get_Range(xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[1, dt.Columns.Count]);
                workSheetRange.Font.Bold = true;
                //workSheetRange.Font.Italic = italic;
                //workSheetRange.Font.Color = color.ToArgb();
                //workSheetRange.Font.Underline = underline;


                //Daten aus DataTable in Worksheet
                //Spalten werden angelegt
                for (Int32 j = 1; j < dt.Columns.Count; j++)
                {
                    //string test = dt.Columns[j].ColumnName.ToString();
                    xlWorkSheet.Cells[1, j] = dt.Columns[j].ColumnName.ToString();


                    //Excel.Range r = xlWorkSheet.get_Range(xlWorkSheet.Cells[1, j]).EntireColumn;
                    //r.Font.Bold = true;
                    //r.Interior.Color = System.Drawing.Color.Beige;

                    //string strDataType = dt.Columns[j].DataType.ToString();

                    //switch (strDataType)
                    //{
                    //    case "System.Decimal":
                    //        r.NumberFormat = "#.##0,00";
                    //        break;

                    //    case "System.Int32":
                    //        r.NumberFormat = "0";
                    //        break;

                    //    case "System.DateTime":
                    //        r.NumberFormat = "dd.MM.yyyy HH:mm:ss";
                    //        break;
                    //    default:
                    //        r.NumberFormat = "@";
                    //        break;
                    //}

                }

                for (Int32 i = 2; i < dt.Rows.Count + 2; i++)
                {
                    for (Int32 j = 1; j < dt.Columns.Count; j++)
                    {
                        //string test = dt.Rows[i - 2][j].ToString();
                        xlWorkSheet.Cells[i, j] = dt.Rows[i - 2][j];


                        //Excel.Range r = xlWorkSheet.UsedRange;
                        //r.Font.Bold = false;
                        //// r.Interior.Color = System.Drawing.Color.Beige;
                        //string strDataType1 = dt.Columns[j].DataType.ToString();

                        //switch (strDataType1)
                        //{
                        //    case "System.Decimal":
                        //        r.NumberFormat = "#.##0,00";
                        //        r.HorizontalAlignment = Excel.XlVAlign.xlVAlignTop;
                        //        r.VerticalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        //        break;

                        //    case "System.Int32":
                        //        r.NumberFormat = "0";
                        //        r.HorizontalAlignment = Excel.XlVAlign.xlVAlignTop;
                        //        r.VerticalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        //        break;

                        //    case "System.DateTime":
                        //        //r.NumberFormat = "dd.MM.yyyy HH:mm:ss";
                        //        r.NumberFormat = "dd.MM.yyyy";
                        //        r.HorizontalAlignment = Excel.XlVAlign.xlVAlignTop;
                        //        r.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        //        break;
                        //    default:
                        //        r.NumberFormat = "@";
                        //        r.HorizontalAlignment = Excel.XlVAlign.xlVAlignTop;
                        //        r.VerticalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        //        break;
                        //}


                    }
                    //myFrmMain.StatusBarWork(true, string.Empty);
                }

                xlWorkBook.SaveAs(filename, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);

                //myFrmMain.StatusLabelInfoUpdate("Daten nach Excel exportiert....");

                if (myAttachmentPath == null && clsMessages.Export_OpenFileInExcel())
                {
                    try
                    {
                        System.Diagnostics.Process.Start(filename);
                    }
                    catch (Exception ex)
                    {
                        clsMessages.Allgemein_FileCanNotOpen();
                    }
                    finally
                    {
                        //myFrmMain.ResetStatusBar();
                    }
                }
            }
            return filename;
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