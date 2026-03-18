namespace LVS.Dokumente
{
    /// <summary>
    /// Summary description for docBriefkopf.
    /// </summary>
    public partial class docBriefkopf : Telerik.Reporting.Report
    {
        public Globals._GL_SYSTEM GL_System;
        public docBriefkopf()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        private void InitBriefkopf()
        {
            //Check Client
            switch (GL_System.Client)
            {
                case "COMTEC_":
                    docBriefkopfComtecLager bkCOMTEC = new docBriefkopfComtecLager();
                    //this = bkCOMTEC;
                    break;

                case "Heisiep_":
                    docBriefkopfHeisiepLager bkHeisiep = new docBriefkopfHeisiepLager();
                    // this = bkHeisiep;
                    break;

                case "SZG":
                    break;

            }

        }
    }
}