using Common.Enumerations;
using LVS.Dokumente;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Text;
//using System.Windows.Forms;


namespace LVS
{
    public class clsDruck
    {
        private string _Path;
        private string _filename;
        private string _DateiEndung;
        private decimal _AuftragID;
        private decimal _AuftragPos;
        private decimal _AuftragPosTableID;
        private docFrachtauftragSU _Report;
        internal Globals._GL_USER GL_User;
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = GL_User.User_ID;
                return _BenutzerID;
            }
            set { _BenutzerID = value; }
        }


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
        public decimal AuftragPos
        {
            get { return _AuftragPos; }
            set { _AuftragPos = value; }
        }
        public decimal AuftragID
        {
            get { return _AuftragID; }
            set { _AuftragID = value; }
        }
        public docFrachtauftragSU Report
        {
            get { return _Report; }
            set { _Report = value; }
        }

        public decimal AuftragPosTableID
        {
            get { return _AuftragPosTableID; }
            set { _AuftragPosTableID = value; }
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
            //string mimType = string.Empty;
            //string extension = string.Empty;
            //Encoding encoding = null;
            //if (GetSavePath("Report1", ".pdf") == true)
            //{
            //    if (Path != null)
            //    {
            //        byte[] buffer = Telerik.Reporting.Processing.ReportProcessor.Render("PDF", Report, null, out mimType, out extension, out encoding);

            //        FileStream fs = new FileStream(Path, FileMode.Create);
            //        fs.Write(buffer, 0, buffer.Length);
            //        fs.Flush();
            //        fs.Close();

            //        //speichern des Auftrags in AuftragScan DB
            //        SaveAuftragDocSUAuftragScan();
            //    }
            //}
        }
        //
        //-------------- SAVE FILE Dialog ---------------------------------
        //
        private bool GetSavePath(string Dateiname, string DateiEndung)
        {
            //SaveFileDialog fd = new SaveFileDialog();
            //string path = Application.StartupPath + "\\" + Dateiname + DateiEndung;
            //fd.FileName = filename;
            //fd.InitialDirectory = path;
            //fd.DefaultExt = "pdf";
            //fd.Filter = "All files (*.*)|*.*";
            //if (fd.ShowDialog() == DialogResult.OK)
            //{
            //    Path = fd.FileName;
            //    fd.Dispose();
            //    return true;
            //}
            //else
            //{
            //    Path = string.Empty;
            //    fd.Dispose();
            //    return false;
            //}
            return false;
        }
        //
        //------------ Auftrag an SU in DB Speichern ---------------------
        //
        private void SaveAuftragDocSUAuftragScan()
        {
            if (Report != null)
            {
                Image img;
                string mimType = string.Empty;
                string extesion = string.Empty;

                Encoding encoding = null;
                Hashtable deviceInfo = new Hashtable();
                deviceInfo["OutputFormat"] = "JPEG";
                byte[] buffer = Telerik.Reporting.Processing.ReportProcessor.Render("IMAGE", Report, deviceInfo, out mimType, out extesion, out encoding);

                using (MemoryStream ms = new MemoryStream(buffer))
                {
                    Image SUimage = Image.FromStream(ms);

                    clsDocScan lfs = new clsDocScan();
                    lfs._GL_User = this.GL_User;
                    lfs.m_dec_AuftragID = clsAuftrag.GetAuftragTableIDByAuftragPosTableID(this.GL_User, _AuftragPosTableID);
                    lfs.m_dec_LAusgangID = 0;
                    lfs.m_dec_LEingangID = 0;
                    lfs.m_i_picnum = 1 + (lfs.GetMaxPicNumByAuftrag());
                    lfs.m_str_ImageArt = enumDokumentenArt.FrachtauftragAnSU.ToString();
                    lfs.AuftragImageIn = SUimage;  // der Ausdruck
                    lfs.AddDocScan();
                    lfs.SaveScanDocToHDD();
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
