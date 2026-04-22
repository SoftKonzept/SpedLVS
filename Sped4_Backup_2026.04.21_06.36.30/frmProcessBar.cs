using System;
using System.Windows.Forms;

namespace Sped4
{

    public partial class frmProcessBar : frmTEMPLATE
    {
        //private BouncingProgressBar.BouncingProgressBar pBar;

        public Int32 GValue = 100;

        public frmProcessBar()
        {
            InitializeComponent();
            // pBar = new BouncingProgressBar.BouncingProgressBar();

            this.Width = 225;
        }
        //
        //
        //
        /***
            public void StartProcessBar()
            {
                pBar.BlockWidth = 12;
                pBar.BouncingSpeed = 10;

                pBar.Start();
            }
            //
            //
            //
            public void StopProcessBar()
            {
                if (pBar.Started)
                {
                    pBar.Stop();
                }
            }

            private void button1_Click(object sender, EventArgs e)
            {
                pBar.Start();
            }
            ***/
        //
        public void ChangeProcessValue(Int32 iFortschritt)
        {
            if (progressBar1.Value < GValue)
            {
                progressBar1.Value = progressBar1.Value + iFortschritt;
                progressBar1.Refresh();
                this.Refresh();
                this.BringToFront();
            }
            else
            {
                this.Close();
            }
        }
        //
        //
        //
        private void frmProcessBar_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            //this.progressBar1.Value = GValue;
            this.BringToFront();
        }

    }
}
