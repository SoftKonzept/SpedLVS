using LVS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;


namespace Sped4
{
    public partial class ctrComLog : UserControl
    {
        public Globals._GL_USER GL_User;
        internal ctrMenu _ctrMenu;
        string LogArt = string.Empty;



        public ctrComLog()
        {
            InitializeComponent();
        }
        ///<summary>ctrComLog / ctrComLog_Load</summary>
        ///<remarks></remarks>
        private void ctrComLog_Load(object sender, EventArgs e)
        {
            this.Calender.SelectedDate = DateTime.Now;
            LogArt = clsLogbuchCon.const_LogASNReadFileName;
        }
        ///<summary>ctrComLog / tsbtnClose_Click</summary>
        ///<remarks></remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this._ctrMenu.CloseCtrCOMLog();
        }
        ///<summary>ctrComLog / ctrComLog_Load</summary>
        ///<remarks></remarks>
        private string CreateLogPath()
        {
            string strPath = this._ctrMenu._frmMain.system.ComPath;
            if (strPath != string.Empty)
            {
                //Ordern
                strPath = strPath + "\\Log\\" + this.Calender.SelectedDate.ToString("yyyy") + "\\" + this.Calender.SelectedDate.ToString("MM") + "\\";
                //Datei
                strPath = strPath + this.Calender.SelectedDate.ToString("yyyy") +
                                    "_" + this.Calender.SelectedDate.ToString("MM") +
                                    "_" + this.Calender.SelectedDate.ToString("dd") +
                                    "_" + LogArt + ".txt";

            }
            return strPath;
        }
        ///<summary>ctrComLog / GetSelLog</summary>
        ///<remarks></remarks>
        private void GetSelectedLog()
        {
            string strFilePaht = CreateLogPath();
            if (File.Exists(strFilePaht))
            {
                List<string> listFileByRow = new List<string>();
                //Protokolldatei einlesen
                StreamReader readFile = new StreamReader(strFilePaht);
                string strLine = string.Empty;
                while ((strLine = readFile.ReadLine()) != null)
                {
                    if (strLine != string.Empty)
                    {
                        listFileByRow.Add(strLine);
                    }
                }
                readFile.Close();
                //Ausgabe füllen
                tbComLog.Text = string.Empty;
                //Überschriften
                string strTxt = "Protokoll vom; " + this.Calender.SelectedDate.ToString("dd.MM.yyyy") + Environment.NewLine;
                strTxt = strTxt + "Pfad: " + strFilePaht + Environment.NewLine + Environment.NewLine;
                //Protokoll
                for (Int32 i = 0; i <= listFileByRow.Count - 1; i++)
                {
                    strTxt = strTxt + listFileByRow[i].ToString() + Environment.NewLine;
                }

                //Ausgabe 
                tbComLog.Text = strTxt;
            }
            else
            {
                tbComLog.Text = "Für das gewählte Datum ist keine Protokolldatei vorhanden!";
            }
        }
        ///<summary>ctrComLog / toolStripLabel1_Click</summary>
        ///<remarks></remarks>
        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            LogArt = clsLogbuchCon.const_LogASNReadFileName;
            GetSelectedLog();
        }
        ///<summary>ctrComLog / tslLogOUT_Click</summary>
        ///<remarks></remarks>
        private void tslLogOUT_Click(object sender, EventArgs e)
        {
            LogArt = clsLogbuchCon.const_LogASNWriteFileName;
            GetSelectedLog();
        }

        private void Calender_SelectionChanged(object sender, EventArgs e)
        {
            string strTmp = Calender.SelectedDate.ToString();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }


    }
}
