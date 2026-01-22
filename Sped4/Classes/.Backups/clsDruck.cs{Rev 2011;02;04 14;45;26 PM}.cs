using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using Sped4;
using Sped4.Classes;
using Sped4.Dokumente;
using Telerik;
using Telerik.Reporting;


namespace Sped4.Classes
{
    class clsDruck
    {
        private string _Path;
        private string _filename;
        private string _DateiEndung;
        private Int32 _AuftragID;
        private Int32 _AuftragPos;
        private docFrachtauftragSU _Report;




    public string Path
    {
      get { return _Path; }
      set { _Path = value; }
    }
    public string filename
    {
      get { return _filename; }
      set { _filename = value; }
    }
    public string DateiEndung
    {
        get { return _DateiEndung; }
        set { _DateiEndung = value; }
    }
    public Int32 AuftragPos
    {
        get { return _AuftragPos; }
        set { _AuftragPos = value; }
    }
    public Int32 AuftragID
    {
        get { return _AuftragID; }
        set { _AuftragID = value; }
    }
    public docFrachtauftragSU Report
    {
        get { return _Report; }
        set { _Report = value; }
    }





        /******************************************************************************************
         * 
         * 
         * 
         * *****************************************************************************************/

        //
        //------------------- PDF Ausgabe Report ---------------------------------------
        //
        public void ReportToPDF()
        {
           string mimType = string.Empty;
           string extension = string.Empty;
           Encoding encoding = null;
           if (GetSavePath("Report1", ".pdf")==true)
           {
               if (Path != null)
               {
                   byte[] buffer = Telerik.Reporting.Processing.ReportProcessor.Render("PDF", Report, null, out mimType, out extension, out encoding);
                   FileStream fs = new FileStream(Path, FileMode.Create);
                   fs.Write(buffer, 0, buffer.Length);
                   fs.Flush();
                   fs.Close();

                   //speichern des Auftrags in AuftragScan DB
                   SaveAuftragDocSUAuftragScan();
               }
           }     
        }
        //
        //-------------- SAVE FILE Dialog ---------------------------------
        //
        private bool GetSavePath(string Dateiname, string DateiEndung)
        {           
          SaveFileDialog fd = new SaveFileDialog();
          string path = Application.StartupPath+"\\"+Dateiname+DateiEndung;
          fd.FileName = filename;
          fd.InitialDirectory = path;
          fd.DefaultExt="pdf";
          fd.Filter = "All files (*.*)|*.*";
          if (fd.ShowDialog() == DialogResult.OK)
          {
              Path = fd.FileName;
              return true;
          }
          else
          {
              Path = string.Empty;
              return false;

          }
          fd.Dispose();
        }
        //
        //------------ Auftrag an SU in DB Speichern ---------------------
        //
        private void SaveAuftragDocSUAuftragScan()
        {
            if(Report!=null)
            {
                Image img;
                string mimType = string.Empty;
                string extesion = string.Empty;

                Encoding encoding = null;
                Hashtable deviceInfo = new Hashtable();
                deviceInfo["OutputFormat"]="JPEG";
                byte[] buffer = Telerik.Reporting.Processing.ReportProcessor.Render("IMAGE", Report, deviceInfo, out mimType, out extesion, out encoding);

                using (MemoryStream ms = new MemoryStream(buffer))
                {
                   img = Image.FromStream(ms);
               
                   clsAuftragScan scan = new clsAuftragScan();
                    scan.AuftragImageIn = img;
                    scan.m_i_AuftragID = AuftragID;
                    scan.m_i_AuftragPos = AuftragPos;
                    scan.m_i_picnum = scan.GetMaxPicNumByAuftrag()+1;
                    scan.m_str_Filename = AuftragID + "_" + AuftragPos + ".jpg";
                    scan.m_str_ImageArt = Globals.enumImageArt.Subunternehmerauftrag.ToString();
                    scan.WriteToAuftragImage();
                }
            }
        }
        //
        //
        //
        public void PrintReport()
        {
            if (Report != null)
            { 
                
            }
        
        }



    }
}
