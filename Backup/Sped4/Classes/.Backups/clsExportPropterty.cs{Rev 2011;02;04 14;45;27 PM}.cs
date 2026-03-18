using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Windows.Forms;
using Sped4.Classes;
using Sped4;
using RKLib.ExportData;
using System.IO;


namespace Sped4.Classes
{
    class clsExportPropterty
    {
       public bool PfadOK = true;
        private string _exExcel_Pfad;
        private string _exExcel_Filename;
        private DataTable _dataTable;
        





        public string exExcel_Pfad
        {
            get
            {
              System.Windows.Forms.FolderBrowserDialog objDialog = new FolderBrowserDialog();
              objDialog.SelectedPath =@"C:\";
              _exExcel_Pfad =objDialog.SelectedPath;
              if (_exExcel_Pfad == objDialog.SelectedPath)
              {
                objDialog.Description = "Pfad für Excelexport";
                DialogResult objResult = objDialog.ShowDialog();

                if (objResult == DialogResult.OK)
                {
                  _exExcel_Pfad = objDialog.SelectedPath;
                }
                else
                {
                  PfadOK = false;
                }
              }
                return _exExcel_Pfad; 
            }
            set { _exExcel_Pfad = value; }
        }
        public DataTable dataTable
        {
            get { return _dataTable; }
            set { _dataTable = value; }
        }
        public string exExcel_Filename
        {
          get 
          {
            _exExcel_Filename = "\\ExcelExport.xls";
            return _exExcel_Filename; 
          }
          set { _exExcel_Filename = value; }
        }
        //
        //------------------- Excel export  -----------
        //
        public void ExportDataTableToExcel()
        {
         
          while(PfadOK)
          {
            RKLib.ExportData.Export ExcelExport = new RKLib.ExportData.Export("Win");
            string exportPfad = exExcel_Pfad+exExcel_Filename;
            DataTable testTable = new DataTable();
            ExcelExport.ExportDetails(dataTable,Export.ExportFormat.Excel,exportPfad);

            if (File.Exists(exportPfad))
            {
              System.Diagnostics.Process.Start(exportPfad);
              PfadOK = false;
            }
          }
        }

    }
}
